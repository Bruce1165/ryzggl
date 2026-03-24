package com.ryzggl.controller;

import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.common.Result;
import com.ryzggl.entity.CertificateLock;
import com.ryzggl.service.CertificateLockService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.media.Schema;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.validation.annotation.Validated;
import org.springframework.web.bind.annotation.*;

import jakarta.validation.Valid;
import java.util.List;

/**
 * CertificateLock Controller - REST API Endpoints
 * Replaces .NET WebForms pages: zjsCertLockList.aspx, zjsCertLockChange.aspx
 */
@RestController
@RequestMapping("/api/certificate-lock")
@Tag(name = "Certificate Lock Management", description = "Certificate lock/unlock workflows for administrative control")
public class CertificateLockController {

    private static final Logger log = LoggerFactory.getLogger(CertificateLockController.class);

    @Autowired
    private CertificateLockService certificateLockService;

    /**
     * Get certificate lock list with pagination
     * Replaces: zjsCertLockList.aspx with Telerik RadGrid
     */
    @Operation(summary = "Get certificate lock list", description = "List certificate lock records with pagination and filtering")
    @GetMapping("/list")
    public Result<Page<CertificateLock>> getCertificateLockList(
            @RequestParam(defaultValue = "1") Integer current,
            @RequestParam(required = false) Long certificateId,
            @RequestParam(required = false) Long workerId,
            @RequestParam(required = false) String unitCode,
            @RequestParam(required = false) String lockStatus,
            @RequestParam(required = false) String lockType,
            @RequestParam(required = false) String keyword) {
        log.info("Getting certificate locks: page={}, certificateId={}, workerId={}, unitCode={}, lockStatus={}, lockType={}, keyword={}",
                current, certificateId, workerId, unitCode, lockStatus, lockType, keyword);

        com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<CertificateLock> queryWrapper =
                new com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<>();

        if (certificateId != null) {
            queryWrapper.eq(CertificateLock::getCertificateId, certificateId);
        }
        if (workerId != null) {
            queryWrapper.eq(CertificateLock::getWorkerId, workerId);
        }
        if (unitCode != null) {
            queryWrapper.eq(CertificateLock::getUnitCode, unitCode);
        }
        if (lockStatus != null) {
            queryWrapper.eq(CertificateLock::getLockStatus, lockStatus);
        }
        if (lockType != null) {
            queryWrapper.eq(CertificateLock::getLockType, lockType);
        }
        if (keyword != null && !keyword.isEmpty()) {
            queryWrapper.and(wrapper -> wrapper
                    .like(CertificateLock::getCertificateCode, keyword)
                    .or()
                    .like(CertificateLock::getWorkerName, keyword)
                    .or()
                    .like(CertificateLock::getLockReason, keyword));
        }

        Page<CertificateLock> page = certificateLockService.page(new Page<>(current, 10), queryWrapper);

        return Result.success(page);
    }

    /**
     * Create certificate lock record
     * Replaces: zjsCertLockFirst.aspx
     */
    @Operation(summary = "Create certificate lock", description = "Create a new certificate lock record")
    @PostMapping
    public Result<Void> createCertificateLock(@Valid @RequestBody CertificateLock certificateLock) {
        log.info("Creating certificate lock for certificate: {}", certificateLock.getCertificateId());
        return certificateLockService.createCertificateLock(certificateLock);
    }

    /**
     * Lock certificate
     */
    @Operation(summary = "Lock certificate", description = "Lock a certificate for administrative control")
    @PostMapping("/lock")
    public Result<Void> lockCertificate(@Valid @RequestBody LockRequest request) {
        log.info("Locking certificate: {}", request.getCertificateId());
        return certificateLockService.lockCertificate(
                request.getCertificateId(),
                request.getLockType(),
                request.getLockReason(),
                request.getLockMan()
        );
    }

    /**
     * Unlock certificate
     */
    @Operation(summary = "Unlock certificate", description = "Unlock a locked certificate")
    @PutMapping("/{id}/unlock")
    public Result<Void> unlockCertificate(
            @PathVariable Long id,
            @Validated @RequestBody UnlockRequest request) {
        log.info("Unlocking certificate lock: {}", id);
        return certificateLockService.unlockCertificate(id, request.getUnlockReason(), request.getUnlockMan());
    }

    /**
     * Get certificate lock by ID
     * Replaces: zjsCertLockDetail.aspx
     */
    @Operation(summary = "Get certificate lock by ID", description = "Get detailed certificate lock information")
    @GetMapping("/{id}")
    public Result<CertificateLock> getCertificateLockById(@PathVariable Long id) {
        log.debug("Getting certificate lock: {}", id);
        return certificateLockService.getCertificateLockById(id);
    }

    /**
     * Get currently locked certificates
     * Replaces: CheckTask.GetLockedCertificates
     */
    @Operation(summary = "Get locked certificates", description = "Get all currently locked certificates")
    @GetMapping("/locked")
    public Result<List<CertificateLock>> getLockedCertificates() {
        log.debug("Getting currently locked certificates");
        return certificateLockService.getLockedCertificates();
    }

    /**
     * Get certificate locks by status
     * Replaces: zjsCertLockList.aspx (filter by LOCKSTATUS)
     */
    @Operation(summary = "Get certificate locks by status", description = "Filter certificate locks by status")
    @GetMapping("/status/{lockStatus}")
    public Result<List<CertificateLock>> getLocksByStatus(@PathVariable String lockStatus) {
        log.debug("Getting certificate locks by status: {}", lockStatus);
        return certificateLockService.getCertificateLocksByStatus(lockStatus);
    }

    /**
     * Get certificate locks by certificate
     */
    @Operation(summary = "Get certificate locks by certificate", description = "Get all lock records for a specific certificate")
    @GetMapping("/certificate/{certificateId}")
    public Result<List<CertificateLock>> getLocksByCertificate(@PathVariable Long certificateId) {
        log.debug("Getting certificate locks for certificate: {}", certificateId);
        return certificateLockService.getCertificateLocksByCertificate(certificateId);
    }

    /**
     * Get recent certificate lock records
     */
    @Operation(summary = "Get recent certificate locks", description = "Get lock records from the last N days")
    @GetMapping("/recent")
    public Result<List<CertificateLock>> getRecentLocks(@RequestParam(defaultValue = "30") int days) {
        log.debug("Getting recent certificate locks (last {} days)", days);
        return certificateLockService.getRecentCertificateLocks(days);
    }

    /**
     * Inner class for lock request
     */
    @Schema(description = "Certificate lock request")
    public static class LockRequest {
        @Schema(description = "Certificate ID", required = true)
        private Long certificateId;

        @Schema(description = "Lock type", required = true)
        private String lockType;

        @Schema(description = "Lock reason", required = true)
        private String lockReason;

        @Schema(description = "Lock person name", required = true)
        private String lockMan;

        public Long getCertificateId() {
            return certificateId;
        }

        public void setCertificateId(Long certificateId) {
            this.certificateId = certificateId;
        }

        public String getLockType() {
            return lockType;
        }

        public void setLockType(String lockType) {
            this.lockType = lockType;
        }

        public String getLockReason() {
            return lockReason;
        }

        public void setLockReason(String lockReason) {
            this.lockReason = lockReason;
        }

        public String getLockMan() {
            return lockMan;
        }

        public void setLockMan(String lockMan) {
            this.lockMan = lockMan;
        }
    }

    /**
     * Inner class for unlock request
     */
    @Schema(description = "Certificate unlock request")
    public static class UnlockRequest {
        @Schema(description = "Unlock reason", required = true)
        private String unlockReason;

        @Schema(description = "Unlock person name", required = true)
        private String unlockMan;

        public String getUnlockReason() {
            return unlockReason;
        }

        public void setUnlockReason(String unlockReason) {
            this.unlockReason = unlockReason;
        }

        public String getUnlockMan() {
            return unlockMan;
        }

        public void setUnlockMan(String unlockMan) {
            this.unlockMan = unlockMan;
        }
    }
}
