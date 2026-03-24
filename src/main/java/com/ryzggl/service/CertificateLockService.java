package com.ryzggl.service;

import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.ryzggl.entity.CertificateLock;
import com.ryzggl.entity.Certificate;
import com.ryzggl.repository.CertificateLockRepository;
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
 * CertificateLock Service - Business Logic Layer
 * Maps to: CertificateLockDAL.cs logic and workflow management
 */
@Service
@Transactional
public class CertificateLockService extends ServiceImpl<CertificateLockRepository, CertificateLock> {

    private static final Logger log = LoggerFactory.getLogger(CertificateLockService.class);

    @Autowired
    private CertificateService certificateService;

    /**
     * Create certificate lock record
     * Maps to: CertificateLockDAL.AddCertificateLock
     */
    public Result<Void> createCertificateLock(CertificateLock certificateLock) {
        log.info("Creating certificate lock for certificate: {}", certificateLock.getCertificateId());

        // Validate certificate exists
        Certificate certificate = certificateService.getById(certificateLock.getCertificateId());
        if (certificate == null) {
            log.error("Certificate not found: {}", certificateLock.getCertificateId());
            return Result.error("证书不存在");
        }

        // Copy certificate information
        certificateLock.setCertificateCode(certificate.getCertificateCode());
        certificateLock.setWorkerId(certificate.getWorkerId());
        certificateLock.setWorkerName(certificate.getWorkerName());
        certificateLock.setUnitCode(certificate.getUnitCode());
        certificateLock.setPostTypeId(certificate.getPostTypeId());
        certificateLock.setPostId(certificate.getPostId());
        certificateLock.setCertificateType(certificate.getCertificateType());

        // Set lock date and status
        certificateLock.setLockDate(LocalDate.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd")));
        certificateLock.setLockStatus("已锁定");

        boolean success = save(certificateLock);

        if (success) {
            log.info("Certificate lock created successfully");
            return Result.success();
        } else {
            log.error("Failed to create certificate lock");
            return Result.error("证书锁定记录创建失败");
        }
    }

    /**
     * Get certificate lock record by ID
     */
    public Result<CertificateLock> getCertificateLockById(Long certificateLockId) {
        log.debug("Getting certificate lock: {}", certificateLockId);
        CertificateLock certificateLock = getById(certificateLockId);

        if (certificateLock == null) {
            return Result.error("证书锁定记录不存在");
        }

        return Result.success(certificateLock);
    }

    /**
     * List certificate lock records with pagination
     * Maps to: CertificateLockDAL.GetCertificateLockList
     */
    public Result<List<CertificateLock>> listCertificateLocks(Long certificateId, Long workerId, String lockStatus) {
        log.debug("Listing certificate locks: certificateId={}, workerId={}, lockStatus={}", certificateId, workerId, lockStatus);

        com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<CertificateLock> queryWrapper =
                new com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<>();

        if (certificateId != null) {
            queryWrapper.eq(CertificateLock::getCertificateId, certificateId);
        }
        if (workerId != null) {
            queryWrapper.eq(CertificateLock::getWorkerId, workerId);
        }
        if (lockStatus != null) {
            queryWrapper.eq(CertificateLock::getLockStatus, lockStatus);
        }

        queryWrapper.orderByDesc(CertificateLock::getCreateTime);

        List<CertificateLock> certificateLocks = list(queryWrapper);
        return Result.success(certificateLocks);
    }

    /**
     * Lock certificate
     * Maps to: CertificateLockDAL.LockCertificate
     */
    public Result<Void> lockCertificate(Long certificateId, String lockType, String lockReason, String lockMan) {
        log.info("Locking certificate: {}, lockType: {}, lockMan: {}", certificateId, lockType, lockMan);

        Certificate certificate = certificateService.getById(certificateId);
        if (certificate == null) {
            return Result.error("证书不存在");
        }

        // Check if certificate is already locked
        List<CertificateLock> existingLocks = baseMapper.findByCertificateId(certificateId);
        for (CertificateLock lock : existingLocks) {
            if ("已锁定".equals(lock.getLockStatus())) {
                return Result.error("证书已被锁定");
            }
        }

        // Create lock record
        CertificateLock certificateLock = new CertificateLock();
        certificateLock.setCertificateId(certificateId);
        certificateLock.setLockType(lockType);
        certificateLock.setLockReason(lockReason);
        certificateLock.setLockMan(lockMan);
        certificateLock.setLockDate(LocalDate.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd")));
        certificateLock.setLockStatus("已锁定");

        boolean saveResult = save(certificateLock);

        if (saveResult) {
            log.info("Certificate locked successfully");
            return Result.success();
        } else {
            return Result.error("证书锁定失败");
        }
    }

    /**
     * Unlock certificate
     * Maps to: CertificateLockDAL.UnlockCertificate
     */
    public Result<Void> unlockCertificate(Long certificateLockId, String unlockReason, String unlockMan) {
        log.info("Unlocking certificate: {}, unlockMan: {}, reason: {}", certificateLockId, unlockMan, unlockReason);

        CertificateLock certificateLock = getById(certificateLockId);
        if (certificateLock == null) {
            return Result.error("证书锁定记录不存在");
        }

        if (!"已锁定".equals(certificateLock.getLockStatus())) {
            return Result.error("只有已锁定的证书才能解锁");
        }

        certificateLock.setLockStatus("已解锁");
        certificateLock.setUnlockMan(unlockMan);
        certificateLock.setUnlockDate(LocalDate.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd")));
        certificateLock.setUnlockReason(unlockReason);
        boolean success = updateById(certificateLock);

        if (success) {
            log.info("Certificate unlocked successfully");
            return Result.success();
        } else {
            return Result.error("证书解锁失败");
        }
    }

    /**
     * Get currently locked certificates
     * Maps to: CertificateLockDAL.GetLockedCertificates
     */
    public Result<List<CertificateLock>> getLockedCertificates() {
        log.debug("Getting currently locked certificates");

        List<CertificateLock> certificateLocks = baseMapper.findLockedCertificates();

        return Result.success(certificateLocks);
    }

    /**
     * Get certificate lock records by status
     */
    public Result<List<CertificateLock>> getCertificateLocksByStatus(String lockStatus) {
        log.debug("Getting certificate locks by status: {}", lockStatus);

        List<CertificateLock> certificateLocks = baseMapper.findByLockStatus(lockStatus);

        return Result.success(certificateLocks);
    }

    /**
     * Get certificate lock records by certificate
     */
    public Result<List<CertificateLock>> getCertificateLocksByCertificate(Long certificateId) {
        log.debug("Getting certificate locks for certificate: {}", certificateId);

        List<CertificateLock> certificateLocks = baseMapper.findByCertificateId(certificateId);

        return Result.success(certificateLocks);
    }

    /**
     * Get recent certificate lock records
     */
    public Result<List<CertificateLock>> getRecentCertificateLocks(int days) {
        log.debug("Getting recent certificate locks (last {} days)", days);

        List<CertificateLock> certificateLocks = baseMapper.findRecent(days);

        return Result.success(certificateLocks);
    }
}
