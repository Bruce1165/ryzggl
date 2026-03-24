package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;
import com.fasterxml.jackson.annotation.JsonFormat;

/**
 * ExamPlace Entity - Exam Place/Venue
 * 考点实体类
 *
 * Maps to legacy ExamPlaceOB.cs
 * Table: ExamPlace
 *
 * Business Context:
 * - ExamPlace represents exam venues/locations for conducting examinations
 * - Each venue has capacity limits (rooms, person capacity)
 * - Includes contact information for coordination
 * - Status can be used to enable/disable venues
 */
@TableName("ExamPlace")
public class ExamPlace {

    /**
     * Primary key - Exam Place ID
     * 考点ID (主键)
     */
    @TableId(type = IdType.AUTO)
    private Long examPlaceId;

    /**
     * Exam Place Name
     * 考点名称
     */
    private String examPlaceName;

    /**
     * Exam Place Address
     * 考点地址
     */
    private String examPlaceAddress;

    /**
     * Contact Person
     * 联系人
     */
    private String linkMan;

    /**
     * Phone
     * 联系电话
     */
    private String phone;

    /**
     * Number of Rooms
     * 房间数量
     */
    private Integer roomNum;

    /**
     * Exam Person Number / Capacity
     * 考试人数（容量）
     */
    private Integer examPersonNum;

    /**
     * Status (ACTIVE, INACTIVE, DELETED)
     * 状态
     */
    private String status;

    // Getters and Setters
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

    public Integer getRoomNum() {
        return roomNum;
    }

    public void setRoomNum(Integer roomNum) {
        this.roomNum = roomNum;
    }

    public Integer getExamPersonNum() {
        return examPersonNum;
    }

    public void setExamPersonNum(Integer examPersonNum) {
        this.examPersonNum = examPersonNum;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }
}
