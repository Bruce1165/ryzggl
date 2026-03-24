package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;

/**
 * CheckObject Entity
 * 核查对象实体类
 *
 * Maps to legacy CheckObjectMDL.cs and CheckObjectDAL.cs
 * Table: CheckObject
 *
 * Business Context:
 * - Manages check/verification objects for inspections
 * - Tracks personnel certificates requiring verification
 * - Records application types and notice dates
 * - Supports check year management
 */
@TableName("CheckObject")
public class CheckObject {

    /**
     * Primary key - CheckID
     * 检查ID (主键)
     */
    @TableId
    private String checkId;

    /**
     * Check Year
     * 检查年度
     */
    private Integer checkYear;

    /**
     * Register No
     * 注册编号/证书编号
     */
    private String psnRegisterNo;

    /**
     * Post Type Name
     * 岗位类型名称
     */
    private String postTypeName;

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
     * Enterprise Name
     * 企业名称
     */
    private String entName;

    /**
     * Profession With Valid
     * 专业及有效期
     */
    private String professionWithValid;

    /**
     * Apply Type
     * 申报事项/申报类型
     */
    private String applyType;

    /**
     * Apply Time
     * 申报时间
     */
    private String applyTime;

    /**
     * Notice Date
     * 办结时间
     */
    private String noticeDate;

    // Getters and Setters
    public String getCheckId() {
        return checkId;
    }

    public void setCheckId(String checkId) {
        this.checkId = checkId;
    }

    public Integer getCheckYear() {
        return checkYear;
    }

    public void setCheckYear(Integer checkYear) {
        this.checkYear = checkYear;
    }

    public String getPsnRegisterNo() {
        return psnRegisterNo;
    }

    public void setPsnRegisterNo(String psnRegisterNo) {
        this.psnRegisterNo = psnRegisterNo;
    }

    public String getPostTypeName() {
        return postTypeName;
    }

    public void setPostTypeName(String postTypeName) {
        this.postTypeName = postTypeName;
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

    public String getEntName() {
        return entName;
    }

    public void setEntName(String entName) {
        this.entName = entName;
    }

    public String getProfessionWithValid() {
        return professionWithValid;
    }

    public void setProfessionWithValid(String professionWithValid) {
        this.professionWithValid = professionWithValid;
    }

    public String getApplyType() {
        return applyType;
    }

    public void setApplyType(String applyType) {
        this.applyType = applyType;
    }

    public String getApplyTime() {
        return applyTime;
    }

    public void setApplyTime(String applyTime) {
        this.applyTime = applyTime;
    }

    public String getNoticeDate() {
        return noticeDate;
    }

    public void setNoticeDate(String noticeDate) {
        this.noticeDate = noticeDate;
    }
}
