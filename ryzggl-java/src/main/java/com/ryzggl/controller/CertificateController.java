package com.ryzggl.controller;

import com.baomidou.mybatisplus.core.metadata.IPage;
import com.ryzggl.common.Result;
import com.ryzggl.entity.Certificate;
import com.ryzggl.service.CertificateService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.springframework.web.bind.annotation.*;
import java.util.Date;

/**
 * Certificate Controller
 * Certificate management endpoints
 */
@Tag(name = "证书管理", description = "证书相关接口")
@RestController
@RequestMapping("/api/certificates")
public class CertificateController {

    private final CertificateService certificateService;

    public CertificateController(CertificateService certificateService) {
        this.certificateService = certificateService;
    }

    public static class CertificateQuery {
        private Integer current = 1;
        private Integer size = 10;
        private String certNo;
        private String holderName;
        private String qualificationName;
        private String status;

        public Integer getCurrent() {
            return current;
        }

        public void setCurrent(Integer current) {
            this.current = current;
        }

        public Integer getSize() {
            return size;
        }

        public void setSize(Integer size) {
            this.size = size;
        }

        public String getCertNo() {
            return certNo;
        }

        public void setCertNo(String certNo) {
            this.certNo = certNo;
        }

        public String getHolderName() {
            return holderName;
        }

        public void setHolderName(String holderName) {
            this.holderName = holderName;
        }

        public String getQualificationName() {
            return qualificationName;
        }

        public void setQualificationName(String qualificationName) {
            this.qualificationName = qualificationName;
        }

        public String getStatus() {
            return status;
        }

        public void setStatus(String status) {
            this.status = status;
        }
    }

    public static class CertificateOperation {
        private Long id;
        private String reason;

        public Long getId() {
            return id;
        }

        public void setId(Long id) {
            this.id = id;
        }

        public String getReason() {
            return reason;
        }

        public void setReason(String reason) {
            this.reason = reason;
        }
    }

    /**
     * Query certificate list
     */
    @Operation(summary = "查询证书列表")
    @GetMapping("/list")
    public Result<IPage<Certificate>> getCertificateList(CertificateQuery query) {
        return certificateService.getCertificateList(query.getCurrent(), query.getSize(),
                query.getCertNo(), query.getHolderName(),
                query.getQualificationName(), query.getStatus());
    }

    /**
     * Query certificate by ID
     */
    @Operation(summary = "查询证书详情")
    @GetMapping("/{id}")
    public Result<Certificate> getCertificateById(@PathVariable Long id) {
        return certificateService.getCertificateById(id);
    }

    /**
     * Create certificate
     */
    @Operation(summary = "创建证书")
    @PostMapping
    public Result<Certificate> createCertificate(@RequestBody Certificate certificate) {
        return certificateService.createCertificate(certificate);
    }

    /**
     * Update certificate
     */
    @Operation(summary = "更新证书")
    @PutMapping
    public Result<Void> updateCertificate(@RequestBody Certificate certificate) {
        return certificateService.updateCertificate(certificate);
    }

    /**
     * Pause certificate
     */
    @Operation(summary = "暂停证书")
    @PutMapping("/{id}/pause")
    public Result<Void> pauseCertificate(@PathVariable Long id, @RequestBody CertificateOperation operation) {
        return certificateService.pauseCertificate(id, operation.getReason());
    }

    /**
     * Resume certificate
     */
    @Operation(summary = "恢复证书")
    @PutMapping("/{id}/resume")
    public Result<Void> resumeCertificate(@PathVariable Long id) {
        return certificateService.resumeCertificate(id);
    }

    /**
     * Revoke certificate
     */
    @Operation(summary = "注销证书")
    @PutMapping("/{id}/revoke")
    public Result<Void> revokeCertificate(@PathVariable Long id, @RequestBody CertificateOperation operation) {
        return certificateService.revokeCertificate(id, operation.getReason());
    }

    /**
     * Delete certificate
     */
    @Operation(summary = "删除证书")
    @DeleteMapping("/{id}")
    public Result<Void> deleteCertificate(@PathVariable Long id) {
        return certificateService.deleteCertificate(id);
    }

    /**
     * Calculate certificate validity end date for continuation
     * Migrated from database function GET_CertificateContinueValidEndDate
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
    @Operation(summary = "计算证书续期有效期")
    @PostMapping("/calculate-valid-end-date")
    public Result<Date> calculateContinueValidEndDate(@RequestParam Integer postTypeID,
                                              @RequestParam Integer postID,
                                              @RequestParam String sex,
                                              @RequestParam Date birthday,
                                              @RequestParam Date validEndDate,
                                              @RequestParam(required = false) String unitCode,
                                              @RequestParam(required = false) String workerName) {
        return certificateService.calculateContinueValidEndDate(
                postTypeID, postID, sex, birthday, validEndDate, unitCode, workerName);
    }
}
