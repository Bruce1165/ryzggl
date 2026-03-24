package com.ryzggl.service;

import com.baomidou.mybatisplus.extension.service.IService;
import com.ryzggl.entity.PostInfo;
import com.ryzggl.repository.PostInfoRepository;
import com.ryzggl.common.Result;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.List;

/**
 * PostInfo Service
 * 岗位信息业务逻辑层
 */
@Service
public class PostInfoService implements IService<PostInfo> {

    @Autowired
    private PostInfoRepository postInfoRepository;

    /**
     * Get post by ID
     */
    public PostInfo getById(Integer postId) {
        return postInfoRepository.getById(postId);
    }

    /**
     * Get posts by type
     */
    public List<PostInfo> getByPostType(String postType) {
        return postInfoRepository.getByPostType(postType);
    }

    /**
     * Get child posts by parent ID
     */
    public List<PostInfo> getByUpPostId(Integer upPostId) {
        return postInfoRepository.getByUpPostId(upPostId);
    }

    /**
     * Get distinct skill levels (post type 3)
     */
    public List<PostInfo> getDistinctSkillLevels() {
        return postInfoRepository.getDistinctSkillLevels();
    }

    /**
     * Search posts
     */
    public List<PostInfo> search(String keyword) {
        if (keyword == null || keyword.trim().isEmpty()) {
            return postInfoRepository.selectList(null);
        }
        return postInfoRepository.search(keyword);
    }

    /**
     * Create post
     */
    @Transactional
    public Result<PostInfo> createPost(PostInfo postInfo) {
        // Validate required fields
        if (postInfo.getPostName() == null || postInfo.getPostName().trim().isEmpty()) {
            return Result.error("岗位名称不能为空");
        }

        // Check for duplicate name under same parent
        int count = postInfoRepository.countByNameUnderParent(
            postInfo.getUpPostId(),
            postInfo.getPostName(),
            null
        );
        if (count > 0) {
            return Result.error("同一上级下已存在同名岗位");
        }

        // Set default values
        if (postInfo.getPostType() == null) {
            postInfo.setPostType("1"); // Default to major/professional
        }
        if (postInfo.getCurrentNumber() == null) {
            postInfo.setCurrentNumber(0L);
        }
        if (postInfo.getCodeYear() == null) {
            postInfo.setCodeYear(Calendar.getInstance().get(Calendar.YEAR));
        }

        postInfoRepository.insert(postInfo);
        return Result.success(postInfo);
    }

    /**
     * Update post
     */
    @Transactional
    public Result<PostInfo> updatePost(PostInfo postInfo) {
        // Validate required fields
        if (postInfo.getPostId() == null) {
            return Result.error("岗位ID不能为空");
        }

        // Check if exists
        PostInfo existing = getById(postInfo.getPostId());
        if (existing == null) {
            return Result.error("岗位不存在");
        }

        // Check for duplicate name under same parent (excluding self)
        int count = postInfoRepository.countByNameUnderParent(
            postInfo.getUpPostId(),
            postInfo.getPostName(),
            postInfo.getPostId()
        );
        if (count > 0) {
            return Result.error("同一上级下已存在同名岗位");
        }

        postInfoRepository.updateById(postInfo);
        return Result.success(postInfo);
    }

    /**
     * Delete post
     */
    @Transactional
    public Result<Void> deletePost(Integer postId) {
        // Check if exists
        PostInfo existing = getById(postId);
        if (existing == null) {
            return Result.error("岗位不存在");
        }

        // Check for child posts
        List<PostInfo> children = getByUpPostId(postId);
        if (!children.isEmpty()) {
            return Result.error("该岗位下存在子岗位，无法删除");
        }

        postInfoRepository.deleteById(postId);
        return Result.success();
    }

    /**
     * Get next certificate number (without updating current number)
     *
     * @param postId Post ID
     * @return Next certificate number
     */
    public String getNextCertificateNo(Integer postId) {
        PostInfo postInfo = getById(postId);
        if (postInfo == null) {
            throw new RuntimeException("岗位不存在");
        }

        return generateCertificateNumber(postInfo, false);
    }

    /**
     * Get and update next certificate number
     *
     * @param postId Post ID
     * @return Next certificate number
     */
    @Transactional
    public String getAndUpdateNextCertificateNo(Integer postId) {
        PostInfo postInfo = getById(postId);
        if (postInfo == null) {
            throw new RuntimeException("岗位不存在");
        }

        String certNo = generateCertificateNumber(postInfo, true);

        // Update the current number and code year
        if (postInfo.getUpPostId() != null && certNo.contains("YPN")) {
            // Update parent-level current number
            postInfoRepository.updateCurrentNumberByUpPostId(
                postInfo.getUpPostId(),
                postInfo.getCurrentNumber(),
                postInfo.getCodeYear()
            );
        } else {
            // Update post-level current number
            postInfoRepository.updateCurrentNumber(
                postId,
                postInfo.getCurrentNumber(),
                postInfo.getCodeYear()
            );
        }

        return certNo;
    }

    /**
     * Generate certificate number based on format rules
     *
     * @param postInfo Post info
     * @param updateCurrent Whether to update current number
     * @return Generated certificate number
     */
    private String generateCertificateNumber(PostInfo postInfo, boolean updateCurrent) {
        String codeFormat = postInfo.getCodeFormat();

        // Handle special format references
        if ("Parent".equals(codeFormat)) {
            postInfo = getById(postInfo.getUpPostId());
            codeFormat = postInfo.getCodeFormat();
        } else if (codeFormat != null && codeFormat.startsWith("Brather")) {
            String brotherId = codeFormat.replace("Brather", "");
            postInfo = getById(Integer.parseInt(brotherId));
            codeFormat = postInfo.getCodeFormat();
        }

        Long currentNumber = postInfo.getCurrentNumber() != null ? postInfo.getCurrentNumber() : 0L;
        int codeYear = postInfo.getCodeYear() != null ? postInfo.getCodeYear() : Calendar.getInstance().get(Calendar.YEAR);
        int currentYear = Calendar.getInstance().get(Calendar.YEAR);

        String[] formatParts = codeFormat.split("\\|");
        StringBuilder result = new StringBuilder();

        for (int i = 0; i < formatParts.length; i++) {
            String part = formatParts[i];
            String[] tokens = part.split(",");

            if (tokens.length == 1) {
                // Constant value
                result.append(tokens[0]);
            } else {
                String type = tokens[0];
                int length = Integer.parseInt(tokens[1]);

                switch (type) {
                    case "Y": // Year
                        result.append(String.format("%0" + length + "d", currentYear % (int) Math.pow(10, length)));
                        break;

                    case "N": // Incremental number
                        currentNumber++;
                        result.append(String.format("%0" + length + "d", currentNumber));
                        if (updateCurrent) {
                            postInfo.setCurrentNumber(currentNumber);
                            postInfo.setCodeYear(currentYear);
                        }
                        break;

                    case "YN": // Year-based incremental
                        if (codeYear == currentYear) {
                            currentNumber++;
                        } else {
                            currentNumber = 1L;
                        }
                        result.append(String.format("%0" + length + "d", currentNumber));
                        if (updateCurrent) {
                            postInfo.setCurrentNumber(currentNumber);
                            postInfo.setCodeYear(currentYear);
                        }
                        break;

                    case "YPN": // Year-based incremental by parent
                        if (codeYear == currentYear) {
                            currentNumber++;
                        } else {
                            currentNumber = 1L;
                        }
                        result.append(String.format("%0" + length + "d", currentNumber));
                        if (updateCurrent) {
                            postInfo.setCurrentNumber(currentNumber);
                            postInfo.setCodeYear(currentYear);
                        }
                        break;

                    default:
                        result.append(part);
                        break;
                }
            }
        }

        return result.toString();
    }

    /**
     * Get skill level code
     *
     * @param skillLevelName Skill level name
     * @return Skill level code (0=普工, 5=初级, 4=中级, 3=高级, 2=技师, 1=高级技师)
     */
    public String getSkillLevelCode(String skillLevelName) {
        if (skillLevelName == null) {
            throw new RuntimeException("技能等级名称不能为空");
        }

        switch (skillLevelName) {
            case "普工":
                return "0";
            case "初级工":
                return "5";
            case "中级工":
                return "4";
            case "高级工":
                return "3";
            case "技师":
                return "2";
            case "高级技师":
                return "1";
            default:
                throw new RuntimeException("技能等级不在编码列表中，无法编号: " + skillLevelName);
        }
    }
}
