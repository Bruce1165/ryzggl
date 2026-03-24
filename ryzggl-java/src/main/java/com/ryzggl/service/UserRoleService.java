package com.ryzggl.service;

import com.baomidou.mybatisplus.extension.service.IService;
import com.ryzggl.entity.UserRole;
import com.ryzggl.repository.UserRoleRepository;
import com.ryzggl.common.Result;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;

/**
 * UserRole Service
 * 用户角色业务逻辑层
 */
@Service
public class UserRoleService implements IService<UserRole> {

    @Autowired
    private UserRoleRepository userRoleRepository;

    /**
     * Get by ID
     */
    public UserRole getById(Integer id) {
        return userRoleRepository.getById(id);
    }

    /**
     * Get by user ID
     */
    public List<UserRole> getByUserId(String userId) {
        return userRoleRepository.getByUserId(userId);
    }

    /**
     * Get by role ID
     */
    public List<UserRole> getByRoleId(String roleId) {
        return userRoleRepository.getByRoleId(roleId);
    }

    /**
     * Get by user ID and role ID
     */
    public UserRole getByUserIdAndRoleId(String userId, String roleId) {
        return userRoleRepository.getByUserIdAndRoleId(userId, roleId);
    }

    /**
     * Check if user has role
     */
    public boolean hasRole(String userId, String roleId) {
        return userRoleRepository.hasRole(userId, roleId) > 0;
    }

    /**
     * Create user role
     */
    @Transactional
    public Result<UserRole> createUserRole(UserRole userRole) {
        // Validate required fields
        if (userRole.getUserId() == null || userRole.getUserId().trim().isEmpty()) {
            return Result.error("用户ID不能为空");
        }
        if (userRole.getRoleId() == null || userRole.getRoleId().trim().isEmpty()) {
            return Result.error("角色ID不能为空");
        }

        userRoleRepository.insertUserRole(userRole);
        return Result.success(userRole);
    }

    /**
     * Delete user role
     */
    @Transactional
    public Result<Void> deleteUserRole(Integer id) {
        UserRole existing = getById(id);
        if (existing == null) {
            return Result.error("用户角色关联不存在");
        }

        userRoleRepository.deleteUserRole(id);
        return Result.success();
    }

    /**
     * Delete all roles for a user
     */
    @Transactional
    public Result<Void> deleteByUserId(String userId) {
        userRoleRepository.deleteByUserId(userId);
        return Result.success();
    }
}
