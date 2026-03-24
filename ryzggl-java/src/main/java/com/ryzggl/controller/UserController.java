package com.ryzggl.controller;

import com.baomidou.mybatisplus.core.metadata.IPage;
import com.ryzggl.common.Result;
import com.ryzggl.entity.User;
import com.ryzggl.service.UserService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.springframework.web.bind.annotation.*;

import java.util.List;

/**
 * User Controller
 * User management endpoints
 */
@Tag(name = "用户管理", description = "用户管理接口")
@RestController
@RequestMapping("/api/v1/users")
public class UserController {

    private final UserService userService;

    public UserController(UserService userService) {
        this.userService = userService;
    }

    public static class UserQuery {
        private Integer current = 1;
        private Integer size = 10;
        private String username;
        private String realName;
        private Long departmentId;
        private Long roleId;
        private String status;

        public Integer getCurrent() {
            return current;
        }

        public void setCurrent(Integer current) {
            this.current = current;
        }

        public Integer getSize() {
            return size;
        }

        public void setSize(Integer size) {
            this.size = size;
        }

        public String getUsername() {
            return username;
        }

        public void setUsername(String username) {
            this.username = username;
        }

        public String getRealName() {
            return realName;
        }

        public void setRealName(String realName) {
            this.realName = realName;
        }

        public Long getDepartmentId() {
            return departmentId;
        }

        public void setDepartmentId(Long departmentId) {
            this.departmentId = departmentId;
        }

        public Long getRoleId() {
            return roleId;
        }

        public void setRoleId(Long roleId) {
            this.roleId = roleId;
        }

        public String getStatus() {
            return status;
        }

        public void setStatus(String status) {
            this.status = status;
        }
    }

    public static class PasswordResetRequest {
        private Long userId;
        private String newPassword;

        public Long getUserId() {
            return userId;
        }

        public void setUserId(Long userId) {
            this.userId = userId;
        }

        public String getNewPassword() {
            return newPassword;
        }

        public void setNewPassword(String newPassword) {
            this.newPassword = newPassword;
        }
    }

    public static class StatusChangeRequest {
        private Long userId;
        private String status;

        public Long getUserId() {
            return userId;
        }

        public void setUserId(Long userId) {
            this.userId = userId;
        }

        public String getStatus() {
            return status;
        }

        public void setStatus(String status) {
            this.status = status;
        }
    }

    /**
     * Query user list
     */
    @Operation(summary = "查询用户列表")
    @GetMapping
    public Result<IPage<User>> getUserList(UserQuery query) {
        return userService.getUserList(query.getCurrent(), query.getSize(),
                query.getUsername(), query.getRealName(),
                query.getDepartmentId(), query.getRoleId(), query.getStatus());
    }

    /**
     * Query all active users
     */
    @Operation(summary = "查询所有活跃用户")
    @GetMapping("/active")
    public Result<List<User>> getActiveUsers() {
        return userService.getActiveUsers();
    }

    /**
     * Query user by ID
     */
    @Operation(summary = "查询用户详情")
    @GetMapping("/{id}")
    public Result<User> getUserById(@PathVariable Long id) {
        return userService.getUserById(id);
    }

    /**
     * Query user by username
     */
    @Operation(summary = "根据用户名查询用户")
    @GetMapping("/username/{username}")
    public Result<User> getUserByUsername(@PathVariable String username) {
        return userService.getUserByUsername(username);
    }

    /**
     * Create user
     */
    @Operation(summary = "创建用户")
    @PostMapping
    public Result<User> createUser(@RequestBody User user) {
        return userService.createUser(user);
    }

    /**
     * Update user
     */
    @Operation(summary = "更新用户")
    @PutMapping("/{id}")
    public Result<User> updateUser(@PathVariable Long id, @RequestBody User user) {
        user.setId(id);
        return userService.updateUser(user);
    }

    /**
     * Delete user
     */
    @Operation(summary = "删除用户")
    @DeleteMapping("/{id}")
    public Result<Boolean> deleteUser(@PathVariable Long id) {
        return userService.deleteUser(id);
    }

    /**
     * Reset user password
     */
    @Operation(summary = "重置用户密码")
    @PutMapping("/reset-password")
    public Result<Void> resetPassword(@RequestBody PasswordResetRequest request) {
        return userService.resetPassword(request.getUserId(), request.getNewPassword());
    }

    /**
     * Change user status
     */
    @Operation(summary = "更改用户状态")
    @PutMapping("/change-status")
    public Result<Void> changeUserStatus(@RequestBody StatusChangeRequest request) {
        return userService.changeUserStatus(request.getUserId(), request.getStatus());
    }
}
