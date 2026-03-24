package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.ExamResult;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

import java.util.List;

/**
 * ExamResult Repository - Data access for exam scores and results
 */
@Mapper
public interface ExamResultRepository extends BaseMapper<ExamResult> {

    /**
     * Find by exam registration ID
     * Maps to: ExamResultDAL.GetResultsByExamID
     */
    @Select("SELECT * FROM EXAMRESULT WHERE examsignid = #{examSignUpId} ORDER BY resultdate DESC")
    List<ExamResult> findByExamSignUpId(@Param("examSignUpId") Long examSignUpId);

    /**
     * Find by worker ID
     * Maps to: ExamResultDAL.GetResultsByWorkerID
     */
    @Select("SELECT * FROM EXAMRESULT WHERE examsignid IN (SELECT EXAMSIGNID FROM EXAMSIGNUP WHERE workerid = #{workerId}) ORDER BY resultdate DESC")
    List<ExamResult> findByWorkerId(@Param("workerId") Long workerId);

    /**
     * Find all results for a specific exam
     */
    @Select("SELECT * FROM EXAMRESULT WHERE examsignid = #{examSignUpId} ORDER BY resultdate DESC")
    List<ExamResult> findAllByExam(@Param("examSignUpId") Long examSignUpId);
}
