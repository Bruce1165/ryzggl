package com.ryzggl.controller;

import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.common.Result;
import com.ryzggl.entity.ApplyAddItem;
import com.ryzggl.service.ApplyAddItemService;
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
 * ApplyAddItem Controller - REST API Endpoints
 * Replaces .NET WebForms pages: zjsApplyItemList.aspx, zjsApplyItemChange.aspx
 */
@RestController
@RequestMapping("/api/apply-additem")
@Tag(name = "Application Additional Item Management", description = "Additional item management for applications")
public class ApplyAddItemController {

    private static final Logger log = LoggerFactory.getLogger(ApplyAddItemController.class);

    @Autowired
    private ApplyAddItemService applyAddItemService;

    /**
     * Get additional item list with pagination
     * Replaces: zjsApplyItemList.aspx with Telerik RadGrid
     */
    @Operation(summary = "Get additional item list", description = "List additional items with pagination and filtering")
    @GetMapping("/list")
    public Result<Page<ApplyAddItem>> getApplyAddItemList(
            @RequestParam(defaultValue = "1") Integer current,
            @RequestParam(required = false) Long applyId,
            @RequestParam(required = false) Long workerId,
            @RequestParam(required = false) String applyType,
            @RequestParam(required = false) String itemType,
            @RequestParam(required = false) String status,
            @RequestParam(required = false) String keyword) {
        log.info("Getting additional items: page={}, applyId={}, workerId={}, applyType={}, itemType={}, status={}, keyword={}",
                current, applyId, workerId, applyType, itemType, status, keyword);

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
        if (itemType != null) {
            queryWrapper.eq(ApplyAddItem::getItemType, itemType);
        }
        if (status != null) {
            queryWrapper.eq(ApplyAddItem::getApplyStatus, status);
        }
        if (keyword != null && !keyword.isEmpty()) {
            queryWrapper.and(wrapper -> wrapper
                    .like(ApplyAddItem::getItemName, keyword)
                    .or()
                    .like(ApplyAddItem::getItemDescription, keyword));
        }

        queryWrapper.orderByAsc(ApplyAddItem::getItemOrder)
                  .orderByDesc(ApplyAddItem::getCreateTime);

        Page<ApplyAddItem> page = applyAddItemService.page(new Page<>(current, 10), queryWrapper);

        return Result.success(page);
    }

    /**
     * Create additional item
     * Replaces: zjsApplyItemFirst.aspx
     */
    @Operation(summary = "Create additional item", description = "Create a new additional item for application")
    @PostMapping
    public Result<Void> createApplyAddItem(@Valid @RequestBody ApplyAddItem applyAddItem) {
        log.info("Creating additional item for apply: {}", applyAddItem.getApplyId());
        return applyAddItemService.createApplyAddItem(applyAddItem);
    }

    /**
     * Submit additional item for review
     * Replaces: Submit workflow in BasePage
     */
    @Operation(summary = "Submit additional item", description = "Submit additional item for approval review")
    @PutMapping("/{id}/submit")
    public Result<Void> submitItem(@PathVariable Long id) {
        log.info("Submitting additional item: {}", id);
        return applyAddItemService.submitApplyAddItem(id);
    }

    /**
     * Approve additional item
     * Replaces: Approve workflow with CHECKMAN and CHECKADVISE
     */
    @Operation(summary = "Approve additional item", description = "Approve additional item")
    @PutMapping("/{id}/approve")
    public Result<Void> approveItem(
            @PathVariable Long id,
            @Validated @RequestBody ApprovalRequest request) {
        log.info("Approving additional item: {}", id);
        return applyAddItemService.approveApplyAddItem(id, request.getCheckMan(), request.getCheckAdvise());
    }

    /**
     * Reject additional item
     * Replaces: Reject workflow with rejection reason
     */
    @Operation(summary = "Reject additional item", description = "Reject additional item with reason")
    @PutMapping("/{id}/reject")
    public Result<Void> rejectItem(
            @PathVariable Long id,
            @Validated @RequestBody RejectionRequest request) {
        log.info("Rejecting additional item: {}", id);
        return applyAddItemService.rejectApplyAddItem(id, request.getRejectionReason());
    }

    /**
     * Delete additional item
     */
    @Operation(summary = "Delete additional item", description = "Delete an additional item")
    @DeleteMapping("/{id}")
    public Result<Void> deleteItem(@PathVariable Long id) {
        log.info("Deleting additional item: {}", id);
        return applyAddItemService.deleteApplyAddItem(id);
    }

    /**
     * Get additional item by ID
     * Replaces: zjsApplyItemDetail.aspx
     */
    @Operation(summary = "Get additional item by ID", description = "Get detailed additional item information")
    @GetMapping("/{id}")
    public Result<ApplyAddItem> getApplyAddItemById(@PathVariable Long id) {
        log.debug("Getting additional item: {}", id);
        return applyAddItemService.getApplyAddItemById(id);
    }

    /**
     * Get additional items by apply
     */
    @Operation(summary = "Get additional items by apply", description = "Get all additional items for a specific application")
    @GetMapping("/apply/{applyId}")
    public Result<List<ApplyAddItem>> getItemsByApply(@PathVariable Long applyId) {
        log.debug("Getting additional items for apply: {}", applyId);
        return applyAddItemService.getApplyAddItemsByApplyId(applyId);
    }

    /**
     * Get pending additional items for approval
     * Replaces: CheckTask.GetPendingApplyAddItems
     */
    @Operation(summary = "Get pending additional items", description = "Get additional items pending for approval")
    @GetMapping("/pending")
    public Result<List<ApplyAddItem>> getPendingItems() {
        log.debug("Getting pending additional items");
        return applyAddItemService.getPendingApplyAddItems();
    }

    /**
     * Get additional items by status
     * Replaces: zjsApplyItemList.aspx (filter by APPLYSTATUS)
     */
    @Operation(summary = "Get additional items by status", description = "Filter additional items by status")
    @GetMapping("/status/{status}")
    public Result<List<ApplyAddItem>> getItemsByStatus(@PathVariable String status) {
        log.debug("Getting additional items by status: {}", status);
        return applyAddItemService.getApplyAddItemsByStatus(status);
    }

    /**
     * Inner class for approval request
     */
    @Schema(description = "Additional item approval request")
    public static class ApprovalRequest {
        @Schema(description = "Checker/Reviewer name", required = true)
        private String checkMan;

        @Schema(description = "Review/Check advice")
        private String checkAdvise;

        public String getCheckMan() {
            return checkMan;
        }

        public void setCheckMan(String checkMan) {
            this.checkMan = checkMan;
        }

        public String getCheckAdvise() {
            return checkAdvise;
        }

        public void setCheckAdvise(String checkAdvise) {
            this.checkAdvise = checkAdvise;
        }
    }

    /**
     * Inner class for rejection request
     */
    @Schema(description = "Additional item rejection request")
    public static class RejectionRequest extends ApprovalRequest {
        @Schema(description = "Rejection reason", required = true)
        private String rejectionReason;

        public String getRejectionReason() {
            return rejectionReason;
        }

        public void setRejectionReason(String rejectionReason) {
            this.rejectionReason = rejectionReason;
        }
    }
}
