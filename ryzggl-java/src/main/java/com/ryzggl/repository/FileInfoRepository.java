package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.ryzggl.entity.FileInfo;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

import java.util.List;

/**
 * FileInfo Repository
 */
@Mapper
public interface FileInfoRepository extends BaseMapper<FileInfo> {

    /**
     * Query file list with pagination
     */
    @Select("<script>" +
            "SELECT * FROM FileInfo WHERE DELETED = 0" +
            "<if test='fileName != null and fileName != \"\"'>" +
            " AND FILENAME LIKE CONCAT('%', #{fileName}, '%')" +
            "</if>" +
            "<if test='fileType != null and fileType != \"\"'>" +
            " AND FILETYPE = #{fileType}" +
            "</if>" +
            " ORDER BY CREATE_TIME DESC" +
            "</script>")
    IPage<FileInfo> selectFilePage(Page<FileInfo> page,
                                  @Param("fileName") String fileName,
                                  @Param("fileType") String fileType);

    /**
     * Query files by business type and ID
     */
    @Select("SELECT * FROM FileInfo WHERE BUSINESSTYPE = #{businessType} AND BUSINESSID = #{businessId} AND DELETED = 0")
    List<FileInfo> selectByBusiness(@Param("businessType") String businessType,
                                   @Param("businessId") Long businessId);
}
