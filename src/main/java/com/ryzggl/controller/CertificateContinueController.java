package com.ryzggl.controller;

import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.common.Result;
import com.ryzggl.entity.CertificateContinue;
import com.ryzggl.service.CertificateContinueService;
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
 * CertificateContinue Controller - REST API Endpoints
 * Replaces .NET WebForms pages: zjsCertContinueList.aspx, zjsCertContinueChange.aspx
 */
@RestController
@RequestMapping("/api/certificate-continue")
@Tag(name = "Certificate Continuation Management", description = "Certificate continuation workflows and tracking")
public class CertificateContinueController {

    private static final Logger log = LoggerFactory.getLogger(CertificateContinueController.class);

    @Autowired
    private CertificateContinueService certificateContinueService;

    /**
     * Get certificate continuation list with pagination
     * Replaces: zjsCertContinueList.aspx with Telerik RadGrid
     */
    @Operation(summary = "Get certificate continuation list", description = "List certificate continuation records with pagination and filtering")
    @GetMapping("/list")
    public Result<Page<CertificateContinue>> getCertificateContinueList(
            @RequestParam(defaultValue = "1") Integer current,
            @RequestParam(required = false) Long certificateId,
            @RequestParam(required = false) Long workerId,
            @RequestParam(required = false) String unitCode,
            @RequestParam(required = false) String status,
            @RequestParam(required = false) String keyword) {
        log.info("Getting certificate continuation records: page={}, certificateId={}, workerId={}, unitCode={}, status={}, keyword={}",
                current, certificateId, workerId, unitCode, status, keyword);

        com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<CertificateContinue> queryWrapper =
                new com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<>();

        if (certificateId != null) {
            queryWrapper.eq(CertificateContinue::getCertificateId, certificateId);
        }
        if (workerId != null) {
            queryWrapper.eq(CertificateContinue::getWorkerId, workerId);
        }
        if (unitCode != null) {
            queryWrapper.eq(CertificateContinue::getUnitCode, unitCode);
        }
        if (status != null) {
            queryWrapper.eq(CertificateContinue::getApplyStatus, status);
        }
        if (keyword != null && !keyword.isEmpty()) {
            queryWrapper.and(wrapper -> wrapper
                    .like(CertificateContinue::getCertificateCode, keyword)
                    .or()
                    .like(CertificateContinue::getWorkerName, keyword)
                    .or()
                    .like(CertificateContinue::getContinueReason, keyword));
        }

        Page<CertificateContinue> page = certificateContinueService.page(new Page<>(current, 10), queryWrapper);

        return Result.success(page);
    }

    /**
     * Create new certificate continuation record
     * Replaces: zjsCertContinueFirst.aspx
     */
    @Operation(summary = "Create certificate continuation", description = "Create a new certificate continuation record")
    @PostMapping
    public Result<Void> createCertificateContinue(@Valid @RequestBody CertificateContinue certificateContinue) {
        log.info("Creating certificate continuation record for certificate: {}", certificateContinue.getCertificateId());
        return certificateContinueService.createCertificateContinue(certificateContinue);
    }

    /**
     * Submit certificate continuation for review
     * Replaces: Submit workflow in BasePage
     */
    @Operation(summary = "Submit certificate continuation", description = "Submit certificate continuation for approval review")
    @PutMapping("/{id}/submit")
    public Result<Void> submitContinuation(@PathVariable Long id) {
        log.info("Submitting certificate continuation: {}", id);
        return certificateContinueService.submitCertificateContinue(id);
    }

    /**
     * Approve certificate continuation
     * Replaces: Approve workflow with CHECKMAN and CHECKADVISE
     */
    @Operation(summary = "Approve certificate continuation", description = "Approve certificate continuation and update certificate validity")
    @PutMapping("/{id}/approve")
    public Result<Void> approveContinuation(
            @PathVariable Long id,
            @Validated @RequestBody ApprovalRequest request) {
        log.info("Approving certificate continuation: {}", id);
        return certificateContinueService.approveCertificateContinue(id, request.getCheckMan(), request.getCheckAdvise());
    }

    /**
     * Reject certificate continuation
     * Replaces: Reject workflow with rejection reason
     */
    @Operation(summary = "Reject certificate continuation", description = "Reject certificate continuation with reason")
    @PutMapping("/{id}/reject")
    public Result<Void> rejectContinuation(
            @PathVariable Long id,
            @Validated @RequestBody RejectionRequest request) {
        log.info("Rejecting certificate continuation: {}", id);
        return certificateContinueService.rejectCertificateContinue(id, request.getRejectionReason());
    }

    /**
     * Get certificate continuation by ID
     * Replaces: zjsCertContinueDetail.aspx
     */
    @Operation(summary = "Get certificate continuation by ID", description = "Get detailed certificate continuation information")
    @GetMapping("/{id}")
    public Result<CertificateContinue> getCertificateContinueById(@PathVariable Long id) {
        log.debug("Getting certificate continuation: {}", id);
        return certificateContinueService.getCertificateContinueById(id);
    }

    /**
     * Get pending certificate continuation records for approval
     * Replaces: CheckTask.GetPendingCertificateContinues
     */
    @Operation(summary = "Get pending certificate continuations", description = "Get certificate continuation records pending for approval")
    @GetMapping("/pending")
    public Result<List<CertificateContinue>> getPendingContinuations() {
        log.debug("Getting pending certificate continuation records");
        return certificateContinueService.getPendingCertificateContinues();
    }

    /**
     * Get certificate continuation records by status
     * Replaces: zjsCertContinueList.aspx (filter by APPLYSTATUS)
     */
    @Operation(summary = "Get certificate continuations by status", description = "Filter certificate continuation records by status")
    @GetMapping("/status/{status}")
    public Result<List<CertificateContinue>> getContinuationsByStatus(@PathVariable String status) {
        log.debug("Getting certificate continuation records by status: {}", status);
        return certificateContinueService.getCertificateContinuesByStatus(status);
    }

    /**
     * Get certificate continuation records by certificate
     */
    @Operation(summary = "Get certificate continuations by certificate", description = "Get all continuation records for a specific certificate")
    @GetMapping("/certificate/{certificateId}")
    public Result<List<CertificateContinue>> getContinuationsByCertificate(@PathVariable Long certificateId) {
        log.debug("Getting certificate continuation records for certificate: {}", certificateId);
        return certificateContinueService.getCertificateContinuesByCertificate(certificateId);
    }

    /**
     * Get recent certificate continuation records
     */
    @Operation(summary = "Get recent certificate continuations", description = "Get continuation records from the last N days")
    @GetMapping("/recent")
    public Result<List<CertificateContinue>> getRecentContinuations(@RequestParam(defaultValue = "30") int days) {
        log.debug("Getting recent certificate continuation records (last {} days)", days);
        return certificateContinueService.getRecentCertificateContinues(days);
    }

    /**
     * Inner class for approval request
     */
    @Schema(description = "Certificate continuation approval request")
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
    @Schema(description = "Certificate continuation rejection request")
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
