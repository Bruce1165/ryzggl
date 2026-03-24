package com.ryzggl.controller;

import com.ryzggl.common.Result;
import com.ryzggl.entity.CertificateAddItem;
import com.ryzggl.service.CertificateAddItemService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

/**
 * CertificateAddItem Controller
 * 证书增项管理API
 */
@Tag(name = "证书增项管理", description = "证书增项管理相关接口")
@RestController
@RequestMapping("/api/v1/certificate-add-items")
public class CertificateAddItemController {

    @Autowired
    private CertificateAddItemService certificateAddItemService;

    /**
     * Get all certificate add items
     */
    @Operation(summary = "获取所有证书增项")
    @GetMapping("/all")
    public Result<List<CertificateAddItem>> getAll() {
        List<CertificateAddItem> list = certificateAddItemService.getAll();
        return Result.success(list);
    }

    /**
     * Get certificate add item by ID
     */
    @Operation(summary = "根据ID获取证书增项")
    @GetMapping("/{certificateAddItemId}")
    public Result<CertificateAddItem> getById(@PathVariable Long certificateAddItemId) {
        CertificateAddItem item = certificateAddItemService.getById(certificateAddItemId);
        if (item == null) {
            return Result.error("证书增项不存在");
        }
        return Result.success(item);
    }

    /**
     * Get certificate add items by certificate ID
     */
    @Operation(summary = "根据证书ID获取证书增项")
    @GetMapping("/certificate/{certificateId}")
    public Result<List<CertificateAddItem>> getByCertificateId(@PathVariable Long certificateId) {
        List<CertificateAddItem> list = certificateAddItemService.getByCertificateId(certificateId);
        return Result.success(list);
    }

    /**
     * Get certificate add items by post type ID
     */
    @Operation(summary = "根据岗位类型ID获取证书增项")
    @GetMapping("/post-type/{postTypeId}")
    public Result<List<CertificateAddItem>> getByPostTypeId(@PathVariable Integer postTypeId) {
        List<CertificateAddItem> list = certificateAddItemService.getByPostTypeId(postTypeId);
        return Result.success(list);
    }

    /**
     * Get certificate add items by post ID
     */
    @Operation(summary = "根据岗位ID获取证书增项")
    @GetMapping("/post/{postId}")
    public Result<List<CertificateAddItem>> getByPostId(@PathVariable Integer postId) {
        List<CertificateAddItem> list = certificateAddItemService.getByPostId(postId);
        return Result.success(list);
    }

    /**
     * Get certificate add items by case status
     */
    @Operation(summary = "根据案件状态获取证书增项")
    @GetMapping("/case-status/{caseStatus}")
    public Result<List<CertificateAddItem>> getByCaseStatus(@PathVariable String caseStatus) {
        List<CertificateAddItem> list = certificateAddItemService.getByCaseStatus(caseStatus);
        return Result.success(list);
    }

    /**
     * Get add item names by certificate ID
     */
    @Operation(summary = "根据证书ID获取增项名称")
    @GetMapping("/names/{certificateId}")
    public Result<List<String>> getAddItemNames(@PathVariable Long certificateId) {
        List<String> names = certificateAddItemService.getAddItemNames(certificateId);
        return Result.success(names);
    }

    /**
     * Get add item names string (comma-separated)
     */
    @Operation(summary = "根据证书ID获取增项名称字符串")
    @GetMapping("/names-string/{certificateId}")
    public Result<String> getAddItemNameString(@PathVariable Long certificateId) {
        String nameString = certificateAddItemService.getAddItemNameString(certificateId);
        return Result.success(nameString);
    }

    /**
     * Create certificate add item
     */
    @Operation(summary = "创建证书增项")
    @PostMapping
    public Result<CertificateAddItem> create(@RequestBody CertificateAddItem certificateAddItem) {
        return certificateAddItemService.createCertificateAddItem(certificateAddItem);
    }

    /**
     * Update certificate add item
     */
    @Operation(summary = "更新证书增项")
    @PutMapping("/{certificateAddItemId}")
    public Result<CertificateAddItem> update(@PathVariable Long certificateAddItemId, @RequestBody CertificateAddItem certificateAddItem) {
        certificateAddItem.setCertificateAddItemId(certificateAddItemId);
        return certificateAddItemService.updateCertificateAddItem(certificateAddItem);
    }

    /**
     * Delete certificate add item
     */
    @Operation(summary = "删除证书增项")
    @DeleteMapping("/{certificateAddItemId}")
    public Result<Void> delete(@PathVariable Long certificateAddItemId) {
        return certificateAddItemService.deleteCertificateAddItem(certificateAddItemId);
    }

    /**
     * Delete by certificate ID
     */
    @Operation(summary = "根据证书ID删除证书增项")
    @DeleteMapping("/certificate/{certificateId}")
    public Result<Void> deleteByCertificateId(@PathVariable Long certificateId) {
        return certificateAddItemService.deleteByCertificateId(certificateId);
    }

    /**
     * Get total count
     */
    @Operation(summary = "获取证书增项总数")
    @GetMapping("/count")
    public Result<Integer> count() {
        int count = certificateAddItemService.count();
        return Result.success(count);
    }

    /**
     * Get count by certificate ID
     */
    @Operation(summary = "根据证书ID获取增项数量")
    @GetMapping("/count/{certificateId}")
    public Result<Integer> countByCertificateId(@PathVariable Long certificateId) {
        int count = certificateAddItemService.countByCertificateId(certificateId);
        return Result.success(count);
    }

    /**
     * Get count by case status
     */
    @Operation(summary = "根据案件状态获取数量")
    @GetMapping("/count/case-status/{caseStatus}")
    public Result<Integer> countByCaseStatus(@PathVariable String caseStatus) {
        int count = certificateAddItemService.countByCaseStatus(caseStatus);
        return Result.success(count);
    }
}
