package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableName;
import com.ryzggl.common.BaseEntity;

/**
 * Worker Entity - 人员表
 */
@TableName("Worker")
public class Worker extends BaseEntity {

    /**
     * 人员代码
     */
    @TableField("WORKERCODE")
    private String workerCode;

    /**
     * 姓名
     */
    @TableField("NAME")
    private String name;

    /**
     * 性别（男、女）
     */
    @TableField("GENDER")
    private String gender;

    /**
     * 身份证号
     */
    @TableField("IDCARD")
    private String idCard;

    /**
     * 手机号
     */
    @TableField("PHONE")
    private String phone;

    /**
     * 所属企业代码
     */
    @TableField("UNITCODE")
    private String unitCode;

    /**
     * 企业名称
     */
    @TableField("UNITNAME")
    private String unitName;

    /**
     * 出生年月
     */
    @TableField("BIRTHDATE")
    private String birthDate;

    /**
     * 民族
     */
    @TableField("NATION")
    private String nation;

    /**
     * 学历
     */
    @TableField("EDUCATION")
    private String education;

    /**
     * 毕业院校
     */
    @TableField("GRADUATESCHOOL")
    private String graduateSchool;

    /**
     * 毕业专业
     */
    @TableField("GRADUATEMAJOR")
    private String graduateMajor;

    /**
     * 工作单位
     */
    @TableField("WORKUNIT")
    private String workUnit;

    /**
     * 职务
     */
    @TableField("JOBPOSITION")
    private String jobPosition;

    /**
     * 职称
     */
    @TableField("JOBTITLE")
    private String jobTitle;

    /**
     * 住址
     */
    @TableField("ADDRESS")
    private String address;

    /**
     * 邮箱
     */
    @TableField("EMAIL")
    private String email;

    /**
     * 人员状态（正常、离职）
     */
    @TableField("STATUS")
    private String status;

    // Getters and Setters
    public String getWorkerCode() {
        return workerCode;
    }

    public void setWorkerCode(String workerCode) {
        this.workerCode = workerCode;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getGender() {
        return gender;
    }

    public void setGender(String gender) {
        this.gender = gender;
    }

    public String getIdCard() {
        return idCard;
    }

    public void setIdCard(String idCard) {
        this.idCard = idCard;
    }

    public String getPhone() {
        return phone;
    }

    public void setPhone(String phone) {
        this.phone = phone;
    }

    public String getUnitCode() {
        return unitCode;
    }

    public void setUnitCode(String unitCode) {
        this.unitCode = unitCode;
    }

    public String getUnitName() {
        return unitName;
    }

    public void setUnitName(String unitName) {
        this.unitName = unitName;
    }

    public String getBirthDate() {
        return birthDate;
    }

    public void setBirthDate(String birthDate) {
        this.birthDate = birthDate;
    }

    public String getNation() {
        return nation;
    }

    public void setNation(String nation) {
        this.nation = nation;
    }

    public String getEducation() {
        return education;
    }

    public void setEducation(String education) {
        this.education = education;
    }

    public String getGraduateSchool() {
        return graduateSchool;
    }

    public void setGraduateSchool(String graduateSchool) {
        this.graduateSchool = graduateSchool;
    }

    public String getGraduateMajor() {
        return graduateMajor;
    }

    public void setGraduateMajor(String graduateMajor) {
        this.graduateMajor = graduateMajor;
    }

    public String getWorkUnit() {
        return workUnit;
    }

    public void setWorkUnit(String workUnit) {
        this.workUnit = workUnit;
    }

    public String getJobPosition() {
        return jobPosition;
    }

    public void setJobPosition(String jobPosition) {
        this.jobPosition = jobPosition;
    }

    public String getJobTitle() {
        return jobTitle;
    }

    public void setJobTitle(String jobTitle) {
        this.jobTitle = jobTitle;
    }

    public String getAddress() {
        return address;
    }

    public void setAddress(String address) {
        this.address = address;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        if (!super.equals(o)) return false;
        Worker worker = (Worker) o;
        return workerCode != null && workerCode.equals(worker.workerCode);
    }

    @Override
    public int hashCode() {
        return super.hashCode() * 31 + (workerCode != null ? workerCode.hashCode() : 0);
    }
}
