package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.CertificateAddItem;
import org.apache.ibatis.annotations.*;

import java.util.List;

/**
 * CertificateAddItem Repository
 * 证书增项数据访问层
 */
public interface CertificateAddItemRepository extends BaseMapper<CertificateAddItem> {

    /**
     * Get by ID
     */
    @Select("SELECT * FROM CertificateAddItem WHERE CertificateAddItemID = #{certificateAddItemId}")
    CertificateAddItem getById(@Param("certificateAddItemId") Long certificateAddItemId);

    /**
     * Get all certificate add items
     */
    @Select("SELECT * FROM VIEW_CERTIFICATE_ADDITEM ORDER BY CertificateAddItemID")
    List<CertificateAddItem> getAll();

    /**
     * Get by certificate ID
     */
    @Select("SELECT * FROM VIEW_CERTIFICATE_ADDITEM WHERE CertificateID = #{certificateId} ORDER BY CertificateAddItemID")
    List<CertificateAddItem> getByCertificateId(@Param("certificateId") Long certificateId);

    /**
     * Get by post type ID
     */
    @Select("SELECT * FROM VIEW_CERTIFICATE_ADDITEM WHERE PostTypeID = #{postTypeId} ORDER BY CertificateAddItemID")
    List<CertificateAddItem> getByPostTypeId(@Param("postTypeId") Integer postTypeId);

    /**
     * Get by post ID
     */
    @Select("SELECT * FROM VIEW_CERTIFICATE_ADDITEM WHERE PostID = #{postId} ORDER BY CertificateAddItemID")
    List<CertificateAddItem> getByPostId(@Param("postId") Integer postId);

    /**
     * Get by case status
     */
    @Select("SELECT * FROM VIEW_CERTIFICATE_ADDITEM WHERE CaseStatus = #{caseStatus} ORDER BY CertificateAddItemID")
    List<CertificateAddItem> getByCaseStatus(@Param("caseStatus") String caseStatus);

    /**
     * Get add item names by certificate ID
     */
    @Select("SELECT p.PostName FROM CertificateAddItem a LEFT JOIN PostInfo p ON a.PostID = p.PostID WHERE a.CertificateID = #{certificateId}")
    List<String> getAddItemNames(@Param("certificateId") Long certificateId);

    /**
     * Get add item names string (comma-separated)
     */
    @Select("SELECT STRING_AGG(',增' + p.PostName, '') FROM CertificateAddItem a LEFT JOIN PostInfo p ON a.PostID = p.PostID WHERE a.CertificateID = #{certificateId}")
    String getAddItemNameString(@Param("certificateId") Long certificateId);

    /**
     * Insert certificate add item
     */
    @Insert("INSERT INTO CertificateAddItem(CertificateID, PostTypeID, PostID, CreatePerson, CreateTime, CaseStatus) VALUES (#{certificateId}, #{postTypeId}, #{postId}, #{createPerson}, #{createTime}, #{caseStatus})")
    @Options(useGeneratedKeys = true, keyProperty = "certificateAddItemId", keyColumn = "CertificateAddItemID")
    int insertCertificateAddItem(CertificateAddItem certificateAddItem);

    /**
     * Update certificate add item
     */
    @Update("UPDATE CertificateAddItem SET CertificateID = #{certificateId}, PostTypeID = #{postTypeId}, PostID = #{postId}, CreatePerson = #{createPerson}, CreateTime = #{createTime}, CaseStatus = #{caseStatus} WHERE CertificateAddItemID = #{certificateAddItemId}")
    int updateCertificateAddItem(CertificateAddItem certificateAddItem);

    /**
     * Delete certificate add item
     */
    @Delete("DELETE FROM CertificateAddItem WHERE CertificateAddItemID = #{certificateAddItemId}")
    int deleteCertificateAddItem(@Param("certificateAddItemId") Long certificateAddItemId);

    /**
     * Delete by certificate ID
     */
    @Delete("DELETE FROM CertificateAddItem WHERE CertificateID = #{certificateId}")
    int deleteByCertificateId(@Param("certificateId") Long certificateId);

    /**
     * Count certificate add items
     */
    @Select("SELECT COUNT(*) FROM CertificateAddItem")
    int count();

    /**
     * Count by certificate ID
     */
    @Select("SELECT COUNT(*) FROM CertificateAddItem WHERE CertificateID = #{certificateId}")
    int countByCertificateId(@Param("certificateId") Long certificateId);

    /**
     * Count by case status
     */
    @Select("SELECT COUNT(*) FROM CertificateAddItem WHERE CaseStatus = #{caseStatus}")
    int countByCaseStatus(@Param("caseStatus") String caseStatus);
}
