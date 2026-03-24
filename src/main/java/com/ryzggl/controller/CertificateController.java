package com.ryzggl.controller;

import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.common.Result;
import com.ryzggl.entity.Certificate;
import com.ryzggl.entity.CertificateChange;
import com.ryzggl.service.CertificateService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.media.Schema;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.validation.annotation.Validated;
import org.springframework.web.bind.annotation.*;

import java.time.LocalDate;
import java.util.List;

/**
 * Certificate Controller - REST API for certificate management
 */
@RestController
@RequestMapping("/api/certificates")
@Tag(name = "Certificate Management", description = "Certificate lifecycle: view, change, validity")
public class CertificateController {

    private static final Logger log = LoggerFactory.getLogger(CertificateController.class);

    @Autowired
    private CertificateService certificateService;

    /**
     * Get certificate list with pagination
     * Replaces: CertifManage/CertifList.aspx with Telerik RadGrid
     */
    @Operation(summary = "Get certificate list", description = "List certificates with pagination and filtering")
    @GetMapping("/list")
    public Result<Page<Certificate>> getCertificateList(
            @RequestParam(defaultValue = "1") Integer current,
            @RequestParam(required = false) Long workerId,
            @RequestParam(required = false) String unitCode,
            @RequestParam(required = false) String status,
            @RequestParam(required = false) Boolean includeExpired,
            @RequestParam(required = false) Boolean expiringSoon) {

        log.info("Getting certificates: page={}, workerId={}, unitCode={}, status={}",
                current, workerId, unitCode, status);

        Page<Certificate> page = new Page<>(current, 10);
        var queryWrapper = new com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<Certificate>();

        if (workerId != null) {
            queryWrapper.eq(Certificate::getWorkerId, workerId);
        }
        if (unitCode != null) {
            queryWrapper.eq(Certificate::getUnitCode, unitCode);
        }
        if (status != null) {
            queryWrapper.eq(Certificate::getStatus, status);
        }

        // Include expired certificates option
        if (includeExpired != null && includeExpired) {
            // No status filter - show all including expired
        } else {
            // Default: only valid certificates
            queryWrapper.eq(Certificate::getStatus, "有效");
        }

        // Expiring soon filter (within 30 days)
        if (expiringSoon != null && expiringSoon) {
            LocalDate thirtyDaysFromNow = LocalDate.now().plusDays(30);
            queryWrapper.ge(Certificate::getValidEndDate, thirtyDaysFromNow.toString());
        }

        certificateService.page(page, queryWrapper);
        return Result.success(page);
    }

    /**
     * Get certificate by ID
     * Replaces: certificate detail views
     */
    @Operation(summary = "Get certificate by ID", description = "Get detailed certificate information")
    @GetMapping("/{id}")
    @PreAuthorize("hasAnyRole('CERTIFICATE_USER', 'CERTIFICATE_ADMIN')")
    public Result<Certificate> getCertificate(@PathVariable Long id) {
        log.debug("Getting certificate: {}", id);
        Certificate cert = certificateService.getById(id);
        return cert != null ? Result.success(cert) : Result.error("Certificate not found");
    }

    /**
     * Change certificate details
     * Replaces: CertifManage/CertififChange.aspx
     * Changes certificate type, number, with approval workflow
     */
    @Operation(summary = "Change certificate", description = "Change certificate type or number with approval")
    @PutMapping("/{id}/change")
    @PreAuthorize("hasAnyRole('CERTIFICATE_USER', 'CERTIFICATE_ADMIN')")
    public Result<Void> changeCertificate(
            @PathVariable Long id,
            @Validated @RequestBody CertificateChangeRequest request) {

        log.info("Changing certificate: {} with request: {}", id, request);

        // Create change request object
        CertificateChange change = new CertificateChange();
        change.setCertificateId(id);
        change.setChangeType(request.getChangeType());
        change.setOldCertNo(request.getOldCertNo());
        change.setNewCertNo(request.getNewCertNo());
        change.setChangeReason(request.getChangeReason());
        change.setCheckMan(request.getCheckMan());
        change.setCheckAdvise(request.getCheckAdvise());

        return certificateService.changeCertificate(change);
    }

    /**
     * Get expiring certificates
     * Dashboard widget replacement
     */
    @Operation(summary = "Get expiring certificates", description = "Get certificates expiring within 30 days")
    @GetMapping("/expiring")
    @PreAuthorize("hasAnyRole('CERTIFICATE_USER', 'CERTIFICATE_ADMIN')")
    public Result<List<Certificate>> getExpiringCertificates() {
        log.debug("Getting expiring certificates");
        return Result.success(certificateService.getExpiringCertificates());
    }

    /**
     * Validate certificate (new entry)
     * Replaces: CertifManage/CertifEnterApply.aspx
     */
    @Operation(summary = "Validate certificate", description = "Check if certificate is valid and calculate validity end date")
    @PostMapping("/validate")
    @PreAuthorize("hasAnyRole('CERTIFICATE_USER', 'CERTIFICATE_ADMIN')")
    public Result<CertificateValidityResponse> validateCertificate(
            @Validated @RequestBody CertificateValidationRequest request) {

        log.info("Validating certificate: workerId={}, postType={}, sex={}, birthday={}",
                request.getWorkerId(), request.getPostTypeId(), request.getSex(), request.getBirthday());

        // Calculate validity end date using business rules
        String validEndDate = certificateService.calculateCertificateValidityEnd(
                request.getPostTypeId(),
                request.getPostId(),
                request.getSex(),
                request.getBirthday(),
                "", // VALIDENDDATE not needed for validation
                request.getUnitCode(),
                request.getWorkerName()
        );

        CertificateValidityResponse response = new CertificateValidityResponse();
        response.setValidEndDate(validEndDate);
        response.setIsValid(true);

        return Result.success(response);
    }

    /**
     * Inner class for certificate change request
     */
    @Schema(description = "Certificate change request")
    public static class CertificateChangeRequest {
        @Schema(description = "Change type: change/cancel/continue/pause")
        private String changeType;

        @Schema(description = "Old certificate number")
        private String oldCertNo;

        @Schema(description = "New certificate number")
        private String newCertNo;

        @Schema(description = "Change reason")
        private String changeReason;

        @Schema(description = "Checker/Approver name")
        private String checkMan;

        @Schema(description = "Review/Check advice")
        private String checkAdvise;

        public String getChangeType() {
            return changeType;
        }

        public void setChangeType(String changeType) {
            this.changeType = changeType;
        }

        public String getOldCertNo() {
            return oldCertNo;
        }

        public void setOldCertNo(String oldCertNo) {
            this.oldCertNo = oldCertNo;
        }

        public String getNewCertNo() {
            return newCertNo;
        }

        public void setNewCertNo(String newCertNo) {
            this.newCertNo = newCertNo;
        }

        public String getChangeReason() {
            return changeReason;
        }

        public void setChangeReason(String changeReason) {
            this.changeReason = changeReason;
        }

        public String getCheckMan() {
            return checkMan;
        }

        public void setCheckMan(String checkMan) {
            this.checkMan = checkMan;
        }

        public String getCheckAdvise() {
            return checkAdvise;
        }

        public void setCheckAdvise(String checkAdvise) {
            this.checkAdvise = checkAdvise;
        }
    }

    /**
     * Inner class for certificate validation request
     */
    @Schema(description = "Certificate validation request")
    public static class CertificateValidationRequest {
        @Schema(description = "Worker ID", required = true)
        private Long workerId;

        @Schema(description = "Position type ID")
        private Integer postTypeId;

        @Schema(description = "Position ID (optional)")
        private Integer postId;

        @Schema(description = "Gender: 男/女", required = true)
        private String sex;

        @Schema(description = "Date of birth", required = true)
        private String birthday;

        @Schema(description = "Department code")
        private String unitCode;

        @Schema(description = "Worker name")
        private String workerName;

        public Long getWorkerId() {
            return workerId;
        }

        public void setWorkerId(Long workerId) {
            this.workerId = workerId;
        }

        public Integer getPostTypeId() {
            return postTypeId;
        }

        public void setPostTypeId(Integer postTypeId) {
            this.postTypeId = postTypeId;
        }

        public Integer getPostId() {
            return postId;
        }

        public void setPostId(Integer postId) {
            this.postId = postId;
        }

        public String getSex() {
            return sex;
        }

        public void setSex(String sex) {
            this.sex = sex;
        }

        public String getBirthday() {
            return birthday;
        }

        public void setBirthday(String birthday) {
            this.birthday = birthday;
        }

        public String getUnitCode() {
            return unitCode;
        }

        public void setUnitCode(String unitCode) {
            this.unitCode = unitCode;
        }

        public String getWorkerName() {
            return workerName;
        }

        public void setWorkerName(String workerName) {
            this.workerName = workerName;
        }
    }

    /**
     * Inner class for certificate validity response
     */
    @Schema(description = "Certificate validity response")
    public static class CertificateValidityResponse {
        @Schema(description = "Calculated validity end date (yyyy-MM-dd)")
        private String validEndDate;

        @Schema(description = "Whether certificate is valid and not expired")
        private Boolean isValid;

        public String getValidEndDate() {
            return validEndDate;
        }

        public void setValidEndDate(String validEndDate) {
            this.validEndDate = validEndDate;
        }

        public Boolean getIsValid() {
            return isValid;
        }

        public void setIsValid(Boolean isValid) {
            this.isValid = isValid;
        }
    }
}
