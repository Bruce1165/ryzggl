package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.TrainUnit;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;
import org.apache.ibatis.annotations.Update;

import java.util.List;

/**
 * TrainUnit Repository
 * 培训单位数据访问层
 */
public interface TrainUnitRepository extends BaseMapper<TrainUnit> {

    /**
     * Get train unit by ID
     */
    @Select("SELECT * FROM TrainUnit WHERE UnitNo = #{unitNo}")
    TrainUnit getById(@Param("unitNo") String unitNo);

    /**
     * Get all train units
     */
    @Select("SELECT * FROM TrainUnit ORDER BY UnitNo")
    List<TrainUnit> getAll();

    /**
     * Get active train units
     */
    @Select("SELECT * FROM TrainUnit WHERE UseStatus = 1 ORDER BY UnitNo")
    List<TrainUnit> getActive();

    /**
     * Search train units by keyword
     */
    @Select("SELECT * FROM TrainUnit WHERE (TrainUnitName LIKE CONCAT('%', #{keyword}, '%') OR UnitCode LIKE CONCAT('%', #{keyword}, '%')) ORDER BY UnitNo")
    List<TrainUnit> search(@Param("keyword") String keyword);

    /**
     * Create train unit
     */
    @Insert("INSERT INTO TrainUnit(UnitNo,TrainUnitName,UnitCode,PostSet,UseStatus) VALUES (#{unitNo}, #{trainUnitName}, #{unitCode}, #{postSet}, #{useStatus})")
    int insertTrainUnit(TrainUnit trainUnit);

    /**
     * Update train unit
     */
    @Update("UPDATE TrainUnit SET TrainUnitName = #{trainUnitName}, UnitCode = #{unitCode}, PostSet = #{postSet}, UseStatus = #{useStatus} WHERE UnitNo = #{unitNo}")
    int updateTrainUnit(@Param("unitNo") String unitNo, @Param("trainUnitName") String trainUnitName, @Param("unitCode") String unitCode,
                      @Param("postSet") String postSet, @Param("useStatus") Integer useStatus);

    /**
     * Delete train unit
     */
    @Delete("DELETE FROM TrainUnit WHERE UnitNo = #{unitNo}")
    int deleteTrainUnit(@Param("unitNo") String unitNo);
}
