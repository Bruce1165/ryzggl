package com.ryzggl.controller;

import com.ryzggl.common.Result;
import com.ryzggl.entity.ExamPlan;
import com.ryzggl.service.ExamPlanService;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

/**
 * ExamPlan Controller - Exam Plan API
 * REST API for managing exam plans
 */
@RestController
@RequestMapping("/api/v1/exam-plans")
public class ExamPlanController {

    private static final Logger logger = LoggerFactory.getLogger(ExamPlanController.class);

    @Autowired
    private ExamPlanService examPlanService;

    /**
     * Create exam plan
     */
    @PostMapping
    public Result<ExamPlan> createExamPlan(@RequestBody ExamPlan examPlan) {
        try {
            ExamPlan created = examPlanService.createExamPlan(examPlan);
            return Result.success(created);
        } catch (Exception e) {
            logger.error("Error creating exam plan", e);
            return Result.error("创建考试计划失败: " + e.getMessage());
        }
    }

    /**
     * Update exam plan
     */
    @PutMapping("/{examPlanId}")
    public Result<ExamPlan> updateExamPlan(@PathVariable Long examPlanId,
                                          @RequestBody ExamPlan examPlan) {
        try {
            examPlan.setExamPlanID(examPlanId);
            ExamPlan updated = examPlanService.updateExamPlan(examPlan);
            return Result.success(updated);
        } catch (Exception e) {
            logger.error("Error updating exam plan: ExamPlanID={}", examPlanId, e);
            return Result.error("更新考试计划失败: " + e.getMessage());
        }
    }

    /**
     * Delete exam plan
     */
    @DeleteMapping("/{examPlanId}")
    public Result<Boolean> deleteExamPlan(@PathVariable Long examPlanId) {
        try {
            boolean deleted = examPlanService.deleteExamPlan(examPlanId);
            return Result.success(deleted);
        } catch (Exception e) {
            logger.error("Error deleting exam plan: ExamPlanID={}", examPlanId, e);
            return Result.error("删除考试计划失败: " + e.getMessage());
        }
    }

    /**
     * Get exam plan by ID
     */
    @GetMapping("/{examPlanId}")
    public Result<ExamPlan> getExamPlan(@PathVariable Long examPlanId) {
        try {
            ExamPlan examPlan = examPlanService.getExamPlanById(examPlanId);
            if (examPlan == null) {
                return Result.error("考试计划不存在");
            }
            return Result.success(examPlan);
        } catch (Exception e) {
            logger.error("Error getting exam plan: ExamPlanID={}", examPlanId, e);
            return Result.error("获取考试计划失败: " + e.getMessage());
        }
    }

    /**
     * Get exam plan with sign up count
     */
    @GetMapping("/{examPlanId}/with-count")
    public Result<ExamPlan> getExamPlanWithCount(@PathVariable Long examPlanId) {
        try {
            ExamPlan examPlan = examPlanService.getExamPlanWithSignUpCount(examPlanId);
            if (examPlan == null) {
                return Result.error("考试计划不存在");
            }
            return Result.success(examPlan);
        } catch (Exception e) {
            logger.error("Error getting exam plan with count: ExamPlanID={}", examPlanId, e);
            return Result.error("获取考试计划失败: " + e.getMessage());
        }
    }

    /**
     * Get exam plans by post type ID
     */
    @GetMapping("/by-post-type/{postTypeId}")
    public Result<List<ExamPlan>> getExamPlansByPostType(@PathVariable Integer postTypeId) {
        try {
            List<ExamPlan> examPlans = examPlanService.getExamPlansByPostTypeId(postTypeId);
            return Result.success(examPlans);
        } catch (Exception e) {
            logger.error("Error getting exam plans by post type: PostTypeID={}", postTypeId, e);
            return Result.error("获取考试计划列表失败: " + e.getMessage());
        }
    }

    /**
     * Get published exam plans
     */
    @GetMapping("/published")
    public Result<List<ExamPlan>> getPublishedExamPlans() {
        try {
            List<ExamPlan> examPlans = examPlanService.getPublishedExamPlans();
            return Result.success(examPlans);
        } catch (Exception e) {
            logger.error("Error getting published exam plans", e);
            return Result.error("获取已发布考试计划失败: " + e.getMessage());
        }
    }

    /**
     * Get active exam plans (within sign up period)
     */
    @GetMapping("/active")
    public Result<List<ExamPlan>> getActiveExamPlans() {
        try {
            List<ExamPlan> examPlans = examPlanService.getActiveExamPlans();
            return Result.success(examPlans);
        } catch (Exception e) {
            logger.error("Error getting active exam plans", e);
            return Result.error("获取可报名考试计划失败: " + e.getMessage());
        }
    }

    /**
     * Get upcoming exam plans
     */
    @GetMapping("/upcoming")
    public Result<List<ExamPlan>> getUpcomingExamPlans(
            @RequestParam(defaultValue = "10") int limit) {
        try {
            List<ExamPlan> examPlans = examPlanService.getUpcomingExamPlans(limit);
            return Result.success(examPlans);
        } catch (Exception e) {
            logger.error("Error getting upcoming exam plans", e);
            return Result.error("获取即将开始考试计划失败: " + e.getMessage());
        }
    }

    /**
     * Publish exam plan
     */
    @PostMapping("/{examPlanId}/publish")
    public Result<ExamPlan> publishExamPlan(@PathVariable Long examPlanId) {
        try {
            ExamPlan examPlan = examPlanService.publishExamPlan(examPlanId);
            if (examPlan == null) {
                return Result.error("考试计划不存在");
            }
            return Result.success(examPlan);
        } catch (Exception e) {
            logger.error("Error publishing exam plan: ExamPlanID={}", examPlanId, e);
            return Result.error("发布考试计划失败: " + e.getMessage());
        }
    }

    /**
     * Close exam plan
     */
    @PostMapping("/{examPlanId}/close")
    public Result<ExamPlan> closeExamPlan(@PathVariable Long examPlanId) {
        try {
            ExamPlan examPlan = examPlanService.closeExamPlan(examPlanId);
            if (examPlan == null) {
                return Result.error("考试计划不存在");
            }
            return Result.success(examPlan);
        } catch (Exception e) {
            logger.error("Error closing exam plan: ExamPlanID={}", examPlanId, e);
            return Result.error("关闭考试计划失败: " + e.getMessage());
        }
    }

    /**
     * Get available spots for exam plan
     */
    @GetMapping("/{examPlanId}/available-spots")
    public Result<Integer> getAvailableSpots(@PathVariable Long examPlanId) {
        try {
            int spots = examPlanService.getAvailableSpots(examPlanId);
            return Result.success(spots);
        } catch (Exception e) {
            logger.error("Error getting available spots: ExamPlanID={}", examPlanId, e);
            return Result.error("获取可报名名额失败: " + e.getMessage());
        }
    }

    /**
     * Check if exam plan is fully booked
     */
    @GetMapping("/{examPlanId}/is-fully-booked")
    public Result<Boolean> isFullyBooked(@PathVariable Long examPlanId) {
        try {
            boolean fullyBooked = examPlanService.isFullyBooked(examPlanId);
            return Result.success(fullyBooked);
        } catch (Exception e) {
            logger.error("Error checking fully booked: ExamPlanID={}", examPlanId, e);
            return Result.error("检查报名情况失败: " + e.getMessage());
        }
    }

    /**
     * Search exam plans
     */
    @GetMapping("/search")
    public Result<List<ExamPlan>> searchExamPlans(
            @RequestParam(required = false) String examPlanName,
            @RequestParam(required = false) Integer postTypeId,
            @RequestParam(required = false) String status) {
        try {
            List<ExamPlan> examPlans = examPlanService.searchExamPlans(
                    examPlanName, postTypeId, status);
            return Result.success(examPlans);
        } catch (Exception e) {
            logger.error("Error searching exam plans", e);
            return Result.error("搜索考试计划失败: " + e.getMessage());
        }
    }
}
