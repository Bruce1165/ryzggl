using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--zjs_CertificateDAL(填写类描述)
	/// </summary>
    public class zjs_CertificateDAL
    {
        public zjs_CertificateDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_zjs_CertificateMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(zjs_CertificateMDL _zjs_CertificateMDL)
		{
		    return Insert(null,_zjs_CertificateMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_zjs_CertificateMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,zjs_CertificateMDL _zjs_CertificateMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.zjs_Certificate(PSN_ServerID,ENT_Name,ENT_OrganizationsCode,ENT_City,END_Addess,PSN_Name,PSN_Sex,PSN_BirthDate,PSN_National,PSN_CertificateType,PSN_CertificateNO,PSN_GraduationSchool,PSN_Specialty,PSN_GraduationTime,PSN_Qualification,PSN_Degree,PSN_MobilePhone,PSN_Email,PSN_Telephone,PSN_RegisteType,PSN_RegisterNO,PSN_RegisterCertificateNo,PSN_RegisteProfession,PSN_CertificationDate,PSN_CertificateValidity,PSN_RegistePermissionDate,PSN_Level,ZGZSBH,CJR,CJSJ,XGR,XGSJ,[VALID],Memo,ApplyCATime,SendCATime,ReturnCATime,CertificateCAID,license_code,auth_code,SignCATime)
			VALUES (@PSN_ServerID,@ENT_Name,@ENT_OrganizationsCode,@ENT_City,@END_Addess,@PSN_Name,@PSN_Sex,@PSN_BirthDate,@PSN_National,@PSN_CertificateType,@PSN_CertificateNO,@PSN_GraduationSchool,@PSN_Specialty,@PSN_GraduationTime,@PSN_Qualification,@PSN_Degree,@PSN_MobilePhone,@PSN_Email,@PSN_Telephone,@PSN_RegisteType,@PSN_RegisterNO,@PSN_RegisterCertificateNo,@PSN_RegisteProfession,@PSN_CertificationDate,@PSN_CertificateValidity,@PSN_RegistePermissionDate,@PSN_Level,@ZGZSBH,@CJR,@CJSJ,@XGR,@XGSJ,@Valid,@Memo,@ApplyCATime,@SendCATime,@ReturnCATime,@CertificateCAID,@license_code,@auth_code,@SignCATime)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("PSN_ServerID",DbType.String, _zjs_CertificateMDL.PSN_ServerID));
			p.Add(db.CreateParameter("ENT_Name",DbType.String, _zjs_CertificateMDL.ENT_Name));
			p.Add(db.CreateParameter("ENT_OrganizationsCode",DbType.String, _zjs_CertificateMDL.ENT_OrganizationsCode));
			p.Add(db.CreateParameter("ENT_City",DbType.String, _zjs_CertificateMDL.ENT_City));
			p.Add(db.CreateParameter("END_Addess",DbType.String, _zjs_CertificateMDL.END_Addess));
			p.Add(db.CreateParameter("PSN_Name",DbType.String, _zjs_CertificateMDL.PSN_Name));
			p.Add(db.CreateParameter("PSN_Sex",DbType.String, _zjs_CertificateMDL.PSN_Sex));
			p.Add(db.CreateParameter("PSN_BirthDate",DbType.DateTime, _zjs_CertificateMDL.PSN_BirthDate));
			p.Add(db.CreateParameter("PSN_National",DbType.String, _zjs_CertificateMDL.PSN_National));
			p.Add(db.CreateParameter("PSN_CertificateType",DbType.String, _zjs_CertificateMDL.PSN_CertificateType));
			p.Add(db.CreateParameter("PSN_CertificateNO",DbType.String, _zjs_CertificateMDL.PSN_CertificateNO));
			p.Add(db.CreateParameter("PSN_GraduationSchool",DbType.String, _zjs_CertificateMDL.PSN_GraduationSchool));
			p.Add(db.CreateParameter("PSN_Specialty",DbType.String, _zjs_CertificateMDL.PSN_Specialty));
			p.Add(db.CreateParameter("PSN_GraduationTime",DbType.DateTime, _zjs_CertificateMDL.PSN_GraduationTime));
			p.Add(db.CreateParameter("PSN_Qualification",DbType.String, _zjs_CertificateMDL.PSN_Qualification));
			p.Add(db.CreateParameter("PSN_Degree",DbType.String, _zjs_CertificateMDL.PSN_Degree));
			p.Add(db.CreateParameter("PSN_MobilePhone",DbType.String, _zjs_CertificateMDL.PSN_MobilePhone));
			p.Add(db.CreateParameter("PSN_Email",DbType.String, _zjs_CertificateMDL.PSN_Email));
			p.Add(db.CreateParameter("PSN_Telephone",DbType.String, _zjs_CertificateMDL.PSN_Telephone));
			p.Add(db.CreateParameter("PSN_RegisteType",DbType.String, _zjs_CertificateMDL.PSN_RegisteType));
			p.Add(db.CreateParameter("PSN_RegisterNO",DbType.String, _zjs_CertificateMDL.PSN_RegisterNO));
			p.Add(db.CreateParameter("PSN_RegisterCertificateNo",DbType.String, _zjs_CertificateMDL.PSN_RegisterCertificateNo));
			p.Add(db.CreateParameter("PSN_RegisteProfession",DbType.String, _zjs_CertificateMDL.PSN_RegisteProfession));
			p.Add(db.CreateParameter("PSN_CertificationDate",DbType.DateTime, _zjs_CertificateMDL.PSN_CertificationDate));
			p.Add(db.CreateParameter("PSN_CertificateValidity",DbType.DateTime, _zjs_CertificateMDL.PSN_CertificateValidity));
			p.Add(db.CreateParameter("PSN_RegistePermissionDate",DbType.DateTime, _zjs_CertificateMDL.PSN_RegistePermissionDate));
			p.Add(db.CreateParameter("PSN_Level",DbType.String, _zjs_CertificateMDL.PSN_Level));
			p.Add(db.CreateParameter("ZGZSBH",DbType.String, _zjs_CertificateMDL.ZGZSBH));
			p.Add(db.CreateParameter("CJR",DbType.String, _zjs_CertificateMDL.CJR));
			p.Add(db.CreateParameter("CJSJ",DbType.DateTime, _zjs_CertificateMDL.CJSJ));
			p.Add(db.CreateParameter("XGR",DbType.String, _zjs_CertificateMDL.XGR));
			p.Add(db.CreateParameter("XGSJ",DbType.DateTime, _zjs_CertificateMDL.XGSJ));
			p.Add(db.CreateParameter("Valid",DbType.Int32, _zjs_CertificateMDL.Valid));
			p.Add(db.CreateParameter("Memo",DbType.String, _zjs_CertificateMDL.Memo));
			p.Add(db.CreateParameter("ApplyCATime",DbType.DateTime, _zjs_CertificateMDL.ApplyCATime));
			p.Add(db.CreateParameter("SendCATime",DbType.DateTime, _zjs_CertificateMDL.SendCATime));
			p.Add(db.CreateParameter("ReturnCATime",DbType.DateTime, _zjs_CertificateMDL.ReturnCATime));
			p.Add(db.CreateParameter("CertificateCAID",DbType.String, _zjs_CertificateMDL.CertificateCAID));
			p.Add(db.CreateParameter("license_code",DbType.String, _zjs_CertificateMDL.license_code));
			p.Add(db.CreateParameter("auth_code",DbType.String, _zjs_CertificateMDL.auth_code));
			p.Add(db.CreateParameter("SignCATime",DbType.DateTime, _zjs_CertificateMDL.SignCATime));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_zjs_CertificateMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(zjs_CertificateMDL _zjs_CertificateMDL)
		{
			return Update(null,_zjs_CertificateMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_zjs_CertificateMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,zjs_CertificateMDL _zjs_CertificateMDL)
		{
			string sql = @"
			UPDATE dbo.zjs_Certificate
				SET	ENT_Name = @ENT_Name,ENT_OrganizationsCode = @ENT_OrganizationsCode,ENT_City = @ENT_City,END_Addess = @END_Addess,PSN_Name = @PSN_Name,PSN_Sex = @PSN_Sex,PSN_BirthDate = @PSN_BirthDate,PSN_National = @PSN_National,PSN_CertificateType = @PSN_CertificateType,PSN_CertificateNO = @PSN_CertificateNO,PSN_GraduationSchool = @PSN_GraduationSchool,PSN_Specialty = @PSN_Specialty,PSN_GraduationTime = @PSN_GraduationTime,PSN_Qualification = @PSN_Qualification,PSN_Degree = @PSN_Degree,PSN_MobilePhone = @PSN_MobilePhone,PSN_Email = @PSN_Email,PSN_Telephone = @PSN_Telephone,PSN_RegisteType = @PSN_RegisteType,PSN_RegisterNO = @PSN_RegisterNO,PSN_RegisterCertificateNo = @PSN_RegisterCertificateNo,PSN_RegisteProfession = @PSN_RegisteProfession,PSN_CertificationDate = @PSN_CertificationDate,PSN_CertificateValidity = @PSN_CertificateValidity,PSN_RegistePermissionDate = @PSN_RegistePermissionDate,PSN_Level = @PSN_Level,ZGZSBH = @ZGZSBH,CJR = @CJR,CJSJ = @CJSJ,XGR = @XGR,XGSJ = @XGSJ,""VALID"" = @Valid,Memo = @Memo,ApplyCATime = @ApplyCATime,SendCATime = @SendCATime,ReturnCATime = @ReturnCATime,CertificateCAID = @CertificateCAID,license_code = @license_code,auth_code = @auth_code,SignCATime = @SignCATime
			WHERE
				PSN_ServerID = @PSN_ServerID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("PSN_ServerID",DbType.String, _zjs_CertificateMDL.PSN_ServerID));
			p.Add(db.CreateParameter("ENT_Name",DbType.String, _zjs_CertificateMDL.ENT_Name));
			p.Add(db.CreateParameter("ENT_OrganizationsCode",DbType.String, _zjs_CertificateMDL.ENT_OrganizationsCode));
			p.Add(db.CreateParameter("ENT_City",DbType.String, _zjs_CertificateMDL.ENT_City));
			p.Add(db.CreateParameter("END_Addess",DbType.String, _zjs_CertificateMDL.END_Addess));
			p.Add(db.CreateParameter("PSN_Name",DbType.String, _zjs_CertificateMDL.PSN_Name));
			p.Add(db.CreateParameter("PSN_Sex",DbType.String, _zjs_CertificateMDL.PSN_Sex));
			p.Add(db.CreateParameter("PSN_BirthDate",DbType.DateTime, _zjs_CertificateMDL.PSN_BirthDate));
			p.Add(db.CreateParameter("PSN_National",DbType.String, _zjs_CertificateMDL.PSN_National));
			p.Add(db.CreateParameter("PSN_CertificateType",DbType.String, _zjs_CertificateMDL.PSN_CertificateType));
			p.Add(db.CreateParameter("PSN_CertificateNO",DbType.String, _zjs_CertificateMDL.PSN_CertificateNO));
			p.Add(db.CreateParameter("PSN_GraduationSchool",DbType.String, _zjs_CertificateMDL.PSN_GraduationSchool));
			p.Add(db.CreateParameter("PSN_Specialty",DbType.String, _zjs_CertificateMDL.PSN_Specialty));
			p.Add(db.CreateParameter("PSN_GraduationTime",DbType.DateTime, _zjs_CertificateMDL.PSN_GraduationTime));
			p.Add(db.CreateParameter("PSN_Qualification",DbType.String, _zjs_CertificateMDL.PSN_Qualification));
			p.Add(db.CreateParameter("PSN_Degree",DbType.String, _zjs_CertificateMDL.PSN_Degree));
			p.Add(db.CreateParameter("PSN_MobilePhone",DbType.String, _zjs_CertificateMDL.PSN_MobilePhone));
			p.Add(db.CreateParameter("PSN_Email",DbType.String, _zjs_CertificateMDL.PSN_Email));
			p.Add(db.CreateParameter("PSN_Telephone",DbType.String, _zjs_CertificateMDL.PSN_Telephone));
			p.Add(db.CreateParameter("PSN_RegisteType",DbType.String, _zjs_CertificateMDL.PSN_RegisteType));
			p.Add(db.CreateParameter("PSN_RegisterNO",DbType.String, _zjs_CertificateMDL.PSN_RegisterNO));
			p.Add(db.CreateParameter("PSN_RegisterCertificateNo",DbType.String, _zjs_CertificateMDL.PSN_RegisterCertificateNo));
			p.Add(db.CreateParameter("PSN_RegisteProfession",DbType.String, _zjs_CertificateMDL.PSN_RegisteProfession));
			p.Add(db.CreateParameter("PSN_CertificationDate",DbType.DateTime, _zjs_CertificateMDL.PSN_CertificationDate));
			p.Add(db.CreateParameter("PSN_CertificateValidity",DbType.DateTime, _zjs_CertificateMDL.PSN_CertificateValidity));
			p.Add(db.CreateParameter("PSN_RegistePermissionDate",DbType.DateTime, _zjs_CertificateMDL.PSN_RegistePermissionDate));
			p.Add(db.CreateParameter("PSN_Level",DbType.String, _zjs_CertificateMDL.PSN_Level));
			p.Add(db.CreateParameter("ZGZSBH",DbType.String, _zjs_CertificateMDL.ZGZSBH));
			p.Add(db.CreateParameter("CJR",DbType.String, _zjs_CertificateMDL.CJR));
			p.Add(db.CreateParameter("CJSJ",DbType.DateTime, _zjs_CertificateMDL.CJSJ));
			p.Add(db.CreateParameter("XGR",DbType.String, _zjs_CertificateMDL.XGR));
			p.Add(db.CreateParameter("XGSJ",DbType.DateTime, _zjs_CertificateMDL.XGSJ));
			p.Add(db.CreateParameter("Valid",DbType.Int32, _zjs_CertificateMDL.Valid));
			p.Add(db.CreateParameter("Memo",DbType.String, _zjs_CertificateMDL.Memo));
			p.Add(db.CreateParameter("ApplyCATime",DbType.DateTime, _zjs_CertificateMDL.ApplyCATime));
			p.Add(db.CreateParameter("SendCATime",DbType.DateTime, _zjs_CertificateMDL.SendCATime));
			p.Add(db.CreateParameter("ReturnCATime",DbType.DateTime, _zjs_CertificateMDL.ReturnCATime));
			p.Add(db.CreateParameter("CertificateCAID",DbType.String, _zjs_CertificateMDL.CertificateCAID));
			p.Add(db.CreateParameter("license_code",DbType.String, _zjs_CertificateMDL.license_code));
			p.Add(db.CreateParameter("auth_code",DbType.String, _zjs_CertificateMDL.auth_code));
			p.Add(db.CreateParameter("SignCATime",DbType.DateTime, _zjs_CertificateMDL.SignCATime));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="zjs_CertificateID">主键</param>
		/// <returns></returns>
        public static int Delete( string PSN_ServerID )
		{
			return Delete(null, PSN_ServerID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="zjs_CertificateID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, string PSN_ServerID)
		{
			string sql=@"DELETE FROM dbo.zjs_Certificate WHERE PSN_ServerID = @PSN_ServerID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("PSN_ServerID",DbType.String,PSN_ServerID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="_zjs_CertificateMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(zjs_CertificateMDL _zjs_CertificateMDL)
		{
			return Delete(null,_zjs_CertificateMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_zjs_CertificateMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,zjs_CertificateMDL _zjs_CertificateMDL)
		{
			string sql=@"DELETE FROM dbo.zjs_Certificate WHERE PSN_ServerID = @PSN_ServerID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("PSN_ServerID",DbType.String,_zjs_CertificateMDL.PSN_ServerID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="zjs_CertificateID">主键</param>
        public static zjs_CertificateMDL GetObject( string PSN_ServerID )
		{
			string sql=@"
			SELECT PSN_ServerID,ENT_Name,ENT_OrganizationsCode,ENT_City,END_Addess,PSN_Name,PSN_Sex,PSN_BirthDate,PSN_National,PSN_CertificateType,PSN_CertificateNO,PSN_GraduationSchool,PSN_Specialty,PSN_GraduationTime,PSN_Qualification,PSN_Degree,PSN_MobilePhone,PSN_Email,PSN_Telephone,PSN_RegisteType,PSN_RegisterNO,PSN_RegisterCertificateNo,PSN_RegisteProfession,PSN_CertificationDate,PSN_CertificateValidity,PSN_RegistePermissionDate,PSN_Level,ZGZSBH,CJR,CJSJ,XGR,XGSJ,[VALID],Memo,ApplyCATime,SendCATime,ReturnCATime,CertificateCAID,license_code,auth_code,SignCATime
			FROM dbo.zjs_Certificate
			WHERE PSN_ServerID = @PSN_ServerID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PSN_ServerID", DbType.String, PSN_ServerID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                zjs_CertificateMDL _zjs_CertificateMDL = null;
                if (reader.Read())
                {
                    _zjs_CertificateMDL = new zjs_CertificateMDL();
					if (reader["PSN_ServerID"] != DBNull.Value) _zjs_CertificateMDL.PSN_ServerID = Convert.ToString(reader["PSN_ServerID"]);
					if (reader["ENT_Name"] != DBNull.Value) _zjs_CertificateMDL.ENT_Name = Convert.ToString(reader["ENT_Name"]);
					if (reader["ENT_OrganizationsCode"] != DBNull.Value) _zjs_CertificateMDL.ENT_OrganizationsCode = Convert.ToString(reader["ENT_OrganizationsCode"]);
					if (reader["ENT_City"] != DBNull.Value) _zjs_CertificateMDL.ENT_City = Convert.ToString(reader["ENT_City"]);
					if (reader["END_Addess"] != DBNull.Value) _zjs_CertificateMDL.END_Addess = Convert.ToString(reader["END_Addess"]);
					if (reader["PSN_Name"] != DBNull.Value) _zjs_CertificateMDL.PSN_Name = Convert.ToString(reader["PSN_Name"]);
					if (reader["PSN_Sex"] != DBNull.Value) _zjs_CertificateMDL.PSN_Sex = Convert.ToString(reader["PSN_Sex"]);
					if (reader["PSN_BirthDate"] != DBNull.Value) _zjs_CertificateMDL.PSN_BirthDate = Convert.ToDateTime(reader["PSN_BirthDate"]);
					if (reader["PSN_National"] != DBNull.Value) _zjs_CertificateMDL.PSN_National = Convert.ToString(reader["PSN_National"]);
					if (reader["PSN_CertificateType"] != DBNull.Value) _zjs_CertificateMDL.PSN_CertificateType = Convert.ToString(reader["PSN_CertificateType"]);
					if (reader["PSN_CertificateNO"] != DBNull.Value) _zjs_CertificateMDL.PSN_CertificateNO = Convert.ToString(reader["PSN_CertificateNO"]);
					if (reader["PSN_GraduationSchool"] != DBNull.Value) _zjs_CertificateMDL.PSN_GraduationSchool = Convert.ToString(reader["PSN_GraduationSchool"]);
					if (reader["PSN_Specialty"] != DBNull.Value) _zjs_CertificateMDL.PSN_Specialty = Convert.ToString(reader["PSN_Specialty"]);
					if (reader["PSN_GraduationTime"] != DBNull.Value) _zjs_CertificateMDL.PSN_GraduationTime = Convert.ToDateTime(reader["PSN_GraduationTime"]);
					if (reader["PSN_Qualification"] != DBNull.Value) _zjs_CertificateMDL.PSN_Qualification = Convert.ToString(reader["PSN_Qualification"]);
					if (reader["PSN_Degree"] != DBNull.Value) _zjs_CertificateMDL.PSN_Degree = Convert.ToString(reader["PSN_Degree"]);
					if (reader["PSN_MobilePhone"] != DBNull.Value) _zjs_CertificateMDL.PSN_MobilePhone = Convert.ToString(reader["PSN_MobilePhone"]);
					if (reader["PSN_Email"] != DBNull.Value) _zjs_CertificateMDL.PSN_Email = Convert.ToString(reader["PSN_Email"]);
					if (reader["PSN_Telephone"] != DBNull.Value) _zjs_CertificateMDL.PSN_Telephone = Convert.ToString(reader["PSN_Telephone"]);
					if (reader["PSN_RegisteType"] != DBNull.Value) _zjs_CertificateMDL.PSN_RegisteType = Convert.ToString(reader["PSN_RegisteType"]);
					if (reader["PSN_RegisterNO"] != DBNull.Value) _zjs_CertificateMDL.PSN_RegisterNO = Convert.ToString(reader["PSN_RegisterNO"]);
					if (reader["PSN_RegisterCertificateNo"] != DBNull.Value) _zjs_CertificateMDL.PSN_RegisterCertificateNo = Convert.ToString(reader["PSN_RegisterCertificateNo"]);
					if (reader["PSN_RegisteProfession"] != DBNull.Value) _zjs_CertificateMDL.PSN_RegisteProfession = Convert.ToString(reader["PSN_RegisteProfession"]);
					if (reader["PSN_CertificationDate"] != DBNull.Value) _zjs_CertificateMDL.PSN_CertificationDate = Convert.ToDateTime(reader["PSN_CertificationDate"]);
					if (reader["PSN_CertificateValidity"] != DBNull.Value) _zjs_CertificateMDL.PSN_CertificateValidity = Convert.ToDateTime(reader["PSN_CertificateValidity"]);
					if (reader["PSN_RegistePermissionDate"] != DBNull.Value) _zjs_CertificateMDL.PSN_RegistePermissionDate = Convert.ToDateTime(reader["PSN_RegistePermissionDate"]);
					if (reader["PSN_Level"] != DBNull.Value) _zjs_CertificateMDL.PSN_Level = Convert.ToString(reader["PSN_Level"]);
					if (reader["ZGZSBH"] != DBNull.Value) _zjs_CertificateMDL.ZGZSBH = Convert.ToString(reader["ZGZSBH"]);
					if (reader["CJR"] != DBNull.Value) _zjs_CertificateMDL.CJR = Convert.ToString(reader["CJR"]);
					if (reader["CJSJ"] != DBNull.Value) _zjs_CertificateMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
					if (reader["XGR"] != DBNull.Value) _zjs_CertificateMDL.XGR = Convert.ToString(reader["XGR"]);
					if (reader["XGSJ"] != DBNull.Value) _zjs_CertificateMDL.XGSJ = Convert.ToDateTime(reader["XGSJ"]);
					if (reader["Valid"] != DBNull.Value) _zjs_CertificateMDL.Valid = Convert.ToInt32(reader["Valid"]);
					if (reader["Memo"] != DBNull.Value) _zjs_CertificateMDL.Memo = Convert.ToString(reader["Memo"]);
					if (reader["ApplyCATime"] != DBNull.Value) _zjs_CertificateMDL.ApplyCATime = Convert.ToDateTime(reader["ApplyCATime"]);
					if (reader["SendCATime"] != DBNull.Value) _zjs_CertificateMDL.SendCATime = Convert.ToDateTime(reader["SendCATime"]);
					if (reader["ReturnCATime"] != DBNull.Value) _zjs_CertificateMDL.ReturnCATime = Convert.ToDateTime(reader["ReturnCATime"]);
					if (reader["CertificateCAID"] != DBNull.Value) _zjs_CertificateMDL.CertificateCAID = Convert.ToString(reader["CertificateCAID"]);
					if (reader["license_code"] != DBNull.Value) _zjs_CertificateMDL.license_code = Convert.ToString(reader["license_code"]);
					if (reader["auth_code"] != DBNull.Value) _zjs_CertificateMDL.auth_code = Convert.ToString(reader["auth_code"]);
					if (reader["SignCATime"] != DBNull.Value) _zjs_CertificateMDL.SignCATime = Convert.ToDateTime(reader["SignCATime"]);
                }
				reader.Close();
                db.Close();
                return _zjs_CertificateMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.zjs_Certificate", "*", filterWhereString, orderBy == "" ? " PSN_ServerID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.zjs_Certificate", filterWhereString);
        }
        
        #region 自定义方法

        /// <summary>
        /// 根据证件号码获取未注销二级注册造价师证书集合
        /// </summary>
        /// <param name="PSN_CertificateNO">证件号码</param>
        /// <returns>注册证书信息</returns>
        public static List<zjs_CertificateMDL> GetObjectByPSN_CertificateNO_NoCancel(string PSN_CertificateNO)
        {
            string sql = @"
			SELECT PSN_ServerID,ENT_Name,ENT_OrganizationsCode,ENT_City,END_Addess,PSN_Name,PSN_Sex,PSN_BirthDate,PSN_National,PSN_CertificateType,PSN_CertificateNO,PSN_GraduationSchool,PSN_Specialty,PSN_GraduationTime,PSN_Qualification,PSN_Degree,PSN_MobilePhone,PSN_Email,PSN_Telephone,PSN_RegisteType,PSN_RegisterNO,PSN_RegisterCertificateNo,PSN_RegisteProfession,PSN_CertificationDate,PSN_CertificateValidity,PSN_RegistePermissionDate,PSN_Level,ZGZSBH,CJR,CJSJ,XGR,XGSJ,[VALID],Memo,ApplyCATime,SendCATime,ReturnCATime,CertificateCAID,license_code,auth_code,SignCATime
			FROM dbo.zjs_Certificate
			WHERE PSN_CertificateNO = @PSN_CertificateNO and [PSN_RegisteType]<'07'
            order by PSN_RegisterNO";
            List<zjs_CertificateMDL> list = new List<zjs_CertificateMDL>();
            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PSN_CertificateNO", DbType.String, PSN_CertificateNO));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                zjs_CertificateMDL _zjs_CertificateMDL = null;
                while (reader.Read())
                {
                    _zjs_CertificateMDL = new zjs_CertificateMDL();
                    if (reader["PSN_ServerID"] != DBNull.Value) _zjs_CertificateMDL.PSN_ServerID = Convert.ToString(reader["PSN_ServerID"]);
                    if (reader["ENT_Name"] != DBNull.Value) _zjs_CertificateMDL.ENT_Name = Convert.ToString(reader["ENT_Name"]);
                    if (reader["ENT_OrganizationsCode"] != DBNull.Value) _zjs_CertificateMDL.ENT_OrganizationsCode = Convert.ToString(reader["ENT_OrganizationsCode"]);
                    if (reader["ENT_City"] != DBNull.Value) _zjs_CertificateMDL.ENT_City = Convert.ToString(reader["ENT_City"]);
                    if (reader["END_Addess"] != DBNull.Value) _zjs_CertificateMDL.END_Addess = Convert.ToString(reader["END_Addess"]);
                    if (reader["PSN_Name"] != DBNull.Value) _zjs_CertificateMDL.PSN_Name = Convert.ToString(reader["PSN_Name"]);
                    if (reader["PSN_Sex"] != DBNull.Value) _zjs_CertificateMDL.PSN_Sex = Convert.ToString(reader["PSN_Sex"]);
                    if (reader["PSN_BirthDate"] != DBNull.Value) _zjs_CertificateMDL.PSN_BirthDate = Convert.ToDateTime(reader["PSN_BirthDate"]);
                    if (reader["PSN_National"] != DBNull.Value) _zjs_CertificateMDL.PSN_National = Convert.ToString(reader["PSN_National"]);
                    if (reader["PSN_CertificateType"] != DBNull.Value) _zjs_CertificateMDL.PSN_CertificateType = Convert.ToString(reader["PSN_CertificateType"]);
                    if (reader["PSN_CertificateNO"] != DBNull.Value) _zjs_CertificateMDL.PSN_CertificateNO = Convert.ToString(reader["PSN_CertificateNO"]);
                    if (reader["PSN_GraduationSchool"] != DBNull.Value) _zjs_CertificateMDL.PSN_GraduationSchool = Convert.ToString(reader["PSN_GraduationSchool"]);
                    if (reader["PSN_Specialty"] != DBNull.Value) _zjs_CertificateMDL.PSN_Specialty = Convert.ToString(reader["PSN_Specialty"]);
                    if (reader["PSN_GraduationTime"] != DBNull.Value) _zjs_CertificateMDL.PSN_GraduationTime = Convert.ToDateTime(reader["PSN_GraduationTime"]);
                    if (reader["PSN_Qualification"] != DBNull.Value) _zjs_CertificateMDL.PSN_Qualification = Convert.ToString(reader["PSN_Qualification"]);
                    if (reader["PSN_Degree"] != DBNull.Value) _zjs_CertificateMDL.PSN_Degree = Convert.ToString(reader["PSN_Degree"]);
                    if (reader["PSN_MobilePhone"] != DBNull.Value) _zjs_CertificateMDL.PSN_MobilePhone = Convert.ToString(reader["PSN_MobilePhone"]);
                    if (reader["PSN_Email"] != DBNull.Value) _zjs_CertificateMDL.PSN_Email = Convert.ToString(reader["PSN_Email"]);
                    if (reader["PSN_Telephone"] != DBNull.Value) _zjs_CertificateMDL.PSN_Telephone = Convert.ToString(reader["PSN_Telephone"]);
                    if (reader["PSN_RegisteType"] != DBNull.Value) _zjs_CertificateMDL.PSN_RegisteType = Convert.ToString(reader["PSN_RegisteType"]);
                    if (reader["PSN_RegisterNO"] != DBNull.Value) _zjs_CertificateMDL.PSN_RegisterNO = Convert.ToString(reader["PSN_RegisterNO"]);
                    if (reader["PSN_RegisterCertificateNo"] != DBNull.Value) _zjs_CertificateMDL.PSN_RegisterCertificateNo = Convert.ToString(reader["PSN_RegisterCertificateNo"]);
                    if (reader["PSN_RegisteProfession"] != DBNull.Value) _zjs_CertificateMDL.PSN_RegisteProfession = Convert.ToString(reader["PSN_RegisteProfession"]);
                    if (reader["PSN_CertificationDate"] != DBNull.Value) _zjs_CertificateMDL.PSN_CertificationDate = Convert.ToDateTime(reader["PSN_CertificationDate"]);
                    if (reader["PSN_CertificateValidity"] != DBNull.Value) _zjs_CertificateMDL.PSN_CertificateValidity = Convert.ToDateTime(reader["PSN_CertificateValidity"]);
                    if (reader["PSN_RegistePermissionDate"] != DBNull.Value) _zjs_CertificateMDL.PSN_RegistePermissionDate = Convert.ToDateTime(reader["PSN_RegistePermissionDate"]);
                    if (reader["PSN_Level"] != DBNull.Value) _zjs_CertificateMDL.PSN_Level = Convert.ToString(reader["PSN_Level"]);
                    if (reader["ZGZSBH"] != DBNull.Value) _zjs_CertificateMDL.ZGZSBH = Convert.ToString(reader["ZGZSBH"]);
                    if (reader["CJR"] != DBNull.Value) _zjs_CertificateMDL.CJR = Convert.ToString(reader["CJR"]);
                    if (reader["CJSJ"] != DBNull.Value) _zjs_CertificateMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
                    if (reader["XGR"] != DBNull.Value) _zjs_CertificateMDL.XGR = Convert.ToString(reader["XGR"]);
                    if (reader["XGSJ"] != DBNull.Value) _zjs_CertificateMDL.XGSJ = Convert.ToDateTime(reader["XGSJ"]);
                    if (reader["Valid"] != DBNull.Value) _zjs_CertificateMDL.Valid = Convert.ToInt32(reader["Valid"]);
                    if (reader["Memo"] != DBNull.Value) _zjs_CertificateMDL.Memo = Convert.ToString(reader["Memo"]);
                    if (reader["ApplyCATime"] != DBNull.Value) _zjs_CertificateMDL.ApplyCATime = Convert.ToDateTime(reader["ApplyCATime"]);
                    if (reader["SendCATime"] != DBNull.Value) _zjs_CertificateMDL.SendCATime = Convert.ToDateTime(reader["SendCATime"]);
                    if (reader["ReturnCATime"] != DBNull.Value) _zjs_CertificateMDL.ReturnCATime = Convert.ToDateTime(reader["ReturnCATime"]);
                    if (reader["CertificateCAID"] != DBNull.Value) _zjs_CertificateMDL.CertificateCAID = Convert.ToString(reader["CertificateCAID"]);
                    if (reader["license_code"] != DBNull.Value) _zjs_CertificateMDL.license_code = Convert.ToString(reader["license_code"]);
                    if (reader["auth_code"] != DBNull.Value) _zjs_CertificateMDL.auth_code = Convert.ToString(reader["auth_code"]);
                    if (reader["SignCATime"] != DBNull.Value) _zjs_CertificateMDL.SignCATime = Convert.ToDateTime(reader["SignCATime"]);
                    list.Add(_zjs_CertificateMDL);
                }
                reader.Close();
                db.Close();
                return list;
            }
        }

        /// <summary>
        /// 根据证件号码获取未注销二级注册造价师证书集合
        /// </summary>
        /// <param name="PSN_CertificateNO">证件号码</param>
        /// <returns>注册证书信息</returns>
        public static List<zjs_CertificateMDL> GetObjectByPSN_CertificateNO_All(string PSN_CertificateNO)
        {
            string sql = @"
			SELECT PSN_ServerID,ENT_Name,ENT_OrganizationsCode,ENT_City,END_Addess,PSN_Name,PSN_Sex,PSN_BirthDate,PSN_National,PSN_CertificateType,PSN_CertificateNO,PSN_GraduationSchool,PSN_Specialty,PSN_GraduationTime,PSN_Qualification,PSN_Degree,PSN_MobilePhone,PSN_Email,PSN_Telephone,PSN_RegisteType,PSN_RegisterNO,PSN_RegisterCertificateNo,PSN_RegisteProfession,PSN_CertificationDate,PSN_CertificateValidity,PSN_RegistePermissionDate,PSN_Level,ZGZSBH,CJR,CJSJ,XGR,XGSJ,[VALID],Memo,ApplyCATime,SendCATime,ReturnCATime,CertificateCAID,license_code,auth_code,SignCATime
			FROM dbo.zjs_Certificate
			WHERE PSN_CertificateNO = @PSN_CertificateNO 
            order by PSN_RegisterNO";
            List<zjs_CertificateMDL> list = new List<zjs_CertificateMDL>();
            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PSN_CertificateNO", DbType.String, PSN_CertificateNO));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                zjs_CertificateMDL _zjs_CertificateMDL = null;
                while (reader.Read())
                {
                    _zjs_CertificateMDL = new zjs_CertificateMDL();
                    if (reader["PSN_ServerID"] != DBNull.Value) _zjs_CertificateMDL.PSN_ServerID = Convert.ToString(reader["PSN_ServerID"]);
                    if (reader["ENT_Name"] != DBNull.Value) _zjs_CertificateMDL.ENT_Name = Convert.ToString(reader["ENT_Name"]);
                    if (reader["ENT_OrganizationsCode"] != DBNull.Value) _zjs_CertificateMDL.ENT_OrganizationsCode = Convert.ToString(reader["ENT_OrganizationsCode"]);
                    if (reader["ENT_City"] != DBNull.Value) _zjs_CertificateMDL.ENT_City = Convert.ToString(reader["ENT_City"]);
                    if (reader["END_Addess"] != DBNull.Value) _zjs_CertificateMDL.END_Addess = Convert.ToString(reader["END_Addess"]);
                    if (reader["PSN_Name"] != DBNull.Value) _zjs_CertificateMDL.PSN_Name = Convert.ToString(reader["PSN_Name"]);
                    if (reader["PSN_Sex"] != DBNull.Value) _zjs_CertificateMDL.PSN_Sex = Convert.ToString(reader["PSN_Sex"]);
                    if (reader["PSN_BirthDate"] != DBNull.Value) _zjs_CertificateMDL.PSN_BirthDate = Convert.ToDateTime(reader["PSN_BirthDate"]);
                    if (reader["PSN_National"] != DBNull.Value) _zjs_CertificateMDL.PSN_National = Convert.ToString(reader["PSN_National"]);
                    if (reader["PSN_CertificateType"] != DBNull.Value) _zjs_CertificateMDL.PSN_CertificateType = Convert.ToString(reader["PSN_CertificateType"]);
                    if (reader["PSN_CertificateNO"] != DBNull.Value) _zjs_CertificateMDL.PSN_CertificateNO = Convert.ToString(reader["PSN_CertificateNO"]);
                    if (reader["PSN_GraduationSchool"] != DBNull.Value) _zjs_CertificateMDL.PSN_GraduationSchool = Convert.ToString(reader["PSN_GraduationSchool"]);
                    if (reader["PSN_Specialty"] != DBNull.Value) _zjs_CertificateMDL.PSN_Specialty = Convert.ToString(reader["PSN_Specialty"]);
                    if (reader["PSN_GraduationTime"] != DBNull.Value) _zjs_CertificateMDL.PSN_GraduationTime = Convert.ToDateTime(reader["PSN_GraduationTime"]);
                    if (reader["PSN_Qualification"] != DBNull.Value) _zjs_CertificateMDL.PSN_Qualification = Convert.ToString(reader["PSN_Qualification"]);
                    if (reader["PSN_Degree"] != DBNull.Value) _zjs_CertificateMDL.PSN_Degree = Convert.ToString(reader["PSN_Degree"]);
                    if (reader["PSN_MobilePhone"] != DBNull.Value) _zjs_CertificateMDL.PSN_MobilePhone = Convert.ToString(reader["PSN_MobilePhone"]);
                    if (reader["PSN_Email"] != DBNull.Value) _zjs_CertificateMDL.PSN_Email = Convert.ToString(reader["PSN_Email"]);
                    if (reader["PSN_Telephone"] != DBNull.Value) _zjs_CertificateMDL.PSN_Telephone = Convert.ToString(reader["PSN_Telephone"]);
                    if (reader["PSN_RegisteType"] != DBNull.Value) _zjs_CertificateMDL.PSN_RegisteType = Convert.ToString(reader["PSN_RegisteType"]);
                    if (reader["PSN_RegisterNO"] != DBNull.Value) _zjs_CertificateMDL.PSN_RegisterNO = Convert.ToString(reader["PSN_RegisterNO"]);
                    if (reader["PSN_RegisterCertificateNo"] != DBNull.Value) _zjs_CertificateMDL.PSN_RegisterCertificateNo = Convert.ToString(reader["PSN_RegisterCertificateNo"]);
                    if (reader["PSN_RegisteProfession"] != DBNull.Value) _zjs_CertificateMDL.PSN_RegisteProfession = Convert.ToString(reader["PSN_RegisteProfession"]);
                    if (reader["PSN_CertificationDate"] != DBNull.Value) _zjs_CertificateMDL.PSN_CertificationDate = Convert.ToDateTime(reader["PSN_CertificationDate"]);
                    if (reader["PSN_CertificateValidity"] != DBNull.Value) _zjs_CertificateMDL.PSN_CertificateValidity = Convert.ToDateTime(reader["PSN_CertificateValidity"]);
                    if (reader["PSN_RegistePermissionDate"] != DBNull.Value) _zjs_CertificateMDL.PSN_RegistePermissionDate = Convert.ToDateTime(reader["PSN_RegistePermissionDate"]);
                    if (reader["PSN_Level"] != DBNull.Value) _zjs_CertificateMDL.PSN_Level = Convert.ToString(reader["PSN_Level"]);
                    if (reader["ZGZSBH"] != DBNull.Value) _zjs_CertificateMDL.ZGZSBH = Convert.ToString(reader["ZGZSBH"]);
                    if (reader["CJR"] != DBNull.Value) _zjs_CertificateMDL.CJR = Convert.ToString(reader["CJR"]);
                    if (reader["CJSJ"] != DBNull.Value) _zjs_CertificateMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
                    if (reader["XGR"] != DBNull.Value) _zjs_CertificateMDL.XGR = Convert.ToString(reader["XGR"]);
                    if (reader["XGSJ"] != DBNull.Value) _zjs_CertificateMDL.XGSJ = Convert.ToDateTime(reader["XGSJ"]);
                    if (reader["Valid"] != DBNull.Value) _zjs_CertificateMDL.Valid = Convert.ToInt32(reader["Valid"]);
                    if (reader["Memo"] != DBNull.Value) _zjs_CertificateMDL.Memo = Convert.ToString(reader["Memo"]);
                    if (reader["ApplyCATime"] != DBNull.Value) _zjs_CertificateMDL.ApplyCATime = Convert.ToDateTime(reader["ApplyCATime"]);
                    if (reader["SendCATime"] != DBNull.Value) _zjs_CertificateMDL.SendCATime = Convert.ToDateTime(reader["SendCATime"]);
                    if (reader["ReturnCATime"] != DBNull.Value) _zjs_CertificateMDL.ReturnCATime = Convert.ToDateTime(reader["ReturnCATime"]);
                    if (reader["CertificateCAID"] != DBNull.Value) _zjs_CertificateMDL.CertificateCAID = Convert.ToString(reader["CertificateCAID"]);
                    if (reader["license_code"] != DBNull.Value) _zjs_CertificateMDL.license_code = Convert.ToString(reader["license_code"]);
                    if (reader["auth_code"] != DBNull.Value) _zjs_CertificateMDL.auth_code = Convert.ToString(reader["auth_code"]);
                    if (reader["SignCATime"] != DBNull.Value) _zjs_CertificateMDL.SignCATime = Convert.ToDateTime(reader["SignCATime"]);
                    list.Add(_zjs_CertificateMDL);
                }
                reader.Close();
                db.Close();
                return list;
            }
        }

        /// <summary>
        /// 根据证件号码和专业获取二级注册造价师证书
        /// </summary>
        /// <param name="PSN_CertificateNO">证件号码</param>
        /// <param name="PSN_RegisteProfession">专业</param>
        /// <returns>注册证书信息</returns>
        public static zjs_CertificateMDL GetObjectByPSN_CertificateNOAndPSN_RegisteProfession(string PSN_CertificateNO, string PSN_RegisteProfession)
        {
            string sql = @"
			SELECT PSN_ServerID,ENT_Name,ENT_OrganizationsCode,ENT_City,END_Addess,PSN_Name,PSN_Sex,PSN_BirthDate,PSN_National,PSN_CertificateType,PSN_CertificateNO,PSN_GraduationSchool,PSN_Specialty,PSN_GraduationTime,PSN_Qualification,PSN_Degree,PSN_MobilePhone,PSN_Email,PSN_Telephone,PSN_RegisteType,PSN_RegisterNO,PSN_RegisterCertificateNo,PSN_RegisteProfession,PSN_CertificationDate,PSN_CertificateValidity,PSN_RegistePermissionDate,PSN_Level,ZGZSBH,CJR,CJSJ,XGR,XGSJ,[VALID],Memo,ApplyCATime,SendCATime,ReturnCATime,CertificateCAID,license_code,auth_code,SignCATime
			FROM dbo.zjs_Certificate
			WHERE PSN_CertificateNO = @PSN_CertificateNO and PSN_RegisteProfession = @PSN_RegisteProfession";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PSN_CertificateNO", DbType.String, PSN_CertificateNO));
            p.Add(db.CreateParameter("PSN_RegisteProfession", DbType.String, PSN_RegisteProfession));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                zjs_CertificateMDL _zjs_CertificateMDL = null;
                if (reader.Read())
                {
                    _zjs_CertificateMDL = new zjs_CertificateMDL();
                    if (reader["PSN_ServerID"] != DBNull.Value) _zjs_CertificateMDL.PSN_ServerID = Convert.ToString(reader["PSN_ServerID"]);
                    if (reader["ENT_Name"] != DBNull.Value) _zjs_CertificateMDL.ENT_Name = Convert.ToString(reader["ENT_Name"]);
                    if (reader["ENT_OrganizationsCode"] != DBNull.Value) _zjs_CertificateMDL.ENT_OrganizationsCode = Convert.ToString(reader["ENT_OrganizationsCode"]);
                    if (reader["ENT_City"] != DBNull.Value) _zjs_CertificateMDL.ENT_City = Convert.ToString(reader["ENT_City"]);
                    if (reader["END_Addess"] != DBNull.Value) _zjs_CertificateMDL.END_Addess = Convert.ToString(reader["END_Addess"]);
                    if (reader["PSN_Name"] != DBNull.Value) _zjs_CertificateMDL.PSN_Name = Convert.ToString(reader["PSN_Name"]);
                    if (reader["PSN_Sex"] != DBNull.Value) _zjs_CertificateMDL.PSN_Sex = Convert.ToString(reader["PSN_Sex"]);
                    if (reader["PSN_BirthDate"] != DBNull.Value) _zjs_CertificateMDL.PSN_BirthDate = Convert.ToDateTime(reader["PSN_BirthDate"]);
                    if (reader["PSN_National"] != DBNull.Value) _zjs_CertificateMDL.PSN_National = Convert.ToString(reader["PSN_National"]);
                    if (reader["PSN_CertificateType"] != DBNull.Value) _zjs_CertificateMDL.PSN_CertificateType = Convert.ToString(reader["PSN_CertificateType"]);
                    if (reader["PSN_CertificateNO"] != DBNull.Value) _zjs_CertificateMDL.PSN_CertificateNO = Convert.ToString(reader["PSN_CertificateNO"]);
                    if (reader["PSN_GraduationSchool"] != DBNull.Value) _zjs_CertificateMDL.PSN_GraduationSchool = Convert.ToString(reader["PSN_GraduationSchool"]);
                    if (reader["PSN_Specialty"] != DBNull.Value) _zjs_CertificateMDL.PSN_Specialty = Convert.ToString(reader["PSN_Specialty"]);
                    if (reader["PSN_GraduationTime"] != DBNull.Value) _zjs_CertificateMDL.PSN_GraduationTime = Convert.ToDateTime(reader["PSN_GraduationTime"]);
                    if (reader["PSN_Qualification"] != DBNull.Value) _zjs_CertificateMDL.PSN_Qualification = Convert.ToString(reader["PSN_Qualification"]);
                    if (reader["PSN_Degree"] != DBNull.Value) _zjs_CertificateMDL.PSN_Degree = Convert.ToString(reader["PSN_Degree"]);
                    if (reader["PSN_MobilePhone"] != DBNull.Value) _zjs_CertificateMDL.PSN_MobilePhone = Convert.ToString(reader["PSN_MobilePhone"]);
                    if (reader["PSN_Email"] != DBNull.Value) _zjs_CertificateMDL.PSN_Email = Convert.ToString(reader["PSN_Email"]);
                    if (reader["PSN_Telephone"] != DBNull.Value) _zjs_CertificateMDL.PSN_Telephone = Convert.ToString(reader["PSN_Telephone"]);
                    if (reader["PSN_RegisteType"] != DBNull.Value) _zjs_CertificateMDL.PSN_RegisteType = Convert.ToString(reader["PSN_RegisteType"]);
                    if (reader["PSN_RegisterNO"] != DBNull.Value) _zjs_CertificateMDL.PSN_RegisterNO = Convert.ToString(reader["PSN_RegisterNO"]);
                    if (reader["PSN_RegisterCertificateNo"] != DBNull.Value) _zjs_CertificateMDL.PSN_RegisterCertificateNo = Convert.ToString(reader["PSN_RegisterCertificateNo"]);
                    if (reader["PSN_RegisteProfession"] != DBNull.Value) _zjs_CertificateMDL.PSN_RegisteProfession = Convert.ToString(reader["PSN_RegisteProfession"]);
                    if (reader["PSN_CertificationDate"] != DBNull.Value) _zjs_CertificateMDL.PSN_CertificationDate = Convert.ToDateTime(reader["PSN_CertificationDate"]);
                    if (reader["PSN_CertificateValidity"] != DBNull.Value) _zjs_CertificateMDL.PSN_CertificateValidity = Convert.ToDateTime(reader["PSN_CertificateValidity"]);
                    if (reader["PSN_RegistePermissionDate"] != DBNull.Value) _zjs_CertificateMDL.PSN_RegistePermissionDate = Convert.ToDateTime(reader["PSN_RegistePermissionDate"]);
                    if (reader["PSN_Level"] != DBNull.Value) _zjs_CertificateMDL.PSN_Level = Convert.ToString(reader["PSN_Level"]);
                    if (reader["ZGZSBH"] != DBNull.Value) _zjs_CertificateMDL.ZGZSBH = Convert.ToString(reader["ZGZSBH"]);
                    if (reader["CJR"] != DBNull.Value) _zjs_CertificateMDL.CJR = Convert.ToString(reader["CJR"]);
                    if (reader["CJSJ"] != DBNull.Value) _zjs_CertificateMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
                    if (reader["XGR"] != DBNull.Value) _zjs_CertificateMDL.XGR = Convert.ToString(reader["XGR"]);
                    if (reader["XGSJ"] != DBNull.Value) _zjs_CertificateMDL.XGSJ = Convert.ToDateTime(reader["XGSJ"]);
                    if (reader["Valid"] != DBNull.Value) _zjs_CertificateMDL.Valid = Convert.ToInt32(reader["Valid"]);
                    if (reader["Memo"] != DBNull.Value) _zjs_CertificateMDL.Memo = Convert.ToString(reader["Memo"]);
                    if (reader["ApplyCATime"] != DBNull.Value) _zjs_CertificateMDL.ApplyCATime = Convert.ToDateTime(reader["ApplyCATime"]);
                    if (reader["SendCATime"] != DBNull.Value) _zjs_CertificateMDL.SendCATime = Convert.ToDateTime(reader["SendCATime"]);
                    if (reader["ReturnCATime"] != DBNull.Value) _zjs_CertificateMDL.ReturnCATime = Convert.ToDateTime(reader["ReturnCATime"]);
                    if (reader["CertificateCAID"] != DBNull.Value) _zjs_CertificateMDL.CertificateCAID = Convert.ToString(reader["CertificateCAID"]);
                    if (reader["license_code"] != DBNull.Value) _zjs_CertificateMDL.license_code = Convert.ToString(reader["license_code"]);
                    if (reader["auth_code"] != DBNull.Value) _zjs_CertificateMDL.auth_code = Convert.ToString(reader["auth_code"]);
                    if (reader["SignCATime"] != DBNull.Value) _zjs_CertificateMDL.SignCATime = Convert.ToDateTime(reader["SignCATime"]);
                }
                reader.Close();
                db.Close();
                return _zjs_CertificateMDL;
            }
        }
        /// <summary>
        /// 根据注册编号获取二级注册造价师证书
        /// </summary>
        /// <param name="PSN_RegisterNO">注册编号</param>
        /// <returns>注册证书信息</returns>
        public static zjs_CertificateMDL GetObjectByPSN_RegisterNO(string PSN_RegisterNO)
        {
            string sql = @"
			SELECT PSN_ServerID,ENT_Name,ENT_OrganizationsCode,ENT_City,END_Addess,PSN_Name,PSN_Sex,PSN_BirthDate,PSN_National,PSN_CertificateType,PSN_CertificateNO,PSN_GraduationSchool,PSN_Specialty,PSN_GraduationTime,PSN_Qualification,PSN_Degree,PSN_MobilePhone,PSN_Email,PSN_Telephone,PSN_RegisteType,PSN_RegisterNO,PSN_RegisterCertificateNo,PSN_RegisteProfession,PSN_CertificationDate,PSN_CertificateValidity,PSN_RegistePermissionDate,PSN_Level,ZGZSBH,CJR,CJSJ,XGR,XGSJ,[VALID],Memo,ApplyCATime,SendCATime,ReturnCATime,CertificateCAID,license_code,auth_code,SignCATime
			FROM dbo.zjs_Certificate
			WHERE PSN_RegisterNO = @PSN_RegisterNO ";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PSN_RegisterNO", DbType.String, PSN_RegisterNO));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                zjs_CertificateMDL _zjs_CertificateMDL = null;
                if (reader.Read())
                {
                    _zjs_CertificateMDL = new zjs_CertificateMDL();
                    if (reader["PSN_ServerID"] != DBNull.Value) _zjs_CertificateMDL.PSN_ServerID = Convert.ToString(reader["PSN_ServerID"]);
                    if (reader["ENT_Name"] != DBNull.Value) _zjs_CertificateMDL.ENT_Name = Convert.ToString(reader["ENT_Name"]);
                    if (reader["ENT_OrganizationsCode"] != DBNull.Value) _zjs_CertificateMDL.ENT_OrganizationsCode = Convert.ToString(reader["ENT_OrganizationsCode"]);
                    if (reader["ENT_City"] != DBNull.Value) _zjs_CertificateMDL.ENT_City = Convert.ToString(reader["ENT_City"]);
                    if (reader["END_Addess"] != DBNull.Value) _zjs_CertificateMDL.END_Addess = Convert.ToString(reader["END_Addess"]);
                    if (reader["PSN_Name"] != DBNull.Value) _zjs_CertificateMDL.PSN_Name = Convert.ToString(reader["PSN_Name"]);
                    if (reader["PSN_Sex"] != DBNull.Value) _zjs_CertificateMDL.PSN_Sex = Convert.ToString(reader["PSN_Sex"]);
                    if (reader["PSN_BirthDate"] != DBNull.Value) _zjs_CertificateMDL.PSN_BirthDate = Convert.ToDateTime(reader["PSN_BirthDate"]);
                    if (reader["PSN_National"] != DBNull.Value) _zjs_CertificateMDL.PSN_National = Convert.ToString(reader["PSN_National"]);
                    if (reader["PSN_CertificateType"] != DBNull.Value) _zjs_CertificateMDL.PSN_CertificateType = Convert.ToString(reader["PSN_CertificateType"]);
                    if (reader["PSN_CertificateNO"] != DBNull.Value) _zjs_CertificateMDL.PSN_CertificateNO = Convert.ToString(reader["PSN_CertificateNO"]);
                    if (reader["PSN_GraduationSchool"] != DBNull.Value) _zjs_CertificateMDL.PSN_GraduationSchool = Convert.ToString(reader["PSN_GraduationSchool"]);
                    if (reader["PSN_Specialty"] != DBNull.Value) _zjs_CertificateMDL.PSN_Specialty = Convert.ToString(reader["PSN_Specialty"]);
                    if (reader["PSN_GraduationTime"] != DBNull.Value) _zjs_CertificateMDL.PSN_GraduationTime = Convert.ToDateTime(reader["PSN_GraduationTime"]);
                    if (reader["PSN_Qualification"] != DBNull.Value) _zjs_CertificateMDL.PSN_Qualification = Convert.ToString(reader["PSN_Qualification"]);
                    if (reader["PSN_Degree"] != DBNull.Value) _zjs_CertificateMDL.PSN_Degree = Convert.ToString(reader["PSN_Degree"]);
                    if (reader["PSN_MobilePhone"] != DBNull.Value) _zjs_CertificateMDL.PSN_MobilePhone = Convert.ToString(reader["PSN_MobilePhone"]);
                    if (reader["PSN_Email"] != DBNull.Value) _zjs_CertificateMDL.PSN_Email = Convert.ToString(reader["PSN_Email"]);
                    if (reader["PSN_Telephone"] != DBNull.Value) _zjs_CertificateMDL.PSN_Telephone = Convert.ToString(reader["PSN_Telephone"]);
                    if (reader["PSN_RegisteType"] != DBNull.Value) _zjs_CertificateMDL.PSN_RegisteType = Convert.ToString(reader["PSN_RegisteType"]);
                    if (reader["PSN_RegisterNO"] != DBNull.Value) _zjs_CertificateMDL.PSN_RegisterNO = Convert.ToString(reader["PSN_RegisterNO"]);
                    if (reader["PSN_RegisterCertificateNo"] != DBNull.Value) _zjs_CertificateMDL.PSN_RegisterCertificateNo = Convert.ToString(reader["PSN_RegisterCertificateNo"]);
                    if (reader["PSN_RegisteProfession"] != DBNull.Value) _zjs_CertificateMDL.PSN_RegisteProfession = Convert.ToString(reader["PSN_RegisteProfession"]);
                    if (reader["PSN_CertificationDate"] != DBNull.Value) _zjs_CertificateMDL.PSN_CertificationDate = Convert.ToDateTime(reader["PSN_CertificationDate"]);
                    if (reader["PSN_CertificateValidity"] != DBNull.Value) _zjs_CertificateMDL.PSN_CertificateValidity = Convert.ToDateTime(reader["PSN_CertificateValidity"]);
                    if (reader["PSN_RegistePermissionDate"] != DBNull.Value) _zjs_CertificateMDL.PSN_RegistePermissionDate = Convert.ToDateTime(reader["PSN_RegistePermissionDate"]);
                    if (reader["PSN_Level"] != DBNull.Value) _zjs_CertificateMDL.PSN_Level = Convert.ToString(reader["PSN_Level"]);
                    if (reader["ZGZSBH"] != DBNull.Value) _zjs_CertificateMDL.ZGZSBH = Convert.ToString(reader["ZGZSBH"]);
                    if (reader["CJR"] != DBNull.Value) _zjs_CertificateMDL.CJR = Convert.ToString(reader["CJR"]);
                    if (reader["CJSJ"] != DBNull.Value) _zjs_CertificateMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
                    if (reader["XGR"] != DBNull.Value) _zjs_CertificateMDL.XGR = Convert.ToString(reader["XGR"]);
                    if (reader["XGSJ"] != DBNull.Value) _zjs_CertificateMDL.XGSJ = Convert.ToDateTime(reader["XGSJ"]);
                    if (reader["Valid"] != DBNull.Value) _zjs_CertificateMDL.Valid = Convert.ToInt32(reader["Valid"]);
                    if (reader["Memo"] != DBNull.Value) _zjs_CertificateMDL.Memo = Convert.ToString(reader["Memo"]);
                    if (reader["ApplyCATime"] != DBNull.Value) _zjs_CertificateMDL.ApplyCATime = Convert.ToDateTime(reader["ApplyCATime"]);
                    if (reader["SendCATime"] != DBNull.Value) _zjs_CertificateMDL.SendCATime = Convert.ToDateTime(reader["SendCATime"]);
                    if (reader["ReturnCATime"] != DBNull.Value) _zjs_CertificateMDL.ReturnCATime = Convert.ToDateTime(reader["ReturnCATime"]);
                    if (reader["CertificateCAID"] != DBNull.Value) _zjs_CertificateMDL.CertificateCAID = Convert.ToString(reader["CertificateCAID"]);
                    if (reader["license_code"] != DBNull.Value) _zjs_CertificateMDL.license_code = Convert.ToString(reader["license_code"]);
                    if (reader["auth_code"] != DBNull.Value) _zjs_CertificateMDL.auth_code = Convert.ToString(reader["auth_code"]);
                    if (reader["SignCATime"] != DBNull.Value) _zjs_CertificateMDL.SignCATime = Convert.ToDateTime(reader["SignCATime"]);
                }
                reader.Close();
                db.Close();
                return _zjs_CertificateMDL;
            }
        }

        /// <summary>
        /// 根据资格证书编号获取二级注册造价师证书
        /// </summary>
        /// <param name="ZGZSBH">资格证书编号</param>
        /// <returns>注册证书信息</returns>
        public static zjs_CertificateMDL GetObjectByZGZSBH(string ZGZSBH)
        {
            string sql = @"
			SELECT PSN_ServerID,ENT_Name,ENT_OrganizationsCode,ENT_City,END_Addess,PSN_Name,PSN_Sex,PSN_BirthDate,PSN_National,PSN_CertificateType,PSN_CertificateNO,PSN_GraduationSchool,PSN_Specialty,PSN_GraduationTime,PSN_Qualification,PSN_Degree,PSN_MobilePhone,PSN_Email,PSN_Telephone,PSN_RegisteType,PSN_RegisterNO,PSN_RegisterCertificateNo,PSN_RegisteProfession,PSN_CertificationDate,PSN_CertificateValidity,PSN_RegistePermissionDate,PSN_Level,ZGZSBH,CJR,CJSJ,XGR,XGSJ,[VALID],Memo,ApplyCATime,SendCATime,ReturnCATime,CertificateCAID,license_code,auth_code,SignCATime
			FROM dbo.zjs_Certificate
			WHERE ZGZSBH = @ZGZSBH ";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ZGZSBH", DbType.String, ZGZSBH));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                zjs_CertificateMDL _zjs_CertificateMDL = null;
                if (reader.Read())
                {
                    _zjs_CertificateMDL = new zjs_CertificateMDL();
                    if (reader["PSN_ServerID"] != DBNull.Value) _zjs_CertificateMDL.PSN_ServerID = Convert.ToString(reader["PSN_ServerID"]);
                    if (reader["ENT_Name"] != DBNull.Value) _zjs_CertificateMDL.ENT_Name = Convert.ToString(reader["ENT_Name"]);
                    if (reader["ENT_OrganizationsCode"] != DBNull.Value) _zjs_CertificateMDL.ENT_OrganizationsCode = Convert.ToString(reader["ENT_OrganizationsCode"]);
                    if (reader["ENT_City"] != DBNull.Value) _zjs_CertificateMDL.ENT_City = Convert.ToString(reader["ENT_City"]);
                    if (reader["END_Addess"] != DBNull.Value) _zjs_CertificateMDL.END_Addess = Convert.ToString(reader["END_Addess"]);
                    if (reader["PSN_Name"] != DBNull.Value) _zjs_CertificateMDL.PSN_Name = Convert.ToString(reader["PSN_Name"]);
                    if (reader["PSN_Sex"] != DBNull.Value) _zjs_CertificateMDL.PSN_Sex = Convert.ToString(reader["PSN_Sex"]);
                    if (reader["PSN_BirthDate"] != DBNull.Value) _zjs_CertificateMDL.PSN_BirthDate = Convert.ToDateTime(reader["PSN_BirthDate"]);
                    if (reader["PSN_National"] != DBNull.Value) _zjs_CertificateMDL.PSN_National = Convert.ToString(reader["PSN_National"]);
                    if (reader["PSN_CertificateType"] != DBNull.Value) _zjs_CertificateMDL.PSN_CertificateType = Convert.ToString(reader["PSN_CertificateType"]);
                    if (reader["PSN_CertificateNO"] != DBNull.Value) _zjs_CertificateMDL.PSN_CertificateNO = Convert.ToString(reader["PSN_CertificateNO"]);
                    if (reader["PSN_GraduationSchool"] != DBNull.Value) _zjs_CertificateMDL.PSN_GraduationSchool = Convert.ToString(reader["PSN_GraduationSchool"]);
                    if (reader["PSN_Specialty"] != DBNull.Value) _zjs_CertificateMDL.PSN_Specialty = Convert.ToString(reader["PSN_Specialty"]);
                    if (reader["PSN_GraduationTime"] != DBNull.Value) _zjs_CertificateMDL.PSN_GraduationTime = Convert.ToDateTime(reader["PSN_GraduationTime"]);
                    if (reader["PSN_Qualification"] != DBNull.Value) _zjs_CertificateMDL.PSN_Qualification = Convert.ToString(reader["PSN_Qualification"]);
                    if (reader["PSN_Degree"] != DBNull.Value) _zjs_CertificateMDL.PSN_Degree = Convert.ToString(reader["PSN_Degree"]);
                    if (reader["PSN_MobilePhone"] != DBNull.Value) _zjs_CertificateMDL.PSN_MobilePhone = Convert.ToString(reader["PSN_MobilePhone"]);
                    if (reader["PSN_Email"] != DBNull.Value) _zjs_CertificateMDL.PSN_Email = Convert.ToString(reader["PSN_Email"]);
                    if (reader["PSN_Telephone"] != DBNull.Value) _zjs_CertificateMDL.PSN_Telephone = Convert.ToString(reader["PSN_Telephone"]);
                    if (reader["PSN_RegisteType"] != DBNull.Value) _zjs_CertificateMDL.PSN_RegisteType = Convert.ToString(reader["PSN_RegisteType"]);
                    if (reader["PSN_RegisterNO"] != DBNull.Value) _zjs_CertificateMDL.PSN_RegisterNO = Convert.ToString(reader["PSN_RegisterNO"]);
                    if (reader["PSN_RegisterCertificateNo"] != DBNull.Value) _zjs_CertificateMDL.PSN_RegisterCertificateNo = Convert.ToString(reader["PSN_RegisterCertificateNo"]);
                    if (reader["PSN_RegisteProfession"] != DBNull.Value) _zjs_CertificateMDL.PSN_RegisteProfession = Convert.ToString(reader["PSN_RegisteProfession"]);
                    if (reader["PSN_CertificationDate"] != DBNull.Value) _zjs_CertificateMDL.PSN_CertificationDate = Convert.ToDateTime(reader["PSN_CertificationDate"]);
                    if (reader["PSN_CertificateValidity"] != DBNull.Value) _zjs_CertificateMDL.PSN_CertificateValidity = Convert.ToDateTime(reader["PSN_CertificateValidity"]);
                    if (reader["PSN_RegistePermissionDate"] != DBNull.Value) _zjs_CertificateMDL.PSN_RegistePermissionDate = Convert.ToDateTime(reader["PSN_RegistePermissionDate"]);
                    if (reader["PSN_Level"] != DBNull.Value) _zjs_CertificateMDL.PSN_Level = Convert.ToString(reader["PSN_Level"]);
                    if (reader["ZGZSBH"] != DBNull.Value) _zjs_CertificateMDL.ZGZSBH = Convert.ToString(reader["ZGZSBH"]);
                    if (reader["CJR"] != DBNull.Value) _zjs_CertificateMDL.CJR = Convert.ToString(reader["CJR"]);
                    if (reader["CJSJ"] != DBNull.Value) _zjs_CertificateMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
                    if (reader["XGR"] != DBNull.Value) _zjs_CertificateMDL.XGR = Convert.ToString(reader["XGR"]);
                    if (reader["XGSJ"] != DBNull.Value) _zjs_CertificateMDL.XGSJ = Convert.ToDateTime(reader["XGSJ"]);
                    if (reader["Valid"] != DBNull.Value) _zjs_CertificateMDL.Valid = Convert.ToInt32(reader["Valid"]);
                    if (reader["Memo"] != DBNull.Value) _zjs_CertificateMDL.Memo = Convert.ToString(reader["Memo"]);
                    if (reader["ApplyCATime"] != DBNull.Value) _zjs_CertificateMDL.ApplyCATime = Convert.ToDateTime(reader["ApplyCATime"]);
                    if (reader["SendCATime"] != DBNull.Value) _zjs_CertificateMDL.SendCATime = Convert.ToDateTime(reader["SendCATime"]);
                    if (reader["ReturnCATime"] != DBNull.Value) _zjs_CertificateMDL.ReturnCATime = Convert.ToDateTime(reader["ReturnCATime"]);
                    if (reader["CertificateCAID"] != DBNull.Value) _zjs_CertificateMDL.CertificateCAID = Convert.ToString(reader["CertificateCAID"]);
                    if (reader["license_code"] != DBNull.Value) _zjs_CertificateMDL.license_code = Convert.ToString(reader["license_code"]);
                    if (reader["auth_code"] != DBNull.Value) _zjs_CertificateMDL.auth_code = Convert.ToString(reader["auth_code"]);
                    if (reader["SignCATime"] != DBNull.Value) _zjs_CertificateMDL.SignCATime = Convert.ToDateTime(reader["SignCATime"]);
                }
                reader.Close();
                db.Close();
                return _zjs_CertificateMDL;
            }
        }

        /// <summary>
        /// 更新上传建设部时间
        /// </summary>
        /// <param name="PSN_ServerID">证书ID</param>
        /// <returns></returns>
        public static int UpdateUpJsbTime(string PSN_ServerID)
        {
            string sql = @"
			UPDATE dbo.zjs_Certificate 
            SET	UpJsbTime = getdate()
            WHERE PSN_ServerID = @PSN_ServerID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PSN_ServerID", DbType.String, PSN_ServerID));           
            return db.GetExcuteNonQuery(sql, p.ToArray());
        }

        /// <summary>
        /// 获取二建业务执法检查对象集合
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        public static DataTable GetListCheck(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.[TJ_ZJGCS_TOW_Check]", "*", filterWhereString, orderBy == "" ? " [PSN_CertificateNO]" : orderBy);
        }
        /// <summary>
        /// 统计二建业务执法检查对象记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountCheck(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.[TJ_ZJGCS_TOW_Check]", filterWhereString);
        }

        /// <summary>
        /// 按专业统计注册造价工程师数量
        /// </summary>
        /// <param name="filterWhereString">过滤条件</param>
        /// <returns></returns>
        public static DataTable GetZhuanYe(string filterWhereString)
        {
            DBHelper db = new DBHelper();
            string sql = @" select [PSN_RegisteProfession],count(*) as num 
                            from  [dbo].[zjs_Certificate]
                            where 1=1 {0}
                            group by [PSN_RegisteProfession] ";
            return db.GetFillData(string.Format(sql, filterWhereString));
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.[View_ZJS]", "*", filterWhereString, orderBy == "" ? " [PSN_ServerID]" : orderBy);
//            return CommonDAL.GetDataTable(startRowIndex, maximumRows,
//               @"dbo.zjs_Certificate A 
//                    left join [dbo].[View_ZJS_Applying] B 
//                        on a.PSN_ServerID=b.PSN_ServerID                    
//                    left join [dbo].[LockZJS] L
//                        on A.[PSN_CertificateNO] = L.[PSN_CertificateNO] and L.LockStatus='加锁' and L.LockEndTime > getdate()"
//               , "A.*,L.LockID,L.LockStatus,L.LockType,L.LockRemark", filterWhereString, orderBy == "" ? "A.psn_serverid" : orderBy);

        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountView(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.[View_ZJS]", filterWhereString);
//            return CommonDAL.SelectRowCount(@"
//                    dbo.zjs_Certificate A 
//                    left join [dbo].[View_ZJS_Applying] B 
//                        on a.PSN_ServerID=b.PSN_ServerID                  
//                    left join [dbo].[LockZJS] L
//                        on A.[PSN_CertificateNO] = L.[PSN_CertificateNO] and L.LockStatus='加锁' and L.LockEndTime > getdate()", filterWhereString);
        }

        #endregion
    }
}
