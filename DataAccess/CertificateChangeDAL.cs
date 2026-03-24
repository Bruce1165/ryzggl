using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
    /// <summary>
    /// 业务类实现--CertificateChangeDAL(填写类描述)
    /// </summary>
    public class CertificateChangeDAL
    {
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="certificateChangeOb">对象实体类</param>
        /// <returns></returns>
        public static long Insert(CertificateChangeOB certificateChangeOb)
        {
            return Insert(null, certificateChangeOb);
        }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="certificateChangeOb">对象实体类</param>
        /// <returns></returns>
        public static long Insert(DbTransaction tran, CertificateChangeOB certificateChangeOb)
        {
            var db = new DBHelper();

            const string sql = @"
			INSERT INTO dbo.CertificateChange(CertificateID,ChangeType,WorkerName,Sex,Birthday,ConferDate,ValidStartDate,ValidEndDate,ConferUnit,DealWay,OldUnitAdvise,NewUnitAdvise,OldConferUnitAdvise,NewConferUnitAdvise,ApplyDate,ApplyMan,ApplyCode,[GETDATE],GetResult,GetMan,GetCode,CheckDate,CheckResult,CheckMan,CheckCode,ConfrimDate,ConfrimResult,ConfrimMan,ConfrimCode,NoticeDate,NoticeResult,NoticeMan,NoticeCode,Status,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,UnitName,NewUnitName,UnitCode,NewUnitCode,WorkerCertificateCode,LinkWay,NewWorkerCertificateCode,NewWorkerName,NewSex,NewBirthday,SheBaoCheck,IfUpdatePhoto,OldUnitCheckTime,NewUnitCheckTime,ChangeRemark,Job,SkillLevel,ENT_ContractType,ENT_ContractStartTime,ENT_ContractENDTime)
			VALUES (@CertificateID,@ChangeType,@WorkerName,@Sex,@Birthday,@ConferDate,@ValidStartDate,@ValidEndDate,@ConferUnit,@DealWay,@OldUnitAdvise,@NewUnitAdvise,@OldConferUnitAdvise,@NewConferUnitAdvise,@ApplyDate,@ApplyMan,@ApplyCode,@GetDate,@GetResult,@GetMan,@GetCode,@CheckDate,@CheckResult,@CheckMan,@CheckCode,@ConfrimDate,@ConfrimResult,@ConfrimMan,@ConfrimCode,@NoticeDate,@NoticeResult,@NoticeMan,@NoticeCode,@Status,@CreatePersonID,@CreateTime,@ModifyPersonID,@ModifyTime,@UnitName,@NewUnitName,@UnitCode,@NewUnitCode,@WorkerCertificateCode,@LinkWay,@NewWorkerCertificateCode,@NewWorkerName,@NewSex,@NewBirthday,@SheBaoCheck,@IfUpdatePhoto,@OldUnitCheckTime,@NewUnitCheckTime,@ChangeRemark,@Job,@SkillLevel,@ENT_ContractType,@ENT_ContractStartTime,@ENT_ContractENDTime);SELECT @CertificateChangeID = @@IDENTITY";

            var p = new List<SqlParameter>
            {
                db.CreateOutParameter("CertificateChangeID", DbType.Int64),
                db.CreateParameter("CertificateID", DbType.Int64, certificateChangeOb.CertificateID),
                db.CreateParameter("ChangeType", DbType.String, certificateChangeOb.ChangeType),
                db.CreateParameter("WorkerName", DbType.String, certificateChangeOb.WorkerName),
                db.CreateParameter("Sex", DbType.String, certificateChangeOb.Sex),
                db.CreateParameter("Birthday", DbType.DateTime, certificateChangeOb.Birthday),
                db.CreateParameter("ConferDate", DbType.DateTime, certificateChangeOb.ConferDate),
                db.CreateParameter("ValidStartDate", DbType.DateTime, certificateChangeOb.ValidStartDate),
                db.CreateParameter("ValidEndDate", DbType.DateTime, certificateChangeOb.ValidEndDate),
                db.CreateParameter("ConferUnit", DbType.String, certificateChangeOb.ConferUnit),
                db.CreateParameter("DealWay", DbType.String, certificateChangeOb.DealWay),
                db.CreateParameter("OldUnitAdvise", DbType.String, certificateChangeOb.OldUnitAdvise),
                db.CreateParameter("NewUnitAdvise", DbType.String, certificateChangeOb.NewUnitAdvise),
                db.CreateParameter("OldConferUnitAdvise", DbType.String, certificateChangeOb.OldConferUnitAdvise),
                db.CreateParameter("NewConferUnitAdvise", DbType.String, certificateChangeOb.NewConferUnitAdvise),
                db.CreateParameter("ApplyDate", DbType.DateTime, certificateChangeOb.ApplyDate),
                db.CreateParameter("ApplyMan", DbType.String, certificateChangeOb.ApplyMan),
                db.CreateParameter("ApplyCode", DbType.String, certificateChangeOb.ApplyCode),
                db.CreateParameter("GetDate", DbType.DateTime, certificateChangeOb.GetDate),
                db.CreateParameter("GetResult", DbType.String, certificateChangeOb.GetResult),
                db.CreateParameter("GetMan", DbType.String, certificateChangeOb.GetMan),
                db.CreateParameter("GetCode", DbType.String, certificateChangeOb.GetCode),
                db.CreateParameter("CheckDate", DbType.DateTime, certificateChangeOb.CheckDate),
                db.CreateParameter("CheckResult", DbType.String, certificateChangeOb.CheckResult),
                db.CreateParameter("CheckMan", DbType.String, certificateChangeOb.CheckMan),
                db.CreateParameter("CheckCode", DbType.String, certificateChangeOb.CheckCode),
                db.CreateParameter("ConfrimDate", DbType.DateTime, certificateChangeOb.ConfrimDate),
                db.CreateParameter("ConfrimResult", DbType.String, certificateChangeOb.ConfrimResult),
                db.CreateParameter("ConfrimMan", DbType.String, certificateChangeOb.ConfrimMan),
                db.CreateParameter("ConfrimCode", DbType.String, certificateChangeOb.ConfrimCode),
                db.CreateParameter("NoticeDate", DbType.DateTime, certificateChangeOb.NoticeDate),
                db.CreateParameter("NoticeResult", DbType.String, certificateChangeOb.NoticeResult),
                db.CreateParameter("NoticeMan", DbType.String, certificateChangeOb.NoticeMan),
                db.CreateParameter("NoticeCode", DbType.String, certificateChangeOb.NoticeCode),
                db.CreateParameter("Status", DbType.String, certificateChangeOb.Status),
                db.CreateParameter("CreatePersonID", DbType.Int64, certificateChangeOb.CreatePersonID),
                db.CreateParameter("CreateTime", DbType.DateTime, certificateChangeOb.CreateTime),
                db.CreateParameter("ModifyPersonID", DbType.Int64, certificateChangeOb.ModifyPersonID),
                db.CreateParameter("ModifyTime", DbType.DateTime, certificateChangeOb.ModifyTime),
                db.CreateParameter("UnitName", DbType.String, certificateChangeOb.UnitName),
                db.CreateParameter("NewUnitName", DbType.String, certificateChangeOb.NewUnitName),
                db.CreateParameter("UnitCode", DbType.String, certificateChangeOb.UnitCode),
                db.CreateParameter("NewUnitCode", DbType.String, certificateChangeOb.NewUnitCode),
                db.CreateParameter("WorkerCertificateCode", DbType.String, certificateChangeOb.WorkerCertificateCode),
                db.CreateParameter("LinkWay", DbType.String, certificateChangeOb.LinkWay),
                db.CreateParameter("NewWorkerCertificateCode", DbType.String,certificateChangeOb.NewWorkerCertificateCode),
                db.CreateParameter("NewWorkerName", DbType.String, certificateChangeOb.NewWorkerName),
                db.CreateParameter("NewSex", DbType.String, certificateChangeOb.NewSex),
                db.CreateParameter("NewBirthday", DbType.DateTime, certificateChangeOb.NewBirthday),
                db.CreateParameter("SheBaoCheck", DbType.Byte, certificateChangeOb.SheBaoCheck),
                db.CreateParameter("IfUpdatePhoto", DbType.Byte, certificateChangeOb.IfUpdatePhoto),
                db.CreateParameter("OldUnitCheckTime", DbType.DateTime, certificateChangeOb.OldUnitCheckTime),
                db.CreateParameter("NewUnitCheckTime", DbType.DateTime, certificateChangeOb.NewUnitCheckTime),
                db.CreateParameter("ChangeRemark", DbType.String, certificateChangeOb.ChangeRemark),
			    db.CreateParameter("Job",DbType.String, certificateChangeOb.Job),
			    db.CreateParameter("SkillLevel",DbType.String, certificateChangeOb.SkillLevel),
                db.CreateParameter("ENT_ContractType", DbType.Int32, certificateChangeOb.ENT_ContractType),
                db.CreateParameter("ENT_ContractStartTime", DbType.DateTime, certificateChangeOb.ENT_ContractStartTime),
                db.CreateParameter("ENT_ContractENDTime", DbType.DateTime, certificateChangeOb.ENT_ContractENDTime)
            };
            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            certificateChangeOb.CertificateChangeID = Convert.ToInt64(p[0].Value);
            return rtn;
        }

        /// <summary>
        /// 新增批量变更申请
        /// </summary>
        /// <param name="certificateChangeOb">变更参数</param>
        /// <param name="filterString">证书过滤条件</param>
        /// <returns></returns>
        public static long InsertBatch(CertificateChangeOB certificateChangeOb, string filterString)
        {
            var db = new DBHelper();
            const string sql = @"INSERT INTO dbo.CertificateChange(CertificateID,ChangeType,WorkerName,Sex,Birthday,ConferDate,ValidStartDate,ValidEndDate,ConferUnit,DealWay,OldUnitAdvise,NewUnitAdvise
,ApplyDate,ApplyMan,ApplyCode,[STATUS],CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,UnitName,NewUnitName,UnitCode,NewUnitCode,WorkerCertificateCode,LinkWay,NewWorkerCertificateCode,NewWorkerName,NewSex,NewBirthday)
select CertificateID,@ChangeType,WorkerName,Sex,Birthday,ConferDate,ValidStartDate,ValidEndDate,ConferUnit,@DealWay,@OldUnitAdvise,@NewUnitAdvise
,@ApplyDate,@ApplyMan,@ApplyCode,@Status,@CreatePersonID,@CreateTime,@ModifyPersonID,@ModifyTime,@UnitName,@NewUnitName,@UnitCode,@NewUnitCode,WorkerCertificateCode
,@LinkWay,WorkerCertificateCode,WorkerName,Sex,Birthday
 from dbo.VIEW_CERTIFICATE_LEFT_CERTIFICATECHANGE where 1=1 {0} order by CertificateID";

            var p = new List<SqlParameter>
            {
                db.CreateParameter("ChangeType", DbType.String, certificateChangeOb.ChangeType),
                db.CreateParameter("DealWay", DbType.String, certificateChangeOb.DealWay),
                db.CreateParameter("OldUnitAdvise", DbType.String, certificateChangeOb.OldUnitAdvise),
                db.CreateParameter("NewUnitAdvise", DbType.String, certificateChangeOb.NewUnitAdvise),
                db.CreateParameter("ApplyDate", DbType.DateTime, certificateChangeOb.ApplyDate),
                db.CreateParameter("ApplyMan", DbType.String, certificateChangeOb.ApplyMan),
                db.CreateParameter("ApplyCode", DbType.String, certificateChangeOb.ApplyCode),
                db.CreateParameter("Status", DbType.String, certificateChangeOb.Status),
                db.CreateParameter("CreatePersonID", DbType.Int64, certificateChangeOb.CreatePersonID),
                db.CreateParameter("CreateTime", DbType.DateTime, certificateChangeOb.CreateTime),
                db.CreateParameter("ModifyPersonID", DbType.Int64, certificateChangeOb.ModifyPersonID),
                db.CreateParameter("ModifyTime", DbType.DateTime, certificateChangeOb.ModifyTime),
                db.CreateParameter("UnitName", DbType.String, certificateChangeOb.UnitName),
                db.CreateParameter("NewUnitName", DbType.String, certificateChangeOb.NewUnitName),
                db.CreateParameter("UnitCode", DbType.String, certificateChangeOb.UnitCode),
                db.CreateParameter("NewUnitCode", DbType.String, certificateChangeOb.NewUnitCode),
                db.CreateParameter("LinkWay", DbType.String, certificateChangeOb.LinkWay)
            };
            return db.ExcuteNonQuery(string.Format(sql, filterString), p.ToArray());
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="certificateChangeOb">对象实体类</param>
        /// <returns></returns>
        public static int Update(CertificateChangeOB certificateChangeOb)
        {
            return Update(null, certificateChangeOb);
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="certificateChangeOb">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, CertificateChangeOB certificateChangeOb)
        {
            const string sql = @"
			UPDATE dbo.CertificateChange
				SET	CertificateID = @CertificateID,ChangeType = @ChangeType,WorkerName = @WorkerName,Sex = @Sex,Birthday = @Birthday,ConferDate = @ConferDate,ValidStartDate = @ValidStartDate,ValidEndDate = @ValidEndDate,ConferUnit = @ConferUnit,DealWay = @DealWay,OldUnitAdvise = @OldUnitAdvise,NewUnitAdvise = @NewUnitAdvise,OldConferUnitAdvise = @OldConferUnitAdvise,NewConferUnitAdvise = @NewConferUnitAdvise,ApplyDate = @ApplyDate,ApplyMan = @ApplyMan,ApplyCode = @ApplyCode,[GETDATE] = @GetDate,GetResult = @GetResult,GetMan = @GetMan,GetCode = @GetCode,CheckDate = @CheckDate,CheckResult = @CheckResult,CheckMan = @CheckMan,CheckCode = @CheckCode,ConfrimDate = @ConfrimDate,ConfrimResult = @ConfrimResult,ConfrimMan = @ConfrimMan,ConfrimCode = @ConfrimCode,NoticeDate = @NoticeDate,NoticeResult = @NoticeResult,NoticeMan = @NoticeMan,NoticeCode = @NoticeCode,Status = @Status,CreatePersonID = @CreatePersonID,CreateTime = @CreateTime,ModifyPersonID = @ModifyPersonID,ModifyTime = @ModifyTime,UnitName = @UnitName,NewUnitName = @NewUnitName,UnitCode = @UnitCode,NewUnitCode = @NewUnitCode,WorkerCertificateCode = @WorkerCertificateCode,LinkWay = @LinkWay,NewWorkerCertificateCode = @NewWorkerCertificateCode,NewWorkerName = @NewWorkerName,NewSex = @NewSex,NewBirthday = @NewBirthday,SheBaoCheck =@SheBaoCheck,IfUpdatePhoto =@IfUpdatePhoto,OldUnitCheckTime=@OldUnitCheckTime,NewUnitCheckTime=@NewUnitCheckTime,ChangeRemark=@ChangeRemark,Job = @Job,SkillLevel = @SkillLevel,ZACheckTime = @ZACheckTime,ZACheckResult = @ZACheckResult,ZACheckRemark = @ZACheckRemark,ENT_ContractType = @ENT_ContractType,ENT_ContractStartTime = @ENT_ContractStartTime,ENT_ContractENDTime = @ENT_ContractENDTime
			WHERE
				CertificateChangeID = @CertificateChangeID";

            var db = new DBHelper();
            var p = new List<SqlParameter>
            {
                db.CreateParameter("CertificateChangeID", DbType.Int64, certificateChangeOb.CertificateChangeID),
                db.CreateParameter("CertificateID", DbType.Int64, certificateChangeOb.CertificateID),
                db.CreateParameter("ChangeType", DbType.String, certificateChangeOb.ChangeType),
                db.CreateParameter("WorkerName", DbType.String, certificateChangeOb.WorkerName),
                db.CreateParameter("Sex", DbType.String, certificateChangeOb.Sex),
                db.CreateParameter("Birthday", DbType.DateTime, certificateChangeOb.Birthday),
                db.CreateParameter("ConferDate", DbType.DateTime, certificateChangeOb.ConferDate),
                db.CreateParameter("ValidStartDate", DbType.DateTime, certificateChangeOb.ValidStartDate),
                db.CreateParameter("ValidEndDate", DbType.DateTime, certificateChangeOb.ValidEndDate),
                db.CreateParameter("ConferUnit", DbType.String, certificateChangeOb.ConferUnit),
                db.CreateParameter("DealWay", DbType.String, certificateChangeOb.DealWay),
                db.CreateParameter("OldUnitAdvise", DbType.String, certificateChangeOb.OldUnitAdvise),
                db.CreateParameter("NewUnitAdvise", DbType.String, certificateChangeOb.NewUnitAdvise),
                db.CreateParameter("OldConferUnitAdvise", DbType.String, certificateChangeOb.OldConferUnitAdvise),
                db.CreateParameter("NewConferUnitAdvise", DbType.String, certificateChangeOb.NewConferUnitAdvise),
                db.CreateParameter("ApplyDate", DbType.DateTime, certificateChangeOb.ApplyDate),
                db.CreateParameter("ApplyMan", DbType.String, certificateChangeOb.ApplyMan),
                db.CreateParameter("ApplyCode", DbType.String, certificateChangeOb.ApplyCode),
                db.CreateParameter("GetDate", DbType.DateTime, certificateChangeOb.GetDate),
                db.CreateParameter("GetResult", DbType.String, certificateChangeOb.GetResult),
                db.CreateParameter("GetMan", DbType.String, certificateChangeOb.GetMan),
                db.CreateParameter("GetCode", DbType.String, certificateChangeOb.GetCode),
                db.CreateParameter("CheckDate", DbType.DateTime, certificateChangeOb.CheckDate),
                db.CreateParameter("CheckResult", DbType.String, certificateChangeOb.CheckResult),
                db.CreateParameter("CheckMan", DbType.String, certificateChangeOb.CheckMan),
                db.CreateParameter("CheckCode", DbType.String, certificateChangeOb.CheckCode),
                db.CreateParameter("ConfrimDate", DbType.DateTime, certificateChangeOb.ConfrimDate),
                db.CreateParameter("ConfrimResult", DbType.String, certificateChangeOb.ConfrimResult),
                db.CreateParameter("ConfrimMan", DbType.String, certificateChangeOb.ConfrimMan),
                db.CreateParameter("ConfrimCode", DbType.String, certificateChangeOb.ConfrimCode),
                db.CreateParameter("NoticeDate", DbType.DateTime, certificateChangeOb.NoticeDate),
                db.CreateParameter("NoticeResult", DbType.String, certificateChangeOb.NoticeResult),
                db.CreateParameter("NoticeMan", DbType.String, certificateChangeOb.NoticeMan),
                db.CreateParameter("NoticeCode", DbType.String, certificateChangeOb.NoticeCode),
                db.CreateParameter("Status", DbType.String, certificateChangeOb.Status),
                db.CreateParameter("CreatePersonID", DbType.Int64, certificateChangeOb.CreatePersonID),
                db.CreateParameter("CreateTime", DbType.DateTime, certificateChangeOb.CreateTime),
                db.CreateParameter("ModifyPersonID", DbType.Int64, certificateChangeOb.ModifyPersonID),
                db.CreateParameter("ModifyTime", DbType.DateTime, certificateChangeOb.ModifyTime),
                db.CreateParameter("UnitName", DbType.String, certificateChangeOb.UnitName),
                db.CreateParameter("NewUnitName", DbType.String, certificateChangeOb.NewUnitName),
                db.CreateParameter("UnitCode", DbType.String, certificateChangeOb.UnitCode),
                db.CreateParameter("NewUnitCode", DbType.String, certificateChangeOb.NewUnitCode),
                db.CreateParameter("WorkerCertificateCode", DbType.String, certificateChangeOb.WorkerCertificateCode),
                db.CreateParameter("LinkWay", DbType.String, certificateChangeOb.LinkWay),
                db.CreateParameter("NewWorkerCertificateCode", DbType.String,certificateChangeOb.NewWorkerCertificateCode),
                db.CreateParameter("NewWorkerName", DbType.String, certificateChangeOb.NewWorkerName),
                db.CreateParameter("NewSex", DbType.String, certificateChangeOb.NewSex),
                db.CreateParameter("NewBirthday", DbType.DateTime, certificateChangeOb.NewBirthday),
                db.CreateParameter("SheBaoCheck", DbType.Byte, certificateChangeOb.SheBaoCheck),
                db.CreateParameter("IfUpdatePhoto", DbType.Byte, certificateChangeOb.IfUpdatePhoto),
                db.CreateParameter("OldUnitCheckTime", DbType.DateTime, certificateChangeOb.OldUnitCheckTime),
                db.CreateParameter("NewUnitCheckTime", DbType.DateTime, certificateChangeOb.NewUnitCheckTime),
                db.CreateParameter("ChangeRemark", DbType.String, certificateChangeOb.ChangeRemark),
			    db.CreateParameter("Job",DbType.String, certificateChangeOb.Job),
			    db.CreateParameter("SkillLevel",DbType.String, certificateChangeOb.SkillLevel),
                db.CreateParameter("ZACheckTime",DbType.DateTime, certificateChangeOb.ZACheckTime),
			    db.CreateParameter("ZACheckResult",DbType.Int32, certificateChangeOb.ZACheckResult),
			    db.CreateParameter("ZACheckRemark",DbType.String, certificateChangeOb.ZACheckRemark),
                db.CreateParameter("ENT_ContractType", DbType.Int32, certificateChangeOb.ENT_ContractType),
                db.CreateParameter("ENT_ContractStartTime", DbType.DateTime, certificateChangeOb.ENT_ContractStartTime),
                db.CreateParameter("ENT_ContractENDTime", DbType.DateTime, certificateChangeOb.ENT_ContractENDTime)
            };
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="certificateChangeId">主键</param>
        /// <returns></returns>
        public static int Delete(long certificateChangeId)
        {
            return Delete(null, certificateChangeId);
        }

        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="certificateChangeId">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, long certificateChangeId)
        {
            const string sql = @"DELETE FROM dbo.CertificateChange WHERE CertificateChangeID = @CertificateChangeID";

            var db = new DBHelper();
            var p = new List<SqlParameter>
            {
                db.CreateParameter("CertificateChangeID", DbType.Int64, certificateChangeId)
            };
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        ///// <summary>
        ///// 根据证件ID删除信息
        ///// </summary>
        ///// <param name="certificateId">证件ID</param>
        ///// <returns></returns>
        //public static int DeleteApplying(long certificateId)
        //{
        //    return DeleteApplying(null, certificateId);
        //}

        ///// <summary>
        ///// 根据证件ID删除信息
        ///// </summary>
        ///// <param name="tran">事务</param>
        ///// <param name="certificateId">证件ID</param>
        ///// <returns></returns>
        //public static int DeleteApplying(DbTransaction tran, long certificateId)
        //{
        //    const string sql = @"DELETE FROM dbo.CertificateChange WHERE CertificateID = @CertificateID ";

        //    var db = new DBHelper();
        //    var p = new List<SqlParameter>
        //    {
        //        db.CreateParameter("CertificateID", DbType.Int64, certificateId)
        //    };
        //    return db.ExcuteNonQuery(tran, sql, p.ToArray());
        //}

        ///// <summary>
        ///// 批量删除变更申请
        ///// </summary>
        ///// <param name="certificateChangeIdList">申请ID集合，用逗号分隔</param>
        ///// <returns></returns>
        //public static int DeleteApplying(string certificateChangeIdList)
        //{
        //    const string sql = @"DELETE FROM dbo.CertificateChange WHERE CertificateID in({0})";
        //    var db = new DBHelper();
        //    return db.ExcuteNonQuery(string.Format(sql, certificateChangeIdList));
        //}

        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="certificateChangeOb">对象实体类</param>
        /// <returns></returns>
        public static int Delete(CertificateChangeOB certificateChangeOb)
        {
            return Delete(null, certificateChangeOb);
        }

        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="certificateChangeOb">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, CertificateChangeOB certificateChangeOb)
        {
            const string sql = @"DELETE FROM dbo.CertificateChange WHERE CertificateChangeID = @CertificateChangeID";

            var db = new DBHelper();
            var p = new List<SqlParameter>
            {
                db.CreateParameter("CertificateChangeID", DbType.Int64, certificateChangeOb.CertificateChangeID)
            };
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="certificateChangeId">主键</param>
        public static CertificateChangeOB GetObject(long certificateChangeId)
        {
            return GetObject(null, certificateChangeId);
        }

        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="certificateChangeId">主键</param>
        public static CertificateChangeOB GetObject(DbTransaction tran, long certificateChangeId)
        {
            const string sql = @"
			SELECT CertificateChangeID,CertificateID,ChangeType,WorkerName,Sex,Birthday,ConferDate,ValidStartDate,ValidEndDate,ConferUnit,DealWay,OldUnitAdvise,NewUnitAdvise,OldConferUnitAdvise,NewConferUnitAdvise,ApplyDate,ApplyMan,ApplyCode,[GETDATE],GetResult,GetMan,GetCode,CheckDate,CheckResult,CheckMan,CheckCode,ConfrimDate,ConfrimResult,ConfrimMan,ConfrimCode,NoticeDate,NoticeResult,NoticeMan,NoticeCode,Status,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,UnitName,NewUnitName,UnitCode,NewUnitCode,WorkerCertificateCode,LinkWay,NewWorkerCertificateCode,NewWorkerName,NewSex,NewBirthday,SheBaoCheck,IfUpdatePhoto,OldUnitCheckTime,NewUnitCheckTime,[ChangeRemark],Job,SkillLevel,ZACheckTime,ZACheckResult,ZACheckRemark,ENT_ContractType,ENT_ContractStartTime,ENT_ContractENDTime
			FROM dbo.CertificateChange
			WHERE CertificateChangeID = @CertificateChangeID";

            var db = new DBHelper();
            var p = new List<SqlParameter>
            {
                db.CreateParameter("CertificateChangeID", DbType.Int64, certificateChangeId)
            };
            try
            {
                using (SqlDataReader reader = db.GetDataReader(tran, sql, p.ToArray()))
                {
                    CertificateChangeOB certificateChangeOb = null;
                    if (reader.Read())
                    {
                        certificateChangeOb = new CertificateChangeOB();
                        if (reader["CertificateChangeID"] != DBNull.Value) certificateChangeOb.CertificateChangeID = Convert.ToInt64(reader["CertificateChangeID"]);
                        if (reader["CertificateID"] != DBNull.Value) certificateChangeOb.CertificateID = Convert.ToInt64(reader["CertificateID"]);
                        if (reader["ChangeType"] != DBNull.Value) certificateChangeOb.ChangeType = Convert.ToString(reader["ChangeType"]);
                        if (reader["WorkerName"] != DBNull.Value) certificateChangeOb.WorkerName = Convert.ToString(reader["WorkerName"]);
                        if (reader["Sex"] != DBNull.Value) certificateChangeOb.Sex = Convert.ToString(reader["Sex"]);
                        if (reader["Birthday"] != DBNull.Value) certificateChangeOb.Birthday = Convert.ToDateTime(reader["Birthday"]);
                        if (reader["ConferDate"] != DBNull.Value) certificateChangeOb.ConferDate = Convert.ToDateTime(reader["ConferDate"]);
                        if (reader["ValidStartDate"] != DBNull.Value) certificateChangeOb.ValidStartDate = Convert.ToDateTime(reader["ValidStartDate"]);
                        if (reader["ValidEndDate"] != DBNull.Value) certificateChangeOb.ValidEndDate = Convert.ToDateTime(reader["ValidEndDate"]);
                        if (reader["ConferUnit"] != DBNull.Value) certificateChangeOb.ConferUnit = Convert.ToString(reader["ConferUnit"]);
                        if (reader["DealWay"] != DBNull.Value) certificateChangeOb.DealWay = Convert.ToString(reader["DealWay"]);
                        if (reader["OldUnitAdvise"] != DBNull.Value) certificateChangeOb.OldUnitAdvise = Convert.ToString(reader["OldUnitAdvise"]);
                        if (reader["NewUnitAdvise"] != DBNull.Value) certificateChangeOb.NewUnitAdvise = Convert.ToString(reader["NewUnitAdvise"]);
                        if (reader["OldConferUnitAdvise"] != DBNull.Value) certificateChangeOb.OldConferUnitAdvise = Convert.ToString(reader["OldConferUnitAdvise"]);
                        if (reader["NewConferUnitAdvise"] != DBNull.Value) certificateChangeOb.NewConferUnitAdvise = Convert.ToString(reader["NewConferUnitAdvise"]);
                        if (reader["ApplyDate"] != DBNull.Value) certificateChangeOb.ApplyDate = Convert.ToDateTime(reader["ApplyDate"]);
                        if (reader["ApplyMan"] != DBNull.Value) certificateChangeOb.ApplyMan = Convert.ToString(reader["ApplyMan"]);
                        if (reader["ApplyCode"] != DBNull.Value) certificateChangeOb.ApplyCode = Convert.ToString(reader["ApplyCode"]);
                        if (reader["GetDate"] != DBNull.Value) certificateChangeOb.GetDate = Convert.ToDateTime(reader["GetDate"]);
                        if (reader["GetResult"] != DBNull.Value) certificateChangeOb.GetResult = Convert.ToString(reader["GetResult"]);
                        if (reader["GetMan"] != DBNull.Value) certificateChangeOb.GetMan = Convert.ToString(reader["GetMan"]);
                        if (reader["GetCode"] != DBNull.Value) certificateChangeOb.GetCode = Convert.ToString(reader["GetCode"]);
                        if (reader["CheckDate"] != DBNull.Value) certificateChangeOb.CheckDate = Convert.ToDateTime(reader["CheckDate"]);
                        if (reader["CheckResult"] != DBNull.Value) certificateChangeOb.CheckResult = Convert.ToString(reader["CheckResult"]);
                        if (reader["CheckMan"] != DBNull.Value) certificateChangeOb.CheckMan = Convert.ToString(reader["CheckMan"]);
                        if (reader["CheckCode"] != DBNull.Value) certificateChangeOb.CheckCode = Convert.ToString(reader["CheckCode"]);
                        if (reader["ConfrimDate"] != DBNull.Value) certificateChangeOb.ConfrimDate = Convert.ToDateTime(reader["ConfrimDate"]);
                        if (reader["ConfrimResult"] != DBNull.Value) certificateChangeOb.ConfrimResult = Convert.ToString(reader["ConfrimResult"]);
                        if (reader["ConfrimMan"] != DBNull.Value) certificateChangeOb.ConfrimMan = Convert.ToString(reader["ConfrimMan"]);
                        if (reader["ConfrimCode"] != DBNull.Value) certificateChangeOb.ConfrimCode = Convert.ToString(reader["ConfrimCode"]);
                        if (reader["NoticeDate"] != DBNull.Value) certificateChangeOb.NoticeDate = Convert.ToDateTime(reader["NoticeDate"]);
                        if (reader["NoticeResult"] != DBNull.Value) certificateChangeOb.NoticeResult = Convert.ToString(reader["NoticeResult"]);
                        if (reader["NoticeMan"] != DBNull.Value) certificateChangeOb.NoticeMan = Convert.ToString(reader["NoticeMan"]);
                        if (reader["NoticeCode"] != DBNull.Value) certificateChangeOb.NoticeCode = Convert.ToString(reader["NoticeCode"]);
                        if (reader["Status"] != DBNull.Value) certificateChangeOb.Status = Convert.ToString(reader["Status"]);
                        if (reader["CreatePersonID"] != DBNull.Value) certificateChangeOb.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                        if (reader["CreateTime"] != DBNull.Value) certificateChangeOb.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                        if (reader["ModifyPersonID"] != DBNull.Value) certificateChangeOb.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                        if (reader["ModifyTime"] != DBNull.Value) certificateChangeOb.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                        if (reader["UnitName"] != DBNull.Value) certificateChangeOb.UnitName = Convert.ToString(reader["UnitName"]);
                        if (reader["NewUnitName"] != DBNull.Value) certificateChangeOb.NewUnitName = Convert.ToString(reader["NewUnitName"]);
                        if (reader["UnitCode"] != DBNull.Value) certificateChangeOb.UnitCode = Convert.ToString(reader["UnitCode"]);
                        if (reader["NewUnitCode"] != DBNull.Value) certificateChangeOb.NewUnitCode = Convert.ToString(reader["NewUnitCode"]);
                        if (reader["WorkerCertificateCode"] != DBNull.Value) certificateChangeOb.WorkerCertificateCode = Convert.ToString(reader["WorkerCertificateCode"]);
                        if (reader["LinkWay"] != DBNull.Value) certificateChangeOb.LinkWay = Convert.ToString(reader["LinkWay"]);
                        if (reader["NewWorkerCertificateCode"] != DBNull.Value) certificateChangeOb.NewWorkerCertificateCode = Convert.ToString(reader["NewWorkerCertificateCode"]);
                        if (reader["NewWorkerName"] != DBNull.Value) certificateChangeOb.NewWorkerName = Convert.ToString(reader["NewWorkerName"]);
                        if (reader["NewSex"] != DBNull.Value) certificateChangeOb.NewSex = Convert.ToString(reader["NewSex"]);
                        if (reader["NewBirthday"] != DBNull.Value) certificateChangeOb.NewBirthday = Convert.ToDateTime(reader["NewBirthday"]);
                        if (reader["SheBaoCheck"] != DBNull.Value) certificateChangeOb.SheBaoCheck = Convert.ToByte(reader["SheBaoCheck"]);
                        if (reader["IfUpdatePhoto"] != DBNull.Value) certificateChangeOb.IfUpdatePhoto = Convert.ToByte(reader["IfUpdatePhoto"]);
                        if (reader["OldUnitCheckTime"] != DBNull.Value) certificateChangeOb.OldUnitCheckTime = Convert.ToDateTime(reader["OldUnitCheckTime"]);
                        if (reader["NewUnitCheckTime"] != DBNull.Value) certificateChangeOb.NewUnitCheckTime = Convert.ToDateTime(reader["NewUnitCheckTime"]);
                        if (reader["ChangeRemark"] != DBNull.Value) certificateChangeOb.ChangeRemark = Convert.ToString(reader["ChangeRemark"]);
                        if (reader["Job"] != DBNull.Value) certificateChangeOb.Job = Convert.ToString(reader["Job"]);
                        if (reader["SkillLevel"] != DBNull.Value) certificateChangeOb.SkillLevel = Convert.ToString(reader["SkillLevel"]);
                        if (reader["ZACheckTime"] != DBNull.Value) certificateChangeOb.ZACheckTime = Convert.ToDateTime(reader["ZACheckTime"]);
                        if (reader["ZACheckResult"] != DBNull.Value) certificateChangeOb.ZACheckResult = Convert.ToInt32(reader["ZACheckResult"]);
                        if (reader["ZACheckRemark"] != DBNull.Value) certificateChangeOb.ZACheckRemark = Convert.ToString(reader["ZACheckRemark"]);
                        if (reader["ENT_ContractType"] != DBNull.Value) certificateChangeOb.ENT_ContractType = Convert.ToInt32(reader["ENT_ContractType"]);
                        if (reader["ENT_ContractStartTime"] != DBNull.Value) certificateChangeOb.ENT_ContractStartTime = Convert.ToDateTime(reader["ENT_ContractStartTime"]);
                        if (reader["ENT_ContractENDTime"] != DBNull.Value) certificateChangeOb.ENT_ContractENDTime = Convert.ToDateTime(reader["ENT_ContractENDTime"]);
                    }
                    reader.Close();
                    if (tran == null) db.Close();
                    return certificateChangeOb;
                }
            }
            catch (Exception)
            {
                db.Close();
                throw;
            }
        }

        /// <summary>
        /// 获取证书最后离京变更申请
        /// </summary>
        /// <param name="CertificateID">证书ID</param>
        /// <returns></returns>
        public static CertificateChangeOB GetObjectOfLiJing( long CertificateID)
        {
            const string sql = @"
			SELECT top 1 CertificateChangeID,CertificateID,ChangeType,WorkerName,Sex,Birthday,ConferDate,ValidStartDate,ValidEndDate,ConferUnit,DealWay,OldUnitAdvise,NewUnitAdvise,OldConferUnitAdvise,NewConferUnitAdvise,ApplyDate,ApplyMan,ApplyCode,[GETDATE],GetResult,GetMan,GetCode,CheckDate,CheckResult,CheckMan,CheckCode,ConfrimDate,ConfrimResult,ConfrimMan,ConfrimCode,NoticeDate,NoticeResult,NoticeMan,NoticeCode,Status,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,UnitName,NewUnitName,UnitCode,NewUnitCode,WorkerCertificateCode,LinkWay,NewWorkerCertificateCode,NewWorkerName,NewSex,NewBirthday,SheBaoCheck,IfUpdatePhoto,OldUnitCheckTime,NewUnitCheckTime,[ChangeRemark],Job,SkillLevel,ZACheckTime,ZACheckResult,ZACheckRemark
			FROM dbo.CertificateChange
			WHERE CertificateID = @CertificateID and ChangeType='离京变更' and [Status]='已告知'
            order by CertificateChangeID desc";

            var db = new DBHelper();
            var p = new List<SqlParameter>
            {
                db.CreateParameter("CertificateID", DbType.Int64, CertificateID)
            };
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    CertificateChangeOB certificateChangeOb = null;
                    if (reader.Read())
                    {
                        certificateChangeOb = new CertificateChangeOB();
                        if (reader["CertificateChangeID"] != DBNull.Value) certificateChangeOb.CertificateChangeID = Convert.ToInt64(reader["CertificateChangeID"]);
                        if (reader["CertificateID"] != DBNull.Value) certificateChangeOb.CertificateID = Convert.ToInt64(reader["CertificateID"]);
                        if (reader["ChangeType"] != DBNull.Value) certificateChangeOb.ChangeType = Convert.ToString(reader["ChangeType"]);
                        if (reader["WorkerName"] != DBNull.Value) certificateChangeOb.WorkerName = Convert.ToString(reader["WorkerName"]);
                        if (reader["Sex"] != DBNull.Value) certificateChangeOb.Sex = Convert.ToString(reader["Sex"]);
                        if (reader["Birthday"] != DBNull.Value) certificateChangeOb.Birthday = Convert.ToDateTime(reader["Birthday"]);
                        if (reader["ConferDate"] != DBNull.Value) certificateChangeOb.ConferDate = Convert.ToDateTime(reader["ConferDate"]);
                        if (reader["ValidStartDate"] != DBNull.Value) certificateChangeOb.ValidStartDate = Convert.ToDateTime(reader["ValidStartDate"]);
                        if (reader["ValidEndDate"] != DBNull.Value) certificateChangeOb.ValidEndDate = Convert.ToDateTime(reader["ValidEndDate"]);
                        if (reader["ConferUnit"] != DBNull.Value) certificateChangeOb.ConferUnit = Convert.ToString(reader["ConferUnit"]);
                        if (reader["DealWay"] != DBNull.Value) certificateChangeOb.DealWay = Convert.ToString(reader["DealWay"]);
                        if (reader["OldUnitAdvise"] != DBNull.Value) certificateChangeOb.OldUnitAdvise = Convert.ToString(reader["OldUnitAdvise"]);
                        if (reader["NewUnitAdvise"] != DBNull.Value) certificateChangeOb.NewUnitAdvise = Convert.ToString(reader["NewUnitAdvise"]);
                        if (reader["OldConferUnitAdvise"] != DBNull.Value) certificateChangeOb.OldConferUnitAdvise = Convert.ToString(reader["OldConferUnitAdvise"]);
                        if (reader["NewConferUnitAdvise"] != DBNull.Value) certificateChangeOb.NewConferUnitAdvise = Convert.ToString(reader["NewConferUnitAdvise"]);
                        if (reader["ApplyDate"] != DBNull.Value) certificateChangeOb.ApplyDate = Convert.ToDateTime(reader["ApplyDate"]);
                        if (reader["ApplyMan"] != DBNull.Value) certificateChangeOb.ApplyMan = Convert.ToString(reader["ApplyMan"]);
                        if (reader["ApplyCode"] != DBNull.Value) certificateChangeOb.ApplyCode = Convert.ToString(reader["ApplyCode"]);
                        if (reader["GetDate"] != DBNull.Value) certificateChangeOb.GetDate = Convert.ToDateTime(reader["GetDate"]);
                        if (reader["GetResult"] != DBNull.Value) certificateChangeOb.GetResult = Convert.ToString(reader["GetResult"]);
                        if (reader["GetMan"] != DBNull.Value) certificateChangeOb.GetMan = Convert.ToString(reader["GetMan"]);
                        if (reader["GetCode"] != DBNull.Value) certificateChangeOb.GetCode = Convert.ToString(reader["GetCode"]);
                        if (reader["CheckDate"] != DBNull.Value) certificateChangeOb.CheckDate = Convert.ToDateTime(reader["CheckDate"]);
                        if (reader["CheckResult"] != DBNull.Value) certificateChangeOb.CheckResult = Convert.ToString(reader["CheckResult"]);
                        if (reader["CheckMan"] != DBNull.Value) certificateChangeOb.CheckMan = Convert.ToString(reader["CheckMan"]);
                        if (reader["CheckCode"] != DBNull.Value) certificateChangeOb.CheckCode = Convert.ToString(reader["CheckCode"]);
                        if (reader["ConfrimDate"] != DBNull.Value) certificateChangeOb.ConfrimDate = Convert.ToDateTime(reader["ConfrimDate"]);
                        if (reader["ConfrimResult"] != DBNull.Value) certificateChangeOb.ConfrimResult = Convert.ToString(reader["ConfrimResult"]);
                        if (reader["ConfrimMan"] != DBNull.Value) certificateChangeOb.ConfrimMan = Convert.ToString(reader["ConfrimMan"]);
                        if (reader["ConfrimCode"] != DBNull.Value) certificateChangeOb.ConfrimCode = Convert.ToString(reader["ConfrimCode"]);
                        if (reader["NoticeDate"] != DBNull.Value) certificateChangeOb.NoticeDate = Convert.ToDateTime(reader["NoticeDate"]);
                        if (reader["NoticeResult"] != DBNull.Value) certificateChangeOb.NoticeResult = Convert.ToString(reader["NoticeResult"]);
                        if (reader["NoticeMan"] != DBNull.Value) certificateChangeOb.NoticeMan = Convert.ToString(reader["NoticeMan"]);
                        if (reader["NoticeCode"] != DBNull.Value) certificateChangeOb.NoticeCode = Convert.ToString(reader["NoticeCode"]);
                        if (reader["Status"] != DBNull.Value) certificateChangeOb.Status = Convert.ToString(reader["Status"]);
                        if (reader["CreatePersonID"] != DBNull.Value) certificateChangeOb.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                        if (reader["CreateTime"] != DBNull.Value) certificateChangeOb.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                        if (reader["ModifyPersonID"] != DBNull.Value) certificateChangeOb.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                        if (reader["ModifyTime"] != DBNull.Value) certificateChangeOb.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                        if (reader["UnitName"] != DBNull.Value) certificateChangeOb.UnitName = Convert.ToString(reader["UnitName"]);
                        if (reader["NewUnitName"] != DBNull.Value) certificateChangeOb.NewUnitName = Convert.ToString(reader["NewUnitName"]);
                        if (reader["UnitCode"] != DBNull.Value) certificateChangeOb.UnitCode = Convert.ToString(reader["UnitCode"]);
                        if (reader["NewUnitCode"] != DBNull.Value) certificateChangeOb.NewUnitCode = Convert.ToString(reader["NewUnitCode"]);
                        if (reader["WorkerCertificateCode"] != DBNull.Value) certificateChangeOb.WorkerCertificateCode = Convert.ToString(reader["WorkerCertificateCode"]);
                        if (reader["LinkWay"] != DBNull.Value) certificateChangeOb.LinkWay = Convert.ToString(reader["LinkWay"]);
                        if (reader["NewWorkerCertificateCode"] != DBNull.Value) certificateChangeOb.NewWorkerCertificateCode = Convert.ToString(reader["NewWorkerCertificateCode"]);
                        if (reader["NewWorkerName"] != DBNull.Value) certificateChangeOb.NewWorkerName = Convert.ToString(reader["NewWorkerName"]);
                        if (reader["NewSex"] != DBNull.Value) certificateChangeOb.NewSex = Convert.ToString(reader["NewSex"]);
                        if (reader["NewBirthday"] != DBNull.Value) certificateChangeOb.NewBirthday = Convert.ToDateTime(reader["NewBirthday"]);
                        if (reader["SheBaoCheck"] != DBNull.Value) certificateChangeOb.SheBaoCheck = Convert.ToByte(reader["SheBaoCheck"]);
                        if (reader["IfUpdatePhoto"] != DBNull.Value) certificateChangeOb.IfUpdatePhoto = Convert.ToByte(reader["IfUpdatePhoto"]);
                        if (reader["OldUnitCheckTime"] != DBNull.Value) certificateChangeOb.OldUnitCheckTime = Convert.ToDateTime(reader["OldUnitCheckTime"]);
                        if (reader["NewUnitCheckTime"] != DBNull.Value) certificateChangeOb.NewUnitCheckTime = Convert.ToDateTime(reader["NewUnitCheckTime"]);
                        if (reader["ChangeRemark"] != DBNull.Value) certificateChangeOb.ChangeRemark = Convert.ToString(reader["ChangeRemark"]);
                        if (reader["Job"] != DBNull.Value) certificateChangeOb.Job = Convert.ToString(reader["Job"]);
                        if (reader["SkillLevel"] != DBNull.Value) certificateChangeOb.SkillLevel = Convert.ToString(reader["SkillLevel"]);
                        if (reader["ZACheckTime"] != DBNull.Value) certificateChangeOb.ZACheckTime = Convert.ToDateTime(reader["ZACheckTime"]);
                        if (reader["ZACheckResult"] != DBNull.Value) certificateChangeOb.ZACheckResult = Convert.ToInt32(reader["ZACheckResult"]);
                        if (reader["ZACheckRemark"] != DBNull.Value) certificateChangeOb.ZACheckRemark = Convert.ToString(reader["ZACheckRemark"]);
                    }
                    reader.Close();
                    db.Close();
                    return certificateChangeOb;
                }
            }
            catch (Exception)
            {
                db.Close();
                throw;
            }
        }


        /// <summary>
        /// 根据证书ID获取在途申请
        /// </summary>
        /// <param name="certificateId">证书ID</param>
        public static CertificateChangeOB GetApplyingObject(long certificateId)
        {
            const string sql = @"
			SELECT CertificateChangeID,CertificateID,ChangeType,WorkerName,Sex,Birthday,ConferDate,ValidStartDate,ValidEndDate,ConferUnit,DealWay,OldUnitAdvise,NewUnitAdvise,OldConferUnitAdvise,NewConferUnitAdvise,ApplyDate,ApplyMan,ApplyCode,[GETDATE],GetResult,GetMan,GetCode,CheckDate,CheckResult,CheckMan,CheckCode,ConfrimDate,ConfrimResult,ConfrimMan,ConfrimCode,NoticeDate,NoticeResult,NoticeMan,NoticeCode,Status,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,UnitName,NewUnitName,UnitCode,NewUnitCode,WorkerCertificateCode,LinkWay,NewWorkerCertificateCode,NewWorkerName,NewSex,NewBirthday,SheBaoCheck,IfUpdatePhoto,OldUnitCheckTime,NewUnitCheckTime,[ChangeRemark],Job,SkillLevel,ZACheckTime,ZACheckResult,ZACheckRemark,ENT_ContractType,ENT_ContractStartTime,ENT_ContractENDTime
			FROM dbo.CertificateChange
			WHERE CertificateID = @CertificateID and Status <> '已告知'";

            var db = new DBHelper();
            var p = new List<SqlParameter>
            {
                db.CreateParameter("CertificateID", DbType.Int64, certificateId)
            };
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    CertificateChangeOB certificateChangeOb = null;
                    if (reader.Read())
                    {
                        certificateChangeOb = new CertificateChangeOB();
                        if (reader["CertificateChangeID"] != DBNull.Value) certificateChangeOb.CertificateChangeID = Convert.ToInt64(reader["CertificateChangeID"]);
                        if (reader["CertificateID"] != DBNull.Value) certificateChangeOb.CertificateID = Convert.ToInt64(reader["CertificateID"]);
                        if (reader["ChangeType"] != DBNull.Value) certificateChangeOb.ChangeType = Convert.ToString(reader["ChangeType"]);
                        if (reader["WorkerName"] != DBNull.Value) certificateChangeOb.WorkerName = Convert.ToString(reader["WorkerName"]);
                        if (reader["Sex"] != DBNull.Value) certificateChangeOb.Sex = Convert.ToString(reader["Sex"]);
                        if (reader["Birthday"] != DBNull.Value) certificateChangeOb.Birthday = Convert.ToDateTime(reader["Birthday"]);
                        if (reader["ConferDate"] != DBNull.Value) certificateChangeOb.ConferDate = Convert.ToDateTime(reader["ConferDate"]);
                        if (reader["ValidStartDate"] != DBNull.Value) certificateChangeOb.ValidStartDate = Convert.ToDateTime(reader["ValidStartDate"]);
                        if (reader["ValidEndDate"] != DBNull.Value) certificateChangeOb.ValidEndDate = Convert.ToDateTime(reader["ValidEndDate"]);
                        if (reader["ConferUnit"] != DBNull.Value) certificateChangeOb.ConferUnit = Convert.ToString(reader["ConferUnit"]);
                        if (reader["DealWay"] != DBNull.Value) certificateChangeOb.DealWay = Convert.ToString(reader["DealWay"]);
                        if (reader["OldUnitAdvise"] != DBNull.Value) certificateChangeOb.OldUnitAdvise = Convert.ToString(reader["OldUnitAdvise"]);
                        if (reader["NewUnitAdvise"] != DBNull.Value) certificateChangeOb.NewUnitAdvise = Convert.ToString(reader["NewUnitAdvise"]);
                        if (reader["OldConferUnitAdvise"] != DBNull.Value) certificateChangeOb.OldConferUnitAdvise = Convert.ToString(reader["OldConferUnitAdvise"]);
                        if (reader["NewConferUnitAdvise"] != DBNull.Value) certificateChangeOb.NewConferUnitAdvise = Convert.ToString(reader["NewConferUnitAdvise"]);
                        if (reader["ApplyDate"] != DBNull.Value) certificateChangeOb.ApplyDate = Convert.ToDateTime(reader["ApplyDate"]);
                        if (reader["ApplyMan"] != DBNull.Value) certificateChangeOb.ApplyMan = Convert.ToString(reader["ApplyMan"]);
                        if (reader["ApplyCode"] != DBNull.Value) certificateChangeOb.ApplyCode = Convert.ToString(reader["ApplyCode"]);
                        if (reader["GetDate"] != DBNull.Value) certificateChangeOb.GetDate = Convert.ToDateTime(reader["GetDate"]);
                        if (reader["GetResult"] != DBNull.Value) certificateChangeOb.GetResult = Convert.ToString(reader["GetResult"]);
                        if (reader["GetMan"] != DBNull.Value) certificateChangeOb.GetMan = Convert.ToString(reader["GetMan"]);
                        if (reader["GetCode"] != DBNull.Value) certificateChangeOb.GetCode = Convert.ToString(reader["GetCode"]);
                        if (reader["CheckDate"] != DBNull.Value) certificateChangeOb.CheckDate = Convert.ToDateTime(reader["CheckDate"]);
                        if (reader["CheckResult"] != DBNull.Value) certificateChangeOb.CheckResult = Convert.ToString(reader["CheckResult"]);
                        if (reader["CheckMan"] != DBNull.Value) certificateChangeOb.CheckMan = Convert.ToString(reader["CheckMan"]);
                        if (reader["CheckCode"] != DBNull.Value) certificateChangeOb.CheckCode = Convert.ToString(reader["CheckCode"]);
                        if (reader["ConfrimDate"] != DBNull.Value) certificateChangeOb.ConfrimDate = Convert.ToDateTime(reader["ConfrimDate"]);
                        if (reader["ConfrimResult"] != DBNull.Value) certificateChangeOb.ConfrimResult = Convert.ToString(reader["ConfrimResult"]);
                        if (reader["ConfrimMan"] != DBNull.Value) certificateChangeOb.ConfrimMan = Convert.ToString(reader["ConfrimMan"]);
                        if (reader["ConfrimCode"] != DBNull.Value) certificateChangeOb.ConfrimCode = Convert.ToString(reader["ConfrimCode"]);
                        if (reader["NoticeDate"] != DBNull.Value) certificateChangeOb.NoticeDate = Convert.ToDateTime(reader["NoticeDate"]);
                        if (reader["NoticeResult"] != DBNull.Value) certificateChangeOb.NoticeResult = Convert.ToString(reader["NoticeResult"]);
                        if (reader["NoticeMan"] != DBNull.Value) certificateChangeOb.NoticeMan = Convert.ToString(reader["NoticeMan"]);
                        if (reader["NoticeCode"] != DBNull.Value) certificateChangeOb.NoticeCode = Convert.ToString(reader["NoticeCode"]);
                        if (reader["Status"] != DBNull.Value) certificateChangeOb.Status = Convert.ToString(reader["Status"]);
                        if (reader["CreatePersonID"] != DBNull.Value) certificateChangeOb.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                        if (reader["CreateTime"] != DBNull.Value) certificateChangeOb.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                        if (reader["ModifyPersonID"] != DBNull.Value) certificateChangeOb.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                        if (reader["ModifyTime"] != DBNull.Value) certificateChangeOb.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                        if (reader["UnitName"] != DBNull.Value) certificateChangeOb.UnitName = Convert.ToString(reader["UnitName"]);
                        if (reader["NewUnitName"] != DBNull.Value) certificateChangeOb.NewUnitName = Convert.ToString(reader["NewUnitName"]);
                        if (reader["UnitCode"] != DBNull.Value) certificateChangeOb.UnitCode = Convert.ToString(reader["UnitCode"]);
                        if (reader["NewUnitCode"] != DBNull.Value) certificateChangeOb.NewUnitCode = Convert.ToString(reader["NewUnitCode"]);
                        if (reader["WorkerCertificateCode"] != DBNull.Value) certificateChangeOb.WorkerCertificateCode = Convert.ToString(reader["WorkerCertificateCode"]);
                        if (reader["LinkWay"] != DBNull.Value) certificateChangeOb.LinkWay = Convert.ToString(reader["LinkWay"]);
                        if (reader["NewWorkerCertificateCode"] != DBNull.Value) certificateChangeOb.NewWorkerCertificateCode = Convert.ToString(reader["NewWorkerCertificateCode"]);
                        if (reader["NewWorkerName"] != DBNull.Value) certificateChangeOb.NewWorkerName = Convert.ToString(reader["NewWorkerName"]);
                        if (reader["NewSex"] != DBNull.Value) certificateChangeOb.NewSex = Convert.ToString(reader["NewSex"]);
                        if (reader["NewBirthday"] != DBNull.Value) certificateChangeOb.NewBirthday = Convert.ToDateTime(reader["NewBirthday"]);
                        if (reader["SheBaoCheck"] != DBNull.Value) certificateChangeOb.SheBaoCheck = Convert.ToByte(reader["SheBaoCheck"]);
                        if (reader["IfUpdatePhoto"] != DBNull.Value) certificateChangeOb.IfUpdatePhoto = Convert.ToByte(reader["IfUpdatePhoto"]);
                        if (reader["OldUnitCheckTime"] != DBNull.Value) certificateChangeOb.OldUnitCheckTime = Convert.ToDateTime(reader["OldUnitCheckTime"]);
                        if (reader["NewUnitCheckTime"] != DBNull.Value) certificateChangeOb.NewUnitCheckTime = Convert.ToDateTime(reader["NewUnitCheckTime"]);
                        if (reader["ChangeRemark"] != DBNull.Value) certificateChangeOb.ChangeRemark = Convert.ToString(reader["ChangeRemark"]);
                        if (reader["Job"] != DBNull.Value) certificateChangeOb.Job = Convert.ToString(reader["Job"]);
                        if (reader["SkillLevel"] != DBNull.Value) certificateChangeOb.SkillLevel = Convert.ToString(reader["SkillLevel"]);
                        if (reader["ZACheckTime"] != DBNull.Value) certificateChangeOb.ZACheckTime = Convert.ToDateTime(reader["ZACheckTime"]);
                        if (reader["ZACheckResult"] != DBNull.Value) certificateChangeOb.ZACheckResult = Convert.ToInt32(reader["ZACheckResult"]);
                        if (reader["ZACheckRemark"] != DBNull.Value) certificateChangeOb.ZACheckRemark = Convert.ToString(reader["ZACheckRemark"]);
                        if (reader["ENT_ContractType"] != DBNull.Value) certificateChangeOb.ENT_ContractType = Convert.ToInt32(reader["ENT_ContractType"]);
                        if (reader["ENT_ContractStartTime"] != DBNull.Value) certificateChangeOb.ENT_ContractStartTime = Convert.ToDateTime(reader["ENT_ContractStartTime"]);
                        if (reader["ENT_ContractENDTime"] != DBNull.Value) certificateChangeOb.ENT_ContractENDTime = Convert.ToDateTime(reader["ENT_ContractENDTime"]);
                    }
                    reader.Close();
                    db.Close();
                    return certificateChangeOb;
                }
            }
            catch (Exception)
            {
                db.Close();
                throw;
            }
        }

        /// <summary>
        /// 根据证书id获取单个实体
        /// </summary>
        /// <param name="certificateId">主键</param>
        public static List<CertificateChangeOB> GetListObject(long certificateId)
        {
            const string sql = @"
			SELECT CertificateChangeID,CertificateID,ChangeType,WorkerName,Sex,Birthday,ConferDate,ValidStartDate,ValidEndDate,ConferUnit,DealWay,OldUnitAdvise,NewUnitAdvise,OldConferUnitAdvise,NewConferUnitAdvise,ApplyDate,ApplyMan,ApplyCode,[GETDATE],GetResult,GetMan,GetCode,CheckDate,CheckResult,CheckMan,CheckCode,ConfrimDate,ConfrimResult,ConfrimMan,ConfrimCode,NoticeDate,NoticeResult,NoticeMan,NoticeCode,Status,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,UnitName,NewUnitName,UnitCode,NewUnitCode,WorkerCertificateCode,LinkWay,NewWorkerCertificateCode,NewWorkerName,NewSex,NewBirthday,SheBaoCheck,IfUpdatePhoto,OldUnitCheckTime,NewUnitCheckTime,[ChangeRemark],Job,SkillLevel,ZACheckTime,ZACheckResult,ZACheckRemark
			FROM dbo.CertificateChange
			WHERE CertificateID = @CertificateID";

            var db = new DBHelper();
            var listCertificateChangeOb = new List<CertificateChangeOB>();
            var p = new List<SqlParameter>
            {
                db.CreateParameter("CertificateID", DbType.Int64, certificateId)
            };
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    while (reader.Read())
                    {
                        var certificateChangeOb = new CertificateChangeOB();
                        if (reader["CertificateChangeID"] != DBNull.Value) certificateChangeOb.CertificateChangeID = Convert.ToInt64(reader["CertificateChangeID"]);
                        if (reader["CertificateID"] != DBNull.Value) certificateChangeOb.CertificateID = Convert.ToInt64(reader["CertificateID"]);
                        if (reader["ChangeType"] != DBNull.Value) certificateChangeOb.ChangeType = Convert.ToString(reader["ChangeType"]);
                        if (reader["WorkerName"] != DBNull.Value) certificateChangeOb.WorkerName = Convert.ToString(reader["WorkerName"]);
                        if (reader["Sex"] != DBNull.Value) certificateChangeOb.Sex = Convert.ToString(reader["Sex"]);
                        if (reader["Birthday"] != DBNull.Value) certificateChangeOb.Birthday = Convert.ToDateTime(reader["Birthday"]);
                        if (reader["ConferDate"] != DBNull.Value) certificateChangeOb.ConferDate = Convert.ToDateTime(reader["ConferDate"]);
                        if (reader["ValidStartDate"] != DBNull.Value) certificateChangeOb.ValidStartDate = Convert.ToDateTime(reader["ValidStartDate"]);
                        if (reader["ValidEndDate"] != DBNull.Value) certificateChangeOb.ValidEndDate = Convert.ToDateTime(reader["ValidEndDate"]);
                        if (reader["ConferUnit"] != DBNull.Value) certificateChangeOb.ConferUnit = Convert.ToString(reader["ConferUnit"]);
                        if (reader["DealWay"] != DBNull.Value) certificateChangeOb.DealWay = Convert.ToString(reader["DealWay"]);
                        if (reader["OldUnitAdvise"] != DBNull.Value) certificateChangeOb.OldUnitAdvise = Convert.ToString(reader["OldUnitAdvise"]);
                        if (reader["NewUnitAdvise"] != DBNull.Value) certificateChangeOb.NewUnitAdvise = Convert.ToString(reader["NewUnitAdvise"]);
                        if (reader["OldConferUnitAdvise"] != DBNull.Value) certificateChangeOb.OldConferUnitAdvise = Convert.ToString(reader["OldConferUnitAdvise"]);
                        if (reader["NewConferUnitAdvise"] != DBNull.Value) certificateChangeOb.NewConferUnitAdvise = Convert.ToString(reader["NewConferUnitAdvise"]);
                        if (reader["ApplyDate"] != DBNull.Value) certificateChangeOb.ApplyDate = Convert.ToDateTime(reader["ApplyDate"]);
                        if (reader["ApplyMan"] != DBNull.Value) certificateChangeOb.ApplyMan = Convert.ToString(reader["ApplyMan"]);
                        if (reader["ApplyCode"] != DBNull.Value) certificateChangeOb.ApplyCode = Convert.ToString(reader["ApplyCode"]);
                        if (reader["GetDate"] != DBNull.Value) certificateChangeOb.GetDate = Convert.ToDateTime(reader["GetDate"]);
                        if (reader["GetResult"] != DBNull.Value) certificateChangeOb.GetResult = Convert.ToString(reader["GetResult"]);
                        if (reader["GetMan"] != DBNull.Value) certificateChangeOb.GetMan = Convert.ToString(reader["GetMan"]);
                        if (reader["GetCode"] != DBNull.Value) certificateChangeOb.GetCode = Convert.ToString(reader["GetCode"]);
                        if (reader["CheckDate"] != DBNull.Value) certificateChangeOb.CheckDate = Convert.ToDateTime(reader["CheckDate"]);
                        if (reader["CheckResult"] != DBNull.Value) certificateChangeOb.CheckResult = Convert.ToString(reader["CheckResult"]);
                        if (reader["CheckMan"] != DBNull.Value) certificateChangeOb.CheckMan = Convert.ToString(reader["CheckMan"]);
                        if (reader["CheckCode"] != DBNull.Value) certificateChangeOb.CheckCode = Convert.ToString(reader["CheckCode"]);
                        if (reader["ConfrimDate"] != DBNull.Value) certificateChangeOb.ConfrimDate = Convert.ToDateTime(reader["ConfrimDate"]);
                        if (reader["ConfrimResult"] != DBNull.Value) certificateChangeOb.ConfrimResult = Convert.ToString(reader["ConfrimResult"]);
                        if (reader["ConfrimMan"] != DBNull.Value) certificateChangeOb.ConfrimMan = Convert.ToString(reader["ConfrimMan"]);
                        if (reader["ConfrimCode"] != DBNull.Value) certificateChangeOb.ConfrimCode = Convert.ToString(reader["ConfrimCode"]);
                        if (reader["NoticeDate"] != DBNull.Value) certificateChangeOb.NoticeDate = Convert.ToDateTime(reader["NoticeDate"]);
                        if (reader["NoticeResult"] != DBNull.Value) certificateChangeOb.NoticeResult = Convert.ToString(reader["NoticeResult"]);
                        if (reader["NoticeMan"] != DBNull.Value) certificateChangeOb.NoticeMan = Convert.ToString(reader["NoticeMan"]);
                        if (reader["NoticeCode"] != DBNull.Value) certificateChangeOb.NoticeCode = Convert.ToString(reader["NoticeCode"]);
                        if (reader["Status"] != DBNull.Value) certificateChangeOb.Status = Convert.ToString(reader["Status"]);
                        if (reader["CreatePersonID"] != DBNull.Value) certificateChangeOb.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                        if (reader["CreateTime"] != DBNull.Value) certificateChangeOb.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                        if (reader["ModifyPersonID"] != DBNull.Value) certificateChangeOb.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                        if (reader["ModifyTime"] != DBNull.Value) certificateChangeOb.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                        if (reader["UnitName"] != DBNull.Value) certificateChangeOb.UnitName = Convert.ToString(reader["UnitName"]);
                        if (reader["NewUnitName"] != DBNull.Value) certificateChangeOb.NewUnitName = Convert.ToString(reader["NewUnitName"]);
                        if (reader["UnitCode"] != DBNull.Value) certificateChangeOb.UnitCode = Convert.ToString(reader["UnitCode"]);
                        if (reader["NewUnitCode"] != DBNull.Value) certificateChangeOb.NewUnitCode = Convert.ToString(reader["NewUnitCode"]);
                        if (reader["WorkerCertificateCode"] != DBNull.Value) certificateChangeOb.WorkerCertificateCode = Convert.ToString(reader["WorkerCertificateCode"]);
                        if (reader["LinkWay"] != DBNull.Value) certificateChangeOb.LinkWay = Convert.ToString(reader["LinkWay"]);
                        if (reader["NewWorkerCertificateCode"] != DBNull.Value) certificateChangeOb.NewWorkerCertificateCode = Convert.ToString(reader["NewWorkerCertificateCode"]);
                        if (reader["NewWorkerName"] != DBNull.Value) certificateChangeOb.NewWorkerName = Convert.ToString(reader["NewWorkerName"]);
                        if (reader["NewSex"] != DBNull.Value) certificateChangeOb.NewSex = Convert.ToString(reader["NewSex"]);
                        if (reader["NewBirthday"] != DBNull.Value) certificateChangeOb.NewBirthday = Convert.ToDateTime(reader["NewBirthday"]);
                        if (reader["SheBaoCheck"] != DBNull.Value) certificateChangeOb.SheBaoCheck = Convert.ToByte(reader["SheBaoCheck"]);
                        if (reader["IfUpdatePhoto"] != DBNull.Value) certificateChangeOb.IfUpdatePhoto = Convert.ToByte(reader["IfUpdatePhoto"]);
                        if (reader["OldUnitCheckTime"] != DBNull.Value) certificateChangeOb.OldUnitCheckTime = Convert.ToDateTime(reader["OldUnitCheckTime"]);
                        if (reader["NewUnitCheckTime"] != DBNull.Value) certificateChangeOb.NewUnitCheckTime = Convert.ToDateTime(reader["NewUnitCheckTime"]);
                        if (reader["ChangeRemark"] != DBNull.Value) certificateChangeOb.ChangeRemark = Convert.ToString(reader["ChangeRemark"]);
                        if (reader["Job"] != DBNull.Value) certificateChangeOb.Job = Convert.ToString(reader["Job"]);
                        if (reader["SkillLevel"] != DBNull.Value) certificateChangeOb.SkillLevel = Convert.ToString(reader["SkillLevel"]);
                        if (reader["ZACheckTime"] != DBNull.Value) certificateChangeOb.ZACheckTime = Convert.ToDateTime(reader["ZACheckTime"]);
                        if (reader["ZACheckResult"] != DBNull.Value) certificateChangeOb.ZACheckResult = Convert.ToInt32(reader["ZACheckResult"]);
                        if (reader["ZACheckRemark"] != DBNull.Value) certificateChangeOb.ZACheckRemark = Convert.ToString(reader["ZACheckRemark"]);
                        listCertificateChangeOb.Add(certificateChangeOb);
                    }
                    reader.Close();
                    db.Close();
                    return listCertificateChangeOb;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.VIEW_CERTIFICATECHANGE", "*", filterWhereString, orderBy == "" ? " CertificateChangeID" : orderBy);
        }

        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.VIEW_CERTIFICATECHANGE", filterWhereString);
        }

        /// <summary>
        /// 统计查询结果批次号数量
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>批次号数量</returns>
        public static int SelectDistinctApplyCodeCount(string filterWhereString)
        {
            string sql = @"SELECT COUNT(distinct ApplyCode) FROM dbo.VIEW_CERTIFICATECHANGE WHERE 1=1 {0}";
            sql = string.Format(sql, filterWhereString);
            var db = new DBHelper();
            return Convert.ToInt32(db.ExecuteScalar(sql));
        }

        /// <summary>
        /// 证书表left变更申请表
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        public static DataTable GetListView(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.VIEW_CERTIFICATE_LEFT_CERTIFICATECHANGE", "*", filterWhereString, orderBy == "" ? " CertificateChangeID" : orderBy);
        }

        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountView(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.VIEW_CERTIFICATE_LEFT_CERTIFICATECHANGE", filterWhereString);
        }


        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        public static DataTable GetDelList(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.View_CERTIFICATECHANGE_DEL", "*", filterWhereString, orderBy == "" ? " DELTIME desc,CertificateChangeID" : orderBy);
        }

        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectDelCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.View_CERTIFICATECHANGE_DEL", filterWhereString);
        }

        /// <summary>
        /// 获取证书变更审批记录集合
        /// </summary>
        /// <param name="certificateChangeId">变更申请ID</param>
        /// <returns>审批历史记录集合</returns>
        public static DataTable GetCheckHistoryList(long certificateChangeId)
        {
            string sql = @"select row_number() over(order by ActionData, Action desc) as RowNo ,t.* from 
(
 SELECT [CHANGETYPE] +'申请' as 'Action', [APPLYDATE] as ActionData, case [STATUS] when '填报中' then '填报中' else '已提交' end as ActionResult,case [STATUS] when '填报中' then '未提交审核' else  (case when PostTypeID > 1 or CREATEPERSONID=0  then '待建委审核' when ChangeRemark ='申请强制执行' then '待建委审核' else '待单位确认' end) end as ActionRemark, [WORKERNAME] as ActionMan 
 FROM [dbo].[VIEW_CERTIFICATECHANGE] where [CERTIFICATECHANGEID]={0}  
  union all
   SELECT '原单位确认' as 'Action', [OldUnitCheckTime] as ActionData,'已确认' ActionResult,[OldUnitAdvise] as ActionRemark, [UNITNAME] as ActionMan 
 FROM [dbo].[CERTIFICATECHANGE] where [CERTIFICATECHANGEID]={0} and [OldUnitCheckTime] >'2019-1-1'
   union all
   SELECT '现单位确认' as 'Action', [NewUnitCheckTime] as ActionData,'已确认' ActionResult,[NewUnitAdvise] as ActionRemark, [NEWUNITNAME] as ActionMan 
 FROM [dbo].[CERTIFICATECHANGE] where [CERTIFICATECHANGEID]={0} and [NewUnitCheckTime] >'2019-1-1'
  union all
   SELECT '建委审核' as 'Action', [GETDATE] as ActionData,case [GETRESULT] when '通过' then '已审核' else '审核未通过' end ActionResult,[GETRESULT] as ActionRemark, [GETMAN] as ActionMan 
 FROM [dbo].[CERTIFICATECHANGE] where [CERTIFICATECHANGEID]={0} and [GETDATE] >'1950-1-1'
   union all 
	SELECT '建委决定' as 'Action', [CONFRIMDATE] as ActionData,case [CONFRIMRESULT] when '通过' then '已决定' else '决定未通过' end ActionResult,[CONFRIMRESULT] as ActionRemark, [CONFRIMMAN] as ActionMan 
 FROM [dbo].[CERTIFICATECHANGE] where [CERTIFICATECHANGEID]={0} and [CONFRIMDATE] >'1950-1-1'
   union all 
    SELECT '住建部核准' as 'Action', 
         case when b.[CHANGETYPE] = '注销' or b.[CHANGETYPE] = '离京变更' then
			        case when b.[CONFRIMDATE] <= c.QRCodeTime then  c.QRCodeTime
			             when c.EleCertErrTime > b.CONFRIMDATE then c.EleCertErrTime
				         else null
			        end
              else
	                case when c.ZZUrlUpTime > b.CONFRIMDATE then  dateadd(hour,1,b.[CONFRIMDATE]) 
			                when  Convert(varchar(10),b.[CONFRIMDATE],21) = Convert(varchar(10),c.CHECKDATE,21) and  c.EleCertErrTime > b.[CONFRIMDATE] then c.EleCertErrTime
			                else null
	                end
              end as ActionData,
         case when b.[CHANGETYPE] = '注销' or b.[CHANGETYPE] = '离京变更' then
			        case when b.[CONFRIMDATE] <= c.QRCodeTime then  '已核准'
			             when c.EleCertErrTime > b.CONFRIMDATE then '核准未通过'
				         else null
			        end
                else
	                case when c.ZZUrlUpTime > b.CONFRIMDATE then '已核准'
			                when  Convert(varchar(10),b.[CONFRIMDATE],21) = Convert(varchar(10),c.CHECKDATE,21) and  c.EleCertErrTime > b.[CONFRIMDATE] then '核准未通过'
			                else null
	                end
                end ActionResult,
         case when b.[CHANGETYPE] = '注销' or b.[CHANGETYPE] = '离京变更' then
			        case when b.[CONFRIMDATE] <= c.QRCodeTime then  '已上报（办结）'
			             when c.EleCertErrTime > b.CONFRIMDATE then c.[EleCertErrDesc]
				         else null
			        end
                else
	                case when c.ZZUrlUpTime > b.CONFRIMDATE then  '已生成电子证书（办结）' 
			                when  Convert(varchar(10),b.[CONFRIMDATE],21) = Convert(varchar(10),c.CHECKDATE,21) and  c.EleCertErrTime > b.[CONFRIMDATE] then '审批不通过，未生成电子证书。' + c.[EleCertErrDesc]
			                else null
	                end
                end  as ActionRemark, 
         '住建部' as ActionMan 
     FROM [dbo].[CERTIFICATECHANGE] b
     inner join [dbo].[CERTIFICATE] c on b.CERTIFICATEID = c.CERTIFICATEID
     where b.[CERTIFICATECHANGEID]={0} and 
     b.[CONFRIMDATE] >'1950-1-1' 
     and (
	    (
		    (b.[CHANGETYPE] = '注销' or b.[CHANGETYPE] = '离京变更') 
		    and  (c.QRCodeTime > b.[CONFRIMDATE] or (c.EleCertErrTime > b.CONFRIMDATE and Convert(varchar(10),b.[CONFRIMDATE],21) = Convert(varchar(10),c.CHECKDATE,21)))
	    )
	    or
	    (
		    b.[CHANGETYPE] = '京内变更' 
		    and (c.ZZUrlUpTime > b.CONFRIMDATE 
			    or (c.EleCertErrTime > b.CONFRIMDATE and Convert(varchar(10),b.[CONFRIMDATE],21) = Convert(varchar(10),c.CHECKDATE,21) )))

     )
) t";
            return CommonDAL.GetDataTable(string.Format(sql, certificateChangeId));
        }

        /// <summary>
        /// 获取证书变更审批记录集合（对个人和企业，业务决定后，住建部校验不通过前3天显示等待，不显示不通过原因）
        /// </summary>
        /// <param name="certificateChangeId">变更申请ID</param>
        /// <returns>审批历史记录集合</returns>
        public static DataTable GetCheckHistoryListForGRQY(long certificateChangeId)
        {
            string sql = @"select row_number() over(order by ActionData, Action desc) as RowNo ,t.* from 
(
 SELECT [CHANGETYPE] +'申请' as 'Action', [APPLYDATE] as ActionData, case [STATUS] when '填报中' then '填报中' else '已提交' end as ActionResult,case [STATUS] when '填报中' then '未提交审核' else  (case when PostTypeID > 1 or CREATEPERSONID=0  then '待建委审核' when ChangeRemark ='申请强制执行' then '待建委审核' else '待单位确认' end) end as ActionRemark, [WORKERNAME] as ActionMan 
 FROM [dbo].[VIEW_CERTIFICATECHANGE] where [CERTIFICATECHANGEID]={0}  
  union all
   SELECT '原单位确认' as 'Action', [OldUnitCheckTime] as ActionData,'已确认' ActionResult,[OldUnitAdvise] as ActionRemark, [UNITNAME] as ActionMan 
 FROM [dbo].[CERTIFICATECHANGE] where [CERTIFICATECHANGEID]={0} and [OldUnitCheckTime] >'2019-1-1'
   union all
   SELECT '现单位确认' as 'Action', [NewUnitCheckTime] as ActionData,'已确认' ActionResult,[NewUnitAdvise] as ActionRemark, [NEWUNITNAME] as ActionMan 
 FROM [dbo].[CERTIFICATECHANGE] where [CERTIFICATECHANGEID]={0} and [NewUnitCheckTime] >'2019-1-1'
  union all
   SELECT '建委审核' as 'Action', [GETDATE] as ActionData,case [GETRESULT] when '通过' then '已审核' else '审核未通过' end ActionResult,[GETRESULT] as ActionRemark, [GETMAN] as ActionMan 
 FROM [dbo].[CERTIFICATECHANGE] where [CERTIFICATECHANGEID]={0} and [GETDATE] >'1950-1-1'
   union all 
	SELECT '建委决定' as 'Action', [CONFRIMDATE] as ActionData,case [CONFRIMRESULT] when '通过' then '已决定' else '决定未通过' end ActionResult,[CONFRIMRESULT] as ActionRemark, [CONFRIMMAN] as ActionMan 
 FROM [dbo].[CERTIFICATECHANGE] where [CERTIFICATECHANGEID]={0} and [CONFRIMDATE] >'1950-1-1'
   union all 
    SELECT '住建部核准' as 'Action', 
         case when b.[CHANGETYPE] = '注销' or b.[CHANGETYPE] = '离京变更' then
			        case when b.[CONFRIMDATE] <= c.QRCodeTime then  c.QRCodeTime
			             when c.EleCertErrTime > b.CONFRIMDATE then c.EleCertErrTime
				         else null
			        end
              else
	                case when c.ZZUrlUpTime > b.CONFRIMDATE then  dateadd(hour,1,b.[CONFRIMDATE]) 
			                when  Convert(varchar(10),b.[CONFRIMDATE],21) = Convert(varchar(10),c.CHECKDATE,21) and  c.EleCertErrTime > b.[CONFRIMDATE] and b.[CONFRIMDATE] < dateadd(day,-2,getdate()) then c.EleCertErrTime 
			                else null
	                end
              end as ActionData,
         case when b.[CHANGETYPE] = '注销' or b.[CHANGETYPE] = '离京变更' then
			        case when b.[CONFRIMDATE] <= c.QRCodeTime then  '已核准'
			             when c.EleCertErrTime > b.CONFRIMDATE then '核准未通过'
				         else null
			        end
                else
	                case when c.ZZUrlUpTime > b.CONFRIMDATE then '已核准'
			                when  Convert(varchar(10),b.[CONFRIMDATE],21) = Convert(varchar(10),c.CHECKDATE,21) and  c.EleCertErrTime > b.[CONFRIMDATE] and b.[CONFRIMDATE] < dateadd(day,-2,getdate()) then '核准未通过' 
			                else null
	                end
                end ActionResult,
         case when b.[CHANGETYPE] = '注销' or b.[CHANGETYPE] = '离京变更' then
			        case when b.[CONFRIMDATE] <= c.QRCodeTime then  '已上报（办结）'
			             when c.EleCertErrTime > b.CONFRIMDATE then c.[EleCertErrDesc]
				         else null
			        end
                else
	                case when c.ZZUrlUpTime > b.CONFRIMDATE then  '已生成电子证书（办结）' 
			            when  Convert(varchar(10),b.[CONFRIMDATE],21) = Convert(varchar(10),c.CHECKDATE,21) and  c.EleCertErrTime > b.[CONFRIMDATE]  and b.[CONFRIMDATE] < dateadd(day,-2,getdate()) then '审批不通过，未生成电子证书。' + c.[EleCertErrDesc]
                        when  Convert(varchar(10),b.[CONFRIMDATE],21) = Convert(varchar(10),c.CHECKDATE,21) and  c.EleCertErrTime > b.[CONFRIMDATE]  then '住建部正在进行数据校验核对，生成电子证书需要1-3个工作日，请您耐心等待。'
			            else null
	                end
                end  as ActionRemark, 
         '住建部' as ActionMan 
     FROM [dbo].[CERTIFICATECHANGE] b
     inner join [dbo].[CERTIFICATE] c on b.CERTIFICATEID = c.CERTIFICATEID
     where b.[CERTIFICATECHANGEID]={0} and 
     b.[CONFRIMDATE] >'1950-1-1' 
     and (
	    (
		    (b.[CHANGETYPE] = '注销' or b.[CHANGETYPE] = '离京变更') 
		    and  (c.QRCodeTime > b.[CONFRIMDATE] or (c.EleCertErrTime > b.CONFRIMDATE and Convert(varchar(10),b.[CONFRIMDATE],21) = Convert(varchar(10),c.CHECKDATE,21)))
	    )
	    or
	    (
		    b.[CHANGETYPE] = '京内变更' 
		    and (c.ZZUrlUpTime > b.CONFRIMDATE 
			    or (c.EleCertErrTime > b.CONFRIMDATE and Convert(varchar(10),b.[CONFRIMDATE],21) = Convert(varchar(10),c.CHECKDATE,21) )))

     )
) t";
            return CommonDAL.GetDataTable(string.Format(sql, certificateChangeId));
        }

        /// <summary>
        /// 获取证书近1年内变更单位次数
        /// </summary>
        /// <param name="CertificateID">证书ID</param>
        /// <returns>次数</returns>
        public static int SelectChangeUnitCountYear(long CertificateID)
        {
            return CommonDAL.SelectRowCount(string.Format(@"
            select count(*) 
            from  dbo.CertificateChange 
            where CertificateID={0} and (NoticeDate between dateadd(year,-1,getdate()) and getdate()) 
                and NoticeResult='通过' and  ChangeType='京内变更'  and [UNITCODE] <> [NEWUNITCODE]", CertificateID));
        }
    }
}