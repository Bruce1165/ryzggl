package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;
import com.fasterxml.jackson.annotation.JsonFormat;
import com.ryzggl.common.BaseEntity;

/**
 * CertificatePause entity - Certificate pause (证书暂停)
 * Replaces .NET: CertificatePause.cs / CertificatePauseDAL.cs
 */
@TableName("CERTIFICATEPAUSE")
public class CertificatePause extends BaseEntity {

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

    @TableField("pausereason")
    private String pauseReason;

    @TableField("pauseman")
    private String pauseMan;

    @JsonFormat(pattern = "yyyy-MM-dd", timezone = "GMT+8")
    @TableField("pausedate")
    private String pauseDate;

    @TableField("resumeman")
    private String resumeMan;

    @JsonFormat(pattern = "yyyy-MM-dd", timezone = "GMT+8")
    @TableField("resumedate")
    private String resumeDate;

    @TableField("resumereason")
    private String resumeReason;

    @TableField("pausestatus")
    private String pauseStatus;

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

    public String getPauseReason() {
        return pauseReason;
    }

    public void setPauseReason(String pauseReason) {
        this.pauseReason = pauseReason;
    }

    public String getPauseMan() {
        return pauseMan;
    }

    public void setPauseMan(String pauseMan) {
        this.pauseMan = pauseMan;
    }

    public String getPauseDate() {
        return pauseDate;
    }

    public void setPauseDate(String pauseDate) {
        this.pauseDate = pauseDate;
    }

    public String getResumeMan() {
        return resumeMan;
    }

    public void setResumeMan(String resumeMan) {
        this.resumeMan = resumeMan;
    }

    public String getResumeDate() {
        return resumeDate;
    }

    public void setResumeDate(String resumeDate) {
        this.resumeDate = resumeDate;
    }

    public String getResumeReason() {
        return resumeReason;
    }

    public void setResumeReason(String resumeReason) {
        this.resumeReason = resumeReason;
    }

    public String getPauseStatus() {
        return pauseStatus;
    }

    public void setPauseStatus(String pauseStatus) {
        this.pauseStatus = pauseStatus;
    }

    public String getRemark() {
        return remark;
    }

    public void setRemark(String remark) {
        this.remark = remark;
    }
}
