package com.ryzggl.service;

import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.ryzggl.common.Result;
import com.ryzggl.entity.Certificate;
import com.ryzggl.repository.CertificateRepository;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.Date;

/**
 * Certificate Service
 * Certificate lifecycle management
 */
@Service
public class CertificateService extends ServiceImpl<CertificateRepository, Certificate> {

    private final CertificateRepository certificateRepository;
    private final CertificateValidityCalculator validityCalculator;

    public CertificateService(CertificateRepository certificateRepository, CertificateValidityCalculator validityCalculator) {
        this.certificateRepository = certificateRepository;
        this.validityCalculator = validityCalculator;
    }

    /**
     * Query certificate list with pagination
     */
    public Result<IPage<Certificate>> getCertificateList(Integer current, Integer size,
                                                       String certNo, String holderName,
                                                       String qualificationName, String status) {
        Page<Certificate> page = new Page<>(current, size);
        IPage<Certificate> result = certificateRepository.selectCertificatePage(page, certNo, holderName, qualificationName, status);
        return Result.success(result);
    }

    /**
     * Query certificate by ID
     */
    public Result<Certificate> getCertificateById(Long id) {
        Certificate certificate = certificateRepository.selectById(id);
        if (certificate == null) {
            return Result.error("证书不存在");
        }
        return Result.success(certificate);
    }

    /**
     * Create certificate
     */
    @Transactional
    public Result<Certificate> createCertificate(Certificate certificate) {
        // Generate certificate number
        String certNo = generateCertNo();
        certificate.setCertNo(certNo);

        // Set initial status
        certificate.setStatus("有效");

        certificate.setCreateBy("system");
        certificate.setUpdateBy("system");

        // Set issue date if not provided
        if (certificate.getIssueDate() == null || certificate.getIssueDate().isEmpty()) {
            certificate.setIssueDate(LocalDate.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd")));
        }

        // Set valid end date if not provided (default 5 years)
        if ((certificate.getValidEnd() == null || certificate.getValidEnd().isEmpty()) &&
            certificate.getValidStart() != null && !certificate.getValidStart().isEmpty()) {
            LocalDate startDate = LocalDate.parse(certificate.getValidStart(), DateTimeFormatter.ofPattern("yyyy-MM-dd"));
            LocalDate endDate = startDate.plusYears(5);
            certificate.setValidEnd(endDate.format(DateTimeFormatter.ofPattern("yyyy-MM-dd")));
        }

        save(certificate);
        return Result.success("证书创建成功", certificate);
    }

    /**
     * Update certificate
     */
    @Transactional
    public Result<Void> updateCertificate(Certificate certificate) {
        Certificate existCert = certificateRepository.selectById(certificate.getId());
        if (existCert == null) {
            return Result.error("证书不存在");
        }

        updateById(certificate);
        return Result.success("证书更新成功");
    }

    /**
     * Pause certificate
     */
    @Transactional
    public Result<Void> pauseCertificate(Long id, String pauseReason) {
        Certificate certificate = certificateRepository.selectById(id);
        if (certificate == null) {
            return Result.error("证书不存在");
        }

        if (!"有效".equals(certificate.getStatus())) {
            return Result.error("只有有效的证书才能暂停");
        }

        certificate.setStatus("暂停");
        certificate.setPauseReason(pauseReason);
        updateById(certificate);
        return Result.success("证书已暂停");
    }

    /**
     * Resume certificate
     */
    @Transactional
    public Result<Void> resumeCertificate(Long id) {
        Certificate certificate = certificateRepository.selectById(id);
        if (certificate == null) {
            return Result.error("证书不存在");
        }

        if (!"暂停".equals(certificate.getStatus())) {
            return Result.error("只有暂停的证书才能恢复");
        }

        certificate.setStatus("有效");
        certificate.setPauseReason(null);
        updateById(certificate);
        return Result.success("证书已恢复");
    }

    /**
     * Revoke certificate
     */
    @Transactional
    public Result<Void> revokeCertificate(Long id, String revokeReason) {
        Certificate certificate = certificateRepository.selectById(id);
        if (certificate == null) {
            return Result.error("证书不存在");
        }

        if ("注销".equals(certificate.getStatus())) {
            return Result.error("证书已注销");
        }

        certificate.setStatus("注销");
        certificate.setRevokeReason(revokeReason);
        updateById(certificate);
        return Result.success("证书已注销");
    }

    /**
     * Delete certificate
     */
    @Transactional
    public Result<Void> deleteCertificate(Long id) {
        Certificate certificate = certificateRepository.selectById(id);
        if (certificate == null) {
            return Result.error("证书不存在");
        }

        removeById(id);
        return Result.success("证书已删除");
    }

    /**
     * Generate certificate number
     */
    private String generateCertNo() {
        return "CERT" + System.currentTimeMillis();
    }

    /**
     * Calculate certificate validity end date for continuation
     * Uses CertificateValidityCalculator to implement complex business rules
     *
     * @param postTypeID Post type (1=安管人员, 2=特种作业)
     * @param postID Post ID
     * @param sex Gender (男/女)
     * @param birthday Worker's birthday
     * @param validEndDate Current certificate valid end date
     * @param unitCode Unit code
     * @param workerName Worker name (for region check)
     * @return Calculated new valid end date
     */
    public Result<Date> calculateContinueValidEndDate(Integer postTypeID, Integer postID, String sex,
                                                   Date birthday, Date validEndDate,
                                                   String unitCode, String workerName) {
        try {
            Date newValidEndDate = validityCalculator.calculateContinueValidEndDate(
                    postTypeID, postID, sex, birthday, validEndDate, unitCode, workerName);

            if (newValidEndDate == null) {
                return Result.error("证书有效期计算失败");
            }

            return Result.success(newValidEndDate);
        } catch (Exception e) {
            return Result.error("证书有效期计算错误: " + e.getMessage());
        }
    }
}
