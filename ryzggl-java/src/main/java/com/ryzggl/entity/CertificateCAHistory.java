package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;

/**
 * CertificateCAHistory Entity
 * 证书CA历史实体类
 *
 * Maps to legacy CertificateCAHistoryMDL.cs and CertificateCAHistoryDAL.cs
 * Table: CertificateCAHistory
 *
 * Business Context:
 * - Manages certificate authority (CA) history for certificates
 * - Tracks CA application, sending, and return times
 * - Links to main Certificate entity
 * - Provides audit trail for CA processing
 */
@TableName("CertificateCAHistory")
public class CertificateCAHistory {

    /**
     * Primary key - CertificateCAID
     * 证书CA历史ID (主键)
     */
    @TableId
    private String certificateCaId;

    /**
     * Apply CA Time
     * 申请CA时间
     */
    private String applyCaTime;

    /**
     * Send CA Time
     * 发送CA时间
     */
    private String sendCaTime;

    /**
     * Return CA Time
     * 返回CA时间
     */
    private String returnCaTime;

    /**
     * Certificate ID
     * 证书ID
     */
    private Long certificateId;

    // Getters and Setters
    public String getCertificateCaId() {
        return certificateCaId;
    }

    public void setCertificateCaId(String certificateCaId) {
        this.certificateCaId = certificateCaId;
    }

    public String getApplyCaTime() {
        return applyCaTime;
    }

    public void setApplyCaTime(String applyCaTime) {
        this.applyCaTime = applyCaTime;
    }

    public String getSendCaTime() {
        return sendCaTime;
    }

    public void setSendCaTime(String sendCaTime) {
        this.sendCaTime = sendCaTime;
    }

    public String getReturnCaTime() {
        return returnCaTime;
    }

    public void setReturnCaTime(String returnCaTime) {
        this.returnCaTime = returnCaTime;
    }

    public Long getCertificateId() {
        return certificateId;
    }

    public void setCertificateId(Long certificateId) {
        this.certificateId = certificateId;
    }
}
