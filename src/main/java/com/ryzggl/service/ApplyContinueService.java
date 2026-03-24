package com.ryzggl.service;

import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.ryzggl.entity.ApplyContinue;
import com.ryzggl.entity.Certificate;
import com.ryzggl.repository.ApplyContinueRepository;
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
 * ApplyContinue Service - Business Logic Layer
 * Maps to: ApplyContinueDAL.cs logic and workflow management
 */
@Service
@Transactional
public class ApplyContinueService extends ServiceImpl<ApplyContinueRepository, ApplyContinue> {

    private static final Logger log = LoggerFactory.getLogger(ApplyContinueService.class);

    @Autowired
    private CertificateService certificateService;

    /**
     * Create new continuation application
     * Maps to: ApplyContinueDAL.AddApplyContinue
     */
    public Result<Void> createApplyContinue(ApplyContinue applyContinue) {
        log.info("Creating continuation application for certificate: {}", applyContinue.getCertificateId());

        // Validate certificate exists
        Certificate certificate = certificateService.getById(applyContinue.getCertificateId());
        if (certificate == null) {
            log.error("Certificate not found: {}", applyContinue.getCertificateId());
            return Result.error("证书不存在");
        }

        // Validate certificate belongs to worker
        if (!certificate.getWorkerId().equals(applyContinue.getWorkerId())) {
            log.error("Certificate {} does not belong to worker {}", certificate.getId(), applyContinue.getWorkerId());
            return Result.error("证书与人员不匹配");
        }

        // Copy certificate validity dates
        applyContinue.setValidStartDate(certificate.getValidStartDate());
        applyContinue.setValidEndDate(certificate.getValidEndDate());

        // Set initial status
        applyContinue.setApplyStatus("未填写");

        boolean success = save(applyContinue);

        if (success) {
            log.info("Continuation application created successfully");
            return Result.success();
        } else {
            log.error("Failed to create continuation application");
            return Result.error("延续申请创建失败");
        }
    }

    /**
     * Get continuation application by ID
     */
    public Result<ApplyContinue> getApplyContinueById(Long applyContinueId) {
        log.debug("Getting continuation application: {}", applyContinueId);
        ApplyContinue applyContinue = getById(applyContinueId);

        if (applyContinue == null) {
            return Result.error("延续申请不存在");
        }

        return Result.success(applyContinue);
    }

    /**
     * List continuation applications with pagination
     * Maps to: ApplyContinueDAL.GetApplyContinueList
     */
    public Result<List<ApplyContinue>> listApplyContinues(Long workerId, String status) {
        log.debug("Listing continuation applications: workerId={}, status={}", workerId, status);

        if (workerId != null) {
            List<ApplyContinue> applyContinues = lambdaQuery()
                    .eq(ApplyContinue::getWorkerId, workerId)
                    .eq(status != null, ApplyContinue::getApplyStatus, status)
                    .orderByDesc(ApplyContinue::getCreateTime)
                    .list();
            return Result.success(applyContinues);
        } else {
            List<ApplyContinue> applyContinues = lambdaQuery()
                    .eq(status != null, ApplyContinue::getApplyStatus, status)
                    .orderByDesc(ApplyContinue::getCreateTime)
                    .list();
            return Result.success(applyContinues);
        }
    }

    /**
     * Submit continuation application for review
     * Maps to: ApplyContinueDAL.SubmitApplyContinue
     */
    public Result<Void> submitApplyContinue(Long applyContinueId) {
        log.info("Submitting continuation application: {}", applyContinueId);

        ApplyContinue applyContinue = getById(applyContinueId);
        if (applyContinue == null) {
            return Result.error("延续申请不存在");
        }

        if (!"未填写".equals(applyContinue.getApplyStatus())) {
            return Result.error("只有未填写的申请才能提交");
        }

        applyContinue.setApplyStatus("待确认");
        boolean success = updateById(applyContinue);

        if (success) {
            log.info("Continuation application submitted successfully");
            return Result.success();
        } else {
            return Result.error("延续申请提交失败");
        }
    }

    /**
     * Approve continuation application
     * Maps to: ApplyContinueDAL.ApproveApplyContinue
     */
    public Result<Void> approveApplyContinue(Long applyContinueId, String checkMan, String checkAdvise) {
        log.info("Approving continuation application: {}, checkMan: {}, advise: {}", applyContinueId, checkMan, checkAdvise);

        ApplyContinue applyContinue = getById(applyContinueId);
        if (applyContinue == null) {
            return Result.error("延续申请不存在");
        }

        if (!"待确认".equals(applyContinue.getApplyStatus())) {
            return Result.error("只有待确认的申请才能通过");
        }

        // Update certificate validity based on continuation type and period
        Certificate certificate = certificateService.getById(applyContinue.getCertificateId());
        if (certificate != null) {
            LocalDate newEndDate = LocalDate.parse(certificate.getValidEndDate());

            if (applyContinue.getContinuePeriod() != null && applyContinue.getContinuePeriod() > 0) {
                // Add continue period (in years) to current end date
                newEndDate = newEndDate.plusYears(applyContinue.getContinuePeriod());
            } else {
                // Default to adding 3 years for 安管人员 or 2 years for 特种作业
                if (certificate.getPostTypeId() == 1) {
                    newEndDate = newEndDate.plusYears(3);
                } else {
                    newEndDate = newEndDate.plusYears(2);
                }
            }

            certificate.setValidEndDate(newEndDate.format(DateTimeFormatter.ofPattern("yyyy-MM-dd")));
            certificateService.updateById(certificate);
            log.info("Certificate {} validity end date updated to {}", certificate.getId(), newEndDate);
        }

        // Set effective date
        applyContinue.setEffectiveDate(LocalDate.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd")));
        applyContinue.setApplyStatus("已受理");
        applyContinue.setCheckMan(checkMan);
        applyContinue.setCheckAdvise(checkAdvise);
        applyContinue.setCheckDate(LocalDate.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd")));
        boolean success = updateById(applyContinue);

        if (success) {
            log.info("Continuation application approved successfully");
            return Result.success();
        } else {
            return Result.error("延续申请审核失败");
        }
    }

    /**
     * Reject continuation application
     * Maps to: ApplyContinueDAL.RejectApplyContinue
     */
    public Result<Void> rejectApplyContinue(Long applyContinueId, String rejectionReason) {
        log.info("Rejecting continuation application: {}, reason: {}", applyContinueId, rejectionReason);

        ApplyContinue applyContinue = getById(applyContinueId);
        if (applyContinue == null) {
            return Result.error("延续申请不存在");
        }

        if (!"待确认".equals(applyContinue.getApplyStatus())) {
            return Result.error("只有待确认的申请才能驳回");
        }

        applyContinue.setApplyStatus("已驳回");
        applyContinue.setCheckAdvise(rejectionReason);
        boolean success = updateById(applyContinue);

        if (success) {
            log.info("Continuation application rejected successfully");
            return Result.success();
        } else {
            return Result.error("延续申请驳回失败");
        }
    }

    /**
     * Get pending continuation applications for approval
     * Maps to: ApplyContinueDAL.GetPendingApplyContinues
     */
    public Result<List<ApplyContinue>> getPendingApplyContinues() {
        log.debug("Getting pending continuation applications");

        List<ApplyContinue> applyContinues = lambdaQuery()
                .eq(ApplyContinue::getApplyStatus, "待确认")
                .orderByAsc(ApplyContinue::getCreateTime)
                .list();

        return Result.success(applyContinues);
    }

    /**
     * Get continuation applications by status
     */
    public Result<List<ApplyContinue>> getApplyContinuesByStatus(String status) {
        log.debug("Getting continuation applications by status: {}", status);

        List<ApplyContinue> applyContinues = lambdaQuery()
                .eq(ApplyContinue::getApplyStatus, status)
                .orderByDesc(ApplyContinue::getCreateTime)
                .list();

        return Result.success(applyContinues);
    }

    /**
     * Get continuation applications by certificate
     */
    public Result<List<ApplyContinue>> getApplyContinuesByCertificate(Long certificateId) {
        log.debug("Getting continuation applications for certificate: {}", certificateId);

        List<ApplyContinue> applyContinues = baseMapper.findByCertificateId(certificateId);

        return Result.success(applyContinues);
    }

    /**
     * Get continuation applications by type
     */
    public Result<List<ApplyContinue>> getApplyContinuesByType(String continueType) {
        log.debug("Getting continuation applications by type: {}", continueType);

        List<ApplyContinue> applyContinues = baseMapper.findByContinueType(continueType);

        return Result.success(applyContinues);
    }
}
