using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--CertificateMergeDAL(从业人员证书合并申请)
	/// </summary>
    public class CertificateMergeDAL
    {
        /// <summary>
        /// 
        /// </summary>
        public CertificateMergeDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_CertificateMergeMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(CertificateMergeMDL _CertificateMergeMDL)
		{
		    return Insert(null,_CertificateMergeMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_CertificateMergeMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,CertificateMergeMDL _CertificateMergeMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.CertificateMerge(WorkerID,CertificateType,WorkerName,WorkerCertificateCode,Sex,Birthday,FacePhoto,UnitName,UnitCode,CertificateID1,PostName1,CertificateCode1,ConferDate1,ValidStartDate1,ValidEndDate1,PostName2,CertificateID2,CertificateCode2,ConferDate2,ValidStartDate2,ValidEndDate2,ConferUnit,ApplyDate,ApplyMan,UnitCheckTime,UnitAdvise,CheckMan,CheckAdvise,CheckDate,ApplyStatus,NewCertificateID,NewCertificateCode,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime)
			VALUES (@WorkerID,@CertificateType,@WorkerName,@WorkerCertificateCode,@Sex,@Birthday,@FacePhoto,@UnitName,@UnitCode,@CertificateID1,@PostName1,@CertificateCode1,@ConferDate1,@ValidStartDate1,@ValidEndDate1,@PostName2,@CertificateID2,@CertificateCode2,@ConferDate2,@ValidStartDate2,@ValidEndDate2,@ConferUnit,@ApplyDate,@ApplyMan,@UnitCheckTime,@UnitAdvise,@CheckMan,@CheckAdvise,@CheckDate,@ApplyStatus,@NewCertificateID,@NewCertificateCode,@CreatePersonID,@CreateTime,@ModifyPersonID,@ModifyTime);SELECT @ApplyID = @@IDENTITY";

		    List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateOutParameter("ApplyID", DbType.Int64));
			p.Add(db.CreateParameter("WorkerID",DbType.Int64, _CertificateMergeMDL.WorkerID));
			p.Add(db.CreateParameter("CertificateType",DbType.String, _CertificateMergeMDL.CertificateType));
			p.Add(db.CreateParameter("WorkerName",DbType.String, _CertificateMergeMDL.WorkerName));
			p.Add(db.CreateParameter("WorkerCertificateCode",DbType.String, _CertificateMergeMDL.WorkerCertificateCode));
			p.Add(db.CreateParameter("Sex",DbType.String, _CertificateMergeMDL.Sex));
			p.Add(db.CreateParameter("Birthday",DbType.DateTime, _CertificateMergeMDL.Birthday));
			p.Add(db.CreateParameter("FacePhoto",DbType.String, _CertificateMergeMDL.FacePhoto));
			p.Add(db.CreateParameter("UnitName",DbType.String, _CertificateMergeMDL.UnitName));
			p.Add(db.CreateParameter("UnitCode",DbType.String, _CertificateMergeMDL.UnitCode));
			p.Add(db.CreateParameter("CertificateID1",DbType.Int64, _CertificateMergeMDL.CertificateID1));
			p.Add(db.CreateParameter("PostName1",DbType.String, _CertificateMergeMDL.PostName1));
			p.Add(db.CreateParameter("CertificateCode1",DbType.String, _CertificateMergeMDL.CertificateCode1));
			p.Add(db.CreateParameter("ConferDate1",DbType.DateTime, _CertificateMergeMDL.ConferDate1));
			p.Add(db.CreateParameter("ValidStartDate1",DbType.DateTime, _CertificateMergeMDL.ValidStartDate1));
			p.Add(db.CreateParameter("ValidEndDate1",DbType.DateTime, _CertificateMergeMDL.ValidEndDate1));
			p.Add(db.CreateParameter("PostName2",DbType.String, _CertificateMergeMDL.PostName2));
			p.Add(db.CreateParameter("CertificateID2",DbType.Int64, _CertificateMergeMDL.CertificateID2));
			p.Add(db.CreateParameter("CertificateCode2",DbType.String, _CertificateMergeMDL.CertificateCode2));
			p.Add(db.CreateParameter("ConferDate2",DbType.DateTime, _CertificateMergeMDL.ConferDate2));
			p.Add(db.CreateParameter("ValidStartDate2",DbType.DateTime, _CertificateMergeMDL.ValidStartDate2));
			p.Add(db.CreateParameter("ValidEndDate2",DbType.DateTime, _CertificateMergeMDL.ValidEndDate2));
			p.Add(db.CreateParameter("ConferUnit",DbType.String, _CertificateMergeMDL.ConferUnit));
			p.Add(db.CreateParameter("ApplyDate",DbType.DateTime, _CertificateMergeMDL.ApplyDate));
			p.Add(db.CreateParameter("ApplyMan",DbType.String, _CertificateMergeMDL.ApplyMan));
			p.Add(db.CreateParameter("UnitCheckTime",DbType.DateTime, _CertificateMergeMDL.UnitCheckTime));
			p.Add(db.CreateParameter("UnitAdvise",DbType.String, _CertificateMergeMDL.UnitAdvise));
			p.Add(db.CreateParameter("CheckMan",DbType.String, _CertificateMergeMDL.CheckMan));
			p.Add(db.CreateParameter("CheckAdvise",DbType.String, _CertificateMergeMDL.CheckAdvise));
			p.Add(db.CreateParameter("CheckDate",DbType.DateTime, _CertificateMergeMDL.CheckDate));
			p.Add(db.CreateParameter("ApplyStatus",DbType.String, _CertificateMergeMDL.ApplyStatus));
			p.Add(db.CreateParameter("NewCertificateID",DbType.Int64, _CertificateMergeMDL.NewCertificateID));
			p.Add(db.CreateParameter("NewCertificateCode",DbType.String, _CertificateMergeMDL.NewCertificateCode));
			p.Add(db.CreateParameter("CreatePersonID",DbType.Int64, _CertificateMergeMDL.CreatePersonID));
			p.Add(db.CreateParameter("CreateTime",DbType.DateTime, _CertificateMergeMDL.CreateTime));
			p.Add(db.CreateParameter("ModifyPersonID",DbType.Int64, _CertificateMergeMDL.ModifyPersonID));
			p.Add(db.CreateParameter("ModifyTime",DbType.DateTime, _CertificateMergeMDL.ModifyTime));
            int rtn = db.GetExcuteNonQuery(tran, sql, p.ToArray());
            _CertificateMergeMDL.ApplyID = Convert.ToInt64(p[0].Value);
            return rtn;
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_CertificateMergeMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(CertificateMergeMDL _CertificateMergeMDL)
		{
			return Update(null,_CertificateMergeMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_CertificateMergeMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,CertificateMergeMDL _CertificateMergeMDL)
		{
			string sql = @"
			UPDATE dbo.CertificateMerge
				SET	WorkerID = @WorkerID,CertificateType = @CertificateType,WorkerName = @WorkerName,WorkerCertificateCode = @WorkerCertificateCode,Sex = @Sex,Birthday = @Birthday,FacePhoto = @FacePhoto,UnitName = @UnitName,UnitCode = @UnitCode,CertificateID1 = @CertificateID1,PostName1 = @PostName1,CertificateCode1 = @CertificateCode1,ConferDate1 = @ConferDate1,ValidStartDate1 = @ValidStartDate1,ValidEndDate1 = @ValidEndDate1,PostName2 = @PostName2,CertificateID2 = @CertificateID2,CertificateCode2 = @CertificateCode2,ConferDate2 = @ConferDate2,ValidStartDate2 = @ValidStartDate2,ValidEndDate2 = @ValidEndDate2,ConferUnit = @ConferUnit,ApplyDate = @ApplyDate,ApplyMan = @ApplyMan,UnitCheckTime = @UnitCheckTime,UnitAdvise = @UnitAdvise,CheckMan = @CheckMan,CheckAdvise = @CheckAdvise,CheckDate = @CheckDate,ApplyStatus = @ApplyStatus,NewCertificateID = @NewCertificateID,NewCertificateCode = @NewCertificateCode,CreatePersonID = @CreatePersonID,CreateTime = @CreateTime,ModifyPersonID = @ModifyPersonID,ModifyTime = @ModifyTime
			WHERE
				ApplyID = @ApplyID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ApplyID",DbType.Int64, _CertificateMergeMDL.ApplyID));
			p.Add(db.CreateParameter("WorkerID",DbType.Int64, _CertificateMergeMDL.WorkerID));
			p.Add(db.CreateParameter("CertificateType",DbType.String, _CertificateMergeMDL.CertificateType));
			p.Add(db.CreateParameter("WorkerName",DbType.String, _CertificateMergeMDL.WorkerName));
			p.Add(db.CreateParameter("WorkerCertificateCode",DbType.String, _CertificateMergeMDL.WorkerCertificateCode));
			p.Add(db.CreateParameter("Sex",DbType.String, _CertificateMergeMDL.Sex));
			p.Add(db.CreateParameter("Birthday",DbType.DateTime, _CertificateMergeMDL.Birthday));
			p.Add(db.CreateParameter("FacePhoto",DbType.String, _CertificateMergeMDL.FacePhoto));
			p.Add(db.CreateParameter("UnitName",DbType.String, _CertificateMergeMDL.UnitName));
			p.Add(db.CreateParameter("UnitCode",DbType.String, _CertificateMergeMDL.UnitCode));
			p.Add(db.CreateParameter("CertificateID1",DbType.Int64, _CertificateMergeMDL.CertificateID1));
			p.Add(db.CreateParameter("PostName1",DbType.String, _CertificateMergeMDL.PostName1));
			p.Add(db.CreateParameter("CertificateCode1",DbType.String, _CertificateMergeMDL.CertificateCode1));
			p.Add(db.CreateParameter("ConferDate1",DbType.DateTime, _CertificateMergeMDL.ConferDate1));
			p.Add(db.CreateParameter("ValidStartDate1",DbType.DateTime, _CertificateMergeMDL.ValidStartDate1));
			p.Add(db.CreateParameter("ValidEndDate1",DbType.DateTime, _CertificateMergeMDL.ValidEndDate1));
			p.Add(db.CreateParameter("PostName2",DbType.String, _CertificateMergeMDL.PostName2));
			p.Add(db.CreateParameter("CertificateID2",DbType.Int64, _CertificateMergeMDL.CertificateID2));
			p.Add(db.CreateParameter("CertificateCode2",DbType.String, _CertificateMergeMDL.CertificateCode2));
			p.Add(db.CreateParameter("ConferDate2",DbType.DateTime, _CertificateMergeMDL.ConferDate2));
			p.Add(db.CreateParameter("ValidStartDate2",DbType.DateTime, _CertificateMergeMDL.ValidStartDate2));
			p.Add(db.CreateParameter("ValidEndDate2",DbType.DateTime, _CertificateMergeMDL.ValidEndDate2));
			p.Add(db.CreateParameter("ConferUnit",DbType.String, _CertificateMergeMDL.ConferUnit));
			p.Add(db.CreateParameter("ApplyDate",DbType.DateTime, _CertificateMergeMDL.ApplyDate));
			p.Add(db.CreateParameter("ApplyMan",DbType.String, _CertificateMergeMDL.ApplyMan));
			p.Add(db.CreateParameter("UnitCheckTime",DbType.DateTime, _CertificateMergeMDL.UnitCheckTime));
			p.Add(db.CreateParameter("UnitAdvise",DbType.String, _CertificateMergeMDL.UnitAdvise));
			p.Add(db.CreateParameter("CheckMan",DbType.String, _CertificateMergeMDL.CheckMan));
			p.Add(db.CreateParameter("CheckAdvise",DbType.String, _CertificateMergeMDL.CheckAdvise));
			p.Add(db.CreateParameter("CheckDate",DbType.DateTime, _CertificateMergeMDL.CheckDate));
			p.Add(db.CreateParameter("ApplyStatus",DbType.String, _CertificateMergeMDL.ApplyStatus));
			p.Add(db.CreateParameter("NewCertificateID",DbType.Int64, _CertificateMergeMDL.NewCertificateID));
			p.Add(db.CreateParameter("NewCertificateCode",DbType.String, _CertificateMergeMDL.NewCertificateCode));
			p.Add(db.CreateParameter("CreatePersonID",DbType.Int64, _CertificateMergeMDL.CreatePersonID));
			p.Add(db.CreateParameter("CreateTime",DbType.DateTime, _CertificateMergeMDL.CreateTime));
			p.Add(db.CreateParameter("ModifyPersonID",DbType.Int64, _CertificateMergeMDL.ModifyPersonID));
			p.Add(db.CreateParameter("ModifyTime",DbType.DateTime, _CertificateMergeMDL.ModifyTime));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="CertificateMergeID">主键</param>
		/// <returns></returns>
        public static int Delete( long ApplyID )
		{
			return Delete(null, ApplyID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ApplyID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, long ApplyID)
		{
			string sql=@"DELETE FROM dbo.CertificateMerge WHERE ApplyID = @ApplyID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ApplyID",DbType.Int64,ApplyID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="_CertificateMergeMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(CertificateMergeMDL _CertificateMergeMDL)
		{
			return Delete(null,_CertificateMergeMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_CertificateMergeMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,CertificateMergeMDL _CertificateMergeMDL)
		{
			string sql=@"DELETE FROM dbo.CertificateMerge WHERE ApplyID = @ApplyID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ApplyID",DbType.Int64,_CertificateMergeMDL.ApplyID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ApplyID">主键</param>
        public static CertificateMergeMDL GetObject( long ApplyID )
		{
			string sql=@"
			SELECT ApplyID,WorkerID,CertificateType,WorkerName,WorkerCertificateCode,Sex,Birthday,FacePhoto,UnitName,UnitCode,CertificateID1,PostName1,CertificateCode1,ConferDate1,ValidStartDate1,ValidEndDate1,PostName2,CertificateID2,CertificateCode2,ConferDate2,ValidStartDate2,ValidEndDate2,ConferUnit,ApplyDate,ApplyMan,UnitCheckTime,UnitAdvise,CheckMan,CheckAdvise,CheckDate,ApplyStatus,NewCertificateID,NewCertificateCode,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime
			FROM dbo.CertificateMerge
			WHERE ApplyID = @ApplyID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.Int64, ApplyID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                CertificateMergeMDL _CertificateMergeMDL = null;
                if (reader.Read())
                {
                    _CertificateMergeMDL = new CertificateMergeMDL();
					if (reader["ApplyID"] != DBNull.Value) _CertificateMergeMDL.ApplyID = Convert.ToInt64(reader["ApplyID"]);
					if (reader["WorkerID"] != DBNull.Value) _CertificateMergeMDL.WorkerID = Convert.ToInt64(reader["WorkerID"]);
					if (reader["CertificateType"] != DBNull.Value) _CertificateMergeMDL.CertificateType = Convert.ToString(reader["CertificateType"]);
					if (reader["WorkerName"] != DBNull.Value) _CertificateMergeMDL.WorkerName = Convert.ToString(reader["WorkerName"]);
					if (reader["WorkerCertificateCode"] != DBNull.Value) _CertificateMergeMDL.WorkerCertificateCode = Convert.ToString(reader["WorkerCertificateCode"]);
					if (reader["Sex"] != DBNull.Value) _CertificateMergeMDL.Sex = Convert.ToString(reader["Sex"]);
					if (reader["Birthday"] != DBNull.Value) _CertificateMergeMDL.Birthday = Convert.ToDateTime(reader["Birthday"]);
					if (reader["FacePhoto"] != DBNull.Value) _CertificateMergeMDL.FacePhoto = Convert.ToString(reader["FacePhoto"]);
					if (reader["UnitName"] != DBNull.Value) _CertificateMergeMDL.UnitName = Convert.ToString(reader["UnitName"]);
					if (reader["UnitCode"] != DBNull.Value) _CertificateMergeMDL.UnitCode = Convert.ToString(reader["UnitCode"]);
					if (reader["CertificateID1"] != DBNull.Value) _CertificateMergeMDL.CertificateID1 = Convert.ToInt64(reader["CertificateID1"]);
					if (reader["PostName1"] != DBNull.Value) _CertificateMergeMDL.PostName1 = Convert.ToString(reader["PostName1"]);
					if (reader["CertificateCode1"] != DBNull.Value) _CertificateMergeMDL.CertificateCode1 = Convert.ToString(reader["CertificateCode1"]);
					if (reader["ConferDate1"] != DBNull.Value) _CertificateMergeMDL.ConferDate1 = Convert.ToDateTime(reader["ConferDate1"]);
					if (reader["ValidStartDate1"] != DBNull.Value) _CertificateMergeMDL.ValidStartDate1 = Convert.ToDateTime(reader["ValidStartDate1"]);
					if (reader["ValidEndDate1"] != DBNull.Value) _CertificateMergeMDL.ValidEndDate1 = Convert.ToDateTime(reader["ValidEndDate1"]);
					if (reader["PostName2"] != DBNull.Value) _CertificateMergeMDL.PostName2 = Convert.ToString(reader["PostName2"]);
					if (reader["CertificateID2"] != DBNull.Value) _CertificateMergeMDL.CertificateID2 = Convert.ToInt64(reader["CertificateID2"]);
					if (reader["CertificateCode2"] != DBNull.Value) _CertificateMergeMDL.CertificateCode2 = Convert.ToString(reader["CertificateCode2"]);
					if (reader["ConferDate2"] != DBNull.Value) _CertificateMergeMDL.ConferDate2 = Convert.ToDateTime(reader["ConferDate2"]);
					if (reader["ValidStartDate2"] != DBNull.Value) _CertificateMergeMDL.ValidStartDate2 = Convert.ToDateTime(reader["ValidStartDate2"]);
					if (reader["ValidEndDate2"] != DBNull.Value) _CertificateMergeMDL.ValidEndDate2 = Convert.ToDateTime(reader["ValidEndDate2"]);
					if (reader["ConferUnit"] != DBNull.Value) _CertificateMergeMDL.ConferUnit = Convert.ToString(reader["ConferUnit"]);
					if (reader["ApplyDate"] != DBNull.Value) _CertificateMergeMDL.ApplyDate = Convert.ToDateTime(reader["ApplyDate"]);
					if (reader["ApplyMan"] != DBNull.Value) _CertificateMergeMDL.ApplyMan = Convert.ToString(reader["ApplyMan"]);
					if (reader["UnitCheckTime"] != DBNull.Value) _CertificateMergeMDL.UnitCheckTime = Convert.ToDateTime(reader["UnitCheckTime"]);
					if (reader["UnitAdvise"] != DBNull.Value) _CertificateMergeMDL.UnitAdvise = Convert.ToString(reader["UnitAdvise"]);
					if (reader["CheckMan"] != DBNull.Value) _CertificateMergeMDL.CheckMan = Convert.ToString(reader["CheckMan"]);
					if (reader["CheckAdvise"] != DBNull.Value) _CertificateMergeMDL.CheckAdvise = Convert.ToString(reader["CheckAdvise"]);
					if (reader["CheckDate"] != DBNull.Value) _CertificateMergeMDL.CheckDate = Convert.ToDateTime(reader["CheckDate"]);
					if (reader["ApplyStatus"] != DBNull.Value) _CertificateMergeMDL.ApplyStatus = Convert.ToString(reader["ApplyStatus"]);
					if (reader["NewCertificateID"] != DBNull.Value) _CertificateMergeMDL.NewCertificateID = Convert.ToInt64(reader["NewCertificateID"]);
					if (reader["NewCertificateCode"] != DBNull.Value) _CertificateMergeMDL.NewCertificateCode = Convert.ToString(reader["NewCertificateCode"]);
					if (reader["CreatePersonID"] != DBNull.Value) _CertificateMergeMDL.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
					if (reader["CreateTime"] != DBNull.Value) _CertificateMergeMDL.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
					if (reader["ModifyPersonID"] != DBNull.Value) _CertificateMergeMDL.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
					if (reader["ModifyTime"] != DBNull.Value) _CertificateMergeMDL.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                }
				reader.Close();
                db.Close();
                return _CertificateMergeMDL;
            }
		}
        /// <summary>
        /// 根据主键获取单个实体（在途业务）
        /// </summary>
        /// <param name="WorkerCertificateCode">证件号码</param>
        public static CertificateMergeMDL GetObject(string WorkerCertificateCode)
        {
            string sql = @"
			SELECT ApplyID,WorkerID,CertificateType,WorkerName,WorkerCertificateCode,Sex,Birthday,FacePhoto,UnitName,UnitCode,CertificateID1,PostName1,CertificateCode1,ConferDate1,ValidStartDate1,ValidEndDate1,PostName2,CertificateID2,CertificateCode2,ConferDate2,ValidStartDate2,ValidEndDate2,ConferUnit,ApplyDate,ApplyMan,UnitCheckTime,UnitAdvise,CheckMan,CheckAdvise,CheckDate,ApplyStatus,NewCertificateID,NewCertificateCode,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime
			FROM dbo.CertificateMerge
			WHERE WorkerCertificateCode = @WorkerCertificateCode";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("WorkerCertificateCode", DbType.String, WorkerCertificateCode));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                CertificateMergeMDL _CertificateMergeMDL = null;
                if (reader.Read())
                {
                    _CertificateMergeMDL = new CertificateMergeMDL();
                    if (reader["ApplyID"] != DBNull.Value) _CertificateMergeMDL.ApplyID = Convert.ToInt64(reader["ApplyID"]);
                    if (reader["WorkerID"] != DBNull.Value) _CertificateMergeMDL.WorkerID = Convert.ToInt64(reader["WorkerID"]);
                    if (reader["CertificateType"] != DBNull.Value) _CertificateMergeMDL.CertificateType = Convert.ToString(reader["CertificateType"]);
                    if (reader["WorkerName"] != DBNull.Value) _CertificateMergeMDL.WorkerName = Convert.ToString(reader["WorkerName"]);
                    if (reader["WorkerCertificateCode"] != DBNull.Value) _CertificateMergeMDL.WorkerCertificateCode = Convert.ToString(reader["WorkerCertificateCode"]);
                    if (reader["Sex"] != DBNull.Value) _CertificateMergeMDL.Sex = Convert.ToString(reader["Sex"]);
                    if (reader["Birthday"] != DBNull.Value) _CertificateMergeMDL.Birthday = Convert.ToDateTime(reader["Birthday"]);
                    if (reader["FacePhoto"] != DBNull.Value) _CertificateMergeMDL.FacePhoto = Convert.ToString(reader["FacePhoto"]);
                    if (reader["UnitName"] != DBNull.Value) _CertificateMergeMDL.UnitName = Convert.ToString(reader["UnitName"]);
                    if (reader["UnitCode"] != DBNull.Value) _CertificateMergeMDL.UnitCode = Convert.ToString(reader["UnitCode"]);
                    if (reader["CertificateID1"] != DBNull.Value) _CertificateMergeMDL.CertificateID1 = Convert.ToInt64(reader["CertificateID1"]);
                    if (reader["PostName1"] != DBNull.Value) _CertificateMergeMDL.PostName1 = Convert.ToString(reader["PostName1"]);
                    if (reader["CertificateCode1"] != DBNull.Value) _CertificateMergeMDL.CertificateCode1 = Convert.ToString(reader["CertificateCode1"]);
                    if (reader["ConferDate1"] != DBNull.Value) _CertificateMergeMDL.ConferDate1 = Convert.ToDateTime(reader["ConferDate1"]);
                    if (reader["ValidStartDate1"] != DBNull.Value) _CertificateMergeMDL.ValidStartDate1 = Convert.ToDateTime(reader["ValidStartDate1"]);
                    if (reader["ValidEndDate1"] != DBNull.Value) _CertificateMergeMDL.ValidEndDate1 = Convert.ToDateTime(reader["ValidEndDate1"]);
                    if (reader["PostName2"] != DBNull.Value) _CertificateMergeMDL.PostName2 = Convert.ToString(reader["PostName2"]);
                    if (reader["CertificateID2"] != DBNull.Value) _CertificateMergeMDL.CertificateID2 = Convert.ToInt64(reader["CertificateID2"]);
                    if (reader["CertificateCode2"] != DBNull.Value) _CertificateMergeMDL.CertificateCode2 = Convert.ToString(reader["CertificateCode2"]);
                    if (reader["ConferDate2"] != DBNull.Value) _CertificateMergeMDL.ConferDate2 = Convert.ToDateTime(reader["ConferDate2"]);
                    if (reader["ValidStartDate2"] != DBNull.Value) _CertificateMergeMDL.ValidStartDate2 = Convert.ToDateTime(reader["ValidStartDate2"]);
                    if (reader["ValidEndDate2"] != DBNull.Value) _CertificateMergeMDL.ValidEndDate2 = Convert.ToDateTime(reader["ValidEndDate2"]);
                    if (reader["ConferUnit"] != DBNull.Value) _CertificateMergeMDL.ConferUnit = Convert.ToString(reader["ConferUnit"]);
                    if (reader["ApplyDate"] != DBNull.Value) _CertificateMergeMDL.ApplyDate = Convert.ToDateTime(reader["ApplyDate"]);
                    if (reader["ApplyMan"] != DBNull.Value) _CertificateMergeMDL.ApplyMan = Convert.ToString(reader["ApplyMan"]);
                    if (reader["UnitCheckTime"] != DBNull.Value) _CertificateMergeMDL.UnitCheckTime = Convert.ToDateTime(reader["UnitCheckTime"]);
                    if (reader["UnitAdvise"] != DBNull.Value) _CertificateMergeMDL.UnitAdvise = Convert.ToString(reader["UnitAdvise"]);
                    if (reader["CheckMan"] != DBNull.Value) _CertificateMergeMDL.CheckMan = Convert.ToString(reader["CheckMan"]);
                    if (reader["CheckAdvise"] != DBNull.Value) _CertificateMergeMDL.CheckAdvise = Convert.ToString(reader["CheckAdvise"]);
                    if (reader["CheckDate"] != DBNull.Value) _CertificateMergeMDL.CheckDate = Convert.ToDateTime(reader["CheckDate"]);
                    if (reader["ApplyStatus"] != DBNull.Value) _CertificateMergeMDL.ApplyStatus = Convert.ToString(reader["ApplyStatus"]);
                    if (reader["NewCertificateID"] != DBNull.Value) _CertificateMergeMDL.NewCertificateID = Convert.ToInt64(reader["NewCertificateID"]);
                    if (reader["NewCertificateCode"] != DBNull.Value) _CertificateMergeMDL.NewCertificateCode = Convert.ToString(reader["NewCertificateCode"]);
                    if (reader["CreatePersonID"] != DBNull.Value) _CertificateMergeMDL.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                    if (reader["CreateTime"] != DBNull.Value) _CertificateMergeMDL.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                    if (reader["ModifyPersonID"] != DBNull.Value) _CertificateMergeMDL.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                    if (reader["ModifyTime"] != DBNull.Value) _CertificateMergeMDL.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                }
                reader.Close();
                db.Close();
                return _CertificateMergeMDL;
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
		/// <summary>
		public static DataTable GetList(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.CertificateMerge", "*", filterWhereString, orderBy == "" ? " ApplyID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.CertificateMerge", filterWhereString);
        }

        /// <summary>
        /// 判断证书是否正在申办证书合并
        /// </summary>
        /// <param name="CertificateCode">证书编号</param>
        /// <returns>有在途申请返回true，否则返回false</returns>
        public static bool IfExistsApplying(string CertificateCode)
        {
            int count =  CommonDAL.SelectRowCount("CertificateMerge",string.Format(" and ([CertificateCode1]='{0}' or [CertificateCode2]='{0}') and [ApplyStatus] <>'已决定'", CertificateCode));
            if (count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 判断证书是否正在申办证书合并
        /// </summary>
        /// <param name="CertificateID">证书ID</param>
        /// <returns>有在途申请返回true，否则返回false</returns>
        public static bool IfExistsApplying(long CertificateID)
        {
            int count = CommonDAL.SelectRowCount("CertificateMerge", string.Format(" and ([CertificateID1]={0} or [CertificateID2]={0}) and [ApplyStatus] <>'已决定'", CertificateID));
            if (count > 0)
                return true;
            else
                return false;
        }
       
        
        #region 自定义方法
        /// <summary>
        /// 获取证书合并申请审批记录集合
        /// </summary>
        /// <param name="ApplyID">申请ID</param>
        /// <returns>审批历史记录集合</returns>
        public static DataTable GetCheckHistoryList(long ApplyID)
        {
            string sql = @"select row_number() over(order by ActionData, Action desc) as RowNo ,t.* from 
                            (
                                 SELECT '个人发起申请' as 'Action', [APPLYDATE] as ActionData, '已提交' as ActionResult, '待单位确认' as ActionRemark, [ApplyMan] as ActionMan 
                                 FROM [dbo].[CertificateMerge] where [ApplyID]={0}  
                                  union all
                                   SELECT '单位确认' as 'Action', [UnitCheckTime] as ActionData,'已确认' ActionResult,[UnitAdvise] as ActionRemark, [UNITNAME] as ActionMan 
                                 FROM [dbo].[CertificateMerge] where [ApplyID]={0} and [UnitCheckTime] >'2019-1-1'
                                   union all 
	                                SELECT '市建委决定' as 'Action', [CheckDATE] as ActionData,case [CheckAdvise] when '通过' then '已决定' else '决定未通过' end ActionResult,[CheckAdvise] + case when NewCertificateCode is not null then '【新证书：' +NewCertificateCode +'】' else '' end as ActionRemark , [CheckMAN] as ActionMan 
                                 FROM [dbo].[CertificateMerge] where [ApplyID]={0} and [CheckDATE] >'1950-1-1'
                                 union all 
                                    SELECT '住建部核准' as 'Action', 
	                                        case when c.ZZUrlUpTime > b.[CheckDATE] then  dateadd(hour,1,b.[CheckDATE]) 
			                                        when  Convert(varchar(10),b.[CheckDATE],21) = Convert(varchar(10),c.CHECKDATE,21) and  c.EleCertErrTime > b.[CheckDATE] then c.EleCertErrTime
			                                        else null
	                                        end as ActionData,
                                            case when c.ZZUrlUpTime > b.[CheckDATE] then  '已核准'
			                                        when  Convert(varchar(10),b.[CheckDATE],21) = Convert(varchar(10),c.CHECKDATE,21) and  c.EleCertErrTime > b.[CheckDATE] then '核准未通过'
			                                        else null
	                                        end as ActionResult,
                                            case when c.ZZUrlUpTime > b.[CheckDATE] then  '已生成电子证书（办结）' 
			                                        when  Convert(varchar(10),b.[CheckDATE],21) = Convert(varchar(10),c.CHECKDATE,21) and  c.EleCertErrTime > b.[CheckDATE] then '审批不通过，未生成电子证书。' + c.[EleCertErrDesc]
			                                        else null
	                                        end as ActionRemark, 
                                         '住建部' as ActionMan 
                                     FROM [dbo].[CertificateMerge] b
                                     inner join [dbo].[CERTIFICATE] c on b.NewCertificateCode = c.CERTIFICATECODE
                                     where b.[APPLYID]={0} and b.[CheckDATE] >'1950-1-1' 
                                     and (c.ZZUrlUpTime > b.[CheckDATE] 
		                                  or (c.EleCertErrTime > b.[CheckDATE] and Convert(varchar(10),b.[CheckDATE],21) = Convert(varchar(10),c.CHECKDATE,21) )
                                     )
                            ) t";
            return CommonDAL.GetDataTable(string.Format(sql, ApplyID));
        }
        #endregion
    }
}
