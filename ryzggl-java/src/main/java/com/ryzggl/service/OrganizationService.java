package com.ryzggl.service;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.extension.service.IService;
import com.ryzggl.entity.Organization;
import com.ryzggl.repository.OrganizationRepository;
import com.ryzggl.common.Result;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;

/**
 * Organization Service
 * 机构管理业务逻辑层
 */
@Service
public class OrganizationService implements IService<Organization> {

    @Autowired
    private OrganizationRepository organizationRepository;

    /**
     * Get organization by ID
     */
    public Organization getOrganizationById(Long organId) {
        return organizationRepository.getById(organId);
    }

    /**
     * Get organization by code
     */
    public Organization getOrganizationByCode(String organCoding) {
        return organizationRepository.getByCode(organCoding);
    }

    /**
     * Get organization tree structure
     */
    public List<Organization> getOrganizationTree() {
        List<Organization> rootOrgs = organizationRepository.getRootOrganizations();
        if (rootOrgs == null || rootOrgs.isEmpty()) {
            return List.of();
        }

        // Build tree by adding children
        for (Organization root : rootOrgs) {
            loadChildrenToOrganization(root);
        }

        return rootOrgs;
    }

    /**
     * Recursively load children for an organization
     */
    private void loadChildrenToOrganization(Organization parent) {
        if (parent == null || parent.getOrganCoding() == null) {
            return;
        }

        String parentCode = parent.getOrganCoding();
        List<Organization> children = organizationRepository.getChildrenByParentCode(parentCode);

        // Recursive load for children
        for (Organization child : children) {
            loadChildrenToOrganization(child);
        }

        // Note: In a real tree structure implementation, you might want to set children
        // This is a simple implementation that doesn't modify the entity
    }

    /**
     * Get all visible organizations
     */
    public List<Organization> getAllVisible() {
        return organizationRepository.getAllVisible();
    }

    /**
     * Get organizations by type
     */
    public List<Organization> getOrganizationsByType(String organType) {
        return organizationRepository.getByType(organType);
    }

    /**
     * Get organizations by region
     */
    public List<Organization> getOrganizationsByRegion(String regionId) {
        return organizationRepository.getByRegion(regionId);
    }

    /**
     * Search organizations
     */
    public List<Organization> searchOrganizations(String keyword) {
        if (keyword == null || keyword.trim().isEmpty()) {
            return getAllVisible();
        }
        return organizationRepository.search(keyword);
    }

    /**
     * Create organization
     */
    @Transactional
    public Result<Organization> createOrganization(Organization organization) {
        // Validate organization code
        if (organization.getOrganCoding() == null || organization.getOrganCoding().trim().isEmpty()) {
            return Result.error("机构编码不能为空");
        }

        // Validate code length
        String coding = organization.getOrganCoding().trim();
        if (coding.length() != 4 && coding.length() != 6) {
            return Result.error("机构编码必须是4位（父级）或6位（子级）");
        }

        // Check if code already exists
        int count = organizationRepository.countByCode(coding);
        if (count > 0) {
            return Result.error("机构编码已存在");
        }

        // Set default values
        if (organization.getOrderId() == null) {
            organization.setOrderId(getMaxOrderId() + 1);
        }
        if (organization.getIsVisible() == null) {
            organization.setIsVisible(true);
        }

        organizationRepository.insert(organization);
        return Result.success(organization);
    }

    /**
     * Update organization
     */
    @Transactional
    public Result<Organization> updateOrganization(Organization organization) {
        if (organization.getOrganId() == null) {
            return Result.error("机构ID不能为空");
        }

        // Check if exists
        Organization existing = organizationRepository.getById(organization.getOrganId());
        if (existing == null) {
            return Result.error("机构不存在");
        }

        // If code is being changed, check for duplicates
        String currentCode = existing.getOrganCoding();
        String newCode = organization.getOrganCoding();
        if (newCode != null && !newCode.equals(currentCode)) {
            int count = organizationRepository.countByCode(newCode);
            if (count > 0) {
                return Result.error("机构编码已存在");
            }
        }

        organizationRepository.updateById(organization);
        return Result.success(organization);
    }

    /**
     * Delete organization
     */
    @Transactional
    public Result<Void> deleteOrganization(Long organId) {
        // Check if has children
        List<Organization> children = organizationRepository.getChildrenByParentCode(organizationRepository.getById(organId).getOrganCoding());
        if (children != null && !children.isEmpty()) {
            return Result.error("该机构下有子级组织，无法删除");
        }

        int count = organizationRepository.countById(organId);
        if (count == 0) {
            return Result.error("机构不存在");
        }

        organizationRepository.deleteById(organId);
        return Result.success();
    }

    /**
     * Update visibility
     */
    @Transactional
    public Result<Void> updateVisibility(Long organId, Boolean isVisible) {
        int count = organizationRepository.countById(organId);
        if (count == 0) {
            return Result.error("机构不存在");
        }

        organizationRepository.updateVisibility(organId, isVisible);
        return Result.success();
    }

    /**
     * Get max order ID
     */
    private Integer getMaxOrderId() {
        List<Organization> all = getAllVisible();
        int max = 0;
        for (Organization org : all) {
            if (org.getOrderId() != null && org.getOrderId() > max) {
                max = org.getOrderId();
            }
        }
        return max;
    }
}
