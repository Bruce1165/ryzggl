package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableName;
import com.ryzggl.common.BaseEntity;

import java.time.LocalDateTime;

/**
 * ApplyFirst Entity - 首次注册申请表
 * Initial registration application for workers
 */
@TableName("ApplyFirst")
public class ApplyFirst extends BaseEntity {

    /**
     * 申请ID (Primary Key)
     */
    @TableField("ApplyID")
    private String applyId;

    /**
     * 建筑师类型
     */
    @TableField("ArchitectType")
    private String architectType;

    /**
     * 电话
     */
    @TableField("PSN_Telephone")
    private String psnTelephone;

    /**
     * 手机
     */
    @TableField("PSN_MobilePhone")
    private String psnMobilePhone;

    /**
     * 邮箱
     */
    @TableField("PSN_Email")
    private String psnEmail;

    /**
     * 民族
     */
    @TableField("Nation")
    private String nation;

    /**
     * 出生日期
     */
    @TableField("Birthday")
    private LocalDateTime birthday;

    /**
     * 毕业学校
     */
    @TableField("School")
    private String school;

    /**
     * 专业
     */
    @TableField("Major")
    private String major;

    /**
     * 毕业时间
     */
    @TableField("GraduationTime")
    private LocalDateTime graduationTime;

    /**
     * 学历
     */
    @TableField("XueLi")
    private String xueLi;

    /**
     * 学位
     */
    @TableField("XueWei")
    private String xueWei;

    /**
     * 发薪
     */
    @TableField("FR")
    private String fr;

    /**
     * 联系人
     */
    @TableField("LinkMan")
    private String linkMan;

    /**
     * 企业电话
     */
    @TableField("ENT_Telephone")
    private String entTelephone;

    /**
     * 企业通讯地址
     */
    @TableField("ENT_Correspondence")
    private String entCorrespondence;

    /**
     * 企业经济性质
     */
    @TableField("ENT_Economic_Nature")
    private String entEconomicNature;

    /**
     * 企业地址
     */
    @TableField("END_Addess")
    private String endAddress;

    /**
     * 邮编
     */
    @TableField("ENT_Postcode")
    private String entPostcode;

    /**
     * 企业类型
     */
    @TableField("ENT_Type")
    private String entType;

    /**
     * 企业类别
     */
    @TableField("ENT_Sort")
    private String entSort;

    /**
     * 企业资质等级
     */
    @TableField("ENT_Grade")
    private String entGrade;

    /**
     * 企业资质证书号
     */
    @TableField("ENT_QualificationCertificateNo")
    private String entQualificationCertificateNo;

    /**
     * 专业
     */
    @TableField("ZhuanYe")
    private String zhuanYe;

    /**
     * 申请类型
     */
    @TableField("GetType")
    private String getType;

    /**
     * 考试证书号
     */
    @TableField("PSN_ExamCertCode")
    private String psnExamCertCode;

    /**
     * 颁证日期
     */
    @TableField("ConferDate")
    private LocalDateTime conferDate;

    /**
     * 申请注册专业
     */
    @TableField("ApplyRegisteProfession")
    private String applyRegisteProfession;

    /**
     * 必修
     */
    @TableField("BiXiu")
    private Integer biXiu;

    /**
     * 选修
     */
    @TableField("XuanXiu")
    private Integer xuanXiu;

    /**
     * 考试信息
     */
    @TableField("ExamInfo")
    private String examInfo;

    /**
     * 其他证书
     */
    @TableField("OtherCert")
    private String otherCert;

    /**
     * 是否同单位
     */
    @TableField("IfSameUnit")
    private Boolean ifSameUnit;

    /**
     * 主业
     */
    @TableField("MainJob")
    private String mainJob;

    // Getters and Setters
    public String getApplyId() {
        return applyId;
    }

    public void setApplyId(String applyId) {
        this.applyId = applyId;
    }

    public String getArchitectType() {
        return architectType;
    }

    public void setArchitectType(String architectType) {
        this.architectType = architectType;
    }

    public String getPsnTelephone() {
        return psnTelephone;
    }

    public void setPsnTelephone(String psnTelephone) {
        this.psnTelephone = psnTelephone;
    }

    public String getPsnMobilePhone() {
        return psnMobilePhone;
    }

    public void setPsnMobilePhone(String psnMobilePhone) {
        this.psnMobilePhone = psnMobilePhone;
    }

    public String getPsnEmail() {
        return psnEmail;
    }

    public void setPsnEmail(String psnEmail) {
        this.psnEmail = psnEmail;
    }

    public String getNation() {
        return nation;
    }

    public void setNation(String nation) {
        this.nation = nation;
    }

    public LocalDateTime getBirthday() {
        return birthday;
    }

    public void setBirthday(LocalDateTime birthday) {
        this.birthday = birthday;
    }

    public String getSchool() {
        return school;
    }

    public void setSchool(String school) {
        this.school = school;
    }

    public String getMajor() {
        return major;
    }

    public void setMajor(String major) {
        this.major = major;
    }

    public LocalDateTime getGraduationTime() {
        return graduationTime;
    }

    public void setGraduationTime(LocalDateTime graduationTime) {
        this.graduationTime = graduationTime;
    }

    public String getXueLi() {
        return xueLi;
    }

    public void setXueLi(String xueLi) {
        this.xueLi = xueLi;
    }

    public String getXueWei() {
        return xueWei;
    }

    public void setXueWei(String xueWei) {
        this.xueWei = xueWei;
    }

    public String getFr() {
        return fr;
    }

    public void setFr(String fr) {
        this.fr = fr;
    }

    public String getLinkMan() {
        return linkMan;
    }

    public void setLinkMan(String linkMan) {
        this.linkMan = linkMan;
    }

    public String getEntTelephone() {
        return entTelephone;
    }

    public void setEntTelephone(String entTelephone) {
        this.entTelephone = entTelephone;
    }

    public String getEntCorrespondence() {
        return entCorrespondence;
    }

    public void setEntCorrespondence(String entCorrespondence) {
        this.entCorrespondence = entCorrespondence;
    }

    public String getEntEconomicNature() {
        return entEconomicNature;
    }

    public void setEntEconomicNature(String entEconomicNature) {
        this.entEconomicNature = entEconomicNature;
    }

    public String getEndAddress() {
        return endAddress;
    }

    public void setEndAddress(String endAddress) {
        this.endAddress = endAddress;
    }

    public String getEntPostcode() {
        return entPostcode;
    }

    public void setEntPostcode(String entPostcode) {
        this.entPostcode = entPostcode;
    }

    public String getEntType() {
        return entType;
    }

    public void setEntType(String entType) {
        this.entType = entType;
    }

    public String getEntSort() {
        return entSort;
    }

    public void setEntSort(String entSort) {
        this.entSort = entSort;
    }

    public String getEntGrade() {
        return entGrade;
    }

    public void setEntGrade(String entGrade) {
        this.entGrade = entGrade;
    }

    public String getEntQualificationCertificateNo() {
        return entQualificationCertificateNo;
    }

    public void setEntQualificationCertificateNo(String entQualificationCertificateNo) {
        this.entQualificationCertificateNo = entQualificationCertificateNo;
    }

    public String getZhuanYe() {
        return zhuanYe;
    }

    public void setZhuanYe(String zhuanYe) {
        this.zhuanYe = zhuanYe;
    }

    public String getGetType() {
        return getType;
    }

    public void setGetType(String getType) {
        this.getType = getType;
    }

    public String getPsnExamCertCode() {
        return psnExamCertCode;
    }

    public void setPsnExamCertCode(String psnExamCertCode) {
        this.psnExamCertCode = psnExamCertCode;
    }

    public LocalDateTime getConferDate() {
        return conferDate;
    }

    public void setConferDate(LocalDateTime conferDate) {
        this.conferDate = conferDate;
    }

    public String getApplyRegisteProfession() {
        return applyRegisteProfession;
    }

    public void setApplyRegisteProfession(String applyRegisteProfession) {
        this.applyRegisteProfession = applyRegisteProfession;
    }

    public Integer getBiXiu() {
        return biXiu;
    }

    public void setBiXiu(Integer biXiu) {
        this.biXiu = biXiu;
    }

    public Integer getXuanXiu() {
        return xuanXiu;
    }

    public void setXuanXiu(Integer xuanXiu) {
        this.xuanXiu = xuanXiu;
    }

    public String getExamInfo() {
        return examInfo;
    }

    public void setExamInfo(String examInfo) {
        this.examInfo = examInfo;
    }

    public String getOtherCert() {
        return otherCert;
    }

    public void setOtherCert(String otherCert) {
        this.otherCert = otherCert;
    }

    public Boolean getIfSameUnit() {
        return ifSameUnit;
    }

    public void setIfSameUnit(Boolean ifSameUnit) {
        this.ifSameUnit = ifSameUnit;
    }

    public String getMainJob() {
        return mainJob;
    }

    public void setMainJob(String mainJob) {
        this.mainJob = mainJob;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        if (!super.equals(o)) return false;
        ApplyFirst applyFirst = (ApplyFirst) o;
        return applyId != null && applyId.equals(applyFirst.applyId);
    }

    @Override
    public int hashCode() {
        return super.hashCode() * 31 + (applyId != null ? applyId.hashCode() : 0);
    }
}
