package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.entity.ApplyReplace;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

import java.util.List;

/**
 * ApplyReplace Repository
 * 证书补办申请数据访问层
 *
 * MyBatis-Plus mapper for ApplyReplace entity
 * Provides CRUD operations and custom queries
 */
public interface ApplyReplaceRepository extends BaseMapper<ApplyReplace> {

    /**
     * Get ApplyReplace by Apply ID
     * 根据申请ID获取补办申请
     *
     * @param applyId Apply ID
     * @return ApplyReplace entity or null
     */
    @Select("SELECT * FROM ApplyReplace WHERE ApplyID = #{applyId}")
    ApplyReplace getByApplyId(@Param("applyId") String applyId);

    /**
     * Get ApplyReplace by Registration Number
     * 根据注册号获取补办申请
     *
     * @param registerNo Registration number
     * @return ApplyReplace entity or null
     */
    @Select("SELECT * FROM ApplyReplace WHERE RegisterNo = #{registerNo}")
    ApplyReplace getByRegisterNo(@Param("registerNo") String registerNo);

    /**
     * Get ApplyReplace by Registration Certificate Number
     * 根据注册证书号获取补办申请
     *
     * @param registerCertificateNo Registration certificate number
     * @return ApplyReplace entity or null
     */
    @Select("SELECT * FROM ApplyReplace WHERE RegisterCertificateNo = #{registerCertificateNo}")
    ApplyReplace getByRegisterCertificateNo(@Param("registerCertificateNo") String registerCertificateNo);

    /**
     * Get ApplyReplace list by status with pagination
     * 根据状态获取补办申请列表（分页）
     *
     * @param page Page object
     * @param status Application status
     * @return Page of ApplyReplace entities
     */
    @Select("<script>" +
            "SELECT * FROM ApplyReplace " +
            "<where>" +
            "  <if test='status != null and status != \"\"'>" +
            "    AND status = #{status}" +
            "  </if>" +
            "</where>" +
            "ORDER BY CreateTime DESC" +
            "</script>")
    IPage<ApplyReplace> getByStatus(Page<ApplyReplace> page, @Param("status") String status);

    /**
     * Search ApplyReplace by multiple criteria
     * 根据多个条件搜索补办申请
     *
     * @param page Page object
     * @param registerNo Registration number (optional)
     * @param registerCertificateNo Registration certificate number (optional)
     * @param replaceType Replace type (optional)
     * @param status Status (optional)
     * @return Page of ApplyReplace entities
     */
    @Select("<script>" +
            "SELECT * FROM ApplyReplace " +
            "<where>" +
            "  <if test='registerNo != null and registerNo != \"\"'>" +
            "    AND RegisterNo LIKE CONCAT('%', #{registerNo}, '%')" +
            "  </if>" +
            "  <if test='registerCertificateNo != null and registerCertificateNo != \"\"'>" +
            "    AND RegisterCertificateNo LIKE CONCAT('%', #{registerCertificateNo}, '%')" +
            "  </if>" +
            "  <if test='replaceType != null and replaceType != \"\"'>" +
            "    AND ReplaceType = #{replaceType}" +
            "  </if>" +
            "  <if test='status != null and status != \"\"'>" +
            "    AND status = #{status}" +
            "  </if>" +
            "</where>" +
            "ORDER BY CreateTime DESC" +
            "</script>")
    IPage<ApplyReplace> search(Page<ApplyReplace> page,
                           @Param("registerNo") String registerNo,
                           @Param("registerCertificateNo") String registerCertificateNo,
                           @Param("replaceType") String replaceType,
                           @Param("status") String status);

    /**
     * Count ApplyReplace by status
     * 根据状态统计补办申请数量
     *
     * @param status Application status
     * @return Count of ApplyReplace entities
     */
    @Select("<script>" +
            "SELECT COUNT(*) FROM ApplyReplace " +
            "<where>" +
            "  <if test='status != null and status != \"\"'>" +
            "    AND status = #{status}" +
            "  </if>" +
            "</where>" +
            "</script>")
    long countByStatus(@Param("status") String status);

    /**
     * Get ApplyReplace by Replace Type
     * 根据补办类型获取补办申请列表
     *
     * @param page Page object
     * @param replaceType Replace type
     * @return Page of ApplyReplace entities
     */
    @Select("SELECT * FROM ApplyReplace WHERE ReplaceType = #{replaceType} ORDER BY CreateTime DESC")
    IPage<ApplyReplace> getByReplaceType(Page<ApplyReplace> page, @Param("replaceType") String replaceType);

    /**
     * Check if ApplyReplace exists by RegisterNo
     * 检查指定注册号的补办申请是否存在
     *
     * @param registerNo Registration number
     * @return Count (0 or 1)
     */
    @Select("SELECT COUNT(*) FROM ApplyReplace WHERE RegisterNo = #{registerNo}")
    int existsByRegisterNo(@Param("registerNo") String registerNo);
}
