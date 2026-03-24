package com.ryzggl.controller;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.common.Result;
import com.ryzggl.entity.Dictionary;
import com.ryzggl.service.DictionaryService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.Map;

/**
 * Dictionary Controller
 * 数据字典REST API
 *
 * Maps to legacy DictionaryDAL.cs
 * Base path: /api/v1/dictionaries
 */
@Tag(name = "Dictionary Management", description = "数据字典管理API")
@RestController
@RequestMapping("/api/v1/dictionaries")
public class DictionaryController {

    @Autowired
    private DictionaryService dictionaryService;

    /**
     * Get dictionary by ID
     */
    @Operation(summary = "Get dictionary by ID")
    @GetMapping("/{dicId}")
    public Result<Dictionary> getById(@PathVariable String dicId) {
        Dictionary dictionary = dictionaryService.getById(dicId);
        if (dictionary == null) {
            return Result.error("字典不存在");
        }
        return Result.success(dictionary);
    }

    /**
     * Get all dictionaries
     */
    @Operation(summary = "Get all dictionaries")
    @GetMapping("/all")
    public Result<List<Dictionary>> getAll() {
        List<Dictionary> dictionaries = dictionaryService.getAll();
        return Result.success(dictionaries);
    }

    /**
     * Get dictionaries by type ID
     */
    @Operation(summary = "Get dictionaries by type ID")
    @GetMapping("/type/{typeId}")
    public Result<List<Dictionary>> getByTypeId(@PathVariable Integer typeId) {
        List<Dictionary> dictionaries = dictionaryService.getByTypeId(typeId);
        return Result.success(dictionaries);
    }

    /**
     * Get dictionary name by type ID and order ID
     */
    @Operation(summary = "Get dictionary name")
    @GetMapping("/name")
    public Result<String> getDicName(@RequestParam Integer typeId, @RequestParam Integer orderId) {
        String dicName = dictionaryService.getDicNameByTypeIdAndOrderId(typeId, orderId);
        if (dicName == null) {
            return Result.error("未找到对应的字典项");
        }
        return Result.success(dicName);
    }

    /**
     * Search dictionaries by type and name
     */
    @Operation(summary = "Search dictionaries by type and name")
    @GetMapping("/search-by-type")
    public Result<List<Dictionary>> searchByName(
            @RequestParam(required = false) Integer typeId,
            @RequestParam(required = false) String dicName
    ) {
        List<Dictionary> dictionaries = dictionaryService.searchByName(typeId, dicName);
        return Result.success(dictionaries);
    }

    /**
     * Search dictionaries globally
     */
    @Operation(summary = "Search dictionaries")
    @GetMapping("/search")
    public Result<List<Dictionary>> search(@RequestParam String keyword) {
        List<Dictionary> dictionaries = dictionaryService.search(keyword);
        return Result.success(dictionaries);
    }

    /**
     * Get dropdown data for a type
     */
    @Operation(summary = "Get dropdown data")
    @GetMapping("/dropdown/{typeId}")
    public Result<List<Dictionary>> getDropdownData(@PathVariable Integer typeId) {
        List<Dictionary> dictionaries = dictionaryService.getDropdownData(typeId);
        return Result.success(dictionaries);
    }

    /**
     * Create dictionary
     */
    @Operation(summary = "Create dictionary")
    @PostMapping("/create")
    public Result<Dictionary> create(@RequestBody Dictionary dictionary) {
        return dictionaryService.createDictionary(dictionary);
    }

    /**
     * Update dictionary
     */
    @Operation(summary = "Update dictionary")
    @PutMapping("/{dicId}")
    public Result<Dictionary> update(@PathVariable String dicId, @RequestBody Dictionary dictionary) {
        dictionary.setDicId(dicId);
        return dictionaryService.updateDictionary(dictionary);
    }

    /**
     * Delete dictionary
     */
    @Operation(summary = "Delete dictionary")
    @DeleteMapping("/{dicId}")
    public Result<Void> delete(@PathVariable String dicId) {
        return dictionaryService.deleteDictionary(dicId);
    }

    /**
     * Batch delete dictionaries
     */
    @Operation(summary = "Batch delete dictionaries")
    @DeleteMapping("/batch")
    public Result<Void> batchDelete(@RequestBody List<String> dicIds) {
        for (String dicId : dicIds) {
            dictionaryService.deleteDictionary(dicId);
        }
        return Result.success();
    }
}
