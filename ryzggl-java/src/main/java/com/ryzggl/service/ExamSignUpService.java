package com.ryzggl.service;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.ryzggl.entity.ExamSignUp;
import com.ryzggl.repository.ExamSignUpRepository;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.Date;
import java.util.List;

/**
 * ExamSignUp Service - Exam Registration Business Logic
 */
@Service
public class ExamSignUpService extends ServiceImpl<ExamSignUpRepository, ExamSignUp> {

    private static final Logger logger = LoggerFactory.getLogger(ExamSignUpService.class);

    @Autowired
    private ExamSignUpRepository examSignUpRepository;

    /**
     * Create exam sign up
     */
    @Transactional
    public ExamSignUp createSignUp(ExamSignUp signUp) {
        signUp.setCreateTime(new Date());
        signUp.setSignUpDate(new Date());
        signUp.setStatus("待审核"); // Default status
        examSignUpRepository.insert(signUp);
        logger.info("Created exam sign up: ExamSignUpID={}, WorkerName={}",
                signUp.getExamSignUpID(), signUp.getWorkerName());
        return signUp;
    }

    /**
     * Update exam sign up
     */
    @Transactional
    public ExamSignUp updateSignUp(ExamSignUp signUp) {
        signUp.setModifyTime(new Date());
        examSignUpRepository.updateById(signUp);
        logger.info("Updated exam sign up: ExamSignUpID={}", signUp.getExamSignUpID());
        return signUp;
    }

    /**
     * Delete exam sign up
     */
    @Transactional
    public boolean deleteSignUp(Long examSignUpId) {
        boolean deleted = removeById(examSignUpId);
        logger.info("Deleted exam sign up: ExamSignUpID={}, success={}", examSignUpId, deleted);
        return deleted;
    }

    /**
     * Get exam sign up by ID
     */
    public ExamSignUp getSignUpById(Long examSignUpId) {
        return examSignUpRepository.selectById(examSignUpId);
    }

    /**
     * Get exam signups by exam plan ID
     */
    public List<ExamSignUp> getSignUpsByExamPlanId(Long examPlanId) {
        return examSignUpRepository.selectByExamPlanId(examPlanId);
    }

    /**
     * Get exam signups by worker ID
     */
    public List<ExamSignUp> getSignUpsByWorkerId(Long workerId) {
        return examSignUpRepository.selectByWorkerId(workerId);
    }

    /**
     * Approve exam sign up
     */
    @Transactional
    public ExamSignUp approveSignUp(Long examSignUpId, String checkMan, String checkResult) {
        ExamSignUp signUp = getSignUpById(examSignUpId);
        if (signUp != null) {
            signUp.setCheckMan(checkMan);
            signUp.setCheckResult(checkResult);
            signUp.setCheckDate(new Date());
            signUp.setStatus("通过".equals(checkResult) ? "已通过" : "已驳回");
            updateSignUp(signUp);
            logger.info("Approved exam sign up: ExamSignUpID={}, CheckMan={}, CheckResult={}",
                    examSignUpId, checkMan, checkResult);
        }
        return signUp;
    }

    /**
     * Confirm payment for exam sign up
     */
    @Transactional
    public ExamSignUp confirmPayment(Long examSignUpId, String payConfirmMan, String payConfirmRult) {
        ExamSignUp signUp = getSignUpById(examSignUpId);
        if (signUp != null) {
            signUp.setPayConfirmMan(payConfirmMan);
            signUp.setPayConfirmRult(payConfirmRult);
            signUp.setPayConfirmDate(new Date());
            if ("通过".equals(payConfirmRult)) {
                signUp.setStatus("已缴费");
            }
            updateSignUp(signUp);
            logger.info("Confirmed payment for exam sign up: ExamSignUpID={}, PayConfirmMan={}",
                    examSignUpId, payConfirmMan);
        }
        return signUp;
    }

    /**
     * Get pending check exam signups
     */
    public List<ExamSignUp> getPendingChecks() {
        return examSignUpRepository.selectPendingChecks();
    }

    /**
     * Get exam signups by status
     */
    public List<ExamSignUp> getSignUpsByStatus(String status) {
        return examSignUpRepository.selectByStatus(status);
    }

    /**
     * Count exam signups by exam plan ID
     */
    public int countByExamPlanId(Long examPlanId) {
        return examSignUpRepository.countByExamPlanId(examPlanId);
    }

    /**
     * Search exam signups
     */
    public List<ExamSignUp> searchSignUps(String workerName, String certificateCode,
                                           Long examPlanId, String status) {
        QueryWrapper<ExamSignUp> wrapper = new QueryWrapper<>();
        if (workerName != null && !workerName.isEmpty()) {
            wrapper.like("WorkerName", workerName);
        }
        if (certificateCode != null && !certificateCode.isEmpty()) {
            wrapper.like("CertificateCode", certificateCode);
        }
        if (examPlanId != null) {
            wrapper.eq("ExamPlanID", examPlanId);
        }
        if (status != null && !status.isEmpty()) {
            wrapper.eq("Status", status);
        }
        wrapper.orderByDesc("SignUpDate");
        return examSignUpRepository.selectList(wrapper);
    }
}
