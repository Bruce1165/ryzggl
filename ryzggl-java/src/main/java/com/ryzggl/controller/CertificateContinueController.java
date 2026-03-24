package com.ryzggl.controller;

import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.common.Result;
import com.ryzggl.entity.CertificateContinue;
import com.ryzggl.service.CertificateContinueService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

/**
 * CertificateContinue Controller - 证书延续REST API
 */
@RestController
@RequestMapping("/api/v1/certificate/continue")
public class CertificateContinueController {

    @Autowired
    private CertificateContinueService certificateContinueService;

    /**
     * Create new certificate continue
     */
    @PostMapping
    public Result<CertificateContinue> create(@RequestBody CertificateContinue certificateContinue) {
        return certificateContinueService.createCertificateContinue(certificateContinue);
    }

    /**
     * Update certificate continue
     */
    @PutMapping("/{id}")
    public Result<CertificateContinue> update(@PathVariable Long id, @RequestBody CertificateContinue certificateContinue) {
        return certificateContinueService.updateCertificateContinue(certificateContinue);
    }

    /**
     * Delete certificate continue (soft delete)
     */
    @DeleteMapping("/{id}")
    public Result<Boolean> delete(@PathVariable Long id) {
        return certificateContinueService.deleteCertificateContinue(id);
    }

    /**
     * Get certificate continue by ID
     */
    @GetMapping("/{id}")
    public Result<CertificateContinue> getById(@PathVariable Long id) {
        return certificateContinueService.getCertificateContinueById(id);
    }

    /**
     * Get certificate continue list with pagination
     */
    @GetMapping("/list")
    public Result<IPage<CertificateContinue>> list(
            @RequestParam(defaultValue = "1") Integer current,
            @RequestParam(defaultValue = "10") Integer size,
            @RequestParam(required = false) Long certificateId,
            @RequestParam(required = false) String certificateCode,
            @RequestParam(required = false) String continueStatus,
            @RequestParam(required = false) String continueType) {
        return certificateContinueService.getCertificateContinuePage(current, size, certificateId, certificateCode, continueStatus, continueType);
    }

    /**
     * Get certificate continues by certificate ID
     */
    @GetMapping("/certificate/{certificateId}")
    public Result getCertificateContinuesById(@PathVariable Long certificateId) {
        return certificateContinueService.getCertificateContinuesById(certificateId);
    }

    /**
     * Get certificate continues by certificate code
     */
    @GetMapping("/code/{certificateCode}")
    public Result getCertificateContinuesByCode(@PathVariable String certificateCode) {
        return certificateContinueService.getCertificateContinuesByCode(certificateCode);
    }

    /**
     * Get certificate continues by status
     */
    @GetMapping("/status/{status}")
    public Result getCertificateContinuesByStatus(@PathVariable String status) {
        return certificateContinueService.getCertificateContinuesByStatus(status);
    }

    /**
     * Get expiring certificate continues
     */
    @GetMapping("/expiring")
    public Result getExpiringSoon() {
        return certificateContinueService.getExpiringSoon();
    }

    /**
     * Get certificate continue statistics
     */
    @GetMapping("/statistics")
    public Result<java.util.List<java.util.Map<String, Object>>> getStatisticsByType() {
        return certificateContinueService.getStatisticsByType();
    }

    /**
     * Process certificate continue - mark as completed
     */
    @PostMapping("/{id}/process")
    public Result<Boolean> processCertificateContinue(@PathVariable Long id) {
        return certificateContinueService.processCertificateContinue(id);
    }

    /**
     * Get certificate continue history
     */
    @GetMapping("/history")
    public Result<List<CertificateContinue>> getHistory(
            @RequestParam(required = false) Long certificateId,
            @RequestParam(required = false) String certificateCode) {
        if (certificateId != null) {
            return certificateContinueService.getCertificateContinuesById(certificateId);
        }
        if (certificateCode != null && !certificateCode.isEmpty()) {
            return certificateContinueService.getCertificateContinuesByCode(certificateCode);
        }
        return Result.error("请提供证书ID或证书编号");
    }
}
