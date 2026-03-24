package com.ryzggl.service;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.ryzggl.entity.ExamPlan;
import com.ryzggl.entity.ExamSignUp;
import com.ryzggl.repository.ExamPlanRepository;
import com.ryzggl.repository.ExamSignUpRepository;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.Date;
import java.util.List;

/**
 * ExamPlan Service - Exam Plan Business Logic
 */
@Service
public class ExamPlanService extends ServiceImpl<ExamPlanRepository, ExamPlan> {

    private static final Logger logger = LoggerFactory.getLogger(ExamPlanService.class);

    @Autowired
    private ExamPlanRepository examPlanRepository;

    @Autowired
    private ExamSignUpRepository examSignUpRepository;

    /**
     * Create exam plan
     */
    @Transactional
    public ExamPlan createExamPlan(ExamPlan examPlan) {
        examPlan.setCreateTime(new Date());
        examPlan.setStatus("草稿"); // Default status
        examPlan.setIfPublish("否");
        examPlanRepository.insert(examPlan);
        logger.info("Created exam plan: ExamPlanID={}, ExamPlanName={}",
                examPlan.getExamPlanID(), examPlan.getExamPlanName());
        return examPlan;
    }

    /**
     * Update exam plan
     */
    @Transactional
    public ExamPlan updateExamPlan(ExamPlan examPlan) {
        examPlan.setModifyTime(new Date());
        examPlanRepository.updateById(examPlan);
        logger.info("Updated exam plan: ExamPlanID={}", examPlan.getExamPlanID());
        return examPlan;
    }

    /**
     * Delete exam plan
     */
    @Transactional
    public boolean deleteExamPlan(Long examPlanId) {
        boolean deleted = removeById(examPlanId);
        logger.info("Deleted exam plan: ExamPlanID={}, success={}", examPlanId, deleted);
        return deleted;
    }

    /**
     * Get exam plan by ID
     */
    public ExamPlan getExamPlanById(Long examPlanId) {
        return examPlanRepository.selectById(examPlanId);
    }

    /**
     * Get exam plans by post type ID
     */
    public List<ExamPlan> getExamPlansByPostTypeId(Integer postTypeId) {
        return examPlanRepository.selectByPostTypeId(postTypeId);
    }

    /**
     * Get published exam plans
     */
    public List<ExamPlan> getPublishedExamPlans() {
        return examPlanRepository.selectPublished();
    }

    /**
     * Get active exam plans (within sign up period)
     */
    public List<ExamPlan> getActiveExamPlans() {
        return examPlanRepository.selectActive();
    }

    /**
     * Get upcoming exam plans
     */
    public List<ExamPlan> getUpcomingExamPlans(int limit) {
        return examPlanRepository.selectUpcoming(limit);
    }

    /**
     * Publish exam plan
     */
    @Transactional
    public ExamPlan publishExamPlan(Long examPlanId) {
        ExamPlan examPlan = getExamPlanById(examPlanId);
        if (examPlan != null) {
            examPlan.setIfPublish("是");
            examPlan.setStatus("已发布");
            updateExamPlan(examPlan);
            logger.info("Published exam plan: ExamPlanID={}", examPlanId);
        }
        return examPlan;
    }

    /**
     * Close exam plan
     */
    @Transactional
    public ExamPlan closeExamPlan(Long examPlanId) {
        ExamPlan examPlan = getExamPlanById(examPlanId);
        if (examPlan != null) {
            examPlan.setStatus("已结束");
            updateExamPlan(examPlan);
            logger.info("Closed exam plan: ExamPlanID={}", examPlanId);
        }
        return examPlan;
    }

    /**
     * Get exam plan with sign up count
     */
    public ExamPlan getExamPlanWithSignUpCount(Long examPlanId) {
        return examPlanRepository.selectWithSignUpCount(examPlanId);
    }

    /**
     * Get available spots for exam plan
     */
    public int getAvailableSpots(Long examPlanId) {
        ExamPlan examPlan = getExamPlanWithSignUpCount(examPlanId);
        if (examPlan == null) {
            return 0;
        }
        int personLimit = examPlan.getPersonLimit() != null ? examPlan.getPersonLimit() : Integer.MAX_VALUE;
        int signUpCount = countByExamPlanId(examPlanId);
        return Math.max(0, personLimit - signUpCount);
    }

    /**
     * Check if exam plan is fully booked
     */
    public boolean isFullyBooked(Long examPlanId) {
        return getAvailableSpots(examPlanId) <= 0;
    }

    /**
     * Search exam plans
     */
    public List<ExamPlan> searchExamPlans(String examPlanName, Integer postTypeId,
                                          String status) {
        QueryWrapper<ExamPlan> wrapper = new QueryWrapper<>();
        if (examPlanName != null && !examPlanName.isEmpty()) {
            wrapper.like("ExamPlanName", examPlanName);
        }
        if (postTypeId != null) {
            wrapper.eq("PostTypeID", postTypeId);
        }
        if (status != null && !status.isEmpty()) {
            wrapper.eq("Status", status);
        }
        wrapper.orderByDesc("ExamStartDate");
        return examPlanRepository.selectList(wrapper);
    }

    /**
     * Count exam signups by exam plan ID
     */
    private int countByExamPlanId(Long examPlanId) {
        return examSignUpRepository.countByExamPlanId(examPlanId);
    }
}
