package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.WorkerLock;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;
import org.apache.ibatis.annotations.Update;

import java.util.List;

/**
 * WorkerLock Repository
 * 人员锁定数据访问层
 */
public interface WorkerLockRepository extends BaseMapper<WorkerLock> {

    /**
     * Get lock by ID
     */
    @Select("SELECT * FROM WorkerLock WHERE LockID = #{lockId}")
    WorkerLock getById(@Param("lockId") Long lockId);

    /**
     * Get locks by worker ID
     */
    @Select("SELECT * FROM WorkerLock WHERE WorkerID = #{workerId} ORDER BY LockTime DESC")
    List<WorkerLock> getByWorkerId(@Param("workerId") Long workerId);

    /**
     * Get last lock for a worker
     */
    @Select("SELECT TOP 1 * FROM WorkerLock WHERE WorkerID = #{workerId} ORDER BY LockTime DESC")
    WorkerLock getLastLock(@Param("workerId") Long workerId);

    /**
     * Get active locks (not expired and not unlocked)
     */
    @Select("SELECT * FROM WorkerLock WHERE LockStatus = 'LOCKED' AND (LockEndTime IS NULL OR LockEndTime > GETDATE()) AND UnlockTime IS NULL ORDER BY LockTime DESC")
    List<WorkerLock> getActiveLocks();

    /**
     * Search locks
     */
    @Select("SELECT * FROM WorkerLock WHERE (LockPerson LIKE CONCAT('%', #{keyword}, '%') OR Remark LIKE CONCAT('%', #{keyword}, '%')) ORDER BY LockTime DESC")
    List<WorkerLock> search(@Param("keyword") String keyword);

    /**
     * Update lock to unlocked
     */
    @Update("UPDATE WorkerLock SET UnlockTime = #{unlockTime}, UnlockPerson = #{unlockPerson}, LockStatus = 'UNLOCKED' WHERE LockID = #{lockId}")
    int unlockLock(@Param("lockId") Long lockId, @Param("unlockTime") String unlockTime, @Param("unlockPerson") String unlockPerson);
}
