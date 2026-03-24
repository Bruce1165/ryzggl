package com.ryzggl.service;

import com.baomidou.mybatisplus.extension.service.IService;
import com.ryzggl.entity.Dictionary;
import com.ryzggl.repository.DictionaryRepository;
import com.ryzggl.common.Result;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;

/**
 * Dictionary Service
 * 数据字典业务逻辑层
 */
@Service
public class DictionaryService implements IService<Dictionary> {

    @Autowired
    private DictionaryRepository dictionaryRepository;

    /**
     * Get dictionary by ID
     */
    public Dictionary getById(String dicId) {
        return dictionaryRepository.getById(dicId);
    }

    /**
     * Get all dictionaries
     */
    public List<Dictionary> getAll() {
        return dictionaryRepository.getAll();
    }

    /**
     * Get dictionaries by type ID
     */
    public List<Dictionary> getByTypeId(Integer typeId) {
        return dictionaryRepository.getByTypeId(typeId);
    }

    /**
     * Get dictionary name by type ID and order ID
     */
    public String getDicNameByTypeIdAndOrderId(Integer typeId, Integer orderId) {
        return dictionaryRepository.getDicNameByTypeIdAndOrderId(typeId, orderId);
    }

    /**
     * Search dictionaries by type and name
     */
    public List<Dictionary> searchByName(Integer typeId, String dicName) {
        if (typeId == null && (dicName == null || dicName.trim().isEmpty())) {
            return getAll();
        }
        return dictionaryRepository.searchByName(typeId, dicName);
    }

    /**
     * Search dictionaries globally
     */
    public List<Dictionary> search(String keyword) {
        if (keyword == null || keyword.trim().isEmpty()) {
            return getAll();
        }
        return dictionaryRepository.search(keyword);
    }

    /**
     * Get dropdown data for a type
     */
    public List<Dictionary> getDropdownData(Integer typeId) {
        return dictionaryRepository.getDropdownData(typeId);
    }

    /**
     * Create dictionary
     */
    @Transactional
    public Result<Dictionary> createDictionary(Dictionary dictionary) {
        // Validate required fields
        if (dictionary.getTypeId() == null) {
            return Result.error("TypeID不能为空");
        }
        if (dictionary.getDicName() == null || dictionary.getDicName().trim().isEmpty()) {
            return Result.error("字典名称不能为空");
        }

        // Set default values
        if (dictionary.getOrderId() == null) {
            dictionary.setOrderId(0);
        }

        dictionaryRepository.insert(dictionary);
        return Result.success(dictionary);
    }

    /**
     * Update dictionary
     */
    @Transactional
    public Result<Dictionary> updateDictionary(Dictionary dictionary) {
        // Validate required fields
        if (dictionary.getDicId() == null || dictionary.getDicId().trim().isEmpty()) {
            return Result.error("字典ID不能为空");
        }

        // Check if exists
        Dictionary existing = getById(dictionary.getDicId());
        if (existing == null) {
            return Result.error("字典不存在");
        }

        dictionaryRepository.updateById(dictionary);
        return Result.success(dictionary);
    }

    /**
     * Delete dictionary
     */
    @Transactional
    public Result<Void> deleteDictionary(String dicId) {
        // Check if exists
        Dictionary existing = getById(dicId);
        if (existing == null) {
            return Result.error("字典不存在");
        }

        dictionaryRepository.deleteById(dicId);
        return Result.success();
    }
}
