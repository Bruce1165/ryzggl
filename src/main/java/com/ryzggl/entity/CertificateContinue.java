package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;
import com.fasterxml.jackson.annotation.JsonFormat;
import com.ryzggl.common.BaseEntity;

/**
 * CertificateContinue entity - Certificate continuation record (证书延续)
 * Replaces .NET: CertificateContinue.cs / CertificateContinueDAL.cs
 */
@TableName("CERTIFICATECONTINUE")
public class CertificateContinue extends BaseEntity {

    @TableField("certificateid")
    private Long certificateId;

    @TableField("certificatecode")
    private String certificateCode;

    @TableField("workerid")
    private Long workerId;

    @TableField("workername")
    private String workerName;

    @TableField("unitcode")
    private String unitCode;

    @TableField("posttypeid")
    private Integer postTypeId;

    @TableField("postid")
    private Integer postId;

    @TableField("certificatetype")
    private String certificateType;

    @TableField("oldvalidstartdate")
    private String oldValidStartDate;

    @TableField("oldvalidenddate")
    private String oldValidEndDate;

    @TableField("newvalidstartdate")
    private String newValidStartDate;

    @TableField("newvalidenddate")
    private String newValidEndDate;

    @TableField("continueperiod")
    private Integer continuePeriod;

    @TableField("continuetype")
    private String continueType;

    @TableField("continuereason")
    private String continueReason;

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
    @TableField("effectivedate")
    private String effectiveDate;

    @TableField("remark")
    private String remark;

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

    public Long getWorkerId() {
        return workerId;
    }

    public void setWorkerId(Long workerId) {
        this.workerId = workerId;
    }

    public String getWorkerName() {
        return workerName;
    }

    public void setWorkerName(String workerName) {
        this.workerName = workerName;
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

    public String getCertificateType() {
        return certificateType;
    }

    public void setCertificateType(String certificateType) {
        this.certificateType = certificateType;
    }

    public String getOldValidStartDate() {
        return oldValidStartDate;
    }

    public void setOldValidStartDate(String oldValidStartDate) {
        this.oldValidStartDate = oldValidStartDate;
    }

    public String getOldValidEndDate() {
        return oldValidEndDate;
    }

    public void setOldValidEndDate(String oldValidEndDate) {
        this.oldValidEndDate = oldValidEndDate;
    }

    public String getNewValidStartDate() {
        return newValidStartDate;
    }

    public void setNewValidStartDate(String newValidStartDate) {
        this.newValidStartDate = newValidStartDate;
    }

    public String getNewValidEndDate() {
        return newValidEndDate;
    }

    public void setNewValidEndDate(String newValidEndDate) {
        this.newValidEndDate = newValidEndDate;
    }

    public Integer getContinuePeriod() {
        return continuePeriod;
    }

    public void setContinuePeriod(Integer continuePeriod) {
        this.continuePeriod = continuePeriod;
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
