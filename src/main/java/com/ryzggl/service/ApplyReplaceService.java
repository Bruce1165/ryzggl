package com.ryzggl.service;

import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.ryzggl.entity.ApplyReplace;
import com.ryzggl.entity.Certificate;
import com.ryzggl.repository.ApplyReplaceRepository;
import com.ryzggl.common.Result;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.List;

/**
 * ApplyReplace Service - Business Logic Layer
 * Maps to: ApplyReplaceDAL.cs logic and workflow management
 */
@Service
@Transactional
public class ApplyReplaceService extends ServiceImpl<ApplyReplaceRepository, ApplyReplace> {

    private static final Logger log = LoggerFactory.getLogger(ApplyReplaceService.class);

    @Autowired
    private CertificateService certificateService;

    /**
     * Create new replacement application
     * Maps to: ApplyReplaceDAL.AddApplyReplace
     */
    public Result<Void> createApplyReplace(ApplyReplace applyReplace) {
        log.info("Creating replacement application for certificate: {}", applyReplace.getCertificateId());

        // Validate certificate exists
        Certificate certificate = certificateService.getById(applyReplace.getCertificateId());
        if (certificate == null) {
            log.error("Certificate not found: {}", applyReplace.getCertificateId());
            return Result.error("证书不存在");
        }

        // Validate certificate belongs to worker
        if (!certificate.getWorkerId().equals(applyReplace.getWorkerId())) {
            log.error("Certificate {} does not belong to worker {}", certificate.getId(), applyReplace.getWorkerId());
            return Result.error("证书与人员不匹配");
        }

        // Set old certificate code from current certificate
        applyReplace.setOldCertificateCode(certificate.getCertificateCode());

        // Set initial status
        applyReplace.setApplyStatus("未填写");

        boolean success = save(applyReplace);

        if (success) {
            log.info("Replacement application created successfully");
            return Result.success();
        } else {
            log.error("Failed to create replacement application");
            return Result.error("替换申请创建失败");
        }
    }

    /**
     * Get replacement application by ID
     */
    public Result<ApplyReplace> getApplyReplaceById(Long applyReplaceId) {
        log.debug("Getting replacement application: {}", applyReplaceId);
        ApplyReplace applyReplace = getById(applyReplaceId);

        if (applyReplace == null) {
            return Result.error("替换申请不存在");
        }

        return Result.success(applyReplace);
    }

    /**
     * List replacement applications with pagination
     * Maps to: ApplyReplaceDAL.GetApplyReplaceList
     */
    public Result<List<ApplyReplace>> listApplyReplaces(Long workerId, String status) {
        log.debug("Listing replacement applications: workerId={}, status={}", workerId, status);

        if (workerId != null) {
            List<ApplyReplace> applyReplaces = lambdaQuery()
                    .eq(ApplyReplace::getWorkerId, workerId)
                    .eq(status != null, ApplyReplace::getApplyStatus, status)
                    .orderByDesc(ApplyReplace::getCreateTime)
                    .list();
            return Result.success(applyReplaces);
        } else {
            List<ApplyReplace> applyReplaces = lambdaQuery()
                    .eq(status != null, ApplyReplace::getApplyStatus, status)
                    .orderByDesc(ApplyReplace::getCreateTime)
                    .list();
            return Result.success(applyReplaces);
        }
    }

    /**
     * Submit replacement application for review
     * Maps to: ApplyReplaceDAL.SubmitApplyReplace
     */
    public Result<Void> submitApplyReplace(Long applyReplaceId) {
        log.info("Submitting replacement application: {}", applyReplaceId);

        ApplyReplace applyReplace = getById(applyReplaceId);
        if (applyReplace == null) {
            return Result.error("替换申请不存在");
        }

        if (!"未填写".equals(applyReplace.getApplyStatus())) {
            return Result.error("只有未填写的申请才能提交");
        }

        applyReplace.setApplyStatus("待确认");
        boolean success = updateById(applyReplace);

        if (success) {
            log.info("Replacement application submitted successfully");
            return Result.success();
        } else {
            return Result.error("替换申请提交失败");
        }
    }

    /**
     * Approve replacement application
     * Maps to: ApplyReplaceDAL.ApproveApplyReplace
     */
    public Result<Void> approveApplyReplace(Long applyReplaceId, String checkMan, String checkAdvise) {
        log.info("Approving replacement application: {}, checkMan: {}, advise: {}", applyReplaceId, checkMan, checkAdvise);

        ApplyReplace applyReplace = getById(applyReplaceId);
        if (applyReplace == null) {
            return Result.error("替换申请不存在");
        }

        if (!"待确认".equals(applyReplace.getApplyStatus())) {
            return Result.error("只有待确认的申请才能通过");
        }

        // Update certificate code
        Certificate certificate = certificateService.getById(applyReplace.getCertificateId());
        if (certificate != null && applyReplace.getCertificateCode() != null) {
            certificate.setCertificateCode(applyReplace.getCertificateCode());
            certificateService.updateById(certificate);
            log.info("Certificate {} code updated to {}", certificate.getId(), applyReplace.getCertificateCode());
        }

        applyReplace.setApplyStatus("已受理");
        applyReplace.setCheckMan(checkMan);
        applyReplace.setCheckAdvise(checkAdvise);
        applyReplace.setCheckDate(LocalDate.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd")));
        boolean success = updateById(applyReplace);

        if (success) {
            log.info("Replacement application approved successfully");
            return Result.success();
        } else {
            return Result.error("替换申请审核失败");
        }
    }

    /**
     * Reject replacement application
     * Maps to: ApplyReplaceDAL.RejectApplyReplace
     */
    public Result<Void> rejectApplyReplace(Long applyReplaceId, String rejectionReason) {
        log.info("Rejecting replacement application: {}, reason: {}", applyReplaceId, rejectionReason);

        ApplyReplace applyReplace = getById(applyReplaceId);
        if (applyReplace == null) {
            return Result.error("替换申请不存在");
        }

        if (!"待确认".equals(applyReplace.getApplyStatus())) {
            return Result.error("只有待确认的申请才能驳回");
        }

        applyReplace.setApplyStatus("已驳回");
        applyReplace.setCheckAdvise(rejectionReason);
        boolean success = updateById(applyReplace);

        if (success) {
            log.info("Replacement application rejected successfully");
            return Result.success();
        } else {
            return Result.error("替换申请驳回失败");
        }
    }

    /**
     * Get pending replacement applications for approval
     * Maps to: ApplyReplaceDAL.GetPendingApplyReplaces
     */
    public Result<List<ApplyReplace>> getPendingApplyReplaces() {
        log.debug("Getting pending replacement applications");

        List<ApplyReplace> applyReplaces = lambdaQuery()
                .eq(ApplyReplace::getApplyStatus, "待确认")
                .orderByAsc(ApplyReplace::getCreateTime)
                .list();

        return Result.success(applyReplaces);
    }

    /**
     * Get replacement applications by status
     */
    public Result<List<ApplyReplace>> getApplyReplacesByStatus(String status) {
        log.debug("Getting replacement applications by status: {}", status);

        List<ApplyReplace> applyReplaces = lambdaQuery()
                .eq(ApplyReplace::getApplyStatus, status)
                .orderByDesc(ApplyReplace::getCreateTime)
                .list();

        return Result.success(applyReplaces);
    }

    /**
     * Get replacement applications by certificate
     */
    public Result<List<ApplyReplace>> getApplyReplacesByCertificate(Long certificateId) {
        log.debug("Getting replacement applications for certificate: {}", certificateId);

        List<ApplyReplace> applyReplaces = baseMapper.findByCertificateId(certificateId);

        return Result.success(applyReplaces);
    }
}
