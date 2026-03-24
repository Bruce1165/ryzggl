package com.ryzggl.service;

import com.baomidou.mybatisplus.extension.service.IService;
import com.ryzggl.entity.OperateLog;
import com.ryzggl.repository.OperateLogRepository;
import com.ryzggl.common.Result;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;

/**
 * OperateLog Service
 * 操作日志业务逻辑层
 */
@Service
public class OperateLogService implements IService<OperateLog> {

    @Autowired
    private OperateLogRepository operateLogRepository;

    /**
     * Get log by ID
     */
    public OperateLog getById(Long logId) {
        return operateLogRepository.getById(logId);
    }

    /**
     * Get logs by person ID
     */
    public List<OperateLog> getByPersonId(String personId) {
        return operateLogRepository.getByPersonId(personId);
    }

    /**
     * Get logs by operation name
     */
    public List<OperateLog> getByOperateName(String operateName) {
        return operateLogRepository.getByOperateName(operateName);
    }

    /**
     * Get logs by date range
     */
    public List<OperateLog> getByDateRange(Date beginDate, Date endDate) {
        return operateLogRepository.getByDateRange(beginDate, endDate);
    }

    /**
     * Get operation statistics
     */
    public List<OperateLog> getStatistics() {
        return operateLogRepository.getStatistics();
    }

    /**
     * Search logs
     */
    public List<OperateLog> search(String keyword) {
        if (keyword == null || keyword.trim().isEmpty()) {
            return operateLogRepository.selectList(null);
        }
        return operateLogRepository.search(keyword);
    }

    /**
     * Create log
     */
    @Transactional
    public Result<OperateLog> createLog(OperateLog operateLog) {
        // Validate required fields
        if (operateLog.getPersonName() == null || operateLog.getPersonName().trim().isEmpty()) {
            return Result.error("操作人不能为空");
        }

        if (operateLog.getOperateName() == null || operateLog.getOperateName().trim().isEmpty()) {
            return Result.error("操作名称不能为空");
        }

        // Set default values
        operateLog.setLogTime(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new Date()));

        operateLogRepository.insertLog(operateLog);
        return Result.success(operateLog);
    }

    /**
     * Delete log
     */
    @Transactional
    public Result<Void> deleteLog(Long logId) {
        OperateLog existing = getById(logId);
        if (existing == null) {
            return Result.error("日志记录不存在");
        }

        operateLogRepository.deleteLog(logId);
        return Result.success();
    }
}
