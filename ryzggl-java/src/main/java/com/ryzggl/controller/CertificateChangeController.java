package com.ryzggl.controller;

import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.common.Result;
import com.ryzggl.entity.CertificateChange;
import com.ryzggl.service.CertificateChangeService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.Map;

/**
 * CertificateChange Controller - 证书变更REST API
 */
@RestController
@RequestMapping("/api/v1/certificate/change")
public class CertificateChangeController {

    @Autowired
    private CertificateChangeService certificateChangeService;

    /**
     * Create new certificate change
     */
    @PostMapping
    public Result<CertificateChange> create(@RequestBody CertificateChange certificateChange) {
        return certificateChangeService.createCertificateChange(certificateChange);
    }

    /**
     * Update certificate change
     */
    @PutMapping("/{id}")
    public Result<CertificateChange> update(@PathVariable Long id, @RequestBody CertificateChange certificateChange) {
        return certificateChangeService.updateCertificateChange(certificateChange);
    }

    /**
     * Delete certificate change (soft delete)
     */
    @DeleteMapping("/{id}")
    public Result<Boolean> delete(@PathVariable Long id) {
        return certificateChangeService.deleteCertificateChange(id);
    }

    /**
     * Get certificate change by ID
     */
    @GetMapping("/{id}")
    public Result<CertificateChange> getById(@PathVariable Long id) {
        return certificateChangeService.getCertificateChangeById(id);
    }

    /**
     * Get certificate change list with pagination
     */
    @GetMapping("/list")
    public Result<IPage<CertificateChange>> list(
            @RequestParam(defaultValue = "1") Integer current,
            @RequestParam(defaultValue = "10") Integer size,
            @RequestParam(required = false) Long certificateId,
            @RequestParam(required = false) String certificateCode,
            @RequestParam(required = false) String changeType) {
        return certificateChangeService.getCertificateChangePage(current, size, certificateId, certificateCode, changeType);
    }

    /**
     * Get certificate changes by certificate ID
     */
    @GetMapping("/certificate/{certificateId}")
    public Result getCertificateChangesByCertificateId(@PathVariable Long certificateId) {
        return certificateChangeService.getCertificateChangesByCertificateId(certificateId);
    }

    /**
     * Get certificate changes by certificate code
     */
    @GetMapping("/code/{certificateCode}")
    public Result getCertificateChangesByCode(@PathVariable String certificateCode) {
        return certificateChangeService.getCertificateChangesByCode(certificateCode);
    }

    /**
     * Get certificate changes by date range
     */
    @GetMapping("/daterange")
    public Result getCertificateChangesByDateRange(@RequestParam String startDate,
                                              @RequestParam String endDate) {
        return certificateChangeService.getCertificateChangesByDateRange(startDate, endDate);
    }

    /**
     * Get certificate change statistics by type
     */
    @GetMapping("/statistics")
    public Result<java.util.List<java.util.Map<String, Object>>> getStatisticsByType() {
        return certificateChangeService.getStatisticsByType();
    }
}
