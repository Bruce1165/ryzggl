package com.ryzggl.controller;

import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.common.Result;
import com.ryzggl.entity.CertificateOut;
import com.ryzggl.service.CertificateOutService;
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
 * CertificateOut Controller - REST API Endpoints
 * Replaces .NET WebForms pages: zjsCertOutList.aspx, zjsCertOutChange.aspx
 */
@RestController
@RequestMapping("/api/certificate-out")
@Tag(name = "Certificate Issuance Management", description = "Certificate issuance/print workflows")
public class CertificateOutController {

    private static final Logger log = LoggerFactory.getLogger(CertificateOutController.class);

    @Autowired
    private CertificateOutService certificateOutService;

    /**
     * Get certificate issuance list with pagination
     * Replaces: zjsCertOutList.aspx with Telerik RadGrid
     */
    @Operation(summary = "Get certificate issuance list", description = "List certificate issuance records with pagination and filtering")
    @GetMapping("/list")
    public Result<Page<CertificateOut>> getCertificateOutList(
            @RequestParam(defaultValue = "1") Integer current,
            @RequestParam(required = false) Long certificateId,
            @RequestParam(required = false) Long workerId,
            @RequestParam(required = false) String unitCode,
            @RequestParam(required = false) String status,
            @RequestParam(required = false) String keyword) {
        log.info("Getting certificate issuance records: page={}, certificateId={}, workerId={}, unitCode={}, status={}, keyword={}",
                current, certificateId, workerId, unitCode, status, keyword);

        com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<CertificateOut> queryWrapper =
                new com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<>();

        if (certificateId != null) {
            queryWrapper.eq(CertificateOut::getCertificateId, certificateId);
        }
        if (workerId != null) {
            queryWrapper.eq(CertificateOut::getWorkerId, workerId);
        }
        if (unitCode != null) {
            queryWrapper.eq(CertificateOut::getUnitCode, unitCode);
        }
        if (status != null) {
            queryWrapper.eq(CertificateOut::getApplyStatus, status);
        }
        if (keyword != null && !keyword.isEmpty()) {
            queryWrapper.and(wrapper -> wrapper
                    .like(CertificateOut::getCertificateCode, keyword)
                    .or()
                    .like(CertificateOut::getWorkerName, keyword)
                    .or()
                    .like(CertificateOut::getOutReason, keyword));
        }

        Page<CertificateOut> page = certificateOutService.page(new Page<>(current, 10), queryWrapper);

        return Result.success(page);
    }

    /**
     * Create certificate issuance record
     * Replaces: zjsCertOutFirst.aspx
     */
    @Operation(summary = "Create certificate issuance", description = "Create a new certificate issuance record")
    @PostMapping
    public Result<Void> createCertificateOut(@Valid @RequestBody CertificateOut certificateOut) {
        log.info("Creating certificate issuance record for certificate: {}", certificateOut.getCertificateId());
        return certificateOutService.createCertificateOut(certificateOut);
    }

    /**
     * Submit certificate issuance for review
     * Replaces: Submit workflow in BasePage
     */
    @Operation(summary = "Submit certificate issuance", description = "Submit certificate issuance for approval review")
    @PutMapping("/{id}/submit")
    public Result<Void> submitIssuance(@PathVariable Long id) {
        log.info("Submitting certificate issuance: {}", id);
        return certificateOutService.submitCertificateOut(id);
    }

    /**
     * Approve certificate issuance
     * Replaces: Approve workflow with CHECKMAN and CHECKADVISE
     */
    @Operation(summary = "Approve certificate issuance", description = "Approve certificate issuance for printing")
    @PutMapping("/{id}/approve")
    public Result<Void> approveIssuance(
            @PathVariable Long id,
            @Validated @RequestBody ApprovalRequest request) {
        log.info("Approving certificate issuance: {}", id);
        return certificateOutService.approveCertificateOut(id, request.getCheckMan(), request.getCheckAdvise());
    }

    /**
     * Print certificate
     * Replaces: Print workflow with PRINTMAN
     */
    @Operation(summary = "Print certificate", description = "Mark certificate as printed")
    @PutMapping("/{id}/print")
    public Result<Void> printCertificate(
            @PathVariable Long id,
            @Validated @RequestBody PrintRequest request) {
        log.info("Printing certificate: {}, printMan: {}", id, request.getPrintMan());
        return certificateOutService.printCertificate(id, request.getPrintMan());
    }

    /**
     * Reject certificate issuance
     * Replaces: Reject workflow with rejection reason
     */
    @Operation(summary = "Reject certificate issuance", description = "Reject certificate issuance with reason")
    @PutMapping("/{id}/reject")
    public Result<Void> rejectIssuance(
            @PathVariable Long id,
            @Validated @RequestBody RejectionRequest request) {
        log.info("Rejecting certificate issuance: {}", id);
        return certificateOutService.rejectCertificateOut(id, request.getRejectionReason());
    }

    /**
     * Get certificate issuance by ID
     * Replaces: zjsCertOutDetail.aspx
     */
    @Operation(summary = "Get certificate issuance by ID", description = "Get detailed certificate issuance information")
    @GetMapping("/{id}")
    public Result<CertificateOut> getCertificateOutById(@PathVariable Long id) {
        log.debug("Getting certificate issuance: {}", id);
        return certificateOutService.getCertificateOutById(id);
    }

    /**
     * Get pending certificate issuance records for approval
     * Replaces: CheckTask.GetPendingCertificateOuts
     */
    @Operation(summary = "Get pending certificate issuances", description = "Get certificate issuance records pending for approval")
    @GetMapping("/pending")
    public Result<List<CertificateOut>> getPendingIssuances() {
        log.debug("Getting pending certificate issuance records");
        return certificateOutService.getPendingCertificateOuts();
    }

    /**
     * Get certificate issuance records by status
     * Replaces: zjsCertOutList.aspx (filter by APPLYSTATUS)
     */
    @Operation(summary = "Get certificate issuances by status", description = "Filter certificate issuance records by status")
    @GetMapping("/status/{status}")
    public Result<List<CertificateOut>> getIssuancesByStatus(@PathVariable String status) {
        log.debug("Getting certificate issuance records by status: {}", status);
        return certificateOutService.getCertificateOutsByStatus(status);
    }

    /**
     * Get certificate issuance records by certificate
     */
    @Operation(summary = "Get certificate issuances by certificate", description = "Get all issuance records for a specific certificate")
    @GetMapping("/certificate/{certificateId}")
    public Result<List<CertificateOut>> getIssuancesByCertificate(@PathVariable Long certificateId) {
        log.debug("Getting certificate issuance records for certificate: {}", certificateId);
        return certificateOutService.getCertificateOutsByCertificate(certificateId);
    }

    /**
     * Get recent certificate issuance records
     */
    @Operation(summary = "Get recent certificate issuances", description = "Get issuance records from the last N days")
    @GetMapping("/recent")
    public Result<List<CertificateOut>> getRecentIssuances(@RequestParam(defaultValue = "30") int days) {
        log.debug("Getting recent certificate issuance records (last {} days)", days);
        return certificateOutService.getRecentCertificateOuts(days);
    }

    /**
     * Inner class for approval request
     */
    @Schema(description = "Certificate issuance approval request")
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
     * Inner class for print request
     */
    @Schema(description = "Certificate print request")
    public static class PrintRequest {
        @Schema(description = "Print person name", required = true)
        private String printMan;

        public String getPrintMan() {
            return printMan;
        }

        public void setPrintMan(String printMan) {
            this.printMan = printMan;
        }
    }

    /**
     * Inner class for rejection request
     */
    @Schema(description = "Certificate issuance rejection request")
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
