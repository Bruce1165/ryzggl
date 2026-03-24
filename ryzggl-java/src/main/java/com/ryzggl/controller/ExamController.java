package com.ryzggl.controller;

import com.baomidou.mybatisplus.core.metadata.IPage;
import com.ryzggl.common.Result;
import com.ryzggl.entity.Exam;
import com.ryzggl.service.ExamService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.springframework.web.bind.annotation.*;

import java.util.List;

/**
 * Exam Controller
 * Examination management endpoints
 */
@Tag(name = "考试管理", description = "考试相关接口")
@RestController
@RequestMapping("/api/exam")
public class ExamController {

    private final ExamService examService;

    public ExamController(ExamService examService) {
        this.examService = examService;
    }

    public static class ExamQuery {
        private Integer current = 1;
        private Integer size = 10;
        private String examName;
        private String qualificationName;
        private String status;

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

        public String getExamName() {
            return examName;
        }

        public void setExamName(String examName) {
            this.examName = examName;
        }

        public String getQualificationName() {
            return qualificationName;
        }

        public void setQualificationName(String qualificationName) {
            this.qualificationName = qualificationName;
        }

        public String getStatus() {
            return status;
        }

        public void setStatus(String status) {
            this.status = status;
        }
    }

    /**
     * Query exam list
     */
    @Operation(summary = "查询考试列表")
    @GetMapping("/list")
    public Result<IPage<Exam>> getExamList(ExamQuery query) {
        return examService.getExamList(query.getCurrent(), query.getSize(),
                query.getExamName(), query.getQualificationName(), query.getStatus());
    }

    /**
     * Query exam by ID
     */
    @Operation(summary = "查询考试详情")
    @GetMapping("/{id}")
    public Result<Exam> getExamById(@PathVariable Long id) {
        return examService.getExamById(id);
    }

    /**
     * Query available exams
     */
    @Operation(summary = "查询可报名考试")
    @GetMapping("/available")
    public Result<List<Exam>> getAvailableExams() {
        return examService.getAvailableExams();
    }

    /**
     * Create exam
     */
    @Operation(summary = "创建考试")
    @PostMapping
    public Result<Exam> createExam(@RequestBody Exam exam) {
        return examService.createExam(exam);
    }

    /**
     * Update exam
     */
    @Operation(summary = "更新考试")
    @PutMapping
    public Result<Void> updateExam(@RequestBody Exam exam) {
        return examService.updateExam(exam);
    }

    /**
     * Delete exam
     */
    @Operation(summary = "删除考试")
    @DeleteMapping("/{id}")
    public Result<Void> deleteExam(@PathVariable Long id) {
        return examService.deleteExam(id);
    }
}
