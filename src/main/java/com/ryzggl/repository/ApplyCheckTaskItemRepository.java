package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.ApplyCheckTaskItem;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

import java.util.List;

/**
 * ApplyCheckTaskItem Repository - Data Access Layer
 * Maps to: ApplyCheckTaskItemDAL.cs
 *
 * Replaces ADO.NET with MyBatis-Plus for database operations
 */
@Mapper
public interface ApplyCheckTaskItemRepository extends BaseMapper<ApplyCheckTaskItem> {

    /**
     * Find check task items by check task ID
     */
    @Select("SELECT * FROM APPLYCHECKTASKITEM WHERE checktaskid = #{checkTaskId} ORDER BY itemorder ASC")
    List<ApplyCheckTaskItem> findByCheckTaskId(@Param("checkTaskId") Long checkTaskId);

    /**
     * Find check task items by apply ID
     */
    @Select("SELECT * FROM APPLYCHECKTASKITEM WHERE applyid = #{applyId} ORDER BY itemorder ASC")
    List<ApplyCheckTaskItem> findByApplyId(@Param("applyId") Long applyId);

    /**
     * Find check task items by worker ID
     */
    @Select("SELECT * FROM APPLYCHECKTASKITEM WHERE workerid = #{workerId} ORDER BY applychecktaskitemid DESC")
    List<ApplyCheckTaskItem> findByWorkerId(@Param("workerId") Long workerId);

    /**
     * Find check task items by apply type
     */
    @Select("SELECT * FROM APPLYCHECKTASKITEM WHERE applytype = #{applyType} ORDER BY applychecktaskitemid DESC")
    List<ApplyCheckTaskItem> findByApplyType(@Param("applyType") String applyType);

    /**
     * Find by item status
     */
    @Select("SELECT * FROM APPLYCHECKTASKITEM WHERE itemstatus = #{itemStatus} ORDER BY applychecktaskitemid DESC")
    List<ApplyCheckTaskItem> findByItemStatus(@Param("itemStatus") String itemStatus);

    /**
     * Find pending items for review
     */
    @Select("SELECT * FROM APPLYCHECKTASKITEM WHERE itemstatus = '待审核' ORDER BY itemorder ASC")
    List<ApplyCheckTaskItem> findPendingItems();

    /**
     * Find items by name
     */
    @Select("SELECT * FROM APPLYCHECKTASKITEM WHERE itemname = #{itemName} ORDER BY applychecktaskitemid DESC")
    List<ApplyCheckTaskItem> findByItemName(@Param("itemName") String itemName);

    /**
     * Count items by check task
     */
    @Select("SELECT COUNT(*) FROM APPLYCHECKTASKITEM WHERE checktaskid = #{checkTaskId}")
    int countByCheckTaskId(@Param("checkTaskId") Long checkTaskId);

    /**
     * Count items by apply
     */
    @Select("SELECT COUNT(*) FROM APPLYCHECKTASKITEM WHERE applyid = #{applyId}")
    int countByApplyId(@Param("applyId") Long applyId);

    /**
     * Count items by worker
     */
    @Select("SELECT COUNT(*) FROM APPLYCHECKTASKITEM WHERE workerid = #{workerId}")
    int countByWorkerId(@Param("workerId") Long workerId);
}
