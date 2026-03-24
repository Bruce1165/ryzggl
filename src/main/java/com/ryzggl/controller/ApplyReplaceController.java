package com.ryzggl.controller;

import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.common.Result;
import com.ryzggl.entity.ApplyReplace;
import com.ryzggl.service.ApplyReplaceService;
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
 * ApplyReplace Controller - REST API Endpoints
 * Replaces .NET WebForms pages: zjsApplyReplaceList.aspx, zjsApplyReplaceChange.aspx
 */
@RestController
@RequestMapping("/api/apply-replace")
@Tag(name = "Replacement Application Management", description = "Certificate replacement application workflows")
public class ApplyReplaceController {

    private static final Logger log = LoggerFactory.getLogger(ApplyReplaceController.class);

    @Autowired
    private ApplyReplaceService applyReplaceService;

    /**
     * Get replacement application list with pagination
     * Replaces: zjsApplyReplaceList.aspx with Telerik RadGrid
     */
    @Operation(summary = "Get replacement application list", description = "List replacement applications with pagination and filtering")
    @GetMapping("/list")
    public Result<Page<ApplyReplace>> getApplyReplaceList(
            @RequestParam(defaultValue = "1") Integer current,
            @RequestParam(required = false) Long workerId,
            @RequestParam(required = false) String unitCode,
            @RequestParam(required = false) String status,
            @RequestParam(required = false) String keyword) {
        log.info("Getting replacement applications: page={}, workerId={}, unitCode={}, status={}, keyword={}",
                current, workerId, unitCode, status, keyword);

        com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<ApplyReplace> queryWrapper =
                new com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<>();

        if (workerId != null) {
            queryWrapper.eq(ApplyReplace::getWorkerId, workerId);
        }
        if (unitCode != null) {
            queryWrapper.eq(ApplyReplace::getUnitCode, unitCode);
        }
        if (status != null) {
            queryWrapper.eq(ApplyReplace::getApplyStatus, status);
        }
        if (keyword != null && !keyword.isEmpty()) {
            queryWrapper.and(wrapper -> wrapper
                    .like(ApplyReplace::getCertificateCode, keyword)
                    .or()
                    .like(ApplyReplace::getOldCertificateCode, keyword)
                    .or()
                    .like(ApplyReplace::getReplaceReason, keyword));
        }

        Page<ApplyReplace> page = applyReplaceService.page(new Page<>(current, 10), queryWrapper);

        return Result.success(page);
    }

    /**
     * Create new replacement application
     * Replaces: zjsApplyReplaceFirst.aspx
     */
    @Operation(summary = "Create replacement application", description = "Create a new certificate replacement application")
    @PostMapping
    public Result<Void> createApplyReplace(@Valid @RequestBody ApplyReplace applyReplace) {
        log.info("Creating replacement application for certificate: {}", applyReplace.getCertificateId());
        return applyReplaceService.createApplyReplace(applyReplace);
    }

    /**
     * Submit replacement application for review
     * Replaces: Submit workflow in BasePage
     */
    @Operation(summary = "Submit replacement application", description = "Submit replacement application for approval review")
    @PutMapping("/{id}/submit")
    public Result<Void> submitApplication(@PathVariable Long id) {
        log.info("Submitting replacement application: {}", id);
        return applyReplaceService.submitApplyReplace(id);
    }

    /**
     * Approve replacement application
     * Replaces: Approve workflow with CHECKMAN and CHECKADVISE
     */
    @Operation(summary = "Approve replacement application", description = "Approve replacement application and update certificate code")
    @PutMapping("/{id}/approve")
    public Result<Void> approveApplication(
            @PathVariable Long id,
            @Validated @RequestBody ApprovalRequest request) {
        log.info("Approving replacement application: {}", id);
        return applyReplaceService.approveApplyReplace(id, request.getCheckMan(), request.getCheckAdvise());
    }

    /**
     * Reject replacement application
     * Replaces: Reject workflow with rejection reason
     */
    @Operation(summary = "Reject replacement application", description = "Reject replacement application with reason")
    @PutMapping("/{id}/reject")
    public Result<Void> rejectApplication(
            @PathVariable Long id,
            @Validated @RequestBody RejectionRequest request) {
        log.info("Rejecting replacement application: {}", id);
        return applyReplaceService.rejectApplyReplace(id, request.getRejectionReason());
    }

    /**
     * Get replacement application by ID
     * Replaces: zjsApplyReplaceDetail.aspx
     */
    @Operation(summary = "Get replacement application by ID", description = "Get detailed replacement application information")
    @GetMapping("/{id}")
    public Result<ApplyReplace> getApplyReplaceById(@PathVariable Long id) {
        log.debug("Getting replacement application: {}", id);
        return applyReplaceService.getApplyReplaceById(id);
    }

    /**
     * Get pending replacement applications for approval
     * Replaces: CheckTask.GetPendingApplyReplaces
     */
    @Operation(summary = "Get pending replacement applications", description = "Get replacement applications pending for approval")
    @GetMapping("/pending")
    public Result<List<ApplyReplace>> getPendingApplications() {
        log.debug("Getting pending replacement applications");
        return applyReplaceService.getPendingApplyReplaces();
    }

    /**
     * Get replacement applications by status
     * Replaces: zjsApplyReplaceList.aspx (filter by APPLYSTATUS)
     */
    @Operation(summary = "Get replacement applications by status", description = "Filter replacement applications by status")
    @GetMapping("/status/{status}")
    public Result<List<ApplyReplace>> getApplicationsByStatus(@PathVariable String status) {
        log.debug("Getting replacement applications by status: {}", status);
        return applyReplaceService.getApplyReplacesByStatus(status);
    }

    /**
     * Get replacement applications by certificate
     */
    @Operation(summary = "Get replacement applications by certificate", description = "Get all replacement applications for a specific certificate")
    @GetMapping("/certificate/{certificateId}")
    public Result<List<ApplyReplace>> getApplicationsByCertificate(@PathVariable Long certificateId) {
        log.debug("Getting replacement applications for certificate: {}", certificateId);
        return applyReplaceService.getApplyReplacesByCertificate(certificateId);
    }

    /**
     * Inner class for approval request
     */
    @Schema(description = "Replacement application approval request")
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
    @Schema(description = "Replacement application rejection request")
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
