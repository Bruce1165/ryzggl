package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;
import com.fasterxml.jackson.annotation.JsonFormat;
import com.ryzggl.common.BaseEntity;

/**
 * Certificate entity - Core certificate table
 * Maps to: Certificate.cs / CertificateOB.cs / CertificateDAL.cs
 * Key lifecycle: Enter, Change, Continue, Pause, Merge, Lock
 */
@TableName("CERTIFICATE")
public class Certificate extends BaseEntity {

    /**
     * Linked worker ID
     */
    @TableField("workerid")
    private Long workerId;

    /**
     * Certificate type
     * Maps to: CERTIFICATETYPE
     */
    @TableField("certificatetype")
    private String certificateType;

    /**
     * Position type ID
     */
    @TableField("posttypeid")
    private Integer postTypeId;

    /**
     * Position ID
     */
    @TableField("postid")
    private Integer postId;

    /**
     * Certificate number
     */
    @TableField("certificatecode")
    private String certificateCode;

    /**
     * Worker name
     */
    @TableField("workername")
    private String workerName;

    /**
     * Sex (男/女)
     */
    @TableField("sex")
    private String sex;

    /**
     * Date of birth
     */
    @JsonFormat(pattern = "yyyy-MM-dd", timezone = "GMT+8")
    @TableField("birthday")
    private String birthday;

    /**
     * Unit/Department name
     */
    @TableField("unitname")
    private String unitName;

    /**
     * Unit/Department code
     */
    @TableField("unitcode")
    private String unitCode;

    /**
     * Confer date (issuance)
     */
    @JsonFormat(pattern = "yyyy-MM-dd", timezone = "GMT+8")
    @TableField("conferdate")
    private String conferDate;

    /**
     * Valid start date
     */
    @JsonFormat(pattern = "yyyy-MM-dd", timezone = "GMT+8")
    @TableField("validstartdate")
    private String validStartDate;

    /**
     * Valid end date
     */
    @JsonFormat(pattern = "yyyy-MM-dd", timezone = "GMT+8")
    @TableField("validenddate")
    private String validEndDate;

    /**
     * Issuance unit
     */
    @TableField("conferunit")
    private String conferUnit;

    /**
     * Certificate status
     */
    @TableField("status")
    private String status;

    /**
     * Checker/Review person
     */
    @TableField("checkman")
    private String checkMan;

    /**
     * Review/Check advice
     */
    @TableField("checkadvise")
    private String checkAdvise;

    /**
     * Check date
     */
    @JsonFormat(pattern = "yyyy-MM-dd", timezone = "GMT+8")
    @TableField("checkdate")
    private String checkDate;

    /**
     * Print person name
     */
    @TableField("printman")
    private String printMan;

    /**
     * Print date
     */
    @JsonFormat(pattern = "yyyy-MM-dd", timezone = "GMT+8")
    @TableField("printdate")
    private String printDate;

    public Long getWorkerId() {
        return workerId;
    }

    public void setWorkerId(Long workerId) {
        this.workerId = workerId;
    }

    public String getCertificateType() {
        return certificateType;
    }

    public void setCertificateType(String certificateType) {
        this.certificateType = certificateType;
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

    public String getCertificateCode() {
        return certificateCode;
    }

    public void setCertificateCode(String certificateCode) {
        this.certificateCode = certificateCode;
    }

    public String getWorkerName() {
        return workerName;
    }

    public void setWorkerName(String workerName) {
        this.workerName = workerName;
    }

    public String getSex() {
        return sex;
    }

    public void setSex(String sex) {
        this.sex = sex;
    }

    public String getBirthday() {
        return birthday;
    }

    public void setBirthday(String birthday) {
        this.birthday = birthday;
    }

    public String getUnitName() {
        return unitName;
    }

    public void setUnitName(String unitName) {
        this.unitName = unitName;
    }

    public String getUnitCode() {
        return unitCode;
    }

    public void setUnitCode(String unitCode) {
        this.unitCode = unitCode;
    }

    public String getConferDate() {
        return conferDate;
    }

    public void setConferDate(String conferDate) {
        this.conferDate = conferDate;
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

    public String getConferUnit() {
        return conferUnit;
    }

    public void setConferUnit(String conferUnit) {
        this.conferUnit = conferUnit;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
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
}
