package com.ryzggl.service;

import com.baomidou.mybatisplus.extension.service.IService;
import com.ryzggl.entity.CertificateAddItem;
import com.ryzggl.repository.CertificateAddItemRepository;
import com.ryzggl.common.Result;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;

/**
 * CertificateAddItem Service
 * 证书增项业务逻辑层
 */
@Service
public class CertificateAddItemService implements IService<CertificateAddItem> {

    @Autowired
    private CertificateAddItemRepository certificateAddItemRepository;

    /**
     * Get by ID
     */
    public CertificateAddItem getById(Long certificateAddItemId) {
        return certificateAddItemRepository.getById(certificateAddItemId);
    }

    /**
     * Get all certificate add items
     */
    public List<CertificateAddItem> getAll() {
        return certificateAddItemRepository.getAll();
    }

    /**
     * Get by certificate ID
     */
    public List<CertificateAddItem> getByCertificateId(Long certificateId) {
        return certificateAddItemRepository.getByCertificateId(certificateId);
    }

    /**
     * Get by post type ID
     */
    public List<CertificateAddItem> getByPostTypeId(Integer postTypeId) {
        return certificateAddItemRepository.getByPostTypeId(postTypeId);
    }

    /**
     * Get by post ID
     */
    public List<CertificateAddItem> getByPostId(Integer postId) {
        return certificateAddItemRepository.getByPostId(postId);
    }

    /**
     * Get by case status
     */
    public List<CertificateAddItem> getByCaseStatus(String caseStatus) {
        return certificateAddItemRepository.getByCaseStatus(caseStatus);
    }

    /**
     * Get add item names by certificate ID
     */
    public List<String> getAddItemNames(Long certificateId) {
        return certificateAddItemRepository.getAddItemNames(certificateId);
    }

    /**
     * Get add item names string (comma-separated)
     */
    public String getAddItemNameString(Long certificateId) {
        String nameString = certificateAddItemRepository.getAddItemNameString(certificateId);
        return nameString != null ? nameString : "";
    }

    /**
     * Create certificate add item
     */
    @Transactional
    public Result<CertificateAddItem> createCertificateAddItem(CertificateAddItem certificateAddItem) {
        // Validate required fields
        if (certificateAddItem.getCertificateId() == null) {
            return Result.error("证书ID不能为空");
        }
        if (certificateAddItem.getPostTypeId() == null) {
            return Result.error("岗位类型ID不能为空");
        }

        // Set default values
        if (certificateAddItem.getCaseStatus() == null) {
            certificateAddItem.setCaseStatus("未审核");
        }

        certificateAddItemRepository.insertCertificateAddItem(certificateAddItem);
        return Result.success(certificateAddItem);
    }

    /**
     * Update certificate add item
     */
    @Transactional
    public Result<CertificateAddItem> updateCertificateAddItem(CertificateAddItem certificateAddItem) {
        CertificateAddItem existing = getById(certificateAddItem.getCertificateAddItemId());
        if (existing == null) {
            return Result.error("证书增项不存在");
        }

        certificateAddItemRepository.updateCertificateAddItem(certificateAddItem);
        return Result.success(certificateAddItem);
    }

    /**
     * Delete certificate add item
     */
    @Transactional
    public Result<Void> deleteCertificateAddItem(Long certificateAddItemId) {
        CertificateAddItem existing = getById(certificateAddItemId);
        if (existing == null) {
            return Result.error("证书增项不存在");
        }

        certificateAddItemRepository.deleteCertificateAddItem(certificateAddItemId);
        return Result.success();
    }

    /**
     * Delete by certificate ID
     */
    @Transactional
    public Result<Void> deleteByCertificateId(Long certificateId) {
        int count = certificateAddItemRepository.deleteByCertificateId(certificateId);
        return Result.success();
    }

    /**
     * Get total count
     */
    public int count() {
        return certificateAddItemRepository.count();
    }

    /**
     * Get count by certificate ID
     */
    public int countByCertificateId(Long certificateId) {
        return certificateAddItemRepository.countByCertificateId(certificateId);
    }

    /**
     * Get count by case status
     */
    public int countByCaseStatus(String caseStatus) {
        return certificateAddItemRepository.countByCaseStatus(caseStatus);
    }
}
