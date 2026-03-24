package com.ryzggl.service;

import com.baomidou.mybatisplus.extension.service.IService;
import com.ryzggl.entity.ApplyNews;
import com.ryzggl.repository.ApplyNewsRepository;
import com.ryzggl.common.Result;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;

/**
 * ApplyNews Service
 * 申请新闻业务逻辑层
 */
@Service
public class ApplyNewsService implements IService<ApplyNews> {

    @Autowired
    private ApplyNewsRepository applyNewsRepository;

    /**
     * Get by ID
     */
    public ApplyNews getById(String id) {
        return applyNewsRepository.getById(id);
    }

    /**
     * Get all apply news
     */
    public List<ApplyNews> getAll() {
        return applyNewsRepository.getAll();
    }

    /**
     * Get by organization code (unchecked only)
     */
    public List<ApplyNews> getByOrganizationCode(String entOrganizationsCode) {
        return applyNewsRepository.getByOrganizationCode(entOrganizationsCode);
    }

    /**
     * Get by person name
     */
    public List<ApplyNews> getByPsnName(String psnName) {
        return applyNewsRepository.getByPsnName(psnName);
    }

    /**
     * Get by certificate no
     */
    public List<ApplyNews> getByPsnCertificateNo(String psnCertificateNo) {
        return applyNewsRepository.getByPsnCertificateNo(psnCertificateNo);
    }

    /**
     * Get by register no
     */
    public List<ApplyNews> getByPsnRegisterNo(String psnRegisterNo) {
        return applyNewsRepository.getByPsnRegisterNo(psnRegisterNo);
    }

    /**
     * Get by apply type
     */
    public List<ApplyNews> getByApplyType(String applyType) {
        return applyNewsRepository.getByApplyType(applyType);
    }

    /**
     * Get by SFCK status
     */
    public List<ApplyNews> getBySfck(Boolean sfck) {
        return applyNewsRepository.getBySfck(sfck);
    }

    /**
     * Get unchecked items
     */
    public List<ApplyNews> getUnchecked() {
        return applyNewsRepository.getUnchecked();
    }

    /**
     * Get checked items
     */
    public List<ApplyNews> getChecked() {
        return applyNewsRepository.getChecked();
    }

    /**
     * Search by keyword
     */
    public List<ApplyNews> search(String keyword) {
        if (keyword == null || keyword.trim().isEmpty()) {
            return applyNewsRepository.getAll();
        }
        return applyNewsRepository.search(keyword);
    }

    /**
     * Create apply news
     */
    @Transactional
    public Result<ApplyNews> createApplyNews(ApplyNews applyNews) {
        // Validate required fields
        if (applyNews.getId() == null || applyNews.getId().trim().isEmpty()) {
            return Result.error("申请新闻ID不能为空");
        }
        if (applyNews.getPsnName() == null || applyNews.getPsnName().trim().isEmpty()) {
            return Result.error("人员姓名不能为空");
        }

        // Set default values
        if (applyNews.getSfck() == null) {
            applyNews.setSfck(false);
        }

        applyNewsRepository.insertApplyNews(applyNews);
        return Result.success(applyNews);
    }

    /**
     * Update apply news
     */
    @Transactional
    public Result<ApplyNews> updateApplyNews(ApplyNews applyNews) {
        ApplyNews existing = getById(applyNews.getId());
        if (existing == null) {
            return Result.error("申请新闻不存在");
        }

        applyNewsRepository.updateApplyNews(applyNews);
        return Result.success(applyNews);
    }

    /**
     * Delete apply news
     */
    @Transactional
    public Result<Void> deleteApplyNews(String id) {
        ApplyNews existing = getById(id);
        if (existing == null) {
            return Result.error("申请新闻不存在");
        }

        applyNewsRepository.deleteApplyNews(id);
        return Result.success();
    }

    /**
     * Get total count
     */
    public int count() {
        return applyNewsRepository.count();
    }

    /**
     * Get count by SFCK status
     */
    public int countBySfck(Boolean sfck) {
        return applyNewsRepository.countBySfck(sfck);
    }
}
