package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.CertificateCAHistory;
import org.apache.ibatis.annotations.*;

import java.util.List;

/**
 * CertificateCAHistory Repository
 * 证书CA历史数据访问层
 */
public interface CertificateCAHistoryRepository extends BaseMapper<CertificateCAHistory> {

    /**
     * Get by ID
     */
    @Select("SELECT * FROM CertificateCAHistory WHERE CertificateCAID = #{certificateCaId}")
    CertificateCAHistory getById(@Param("certificateCaId") String certificateCaId);

    /**
     * Get all CA history records
     */
    @Select("SELECT * FROM CertificateCAHistory ORDER BY CertificateCAID")
    List<CertificateCAHistory> getAll();

    /**
     * Get by certificate ID
     */
    @Select("SELECT * FROM CertificateCAHistory WHERE CertificateID = #{certificateId} ORDER BY ApplyCATime DESC")
    List<CertificateCAHistory> getByCertificateId(@Param("certificateId") Long certificateId);

    /**
     * Search by keyword
     */
    @Select("SELECT * FROM CertificateCAHistory WHERE CertificateCAID LIKE CONCAT('%', #{keyword}, '%') ORDER BY ApplyCATime DESC")
    List<CertificateCAHistory> search(@Param("keyword") String keyword);

    /**
     * Insert CA history
     */
    @Insert("INSERT INTO CertificateCAHistory(CertificateCAID, ApplyCATime, SendCATime, ReturnCATime, CertificateID) VALUES (#{certificateCaId}, #{applyCaTime}, #{sendCaTime}, #{returnCaTime}, #{certificateId})")
    int insertCertificateCAHistory(CertificateCAHistory certificateCAHistory);

    /**
     * Update CA history
     */
    @Update("UPDATE CertificateCAHistory SET ApplyCATime = #{applyCaTime}, SendCATime = #{sendCaTime}, ReturnCATime = #{returnCaTime}, CertificateID = #{certificateId} WHERE CertificateCAID = #{certificateCaId}")
    int updateCertificateCAHistory(CertificateCAHistory certificateCAHistory);

    /**
     * Delete CA history
     */
    @Delete("DELETE FROM CertificateCAHistory WHERE CertificateCAID = #{certificateCaId}")
    int deleteCertificateCAHistory(@Param("certificateCaId") String certificateCaId);

    /**
     * Delete by certificate ID
     */
    @Delete("DELETE FROM CertificateCAHistory WHERE CertificateID = #{certificateId}")
    int deleteByCertificateId(@Param("certificateId") Long certificateId);

    /**
     * Count CA history records
     */
    @Select("SELECT COUNT(*) FROM CertificateCAHistory")
    int count();

    /**
     * Count by certificate ID
     */
    @Select("SELECT COUNT(*) FROM CertificateCAHistory WHERE CertificateID = #{certificateId}")
    int countByCertificateId(@Param("certificateId") Long certificateId);
}
