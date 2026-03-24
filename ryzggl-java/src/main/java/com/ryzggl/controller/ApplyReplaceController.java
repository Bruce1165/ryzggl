package com.ryzggl.controller;

import com.baomidou.mybatisplus.core.metadata.IPage;
import com.ryzggl.common.Result;
import com.ryzggl.entity.ApplyReplace;
import com.ryzggl.service.ApplyReplaceService;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

/**
 * ApplyReplace Controller - Certificate Replace Application API
 * 证书补办申请控制器
 *
 * REST API for managing certificate replacement applications
 * Maps to legacy ApplyReplaceDAL functionality
 *
 * Endpoints:
 * - POST   /api/v1/apply-replace              - Create replace application
 * - PUT    /api/v1/apply-replace/{applyId}     - Update replace application
 * - DELETE /api/v1/apply-replace/{applyId}     - Delete replace application
 * - GET    /api/v1/apply-replace/{applyId}     - Get replace application by ID
 * - GET    /api/v1/apply-replace/list            - Get replace application list
 * - GET    /api/v1/apply-replace/search          - Search replace applications
 * - GET    /api/v1/apply-replace/by-register-no   - Get by registration number
 * - GET    /api/v1/apply-replace/status/{status}  - Get by status
 * - PUT    /api/v1/apply-replace/{applyId}/status - Update status
 */
@RestController
@RequestMapping("/api/v1/apply-replace")
public class ApplyReplaceController {

    private static final Logger logger = LoggerFactory.getLogger(ApplyReplaceController.class);

    @Autowired
    private ApplyReplaceService applyReplaceService;

    /**
     * Create ApplyReplace
     * 创建证书补办申请
     *
     * POST /api/v1/apply-replace
     *
     * Request body: ApplyReplace JSON object
     * Response: Result<ApplyReplace>
     */
    @PostMapping
    public Result<ApplyReplace> createReplace(@RequestBody ApplyReplace applyReplace) {
        try {
            ApplyReplace created = applyReplaceService.createReplace(applyReplace);
            return Result.success(created);
        } catch (IllegalArgumentException e) {
            logger.warn("Validation error creating ApplyReplace: {}", e.getMessage());
            return Result.error("数据验证失败: " + e.getMessage());
        } catch (Exception e) {
            logger.error("Error creating ApplyReplace", e);
            return Result.error("创建证书补办申请失败: " + e.getMessage());
        }
    }

    /**
     * Update ApplyReplace
     * 更新证书补办申请
     *
     * PUT /api/v1/apply-replace/{applyId}
     *
     * Request body: ApplyReplace JSON object
     * Response: Result<ApplyReplace>
     */
    @PutMapping("/{applyId}")
    public Result<ApplyReplace> updateReplace(@PathVariable String applyId,
                                          @RequestBody ApplyReplace applyReplace) {
        try {
            applyReplace.setApplyId(applyId);
            ApplyReplace updated = applyReplaceService.updateReplace(applyReplace);
            return Result.success(updated);
        } catch (IllegalArgumentException e) {
            logger.warn("Validation error updating ApplyReplace: ApplyID={}, error={}", applyId, e.getMessage());
            return Result.error("数据验证失败: " + e.getMessage());
        } catch (Exception e) {
            logger.error("Error updating ApplyReplace: ApplyID={}", applyId, e);
            return Result.error("更新证书补办申请失败: " + e.getMessage());
        }
    }

    /**
     * Delete ApplyReplace
     * 删除证书补办申请
     *
     * DELETE /api/v1/apply-replace/{applyId}
     *
     * Response: Result<Boolean>
     */
    @DeleteMapping("/{applyId}")
    public Result<Boolean> deleteReplace(@PathVariable String applyId) {
        try {
            boolean deleted = applyReplaceService.deleteReplace(applyId);
            return Result.success(deleted);
        } catch (Exception e) {
            logger.error("Error deleting ApplyReplace: ApplyID={}", applyId, e);
            return Result.error("删除证书补办申请失败: " + e.getMessage());
        }
    }

    /**
     * Get ApplyReplace by ID
     * 获取证书补办申请详情
     *
     * GET /api/v1/apply-replace/{applyId}
     *
     * Response: Result<ApplyReplace>
     */
    @GetMapping("/{applyId}")
    public Result<ApplyReplace> getReplaceById(@PathVariable String applyId) {
        try {
            ApplyReplace applyReplace = applyReplaceService.getReplaceById(applyId);
            if (applyReplace == null) {
                return Result.error("证书补办申请不存在");
            }
            return Result.success(applyReplace);
        } catch (Exception e) {
            logger.error("Error getting ApplyReplace: ApplyID={}", applyId, e);
            return Result.error("获取证书补办申请失败: " + e.getMessage());
        }
    }

    /**
     * Get ApplyReplace by Registration Number
     * 根据注册号获取证书补办申请
     *
     * GET /api/v1/apply-replace/by-register-no?registerNo={registerNo}
     *
     * Response: Result<ApplyReplace>
     */
    @GetMapping("/by-register-no")
    public Result<ApplyReplace> getReplaceByRegisterNo(@RequestParam String registerNo) {
        try {
            ApplyReplace applyReplace = applyReplaceService.getReplaceById(registerNo);
            if (applyReplace == null) {
                return Result.error("未找到相关证书补办申请");
            }
            return Result.success(applyReplace);
        } catch (Exception e) {
            logger.error("Error getting ApplyReplace by register no: registerNo={}", registerNo, e);
            return Result.error("获取证书补办申请失败: " + e.getMessage());
        }
    }

    /**
     * Get ApplyReplace list with pagination
     * 获取证书补办申请列表
     *
     * GET /api/v1/apply-replace/list?current=1&size=10&status=PENDING
     *
     * Query params:
     * - current: Current page number (default: 1)
     * - size: Page size (default: 10)
     * - status: Filter by status (optional)
     *
     * Response: Result<IPage<ApplyReplace>>
     */
    @GetMapping("/list")
    public Result<IPage<ApplyReplace>> getReplaceList(
            @RequestParam(defaultValue = "1") int current,
            @RequestParam(defaultValue = "10") int size,
            @RequestParam(required = false) String status) {
        try {
            IPage<ApplyReplace> page = applyReplaceService.getReplaceList(current, size, status);
            return Result.success(page);
        } catch (Exception e) {
            logger.error("Error getting ApplyReplace list", e);
            return Result.error("获取证书补办申请列表失败: " + e.getMessage());
        }
    }

    /**
     * Search ApplyReplace by multiple criteria
     * 搜索证书补办申请
     *
     * GET /api/v1/apply-replace/search?registerNo={}&replaceType={}&status={}
     *
     * Query params (all optional):
     * - registerNo: Registration number (supports partial match)
     * - registerCertificateNo: Registration certificate number (supports partial match)
     * - replaceType: Replace type (exact match)
     * - status: Application status (exact match)
     * - current: Current page number (default: 1)
     * - size: Page size (default: 10)
     *
     * Response: Result<IPage<ApplyReplace>>
     */
    @GetMapping("/search")
    public Result<IPage<ApplyReplace>> searchReplaces(
            @RequestParam(required = false) String registerNo,
            @RequestParam(required = false) String registerCertificateNo,
            @RequestParam(required = false) String replaceType,
            @RequestParam(required = false) String status,
            @RequestParam(defaultValue = "1") int current,
            @RequestParam(defaultValue = "10") int size) {
        try {
            IPage<ApplyReplace> page = applyReplaceService.searchReplaces(
                    current, size, registerNo, registerCertificateNo, replaceType, status);
            return Result.success(page);
        } catch (Exception e) {
            logger.error("Error searching ApplyReplace", e);
            return Result.error("搜索证书补办申请失败: " + e.getMessage());
        }
    }

    /**
     * Get ApplyReplace by status
     * 根据状态获取证书补办申请
     *
     * GET /api/v1/apply-replace/status/{status}?current=1&size=10
     *
     * Path params:
     * - status: Application status
     *
     * Query params:
     * - current: Current page number (default: 1)
     * - size: Page size (default: 10)
     *
     * Response: Result<IPage<ApplyReplace>>
     */
    @GetMapping("/status/{status}")
    public Result<IPage<ApplyReplace>> getReplacesByStatus(
            @PathVariable String status,
            @RequestParam(defaultValue = "1") int current,
            @RequestParam(defaultValue = "10") int size) {
        try {
            IPage<ApplyReplace> page = applyReplaceService.getReplacesByStatus(status, current, size);
            return Result.success(page);
        } catch (Exception e) {
            logger.error("Error getting ApplyReplace by status: status={}", status, e);
            return Result.error("获取证书补办申请失败: " + e.getMessage());
        }
    }

    /**
     * Get ApplyReplace by Replace Type
     * 根据补办类型获取证书补办申请
     *
     * GET /api/v1/apply-replace/type/{replaceType}?current=1&size=10
     *
     * Path params:
     * - replaceType: Replace type
     *
     * Query params:
     * - current: Current page number (default: 1)
     * - size: Page size (default: 10)
     *
     * Response: Result<IPage<ApplyReplace>>
     */
    @GetMapping("/type/{replaceType}")
    public Result<IPage<ApplyReplace>> getReplacesByReplaceType(
            @PathVariable String replaceType,
            @RequestParam(defaultValue = "1") int current,
            @RequestParam(defaultValue = "10") int size) {
        try {
            IPage<ApplyReplace> page = applyReplaceService.getReplacesByReplaceType(replaceType, current, size);
            return Result.success(page);
        } catch (Exception e) {
            logger.error("Error getting ApplyReplace by replace type: replaceType={}", replaceType, e);
            return Result.error("获取证书补办申请失败: " + e.getMessage());
        }
    }

    /**
     * Update ApplyReplace status
     * 更新证书补办申请状态
     *
     * PUT /api/v1/apply-replace/{applyId}/status
     *
     * Path params:
     * - applyId: Apply ID
     *
     * Request body: { "status": "NEW_STATUS" }
     *
     * Response: Result<ApplyReplace>
     */
    @PutMapping("/{applyId}/status")
    public Result<ApplyReplace> updateStatus(
            @PathVariable String applyId,
            @RequestBody StatusUpdateRequest request) {
        try {
            if (request.getStatus() == null || request.getStatus().isEmpty()) {
                return Result.error("状态不能为空");
            }

            ApplyReplace updated = applyReplaceService.updateStatus(applyId, request.getStatus());
            return Result.success(updated);
        } catch (IllegalArgumentException e) {
            logger.warn("Validation error updating status: ApplyID={}, error={}", applyId, e.getMessage());
            return Result.error("数据验证失败: " + e.getMessage());
        } catch (Exception e) {
            logger.error("Error updating ApplyReplace status: ApplyID={}", applyId, e);
            return Result.error("更新状态失败: " + e.getMessage());
        }
    }

    /**
     * Count ApplyReplace by status
     * 根据统计证书补办申请数量
     *
     * GET /api/v1/apply-replace/count?status=PENDING
     *
     * Query params:
     * - status: Application status (optional, default: all)
     *
     * Response: Result<Long>
     */
    @GetMapping("/count")
    public Result<Long> countByStatus(@RequestParam(required = false) String status) {
        try {
            long count = applyReplaceService.countByStatus(status);
            return Result.success(count);
        } catch (Exception e) {
            logger.error("Error counting ApplyReplace: status={}", status, e);
            return Result.error("统计失败: " + e.getMessage());
        }
    }

    /**
     * Status update request DTO
     */
    public static class StatusUpdateRequest {
        private String status;

        public String getStatus() {
            return status;
        }

        public void setStatus(String status) {
            this.status = status;
        }
    }
}
