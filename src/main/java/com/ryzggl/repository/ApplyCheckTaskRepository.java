package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.ApplyCheckTask;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

import java.util.List;

/**
 * ApplyCheckTask Repository - Data Access Layer
 * Maps to: ApplyCheckTaskDAL.cs
 *
 * Replaces ADO.NET with MyBatis-Plus for database operations
 */
@Mapper
public interface ApplyCheckTaskRepository extends BaseMapper<ApplyCheckTask> {

    /**
     * Find check tasks by apply ID
     */
    @Select("SELECT * FROM APPLYCHECKTASK WHERE applyid = #{applyId} ORDER BY taskorder ASC")
    List<ApplyCheckTask> findByApplyId(@Param("applyId") Long applyId);

    /**
     * Find check tasks by worker ID
     */
    @Select("SELECT * FROM APPLYCHECKTASK WHERE workerid = #{workerId} ORDER BY applychecktaskid DESC")
    List<ApplyCheckTask> findByWorkerId(@Param("workerId") Long workerId);

    /**
     * Find check tasks by apply type
     */
    @Select("SELECT * FROM APPLYCHECKTASK WHERE applytype = #{applyType} ORDER BY applychecktaskid DESC")
    List<ApplyCheckTask> findByApplyType(@Param("applyType") String applyType);

    /**
     * Find check tasks by department/unit
     */
    @Select("SELECT * FROM APPLYCHECKTASK WHERE unitcode = #{unitCode} ORDER BY applychecktaskid DESC")
    List<ApplyCheckTask> findByUnitCode(@Param("unitCode") String unitCode);

    /**
     * Find by task status
     */
    @Select("SELECT * FROM APPLYCHECKTASK WHERE taskstatus = #{taskStatus} ORDER BY applychecktaskid DESC")
    List<ApplyCheckTask> findByTaskStatus(@Param("taskStatus") String taskStatus);

    /**
     * Find pending tasks for approval
     */
    @Select("SELECT * FROM APPLYCHECKTASK WHERE taskstatus = '待审核' ORDER BY taskorder ASC")
    List<ApplyCheckTask> findPendingTasks();

    /**
     * Find tasks assigned to specific user
     */
    @Select("SELECT * FROM APPLYCHECKTASK WHERE assignedto = #{assignedTo} ORDER BY applychecktaskid DESC")
    List<ApplyCheckTask> findByAssignedTo(@Param("assignedTo") String assignedTo);

    /**
     * Find tasks by task result
     */
    @Select("SELECT * FROM APPLYCHECKTASK WHERE taskresult = #{taskResult} ORDER BY applychecktaskid DESC")
    List<ApplyCheckTask> findByTaskResult(@Param("taskResult") String taskResult);

    /**
     * Count tasks by apply
     */
    @Select("SELECT COUNT(*) FROM APPLYCHECKTASK WHERE applyid = #{applyId}")
    int countByApplyId(@Param("applyId") Long applyId);

    /**
     * Count tasks by worker
     */
    @Select("SELECT COUNT(*) FROM APPLYCHECKTASK WHERE workerid = #{workerId}")
    int countByWorkerId(@Param("workerId") Long workerId);
}
