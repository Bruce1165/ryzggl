package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;
import com.fasterxml.jackson.annotation.JsonFormat;

/**
 * StudyPlan Entity
 * 学习计划实体类
 *
 * Maps to legacy StudyPlanDAL.cs and StudyPlanMDL.cs
 * Table: StudyPlan
 *
 * Business Context:
 * - Training study plans for workers
 * - Composite key: WorkerCertificateCode + PackageID
 * - Supports individual and system-assigned plans
 * - Tracks test status and completion dates
 */
@TableName("StudyPlan")
public class StudyPlan {

    /**
     * Worker Certificate Code
     * 证件号码 (复合主键之一)
     */
    private String workerCertificateCode;

    /**
     * Package ID
     * 培训包ID (复合主键之一)
     */
    @TableId(type = IdType.NONE)
    private Long packageId;

    /**
     * Worker Name
     * 姓名
     */
    private String workerName;

    /**
     * Create Time
     * 创建时间
     */
    @JsonFormat(pattern = "yyyy-MM-dd HH:mm:ss")
    private String createTime;

    /**
     * Creator
     * 创建人
     */
    private String creater;

    /**
     * Add Type
     * 学习计划加入方式
     * Options: 个人添加, 系统指派
     */
    private String addType;

    /**
     * Finish Date
     * 完成时间
     */
    @JsonFormat(pattern = "yyyy-MM-dd HH:mm:ss")
    private String finishDate;

    /**
     * Test Status
     * 测试状态
     * Options: 未达标, 已达标
     */
    private String testStatus;

    // Getters and Setters
    public String getWorkerCertificateCode() {
        return workerCertificateCode;
    }

    public void setWorkerCertificateCode(String workerCertificateCode) {
        this.workerCertificateCode = workerCertificateCode;
    }

    public Long getPackageId() {
        return packageId;
    }

    public void setPackageId(Long packageId) {
        this.packageId = packageId;
    }

    public String getWorkerName() {
        return workerName;
    }

    public void setWorkerName(String workerName) {
        this.workerName = workerName;
    }

    public String getCreateTime() {
        return createTime;
    }

    public void setCreateTime(String createTime) {
        this.createTime = createTime;
    }

    public String getCreater() {
        return creater;
    }

    public void setCreater(String creater) {
        this.creater = creater;
    }

    public String getAddType() {
        return addType;
    }

    public void setAddType(String addType) {
        this.addType = addType;
    }

    public String getFinishDate() {
        return finishDate;
    }

    public void setFinishDate(String finishDate) {
        this.finishDate = finishDate;
    }

    public String getTestStatus() {
        return testStatus;
    }

    public void setTestStatus(String testStatus) {
        this.testStatus = testStatus;
    }
}
