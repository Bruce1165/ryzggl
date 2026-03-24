package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;
import com.fasterxml.jackson.annotation.JsonFormat;
import com.ryzggl.common.BaseEntity;

/**
 * ApplyContinue entity - Continuation application (延续申请)
 * Replaces .NET: ApplyContinue.cs / ApplyContinueDAL.cs
 */
@TableName("ApplyContinue")
public class ApplyContinue extends BaseEntity {

    @TableField("workerid")
    private Long workerId;

    @TableField("unitcode")
    private String unitCode;

    @TableField("posttypeid")
    private Integer postTypeId;

    @TableField("postid")
    private Integer postId;

    @TableField("certificatetype")
    private Integer certificateType;

    @TableField("certificateid")
    private Long certificateId;

    @TableField("certificatecode")
    private String certificateCode;

    @TableField("validstartdate")
    private String validStartDate;

    @TableField("validenddate")
    private String validEndDate;

    @TableField("continuetype")
    private String continueType;

    @TableField("continuereason")
    private String continueReason;

    @TableField("continueperiod")
    private Integer continuePeriod;

    @TableField("applystatus")
    private String applyStatus;

    @TableField("checkman")
    private String checkMan;

    @TableField("checkadvise")
    private String checkAdvise;

    @JsonFormat(pattern = "yyyy-MM-dd", timezone = "GMT+8")
    @TableField("checkdate")
    private String checkDate;

    @JsonFormat(pattern = "yyyy-MM-dd", timezone = "GMT+8")
    @TableField("printdate")
    private String printDate;

    @JsonFormat(pattern = "yyyy-MM-dd", timezone = "GMT+8")
    @TableField("effectivedate")
    private String effectiveDate;

    @TableField("remark")
    private String remark;

    public Long getWorkerId() {
        return workerId;
    }

    public void setWorkerId(Long workerId) {
        this.workerId = workerId;
    }

    public String getUnitCode() {
        return unitCode;
    }

    public void setUnitCode(String unitCode) {
        this.unitCode = unitCode;
    }

    public Integer getPostTypeId() {
        return postTypeId;
    }

    public void setPostTypeId(Integer postTypeId) {
        this.postTypeId = postTypeId;
    }

    public Integer getPostId() {
        return postId;
    }

    public void setPostId(Integer postId) {
        this.postId = postId;
    }

    public Integer getCertificateType() {
        return certificateType;
    }

    public void setCertificateType(Integer certificateType) {
        this.certificateType = certificateType;
    }

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

    public String getValidStartDate() {
        return validStartDate;
    }

    public void setValidStartDate(String validStartDate) {
        this.validStartDate = validStartDate;
    }

    public String getValidEndDate() {
        return validEndDate;
    }

    public void setValidEndDate(String validEndDate) {
        this.validEndDate = validEndDate;
    }

    public String getContinueType() {
        return continueType;
    }

    public void setContinueType(String continueType) {
        this.continueType = continueType;
    }

    public String getContinueReason() {
        return continueReason;
    }

    public void setContinueReason(String continueReason) {
        this.continueReason = continueReason;
    }

    public Integer getContinuePeriod() {
        return continuePeriod;
    }

    public void setContinuePeriod(Integer continuePeriod) {
        this.continuePeriod = continuePeriod;
    }

    public String getApplyStatus() {
        return applyStatus;
    }

    public void setApplyStatus(String applyStatus) {
        this.applyStatus = applyStatus;
    }

    public String getCheckMan() {
        return checkMan;
    }

    public void setCheckMan(String checkMan) {
        this.checkMan = checkMan;
    }

    public String getCheckAdvise() {
        return checkAdvise;
    }

    public void setCheckAdvise(String checkAdvise) {
        this.checkAdvise = checkAdvise;
    }

    public String getCheckDate() {
        return checkDate;
    }

    public void setCheckDate(String checkDate) {
        this.checkDate = checkDate;
    }

    public String getPrintDate() {
        return printDate;
    }

    public void setPrintDate(String printDate) {
        this.printDate = printDate;
    }

    public String getEffectiveDate() {
        return effectiveDate;
    }

    public void setEffectiveDate(String effectiveDate) {
        this.effectiveDate = effectiveDate;
    }

    public String getRemark() {
        return remark;
    }

    public void setRemark(String remark) {
        this.remark = remark;
    }
}
