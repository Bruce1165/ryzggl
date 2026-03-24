package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.CertificateMerge;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

import java.util.List;

/**
 * CertificateMerge Repository - Data Access Layer
 * Maps to: CertificateMergeDAL.cs
 *
 * Replaces ADO.NET with MyBatis-Plus for database operations
 */
@Mapper
public interface CertificateMergeRepository extends BaseMapper<CertificateMerge> {

    /**
     * Find merge records by certificate ID
     */
    @Select("SELECT * FROM CERTIFICATEGEMERGE WHERE certificateid = #{certificateId} ORDER BY certificatemergeid DESC")
    List<CertificateMerge> findByCertificateId(@Param("certificateId") Long certificateId);

    /**
     * Find merge records by worker ID
     */
    @Select("SELECT * FROM CERTIFICATEGEMERGE WHERE workerid = #{workerId} ORDER BY certificatemergeid DESC")
    List<CertificateMerge> findByWorkerId(@Param("workerId") Long workerId);

    /**
     * Find merge records by department/unit
     */
    @Select("SELECT * FROM CERTIFICATEGEMERGE WHERE unitcode = #{unitCode} ORDER BY certificatemergeid DESC")
    List<CertificateMerge> findByUnitCode(@Param("unitCode") String unitCode);

    /**
     * Find by merge type
     */
    @Select("SELECT * FROM CERTIFICATEGEMERGE WHERE mergereason = #{mergeReason} ORDER BY certificatemergeid DESC")
    List<CertificateMerge> findByMergeReason(@Param("mergeReason") String mergeReason);

    /**
     * Find by status
     */
    @Select("SELECT * FROM CERTIFICATEGEMERGE WHERE applystatus = #{applyStatus} ORDER BY certificatemergeid DESC")
    List<CertificateMerge> findByStatus(@Param("applyStatus") String applyStatus);

    /**
     * Find pending merge records for approval
     */
    @Select("SELECT * FROM CERTIFICATEGEMERGE WHERE applystatus IN ('待确认', '已受理') ORDER BY certificatemergeid DESC")
    List<CertificateMerge> findPendingForApproval();

    /**
     * Find merge records by from certificate ID
     */
    @Select("SELECT * FROM CERTIFICATEGEMERGE WHERE fromcertificateid = #{fromCertificateId} ORDER BY certificatemergeid DESC")
    List<CertificateMerge> findByFromCertificateId(@Param("fromCertificateId") Long fromCertificateId);

    /**
     * Find merge records by to certificate ID
     */
    @Select("SELECT * FROM CERTIFICATEGEMERGE WHERE tocertificateid = #{toCertificateId} ORDER BY certificatemergeid DESC")
    List<CertificateMerge> findByToCertificateId(@Param("toCertificateId") Long toCertificateId);

    /**
     * Find recent merge records (last N days)
     */
    @Select("SELECT * FROM CERTIFICATEGEMERGE WHERE createtime >= DATEADD(DAY, -#{days}, GETDATE()) ORDER BY certificatemergeid DESC")
    List<CertificateMerge> findRecent(@Param("days") int days);

    /**
     * Count merge records by worker
     */
    @Select("SELECT COUNT(*) FROM CERTIFICATEGEMERGE WHERE workerid = #{workerId}")
    int countByWorkerId(@Param("workerId") Long workerId);

    /**
     * Count merge records by certificate
     */
    @Select("SELECT COUNT(*) FROM CERTIFICATEGEMERGE WHERE certificateid = #{certificateId}")
    int countByCertificateId(@Param("certificateId") Long certificateId);
}
