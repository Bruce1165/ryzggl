package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;
import com.fasterxml.jackson.annotation.JsonFormat;
import com.ryzggl.common.BaseEntity;

/**
 * Exam Age entity - Exam plans/periods
 */
@TableName("EXAMPAGE")
public class ExampAge extends BaseEntity {

    @TableId(type = IdType.INPUT)
    private Long examAgeId;

    @TableField("examyear")
    private Integer examYear;

    @TableField("subjectid")
    private Integer subjectId;

    @TableField("exampagetitle")
    private String examAgeTitle;

    @TableField("remark")
    private String remark;

    public Long getExamAgeId() {
        return examAgeId;
    }

    public void setExamAgeId(Long examAgeId) {
        this.examAgeId = examAgeId;
    }

    public Integer getExamYear() {
        return examYear;
    }

    public void setExamYear(Integer examYear) {
        this.examYear = examYear;
    }

    public Integer getSubjectId() {
        return subjectId;
    }

    public void setSubjectId(Integer subjectId) {
        this.subjectId = subjectId;
    }

    public String getExamAgeTitle() {
        return examAgeTitle;
    }

    public void setExamAgeTitle(String examAgeTitle) {
        this.examAgeTitle = examAgeTitle;
    }

    public String getRemark() {
        return remark;
    }

    public void setRemark(String remark) {
        this.remark = remark;
    }
}
