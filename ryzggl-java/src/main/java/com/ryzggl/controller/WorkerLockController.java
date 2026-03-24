package com.ryzggl.controller;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.common.Result;
import com.ryzggl.entity.WorkerLock;
import com.ryzggl.service.WorkerLockService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * WorkerLock Controller
 * 人员锁定REST API
 *
 * Maps to legacy WorkerLockDAL.cs
 * Base path: /api/v1/worker-locks
 */
@Tag(name = "WorkerLock Management", description = "人员锁定管理API")
@RestController
@RequestMapping("/api/v1/worker-locks")
public class WorkerLockController {

    @Autowired
    private WorkerLockService workerLockService;

    /**
     * Get lock by ID
     */
    @Operation(summary = "Get lock by ID")
    @GetMapping("/{lockId}")
    public Result<WorkerLock> getById(@PathVariable Long lockId) {
        WorkerLock lock = workerLockService.getById(lockId);
        if (lock == null) {
            return Result.error("锁定记录不存在");
        }
        return Result.success(lock);
    }

    /**
     * Get locks by worker ID
     */
    @Operation(summary = "Get locks by worker ID")
    @GetMapping("/worker/{workerId}")
    public Result<List<WorkerLock>> getByWorkerId(@PathVariable Long workerId) {
        List<WorkerLock> locks = workerLockService.getByWorkerId(workerId);
        return Result.success(locks);
    }

    /**
     * Get last lock for a worker
     */
    @Operation(summary = "Get last lock for a worker")
    @GetMapping("/worker/{workerId}/last")
    public Result<WorkerLock> getLastLock(@PathVariable Long workerId) {
        WorkerLock lock = workerLockService.getLastLock(workerId);
        return Result.success(lock);
    }

    /**
     * Get all active locks
     */
    @Operation(summary = "Get all active locks")
    @GetMapping("/active")
    public Result<List<WorkerLock>> getActiveLocks() {
        List<WorkerLock> locks = workerLockService.getActiveLocks();
        return Result.success(locks);
    }

    /**
     * Get all locks with pagination
     */
    @Operation(summary = "Get all locks with pagination")
    @GetMapping
    public Result<Page<WorkerLock>> list(
            @RequestParam(defaultValue = "1") int current,
            @RequestParam(defaultValue = "10") int size,
            @RequestParam(required = false) String keyword
    ) {
        Page<WorkerLock> page = new Page<>(current, size);
        QueryWrapper<WorkerLock> queryWrapper = new QueryWrapper<>();

        if (keyword != null && !keyword.isEmpty()) {
            queryWrapper.and(wrapper -> wrapper
                    .like("LockPerson", keyword)
                    .or()
                    .like("Remark", keyword)
            );
        }
        queryWrapper.orderByDesc("LockTime");

        Page<WorkerLock> result = workerLockService.page(page, queryWrapper);
        return Result.success(result);
    }

    /**
     * Search locks
     */
    @Operation(summary = "Search locks")
    @GetMapping("/search")
    public Result<List<WorkerLock>> search(@RequestParam String keyword) {
        List<WorkerLock> locks = workerLockService.search(keyword);
        return Result.success(locks);
    }

    /**
     * Lock worker
     */
    @Operation(summary = "Lock worker")
    @PostMapping("/lock")
    public Result<WorkerLock> lock(@RequestBody Map<String, Object> params) {
        Long workerId = Long.valueOf(params.get("workerId").toString());
        String lockType = (String) params.getOrDefault("lockType", "BUSINESS");
        String lockPerson = (String) params.get("lockPerson");
        String lockEndTime = (String) params.get("lockEndTime");
        String remark = (String) params.get("remark");

        return workerLockService.lockWorker(workerId, lockType, lockPerson, lockEndTime, remark);
    }

    /**
     * Unlock worker
     */
    @Operation(summary = "Unlock worker")
    @PostMapping("/unlock")
    public Result<Void> unlock(@RequestParam Long lockId, @RequestParam String unlockPerson) {
        return workerLockService.unlockWorker(lockId, unlockPerson);
    }

    /**
     * Check if worker is locked
     */
    @Operation(summary = "Check if worker is locked")
    @GetMapping("/worker/{workerId}/status")
    public Result<Map<String, Object>> checkWorkerLockStatus(@PathVariable Long workerId) {
        boolean isLocked = workerLockService.isWorkerLocked(workerId);
        WorkerLock lastLock = workerLockService.getLastLock(workerId);

        Map<String, Object> result = new HashMap<>();
        result.put("isLocked", isLocked);
        result.put("lastLock", lastLock);

        return Result.success(result);
    }

    /**
     * Delete lock record
     */
    @Operation(summary = "Delete lock record")
    @DeleteMapping("/{lockId}")
    public Result<Void> delete(@PathVariable Long lockId) {
        return workerLockService.deleteLock(lockId);
    }
}
