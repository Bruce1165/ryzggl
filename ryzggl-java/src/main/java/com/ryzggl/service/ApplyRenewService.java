package com.ryzggl.service;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.ryzggl.common.Result;
import com.ryzggl.entity.ApplyRenew;
import com.ryzggl.repository.ApplyRenewRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;

/**
 * ApplyRenew Service - 续期申请业务逻辑层
 */
@Service
public class ApplyRenewService extends ServiceImpl<ApplyRenewRepository, ApplyRenew> {

    @Autowired
    private ApplyRenewRepository applyRenewRepository;

    /**
     * Create new apply renew
     */
    @Transactional
    public Result<ApplyRenew> createApplyRenew(ApplyRenew applyRenew) {
        try {
            applyRenewRepository.insert(applyRenew);
            return Result.success(applyRenew);
        } catch (Exception e) {
            return Result.error("创建续期申请失败: " + e.getMessage());
        }
    }

    /**
     * Update apply renew
     */
    @Transactional
    public Result<ApplyRenew> updateApplyRenew(ApplyRenew applyRenew) {
        try {
            applyRenewRepository.updateById(applyRenew);
            return Result.success(applyRenew);
        } catch (Exception e) {
            return Result.error("更新续期申请失败: " + e.getMessage());
        }
    }

    /**
     * Delete apply renew (soft delete)
     */
    @Transactional
    public Result<Boolean> deleteApplyRenew(Long id) {
        try {
            ApplyRenew applyRenew = new ApplyRenew();
            applyRenew.setId(id);
            applyRenew.setDeleted(1);
            applyRenewRepository.updateById(applyRenew);
            return Result.success(true);
        } catch (Exception e) {
            return Result.error("删除续期申请失败: " + e.getMessage());
        }
    }

    /**
     * Query apply renew by ID
     */
    public Result<ApplyRenew> getApplyRenewById(Long id) {
        ApplyRenew applyRenew = applyRenewRepository.selectById(id);
        if (applyRenew == null) {
            return Result.error("续期申请不存在");
        }
        return Result.success(applyRenew);
    }

    /**
     * Query apply renew list with pagination
     */
    public Result<IPage<ApplyRenew>> getApplyRenewPage(Integer current, Integer size,
                                                         String applyNo, String applicantName,
                                                         String certificateCode, String applyStatus) {
        try {
            Page<ApplyRenew> page = new Page<>(current, size);
            IPage<ApplyRenew> result = applyRenewRepository.selectApplyRenewPage(
                page, applyNo, applicantName, certificateCode, applyStatus);
            return Result.success(result);
        } catch (Exception e) {
            return Result.error("查询续期申请列表失败: " + e.getMessage());
        }
    }

    /**
     * Query apply renews by worker ID
     */
    public Result<List<ApplyRenew>> getApplyRenewByWorkerId(Long workerId) {
        try {
            List<ApplyRenew> list = applyRenewRepository.selectByWorkerId(workerId);
            return Result.success(list);
        } catch (Exception e) {
            return Result.error("查询人员续期申请失败: " + e.getMessage());
        }
    }

    /**
     * Query apply renews by certificate code
     */
    public Result<List<ApplyRenew>> getApplyRenewByCertificateCode(String certificateCode) {
        try {
            List<ApplyRenew> list = applyRenewRepository.selectByCertificateCode(certificateCode);
            return Result.success(list);
        } catch (Exception e) {
            return Result.error("查询证书续期申请失败: " + e.getMessage());
        }
    }

    /**
     * Query apply renews by status
     */
    public Result<List<ApplyRenew>> getApplyRenewByStatus(String status) {
        try {
            List<ApplyRenew> list = applyRenewRepository.selectByStatus(status);
            return Result.success(list);
        } catch (Exception e) {
            return Result.error("查询续期申请状态失败: " + e.getMessage());
        }
    }

    /**
     * Submit apply renew for review
     */
    @Transactional
    public Result<Boolean> submitForReview(Long id, String checkMan, String checkAdvise) {
        try {
            ApplyRenew applyRenew = applyRenewRepository.selectById(id);
            if (applyRenew == null) {
                return Result.error("续期申请不存在");
            }

            applyRenew.setApplyStatus("待确认");
            applyRenew.setCheckMan(checkMan);
            applyRenew.setCheckAdvise(checkAdvise);
            applyRenew.setCheckDate(new java.text.SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new java.util.Date()));

            applyRenewRepository.updateById(applyRenew);
            return Result.success(true);
        } catch (Exception e) {
            return Result.error("提交续期申请失败: " + e.getMessage());
        }
    }

    /**
     * Approve apply renew
     */
    @Transactional
    public Result<Boolean> approve(Long id, String checkMan, String checkAdvise) {
        try {
            ApplyRenew applyRenew = applyRenewRepository.selectById(id);
            if (applyRenew == null) {
                return Result.error("续期申请不存在");
            }

            applyRenew.setApplyStatus("已受理");
            applyRenew.setCheckMan(checkMan);
            applyRenew.setCheckAdvise(checkAdvise);
            applyRenew.setCheckDate(new java.text.SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new java.util.Date()));

            applyRenewRepository.updateById(applyRenew);
            return Result.success(true);
        } catch (Exception e) {
            return Result.error("审批续期申请失败: " + e.getMessage());
        }
    }

    /**
     * Reject apply renew
     */
    @Transactional
    public Result<Boolean> reject(Long id, String checkMan, String checkAdvise) {
        try {
            ApplyRenew applyRenew = applyRenewRepository.selectById(id);
            if (applyRenew == null) {
                return Result.error("续期申请不存在");
            }

            applyRenew.setApplyStatus("已驳回");
            applyRenew.setCheckMan(checkMan);
            applyRenew.setCheckAdvise(checkAdvise);
            applyRenew.setCheckDate(new java.text.SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new java.util.Date()));

            applyRenewRepository.updateById(applyRenew);
            return Result.success(true);
        } catch (Exception e) {
            return Result.error("驳回续期申请失败: " + e.getMessage());
        }
    }
}
