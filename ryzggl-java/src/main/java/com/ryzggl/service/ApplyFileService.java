package com.ryzggl.service;

import com.baomidou.mybatisplus.extension.service.IService;
import com.ryzggl.entity.ApplyFile;
import com.ryzggl.repository.ApplyFileRepository;
import com.ryzggl.common.Result;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;

/**
 * ApplyFile Service
 * 申请文件业务逻辑层
 */
@Service
public class ApplyFileService implements IService<ApplyFile> {

    @Autowired
    private ApplyFileRepository applyFileRepository;

    /**
     * Get by ID
     */
    public ApplyFile getById(String fileId) {
        return applyFileRepository.getById(fileId);
    }

    /**
     * Get all apply files
     */
    public List<ApplyFile> getAll() {
        return applyFileRepository.getAll();
    }

    /**
     * Get by apply ID
     */
    public List<ApplyFile> getByApplyId(String applyId) {
        return applyFileRepository.getByApplyId(applyId);
    }

    /**
     * Get by check result
     */
    public List<ApplyFile> getByCheckResult(Integer checkResult) {
        return applyFileRepository.getByCheckResult(checkResult);
    }

    /**
     * Search by keyword
     */
    public List<ApplyFile> search(String keyword) {
        if (keyword == null || keyword.trim().isEmpty()) {
            return applyFileRepository.getAll();
        }
        return applyFileRepository.search(keyword);
    }

    /**
     * Create apply file
     */
    @Transactional
    public Result<ApplyFile> createApplyFile(ApplyFile applyFile) {
        // Validate required fields
        if (applyFile.getFileId() == null || applyFile.getFileId().trim().isEmpty()) {
            return Result.error("文件ID不能为空");
        }
        if (applyFile.getApplyId() == null || applyFile.getApplyId().trim().isEmpty()) {
            return Result.error("申请ID不能为空");
        }

        applyFileRepository.insertApplyFile(applyFile);
        return Result.success(applyFile);
    }

    /**
     * Update apply file
     */
    @Transactional
    public Result<ApplyFile> updateApplyFile(ApplyFile applyFile) {
        ApplyFile existing = getById(applyFile.getFileId());
        if (existing == null) {
            return Result.error("申请文件不存在");
        }

        applyFileRepository.updateApplyFile(applyFile);
        return Result.success(applyFile);
    }

    /**
     * Delete apply file
     */
    @Transactional
    public Result<Void> deleteApplyFile(String fileId, String applyId) {
        ApplyFile existing = getById(fileId);
        if (existing == null) {
            return Result.error("申请文件不存在");
        }

        applyFileRepository.deleteApplyFile(fileId, applyId);
        return Result.success();
    }

    /**
     * Delete by apply ID
     */
    @Transactional
    public Result<Void> deleteByApplyId(String applyId) {
        int count = applyFileRepository.deleteByApplyId(applyId);
        return Result.success();
    }

    /**
     * Get total count
     */
    public int count() {
        return applyFileRepository.count();
    }

    /**
     * Get count by apply ID
     */
    public int countByApplyId(String applyId) {
        return applyFileRepository.countByApplyId(applyId);
    }

    /**
     * Get count by check result
     */
    public int countByCheckResult(Integer checkResult) {
        return applyFileRepository.countByCheckResult(checkResult);
    }

    /**
     * Get check result description
     */
    public String getCheckResultDescription(Integer checkResult) {
        if (checkResult == null) {
            return "未审核";
        }
        switch (checkResult) {
            case 1: return "通过";
            case 2: return "不通过";
            case 3: return "待审核";
            default: return "未知";
        }
    }
}
