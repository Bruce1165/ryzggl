package com.ryzggl.controller;

import com.ryzggl.common.Result;
import com.ryzggl.entity.BlackList;
import com.ryzggl.service.BlackListService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

/**
 * BlackList Controller
 * 黑名单管理API
 */
@Tag(name = "黑名单管理", description = "黑名单管理相关接口")
@RestController
@RequestMapping("/api/v1/blacklist")
public class BlackListController {

    @Autowired
    private BlackListService blackListService;

    /**
     * Get all blacklist entries
     */
    @Operation(summary = "获取所有黑名单记录")
    @GetMapping("/all")
    public Result<List<BlackList>> getAll() {
        List<BlackList> list = blackListService.getAll();
        return Result.success(list);
    }

    /**
     * Get blacklist by ID
     */
    @Operation(summary = "根据ID获取黑名单记录")
    @GetMapping("/{blackListId}")
    public Result<BlackList> getById(@PathVariable Long blackListId) {
        BlackList blackList = blackListService.getById(blackListId);
        if (blackList == null) {
            return Result.error("黑名单记录不存在");
        }
        return Result.success(blackList);
    }

    /**
     * Get blacklist by worker name
     */
    @Operation(summary = "根据人员姓名获取黑名单")
    @GetMapping("/worker/{workerName}")
    public Result<List<BlackList>> getByWorkerName(@PathVariable String workerName) {
        List<BlackList> list = blackListService.getByWorkerName(workerName);
        return Result.success(list);
    }

    /**
     * Get blacklist by certificate code
     */
    @Operation(summary = "根据证书编号获取黑名单")
    @GetMapping("/certificate/{certificateCode}")
    public Result<List<BlackList>> getByCertificateCode(@PathVariable String certificateCode) {
        List<BlackList> list = blackListService.getByCertificateCode(certificateCode);
        return Result.success(list);
    }

    /**
     * Get blacklist by unit code
     */
    @Operation(summary = "根据单位编码获取黑名单")
    @GetMapping("/unit/{unitCode}")
    public Result<List<BlackList>> getByUnitCode(@PathVariable String unitCode) {
        List<BlackList> list = blackListService.getByUnitCode(unitCode);
        return Result.success(list);
    }

    /**
     * Get blacklist by type
     */
    @Operation(summary = "根据类型获取黑名单")
    @GetMapping("/type/{blackType}")
    public Result<List<BlackList>> getByType(@PathVariable String blackType) {
        List<BlackList> list = blackListService.getByBlackType(blackType);
        return Result.success(list);
    }

    /**
     * Get blacklist by status
     */
    @Operation(summary = "根据状态获取黑名单")
    @GetMapping("/status/{blackStatus}")
    public Result<List<BlackList>> getByStatus(@PathVariable String blackStatus) {
        List<BlackList> list = blackListService.getByBlackStatus(blackStatus);
        return Result.success(list);
    }

    /**
     * Search blacklist
     */
    @Operation(summary = "搜索黑名单")
    @GetMapping("/search")
    public Result<List<BlackList>> search(@RequestParam String keyword) {
        List<BlackList> list = blackListService.search(keyword);
        return Result.success(list);
    }

    /**
     * Check if certificate is blacklisted
     */
    @Operation(summary = "检查证书是否在黑名单中")
    @GetMapping("/check/{certificateCode}")
    public Result<Boolean> checkCertificate(@PathVariable String certificateCode) {
        boolean blacklisted = blackListService.isCertificateBlacklisted(certificateCode);
        return Result.success(blacklisted);
    }

    /**
     * Create blacklist entry
     */
    @Operation(summary = "创建黑名单记录")
    @PostMapping
    public Result<BlackList> create(@RequestBody BlackList blackList) {
        return blackListService.createBlackList(blackList);
    }

    /**
     * Update blacklist entry
     */
    @Operation(summary = "更新黑名单记录")
    @PutMapping("/{blackListId}")
    public Result<BlackList> update(@PathVariable Long blackListId, @RequestBody BlackList blackList) {
        blackList.setBlackListId(blackListId);
        return blackListService.updateBlackList(blackList);
    }

    /**
     * Delete blacklist entry
     */
    @Operation(summary = "删除黑名单记录")
    @DeleteMapping("/{blackListId}")
    public Result<Void> delete(@PathVariable Long blackListId) {
        return blackListService.deleteBlackList(blackListId);
    }

    /**
     * Get total count
     */
    @Operation(summary = "获取黑名单总数")
    @GetMapping("/count")
    public Result<Integer> count() {
        int count = blackListService.count();
        return Result.success(count);
    }
}
