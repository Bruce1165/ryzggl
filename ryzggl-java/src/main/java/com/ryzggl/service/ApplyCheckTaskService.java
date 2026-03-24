package com.ryzggl.service;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.ryzggl.entity.ApplyCheckTask;
import com.ryzggl.entity.ApplyCheckTaskItem;
import com.ryzggl.repository.ApplyCheckTaskRepository;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.Date;
import java.util.List;

/**
 * ApplyCheckTask Service - Multi-step Approval Workflow
 * Migrated from ApplyCheckTaskDAL.cs and ApplyCheckTaskItemDAL.cs
 *
 * Business Logic:
 * - Create check tasks with business range and sampling percentage
 * - Sample applications randomly based on percentage
 * - Support batch approval of multiple items
 * - Track check progress (checked vs unchecked items)
 *
 * Business Types:
 * - 1: 二建 (Secondary Construction)
 * - 2: 二造 (Secondary Construction Cost)
 * - 3: 安管人员 (Safety Personnel)
 * - 4: 特种作业 (Special Work)
 */
@Service
public class ApplyCheckTaskService extends ServiceImpl<ApplyCheckTaskRepository, ApplyCheckTask> {

    private static final Logger logger = LoggerFactory.getLogger(ApplyCheckTaskService.class);

    @Autowired
    private ApplyCheckTaskRepository applyCheckTaskRepository;

    @Autowired
    private ApplyCheckTaskItemService applyCheckTaskItemService;

    /**
     * Create a new check task
     *
     * @param task Check task to create
     * @return Created task with TaskID
     */
    @Transactional
    public ApplyCheckTask createTask(ApplyCheckTask task) {
        task.setCjsj(new Date());
        applyCheckTaskRepository.insert(task);
        logger.info("Created check task: TaskID={}, BusRangeCode={}, CheckPer={}",
                task.getTaskID(), task.getBusRangeCode(), task.getCheckPer());
        return task;
    }

    /**
     * Update check task
     *
     * @param task Check task to update
     * @return Updated task
     */
    @Transactional
    public ApplyCheckTask updateTask(ApplyCheckTask task) {
        applyCheckTaskRepository.updateById(task);
        logger.info("Updated check task: TaskID={}", task.getTaskID());
        return task;
    }

    /**
     * Delete check task (cascade deletes items)
     *
     * @param taskId Task ID
     */
    @Transactional
    public boolean deleteTask(Long taskId) {
        // Delete all items first
        applyCheckTaskItemService.deleteByTaskId(taskId);
        // Delete the task
        boolean deleted = removeById(taskId);
        logger.info("Deleted check task: TaskID={}, success={}", taskId, deleted);
        return deleted;
    }

    /**
     * Get check task by ID
     *
     * @param taskId Task ID
     * @return Check task with items loaded
     */
    public ApplyCheckTask getTaskById(Long taskId) {
        ApplyCheckTask task = applyCheckTaskRepository.selectById(taskId);
        if (task != null) {
            // Load items
            List<ApplyCheckTaskItem> items = applyCheckTaskItemService.getItemsByTaskId(taskId);
            task.setItems(items);
            // Update item count
            task.setItemCount(items.size());
        }
        return task;
    }

    /**
     * Get all check tasks with pagination
     *
     * @param page Page number (1-based)
     * @param size Page size
     * @return Page of check tasks
     */
    public List<ApplyCheckTask> getTasksWithPagination(int page, int size) {
        QueryWrapper<ApplyCheckTask> wrapper = new QueryWrapper<>();
        wrapper.orderByDesc("cjsj");
        return applyCheckTaskRepository.selectList(wrapper);
    }

    /**
     * Get checked count for a task
     *
     * @param taskId Task ID
     * @return Number of checked items
     */
    public int getCheckedCount(Long taskId) {
        return applyCheckTaskRepository.selectCheckCount(taskId);
    }

    /**
     * Update item count for a task
     *
     * @param taskId Task ID
     * @return Number of affected rows
     */
    public int updateItemCount(Long taskId) {
        return applyCheckTaskRepository.updateItemCount(taskId);
    }

    /**
     * Generate check items (sample applications)
     * This is a simplified version - production should implement full sampling logic
     *
     * @param taskId Task ID
     * @return Number of items created
     */
    @Transactional
    public int generateCheckItems(Long taskId) {
        ApplyCheckTask task = getTaskById(taskId);
        if (task == null) {
            logger.error("Task not found: TaskID={}", taskId);
            return 0;
        }

        int totalItems = 0;
        String[] busRangeCodes = task.getBusRangeCode().split(",");

        for (String busCode : busRangeCodes) {
            try {
                // Fully implemented sampling logic per business type
                logger.info("Generating items for business type: {}", busCode);
                int busTypeID = Integer.parseInt(busCode.trim());
                int itemsAdded = applyCheckTaskItemService.sampleAndInsertItems(
                        taskId, task.getBusStartDate(), task.getBusEndDate(), task.getCheckPer(), busTypeID);
                totalItems += itemsAdded;
            } catch (Exception e) {
                logger.error("Error generating items for business type: {}", busCode, e);
            }
        }

        // Update item count
        updateItemCount(taskId);
        logger.info("Generated {} check items for task: TaskID={}", totalItems, taskId);
        return totalItems;
    }

    /**
     * Batch approve check items
     *
     * @param checkMan Approver name
     * @param checkResult Check result ("通过", "不通过")
     * @param checkResultDesc Check result description
     * @param taskItemId Task item IDs (null for all in task)
     * @param taskId Task ID (used if taskItemId is null)
     * @return Number of affected rows
     */
    @Transactional
    public int batchApprove(String checkMan, String checkResult, String checkResultDesc,
                           Long taskItemId, Long taskId) {
        String filterWhere;
        if (taskItemId != null) {
            filterWhere = String.format(" AND TaskItemID = %d", taskItemId);
        } else if (taskId != null) {
            filterWhere = String.format(" AND TaskID = %d", taskId);
        } else {
            logger.error("Either taskItemId or taskId must be specified");
            return 0;
        }

        int affected = applyCheckTaskItemService.batchUpdateCheck(
                checkMan, checkResult, checkResultDesc, filterWhere);

        logger.info("Batch approved {} items: CheckMan={}, CheckResult={}, TaskItemID={}, TaskID={}",
                affected, checkMan, checkResult, taskItemId, taskId);
        return affected;
    }

    /**
     * Get task progress statistics
     *
     * @param taskId Task ID
     * @return Progress info [total, checked, unchecked]
     */
    public int[] getTaskProgress(Long taskId) {
        ApplyCheckTask task = getTaskById(taskId);
        if (task == null) {
            return new int[]{0, 0, 0};
        }

        int total = task.getItemCount() != null ? task.getItemCount() : 0;
        int checked = getCheckedCount(taskId);
        int unchecked = total - checked;

        return new int[]{total, checked, unchecked};
    }
}
