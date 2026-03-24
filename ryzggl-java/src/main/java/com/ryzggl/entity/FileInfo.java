package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableName;
import com.ryzggl.common.BaseEntity;

/**
 * FileInfo Entity - 文件信息表
 */
@TableName("FileInfo")
public class FileInfo extends BaseEntity {

    /**
     * 文件名
     */
    @TableField("FILENAME")
    private String fileName;

    /**
     * 文件路径
     */
    @TableField("FILEPATH")
    private String filePath;

    /**
     * 文件大小（字节）
     */
    @TableField("FILESIZE")
    private Long fileSize;

    /**
     * 文件类型（image、document、spreadsheet、other）
     */
    @TableField("FILETYPE")
    private String fileType;

    /**
     * 文件扩展名
     */
    @TableField("EXTENSION")
    private String extension;

    /**
     * 上传人
     */
    @TableField("UPLOADER")
    private String uploader;

    /**
     * 关联业务类型
     */
    @TableField("BUSINESSTYPE")
    private String businessType;

    /**
     * 关联业务ID
     */
    @TableField("BUSINESSID")
    private Long businessId;

    /**
     * 下载次数
     */
    @TableField("DOWNLOADCOUNT")
    private Integer downloadCount;

    /**
     * 文件状态（正常、停用）
     */
    @TableField("STATUS")
    private String status;

    // Getters and Setters
    public String getFileName() {
        return fileName;
    }

    public void setFileName(String fileName) {
        this.fileName = fileName;
    }

    public String getFilePath() {
        return filePath;
    }

    public void setFilePath(String filePath) {
        this.filePath = filePath;
    }

    public Long getFileSize() {
        return fileSize;
    }

    public void setFileSize(Long fileSize) {
        this.fileSize = fileSize;
    }

    public String getFileType() {
        return fileType;
    }

    public void setFileType(String fileType) {
        this.fileType = fileType;
    }

    public String getExtension() {
        return extension;
    }

    public void setExtension(String extension) {
        this.extension = extension;
    }

    public String getUploader() {
        return uploader;
    }

    public void setUploader(String uploader) {
        this.uploader = uploader;
    }

    public String getBusinessType() {
        return businessType;
    }

    public void setBusinessType(String businessType) {
        this.businessType = businessType;
    }

    public Long getBusinessId() {
        return businessId;
    }

    public void setBusinessId(Long businessId) {
        this.businessId = businessId;
    }

    public Integer getDownloadCount() {
        return downloadCount;
    }

    public void setDownloadCount(Integer downloadCount) {
        this.downloadCount = downloadCount;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        if (!super.equals(o)) return false;
        FileInfo fileInfo = (FileInfo) o;
        return fileName != null && fileName.equals(fileInfo.fileName);
    }

    @Override
    public int hashCode() {
        return super.hashCode() * 31 + (fileName != null ? fileName.hashCode() : 0);
    }
}
