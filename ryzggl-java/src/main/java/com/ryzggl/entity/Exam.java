package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableName;
import com.ryzggl.common.BaseEntity;

/**
 * Exam Entity - 考试表
 */
@TableName("Exam")
public class Exam extends BaseEntity {

    /**
     * 考试编号
     */
    @TableField("EXAMCODE")
    private String examCode;

    /**
     * 考试名称
     */
    @TableField("EXAMNAME")
    private String examName;

    /**
     * 资格代码
     */
    @TableField("QUALIFICATIONCODE")
    private String qualificationCode;

    /**
     * 资格名称
     */
    @TableField("QUALIFICATIONNAME")
    private String qualificationName;

    /**
     * 资格等级
     */
    @TableField("LEVEL")
    private String level;

    /**
     * 专业类别
     */
    @TableField("PROFESSION")
    private String profession;

    /**
     * 考试日期
     */
    @TableField("EXAMDATE")
    private String examDate;

    /**
     * 考试时间
     */
    @TableField("EXAMTIME")
    private String examTime;

    /**
     * 考点代码
     */
    @TableField("PLACECODE")
    private String placeCode;

    /**
     * 考点名称
     */
    @TableField("PLACENAME")
    private String placeName;

    /**
     * 考点地址
     */
    @TableField("PLACEADDRESS")
    private String placeAddress;

    /**
     * 报名人数
     */
    @TableField("SIGNUPCOUNT")
    private Integer signUpCount;

    /**
     * 实考人数
     */
    @TableField("ACTUALCOUNT")
    private Integer actualCount;

    /**
     * 通过人数
     */
    @TableField("PASSCOUNT")
    private Integer passCount;

    /**
     * 考试状态（未开始、进行中、已结束）
     */
    @TableField("STATUS")
    private String status;

    /**
     * 报名开始日期
     */
    @TableField("SIGNUPSTART")
    private String signUpStart;

    /**
     * 报名结束日期
     */
    @TableField("SIGNUPEND")
    private String signUpEnd;

    // Getters and Setters
    public String getExamCode() {
        return examCode;
    }

    public void setExamCode(String examCode) {
        this.examCode = examCode;
    }

    public String getExamName() {
        return examName;
    }

    public void setExamName(String examName) {
        this.examName = examName;
    }

    public String getQualificationCode() {
        return qualificationCode;
    }

    public void setQualificationCode(String qualificationCode) {
        this.qualificationCode = qualificationCode;
    }

    public String getQualificationName() {
        return qualificationName;
    }

    public void setQualificationName(String qualificationName) {
        this.qualificationName = qualificationName;
    }

    public String getLevel() {
        return level;
    }

    public void setLevel(String level) {
        this.level = level;
    }

    public String getProfession() {
        return profession;
    }

    public void setProfession(String profession) {
        this.profession = profession;
    }

    public String getExamDate() {
        return examDate;
    }

    public void setExamDate(String examDate) {
        this.examDate = examDate;
    }

    public String getExamTime() {
        return examTime;
    }

    public void setExamTime(String examTime) {
        this.examTime = examTime;
    }

    public String getPlaceCode() {
        return placeCode;
    }

    public void setPlaceCode(String placeCode) {
        this.placeCode = placeCode;
    }

    public String getPlaceName() {
        return placeName;
    }

    public void setPlaceName(String placeName) {
        this.placeName = placeName;
    }

    public String getPlaceAddress() {
        return placeAddress;
    }

    public void setPlaceAddress(String placeAddress) {
        this.placeAddress = placeAddress;
    }

    public Integer getSignUpCount() {
        return signUpCount;
    }

    public void setSignUpCount(Integer signUpCount) {
        this.signUpCount = signUpCount;
    }

    public Integer getActualCount() {
        return actualCount;
    }

    public void setActualCount(Integer actualCount) {
        this.actualCount = actualCount;
    }

    public Integer getPassCount() {
        return passCount;
    }

    public void setPassCount(Integer passCount) {
        this.passCount = passCount;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    public String getSignUpStart() {
        return signUpStart;
    }

    public void setSignUpStart(String signUpStart) {
        this.signUpStart = signUpStart;
    }

    public String getSignUpEnd() {
        return signUpEnd;
    }

    public void setSignUpEnd(String signUpEnd) {
        this.signUpEnd = signUpEnd;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        if (!super.equals(o)) return false;
        Exam exam = (Exam) o;
        return examCode != null && examCode.equals(exam.examCode);
    }

    @Override
    public int hashCode() {
        return super.hashCode() * 31 + (examCode != null ? examCode.hashCode() : 0);
    }
}
