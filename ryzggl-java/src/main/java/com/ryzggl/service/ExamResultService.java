package com.ryzggl.service;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.ryzggl.entity.ExamResult;
import com.ryzggl.repository.ExamResultRepository;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.Date;
import java.util.List;

/**
 * ExamResult Service - Exam Results Business Logic
 */
@Service
public class ExamResultService extends ServiceImpl<ExamResultRepository, ExamResult> {

    private static final Logger logger = LoggerFactory.getLogger(ExamResultService.class);

    @Autowired
    private ExamResultRepository examResultRepository;

    /**
     * Create exam result
     */
    @Transactional
    public ExamResult createResult(ExamResult examResult) {
        examResult.setCreateTime(new Date());
        examResult.setStatus("未发布"); // Default status
        examResultRepository.insert(examResult);
        logger.info("Created exam result: ExamResultID={}, ExamCardID={}",
                examResult.getExamResultID(), examResult.getExamCardID());
        return examResult;
    }

    /**
     * Update exam result
     */
    @Transactional
    public ExamResult updateResult(ExamResult examResult) {
        examResult.setModifyTime(new Date());
        examResultRepository.updateById(examResult);
        logger.info("Updated exam result: ExamResultID={}", examResult.getExamResultID());
        return examResult;
    }

    /**
     * Delete exam result
     */
    @Transactional
    public boolean deleteResult(Long examResultId) {
        boolean deleted = removeById(examResultId);
        logger.info("Deleted exam result: ExamResultID={}, success={}", examResultId, deleted);
        return deleted;
    }

    /**
     * Delete exam results by exam plan ID
     */
    @Transactional
    public int deleteResultsByExamPlanId(Long examPlanId) {
        int deleted = examResultRepository.deleteByExamPlanId(examPlanId);
        logger.info("Deleted exam results by exam plan: ExamPlanID={}, count={}", examPlanId, deleted);
        return deleted;
    }

    /**
     * Get exam result by ID
     */
    public ExamResult getResultById(Long examResultId) {
        return examResultRepository.selectById(examResultId);
    }

    /**
     * Get exam result by exam card ID
     */
    public ExamResult getResultByExamCardId(String examCardId) {
        return examResultRepository.selectByExamCardId(examCardId);
    }

    /**
     * Get exam results by exam plan ID
     */
    public List<ExamResult> getResultsByExamPlanId(Long examPlanId) {
        return examResultRepository.selectByExamPlanId(examPlanId);
    }

    /**
     * Get exam results by worker ID
     */
    public List<ExamResult> getResultsByWorkerId(Long workerId) {
        return examResultRepository.selectByWorkerId(workerId);
    }

    /**
     * Get exam results by status
     */
    public List<ExamResult> getResultsByStatus(String status) {
        return examResultRepository.selectByStatus(status);
    }

    /**
     * Get exam results by result (pass/fail)
     */
    public List<ExamResult> getResultsByExamResult(String examResult) {
        return examResultRepository.selectByResult(examResult);
    }

    /**
     * Pass an exam result
     */
    @Transactional
    public ExamResult passResult(Long examResultId) {
        ExamResult examResult = getResultById(examResultId);
        if (examResult != null) {
            examResult.setExamResult("合格");
            updateResult(examResult);
            logger.info("Passed exam result: ExamResultID={}", examResultId);
        }
        return examResult;
    }

    /**
     * Fail an exam result
     */
    @Transactional
    public ExamResult failResult(Long examResultId) {
        ExamResult examResult = getResultById(examResultId);
        if (examResult != null) {
            examResult.setExamResult("不合格");
            updateResult(examResult);
            logger.info("Failed exam result: ExamResultID={}", examResultId);
        }
        return examResult;
    }

    /**
     * Publish exam results for an exam plan
     */
    @Transactional
    public int publishResultsForExamPlan(Long examPlanId) {
        int published = examResultRepository.publishByExamPlanId(examPlanId);
        logger.info("Published exam results: ExamPlanID={}, count={}", examPlanId, published);
        return published;
    }

    /**
     * Get pass rate for exam plan
     */
    public double getPassRate(Long examPlanId) {
        int total = examResultRepository.countByExamPlanId(examPlanId);
        int passed = examResultRepository.countPassedByExamPlanId(examPlanId);
        if (total == 0) {
            return 0.0;
        }
        return (double) passed / total * 100;
    }

    /**
     * Get pass statistics for exam plan
     */
    public int[] getPassStatistics(Long examPlanId) {
        int total = examResultRepository.countByExamPlanId(examPlanId);
        int passed = examResultRepository.countPassedByExamPlanId(examPlanId);
        int failed = total - passed;
        return new int[]{total, passed, failed};
    }

    /**
     * Search exam results
     */
    public List<ExamResult> searchResults(String examCardId, Long examPlanId,
                                         String status, String examResult) {
        QueryWrapper<ExamResult> wrapper = new QueryWrapper<>();
        if (examCardId != null && !examCardId.isEmpty()) {
            wrapper.like("ExamCardID", examCardId);
        }
        if (examPlanId != null) {
            wrapper.eq("ExamPlanID", examPlanId);
        }
        if (status != null && !status.isEmpty()) {
            wrapper.eq("Status", status);
        }
        if (examResult != null && !examResult.isEmpty()) {
            wrapper.eq("ExamResult", examResult);
        }
        wrapper.orderByDesc("CreateTime");
        return examResultRepository.selectList(wrapper);
    }
}
