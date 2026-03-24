package com.ryzggl.controller;

import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.common.Result;
import com.ryzggl.entity.ApplyRenew;
import com.ryzggl.service.ApplyRenewService;
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
 * ApplyRenew Controller - REST API Endpoints
 * Replaces .NET WebForms pages: zjsApplyRenewList.aspx, zjsApplyRenewChange.aspx
 */
@RestController
@RequestMapping("/api/apply-renew")
@Tag(name = "Renewal Application Management", description = "Certificate renewal application workflows")
public class ApplyRenewController {

    private static final Logger log = LoggerFactory.getLogger(ApplyRenewController.class);

    @Autowired
    private ApplyRenewService applyRenewService;

    /**
     * Get renewal application list with pagination
     * Replaces: zjsApplyRenewList.aspx with Telerik RadGrid
     */
    @Operation(summary = "Get renewal application list", description = "List renewal applications with pagination and filtering")
    @GetMapping("/list")
    public Result<Page<ApplyRenew>> getApplyRenewList(
            @RequestParam(defaultValue = "1") Integer current,
            @RequestParam(required = false) Long workerId,
            @RequestParam(required = false) String unitCode,
            @RequestParam(required = false) String status,
            @RequestParam(required = false) String keyword) {
        log.info("Getting renewal applications: page={}, workerId={}, unitCode={}, status={}, keyword={}",
                current, workerId, unitCode, status, keyword);

        com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<ApplyRenew> queryWrapper =
                new com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<>();

        if (workerId != null) {
            queryWrapper.eq(ApplyRenew::getWorkerId, workerId);
        }
        if (unitCode != null) {
            queryWrapper.eq(ApplyRenew::getUnitCode, unitCode);
        }
        if (status != null) {
            queryWrapper.eq(ApplyRenew::getApplyStatus, status);
        }
        if (keyword != null && !keyword.isEmpty()) {
            queryWrapper.and(wrapper -> wrapper
                    .like(ApplyRenew::getCertificateCode, keyword)
                    .or()
                    .like(ApplyRenew::getRenewReason, keyword));
        }

        Page<ApplyRenew> page = applyRenewService.page(new Page<>(current, 10), queryWrapper);

        return Result.success(page);
    }

    /**
     * Create new renewal application
     * Replaces: zjsApplyRenewFirst.aspx
     */
    @Operation(summary = "Create renewal application", description = "Create a new certificate renewal application")
    @PostMapping
    public Result<Void> createApplyRenew(@Valid @RequestBody ApplyRenew applyRenew) {
        log.info("Creating renewal application for certificate: {}", applyRenew.getCertificateId());
        return applyRenewService.createApplyRenew(applyRenew);
    }

    /**
     * Submit renewal application for review
     * Replaces: Submit workflow in BasePage
     */
    @Operation(summary = "Submit renewal application", description = "Submit renewal application for approval review")
    @PutMapping("/{id}/submit")
    public Result<Void> submitApplication(@PathVariable Long id) {
        log.info("Submitting renewal application: {}", id);
        return applyRenewService.submitApplyRenew(id);
    }

    /**
     * Approve renewal application
     * Replaces: Approve workflow with CHECKMAN and CHECKADVISE
     */
    @Operation(summary = "Approve renewal application", description = "Approve renewal application and update certificate validity")
    @PutMapping("/{id}/approve")
    public Result<Void> approveApplication(
            @PathVariable Long id,
            @Validated @RequestBody ApprovalRequest request) {
        log.info("Approving renewal application: {}", id);
        return applyRenewService.approveApplyRenew(id, request.getCheckMan(), request.getCheckAdvise());
    }

    /**
     * Reject renewal application
     * Replaces: Reject workflow with rejection reason
     */
    @Operation(summary = "Reject renewal application", description = "Reject renewal application with reason")
    @PutMapping("/{id}/reject")
    public Result<Void> rejectApplication(
            @PathVariable Long id,
            @Validated @RequestBody RejectionRequest request) {
        log.info("Rejecting renewal application: {}", id);
        return applyRenewService.rejectApplyRenew(id, request.getRejectionReason());
    }

    /**
     * Get renewal application by ID
     * Replaces: zjsApplyRenewDetail.aspx
     */
    @Operation(summary = "Get renewal application by ID", description = "Get detailed renewal application information")
    @GetMapping("/{id}")
    public Result<ApplyRenew> getApplyRenewById(@PathVariable Long id) {
        log.debug("Getting renewal application: {}", id);
        return applyRenewService.getApplyRenewById(id);
    }

    /**
     * Get pending renewal applications for approval
     * Replaces: CheckTask.GetPendingApplyRenews
     */
    @Operation(summary = "Get pending renewal applications", description = "Get renewal applications pending for approval")
    @GetMapping("/pending")
    public Result<List<ApplyRenew>> getPendingApplications() {
        log.debug("Getting pending renewal applications");
        return applyRenewService.getPendingApplyRenews();
    }

    /**
     * Get renewal applications by status
     * Replaces: zjsApplyRenewList.aspx (filter by APPLYSTATUS)
     */
    @Operation(summary = "Get renewal applications by status", description = "Filter renewal applications by status")
    @GetMapping("/status/{status}")
    public Result<List<ApplyRenew>> getApplicationsByStatus(@PathVariable String status) {
        log.debug("Getting renewal applications by status: {}", status);
        return applyRenewService.getApplyRenewsByStatus(status);
    }

    /**
     * Get renewal applications by certificate
     */
    @Operation(summary = "Get renewal applications by certificate", description = "Get all renewal applications for a specific certificate")
    @GetMapping("/certificate/{certificateId}")
    public Result<List<ApplyRenew>> getApplicationsByCertificate(@PathVariable Long certificateId) {
        log.debug("Getting renewal applications for certificate: {}", certificateId);
        return applyRenewService.getApplyRenewsByCertificate(certificateId);
    }

    /**
     * Inner class for approval request
     */
    @Schema(description = "Renewal application approval request")
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
    @Schema(description = "Renewal application rejection request")
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
