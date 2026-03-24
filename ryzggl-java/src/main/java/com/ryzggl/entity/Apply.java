package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableName;
import com.ryzggl.common.BaseEntity;

/**
 * Apply Entity - 申请表
 */
@TableName("Apply")
public class Apply extends BaseEntity {

    /**
     * 申请编号
     */
    @TableField("APPLYNO")
    private String applyNo;

    /**
     * 申请人ID
     */
    @TableField("WORKERID")
    private Long workerId;

    /**
     * 申请人姓名
     */
    @TableField("APPLICANTNAME")
    private String applicantName;

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
     * 申请类型（首次申请、变更申请、延续申请、注销申请）
     */
    @TableField("APPLYTYPE")
    private String applyType;

    /**
     * 申请状态（未填写、待确认、已受理、已申报、已驳回）
     */
    @TableField("APPLYSTATUS")
    private String applyStatus;

    /**
     * 审批意见
     */
    @TableField("APPROVALOPINION")
    private String approvalOpinion;

    /**
     * 审批人
     */
    @TableField("APPROVER")
    private String approver;

    /**
     * 审批时间
     */
    @TableField("APPROVALTIME")
    private String approvalTime;

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
    public String getApplyNo() {
        return applyNo;
    }

    public void setApplyNo(String applyNo) {
        this.applyNo = applyNo;
    }

    public Long getWorkerId() {
        return workerId;
    }

    public void setWorkerId(Long workerId) {
        this.workerId = workerId;
    }

    public String getApplicantName() {
        return applicantName;
    }

    public void setApplicantName(String applicantName) {
        this.applicantName = applicantName;
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

    public String getApplyType() {
        return applyType;
    }

    public void setApplyType(String applyType) {
        this.applyType = applyType;
    }

    public String getApplyStatus() {
        return applyStatus;
    }

    public void setApplyStatus(String applyStatus) {
        this.applyStatus = applyStatus;
    }

    public String getApprovalOpinion() {
        return approvalOpinion;
    }

    public void setApprovalOpinion(String approvalOpinion) {
        this.approvalOpinion = approvalOpinion;
    }

    public String getApprover() {
        return approver;
    }

    public void setApprover(String approver) {
        this.approver = approver;
    }

    public String getApprovalTime() {
        return approvalTime;
    }

    public void setApprovalTime(String approvalTime) {
        this.approvalTime = approvalTime;
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
        Apply apply = (Apply) o;
        return applyNo != null && applyNo.equals(apply.applyNo);
    }

    @Override
    public int hashCode() {
        return super.hashCode() * 31 + (applyNo != null ? applyNo.hashCode() : 0);
    }
}
