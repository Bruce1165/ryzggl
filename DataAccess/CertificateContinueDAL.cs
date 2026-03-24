using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
    /// <summary>
    ///
    /// </summary>
    public class CertificateContinueDAL
    {
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="certificateContinueOb">对象实体类</param>
        /// <returns></returns>
        public static long Insert(CertificateContinueOB certificateContinueOb)
        {
            return Insert(null, certificateContinueOb);
        }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="certificateContinueOb">对象实体类</param>
        /// <returns></returns>
        public static long Insert(DbTransaction tran, CertificateContinueOB certificateContinueOb)
        {
            DBHelper db = new DBHelper();

            string sql = @"INSERT INTO dbo.CertificateContinue(CertificateID,ApplyDate,ApplyMan,ApplyCode,[GETDATE],GetResult,GetMan,GetCode,CheckDate,CheckResult,CheckMan,CheckCode,ConfirmDate,ConfirmResult,ConfirmMan,ConfirmCode,ValidStartDate,ValidEndDate,[STATUS],CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,UnitCode,Phone,FirstCheckUnitID,NewUnitName,NewUnitCode,NewUnitCheckTime,NewUnitAdvise,FirstCheckUnitName,FirstCheckUnitCode,jxjyway) 
                            VALUES (@CertificateID,@ApplyDate,@ApplyMan,@ApplyCode,@GetDate,@GetResult,@GetMan,@GetCode,@CheckDate,@CheckResult,@CheckMan,@CheckCode,@ConfirmDate,@ConfirmResult,@ConfirmMan,@ConfirmCode,@ValidStartDate,@ValidEndDate,@Status,@CreatePersonID,@CreateTime,@ModifyPersonID,@ModifyTime,@UnitCode,@Phone,@FirstCheckUnitID,@NewUnitName,@NewUnitCode,@NewUnitCheckTime,@NewUnitAdvise,@FirstCheckUnitName,@FirstCheckUnitCode,@jxjyway);SELECT @CertificateContinueID = @@IDENTITY";

            List<SqlParameter> p = new List<SqlParameter>
            {
                db.CreateOutParameter("CertificateContinueID", DbType.Int64),
                db.CreateParameter("CertificateID", DbType.Int64, certificateContinueOb.CertificateID),
                db.CreateParameter("ApplyDate", DbType.DateTime, certificateContinueOb.ApplyDate),
                db.CreateParameter("ApplyMan", DbType.String, certificateContinueOb.ApplyMan),
                db.CreateParameter("ApplyCode", DbType.String, certificateContinueOb.ApplyCode),
                db.CreateParameter("GetDate", DbType.DateTime, certificateContinueOb.GetDate),
                db.CreateParameter("GetResult", DbType.String, certificateContinueOb.GetResult),
                db.CreateParameter("GetMan", DbType.String, certificateContinueOb.GetMan),
                db.CreateParameter("GetCode", DbType.String, certificateContinueOb.GetCode),
                db.CreateParameter("CheckDate", DbType.DateTime, certificateContinueOb.CheckDate),
                db.CreateParameter("CheckResult", DbType.String, certificateContinueOb.CheckResult),
                db.CreateParameter("CheckMan", DbType.String, certificateContinueOb.CheckMan),
                db.CreateParameter("CheckCode", DbType.String, certificateContinueOb.CheckCode),
                db.CreateParameter("ConfirmDate", DbType.DateTime, certificateContinueOb.ConfirmDate),
                db.CreateParameter("ConfirmResult", DbType.String, certificateContinueOb.ConfirmResult),
                db.CreateParameter("ConfirmMan", DbType.String, certificateContinueOb.ConfirmMan),
                db.CreateParameter("ConfirmCode", DbType.String, certificateContinueOb.ConfirmCode),
                db.CreateParameter("ValidStartDate", DbType.DateTime, certificateContinueOb.ValidStartDate),
                db.CreateParameter("ValidEndDate", DbType.DateTime, certificateContinueOb.ValidEndDate),
                db.CreateParameter("Status", DbType.String, certificateContinueOb.Status),
                db.CreateParameter("CreatePersonID", DbType.Int64, certificateContinueOb.CreatePersonID),
                db.CreateParameter("CreateTime", DbType.DateTime, certificateContinueOb.CreateTime),
                db.CreateParameter("ModifyPersonID", DbType.Int64, certificateContinueOb.ModifyPersonID),
                db.CreateParameter("ModifyTime", DbType.DateTime, certificateContinueOb.ModifyTime),
                db.CreateParameter("UnitCode", DbType.String, certificateContinueOb.UnitCode),
                db.CreateParameter("Phone", DbType.String, certificateContinueOb.Phone),
                db.CreateParameter("FirstCheckUnitID", DbType.Int64, certificateContinueOb.FirstCheckUnitID),
                db.CreateParameter("NewUnitName", DbType.String, certificateContinueOb.NewUnitName),
                db.CreateParameter("NewUnitCode", DbType.String, certificateContinueOb.NewUnitCode),
                db.CreateParameter("NewUnitCheckTime", DbType.DateTime, certificateContinueOb.NewUnitCheckTime),
                db.CreateParameter("NewUnitAdvise", DbType.String, certificateContinueOb.NewUnitAdvise),
                db.CreateParameter("FirstCheckUnitName", DbType.String, certificateContinueOb.FirstCheckUnitName),
                db.CreateParameter("FirstCheckUnitCode", DbType.String, certificateContinueOb.FirstCheckUnitCode),
                db.CreateParameter("jxjyway", DbType.String, certificateContinueOb.jxjyway)

            };
            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            certificateContinueOb.CertificateContinueID = Convert.ToInt64(p[0].Value);
            return rtn;
        }

        /// <summary>
        /// 新增批量续期申请
        /// </summary>
        /// <param name="certificateContinueOb">续期参数</param>
        /// <param name="filterString">证书过滤条件</param>
        /// <returns></returns>
        public static long InsertBatch(CertificateContinueOB certificateContinueOb, string filterString)
        {
            DBHelper db = new DBHelper();
            const string sql = @"insert into DBO.CERTIFICATECONTINUE
(CERTIFICATEID,APPLYDATE,APPLYMAN,APPLYCODE,[STATUS],CREATEPERSONID,CREATETIME,UNITCODE,PHONE,FIRSTCHECKUNITID,NewUnitName,NewUnitCode )
SELECT CERTIFICATEID,@APPLYDATE,@APPLYMAN,@APPLYCODE,@STATUS,@CREATEPERSONID,@CREATETIME,@UNITCODE,@PHONE,@FIRSTCHECKUNITID,UnitName,@UNITCODE
FROM DBO.VIEW_CERTIFICATE_LEFT_CERTIFICATECONTINUE where 1=1 {0} order by CertificateID";
            List<SqlParameter> p = new List<SqlParameter>
            {
                db.CreateParameter("CertificateID", DbType.Int64, certificateContinueOb.CertificateID),
                db.CreateParameter("ApplyDate", DbType.DateTime, certificateContinueOb.ApplyDate),
                db.CreateParameter("ApplyMan", DbType.String, certificateContinueOb.ApplyMan),
                db.CreateParameter("ApplyCode", DbType.String, certificateContinueOb.ApplyCode),
                db.CreateParameter("Status", DbType.String, certificateContinueOb.Status),
                db.CreateParameter("CreatePersonID", DbType.Int64, certificateContinueOb.CreatePersonID),
                db.CreateParameter("CreateTime", DbType.DateTime, certificateContinueOb.CreateTime),
                db.CreateParameter("UnitCode", DbType.String, certificateContinueOb.UnitCode),
                db.CreateParameter("Phone", DbType.String, certificateContinueOb.Phone),
                db.CreateParameter("FirstCheckUnitID", DbType.Int64, certificateContinueOb.FirstCheckUnitID)
            };
            return db.ExcuteNonQuery(string.Format(sql, filterString), p.ToArray());
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="certificateContinueOb">对象实体类</param>
        /// <returns></returns>
        public static int Update(CertificateContinueOB certificateContinueOb)
        {
            return Update(null, certificateContinueOb);
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="certificateContinueOb"></param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, CertificateContinueOB certificateContinueOb)
        {
            const string sql = @"UPDATE dbo.CertificateContinue 
                                SET	CertificateID = @CertificateID,ApplyDate = @ApplyDate,ApplyMan = @ApplyMan,ApplyCode = @ApplyCode,[GETDATE] = @GetDate,GetResult = @GetResult,
                                GetMan = @GetMan,GetCode = @GetCode,CheckDate = @CheckDate,CheckResult = @CheckResult,CheckMan = @CheckMan,CheckCode = @CheckCode,ConfirmDate = @ConfirmDate,
                                ConfirmResult = @ConfirmResult,ConfirmMan = @ConfirmMan,ConfirmCode = @ConfirmCode,ValidStartDate = @ValidStartDate,ValidEndDate = @ValidEndDate,
                                [STATUS]= @Status,CreatePersonID = @CreatePersonID,CreateTime = @CreateTime,ModifyPersonID = @ModifyPersonID,ModifyTime = @ModifyTime,UnitCode= @UnitCode,
                                Phone= @Phone,FirstCheckUnitID= @FirstCheckUnitID,NewUnitName=@NewUnitName,NewUnitCode=@NewUnitCode ,NewUnitCheckTime=@NewUnitCheckTime,NewUnitAdvise=@NewUnitAdvise,
                                FirstCheckUnitName=@FirstCheckUnitName,FirstCheckUnitCode= @FirstCheckUnitCode,ReportCode=@ReportCode,ReportDate=@ReportDate,ReportMan=@ReportMan,jxjyway = @jxjyway
                                WHERE CertificateContinueID = @CertificateContinueID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>
            {
                db.CreateParameter("CertificateContinueID", DbType.Int64, certificateContinueOb.CertificateContinueID),
                db.CreateParameter("CertificateID", DbType.Int64, certificateContinueOb.CertificateID),
                db.CreateParameter("ApplyDate", DbType.DateTime, certificateContinueOb.ApplyDate),
                db.CreateParameter("ApplyMan", DbType.String, certificateContinueOb.ApplyMan),
                db.CreateParameter("ApplyCode", DbType.String, certificateContinueOb.ApplyCode),
                db.CreateParameter("GetDate", DbType.DateTime, certificateContinueOb.GetDate),
                db.CreateParameter("GetResult", DbType.String, certificateContinueOb.GetResult),
                db.CreateParameter("GetMan", DbType.String, certificateContinueOb.GetMan),
                db.CreateParameter("GetCode", DbType.String, certificateContinueOb.GetCode),
                db.CreateParameter("CheckDate", DbType.DateTime, certificateContinueOb.CheckDate),
                db.CreateParameter("CheckResult", DbType.String, certificateContinueOb.CheckResult),
                db.CreateParameter("CheckMan", DbType.String, certificateContinueOb.CheckMan),
                db.CreateParameter("CheckCode", DbType.String, certificateContinueOb.CheckCode),
                db.CreateParameter("ConfirmDate", DbType.DateTime, certificateContinueOb.ConfirmDate),
                db.CreateParameter("ConfirmResult", DbType.String, certificateContinueOb.ConfirmResult),
                db.CreateParameter("ConfirmMan", DbType.String, certificateContinueOb.ConfirmMan),
                db.CreateParameter("ConfirmCode", DbType.String, certificateContinueOb.ConfirmCode),
                db.CreateParameter("ValidStartDate", DbType.DateTime, certificateContinueOb.ValidStartDate),
                db.CreateParameter("ValidEndDate", DbType.DateTime, certificateContinueOb.ValidEndDate),
                db.CreateParameter("Status", DbType.String, certificateContinueOb.Status),
                db.CreateParameter("CreatePersonID", DbType.Int64, certificateContinueOb.CreatePersonID),
                db.CreateParameter("CreateTime", DbType.DateTime, certificateContinueOb.CreateTime),
                db.CreateParameter("ModifyPersonID", DbType.Int64, certificateContinueOb.ModifyPersonID),
                db.CreateParameter("ModifyTime", DbType.DateTime, certificateContinueOb.ModifyTime),
                db.CreateParameter("UnitCode", DbType.String, certificateContinueOb.UnitCode),
                db.CreateParameter("Phone", DbType.String, certificateContinueOb.Phone),
                db.CreateParameter("FirstCheckUnitID", DbType.Int64, certificateContinueOb.FirstCheckUnitID),
                db.CreateParameter("NewUnitName", DbType.String, certificateContinueOb.NewUnitName),
                db.CreateParameter("NewUnitCode", DbType.String, certificateContinueOb.NewUnitCode),
                db.CreateParameter("NewUnitCheckTime", DbType.DateTime, certificateContinueOb.NewUnitCheckTime),
                db.CreateParameter("NewUnitAdvise", DbType.String, certificateContinueOb.NewUnitAdvise),
                db.CreateParameter("FirstCheckUnitName", DbType.String, certificateContinueOb.FirstCheckUnitName),
                db.CreateParameter("FirstCheckUnitCode", DbType.String, certificateContinueOb.FirstCheckUnitCode),
                db.CreateParameter("ReportCode", DbType.String, certificateContinueOb.ReportCode),
                db.CreateParameter("ReportDate", DbType.DateTime, certificateContinueOb.ReportDate),
                db.CreateParameter("ReportMan", DbType.String, certificateContinueOb.ReportMan),
                db.CreateParameter("jxjyway", DbType.String, certificateContinueOb.jxjyway)

            };
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="certificateContinueId">主键</param>
        /// <returns></returns>
        public static int Delete(long certificateContinueId)
        {
            return Delete(null, certificateContinueId);
        }

        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="certificateContinueId">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, long certificateContinueId)
        {
            const string sql = @"DELETE FROM dbo.CertificateContinue WHERE CertificateContinueID = @CertificateContinueID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>
            {
                db.CreateParameter("CertificateContinueID", DbType.Int64, certificateContinueId)
            };
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="certificateContinueOb">对象实体类</param>
        /// <returns></returns>
        public static int Delete(CertificateContinueOB certificateContinueOb)
        {
            return Delete(null, certificateContinueOb);
        }

        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="certificateContinueOb"></param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, CertificateContinueOB certificateContinueOb)
        {
            const string sql = @"DELETE FROM dbo.CertificateContinue WHERE CertificateContinueID = @CertificateContinueID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>
            {
                db.CreateParameter("CertificateContinueID", DbType.Int64, certificateContinueOb.CertificateContinueID)
            };
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="certificateContinueId">主键</param>
        public static CertificateContinueOB GetObject(long certificateContinueId)
        {
            const string sql = @"SELECT CertificateContinueID,CertificateID,ApplyDate,ApplyMan,ApplyCode,[GETDATE],GetResult,GetMan,GetCode,CheckDate,CheckResult,CheckMan,CheckCode,
                                ConfirmDate,ConfirmResult,ConfirmMan,ConfirmCode,ValidStartDate,ValidEndDate,[STATUS],CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,UnitCode,Phone,
                                FirstCheckUnitID,NewUnitName,NewUnitCode,[SHEBAOCHECK],NewUnitCheckTime,NewUnitAdvise,FirstCheckUnitName,FirstCheckUnitCode,ReportCode,ReportMan,ReportDate,
                                [Job],[SheBaoCheckTime],[ZACheckTime],[ZACheckResult],[ZACheckRemark],jxjyway
                                FROM dbo.CertificateContinue 
                                WHERE CertificateContinueID = @CertificateContinueID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("CertificateContinueID", DbType.Int64, certificateContinueId));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    CertificateContinueOB certificateContinueOb = null;
                    if (reader.Read())
                    {
                        certificateContinueOb = new CertificateContinueOB();
                        if (reader["CertificateContinueID"] != DBNull.Value) certificateContinueOb.CertificateContinueID = Convert.ToInt64(reader["CertificateContinueID"]);
                        if (reader["CertificateID"] != DBNull.Value) certificateContinueOb.CertificateID = Convert.ToInt64(reader["CertificateID"]);
                        if (reader["ApplyDate"] != DBNull.Value) certificateContinueOb.ApplyDate = Convert.ToDateTime(reader["ApplyDate"]);
                        if (reader["ApplyMan"] != DBNull.Value) certificateContinueOb.ApplyMan = Convert.ToString(reader["ApplyMan"]);
                        if (reader["ApplyCode"] != DBNull.Value) certificateContinueOb.ApplyCode = Convert.ToString(reader["ApplyCode"]);
                        if (reader["GetDate"] != DBNull.Value) certificateContinueOb.GetDate = Convert.ToDateTime(reader["GetDate"]);
                        if (reader["GetResult"] != DBNull.Value) certificateContinueOb.GetResult = Convert.ToString(reader["GetResult"]);
                        if (reader["GetMan"] != DBNull.Value) certificateContinueOb.GetMan = Convert.ToString(reader["GetMan"]);
                        if (reader["GetCode"] != DBNull.Value) certificateContinueOb.GetCode = Convert.ToString(reader["GetCode"]);
                        if (reader["CheckDate"] != DBNull.Value) certificateContinueOb.CheckDate = Convert.ToDateTime(reader["CheckDate"]);
                        if (reader["CheckResult"] != DBNull.Value) certificateContinueOb.CheckResult = Convert.ToString(reader["CheckResult"]);
                        if (reader["CheckMan"] != DBNull.Value) certificateContinueOb.CheckMan = Convert.ToString(reader["CheckMan"]);
                        if (reader["CheckCode"] != DBNull.Value) certificateContinueOb.CheckCode = Convert.ToString(reader["CheckCode"]);
                        if (reader["ConfirmDate"] != DBNull.Value) certificateContinueOb.ConfirmDate = Convert.ToDateTime(reader["ConfirmDate"]);
                        if (reader["ConfirmResult"] != DBNull.Value) certificateContinueOb.ConfirmResult = Convert.ToString(reader["ConfirmResult"]);
                        if (reader["ConfirmMan"] != DBNull.Value) certificateContinueOb.ConfirmMan = Convert.ToString(reader["ConfirmMan"]);
                        if (reader["ConfirmCode"] != DBNull.Value) certificateContinueOb.ConfirmCode = Convert.ToString(reader["ConfirmCode"]);
                        if (reader["ValidStartDate"] != DBNull.Value) certificateContinueOb.ValidStartDate = Convert.ToDateTime(reader["ValidStartDate"]);
                        if (reader["ValidEndDate"] != DBNull.Value) certificateContinueOb.ValidEndDate = Convert.ToDateTime(reader["ValidEndDate"]);
                        if (reader["Status"] != DBNull.Value) certificateContinueOb.Status = Convert.ToString(reader["Status"]);
                        if (reader["CreatePersonID"] != DBNull.Value) certificateContinueOb.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                        if (reader["CreateTime"] != DBNull.Value) certificateContinueOb.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                        if (reader["ModifyPersonID"] != DBNull.Value) certificateContinueOb.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                        if (reader["ModifyTime"] != DBNull.Value) certificateContinueOb.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                        if (reader["UnitCode"] != DBNull.Value) certificateContinueOb.UnitCode = Convert.ToString(reader["UnitCode"]);
                        if (reader["Phone"] != DBNull.Value) certificateContinueOb.Phone = Convert.ToString(reader["Phone"]);
                        if (reader["FirstCheckUnitID"] != DBNull.Value) certificateContinueOb.FirstCheckUnitID = Convert.ToInt64(reader["FirstCheckUnitID"]);
                        if (reader["NewUnitName"] != DBNull.Value) certificateContinueOb.NewUnitName = Convert.ToString(reader["NewUnitName"]);
                        if (reader["NewUnitCode"] != DBNull.Value) certificateContinueOb.NewUnitCode = Convert.ToString(reader["NewUnitCode"]);
                        if (reader["SheBaoCheck"] != DBNull.Value) certificateContinueOb.SheBaoCheck = Convert.ToByte(reader["SheBaoCheck"]);
                        if (reader["NewUnitCheckTime"] != DBNull.Value) certificateContinueOb.NewUnitCheckTime = Convert.ToDateTime(reader["NewUnitCheckTime"]);
                        if (reader["NewUnitAdvise"] != DBNull.Value) certificateContinueOb.NewUnitAdvise = Convert.ToString(reader["NewUnitAdvise"]);
                        if (reader["FirstCheckUnitName"] != DBNull.Value) certificateContinueOb.FirstCheckUnitName = Convert.ToString(reader["FirstCheckUnitName"]);
                        if (reader["FirstCheckUnitCode"] != DBNull.Value) certificateContinueOb.FirstCheckUnitCode = Convert.ToString(reader["FirstCheckUnitCode"]);
                        if (reader["ReportCode"] != DBNull.Value) certificateContinueOb.ReportCode = Convert.ToString(reader["ReportCode"]);
                        if (reader["ReportMan"] != DBNull.Value) certificateContinueOb.ReportMan = Convert.ToString(reader["ReportMan"]);
                        if (reader["ReportDate"] != DBNull.Value) certificateContinueOb.ReportDate = Convert.ToDateTime(reader["ReportDate"]);

                        if (reader["Job"] != DBNull.Value) certificateContinueOb.Job = Convert.ToString(reader["Job"]);
                        if (reader["SheBaoCheckTime"] != DBNull.Value) certificateContinueOb.SheBaoCheckTime = Convert.ToDateTime(reader["SheBaoCheckTime"]);
                        if (reader["ZACheckTime"] != DBNull.Value) certificateContinueOb.ZACheckTime = Convert.ToDateTime(reader["ZACheckTime"]);
                        if (reader["ZACheckResult"] != DBNull.Value) certificateContinueOb.ZACheckResult = Convert.ToInt32(reader["ZACheckResult"]);
                        if (reader["ZACheckRemark"] != DBNull.Value) certificateContinueOb.ZACheckRemark = Convert.ToString(reader["ZACheckRemark"]);
                        if (reader["jxjyway"] != DBNull.Value) certificateContinueOb.jxjyway = Convert.ToInt32(reader["jxjyway"]);

                    }
                    reader.Close();
                    db.Close();
                    return certificateContinueOb;
                }
            }
            catch
            {
                db.Close();
                throw;
            }
        }

        /// <summary>
        /// 获取续期历史集合
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        public static DataTable GetList(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "DBO.VIEW_CERTIFICATECONTINUE", "*", filterWhereString, orderBy == "" ? "CERTIFICATECONTINUEID desc" : orderBy);
        }


        /// <summary>
        /// 统计查询续期历史结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("DBO.VIEW_CERTIFICATECONTINUE", filterWhereString);
        }

        /// <summary>
        /// 获取带最后一次续期状态（包含未续期）证书集合
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        public static DataTable GetListView(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "DBO.VIEW_CERTIFICATE_LEFT_CERTIFICATECONTINUE", "*", filterWhereString, orderBy == "" ? " POSTID,CERTIFICATECODE" : orderBy);
        }

        /// <summary>
        /// 统计查询带最后一次续期状态（包含未续期）证书集合记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountView(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("DBO.VIEW_CERTIFICATE_LEFT_CERTIFICATECONTINUE", filterWhereString);
        }

        /// <summary>
        /// 当年三类人应续期证书信息（过滤企业资质和建造师）
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <param name="filterWhereString"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static DataTable GetListView_ContinuenableCurYearOfThreeClass(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "DBO.VIEW_CERTIFICATECONTINUE_THREECLASS_CURYEAR", "*", filterWhereString, orderBy == "" ? " CERTIFICATEID" : orderBy);
        }

        /// <summary>
        /// 统计当年三类人应续期证书信息（过滤企业资质和建造师）
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountView_ContinuenableCurYearOfThreeClass(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("DBO.VIEW_CERTIFICATECONTINUE_THREECLASS_CURYEAR", filterWhereString);
        }

        /// <summary>
        ///企业批量确认
        /// </summary>
        /// <param name="certificateContinueOb"></param>
        /// <param name="filterWhereString"></param>
        /// <returns></returns>
        public static int CheckUnit(CertificateContinueOB certificateContinueOb, string filterWhereString)
        {
            string sql = "UPDATE dbo.CertificateContinue SET NewUnitAdvise =@NewUnitAdvise,[NewUnitCheckTime] = @NewUnitCheckTime,[STATUS]=@Status WHERE 2>1" + filterWhereString;
            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>
            {
                db.CreateParameter("NewUnitCheckTime", DbType.DateTime, certificateContinueOb.NewUnitCheckTime),
                db.CreateParameter("NewUnitAdvise", DbType.String, certificateContinueOb.NewUnitAdvise),  
                db.CreateParameter("Status", DbType.String, certificateContinueOb.Status)
            };
            return db.ExcuteNonQuery(sql, p.ToArray());
        }

        /// <summary>
        ///续期受理
        /// </summary>
        /// <param name="certificateContinueOb"></param>
        /// <param name="filterWhereString"></param>
        /// <returns></returns>
        public static int CheckAccept(DbTransaction tran, CertificateContinueOB certificateContinueOb, string filterWhereString)
        {
            string sql = "UPDATE dbo.CertificateContinue SET GetResult =@GetResult,[GETDATE] = @GetDate,GetMan = @GetMan,GetCode = @GetCode,[STATUS]=@Status WHERE 2>1" + filterWhereString;
            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>
            {
                db.CreateParameter("GetDate", DbType.DateTime, certificateContinueOb.GetDate),
                db.CreateParameter("GetResult", DbType.String, certificateContinueOb.GetResult),
                db.CreateParameter("GetMan", DbType.String, certificateContinueOb.GetMan),
                db.CreateParameter("GetCode", DbType.String, certificateContinueOb.GetCode),
                db.CreateParameter("Status", DbType.String, certificateContinueOb.Status)
            };
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 续期批量审核
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="certificateContinueOb">审核结果参数</param>
        /// <param name="filterWhereString">过滤条件</param>
        /// <returns></returns>
        public static int Check(DbTransaction tran, CertificateContinueOB certificateContinueOb, string filterWhereString)
        {
            string sql = "UPDATE dbo.CertificateContinue SET CheckResult =@CheckResult,CheckDate = @CheckDate,CheckMan = @CheckMan,CheckCode = @CheckCode,[STATUS]=@Status WHERE 2>1" + filterWhereString;
            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>
            {
                db.CreateParameter("CheckDate", DbType.DateTime, certificateContinueOb.CheckDate),
                db.CreateParameter("CheckResult", DbType.String, certificateContinueOb.CheckResult),
                db.CreateParameter("CheckMan", DbType.String, certificateContinueOb.CheckMan),
                db.CreateParameter("CheckCode", DbType.String, certificateContinueOb.CheckCode),
                db.CreateParameter("Status", DbType.String, certificateContinueOb.Status)
            };
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 续期批量决定
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="certificateContinueOb">审核结果参数</param>
        /// <param name="filterWhereString">过滤条件</param>
        /// <returns></returns>
        public static int Confirm(DbTransaction tran, CertificateContinueOB certificateContinueOb, string filterWhereString)
        {
            string sql = "UPDATE dbo.CertificateContinue SET ConfirmResult =@ConfirmResult,ConfirmDate = @ConfirmDate,ConfirmMan = @ConfirmMan,ConfirmCode = @ConfirmCode,[STATUS]=@Status WHERE 2>1" + filterWhereString;
            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>
            {
                db.CreateParameter("ConfirmDate", DbType.DateTime, certificateContinueOb.ConfirmDate),
                db.CreateParameter("ConfirmResult", DbType.String, certificateContinueOb.ConfirmResult),
                db.CreateParameter("ConfirmMan", DbType.String, certificateContinueOb.ConfirmMan),
                db.CreateParameter("ConfirmCode", DbType.String, certificateContinueOb.ConfirmCode),
                db.CreateParameter("Status", DbType.String, certificateContinueOb.Status)
            };
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 返回查询续期申请证书ID集合，用逗号分隔
        /// </summary>
        /// <param name="filterWhereString">证书ID集合，用逗号分隔</param>
        /// <returns></returns>
        public static string GetCertificateIDList(string filterWhereString)
        {
            DataTable dt = CommonDAL.GetDataTable(0, int.MaxValue - 1, "DBO.VIEW_CERTIFICATECONTINUE", "CertificateID", filterWhereString, "CERTIFICATECONTINUEID");
            StringBuilder sb = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                sb.Append(",").Append(dr["CertificateID"]);
            }
            if (sb.Length > 0) sb.Remove(0, 1);
            return sb.ToString();
        }

        /// <summary>
        /// 添加续期初审汇总记录
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="ReportCode">汇总批次号</param>
        /// <param name="filterWhereString">过滤条件</param>
        /// <returns></returns>
        public static int ReportAdd(DbTransaction tran, string ReportCode, string filterWhereString)
        {
            string sql = "update [CERTIFICATECONTINUE] set ReportCode='{0}',ReportDate=getdate() where 2>1 {1}";
            DBHelper db = new DBHelper();

            return db.ExcuteNonQuery(tran, string.Format(sql, ReportCode, filterWhereString));
        }

        /// <summary>
        /// 取消初审汇总
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="ReportCode">汇总批次号</param>
        /// <returns></returns>
        public static int ReportCancel(DbTransaction tran, string ReportCode)
        {
            string sql = "update CERTIFICATECONTINUE set ReportCode=null,ReportDate=null where ReportCode='{0}'";
            DBHelper db = new DBHelper();

            return db.ExcuteNonQuery(tran, string.Format(sql, ReportCode));
        }

        /// <summary>
        /// 汇总上报/取消
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="ReportCode">汇总批次号</param>
        /// <param name="IfCommit">true上报，false取消上报</param>
        /// <param name="ReportMan">上报人</param>
        /// <returns></returns>
        public static int ReportCommit(string ReportCode, bool IfCommit, string ReportMan)
        {
            string sql = "";
            if (IfCommit == true)//上报
            {
                sql = "update CERTIFICATECONTINUE set ReportMan='{1}',ReportDate=getdate() where ReportCode='{0}'";
            }
            else//取消上报
            {
                sql = "update CERTIFICATECONTINUE set ReportMan=null where ReportCode='{0}'";
            }
            DBHelper db = new DBHelper();

            return db.ExcuteNonQuery(string.Format(sql, ReportCode, ReportMan));
        }

        /// <summary>
        /// 检查提交的汇总上报是否已经审批
        /// </summary>
        /// <param name="ReportCode">汇总批次号</param>
        /// <returns></returns>
        public static bool IfReportChecked(string ReportCode)
        {
            string sql = "select count(*) ItemCount,[STATUS] from CERTIFICATECONTINUE where ReportCode='{0}' group by [STATUS] ";

            DBHelper db = new DBHelper();

            DataTable dt = db.GetFillData(string.Format(sql, ReportCode));

            if (dt != null && dt.Rows.Count == 1 && dt.Rows[0]["STATUS"].ToString() == EnumManager.CertificateContinueStatus.Accepted)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        /// <summary>
        public static DataTable GetReportList(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows
              , @"(
                    select ReportCode,
                    max(PostTypeID) as PostTypeID,
                    count(*) as CertCount,
                    max(ReportMan) as ReportMan,
                    max(FIRSTCHECKUNITNAME) as FIRSTCHECKUNITNAME,
                    max(FirstCheckUnitCode) as FirstCheckUnitCode,
                    max(ReportDate) as ReportDate,
                    min([GetDate]) as FirstCheckStartDate,
                    max([GetDate])  as FirstCheckEndDate
                    ,(case when max([ReportMan]) is null then '未上报' else '已上报' end ) ReportStatus
                    ,max([STATUS]) CheckStatus
                    from [VIEW_CERTIFICATECONTINUE]
                    where ReportCode >''
                    group by ReportCode 
                  ) t"
              , "*"
              , filterWhereString, orderBy == "" ? " ReportCode" : orderBy);
        }

        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectReportCount(string filterWhereString)
        {

            return CommonDAL.SelectRowCount(@"( select ReportCode,
                                                max(PostTypeID) as PostTypeID,
                                                count(*) as CertCount,
                                                max(ReportMan) as ReportMan,
                                                max(FIRSTCHECKUNITNAME) as FIRSTCHECKUNITNAME,
                                                max(FirstCheckUnitCode) as FirstCheckUnitCode,
                                                max(ReportDate) as ReportDate,
                                                min([GetDate]) as FirstCheckStartDate,
                                                max([GetDate])  as FirstCheckEndDate
                                                ,(case when max([ReportMan]) is null then '未上报' else '已上报' end ) ReportStatus
                                                ,(case when max([CheckMan]) is null then '未审查' else max([STATUS]) end ) CheckStatus
                                                from [VIEW_CERTIFICATECONTINUE]
                                                where ReportCode >''
                                                group by ReportCode
                                                ) t", filterWhereString);
        }

        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        /// <summary>
        public static DataTable GetDeleteList(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "DBO.View_CERTIFICATECONTINUE_DEL", "*", filterWhereString, orderBy == "" ? " DELTIME desc,CERTIFICATECONTINUEID" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectDeleteCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("DBO.View_CERTIFICATECONTINUE_DEL", filterWhereString);
        }

        /// <summary>
        /// 获取证书变续期批记录集合(个人、企业查看：区县上报后才显示初审信息)，业务决定后，住建部校验不通过前3天显示等待，不显示不通过原因
        /// </summary>
        /// <param name="certificateContinueId">续期申请ID</param>
        /// <returns>审批历史记录集合</returns>
        public static DataTable GetCheckHistoryListForGRQY(long certificateContinueId)
        {
            string sql = @"select row_number() over(order by ActionData, Action desc) as RowNo ,t.* from 
                            (
                                 SELECT '续期申请' as 'Action', [APPLYDATE] as ActionData, case [STATUS] when '填报中' then '填报中' else '已提交' end as ActionResult,
	                                case [STATUS] when '填报中' then '未提交审核' else  (case  CREATEPERSONID when 0 then '已申请' else '待单位确认' end) end as ActionRemark, [APPLYMAN] as ActionMan 
                                 FROM [dbo].[VIEW_CERTIFICATECONTINUE] where [CERTIFICATECONTINUEID]={0}  
                                  union all
                                   SELECT '现单位确认' as 'Action', [NewUnitCheckTime] as ActionData,'已确认' ActionResult,[NewUnitAdvise] as ActionRemark, [NEWUNITNAME] as ActionMan 
                                 FROM [dbo].[VIEW_CERTIFICATECONTINUE] where [CERTIFICATECONTINUEID]={0} and [NewUnitCheckTime] >'2019-1-1'
                                  union all
                                   SELECT '初审受理' as 'Action', [GETDATE] as ActionData,case [GETRESULT] when '初审通过' then '已受理' else '不予受理' end ActionResult,[GETRESULT] as ActionRemark, [FirstCheckUnitName] as ActionMan 
                                 FROM [dbo].[VIEW_CERTIFICATECONTINUE] where [CERTIFICATECONTINUEID]={0} and ([ReportMan] is not null or [STATUS]='退回修改')
                                 union all
                                   SELECT '初审上报' as 'Action', [ReportDate] as ActionData,'汇总上报' ActionResult,'已上报' as ActionRemark, [ReportMan] as ActionMan 
                                 FROM [dbo].[VIEW_CERTIFICATECONTINUE] where [CERTIFICATECONTINUEID]={0} and [ReportMan] is not null
                                  union all
                                   SELECT '建委审核' as 'Action', [CHECKDATE] as ActionData,case [CHECKRESULT] when '审核通过' then '已审核' else '审核未通过' end ActionResult,[CHECKRESULT] as ActionRemark, '市住建委' as ActionMan 
                                 FROM [dbo].[VIEW_CERTIFICATECONTINUE] where [CERTIFICATECONTINUEID]={0} and [CHECKDATE] >'1950-1-1'
                                   union all 
	                                SELECT '建委决定' as 'Action', [CONFIRMDATE] as ActionData,case [CONFIRMRESULT] when '决定通过' then '已决定' else '决定未通过' end ActionResult,[CONFIRMRESULT] as ActionRemark, '市住建委' as ActionMan 
                                 FROM [dbo].[VIEW_CERTIFICATECONTINUE] where [CERTIFICATECONTINUEID]={0} and [CONFIRMDATE] >'1950-1-1'
                                   union all 
                                    SELECT '住建部核准' as 'Action', 
	                                    case when c.ZZUrlUpTime > b.[CONFIRMDATE] then  dateadd(hour,1,b.[CONFIRMDATE]) 
			                                    when  Convert(varchar(10),b.[CONFIRMDATE],21) = Convert(varchar(10),c.CHECKDATE,21) and  c.EleCertErrTime > b.[CONFIRMDATE] and b.[CONFIRMDATE] < dateadd(day,-2,getdate()) then c.EleCertErrTime
			                                    else null
	                                    end as ActionData,
                                         case when c.ZZUrlUpTime > b.[CONFIRMDATE] then  '已核准'
			                                    when  Convert(varchar(10),b.[CONFIRMDATE],21) = Convert(varchar(10),c.CHECKDATE,21) and  c.EleCertErrTime > b.[CONFIRMDATE] and b.[CONFIRMDATE] < dateadd(day,-2,getdate()) then '核准未通过'
			                                    else null
	                                    end as ActionResult,
                                        case when c.ZZUrlUpTime > b.[CONFIRMDATE] then  '已生成电子证书（办结）' 
                                                when  Convert(varchar(10),b.[CONFIRMDATE],21) = Convert(varchar(10),c.CHECKDATE,21) and  c.EleCertErrTime > b.[CONFIRMDATE] and b.[CONFIRMDATE] < dateadd(day,-2,getdate()) then '审批不通过，未生成电子证书。' + c.[EleCertErrDesc]
			                                    when  Convert(varchar(10),b.[CONFIRMDATE],21) = Convert(varchar(10),c.CHECKDATE,21) and  c.EleCertErrTime > b.[CONFIRMDATE] then '住建部正在进行数据校验核对，生成电子证书需要1-3个工作日，请您耐心等待。' 
			                                    else null
	                                    end as ActionRemark, 
                                        '住建部' as ActionMan 
                                    FROM [dbo].[VIEW_CERTIFICATECONTINUE] b
                                    inner join [dbo].[CERTIFICATE] c on b.CERTIFICATEID = c.CERTIFICATEID
                                    where b.[CERTIFICATECONTINUEID]={0} and b.[CONFIRMDATE] >'1950-1-1' 
                                    and (c.ZZUrlUpTime > b.[CONFIRMDATE] 
		                                or (c.EleCertErrTime > b.[CONFIRMDATE] and Convert(varchar(10),b.[CONFIRMDATE],21) = Convert(varchar(10),c.CHECKDATE,21) )
                                    )
                            ) t";
            return CommonDAL.GetDataTable(string.Format(sql, certificateContinueId));
        }

        /// <summary>
        /// 获取证书变续期批记录集合（管理端查看）
        /// </summary>
        /// <param name="certificateContinueId">续期申请ID</param>
        /// <returns>审批历史记录集合</returns>
        public static DataTable GetCheckHistoryListForAdmin(long certificateContinueId)
        {
            string sql = @"select row_number() over(order by ActionData, Action desc) as RowNo ,t.* from 
                            (
                             SELECT '续期申请' as 'Action', [APPLYDATE] as ActionData, case [STATUS] when '填报中' then '填报中' else '已提交' end as ActionResult,
	                            case [STATUS] when '填报中' then '未提交审核' else  (case  CREATEPERSONID when 0 then '已申请' else '待单位确认' end) end as ActionRemark, [APPLYMAN] as ActionMan 
                             FROM [dbo].[VIEW_CERTIFICATECONTINUE] where [CERTIFICATECONTINUEID]={0}  
                              union all
                               SELECT '现单位确认' as 'Action', [NewUnitCheckTime] as ActionData,'已确认' ActionResult,[NewUnitAdvise] as ActionRemark, [NEWUNITNAME] as ActionMan 
                             FROM [dbo].[VIEW_CERTIFICATECONTINUE] where [CERTIFICATECONTINUEID]={0} and [NewUnitCheckTime] >'2019-1-1'
                              union all
                               SELECT '初审受理' as 'Action', [GETDATE] as ActionData,case [GETRESULT] when '初审通过' then '已受理' else '不予受理' end ActionResult,[GETRESULT] as ActionRemark, [FirstCheckUnitName] as ActionMan 
                             FROM [dbo].[VIEW_CERTIFICATECONTINUE] where [CERTIFICATECONTINUEID]={0} and [GETDATE] >'1950-1-1'
                             union all
                               SELECT '初审上报' as 'Action', [ReportDate] as ActionData,'汇总上报' ActionResult,'已上报' as ActionRemark, [ReportMan] as ActionMan 
                             FROM [dbo].[VIEW_CERTIFICATECONTINUE] where [CERTIFICATECONTINUEID]={0} and [ReportMan] is not null
                              union all
                               SELECT '建委审核' as 'Action', [CHECKDATE] as ActionData,case [CHECKRESULT] when '审核通过' then '已审核' else '审核未通过' end ActionResult,[CHECKRESULT] as ActionRemark, '市住建委' as ActionMan 
                             FROM [dbo].[VIEW_CERTIFICATECONTINUE] where [CERTIFICATECONTINUEID]={0} and [CHECKDATE] >'1950-1-1'
                               union all 
	                            SELECT '建委决定' as 'Action', [CONFIRMDATE] as ActionData,case [CONFIRMRESULT] when '决定通过' then '已决定' else '决定未通过' end ActionResult,[CONFIRMRESULT] as ActionRemark, '市住建委' as ActionMan 
                             FROM [dbo].[VIEW_CERTIFICATECONTINUE] where [CERTIFICATECONTINUEID]={0} and [CONFIRMDATE] >'1950-1-1'
                                union all 
                                    SELECT '住建部核准' as 'Action', 
	                                    case when c.ZZUrlUpTime > b.[CONFIRMDATE] then  dateadd(hour,1,b.[CONFIRMDATE]) 
			                                    when  Convert(varchar(10),b.[CONFIRMDATE],21) = Convert(varchar(10),c.CHECKDATE,21) and  c.EleCertErrTime > b.[CONFIRMDATE] then c.EleCertErrTime
			                                    else null
	                                    end as ActionData,
                                        case when c.ZZUrlUpTime > b.[CONFIRMDATE] then  '已核准'
			                                    when  Convert(varchar(10),b.[CONFIRMDATE],21) = Convert(varchar(10),c.CHECKDATE,21) and  c.EleCertErrTime > b.[CONFIRMDATE] then '核准未通过'
			                                    else null
	                                    end as ActionResult,
                                        case when c.ZZUrlUpTime > b.[CONFIRMDATE] then  '已生成电子证书（办结）' 
			                                    when  Convert(varchar(10),b.[CONFIRMDATE],21) = Convert(varchar(10),c.CHECKDATE,21) and  c.EleCertErrTime > b.[CONFIRMDATE] then '审批不通过，未生成电子证书。' + c.[EleCertErrDesc]
			                                    else null
	                                    end as ActionRemark, 
                                        '住建部' as ActionMan 
                                    FROM [dbo].[VIEW_CERTIFICATECONTINUE] b
                                    inner join [dbo].[CERTIFICATE] c on b.CERTIFICATEID = c.CERTIFICATEID
                                    where b.[CERTIFICATECONTINUEID]={0} and b.[CONFIRMDATE] >'1950-1-1' 
                                    and (c.ZZUrlUpTime > b.[CONFIRMDATE] 
		                                or (c.EleCertErrTime > b.[CONFIRMDATE] and Convert(varchar(10),b.[CONFIRMDATE],21) = Convert(varchar(10),c.CHECKDATE,21) )
                                    )
                            ) t";
            return CommonDAL.GetDataTable(string.Format(sql, certificateContinueId));
        }
    }
}