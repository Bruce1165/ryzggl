package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.LearnRecord;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;
import org.apache.ibatis.annotations.Update;

import java.util.List;

/**
 * LearnRecord Repository
 * 学习记录数据访问层
 */
public interface LearnRecordRepository extends BaseMapper<LearnRecord> {

    /**
     * Get record by ID
     */
    @Select("SELECT * FROM earnRecord WHERE RecordID = #{recordId}")
    LearnRecord getById(@Param("recordId") Long recordId);

    /**
     * Get records by worker certificate code
     */
    @Select("SELECT * FROM earnRecord WHERE WorkerCertificateCode = #{workerCertificateCode} ORDER BY RecordID DESC")
    List<LearnRecord> getByWorkerCertificateCode(@Param("workerCertificateCode") String workerCertificateCode);

    /**
     * Get records by post name
     */
    @Select("SELECT * FROM earnRecord WHERE PostName LIKE CONCAT('%', #{postName}, '%') ORDER BY RecordID DESC")
    List<LearnRecord> getByPostName(@Param("postName") String postName);

    /**
     * Get records by worker name
     */
    @Select("SELECT * FROM earnRecord WHERE WorkerName LIKE CONCAT('%', #{workerName}, '%') ORDER BY RecordID DESC")
    List<LearnRecord> getByWorkerName(@Param("workerName") String workerName);

    /**
     * Search records by keyword
     */
    @Select("SELECT * FROM earnRecord WHERE (WorkerName LIKE CONCAT('%', #{keyword}, '%') OR PostName LIKE CONCAT('%', #{keyword}, '%') OR LinkTel LIKE CONCAT('%', #{keyword}, '%')) ORDER BY RecordID DESC")
    List<LearnRecord> search(@Param("keyword") String keyword);

    /**
     * Update record
     */
    @Update("UPDATE earnRecord SET RecordNo = #{recordNo}, PostName = #{postName}, WorkerName = #{workerName}, LinkTel = #{linkTel}, CertificateCode = #{certificateCode}, WorkerCertificateCode = #{workerCertificateCode}, ClassHour = #{classHour} WHERE RecordID = #{recordId}")
    int updateRecord(@Param("recordId") Long recordId, @Param("recordNo") String recordNo, @Param("postName") String postName,
                  @Param("workerName") String workerName, @Param("linkTel") String linkTel,
                  @Param("certificateCode") String certificateCode, @Param("workerCertificateCode") String workerCertificateCode,
                  @Param("classHour") String classHour);
}
