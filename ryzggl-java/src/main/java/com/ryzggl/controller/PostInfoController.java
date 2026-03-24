package com.ryzggl.controller;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.common.Result;
import com.ryzggl.entity.PostInfo;
import com.ryzggl.service.PostInfoService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * PostInfo Controller
 * 岗位信息REST API
 *
 * Maps to legacy PostInfoDAL.cs
 * Base path: /api/v1/posts
 */
@Tag(name = "PostInfo Management", description = "岗位信息管理API")
@RestController
@RequestMapping("/api/v1/posts")
public class PostInfoController {

    @Autowired
    private PostInfoService postInfoService;

    /**
     * Get post by ID
     */
    @Operation(summary = "Get post by ID")
    @GetMapping("/{postId}")
    public Result<PostInfo> getById(@PathVariable Integer postId) {
        PostInfo postInfo = postInfoService.getById(postId);
        if (postInfo == null) {
            return Result.error("岗位不存在");
        }
        return Result.success(postInfo);
    }

    /**
     * Get posts by type
     */
    @Operation(summary = "Get posts by type")
    @GetMapping("/type/{postType}")
    public Result<List<PostInfo>> getByPostType(@PathVariable String postType) {
        List<PostInfo> posts = postInfoService.getByPostType(postType);
        return Result.success(posts);
    }

    /**
     * Get child posts by parent ID
     */
    @Operation(summary = "Get child posts by parent ID")
    @GetMapping("/parent/{upPostId}")
    public Result<List<PostInfo>> getByUpPostId(@PathVariable Integer upPostId) {
        List<PostInfo> posts = postInfoService.getByUpPostId(upPostId);
        return Result.success(posts);
    }

    /**
     * Get distinct skill levels
     */
    @Operation(summary = "Get distinct skill levels")
    @GetMapping("/skill-levels")
    public Result<List<PostInfo>> getDistinctSkillLevels() {
        List<PostInfo> skillLevels = postInfoService.getDistinctSkillLevels();
        return Result.success(skillLevels);
    }

    /**
     * Get all posts
     */
    @Operation(summary = "Get all posts with pagination")
    @GetMapping
    public Result<Page<PostInfo>> list(
            @RequestParam(defaultValue = "1") int current,
            @RequestParam(defaultValue = "10") int size,
            @RequestParam(required = false) String postType,
            @RequestParam(required = false) String keyword
    ) {
        Page<PostInfo> page = new Page<>(current, size);
        QueryWrapper<PostInfo> queryWrapper = new QueryWrapper<>();

        if (postType != null && !postType.isEmpty()) {
            queryWrapper.eq("PostType", postType);
        }
        if (keyword != null && !keyword.isEmpty()) {
            queryWrapper.like("PostName", keyword);
        }
        queryWrapper.orderByAsc("PostName");

        Page<PostInfo> result = postInfoService.page(page, queryWrapper);
        return Result.success(result);
    }

    /**
     * Search posts
     */
    @Operation(summary = "Search posts")
    @GetMapping("/search")
    public Result<List<PostInfo>> search(@RequestParam String keyword) {
        List<PostInfo> posts = postInfoService.search(keyword);
        return Result.success(posts);
    }

    /**
     * Create post
     */
    @Operation(summary = "Create post")
    @PostMapping("/create")
    public Result<PostInfo> create(@RequestBody PostInfo postInfo) {
        return postInfoService.createPost(postInfo);
    }

    /**
     * Update post
     */
    @Operation(summary = "Update post")
    @PutMapping("/{postId}")
    public Result<PostInfo> update(@PathVariable Integer postId, @RequestBody PostInfo postInfo) {
        postInfo.setPostId(postId);
        return postInfoService.updatePost(postInfo);
    }

    /**
     * Delete post
     */
    @Operation(summary = "Delete post")
    @DeleteMapping("/{postId}")
    public Result<Void> delete(@PathVariable Integer postId) {
        return postInfoService.deletePost(postId);
    }

    /**
     * Get next certificate number (without updating)
     */
    @Operation(summary = "Get next certificate number")
    @GetMapping("/{postId}/next-cert-no")
    public Result<Map<String, String>> getNextCertificateNo(@PathVariable Integer postId) {
        try {
            String certNo = postInfoService.getNextCertificateNo(postId);
            Map<String, String> result = new HashMap<>();
            result.put("certNo", certNo);
            result.put("updated", "false");
            return Result.success(result);
        } catch (Exception e) {
            return Result.error(e.getMessage());
        }
    }

    /**
     * Get and update next certificate number
     */
    @Operation(summary = "Get and update next certificate number")
    @PostMapping("/{postId}/next-cert-no")
    public Result<Map<String, String>> getAndUpdateNextCertificateNo(@PathVariable Integer postId) {
        try {
            String certNo = postInfoService.getAndUpdateNextCertificateNo(postId);
            Map<String, String> result = new HashMap<>();
            result.put("certNo", certNo);
            result.put("updated", "true");
            return Result.success(result);
        } catch (Exception e) {
            return Result.error(e.getMessage());
        }
    }

    /**
     * Get skill level code
     */
    @Operation(summary = "Get skill level code")
    @GetMapping("/skill-level-code")
    public Result<Map<String, String>> getSkillLevelCode(@RequestParam String skillLevelName) {
        try {
            String code = postInfoService.getSkillLevelCode(skillLevelName);
            Map<String, String> result = new HashMap<>();
            result.put("skillLevelName", skillLevelName);
            result.put("code", code);
            return Result.success(result);
        } catch (Exception e) {
            return Result.error(e.getMessage());
        }
    }
}
