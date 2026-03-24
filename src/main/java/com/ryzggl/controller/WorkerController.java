package com.ryzggl.controller;

import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.common.Result;
import com.ryzggl.entity.Worker;
import com.ryzggl.entity.Department;
import com.ryzggl.service.WorkerService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.validation.annotation.Validated;
import org.springframework.web.bind.annotation.*;

import jakarta.servlet.http.HttpServletRequest;
import java.util.List;

/**
 * Worker Controller - REST API for worker/personnel management
 * Maps to: Organization/Department pages in .NET system
 */
@RestController
@RequestMapping("/api/worker")
@Tag(name = "Worker Management", description = "Worker and department management")
public class WorkerController {

    private static final Logger log = LoggerFactory.getLogger(WorkerController.class);

    @Autowired
    private WorkerService workerService;

    /**
     * Get workers list with pagination
     * Maps to: Worker management pages
     */
    @Operation(summary = "Get workers list", description = "List workers with pagination and filtering")
    @GetMapping("/list")
    public Result<Page<Worker>> getWorkerList(
            @RequestParam(defaultValue = "1") Integer current,
            @RequestParam(required = false) Long id,
            @RequestParam(required = false) String unitCode,
            @RequestParam(required = false) String status,
            @RequestParam(required = false) String keyword) {
        log.info("Getting workers: page={}, id={}, unitCode={}, status={}, keyword={}",
                current, id, unitCode, status, keyword);

        com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<Worker> queryWrapper =
                new com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<>();

        if (id != null) {
            queryWrapper.eq(Worker::getId, id);
        }
        if (unitCode != null && !unitCode.isEmpty()) {
            queryWrapper.eq(Worker::getUnitCode, unitCode);
        }
        if (status != null && !status.isEmpty()) {
            queryWrapper.eq(Worker::getWorkerStatus, status);
        }
        if (keyword != null && !keyword.isEmpty()) {
            queryWrapper.and(wrapper -> wrapper
                    .like(Worker::getWorkerName, keyword)
                    .or()
                    .like(Worker::getIdCardNo, keyword)
                    .or()
                    .like(Worker::getTelPhone, keyword));
        }

        Page<Worker> page = workerService.page(new Page<>(current, 10), queryWrapper);
        return Result.success(page);
    }

    /**
     * Get workers by department
     * Maps to: Organization/Department.aspx
     */
    @Operation(summary = "Get workers by department", description = "List workers by department code")
    @GetMapping("/department/{unitCode}")
    public Result<List<Worker>> getWorkersByDepartment(@PathVariable String unitCode) {
        log.info("Getting workers for department: {}", unitCode);
        return workerService.getWorkersByUnitCode(unitCode);
    }

    /**
     * Get workers by status
     * Maps to: Organization/DepartmentList.aspx
     */
    @Operation(summary = "Get workers by status", description = "List workers by status")
    @GetMapping("/status/{status}")
    public Result<List<Worker>> getWorkersByStatus(@PathVariable String status) {
        log.info("Getting workers by status: {}", status);
        return workerService.getWorkersByStatus(status);
    }

    /**
     * Create worker
     * Maps to: Organization/DepartmentAdd.aspx
     */
    @Operation(summary = "Create worker", description = "Create a new worker record")
    @PostMapping
    @PreAuthorize("hasRole('WORKER_ADMIN')")
    public Result<Void> createWorker(@Validated @RequestBody Worker worker) {
        log.info("Creating worker: {}", worker.getWorkerName());
        return workerService.createWorker(worker);
    }

    /**
     * Get worker by ID
     * Maps to: Organization/DepartmentDetail.aspx
     */
    @Operation(summary = "Get worker by ID", description = "Get detailed worker information")
    @GetMapping("/{id}")
    @PreAuthorize("hasRole('WORKER_USER', 'WORKER_ADMIN')")
    public Result<Worker> getWorker(@PathVariable Long id) {
        log.debug("Getting worker: {}", id);
        return workerService.getWorker(id);
    }

    /**
     * Update worker
     * Maps to: Organization/DepartmentUpdate.aspx
     */
    @Operation(summary = "Update worker", description = "Update worker information")
    @PutMapping("/{id}")
    @PreAuthorize("hasRole('WORKER_USER', 'WORKER_ADMIN')")
    public Result<Void> updateWorker(@PathVariable Long id, @Validated @RequestBody Worker worker) {
        log.info("Updating worker: {}", id);
        return workerService.updateWorker(worker);
    }

    /**
     * Delete worker
     * Maps to: Organization/DepartmentDelete.aspx
     */
    @Operation(summary = "Delete worker", description = "Soft delete worker by updating status")
    @DeleteMapping("/{id}")
    @PreAuthorize("hasRole('WORKER_ADMIN')")
    public Result<Void> deleteWorker(@PathVariable Long id) {
        log.info("Deleting worker: {}", id);
        return workerService.deleteWorker(id);
    }

    /**
     * Get worker count by department
     * Maps to: Organization/DepartmentCount.aspx
     */
    @Operation(summary = "Get worker count", description = "Count workers in a department")
    @GetMapping("/count/{unitCode}")
    public Result<Long> getWorkerCount(@PathVariable String unitCode) {
        log.debug("Counting workers in department: {}", unitCode);
        return workerService.getWorkerCount(unitCode);
    }
}
