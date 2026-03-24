package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.ApplyAddItem;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

import java.util.List;

/**
 * ApplyAddItem Repository - Data Access Layer
 * Maps to: ApplyAddItemDAL.cs
 *
 * Replaces ADO.NET with MyBatis-Plus for database operations
 */
@Mapper
public interface ApplyAddItemRepository extends BaseMapper<ApplyAddItem> {

    /**
     * Find additional items by apply ID
     */
    @Select("SELECT * FROM APPLYADDITEM WHERE applyid = #{applyId} ORDER BY itemorder ASC")
    List<ApplyAddItem> findByApplyId(@Param("applyId") Long applyId);

    /**
     * Find additional items by worker ID
     */
    @Select("SELECT * FROM APPLYADDITEM WHERE workerid = #{workerId} ORDER BY applyadditemid DESC")
    List<ApplyAddItem> findByWorkerId(@Param("workerId") Long workerId);

    /**
     * Find additional items by apply type
     */
    @Select("SELECT * FROM APPLYADDITEM WHERE applytype = #{applyType} ORDER BY applyadditemid DESC")
    List<ApplyAddItem> findByApplyType(@Param("applyType") String applyType);

    /**
     * Find additional items by item type
     */
    @Select("SELECT * FROM APPLYADDITEM WHERE itemtype = #{itemType} ORDER BY applyadditemid DESC")
    List<ApplyAddItem> findByItemType(@Param("itemType") String itemType);

    /**
     * Find additional items by department/unit
     */
    @Select("SELECT * FROM APPLYADDITEM WHERE unitcode = #{unitCode} ORDER BY applyadditemid DESC")
    List<ApplyAddItem> findByUnitCode(@Param("unitCode") String unitCode);

    /**
     * Find by status
     */
    @Select("SELECT * FROM APPLYADDITEM WHERE applystatus = #{status} ORDER BY applyadditemid DESC")
    List<ApplyAddItem> findByStatus(@Param("status") String status);

    /**
     * Find pending additional items for approval
     */
    @Select("SELECT * FROM APPLYADDITEM WHERE applystatus IN ('待确认', '已受理') ORDER BY applyadditemid DESC")
    List<ApplyAddItem> findPendingForApproval();

    /**
     * Count additional items by apply
     */
    @Select("SELECT COUNT(*) FROM APPLYADDITEM WHERE applyid = #{applyId}")
    int countByApplyId(@Param("applyId") Long applyId);

    /**
     * Count additional items by worker
     */
    @Select("SELECT COUNT(*) FROM APPLYADDITEM WHERE workerid = #{workerId}")
    int countByWorkerId(@Param("workerId") Long workerId);
}
