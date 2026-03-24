package com.ryzggl.controller;

import com.baomidou.mybatisplus.core.metadata.IPage;
import com.ryzggl.common.Result;
import com.ryzggl.entity.ApplyChange;
import com.ryzggl.service.ApplyChangeService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

/**
 * ApplyChange Controller - 变更申请管理
 * Certificate change application management with from/to transfer fields
 */
@Tag(name = "变更申请", description = "变更申请相关接口")
@RestController
@RequestMapping("/api/v1/apply-change")
public class ApplyChangeController {

    private static final Logger logger = LoggerFactory.getLogger(ApplyChangeController.class);

    private final ApplyChangeService applyChangeService;

    public ApplyChangeController(ApplyChangeService applyChangeService) {
        this.applyChangeService = applyChangeService;
    }

    public static class ApplyChangeQuery {
        private Integer current = 1;
        private Integer size = 10;
        private String workerName;
        private String psnRegisterNo;
        private String changeReason;
        private String ifOutside;

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

        public String getWorkerName() {
            return workerName;
        }

        public void setWorkerName(String workerName) {
            this.workerName = workerName;
        }

        public String getPsnRegisterNo() {
            return psnRegisterNo;
        }

        public void setPsnRegisterNo(String psnRegisterNo) {
            this.psnRegisterNo = psnRegisterNo;
        }

        public String getChangeReason() {
            return changeReason;
        }

        public void setChangeReason(String changeReason) {
            this.changeReason = changeReason;
        }

        public String getIfOutside() {
            return ifOutside;
        }

        public void setIfOutside(String ifOutside) {
            this.ifOutside = ifOutside;
        }
    }

    /**
     * Create change application
     */
    @Operation(summary = "创建变更申请")
    @PostMapping
    public Result<ApplyChange> createApplication(@RequestBody ApplyChange applyChange) {
        try {
            ApplyChange created = applyChangeService.createApplication(applyChange);
            logger.info("Created change application: ApplyID={}",
                    applyChange.getApplyId());
            return Result.success(created);
        } catch (Exception e) {
            logger.error("Error creating change application", e);
            return Result.error("创建变更申请失败: " + e.getMessage());
        }
    }

    /**
     * Get change application list
     */
    @Operation(summary = "获取变更申请列表")
    @GetMapping
    public Result<IPage<ApplyChange>> getApplicationList(ApplyChangeQuery query) {
        try {
            IPage<ApplyChange> page = applyChangeService.getApplicationList(
                    query.getCurrent(),
                    query.getSize(),
                    query.getWorkerName(),
                    query.getPsnRegisterNo(),
                    query.getChangeReason(),
                    query.getIfOutside()
            );
            return Result.success(page);
        } catch (Exception e) {
            logger.error("Error getting change application list", e);
            return Result.error("获取变更申请列表失败: " + e.getMessage());
        }
    }

    /**
     * Get change application by ID
     */
    @Operation(summary = "获取变更申请详情")
    @GetMapping("/{id}")
    public Result<ApplyChange> getApplicationById(@PathVariable String id) {
        try {
            ApplyChange applyChange = applyChangeService.getApplicationById(id);
            if (applyChange == null) {
                return Result.error("变更申请不存在");
            }
            return Result.success(applyChange);
        } catch (Exception e) {
            logger.error("Error getting change application: ApplyID={}", id, e);
            return Result.error("获取变更申请详情失败: " + e.getMessage());
        }
    }

    /**
     * Update change application
     */
    @Operation(summary = "更新变更申请")
    @PutMapping("/{id}")
    public Result<ApplyChange> updateApplication(@PathVariable String id,
                                             @RequestBody ApplyChange applyChange) {
        try {
            applyChange.setApplyId(id);
            ApplyChange updated = applyChangeService.updateApplication(applyChange);
            logger.info("Updated change application: ApplyID={}", id);
            return Result.success(updated);
        } catch (Exception e) {
            logger.error("Error updating change application: ApplyID={}", id, e);
            return Result.error("更新变更申请失败: " + e.getMessage());
        }
    }

    /**
     * Delete change application
     */
    @Operation(summary = "删除变更申请")
    @DeleteMapping("/{id}")
    public Result<Boolean> deleteApplication(@PathVariable String id) {
        try {
            boolean deleted = applyChangeService.deleteApplication(id);
            if (deleted) {
                logger.info("Deleted change application: ApplyID={}", id);
            }
            return Result.success(deleted);
        } catch (Exception e) {
            logger.error("Error deleting change application: ApplyID={}", id, e);
            return Result.error("删除变更申请失败: " + e.getMessage());
        }
    }

    /**
     * Get applications by status
     */
    @Operation(summary = "按状态获取变更申请")
    @GetMapping("/status/{status}")
    public Result<IPage<ApplyChange>> getApplicationsByStatus(@PathVariable String status) {
        try {
            IPage<ApplyChange> page = applyChangeService.getApplicationsByStatus(status);
            return Result.success(page);
        } catch (Exception e) {
            logger.error("Error getting applications by status", e);
            return Result.error("按状态获取变更申请失败: " + e.getMessage());
        }
    }

    /**
     * Search change applications
     */
    @Operation(summary = "搜索变更申请")
    @GetMapping("/search")
    public Result<IPage<ApplyChange>> searchApplications(
            @RequestParam(required = false) String workerName,
            @RequestParam(required = false) String psnRegisterNo,
            @RequestParam(required = false) String changeReason,
            @RequestParam(required = false) String fromEntCity,
            @RequestParam(required = false) String toEntCity,
            @RequestParam(required = false) String fromPsnSex,
            @RequestParam(required = false) String toPsnSex) {
        try {
            IPage<ApplyChange> page = applyChangeService.searchApplications(
                    workerName, psnRegisterNo, changeReason,
                    fromEntCity, toEntCity, fromPsnSex, toPsnSex);
            return Result.success(page);
        } catch (Exception e) {
            logger.error("Error searching change applications", e);
            return Result.error("搜索变更申请失败: " + e.getMessage());
        }
    }
}
