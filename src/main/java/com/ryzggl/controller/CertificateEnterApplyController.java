package com.ryzggl.controller;

import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.common.Result;
import com.ryzggl.entity.CertificateEnterApply;
import com.ryzggl.service.CertificateEnterApplyService;
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
 * CertificateEnterApply Controller - REST API Endpoints
 * Replaces .NET WebForms pages: zjsCertEnterList.aspx, zjsCertEnterChange.aspx
 */
@RestController
@RequestMapping("/api/certificate-enter")
@Tag(name = "New Certificate Application Management", description = "New certificate application workflows")
public class CertificateEnterApplyController {

    private static final Logger log = LoggerFactory.getLogger(CertificateEnterApplyController.class);

    @Autowired
    private CertificateEnterApplyService certificateEnterApplyService;

    /**
     * Get new certificate application list with pagination
     * Replaces: zjsCertEnterList.aspx with Telerik RadGrid
     */
    @Operation(summary = "Get new certificate application list", description = "List new certificate applications with pagination and filtering")
    @GetMapping("/list")
    public Result<Page<CertificateEnterApply>> getCertificateEnterApplyList(
            @RequestParam(defaultValue = "1") Integer current,
            @RequestParam(required = false) Long workerId,
            @RequestParam(required = false) String unitCode,
            @RequestParam(required = false) String status,
            @RequestParam(required = false) String keyword) {
        log.info("Getting new certificate applications: page={}, workerId={}, unitCode={}, status={}, keyword={}",
                current, workerId, unitCode, status, keyword);

        com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<CertificateEnterApply> queryWrapper =
                new com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<>();

        if (workerId != null) {
            queryWrapper.eq(CertificateEnterApply::getWorkerId, workerId);
        }
        if (unitCode != null) {
            queryWrapper.eq(CertificateEnterApply::getUnitCode, unitCode);
        }
        if (status != null) {
            queryWrapper.eq(CertificateEnterApply::getApplyStatus, status);
        }
        if (keyword != null && !keyword.isEmpty()) {
            queryWrapper.and(wrapper -> wrapper
                    .like(CertificateEnterApply::getCertificateCode, keyword)
                    .or()
                    .like(CertificateEnterApply::getWorkerName, keyword)
                    .or()
                    .like(CertificateEnterApply::getApplyReason, keyword));
        }

        Page<CertificateEnterApply> page = certificateEnterApplyService.page(new Page<>(current, 10), queryWrapper);

        return Result.success(page);
    }

    /**
     * Create new certificate application
     * Replaces: zjsCertEnterFirst.aspx
     */
    @Operation(summary = "Create new certificate application", description = "Create a new certificate application")
    @PostMapping
    public Result<Void> createCertificateEnterApply(@Valid @RequestBody CertificateEnterApply certificateEnterApply) {
        log.info("Creating new certificate application for worker: {}", certificateEnterApply.getWorkerId());
        return certificateEnterApplyService.createCertificateEnterApply(certificateEnterApply);
    }

    /**
     * Submit new certificate application for review
     * Replaces: Submit workflow in BasePage
     */
    @Operation(summary = "Submit new certificate application", description = "Submit new certificate application for approval review")
    @PutMapping("/{id}/submit")
    public Result<Void> submitApplication(@PathVariable Long id) {
        log.info("Submitting new certificate application: {}", id);
        return certificateEnterApplyService.submitCertificateEnterApply(id);
    }

    /**
     * Approve new certificate application
     * Replaces: Approve workflow with CHECKMAN and CHECKADVISE
     */
    @Operation(summary = "Approve new certificate application", description = "Approve new certificate application and create certificate")
    @PutMapping("/{id}/approve")
    public Result<Void> approveApplication(
            @PathVariable Long id,
            @Validated @RequestBody ApprovalRequest request) {
        log.info("Approving new certificate application: {}", id);
        return certificateEnterApplyService.approveCertificateEnterApply(id, request.getCheckMan(), request.getCheckAdvise());
    }

    /**
     * Reject new certificate application
     * Replaces: Reject workflow with rejection reason
     */
    @Operation(summary = "Reject new certificate application", description = "Reject new certificate application with reason")
    @PutMapping("/{id}/reject")
    public Result<Void> rejectApplication(
            @PathVariable Long id,
            @Validated @RequestBody RejectionRequest request) {
        log.info("Rejecting new certificate application: {}", id);
        return certificateEnterApplyService.rejectCertificateEnterApply(id, request.getRejectionReason());
    }

    /**
     * Get new certificate application by ID
     * Replaces: zjsCertEnterDetail.aspx
     */
    @Operation(summary = "Get new certificate application by ID", description = "Get detailed new certificate application information")
    @GetMapping("/{id}")
    public Result<CertificateEnterApply> getCertificateEnterApplyById(@PathVariable Long id) {
        log.debug("Getting new certificate application: {}", id);
        return certificateEnterApplyService.getCertificateEnterApplyById(id);
    }

    /**
     * Get pending new certificate applications for approval
     * Replaces: CheckTask.GetPendingCertificateEnterApplys
     */
    @Operation(summary = "Get pending new certificate applications", description = "Get new certificate applications pending for approval")
    @GetMapping("/pending")
    public Result<List<CertificateEnterApply>> getPendingApplications() {
        log.debug("Getting pending new certificate applications");
        return certificateEnterApplyService.getPendingCertificateEnterApplys();
    }

    /**
     * Get new certificate applications by status
     * Replaces: zjsCertEnterList.aspx (filter by APPLYSTATUS)
     */
    @Operation(summary = "Get new certificate applications by status", description = "Filter new certificate applications by status")
    @GetMapping("/status/{status}")
    public Result<List<CertificateEnterApply>> getApplicationsByStatus(@PathVariable String status) {
        log.debug("Getting new certificate applications by status: {}", status);
        return certificateEnterApplyService.getCertificateEnterApplysByStatus(status);
    }

    /**
     * Inner class for approval request
     */
    @Schema(description = "New certificate application approval request")
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
    @Schema(description = "New certificate application rejection request")
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
