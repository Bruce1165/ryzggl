package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;
import com.fasterxml.jackson.annotation.JsonFormat;

/**
 * Qualification Entity
 * 资格证书管理实体类
 *
 * Maps to legacy QualificationMDL.cs
 * Table: Qualification
 *
 * Business Context:
 * - Multi-province qualification system
 * - Supports different regions/provinces
 * - Tracks qualification history and status
 * - Used for worker certification management
 */
@TableName("Qualification")
public class Qualification {

    /**
     * Primary key - Qualification ID
     * 资格ID (主键)
     */
    @TableId(type = IdType.AUTO)
    private Long qualId;

    /**
     * Qualification Code
     * 资格编码（唯一标识）
     */
    private String qualCode;

    /**
     * Qualification Name
     * 资格名称
     */
    private String qualName;

    /**
     * Qualification Type
     * 资格类型
     */
    private String qualType;

    /**
     * Qualification Level
     * 资格等级（如：初级、中级、高级）
     */
    private String qualLevel;

    /**
     * Qualification Category
     * 资格类别（如：建造、造价、监理等）
     */
    private String qualCategory;

    /**
     * Province/Region - Shanxi (ZGZSBH)
     * 陕西省（编码：ZGZSBH）
     */
    private String zGZSBH;

    /**
     * Province/Region - Henan (XM)
     * 河南省（编码：XM）
     */
    private String xm;

    /**
     * Province/Region - Fujian (ZJHM)
     * 福建省（编码：ZJHM）
     */
    private String zJHM;

    /**
     * Province/Region - Shandong (ZJHM)
     * 山东省（编码：ZYLB）
     */
    private String zYLB;

    /**
     * Province/Region - Shanxi (BYSJ)
     * 陕西省（编码：BYSJ，日期型）
     */
    private String bySJ;

    /**
     * Province/Region - Sichuan (SXZY)
     * 四川省（编码：SXZY）
     */
    private String sXZY;

    /**
     * Province/Region - Hunan (ZGXL)
     * 湖南省（编码：ZGXL）
     */
    private String zGXL;

    /**
     * Province/Region - Shandong (ZGXL)
     * 山东省（编码：ZGXL）
     */
    private String zGXL;

    /**
     * Province/Region - QFSJ
     * 其他地区（编码：QFSJ）
     */
    private String qFSJ;

    /**
     * Qualification Description
     * 资格描述
     */
    private String description;

    /**
     * Is Valid
     * 是否有效
     */
    private Boolean isValid;

    /**
     * Issue Date
     * 发证日期
     */
    @JsonFormat(pattern = "yyyy-MM-dd")
    private String issueDate;

    /**
     * Valid From Date
     * 有效期开始日期
     */
    @JsonFormat(pattern = "yyyy-MM-dd")
    private String validFromDate;

    /**
     * Valid To Date
     * 有效期结束日期
     */
    @JsonFormat(pattern = "yyyy-MM-dd")
    private String validToDate;

    /**
     * Issuing Authority
     * 发证机构
     */
    private String issuingAuthority;

    /**
     * Creator ID
     * 创建人
     */
    private Long createPersonId;

    /**
     * Creation Time
     * 创建时间
     */
    @JsonFormat(pattern = "yyyy-MM-dd HH:mm:ss")
    private String createTime;

    /**
     * Modifier ID
     * 修改人
     */
    private Long modifyPersonId;

    /**
     * Modification Time
     * 修改时间
     */
    @JsonFormat(pattern = "yyyy-MM-dd HH:mm:ss")
    private String modifyTime;

    // Getters and Setters
    public Long getQualId() {
        return qualId;
    }

    public void setQualId(Long qualId) {
        this.qualId = qualId;
    }

    public String getQualCode() {
        return qualCode;
    }

    public void setQualCode(String qualCode) {
        this.qualCode = qualCode;
    }

    public String getQualName() {
        return qualName;
    }

    public void setQualName(String qualName) {
        this.qualName = qualName;
    }

    public String getQualType() {
        return qualType;
    }

    public void setQualType(String qualType) {
        this.qualType = qualType;
    }

    public String getQualLevel() {
        return qualLevel;
    }

    public void setQualLevel(String qualLevel) {
        this.qualLevel = qualLevel;
    }

    public String getQualCategory() {
        return qualCategory;
    }

    public void setQualCategory(String qualCategory) {
        this.qualCategory = qualCategory;
    }

    public String getZGZSBH() {
        return zGZSBH;
    }

    public void setZGZSBH(String zGZSBH) {
        this.zGZSBH = zGZSBH;
    }

    public String getXm() {
        return xm;
    }

    public void setXm(String xm) {
        this.xm = xm;
    }

    public String getZJHM() {
        return zJHM;
    }

    public void setZJHM(String zJHM) {
        this.zJHM = zJHM;
    }

    public String getZYLB() {
        return zYLB;
    }

    public void setZYLB(String zYLB) {
        this.zYLB = zYLB;
    }

    public String getBySJ() {
        return bySJ;
    }

    public void setBySJ(String bySJ) {
        this.bySJ = bySJ;
    }

    public String getSXZY() {
        return sXZY;
    }

    public void setSXZY(String sXZY) {
        this.sXZY = sXZY;
    }

    public String getZGXL() {
        return zGXL;
    }

    public void setZGXL(String zGXL) {
        this.zGXL = zGXL;
    }

    public String getQFSJ() {
        return qFSJ;
    }

    public void setQFSJ(String qFSJ) {
        this.qFSJ = qFSJ;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public Boolean getIsValid() {
        return isValid;
    }

    public void setIsValid(Boolean isValid) {
        this.isValid = isValid;
    }

    public String getIssueDate() {
        return issueDate;
    }

    public void setIssueDate(String issueDate) {
        this.issueDate = issueDate;
    }

    public String getValidFromDate() {
        return validFromDate;
    }

    public void setValidFromDate(String validFromDate) {
        this.validFromDate = validFromDate;
    }

    public String getValidToDate() {
        return validToDate;
    }

    public void setValidToDate(String validToDate) {
        this.validToDate = validToDate;
    }

    public String getIssuingAuthority() {
        return issuingAuthority;
    }

    public void setIssuingAuthority(String issuingAuthority) {
        this.issuingAuthority = issuingAuthority;
    }

    public Long getCreatePersonId() {
        return createPersonId;
    }

    public void setCreatePersonId(Long createPersonId) {
        this.createPersonId = createPersonId;
    }

    public String getCreateTime() {
        return createTime;
    }

    public void setCreateTime(String createTime) {
        this.createTime = createTime;
    }

    public Long getModifyPersonId() {
        return modifyPersonId;
    }

    public void setModifyPersonId(Long modifyPersonId) {
        this.modifyPersonId = modifyPersonId;
    }

    public String getModifyTime() {
        return modifyTime;
    }

    public void setModifyTime(String modifyTime) {
        this.modifyTime = modifyTime;
    }
}
