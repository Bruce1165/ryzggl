package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;

/**
 * BlackList Entity
 * 黑名单实体类
 *
 * Maps to legacy BlackListOB.cs and BlackListDAL.cs
 * Table: BlackList
 *
 * Business Context:
 * - Manages personnel blacklist entries
 * - Tracks blacklisted workers, certificates, and organizations
 * - Supports different blacklist types
 * - Records creation and modification history
 */
@TableName("BlackList")
public class BlackList {

    /**
     * Primary key - BlackListID
     * 黑名单ID (主键)
     */
    @TableId(type = IdType.AUTO)
    private Long blackListId;

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
     * Worker Name
     * 人员姓名
     */
    private String workerName;

    /**
     * Certificate Code
     * 证书编号
     */
    private String certificateCode;

    /**
     * Unit Name
     * 单位名称
     */
    private String unitName;

    /**
     * Unit Code
     * 单位编码
     */
    private String unitCode;

    /**
     * Train Unit Name
     * 培训单位名称
     */
    private String trainUnitName;

    /**
     * Black Type
     * 黑名单类型
     */
    private String blackType;

    /**
     * Start Time
     * 开始时间
     */
    private String startTime;

    /**
     * Black Status
     * 黑名单状态
     */
    private String blackStatus;

    /**
     * Remark
     * 备注
     */
    private String remark;

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
     * Modify Person
     * 修改人
     */
    private String modifyPerson;

    /**
     * Modify Time
     * 修改时间
     */
    private String modifyTime;

    // Getters and Setters
    public Long getBlackListId() {
        return blackListId;
    }

    public void setBlackListId(Long blackListId) {
        this.blackListId = blackListId;
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

    public String getWorkerName() {
        return workerName;
    }

    public void setWorkerName(String workerName) {
        this.workerName = workerName;
    }

    public String getCertificateCode() {
        return certificateCode;
    }

    public void setCertificateCode(String certificateCode) {
        this.certificateCode = certificateCode;
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

    public String getTrainUnitName() {
        return trainUnitName;
    }

    public void setTrainUnitName(String trainUnitName) {
        this.trainUnitName = trainUnitName;
    }

    public String getBlackType() {
        return blackType;
    }

    public void setBlackType(String blackType) {
        this.blackType = blackType;
    }

    public String getStartTime() {
        return startTime;
    }

    public void setStartTime(String startTime) {
        this.startTime = startTime;
    }

    public String getBlackStatus() {
        return blackStatus;
    }

    public void setBlackStatus(String blackStatus) {
        this.blackStatus = blackStatus;
    }

    public String getRemark() {
        return remark;
    }

    public void setRemark(String remark) {
        this.remark = remark;
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

    public String getModifyPerson() {
        return modifyPerson;
    }

    public void setModifyPerson(String modifyPerson) {
        this.modifyPerson = modifyPerson;
    }

    public String getModifyTime() {
        return modifyTime;
    }

    public void setModifyTime(String modifyTime) {
        this.modifyTime = modifyTime;
    }
}
