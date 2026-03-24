package com.ryzggl.controller;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.entity.Organization;
import com.ryzggl.service.OrganizationService;
import com.ryzggl.common.Result;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

/**
 * Organization Controller
 * 机构管理接口
 */
@RestController
@RequestMapping("/api/v1/organizations")
@Tag(name = "机构管理", description = "组织机构管理相关接口")
public class OrganizationController {

    @Autowired
    private OrganizationService organizationService;

    /**
     * Get organization by ID
     */
    @Operation(summary = "获取机构详情", description = "根据ID获取机构信息")
    @GetMapping("/{id}")
    public Result<Organization> getById(@PathVariable Long id) {
        Organization org = organizationService.getOrganizationById(id);
        if (org == null) {
            return Result.error("机构不存在");
        }
        return Result.success(org);
    }

    /**
     * Get organization by code
     */
    @Operation(summary = "根据编码获取机构", description = "根据机构编码获取机构信息")
    @GetMapping("/code/{code}")
    public Result<Organization> getByCode(@PathVariable String code) {
        Organization org = organizationService.getOrganizationByCode(code);
        if (org == null) {
            return Result.error("机构不存在");
        }
        return Result.success(org);
    }

    /**
     * Get organization tree
     */
    @Operation(summary = "获取机构树", description = "获取完整的机构树结构")
    @GetMapping("/tree")
    public Result<List<Organization>> getTree() {
        List<Organization> tree = organizationService.getOrganizationTree();
        return Result.success(tree);
    }

    /**
     * Get all visible organizations (paginated)
     */
    @Operation(summary = "获取机构列表", description = "获取所有可见的机构列表（分页）")
    @GetMapping("/list")
    public Result<Page<Organization>> list(
            @RequestParam(defaultValue = "1") Integer current,
            @RequestParam(defaultValue = "10") Integer size) {
        Page<Organization> page = new Page<>(current, size);
        QueryWrapper<Organization> wrapper = new QueryWrapper<>();
        wrapper.eq("isVisible", true);
        wrapper.orderByAsc("orderId");
        page = organizationService.page(page, wrapper);
        return Result.success(page);
    }

    /**
     * Get organizations by type
     */
    @Operation(summary = "按类型获取机构", description = "根据机构类型获取机构列表")
    @GetMapping("/type/{type}")
    public Result<List<Organization>> getByType(@PathVariable String type) {
        List<Organization> list = organizationService.getOrganizationsByType(type);
        return Result.success(list);
    }

    /**
     * Get organizations by region
     */
    @Operation(summary = "按地区获取机构", description = "根据地区ID获取机构列表")
    @GetMapping("/region/{regionId}")
    public Result<List<Organization>> getByRegion(@PathVariable String regionId) {
        List<Organization> list = organizationService.getOrganizationsByRegion(regionId);
        return Result.success(list);
    }

    /**
     * Search organizations
     */
    @Operation(summary = "搜索机构", description = "根据关键词搜索机构")
    @GetMapping("/search")
    public Result<List<Organization>> search(
            @RequestParam(required = false) String keyword) {
        List<Organization> list = organizationService.searchOrganizations(keyword);
        return Result.success(list);
    }

    /**
     * Create organization
     */
    @Operation(summary = "创建机构", description = "创建新的机构")
    @PostMapping
    public Result<Organization> create(@RequestBody Organization organization) {
        return organizationService.createOrganization(organization);
    }

    /**
     * Update organization
     */
    @Operation(summary = "更新机构", description = "更新机构信息")
    @PutMapping("/{id}")
    public Result<Organization> update(@PathVariable Long id, @RequestBody Organization organization) {
        organization.setOrganId(id);
        return organizationService.updateOrganization(organization);
    }

    /**
     * Delete organization
     */
    @Operation(summary = "删除机构", description = "根据ID删除机构")
    @DeleteMapping("/{id}")
    public Result<Void> delete(@PathVariable Long id) {
        return organizationService.deleteOrganization(id);
    }

    /**
     * Update visibility
     */
    @Operation(summary = "更新可见性", description = "更新机构的可见状态")
    @PutMapping("/{id}/visibility")
    public Result<Void> updateVisibility(@PathVariable Long id, @RequestParam Boolean isVisible) {
        return organizationService.updateVisibility(id, isVisible);
    }

    /**
     * Get all organizations (no pagination - for tree view)
     */
    @Operation(summary = "获取所有机构", description = "获取所有可见机构（不分页，用于树形展示）")
    @GetMapping("/all")
    public Result<List<Organization>> getAll() {
        List<Organization> list = organizationService.getAllVisible();
        return Result.success(list);
    }
}
