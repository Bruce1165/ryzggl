package com.ryzggl.service;

import com.baomidou.mybatisplus.extension.service.IService;
import com.ryzggl.entity.CheckFeedBack;
import com.ryzggl.repository.CheckFeedBackRepository;
import com.ryzggl.common.Result;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;

/**
 * CheckFeedBack Service
 * 核查反馈业务逻辑层
 */
@Service
public class CheckFeedBackService implements IService<CheckFeedBack> {

    @Autowired
    private CheckFeedBackRepository checkFeedBackRepository;

    /**
     * Get by ID
     */
    public CheckFeedBack getById(String dataId) {
        return checkFeedBackRepository.getById(dataId);
    }

    /**
     * Get all feedback records
     */
    public List<CheckFeedBack> getAll() {
        return checkFeedBackRepository.getAll();
    }

    /**
     * Get by patch code
     */
    public List<CheckFeedBack> getByPatchCode(Integer patchCode) {
        return checkFeedBackRepository.getByPatchCode(patchCode);
    }

    /**
     * Get by check type
     */
    public List<CheckFeedBack> getByCheckType(String checkType) {
        return checkFeedBackRepository.getByCheckType(checkType);
    }

    /**
     * Get by status code
     */
    public List<CheckFeedBack> getByStatusCode(Integer dataStatusCode) {
        return checkFeedBackRepository.getByStatusCode(dataStatusCode);
    }

    /**
     * Get by worker name
     */
    public List<CheckFeedBack> getByWorkerName(String workerName) {
        return checkFeedBackRepository.getByWorkerName(workerName);
    }

    /**
     * Get by certificate code
     */
    public List<CheckFeedBack> getByCertificateCode(String certificateCode) {
        return checkFeedBackRepository.getByCertificateCode(certificateCode);
    }

    /**
     * Get by unit code
     */
    public List<CheckFeedBack> getByUnitCode(String unitCode) {
        return checkFeedBackRepository.getByUnitCode(unitCode);
    }

    /**
     * Get by country
     */
    public List<CheckFeedBack> getByCountry(String country) {
        return checkFeedBackRepository.getByCountry(country);
    }

    /**
     * Search by keyword
     */
    public List<CheckFeedBack> search(String keyword) {
        if (keyword == null || keyword.trim().isEmpty()) {
            return checkFeedBackRepository.getAll();
        }
        return checkFeedBackRepository.search(keyword);
    }

    /**
     * Get pending feedback records (status = 1)
     */
    public List<CheckFeedBack> getPendingFeedback() {
        return checkFeedBackRepository.getPendingFeedback();
    }

    /**
     * Get feedback requiring initial review (status = 3)
     */
    public List<CheckFeedBack> getPendingReview() {
        return checkFeedBackRepository.getPendingReview();
    }

    /**
     * Get feedback requiring city review (status = 4)
     */
    public List<CheckFeedBack> getPendingCityReview() {
        return checkFeedBackRepository.getPendingCityReview();
    }

    /**
     * Get feedback requiring decision (status = 6)
     */
    public List<CheckFeedBack> getPendingDecision() {
        return checkFeedBackRepository.getPendingDecision();
    }

    /**
     * Create feedback
     */
    @Transactional
    public Result<CheckFeedBack> createCheckFeedBack(CheckFeedBack checkFeedBack) {
        // Validate required fields
        if (checkFeedBack.getDataId() == null || checkFeedBack.getDataId().trim().isEmpty()) {
            return Result.error("填报单ID不能为空");
        }
        if (checkFeedBack.getWorkerName() == null || checkFeedBack.getWorkerName().trim().isEmpty()) {
            return Result.error("人员姓名不能为空");
        }
        if (checkFeedBack.getCertificateCode() == null || checkFeedBack.getCertificateCode().trim().isEmpty()) {
            return Result.error("证书编号不能为空");
        }
        if (checkFeedBack.getPhone() == null || checkFeedBack.getPhone().trim().isEmpty()) {
            return Result.error("电话不能为空");
        }

        // Set default values
        if (checkFeedBack.getDataStatusCode() == null) {
            checkFeedBack.setDataStatusCode(0); // 未发布
        }
        if (checkFeedBack.getDataStatus() == null) {
            checkFeedBack.setDataStatus("未发布");
        }

        checkFeedBackRepository.insertCheckFeedBack(checkFeedBack);
        return Result.success(checkFeedBack);
    }

    /**
     * Update feedback
     */
    @Transactional
    public Result<CheckFeedBack> updateCheckFeedBack(CheckFeedBack checkFeedBack) {
        CheckFeedBack existing = getById(checkFeedBack.getDataId());
        if (existing == null) {
            return Result.error("核查反馈记录不存在");
        }

        checkFeedBackRepository.updateCheckFeedBack(checkFeedBack);
        return Result.success(checkFeedBack);
    }

    /**
     * Delete feedback
     */
    @Transactional
    public Result<Void> deleteCheckFeedBack(String dataId) {
        CheckFeedBack existing = getById(dataId);
        if (existing == null) {
            return Result.error("核查反馈记录不存在");
        }

        checkFeedBackRepository.deleteCheckFeedBack(dataId);
        return Result.success();
    }

    /**
     * Get total count
     */
    public int count() {
        return checkFeedBackRepository.count();
    }

    /**
     * Get status description
     */
    public String getStatusDescription(Integer statusCode) {
        if (statusCode == null) {
            return "未知";
        }
        switch (statusCode) {
            case 0: return "未发布";
            case 1: return "待反馈";
            case 2: return "已驳回";
            case 3: return "待审查";
            case 4: return "待复审";
            case 6: return "待决定";
            case 7: return "已决定";
            default: return "未知";
        }
    }
}
