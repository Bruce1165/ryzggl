package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;
import com.fasterxml.jackson.annotation.JsonFormat;
import com.ryzggl.common.BaseEntity;

/**
 * CertificateOut entity - Certificate issuance/print (证书发放)
 * Replaces .NET: CertificateOut.cs / CertificateOutDAL.cs
 */
@TableName("CERTIFICATEOUT")
public class CertificateOut extends BaseEntity {

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

    @TableField("outtype")
    private String outType;

    @TableField("outreason")
    private String outReason;

    @TableField("applystatus")
    private String applyStatus;

    @TableField("checkman")
    private String checkMan;

    @TableField("checkadvise")
    private String checkAdvise;

    @JsonFormat(pattern = "yyyy-MM-dd", timezone = "GMT+8")
    @TableField("checkdate")
    private String checkDate;

    @TableField("printman")
    private String printMan;

    @JsonFormat(pattern = "yyyy-MM-dd", timezone = "GMT+8")
    @TableField("printdate")
    private String printDate;

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

    public String getOutType() {
        return outType;
    }

    public void setOutType(String outType) {
        this.outType = outType;
    }

    public String getOutReason() {
        return outReason;
    }

    public void setOutReason(String outReason) {
        this.outReason = outReason;
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

    public String getPrintMan() {
        return printMan;
    }

    public void setPrintMan(String printMan) {
        this.printMan = printMan;
    }

    public String getPrintDate() {
        return printDate;
    }

    public void setPrintDate(String printDate) {
        this.printDate = printDate;
    }

    public String getRemark() {
        return remark;
    }

    public void setRemark(String remark) {
        this.remark = remark;
    }
}
