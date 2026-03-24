package com.ryzggl.service;

import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.ryzggl.entity.CertificateOut;
import com.ryzggl.entity.Certificate;
import com.ryzggl.repository.CertificateOutRepository;
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
 * CertificateOut Service - Business Logic Layer
 * Maps to: CertificateOutDAL.cs logic and workflow management
 */
@Service
@Transactional
public class CertificateOutService extends ServiceImpl<CertificateOutRepository, CertificateOut> {

    private static final Logger log = LoggerFactory.getLogger(CertificateOutService.class);

    @Autowired
    private CertificateService certificateService;

    /**
     * Create certificate issuance record
     * Maps to: CertificateOutDAL.AddCertificateOut
     */
    public Result<Void> createCertificateOut(CertificateOut certificateOut) {
        log.info("Creating certificate issuance record for certificate: {}", certificateOut.getCertificateId());

        // Validate certificate exists
        Certificate certificate = certificateService.getById(certificateOut.getCertificateId());
        if (certificate == null) {
            log.error("Certificate not found: {}", certificateOut.getCertificateId());
            return Result.error("证书不存在");
        }

        // Copy certificate information
        certificateOut.setCertificateCode(certificate.getCertificateCode());
        certificateOut.setWorkerId(certificate.getWorkerId());
        certificateOut.setWorkerName(certificate.getWorkerName());
        certificateOut.setUnitCode(certificate.getUnitCode());
        certificateOut.setPostTypeId(certificate.getPostTypeId());
        certificateOut.setPostId(certificate.getPostId());
        certificateOut.setCertificateType(certificate.getCertificateType());

        // Set initial status
        certificateOut.setApplyStatus("未填写");

        boolean success = save(certificateOut);

        if (success) {
            log.info("Certificate issuance record created successfully");
            return Result.success();
        } else {
            log.error("Failed to create certificate issuance record");
            return Result.error("证书发放记录创建失败");
        }
    }

    /**
     * Get certificate issuance record by ID
     */
    public Result<CertificateOut> getCertificateOutById(Long certificateOutId) {
        log.debug("Getting certificate issuance record: {}", certificateOutId);
        CertificateOut certificateOut = getById(certificateOutId);

        if (certificateOut == null) {
            return Result.error("证书发放记录不存在");
        }

        return Result.success(certificateOut);
    }

    /**
     * List certificate issuance records with pagination
     * Maps to: CertificateOutDAL.GetCertificateOutList
     */
    public Result<List<CertificateOut>> listCertificateOuts(Long certificateId, Long workerId, String status) {
        log.debug("Listing certificate issuance records: certificateId={}, workerId={}, status={}", certificateId, workerId, status);

        com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<CertificateOut> queryWrapper =
                new com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<>();

        if (certificateId != null) {
            queryWrapper.eq(CertificateOut::getCertificateId, certificateId);
        }
        if (workerId != null) {
            queryWrapper.eq(CertificateOut::getWorkerId, workerId);
        }
        if (status != null) {
            queryWrapper.eq(CertificateOut::getApplyStatus, status);
        }

        queryWrapper.orderByDesc(CertificateOut::getCreateTime);

        List<CertificateOut> certificateOuts = list(queryWrapper);
        return Result.success(certificateOuts);
    }

    /**
     * Submit certificate issuance for review
     * Maps to: CertificateOutDAL.SubmitCertificateOut
     */
    public Result<Void> submitCertificateOut(Long certificateOutId) {
        log.info("Submitting certificate issuance: {}", certificateOutId);

        CertificateOut certificateOut = getById(certificateOutId);
        if (certificateOut == null) {
            return Result.error("证书发放记录不存在");
        }

        if (!"未填写".equals(certificateOut.getApplyStatus())) {
            return Result.error("只有未填写的申请才能提交");
        }

        certificateOut.setApplyStatus("待确认");
        boolean success = updateById(certificateOut);

        if (success) {
            log.info("Certificate issuance submitted successfully");
            return Result.success();
        } else {
            return Result.error("证书发放提交失败");
        }
    }

    /**
     * Approve certificate issuance
     * Maps to: CertificateOutDAL.ApproveCertificateOut
     */
    public Result<Void> approveCertificateOut(Long certificateOutId, String checkMan, String checkAdvise) {
        log.info("Approving certificate issuance: {}, checkMan: {}, advise: {}", certificateOutId, checkMan, checkAdvise);

        CertificateOut certificateOut = getById(certificateOutId);
        if (certificateOut == null) {
            return Result.error("证书发放记录不存在");
        }

        if (!"待确认".equals(certificateOut.getApplyStatus())) {
            return Result.error("只有待确认的申请才能通过");
        }

        // Update certificate print information
        Certificate certificate = certificateService.getById(certificateOut.getCertificateId());
        if (certificate != null) {
            certificate.setCheckMan(checkMan);
            certificate.setCheckDate(LocalDate.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd")));
            certificateService.updateById(certificate);
            log.info("Certificate {} print information updated", certificate.getId());
        }

        certificateOut.setApplyStatus("已受理");
        certificateOut.setCheckMan(checkMan);
        certificateOut.setCheckAdvise(checkAdvise);
        certificateOut.setCheckDate(LocalDate.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd")));
        certificateOut.setPrintDate(LocalDate.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd")));
        boolean success = updateById(certificateOut);

        if (success) {
            log.info("Certificate issuance approved successfully");
            return Result.success();
        } else {
            return Result.error("证书发放审核失败");
        }
    }

    /**
     * Print certificate
     * Maps to: CertificateOutDAL.PrintCertificate
     */
    public Result<Void> printCertificate(Long certificateOutId, String printMan) {
        log.info("Printing certificate: {}, printMan: {}", certificateOutId, printMan);

        CertificateOut certificateOut = getById(certificateOutId);
        if (certificateOut == null) {
            return Result.error("证书发放记录不存在");
        }

        if (!"已受理".equals(certificateOut.getApplyStatus())) {
            return Result.error("只有已受理的申请才能打印");
        }

        // Update certificate print information
        Certificate certificate = certificateService.getById(certificateOut.getCertificateId());
        if (certificate != null) {
            certificate.setPrintMan(printMan);
            certificate.setPrintDate(LocalDate.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd")));
            certificateService.updateById(certificate);
            log.info("Certificate {} printed by {}", certificate.getId(), printMan);
        }

        certificateOut.setPrintMan(printMan);
        certificateOut.setPrintDate(LocalDate.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd")));
        certificateOut.setApplyStatus("已打印");
        boolean success = updateById(certificateOut);

        if (success) {
            log.info("Certificate printed successfully");
            return Result.success();
        } else {
            return Result.error("证书打印失败");
        }
    }

    /**
     * Reject certificate issuance
     * Maps to: CertificateOutDAL.RejectCertificateOut
     */
    public Result<Void> rejectCertificateOut(Long certificateOutId, String rejectionReason) {
        log.info("Rejecting certificate issuance: {}, reason: {}", certificateOutId, rejectionReason);

        CertificateOut certificateOut = getById(certificateOutId);
        if (certificateOut == null) {
            return Result.error("证书发放记录不存在");
        }

        if (!"待确认".equals(certificateOut.getApplyStatus())) {
            return Result.error("只有待确认的申请才能驳回");
        }

        certificateOut.setApplyStatus("已驳回");
        certificateOut.setCheckAdvise(rejectionReason);
        boolean success = updateById(certificateOut);

        if (success) {
            log.info("Certificate issuance rejected successfully");
            return Result.success();
        } else {
            return Result.error("证书发放驳回失败");
        }
    }

    /**
     * Get pending certificate issuance records for approval
     * Maps to: CertificateOutDAL.GetPendingCertificateOuts
     */
    public Result<List<CertificateOut>> getPendingCertificateOuts() {
        log.debug("Getting pending certificate issuance records");

        List<CertificateOut> certificateOuts = lambdaQuery()
                .eq(CertificateOut::getApplyStatus, "待确认")
                .orderByAsc(CertificateOut::getCreateTime)
                .list();

        return Result.success(certificateOuts);
    }

    /**
     * Get certificate issuance records by status
     */
    public Result<List<CertificateOut>> getCertificateOutsByStatus(String status) {
        log.debug("Getting certificate issuance records by status: {}", status);

        List<CertificateOut> certificateOuts = lambdaQuery()
                .eq(CertificateOut::getApplyStatus, status)
                .orderByDesc(CertificateOut::getCreateTime)
                .list();

        return Result.success(certificateOuts);
    }

    /**
     * Get certificate issuance records by certificate
     */
    public Result<List<CertificateOut>> getCertificateOutsByCertificate(Long certificateId) {
        log.debug("Getting certificate issuance records for certificate: {}", certificateId);

        List<CertificateOut> certificateOuts = baseMapper.findByCertificateId(certificateId);

        return Result.success(certificateOuts);
    }

    /**
     * Get recent certificate issuance records
     */
    public Result<List<CertificateOut>> getRecentCertificateOuts(int days) {
        log.debug("Getting recent certificate issuance records (last {} days)", days);

        List<CertificateOut> certificateOuts = baseMapper.findRecent(days);

        return Result.success(certificateOuts);
    }
}
