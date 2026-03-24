using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
    /// <summary>
    /// 业务类实现--COC_TOW_Person_BaseInfoDAL(填写类描述)
    /// </summary>
    public class COC_TOW_Person_BaseInfoDAL
    {
        public COC_TOW_Person_BaseInfoDAL() { }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="COC_TOW_Person_BaseInfoMDL">对象实体类</param>
        /// <returns></returns>
        public static int Insert(COC_TOW_Person_BaseInfoMDL _COC_TOW_Person_BaseInfoMDL)
        {
            return Insert(null, _COC_TOW_Person_BaseInfoMDL);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="COC_TOW_Person_BaseInfoMDL">对象实体类</param>
        /// <returns></returns>
        public static int Insert(DbTransaction tran, COC_TOW_Person_BaseInfoMDL _COC_TOW_Person_BaseInfoMDL)
        {
            DBHelper db = new DBHelper();
            string sql = @"
			INSERT INTO dbo.COC_TOW_Person_BaseInfo(PSN_ServerID,ENT_ServerID,ENT_Name,ENT_OrganizationsCode,ENT_City,BeginTime,EndTime,PSN_Name,PSN_Sex,PSN_BirthDate,PSN_National,PSN_CertificateType,PSN_CertificateNO,PSN_GraduationSchool,PSN_Specialty,PSN_GraduationTime,PSN_Qualification,PSN_Degree,PSN_MobilePhone,PSN_Telephone,PSN_Email,PSN_PMGrade,PSN_PMCertificateNo,PSN_RegisteType,PSN_RegisterNO,PSN_RegisterCertificateNo,PSN_RegisteProfession,PSN_CertificationDate,PSN_CertificateValidity,PSN_RegistePermissionDate,PSN_ChangeReason,PSN_BeforENT_Name,PSN_BeforENT_ServerID,PSN_BeforPersonName,PSN_InterprovincialChange,PSN_ExpiryReasons,PSN_ExpiryDate,PSN_RenewalProfession,PSN_AddProfession,PSN_CancelPerson,PSN_CancelReason,PSN_ReReasons,PSN_ReContent,PSN_CheckCode,ENT_Province_Code,PSN_Level,ZGZSBH,CJR,CJSJ,XGR,XGSJ,[VALID],Memo)
			VALUES (@PSN_ServerID,@ENT_ServerID,@ENT_Name,@ENT_OrganizationsCode,@ENT_City,@BeginTime,@EndTime,@PSN_Name,@PSN_Sex,@PSN_BirthDate,@PSN_National,@PSN_CertificateType,@PSN_CertificateNO,@PSN_GraduationSchool,@PSN_Specialty,@PSN_GraduationTime,@PSN_Qualification,@PSN_Degree,@PSN_MobilePhone,@PSN_Telephone,@PSN_Email,@PSN_PMGrade,@PSN_PMCertificateNo,@PSN_RegisteType,@PSN_RegisterNO,@PSN_RegisterCertificateNo,@PSN_RegisteProfession,@PSN_CertificationDate,@PSN_CertificateValidity,@PSN_RegistePermissionDate,@PSN_ChangeReason,@PSN_BeforENT_Name,@PSN_BeforENT_ServerID,@PSN_BeforPersonName,@PSN_InterprovincialChange,@PSN_ExpiryReasons,@PSN_ExpiryDate,@PSN_RenewalProfession,@PSN_AddProfession,@PSN_CancelPerson,@PSN_CancelReason,@PSN_ReReasons,@PSN_ReContent,@PSN_CheckCode,@ENT_Province_Code,@PSN_Level,@ZGZSBH,@CJR,@CJSJ,@XGR,@XGSJ,@Valid,@Memo)";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PSN_ServerID", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_ServerID));
            p.Add(db.CreateParameter("ENT_ServerID", DbType.String, _COC_TOW_Person_BaseInfoMDL.ENT_ServerID));
            p.Add(db.CreateParameter("ENT_Name", DbType.String, _COC_TOW_Person_BaseInfoMDL.ENT_Name));
            p.Add(db.CreateParameter("ENT_OrganizationsCode", DbType.String, _COC_TOW_Person_BaseInfoMDL.ENT_OrganizationsCode));
            p.Add(db.CreateParameter("ENT_City", DbType.String, _COC_TOW_Person_BaseInfoMDL.ENT_City));
            p.Add(db.CreateParameter("BeginTime", DbType.DateTime, _COC_TOW_Person_BaseInfoMDL.BeginTime));
            p.Add(db.CreateParameter("EndTime", DbType.DateTime, _COC_TOW_Person_BaseInfoMDL.EndTime));
            p.Add(db.CreateParameter("PSN_Name", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_Name));
            p.Add(db.CreateParameter("PSN_Sex", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_Sex));
            p.Add(db.CreateParameter("PSN_BirthDate", DbType.DateTime, _COC_TOW_Person_BaseInfoMDL.PSN_BirthDate));
            p.Add(db.CreateParameter("PSN_National", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_National));
            p.Add(db.CreateParameter("PSN_CertificateType", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_CertificateType));
            p.Add(db.CreateParameter("PSN_CertificateNO", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_CertificateNO));
            p.Add(db.CreateParameter("PSN_GraduationSchool", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_GraduationSchool));
            p.Add(db.CreateParameter("PSN_Specialty", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_Specialty));
            p.Add(db.CreateParameter("PSN_GraduationTime", DbType.DateTime, _COC_TOW_Person_BaseInfoMDL.PSN_GraduationTime));
            p.Add(db.CreateParameter("PSN_Qualification", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_Qualification));
            p.Add(db.CreateParameter("PSN_Degree", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_Degree));
            p.Add(db.CreateParameter("PSN_MobilePhone", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_MobilePhone));
            p.Add(db.CreateParameter("PSN_Telephone", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_Telephone));
            p.Add(db.CreateParameter("PSN_Email", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_Email));
            p.Add(db.CreateParameter("PSN_PMGrade", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_PMGrade));
            p.Add(db.CreateParameter("PSN_PMCertificateNo", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_PMCertificateNo));
            p.Add(db.CreateParameter("PSN_RegisteType", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_RegisteType));
            p.Add(db.CreateParameter("PSN_RegisterNO", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_RegisterNO));
            p.Add(db.CreateParameter("PSN_RegisterCertificateNo", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_RegisterCertificateNo));
            p.Add(db.CreateParameter("PSN_RegisteProfession", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_RegisteProfession));
            p.Add(db.CreateParameter("PSN_CertificationDate", DbType.DateTime, _COC_TOW_Person_BaseInfoMDL.PSN_CertificationDate));
            p.Add(db.CreateParameter("PSN_CertificateValidity", DbType.DateTime, _COC_TOW_Person_BaseInfoMDL.PSN_CertificateValidity));
            p.Add(db.CreateParameter("PSN_RegistePermissionDate", DbType.DateTime, _COC_TOW_Person_BaseInfoMDL.PSN_RegistePermissionDate));
            p.Add(db.CreateParameter("PSN_ChangeReason", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_ChangeReason));
            p.Add(db.CreateParameter("PSN_BeforENT_Name", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_BeforENT_Name));
            p.Add(db.CreateParameter("PSN_BeforENT_ServerID", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_BeforENT_ServerID));
            p.Add(db.CreateParameter("PSN_BeforPersonName", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_BeforPersonName));
            p.Add(db.CreateParameter("PSN_InterprovincialChange", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_InterprovincialChange));
            p.Add(db.CreateParameter("PSN_ExpiryReasons", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_ExpiryReasons));
            p.Add(db.CreateParameter("PSN_ExpiryDate", DbType.DateTime, _COC_TOW_Person_BaseInfoMDL.PSN_ExpiryDate));
            p.Add(db.CreateParameter("PSN_RenewalProfession", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_RenewalProfession));
            p.Add(db.CreateParameter("PSN_AddProfession", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_AddProfession));
            p.Add(db.CreateParameter("PSN_CancelPerson", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_CancelPerson));
            p.Add(db.CreateParameter("PSN_CancelReason", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_CancelReason));
            p.Add(db.CreateParameter("PSN_ReReasons", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_ReReasons));
            p.Add(db.CreateParameter("PSN_ReContent", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_ReContent));
            p.Add(db.CreateParameter("PSN_CheckCode", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_CheckCode));
            p.Add(db.CreateParameter("ENT_Province_Code", DbType.String, _COC_TOW_Person_BaseInfoMDL.ENT_Province_Code));
            p.Add(db.CreateParameter("PSN_Level", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_Level));
            p.Add(db.CreateParameter("ZGZSBH", DbType.String, _COC_TOW_Person_BaseInfoMDL.ZGZSBH));
            p.Add(db.CreateParameter("CJR", DbType.String, _COC_TOW_Person_BaseInfoMDL.CJR));
            p.Add(db.CreateParameter("CJSJ", DbType.DateTime, _COC_TOW_Person_BaseInfoMDL.CJSJ));
            p.Add(db.CreateParameter("XGR", DbType.String, _COC_TOW_Person_BaseInfoMDL.XGR));
            p.Add(db.CreateParameter("XGSJ", DbType.DateTime, _COC_TOW_Person_BaseInfoMDL.XGSJ));
            p.Add(db.CreateParameter("Valid", DbType.Int32, _COC_TOW_Person_BaseInfoMDL.Valid));
            p.Add(db.CreateParameter("Memo", DbType.String, _COC_TOW_Person_BaseInfoMDL.Memo));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="COC_TOW_Person_BaseInfoMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(COC_TOW_Person_BaseInfoMDL _COC_TOW_Person_BaseInfoMDL)
        {
            return Update(null, _COC_TOW_Person_BaseInfoMDL);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="COC_TOW_Person_BaseInfoMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, COC_TOW_Person_BaseInfoMDL _COC_TOW_Person_BaseInfoMDL)
        {
            string sql = @"
			UPDATE dbo.COC_TOW_Person_BaseInfo
				SET	ENT_ServerID = @ENT_ServerID,ENT_Name = @ENT_Name,ENT_OrganizationsCode = @ENT_OrganizationsCode,ENT_City = @ENT_City,BeginTime = @BeginTime,EndTime = @EndTime,PSN_Name = @PSN_Name,PSN_Sex = @PSN_Sex,PSN_BirthDate = @PSN_BirthDate,PSN_National = @PSN_National,PSN_CertificateType = @PSN_CertificateType,PSN_CertificateNO = @PSN_CertificateNO,PSN_GraduationSchool = @PSN_GraduationSchool,PSN_Specialty = @PSN_Specialty,PSN_GraduationTime = @PSN_GraduationTime,PSN_Qualification = @PSN_Qualification,PSN_Degree = @PSN_Degree,PSN_MobilePhone = @PSN_MobilePhone,PSN_Telephone = @PSN_Telephone,PSN_Email = @PSN_Email,PSN_PMGrade = @PSN_PMGrade,PSN_PMCertificateNo = @PSN_PMCertificateNo,PSN_RegisteType = @PSN_RegisteType,PSN_RegisterNO = @PSN_RegisterNO,PSN_RegisterCertificateNo = @PSN_RegisterCertificateNo,PSN_RegisteProfession = @PSN_RegisteProfession,PSN_CertificationDate = @PSN_CertificationDate,PSN_CertificateValidity = @PSN_CertificateValidity,PSN_RegistePermissionDate = @PSN_RegistePermissionDate,PSN_ChangeReason = @PSN_ChangeReason,PSN_BeforENT_Name = @PSN_BeforENT_Name,PSN_BeforENT_ServerID = @PSN_BeforENT_ServerID,PSN_BeforPersonName = @PSN_BeforPersonName,PSN_InterprovincialChange = @PSN_InterprovincialChange,PSN_ExpiryReasons = @PSN_ExpiryReasons,PSN_ExpiryDate = @PSN_ExpiryDate,PSN_RenewalProfession = @PSN_RenewalProfession,PSN_AddProfession = @PSN_AddProfession,PSN_CancelPerson = @PSN_CancelPerson,PSN_CancelReason = @PSN_CancelReason,PSN_ReReasons = @PSN_ReReasons,PSN_ReContent = @PSN_ReContent,PSN_CheckCode = @PSN_CheckCode,ENT_Province_Code = @ENT_Province_Code,PSN_Level = @PSN_Level,ZGZSBH = @ZGZSBH,CJR = @CJR,CJSJ = @CJSJ,XGR = @XGR,XGSJ = @XGSJ,""VALID"" = @Valid,Memo = @Memo
			WHERE
				PSN_ServerID = @PSN_ServerID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PSN_ServerID", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_ServerID));
            p.Add(db.CreateParameter("ENT_ServerID", DbType.String, _COC_TOW_Person_BaseInfoMDL.ENT_ServerID));
            p.Add(db.CreateParameter("ENT_Name", DbType.String, _COC_TOW_Person_BaseInfoMDL.ENT_Name));
            p.Add(db.CreateParameter("ENT_OrganizationsCode", DbType.String, _COC_TOW_Person_BaseInfoMDL.ENT_OrganizationsCode));
            p.Add(db.CreateParameter("ENT_City", DbType.String, _COC_TOW_Person_BaseInfoMDL.ENT_City));
            p.Add(db.CreateParameter("BeginTime", DbType.DateTime, _COC_TOW_Person_BaseInfoMDL.BeginTime));
            p.Add(db.CreateParameter("EndTime", DbType.DateTime, _COC_TOW_Person_BaseInfoMDL.EndTime));
            p.Add(db.CreateParameter("PSN_Name", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_Name));
            p.Add(db.CreateParameter("PSN_Sex", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_Sex));
            p.Add(db.CreateParameter("PSN_BirthDate", DbType.DateTime, _COC_TOW_Person_BaseInfoMDL.PSN_BirthDate));
            p.Add(db.CreateParameter("PSN_National", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_National));
            p.Add(db.CreateParameter("PSN_CertificateType", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_CertificateType));
            p.Add(db.CreateParameter("PSN_CertificateNO", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_CertificateNO));
            p.Add(db.CreateParameter("PSN_GraduationSchool", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_GraduationSchool));
            p.Add(db.CreateParameter("PSN_Specialty", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_Specialty));
            p.Add(db.CreateParameter("PSN_GraduationTime", DbType.DateTime, _COC_TOW_Person_BaseInfoMDL.PSN_GraduationTime));
            p.Add(db.CreateParameter("PSN_Qualification", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_Qualification));
            p.Add(db.CreateParameter("PSN_Degree", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_Degree));
            p.Add(db.CreateParameter("PSN_MobilePhone", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_MobilePhone));
            p.Add(db.CreateParameter("PSN_Telephone", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_Telephone));
            p.Add(db.CreateParameter("PSN_Email", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_Email));
            p.Add(db.CreateParameter("PSN_PMGrade", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_PMGrade));
            p.Add(db.CreateParameter("PSN_PMCertificateNo", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_PMCertificateNo));
            p.Add(db.CreateParameter("PSN_RegisteType", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_RegisteType));
            p.Add(db.CreateParameter("PSN_RegisterNO", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_RegisterNO));
            p.Add(db.CreateParameter("PSN_RegisterCertificateNo", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_RegisterCertificateNo));
            p.Add(db.CreateParameter("PSN_RegisteProfession", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_RegisteProfession));
            p.Add(db.CreateParameter("PSN_CertificationDate", DbType.DateTime, _COC_TOW_Person_BaseInfoMDL.PSN_CertificationDate));
            p.Add(db.CreateParameter("PSN_CertificateValidity", DbType.DateTime, _COC_TOW_Person_BaseInfoMDL.PSN_CertificateValidity));
            p.Add(db.CreateParameter("PSN_RegistePermissionDate", DbType.DateTime, _COC_TOW_Person_BaseInfoMDL.PSN_RegistePermissionDate));
            p.Add(db.CreateParameter("PSN_ChangeReason", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_ChangeReason));
            p.Add(db.CreateParameter("PSN_BeforENT_Name", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_BeforENT_Name));
            p.Add(db.CreateParameter("PSN_BeforENT_ServerID", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_BeforENT_ServerID));
            p.Add(db.CreateParameter("PSN_BeforPersonName", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_BeforPersonName));
            p.Add(db.CreateParameter("PSN_InterprovincialChange", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_InterprovincialChange));
            p.Add(db.CreateParameter("PSN_ExpiryReasons", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_ExpiryReasons));
            p.Add(db.CreateParameter("PSN_ExpiryDate", DbType.DateTime, _COC_TOW_Person_BaseInfoMDL.PSN_ExpiryDate));
            p.Add(db.CreateParameter("PSN_RenewalProfession", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_RenewalProfession));
            p.Add(db.CreateParameter("PSN_AddProfession", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_AddProfession));
            p.Add(db.CreateParameter("PSN_CancelPerson", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_CancelPerson));
            p.Add(db.CreateParameter("PSN_CancelReason", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_CancelReason));
            p.Add(db.CreateParameter("PSN_ReReasons", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_ReReasons));
            p.Add(db.CreateParameter("PSN_ReContent", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_ReContent));
            p.Add(db.CreateParameter("PSN_CheckCode", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_CheckCode));
            p.Add(db.CreateParameter("ENT_Province_Code", DbType.String, _COC_TOW_Person_BaseInfoMDL.ENT_Province_Code));
            p.Add(db.CreateParameter("PSN_Level", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_Level));
            p.Add(db.CreateParameter("ZGZSBH", DbType.String, _COC_TOW_Person_BaseInfoMDL.ZGZSBH));
            p.Add(db.CreateParameter("CJR", DbType.String, _COC_TOW_Person_BaseInfoMDL.CJR));
            p.Add(db.CreateParameter("CJSJ", DbType.DateTime, _COC_TOW_Person_BaseInfoMDL.CJSJ));
            p.Add(db.CreateParameter("XGR", DbType.String, _COC_TOW_Person_BaseInfoMDL.XGR));
            p.Add(db.CreateParameter("XGSJ", DbType.DateTime, _COC_TOW_Person_BaseInfoMDL.XGSJ));
            p.Add(db.CreateParameter("Valid", DbType.Int32, _COC_TOW_Person_BaseInfoMDL.Valid));
            p.Add(db.CreateParameter("Memo", DbType.String, _COC_TOW_Person_BaseInfoMDL.Memo));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="COC_TOW_Person_BaseInfoID">主键</param>
        /// <returns></returns>
        public static int Delete(string PSN_ServerID)
        {
            return Delete(null, PSN_ServerID);
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="COC_TOW_Person_BaseInfoID">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, string PSN_ServerID)
        {
            string sql = @"DELETE FROM dbo.COC_TOW_Person_BaseInfo WHERE PSN_ServerID = @PSN_ServerID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PSN_ServerID", DbType.String, PSN_ServerID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="COC_TOW_Person_BaseInfoMDL">对象实体类</param>
        /// <returns></returns>
        public static int Delete(COC_TOW_Person_BaseInfoMDL _COC_TOW_Person_BaseInfoMDL)
        {
            return Delete(null, _COC_TOW_Person_BaseInfoMDL);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="COC_TOW_Person_BaseInfoMDL">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, COC_TOW_Person_BaseInfoMDL _COC_TOW_Person_BaseInfoMDL)
        {
            string sql = @"DELETE FROM dbo.COC_TOW_Person_BaseInfo WHERE PSN_ServerID = @PSN_ServerID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PSN_ServerID", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_ServerID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键取证书信息
        /// </summary>
        /// <param name="COC_TOW_Person_BaseInfoID">主键</param>
        public static COC_TOW_Person_BaseInfoMDL GetObject(string PSN_ServerID)
        {
            string sql = @"
			SELECT PSN_ServerID,ENT_ServerID,ENT_Name,ENT_OrganizationsCode,ENT_City,BeginTime,EndTime,PSN_Name,PSN_Sex,PSN_BirthDate,PSN_National,PSN_CertificateType,PSN_CertificateNO,PSN_GraduationSchool,PSN_Specialty,PSN_GraduationTime,PSN_Qualification,PSN_Degree,PSN_MobilePhone,PSN_Telephone,PSN_Email,PSN_PMGrade,PSN_PMCertificateNo,PSN_RegisteType,PSN_RegisterNO,PSN_RegisterCertificateNo,PSN_RegisteProfession,PSN_CertificationDate,PSN_CertificateValidity,PSN_RegistePermissionDate,PSN_ChangeReason,PSN_BeforENT_Name,PSN_BeforENT_ServerID,PSN_BeforPersonName,PSN_InterprovincialChange,PSN_ExpiryReasons,PSN_ExpiryDate,PSN_RenewalProfession,PSN_AddProfession,PSN_CancelPerson,PSN_CancelReason,PSN_ReReasons,PSN_ReContent,PSN_CheckCode,ENT_Province_Code,PSN_Level,ZGZSBH,CJR,CJSJ,XGR,XGSJ,[VALID],Memo,[ApplyCATime],[ReturnCATime],[CertificateCAID]
			FROM dbo.COC_TOW_Person_BaseInfo
			WHERE PSN_ServerID = @PSN_ServerID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PSN_ServerID", DbType.String, PSN_ServerID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                COC_TOW_Person_BaseInfoMDL _COC_TOW_Person_BaseInfoMDL = null;
                if (reader.Read())
                {
                    _COC_TOW_Person_BaseInfoMDL = new COC_TOW_Person_BaseInfoMDL();
                    if (reader["PSN_ServerID"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ServerID = Convert.ToString(reader["PSN_ServerID"]);
                    if (reader["ENT_ServerID"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_ServerID = Convert.ToString(reader["ENT_ServerID"]);
                    if (reader["ENT_Name"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_Name = Convert.ToString(reader["ENT_Name"]);
                    if (reader["ENT_OrganizationsCode"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_OrganizationsCode = Convert.ToString(reader["ENT_OrganizationsCode"]);
                    if (reader["ENT_City"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_City = Convert.ToString(reader["ENT_City"]);
                    if (reader["BeginTime"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.BeginTime = Convert.ToDateTime(reader["BeginTime"]);
                    if (reader["EndTime"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.EndTime = Convert.ToDateTime(reader["EndTime"]);
                    if (reader["PSN_Name"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Name = Convert.ToString(reader["PSN_Name"]);
                    if (reader["PSN_Sex"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Sex = Convert.ToString(reader["PSN_Sex"]);
                    if (reader["PSN_BirthDate"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_BirthDate = Convert.ToDateTime(reader["PSN_BirthDate"]);
                    if (reader["PSN_National"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_National = Convert.ToString(reader["PSN_National"]);
                    if (reader["PSN_CertificateType"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CertificateType = Convert.ToString(reader["PSN_CertificateType"]);
                    if (reader["PSN_CertificateNO"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CertificateNO = Convert.ToString(reader["PSN_CertificateNO"]);
                    if (reader["PSN_GraduationSchool"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_GraduationSchool = Convert.ToString(reader["PSN_GraduationSchool"]);
                    if (reader["PSN_Specialty"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Specialty = Convert.ToString(reader["PSN_Specialty"]);
                    if (reader["PSN_GraduationTime"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_GraduationTime = Convert.ToDateTime(reader["PSN_GraduationTime"]);
                    if (reader["PSN_Qualification"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Qualification = Convert.ToString(reader["PSN_Qualification"]);
                    if (reader["PSN_Degree"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Degree = Convert.ToString(reader["PSN_Degree"]);
                    if (reader["PSN_MobilePhone"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_MobilePhone = Convert.ToString(reader["PSN_MobilePhone"]);
                    if (reader["PSN_Telephone"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Telephone = Convert.ToString(reader["PSN_Telephone"]);
                    if (reader["PSN_Email"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Email = Convert.ToString(reader["PSN_Email"]);
                    if (reader["PSN_PMGrade"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_PMGrade = Convert.ToString(reader["PSN_PMGrade"]);
                    if (reader["PSN_PMCertificateNo"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_PMCertificateNo = Convert.ToString(reader["PSN_PMCertificateNo"]);
                    if (reader["PSN_RegisteType"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegisteType = Convert.ToString(reader["PSN_RegisteType"]);
                    if (reader["PSN_RegisterNO"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegisterNO = Convert.ToString(reader["PSN_RegisterNO"]);
                    if (reader["PSN_RegisterCertificateNo"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegisterCertificateNo = Convert.ToString(reader["PSN_RegisterCertificateNo"]);
                    if (reader["PSN_RegisteProfession"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegisteProfession = Convert.ToString(reader["PSN_RegisteProfession"]);
                    if (reader["PSN_CertificationDate"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CertificationDate = Convert.ToDateTime(reader["PSN_CertificationDate"]);
                    if (reader["PSN_CertificateValidity"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CertificateValidity = Convert.ToDateTime(reader["PSN_CertificateValidity"]);
                    if (reader["PSN_RegistePermissionDate"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegistePermissionDate = Convert.ToDateTime(reader["PSN_RegistePermissionDate"]);
                    if (reader["PSN_ChangeReason"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ChangeReason = Convert.ToString(reader["PSN_ChangeReason"]);
                    if (reader["PSN_BeforENT_Name"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_BeforENT_Name = Convert.ToString(reader["PSN_BeforENT_Name"]);
                    if (reader["PSN_BeforENT_ServerID"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_BeforENT_ServerID = Convert.ToString(reader["PSN_BeforENT_ServerID"]);
                    if (reader["PSN_BeforPersonName"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_BeforPersonName = Convert.ToString(reader["PSN_BeforPersonName"]);
                    if (reader["PSN_InterprovincialChange"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_InterprovincialChange = Convert.ToString(reader["PSN_InterprovincialChange"]);
                    if (reader["PSN_ExpiryReasons"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ExpiryReasons = Convert.ToString(reader["PSN_ExpiryReasons"]);
                    if (reader["PSN_ExpiryDate"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ExpiryDate = Convert.ToDateTime(reader["PSN_ExpiryDate"]);
                    if (reader["PSN_RenewalProfession"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RenewalProfession = Convert.ToString(reader["PSN_RenewalProfession"]);
                    if (reader["PSN_AddProfession"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_AddProfession = Convert.ToString(reader["PSN_AddProfession"]);
                    if (reader["PSN_CancelPerson"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CancelPerson = Convert.ToString(reader["PSN_CancelPerson"]);
                    if (reader["PSN_CancelReason"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CancelReason = Convert.ToString(reader["PSN_CancelReason"]);
                    if (reader["PSN_ReReasons"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ReReasons = Convert.ToString(reader["PSN_ReReasons"]);
                    if (reader["PSN_ReContent"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ReContent = Convert.ToString(reader["PSN_ReContent"]);
                    if (reader["PSN_CheckCode"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CheckCode = Convert.ToString(reader["PSN_CheckCode"]);
                    if (reader["ENT_Province_Code"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_Province_Code = Convert.ToString(reader["ENT_Province_Code"]);
                    if (reader["PSN_Level"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Level = Convert.ToString(reader["PSN_Level"]);
                    if (reader["ZGZSBH"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ZGZSBH = Convert.ToString(reader["ZGZSBH"]);
                    if (reader["CJR"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.CJR = Convert.ToString(reader["CJR"]);
                    if (reader["CJSJ"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
                    if (reader["XGR"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.XGR = Convert.ToString(reader["XGR"]);
                    if (reader["XGSJ"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.XGSJ = Convert.ToDateTime(reader["XGSJ"]);
                    if (reader["Valid"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.Valid = Convert.ToInt32(reader["Valid"]);
                    if (reader["Memo"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.Memo = Convert.ToString(reader["Memo"]);
                    if (reader["ApplyCATime"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ApplyCATime = Convert.ToDateTime(reader["ApplyCATime"]);
                    if (reader["ReturnCATime"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ReturnCATime = Convert.ToDateTime(reader["ReturnCATime"]);
                    if (reader["CertificateCAID"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.CertificateCAID = Convert.ToString(reader["CertificateCAID"]);
                }
                reader.Close();
                db.Close();
                return _COC_TOW_Person_BaseInfoMDL;
            }
        }

        /// <summary>
        /// 根据证件号码获取证书信息
        /// </summary>
        /// <param name="PSN_CertificateNO">证件号码</param>
        public static COC_TOW_Person_BaseInfoMDL GetObjectByPSN_CertificateNO(string PSN_CertificateNO)
        {
            string sql = @"
			SELECT top 1 PSN_ServerID,ENT_ServerID,ENT_Name,ENT_OrganizationsCode,ENT_City,BeginTime,EndTime,PSN_Name,PSN_Sex,PSN_BirthDate,PSN_National,PSN_CertificateType,PSN_CertificateNO,PSN_GraduationSchool,PSN_Specialty,PSN_GraduationTime,PSN_Qualification,PSN_Degree,PSN_MobilePhone,PSN_Telephone,PSN_Email,PSN_PMGrade,PSN_PMCertificateNo,PSN_RegisteType,PSN_RegisterNO,PSN_RegisterCertificateNo,PSN_RegisteProfession,PSN_CertificationDate,PSN_CertificateValidity,PSN_RegistePermissionDate,PSN_ChangeReason,PSN_BeforENT_Name,PSN_BeforENT_ServerID,PSN_BeforPersonName,PSN_InterprovincialChange,PSN_ExpiryReasons,PSN_ExpiryDate,PSN_RenewalProfession,PSN_AddProfession,PSN_CancelPerson,PSN_CancelReason,PSN_ReReasons,PSN_ReContent,PSN_CheckCode,ENT_Province_Code,PSN_Level,ZGZSBH,CJR,CJSJ,XGR,XGSJ,[VALID],Memo
			FROM dbo.COC_TOW_Person_BaseInfo
			WHERE PSN_CertificateNO = @PSN_CertificateNO";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PSN_CertificateNO", DbType.String, PSN_CertificateNO));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                COC_TOW_Person_BaseInfoMDL _COC_TOW_Person_BaseInfoMDL = null;
                if (reader.Read())
                {
                    _COC_TOW_Person_BaseInfoMDL = new COC_TOW_Person_BaseInfoMDL();
                    if (reader["PSN_ServerID"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ServerID = Convert.ToString(reader["PSN_ServerID"]);
                    if (reader["ENT_ServerID"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_ServerID = Convert.ToString(reader["ENT_ServerID"]);
                    if (reader["ENT_Name"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_Name = Convert.ToString(reader["ENT_Name"]);
                    if (reader["ENT_OrganizationsCode"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_OrganizationsCode = Convert.ToString(reader["ENT_OrganizationsCode"]);
                    if (reader["ENT_City"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_City = Convert.ToString(reader["ENT_City"]);
                    if (reader["BeginTime"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.BeginTime = Convert.ToDateTime(reader["BeginTime"]);
                    if (reader["EndTime"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.EndTime = Convert.ToDateTime(reader["EndTime"]);
                    if (reader["PSN_Name"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Name = Convert.ToString(reader["PSN_Name"]);
                    if (reader["PSN_Sex"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Sex = Convert.ToString(reader["PSN_Sex"]);
                    if (reader["PSN_BirthDate"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_BirthDate = Convert.ToDateTime(reader["PSN_BirthDate"]);
                    if (reader["PSN_National"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_National = Convert.ToString(reader["PSN_National"]);
                    if (reader["PSN_CertificateType"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CertificateType = Convert.ToString(reader["PSN_CertificateType"]);
                    if (reader["PSN_CertificateNO"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CertificateNO = Convert.ToString(reader["PSN_CertificateNO"]);
                    if (reader["PSN_GraduationSchool"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_GraduationSchool = Convert.ToString(reader["PSN_GraduationSchool"]);
                    if (reader["PSN_Specialty"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Specialty = Convert.ToString(reader["PSN_Specialty"]);
                    if (reader["PSN_GraduationTime"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_GraduationTime = Convert.ToDateTime(reader["PSN_GraduationTime"]);
                    if (reader["PSN_Qualification"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Qualification = Convert.ToString(reader["PSN_Qualification"]);
                    if (reader["PSN_Degree"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Degree = Convert.ToString(reader["PSN_Degree"]);
                    if (reader["PSN_MobilePhone"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_MobilePhone = Convert.ToString(reader["PSN_MobilePhone"]);
                    if (reader["PSN_Telephone"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Telephone = Convert.ToString(reader["PSN_Telephone"]);
                    if (reader["PSN_Email"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Email = Convert.ToString(reader["PSN_Email"]);
                    if (reader["PSN_PMGrade"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_PMGrade = Convert.ToString(reader["PSN_PMGrade"]);
                    if (reader["PSN_PMCertificateNo"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_PMCertificateNo = Convert.ToString(reader["PSN_PMCertificateNo"]);
                    if (reader["PSN_RegisteType"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegisteType = Convert.ToString(reader["PSN_RegisteType"]);
                    if (reader["PSN_RegisterNO"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegisterNO = Convert.ToString(reader["PSN_RegisterNO"]);
                    if (reader["PSN_RegisterCertificateNo"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegisterCertificateNo = Convert.ToString(reader["PSN_RegisterCertificateNo"]);
                    if (reader["PSN_RegisteProfession"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegisteProfession = Convert.ToString(reader["PSN_RegisteProfession"]);
                    if (reader["PSN_CertificationDate"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CertificationDate = Convert.ToDateTime(reader["PSN_CertificationDate"]);
                    if (reader["PSN_CertificateValidity"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CertificateValidity = Convert.ToDateTime(reader["PSN_CertificateValidity"]);
                    if (reader["PSN_RegistePermissionDate"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegistePermissionDate = Convert.ToDateTime(reader["PSN_RegistePermissionDate"]);
                    if (reader["PSN_ChangeReason"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ChangeReason = Convert.ToString(reader["PSN_ChangeReason"]);
                    if (reader["PSN_BeforENT_Name"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_BeforENT_Name = Convert.ToString(reader["PSN_BeforENT_Name"]);
                    if (reader["PSN_BeforENT_ServerID"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_BeforENT_ServerID = Convert.ToString(reader["PSN_BeforENT_ServerID"]);
                    if (reader["PSN_BeforPersonName"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_BeforPersonName = Convert.ToString(reader["PSN_BeforPersonName"]);
                    if (reader["PSN_InterprovincialChange"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_InterprovincialChange = Convert.ToString(reader["PSN_InterprovincialChange"]);
                    if (reader["PSN_ExpiryReasons"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ExpiryReasons = Convert.ToString(reader["PSN_ExpiryReasons"]);
                    if (reader["PSN_ExpiryDate"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ExpiryDate = Convert.ToDateTime(reader["PSN_ExpiryDate"]);
                    if (reader["PSN_RenewalProfession"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RenewalProfession = Convert.ToString(reader["PSN_RenewalProfession"]);
                    if (reader["PSN_AddProfession"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_AddProfession = Convert.ToString(reader["PSN_AddProfession"]);
                    if (reader["PSN_CancelPerson"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CancelPerson = Convert.ToString(reader["PSN_CancelPerson"]);
                    if (reader["PSN_CancelReason"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CancelReason = Convert.ToString(reader["PSN_CancelReason"]);
                    if (reader["PSN_ReReasons"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ReReasons = Convert.ToString(reader["PSN_ReReasons"]);
                    if (reader["PSN_ReContent"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ReContent = Convert.ToString(reader["PSN_ReContent"]);
                    if (reader["PSN_CheckCode"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CheckCode = Convert.ToString(reader["PSN_CheckCode"]);
                    if (reader["ENT_Province_Code"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_Province_Code = Convert.ToString(reader["ENT_Province_Code"]);
                    if (reader["PSN_Level"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Level = Convert.ToString(reader["PSN_Level"]);
                    if (reader["ZGZSBH"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ZGZSBH = Convert.ToString(reader["ZGZSBH"]);
                    if (reader["CJR"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.CJR = Convert.ToString(reader["CJR"]);
                    if (reader["CJSJ"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
                    if (reader["XGR"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.XGR = Convert.ToString(reader["XGR"]);
                    if (reader["XGSJ"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.XGSJ = Convert.ToDateTime(reader["XGSJ"]);
                    if (reader["Valid"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.Valid = Convert.ToInt32(reader["Valid"]);
                    if (reader["Memo"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.Memo = Convert.ToString(reader["Memo"]);
                }
                reader.Close();
                db.Close();
                return _COC_TOW_Person_BaseInfoMDL;
            }
        }

        /// <summary>
        /// 根据注册号获取证书信息
        /// </summary>
        /// <param name="PSN_RegisterNO">注册号</param>
        public static COC_TOW_Person_BaseInfoMDL GetObjectByPSN_RegisterNO(string PSN_RegisterNO)
        {
            string sql = @"
			SELECT top 1 PSN_ServerID,ENT_ServerID,ENT_Name,ENT_OrganizationsCode,ENT_City,BeginTime,EndTime,PSN_Name,PSN_Sex,PSN_BirthDate,PSN_National,PSN_CertificateType,PSN_CertificateNO,PSN_GraduationSchool,PSN_Specialty,PSN_GraduationTime,PSN_Qualification,PSN_Degree,PSN_MobilePhone,PSN_Telephone,PSN_Email,PSN_PMGrade,PSN_PMCertificateNo,PSN_RegisteType,PSN_RegisterNO,PSN_RegisterCertificateNo,PSN_RegisteProfession,PSN_CertificationDate,PSN_CertificateValidity,PSN_RegistePermissionDate,PSN_ChangeReason,PSN_BeforENT_Name,PSN_BeforENT_ServerID,PSN_BeforPersonName,PSN_InterprovincialChange,PSN_ExpiryReasons,PSN_ExpiryDate,PSN_RenewalProfession,PSN_AddProfession,PSN_CancelPerson,PSN_CancelReason,PSN_ReReasons,PSN_ReContent,PSN_CheckCode,ENT_Province_Code,PSN_Level,ZGZSBH,CJR,CJSJ,XGR,XGSJ,[VALID],Memo
			FROM dbo.COC_TOW_Person_BaseInfo
			WHERE PSN_RegisterNO = @PSN_RegisterNO";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PSN_RegisterNO", DbType.String, PSN_RegisterNO));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                COC_TOW_Person_BaseInfoMDL _COC_TOW_Person_BaseInfoMDL = null;
                if (reader.Read())
                {
                    _COC_TOW_Person_BaseInfoMDL = new COC_TOW_Person_BaseInfoMDL();
                    if (reader["PSN_ServerID"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ServerID = Convert.ToString(reader["PSN_ServerID"]);
                    if (reader["ENT_ServerID"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_ServerID = Convert.ToString(reader["ENT_ServerID"]);
                    if (reader["ENT_Name"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_Name = Convert.ToString(reader["ENT_Name"]);
                    if (reader["ENT_OrganizationsCode"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_OrganizationsCode = Convert.ToString(reader["ENT_OrganizationsCode"]);
                    if (reader["ENT_City"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_City = Convert.ToString(reader["ENT_City"]);
                    if (reader["BeginTime"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.BeginTime = Convert.ToDateTime(reader["BeginTime"]);
                    if (reader["EndTime"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.EndTime = Convert.ToDateTime(reader["EndTime"]);
                    if (reader["PSN_Name"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Name = Convert.ToString(reader["PSN_Name"]);
                    if (reader["PSN_Sex"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Sex = Convert.ToString(reader["PSN_Sex"]);
                    if (reader["PSN_BirthDate"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_BirthDate = Convert.ToDateTime(reader["PSN_BirthDate"]);
                    if (reader["PSN_National"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_National = Convert.ToString(reader["PSN_National"]);
                    if (reader["PSN_CertificateType"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CertificateType = Convert.ToString(reader["PSN_CertificateType"]);
                    if (reader["PSN_CertificateNO"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CertificateNO = Convert.ToString(reader["PSN_CertificateNO"]);
                    if (reader["PSN_GraduationSchool"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_GraduationSchool = Convert.ToString(reader["PSN_GraduationSchool"]);
                    if (reader["PSN_Specialty"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Specialty = Convert.ToString(reader["PSN_Specialty"]);
                    if (reader["PSN_GraduationTime"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_GraduationTime = Convert.ToDateTime(reader["PSN_GraduationTime"]);
                    if (reader["PSN_Qualification"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Qualification = Convert.ToString(reader["PSN_Qualification"]);
                    if (reader["PSN_Degree"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Degree = Convert.ToString(reader["PSN_Degree"]);
                    if (reader["PSN_MobilePhone"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_MobilePhone = Convert.ToString(reader["PSN_MobilePhone"]);
                    if (reader["PSN_Telephone"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Telephone = Convert.ToString(reader["PSN_Telephone"]);
                    if (reader["PSN_Email"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Email = Convert.ToString(reader["PSN_Email"]);
                    if (reader["PSN_PMGrade"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_PMGrade = Convert.ToString(reader["PSN_PMGrade"]);
                    if (reader["PSN_PMCertificateNo"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_PMCertificateNo = Convert.ToString(reader["PSN_PMCertificateNo"]);
                    if (reader["PSN_RegisteType"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegisteType = Convert.ToString(reader["PSN_RegisteType"]);
                    if (reader["PSN_RegisterNO"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegisterNO = Convert.ToString(reader["PSN_RegisterNO"]);
                    if (reader["PSN_RegisterCertificateNo"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegisterCertificateNo = Convert.ToString(reader["PSN_RegisterCertificateNo"]);
                    if (reader["PSN_RegisteProfession"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegisteProfession = Convert.ToString(reader["PSN_RegisteProfession"]);
                    if (reader["PSN_CertificationDate"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CertificationDate = Convert.ToDateTime(reader["PSN_CertificationDate"]);
                    if (reader["PSN_CertificateValidity"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CertificateValidity = Convert.ToDateTime(reader["PSN_CertificateValidity"]);
                    if (reader["PSN_RegistePermissionDate"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegistePermissionDate = Convert.ToDateTime(reader["PSN_RegistePermissionDate"]);
                    if (reader["PSN_ChangeReason"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ChangeReason = Convert.ToString(reader["PSN_ChangeReason"]);
                    if (reader["PSN_BeforENT_Name"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_BeforENT_Name = Convert.ToString(reader["PSN_BeforENT_Name"]);
                    if (reader["PSN_BeforENT_ServerID"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_BeforENT_ServerID = Convert.ToString(reader["PSN_BeforENT_ServerID"]);
                    if (reader["PSN_BeforPersonName"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_BeforPersonName = Convert.ToString(reader["PSN_BeforPersonName"]);
                    if (reader["PSN_InterprovincialChange"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_InterprovincialChange = Convert.ToString(reader["PSN_InterprovincialChange"]);
                    if (reader["PSN_ExpiryReasons"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ExpiryReasons = Convert.ToString(reader["PSN_ExpiryReasons"]);
                    if (reader["PSN_ExpiryDate"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ExpiryDate = Convert.ToDateTime(reader["PSN_ExpiryDate"]);
                    if (reader["PSN_RenewalProfession"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RenewalProfession = Convert.ToString(reader["PSN_RenewalProfession"]);
                    if (reader["PSN_AddProfession"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_AddProfession = Convert.ToString(reader["PSN_AddProfession"]);
                    if (reader["PSN_CancelPerson"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CancelPerson = Convert.ToString(reader["PSN_CancelPerson"]);
                    if (reader["PSN_CancelReason"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CancelReason = Convert.ToString(reader["PSN_CancelReason"]);
                    if (reader["PSN_ReReasons"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ReReasons = Convert.ToString(reader["PSN_ReReasons"]);
                    if (reader["PSN_ReContent"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ReContent = Convert.ToString(reader["PSN_ReContent"]);
                    if (reader["PSN_CheckCode"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CheckCode = Convert.ToString(reader["PSN_CheckCode"]);
                    if (reader["ENT_Province_Code"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_Province_Code = Convert.ToString(reader["ENT_Province_Code"]);
                    if (reader["PSN_Level"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Level = Convert.ToString(reader["PSN_Level"]);
                    if (reader["ZGZSBH"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ZGZSBH = Convert.ToString(reader["ZGZSBH"]);
                    if (reader["CJR"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.CJR = Convert.ToString(reader["CJR"]);
                    if (reader["CJSJ"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
                    if (reader["XGR"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.XGR = Convert.ToString(reader["XGR"]);
                    if (reader["XGSJ"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.XGSJ = Convert.ToDateTime(reader["XGSJ"]);
                    if (reader["Valid"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.Valid = Convert.ToInt32(reader["Valid"]);
                    if (reader["Memo"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.Memo = Convert.ToString(reader["Memo"]);
                }
                reader.Close();
                db.Close();
                return _COC_TOW_Person_BaseInfoMDL;
            }
        }

        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="COC_TOW_Person_BaseInfoID">主键</param>
        public static COC_TOW_Person_BaseInfoMDL GetObject(DbTransaction tran, string PSN_ServerID)
        {
            string sql = @"
			SELECT PSN_ServerID,ENT_ServerID,ENT_Name,ENT_OrganizationsCode,ENT_City,BeginTime,EndTime,PSN_Name,PSN_Sex,PSN_BirthDate,PSN_National,PSN_CertificateType,PSN_CertificateNO,PSN_GraduationSchool,PSN_Specialty,PSN_GraduationTime,PSN_Qualification,PSN_Degree,PSN_MobilePhone,PSN_Telephone,PSN_Email,PSN_PMGrade,PSN_PMCertificateNo,PSN_RegisteType,PSN_RegisterNO,PSN_RegisterCertificateNo,PSN_RegisteProfession,PSN_CertificationDate,PSN_CertificateValidity,PSN_RegistePermissionDate,PSN_ChangeReason,PSN_BeforENT_Name,PSN_BeforENT_ServerID,PSN_BeforPersonName,PSN_InterprovincialChange,PSN_ExpiryReasons,PSN_ExpiryDate,PSN_RenewalProfession,PSN_AddProfession,PSN_CancelPerson,PSN_CancelReason,PSN_ReReasons,PSN_ReContent,PSN_CheckCode,ENT_Province_Code,PSN_Level,ZGZSBH,CJR,CJSJ,XGR,XGSJ,[VALID],Memo
			FROM dbo.COC_TOW_Person_BaseInfo
			WHERE PSN_ServerID = @PSN_ServerID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PSN_ServerID", DbType.String, PSN_ServerID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                COC_TOW_Person_BaseInfoMDL _COC_TOW_Person_BaseInfoMDL = null;
                if (reader.Read())
                {
                    _COC_TOW_Person_BaseInfoMDL = new COC_TOW_Person_BaseInfoMDL();
                    if (reader["PSN_ServerID"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ServerID = Convert.ToString(reader["PSN_ServerID"]);
                    if (reader["ENT_ServerID"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_ServerID = Convert.ToString(reader["ENT_ServerID"]);
                    if (reader["ENT_Name"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_Name = Convert.ToString(reader["ENT_Name"]);
                    if (reader["ENT_OrganizationsCode"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_OrganizationsCode = Convert.ToString(reader["ENT_OrganizationsCode"]);
                    if (reader["ENT_City"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_City = Convert.ToString(reader["ENT_City"]);
                    if (reader["BeginTime"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.BeginTime = Convert.ToDateTime(reader["BeginTime"]);
                    if (reader["EndTime"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.EndTime = Convert.ToDateTime(reader["EndTime"]);
                    if (reader["PSN_Name"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Name = Convert.ToString(reader["PSN_Name"]);
                    if (reader["PSN_Sex"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Sex = Convert.ToString(reader["PSN_Sex"]);
                    if (reader["PSN_BirthDate"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_BirthDate = Convert.ToDateTime(reader["PSN_BirthDate"]);
                    if (reader["PSN_National"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_National = Convert.ToString(reader["PSN_National"]);
                    if (reader["PSN_CertificateType"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CertificateType = Convert.ToString(reader["PSN_CertificateType"]);
                    if (reader["PSN_CertificateNO"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CertificateNO = Convert.ToString(reader["PSN_CertificateNO"]);
                    if (reader["PSN_GraduationSchool"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_GraduationSchool = Convert.ToString(reader["PSN_GraduationSchool"]);
                    if (reader["PSN_Specialty"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Specialty = Convert.ToString(reader["PSN_Specialty"]);
                    if (reader["PSN_GraduationTime"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_GraduationTime = Convert.ToDateTime(reader["PSN_GraduationTime"]);
                    if (reader["PSN_Qualification"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Qualification = Convert.ToString(reader["PSN_Qualification"]);
                    if (reader["PSN_Degree"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Degree = Convert.ToString(reader["PSN_Degree"]);
                    if (reader["PSN_MobilePhone"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_MobilePhone = Convert.ToString(reader["PSN_MobilePhone"]);
                    if (reader["PSN_Telephone"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Telephone = Convert.ToString(reader["PSN_Telephone"]);
                    if (reader["PSN_Email"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Email = Convert.ToString(reader["PSN_Email"]);
                    if (reader["PSN_PMGrade"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_PMGrade = Convert.ToString(reader["PSN_PMGrade"]);
                    if (reader["PSN_PMCertificateNo"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_PMCertificateNo = Convert.ToString(reader["PSN_PMCertificateNo"]);
                    if (reader["PSN_RegisteType"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegisteType = Convert.ToString(reader["PSN_RegisteType"]);
                    if (reader["PSN_RegisterNO"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegisterNO = Convert.ToString(reader["PSN_RegisterNO"]);
                    if (reader["PSN_RegisterCertificateNo"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegisterCertificateNo = Convert.ToString(reader["PSN_RegisterCertificateNo"]);
                    if (reader["PSN_RegisteProfession"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegisteProfession = Convert.ToString(reader["PSN_RegisteProfession"]);
                    if (reader["PSN_CertificationDate"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CertificationDate = Convert.ToDateTime(reader["PSN_CertificationDate"]);
                    if (reader["PSN_CertificateValidity"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CertificateValidity = Convert.ToDateTime(reader["PSN_CertificateValidity"]);
                    if (reader["PSN_RegistePermissionDate"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegistePermissionDate = Convert.ToDateTime(reader["PSN_RegistePermissionDate"]);
                    if (reader["PSN_ChangeReason"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ChangeReason = Convert.ToString(reader["PSN_ChangeReason"]);
                    if (reader["PSN_BeforENT_Name"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_BeforENT_Name = Convert.ToString(reader["PSN_BeforENT_Name"]);
                    if (reader["PSN_BeforENT_ServerID"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_BeforENT_ServerID = Convert.ToString(reader["PSN_BeforENT_ServerID"]);
                    if (reader["PSN_BeforPersonName"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_BeforPersonName = Convert.ToString(reader["PSN_BeforPersonName"]);
                    if (reader["PSN_InterprovincialChange"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_InterprovincialChange = Convert.ToString(reader["PSN_InterprovincialChange"]);
                    if (reader["PSN_ExpiryReasons"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ExpiryReasons = Convert.ToString(reader["PSN_ExpiryReasons"]);
                    if (reader["PSN_ExpiryDate"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ExpiryDate = Convert.ToDateTime(reader["PSN_ExpiryDate"]);
                    if (reader["PSN_RenewalProfession"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RenewalProfession = Convert.ToString(reader["PSN_RenewalProfession"]);
                    if (reader["PSN_AddProfession"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_AddProfession = Convert.ToString(reader["PSN_AddProfession"]);
                    if (reader["PSN_CancelPerson"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CancelPerson = Convert.ToString(reader["PSN_CancelPerson"]);
                    if (reader["PSN_CancelReason"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CancelReason = Convert.ToString(reader["PSN_CancelReason"]);
                    if (reader["PSN_ReReasons"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ReReasons = Convert.ToString(reader["PSN_ReReasons"]);
                    if (reader["PSN_ReContent"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ReContent = Convert.ToString(reader["PSN_ReContent"]);
                    if (reader["PSN_CheckCode"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CheckCode = Convert.ToString(reader["PSN_CheckCode"]);
                    if (reader["ENT_Province_Code"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_Province_Code = Convert.ToString(reader["ENT_Province_Code"]);
                    if (reader["PSN_Level"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Level = Convert.ToString(reader["PSN_Level"]);
                    if (reader["ZGZSBH"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ZGZSBH = Convert.ToString(reader["ZGZSBH"]);
                    if (reader["CJR"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.CJR = Convert.ToString(reader["CJR"]);
                    if (reader["CJSJ"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
                    if (reader["XGR"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.XGR = Convert.ToString(reader["XGR"]);
                    if (reader["XGSJ"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.XGSJ = Convert.ToDateTime(reader["XGSJ"]);
                    if (reader["Valid"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.Valid = Convert.ToInt32(reader["Valid"]);
                    if (reader["Memo"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.Memo = Convert.ToString(reader["Memo"]);
                }
                reader.Close();
                db.Close();
                return _COC_TOW_Person_BaseInfoMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.COC_TOW_Person_BaseInfo", "*", filterWhereString, orderBy == "" ? " PSN_RegisterCertificateNo" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.COC_TOW_Person_BaseInfo", filterWhereString);
        }

        /// <summary>
        /// 根据证件号码获取二建二级证书信息
        /// </summary>
        /// <param name="PSN_CertificateNO">证件号码</param>
        public static COC_TOW_Person_BaseInfoMDL GetObjectByPSN_CertificateNOPSN_Level(string PSN_CertificateNO)
        {
            string sql = @"
			SELECT top 1 PSN_ServerID,ENT_ServerID,ENT_Name,ENT_OrganizationsCode,ENT_City,BeginTime,EndTime,PSN_Name,PSN_Sex,PSN_BirthDate,PSN_National,PSN_CertificateType,PSN_CertificateNO,PSN_GraduationSchool,PSN_Specialty,PSN_GraduationTime,PSN_Qualification,PSN_Degree,PSN_MobilePhone,PSN_Telephone,PSN_Email,PSN_PMGrade,PSN_PMCertificateNo,PSN_RegisteType,PSN_RegisterNO,PSN_RegisterCertificateNo,PSN_RegisteProfession,PSN_CertificationDate,PSN_CertificateValidity,PSN_RegistePermissionDate,PSN_ChangeReason,PSN_BeforENT_Name,PSN_BeforENT_ServerID,PSN_BeforPersonName,PSN_InterprovincialChange,PSN_ExpiryReasons,PSN_ExpiryDate,PSN_RenewalProfession,PSN_AddProfession,PSN_CancelPerson,PSN_CancelReason,PSN_ReReasons,PSN_ReContent,PSN_CheckCode,ENT_Province_Code,PSN_Level,ZGZSBH,CJR,CJSJ,XGR,XGSJ,[VALID],Memo
			FROM dbo.COC_TOW_Person_BaseInfo
			WHERE PSN_CertificateNO = @PSN_CertificateNO and PSN_Level !='二级临时'";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PSN_CertificateNO", DbType.String, PSN_CertificateNO));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                COC_TOW_Person_BaseInfoMDL _COC_TOW_Person_BaseInfoMDL = null;
                if (reader.Read())
                {
                    _COC_TOW_Person_BaseInfoMDL = new COC_TOW_Person_BaseInfoMDL();
                    if (reader["PSN_ServerID"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ServerID = Convert.ToString(reader["PSN_ServerID"]);
                    if (reader["ENT_ServerID"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_ServerID = Convert.ToString(reader["ENT_ServerID"]);
                    if (reader["ENT_Name"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_Name = Convert.ToString(reader["ENT_Name"]);
                    if (reader["ENT_OrganizationsCode"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_OrganizationsCode = Convert.ToString(reader["ENT_OrganizationsCode"]);
                    if (reader["ENT_City"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_City = Convert.ToString(reader["ENT_City"]);
                    if (reader["BeginTime"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.BeginTime = Convert.ToDateTime(reader["BeginTime"]);
                    if (reader["EndTime"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.EndTime = Convert.ToDateTime(reader["EndTime"]);
                    if (reader["PSN_Name"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Name = Convert.ToString(reader["PSN_Name"]);
                    if (reader["PSN_Sex"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Sex = Convert.ToString(reader["PSN_Sex"]);
                    if (reader["PSN_BirthDate"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_BirthDate = Convert.ToDateTime(reader["PSN_BirthDate"]);
                    if (reader["PSN_National"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_National = Convert.ToString(reader["PSN_National"]);
                    if (reader["PSN_CertificateType"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CertificateType = Convert.ToString(reader["PSN_CertificateType"]);
                    if (reader["PSN_CertificateNO"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CertificateNO = Convert.ToString(reader["PSN_CertificateNO"]);
                    if (reader["PSN_GraduationSchool"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_GraduationSchool = Convert.ToString(reader["PSN_GraduationSchool"]);
                    if (reader["PSN_Specialty"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Specialty = Convert.ToString(reader["PSN_Specialty"]);
                    if (reader["PSN_GraduationTime"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_GraduationTime = Convert.ToDateTime(reader["PSN_GraduationTime"]);
                    if (reader["PSN_Qualification"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Qualification = Convert.ToString(reader["PSN_Qualification"]);
                    if (reader["PSN_Degree"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Degree = Convert.ToString(reader["PSN_Degree"]);
                    if (reader["PSN_MobilePhone"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_MobilePhone = Convert.ToString(reader["PSN_MobilePhone"]);
                    if (reader["PSN_Telephone"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Telephone = Convert.ToString(reader["PSN_Telephone"]);
                    if (reader["PSN_Email"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Email = Convert.ToString(reader["PSN_Email"]);
                    if (reader["PSN_PMGrade"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_PMGrade = Convert.ToString(reader["PSN_PMGrade"]);
                    if (reader["PSN_PMCertificateNo"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_PMCertificateNo = Convert.ToString(reader["PSN_PMCertificateNo"]);
                    if (reader["PSN_RegisteType"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegisteType = Convert.ToString(reader["PSN_RegisteType"]);
                    if (reader["PSN_RegisterNO"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegisterNO = Convert.ToString(reader["PSN_RegisterNO"]);
                    if (reader["PSN_RegisterCertificateNo"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegisterCertificateNo = Convert.ToString(reader["PSN_RegisterCertificateNo"]);
                    if (reader["PSN_RegisteProfession"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegisteProfession = Convert.ToString(reader["PSN_RegisteProfession"]);
                    if (reader["PSN_CertificationDate"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CertificationDate = Convert.ToDateTime(reader["PSN_CertificationDate"]);
                    if (reader["PSN_CertificateValidity"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CertificateValidity = Convert.ToDateTime(reader["PSN_CertificateValidity"]);
                    if (reader["PSN_RegistePermissionDate"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegistePermissionDate = Convert.ToDateTime(reader["PSN_RegistePermissionDate"]);
                    if (reader["PSN_ChangeReason"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ChangeReason = Convert.ToString(reader["PSN_ChangeReason"]);
                    if (reader["PSN_BeforENT_Name"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_BeforENT_Name = Convert.ToString(reader["PSN_BeforENT_Name"]);
                    if (reader["PSN_BeforENT_ServerID"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_BeforENT_ServerID = Convert.ToString(reader["PSN_BeforENT_ServerID"]);
                    if (reader["PSN_BeforPersonName"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_BeforPersonName = Convert.ToString(reader["PSN_BeforPersonName"]);
                    if (reader["PSN_InterprovincialChange"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_InterprovincialChange = Convert.ToString(reader["PSN_InterprovincialChange"]);
                    if (reader["PSN_ExpiryReasons"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ExpiryReasons = Convert.ToString(reader["PSN_ExpiryReasons"]);
                    if (reader["PSN_ExpiryDate"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ExpiryDate = Convert.ToDateTime(reader["PSN_ExpiryDate"]);
                    if (reader["PSN_RenewalProfession"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RenewalProfession = Convert.ToString(reader["PSN_RenewalProfession"]);
                    if (reader["PSN_AddProfession"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_AddProfession = Convert.ToString(reader["PSN_AddProfession"]);
                    if (reader["PSN_CancelPerson"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CancelPerson = Convert.ToString(reader["PSN_CancelPerson"]);
                    if (reader["PSN_CancelReason"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CancelReason = Convert.ToString(reader["PSN_CancelReason"]);
                    if (reader["PSN_ReReasons"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ReReasons = Convert.ToString(reader["PSN_ReReasons"]);
                    if (reader["PSN_ReContent"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ReContent = Convert.ToString(reader["PSN_ReContent"]);
                    if (reader["PSN_CheckCode"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CheckCode = Convert.ToString(reader["PSN_CheckCode"]);
                    if (reader["ENT_Province_Code"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_Province_Code = Convert.ToString(reader["ENT_Province_Code"]);
                    if (reader["PSN_Level"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Level = Convert.ToString(reader["PSN_Level"]);
                    if (reader["ZGZSBH"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ZGZSBH = Convert.ToString(reader["ZGZSBH"]);
                    if (reader["CJR"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.CJR = Convert.ToString(reader["CJR"]);
                    if (reader["CJSJ"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
                    if (reader["XGR"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.XGR = Convert.ToString(reader["XGR"]);
                    if (reader["XGSJ"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.XGSJ = Convert.ToDateTime(reader["XGSJ"]);
                    if (reader["Valid"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.Valid = Convert.ToInt32(reader["Valid"]);
                    if (reader["Memo"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.Memo = Convert.ToString(reader["Memo"]);
                }
                reader.Close();
                db.Close();
                return _COC_TOW_Person_BaseInfoMDL;
            }
        }

        //查询个人证书是否打印
        public static int PrintIsorNot(string PSN_CertificateNO)
        {

            string sql = string.Format(@"and a.PSN_CertificateNO='{0}' and (a.NoticeDate > b.[PrintTime] or b.PrintTime is null) 
            and a.ApplyStatus='已公告' and a.NoticeDate is not null and a.NoticeDate>'2018-05-10'
            and (((a.ApplyType !='变更注册' and a.ApplyType!='注销' or ApplyTypeSub='个人信息变更')and a.ConfirmResult='通过') or (a.ApplyType ='变更注册' and a.ENT_Name!=b.PSN_BeforENT_Name and b.PSN_BeforENT_Name is not null)) and b.Valid=1", PSN_CertificateNO);//5-10之后开始校验
            return CommonDAL.SelectRowCount("apply  a inner join  COC_TOW_Person_BaseInfo  b on a.PSN_CertificateNO=b.PSN_CertificateNO", sql);
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.View_JZS_TOW", "*", filterWhereString, orderBy == "" ? " xgsj desc,PSN_ServerID" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountView(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.View_JZS_TOW", filterWhereString);
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
        public static DataTable GetListWithProfession(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.View_JZS_TOW_WithProfession", "*", filterWhereString, orderBy == "" ? " PSN_ServerID" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountWithProfession(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.View_JZS_TOW_WithProfession", filterWhereString);
        }


        /// <summary>
        /// 根据组织机构代码获取一个公司的集合，对企业集体变更企业名称
        /// </summary>
        /// <param name="COC_TOW_Person_BaseInfoID">组织机构代码</param>
        public static List<COC_TOW_Person_BaseInfoMDL> ListGetObject(string ENT_OrganizationsCode)
        {
            string sql = @"
			SELECT PSN_ServerID,ENT_ServerID,ENT_Name,ENT_OrganizationsCode,ENT_City,BeginTime,EndTime,PSN_Name,PSN_Sex,PSN_BirthDate,PSN_National,PSN_CertificateType,PSN_CertificateNO,PSN_GraduationSchool,PSN_Specialty,PSN_GraduationTime,PSN_Qualification,PSN_Degree,PSN_MobilePhone,PSN_Telephone,PSN_Email,PSN_PMGrade,PSN_PMCertificateNo,PSN_RegisteType,PSN_RegisterNO,PSN_RegisterCertificateNo,PSN_RegisteProfession,PSN_CertificationDate,PSN_CertificateValidity,PSN_RegistePermissionDate,PSN_ChangeReason,PSN_BeforENT_Name,PSN_BeforENT_ServerID,PSN_BeforPersonName,PSN_InterprovincialChange,PSN_ExpiryReasons,PSN_ExpiryDate,PSN_RenewalProfession,PSN_AddProfession,PSN_CancelPerson,PSN_CancelReason,PSN_ReReasons,PSN_ReContent,PSN_CheckCode,ENT_Province_Code,PSN_Level,CJR,CJSJ,XGR,XGSJ,[VALID],Memo
			FROM dbo.COC_TOW_Person_BaseInfo
			WHERE (ENT_OrganizationsCode = @ENT_OrganizationsCode or ENT_OrganizationsCode like '________" + ENT_OrganizationsCode + "_')";
            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ENT_OrganizationsCode", DbType.String, ENT_OrganizationsCode));
            using (DataTable dt = db.GetFillData(sql, p.ToArray()))
            {
                List<COC_TOW_Person_BaseInfoMDL> __COC_TOW_Person_BaseInfoMDL = new List<COC_TOW_Person_BaseInfoMDL>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        COC_TOW_Person_BaseInfoMDL _COC_TOW_Person_BaseInfoMDL = new COC_TOW_Person_BaseInfoMDL();
                        if (dt.Rows[i]["PSN_ServerID"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ServerID = Convert.ToString(dt.Rows[i]["PSN_ServerID"]);
                        if (dt.Rows[i]["ENT_ServerID"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_ServerID = Convert.ToString(dt.Rows[i]["ENT_ServerID"]);
                        if (dt.Rows[i]["ENT_Name"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_Name = Convert.ToString(dt.Rows[i]["ENT_Name"]);
                        if (dt.Rows[i]["ENT_OrganizationsCode"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_OrganizationsCode = Convert.ToString(dt.Rows[i]["ENT_OrganizationsCode"]);
                        if (dt.Rows[i]["ENT_City"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_City = Convert.ToString(dt.Rows[i]["ENT_City"]);
                        if (dt.Rows[i]["BeginTime"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.BeginTime = Convert.ToDateTime(dt.Rows[i]["BeginTime"]);
                        if (dt.Rows[i]["EndTime"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.EndTime = Convert.ToDateTime(dt.Rows[i]["EndTime"]);
                        if (dt.Rows[i]["PSN_Name"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Name = Convert.ToString(dt.Rows[i]["PSN_Name"]);
                        if (dt.Rows[i]["PSN_Sex"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Sex = Convert.ToString(dt.Rows[i]["PSN_Sex"]);
                        if (dt.Rows[i]["PSN_BirthDate"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_BirthDate = Convert.ToDateTime(dt.Rows[i]["PSN_BirthDate"]);
                        if (dt.Rows[i]["PSN_National"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_National = Convert.ToString(dt.Rows[i]["PSN_National"]);
                        if (dt.Rows[i]["PSN_CertificateType"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CertificateType = Convert.ToString(dt.Rows[i]["PSN_CertificateType"]);
                        if (dt.Rows[i]["PSN_CertificateNO"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CertificateNO = Convert.ToString(dt.Rows[i]["PSN_CertificateNO"]);
                        if (dt.Rows[i]["PSN_GraduationSchool"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_GraduationSchool = Convert.ToString(dt.Rows[i]["PSN_GraduationSchool"]);
                        if (dt.Rows[i]["PSN_Specialty"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Specialty = Convert.ToString(dt.Rows[i]["PSN_Specialty"]);
                        if (dt.Rows[i]["PSN_GraduationTime"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_GraduationTime = Convert.ToDateTime(dt.Rows[i]["PSN_GraduationTime"]);
                        if (dt.Rows[i]["PSN_Qualification"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Qualification = Convert.ToString(dt.Rows[i]["PSN_Qualification"]);
                        if (dt.Rows[i]["PSN_Degree"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Degree = Convert.ToString(dt.Rows[i]["PSN_Degree"]);
                        if (dt.Rows[i]["PSN_MobilePhone"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_MobilePhone = Convert.ToString(dt.Rows[i]["PSN_MobilePhone"]);
                        if (dt.Rows[i]["PSN_Telephone"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Telephone = Convert.ToString(dt.Rows[i]["PSN_Telephone"]);
                        if (dt.Rows[i]["PSN_Email"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Email = Convert.ToString(dt.Rows[i]["PSN_Email"]);
                        if (dt.Rows[i]["PSN_PMGrade"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_PMGrade = Convert.ToString(dt.Rows[i]["PSN_PMGrade"]);
                        if (dt.Rows[i]["PSN_PMCertificateNo"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_PMCertificateNo = Convert.ToString(dt.Rows[i]["PSN_PMCertificateNo"]);
                        if (dt.Rows[i]["PSN_RegisteType"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegisteType = Convert.ToString(dt.Rows[i]["PSN_RegisteType"]);
                        if (dt.Rows[i]["PSN_RegisterNO"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegisterNO = Convert.ToString(dt.Rows[i]["PSN_RegisterNO"]);
                        if (dt.Rows[i]["PSN_RegisterCertificateNo"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegisterCertificateNo = Convert.ToString(dt.Rows[i]["PSN_RegisterCertificateNo"]);
                        if (dt.Rows[i]["PSN_RegisteProfession"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegisteProfession = Convert.ToString(dt.Rows[i]["PSN_RegisteProfession"]);
                        if (dt.Rows[i]["PSN_CertificationDate"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CertificationDate = Convert.ToDateTime(dt.Rows[i]["PSN_CertificationDate"]);
                        if (dt.Rows[i]["PSN_CertificateValidity"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CertificateValidity = Convert.ToDateTime(dt.Rows[i]["PSN_CertificateValidity"]);
                        if (dt.Rows[i]["PSN_RegistePermissionDate"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegistePermissionDate = Convert.ToDateTime(dt.Rows[i]["PSN_RegistePermissionDate"]);
                        if (dt.Rows[i]["PSN_ChangeReason"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ChangeReason = Convert.ToString(dt.Rows[i]["PSN_ChangeReason"]);
                        if (dt.Rows[i]["PSN_BeforENT_Name"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_BeforENT_Name = Convert.ToString(dt.Rows[i]["PSN_BeforENT_Name"]);
                        if (dt.Rows[i]["PSN_BeforENT_ServerID"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_BeforENT_ServerID = Convert.ToString(dt.Rows[i]["PSN_BeforENT_ServerID"]);
                        if (dt.Rows[i]["PSN_BeforPersonName"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_BeforPersonName = Convert.ToString(dt.Rows[i]["PSN_BeforPersonName"]);
                        if (dt.Rows[i]["PSN_InterprovincialChange"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_InterprovincialChange = Convert.ToString(dt.Rows[i]["PSN_InterprovincialChange"]);
                        if (dt.Rows[i]["PSN_ExpiryReasons"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ExpiryReasons = Convert.ToString(dt.Rows[i]["PSN_ExpiryReasons"]);
                        if (dt.Rows[i]["PSN_ExpiryDate"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ExpiryDate = Convert.ToDateTime(dt.Rows[i]["PSN_ExpiryDate"]);
                        if (dt.Rows[i]["PSN_RenewalProfession"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RenewalProfession = Convert.ToString(dt.Rows[i]["PSN_RenewalProfession"]);
                        if (dt.Rows[i]["PSN_AddProfession"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_AddProfession = Convert.ToString(dt.Rows[i]["PSN_AddProfession"]);
                        if (dt.Rows[i]["PSN_CancelPerson"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CancelPerson = Convert.ToString(dt.Rows[i]["PSN_CancelPerson"]);
                        if (dt.Rows[i]["PSN_CancelReason"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CancelReason = Convert.ToString(dt.Rows[i]["PSN_CancelReason"]);
                        if (dt.Rows[i]["PSN_ReReasons"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ReReasons = Convert.ToString(dt.Rows[i]["PSN_ReReasons"]);
                        if (dt.Rows[i]["PSN_ReContent"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ReContent = Convert.ToString(dt.Rows[i]["PSN_ReContent"]);
                        if (dt.Rows[i]["PSN_CheckCode"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CheckCode = Convert.ToString(dt.Rows[i]["PSN_CheckCode"]);
                        if (dt.Rows[i]["ENT_Province_Code"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_Province_Code = Convert.ToString(dt.Rows[i]["ENT_Province_Code"]);
                        if (dt.Rows[i]["PSN_Level"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Level = Convert.ToString(dt.Rows[i]["PSN_Level"]);
                        if (dt.Rows[i]["CJR"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.CJR = Convert.ToString(dt.Rows[i]["CJR"]);
                        if (dt.Rows[i]["CJSJ"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.CJSJ = Convert.ToDateTime(dt.Rows[i]["CJSJ"]);
                        if (dt.Rows[i]["XGR"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.XGR = Convert.ToString(dt.Rows[i]["XGR"]);
                        if (dt.Rows[i]["XGSJ"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.XGSJ = Convert.ToDateTime(dt.Rows[i]["XGSJ"]);
                        if (dt.Rows[i]["Valid"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.Valid = Convert.ToInt32(dt.Rows[i]["Valid"]);
                        if (dt.Rows[i]["Memo"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.Memo = Convert.ToString(dt.Rows[i]["Memo"]);
                        __COC_TOW_Person_BaseInfoMDL.Add(_COC_TOW_Person_BaseInfoMDL);
                    }

                }
                db.Close();
                return __COC_TOW_Person_BaseInfoMDL;
            }
        }
        /// <summary>
        /// 执业人员增长趋势
        /// </summary>
        /// <param name="year">年份</param>
        /// <returns></returns>
        public static DataTable GetTowPerson(string year)
        {
            string sql = @"
                        select count(PSN_ServerID) as Num,month(PSN_CertificationDate) as Yue  FROM [dbo].[COC_TOW_Person_BaseInfo] 
                        where year(PSN_CertificationDate)= year('" + year + "')  group by  month(PSN_CertificationDate) having month(PSN_CertificationDate) is not null  order by  month(PSN_CertificationDate) asc";
            return (new DBHelper()).GetFillData(sql);
        }
        //各区县人员总量及分布情况
        public static DataTable GetCountPerson()
        {
            //            string sql = @"select case when len(ent_city)>4  then substring(ent_city,1,2)
            //                                    when len(ent_city)>3  then substring(ent_city,1,3)
            //                                    else  substring(ent_city,1,2) end  as City,count(psn_serverid) as Num FROM [dbo].[COC_TOW_Person_BaseInfo]
            //                                    group by ent_city having ent_city is not null and ent_city <>'武警水电'";
            string sql = @"select case when City='武警水' then '其他' else  City end as City,Num from 
                                        (
                                           select case when len(ent_city)>4  then substring(ent_city,1,2)
                                                when len(ent_city)>3  then substring(ent_city,1,3)
                                                else  substring(ent_city,1,2) end  as City,count(psn_serverid) as Num FROM [dbo].[COC_TOW_Person_BaseInfo]
                                                group by ent_city having ent_city is not null
                                      ) t";

            return (new DBHelper()).GetFillData(sql);
        }
        //执业企业变更，根据注册号和证件号码抓取人员信息
        public static DataTable ChangePerson(string PSN_RegisterNO, string PSN_CertificateNO)
        {
            string sql = string.Format("SELECT * FROM COC_TOW_Person_BaseInfo WHERE PSN_RegisterNO='{0}'  AND PSN_CertificateNO='{1}'", PSN_RegisterNO, PSN_CertificateNO);
            return (new DBHelper()).GetFillData(sql);
        }

        /// <summary>
        /// 重载：根据注册证号和证件号码获取单个实体
        /// </summary>
        /// <param name="PSN_RegisterNO">注册证号</param>
        /// <param name="PSN_CertificateNO">证件号码</param>
        public static COC_TOW_Person_BaseInfoMDL GetObject(string PSN_RegisterNO, string PSN_CertificateNO)
        {
            string sql = @"
			SELECT top 1 PSN_ServerID,ENT_ServerID,ENT_Name,ENT_OrganizationsCode,ENT_City,BeginTime,EndTime,PSN_Name,PSN_Sex,PSN_BirthDate,PSN_National,PSN_CertificateType,PSN_CertificateNO,PSN_GraduationSchool,PSN_Specialty,PSN_GraduationTime,PSN_Qualification,PSN_Degree,PSN_MobilePhone,PSN_Telephone,PSN_Email,PSN_PMGrade,PSN_PMCertificateNo,PSN_RegisteType,PSN_RegisterNO,PSN_RegisterCertificateNo,PSN_RegisteProfession,PSN_CertificationDate,PSN_CertificateValidity,PSN_RegistePermissionDate,PSN_ChangeReason,PSN_BeforENT_Name,PSN_BeforENT_ServerID,PSN_BeforPersonName,PSN_InterprovincialChange,PSN_ExpiryReasons,PSN_ExpiryDate,PSN_RenewalProfession,PSN_AddProfession,PSN_CancelPerson,PSN_CancelReason,PSN_ReReasons,PSN_ReContent,PSN_CheckCode,ENT_Province_Code,PSN_Level,CJR,CJSJ,XGR,XGSJ,[VALID],Memo,[ApplyCATime],[ReturnCATime],[CertificateCAID]
			FROM dbo.COC_TOW_Person_BaseInfo
			WHERE PSN_RegisterNO = @PSN_RegisterNO and PSN_CertificateNO=@PSN_CertificateNO";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PSN_RegisterNO", DbType.String, PSN_RegisterNO));
            p.Add(db.CreateParameter("PSN_CertificateNO", DbType.String, PSN_CertificateNO));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                COC_TOW_Person_BaseInfoMDL _COC_TOW_Person_BaseInfoMDL = null;
                if (reader.Read())
                {
                    _COC_TOW_Person_BaseInfoMDL = new COC_TOW_Person_BaseInfoMDL();
                    if (reader["PSN_ServerID"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ServerID = Convert.ToString(reader["PSN_ServerID"]);
                    if (reader["ENT_ServerID"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_ServerID = Convert.ToString(reader["ENT_ServerID"]);
                    if (reader["ENT_Name"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_Name = Convert.ToString(reader["ENT_Name"]);
                    if (reader["ENT_OrganizationsCode"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_OrganizationsCode = Convert.ToString(reader["ENT_OrganizationsCode"]);
                    if (reader["ENT_City"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_City = Convert.ToString(reader["ENT_City"]);
                    if (reader["BeginTime"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.BeginTime = Convert.ToDateTime(reader["BeginTime"]);
                    if (reader["EndTime"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.EndTime = Convert.ToDateTime(reader["EndTime"]);
                    if (reader["PSN_Name"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Name = Convert.ToString(reader["PSN_Name"]);
                    if (reader["PSN_Sex"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Sex = Convert.ToString(reader["PSN_Sex"]);
                    if (reader["PSN_BirthDate"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_BirthDate = Convert.ToDateTime(reader["PSN_BirthDate"]);
                    if (reader["PSN_National"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_National = Convert.ToString(reader["PSN_National"]);
                    if (reader["PSN_CertificateType"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CertificateType = Convert.ToString(reader["PSN_CertificateType"]);
                    if (reader["PSN_CertificateNO"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CertificateNO = Convert.ToString(reader["PSN_CertificateNO"]);
                    if (reader["PSN_GraduationSchool"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_GraduationSchool = Convert.ToString(reader["PSN_GraduationSchool"]);
                    if (reader["PSN_Specialty"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Specialty = Convert.ToString(reader["PSN_Specialty"]);
                    if (reader["PSN_GraduationTime"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_GraduationTime = Convert.ToDateTime(reader["PSN_GraduationTime"]);
                    if (reader["PSN_Qualification"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Qualification = Convert.ToString(reader["PSN_Qualification"]);
                    if (reader["PSN_Degree"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Degree = Convert.ToString(reader["PSN_Degree"]);
                    if (reader["PSN_MobilePhone"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_MobilePhone = Convert.ToString(reader["PSN_MobilePhone"]);
                    if (reader["PSN_Telephone"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Telephone = Convert.ToString(reader["PSN_Telephone"]);
                    if (reader["PSN_Email"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Email = Convert.ToString(reader["PSN_Email"]);
                    if (reader["PSN_PMGrade"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_PMGrade = Convert.ToString(reader["PSN_PMGrade"]);
                    if (reader["PSN_PMCertificateNo"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_PMCertificateNo = Convert.ToString(reader["PSN_PMCertificateNo"]);
                    if (reader["PSN_RegisteType"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegisteType = Convert.ToString(reader["PSN_RegisteType"]);
                    if (reader["PSN_RegisterNO"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegisterNO = Convert.ToString(reader["PSN_RegisterNO"]);
                    if (reader["PSN_RegisterCertificateNo"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegisterCertificateNo = Convert.ToString(reader["PSN_RegisterCertificateNo"]);
                    if (reader["PSN_RegisteProfession"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegisteProfession = Convert.ToString(reader["PSN_RegisteProfession"]);
                    if (reader["PSN_CertificationDate"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CertificationDate = Convert.ToDateTime(reader["PSN_CertificationDate"]);
                    if (reader["PSN_CertificateValidity"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CertificateValidity = Convert.ToDateTime(reader["PSN_CertificateValidity"]);
                    if (reader["PSN_RegistePermissionDate"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegistePermissionDate = Convert.ToDateTime(reader["PSN_RegistePermissionDate"]);
                    if (reader["PSN_ChangeReason"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ChangeReason = Convert.ToString(reader["PSN_ChangeReason"]);
                    if (reader["PSN_BeforENT_Name"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_BeforENT_Name = Convert.ToString(reader["PSN_BeforENT_Name"]);
                    if (reader["PSN_BeforENT_ServerID"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_BeforENT_ServerID = Convert.ToString(reader["PSN_BeforENT_ServerID"]);
                    if (reader["PSN_BeforPersonName"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_BeforPersonName = Convert.ToString(reader["PSN_BeforPersonName"]);
                    if (reader["PSN_InterprovincialChange"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_InterprovincialChange = Convert.ToString(reader["PSN_InterprovincialChange"]);
                    if (reader["PSN_ExpiryReasons"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ExpiryReasons = Convert.ToString(reader["PSN_ExpiryReasons"]);
                    if (reader["PSN_ExpiryDate"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ExpiryDate = Convert.ToDateTime(reader["PSN_ExpiryDate"]);
                    if (reader["PSN_RenewalProfession"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RenewalProfession = Convert.ToString(reader["PSN_RenewalProfession"]);
                    if (reader["PSN_AddProfession"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_AddProfession = Convert.ToString(reader["PSN_AddProfession"]);
                    if (reader["PSN_CancelPerson"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CancelPerson = Convert.ToString(reader["PSN_CancelPerson"]);
                    if (reader["PSN_CancelReason"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CancelReason = Convert.ToString(reader["PSN_CancelReason"]);
                    if (reader["PSN_ReReasons"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ReReasons = Convert.ToString(reader["PSN_ReReasons"]);
                    if (reader["PSN_ReContent"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ReContent = Convert.ToString(reader["PSN_ReContent"]);
                    if (reader["PSN_CheckCode"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CheckCode = Convert.ToString(reader["PSN_CheckCode"]);
                    if (reader["ENT_Province_Code"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_Province_Code = Convert.ToString(reader["ENT_Province_Code"]);
                    if (reader["PSN_Level"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Level = Convert.ToString(reader["PSN_Level"]);
                    if (reader["CJR"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.CJR = Convert.ToString(reader["CJR"]);
                    if (reader["CJSJ"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
                    if (reader["XGR"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.XGR = Convert.ToString(reader["XGR"]);
                    if (reader["XGSJ"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.XGSJ = Convert.ToDateTime(reader["XGSJ"]);
                    if (reader["Valid"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.Valid = Convert.ToInt32(reader["Valid"]);
                    if (reader["Memo"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.Memo = Convert.ToString(reader["Memo"]);
                    if (reader["ApplyCATime"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ApplyCATime = Convert.ToDateTime(reader["ApplyCATime"]);
                    if (reader["ReturnCATime"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ReturnCATime = Convert.ToDateTime(reader["ReturnCATime"]);
                    if (reader["CertificateCAID"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.CertificateCAID = Convert.ToString(reader["CertificateCAID"]);
                }
                reader.Close();
                db.Close();
                return _COC_TOW_Person_BaseInfoMDL;
            }
        }


        /// <summary>
        /// 获取过期预警实体集合
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        public static DataTable GetListOverdueNotice(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "[dbo].[COC_TOW_Person_BaseInfo] u inner join [dbo].[COC_TOW_Register_Profession] p on u.[PSN_ServerID] = p.[PSN_ServerID]", "u.*,PRO_ValidityEnd", filterWhereString, orderBy == "" ? " u.PSN_ServerID" : orderBy);
        }
        /// <summary>
        /// 统计过期预警查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCounttOverdueNotice(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("[dbo].[COC_TOW_Person_BaseInfo] u inner join [dbo].[COC_TOW_Register_Profession] p on u.[PSN_ServerID] = p.[PSN_ServerID]", filterWhereString);
        }





        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        public static DataTable GetListWithCertificate(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows,
               @"dbo.View_JZS_TOW_WithProfession A 
                    left join [dbo].[View_JZS_TOW_Applying] B 
                        on a.PSN_ServerID=b.PSN_ServerID 
                    left join [jcsjk_RY_JZS_ZSSD] C 
                        on a.PSN_RegisterNO=c.ZCH 
                    left join [dbo].[LockJZS] L
                        on A.[PSN_ServerID] = L.[PSN_ServerID] and L.LockStatus='加锁' and L.LockEndTime > getdate()"
               , "A.*,c.SDZT,L.LockID,L.LockStatus,L.LockType,L.LockRemark", filterWhereString, orderBy == "" ? "A.psn_serverid" : orderBy);

//            return CommonDAL.GetDataTable(startRowIndex, maximumRows,
//                @"dbo.View_JZS_TOW_WithProfession A 
//                    left join [dbo].[View_JZS_TOW_Applying] B 
//                        on a.PSN_ServerID=b.PSN_ServerID 
//                    left join [jcsjk_RY_JZS_ZSSD] C 
//                        on a.PSN_RegisterNO=c.ZCH 
//                    left join [dbo].[CertificateLock_JZS] L
//                        on A.[PSN_CertificateNO] = L.[PSN_CertificateNO] and L.[LockStates]='加锁'"
//                , "a.*,c.SDZT,L.LockStates,L.LockContent", filterWhereString, orderBy == "" ? "A.psn_serverid" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountWithCertificate(string filterWhereString)
        {
            return CommonDAL.SelectRowCount(@"
                    dbo.View_JZS_TOW_WithProfession A 
                    left join [dbo].[View_JZS_TOW_Applying] B 
                        on a.PSN_ServerID=b.PSN_ServerID
                    left join [jcsjk_RY_JZS_ZSSD] C 
                        on a.PSN_RegisterNO=c.ZCH 
                    left join [dbo].[LockJZS] L
                        on A.[PSN_ServerID] = L.[PSN_ServerID] and L.LockStatus='加锁' and L.LockEndTime > getdate()", filterWhereString);

//            return CommonDAL.SelectRowCount(@"
//                    dbo.View_JZS_TOW_WithProfession A 
//                    left join [dbo].[View_JZS_TOW_Applying] B 
//                        on a.PSN_ServerID=b.PSN_ServerID
//                    left join [jcsjk_RY_JZS_ZSSD] C 
//                        on a.PSN_RegisterNO=c.ZCH 
//                    left join [dbo].[CertificateLock_JZS] L
//                        on A.[PSN_CertificateNO] = L.[PSN_CertificateNO] and L.[LockStates]='加锁'", filterWhereString);
        }



        public static COC_TOW_Person_BaseInfoMDL GetObjectByPSN_CertificateNO(string PSN_CertificateNO, string PSN_Level)
        {
            string sql = @"
			SELECT top 1 PSN_ServerID,ENT_ServerID,ENT_Name,ENT_OrganizationsCode,ENT_City,BeginTime,EndTime,PSN_Name,PSN_Sex,PSN_BirthDate,PSN_National,PSN_CertificateType,PSN_CertificateNO,PSN_GraduationSchool,PSN_Specialty,PSN_GraduationTime,PSN_Qualification,PSN_Degree,PSN_MobilePhone,PSN_Telephone,PSN_Email,PSN_PMGrade,PSN_PMCertificateNo,PSN_RegisteType,PSN_RegisterNO,PSN_RegisterCertificateNo,PSN_RegisteProfession,PSN_CertificationDate,PSN_CertificateValidity,PSN_RegistePermissionDate,PSN_ChangeReason,PSN_BeforENT_Name,PSN_BeforENT_ServerID,PSN_BeforPersonName,PSN_InterprovincialChange,PSN_ExpiryReasons,PSN_ExpiryDate,PSN_RenewalProfession,PSN_AddProfession,PSN_CancelPerson,PSN_CancelReason,PSN_ReReasons,PSN_ReContent,PSN_CheckCode,ENT_Province_Code,PSN_Level,ZGZSBH,CJR,CJSJ,XGR,XGSJ,[VALID],Memo
			FROM dbo.COC_TOW_Person_BaseInfo
			WHERE PSN_CertificateNO = @PSN_CertificateNO and PSN_Level=@PSN_Level";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PSN_CertificateNO", DbType.String, PSN_CertificateNO));
            p.Add(db.CreateParameter("PSN_Level", DbType.String, PSN_Level));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                COC_TOW_Person_BaseInfoMDL _COC_TOW_Person_BaseInfoMDL = null;
                if (reader.Read())
                {
                    _COC_TOW_Person_BaseInfoMDL = new COC_TOW_Person_BaseInfoMDL();
                    if (reader["PSN_ServerID"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ServerID = Convert.ToString(reader["PSN_ServerID"]);
                    if (reader["ENT_ServerID"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_ServerID = Convert.ToString(reader["ENT_ServerID"]);
                    if (reader["ENT_Name"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_Name = Convert.ToString(reader["ENT_Name"]);
                    if (reader["ENT_OrganizationsCode"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_OrganizationsCode = Convert.ToString(reader["ENT_OrganizationsCode"]);
                    if (reader["ENT_City"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_City = Convert.ToString(reader["ENT_City"]);
                    if (reader["BeginTime"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.BeginTime = Convert.ToDateTime(reader["BeginTime"]);
                    if (reader["EndTime"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.EndTime = Convert.ToDateTime(reader["EndTime"]);
                    if (reader["PSN_Name"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Name = Convert.ToString(reader["PSN_Name"]);
                    if (reader["PSN_Sex"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Sex = Convert.ToString(reader["PSN_Sex"]);
                    if (reader["PSN_BirthDate"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_BirthDate = Convert.ToDateTime(reader["PSN_BirthDate"]);
                    if (reader["PSN_National"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_National = Convert.ToString(reader["PSN_National"]);
                    if (reader["PSN_CertificateType"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CertificateType = Convert.ToString(reader["PSN_CertificateType"]);
                    if (reader["PSN_CertificateNO"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CertificateNO = Convert.ToString(reader["PSN_CertificateNO"]);
                    if (reader["PSN_GraduationSchool"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_GraduationSchool = Convert.ToString(reader["PSN_GraduationSchool"]);
                    if (reader["PSN_Specialty"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Specialty = Convert.ToString(reader["PSN_Specialty"]);
                    if (reader["PSN_GraduationTime"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_GraduationTime = Convert.ToDateTime(reader["PSN_GraduationTime"]);
                    if (reader["PSN_Qualification"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Qualification = Convert.ToString(reader["PSN_Qualification"]);
                    if (reader["PSN_Degree"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Degree = Convert.ToString(reader["PSN_Degree"]);
                    if (reader["PSN_MobilePhone"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_MobilePhone = Convert.ToString(reader["PSN_MobilePhone"]);
                    if (reader["PSN_Telephone"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Telephone = Convert.ToString(reader["PSN_Telephone"]);
                    if (reader["PSN_Email"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Email = Convert.ToString(reader["PSN_Email"]);
                    if (reader["PSN_PMGrade"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_PMGrade = Convert.ToString(reader["PSN_PMGrade"]);
                    if (reader["PSN_PMCertificateNo"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_PMCertificateNo = Convert.ToString(reader["PSN_PMCertificateNo"]);
                    if (reader["PSN_RegisteType"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegisteType = Convert.ToString(reader["PSN_RegisteType"]);
                    if (reader["PSN_RegisterNO"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegisterNO = Convert.ToString(reader["PSN_RegisterNO"]);
                    if (reader["PSN_RegisterCertificateNo"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegisterCertificateNo = Convert.ToString(reader["PSN_RegisterCertificateNo"]);
                    if (reader["PSN_RegisteProfession"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegisteProfession = Convert.ToString(reader["PSN_RegisteProfession"]);
                    if (reader["PSN_CertificationDate"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CertificationDate = Convert.ToDateTime(reader["PSN_CertificationDate"]);
                    if (reader["PSN_CertificateValidity"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CertificateValidity = Convert.ToDateTime(reader["PSN_CertificateValidity"]);
                    if (reader["PSN_RegistePermissionDate"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RegistePermissionDate = Convert.ToDateTime(reader["PSN_RegistePermissionDate"]);
                    if (reader["PSN_ChangeReason"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ChangeReason = Convert.ToString(reader["PSN_ChangeReason"]);
                    if (reader["PSN_BeforENT_Name"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_BeforENT_Name = Convert.ToString(reader["PSN_BeforENT_Name"]);
                    if (reader["PSN_BeforENT_ServerID"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_BeforENT_ServerID = Convert.ToString(reader["PSN_BeforENT_ServerID"]);
                    if (reader["PSN_BeforPersonName"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_BeforPersonName = Convert.ToString(reader["PSN_BeforPersonName"]);
                    if (reader["PSN_InterprovincialChange"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_InterprovincialChange = Convert.ToString(reader["PSN_InterprovincialChange"]);
                    if (reader["PSN_ExpiryReasons"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ExpiryReasons = Convert.ToString(reader["PSN_ExpiryReasons"]);
                    if (reader["PSN_ExpiryDate"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ExpiryDate = Convert.ToDateTime(reader["PSN_ExpiryDate"]);
                    if (reader["PSN_RenewalProfession"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_RenewalProfession = Convert.ToString(reader["PSN_RenewalProfession"]);
                    if (reader["PSN_AddProfession"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_AddProfession = Convert.ToString(reader["PSN_AddProfession"]);
                    if (reader["PSN_CancelPerson"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CancelPerson = Convert.ToString(reader["PSN_CancelPerson"]);
                    if (reader["PSN_CancelReason"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CancelReason = Convert.ToString(reader["PSN_CancelReason"]);
                    if (reader["PSN_ReReasons"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ReReasons = Convert.ToString(reader["PSN_ReReasons"]);
                    if (reader["PSN_ReContent"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_ReContent = Convert.ToString(reader["PSN_ReContent"]);
                    if (reader["PSN_CheckCode"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_CheckCode = Convert.ToString(reader["PSN_CheckCode"]);
                    if (reader["ENT_Province_Code"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ENT_Province_Code = Convert.ToString(reader["ENT_Province_Code"]);
                    if (reader["PSN_Level"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.PSN_Level = Convert.ToString(reader["PSN_Level"]);
                    if (reader["ZGZSBH"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.ZGZSBH = Convert.ToString(reader["ZGZSBH"]);
                    if (reader["CJR"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.CJR = Convert.ToString(reader["CJR"]);
                    if (reader["CJSJ"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
                    if (reader["XGR"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.XGR = Convert.ToString(reader["XGR"]);
                    if (reader["XGSJ"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.XGSJ = Convert.ToDateTime(reader["XGSJ"]);
                    if (reader["Valid"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.Valid = Convert.ToInt32(reader["Valid"]);
                    if (reader["Memo"] != DBNull.Value) _COC_TOW_Person_BaseInfoMDL.Memo = Convert.ToString(reader["Memo"]);
                }
                reader.Close();
                db.Close();
                return _COC_TOW_Person_BaseInfoMDL;
            }
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="COC_TOW_Person_BaseInfoMDL">对象实体类</param>
        /// <returns></returns>
        public static int UpdateByPSN_RegisterNO(COC_TOW_Person_BaseInfoMDL _COC_TOW_Person_BaseInfoMDL)
        {
            return UpdateByPSN_RegisterNO(null, _COC_TOW_Person_BaseInfoMDL);
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="COC_TOW_Person_BaseInfoMDL">对象实体类</param>
        /// <returns></returns>
        public static int UpdateByPSN_RegisterNO(DbTransaction tran, COC_TOW_Person_BaseInfoMDL _COC_TOW_Person_BaseInfoMDL)
        {
            string sql = @"
			UPDATE dbo.COC_TOW_Person_BaseInfo
				SET	PSN_Name = @PSN_Name,PSN_Sex = @PSN_Sex,ZGZSBH = @ZGZSBH
			WHERE
				PSN_CertificateNO = @PSN_CertificateNO and PSN_Level=@PSN_Level";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PSN_ServerID", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_ServerID));
            p.Add(db.CreateParameter("ENT_ServerID", DbType.String, _COC_TOW_Person_BaseInfoMDL.ENT_ServerID));
            p.Add(db.CreateParameter("ENT_Name", DbType.String, _COC_TOW_Person_BaseInfoMDL.ENT_Name));
            p.Add(db.CreateParameter("ENT_OrganizationsCode", DbType.String, _COC_TOW_Person_BaseInfoMDL.ENT_OrganizationsCode));
            p.Add(db.CreateParameter("ENT_City", DbType.String, _COC_TOW_Person_BaseInfoMDL.ENT_City));
            p.Add(db.CreateParameter("BeginTime", DbType.DateTime, _COC_TOW_Person_BaseInfoMDL.BeginTime));
            p.Add(db.CreateParameter("EndTime", DbType.DateTime, _COC_TOW_Person_BaseInfoMDL.EndTime));
            p.Add(db.CreateParameter("PSN_Name", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_Name));
            p.Add(db.CreateParameter("PSN_Sex", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_Sex));
            p.Add(db.CreateParameter("PSN_BirthDate", DbType.DateTime, _COC_TOW_Person_BaseInfoMDL.PSN_BirthDate));
            p.Add(db.CreateParameter("PSN_National", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_National));
            p.Add(db.CreateParameter("PSN_CertificateType", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_CertificateType));
            p.Add(db.CreateParameter("PSN_CertificateNO", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_CertificateNO));
            p.Add(db.CreateParameter("PSN_GraduationSchool", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_GraduationSchool));
            p.Add(db.CreateParameter("PSN_Specialty", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_Specialty));
            p.Add(db.CreateParameter("PSN_GraduationTime", DbType.DateTime, _COC_TOW_Person_BaseInfoMDL.PSN_GraduationTime));
            p.Add(db.CreateParameter("PSN_Qualification", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_Qualification));
            p.Add(db.CreateParameter("PSN_Degree", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_Degree));
            p.Add(db.CreateParameter("PSN_MobilePhone", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_MobilePhone));
            p.Add(db.CreateParameter("PSN_Telephone", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_Telephone));
            p.Add(db.CreateParameter("PSN_Email", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_Email));
            p.Add(db.CreateParameter("PSN_PMGrade", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_PMGrade));
            p.Add(db.CreateParameter("PSN_PMCertificateNo", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_PMCertificateNo));
            p.Add(db.CreateParameter("PSN_RegisteType", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_RegisteType));
            p.Add(db.CreateParameter("PSN_RegisterNO", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_RegisterNO));
            p.Add(db.CreateParameter("PSN_RegisterCertificateNo", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_RegisterCertificateNo));
            p.Add(db.CreateParameter("PSN_RegisteProfession", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_RegisteProfession));
            p.Add(db.CreateParameter("PSN_CertificationDate", DbType.DateTime, _COC_TOW_Person_BaseInfoMDL.PSN_CertificationDate));
            p.Add(db.CreateParameter("PSN_CertificateValidity", DbType.DateTime, _COC_TOW_Person_BaseInfoMDL.PSN_CertificateValidity));
            p.Add(db.CreateParameter("PSN_RegistePermissionDate", DbType.DateTime, _COC_TOW_Person_BaseInfoMDL.PSN_RegistePermissionDate));
            p.Add(db.CreateParameter("PSN_ChangeReason", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_ChangeReason));
            p.Add(db.CreateParameter("PSN_BeforENT_Name", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_BeforENT_Name));
            p.Add(db.CreateParameter("PSN_BeforENT_ServerID", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_BeforENT_ServerID));
            p.Add(db.CreateParameter("PSN_BeforPersonName", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_BeforPersonName));
            p.Add(db.CreateParameter("PSN_InterprovincialChange", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_InterprovincialChange));
            p.Add(db.CreateParameter("PSN_ExpiryReasons", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_ExpiryReasons));
            p.Add(db.CreateParameter("PSN_ExpiryDate", DbType.DateTime, _COC_TOW_Person_BaseInfoMDL.PSN_ExpiryDate));
            p.Add(db.CreateParameter("PSN_RenewalProfession", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_RenewalProfession));
            p.Add(db.CreateParameter("PSN_AddProfession", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_AddProfession));
            p.Add(db.CreateParameter("PSN_CancelPerson", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_CancelPerson));
            p.Add(db.CreateParameter("PSN_CancelReason", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_CancelReason));
            p.Add(db.CreateParameter("PSN_ReReasons", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_ReReasons));
            p.Add(db.CreateParameter("PSN_ReContent", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_ReContent));
            p.Add(db.CreateParameter("PSN_CheckCode", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_CheckCode));
            p.Add(db.CreateParameter("ENT_Province_Code", DbType.String, _COC_TOW_Person_BaseInfoMDL.ENT_Province_Code));
            p.Add(db.CreateParameter("PSN_Level", DbType.String, _COC_TOW_Person_BaseInfoMDL.PSN_Level));
            p.Add(db.CreateParameter("ZGZSBH", DbType.String, _COC_TOW_Person_BaseInfoMDL.ZGZSBH));
            p.Add(db.CreateParameter("CJR", DbType.String, _COC_TOW_Person_BaseInfoMDL.CJR));
            p.Add(db.CreateParameter("CJSJ", DbType.DateTime, _COC_TOW_Person_BaseInfoMDL.CJSJ));
            p.Add(db.CreateParameter("XGR", DbType.String, _COC_TOW_Person_BaseInfoMDL.XGR));
            p.Add(db.CreateParameter("XGSJ", DbType.DateTime, _COC_TOW_Person_BaseInfoMDL.XGSJ));
            p.Add(db.CreateParameter("Valid", DbType.Int32, _COC_TOW_Person_BaseInfoMDL.Valid));
            p.Add(db.CreateParameter("Memo", DbType.String, _COC_TOW_Person_BaseInfoMDL.Memo));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        ///获取六类人员列表
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <param name="filterWhereString"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static DataTable GetPeoPleList(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.[View_SixPeople]", "*", filterWhereString, orderBy == "" ? "LX" : orderBy);
        }

        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountPeoPleList(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.[View_SixPeople]", filterWhereString);
        }


        /// <summary>
        ///获取锁定人员列表
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <param name="filterWhereString"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static DataTable GetLockList(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "[dbo].[View_SixPeople] a left join [dbo].[CertificateLock_JZS] b on a.Fid=b.Fid and a.LX=b.LX and b.LockEndTime >GETDATE()", "a.*,b.LockStates,b.LockEndTime", filterWhereString, orderBy == "" ? "a.LX" : orderBy);
        }


        /// <summary>
        ///获取锁定人员个数
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <param name="filterWhereString"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static int GetCountLockList(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("[dbo].[View_SixPeople] a left join [dbo].[CertificateLock_JZS] b on a.Fid=b.Fid and a.LX=b.LX and b.LockEndTime >GETDATE()", filterWhereString);

        }

        /// <summary>
        /// 查询企业各类人员的个数
        /// </summary>
        /// <param name="PSN_RegisterNO"></param>
        /// <param name="PSN_CertificateNO"></param>
        /// <returns></returns>
        public static DataTable PeopleCount(string ENT_OrganizationsCode)
        {
            string sql = string.Format("SELECT LX ,COUNT(LX) as Num FROM [dbo].[View_SixPeople] WHERE ENT_OrganizationsCode='{0}' group by  LX ", ENT_OrganizationsCode);
            return (new DBHelper()).GetFillData(sql);
        }
        #endregion

        public static int ENT_PrintIsorNot(string ENT_OrganizationsCode)
        {
            string sql = string.Format(@"and a.ENT_OrganizationsCode='{0}' and (a.NoticeDate > b.[PrintTime] or b.PrintTime is null) 
            and a.ApplyStatus='已公告' and a.NoticeDate is not null and a.NoticeDate>'2018-05-10'
            and (((a.ApplyType !='变更注册' and a.ApplyType!='注销' or ApplyTypeSub='个人信息变更')and a.ConfirmResult='通过') or (a.ApplyType ='变更注册' and a.ENT_ServerID!=b.PSN_BeforENT_ServerID and b.PSN_BeforENT_ServerID is not null))", ENT_OrganizationsCode);//5-10之后开始校验
            return CommonDAL.SelectRowCount("apply  a inner join  COC_TOW_Person_BaseInfo  b on a.PSN_CertificateNO=b.PSN_CertificateNO", sql);

        }

        //查询证书区县与企业信息中的区县是否一致
        public static int ENT_IsorNotSame(string ZZJGDM)
        {

            string sql = string.Format(@"and  A.ENT_City!=B.ENT_City AND A.ENT_OrganizationsCode='{0}' and  B.PSN_RegisteType !='07'", ZZJGDM);//5-10之后开始校验
            return CommonDAL.SelectRowCount("Unit A INNER JOIN COC_TOW_Person_BaseInfo B ON A.ENT_OrganizationsCode=B.ENT_OrganizationsCode ", sql);
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.[TJ_JZS_TOW_Check]", "*", filterWhereString, orderBy == "" ? " [PSN_CertificateNO]" : orderBy);
        }
        /// <summary>
        /// 统计二建业务执法检查对象记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountCheck(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.[TJ_JZS_TOW_Check]", filterWhereString);
        }


    }
}
