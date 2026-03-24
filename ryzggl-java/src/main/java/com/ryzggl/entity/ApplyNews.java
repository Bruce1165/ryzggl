package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;

/**
 * ApplyNews Entity
 * 申请新闻实体类
 *
 * Maps to legacy ApplyNewsMDL.cs and ApplyNewsDAL.cs
 * Table: ApplyNews
 *
 * Business Context:
 * - Manages application news/notifications
 * - Tracks personnel name, certificate, and register number
 * - Supports apply type categorization
 * - SFCK flag for checked status
 * - Organization code filtering
 */
@TableName("ApplyNews")
public class ApplyNews {

    /**
     * Primary key - ID
     * 申请新闻ID (主键)
     */
    @TableId
    private String id;

    /**
     * Person Name
     * 人员姓名
     */
    private String psnName;

    /**
     * Certificate No
     * 证件号码
     */
    private String psnCertificateNo;

    /**
     * Register No
     * 注册编号
     */
    private String psnRegisterNo;

    /**
     * Apply Type
     * 申报类型
     */
    private String applyType;

    /**
     * Checked flag (SFCK)
     * 是否已审核
     */
    private Boolean sfck;

    /**
     * Enterprise Organization Code
     * 企业组织机构代码
     */
    private String entOrganizationsCode;

    // Getters and Setters
    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getPsnName() {
        return psnName;
    }

    public void setPsnName(String psnName) {
        this.psnName = psnName;
    }

    public String getPsnCertificateNo() {
        return psnCertificateNo;
    }

    public void setPsnCertificateNo(String psnCertificateNo) {
        this.psnCertificateNo = psnCertificateNo;
    }

    public String getPsnRegisterNo() {
        return psnRegisterNo;
    }

    public void setPsnRegisterNo(String psnRegisterNo) {
        this.psnRegisterNo = psnRegisterNo;
    }

    public String getApplyType() {
        return applyType;
    }

    public void setApplyType(String applyType) {
        this.applyType = applyType;
    }

    public Boolean getSfck() {
        return sfck;
    }

    public void setSfck(Boolean sfck) {
        this.sfck = sfck;
    }

    public String getEntOrganizationsCode() {
        return entOrganizationsCode;
    }

    public void setEntOrganizationsCode(String entOrganizationsCode) {
        this.entOrganizationsCode = entOrganizationsCode;
    }
}
