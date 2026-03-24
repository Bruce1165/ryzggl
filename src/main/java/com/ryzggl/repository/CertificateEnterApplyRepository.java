package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.CertificateEnterApply;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

import java.util.List;

/**
 * CertificateEnterApply Repository - Data Access Layer
 * Maps to: CertificateEnterApplyDAL.cs
 *
 * Replaces ADO.NET with MyBatis-Plus for database operations
 */
@Mapper
public interface CertificateEnterApplyRepository extends BaseMapper<CertificateEnterApply> {

    /**
     * Find new certificate applications by worker ID
     */
    @Select("SELECT * FROM CERTIFICATEENTERAPPLY WHERE workerid = #{workerId} ORDER BY certificateenterapplyid DESC")
    List<CertificateEnterApply> findByWorkerId(@Param("workerId") Long workerId);

    /**
     * Find new certificate applications by department/unit
     */
    @Select("SELECT * FROM CERTIFICATEENTERAPPLY WHERE unitcode = #{unitCode} ORDER BY certificateenterapplyid DESC")
    List<CertificateEnterApply> findByUnitCode(@Param("unitCode") String unitCode);

    /**
     * Find by status
     */
    @Select("SELECT * FROM CERTIFICATEENTERAPPLY WHERE applystatus = #{status} ORDER BY certificateenterapplyid DESC")
    List<CertificateEnterApply> findByStatus(@Param("status") String status);

    /**
     * Find pending new certificate applications for approval
     */
    @Select("SELECT * FROM CERTIFICATEENTERAPPLY WHERE applystatus IN ('待确认', '已受理') ORDER BY certificateenterapplyid DESC")
    List<CertificateEnterApply> findPendingForApproval();

    /**
     * Find new certificate applications by certificate code
     */
    @Select("SELECT * FROM CERTIFICATEENTERAPPLY WHERE certificatecode = #{certificateCode} ORDER BY certificateenterapplyid DESC")
    List<CertificateEnterApply> findByCertificateCode(@Param("certificateCode") String certificateCode);

    /**
     * Find new certificate applications by type
     */
    @Select("SELECT * FROM CERTIFICATEENTERAPPLY WHERE certificatetype = #{certificateType} ORDER BY certificateenterapplyid DESC")
    List<CertificateEnterApply> findByCertificateType(@Param("certificateType") String certificateType);

    /**
     * Count new certificate applications by worker
     */
    @Select("SELECT COUNT(*) FROM CERTIFICATEENTERAPPLY WHERE workerid = #{workerId}")
    int countByWorkerId(@Param("workerId") Long workerId);
}
