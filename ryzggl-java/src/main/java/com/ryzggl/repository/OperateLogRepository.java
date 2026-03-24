package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.OperateLog;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;
import org.apache.ibatis.annotations.Update;

import java.util.Date;
import java.util.List;

/**
 * OperateLog Repository
 * 操作日志数据访问层
 */
public interface OperateLogRepository extends BaseMapper<OperateLog> {

    /**
     * Get log by ID
     */
    @Select("SELECT * FROM OperateLog WHERE LogID = #{logId}")
    OperateLog getById(@Param("logId") Long logId);

    /**
     * Get logs by person ID
     */
    @Select("SELECT * FROM OperateLog WHERE PersonID = #{personId} ORDER BY LogTime DESC")
    List<OperateLog> getByPersonId(@Param("personId") String personId);

    /**
     * Get logs by operation name
     */
    @Select("SELECT * FROM OperateLog WHERE OperateName = #{operateName} ORDER BY LogTime DESC")
    List<OperateLog> getByOperateName(@Param("operateName") String operateName);

    /**
     * Get logs by date range
     */
    @Select("SELECT * FROM OperateLog WHERE LogTime BETWEEN #{beginDate} AND #{endDate} ORDER BY LogTime DESC")
    List<OperateLog> getByDateRange(@Param("beginDate") Date beginDate, @Param("endDate") Date endDate);

    /**
     * Get operation statistics by name
     */
    @Select("SELECT OperateName, COUNT(*) as StatisticsResult FROM OperateLog WHERE to_char(LOGTIME,'yyyy-mm-dd') between  '{0}' and  '{0}' GROUP BY OperateName")
    List<OperateLog> getStatistics();

    /**
     * Search logs by keyword
     */
    @Select("SELECT * FROM OperateLog WHERE (PersonName LIKE CONCAT('%', #{keyword}, '%') OR OperateName LIKE CONCAT('%', #{keyword}, '%') OR LogDetail LIKE CONCAT('%', #{keyword}, '%')) ORDER BY LogTime DESC")
    List<OperateLog> search(@Param("keyword") String keyword);

    /**
     * Create log
     */
    @Insert("INSERT INTO OperateLog(LogTime, PersonName, PersonID, OperateName, LogDetail) VALUES (#{logTime}, #{personName}, #{personId}, #{operateName}, #{logDetail})")
    int insertLog(OperateLog operateLog);

    /**
     * Update log
     */
    @Update("UPDATE OperateLog SET LogTime = #{logTime}, PersonName = #{personName}, PersonID = #{personId}, OperateName = #{operateName}, LogDetail = #{logDetail} WHERE LogID = #{logId}")
    int updateLog(OperateLog operateLog);

    /**
     * Delete log
     */
    @Delete("DELETE FROM OperateLog WHERE LogID = #{logId}")
    int deleteLog(@Param("logId") Long logId);
}
