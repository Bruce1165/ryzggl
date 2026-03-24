using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--COC_TOW_Person_BaseInfo_HisDAL(填写类描述)
	/// </summary>
    public class COC_TOW_Person_BaseInfo_HisDAL
    {
        public COC_TOW_Person_BaseInfo_HisDAL() { }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="COC_TOW_Person_BaseInfo_HisMDL">对象实体类</param>
        /// <returns></returns>
        public static int Insert(COC_TOW_Person_BaseInfo_HisMDL _COC_TOW_Person_BaseInfo_HisMDL)
        {
            return Insert(null, _COC_TOW_Person_BaseInfo_HisMDL);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="COC_TOW_Person_BaseInfo_HisMDL">对象实体类</param>
        /// <returns></returns>
        public static int Insert(DbTransaction tran, COC_TOW_Person_BaseInfo_HisMDL _COC_TOW_Person_BaseInfo_HisMDL)
        {
            DBHelper db = new DBHelper();
            string sql = @"
			INSERT INTO dbo.COC_TOW_Person_BaseInfo_His(HisID,PSN_ServerID,ENT_ServerID,ENT_Name,ENT_OrganizationsCode,ENT_City,BeginTime,EndTime,PSN_Name,PSN_Sex,PSN_BirthDate,PSN_National,PSN_CertificateType,PSN_CertificateNO,PSN_GraduationSchool,PSN_Specialty,PSN_GraduationTime,PSN_Qualification,PSN_Degree,PSN_MobilePhone,PSN_Telephone,PSN_Email,PSN_PMGrade,PSN_PMCertificateNo,PSN_RegisteType,PSN_RegisterNO,PSN_RegisterCertificateNo,PSN_RegisteProfession,PSN_CertificationDate,PSN_CertificateValidity,PSN_RegistePermissionDate,PSN_ChangeReason,PSN_BeforENT_Name,PSN_BeforENT_ServerID,PSN_BeforPersonName,PSN_InterprovincialChange,PSN_ExpiryReasons,PSN_ExpiryDate,PSN_RenewalProfession,PSN_AddProfession,PSN_CancelPerson,PSN_CancelReason,PSN_ReReasons,PSN_ReContent,PSN_CheckCode,ENT_Province_Code,PSN_Level,CJR,CJSJ,XGR,XGSJ,[VALID],Memo,GteDate)
			VALUES (@HisID,@PSN_ServerID,@ENT_ServerID,@ENT_Name,@ENT_OrganizationsCode,@ENT_City,@BeginTime,@EndTime,@PSN_Name,@PSN_Sex,@PSN_BirthDate,@PSN_National,@PSN_CertificateType,@PSN_CertificateNO,@PSN_GraduationSchool,@PSN_Specialty,@PSN_GraduationTime,@PSN_Qualification,@PSN_Degree,@PSN_MobilePhone,@PSN_Telephone,@PSN_Email,@PSN_PMGrade,@PSN_PMCertificateNo,@PSN_RegisteType,@PSN_RegisterNO,@PSN_RegisterCertificateNo,@PSN_RegisteProfession,@PSN_CertificationDate,@PSN_CertificateValidity,@PSN_RegistePermissionDate,@PSN_ChangeReason,@PSN_BeforENT_Name,@PSN_BeforENT_ServerID,@PSN_BeforPersonName,@PSN_InterprovincialChange,@PSN_ExpiryReasons,@PSN_ExpiryDate,@PSN_RenewalProfession,@PSN_AddProfession,@PSN_CancelPerson,@PSN_CancelReason,@PSN_ReReasons,@PSN_ReContent,@PSN_CheckCode,@ENT_Province_Code,@PSN_Level,@CJR,@CJSJ,@XGR,@XGSJ,@Valid,@Memo,@GteDate)";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("HisID", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.HisID));
            p.Add(db.CreateParameter("PSN_ServerID", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_ServerID));
            p.Add(db.CreateParameter("ENT_ServerID", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.ENT_ServerID));
            p.Add(db.CreateParameter("ENT_Name", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.ENT_Name));
            p.Add(db.CreateParameter("ENT_OrganizationsCode", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.ENT_OrganizationsCode));
            p.Add(db.CreateParameter("ENT_City", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.ENT_City));
            p.Add(db.CreateParameter("BeginTime", DbType.DateTime, _COC_TOW_Person_BaseInfo_HisMDL.BeginTime));
            p.Add(db.CreateParameter("EndTime", DbType.DateTime, _COC_TOW_Person_BaseInfo_HisMDL.EndTime));
            p.Add(db.CreateParameter("PSN_Name", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_Name));
            p.Add(db.CreateParameter("PSN_Sex", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_Sex));
            p.Add(db.CreateParameter("PSN_BirthDate", DbType.DateTime, _COC_TOW_Person_BaseInfo_HisMDL.PSN_BirthDate));
            p.Add(db.CreateParameter("PSN_National", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_National));
            p.Add(db.CreateParameter("PSN_CertificateType", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_CertificateType));
            p.Add(db.CreateParameter("PSN_CertificateNO", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_CertificateNO));
            p.Add(db.CreateParameter("PSN_GraduationSchool", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_GraduationSchool));
            p.Add(db.CreateParameter("PSN_Specialty", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_Specialty));
            p.Add(db.CreateParameter("PSN_GraduationTime", DbType.DateTime, _COC_TOW_Person_BaseInfo_HisMDL.PSN_GraduationTime));
            p.Add(db.CreateParameter("PSN_Qualification", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_Qualification));
            p.Add(db.CreateParameter("PSN_Degree", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_Degree));
            p.Add(db.CreateParameter("PSN_MobilePhone", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_MobilePhone));
            p.Add(db.CreateParameter("PSN_Telephone", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_Telephone));
            p.Add(db.CreateParameter("PSN_Email", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_Email));
            p.Add(db.CreateParameter("PSN_PMGrade", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_PMGrade));
            p.Add(db.CreateParameter("PSN_PMCertificateNo", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_PMCertificateNo));
            p.Add(db.CreateParameter("PSN_RegisteType", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_RegisteType));
            p.Add(db.CreateParameter("PSN_RegisterNO", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_RegisterNO));
            p.Add(db.CreateParameter("PSN_RegisterCertificateNo", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_RegisterCertificateNo));
            p.Add(db.CreateParameter("PSN_RegisteProfession", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_RegisteProfession));
            p.Add(db.CreateParameter("PSN_CertificationDate", DbType.DateTime, _COC_TOW_Person_BaseInfo_HisMDL.PSN_CertificationDate));
            p.Add(db.CreateParameter("PSN_CertificateValidity", DbType.DateTime, _COC_TOW_Person_BaseInfo_HisMDL.PSN_CertificateValidity));
            p.Add(db.CreateParameter("PSN_RegistePermissionDate", DbType.DateTime, _COC_TOW_Person_BaseInfo_HisMDL.PSN_RegistePermissionDate));
            p.Add(db.CreateParameter("PSN_ChangeReason", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_ChangeReason));
            p.Add(db.CreateParameter("PSN_BeforENT_Name", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_BeforENT_Name));
            p.Add(db.CreateParameter("PSN_BeforENT_ServerID", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_BeforENT_ServerID));
            p.Add(db.CreateParameter("PSN_BeforPersonName", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_BeforPersonName));
            p.Add(db.CreateParameter("PSN_InterprovincialChange", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_InterprovincialChange));
            p.Add(db.CreateParameter("PSN_ExpiryReasons", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_ExpiryReasons));
            p.Add(db.CreateParameter("PSN_ExpiryDate", DbType.DateTime, _COC_TOW_Person_BaseInfo_HisMDL.PSN_ExpiryDate));
            p.Add(db.CreateParameter("PSN_RenewalProfession", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_RenewalProfession));
            p.Add(db.CreateParameter("PSN_AddProfession", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_AddProfession));
            p.Add(db.CreateParameter("PSN_CancelPerson", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_CancelPerson));
            p.Add(db.CreateParameter("PSN_CancelReason", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_CancelReason));
            p.Add(db.CreateParameter("PSN_ReReasons", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_ReReasons));
            p.Add(db.CreateParameter("PSN_ReContent", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_ReContent));
            p.Add(db.CreateParameter("PSN_CheckCode", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_CheckCode));
            p.Add(db.CreateParameter("ENT_Province_Code", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.ENT_Province_Code));
            p.Add(db.CreateParameter("PSN_Level", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_Level));
            p.Add(db.CreateParameter("CJR", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.CJR));
            p.Add(db.CreateParameter("CJSJ", DbType.DateTime, _COC_TOW_Person_BaseInfo_HisMDL.CJSJ));
            p.Add(db.CreateParameter("XGR", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.XGR));
            p.Add(db.CreateParameter("XGSJ", DbType.DateTime, _COC_TOW_Person_BaseInfo_HisMDL.XGSJ));
            p.Add(db.CreateParameter("Valid", DbType.Int32, _COC_TOW_Person_BaseInfo_HisMDL.Valid));
            p.Add(db.CreateParameter("Memo", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.Memo));
            p.Add(db.CreateParameter("GteDate", DbType.DateTime, _COC_TOW_Person_BaseInfo_HisMDL.GteDate));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="COC_TOW_Person_BaseInfo_HisMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(COC_TOW_Person_BaseInfo_HisMDL _COC_TOW_Person_BaseInfo_HisMDL)
        {
            return Update(null, _COC_TOW_Person_BaseInfo_HisMDL);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="COC_TOW_Person_BaseInfo_HisMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, COC_TOW_Person_BaseInfo_HisMDL _COC_TOW_Person_BaseInfo_HisMDL)
        {
            string sql = @"
			UPDATE dbo.COC_TOW_Person_BaseInfo_His
				SET	PSN_ServerID = @PSN_ServerID,ENT_ServerID = @ENT_ServerID,ENT_Name = @ENT_Name,ENT_OrganizationsCode = @ENT_OrganizationsCode,ENT_City = @ENT_City,BeginTime = @BeginTime,EndTime = @EndTime,PSN_Name = @PSN_Name,PSN_Sex = @PSN_Sex,PSN_BirthDate = @PSN_BirthDate,PSN_National = @PSN_National,PSN_CertificateType = @PSN_CertificateType,PSN_CertificateNO = @PSN_CertificateNO,PSN_GraduationSchool = @PSN_GraduationSchool,PSN_Specialty = @PSN_Specialty,PSN_GraduationTime = @PSN_GraduationTime,PSN_Qualification = @PSN_Qualification,PSN_Degree = @PSN_Degree,PSN_MobilePhone = @PSN_MobilePhone,PSN_Telephone = @PSN_Telephone,PSN_Email = @PSN_Email,PSN_PMGrade = @PSN_PMGrade,PSN_PMCertificateNo = @PSN_PMCertificateNo,PSN_RegisteType = @PSN_RegisteType,PSN_RegisterNO = @PSN_RegisterNO,PSN_RegisterCertificateNo = @PSN_RegisterCertificateNo,PSN_RegisteProfession = @PSN_RegisteProfession,PSN_CertificationDate = @PSN_CertificationDate,PSN_CertificateValidity = @PSN_CertificateValidity,PSN_RegistePermissionDate = @PSN_RegistePermissionDate,PSN_ChangeReason = @PSN_ChangeReason,PSN_BeforENT_Name = @PSN_BeforENT_Name,PSN_BeforENT_ServerID = @PSN_BeforENT_ServerID,PSN_BeforPersonName = @PSN_BeforPersonName,PSN_InterprovincialChange = @PSN_InterprovincialChange,PSN_ExpiryReasons = @PSN_ExpiryReasons,PSN_ExpiryDate = @PSN_ExpiryDate,PSN_RenewalProfession = @PSN_RenewalProfession,PSN_AddProfession = @PSN_AddProfession,PSN_CancelPerson = @PSN_CancelPerson,PSN_CancelReason = @PSN_CancelReason,PSN_ReReasons = @PSN_ReReasons,PSN_ReContent = @PSN_ReContent,PSN_CheckCode = @PSN_CheckCode,ENT_Province_Code = @ENT_Province_Code,PSN_Level = @PSN_Level,CJR = @CJR,CJSJ = @CJSJ,XGR = @XGR,XGSJ = @XGSJ,""VALID"" = @Valid,Memo = @Memo,GteDate = @GteDate
			WHERE
				HisID = @HisID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("HisID", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.HisID));
            p.Add(db.CreateParameter("PSN_ServerID", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_ServerID));
            p.Add(db.CreateParameter("ENT_ServerID", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.ENT_ServerID));
            p.Add(db.CreateParameter("ENT_Name", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.ENT_Name));
            p.Add(db.CreateParameter("ENT_OrganizationsCode", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.ENT_OrganizationsCode));
            p.Add(db.CreateParameter("ENT_City", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.ENT_City));
            p.Add(db.CreateParameter("BeginTime", DbType.DateTime, _COC_TOW_Person_BaseInfo_HisMDL.BeginTime));
            p.Add(db.CreateParameter("EndTime", DbType.DateTime, _COC_TOW_Person_BaseInfo_HisMDL.EndTime));
            p.Add(db.CreateParameter("PSN_Name", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_Name));
            p.Add(db.CreateParameter("PSN_Sex", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_Sex));
            p.Add(db.CreateParameter("PSN_BirthDate", DbType.DateTime, _COC_TOW_Person_BaseInfo_HisMDL.PSN_BirthDate));
            p.Add(db.CreateParameter("PSN_National", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_National));
            p.Add(db.CreateParameter("PSN_CertificateType", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_CertificateType));
            p.Add(db.CreateParameter("PSN_CertificateNO", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_CertificateNO));
            p.Add(db.CreateParameter("PSN_GraduationSchool", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_GraduationSchool));
            p.Add(db.CreateParameter("PSN_Specialty", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_Specialty));
            p.Add(db.CreateParameter("PSN_GraduationTime", DbType.DateTime, _COC_TOW_Person_BaseInfo_HisMDL.PSN_GraduationTime));
            p.Add(db.CreateParameter("PSN_Qualification", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_Qualification));
            p.Add(db.CreateParameter("PSN_Degree", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_Degree));
            p.Add(db.CreateParameter("PSN_MobilePhone", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_MobilePhone));
            p.Add(db.CreateParameter("PSN_Telephone", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_Telephone));
            p.Add(db.CreateParameter("PSN_Email", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_Email));
            p.Add(db.CreateParameter("PSN_PMGrade", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_PMGrade));
            p.Add(db.CreateParameter("PSN_PMCertificateNo", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_PMCertificateNo));
            p.Add(db.CreateParameter("PSN_RegisteType", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_RegisteType));
            p.Add(db.CreateParameter("PSN_RegisterNO", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_RegisterNO));
            p.Add(db.CreateParameter("PSN_RegisterCertificateNo", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_RegisterCertificateNo));
            p.Add(db.CreateParameter("PSN_RegisteProfession", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_RegisteProfession));
            p.Add(db.CreateParameter("PSN_CertificationDate", DbType.DateTime, _COC_TOW_Person_BaseInfo_HisMDL.PSN_CertificationDate));
            p.Add(db.CreateParameter("PSN_CertificateValidity", DbType.DateTime, _COC_TOW_Person_BaseInfo_HisMDL.PSN_CertificateValidity));
            p.Add(db.CreateParameter("PSN_RegistePermissionDate", DbType.DateTime, _COC_TOW_Person_BaseInfo_HisMDL.PSN_RegistePermissionDate));
            p.Add(db.CreateParameter("PSN_ChangeReason", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_ChangeReason));
            p.Add(db.CreateParameter("PSN_BeforENT_Name", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_BeforENT_Name));
            p.Add(db.CreateParameter("PSN_BeforENT_ServerID", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_BeforENT_ServerID));
            p.Add(db.CreateParameter("PSN_BeforPersonName", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_BeforPersonName));
            p.Add(db.CreateParameter("PSN_InterprovincialChange", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_InterprovincialChange));
            p.Add(db.CreateParameter("PSN_ExpiryReasons", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_ExpiryReasons));
            p.Add(db.CreateParameter("PSN_ExpiryDate", DbType.DateTime, _COC_TOW_Person_BaseInfo_HisMDL.PSN_ExpiryDate));
            p.Add(db.CreateParameter("PSN_RenewalProfession", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_RenewalProfession));
            p.Add(db.CreateParameter("PSN_AddProfession", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_AddProfession));
            p.Add(db.CreateParameter("PSN_CancelPerson", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_CancelPerson));
            p.Add(db.CreateParameter("PSN_CancelReason", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_CancelReason));
            p.Add(db.CreateParameter("PSN_ReReasons", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_ReReasons));
            p.Add(db.CreateParameter("PSN_ReContent", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_ReContent));
            p.Add(db.CreateParameter("PSN_CheckCode", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_CheckCode));
            p.Add(db.CreateParameter("ENT_Province_Code", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.ENT_Province_Code));
            p.Add(db.CreateParameter("PSN_Level", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.PSN_Level));
            p.Add(db.CreateParameter("CJR", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.CJR));
            p.Add(db.CreateParameter("CJSJ", DbType.DateTime, _COC_TOW_Person_BaseInfo_HisMDL.CJSJ));
            p.Add(db.CreateParameter("XGR", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.XGR));
            p.Add(db.CreateParameter("XGSJ", DbType.DateTime, _COC_TOW_Person_BaseInfo_HisMDL.XGSJ));
            p.Add(db.CreateParameter("Valid", DbType.Int32, _COC_TOW_Person_BaseInfo_HisMDL.Valid));
            p.Add(db.CreateParameter("Memo", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.Memo));
            p.Add(db.CreateParameter("GteDate", DbType.DateTime, _COC_TOW_Person_BaseInfo_HisMDL.GteDate));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="COC_TOW_Person_BaseInfo_HisID">主键</param>
        /// <returns></returns>
        public static int Delete(string HisID)
        {
            return Delete(null, HisID);
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="COC_TOW_Person_BaseInfo_HisID">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, string HisID)
        {
            string sql = @"DELETE FROM dbo.COC_TOW_Person_BaseInfo_His WHERE HisID = @HisID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("HisID", DbType.String, HisID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="COC_TOW_Person_BaseInfo_HisMDL">对象实体类</param>
        /// <returns></returns>
        public static int Delete(COC_TOW_Person_BaseInfo_HisMDL _COC_TOW_Person_BaseInfo_HisMDL)
        {
            return Delete(null, _COC_TOW_Person_BaseInfo_HisMDL);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="COC_TOW_Person_BaseInfo_HisMDL">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, COC_TOW_Person_BaseInfo_HisMDL _COC_TOW_Person_BaseInfo_HisMDL)
        {
            string sql = @"DELETE FROM dbo.COC_TOW_Person_BaseInfo_His WHERE HisID = @HisID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("HisID", DbType.String, _COC_TOW_Person_BaseInfo_HisMDL.HisID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="COC_TOW_Person_BaseInfo_HisID">主键</param>
        public static COC_TOW_Person_BaseInfo_HisMDL GetObject(string HisID)
        {
            string sql = @"
			SELECT HisID,PSN_ServerID,ENT_ServerID,ENT_Name,ENT_OrganizationsCode,ENT_City,BeginTime,EndTime,PSN_Name,PSN_Sex,PSN_BirthDate,PSN_National,PSN_CertificateType,PSN_CertificateNO,PSN_GraduationSchool,PSN_Specialty,PSN_GraduationTime,PSN_Qualification,PSN_Degree,PSN_MobilePhone,PSN_Telephone,PSN_Email,PSN_PMGrade,PSN_PMCertificateNo,PSN_RegisteType,PSN_RegisterNO,PSN_RegisterCertificateNo,PSN_RegisteProfession,PSN_CertificationDate,PSN_CertificateValidity,PSN_RegistePermissionDate,PSN_ChangeReason,PSN_BeforENT_Name,PSN_BeforENT_ServerID,PSN_BeforPersonName,PSN_InterprovincialChange,PSN_ExpiryReasons,PSN_ExpiryDate,PSN_RenewalProfession,PSN_AddProfession,PSN_CancelPerson,PSN_CancelReason,PSN_ReReasons,PSN_ReContent,PSN_CheckCode,ENT_Province_Code,PSN_Level,CJR,CJSJ,XGR,XGSJ,[VALID],Memo,GteDate
			FROM dbo.COC_TOW_Person_BaseInfo_His
			WHERE HisID = @HisID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("HisID", DbType.String, HisID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                COC_TOW_Person_BaseInfo_HisMDL _COC_TOW_Person_BaseInfo_HisMDL = null;
                if (reader.Read())
                {
                    _COC_TOW_Person_BaseInfo_HisMDL = new COC_TOW_Person_BaseInfo_HisMDL();
                    if (reader["HisID"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.HisID = Convert.ToString(reader["HisID"]);
                    if (reader["PSN_ServerID"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_ServerID = Convert.ToString(reader["PSN_ServerID"]);
                    if (reader["ENT_ServerID"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.ENT_ServerID = Convert.ToString(reader["ENT_ServerID"]);
                    if (reader["ENT_Name"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.ENT_Name = Convert.ToString(reader["ENT_Name"]);
                    if (reader["ENT_OrganizationsCode"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.ENT_OrganizationsCode = Convert.ToString(reader["ENT_OrganizationsCode"]);
                    if (reader["ENT_City"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.ENT_City = Convert.ToString(reader["ENT_City"]);
                    if (reader["BeginTime"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.BeginTime = Convert.ToDateTime(reader["BeginTime"]);
                    if (reader["EndTime"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.EndTime = Convert.ToDateTime(reader["EndTime"]);
                    if (reader["PSN_Name"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_Name = Convert.ToString(reader["PSN_Name"]);
                    if (reader["PSN_Sex"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_Sex = Convert.ToString(reader["PSN_Sex"]);
                    if (reader["PSN_BirthDate"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_BirthDate = Convert.ToDateTime(reader["PSN_BirthDate"]);
                    if (reader["PSN_National"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_National = Convert.ToString(reader["PSN_National"]);
                    if (reader["PSN_CertificateType"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_CertificateType = Convert.ToString(reader["PSN_CertificateType"]);
                    if (reader["PSN_CertificateNO"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_CertificateNO = Convert.ToString(reader["PSN_CertificateNO"]);
                    if (reader["PSN_GraduationSchool"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_GraduationSchool = Convert.ToString(reader["PSN_GraduationSchool"]);
                    if (reader["PSN_Specialty"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_Specialty = Convert.ToString(reader["PSN_Specialty"]);
                    if (reader["PSN_GraduationTime"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_GraduationTime = Convert.ToDateTime(reader["PSN_GraduationTime"]);
                    if (reader["PSN_Qualification"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_Qualification = Convert.ToString(reader["PSN_Qualification"]);
                    if (reader["PSN_Degree"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_Degree = Convert.ToString(reader["PSN_Degree"]);
                    if (reader["PSN_MobilePhone"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_MobilePhone = Convert.ToString(reader["PSN_MobilePhone"]);
                    if (reader["PSN_Telephone"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_Telephone = Convert.ToString(reader["PSN_Telephone"]);
                    if (reader["PSN_Email"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_Email = Convert.ToString(reader["PSN_Email"]);
                    if (reader["PSN_PMGrade"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_PMGrade = Convert.ToString(reader["PSN_PMGrade"]);
                    if (reader["PSN_PMCertificateNo"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_PMCertificateNo = Convert.ToString(reader["PSN_PMCertificateNo"]);
                    if (reader["PSN_RegisteType"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_RegisteType = Convert.ToString(reader["PSN_RegisteType"]);
                    if (reader["PSN_RegisterNO"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_RegisterNO = Convert.ToString(reader["PSN_RegisterNO"]);
                    if (reader["PSN_RegisterCertificateNo"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_RegisterCertificateNo = Convert.ToString(reader["PSN_RegisterCertificateNo"]);
                    if (reader["PSN_RegisteProfession"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_RegisteProfession = Convert.ToString(reader["PSN_RegisteProfession"]);
                    if (reader["PSN_CertificationDate"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_CertificationDate = Convert.ToDateTime(reader["PSN_CertificationDate"]);
                    if (reader["PSN_CertificateValidity"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_CertificateValidity = Convert.ToDateTime(reader["PSN_CertificateValidity"]);
                    if (reader["PSN_RegistePermissionDate"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_RegistePermissionDate = Convert.ToDateTime(reader["PSN_RegistePermissionDate"]);
                    if (reader["PSN_ChangeReason"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_ChangeReason = Convert.ToString(reader["PSN_ChangeReason"]);
                    if (reader["PSN_BeforENT_Name"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_BeforENT_Name = Convert.ToString(reader["PSN_BeforENT_Name"]);
                    if (reader["PSN_BeforENT_ServerID"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_BeforENT_ServerID = Convert.ToString(reader["PSN_BeforENT_ServerID"]);
                    if (reader["PSN_BeforPersonName"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_BeforPersonName = Convert.ToString(reader["PSN_BeforPersonName"]);
                    if (reader["PSN_InterprovincialChange"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_InterprovincialChange = Convert.ToString(reader["PSN_InterprovincialChange"]);
                    if (reader["PSN_ExpiryReasons"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_ExpiryReasons = Convert.ToString(reader["PSN_ExpiryReasons"]);
                    if (reader["PSN_ExpiryDate"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_ExpiryDate = Convert.ToDateTime(reader["PSN_ExpiryDate"]);
                    if (reader["PSN_RenewalProfession"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_RenewalProfession = Convert.ToString(reader["PSN_RenewalProfession"]);
                    if (reader["PSN_AddProfession"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_AddProfession = Convert.ToString(reader["PSN_AddProfession"]);
                    if (reader["PSN_CancelPerson"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_CancelPerson = Convert.ToString(reader["PSN_CancelPerson"]);
                    if (reader["PSN_CancelReason"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_CancelReason = Convert.ToString(reader["PSN_CancelReason"]);
                    if (reader["PSN_ReReasons"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_ReReasons = Convert.ToString(reader["PSN_ReReasons"]);
                    if (reader["PSN_ReContent"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_ReContent = Convert.ToString(reader["PSN_ReContent"]);
                    if (reader["PSN_CheckCode"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_CheckCode = Convert.ToString(reader["PSN_CheckCode"]);
                    if (reader["ENT_Province_Code"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.ENT_Province_Code = Convert.ToString(reader["ENT_Province_Code"]);
                    if (reader["PSN_Level"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.PSN_Level = Convert.ToString(reader["PSN_Level"]);
                    if (reader["CJR"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.CJR = Convert.ToString(reader["CJR"]);
                    if (reader["CJSJ"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
                    if (reader["XGR"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.XGR = Convert.ToString(reader["XGR"]);
                    if (reader["XGSJ"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.XGSJ = Convert.ToDateTime(reader["XGSJ"]);
                    if (reader["Valid"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.Valid = Convert.ToInt32(reader["Valid"]);
                    if (reader["Memo"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.Memo = Convert.ToString(reader["Memo"]);
                    if (reader["GteDate"] != DBNull.Value) _COC_TOW_Person_BaseInfo_HisMDL.GteDate = Convert.ToDateTime(reader["GteDate"]);
                }
                reader.Close();
                db.Close();
                return _COC_TOW_Person_BaseInfo_HisMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.COC_TOW_Person_BaseInfo_His", "*", filterWhereString, orderBy == "" ? " HisID" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.COC_TOW_Person_BaseInfo_His", filterWhereString);
        }
        
        #region 自定义方法
        /// <summary>
        /// 正式表往历史记录表写入数据
        /// </summary>
        /// <param name="_COC_TOW_Person_BaseInfoMDL">二级建造师人员基本信息对象</param>
        /// <returns></returns
        public static COC_TOW_Person_BaseInfo_HisMDL _COC_TOW_Person_BaseInfo_HisMDL(DbTransaction tran,COC_TOW_Person_BaseInfoMDL _COC_TOW_Person_BaseInfoMDL)
        {
            //为历史记录表写入一个对象
            COC_TOW_Person_BaseInfo_HisMDL _COC_TOW_Person_BaseInfo_HisMDL = new COC_TOW_Person_BaseInfo_HisMDL();
            _COC_TOW_Person_BaseInfo_HisMDL.HisID = Guid.NewGuid().ToString();
            _COC_TOW_Person_BaseInfo_HisMDL.PSN_ServerID = _COC_TOW_Person_BaseInfoMDL.PSN_ServerID;
            _COC_TOW_Person_BaseInfo_HisMDL.ENT_ServerID = _COC_TOW_Person_BaseInfoMDL.ENT_ServerID;
            _COC_TOW_Person_BaseInfo_HisMDL.ENT_Name = _COC_TOW_Person_BaseInfoMDL.ENT_Name;
            _COC_TOW_Person_BaseInfo_HisMDL.ENT_OrganizationsCode = _COC_TOW_Person_BaseInfoMDL.ENT_OrganizationsCode;
            _COC_TOW_Person_BaseInfo_HisMDL.ENT_City = _COC_TOW_Person_BaseInfoMDL.ENT_City;
            _COC_TOW_Person_BaseInfo_HisMDL.BeginTime = _COC_TOW_Person_BaseInfoMDL.BeginTime;
            _COC_TOW_Person_BaseInfo_HisMDL.EndTime = _COC_TOW_Person_BaseInfoMDL.EndTime;
            _COC_TOW_Person_BaseInfo_HisMDL.PSN_Name = _COC_TOW_Person_BaseInfoMDL.PSN_Name;
            _COC_TOW_Person_BaseInfo_HisMDL.PSN_Sex = _COC_TOW_Person_BaseInfoMDL.PSN_Sex;
            _COC_TOW_Person_BaseInfo_HisMDL.PSN_BirthDate = _COC_TOW_Person_BaseInfoMDL.PSN_BirthDate;
            _COC_TOW_Person_BaseInfo_HisMDL.PSN_National = _COC_TOW_Person_BaseInfoMDL.PSN_National;
            _COC_TOW_Person_BaseInfo_HisMDL.PSN_CertificateType = _COC_TOW_Person_BaseInfoMDL.PSN_CertificateType;
            _COC_TOW_Person_BaseInfo_HisMDL.PSN_CertificateNO = _COC_TOW_Person_BaseInfoMDL.PSN_CertificateNO;
            _COC_TOW_Person_BaseInfo_HisMDL.PSN_GraduationSchool = _COC_TOW_Person_BaseInfoMDL.PSN_GraduationSchool;
            _COC_TOW_Person_BaseInfo_HisMDL.PSN_Specialty = _COC_TOW_Person_BaseInfoMDL.PSN_Specialty;
            _COC_TOW_Person_BaseInfo_HisMDL.PSN_GraduationTime = _COC_TOW_Person_BaseInfoMDL.PSN_GraduationTime;
            _COC_TOW_Person_BaseInfo_HisMDL.PSN_Qualification = _COC_TOW_Person_BaseInfoMDL.PSN_Qualification;
            _COC_TOW_Person_BaseInfo_HisMDL.PSN_Degree = _COC_TOW_Person_BaseInfoMDL.PSN_Degree;
            _COC_TOW_Person_BaseInfo_HisMDL.PSN_MobilePhone = _COC_TOW_Person_BaseInfoMDL.PSN_MobilePhone;
            _COC_TOW_Person_BaseInfo_HisMDL.PSN_Telephone = _COC_TOW_Person_BaseInfoMDL.PSN_Telephone;
            _COC_TOW_Person_BaseInfo_HisMDL.PSN_Email = _COC_TOW_Person_BaseInfoMDL.PSN_Email;
            _COC_TOW_Person_BaseInfo_HisMDL.PSN_PMGrade = _COC_TOW_Person_BaseInfoMDL.PSN_PMGrade;
            _COC_TOW_Person_BaseInfo_HisMDL.PSN_PMCertificateNo = _COC_TOW_Person_BaseInfoMDL.PSN_PMCertificateNo;
            _COC_TOW_Person_BaseInfo_HisMDL.PSN_RegisteType = _COC_TOW_Person_BaseInfoMDL.PSN_RegisteType;
            _COC_TOW_Person_BaseInfo_HisMDL.PSN_RegisterNO = _COC_TOW_Person_BaseInfoMDL.PSN_RegisterNO;
            _COC_TOW_Person_BaseInfo_HisMDL.PSN_RegisterCertificateNo = _COC_TOW_Person_BaseInfoMDL.PSN_RegisterCertificateNo;
            _COC_TOW_Person_BaseInfo_HisMDL.PSN_RegisteProfession = _COC_TOW_Person_BaseInfoMDL.PSN_RegisteProfession;
            _COC_TOW_Person_BaseInfo_HisMDL.PSN_CertificationDate = _COC_TOW_Person_BaseInfoMDL.PSN_CertificationDate;
            _COC_TOW_Person_BaseInfo_HisMDL.PSN_CertificateValidity = _COC_TOW_Person_BaseInfoMDL.PSN_CertificateValidity;
            _COC_TOW_Person_BaseInfo_HisMDL.PSN_RegistePermissionDate = _COC_TOW_Person_BaseInfoMDL.PSN_RegistePermissionDate;
            _COC_TOW_Person_BaseInfo_HisMDL.PSN_ChangeReason = _COC_TOW_Person_BaseInfoMDL.PSN_ChangeReason;
            _COC_TOW_Person_BaseInfo_HisMDL.PSN_BeforENT_Name = _COC_TOW_Person_BaseInfoMDL.PSN_BeforENT_Name;
            _COC_TOW_Person_BaseInfo_HisMDL.PSN_BeforENT_ServerID = _COC_TOW_Person_BaseInfoMDL.PSN_BeforENT_ServerID;
            _COC_TOW_Person_BaseInfo_HisMDL.PSN_InterprovincialChange = _COC_TOW_Person_BaseInfoMDL.PSN_InterprovincialChange;
            _COC_TOW_Person_BaseInfo_HisMDL.PSN_ExpiryReasons = _COC_TOW_Person_BaseInfoMDL.PSN_ExpiryReasons;
            _COC_TOW_Person_BaseInfo_HisMDL.PSN_ExpiryDate = _COC_TOW_Person_BaseInfoMDL.PSN_ExpiryDate;
            _COC_TOW_Person_BaseInfo_HisMDL.PSN_RenewalProfession = _COC_TOW_Person_BaseInfoMDL.PSN_RenewalProfession;
            _COC_TOW_Person_BaseInfo_HisMDL.PSN_AddProfession = _COC_TOW_Person_BaseInfoMDL.PSN_AddProfession;
            _COC_TOW_Person_BaseInfo_HisMDL.PSN_CancelPerson = _COC_TOW_Person_BaseInfoMDL.PSN_CancelPerson;
            _COC_TOW_Person_BaseInfo_HisMDL.PSN_CancelReason = _COC_TOW_Person_BaseInfoMDL.PSN_CancelReason;
            _COC_TOW_Person_BaseInfo_HisMDL.PSN_ReReasons = _COC_TOW_Person_BaseInfoMDL.PSN_ReReasons;
            _COC_TOW_Person_BaseInfo_HisMDL.PSN_ReContent = _COC_TOW_Person_BaseInfoMDL.PSN_ReContent;
            _COC_TOW_Person_BaseInfo_HisMDL.PSN_CheckCode = _COC_TOW_Person_BaseInfoMDL.PSN_CheckCode;
            _COC_TOW_Person_BaseInfo_HisMDL.ENT_Province_Code = _COC_TOW_Person_BaseInfoMDL.ENT_Province_Code;
            _COC_TOW_Person_BaseInfo_HisMDL.PSN_Level = _COC_TOW_Person_BaseInfoMDL.PSN_Level;
            _COC_TOW_Person_BaseInfo_HisMDL.CJR = _COC_TOW_Person_BaseInfoMDL.CJR;
            _COC_TOW_Person_BaseInfo_HisMDL.CJSJ = _COC_TOW_Person_BaseInfoMDL.CJSJ;
            _COC_TOW_Person_BaseInfo_HisMDL.XGR = _COC_TOW_Person_BaseInfoMDL.XGR;
            _COC_TOW_Person_BaseInfo_HisMDL.XGSJ = _COC_TOW_Person_BaseInfoMDL.XGSJ;
            _COC_TOW_Person_BaseInfo_HisMDL.Valid = _COC_TOW_Person_BaseInfoMDL.Valid;
            _COC_TOW_Person_BaseInfo_HisMDL.Memo = _COC_TOW_Person_BaseInfoMDL.Memo;
            _COC_TOW_Person_BaseInfo_HisMDL.GteDate = DateTime.Now;
            return _COC_TOW_Person_BaseInfo_HisMDL;
          
        }
        #endregion
    }
}
