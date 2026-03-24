package com.ryzggl.controller;

import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.common.Result;
import com.ryzggl.entity.ApplyContinue;
import com.ryzggl.service.ApplyContinueService;
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
 * ApplyContinue Controller - REST API Endpoints
 * Replaces .NET WebForms pages: zjsApplyContinueList.aspx, zjsApplyContinueChange.aspx
 */
@RestController
@RequestMapping("/api/apply-continue")
@Tag(name = "Continuation Application Management", description = "Certificate continuation application workflows")
public class ApplyContinueController {

    private static final Logger log = LoggerFactory.getLogger(ApplyContinueController.class);

    @Autowired
    private ApplyContinueService applyContinueService;

    /**
     * Get continuation application list with pagination
     * Replaces: zjsApplyContinueList.aspx with Telerik RadGrid
     */
    @Operation(summary = "Get continuation application list", description = "List continuation applications with pagination and filtering")
    @GetMapping("/list")
    public Result<Page<ApplyContinue>> getApplyContinueList(
            @RequestParam(defaultValue = "1") Integer current,
            @RequestParam(required = false) Long workerId,
            @RequestParam(required = false) String unitCode,
            @RequestParam(required = false) String status,
            @RequestParam(required = false) String keyword) {
        log.info("Getting continuation applications: page={}, workerId={}, unitCode={}, status={}, keyword={}",
                current, workerId, unitCode, status, keyword);

        com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<ApplyContinue> queryWrapper =
                new com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<>();

        if (workerId != null) {
            queryWrapper.eq(ApplyContinue::getWorkerId, workerId);
        }
        if (unitCode != null) {
            queryWrapper.eq(ApplyContinue::getUnitCode, unitCode);
        }
        if (status != null) {
            queryWrapper.eq(ApplyContinue::getApplyStatus, status);
        }
        if (keyword != null && !keyword.isEmpty()) {
            queryWrapper.and(wrapper -> wrapper
                    .like(ApplyContinue::getCertificateCode, keyword)
                    .or()
                    .like(ApplyContinue::getContinueReason, keyword));
        }

        Page<ApplyContinue> page = applyContinueService.page(new Page<>(current, 10), queryWrapper);

        return Result.success(page);
    }

    /**
     * Create new continuation application
     * Replaces: zjsApplyContinueFirst.aspx
     */
    @Operation(summary = "Create continuation application", description = "Create a new certificate continuation application")
    @PostMapping
    public Result<Void> createApplyContinue(@Valid @RequestBody ApplyContinue applyContinue) {
        log.info("Creating continuation application for certificate: {}", applyContinue.getCertificateId());
        return applyContinueService.createApplyContinue(applyContinue);
    }

    /**
     * Submit continuation application for review
     * Replaces: Submit workflow in BasePage
     */
    @Operation(summary = "Submit continuation application", description = "Submit continuation application for approval review")
    @PutMapping("/{id}/submit")
    public Result<Void> submitApplication(@PathVariable Long id) {
        log.info("Submitting continuation application: {}", id);
        return applyContinueService.submitApplyContinue(id);
    }

    /**
     * Approve continuation application
     * Replaces: Approve workflow with CHECKMAN and CHECKADVISE
     */
    @Operation(summary = "Approve continuation application", description = "Approve continuation application and update certificate validity")
    @PutMapping("/{id}/approve")
    public Result<Void> approveApplication(
            @PathVariable Long id,
            @Validated @RequestBody ApprovalRequest request) {
        log.info("Approving continuation application: {}", id);
        return applyContinueService.approveApplyContinue(id, request.getCheckMan(), request.getCheckAdvise());
    }

    /**
     * Reject continuation application
     * Replaces: Reject workflow with rejection reason
     */
    @Operation(summary = "Reject continuation application", description = "Reject continuation application with reason")
    @PutMapping("/{id}/reject")
    public Result<Void> rejectApplication(
            @PathVariable Long id,
            @Validated @RequestBody RejectionRequest request) {
        log.info("Rejecting continuation application: {}", id);
        return applyContinueService.rejectApplyContinue(id, request.getRejectionReason());
    }

    /**
     * Get continuation application by ID
     * Replaces: zjsApplyContinueDetail.aspx
     */
    @Operation(summary = "Get continuation application by ID", description = "Get detailed continuation application information")
    @GetMapping("/{id}")
    public Result<ApplyContinue> getApplyContinueById(@PathVariable Long id) {
        log.debug("Getting continuation application: {}", id);
        return applyContinueService.getApplyContinueById(id);
    }

    /**
     * Get pending continuation applications for approval
     * Replaces: CheckTask.GetPendingApplyContinues
     */
    @Operation(summary = "Get pending continuation applications", description = "Get continuation applications pending for approval")
    @GetMapping("/pending")
    public Result<List<ApplyContinue>> getPendingApplications() {
        log.debug("Getting pending continuation applications");
        return applyContinueService.getPendingApplyContinues();
    }

    /**
     * Get continuation applications by status
     * Replaces: zjsApplyContinueList.aspx (filter by APPLYSTATUS)
     */
    @Operation(summary = "Get continuation applications by status", description = "Filter continuation applications by status")
    @GetMapping("/status/{status}")
    public Result<List<ApplyContinue>> getApplicationsByStatus(@PathVariable String status) {
        log.debug("Getting continuation applications by status: {}", status);
        return applyContinueService.getApplyContinuesByStatus(status);
    }

    /**
     * Get continuation applications by certificate
     */
    @Operation(summary = "Get continuation applications by certificate", description = "Get all continuation applications for a specific certificate")
    @GetMapping("/certificate/{certificateId}")
    public Result<List<ApplyContinue>> getApplicationsByCertificate(@PathVariable Long certificateId) {
        log.debug("Getting continuation applications for certificate: {}", certificateId);
        return applyContinueService.getApplyContinuesByCertificate(certificateId);
    }

    /**
     * Get continuation applications by type
     */
    @Operation(summary = "Get continuation applications by type", description = "Filter continuation applications by continue type")
    @GetMapping("/type/{continueType}")
    public Result<List<ApplyContinue>> getApplicationsByType(@PathVariable String continueType) {
        log.debug("Getting continuation applications by type: {}", continueType);
        return applyContinueService.getApplyContinuesByType(continueType);
    }

    /**
     * Inner class for approval request
     */
    @Schema(description = "Continuation application approval request")
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
    @Schema(description = "Continuation application rejection request")
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
