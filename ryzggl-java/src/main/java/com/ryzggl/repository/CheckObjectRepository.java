package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.CheckObject;
import org.apache.ibatis.annotations.*;

import java.util.List;

/**
 * CheckObject Repository
 * 核查对象数据访问层
 */
public interface CheckObjectRepository extends BaseMapper<CheckObject> {

    /**
     * Get by ID
     */
    @Select("SELECT * FROM CheckObject WHERE CheckID = #{checkId}")
    CheckObject getById(@Param("checkId") String checkId);

    /**
     * Get all check objects
     */
    @Select("SELECT * FROM CheckObject ORDER BY CheckYear DESC, PSN_RegisterNo, CheckID")
    List<CheckObject> getAll();

    /**
     * Get by check year
     */
    @Select("SELECT * FROM CheckObject WHERE CheckYear = #{checkYear} ORDER BY PSN_RegisterNo")
    List<CheckObject> getByCheckYear(@Param("checkYear") Integer checkYear);

    /**
     * Get by register number
     */
    @Select("SELECT * FROM CheckObject WHERE PSN_RegisterNo = #{psnRegisterNo} ORDER BY CheckYear DESC")
    List<CheckObject> getByPsnRegisterNo(@Param("psnRegisterNo") String psnRegisterNo);

    /**
     * Get by certificate number
     */
    @Select("SELECT * FROM CheckObject WHERE PSN_CertificateNO = #{psnCertificateNo} ORDER BY CheckYear DESC")
    List<CheckObject> getByPsnCertificateNo(@Param("psnCertificateNo") String psnCertificateNo);

    /**
     * Get by person name
     */
    @Select("SELECT * FROM CheckObject WHERE PSN_Name LIKE CONCAT('%', #{psnName}, '%') ORDER BY CheckYear DESC")
    List<CheckObject> getByPsnName(@Param("psnName") String psnName);

    /**
     * Get by apply type
     */
    @Select("SELECT * FROM CheckObject WHERE ApplyType = #{applyType} ORDER BY CheckYear DESC")
    List<CheckObject> getByApplyType(@Param("applyType") String applyType);

    /**
     * Search by keyword
     */
    @Select("SELECT * FROM CheckObject WHERE PSN_Name LIKE CONCAT('%', #{keyword}, '%') OR PSN_RegisterNo LIKE CONCAT('%', #{keyword}, '%') OR ENT_Name LIKE CONCAT('%', #{keyword}, '%') ORDER BY CheckYear DESC")
    List<CheckObject> search(@Param("keyword") String keyword);

    /**
     * Insert check object
     */
    @Insert("INSERT INTO CheckObject(CheckID, CheckYear, PSN_RegisterNo, PostTypeName, PSN_Name, PSN_CertificateNO, ENT_Name, ProfessionWithValid, ApplyType, ApplyTime, NoticeDate) VALUES (#{checkId}, #{checkYear}, #{psnRegisterNo}, #{postTypeName}, #{psnName}, #{psnCertificateNo}, #{entName}, #{professionWithValid}, #{applyType}, #{applyTime}, #{noticeDate})")
    int insertCheckObject(CheckObject checkObject);

    /**
     * Update check object
     */
    @Update("UPDATE CheckObject SET CheckYear = #{checkYear}, PSN_RegisterNo = #{psnRegisterNo}, PostTypeName = #{postTypeName}, PSN_Name = #{psnName}, PSN_CertificateNO = #{psnCertificateNo}, ENT_Name = #{entName}, ProfessionWithValid = #{professionWithValid}, ApplyType = #{applyType}, ApplyTime = #{applyTime}, NoticeDate = #{noticeDate} WHERE CheckID = #{checkId}")
    int updateCheckObject(CheckObject checkObject);

    /**
     * Delete check object
     */
    @Delete("DELETE FROM CheckObject WHERE CheckID = #{checkId}")
    int deleteCheckObject(@Param("checkId") String checkId);

    /**
     * Count check objects
     */
    @Select("SELECT COUNT(*) FROM CheckObject")
    int count();
}
