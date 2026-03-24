package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.entity.User;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

import java.util.List;

/**
 * User Repository - 用户管理
 * User management repository
 */
@Mapper
public interface UserRepository extends BaseMapper<User> {

    /**
     * Query user list with pagination
     */
    @Select("<script>" +
            "SELECT * FROM [User] WHERE DELETED = 0" +
            "<if test='username != null and username != \"\"'>" +
            " AND USERNAME LIKE CONCAT('%', #{username}, '%')" +
            "</if>" +
            "<if test='realName != null and realName != \"\"'>" +
            " AND REALNAME LIKE CONCAT('%', #{realName}, '%')" +
            "</if>" +
            "<if test='departmentId != null'>" +
            " AND DEPARTMENTID = #{departmentId}" +
            "</if>" +
            "<if test='roleId != null'>" +
            " AND ROLEID = #{roleId}" +
            "</if>" +
            "<if test='status != null and status != \"\"'>" +
            " AND STATUS = #{status}" +
            "</if>" +
            " ORDER BY CREATE_TIME DESC" +
            "</script>")
    IPage<User> selectUserPage(Page<User> page,
                              @Param("username") String username,
                              @Param("realName") String realName,
                              @Param("departmentId") Long departmentId,
                              @Param("roleId") Long roleId,
                              @Param("status") String status);

    /**
     * Query user by username
     */
    @Select("SELECT * FROM [User] WHERE USERNAME = #{username} AND DELETED = 0")
    User selectByUsername(@Param("username") String username);

    /**
     * Query user by phone
     */
    @Select("SELECT * FROM [User] WHERE PHONE = #{phone} AND DELETED = 0")
    User selectByPhone(@Param("phone") String phone);

    /**
     * Query user by ID
     */
    @Select("SELECT * FROM [User] WHERE ID = #{id} AND DELETED = 0")
    User selectById(@Param("id") Long id);

    /**
     * Query users by role ID
     */
    @Select("SELECT * FROM [User] WHERE ROLEID = #{roleId} AND DELETED = 0 ORDER BY CREATE_TIME DESC")
    List<User> selectByRoleId(@Param("roleId") Long roleId);

    /**
     * Query users by department ID
     */
    @Select("SELECT * FROM [User] WHERE DEPARTMENTID = #{departmentId} AND DELETED = 0 ORDER BY CREATE_TIME DESC")
    List<User> selectByDepartmentId(@Param("departmentId") Long departmentId);

    /**
     * Check if username exists
     */
    @Select("SELECT CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END FROM [User] WHERE USERNAME = #{username} AND DELETED = 0")
    int existsByUsername(@Param("username") String username);

    /**
     * Query all active users
     */
    @Select("SELECT * FROM [User] WHERE STATUS = '正常' AND DELETED = 0 ORDER BY CREATE_TIME DESC")
    List<User> selectActiveUsers();
}
