package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.ApplyCancel;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

import java.util.List;

/**
 * ApplyCancel Repository - Data Access Layer
 * Maps to: ApplyCancelDAL.cs
 *
 * Replaces ADO.NET with MyBatis-Plus for database operations
 */
@Mapper
public interface ApplyCancelRepository extends BaseMapper<ApplyCancel> {

    /**
     * Find cancellation applications by worker ID
     */
    @Select("SELECT * FROM ApplyCancel WHERE workerid = #{workerId} ORDER BY applycancelid DESC")
    List<ApplyCancel> findByWorkerId(@Param("workerId") Long workerId);

    /**
     * Find cancellation applications by certificate ID
     */
    @Select("SELECT * FROM ApplyCancel WHERE certificateid = #{certificateId} ORDER BY applycancelid DESC")
    List<ApplyCancel> findByCertificateId(@Param("certificateId") Long certificateId);

    /**
     * Find cancellation applications by department/unit
     */
    @Select("SELECT * FROM ApplyCancel WHERE unitcode = #{unitCode} ORDER BY applycancelid DESC")
    List<ApplyCancel> findByUnitCode(@Param("unitCode") String unitCode);

    /**
     * Find by status
     */
    @Select("SELECT * FROM ApplyCancel WHERE applystatus = #{status} ORDER BY applycancelid DESC")
    List<ApplyCancel> findByStatus(@Param("status") String status);

    /**
     * Find pending cancellation applications for approval
     */
    @Select("SELECT * FROM ApplyCancel WHERE applystatus IN ('待确认', '已受理') ORDER BY applycancelid DESC")
    List<ApplyCancel> findPendingForApproval();

    /**
     * Find cancellation applications by certificate code
     */
    @Select("SELECT * FROM ApplyCancel WHERE certificatecode = #{certificateCode} ORDER BY applycancelid DESC")
    List<ApplyCancel> findByCertificateCode(@Param("certificateCode") String certificateCode);

    /**
     * Count cancellation applications by worker
     */
    @Select("SELECT COUNT(*) FROM ApplyCancel WHERE workerid = #{workerId}")
    int countByWorkerId(@Param("workerId") Long workerId);

    /**
     * Count cancellation applications by certificate
     */
    @Select("SELECT COUNT(*) FROM ApplyCancel WHERE certificateid = #{certificateId}")
    int countByCertificateId(@Param("certificateId") Long certificateId);
}
