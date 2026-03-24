package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.entity.Department;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

import java.util.List;

/**
 * Department Repository - 部门管理
 * Department management repository
 */
@Mapper
public interface DepartmentRepository extends BaseMapper<Department> {

    /**
     * Query department list with pagination
     */
    @Select("<script>" +
            "SELECT * FROM Department WHERE DELETED = 0" +
            "<if test='departmentName != null and departmentName != \"\"'>" +
            " AND DepartmentName LIKE CONCAT('%', #{departmentName}, '%')" +
            "</if>" +
            "<if test='departmentType != null and departmentType != \"\"'>" +
            " AND DepartmentType = #{departmentType}" +
            "</if>" +
            " ORDER BY SortOrder ASC, CreateTime DESC" +
            "</script>")
    IPage<Department> selectDepartmentPage(Page<Department> page,
                                          @Param("departmentName") String departmentName,
                                          @Param("departmentType") String departmentType);

    /**
     * Query department by ID
     */
    @Select("SELECT * FROM Department WHERE DepartmentID = #{id} AND DELETED = 0")
    Department selectById(@Param("id") String id);

    /**
     * Query departments by parent ID (for tree structure)
     */
    @Select("SELECT * FROM Department WHERE ParentID = #{parentId} AND DELETED = 0 ORDER BY SortOrder ASC")
    List<Department> selectByParentId(@Param("parentId") String parentId);

    /**
     * Query top-level departments (no parent)
     */
    @Select("SELECT * FROM Department WHERE (ParentID IS NULL OR ParentID = '') AND DELETED = 0 ORDER BY SortOrder ASC")
    List<Department> selectTopLevel();

    /**
     * Query departments by type
     */
    @Select("SELECT * FROM Department WHERE DepartmentType = #{type} AND DELETED = 0 ORDER BY SortOrder ASC")
    List<Department> selectByType(@Param("type") String type);

    /**
     * Query all departments (for dropdown)
     */
    @Select("SELECT * FROM Department WHERE DELETED = 0 ORDER BY SortOrder ASC, CreateTime DESC")
    List<Department> selectAll();

    /**
     * Count departments under a parent
     */
    @Select("SELECT COUNT(*) FROM Department WHERE ParentID = #{parentId} AND DELETED = 0")
    int countByParentId(@Param("parentId") String parentId);

    /**
     * Check if department has children
     */
    @Select("SELECT CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END FROM Department WHERE ParentID = #{id} AND DELETED = 0")
    int hasChildren(@Param("id") String id);
}
