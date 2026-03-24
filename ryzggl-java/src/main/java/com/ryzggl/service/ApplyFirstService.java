package com.ryzggl.service;

import com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper;
import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.ryzggl.common.Result;
import com.ryzggl.entity.ApplyFirst;
import com.ryzggl.repository.ApplyFirstRepository;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

/**
 * ApplyFirst Service - 首次注册申请管理
 * Initial registration application service
 */
@Service
public class ApplyFirstService extends ServiceImpl<ApplyFirstRepository, ApplyFirst> {

    private static final Logger logger = LoggerFactory.getLogger(ApplyFirstService.class);

    private final ApplyFirstRepository applyFirstRepository;

    public ApplyFirstService(ApplyFirstRepository applyFirstRepository) {
        this.applyFirstRepository = applyFirstRepository;
    }

    /**
     * Query apply list with pagination
     */
    public Result<IPage<ApplyFirst>> getApplicationList(Integer current, Integer size,
                                                        String workerName, String idCard,
                                                        String school, String major,
                                                        String applyStatus) {
        Page<ApplyFirst> page = new Page<>(current, size);
        LambdaQueryWrapper<ApplyFirst> wrapper = new LambdaQueryWrapper<>();

        if (workerName != null && !workerName.isEmpty()) {
            wrapper.like(ApplyFirst::getPsnTelephone, workerName);
        }
        if (idCard != null && !idCard.isEmpty()) {
            wrapper.eq(ApplyFirst::getPsnCertificateNo, idCard);
        }
        if (school != null && !school.isEmpty()) {
            wrapper.like(ApplyFirst::getSchool, school);
        }
        if (major != null && !major.isEmpty()) {
            wrapper.like(ApplyFirst::getMajor, major);
        }
        if (applyStatus != null && !applyStatus.isEmpty()) {
            wrapper.eq(ApplyFirst::getApplyStatus, applyStatus);
        }

        IPage<ApplyFirst> result = applyFirstRepository.selectPage(page, wrapper);
        return Result.success(result);
    }

    /**
     * Query apply by ID
     */
    public Result<ApplyFirst> getApplicationById(String id) {
        ApplyFirst applyFirst = applyFirstRepository.selectById(id);
        if (applyFirst == null) {
            return Result.error("首次注册申请不存在");
        }
        return Result.success(applyFirst);
    }

    /**
     * Create apply
     */
    @Transactional
    public Result<ApplyFirst> createApplication(ApplyFirst applyFirst) {
        applyFirstRepository.insert(applyFirst);
        logger.info("Created initial registration application: ApplyID={}",
                applyFirst.getApplyId());
        return Result.success("首次注册申请创建成功", applyFirst);
    }

    /**
     * Update apply
     */
    @Transactional
    public Result<ApplyFirst> updateApplication(ApplyFirst applyFirst) {
        ApplyFirst exist = applyFirstRepository.selectById(applyFirst.getApplyId());
        if (exist == null) {
            return Result.error("首次注册申请不存在");
        }
        applyFirstRepository.updateById(applyFirst, true);
        logger.info("Updated initial registration application: ApplyID={}",
                applyFirst.getApplyId());
        return Result.success("首次注册申请更新成功", applyFirst);
    }

    /**
     * Delete apply
     */
    @Transactional
    public Result<Boolean> deleteApplication(String id) {
        boolean deleted = applyFirstRepository.deleteById(id);
        if (deleted) {
            logger.info("Deleted initial registration application: ApplyID={}", id);
        }
        return Result.success(deleted);
    }

    /**
     * Get applications by status
     */
    public Result<IPage<ApplyFirst>> getApplicationsByStatus(String status) {
        Page<ApplyFirst> page = new Page<>(1, 1000);
        LambdaQueryWrapper<ApplyFirst> wrapper = new LambdaQueryWrapper<>();
        wrapper.eq(ApplyFirst::getApplyStatus, status);
        IPage<ApplyFirst> result = applyFirstRepository.selectPage(page, wrapper);
        return Result.success(result);
    }

    /**
     * Search applications
     */
    public Result<IPage<ApplyFirst>> searchApplications(String workerName, String idCard,
                                                        String school, String major) {
        Page<ApplyFirst> page = new Page<>(1, 1000);
        LambdaQueryWrapper<ApplyFirst> wrapper = new LambdaQueryWrapper<>();

        if (workerName != null && !workerName.isEmpty()) {
            wrapper.like(ApplyFirst::getPsnTelephone, workerName);
        }
        if (idCard != null && !idCard.isEmpty()) {
            wrapper.eq(ApplyFirst::getPsnCertificateNo, idCard);
        }
        if (school != null && !school.isEmpty()) {
            wrapper.like(ApplyFirst::getSchool, school);
        }
        if (major != null && !major.isEmpty()) {
            wrapper.like(ApplyFirst::getMajor, major);
        }

        IPage<ApplyFirst> result = applyFirstRepository.selectPage(page, wrapper);
        return Result.success(result);
    }
}
