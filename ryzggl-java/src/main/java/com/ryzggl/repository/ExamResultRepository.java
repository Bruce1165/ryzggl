package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.ExamResult;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;
import org.apache.ibatis.annotations.Update;

import java.util.List;

/**
 * ExamResult Repository
 */
@Mapper
public interface ExamResultRepository extends BaseMapper<ExamResult> {

    /**
     * Get exam results by exam plan ID
     * @param examPlanId Exam Plan ID
     * @return List of exam results
     */
    @Select("SELECT * FROM ExamResult WHERE ExamPlanID = #{examPlanId}")
    List<ExamResult> selectByExamPlanId(@Param("examPlanId") Long examPlanId);

    /**
     * Get exam results by worker ID
     * @param workerId Worker ID
     * @return List of exam results
     */
    @Select("SELECT * FROM ExamResult WHERE WorkerID = #{workerId}")
    List<ExamResult> selectByWorkerId(@Param("workerId") Long workerId);

    /**
     * Get exam results by status
     * @param status Status
     * @return List of exam results
     */
    @Select("SELECT * FROM ExamResult WHERE Status = #{status}")
    List<ExamResult> selectByStatus(@Param("status") String status);

    /**
     * Get exam results by result (pass/fail)
     * @param examResult Exam result (合格/不合格)
     * @return List of exam results
     */
    @Select("SELECT * FROM ExamResult WHERE ExamResult = #{examResult}")
    List<ExamResult> selectByResult(@Param("examResult") String examResult);

    /**
     * Get exam results by exam card ID
     * @param examCardId Exam card ID
     * @return Exam result
     */
    @Select("SELECT * FROM ExamResult WHERE ExamCardID = #{examCardId}")
    ExamResult selectByExamCardId(@Param("examCardId") String examCardId);

    /**
     * Count passed exam results by exam plan ID
     * @param examPlanId Exam Plan ID
     * @return Count of passed results
     */
    @Select("SELECT COUNT(*) FROM ExamResult " +
            "WHERE ExamPlanID = #{examPlanId} AND ExamResult = '合格'")
    int countPassedByExamPlanId(@Param("examPlanId") Long examPlanId);

    /**
     * Count total exam results by exam plan ID
     * @param examPlanId Exam Plan ID
     * @return Count of total results
     */
    @Select("SELECT COUNT(*) FROM ExamResult WHERE ExamPlanID = #{examPlanId}")
    int countByExamPlanId(@Param("examPlanId") Long examPlanId);

    /**
     * Delete exam results by exam plan ID
     * @param examPlanId Exam Plan ID
     * @return Number of affected rows
     */
    @Delete("DELETE FROM ExamResult WHERE ExamPlanID = #{examPlanId}")
    int deleteByExamPlanId(@Param("examPlanId") Long examPlanId);

    /**
     * Publish exam results for an exam plan
     * @param examPlanId Exam Plan ID
     * @return Number of affected rows
     */
    @Update("UPDATE ExamResult " +
            "SET Status = '已发布', ModifyTime = GETDATE() " +
            "WHERE ExamPlanID = #{examPlanId}")
    int publishByExamPlanId(@Param("examPlanId") Long examPlanId);

    /**
     * Get exam results with signup details (via view)
     * @param examPlanId Exam Plan ID
     * @return List of detailed exam results
     */
    @Select("SELECT * FROM View_ExamResult WHERE ExamPlanID = #{examPlanId}")
    List<ExamResult> selectViewByExamPlanId(@Param("examPlanId") Long examPlanId);

    /**
     * Get exam results with exam card information
     * @param examPlanId Exam Plan ID
     * @return List of exam results with worker info
     */
    @Select("SELECT er.*, es.WorkerName, es.CertificateCode, es.UnitName " +
            "FROM ExamResult er " +
            "LEFT JOIN ExamSignUp es ON er.ExamSignUp_ID = es.ExamSignUpID " +
            "WHERE er.ExamPlanID = #{examPlanId}")
    List<ExamResult> selectWithWorkerInfo(@Param("examPlanId") Long examPlanId);
}
