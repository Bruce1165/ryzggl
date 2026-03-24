package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;

/**
 * CertificateAddItem Entity
 * 证书增项实体类
 *
 * Maps to legacy CertificateAddItemOB.cs and CertificateAddItemDAL.cs
 * Table: CertificateAddItem
 *
 * Business Context:
 * - Manages certificate add-on items (post types attached to certificates)
 * - Tracks certificate ID, post type, and post ID
 * - Supports case status management
 * - Batch creation from exam results
 */
@TableName("CertificateAddItem")
public class CertificateAddItem {

    /**
     * Primary key - CertificateAddItemID
     * 证书增项ID (主键)
     */
    @TableId(type = IdType.AUTO)
    private Long certificateAddItemId;

    /**
     * Certificate ID
     * 证书ID
     */
    private Long certificateId;

    /**
     * Post Type ID
     * 岗位类型ID
     */
    private Integer postTypeId;

    /**
     * Post ID
     * 岗位ID
     */
    private Integer postId;

    /**
     * Create Person
     * 创建人
     */
    private String createPerson;

    /**
     * Create Time
     * 创建时间
     */
    private String createTime;

    /**
     * Case Status
     * 案件状态
     */
    private String caseStatus;

    // Getters and Setters
    public Long getCertificateAddItemId() {
        return certificateAddItemId;
    }

    public void setCertificateAddItemId(Long certificateAddItemId) {
        this.certificateAddItemId = certificateAddItemId;
    }

    public Long getCertificateId() {
        return certificateId;
    }

    public void setCertificateId(Long certificateId) {
        this.certificateId = certificateId;
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

    public String getCreatePerson() {
        return createPerson;
    }

    public void setCreatePerson(String createPerson) {
        this.createPerson = createPerson;
    }

    public String getCreateTime() {
        return createTime;
    }

    public void setCreateTime(String createTime) {
        this.createTime = createTime;
    }

    public String getCaseStatus() {
        return caseStatus;
    }

    public void setCaseStatus(String caseStatus) {
        this.caseStatus = caseStatus;
    }
}
