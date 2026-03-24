package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableName;
import com.ryzggl.common.BaseEntity;
import java.time.LocalDate;

/**
 * CertificateChange Entity - 证书变更表
 */
@TableName("CertificateChange")
public class CertificateChange extends BaseEntity {

    /**
     * 证书ID
     */
    @TableField("CERTIFICATEID")
    private Long certificateId;

    /**
     * 证书编号
     */
    @TableField("CERTIFICATECODE")
    private String certificateCode;

    /**
     * 变更类型（个人信息、单位信息、资格等级等）
     */
    @TableField("CHANGETYPE")
    private String changeType;

    /**
     * 变更原因
     */
    @TableField("CHANGEREASON")
    private String changeReason;

    /**
     * 变更前值
     */
    @TableField("OLDVALUE")
    private String oldValue;

    /**
     * 变更后值
     */
    @TableField("NEWVALUE")
    private String newValue;

    /**
     * 变更日期
     */
    @TableField("CHANGEDATE")
    private LocalDate changeDate;

    /**
     * 变更人
     */
    @TableField("CHANGEMAN")
    private String changeMan;

    /**
     * 变更人ID
     */
    @TableField("CHANGEMANID")
    private Long changeManId;

    /**
     * 审批意见
     */
    @TableField("CHECKADVISE")
    private String checkAdvise;

    /**
     * 审批人
     */
    @TableField("CHECKMAN")
    private String checkMan;

    /**
     * 审批日期
     */
    @TableField("CHECKDATE")
    private String checkDate;

    /**
     * 附件路径
     */
    @TableField("FILEPATH")
    private String filePath;

    /**
     * 备注
     */
    @TableField("REMARK")
    private String remark;

    // Getters and Setters
    public Long getCertificateId() {
        return certificateId;
    }

    public void setCertificateId(Long certificateId) {
        this.certificateId = certificateId;
    }

    public String getCertificateCode() {
        return certificateCode;
    }

    public void setCertificateCode(String certificateCode) {
        this.certificateCode = certificateCode;
    }

    public String getChangeType() {
        return changeType;
    }

    public void setChangeType(String changeType) {
        this.changeType = changeType;
    }

    public String getChangeReason() {
        return changeReason;
    }

    public void setChangeReason(String changeReason) {
        this.changeReason = changeReason;
    }

    public String getOldValue() {
        return oldValue;
    }

    public void setOldValue(String oldValue) {
        this.oldValue = oldValue;
    }

    public String getNewValue() {
        return newValue;
    }

    public void setNewValue(String newValue) {
        this.newValue = newValue;
    }

    public LocalDate getChangeDate() {
        return changeDate;
    }

    public void setChangeDate(LocalDate changeDate) {
        this.changeDate = changeDate;
    }

    public String getChangeMan() {
        return changeMan;
    }

    public void setChangeMan(String changeMan) {
        this.changeMan = changeMan;
    }

    public Long getChangeManId() {
        return changeManId;
    }

    public void setChangeManId(Long changeManId) {
        this.changeManId = changeManId;
    }

    public String getCheckAdvise() {
        return checkAdvise;
    }

    public void setCheckAdvise(String checkAdvise) {
        this.checkAdvise = checkAdvise;
    }

    public String getCheckMan() {
        return checkMan;
    }

    public void setCheckMan(String checkMan) {
        this.checkMan = checkMan;
    }

    public String getCheckDate() {
        return checkDate;
    }

    public void setCheckDate(String checkDate) {
        this.checkDate = checkDate;
    }

    public String getFilePath() {
        return filePath;
    }

    public void setFilePath(String filePath) {
        this.filePath = filePath;
    }

    public String getRemark() {
        return remark;
    }

    public void setRemark(String remark) {
        this.remark = remark;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        if (!super.equals(o)) return false;
        CertificateChange that = (CertificateChange) o;
        return certificateId != null && certificateId.equals(that.certificateId);
    }

    @Override
    public int hashCode() {
        return super.hashCode() * 31 + (certificateId != null ? certificateId.hashCode() : 0);
    }
}