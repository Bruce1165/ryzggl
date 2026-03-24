package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.ApplyContinue;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

import java.util.List;

/**
 * ApplyContinue Repository - Data Access Layer
 * Maps to: ApplyContinueDAL.cs
 *
 * Replaces ADO.NET with MyBatis-Plus for database operations
 */
@Mapper
public interface ApplyContinueRepository extends BaseMapper<ApplyContinue> {

    /**
     * Find continuation applications by worker ID
     */
    @Select("SELECT * FROM ApplyContinue WHERE workerid = #{workerId} ORDER BY applycontinueid DESC")
    List<ApplyContinue> findByWorkerId(@Param("workerId") Long workerId);

    /**
     * Find continuation applications by certificate ID
     */
    @Select("SELECT * FROM ApplyContinue WHERE certificateid = #{certificateId} ORDER BY applycontinueid DESC")
    List<ApplyContinue> findByCertificateId(@Param("certificateId") Long certificateId);

    /**
     * Find continuation applications by department/unit
     */
    @Select("SELECT * FROM ApplyContinue WHERE unitcode = #{unitCode} ORDER BY applycontinueid DESC")
    List<ApplyContinue> findByUnitCode(@Param("unitCode") String unitCode);

    /**
     * Find by status
     */
    @Select("SELECT * FROM ApplyContinue WHERE applystatus = #{status} ORDER BY applycontinueid DESC")
    List<ApplyContinue> findByStatus(@Param("status") String status);

    /**
     * Find pending continuation applications for approval
     */
    @Select("SELECT * FROM ApplyContinue WHERE applystatus IN ('待确认', '已受理') ORDER BY applycontinueid DESC")
    List<ApplyContinue> findPendingForApproval();

    /**
     * Find continuation applications by certificate code
     */
    @Select("SELECT * FROM ApplyContinue WHERE certificatecode = #{certificateCode} ORDER BY applycontinueid DESC")
    List<ApplyContinue> findByCertificateCode(@Param("certificateCode") String certificateCode);

    /**
     * Find continuation applications by type
     */
    @Select("SELECT * FROM ApplyContinue WHERE continuetype = #{continueType} ORDER BY applycontinueid DESC")
    List<ApplyContinue> findByContinueType(@Param("continueType") String continueType);

    /**
     * Count continuation applications by worker
     */
    @Select("SELECT COUNT(*) FROM ApplyContinue WHERE workerid = #{workerId}")
    int countByWorkerId(@Param("workerId") Long workerId);

    /**
     * Count continuation applications by certificate
     */
    @Select("SELECT COUNT(*) FROM ApplyContinue WHERE certificateid = #{certificateId}")
    int countByCertificateId(@Param("certificateId") Long certificateId);
}
