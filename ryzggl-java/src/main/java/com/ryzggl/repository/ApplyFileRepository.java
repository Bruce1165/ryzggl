package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.ApplyFile;
import org.apache.ibatis.annotations.*;

import java.util.List;

/**
 * ApplyFile Repository
 * 申请文件数据访问层
 */
public interface ApplyFileRepository extends BaseMapper<ApplyFile> {

    /**
     * Get by ID
     */
    @Select("SELECT * FROM ApplyFile WHERE FileID = #{fileId}")
    ApplyFile getById(@Param("fileId") String fileId);

    /**
     * Get all apply files
     */
    @Select("SELECT * FROM ApplyFile ORDER BY FileID")
    List<ApplyFile> getAll();

    /**
     * Get by apply ID
     */
    @Select("SELECT * FROM ApplyFile WHERE ApplyID = #{applyId} ORDER BY FileID")
    List<ApplyFile> getByApplyId(@Param("applyId") String applyId);

    /**
     * Get by check result
     */
    @Select("SELECT * FROM ApplyFile WHERE CheckResult = #{checkResult} ORDER BY FileID")
    List<ApplyFile> getByCheckResult(@Param("checkResult") Integer checkResult);

    /**
     * Search by keyword
     */
    @Select("SELECT * FROM ApplyFile WHERE CheckDesc LIKE CONCAT('%', #{keyword}, '%') ORDER BY FileID")
    List<ApplyFile> search(@Param("keyword") String keyword);

    /**
     * Insert apply file
     */
    @Insert("INSERT INTO ApplyFile(FileID, ApplyID, CheckResult, CheckDesc) VALUES (#{fileId}, #{applyId}, #{checkResult}, #{checkDesc})")
    int insertApplyFile(ApplyFile applyFile);

    /**
     * Update apply file
     */
    @Update("UPDATE ApplyFile SET ApplyID = #{applyId}, CheckResult = #{checkResult}, CheckDesc = #{checkDesc} WHERE FileID = #{fileId}")
    int updateApplyFile(ApplyFile applyFile);

    /**
     * Delete apply file
     */
    @Delete("DELETE FROM ApplyFile WHERE FileID = #{fileId} AND ApplyID = #{applyId}")
    int deleteApplyFile(@Param("fileId") String fileId, @Param("applyId") String applyId);

    /**
     * Delete by apply ID
     */
    @Delete("DELETE FROM ApplyFile WHERE ApplyID = #{applyId}")
    int deleteByApplyId(@Param("applyId") String applyId);

    /**
     * Count apply files
     */
    @Select("SELECT COUNT(*) FROM ApplyFile")
    int count();

    /**
     * Count by apply ID
     */
    @Select("SELECT COUNT(*) FROM ApplyFile WHERE ApplyID = #{applyId}")
    int countByApplyId(@Param("applyId") String applyId);

    /**
     * Count by check result
     */
    @Select("SELECT COUNT(*) FROM ApplyFile WHERE CheckResult = #{checkResult}")
    int countByCheckResult(@Param("checkResult") Integer checkResult);
}
