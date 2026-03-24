package com.ryzggl.controller;

import com.ryzggl.common.Result;
import com.ryzggl.entity.CheckObject;
import com.ryzggl.service.CheckObjectService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

/**
 * CheckObject Controller
 * 核查对象管理API
 */
@Tag(name = "核查对象管理", description = "核查对象管理相关接口")
@RestController
@RequestMapping("/api/v1/check-objects")
public class CheckObjectController {

    @Autowired
    private CheckObjectService checkObjectService;

    /**
     * Get all check objects
     */
    @Operation(summary = "获取所有核查对象")
    @GetMapping("/all")
    public Result<List<CheckObject>> getAll() {
        List<CheckObject> list = checkObjectService.getAll();
        return Result.success(list);
    }

    /**
     * Get check object by ID
     */
    @Operation(summary = "根据ID获取核查对象")
    @GetMapping("/{checkId}")
    public Result<CheckObject> getById(@PathVariable String checkId) {
        CheckObject checkObject = checkObjectService.getById(checkId);
        if (checkObject == null) {
            return Result.error("核查对象不存在");
        }
        return Result.success(checkObject);
    }

    /**
     * Get check objects by year
     */
    @Operation(summary = "根据检查年度获取核查对象")
    @GetMapping("/year/{checkYear}")
    public Result<List<CheckObject>> getByYear(@PathVariable Integer checkYear) {
        List<CheckObject> list = checkObjectService.getByCheckYear(checkYear);
        return Result.success(list);
    }

    /**
     * Get check objects by register number
     */
    @Operation(summary = "根据注册编号获取核查对象")
    @GetMapping("/register/{psnRegisterNo}")
    public Result<List<CheckObject>> getByRegisterNo(@PathVariable String psnRegisterNo) {
        List<CheckObject> list = checkObjectService.getByPsnRegisterNo(psnRegisterNo);
        return Result.success(list);
    }

    /**
     * Get check objects by certificate number
     */
    @Operation(summary = "根据证件号码获取核查对象")
    @GetMapping("/certificate/{psnCertificateNo}")
    public Result<List<CheckObject>> getByCertificateNo(@PathVariable String psnCertificateNo) {
        List<CheckObject> list = checkObjectService.getByPsnCertificateNo(psnCertificateNo);
        return Result.success(list);
    }

    /**
     * Get check objects by person name
     */
    @Operation(summary = "根据人员姓名获取核查对象")
    @GetMapping("/person/{psnName}")
    public Result<List<CheckObject>> getByPersonName(@PathVariable String psnName) {
        List<CheckObject> list = checkObjectService.getByPsnName(psnName);
        return Result.success(list);
    }

    /**
     * Get check objects by apply type
     */
    @Operation(summary = "根据申报类型获取核查对象")
    @GetMapping("/apply-type/{applyType}")
    public Result<List<CheckObject>> getByApplyType(@PathVariable String applyType) {
        List<CheckObject> list = checkObjectService.getByApplyType(applyType);
        return Result.success(list);
    }

    /**
     * Search check objects
     */
    @Operation(summary = "搜索核查对象")
    @GetMapping("/search")
    public Result<List<CheckObject>> search(@RequestParam String keyword) {
        List<CheckObject> list = checkObjectService.search(keyword);
        return Result.success(list);
    }

    /**
     * Create check object
     */
    @Operation(summary = "创建核查对象")
    @PostMapping
    public Result<CheckObject> create(@RequestBody CheckObject checkObject) {
        return checkObjectService.createCheckObject(checkObject);
    }

    /**
     * Update check object
     */
    @Operation(summary = "更新核查对象")
    @PutMapping("/{checkId}")
    public Result<CheckObject> update(@PathVariable String checkId, @RequestBody CheckObject checkObject) {
        checkObject.setCheckId(checkId);
        return checkObjectService.updateCheckObject(checkObject);
    }

    /**
     * Delete check object
     */
    @Operation(summary = "删除核查对象")
    @DeleteMapping("/{checkId}")
    public Result<Void> delete(@PathVariable String checkId) {
        return checkObjectService.deleteCheckObject(checkId);
    }

    /**
     * Get total count
     */
    @Operation(summary = "获取核查对象总数")
    @GetMapping("/count")
    public Result<Integer> count() {
        int count = checkObjectService.count();
        return Result.success(count);
    }
}
