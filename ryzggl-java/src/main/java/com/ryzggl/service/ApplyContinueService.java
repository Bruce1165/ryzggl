package com.ryzggl.service;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.ryzggl.common.Result;
import com.ryzggl.entity.ApplyContinue;
import com.ryzggl.repository.ApplyContinueRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;

/**
 * ApplyContinue Service - 延续申请业务逻辑层
 */
@Service
public class ApplyContinueService extends ServiceImpl<ApplyContinueRepository, ApplyContinue> {

    @Autowired
    private ApplyContinueRepository applyContinueRepository;

    /**
     * Create new apply continue
     */
    @Transactional
    public Result<ApplyContinue> createApplyContinue(ApplyContinue applyContinue) {
        try {
            applyContinueRepository.insert(applyContinue);
            return Result.success(applyContinue);
        } catch (Exception e) {
            return Result.error("创建延续申请失败: " + e.getMessage());
        }
    }

    /**
     * Update apply continue
     */
    @Transactional
    public Result<ApplyContinue> updateApplyContinue(ApplyContinue applyContinue) {
        try {
            applyContinueRepository.updateById(applyContinue);
            return Result.success(applyContinue);
        } catch (Exception e) {
            return Result.error("更新延续申请失败: " + e.getMessage());
        }
    }

    /**
     * Delete apply continue (soft delete)
     */
    @Transactional
    public Result<Boolean> deleteApplyContinue(Long id) {
        try {
            ApplyContinue applyContinue = new ApplyContinue();
            applyContinue.setId(id);
            applyContinue.setDeleted(1);
            applyContinueRepository.updateById(applyContinue);
            return Result.success(true);
        } catch (Exception e) {
            return Result.error("删除延续申请失败: " + e.getMessage());
        }
    }

    /**
     * Query apply continue by ID
     */
    public Result<ApplyContinue> getApplyContinueById(Long id) {
        ApplyContinue applyContinue = applyContinueRepository.selectById(id);
        if (applyContinue == null) {
            return Result.error("延续申请不存在");
        }
        return Result.success(applyContinue);
    }

    /**
     * Query apply continue list with pagination
     */
    public Result<IPage<ApplyContinue>> getApplyContinuePage(Integer current, Integer size,
                                                           String applyNo, String applicantName,
                                                           String certificateCode, String applyStatus) {
        try {
            Page<ApplyContinue> page = new Page<>(current, size);
            IPage<ApplyContinue> result = applyContinueRepository.selectApplyContinuePage(
                page, applyNo, applicantName, certificateCode, applyStatus);
            return Result.success(result);
        } catch (Exception e) {
            return Result.error("查询延续申请列表失败: " + e.getMessage());
        }
    }

    /**
     * Query apply continues by worker ID
     */
    public Result<List<ApplyContinue>> getApplyContinuesByWorkerId(Long workerId) {
        try {
            List<ApplyContinue> list = applyContinueRepository.selectByWorkerId(workerId);
            return Result.success(list);
        } catch (Exception e) {
            return Result.error("查询人员延续申请失败: " + e.getMessage());
        }
    }

    /**
     * Query apply continues by certificate code
     */
    public Result<List<ApplyContinue>> getApplyContinuesByCertificateCode(String certificateCode) {
        try {
            List<ApplyContinue> list = applyContinueRepository.selectByCertificateCode(certificateCode);
            return Result.success(list);
        } catch (Exception e) {
            return Result.error("查询证书延续申请失败: " + e.getMessage());
        }
    }

    /**
     * Query apply continues by status
     */
    public Result<List<ApplyContinue>> getApplyContinuesByStatus(String status) {
        try {
            List<ApplyContinue> list = applyContinueRepository.selectByStatus(status);
            return Result.success(list);
        } catch (Exception e) {
            return Result.error("查询延续申请状态失败: " + e.getMessage());
        }
    }

    /**
     * Submit apply continue for review
     */
    @Transactional
    public Result<Boolean> submitForReview(Long id, String checkMan, String checkAdvise) {
        try {
            ApplyContinue applyContinue = applyContinueRepository.selectById(id);
            if (applyContinue == null) {
                return Result.error("延续申请不存在");
            }

            applyContinue.setApplyStatus("待确认");
            applyContinue.setCheckMan(checkMan);
            applyContinue.setCheckAdvise(checkAdvise);
            applyContinue.setCheckDate(new java.text.SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new java.util.Date()));

            applyContinueRepository.updateById(applyContinue);
            return Result.success(true);
        } catch (Exception e) {
            return Result.error("提交延续申请失败: " + e.getMessage());
        }
    }

    /**
     * Approve apply continue
     */
    @Transactional
    public Result<Boolean> approve(Long id, String checkMan, String checkAdvise) {
        try {
            ApplyContinue applyContinue = applyContinueRepository.selectById(id);
            if (applyContinue == null) {
                return Result.error("延续申请不存在");
            }

            applyContinue.setApplyStatus("已受理");
            applyContinue.setCheckMan(checkMan);
            applyContinue.setCheckAdvise(checkAdvise);
            applyContinue.setCheckDate(new java.text.SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new java.util.Date()));

            applyContinueRepository.updateById(applyContinue);
            return Result.success(true);
        } catch (Exception e) {
            return Result.error("审批延续申请失败: " + e.getMessage());
        }
    }

    /**
     * Reject apply continue
     */
    @Transactional
    public Result<Boolean> reject(Long id, String checkMan, String checkAdvise) {
        try {
            ApplyContinue applyContinue = applyContinueRepository.selectById(id);
            if (applyContinue == null) {
                return Result.error("延续申请不存在");
            }

            applyContinue.setApplyStatus("已驳回");
            applyContinue.setCheckMan(checkMan);
            applyContinue.setCheckAdvise(checkAdvise);
            applyContinue.setCheckDate(new java.text.SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new java.util.Date()));

            applyContinueRepository.updateById(applyContinue);
            return Result.success(true);
        } catch (Exception e) {
            return Result.error("驳回延续申请失败: " + e.getMessage());
        }
    }
}