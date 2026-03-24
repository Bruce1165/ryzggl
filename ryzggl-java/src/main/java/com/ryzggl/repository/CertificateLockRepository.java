package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.CertificateLock;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;
import org.apache.ibatis.annotations.Update;

import java.util.List;

/**
 * CertificateLock Repository
 * 证书锁定管理数据访问层
 */
public interface CertificateLockRepository extends BaseMapper<CertificateLock> {

    /**
     * Get lock by ID
     */
    @Select("SELECT * FROM CertificateLock WHERE lockId = #{lockId}")
    CertificateLock getById(Long lockId);

    /**
     * Get locks by certificate ID
     */
    @Select("SELECT * FROM CertificateLock WHERE certificateId = #{certificateId} ORDER BY lockTime DESC")
    List<CertificateLock> getByCertificateId(Long certificateId);

    /**
     * Get all active locks
     */
    @Select("SELECT * FROM CertificateLock WHERE lockStatus = 'LOCKED' ORDER BY lockTime DESC")
    List<CertificateLock> getActiveLocks();

    /**
     * Get lock history
     */
    @Select("SELECT * FROM CertificateLock WHERE certificateId = #{certificateId} ORDER BY createTime DESC")
    List<CertificateLock> getLockHistory(Long certificateId);

    /**
     * Search locks
     */
    @Select("SELECT * FROM CertificateLock WHERE (lockPerson LIKE CONCAT('%', #{keyword}, '%') OR remark LIKE CONCAT('%', #{keyword}, '%')) ORDER BY lockTime DESC")
    List<CertificateLock> search(@Param("keyword") String keyword);

    /**
     * Get last lock for a certificate
     */
    @Select("SELECT TOP 1 * FROM CertificateLock WHERE certificateId = #{certificateId} ORDER BY lockTime DESC")
    CertificateLock getLastLock(Long certificateId);

    /**
     * Check if certificate is currently locked
     */
    @Select("SELECT TOP 1 * FROM CertificateLock WHERE certificateId = #{certificateId} AND lockStatus = 'LOCKED' ORDER BY lockTime DESC")
    CertificateLock isCertificateLocked(Long certificateId);

    /**
     * Create lock
     */
    @Insert("INSERT INTO CertificateLock (certificateId, lockType, lockPerson, remark) VALUES (#{certificateId}, #{lockType}, #{lockPerson}, #{remark})")
    int insert(CertificateLock lock);

    /**
     * Update lock
     */
    @Update("UPDATE CertificateLock SET lockEndTime = #{lockEndTime}, unlockTime = #{unlockTime}, unlockPerson = #{unlockPerson}, lockStatus = #{lockStatus} WHERE lockId = #{lockId}")
    int updateLock(CertificateLock lock);

    /**
     * Delete lock
     */
    @Select("SELECT COUNT(*) FROM CertificateLock WHERE lockId = #{lockId}")
    int countById(Long lockId);
}
