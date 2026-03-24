package com.ryzggl.service;

import com.baomidou.mybatisplus.extension.service.IService;
import com.ryzggl.entity.TrainUnit;
import com.ryzggl.repository.TrainUnitRepository;
import com.ryzggl.common.Result;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;

/**
 * TrainUnit Service
 * 培训单位业务逻辑层
 */
@Service
public class TrainUnitService implements IService<TrainUnit> {

    @Autowired
    private TrainUnitRepository trainUnitRepository;

    /**
     * Get train unit by ID
     */
    public TrainUnit getById(String unitNo) {
        return trainUnitRepository.getById(unitNo);
    }

    /**
     * Get all train units
     */
    public List<TrainUnit> getAll() {
        return trainUnitRepository.getAll();
    }

    /**
     * Get active train units
     */
    public List<TrainUnit> getActive() {
        return trainUnitRepository.getActive();
    }

    /**
     * Search train units
     */
    public List<TrainUnit> search(String keyword) {
        if (keyword == null || keyword.trim().isEmpty()) {
            return trainUnitRepository.selectList(null);
        }
        return trainUnitRepository.search(keyword);
    }

    /**
     * Create train unit
     */
    @Transactional
    public Result<TrainUnit> createTrainUnit(TrainUnit trainUnit) {
        // Validate required fields
        if (trainUnit.getTrainUnitName() == null || trainUnit.getTrainUnitName().trim().isEmpty()) {
            return Result.error("培训单位名称不能为空");
        }

        // Set default values
        if (trainUnit.getUseStatus() == null) {
            trainUnit.setUseStatus(1);
        }

        trainUnitRepository.insertTrainUnit(trainUnit);
        return Result.success(trainUnit);
    }

    /**
     * Update train unit
     */
    @Transactional
    public Result<TrainUnit> updateTrainUnit(TrainUnit trainUnit) {
        // Validate required fields
        if (trainUnit.getUnitNo() == null) {
            return Result.error("单位编号不能为空");
        }

        TrainUnit existing = getById(trainUnit.getUnitNo());
        if (existing == null) {
            return Result.error("培训单位不存在");
        }

        trainUnitRepository.updateTrainUnit(trainUnit);
        return Result.success(trainUnit);
    }

    /**
     * Update train unit status
     */
    @Transactional
    public Result<TrainUnit> updateTrainUnitStatus(String unitNo, Integer useStatus) {
        TrainUnit existing = getById(unitNo);
        if (existing == null) {
            return Result.error("培训单位不存在");
        }

        existing.setUseStatus(useStatus);
        trainUnitRepository.updateTrainUnit(existing);
        return Result.success(existing);
    }

    /**
     * Delete train unit
     */
    @Transactional
    public Result<Void> deleteTrainUnit(String unitNo) {
        TrainUnit existing = getById(unitNo);
        if (existing == null) {
            return Result.error("培训单位不存在");
        }

        trainUnitRepository.deleteTrainUnit(unitNo);
        return Result.success();
    }
}
