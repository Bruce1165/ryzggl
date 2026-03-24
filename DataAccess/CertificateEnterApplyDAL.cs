using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
    /// <summary>
    /// 业务类实现--CertificateEnterApplyDAL(填写类描述)
    /// </summary>
    public class CertificateEnterApplyDAL
    {
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="certificateEnterApplyOb">对象实体类</param>
        /// <returns></returns>
        public static long Insert(CertificateEnterApplyOB certificateEnterApplyOb)
        {
            return Insert(null, certificateEnterApplyOb);
        }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="certificateEnterApplyOb">对象实体类</param>
        /// <returns></returns>
        public static long Insert(DbTransaction tran, CertificateEnterApplyOB certificateEnterApplyOb)
        {
            var db = new DBHelper();

            const string sql = @"
			INSERT INTO dbo.CertificateEnterApply(WorkerID,PostTypeID,PostID,WorkerName,Sex,Birthday,WorkerCertificateCode,OldUnitName,UnitName,UnitCode,Phone,CertificateCode,ConferDate,ValidStartDate,ValidEndDate,ConferUnit,ApplyStatus,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,ApplyDate,ApplyMan,ApplyCode,AcceptDate,GetResult,GetMan,GetCode,CheckDate,CheckResult,CheckMan,CheckCode,ConfrimDate,ConfrimResult,ConfrimMan,ConfrimCode,CertificateID,AddPostID,CreditCode,NewUnitCheckTime,NewUnitAdvise,ZACheckTime,ZACheckResult,ZACheckRemark,Job,SkillLevel)
			VALUES (@WorkerID,@PostTypeID,@PostID,@WorkerName,@Sex,@Birthday,@WorkerCertificateCode,@OldUnitName,@UnitName,@UnitCode,@Phone,@CertificateCode,@ConferDate,@ValidStartDate,@ValidEndDate,@ConferUnit,@ApplyStatus,@CreatePersonID,@CreateTime,@ModifyPersonID,@ModifyTime,@ApplyDate,@ApplyMan,@ApplyCode,@AcceptDate,@GetResult,@GetMan,@GetCode,@CheckDate,@CheckResult,@CheckMan,@CheckCode,@ConfrimDate,@ConfrimResult,@ConfrimMan,@ConfrimCode,@CertificateID,@AddPostID,@CreditCode,@NewUnitCheckTime,@NewUnitAdvise,@ZACheckTime,@ZACheckResult,@ZACheckRemark,@Job,@SkillLevel);SELECT @ApplyID = @@IDENTITY";

            var p = new List<SqlParameter>
            {
                db.CreateOutParameter("ApplyID", DbType.Int64),
                db.CreateParameter("WorkerID", DbType.Int64, certificateEnterApplyOb.WorkerID),
                db.CreateParameter("PostTypeID", DbType.Int32, certificateEnterApplyOb.PostTypeID),
                db.CreateParameter("PostID", DbType.Int32, certificateEnterApplyOb.PostID),
                db.CreateParameter("WorkerName", DbType.String, certificateEnterApplyOb.WorkerName),
                db.CreateParameter("Sex", DbType.String, certificateEnterApplyOb.Sex),
                db.CreateParameter("Birthday", DbType.DateTime, certificateEnterApplyOb.Birthday),
                db.CreateParameter("WorkerCertificateCode", DbType.String,certificateEnterApplyOb.WorkerCertificateCode),
                db.CreateParameter("OldUnitName", DbType.String, certificateEnterApplyOb.OldUnitName),
                db.CreateParameter("UnitName", DbType.String, certificateEnterApplyOb.UnitName),
                db.CreateParameter("UnitCode", DbType.String, certificateEnterApplyOb.UnitCode),
                db.CreateParameter("Phone", DbType.String, certificateEnterApplyOb.Phone),
                db.CreateParameter("CertificateCode", DbType.String, certificateEnterApplyOb.CertificateCode),
                db.CreateParameter("ConferDate", DbType.DateTime, certificateEnterApplyOb.ConferDate),
                db.CreateParameter("ValidStartDate", DbType.DateTime, certificateEnterApplyOb.ValidStartDate),
                db.CreateParameter("ValidEndDate", DbType.DateTime, certificateEnterApplyOb.ValidEndDate),
                db.CreateParameter("ConferUnit", DbType.String, certificateEnterApplyOb.ConferUnit),
                db.CreateParameter("ApplyStatus", DbType.String, certificateEnterApplyOb.ApplyStatus),
                db.CreateParameter("CreatePersonID", DbType.Int64, certificateEnterApplyOb.CreatePersonID),
                db.CreateParameter("CreateTime", DbType.DateTime, certificateEnterApplyOb.CreateTime),
                db.CreateParameter("ModifyPersonID", DbType.Int64, certificateEnterApplyOb.ModifyPersonID),
                db.CreateParameter("ModifyTime", DbType.DateTime, certificateEnterApplyOb.ModifyTime),
                db.CreateParameter("ApplyDate", DbType.DateTime, certificateEnterApplyOb.ApplyDate),
                db.CreateParameter("ApplyMan", DbType.String, certificateEnterApplyOb.ApplyMan),
                db.CreateParameter("ApplyCode", DbType.String, certificateEnterApplyOb.ApplyCode),
                db.CreateParameter("AcceptDate", DbType.DateTime, certificateEnterApplyOb.AcceptDate),
                db.CreateParameter("GetResult", DbType.String, certificateEnterApplyOb.GetResult),
                db.CreateParameter("GetMan", DbType.String, certificateEnterApplyOb.GetMan),
                db.CreateParameter("GetCode", DbType.String, certificateEnterApplyOb.GetCode),
                db.CreateParameter("CheckDate", DbType.DateTime, certificateEnterApplyOb.CheckDate),
                db.CreateParameter("CheckResult", DbType.String, certificateEnterApplyOb.CheckResult),
                db.CreateParameter("CheckMan", DbType.String, certificateEnterApplyOb.CheckMan),
                db.CreateParameter("CheckCode", DbType.String, certificateEnterApplyOb.CheckCode),
                db.CreateParameter("ConfrimDate", DbType.DateTime, certificateEnterApplyOb.ConfrimDate),
                db.CreateParameter("ConfrimResult", DbType.String, certificateEnterApplyOb.ConfrimResult),
                db.CreateParameter("ConfrimMan", DbType.String, certificateEnterApplyOb.ConfrimMan),
                db.CreateParameter("ConfrimCode", DbType.String, certificateEnterApplyOb.ConfrimCode),
                db.CreateParameter("CertificateID", DbType.Int64, certificateEnterApplyOb.CertificateID),
                db.CreateParameter("AddPostID", DbType.String, certificateEnterApplyOb.AddPostID),
                db.CreateParameter("CreditCode", DbType.String, certificateEnterApplyOb.CreditCode),
                db.CreateParameter("NewUnitCheckTime", DbType.DateTime, certificateEnterApplyOb.NewUnitCheckTime),
                db.CreateParameter("NewUnitAdvise", DbType.String, certificateEnterApplyOb.NewUnitAdvise),
                db.CreateParameter("ZACheckTime",DbType.DateTime, certificateEnterApplyOb.ZACheckTime),
			    db.CreateParameter("ZACheckResult",DbType.Int32, certificateEnterApplyOb.ZACheckResult),
			    db.CreateParameter("ZACheckRemark",DbType.String, certificateEnterApplyOb.ZACheckRemark),
			    db.CreateParameter("Job",DbType.String, certificateEnterApplyOb.Job),
			    db.CreateParameter("SkillLevel",DbType.String, certificateEnterApplyOb.SkillLevel)
            };
            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            certificateEnterApplyOb.ApplyID = Convert.ToInt64(p[0].Value);
            return rtn;
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="certificateEnterApplyOb">对象实体类</param>
        /// <returns></returns>
        public static int Update(CertificateEnterApplyOB certificateEnterApplyOb)
        {
            return Update(null, certificateEnterApplyOb);
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="certificateEnterApplyOb">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, CertificateEnterApplyOB certificateEnterApplyOb)
        {
            const string sql = @"
			UPDATE dbo.CertificateEnterApply
				SET	WorkerID = @WorkerID,PostTypeID = @PostTypeID,PostID = @PostID,WorkerName = @WorkerName,Sex = @Sex,Birthday = @Birthday,WorkerCertificateCode = @WorkerCertificateCode,OldUnitName = @OldUnitName,UnitName = @UnitName,UnitCode = @UnitCode,Phone = @Phone,CertificateCode = @CertificateCode,ConferDate = @ConferDate,ValidStartDate = @ValidStartDate,ValidEndDate = @ValidEndDate,ConferUnit = @ConferUnit,ApplyStatus = @ApplyStatus,CreatePersonID = @CreatePersonID,CreateTime = @CreateTime,ModifyPersonID = @ModifyPersonID,ModifyTime = @ModifyTime,ApplyDate = @ApplyDate,ApplyMan = @ApplyMan,ApplyCode = @ApplyCode,AcceptDate = @AcceptDate,GetResult = @GetResult,GetMan = @GetMan,GetCode = @GetCode,CheckDate = @CheckDate,CheckResult = @CheckResult,CheckMan = @CheckMan,CheckCode = @CheckCode,ConfrimDate = @ConfrimDate,ConfrimResult = @ConfrimResult,ConfrimMan = @ConfrimMan,ConfrimCode = @ConfrimCode,CertificateID = @CertificateID,AddPostID = @AddPostID,CreditCode =@CreditCode,NewUnitCheckTime = @NewUnitCheckTime,NewUnitAdvise = @NewUnitAdvise,ZACheckTime = @ZACheckTime,ZACheckResult = @ZACheckResult,ZACheckRemark = @ZACheckRemark,Job = @Job,SkillLevel = @SkillLevel
			WHERE
				ApplyID = @ApplyID";

            var db = new DBHelper();
            var p = new List<SqlParameter>
            {
                db.CreateParameter("ApplyID", DbType.Int64, certificateEnterApplyOb.ApplyID),
                db.CreateParameter("WorkerID", DbType.Int64, certificateEnterApplyOb.WorkerID),
                db.CreateParameter("PostTypeID", DbType.Int32, certificateEnterApplyOb.PostTypeID),
                db.CreateParameter("PostID", DbType.Int32, certificateEnterApplyOb.PostID),
                db.CreateParameter("WorkerName", DbType.String, certificateEnterApplyOb.WorkerName),
                db.CreateParameter("Sex", DbType.String, certificateEnterApplyOb.Sex),
                db.CreateParameter("Birthday", DbType.DateTime, certificateEnterApplyOb.Birthday),
                db.CreateParameter("WorkerCertificateCode", DbType.String,certificateEnterApplyOb.WorkerCertificateCode),
                db.CreateParameter("OldUnitName", DbType.String, certificateEnterApplyOb.OldUnitName),
                db.CreateParameter("UnitName", DbType.String, certificateEnterApplyOb.UnitName),
                db.CreateParameter("UnitCode", DbType.String, certificateEnterApplyOb.UnitCode),
                db.CreateParameter("Phone", DbType.String, certificateEnterApplyOb.Phone),
                db.CreateParameter("CertificateCode", DbType.String, certificateEnterApplyOb.CertificateCode),
                db.CreateParameter("ConferDate", DbType.DateTime, certificateEnterApplyOb.ConferDate),
                db.CreateParameter("ValidStartDate", DbType.DateTime, certificateEnterApplyOb.ValidStartDate),
                db.CreateParameter("ValidEndDate", DbType.DateTime, certificateEnterApplyOb.ValidEndDate),
                db.CreateParameter("ConferUnit", DbType.String, certificateEnterApplyOb.ConferUnit),
                db.CreateParameter("ApplyStatus", DbType.String, certificateEnterApplyOb.ApplyStatus),
                db.CreateParameter("CreatePersonID", DbType.Int64, certificateEnterApplyOb.CreatePersonID),
                db.CreateParameter("CreateTime", DbType.DateTime, certificateEnterApplyOb.CreateTime),
                db.CreateParameter("ModifyPersonID", DbType.Int64, certificateEnterApplyOb.ModifyPersonID),
                db.CreateParameter("ModifyTime", DbType.DateTime, certificateEnterApplyOb.ModifyTime),
                db.CreateParameter("ApplyDate", DbType.DateTime, certificateEnterApplyOb.ApplyDate),
                db.CreateParameter("ApplyMan", DbType.String, certificateEnterApplyOb.ApplyMan),
                db.CreateParameter("ApplyCode", DbType.String, certificateEnterApplyOb.ApplyCode),
                db.CreateParameter("AcceptDate", DbType.DateTime, certificateEnterApplyOb.AcceptDate),
                db.CreateParameter("GetResult", DbType.String, certificateEnterApplyOb.GetResult),
                db.CreateParameter("GetMan", DbType.String, certificateEnterApplyOb.GetMan),
                db.CreateParameter("GetCode", DbType.String, certificateEnterApplyOb.GetCode),
                db.CreateParameter("CheckDate", DbType.DateTime, certificateEnterApplyOb.CheckDate),
                db.CreateParameter("CheckResult", DbType.String, certificateEnterApplyOb.CheckResult),
                db.CreateParameter("CheckMan", DbType.String, certificateEnterApplyOb.CheckMan),
                db.CreateParameter("CheckCode", DbType.String, certificateEnterApplyOb.CheckCode),
                db.CreateParameter("ConfrimDate", DbType.DateTime, certificateEnterApplyOb.ConfrimDate),
                db.CreateParameter("ConfrimResult", DbType.String, certificateEnterApplyOb.ConfrimResult),
                db.CreateParameter("ConfrimMan", DbType.String, certificateEnterApplyOb.ConfrimMan),
                db.CreateParameter("ConfrimCode", DbType.String, certificateEnterApplyOb.ConfrimCode),
                db.CreateParameter("CertificateID", DbType.Int64, certificateEnterApplyOb.CertificateID),
                db.CreateParameter("AddPostID", DbType.String, certificateEnterApplyOb.AddPostID),
                db.CreateParameter("CreditCode", DbType.String, certificateEnterApplyOb.CreditCode),
                db.CreateParameter("NewUnitCheckTime", DbType.DateTime, certificateEnterApplyOb.NewUnitCheckTime),
                db.CreateParameter("NewUnitAdvise", DbType.String, certificateEnterApplyOb.NewUnitAdvise),
                db.CreateParameter("ZACheckTime",DbType.DateTime, certificateEnterApplyOb.ZACheckTime),
			    db.CreateParameter("ZACheckResult",DbType.Int32, certificateEnterApplyOb.ZACheckResult),
			    db.CreateParameter("ZACheckRemark",DbType.String, certificateEnterApplyOb.ZACheckRemark),
			    db.CreateParameter("Job",DbType.String, certificateEnterApplyOb.Job),
			    db.CreateParameter("SkillLevel",DbType.String, certificateEnterApplyOb.SkillLevel)                
            };
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="applyId">主键</param>
        /// <returns></returns>
        public static int Delete(long applyId)
        {
            return Delete(null, applyId);
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="applyId">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, long applyId)
        {
            const string sql = @"DELETE FROM dbo.CertificateEnterApply WHERE ApplyID = @ApplyID";

            var db = new DBHelper();
            var p = new List<SqlParameter>
            {
                db.CreateParameter("ApplyID", DbType.Int64, applyId)
            };
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="certificateEnterApplyOb">对象实体类</param>
        /// <returns></returns>
        public static int Delete(CertificateEnterApplyOB certificateEnterApplyOb)
        {
            return Delete(null, certificateEnterApplyOb);
        }

        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="certificateEnterApplyOb">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, CertificateEnterApplyOB certificateEnterApplyOb)
        {
            const string sql = @"DELETE FROM dbo.CertificateEnterApply WHERE ApplyID = @ApplyID";

            var db = new DBHelper();
            var p = new List<SqlParameter>
            {
                db.CreateParameter("ApplyID", DbType.Int64, certificateEnterApplyOb.ApplyID)
            };
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="applyId">主键</param>
        public static CertificateEnterApplyOB GetObject(long applyId)
        {
            const string sql = @"
			SELECT ApplyID,WorkerID,PostTypeID,PostID,WorkerName,Sex,Birthday,WorkerCertificateCode,OldUnitName,UnitName,UnitCode,Phone,CertificateCode,ConferDate,ValidStartDate,ValidEndDate,ConferUnit,ApplyStatus,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,ApplyDate,ApplyMan,ApplyCode,AcceptDate,GetResult,GetMan,GetCode,CheckDate,CheckResult,CheckMan,CheckCode,ConfrimDate,ConfrimResult,ConfrimMan,ConfrimCode,CertificateID,AddPostID,CreditCode,NewUnitCheckTime,NewUnitAdvise,[SHEBAOCHECK],ZACheckTime,ZACheckResult,ZACheckRemark,Job,SkillLevel
			FROM dbo.CertificateEnterApply
			WHERE ApplyID = @ApplyID";

            var db = new DBHelper();
            var p = new List<SqlParameter>
            {
                db.CreateParameter("ApplyID", DbType.Int64, applyId)
            };
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    CertificateEnterApplyOB certificateEnterApplyOb = null;
                    if (reader.Read())
                    {
                        certificateEnterApplyOb = new CertificateEnterApplyOB();
                        if (reader["ApplyID"] != DBNull.Value) certificateEnterApplyOb.ApplyID = Convert.ToInt64(reader["ApplyID"]);
                        if (reader["WorkerID"] != DBNull.Value) certificateEnterApplyOb.WorkerID = Convert.ToInt64(reader["WorkerID"]);
                        if (reader["PostTypeID"] != DBNull.Value) certificateEnterApplyOb.PostTypeID = Convert.ToInt32(reader["PostTypeID"]);
                        if (reader["PostID"] != DBNull.Value) certificateEnterApplyOb.PostID = Convert.ToInt32(reader["PostID"]);
                        if (reader["WorkerName"] != DBNull.Value) certificateEnterApplyOb.WorkerName = Convert.ToString(reader["WorkerName"]);
                        if (reader["Sex"] != DBNull.Value) certificateEnterApplyOb.Sex = Convert.ToString(reader["Sex"]);
                        if (reader["Birthday"] != DBNull.Value) certificateEnterApplyOb.Birthday = Convert.ToDateTime(reader["Birthday"]);
                        if (reader["WorkerCertificateCode"] != DBNull.Value) certificateEnterApplyOb.WorkerCertificateCode = Convert.ToString(reader["WorkerCertificateCode"]);
                        if (reader["OldUnitName"] != DBNull.Value) certificateEnterApplyOb.OldUnitName = Convert.ToString(reader["OldUnitName"]);
                        if (reader["UnitName"] != DBNull.Value) certificateEnterApplyOb.UnitName = Convert.ToString(reader["UnitName"]);
                        if (reader["UnitCode"] != DBNull.Value) certificateEnterApplyOb.UnitCode = Convert.ToString(reader["UnitCode"]);
                        if (reader["Phone"] != DBNull.Value) certificateEnterApplyOb.Phone = Convert.ToString(reader["Phone"]);
                        if (reader["CertificateCode"] != DBNull.Value) certificateEnterApplyOb.CertificateCode = Convert.ToString(reader["CertificateCode"]);
                        if (reader["ConferDate"] != DBNull.Value) certificateEnterApplyOb.ConferDate = Convert.ToDateTime(reader["ConferDate"]);
                        if (reader["ValidStartDate"] != DBNull.Value) certificateEnterApplyOb.ValidStartDate = Convert.ToDateTime(reader["ValidStartDate"]);
                        if (reader["ValidEndDate"] != DBNull.Value) certificateEnterApplyOb.ValidEndDate = Convert.ToDateTime(reader["ValidEndDate"]);
                        if (reader["ConferUnit"] != DBNull.Value) certificateEnterApplyOb.ConferUnit = Convert.ToString(reader["ConferUnit"]);
                        if (reader["ApplyStatus"] != DBNull.Value) certificateEnterApplyOb.ApplyStatus = Convert.ToString(reader["ApplyStatus"]);
                        if (reader["CreatePersonID"] != DBNull.Value) certificateEnterApplyOb.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                        if (reader["CreateTime"] != DBNull.Value) certificateEnterApplyOb.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                        if (reader["ModifyPersonID"] != DBNull.Value) certificateEnterApplyOb.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                        if (reader["ModifyTime"] != DBNull.Value) certificateEnterApplyOb.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                        if (reader["ApplyDate"] != DBNull.Value) certificateEnterApplyOb.ApplyDate = Convert.ToDateTime(reader["ApplyDate"]);
                        if (reader["ApplyMan"] != DBNull.Value) certificateEnterApplyOb.ApplyMan = Convert.ToString(reader["ApplyMan"]);
                        if (reader["ApplyCode"] != DBNull.Value) certificateEnterApplyOb.ApplyCode = Convert.ToString(reader["ApplyCode"]);
                        if (reader["AcceptDate"] != DBNull.Value) certificateEnterApplyOb.AcceptDate = Convert.ToDateTime(reader["AcceptDate"]);
                        if (reader["GetResult"] != DBNull.Value) certificateEnterApplyOb.GetResult = Convert.ToString(reader["GetResult"]);
                        if (reader["GetMan"] != DBNull.Value) certificateEnterApplyOb.GetMan = Convert.ToString(reader["GetMan"]);
                        if (reader["GetCode"] != DBNull.Value) certificateEnterApplyOb.GetCode = Convert.ToString(reader["GetCode"]);
                        if (reader["CheckDate"] != DBNull.Value) certificateEnterApplyOb.CheckDate = Convert.ToDateTime(reader["CheckDate"]);
                        if (reader["CheckResult"] != DBNull.Value) certificateEnterApplyOb.CheckResult = Convert.ToString(reader["CheckResult"]);
                        if (reader["CheckMan"] != DBNull.Value) certificateEnterApplyOb.CheckMan = Convert.ToString(reader["CheckMan"]);
                        if (reader["CheckCode"] != DBNull.Value) certificateEnterApplyOb.CheckCode = Convert.ToString(reader["CheckCode"]);
                        if (reader["ConfrimDate"] != DBNull.Value) certificateEnterApplyOb.ConfrimDate = Convert.ToDateTime(reader["ConfrimDate"]);
                        if (reader["ConfrimResult"] != DBNull.Value) certificateEnterApplyOb.ConfrimResult = Convert.ToString(reader["ConfrimResult"]);
                        if (reader["ConfrimMan"] != DBNull.Value) certificateEnterApplyOb.ConfrimMan = Convert.ToString(reader["ConfrimMan"]);
                        if (reader["ConfrimCode"] != DBNull.Value) certificateEnterApplyOb.ConfrimCode = Convert.ToString(reader["ConfrimCode"]);
                        if (reader["CertificateID"] != DBNull.Value) certificateEnterApplyOb.CertificateID = Convert.ToInt64(reader["CertificateID"]);
                        if (reader["AddPostID"] != DBNull.Value) certificateEnterApplyOb.AddPostID = Convert.ToString(reader["AddPostID"]);
                        if (reader["SheBaoCheck"] != DBNull.Value) certificateEnterApplyOb.SheBaoCheck = Convert.ToByte(reader["SheBaoCheck"]);
                        if (reader["CreditCode"] != DBNull.Value) certificateEnterApplyOb.CreditCode = Convert.ToString(reader["CreditCode"]);
                        if (reader["NewUnitCheckTime"] != DBNull.Value) certificateEnterApplyOb.NewUnitCheckTime = Convert.ToDateTime(reader["NewUnitCheckTime"]);
                        if (reader["NewUnitAdvise"] != DBNull.Value) certificateEnterApplyOb.NewUnitAdvise = Convert.ToString(reader["NewUnitAdvise"]);
                        if (reader["ZACheckTime"] != DBNull.Value) certificateEnterApplyOb.ZACheckTime = Convert.ToDateTime(reader["ZACheckTime"]);
                        if (reader["ZACheckResult"] != DBNull.Value) certificateEnterApplyOb.ZACheckResult = Convert.ToInt32(reader["ZACheckResult"]);
                        if (reader["ZACheckRemark"] != DBNull.Value) certificateEnterApplyOb.ZACheckRemark = Convert.ToString(reader["ZACheckRemark"]);
                        if (reader["Job"] != DBNull.Value) certificateEnterApplyOb.Job = Convert.ToString(reader["Job"]);
                        if (reader["SkillLevel"] != DBNull.Value) certificateEnterApplyOb.SkillLevel = Convert.ToString(reader["SkillLevel"]);
                    }
                    reader.Close();
                    db.Close();
                    return certificateEnterApplyOb;
                }
            }
            catch (Exception)
            {
                db.Close();
                throw;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.CertificateEnterApply", "*", filterWhereString, orderBy == "" ? "ApplyDate desc,ApplyID" : orderBy);
        }

        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.CertificateEnterApply", filterWhereString);
        }

        /// <summary>
        /// 统计查询结果现聘用单位组织机构代码数量
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>现聘用单位组织机构代码个数</returns>
        public static int SelectDistinctUnitCodeCount(string filterWhereString)
        {
            string sql = @"SELECT COUNT(distinct UnitCode) FROM dbo.CERTIFICATEENTERAPPLY WHERE 1=1 {0}";
            sql = string.Format(sql, filterWhereString);
            var db = new DBHelper();
            return Convert.ToInt32(db.ExecuteScalar(sql));
        }

        /// <summary>
        /// 进京申请受理
        /// </summary>
        /// <param name="certificateEnterApplyOb">修改参数对象</param>
        /// <param name="filterWhereString">过滤条件</param>
        /// <returns></returns>
        public static int Accepted(CertificateEnterApplyOB certificateEnterApplyOb, string filterWhereString)
        {
            string sql = "UPDATE dbo.CertificateEnterApply SET GetResult =@GetResult,AcceptDate = @AcceptDate,GetMan = @GetMan,GetCode = @GetCode,ApplyStatus=@ApplyStatus WHERE 1=1 " + filterWhereString;
            var db = new DBHelper();
            var p = new List<SqlParameter>
            {
                db.CreateParameter("AcceptDate", DbType.DateTime, certificateEnterApplyOb.AcceptDate),
                db.CreateParameter("GetResult", DbType.String, certificateEnterApplyOb.GetResult),
                db.CreateParameter("GetMan", DbType.String, certificateEnterApplyOb.GetMan),
                db.CreateParameter("GetCode", DbType.String, certificateEnterApplyOb.GetCode),
                db.CreateParameter("ApplyStatus", DbType.String, certificateEnterApplyOb.ApplyStatus)
            };
            return db.ExcuteNonQuery(sql, p.ToArray());
        }

        /// <summary>
        /// 进京申请审核
        /// </summary>
        /// <param name="certificateEnterApplyOb">修改参数对象</param>
        /// <param name="filterWhereString">过滤条件</param>
        /// <returns></returns>
        public static int Check(CertificateEnterApplyOB certificateEnterApplyOb, string filterWhereString)
        {
            string sql = "UPDATE dbo.CertificateEnterApply SET CheckResult =@CheckResult,CheckDate = @CheckDate,CheckMan = @CheckMan,CheckCode = @CheckCode,ApplyStatus=@ApplyStatus WHERE 1=1 " + filterWhereString;
            var db = new DBHelper();
            var p = new List<SqlParameter>
            {
                db.CreateParameter("CheckDate", DbType.DateTime, certificateEnterApplyOb.CheckDate),
                db.CreateParameter("CheckResult", DbType.String, certificateEnterApplyOb.CheckResult),
                db.CreateParameter("CheckMan", DbType.String, certificateEnterApplyOb.CheckMan),
                db.CreateParameter("CheckCode", DbType.String, certificateEnterApplyOb.CheckCode),
                db.CreateParameter("ApplyStatus", DbType.String, certificateEnterApplyOb.ApplyStatus)
            };
            return db.ExcuteNonQuery(sql, p.ToArray());
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.VIEW_CERTIFICATE_ENTER", "*", filterWhereString, orderBy == "" ? "ApplyDate desc,ApplyID" : orderBy);
        }

        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectViewCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.VIEW_CERTIFICATE_ENTER", filterWhereString);
        }

        /// <summary>
        /// 获取进京申请审批记录集合
        /// </summary>
        /// <param name="applyId">申请ID</param>
        /// <returns>审批历史记录集合</returns>
        public static DataTable GetCheckHistoryList(long applyId)
        {
            string sql = @"select row_number() over(order by ActionData, Action desc) as RowNo ,t.* from 
(
 SELECT '进京申请' as 'Action', [APPLYDATE] as ActionData, case [APPLYSTATUS] when '填报中' then '填报中' else '已提交' end as ActionResult,case [APPLYSTATUS] when '填报中' then '未提交审核' else '待现单位确认' end as ActionRemark, [WORKERNAME] as ActionMan 
 FROM [dbo].[CERTIFICATEENTERAPPLY] where [APPLYID]={0}  
  union all
   SELECT '现单位确认' as 'Action', [NewUnitCheckTime] as ActionData,'已确认' ActionResult,[NewUnitAdvise] as ActionRemark, [UNITNAME] as ActionMan 
 FROM [dbo].[CERTIFICATEENTERAPPLY] where [APPLYID]={0} and  [NewUnitCheckTime] >'2019-1-1'
  union all
   SELECT '进京受理' as 'Action', [ACCEPTDATE] as ActionData,case [GETRESULT] when '通过' then '已受理' else '不予受理' end ActionResult,[GETRESULT] as ActionRemark, [GETMAN] as ActionMan 
 FROM [dbo].[CERTIFICATEENTERAPPLY] where [APPLYID]={0} and [ACCEPTDATE] >'1950-1-1'
   union all 
	SELECT '进京审核' as 'Action', [CHECKDATE] as ActionData,case [CHECKRESULT] when '通过' then '已审核' else '审核未通过' end  ActionResult,[CHECKRESULT] as ActionRemark, [CHECKMAN] as ActionMan 
 FROM [dbo].[CERTIFICATEENTERAPPLY] where [APPLYID]={0} and [CHECKDATE] >'1950-1-1'
   union all 
	SELECT '证书编号' as 'Action', [CONFRIMDATE] as ActionData,'已编号' ActionResult,[CONFRIMRESULT] as ActionRemark, [CONFRIMMAN] as ActionMan 
 FROM [dbo].[CERTIFICATEENTERAPPLY] where [APPLYID]={0} and [CONFRIMDATE] >'1950-1-1'
   union all 
    SELECT '住建部核准' as 'Action', 
	        case when c.ZZUrlUpTime > b.[CONFRIMDATE] then  dateadd(hour,1,b.[CONFRIMDATE]) 
			        when  Convert(varchar(10),b.[CONFRIMDATE],21) = Convert(varchar(10),c.CHECKDATE,21) and  c.EleCertErrTime > b.[CONFRIMDATE] then c.EleCertErrTime
			        else null
	        end as ActionData,
            case when c.ZZUrlUpTime > b.[CONFRIMDATE] then  '已核准'
			        when  Convert(varchar(10),b.[CONFRIMDATE],21) = Convert(varchar(10),c.CHECKDATE,21) and  c.EleCertErrTime > b.[CONFRIMDATE] then '核准未通过'
			        else null
	        end as ActionResult,
            case when c.ZZUrlUpTime > b.[CONFRIMDATE] then  '已生成电子证书【' + c.CERTIFICATECode + '】（办结）' 
			        when  Convert(varchar(10),b.[CONFRIMDATE],21) = Convert(varchar(10),c.CHECKDATE,21) and  c.EleCertErrTime > b.[CONFRIMDATE] then '审批不通过，未生成电子证书。' + c.[EleCertErrDesc]
			        else null
	        end as ActionRemark, 
         '住建部' as ActionMan 
     FROM [dbo].[CERTIFICATEENTERAPPLY] b
     inner join [dbo].[CERTIFICATE] c on b.CERTIFICATEID = c.CERTIFICATEID
     where b.[APPLYID]={0} and b.[CONFRIMDATE] >'1950-1-1' 
     and (c.ZZUrlUpTime > b.[CONFRIMDATE] 
		  or (c.EleCertErrTime > b.[CONFRIMDATE] and Convert(varchar(10),b.[CONFRIMDATE],21) = Convert(varchar(10),c.CHECKDATE,21) )
     )
) t";
            return CommonDAL.GetDataTable(string.Format(sql, applyId));
        }

        /// <summary>
        /// 获取进京申请审批记录集合（对个人和企业，业务决定后，住建部校验不通过前3天显示等待，不显示不通过原因）
        /// </summary>
        /// <param name="applyId">申请ID</param>
        /// <returns>审批历史记录集合</returns>
        public static DataTable GetCheckHistoryListForGRQY(long applyId)
        {
            string sql = @"select row_number() over(order by ActionData, Action desc) as RowNo ,t.* from 
(
 SELECT '进京申请' as 'Action', [APPLYDATE] as ActionData, case [APPLYSTATUS] when '填报中' then '填报中' else '已提交' end as ActionResult,case [APPLYSTATUS] when '填报中' then '未提交审核' else '待现单位确认' end as ActionRemark, [WORKERNAME] as ActionMan 
 FROM [dbo].[CERTIFICATEENTERAPPLY] where [APPLYID]={0}  
  union all
   SELECT '现单位确认' as 'Action', [NewUnitCheckTime] as ActionData,'已确认' ActionResult,[NewUnitAdvise] as ActionRemark, [UNITNAME] as ActionMan 
 FROM [dbo].[CERTIFICATEENTERAPPLY] where [APPLYID]={0} and  [NewUnitCheckTime] >'2019-1-1'
  union all
   SELECT '进京受理' as 'Action', [ACCEPTDATE] as ActionData,case [GETRESULT] when '通过' then '已受理' else '不予受理' end ActionResult,[GETRESULT] as ActionRemark, [GETMAN] as ActionMan 
 FROM [dbo].[CERTIFICATEENTERAPPLY] where [APPLYID]={0} and [ACCEPTDATE] >'1950-1-1'
   union all 
	SELECT '进京审核' as 'Action', [CHECKDATE] as ActionData,case [CHECKRESULT] when '通过' then '已审核' else '审核未通过' end  ActionResult,[CHECKRESULT] as ActionRemark, [CHECKMAN] as ActionMan 
 FROM [dbo].[CERTIFICATEENTERAPPLY] where [APPLYID]={0} and [CHECKDATE] >'1950-1-1'
   union all 
	SELECT '证书编号' as 'Action', [CONFRIMDATE] as ActionData,'已编号' ActionResult,[CONFRIMRESULT] as ActionRemark, [CONFRIMMAN] as ActionMan 
 FROM [dbo].[CERTIFICATEENTERAPPLY] where [APPLYID]={0} and [CONFRIMDATE] >'1950-1-1'
   union all 
    SELECT '住建部核准' as 'Action', 
	        case when c.ZZUrlUpTime > b.[CONFRIMDATE] then  dateadd(hour,1,b.[CONFRIMDATE]) 
			        when  Convert(varchar(10),b.[CONFRIMDATE],21) = Convert(varchar(10),c.CHECKDATE,21) and  c.EleCertErrTime > b.[CONFRIMDATE] and b.[CONFRIMDATE] < dateadd(day,-2,getdate()) then c.EleCertErrTime
			        else null
	        end as ActionData,
            case when c.ZZUrlUpTime > b.[CONFRIMDATE] then  '已核准'
			        when  Convert(varchar(10),b.[CONFRIMDATE],21) = Convert(varchar(10),c.CHECKDATE,21) and  c.EleCertErrTime > b.[CONFRIMDATE] and b.[CONFRIMDATE] < dateadd(day,-2,getdate()) then '核准未通过'
			        else null
	        end as ActionResult,
            case when c.ZZUrlUpTime > b.[CONFRIMDATE] then  '已生成电子证书【' + c.CERTIFICATECode + '】（办结）' 
			    when  Convert(varchar(10),b.[CONFRIMDATE],21) = Convert(varchar(10),c.CHECKDATE,21) and  c.EleCertErrTime > b.[CONFRIMDATE] and b.[CONFRIMDATE] < dateadd(day,-2,getdate()) then '审批不通过，未生成电子证书。' + c.[EleCertErrDesc]
                when  Convert(varchar(10),b.[CONFRIMDATE],21) = Convert(varchar(10),c.CHECKDATE,21) and  c.EleCertErrTime > b.[CONFRIMDATE] then '住建部正在进行数据校验核对，生成电子证书需要1-3个工作日，请您耐心等待。'
			    else null
	        end as ActionRemark, 
         '住建部' as ActionMan 
     FROM [dbo].[CERTIFICATEENTERAPPLY] b
     inner join [dbo].[CERTIFICATE] c on b.CERTIFICATEID = c.CERTIFICATEID
     where b.[APPLYID]={0} and b.[CONFRIMDATE] >'1950-1-1' 
     and (c.ZZUrlUpTime > b.[CONFRIMDATE] 
		  or (c.EleCertErrTime > b.[CONFRIMDATE] and Convert(varchar(10),b.[CONFRIMDATE],21) = Convert(varchar(10),c.CHECKDATE,21) )
     )
) t";
            return CommonDAL.GetDataTable(string.Format(sql, applyId));
        }

        /// <summary>
        /// 根据证书ID获取证书进京申请单中填写的原省证书编号
        /// </summary>
        /// <param name="CertificateID">本省证书ID</param>
        /// <returns>原省证书编号</returns>
        public static string GetCertificateCodeByCertificateID(long CertificateID)
        {
            const string sql = @"
			SELECT top 1 ApplyID,WorkerID,PostTypeID,PostID,WorkerName,Sex,Birthday,WorkerCertificateCode,OldUnitName,UnitName,UnitCode,Phone,CertificateCode,ConferDate,ValidStartDate,ValidEndDate,ConferUnit,ApplyStatus,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,ApplyDate,ApplyMan,ApplyCode,AcceptDate,GetResult,GetMan,GetCode,CheckDate,CheckResult,CheckMan,CheckCode,ConfrimDate,ConfrimResult,ConfrimMan,ConfrimCode,CertificateID,AddPostID,CreditCode,NewUnitCheckTime,NewUnitAdvise,[SHEBAOCHECK],ZACheckTime,ZACheckResult,ZACheckRemark,Job,SkillLevel
			FROM dbo.CertificateEnterApply
			WHERE CertificateID = @CertificateID
            order by ApplyID desc";

            var db = new DBHelper();
            var p = new List<SqlParameter>
            {
                db.CreateParameter("CertificateID", DbType.Int64, CertificateID)
            };
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    string CertificateCode = "";
                    if (reader.Read())
                    {
                        CertificateCode= Convert.ToString(reader["CertificateCode"]);                     
                    }
                    reader.Close();
                    db.Close();
                    return CertificateCode;
                }
            }
            catch (Exception)
            {
                db.Close();
                throw;
            }
        }

        /// <summary>
        /// 根据证书ID获取原省证书标识
        /// </summary>
        /// <param name="CertificateID">本省证书ID</param>
        /// <returns>外省证书标识</returns>
        public static string GetzzeCertIDByCertificateID(long CertificateID)
        {
            const string sql = @"
            SELECT [out_zzeCertID] FROM [dbo].[CertificateOut] where [out_certNum]=
            (
			    SELECT top 1 CertificateCode
			    FROM dbo.CertificateEnterApply
			    WHERE CertificateID = @CertificateID
                order by ApplyID desc
            )";

            var db = new DBHelper();
            var p = new List<SqlParameter>
            {
                db.CreateParameter("CertificateID", DbType.Int64, CertificateID)
            };
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    string out_zzeCertID = "";
                    if (reader.Read())
                    {
                        out_zzeCertID = Convert.ToString(reader["out_zzeCertID"]);
                    }
                    reader.Close();
                    db.Close();
                    return out_zzeCertID;
                }
            }
            catch (Exception)
            {
                db.Close();
                throw;
            }
        }
    }
}