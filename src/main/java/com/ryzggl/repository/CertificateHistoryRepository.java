package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.CertificateHistory;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

import java.util.List;

/**
 * CertificateHistory Repository - Data Access Layer
 * Maps to: CertificateHistoryDAL.cs
 *
 * Replaces ADO.NET with MyBatis-Plus for database operations
 */
@Mapper
public interface CertificateHistoryRepository extends BaseMapper<CertificateHistory> {

    /**
     * Find history records by certificate ID
     */
    @Select("SELECT * FROM CERTIFICATEHISTORY WHERE certificateid = #{certificateId} ORDER BY changedate DESC")
    List<CertificateHistory> findByCertificateId(@Param("certificateId") Long certificateId);

    /**
     * Find history records by worker ID
     */
    @Select("SELECT * FROM CERTIFICATEHISTORY WHERE workerid = #{workerId} ORDER BY changedate DESC")
    List<CertificateHistory> findByWorkerId(@Param("workerId") Long workerId);

    /**
     * Find history records by department/unit
     */
    @Select("SELECT * FROM CERTIFICATEHISTORY WHERE unitcode = #{unitCode} ORDER BY changedate DESC")
    List<CertificateHistory> findByUnitCode(@Param("unitCode") String unitCode);

    /**
     * Find by change type
     */
    @Select("SELECT * FROM CERTIFICATEHISTORY WHERE changetype = #{changeType} ORDER BY changedate DESC")
    List<CertificateHistory> findByChangeType(@Param("changeType") String changeType);

    /**
     * Find history records by certificate code
     */
    @Select("SELECT * FROM CERTIFICATEHISTORY WHERE certificatecode = #{certificateCode} ORDER BY changedate DESC")
    List<CertificateHistory> findByCertificateCode(@Param("certificateCode") String certificateCode);

    /**
     * Find recent history records (last N days)
     */
    @Select("SELECT * FROM CERTIFICATEHISTORY WHERE changedate >= DATEADD(DAY, -#{days}, GETDATE()) ORDER BY changedate DESC")
    List<CertificateHistory> findRecent(@Param("days") int days);

    /**
     * Count history records by worker
     */
    @Select("SELECT COUNT(*) FROM CERTIFICATEHISTORY WHERE workerid = #{workerId}")
    int countByWorkerId(@Param("workerId") Long workerId);

    /**
     * Count history records by certificate
     */
    @Select("SELECT COUNT(*) FROM CERTIFICATEHISTORY WHERE certificateid = #{certificateId}")
    int countByCertificateId(@Param("certificateId") Long certificateId);
}
