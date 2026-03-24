package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.entity.Exam;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

import java.util.List;

/**
 * Exam Repository
 */
@Mapper
public interface ExamRepository extends BaseMapper<Exam> {

    /**
     * Query exam list with pagination
     */
    @Select("<script>" +
            "SELECT * FROM Exam WHERE DELETED = 0" +
            "<if test='examName != null and examName != \"\"'>" +
            " AND EXAMNAME LIKE CONCAT('%', #{examName}, '%')" +
            "</if>" +
            "<if test='qualificationName != null and qualificationName != \"\"'>" +
            " AND QUALIFICATIONNAME LIKE CONCAT('%', #{qualificationName}, '%')" +
            "</if>" +
            "<if test='status != null and status != \"\"'>" +
            " AND STATUS = #{status}" +
            "</if>" +
            " ORDER BY EXAMDATE DESC" +
            "</script>")
    IPage<Exam> selectExamPage(Page<Exam> page,
                              @Param("examName") String examName,
                              @Param("qualificationName") String qualificationName,
                              @Param("status") String status);

    /**
     * Query exam by exam code
     */
    @Select("SELECT * FROM Exam WHERE EXAMCODE = #{examCode} AND DELETED = 0")
    Exam selectByExamCode(@Param("examCode") String examCode);

    /**
     * Query exams by status
     */
    @Select("SELECT * FROM Exam WHERE STATUS = #{status} AND DELETED = 0 ORDER BY EXAMDATE DESC")
    List<Exam> selectByStatus(@Param("status") String status);

    /**
     * Query available exams (sign up not ended)
     */
    @Select("SELECT * FROM Exam WHERE STATUS = '未开始' AND SIGNUPEND >= GETDATE() AND DELETED = 0 ORDER BY EXAMDATE ASC")
    List<Exam> selectAvailableExams();
}
