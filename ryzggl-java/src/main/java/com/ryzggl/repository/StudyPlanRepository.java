package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.StudyPlan;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;
import org.apache.ibatis.annotations.Update;

import java.util.List;

/**
 * StudyPlan Repository
 * 学习计划数据访问层
 */
public interface StudyPlanRepository extends BaseMapper<StudyPlan> {

    /**
     * Get study plan by worker certificate code and package ID
     */
    @Select("SELECT * FROM StudyPlan WHERE WorkerCertificateCode = #{workerCertificateCode} AND PackageID = #{packageId}")
    StudyPlan getByWorkerCertificateAndPackage(@Param("workerCertificateCode") String workerCertificateCode, @Param("packageId") Long packageId);

    /**
     * Get study plans by worker certificate code
     */
    @Select("SELECT * FROM StudyPlan WHERE WorkerCertificateCode = #{workerCertificateCode} ORDER BY CreateTime DESC")
    List<StudyPlan> getByWorkerCertificateCode(@Param("workerCertificateCode") String workerCertificateCode);

    /**
     * Get study plans by package ID
     */
    @Select("SELECT * FROM StudyPlan WHERE PackageID = #{packageId} ORDER BY CreateTime DESC")
    List<StudyPlan> getByPackageId(@Param("packageId") Long packageId);

    /**
     * Search study plans by worker name
     */
    @Select("SELECT * FROM StudyPlan WHERE WorkerName LIKE CONCAT('%', #{workerName}, '%') ORDER BY CreateTime DESC")
    List<StudyPlan> searchByWorkerName(@Param("workerName") String workerName);

    /**
     * Search study plans by keyword
     */
    @Select("SELECT * FROM StudyPlan WHERE (WorkerName LIKE CONCAT('%', #{keyword}, '%') OR Creater LIKE CONCAT('%', #{keyword}, '%') OR AddType LIKE CONCAT('%', #{keyword}, '%')) ORDER BY CreateTime DESC")
    List<StudyPlan> search(@Param("keyword") String keyword);

    /**
     * Create study plan
     */
    @Insert("INSERT INTO StudyPlan(WorkerCertificateCode, PackageID, WorkerName, CreateTime, Creater, AddType, FinishDate, TestStatus) VALUES (#{workerCertificateCode}, #{packageId}, #{workerName}, #{createTime}, #{creater}, #{addType}, #{finishDate}, #{testStatus})")
    int insertStudyPlan(StudyPlan studyPlan);

    /**
     * Update study plan
     */
    @Update("UPDATE StudyPlan SET WorkerName = #{workerName}, CreateTime = #{createTime}, Creater = #{creater}, AddType = #{addType}, FinishDate = #{finishDate}, TestStatus = #{testStatus} WHERE WorkerCertificateCode = #{workerCertificateCode} AND PackageID = #{packageId}")
    int updateStudyPlan(@Param("workerCertificateCode") String workerCertificateCode, @Param("packageId") Long packageId, @Param("workerName") String workerName,
                       @Param("createTime") String createTime, @Param("creater") String creater,
                       @Param("addType") String addType, @Param("finishDate") String finishDate, @Param("testStatus") String testStatus);

    /**
     * Delete study plan
     */
    @Delete("DELETE FROM StudyPlan WHERE WorkerCertificateCode = #{workerCertificateCode} AND PackageID = #{packageId}")
    int deleteStudyPlan(@Param("workerCertificateCode") String workerCertificateCode, @Param("packageId") Long packageId);
}
