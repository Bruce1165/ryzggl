package com.ryzggl.service;

import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.ryzggl.entity.CertificateMerge;
import com.ryzggl.entity.Certificate;
import com.ryzggl.repository.CertificateMergeRepository;
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
 * CertificateMerge Service - Business Logic Layer
 * Maps to: CertificateMergeDAL.cs logic and workflow management
 */
@Service
@Transactional
public class CertificateMergeService extends ServiceImpl<CertificateMergeRepository, CertificateMerge> {

    private static final Logger log = LoggerFactory.getLogger(CertificateMergeService.class);

    @Autowired
    private CertificateService certificateService;

    /**
     * Create certificate merge record
     * Maps to: CertificateMergeDAL.AddCertificateMerge
     */
    public Result<Void> createCertificateMerge(CertificateMerge certificateMerge) {
        log.info("Creating certificate merge record: from {} to {}", certificateMerge.getFromCertificateId(), certificateMerge.getToCertificateId());

        // Validate certificates exist
        Certificate fromCertificate = certificateService.getById(certificateMerge.getFromCertificateId());
        if (fromCertificate == null) {
            log.error("From certificate not found: {}", certificateMerge.getFromCertificateId());
            return Result.error("源证书不存在");
        }

        Certificate toCertificate = certificateService.getById(certificateMerge.getToCertificateId());
        if (toCertificate == null) {
            log.error("To certificate not found: {}", certificateMerge.getToCertificateId());
            return Result.error("目标证书不存在");
        }

        // Validate certificates belong to same worker
        if (!fromCertificate.getWorkerId().equals(toCertificate.getWorkerId())) {
            log.error("Certificates belong to different workers");
            return Result.error("证书不属于同一人员");
        }

        // Copy certificate information
        certificateMerge.setCertificateCode(toCertificate.getCertificateCode());
        certificateMerge.setWorkerId(toCertificate.getWorkerId());
        certificateMerge.setWorkerName(toCertificate.getWorkerName());
        certificateMerge.setUnitCode(toCertificate.getUnitCode());
        certificateMerge.setPostTypeId(toCertificate.getPostTypeId());
        certificateMerge.setPostId(toCertificate.getPostId());
        certificateMerge.setCertificateType(toCertificate.getCertificateType());
        certificateMerge.setFromCertificateCode(fromCertificate.getCertificateCode());
        certificateMerge.setToCertificateCode(toCertificate.getCertificateCode());

        // Set initial status
        certificateMerge.setApplyStatus("未填写");

        boolean success = save(certificateMerge);

        if (success) {
            log.info("Certificate merge record created successfully");
            return Result.success();
        } else {
            log.error("Failed to create certificate merge record");
            return Result.error("证书合并记录创建失败");
        }
    }

    /**
     * Get certificate merge record by ID
     */
    public Result<CertificateMerge> getCertificateMergeById(Long certificateMergeId) {
        log.debug("Getting certificate merge: {}", certificateMergeId);
        CertificateMerge certificateMerge = getById(certificateMergeId);

        if (certificateMerge == null) {
            return Result.error("证书合并记录不存在");
        }

        return Result.success(certificateMerge);
    }

    /**
     * List certificate merge records with pagination
     * Maps to: CertificateMergeDAL.GetCertificateMergeList
     */
    public Result<List<CertificateMerge>> listCertificateMerges(Long certificateId, Long workerId, String status) {
        log.debug("Listing certificate merges: certificateId={}, workerId={}, status={}", certificateId, workerId, status);

        com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<CertificateMerge> queryWrapper =
                new com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<>();

        if (certificateId != null) {
            queryWrapper.eq(CertificateMerge::getCertificateId, certificateId);
        }
        if (workerId != null) {
            queryWrapper.eq(CertificateMerge::getWorkerId, workerId);
        }
        if (status != null) {
            queryWrapper.eq(CertificateMerge::getApplyStatus, status);
        }

        queryWrapper.orderByDesc(CertificateMerge::getCreateTime);

        List<CertificateMerge> certificateMerges = list(queryWrapper);
        return Result.success(certificateMerges);
    }

    /**
     * Submit certificate merge for review
     * Maps to: CertificateMergeDAL.SubmitCertificateMerge
     */
    public Result<Void> submitCertificateMerge(Long certificateMergeId) {
        log.info("Submitting certificate merge: {}", certificateMergeId);

        CertificateMerge certificateMerge = getById(certificateMergeId);
        if (certificateMerge == null) {
            return Result.error("证书合并记录不存在");
        }

        if (!"未填写".equals(certificateMerge.getApplyStatus())) {
            return Result.error("只有未填写的申请才能提交");
        }

        certificateMerge.setApplyStatus("待确认");
        boolean success = updateById(certificateMerge);

        if (success) {
            log.info("Certificate merge submitted successfully");
            return Result.success();
        } else {
            return Result.error("证书合并提交失败");
        }
    }

    /**
     * Approve certificate merge
     * Maps to: CertificateMergeDAL.ApproveCertificateMerge
     */
    public Result<Void> approveCertificateMerge(Long certificateMergeId, String checkMan, String checkAdvise) {
        log.info("Approving certificate merge: {}, checkMan: {}, advise: {}", certificateMergeId, checkMan, checkAdvise);

        CertificateMerge certificateMerge = getById(certificateMergeId);
        if (certificateMerge == null) {
            return Result.error("证书合并记录不存在");
        }

        if (!"待确认".equals(certificateMerge.getApplyStatus())) {
            return Result.error("只有待确认的申请才能通过");
        }

        // Set merge date and status
        certificateMerge.setMergeDate(LocalDate.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd")));
        certificateMerge.setApplyStatus("已受理");
        certificateMerge.setCheckMan(checkMan);
        certificateMerge.setCheckAdvise(checkAdvise);
        certificateMerge.setCheckDate(LocalDate.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd")));
        boolean success = updateById(certificateMerge);

        if (success) {
            log.info("Certificate merge approved successfully");
            return Result.success();
        } else {
            return Result.error("证书合并审核失败");
        }
    }

    /**
     * Reject certificate merge
     * Maps to: CertificateMergeDAL.RejectCertificateMerge
     */
    public Result<Void> rejectCertificateMerge(Long certificateMergeId, String rejectionReason) {
        log.info("Rejecting certificate merge: {}, reason: {}", certificateMergeId, rejectionReason);

        CertificateMerge certificateMerge = getById(certificateMergeId);
        if (certificateMerge == null) {
            return Result.error("证书合并记录不存在");
        }

        if (!"待确认".equals(certificateMerge.getApplyStatus())) {
            return Result.error("只有待确认的申请才能驳回");
        }

        certificateMerge.setApplyStatus("已驳回");
        certificateMerge.setCheckAdvise(rejectionReason);
        boolean success = updateById(certificateMerge);

        if (success) {
            log.info("Certificate merge rejected successfully");
            return Result.success();
        } else {
            return Result.error("证书合并驳回失败");
        }
    }

    /**
     * Get pending certificate merge records for approval
     * Maps to: CertificateMergeDAL.GetPendingCertificateMerges
     */
    public Result<List<CertificateMerge>> getPendingCertificateMerges() {
        log.debug("Getting pending certificate merges");

        List<CertificateMerge> certificateMerges = lambdaQuery()
                .eq(CertificateMerge::getApplyStatus, "待确认")
                .orderByAsc(CertificateMerge::getCreateTime)
                .list();

        return Result.success(certificateMerges);
    }

    /**
     * Get certificate merge records by status
     */
    public Result<List<CertificateMerge>> getCertificateMergesByStatus(String status) {
        log.debug("Getting certificate merges by status: {}", status);

        List<CertificateMerge> certificateMerges = lambdaQuery()
                .eq(CertificateMerge::getApplyStatus, status)
                .orderByDesc(CertificateMerge::getCreateTime)
                .list();

        return Result.success(certificateMerges);
    }

    /**
     * Get certificate merge records by certificate
     */
    public Result<List<CertificateMerge>> getCertificateMergesByCertificate(Long certificateId) {
        log.debug("Getting certificate merges for certificate: {}", certificateId);

        List<CertificateMerge> certificateMerges = baseMapper.findByCertificateId(certificateId);

        return Result.success(certificateMerges);
    }

    /**
     * Get recent certificate merge records
     */
    public Result<List<CertificateMerge>> getRecentCertificateMerges(int days) {
        log.debug("Getting recent certificate merges (last {} days)", days);

        List<CertificateMerge> certificateMerges = baseMapper.findRecent(days);

        return Result.success(certificateMerges);
    }
}
