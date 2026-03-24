package com.ryzggl.controller;

import com.ryzggl.common.Result;
import com.ryzggl.entity.CertificateCAHistory;
import com.ryzggl.service.CertificateCAHistoryService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

/**
 * CertificateCAHistory Controller
 * 证书CA历史管理API
 */
@Tag(name = "证书CA历史管理", description = "证书CA历史管理相关接口")
@RestController
@RequestMapping("/api/v1/certificate-ca-history")
public class CertificateCAHistoryController {

    @Autowired
    private CertificateCAHistoryService certificateCAHistoryService;

    /**
     * Get all CA history records
     */
    @Operation(summary = "获取所有证书CA历史记录")
    @GetMapping("/all")
    public Result<List<CertificateCAHistory>> getAll() {
        List<CertificateCAHistory> list = certificateCAHistoryService.getAll();
        return Result.success(list);
    }

    /**
     * Get CA history by ID
     */
    @Operation(summary = "根据ID获取证书CA历史")
    @GetMapping("/{certificateCaId}")
    public Result<CertificateCAHistory> getById(@PathVariable String certificateCaId) {
        CertificateCAHistory history = certificateCAHistoryService.getById(certificateCaId);
        if (history == null) {
            return Result.error("证书CA历史不存在");
        }
        return Result.success(history);
    }

    /**
     * Get CA history by certificate ID
     */
    @Operation(summary = "根据证书ID获取CA历史")
    @GetMapping("/certificate/{certificateId}")
    public Result<List<CertificateCAHistory>> getByCertificateId(@PathVariable Long certificateId) {
        List<CertificateCAHistory> list = certificateCAHistoryService.getByCertificateId(certificateId);
        return Result.success(list);
    }

    /**
     * Search CA history
     */
    @Operation(summary = "搜索证书CA历史")
    @GetMapping("/search")
    public Result<List<CertificateCAHistory>> search(@RequestParam String keyword) {
        List<CertificateCAHistory> list = certificateCAHistoryService.search(keyword);
        return Result.success(list);
    }

    /**
     * Create CA history
     */
    @Operation(summary = "创建证书CA历史")
    @PostMapping
    public Result<CertificateCAHistory> create(@RequestBody CertificateCAHistory certificateCAHistory) {
        return certificateCAHistoryService.createCertificateCAHistory(certificateCAHistory);
    }

    /**
     * Update CA history
     */
    @Operation(summary = "更新证书CA历史")
    @PutMapping("/{certificateCaId}")
    public Result<CertificateCAHistory> update(@PathVariable String certificateCaId, @RequestBody CertificateCAHistory certificateCAHistory) {
        certificateCAHistory.setCertificateCaId(certificateCaId);
        return certificateCAHistoryService.updateCertificateCAHistory(certificateCAHistory);
    }

    /**
     * Delete CA history
     */
    @Operation(summary = "删除证书CA历史")
    @DeleteMapping("/{certificateCaId}")
    public Result<Void> delete(@PathVariable String certificateCaId) {
        return certificateCAHistoryService.deleteCertificateCAHistory(certificateCaId);
    }

    /**
     * Delete by certificate ID
     */
    @Operation(summary = "根据证书ID删除CA历史")
    @DeleteMapping("/certificate/{certificateId}")
    public Result<Void> deleteByCertificateId(@PathVariable Long certificateId) {
        return certificateCAHistoryService.deleteByCertificateId(certificateId);
    }

    /**
     * Get total count
     */
    @Operation(summary = "获取证书CA历史总数")
    @GetMapping("/count")
    public Result<Integer> count() {
        int count = certificateCAHistoryService.count();
        return Result.success(count);
    }

    /**
     * Get count by certificate ID
     */
    @Operation(summary = "根据证书ID获取CA历史数量")
    @GetMapping("/count/{certificateId}")
    public Result<Integer> countByCertificateId(@PathVariable Long certificateId) {
        int count = certificateCAHistoryService.countByCertificateId(certificateId);
        return Result.success(count);
    }
}
