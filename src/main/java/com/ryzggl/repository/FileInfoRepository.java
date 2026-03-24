package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.FileInfo;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

import java.util.List;

/**
 * FileInfo Repository - Data access for file management
 */
@Mapper
public interface FileInfoRepository extends BaseMapper<FileInfo> {

    /**
     * Find files by business record
     * Maps to: FileInfoDAL.GetFilesByRecordID
     */
    @Select("SELECT * FROM FileInfo WHERE recordid = #{recordId} ORDER BY uploadtime DESC")
    List<FileInfo> findByRecordId(@Param("recordId") Long recordId);

    /**
     * Find files by business entity
     * Maps to: FileInfoDAL.GetFilesByBusinessID
     */
    @Select("SELECT * FROM FileInfo WHERE businessid = #{businessId} ORDER BY uploadtime DESC")
    List<FileInfo> findByBusinessId(@Param("businessId") Long businessId);

    /**
     * Find files by uploader
     * Maps to: FileInfoDAL.GetFilesByUploadPersonID
     */
    @Select("SELECT * FROM FileInfo WHERE uploadpersonid = #{uploadPersonId} ORDER BY uploadtime DESC")
    List<FileInfo> findByUploadPersonId(@Param("uploadPersonId") Long uploadPersonId);

    /**
     * Find files by name (LIKE search)
     */
    @Select("SELECT * FROM FileInfo WHERE filename LIKE CONCAT('%', #{keyword}, '%') ORDER BY uploadtime DESC")
    List<FileInfo> findByFilename(@Param("keyword") String keyword);

    /**
     * Find files by type
     */
    @Select("SELECT * FROM FileInfo WHERE filetype = #{fileType} ORDER BY uploadtime DESC")
    List<FileInfo> findByFileType(@Param("fileType") String fileType);

    /**
     * Count files by business record
     */
    @Select("SELECT COUNT(*) FROM FileInfo WHERE recordid = #{recordId}")
    long countByRecordId(@Param("recordId") Long recordId);

    /**
     * Find by ID
     */
    @Select("SELECT * FROM FileInfo WHERE FILEID = #{fileId}")
    FileInfo getById(@Param("fileId") Long fileId);
}
