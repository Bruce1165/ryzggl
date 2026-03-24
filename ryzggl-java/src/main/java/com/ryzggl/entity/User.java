package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableName;
import com.ryzggl.common.BaseEntity;

/**
 * User Entity - 用户表
 */
@TableName("User")
public class User extends BaseEntity {

    /**
     * 用户名
     */
    @TableField("USERNAME")
    private String username;

    /**
     * 密码（加密）
     */
    @TableField("PASSWORD")
    private String password;

    /**
     * 真实姓名
     */
    @TableField("REALNAME")
    private String realName;

    /**
     * 手机号
     */
    @TableField("PHONE")
    private String phone;

    /**
     * 邮箱
     */
    @TableField("EMAIL")
    private String email;

    /**
     * 所属企业代码
     */
    @TableField("UNITCODE")
    private String unitCode;

    /**
     * 企业名称
     */
    @TableField("UNITNAME")
    private String unitName;

    /**
     * 部门ID
     */
    @TableField("DEPARTMENTID")
    private Long departmentId;

    /**
     * 部门名称
     */
    @TableField("DEPARTMENTNAME")
    private String departmentName;

    /**
     * 角色ID
     */
    @TableField("ROLEID")
    private Long roleId;

    /**
     * 角色名称
     */
    @TableField("ROLENAME")
    private String roleName;

    /**
     * 用户状态（正常、停用）
     */
    @TableField("STATUS")
    private String status;

    // Getters and Setters
    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public String getRealName() {
        return realName;
    }

    public void setRealName(String realName) {
        this.realName = realName;
    }

    public String getPhone() {
        return phone;
    }

    public void setPhone(String phone) {
        this.phone = phone;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getUnitCode() {
        return unitCode;
    }

    public void setUnitCode(String unitCode) {
        this.unitCode = unitCode;
    }

    public String getUnitName() {
        return unitName;
    }

    public void setUnitName(String unitName) {
        this.unitName = unitName;
    }

    public Long getDepartmentId() {
        return departmentId;
    }

    public void setDepartmentId(Long departmentId) {
        this.departmentId = departmentId;
    }

    public String getDepartmentName() {
        return departmentName;
    }

    public void setDepartmentName(String departmentName) {
        this.departmentName = departmentName;
    }

    public Long getRoleId() {
        return roleId;
    }

    public void setRoleId(Long roleId) {
        this.roleId = roleId;
    }

    public String getRoleName() {
        return roleName;
    }

    public void setRoleName(String roleName) {
        this.roleName = roleName;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        if (!super.equals(o)) return false;
        User user = (User) o;
        return username != null && username.equals(user.username);
    }

    @Override
    public int hashCode() {
        return super.hashCode() * 31 + (username != null ? username.hashCode() : 0);
    }
}
