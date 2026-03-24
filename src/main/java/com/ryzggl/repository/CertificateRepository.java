package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.Certificate;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

import java.time.LocalDate;
import java.util.List;

/**
 * Certificate Repository - Data Access Layer
 * Maps to: CertificateDAL.cs / CertificateDAL.cs
 *
 * Key lifecycle operations: Enter, Change, Continue, Pause, Merge, Lock
 */
@Mapper
public interface CertificateRepository extends BaseMapper<Certificate> {

    /**
     * Find certificates by worker ID
     * Maps to: CertificateDAL.GetCertificatesByWorkerID
     */
    @Select("SELECT * FROM CERTIFICATE WHERE workerid = #{workerId} ORDER BY certificatid DESC")
    List<Certificate> findByWorkerId(@Param("workerId") Long workerId);

    /**
     * Find by certificate number
     * Maps to: CertificateDAL.GetByCertificateCode
     */
    @Select("SELECT * FROM CERTIFICATE WHERE certificatecode = #{certificateCode}")
    Certificate findByCertificateCode(@Param("certificateCode") String certificateCode);

    /**
     * Find valid certificates
     * Maps to: Active certificate queries
     */
    @Select("SELECT * FROM CERTIFICATE WHERE status = '有效' AND validenddate >= #{currentDate}")
    List<Certificate> findValidCertificates(@Param("currentDate") LocalDate currentDate);

    /**
     * Find expiring soon certificates (within 30 days)
     */
    @Select("SELECT * FROM CERTIFICATE WHERE status = '有效' " +
            "AND validenddate BETWEEN #{startDate} AND #{endDate} " +
            "ORDER BY validenddate ASC")
    List<Certificate> findExpiringSoon(
            @Param("startDate") LocalDate startDate,
            @Param("endDate") LocalDate endDate);

    /**
     * Count certificates by status
     */
    @Select("SELECT COUNT(*) FROM CERTIFICATE WHERE status = #{status}")
    int countByStatus(@Param("status") String status);

    /**
     * Find by unit/department
     */
    @Select("SELECT * FROM CERTIFICATE WHERE unitcode = #{unitCode} ORDER BY certificatid DESC")
    List<Certificate> findByUnitCode(@Param("unitCode") String unitCode);
}
