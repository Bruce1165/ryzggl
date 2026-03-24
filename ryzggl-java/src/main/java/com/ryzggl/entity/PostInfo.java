package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;

import java.math.BigDecimal;

/**
 * PostInfo Entity
 * 岗位信息实体类
 *
 * Maps to legacy PostInfoDAL.cs and PostInfoOB.cs
 * Table: PostInfo
 *
 * Business Context:
 * - Job post category management system
 * - Hierarchical post structure (parent-child via UpPostID)
 * - Certificate number generation with format rules
 * - Exam fee tracking per post
 * - Year-based serial number tracking
 *
 * Certificate Number Format Rules:
 * - "Parent" - Use parent post's format
 * - "Brather{ID}" - Use sibling post's format
 * - "Y,N" - Constant string
 * - "Y,{n}" - Year with n digits
 * - "N,{n}" - Incremental number with n digits
 * - "YN,{n}" - Year-based incremental with n digits
 * - "YPN,{n}" - Year-based incremental by parent with n digits
 * - "Level" - Skill level code (0=普工, 5=初级, 4=中级, 3=高级, 2=技师, 1=高级技师)
 * - "TrainCode" - Training unit code
 * - "TLPYN,{n}" - Training unit + skill level + post year-based serial
 * - "TrainCodePYN,{n}" - Training unit + post type year-based serial
 */
@TableName("PostInfo")
public class PostInfo {

    /**
     * Primary key - Post ID
     * 岗位ID (主键)
     */
    @TableId(type = IdType.AUTO)
    private Integer postId;

    /**
     * Post Type
     * 岗位类型 (1=专业, 2=工种, 3=技能等级)
     */
    private String postType;

    /**
     * Post Name
     * 岗位名称
     */
    private String postName;

    /**
     * Parent Post ID
     * 上级岗位ID (形成层级结构)
     */
    private Integer upPostId;

    /**
     * Exam Fee
     * 考试费用
     */
    private BigDecimal examFee;

    /**
     * Current Number
     * 当前流水号 (用于证书编号生成)
     */
    private Long currentNumber;

    /**
     * Code Year
     * 编码年度 (用于证书编号生成)
     */
    private Integer codeYear;

    /**
     * Code Format
     * 证书编号格式规则
     *
     * Examples:
     * - "京|Y,2|N,7" - 常量"京" + 2位年份 + 7位流水号
     * - "京|Y,2|YN,7" - 常量"京" + 2位年份 + 本年度7位流水号
     * - "Level" - 技能等级编码
     * - "Parent" - 使用上级岗位格式
     * - "Brather101" - 使用兄弟岗位(101)格式
     */
    private String codeFormat;

    // Getters and Setters
    public Integer getPostId() {
        return postId;
    }

    public void setPostId(Integer postId) {
        this.postId = postId;
    }

    public String getPostType() {
        return postType;
    }

    public void setPostType(String postType) {
        this.postType = postType;
    }

    public String getPostName() {
        return postName;
    }

    public void setPostName(String postName) {
        this.postName = postName;
    }

    public Integer getUpPostId() {
        return upPostId;
    }

    public void setUpPostId(Integer upPostId) {
        this.upPostId = upPostId;
    }

    public BigDecimal getExamFee() {
        return examFee;
    }

    public void setExamFee(BigDecimal examFee) {
        this.examFee = examFee;
    }

    public Long getCurrentNumber() {
        return currentNumber;
    }

    public void setCurrentNumber(Long currentNumber) {
        this.currentNumber = currentNumber;
    }

    public Integer getCodeYear() {
        return codeYear;
    }

    public void setCodeYear(Integer codeYear) {
        this.codeYear = codeYear;
    }

    public String getCodeFormat() {
        return codeFormat;
    }

    public void setCodeFormat(String codeFormat) {
        this.codeFormat = codeFormat;
    }
}
