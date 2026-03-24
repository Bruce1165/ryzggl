using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--COC_ONE_Person_BaseInfoDAL(填写类描述)
	/// </summary>
    public class COC_ONE_Person_BaseInfoDAL
    {
        public COC_ONE_Person_BaseInfoDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="COC_ONE_Person_BaseInfoMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(COC_ONE_Person_BaseInfoMDL _COC_ONE_Person_BaseInfoMDL)
		{
		    return Insert(null,_COC_ONE_Person_BaseInfoMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="COC_ONE_Person_BaseInfoMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,COC_ONE_Person_BaseInfoMDL _COC_ONE_Person_BaseInfoMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.COC_ONE_Person_BaseInfo(Fid,PSN_ServerID,PSN_ShareID,PSN_LocalID,ENT_ServerID,ENT_LocalID,BeginTime,EndTime,PSN_Name,PSN_Sex,PSN_BirthDate,PSN_National,PSN_CertificateType,PSN_CertificateNO,PSN_GraduationSchool,PSN_Specialty,PSN_GraduationTime,PSN_Qualification,PSN_Degree,PSN_MobilePhone,PSN_Telephone,PSN_Email,PSN_PMGrade,PSN_PMCertificateNo,PSN_RegisteType,PSN_RegisterNO,PSN_RegisterCertificateNo,PSN_RegisteProfession,PSN_CertificationDate,PSN_CertificateValidity,PSN_RegistePermissionDate,PSN_ChangeReason,PSN_BeforENT_Name,PSN_BeforENT_ServerID,PSN_BeforPersonName,PSN_InterprovincialChange,PSN_ExpiryReasons,PSN_ExpiryDate,PSN_RenewalProfession,PSN_AddProfession,PSN_CancelPerson,PSN_CancelReason,PSN_ReReasons,PSN_ReContent,PSN_CheckCode,DogID,ENT_Province_Code,DownType,LastModifyTime)
			VALUES (@Fid,@PSN_ServerID,@PSN_ShareID,@PSN_LocalID,@ENT_ServerID,@ENT_LocalID,@BeginTime,@EndTime,@PSN_Name,@PSN_Sex,@PSN_BirthDate,@PSN_National,@PSN_CertificateType,@PSN_CertificateNO,@PSN_GraduationSchool,@PSN_Specialty,@PSN_GraduationTime,@PSN_Qualification,@PSN_Degree,@PSN_MobilePhone,@PSN_Telephone,@PSN_Email,@PSN_PMGrade,@PSN_PMCertificateNo,@PSN_RegisteType,@PSN_RegisterNO,@PSN_RegisterCertificateNo,@PSN_RegisteProfession,@PSN_CertificationDate,@PSN_CertificateValidity,@PSN_RegistePermissionDate,@PSN_ChangeReason,@PSN_BeforENT_Name,@PSN_BeforENT_ServerID,@PSN_BeforPersonName,@PSN_InterprovincialChange,@PSN_ExpiryReasons,@PSN_ExpiryDate,@PSN_RenewalProfession,@PSN_AddProfession,@PSN_CancelPerson,@PSN_CancelReason,@PSN_ReReasons,@PSN_ReContent,@PSN_CheckCode,@DogID,@ENT_Province_Code,@DownType,@LastModifyTime)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("Fid",DbType.String, _COC_ONE_Person_BaseInfoMDL.Fid));
			p.Add(db.CreateParameter("PSN_ServerID",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_ServerID));
			p.Add(db.CreateParameter("PSN_ShareID",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_ShareID));
			p.Add(db.CreateParameter("PSN_LocalID",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_LocalID));
			p.Add(db.CreateParameter("ENT_ServerID",DbType.String, _COC_ONE_Person_BaseInfoMDL.ENT_ServerID));
			p.Add(db.CreateParameter("ENT_LocalID",DbType.String, _COC_ONE_Person_BaseInfoMDL.ENT_LocalID));
			p.Add(db.CreateParameter("BeginTime",DbType.DateTime, _COC_ONE_Person_BaseInfoMDL.BeginTime));
			p.Add(db.CreateParameter("EndTime",DbType.DateTime, _COC_ONE_Person_BaseInfoMDL.EndTime));
			p.Add(db.CreateParameter("PSN_Name",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_Name));
			p.Add(db.CreateParameter("PSN_Sex",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_Sex));
			p.Add(db.CreateParameter("PSN_BirthDate",DbType.DateTime, _COC_ONE_Person_BaseInfoMDL.PSN_BirthDate));
			p.Add(db.CreateParameter("PSN_National",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_National));
			p.Add(db.CreateParameter("PSN_CertificateType",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_CertificateType));
			p.Add(db.CreateParameter("PSN_CertificateNO",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_CertificateNO));
			p.Add(db.CreateParameter("PSN_GraduationSchool",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_GraduationSchool));
			p.Add(db.CreateParameter("PSN_Specialty",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_Specialty));
			p.Add(db.CreateParameter("PSN_GraduationTime",DbType.DateTime, _COC_ONE_Person_BaseInfoMDL.PSN_GraduationTime));
			p.Add(db.CreateParameter("PSN_Qualification",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_Qualification));
			p.Add(db.CreateParameter("PSN_Degree",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_Degree));
			p.Add(db.CreateParameter("PSN_MobilePhone",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_MobilePhone));
			p.Add(db.CreateParameter("PSN_Telephone",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_Telephone));
			p.Add(db.CreateParameter("PSN_Email",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_Email));
			p.Add(db.CreateParameter("PSN_PMGrade",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_PMGrade));
			p.Add(db.CreateParameter("PSN_PMCertificateNo",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_PMCertificateNo));
			p.Add(db.CreateParameter("PSN_RegisteType",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_RegisteType));
			p.Add(db.CreateParameter("PSN_RegisterNO",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_RegisterNO));
			p.Add(db.CreateParameter("PSN_RegisterCertificateNo",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_RegisterCertificateNo));
			p.Add(db.CreateParameter("PSN_RegisteProfession",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_RegisteProfession));
			p.Add(db.CreateParameter("PSN_CertificationDate",DbType.DateTime, _COC_ONE_Person_BaseInfoMDL.PSN_CertificationDate));
			p.Add(db.CreateParameter("PSN_CertificateValidity",DbType.DateTime, _COC_ONE_Person_BaseInfoMDL.PSN_CertificateValidity));
			p.Add(db.CreateParameter("PSN_RegistePermissionDate",DbType.DateTime, _COC_ONE_Person_BaseInfoMDL.PSN_RegistePermissionDate));
			p.Add(db.CreateParameter("PSN_ChangeReason",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_ChangeReason));
			p.Add(db.CreateParameter("PSN_BeforENT_Name",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_BeforENT_Name));
			p.Add(db.CreateParameter("PSN_BeforENT_ServerID",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_BeforENT_ServerID));
			p.Add(db.CreateParameter("PSN_BeforPersonName",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_BeforPersonName));
			p.Add(db.CreateParameter("PSN_InterprovincialChange",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_InterprovincialChange));
			p.Add(db.CreateParameter("PSN_ExpiryReasons",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_ExpiryReasons));
			p.Add(db.CreateParameter("PSN_ExpiryDate",DbType.DateTime, _COC_ONE_Person_BaseInfoMDL.PSN_ExpiryDate));
			p.Add(db.CreateParameter("PSN_RenewalProfession",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_RenewalProfession));
			p.Add(db.CreateParameter("PSN_AddProfession",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_AddProfession));
			p.Add(db.CreateParameter("PSN_CancelPerson",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_CancelPerson));
			p.Add(db.CreateParameter("PSN_CancelReason",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_CancelReason));
			p.Add(db.CreateParameter("PSN_ReReasons",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_ReReasons));
			p.Add(db.CreateParameter("PSN_ReContent",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_ReContent));
			p.Add(db.CreateParameter("PSN_CheckCode",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_CheckCode));
			p.Add(db.CreateParameter("DogID",DbType.String, _COC_ONE_Person_BaseInfoMDL.DogID));
			p.Add(db.CreateParameter("ENT_Province_Code",DbType.String, _COC_ONE_Person_BaseInfoMDL.ENT_Province_Code));
			p.Add(db.CreateParameter("DownType",DbType.String, _COC_ONE_Person_BaseInfoMDL.DownType));
			p.Add(db.CreateParameter("LastModifyTime",DbType.DateTime, _COC_ONE_Person_BaseInfoMDL.LastModifyTime));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="COC_ONE_Person_BaseInfoMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(COC_ONE_Person_BaseInfoMDL _COC_ONE_Person_BaseInfoMDL)
		{
			return Update(null,_COC_ONE_Person_BaseInfoMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="COC_ONE_Person_BaseInfoMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,COC_ONE_Person_BaseInfoMDL _COC_ONE_Person_BaseInfoMDL)
		{
			string sql = @"
			UPDATE dbo.COC_ONE_Person_BaseInfo
				SET	PSN_ServerID = @PSN_ServerID,PSN_ShareID = @PSN_ShareID,PSN_LocalID = @PSN_LocalID,ENT_ServerID = @ENT_ServerID,ENT_LocalID = @ENT_LocalID,BeginTime = @BeginTime,EndTime = @EndTime,PSN_Name = @PSN_Name,PSN_Sex = @PSN_Sex,PSN_BirthDate = @PSN_BirthDate,PSN_National = @PSN_National,PSN_CertificateType = @PSN_CertificateType,PSN_CertificateNO = @PSN_CertificateNO,PSN_GraduationSchool = @PSN_GraduationSchool,PSN_Specialty = @PSN_Specialty,PSN_GraduationTime = @PSN_GraduationTime,PSN_Qualification = @PSN_Qualification,PSN_Degree = @PSN_Degree,PSN_MobilePhone = @PSN_MobilePhone,PSN_Telephone = @PSN_Telephone,PSN_Email = @PSN_Email,PSN_PMGrade = @PSN_PMGrade,PSN_PMCertificateNo = @PSN_PMCertificateNo,PSN_RegisteType = @PSN_RegisteType,PSN_RegisterNO = @PSN_RegisterNO,PSN_RegisterCertificateNo = @PSN_RegisterCertificateNo,PSN_RegisteProfession = @PSN_RegisteProfession,PSN_CertificationDate = @PSN_CertificationDate,PSN_CertificateValidity = @PSN_CertificateValidity,PSN_RegistePermissionDate = @PSN_RegistePermissionDate,PSN_ChangeReason = @PSN_ChangeReason,PSN_BeforENT_Name = @PSN_BeforENT_Name,PSN_BeforENT_ServerID = @PSN_BeforENT_ServerID,PSN_BeforPersonName = @PSN_BeforPersonName,PSN_InterprovincialChange = @PSN_InterprovincialChange,PSN_ExpiryReasons = @PSN_ExpiryReasons,PSN_ExpiryDate = @PSN_ExpiryDate,PSN_RenewalProfession = @PSN_RenewalProfession,PSN_AddProfession = @PSN_AddProfession,PSN_CancelPerson = @PSN_CancelPerson,PSN_CancelReason = @PSN_CancelReason,PSN_ReReasons = @PSN_ReReasons,PSN_ReContent = @PSN_ReContent,PSN_CheckCode = @PSN_CheckCode,DogID = @DogID,ENT_Province_Code = @ENT_Province_Code,DownType = @DownType,LastModifyTime = @LastModifyTime
			WHERE
				Fid = @Fid";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("Fid",DbType.String, _COC_ONE_Person_BaseInfoMDL.Fid));
			p.Add(db.CreateParameter("PSN_ServerID",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_ServerID));
			p.Add(db.CreateParameter("PSN_ShareID",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_ShareID));
			p.Add(db.CreateParameter("PSN_LocalID",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_LocalID));
			p.Add(db.CreateParameter("ENT_ServerID",DbType.String, _COC_ONE_Person_BaseInfoMDL.ENT_ServerID));
			p.Add(db.CreateParameter("ENT_LocalID",DbType.String, _COC_ONE_Person_BaseInfoMDL.ENT_LocalID));
			p.Add(db.CreateParameter("BeginTime",DbType.DateTime, _COC_ONE_Person_BaseInfoMDL.BeginTime));
			p.Add(db.CreateParameter("EndTime",DbType.DateTime, _COC_ONE_Person_BaseInfoMDL.EndTime));
			p.Add(db.CreateParameter("PSN_Name",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_Name));
			p.Add(db.CreateParameter("PSN_Sex",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_Sex));
			p.Add(db.CreateParameter("PSN_BirthDate",DbType.DateTime, _COC_ONE_Person_BaseInfoMDL.PSN_BirthDate));
			p.Add(db.CreateParameter("PSN_National",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_National));
			p.Add(db.CreateParameter("PSN_CertificateType",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_CertificateType));
			p.Add(db.CreateParameter("PSN_CertificateNO",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_CertificateNO));
			p.Add(db.CreateParameter("PSN_GraduationSchool",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_GraduationSchool));
			p.Add(db.CreateParameter("PSN_Specialty",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_Specialty));
			p.Add(db.CreateParameter("PSN_GraduationTime",DbType.DateTime, _COC_ONE_Person_BaseInfoMDL.PSN_GraduationTime));
			p.Add(db.CreateParameter("PSN_Qualification",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_Qualification));
			p.Add(db.CreateParameter("PSN_Degree",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_Degree));
			p.Add(db.CreateParameter("PSN_MobilePhone",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_MobilePhone));
			p.Add(db.CreateParameter("PSN_Telephone",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_Telephone));
			p.Add(db.CreateParameter("PSN_Email",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_Email));
			p.Add(db.CreateParameter("PSN_PMGrade",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_PMGrade));
			p.Add(db.CreateParameter("PSN_PMCertificateNo",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_PMCertificateNo));
			p.Add(db.CreateParameter("PSN_RegisteType",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_RegisteType));
			p.Add(db.CreateParameter("PSN_RegisterNO",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_RegisterNO));
			p.Add(db.CreateParameter("PSN_RegisterCertificateNo",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_RegisterCertificateNo));
			p.Add(db.CreateParameter("PSN_RegisteProfession",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_RegisteProfession));
			p.Add(db.CreateParameter("PSN_CertificationDate",DbType.DateTime, _COC_ONE_Person_BaseInfoMDL.PSN_CertificationDate));
			p.Add(db.CreateParameter("PSN_CertificateValidity",DbType.DateTime, _COC_ONE_Person_BaseInfoMDL.PSN_CertificateValidity));
			p.Add(db.CreateParameter("PSN_RegistePermissionDate",DbType.DateTime, _COC_ONE_Person_BaseInfoMDL.PSN_RegistePermissionDate));
			p.Add(db.CreateParameter("PSN_ChangeReason",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_ChangeReason));
			p.Add(db.CreateParameter("PSN_BeforENT_Name",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_BeforENT_Name));
			p.Add(db.CreateParameter("PSN_BeforENT_ServerID",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_BeforENT_ServerID));
			p.Add(db.CreateParameter("PSN_BeforPersonName",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_BeforPersonName));
			p.Add(db.CreateParameter("PSN_InterprovincialChange",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_InterprovincialChange));
			p.Add(db.CreateParameter("PSN_ExpiryReasons",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_ExpiryReasons));
			p.Add(db.CreateParameter("PSN_ExpiryDate",DbType.DateTime, _COC_ONE_Person_BaseInfoMDL.PSN_ExpiryDate));
			p.Add(db.CreateParameter("PSN_RenewalProfession",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_RenewalProfession));
			p.Add(db.CreateParameter("PSN_AddProfession",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_AddProfession));
			p.Add(db.CreateParameter("PSN_CancelPerson",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_CancelPerson));
			p.Add(db.CreateParameter("PSN_CancelReason",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_CancelReason));
			p.Add(db.CreateParameter("PSN_ReReasons",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_ReReasons));
			p.Add(db.CreateParameter("PSN_ReContent",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_ReContent));
			p.Add(db.CreateParameter("PSN_CheckCode",DbType.String, _COC_ONE_Person_BaseInfoMDL.PSN_CheckCode));
			p.Add(db.CreateParameter("DogID",DbType.String, _COC_ONE_Person_BaseInfoMDL.DogID));
			p.Add(db.CreateParameter("ENT_Province_Code",DbType.String, _COC_ONE_Person_BaseInfoMDL.ENT_Province_Code));
			p.Add(db.CreateParameter("DownType",DbType.String, _COC_ONE_Person_BaseInfoMDL.DownType));
			p.Add(db.CreateParameter("LastModifyTime",DbType.DateTime, _COC_ONE_Person_BaseInfoMDL.LastModifyTime));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="COC_ONE_Person_BaseInfoID">主键</param>
		/// <returns></returns>
        public static int Delete( string Fid )
		{
			return Delete(null, Fid);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="COC_ONE_Person_BaseInfoID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, string Fid)
		{
			string sql=@"DELETE FROM dbo.COC_ONE_Person_BaseInfo WHERE Fid = @Fid";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("Fid",DbType.String,Fid));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="COC_ONE_Person_BaseInfoMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(COC_ONE_Person_BaseInfoMDL _COC_ONE_Person_BaseInfoMDL)
		{
			return Delete(null,_COC_ONE_Person_BaseInfoMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="COC_ONE_Person_BaseInfoMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,COC_ONE_Person_BaseInfoMDL _COC_ONE_Person_BaseInfoMDL)
		{
			string sql=@"DELETE FROM dbo.COC_ONE_Person_BaseInfo WHERE Fid = @Fid";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("Fid",DbType.String,_COC_ONE_Person_BaseInfoMDL.Fid));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体（已作废，不更新了，改从[jcsjk_jzs]表获取一建数据）
        /// </summary>
        /// <param name="COC_ONE_Person_BaseInfoID">主键</param>
        public static COC_ONE_Person_BaseInfoMDL GetObject( string Fid )
		{
			string sql=@"
			SELECT Fid,PSN_ServerID,PSN_ShareID,PSN_LocalID,ENT_ServerID,ENT_LocalID,BeginTime,EndTime,PSN_Name,PSN_Sex,PSN_BirthDate,PSN_National,PSN_CertificateType,PSN_CertificateNO,PSN_GraduationSchool,PSN_Specialty,PSN_GraduationTime,PSN_Qualification,PSN_Degree,PSN_MobilePhone,PSN_Telephone,PSN_Email,PSN_PMGrade,PSN_PMCertificateNo,PSN_RegisteType,PSN_RegisterNO,PSN_RegisterCertificateNo,PSN_RegisteProfession,PSN_CertificationDate,PSN_CertificateValidity,PSN_RegistePermissionDate,PSN_ChangeReason,PSN_BeforENT_Name,PSN_BeforENT_ServerID,PSN_BeforPersonName,PSN_InterprovincialChange,PSN_ExpiryReasons,PSN_ExpiryDate,PSN_RenewalProfession,PSN_AddProfession,PSN_CancelPerson,PSN_CancelReason,PSN_ReReasons,PSN_ReContent,PSN_CheckCode,DogID,ENT_Province_Code,DownType,LastModifyTime
			FROM dbo.COC_ONE_Person_BaseInfo
			WHERE Fid = @Fid";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("Fid", DbType.String, Fid));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                COC_ONE_Person_BaseInfoMDL _COC_ONE_Person_BaseInfoMDL = null;
                if (reader.Read())
                {
                    _COC_ONE_Person_BaseInfoMDL = new COC_ONE_Person_BaseInfoMDL();
					if (reader["Fid"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.Fid = Convert.ToString(reader["Fid"]);
					if (reader["PSN_ServerID"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_ServerID = Convert.ToString(reader["PSN_ServerID"]);
					if (reader["PSN_ShareID"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_ShareID = Convert.ToString(reader["PSN_ShareID"]);
					if (reader["PSN_LocalID"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_LocalID = Convert.ToString(reader["PSN_LocalID"]);
					if (reader["ENT_ServerID"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.ENT_ServerID = Convert.ToString(reader["ENT_ServerID"]);
					if (reader["ENT_LocalID"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.ENT_LocalID = Convert.ToString(reader["ENT_LocalID"]);
					if (reader["BeginTime"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.BeginTime = Convert.ToDateTime(reader["BeginTime"]);
					if (reader["EndTime"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.EndTime = Convert.ToDateTime(reader["EndTime"]);
					if (reader["PSN_Name"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_Name = Convert.ToString(reader["PSN_Name"]);
					if (reader["PSN_Sex"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_Sex = Convert.ToString(reader["PSN_Sex"]);
					if (reader["PSN_BirthDate"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_BirthDate = Convert.ToDateTime(reader["PSN_BirthDate"]);
					if (reader["PSN_National"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_National = Convert.ToString(reader["PSN_National"]);
					if (reader["PSN_CertificateType"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_CertificateType = Convert.ToString(reader["PSN_CertificateType"]);
					if (reader["PSN_CertificateNO"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_CertificateNO = Convert.ToString(reader["PSN_CertificateNO"]);
					if (reader["PSN_GraduationSchool"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_GraduationSchool = Convert.ToString(reader["PSN_GraduationSchool"]);
					if (reader["PSN_Specialty"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_Specialty = Convert.ToString(reader["PSN_Specialty"]);
					if (reader["PSN_GraduationTime"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_GraduationTime = Convert.ToDateTime(reader["PSN_GraduationTime"]);
					if (reader["PSN_Qualification"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_Qualification = Convert.ToString(reader["PSN_Qualification"]);
					if (reader["PSN_Degree"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_Degree = Convert.ToString(reader["PSN_Degree"]);
					if (reader["PSN_MobilePhone"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_MobilePhone = Convert.ToString(reader["PSN_MobilePhone"]);
					if (reader["PSN_Telephone"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_Telephone = Convert.ToString(reader["PSN_Telephone"]);
					if (reader["PSN_Email"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_Email = Convert.ToString(reader["PSN_Email"]);
					if (reader["PSN_PMGrade"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_PMGrade = Convert.ToString(reader["PSN_PMGrade"]);
					if (reader["PSN_PMCertificateNo"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_PMCertificateNo = Convert.ToString(reader["PSN_PMCertificateNo"]);
					if (reader["PSN_RegisteType"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_RegisteType = Convert.ToString(reader["PSN_RegisteType"]);
					if (reader["PSN_RegisterNO"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_RegisterNO = Convert.ToString(reader["PSN_RegisterNO"]);
					if (reader["PSN_RegisterCertificateNo"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_RegisterCertificateNo = Convert.ToString(reader["PSN_RegisterCertificateNo"]);
					if (reader["PSN_RegisteProfession"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_RegisteProfession = Convert.ToString(reader["PSN_RegisteProfession"]);
					if (reader["PSN_CertificationDate"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_CertificationDate = Convert.ToDateTime(reader["PSN_CertificationDate"]);
					if (reader["PSN_CertificateValidity"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_CertificateValidity = Convert.ToDateTime(reader["PSN_CertificateValidity"]);
					if (reader["PSN_RegistePermissionDate"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_RegistePermissionDate = Convert.ToDateTime(reader["PSN_RegistePermissionDate"]);
					if (reader["PSN_ChangeReason"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_ChangeReason = Convert.ToString(reader["PSN_ChangeReason"]);
					if (reader["PSN_BeforENT_Name"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_BeforENT_Name = Convert.ToString(reader["PSN_BeforENT_Name"]);
					if (reader["PSN_BeforENT_ServerID"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_BeforENT_ServerID = Convert.ToString(reader["PSN_BeforENT_ServerID"]);
					if (reader["PSN_BeforPersonName"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_BeforPersonName = Convert.ToString(reader["PSN_BeforPersonName"]);
					if (reader["PSN_InterprovincialChange"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_InterprovincialChange = Convert.ToString(reader["PSN_InterprovincialChange"]);
					if (reader["PSN_ExpiryReasons"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_ExpiryReasons = Convert.ToString(reader["PSN_ExpiryReasons"]);
					if (reader["PSN_ExpiryDate"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_ExpiryDate = Convert.ToDateTime(reader["PSN_ExpiryDate"]);
					if (reader["PSN_RenewalProfession"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_RenewalProfession = Convert.ToString(reader["PSN_RenewalProfession"]);
					if (reader["PSN_AddProfession"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_AddProfession = Convert.ToString(reader["PSN_AddProfession"]);
					if (reader["PSN_CancelPerson"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_CancelPerson = Convert.ToString(reader["PSN_CancelPerson"]);
					if (reader["PSN_CancelReason"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_CancelReason = Convert.ToString(reader["PSN_CancelReason"]);
					if (reader["PSN_ReReasons"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_ReReasons = Convert.ToString(reader["PSN_ReReasons"]);
					if (reader["PSN_ReContent"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_ReContent = Convert.ToString(reader["PSN_ReContent"]);
					if (reader["PSN_CheckCode"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.PSN_CheckCode = Convert.ToString(reader["PSN_CheckCode"]);
					if (reader["DogID"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.DogID = Convert.ToString(reader["DogID"]);
					if (reader["ENT_Province_Code"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.ENT_Province_Code = Convert.ToString(reader["ENT_Province_Code"]);
					if (reader["DownType"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.DownType = Convert.ToString(reader["DownType"]);
					if (reader["LastModifyTime"] != DBNull.Value) _COC_ONE_Person_BaseInfoMDL.LastModifyTime = Convert.ToDateTime(reader["LastModifyTime"]);
                }
				reader.Close();
                db.Close();
                return _COC_ONE_Person_BaseInfoMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.COC_ONE_Person_BaseInfo", "*", filterWhereString, orderBy == "" ? " Fid" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.COC_ONE_Person_BaseInfo", filterWhereString);
        }
        
        #region 自定义方法

        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        /// <summary>
        public static DataTable GetListView(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.View_JZS_ONE", "*", filterWhereString, orderBy == "" ? " Fid" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountView(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.View_JZS_ONE", filterWhereString);
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
        public static DataTable GetListView_JZS_OneLevel(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.View_JZS_OneLevel", "*", filterWhereString, orderBy == "" ? " PSN_CertificateNO" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountView_JZS_OneLevel(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.View_JZS_OneLevel", filterWhereString);
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
        public static DataTable GetListView_JZS_OneTempLevel(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.View_JZS_OneTempLevel", "*", filterWhereString, orderBy == "" ? " Fid" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountView_JZS_OneTempLevel(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.View_JZS_OneTempLevel", filterWhereString);
        }

        
        #endregion
    }
}
