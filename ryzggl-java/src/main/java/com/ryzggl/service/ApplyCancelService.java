package com.ryzggl.service;

import com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper;
import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.ryzggl.common.Result;
import com.ryzggl.entity.ApplyCancel;
import com.ryzggl.repository.ApplyCancelRepository;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

/**
 * ApplyCancel Service - 注销申请管理
 * Certificate cancellation application service
 */
@Service
public class ApplyCancelService extends ServiceImpl<ApplyCancelRepository, ApplyCancel> {

    private static final Logger logger = LoggerFactory.getLogger(ApplyCancelService.class);

    private final ApplyCancelRepository applyCancelRepository;

    public ApplyCancelService(ApplyCancelRepository applyCancelRepository) {
        this.applyCancelRepository = applyCancelRepository;
    }

    /**
     * Query apply list with pagination
     */
    public Result<IPage<ApplyCancel>> getApplicationList(Integer current, Integer size,
                                                        String workerName, String psnRegisterNo,
                                                        String applyStatus) {
        Page<ApplyCancel> page = new Page<>(current, size);
        LambdaQueryWrapper<ApplyCancel> wrapper = new LambdaQueryWrapper<>();

        if (workerName != null && !workerName.isEmpty()) {
            wrapper.like(ApplyCancel::getPsnMobilePhone, workerName);
        }
        if (psnRegisterNo != null && !psnRegisterNo.isEmpty()) {
            wrapper.eq(ApplyCancel::getPsnRegisterNo, psnRegisterNo);
        }
        if (applyStatus != null && !applyStatus.isEmpty()) {
            wrapper.eq(ApplyCancel::getApplyStatus, applyStatus);
        }

        IPage<ApplyCancel> result = applyCancelRepository.selectPage(page, wrapper);
        return Result.success(result);
    }

    /**
     * Query apply by ID
     */
    public Result<ApplyCancel> getApplicationById(String id) {
        ApplyCancel applyCancel = applyCancelRepository.selectById(id);
        if (applyCancel == null) {
            return Result.error("注销申请不存在");
        }
        return Result.success(applyCancel);
    }

    /**
     * Create apply
     */
    @Transactional
    public Result<ApplyCancel> createApplication(ApplyCancel applyCancel) {
        applyCancelRepository.insert(applyCancel);
        logger.info("Created cancellation application: ApplyID={}",
                applyCancel.getApplyId());
        return Result.success("注销申请创建成功", applyCancel);
    }

    /**
     * Update apply
     */
    @Transactional
    public Result<ApplyCancel> updateApplication(ApplyCancel applyCancel) {
        ApplyCancel exist = applyCancelRepository.selectById(applyCancel.getApplyId());
        if (exist == null) {
            return Result.error("注销申请不存在");
        }
        applyCancelRepository.updateById(applyCancel, true);
        logger.info("Updated cancellation application: ApplyID={}",
                applyCancel.getApplyId());
        return Result.success("注销申请更新成功", applyCancel);
    }

    /**
     * Delete apply
     */
    @Transactional
    public Result<Boolean> deleteApplication(String id) {
        boolean deleted = applyCancelRepository.deleteById(id);
        if (deleted) {
            logger.info("Deleted cancellation application: ApplyID={}", id);
        }
        return Result.success(deleted);
    }

    /**
     * Get applications by status
     */
    public Result<IPage<ApplyCancel>> getApplicationsByStatus(String status) {
        Page<ApplyCancel> page = new Page<>(1, 1000);
        LambdaQueryWrapper<ApplyCancel> wrapper = new LambdaQueryWrapper<>();
        wrapper.eq(ApplyCancel::getApplyStatus, status);
        IPage<ApplyCancel> result = applyCancelRepository.selectPage(page, wrapper);
        return Result.success(result);
    }

    /**
     * Search applications
     */
    public Result<IPage<ApplyCancel>> searchApplications(String workerName, String psnRegisterNo,
                                                        String cancelReason) {
        Page<ApplyCancel> page = new Page<>(1, 1000);
        LambdaQueryWrapper<ApplyCancel> wrapper = new LambdaQueryWrapper<>();

        if (workerName != null && !workerName.isEmpty()) {
            wrapper.like(ApplyCancel::getPsnMobilePhone, workerName);
        }
        if (psnRegisterNo != null && !psnRegisterNo.isEmpty()) {
            wrapper.eq(ApplyCancel::getPsnRegisterNo, psnRegisterNo);
        }
        if (cancelReason != null && !cancelReason.isEmpty()) {
            wrapper.like(ApplyCancel::getCancelReason, cancelReason);
        }

        IPage<ApplyCancel> result = applyCancelRepository.selectPage(page, wrapper);
        return Result.success(result);
    }
}
