using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
    /// <summary>
    /// 业务类实现--CheckFeedBackDAL(填写类描述)
    /// </summary>
    public class CheckFeedBackDAL
    {
        public CheckFeedBackDAL() { }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_CheckFeedBackMDL">对象实体类</param>
        /// <returns></returns>
        public static int Insert(CheckFeedBackMDL _CheckFeedBackMDL)
        {
            return Insert(null, _CheckFeedBackMDL);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_CheckFeedBackMDL">对象实体类</param>
        /// <returns></returns>
        public static int Insert(DbTransaction tran, CheckFeedBackMDL _CheckFeedBackMDL)
        {
            DBHelper db = new DBHelper();
            string sql = @"
			INSERT INTO dbo.CheckFeedBack(DataID,PatchCode,CheckType,CreateTime,CJR,PublishiTime,LastReportTime,DataStatus,DataStatusCode,WorkerName,WorkerCertificateCode,CertificateCode,phone,PostTypeName,Unit,UnitCode,Country,SheBaoCase,ShebaoUnit,GongjijinCase,ProjectCase,SourceTime,CaseDesc,WorkerRerpotTime,AcceptCountry,AcceptTime,AcceptMan,AcceptResult,CountryReportTime,CountryReportCode,CheckTime,CheckMan,CheckResult,ConfirmTime,ConfirmMan,ConfirmResult,SheBaoCheckTime,SheBaoRtnTime,sn,BackReason,BackUnit,PassType)
			VALUES (@DataID,@PatchCode,@CheckType,@CreateTime,@CJR,@PublishiTime,@LastReportTime,@DataStatus,@DataStatusCode,@WorkerName,@WorkerCertificateCode,@CertificateCode,@phone,@PostTypeName,@Unit,@UnitCode,@Country,@SheBaoCase,@ShebaoUnit,@GongjijinCase,@ProjectCase,@SourceTime,@CaseDesc,@WorkerRerpotTime,@AcceptCountry,@AcceptTime,@AcceptMan,@AcceptResult,@CountryReportTime,@CountryReportCode,@CheckTime,@CheckMan,@CheckResult,@ConfirmTime,@ConfirmMan,@ConfirmResult,@SheBaoCheckTime,@SheBaoRtnTime,@sn,@BackReason,@BackUnit,@PassType)";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("DataID", DbType.String, _CheckFeedBackMDL.DataID));
            p.Add(db.CreateParameter("PatchCode", DbType.Int32, _CheckFeedBackMDL.PatchCode));
            p.Add(db.CreateParameter("CheckType", DbType.String, _CheckFeedBackMDL.CheckType));
            p.Add(db.CreateParameter("CreateTime", DbType.DateTime, _CheckFeedBackMDL.CreateTime));
            p.Add(db.CreateParameter("CJR", DbType.String, _CheckFeedBackMDL.CJR));
            p.Add(db.CreateParameter("PublishiTime", DbType.DateTime, _CheckFeedBackMDL.PublishiTime));
            p.Add(db.CreateParameter("LastReportTime", DbType.DateTime, _CheckFeedBackMDL.LastReportTime));
            p.Add(db.CreateParameter("DataStatus", DbType.String, _CheckFeedBackMDL.DataStatus));
            p.Add(db.CreateParameter("DataStatusCode", DbType.Int32, _CheckFeedBackMDL.DataStatusCode));
            p.Add(db.CreateParameter("WorkerName", DbType.String, _CheckFeedBackMDL.WorkerName));
            p.Add(db.CreateParameter("WorkerCertificateCode", DbType.String, _CheckFeedBackMDL.WorkerCertificateCode));
            p.Add(db.CreateParameter("CertificateCode", DbType.String, _CheckFeedBackMDL.CertificateCode));
            p.Add(db.CreateParameter("phone", DbType.String, _CheckFeedBackMDL.phone));
            p.Add(db.CreateParameter("PostTypeName", DbType.String, _CheckFeedBackMDL.PostTypeName));
            p.Add(db.CreateParameter("Unit", DbType.String, _CheckFeedBackMDL.Unit));
            p.Add(db.CreateParameter("UnitCode", DbType.String, _CheckFeedBackMDL.UnitCode));
            p.Add(db.CreateParameter("Country", DbType.String, _CheckFeedBackMDL.Country));
            p.Add(db.CreateParameter("SheBaoCase", DbType.String, _CheckFeedBackMDL.SheBaoCase));
            p.Add(db.CreateParameter("ShebaoUnit", DbType.String, _CheckFeedBackMDL.ShebaoUnit));
            p.Add(db.CreateParameter("GongjijinCase", DbType.String, _CheckFeedBackMDL.GongjijinCase));
            p.Add(db.CreateParameter("ProjectCase", DbType.String, _CheckFeedBackMDL.ProjectCase));
            p.Add(db.CreateParameter("SourceTime", DbType.DateTime, _CheckFeedBackMDL.SourceTime));
            p.Add(db.CreateParameter("CaseDesc", DbType.String, _CheckFeedBackMDL.CaseDesc));
            p.Add(db.CreateParameter("WorkerRerpotTime", DbType.DateTime, _CheckFeedBackMDL.WorkerRerpotTime));
            p.Add(db.CreateParameter("AcceptCountry", DbType.String, _CheckFeedBackMDL.AcceptCountry));
            p.Add(db.CreateParameter("AcceptTime", DbType.DateTime, _CheckFeedBackMDL.AcceptTime));
            p.Add(db.CreateParameter("AcceptMan", DbType.String, _CheckFeedBackMDL.AcceptMan));
            p.Add(db.CreateParameter("AcceptResult", DbType.String, _CheckFeedBackMDL.AcceptResult));
            p.Add(db.CreateParameter("CountryReportTime", DbType.DateTime, _CheckFeedBackMDL.CountryReportTime));
            p.Add(db.CreateParameter("CountryReportCode", DbType.String, _CheckFeedBackMDL.CountryReportCode));
            p.Add(db.CreateParameter("CheckTime", DbType.DateTime, _CheckFeedBackMDL.CheckTime));
            p.Add(db.CreateParameter("CheckMan", DbType.String, _CheckFeedBackMDL.CheckMan));
            p.Add(db.CreateParameter("CheckResult", DbType.String, _CheckFeedBackMDL.CheckResult));
            p.Add(db.CreateParameter("ConfirmTime", DbType.DateTime, _CheckFeedBackMDL.ConfirmTime));
            p.Add(db.CreateParameter("ConfirmMan", DbType.String, _CheckFeedBackMDL.ConfirmMan));
            p.Add(db.CreateParameter("ConfirmResult", DbType.String, _CheckFeedBackMDL.ConfirmResult));
            p.Add(db.CreateParameter("SheBaoCheckTime", DbType.DateTime, _CheckFeedBackMDL.SheBaoCheckTime));
            p.Add(db.CreateParameter("SheBaoRtnTime", DbType.DateTime, _CheckFeedBackMDL.SheBaoRtnTime));
            p.Add(db.CreateParameter("sn", DbType.Int32, _CheckFeedBackMDL.sn));
            p.Add(db.CreateParameter("BackReason", DbType.String, _CheckFeedBackMDL.BackReason));
            p.Add(db.CreateParameter("BackUnit", DbType.String, _CheckFeedBackMDL.BackUnit));
            p.Add(db.CreateParameter("PassType", DbType.String, _CheckFeedBackMDL.PassType));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_CheckFeedBackMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(CheckFeedBackMDL _CheckFeedBackMDL)
        {
            return Update(null, _CheckFeedBackMDL);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_CheckFeedBackMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, CheckFeedBackMDL _CheckFeedBackMDL)
        {
            string sql = @"
			UPDATE dbo.CheckFeedBack
				SET	PatchCode = @PatchCode,CheckType = @CheckType,CreateTime = @CreateTime,CJR = @CJR,PublishiTime = @PublishiTime,LastReportTime = @LastReportTime,DataStatus = @DataStatus,DataStatusCode = @DataStatusCode,WorkerName = @WorkerName,WorkerCertificateCode = @WorkerCertificateCode,CertificateCode = @CertificateCode,phone = @phone,PostTypeName = @PostTypeName,Unit = @Unit,UnitCode = @UnitCode,Country = @Country,SheBaoCase = @SheBaoCase,ShebaoUnit = @ShebaoUnit,GongjijinCase = @GongjijinCase,ProjectCase = @ProjectCase,SourceTime = @SourceTime,CaseDesc = @CaseDesc,WorkerRerpotTime = @WorkerRerpotTime,AcceptCountry = @AcceptCountry,AcceptTime = @AcceptTime,AcceptMan = @AcceptMan,AcceptResult = @AcceptResult,CountryReportTime = @CountryReportTime,CountryReportCode = @CountryReportCode,CheckTime = @CheckTime,CheckMan = @CheckMan,CheckResult = @CheckResult,ConfirmTime = @ConfirmTime,ConfirmMan = @ConfirmMan,ConfirmResult = @ConfirmResult,SheBaoCheckTime = @SheBaoCheckTime,SheBaoRtnTime = @SheBaoRtnTime,sn = @sn,BackReason = @BackReason,BackUnit = @BackUnit,PassType = @PassType
			WHERE
				DataID = @DataID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("DataID", DbType.String, _CheckFeedBackMDL.DataID));
            p.Add(db.CreateParameter("PatchCode", DbType.Int32, _CheckFeedBackMDL.PatchCode));
            p.Add(db.CreateParameter("CheckType", DbType.String, _CheckFeedBackMDL.CheckType));
            p.Add(db.CreateParameter("CreateTime", DbType.DateTime, _CheckFeedBackMDL.CreateTime));
            p.Add(db.CreateParameter("CJR", DbType.String, _CheckFeedBackMDL.CJR));
            p.Add(db.CreateParameter("PublishiTime", DbType.DateTime, _CheckFeedBackMDL.PublishiTime));
            p.Add(db.CreateParameter("LastReportTime", DbType.DateTime, _CheckFeedBackMDL.LastReportTime));
            p.Add(db.CreateParameter("DataStatus", DbType.String, _CheckFeedBackMDL.DataStatus));
            p.Add(db.CreateParameter("DataStatusCode", DbType.Int32, _CheckFeedBackMDL.DataStatusCode));
            p.Add(db.CreateParameter("WorkerName", DbType.String, _CheckFeedBackMDL.WorkerName));
            p.Add(db.CreateParameter("WorkerCertificateCode", DbType.String, _CheckFeedBackMDL.WorkerCertificateCode));
            p.Add(db.CreateParameter("CertificateCode", DbType.String, _CheckFeedBackMDL.CertificateCode));
            p.Add(db.CreateParameter("phone", DbType.String, _CheckFeedBackMDL.phone));
            p.Add(db.CreateParameter("PostTypeName", DbType.String, _CheckFeedBackMDL.PostTypeName));
            p.Add(db.CreateParameter("Unit", DbType.String, _CheckFeedBackMDL.Unit));
            p.Add(db.CreateParameter("UnitCode", DbType.String, _CheckFeedBackMDL.UnitCode));
            p.Add(db.CreateParameter("Country", DbType.String, _CheckFeedBackMDL.Country));
            p.Add(db.CreateParameter("SheBaoCase", DbType.String, _CheckFeedBackMDL.SheBaoCase));
            p.Add(db.CreateParameter("ShebaoUnit", DbType.String, _CheckFeedBackMDL.ShebaoUnit));
            p.Add(db.CreateParameter("GongjijinCase", DbType.String, _CheckFeedBackMDL.GongjijinCase));
            p.Add(db.CreateParameter("ProjectCase", DbType.String, _CheckFeedBackMDL.ProjectCase));
            p.Add(db.CreateParameter("SourceTime", DbType.DateTime, _CheckFeedBackMDL.SourceTime));
            p.Add(db.CreateParameter("CaseDesc", DbType.String, _CheckFeedBackMDL.CaseDesc));
            p.Add(db.CreateParameter("WorkerRerpotTime", DbType.DateTime, _CheckFeedBackMDL.WorkerRerpotTime));
            p.Add(db.CreateParameter("AcceptCountry", DbType.String, _CheckFeedBackMDL.AcceptCountry));
            p.Add(db.CreateParameter("AcceptTime", DbType.DateTime, _CheckFeedBackMDL.AcceptTime));
            p.Add(db.CreateParameter("AcceptMan", DbType.String, _CheckFeedBackMDL.AcceptMan));
            p.Add(db.CreateParameter("AcceptResult", DbType.String, _CheckFeedBackMDL.AcceptResult));
            p.Add(db.CreateParameter("CountryReportTime", DbType.DateTime, _CheckFeedBackMDL.CountryReportTime));
            p.Add(db.CreateParameter("CountryReportCode", DbType.String, _CheckFeedBackMDL.CountryReportCode));
            p.Add(db.CreateParameter("CheckTime", DbType.DateTime, _CheckFeedBackMDL.CheckTime));
            p.Add(db.CreateParameter("CheckMan", DbType.String, _CheckFeedBackMDL.CheckMan));
            p.Add(db.CreateParameter("CheckResult", DbType.String, _CheckFeedBackMDL.CheckResult));
            p.Add(db.CreateParameter("ConfirmTime", DbType.DateTime, _CheckFeedBackMDL.ConfirmTime));
            p.Add(db.CreateParameter("ConfirmMan", DbType.String, _CheckFeedBackMDL.ConfirmMan));
            p.Add(db.CreateParameter("ConfirmResult", DbType.String, _CheckFeedBackMDL.ConfirmResult));
            p.Add(db.CreateParameter("SheBaoCheckTime", DbType.DateTime, _CheckFeedBackMDL.SheBaoCheckTime));
            p.Add(db.CreateParameter("SheBaoRtnTime", DbType.DateTime, _CheckFeedBackMDL.SheBaoRtnTime));
            p.Add(db.CreateParameter("sn", DbType.Int32, _CheckFeedBackMDL.sn));
            p.Add(db.CreateParameter("BackReason", DbType.String, _CheckFeedBackMDL.BackReason));
            p.Add(db.CreateParameter("BackUnit", DbType.String, _CheckFeedBackMDL.BackUnit));
            p.Add(db.CreateParameter("PassType", DbType.String, _CheckFeedBackMDL.PassType));
            
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="CheckFeedBackID">主键</param>
        /// <returns></returns>
        public static int Delete(string DataID)
        {
            return Delete(null, DataID);
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="CheckFeedBackID">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, string DataID)
        {
            string sql = @"DELETE FROM dbo.CheckFeedBack WHERE DataID = @DataID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("DataID", DbType.String, DataID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="_CheckFeedBackMDL">对象实体类</param>
        /// <returns></returns>
        public static int Delete(CheckFeedBackMDL _CheckFeedBackMDL)
        {
            return Delete(null, _CheckFeedBackMDL);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_CheckFeedBackMDL">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, CheckFeedBackMDL _CheckFeedBackMDL)
        {
            string sql = @"DELETE FROM dbo.CheckFeedBack WHERE DataID = @DataID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("DataID", DbType.String, _CheckFeedBackMDL.DataID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="CheckFeedBackID">主键</param>
        public static CheckFeedBackMDL GetObject(string DataID)
        {
            string sql = @"
			SELECT DataID,PatchCode,CheckType,CreateTime,CJR,PublishiTime,LastReportTime,DataStatus,DataStatusCode,WorkerName,WorkerCertificateCode,CertificateCode,phone,PostTypeName,Unit,UnitCode,Country,SheBaoCase,ShebaoUnit,GongjijinCase,ProjectCase,SourceTime,CaseDesc,WorkerRerpotTime,AcceptCountry,AcceptTime,AcceptMan,AcceptResult,CountryReportTime,CountryReportCode,CheckTime,CheckMan,CheckResult,ConfirmTime,ConfirmMan,ConfirmResult,SheBaoCheckTime,SheBaoRtnTime,sn,BackReason,BackUnit,PassType
			FROM dbo.CheckFeedBack
			WHERE DataID = @DataID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("DataID", DbType.String, DataID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                CheckFeedBackMDL _CheckFeedBackMDL = null;
                if (reader.Read())
                {
                    _CheckFeedBackMDL = new CheckFeedBackMDL();
                    if (reader["DataID"] != DBNull.Value) _CheckFeedBackMDL.DataID = Convert.ToString(reader["DataID"]);
                    if (reader["PatchCode"] != DBNull.Value) _CheckFeedBackMDL.PatchCode = Convert.ToInt32(reader["PatchCode"]);
                    if (reader["CheckType"] != DBNull.Value) _CheckFeedBackMDL.CheckType = Convert.ToString(reader["CheckType"]);
                    if (reader["CreateTime"] != DBNull.Value) _CheckFeedBackMDL.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                    if (reader["CJR"] != DBNull.Value) _CheckFeedBackMDL.CJR = Convert.ToString(reader["CJR"]);
                    if (reader["PublishiTime"] != DBNull.Value) _CheckFeedBackMDL.PublishiTime = Convert.ToDateTime(reader["PublishiTime"]);
                    if (reader["LastReportTime"] != DBNull.Value) _CheckFeedBackMDL.LastReportTime = Convert.ToDateTime(reader["LastReportTime"]);
                    if (reader["DataStatus"] != DBNull.Value) _CheckFeedBackMDL.DataStatus = Convert.ToString(reader["DataStatus"]);
                    if (reader["DataStatusCode"] != DBNull.Value) _CheckFeedBackMDL.DataStatusCode = Convert.ToInt32(reader["DataStatusCode"]);
                    if (reader["WorkerName"] != DBNull.Value) _CheckFeedBackMDL.WorkerName = Convert.ToString(reader["WorkerName"]);
                    if (reader["WorkerCertificateCode"] != DBNull.Value) _CheckFeedBackMDL.WorkerCertificateCode = Convert.ToString(reader["WorkerCertificateCode"]);
                    if (reader["CertificateCode"] != DBNull.Value) _CheckFeedBackMDL.CertificateCode = Convert.ToString(reader["CertificateCode"]);
                    if (reader["phone"] != DBNull.Value) _CheckFeedBackMDL.phone = Convert.ToString(reader["phone"]);
                    if (reader["PostTypeName"] != DBNull.Value) _CheckFeedBackMDL.PostTypeName = Convert.ToString(reader["PostTypeName"]);
                    if (reader["Unit"] != DBNull.Value) _CheckFeedBackMDL.Unit = Convert.ToString(reader["Unit"]);
                    if (reader["UnitCode"] != DBNull.Value) _CheckFeedBackMDL.UnitCode = Convert.ToString(reader["UnitCode"]);
                    if (reader["Country"] != DBNull.Value) _CheckFeedBackMDL.Country = Convert.ToString(reader["Country"]);
                    if (reader["SheBaoCase"] != DBNull.Value) _CheckFeedBackMDL.SheBaoCase = Convert.ToString(reader["SheBaoCase"]);
                    if (reader["ShebaoUnit"] != DBNull.Value) _CheckFeedBackMDL.ShebaoUnit = Convert.ToString(reader["ShebaoUnit"]);
                    if (reader["GongjijinCase"] != DBNull.Value) _CheckFeedBackMDL.GongjijinCase = Convert.ToString(reader["GongjijinCase"]);
                    if (reader["ProjectCase"] != DBNull.Value) _CheckFeedBackMDL.ProjectCase = Convert.ToString(reader["ProjectCase"]);
                    if (reader["SourceTime"] != DBNull.Value) _CheckFeedBackMDL.SourceTime = Convert.ToDateTime(reader["SourceTime"]);
                    if (reader["CaseDesc"] != DBNull.Value) _CheckFeedBackMDL.CaseDesc = Convert.ToString(reader["CaseDesc"]);
                    if (reader["WorkerRerpotTime"] != DBNull.Value) _CheckFeedBackMDL.WorkerRerpotTime = Convert.ToDateTime(reader["WorkerRerpotTime"]);
                    if (reader["AcceptCountry"] != DBNull.Value) _CheckFeedBackMDL.AcceptCountry = Convert.ToString(reader["AcceptCountry"]);
                    if (reader["AcceptTime"] != DBNull.Value) _CheckFeedBackMDL.AcceptTime = Convert.ToDateTime(reader["AcceptTime"]);
                    if (reader["AcceptMan"] != DBNull.Value) _CheckFeedBackMDL.AcceptMan = Convert.ToString(reader["AcceptMan"]);
                    if (reader["AcceptResult"] != DBNull.Value) _CheckFeedBackMDL.AcceptResult = Convert.ToString(reader["AcceptResult"]);
                    if (reader["CountryReportTime"] != DBNull.Value) _CheckFeedBackMDL.CountryReportTime = Convert.ToDateTime(reader["CountryReportTime"]);
                    if (reader["CountryReportCode"] != DBNull.Value) _CheckFeedBackMDL.CountryReportCode = Convert.ToString(reader["CountryReportCode"]);
                    if (reader["CheckTime"] != DBNull.Value) _CheckFeedBackMDL.CheckTime = Convert.ToDateTime(reader["CheckTime"]);
                    if (reader["CheckMan"] != DBNull.Value) _CheckFeedBackMDL.CheckMan = Convert.ToString(reader["CheckMan"]);
                    if (reader["CheckResult"] != DBNull.Value) _CheckFeedBackMDL.CheckResult = Convert.ToString(reader["CheckResult"]);
                    if (reader["ConfirmTime"] != DBNull.Value) _CheckFeedBackMDL.ConfirmTime = Convert.ToDateTime(reader["ConfirmTime"]);
                    if (reader["ConfirmMan"] != DBNull.Value) _CheckFeedBackMDL.ConfirmMan = Convert.ToString(reader["ConfirmMan"]);
                    if (reader["ConfirmResult"] != DBNull.Value) _CheckFeedBackMDL.ConfirmResult = Convert.ToString(reader["ConfirmResult"]);
                    if (reader["SheBaoCheckTime"] != DBNull.Value) _CheckFeedBackMDL.SheBaoCheckTime = Convert.ToDateTime(reader["SheBaoCheckTime"]);
                    if (reader["SheBaoRtnTime"] != DBNull.Value) _CheckFeedBackMDL.SheBaoRtnTime = Convert.ToDateTime(reader["SheBaoRtnTime"]);
                    if (reader["sn"] != DBNull.Value) _CheckFeedBackMDL.sn = Convert.ToInt32(reader["sn"]);
                    if (reader["BackReason"] != DBNull.Value) _CheckFeedBackMDL.BackReason = Convert.ToString(reader["BackReason"]);
                    if (reader["BackUnit"] != DBNull.Value) _CheckFeedBackMDL.BackUnit = Convert.ToString(reader["BackUnit"]);
                    if (reader["PassType"] != DBNull.Value) _CheckFeedBackMDL.PassType = Convert.ToString(reader["PassType"]);
                    
                }
                reader.Close();
                db.Close();
                return _CheckFeedBackMDL;
            }
        }
        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        public static DataTable GetList(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.CheckFeedBack", "*", filterWhereString, orderBy == "" ? " DataID" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.CheckFeedBack", filterWhereString);
        }

        #region 自定义方法

        /// <summary>
        /// 根据监管主表更新明细信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_CheckTaskMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, CheckTaskMDL _CheckTaskMDL)
        {
            string sql = @"
			UPDATE dbo.CheckFeedBack
				SET	CheckType = @CheckType,PublishiTime = @PublishiTime,LastReportTime = @LastReportTime,DataStatus = @DataStatus,DataStatusCode = @DataStatusCode
			WHERE
				PatchCode = @PatchCode";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PatchCode", DbType.Int32, _CheckTaskMDL.PatchCode));
            p.Add(db.CreateParameter("CheckType", DbType.String, _CheckTaskMDL.CheckType));
            p.Add(db.CreateParameter("PublishiTime", DbType.DateTime, _CheckTaskMDL.PublishiTime));
            p.Add(db.CreateParameter("LastReportTime", DbType.DateTime, _CheckTaskMDL.LastReportTime));
            p.Add(db.CreateParameter("DataStatus", DbType.String, _CheckTaskMDL.PublishiTime.HasValue == true ? EnumManager.CheckFeedStatus.待反馈 : EnumManager.CheckFeedStatus.未发布));
            p.Add(db.CreateParameter("DataStatusCode", DbType.Int32, _CheckTaskMDL.PublishiTime.HasValue == true ? EnumManager.CheckFeedStatusCode.待反馈 : EnumManager.CheckFeedStatusCode.未发布));

            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }



        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        public static DataTable GetListView(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.View_CheckFeedBack", "*", filterWhereString, orderBy == "" ? " PatchCode desc" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountView(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.View_CheckFeedBack", filterWhereString);
        }

        /// <summary>
        /// 监管审批记录集合（用于管理端）。
        /// </summary>   
        /// <param name="ApplyID">申请ID</param>
        /// <returns>审批历史记录集合</returns>
        public static DataTable GetCheckHistoryList(string ApplyID)
        {
            string sql = @"
               select row_number() over(order by ActionData ) as RowNo ,t.* from 
                (
                select  '个人提交' as 'Action',[WorkerRerpotTime] as ActionData,'提交审核' as ActionResult,'已填写反馈信息' as ActionRemark,WorkerName as ActionMan FROM [dbo].[CheckFeedBack] where [DataID]='{0}' and [WorkerRerpotTime] is not null 
                 union all
                  select  '区级审查',[AcceptTime],[AcceptResult],(case when [AcceptResult]='通过' then '允许通过' else [BackReason] end),[AcceptMan] FROM [dbo].[CheckFeedBack] where [DataID]='{0}' and [AcceptTime] is not null
                 union all
                  select  '市级审查',[CheckTime],[CheckResult],(case when [CheckResult]='通过' then '允许通过' else [BackReason] end),[CheckMan] FROM [dbo].[CheckFeedBack] where [DataID]='{0}' and [CheckTime] is not null 
                 union all
                  select  '市级决定',[ConfirmTime],[ConfirmResult],(case when [ConfirmResult]='通过' then '通过(办结)' else [BackReason] end),[ConfirmMan] FROM [dbo].[CheckFeedBack] where [DataID]='{0}' and [ConfirmTime] is not null 
                ) t";
            return CommonDAL.GetDataTable(string.Format(sql, ApplyID));
        }

        /// <summary>
        /// 监管审批记录集合（用于个人和企业）。
        /// </summary>   
        /// <param name="ApplyID">申请ID</param>
        /// <returns>审批历史记录集合</returns>
        public static DataTable GetCheckHistoryListOfWorker(string ApplyID)
        {
            string sql = @"
               select row_number() over(order by ActionData ) as RowNo ,t.* from 
                (
                select  '个人提交' as 'Action',[WorkerRerpotTime] as ActionData,'提交审核' as ActionResult,'已填写反馈信息' as ActionRemark,WorkerName as ActionMan FROM [dbo].[CheckFeedBack] where [DataID]='{0}' and [WorkerRerpotTime] is not null 
                 union all
                  select  '区级审查',[AcceptTime],[AcceptResult],(case when [AcceptResult]='通过' then '允许通过' else [BackReason] end),[AcceptMan] FROM [dbo].[CheckFeedBack] where [DataID]='{0}' and [AcceptTime] is not null
                 union all
                  select  '市级审查',[CheckTime],[CheckResult],(case when [CheckResult]='通过' then '允许通过' else [BackReason] end),[CheckMan] FROM [dbo].[CheckFeedBack] where [DataID]='{0}' and [CheckTime] is not null and ([CheckResult]='通过' or ([ConfirmTime] is null and [DataStatusCode]=2))
                 union all
                  select  '市级决定',[ConfirmTime],[ConfirmResult],(case when [ConfirmResult]='通过' then '通过(办结)' else [BackReason] end),[ConfirmMan] FROM [dbo].[CheckFeedBack] where [DataID]='{0}' and [ConfirmTime] is not null 
                ) t";
            return CommonDAL.GetDataTable(string.Format(sql, ApplyID));
        }

        /// <summary>
        /// 批量决定
        /// </summary>
        /// <param name="QuerySql">过滤条件</param>
        /// <param name="ConfirmMan">决定人</param>
        /// <param name="ConfirmResult">决定结果</param>
        /// <param name="BackReason">驳回原因</param>
        /// <returns></returns>
        public static int Confirm(string QuerySql,string ConfirmMan, string ConfirmResult,string BackReason)
        {
            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();

            string sql = string.Format(@"UPDATE dbo.CheckFeedBack
				                SET	DataStatus = @DataStatus,DataStatusCode = @DataStatusCode,ConfirmTime = getdate(),ConfirmMan = @ConfirmMan,ConfirmResult = @ConfirmResult,PassType = case when [CertCancelCheckResult]=1 then '已注销完成整改' else PassType end
			                WHERE
				                1=1 {0}", QuerySql);

            if (ConfirmResult !="通过")
            {
                sql = string.Format(@"UPDATE dbo.CheckFeedBack
				        SET	DataStatus = @DataStatus,DataStatusCode = @DataStatusCode,ConfirmTime = getdate(),ConfirmMan = @ConfirmMan,ConfirmResult = @ConfirmResult,BackUnit = @BackUnit,
                            BackReason = case when @BackReason ='决定未通过' and CheckResult = '不通过' then BackReason else @BackReason end
			            WHERE
				            1=1 {0}", QuerySql);

                p.Add(db.CreateParameter("ConfirmMan", DbType.String, ConfirmMan));
                p.Add(db.CreateParameter("ConfirmResult", DbType.String, ConfirmResult));
                p.Add(db.CreateParameter("BackReason", DbType.String, BackReason));
                p.Add(db.CreateParameter("BackUnit", DbType.String, "市建委"));
                p.Add(db.CreateParameter("DataStatus", DbType.String, EnumManager.CheckFeedStatus.已驳回));
                p.Add(db.CreateParameter("DataStatusCode", DbType.Int32, EnumManager.CheckFeedStatusCode.已驳回));
            }
            else
            {
                p.Add(db.CreateParameter("ConfirmMan", DbType.String, ConfirmMan));
                p.Add(db.CreateParameter("ConfirmResult", DbType.String, ConfirmResult));
                p.Add(db.CreateParameter("DataStatus", DbType.String, EnumManager.CheckFeedStatus.已决定));
                p.Add(db.CreateParameter("DataStatusCode", DbType.Int32, EnumManager.CheckFeedStatusCode.已决定));
            }       
            return db.GetExcuteNonQuery(sql, p.ToArray());
        }
        #endregion
    }
}
