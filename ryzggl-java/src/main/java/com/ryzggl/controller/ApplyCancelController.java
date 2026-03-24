package com.ryzggl.controller;

import com.baomidou.mybatisplus.core.metadata.IPage;
import com.ryzggl.common.Result;
import com.ryzggl.entity.ApplyCancel;
import com.ryzggl.service.ApplyCancelService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

/**
 * ApplyCancel Controller - 注销申请管理
 * Certificate cancellation application management
 */
@Tag(name = "注销申请", description = "注销申请相关接口")
@RestController
@RequestMapping("/api/v1/apply-cancel")
public class ApplyCancelController {

    private static final Logger logger = LoggerFactory.getLogger(ApplyCancelController.class);

    private final ApplyCancelService applyCancelService;

    public ApplyCancelController(ApplyCancelService applyCancelService) {
        this.applyCancelService = applyCancelService;
    }

    public static class ApplyCancelQuery {
        private Integer current = 1;
        private Integer size = 10;
        private String workerName;
        private String psnRegisterNo;
        private String applyStatus;

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

        public String getApplyStatus() {
            return applyStatus;
        }

        public void setApplyStatus(String applyStatus) {
            this.applyStatus = applyStatus;
        }
    }

    /**
     * Create cancellation application
     */
    @Operation(summary = "创建注销申请")
    @PostMapping
    public Result<ApplyCancel> createApplication(@RequestBody ApplyCancel applyCancel) {
        try {
            ApplyCancel created = applyCancelService.createApplication(applyCancel);
            logger.info("Created cancellation application: ApplyID={}",
                    applyCancel.getApplyId());
            return Result.success(created);
        } catch (Exception e) {
            logger.error("Error creating cancellation application", e);
            return Result.error("创建注销申请失败: " + e.getMessage());
        }
    }

    /**
     * Get cancellation application list
     */
    @Operation(summary = "获取注销申请列表")
    @GetMapping
    public Result<IPage<ApplyCancel>> getApplicationList(ApplyCancelQuery query) {
        try {
            IPage<ApplyCancel> page = applyCancelService.getApplicationList(
                    query.getCurrent(),
                    query.getSize(),
                    query.getWorkerName(),
                    query.getPsnRegisterNo(),
                    query.getApplyStatus()
            );
            return Result.success(page);
        } catch (Exception e) {
            logger.error("Error getting cancellation application list", e);
            return Result.error("获取注销申请列表失败: " + e.getMessage());
        }
    }

    /**
     * Get cancellation application by ID
     */
    @Operation(summary = "获取注销申请详情")
    @GetMapping("/{id}")
    public Result<ApplyCancel> getApplicationById(@PathVariable String id) {
        try {
            ApplyCancel applyCancel = applyCancelService.getApplicationById(id);
            if (applyCancel == null) {
                return Result.error("注销申请不存在");
            }
            return Result.success(applyCancel);
        } catch (Exception e) {
            logger.error("Error getting cancellation application: ApplyID={}", id, e);
            return Result.error("获取注销申请详情失败: " + e.getMessage());
        }
    }

    /**
     * Update cancellation application
     */
    @Operation(summary = "更新注销申请")
    @PutMapping("/{id}")
    public Result<ApplyCancel> updateApplication(@PathVariable String id,
                                             @RequestBody ApplyCancel applyCancel) {
        try {
            applyCancel.setApplyId(id);
            ApplyCancel updated = applyCancelService.updateApplication(applyCancel);
            logger.info("Updated cancellation application: ApplyID={}", id);
            return Result.success(updated);
        } catch (Exception e) {
            logger.error("Error updating cancellation application: ApplyID={}", id, e);
            return Result.error("更新注销申请失败: " + e.getMessage());
        }
    }

    /**
     * Delete cancellation application
     */
    @Operation(summary = "删除注销申请")
    @DeleteMapping("/{id}")
    public Result<Boolean> deleteApplication(@PathVariable String id) {
        try {
            boolean deleted = applyCancelService.deleteApplication(id);
            if (deleted) {
                logger.info("Deleted cancellation application: ApplyID={}", id);
            }
            return Result.success(deleted);
        } catch (Exception e) {
            logger.error("Error deleting cancellation application: ApplyID={}", id, e);
            return Result.error("删除注销申请失败: " + e.getMessage());
        }
    }

    /**
     * Get applications by status
     */
    @Operation(summary = "按状态获取注销申请")
    @GetMapping("/status/{status}")
    public Result<IPage<ApplyCancel>> getApplicationsByStatus(@PathVariable String status) {
        try {
            IPage<ApplyCancel> page = applyCancelService.getApplicationsByStatus(status);
            return Result.success(page);
        } catch (Exception e) {
            logger.error("Error getting applications by status", e);
            return Result.error("按状态获取注销申请失败: " + e.getMessage());
        }
    }

    /**
     * Search cancellation applications
     */
    @Operation(summary = "搜索注销申请")
    @GetMapping("/search")
    public Result<IPage<ApplyCancel>> searchApplications(
            @RequestParam(required = false) String workerName,
            @RequestParam(required = false) String psnRegisterNo,
            @RequestParam(required = false) String cancelReason) {
        try {
            IPage<ApplyCancel> page = applyCancelService.searchApplications(
                    workerName, psnRegisterNo, cancelReason);
            return Result.success(page);
        } catch (Exception e) {
            logger.error("Error searching cancellation applications", e);
            return Result.error("搜索注销申请失败: " + e.getMessage());
        }
    }
}
