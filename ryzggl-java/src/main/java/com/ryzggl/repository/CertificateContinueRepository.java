package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.entity.CertificateContinue;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

import java.util.List;

/**
 * CertificateContinue Repository - 证书延续数据访问层
 */
@Mapper
public interface CertificateContinueRepository extends BaseMapper<CertificateContinue> {

    /**
     * Query certificate continue list with pagination
     */
    @Select("<script>" +
            "SELECT * FROM CertificateContinue" +
            "<where>" +
            "<if test='certificateId != null'>" +
            " AND CERTIFICATEID = #{certificateId}" +
            "</if>" +
            "<if test='certificateCode != null and certificateCode != \"\"'>" +
            " AND CERTIFICATECODE LIKE CONCAT('%', #{certificateCode}, '%')" +
            "</if>" +
            "<if test='continueStatus != null and continueStatus != \"\"'>" +
            " AND CONTINUESTATUS = #{continueStatus}" +
            "</if>" +
            "<if test='continueType != null and continueType != \"\"'>" +
            " AND CONTINUETYPE = #{continueType}" +
            "</if>" +
            "</where>" +
            " ORDER BY CONTINUEDATE DESC" +
            "</script>")
    IPage<CertificateContinue> selectCertificateContinuePage(Page<CertificateContinue> page,
                                                          @Param("certificateId") Long certificateId,
                                                          @Param("certificateCode") String certificateCode,
                                                          @Param("continueStatus") String continueStatus,
                                                          @Param("continueType") String continueType);

    /**
     * Query certificate continue by ID
     */
    @Select("SELECT * FROM CertificateContinue WHERE ID = #{id}")
    CertificateContinue selectById(@Param("id") Long id);

    /**
     * Query certificate continues by certificate ID
     */
    @Select("SELECT * FROM CertificateContinue WHERE CERTIFICATEID = #{certificateId} ORDER BY CONTINUEDATE DESC")
    List<CertificateContinue> selectByCertificateId(@Param("certificateId") Long certificateId);

    /**
     * Query certificate continues by certificate code
     */
    @Select("SELECT * FROM CertificateContinue WHERE CERTIFICATECODE = #{certificateCode} ORDER BY CONTINUEDATE DESC")
    List<CertificateContinue> selectByCertificateCode(@Param("certificateCode") String certificateCode);

    /**
     * Query certificate continues by status
     */
    @Select("SELECT * FROM CertificateContinue WHERE CONTINUESTATUS = #{status} ORDER BY CONTINUEDATE DESC")
    List<CertificateContinue> selectByStatus(@Param("status") String continueStatus);

    /**
     * Query expiring certificate continues
     */
    @Select("SELECT * FROM CertificateContinue WHERE CONTINUESTATUS IN ('已延续', '未延续') AND CONTINUEENDDATE >= GETDATE() AND CONTINUEENDDATE <= DATEADD(MONTH, 3, GETDATE()) ORDER BY CONTINUEENDDATE ASC")
    List<CertificateContinue> selectExpiringSoon();

    /**
     * Query certificate continue statistics by type
     */
    @Select("SELECT CONTINUETYPE, COUNT(*) as count FROM CertificateContinue GROUP BY CONTINUETYPE")
    List<java.util.Map<String, Object>> getStatisticsByType();
}
