package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;
import com.fasterxml.jackson.annotation.JsonFormat;
import com.ryzggl.common.BaseEntity;

/**
 * ExamResult entity - Exam scores and results
 */
@TableName("EXAMRESULT")
public class ExamResult extends BaseEntity {

    @TableId(type = IdType.INPUT)
    private Long examResultId;

    @TableField("examsignid")
    private Long examSignUpId;

    @TableField("score")
    private Integer score;

    @TableField("grade")
    private String grade;

    @TableField("resultdate")
    @JsonFormat(pattern = "yyyy-MM-dd", timezone = "GMT+8")
    private String resultDate;

    public Long getExamResultId() {
        return examResultId;
    }

    public void setExamResultId(Long examResultId) {
        this.examResultId = examResultId;
    }

    public Long getExamSignUpId() {
        return examSignUpId;
    }

    public void setExamSignUpId(Long examSignUpId) {
        this.examSignUpId = examSignUpId;
    }

    public Integer getScore() {
        return score;
    }

    public void setScore(Integer score) {
        this.score = score;
    }

    public String getGrade() {
        return grade;
    }

    public void setGrade(String grade) {
        this.grade = grade;
    }

    public String getResultDate() {
        return resultDate;
    }

    public void setResultDate(String resultDate) {
        this.resultDate = resultDate;
    }
}
