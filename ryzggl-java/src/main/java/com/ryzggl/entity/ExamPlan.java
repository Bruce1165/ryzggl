package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;
import lombok.Data;
import lombok.EqualsAndHashCode;

import java.math.BigDecimal;
import java.util.Date;

/**
 * ExamPlan Entity - Exam Plan (考试计划)
 * Represents an exam schedule/plan
 */
@Data
@EqualsAndHashCode(callSuper = false)
@TableName("ExamPlan")
public class ExamPlan {

    /**
     * Exam Plan ID - Primary Key
     */
    @TableId(type = IdType.AUTO)
    private Long ExamPlanID;

    /**
     * Post Type ID - Position type
     * 1=安管人员, 2=特种作业
     */
    private Integer PostTypeID;

    /**
     * Post Type Name - Position type name
     */
    private String PostTypeName;

    /**
     * Post ID - Specific position/subject
     */
    private Integer PostID;

    /**
     * Post Name - Position name
     */
    private String PostName;

    /**
     * Sign Up Start Date - Registration start date
     */
    private Date SignUpStartDate;

    /**
     * Sign Up End Date - Registration end date
     */
    private Date SignUpEndDate;

    /**
     * Exam Card Send Start Date - Exam card issue start date
     */
    private Date ExamCardSendStartDate;

    /**
     * Exam Card Send End Date - Exam card issue end date
     */
    private Date ExamCardSendEndDate;

    /**
     * Exam Start Date - Exam start date
     */
    private Date ExamStartDate;

    /**
     * Exam End Date - Exam end date
     */
    private Date ExamEndDate;

    /**
     * Sign Up Place - Registration location
     */
    private String SignUpPlace;

    /**
     * Remark - Notes/remarks
     */
    private String Remark;

    /**
     * Status - Plan status
     * e.g., "草稿", "已发布", "已结束"
     */
    private String Status;

    /**
     * Exam Plan Name - Plan name/title
     */
    private String ExamPlanName;

    /**
     * Create Person ID - Creator user ID
     */
    private Long CreatePersonID;

    /**
     * Create Time - Creation timestamp
     */
    private Date CreateTime;

    /**
     * Modify Person ID - Modifier user ID
     */
    private Long ModifyPersonID;

    /**
     * Modify Time - Modification timestamp
     */
    private Date ModifyTime;

    /**
     * Latest Check Date - Latest check date
     */
    private Date LatestCheckDate;

    /**
     * Latest Pay Date - Latest payment date
     */
    private Date LatestPayDate;

    /**
     * Exam Fee - Examination fee
     */
    private BigDecimal ExamFee;

    /**
     * If Publish - Publish indicator
     * e.g., "是", "否"
     */
    private String IfPublish;

    /**
     * Plan Skill Level - Skill level requirement
     */
    private String PlanSkillLevel;

    /**
     * Start Check Date - Check start date
     */
    private Date StartCheckDate;

    /**
     * Person Limit - Maximum number of participants
     */
    private Integer PersonLimit;

    /**
     * Exam Way - Exam method
     * e.g., "机考", "纸考"
     */
    private String ExamWay;
}
