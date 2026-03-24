package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.Qualification;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;
import org.apache.ibatis.annotations.Update;

import java.util.List;

/**
 * Qualification Repository
 * 资格证书管理数据访问层
 */
public interface QualificationRepository extends BaseMapper<Qualification> {

    /**
     * Get qualification by ID
     */
    @Select("SELECT * FROM Qualification WHERE qualId = #{qualId}")
    Qualification getById(Long qualId);

    /**
     * Get qualification by code
     */
    @Select("SELECT * FROM Qualification WHERE qualCode = #{qualCode}")
    Qualification getByCode(String qualCode);

    /**
     * Get all qualifications
     */
    @Select("SELECT * FROM Qualification WHERE isValid = 1 ORDER BY qualId DESC")
    List<Qualification> getAllValid();

    /**
     * Get qualifications by type
     */
    @Select("SELECT * FROM Qualification WHERE qualType = #{qualType} AND isValid = 1 ORDER BY qualId DESC")
    List<Qualification> getByType(String qualType);

    /**
     * Get qualifications by level
     */
    @Select("SELECT * FROM Qualification WHERE qualLevel = #{qualLevel} AND isValid = 1 ORDER BY qualId DESC")
    List<Qualification> getByLevel(String qualLevel);

    /**
     * Get qualifications by category
     */
    @Select("SELECT * FROM Qualification WHERE qualCategory = #{qualCategory} AND isValid = 1 ORDER BY qualId DESC")
    List<Qualification> getByCategory(String qualCategory);

    /**
     * Get qualifications by province (ZGZSBH)
     */
    @Select("SELECT * FROM Qualification WHERE zGZSBH = #{zGZSBH} AND isValid = 1 ORDER BY qualId DESC")
    List<Qualification> getByProvince(@Param("zGZSBH") String zGZSBH);

    /**
     * Search qualifications
     */
    @Select("SELECT * FROM Qualification WHERE isValid = 1 AND (qualName LIKE CONCAT('%', #{keyword}, '%') OR qualCode LIKE CONCAT('%', #{keyword}, '%')) ORDER BY qualId DESC")
    List<Qualification> search(@Param("keyword") String keyword);

    /**
     * Check if qualification code exists
     */
    @Select("SELECT COUNT(*) FROM Qualification WHERE qualCode = #{qualCode}")
    int countByCode(String qualCode);

    /**
     * Update validity
     */
    @Update("UPDATE Qualification SET isValid = #{isValid} WHERE qualId = #{qualId}")
    int updateValidity(@Param("qualId") Long qualId, @Param("isValid") Boolean isValid);

    /**
     * Delete qualification
     */
    @Select("SELECT COUNT(*) FROM Qualification WHERE qualId = #{qualId}")
    int countById(Long qualId);
}
