package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.CertificateContinue;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

import java.util.List;

/**
 * CertificateContinue Repository - Data Access Layer
 * Maps to: CertificateContinueDAL.cs
 *
 * Replaces ADO.NET with MyBatis-Plus for database operations
 */
@Mapper
public interface CertificateContinueRepository extends BaseMapper<CertificateContinue> {

    /**
     * Find certificate continuation records by certificate ID
     */
    @Select("SELECT * FROM CERTIFICATECONTINUE WHERE certificateid = #{certificateId} ORDER BY certificatecontinueid DESC")
    List<CertificateContinue> findByCertificateId(@Param("certificateId") Long certificateId);

    /**
     * Find certificate continuation records by worker ID
     */
    @Select("SELECT * FROM CERTIFICATECONTINUE WHERE workerid = #{workerId} ORDER BY certificatecontinueid DESC")
    List<CertificateContinue> findByWorkerId(@Param("workerId") Long workerId);

    /**
     * Find certificate continuation records by department/unit
     */
    @Select("SELECT * FROM CERTIFICATECONTINUE WHERE unitcode = #{unitCode} ORDER BY certificatecontinueid DESC")
    List<CertificateContinue> findByUnitCode(@Param("unitCode") String unitCode);

    /**
     * Find by status
     */
    @Select("SELECT * FROM CERTIFICATECONTINUE WHERE applystatus = #{status} ORDER BY certificatecontinueid DESC")
    List<CertificateContinue> findByStatus(@Param("status") String status);

    /**
     * Find pending certificate continuation records for approval
     */
    @Select("SELECT * FROM CERTIFICATECONTINUE WHERE applystatus IN ('待确认', '已受理') ORDER BY certificatecontinueid DESC")
    List<CertificateContinue> findPendingForApproval();

    /**
     * Find certificate continuation records by certificate code
     */
    @Select("SELECT * FROM CERTIFICATECONTINUE WHERE certificatecode = #{certificateCode} ORDER BY certificatecontinueid DESC")
    List<CertificateContinue> findByCertificateCode(@Param("certificateCode") String certificateCode);

    /**
     * Find certificate continuation records by type
     */
    @Select("SELECT * FROM CERTIFICATECONTINUE WHERE continuetype = #{continueType} ORDER BY certificatecontinueid DESC")
    List<CertificateContinue> findByContinueType(@Param("continueType") String continueType);

    /**
     * Find recent continuation records (last N days)
     */
    @Select("SELECT * FROM CERTIFICATECONTINUE WHERE createtime >= DATEADD(DAY, -#{days}, GETDATE()) ORDER BY certificatecontinueid DESC")
    List<CertificateContinue> findRecent(@Param("days") int days);

    /**
     * Count continuation records by worker
     */
    @Select("SELECT COUNT(*) FROM CERTIFICATECONTINUE WHERE workerid = #{workerId}")
    int countByWorkerId(@Param("workerId") Long workerId);

    /**
     * Count continuation records by certificate
     */
    @Select("SELECT COUNT(*) FROM CERTIFICATECONTINUE WHERE certificateid = #{certificateId}")
    int countByCertificateId(@Param("certificateId") Long certificateId);
}
