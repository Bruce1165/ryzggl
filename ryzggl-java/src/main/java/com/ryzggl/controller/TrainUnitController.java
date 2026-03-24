package com.ryzggl.controller;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.common.Result;
import com.ryzggl.entity.TrainUnit;
import com.ryzggl.service.TrainUnitService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

/**
 * TrainUnit Controller
 * 培训单位REST API
 *
 * Maps to legacy TrainUnitDAL.cs
 * Base path: /api/v1/train-units
 */
@Tag(name = "TrainUnit Management", description = "培训单位管理API")
@RestController
@RequestMapping("/api/v1/train-units")
public class TrainUnitController {

    @Autowired
    private TrainUnitService trainUnitService;

    /**
     * Get train unit by ID
     */
    @Operation(summary = "Get train unit by ID")
    @GetMapping("/{unitNo}")
    public Result<TrainUnit> getById(@PathVariable String unitNo) {
        TrainUnit trainUnit = trainUnitService.getById(unitNo);
        if (trainUnit == null) {
            return Result.error("培训单位不存在");
        }
        return Result.success(trainUnit);
    }

    /**
     * Get all train units
     */
    @Operation(summary = "Get all train units")
    @GetMapping("/all")
    public Result<List<TrainUnit>> getAll() {
        List<TrainUnit> trainUnits = trainUnitService.getAll();
        return Result.success(trainUnits);
    }

    /**
     * Get active train units
     */
    @Operation(summary = "Get active train units")
    @GetMapping("/active")
    public Result<List<TrainUnit>> getActive() {
        List<TrainUnit> trainUnits = trainUnitService.getActive();
        return Result.success(trainUnits);
    }

    /**
     * Search train units
     */
    @Operation(summary = "Search train units")
    @GetMapping("/search")
    public Result<List<TrainUnit>> search(@RequestParam String keyword) {
        List<TrainUnit> trainUnits = trainUnitService.search(keyword);
        return Result.success(trainUnits);
    }

    /**
     * Get all train units with pagination
     */
    @Operation(summary = "Get all train units with pagination")
    @GetMapping
    public Result<Page<TrainUnit>> list(
            @RequestParam(defaultValue = "1") int current,
            @RequestParam(defaultValue = "10") int size,
            @RequestParam(required = false) String keyword
    ) {
        Page<TrainUnit> page = new Page<>(current, size);
        QueryWrapper<TrainUnit> queryWrapper = new QueryWrapper<>();

        if (keyword != null && !keyword.isEmpty()) {
            queryWrapper.and(wrapper -> wrapper
                    .like("TrainUnitName", keyword)
                    .or()
                    .like("UnitCode", keyword)
            );
        }
        queryWrapper.orderByAsc("UnitNo");

        Page<TrainUnit> result = trainUnitService.page(page, queryWrapper);
        return Result.success(result);
    }

    /**
     * Create train unit
     */
    @Operation(summary = "Create train unit")
    @PostMapping("/create")
    public Result<TrainUnit> createTrainUnit(@RequestBody TrainUnit trainUnit) {
        return trainUnitService.createTrainUnit(trainUnit);
    }

    /**
     * Update train unit
     */
    @Operation(summary = "Update train unit")
    @PutMapping("/{unitNo}")
    public Result<TrainUnit> updateTrainUnit(@PathVariable String unitNo, @RequestBody TrainUnit trainUnit) {
        trainUnit.setUnitNo(unitNo);
        return trainUnitService.updateTrainUnit(trainUnit);
    }

    /**
     * Update train unit status
     */
    @Operation(summary = "Update train unit status")
    @PutMapping("/{unitNo}/status")
    public Result<TrainUnit> updateTrainUnitStatus(@PathVariable String unitNo, @RequestParam Integer useStatus) {
        return trainUnitService.updateTrainUnitStatus(unitNo, useStatus);
    }

    /**
     * Delete train unit
     */
    @Operation(summary = "Delete train unit")
    @DeleteMapping("/{unitNo}")
    public Result<Void> deleteTrainUnit(@PathVariable String unitNo) {
        return trainUnitService.deleteTrainUnit(unitNo);
    }
}
