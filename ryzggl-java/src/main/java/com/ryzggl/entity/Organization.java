package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;
import com.fasterxml.jackson.annotation.JsonFormat;

/**
 * Organization Entity
 * 机构管理实体类
 *
 * Maps to legacy OrganizationOB.cs
 * Table: Organization
 *
 * Business Context:
 * - Hierarchical organization structure with parent-child relationships
 * - OrganCoding length: 4 chars for parent, 6 chars for child organizations
 * - Supports region-based organization management
 * - Used across multiple modules (worker, certificate, exam)
 */
@TableName("Organization")
public class Organization {

    /**
     * Primary key - Organization ID
     * 机构ID (主键)
     */
    @TableId(type = IdType.AUTO)
    private Long organId;

    /**
     * Order ID
     * 排序ID
     */
    private Integer orderId;

    /**
     * Organization Code
     * 机构编码 (4位为父级，6位为子级)
     */
    private String organCoding;

    /**
     * Organization Type
     * 机构类型
     */
    private String organType;

    /**
     * Organization Nature
     * 机构性质
     */
    private String organNature;

    /**
     * Organization Name
     * 机构名称
     */
    private String organName;

    /**
     * Organization Description
     * 机构描述
     */
    private String organDescription;

    /**
     * Business Properties
     * 业务属性
     */
    private String businessProperties;

    /**
     * Organization Phone
     * 机构电话
     */
    private String organTelphone;

    /**
     * Organization Address
     * 机构地址
     */
    private String organAddress;

    /**
     * Organization Code
     * 机构编码
     */
    private String organCode;

    /**
     * Region ID
     * 地区ID
     */
    private String regionId;

    /**
     * Visibility flag
     * 是否可见
     */
    private Boolean isVisible;

    /**
     * Creator ID
     * 创建人
     */
    private Long createPersonId;

    /**
     * Creation Time
     * 创建时间
     */
    @JsonFormat(pattern = "yyyy-MM-dd HH:mm:ss")
    private String createTime;

    /**
     * Modifier ID
     * 修改人
     */
    private Long modifyPersonId;

    /**
     * Modification Time
     * 修改时间
     */
    @JsonFormat(pattern = "yyyy-MM-dd HH:mm:ss")
    private String modifyTime;

    // Getters and Setters
    public Long getOrganId() {
        return organId;
    }

    public void setOrganId(Long organId) {
        this.organId = organId;
    }

    public Integer getOrderId() {
        return orderId;
    }

    public void setOrderId(Integer orderId) {
        this.orderId = orderId;
    }

    public String getOrganCoding() {
        return organCoding;
    }

    public void setOrganCoding(String organCoding) {
        this.organCoding = organCoding;
    }

    public String getOrganType() {
        return organType;
    }

    public void setOrganType(String organType) {
        this.organType = organType;
    }

    public String getOrganNature() {
        return organNature;
    }

    public void setOrganNature(String organNature) {
        this.organNature = organNature;
    }

    public String getOrganName() {
        return organName;
    }

    public void setOrganName(String organName) {
        this.organName = organName;
    }

    public String getOrganDescription() {
        return organDescription;
    }

    public void setOrganDescription(String organDescription) {
        this.organDescription = organDescription;
    }

    public String getBusinessProperties() {
        return businessProperties;
    }

    public void setBusinessProperties(String businessProperties) {
        this.businessProperties = businessProperties;
    }

    public String getOrganTelphone() {
        return organTelphone;
    }

    public void setOrganTelphone(String organTelphone) {
        this.organTelphone = organTelphone;
    }

    public String getOrganAddress() {
        return organAddress;
    }

    public void setOrganAddress(String organAddress) {
        this.organAddress = organAddress;
    }

    public String getOrganCode() {
        return organCode;
    }

    public void setOrganCode(String organCode) {
        this.organCode = organCode;
    }

    public String getRegionId() {
        return regionId;
    }

    public void setRegionId(String regionId) {
        this.regionId = regionId;
    }

    public Boolean getIsVisible() {
        return isVisible;
    }

    public void setIsVisible(Boolean isVisible) {
        this.isVisible = isVisible;
    }

    public Long getCreatePersonId() {
        return createPersonId;
    }

    public void setCreatePersonId(Long createPersonId) {
        this.createPersonId = createPersonId;
    }

    public String getCreateTime() {
        return createTime;
    }

    public void setCreateTime(String createTime) {
        this.createTime = createTime;
    }

    public Long getModifyPersonId() {
        return modifyPersonId;
    }

    public void setModifyPersonId(Long modifyPersonId) {
        this.modifyPersonId = modifyPersonId;
    }

    public String getModifyTime() {
        return modifyTime;
    }

    public void setModifyTime(String modifyTime) {
        this.modifyTime = modifyTime;
    }
}
