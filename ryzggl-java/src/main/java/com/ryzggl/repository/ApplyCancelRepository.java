package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.entity.ApplyCancel;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

/**
 * ApplyCancel Repository - 注销申请
 * Cancellation application repository
 */
@Mapper
public interface ApplyCancelRepository extends BaseMapper<ApplyCancel> {

    /**
     * Query apply list with pagination
     */
    @Select("<script>" +
            "SELECT * FROM ApplyCancel WHERE DELETED = 0" +
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
    IPage<ApplyCancel> selectApplyCancelPage(Page<ApplyCancel> page,
                                           @Param("workerName") String workerName,
                                           @Param("idCard") String idCard,
                                           @Param("unitCode") String unitCode,
                                           @Param("applyStatus") String applyStatus);

    /**
     * Query apply by ID
     */
    @Select("SELECT * FROM ApplyCancel WHERE ApplyID = #{id} AND DELETED = 0")
    ApplyCancel selectById(@Param("id") String id);

    /**
     * Query applies by worker ID
     */
    @Select("SELECT * FROM ApplyCancel WHERE WorkerID = #{workerId} AND DELETED = 0 ORDER BY CreateTime DESC")
    com.baomidou.mybatisplus.core.metadata.IPage<ApplyCancel> selectByWorkerId(
            Page<ApplyCancel> page,
            @Param("workerId") String workerId);

    /**
     * Query applies by status
     */
    @Select("SELECT * FROM ApplyCancel WHERE ApplyStatus = #{status} AND DELETED = 0 ORDER BY CreateTime DESC")
    com.baomidou.mybatisplus.core.metadata.IPage<ApplyCancel> selectByStatus(
            Page<ApplyCancel> page,
            @Param("status") String status);

    /**
     * Search applications by cancel reason
     */
    @Select("<script>" +
            "SELECT * FROM ApplyCancel WHERE DELETED = 0" +
            "<if test='cancelReason != null and cancelReason != \"\"'>" +
            " AND CancelReason LIKE CONCAT('%', #{cancelReason}, '%')" +
            "</if>" +
            "<if test='applyStatus != null and applyStatus != \"\"'>" +
            " AND ApplyStatus = #{applyStatus}" +
            "</if>" +
            " ORDER BY CreateTime DESC" +
            "</script>")
    com.baomidou.mybatisplus.core.metadata.IPage<ApplyCancel> searchByCancelReason(
            Page<ApplyCancel> page,
            @Param("cancelReason") String cancelReason,
            @Param("applyStatus") String applyStatus);

    /**
     * Search applications
     */
    @Select("<script>" +
            "SELECT * FROM ApplyCancel WHERE DELETED = 0" +
            "<if test='workerName != null and workerName != \"\"'>" +
            " AND PsnName LIKE CONCAT('%', #{workerName}, '%')" +
            "</if>" +
            "<if test='idCard != null and idCard != \"\"'>" +
            " AND PsnIDCard = #{idCard}" +
            "</if>" +
            "<if test='phone != null and phone != \"\"'>" +
            " AND PsnMobilePhone = #{phone}" +
            "</if>" +
            "<if test='cancelReason != null and cancelReason != \"\"'>" +
            " AND CancelReason LIKE CONCAT('%', #{cancelReason}, '%')" +
            "</if>" +
            " ORDER BY CreateTime DESC" +
            "</script>")
    com.baomidou.mybatisplus.core.metadata.IPage<ApplyCancel> searchApplications(
            Page<ApplyCancel> page,
            @Param("workerName") String workerName,
            @Param("idCard") String idCard,
            @Param("phone") String phone,
            @Param("cancelReason") String cancelReason);
}
