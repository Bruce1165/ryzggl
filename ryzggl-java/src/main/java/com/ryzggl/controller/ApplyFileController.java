package com.ryzggl.controller;

import com.ryzggl.common.Result;
import com.ryzggl.entity.ApplyFile;
import com.ryzggl.service.ApplyFileService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

/**
 * ApplyFile Controller
 * 申请文件管理API
 */
@Tag(name = "申请文件管理", description = "申请文件管理相关接口")
@RestController
@RequestMapping("/api/v1/apply-files")
public class ApplyFileController {

    @Autowired
    private ApplyFileService applyFileService;

    /**
     * Get all apply files
     */
    @Operation(summary = "获取所有申请文件")
    @GetMapping("/all")
    public Result<List<ApplyFile>> getAll() {
        List<ApplyFile> list = applyFileService.getAll();
        return Result.success(list);
    }

    /**
     * Get apply file by ID
     */
    @Operation(summary = "根据ID获取申请文件")
    @GetMapping("/{fileId}")
    public Result<ApplyFile> getById(@PathVariable String fileId) {
        ApplyFile applyFile = applyFileService.getById(fileId);
        if (applyFile == null) {
            return Result.error("申请文件不存在");
        }
        return Result.success(applyFile);
    }

    /**
     * Get apply files by apply ID
     */
    @Operation(summary = "根据申请ID获取申请文件")
    @GetMapping("/apply/{applyId}")
    public Result<List<ApplyFile>> getByApplyId(@PathVariable String applyId) {
        List<ApplyFile> list = applyFileService.getByApplyId(applyId);
        return Result.success(list);
    }

    /**
     * Get apply files by check result
     */
    @Operation(summary = "根据审核结果获取申请文件")
    @GetMapping("/check-result/{checkResult}")
    public Result<List<ApplyFile>> getByCheckResult(@PathVariable Integer checkResult) {
        List<ApplyFile> list = applyFileService.getByCheckResult(checkResult);
        return Result.success(list);
    }

    /**
     * Search apply files
     */
    @Operation(summary = "搜索申请文件")
    @GetMapping("/search")
    public Result<List<ApplyFile>> search(@RequestParam String keyword) {
        List<ApplyFile> list = applyFileService.search(keyword);
        return Result.success(list);
    }

    /**
     * Create apply file
     */
    @Operation(summary = "创建申请文件")
    @PostMapping
    public Result<ApplyFile> create(@RequestBody ApplyFile applyFile) {
        return applyFileService.createApplyFile(applyFile);
    }

    /**
     * Update apply file
     */
    @Operation(summary = "更新申请文件")
    @PutMapping("/{fileId}")
    public Result<ApplyFile> update(@PathVariable String fileId, @RequestBody ApplyFile applyFile) {
        applyFile.setFileId(fileId);
        return applyFileService.updateApplyFile(applyFile);
    }

    /**
     * Delete apply file
     */
    @Operation(summary = "删除申请文件")
    @DeleteMapping("/{fileId}")
    public Result<Void> delete(@PathVariable String fileId, @RequestParam String applyId) {
        return applyFileService.deleteApplyFile(fileId, applyId);
    }

    /**
     * Delete by apply ID
     */
    @Operation(summary = "根据申请ID删除申请文件")
    @DeleteMapping("/apply/{applyId}")
    public Result<Void> deleteByApplyId(@PathVariable String applyId) {
        return applyFileService.deleteByApplyId(applyId);
    }

    /**
     * Get total count
     */
    @Operation(summary = "获取申请文件总数")
    @GetMapping("/count")
    public Result<Integer> count() {
        int count = applyFileService.count();
        return Result.success(count);
    }

    /**
     * Get count by apply ID
     */
    @Operation(summary = "根据申请ID获取文件数量")
    @GetMapping("/count/{applyId}")
    public Result<Integer> countByApplyId(@PathVariable String applyId) {
        int count = applyFileService.countByApplyId(applyId);
        return Result.success(count);
    }

    /**
     * Get count by check result
     */
    @Operation(summary = "根据审核结果获取文件数量")
    @GetMapping("/count/check-result/{checkResult}")
    public Result<Integer> countByCheckResult(@PathVariable Integer checkResult) {
        int count = applyFileService.countByCheckResult(checkResult);
        return Result.success(count);
    }

    /**
     * Get check result description
     */
    @Operation(summary = "获取审核结果描述")
    @GetMapping("/check-result-desc/{checkResult}")
    public Result<String> getCheckResultDescription(@PathVariable Integer checkResult) {
        String desc = applyFileService.getCheckResultDescription(checkResult);
        return Result.success(desc);
    }
}
