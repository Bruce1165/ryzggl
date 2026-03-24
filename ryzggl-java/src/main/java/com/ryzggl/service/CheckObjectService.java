package com.ryzggl.service;

import com.baomidou.mybatisplus.extension.service.IService;
import com.ryzggl.entity.CheckObject;
import com.ryzggl.repository.CheckObjectRepository;
import com.ryzggl.common.Result;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;

/**
 * CheckObject Service
 * 核查对象业务逻辑层
 */
@Service
public class CheckObjectService implements IService<CheckObject> {

    @Autowired
    private CheckObjectRepository checkObjectRepository;

    /**
     * Get by ID
     */
    public CheckObject getById(String checkId) {
        return checkObjectRepository.getById(checkId);
    }

    /**
     * Get all check objects
     */
    public List<CheckObject> getAll() {
        return checkObjectRepository.getAll();
    }

    /**
     * Get by check year
     */
    public List<CheckObject> getByCheckYear(Integer checkYear) {
        return checkObjectRepository.getByCheckYear(checkYear);
    }

    /**
     * Get by register number
     */
    public List<CheckObject> getByPsnRegisterNo(String psnRegisterNo) {
        return checkObjectRepository.getByPsnRegisterNo(psnRegisterNo);
    }

    /**
     * Get by certificate number
     */
    public List<CheckObject> getByPsnCertificateNo(String psnCertificateNo) {
        return checkObjectRepository.getByPsnCertificateNo(psnCertificateNo);
    }

    /**
     * Get by person name
     */
    public List<CheckObject> getByPsnName(String psnName) {
        return checkObjectRepository.getByPsnName(psnName);
    }

    /**
     * Get by apply type
     */
    public List<CheckObject> getByApplyType(String applyType) {
        return checkObjectRepository.getByApplyType(applyType);
    }

    /**
     * Search by keyword
     */
    public List<CheckObject> search(String keyword) {
        if (keyword == null || keyword.trim().isEmpty()) {
            return checkObjectRepository.getAll();
        }
        return checkObjectRepository.search(keyword);
    }

    /**
     * Create check object
     */
    @Transactional
    public Result<CheckObject> createCheckObject(CheckObject checkObject) {
        // Validate required fields
        if (checkObject.getCheckId() == null || checkObject.getCheckId().trim().isEmpty()) {
            return Result.error("检查ID不能为空");
        }
        if (checkObject.getCheckYear() == null) {
            return Result.error("检查年度不能为空");
        }
        if (checkObject.getPsnRegisterNo() == null || checkObject.getPsnRegisterNo().trim().isEmpty()) {
            return Result.error("注册编号不能为空");
        }
        if (checkObject.getPsnName() == null || checkObject.getPsnName().trim().isEmpty()) {
            return Result.error("人员姓名不能为空");
        }

        checkObjectRepository.insertCheckObject(checkObject);
        return Result.success(checkObject);
    }

    /**
     * Update check object
     */
    @Transactional
    public Result<CheckObject> updateCheckObject(CheckObject checkObject) {
        CheckObject existing = getById(checkObject.getCheckId());
        if (existing == null) {
            return Result.error("核查对象不存在");
        }

        checkObjectRepository.updateCheckObject(checkObject);
        return Result.success(checkObject);
    }

    /**
     * Delete check object
     */
    @Transactional
    public Result<Void> deleteCheckObject(String checkId) {
        CheckObject existing = getById(checkId);
        if (existing == null) {
            return Result.error("核查对象不存在");
        }

        checkObjectRepository.deleteCheckObject(checkId);
        return Result.success();
    }

    /**
     * Get total count
     */
    public int count() {
        return checkObjectRepository.count();
    }
}
