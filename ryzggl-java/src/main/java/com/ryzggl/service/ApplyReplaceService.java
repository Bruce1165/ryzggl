package com.ryzggl.service;

import com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper;
import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.common.Result;
import com.ryzggl.entity.ApplyReplace;
import com.ryzggl.repository.ApplyReplaceRepository;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.time.LocalDateTime;
import java.util.List;

/**
 * ApplyReplace Service
 * 证书补办申请服务层
 *
 * Business logic for certificate replacement applications
 */
@Service
public class ApplyReplaceService {

    private static final Logger logger = LoggerFactory.getLogger(ApplyReplaceService.class);

    @Autowired
    private ApplyReplaceRepository applyReplaceRepository;

    /**
     * Create ApplyReplace
     * 创建证书补办申请
     *
     * @param applyReplace ApplyReplace entity
     * @return Created ApplyReplace
     */
    @Transactional
    public ApplyReplace createReplace(ApplyReplace applyReplace) {
        try {
            // Validate required fields
            validateReplaceApplication(applyReplace);

            // Set timestamps
            applyReplace.setCreateTime(LocalDateTime.now());
            applyReplace.setUpdateTime(LocalDateTime.now());

            // Set default status if not provided
            if (applyReplace.getStatus() == null || applyReplace.getStatus().isEmpty()) {
                applyReplace.setStatus("PENDING");
            }

            applyReplaceRepository.insert(applyReplace);
            logger.info("ApplyReplace created successfully: ApplyID={}", applyReplace.getApplyId());

            return applyReplace;
        } catch (Exception e) {
            logger.error("Error creating ApplyReplace", e);
            throw new RuntimeException("创建证书补办申请失败: " + e.getMessage(), e);
        }
    }

    /**
     * Update ApplyReplace
     * 更新证书补办申请
     *
     * @param applyReplace ApplyReplace entity
     * @return Updated ApplyReplace
     */
    @Transactional
    public ApplyReplace updateReplace(ApplyReplace applyReplace) {
        try {
            if (applyReplace.getApplyId() == null || applyReplace.getApplyId().isEmpty()) {
                throw new IllegalArgumentException("申请ID不能为空");
            }

            // Check if record exists
            ApplyReplace existing = applyReplaceRepository.getByApplyId(applyReplace.getApplyId());
            if (existing == null) {
                throw new IllegalArgumentException("证书补办申请不存在");
            }

            // Set update timestamp
            applyReplace.setUpdateTime(LocalDateTime.now());

            // Perform update
            applyReplaceRepository.updateById(applyReplace);
            logger.info("ApplyReplace updated successfully: ApplyID={}", applyReplace.getApplyId());

            return applyReplace;
        } catch (Exception e) {
            logger.error("Error updating ApplyReplace: ApplyID={}", applyReplace.getApplyId(), e);
            throw new RuntimeException("更新证书补办申请失败: " + e.getMessage(), e);
        }
    }

    /**
     * Delete ApplyReplace
     * 删除证书补办申请
     *
     * @param applyId Apply ID
     * @return true if deleted successfully
     */
    @Transactional
    public boolean deleteReplace(String applyId) {
        try {
            if (applyId == null || applyId.isEmpty()) {
                throw new IllegalArgumentException("申请ID不能为空");
            }

            int result = applyReplaceRepository.deleteById(applyId);
            boolean deleted = result > 0;

            if (deleted) {
                logger.info("ApplyReplace deleted successfully: ApplyID={}", applyId);
            } else {
                logger.warn("ApplyReplace not found for deletion: ApplyID={}", applyId);
            }

            return deleted;
        } catch (Exception e) {
            logger.error("Error deleting ApplyReplace: ApplyID={}", applyId, e);
            throw new RuntimeException("删除证书补办申请失败: " + e.getMessage(), e);
        }
    }

    /**
     * Get ApplyReplace by Apply ID
     * 根据申请ID获取证书补办申请
     *
     * @param applyId Apply ID
     * @return ApplyReplace entity or null
     */
    public ApplyReplace getReplaceById(String applyId) {
        try {
            return applyReplaceRepository.getByApplyId(applyId);
        } catch (Exception e) {
            logger.error("Error getting ApplyReplace: ApplyID={}", applyId, e);
            throw new RuntimeException("获取证书补办申请失败: " + e.getMessage(), e);
        }
    }

    /**
     * Get ApplyReplace list with pagination
     * 获取证书补办申请列表（分页）
     *
     * @param current Current page number (default: 1)
     * @param size Page size (default: 10)
     * @param status Filter by status (optional)
     * @return Page of ApplyReplace entities
     */
    public IPage<ApplyReplace> getReplaceList(int current, int size, String status) {
        try {
            Page<ApplyReplace> page = new Page<>(current, size);

            if (status != null && !status.isEmpty()) {
                return applyReplaceRepository.getByStatus(page, status);
            } else {
                return applyReplaceRepository.selectPage(page, null);
            }
        } catch (Exception e) {
            logger.error("Error getting ApplyReplace list", e);
            throw new RuntimeException("获取证书补办申请列表失败: " + e.getMessage(), e);
        }
    }

    /**
     * Search ApplyReplace by multiple criteria
     * 搜索证书补办申请
     *
     * @param current Current page number
     * @param size Page size
     * @param registerNo Registration number (optional)
     * @param registerCertificateNo Registration certificate number (optional)
     * @param replaceType Replace type (optional)
     * @param status Status (optional)
     * @return Page of ApplyReplace entities
     */
    public IPage<ApplyReplace> searchReplaces(int current, int size,
                                              String registerNo,
                                              String registerCertificateNo,
                                              String replaceType,
                                              String status) {
        try {
            Page<ApplyReplace> page = new Page<>(current, size);
            return applyReplaceRepository.search(page, registerNo, registerCertificateNo,
                    replaceType, status);
        } catch (Exception e) {
            logger.error("Error searching ApplyReplace", e);
            throw new RuntimeException("搜索证书补办申请失败: " + e.getMessage(), e);
        }
    }

    /**
     * Get ApplyReplace by status
     * 根据状态获取证书补办申请
     *
     * @param status Application status
     * @param current Current page number
     * @param size Page size
     * @return Page of ApplyReplace entities
     */
    public IPage<ApplyReplace> getReplacesByStatus(String status, int current, int size) {
        try {
            if (status == null || status.isEmpty()) {
                return getReplaceList(current, size, null);
            }
            Page<ApplyReplace> page = new Page<>(current, size);
            return applyReplaceRepository.getByStatus(page, status);
        } catch (Exception e) {
            logger.error("Error getting ApplyReplace by status: status={}", status, e);
            throw new RuntimeException("获取证书补办申请失败: " + e.getMessage(), e);
        }
    }

    /**
     * Get ApplyReplace by Replace Type
     * 根据补办类型获取证书补办申请
     *
     * @param replaceType Replace type
     * @param current Current page number
     * @param size Page size
     * @return Page of ApplyReplace entities
     */
    public IPage<ApplyReplace> getReplacesByReplaceType(String replaceType, int current, int size) {
        try {
            Page<ApplyReplace> page = new Page<>(current, size);
            return applyReplaceRepository.getByReplaceType(page, replaceType);
        } catch (Exception e) {
            logger.error("Error getting ApplyReplace by replace type: replaceType={}", replaceType, e);
            throw new RuntimeException("获取证书补办申请失败: " + e.getMessage(), e);
        }
    }

    /**
     * Count ApplyReplace by status
     * 根据状态统计证书补办申请数量
     *
     * @param status Application status
     * @return Count
     */
    public long countByStatus(String status) {
        try {
            if (status == null || status.isEmpty()) {
                return applyReplaceRepository.selectCount(null);
            }
            return applyReplaceRepository.countByStatus(status);
        } catch (Exception e) {
            logger.error("Error counting ApplyReplace: status={}", status, e);
            throw new RuntimeException("统计证书补办申请数量失败: " + e.getMessage(), e);
        }
    }

    /**
     * Update ApplyReplace status
     * 更新证书补办申请状态
     *
     * @param applyId Apply ID
     * @param status New status
     * @return Updated ApplyReplace
     */
    @Transactional
    public ApplyReplace updateStatus(String applyId, String status) {
        try {
            ApplyReplace existing = getReplaceById(applyId);
            if (existing == null) {
                throw new IllegalArgumentException("证书补办申请不存在");
            }

            existing.setStatus(status);
            existing.setUpdateTime(LocalDateTime.now());
            applyReplaceRepository.updateById(existing);

            logger.info("ApplyReplace status updated: ApplyID={}, status={}", applyId, status);
            return existing;
        } catch (Exception e) {
            logger.error("Error updating ApplyReplace status: ApplyID={}, status={}", applyId, status, e);
            throw new RuntimeException("更新证书补办申请状态失败: " + e.getMessage(), e);
        }
    }

    /**
     * Validate replace application before creation/update
     * 验证补办申请数据
     *
     * @param applyReplace ApplyReplace entity
     */
    private void validateReplaceApplication(ApplyReplace applyReplace) {
        if (applyReplace.getRegisterNo() == null || applyReplace.getRegisterNo().isEmpty()) {
            throw new IllegalArgumentException("注册号不能为空");
        }

        if (applyReplace.getRegisterCertificateNo() == null ||
                applyReplace.getRegisterCertificateNo().isEmpty()) {
            throw new IllegalArgumentException("注册证书号不能为空");
        }

        if (applyReplace.getReplaceReason() == null || applyReplace.getReplaceReason().isEmpty()) {
            throw new IllegalArgumentException("补办原因不能为空");
        }

        if (applyReplace.getReplaceType() == null || applyReplace.getReplaceType().isEmpty()) {
            throw new IllegalArgumentException("补办类型不能为空");
        }

        // Validate mobile phone format
        if (applyReplace.getPsnMobilePhone() != null &&
                !applyReplace.getPsnMobilePhone().isEmpty()) {
            String phone = applyReplace.getPsnMobilePhone();
            if (!phone.matches("^1[3-9]\\d{9}$")) {
                throw new IllegalArgumentException("手机号格式不正确");
            }
        }

        // Validate email format
        if (applyReplace.getPsnEmail() != null && !applyReplace.getPsnEmail().isEmpty()) {
            String email = applyReplace.getPsnEmail();
            if (!email.matches("^[A-Za-z0-9+_.-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}$")) {
                throw new IllegalArgumentException("邮箱格式不正确");
            }
        }
    }
}
