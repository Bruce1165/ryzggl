package com.ryzggl.service;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.extension.service.IService;
import com.ryzggl.entity.certificateLock;
import com.ryzggl.repository.certificateLockRepository;
import com.ryzggl.common.Result;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;

/**
 * CertificateLock Service
 * 证书锁定管理业务逻辑层
 */
@Service
public class CertificateLockService implements IService<certificateLock> {

    @Autowired
    private CertificateLockRepository certificateLockRepository;

    /**
     * Get lock by ID
     */
    public certificateLock getLockById(Long lockId) {
        return certificateLockRepository.getById(lockId);
    }

    /**
     * Get all locks for a certificate
     */
    public List<certificateLock> getLocksByCertificateId(Long certificateId) {
        return certificateLockRepository.getByCertificateId(certificateId);
    }

    /**
     * Get all active locks
     */
    public List<certificateLock> getActiveLocks() {
        return certificateLockRepository.getActiveLocks();
    }

    /**
     * Get lock history
     */
    public List<certificateLock> getLockHistory(Long certificateId) {
        return certificateLockRepository.getLockHistory(certificateId);
    }

    /**
     * Search locks
     */
    public List<certificateLock> searchLocks(String keyword) {
        if (keyword == null || keyword.trim().isEmpty()) {
            return getActiveLocks();
        }
        return certificateLockRepository.search(keyword);
    }

    /**
     * Lock certificate
     */
    @Transactional
    public Result<certificateLock> lockCertificate(Long certificateId, String lockType, String lockPerson, String remark) {
        // Validate certificate ID
        if (certificateId == null) {
            return Result.error("证书ID不能为空");
        }

        certificateLock lock = new certificateLock();
        lock.setCertificateId(certificateId);
        lock.setLockType(lockType);
        lock.setLockPerson(lockPerson);
        lock.setRemark(remark);
        lock.setLockStatus("LOCKED");

        certificateLockRepository.insert(lock);
        return Result.success(lock);
    }

    /**
     * Unlock certificate
     */
    @Transactional
    public Result<Void> unlockCertificate(Long lockId, String unlockPerson) {
        // Get the active lock
        certificateLock lastLock = certificateLockRepository.getLastLock(certificateId);
        if (lastLock == null) {
            return Result.error("没有找到锁定记录");
        }

        if (!"LOCKED".equals(lastLock.getLockStatus())) {
            return Result.error("证书当前未被锁定");
        }

        // Update lock record
        certificateLock unlockRecord = new certificateLock();
        unlockRecord.setLockId(lastLock.getLockId());
        unlockRecord.setUnlockTime(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new Date()));
        unlockRecord.setUnlockPerson(unlockPerson);
        unlockRecord.setLockStatus("UNLOCKED");

        certificateLockRepository.updateLock(unlockRecord);
        return Result.success();
    }

    /**
     * Search locks
     */
    public List<certificateLock> searchLocks(String keyword) {
        if (keyword == null || keyword.trim().isEmpty()) {
            return getActiveLocks();
        }
        return certificateLockRepository.search(keyword);
    }

    /**
     * Get last lock for a certificate
     */
    public certificateLock getLastLock(Long certificateId) {
        return certificateLockRepository.getLastLock(certificateId);
    }

    /**
     * Check if certificate is currently locked
     */
    public boolean isCertificateLocked(Long certificateId) {
        return certificateLockRepository.isCertificateLocked(certificateId);
    }

    /**
     * Delete lock record
     */
    @Transactional
    public Result<Void> deleteLock(Long lockId) {
        int count = certificateLockRepository.countById(lockId);
        if (count == 0) {
            return Result.error("锁定记录不存在");
        }

        certificateLockRepository.deleteById(lockId);
        return Result.success();
    }
}
