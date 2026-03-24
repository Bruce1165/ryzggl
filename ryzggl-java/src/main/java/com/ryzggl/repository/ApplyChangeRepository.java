package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.entity.ApplyChange;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

/**
 * ApplyChange Repository - 变更申请
 * Change application repository with from/to transfer fields
 */
@Mapper
public interface ApplyChangeRepository extends BaseMapper<ApplyChange> {

    /**
     * Query apply list with pagination
     */
    @Select("<script>" +
            "SELECT * FROM ApplyChange WHERE DELETED = 0" +
            "<if test='workerName != null and workerName != \"\"'>" +
            " AND PsnName LIKE CONCAT('%', #{workerName}, '%')" +
            "</if>" +
            "<if test='psnRegisterNo != null and psnRegisterNo != \"\"'>" +
            " AND PsnRegisterNo = #{psnRegisterNo}" +
            "</if>" +
            "<if test='changeReason != null and changeReason != \"\"'>" +
            " AND ChangeReason LIKE CONCAT('%', #{changeReason}, '%')" +
            "</if>" +
            "<if test='ifOutside != null'>" +
            " AND IfOutside = #{ifOutside}" +
            "</if>" +
            " ORDER BY CreateTime DESC" +
            "</script>")
    IPage<ApplyChange> selectApplyChangePage(Page<ApplyChange> page,
                                           @Param("workerName") String workerName,
                                           @Param("psnRegisterNo") String psnRegisterNo,
                                           @Param("changeReason") String changeReason,
                                           @Param("ifOutside") String ifOutside);

    /**
     * Query apply by ID
     */
    @Select("SELECT * FROM ApplyChange WHERE ApplyID = #{id} AND DELETED = 0")
    ApplyChange selectById(@Param("id") String id);

    /**
     * Query applies by worker ID
     */
    @Select("SELECT * FROM ApplyChange WHERE WorkerID = #{workerId} AND DELETED = 0 ORDER BY CreateTime DESC")
    com.baomidou.mybatisplus.core.metadata.IPage<ApplyChange> selectByWorkerId(
            Page<ApplyChange> page,
            @Param("workerId") String workerId);

    /**
     * Query applies by status
     */
    @Select("SELECT * FROM ApplyChange WHERE ApplyStatus = #{status} AND DELETED = 0 ORDER BY CreateTime DESC")
    com.baomidou.mybatisplus.core.metadata.IPage<ApplyChange> selectByStatus(
            Page<ApplyChange> page,
            @Param("status") String status);

    /**
     * Search applications with from/to transfer fields
     */
    @Select("<script>" +
            "SELECT * FROM ApplyChange WHERE DELETED = 0" +
            "<if test='workerName != null and workerName != \"\"'>" +
            " AND PsnName LIKE CONCAT('%', #{workerName}, '%')" +
            "</if>" +
            "<if test='psnRegisterNo != null and psnRegisterNo != \"\"'>" +
            " AND PsnRegisterNo = #{psnRegisterNo}" +
            "</if>" +
            "<if test='changeReason != null and changeReason != \"\"'>" +
            " AND ChangeReason LIKE CONCAT('%', #{changeReason}, '%')" +
            "</if>" +
            "<if test='fromEntCity != null and fromEntCity != \"\"'>" +
            " AND FromEntCity = #{fromEntCity}" +
            "</if>" +
            "<if test='toEntCity != null and toEntCity != \"\"'>" +
            " AND ToEntCity = #{toEntCity}" +
            "</if>" +
            "<if test='fromPsnSex != null and fromPsnSex != \"\"'>" +
            " AND FromPsnSex = #{fromPsnSex}" +
            "</if>" +
            "<if test='toPsnSex != null and toPsnSex != \"\"'>" +
            " AND ToPsnSex = #{toPsnSex}" +
            "</if>" +
            " ORDER BY CreateTime DESC" +
            "</script>")
    com.baomidou.mybatisplus.core.metadata.IPage<ApplyChange> searchApplications(
            Page<ApplyChange> page,
            @Param("workerName") String workerName,
            @Param("psnRegisterNo") String psnRegisterNo,
            @Param("changeReason") String changeReason,
            @Param("fromEntCity") String fromEntCity,
            @Param("toEntCity") String toEntCity,
            @Param("fromPsnSex") String fromPsnSex,
            @Param("toPsnSex") String toPsnSex);
}
