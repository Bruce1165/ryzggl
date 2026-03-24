package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.entity.CertificateChange;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

import java.util.List;

/**
 * CertificateChange Repository - 证书变更数据访问层
 */
@Mapper
public interface CertificateChangeRepository extends BaseMapper<CertificateChange> {

    /**
     * Query certificate change list with pagination
     */
    @Select("<script>" +
            "SELECT * FROM CertificateChange" +
            "<where>" +
            "<if test='certificateId != null'>" +
            " AND CERTIFICATEID = #{certificateId}" +
            "</if>" +
            "<if test='certificateCode != null and certificateCode != \"\"'>" +
            " AND CERTIFICATECODE LIKE CONCAT('%', #{certificateCode}, '%')" +
            "</if>" +
            "<if test='changeType != null and changeType != \"\"'>" +
            " AND CHANGETYPE = #{changeType}" +
            "</if>" +
            "</where>" +
            " ORDER BY CHANGEDATE DESC" +
            "</script>")
    IPage<CertificateChange> selectCertificateChangePage(Page<CertificateChange> page,
                                                        @Param("certificateId") Long certificateId,
                                                        @Param("certificateCode") String certificateCode,
                                                        @Param("changeType") String changeType);

    /**
     * Query certificate change by ID
     */
    @Select("SELECT * FROM CertificateChange WHERE ID = #{id}")
    CertificateChange selectById(@Param("id") Long id);

    /**
     * Query certificate changes by certificate ID
     */
    @Select("SELECT * FROM CertificateChange WHERE CERTIFICATEID = #{certificateId} ORDER BY CHANGEDATE DESC")
    List<CertificateChange> selectByCertificateId(@Param("certificateId") Long certificateId);

    /**
     * Query certificate changes by certificate code
     */
    @Select("SELECT * FROM CertificateChange WHERE CERTIFICATECODE = #{certificateCode} ORDER BY CHANGEDATE DESC")
    List<CertificateChange> selectByCertificateCode(@Param("certificateCode") String certificateCode);

    /**
     * Query certificate changes by date range
     */
    @Select("SELECT * FROM CertificateChange WHERE CHANGEDATE >= #{startDate} AND CHANGEDATE <= #{endDate} ORDER BY CHANGEDATE DESC")
    List<CertificateChange> selectByDateRange(@Param("startDate") String startDate,
                                                   @Param("endDate") String endDate);

    /**
     * Get certificate change statistics by type
     */
    @Select("SELECT CHANGETYPE as type, COUNT(*) as count FROM CertificateChange GROUP BY CHANGETYPE")
    List<java.util.Map<String, Object>> getStatisticsByType();
}
