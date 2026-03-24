package com.ryzggl.controller;

import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.common.Result;
import com.ryzggl.entity.CertificateHistory;
import com.ryzggl.service.CertificateHistoryService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

/**
 * CertificateHistory Controller - REST API Endpoints
 * Replaces .NET WebForms pages: zjsCertHistoryList.aspx
 */
@RestController
@RequestMapping("/api/certificate-history")
@Tag(name = "Certificate History Management", description = "Certificate change history tracking and audit trails")
public class CertificateHistoryController {

    private static final Logger log = LoggerFactory.getLogger(CertificateHistoryController.class);

    @Autowired
    private CertificateHistoryService certificateHistoryService;

    /**
     * Get certificate history list with pagination
     * Replaces: zjsCertHistoryList.aspx with Telerik RadGrid
     */
    @Operation(summary = "Get certificate history list", description = "List certificate history records with pagination and filtering")
    @GetMapping("/list")
    public Result<Page<CertificateHistory>> getCertificateHistoryList(
            @RequestParam(defaultValue = "1") Integer current,
            @RequestParam(required = false) Long certificateId,
            @RequestParam(required = false) Long workerId,
            @RequestParam(required = false) String unitCode,
            @RequestParam(required = false) String changeType,
            @RequestParam(required = false) String keyword) {
        log.info("Getting certificate history: page={}, certificateId={}, workerId={}, unitCode={}, changeType={}, keyword={}",
                current, certificateId, workerId, unitCode, changeType, keyword);

        com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<CertificateHistory> queryWrapper =
                new com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<>();

        if (certificateId != null) {
            queryWrapper.eq(CertificateHistory::getCertificateId, certificateId);
        }
        if (workerId != null) {
            queryWrapper.eq(CertificateHistory::getWorkerId, workerId);
        }
        if (unitCode != null) {
            queryWrapper.eq(CertificateHistory::getUnitCode, unitCode);
        }
        if (changeType != null) {
            queryWrapper.eq(CertificateHistory::getChangeType, changeType);
        }
        if (keyword != null && !keyword.isEmpty()) {
            queryWrapper.and(wrapper -> wrapper
                    .like(CertificateHistory::getCertificateCode, keyword)
                    .or()
                    .like(CertificateHistory::getWorkerName, keyword)
                    .or()
                    .like(CertificateHistory::getChangeDescription, keyword));
        }

        queryWrapper.orderByDesc(CertificateHistory::getChangeDate);

        Page<CertificateHistory> page = certificateHistoryService.page(new Page<>(current, 10), queryWrapper);

        return Result.success(page);
    }

    /**
     * Get certificate history by ID
     * Replaces: zjsCertHistoryDetail.aspx
     */
    @Operation(summary = "Get certificate history by ID", description = "Get detailed certificate history information")
    @GetMapping("/{id}")
    public Result<CertificateHistory> getCertificateHistoryById(@PathVariable Long id) {
        log.debug("Getting certificate history: {}", id);
        return certificateHistoryService.getCertificateHistoryById(id);
    }

    /**
     * Get certificate history by certificate
     */
    @Operation(summary = "Get certificate history by certificate", description = "Get all history records for a specific certificate")
    @GetMapping("/certificate/{certificateId}")
    public Result<List<CertificateHistory>> getHistoryByCertificate(@PathVariable Long certificateId) {
        log.debug("Getting certificate history for certificate: {}", certificateId);
        return certificateHistoryService.getCertificateHistoryByCertificate(certificateId);
    }

    /**
     * Get certificate history by worker
     */
    @Operation(summary = "Get certificate history by worker", description = "Get all history records for a specific worker")
    @GetMapping("/worker/{workerId}")
    public Result<List<CertificateHistory>> getHistoryByWorker(@PathVariable Long workerId) {
        log.debug("Getting certificate history for worker: {}", workerId);
        return certificateHistoryService.getCertificateHistoryByWorker(workerId);
    }

    /**
     * Get certificate history by change type
     */
    @Operation(summary = "Get certificate history by change type", description = "Filter certificate history by change type")
    @GetMapping("/type/{changeType}")
    public Result<List<CertificateHistory>> getHistoryByChangeType(@PathVariable String changeType) {
        log.debug("Getting certificate history by type: {}", changeType);
        return certificateHistoryService.getCertificateHistoryByChangeType(changeType);
    }

    /**
     * Get recent certificate history records
     */
    @Operation(summary = "Get recent certificate history", description = "Get history records from the last N days")
    @GetMapping("/recent")
    public Result<List<CertificateHistory>> getRecentHistory(@RequestParam(defaultValue = "30") int days) {
        log.debug("Getting recent certificate history (last {} days)", days);
        return certificateHistoryService.getRecentCertificateHistory(days);
    }
}
