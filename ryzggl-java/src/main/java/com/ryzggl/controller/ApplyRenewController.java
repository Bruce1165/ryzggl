package com.ryzggl.controller;

import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.common.Result;
import com.ryzggl.entity.ApplyRenew;
import com.ryzggl.service.ApplyRenewService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

/**
 * ApplyRenew Controller - 续期申请REST API
 */
@RestController
@RequestMapping("/api/v1/apply/renew")
public class ApplyRenewController {

    @Autowired
    private ApplyRenewService applyRenewService;

    /**
     * Create new apply renew
     */
    @PostMapping
    public Result<ApplyRenew> create(@RequestBody ApplyRenew applyRenew) {
        return applyRenewService.createApplyRenew(applyRenew);
    }

    /**
     * Update apply renew
     */
    @PutMapping("/{id}")
    public Result<ApplyRenew> update(@PathVariable Long id, @RequestBody ApplyRenew applyRenew) {
        return applyRenewService.updateApplyRenew(applyRenew);
    }

    /**
     * Delete apply renew (soft delete)
     */
    @DeleteMapping("/{id}")
    public Result<Boolean> delete(@PathVariable Long id) {
        return applyRenewService.deleteApplyRenew(id);
    }

    /**
     * Get apply renew by ID
     */
    @GetMapping("/{id}")
    public Result<ApplyRenew> getById(@PathVariable Long id) {
        return applyRenewService.getApplyRenewById(id);
    }

    /**
     * Get apply renew list with pagination
     */
    @GetMapping("/list")
    public Result<IPage<ApplyRenew>> list(
            @RequestParam(defaultValue = "1") Integer current,
            @RequestParam(defaultValue = "10") Integer size,
            @RequestParam(required = false) String applyNo,
            @RequestParam(required = false) String applicantName,
            @RequestParam(required = false) String certificateCode,
            @RequestParam(required = false) String applyStatus) {
        return applyRenewService.getApplyRenewPage(current, size, applyNo, applicantName, certificateCode, applyStatus);
    }

    /**
     * Get apply renews by worker ID
     */
    @GetMapping("/worker/{workerId}")
    public Result getApplyRenewByWorkerId(@PathVariable Long workerId) {
        return applyRenewService.getApplyRenewByWorkerId(workerId);
    }

    /**
     * Get apply renews by certificate code
     */
    @GetMapping("/certificate/{certificateCode}")
    public Result getApplyRenewByCertificateCode(@PathVariable String certificateCode) {
        return applyRenewService.getApplyRenewByCertificateCode(certificateCode);
    }

    /**
     * Get apply renews by status
     */
    @GetMapping("/status/{status}")
    public Result getApplyRenewByStatus(@PathVariable String status) {
        return applyRenewService.getApplyRenewByStatus(status);
    }

    /**
     * Submit apply renew for review
     */
    @PostMapping("/{id}/submit")
    public Result<Boolean> submitForReview(@PathVariable Long id,
                                        @RequestParam String checkMan,
                                        @RequestParam String checkAdvise) {
        return applyRenewService.submitForReview(id, checkMan, checkAdvise);
    }

    /**
     * Approve apply renew
     */
    @PostMapping("/{id}/approve")
    public Result<Boolean> approve(@PathVariable Long id,
                                     @RequestParam String checkMan,
                                     @RequestParam String checkAdvise) {
        return applyRenewService.approve(id, checkMan, checkAdvise);
    }

    /**
     * Reject apply renew
     */
    @PostMapping("/{id}/reject")
    public Result<Boolean> reject(@PathVariable Long id,
                                     @RequestParam String checkMan,
                                     @RequestParam String checkAdvise) {
        return applyRenewService.reject(id, checkMan, checkAdvise);
    }
}
