package com.ryzggl.service;

import com.baomidou.mybatisplus.extension.service.IService;
import com.ryzggl.entity.LearnRecord;
import com.ryzggl.repository.LearnRecordRepository;
import com.ryzggl.common.Result;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;

/**
 * LearnRecord Service
 * 学习记录业务逻辑层
 */
@Service
public class LearnRecordService implements IService<LearnRecord> {

    @Autowired
    private LearnRecordRepository learnRecordRepository;

    /**
     * Get record by ID
     */
    public LearnRecord getById(Long recordId) {
        return learnRecordRepository.getById(recordId);
    }

    /**
     * Get records by worker certificate code
     */
    public List<LearnRecord> getByWorkerCertificateCode(String workerCertificateCode) {
        return learnRecordRepository.getByWorkerCertificateCode(workerCertificateCode);
    }

    /**
     * Get records by post name
     */
    public List<LearnRecord> getByPostName(String postName) {
        return learnRecordRepository.getByPostName(postName);
    }

    /**
     * Get records by worker name
     */
    public List<LearnRecord> getByWorkerName(String workerName) {
        return learnRecordRepository.getByWorkerName(workerName);
    }

    /**
     * Search records
     */
    public List<LearnRecord> search(String keyword) {
        if (keyword == null || keyword.trim().isEmpty()) {
            return learnRecordRepository.selectList(null);
        }
        return learnRecordRepository.search(keyword);
    }

    /**
     * Create learn record
     */
    @Transactional
    public Result<LearnRecord> createRecord(LearnRecord record) {
        // Validate required fields
        if (record.getPostName() == null || record.getPostName().trim().isEmpty()) {
            return Result.error("岗位名称不能为空");
        }

        if (record.getWorkerName() == null || record.getWorkerName().trim().isEmpty()) {
            return Result.error("人员姓名不能为空");
        }

        learnRecordRepository.insert(record);
        return Result.success(record);
    }

    /**
     * Update learn record
     */
    @Transactional
    public Result<LearnRecord> updateRecord(LearnRecord record) {
        // Validate required fields
        if (record.getRecordId() == null) {
            return Result.error("记录ID不能为空");
        }

        LearnRecord existing = getById(record.getRecordId());
        if (existing == null) {
            return Result.error("学习记录不存在");
        }

        learnRecordRepository.updateById(record);
        return Result.success(record);
    }

    /**
     * Delete learn record
     */
    @Transactional
    public Result<Void> deleteRecord(Long recordId) {
        LearnRecord existing = getById(recordId);
        if (existing == null) {
            return Result.error("学习记录不存在");
        }

        learnRecordRepository.deleteById(recordId);
        return Result.success();
    }
}
