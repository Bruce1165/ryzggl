package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.CertificateLock;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

import java.util.List;

/**
 * CertificateLock Repository - Data Access Layer
 * Maps to: CertificateLockDAL.cs
 *
 * Replaces ADO.NET with MyBatis-Plus for database operations
 */
@Mapper
public interface CertificateLockRepository extends BaseMapper<CertificateLock> {

    /**
     * Find lock records by certificate ID
     */
    @Select("SELECT * FROM CERTIFICATELOCK WHERE certificateid = #{certificateId} ORDER BY certificatelockid DESC")
    List<CertificateLock> findByCertificateId(@Param("certificateId") Long certificateId);

    /**
     * Find lock records by worker ID
     */
    @Select("SELECT * FROM CERTIFICATELOCK WHERE workerid = #{workerId} ORDER BY certificatelockid DESC")
    List<CertificateLock> findByWorkerId(@Param("workerId") Long workerId);

    /**
     * Find lock records by department/unit
     */
    @Select("SELECT * FROM CERTIFICATELOCK WHERE unitcode = #{unitCode} ORDER BY certificatelockid DESC")
    List<CertificateLock> findByUnitCode(@Param("unitCode") String unitCode);

    /**
     * Find by lock status
     */
    @Select("SELECT * FROM CERTIFICATELOCK WHERE lockstatus = #{lockStatus} ORDER BY certificatelockid DESC")
    List<CertificateLock> findByLockStatus(@Param("lockStatus") String lockStatus);

    /**
     * Find by lock type
     */
    @Select("SELECT * FROM CERTIFICATELOCK WHERE locktype = #{lockType} ORDER BY certificatelockid DESC")
    List<CertificateLock> findByLockType(@Param("lockType") String lockType);

    /**
     * Find lock records by certificate code
     */
    @Select("SELECT * FROM CERTIFICATELOCK WHERE certificatecode = #{certificateCode} ORDER BY certificatelockid DESC")
    List<CertificateLock> findByCertificateCode(@Param("certificateCode") String certificateCode);

    /**
     * Find currently locked certificates
     */
    @Select("SELECT * FROM CERTIFICATELOCK WHERE lockstatus = '已锁定' ORDER BY certificatelockid DESC")
    List<CertificateLock> findLockedCertificates();

    /**
     * Find recent lock records (last N days)
     */
    @Select("SELECT * FROM CERTIFICATELOCK WHERE createtime >= DATEADD(DAY, -#{days}, GETDATE()) ORDER BY certificatelockid DESC")
    List<CertificateLock> findRecent(@Param("days") int days);

    /**
     * Count lock records by worker
     */
    @Select("SELECT COUNT(*) FROM CERTIFICATELOCK WHERE workerid = #{workerId}")
    int countByWorkerId(@Param("workerId") Long workerId);

    /**
     * Count lock records by certificate
     */
    @Select("SELECT COUNT(*) FROM CERTIFICATELOCK WHERE certificateid = #{certificateId}")
    int countByCertificateId(@Param("certificateId") Long certificateId);
}
