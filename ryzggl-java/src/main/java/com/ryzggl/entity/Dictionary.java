package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;

/**
 * Dictionary Entity
 * 数据字典实体类
 *
 * Maps to legacy DictionaryDAL.cs and DictionaryMDL.cs
 * Table: Dictionary
 *
 * Business Context:
 * - System code table for dropdowns and reference data
 * - Organizes codes by TypeID (type) and OrderID (order within type)
 * - Used throughout the application for standardized values
 * - Example: Certificate types, Exam statuses, Worker categories
 */
@TableName("Dictionary")
public class Dictionary {

    /**
     * Primary key - Dictionary ID
     * 字典表主键
     */
    @TableId(type = IdType.AUTO)
    private String dicId;

    /**
     * Type ID
     * 类型ID (字典类型分类)
     */
    private Integer typeId;

    /**
     * Type Name
     * 类型名称 (字典类型的名称)
     */
    private String typeName;

    /**
     * Order ID
     * 排序值 (同一类型内的排序)
     */
    private Integer orderId;

    /**
     * Dictionary Name
     * 名称 (字典项名称)
     */
    private String dicName;

    /**
     * Dictionary Description
     * 描述 (字典项描述)
     */
    private String dicDesc;

    /**
     * Category
     * 分类 (可选分类字段)
     */
    private Integer category;

    // Getters and Setters
    public String getDicId() {
        return dicId;
    }

    public void setDicId(String dicId) {
        this.dicId = dicId;
    }

    public Integer getTypeId() {
        return typeId;
    }

    public void setTypeId(Integer typeId) {
        this.typeId = typeId;
    }

    public String getTypeName() {
        return typeName;
    }

    public void setTypeName(String typeName) {
        this.typeName = typeName;
    }

    public Integer getOrderId() {
        return orderId;
    }

    public void setOrderId(Integer orderId) {
        this.orderId = orderId;
    }

    public String getDicName() {
        return dicName;
    }

    public void setDicName(String dicName) {
        this.dicName = dicName;
    }

    public String getDicDesc() {
        return dicDesc;
    }

    public void setDicDesc(String dicDesc) {
        this.dicDesc = dicDesc;
    }

    public Integer getCategory() {
        return category;
    }

    public void setCategory(Integer category) {
        this.category = category;
    }
}
