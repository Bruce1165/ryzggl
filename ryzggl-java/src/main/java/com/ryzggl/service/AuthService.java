package com.ryzggl.service;

import com.ryzggl.common.JwtTokenProvider;
import com.ryzggl.common.Result;
import com.ryzggl.entity.User;
import com.ryzggl.repository.UserRepository;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;

import java.util.Collections;

/**
 * Authentication Service
 * Test mode: returns mock data without database
 */
@Service
public class AuthService implements UserDetailsService {

    private final UserRepository userRepository;
    private final PasswordEncoder passwordEncoder;
    private final JwtTokenProvider jwtTokenProvider;

    @Value("${test.mode:false}")
    private boolean testMode;

    public AuthService(UserRepository userRepository,
                      PasswordEncoder passwordEncoder,
                      JwtTokenProvider jwtTokenProvider) {
        this.userRepository = userRepository;
        this.passwordEncoder = passwordEncoder;
        this.jwtTokenProvider = jwtTokenProvider;
    }

    /**
     * Login
     */
    public Result<String> login(String username, String password) {
        // Test mode: always succeed with mock data
        if (testMode) {
            User mockUser = new User();
            mockUser.setUsername(username);
            mockUser.setPassword(passwordEncoder.encode(password));
            mockUser.setStatus("正常");
            String token = jwtTokenProvider.generateToken(username);
            return Result.success("登录成功 (测试模式)", token);
        }

        // Find user by username
        User user = userRepository.selectByUsername(username);
        if (user == null) {
            return Result.error("用户名或密码错误");
        }

        // Check user status
        if (!"正常".equals(user.getStatus())) {
            return Result.error("账号已停用，请联系管理员");
        }

        // Verify password
        if (!passwordEncoder.matches(password, user.getPassword())) {
            return Result.error("用户名或密码错误");
        }

        // Generate JWT token
        String token = jwtTokenProvider.generateToken(username);

        return Result.success("登录成功", token);
    }

    /**
     * Register
     */
    public Result<Void> register(String username, String password, String phone, String realName) {
        // Test mode: always succeed with mock data
        if (testMode) {
            return Result.success("注册成功 (测试模式)");
        }

        // Check if username already exists
        User existingUser = userRepository.selectByUsername(username);
        if (existingUser != null) {
            return Result.error("用户名已存在");
        }

        // Create new user
        User user = new User();
        user.setUsername(username);
        user.setPassword(passwordEncoder.encode(password));
        user.setPhone(phone);
        user.setRealName(realName);
        user.setStatus("正常");

        // Insert user
        int result = userRepository.insert(user);
        if (result > 0) {
            return Result.success("注册成功");
        } else {
            return Result.error("注册失败");
        }
    }

    /**
     * Load user by username (for Spring Security)
     */
    @Override
    public UserDetails loadUserByUsername(String username) throws UsernameNotFoundException {
        User user = userRepository.selectByUsername(username);
        if (user == null) {
            throw new UsernameNotFoundException("User not found: " + username);
        }

        return org.springframework.security.core.userdetails.User.builder()
                .username(user.getUsername())
                .password(user.getPassword())
                .roles("USER")
                .build();
    }
}
