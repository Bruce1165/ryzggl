package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.ExamSignUp;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

import java.util.Date;
import java.util.List;

/**
 * ExamSignUp Repository
 */
@Mapper
public interface ExamSignUpRepository extends BaseMapper<ExamSignUp> {

    /**
     * Get exam signups by exam plan ID
     * @param examPlanId Exam Plan ID
     * @return List of exam signups
     */
    @Select("SELECT * FROM ExamSignUp WHERE ExamPlanID = #{examPlanId}")
    List<ExamSignUp> selectByExamPlanId(@Param("examPlanId") Long examPlanId);

    /**
     * Get exam signups by worker ID
     * @param workerId Worker ID
     * @return List of exam signups
     */
    @Select("SELECT * FROM ExamSignUp WHERE WorkerID = #{workerId}")
    List<ExamSignUp> selectByWorkerId(@Param("workerId") Long workerId);

    /**
     * Get exam signups with payment confirmation in date range
     * @param startDate Start date
     * @param endDate End date
     * @return List of exam signups with payment confirmation
     */
    @Select("SELECT * FROM ExamSignUp " +
            "WHERE PayConfirmDate BETWEEN #{startDate} AND #{endDate} " +
            "AND ResultCertificateCode IS NOT NULL")
    List<ExamSignUp> selectWithPaymentConfirmation(
            @Param("startDate") String startDate,
            @Param("endDate") String endDate);

    /**
     * Get exam signups with payment confirmation by exam plan and date range
     * @param examPlanId Exam Plan ID
     * @param startDate Start date
     * @param endDate End date
     * @return List of exam signups
     */
    @Select("SELECT * FROM ExamSignUp s " +
            "INNER JOIN ExamPlan e ON s.ExamPlanID = e.ExamPlanID " +
            "WHERE e.ExamPlanID = #{examPlanId} " +
            "AND s.PayConfirmDate BETWEEN #{startDate} AND #{endDate} " +
            "AND s.ResultCertificateCode IS NOT NULL")
    List<ExamSignUp> selectByExamPlanWithPayment(
            @Param("examPlanId") Long examPlanId,
            @Param("startDate") String startDate,
            @Param("endDate") String endDate);

    /**
     * Get pending check exam signups
     * @return List of pending signups
     */
    @Select("SELECT * FROM ExamSignUp " +
            "WHERE Status = '待审核' OR Status = '未申报' " +
            "ORDER BY SignUpDate ASC")
    List<ExamSignUp> selectPendingChecks();

    /**
     * Get exam signups by status
     * @param status Status
     * @return List of exam signups
     */
    @Select("SELECT * FROM ExamSignUp WHERE Status = #{status}")
    List<ExamSignUp> selectByStatus(@Param("status") String status);

    /**
     * Count exam signups by exam plan ID
     * @param examPlanId Exam Plan ID
     * @return Count
     */
    @Select("SELECT COUNT(*) FROM ExamSignUp WHERE ExamPlanID = #{examPlanId}")
    int countByExamPlanId(@Param("examPlanId") Long examPlanId);
}
