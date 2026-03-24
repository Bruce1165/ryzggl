package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;
import com.fasterxml.jackson.annotation.JsonFormat;
import com.ryzggl.common.BaseEntity;

/**
 * ExamSignUp entity - Exam registration
 */
@TableName("EXAMSIGNUP")
public class ExamSignUp extends BaseEntity {

    @TableId(type = IdType.INPUT)
    private Long examSignUpId;

    @TableField("examyear")
    private Integer examYear;

    @TableField("exampageid")
    private Long examAgeId;

    @TableField("workerid")
    private Long workerId;

    @TableField("sigupdate")
    private String signUpdate;

    @TableField("subjectid")
    private Integer subjectId;

    @JsonFormat(pattern = "yyyy-MM-dd", timezone = "GMT+8")
    @TableField("signupdate")
    private String signUpDate;

    @TableField("score")
    private Integer score;

    @TableField("status")
    private String status;

    public Long getExamSignUpId() {
        return examSignUpId;
    }

    public void setExamSignUpId(Long examSignUpId) {
        this.examSignUpId = examSignUpId;
    }

    public Integer getExamYear() {
        return examYear;
    }

    public void setExamYear(Integer examYear) {
        this.examYear = examYear;
    }

    public Long getExamAgeId() {
        return examAgeId;
    }

    public void setExamAgeId(Long examAgeId) {
        this.examAgeId = examAgeId;
    }

    public Long getWorkerId() {
        return workerId;
    }

    public void setWorkerId(Long workerId) {
        this.workerId = workerId;
    }

    public String getSignUpdate() {
        return signUpdate;
    }

    public void setSignUpdate(String signUpdate) {
        this.signUpdate = signUpdate;
    }

    public Integer getSubjectId() {
        return subjectId;
    }

    public void setSubjectId(Integer subjectId) {
        this.subjectId = subjectId;
    }

    public String getSignUpDate() {
        return signUpDate;
    }

    public void setSignUpDate(String signUpDate) {
        this.signUpDate = signUpDate;
    }

    public Integer getScore() {
        return score;
    }

    public void setScore(Integer score) {
        this.score = score;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }
}
