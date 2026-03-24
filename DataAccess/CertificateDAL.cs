using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
    /// <summary>
    /// 业务类实现--CertificateDAL(填写类描述)
    /// </summary>
    public class CertificateDAL
    {
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="certificateOb">对象实体类</param>
        /// <returns></returns>
        public static long Insert(CertificateOB certificateOb)
        {
            return Insert(null, certificateOb);
        }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="certificateOb">对象实体类</param>
        /// <returns></returns>
        public static long Insert(DbTransaction tran, CertificateOB certificateOb)
        {
            DBHelper db = new DBHelper();

            string sql = @"
			INSERT INTO dbo.Certificate(ExamPlanID,WorkerID,CertificateType,PostTypeID,PostID,CertificateCode,WorkerName,Sex,Birthday,UnitName,ConferDate,ValidStartDate,ValidEndDate,ConferUnit,Status,CheckMan,CheckAdvise,CheckDate,PrintMan,PrintDate,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,WorkerCertificateCode,UnitCode,ApplyMan,CaseStatus,AddItemName,Remark,SkillLevel,TrainUnitName,PrintCount,FacePhoto,PrintVer,PostTypeName,PostName,Job)
			VALUES (@ExamPlanID,@WorkerID,@CertificateType,@PostTypeID,@PostID,@CertificateCode,@WorkerName,@Sex,@Birthday,@UnitName,@ConferDate,@ValidStartDate,@ValidEndDate,@ConferUnit,@Status,@CheckMan,@CheckAdvise,@CheckDate,@PrintMan,@PrintDate,@CreatePersonID,@CreateTime,@ModifyPersonID,@ModifyTime,@WorkerCertificateCode,@UnitCode,@ApplyMan,@CaseStatus,@AddItemName,@Remark,@SkillLevel,@TrainUnitName,@PrintCount,@FacePhoto,@PrintVer,@PostTypeName,@PostName,@Job);SELECT @CertificateID = @@IDENTITY";

            List<SqlParameter> p = new List<SqlParameter>
            {
                db.CreateOutParameter("CertificateID", DbType.Int64),
                db.CreateParameter("ExamPlanID", DbType.Int64, certificateOb.ExamPlanID),
                db.CreateParameter("WorkerID", DbType.Int64, certificateOb.WorkerID),
                db.CreateParameter("CertificateType", DbType.String, certificateOb.CertificateType),
                db.CreateParameter("PostTypeID", DbType.Int32, certificateOb.PostTypeID),
                db.CreateParameter("PostID", DbType.Int32, certificateOb.PostID),
                db.CreateParameter("CertificateCode", DbType.String, certificateOb.CertificateCode),
                db.CreateParameter("WorkerName", DbType.String, certificateOb.WorkerName),
                db.CreateParameter("Sex", DbType.String, certificateOb.Sex),
                db.CreateParameter("Birthday", DbType.DateTime, certificateOb.Birthday),
                db.CreateParameter("UnitName", DbType.String, certificateOb.UnitName),
                db.CreateParameter("ConferDate", DbType.DateTime, certificateOb.ConferDate),
                db.CreateParameter("ValidStartDate", DbType.DateTime, certificateOb.ValidStartDate),
                db.CreateParameter("ValidEndDate", DbType.DateTime, certificateOb.ValidEndDate),
                db.CreateParameter("ConferUnit", DbType.String, certificateOb.ConferUnit),
                db.CreateParameter("Status", DbType.String, certificateOb.Status),
                db.CreateParameter("CheckMan", DbType.String, certificateOb.CheckMan),
                db.CreateParameter("CheckAdvise", DbType.String, certificateOb.CheckAdvise),
                db.CreateParameter("CheckDate", DbType.DateTime, certificateOb.CheckDate),
                db.CreateParameter("PrintMan", DbType.String, certificateOb.PrintMan),
                db.CreateParameter("PrintDate", DbType.DateTime, certificateOb.PrintDate),
                db.CreateParameter("CreatePersonID", DbType.Int64, certificateOb.CreatePersonID),
                db.CreateParameter("CreateTime", DbType.DateTime, certificateOb.CreateTime),
                db.CreateParameter("ModifyPersonID", DbType.Int64, certificateOb.ModifyPersonID),
                db.CreateParameter("ModifyTime", DbType.DateTime, certificateOb.ModifyTime),
                db.CreateParameter("WorkerCertificateCode", DbType.String, certificateOb.WorkerCertificateCode),
                db.CreateParameter("UnitCode", DbType.String, certificateOb.UnitCode),
                db.CreateParameter("ApplyMan", DbType.String, certificateOb.ApplyMan),
                db.CreateParameter("CaseStatus", DbType.String, certificateOb.CaseStatus),
                db.CreateParameter("AddItemName", DbType.String, certificateOb.AddItemName),
                db.CreateParameter("Remark", DbType.String, certificateOb.Remark),
                db.CreateParameter("SkillLevel", DbType.String, certificateOb.SkillLevel),
                db.CreateParameter("TrainUnitName", DbType.String, certificateOb.TrainUnitName),
                db.CreateParameter("PrintCount", DbType.Int32, certificateOb.PrintCount),                
                db.CreateParameter("FacePhoto", DbType.String, certificateOb.FacePhoto),
                db.CreateParameter("PrintVer", DbType.Int32, certificateOb.PrintVer),
                db.CreateParameter("PostTypeName",DbType.String, certificateOb.PostTypeName),
                db.CreateParameter("PostName",DbType.String, certificateOb.PostName),
			    db.CreateParameter("Job",DbType.String, certificateOb.Job)
            };

            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            certificateOb.CertificateID = Convert.ToInt64(p[0].Value);
            return rtn;
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="certificateOb">对象实体类</param>
        /// <returns></returns>
        public static int Update(CertificateOB certificateOb)
        {
            return Update(null, certificateOb);
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="certificateOb">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, CertificateOB certificateOb)
        {
            const string sql = @"
			UPDATE dbo.Certificate
				SET	ExamPlanID = @ExamPlanID,WorkerID = @WorkerID,CertificateType = @CertificateType,PostTypeID = @PostTypeID,PostID = @PostID,CertificateCode = @CertificateCode,WorkerName = @WorkerName,Sex = @Sex,Birthday = @Birthday,UnitName = @UnitName,ConferDate = @ConferDate,ValidStartDate = @ValidStartDate,ValidEndDate = @ValidEndDate,ConferUnit = @ConferUnit,Status = @Status,CheckMan = @CheckMan,CheckAdvise = @CheckAdvise,CheckDate = @CheckDate,PrintMan = @PrintMan,PrintDate = @PrintDate,CreatePersonID = @CreatePersonID,CreateTime = @CreateTime,ModifyPersonID = @ModifyPersonID,ModifyTime = @ModifyTime,WorkerCertificateCode = @WorkerCertificateCode,UnitCode = @UnitCode,ApplyMan= @ApplyMan,CaseStatus = @CaseStatus,AddItemName = @AddItemName,Remark= @Remark,SkillLevel= @SkillLevel,TrainUnitName= @TrainUnitName,PrintCount= @PrintCount,FacePhoto= @FacePhoto,PrintVer= @PrintVer,PostTypeName = @PostTypeName,PostName = @PostName,Job =@Job
			WHERE
				CertificateID = @CertificateID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("CertificateID", DbType.Int64, certificateOb.CertificateID));
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, certificateOb.ExamPlanID));
            p.Add(db.CreateParameter("WorkerID", DbType.Int64, certificateOb.WorkerID));
            p.Add(db.CreateParameter("CertificateType", DbType.String, certificateOb.CertificateType));
            p.Add(db.CreateParameter("PostTypeID", DbType.Int32, certificateOb.PostTypeID));
            p.Add(db.CreateParameter("PostID", DbType.Int32, certificateOb.PostID));
            p.Add(db.CreateParameter("CertificateCode", DbType.String, certificateOb.CertificateCode));
            p.Add(db.CreateParameter("WorkerName", DbType.String, certificateOb.WorkerName));
            p.Add(db.CreateParameter("Sex", DbType.String, certificateOb.Sex));
            p.Add(db.CreateParameter("Birthday", DbType.DateTime, certificateOb.Birthday));
            p.Add(db.CreateParameter("UnitName", DbType.String, certificateOb.UnitName));
            p.Add(db.CreateParameter("ConferDate", DbType.DateTime, certificateOb.ConferDate));
            p.Add(db.CreateParameter("ValidStartDate", DbType.DateTime, certificateOb.ValidStartDate));
            p.Add(db.CreateParameter("ValidEndDate", DbType.DateTime, certificateOb.ValidEndDate));
            p.Add(db.CreateParameter("ConferUnit", DbType.String, certificateOb.ConferUnit));
            p.Add(db.CreateParameter("Status", DbType.String, certificateOb.Status));
            p.Add(db.CreateParameter("CheckMan", DbType.String, certificateOb.CheckMan));
            p.Add(db.CreateParameter("CheckAdvise", DbType.String, certificateOb.CheckAdvise));
            p.Add(db.CreateParameter("CheckDate", DbType.DateTime, certificateOb.CheckDate));
            p.Add(db.CreateParameter("PrintMan", DbType.String, certificateOb.PrintMan));
            p.Add(db.CreateParameter("PrintDate", DbType.DateTime, certificateOb.PrintDate));
            p.Add(db.CreateParameter("CreatePersonID", DbType.Int64, certificateOb.CreatePersonID));
            p.Add(db.CreateParameter("CreateTime", DbType.DateTime, certificateOb.CreateTime));
            p.Add(db.CreateParameter("ModifyPersonID", DbType.Int64, certificateOb.ModifyPersonID));
            p.Add(db.CreateParameter("ModifyTime", DbType.DateTime, certificateOb.ModifyTime));
            p.Add(db.CreateParameter("WorkerCertificateCode", DbType.String, certificateOb.WorkerCertificateCode));
            p.Add(db.CreateParameter("UnitCode", DbType.String, certificateOb.UnitCode));
            p.Add(db.CreateParameter("ApplyMan", DbType.String, certificateOb.ApplyMan));
            p.Add(db.CreateParameter("CaseStatus", DbType.String, certificateOb.CaseStatus));
            p.Add(db.CreateParameter("AddItemName", DbType.String, certificateOb.AddItemName));
            p.Add(db.CreateParameter("Remark", DbType.String, certificateOb.Remark));
            p.Add(db.CreateParameter("SkillLevel", DbType.String, certificateOb.SkillLevel));
            p.Add(db.CreateParameter("TrainUnitName", DbType.String, certificateOb.TrainUnitName));
            p.Add(db.CreateParameter("PrintCount", DbType.Int32, certificateOb.PrintCount));
            p.Add(db.CreateParameter("FacePhoto", DbType.String, certificateOb.FacePhoto));
            p.Add(db.CreateParameter("PrintVer", DbType.Int32, certificateOb.PrintVer));
            p.Add(db.CreateParameter("PostTypeName", DbType.String, certificateOb.PostTypeName));
            p.Add(db.CreateParameter("PostName", DbType.String, certificateOb.PostName));
            p.Add(db.CreateParameter("Job", DbType.String, certificateOb.Job));

            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 更新打印时间和打印人
        /// </summary>
        /// <param name="certificateId">证书ID</param>
        /// <param name="printMan">打印人</param>
        /// <param name="printDate">打印时间</param>
        /// <param name="Ver">打印版本号</param>
        /// <returns></returns>
        public static int UpdatePrintTime(long certificateId, string printMan, DateTime printDate, int Ver)
        {
            const string sql = @"
			UPDATE dbo.Certificate
				SET	PrintMan = @PrintMan,PrintDate = @PrintDate,PrintVer =@PrintVer
			WHERE
				CertificateID = @CertificateID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>
            {
                db.CreateParameter("CertificateID", DbType.Int64, certificateId),
                db.CreateParameter("PrintMan", DbType.String, printMan),
                db.CreateParameter("PrintDate", DbType.DateTime, printDate),
                db.CreateParameter("PrintVer", DbType.Int32, Ver)
            };

            return db.ExcuteNonQuery(sql, p.ToArray());
        }

        /// <summary>
        /// 批量更新增项后证书
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="examPlanId">考试计划ID</param>
        /// <param name="postId">证书工种ID</param>
        /// <param name="modifyPersonId">修改人</param>
        /// <param name="modifyTime">修改时间</param>
        /// <param name="addItemName">增项名称</param>
        /// <returns></returns>
        public static int UpdateAddItem(DbTransaction tran, long examPlanId, int postId, long modifyPersonId, DateTime modifyTime, string addItemName)
        {
            string sql = @"UPDATE dbo.Certificate set ModifyPersonID = @ModifyPersonID,ModifyTime = @ModifyTime,AddItemName = @AddItemName
 WHERE POSTID=@POSTID and validenddate >=getdate()
and [STATUS] <>'注销' and [STATUS] <>'离京变更' and additemname is null and workercertificatecode in (select workercertificatecode from
DBO.VIEW_EXAMSCORE where ExamPlanID=@ExamPlanID and Status = '成绩已公告' and ExamResult = '合格' )";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, examPlanId));
            p.Add(db.CreateParameter("PostID", DbType.Int32, postId));
            p.Add(db.CreateParameter("ModifyPersonID", DbType.Int64, modifyPersonId));
            p.Add(db.CreateParameter("ModifyTime", DbType.DateTime, modifyTime));
            p.Add(db.CreateParameter("AddItemName", DbType.String, addItemName));

            return db.ExcuteNonQuery(sql, p.ToArray());
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="certificateId">主键</param>
        /// <returns></returns>
        public static int Delete(long certificateId)
        {
            return Delete(null, certificateId);
        }

        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="certificateId">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, long certificateId)
        {
            string sql = @"DELETE FROM dbo.Certificate WHERE CertificateID = @CertificateID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("CertificateID", DbType.Int64, certificateId));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="certificateOb">对象实体类</param>
        /// <returns></returns>
        public static int Delete(CertificateOB certificateOb)
        {
            return Delete(null, certificateOb);
        }

        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="certificateOb">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, CertificateOB certificateOb)
        {
            string sql = @"DELETE FROM dbo.Certificate WHERE CertificateID = @CertificateID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("CertificateID", DbType.Int64, certificateOb.CertificateID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="certificateId">主键</param>
        public static CertificateOB GetObject(long certificateId)
        {
            return GetObject(null, certificateId);
        }

        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="certificateId">主键</param>
        public static CertificateOB GetObject(DbTransaction tran, long certificateId)
        {
            const string sql = @"
			SELECT CertificateID,ExamPlanID,WorkerID,CertificateType,PostTypeID,PostID,CertificateCode,WorkerName,Sex,Birthday,UnitName,ConferDate,ValidStartDate,ValidEndDate,ConferUnit,Status,CheckMan,CheckAdvise,CheckDate,PrintMan,PrintDate,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,WorkerCertificateCode,UnitCode,ApplyMan,CaseStatus,AddItemName,Remark,SkillLevel,TrainUnitName,PrintCount,FacePhoto,PrintVer,PostTypeName,PostName,ApplyCATime,ReturnCATime,SendCATime,WriteJHKCATime,CertificateCAID,Job,ZACheckTime,ZACheckResult,ZACheckRemark,[EleCertErrTime],[EleCertErrStep],[EleCertErrDesc],QRCodeTime,ZZUrlUpTime,Ofd_ReturnCATime
			FROM dbo.Certificate
			WHERE CertificateID = @CertificateID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("CertificateID", DbType.Int64, certificateId));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(tran, sql, p.ToArray()))
                {
                    CertificateOB certificateOb = null;
                    if (reader.Read())
                    {
                        certificateOb = new CertificateOB();
                        if (reader["CertificateID"] != DBNull.Value) certificateOb.CertificateID = Convert.ToInt64(reader["CertificateID"]);
                        if (reader["ExamPlanID"] != DBNull.Value) certificateOb.ExamPlanID = Convert.ToInt64(reader["ExamPlanID"]);
                        if (reader["WorkerID"] != DBNull.Value) certificateOb.WorkerID = Convert.ToInt64(reader["WorkerID"]);
                        if (reader["CertificateType"] != DBNull.Value) certificateOb.CertificateType = Convert.ToString(reader["CertificateType"]);
                        if (reader["PostTypeID"] != DBNull.Value) certificateOb.PostTypeID = Convert.ToInt32(reader["PostTypeID"]);
                        if (reader["PostID"] != DBNull.Value) certificateOb.PostID = Convert.ToInt32(reader["PostID"]);
                        if (reader["CertificateCode"] != DBNull.Value) certificateOb.CertificateCode = Convert.ToString(reader["CertificateCode"]);
                        if (reader["WorkerName"] != DBNull.Value) certificateOb.WorkerName = Convert.ToString(reader["WorkerName"]);
                        if (reader["Sex"] != DBNull.Value) certificateOb.Sex = Convert.ToString(reader["Sex"]);
                        if (reader["Birthday"] != DBNull.Value) certificateOb.Birthday = Convert.ToDateTime(reader["Birthday"]);
                        if (reader["UnitName"] != DBNull.Value) certificateOb.UnitName = Convert.ToString(reader["UnitName"]);
                        if (reader["ConferDate"] != DBNull.Value) certificateOb.ConferDate = Convert.ToDateTime(reader["ConferDate"]);
                        if (reader["ValidStartDate"] != DBNull.Value) certificateOb.ValidStartDate = Convert.ToDateTime(reader["ValidStartDate"]);
                        if (reader["ValidEndDate"] != DBNull.Value) certificateOb.ValidEndDate = Convert.ToDateTime(reader["ValidEndDate"]);
                        if (reader["ConferUnit"] != DBNull.Value) certificateOb.ConferUnit = Convert.ToString(reader["ConferUnit"]);
                        if (reader["Status"] != DBNull.Value) certificateOb.Status = Convert.ToString(reader["Status"]);
                        if (reader["CheckMan"] != DBNull.Value) certificateOb.CheckMan = Convert.ToString(reader["CheckMan"]);
                        if (reader["CheckAdvise"] != DBNull.Value) certificateOb.CheckAdvise = Convert.ToString(reader["CheckAdvise"]);
                        if (reader["CheckDate"] != DBNull.Value) certificateOb.CheckDate = Convert.ToDateTime(reader["CheckDate"]);
                        if (reader["PrintMan"] != DBNull.Value) certificateOb.PrintMan = Convert.ToString(reader["PrintMan"]);
                        if (reader["PrintDate"] != DBNull.Value) certificateOb.PrintDate = Convert.ToDateTime(reader["PrintDate"]);
                        if (reader["CreatePersonID"] != DBNull.Value) certificateOb.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                        if (reader["CreateTime"] != DBNull.Value) certificateOb.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                        if (reader["ModifyPersonID"] != DBNull.Value) certificateOb.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                        if (reader["ModifyTime"] != DBNull.Value) certificateOb.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                        if (reader["WorkerCertificateCode"] != DBNull.Value) certificateOb.WorkerCertificateCode = Convert.ToString(reader["WorkerCertificateCode"]);
                        if (reader["UnitCode"] != DBNull.Value) certificateOb.UnitCode = Convert.ToString(reader["UnitCode"]);
                        if (reader["ApplyMan"] != DBNull.Value) certificateOb.ApplyMan = Convert.ToString(reader["ApplyMan"]);
                        if (reader["CaseStatus"] != DBNull.Value) certificateOb.CaseStatus = Convert.ToString(reader["CaseStatus"]);
                        if (reader["AddItemName"] != DBNull.Value) certificateOb.AddItemName = Convert.ToString(reader["AddItemName"]);
                        if (reader["Remark"] != DBNull.Value) certificateOb.Remark = Convert.ToString(reader["Remark"]);
                        if (reader["SkillLevel"] != DBNull.Value) certificateOb.SkillLevel = Convert.ToString(reader["SkillLevel"]);
                        if (reader["TrainUnitName"] != DBNull.Value) certificateOb.TrainUnitName = Convert.ToString(reader["TrainUnitName"]);
                        if (reader["PrintCount"] != DBNull.Value) certificateOb.PrintCount = Convert.ToInt32(reader["PrintCount"]);
                        if (reader["FacePhoto"] != DBNull.Value) certificateOb.FacePhoto = Convert.ToString(reader["FacePhoto"]);
                        if (reader["PrintVer"] != DBNull.Value) certificateOb.PrintVer = Convert.ToInt32(reader["PrintVer"]);
                        if (reader["PostTypeName"] != DBNull.Value) certificateOb.PostTypeName = Convert.ToString(reader["PostTypeName"]);
                        if (reader["PostName"] != DBNull.Value) certificateOb.PostName = Convert.ToString(reader["PostName"]);
                        if (reader["ApplyCATime"] != DBNull.Value) certificateOb.ApplyCATime = Convert.ToDateTime(reader["ApplyCATime"]);
                        if (reader["ReturnCATime"] != DBNull.Value) certificateOb.ReturnCATime = Convert.ToDateTime(reader["ReturnCATime"]);
                        if (reader["SendCATime"] != DBNull.Value) certificateOb.SendCATime = Convert.ToDateTime(reader["SendCATime"]);
                        if (reader["WriteJHKCATime"] != DBNull.Value) certificateOb.WriteJHKCATime = Convert.ToDateTime(reader["WriteJHKCATime"]);
                        if (reader["CertificateCAID"] != DBNull.Value) certificateOb.CertificateCAID = Convert.ToString(reader["CertificateCAID"]);
                        if (reader["Job"] != DBNull.Value) certificateOb.Job = Convert.ToString(reader["Job"]);
                        if (reader["ZACheckTime"] != DBNull.Value) certificateOb.ZACheckTime = Convert.ToDateTime(reader["ZACheckTime"]);
                        if (reader["ZACheckResult"] != DBNull.Value) certificateOb.ZACheckResult = Convert.ToInt32(reader["ZACheckResult"]);
                        if (reader["ZACheckRemark"] != DBNull.Value) certificateOb.ZACheckRemark = Convert.ToString(reader["ZACheckRemark"]);
                        if (reader["EleCertErrTime"] != DBNull.Value) certificateOb.EleCertErrTime = Convert.ToDateTime(reader["EleCertErrTime"]);
                        if (reader["EleCertErrStep"] != DBNull.Value) certificateOb.EleCertErrStep = Convert.ToString(reader["EleCertErrStep"]);
                        if (reader["EleCertErrDesc"] != DBNull.Value) certificateOb.EleCertErrDesc = Convert.ToString(reader["EleCertErrDesc"]);
                        if (reader["QRCodeTime"] != DBNull.Value) certificateOb.QRCodeTime = Convert.ToDateTime(reader["QRCodeTime"]);
                        if (reader["ZZUrlUpTime"] != DBNull.Value) certificateOb.ZZUrlUpTime = Convert.ToDateTime(reader["ZZUrlUpTime"]);
                        if (reader["Ofd_ReturnCATime"] != DBNull.Value) certificateOb.Ofd_ReturnCATime = Convert.ToDateTime(reader["Ofd_ReturnCATime"]);
                    }
                    reader.Close();
                    if (tran == null) db.Close();
                    return certificateOb;
                }
            }
            catch (Exception)
            {
                if (tran == null) db.Close();
                throw;
            }
        }


        /// <summary>
        /// 根据身份证号获取单个实体
        /// </summary>
        public static CertificateOB GetObject(string workercertificatecode)
        {
            return GetObject(null, workercertificatecode);
        }

        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="workercertificatecode">身份证号</param>
        public static CertificateOB GetObject(DbTransaction tran, string workercertificatecode)
        {
            const string sql = @"
			SELECT CertificateID,ExamPlanID,WorkerID,CertificateType,PostTypeID,PostID,CertificateCode,WorkerName,Sex,Birthday,UnitName,ConferDate,ValidStartDate,ValidEndDate,ConferUnit,Status,CheckMan,CheckAdvise,CheckDate,PrintMan,PrintDate,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,WorkerCertificateCode,UnitCode,ApplyMan,CaseStatus,AddItemName,Remark,SkillLevel,TrainUnitName,PrintCount,FacePhoto,PrintVer,PostTypeName,PostName,ApplyCATime,ReturnCATime,SendCATime,WriteJHKCATime,CertificateCAID,Job,ZACheckTime,ZACheckResult,ZACheckRemark,[EleCertErrTime],[EleCertErrStep],[EleCertErrDesc],QRCodeTime,ZZUrlUpTime,Ofd_ReturnCATime
			FROM dbo.Certificate
			WHERE workercertificatecode = @workercertificatecode";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("WorkerCertificateCode", DbType.String, workercertificatecode));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(tran, sql, p.ToArray()))
                {
                    CertificateOB certificateOb = null;
                    if (reader.Read())
                    {
                        certificateOb = new CertificateOB();
                        if (reader["CertificateID"] != DBNull.Value) certificateOb.CertificateID = Convert.ToInt64(reader["CertificateID"]);
                        if (reader["ExamPlanID"] != DBNull.Value) certificateOb.ExamPlanID = Convert.ToInt64(reader["ExamPlanID"]);
                        if (reader["WorkerID"] != DBNull.Value) certificateOb.WorkerID = Convert.ToInt64(reader["WorkerID"]);
                        if (reader["CertificateType"] != DBNull.Value) certificateOb.CertificateType = Convert.ToString(reader["CertificateType"]);
                        if (reader["PostTypeID"] != DBNull.Value) certificateOb.PostTypeID = Convert.ToInt32(reader["PostTypeID"]);
                        if (reader["PostID"] != DBNull.Value) certificateOb.PostID = Convert.ToInt32(reader["PostID"]);
                        if (reader["CertificateCode"] != DBNull.Value) certificateOb.CertificateCode = Convert.ToString(reader["CertificateCode"]);
                        if (reader["WorkerName"] != DBNull.Value) certificateOb.WorkerName = Convert.ToString(reader["WorkerName"]);
                        if (reader["Sex"] != DBNull.Value) certificateOb.Sex = Convert.ToString(reader["Sex"]);
                        if (reader["Birthday"] != DBNull.Value) certificateOb.Birthday = Convert.ToDateTime(reader["Birthday"]);
                        if (reader["UnitName"] != DBNull.Value) certificateOb.UnitName = Convert.ToString(reader["UnitName"]);
                        if (reader["ConferDate"] != DBNull.Value) certificateOb.ConferDate = Convert.ToDateTime(reader["ConferDate"]);
                        if (reader["ValidStartDate"] != DBNull.Value) certificateOb.ValidStartDate = Convert.ToDateTime(reader["ValidStartDate"]);
                        if (reader["ValidEndDate"] != DBNull.Value) certificateOb.ValidEndDate = Convert.ToDateTime(reader["ValidEndDate"]);
                        if (reader["ConferUnit"] != DBNull.Value) certificateOb.ConferUnit = Convert.ToString(reader["ConferUnit"]);
                        if (reader["Status"] != DBNull.Value) certificateOb.Status = Convert.ToString(reader["Status"]);
                        if (reader["CheckMan"] != DBNull.Value) certificateOb.CheckMan = Convert.ToString(reader["CheckMan"]);
                        if (reader["CheckAdvise"] != DBNull.Value) certificateOb.CheckAdvise = Convert.ToString(reader["CheckAdvise"]);
                        if (reader["CheckDate"] != DBNull.Value) certificateOb.CheckDate = Convert.ToDateTime(reader["CheckDate"]);
                        if (reader["PrintMan"] != DBNull.Value) certificateOb.PrintMan = Convert.ToString(reader["PrintMan"]);
                        if (reader["PrintDate"] != DBNull.Value) certificateOb.PrintDate = Convert.ToDateTime(reader["PrintDate"]);
                        if (reader["CreatePersonID"] != DBNull.Value) certificateOb.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                        if (reader["CreateTime"] != DBNull.Value) certificateOb.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                        if (reader["ModifyPersonID"] != DBNull.Value) certificateOb.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                        if (reader["ModifyTime"] != DBNull.Value) certificateOb.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                        if (reader["WorkerCertificateCode"] != DBNull.Value) certificateOb.WorkerCertificateCode = Convert.ToString(reader["WorkerCertificateCode"]);
                        if (reader["UnitCode"] != DBNull.Value) certificateOb.UnitCode = Convert.ToString(reader["UnitCode"]);
                        if (reader["ApplyMan"] != DBNull.Value) certificateOb.ApplyMan = Convert.ToString(reader["ApplyMan"]);
                        if (reader["CaseStatus"] != DBNull.Value) certificateOb.CaseStatus = Convert.ToString(reader["CaseStatus"]);
                        if (reader["AddItemName"] != DBNull.Value) certificateOb.AddItemName = Convert.ToString(reader["AddItemName"]);
                        if (reader["Remark"] != DBNull.Value) certificateOb.Remark = Convert.ToString(reader["Remark"]);
                        if (reader["SkillLevel"] != DBNull.Value) certificateOb.SkillLevel = Convert.ToString(reader["SkillLevel"]);
                        if (reader["TrainUnitName"] != DBNull.Value) certificateOb.TrainUnitName = Convert.ToString(reader["TrainUnitName"]);
                        if (reader["PrintCount"] != DBNull.Value) certificateOb.PrintCount = Convert.ToInt32(reader["PrintCount"]);
                        if (reader["FacePhoto"] != DBNull.Value) certificateOb.FacePhoto = Convert.ToString(reader["FacePhoto"]);
                        if (reader["PrintVer"] != DBNull.Value) certificateOb.PrintVer = Convert.ToInt32(reader["PrintVer"]);
                        if (reader["PostTypeName"] != DBNull.Value) certificateOb.PostTypeName = Convert.ToString(reader["PostTypeName"]);
                        if (reader["PostName"] != DBNull.Value) certificateOb.PostName = Convert.ToString(reader["PostName"]);
                        if (reader["ApplyCATime"] != DBNull.Value) certificateOb.ApplyCATime = Convert.ToDateTime(reader["ApplyCATime"]);
                        if (reader["ReturnCATime"] != DBNull.Value) certificateOb.ReturnCATime = Convert.ToDateTime(reader["ReturnCATime"]);
                        if (reader["SendCATime"] != DBNull.Value) certificateOb.SendCATime = Convert.ToDateTime(reader["SendCATime"]);
                        if (reader["WriteJHKCATime"] != DBNull.Value) certificateOb.WriteJHKCATime = Convert.ToDateTime(reader["WriteJHKCATime"]);
                        if (reader["CertificateCAID"] != DBNull.Value) certificateOb.CertificateCAID = Convert.ToString(reader["CertificateCAID"]);
                        if (reader["Job"] != DBNull.Value) certificateOb.Job = Convert.ToString(reader["Job"]);
                        if (reader["ZACheckTime"] != DBNull.Value) certificateOb.ZACheckTime = Convert.ToDateTime(reader["ZACheckTime"]);
                        if (reader["ZACheckResult"] != DBNull.Value) certificateOb.ZACheckResult = Convert.ToInt32(reader["ZACheckResult"]);
                        if (reader["ZACheckRemark"] != DBNull.Value) certificateOb.ZACheckRemark = Convert.ToString(reader["ZACheckRemark"]);
                        if (reader["EleCertErrTime"] != DBNull.Value) certificateOb.EleCertErrTime = Convert.ToDateTime(reader["EleCertErrTime"]);
                        if (reader["EleCertErrStep"] != DBNull.Value) certificateOb.EleCertErrStep = Convert.ToString(reader["EleCertErrStep"]);
                        if (reader["EleCertErrDesc"] != DBNull.Value) certificateOb.EleCertErrDesc = Convert.ToString(reader["EleCertErrDesc"]);
                        if (reader["QRCodeTime"] != DBNull.Value) certificateOb.QRCodeTime = Convert.ToDateTime(reader["QRCodeTime"]);
                        if (reader["ZZUrlUpTime"] != DBNull.Value) certificateOb.ZZUrlUpTime = Convert.ToDateTime(reader["ZZUrlUpTime"]);
                    }
                    reader.Close();
                    if (tran == null) db.Close();
                    return certificateOb;
                }
            }
            catch (Exception)
            {
                if (tran == null) db.Close();
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.Certificate", "*", filterWhereString, orderBy == "" ? " CertificateID" : orderBy);
        }

        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        public static DataTable GetListContinuePrint(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.VIEW_CERTIFICATE_PRINTCONTINUE", "*", filterWhereString, orderBy == "" ? " CertificateID" : orderBy);
            //return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.VIEW_CERTIFICATE_PRINTCONTINUE", "rn as printno,*", filterWhereString, orderBy == "" ? " CertificateID" : orderBy);
        }

        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountContinuePrint(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.VIEW_CERTIFICATE_PRINTCONTINUE", filterWhereString);
        }

        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        public static DataTable GetList(DbTransaction tran, int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(tran, startRowIndex, maximumRows, "dbo.Certificate", "*", filterWhereString, orderBy == "" ? " CertificateID" : orderBy);
        }

        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.Certificate", filterWhereString);
        }

        /// <summary>
        /// 根据查询条件统计企业个数
        /// </summary>
        /// <param name="filterWhereString"></param>
        /// <returns></returns>
        public static int SelectDistinctCountByUnitName(string filterWhereString)
        {
            string sql = @"SELECT COUNT(distinct UnitCode) FROM dbo.VIEW_CERTIFICATE_LEFT_CERTIFICATECHANGE WHERE 1=1 {0}";
            sql = string.Format(sql, filterWhereString);
            DBHelper db = new DBHelper();
            return Convert.ToInt32(db.ExecuteScalar(sql));
        }

        /// <summary>
        /// 审批通过
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="certificateOb">对象实体类</param>
        /// <param name="whereString"></param>
        /// <returns></returns>
        public static int Check(DbTransaction tran, CertificateOB certificateOb, string whereString)
        {
            string sql = @"
			UPDATE dbo.Certificate
				SET
                    Status = @Status,CheckMan = @CheckMan,CheckAdvise = @CheckAdvise,
                    CheckDate = ConferDate,ModifyPersonID = @ModifyPersonID,ModifyTime = @ModifyTime
			WHERE
				1=1 {0}";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            //p.Add(db.CreateParameter("ConferUnit", DbType.String, certificateOb.ConferUnit));
            p.Add(db.CreateParameter("Status", DbType.String, certificateOb.Status));
            p.Add(db.CreateParameter("CheckMan", DbType.String, certificateOb.CheckMan));
            p.Add(db.CreateParameter("CheckAdvise", DbType.String, certificateOb.CheckAdvise));
            //p.Add(db.CreateParameter("CheckDate", DbType.DateTime, certificateOb.CheckDate));
            p.Add(db.CreateParameter("ModifyPersonID", DbType.Int64, certificateOb.ModifyPersonID));
            p.Add(db.CreateParameter("ModifyTime", DbType.DateTime, certificateOb.ModifyTime));
            return db.ExcuteNonQuery(tran, string.Format(sql, whereString), p.ToArray());
        }

        /// <summary>
        /// 根据证书表更新考试报名表证书编号字段
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="filterWhereString">证书表过滤条件</param>
        /// <returns></returns>
        public static int UpdateExamSignup_ResultCertificatecode(DbTransaction tran, string filterWhereString)
        {
            string sql = @"MERGE INTO dbo.examsignup t1 USING
                (select s.examsignupid,c.certificatecode from dbo.examsignup s inner join DBO.CERTIFICATE c on s.WORKERID =c.WORKERID and s.EXAMPLANID =c.EXAMPLANID
                where c.EXAMPLANID in(select distinct EXAMPLANID from dbo.Certificate where 1=1 {0})
                ) t2 ON t1.examsignupid=t2.examsignupid
                 WHEN MATCHED THEN UPDATE SET t1.RESULTCERTIFICATECODE = t2.certificatecode;";
            sql = string.Format(sql, filterWhereString);

            DBHelper db = new DBHelper();
            return db.ExcuteNonQuery(tran, sql);
        }

        /// <summary>
        /// 审批不通过
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="whereString"></param>
        /// <returns></returns>
        public static int NoCheck(DbTransaction tran, string whereString)
        {
            string sql = @"
            Delete from dbo.Certificate WHERE 1=1 {0}";
            DBHelper db = new DBHelper();
            return db.ExcuteNonQuery(tran, string.Format(sql, whereString));
        }

        /// <summary>
        /// 证书续期决定后，批量更新证书表有效期
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="certificateOb">证书变化参数</param>
        /// <param name="addYears"></param>
        /// <param name="filterString">过滤条件</param>
        /// <returns></returns>
        public static int UpdateByContinueConfirm(DbTransaction tran, CertificateOB certificateOb, int addYears, string filterString)
        {
            string sql;
            if (certificateOb.PrintCount.HasValue)
            {
                sql = "UPDATE dbo.Certificate SET FACEPHOTO=(case when FACEPHOTO='' then null else FACEPHOTO end),ValidEndDate=[dbo].[GET_CertificateContinueValidEndDate3]([POSTTYPEID],[POSTID],[SEX],[BIRTHDAY],[VALIDENDDATE],[IfFR]),[VALIDSTARTDATE] = cast(@CheckDate as date),CheckDate = @CheckDate,CheckAdvise = @CheckAdvise,CheckMan = @CheckMan,Status = @Status,ModifyPersonID = @ModifyPersonID,ModifyTime =@ModifyTime,PrintCount =@PrintCount WHERE 1=1 " + filterString;
            }
            else
            {
                sql = "UPDATE dbo.Certificate SET FACEPHOTO=(case when FACEPHOTO='' then null else FACEPHOTO end),ValidEndDate=[dbo].[GET_CertificateContinueValidEndDate3]([POSTTYPEID],[POSTID],[SEX],[BIRTHDAY],[VALIDENDDATE],[IfFR]),[VALIDSTARTDATE] = cast(@CheckDate as date),CheckDate = @CheckDate,CheckAdvise = @CheckAdvise,CheckMan = @CheckMan,Status = @Status,ModifyPersonID = @ModifyPersonID,ModifyTime =@ModifyTime WHERE 1=1 " + filterString;
            }
            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("CheckDate", DbType.DateTime, certificateOb.CheckDate));
            p.Add(db.CreateParameter("CheckAdvise", DbType.String, certificateOb.CheckAdvise));
            p.Add(db.CreateParameter("CheckMan", DbType.String, certificateOb.CheckMan));
            //p.Add(db.CreateParameter("PrintDate", DbType.DateTime, certificateOb.PrintDate));
            //p.Add(db.CreateParameter("PrintMan", DbType.String, certificateOb.PrintMan));
            p.Add(db.CreateParameter("Status", DbType.String, certificateOb.Status));
            p.Add(db.CreateParameter("ModifyPersonID", DbType.Int64, certificateOb.ModifyPersonID));
            p.Add(db.CreateParameter("ModifyTime", DbType.DateTime, certificateOb.ModifyTime));
            p.Add(db.CreateParameter("AddYears", DbType.Int32, addYears));
            if (certificateOb.PrintCount.HasValue) p.Add(db.CreateParameter("PrintCount", DbType.Int32, certificateOb.PrintCount));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 特种作业证书续期决定后，批量更新证书表有效期，同时变更单位
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="certificateOb">证书变化参数</param>
        /// <param name="addYears"></param>
        /// <param name="filterString">过滤条件</param>
        /// <returns></returns>
        public static int UpdateByContinueConfirmWithChangeUnit(DbTransaction tran, CertificateOB certificateOb, int addYears, string filterString)
        {
            string sql;
            if (certificateOb.PrintCount.HasValue)
            {
                sql = string.Format(@"MERGE INTO dbo.Certificate t1 USING 
                        (select CertificateID,NewUnitName,NewUnitCode from dbo.CertificateContinue where 1=1 {0}) t2 ON t1.CertificateID=t2.CertificateID
                         WHEN MATCHED THEN UPDATE SET t1.UnitName = t2.NewUnitName , t1.UnitCode = t2.NewUnitCode,
                         FACEPHOTO=(case when FACEPHOTO='' then null else FACEPHOTO end),
                         [VALIDSTARTDATE] = cast(@CheckDate as date),
                        ValidEndDate=[dbo].[GET_CertificateContinueValidEndDate3](t1.[POSTTYPEID],t1.[POSTID],t1.[SEX],t1.[BIRTHDAY],t1.[VALIDENDDATE],t1.[IfFR]),CheckDate = @CheckDate,CheckAdvise = @CheckAdvise,CheckMan = @CheckMan,Status = @Status,ModifyPersonID = @ModifyPersonID,ModifyTime =@ModifyTime,PrintCount =@PrintCount;
                        ", filterString);
            }
            else
            {
                sql = string.Format(@"MERGE INTO dbo.Certificate t1 USING 
                        (select CertificateID,NewUnitName,NewUnitCode from dbo.CertificateContinue where 1=1 {0}) t2 ON t1.CertificateID=t2.CertificateID
                         WHEN MATCHED THEN UPDATE SET t1.UnitName = t2.NewUnitName , t1.UnitCode = t2.NewUnitCode,
                        FACEPHOTO=(case when FACEPHOTO='' then null else FACEPHOTO end),
                        [VALIDSTARTDATE] = cast(@CheckDate as date),
                        ValidEndDate=[dbo].[GET_CertificateContinueValidEndDate3](t1.[POSTTYPEID],t1.[POSTID],t1.[SEX],t1.[BIRTHDAY],t1.[VALIDENDDATE],t1.[IfFR]),CheckDate = @CheckDate,CheckAdvise = @CheckAdvise,CheckMan = @CheckMan,Status = @Status,ModifyPersonID = @ModifyPersonID,ModifyTime =@ModifyTime;
                        ", filterString);
            }
            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("CheckDate", DbType.DateTime, certificateOb.CheckDate));
            p.Add(db.CreateParameter("CheckAdvise", DbType.String, certificateOb.CheckAdvise));
            p.Add(db.CreateParameter("CheckMan", DbType.String, certificateOb.CheckMan));
            //p.Add(db.CreateParameter("PrintDate", DbType.DateTime, certificateOb.PrintDate));
            //p.Add(db.CreateParameter("PrintMan", DbType.String, certificateOb.PrintMan));
            p.Add(db.CreateParameter("Status", DbType.String, certificateOb.Status));
            p.Add(db.CreateParameter("ModifyPersonID", DbType.Int64, certificateOb.ModifyPersonID));
            p.Add(db.CreateParameter("ModifyTime", DbType.DateTime, certificateOb.ModifyTime));
            p.Add(db.CreateParameter("AddYears", DbType.Int32, addYears));
            if (certificateOb.PrintCount.HasValue) p.Add(db.CreateParameter("PrintCount", DbType.Int32, certificateOb.PrintCount));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 根据证书ID集合，更新证书归档状态
        /// </summary>
        /// <param name="filterString">证书Id集合（逗号分割）</param>
        /// <returns></returns>
        public static int UpdateCaseStatus(string filterString)
        {
            string sql = string.Format("UPDATE dbo.Certificate SET CaseStatus = '已归档' WHERE CertificateID in(select CertificateID from dbo.certificate where 1=1 {0})", filterString);
            DBHelper db = new DBHelper();
            return db.ExcuteNonQuery(sql);
        }

        /// <summary>
        /// 清除质安网校赋码时间，重发重新赋码
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="certificateId">证书ID</param>
        /// <returns></returns>
        public static int ClearZACheckTime(DbTransaction tran, long certificateId)
        {
            string sql = string.Format("UPDATE dbo.Certificate SET [QRCodeTime] = null WHERE CertificateID ={0}", certificateId);
            DBHelper db = new DBHelper();
            return db.ExcuteNonQuery(tran,sql);
        }

        /// <summary>
        /// 根据变更申请Id集合，更新证书归档状态
        /// </summary>
        /// <param name="certificateChangeIdList">证书变更申请Id集合（逗号分割）</param>
        /// <returns></returns>
        public static int UpdateCaseStatusByChange(string certificateChangeIdList)
        {
            string sql = string.Format("UPDATE dbo.Certificate SET CaseStatus = '已归档' WHERE CertificateID in( SELECT CertificateID FROM dbo.CertificateChange WHERE CertificateChangeID in({0}))", certificateChangeIdList);
            DBHelper db = new DBHelper();
            return db.ExcuteNonQuery(sql);
        }

        /// <summary>
        /// 根据证书编号获取单个实体
        /// </summary>
        /// <param name="certificateCode">证书编号</param>
        public static CertificateOB GetCertificateOBObject(string certificateCode)
        {
            const string sql = @"
			SELECT CertificateID,ExamPlanID,WorkerID,CertificateType,PostTypeID,PostID,CertificateCode,WorkerName,Sex,Birthday,UnitName,ConferDate,ValidStartDate,ValidEndDate,ConferUnit,Status,CheckMan,CheckAdvise,CheckDate,PrintMan,PrintDate,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,WorkerCertificateCode,UnitCode,ApplyMan,CaseStatus,AddItemName,Remark,SkillLevel,TrainUnitName,PrintCount,FacePhoto,PrintVer,PostTypeName,PostName,ApplyCATime,ReturnCATime,SendCATime,WriteJHKCATime,CertificateCAID,Job,ZACheckTime,ZACheckResult,ZACheckRemark,[EleCertErrTime],[EleCertErrStep],[EleCertErrDesc],QRCodeTime,ZZUrlUpTime,Ofd_ReturnCATime
			FROM dbo.Certificate
			WHERE CertificateCode = @CertificateCode";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("CertificateCode", DbType.String, certificateCode));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    CertificateOB certificateOb = null;
                    if (reader.Read())
                    {
                        certificateOb = new CertificateOB();
                        if (reader["CertificateID"] != DBNull.Value) certificateOb.CertificateID = Convert.ToInt64(reader["CertificateID"]);
                        if (reader["ExamPlanID"] != DBNull.Value) certificateOb.ExamPlanID = Convert.ToInt64(reader["ExamPlanID"]);
                        if (reader["WorkerID"] != DBNull.Value) certificateOb.WorkerID = Convert.ToInt64(reader["WorkerID"]);
                        if (reader["CertificateType"] != DBNull.Value) certificateOb.CertificateType = Convert.ToString(reader["CertificateType"]);
                        if (reader["PostTypeID"] != DBNull.Value) certificateOb.PostTypeID = Convert.ToInt32(reader["PostTypeID"]);
                        if (reader["PostID"] != DBNull.Value) certificateOb.PostID = Convert.ToInt32(reader["PostID"]);
                        if (reader["CertificateCode"] != DBNull.Value) certificateOb.CertificateCode = Convert.ToString(reader["CertificateCode"]);
                        if (reader["WorkerName"] != DBNull.Value) certificateOb.WorkerName = Convert.ToString(reader["WorkerName"]);
                        if (reader["Sex"] != DBNull.Value) certificateOb.Sex = Convert.ToString(reader["Sex"]);
                        if (reader["Birthday"] != DBNull.Value) certificateOb.Birthday = Convert.ToDateTime(reader["Birthday"]);
                        if (reader["UnitName"] != DBNull.Value) certificateOb.UnitName = Convert.ToString(reader["UnitName"]);
                        if (reader["ConferDate"] != DBNull.Value) certificateOb.ConferDate = Convert.ToDateTime(reader["ConferDate"]);
                        if (reader["ValidStartDate"] != DBNull.Value) certificateOb.ValidStartDate = Convert.ToDateTime(reader["ValidStartDate"]);
                        if (reader["ValidEndDate"] != DBNull.Value) certificateOb.ValidEndDate = Convert.ToDateTime(reader["ValidEndDate"]);
                        if (reader["ConferUnit"] != DBNull.Value) certificateOb.ConferUnit = Convert.ToString(reader["ConferUnit"]);
                        if (reader["Status"] != DBNull.Value) certificateOb.Status = Convert.ToString(reader["Status"]);
                        if (reader["CheckMan"] != DBNull.Value) certificateOb.CheckMan = Convert.ToString(reader["CheckMan"]);
                        if (reader["CheckAdvise"] != DBNull.Value) certificateOb.CheckAdvise = Convert.ToString(reader["CheckAdvise"]);
                        if (reader["CheckDate"] != DBNull.Value) certificateOb.CheckDate = Convert.ToDateTime(reader["CheckDate"]);
                        if (reader["PrintMan"] != DBNull.Value) certificateOb.PrintMan = Convert.ToString(reader["PrintMan"]);
                        if (reader["PrintDate"] != DBNull.Value) certificateOb.PrintDate = Convert.ToDateTime(reader["PrintDate"]);
                        if (reader["CreatePersonID"] != DBNull.Value) certificateOb.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                        if (reader["CreateTime"] != DBNull.Value) certificateOb.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                        if (reader["ModifyPersonID"] != DBNull.Value) certificateOb.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                        if (reader["ModifyTime"] != DBNull.Value) certificateOb.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                        if (reader["WorkerCertificateCode"] != DBNull.Value) certificateOb.WorkerCertificateCode = Convert.ToString(reader["WorkerCertificateCode"]);
                        if (reader["UnitCode"] != DBNull.Value) certificateOb.UnitCode = Convert.ToString(reader["UnitCode"]);
                        if (reader["ApplyMan"] != DBNull.Value) certificateOb.ApplyMan = Convert.ToString(reader["ApplyMan"]);
                        if (reader["CaseStatus"] != DBNull.Value) certificateOb.CaseStatus = Convert.ToString(reader["CaseStatus"]);
                        if (reader["AddItemName"] != DBNull.Value) certificateOb.AddItemName = Convert.ToString(reader["AddItemName"]);
                        if (reader["Remark"] != DBNull.Value) certificateOb.Remark = Convert.ToString(reader["Remark"]);
                        if (reader["SkillLevel"] != DBNull.Value) certificateOb.SkillLevel = Convert.ToString(reader["SkillLevel"]);
                        if (reader["TrainUnitName"] != DBNull.Value) certificateOb.TrainUnitName = Convert.ToString(reader["TrainUnitName"]);
                        if (reader["PrintCount"] != DBNull.Value) certificateOb.PrintCount = Convert.ToInt32(reader["PrintCount"]);
                        if (reader["FacePhoto"] != DBNull.Value) certificateOb.FacePhoto = Convert.ToString(reader["FacePhoto"]);
                        if (reader["PrintVer"] != DBNull.Value) certificateOb.PrintVer = Convert.ToInt32(reader["PrintVer"]);
                        if (reader["PostTypeName"] != DBNull.Value) certificateOb.PostTypeName = Convert.ToString(reader["PostTypeName"]);
                        if (reader["PostName"] != DBNull.Value) certificateOb.PostName = Convert.ToString(reader["PostName"]);
                        if (reader["ApplyCATime"] != DBNull.Value) certificateOb.ApplyCATime = Convert.ToDateTime(reader["ApplyCATime"]);
                        if (reader["ReturnCATime"] != DBNull.Value) certificateOb.ReturnCATime = Convert.ToDateTime(reader["ReturnCATime"]);
                        if (reader["SendCATime"] != DBNull.Value) certificateOb.SendCATime = Convert.ToDateTime(reader["SendCATime"]);
                        if (reader["WriteJHKCATime"] != DBNull.Value) certificateOb.WriteJHKCATime = Convert.ToDateTime(reader["WriteJHKCATime"]);
                        if (reader["CertificateCAID"] != DBNull.Value) certificateOb.CertificateCAID = Convert.ToString(reader["CertificateCAID"]);
                        if (reader["Job"] != DBNull.Value) certificateOb.Job = Convert.ToString(reader["Job"]);
                        if (reader["ZACheckTime"] != DBNull.Value) certificateOb.ZACheckTime = Convert.ToDateTime(reader["ZACheckTime"]);
                        if (reader["ZACheckResult"] != DBNull.Value) certificateOb.ZACheckResult = Convert.ToInt32(reader["ZACheckResult"]);
                        if (reader["ZACheckRemark"] != DBNull.Value) certificateOb.ZACheckRemark = Convert.ToString(reader["ZACheckRemark"]);
                        if (reader["EleCertErrTime"] != DBNull.Value) certificateOb.EleCertErrTime = Convert.ToDateTime(reader["EleCertErrTime"]);
                        if (reader["EleCertErrStep"] != DBNull.Value) certificateOb.EleCertErrStep = Convert.ToString(reader["EleCertErrStep"]);
                        if (reader["EleCertErrDesc"] != DBNull.Value) certificateOb.EleCertErrDesc = Convert.ToString(reader["EleCertErrDesc"]);
                        if (reader["QRCodeTime"] != DBNull.Value) certificateOb.QRCodeTime = Convert.ToDateTime(reader["QRCodeTime"]);
                        if (reader["ZZUrlUpTime"] != DBNull.Value) certificateOb.ZZUrlUpTime = Convert.ToDateTime(reader["ZZUrlUpTime"]);
                        if (reader["Ofd_ReturnCATime"] != DBNull.Value) certificateOb.Ofd_ReturnCATime = Convert.ToDateTime(reader["Ofd_ReturnCATime"]);
                    }
                    reader.Close();
                    db.Close();
                    return certificateOb;
                }
            }
            catch (Exception)
            {
                db.Close();
                throw;
            }
        }

        /// <summary>
        ///  根据证件号码检查该人是否存在未过期的岗位证书
        /// </summary>
        /// <param name="workerCertificateCode">证件号码</param>
        /// <param name="postId">岗位工种ID</param>
        /// <param name="nowdate">当前时间</param>
        /// <returns>证书对象</returns>
        public static CertificateOB GetCertificateOBObject(string workerCertificateCode, int? postId, DateTime nowdate)
        {
            //有效截止日期大于当前时间即为有效证书
            string sql = @"
			SELECT CertificateID,ExamPlanID,WorkerID,CertificateType,PostTypeID,PostID,CertificateCode,WorkerName,Sex,Birthday,UnitName,ConferDate,ValidStartDate,ValidEndDate,ConferUnit,Status,CheckMan,CheckAdvise,CheckDate,PrintMan,PrintDate,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,WorkerCertificateCode,UnitCode,ApplyMan,CaseStatus,AddItemName,Remark,SkillLevel,TrainUnitName,PrintCount,FacePhoto,PrintVer,PostTypeName,PostName,ApplyCATime,ReturnCATime,SendCATime,WriteJHKCATime,CertificateCAID,Job,ZACheckTime,ZACheckResult,ZACheckRemark,[EleCertErrTime],[EleCertErrStep],[EleCertErrDesc],QRCodeTime,ZZUrlUpTime,Ofd_ReturnCATime
			FROM dbo.Certificate
            WHERE WorkerCertificateCode = @WorkerCertificateCode and PostID=@PostID and ValidEndDate > @ValidEndDate and Status <> '离京变更' and Status <> '注销'";

            //WHERE WorkerCertificateCode = @WorkerCertificateCode and (PostID=@PostID or CertificateID in(select CertificateID from dbo.CertificateAddItem where CertificateID =dbo.Certificate.CertificateID and PostID=@PostID)) and ValidEndDate > @ValidEndDate and Status <> '离京变更' and Status <> '注销'";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("WorkerCertificateCode", DbType.String, workerCertificateCode));
            p.Add(db.CreateParameter("PostID", DbType.Int64, postId));
            p.Add(db.CreateParameter("ValidEndDate", DbType.DateTime, nowdate));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    CertificateOB certificateOb = null;
                    if (reader.Read())
                    {
                        certificateOb = new CertificateOB();
                        if (reader["CertificateID"] != DBNull.Value) certificateOb.CertificateID = Convert.ToInt64(reader["CertificateID"]);
                        if (reader["ExamPlanID"] != DBNull.Value) certificateOb.ExamPlanID = Convert.ToInt64(reader["ExamPlanID"]);
                        if (reader["WorkerID"] != DBNull.Value) certificateOb.WorkerID = Convert.ToInt64(reader["WorkerID"]);
                        if (reader["CertificateType"] != DBNull.Value) certificateOb.CertificateType = Convert.ToString(reader["CertificateType"]);
                        if (reader["PostTypeID"] != DBNull.Value) certificateOb.PostTypeID = Convert.ToInt32(reader["PostTypeID"]);
                        if (reader["PostID"] != DBNull.Value) certificateOb.PostID = Convert.ToInt32(reader["PostID"]);
                        if (reader["CertificateCode"] != DBNull.Value) certificateOb.CertificateCode = Convert.ToString(reader["CertificateCode"]);
                        if (reader["WorkerName"] != DBNull.Value) certificateOb.WorkerName = Convert.ToString(reader["WorkerName"]);
                        if (reader["Sex"] != DBNull.Value) certificateOb.Sex = Convert.ToString(reader["Sex"]);
                        if (reader["Birthday"] != DBNull.Value) certificateOb.Birthday = Convert.ToDateTime(reader["Birthday"]);
                        if (reader["UnitName"] != DBNull.Value) certificateOb.UnitName = Convert.ToString(reader["UnitName"]);
                        if (reader["ConferDate"] != DBNull.Value) certificateOb.ConferDate = Convert.ToDateTime(reader["ConferDate"]);
                        if (reader["ValidStartDate"] != DBNull.Value) certificateOb.ValidStartDate = Convert.ToDateTime(reader["ValidStartDate"]);
                        if (reader["ValidEndDate"] != DBNull.Value) certificateOb.ValidEndDate = Convert.ToDateTime(reader["ValidEndDate"]);
                        if (reader["ConferUnit"] != DBNull.Value) certificateOb.ConferUnit = Convert.ToString(reader["ConferUnit"]);
                        if (reader["Status"] != DBNull.Value) certificateOb.Status = Convert.ToString(reader["Status"]);
                        if (reader["CheckMan"] != DBNull.Value) certificateOb.CheckMan = Convert.ToString(reader["CheckMan"]);
                        if (reader["CheckAdvise"] != DBNull.Value) certificateOb.CheckAdvise = Convert.ToString(reader["CheckAdvise"]);
                        if (reader["CheckDate"] != DBNull.Value) certificateOb.CheckDate = Convert.ToDateTime(reader["CheckDate"]);
                        if (reader["PrintMan"] != DBNull.Value) certificateOb.PrintMan = Convert.ToString(reader["PrintMan"]);
                        if (reader["PrintDate"] != DBNull.Value) certificateOb.PrintDate = Convert.ToDateTime(reader["PrintDate"]);
                        if (reader["CreatePersonID"] != DBNull.Value) certificateOb.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                        if (reader["CreateTime"] != DBNull.Value) certificateOb.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                        if (reader["ModifyPersonID"] != DBNull.Value) certificateOb.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                        if (reader["ModifyTime"] != DBNull.Value) certificateOb.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                        if (reader["WorkerCertificateCode"] != DBNull.Value) certificateOb.WorkerCertificateCode = Convert.ToString(reader["WorkerCertificateCode"]);
                        if (reader["UnitCode"] != DBNull.Value) certificateOb.UnitCode = Convert.ToString(reader["UnitCode"]);
                        if (reader["ApplyMan"] != DBNull.Value) certificateOb.ApplyMan = Convert.ToString(reader["ApplyMan"]);
                        if (reader["CaseStatus"] != DBNull.Value) certificateOb.CaseStatus = Convert.ToString(reader["CaseStatus"]);
                        if (reader["AddItemName"] != DBNull.Value) certificateOb.CaseStatus = Convert.ToString(reader["AddItemName"]);
                        if (reader["Remark"] != DBNull.Value) certificateOb.Remark = Convert.ToString(reader["Remark"]);
                        if (reader["SkillLevel"] != DBNull.Value) certificateOb.SkillLevel = Convert.ToString(reader["SkillLevel"]);
                        if (reader["TrainUnitName"] != DBNull.Value) certificateOb.TrainUnitName = Convert.ToString(reader["TrainUnitName"]);
                        if (reader["PrintCount"] != DBNull.Value) certificateOb.PrintCount = Convert.ToInt32(reader["PrintCount"]);

                        if (reader["FacePhoto"] != DBNull.Value) certificateOb.FacePhoto = Convert.ToString(reader["FacePhoto"]);
                        if (reader["PrintVer"] != DBNull.Value) certificateOb.PrintVer = Convert.ToInt32(reader["PrintVer"]);
                        if (reader["PostTypeName"] != DBNull.Value) certificateOb.PostTypeName = Convert.ToString(reader["PostTypeName"]);
                        if (reader["PostName"] != DBNull.Value) certificateOb.PostName = Convert.ToString(reader["PostName"]);
                        if (reader["ApplyCATime"] != DBNull.Value) certificateOb.ApplyCATime = Convert.ToDateTime(reader["ApplyCATime"]);
                        if (reader["ReturnCATime"] != DBNull.Value) certificateOb.ReturnCATime = Convert.ToDateTime(reader["ReturnCATime"]);
                        if (reader["SendCATime"] != DBNull.Value) certificateOb.SendCATime = Convert.ToDateTime(reader["SendCATime"]);
                        if (reader["WriteJHKCATime"] != DBNull.Value) certificateOb.WriteJHKCATime = Convert.ToDateTime(reader["WriteJHKCATime"]);
                        if (reader["CertificateCAID"] != DBNull.Value) certificateOb.CertificateCAID = Convert.ToString(reader["CertificateCAID"]);
                        if (reader["Job"] != DBNull.Value) certificateOb.Job = Convert.ToString(reader["Job"]);
                        if (reader["ZACheckTime"] != DBNull.Value) certificateOb.ZACheckTime = Convert.ToDateTime(reader["ZACheckTime"]);
                        if (reader["ZACheckResult"] != DBNull.Value) certificateOb.ZACheckResult = Convert.ToInt32(reader["ZACheckResult"]);
                        if (reader["ZACheckRemark"] != DBNull.Value) certificateOb.ZACheckRemark = Convert.ToString(reader["ZACheckRemark"]);
                        if (reader["EleCertErrTime"] != DBNull.Value) certificateOb.EleCertErrTime = Convert.ToDateTime(reader["EleCertErrTime"]);
                        if (reader["EleCertErrStep"] != DBNull.Value) certificateOb.EleCertErrStep = Convert.ToString(reader["EleCertErrStep"]);
                        if (reader["EleCertErrDesc"] != DBNull.Value) certificateOb.EleCertErrDesc = Convert.ToString(reader["EleCertErrDesc"]);
                        if (reader["QRCodeTime"] != DBNull.Value) certificateOb.QRCodeTime = Convert.ToDateTime(reader["QRCodeTime"]);
                        if (reader["ZZUrlUpTime"] != DBNull.Value) certificateOb.ZZUrlUpTime = Convert.ToDateTime(reader["ZZUrlUpTime"]);
                        if (reader["Ofd_ReturnCATime"] != DBNull.Value) certificateOb.Ofd_ReturnCATime = Convert.ToDateTime(reader["Ofd_ReturnCATime"]);
                    }
                    reader.Close();
                    db.Close();
                    return certificateOb;
                }
            }
            catch (Exception)
            {
                db.Close();
                throw;
            }
        }

        /// <summary>
        ///  根据证件号码检查该人是否存在未过期的A,B,C1,C2,C3岗位证书
        /// </summary>
        /// <param name="workerCertificateCode">证件号码</param>
        /// <param name="postId">岗位工种ID</param>
        /// <param name="nowdate">当前时间</param>
        /// <returns>证书对象</returns>
        public static List<CertificateOB> GetCertificateOBObjectABC(string workerCertificateCode, DateTime nowdate)
        {
            //有效截止日期大于当前时间即为有效证书,增项的也记为有效证书
            string sql = @"
			SELECT  CertificateID,ExamPlanID,WorkerID,CertificateType,PostTypeID,PostID,CertificateCode,WorkerName,Sex,Birthday,UnitName,ConferDate,ValidStartDate,ValidEndDate,ConferUnit,Status,CheckMan,CheckAdvise,CheckDate,PrintMan,PrintDate,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,WorkerCertificateCode,UnitCode,ApplyMan,CaseStatus,AddItemName,Remark,SkillLevel,TrainUnitName,PrintCount,FacePhoto,PrintVer,PostTypeName,PostName,ApplyCATime,ReturnCATime,SendCATime,WriteJHKCATime,CertificateCAID,Job,ZACheckTime,ZACheckResult,ZACheckRemark,[EleCertErrTime],[EleCertErrStep],[EleCertErrDesc],QRCodeTime,ZZUrlUpTime,Ofd_ReturnCATime
			FROM dbo.Certificate
            WHERE WorkerCertificateCode = @WorkerCertificateCode and (PostID=147 or PostID=148 or PostID=6 or PostID=1123 or PostID=1125)  and ValidEndDate > @ValidEndDate and Status <> '离京变更' and Status <> '注销'";

            //WHERE WorkerCertificateCode = @WorkerCertificateCode and (PostID=@PostID or CertificateID in(select CertificateID from dbo.CertificateAddItem where CertificateID =dbo.Certificate.CertificateID and PostID=@PostID)) and ValidEndDate > @ValidEndDate and Status <> '离京变更' and Status <> '注销'";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("WorkerCertificateCode", DbType.String, workerCertificateCode));
            p.Add(db.CreateParameter("ValidEndDate", DbType.DateTime, nowdate));
            List<CertificateOB> list = new List<CertificateOB>();
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    while (reader.Read())
                    {
                        CertificateOB certificateOb = new CertificateOB();

                        if (reader["CertificateID"] != DBNull.Value) certificateOb.CertificateID = Convert.ToInt64(reader["CertificateID"]);
                        if (reader["ExamPlanID"] != DBNull.Value) certificateOb.ExamPlanID = Convert.ToInt64(reader["ExamPlanID"]);
                        if (reader["WorkerID"] != DBNull.Value) certificateOb.WorkerID = Convert.ToInt64(reader["WorkerID"]);
                        if (reader["CertificateType"] != DBNull.Value) certificateOb.CertificateType = Convert.ToString(reader["CertificateType"]);
                        if (reader["PostTypeID"] != DBNull.Value) certificateOb.PostTypeID = Convert.ToInt32(reader["PostTypeID"]);
                        if (reader["PostID"] != DBNull.Value) certificateOb.PostID = Convert.ToInt32(reader["PostID"]);
                        if (reader["CertificateCode"] != DBNull.Value) certificateOb.CertificateCode = Convert.ToString(reader["CertificateCode"]);
                        if (reader["WorkerName"] != DBNull.Value) certificateOb.WorkerName = Convert.ToString(reader["WorkerName"]);
                        if (reader["Sex"] != DBNull.Value) certificateOb.Sex = Convert.ToString(reader["Sex"]);
                        if (reader["Birthday"] != DBNull.Value) certificateOb.Birthday = Convert.ToDateTime(reader["Birthday"]);
                        if (reader["UnitName"] != DBNull.Value) certificateOb.UnitName = Convert.ToString(reader["UnitName"]);
                        if (reader["ConferDate"] != DBNull.Value) certificateOb.ConferDate = Convert.ToDateTime(reader["ConferDate"]);
                        if (reader["ValidStartDate"] != DBNull.Value) certificateOb.ValidStartDate = Convert.ToDateTime(reader["ValidStartDate"]);
                        if (reader["ValidEndDate"] != DBNull.Value) certificateOb.ValidEndDate = Convert.ToDateTime(reader["ValidEndDate"]);
                        if (reader["ConferUnit"] != DBNull.Value) certificateOb.ConferUnit = Convert.ToString(reader["ConferUnit"]);
                        if (reader["Status"] != DBNull.Value) certificateOb.Status = Convert.ToString(reader["Status"]);
                        if (reader["CheckMan"] != DBNull.Value) certificateOb.CheckMan = Convert.ToString(reader["CheckMan"]);
                        if (reader["CheckAdvise"] != DBNull.Value) certificateOb.CheckAdvise = Convert.ToString(reader["CheckAdvise"]);
                        if (reader["CheckDate"] != DBNull.Value) certificateOb.CheckDate = Convert.ToDateTime(reader["CheckDate"]);
                        if (reader["PrintMan"] != DBNull.Value) certificateOb.PrintMan = Convert.ToString(reader["PrintMan"]);
                        if (reader["PrintDate"] != DBNull.Value) certificateOb.PrintDate = Convert.ToDateTime(reader["PrintDate"]);
                        if (reader["CreatePersonID"] != DBNull.Value) certificateOb.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                        if (reader["CreateTime"] != DBNull.Value) certificateOb.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                        if (reader["ModifyPersonID"] != DBNull.Value) certificateOb.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                        if (reader["ModifyTime"] != DBNull.Value) certificateOb.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                        if (reader["WorkerCertificateCode"] != DBNull.Value) certificateOb.WorkerCertificateCode = Convert.ToString(reader["WorkerCertificateCode"]);
                        if (reader["UnitCode"] != DBNull.Value) certificateOb.UnitCode = Convert.ToString(reader["UnitCode"]);
                        if (reader["ApplyMan"] != DBNull.Value) certificateOb.ApplyMan = Convert.ToString(reader["ApplyMan"]);
                        if (reader["CaseStatus"] != DBNull.Value) certificateOb.CaseStatus = Convert.ToString(reader["CaseStatus"]);
                        if (reader["AddItemName"] != DBNull.Value) certificateOb.CaseStatus = Convert.ToString(reader["AddItemName"]);
                        if (reader["Remark"] != DBNull.Value) certificateOb.Remark = Convert.ToString(reader["Remark"]);
                        if (reader["SkillLevel"] != DBNull.Value) certificateOb.SkillLevel = Convert.ToString(reader["SkillLevel"]);
                        if (reader["TrainUnitName"] != DBNull.Value) certificateOb.TrainUnitName = Convert.ToString(reader["TrainUnitName"]);
                        if (reader["PrintCount"] != DBNull.Value) certificateOb.PrintCount = Convert.ToInt32(reader["PrintCount"]);

                        if (reader["FacePhoto"] != DBNull.Value) certificateOb.FacePhoto = Convert.ToString(reader["FacePhoto"]);
                        if (reader["PrintVer"] != DBNull.Value) certificateOb.PrintVer = Convert.ToInt32(reader["PrintVer"]);
                        if (reader["PostTypeName"] != DBNull.Value) certificateOb.PostTypeName = Convert.ToString(reader["PostTypeName"]);
                        if (reader["PostName"] != DBNull.Value) certificateOb.PostName = Convert.ToString(reader["PostName"]);
                        if (reader["ApplyCATime"] != DBNull.Value) certificateOb.ApplyCATime = Convert.ToDateTime(reader["ApplyCATime"]);
                        if (reader["ReturnCATime"] != DBNull.Value) certificateOb.ReturnCATime = Convert.ToDateTime(reader["ReturnCATime"]);
                        if (reader["SendCATime"] != DBNull.Value) certificateOb.SendCATime = Convert.ToDateTime(reader["SendCATime"]);
                        if (reader["WriteJHKCATime"] != DBNull.Value) certificateOb.WriteJHKCATime = Convert.ToDateTime(reader["WriteJHKCATime"]);
                        if (reader["CertificateCAID"] != DBNull.Value) certificateOb.CertificateCAID = Convert.ToString(reader["CertificateCAID"]);
                        if (reader["Job"] != DBNull.Value) certificateOb.Job = Convert.ToString(reader["Job"]);
                        if (reader["ZACheckTime"] != DBNull.Value) certificateOb.ZACheckTime = Convert.ToDateTime(reader["ZACheckTime"]);
                        if (reader["ZACheckResult"] != DBNull.Value) certificateOb.ZACheckResult = Convert.ToInt32(reader["ZACheckResult"]);
                        if (reader["ZACheckRemark"] != DBNull.Value) certificateOb.ZACheckRemark = Convert.ToString(reader["ZACheckRemark"]);
                        if (reader["EleCertErrTime"] != DBNull.Value) certificateOb.EleCertErrTime = Convert.ToDateTime(reader["EleCertErrTime"]);
                        if (reader["EleCertErrStep"] != DBNull.Value) certificateOb.EleCertErrStep = Convert.ToString(reader["EleCertErrStep"]);
                        if (reader["EleCertErrDesc"] != DBNull.Value) certificateOb.EleCertErrDesc = Convert.ToString(reader["EleCertErrDesc"]);
                        if (reader["QRCodeTime"] != DBNull.Value) certificateOb.QRCodeTime = Convert.ToDateTime(reader["QRCodeTime"]);
                        if (reader["ZZUrlUpTime"] != DBNull.Value) certificateOb.ZZUrlUpTime = Convert.ToDateTime(reader["ZZUrlUpTime"]);
                        if (reader["Ofd_ReturnCATime"] != DBNull.Value) certificateOb.Ofd_ReturnCATime = Convert.ToDateTime(reader["Ofd_ReturnCATime"]);
                        list.Add(certificateOb);
                    }
                    reader.Close();
                    db.Close();
                    return list;
                }
            }
            catch (Exception)
            {
                db.Close();
                throw;
            }
        }

        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="postTypeId">岗位类别ID</param>
        /// <param name="postId"></param>
        /// <param name="certificateCode">证书编号 like '%输入条件'</param>
        ///  <returns></returns>
        public static CertificateOB GetCertificateOBObject(string postTypeId, string postId, string certificateCode)
        {
            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            string sql;
            if (postId != "")
            {
                sql = @"
			SELECT CertificateID,ExamPlanID,WorkerID,CertificateType,PostTypeID,PostID,CertificateCode,WorkerName,Sex,Birthday,UnitName,ConferDate,ValidStartDate,ValidEndDate,ConferUnit,Status,CheckMan,CheckAdvise,CheckDate,PrintMan,PrintDate,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,WorkerCertificateCode,UnitCode,ApplyMan,CaseStatus,AddItemName,Remark,SkillLevel,TrainUnitName,PrintCount,FacePhoto,PrintVer,PostTypeName,PostName,ApplyCATime,ReturnCATime,SendCATime,WriteJHKCATime,CertificateCAID,Job,ZACheckTime,ZACheckResult,ZACheckRemark,[EleCertErrTime],[EleCertErrStep],[EleCertErrDesc],QRCodeTime,ZZUrlUpTime,Ofd_ReturnCATime
			FROM dbo.Certificate
			WHERE PostID=@PostID and CertificateCode like @CertificateCode";
                p.Add(db.CreateParameter("CertificateCode", DbType.String, certificateCode));
                p.Add(db.CreateParameter("PostID", DbType.Int32, Convert.ToInt32(postId)));
            }
            else if (postTypeId != "")
            {
                sql = @"
			SELECT CertificateID,ExamPlanID,WorkerID,CertificateType,PostTypeID,PostID,CertificateCode,WorkerName,Sex,Birthday,UnitName,ConferDate,ValidStartDate,ValidEndDate,ConferUnit,Status,CheckMan,CheckAdvise,CheckDate,PrintMan,PrintDate,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,WorkerCertificateCode,UnitCode,ApplyMan,CaseStatus,AddItemName,Remark,SkillLevel,TrainUnitName,PrintCount,FacePhoto,PrintVer,PostTypeName,PostName,ApplyCATime,ReturnCATime,SendCATime,WriteJHKCATime,CertificateCAID,Job,ZACheckTime,ZACheckResult,ZACheckRemark,[EleCertErrTime],[EleCertErrStep],[EleCertErrDesc],QRCodeTime,ZZUrlUpTime,Ofd_ReturnCATime
			FROM dbo.Certificate
			WHERE PostTypeID=@PostTypeID and CertificateCode like @CertificateCode";
                p.Add(db.CreateParameter("CertificateCode", DbType.String, certificateCode));
                p.Add(db.CreateParameter("PostTypeID", DbType.Int32, Convert.ToInt32(postTypeId)));
            }
            else
            {
                sql = @"
			SELECT CertificateID,ExamPlanID,WorkerID,CertificateType,PostTypeID,PostID,CertificateCode,WorkerName,Sex,Birthday,UnitName,ConferDate,ValidStartDate,ValidEndDate,ConferUnit,Status,CheckMan,CheckAdvise,CheckDate,PrintMan,PrintDate,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,WorkerCertificateCode,UnitCode,ApplyMan,CaseStatus,AddItemName,Remark,SkillLevel,TrainUnitName,PrintCount,FacePhoto,PrintVer,PostTypeName,PostName,ApplyCATime,ReturnCATime,SendCATime,WriteJHKCATime,CertificateCAID,Job,ZACheckTime,ZACheckResult,ZACheckRemark,[EleCertErrTime],[EleCertErrStep],[EleCertErrDesc],QRCodeTime,ZZUrlUpTime,Ofd_ReturnCATime
			FROM dbo.Certificate
			WHERE CertificateCode like @CertificateCode";
                p.Add(db.CreateParameter("CertificateCode", DbType.String, certificateCode));
            }

            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    CertificateOB certificateOb = null;
                    if (reader.Read())
                    {
                        certificateOb = new CertificateOB();
                        if (reader["CertificateID"] != DBNull.Value) certificateOb.CertificateID = Convert.ToInt64(reader["CertificateID"]);
                        if (reader["ExamPlanID"] != DBNull.Value) certificateOb.ExamPlanID = Convert.ToInt64(reader["ExamPlanID"]);
                        if (reader["WorkerID"] != DBNull.Value) certificateOb.WorkerID = Convert.ToInt64(reader["WorkerID"]);
                        if (reader["CertificateType"] != DBNull.Value) certificateOb.CertificateType = Convert.ToString(reader["CertificateType"]);
                        if (reader["PostTypeID"] != DBNull.Value) certificateOb.PostTypeID = Convert.ToInt32(reader["PostTypeID"]);
                        if (reader["PostID"] != DBNull.Value) certificateOb.PostID = Convert.ToInt32(reader["PostID"]);
                        if (reader["CertificateCode"] != DBNull.Value) certificateOb.CertificateCode = Convert.ToString(reader["CertificateCode"]);
                        if (reader["WorkerName"] != DBNull.Value) certificateOb.WorkerName = Convert.ToString(reader["WorkerName"]);
                        if (reader["Sex"] != DBNull.Value) certificateOb.Sex = Convert.ToString(reader["Sex"]);
                        if (reader["Birthday"] != DBNull.Value) certificateOb.Birthday = Convert.ToDateTime(reader["Birthday"]);
                        if (reader["UnitName"] != DBNull.Value) certificateOb.UnitName = Convert.ToString(reader["UnitName"]);
                        if (reader["ConferDate"] != DBNull.Value) certificateOb.ConferDate = Convert.ToDateTime(reader["ConferDate"]);
                        if (reader["ValidStartDate"] != DBNull.Value) certificateOb.ValidStartDate = Convert.ToDateTime(reader["ValidStartDate"]);
                        if (reader["ValidEndDate"] != DBNull.Value) certificateOb.ValidEndDate = Convert.ToDateTime(reader["ValidEndDate"]);
                        if (reader["ConferUnit"] != DBNull.Value) certificateOb.ConferUnit = Convert.ToString(reader["ConferUnit"]);
                        if (reader["Status"] != DBNull.Value) certificateOb.Status = Convert.ToString(reader["Status"]);
                        if (reader["CheckMan"] != DBNull.Value) certificateOb.CheckMan = Convert.ToString(reader["CheckMan"]);
                        if (reader["CheckAdvise"] != DBNull.Value) certificateOb.CheckAdvise = Convert.ToString(reader["CheckAdvise"]);
                        if (reader["CheckDate"] != DBNull.Value) certificateOb.CheckDate = Convert.ToDateTime(reader["CheckDate"]);
                        if (reader["PrintMan"] != DBNull.Value) certificateOb.PrintMan = Convert.ToString(reader["PrintMan"]);
                        if (reader["PrintDate"] != DBNull.Value) certificateOb.PrintDate = Convert.ToDateTime(reader["PrintDate"]);
                        if (reader["CreatePersonID"] != DBNull.Value) certificateOb.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                        if (reader["CreateTime"] != DBNull.Value) certificateOb.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                        if (reader["ModifyPersonID"] != DBNull.Value) certificateOb.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                        if (reader["ModifyTime"] != DBNull.Value) certificateOb.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                        if (reader["WorkerCertificateCode"] != DBNull.Value) certificateOb.WorkerCertificateCode = Convert.ToString(reader["WorkerCertificateCode"]);
                        if (reader["UnitCode"] != DBNull.Value) certificateOb.UnitCode = Convert.ToString(reader["UnitCode"]);
                        if (reader["ApplyMan"] != DBNull.Value) certificateOb.ApplyMan = Convert.ToString(reader["ApplyMan"]);
                        if (reader["CaseStatus"] != DBNull.Value) certificateOb.CaseStatus = Convert.ToString(reader["CaseStatus"]);
                        if (reader["AddItemName"] != DBNull.Value) certificateOb.CaseStatus = Convert.ToString(reader["AddItemName"]);
                        if (reader["Remark"] != DBNull.Value) certificateOb.Remark = Convert.ToString(reader["Remark"]);
                        if (reader["SkillLevel"] != DBNull.Value) certificateOb.SkillLevel = Convert.ToString(reader["SkillLevel"]);
                        if (reader["TrainUnitName"] != DBNull.Value) certificateOb.TrainUnitName = Convert.ToString(reader["TrainUnitName"]);
                        if (reader["PrintCount"] != DBNull.Value) certificateOb.PrintCount = Convert.ToInt32(reader["PrintCount"]);
                        if (reader["FacePhoto"] != DBNull.Value) certificateOb.FacePhoto = Convert.ToString(reader["FacePhoto"]);
                        if (reader["PrintVer"] != DBNull.Value) certificateOb.PrintVer = Convert.ToInt32(reader["PrintVer"]);
                        if (reader["PostTypeName"] != DBNull.Value) certificateOb.PostTypeName = Convert.ToString(reader["PostTypeName"]);
                        if (reader["PostName"] != DBNull.Value) certificateOb.PostName = Convert.ToString(reader["PostName"]);
                        if (reader["ApplyCATime"] != DBNull.Value) certificateOb.ApplyCATime = Convert.ToDateTime(reader["ApplyCATime"]);
                        if (reader["ReturnCATime"] != DBNull.Value) certificateOb.ReturnCATime = Convert.ToDateTime(reader["ReturnCATime"]);
                        if (reader["SendCATime"] != DBNull.Value) certificateOb.SendCATime = Convert.ToDateTime(reader["SendCATime"]);
                        if (reader["WriteJHKCATime"] != DBNull.Value) certificateOb.WriteJHKCATime = Convert.ToDateTime(reader["WriteJHKCATime"]);
                        if (reader["CertificateCAID"] != DBNull.Value) certificateOb.CertificateCAID = Convert.ToString(reader["CertificateCAID"]);
                        if (reader["Job"] != DBNull.Value) certificateOb.Job = Convert.ToString(reader["Job"]);
                        if (reader["ZACheckTime"] != DBNull.Value) certificateOb.ZACheckTime = Convert.ToDateTime(reader["ZACheckTime"]);
                        if (reader["ZACheckResult"] != DBNull.Value) certificateOb.ZACheckResult = Convert.ToInt32(reader["ZACheckResult"]);
                        if (reader["ZACheckRemark"] != DBNull.Value) certificateOb.ZACheckRemark = Convert.ToString(reader["ZACheckRemark"]);
                        if (reader["EleCertErrTime"] != DBNull.Value) certificateOb.EleCertErrTime = Convert.ToDateTime(reader["EleCertErrTime"]);
                        if (reader["EleCertErrStep"] != DBNull.Value) certificateOb.EleCertErrStep = Convert.ToString(reader["EleCertErrStep"]);
                        if (reader["EleCertErrDesc"] != DBNull.Value) certificateOb.EleCertErrDesc = Convert.ToString(reader["EleCertErrDesc"]);
                        if (reader["QRCodeTime"] != DBNull.Value) certificateOb.QRCodeTime = Convert.ToDateTime(reader["QRCodeTime"]);
                        if (reader["ZZUrlUpTime"] != DBNull.Value) certificateOb.ZZUrlUpTime = Convert.ToDateTime(reader["ZZUrlUpTime"]);
                        if (reader["Ofd_ReturnCATime"] != DBNull.Value) certificateOb.Ofd_ReturnCATime = Convert.ToDateTime(reader["Ofd_ReturnCATime"]);
                    }
                    reader.Close();
                    db.Close();
                    return certificateOb;
                }
            }
            catch (Exception)
            {
                db.Close();
                throw;
            }
        }

        /// <summary>
        /// 根据实施国标前证书编号获取特种作业证书信息
        /// </summary>
        /// <param name="postId">岗位工种ID</param>
        /// <param name="JZB_CERTIFICATECODE">施国标前原证书编号</param>
        /// <returns></returns>
        public static CertificateOB GetCertificateByJZB_CERTIFICATECODE(string postId, string JZB_CERTIFICATECODE)
        {
            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            string sql = @"
			SELECT top 1 CertificateID,ExamPlanID,WorkerID,CertificateType,PostTypeID,PostID,CertificateCode,WorkerName,Sex,Birthday,UnitName,ConferDate,ValidStartDate,ValidEndDate,ConferUnit,Status,CheckMan,CheckAdvise,CheckDate,PrintMan,PrintDate,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,WorkerCertificateCode,UnitCode,ApplyMan,CaseStatus,AddItemName,Remark,SkillLevel,TrainUnitName,PrintCount,FacePhoto,PrintVer,PostTypeName,PostName,ApplyCATime,ReturnCATime,SendCATime,WriteJHKCATime,CertificateCAID,Job,ZACheckTime,ZACheckResult,ZACheckRemark,[EleCertErrTime],[EleCertErrStep],[EleCertErrDesc],QRCodeTime,ZZUrlUpTime,Ofd_ReturnCATime
			FROM dbo.Certificate
			WHERE PostID=@PostID and JZB_CERTIFICATECODE =@JZB_CERTIFICATECODE
            order by ValidEndDate desc";
            p.Add(db.CreateParameter("PostID", DbType.Int32, Convert.ToInt32(postId)));
            p.Add(db.CreateParameter("JZB_CERTIFICATECODE", DbType.String, JZB_CERTIFICATECODE));

            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    CertificateOB certificateOb = null;
                    if (reader.Read())
                    {
                        certificateOb = new CertificateOB();
                        if (reader["CertificateID"] != DBNull.Value) certificateOb.CertificateID = Convert.ToInt64(reader["CertificateID"]);
                        if (reader["ExamPlanID"] != DBNull.Value) certificateOb.ExamPlanID = Convert.ToInt64(reader["ExamPlanID"]);
                        if (reader["WorkerID"] != DBNull.Value) certificateOb.WorkerID = Convert.ToInt64(reader["WorkerID"]);
                        if (reader["CertificateType"] != DBNull.Value) certificateOb.CertificateType = Convert.ToString(reader["CertificateType"]);
                        if (reader["PostTypeID"] != DBNull.Value) certificateOb.PostTypeID = Convert.ToInt32(reader["PostTypeID"]);
                        if (reader["PostID"] != DBNull.Value) certificateOb.PostID = Convert.ToInt32(reader["PostID"]);
                        if (reader["CertificateCode"] != DBNull.Value) certificateOb.CertificateCode = Convert.ToString(reader["CertificateCode"]);
                        if (reader["WorkerName"] != DBNull.Value) certificateOb.WorkerName = Convert.ToString(reader["WorkerName"]);
                        if (reader["Sex"] != DBNull.Value) certificateOb.Sex = Convert.ToString(reader["Sex"]);
                        if (reader["Birthday"] != DBNull.Value) certificateOb.Birthday = Convert.ToDateTime(reader["Birthday"]);
                        if (reader["UnitName"] != DBNull.Value) certificateOb.UnitName = Convert.ToString(reader["UnitName"]);
                        if (reader["ConferDate"] != DBNull.Value) certificateOb.ConferDate = Convert.ToDateTime(reader["ConferDate"]);
                        if (reader["ValidStartDate"] != DBNull.Value) certificateOb.ValidStartDate = Convert.ToDateTime(reader["ValidStartDate"]);
                        if (reader["ValidEndDate"] != DBNull.Value) certificateOb.ValidEndDate = Convert.ToDateTime(reader["ValidEndDate"]);
                        if (reader["ConferUnit"] != DBNull.Value) certificateOb.ConferUnit = Convert.ToString(reader["ConferUnit"]);
                        if (reader["Status"] != DBNull.Value) certificateOb.Status = Convert.ToString(reader["Status"]);
                        if (reader["CheckMan"] != DBNull.Value) certificateOb.CheckMan = Convert.ToString(reader["CheckMan"]);
                        if (reader["CheckAdvise"] != DBNull.Value) certificateOb.CheckAdvise = Convert.ToString(reader["CheckAdvise"]);
                        if (reader["CheckDate"] != DBNull.Value) certificateOb.CheckDate = Convert.ToDateTime(reader["CheckDate"]);
                        if (reader["PrintMan"] != DBNull.Value) certificateOb.PrintMan = Convert.ToString(reader["PrintMan"]);
                        if (reader["PrintDate"] != DBNull.Value) certificateOb.PrintDate = Convert.ToDateTime(reader["PrintDate"]);
                        if (reader["CreatePersonID"] != DBNull.Value) certificateOb.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                        if (reader["CreateTime"] != DBNull.Value) certificateOb.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                        if (reader["ModifyPersonID"] != DBNull.Value) certificateOb.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                        if (reader["ModifyTime"] != DBNull.Value) certificateOb.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                        if (reader["WorkerCertificateCode"] != DBNull.Value) certificateOb.WorkerCertificateCode = Convert.ToString(reader["WorkerCertificateCode"]);
                        if (reader["UnitCode"] != DBNull.Value) certificateOb.UnitCode = Convert.ToString(reader["UnitCode"]);
                        if (reader["ApplyMan"] != DBNull.Value) certificateOb.ApplyMan = Convert.ToString(reader["ApplyMan"]);
                        if (reader["CaseStatus"] != DBNull.Value) certificateOb.CaseStatus = Convert.ToString(reader["CaseStatus"]);
                        if (reader["AddItemName"] != DBNull.Value) certificateOb.CaseStatus = Convert.ToString(reader["AddItemName"]);
                        if (reader["Remark"] != DBNull.Value) certificateOb.Remark = Convert.ToString(reader["Remark"]);
                        if (reader["SkillLevel"] != DBNull.Value) certificateOb.SkillLevel = Convert.ToString(reader["SkillLevel"]);
                        if (reader["TrainUnitName"] != DBNull.Value) certificateOb.TrainUnitName = Convert.ToString(reader["TrainUnitName"]);
                        if (reader["PrintCount"] != DBNull.Value) certificateOb.PrintCount = Convert.ToInt32(reader["PrintCount"]);
                        if (reader["FacePhoto"] != DBNull.Value) certificateOb.FacePhoto = Convert.ToString(reader["FacePhoto"]);
                        if (reader["PrintVer"] != DBNull.Value) certificateOb.PrintVer = Convert.ToInt32(reader["PrintVer"]);
                        if (reader["PostTypeName"] != DBNull.Value) certificateOb.PostTypeName = Convert.ToString(reader["PostTypeName"]);
                        if (reader["PostName"] != DBNull.Value) certificateOb.PostName = Convert.ToString(reader["PostName"]);
                        if (reader["ApplyCATime"] != DBNull.Value) certificateOb.ApplyCATime = Convert.ToDateTime(reader["ApplyCATime"]);
                        if (reader["ReturnCATime"] != DBNull.Value) certificateOb.ReturnCATime = Convert.ToDateTime(reader["ReturnCATime"]);
                        if (reader["SendCATime"] != DBNull.Value) certificateOb.SendCATime = Convert.ToDateTime(reader["SendCATime"]);
                        if (reader["WriteJHKCATime"] != DBNull.Value) certificateOb.WriteJHKCATime = Convert.ToDateTime(reader["WriteJHKCATime"]);
                        if (reader["CertificateCAID"] != DBNull.Value) certificateOb.CertificateCAID = Convert.ToString(reader["CertificateCAID"]);
                        if (reader["Job"] != DBNull.Value) certificateOb.Job = Convert.ToString(reader["Job"]);
                        if (reader["ZACheckTime"] != DBNull.Value) certificateOb.ZACheckTime = Convert.ToDateTime(reader["ZACheckTime"]);
                        if (reader["ZACheckResult"] != DBNull.Value) certificateOb.ZACheckResult = Convert.ToInt32(reader["ZACheckResult"]);
                        if (reader["ZACheckRemark"] != DBNull.Value) certificateOb.ZACheckRemark = Convert.ToString(reader["ZACheckRemark"]);
                        if (reader["EleCertErrTime"] != DBNull.Value) certificateOb.EleCertErrTime = Convert.ToDateTime(reader["EleCertErrTime"]);
                        if (reader["EleCertErrStep"] != DBNull.Value) certificateOb.EleCertErrStep = Convert.ToString(reader["EleCertErrStep"]);
                        if (reader["EleCertErrDesc"] != DBNull.Value) certificateOb.EleCertErrDesc = Convert.ToString(reader["EleCertErrDesc"]);
                        if (reader["QRCodeTime"] != DBNull.Value) certificateOb.QRCodeTime = Convert.ToDateTime(reader["QRCodeTime"]);
                        if (reader["ZZUrlUpTime"] != DBNull.Value) certificateOb.ZZUrlUpTime = Convert.ToDateTime(reader["ZZUrlUpTime"]);
                        if (reader["Ofd_ReturnCATime"] != DBNull.Value) certificateOb.Ofd_ReturnCATime = Convert.ToDateTime(reader["Ofd_ReturnCATime"]);
                    }
                    reader.Close();
                    db.Close();
                    return certificateOb;
                }
            }
            catch (Exception)
            {
                db.Close();
                throw;
            }
        }

        /// <summary>
        /// 更新证书绑定照片
        /// </summary>
        /// <param name="CERTIFICATEID">证书ID</param>
        /// <param name="FacePhoto">一寸照片相对路径</param>
        /// <returns></returns>
        public static bool ModifyCertificate(long CERTIFICATEID, string FacePhoto)
        {
            string sql = @"UPDATE dbo.Certificate  SET FacePhoto ='{0}' WHERE CERTIFICATEID = {1}";

            try
            {
                return (new DBHelper()).ExcuteNonQuery(string.Format(sql, FacePhoto, CERTIFICATEID)) > 0 ? true : false;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        #region 报名限制校验规则

        /// <summary>
        /// 报名限制规则：每人允许持有多个"企业主要负责人"证书，但必须是不同单位的；
        /// </summary>
        /// <param name="ob">被检验证书</param>
        /// <param name="unitCode">单位组织机构代码</param>
        /// <returns>符合要求返回true</returns>
        public static bool CheckRegular_UnitMaster(CertificateOB ob, string unitCode)
        {
            if (ob.PostID != 147) return true;//不是企业主要负责人证书
            if (ob.UnitCode == unitCode) return false;//单位相同了
            return true;
        }

        /// <summary>
        /// 报名限制规则：
        /// 同时持有多本安管人员ABC证书的，其项目负责人B本、专职安管人员C本应与其一本法人A本证书工作单位一致。
        /// 只允许在同一单位同时取得“项目负责人”和“专职安全生产管理人员(C1、C2、C3)”证书，并且有C3不能再报考C1和C2，有C1或C2不能报考C3
        /// </summary>
        /// <param name="workerCertificateCode">证件号码</param>
        /// <param name="unitCode">单位组织机构代码</param>
        /// <param name="postId">岗位工种ID</param>
        /// <param name="postId">职务 </param>
        /// <returns>符合要求返回true</returns>
        public static bool CheckRegular_ItemMaster(string workerCertificateCode, string unitCode, int postId,string job)
        {
            //6		土建类专职安全生产管理人员
            //147		企业主要负责人
            //148		项目负责人
            //1123	机械类专职安全生产管理人员
            //1125	综合类专职安全生产管理人员

            int count_A = 0;
            if(postId == 147 )//校验A
            {
                if(job !="法定代表人")
                {
                    count_A =CommonDAL.GetRowCount("CERTIFICATE",string.Format(" and [WorkerCertificateCode]='{0}' and POSTID = 147 and ValidEndDate >=dateadd(day,-1,getdate()) and [Status] <> '离京变更' and [Status] <> '注销' and (job <> '法定代表人' or job is null)",workerCertificateCode));
                    if(count_A > 0) 
                    {
                        return false;//已经存在非法人A
                    }
                }

                 //判断是否与旗下B、C在同一家单位
                string sql = @"select count(*)
                                from
                                (
	                                select UNITCODE
	                                from [dbo].[CERTIFICATE]
	                                where WORKERCERTIFICATECODE='{0}' and postid in(148,6,1123,1125) and ValidEndDate >= dateadd(day,-1,getdate()) and [Status] <> '离京变更' and [Status] <> '注销'
	                                union
	                                select UNITCODE
	                                from [dbo].[CERTIFICATE]
	                                where WORKERCERTIFICATECODE='{0}' and postid in(148,6,1123,1125) and UNITCODE ='{1}' and ValidEndDate >= dateadd(day,-1,getdate()) and [Status] <> '离京变更' and [Status] <> '注销'
                                ) t
                                ";

                count_A=CommonDAL.SelectRowCount(string.Format(sql, workerCertificateCode, unitCode));
                if (count_A > 1)
                {
                    return false;//BC和A不在同一家单位
                }
            }    
            else if (postId == 6 || postId == 148 || postId == 1123 || postId == 1125)//校验B、C
            {
                bool p_c1 = false;//有C1
                bool p_c2 = false;//有C2
                bool p_c3 = false;//有C3
                int A_count=0;//存在几本A证
                bool ifACheckPass=false;//是否存在A证与BC在同一家单位

                List<CertificateOB> list = GetCertificateOBObjectABC(workerCertificateCode, DateTime.Now);
                if (list.Count == 0) return true;
                foreach (CertificateOB o in list)
                {
                    if(o.PostID == 147)
                    {
                        A_count +=1;//统计存在几本A证
                        if (o.UnitCode == unitCode)
                        {
                            ifACheckPass = true;//存在A证与BC在同一家单位
                        }
                    }
                    else
                    {
                        if (o.UnitCode != unitCode)//存在BC不在一家单位
                        {
                            return false;
                        }
                    }

                    if (o.PostID == 1123)
                    {
                        p_c1 = true;//有C1
                    }
                    if (o.PostID == 1125)
                    {
                        p_c3 = true;//有C3
                    }
                    if (o.PostID == 6)
                    {
                        p_c2 = true;//有C2
                    }
                }

                switch (postId)
                {
                    case 1123:
                    case 6:
                        if (p_c3 == true) return false;//有C1或C2不能报考C3
                        break;
                    case 1125:
                        if (p_c1 == true || p_c2 == true) return false;//有C3不能再报考C1和C2
                        break;
                }

                if(A_count >0 //存在A证
                    && ifACheckPass==false)//是否存在A证与BC在同一家单位
                {
                    return false;//没有一本A证与BC在同一家单位
                }
            }
            return true;
        }

        /// <summary>
        /// 报名限制规则：必须满18周岁
        /// </summary>
        /// <param name="birthday">出生日期</param>
        /// <returns>符合要求返回true</returns>
        public static bool CheckRegular_SpecialOperator(DateTime birthday)
        {
            //“周岁”，按照公历的年、月、日计算，从周岁生日的第二天起算。
            if (birthday.AddYears(18) > DateTime.Now) return false;//未满10周岁
            return true;
        }

        #endregion 报名限制校验规则

        #region 统计分析

        /// <summary>
        /// 获取证书管理统计模版
        /// </summary>
        /// <param name="postTypeId">岗位类别ID</param>
        /// <param name="postId">岗位工种ID</param>
        /// <returns></returns>
        public static DataTable AnalysisCertificateManageBase(int? postTypeId, int? postId)
        {
            string sql = string.Format(@"select n1.POSTID as PostTypeID,n1.POSTNAME as PostTypeName,n2.POSTID,n2.POSTNAME
,0 as 当前有效,0 as 首次,0 as 续期,0 as 京内变更,0 as 离京变更,0 as 进京变更,0 as 注销,0 as 补办,0 as 小计
from dbo.PostInfo as n1 inner join dbo.PostInfo as n2 on n1.PostType='1' and n2.PostType='2' and n1.PostID= n2.UpPostID
Where 1=1 {0} {1}
union all
select 3 as PostTypeID,'造价员' as PostTypeName,9 as POSTID,'土建,增安装' as POSTNAME
,0 as 当前有效,0 as 首次,0 as 续期,0 as 京内变更,0 as 离京变更,0 as 进京变更,0 as 注销,0 as 补办,0 as 小计
union all
select 3 as PostTypeID,'造价员' as PostTypeName,12 as POSTID,'安装,增土建' as POSTNAME
,0 as 当前有效,0 as 首次,0 as 续期,0 as 京内变更,0 as 离京变更,0 as 进京变更,0 as 注销,0 as 补办,0 as 小计
order by PostTypeID,PostID", postTypeId.HasValue ? " and n1.PostType=" + postTypeId.Value : ""
, postId.HasValue ? " and n2.PostID=" + postId.Value : "");

            DBHelper db = new DBHelper();
            return db.GetFillData(sql);
        }

        /// <summary>
        /// 获取证书管理统计数据
        /// </summary>
        /// <param name="startDate">查询时间起始</param>
        /// <param name="endDate">查询时间截止</param>
        /// <param name="postTypeId">岗位类别ID</param>
        /// <param name="postId">岗位工种ID</param>
        /// <returns></returns>
        public static DataTable AnalysisCertificateManageData(DateTime? startDate, DateTime? endDate, int? postTypeId, int? postId)
        {
            string sql = @"SELECT
POSTTYPENAME,POSTNAME,CHANGETYPE, count(*) 'count'
FROM DBO.VIEW_CERTIFICATECHANGE
where [STATUS]='已告知' and NOTICEDATE BETWEEN '{0}' AND '{1}' {2} {3}
group by POSTTYPENAME,POSTNAME,CHANGETYPE
union all
SELECT
POSTTYPENAME,POSTNAME,'进京变更' as STATUS, count(*)
FROM DBO.VIEW_CERTIFICATE_ENTER
where APPLYSTATUS='已编号' and 1=1  and CONFRIMDATE BETWEEN '{0}' AND '{1}' {2} {3}
group by POSTTYPENAME,POSTNAME
union all
SELECT
POSTTYPENAME,POSTNAME,'首次' as STATUS, count(*)
FROM DBO.CERTIFICATE
where [STATUS]<>'待审批' and [STATUS]<>'进京待审批' and [STATUS]<>'进京变更' and CONFERDATE BETWEEN '{0}' AND '{1}' {2} {3}
group by POSTTYPENAME,POSTNAME
union all
SELECT
POSTTYPENAME,POSTNAME,'续期' as STATUS, count(*)
FROM DBO.VIEW_CERTIFICATECONTINUE
where [STATUS]='已决定' and CONFIRMDATE BETWEEN '{0}' AND '{1}' {2} {3}
group by POSTTYPENAME,POSTNAME
union all
select POSTTYPENAME,POSTNAME,'当前有效', count(*)
FROM DBO.CERTIFICATE
where ValidEndDate >=getdate() and ([STATUS] = '首次' or [STATUS] = '续期' or [STATUS] = '进京变更' or [STATUS] = '京内变更' or [STATUS] = '补办')
 and CONFERDATE BETWEEN '{0}' AND '{1}' {2} {3}
group by POSTTYPENAME,POSTNAME
";
            sql = string.Format(sql, startDate.HasValue ? startDate.Value.ToString("yyyy-MM-dd") : "1900-1-1"
                , endDate.HasValue ? endDate.Value.ToString("yyyy-MM-dd") + " 23:59:59" : "2050-1-1"
                , postTypeId.HasValue ? " and PostTypeID=" + postTypeId.Value : ""
                , postId.HasValue ? " and PostID=" + postId.Value : "");
            DBHelper db = new DBHelper();
            return db.GetFillData(sql);
        }

        /// <summary>
        /// 获取证书更新统计数据
        /// </summary>
        /// <param name="startDate">查询时间起始</param>
        /// <param name="endDate">查询时间截止</param>
        /// <param name="postTypeId">岗位类别ID</param>
        /// <param name="postId">岗位工种ID</param>
        /// <returns></returns>
        public static DataTable AnalysisCertificateUpdateData(DateTime? startDate, DateTime? endDate, int? postTypeId, int? postId)
        {
            string sql = @"SELECT
POSTTYPENAME,POSTNAME,[STATUS] as CHANGETYPE, count(*)
FROM DBO.CERTIFICATE
where 1=1 and [STATUS]<>'待审批' and [STATUS]<>'进京待审批' and ((ModifyTime BETWEEN '{0}' AND '{1}') or (CreateTime BETWEEN '{0}' AND '{1}' and ModifyTime is null)) {2} {3}
group by POSTTYPENAME,POSTNAME,[STATUS]
";

            sql = string.Format(sql, startDate.HasValue ? startDate.Value.ToString("yyyy-MM-dd") : "1900-1-1"
                , endDate.HasValue ? endDate.Value.ToString("yyyy-MM-dd") + " 23:59:59" : "2050-1-1"
                , postTypeId.HasValue ? " and PostTypeID=" + postTypeId.Value : ""
                , postId.HasValue ? " and PostID=" + postId.Value : "");
            DBHelper db = new DBHelper();
            return db.GetFillData(sql);
        }

        /// <summary>
        /// 获取考务管理统计模版
        /// </summary>
        /// <param name="postTypeId">岗位类别ID</param>
        /// <param name="postId">岗位工种ID</param>
        /// <returns></returns>
        public static DataTable AnalysisExamManageBase(int? postTypeId, int? postId)
        {
            string sql = string.Format(@"select n1.POSTID as PostTypeID,n1.POSTNAME as PostTypeName,n2.POSTID,n2.POSTNAME
                                                        ,0 as 考试人数,0 as 合格人数,0.0 as 合格率
                                                        from dbo.PostInfo as n1 inner join dbo.PostInfo as n2 on n1.PostType='1' and n2.PostType='2' and n1.PostID= n2.UpPostID
                                                        Where 1=1 {0} {1}
                                                        order by n2.UpPostID,n2.PostID", postTypeId.HasValue ? " and n1.PostID=" + postTypeId.Value : ""
                                                        , postId.HasValue ? " and n2.PostID=" + postId.Value : "");

            DBHelper db = new DBHelper();
            return db.GetFillData(sql);
        }

        /// <summary>
        /// 获取考务管理统计数据
        /// </summary>
        /// <param name="startDate">查询时间起始</param>
        /// <param name="endDate">查询时间截止</param>
        /// <param name="postTypeId">岗位类别ID</param>
        /// <param name="postId">岗位工种ID</param>
        /// <returns></returns>
        public static DataTable AnalysisExamManageData(DateTime? startDate, DateTime? endDate, int? postTypeId, int? postId)
        {
            string sql = @"SELECT
POSTTYPENAME,POSTID,POSTNAME,'合格人数' as ITEMTYPE, count(*) 'count'
FROM DBO.VIEW_EXAMSCORE
where EXAMRESULT='合格' and [STATUS]='成绩已公告' and EXAMSTARTDATE BETWEEN '{0}' AND '{1}' {2} {3}
group by POSTTYPENAME,POSTID,POSTNAME
union all
SELECT
POSTTYPENAME,POSTID,POSTNAME,'考试人数' as ITEMTYPE, count(*)
FROM DBO.VIEW_EXAMSCORE
where EXAMSTARTDATE BETWEEN '{0}' AND '{1}' {2} {3}
group by POSTTYPENAME,POSTID,POSTNAME
";
            sql = string.Format(sql, startDate.HasValue ? startDate.Value.ToString("yyyy-MM-dd") : "1900-1-1"
                , endDate.HasValue ? endDate.Value.ToString("yyyy-MM-dd") + " 23:59:59" : "2050-1-1"
                , postTypeId.HasValue ? " and PostTypeID=" + postTypeId.Value : ""
                , postId.HasValue ? " and PostID=" + postId.Value : "");
            DBHelper db = new DBHelper();
            return db.GetFillData(sql);
        }

        /// <summary>
        /// 获取证书申请及审查结果统计模版
        /// </summary>
        /// <returns></returns>
        public static DataTable AnalysisCertificateApplyResultBase()
        {
            //            string sql = string.Format(@"select POSTID as PostTypeID,POSTNAME as PostTypeName
            //,0 as 考试申请个数,0 as 考试通过个数,0 as 续期申请个数,0 as 续期通过个数,0 as 京内变更申请个数,0 as 京内变更通过个数,0 as 离京变更申请个数,0 as 离京变更通过个数,
            //0 as 进京变更申请个数,0 as 进京变更通过个数,0 as 注销申请个数,0 as 注销通过个数,0 as 补办申请个数,0 as 补办通过个数
            //from dbo.PostInfo
            //Where UpPostID=1
            //union all
            //select POSTID as PostTypeID,POSTNAME as PostTypeName
            //,0 as 考试申请个数,0 as 考试通过个数,0 as 续期申请个数,0 as 续期通过个数,0 as 京内变更申请个数,0 as 京内变更通过个数,0 as 离京变更申请个数,0 as 离京变更通过个数,
            //0 as 进京变更申请个数,0 as 进京变更通过个数,0 as 注销申请个数,0 as 注销通过个数,0 as 补办申请个数,0 as 补办通过个数
            //from dbo.PostInfo
            //Where (PostID=2 or PostID=3 or PostID=4 or PostID=5 )
            //order by PostTypeID desc
            //");
            string sql = string.Format(@"
select POSTID as PostTypeID,POSTNAME as PostTypeName
,0 as 考试申请个数,0 as 考试通过个数,0 as 缺考人数
,0 as 续期申请个数,0 as 续期通过个数,0 as 续期申请并通过个数
,0 as 京内变更申请个数,0 as 京内变更通过个数,0 as 京内变更申请并通过个数
,0 as 离京变更申请个数,0 as 离京变更通过个数,0 as 离京变更申请并通过个数
,0 as 进京变更申请个数,0 as 进京变更通过个数,0 as 进京变更申请并通过个数
,0 as 注销申请个数,0 as 注销通过个数,0 as 注销申请并通过个数
,0 as 补办申请个数,0 as 补办通过个数,0 as 补办申请并通过个数
from dbo.PostInfo
Where UpPostID=1
union all
select POSTID as PostTypeID,POSTNAME as PostTypeName
,0 as 考试申请个数,0 as 考试通过个数,0 as 缺考人数
,0 as 续期申请个数,0 as 续期通过个数,0 as 续期申请并通过个数
,0 as 京内变更申请个数,0 as 京内变更通过个数,0 as 京内变更申请并通过个数
,0 as 离京变更申请个数,0 as 离京变更通过个数,0 as 离京变更申请并通过个数
,0 as 进京变更申请个数,0 as 进京变更通过个数,0 as 进京变更申请并通过个数
,0 as 注销申请个数,0 as 注销通过个数,0 as 注销申请并通过个数
,0 as 补办申请个数,0 as 补办通过个数,0 as 补办申请并通过个数
from dbo.PostInfo
Where (PostID=2 or PostID=3 or PostID=4 or PostID=5 )
order by PostTypeID desc
");

            DBHelper db = new DBHelper();
            return db.GetFillData(sql);
        }

        /// <summary>
        /// 获取证书申请及审查结果统计数据
        /// </summary>
        /// <param name="startDate">查询时间起始</param>
        /// <param name="endDate">查询时间截止</param>
        /// <returns></returns>
        public static DataTable AnalysisCertificateApplyResultData(DateTime? startDate, DateTime? endDate)
        {
            //            string sql = @"SELECT  posttypename,'京内变更' as funType,'京内变更申请个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_CERTIFICATECHANGE""
            //where CHANGETYPE ='京内变更' and Applydate between '{0}' and '{1}' and posttypeid >1
            //group by posttypename
            //union all
            //SELECT  posttypename,'京内变更' as funType,'京内变更通过个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_CERTIFICATECHANGE""
            //where CHANGETYPE ='京内变更' and noticedate between '{0}' and '{1}' and posttypeid >1
            //group by posttypename
            //union all
            //SELECT  posttypename,'京内变更' as funType,'京内变更申请并通过个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_CERTIFICATECHANGE""
            //where CHANGETYPE ='京内变更' and Applydate between '{0}' and '{1}' and noticedate between '{0}' and '{1}' and posttypeid >1
            //group by posttypename
            //union all
            //SELECT  posttypename,'离京变更' as funType,'离京变更申请个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_CERTIFICATECHANGE""
            //where CHANGETYPE ='离京变更' and Applydate between '{0}' and '{1}' and posttypeid >1
            //group by posttypename
            //union all
            //SELECT  posttypename,'离京变更' as funType,'离京变更通过个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_CERTIFICATECHANGE""
            //where CHANGETYPE ='离京变更' and noticedate between '{0}' and '{1}' and posttypeid >1
            //group by posttypename
            //union all
            //SELECT  posttypename,'离京变更' as funType,'离京变更申请并通过个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_CERTIFICATECHANGE""
            //where CHANGETYPE ='离京变更' and Applydate between '{0}' and '{1}' and noticedate between '{0}' and '{1}' and posttypeid >1
            //group by posttypename
            //union all
            //SELECT  posttypename,'补办' as funType,'补办申请个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_CERTIFICATECHANGE""
            //where CHANGETYPE ='补办' and Applydate between '{0}' and '{1}' and posttypeid >1
            //group by posttypename
            //union all
            //SELECT  posttypename,'补办' as funType,'补办通过个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_CERTIFICATECHANGE""
            //where CHANGETYPE ='补办' and noticedate between '{0}' and '{1}' and posttypeid >1
            //group by posttypename
            //union all
            //SELECT  posttypename,'补办' as funType,'补办申请并通过个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_CERTIFICATECHANGE""
            //where CHANGETYPE ='补办' and Applydate between '{0}' and '{1}' and noticedate between '{0}' and '{1}' and posttypeid >1
            //group by posttypename
            //union all
            //SELECT  posttypename,'注销' as funType,'注销申请个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_CERTIFICATECHANGE""
            //where CHANGETYPE ='注销' and Applydate between '{0}' and '{1}' and posttypeid >1
            //group by posttypename
            //union all
            //SELECT  posttypename,'注销' as funType,'注销通过个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_CERTIFICATECHANGE""
            //where CHANGETYPE ='注销' and noticedate between '{0}' and '{1}' and posttypeid >1
            //group by posttypename
            //union all
            //SELECT  posttypename,'注销' as funType,'注销申请并通过个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_CERTIFICATECHANGE""
            //where CHANGETYPE ='注销' and Applydate between '{0}' and '{1}' and noticedate between '{0}' and '{1}' and posttypeid >1
            //group by posttypename
            //union all
            //SELECT  posttypename,'进京变更' as funType,'进京变更申请个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_CERTIFICATE_ENTER""
            //where Applydate between '{0}' and '{1}' and posttypeid >1
            //group by posttypename
            //union all
            //SELECT  posttypename,'进京变更' as funType,'进京变更通过个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_CERTIFICATE_ENTER""
            //where confrimdate between '{0}' and '{1}' and posttypeid >1
            //group by posttypename
            //union all
            //SELECT  posttypename,'进京变更' as funType,'进京变更申请并通过个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_CERTIFICATE_ENTER""
            //where Applydate between '{0}' and '{1}' and confrimdate between '{0}' and '{1}' and posttypeid >1
            //group by posttypename
            //union all
            //SELECT  posttypename,'续期' as funType,'续期申请个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_CERTIFICATECONTINUE""
            //where Applydate between '{0}' and '{1}' and posttypeid >1
            //group by posttypename
            //union all
            //SELECT  posttypename,'续期' as funType,'续期通过个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_CERTIFICATECONTINUE""
            //where confirmdate between '{0}' and '{1}' and posttypeid >1
            //group by posttypename
            //union all
            //SELECT  posttypename,'续期' as funType,'续期申请并通过个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_CERTIFICATECONTINUE""
            //where Applydate between '{0}' and '{1}' and confirmdate between '{0}' and '{1}' and posttypeid >1
            //group by posttypename
            //union all
            //SELECT  posttypename,'考试' as funType,'考试申请个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_EXAMRESULT""
            //where examplanid in(
            //SELECT distinct ""EXAMPLANID""
            //FROM ""DBO"".""VIEW_EXAMSCORE""
            //where ""EXAMSTARTDATE"" BETWEEN '{0}' AND '{1}'
            //) and posttypeid >1
            //group by posttypename
            //union all
            //SELECT  posttypename,'考试' as funType,'考试通过个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_EXAMRESULT""
            //where examplanid in(
            //SELECT distinct ""EXAMPLANID""
            //FROM ""DBO"".""VIEW_EXAMSCORE""
            //where ""EXAMRESULT""='合格' and [STATUS]='成绩已公告' and ""EXAMSTARTDATE"" BETWEEN '{0}' AND '{1}'
            //) and examresult='合格' and posttypeid >1
            //group by posttypename
            //union all
            //SELECT  postname as posttypename,'京内变更' as funType,'京内变更申请个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_CERTIFICATECHANGE""
            //where CHANGETYPE ='京内变更' and Applydate between '{0}' and '{1}' and (postid=6 or postid=147 or postid=148 )
            //group by postname
            //union all
            //SELECT  postname as posttypename,'京内变更' as funType,'京内变更通过个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_CERTIFICATECHANGE""
            //where CHANGETYPE ='京内变更' and noticedate between '{0}' and '{1}' and (postid=6 or postid=147 or postid=148 )
            //group by postname
            //union all
            //SELECT  postname as posttypename,'京内变更' as funType,'京内变更申请并通过个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_CERTIFICATECHANGE""
            //where CHANGETYPE ='京内变更' and Applydate between '{0}' and '{1}' and noticedate between '{0}' and '{1}' and (postid=6 or postid=147 or postid=148 )
            //group by postname
            //union all
            //SELECT  postname as posttypename,'离京变更' as funType,'离京变更申请个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_CERTIFICATECHANGE""
            //where CHANGETYPE ='离京变更' and Applydate between '{0}' and '{1}' and (postid=6 or postid=147 or postid=148 )
            //group by postname
            //union all
            //SELECT  postname as posttypename,'离京变更' as funType,'离京变更通过个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_CERTIFICATECHANGE""
            //where CHANGETYPE ='离京变更' and noticedate between '{0}' and '{1}' and (postid=6 or postid=147 or postid=148 )
            //group by postname
            //union all
            //SELECT  postname as posttypename,'离京变更' as funType,'离京变更申请并通过个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_CERTIFICATECHANGE""
            //where CHANGETYPE ='离京变更' and Applydate between '{0}' and '{1}' and noticedate between '{0}' and '{1}' and (postid=6 or postid=147 or postid=148 )
            //group by postname
            //union all
            //SELECT  postname as posttypename,'补办' as funType,'补办申请个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_CERTIFICATECHANGE""
            //where CHANGETYPE ='补办' and Applydate between '{0}' and '{1}' and (postid=6 or postid=147 or postid=148 )
            //group by postname
            //union all
            //SELECT  postname as posttypename,'补办' as funType,'补办通过个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_CERTIFICATECHANGE""
            //where CHANGETYPE ='补办' and noticedate between '{0}' and '{1}' and (postid=6 or postid=147 or postid=148 )
            //group by postname
            //union all
            //SELECT  postname as posttypename,'补办' as funType,'补办申请并通过个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_CERTIFICATECHANGE""
            //where CHANGETYPE ='补办' and Applydate between '{0}' and '{1}' and noticedate between '{0}' and '{1}' and (postid=6 or postid=147 or postid=148 )
            //group by postname
            //union all
            //SELECT  postname as posttypename,'注销' as funType,'注销申请个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_CERTIFICATECHANGE""
            //where CHANGETYPE ='注销' and Applydate between '{0}' and '{1}' and (postid=6 or postid=147 or postid=148 )
            //group by postname
            //union all
            //SELECT  postname as posttypename,'注销' as funType,'注销通过个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_CERTIFICATECHANGE""
            //where CHANGETYPE ='注销' and noticedate between '{0}' and '{1}' and (postid=6 or postid=147 or postid=148 )
            //group by postname
            //union all
            //SELECT  postname as posttypename,'注销' as funType,'注销申请并通过个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_CERTIFICATECHANGE""
            //where CHANGETYPE ='注销' and Applydate between '{0}' and '{1}' and noticedate between '{0}' and '{1}' and (postid=6 or postid=147 or postid=148 )
            //group by postname
            //union all
            //SELECT  postname as posttypename,'进京变更' as funType,'进京变更申请个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_CERTIFICATE_ENTER""
            //where Applydate between '{0}' and '{1}' and (postid=6 or postid=147 or postid=148 )
            //group by postname
            //union all
            //SELECT  postname as posttypename,'进京变更' as funType,'进京变更通过个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_CERTIFICATE_ENTER""
            //where confrimdate between '{0}' and '{1}' and (postid=6 or postid=147 or postid=148 )
            //group by postname
            //union all
            //SELECT  postname as posttypename,'进京变更' as funType,'进京变更申请并通过个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_CERTIFICATE_ENTER""
            //where Applydate between '{0}' and '{1}' and confrimdate between '{0}' and '{1}' and (postid=6 or postid=147 or postid=148 )
            //group by postname
            //union all
            //SELECT  postname as posttypename,'续期' as funType,'续期申请个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_CERTIFICATECONTINUE""
            //where Applydate between '{0}' and '{1}' and (postid=6 or postid=147 or postid=148 )
            //group by postname
            //union all
            //SELECT  postname as posttypename,'续期' as funType,'续期通过个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_CERTIFICATECONTINUE""
            //where confirmdate between '{0}' and '{1}' and (postid=6 or postid=147 or postid=148 )
            //group by postname
            //union all
            //SELECT  postname as posttypename,'续期' as funType,'续期申请并通过个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_CERTIFICATECONTINUE""
            //where Applydate between '{0}' and '{1}' and confirmdate between '{0}' and '{1}' and (postid=6 or postid=147 or postid=148 )
            //group by postname
            //union all
            //SELECT  postname as posttypename,'考试' as funType,'考试申请个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_EXAMSCORE""
            //where ""EXAMSTARTDATE"" BETWEEN '{0}' AND '{1}' and (postid=6 or postid=147 or postid=148 )
            //group by postname
            //union all
            //SELECT  postname as posttypename,'考试' as funType,'考试通过个数' as countType,count(*)
            //FROM ""DBO"".""VIEW_EXAMRESULT""
            //where examplanid in(
            //SELECT distinct ""EXAMPLANID""
            //FROM ""DBO"".""VIEW_EXAMSCORE""
            //where ""EXAMRESULT""='合格' and [STATUS]='成绩已公告' and ""EXAMSTARTDATE"" BETWEEN '{0}' AND '{1}'
            //) and examresult='合格' and (postid=6 or postid=147 or postid=148 )
            //group by postname
            //";
            string sql = @"select POSTTYPENAME,FUNTYPE,COUNTTYPE,sum(ITEMCOUNT) ITEMCOUNT from DBO.TJ_YEWU where TJ_DATE  BETWEEN '{0}' AND '{1}' group by POSTTYPENAME,FUNTYPE,COUNTTYPE";

            sql = string.Format(sql, startDate.HasValue ? startDate.Value.ToString("yyyy-MM-dd") : "1900-1-1"
                , endDate.HasValue ? endDate.Value.ToString("yyyy-MM-dd") + " 23:59:59" : "2050-1-1"
               );
            DBHelper db = new DBHelper();
            return db.GetFillData(sql);
        }

        /// <summary>
        /// 获取证书数量总体情况
        /// </summary>
        /// <param name="today">今天日期</param>
        /// <returns></returns>
        public static DataTable AnalysisCertificateBaseCount(DateTime today)
        {
            string sql = @"
select 'AllCount' as DataName,count(*) as DataCount
FROM DBO.CERTIFICATE where [STATUS]<>'待审批' and [STATUS]<>'进京待审批'
union all
select 'HistoryCount' as DataName,count(*) as DataCount
FROM DBO.CERTIFICATE where EXAMPLANID =-100 and  [STATUS]<>'待审批' and [STATUS]<>'进京待审批'
union all
select 'SysCreateCount' as DataName,count(*) as DataCount
FROM DBO.CERTIFICATE where (EXAMPLANID >0 or EXAMPLANID <-100) and  [STATUS]<>'待审批' and [STATUS]<>'进京待审批'
union all
select 'ExpireCount' as DataName,count(*) as DataCount
FROM DBO.CERTIFICATE where VALIDENDDATE < '2018-12-31' and  [STATUS]<>'待审批' and [STATUS]<>'进京待审批' and [STATUS]<>'注销' and [STATUS]<>'离京变更'
union all
select 'ExpireHistoryCount' as DataName,count(*) as DataCount
FROM DBO.CERTIFICATE where EXAMPLANID =-100 and VALIDENDDATE < '2018-12-31' and  [STATUS]<>'待审批' and [STATUS]<>'进京待审批' and [STATUS]<>'注销' and [STATUS]<>'离京变更'
union all
select 'ExpireSysCreateCount' as DataName,count(*) as DataCount
FROM DBO.CERTIFICATE where (EXAMPLANID >0 or EXAMPLANID <-100) and  VALIDENDDATE < '2018-12-31' and  [STATUS]<>'待审批' and [STATUS]<>'进京待审批' and [STATUS]<>'注销' and [STATUS]<>'离京变更'
union all
select 'ValidCount' as DataName,count(*) as DataCount
FROM DBO.CERTIFICATE where VALIDENDDATE >= '2018-12-31' and  [STATUS]<>'待审批' and [STATUS]<>'进京待审批' and [STATUS]<>'注销' and [STATUS]<>'离京变更'
union all
select 'ValidHistoryCount' as DataName,count(*) as DataCount
FROM DBO.CERTIFICATE where EXAMPLANID =-100  and VALIDENDDATE >= '2018-12-31' and  [STATUS]<>'待审批' and [STATUS]<>'进京待审批' and [STATUS]<>'注销' and [STATUS]<>'离京变更'
union all
select 'ValidSysCreateCount' as DataName,count(*) as DataCount
FROM DBO.CERTIFICATE where (EXAMPLANID >0 or EXAMPLANID <-100) and  VALIDENDDATE >= '2018-12-31' and  [STATUS]<>'待审批' and [STATUS]<>'进京待审批' and [STATUS]<>'注销' and [STATUS]<>'离京变更'
union all
select 'ZuXiaoCount' as DataName,count(*) as DataCount
FROM DBO.CERTIFICATE where VALIDENDDATE >= '2018-12-31' and  [STATUS]='注销'
union all
select 'ZuXiaoHistoryCount' as DataName,count(*) as DataCount
FROM DBO.CERTIFICATE where EXAMPLANID =-100  and VALIDENDDATE >= '2018-12-31' and  [STATUS]='注销'
union all
select 'ZuXiaoSysCreateCount' as DataName,count(*) as DataCount
FROM DBO.CERTIFICATE where (EXAMPLANID >0 or EXAMPLANID <-100) and  VALIDENDDATE >= '2018-12-31' and  [STATUS]='注销'
union all
select 'LiJingCount' as DataName,count(*) as DataCount
FROM DBO.CERTIFICATE where VALIDENDDATE >= '2018-12-31' and [STATUS]='离京变更'
union all
select 'LiJingHistoryCount' as DataName,count(*) as DataCount
FROM DBO.CERTIFICATE where EXAMPLANID =-100  and VALIDENDDATE >= '2018-12-31' and  [STATUS]='离京变更'
union all
select 'LiJingSysCreateCount' as DataName,count(*) as DataCount
FROM DBO.CERTIFICATE where (EXAMPLANID >0 or EXAMPLANID <-100) and  VALIDENDDATE >= '2018-12-31' and  [STATUS]='离京变更'
";
            sql = string.Format(sql, today.ToString("yyyy-MM-dd"));
            DBHelper db = new DBHelper();
            return db.GetFillData(sql);
        }

        /// <summary>
        /// 获取考务管理统计模版
        /// </summary>
        /// <param name="postTypeId">岗位类别ID</param>
        /// <param name="postId">岗位工种ID</param>
        /// <returns></returns>
        public static DataTable AnalysisCertificateSkillLevelBase(int? postTypeId, int? postId)
        {
            string sql = string.Format(@"select n1.POSTID as PostTypeID,n1.POSTNAME as PostTypeName,n2.POSTID,n2.POSTNAME
,0 as 初级工,0 as 中级工,0 as 高级工,0 as 技师,0 as 高级技师,0 as 无,0 as 小计
from dbo.PostInfo as n1 inner join dbo.PostInfo as n2 on n1.PostType='1' and n2.PostType='2' and n1.PostID= n2.UpPostID
Where 1=1 {0} {1}
order by n2.UpPostID,n2.PostID", postTypeId.HasValue ? " and n1.PostID=" + postTypeId.Value : ""
, postId.HasValue ? " and n2.PostID=" + postId.Value : "");

            DBHelper db = new DBHelper();
            return db.GetFillData(sql);
        }

        /// <summary>
        /// 获取证书管理统计数据
        /// </summary>
        /// <param name="startDate">查询时间起始</param>
        /// <param name="endDate">查询时间截止</param>
        /// <param name="postTypeId">岗位类别ID</param>
        /// <param name="postId">岗位工种ID</param>
        /// <param name="TRAINUNITNAME">培训点名称</param>
        /// <returns></returns>
        public static DataTable AnalysisCertificateSkillLevel(DateTime? startDate, DateTime? endDate, int? postTypeId, int? postId, string TRAINUNITNAME)
        {
            string sql = @"SELECT POSTTYPENAME,POSTNAME, SkillLevel,count(*) 'count'
                            FROM DBO.CERTIFICATE
                            where 1=1 and [STATUS]<>'待审批' and [STATUS]<>'进京待审批' and CONFERDATE BETWEEN '{0}' AND '{1}' {2} {3} {4}
                            group by POSTTYPENAME,POSTNAME,SkillLevel";

            sql = string.Format(sql, startDate.HasValue ? startDate.Value.ToString("yyyy-MM-dd") : "1900-1-1"
                , endDate.HasValue ? endDate.Value.ToString("yyyy-MM-dd") + " 23:59:59" : "2050-1-1"
                , postTypeId.HasValue ? " and PostTypeID=" + postTypeId.Value : ""
                , postId.HasValue ? " and PostID=" + postId.Value : ""
                , string.IsNullOrEmpty(TRAINUNITNAME) == false ? string.Format(" and TRAINUNITNAME like '{0}%'",TRAINUNITNAME) : "");
            DBHelper db = new DBHelper();
            return db.GetFillData(sql);
        }

        /// <summary>
        /// 获取项目负责人B本比对建造师数据集合
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        /// <summary/>
        public static DataTable GetListCompareJZS(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.VIEW_CERTIFICATE_JZS", "*", filterWhereString, orderBy == "" ? " CertificateID" : orderBy);
        }

        /// <summary>
        /// 统计项目负责人B本比对建造师查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountCompareJZS(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.VIEW_CERTIFICATE_JZS", filterWhereString);
        }

        /// <summary>
        /// 统计项目负责人比对建造师结果
        /// </summary>
        /// <returns></returns>
        public static DataTable AnalysisCompareJZS()
        {
            string sql = string.Format(@" SELECT '有效项目负责人（B本）证书数量' as 统计项,count(*) as 数量
FROM DBO.CERTIFICATE
where postid=148 and ValidEndDate >= getdate()
and ( [STATUS]  = '首次' or  [STATUS]  = '续期' or  [STATUS]  = '进京变更' or  [STATUS]  = '京内变更' or  [STATUS]  = '补办')
union all
SELECT '其中与建造师证书不一致数量', count(*)
FROM DBO.CERTIFICATE c left join dbo.ry_zcjzs r
on c.workercertificatecode = r.zjhm and c.unitcode = r.zzjgdm
where c.postid=148 and c.ValidEndDate >=  getdate()
and ( c.[STATUS]  = '首次' or  c.[STATUS]  = '续期' or  c.[STATUS]  = '进京变更' or  c.[STATUS]  = '京内变更' or  c.[STATUS]  = '补办')
and r.id is null ");

            DBHelper db = new DBHelper();
            return db.GetFillData(sql);
        }

        /// <summary>
        /// 统计该企业人员在今年即将要过期的证书数量
        /// </summary>
        /// <param name="unitCode">组织机构代码</param>
        /// <returns></returns>
        public static DataTable AnalysisEnterprisesToExpireCertificateDataByUnitCode(string unitCode)
        {
            string cmdtxt = string.Format(@"SELECT POSTTYPEID,POSTTYPENAME ,count(*) AS 'COUNT'
                                            FROM dbo.Certificate
                                            WHERE UnitCode='{0}' 
                                            AND   year(ValidEndDate) ={1}
                                            AND ([STATUS] = '首次' or [STATUS] = '续期' or [STATUS] = '进京变更' or [STATUS] = '京内变更' or [STATUS] = '补办') 
                                            AND posttypeid in ('1','2','3')
                                            GROUP BY POSTTYPEID,POSTTYPENAME", unitCode,
                DateTime.Now.Year);
            return new DBHelper().GetFillData(cmdtxt);
        }
        /// <summary>
        /// 统计该企业人员证书数量
        /// </summary>
        /// <param name="unitCode">组织机构代码</param>
        /// <returns></returns>
        public static DataTable AnalysisEnterprisesCertificateDataByUnitCode(string unitCode)
        {
            string cmdtxt = string.Format(@"SELECT POSTTYPEID,POSTTYPENAME,COUNT(POSTTYPEID) AS 'COUNT'
                                                            FROM dbo.Certificate
                                                            WHERE UnitCode='{0}' 
                                                            AND   ValidEndDate >'{1}' 
                                                            AND ([STATUS] = '首次' or [STATUS] = '续期' or [STATUS] = '进京变更' or [STATUS] = '京内变更' or [STATUS] = '补办') 
                                                            GROUP BY POSTTYPEID,POSTTYPENAME", unitCode, DateTime.Now.ToString("yyyy-MM-dd"));
            return new DBHelper().GetFillData(cmdtxt);
        }


        #endregion 统计分析


        /// <summary>
        /// 查看该人员是否有有效的A本
        /// </summary>
        /// <param name="WorkerID">主键</param>
        public static CertificateOB GetCertificateA(string WorkerCertificateCode)
        {
            return GetCertificateA(null, WorkerCertificateCode);
        }

        ///  <summary>
        ///     查看该人员是否有有效的A本
        ///  </summary>
        /// <param name="tran">事务</param>
        public static CertificateOB GetCertificateA(DbTransaction tran, string WorkerCertificateCode)
        {
            string sql = @"
			SELECT TOP(1) CertificateID,ExamPlanID,WorkerID,CertificateType,PostTypeID,PostID,CertificateCode,WorkerName,Sex,Birthday,UnitName,ConferDate,ValidStartDate,ValidEndDate,ConferUnit,Status,CheckMan,CheckAdvise,CheckDate,PrintMan,PrintDate,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,WorkerCertificateCode,UnitCode,ApplyMan,CaseStatus,AddItemName,Remark,SkillLevel,TrainUnitName,PrintCount,FacePhoto,PrintVer,PostTypeName,PostName,ApplyCATime,ReturnCATime,SendCATime,WriteJHKCATime,CertificateCAID,Job,ZACheckTime,ZACheckResult,ZACheckRemark,[EleCertErrTime],[EleCertErrStep],[EleCertErrDesc],QRCodeTime,ZZUrlUpTime,Ofd_ReturnCATime
			FROM dbo.Certificate
			WHERE WorkerCertificateCode = @WorkerCertificateCode 
            and PostTypeName='安全生产考核三类人员' and CertificateCode like '京建安A%' AND ValidEndDate > dateadd(day,-1,getdate()) AND STATUS !='离京变更' AND STATUS !='注销' ORDER BY ValidEndDate DESC";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("WorkerCertificateCode", DbType.String, WorkerCertificateCode));

            try
            {
                using (SqlDataReader reader = db.GetDataReader(tran, sql, p.ToArray()))
                {
                    CertificateOB certificateOb = null;
                    if (reader.Read())
                    {
                        certificateOb = new CertificateOB();
                        if (reader["CertificateID"] != DBNull.Value) certificateOb.CertificateID = Convert.ToInt64(reader["CertificateID"]);
                        if (reader["ExamPlanID"] != DBNull.Value) certificateOb.ExamPlanID = Convert.ToInt64(reader["ExamPlanID"]);
                        if (reader["WorkerID"] != DBNull.Value) certificateOb.WorkerID = Convert.ToInt64(reader["WorkerID"]);
                        if (reader["CertificateType"] != DBNull.Value) certificateOb.CertificateType = Convert.ToString(reader["CertificateType"]);
                        if (reader["PostTypeID"] != DBNull.Value) certificateOb.PostTypeID = Convert.ToInt32(reader["PostTypeID"]);
                        if (reader["PostID"] != DBNull.Value) certificateOb.PostID = Convert.ToInt32(reader["PostID"]);
                        if (reader["CertificateCode"] != DBNull.Value) certificateOb.CertificateCode = Convert.ToString(reader["CertificateCode"]);
                        if (reader["WorkerName"] != DBNull.Value) certificateOb.WorkerName = Convert.ToString(reader["WorkerName"]);
                        if (reader["Sex"] != DBNull.Value) certificateOb.Sex = Convert.ToString(reader["Sex"]);
                        if (reader["Birthday"] != DBNull.Value) certificateOb.Birthday = Convert.ToDateTime(reader["Birthday"]);
                        if (reader["UnitName"] != DBNull.Value) certificateOb.UnitName = Convert.ToString(reader["UnitName"]);
                        if (reader["ConferDate"] != DBNull.Value) certificateOb.ConferDate = Convert.ToDateTime(reader["ConferDate"]);
                        if (reader["ValidStartDate"] != DBNull.Value) certificateOb.ValidStartDate = Convert.ToDateTime(reader["ValidStartDate"]);
                        if (reader["ValidEndDate"] != DBNull.Value) certificateOb.ValidEndDate = Convert.ToDateTime(reader["ValidEndDate"]);
                        if (reader["ConferUnit"] != DBNull.Value) certificateOb.ConferUnit = Convert.ToString(reader["ConferUnit"]);
                        if (reader["Status"] != DBNull.Value) certificateOb.Status = Convert.ToString(reader["Status"]);
                        if (reader["CheckMan"] != DBNull.Value) certificateOb.CheckMan = Convert.ToString(reader["CheckMan"]);
                        if (reader["CheckAdvise"] != DBNull.Value) certificateOb.CheckAdvise = Convert.ToString(reader["CheckAdvise"]);
                        if (reader["CheckDate"] != DBNull.Value) certificateOb.CheckDate = Convert.ToDateTime(reader["CheckDate"]);
                        if (reader["PrintMan"] != DBNull.Value) certificateOb.PrintMan = Convert.ToString(reader["PrintMan"]);
                        if (reader["PrintDate"] != DBNull.Value) certificateOb.PrintDate = Convert.ToDateTime(reader["PrintDate"]);
                        if (reader["CreatePersonID"] != DBNull.Value) certificateOb.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                        if (reader["CreateTime"] != DBNull.Value) certificateOb.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                        if (reader["ModifyPersonID"] != DBNull.Value) certificateOb.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                        if (reader["ModifyTime"] != DBNull.Value) certificateOb.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                        if (reader["WorkerCertificateCode"] != DBNull.Value) certificateOb.WorkerCertificateCode = Convert.ToString(reader["WorkerCertificateCode"]);
                        if (reader["UnitCode"] != DBNull.Value) certificateOb.UnitCode = Convert.ToString(reader["UnitCode"]);
                        if (reader["ApplyMan"] != DBNull.Value) certificateOb.ApplyMan = Convert.ToString(reader["ApplyMan"]);
                        if (reader["CaseStatus"] != DBNull.Value) certificateOb.CaseStatus = Convert.ToString(reader["CaseStatus"]);
                        if (reader["AddItemName"] != DBNull.Value) certificateOb.CaseStatus = Convert.ToString(reader["AddItemName"]);
                        if (reader["Remark"] != DBNull.Value) certificateOb.Remark = Convert.ToString(reader["Remark"]);
                        if (reader["SkillLevel"] != DBNull.Value) certificateOb.SkillLevel = Convert.ToString(reader["SkillLevel"]);
                        if (reader["TrainUnitName"] != DBNull.Value) certificateOb.TrainUnitName = Convert.ToString(reader["TrainUnitName"]);
                        if (reader["PrintCount"] != DBNull.Value) certificateOb.PrintCount = Convert.ToInt32(reader["PrintCount"]);
                        if (reader["FacePhoto"] != DBNull.Value) certificateOb.FacePhoto = Convert.ToString(reader["FacePhoto"]);
                        if (reader["PrintVer"] != DBNull.Value) certificateOb.PrintVer = Convert.ToInt32(reader["PrintVer"]);
                        if (reader["PostTypeName"] != DBNull.Value) certificateOb.PostTypeName = Convert.ToString(reader["PostTypeName"]);
                        if (reader["PostName"] != DBNull.Value) certificateOb.PostName = Convert.ToString(reader["PostName"]);
                        if (reader["ApplyCATime"] != DBNull.Value) certificateOb.ApplyCATime = Convert.ToDateTime(reader["ApplyCATime"]);
                        if (reader["ReturnCATime"] != DBNull.Value) certificateOb.ReturnCATime = Convert.ToDateTime(reader["ReturnCATime"]);
                        if (reader["SendCATime"] != DBNull.Value) certificateOb.SendCATime = Convert.ToDateTime(reader["SendCATime"]);
                        if (reader["WriteJHKCATime"] != DBNull.Value) certificateOb.WriteJHKCATime = Convert.ToDateTime(reader["WriteJHKCATime"]);
                        if (reader["CertificateCAID"] != DBNull.Value) certificateOb.CertificateCAID = Convert.ToString(reader["CertificateCAID"]);
                        if (reader["Job"] != DBNull.Value) certificateOb.Job = Convert.ToString(reader["Job"]);
                        if (reader["ZACheckTime"] != DBNull.Value) certificateOb.ZACheckTime = Convert.ToDateTime(reader["ZACheckTime"]);
                        if (reader["ZACheckResult"] != DBNull.Value) certificateOb.ZACheckResult = Convert.ToInt32(reader["ZACheckResult"]);
                        if (reader["ZACheckRemark"] != DBNull.Value) certificateOb.ZACheckRemark = Convert.ToString(reader["ZACheckRemark"]);
                        if (reader["EleCertErrTime"] != DBNull.Value) certificateOb.EleCertErrTime = Convert.ToDateTime(reader["EleCertErrTime"]);
                        if (reader["EleCertErrStep"] != DBNull.Value) certificateOb.EleCertErrStep = Convert.ToString(reader["EleCertErrStep"]);
                        if (reader["EleCertErrDesc"] != DBNull.Value) certificateOb.EleCertErrDesc = Convert.ToString(reader["EleCertErrDesc"]);
                        if (reader["QRCodeTime"] != DBNull.Value) certificateOb.QRCodeTime = Convert.ToDateTime(reader["QRCodeTime"]);
                        if (reader["ZZUrlUpTime"] != DBNull.Value) certificateOb.ZZUrlUpTime = Convert.ToDateTime(reader["ZZUrlUpTime"]);
                        if (reader["Ofd_ReturnCATime"] != DBNull.Value) certificateOb.Ofd_ReturnCATime = Convert.ToDateTime(reader["Ofd_ReturnCATime"]);
                    }
                    reader.Close();
                    if (tran == null) db.Close();
                    return certificateOb;
                }
            }
            catch (Exception)
            {
                db.Close();
                throw;
            }
        }

        /// <summary>
        /// 清除个人证书免冠照片地址为空字符串信息为null
        /// </summary>
        /// <param name="WorkerCertificateCode">身份证号</param>
        /// <returns></returns>
        public static int ClearFacePhoto(string WorkerCertificateCode)
        {
            string sql = @"UPDATE [dbo].[CERTIFICATE] SET  [FACEPHOTO]=null WHERE [WORKERCERTIFICATECODE]=@WorkerCertificateCode and [FACEPHOTO]=''";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("WorkerCertificateCode", DbType.String, WorkerCertificateCode));

            return db.ExcuteNonQuery(sql, p.ToArray());
        }

        /// <summary>
        /// 按岗位工种统计证书数量
        /// </summary>
        /// <returns></returns>
        public static DataTable GetListCountByPost()
        {
            return CommonDAL.GetDataTable(@"select a.*,isnull(b.ValidCount,0) as ValidCount,cast(round(isnull(b.ValidCount,0) *100.0 /a.AllCount,2) as numeric(5,2))   as Perc
                                            from
                                            (
	                                            select PostTypeid,Postid,max(PostTypeName) PostTypeName,max(postName) postName ,count(*) AllCount
	                                            from dbo.Certificate
	                                            group by PostTypeid,Postid
                                            ) a
                                            left join 
                                            (
	                                            select Postid,count(*) ValidCount
	                                            from dbo.Certificate
	                                            where 1=1 and ValidEndDate >='2020-05-26' and ([STATUS] = '首次' or [STATUS] = '续期' or [STATUS] = '进京变更' or [STATUS] = '京内变更' or [STATUS] = '补办')
	                                            group by PostTypeid,Postid
                                            ) b on a.POSTID = b.POSTID
                                            order by PostTypeid,Postid");
        }


        /// <summary>
        /// 获取从业人员业务执法检查对象集合
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        public static DataTable GetListCheck(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.[TJ_CongYe_Check]", "*", filterWhereString, orderBy == "" ? " [PSN_CertificateNO]" : orderBy);
        }
        /// <summary>
        /// 统计从业人员业务执法检查对象记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountCheck(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.[TJ_CongYe_Check]", filterWhereString);
        }


        /// <summary>
        /// 获取电子证书生成、签章错误记录
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        public static DataTable GetListEleCertError(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.EleCertError", "*", filterWhereString, orderBy == "" ? " DoTime desc,CertNo" : orderBy);
        }

        /// <summary>
        /// 统计电子证书生成、签章错误记录
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountEleCertError(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.EleCertError", filterWhereString);
        }

        /// <summary>
        /// 获取电子证书生成中途失败证书列表
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        public static DataTable GetListEleCertHalf(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows,
@"(select [POSTTYPENAME],[POSTNAME],[CERTIFICATECODE],[VALIDENDDATE],[WORKERNAME],[WORKERCERTIFICATECODE] ,[ApplyCATime],[SendCATime],[SignCATime],[ReturnCATime],[CertificateCAID],[license_code],[auth_code],[EleCertErrTime],[EleCertErrStep],[EleCertErrDesc]
  FROM [dbo].[CERTIFICATE]
  where VALIDENDDATE > dateadd(day,-1,getdate()) and [STATUS] <>'注销'  and [STATUS] <>'离京变更'   
  and (
		([ApplyCATime]>'2000-1-1' and [ApplyCATime] < dateadd(hour,-2,getdate()) and [ReturnCATime] is null) 
		or (posttypeid <3 and (ZZUrlUpTime <[CHECKDATE] or ZZUrlUpTime is null))
	)
  union
  SELECT [PSN_Level] +'建造师',[PSN_RegisteProfession], [PSN_RegisterNO] ,[PSN_CertificateValidity],[PSN_Name],[PSN_CertificateNO],[ApplyCATime] ,[SendCATime],[SignCATime],[ReturnCATime],[CertificateCAID],[license_code],[auth_code] ,null,null,null    
  FROM [dbo].[COC_TOW_Person_BaseInfo]
  where  [ApplyCATime]>'2000-1-1' and [ApplyCATime] < dateadd(hour,-2,getdate()) and [ReturnCATime] is null
  union
  SELECT '二级造价工程师',[PSN_RegisteProfession], [PSN_RegisterNO] ,[PSN_CertificateValidity],[PSN_Name],[PSN_CertificateNO],[ApplyCATime] ,[SendCATime],[SignCATime],[ReturnCATime],[CertificateCAID],[license_code],[auth_code],null,null,null     
  FROM [dbo].[zjs_Certificate]
  where  [ApplyCATime]>'2000-1-1' and [ApplyCATime] < dateadd(hour,-2,getdate()) and [ReturnCATime] is null
) temp",
"*", filterWhereString, orderBy == "" ? " [POSTTYPENAME],[POSTNAME],[CERTIFICATECODE]" : orderBy);
        }

        /// <summary>
        /// 统计电子证书生成中途失败证书记录
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountEleCertHalf(string filterWhereString)
        {
            return CommonDAL.SelectRowCount(
@"(select [POSTTYPENAME],[POSTNAME],[CERTIFICATECODE],[VALIDENDDATE],[WORKERNAME],[WORKERCERTIFICATECODE] ,[ApplyCATime],[SendCATime],[SignCATime],[ReturnCATime],[CertificateCAID],[license_code],[auth_code],[EleCertErrTime],[EleCertErrStep],[EleCertErrDesc]
  FROM [dbo].[CERTIFICATE]
  where VALIDENDDATE > dateadd(day,-1,getdate()) and [STATUS] <>'注销'  and [STATUS] <>'离京变更'   
  and (
		([ApplyCATime]>'2000-1-1' and [ApplyCATime] < dateadd(hour,-2,getdate()) and [ReturnCATime] is null) 
		or (posttypeid <3 and (ZZUrlUpTime <[CHECKDATE] or ZZUrlUpTime is null))
	)
  union
  SELECT [PSN_Level] +'建造师',[PSN_RegisteProfession], [PSN_RegisterNO] ,[PSN_CertificateValidity],[PSN_Name],[PSN_CertificateNO],[ApplyCATime] ,[SendCATime],[SignCATime],[ReturnCATime],[CertificateCAID],[license_code],[auth_code],null,null,null       
  FROM [dbo].[COC_TOW_Person_BaseInfo]
  where  [ApplyCATime]>'2000-1-1' and [ApplyCATime] < dateadd(hour,-2,getdate()) and [ReturnCATime] is null
  union
  SELECT '二级造价工程师',[PSN_RegisteProfession], [PSN_RegisterNO] ,[PSN_CertificateValidity],[PSN_Name],[PSN_CertificateNO],[ApplyCATime] ,[SendCATime],[SignCATime],[ReturnCATime],[CertificateCAID],[license_code],[auth_code],null,null,null       
  FROM [dbo].[zjs_Certificate]
  where  [ApplyCATime]>'2000-1-1' and [ApplyCATime] < dateadd(hour,-2,getdate()) and [ReturnCATime] is null
) temp", filterWhereString);
        }


        /// <summary>
        /// 根据工商信息判定是否是指定单位的法定代表人
        /// </summary>
        /// <param name="UNI_SCID">18位社会统一信用代码</param>
        /// <param name="FDDBR">法定代表人姓名</param>
        /// <returns></returns>
        public static bool IFExistFRByUNI_SCID(string UNI_SCID, string FDDBR)
        {
            string sql = string.Format(
                @"select count(*) FROM [dbo].[QY_GSDJXX] where [UNI_SCID] like '{0}' and [CORP_RPT]='{1}'", UNI_SCID, FDDBR);
            var db = new DBHelper();
            int count = Convert.ToInt32(db.ExecuteScalar(sql));

            if (count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 根据工商信息判定是否是指定单位的法定代表人
        /// </summary>
        /// <param name="UnitCode">9位组织机构代码</param>
        /// <param name="FDDBR">法定代表人姓名</param>
        /// <returns></returns>
        public static bool IFExistFRByUnitCode(string UnitCode, string FDDBR)
        {
            string sql = string.Format(
                @"select count(*) FROM [dbo].[QY_GSDJXX] where [UNI_SCID] like '________{0}_' and [CORP_RPT]='{1}'", UnitCode, FDDBR);
            var db = new DBHelper();
            int count = Convert.ToInt32(db.ExecuteScalar(sql));

            if (count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 获取企业法人A证
        /// </summary>
        /// <param name="UnitCode">机构代码或社会统一信用代码</param>
        /// <returns></returns>
        public static CertificateOB GetFRCertA(string UnitCode)
        {
            string _UnitCode = (UnitCode.Length == 9 ? UnitCode : UnitCode.Substring(8, 9));

            string sql = @"
			SELECT TOP(1) CertificateID,ExamPlanID,WorkerID,CertificateType,PostTypeID,PostID,CertificateCode,WorkerName,Sex,Birthday,UnitName,ConferDate,ValidStartDate,ValidEndDate,ConferUnit,Status,CheckMan,CheckAdvise,CheckDate,PrintMan,PrintDate,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,WorkerCertificateCode,UnitCode,ApplyMan,CaseStatus,AddItemName,Remark,SkillLevel,TrainUnitName,PrintCount,FacePhoto,PrintVer,PostTypeName,PostName,ApplyCATime,ReturnCATime,SendCATime,WriteJHKCATime,CertificateCAID,Job,ZACheckTime,ZACheckResult,ZACheckRemark,[EleCertErrTime],[EleCertErrStep],[EleCertErrDesc],QRCodeTime,ZZUrlUpTime,Ofd_ReturnCATime
			FROM dbo.Certificate
			WHERE UnitCode = @UnitCode 
            and PostID=147 and Job='法定代表人' AND ValidEndDate > dateadd(day,-1,getdate()) AND STATUS !='离京变更' AND STATUS !='注销'";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("UnitCode", DbType.String, _UnitCode));

            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    CertificateOB certificateOb = null;
                    if (reader.Read())
                    {
                        certificateOb = new CertificateOB();
                        if (reader["CertificateID"] != DBNull.Value) certificateOb.CertificateID = Convert.ToInt64(reader["CertificateID"]);
                        if (reader["ExamPlanID"] != DBNull.Value) certificateOb.ExamPlanID = Convert.ToInt64(reader["ExamPlanID"]);
                        if (reader["WorkerID"] != DBNull.Value) certificateOb.WorkerID = Convert.ToInt64(reader["WorkerID"]);
                        if (reader["CertificateType"] != DBNull.Value) certificateOb.CertificateType = Convert.ToString(reader["CertificateType"]);
                        if (reader["PostTypeID"] != DBNull.Value) certificateOb.PostTypeID = Convert.ToInt32(reader["PostTypeID"]);
                        if (reader["PostID"] != DBNull.Value) certificateOb.PostID = Convert.ToInt32(reader["PostID"]);
                        if (reader["CertificateCode"] != DBNull.Value) certificateOb.CertificateCode = Convert.ToString(reader["CertificateCode"]);
                        if (reader["WorkerName"] != DBNull.Value) certificateOb.WorkerName = Convert.ToString(reader["WorkerName"]);
                        if (reader["Sex"] != DBNull.Value) certificateOb.Sex = Convert.ToString(reader["Sex"]);
                        if (reader["Birthday"] != DBNull.Value) certificateOb.Birthday = Convert.ToDateTime(reader["Birthday"]);
                        if (reader["UnitName"] != DBNull.Value) certificateOb.UnitName = Convert.ToString(reader["UnitName"]);
                        if (reader["ConferDate"] != DBNull.Value) certificateOb.ConferDate = Convert.ToDateTime(reader["ConferDate"]);
                        if (reader["ValidStartDate"] != DBNull.Value) certificateOb.ValidStartDate = Convert.ToDateTime(reader["ValidStartDate"]);
                        if (reader["ValidEndDate"] != DBNull.Value) certificateOb.ValidEndDate = Convert.ToDateTime(reader["ValidEndDate"]);
                        if (reader["ConferUnit"] != DBNull.Value) certificateOb.ConferUnit = Convert.ToString(reader["ConferUnit"]);
                        if (reader["Status"] != DBNull.Value) certificateOb.Status = Convert.ToString(reader["Status"]);
                        if (reader["CheckMan"] != DBNull.Value) certificateOb.CheckMan = Convert.ToString(reader["CheckMan"]);
                        if (reader["CheckAdvise"] != DBNull.Value) certificateOb.CheckAdvise = Convert.ToString(reader["CheckAdvise"]);
                        if (reader["CheckDate"] != DBNull.Value) certificateOb.CheckDate = Convert.ToDateTime(reader["CheckDate"]);
                        if (reader["PrintMan"] != DBNull.Value) certificateOb.PrintMan = Convert.ToString(reader["PrintMan"]);
                        if (reader["PrintDate"] != DBNull.Value) certificateOb.PrintDate = Convert.ToDateTime(reader["PrintDate"]);
                        if (reader["CreatePersonID"] != DBNull.Value) certificateOb.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                        if (reader["CreateTime"] != DBNull.Value) certificateOb.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                        if (reader["ModifyPersonID"] != DBNull.Value) certificateOb.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                        if (reader["ModifyTime"] != DBNull.Value) certificateOb.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                        if (reader["WorkerCertificateCode"] != DBNull.Value) certificateOb.WorkerCertificateCode = Convert.ToString(reader["WorkerCertificateCode"]);
                        if (reader["UnitCode"] != DBNull.Value) certificateOb.UnitCode = Convert.ToString(reader["UnitCode"]);
                        if (reader["ApplyMan"] != DBNull.Value) certificateOb.ApplyMan = Convert.ToString(reader["ApplyMan"]);
                        if (reader["CaseStatus"] != DBNull.Value) certificateOb.CaseStatus = Convert.ToString(reader["CaseStatus"]);
                        if (reader["AddItemName"] != DBNull.Value) certificateOb.CaseStatus = Convert.ToString(reader["AddItemName"]);
                        if (reader["Remark"] != DBNull.Value) certificateOb.Remark = Convert.ToString(reader["Remark"]);
                        if (reader["SkillLevel"] != DBNull.Value) certificateOb.SkillLevel = Convert.ToString(reader["SkillLevel"]);
                        if (reader["TrainUnitName"] != DBNull.Value) certificateOb.TrainUnitName = Convert.ToString(reader["TrainUnitName"]);
                        if (reader["PrintCount"] != DBNull.Value) certificateOb.PrintCount = Convert.ToInt32(reader["PrintCount"]);
                        if (reader["FacePhoto"] != DBNull.Value) certificateOb.FacePhoto = Convert.ToString(reader["FacePhoto"]);
                        if (reader["PrintVer"] != DBNull.Value) certificateOb.PrintVer = Convert.ToInt32(reader["PrintVer"]);
                        if (reader["PostTypeName"] != DBNull.Value) certificateOb.PostTypeName = Convert.ToString(reader["PostTypeName"]);
                        if (reader["PostName"] != DBNull.Value) certificateOb.PostName = Convert.ToString(reader["PostName"]);
                        if (reader["ApplyCATime"] != DBNull.Value) certificateOb.ApplyCATime = Convert.ToDateTime(reader["ApplyCATime"]);
                        if (reader["ReturnCATime"] != DBNull.Value) certificateOb.ReturnCATime = Convert.ToDateTime(reader["ReturnCATime"]);
                        if (reader["SendCATime"] != DBNull.Value) certificateOb.SendCATime = Convert.ToDateTime(reader["SendCATime"]);
                        if (reader["WriteJHKCATime"] != DBNull.Value) certificateOb.WriteJHKCATime = Convert.ToDateTime(reader["WriteJHKCATime"]);
                        if (reader["CertificateCAID"] != DBNull.Value) certificateOb.CertificateCAID = Convert.ToString(reader["CertificateCAID"]);
                        if (reader["Job"] != DBNull.Value) certificateOb.Job = Convert.ToString(reader["Job"]);
                        if (reader["ZACheckTime"] != DBNull.Value) certificateOb.ZACheckTime = Convert.ToDateTime(reader["ZACheckTime"]);
                        if (reader["ZACheckResult"] != DBNull.Value) certificateOb.ZACheckResult = Convert.ToInt32(reader["ZACheckResult"]);
                        if (reader["ZACheckRemark"] != DBNull.Value) certificateOb.ZACheckRemark = Convert.ToString(reader["ZACheckRemark"]);
                        if (reader["EleCertErrTime"] != DBNull.Value) certificateOb.EleCertErrTime = Convert.ToDateTime(reader["EleCertErrTime"]);
                        if (reader["EleCertErrStep"] != DBNull.Value) certificateOb.EleCertErrStep = Convert.ToString(reader["EleCertErrStep"]);
                        if (reader["EleCertErrDesc"] != DBNull.Value) certificateOb.EleCertErrDesc = Convert.ToString(reader["EleCertErrDesc"]);
                        if (reader["QRCodeTime"] != DBNull.Value) certificateOb.QRCodeTime = Convert.ToDateTime(reader["QRCodeTime"]);
                        if (reader["ZZUrlUpTime"] != DBNull.Value) certificateOb.ZZUrlUpTime = Convert.ToDateTime(reader["ZZUrlUpTime"]);
                        if (reader["Ofd_ReturnCATime"] != DBNull.Value) certificateOb.Ofd_ReturnCATime = Convert.ToDateTime(reader["Ofd_ReturnCATime"]);
                    }
                    reader.Close();
                    db.Close();
                    return certificateOb;
                }
            }
            catch (Exception)
            {
                db.Close();
                throw;
            }
        }

        /// <summary>
        /// 获取人员其他非法人A证
        /// </summary>
        /// <param name="IDCard">身份证</param>
        /// <param name="FilterCertificateCode">排除证书编号</param>
        /// <returns></returns>
        public static CertificateOB GetOtherNoFRCertA(string IDCard, string FilterCertificateCode)
        {
            string sql = @"
			SELECT TOP(1) CertificateID,ExamPlanID,WorkerID,CertificateType,PostTypeID,PostID,CertificateCode,WorkerName,Sex,Birthday,UnitName,ConferDate,ValidStartDate,ValidEndDate,ConferUnit,Status,CheckMan,CheckAdvise,CheckDate,PrintMan,PrintDate,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,WorkerCertificateCode,UnitCode,ApplyMan,CaseStatus,AddItemName,Remark,SkillLevel,TrainUnitName,PrintCount,FacePhoto,PrintVer,PostTypeName,PostName,ApplyCATime,ReturnCATime,SendCATime,WriteJHKCATime,CertificateCAID,Job,ZACheckTime,ZACheckResult,ZACheckRemark,[EleCertErrTime],[EleCertErrStep],[EleCertErrDesc],QRCodeTime,ZZUrlUpTime,Ofd_ReturnCATime
			FROM dbo.Certificate
			WHERE WorkerCertificateCode = @WorkerCertificateCode and CertificateCode <> @CertificateCode
            and PostID=147 and Job <> '法定代表人' AND ValidEndDate > dateadd(day,-1,getdate()) AND STATUS !='离京变更' AND STATUS !='注销'";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("WorkerCertificateCode", DbType.String, IDCard));
            p.Add(db.CreateParameter("CertificateCode", DbType.String, FilterCertificateCode));

            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    CertificateOB certificateOb = null;
                    if (reader.Read())
                    {
                        certificateOb = new CertificateOB();
                        if (reader["CertificateID"] != DBNull.Value) certificateOb.CertificateID = Convert.ToInt64(reader["CertificateID"]);
                        if (reader["ExamPlanID"] != DBNull.Value) certificateOb.ExamPlanID = Convert.ToInt64(reader["ExamPlanID"]);
                        if (reader["WorkerID"] != DBNull.Value) certificateOb.WorkerID = Convert.ToInt64(reader["WorkerID"]);
                        if (reader["CertificateType"] != DBNull.Value) certificateOb.CertificateType = Convert.ToString(reader["CertificateType"]);
                        if (reader["PostTypeID"] != DBNull.Value) certificateOb.PostTypeID = Convert.ToInt32(reader["PostTypeID"]);
                        if (reader["PostID"] != DBNull.Value) certificateOb.PostID = Convert.ToInt32(reader["PostID"]);
                        if (reader["CertificateCode"] != DBNull.Value) certificateOb.CertificateCode = Convert.ToString(reader["CertificateCode"]);
                        if (reader["WorkerName"] != DBNull.Value) certificateOb.WorkerName = Convert.ToString(reader["WorkerName"]);
                        if (reader["Sex"] != DBNull.Value) certificateOb.Sex = Convert.ToString(reader["Sex"]);
                        if (reader["Birthday"] != DBNull.Value) certificateOb.Birthday = Convert.ToDateTime(reader["Birthday"]);
                        if (reader["UnitName"] != DBNull.Value) certificateOb.UnitName = Convert.ToString(reader["UnitName"]);
                        if (reader["ConferDate"] != DBNull.Value) certificateOb.ConferDate = Convert.ToDateTime(reader["ConferDate"]);
                        if (reader["ValidStartDate"] != DBNull.Value) certificateOb.ValidStartDate = Convert.ToDateTime(reader["ValidStartDate"]);
                        if (reader["ValidEndDate"] != DBNull.Value) certificateOb.ValidEndDate = Convert.ToDateTime(reader["ValidEndDate"]);
                        if (reader["ConferUnit"] != DBNull.Value) certificateOb.ConferUnit = Convert.ToString(reader["ConferUnit"]);
                        if (reader["Status"] != DBNull.Value) certificateOb.Status = Convert.ToString(reader["Status"]);
                        if (reader["CheckMan"] != DBNull.Value) certificateOb.CheckMan = Convert.ToString(reader["CheckMan"]);
                        if (reader["CheckAdvise"] != DBNull.Value) certificateOb.CheckAdvise = Convert.ToString(reader["CheckAdvise"]);
                        if (reader["CheckDate"] != DBNull.Value) certificateOb.CheckDate = Convert.ToDateTime(reader["CheckDate"]);
                        if (reader["PrintMan"] != DBNull.Value) certificateOb.PrintMan = Convert.ToString(reader["PrintMan"]);
                        if (reader["PrintDate"] != DBNull.Value) certificateOb.PrintDate = Convert.ToDateTime(reader["PrintDate"]);
                        if (reader["CreatePersonID"] != DBNull.Value) certificateOb.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                        if (reader["CreateTime"] != DBNull.Value) certificateOb.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                        if (reader["ModifyPersonID"] != DBNull.Value) certificateOb.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                        if (reader["ModifyTime"] != DBNull.Value) certificateOb.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                        if (reader["WorkerCertificateCode"] != DBNull.Value) certificateOb.WorkerCertificateCode = Convert.ToString(reader["WorkerCertificateCode"]);
                        if (reader["UnitCode"] != DBNull.Value) certificateOb.UnitCode = Convert.ToString(reader["UnitCode"]);
                        if (reader["ApplyMan"] != DBNull.Value) certificateOb.ApplyMan = Convert.ToString(reader["ApplyMan"]);
                        if (reader["CaseStatus"] != DBNull.Value) certificateOb.CaseStatus = Convert.ToString(reader["CaseStatus"]);
                        if (reader["AddItemName"] != DBNull.Value) certificateOb.CaseStatus = Convert.ToString(reader["AddItemName"]);
                        if (reader["Remark"] != DBNull.Value) certificateOb.Remark = Convert.ToString(reader["Remark"]);
                        if (reader["SkillLevel"] != DBNull.Value) certificateOb.SkillLevel = Convert.ToString(reader["SkillLevel"]);
                        if (reader["TrainUnitName"] != DBNull.Value) certificateOb.TrainUnitName = Convert.ToString(reader["TrainUnitName"]);
                        if (reader["PrintCount"] != DBNull.Value) certificateOb.PrintCount = Convert.ToInt32(reader["PrintCount"]);
                        if (reader["FacePhoto"] != DBNull.Value) certificateOb.FacePhoto = Convert.ToString(reader["FacePhoto"]);
                        if (reader["PrintVer"] != DBNull.Value) certificateOb.PrintVer = Convert.ToInt32(reader["PrintVer"]);
                        if (reader["PostTypeName"] != DBNull.Value) certificateOb.PostTypeName = Convert.ToString(reader["PostTypeName"]);
                        if (reader["PostName"] != DBNull.Value) certificateOb.PostName = Convert.ToString(reader["PostName"]);
                        if (reader["ApplyCATime"] != DBNull.Value) certificateOb.ApplyCATime = Convert.ToDateTime(reader["ApplyCATime"]);
                        if (reader["ReturnCATime"] != DBNull.Value) certificateOb.ReturnCATime = Convert.ToDateTime(reader["ReturnCATime"]);
                        if (reader["SendCATime"] != DBNull.Value) certificateOb.SendCATime = Convert.ToDateTime(reader["SendCATime"]);
                        if (reader["WriteJHKCATime"] != DBNull.Value) certificateOb.WriteJHKCATime = Convert.ToDateTime(reader["WriteJHKCATime"]);
                        if (reader["CertificateCAID"] != DBNull.Value) certificateOb.CertificateCAID = Convert.ToString(reader["CertificateCAID"]);
                        if (reader["Job"] != DBNull.Value) certificateOb.Job = Convert.ToString(reader["Job"]);
                        if (reader["ZACheckTime"] != DBNull.Value) certificateOb.ZACheckTime = Convert.ToDateTime(reader["ZACheckTime"]);
                        if (reader["ZACheckResult"] != DBNull.Value) certificateOb.ZACheckResult = Convert.ToInt32(reader["ZACheckResult"]);
                        if (reader["ZACheckRemark"] != DBNull.Value) certificateOb.ZACheckRemark = Convert.ToString(reader["ZACheckRemark"]);
                        if (reader["EleCertErrTime"] != DBNull.Value) certificateOb.EleCertErrTime = Convert.ToDateTime(reader["EleCertErrTime"]);
                        if (reader["EleCertErrStep"] != DBNull.Value) certificateOb.EleCertErrStep = Convert.ToString(reader["EleCertErrStep"]);
                        if (reader["EleCertErrDesc"] != DBNull.Value) certificateOb.EleCertErrDesc = Convert.ToString(reader["EleCertErrDesc"]);
                        if (reader["QRCodeTime"] != DBNull.Value) certificateOb.QRCodeTime = Convert.ToDateTime(reader["QRCodeTime"]);
                        if (reader["ZZUrlUpTime"] != DBNull.Value) certificateOb.ZZUrlUpTime = Convert.ToDateTime(reader["ZZUrlUpTime"]);
                        if (reader["Ofd_ReturnCATime"] != DBNull.Value) certificateOb.Ofd_ReturnCATime = Convert.ToDateTime(reader["Ofd_ReturnCATime"]);
                    }
                    reader.Close();
                    db.Close();
                    return certificateOb;
                }
            }
            catch (Exception)
            {
                db.Close();
                throw;
            }
        }

        #region 国标电子证书字段标准化函数

        /// <summary>
        /// 获取安全生产管理人员国标证书状态编码
        /// </summary>
        /// <param name="CertState">人员资格系统中证书状态</param>
        /// <param name="ValidEndDate">有效期截止日期</param>
        ///  <param name="PauseStatus">暂扣申报状态</param> 
        /// <returns>国标证书状态编码：01有效，02暂扣，03撤销，04注销，05失效，06办理转出，07：吊销，99其他（注：用于安管人员多本证书变更到同一单位时的过度状态，相当于挂起）</returns>
        public static string Get_GB_Aqscglry_CertStateCode(string CertState, DateTime ValidEndDate, string PauseStatus = "")
        {
            return (CertState == EnumManager.CertificateUpdateType.Logout ? "04"
                    : CertState.ToString() == EnumManager.CertificateUpdateType.OutBeiJing ? "06"
                    : ValidEndDate < DateTime.Now.AddDays(-1) ? "04"//过期也传注销，否则他省不认
                    : PauseStatus == EnumManager.CertificatePauseStatus.ApplyPause?"02"
                    : PauseStatus == EnumManager.CertificatePauseStatus.ApplyReturn ? "01"
                    : "01");//01有效，02暂扣，03撤销，04注销，05失效，06办理转出，07：吊销，99其他
        }

        /// <summary>
        /// 根据编码获取安全生产管理人员国标证书状态名称
        /// </summary>
        /// <param name="code">安全生产管理人员国标证书状态编码</param>
        /// <returns>安全生产管理人员国标证书状态名称</returns>
        public static string Get_GB_Aqscglry_CertStateNameByCode(string code)
        {
            switch (code)
            {
                case "01":
                    return "有效";
                case "02":
                    return "暂扣";
                case "03":
                    return "撤销";
                case "04":
                    return "注销";
                case "05":
                    return "失效";
                case "06":
                    return "办理转出";
                case "07":
                    return "吊销";
                case "99":
                    return "其他";
                default:
                    return "";
            }
        }

        /// <summary>
        /// 获取安全生产管理人员国标证书状态名称
        /// </summary>
        /// <param name="CertState">人员资格系统中证书状态</param>
        /// <param name="ValidEndDate">有效期截止日期</param>
        /// <param name="PauseStatus">暂扣申报状态</param> 
        /// <returns>国标证书状态名称</returns>
        public static string Get_GB_Aqscglry_CertStateDesc(string CertState, DateTime ValidEndDate, string PauseStatus = "")
        {
            return (CertState == EnumManager.CertificateUpdateType.Logout ? "注销"
                : CertState == EnumManager.CertificateUpdateType.OutBeiJing ? "办理转出"
                : ValidEndDate < DateTime.Now.AddDays(-1) ? "注销" //过期也传注销，否则他省不认
                : PauseStatus == EnumManager.CertificatePauseStatus.ApplyPause ? "暂扣"
                : PauseStatus == EnumManager.CertificatePauseStatus.ApplyReturn ? "有效"
                : "有效");
        }

        /// <summary>
        /// 获取国标三类人员工种类别编码（char(2)）
        /// </summary>
        /// <param name="PostName">人员系统中工种名称</param>
        /// <returns>工种类别编码</returns>
        public static string GetSLRPostCode(string PostName)
        {
            switch (PostName)
            {
                case "企业主要负责人":
                    return "A";
                case "项目负责人":
                    return "B";
                case "机械类专职安全生产管理人员":
                    return "C1";
                case "土建类专职安全生产管理人员":
                    return "C2";
                case "综合类专职安全生产管理人员":
                    return "C3";
                default:
                    return "";
            }
        }

        /// <summary>
        /// 获取国标三类人员工种类别名称
        /// </summary>
        /// <param name="PostCode">三类人员工种类别编码</param>
        /// <returns>工种类别名称</returns>
        public static string GetSLRPostNameByCode(string PostCode)
        {
            switch (PostCode)
            {
                case "A":
                    return "企业主要负责人";
                case "B":
                    return "项目负责人";
                case "C1":
                    return "机械类专职安全生产管理人员";
                case "C2":
                    return "土建类专职安全生产管理人员";
                case "C3":
                    return "综合类专职安全生产管理人员";
                default:
                    return "";
            }
        }

        /// <summary>
        /// 获取国标三类人员职务编码
        /// </summary>
        /// <param name="dr">数据行</param>
        /// <returns>职务编码</returns>
        public static string GetAppointmentCode(DataRow dr)
        {
            //01	法定代表人
            //02	总经理（总裁）
            //03	分管安全生产的副总经理（副总裁 ）
            //04	分管生产经营的副总经理（副总裁）
            //05	技术负责人
            //06	安全总监
            //07	项目负责人（项目经理）
            //08	专职安全生产管理人员
            //99	其他
            if (dr["Job"] != null && dr["Job"] != DBNull.Value)
            {
                switch (dr["Job"].ToString())
                {
                    case "法定代表人":
                        return "01";
                    case "总经理（总裁）":
                        return "02";
                    case "分管安全生产的副总经理（副总裁）":
                        return "03";
                    case "分管生产经营的副总经理（副总裁）":
                        return "04";
                    case "技术负责人":
                        return "05";
                    case "安全总监":
                        return "06";
                    case "项目负责人（项目经理）":
                        return "07";
                    case "专职安全生产管理人员":
                        return "08";
                    case "其他":
                        return "99";
                }
            }

            switch (dr["POSTNAME"].ToString())
            {
                case "企业主要负责人":
                    if (CertificateDAL.IFExistFRByUnitCode(dr["UNITCODE"].ToString(), dr["WORKERNAME"].ToString()) == true)
                        return "01";
                    else
                        return "99";
                case "项目负责人":
                    return "07";
                case "机械类专职安全生产管理人员":
                    return "08";
                case "土建类专职安全生产管理人员":
                    return "08";
                case "综合类专职安全生产管理人员":
                    return "08";
                default:
                    return "99";
            }
        }

        /// <summary>
        /// 获取国标三类人员职务
        /// </summary>
        /// <param name="dr">数据行</param>
        /// <returns>职务名称</returns>
        public static string GetAppointmentName(DataRow dr)
        {
            //从数据行获取证书表职务字段值[SKILLLEVEL]，如果不在国标字典中按岗位、法人信息自动天聪职务信息。
            if (dr["Job"] != null && dr["Job"] != DBNull.Value)
            {
                switch (dr["Job"].ToString())
                {
                    case "法定代表人":
                    case "总经理（总裁）":
                    case "分管安全生产的副总经理（副总裁）":
                    case "分管生产经营的副总经理（副总裁）":
                    case "技术负责人":
                    case "安全总监":
                    case "项目负责人（项目经理）":
                    case "专职安全生产管理人员":
                        return dr["Job"].ToString();
                }
            }

            //01	法定代表人
            //02	总经理（总裁）
            //03	分管安全生产的副总经理（副总裁）
            //04	分管生产经营的副总经理（副总裁）
            //05	技术负责人
            //06	安全总监
            //07	项目负责人（项目经理）
            //08	专职安全生产管理人员
            //99	其他
            switch (dr["POSTNAME"].ToString())
            {
                case "企业主要负责人":
                    if (CertificateDAL.IFExistFRByUnitCode(dr["UNITCODE"].ToString(), dr["WORKERNAME"].ToString()) == true)
                        return "法定代表人";
                    else
                        return "";
                case "项目负责人":
                    return "项目负责人（项目经理）";
                case "机械类专职安全生产管理人员":
                case "土建类专职安全生产管理人员":
                case "综合类专职安全生产管理人员":
                    return "专职安全生产管理人员";
                default:
                    return "其他";
            }
        }


        /// <summary>
        /// 获取国标技术职称编码
        /// </summary>
        /// <param name="SKILLLEVEL">技术职称</param>
        /// <returns>技术职称编码</returns>
        public static string GetTechnicalTitleCode(string SKILLLEVEL)
        {
            //01	正高级工程师
            //02	高级工程师
            //03	工程师
            //04	助理工程师
            //05	技术员

            if (SKILLLEVEL.Contains("正高"))
                return "01";
            else if (SKILLLEVEL.Contains("高级"))
                return "02";
            else if (SKILLLEVEL.Contains("助理"))
                return "04";
            else if (SKILLLEVEL.Contains("工程师"))
                return "03";
            else
                return "05";
        }

        /// <summary>
        /// 获取国标文化程度编码
        /// </summary>
        /// <param name="CULTURALLEVEL">文化程度</param>
        /// <returns>文化程度编码</returns>
        public static string GetEducationDegreeCode(string CULTURALLEVEL)
        {
            //01	博士研究生
            //02	硕士研究生
            //03	大学本科
            //04	大学专科
            //05	中专
            //06	高中
            //07	初中及以下
            //99	其他

            if (CULTURALLEVEL.Contains("博士"))
                return "01";
            else if (CULTURALLEVEL.Contains("硕士"))
                return "02";
            else if (CULTURALLEVEL.Contains("研究生"))
                return "02";
            else if (CULTURALLEVEL.Contains("本科"))
                return "03";
            else if (CULTURALLEVEL.Contains("专科"))
                return "04";
            else if (CULTURALLEVEL.Contains("中专"))
                return "05";
            else if (CULTURALLEVEL.Contains("高中"))
                return "06";
            else if (CULTURALLEVEL.Contains("初中"))
                return "07";
            else
                return "99";
        }

        /// <summary>
        /// 获取特种作业工种类别编码（char(2)）
        /// </summary>
        /// <param name="PostName">人员系统中工种名称</param>
        /// <returns>工种类别编码</returns>
        public static string GetTZZYPostCode(string PostName)
        {
            switch (PostName)
            {
                case "建筑电工":
                    return "01";
                case "建筑架子工（普通脚手架）":
                    return "02";
                case "建筑架子工（附着升降脚手架）":
                    return "03";
                case "建筑起重司索信号工":
                    return "04";
                case "建筑起重机械司机（塔式起重机）":
                    return "05";
                case "建筑起重机械司机（施工升降机）":
                    return "06";
                case "建筑起重机械司机（物料提升机）":
                    return "07";
                case "建筑起重机械安装拆卸工（塔式起重机）":
                    return "08";
                case "建筑起重机械安装拆卸工（施工升降机）":
                    return "09";
                case "建筑起重机械安装拆卸工（物料提升机）":
                    return "10";
                case "高处作业吊篮安装拆卸工":
                    return "11";
                default:
                    return "99";//经省级以上住房和城乡建设主管部门认定的其他工种类别
            }
        }

        /// <summary>
        /// 获取特种作业工种类别国标名称（char(2)）
        /// </summary>
        /// <param name="PostName">人员系统中工种名称</param>
        /// <returns>工种类别名称</returns>
        public static string GetTZZYPostName(string PostName)
        {
            switch (PostName)
            {
                case "建筑电工":
                    return "建筑电工";
                case "建筑架子工（普通脚手架）":
                    return "建筑架子工（普通脚手架）";
                case "建筑架子工（附着升降脚手架）":
                    return "建筑架子工（附着升降脚手架）";
                case "建筑起重司索信号工":
                    return "建筑起重司索信号工";
                case "建筑起重机械司机（塔式起重机）":
                    return "建筑起重机械司机（塔式起重机）";
                case "建筑起重机械司机（施工升降机）":
                    return "建筑起重机械司机（施工升降机）";
                case "建筑起重机械司机（物料提升机）":
                    return "建筑起重机械司机（物料提升机）";
                case "建筑起重机械安装拆卸工（塔式起重机）":
                    return "建筑起重机械安装拆卸工（塔式起重机）";
                case "建筑起重机械安装拆卸工（施工升降机）":
                    return "建筑起重机械安装拆卸工（施工升降机）";
                case "建筑起重机械安装拆卸工（物料提升机）":
                    return "建筑起重机械安装拆卸工（物料提升机）";
                case "高处作业吊篮安装拆卸工":
                    return "高处作业吊篮安装拆卸工";
                default:
                    return "经省级以上住房和城乡建设主管部门认定的其他工种类别";
            }
        }

        /// <summary>
        /// 获取特种作业国标证书状态编码
        /// </summary>
        /// <param name="CertState">人员资格系统中证书状态</param>
        /// <param name="ValidEndDate">有效期截止日期</param>
        /// <param name="PauseStatus">暂扣申报状态</param> 
        /// <returns>国标证书状态编码</returns>
        public static string Get_GB_Tzzy_CertStateCode(string CertState, DateTime ValidEndDate, string PauseStatus = "")
        {
            return (CertState == EnumManager.CertificateUpdateType.Logout ? "05"
                    : ValidEndDate < DateTime.Now.AddDays(-1) ? "05"
                    : PauseStatus == EnumManager.CertificatePauseStatus.ApplyPause ? "02"
                    : PauseStatus == EnumManager.CertificatePauseStatus.ApplyReturn ? "01"
                    : "01");//01有效,02	暂扣,03	吊销,04	撤销,05	注销,06	失效,99	其他
        }

        /// <summary>
        /// 获取特种作业国标证书状态名称
        /// </summary>
        /// <param name="CertState">人员资格系统中证书状态</param>
        /// <param name="ValidEndDate">有效期截止日期</param>
        /// <param name="PauseStatus">暂扣申报状态</param> 
        /// <returns>国标证书状态名称</returns>
        public static string Get_GB_Tzzy_CertStateDesc(string CertState, DateTime ValidEndDate, string PauseStatus="")
        {
            return (CertState == EnumManager.CertificateUpdateType.Logout ? "注销"
                : ValidEndDate < DateTime.Now.AddDays(-1) ? "注销"
                : PauseStatus == EnumManager.CertificatePauseStatus.ApplyPause ? "暂扣"
                : PauseStatus == EnumManager.CertificatePauseStatus.ApplyReturn ? "有效"
                : "有效");//01有效,02	暂扣,03	吊销,04	撤销,05	注销,06	失效,99	其他
        }


        public static DataTable GetListValidEndWarn(string workercertificatecode)
        {
            string sql = @"            
                select 'ej' as certType,'二级建造师' as PostTypeName,c.PSN_Name,c.PSN_RegisterNO,p.PRO_Profession,p.PRO_ValidityEnd
                    from [dbo].[COC_TOW_Person_BaseInfo] c 
                    inner join [dbo].[COC_TOW_Register_Profession] p on c.[PSN_ServerID] = p.[PSN_ServerID]
                    left join Apply a on c.PSN_RegisterNO = a.PSN_RegisterNo and a.ApplyType='延期注册' and a.ApplyTime > dateadd(month,-4,p.PRO_ValidityEnd)
                    where c.PSN_CertificateNO like '{2}' and p.[PRO_ValidityEnd] between '{0}' and '{1}' and  c.PSN_RegisteType < '07' and a.applyid is null
                union
                select 'ez' as certType,'二级造价工程师' as PostTypeName,c.PSN_Name,c.PSN_RegisterNO,c.PSN_RegisteProfession,c.PSN_CertificateValidity
                    from [dbo].[zjs_Certificate] c 
                    left join [dbo].[zjs_Apply] a on c.PSN_RegisterNO = a.PSN_RegisterNo and a.ApplyType='延期注册' and a.ApplyTime > dateadd(month,-4,c.[PSN_CertificateValidity])
                    where c.PSN_CertificateNO like '{2}' and c.[PSN_CertificateValidity] between '{0}' and '{1}' and  c.PSN_RegisteType < '07' and a.applyid is null
                union
                select 'slr' as certType,'安全生产三类人' as PostTypeName,c.WORKERNAME,c.CERTIFICATECODE,c.POSTNAME,c.VALIDENDDATE
                    from [dbo].[CERTIFICATE] c 
                    left join [dbo].[CERTIFICATECONTINUE] a on c.[CERTIFICATEID] = a.[CERTIFICATEID]  and a.APPLYDATE > dateadd(month,-4,c.VALIDENDDATE)
                    left join [VIEW_UNIT_LSGX] s on c.UNITCODE = s.unitcode
                    where c.WorkerCertificateCode like '{2}' and c.POSTTYPEID=1 and c.VALIDENDDATE between '{0}' and '{1}' 
                    and  c.[STATUS] <> '待审批' AND c.[STATUS] <> '待进京审批' AND c.[STATUS] <> '离京变更' AND c.[STATUS] <> '注销'
                    and a.CertificateContinueid is null
                union
                    select 'tzzy' as certType,'特种作业' as PostTypeName,c.WORKERNAME,c.CERTIFICATECODE,c.POSTNAME,c.VALIDENDDATE
                    from [dbo].[CERTIFICATE] c 
                    left join [dbo].[CERTIFICATECONTINUE] a on c.[CERTIFICATEID] = a.[CERTIFICATEID]  and a.APPLYDATE > dateadd(month,-4,c.VALIDENDDATE)
                    where c.WorkerCertificateCode like '{2}' and c.POSTTYPEID=2 and c.VALIDENDDATE between '{0}' and '{1}'
	                and  c.[STATUS] <> '待审批' AND c.[STATUS] <> '注销' and a.CertificateContinueid is null";

            return CommonDAL.GetDataTable(string.Format(sql, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.AddMonths(3).ToString("yyyy-MM-dd"), workercertificatecode));
        }

      

        //二建过期预警List
        public static DataTable GetListValidEndWarnEJ(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            string td = @"[dbo].[COC_TOW_Person_BaseInfo] c 
                            inner join [dbo].[COC_TOW_Register_Profession] p on c.[PSN_ServerID] = p.[PSN_ServerID]
                            left join Apply a on c.PSN_RegisterNO = a.PSN_RegisterNo and a.ApplyType='延期注册' and a.ApplyTime > dateadd(month,-4,p.PRO_ValidityEnd)";

            return CommonDAL.GetDataTable(startRowIndex, maximumRows, td, "'ej' as certType,'二级建造师' as PostTypeName,c.PSN_Name,c.PSN_RegisterNO,p.PRO_Profession,p.PRO_ValidityEnd", filterWhereString, orderBy == "" ? " c.PSN_RegisterNO" : orderBy);
        }
        //二建过期预警count
        public static int SelectCountValidEndWarnEJ(string filterWhereString)
        {
            string td = @"[dbo].[COC_TOW_Person_BaseInfo] c 
                            inner join [dbo].[COC_TOW_Register_Profession] p on c.[PSN_ServerID] = p.[PSN_ServerID]
                            left join Apply a on c.PSN_RegisterNO = a.PSN_RegisterNo and a.ApplyType='延期注册' and a.ApplyTime > dateadd(month,-4,p.PRO_ValidityEnd)";

            return CommonDAL.SelectRowCount(td, filterWhereString);
        }
        //二造过期预警List
        public static DataTable GetListValidEndWarnEZ(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            string td = @"[dbo].[zjs_Certificate] c 
                          left join [dbo].[zjs_Apply] a on c.PSN_RegisterNO = a.PSN_RegisterNo 
                          and a.ApplyType='延期注册' and a.ApplyTime > dateadd(month,-4,c.[PSN_CertificateValidity])";

            return CommonDAL.GetDataTable(startRowIndex, maximumRows, td, "'ez' as certType,'二级造价工程师' as PostTypeName,c.PSN_Name,c.PSN_RegisterNO,c.PSN_RegisteProfession as PRO_Profession,c.PSN_CertificateValidity as PRO_ValidityEnd", filterWhereString, orderBy == "" ? " c.PSN_RegisterNO" : orderBy);
        }
        //二造过期预警count
        public static int SelectCountValidEndWarnEZ(string filterWhereString)
        {
            string td = @"[dbo].[zjs_Certificate] c 
                          left join [dbo].[zjs_Apply] a on c.PSN_RegisterNO = a.PSN_RegisterNo 
                          and a.ApplyType='延期注册' and a.ApplyTime > dateadd(month,-4,c.[PSN_RegisteProfession])";

            return CommonDAL.SelectRowCount(td, filterWhereString);
        }

        //三类人过期预警List
        public static DataTable GetListValidEndWarnSLR(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            string td = @"[dbo].[CERTIFICATE] c 
                         left join [dbo].[CERTIFICATECONTINUE] a 
                        on c.posttypeid=1 and c.[CERTIFICATEID] = a.[CERTIFICATEID]  and a.APPLYDATE > dateadd(month,-4,c.VALIDENDDATE)";

            return CommonDAL.GetDataTable(startRowIndex, maximumRows, td, "'slr' as certType,'安全生产三类人' as PostTypeName,c.WORKERNAME as PSN_Name,c.CERTIFICATECODE as PSN_RegisterNO,c.POSTNAME as PRO_Profession,c.VALIDENDDATE as PRO_ValidityEnd", filterWhereString, orderBy == "" ? " c.CERTIFICATECODE" : orderBy);
                                                                                        
        }
        //三类人过期预警count
        public static int SelectCountValidEndWarnSLR(string filterWhereString)
        {
            string td = @"[dbo].[CERTIFICATE] c 
                          left join [dbo].[CERTIFICATECONTINUE] a 
                          on c.posttypeid=1 and c.[CERTIFICATEID] = a.[CERTIFICATEID]  and a.APPLYDATE > dateadd(month,-4,c.VALIDENDDATE)";

            return CommonDAL.SelectRowCount(td, filterWhereString);
        }

        //特种作业过期预警List
        public static DataTable GetListValidEndWarnTZZY(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            string td = @"[dbo].[CERTIFICATE] c 
                         left join [dbo].[CERTIFICATECONTINUE] a 
                        on c.posttypeid=2 and c.[CERTIFICATEID] = a.[CERTIFICATEID]  and a.APPLYDATE > dateadd(month,-4,c.VALIDENDDATE)";

            return CommonDAL.GetDataTable(startRowIndex, maximumRows, td, "'tzzy' as certType,'特种作业' as PostTypeName,c.WORKERNAME as PSN_Name,c.CERTIFICATECODE as PSN_RegisterNO,c.POSTNAME as PRO_Profession,c.VALIDENDDATE as PRO_ValidityEnd", filterWhereString, orderBy == "" ? " c.CERTIFICATECODE" : orderBy);

        }
        //特种作业过期预警count
        public static int SelectCountValidEndWarnTZZY(string filterWhereString)
        {
            string td = @"[dbo].[CERTIFICATE] c 
                          left join [dbo].[CERTIFICATECONTINUE] a 
                          on c.posttypeid=2 and c.[CERTIFICATEID] = a.[CERTIFICATEID]  and a.APPLYDATE > dateadd(month,-4,c.VALIDENDDATE)";

            return CommonDAL.SelectRowCount(td, filterWhereString);
        }
        #endregion
    }
}