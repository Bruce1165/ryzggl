package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableName;
import com.ryzggl.common.BaseEntity;

import java.time.LocalDateTime;

/**
 * ApplyChange Entity - 变更申请表
 * Certificate change application with from/to fields for transfer scenarios
 */
@TableName("ApplyChange")
public class ApplyChange extends BaseEntity {

    /**
     * 申请ID (Primary Key)
     */
    @TableField("ApplyID")
    private String applyId;

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
     * 注册号
     */
    @TableField("PSN_RegisterNo")
    private String psnRegisterNo;

    /**
     * 有效日期
     */
    @TableField("ValidDate")
    private LocalDateTime validDate;

    /**
     * 变更原因
     */
    @TableField("ChangeReason")
    private String changeReason;

    /**
     * 原企业名称
     */
    @TableField("OldENT_Name")
    private String oldEntName;

    /**
     * 原联系人
     */
    @TableField("OldLinkMan")
    private String oldLinkMan;

    /**
     * 原企业电话
     */
    @TableField("OldENT_Telephone")
    private String oldEntTelephone;

    /**
     * 原企业通讯地址
     */
    @TableField("OldENT_Correspondence")
    private String oldEntCorrespondence;

    /**
     * 原邮编
     */
    @TableField("OldENT_Postcode")
    private String oldEntPostcode;

    /**
     * 企业名称
     */
    @TableField("ENT_Name")
    private String entName;

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
     * 邮编
     */
    @TableField("ENT_Postcode")
    private String entPostcode;

    /**
     * 企业地址
     */
    @TableField("END_Addess")
    private String endAddress;

    /**
     * 是否外省市
     */
    @TableField("IfOutside")
    private Boolean ifOutside;

    /**
     * 企业类型
     */
    @TableField("ENT_Type")
    private String entType;

    /**
     * 企业经济性质
     */
    @TableField("ENT_Economic_Nature")
    private String entEconomicNature;

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
     * 企业类别2
     */
    @TableField("ENT_Sort2")
    private String entSort2;

    /**
     * 企业资质等级2
     */
    @TableField("ENT_Grade2")
    private String entGrade2;

    /**
     * 企业资质证书号2
     */
    @TableField("ENT_QualificationCertificateNo2")
    private String entQualificationCertificateNo2;

    /**
     * 企业名称(从)
     */
    @TableField("ENT_NameFrom")
    private String entNameFrom;

    /**
     * 企业名称(到)
     */
    @TableField("ENT_NameTo")
    private String entNameTo;

    /**
     * 人员姓名(从)
     */
    @TableField("PSN_NameFrom")
    private String psnNameFrom;

    /**
     * 人员姓名(到)
     */
    @TableField("PSN_NameTo")
    private String psnNameTo;

    /**
     * 企业城市(从)
     */
    @TableField("FromENT_City")
    private String fromEntCity;

    /**
     * 企业城市(到)
     */
    @TableField("ToENT_City")
    private String toEntCity;

    /**
     * 人员性别(从)
     */
    @TableField("FromPSN_Sex")
    private String fromPsnSex;

    /**
     * 人员性别(到)
     */
    @TableField("ToPSN_Sex")
    private String toPsnSex;

    /**
     * 人员出生日期(从)
     */
    @TableField("FromPSN_BirthDate")
    private LocalDateTime fromPsnBirthDate;

    /**
     * 人员出生日期(到)
     */
    @TableField("ToPSN_BirthDate")
    private LocalDateTime toPsnBirthDate;

    /**
     * 人员证件类型(从)
     */
    @TableField("FromPSN_CertificateType")
    private String fromPsnCertificateType;

    /**
     * 人员证件类型(到)
     */
    @TableField("ToPSN_CertificateType")
    private String toPsnCertificateType;

    /**
     * 人员证件号(从)
     */
    @TableField("FromPSN_CertificateNO")
    private String fromPsnCertificateNo;

    /**
     * 人员证件号(到)
     */
    @TableField("ToPSN_CertificateNO")
    private String toPsnCertificateNo;

    /**
     * 资质证书编号
     */
    @TableField("ZGZSBH")
    private String zgzsbh;

    /**
     * 资质证书编号(变更后)
     */
    @TableField("To_ZGZSBH")
    private String toZgzsbh;

    /**
     * 企业地址(从)
     */
    @TableField("FromEND_Addess")
    private String fromEndAddress;

    /**
     * 企业地址(到)
     */
    @TableField("ToEND_Addess")
    private String toEndAddress;

    /**
     * 民族
     */
    @TableField("Nation")
    private String nation;

    // Getters and Setters
    public String getApplyId() {
        return applyId;
    }

    public void setApplyId(String applyId) {
        this.applyId = applyId;
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

    public String getPsnRegisterNo() {
        return psnRegisterNo;
    }

    public void setPsnRegisterNo(String psnRegisterNo) {
        this.psnRegisterNo = psnRegisterNo;
    }

    public LocalDateTime getValidDate() {
        return validDate;
    }

    public void setValidDate(LocalDateTime validDate) {
        this.validDate = validDate;
    }

    public String getChangeReason() {
        return changeReason;
    }

    public void setChangeReason(String changeReason) {
        this.changeReason = changeReason;
    }

    public String getOldEntName() {
        return oldEntName;
    }

    public void setOldEntName(String oldEntName) {
        this.oldEntName = oldEntName;
    }

    public String getOldLinkMan() {
        return oldLinkMan;
    }

    public void setOldLinkMan(String oldLinkMan) {
        this.oldLinkMan = oldLinkMan;
    }

    public String getOldEntTelephone() {
        return oldEntTelephone;
    }

    public void setOldEntTelephone(String oldEntTelephone) {
        this.oldEntTelephone = oldEntTelephone;
    }

    public String getOldEntCorrespondence() {
        return oldEntCorrespondence;
    }

    public void setOldEntCorrespondence(String oldEntCorrespondence) {
        this.oldEntCorrespondence = oldEntCorrespondence;
    }

    public String getOldEntPostcode() {
        return oldEntPostcode;
    }

    public void setOldEntPostcode(String oldEntPostcode) {
        this.oldEntPostcode = oldEntPostcode;
    }

    public String getEntName() {
        return entName;
    }

    public void setEntName(String entName) {
        this.entName = entName;
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

    public String getEntPostcode() {
        return entPostcode;
    }

    public void setEntPostcode(String entPostcode) {
        this.entPostcode = entPostcode;
    }

    public String getEndAddress() {
        return endAddress;
    }

    public void setEndAddress(String endAddress) {
        this.endAddress = endAddress;
    }

    public Boolean getIfOutside() {
        return ifOutside;
    }

    public void setIfOutside(Boolean ifOutside) {
        this.ifOutside = ifOutside;
    }

    public String getEntType() {
        return entType;
    }

    public void setEntType(String entType) {
        this.entType = entType;
    }

    public String getEntEconomicNature() {
        return entEconomicNature;
    }

    public void setEntEconomicNature(String entEconomicNature) {
        this.entEconomicNature = entEconomicNature;
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

    public String getEntSort2() {
        return entSort2;
    }

    public void setEntSort2(String entSort2) {
        this.entSort2 = entSort2;
    }

    public String getEntGrade2() {
        return entGrade2;
    }

    public void setEntGrade2(String entGrade2) {
        this.entGrade2 = entGrade2;
    }

    public String getEntQualificationCertificateNo2() {
        return entQualificationCertificateNo2;
    }

    public void setEntQualificationCertificateNo2(String entQualificationCertificateNo2) {
        this.entQualificationCertificateNo2 = entQualificationCertificateNo2;
    }

    public String getEntNameFrom() {
        return entNameFrom;
    }

    public void setEntNameFrom(String entNameFrom) {
        this.entNameFrom = entNameFrom;
    }

    public String getEntNameTo() {
        return entNameTo;
    }

    public void setEntNameTo(String entNameTo) {
        this.entNameTo = entNameTo;
    }

    public String getPsnNameFrom() {
        return psnNameFrom;
    }

    public void setPsnNameFrom(String psnNameFrom) {
        this.psnNameFrom = psnNameFrom;
    }

    public String getPsnNameTo() {
        return psnNameTo;
    }

    public void setPsnNameTo(String psnNameTo) {
        this.psnNameTo = psnNameTo;
    }

    public String getFromEntCity() {
        return fromEntCity;
    }

    public void setFromEntCity(String fromEntCity) {
        this.fromEntCity = fromEntCity;
    }

    public String getToEntCity() {
        return toEntCity;
    }

    public void setToEntCity(String toEntCity) {
        this.toEntCity = toEntCity;
    }

    public String getFromPsnSex() {
        return fromPsnSex;
    }

    public void setFromPsnSex(String fromPsnSex) {
        this.fromPsnSex = fromPsnSex;
    }

    public String getToPsnSex() {
        return toPsnSex;
    }

    public void setToPsnSex(String toPsnSex) {
        this.toPsnSex = toPsnSex;
    }

    public LocalDateTime getFromPsnBirthDate() {
        return fromPsnBirthDate;
    }

    public void setFromPsnBirthDate(LocalDateTime fromPsnBirthDate) {
        this.fromPsnBirthDate = fromPsnBirthDate;
    }

    public LocalDateTime getToPsnBirthDate() {
        return toPsnBirthDate;
    }

    public void setToPsnBirthDate(LocalDateTime toPsnBirthDate) {
        this.toPsnBirthDate = toPsnBirthDate;
    }

    public String getFromPsnCertificateType() {
        return fromPsnCertificateType;
    }

    public void setFromPsnCertificateType(String fromPsnCertificateType) {
        this.fromPsnCertificateType = fromPsnCertificateType;
    }

    public String getToPsnCertificateType() {
        return toPsnCertificateType;
    }

    public void setToPsnCertificateType(String toPsnCertificateType) {
        this.toPsnCertificateType = toPsnCertificateType;
    }

    public String getFromPsnCertificateNo() {
        return fromPsnCertificateNo;
    }

    public void setFromPsnCertificateNo(String fromPsnCertificateNo) {
        this.fromPsnCertificateNo = fromPsnCertificateNo;
    }

    public String getToPsnCertificateNo() {
        return toPsnCertificateNo;
    }

    public void setToPsnCertificateNo(String toPsnCertificateNo) {
        this.toPsnCertificateNo = toPsnCertificateNo;
    }

    public String getZgzsbh() {
        return zgzsbh;
    }

    public void setZgzsbh(String zgzsbh) {
        this.zgzsbh = zgzsbh;
    }

    public String getToZgzsbh() {
        return toZgzsbh;
    }

    public void setToZgzsbh(String toZgzsbh) {
        this.toZgzsbh = toZgzsbh;
    }

    public String getFromEndAddress() {
        return fromEndAddress;
    }

    public void setFromEndAddress(String fromEndAddress) {
        this.fromEndAddress = fromEndAddress;
    }

    public String getToEndAddress() {
        return toEndAddress;
    }

    public void setToEndAddress(String toEndAddress) {
        this.toEndAddress = toEndAddress;
    }

    public String getNation() {
        return nation;
    }

    public void setNation(String nation) {
        this.nation = nation;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        if (!super.equals(o)) return false;
        ApplyChange applyChange = (ApplyChange) o;
        return applyId != null && applyId.equals(applyChange.applyId);
    }

    @Override
    public int hashCode() {
        return super.hashCode() * 31 + (applyId != null ? applyId.hashCode() : 0);
    }
}
