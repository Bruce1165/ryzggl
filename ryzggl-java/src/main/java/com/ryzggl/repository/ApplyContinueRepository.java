package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.entity.ApplyContinue;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

import java.util.List;

/**
 * ApplyContinue Repository - 延续申请数据访问层
 */
@Mapper
public interface ApplyContinueRepository extends BaseMapper<ApplyContinue> {

    /**
     * Query apply continue list with pagination
     */
    @Select("<script>" +
            "SELECT * FROM ApplyContinue" +
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
    IPage<ApplyContinue> selectApplyContinuePage(Page<ApplyContinue> page,
                                                    @Param("applyNo") String applyNo,
                                                    @Param("applicantName") String applicantName,
                                                    @Param("certificateCode") String certificateCode,
                                                    @Param("applyStatus") String applyStatus);

    /**
     * Query apply continue by ID
     */
    @Select("SELECT * FROM ApplyContinue WHERE ID = #{id}")
    ApplyContinue selectById(@Param("id") Long id);

    /**
     * Query apply continues by worker ID
     */
    @Select("SELECT * FROM ApplyContinue WHERE WORKERID = #{workerId} ORDER BY CREATE_TIME DESC")
    List<ApplyContinue> selectByWorkerId(@Param("workerId") Long workerId);

    /**
     * Query apply continues by certificate code
     */
    @Select("SELECT * FROM ApplyContinue WHERE CERTIFICATECODE = #{certificateCode} ORDER BY CREATE_TIME DESC")
    List<ApplyContinue> selectByCertificateCode(@Param("certificateCode") String certificateCode);

    /**
     * Query apply continues by status
     */
    @Select("SELECT * FROM ApplyContinue WHERE APPLYSTATUS = #{status} ORDER BY CREATE_TIME DESC")
    List<ApplyContinue> selectByStatus(@Param("status") String applyStatus);
}
