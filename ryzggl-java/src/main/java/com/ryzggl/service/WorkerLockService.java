package com.ryzggl.service;

import com.baomidou.mybatisplus.extension.service.IService;
import com.ryzggl.entity.WorkerLock;
import com.ryzggl.repository.WorkerLockRepository;
import com.ryzggl.common.Result;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;

/**
 * WorkerLock Service
 * 人员锁定业务逻辑层
 */
@Service
public class WorkerLockService implements IService<WorkerLock> {

    @Autowired
    private WorkerLockRepository workerLockRepository;

    /**
     * Get lock by ID
     */
    public WorkerLock getById(Long lockId) {
        return workerLockRepository.getById(lockId);
    }

    /**
     * Get locks by worker ID
     */
    public List<WorkerLock> getByWorkerId(Long workerId) {
        return workerLockRepository.getByWorkerId(workerId);
    }

    /**
     * Get last lock for a worker
     */
    public WorkerLock getLastLock(Long workerId) {
        return workerLockRepository.getLastLock(workerId);
    }

    /**
     * Get all active locks
     */
    public List<WorkerLock> getActiveLocks() {
        return workerLockRepository.getActiveLocks();
    }

    /**
     * Search locks
     */
    public List<WorkerLock> search(String keyword) {
        if (keyword == null || keyword.trim().isEmpty()) {
            return workerLockRepository.selectList(null);
        }
        return workerLockRepository.search(keyword);
    }

    /**
     * Lock worker
     */
    @Transactional
    public Result<WorkerLock> lockWorker(Long workerId, String lockType, String lockPerson, String lockEndTime, String remark) {
        // Validate worker ID
        if (workerId == null) {
            return Result.error("人员ID不能为空");
        }

        WorkerLock lock = new WorkerLock();
        lock.setWorkerId(workerId);
        lock.setLockType(lockType);
        lock.setLockPerson(lockPerson);
        lock.setLockEndTime(lockEndTime);
        lock.setRemark(remark);
        lock.setLockStatus("LOCKED");
        lock.setLockTime(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new Date()));

        workerLockRepository.insert(lock);
        return Result.success(lock);
    }

    /**
     * Unlock worker
     */
    @Transactional
    public Result<Void> unlockWorker(Long lockId, String unlockPerson) {
        // Get the active lock
        WorkerLock lastLock = workerLockRepository.getById(lockId);
        if (lastLock == null) {
            return Result.error("锁定记录不存在");
        }

        if (!"LOCKED".equals(lastLock.getLockStatus())) {
            return Result.error("该人员当前未被锁定");
        }

        // Update lock record
        String unlockTime = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new Date());
        workerLockRepository.unlockLock(lockId, unlockTime, unlockPerson);
        return Result.success();
    }

    /**
     * Check if worker is currently locked
     */
    public boolean isWorkerLocked(Long workerId) {
        WorkerLock lastLock = getLastLock(workerId);
        if (lastLock == null) {
            return false;
        }

        if (!"LOCKED".equals(lastLock.getLockStatus())) {
            return false;
        }

        // Check if lock is expired
        if (lastLock.getLockEndTime() != null && !lastLock.getLockEndTime().isEmpty()) {
            try {
                SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
                Date endTime = sdf.parse(lastLock.getLockEndTime());
                if (endTime.before(new Date())) {
                    return false;
                }
            } catch (Exception e) {
                // If parsing fails, consider it locked
            }
        }

        return true;
    }

    /**
     * Delete lock record
     */
    @Transactional
    public Result<Void> deleteLock(Long lockId) {
        WorkerLock existing = getById(lockId);
        if (existing == null) {
            return Result.error("锁定记录不存在");
        }

        workerLockRepository.deleteById(lockId);
        return Result.success();
    }
}
