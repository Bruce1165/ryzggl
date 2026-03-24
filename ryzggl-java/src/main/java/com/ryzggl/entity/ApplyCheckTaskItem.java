package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;
import lombok.Data;
import lombok.EqualsAndHashCode;

import java.util.Date;

/**
 * ApplyCheckTaskItem Entity - Application Check Task Item (抽查任务项)
 * Represents a single application item within a check task
 */
@Data
@EqualsAndHashCode(callSuper = false)
@TableName("ApplyCheckTaskItem")
public class ApplyCheckTaskItem {

    /**
     * Task Item ID - Primary Key
     */
    @TableId(type = IdType.AUTO)
    private Long TaskItemID;

    /**
     * Task ID - Foreign key to ApplyCheckTask
     */
    private Long TaskID;

    /**
     * Business Type ID - Business type code
     * 1=二建, 2=二造, 3=安管人员, 4=特种作业
     */
    private Integer BusTypeID;

    /**
     * Apply Type - Type of application
     * e.g., "初始注册", "重新注册", "增项注册", "延期注册", "证书进京", "证书续期", "考试报名"
     */
    private String ApplyType;

    /**
     * Apply Table Name - Source table name
     * e.g., "Apply", "zjs_Apply", "EXAMSIGNUP", "CERTIFICATECONTINUE", "CERTIFICATE_ENTER"
     */
    private String ApplyTableName;

    /**
     * Data ID - Primary key of the application record
     */
    private String DataID;

    /**
     * Worker Name - Name of the worker/applicant
     */
    private String WorkerName;

    /**
     * ID Card - Worker's ID card number
     */
    private String IDCard;

    /**
     * ID Card Type - Type of ID card
     * e.g., "身份证"
     */
    private String IDCardType;

    /**
     * Certificate Code - Certificate registration number
     */
    private String CertificateCode;

    /**
     * Apply Finish Time - Application completion/approval time
     */
    private Date ApplyFinishTime;

    /**
     * Check Man - Approver/Checker name
     */
    private String CheckMan;

    /**
     * Check Time - Approval timestamp
     */
    private Date CheckTime;

    /**
     * Check Result - Approval result
     * e.g., "通过", "不通过"
     */
    private String CheckResult;

    /**
     * Check Result Description - Approval comments/notes
     */
    private String CheckResultDesc;

    /**
     * Re-check Man - Re-approver name (for double-check workflow)
     */
    private String ReCheckMan;

    /**
     * Re-check Time - Re-approval timestamp
     */
    private Date ReCheckTime;
}
