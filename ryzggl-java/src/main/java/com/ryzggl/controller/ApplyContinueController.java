package com.ryzggl.controller;

import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.common.Result;
import com.ryzggl.entity.ApplyContinue;
import com.ryzggl.service.ApplyContinueService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

/**
 * ApplyContinue Controller - 延续申请REST API
 */
@RestController
@RequestMapping("/api/v1/apply-continue")
public class ApplyContinueController {

    @Autowired
    private ApplyContinueService applyContinueService;

    /**
     * Create new apply continue
     */
    @PostMapping
    public Result<ApplyContinue> create(@RequestBody ApplyContinue applyContinue) {
        return applyContinueService.createApplyContinue(applyContinue);
    }

    /**
     * Update apply continue
     */
    @PutMapping("/{id}")
    public Result<ApplyContinue> update(@PathVariable Long id, @RequestBody ApplyContinue applyContinue) {
        return applyContinueService.updateApplyContinue(applyContinue);
    }

    /**
     * Delete apply continue (soft delete)
     */
    @DeleteMapping("/{id}")
    public Result<Boolean> delete(@PathVariable Long id) {
        return applyContinueService.deleteApplyContinue(id);
    }

    /**
     * Get apply continue by ID
     */
    @GetMapping("/{id}")
    public Result<ApplyContinue> getById(@PathVariable Long id) {
        return applyContinueService.getApplyContinueById(id);
    }

    /**
     * Get apply continue list with pagination
     */
    @GetMapping("/list")
    public Result<IPage<ApplyContinue>> list(
            @RequestParam(defaultValue = "1") Integer current,
            @RequestParam(defaultValue = "10") Integer size,
            @RequestParam(required = false) String applyNo,
            @RequestParam(required = false) String applicantName,
            @RequestParam(required = false) String certificateCode,
            @RequestParam(required = false) String applyStatus) {
        return applyContinueService.getApplyContinuePage(current, size, applyNo, applicantName, certificateCode, applyStatus);
    }

    /**
     * Get apply continues by worker ID
     */
    @GetMapping("/worker/{workerId}")
    public Result getApplyContinuesByWorkerId(@PathVariable Long workerId) {
        return applyContinueService.getApplyContinuesByWorkerId(workerId);
    }

    /**
     * Get apply continues by certificate code
     */
    @GetMapping("/certificate/{certificateCode}")
    public Result getApplyContinuesByCertificateCode(@PathVariable String certificateCode) {
        return applyContinueService.getApplyContinuesByCertificateCode(certificateCode);
    }

    /**
     * Get apply continues by status
     */
    @GetMapping("/status/{status}")
    public Result getApplyContinuesByStatus(@PathVariable String status) {
        return applyContinueService.getApplyContinuesByStatus(status);
    }

    /**
     * Submit apply continue for review
     */
    @PostMapping("/{id}/submit")
    public Result<Boolean> submitForReview(@PathVariable Long id,
                                          @RequestParam String checkMan,
                                          @RequestParam String checkAdvise) {
        return applyContinueService.submitForReview(id, checkMan, checkAdvise);
    }

    /**
     * Approve apply continue
     */
    @PostMapping("/{id}/approve")
    public Result<Boolean> approve(@PathVariable Long id,
                                      @RequestParam String checkMan,
                                      @RequestParam String checkAdvise) {
        return applyContinueService.approve(id, checkMan, checkAdvise);
    }

    /**
     * Reject apply continue
     */
    @PostMapping("/{id}/reject")
    public Result<Boolean> reject(@PathVariable Long id,
                                     @RequestParam String checkMan,
                                     @RequestParam String checkAdvise) {
        return applyContinueService.reject(id, checkMan, checkAdvise);
    }
}
