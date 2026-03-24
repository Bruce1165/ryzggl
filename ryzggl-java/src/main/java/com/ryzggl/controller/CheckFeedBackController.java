package com.ryzggl.controller;

import com.ryzggl.common.Result;
import com.ryzggl.entity.CheckFeedBack;
import com.ryzggl.service.CheckFeedBackService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

/**
 * CheckFeedBack Controller
 * 核查反馈管理API
 */
@Tag(name = "核查反馈管理", description = "核查反馈管理相关接口")
@RestController
@RequestMapping("/api/v1/check-feedback")
public class CheckFeedBackController {

    @Autowired
    private CheckFeedBackService checkFeedBackService;

    /**
     * Get all feedback records
     */
    @Operation(summary = "获取所有核查反馈记录")
    @GetMapping("/all")
    public Result<List<CheckFeedBack>> getAll() {
        List<CheckFeedBack> list = checkFeedBackService.getAll();
        return Result.success(list);
    }

    /**
     * Get feedback by ID
     */
    @Operation(summary = "根据ID获取核查反馈")
    @GetMapping("/{dataId}")
    public Result<CheckFeedBack> getById(@PathVariable String dataId) {
        CheckFeedBack feedback = checkFeedBackService.getById(dataId);
        if (feedback == null) {
            return Result.error("核查反馈记录不存在");
        }
        return Result.success(feedback);
    }

    /**
     * Get feedback by patch code
     */
    @Operation(summary = "根据批次号获取核查反馈")
    @GetMapping("/patch/{patchCode}")
    public Result<List<CheckFeedBack>> getByPatchCode(@PathVariable Integer patchCode) {
        List<CheckFeedBack> list = checkFeedBackService.getByPatchCode(patchCode);
        return Result.success(list);
    }

    /**
     * Get feedback by check type
     */
    @Operation(summary = "根据监管类型获取核查反馈")
    @GetMapping("/type/{checkType}")
    public Result<List<CheckFeedBack>> getByCheckType(@PathVariable String checkType) {
        List<CheckFeedBack> list = checkFeedBackService.getByCheckType(checkType);
        return Result.success(list);
    }

    /**
     * Get feedback by status code
     */
    @Operation(summary = "根据状态码获取核查反馈")
    @GetMapping("/status/{dataStatusCode}")
    public Result<List<CheckFeedBack>> getByStatusCode(@PathVariable Integer dataStatusCode) {
        List<CheckFeedBack> list = checkFeedBackService.getByStatusCode(dataStatusCode);
        return Result.success(list);
    }

    /**
     * Get feedback by worker name
     */
    @Operation(summary = "根据人员姓名获取核查反馈")
    @GetMapping("/worker/{workerName}")
    public Result<List<CheckFeedBack>> getByWorkerName(@PathVariable String workerName) {
        List<CheckFeedBack> list = checkFeedBackService.getByWorkerName(workerName);
        return Result.success(list);
    }

    /**
     * Get feedback by certificate code
     */
    @Operation(summary = "根据证书编号获取核查反馈")
    @GetMapping("/certificate/{certificateCode}")
    public Result<List<CheckFeedBack>> getByCertificateCode(@PathVariable String certificateCode) {
        List<CheckFeedBack> list = checkFeedBackService.getByCertificateCode(certificateCode);
        return Result.success(list);
    }

    /**
     * Get feedback by unit code
     */
    @Operation(summary = "根据单位编码获取核查反馈")
    @GetMapping("/unit/{unitCode}")
    public Result<List<CheckFeedBack>> getByUnitCode(@PathVariable String unitCode) {
        List<CheckFeedBack> list = checkFeedBackService.getByUnitCode(unitCode);
        return Result.success(list);
    }

    /**
     * Get feedback by country
     */
    @Operation(summary = "根据所属区获取核查反馈")
    @GetMapping("/country/{country}")
    public Result<List<CheckFeedBack>> getByCountry(@PathVariable String country) {
        List<CheckFeedBack> list = checkFeedBackService.getByCountry(country);
        return Result.success(list);
    }

    /**
     * Search feedback
     */
    @Operation(summary = "搜索核查反馈")
    @GetMapping("/search")
    public Result<List<CheckFeedBack>> search(@RequestParam String keyword) {
        List<CheckFeedBack> list = checkFeedBackService.search(keyword);
        return Result.success(list);
    }

    /**
     * Get pending feedback records
     */
    @Operation(summary = "获取待反馈的记录")
    @GetMapping("/pending-feedback")
    public Result<List<CheckFeedBack>> getPendingFeedback() {
        List<CheckFeedBack> list = checkFeedBackService.getPendingFeedback();
        return Result.success(list);
    }

    /**
     * Get feedback requiring initial review
     */
    @Operation(summary = "获取待审查的记录")
    @GetMapping("/pending-review")
    public Result<List<CheckFeedBack>> getPendingReview() {
        List<CheckFeedBack> list = checkFeedBackService.getPendingReview();
        return Result.success(list);
    }

    /**
     * Get feedback requiring city review
     */
    @Operation(summary = "获取待复审的记录")
    @GetMapping("/pending-city-review")
    public Result<List<CheckFeedBack>> getPendingCityReview() {
        List<CheckFeedBack> list = checkFeedBackService.getPendingCityReview();
        return Result.success(list);
    }

    /**
     * Get feedback requiring decision
     */
    @Operation(summary = "获取待决定的记录")
    @GetMapping("/pending-decision")
    public Result<List<CheckFeedBack>> getPendingDecision() {
        List<CheckFeedBack> list = checkFeedBackService.getPendingDecision();
        return Result.success(list);
    }

    /**
     * Get status description
     */
    @Operation(summary = "获取状态描述")
    @GetMapping("/status-desc/{statusCode}")
    public Result<String> getStatusDescription(@PathVariable Integer statusCode) {
        String desc = checkFeedBackService.getStatusDescription(statusCode);
        return Result.success(desc);
    }

    /**
     * Create feedback
     */
    @Operation(summary = "创建核查反馈")
    @PostMapping
    public Result<CheckFeedBack> create(@RequestBody CheckFeedBack checkFeedBack) {
        return checkFeedBackService.createCheckFeedBack(checkFeedBack);
    }

    /**
     * Update feedback
     */
    @Operation(summary = "更新核查反馈")
    @PutMapping("/{dataId}")
    public Result<CheckFeedBack> update(@PathVariable String dataId, @RequestBody CheckFeedBack checkFeedBack) {
        checkFeedBack.setDataId(dataId);
        return checkFeedBackService.updateCheckFeedBack(checkFeedBack);
    }

    /**
     * Delete feedback
     */
    @Operation(summary = "删除核查反馈")
    @DeleteMapping("/{dataId}")
    public Result<Void> delete(@PathVariable String dataId) {
        return checkFeedBackService.deleteCheckFeedBack(dataId);
    }

    /**
     * Get total count
     */
    @Operation(summary = "获取核查反馈总数")
    @GetMapping("/count")
    public Result<Integer> count() {
        int count = checkFeedBackService.count();
        return Result.success(count);
    }

    /**
     * Get count by status code
     */
    @Operation(summary = "根据状态码获取数量")
    @GetMapping("/count/{dataStatusCode}")
    public Result<Integer> countByStatusCode(@PathVariable Integer dataStatusCode) {
        int count = checkFeedBackService.getByStatusCode(dataStatusCode).size();
        return Result.success(count);
    }
}
