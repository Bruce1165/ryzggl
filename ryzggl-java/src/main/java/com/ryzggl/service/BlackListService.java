package com.ryzggl.service;

import com.baomidou.mybatisplus.extension.service.IService;
import com.ryzggl.entity.BlackList;
import com.ryzggl.repository.BlackListRepository;
import com.ryzggl.common.Result;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;

/**
 * BlackList Service
 * 黑名单业务逻辑层
 */
@Service
public class BlackListService implements IService<BlackList> {

    @Autowired
    private BlackListRepository blackListRepository;

    /**
     * Get by ID
     */
    public BlackList getById(Long blackListId) {
        return blackListRepository.getById(blackListId);
    }

    /**
     * Get all blacklist entries
     */
    public List<BlackList> getAll() {
        return blackListRepository.getAll();
    }

    /**
     * Get by worker name
     */
    public List<BlackList> getByWorkerName(String workerName) {
        return blackListRepository.getByWorkerName(workerName);
    }

    /**
     * Get by certificate code
     */
    public List<BlackList> getByCertificateCode(String certificateCode) {
        return blackListRepository.getByCertificateCode(certificateCode);
    }

    /**
     * Get by unit code
     */
    public List<BlackList> getByUnitCode(String unitCode) {
        return blackListRepository.getByUnitCode(unitCode);
    }

    /**
     * Get by black type
     */
    public List<BlackList> getByBlackType(String blackType) {
        return blackListRepository.getByBlackType(blackType);
    }

    /**
     * Get by black status
     */
    public List<BlackList> getByBlackStatus(String blackStatus) {
        return blackListRepository.getByBlackStatus(blackStatus);
    }

    /**
     * Search by keyword
     */
    public List<BlackList> search(String keyword) {
        if (keyword == null || keyword.trim().isEmpty()) {
            return blackListRepository.getAll();
        }
        return blackListRepository.search(keyword);
    }

    /**
     * Check if certificate is blacklisted
     */
    public boolean isCertificateBlacklisted(String certificateCode) {
        return blackListRepository.isCertificateBlacklisted(certificateCode) > 0;
    }

    /**
     * Create blacklist entry
     */
    @Transactional
    public Result<BlackList> createBlackList(BlackList blackList) {
        // Validate required fields
        if (blackList.getWorkerName() == null || blackList.getWorkerName().trim().isEmpty()) {
            return Result.error("人员姓名不能为空");
        }
        if (blackList.getCertificateCode() == null || blackList.getCertificateCode().trim().isEmpty()) {
            return Result.error("证书编号不能为空");
        }
        if (blackList.getBlackType() == null || blackList.getBlackType().trim().isEmpty()) {
            return Result.error("黑名单类型不能为空");
        }
        if (blackList.getBlackStatus() == null || blackList.getBlackStatus().trim().isEmpty()) {
            return Result.error("黑名单状态不能为空");
        }

        // Set default values
        if (blackList.getBlackStatus() == null) {
            blackList.setBlackStatus("有效");
        }

        blackListRepository.insertBlackList(blackList);
        return Result.success(blackList);
    }

    /**
     * Update blacklist entry
     */
    @Transactional
    public Result<BlackList> updateBlackList(BlackList blackList) {
        BlackList existing = getById(blackList.getBlackListId());
        if (existing == null) {
            return Result.error("黑名单记录不存在");
        }

        blackListRepository.updateBlackList(blackList);
        return Result.success(blackList);
    }

    /**
     * Delete blacklist entry
     */
    @Transactional
    public Result<Void> deleteBlackList(Long blackListId) {
        BlackList existing = getById(blackListId);
        if (existing == null) {
            return Result.error("黑名单记录不存在");
        }

        blackListRepository.deleteBlackList(blackListId);
        return Result.success();
    }

    /**
     * Get total count
     */
    public int count() {
        return blackListRepository.count();
    }
}
