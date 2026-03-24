package com.ryzggl.controller;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.common.Result;
import com.ryzggl.entity.StudyPlan;
import com.ryzggl.service.StudyPlanService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

/**
 * StudyPlan Controller
 * 学习计划REST API
 *
 * Maps to legacy StudyPlanDAL.cs
 * Base path: /api/v1/study-plans
 */
@Tag(name = "StudyPlan Management", description = "学习计划管理API")
@RestController
@RequestMapping("/api/v1/study-plans")
public class StudyPlanController {

    @Autowired
    private StudyPlanService studyPlanService;

    /**
     * Get study plan by worker certificate code and package ID
     */
    @Operation(summary = "Get study plan by worker certificate code and package ID")
    @GetMapping("/{workerCertificateCode}/{packageId}")
    public Result<StudyPlan> getByWorkerCertificateAndPackage(
            @PathVariable String workerCertificateCode,
            @PathVariable Long packageId) {
        StudyPlan studyPlan = studyPlanService.getByWorkerCertificateAndPackage(workerCertificateCode, packageId);
        if (studyPlan == null) {
            return Result.error("学习计划不存在");
        }
        return Result.success(studyPlan);
    }

    /**
     * Get study plans by worker certificate code
     */
    @Operation(summary = "Get study plans by worker certificate code")
    @GetMapping("/worker/{workerCertificateCode}")
    public Result<List<StudyPlan>> getByWorkerCertificateCode(@PathVariable String workerCertificateCode) {
        List<StudyPlan> plans = studyPlanService.getByWorkerCertificateCode(workerCertificateCode);
        return Result.success(plans);
    }

    /**
     * Get study plans by package ID
     */
    @Operation(summary = "Get study plans by package ID")
    @GetMapping("/package/{packageId}")
    public Result<List<StudyPlan>> getByPackageId(@PathVariable Long packageId) {
        List<StudyPlan> plans = studyPlanService.getByPackageId(packageId);
        return Result.success(plans);
    }

    /**
     * Search study plans
     */
    @Operation(summary = "Search study plans")
    @GetMapping("/search")
    public Result<List<StudyPlan>> search(@RequestParam String keyword) {
        List<StudyPlan> plans = studyPlanService.search(keyword);
        return Result.success(plans);
    }

    /**
     * Get all study plans with pagination
     */
    @Operation(summary = "Get all study plans with pagination")
    @GetMapping
    public Result<Page<StudyPlan>> list(
            @RequestParam(defaultValue = "1") int current,
            @RequestParam(defaultValue = "10") int size,
            @RequestParam(required = false) String keyword
    ) {
        Page<StudyPlan> page = new Page<>(current, size);
        QueryWrapper<StudyPlan> queryWrapper = new QueryWrapper<>();

        if (keyword != null && !keyword.isEmpty()) {
            queryWrapper.and(wrapper -> wrapper
                    .like("WorkerName", keyword)
                    .or()
                    .like("Creater", keyword)
                    .or()
                    .like("AddType", keyword)
            );
        }
        queryWrapper.orderByDesc("CreateTime");

        Page<StudyPlan> result = studyPlanService.page(page, queryWrapper);
        return Result.success(result);
    }

    /**
     * Create study plan
     */
    @Operation(summary = "Create study plan")
    @PostMapping("/create")
    public Result<StudyPlan> createStudyPlan(@RequestBody StudyPlan studyPlan) {
        return studyPlanService.createStudyPlan(studyPlan);
    }

    /**
     * Update study plan
     */
    @Operation(summary = "Update study plan")
    @PutMapping("/{workerCertificateCode}/{packageId}")
    public Result<StudyPlan> updateStudyPlan(
            @PathVariable String workerCertificateCode,
            @PathVariable Long packageId,
            @RequestBody StudyPlan studyPlan) {
        return studyPlanService.updateStudyPlan(studyPlan);
    }

    /**
     * Update study plan status
     */
    @Operation(summary = "Update study plan status")
    @PutMapping("/status")
    public Result<StudyPlan> updateStudyPlanStatus(
            @RequestParam String workerCertificateCode,
            @RequestParam Long packageId,
            @RequestParam String testStatus) {
        return studyPlanService.updateStudyPlanStatus(workerCertificateCode, packageId, testStatus);
    }

    /**
     * Delete study plan
     */
    @Operation(summary = "Delete study plan")
    @DeleteMapping("/{workerCertificateCode}/{packageId}")
    public Result<Void> deleteStudyPlan(
            @PathVariable String workerCertificateCode,
            @PathVariable Long packageId) {
        return studyPlanService.deleteStudyPlan(workerCertificateCode, packageId);
    }
}
