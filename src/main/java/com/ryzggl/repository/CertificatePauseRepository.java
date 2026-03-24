package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.CertificatePause;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

import java.util.List;

/**
 * CertificatePause Repository - Data Access Layer
 * Maps to: CertificatePauseDAL.cs
 *
 * Replaces ADO.NET with MyBatis-Plus for database operations
 */
@Mapper
public interface CertificatePauseRepository extends BaseMapper<CertificatePause> {

    /**
     * Find pause records by certificate ID
     */
    @Select("SELECT * FROM CERTIFICATEPAUSE WHERE certificateid = #{certificateId} ORDER BY certificatepauseid DESC")
    List<CertificatePause> findByCertificateId(@Param("certificateId") Long certificateId);

    /**
     * Find pause records by worker ID
     */
    @Select("SELECT * FROM CERTIFICATEPAUSE WHERE workerid = #{workerId} ORDER BY certificatepauseid DESC")
    List<CertificatePause> findByWorkerId(@Param("workerId") Long workerId);

    /**
     * Find pause records by department/unit
     */
    @Select("SELECT * FROM CERTIFICATEPAUSE WHERE unitcode = #{unitCode} ORDER BY certificatepauseid DESC")
    List<CertificatePause> findByUnitCode(@Param("unitCode") String unitCode);

    /**
     * Find by pause status
     */
    @Select("SELECT * FROM CERTIFICATEPAUSE WHERE pausestatus = #{pauseStatus} ORDER BY certificatepauseid DESC")
    List<CertificatePause> findByPauseStatus(@Param("pauseStatus") String pauseStatus);

    /**
     * Find currently paused certificates
     */
    @Select("SELECT * FROM CERTIFICATEPAUSE WHERE pausestatus = '已暂停' ORDER BY certificatepauseid DESC")
    List<CertificatePause> findPausedCertificates();

    /**
     * Find pause records by certificate code
     */
    @Select("SELECT * FROM CERTIFICATEPAUSE WHERE certificatecode = #{certificateCode} ORDER BY certificatepauseid DESC")
    List<CertificatePause> findByCertificateCode(@Param("certificateCode") String certificateCode);

    /**
     * Find recent pause records (last N days)
     */
    @Select("SELECT * FROM CERTIFICATEPAUSE WHERE createtime >= DATEADD(DAY, -#{days}, GETDATE()) ORDER BY certificatepauseid DESC")
    List<CertificatePause> findRecent(@Param("days") int days);

    /**
     * Count pause records by worker
     */
    @Select("SELECT COUNT(*) FROM CERTIFICATEPAUSE WHERE workerid = #{workerId}")
    int countByWorkerId(@Param("workerId") Long workerId);

    /**
     * Count pause records by certificate
     */
    @Select("SELECT COUNT(*) FROM CERTIFICATEPAUSE WHERE certificateid = #{certificateId}")
    int countByCertificateId(@Param("certificateId") Long certificateId);
}
