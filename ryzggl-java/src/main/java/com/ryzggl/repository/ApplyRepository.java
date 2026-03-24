package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.entity.Apply;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

import java.util.List;

/**
 * Apply Repository
 */
@Mapper
public interface ApplyRepository extends BaseMapper<Apply> {

    /**
     * Query apply list with pagination
     */
    @Select("<script>" +
            "SELECT * FROM Apply WHERE DELETED = 0" +
            "<if test='applyName != null and applyName != \"\"'>" +
            " AND APPLICANTNAME LIKE CONCAT('%', #{applyName}, '%')" +
            "</if>" +
            "<if test='applyType != null and applyType != \"\"'>" +
            " AND APPLYTYPE = #{applyType}" +
            "</if>" +
            "<if test='applyStatus != null and applyStatus != \"\"'>" +
            " AND APPLYSTATUS = #{applyStatus}" +
            "</if>" +
            " ORDER BY CREATE_TIME DESC" +
            "</script>")
    IPage<Apply> selectApplyPage(Page<Apply> page,
                                @Param("applyName") String applyName,
                                @Param("applyType") String applyType,
                                @Param("applyStatus") String applyStatus);

    /**
     * Query apply by ID
     */
    @Select("SELECT * FROM Apply WHERE ID = #{id} AND DELETED = 0")
    Apply selectById(@Param("id") Long id);

    /**
     * Query applies by worker ID
     */
    @Select("SELECT * FROM Apply WHERE WORKERID = #{workerId} AND DELETED = 0 ORDER BY CREATE_TIME DESC")
    List<Apply> selectByWorkerId(@Param("workerId") Long workerId);

    /**
     * Query applies by status
     */
    @Select("SELECT * FROM Apply WHERE APPLYSTATUS = #{status} AND DELETED = 0 ORDER BY CREATE_TIME DESC")
    List<Apply> selectByStatus(@Param("status") String status);
}
