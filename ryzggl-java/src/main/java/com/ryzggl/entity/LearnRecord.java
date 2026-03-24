package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;
import com.fasterxml.jackson.annotation.JsonFormat;

/**
 * LearnRecord Entity
 * 学习记录实体类
 *
 * Maps to legacy LearnRecordDAL.cs and LearnRecordOB.cs
 * Table: earnRecord
 *
 * Business Context:
 * - Tracks worker learning records and class hours
 * - Links to worker certificates and posts
 * - Records contact information (linkTel)
 * - Used for training management workflows
 */
@TableName("earnRecord")
public class LearnRecord {

    /**
     * Primary key - Record ID
     * 记录ID (主键)
     */
    @TableId(type = IdType.AUTO)
    private Long recordId;

    /**
     * Record Number
     * 记录编号
     */
    private String recordNo;

    /**
     * Post Name
     * 岗位名称
     */
    private String postName;

    /**
     * Worker Name
     * 人员姓名
     */
    private String workerName;

    /**
     * Link Telephone
     * 联系电话
     */
    private String linkTel;

    /**
     * Certificate Code
     * 证书编码
     */
    private String certificateCode;

    /**
     * Worker Certificate Code
     * 人员证书编码
     */
    private String workerCertificateCode;

    /**
     * Class Hour
     * 课时
     */
    private String classHour;

    // Getters and Setters
    public Long getRecordId() {
        return recordId;
    }

    public void setRecordId(Long recordId) {
        this.recordId = recordId;
    }

    public String getRecordNo() {
        return recordNo;
    }

    public void setRecordNo(String recordNo) {
        this.recordNo = recordNo;
    }

    public String getPostName() {
        return postName;
    }

    public void setPostName(String postName) {
        this.postName = postName;
    }

    public String getWorkerName() {
        return workerName;
    }

    public void setWorkerName(String workerName) {
        this.workerName = workerName;
    }

    public String getLinkTel() {
        return linkTel;
    }

    public void setLinkTel(String linkTel) {
        this.linkTel = linkTel;
    }

    public String getCertificateCode() {
        return certificateCode;
    }

    public void setCertificateCode(String certificateCode) {
        this.certificateCode = certificateCode;
    }

    public String getWorkerCertificateCode() {
        return workerCertificateCode;
    }

    public void setWorkerCertificateCode(String workerCertificateCode) {
        this.workerCertificateCode = workerCertificateCode;
    }

    public String getClassHour() {
        return classHour;
    }

    public void setClassHour(String classHour) {
        this.classHour = classHour;
    }
}
