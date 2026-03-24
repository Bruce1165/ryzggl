package com.ryzggl.controller;

import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.common.Result;
import com.ryzggl.entity.CertificatePause;
import com.ryzggl.service.CertificatePauseService;
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
 * CertificatePause Controller - REST API Endpoints
 * Replaces .NET WebForms pages: zjsCertPauseList.aspx, zjsCertPauseChange.aspx
 */
@RestController
@RequestMapping("/api/certificate-pause")
@Tag(name = "Certificate Pause Management", description = "Certificate pause/resume workflows")
public class CertificatePauseController {

    private static final Logger log = LoggerFactory.getLogger(CertificatePauseController.class);

    @Autowired
    private CertificatePauseService certificatePauseService;

    /**
     * Get certificate pause list with pagination
     * Replaces: zjsCertPauseList.aspx with Telerik RadGrid
     */
    @Operation(summary = "Get certificate pause list", description = "List certificate pause records with pagination and filtering")
    @GetMapping("/list")
    public Result<Page<CertificatePause>> getCertificatePauseList(
            @RequestParam(defaultValue = "1") Integer current,
            @RequestParam(required = false) Long certificateId,
            @RequestParam(required = false) Long workerId,
            @RequestParam(required = false) String unitCode,
            @RequestParam(required = false) String pauseStatus,
            @RequestParam(required = false) String keyword) {
        log.info("Getting certificate pauses: page={}, certificateId={}, workerId={}, unitCode={}, pauseStatus={}, keyword={}",
                current, certificateId, workerId, unitCode, pauseStatus, keyword);

        com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<CertificatePause> queryWrapper =
                new com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<>();

        if (certificateId != null) {
            queryWrapper.eq(CertificatePause::getCertificateId, certificateId);
        }
        if (workerId != null) {
            queryWrapper.eq(CertificatePause::getWorkerId, workerId);
        }
        if (unitCode != null) {
            queryWrapper.eq(CertificatePause::getUnitCode, unitCode);
        }
        if (pauseStatus != null) {
            queryWrapper.eq(CertificatePause::getPauseStatus, pauseStatus);
        }
        if (keyword != null && !keyword.isEmpty()) {
            queryWrapper.and(wrapper -> wrapper
                    .like(CertificatePause::getCertificateCode, keyword)
                    .or()
                    .like(CertificatePause::getWorkerName, keyword)
                    .or()
                    .like(CertificatePause::getPauseReason, keyword));
        }

        Page<CertificatePause> page = certificatePauseService.page(new Page<>(current, 10), queryWrapper);

        return Result.success(page);
    }

    /**
     * Create certificate pause record
     * Replaces: zjsCertPauseFirst.aspx
     */
    @Operation(summary = "Create certificate pause", description = "Create a new certificate pause record")
    @PostMapping
    public Result<Void> createCertificatePause(@Valid @RequestBody CertificatePause certificatePause) {
        log.info("Creating certificate pause for certificate: {}", certificatePause.getCertificateId());
        return certificatePauseService.createCertificatePause(certificatePause);
    }

    /**
     * Pause certificate
     */
    @Operation(summary = "Pause certificate", description = "Pause a certificate")
    @PostMapping("/pause")
    public Result<Void> pauseCertificate(@Valid @RequestBody PauseRequest request) {
        log.info("Pausing certificate: {}", request.getCertificateId());
        return certificatePauseService.pauseCertificate(
                request.getCertificateId(),
                request.getPauseReason(),
                request.getPauseMan()
        );
    }

    /**
     * Resume certificate
     */
    @Operation(summary = "Resume certificate", description = "Resume a paused certificate")
    @PutMapping("/{id}/resume")
    public Result<Void> resumeCertificate(
            @PathVariable Long id,
            @Validated @RequestBody ResumeRequest request) {
        log.info("Resuming certificate pause: {}", id);
        return certificatePauseService.resumeCertificate(id, request.getResumeReason(), request.getResumeMan());
    }

    /**
     * Get certificate pause by ID
     * Replaces: zjsCertPauseDetail.aspx
     */
    @Operation(summary = "Get certificate pause by ID", description = "Get detailed certificate pause information")
    @GetMapping("/{id}")
    public Result<CertificatePause> getCertificatePauseById(@PathVariable Long id) {
        log.debug("Getting certificate pause: {}", id);
        return certificatePauseService.getCertificatePauseById(id);
    }

    /**
     * Get currently paused certificates
     * Replaces: CheckTask.GetPausedCertificates
     */
    @Operation(summary = "Get paused certificates", description = "Get all currently paused certificates")
    @GetMapping("/paused")
    public Result<List<CertificatePause>> getPausedCertificates() {
        log.debug("Getting currently paused certificates");
        return certificatePauseService.getPausedCertificates();
    }

    /**
     * Get certificate pauses by status
     * Replaces: zjsCertPauseList.aspx (filter by PAUSESTATUS)
     */
    @Operation(summary = "Get certificate pauses by status", description = "Filter certificate pauses by status")
    @GetMapping("/status/{pauseStatus}")
    public Result<List<CertificatePause>> getPausesByStatus(@PathVariable String pauseStatus) {
        log.debug("Getting certificate pauses by status: {}", pauseStatus);
        return certificatePauseService.getCertificatePausesByStatus(pauseStatus);
    }

    /**
     * Get certificate pauses by certificate
     */
    @Operation(summary = "Get certificate pauses by certificate", description = "Get all pause records for a specific certificate")
    @GetMapping("/certificate/{certificateId}")
    public Result<List<CertificatePause>> getPausesByCertificate(@PathVariable Long certificateId) {
        log.debug("Getting certificate pauses for certificate: {}", certificateId);
        return certificatePauseService.getCertificatePausesByCertificate(certificateId);
    }

    /**
     * Get recent certificate pause records
     */
    @Operation(summary = "Get recent certificate pauses", description = "Get pause records from the last N days")
    @GetMapping("/recent")
    public Result<List<CertificatePause>> getRecentPauses(@RequestParam(defaultValue = "30") int days) {
        log.debug("Getting recent certificate pauses (last {} days)", days);
        return certificatePauseService.getRecentCertificatePauses(days);
    }

    /**
     * Inner class for pause request
     */
    @Schema(description = "Certificate pause request")
    public static class PauseRequest {
        @Schema(description = "Certificate ID", required = true)
        private Long certificateId;

        @Schema(description = "Pause reason", required = true)
        private String pauseReason;

        @Schema(description = "Pause person name", required = true)
        private String pauseMan;

        public Long getCertificateId() {
            return certificateId;
        }

        public void setCertificateId(Long certificateId) {
            this.certificateId = certificateId;
        }

        public String getPauseReason() {
            return pauseReason;
        }

        public void setPauseReason(String pauseReason) {
            this.pauseReason = pauseReason;
        }

        public String getPauseMan() {
            return pauseMan;
        }

        public void setPauseMan(String pauseMan) {
            this.pauseMan = pauseMan;
        }
    }

    /**
     * Inner class for resume request
     */
    @Schema(description = "Certificate resume request")
    public static class ResumeRequest {
        @Schema(description = "Resume reason", required = true)
        private String resumeReason;

        @Schema(description = "Resume person name", required = true)
        private String resumeMan;

        public String getResumeReason() {
            return resumeReason;
        }

        public void setResumeReason(String resumeReason) {
            this.resumeReason = resumeReason;
        }

        public String getResumeMan() {
            return resumeMan;
        }

        public void setResumeMan(String resumeMan) {
            this.resumeMan = resumeMan;
        }
    }
}
