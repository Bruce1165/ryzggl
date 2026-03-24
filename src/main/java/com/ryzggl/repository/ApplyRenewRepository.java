package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.ApplyRenew;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

import java.util.List;

/**
 * ApplyRenew Repository - Data Access Layer
 * Maps to: ApplyRenewDAL.cs
 *
 * Replaces ADO.NET with MyBatis-Plus for database operations
 */
@Mapper
public interface ApplyRenewRepository extends BaseMapper<ApplyRenew> {

    /**
     * Find renewal applications by worker ID
     */
    @Select("SELECT * FROM ApplyRenew WHERE workerid = #{workerId} ORDER BY applyrenewid DESC")
    List<ApplyRenew> findByWorkerId(@Param("workerId") Long workerId);

    /**
     * Find renewal applications by certificate ID
     */
    @Select("SELECT * FROM ApplyRenew WHERE certificateid = #{certificateId} ORDER BY applyrenewid DESC")
    List<ApplyRenew> findByCertificateId(@Param("certificateId") Long certificateId);

    /**
     * Find renewal applications by department/unit
     */
    @Select("SELECT * FROM ApplyRenew WHERE unitcode = #{unitCode} ORDER BY applyrenewid DESC")
    List<ApplyRenew> findByUnitCode(@Param("unitCode") String unitCode);

    /**
     * Find by status
     */
    @Select("SELECT * FROM ApplyRenew WHERE applystatus = #{status} ORDER BY applyrenewid DESC")
    List<ApplyRenew> findByStatus(@Param("status") String status);

    /**
     * Find pending renewal applications for approval
     */
    @Select("SELECT * FROM ApplyRenew WHERE applystatus IN ('待确认', '已受理') ORDER BY applyrenewid DESC")
    List<ApplyRenew> findPendingForApproval();

    /**
     * Find renewal applications by certificate code
     */
    @Select("SELECT * FROM ApplyRenew WHERE certificatecode = #{certificateCode} ORDER BY applyrenewid DESC")
    List<ApplyRenew> findByCertificateCode(@Param("certificateCode") String certificateCode);

    /**
     * Count renewal applications by worker
     */
    @Select("SELECT COUNT(*) FROM ApplyRenew WHERE workerid = #{workerId}")
    int countByWorkerId(@Param("workerId") Long workerId);

    /**
     * Count renewal applications by certificate
     */
    @Select("SELECT COUNT(*) FROM ApplyRenew WHERE certificateid = #{certificateId}")
    int countByCertificateId(@Param("certificateId") Long certificateId);
}
