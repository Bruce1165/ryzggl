package com.ryzggl.controller;

import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.common.Result;
import com.ryzggl.entity.ExamResult;
import com.ryzggl.entity.ExamSignUp;
import com.ryzggl.service.ExamService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.media.Schema;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.validation.annotation.Validated;
import org.springframework.web.bind.annotation.*;

import java.util.List;

/**
 * Exam Controller - REST API for examination system
 */
@RestController
@RequestMapping("/api/exam")
@Tag(name = "Examination Management", description = "Exam plans, venues, registration, results")
public class ExamController {

    private static final Logger log = LoggerFactory.getLogger(ExamController.class);

    @Autowired
    private ExamService examService;

    /**
     * Create exam result
     */
    @Operation(summary = "Record exam result", description = "Create exam result for worker's exam")
    @PostMapping("/results")
    @PreAuthorize("hasAnyRole('EXAM_ADMIN', 'WORKER_ADMIN')")
    public Result<Void> createExamResult(@Validated @RequestBody ExamResultCreateRequest request) {
        log.info("Creating exam result: workerId={}, score={}, grade={}",
                request.getWorkerId(), request.getScore(), request.getGrade());

        return examService.createExamResult(
                request.getExamSignUpId(),
                request.getScore(),
                request.getGrade()
        );
    }

    /**
     * Get exam results by worker
     */
    @Operation(summary = "Get worker exam results", description = "Get all exam results for a worker")
    @GetMapping("/results/worker/{workerId}")
    @PreAuthorize("hasAnyRole('EXAM_ADMIN', 'WORKER_ADMIN')")
    public Result<List<ExamResult>> getExamResultsByWorker(@PathVariable Long workerId) {
        log.info("Getting exam results for worker: {}", workerId);
        return examService.getExamResultsByWorker(workerId);
    }

    /**
     * Get exam results by exam
     */
    @Operation(summary = "Get exam results", description = "Get all results for a specific exam")
    @GetMapping("/results/exam/{examSignUpId}")
    @PreAuthorize("hasAnyRole('EXAM_ADMIN', 'WORKER_ADMIN')")
    public Result<List<ExamResult>> getExamResultsByExam(@PathVariable Long examSignUpId) {
        log.info("Getting exam results for exam: {}", examSignUpId);
        return examService.getExamResultsByExam(examSignUpId);
    }

    /**
     * Get exam statistics
     */
    @Operation(summary = "Get exam statistics", description = "Get exam statistics (total, average, pass rate)")
    @GetMapping("/statistics/{examSignUpId}")
    @PreAuthorize("hasAnyRole('EXAM_ADMIN', 'WORKER_ADMIN')")
    public Result<ExamService.ExamStatistics> getExamStatistics(@PathVariable Long examSignUpId) {
        log.info("Getting exam statistics: {}", examSignUpId);
        return examService.getExamStatistics(examSignUpId);
    }

    /**
     * Inner class for exam result creation
     */
    @Schema(description = "Exam result creation request")
    public static class ExamResultCreateRequest {
        @Schema(description = "Worker ID", required = true)
        private Long workerId;

        @Schema(description = "Exam registration ID", required = true)
        private Long examSignUpId;

        @Schema(description = "Exam score", required = true)
        private Integer score;

        @Schema(description = "Grade level (优秀, 良好, 合格, 不合格)")
        private String grade;

        public Long getWorkerId() {
            return workerId;
        }

        public void setWorkerId(Long workerId) {
            this.workerId = workerId;
        }

        public Long getExamSignUpId() {
            return examSignUpId;
        }

        public void setExamSignUpId(Long examSignUpId) {
            this.examSignUpId = examSignUpId;
        }

        public Integer getScore() {
            return score;
        }

        public void setScore(Integer score) {
            this.score = score;
        }

        public String getGrade() {
            return grade;
        }

        public void setGrade(String grade) {
            this.grade = grade;
        }
    }
}
