package com.ryzggl.controller;

import com.baomidou.mybatisplus.core.metadata.IPage;
import com.ryzggl.common.Result;
import com.ryzggl.entity.ApplyFirst;
import com.ryzggl.service.ApplyFirstService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

/**
 * ApplyFirst Controller - 首次注册申请管理
 * Initial registration application management
 */
@Tag(name = "首次注册申请", description = "首次注册申请相关接口")
@RestController
@RequestMapping("/api/v1/apply-first")
public class ApplyFirstController {

    private static final Logger logger = LoggerFactory.getLogger(ApplyFirstController.class);

    private final ApplyFirstService applyFirstService;

    public ApplyFirstController(ApplyFirstService applyFirstService) {
        this.applyFirstService = applyFirstService;
    }

    public static class ApplyFirstQuery {
        private Integer current = 1;
        private Integer size = 10;
        private String workerName;
        private String idCard;
        private String school;
        private String major;
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

        public String getIdCard() {
            return idCard;
        }

        public void setIdCard(String idCard) {
            this.idCard = idCard;
        }

        public String getSchool() {
            return school;
        }

        public void setSchool(String school) {
            this.school = school;
        }

        public String getMajor() {
            return major;
        }

        public void setMajor(String major) {
            this.major = major;
        }

        public String getApplyStatus() {
            return applyStatus;
        }

        public void setApplyStatus(String applyStatus) {
            this.applyStatus = applyStatus;
        }
    }

    /**
     * Create initial registration application
     */
    @Operation(summary = "创建首次注册申请")
    @PostMapping
    public Result<ApplyFirst> createApplication(@RequestBody ApplyFirst applyFirst) {
        try {
            ApplyFirst created = applyFirstService.createApplication(applyFirst);
            logger.info("Created initial registration application: ApplyID={}",
                    applyFirst.getApplyId());
            return Result.success(created);
        } catch (Exception e) {
            logger.error("Error creating initial registration application", e);
            return Result.error("创建首次注册申请失败: " + e.getMessage());
        }
    }

    /**
     * Get initial registration application list
     */
    @Operation(summary = "获取首次注册申请列表")
    @GetMapping
    public Result<IPage<ApplyFirst>> getApplicationList(ApplyFirstQuery query) {
        try {
            IPage<ApplyFirst> page = applyFirstService.getApplicationList(
                    query.getCurrent(),
                    query.getSize(),
                    query.getWorkerName(),
                    query.getIdCard(),
                    query.getSchool(),
                    query.getMajor(),
                    query.getApplyStatus()
            );
            return Result.success(page);
        } catch (Exception e) {
            logger.error("Error getting initial registration application list", e);
            return Result.error("获取首次注册申请列表失败: " + e.getMessage());
        }
    }

    /**
     * Get initial registration application by ID
     */
    @Operation(summary = "获取首次注册申请详情")
    @GetMapping("/{id}")
    public Result<ApplyFirst> getApplicationById(@PathVariable String id) {
        try {
            ApplyFirst applyFirst = applyFirstService.getApplicationById(id);
            if (applyFirst == null) {
                return Result.error("首次注册申请不存在");
            }
            return Result.success(applyFirst);
        } catch (Exception e) {
            logger.error("Error getting initial registration application: ApplyID={}", id, e);
            return Result.error("获取首次注册申请详情失败: " + e.getMessage());
        }
    }

    /**
     * Update initial registration application
     */
    @Operation(summary = "更新首次注册申请")
    @PutMapping("/{id}")
    public Result<ApplyFirst> updateApplication(@PathVariable String id,
                                             @RequestBody ApplyFirst applyFirst) {
        try {
            applyFirst.setApplyId(id);
            ApplyFirst updated = applyFirstService.updateApplication(applyFirst);
            logger.info("Updated initial registration application: ApplyID={}", id);
            return Result.success(updated);
        } catch (Exception e) {
            logger.error("Error updating initial registration application: ApplyID={}", id, e);
            return Result.error("更新首次注册申请失败: " + e.getMessage());
        }
    }

    /**
     * Delete initial registration application
     */
    @Operation(summary = "删除首次注册申请")
    @DeleteMapping("/{id}")
    public Result<Boolean> deleteApplication(@PathVariable String id) {
        try {
            boolean deleted = applyFirstService.deleteApplication(id);
            if (deleted) {
                logger.info("Deleted initial registration application: ApplyID={}", id);
            }
            return Result.success(deleted);
        } catch (Exception e) {
            logger.error("Error deleting initial registration application: ApplyID={}", id, e);
            return Result.error("删除首次注册申请失败: " + e.getMessage());
        }
    }

    /**
     * Get applications by status
     */
    @Operation(summary = "按状态获取首次注册申请")
    @GetMapping("/status/{status}")
    public Result<IPage<ApplyFirst>> getApplicationsByStatus(@PathVariable String status) {
        try {
            IPage<ApplyFirst> page = applyFirstService.getApplicationsByStatus(status);
            return Result.success(page);
        } catch (Exception e) {
            logger.error("Error getting applications by status", e);
            return Result.error("按状态获取首次注册申请失败: " + e.getMessage());
        }
    }

    /**
     * Search initial registration applications
     */
    @Operation(summary = "搜索首次注册申请")
    @GetMapping("/search")
    public Result<IPage<ApplyFirst>> searchApplications(
            @RequestParam(required = false) String workerName,
            @RequestParam(required = false) String idCard,
            @RequestParam(required = false) String school,
            @RequestParam(required = false) String major) {
        try {
            IPage<ApplyFirst> page = applyFirstService.searchApplications(
                    workerName, idCard, school, major);
            return Result.success(page);
        } catch (Exception e) {
            logger.error("Error searching initial registration applications", e);
            return Result.error("搜索首次注册申请失败: " + e.getMessage());
        }
    }
}
