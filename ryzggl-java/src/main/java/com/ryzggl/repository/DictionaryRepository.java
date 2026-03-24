package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.Dictionary;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

import java.util.List;

/**
 * Dictionary Repository
 * 数据字典数据访问层
 */
public interface DictionaryRepository extends BaseMapper<Dictionary> {

    /**
     * Get dictionary by ID
     */
    @Select("SELECT * FROM Dictionary WHERE DicID = #{dicId}")
    Dictionary getById(String dicId);

    /**
     * Get all dictionaries
     */
    @Select("SELECT * FROM Dictionary ORDER BY TypeID, OrderID")
    List<Dictionary> getAll();

    /**
     * Get dictionaries by type ID
     */
    @Select("SELECT * FROM Dictionary WHERE TypeID = #{typeId} ORDER BY OrderID")
    List<Dictionary> getByTypeId(@Param("typeId") Integer typeId);

    /**
     * Get dictionary name by type ID and order ID
     */
    @Select("SELECT DicName FROM Dictionary WHERE TypeID = #{typeId} AND OrderID = #{orderId}")
    String getDicNameByTypeIdAndOrderId(@Param("typeId") Integer typeId, @Param("orderId") Integer orderId);

    /**
     * Get dictionaries by type ID and name pattern
     */
    @Select("SELECT * FROM Dictionary WHERE TypeID = #{typeId} AND DicName LIKE CONCAT('%', #{dicName}, '%') ORDER BY OrderID")
    List<Dictionary> searchByName(@Param("typeId") Integer typeId, @Param("dicName") String dicName);

    /**
     * Get dictionary name by type ID and name
     */
    @Select("SELECT DicName FROM Dictionary WHERE TypeID = #{typeId} AND DicName IN #{dicNames} ORDER BY OrderID")
    List<String> getDicNamesByTypeIdAndNames(@Param("typeId") Integer typeId, @Param("dicNames") List<String> dicNames);

    /**
     * Search dictionaries by keyword
     */
    @Select("SELECT * FROM Dictionary WHERE DicName LIKE CONCAT('%', #{keyword}, '%') OR DicDesc LIKE CONCAT('%', #{keyword}, '%') ORDER BY TypeID, OrderID")
    List<Dictionary> search(@Param("keyword") String keyword);

    /**
     * Get dropdown data for a type
     */
    @Select("SELECT DicName AS TypeName, OrderID AS TypeValue FROM Dictionary WHERE TypeID = #{typeId} ORDER BY OrderID")
    List<Dictionary> getDropdownData(@Param("typeId") Integer typeId);
}
