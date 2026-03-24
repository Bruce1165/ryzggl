package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;
import com.fasterxml.jackson.annotation.JsonFormat;
import com.ryzggl.common.BaseEntity;

/**
 * CertificateMerge entity - Certificate merge (证书合并)
 * Replaces .NET: CertificateMerge.cs / CertificateMergeDAL.cs
 */
@TableName("CERTIFICATEGEMERGE")
public class CertificateMerge extends BaseEntity {

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

    @TableField("mergetype")
    private String mergeType;

    @TableField("merge reason")
    private String mergeReason;

    @TableField("fromcertificateid")
    private Long fromCertificateId;

    @TableField("fromcertificatecode")
    private String fromCertificateCode;

    @TableField("tocertificateid")
    private Long toCertificateId;

    @TableField("tocertificatecode")
    private String toCertificateCode;

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
    @TableField("mergedate")
    private String mergeDate;

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

    public String getMergeType() {
        return mergeType;
    }

    public void setMergeType(String mergeType) {
        this.mergeType = mergeType;
    }

    public String getMergeReason() {
        return mergeReason;
    }

    public void setMergeReason(String mergeReason) {
        this.mergeReason = mergeReason;
    }

    public Long getFromCertificateId() {
        return fromCertificateId;
    }

    public void setFromCertificateId(Long fromCertificateId) {
        this.fromCertificateId = fromCertificateId;
    }

    public String getFromCertificateCode() {
        return fromCertificateCode;
    }

    public void setFromCertificateCode(String fromCertificateCode) {
        this.fromCertificateCode = fromCertificateCode;
    }

    public Long getToCertificateId() {
        return toCertificateId;
    }

    public void setToCertificateId(Long toCertificateId) {
        this.toCertificateId = toCertificateId;
    }

    public String getToCertificateCode() {
        return toCertificateCode;
    }

    public void setToCertificateCode(String toCertificateCode) {
        this.toCertificateCode = toCertificateCode;
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

    public String getMergeDate() {
        return mergeDate;
    }

    public void setMergeDate(String mergeDate) {
        this.mergeDate = mergeDate;
    }

    public String getRemark() {
        return remark;
    }

    public void setRemark(String remark) {
        this.remark = remark;
    }
}
