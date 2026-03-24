package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;
import com.fasterxml.jackson.annotation.JsonFormat;

/**
 * CertificateLock Entity
 * 证书锁定记录实体类
 *
 * Maps to legacy CertificateLockDAL.cs
 * Table: CertificateLock
 *
 * Business Context:
 * - Tracks certificate lock/unlock operations
 * - Supports multiple lock types
 * - Records who locked/unlocked and when
 * - Used for certificate management workflows
 * - Integrates with Certificate entity
 */
@TableName("CertificateLock")
public class CertificateLock {

    /**
     * Primary key - Certificate Lock ID
     * 锁定记录ID (主键)
     */
    @TableId(type = IdType.AUTO)
    private Long lockId;

    /**
     * Certificate ID
     * 证书ID (外键)
     */
    private Long certificateId;

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

    /**
     * Creator ID
     * 创建人
     */
    private Long createPersonId;

    /**
     * Creation Time
     * 创建时间
     */
    @JsonFormat(pattern = "yyyy-MM-dd HH:mm:ss")
    private String createTime;

    /**
     * Modifier ID
     * 修改人
     */
    private Long modifyPersonId;

    /**
     * Modification Time
     * 修改时间
     */
    @JsonFormat(pattern = "yyyy-MM-dd HH:mm:ss")
    private String modifyTime;

    // Getters and Setters
    public Long getLockId() {
        return lockId;
    }

    public void setLockId(Long lockId) {
        this.lockId = lockId;
    }

    public Long getCertificateId() {
        return certificateId;
    }

    public void setCertificateId(Long certificateId) {
        this.certificateId = certificateId;
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

    public Long getCreatePersonId() {
        return createPersonId;
    }

    public void setCreatePersonId(Long createPersonId) {
        this.createPersonId = createPersonId;
    }

    public String getCreateTime() {
        return createTime;
    }

    public void setCreateTime(String createTime) {
        this.createTime = createTime;
    }

    public Long getModifyPersonId() {
        return modifyPersonId;
    }

    public void setModifyPersonId(Long modifyPersonId) {
        this.modifyPersonId = modifyPersonId;
    }

    public String getModifyTime() {
        return modifyTime;
    }

    public void setModifyTime(String modifyTime) {
        this.modifyTime = modifyTime;
    }
}
