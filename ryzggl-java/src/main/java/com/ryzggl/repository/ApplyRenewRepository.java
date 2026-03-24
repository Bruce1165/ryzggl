package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.entity.ApplyRenew;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

import java.util.List;

/**
 * ApplyRenew Repository - 续期申请数据访问层
 */
@Mapper
public interface ApplyRenewRepository extends BaseMapper<ApplyRenew> {

    /**
     * Query apply renew list with pagination
     */
    @Select("<script>" +
            "SELECT * FROM ApplyRenew" +
            "<where>" +
            "<if test='applyNo != null and applyNo != \"\"'>" +
            " AND APPLYNO LIKE CONCAT('%', #{applyNo}, '%')" +
            "</if>" +
            "<if test='applicantName != null and applicantName != \"\"'>" +
            " AND APPLICANTNAME LIKE CONCAT('%', #{applicantName}, '%')" +
            "</if>" +
            "<if test='certificateCode != null and certificateCode != \"\"'>" +
            " AND CERTIFICATECODE LIKE CONCAT('%', #{certificateCode}, '%')" +
            "</if>" +
            "<if test='applyStatus != null and applyStatus != \"\"'>" +
            " AND APPLYSTATUS = #{applyStatus}" +
            "</if>" +
            "</where>" +
            " ORDER BY CREATE_TIME DESC" +
            "</script>")
    IPage<ApplyRenew> selectApplyRenewPage(Page<ApplyRenew> page,
                                                 @Param("applyNo") String applyNo,
                                                 @Param("applicantName") String applicantName,
                                                 @Param("certificateCode") String certificateCode,
                                                 @Param("applyStatus") String applyStatus);

    /**
     * Query apply renew by ID
     */
    @Select("SELECT * FROM ApplyRenew WHERE ID = #{id}")
    ApplyRenew selectById(@Param("id") Long id);

    /**
     * Query apply renews by worker ID
     */
    @Select("SELECT * FROM ApplyRenew WHERE WORKERID = #{workerId} ORDER BY CREATE_TIME DESC")
    List<ApplyRenew> selectByWorkerId(@Param("workerId") Long workerId);

    /**
     * Query apply renews by certificate code
     */
    @Select("SELECT * FROM ApplyRenew WHERE CERTIFICATECODE = #{certificateCode} ORDER BY CREATE_TIME DESC")
    List<ApplyRenew> selectByCertificateCode(@Param("certificateCode") String certificateCode);

    /**
     * Query apply renews by status
     */
    @Select("SELECT * FROM ApplyRenew WHERE APPLYSTATUS = #{status} ORDER BY CREATE_TIME DESC")
    List<ApplyRenew> selectByStatus(@Param("status") String applyStatus);
}
