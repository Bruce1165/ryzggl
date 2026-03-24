package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.UserRole;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;
import org.apache.ibatis.annotations.Insert;
import org.apache.ibatis.annotations.Update;
import org.apache.ibatis.annotations.Delete;

import java.util.List;

/**
 * UserRole Repository
 * 用户角色数据访问层
 */
public interface UserRoleRepository extends BaseMapper<UserRole> {

    /**
     * Get by ID
     */
    @Select("SELECT * FROM UserRole WHERE ID = #{id}")
    UserRole getById(@Param("id") Integer id);

    /**
     * Get by user ID
     */
    @Select("SELECT * FROM UserRole WHERE UserID = #{userId} ORDER BY ID")
    List<UserRole> getByUserId(@Param("userId") String userId);

    /**
     * Get by role ID
     */
    @Select("SELECT * FROM UserRole WHERE RoleID = #{roleId} ORDER BY ID")
    List<UserRole> getByRoleId(@Param("roleId") String roleId);

    /**
     * Get by user ID and role ID
     */
    @Select("SELECT * FROM UserRole WHERE UserID = #{userId} AND RoleID = #{roleId} ORDER BY ID")
    UserRole getByUserIdAndRoleId(@Param("userId") String userId, @Param("roleId") String roleId);

    /**
     * Check if user has role
     */
    @Select("SELECT COUNT(*) FROM UserRole WHERE UserID = #{userId} AND RoleID = #{roleId}")
    int hasRole(@Param("userId") String userId, @Param("roleId") String roleId);

    /**
     * Create user role
     */
    @Insert("INSERT INTO UserRole(UserID, RoleID) VALUES (#{userId}, #{roleId})")
    int insertUserRole(UserRole userRole);

    /**
     * Delete user role
     */
    @Delete("DELETE FROM UserRole WHERE ID = #{id}")
    int deleteUserRole(@Param("id") Integer id);

    /**
     * Delete all roles for user
     */
    @Delete("DELETE FROM UserRole WHERE UserID = #{userId}")
    int deleteByUserId(@Param("userId") String userId);
}
