package com.ryzggl.controller;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.common.Result;
import com.ryzggl.entity.LearnRecord;
import com.ryzggl.service.LearnRecordService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

/**
 * LearnRecord Controller
 * 学习记录REST API
 *
 * Maps to legacy LearnRecordDAL.cs
 * Base path: /api/v1/learn-records
 */
@Tag(name = "LearnRecord Management", description = "学习记录管理API")
@RestController
@RequestMapping("/api/v1/learn-records")
public class LearnRecordController {

    @Autowired
    private LearnRecordService learnRecordService;

    /**
     * Get record by ID
     */
    @Operation(summary = "Get learn record by ID")
    @GetMapping("/{recordId}")
    public Result<LearnRecord> getById(@PathVariable Long recordId) {
        LearnRecord record = learnRecordService.getById(recordId);
        if (record == null) {
            return Result.error("学习记录不存在");
        }
        return Result.success(record);
    }

    /**
     * Get records by worker certificate code
     */
    @Operation(summary = "Get learn records by worker certificate code")
    @GetMapping("/worker/{workerCertificateCode}")
    public Result<List<LearnRecord>> getByWorkerCertificateCode(@PathVariable String workerCertificateCode) {
        List<LearnRecord> records = learnRecordService.getByWorkerCertificateCode(workerCertificateCode);
        return Result.success(records);
    }

    /**
     * Get records by post name
     */
    @Operation(summary = "Get learn records by post name")
    @GetMapping("/post/{postName}")
    public Result<List<LearnRecord>> getByPostName(@PathVariable String postName) {
        List<LearnRecord> records = learnRecordService.getByPostName(postName);
        return Result.success(records);
    }

    /**
     * Get records by worker name
     */
    @Operation(summary = "Get learn records by worker name")
    @GetMapping("/worker/{workerName}")
    public Result<List<LearnRecord>> getByWorkerName(@PathVariable String workerName) {
        List<LearnRecord> records = learnRecordService.getByWorkerName(workerName);
        return Result.success(records);
    }

    /**
     * Search records
     */
    @Operation(summary = "Search learn records")
    @GetMapping("/search")
    public Result<List<LearnRecord>> search(@RequestParam String keyword) {
        List<LearnRecord> records = learnRecordService.search(keyword);
        return Result.success(records);
    }

    /**
     * Get all records with pagination
     */
    @Operation(summary = "Get all learn records with pagination")
    @GetMapping
    public Result<Page<LearnRecord>> list(
            @RequestParam(defaultValue = "1") int current,
            @RequestParam(defaultValue = "10") int size,
            @RequestParam(required = false) String keyword
    ) {
        Page<LearnRecord> page = new Page<>(current, size);
        QueryWrapper<LearnRecord> queryWrapper = new QueryWrapper<>();

        if (keyword != null && !keyword.isEmpty()) {
            queryWrapper.and(wrapper -> wrapper
                    .like("WorkerName", keyword)
                    .or()
                    .like("PostName", keyword)
                    .or()
                    .like("LinkTel", keyword)
            );
        }
        queryWrapper.orderByDesc("RecordID");

        Page<LearnRecord> result = learnRecordService.page(page, queryWrapper);
        return Result.success(result);
    }

    /**
     * Create learn record
     */
    @Operation(summary = "Create learn record")
    @PostMapping("/create")
    public Result<LearnRecord> create(@RequestBody LearnRecord learnRecord) {
        return learnRecordService.createRecord(learnRecord);
    }

    /**
     * Update learn record
     */
    @Operation(summary = "Update learn record")
    @PutMapping("/{recordId}")
    public Result<LearnRecord> update(@PathVariable Long recordId, @RequestBody LearnRecord learnRecord) {
        learnRecord.setRecordId(recordId);
        return learnRecordService.updateRecord(learnRecord);
    }

    /**
     * Delete learn record
     */
    @Operation(summary = "Delete learn record")
    @DeleteMapping("/{recordId}")
    public Result<Void> delete(@PathVariable Long recordId) {
        return learnRecordService.deleteRecord(recordId);
    }
}
