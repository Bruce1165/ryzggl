package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;

/**
 * UserRole Entity
 * 用户角色关联实体类
 *
 * Maps to legacy UserRoleDAL.cs and UserRoleMDL.cs
 * Table: UserRole
 *
 * Business Context:
 * - Maps users to roles (many-to-many relationship)
 * - A user can have multiple roles
 * - Used for authorization management
 * - Integrates with Role entity
 */
@TableName("UserRole")
public class UserRole {

    /**
     * Primary key - ID
     * ID (主键)
     */
    @TableId(type = IdType.AUTO)
    private Integer id;

    /**
     * User ID
     * 用户ID
     */
    private String userId;

    /**
     * Role ID
     * 角色ID
     */
    private String roleId;

    // Getters and Setters
    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public String getUserId() {
        return userId;
    }

    public void setUserId(String userId) {
        this.userId = userId;
    }

    public String getRoleId() {
        return roleId;
    }

    public void setRoleId(String roleId) {
        this.roleId = roleId;
    }
}
