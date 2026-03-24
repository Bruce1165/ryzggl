package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.ApplyCheckTask;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

/**
 * ApplyCheckTask Repository
 */
@Mapper
public interface ApplyCheckTaskRepository extends BaseMapper<ApplyCheckTask> {

    /**
     * Get checked count for a task
     * @param taskId Task ID
     * @return Number of checked items
     */
    @Select("SELECT ISNULL(COUNT(CheckTime), 0) " +
            "FROM ApplyCheckTaskItem " +
            "WHERE TaskID = #{taskId}")
    int selectCheckCount(@Param("taskId") Long taskId);

    /**
     * Update item count for a task
     * @param taskId Task ID
     * @return Number of affected rows
     */
    int updateItemCount(@Param("taskId") Long taskId);
}
