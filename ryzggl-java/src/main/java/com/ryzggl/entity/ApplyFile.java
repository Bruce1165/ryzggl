package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;

/**
 * ApplyFile Entity
 * 申请文件实体类
 *
 * Maps to legacy ApplyFileMDL.cs and ApplyFileDAL.cs
 * Table: ApplyFile
 *
 * Business Context:
 * - Manages file attachments for applications
 * - Links application files with FileInfo
 * - Tracks check results and descriptions
 * - Supports file type categorization for sorting
 * - Links to certificate files for copying
 */
@TableName("ApplyFile")
public class ApplyFile {

    /**
     * Primary key - FileID
     * 文件ID (主键)
     */
    @TableId
    private String fileId;

    /**
     * Apply ID
     * 申请ID
     */
    private String applyId;

    /**
     * Check Result
     * 审核结果
     */
    private Integer checkResult;

    /**
     * Check Description
     * 审核描述
     */
    private String checkDesc;

    // Getters and Setters
    public String getFileId() {
        return fileId;
    }

    public void setFileId(String fileId) {
        this.fileId = fileId;
    }

    public String getApplyId() {
        return applyId;
    }

    public void setApplyId(String applyId) {
        this.applyId = applyId;
    }

    public Integer getCheckResult() {
        return checkResult;
    }

    public void setCheckResult(Integer checkResult) {
        this.checkResult = checkResult;
    }

    public String getCheckDesc() {
        return checkDesc;
    }

    public void setCheckDesc(String checkDesc) {
        this.checkDesc = checkDesc;
    }
}
