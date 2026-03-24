package com.ryzggl.service;

import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.ryzggl.common.Result;
import com.ryzggl.entity.Exam;
import com.ryzggl.repository.ExamRepository;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;

/**
 * Exam Service
 * Examination management
 */
@Service
public class ExamService extends ServiceImpl<ExamRepository, Exam> {

    private final ExamRepository examRepository;

    public ExamService(ExamRepository examRepository) {
        this.examRepository = examRepository;
    }

    /**
     * Query exam list with pagination
     */
    public Result<IPage<Exam>> getExamList(Integer current, Integer size,
                                          String examName, String qualificationName, String status) {
        Page<Exam> page = new Page<>(current, size);
        IPage<Exam> result = examRepository.selectExamPage(page, examName, qualificationName, status);
        return Result.success(result);
    }

    /**
     * Query exam by ID
     */
    public Result<Exam> getExamById(Long id) {
        Exam exam = examRepository.selectById(id);
        if (exam == null) {
            return Result.error("考试不存在");
        }
        return Result.success(exam);
    }

    /**
     * Query available exams
     */
    public Result<List<Exam>> getAvailableExams() {
        List<Exam> exams = examRepository.selectAvailableExams();
        return Result.success(exams);
    }

    /**
     * Create exam
     */
    @Transactional
    public Result<Exam> createExam(Exam exam) {
        // Generate exam code
        String examCode = generateExamCode();
        exam.setExamCode(examCode);

        // Set initial status
        exam.setStatus("未开始");

        exam.setCreateBy("system");
        exam.setUpdateBy("system");

        // Initialize counts
        exam.setSignUpCount(0);
        exam.setActualCount(0);
        exam.setPassCount(0);

        save(exam);
        return Result.success("考试创建成功", exam);
    }

    /**
     * Update exam
     */
    @Transactional
    public Result<Void> updateExam(Exam exam) {
        Exam existExam = examRepository.selectById(exam.getId());
        if (existExam == null) {
            return Result.error("考试不存在");
        }

        updateById(exam);
        return Result.success("考试更新成功");
    }

    /**
     * Delete exam
     */
    @Transactional
    public Result<Void> deleteExam(Long id) {
        Exam exam = examRepository.selectById(id);
        if (exam == null) {
            return Result.error("考试不存在");
        }

        // Only allow delete of exams that haven't started
        if (!"未开始".equals(exam.getStatus())) {
            return Result.error("只有未开始的考试才能删除");
        }

        removeById(id);
        return Result.success("考试已删除");
    }

    /**
     * Generate exam code
     */
    private String generateExamCode() {
        return "EXAM" + System.currentTimeMillis();
    }
}
