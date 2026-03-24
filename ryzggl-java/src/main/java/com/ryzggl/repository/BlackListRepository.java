package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.BlackList;
import org.apache.ibatis.annotations.*;

import java.util.List;

/**
 * BlackList Repository
 * 黑名单数据访问层
 */
public interface BlackListRepository extends BaseMapper<BlackList> {

    /**
     * Get by ID
     */
    @Select("SELECT * FROM BlackList WHERE BlackListID = #{blackListId}")
    BlackList getById(@Param("blackListId") Long blackListId);

    /**
     * Get all blacklist entries
     */
    @Select("SELECT * FROM BlackList ORDER BY BlackListID DESC")
    List<BlackList> getAll();

    /**
     * Get by worker name
     */
    @Select("SELECT * FROM BlackList WHERE WorkerName LIKE CONCAT('%', #{workerName}, '%') ORDER BY BlackListID DESC")
    List<BlackList> getByWorkerName(@Param("workerName") String workerName);

    /**
     * Get by certificate code
     */
    @Select("SELECT * FROM BlackList WHERE CertificateCode = #{certificateCode} ORDER BY BlackListID DESC")
    List<BlackList> getByCertificateCode(@Param("certificateCode") String certificateCode);

    /**
     * Get by unit code
     */
    @Select("SELECT * FROM BlackList WHERE UnitCode = #{unitCode} ORDER BY BlackListID DESC")
    List<BlackList> getByUnitCode(@Param("unitCode") String unitCode);

    /**
     * Get by black type
     */
    @Select("SELECT * FROM BlackList WHERE BlackType = #{blackType} ORDER BY BlackListID DESC")
    List<BlackList> getByBlackType(@Param("blackType") String blackType);

    /**
     * Get by black status
     */
    @Select("SELECT * FROM BlackList WHERE BlackStatus = #{blackStatus} ORDER BY BlackListID DESC")
    List<BlackList> getByBlackStatus(@Param("blackStatus") String blackStatus);

    /**
     * Search by keyword
     */
    @Select("SELECT * FROM BlackList WHERE WorkerName LIKE CONCAT('%', #{keyword}, '%') OR CertificateCode LIKE CONCAT('%', #{keyword}, '%') OR UnitName LIKE CONCAT('%', #{keyword}, '%') ORDER BY BlackListID DESC")
    List<BlackList> search(@Param("keyword") String keyword);

    /**
     * Check if certificate is blacklisted
     */
    @Select("SELECT COUNT(*) FROM BlackList WHERE CertificateCode = #{certificateCode} AND BlackStatus = '有效'")
    int isCertificateBlacklisted(@Param("certificateCode") String certificateCode);

    /**
     * Insert blacklist
     */
    @Insert("INSERT INTO BlackList(PostTypeID, PostID, WorkerName, CertificateCode, UnitName, UnitCode, TrainUnitName, BlackType, StartTime, BlackStatus, Remark, CreatePerson, CreateTime, ModifyPerson, ModifyTime) VALUES (#{postTypeId}, #{postId}, #{workerName}, #{certificateCode}, #{unitName}, #{unitCode}, #{trainUnitName}, #{blackType}, #{startTime}, #{blackStatus}, #{remark}, #{createPerson}, #{createTime}, #{modifyPerson}, #{modifyTime})")
    @Options(useGeneratedKeys = true, keyProperty = "blackListId", keyColumn = "BlackListID")
    int insertBlackList(BlackList blackList);

    /**
     * Update blacklist
     */
    @Update("UPDATE BlackList SET PostTypeID = #{postTypeId}, PostID = #{postId}, WorkerName = #{workerName}, CertificateCode = #{certificateCode}, UnitName = #{unitName}, UnitCode = #{unitCode}, TrainUnitName = #{trainUnitName}, BlackType = #{blackType}, StartTime = #{startTime}, BlackStatus = #{blackStatus}, Remark = #{remark}, ModifyPerson = #{modifyPerson}, ModifyTime = #{modifyTime} WHERE BlackListID = #{blackListId}")
    int updateBlackList(BlackList blackList);

    /**
     * Delete blacklist
     */
    @Delete("DELETE FROM BlackList WHERE BlackListID = #{blackListId}")
    int deleteBlackList(@Param("blackListId") Long blackListId);

    /**
     * Count blacklist entries
     */
    @Select("SELECT COUNT(*) FROM BlackList")
    int count();
}
