package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableName;
import com.ryzggl.common.BaseEntity;

import java.time.LocalDateTime;

/**
 * ApplyCancel Entity - 注销申请表
 * Certificate cancellation application
 */
@TableName("ApplyCancel")
public class ApplyCancel extends BaseEntity {

    /**
     * 申请ID (Primary Key)
     */
    @TableField("ApplyID")
    private String applyId;

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
     * 企业电话
     */
    @TableField("ENT_Telephone")
    private String entTelephone;

    /**
     * 手机
     */
    @TableField("PSN_MobilePhone")
    private String psnMobilePhone;

    /**
     * 注册证书号
     */
    @TableField("PSN_RegisterCertificateNo")
    private String psnRegisterCertificateNo;

    /**
     * 注册号
     */
    @TableField("PSN_RegisterNO")
    private String psnRegisterNo;

    /**
     * 注册有效期
     */
    @TableField("RegisterValidity")
    private LocalDateTime registerValidity;

    /**
     * 注销原因
     */
    @TableField("CancelReason")
    private String cancelReason;

    /**
     * 邮箱
     */
    @TableField("PSN_Email")
    private String psnEmail;

    /**
     * 联系人
     */
    @TableField("LinkMan")
    private String linkMan;

    /**
     * 申请人类型
     */
    @TableField("ApplyManType")
    private String applyManType;

    /**
     * 资质ID项
     */
    @TableField("ZyIDItem")
    private String zyIdItem;

    /**
     * 资质项
     */
    @TableField("ZyItem")
    private String zyItem;

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

    public String getEntTelephone() {
        return entTelephone;
    }

    public void setEntTelephone(String entTelephone) {
        this.entTelephone = entTelephone;
    }

    public String getPsnMobilePhone() {
        return psnMobilePhone;
    }

    public void setPsnMobilePhone(String psnMobilePhone) {
        this.psnMobilePhone = psnMobilePhone;
    }

    public String getPsnRegisterCertificateNo() {
        return psnRegisterCertificateNo;
    }

    public void setPsnRegisterCertificateNo(String psnRegisterCertificateNo) {
        this.psnRegisterCertificateNo = psnRegisterCertificateNo;
    }

    public String getPsnRegisterNo() {
        return psnRegisterNo;
    }

    public void setPsnRegisterNo(String psnRegisterNo) {
        this.psnRegisterNo = psnRegisterNo;
    }

    public LocalDateTime getRegisterValidity() {
        return registerValidity;
    }

    public void setRegisterValidity(LocalDateTime registerValidity) {
        this.registerValidity = registerValidity;
    }

    public String getCancelReason() {
        return cancelReason;
    }

    public void setCancelReason(String cancelReason) {
        this.cancelReason = cancelReason;
    }

    public String getPsnEmail() {
        return psnEmail;
    }

    public void setPsnEmail(String psnEmail) {
        this.psnEmail = psnEmail;
    }

    public String getLinkMan() {
        return linkMan;
    }

    public void setLinkMan(String linkMan) {
        this.linkMan = linkMan;
    }

    public String getApplyManType() {
        return applyManType;
    }

    public void setApplyManType(String applyManType) {
        this.applyManType = applyManType;
    }

    public String getZyIdItem() {
        return zyIdItem;
    }

    public void setZyIdItem(String zyIdItem) {
        this.zyIdItem = zyIdItem;
    }

    public String getZyItem() {
        return zyItem;
    }

    public void setZyItem(String zyItem) {
        this.zyItem = zyItem;
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
        ApplyCancel applyCancel = (ApplyCancel) o;
        return applyId != null && applyId.equals(applyCancel.applyId);
    }

    @Override
    public int hashCode() {
        return super.hashCode() * 31 + (applyId != null ? applyId.hashCode() : 0);
    }
}
