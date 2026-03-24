package com.ryzggl.service;

import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.ryzggl.common.Result;
import com.ryzggl.entity.User;
import com.ryzggl.repository.UserRepository;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

/**
 * User Service - 用户管理
 * User management service
 */
@Service
public class UserService extends ServiceImpl<UserRepository, User> {

    private static final Logger logger = LoggerFactory.getLogger(UserService.class);

    private final UserRepository userRepository;
    private final PasswordEncoder passwordEncoder;

    public UserService(UserRepository userRepository, PasswordEncoder passwordEncoder) {
        this.userRepository = userRepository;
        this.passwordEncoder = passwordEncoder;
    }

    /**
     * Query user list with pagination
     */
    public Result<IPage<User>> getUserList(Integer current, Integer size,
                                          String username, String realName,
                                          Long departmentId, Long roleId, String status) {
        Page<User> page = new Page<>(current, size);
        IPage<User> result = userRepository.selectUserPage(page, username, realName, departmentId, roleId, status);
        return Result.success(result);
    }

    /**
     * Query user by ID
     */
    public Result<User> getUserById(Long id) {
        User user = userRepository.selectById(id);
        if (user == null) {
            return Result.error("用户不存在");
        }
        // Clear password before returning
        user.setPassword(null);
        return Result.success(user);
    }

    /**
     * Query user by username
     */
    public Result<User> getUserByUsername(String username) {
        User user = userRepository.selectByUsername(username);
        if (user == null) {
            return Result.error("用户不存在");
        }
        user.setPassword(null);
        return Result.success(user);
    }

    /**
     * Create user
     */
    @Transactional
    public Result<User> createUser(User user) {
        // Check if username already exists
        int exists = userRepository.existsByUsername(user.getUsername());
        if (exists > 0) {
            return Result.error("用户名已存在");
        }

        // Encode password
        if (user.getPassword() != null && !user.getPassword().isEmpty()) {
            user.setPassword(passwordEncoder.encode(user.getPassword()));
        }

        // Set default status
        if (user.getStatus() == null || user.getStatus().isEmpty()) {
            user.setStatus("正常");
        }

        user.setCreateBy("system");
        user.setUpdateBy("system");

        save(user);
        logger.info("Created user: username={}", user.getUsername());

        // Clear password before returning
        user.setPassword(null);
        return Result.success("用户创建成功", user);
    }

    /**
     * Update user
     */
    @Transactional
    public Result<User> updateUser(User user) {
        User existUser = userRepository.selectById(user.getId());
        if (existUser == null) {
            return Result.error("用户不存在");
        }

        // Check if username is being changed and already exists
        if (!existUser.getUsername().equals(user.getUsername())) {
            int exists = userRepository.existsByUsername(user.getUsername());
            if (exists > 0) {
                return Result.error("用户名已存在");
            }
        }

        // Encode password if provided
        if (user.getPassword() != null && !user.getPassword().isEmpty()) {
            user.setPassword(passwordEncoder.encode(user.getPassword()));
        } else {
            // Keep existing password
            user.setPassword(existUser.getPassword());
        }

        user.setUpdateBy("system");

        updateById(user);
        logger.info("Updated user: username={}", user.getUsername());

        // Clear password before returning
        user.setPassword(null);
        return Result.success("用户更新成功", user);
    }

    /**
     * Delete user
     */
    @Transactional
    public Result<Boolean> deleteUser(Long id) {
        User user = userRepository.selectById(id);
        if (user == null) {
            return Result.error("用户不存在");
        }

        // Prevent deleting the current user or admin users
        if ("admin".equals(user.getUsername())) {
            return Result.error("不能删除管理员用户");
        }

        boolean deleted = removeById(id);
        if (deleted) {
            logger.info("Deleted user: username={}", user.getUsername());
        }
        return Result.success(deleted);
    }

    /**
     * Reset user password
     */
    @Transactional
    public Result<Void> resetPassword(Long id, String newPassword) {
        User user = userRepository.selectById(id);
        if (user == null) {
            return Result.error("用户不存在");
        }

        // Encode new password
        String encodedPassword = passwordEncoder.encode(newPassword);
        user.setPassword(encodedPassword);
        user.setUpdateBy("system");

        updateById(user);
        logger.info("Reset password for user: username={}", user.getUsername());
        return Result.success("密码重置成功");
    }

    /**
     * Change user status
     */
    @Transactional
    public Result<Void> changeUserStatus(Long id, String status) {
        User user = userRepository.selectById(id);
        if (user == null) {
            return Result.error("用户不存在");
        }

        // Prevent disabling admin users
        if ("admin".equals(user.getUsername()) && !"正常".equals(status)) {
            return Result.error("不能禁用管理员用户");
        }

        user.setStatus(status);
        user.setUpdateBy("system");

        updateById(user);
        logger.info("Changed status for user: username={}, status={}", user.getUsername(), status);
        return Result.success("用户状态更新成功");
    }

    /**
     * Get all active users
     */
    public Result<java.util.List<User>> getActiveUsers() {
        java.util.List<User> users = userRepository.selectActiveUsers();
        // Clear passwords
        users.forEach(u -> u.setPassword(null));
        return Result.success(users);
    }
}
