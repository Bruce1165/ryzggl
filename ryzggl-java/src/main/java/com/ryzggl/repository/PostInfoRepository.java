package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.PostInfo;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;
import org.apache.ibatis.annotations.Update;

import java.util.List;

/**
 * PostInfo Repository
 * 岗位信息数据访问层
 */
public interface PostInfoRepository extends BaseMapper<PostInfo> {

    /**
     * Get post by ID
     */
    @Select("SELECT * FROM PostInfo WHERE PostID = #{postId}")
    PostInfo getById(@Param("postId") Integer postId);

    /**
     * Get post by parent ID and name
     */
    @Select("SELECT * FROM PostInfo WHERE UpPostID = #{upPostId} AND PostName = #{postName}")
    PostInfo getByUpPostIdAndName(@Param("upPostId") Integer upPostId, @Param("postName") String postName);

    /**
     * Get first post by parent post ID
     */
    @Select("SELECT TOP 1 * FROM PostInfo WHERE UpPostID = #{upPostId}")
    PostInfo getFirstByUpPostId(@Param("upPostId") Integer upPostId);

    /**
     * Get all posts by parent post ID
     */
    @Select("SELECT * FROM PostInfo WHERE UpPostID = #{upPostId} ORDER BY PostName")
    List<PostInfo> getByUpPostId(@Param("upPostId") Integer upPostId);

    /**
     * Get posts by type
     */
    @Select("SELECT * FROM PostInfo WHERE PostType = #{postType} ORDER BY PostName")
    List<PostInfo> getByPostType(@Param("postType") String postType);

    /**
     * Get all distinct post names for post type 3 (skill levels)
     */
    @Select("SELECT DISTINCT PostName FROM PostInfo WHERE PostType = 3 ORDER BY PostName")
    List<PostInfo> getDistinctSkillLevels();

    /**
     * Search posts by keyword
     */
    @Select("SELECT * FROM PostInfo WHERE PostName LIKE CONCAT('%', #{keyword}, '%') ORDER BY PostName")
    List<PostInfo> search(@Param("keyword") String keyword);

    /**
     * Update current number and code year
     */
    @Update("UPDATE PostInfo SET CurrentNumber = #{currentNumber}, CodeYear = #{codeYear} WHERE PostID = #{postId}")
    int updateCurrentNumber(@Param("postId") Integer postId, @Param("currentNumber") Long currentNumber, @Param("codeYear") Integer codeYear);

    /**
     * Update current number by parent post ID
     */
    @Update("UPDATE PostInfo SET CurrentNumber = #{currentNumber}, CodeYear = #{codeYear} WHERE UpPostID = #{upPostId}")
    int updateCurrentNumberByUpPostId(@Param("upPostId") Integer upPostId, @Param("currentNumber") Long currentNumber, @Param("codeYear") Integer codeYear);

    /**
     * Check if post name exists under parent
     */
    @Select("SELECT COUNT(*) FROM PostInfo WHERE UpPostID = #{upPostId} AND PostName = #{postName} AND PostID != #{excludePostId}")
    int countByNameUnderParent(@Param("upPostId") Integer upPostId, @Param("postName") String postName, @Param("excludePostId") Integer excludePostId);
}
