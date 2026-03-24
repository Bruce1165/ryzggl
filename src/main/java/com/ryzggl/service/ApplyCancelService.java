package com.ryzggl.service;

import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.ryzggl.entity.ApplyCancel;
import com.ryzggl.entity.Certificate;
import com.ryzggl.repository.ApplyCancelRepository;
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
 * ApplyCancel Service - Business Logic Layer
 * Maps to: ApplyCancelDAL.cs logic and workflow management
 */
@Service
@Transactional
public class ApplyCancelService extends ServiceImpl<ApplyCancelRepository, ApplyCancel> {

    private static final Logger log = LoggerFactory.getLogger(ApplyCancelService.class);

    @Autowired
    private CertificateService certificateService;

    /**
     * Create new cancellation application
     * Maps to: ApplyCancelDAL.AddApplyCancel
     */
    public Result<Void> createApplyCancel(ApplyCancel applyCancel) {
        log.info("Creating cancellation application for certificate: {}", applyCancel.getCertificateId());

        // Validate certificate exists
        Certificate certificate = certificateService.getById(applyCancel.getCertificateId());
        if (certificate == null) {
            log.error("Certificate not found: {}", applyCancel.getCertificateId());
            return Result.error("证书不存在");
        }

        // Validate certificate belongs to worker
        if (!certificate.getWorkerId().equals(applyCancel.getWorkerId())) {
            log.error("Certificate {} does not belong to worker {}", certificate.getId(), applyCancel.getWorkerId());
            return Result.error("证书与人员不匹配");
        }

        // Set initial status
        applyCancel.setApplyStatus("未填写");

        boolean success = save(applyCancel);

        if (success) {
            log.info("Cancellation application created successfully");
            return Result.success();
        } else {
            log.error("Failed to create cancellation application");
            return Result.error("取消申请创建失败");
        }
    }

    /**
     * Get cancellation application by ID
     */
    public Result<ApplyCancel> getApplyCancelById(Long applyCancelId) {
        log.debug("Getting cancellation application: {}", applyCancelId);
        ApplyCancel applyCancel = getById(applyCancelId);

        if (applyCancel == null) {
            return Result.error("取消申请不存在");
        }

        return Result.success(applyCancel);
    }

    /**
     * List cancellation applications with pagination
     * Maps to: ApplyCancelDAL.GetApplyCancelList
     */
    public Result<List<ApplyCancel>> listApplyCancels(Long workerId, String status) {
        log.debug("Listing cancellation applications: workerId={}, status={}", workerId, status);

        if (workerId != null) {
            List<ApplyCancel> applyCancels = lambdaQuery()
                    .eq(ApplyCancel::getWorkerId, workerId)
                    .eq(status != null, ApplyCancel::getApplyStatus, status)
                    .orderByDesc(ApplyCancel::getCreateTime)
                    .list();
            return Result.success(applyCancels);
        } else {
            List<ApplyCancel> applyCancels = lambdaQuery()
                    .eq(status != null, ApplyCancel::getApplyStatus, status)
                    .orderByDesc(ApplyCancel::getCreateTime)
                    .list();
            return Result.success(applyCancels);
        }
    }

    /**
     * Submit cancellation application for review
     * Maps to: ApplyCancelDAL.SubmitApplyCancel
     */
    public Result<Void> submitApplyCancel(Long applyCancelId) {
        log.info("Submitting cancellation application: {}", applyCancelId);

        ApplyCancel applyCancel = getById(applyCancelId);
        if (applyCancel == null) {
            return Result.error("取消申请不存在");
        }

        if (!"未填写".equals(applyCancel.getApplyStatus())) {
            return Result.error("只有未填写的申请才能提交");
        }

        // Set cancel date to current date
        applyCancel.setCancelDate(LocalDate.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd")));
        applyCancel.setApplyStatus("待确认");
        boolean success = updateById(applyCancel);

        if (success) {
            log.info("Cancellation application submitted successfully");
            return Result.success();
        } else {
            return Result.error("取消申请提交失败");
        }
    }

    /**
     * Approve cancellation application
     * Maps to: ApplyCancelDAL.ApproveApplyCancel
     */
    public Result<Void> approveApplyCancel(Long applyCancelId, String checkMan, String checkAdvise) {
        log.info("Approving cancellation application: {}, checkMan: {}, advise: {}", applyCancelId, checkMan, checkAdvise);

        ApplyCancel applyCancel = getById(applyCancelId);
        if (applyCancel == null) {
            return Result.error("取消申请不存在");
        }

        if (!"待确认".equals(applyCancel.getApplyStatus())) {
            return Result.error("只有待确认的申请才能通过");
        }

        // Update certificate status to cancelled
        Certificate certificate = certificateService.getById(applyCancel.getCertificateId());
        if (certificate != null) {
            certificate.setStatus("已注销");
            certificateService.updateById(certificate);
            log.info("Certificate {} status updated to cancelled", certificate.getId());
        }

        applyCancel.setApplyStatus("已受理");
        applyCancel.setCheckMan(checkMan);
        applyCancel.setCheckAdvise(checkAdvise);
        applyCancel.setCheckDate(LocalDate.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd")));
        boolean success = updateById(applyCancel);

        if (success) {
            log.info("Cancellation application approved successfully");
            return Result.success();
        } else {
            return Result.error("取消申请审核失败");
        }
    }

    /**
     * Reject cancellation application
     * Maps to: ApplyCancelDAL.RejectApplyCancel
     */
    public Result<Void> rejectApplyCancel(Long applyCancelId, String rejectionReason) {
        log.info("Rejecting cancellation application: {}, reason: {}", applyCancelId, rejectionReason);

        ApplyCancel applyCancel = getById(applyCancelId);
        if (applyCancel == null) {
            return Result.error("取消申请不存在");
        }

        if (!"待确认".equals(applyCancel.getApplyStatus())) {
            return Result.error("只有待确认的申请才能驳回");
        }

        applyCancel.setApplyStatus("已驳回");
        applyCancel.setCheckAdvise(rejectionReason);
        boolean success = updateById(applyCancel);

        if (success) {
            log.info("Cancellation application rejected successfully");
            return Result.success();
        } else {
            return Result.error("取消申请驳回失败");
        }
    }

    /**
     * Get pending cancellation applications for approval
     * Maps to: ApplyCancelDAL.GetPendingApplyCancels
     */
    public Result<List<ApplyCancel>> getPendingApplyCancels() {
        log.debug("Getting pending cancellation applications");

        List<ApplyCancel> applyCancels = lambdaQuery()
                .eq(ApplyCancel::getApplyStatus, "待确认")
                .orderByAsc(ApplyCancel::getCreateTime)
                .list();

        return Result.success(applyCancels);
    }

    /**
     * Get cancellation applications by status
     */
    public Result<List<ApplyCancel>> getApplyCancelsByStatus(String status) {
        log.debug("Getting cancellation applications by status: {}", status);

        List<ApplyCancel> applyCancels = lambdaQuery()
                .eq(ApplyCancel::getApplyStatus, status)
                .orderByDesc(ApplyCancel::getCreateTime)
                .list();

        return Result.success(applyCancels);
    }

    /**
     * Get cancellation applications by certificate
     */
    public Result<List<ApplyCancel>> getApplyCancelsByCertificate(Long certificateId) {
        log.debug("Getting cancellation applications for certificate: {}", certificateId);

        List<ApplyCancel> applyCancels = baseMapper.findByCertificateId(certificateId);

        return Result.success(applyCancels);
    }
}
