package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;
import com.fasterxml.jackson.annotation.JsonFormat;

/**
 * OperateLog Entity
 * 操作日志实体类
 *
 * Maps to legacy OperateLogDAL.cs and OperateLogOB.cs
 * Table: OperateLog
 *
 * Business Context:
 * - System operation logging
 * - Tracks user operations (create, update, delete)
 * - Records person ID and role ID
 * - Supports statistics by operation type and date range
 */
@TableName("OperateLog")
public class OperateLog {

    /**
     * Primary key - Log ID
     * 日志ID (主键)
     */
    @TableId(type = IdType.AUTO)
    private Long logId;

    /**
     * Log Time
     * 操作时间
     */
    @JsonFormat(pattern = "yyyy-MM-dd HH:mm:ss")
    private String logTime;

    /**
     * Person Name
     * 操作人
     */
    private String personName;

    /**
     * Person ID
     * 人员ID
     */
    private String personId;

    /**
     * Operate Name
     * 操作名称
     */
    private String operateName;

    /**
     * Log Detail
     * 操作详情
     */
    private String logDetail;

    // Getters and Setters
    public Long getLogId() {
        return logId;
    }

    public void setLogId(Long logId) {
        this.logId = logId;
    }

    public String getLogTime() {
        return logTime;
    }

    public void setLogTime(String logTime) {
        this.logTime = logTime;
    }

    public String getPersonName() {
        return personName;
    }

    public void setPersonName(String personName) {
        this.personName = personName;
    }

    public String getPersonId() {
        return personId;
    }

    public void setPersonId(String personId) {
        this.personId = personId;
    }

    public String getOperateName() {
        return operateName;
    }

    public void setOperateName(String operateName) {
        this.operateName = operateName;
    }

    public String getLogDetail() {
        return logDetail;
    }

    public void setLogDetail(String logDetail) {
        this.logDetail = logDetail;
    }
}
