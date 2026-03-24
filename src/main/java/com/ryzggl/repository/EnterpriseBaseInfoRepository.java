package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.EnterpriseBaseInfo;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

import java.util.List;

/**
 * EnterpriseBaseInfo Repository - Data Access Layer
 * Maps to: EnterpriseBaseInfoDAL.cs
 *
 * Replaces ADO.NET with MyBatis-Plus for database operations
 */
@Mapper
public interface EnterpriseBaseInfoRepository extends BaseMapper<EnterpriseBaseInfo> {

    /**
     * Find enterprise by organization code
     */
    @Select("SELECT * FROM COC_ONE_ENT_BASEINFO WHERE ENT_ORGANIZATIONSCODE = #{entOrganizationsCode}")
    EnterpriseBaseInfo findByEntOrganizationsCode(@Param("entOrganizationsCode") String entOrganizationsCode);

    /**
     * Find enterprise by name
     */
    @Select("SELECT * FROM COC_ONE_ENT_BASEINFO WHERE ENT_NAME LIKE '%' + #{entName} + '%' ORDER BY ENT_NAME")
    List<EnterpriseBaseInfo> findByEntName(@Param("entName") String entName);

    /**
     * Find by enterprise type
     */
    @Select("SELECT * FROM COC_ONE_ENT_BASEINFO WHERE ENT_TYPE = #{entType} ORDER BY ENT_NAME")
    List<EnterpriseBaseInfo> findByEntType(@Param("entType") String entType);

    /**
     * Find by grade level
     */
    @Select("SELECT * FROM COC_ONE_ENT_BASEINFO WHERE ENT_GRADE = #{entGrade} ORDER BY ENT_NAME")
    List<EnterpriseBaseInfo> findByEntGrade(@Param("entGrade") String entGrade);

    /**
     * Find by status
     */
    @Select("SELECT * FROM COC_ONE_ENT_BASEINFO WHERE ENT_STATUS = #{entStatus} ORDER BY ENT_NAME")
    List<EnterpriseBaseInfo> findByEntStatus(@Param("entStatus") String entStatus);

    /**
     * Find by province
     */
    @Select("SELECT * FROM COC_ONE_ENT_BASEINFO WHERE ENT_PROVINCE = #{entProvince} ORDER BY ENT_NAME")
    List<EnterpriseBaseInfo> findByEntProvince(@Param("entProvince") String entProvince);

    /**
     * Find all enterprises
     */
    @Select("SELECT * FROM COC_ONE_ENT_BASEINFO ORDER BY ENT_NAME")
    List<EnterpriseBaseInfo> findAllEnterprises();

    /**
     * Find recently registered enterprises (last N days)
     */
    @Select("SELECT * FROM COC_ONE_ENT_BASEINFO WHERE ENT_REGISTERDATE >= DATEADD(DAY, -#{days}, GETDATE()) ORDER BY ENT_REGISTERDATE DESC")
    List<EnterpriseBaseInfo> findRecent(@Param("days") int days);

    /**
     * Count enterprises by type
     */
    @Select("SELECT COUNT(*) FROM COC_ONE_ENT_BASEINFO WHERE ENT_TYPE = #{entType}")
    int countByEntType(@Param("entType") String entType);
}
