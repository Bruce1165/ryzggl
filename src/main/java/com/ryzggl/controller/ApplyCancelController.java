package com.ryzggl.controller;

import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.common.Result;
import com.ryzggl.entity.ApplyCancel;
import com.ryzggl.service.ApplyCancelService;
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
 * ApplyCancel Controller - REST API Endpoints
 * Replaces .NET WebForms pages: zjsApplyCancelList.aspx, zjsApplyCancelChange.aspx
 */
@RestController
@RequestMapping("/api/apply-cancel")
@Tag(name = "Cancellation Application Management", description = "Certificate cancellation application workflows")
public class ApplyCancelController {

    private static final Logger log = LoggerFactory.getLogger(ApplyCancelController.class);

    @Autowired
    private ApplyCancelService applyCancelService;

    /**
     * Get cancellation application list with pagination
     * Replaces: zjsApplyCancelList.aspx with Telerik RadGrid
     */
    @Operation(summary = "Get cancellation application list", description = "List cancellation applications with pagination and filtering")
    @GetMapping("/list")
    public Result<Page<ApplyCancel>> getApplyCancelList(
            @RequestParam(defaultValue = "1") Integer current,
            @RequestParam(required = false) Long workerId,
            @RequestParam(required = false) String unitCode,
            @RequestParam(required = false) String status,
            @RequestParam(required = false) String keyword) {
        log.info("Getting cancellation applications: page={}, workerId={}, unitCode={}, status={}, keyword={}",
                current, workerId, unitCode, status, keyword);

        com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<ApplyCancel> queryWrapper =
                new com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<>();

        if (workerId != null) {
            queryWrapper.eq(ApplyCancel::getWorkerId, workerId);
        }
        if (unitCode != null) {
            queryWrapper.eq(ApplyCancel::getUnitCode, unitCode);
        }
        if (status != null) {
            queryWrapper.eq(ApplyCancel::getApplyStatus, status);
        }
        if (keyword != null && !keyword.isEmpty()) {
            queryWrapper.and(wrapper -> wrapper
                    .like(ApplyCancel::getCertificateCode, keyword)
                    .or()
                    .like(ApplyCancel::getCancelReason, keyword));
        }

        Page<ApplyCancel> page = applyCancelService.page(new Page<>(current, 10), queryWrapper);

        return Result.success(page);
    }

    /**
     * Create new cancellation application
     * Replaces: zjsApplyCancelFirst.aspx
     */
    @Operation(summary = "Create cancellation application", description = "Create a new certificate cancellation application")
    @PostMapping
    public Result<Void> createApplyCancel(@Valid @RequestBody ApplyCancel applyCancel) {
        log.info("Creating cancellation application for certificate: {}", applyCancel.getCertificateId());
        return applyCancelService.createApplyCancel(applyCancel);
    }

    /**
     * Submit cancellation application for review
     * Replaces: Submit workflow in BasePage
     */
    @Operation(summary = "Submit cancellation application", description = "Submit cancellation application for approval review")
    @PutMapping("/{id}/submit")
    public Result<Void> submitApplication(@PathVariable Long id) {
        log.info("Submitting cancellation application: {}", id);
        return applyCancelService.submitApplyCancel(id);
    }

    /**
     * Approve cancellation application
     * Replaces: Approve workflow with CHECKMAN and CHECKADVISE
     */
    @Operation(summary = "Approve cancellation application", description = "Approve cancellation application and update certificate status")
    @PutMapping("/{id}/approve")
    public Result<Void> approveApplication(
            @PathVariable Long id,
            @Validated @RequestBody ApprovalRequest request) {
        log.info("Approving cancellation application: {}", id);
        return applyCancelService.approveApplyCancel(id, request.getCheckMan(), request.getCheckAdvise());
    }

    /**
     * Reject cancellation application
     * Replaces: Reject workflow with rejection reason
     */
    @Operation(summary = "Reject cancellation application", description = "Reject cancellation application with reason")
    @PutMapping("/{id}/reject")
    public Result<Void> rejectApplication(
            @PathVariable Long id,
            @Validated @RequestBody RejectionRequest request) {
        log.info("Rejecting cancellation application: {}", id);
        return applyCancelService.rejectApplyCancel(id, request.getRejectionReason());
    }

    /**
     * Get cancellation application by ID
     * Replaces: zjsApplyCancelDetail.aspx
     */
    @Operation(summary = "Get cancellation application by ID", description = "Get detailed cancellation application information")
    @GetMapping("/{id}")
    public Result<ApplyCancel> getApplyCancelById(@PathVariable Long id) {
        log.debug("Getting cancellation application: {}", id);
        return applyCancelService.getApplyCancelById(id);
    }

    /**
     * Get pending cancellation applications for approval
     * Replaces: CheckTask.GetPendingApplyCancels
     */
    @Operation(summary = "Get pending cancellation applications", description = "Get cancellation applications pending for approval")
    @GetMapping("/pending")
    public Result<List<ApplyCancel>> getPendingApplications() {
        log.debug("Getting pending cancellation applications");
        return applyCancelService.getPendingApplyCancels();
    }

    /**
     * Get cancellation applications by status
     * Replaces: zjsApplyCancelList.aspx (filter by APPLYSTATUS)
     */
    @Operation(summary = "Get cancellation applications by status", description = "Filter cancellation applications by status")
    @GetMapping("/status/{status}")
    public Result<List<ApplyCancel>> getApplicationsByStatus(@PathVariable String status) {
        log.debug("Getting cancellation applications by status: {}", status);
        return applyCancelService.getApplyCancelsByStatus(status);
    }

    /**
     * Get cancellation applications by certificate
     */
    @Operation(summary = "Get cancellation applications by certificate", description = "Get all cancellation applications for a specific certificate")
    @GetMapping("/certificate/{certificateId}")
    public Result<List<ApplyCancel>> getApplicationsByCertificate(@PathVariable Long certificateId) {
        log.debug("Getting cancellation applications for certificate: {}", certificateId);
        return applyCancelService.getApplyCancelsByCertificate(certificateId);
    }

    /**
     * Inner class for approval request
     */
    @Schema(description = "Cancellation application approval request")
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
    @Schema(description = "Cancellation application rejection request")
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
