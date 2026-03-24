package com.ryzggl.controller;

import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.common.Result;
import com.ryzggl.entity.CertificateMerge;
import com.ryzggl.service.CertificateMergeService;
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
 * CertificateMerge Controller - REST API Endpoints
 * Replaces .NET WebForms pages: zjsCertMergeList.aspx, zjsCertMergeChange.aspx
 */
@RestController
@RequestMapping("/api/certificate-merge")
@Tag(name = "Certificate Merge Management", description = "Certificate merge/split workflows")
public class CertificateMergeController {

    private static final Logger log = LoggerFactory.getLogger(CertificateMergeController.class);

    @Autowired
    private CertificateMergeService certificateMergeService;

    /**
     * Get certificate merge list with pagination
     * Replaces: zjsCertMergeList.aspx with Telerik RadGrid
     */
    @Operation(summary = "Get certificate merge list", description = "List certificate merge records with pagination and filtering")
    @GetMapping("/list")
    public Result<Page<CertificateMerge>> getCertificateMergeList(
            @RequestParam(defaultValue = "1") Integer current,
            @RequestParam(required = false) Long certificateId,
            @RequestParam(required = false) Long workerId,
            @RequestParam(required = false) String unitCode,
            @RequestParam(required = false) String status,
            @RequestParam(required = false) String mergeReason,
            @RequestParam(required = false) String keyword) {
        log.info("Getting certificate merges: page={}, certificateId={}, workerId={}, unitCode={}, status={}, mergeReason={}, keyword={}",
                current, certificateId, workerId, unitCode, status, mergeReason, keyword);

        com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<CertificateMerge> queryWrapper =
                new com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<>();

        if (certificateId != null) {
            queryWrapper.eq(CertificateMerge::getCertificateId, certificateId);
        }
        if (workerId != null) {
            queryWrapper.eq(CertificateMerge::getWorkerId, workerId);
        }
        if (unitCode != null) {
            queryWrapper.eq(CertificateMerge::getUnitCode, unitCode);
        }
        if (status != null) {
            queryWrapper.eq(CertificateMerge::getApplyStatus, status);
        }
        if (mergeReason != null) {
            queryWrapper.eq(CertificateMerge::getMergeReason, mergeReason);
        }
        if (keyword != null && !keyword.isEmpty()) {
            queryWrapper.and(wrapper -> wrapper
                    .like(CertificateMerge::getFromCertificateCode, keyword)
                    .or()
                    .like(CertificateMerge::getToCertificateCode, keyword)
                    .or()
                    .like(CertificateMerge::getWorkerName, keyword)
                    .or()
                    .like(CertificateMerge::getMergeReason, keyword));
        }

        Page<CertificateMerge> page = certificateMergeService.page(new Page<>(current, 10), queryWrapper);

        return Result.success(page);
    }

    /**
     * Create certificate merge record
     * Replaces: zjsCertMergeFirst.aspx
     */
    @Operation(summary = "Create certificate merge", description = "Create a new certificate merge record")
    @PostMapping
    public Result<Void> createCertificateMerge(@Valid @RequestBody CertificateMerge certificateMerge) {
        log.info("Creating certificate merge: from {} to {}", certificateMerge.getFromCertificateId(), certificateMerge.getToCertificateId());
        return certificateMergeService.createCertificateMerge(certificateMerge);
    }

    /**
     * Submit certificate merge for review
     * Replaces: Submit workflow in BasePage
     */
    @Operation(summary = "Submit certificate merge", description = "Submit certificate merge for approval review")
    @PutMapping("/{id}/submit")
    public Result<Void> submitMerge(@PathVariable Long id) {
        log.info("Submitting certificate merge: {}", id);
        return certificateMergeService.submitCertificateMerge(id);
    }

    /**
     * Approve certificate merge
     * Replaces: Approve workflow with CHECKMAN and CHECKADVISE
     */
    @Operation(summary = "Approve certificate merge", description = "Approve certificate merge")
    @PutMapping("/{id}/approve")
    public Result<Void> approveMerge(
            @PathVariable Long id,
            @Validated @RequestBody ApprovalRequest request) {
        log.info("Approving certificate merge: {}", id);
        return certificateMergeService.approveCertificateMerge(id, request.getCheckMan(), request.getCheckAdvise());
    }

    /**
     * Reject certificate merge
     * Replaces: Reject workflow with rejection reason
     */
    @Operation(summary = "Reject certificate merge", description = "Reject certificate merge with reason")
    @PutMapping("/{id}/reject")
    public Result<Void> rejectMerge(
            @PathVariable Long id,
            @Validated @RequestBody RejectionRequest request) {
        log.info("Rejecting certificate merge: {}", id);
        return certificateMergeService.rejectCertificateMerge(id, request.getRejectionReason());
    }

    /**
     * Get certificate merge by ID
     * Replaces: zjsCertMergeDetail.aspx
     */
    @Operation(summary = "Get certificate merge by ID", description = "Get detailed certificate merge information")
    @GetMapping("/{id}")
    public Result<CertificateMerge> getCertificateMergeById(@PathVariable Long id) {
        log.debug("Getting certificate merge: {}", id);
        return certificateMergeService.getCertificateMergeById(id);
    }

    /**
     * Get pending certificate merge records for approval
     * Replaces: CheckTask.GetPendingCertificateMerges
     */
    @Operation(summary = "Get pending certificate merges", description = "Get certificate merge records pending for approval")
    @GetMapping("/pending")
    public Result<List<CertificateMerge>> getPendingMerges() {
        log.debug("Getting pending certificate merge records");
        return certificateMergeService.getPendingCertificateMerges();
    }

    /**
     * Get certificate merge records by status
     * Replaces: zjsCertMergeList.aspx (filter by APPLYSTATUS)
     */
    @Operation(summary = "Get certificate merges by status", description = "Filter certificate merge records by status")
    @GetMapping("/status/{status}")
    public Result<List<CertificateMerge>> getMergesByStatus(@PathVariable String status) {
        log.debug("Getting certificate merges by status: {}", status);
        return certificateMergeService.getCertificateMergesByStatus(status);
    }

    /**
     * Get certificate merge records by certificate
     */
    @Operation(summary = "Get certificate merges by certificate", description = "Get all merge records for a specific certificate")
    @GetMapping("/certificate/{certificateId}")
    public Result<List<CertificateMerge>> getMergesByCertificate(@PathVariable Long certificateId) {
        log.debug("Getting certificate merges for certificate: {}", certificateId);
        return certificateMergeService.getCertificateMergesByCertificate(certificateId);
    }

    /**
     * Get recent certificate merge records
     */
    @Operation(summary = "Get recent certificate merges", description = "Get merge records from the last N days")
    @GetMapping("/recent")
    public Result<List<CertificateMerge>> getRecentMerges(@RequestParam(defaultValue = "30") int days) {
        log.debug("Getting recent certificate merges (last {} days)", days);
        return certificateMergeService.getRecentCertificateMerges(days);
    }

    /**
     * Inner class for approval request
     */
    @Schema(description = "Certificate merge approval request")
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
    @Schema(description = "Certificate merge rejection request")
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
