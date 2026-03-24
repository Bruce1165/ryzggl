package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;
import com.ryzggl.common.BaseEntity;

/**
 * Department entity - Organizational departments
 */
@TableName("Department")
public class Department extends BaseEntity {

    @TableId(type = IdType.INPUT)
    private String unitCode;

    @TableField("unitname")
    private String unitName;

    @TableField("orgid")
    private String orgId;

    @TableField("departlevel")
    private Integer departLevel;

    public String getUnitCode() {
        return unitCode;
    }

    public void setUnitCode(String unitCode) {
        this.unitCode = unitCode;
    }

    public String getUnitName() {
        return unitName;
    }

    public void setUnitName(String unitName) {
        this.unitName = unitName;
    }

    public String getOrgId() {
        return orgId;
    }

    public void setOrgId(String orgId) {
        this.orgId = orgId;
    }

    public Integer getDepartLevel() {
        return departLevel;
    }

    public void setDepartLevel(Integer departLevel) {
        this.departLevel = departLevel;
    }
}
