package com.ryzggl.service;

import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.ryzggl.entity.CertificateHistory;
import com.ryzggl.entity.Certificate;
import com.ryzggl.repository.CertificateHistoryRepository;
import com.ryzggl.common.Result;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.List;

/**
 * CertificateHistory Service - Business Logic Layer
 * Maps to: CertificateHistoryDAL.cs logic and workflow management
 */
@Service
@Transactional
public class CertificateHistoryService extends ServiceImpl<CertificateHistoryRepository, CertificateHistory> {

    private static final Logger log = LoggerFactory.getLogger(CertificateHistoryService.class);

    @Autowired
    private CertificateService certificateService;

    /**
     * Create certificate history record
     * Maps to: CertificateHistoryDAL.AddCertificateHistory
     */
    public Result<Void> createCertificateHistory(CertificateHistory certificateHistory) {
        log.info("Creating certificate history for certificate: {}", certificateHistory.getCertificateId());

        // Validate certificate exists
        Certificate certificate = certificateService.getById(certificateHistory.getCertificateId());
        if (certificate == null) {
            log.error("Certificate not found: {}", certificateHistory.getCertificateId());
            return Result.error("证书不存在");
        }

        // Set change date to current date
        certificateHistory.setChangeDate(LocalDate.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd")));

        boolean success = save(certificateHistory);

        if (success) {
            log.info("Certificate history created successfully");
            return Result.success();
        } else {
            log.error("Failed to create certificate history");
            return Result.error("证书历史记录创建失败");
        }
    }

    /**
     * Get certificate history record by ID
     */
    public Result<CertificateHistory> getCertificateHistoryById(Long certificateHistoryId) {
        log.debug("Getting certificate history: {}", certificateHistoryId);
        CertificateHistory certificateHistory = getById(certificateHistoryId);

        if (certificateHistory == null) {
            return Result.error("证书历史记录不存在");
        }

        return Result.success(certificateHistory);
    }

    /**
     * List certificate history records
     * Maps to: CertificateHistoryDAL.GetCertificateHistoryList
     */
    public Result<List<CertificateHistory>> listCertificateHistory(Long certificateId, Long workerId, String changeType) {
        log.debug("Listing certificate history: certificateId={}, workerId={}, changeType={}", certificateId, workerId, changeType);

        com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<CertificateHistory> queryWrapper =
                new com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<>();

        if (certificateId != null) {
            queryWrapper.eq(CertificateHistory::getCertificateId, certificateId);
        }
        if (workerId != null) {
            queryWrapper.eq(CertificateHistory::getWorkerId, workerId);
        }
        if (changeType != null) {
            queryWrapper.eq(CertificateHistory::getChangeType, changeType);
        }

        queryWrapper.orderByDesc(CertificateHistory::getChangeDate);

        List<CertificateHistory> certificateHistories = list(queryWrapper);
        return Result.success(certificateHistories);
    }

    /**
     * Get certificate history by certificate
     */
    public Result<List<CertificateHistory>> getCertificateHistoryByCertificate(Long certificateId) {
        log.debug("Getting certificate history for certificate: {}", certificateId);

        List<CertificateHistory> certificateHistories = baseMapper.findByCertificateId(certificateId);

        return Result.success(certificateHistories);
    }

    /**
     * Get certificate history by worker
     */
    public Result<List<CertificateHistory>> getCertificateHistoryByWorker(Long workerId) {
        log.debug("Getting certificate history for worker: {}", workerId);

        List<CertificateHistory> certificateHistories = baseMapper.findByWorkerId(workerId);

        return Result.success(certificateHistories);
    }

    /**
     * Get certificate history by change type
     */
    public Result<List<CertificateHistory>> getCertificateHistoryByChangeType(String changeType) {
        log.debug("Getting certificate history by change type: {}", changeType);

        List<CertificateHistory> certificateHistories = baseMapper.findByChangeType(changeType);

        return Result.success(certificateHistories);
    }

    /**
     * Get recent certificate history records
     */
    public Result<List<CertificateHistory>> getRecentCertificateHistory(int days) {
        log.debug("Getting recent certificate history (last {} days)", days);

        List<CertificateHistory> certificateHistories = baseMapper.findRecent(days);

        return Result.success(certificateHistories);
    }

    /**
     * Auto-create history record for certificate change
     * Called internally by other services when certificate is modified
     */
    public void autoCreateHistory(Long certificateId, String changeType, String changeDescription, String changeBefore, String changeAfter) {
        log.info("Auto-creating history for certificate: {}, type: {}", certificateId, changeType);

        Certificate certificate = certificateService.getById(certificateId);
        if (certificate == null) {
            log.error("Certificate not found for history: {}", certificateId);
            return;
        }

        CertificateHistory history = new CertificateHistory();
        history.setCertificateId(certificateId);
        history.setCertificateCode(certificate.getCertificateCode());
        history.setWorkerId(certificate.getWorkerId());
        history.setWorkerName(certificate.getWorkerName());
        history.setUnitCode(certificate.getUnitCode());
        history.setChangeType(changeType);
        history.setChangeDescription(changeDescription);
        history.setChangeBefore(changeBefore);
        history.setChangeAfter(changeAfter);
        history.setChangeDate(LocalDate.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd")));

        save(history);
    }
}
