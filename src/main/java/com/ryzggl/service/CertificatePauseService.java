package com.ryzggl.service;

import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.ryzggl.entity.CertificatePause;
import com.ryzggl.entity.Certificate;
import com.ryzggl.repository.CertificatePauseRepository;
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
 * CertificatePause Service - Business Logic Layer
 * Maps to: CertificatePauseDAL.cs logic and workflow management
 */
@Service
@Transactional
public class CertificatePauseService extends ServiceImpl<CertificatePauseRepository, CertificatePause> {

    private static final Logger log = LoggerFactory.getLogger(CertificatePauseService.class);

    @Autowired
    private CertificateService certificateService;

    /**
     * Create certificate pause record
     * Maps to: CertificatePauseDAL.AddCertificatePause
     */
    public Result<Void> createCertificatePause(CertificatePause certificatePause) {
        log.info("Creating certificate pause for certificate: {}", certificatePause.getCertificateId());

        // Validate certificate exists
        Certificate certificate = certificateService.getById(certificatePause.getCertificateId());
        if (certificate == null) {
            log.error("Certificate not found: {}", certificatePause.getCertificateId());
            return Result.error("证书不存在");
        }

        // Copy certificate information
        certificatePause.setCertificateCode(certificate.getCertificateCode());
        certificatePause.setWorkerId(certificate.getWorkerId());
        certificatePause.setWorkerName(certificate.getWorkerName());
        certificatePause.setUnitCode(certificate.getUnitCode());
        certificatePause.setPostTypeId(certificate.getPostTypeId());
        certificatePause.setPostId(certificate.getPostId());
        certificatePause.setCertificateType(certificate.getCertificateType());

        // Set pause date and status
        certificatePause.setPauseDate(LocalDate.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd")));
        certificatePause.setPauseStatus("已暂停");

        boolean success = save(certificatePause);

        if (success) {
            log.info("Certificate pause created successfully");
            return Result.success();
        } else {
            log.error("Failed to create certificate pause");
            return Result.error("证书暂停记录创建失败");
        }
    }

    /**
     * Get certificate pause record by ID
     */
    public Result<CertificatePause> getCertificatePauseById(Long certificatePauseId) {
        log.debug("Getting certificate pause: {}", certificatePauseId);
        CertificatePause certificatePause = getById(certificatePauseId);

        if (certificatePause == null) {
            return Result.error("证书暂停记录不存在");
        }

        return Result.success(certificatePause);
    }

    /**
     * List certificate pause records with pagination
     * Maps to: CertificatePauseDAL.GetCertificatePauseList
     */
    public Result<List<CertificatePause>> listCertificatePauses(Long certificateId, Long workerId, String pauseStatus) {
        log.debug("Listing certificate pauses: certificateId={}, workerId={}, pauseStatus={}", certificateId, workerId, pauseStatus);

        com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<CertificatePause> queryWrapper =
                new com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<>();

        if (certificateId != null) {
            queryWrapper.eq(CertificatePause::getCertificateId, certificateId);
        }
        if (workerId != null) {
            queryWrapper.eq(CertificatePause::getWorkerId, workerId);
        }
        if (pauseStatus != null) {
            queryWrapper.eq(CertificatePause::getPauseStatus, pauseStatus);
        }

        queryWrapper.orderByDesc(CertificatePause::getCreateTime);

        List<CertificatePause> certificatePauses = list(queryWrapper);
        return Result.success(certificatePauses);
    }

    /**
     * Pause certificate
     * Maps to: CertificatePauseDAL.PauseCertificate
     */
    public Result<Void> pauseCertificate(Long certificateId, String pauseReason, String pauseMan) {
        log.info("Pausing certificate: {}, pauseMan: {}", certificateId, pauseMan);

        Certificate certificate = certificateService.getById(certificateId);
        if (certificate == null) {
            return Result.error("证书不存在");
        }

        // Check if certificate is already paused
        List<CertificatePause> existingPauses = baseMapper.findByCertificateId(certificateId);
        for (CertificatePause pause : existingPauses) {
            if ("已暂停".equals(pause.getPauseStatus())) {
                return Result.error("证书已暂停");
            }
        }

        // Create pause record
        CertificatePause certificatePause = new CertificatePause();
        certificatePause.setCertificateId(certificateId);
        certificatePause.setPauseReason(pauseReason);
        certificatePause.setPauseMan(pauseMan);
        certificatePause.setPauseDate(LocalDate.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd")));
        certificatePause.setPauseStatus("已暂停");

        boolean saveResult = save(certificatePause);

        if (saveResult) {
            log.info("Certificate paused successfully");
            return Result.success();
        } else {
            return Result.error("证书暂停失败");
        }
    }

    /**
     * Resume certificate
     * Maps to: CertificatePauseDAL.ResumeCertificate
     */
    public Result<Void> resumeCertificate(Long certificatePauseId, String resumeReason, String resumeMan) {
        log.info("Resuming certificate: {}, resumeMan: {}, reason: {}", certificatePauseId, resumeMan, resumeReason);

        CertificatePause certificatePause = getById(certificatePauseId);
        if (certificatePause == null) {
            return Result.error("证书暂停记录不存在");
        }

        if (!"已暂停".equals(certificatePause.getPauseStatus())) {
            return Result.error("只有已暂停的证书才能恢复");
        }

        certificatePause.setPauseStatus("已恢复");
        certificatePause.setResumeMan(resumeMan);
        certificatePause.setResumeDate(LocalDate.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd")));
        certificatePause.setResumeReason(resumeReason);
        boolean success = updateById(certificatePause);

        if (success) {
            log.info("Certificate resumed successfully");
            return Result.success();
        } else {
            return Result.error("证书恢复失败");
        }
    }

    /**
     * Get currently paused certificates
     * Maps to: CertificatePauseDAL.GetPausedCertificates
     */
    public Result<List<CertificatePause>> getPausedCertificates() {
        log.debug("Getting currently paused certificates");

        List<CertificatePause> certificatePauses = baseMapper.findPausedCertificates();

        return Result.success(certificatePauses);
    }

    /**
     * Get certificate pause records by status
     */
    public Result<List<CertificatePause>> getCertificatePausesByStatus(String pauseStatus) {
        log.debug("Getting certificate pauses by status: {}", pauseStatus);

        List<CertificatePause> certificatePauses = baseMapper.findByPauseStatus(pauseStatus);

        return Result.success(certificatePauses);
    }

    /**
     * Get certificate pause records by certificate
     */
    public Result<List<CertificatePause>> getCertificatePausesByCertificate(Long certificateId) {
        log.debug("Getting certificate pauses for certificate: {}", certificateId);

        List<CertificatePause> certificatePauses = baseMapper.findByCertificateId(certificateId);

        return Result.success(certificatePauses);
    }

    /**
     * Get recent certificate pause records
     */
    public Result<List<CertificatePause>> getRecentCertificatePauses(int days) {
        log.debug("Getting recent certificate pauses (last {} days)", days);

        List<CertificatePause> certificatePauses = baseMapper.findRecent(days);

        return Result.success(certificatePauses);
    }
}
