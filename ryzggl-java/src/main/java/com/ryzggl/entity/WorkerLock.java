package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;
import com.fasterxml.jackson.annotation.JsonFormat;

/**
 * WorkerLock Entity
 * 人员锁定记录实体类
 *
 * Maps to legacy WorkerLockDAL.cs and WorkerLockOB.cs
 * Table: WorkerLock (Note: Table name in legacy is WorkerLock with typo as WorkerLock)
 *
 * Business Context:
 * - Tracks worker lock/unlock operations
 * - Similar to CertificateLock but for individual workers
 * - Supports temporary locks with end time
 * - Records who locked/unlocked and when
 * - Used for worker management workflows
 */
@TableName("WorkerLock")
public class WorkerLock {

    /**
     * Primary key - Lock ID
     * 锁定记录ID (主键)
     */
    @TableId(type = IdType.AUTO)
    private Long lockId;

    /**
     * Worker ID
     * 人员ID (外键)
     */
    private Long workerId;

    /**
     * Lock Type
     * 锁定类型
     */
    private String lockType;

    /**
     * Lock Time
     * 锁定时间
     */
    @JsonFormat(pattern = "yyyy-MM-dd HH:mm:ss")
    private String lockTime;

    /**
     * Lock End Time
     * 锁定结束时间
     */
    @JsonFormat(pattern = "yyyy-MM-dd HH:mm:ss")
    private String lockEndTime;

    /**
     * Lock Person
     * 锁定人
     */
    private String lockPerson;

    /**
     * Remark
     * 备注
     */
    private String remark;

    /**
     * Unlock Time
     * 解锁时间
     */
    @JsonFormat(pattern = "yyyy-MM-dd HH:mm:ss")
    private String unlockTime;

    /**
     * Unlock Person
     * 解锁人
     */
    private String unlockPerson;

    /**
     * Lock Status
     * 锁定状态（LOCKED-已锁定，UNLOCKED-已解锁）
     */
    private String lockStatus;

    // Getters and Setters
    public Long getLockId() {
        return lockId;
    }

    public void setLockId(Long lockId) {
        this.lockId = lockId;
    }

    public Long getWorkerId() {
        return workerId;
    }

    public void setWorkerId(Long workerId) {
        this.workerId = workerId;
    }

    public String getLockType() {
        return lockType;
    }

    public void setLockType(String lockType) {
        this.lockType = lockType;
    }

    public String getLockTime() {
        return lockTime;
    }

    public void setLockTime(String lockTime) {
        this.lockTime = lockTime;
    }

    public String getLockEndTime() {
        return lockEndTime;
    }

    public void setLockEndTime(String lockEndTime) {
        this.lockEndTime = lockEndTime;
    }

    public String getLockPerson() {
        return lockPerson;
    }

    public void setLockPerson(String lockPerson) {
        this.lockPerson = lockPerson;
    }

    public String getRemark() {
        return remark;
    }

    public void setRemark(String remark) {
        this.remark = remark;
    }

    public String getUnlockTime() {
        return unlockTime;
    }

    public void setUnlockTime(String unlockTime) {
        this.unlockTime = unlockTime;
    }

    public String getUnlockPerson() {
        return unlockPerson;
    }

    public void setUnlockPerson(String unlockPerson) {
        this.unlockPerson = unlockPerson;
    }

    public String getLockStatus() {
        return lockStatus;
    }

    public void setLockStatus(String lockStatus) {
        this.lockStatus = lockStatus;
    }
}
