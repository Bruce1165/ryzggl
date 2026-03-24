package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.Worker;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

import java.util.List;

/**
 * Worker Repository - Data access for worker/personnel
 */
@Mapper
public interface WorkerRepository extends BaseMapper<Worker> {

    /**
     * Find by ID
     */
    @Select("SELECT * FROM Worker WHERE WORKERID = #{workerId}")
    Worker getById(@Param("workerId") Long workerId);

    /**
     * Find by department/unit
     * Maps to: DepartmentDAL.GetWorkersByUnitCode
     */
    @Select("SELECT * FROM Worker WHERE unitcode = #{unitCode} ORDER BY WORKERID DESC")
    List<Worker> findByUnitCode(@Param("unitCode") String unitCode);

    /**
     * Find by name (with LIKE)
     */
    @Select("SELECT * FROM Worker WHERE workername LIKE CONCAT('%', #{keyword}, '%') ORDER BY WORKERID DESC")
    List<Worker> findByName(@Param("keyword") String keyword);

    /**
     * Find by status
     * Maps to: WorkerDAL.GetByWorkerStatus
     */
    @Select("SELECT * FROM Worker WHERE workerstatus = #{status} ORDER BY WORKERID DESC")
    List<Worker> findByStatus(@Param("status") String status);

    /**
     * Count by department
     */
    @Select("SELECT COUNT(*) FROM Worker WHERE unitcode = #{unitCode}")
    int countByUnitCode(@Param("unitCode") String unitCode);
}
