package com.ryzggl.service;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.ryzggl.common.Result;
import com.ryzggl.entity.CertificateContinue;
import com.ryzggl.repository.CertificateContinueRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;

/**
 * CertificateContinue Service - 证书延续业务逻辑层
 */
@Service
public class CertificateContinueService extends ServiceImpl<CertificateContinueRepository, CertificateContinue> {

    @Autowired
    private CertificateContinueRepository certificateContinueRepository;

    /**
     * Create new certificate continue
     */
    @Transactional
    public Result<CertificateContinue> createCertificateContinue(CertificateContinue certificateContinue) {
        try {
            certificateContinueRepository.insert(certificateContinue);
            return Result.success(certificateContinue);
        } catch (Exception e) {
            return Result.error("创建证书延续失败: " + e.getMessage());
        }
    }

    /**
     * Update certificate continue
     */
    @Transactional
    public Result<CertificateContinue> updateCertificateContinue(CertificateContinue certificateContinue) {
        try {
            certificateContinueRepository.updateById(certificateContinue);
            return Result.success(certificateContinue);
        } catch (Exception e) {
            return Result.error("更新证书延续失败: " + e.getMessage());
        }
    }

    /**
     * Delete certificate continue (soft delete)
     */
    @Transactional
    public Result<Boolean> deleteCertificateContinue(Long id) {
        try {
            CertificateContinue certificateContinue = certificateContinueRepository.selectById(id);
            certificateContinue.setDeleted(1);
            certificateContinueRepository.updateById(certificateContinue);
            return Result.success(true);
        } catch (Exception e) {
            return Result.error("删除证书延续失败: " + e.getMessage());
        }
    }

    /**
     * Query certificate continue by ID
     */
    public Result<CertificateContinue> getCertificateContinueById(Long id) {
        CertificateContinue certificateContinue = certificateContinueRepository.selectById(id);
        if (certificateContinue == null) {
            return Result.error("证书延续不存在");
        }
        return Result.success(certificateContinue);
    }

    /**
     * Query certificate continue list with pagination
     */
    public Result<IPage<CertificateContinue>> getCertificateContinuePage(Integer current, Integer size,
                                                                     Long certificateId,
                                                                     String certificateCode,
                                                                     String continueStatus,
                                                                     String continueType) {
        try {
            Page<CertificateContinue> page = new Page<>(current, size);
            IPage<CertificateContinue> result = certificateContinueRepository.selectCertificateContinuePage(
                page, certificateId, certificateCode, continueStatus, continueType);
            return Result.success(result);
        } catch (Exception e) {
            return Result.error("查询证书延续列表失败: " + e.getMessage());
        }
    }

    /**
     * Query certificate continues by certificate ID
     */
    public Result<List<CertificateContinue>> getCertificateContinuesById(Long certificateId) {
        try {
            List<CertificateContinue> list = certificateContinueRepository.selectByCertificateId(certificateId);
            return Result.success(list);
        } catch (Exception e) {
            return Result.error("查询证书延续历史失败: " + e.getMessage());
        }
    }

    /**
     * Query certificate continues by certificate code
     */
    public Result<List<CertificateContinue>> getCertificateContinuesByCode(String certificateCode) {
        try {
            List<CertificateContinue> list = certificateContinueRepository.selectByCertificateCode(certificateCode);
            return Result.success(list);
        } catch (Exception e) {
            return Result.error("查询证书延续历史失败: " + e.getMessage());
        }
    }

    /**
     * Query certificate continues by status
     */
    public Result<List<CertificateContinue>> getCertificateContinuesByStatus(String status) {
        try {
            List<CertificateContinue> list = certificateContinueRepository.selectByStatus(status);
            return Result.success(list);
        } catch (Exception e) {
            return Result.error("查询证书延续状态失败: " + e.getMessage());
        }
    }

    /**
     * Query expiring certificate continues
     */
    public Result<List<CertificateContinue>> getExpiringSoon() {
        try {
            List<CertificateContinue> list = certificateContinueRepository.selectExpiringSoon();
            return Result.success(list);
        } catch (Exception e) {
            return Result.error("查询即将过期证书延续失败: " + e.getMessage());
        }
    }

    /**
     * Get certificate continue statistics
     */
    public Result<java.util.List<java.util.Map<String, Object>>> getStatisticsByType() {
        try {
            List<java.util.Map<String, Object>> stats = certificateContinueRepository.getStatisticsByType();
            return Result.success(stats);
        } catch (Exception e) {
            return Result.error("查询证书延续统计失败: " + e.getMessage());
        }
    }

    /**
     * Process certificate continue - mark as completed
     */
    @Transactional
    public Result<Boolean> processCertificateContinue(Long id) {
        try {
            CertificateContinue certificateContinue = certificateContinueRepository.selectById(id);
            if (certificateContinue == null) {
                return Result.error("证书延续不存在");
            }

            certificateContinue.setContinueStatus("已延续");
            certificateContinueRepository.updateById(certificateContinue);
            return Result.success(true);
        } catch (Exception e) {
            return Result.error("处理证书延续失败: " + e.getMessage());
        }
    }
}
