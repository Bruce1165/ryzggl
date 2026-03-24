package com.ryzggl.service;

import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.ryzggl.entity.EnterpriseBaseInfo;
import com.ryzggl.repository.EnterpriseBaseInfoRepository;
import com.ryzggl.common.Result;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;

/**
 * EnterpriseBaseInfo Service - Business Logic Layer
 * Maps to: EnterpriseBaseInfoDAL.cs logic and workflow management
 */
@Service
@Transactional
public class EnterpriseBaseInfoService extends ServiceImpl<EnterpriseBaseInfoRepository, EnterpriseBaseInfo> {

    private static final Logger log = LoggerFactory.getLogger(EnterpriseBaseInfoService.class);

    /**
     * Create enterprise base information
     * Maps to: EnterpriseBaseInfoDAL.AddEnterpriseBaseInfo
     */
    public Result<Void> createEnterpriseBaseInfo(EnterpriseBaseInfo enterpriseBaseInfo) {
        log.info("Creating enterprise base information: {}", enterpriseBaseInfo.getEntName());

        boolean success = save(enterpriseBaseInfo);

        if (success) {
            log.info("Enterprise base info created successfully");
            return Result.success();
        } else {
            log.error("Failed to create enterprise base info");
            return Result.error("企业信息创建失败");
        }
    }

    /**
     * Get enterprise base information by ID
     */
    public Result<EnterpriseBaseInfo> getEnterpriseBaseInfoById(Long enterpriseBaseInfoId) {
        log.debug("Getting enterprise base info: {}", enterpriseBaseInfoId);
        EnterpriseBaseInfo enterpriseBaseInfo = getById(enterpriseBaseInfoId);

        if (enterpriseBaseInfo == null) {
            return Result.error("企业信息不存在");
        }

        return Result.success(enterpriseBaseInfo);
    }

    /**
     * List enterprise base information
     * Maps to: EnterpriseBaseInfoDAL.GetEnterpriseBaseInfoList
     */
    public Result<List<EnterpriseBaseInfo>> listEnterpriseBaseInfo(String entType, String entGrade, String entStatus, String entProvince) {
        log.debug("Listing enterprise base info: entType={}, entGrade={}, entStatus={}, entProvince={}", entType, entGrade, entStatus, entProvince);

        com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<EnterpriseBaseInfo> queryWrapper =
                new com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper<>();

        if (entType != null) {
            queryWrapper.eq(EnterpriseBaseInfo::getEntType, entType);
        }
        if (entGrade != null) {
            queryWrapper.eq(EnterpriseBaseInfo::getEntGrade, entGrade);
        }
        if (entStatus != null) {
            queryWrapper.eq(EnterpriseBaseInfo::getEntStatus, entStatus);
        }
        if (entProvince != null) {
            queryWrapper.eq(EnterpriseBaseInfo::getEntProvince, entProvince);
        }

        queryWrapper.orderByAsc(EnterpriseBaseInfo::getEntName);

        List<EnterpriseBaseInfo> enterpriseBaseInfos = list(queryWrapper);
        return Result.success(enterpriseBaseInfos);
    }

    /**
     * Get enterprise by organization code
     */
    public Result<EnterpriseBaseInfo> getEnterpriseByCode(String entOrganizationsCode) {
        log.debug("Getting enterprise by code: {}", entOrganizationsCode);
        EnterpriseBaseInfo enterpriseBaseInfo = baseMapper.findByEntOrganizationsCode(entOrganizationsCode);

        if (enterpriseBaseInfo == null) {
            return Result.error("企业不存在");
        }

        return Result.success(enterpriseBaseInfo);
    }

    /**
     * Get enterprises by name
     */
    public Result<List<EnterpriseBaseInfo>> getEnterprisesByName(String entName) {
        log.debug("Getting enterprises by name: {}", entName);

        List<EnterpriseBaseInfo> enterpriseBaseInfos = baseMapper.findByEntName(entName);

        return Result.success(enterpriseBaseInfos);
    }

    /**
     * Get all enterprises
     */
    public Result<List<EnterpriseBaseInfo>> getAllEnterprises() {
        log.debug("Getting all enterprises");

        List<EnterpriseBaseInfo> enterpriseBaseInfos = baseMapper.findAllEnterprises();

        return Result.success(enterpriseBaseInfos);
    }

    /**
     * Get enterprises by type
     */
    public Result<List<EnterpriseBaseInfo>> getEnterprisesByType(String entType) {
        log.debug("Getting enterprises by type: {}", entType);

        List<EnterpriseBaseInfo> enterpriseBaseInfos = baseMapper.findByEntType(entType);

        return Result.success(enterpriseBaseInfos);
    }

    /**
     * Get enterprises by grade
     */
    public Result<List<EnterpriseBaseInfo>> getEnterprisesByGrade(String entGrade) {
        log.debug("Getting enterprises by grade: {}", entGrade);

        List<EnterpriseBaseInfo> enterpriseBaseInfos = baseMapper.findByEntGrade(entGrade);

        return Result.success(enterpriseBaseInfos);
    }

    /**
     * Get enterprises by status
     */
    public Result<List<EnterpriseBaseInfo>> getEnterprisesByStatus(String entStatus) {
        log.debug("Getting enterprises by status: {}", entStatus);

        List<EnterpriseBaseInfo> enterpriseBaseInfos = baseMapper.findByEntStatus(entStatus);

        return Result.success(enterpriseBaseInfos);
    }

    /**
     * Get enterprises by province
     */
    public Result<List<EnterpriseBaseInfo>> getEnterprisesByProvince(String entProvince) {
        log.debug("Getting enterprises by province: {}", entProvince);

        List<EnterpriseBaseInfo> enterpriseBaseInfos = baseMapper.findByEntProvince(entProvince);

        return Result.success(enterpriseBaseInfos);
    }

    /**
     * Update enterprise base information
     */
    public Result<Void> updateEnterpriseBaseInfo(EnterpriseBaseInfo enterpriseBaseInfo) {
        log.info("Updating enterprise base information: {}", enterpriseBaseInfo.getId());

        boolean success = updateById(enterpriseBaseInfo);

        if (success) {
            log.info("Enterprise base info updated successfully");
            return Result.success();
        } else {
            log.error("Failed to update enterprise base info");
            return Result.error("企业信息更新失败");
        }
    }

    /**
     * Delete enterprise base information
     */
    public Result<Void> deleteEnterpriseBaseInfo(Long enterpriseBaseInfoId) {
        log.info("Deleting enterprise base information: {}", enterpriseBaseInfoId);

        boolean success = removeById(enterpriseBaseInfoId);

        if (success) {
            log.info("Enterprise base info deleted successfully");
            return Result.success();
        } else {
            log.error("Failed to delete enterprise base info");
            return Result.error("企业信息删除失败");
        }
    }

    /**
     * Get recent registered enterprises
     */
    public Result<List<EnterpriseBaseInfo>> getRecentEnterprises(int days) {
        log.debug("Getting recent registered enterprises (last {} days)", days);

        List<EnterpriseBaseInfo> enterpriseBaseInfos = baseMapper.findRecent(days);

        return Result.success(enterpriseBaseInfos);
    }
}
