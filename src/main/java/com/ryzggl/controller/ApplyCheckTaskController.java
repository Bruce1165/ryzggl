package com.ryzggl.controller;

import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.common.Result;
import com.ryzggl.entity.ApplyCheckTask;
import com.ryzggl.service.ApplyCheckTaskService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.media.Schema;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.validation.annotation.Validated;
import org.springframework.web.bind.annotation.*;

import jakarta.validation.Valid;
import java.util.List;

/**
 * ApplyCheckTask Controller - REST API Endpoints
 * Replaces .NET WebForms pages: zjsCheckTaskList.aspx, zjsCheckTaskChange.aspx
 */
@RestController
@RequestMapping("/api/apply-check-task")
@Tag(name = "Check Task Management", description = "Multi-step approval check task workflows")
public class ApplyCheckTaskController {

    private static final Logger log = LoggerFactory.getLogger(ApplyCheckTaskController.class);

    @Autowired
    private ApplyCheckTaskService applyCheckTaskService;

    /**
     * Get check task list with pagination
     * Replaces: zjsCheckTaskList.aspx with Telerik RadGrid
     */
    @Operation(summary = "Get check task list", description = "List check tasks with pagination and filtering")
    @GetMapping("/list")
    public Result<Page<ApplyCheckTask>> getApplyCheckTaskList(
            @RequestParam(defaultValue = "1") Integer current,
            @RequestParam(required = false) Long applyId,
            @RequestParam(required = false) Long workerId,
            @RequestParam(required = false) String applyType,
            @RequestParam(required = false) String taskStatus,
            @RequestParam(required = false) String assignedTo,
            @RequestParam(required = false) String keyword) {
        log.info("Getting check tasks: page={}, applyId={}, workerId={}, applyType={}, taskStatus={}, assignedTo={}, keyword={}",
                current, applyId, workerId, applyType, taskStatus, assignedTo, keyword);

        com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<ApplyCheckTask> queryWrapper =
                new com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<>();

        if (applyId != null) {
            queryWrapper.eq(ApplyCheckTask::getApplyId, applyId);
        }
        if (workerId != null) {
            queryWrapper.eq(ApplyCheckTask::getWorkerId, workerId);
        }
        if (applyType != null) {
            queryWrapper.eq(ApplyCheckTask::getApplyType, applyType);
        }
        if (taskStatus != null) {
            queryWrapper.eq(ApplyCheckTask::getTaskStatus, taskStatus);
        }
        if (assignedTo != null) {
            queryWrapper.eq(ApplyCheckTask::getAssignedTo, assignedTo);
        }
        if (keyword != null && !keyword.isEmpty()) {
            queryWrapper.and(wrapper -> wrapper
                    .like(ApplyCheckTask::getTaskName, keyword)
                    .or()
                    .like(ApplyCheckTask::getTaskDescription, keyword)
                    .or()
                    .like(ApplyCheckTask::getWorkerName, keyword));
        }

        queryWrapper.orderByAsc(ApplyCheckTask::getTaskOrder)
                  .orderByDesc(ApplyCheckTask::getCreateTime);

        Page<ApplyCheckTask> page = applyCheckTaskService.page(new Page<>(current, 10), queryWrapper);

        return Result.success(page);
    }

    /**
     * Create check task
     * Replaces: zjsCheckTaskFirst.aspx
     */
    @Operation(summary = "Create check task", description = "Create a new check task for application")
    @PostMapping
    public Result<Void> createApplyCheckTask(@Valid @RequestBody ApplyCheckTask applyCheckTask) {
        log.info("Creating check task for apply: {}", applyCheckTask.getApplyId());
        return applyCheckTaskService.createApplyCheckTask(applyCheckTask);
    }

    /**
     * Assign check task to user
     */
    @Operation(summary = "Assign check task", description = "Assign a check task to a specific user")
    @PostMapping("/{id}/assign")
    public Result<Void> assignTask(@PathVariable Long id, @RequestParam String assignedTo) {
        log.info("Assigning check task: {} to: {}", id, assignedTo);
        return applyCheckTaskService.assignTask(id, assignedTo);
    }

    /**
     * Complete check task
     * Replaces: Complete workflow with TASKRESULT
     */
    @Operation(summary = "Complete check task", description = "Complete a check task with result")
    @PutMapping("/{id}/complete")
    public Result<Void> completeTask(
            @PathVariable Long id,
            @Validated @RequestBody CompleteRequest request) {
        log.info("Completing check task: {}", id);
        return applyCheckTaskService.completeTask(id, request.getTaskResult(), request.getCheckAdvise(), request.getCompletedBy());
    }

    /**
     * Reject check task
     * Replaces: Reject workflow with rejection reason
     */
    @Operation(summary = "Reject check task", description = "Reject a check task with reason")
    @PutMapping("/{id}/reject")
    public Result<Void> rejectTask(
            @PathVariable Long id,
            @Validated @RequestBody RejectRequest request) {
        log.info("Rejecting check task: {}", id);
        return applyCheckTaskService.rejectTask(id, request.getRejectionReason(), request.getCheckMan());
    }

    /**
     * Get check task by ID
     * Replaces: zjsCheckTaskDetail.aspx
     */
    @Operation(summary = "Get check task by ID", description = "Get detailed check task information")
    @GetMapping("/{id}")
    public Result<ApplyCheckTask> getApplyCheckTaskById(@PathVariable Long id) {
        log.debug("Getting check task: {}", id);
        return applyCheckTaskService.getApplyCheckTaskById(id);
    }

    /**
     * Get pending check tasks for approval
     * Replaces: CheckTask.GetPendingTasks
     */
    @Operation(summary = "Get pending check tasks", description = "Get check tasks pending for approval")
    @GetMapping("/pending")
    public Result<List<ApplyCheckTask>> getPendingTasks() {
        log.debug("Getting pending check tasks");
        return applyCheckTaskService.getPendingTasks();
    }

    /**
     * Get check tasks by status
     * Replaces: zjsCheckTaskList.aspx (filter by TASKSTATUS)
     */
    @Operation(summary = "Get check tasks by status", description = "Filter check tasks by status")
    @GetMapping("/status/{taskStatus}")
    public Result<List<ApplyCheckTask>> getTasksByStatus(@PathVariable String taskStatus) {
        log.debug("Getting check tasks by status: {}", taskStatus);
        return applyCheckTaskService.getApplyCheckTasksByStatus(taskStatus);
    }

    /**
     * Get check tasks assigned to user
     */
    @Operation(summary = "Get tasks assigned to user", description = "Get all tasks assigned to a specific user")
    @GetMapping("/assigned/{assignedTo}")
    public Result<List<ApplyCheckTask>> getTasksByAssignedTo(@PathVariable String assignedTo) {
        log.debug("Getting check tasks assigned to: {}", assignedTo);
        return applyCheckTaskService.getTasksByAssignedTo(assignedTo);
    }

    /**
     * Get check tasks by apply
     */
    @Operation(summary = "Get check tasks by apply", description = "Get all check tasks for a specific application")
    @GetMapping("/apply/{applyId}")
    public Result<List<ApplyCheckTask>> getTasksByApplyId(@PathVariable Long applyId) {
        log.debug("Getting check tasks for apply: {}", applyId);
        return applyCheckTaskService.getTasksByApplyId(applyId);
    }

    /**
     * Inner class for complete request
     */
    @Schema(description = "Check task complete request")
    public static class CompleteRequest {
        @Schema(description = "Task result", required = true)
        private String taskResult;

        @Schema(description = "Check advice")
        private String checkAdvise;

        @Schema(description = "Completed by", required = true)
        private String completedBy;

        public String getTaskResult() {
            return taskResult;
        }

        public void setTaskResult(String taskResult) {
            this.taskResult = taskResult;
        }

        public String getCheckAdvise() {
            return checkAdvise;
        }

        public void setCheckAdvise(String checkAdvise) {
            this.checkAdvise = checkAdvise;
        }

        public String getCompletedBy() {
            return completedBy;
        }

        public void setCompletedBy(String completedBy) {
            this.completedBy = completedBy;
        }
    }

    /**
     * Inner class for reject request
     */
    @Schema(description = "Check task reject request")
    public static class RejectRequest {
        @Schema(description = "Rejection reason", required = true)
        private String rejectionReason;

        @Schema(description = "Check man", required = true)
        private String checkMan;

        public String getRejectionReason() {
            return rejectionReason;
        }

        public void setRejectionReason(String rejectionReason) {
            this.rejectionReason = rejectionReason;
        }

        public String getCheckMan() {
            return checkMan;
        }

        public void setCheckMan(String checkMan) {
            this.checkMan = checkMan;
        }
    }
}
