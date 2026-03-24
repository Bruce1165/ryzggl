package com.ryzggl.repository;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.ryzggl.entity.ApplyCheckTaskItem;
import org.apache.ibatis.annotations.*;
import java.util.List;

/**
 * ApplyCheckTaskItem Repository
 */
@Mapper
public interface ApplyCheckTaskItemRepository extends BaseMapper<ApplyCheckTaskItem> {

    /**
     * Delete all items by Task ID
     * @param taskId Task ID
     * @return Number of affected rows
     */
    @Delete("DELETE FROM ApplyCheckTaskItem WHERE TaskID = #{taskId}")
    int deleteByTaskId(@Param("taskId") Long taskId);

    /**
     * Batch update check results
     * @param checkMan Approver name
     * @param checkResult Check result
     * @param checkResultDesc Check result description
     * @param filterWhere Where clause condition (without WHERE keyword)
     * @return Number of affected rows
     */
    @Update("UPDATE ApplyCheckTaskItem " +
            "SET CheckMan = #{checkMan}, " +
            "CheckTime = GETDATE(), " +
            "CheckResult = #{checkResult}, " +
            "CheckResultDesc = #{checkResultDesc} " +
            "WHERE 1=1 ${filterWhere}")
    int batchUpdateCheck(@Param("checkMan") String checkMan,
                         @Param("checkResult") String checkResult,
                         @Param("checkResultDesc") String checkResultDesc,
                         @Param("filterWhere") String filterWhere);

    /**
     * Get unchecked items by Task ID
     * @param taskId Task ID
     * @return List of unchecked items
     */
    @Select("SELECT * FROM ApplyCheckTaskItem " +
            "WHERE TaskID = #{taskId} AND CheckTime IS NULL")
    List<ApplyCheckTaskItem> selectUncheckedItems(@Param("taskId") Long taskId);

    /**
     * Get checked items by Task ID
     * @param taskId Task ID
     * @return List of checked items
     */
    @Select("SELECT * FROM ApplyCheckTaskItem " +
            "WHERE TaskID = #{taskId} AND CheckTime IS NOT NULL " +
            "ORDER BY CheckTime DESC")
    List<ApplyCheckTaskItem> selectCheckedItems(@Param("taskId") Long taskId);

    // ==================== SAMPLING METHODS FOR CHECK TASK ====================

    /**
     * Sample from Apply table (二建)
     * @param taskId Task ID
     * @param startDate Start date (yyyy-MM-dd)
     * @param endDate End date (yyyy-MM-dd 23:59:59)
     * @param limit Maximum number of records
     * @return List of sampled items
     */
    @Select("<script>" +
            "SELECT TOP(${limit}) " +
            "#{taskId} AS TaskID, " +
            "1 AS BusTypeID, " +
            "a.ApplyType AS ApplyType, " +
            "'Apply' AS ApplyTableName, " +
            "CAST(a.ApplyID AS VARCHAR(64)) AS DataID, " +
            "a.PSN_Name AS WorkerName, " +
            "a.PSN_CertificateNO AS IDCard, " +
            "'身份证' AS IDCardType, " +
            "a.PSN_RegisterNo AS CertificateCode, " +
            "a.ConfirmDate AS ApplyFinishTime, " +
            "CAST((RAND() * 100000000) AS BIGINT) AS randID " +
            "FROM Apply a " +
            "WHERE (ApplyType = '初始注册' OR ApplyType = '重新注册' OR ApplyType = '增项注册' OR ApplyType = '延期注册') " +
            "AND ConfirmResult = '通过' " +
            "AND PSN_RegisterNo IS NOT NULL " +
            "AND ConfirmDate BETWEEN #{startDate} AND #{endDate} " +
            "ORDER BY randID" +
            "</script>")
    List<ApplyCheckTaskItem> sampleFromApply(
            @Param("taskId") Long taskId,
            @Param("startDate") String startDate,
            @Param("endDate") String endDate,
            @Param("limit") int limit);

    /**
     * Count from Apply table (二建)
     */
    @Select("SELECT COUNT(*) FROM Apply " +
            "WHERE (ApplyType = '初始注册' OR ApplyType = '重新注册' OR ApplyType = '增项注册' OR ApplyType = '延期注册') " +
            "AND ConfirmResult = '通过' " +
            "AND PSN_RegisterNo IS NOT NULL " +
            "AND ConfirmDate BETWEEN #{startDate} AND #{endDate}")
    int countFromApply(
            @Param("startDate") String startDate,
            @Param("endDate") String endDate);

    /**
     * Sample from zjs_Apply table (二造)
     * @param taskId Task ID
     * @param startDate Start date (yyyy-MM-dd)
     * @param endDate End date (yyyy-MM-dd 23:59:59)
     * @param limit Maximum number of records
     * @return List of sampled items
     */
    @Select("<script>" +
            "SELECT TOP(${limit}) " +
            "#{taskId} AS TaskID, " +
            "2 AS BusTypeID, " +
            "a.ApplyType AS ApplyType, " +
            "'zjs_Apply' AS ApplyTableName, " +
            "CAST(a.ApplyID AS VARCHAR(64)) AS DataID, " +
            "a.PSN_Name AS WorkerName, " +
            "a.PSN_CertificateNO AS IDCard, " +
            "'身份证' AS IDCardType, " +
            "a.PSN_RegisterNo AS CertificateCode, " +
            "a.ConfirmDate AS ApplyFinishTime, " +
            "CAST((RAND() * 100000000) AS BIGINT) AS randID " +
            "FROM zjs_Apply a " +
            "WHERE (ApplyType = '初始注册' OR ApplyType = '延期注册') " +
            "AND ConfirmResult = '通过' " +
            "AND PSN_RegisterNo IS NOT NULL " +
            "AND ConfirmDate BETWEEN #{startDate} AND #{endDate} " +
            "ORDER BY randID" +
            "</script>")
    List<ApplyCheckTaskItem> sampleFromZjsApply(
            @Param("taskId") Long taskId,
            @Param("startDate") String startDate,
            @Param("endDate") String endDate,
            @Param("limit") int limit);

    /**
     * Count from zjs_Apply table (二造)
     */
    @Select("SELECT COUNT(*) FROM zjs_Apply " +
            "WHERE (ApplyType = '初始注册' OR ApplyType = '延期注册') " +
            "AND ConfirmResult = '通过' " +
            "AND PSN_RegisterNo IS NOT NULL " +
            "AND ConfirmDate BETWEEN #{startDate} AND #{endDate}")
    int countFromZjsApply(
            @Param("startDate") String startDate,
            @Param("endDate") String endDate);

    /**
     * Sample from ExamSignUp table (安管人员考试报名)
     * @param taskId Task ID
     * @param startDate Start date (yyyy-MM-dd)
     * @param endDate End date (yyyy-MM-dd 23:59:59)
     * @param limit Maximum number of records
     * @return List of sampled items
     */
    @Select("<script>" +
            "SELECT TOP(${limit}) " +
            "#{taskId} AS TaskID, " +
            "3 AS BusTypeID, " +
            "'考试报名' AS ApplyType, " +
            "'EXAMSIGNUP' AS ApplyTableName, " +
            "CAST(s.EXAMSIGNUPID AS VARCHAR(64)) AS DataID, " +
            "s.WORKERNAME AS WorkerName, " +
            "s.CERTIFICATECODE AS IDCard, " +
            "'身份证' AS IDCardType, " +
            "s.RESULTCERTIFICATECODE AS CertificateCode, " +
            "s.PAYCONFIRMDATE AS ApplyFinishTime, " +
            "CAST((RAND() * 100000000) AS BIGINT) AS randID " +
            "FROM EXAMSIGNUP s " +
            "INNER JOIN EXAMPLAN e ON s.EXAMPLANID = e.EXAMPLANID " +
            "WHERE e.PostTypeID < 2 " +
            "AND s.PAYCONFIRMDATE BETWEEN #{startDate} AND #{endDate} " +
            "AND s.RESULTCERTIFICATECODE IS NOT NULL " +
            "ORDER BY randID" +
            "</script>")
    List<ApplyCheckTaskItem> sampleFromExamSignUpForSafety(
            @Param("taskId") Long taskId,
            @Param("startDate") String startDate,
            @Param("endDate") String endDate,
            @Param("limit") int limit);

    /**
     * Count from ExamSignUp table (安管人员考试报名)
     */
    @Select("SELECT COUNT(*) FROM EXAMSIGNUP s " +
            "INNER JOIN EXAMPLAN e ON s.EXAMPLANID = e.EXAMPLANID " +
            "WHERE e.PostTypeID < 2 " +
            "AND s.PAYCONFIRMDATE BETWEEN #{startDate} AND #{endDate} " +
            "AND s.RESULTCERTIFICATECODE IS NOT NULL")
    int countFromExamSignUpForSafety(
            @Param("startDate") String startDate,
            @Param("endDate") String endDate);

    /**
     * Sample from VIEW_CERTIFICATE_ENTER table (安管人员证书进京)
     * @param taskId Task ID
     * @param startDate Start date (yyyy-MM-dd)
     * @param endDate End date (yyyy-MM-dd 23:59:59)
     * @param limit Maximum number of records
     * @return List of sampled items
     */
    @Select("<script>" +
            "SELECT TOP(${limit}) " +
            "#{taskId} AS TaskID, " +
            "3 AS BusTypeID, " +
            "'证书进京' AS ApplyType, " +
            "'CERTIFICATE_ENTER' AS ApplyTableName, " +
            "CAST(ApplyID AS VARCHAR(64)) AS DataID, " +
            "WORKERNAME AS WorkerName, " +
            "WORKERCERTIFICATECODE AS IDCard, " +
            "'身份证' AS IDCardType, " +
            "NEWCERTIFICATECODE AS CertificateCode, " +
            "CONFRIMDATE AS ApplyFinishTime, " +
            "CAST((RAND() * 100000000) AS BIGINT) AS randID " +
            "FROM VIEW_CERTIFICATE_ENTER " +
            "WHERE CONFRIMDATE BETWEEN #{startDate} AND #{endDate} " +
            "AND NEWCERTIFICATECODE IS NOT NULL " +
            "ORDER BY randID" +
            "</script>")
    List<ApplyCheckTaskItem> sampleFromCertificateEnter(
            @Param("taskId") Long taskId,
            @Param("startDate") String startDate,
            @Param("endDate") String endDate,
            @Param("limit") int limit);

    /**
     * Count from VIEW_CERTIFICATE_ENTER table (安管人员证书进京)
     */
    @Select("SELECT COUNT(*) FROM VIEW_CERTIFICATE_ENTER " +
            "WHERE CONFRIMDATE BETWEEN #{startDate} AND #{endDate} " +
            "AND NEWCERTIFICATECODE IS NOT NULL")
    int countFromCertificateEnter(
            @Param("startDate") String startDate,
            @Param("endDate") String endDate);

    /**
     * Sample from CertificateContinue table (安管人员证书续期)
     * @param taskId Task ID
     * @param startDate Start date (yyyy-MM-dd)
     * @param endDate End date (yyyy-MM-dd 23:59:59)
     * @param limit Maximum number of records
     * @return List of sampled items
     */
    @Select("<script>" +
            "SELECT TOP(${limit}) " +
            "#{taskId} AS TaskID, " +
            "3 AS BusTypeID, " +
            "'证书续期' AS ApplyType, " +
            "'CERTIFICATECONTINUE' AS ApplyTableName, " +
            "CAST(x.CERTIFICATECONTINUEID AS VARCHAR(64)) AS DataID, " +
            "c.WORKERNAME AS WorkerName, " +
            "c.WORKERCERTIFICATECODE AS IDCard, " +
            "'身份证' AS IDCardType, " +
            "c.CERTIFICATECODE AS CertificateCode, " +
            "x.CONFIRMDATE AS ApplyFinishTime, " +
            "CAST((RAND() * 100000000) AS BIGINT) AS randID " +
            "FROM CERTIFICATECONTINUE x " +
            "INNER JOIN CERTIFICATE c ON x.CERTIFICATEID = c.CERTIFICATEID " +
            "WHERE c.PostTypeID < 2 " +
            "AND x.CONFIRMDATE BETWEEN #{startDate} AND #{endDate} " +
            "AND x.CONFIRMRESULT = '决定通过' " +
            "ORDER BY randID" +
            "</script>")
    List<ApplyCheckTaskItem> sampleFromCertificateContinueForSafety(
            @Param("taskId") Long taskId,
            @Param("startDate") String startDate,
            @Param("endDate") String endDate,
            @Param("limit") int limit);

    /**
     * Count from CertificateContinue table (安管人员证书续期)
     */
    @Select("SELECT COUNT(*) FROM CERTIFICATECONTINUE x " +
            "INNER JOIN CERTIFICATE c ON x.CERTIFICATEID = c.CERTIFICATEID " +
            "WHERE c.PostTypeID < 2 " +
            "AND x.CONFIRMDATE BETWEEN #{startDate} AND #{endDate} " +
            "AND x.CONFIRMRESULT = '决定通过'")
    int countFromCertificateContinueForSafety(
            @Param("startDate") String startDate,
            @Param("endDate") String endDate);

    /**
     * Sample from ExamSignUp table (特种作业考试报名)
     * @param taskId Task ID
     * @param startDate Start date (yyyy-MM-dd)
     * @param endDate End date (yyyy-MM-dd 23:59:59)
     * @param limit Maximum number of records
     * @return List of sampled items
     */
    @Select("<script>" +
            "SELECT TOP(${limit}) " +
            "#{taskId} AS TaskID, " +
            "4 AS BusTypeID, " +
            "'考试报名' AS ApplyType, " +
            "'EXAMSIGNUP' AS ApplyTableName, " +
            "CAST(s.EXAMSIGNUPID AS VARCHAR(64)) AS DataID, " +
            "s.WORKERNAME AS WorkerName, " +
            "s.CERTIFICATECODE AS IDCard, " +
            "'身份证' AS IDCardType, " +
            "s.RESULTCERTIFICATECODE AS CertificateCode, " +
            "s.PAYCONFIRMDATE AS ApplyFinishTime, " +
            "CAST((RAND() * 100000000) AS BIGINT) AS randID " +
            "FROM EXAMSIGNUP s " +
            "INNER JOIN EXAMPLAN e ON s.EXAMPLANID = e.EXAMPLANID " +
            "WHERE e.PostTypeID = 2 " +
            "AND s.PAYCONFIRMDATE BETWEEN #{startDate} AND #{endDate} " +
            "AND s.RESULTCERTIFICATECODE IS NOT NULL " +
            "ORDER BY randID" +
            "</script>")
    List<ApplyCheckTaskItem> sampleFromExamSignUpForSpecial(
            @Param("taskId") Long taskId,
            @Param("startDate") String startDate,
            @Param("endDate") String endDate,
            @Param("limit") int limit);

    /**
     * Count from ExamSignUp table (特种作业考试报名)
     */
    @Select("SELECT COUNT(*) FROM EXAMSIGNUP s " +
            "INNER JOIN EXAMPLAN e ON s.EXAMPLANID = e.EXAMPLANID " +
            "WHERE e.PostTypeID = 2 " +
            "AND s.PAYCONFIRMDATE BETWEEN #{startDate} AND #{endDate} " +
            "AND s.RESULTCERTIFICATECODE IS NOT NULL")
    int countFromExamSignUpForSpecial(
            @Param("startDate") String startDate,
            @Param("endDate") String endDate);

    /**
     * Sample from CertificateContinue table (特种作业证书续期)
     * @param taskId Task ID
     * @param startDate Start date (yyyy-MM-dd)
     * @param endDate End date (yyyy-MM-dd 23:59:59)
     * @param limit Maximum number of records
     * @return List of sampled items
     */
    @Select("<script>" +
            "SELECT TOP(${limit}) " +
            "#{taskId} AS TaskID, " +
            "4 AS BusTypeID, " +
            "'证书续期' AS ApplyType, " +
            "'CERTIFICATECONTINUE' AS ApplyTableName, " +
            "CAST(x.CERTIFICATECONTINUEID AS VARCHAR(64)) AS DataID, " +
            "c.WORKERNAME AS WorkerName, " +
            "c.WORKERCERTIFICATECODE AS IDCard, " +
            "'身份证' AS IDCardType, " +
            "c.CERTIFICATECODE AS CertificateCode, " +
            "x.CONFIRMDATE AS ApplyFinishTime, " +
            "CAST((RAND() * 100000000) AS BIGINT) AS randID " +
            "FROM CERTIFICATECONTINUE x " +
            "INNER JOIN CERTIFICATE c ON x.CERTIFICATEID = c.CERTIFICATEID " +
            "WHERE c.PostTypeID = 2 " +
            "AND x.CONFIRMDATE BETWEEN #{startDate} AND #{endDate} " +
            "AND x.CONFIRMRESULT = '决定通过' " +
            "ORDER BY randID" +
            "</script>")
    List<ApplyCheckTaskItem> sampleFromCertificateContinueForSpecial(
            @Param("taskId") Long taskId,
            @Param("startDate") String startDate,
            @Param("endDate") String endDate,
            @Param("limit") int limit);

    /**
     * Count from CertificateContinue table (特种作业证书续期)
     */
    @Select("SELECT COUNT(*) FROM CERTIFICATECONTINUE x " +
            "INNER JOIN CERTIFICATE c ON x.CERTIFICATEID = c.CERTIFICATEID " +
            "WHERE c.PostTypeID = 2 " +
            "AND x.CONFIRMDATE BETWEEN #{startDate} AND #{endDate} " +
            "AND x.CONFIRMRESULT = '决定通过'")
    int countFromCertificateContinueForSpecial(
            @Param("startDate") String startDate,
            @Param("endDate") String endDate);
}
