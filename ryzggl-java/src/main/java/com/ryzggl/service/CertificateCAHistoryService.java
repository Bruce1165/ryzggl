package com.ryzggl.service;

import com.baomidou.mybatisplus.extension.service.IService;
import com.ryzggl.entity.CertificateCAHistory;
import com.ryzggl.repository.CertificateCAHistoryRepository;
import com.ryzggl.common.Result;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;

/**
 * CertificateCAHistory Service
 * 证书CA历史业务逻辑层
 */
@Service
public class CertificateCAHistoryService implements IService<CertificateCAHistory> {

    @Autowired
    private CertificateCAHistoryRepository certificateCAHistoryRepository;

    /**
     * Get by ID
     */
    public CertificateCAHistory getById(String certificateCaId) {
        return certificateCAHistoryRepository.getById(certificateCaId);
    }

    /**
     * Get all CA history records
     */
    public List<CertificateCAHistory> getAll() {
        return certificateCAHistoryRepository.getAll();
    }

    /**
     * Get by certificate ID
     */
    public List<CertificateCAHistory> getByCertificateId(Long certificateId) {
        return certificateCAHistoryRepository.getByCertificateId(certificateId);
    }

    /**
     * Search by keyword
     */
    public List<CertificateCAHistory> search(String keyword) {
        if (keyword == null || keyword.trim().isEmpty()) {
            return certificateCAHistoryRepository.getAll();
        }
        return certificateCAHistoryRepository.search(keyword);
    }

    /**
     * Create CA history
     */
    @Transactional
    public Result<CertificateCAHistory> createCertificateCAHistory(CertificateCAHistory certificateCAHistory) {
        // Validate required fields
        if (certificateCAHistory.getCertificateCaId() == null || certificateCAHistory.getCertificateCaId().trim().isEmpty()) {
            return Result.error("证书CA历史ID不能为空");
        }

        certificateCAHistoryRepository.insertCertificateCAHistory(certificateCAHistory);
        return Result.success(certificateCAHistory);
    }

    /**
     * Update CA history
     */
    @Transactional
    public Result<CertificateCAHistory> updateCertificateCAHistory(CertificateCAHistory certificateCAHistory) {
        CertificateCAHistory existing = getById(certificateCAHistory.getCertificateCaId());
        if (existing == null) {
            return Result.error("证书CA历史不存在");
        }

        certificateCAHistoryRepository.updateCertificateCAHistory(certificateCAHistory);
        return Result.success(certificateCAHistory);
    }

    /**
     * Delete CA history
     */
    @Transactional
    public Result<Void> deleteCertificateCAHistory(String certificateCaId) {
        CertificateCAHistory existing = getById(certificateCaId);
        if (existing == null) {
            return Result.error("证书CA历史不存在");
        }

        certificateCAHistoryRepository.deleteCertificateCAHistory(certificateCaId);
        return Result.success();
    }

    /**
     * Delete by certificate ID
     */
    @Transactional
    public Result<Void> deleteByCertificateId(Long certificateId) {
        int count = certificateCAHistoryRepository.deleteByCertificateId(certificateId);
        return Result.success();
    }

    /**
     * Get total count
     */
    public int count() {
        return certificateCAHistoryRepository.count();
    }

    /**
     * Get count by certificate ID
     */
    public int countByCertificateId(Long certificateId) {
        return certificateCAHistoryRepository.countByCertificateId(certificateId);
    }
}
