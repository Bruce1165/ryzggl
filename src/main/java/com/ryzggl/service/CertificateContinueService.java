package com.ryzggl.service;

import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.ryzggl.entity.CertificateContinue;
import com.ryzggl.entity.Certificate;
import com.ryzggl.repository.CertificateContinueRepository;
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
 * CertificateContinue Service - Business Logic Layer
 * Maps to: CertificateContinueDAL.cs logic and workflow management
 */
@Service
@Transactional
public class CertificateContinueService extends ServiceImpl<CertificateContinueRepository, CertificateContinue> {

    private static final Logger log = LoggerFactory.getLogger(CertificateContinueService.class);

    @Autowired
    private CertificateService certificateService;

    /**
     * Create certificate continuation record
     * Maps to: CertificateContinueDAL.AddCertificateContinue
     */
    public Result<Void> createCertificateContinue(CertificateContinue certificateContinue) {
        log.info("Creating certificate continuation record for certificate: {}", certificateContinue.getCertificateId());

        // Validate certificate exists
        Certificate certificate = certificateService.getById(certificateContinue.getCertificateId());
        if (certificate == null) {
            log.error("Certificate not found: {}", certificateContinue.getCertificateId());
            return Result.error("证书不存在");
        }

        // Copy current certificate validity dates to old values
        certificateContinue.setCertificateCode(certificate.getCertificateCode());
        certificateContinue.setWorkerId(certificate.getWorkerId());
        certificateContinue.setWorkerName(certificate.getWorkerName());
        certificateContinue.setUnitCode(certificate.getUnitCode());
        certificateContinue.setPostTypeId(certificate.getPostTypeId());
        certificateContinue.setPostId(certificate.getPostId());
        certificateContinue.setCertificateType(certificate.getCertificateType());
        certificateContinue.setOldValidStartDate(certificate.getValidStartDate());
        certificateContinue.setOldValidEndDate(certificate.getValidEndDate());

        // Set initial status
        certificateContinue.setApplyStatus("未填写");

        boolean success = save(certificateContinue);

        if (success) {
            log.info("Certificate continuation record created successfully");
            return Result.success();
        } else {
            log.error("Failed to create certificate continuation record");
            return Result.error("证书延续记录创建失败");
        }
    }

    /**
     * Get certificate continuation record by ID
     */
    public Result<CertificateContinue> getCertificateContinueById(Long certificateContinueId) {
        log.debug("Getting certificate continuation record: {}", certificateContinueId);
        CertificateContinue certificateContinue = getById(certificateContinueId);

        if (certificateContinue == null) {
            return Result.error("证书延续记录不存在");
        }

        return Result.success(certificateContinue);
    }

    /**
     * List certificate continuation records with pagination
     * Maps to: CertificateContinueDAL.GetCertificateContinueList
     */
    public Result<List<CertificateContinue>> listCertificateContinues(Long certificateId, Long workerId, String status) {
        log.debug("Listing certificate continuation records: certificateId={}, workerId={}, status={}", certificateId, workerId, status);

        com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<CertificateContinue> queryWrapper =
                new com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<>();

        if (certificateId != null) {
            queryWrapper.eq(CertificateContinue::getCertificateId, certificateId);
        }
        if (workerId != null) {
            queryWrapper.eq(CertificateContinue::getWorkerId, workerId);
        }
        if (status != null) {
            queryWrapper.eq(CertificateContinue::getApplyStatus, status);
        }

        queryWrapper.orderByDesc(CertificateContinue::getCreateTime);

        List<CertificateContinue> certificateContinues = list(queryWrapper);
        return Result.success(certificateContinues);
    }

    /**
     * Submit certificate continuation for review
     * Maps to: CertificateContinueDAL.SubmitCertificateContinue
     */
    public Result<Void> submitCertificateContinue(Long certificateContinueId) {
        log.info("Submitting certificate continuation: {}", certificateContinueId);

        CertificateContinue certificateContinue = getById(certificateContinueId);
        if (certificateContinue == null) {
            return Result.error("证书延续记录不存在");
        }

        if (!"未填写".equals(certificateContinue.getApplyStatus())) {
            return Result.error("只有未填写的申请才能提交");
        }

        // Calculate new validity end date
        Certificate certificate = certificateService.getById(certificateContinue.getCertificateId());
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
            certificateContinue.setNewValidEndDate(newValidEndDate);

            // Set new valid start date as next day after old end date
            LocalDate oldEndDate = LocalDate.parse(certificate.getValidEndDate());
            LocalDate newStartDate = oldEndDate.plusDays(1);
            certificateContinue.setNewValidStartDate(newStartDate.format(DateTimeFormatter.ofPattern("yyyy-MM-dd")));
        }

        certificateContinue.setApplyStatus("待确认");
        boolean success = updateById(certificateContinue);

        if (success) {
            log.info("Certificate continuation submitted successfully");
            return Result.success();
        } else {
            return Result.error("证书延续提交失败");
        }
    }

    /**
     * Approve certificate continuation
     * Maps to: CertificateContinueDAL.ApproveCertificateContinue
     */
    public Result<Void> approveCertificateContinue(Long certificateContinueId, String checkMan, String checkAdvise) {
        log.info("Approving certificate continuation: {}, checkMan: {}, advise: {}", certificateContinueId, checkMan, checkAdvise);

        CertificateContinue certificateContinue = getById(certificateContinueId);
        if (certificateContinue == null) {
            return Result.error("证书延续记录不存在");
        }

        if (!"待确认".equals(certificateContinue.getApplyStatus())) {
            return Result.error("只有待确认的申请才能通过");
        }

        // Update certificate validity dates
        Certificate certificate = certificateService.getById(certificateContinue.getCertificateId());
        if (certificate != null && certificateContinue.getNewValidEndDate() != null) {
            certificate.setValidStartDate(certificateContinue.getNewValidStartDate());
            certificate.setValidEndDate(certificateContinue.getNewValidEndDate());
            certificateService.updateById(certificate);
            log.info("Certificate {} validity dates updated", certificate.getId());
        }

        certificateContinue.setApplyStatus("已受理");
        certificateContinue.setCheckMan(checkMan);
        certificateContinue.setCheckAdvise(checkAdvise);
        certificateContinue.setCheckDate(LocalDate.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd")));
        certificateContinue.setEffectiveDate(LocalDate.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd")));
        boolean success = updateById(certificateContinue);

        if (success) {
            log.info("Certificate continuation approved successfully");
            return Result.success();
        } else {
            return Result.error("证书延续审核失败");
        }
    }

    /**
     * Reject certificate continuation
     * Maps to: CertificateContinueDAL.RejectCertificateContinue
     */
    public Result<Void> rejectCertificateContinue(Long certificateContinueId, String rejectionReason) {
        log.info("Rejecting certificate continuation: {}, reason: {}", certificateContinueId, rejectionReason);

        CertificateContinue certificateContinue = getById(certificateContinueId);
        if (certificateContinue == null) {
            return Result.error("证书延续记录不存在");
        }

        if (!"待确认".equals(certificateContinue.getApplyStatus())) {
            return Result.error("只有待确认的申请才能驳回");
        }

        certificateContinue.setApplyStatus("已驳回");
        certificateContinue.setCheckAdvise(rejectionReason);
        boolean success = updateById(certificateContinue);

        if (success) {
            log.info("Certificate continuation rejected successfully");
            return Result.success();
        } else {
            return Result.error("证书延续驳回失败");
        }
    }

    /**
     * Get pending certificate continuation records for approval
     * Maps to: CertificateContinueDAL.GetPendingCertificateContinues
     */
    public Result<List<CertificateContinue>> getPendingCertificateContinues() {
        log.debug("Getting pending certificate continuation records");

        List<CertificateContinue> certificateContinues = lambdaQuery()
                .eq(CertificateContinue::getApplyStatus, "待确认")
                .orderByAsc(CertificateContinue::getCreateTime)
                .list();

        return Result.success(certificateContinues);
    }

    /**
     * Get certificate continuation records by status
     */
    public Result<List<CertificateContinue>> getCertificateContinuesByStatus(String status) {
        log.debug("Getting certificate continuation records by status: {}", status);

        List<CertificateContinue> certificateContinues = lambdaQuery()
                .eq(CertificateContinue::getApplyStatus, status)
                .orderByDesc(CertificateContinue::getCreateTime)
                .list();

        return Result.success(certificateContinues);
    }

    /**
     * Get certificate continuation records by certificate
     */
    public Result<List<CertificateContinue>> getCertificateContinuesByCertificate(Long certificateId) {
        log.debug("Getting certificate continuation records for certificate: {}", certificateId);

        List<CertificateContinue> certificateContinues = baseMapper.findByCertificateId(certificateId);

        return Result.success(certificateContinues);
    }

    /**
     * Get recent certificate continuation records
     */
    public Result<List<CertificateContinue>> getRecentCertificateContinues(int days) {
        log.debug("Getting recent certificate continuation records (last {} days)", days);

        List<CertificateContinue> certificateContinues = baseMapper.findRecent(days);

        return Result.success(certificateContinues);
    }

    /**
     * Calculate new validity end date for certificate continuation
     * This calls the database function GET_CertificateContinueValidEndDate
     */
    private String calculateNewValidEndDate(Integer postTypeId, Integer postId,
                                          String sex, String birthday,
                                          String validEndDate, String unitCode,
                                          String workerName) {
        // This should call the database function GET_CertificateContinueValidEndDate
        // For now, implement basic logic
        LocalDate currentEndDate = LocalDate.parse(validEndDate);
        LocalDate newEndDate;

        // Basic implementation
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
