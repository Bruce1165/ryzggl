package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.entity.Worker;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

import java.util.List;

/**
 * Worker Repository
 */
@Mapper
public interface WorkerRepository extends BaseMapper<Worker> {

    /**
     * Query worker list with pagination
     */
    @Select("<script>" +
            "SELECT * FROM Worker WHERE DELETED = 0" +
            "<if test='name != null and name != \"\"'>" +
            " AND NAME LIKE CONCAT('%', #{name}, '%')" +
            "</if>" +
            "<if test='idCard != null and idCard != \"\"'>" +
            " AND IDCARD = #{idCard}" +
            "</if>" +
            "<if test='unitCode != null and unitCode != \"\"'>" +
            " AND UNITCODE = #{unitCode}" +
            "</if>" +
            " ORDER BY CREATE_TIME DESC" +
            "</script>")
    IPage<Worker> selectWorkerPage(Page<Worker> page,
                                  @Param("name") String name,
                                  @Param("idCard") String idCard,
                                  @Param("unitCode") String unitCode);

    /**
     * Query worker by worker code
     */
    @Select("SELECT * FROM Worker WHERE WORKERCODE = #{workerCode} AND DELETED = 0")
    Worker selectByWorkerCode(@Param("workerCode") String workerCode);

    /**
     * Query worker by ID card
     */
    @Select("SELECT * FROM Worker WHERE IDCARD = #{idCard} AND DELETED = 0")
    Worker selectByIdCard(@Param("idCard") String idCard);

    /**
     * Query workers by unit code
     */
    @Select("SELECT * FROM Worker WHERE UNITCODE = #{unitCode} AND DELETED = 0 ORDER BY CREATE_TIME DESC")
    List<Worker> selectByUnitCode(@Param("unitCode") String unitCode);
}
