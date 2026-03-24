package com.ryzggl.controller;

import com.ryzggl.common.Result;
import com.ryzggl.entity.ExamResult;
import com.ryzggl.service.ExamResultService;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * ExamResult Controller - Exam Results API
 * REST API for managing exam results
 */
@RestController
@RequestMapping("/api/v1/exam-results")
public class ExamResultController {

    private static final Logger logger = LoggerFactory.getLogger(ExamResultController.class);

    @Autowired
    private ExamResultService examResultService;

    /**
     * Create exam result
     */
    @PostMapping
    public Result<ExamResult> createResult(@RequestBody ExamResult examResult) {
        try {
            ExamResult created = examResultService.createResult(examResult);
            return Result.success(created);
        } catch (Exception e) {
            logger.error("Error creating exam result", e);
            return Result.error("创建考试成绩失败: " + e.getMessage());
        }
    }

    /**
     * Update exam result
     */
    @PutMapping("/{examResultId}")
    public Result<ExamResult> updateResult(@PathVariable Long examResultId,
                                         @RequestBody ExamResult examResult) {
        try {
            examResult.setExamResultID(examResultId);
            ExamResult updated = examResultService.updateResult(examResult);
            return Result.success(updated);
        } catch (Exception e) {
            logger.error("Error updating exam result: ExamResultID={}", examResultId, e);
            return Result.error("更新考试成绩失败: " + e.getMessage());
        }
    }

    /**
     * Delete exam result
     */
    @DeleteMapping("/{examResultId}")
    public Result<Boolean> deleteResult(@PathVariable Long examResultId) {
        try {
            boolean deleted = examResultService.deleteResult(examResultId);
            return Result.success(deleted);
        } catch (Exception e) {
            logger.error("Error deleting exam result: ExamResultID={}", examResultId, e);
            return Result.error("删除考试成绩失败: " + e.getMessage());
        }
    }

    /**
     * Delete exam results by exam plan ID
     */
    @DeleteMapping("/by-exam-plan/{examPlanId}")
    public Result<Integer> deleteResultsByExamPlanId(@PathVariable Long examPlanId) {
        try {
            int deleted = examResultService.deleteResultsByExamPlanId(examPlanId);
            return Result.success(deleted);
        } catch (Exception e) {
            logger.error("Error deleting exam results: ExamPlanID={}", examPlanId, e);
            return Result.error("删除考试成绩失败: " + e.getMessage());
        }
    }

    /**
     * Get exam result by ID
     */
    @GetMapping("/{examResultId}")
    public Result<ExamResult> getResult(@PathVariable Long examResultId) {
        try {
            ExamResult examResult = examResultService.getResultById(examResultId);
            if (examResult == null) {
                return Result.error("考试成绩不存在");
            }
            return Result.success(examResult);
        } catch (Exception e) {
            logger.error("Error getting exam result: ExamResultID={}", examResultId, e);
            return Result.error("获取考试成绩失败: " + e.getMessage());
        }
    }

    /**
     * Get exam result by exam card ID
     */
    @GetMapping("/by-exam-card/{examCardId}")
    public Result<ExamResult> getResultByExamCardId(@PathVariable String examCardId) {
        try {
            ExamResult examResult = examResultService.getResultByExamCardId(examCardId);
            if (examResult == null) {
                return Result.error("考试成绩不存在");
            }
            return Result.success(examResult);
        } catch (Exception e) {
            logger.error("Error getting exam result by card: ExamCardID={}", examCardId, e);
            return Result.error("获取考试成绩失败: " + e.getMessage());
        }
    }

    /**
     * Get exam results by exam plan ID
     */
    @GetMapping("/by-exam-plan/{examPlanId}")
    public Result<List<ExamResult>> getResultsByExamPlan(@PathVariable Long examPlanId) {
        try {
            List<ExamResult> results = examResultService.getResultsByExamPlanId(examPlanId);
            return Result.success(results);
        } catch (Exception e) {
            logger.error("Error getting exam results by exam plan: ExamPlanID={}", examPlanId, e);
            return Result.error("获取考试成绩列表失败: " + e.getMessage());
        }
    }

    /**
     * Get exam results by worker ID
     */
    @GetMapping("/by-worker/{workerId}")
    public Result<List<ExamResult>> getResultsByWorker(@PathVariable Long workerId) {
        try {
            List<ExamResult> results = examResultService.getResultsByWorkerId(workerId);
            return Result.success(results);
        } catch (Exception e) {
            logger.error("Error getting exam results by worker: WorkerID={}", workerId, e);
            return Result.error("获取考试成绩列表失败: " + e.getMessage());
        }
    }

    /**
     * Pass an exam result
     */
    @PostMapping("/{examResultId}/pass")
    public Result<ExamResult> passResult(@PathVariable Long examResultId) {
        try {
            ExamResult result = examResultService.passResult(examResultId);
            if (result == null) {
                return Result.error("考试成绩不存在");
            }
            return Result.success(result);
        } catch (Exception e) {
            logger.error("Error passing exam result: ExamResultID={}", examResultId, e);
            return Result.error("设置合格失败: " + e.getMessage());
        }
    }

    /**
     * Fail an exam result
     */
    @PostMapping("/{examResultId}/fail")
    public Result<ExamResult> failResult(@PathVariable Long examResultId) {
        try {
            ExamResult result = examResultService.failResult(examResultId);
            if (result == null) {
                return Result.error("考试成绩不存在");
            }
            return Result.success(result);
        } catch (Exception e) {
            logger.error("Error failing exam result: ExamResultID={}", examResultId, e);
            return Result.error("设置不合格失败: " + e.getMessage());
        }
    }

    /**
     * Publish exam results for an exam plan
     */
    @PostMapping("/publish/{examPlanId}")
    public Result<Integer> publishResults(@PathVariable Long examPlanId) {
        try {
            int published = examResultService.publishResultsForExamPlan(examPlanId);
            return Result.success(published);
        } catch (Exception e) {
            logger.error("Error publishing results: ExamPlanID={}", examPlanId, e);
            return Result.error("发布成绩失败: " + e.getMessage());
        }
    }

    /**
     * Get pass rate for exam plan
     */
    @GetMapping("/pass-rate/{examPlanId}")
    public Result<Double> getPassRate(@PathVariable Long examPlanId) {
        try {
            double passRate = examResultService.getPassRate(examPlanId);
            return Result.success(passRate);
        } catch (Exception e) {
            logger.error("Error getting pass rate: ExamPlanID={}", examPlanId, e);
            return Result.error("获取通过率失败: " + e.getMessage());
        }
    }

    /**
     * Get pass statistics for exam plan
     */
    @GetMapping("/statistics/{examPlanId}")
    public Result<Map<String, Object>> getStatistics(@PathVariable Long examPlanId) {
        try {
            int[] stats = examResultService.getPassStatistics(examPlanId);
            Map<String, Object> result = new HashMap<>();
            result.put("total", stats[0]);
            result.put("passed", stats[1]);
            result.put("failed", stats[2]);
            result.put("passRate", stats[0] > 0 ? (double) stats[1] / stats[0] * 100 : 0);
            return Result.success(result);
        } catch (Exception e) {
            logger.error("Error getting statistics: ExamPlanID={}", examPlanId, e);
            return Result.error("获取统计信息失败: " + e.getMessage());
        }
    }

    /**
     * Search exam results
     */
    @GetMapping("/search")
    public Result<List<ExamResult>> searchResults(
            @RequestParam(required = false) String examCardId,
            @RequestParam(required = false) Long examPlanId,
            @RequestParam(required = false) String status,
            @RequestParam(required = false) String examResult) {
        try {
            List<ExamResult> results = examResultService.searchResults(
                    examCardId, examPlanId, status, examResult);
            return Result.success(results);
        } catch (Exception e) {
            logger.error("Error searching exam results", e);
            return Result.error("搜索考试成绩失败: " + e.getMessage());
        }
    }
}
