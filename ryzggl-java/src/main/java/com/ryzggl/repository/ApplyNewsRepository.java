package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.ApplyNews;
import org.apache.ibatis.annotations.*;

import java.util.List;

/**
 * ApplyNews Repository
 * 申请新闻数据访问层
 */
public interface ApplyNewsRepository extends BaseMapper<ApplyNews> {

    /**
     * Get by ID
     */
    @Select("SELECT * FROM ApplyNews WHERE [ID] = #{id}")
    ApplyNews getById(@Param("id") String id);

    /**
     * Get all apply news
     */
    @Select("SELECT * FROM ApplyNews ORDER BY [ID]")
    List<ApplyNews> getAll();

    /**
     * Get by organization code (unchecked only)
     */
    @Select("SELECT * FROM ApplyNews WHERE ENT_OrganizationsCode = #{entOrganizationsCode} AND SFCK = 0 ORDER BY [ID]")
    List<ApplyNews> getByOrganizationCode(@Param("entOrganizationsCode") String entOrganizationsCode);

    /**
     * Get by person name
     */
    @Select("SELECT * FROM ApplyNews WHERE PSN_Name LIKE CONCAT('%', #{psnName}, '%') ORDER BY [ID]")
    List<ApplyNews> getByPsnName(@Param("psnName") String psnName);

    /**
     * Get by certificate no
     */
    @Select("SELECT * FROM ApplyNews WHERE PSN_CertificateNO = #{psnCertificateNo} ORDER BY [ID]")
    List<ApplyNews> getByPsnCertificateNo(@Param("psnCertificateNo") String psnCertificateNo);

    /**
     * Get by register no
     */
    @Select("SELECT * FROM ApplyNews WHERE PSN_RegisterNo = #{psnRegisterNo} ORDER BY [ID]")
    List<ApplyNews> getByPsnRegisterNo(@Param("psnRegisterNo") String psnRegisterNo);

    /**
     * Get by apply type
     */
    @Select("SELECT * FROM ApplyNews WHERE ApplyType = #{applyType} ORDER BY [ID]")
    List<ApplyNews> getByApplyType(@Param("applyType") String applyType);

    /**
     * Get by SFCK status
     */
    @Select("SELECT * FROM ApplyNews WHERE SFCK = #{sfck} ORDER BY [ID]")
    List<ApplyNews> getBySfck(@Param("sfck") Boolean sfck);

    /**
     * Get unchecked items
     */
    @Select("SELECT * FROM ApplyNews WHERE SFCK = 0 ORDER BY [ID]")
    List<ApplyNews> getUnchecked();

    /**
     * Get checked items
     */
    @Select("SELECT * FROM ApplyNews WHERE SFCK = 1 ORDER BY [ID]")
    List<ApplyNews> getChecked();

    /**
     * Search by keyword
     */
    @Select("SELECT * FROM ApplyNews WHERE PSN_Name LIKE CONCAT('%', #{keyword}, '%') OR PSN_CertificateNO LIKE CONCAT('%', #{keyword}, '%') OR PSN_RegisterNo LIKE CONCAT('%', #{keyword}, '%') OR ENT_OrganizationsCode LIKE CONCAT('%', #{keyword}, '%') ORDER BY [ID]")
    List<ApplyNews> search(@Param("keyword") String keyword);

    /**
     * Insert apply news
     */
    @Insert("INSERT INTO ApplyNews([ID], PSN_Name, PSN_CertificateNO, PSN_RegisterNo, ApplyType, SFCK, ENT_OrganizationsCode) VALUES (#{id}, #{psnName}, #{psnCertificateNo}, #{psnRegisterNo}, #{applyType}, #{sfck}, #{entOrganizationsCode})")
    int insertApplyNews(ApplyNews applyNews);

    /**
     * Update apply news
     */
    @Update("UPDATE ApplyNews SET PSN_Name = #{psnName}, PSN_CertificateNO = #{psnCertificateNo}, PSN_RegisterNo = #{psnRegisterNo}, ApplyType = #{applyType}, SFCK = #{sfck}, ENT_OrganizationsCode = #{entOrganizationsCode} WHERE [ID] = #{id}")
    int updateApplyNews(ApplyNews applyNews);

    /**
     * Delete apply news
     */
    @Delete("DELETE FROM ApplyNews WHERE [ID] = #{id}")
    int deleteApplyNews(@Param("id") String id);

    /**
     * Count apply news
     */
    @Select("SELECT COUNT(*) FROM ApplyNews")
    int count();

    /**
     * Count by SFCK status
     */
    @Select("SELECT COUNT(*) FROM ApplyNews WHERE SFCK = #{sfck}")
    int countBySfck(@Param("sfck") Boolean sfck);
}
