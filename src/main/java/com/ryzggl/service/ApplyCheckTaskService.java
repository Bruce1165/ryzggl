package com.ryzggl.service;

import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.ryzggl.entity.ApplyCheckTask;
import com.ryzggl.entity.Apply;
import com.ryzggl.repository.ApplyCheckTaskRepository;
import com.ryzggl.common.Result;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.List;

/**
 * ApplyCheckTask Service - Business Logic Layer
 * Maps to: ApplyCheckTaskDAL.cs logic and workflow management
 */
@Service
@Transactional
public class ApplyCheckTaskService extends ServiceImpl<ApplyCheckTaskRepository, ApplyCheckTask> {

    private static final Logger log = LoggerFactory.getLogger(ApplyCheckTaskService.class);

    @Autowired
    private ApplyService applyService;

    /**
     * Create check task for application
     * Maps to: ApplyCheckTaskDAL.AddApplyCheckTask
     */
    public Result<Void> createApplyCheckTask(ApplyCheckTask applyCheckTask) {
        log.info("Creating check task for apply: {}", applyCheckTask.getApplyId());

        // Validate application exists
        Apply apply = applyService.getById(applyCheckTask.getApplyId());
        if (apply == null) {
            log.error("Apply not found: {}", applyCheckTask.getApplyId());
            return Result.error("申请不存在");
        }

        // Set initial task status
        applyCheckTask.setTaskStatus("待审核");
        applyCheckTask.setAssignedDate(LocalDate.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd")));

        boolean success = save(applyCheckTask);

        if (success) {
            log.info("Check task created successfully");
            return Result.success();
        } else {
            log.error("Failed to create check task");
            return Result.error("审批任务创建失败");
        }
    }

    /**
     * Get check task by ID
     */
    public Result<ApplyCheckTask> getApplyCheckTaskById(Long checkTaskId) {
        log.debug("Getting check task: {}", checkTaskId);
        ApplyCheckTask applyCheckTask = getById(checkTaskId);

        if (applyCheckTask == null) {
            return Result.error("审批任务不存在");
        }

        return Result.success(applyCheckTask);
    }

    /**
     * List check tasks for an application
     * Maps to: ApplyCheckTaskDAL.GetApplyCheckTasks
     */
    public Result<List<ApplyCheckTask>> listApplyCheckTasks(Long applyId, Long workerId, String applyType) {
        log.debug("Listing check tasks: applyId={}, workerId={}, applyType={}", applyId, workerId, applyType);

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

        queryWrapper.orderByAsc(ApplyCheckTask::getTaskOrder)
                  .orderByDesc(ApplyCheckTask::getCreateTime);

        List<ApplyCheckTask> applyCheckTasks = list(queryWrapper);
        return Result.success(applyCheckTasks);
    }

    /**
     * Assign check task to user
     * Maps to: ApplyCheckTaskDAL.AssignTask
     */
    public Result<Void> assignTask(Long checkTaskId, String assignedTo) {
        log.info("Assigning check task: {} to: {}", checkTaskId, assignedTo);

        ApplyCheckTask applyCheckTask = getById(checkTaskId);
        if (applyCheckTask == null) {
            return Result.error("审批任务不存在");
        }

        if (!"待审核".equals(applyCheckTask.getTaskStatus())) {
            return Result.error("只有待审核的任务才能分配");
        }

        applyCheckTask.setAssignedTo(assignedTo);
        applyCheckTask.setAssignedDate(LocalDate.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd")));
        boolean success = updateById(applyCheckTask);

        if (success) {
            log.info("Check task assigned successfully");
            return Result.success();
        } else {
            return Result.error("任务分配失败");
        }
    }

    /**
     * Complete check task with approval
     * Maps to: ApplyCheckTaskDAL.CompleteTask
     */
    public Result<Void> completeTask(Long checkTaskId, String taskResult, String checkAdvise, String completedBy) {
        log.info("Completing check task: {}, result: {}, completedBy: {}", checkTaskId, taskResult, completedBy);

        ApplyCheckTask applyCheckTask = getById(checkTaskId);
        if (applyCheckTask == null) {
            return Result.error("审批任务不存在");
        }

        if (!"待审核".equals(applyCheckTask.getTaskStatus())) {
            return Result.error("只有待审核的任务才能完成");
        }

        applyCheckTask.setTaskStatus("已完成");
        applyCheckTask.setTaskResult(taskResult);
        applyCheckTask.setCheckAdvise(checkAdvise);
        applyCheckTask.setCompletedBy(completedBy);
        applyCheckTask.setCompletedDate(LocalDate.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd")));
        boolean success = updateById(applyCheckTask);

        if (success) {
            log.info("Check task completed successfully");
            return Result.success();
        } else {
            return Result.error("任务完成失败");
        }
    }

    /**
     * Reject check task
     * Maps to: ApplyCheckTaskDAL.RejectTask
     */
    public Result<Void> rejectTask(Long checkTaskId, String rejectionReason, String checkMan) {
        log.info("Rejecting check task: {}, reason: {}", checkTaskId, rejectionReason);

        ApplyCheckTask applyCheckTask = getById(checkTaskId);
        if (applyCheckTask == null) {
            return Result.error("审批任务不存在");
        }

        if (!"待审核".equals(applyCheckTask.getTaskStatus())) {
            return Result.error("只有待审核的任务才能驳回");
        }

        applyCheckTask.setTaskStatus("已驳回");
        applyCheckTask.setTaskResult("驳回");
        applyCheckTask.setCheckAdvise(rejectionReason);
        applyCheckTask.setCheckMan(checkMan);
        applyCheckTask.setCheckDate(LocalDate.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd")));
        applyCheckTask.setCompletedBy(checkMan);
        applyCheckTask.setCompletedDate(LocalDate.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd")));
        boolean success = updateById(applyCheckTask);

        if (success) {
            log.info("Check task rejected successfully");
            return Result.success();
        } else {
            return Result.error("任务驳回失败");
        }
    }

    /**
     * Get pending check tasks for approval
     * Maps to: ApplyCheckTaskDAL.GetPendingTasks
     */
    public Result<List<ApplyCheckTask>> getPendingTasks() {
        log.debug("Getting pending check tasks");

        List<ApplyCheckTask> applyCheckTasks = baseMapper.findPendingTasks();

        return Result.success(applyCheckTasks);
    }

    /**
     * Get check tasks by status
     */
    public Result<List<ApplyCheckTask>> getApplyCheckTasksByStatus(String taskStatus) {
        log.debug("Getting check tasks by status: {}", taskStatus);

        List<ApplyCheckTask> applyCheckTasks = baseMapper.findByTaskStatus(taskStatus);

        return Result.success(applyCheckTasks);
    }

    /**
     * Get check tasks assigned to user
     */
    public Result<List<ApplyCheckTask>> getTasksByAssignedTo(String assignedTo) {
        log.debug("Getting check tasks assigned to: {}", assignedTo);

        List<ApplyCheckTask> applyCheckTasks = baseMapper.findByAssignedTo(assignedTo);

        return Result.success(applyCheckTasks);
    }

    /**
     * Get check tasks by apply
     */
    public Result<List<ApplyCheckTask>> getTasksByApplyId(Long applyId) {
        log.debug("Getting check tasks for apply: {}", applyId);

        List<ApplyCheckTask> applyCheckTasks = baseMapper.findByApplyId(applyId);

        return Result.success(applyCheckTasks);
    }
}
