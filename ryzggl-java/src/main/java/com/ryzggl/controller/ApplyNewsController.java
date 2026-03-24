package com.ryzggl.controller;

import com.ryzggl.common.Result;
import com.ryzggl.entity.ApplyNews;
import com.ryzggl.service.ApplyNewsService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

/**
 * ApplyNews Controller
 * 申请新闻管理API
 */
@Tag(name = "申请新闻管理", description = "申请新闻管理相关接口")
@RestController
@RequestMapping("/api/v1/apply-news")
public class ApplyNewsController {

    @Autowired
    private ApplyNewsService applyNewsService;

    /**
     * Get all apply news
     */
    @Operation(summary = "获取所有申请新闻")
    @GetMapping("/all")
    public Result<List<ApplyNews>> getAll() {
        List<ApplyNews> list = applyNewsService.getAll();
        return Result.success(list);
    }

    /**
     * Get apply news by ID
     */
    @Operation(summary = "根据ID获取申请新闻")
    @GetMapping("/{id}")
    public Result<ApplyNews> getById(@PathVariable String id) {
        ApplyNews applyNews = applyNewsService.getById(id);
        if (applyNews == null) {
            return Result.error("申请新闻不存在");
        }
        return Result.success(applyNews);
    }

    /**
     * Get apply news by organization code
     */
    @Operation(summary = "根据组织机构代码获取申请新闻（未审核）")
    @GetMapping("/organization/{entOrganizationsCode}")
    public Result<List<ApplyNews>> getByOrganizationCode(@PathVariable String entOrganizationsCode) {
        List<ApplyNews> list = applyNewsService.getByOrganizationCode(entOrganizationsCode);
        return Result.success(list);
    }

    /**
     * Get apply news by person name
     */
    @Operation(summary = "根据人员姓名获取申请新闻")
    @GetMapping("/person/{psnName}")
    public Result<List<ApplyNews>> getByPsnName(@PathVariable String psnName) {
        List<ApplyNews> list = applyNewsService.getByPsnName(psnName);
        return Result.success(list);
    }

    /**
     * Get apply news by certificate no
     */
    @Operation(summary = "根据证件号码获取申请新闻")
    @GetMapping("/certificate/{psnCertificateNo}")
    public Result<List<ApplyNews>> getByPsnCertificateNo(@PathVariable String psnCertificateNo) {
        List<ApplyNews> list = applyNewsService.getByPsnCertificateNo(psnCertificateNo);
        return Result.success(list);
    }

    /**
     * Get apply news by register no
     */
    @Operation(summary = "根据注册编号获取申请新闻")
    @GetMapping("/register/{psnRegisterNo}")
    public Result<List<ApplyNews>> getByPsnRegisterNo(@PathVariable String psnRegisterNo) {
        List<ApplyNews> list = applyNewsService.getByPsnRegisterNo(psnRegisterNo);
        return Result.success(list);
    }

    /**
     * Get apply news by apply type
     */
    @Operation(summary = "根据申报类型获取申请新闻")
    @GetMapping("/type/{applyType}")
    public Result<List<ApplyNews>> getByApplyType(@PathVariable String applyType) {
        List<ApplyNews> list = applyNewsService.getByApplyType(applyType);
        return Result.success(list);
    }

    /**
     * Get apply news by SFCK status
     */
    @Operation(summary = "根据审核状态获取申请新闻")
    @GetMapping("/sfck/{sfck}")
    public Result<List<ApplyNews>> getBySfck(@PathVariable Boolean sfck) {
        List<ApplyNews> list = applyNewsService.getBySfck(sfck);
        return Result.success(list);
    }

    /**
     * Get unchecked apply news
     */
    @Operation(summary = "获取未审核的申请新闻")
    @GetMapping("/unchecked")
    public Result<List<ApplyNews>> getUnchecked() {
        List<ApplyNews> list = applyNewsService.getUnchecked();
        return Result.success(list);
    }

    /**
     * Get checked apply news
     */
    @Operation(summary = "获取已审核的申请新闻")
    @GetMapping("/checked")
    public Result<List<ApplyNews>> getChecked() {
        List<ApplyNews> list = applyNewsService.getChecked();
        return Result.success(list);
    }

    /**
     * Search apply news
     */
    @Operation(summary = "搜索申请新闻")
    @GetMapping("/search")
    public Result<List<ApplyNews>> search(@RequestParam String keyword) {
        List<ApplyNews> list = applyNewsService.search(keyword);
        return Result.success(list);
    }

    /**
     * Create apply news
     */
    @Operation(summary = "创建申请新闻")
    @PostMapping
    public Result<ApplyNews> create(@RequestBody ApplyNews applyNews) {
        return applyNewsService.createApplyNews(applyNews);
    }

    /**
     * Update apply news
     */
    @Operation(summary = "更新申请新闻")
    @PutMapping("/{id}")
    public Result<ApplyNews> update(@PathVariable String id, @RequestBody ApplyNews applyNews) {
        applyNews.setId(id);
        return applyNewsService.updateApplyNews(applyNews);
    }

    /**
     * Delete apply news
     */
    @Operation(summary = "删除申请新闻")
    @DeleteMapping("/{id}")
    public Result<Void> delete(@PathVariable String id) {
        return applyNewsService.deleteApplyNews(id);
    }

    /**
     * Get total count
     */
    @Operation(summary = "获取申请新闻总数")
    @GetMapping("/count")
    public Result<Integer> count() {
        int count = applyNewsService.count();
        return Result.success(count);
    }

    /**
     * Get count by SFCK status
     */
    @Operation(summary = "根据审核状态获取数量")
    @GetMapping("/count/{sfck}")
    public Result<Integer> countBySfck(@PathVariable Boolean sfck) {
        int count = applyNewsService.countBySfck(sfck);
        return Result.success(count);
    }
}
