package com.ryzggl.service;

import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.ryzggl.entity.Apply;
import com.ryzggl.repository.ApplyRepository;
import com.ryzggl.common.Result;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;

/**
 * Apply Service - Business Logic Layer
 * Maps to: ApplyDAL.cs logic and workflow management
 */
@Service
@Transactional
public class ApplyService extends ServiceImpl<ApplyRepository, Apply> {

    private static final Logger log = LoggerFactory.getLogger(ApplyService.class);

    /**
     * Create new application
     * Maps to: ApplyDAL.AddApply
     */
    public Result<Void> createApply(Apply apply) {
        log.info("Creating new application for worker: {}", apply.getWorkerId());

        boolean success = save(apply);

        if (success) {
            log.info("Application created successfully");
            return Result.success();
        } else {
            log.error("Failed to create application");
            return Result.error("申请创建失败");
        }
    }

    /**
     * Get application by ID
     */
    public Result<Apply> getApplyById(Long applyId) {
        log.debug("Getting application: {}", applyId);
        Apply apply = getById(applyId);

        if (apply == null) {
            return Result.error("申请不存在");
        }

        return Result.success(apply);
    }

    /**
     * List applications with pagination
     * Maps to: ApplyDAL.GetApplyList
     */
    public Result<List<Apply>> listApplications(Long workerId, String status) {
        log.debug("Listing applications: workerId={}, status={}", workerId, status);

        if (workerId != null) {
            List<Apply> applies = lambdaQuery()
                    .eq(Apply::getWorkerId, workerId)
                    .eq(status != null, Apply::getApplyStatus, status)
                    .orderByDesc(Apply::getCreateTime)
                    .list();
            return Result.success(applies);
        } else {
            List<Apply> applies = lambdaQuery()
                    .eq(status != null, Apply::getApplyStatus, status)
                    .orderByDesc(Apply::getCreateTime)
                    .list();
            return Result.success(applies);
        }
    }

    /**
     * Submit application for review
     * Maps to: ApplyDAL.SubmitApply
     */
    public Result<Void> submitApply(Long applyId) {
        log.info("Submitting application: {}", applyId);

        Apply apply = getById(applyId);
        if (apply == null) {
            return Result.error("申请不存在");
        }

        if (!"未填写".equals(apply.getApplyStatus())) {
            return Result.error("只有未填写的申请才能提交");
        }

        apply.setApplyStatus("待确认");
        boolean success = updateById(apply);

        if (success) {
            log.info("Application submitted successfully");
            return Result.success();
        } else {
            return Result.error("申请提交失败");
        }
    }

    /**
     * Approve application
     * Maps to: ApplyDAL.ApproveApply
     */
    public Result<Void> approveApply(Long applyId, String checkAdvise) {
        log.info("Approving application: {}, advise: {}", applyId, checkAdvise);

        Apply apply = getById(applyId);
        if (apply == null) {
            return Result.error("申请不存在");
        }

        if (!"待确认".equals(apply.getApplyStatus())) {
            return Result.error("只有待确认的申请才能通过");
        }

        apply.setApplyStatus("已受理");
        apply.setCheckAdvise(checkAdvise);
        boolean success = updateById(apply);

        if (success) {
            log.info("Application approved successfully");
            return Result.success();
        } else {
            return Result.error("申请审核失败");
        }
    }

    /**
     * Reject application
     * Maps to: ApplyDAL.RejectApply
     */
    public Result<Void> rejectApply(Long applyId, String reason) {
        log.info("Rejecting application: {}, reason: {}", applyId, reason);

        Apply apply = getById(applyId);
        if (apply == null) {
            return Result.error("申请不存在");
        }

        if (!"待确认".equals(apply.getApplyStatus())) {
            return Result.error("只有待确认的申请才能驳回");
        }

        apply.setApplyStatus("已驳回");
        apply.setCheckAdvise(reason);
        boolean success = updateById(apply);

        if (success) {
            log.info("Application rejected successfully");
            return Result.success();
        } else {
            return Result.error("申请驳回失败");
        }
    }

    /**
     * Approve application with checker name
     * Maps to: ApplyDAL.ApproveApply with CHECKMAN
     */
    public Result<Void> approveApplication(Long applyId, String checkMan, String checkAdvise) {
        log.info("Approving application: {}, checkMan: {}, advise: {}", applyId, checkMan, checkAdvise);

        Apply apply = getById(applyId);
        if (apply == null) {
            return Result.error("申请不存在");
        }

        if (!"待确认".equals(apply.getApplyStatus())) {
            return Result.error("只有待确认的申请才能通过");
        }

        apply.setApplyStatus("已受理");
        apply.setCheckMan(checkMan);
        apply.setCheckAdvise(checkAdvise);
        boolean success = updateById(apply);

        if (success) {
            log.info("Application approved successfully");
            return Result.success();
        } else {
            return Result.error("申请审核失败");
        }
    }

    /**
     * Reject application with rejection reason
     */
    public Result<Void> rejectApplication(Long applyId, String rejectionReason) {
        log.info("Rejecting application: {}, reason: {}", applyId, rejectionReason);

        Apply apply = getById(applyId);
        if (apply == null) {
            return Result.error("申请不存在");
        }

        if (!"待确认".equals(apply.getApplyStatus())) {
            return Result.error("只有待确认的申请才能驳回");
        }

        apply.setApplyStatus("已驳回");
        apply.setCheckAdvise(rejectionReason);
        boolean success = updateById(apply);

        if (success) {
            log.info("Application rejected successfully");
            return Result.success();
        } else {
            return Result.error("申请驳回失败");
        }
    }

    /**
     * Get pending applications for approval
     * Maps to: CheckTask.GetPendingApplications
     */
    public Result<List<Apply>> getPendingApplications() {
        log.debug("Getting pending applications");

        List<Apply> applies = lambdaQuery()
                .eq(Apply::getApplyStatus, "待确认")
                .orderByAsc(Apply::getCreateTime)
                .list();

        return Result.success(applies);
    }

    /**
     * Get applications by status
     * Maps to: ApplyDAL.GetApplyList filtering by status
     */
    public Result<List<Apply>> getApplicationsByStatus(String status) {
        log.debug("Getting applications by status: {}", status);

        List<Apply> applies = lambdaQuery()
                .eq(Apply::getApplyStatus, status)
                .orderByDesc(Apply::getCreateTime)
                .list();

        return Result.success(applies);
    }
}
