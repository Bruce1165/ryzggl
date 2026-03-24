package com.ryzggl.controller;

import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.common.Result;
import com.ryzggl.entity.Apply;
import com.ryzggl.service.ApplyService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.media.Schema;
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
 * Apply Controller - REST API Endpoints
 * Replaces .NET WebForms pages: zjsApplyList.aspx, zjsApplyChange.aspx, etc.
 */
@RestController
@RequestMapping("/api/apply")
@Tag(name = "Application Management", description = "Application workflows: create, submit, approve, reject")
public class ApplyController {

    private static final Logger log = LoggerFactory.getLogger(ApplyController.class);

    @Autowired
    private ApplyService applyService;

    /**
     * Get application list with pagination
     * Replaces: zjsApplyList.aspx with Telerik RadGrid and search
     */
    @Operation(summary = "Get application list", description = "List applications with pagination and filtering")
    @GetMapping("/list")
    public Result<Page<Apply>> getApplyList(
            @RequestParam(defaultValue = "1") Integer current,
            @RequestParam(required = false) Long workerId,
            @RequestParam(required = false) String unitCode,
            @RequestParam(required = false) String status,
            @RequestParam(required = false) String keyword) {
        log.info("Getting applications: page={}, workerId={}, unitCode={}, status={}, keyword={}",
                current, workerId, unitCode, status, keyword);

        com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<Apply> queryWrapper =
                new com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<>();

        if (workerId != null) {
            queryWrapper.eq(Apply::getWorkerId, workerId);
        }
        if (unitCode != null) {
            queryWrapper.eq(Apply::getUnitCode, unitCode);
        }
        if (status != null) {
            queryWrapper.eq(Apply::getApplyStatus, status);
        }
        if (keyword != null && !keyword.isEmpty()) {
            queryWrapper.and(wrapper -> wrapper
                    .like(Apply::getCheckMan, keyword)
                    .or()
                    .like(Apply::getCheckAdvise, keyword));
        }

        Page<Apply> page = applyService.page(new Page<>(current, 10), queryWrapper);

        return Result.success(page);
    }

    /**
     * Create new application
     * Replaces: zjsApplyFirst.aspx (initial registration)
     */
    @Operation(summary = "Create new application", description = "Create a new application for a worker")
    @PostMapping
    @PreAuthorize("hasAnyRole('APPLY_USER', 'APPLY_ADMIN')")
    public Result<Void> createApply(@Validated @RequestBody Apply apply) {
        log.info("Creating new application for worker: {}", apply.getWorkerId());
        return applyService.createApply(apply);
    }

    /**
     * Submit application for review
     * Replaces: Submit workflow in BasePage
     */
    @Operation(summary = "Submit application", description = "Submit application for approval review")
    @PutMapping("/{id}/submit")
    @PreAuthorize("hasAnyRole('APPLY_USER', 'APPLY_ADMIN')")
    public Result<Void> submitApplication(@PathVariable Long id) {
        log.info("Submitting application: {}", id);
        return applyService.submitApply(id);
    }

    /**
     * Approve application
     * Replaces: Approve workflow with CHECKMAN and CHECKADVISE
     */
    @Operation(summary = "Approve application", description = "Approve application for processing")
    @PutMapping("/{id}/approve")
    @PreAuthorize("hasAnyRole('APPLY_ADMIN', 'APPLY_ADMIN')")
    public Result<Void> approveApplication(
            @PathVariable Long id,
            @Validated @RequestBody ApprovalRequest request) {
        log.info("Approving application: {}", id);
        return applyService.approveApplication(id, request.getCheckMan(), request.getCheckAdvise());
    }

    /**
     * Reject application
     * Replaces: Reject workflow with rejection reason
     */
    @Operation(summary = "Reject application", description = "Reject application with reason")
    @PutMapping("/{id}/reject")
    @PreAuthorize("hasAnyRole('APPLY_USER', 'APPLY_ADMIN')")
    public Result<Void> rejectApplication(
            @PathVariable Long id,
            @Validated @RequestBody RejectionRequest request) {
        log.info("Rejecting application: {}", id);
        return applyService.rejectApplication(id, request.getRejectionReason());
    }

    /**
     * Get application by ID
     * Replaces: zjsApplyDetail.aspx
     */
    @Operation(summary = "Get application by ID", description = "Get detailed application information")
    @GetMapping("/{id}")
    @PreAuthorize("hasAnyRole('APPLY_USER', 'APPLY_ADMIN')")
    public Result<Apply> getApplyById(@PathVariable Long id) {
        log.debug("Getting application: {}", id);
        Apply apply = applyService.getById(id);
        return apply != null ? Result.success(apply) : Result.error("Application not found");
    }

    /**
     * Get pending applications for approval
     * Replaces: CheckTask.GetPendingApplications
     */
    @Operation(summary = "Get pending applications", description = "Get applications pending for approval")
    @GetMapping("/pending")
    @PreAuthorize("hasAnyRole('APPLY_ADMIN', 'APPLY_ADMIN')")
    public Result<List<Apply>> getPendingApplications() {
        log.debug("Getting pending applications");
        return applyService.getPendingApplications();
    }

    /**
     * Get applications by status
     * Replaces: zjsApplyList.aspx (filter by APPLYSTATUS)
     */
    @Operation(summary = "Get applications by status", description = "Filter applications by status")
    @GetMapping("/status/{status}")
    @PreAuthorize("hasAnyRole('APPLY_USER', 'APPLY_ADMIN')")
    public Result<List<Apply>> getApplicationsByStatus(@PathVariable String status) {
        log.debug("Getting applications by status: {}", status);
        return applyService.getApplicationsByStatus(status);
    }

    /**
     * Inner class for approval request
     */
    @io.swagger.v3.oas.annotations.media.Schema(description = "Application approval request")
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
    @io.swagger.v3.oas.annotations.media.Schema(description = "Application rejection request")
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
