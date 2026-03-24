package com.ryzggl.controller;

import com.ryzggl.entity.Qualification;
import com.ryzggl.service.QualificationService;
import com.ryzggl.common.Result;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

/**
 * Qualification Controller
 * 资格证书管理接口
 */
@RestController
@RequestMapping("/api/v1/qualifications")
@Tag(name = "资格管理", description = "资格证书管理相关接口")
public class QualificationController {

    @Autowired
    private QualificationService qualificationService;

    /**
     * Get qualification by ID
     */
    @Operation(summary = "获取资格详情", description = "根据ID获取资格证书信息")
    @GetMapping("/{id}")
    public Result<Qualification> getById(@PathVariable Long id) {
        Qualification qual = qualificationService.getQualificationById(id);
        if (qual == null) {
            return Result.error("资格不存在");
        }
        return Result.success(qual);
    }

    /**
     * Get qualification by code
     */
    @Operation(summary = "根据编码获取资格", description = "根据资格编码获取证书信息")
    @GetMapping("/code/{code}")
    public Result<Qualification> getByCode(@PathVariable String code) {
        Qualification qual = qualificationService.getQualificationByCode(code);
        if (qual == null) {
            return Result.error("资格不存在");
        }
        return Result.success(qual);
    }

    /**
     * Get all valid qualifications
     */
    @Operation(summary = "获取所有有效资格", description = "获取所有有效的资格证书列表")
    @GetMapping("/all")
    public Result<List<Qualification>> getAll() {
        List<Qualification> list = qualificationService.getAllValid();
        return Result.success(list);
    }

    /**
     * Get qualifications by type
     */
    @Operation(summary = "按类型获取资格", description = "根据资格类型获取证书列表")
    @GetMapping("/type/{type}")
    public Result<List<Qualification>> getByType(@PathVariable String type) {
        List<Qualification> list = qualificationService.getQualificationsByType(type);
        return Result.success(list);
    }

    /**
     * Get qualifications by level
     */
    @Operation(summary = "按等级获取资格", description = "根据资格等级获取证书列表")
    @GetMapping("/level/{level}")
    public Result<List<Qualification>> getByLevel(@PathVariable String level) {
        List<Qualification> list = qualificationService.getQualificationsByLevel(level);
        return Result.success(list);
    }

    /**
     * Get qualifications by category
     */
    @Operation(summary = "按类别获取资格", description = "根据资格类别获取证书列表")
    @GetMapping("/category/{category}")
    public Result<List<Qualification>> getByCategory(@PathVariable String category) {
        List<Qualification> list = qualificationService.getQualificationsByCategory(category);
        return Result.success(list);
    }

    /**
     * Get qualifications by province
     */
    @Operation(summary = "按省份获取资格", description = "根据省份编码获取证书列表")
    @GetMapping("/province/{provinceCode}")
    public Result<List<Qualification>> getByProvince(@PathVariable String provinceCode) {
        List<Qualification> list = qualificationService.getQualificationsByProvince(provinceCode);
        return Result.success(list);
    }

    /**
     * Search qualifications
     */
    @Operation(summary = "搜索资格", description = "根据关键词搜索资格证书")
    @GetMapping("/search")
    public Result<List<Qualification>> search(
            @RequestParam(required = false) String keyword) {
        List<Qualification> list = qualificationService.searchQualifications(keyword);
        return Result.success(list);
    }

    /**
     * Create qualification
     */
    @Operation(summary = "创建资格", description = "创建新的资格证书")
    @PostMapping
    public Result<Qualification> create(@RequestBody Qualification qualification) {
        return qualificationService.createQualification(qualification);
    }

    /**
     * Update qualification
     */
    @Operation(summary = "更新资格", description = "更新资格证书信息")
    @PutMapping("/{id}")
    public Result<Qualification> update(@PathVariable Long id, @RequestBody Qualification qualification) {
        qualification.setQualId(id);
        return qualificationService.updateQualification(qualification);
    }

    /**
     * Delete qualification
     */
    @Operation(summary = "删除资格", description = "根据ID删除资格证书")
    @DeleteMapping("/{id}")
    public Result<Void> delete(@PathVariable Long id) {
        return qualificationService.deleteQualification(id);
    }

    /**
     * Update validity
     */
    @Operation(summary = "更新有效性", description = "更新资格证书的有效状态")
    @PutMapping("/{id}/validity")
    public Result<Void> updateValidity(@PathVariable Long id, @RequestParam Boolean isValid) {
        return qualificationService.updateValidity(id, isValid);
    }

    /**
     * Batch update validity
     */
    @Operation(summary = "批量更新有效性", description = "批量更新资格证书的有效状态")
    @PutMapping("/validity/batch")
    public Result<Void> batchUpdateValidity(@RequestBody List<Long> ids, @RequestParam Boolean isValid) {
        return qualificationService.batchUpdateValidity(ids, isValid);
    }
}
