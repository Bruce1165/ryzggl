package com.ryzggl.service;

import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.ryzggl.entity.CertificateEnterApply;
import com.ryzggl.entity.Certificate;
import com.ryzggl.entity.Worker;
import com.ryzggl.repository.CertificateEnterApplyRepository;
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
 * CertificateEnterApply Service - Business Logic Layer
 * Maps to: CertificateEnterApplyDAL.cs logic and workflow management
 */
@Service
@Transactional
public class CertificateEnterApplyService extends ServiceImpl<CertificateEnterApplyRepository, CertificateEnterApply> {

    private static final Logger log = LoggerFactory.getLogger(CertificateEnterApplyService.class);

    @Autowired
    private CertificateService certificateService;

    @Autowired
    private WorkerService workerService;

    /**
     * Create new certificate application
     * Maps to: CertificateEnterApplyDAL.AddCertificateEnterApply
     */
    public Result<Void> createCertificateEnterApply(CertificateEnterApply certificateEnterApply) {
        log.info("Creating new certificate application for worker: {}", certificateEnterApply.getWorkerId());

        // Validate worker exists
        Worker worker = workerService.getById(certificateEnterApply.getWorkerId());
        if (worker == null) {
            log.error("Worker not found: {}", certificateEnterApply.getWorkerId());
            return Result.error("人员不存在");
        }

        // Set initial status
        certificateEnterApply.setApplyStatus("未填写");
        certificateEnterApply.setWorkerName(worker.getWorkerName());

        boolean success = save(certificateEnterApply);

        if (success) {
            log.info("New certificate application created successfully");
            return Result.success();
        } else {
            log.error("Failed to create new certificate application");
            return Result.error("新证书申请创建失败");
        }
    }

    /**
     * Get new certificate application by ID
     */
    public Result<CertificateEnterApply> getCertificateEnterApplyById(Long certificateEnterApplyId) {
        log.debug("Getting new certificate application: {}", certificateEnterApplyId);
        CertificateEnterApply certificateEnterApply = getById(certificateEnterApplyId);

        if (certificateEnterApply == null) {
            return Result.error("新证书申请不存在");
        }

        return Result.success(certificateEnterApply);
    }

    /**
     * List new certificate applications with pagination
     * Maps to: CertificateEnterApplyDAL.GetCertificateEnterApplyList
     */
    public Result<List<CertificateEnterApply>> listCertificateEnterApplys(Long workerId, String status) {
        log.debug("Listing new certificate applications: workerId={}, status={}", workerId, status);

        com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<CertificateEnterApply> queryWrapper =
                new com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<>();

        if (workerId != null) {
            queryWrapper.eq(CertificateEnterApply::getWorkerId, workerId);
        }
        if (status != null) {
            queryWrapper.eq(CertificateEnterApply::getApplyStatus, status);
        }

        queryWrapper.orderByDesc(CertificateEnterApply::getCreateTime);

        List<CertificateEnterApply> certificateEnterApplys = list(queryWrapper);
        return Result.success(certificateEnterApplys);
    }

    /**
     * Submit new certificate application for review
     * Maps to: CertificateEnterApplyDAL.SubmitCertificateEnterApply
     */
    public Result<Void> submitCertificateEnterApply(Long certificateEnterApplyId) {
        log.info("Submitting new certificate application: {}", certificateEnterApplyId);

        CertificateEnterApply certificateEnterApply = getById(certificateEnterApplyId);
        if (certificateEnterApply == null) {
            return Result.error("新证书申请不存在");
        }

        if (!"未填写".equals(certificateEnterApply.getApplyStatus())) {
            return Result.error("只有未填写的申请才能提交");
        }

        certificateEnterApply.setApplyStatus("待确认");
        boolean success = updateById(certificateEnterApply);

        if (success) {
            log.info("New certificate application submitted successfully");
            return Result.success();
        } else {
            return Result.error("新证书申请提交失败");
        }
    }

    /**
     * Approve new certificate application
     * Maps to: CertificateEnterApplyDAL.ApproveCertificateEnterApply
     */
    public Result<Void> approveCertificateEnterApply(Long certificateEnterApplyId, String checkMan, String checkAdvise) {
        log.info("Approving new certificate application: {}, checkMan: {}, advise: {}", certificateEnterApplyId, checkMan, checkAdvise);

        CertificateEnterApply certificateEnterApply = getById(certificateEnterApplyId);
        if (certificateEnterApply == null) {
            return Result.error("新证书申请不存在");
        }

        if (!"待确认".equals(certificateEnterApply.getApplyStatus())) {
            return Result.error("只有待确认的申请才能通过");
        }

        // Create new certificate
        Certificate certificate = new Certificate();
        certificate.setWorkerId(certificateEnterApply.getWorkerId());
        certificate.setWorkerName(certificateEnterApply.getWorkerName());
        certificate.setUnitCode(certificateEnterApply.getUnitCode());
        certificate.setPostTypeId(certificateEnterApply.getPostTypeId());
        certificate.setPostId(certificateEnterApply.getPostId());
        certificate.setCertificateType(certificateEnterApply.getCertificateType());
        certificate.setCertificateCode(certificateEnterApply.getCertificateCode());
        certificate.setValidStartDate(certificateEnterApply.getValidStartDate());
        certificate.setValidEndDate(certificateEnterApply.getValidEndDate());
        certificate.setConferDate(LocalDate.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd")));
        certificate.setStatus("有效");

        boolean saveResult = certificateService.save(certificate);

        if (saveResult) {
            log.info("New certificate created successfully: {}", certificate.getId());
        } else {
            log.error("Failed to create new certificate");
            return Result.error("证书创建失败");
        }

        // Update application status
        certificateEnterApply.setApplyStatus("已受理");
        certificateEnterApply.setCheckMan(checkMan);
        certificateEnterApply.setCheckAdvise(checkAdvise);
        certificateEnterApply.setCheckDate(LocalDate.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd")));
        boolean success = updateById(certificateEnterApply);

        if (success) {
            log.info("New certificate application approved successfully");
            return Result.success();
        } else {
            return Result.error("新证书申请审核失败");
        }
    }

    /**
     * Reject new certificate application
     * Maps to: CertificateEnterApplyDAL.RejectCertificateEnterApply
     */
    public Result<Void> rejectCertificateEnterApply(Long certificateEnterApplyId, String rejectionReason) {
        log.info("Rejecting new certificate application: {}, reason: {}", certificateEnterApplyId, rejectionReason);

        CertificateEnterApply certificateEnterApply = getById(certificateEnterApplyId);
        if (certificateEnterApply == null) {
            return Result.error("新证书申请不存在");
        }

        if (!"待确认".equals(certificateEnterApply.getApplyStatus())) {
            return Result.error("只有待确认的申请才能驳回");
        }

        certificateEnterApply.setApplyStatus("已驳回");
        certificateEnterApply.setCheckAdvise(rejectionReason);
        boolean success = updateById(certificateEnterApply);

        if (success) {
            log.info("New certificate application rejected successfully");
            return Result.success();
        } else {
            return Result.error("新证书申请驳回失败");
        }
    }

    /**
     * Get pending new certificate applications for approval
     * Maps to: CertificateEnterApplyDAL.GetPendingCertificateEnterApplys
     */
    public Result<List<CertificateEnterApply>> getPendingCertificateEnterApplys() {
        log.debug("Getting pending new certificate applications");

        List<CertificateEnterApply> certificateEnterApplys = lambdaQuery()
                .eq(CertificateEnterApply::getApplyStatus, "待确认")
                .orderByAsc(CertificateEnterApply::getCreateTime)
                .list();

        return Result.success(certificateEnterApplys);
    }

    /**
     * Get new certificate applications by status
     */
    public Result<List<CertificateEnterApply>> getCertificateEnterApplysByStatus(String status) {
        log.debug("Getting new certificate applications by status: {}", status);

        List<CertificateEnterApply> certificateEnterApplys = lambdaQuery()
                .eq(CertificateEnterApply::getApplyStatus, status)
                .orderByDesc(CertificateEnterApply::getCreateTime)
                .list();

        return Result.success(certificateEnterApplys);
    }
}
