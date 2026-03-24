package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.entity.ExamPlace;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

/**
 * ExamPlace Repository
 * 考点数据访问层
 *
 * MyBatis-Plus mapper for ExamPlace entity
 * Provides CRUD operations and custom queries
 */
public interface ExamPlaceRepository extends BaseMapper<ExamPlace> {

    /**
     * Get ExamPlace by Exam Place Name
     * 根据考点名称获取考点
     *
     * @param examPlaceName Exam Place name
     * @return ExamPlace entity or null
     */
    @Select("SELECT * FROM ExamPlace WHERE ExamPlaceName = #{examPlaceName}")
    ExamPlace getByExamPlaceName(@Param("examPlaceName") String examPlaceName);

    /**
     * Get ExamPlace by Exam Place Name with status filter
     * 根据考点名称获取考点（带状态过滤）
     *
     * @param examPlaceName Exam Place name
     * @param status Status filter
     * @return ExamPlace entity or null
     */
    @Select("SELECT TOP 1 * FROM ExamPlace WHERE ExamPlaceName = #{examPlaceName} AND Status = #{status}")
    ExamPlace getByExamPlaceNameAndStatus(@Param("examPlaceName") String examPlaceName,
                                         @Param("status") String status);

    /**
     * Get ExamPlace list with pagination
     * 获取考点列表（分页）
     *
     * @param page Page object
     * @param status Filter by status (optional)
     * @return Page of ExamPlace entities
     */
    @Select("<script>" +
            "SELECT * FROM ExamPlace " +
            "<where>" +
            "  <if test='status != null and status != \"\"'>" +
            "    AND Status = #{status}" +
            "  </if>" +
            "</where>" +
            "ORDER BY ExamPlaceID DESC" +
            "</script>")
    IPage<ExamPlace> getByStatus(Page<ExamPlace> page, @Param("status") String status);

    /**
     * Search ExamPlace by name
     * 搜索考点（按名称）
     *
     * @param page Page object
     * @param examPlaceName Exam Place name (supports partial match)
     * @param status Status filter (optional)
     * @return Page of ExamPlace entities
     */
    @Select("<script>" +
            "SELECT * FROM ExamPlace " +
            "<where>" +
            "  <if test='examPlaceName != null and examPlaceName != \"\"'>" +
            "    AND ExamPlaceName LIKE CONCAT('%', #{examPlaceName}, '%')" +
            "  </if>" +
            "  <if test='status != null and status != \"\"'>" +
            "    AND Status = #{status}" +
            "  </if>" +
            "</where>" +
            "ORDER BY ExamPlaceID DESC" +
            "</script>")
    IPage<ExamPlace> search(Page<ExamPlace> page,
                         @Param("examPlaceName") String examPlaceName,
                         @Param("status") String status);

    /**
     * Get active exam places
     * 获取可用的考点列表
     *
     * @param page Page object
     * @return Page of ExamPlace entities
     */
    @Select("SELECT * FROM ExamPlace WHERE Status = 'ACTIVE' ORDER BY ExamPlaceID DESC")
    IPage<ExamPlace> getActivePlaces(Page<ExamPlace> page);

    /**
     * Count ExamPlace by status
     * 根据状态统计考点数量
     *
     * @param status Exam Place status
     * @return Count of ExamPlace entities
     */
    @Select("<script>" +
            "SELECT COUNT(*) FROM ExamPlace " +
            "<where>" +
            "  <if test='status != null and status != \"\"'>" +
            "    AND Status = #{status}" +
            "  </if>" +
            "</where>" +
            "</script>")
    long countByStatus(@Param("status") String status);

    /**
     * Check if ExamPlace name exists
     * 检查考点名称是否存在
     *
     * @param examPlaceName Exam Place name
     * @return Count (0 or 1)
     */
    @Select("SELECT COUNT(*) FROM ExamPlace WHERE ExamPlaceName = #{examPlaceName}")
    int existsByExamPlaceName(@Param("examPlaceName") String examPlaceName);

    /**
     * Get ExamPlace with available capacity
     * 获取有容量的考点列表
     *
     * @param page Page object
     * @param requiredCapacity Required minimum capacity
     * @return Page of ExamPlace entities
     */
    @Select("<script>" +
            "SELECT * FROM ExamPlace " +
            "<where>" +
            "  <if test='requiredCapacity != null'>" +
            "    AND ExamPersonNum >= #{requiredCapacity}" +
            "  </if>" +
            "  <if test='status != null and status != \"\"'>" +
            "    AND Status = #{status}" +
            "  </if>" +
            "</where>" +
            "ORDER BY ExamPersonNum DESC" +
            "</script>")
    IPage<ExamPlace> getPlacesWithCapacity(Page<ExamPlace> page,
                                        @Param("requiredCapacity") Integer requiredCapacity,
                                        @Param("status") String status);
}
