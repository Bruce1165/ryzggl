package com.ryzggl.service;

import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.ryzggl.entity.ExamResult;
import com.ryzggl.repository.ExamResultRepository;
import com.ryzggl.common.Result;
import io.swagger.v3.oas.annotations.media.Schema;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;

/**
 * Exam Service - Business logic for examination system
 */
@Service
@Transactional
public class ExamService extends ServiceImpl<ExamResultRepository, ExamResult> {

    private static final Logger log = LoggerFactory.getLogger(ExamService.class);

    /**
     * Create exam result for worker
     * Maps to: ExamResultDAL.AddResult
     */
    public Result<Void> createExamResult(Long examSignUpId, Integer score, String grade) {
        log.info("Creating exam result: signUpId={}, score={}, grade={}", examSignUpId, score, grade);

        ExamResult result = new ExamResult();
        result.setExamSignUpId(examSignUpId);
        result.setScore(score);
        result.setGrade(grade);

        boolean success = save(result);

        if (success) {
            log.info("Exam result created successfully");
            return Result.success();
        } else {
            log.error("Failed to create exam result");
            return Result.error("成绩录入失败");
        }
    }

    /**
     * Get exam results for worker
     * Maps to: ExamResultDAL.GetResultsByWorkerID
     */
    public Result<List<ExamResult>> getExamResultsByWorker(Long workerId) {
        log.debug("Getting exam results for worker: {}", workerId);
        List<ExamResult> results = lambdaQuery()
                .eq(ExamResult::getExamSignUpId, workerId)
                .orderByDesc(ExamResult::getResultDate)
                .list();

        return Result.success(results);
    }

    /**
     * Get exam results by exam
     * Maps to: ExamResultDAL.GetResultsByExamID
     */
    public Result<List<ExamResult>> getExamResultsByExam(Long examSignUpId) {
        log.debug("Getting exam results for exam: {}", examSignUpId);
        List<ExamResult> results = lambdaQuery()
                .eq(ExamResult::getExamSignUpId, examSignUpId)
                .orderByDesc(ExamResult::getResultDate)
                .list();

        return Result.success(results);
    }

    /**
     * Get exam statistics
     */
    public Result<ExamStatistics> getExamStatistics(Long examSignUpId) {
        log.debug("Getting exam statistics: {}", examSignUpId);

        List<ExamResult> allResults = lambdaQuery()
                .eq(ExamResult::getExamSignUpId, examSignUpId)
                .list();

        if (allResults == null || allResults.isEmpty()) {
            return Result.error("没有考试数据");
        }

        // Calculate statistics
        ExamStatistics stats = new ExamStatistics();
        stats.setTotalCount((long) allResults.size());
        stats.setAverageScore(calculateAverage(allResults));
        stats.setPassCount(calculatePassCount(allResults));

        return Result.success(stats);
    }

    /**
     * Calculate average score
     */
    private float calculateAverage(List<ExamResult> results) {
        if (results == null || results.isEmpty()) {
            return 0f;
        }

        int sum = 0;
        for (ExamResult result : results) {
            if (result.getScore() != null) {
                sum += result.getScore();
            }
        }

        return (float) sum / results.size();
    }

    /**
     * Calculate pass count
     */
    private long calculatePassCount(List<ExamResult> results) {
        return results.stream()
                .filter(r -> r.getGrade() != null && !"不合格".equals(r.getGrade()))
                .count();
    }

    /**
     * Inner class for exam statistics
     */
    @Schema(description = "Exam statistics response")
    public static class ExamStatistics {
        @Schema(description = "Total candidates", required = true)
        private Long totalCount;

        @Schema(description = "Average score", required = true)
        private Float averageScore;

        @Schema(description = "Pass count", required = true)
        private Long passCount;

        public Long getTotalCount() {
            return totalCount;
        }

        public void setTotalCount(Long totalCount) {
            this.totalCount = totalCount;
        }

        public Float getAverageScore() {
            return averageScore;
        }

        public void setAverageScore(Float averageScore) {
            this.averageScore = averageScore;
        }

        public Long getPassCount() {
            return passCount;
        }

        public void setPassCount(Long passCount) {
            this.passCount = passCount;
        }
    }
}
