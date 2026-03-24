package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;
import com.fasterxml.jackson.annotation.JsonFormat;
import com.ryzggl.common.BaseEntity;

/**
 * ExamPlace entity - Exam venues
 */
@TableName("EXAMPLACE")
public class ExamPlace extends BaseEntity {

    @TableId(type = IdType.INPUT)
    private Long examPlaceId;

    @TableField("examplacename")
    private String examPlaceName;

    @TableField("examplaceaddress")
    private String examPlaceAddress;

    @TableField("linkman")
    private String linkMan;

    @TableField("phone")
    private String phone;

    @TableField("capacity")
    private Integer capacity;

    public Long getExamPlaceId() {
        return examPlaceId;
    }

    public void setExamPlaceId(Long examPlaceId) {
        this.examPlaceId = examPlaceId;
    }

    public String getExamPlaceName() {
        return examPlaceName;
    }

    public void setExamPlaceName(String examPlaceName) {
        this.examPlaceName = examPlaceName;
    }

    public String getExamPlaceAddress() {
        return examPlaceAddress;
    }

    public void setExamPlaceAddress(String examPlaceAddress) {
        this.examPlaceAddress = examPlaceAddress;
    }

    public String getLinkMan() {
        return linkMan;
    }

    public void setLinkMan(String linkMan) {
        this.linkMan = linkMan;
    }

    public String getPhone() {
        return phone;
    }

    public void setPhone(String phone) {
        this.phone = phone;
    }

    public Integer getCapacity() {
        return capacity;
    }

    public void setCapacity(Integer capacity) {
        this.capacity = capacity;
    }
}
