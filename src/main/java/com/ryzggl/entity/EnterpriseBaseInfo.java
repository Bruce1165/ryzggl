package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;
import com.fasterxml.jackson.annotation.JsonFormat;
import com.ryzggl.common.BaseEntity;

/**
 * EnterpriseBaseInfo entity - Enterprise base information (企业基本信息)
 * Replaces .NET: COC_ONE_ENT_BaseInfo.cs / EnterpriseBaseInfoDAL.cs
 */
@TableName("COC_ONE_ENT_BASEINFO")
public class EnterpriseBaseInfo extends BaseEntity {

    @TableField("ENT_NAME")
    private String entName;

    @TableField("ENT_ORGANIZATIONSCODE")
    private String entOrganizationsCode;

    @TableField("ENT_ECONOMIC_NATURE")
    private String entEconomicNature;

    @TableField("ENT_PROVINCE")
    private String entProvince;

    @TableField("ENT_CITY")
    private String entCity;

    @TableField("ENT_ADDRESS")
    private String entAddress;

    @TableField("ENT_CONTACT")
    private String entContact;

    @TableField("ENT_TELEPHONE")
    private String entTelephone;

    @TableField("ENT_MOBILEPHONE")
    private String entMobilePhone;

    @TableField("ENT_TYPE")
    private String entType;

    @TableField("ENT_SORT")
    private String entSort;

    @TableField("ENT_GRADE")
    private String entGrade;

    @TableField("ENT_STATUS")
    private String entStatus;

    @TableField("ENT_REGISTERDATE")
    private String entRegisterDate;

    @TableField("ENT_LICENCE")
    private String entLicence;

    @TableField("ENT_LEGALREPRESENTATIVE")
    private String entLegalRepresentative;

    @TableField("ENT_REGISTERCAPITAL")
    private String entRegisterCapital;

    @TableField("ENT_BUSINESSSCOPE")
    private String entBusinessScope;

    @TableField("ENT_TAXNO")
    private String entTaxNo;

    @TableField("ENT_OPENBANK")
    private String entOpenBank;

    @TableField("ENT_BANKACCOUNT")
    private String entBankAccount;

    @TableField("ENT_EMAIL")
    private String entEmail;

    @TableField("ENT_WEBSITE")
    private String entWebsite;

    @TableField("ENT_FAX")
    private String entFax;

    @TableField("ENT_ZIPCODE")
    private String entZipCode;

    @TableField("remark")
    private String remark;

    public String getEntName() {
        return entName;
    }

    public void setEntName(String entName) {
        this.entName = entName;
    }

    public String getEntOrganizationsCode() {
        return entOrganizationsCode;
    }

    public void setEntOrganizationsCode(String entOrganizationsCode) {
        this.entOrganizationsCode = entOrganizationsCode;
    }

    public String getEntEconomicNature() {
        return entEconomicNature;
    }

    public void setEntEconomicNature(String entEconomicNature) {
        this.entEconomicNature = entEconomicNature;
    }

    public String getEntProvince() {
        return entProvince;
    }

    public void setEntProvince(String entProvince) {
        this.entProvince = entProvince;
    }

    public String getEntCity() {
        return entCity;
    }

    public void setEntCity(String entCity) {
        this.entCity = entCity;
    }

    public String getEntAddress() {
        return entAddress;
    }

    public void setEntAddress(String entAddress) {
        this.entAddress = entAddress;
    }

    public String getEntContact() {
        return entContact;
    }

    public void setEntContact(String entContact) {
        this.entContact = entContact;
    }

    public String getEntTelephone() {
        return entTelephone;
    }

    public void setEntTelephone(String entTelephone) {
        this.entTelephone = entTelephone;
    }

    public String getEntMobilePhone() {
        return entMobilePhone;
    }

    public void setEntMobilePhone(String entMobilePhone) {
        this.entMobilePhone = entMobilePhone;
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

    public String getEntStatus() {
        return entStatus;
    }

    public void setEntStatus(String entStatus) {
        this.entStatus = entStatus;
    }

    public String getEntRegisterDate() {
        return entRegisterDate;
    }

    public void setEntRegisterDate(String entRegisterDate) {
        this.entRegisterDate = entRegisterDate;
    }

    public String getEntLicence() {
        return entLicence;
    }

    public void setEntLicence(String entLicence) {
        this.entLicence = entLicence;
    }

    public String getEntLegalRepresentative() {
        return entLegalRepresentative;
    }

    public void setEntLegalRepresentative(String entLegalRepresentative) {
        this.entLegalRepresentative = entLegalRepresentative;
    }

    public String getEntRegisterCapital() {
        return entRegisterCapital;
    }

    public void setEntRegisterCapital(String entRegisterCapital) {
        this.entRegisterCapital = entRegisterCapital;
    }

    public String getEntBusinessScope() {
        return entBusinessScope;
    }

    public void setEntBusinessScope(String entBusinessScope) {
        this.entBusinessScope = entBusinessScope;
    }

    public String getEntTaxNo() {
        return entTaxNo;
    }

    public void setEntTaxNo(String entTaxNo) {
        this.entTaxNo = entTaxNo;
    }

    public String getEntOpenBank() {
        return entOpenBank;
    }

    public void setEntOpenBank(String entOpenBank) {
        this.entOpenBank = entOpenBank;
    }

    public String getEntBankAccount() {
        return entBankAccount;
    }

    public void setEntBankAccount(String entBankAccount) {
        this.entBankAccount = entBankAccount;
    }

    public String getEntEmail() {
        return entEmail;
    }

    public void setEntEmail(String entEmail) {
        this.entEmail = entEmail;
    }

    public String getEntWebsite() {
        return entWebsite;
    }

    public void setEntWebsite(String entWebsite) {
        this.entWebsite = entWebsite;
    }

    public String getEntFax() {
        return entFax;
    }

    public void setEntFax(String entFax) {
        this.entFax = entFax;
    }

    public String getEntZipCode() {
        return entZipCode;
    }

    public void setEntZipCode(String entZipCode) {
        this.entZipCode = entZipCode;
    }

    public String getRemark() {
        return remark;
    }

    public void setRemark(String remark) {
        this.remark = remark;
    }
}
