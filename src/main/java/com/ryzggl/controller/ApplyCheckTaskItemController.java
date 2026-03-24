package com.ryzggl.controller;

import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.common.Result;
import com.ryzggl.entity.ApplyCheckTaskItem;
import com.ryzggl.service.ApplyCheckTaskItemService;
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
 * ApplyCheckTaskItem Controller - REST API Endpoints
 * Replaces .NET WebForms pages: zjsCheckTaskItemList.aspx, zjsCheckTaskItemChange.aspx
 */
@RestController
@RequestMapping("/api/apply-check-task-item")
@Tag(name = "Check Task Item Management", description = "Multi-step approval task item management")
public class ApplyCheckTaskItemController {

    private static final Logger log = LoggerFactory.getLogger(ApplyCheckTaskItemController.class);

    @Autowired
    private ApplyCheckTaskItemService applyCheckTaskItemService;

    /**
     * Get check task item list with pagination
     * Replaces: zjsCheckTaskItemList.aspx with Telerik RadGrid
     */
    @Operation(summary = "Get check task item list", description = "List check task items with pagination and filtering")
    @GetMapping("/list")
    public Result<Page<ApplyCheckTaskItem>> getApplyCheckTaskItemList(
            @RequestParam(defaultValue = "1") Integer current,
            @RequestParam(required = false) Long checkTaskId,
            @RequestParam(required = false) Long applyId,
            @RequestParam(required = false) Long workerId,
            @RequestParam(required = false) String applyType,
            @RequestParam(required = false) String itemStatus,
            @RequestParam(required = false) String keyword) {
        log.info("Getting check task items: page={}, checkTaskId={}, applyId={}, workerId={}, applyType={}, itemStatus={}, keyword={}",
                current, checkTaskId, applyId, workerId, applyType, itemStatus, keyword);

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
        if (applyType != null) {
            queryWrapper.eq(ApplyCheckTaskItem::getApplyType, applyType);
        }
        if (itemStatus != null) {
            queryWrapper.eq(ApplyCheckTaskItem::getItemStatus, itemStatus);
        }
        if (keyword != null && !keyword.isEmpty()) {
            queryWrapper.and(wrapper -> wrapper
                    .like(ApplyCheckTaskItem::getItemName, keyword)
                    .or()
                    .like(ApplyCheckTaskItem::getItemDescription, keyword)
                    .or()
                    .like(ApplyCheckTaskItem::getWorkerName, keyword));
        }

        queryWrapper.orderByAsc(ApplyCheckTaskItem::getItemOrder)
                  .orderByDesc(ApplyCheckTaskItem::getCreateTime);

        Page<ApplyCheckTaskItem> page = applyCheckTaskItemService.page(new Page<>(current, 10), queryWrapper);

        return Result.success(page);
    }

    /**
     * Create check task item
     * Replaces: zjsCheckTaskItemFirst.aspx
     */
    @Operation(summary = "Create check task item", description = "Create a new check task item")
    @PostMapping
    public Result<Void> createApplyCheckTaskItem(@Valid @RequestBody ApplyCheckTaskItem applyCheckTaskItem) {
        log.info("Creating check task item for check task: {}", applyCheckTaskItem.getCheckTaskId());
        return applyCheckTaskItemService.createApplyCheckTaskItem(applyCheckTaskItem);
    }

    /**
     * Complete check task item
     * Replaces: Complete workflow with CHECKADVISE
     */
    @Operation(summary = "Complete check task item", description = "Complete a check task item")
    @PutMapping("/{id}/complete")
    public Result<Void> completeItem(
            @PathVariable Long id,
            @Validated @RequestBody CompleteRequest request) {
        log.info("Completing check task item: {}", id);
        return applyCheckTaskItemService.completeItem(id, request.getCheckAdvise(), request.getCheckMan());
    }

    /**
     * Reject check task item
     * Replaces: Reject workflow with rejection reason
     */
    @Operation(summary = "Reject check task item", description = "Reject a check task item with reason")
    @PutMapping("/{id}/reject")
    public Result<Void> rejectItem(
            @PathVariable Long id,
            @Validated @RequestBody RejectRequest request) {
        log.info("Rejecting check task item: {}", id);
        return applyCheckTaskItemService.rejectItem(id, request.getRejectionReason(), request.getCheckMan());
    }

    /**
     * Delete check task item
     */
    @Operation(summary = "Delete check task item", description = "Delete a check task item")
    @DeleteMapping("/{id}")
    public Result<Void> deleteItem(@PathVariable Long id) {
        log.info("Deleting check task item: {}", id);
        return applyCheckTaskItemService.deleteApplyCheckTaskItem(id);
    }

    /**
     * Get check task item by ID
     * Replaces: zjsCheckTaskItemDetail.aspx
     */
    @Operation(summary = "Get check task item by ID", description = "Get detailed check task item information")
    @GetMapping("/{id}")
    public Result<ApplyCheckTaskItem> getApplyCheckTaskItemById(@PathVariable Long id) {
        log.debug("Getting check task item: {}", id);
        return applyCheckTaskItemService.getApplyCheckTaskItemById(id);
    }

    /**
     * Get pending check task items for approval
     * Replaces: CheckTask.GetPendingItems
     */
    @Operation(summary = "Get pending check task items", description = "Get check task items pending for approval")
    @GetMapping("/pending")
    public Result<List<ApplyCheckTaskItem>> getPendingItems() {
        log.debug("Getting pending check task items");
        return applyCheckTaskItemService.getPendingItems();
    }

    /**
     * Get check task items by status
     * Replaces: zjsCheckTaskItemList.aspx (filter by ITEMSTATUS)
     */
    @Operation(summary = "Get check task items by status", description = "Filter check task items by status")
    @GetMapping("/status/{itemStatus}")
    public Result<List<ApplyCheckTaskItem>> getItemsByStatus(@PathVariable String itemStatus) {
        log.debug("Getting check task items by status: {}", itemStatus);
        return applyCheckTaskItemService.getApplyCheckTaskItemsByStatus(itemStatus);
    }

    /**
     * Get check task items by check task
     */
    @Operation(summary = "Get check task items by check task", description = "Get all items for a specific check task")
    @GetMapping("/check-task/{checkTaskId}")
    public Result<List<ApplyCheckTaskItem>> getItemsByCheckTaskId(@PathVariable Long checkTaskId) {
        log.debug("Getting check task items for check task: {}", checkTaskId);
        return applyCheckTaskItemService.getItemsByCheckTaskId(checkTaskId);
    }

    /**
     * Inner class for complete request
     */
    @Schema(description = "Check task item complete request")
    public static class CompleteRequest {
        @Schema(description = "Check advice")
        private String checkAdvise;

        @Schema(description = "Check man", required = true)
        private String checkMan;

        public String getCheckAdvise() {
            return checkAdvise;
        }

        public void setCheckAdvise(String checkAdvise) {
            this.checkAdvise = checkAdvise;
        }

        public String getCheckMan() {
            return checkMan;
        }

        public void setCheckMan(String checkMan) {
            this.checkMan = checkMan;
        }
    }

    /**
     * Inner class for reject request
     */
    @Schema(description = "Check task item reject request")
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
