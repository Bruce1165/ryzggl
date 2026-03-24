package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.Organization;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;
import org.apache.ibatis.annotations.Update;

import java.util.List;

/**
 * Organization Repository
 * 机构管理数据访问层
 */
public interface OrganizationRepository extends BaseMapper<Organization> {

    /**
     * Get organization by ID
     */
    @Select("SELECT * FROM Organization WHERE organId = #{organId}")
    Organization getById(Long organId);

    /**
     * Get organization by code
     */
    @Select("SELECT * FROM Organization WHERE organCoding = #{organCoding}")
    Organization getByCode(String organCoding);

    /**
     * Get all root organizations (4-character codes)
     */
    @Select("SELECT * FROM Organization WHERE LENGTH(organCoding) = 4 AND isVisible = 1 ORDER BY orderId")
    List<Organization> getRootOrganizations();

    /**
     * Get child organizations by parent code
     */
    @Select("SELECT * FROM Organization WHERE organCoding LIKE CONCAT(#{parentCode}, '%') AND LENGTH(organCoding) = 6 AND isVisible = 1 ORDER BY orderId")
    List<Organization> getChildrenByParentCode(@Param("parentCode") String parentCode);

    /**
     * Get all visible organizations
     */
    @Select("SELECT * FROM Organization WHERE isVisible = 1 ORDER BY organCoding")
    List<Organization> getAllVisible();

    /**
     * Get organizations by type
     */
    @Select("SELECT * FROM Organization WHERE organType = #{organType} AND isVisible = 1 ORDER BY orderId")
    List<Organization> getByType(String organType);

    /**
     * Get organizations by region
     */
    @Select("SELECT * FROM Organization WHERE regionId = #{regionId} AND isVisible = 1 ORDER BY orderId")
    List<Organization> getByRegion(String regionId);

    /**
     * Search organizations by keyword
     */
    @Select("SELECT * FROM Organization WHERE isVisible = 1 AND (organName LIKE CONCAT('%', #{keyword}, '%') OR organCoding LIKE CONCAT('%', #{keyword}, '%')) ORDER BY orderId")
    List<Organization> search(@Param("keyword") String keyword);

    /**
     * Check if organization code exists
     */
    @Select("SELECT COUNT(*) FROM Organization WHERE organCoding = #{organCoding}")
    int countByCode(String organCoding);

    /**
     * Update visibility
     */
    @Update("UPDATE Organization SET isVisible = #{isVisible} WHERE organId = #{organId}")
    int updateVisibility(@Param("organId") Long organId, @Param("isVisible") Boolean isVisible);

    /**
     * Delete organization
     */
    @Select("SELECT COUNT(*) FROM Organization WHERE organId = #{organId}")
    int countById(Long organId);
}
