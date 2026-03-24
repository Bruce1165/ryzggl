package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.CheckFeedBack;
import org.apache.ibatis.annotations.*;

import java.util.List;

/**
 * CheckFeedBack Repository
 * 核查反馈数据访问层
 */
public interface CheckFeedBackRepository extends BaseMapper<CheckFeedBack> {

    /**
     * Get by ID
     */
    @Select("SELECT * FROM CheckFeedBack WHERE DataID = #{dataId}")
    CheckFeedBack getById(@Param("dataId") String dataId);

    /**
     * Get all feedback records
     */
    @Select("SELECT * FROM CheckFeedBack ORDER BY sn, PatchCode DESC")
    List<CheckFeedBack> getAll();

    /**
     * Get by patch code
     */
    @Select("SELECT * FROM CheckFeedBack WHERE PatchCode = #{patchCode} ORDER BY sn")
    List<CheckFeedBack> getByPatchCode(@Param("patchCode") Integer patchCode);

    /**
     * Get by check type
     */
    @Select("SELECT * FROM CheckFeedBack WHERE CheckType = #{checkType} ORDER BY PatchCode DESC, sn")
    List<CheckFeedBack> getByCheckType(@Param("checkType") String checkType);

    /**
     * Get by status code
     */
    @Select("SELECT * FROM CheckFeedBack WHERE DataStatusCode = #{dataStatusCode} ORDER BY PatchCode DESC, sn")
    List<CheckFeedBack> getByStatusCode(@Param("dataStatusCode") Integer dataStatusCode);

    /**
     * Get by worker name
     */
    @Select("SELECT * FROM CheckFeedBack WHERE WorkerName LIKE CONCAT('%', #{workerName}, '%') ORDER BY PatchCode DESC, sn")
    List<CheckFeedBack> getByWorkerName(@Param("workerName") String workerName);

    /**
     * Get by certificate code
     */
    @Select("SELECT * FROM CheckFeedBack WHERE CertificateCode = #{certificateCode} ORDER BY PatchCode DESC, sn")
    List<CheckFeedBack> getByCertificateCode(@Param("certificateCode") String certificateCode);

    /**
     * Get by unit code
     */
    @Select("SELECT * FROM CheckFeedBack WHERE UnitCode = #{unitCode} ORDER BY PatchCode DESC, sn")
    List<CheckFeedBack> getByUnitCode(@Param("unitCode") String unitCode);

    /**
     * Get by country
     */
    @Select("SELECT * FROM CheckFeedBack WHERE Country = #{country} ORDER BY PatchCode DESC, sn")
    List<CheckFeedBack> getByCountry(@Param("country") String country);

    /**
     * Search by keyword
     */
    @Select("SELECT * FROM CheckFeedBack WHERE WorkerName LIKE CONCAT('%', #{keyword}, '%') OR CertificateCode LIKE CONCAT('%', #{keyword}, '%') OR Unit LIKE CONCAT('%', #{keyword}, '%') ORDER BY PatchCode DESC, sn")
    List<CheckFeedBack> search(@Param("keyword") String keyword);

    /**
     * Get pending feedback records (status = 1)
     */
    @Select("SELECT * FROM CheckFeedBack WHERE DataStatusCode = 1 ORDER BY PatchCode DESC, sn")
    List<CheckFeedBack> getPendingFeedback();

    /**
     * Get feedback requiring initial review (status = 3)
     */
    @Select("SELECT * FROM CheckFeedBack WHERE DataStatusCode = 3 ORDER BY PatchCode DESC, sn")
    List<CheckFeedBack> getPendingReview();

    /**
     * Get feedback requiring city review (status = 4)
     */
    @Select("SELECT * FROM CheckFeedBack WHERE DataStatusCode = 4 ORDER BY PatchCode DESC, sn")
    List<CheckFeedBack> getPendingCityReview();

    /**
     * Get feedback requiring decision (status = 6)
     */
    @Select("SELECT * FROM CheckFeedBack WHERE DataStatusCode = 6 ORDER BY PatchCode DESC, sn")
    List<CheckFeedBack> getPendingDecision();

    /**
     * Insert feedback
     */
    @Insert("INSERT INTO CheckFeedBack(DataID, PatchCode, CheckType, CreateTime, CJR, PublishiTime, LastReportTime, DataStatus, DataStatusCode, WorkerName, WorkerCertificateCode, CertificateCode, phone, PostTypeName, Unit, UnitCode, Country, SheBaoCase, ShebaoUnit, GongjijinCase, ProjectCase, SourceTime, CaseDesc, WorkerRerpotTime, AcceptCountry, AcceptTime, AcceptMan, AcceptResult, CountryReportTime, CountryReportCode, CheckTime, CheckMan, CheckResult, ConfirmTime, ConfirmMan, ConfirmResult, SheBaoCheckTime, SheBaoRtnTime, sn, BackReason, BackUnit, PassType) VALUES (#{dataId}, #{patchCode}, #{checkType}, #{createTime}, #{cjr}, #{publishiTime}, #{lastReportTime}, #{dataStatus}, #{dataStatusCode}, #{workerName}, #{workerCertificateCode}, #{certificateCode}, #{phone}, #{postTypeName}, #{unit}, #{unitCode}, #{country}, #{sheBaoCase}, #{shebaoUnit}, #{gongjijinCase}, #{projectCase}, #{sourceTime}, #{caseDesc}, #{workerRerpotTime}, #{acceptCountry}, #{acceptTime}, #{acceptMan}, #{acceptResult}, #{countryReportTime}, #{countryReportCode}, #{checkTime}, #{checkMan}, #{checkResult}, #{confirmTime}, #{confirmMan}, #{confirmResult}, #{sheBaoCheckTime}, #{sheBaoRtnTime}, #{sn}, #{backReason}, #{backUnit}, #{passType})")
    int insertCheckFeedBack(CheckFeedBack checkFeedBack);

    /**
     * Update feedback
     */
    @Update("UPDATE CheckFeedBack SET PatchCode = #{patchCode}, CheckType = #{checkType}, PublishiTime = #{publishiTime}, LastReportTime = #{lastReportTime}, DataStatus = #{dataStatus}, DataStatusCode = #{dataStatusCode}, WorkerName = #{workerName}, WorkerCertificateCode = #{workerCertificateCode}, CertificateCode = #{certificateCode}, phone = #{phone}, PostTypeName = #{postTypeName}, Unit = #{unit}, UnitCode = #{unitCode}, Country = #{country}, SheBaoCase = #{sheBaoCase}, ShebaoUnit = #{shebaoUnit}, GongjijinCase = #{gongjijinCase}, ProjectCase = #{projectCase}, SourceTime = #{sourceTime}, CaseDesc = #{caseDesc}, WorkerRerpotTime = #{workerRerpotTime}, AcceptCountry = #{acceptCountry}, AcceptTime = #{acceptTime}, AcceptMan = #{acceptMan}, AcceptResult = #{acceptResult}, CountryReportTime = #{countryReportTime}, CountryReportCode = #{countryReportCode}, CheckTime = #{checkTime}, CheckMan = #{checkMan}, CheckResult = #{checkResult}, ConfirmTime = #{confirmTime}, ConfirmMan = #{confirmMan}, ConfirmResult = #{confirmResult}, SheBaoCheckTime = #{sheBaoCheckTime}, SheBaoRtnTime = #{sheBaoRtnTime}, sn = #{sn}, BackReason = #{backReason}, BackUnit = #{backUnit}, PassType = #{passType} WHERE DataID = #{dataId}")
    int updateCheckFeedBack(CheckFeedBack checkFeedBack);

    /**
     * Delete feedback
     */
    @Delete("DELETE FROM CheckFeedBack WHERE DataID = #{dataId}")
    int deleteCheckFeedBack(@Param("dataId") String dataId);

    /**
     * Count feedback records
     */
    @Select("SELECT COUNT(*) FROM CheckFeedBack")
    int count();

    /**
     * Count by status code
     */
    @Select("SELECT COUNT(*) FROM CheckFeedBack WHERE DataStatusCode = #{dataStatusCode}")
    int countByStatusCode(@Param("dataStatusCode") Integer dataStatusCode);
}
