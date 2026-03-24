package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.CertificateOut;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

import java.util.List;

/**
 * CertificateOut Repository - Data Access Layer
 * Maps to: CertificateOutDAL.cs
 *
 * Replaces ADO.NET with MyBatis-Plus for database operations
 */
@Mapper
public interface CertificateOutRepository extends BaseMapper<CertificateOut> {

    /**
     * Find certificate issuance records by certificate ID
     */
    @Select("SELECT * FROM CERTIFICATEOUT WHERE certificateid = #{certificateId} ORDER BY certificateoutid DESC")
    List<CertificateOut> findByCertificateId(@Param("certificateId") Long certificateId);

    /**
     * Find certificate issuance records by worker ID
     */
    @Select("SELECT * FROM CERTIFICATEOUT WHERE workerid = #{workerId} ORDER BY certificateoutid DESC")
    List<CertificateOut> findByWorkerId(@Param("workerId") Long workerId);

    /**
     * Find certificate issuance records by department/unit
     */
    @Select("SELECT * FROM CERTIFICATEOUT WHERE unitcode = #{unitCode} ORDER BY certificateoutid DESC")
    List<CertificateOut> findByUnitCode(@Param("unitCode") String unitCode);

    /**
     * Find by status
     */
    @Select("SELECT * FROM CERTIFICATEOUT WHERE applystatus = #{status} ORDER BY certificateoutid DESC")
    List<CertificateOut> findByStatus(@Param("status") String status);

    /**
     * Find pending certificate issuance records for approval
     */
    @Select("SELECT * FROM CERTIFICATEOUT WHERE applystatus IN ('待确认', '已受理') ORDER BY certificateoutid DESC")
    List<CertificateOut> findPendingForApproval();

    /**
     * Find certificate issuance records by certificate code
     */
    @Select("SELECT * FROM CERTIFICATEOUT WHERE certificatecode = #{certificateCode} ORDER BY certificateoutid DESC")
    List<CertificateOut> findByCertificateCode(@Param("certificateCode") String certificateCode);

    /**
     * Find certificate issuance records by out type
     */
    @Select("SELECT * FROM CERTIFICATEOUT WHERE outtype = #{outType} ORDER BY certificateoutid DESC")
    List<CertificateOut> findByOutType(@Param("outType") String outType);

    /**
     * Find recent issuance records (last N days)
     */
    @Select("SELECT * FROM CERTIFICATEOUT WHERE createtime >= DATEADD(DAY, -#{days}, GETDATE()) ORDER BY certificateoutid DESC")
    List<CertificateOut> findRecent(@Param("days") int days);

    /**
     * Count issuance records by worker
     */
    @Select("SELECT COUNT(*) FROM CERTIFICATEOUT WHERE workerid = #{workerId}")
    int countByWorkerId(@Param("workerId") Long workerId);

    /**
     * Count issuance records by certificate
     */
    @Select("SELECT COUNT(*) FROM CERTIFICATEOUT WHERE certificateid = #{certificateId}")
    int countByCertificateId(@Param("certificateId") Long certificateId);
}
