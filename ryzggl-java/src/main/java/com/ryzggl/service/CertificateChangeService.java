package com.ryzggl.service;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.ryzggl.common.Result;
import com.ryzggl.entity.CertificateChange;
import com.ryzggl.repository.CertificateChangeRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;

/**
 * CertificateChange Service - 证书变更业务逻辑层
 */
@Service
public class CertificateChangeService extends ServiceImpl<CertificateChangeRepository, CertificateChange> {

    @Autowired
    private CertificateChangeRepository certificateChangeRepository;

    /**
     * Create new certificate change
     */
    @Transactional
    public Result<CertificateChange> createCertificateChange(CertificateChange certificateChange) {
        try {
            certificateChangeRepository.insert(certificateChange);
            return Result.success(certificateChange);
        } catch (Exception e) {
            return Result.error("创建证书变更失败: " + e.getMessage());
        }
    }

    /**
     * Update certificate change
     */
    @Transactional
    public Result<CertificateChange> updateCertificateChange(CertificateChange certificateChange) {
        try {
            certificateChangeRepository.updateById(certificateChange);
            return Result.success(certificateChange);
        } catch (Exception e) {
            return Result.error("更新证书变更失败: " + e.getMessage());
        }
    }

    /**
     * Delete certificate change (soft delete)
     */
    @Transactional
    public Result<Boolean> deleteCertificateChange(Long id) {
        try {
            CertificateChange certificateChange = certificateChangeRepository.selectById(id);
            certificateChange.setDeleted(1);
            certificateChangeRepository.updateById(certificateChange);
            return Result.success(true);
        } catch (Exception e) {
            return Result.error("删除证书变更失败: " + e.getMessage());
        }
    }

    /**
     * Query certificate change by ID
     */
    public Result<CertificateChange> getCertificateChangeById(Long id) {
        CertificateChange certificateChange = certificateChangeRepository.selectById(id);
        if (certificateChange == null) {
            return Result.error("证书变更不存在");
        }
        return Result.success(certificateChange);
    }

    /**
     * Query certificate change list with pagination
     */
    public Result<IPage<CertificateChange>> getCertificateChangePage(Integer current, Integer size,
                                                                   Long certificateId,
                                                                   String certificateCode,
                                                                   String changeType) {
        try {
            Page<CertificateChange> page = new Page<>(current, size);
            IPage<CertificateChange> result = certificateChangeRepository.selectCertificateChangePage(
                page, certificateId, certificateCode, changeType);
            return Result.success(result);
        } catch (Exception e) {
            return Result.error("查询证书变更列表失败: " + e.getMessage());
        }
    }

    /**
     * Query certificate changes by certificate ID
     */
    public Result<List<CertificateChange>> getCertificateChangesByCertificateId(Long certificateId) {
        try {
            List<CertificateChange> list = certificateChangeRepository.selectByCertificateId(certificateId);
            return Result.success(list);
        } catch (Exception e) {
            return Result.error("查询证书变更历史失败: " + e.getMessage());
        }
    }

    /**
     * Query certificate changes by certificate code
     */
    public Result<List<CertificateChange>> getCertificateChangesByCode(String certificateCode) {
        try {
            List<CertificateChange> list = certificateChangeRepository.selectByCertificateCode(certificateCode);
            return Result.success(list);
        } catch (Exception e) {
            return Result.error("查询证书变更历史失败: " + e.getMessage());
        }
    }

    /**
     * Query certificate changes by date range
     */
    public Result<List<CertificateChange>> getCertificateChangesByDateRange(String startDate, String endDate) {
        try {
            List<CertificateChange> list = certificateChangeRepository.selectByDateRange(startDate, endDate);
            return Result.success(list);
        } catch (Exception e) {
            return Result.error("查询日期范围内证书变更失败: " + e.getMessage());
        }
    }

    /**
     * Get certificate change statistics by type
     */
    public Result<java.util.List<java.util.Map<String, Object>>> getStatisticsByType() {
        try {
            List<java.util.Map<String, Object>> stats = certificateChangeRepository.getStatisticsByType();
            return Result.success(stats);
        } catch (Exception e) {
            return Result.error("查询证书变更统计失败: " + e.getMessage());
        }
    }
}
