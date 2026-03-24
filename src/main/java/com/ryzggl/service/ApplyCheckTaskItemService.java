package com.ryzggl.service;

import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.ryzggl.entity.ApplyCheckTaskItem;
import com.ryzggl.entity.ApplyCheckTask;
import com.ryzggl.repository.ApplyCheckTaskItemRepository;
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
 * ApplyCheckTaskItem Service - Business Logic Layer
 * Maps to: ApplyCheckTaskItemDAL.cs logic and workflow management
 */
@Service
@Transactional
public class ApplyCheckTaskItemService extends ServiceImpl<ApplyCheckTaskItemRepository, ApplyCheckTaskItem> {

    private static final Logger log = LoggerFactory.getLogger(ApplyCheckTaskItemService.class);

    @Autowired
    private ApplyCheckTaskService applyCheckTaskService;

    /**
     * Create check task item
     * Maps to: ApplyCheckTaskItemDAL.AddApplyCheckTaskItem
     */
    public Result<Void> createApplyCheckTaskItem(ApplyCheckTaskItem applyCheckTaskItem) {
        log.info("Creating check task item for check task: {}", applyCheckTaskItem.getCheckTaskId());

        // Validate check task exists
        ApplyCheckTask checkTask = applyCheckTaskService.getById(applyCheckTaskItem.getCheckTaskId());
        if (checkTask == null) {
            log.error("Check task not found: {}", applyCheckTaskItem.getCheckTaskId());
            return Result.error("审批任务不存在");
        }

        // Set initial item status
        applyCheckTaskItem.setItemStatus("待审核");

        boolean success = save(applyCheckTaskItem);

        if (success) {
            log.info("Check task item created successfully");
            return Result.success();
        } else {
            log.error("Failed to create check task item");
            return Result.error("审批任务项创建失败");
        }
    }

    /**
     * Get check task item by ID
     */
    public Result<ApplyCheckTaskItem> getApplyCheckTaskItemById(Long checkTaskItemId) {
        log.debug("Getting check task item: {}", checkTaskItemId);
        ApplyCheckTaskItem applyCheckTaskItem = getById(checkTaskItemId);

        if (applyCheckTaskItem == null) {
            return Result.error("审批任务项不存在");
        }

        return Result.success(applyCheckTaskItem);
    }

    /**
     * List check task items
     * Maps to: ApplyCheckTaskItemDAL.GetApplyCheckTaskItems
     */
    public Result<List<ApplyCheckTaskItem>> listApplyCheckTaskItems(Long checkTaskId, Long applyId, Long workerId) {
        log.debug("Listing check task items: checkTaskId={}, applyId={}, workerId={}", checkTaskId, applyId, workerId);

        com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<ApplyCheckTaskItem> queryWrapper =
                new com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<>();

        if (checkTaskId != null) {
            queryWrapper.eq(ApplyCheckTaskItem::getCheckTaskId, checkTaskId);
        }
        if (applyId != null) {
            queryWrapper.eq(ApplyCheckTaskItem::getApplyId, applyId);
        }
        if (workerId != null) {
            queryWrapper.eq(ApplyCheckTaskItem::getWorkerId, workerId);
        }

        queryWrapper.orderByAsc(ApplyCheckTaskItem::getItemOrder)
                  .orderByDesc(ApplyCheckTaskItem::getCreateTime);

        List<ApplyCheckTaskItem> applyCheckTaskItems = list(queryWrapper);
        return Result.success(applyCheckTaskItems);
    }

    /**
     * Complete check task item
     * Maps to: ApplyCheckTaskItemDAL.CompleteItem
     */
    public Result<Void> completeItem(Long checkTaskItemId, String checkAdvise, String checkMan) {
        log.info("Completing check task item: {}, checkMan: {}", checkTaskItemId, checkMan);

        ApplyCheckTaskItem applyCheckTaskItem = getById(checkTaskItemId);
        if (applyCheckTaskItem == null) {
            return Result.error("审批任务项不存在");
        }

        if (!"待审核".equals(applyCheckTaskItem.getItemStatus())) {
            return Result.error("只有待审核的任务项才能完成");
        }

        applyCheckTaskItem.setItemStatus("已完成");
        applyCheckTaskItem.setCheckAdvise(checkAdvise);
        applyCheckTaskItem.setCheckMan(checkMan);
        applyCheckTaskItem.setCheckDate(LocalDate.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd")));
        boolean success = updateById(applyCheckTaskItem);

        if (success) {
            log.info("Check task item completed successfully");
            return Result.success();
        } else {
            return Result.error("任务项完成失败");
        }
    }

    /**
     * Reject check task item
     * Maps to: ApplyCheckTaskItemDAL.RejectItem
     */
    public Result<Void> rejectItem(Long checkTaskItemId, String rejectionReason, String checkMan) {
        log.info("Rejecting check task item: {}, reason: {}", checkTaskItemId, rejectionReason);

        ApplyCheckTaskItem applyCheckTaskItem = getById(checkTaskItemId);
        if (applyCheckTaskItem == null) {
            return Result.error("审批任务项不存在");
        }

        if (!"待审核".equals(applyCheckTaskItem.getItemStatus())) {
            return Result.error("只有待审核的任务项才能驳回");
        }

        applyCheckTaskItem.setItemStatus("已驳回");
        applyCheckTaskItem.setCheckAdvise(rejectionReason);
        applyCheckTaskItem.setCheckMan(checkMan);
        applyCheckTaskItem.setCheckDate(LocalDate.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd")));
        boolean success = updateById(applyCheckTaskItem);

        if (success) {
            log.info("Check task item rejected successfully");
            return Result.success();
        } else {
            return Result.error("任务项驳回失败");
        }
    }

    /**
     * Get pending check task items for approval
     * Maps to: ApplyCheckTaskItemDAL.GetPendingItems
     */
    public Result<List<ApplyCheckTaskItem>> getPendingItems() {
        log.debug("Getting pending check task items");

        List<ApplyCheckTaskItem> applyCheckTaskItems = baseMapper.findPendingItems();

        return Result.success(applyCheckTaskItems);
    }

    /**
     * Get check task items by status
     */
    public Result<List<ApplyCheckTaskItem>> getApplyCheckTaskItemsByStatus(String itemStatus) {
        log.debug("Getting check task items by status: {}", itemStatus);

        List<ApplyCheckTaskItem> applyCheckTaskItems = baseMapper.findByItemStatus(itemStatus);

        return Result.success(applyCheckTaskItems);
    }

    /**
     * Get check task items by check task
     */
    public Result<List<ApplyCheckTaskItem>> getItemsByCheckTaskId(Long checkTaskId) {
        log.debug("Getting check task items for check task: {}", checkTaskId);

        List<ApplyCheckTaskItem> applyCheckTaskItems = baseMapper.findByCheckTaskId(checkTaskId);

        return Result.success(applyCheckTaskItems);
    }

    /**
     * Delete check task item
     */
    public Result<Void> deleteApplyCheckTaskItem(Long checkTaskItemId) {
        log.info("Deleting check task item: {}", checkTaskItemId);

        boolean success = removeById(checkTaskItemId);

        if (success) {
            log.info("Check task item deleted successfully");
            return Result.success();
        } else {
            return Result.error("任务项删除失败");
        }
    }
}
