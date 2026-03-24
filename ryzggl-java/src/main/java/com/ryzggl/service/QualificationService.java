package com.ryzggl.service;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.extension.service.IService;
import com.ryzggl.entity.Qualification;
import com.ryzggl.repository.QualificationRepository;
import com.ryzggl.common.Result;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;

/**
 * Qualification Service
 * 资格证书管理业务逻辑层
 */
@Service
public class QualificationService implements IService<Qualification> {

    @Autowired
    private QualificationRepository qualificationRepository;

    /**
     * Get qualification by ID
     */
    public Qualification getQualificationById(Long qualId) {
        return qualificationRepository.getById(qualId);
    }

    /**
     * Get qualification by code
     */
    public Qualification getQualificationByCode(String qualCode) {
        return qualificationRepository.getByCode(qualCode);
    }

    /**
     * Get all valid qualifications
     */
    public List<Qualification> getAllValid() {
        return qualificationRepository.getAllValid();
    }

    /**
     * Get qualifications by type
     */
    public List<Qualification> getQualificationsByType(String qualType) {
        return qualificationRepository.getByType(qualType);
    }

    /**
     * Get qualifications by level
     */
    public List<Qualification> getQualificationsByLevel(String qualLevel) {
        return qualificationRepository.getByLevel(qualLevel);
    }

    /**
     * Get qualifications by category
     */
    public List<Qualification> getQualificationsByCategory(String qualCategory) {
        return qualificationRepository.getByCategory(qualCategory);
    }

    /**
     * Get qualifications by province
     */
    public List<Qualification> getQualificationsByProvince(String zGZSBH) {
        return qualificationRepository.getByProvince(zGZSBH);
    }

    /**
     * Search qualifications
     */
    public List<Qualification> searchQualifications(String keyword) {
        if (keyword == null || keyword.trim().isEmpty()) {
            return getAllValid();
        }
        return qualificationRepository.search(keyword);
    }

    /**
     * Create qualification
     */
    @Transactional
    public Result<Qualification> createQualification(Qualification qualification) {
        // Validate qualification code
        if (qualification.getQualCode() == null || qualification.getQualCode().trim().isEmpty()) {
            return Result.error("资格编码不能为空");
        }

        // Check if code already exists
        int count = qualificationRepository.countByCode(qualification.getQualCode());
        if (count > 0) {
            return Result.error("资格编码已存在");
        }

        // Validate required fields
        if (qualification.getQualName() == null || qualification.getQualName().trim().isEmpty()) {
            return Result.error("资格名称不能为空");
        }
        if (qualification.getQualType() == null || qualification.getQualType().trim().isEmpty()) {
            return Result.error("资格类型不能为空");
        }

        // Set default values
        if (qualification.getIsValid() == null) {
            qualification.setIsValid(true);
        }

        qualificationRepository.insert(qualification);
        return Result.success(qualification);
    }

    /**
     * Update qualification
     */
    @Transactional
    public Result<Qualification> updateQualification(Qualification qualification) {
        if (qualification.getQualId() == null) {
            return Result.error("资格ID不能为空");
        }

        // Check if exists
        Qualification existing = qualificationRepository.getById(qualification.getQualId());
        if (existing == null) {
            return Result.error("资格不存在");
        }

        // If code is being changed, check for duplicates
        String currentCode = existing.getQualCode();
        String newCode = qualification.getQualCode();
        if (newCode != null && !newCode.equals(currentCode)) {
            int count = qualificationRepository.countByCode(newCode);
            if (count > 0) {
                return Result.error("资格编码已存在");
            }
        }

        qualificationRepository.updateById(qualification);
        return Result.success(qualification);
    }

    /**
     * Delete qualification
     */
    @Transactional
    public Result<Void> deleteQualification(Long qualId) {
        int count = qualificationRepository.countById(qualId);
        if (count == 0) {
            return Result.error("资格不存在");
        }

        qualificationRepository.deleteById(qualId);
        return Result.success();
    }

    /**
     * Update validity
     */
    @Transactional
    public Result<Void> updateValidity(Long qualId, Boolean isValid) {
        int count = qualificationRepository.countById(qualId);
        if (count == 0) {
            return Result.error("资格不存在");
        }

        qualificationRepository.updateValidity(qualId, isValid);
        return Result.success();
    }

    /**
     * Batch update validity
     */
    @Transactional
    public Result<Void> batchUpdateValidity(List<Long> qualIds, Boolean isValid) {
        for (Long id : qualIds) {
            qualificationRepository.updateValidity(id, isValid);
        }
        return Result.success();
    }
}
