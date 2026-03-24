package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableName;
import com.ryzggl.common.BaseEntity;

/**
 * Certificate Entity - 证书表
 */
@TableName("Certificate")
public class Certificate extends BaseEntity {

    /**
     * 证书编号
     */
    @TableField("CERTNO")
    private String certNo;

    /**
     * 持证人ID
     */
    @TableField("WORKERID")
    private Long workerId;

    /**
     * 持证人姓名
     */
    @TableField("HOLDERNAME")
    private String holderName;

    /**
     * 身份证号
     */
    @TableField("IDCARD")
    private String idCard;

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
     * 证书类型
     */
    @TableField("CERTIFICATETYPE")
    private String certificateType;

    /**
     * 有效期起始日期
     */
    @TableField("VALIDSTART")
    private String validStart;

    /**
     * 有效期结束日期
     */
    @TableField("VALIDEND")
    private String validEnd;

    /**
     * 证书状态（有效、暂停、注销）
     */
    @TableField("STATUS")
    private String status;

    /**
     * 暂停原因
     */
    @TableField("PAUSEREASON")
    private String pauseReason;

    /**
     * 注销原因
     */
    @TableField("REVOKEREASON")
    private String revokeReason;

    /**
     * 发证日期
     */
    @TableField("ISSUEDATE")
    private String issueDate;

    /**
     * 发证机关
     */
    @TableField("ISSUEAUTHORITY")
    private String issueAuthority;

    /**
     * 证书照片路径
     */
    @TableField("PHOTO")
    private String photo;

    // Getters and Setters
    public String getCertNo() {
        return certNo;
    }

    public void setCertNo(String certNo) {
        this.certNo = certNo;
    }

    public Long getWorkerId() {
        return workerId;
    }

    public void setWorkerId(Long workerId) {
        this.workerId = workerId;
    }

    public String getHolderName() {
        return holderName;
    }

    public void setHolderName(String holderName) {
        this.holderName = holderName;
    }

    public String getIdCard() {
        return idCard;
    }

    public void setIdCard(String idCard) {
        this.idCard = idCard;
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

    public String getCertificateType() {
        return certificateType;
    }

    public void setCertificateType(String certificateType) {
        this.certificateType = certificateType;
    }

    public String getValidStart() {
        return validStart;
    }

    public void setValidStart(String validStart) {
        this.validStart = validStart;
    }

    public String getValidEnd() {
        return validEnd;
    }

    public void setValidEnd(String validEnd) {
        this.validEnd = validEnd;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    public String getPauseReason() {
        return pauseReason;
    }

    public void setPauseReason(String pauseReason) {
        this.pauseReason = pauseReason;
    }

    public String getRevokeReason() {
        return revokeReason;
    }

    public void setRevokeReason(String revokeReason) {
        this.revokeReason = revokeReason;
    }

    public String getIssueDate() {
        return issueDate;
    }

    public void setIssueDate(String issueDate) {
        this.issueDate = issueDate;
    }

    public String getIssueAuthority() {
        return issueAuthority;
    }

    public void setIssueAuthority(String issueAuthority) {
        this.issueAuthority = issueAuthority;
    }

    public String getPhoto() {
        return photo;
    }

    public void setPhoto(String photo) {
        this.photo = photo;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        if (!super.equals(o)) return false;
        Certificate that = (Certificate) o;
        return certNo != null && certNo.equals(that.certNo);
    }

    @Override
    public int hashCode() {
        return super.hashCode() * 31 + (certNo != null ? certNo.hashCode() : 0);
    }
}
