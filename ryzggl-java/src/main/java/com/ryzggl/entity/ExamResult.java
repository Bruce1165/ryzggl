package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;
import lombok.Data;
import lombok.EqualsAndHashCode;

import java.util.Date;

/**
 * ExamResult Entity - Exam Result (考试成绩)
 * Represents exam results for a student
 */
@Data
@EqualsAndHashCode(callSuper = false)
@TableName("ExamResult")
public class ExamResult {

    /**
     * Exam Result ID - Primary Key
     */
    @TableId(type = IdType.AUTO)
    private Long ExamResultID;

    /**
     * Exam Room Allot ID - Exam room allocation ID
     */
    private Long ExamRoomAllotID;

    /**
     * Exam Plan ID - Foreign key to ExamPlan
     */
    private Long ExamPlanID;

    /**
     * Worker ID - Foreign key to Worker
     */
    private Long WorkerID;

    /**
     * Exam Card ID - Exam ticket number
     */
    private String ExamCardID;

    /**
     * Exam Result - Overall result
     * e.g., "合格", "不合格"
     */
    private String ExamResult;

    /**
     * Status - Result status
     * e.g., "未发布", "已发布"
     */
    private String Status;

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
     * Exam Sign Up ID - Foreign key to ExamSignUp
     */
    private Long ExamSignUp_ID;
}
