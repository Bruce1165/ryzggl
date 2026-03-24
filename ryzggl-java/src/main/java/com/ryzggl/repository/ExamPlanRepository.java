package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.ExamPlan;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

import java.util.Date;
import java.util.List;

/**
 * ExamPlan Repository
 */
@Mapper
public interface ExamPlanRepository extends BaseMapper<ExamPlan> {

    /**
     * Get exam plans by post type ID
     * @param postTypeId Post Type ID (1=安管人员, 2=特种作业)
     * @return List of exam plans
     */
    @Select("SELECT * FROM ExamPlan WHERE PostTypeID = #{postTypeId}")
    List<ExamPlan> selectByPostTypeId(@Param("postTypeId") Integer postTypeId);

    /**
     * Get published exam plans
     * @return List of published exam plans
     */
    @Select("SELECT * FROM ExamPlan WHERE IfPublish = '是' " +
            "ORDER BY ExamStartDate DESC")
    List<ExamPlan> selectPublished();

    /**
     * Get active exam plans (within sign up period)
     * @return List of active exam plans
     */
    @Select("SELECT * FROM ExamPlan " +
            "WHERE Status = '已发布' " +
            "AND GETDATE() BETWEEN SignUpStartDate AND SignUpEndDate " +
            "ORDER BY SignUpStartDate ASC")
    List<ExamPlan> selectActive();

    /**
     * Get exam plans by date range
     * @param startDate Start date
     * @param endDate End date
     * @return List of exam plans
     */
    @Select("SELECT * FROM ExamPlan " +
            "WHERE ExamStartDate BETWEEN #{startDate} AND #{endDate} " +
            "ORDER BY ExamStartDate ASC")
    List<ExamPlan> selectByDateRange(
            @Param("startDate") Date startDate,
            @Param("endDate") Date endDate);

    /**
     * Get exam plan by name
     * @param examPlanName Exam plan name
     * @return Exam plan
     */
    @Select("SELECT * FROM ExamPlan WHERE ExamPlanName = #{examPlanName}")
    ExamPlan selectByName(@Param("examPlanName") String examPlanName);

    /**
     * Get exam plans with person limit check
     * @param examPlanId Exam Plan ID
     * @return Exam plan with count
     */
    @Select("SELECT p.*, " +
            "(SELECT COUNT(*) FROM ExamSignUp WHERE ExamPlanID = p.ExamPlanID) as SignUpCount " +
            "FROM ExamPlan p " +
            "WHERE p.ExamPlanID = #{examPlanId}")
    ExamPlan selectWithSignUpCount(@Param("examPlanId") Long examPlanId);

    /**
     * Get upcoming exam plans
     * @param limit Maximum number of records
     * @return List of upcoming exam plans
     */
    @Select("SELECT TOP(${limit}) * FROM ExamPlan " +
            "WHERE Status = '已发布' " +
            "AND ExamStartDate > GETDATE() " +
            "ORDER BY ExamStartDate ASC")
    List<ExamPlan> selectUpcoming(@Param("limit") int limit);
}
