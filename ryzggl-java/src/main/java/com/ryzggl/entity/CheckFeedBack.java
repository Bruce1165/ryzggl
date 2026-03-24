package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;

/**
 * CheckFeedBack Entity
 * 核查反馈实体类
 *
 * Maps to legacy CheckFeedBackMDL.cs and CheckFeedBackDAL.cs
 * Table: CheckFeedBack
 *
 * Business Context:
 * - Manages verification feedback workflow
 * - Tracks worker information and certificate details
 * - Supports social security and public fund verification
 * - Multi-level approval: initial review, city review, final decision
 * - Status codes: 0=未发布, 1=待反馈, 2=已驳回, 3=待审查, 4=待复审, 6=待决定, 7=已决定
 */
@TableName("CheckFeedBack")
public class CheckFeedBack {

    /**
     * Primary key - DataID
     * 填报单ID (主键)
     */
    @TableId
    private String dataId;

    /**
     * Patch Code
     * 批次号
     */
    private Integer patchCode;

    /**
     * Check Type
     * 监管类型
     */
    private String checkType;

    /**
     * Create Time
     * 创建时间
     */
    private String createTime;

    /**
     * Create Person
     * 创建人
     */
    private String cjr;

    /**
     * Publish Time
     * 发布时间
     */
    private String publishiTime;

    /**
     * Last Report Time
     * 上报截止时间
     */
    private String lastReportTime;

    /**
     * Data Status
     * 状态
     */
    private String dataStatus;

    /**
     * Data Status Code
     * 状态编码：未发布 = 0; 待反馈 = 1; 已驳回 = 2; 待审查 = 3; 待复审 = 4; 待决定 = 6; 已决定 = 7;
     */
    private Integer dataStatusCode;

    /**
     * Worker Name
     * 姓名
     */
    private String workerName;

    /**
     * Worker Certificate Code
     * 证件号码
     */
    private String workerCertificateCode;

    /**
     * Certificate Code
     * 证书编号
     */
    private String certificateCode;

    /**
     * Phone
     * 电话
     */
    private String phone;

    /**
     * Post Type Name
     * 注册类别
     */
    private String postTypeName;

    /**
     * Unit
     * 注册单位
     */
    private String unit;

    /**
     * Unit Code
     * 单位社会统一代码
     */
    private String unitCode;

    /**
     * Country
     * 所属区
     */
    private String country;

    /**
     * Social Security Case
     * 社保情况
     */
    private String sheBaoCase;

    /**
     * Social Security Unit
     * 社保单位
     */
    private String shebaoUnit;

    /**
     * Public Fund Case
     * 公积金情况
     */
    private String gongjijinCase;

    /**
     * Project Case
     * 在施项目
     */
    private String projectCase;

    /**
     * Source Time
     * 数据来源时间
     */
    private String sourceTime;

    /**
     * Case Description
     * 个人情况说明
     */
    private String caseDesc;

    /**
     * Worker Report Time
     * 个人上报时间
     */
    private String workerRerpotTime;

    /**
     * Accept Country
     * 初审机构
     */
    private String acceptCountry;

    /**
     * Accept Time
     * 初审受理时间
     */
    private String acceptTime;

    /**
     * Accept Man
     * 初审人
     */
    private String acceptMan;

    /**
     * Accept Result
     * 初审意见
     */
    private String acceptResult;

    /**
     * Country Report Time
     * 初审上报时间
     */
    private String countryReportTime;

    /**
     * Country Report Code
     * 初审少报批次号
     */
    private String countryReportCode;

    /**
     * Check Time
     * 市建委审核时间
     */
    private String checkTime;

    /**
     * Check Man
     * 市建委审核人
     */
    private String checkMan;

    /**
     * Check Result
     * 市建委审核意见
     */
    private String checkResult;

    /**
     * Confirm Time
     * 市建委决定时间
     */
    private String confirmTime;

    /**
     * Confirm Man
     * 市建委决定人
     */
    private String confirmMan;

    /**
     * Confirm Result
     * 市建委决定意见
     */
    private String confirmResult;

    /**
     * Social Security Check Time
     * 社保比对时间
     */
    private String sheBaoCheckTime;

    /**
     * Social Security Return Time
     * 社保比对返回结果时间
     */
    private String sheBaoRtnTime;

    /**
     * SN
     * 导入排序号
     */
    private Integer sn;

    /**
     * Back Reason
     * 驳回原因
     */
    private String backReason;

    /**
     * Back Unit
     * 驳回机构
     */
    private String backUnit;

    /**
     * Pass Type
     * 合格类型：已注销完成整改；社保一致完成整改；特殊六类总分公司；特殊六类已退休；特殊六类事业单位改制；特殊六类其他
     */
    private String passType;

    // Getters and Setters
    public String getDataId() {
        return dataId;
    }

    public void setDataId(String dataId) {
        this.dataId = dataId;
    }

    public Integer getPatchCode() {
        return patchCode;
    }

    public void setPatchCode(Integer patchCode) {
        this.patchCode = patchCode;
    }

    public String getCheckType() {
        return checkType;
    }

    public void setCheckType(String checkType) {
        this.checkType = checkType;
    }

    public String getCreateTime() {
        return createTime;
    }

    public void setCreateTime(String createTime) {
        this.createTime = createTime;
    }

    public String getCjr() {
        return cjr;
    }

    public void setCjr(String cjr) {
        this.cjr = cjr;
    }

    public String getPublishiTime() {
        return publishiTime;
    }

    public void setPublishiTime(String publishiTime) {
        this.publishiTime = publishiTime;
    }

    public String getLastReportTime() {
        return lastReportTime;
    }

    public void setLastReportTime(String lastReportTime) {
        this.lastReportTime = lastReportTime;
    }

    public String getDataStatus() {
        return dataStatus;
    }

    public void setDataStatus(String dataStatus) {
        this.dataStatus = dataStatus;
    }

    public Integer getDataStatusCode() {
        return dataStatusCode;
    }

    public void setDataStatusCode(Integer dataStatusCode) {
        this.dataStatusCode = dataStatusCode;
    }

    public String getWorkerName() {
        return workerName;
    }

    public void setWorkerName(String workerName) {
        this.workerName = workerName;
    }

    public String getWorkerCertificateCode() {
        return workerCertificateCode;
    }

    public void setWorkerCertificateCode(String workerCertificateCode) {
        this.workerCertificateCode = workerCertificateCode;
    }

    public String getCertificateCode() {
        return certificateCode;
    }

    public void setCertificateCode(String certificateCode) {
        this.certificateCode = certificateCode;
    }

    public String getPhone() {
        return phone;
    }

    public void setPhone(String phone) {
        this.phone = phone;
    }

    public String getPostTypeName() {
        return postTypeName;
    }

    public void setPostTypeName(String postTypeName) {
        this.postTypeName = postTypeName;
    }

    public String getUnit() {
        return unit;
    }

    public void setUnit(String unit) {
        this.unit = unit;
    }

    public String getUnitCode() {
        return unitCode;
    }

    public void setUnitCode(String unitCode) {
        this.unitCode = unitCode;
    }

    public String getCountry() {
        return country;
    }

    public void setCountry(String country) {
        this.country = country;
    }

    public String getSheBaoCase() {
        return sheBaoCase;
    }

    public void setSheBaoCase(String sheBaoCase) {
        this.sheBaoCase = sheBaoCase;
    }

    public String getShebaoUnit() {
        return shebaoUnit;
    }

    public void setShebaoUnit(String shebaoUnit) {
        this.shebaoUnit = shebaoUnit;
    }

    public String getGongjijinCase() {
        return gongjijinCase;
    }

    public void setGongjijinCase(String gongjijinCase) {
        this.gongjijinCase = gongjijinCase;
    }

    public String getProjectCase() {
        return projectCase;
    }

    public void setProjectCase(String projectCase) {
        this.projectCase = projectCase;
    }

    public String getSourceTime() {
        return sourceTime;
    }

    public void setSourceTime(String sourceTime) {
        this.sourceTime = sourceTime;
    }

    public String getCaseDesc() {
        return caseDesc;
    }

    public void setCaseDesc(String caseDesc) {
        this.caseDesc = caseDesc;
    }

    public String getWorkerRerpotTime() {
        return workerRerpotTime;
    }

    public void setWorkerRerpotTime(String workerRerpotTime) {
        this.workerRerpotTime = workerRerpotTime;
    }

    public String getAcceptCountry() {
        return acceptCountry;
    }

    public void setAcceptCountry(String acceptCountry) {
        this.acceptCountry = acceptCountry;
    }

    public String getAcceptTime() {
        return acceptTime;
    }

    public void setAcceptTime(String acceptTime) {
        this.acceptTime = acceptTime;
    }

    public String getAcceptMan() {
        return acceptMan;
    }

    public void setAcceptMan(String acceptMan) {
        this.acceptMan = acceptMan;
    }

    public String getAcceptResult() {
        return acceptResult;
    }

    public void setAcceptResult(String acceptResult) {
        this.acceptResult = acceptResult;
    }

    public String getCountryReportTime() {
        return countryReportTime;
    }

    public void setCountryReportTime(String countryReportTime) {
        this.countryReportTime = countryReportTime;
    }

    public String getCountryReportCode() {
        return countryReportCode;
    }

    public void setCountryReportCode(String countryReportCode) {
        this.countryReportCode = countryReportCode;
    }

    public String getCheckTime() {
        return checkTime;
    }

    public void setCheckTime(String checkTime) {
        this.checkTime = checkTime;
    }

    public String getCheckMan() {
        return checkMan;
    }

    public void setCheckMan(String checkMan) {
        this.checkMan = checkMan;
    }

    public String getCheckResult() {
        return checkResult;
    }

    public void setCheckResult(String checkResult) {
        this.checkResult = checkResult;
    }

    public String getConfirmTime() {
        return confirmTime;
    }

    public void setConfirmTime(String confirmTime) {
        this.confirmTime = confirmTime;
    }

    public String getConfirmMan() {
        return confirmMan;
    }

    public void setConfirmMan(String confirmMan) {
        this.confirmMan = confirmMan;
    }

    public String getConfirmResult() {
        return confirmResult;
    }

    public void setConfirmResult(String confirmResult) {
        this.confirmResult = confirmResult;
    }

    public String getSheBaoCheckTime() {
        return sheBaoCheckTime;
    }

    public void setSheBaoCheckTime(String sheBaoCheckTime) {
        this.sheBaoCheckTime = sheBaoCheckTime;
    }

    public String getSheBaoRtnTime() {
        return sheBaoRtnTime;
    }

    public void setSheBaoRtnTime(String sheBaoRtnTime) {
        this.sheBaoRtnTime = sheBaoRtnTime;
    }

    public Integer getSn() {
        return sn;
    }

    public void setSn(Integer sn) {
        this.sn = sn;
    }

    public String getBackReason() {
        return backReason;
    }

    public void setBackReason(String backReason) {
        this.backReason = backReason;
    }

    public String getBackUnit() {
        return backUnit;
    }

    public void setBackUnit(String backUnit) {
        this.backUnit = backUnit;
    }

    public String getPassType() {
        return passType;
    }

    public void setPassType(String passType) {
        this.passType = passType;
    }
}
