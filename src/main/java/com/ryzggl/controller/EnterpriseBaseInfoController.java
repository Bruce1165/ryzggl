package com.ryzggl.controller;

import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.common.Result;
import com.ryzggl.entity.EnterpriseBaseInfo;
import com.ryzggl.service.EnterpriseBaseInfoService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.validation.annotation.Validated;
import org.springframework.web.bind.annotation.*;

import jakarta.validation.Valid;
import java.util.List;

/**
 * EnterpriseBaseInfo Controller - REST API Endpoints
 * Replaces .NET WebForms pages: zjsEntList.aspx, zjsEntChange.aspx
 */
@RestController
@RequestMapping("/api/enterprise")
@Tag(name = "Enterprise Information Management", description = "Enterprise base information CRUD operations")
public class EnterpriseBaseInfoController {

    private static final Logger log = LoggerFactory.getLogger(EnterpriseBaseInfoController.class);

    @Autowired
    private EnterpriseBaseInfoService enterpriseBaseInfoService;

    /**
     * Get enterprise list with pagination
     * Replaces: zjsEntList.aspx with Telerik RadGrid
     */
    @Operation(summary = "Get enterprise list", description = "List enterprises with pagination and filtering")
    @GetMapping("/list")
    public Result<Page<EnterpriseBaseInfo>> getEnterpriseList(
            @RequestParam(defaultValue = "1") Integer current,
            @RequestParam(required = false) String entType,
            @RequestParam(required = false) String entGrade,
            @RequestParam(required = false) String entStatus,
            @RequestParam(required = false) String entProvince,
            @RequestParam(required = false) String keyword) {
        log.info("Getting enterprises: page={}, entType={}, entGrade={}, entStatus={}, entProvince={}, keyword={}",
                current, entType, entGrade, entStatus, entProvince, keyword);

        com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<EnterpriseBaseInfo> queryWrapper =
                new com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<>();

        if (entType != null) {
            queryWrapper.eq(EnterpriseBaseInfo::getEntType, entType);
        }
        if (entGrade != null) {
            queryWrapper.eq(EnterpriseBaseInfo::getEntGrade, entGrade);
        }
        if (entStatus != null) {
            queryWrapper.eq(EnterpriseBaseInfo::getEntStatus, entStatus);
        }
        if (entProvince != null) {
            queryWrapper.eq(EnterpriseBaseInfo::getEntProvince, entProvince);
        }
        if (keyword != null && !keyword.isEmpty()) {
            queryWrapper.and(wrapper -> wrapper
                    .like(EnterpriseBaseInfo::getEntName, keyword)
                    .or()
                    .like(EnterpriseBaseInfo::getEntAddress, keyword)
                    .or()
                    .like(EnterpriseBaseInfo::getEntContact, keyword)
                    .or()
                    .like(EnterpriseBaseInfo::getEntMobilePhone, keyword));
        }

        queryWrapper.orderByAsc(EnterpriseBaseInfo::getEntName);

        Page<EnterpriseBaseInfo> page = enterpriseBaseInfoService.page(new Page<>(current, 10), queryWrapper);

        return Result.success(page);
    }

    /**
     * Create enterprise
     * Replaces: zjsEntFirst.aspx
     */
    @Operation(summary = "Create enterprise", description = "Create a new enterprise")
    @PostMapping
    public Result<Void> createEnterprise(@Valid @RequestBody EnterpriseBaseInfo enterpriseBaseInfo) {
        log.info("Creating enterprise: {}", enterpriseBaseInfo.getEntName());
        return enterpriseBaseInfoService.createEnterpriseBaseInfo(enterpriseBaseInfo);
    }

    /**
     * Update enterprise
     * Replaces: zjsEntChange.aspx
     */
    @Operation(summary = "Update enterprise", description = "Update enterprise information")
    @PutMapping
    public Result<Void> updateEnterprise(@Valid @RequestBody EnterpriseBaseInfo enterpriseBaseInfo) {
        log.info("Updating enterprise: {}", enterpriseBaseInfo.getId());
        return enterpriseBaseInfoService.updateEnterpriseBaseInfo(enterpriseBaseInfo);
    }

    /**
     * Delete enterprise
     */
    @Operation(summary = "Delete enterprise", description = "Delete an enterprise")
    @DeleteMapping("/{id}")
    public Result<Void> deleteEnterprise(@PathVariable Long id) {
        log.info("Deleting enterprise: {}", id);
        return enterpriseBaseInfoService.deleteEnterpriseBaseInfo(id);
    }

    /**
     * Get enterprise by ID
     * Replaces: zjsEntDetail.aspx
     */
    @Operation(summary = "Get enterprise by ID", description = "Get detailed enterprise information")
    @GetMapping("/{id}")
    public Result<EnterpriseBaseInfo> getEnterpriseById(@PathVariable Long id) {
        log.debug("Getting enterprise: {}", id);
        return enterpriseBaseInfoService.getEnterpriseBaseInfoById(id);
    }

    /**
     * Get enterprise by organization code
     */
    @Operation(summary = "Get enterprise by code", description = "Get enterprise by organization code")
    @GetMapping("/code/{entOrganizationsCode}")
    public Result<EnterpriseBaseInfo> getEnterpriseByCode(@PathVariable String entOrganizationsCode) {
        log.debug("Getting enterprise by code: {}", entOrganizationsCode);
        return enterpriseBaseInfoService.getEnterpriseByCode(entOrganizationsCode);
    }

    /**
     * Get enterprises by type
     */
    @Operation(summary = "Get enterprises by type", description = "Filter enterprises by type")
    @GetMapping("/type/{entType}")
    public Result<List<EnterpriseBaseInfo>> getEnterprisesByType(@PathVariable String entType) {
        log.debug("Getting enterprises by type: {}", entType);
        return enterpriseBaseInfoService.getEnterprisesByType(entType);
    }

    /**
     * Get enterprises by grade
     */
    @Operation(summary = "Get enterprises by grade", description = "Filter enterprises by grade")
    @GetMapping("/grade/{entGrade}")
    public Result<List<EnterpriseBaseInfo>> getEnterprisesByGrade(@PathVariable String entGrade) {
        log.debug("Getting enterprises by grade: {}", entGrade);
        return enterpriseBaseInfoService.getEnterprisesByGrade(entGrade);
    }

    /**
     * Get enterprises by status
     */
    @Operation(summary = "Get enterprises by status", description = "Filter enterprises by status")
    @GetMapping("/status/{entStatus}")
    public Result<List<EnterpriseBaseInfo>> getEnterprisesByStatus(@PathVariable String entStatus) {
        log.debug("Getting enterprises by status: {}", entStatus);
        return enterpriseBaseInfoService.getEnterprisesByStatus(entStatus);
    }

    /**
     * Get enterprises by province
     */
    @Operation(summary = "Get enterprises by province", description = "Filter enterprises by province")
    @GetMapping("/province/{entProvince}")
    public Result<List<EnterpriseBaseInfo>> getEnterprisesByProvince(@PathVariable String entProvince) {
        log.debug("Getting enterprises by province: {}", entProvince);
        return enterpriseBaseInfoService.getEnterprisesByProvince(entProvince);
    }

    /**
     * Get all enterprises
     */
    @Operation(summary = "Get all enterprises", description = "Get all enterprises")
    @GetMapping("/all")
    public Result<List<EnterpriseBaseInfo>> getAllEnterprises() {
        log.debug("Getting all enterprises");
        return enterpriseBaseInfoService.getAllEnterprises();
    }

    /**
     * Get recent registered enterprises
     */
    @Operation(summary = "Get recent enterprises", description = "Get enterprises registered in the last N days")
    @GetMapping("/recent")
    public Result<List<EnterpriseBaseInfo>> getRecentEnterprises(@RequestParam(defaultValue = "30") int days) {
        log.debug("Getting recent enterprises (last {} days)", days);
        return enterpriseBaseInfoService.getRecentEnterprises(days);
    }
}
