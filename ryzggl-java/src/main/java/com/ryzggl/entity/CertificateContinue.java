package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableName;
import com.ryzggl.common.BaseEntity;
import java.time.LocalDate;

/**
 * CertificateContinue Entity - 证书延续表
 */
@TableName("CertificateContinue")
public class CertificateContinue extends BaseEntity {

    /**
     * 证书ID
     */
    @TableField("CERTIFICATEID")
    private Long certificateId;

    /**
     * 证书编号
     */
    @TableField("CERTIFICATECODE")
    private String certificateCode;

    /**
     * 延续类型（年度延续、专项延续）
     */
    @TableField("CONTINUETYPE")
    private String continueType;

    /**
     * 延续开始日期
     */
    @TableField("CONTINUESTARTDATE")
    private LocalDate continueStartDate;

    /**
     * 延续结束日期
     */
    @TableField("CONTINUEENDDATE")
    private LocalDate continueEndDate;

    /**
     * 延续年数
     */
    @TableField("CONTINUEYEARS")
    private Integer continueYears;

    /**
     * 延续费用
     */
    @TableField("CONTINUEFEE")
    private Double continueFee;

    /**
     * 延续原因
     */
    @TableField("CONTINUEREASON")
    private String continueReason;

    /**
     * 延续状态（未延续、已延续、已驳回）
     */
    @TableField("CONTINUESTATUS")
    private String continueStatus;

    /**
     * 延续人
     */
    @TableField("CONTINUEMAN")
    private String continueMan;

    /**
     * 延续人ID
     */
    @TableField("CONTINUEMANID")
    private Long continueManId;

    /**
     * 延续日期
     */
    @TableField("CONTINUEDATE")
    private LocalDate continueDate;

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
     * 审批日期
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
    public Long getCertificateId() {
        return certificateId;
    }

    public void setCertificateId(Long certificateId) {
        this.certificateId = certificateId;
    }

    public String getCertificateCode() {
        return certificateCode;
    }

    public void setCertificateCode(String certificateCode) {
        this.certificateCode = certificateCode;
    }

    public String getContinueType() {
        return continueType;
    }

    public void setContinueType(String continueType) {
        this.continueType = continueType;
    }

    public LocalDate getContinueStartDate() {
        return continueStartDate;
    }

    public void setContinueStartDate(LocalDate continueStartDate) {
        this.continueStartDate = continueStartDate;
    }

    public LocalDate getContinueEndDate() {
        return continueEndDate;
    }

    public void setContinueEndDate(LocalDate continueEndDate) {
        this.continueEndDate = continueEndDate;
    }

    public Integer getContinueYears() {
        return continueYears;
    }

    public void setContinueYears(Integer continueYears) {
        this.continueYears = continueYears;
    }

    public Double getContinueFee() {
        return continueFee;
    }

    public void setContinueFee(Double continueFee) {
        this.continueFee = continueFee;
    }

    public String getContinueReason() {
        return continueReason;
    }

    public void setContinueReason(String continueReason) {
        this.continueReason = continueReason;
    }

    public String getContinueStatus() {
        return continueStatus;
    }

    public void setContinueStatus(String continueStatus) {
        this.continueStatus = continueStatus;
    }

    public String getContinueMan() {
        return continueMan;
    }

    public void setContinueMan(String continueMan) {
        this.continueMan = continueMan;
    }

    public Long getContinueManId() {
        return continueManId;
    }

    public void setContinueManId(Long continueManId) {
        this.continueManId = continueManId;
    }

    public LocalDate getContinueDate() {
        return continueDate;
    }

    public void setContinueDate(LocalDate continueDate) {
        this.continueDate = continueDate;
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
        CertificateContinue that = (CertificateContinue) o;
        return certificateId != null && certificateId.equals(that.certificateId);
    }

    @Override
    public int hashCode() {
        return super.hashCode() * 31 + (certificateId != null ? certificateId.hashCode() : 0);
    }
}