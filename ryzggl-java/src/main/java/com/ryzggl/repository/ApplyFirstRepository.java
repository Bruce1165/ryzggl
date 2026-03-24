package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.entity.ApplyFirst;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

/**
 * ApplyFirst Repository - 首次注册申请
 * Initial registration application repository
 */
@Mapper
public interface ApplyFirstRepository extends BaseMapper<ApplyFirst> {

    /**
     * Query apply list with pagination
     */
    @Select("<script>" +
            "SELECT * FROM ApplyFirst WHERE DELETED = 0" +
            "<if test='workerName != null and workerName != \"\"'>" +
            " AND PsnName LIKE CONCAT('%', #{workerName}, '%')" +
            "</if>" +
            "<if test='idCard != null and idCard != \"\"'>" +
            " AND PsnIDCard = #{idCard}" +
            "</if>" +
            "<if test='unitCode != null and unitCode != \"\"'>" +
            " AND UnitCode = #{unitCode}" +
            "</if>" +
            "<if test='applyStatus != null and applyStatus != \"\"'>" +
            " AND ApplyStatus = #{applyStatus}" +
            "</if>" +
            " ORDER BY CreateTime DESC" +
            "</script>")
    IPage<ApplyFirst> selectApplyFirstPage(Page<ApplyFirst> page,
                                          @Param("workerName") String workerName,
                                          @Param("idCard") String idCard,
                                          @Param("unitCode") String unitCode,
                                          @Param("applyStatus") String applyStatus);

    /**
     * Query apply by ID
     */
    @Select("SELECT * FROM ApplyFirst WHERE ApplyID = #{id} AND DELETED = 0")
    ApplyFirst selectById(@Param("id") String id);

    /**
     * Query applies by worker ID
     */
    @Select("SELECT * FROM ApplyFirst WHERE WorkerID = #{workerId} AND DELETED = 0 ORDER BY CreateTime DESC")
    com.baomidou.mybatisplus.core.metadata.IPage<ApplyFirst> selectByWorkerId(
            Page<ApplyFirst> page,
            @Param("workerId") String workerId);

    /**
     * Query applies by status
     */
    @Select("SELECT * FROM ApplyFirst WHERE ApplyStatus = #{status} AND DELETED = 0 ORDER BY CreateTime DESC")
    com.baomidou.mybatisplus.core.metadata.IPage<ApplyFirst> selectByStatus(
            Page<ApplyFirst> page,
            @Param("status") String status);

    /**
     * Search applications
     */
    @Select("<script>" +
            "SELECT * FROM ApplyFirst WHERE DELETED = 0" +
            "<if test='workerName != null and workerName != \"\"'>" +
            " AND PsnName LIKE CONCAT('%', #{workerName}, '%')" +
            "</if>" +
            "<if test='idCard != null and idCard != \"\"'>" +
            " AND PsnIDCard = #{idCard}" +
            "</if>" +
            "<if test='phone != null and phone != \"\"'>" +
            " AND PsnMobilePhone = #{phone}" +
            "</if>" +
            "<if test='qualificationName != null and qualificationName != \"\"'>" +
            " AND QualificationName LIKE CONCAT('%', #{qualificationName}, '%')" +
            "</if>" +
            " ORDER BY CreateTime DESC" +
            "</script>")
    com.baomidou.mybatisplus.core.metadata.IPage<ApplyFirst> searchApplications(
            Page<ApplyFirst> page,
            @Param("workerName") String workerName,
            @Param("idCard") String idCard,
            @Param("phone") String phone,
            @Param("qualificationName") String qualificationName);
}
