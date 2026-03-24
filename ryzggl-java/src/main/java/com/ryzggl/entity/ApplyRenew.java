package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableName;
import com.ryzggl.common.BaseEntity;
import java.time.LocalDate;

/**
 * ApplyRenew Entity - 续期申请表
 */
@TableName("ApplyRenew")
public class ApplyRenew extends BaseEntity {

    /**
     * 申请编号
     */
    @TableField("APPLYNO")
    private String applyNo;

    /**
     * 申请人ID
     */
    @TableField("WORKERID")
    private Long workerId;

    /**
     * 申请人姓名
     */
    @TableField("APPLICANTNAME")
    private String applicantName;

    /**
     * 身份证号
     */
    @TableField("IDCARD")
    private String idCard;

    /**
     * 手机号
     */
    @TableField("PHONE")
    private String phone;

    /**
     * 所属企业代码
     */
    @TableField("UNITCODE")
    private String unitCode;

    /**
     * 企业名称
     */
    @TableField("UNITNAME")
    private String unitName;

    /**
     * 证书编号
     */
    @TableField("CERTIFICATECODE")
    private String certificateCode;

    /**
     * 续期原因
     */
    @TableField("RENEWREASON")
    private String renewReason;

    /**
     * 续期类型
     */
    @TableField("RENEWTYPE")
    private String renewType;

    /**
     * 续期开始日期
     */
    @TableField("RENEWSTARTDATE")
    private LocalDate renewStartDate;

    /**
     * 续期结束日期
     */
    @TableField("RENEWENDDATE")
    private LocalDate renewEndDate;

    /**
     * 申请状态（未填写、待确认、已申报、已受理、已驳回）
     */
    @TableField("APPLYSTATUS")
    private String applyStatus;

    /**
     * 审批意见
     */
    @TableField("CHECKADVISE")
    private String checkAdvise;

    /**
     * 审批人
     */
    @TableField("CHECKMAN")
    private String checkMan;

    /**
     * 审批时间
     */
    @TableField("CHECKDATE")
    private String checkDate;

    /**
     * 附件路径
     */
    @TableField("FILEPATH")
    private String filePath;

    /**
     * 备注
     */
    @TableField("REMARK")
    private String remark;

    // Getters and Setters
    public String getApplyNo() {
        return applyNo;
    }

    public void setApplyNo(String applyNo) {
        this.applyNo = applyNo;
    }

    public Long getWorkerId() {
        return workerId;
    }

    public void setWorkerId(Long workerId) {
        this.workerId = workerId;
    }

    public String getApplicantName() {
        return applicantName;
    }

    public void setApplicantName(String applicantName) {
        this.applicantName = applicantName;
    }

    public String getIdCard() {
        return idCard;
    }

    public void setIdCard(String idCard) {
        this.idCard = idCard;
    }

    public String getPhone() {
        return phone;
    }

    public void setPhone(String phone) {
        this.phone = phone;
    }

    public String getUnitCode() {
        return unitCode;
    }

    public void setUnitCode(String unitCode) {
        this.unitCode = unitCode;
    }

    public String getUnitName() {
        return unitName;
    }

    public void setUnitName(String unitName) {
        this.unitName = unitName;
    }

    public String getCertificateCode() {
        return certificateCode;
    }

    public void setCertificateCode(String certificateCode) {
        this.certificateCode = certificateCode;
    }

    public String getRenewReason() {
        return renewReason;
    }

    public void setRenewReason(String renewReason) {
        this.renewReason = renewReason;
    }

    public String getRenewType() {
        return renewType;
    }

    public void setRenewType(String renewType) {
        this.renewType = renewType;
    }

    public LocalDate getRenewStartDate() {
        return renewStartDate;
    }

    public void setRenewStartDate(LocalDate renewStartDate) {
        this.renewStartDate = renewStartDate;
    }

    public LocalDate getRenewEndDate() {
        return renewEndDate;
    }

    public void setRenewEndDate(LocalDate renewEndDate) {
        this.renewEndDate = renewEndDate;
    }

    public String getApplyStatus() {
        return applyStatus;
    }

    public void setApplyStatus(String applyStatus) {
        this.applyStatus = applyStatus;
    }

    public String getCheckAdvise() {
        return checkAdvise;
    }

    public void setCheckAdvise(String checkAdvise) {
        this.checkAdvise = checkAdvise;
    }

    public String getCheckMan() {
        return checkMan;
    }

    public void setCheckMan(String checkMan) {
        this.checkMan = checkMan;
    }

    public String getCheckDate() {
        return checkDate;
    }

    public void setCheckDate(String checkDate) {
        this.checkDate = checkDate;
    }

    public String getFilePath() {
        return filePath;
    }

    public void setFilePath(String filePath) {
        this.filePath = filePath;
    }

    public String getRemark() {
        return remark;
    }

    public void setRemark(String remark) {
        this.remark = remark;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        if (!super.equals(o)) return false;
        ApplyRenew that = (ApplyRenew) o;
        return applyNo != null && applyNo.equals(that.applyNo);
    }

    @Override
    public int hashCode() {
        return super.hashCode() * 31 + (applyNo != null ? applyNo.hashCode() : 0);
    }
}