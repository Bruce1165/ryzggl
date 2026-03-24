package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.ApplyReplace;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

import java.util.List;

/**
 * ApplyReplace Repository - Data Access Layer
 * Maps to: ApplyReplaceDAL.cs
 *
 * Replaces ADO.NET with MyBatis-Plus for database operations
 */
@Mapper
public interface ApplyReplaceRepository extends BaseMapper<ApplyReplace> {

    /**
     * Find replacement applications by worker ID
     */
    @Select("SELECT * FROM ApplyReplace WHERE workerid = #{workerId} ORDER BY applyreplaceid DESC")
    List<ApplyReplace> findByWorkerId(@Param("workerId") Long workerId);

    /**
     * Find replacement applications by certificate ID
     */
    @Select("SELECT * FROM ApplyReplace WHERE certificateid = #{certificateId} ORDER BY applyreplaceid DESC")
    List<ApplyReplace> findByCertificateId(@Param("certificateId") Long certificateId);

    /**
     * Find replacement applications by department/unit
     */
    @Select("SELECT * FROM ApplyReplace WHERE unitcode = #{unitCode} ORDER BY applyreplaceid DESC")
    List<ApplyReplace> findByUnitCode(@Param("unitCode") String unitCode);

    /**
     * Find by status
     */
    @Select("SELECT * FROM ApplyReplace WHERE applystatus = #{status} ORDER BY applyreplaceid DESC")
    List<ApplyReplace> findByStatus(@Param("status") String status);

    /**
     * Find pending replacement applications for approval
     */
    @Select("SELECT * FROM ApplyReplace WHERE applystatus IN ('待确认', '已受理') ORDER BY applyreplaceid DESC")
    List<ApplyReplace> findPendingForApproval();

    /**
     * Find replacement applications by certificate code
     */
    @Select("SELECT * FROM ApplyReplace WHERE certificatecode = #{certificateCode} ORDER BY applyreplaceid DESC")
    List<ApplyReplace> findByCertificateCode(@Param("certificateCode") String certificateCode);

    /**
     * Find replacement applications by old certificate code
     */
    @Select("SELECT * FROM ApplyReplace WHERE oldcertificatecode = #{oldCertificateCode} ORDER BY applyreplaceid DESC")
    List<ApplyReplace> findByOldCertificateCode(@Param("oldCertificateCode") String oldCertificateCode);

    /**
     * Count replacement applications by worker
     */
    @Select("SELECT COUNT(*) FROM ApplyReplace WHERE workerid = #{workerId}")
    int countByWorkerId(@Param("workerId") Long workerId);
}
