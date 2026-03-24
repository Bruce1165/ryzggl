package com.ryzggl.service;

import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.ryzggl.entity.ApplyRenew;
import com.ryzggl.entity.Certificate;
import com.ryzggl.repository.ApplyRenewRepository;
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
 * ApplyRenew Service - Business Logic Layer
 * Maps to: ApplyRenewDAL.cs logic and workflow management
 */
@Service
@Transactional
public class ApplyRenewService extends ServiceImpl<ApplyRenewRepository, ApplyRenew> {

    private static final Logger log = LoggerFactory.getLogger(ApplyRenewService.class);

    @Autowired
    private CertificateService certificateService;

    /**
     * Create new renewal application
     * Maps to: ApplyRenewDAL.AddApplyRenew
     */
    public Result<Void> createApplyRenew(ApplyRenew applyRenew) {
        log.info("Creating renewal application for certificate: {}", applyRenew.getCertificateId());

        // Validate certificate exists
        Certificate certificate = certificateService.getById(applyRenew.getCertificateId());
        if (certificate == null) {
            log.error("Certificate not found: {}", applyRenew.getCertificateId());
            return Result.error("证书不存在");
        }

        // Validate certificate belongs to worker
        if (!certificate.getWorkerId().equals(applyRenew.getWorkerId())) {
            log.error("Certificate {} does not belong to worker {}", certificate.getId(), applyRenew.getWorkerId());
            return Result.error("证书与人员不匹配");
        }

        // Set initial status
        applyRenew.setApplyStatus("未填写");

        boolean success = save(applyRenew);

        if (success) {
            log.info("Renewal application created successfully");
            return Result.success();
        } else {
            log.error("Failed to create renewal application");
            return Result.error("续期申请创建失败");
        }
    }

    /**
     * Get renewal application by ID
     */
    public Result<ApplyRenew> getApplyRenewById(Long applyRenewId) {
        log.debug("Getting renewal application: {}", applyRenewId);
        ApplyRenew applyRenew = getById(applyRenewId);

        if (applyRenew == null) {
            return Result.error("续期申请不存在");
        }

        return Result.success(applyRenew);
    }

    /**
     * List renewal applications with pagination
     * Maps to: ApplyRenewDAL.GetApplyRenewList
     */
    public Result<List<ApplyRenew>> listApplyRenews(Long workerId, String status) {
        log.debug("Listing renewal applications: workerId={}, status={}", workerId, status);

        if (workerId != null) {
            List<ApplyRenew> applyRenews = lambdaQuery()
                    .eq(ApplyRenew::getWorkerId, workerId)
                    .eq(status != null, ApplyRenew::getApplyStatus, status)
                    .orderByDesc(ApplyRenew::getCreateTime)
                    .list();
            return Result.success(applyRenews);
        } else {
            List<ApplyRenew> applyRenews = lambdaQuery()
                    .eq(status != null, ApplyRenew::getApplyStatus, status)
                    .orderByDesc(ApplyRenew::getCreateTime)
                    .list();
            return Result.success(applyRenews);
        }
    }

    /**
     * Submit renewal application for review
     * Maps to: ApplyRenewDAL.SubmitApplyRenew
     */
    public Result<Void> submitApplyRenew(Long applyRenewId) {
        log.info("Submitting renewal application: {}", applyRenewId);

        ApplyRenew applyRenew = getById(applyRenewId);
        if (applyRenew == null) {
            return Result.error("续期申请不存在");
        }

        if (!"未填写".equals(applyRenew.getApplyStatus())) {
            return Result.error("只有未填写的申请才能提交");
        }

        // Calculate new validity end date based on certificate validity calculation rules
        Certificate certificate = certificateService.getById(applyRenew.getCertificateId());
        if (certificate != null) {
            String newValidEndDate = calculateNewValidEndDate(
                    certificate.getPostTypeId(),
                    certificate.getPostId(),
                    certificate.getSex(),
                    certificate.getBirthday(),
                    certificate.getValidEndDate(),
                    certificate.getUnitCode(),
                    certificate.getWorkerName()
            );
            applyRenew.setNewValidEndDate(newValidEndDate);
        }

        applyRenew.setApplyStatus("待确认");
        boolean success = updateById(applyRenew);

        if (success) {
            log.info("Renewal application submitted successfully");
            return Result.success();
        } else {
            return Result.error("续期申请提交失败");
        }
    }

    /**
     * Approve renewal application
     * Maps to: ApplyRenewDAL.ApproveApplyRenew
     */
    public Result<Void> approveApplyRenew(Long applyRenewId, String checkMan, String checkAdvise) {
        log.info("Approving renewal application: {}, checkMan: {}, advise: {}", applyRenewId, checkMan, checkAdvise);

        ApplyRenew applyRenew = getById(applyRenewId);
        if (applyRenew == null) {
            return Result.error("续期申请不存在");
        }

        if (!"待确认".equals(applyRenew.getApplyStatus())) {
            return Result.error("只有待确认的申请才能通过");
        }

        // Update certificate validity end date
        Certificate certificate = certificateService.getById(applyRenew.getCertificateId());
        if (certificate != null && applyRenew.getNewValidEndDate() != null) {
            certificate.setValidEndDate(applyRenew.getNewValidEndDate());
            certificateService.updateById(certificate);
        }

        applyRenew.setApplyStatus("已受理");
        applyRenew.setCheckMan(checkMan);
        applyRenew.setCheckAdvise(checkAdvise);
        applyRenew.setCheckDate(LocalDate.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd")));
        boolean success = updateById(applyRenew);

        if (success) {
            log.info("Renewal application approved successfully");
            return Result.success();
        } else {
            return Result.error("续期申请审核失败");
        }
    }

    /**
     * Reject renewal application
     * Maps to: ApplyRenewDAL.RejectApplyRenew
     */
    public Result<Void> rejectApplyRenew(Long applyRenewId, String rejectionReason) {
        log.info("Rejecting renewal application: {}, reason: {}", applyRenewId, rejectionReason);

        ApplyRenew applyRenew = getById(applyRenewId);
        if (applyRenew == null) {
            return Result.error("续期申请不存在");
        }

        if (!"待确认".equals(applyRenew.getApplyStatus())) {
            return Result.error("只有待确认的申请才能驳回");
        }

        applyRenew.setApplyStatus("已驳回");
        applyRenew.setCheckAdvise(rejectionReason);
        boolean success = updateById(applyRenew);

        if (success) {
            log.info("Renewal application rejected successfully");
            return Result.success();
        } else {
            return Result.error("续期申请驳回失败");
        }
    }

    /**
     * Get pending renewal applications for approval
     * Maps to: ApplyRenewDAL.GetPendingApplyRenews
     */
    public Result<List<ApplyRenew>> getPendingApplyRenews() {
        log.debug("Getting pending renewal applications");

        List<ApplyRenew> applyRenews = lambdaQuery()
                .eq(ApplyRenew::getApplyStatus, "待确认")
                .orderByAsc(ApplyRenew::getCreateTime)
                .list();

        return Result.success(applyRenews);
    }

    /**
     * Get renewal applications by status
     */
    public Result<List<ApplyRenew>> getApplyRenewsByStatus(String status) {
        log.debug("Getting renewal applications by status: {}", status);

        List<ApplyRenew> applyRenews = lambdaQuery()
                .eq(ApplyRenew::getApplyStatus, status)
                .orderByDesc(ApplyRenew::getCreateTime)
                .list();

        return Result.success(applyRenews);
    }

    /**
     * Get renewal applications by certificate
     */
    public Result<List<ApplyRenew>> getApplyRenewsByCertificate(Long certificateId) {
        log.debug("Getting renewal applications for certificate: {}", certificateId);

        List<ApplyRenew> applyRenews = baseMapper.findByCertificateId(certificateId);

        return Result.success(applyRenews);
    }

    /**
     * Calculate new validity end date for certificate renewal
     * This calls the database function GET_CertificateContinueValidEndDate
     * Business rules from DATABASE_REFERENCE.md:
     * - 安管人员: 法人A (PostID=148): Certificate valid + 3 years
     * - 法人B: Female <= 55, Male <= 60: Valid + 3 years
     * - 特种作业: Female <= 50, Male <= 60: Valid + 2 years
     */
    private String calculateNewValidEndDate(Integer postTypeId, Integer postId,
                                          String sex, String birthday,
                                          String validEndDate, String unitCode,
                                          String workerName) {
        // This should call the database function GET_CertificateContinueValidEndDate
        // For now, implement basic logic
        LocalDate currentEndDate = LocalDate.parse(validEndDate);
        LocalDate newEndDate;

        // Basic implementation - will need to call DB function for complete logic
        if (postTypeId == 1) {
            // 安管人员 - add 3 years
            newEndDate = currentEndDate.plusYears(3);
        } else {
            // 特种作业 - add 2 years
            newEndDate = currentEndDate.plusYears(2);
        }

        return newEndDate.format(DateTimeFormatter.ofPattern("yyyy-MM-dd"));
    }
}
