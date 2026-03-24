package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;

/**
 * TrainUnit Entity
 * 培训单位实体类
 *
 * Maps to legacy TrainUnitDAL.cs and TrainUnitMDL.cs
 * Table: TrainUnit
 *
 * Business Context:
 * - Training unit management system
 * - Training units for worker training programs
 * - Links to posts via PostSet field
 * - Tracks unit availability via UseStatus
 * - Supports social credit code (UnitCode) lookup
 */
@TableName("TrainUnit")
public class TrainUnit {

    /**
     * Primary key - Unit No
     * 单位编号 (主键)
     */
    @TableId(type = IdType.AUTO)
    private String unitNo;

    /**
     * Train Unit Name
     * 培训单位名称
     */
    private String trainUnitName;

    /**
     * Unit Code
     * 单位编码
     */
    private String unitCode;

    /**
     * Post Set
     * 关联岗位集合
     */
    private String postSet;

    /**
     * Use Status
     * 使用状态
     */
    private Integer useStatus;

    // Getters and Setters
    public String getUnitNo() {
        return unitNo;
    }

    public void setUnitNo(String unitNo) {
        this.unitNo = unitNo;
    }

    public String getTrainUnitName() {
        return trainUnitName;
    }

    public void setTrainUnitName(String trainUnitName) {
        this.trainUnitName = trainUnitName;
    }

    public String getUnitCode() {
        return unitCode;
    }

    public void setUnitCode(String unitCode) {
        this.unitCode = unitCode;
    }

    public String getPostSet() {
        return postSet;
    }

    public void setPostSet(String postSet) {
        this.postSet = postSet;
    }

    public Integer getUseStatus() {
        return useStatus;
    }

    public void setUseStatus(Integer useStatus) {
        this.useStatus = useStatus;
    }
}
