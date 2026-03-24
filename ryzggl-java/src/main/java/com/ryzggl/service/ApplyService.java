package com.ryzggl.service;

import com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper;
import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.ryzggl.common.Result;
import com.ryzggl.entity.Apply;
import com.ryzggl.repository.ApplyRepository;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.time.LocalDateTime;

/**
 * Apply Service
 * Application workflow management
 */
@Service
public class ApplyService extends ServiceImpl<ApplyRepository, Apply> {

    private final ApplyRepository applyRepository;

    public ApplyService(ApplyRepository applyRepository) {
        this.applyRepository = applyRepository;
    }

    /**
     * Query apply list with pagination
     */
    public Result<IPage<Apply>> getApplyList(Integer current, Integer size,
                                            String applyName, String applyType, String applyStatus) {
        Page<Apply> page = new Page<>(current, size);
        IPage<Apply> result = applyRepository.selectApplyPage(page, applyName, applyType, applyStatus);
        return Result.success(result);
    }

    /**
     * Query apply by ID
     */
    public Result<Apply> getApplyById(Long id) {
        Apply apply = applyRepository.selectById(id);
        if (apply == null) {
            return Result.error("申请记录不存在");
        }
        return Result.success(apply);
    }

    /**
     * Create apply
     */
    @Transactional
    public Result<Apply> createApply(Apply apply) {
        // Generate apply number
        String applyNo = generateApplyNo();
        apply.setApplyNo(applyNo);

        // Set initial status
        apply.setApplyStatus("未填写");

        apply.setCreateBy("system");
        apply.setUpdateBy("system");

        save(apply);
        return Result.success("申请创建成功", apply);
    }

    /**
     * Update apply
     */
    @Transactional
    public Result<Void> updateApply(Apply apply) {
        Apply existApply = applyRepository.selectById(apply.getId());
        if (existApply == null) {
            return Result.error("申请记录不存在");
        }

        updateById(apply);
        return Result.success("申请更新成功");
    }

    /**
     * Submit apply for review
     */
    @Transactional
    public Result<Void> submitApply(Long id) {
        Apply apply = applyRepository.selectById(id);
        if (apply == null) {
            return Result.error("申请记录不存在");
        }

        apply.setApplyStatus("待确认");
        updateById(apply);
        return Result.success("申请已提交");
    }

    /**
     * Approve apply
     */
    @Transactional
    public Result<Void> approveApply(Long id, String approvalOpinion, String approver) {
        Apply apply = applyRepository.selectById(id);
        if (apply == null) {
            return Result.error("申请记录不存在");
        }

        apply.setApplyStatus("已受理");
        apply.setApprovalOpinion(approvalOpinion);
        apply.setApprover(approver);
        apply.setApprovalTime(LocalDateTime.now().toString());
        updateById(apply);
        return Result.success("申请已审批通过");
    }

    /**
     * Reject apply
     */
    @Transactional
    public Result<Void> rejectApply(Long id, String approvalOpinion, String approver) {
        Apply apply = applyRepository.selectById(id);
        if (apply == null) {
            return Result.error("申请记录不存在");
        }

        apply.setApplyStatus("已驳回");
        apply.setApprovalOpinion(approvalOpinion);
        apply.setApprover(approver);
        apply.setApprovalTime(LocalDateTime.now().toString());
        updateById(apply);
        return Result.success("申请已驳回");
    }

    /**
     * Delete apply
     */
    @Transactional
    public Result<Void> deleteApply(Long id) {
        Apply apply = applyRepository.selectById(id);
        if (apply == null) {
            return Result.error("申请记录不存在");
        }

        // Only allow delete of unfilled or rejected applications
        if (!"未填写".equals(apply.getApplyStatus()) &&
            !"已驳回".equals(apply.getApplyStatus())) {
            return Result.error("只有未填写或已驳回的申请才能删除");
        }

        removeById(id);
        return Result.success("申请已删除");
    }

    /**
     * Generate apply number
     */
    private String generateApplyNo() {
        return "AP" + System.currentTimeMillis();
    }
}
