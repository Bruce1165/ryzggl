package com.ryzggl.controller;

import com.ryzggl.common.Result;
import com.ryzggl.entity.ExamSignUp;
import com.ryzggl.service.ExamSignUpService;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

/**
 * ExamSignUp Controller - Exam Registration API
 * REST API for managing exam registrations
 */
@RestController
@RequestMapping("/api/v1/exam-signups")
public class ExamSignUpController {

    private static final Logger logger = LoggerFactory.getLogger(ExamSignUpController.class);

    @Autowired
    private ExamSignUpService examSignUpService;

    /**
     * Create exam sign up
     */
    @PostMapping
    public Result<ExamSignUp> createSignUp(@RequestBody ExamSignUp signUp) {
        try {
            ExamSignUp created = examSignUpService.createSignUp(signUp);
            return Result.success(created);
        } catch (Exception e) {
            logger.error("Error creating exam sign up", e);
            return Result.error("创建报名失败: " + e.getMessage());
        }
    }

    /**
     * Update exam sign up
     */
    @PutMapping("/{examSignUpId}")
    public Result<ExamSignUp> updateSignUp(@PathVariable Long examSignUpId,
                                         @RequestBody ExamSignUp signUp) {
        try {
            signUp.setExamSignUpID(examSignUpId);
            ExamSignUp updated = examSignUpService.updateSignUp(signUp);
            return Result.success(updated);
        } catch (Exception e) {
            logger.error("Error updating exam sign up: ExamSignUpID={}", examSignUpId, e);
            return Result.error("更新报名失败: " + e.getMessage());
        }
    }

    /**
     * Delete exam sign up
     */
    @DeleteMapping("/{examSignUpId}")
    public Result<Boolean> deleteSignUp(@PathVariable Long examSignUpId) {
        try {
            boolean deleted = examSignUpService.deleteSignUp(examSignUpId);
            return Result.success(deleted);
        } catch (Exception e) {
            logger.error("Error deleting exam sign up: ExamSignUpID={}", examSignUpId, e);
            return Result.error("删除报名失败: " + e.getMessage());
        }
    }

    /**
     * Get exam sign up by ID
     */
    @GetMapping("/{examSignUpId}")
    public Result<ExamSignUp> getSignUp(@PathVariable Long examSignUpId) {
        try {
            ExamSignUp signUp = examSignUpService.getSignUpById(examSignUpId);
            if (signUp == null) {
                return Result.error("报名记录不存在");
            }
            return Result.success(signUp);
        } catch (Exception e) {
            logger.error("Error getting exam sign up: ExamSignUpID={}", examSignUpId, e);
            return Result.error("获取报名失败: " + e.getMessage());
        }
    }

    /**
     * Get exam signups by exam plan ID
     */
    @GetMapping("/by-exam-plan/{examPlanId}")
    public Result<List<ExamSignUp>> getSignUpsByExamPlan(@PathVariable Long examPlanId) {
        try {
            List<ExamSignUp> signUps = examSignUpService.getSignUpsByExamPlanId(examPlanId);
            return Result.success(signUps);
        } catch (Exception e) {
            logger.error("Error getting exam signups by exam plan: ExamPlanID={}", examPlanId, e);
            return Result.error("获取报名列表失败: " + e.getMessage());
        }
    }

    /**
     * Get exam signups by worker ID
     */
    @GetMapping("/by-worker/{workerId}")
    public Result<List<ExamSignUp>> getSignUpsByWorker(@PathVariable Long workerId) {
        try {
            List<ExamSignUp> signUps = examSignUpService.getSignUpsByWorkerId(workerId);
            return Result.success(signUps);
        } catch (Exception e) {
            logger.error("Error getting exam signups by worker: WorkerID={}", workerId, e);
            return Result.error("获取报名列表失败: " + e.getMessage());
        }
    }

    /**
     * Approve exam sign up
     */
    @PostMapping("/{examSignUpId}/approve")
    public Result<ExamSignUp> approveSignUp(@PathVariable Long examSignUpId,
                                           @RequestParam String checkMan,
                                           @RequestParam String checkResult) {
        try {
            ExamSignUp approved = examSignUpService.approveSignUp(examSignUpId, checkMan, checkResult);
            if (approved == null) {
                return Result.error("报名记录不存在");
            }
            return Result.success(approved);
        } catch (Exception e) {
            logger.error("Error approving exam sign up: ExamSignUpID={}", examSignUpId, e);
            return Result.error("审批报名失败: " + e.getMessage());
        }
    }

    /**
     * Confirm payment for exam sign up
     */
    @PostMapping("/{examSignUpId}/confirm-payment")
    public Result<ExamSignUp> confirmPayment(@PathVariable Long examSignUpId,
                                            @RequestParam String payConfirmMan,
                                            @RequestParam String payConfirmRult) {
        try {
            ExamSignUp confirmed = examSignUpService.confirmPayment(examSignUpId, payConfirmMan, payConfirmRult);
            if (confirmed == null) {
                return Result.error("报名记录不存在");
            }
            return Result.success(confirmed);
        } catch (Exception e) {
            logger.error("Error confirming payment: ExamSignUpID={}", examSignUpId, e);
            return Result.error("确认缴费失败: " + e.getMessage());
        }
    }

    /**
     * Get pending check exam signups
     */
    @GetMapping("/pending-check")
    public Result<List<ExamSignUp>> getPendingChecks() {
        try {
            List<ExamSignUp> signUps = examSignUpService.getPendingChecks();
            return Result.success(signUps);
        } catch (Exception e) {
            logger.error("Error getting pending checks", e);
            return Result.error("获取待审核报名失败: " + e.getMessage());
        }
    }

    /**
     * Search exam signups
     */
    @GetMapping("/search")
    public Result<List<ExamSignUp>> searchSignUps(
            @RequestParam(required = false) String workerName,
            @RequestParam(required = false) String certificateCode,
            @RequestParam(required = false) Long examPlanId,
            @RequestParam(required = false) String status) {
        try {
            List<ExamSignUp> signUps = examSignUpService.searchSignUps(
                    workerName, certificateCode, examPlanId, status);
            return Result.success(signUps);
        } catch (Exception e) {
            logger.error("Error searching exam signups", e);
            return Result.error("搜索报名失败: " + e.getMessage());
        }
    }

    /**
     * Count exam signups by exam plan ID
     */
    @GetMapping("/count/{examPlanId}")
    public Result<Integer> countByExamPlanId(@PathVariable Long examPlanId) {
        try {
            int count = examSignUpService.countByExamPlanId(examPlanId);
            return Result.success(count);
        } catch (Exception e) {
            logger.error("Error counting signups: ExamPlanID={}", examPlanId, e);
            return Result.error("统计报名数量失败: " + e.getMessage());
        }
    }
}
