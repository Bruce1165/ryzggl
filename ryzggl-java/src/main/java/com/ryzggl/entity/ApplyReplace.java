package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;
import com.fasterxml.jackson.annotation.JsonFormat;

import java.time.LocalDate;
import java.time.LocalDateTime;

/**
 * ApplyReplace Entity - Certificate Replace Application
 * 证书补办申请实体类
 *
 * Maps to legacy ApplyReplaceMDL.cs
 * Table: ApplyReplace
 *
 * Business Context:
 * - ApplyReplace handles certificate replacement/reissuance applications
 * - Worker can apply for certificate replacement when original is lost/damaged
 * - Supports up to 4 professional qualifications per application
 * - Includes contact information for both person and enterprise
 */
@TableName("ApplyReplace")
public class ApplyReplace {

    /**
     * Primary key - Apply ID
     * 申请ID (主键)
     */
    @TableId(type = IdType.ASSIGN_ID)
    private String applyId;

    /**
     * Personnel mobile phone
     * 人员手机号
     */
    private String psnMobilePhone;

    /**
     * Personnel email
     * 人员邮箱
     */
    private String psnEmail;

    /**
     * Registration number
     * 注册号
     */
    private String registerNo;

    /**
     * Registration certificate number
     * 注册证书号
     */
    private String registerCertificateNo;

    /**
     * Disable/invalid date
     * 失效日期
     */
    @JsonFormat(pattern = "yyyy-MM-dd")
    private LocalDate disnableDate;

    /**
     * Validation code
     * 验证码
     */
    private String validCode;

    /**
     * Contact person
     * 联系人
     */
    private String linkMan;

    /**
     * Enterprise telephone
     * 企业电话
     */
    private String entTelephone;

    /**
     * Enterprise address/correspondence
     * 企业地址
     */
    private String entCorrespondence;

    /**
     * Enterprise postcode
     * 企业邮编
     */
    private String entPostcode;

    /**
     * Registered profession 1
     * 注册专业1
     */
    private String psnRegisteProfession1;

    /**
     * Certificate validity 1
     * 证书有效期1
     */
    @JsonFormat(pattern = "yyyy-MM-dd")
    private LocalDate psnCertificateValidity1;

    /**
     * Registered profession 2
     * 注册专业2
     */
    private String psnRegisteProfession2;

    /**
     * Certificate validity 2
     * 证书有效期2
     */
    @JsonFormat(pattern = "yyyy-MM-dd")
    private LocalDate psnCertificateValidity2;

    /**
     * Registered profession 3
     * 注册专业3
     */
    private String psnRegisteProfession3;

    /**
     * Certificate validity 3
     * 证书有效期3
     */
    @JsonFormat(pattern = "yyyy-MM-dd")
    private LocalDate psnCertificateValidity3;

    /**
     * Registered profession 4
     * 注册专业4
     */
    private String psnRegisteProfession4;

    /**
     * Certificate validity 4
     * 证书有效期4
     */
    @JsonFormat(pattern = "yyyy-MM-dd")
    private LocalDate psnCertificateValidity4;

    /**
     * Replace reason
     * 补办原因
     */
    private String replaceReason;

    /**
     * Replace type
     * 补办类型
     */
    private String replaceType;

    /**
     * Application status (inferred from workflow)
     * 申请状态 (not in original table, added for workflow)
     */
    private String status;

    /**
     * Create time (auto-populated)
     * 创建时间
     */
    @JsonFormat(pattern = "yyyy-MM-dd HH:mm:ss")
    private LocalDateTime createTime;

    /**
     * Update time (auto-populated)
     * 更新时间
     */
    @JsonFormat(pattern = "yyyy-MM-dd HH:mm:ss")
    private LocalDateTime updateTime;

    // Getters and Setters
    public String getApplyId() {
        return applyId;
    }

    public void setApplyId(String applyId) {
        this.applyId = applyId;
    }

    public String getPsnMobilePhone() {
        return psnMobilePhone;
    }

    public void setPsnMobilePhone(String psnMobilePhone) {
        this.psnMobilePhone = psnMobilePhone;
    }

    public String getPsnEmail() {
        return psnEmail;
    }

    public void setPsnEmail(String psnEmail) {
        this.psnEmail = psnEmail;
    }

    public String getRegisterNo() {
        return registerNo;
    }

    public void setRegisterNo(String registerNo) {
        this.registerNo = registerNo;
    }

    public String getRegisterCertificateNo() {
        return registerCertificateNo;
    }

    public void setRegisterCertificateNo(String registerCertificateNo) {
        this.registerCertificateNo = registerCertificateNo;
    }

    public LocalDate getDisnableDate() {
        return disnableDate;
    }

    public void setDisnableDate(LocalDate disnableDate) {
        this.disnableDate = disnableDate;
    }

    public String getValidCode() {
        return validCode;
    }

    public void setValidCode(String validCode) {
        this.validCode = validCode;
    }

    public String getLinkMan() {
        return linkMan;
    }

    public void setLinkMan(String linkMan) {
        this.linkMan = linkMan;
    }

    public String getEntTelephone() {
        return entTelephone;
    }

    public void setEntTelephone(String entTelephone) {
        this.entTelephone = entTelephone;
    }

    public String getEntCorrespondence() {
        return entCorrespondence;
    }

    public void setEntCorrespondence(String entCorrespondence) {
        this.entCorrespondence = entCorrespondence;
    }

    public String getEntPostcode() {
        return entPostcode;
    }

    public void setEntPostcode(String entPostcode) {
        this.entPostcode = entPostcode;
    }

    public String getPsnRegisteProfession1() {
        return psnRegisteProfession1;
    }

    public void setPsnRegisteProfession1(String psnRegisteProfession1) {
        this.psnRegisteProfession1 = psnRegisteProfession1;
    }

    public LocalDate getPsnCertificateValidity1() {
        return psnCertificateValidity1;
    }

    public void setPsnCertificateValidity1(LocalDate psnCertificateValidity1) {
        this.psnCertificateValidity1 = psnCertificateValidity1;
    }

    public String getPsnRegisteProfession2() {
        return psnRegisteProfession2;
    }

    public void setPsnRegisteProfession2(String psnRegisteProfession2) {
        this.psnRegisteProfession2 = psnRegisteProfession2;
    }

    public LocalDate getPsnCertificateValidity2() {
        return psnCertificateValidity2;
    }

    public void setPsnCertificateValidity2(LocalDate psnCertificateValidity2) {
        this.psnCertificateValidity2 = psnCertificateValidity2;
    }

    public String getPsnRegisteProfession3() {
        return psnRegisteProfession3;
    }

    public void setPsnRegisteProfession3(String psnRegisteProfession3) {
        this.psnRegisteProfession3 = psnRegisteProfession3;
    }

    public LocalDate getPsnCertificateValidity3() {
        return psnCertificateValidity3;
    }

    public void setPsnCertificateValidity3(LocalDate psnCertificateValidity3) {
        this.psnCertificateValidity3 = psnCertificateValidity3;
    }

    public String getPsnRegisteProfession4() {
        return psnRegisteProfession4;
    }

    public void setPsnRegisteProfession4(String psnRegisteProfession4) {
        this.psnRegisteProfession4 = psnRegisteProfession4;
    }

    public LocalDate getPsnCertificateValidity4() {
        return psnCertificateValidity4;
    }

    public void setPsnCertificateValidity4(LocalDate psnCertificateValidity4) {
        this.psnCertificateValidity4 = psnCertificateValidity4;
    }

    public String getReplaceReason() {
        return replaceReason;
    }

    public void setReplaceReason(String replaceReason) {
        this.replaceReason = replaceReason;
    }

    public String getReplaceType() {
        return replaceType;
    }

    public void setReplaceType(String replaceType) {
        this.replaceType = replaceType;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    public LocalDateTime getCreateTime() {
        return createTime;
    }

    public void setCreateTime(LocalDateTime createTime) {
        this.createTime = createTime;
    }

    public LocalDateTime getUpdateTime() {
        return updateTime;
    }

    public void setUpdateTime(LocalDateTime updateTime) {
        this.updateTime = updateTime;
    }
}
