package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.Apply;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

import java.util.List;

/**
 * Apply Repository - Data Access Layer
 * Maps to: ApplyDAL.cs / ApplyDAL.cs
 *
 * Replaces ADO.NET with MyBatis-Plus for database operations
 */
@Mapper
public interface ApplyRepository extends BaseMapper<Apply> {

    /**
     * Find applications by worker ID
     * Maps to: ApplyDAL.GetApplyByWorkerID
     */
    @Select("SELECT * FROM Apply WHERE workerid = #{workerId} ORDER BY applyid DESC")
    List<Apply> findByWorkerId(@Param("workerId") Long workerId);

    /**
     * Find applications by department/unit
     * Maps to: ApplyDAL.GetApplyByUnitCode
     */
    @Select("SELECT * FROM Apply WHERE unitcode = #{unitCode} ORDER BY applyid DESC")
    List<Apply> findByUnitCode(@Param("unitCode") String unitCode);

    /**
     * Find by status
     * Maps to: ApplyDAL queries with APPLYSTATUS filter
     */
    @Select("SELECT * FROM Apply WHERE applystatus = #{status} ORDER BY applyid DESC")
    List<Apply> findByStatus(@Param("status") String status);

    /**
     * Find pending applications for approval workflow
     * Maps to: Approval workflow in ApplyCheckTask
     */
    @Select("SELECT * FROM Apply WHERE applystatus IN ('待确认', '已受理') ORDER BY applyid DESC")
    List<Apply> findPendingForApproval();

    /**
     * Count applications by worker
     */
    @Select("SELECT COUNT(*) FROM Apply WHERE workerid = #{workerId}")
    int countByWorkerId(@Param("workerId") Long workerId);
}
