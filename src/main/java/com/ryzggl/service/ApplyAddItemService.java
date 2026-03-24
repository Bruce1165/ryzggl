package com.ryzggl.service;

import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.ryzggl.entity.ApplyAddItem;
import com.ryzggl.entity.Worker;
import com.ryzggl.repository.ApplyAddItemRepository;
import com.ryzggl.common.Result;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;

/**
 * ApplyAddItem Service - Business Logic Layer
 * Maps to: ApplyAddItemDAL.cs logic and workflow management
 */
@Service
@Transactional
public class ApplyAddItemService extends ServiceImpl<ApplyAddItemRepository, ApplyAddItem> {

    private static final Logger log = LoggerFactory.getLogger(ApplyAddItemService.class);

    @Autowired
    private WorkerService workerService;

    /**
     * Create additional item for application
     * Maps to: ApplyAddItemDAL.AddApplyAddItem
     */
    public Result<Void> createApplyAddItem(ApplyAddItem applyAddItem) {
        log.info("Creating additional item for apply: {}", applyAddItem.getApplyId());

        // Validate worker exists
        if (applyAddItem.getWorkerId() != null) {
            Worker worker = workerService.getById(applyAddItem.getWorkerId());
            if (worker == null) {
                log.error("Worker not found: {}", applyAddItem.getWorkerId());
                return Result.error("人员不存在");
            }
        }

        // Set initial status
        applyAddItem.setApplyStatus("未填写");

        boolean success = save(applyAddItem);

        if (success) {
            log.info("Additional item created successfully");
            return Result.success();
        } else {
            log.error("Failed to create additional item");
            return Result.error("附加项创建失败");
        }
    }

    /**
     * Get additional item by ID
     */
    public Result<ApplyAddItem> getApplyAddItemById(Long applyAddItemId) {
        log.debug("Getting additional item: {}", applyAddItemId);
        ApplyAddItem applyAddItem = getById(applyAddItemId);

        if (applyAddItem == null) {
            return Result.error("附加项不存在");
        }

        return Result.success(applyAddItem);
    }

    /**
     * List additional items for an application
     * Maps to: ApplyAddItemDAL.GetApplyAddItems
     */
    public Result<List<ApplyAddItem>> listApplyAddItems(Long applyId, Long workerId, String applyType) {
        log.debug("Listing additional items: applyId={}, workerId={}, applyType={}", applyId, workerId, applyType);

        com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<ApplyAddItem> queryWrapper =
                new com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<>();

        if (applyId != null) {
            queryWrapper.eq(ApplyAddItem::getApplyId, applyId);
        }
        if (workerId != null) {
            queryWrapper.eq(ApplyAddItem::getWorkerId, workerId);
        }
        if (applyType != null) {
            queryWrapper.eq(ApplyAddItem::getApplyType, applyType);
        }

        queryWrapper.orderByAsc(ApplyAddItem::getItemOrder)
                  .orderByDesc(ApplyAddItem::getCreateTime);

        List<ApplyAddItem> applyAddItems = list(queryWrapper);
        return Result.success(applyAddItems);
    }

    /**
     * Submit additional item for review
     * Maps to: ApplyAddItemDAL.SubmitApplyAddItem
     */
    public Result<Void> submitApplyAddItem(Long applyAddItemId) {
        log.info("Submitting additional item: {}", applyAddItemId);

        ApplyAddItem applyAddItem = getById(applyAddItemId);
        if (applyAddItem == null) {
            return Result.error("附加项不存在");
        }

        if (!"未填写".equals(applyAddItem.getApplyStatus())) {
            return Result.error("只有未填写的附加项才能提交");
        }

        applyAddItem.setApplyStatus("待确认");
        boolean success = updateById(applyAddItem);

        if (success) {
            log.info("Additional item submitted successfully");
            return Result.success();
        } else {
            return Result.error("附加项提交失败");
        }
    }

    /**
     * Approve additional item
     * Maps to: ApplyAddItemDAL.ApproveApplyAddItem
     */
    public Result<Void> approveApplyAddItem(Long applyAddItemId, String checkMan, String checkAdvise) {
        log.info("Approving additional item: {}, checkMan: {}, advise: {}", applyAddItemId, checkMan, checkAdvise);

        ApplyAddItem applyAddItem = getById(applyAddItemId);
        if (applyAddItem == null) {
            return Result.error("附加项不存在");
        }

        if (!"待确认".equals(applyAddItem.getApplyStatus())) {
            return Result.error("只有待确认的附加项才能通过");
        }

        applyAddItem.setApplyStatus("已受理");
        applyAddItem.setCheckMan(checkMan);
        applyAddItem.setCheckAdvise(checkAdvise);
        boolean success = updateById(applyAddItem);

        if (success) {
            log.info("Additional item approved successfully");
            return Result.success();
        } else {
            return Result.error("附加项审核失败");
        }
    }

    /**
     * Reject additional item
     * Maps to: ApplyAddItemDAL.RejectApplyAddItem
     */
    public Result<Void> rejectApplyAddItem(Long applyAddItemId, String rejectionReason) {
        log.info("Rejecting additional item: {}, reason: {}", applyAddItemId, rejectionReason);

        ApplyAddItem applyAddItem = getById(applyAddItemId);
        if (applyAddItem == null) {
            return Result.error("附加项不存在");
        }

        if (!"待确认".equals(applyAddItem.getApplyStatus())) {
            return Result.error("只有待确认的附加项才能驳回");
        }

        applyAddItem.setApplyStatus("已驳回");
        applyAddItem.setCheckAdvise(rejectionReason);
        boolean success = updateById(applyAddItem);

        if (success) {
            log.info("Additional item rejected successfully");
            return Result.success();
        } else {
            return Result.error("附加项驳回失败");
        }
    }

    /**
     * Get pending additional items for approval
     * Maps to: ApplyAddItemDAL.GetPendingApplyAddItems
     */
    public Result<List<ApplyAddItem>> getPendingApplyAddItems() {
        log.debug("Getting pending additional items");

        List<ApplyAddItem> applyAddItems = lambdaQuery()
                .eq(ApplyAddItem::getApplyStatus, "待确认")
                .orderByAsc(ApplyAddItem::getCreateTime)
                .list();

        return Result.success(applyAddItems);
    }

    /**
     * Get additional items by status
     */
    public Result<List<ApplyAddItem>> getApplyAddItemsByStatus(String status) {
        log.debug("Getting additional items by status: {}", status);

        List<ApplyAddItem> applyAddItems = lambdaQuery()
                .eq(ApplyAddItem::getApplyStatus, status)
                .orderByDesc(ApplyAddItem::getCreateTime)
                .list();

        return Result.success(applyAddItems);
    }

    /**
     * Get additional items by apply
     */
    public Result<List<ApplyAddItem>> getApplyAddItemsByApplyId(Long applyId) {
        log.debug("Getting additional items for apply: {}", applyId);

        List<ApplyAddItem> applyAddItems = baseMapper.findByApplyId(applyId);

        return Result.success(applyAddItems);
    }

    /**
     * Delete additional item
     */
    public Result<Void> deleteApplyAddItem(Long applyAddItemId) {
        log.info("Deleting additional item: {}", applyAddItemId);

        boolean success = removeById(applyAddItemId);

        if (success) {
            log.info("Additional item deleted successfully");
            return Result.success();
        } else {
            return Result.error("附加项删除失败");
        }
    }
}
