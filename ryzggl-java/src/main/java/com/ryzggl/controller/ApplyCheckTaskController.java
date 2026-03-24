package com.ryzggl.controller;

import com.ryzggl.common.Result;
import com.ryzggl.entity.ApplyCheckTask;
import com.ryzggl.entity.ApplyCheckTaskItem;
import com.ryzggl.service.ApplyCheckTaskItemService;
import com.ryzggl.service.ApplyCheckTaskService;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * ApplyCheckTask Controller - Multi-step Approval Workflow API
 * REST API for managing application check tasks and approvals
 */
@RestController
@RequestMapping("/api/v1/check-tasks")
public class ApplyCheckTaskController {

    private static final Logger logger = LoggerFactory.getLogger(ApplyCheckTaskController.class);

    @Autowired
    private ApplyCheckTaskService applyCheckTaskService;

    @Autowired
    private ApplyCheckTaskItemService applyCheckTaskItemService;

    /**
     * Create a new check task
     */
    @PostMapping
    public Result<ApplyCheckTask> createTask(@RequestBody ApplyCheckTask task) {
        try {
            ApplyCheckTask created = applyCheckTaskService.createTask(task);
            return Result.success(created);
        } catch (Exception e) {
            logger.error("Error creating check task", e);
            return Result.error("创建抽查任务失败: " + e.getMessage());
        }
    }

    /**
     * Update check task
     */
    @PutMapping("/{taskId}")
    public Result<ApplyCheckTask> updateTask(@PathVariable Long taskId,
                                             @RequestBody ApplyCheckTask task) {
        try {
            task.setTaskID(taskId);
            ApplyCheckTask updated = applyCheckTaskService.updateTask(task);
            return Result.success(updated);
        } catch (Exception e) {
            logger.error("Error updating check task: TaskID={}", taskId, e);
            return Result.error("更新抽查任务失败: " + e.getMessage());
        }
    }

    /**
     * Delete check task
     */
    @DeleteMapping("/{taskId}")
    public Result<Boolean> deleteTask(@PathVariable Long taskId) {
        try {
            boolean deleted = applyCheckTaskService.deleteTask(taskId);
            return Result.success(deleted);
        } catch (Exception e) {
            logger.error("Error deleting check task: TaskID={}", taskId, e);
            return Result.error("删除抽查任务失败: " + e.getMessage());
        }
    }

    /**
     * Get check task by ID
     */
    @GetMapping("/{taskId}")
    public Result<ApplyCheckTask> getTask(@PathVariable Long taskId) {
        try {
            ApplyCheckTask task = applyCheckTaskService.getTaskById(taskId);
            if (task == null) {
                return Result.error("抽查任务不存在");
            }
            return Result.success(task);
        } catch (Exception e) {
            logger.error("Error getting check task: TaskID={}", taskId, e);
            return Result.error("获取抽查任务失败: " + e.getMessage());
        }
    }

    /**
     * List check tasks with pagination
     */
    @GetMapping
    public Result<List<ApplyCheckTask>> listTasks(
            @RequestParam(defaultValue = "1") int page,
            @RequestParam(defaultValue = "20") int size) {
        try {
            List<ApplyCheckTask> tasks = applyCheckTaskService.getTasksWithPagination(page, size);
            return Result.success(tasks);
        } catch (Exception e) {
            logger.error("Error listing check tasks", e);
            return Result.error("获取抽查任务列表失败: " + e.getMessage());
        }
    }

    /**
     * Generate check items (sample applications)
     */
    @PostMapping("/{taskId}/generate-items")
    public Result<Integer> generateItems(@PathVariable Long taskId) {
        try {
            int itemCount = applyCheckTaskService.generateCheckItems(taskId);
            return Result.success(itemCount);
        } catch (Exception e) {
            logger.error("Error generating check items: TaskID={}", taskId, e);
            return Result.error("生成抽查项失败: " + e.getMessage());
        }
    }

    /**
     * Get task progress
     */
    @GetMapping("/{taskId}/progress")
    public Result<Map<String, Object>> getTaskProgress(@PathVariable Long taskId) {
        try {
            int[] progress = applyCheckTaskService.getTaskProgress(taskId);
            Map<String, Object> result = new HashMap<>();
            result.put("total", progress[0]);
            result.put("checked", progress[1]);
            result.put("unchecked", progress[2]);
            result.put("completionRate", progress[0] > 0 ? (double) progress[1] / progress[0] * 100 : 0);
            return Result.success(result);
        } catch (Exception e) {
            logger.error("Error getting task progress: TaskID={}", taskId, e);
            return Result.error("获取进度失败: " + e.getMessage());
        }
    }

    /**
     * Approve a single check item
     */
    @PostMapping("/items/{taskItemId}/approve")
    public Result<ApplyCheckTaskItem> approveItem(
            @PathVariable Long taskItemId,
            @RequestParam String checkMan,
            @RequestParam String checkResult,
            @RequestParam(required = false) String checkResultDesc) {
        try {
            ApplyCheckTaskItem item = applyCheckTaskItemService.approveItem(
                    taskItemId, checkMan, checkResult, checkResultDesc);
            if (item == null) {
                return Result.error("抽查项不存在");
            }
            return Result.success(item);
        } catch (Exception e) {
            logger.error("Error approving item: TaskItemID={}", taskItemId, e);
            return Result.error("审批失败: " + e.getMessage());
        }
    }

    /**
     * Batch approve check items
     */
    @PostMapping("/batch-approve")
    public Result<Integer> batchApprove(
            @RequestParam String checkMan,
            @RequestParam String checkResult,
            @RequestParam(required = false) String checkResultDesc,
            @RequestParam(required = false) Long taskItemId,
            @RequestParam(required = false) Long taskId) {
        try {
            int affected = applyCheckTaskService.batchApprove(
                    checkMan, checkResult, checkResultDesc, taskItemId, taskId);
            return Result.success(affected);
        } catch (Exception e) {
            logger.error("Error batch approving items", e);
            return Result.error("批量审批失败: " + e.getMessage());
        }
    }

    /**
     * Get unchecked items for a task
     */
    @GetMapping("/{taskId}/unchecked-items")
    public Result<List<ApplyCheckTaskItem>> getUncheckedItems(@PathVariable Long taskId) {
        try {
            List<ApplyCheckTaskItem> items = applyCheckTaskItemService.getUncheckedItems(taskId);
            return Result.success(items);
        } catch (Exception e) {
            logger.error("Error getting unchecked items: TaskID={}", taskId, e);
            return Result.error("获取未审批项失败: " + e.getMessage());
        }
    }

    /**
     * Get checked items for a task
     */
    @GetMapping("/{taskId}/checked-items")
    public Result<List<ApplyCheckTaskItem>> getCheckedItems(@PathVariable Long taskId) {
        try {
            List<ApplyCheckTaskItem> items = applyCheckTaskItemService.getCheckedItems(taskId);
            return Result.success(items);
        } catch (Exception e) {
            logger.error("Error getting checked items: TaskID={}", taskId, e);
            return Result.error("获取已审批项失败: " + e.getMessage());
        }
    }

    /**
     * Re-check an item (second-level approval)
     */
    @PostMapping("/items/{taskItemId}/recheck")
    public Result<ApplyCheckTaskItem> reCheckItem(
            @PathVariable Long taskItemId,
            @RequestParam String reCheckMan) {
        try {
            ApplyCheckTaskItem item = applyCheckTaskItemService.reCheckItem(taskItemId, reCheckMan);
            if (item == null) {
                return Result.error("抽查项不存在");
            }
            return Result.success(item);
        } catch (Exception e) {
            logger.error("Error re-checking item: TaskItemID={}", taskItemId, e);
            return Result.error("复检失败: " + e.getMessage());
        }
    }

    /**
     * Delete a check item
     */
    @DeleteMapping("/items/{taskItemId}")
    public Result<Boolean> deleteItem(@PathVariable Long taskItemId) {
        try {
            boolean deleted = applyCheckTaskItemService.deleteItem(taskItemId);
            return Result.success(deleted);
        } catch (Exception e) {
            logger.error("Error deleting item: TaskItemID={}", taskItemId, e);
            return Result.error("删除抽查项失败: " + e.getMessage());
        }
    }
}
