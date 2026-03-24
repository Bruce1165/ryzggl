package com.ryzggl.service;

import com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper;
import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.ryzggl.common.Result;
import com.ryzggl.entity.ApplyChange;
import com.ryzggl.repository.ApplyChangeRepository;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

/**
 * ApplyChange Service - 变更申请管理
 * Certificate change application service with from/to transfer fields
 */
@Service
public class ApplyChangeService extends ServiceImpl<ApplyChangeRepository, ApplyChange> {

    private static final Logger logger = LoggerFactory.getLogger(ApplyChangeService.class);

    private final ApplyChangeRepository applyChangeRepository;

    public ApplyChangeService(ApplyChangeRepository applyChangeRepository) {
        this.applyChangeRepository = applyChangeRepository;
    }

    /**
     * Query apply list with pagination
     */
    public Result<IPage<ApplyChange>> getApplicationList(Integer current, Integer size,
                                                        String workerName, String psnRegisterNo,
                                                        String changeReason, String ifOutside) {
        Page<ApplyChange> page = new Page<>(current, size);
        LambdaQueryWrapper<ApplyChange> wrapper = new LambdaQueryWrapper<>();

        if (workerName != null && !workerName.isEmpty()) {
            wrapper.like(ApplyChange::getPsnMobilePhone, workerName);
        }
        if (psnRegisterNo != null && !psnRegisterNo.isEmpty()) {
            wrapper.eq(ApplyChange::getPsnRegisterNo, psnRegisterNo);
        }
        if (changeReason != null && !changeReason.isEmpty()) {
            wrapper.like(ApplyChange::getChangeReason, changeReason);
        }
        if (ifOutside != null) {
            wrapper.eq(ApplyChange::getIfOutside, ifOutside);
        }

        IPage<ApplyChange> result = applyChangeRepository.selectPage(page, wrapper);
        return Result.success(result);
    }

    /**
     * Query apply by ID
     */
    public Result<ApplyChange> getApplicationById(String id) {
        ApplyChange applyChange = applyChangeRepository.selectById(id);
        if (applyChange == null) {
            return Result.error("变更申请不存在");
        }
        return Result.success(applyChange);
    }

    /**
     * Create apply
     */
    @Transactional
    public Result<ApplyChange> createApplication(ApplyChange applyChange) {
        applyChangeRepository.insert(applyChange);
        logger.info("Created change application: ApplyID={}",
                applyChange.getApplyId());
        return Result.success("变更申请创建成功", applyChange);
    }

    /**
     * Update apply
     */
    @Transactional
    public Result<ApplyChange> updateApplication(ApplyChange applyChange) {
        ApplyChange exist = applyChangeRepository.selectById(applyChange.getApplyId());
        if (exist == null) {
            return Result.error("变更申请不存在");
        }
        applyChangeRepository.updateById(applyChange, true);
        logger.info("Updated change application: ApplyID={}",
                applyChange.getApplyId());
        return Result.success("变更申请更新成功", applyChange);
    }

    /**
     * Delete apply
     */
    @Transactional
    public Result<Boolean> deleteApplication(String id) {
        boolean deleted = applyChangeRepository.deleteById(id);
        if (deleted) {
            logger.info("Deleted change application: ApplyID={}", id);
        }
        return Result.success(deleted);
    }

    /**
     * Get applications by status
     */
    public Result<IPage<ApplyChange>> getApplicationsByStatus(String status) {
        Page<ApplyChange> page = new Page<>(1, 1000);
        LambdaQueryWrapper<ApplyChange> wrapper = new LambdaQueryWrapper<>();
        wrapper.eq(ApplyChange::getApplyStatus, status);
        IPage<ApplyChange> result = applyChangeRepository.selectPage(page, wrapper);
        return Result.success(result);
    }

    /**
     * Search applications
     */
    public Result<IPage<ApplyChange>> searchApplications(String workerName, String psnRegisterNo,
                                                        String changeReason, String fromEntCity,
                                                        String toEntCity, String fromPsnSex,
                                                        String toPsnSex) {
        Page<ApplyChange> page = new Page<>(1, 1000);
        LambdaQueryWrapper<ApplyChange> wrapper = new LambdaQueryWrapper<>();

        if (workerName != null && !workerName.isEmpty()) {
            wrapper.like(ApplyChange::getPsnMobilePhone, workerName);
        }
        if (psnRegisterNo != null && !psnRegisterNo.isEmpty()) {
            wrapper.eq(ApplyChange::getPsnRegisterNo, psnRegisterNo);
        }
        if (changeReason != null && !changeReason.isEmpty()) {
            wrapper.like(ApplyChange::getChangeReason, changeReason);
        }
        if (fromEntCity != null && !fromEntCity.isEmpty()) {
            wrapper.eq(ApplyChange::getFromEntCity, fromEntCity);
        }
        if (toEntCity != null && !toEntCity.isEmpty()) {
            wrapper.eq(ApplyChange::getToEntCity, toEntCity);
        }
        if (fromPsnSex != null && !fromPsnSex.isEmpty()) {
            wrapper.eq(ApplyChange::getFromPsnSex, fromPsnSex);
        }
        if (toPsnSex != null && !toPsnSex.isEmpty()) {
            wrapper.eq(ApplyChange::getToPsnSex, toPsnSex);
        }

        IPage<ApplyChange> result = applyChangeRepository.selectPage(page, wrapper);
        return Result.success(result);
    }
}
