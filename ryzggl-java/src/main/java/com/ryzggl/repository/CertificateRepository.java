package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.entity.Certificate;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

import java.util.List;

/**
 * Certificate Repository
 */
@Mapper
public interface CertificateRepository extends BaseMapper<Certificate> {

    /**
     * Query certificate list with pagination
     */
    @Select("<script>" +
            "SELECT * FROM Certificate WHERE DELETED = 0" +
            "<if test='certNo != null and certNo != \"\"'>" +
            " AND CERTNO LIKE CONCAT('%', #{certNo}, '%')" +
            "</if>" +
            "<if test='holderName != null and holderName != \"\"'>" +
            " AND HOLDERNAME LIKE CONCAT('%', #{holderName}, '%')" +
            "</if>" +
            "<if test='qualificationName != null and qualificationName != \"\"'>" +
            " AND QUALIFICATIONNAME LIKE CONCAT('%', #{qualificationName}, '%')" +
            "</if>" +
            "<if test='status != null and status != \"\"'>" +
            " AND STATUS = #{status}" +
            "</if>" +
            " ORDER BY CREATE_TIME DESC" +
            "</script>")
    IPage<Certificate> selectCertificatePage(Page<Certificate> page,
                                         @Param("certNo") String certNo,
                                         @Param("holderName") String holderName,
                                         @Param("qualificationName") String qualificationName,
                                         @Param("status") String status);

    /**
     * Query certificate by cert number
     */
    @Select("SELECT * FROM Certificate WHERE CERTNO = #{certNo} AND DELETED = 0")
    Certificate selectByCertNo(@Param("certNo") String certNo);

    /**
     * Query certificates by worker ID
     */
    @Select("SELECT * FROM Certificate WHERE WORKERID = #{workerId} AND DELETED = 0 ORDER BY CREATE_TIME DESC")
    List<Certificate> selectByWorkerId(@Param("workerId") Long workerId);

    /**
     * Query certificates by unit code
     */
    @Select("SELECT * FROM Certificate WHERE UNITCODE = #{unitCode} AND DELETED = 0 ORDER BY CREATE_TIME DESC")
    List<Certificate> selectByUnitCode(@Param("unitCode") String unitCode);
}
