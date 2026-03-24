package com.ryzggl.controller;

import com.baomidou.mybatisplus.core.metadata.IPage;
import com.ryzggl.common.Result;
import com.ryzggl.entity.ExamPlace;
import com.ryzggl.service.ExamPlaceService;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

/**
 * ExamPlace Controller - Exam Place/Venue API
 * 考点控制器
 *
 * REST API for managing exam places/venues
 * Maps to legacy ExamPlaceDAL functionality
 *
 * Endpoints:
 * - POST   /api/v1/exam-places              - Create exam place
 * - PUT    /api/v1/exam-places/{examPlaceId}     - Update exam place
 * - DELETE /api/v1/exam-places/{examPlaceId}     - Delete exam place
 * - GET    /api/v1/exam-places/{examPlaceId}     - Get exam place by ID
 * - GET    /api/v1/exam-places/list            - Get exam place list
 * - GET    /api/v1/exam-places/search          - Search exam places
 * - GET    /api/v1/exam-places/active         - Get active places
 * - GET    /api/v1/exam-places/available       - Get places with capacity
 * - PUT    /api/v1/exam-places/{examPlaceId}/status - Update status
 * - GET    /api/v1/exam-places/name/{name}     - Get by name
 * - GET    /api/v1/exam-places/count          - Count by status
 */
@RestController
@RequestMapping("/api/v1/exam-places")
public class ExamPlaceController {

    private static final Logger logger = LoggerFactory.getLogger(ExamPlaceController.class);

    @Autowired
    private ExamPlaceService examPlaceService;

    /**
     * Create ExamPlace
     * 创建考点
     *
     * POST /api/v1/exam-places
     *
     * Request body: ExamPlace JSON object
     * Response: Result<ExamPlace>
     */
    @PostMapping
    public Result<ExamPlace> createPlace(@RequestBody ExamPlace examPlace) {
        try {
            ExamPlace created = examPlaceService.createPlace(examPlace);
            return Result.success(created);
        } catch (IllegalArgumentException e) {
            logger.warn("Validation error creating ExamPlace: {}", e.getMessage());
            return Result.error("数据验证失败: " + e.getMessage());
        } catch (Exception e) {
            logger.error("Error creating ExamPlace", e);
            return Result.error("创建考点失败: " + e.getMessage());
        }
    }

    /**
     * Update ExamPlace
     * 更新考点
     *
     * PUT /api/v1/exam-places/{examPlaceId}
     *
     * Request body: ExamPlace JSON object
     * Response: Result<ExamPlace>
     */
    @PutMapping("/{examPlaceId}")
    public Result<ExamPlace> updatePlace(@PathVariable Long examPlaceId,
                                        @RequestBody ExamPlace examPlace) {
        try {
            examPlace.setExamPlaceId(examPlaceId);
            ExamPlace updated = examPlaceService.updatePlace(examPlace);
            return Result.success(updated);
        } catch (IllegalArgumentException e) {
            logger.warn("Validation error updating ExamPlace: ExamPlaceID={}, error={}", examPlaceId, e.getMessage());
            return Result.error("数据验证失败: " + e.getMessage());
        } catch (Exception e) {
            logger.error("Error updating ExamPlace: ExamPlaceID={}", examPlaceId, e);
            return Result.error("更新考点失败: " + e.getMessage());
        }
    }

    /**
     * Delete ExamPlace
     * 删除考点
     *
     * DELETE /api/v1/exam-places/{examPlaceId}
     *
     * Response: Result<Boolean>
     */
    @DeleteMapping("/{examPlaceId}")
    public Result<Boolean> deletePlace(@PathVariable Long examPlaceId) {
        try {
            boolean deleted = examPlaceService.deletePlace(examPlaceId);
            return Result.success(deleted);
        } catch (Exception e) {
            logger.error("Error deleting ExamPlace: ExamPlaceID={}", examPlaceId, e);
            return Result.error("删除考点失败: " + e.getMessage());
        }
    }

    /**
     * Get ExamPlace by ID
     * 获取考点详情
     *
     * GET /api/v1/exam-places/{examPlaceId}
     *
     * Response: Result<ExamPlace>
     */
    @GetMapping("/{examPlaceId}")
    public Result<ExamPlace> getPlaceById(@PathVariable Long examPlaceId) {
        try {
            ExamPlace examPlace = examPlaceService.getPlaceById(examPlaceId);
            if (examPlace == null) {
                return Result.error("考点不存在");
            }
            return Result.success(examPlace);
        } catch (Exception e) {
            logger.error("Error getting ExamPlace: ExamPlaceID={}", examPlaceId, e);
            return Result.error("获取考点失败: " + e.getMessage());
        }
    }

    /**
     * Get ExamPlace by Name
     * 根据名称获取考点
     *
     * GET /api/v1/exam-places/name/{name}
     *
     * Response: Result<ExamPlace>
     */
    @GetMapping("/name/{name}")
    public Result<ExamPlace> getPlaceByName(@PathVariable String name) {
        try {
            ExamPlace examPlace = examPlaceService.getPlaceByName(name);
            if (examPlace == null) {
                return Result.error("未找到相关考点");
            }
            return Result.success(examPlace);
        } catch (Exception e) {
            logger.error("Error getting ExamPlace by name: name={}", name, e);
            return Result.error("获取考点失败: " + e.getMessage());
        }
    }

    /**
     * Get ExamPlace list with pagination
     * 获取考点列表
     *
     * GET /api/v1/exam-places/list?current=1&size=10&status=ACTIVE
     *
     * Query params:
     * - current: Current page number (default: 1)
     * - size: Page size (default: 10)
     * - status: Filter by status (optional)
     *
     * Response: Result<IPage<ExamPlace>>
     */
    @GetMapping("/list")
    public Result<IPage<ExamPlace>> getPlaceList(
            @RequestParam(defaultValue = "1") int current,
            @RequestParam(defaultValue = "10") int size,
            @RequestParam(required = false) String status) {
        try {
            IPage<ExamPlace> page = examPlaceService.getPlaceList(current, size, status);
            return Result.success(page);
        } catch (Exception e) {
            logger.error("Error getting ExamPlace list", e);
            return Result.error("获取考点列表失败: " + e.getMessage());
        }
    }

    /**
     * Search ExamPlace by name
     * 搜索考点（按名称）
     *
     * GET /api/v1/exam-places/search?examPlaceName={}&status={}
     *
     * Query params (all optional):
     * - examPlaceName: Exam Place name (supports partial match)
     * - status: Exam Place status (exact match)
     * - current: Current page number (default: 1)
     * - size: Page size (default: 10)
     *
     * Response: Result<IPage<ExamPlace>>
     */
    @GetMapping("/search")
    public Result<IPage<ExamPlace>> searchPlaces(
            @RequestParam(required = false) String examPlaceName,
            @RequestParam(required = false) String status,
            @RequestParam(defaultValue = "1") int current,
            @RequestParam(defaultValue = "10") int size) {
        try {
            IPage<ExamPlace> page = examPlaceService.searchPlaces(
                    current, size, examPlaceName, status);
            return Result.success(page);
        } catch (Exception e) {
            logger.error("Error searching ExamPlace", e);
            return Result.error("搜索考点失败: " + e.getMessage());
        }
    }

    /**
     * Get active exam places
     * 获取可用的考点列表
     *
     * GET /api/v1/exam-places/active?current=1&size=10
     *
     * Query params:
     * - current: Current page number (default: 1)
     * - size: Page size (default: 10)
     *
     * Response: Result<IPage<ExamPlace>>
     */
    @GetMapping("/active")
    public Result<IPage<ExamPlace>> getActivePlaces(
            @RequestParam(defaultValue = "1") int current,
            @RequestParam(defaultValue = "10") int size) {
        try {
            IPage<ExamPlace> page = examPlaceService.getActivePlaces(current, size);
            return Result.success(page);
        } catch (Exception e) {
            logger.error("Error getting active ExamPlace", e);
            return Result.error("获取可用考点列表失败: " + e.getMessage());
        }
    }

    /**
     * Get places with available capacity
     * 获取有容量的考点列表
     *
     * GET /api/v1/exam-places/available?requiredCapacity=100&status=ACTIVE
     *
     * Query params:
     * - requiredCapacity: Required minimum capacity (optional)
     * - status: Status filter (optional)
     * - current: Current page number (default: 1)
     * - size: Page size (default: 10)
     *
     * Response: Result<IPage<ExamPlace>>
     */
    @GetMapping("/available")
    public Result<IPage<ExamPlace>> getPlacesWithCapacity(
            @RequestParam(required = false) Integer requiredCapacity,
            @RequestParam(required = false) String status,
            @RequestParam(defaultValue = "1") int current,
            @RequestParam(defaultValue = "10") int size) {
        try {
            IPage<ExamPlace> page = examPlaceService.getPlacesWithCapacity(
                    current, size, requiredCapacity, status);
            return Result.success(page);
        } catch (Exception e) {
            logger.error("Error getting ExamPlace with capacity: requiredCapacity={}", requiredCapacity, e);
            return Result.error("获取考点列表失败: " + e.getMessage());
        }
    }

    /**
     * Update ExamPlace status
     * 更新考点状态
     *
     * PUT /api/v1/exam-places/{examPlaceId}/status
     *
     * Path params:
     * - examPlaceId: Exam Place ID
     *
     * Request body: { "status": "NEW_STATUS" }
     *
     * Response: Result<ExamPlace>
     */
    @PutMapping("/{examPlaceId}/status")
    public Result<ExamPlace> updateStatus(
            @PathVariable Long examPlaceId,
            @RequestBody StatusUpdateRequest request) {
        try {
            if (request.getStatus() == null || request.getStatus().isEmpty()) {
                return Result.error("状态不能为空");
            }

            ExamPlace updated = examPlaceService.updateStatus(examPlaceId, request.getStatus());
            return Result.success(updated);
        } catch (IllegalArgumentException e) {
            logger.warn("Validation error updating status: ExamPlaceID={}, error={}", examPlaceId, e.getMessage());
            return Result.error("数据验证失败: " + e.getMessage());
        } catch (Exception e) {
            logger.error("Error updating ExamPlace status: ExamPlaceID={}", examPlaceId, e);
            return Result.error("更新状态失败: " + e.getMessage());
        }
    }

    /**
     * Count ExamPlace by status
     * 根据统计考点数量
     *
     * GET /api/v1/exam-places/count?status=ACTIVE
     *
     * Query params:
     * - status: Exam Place status (optional, default: all)
     *
     * Response: Result<Long>
     */
    @GetMapping("/count")
    public Result<Long> countByStatus(@RequestParam(required = false) String status) {
        try {
            long count = examPlaceService.countByStatus(status);
            return Result.success(count);
        } catch (Exception e) {
            logger.error("Error counting ExamPlace: status={}", status, e);
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
