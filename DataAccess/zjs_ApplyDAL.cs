using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
    /// <summary>
    /// 业务类实现--zjs_ApplyDAL(填写类描述)
    /// </summary>
    public class zjs_ApplyDAL
    {
        public zjs_ApplyDAL() { }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_zjs_ApplyMDL">对象实体类</param>
        /// <returns></returns>
        public static int Insert(zjs_ApplyMDL _zjs_ApplyMDL)
        {
            return Insert(null, _zjs_ApplyMDL);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_zjs_ApplyMDL">对象实体类</param>
        /// <returns></returns>
        public static int Insert(DbTransaction tran, zjs_ApplyMDL _zjs_ApplyMDL)
        {
            DBHelper db = new DBHelper();
            string sql = @"
			INSERT INTO dbo.zjs_Apply(ApplyID,ApplyType,ApplyTypeSub,ENT_Name,ENT_OrganizationsCode,ENT_City,PSN_Name,PSN_Sex,PSN_CertificateType,PSN_CertificateNO,PSN_RegisterNo,PSN_RegisterCertificateNo,PSN_RegisteProfession,ApplyTime,ApplyCode,ApplyStatus,GetDateTime,GetResult,GetRemark,GetMan,ExamineDatetime,ExamineResult,ExamineRemark,ExamineMan,ReportDate,ReportMan,ReportCode,CheckDate,CheckResult,CheckRemark,CheckMan,ConfirmDate,ConfirmResult,ConfirmMan,PublicDate,PublicMan,PublicCode,NoticeDate,NoticeMan,NoticeCode,CodeDate,CodeMan,CJR,CJSJ,XGR,XGSJ,[VALID],Memo,SheBaoCheck,OldUnitName,OldEnt_QYZJJGDM,OldUnitCheckTime,OldUnitCheckResult,OldUnitCheckRemark,ENT_ContractType,ENT_ContractStartTime,ENT_ContractENDTime,LastBackResult,CheckZSSD,CheckXSL,CheckCFZC,CheckYCZC,newUnitCheckTime,newUnitCheckResult,newUnitCheckRemark,Nation,Birthday)
			VALUES (@ApplyID,@ApplyType,@ApplyTypeSub,@ENT_Name,@ENT_OrganizationsCode,@ENT_City,@PSN_Name,@PSN_Sex,@PSN_CertificateType,@PSN_CertificateNO,@PSN_RegisterNo,@PSN_RegisterCertificateNo,@PSN_RegisteProfession,@ApplyTime,@ApplyCode,@ApplyStatus,@GetDateTime,@GetResult,@GetRemark,@GetMan,@ExamineDatetime,@ExamineResult,@ExamineRemark,@ExamineMan,@ReportDate,@ReportMan,@ReportCode,@CheckDate,@CheckResult,@CheckRemark,@CheckMan,@ConfirmDate,@ConfirmResult,@ConfirmMan,@PublicDate,@PublicMan,@PublicCode,@NoticeDate,@NoticeMan,@NoticeCode,@CodeDate,@CodeMan,@CJR,@CJSJ,@XGR,@XGSJ,@Valid,@Memo,@SheBaoCheck,@OldUnitName,@OldEnt_QYZJJGDM,@OldUnitCheckTime,@OldUnitCheckResult,@OldUnitCheckRemark,@ENT_ContractType,@ENT_ContractStartTime,@ENT_ContractENDTime,@LastBackResult,@CheckZSSD,@CheckXSL,@CheckCFZC,@CheckYCZC,@newUnitCheckTime,@newUnitCheckResult,@newUnitCheckRemark,@Nation,@Birthday)";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, _zjs_ApplyMDL.ApplyID));
            p.Add(db.CreateParameter("ApplyType", DbType.String, _zjs_ApplyMDL.ApplyType));
            p.Add(db.CreateParameter("ApplyTypeSub", DbType.String, _zjs_ApplyMDL.ApplyTypeSub));
            p.Add(db.CreateParameter("ENT_Name", DbType.String, _zjs_ApplyMDL.ENT_Name));
            p.Add(db.CreateParameter("ENT_OrganizationsCode", DbType.String, _zjs_ApplyMDL.ENT_OrganizationsCode));
            p.Add(db.CreateParameter("ENT_City", DbType.String, _zjs_ApplyMDL.ENT_City));
            p.Add(db.CreateParameter("PSN_Name", DbType.String, _zjs_ApplyMDL.PSN_Name));
            p.Add(db.CreateParameter("PSN_Sex", DbType.String, _zjs_ApplyMDL.PSN_Sex));
            p.Add(db.CreateParameter("PSN_CertificateType", DbType.String, _zjs_ApplyMDL.PSN_CertificateType));
            p.Add(db.CreateParameter("PSN_CertificateNO", DbType.String, _zjs_ApplyMDL.PSN_CertificateNO));
            p.Add(db.CreateParameter("PSN_RegisterNo", DbType.String, _zjs_ApplyMDL.PSN_RegisterNo));
            p.Add(db.CreateParameter("PSN_RegisterCertificateNo", DbType.String, _zjs_ApplyMDL.PSN_RegisterCertificateNo));
            p.Add(db.CreateParameter("PSN_RegisteProfession", DbType.String, _zjs_ApplyMDL.PSN_RegisteProfession));
            p.Add(db.CreateParameter("ApplyTime", DbType.DateTime, _zjs_ApplyMDL.ApplyTime));
            p.Add(db.CreateParameter("ApplyCode", DbType.String, _zjs_ApplyMDL.ApplyCode));
            p.Add(db.CreateParameter("ApplyStatus", DbType.String, _zjs_ApplyMDL.ApplyStatus));
            p.Add(db.CreateParameter("GetDateTime", DbType.DateTime, _zjs_ApplyMDL.GetDateTime));
            p.Add(db.CreateParameter("GetResult", DbType.String, _zjs_ApplyMDL.GetResult));
            p.Add(db.CreateParameter("GetRemark", DbType.String, _zjs_ApplyMDL.GetRemark));
            p.Add(db.CreateParameter("GetMan", DbType.String, _zjs_ApplyMDL.GetMan));
            p.Add(db.CreateParameter("ExamineDatetime", DbType.DateTime, _zjs_ApplyMDL.ExamineDatetime));
            p.Add(db.CreateParameter("ExamineResult", DbType.String, _zjs_ApplyMDL.ExamineResult));
            p.Add(db.CreateParameter("ExamineRemark", DbType.String, _zjs_ApplyMDL.ExamineRemark));
            p.Add(db.CreateParameter("ExamineMan", DbType.String, _zjs_ApplyMDL.ExamineMan));
            p.Add(db.CreateParameter("ReportDate", DbType.DateTime, _zjs_ApplyMDL.ReportDate));
            p.Add(db.CreateParameter("ReportMan", DbType.String, _zjs_ApplyMDL.ReportMan));
            p.Add(db.CreateParameter("ReportCode", DbType.String, _zjs_ApplyMDL.ReportCode));
            p.Add(db.CreateParameter("CheckDate", DbType.DateTime, _zjs_ApplyMDL.CheckDate));
            p.Add(db.CreateParameter("CheckResult", DbType.String, _zjs_ApplyMDL.CheckResult));
            p.Add(db.CreateParameter("CheckRemark", DbType.String, _zjs_ApplyMDL.CheckRemark));
            p.Add(db.CreateParameter("CheckMan", DbType.String, _zjs_ApplyMDL.CheckMan));
            p.Add(db.CreateParameter("ConfirmDate", DbType.DateTime, _zjs_ApplyMDL.ConfirmDate));
            p.Add(db.CreateParameter("ConfirmResult", DbType.String, _zjs_ApplyMDL.ConfirmResult));
            p.Add(db.CreateParameter("ConfirmMan", DbType.String, _zjs_ApplyMDL.ConfirmMan));
            p.Add(db.CreateParameter("PublicDate", DbType.DateTime, _zjs_ApplyMDL.PublicDate));
            p.Add(db.CreateParameter("PublicMan", DbType.String, _zjs_ApplyMDL.PublicMan));
            p.Add(db.CreateParameter("PublicCode", DbType.String, _zjs_ApplyMDL.PublicCode));
            p.Add(db.CreateParameter("NoticeDate", DbType.DateTime, _zjs_ApplyMDL.NoticeDate));
            p.Add(db.CreateParameter("NoticeMan", DbType.String, _zjs_ApplyMDL.NoticeMan));
            p.Add(db.CreateParameter("NoticeCode", DbType.String, _zjs_ApplyMDL.NoticeCode));
            p.Add(db.CreateParameter("CodeDate", DbType.DateTime, _zjs_ApplyMDL.CodeDate));
            p.Add(db.CreateParameter("CodeMan", DbType.String, _zjs_ApplyMDL.CodeMan));
            p.Add(db.CreateParameter("CJR", DbType.String, _zjs_ApplyMDL.CJR));
            p.Add(db.CreateParameter("CJSJ", DbType.DateTime, _zjs_ApplyMDL.CJSJ));
            p.Add(db.CreateParameter("XGR", DbType.String, _zjs_ApplyMDL.XGR));
            p.Add(db.CreateParameter("XGSJ", DbType.DateTime, _zjs_ApplyMDL.XGSJ));
            p.Add(db.CreateParameter("Valid", DbType.Int32, _zjs_ApplyMDL.Valid));
            p.Add(db.CreateParameter("Memo", DbType.String, _zjs_ApplyMDL.Memo));
            p.Add(db.CreateParameter("SheBaoCheck", DbType.Int32, _zjs_ApplyMDL.SheBaoCheck));
            p.Add(db.CreateParameter("OldUnitName", DbType.String, _zjs_ApplyMDL.OldUnitName));
            p.Add(db.CreateParameter("OldEnt_QYZJJGDM", DbType.String, _zjs_ApplyMDL.OldEnt_QYZJJGDM));
            p.Add(db.CreateParameter("OldUnitCheckTime", DbType.DateTime, _zjs_ApplyMDL.OldUnitCheckTime));
            p.Add(db.CreateParameter("OldUnitCheckResult", DbType.String, _zjs_ApplyMDL.OldUnitCheckResult));
            p.Add(db.CreateParameter("OldUnitCheckRemark", DbType.String, _zjs_ApplyMDL.OldUnitCheckRemark));
            p.Add(db.CreateParameter("ENT_ContractType", DbType.Int32, _zjs_ApplyMDL.ENT_ContractType));
            p.Add(db.CreateParameter("ENT_ContractStartTime", DbType.DateTime, _zjs_ApplyMDL.ENT_ContractStartTime));
            p.Add(db.CreateParameter("ENT_ContractENDTime", DbType.DateTime, _zjs_ApplyMDL.ENT_ContractENDTime));
            p.Add(db.CreateParameter("LastBackResult", DbType.String, _zjs_ApplyMDL.LastBackResult));
            p.Add(db.CreateParameter("CheckZSSD", DbType.Int32, _zjs_ApplyMDL.CheckZSSD));
            p.Add(db.CreateParameter("CheckXSL", DbType.Int32, _zjs_ApplyMDL.CheckXSL));
            p.Add(db.CreateParameter("CheckCFZC", DbType.Int32, _zjs_ApplyMDL.CheckCFZC));
            p.Add(db.CreateParameter("CheckYCZC", DbType.Int32, _zjs_ApplyMDL.CheckYCZC));
            p.Add(db.CreateParameter("newUnitCheckTime", DbType.DateTime, _zjs_ApplyMDL.newUnitCheckTime));
            p.Add(db.CreateParameter("newUnitCheckResult", DbType.String, _zjs_ApplyMDL.newUnitCheckResult));
            p.Add(db.CreateParameter("newUnitCheckRemark", DbType.String, _zjs_ApplyMDL.newUnitCheckRemark));
            p.Add(db.CreateParameter("Nation", DbType.String, _zjs_ApplyMDL.Nation));
            p.Add(db.CreateParameter("Birthday", DbType.DateTime, _zjs_ApplyMDL.Birthday));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_zjs_ApplyMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(zjs_ApplyMDL _zjs_ApplyMDL)
        {
            return Update(null, _zjs_ApplyMDL);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_zjs_ApplyMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, zjs_ApplyMDL _zjs_ApplyMDL)
        {
            string sql = @"
			UPDATE dbo.zjs_Apply
				SET	ApplyType = @ApplyType,ApplyTypeSub = @ApplyTypeSub,ENT_Name = @ENT_Name,ENT_OrganizationsCode = @ENT_OrganizationsCode,ENT_City = @ENT_City,PSN_Name = @PSN_Name,PSN_Sex = @PSN_Sex,PSN_CertificateType = @PSN_CertificateType,PSN_CertificateNO = @PSN_CertificateNO,PSN_RegisterNo = @PSN_RegisterNo,PSN_RegisterCertificateNo = @PSN_RegisterCertificateNo,PSN_RegisteProfession = @PSN_RegisteProfession,ApplyTime = @ApplyTime,ApplyCode = @ApplyCode,ApplyStatus = @ApplyStatus,GetDateTime = @GetDateTime,GetResult = @GetResult,GetRemark = @GetRemark,GetMan = @GetMan,ExamineDatetime = @ExamineDatetime,ExamineResult = @ExamineResult,ExamineRemark = @ExamineRemark,ExamineMan = @ExamineMan,ReportDate = @ReportDate,ReportMan = @ReportMan,ReportCode = @ReportCode,CheckDate = @CheckDate,CheckResult = @CheckResult,CheckRemark = @CheckRemark,CheckMan = @CheckMan,ConfirmDate = @ConfirmDate,ConfirmResult = @ConfirmResult,ConfirmMan = @ConfirmMan,PublicDate = @PublicDate,PublicMan = @PublicMan,PublicCode = @PublicCode,NoticeDate = @NoticeDate,NoticeMan = @NoticeMan,NoticeCode = @NoticeCode,CodeDate = @CodeDate,CodeMan = @CodeMan,CJR = @CJR,CJSJ = @CJSJ,XGR = @XGR,XGSJ = @XGSJ,""VALID"" = @Valid,Memo = @Memo,SheBaoCheck = @SheBaoCheck,OldUnitName = @OldUnitName,OldEnt_QYZJJGDM = @OldEnt_QYZJJGDM,OldUnitCheckTime = @OldUnitCheckTime,OldUnitCheckResult = @OldUnitCheckResult,OldUnitCheckRemark = @OldUnitCheckRemark,ENT_ContractType = @ENT_ContractType,ENT_ContractStartTime = @ENT_ContractStartTime,ENT_ContractENDTime = @ENT_ContractENDTime,LastBackResult = @LastBackResult,CheckZSSD = @CheckZSSD,CheckXSL = @CheckXSL,CheckCFZC = @CheckCFZC,CheckYCZC = @CheckYCZC,newUnitCheckTime = @newUnitCheckTime,newUnitCheckResult = @newUnitCheckResult,newUnitCheckRemark = @newUnitCheckRemark,Nation = @Nation,Birthday = @Birthday
			WHERE
				ApplyID = @ApplyID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, _zjs_ApplyMDL.ApplyID));
            p.Add(db.CreateParameter("ApplyType", DbType.String, _zjs_ApplyMDL.ApplyType));
            p.Add(db.CreateParameter("ApplyTypeSub", DbType.String, _zjs_ApplyMDL.ApplyTypeSub));
            p.Add(db.CreateParameter("ENT_Name", DbType.String, _zjs_ApplyMDL.ENT_Name));
            p.Add(db.CreateParameter("ENT_OrganizationsCode", DbType.String, _zjs_ApplyMDL.ENT_OrganizationsCode));
            p.Add(db.CreateParameter("ENT_City", DbType.String, _zjs_ApplyMDL.ENT_City));
            p.Add(db.CreateParameter("PSN_Name", DbType.String, _zjs_ApplyMDL.PSN_Name));
            p.Add(db.CreateParameter("PSN_Sex", DbType.String, _zjs_ApplyMDL.PSN_Sex));
            p.Add(db.CreateParameter("PSN_CertificateType", DbType.String, _zjs_ApplyMDL.PSN_CertificateType));
            p.Add(db.CreateParameter("PSN_CertificateNO", DbType.String, _zjs_ApplyMDL.PSN_CertificateNO));
            p.Add(db.CreateParameter("PSN_RegisterNo", DbType.String, _zjs_ApplyMDL.PSN_RegisterNo));
            p.Add(db.CreateParameter("PSN_RegisterCertificateNo", DbType.String, _zjs_ApplyMDL.PSN_RegisterCertificateNo));
            p.Add(db.CreateParameter("PSN_RegisteProfession", DbType.String, _zjs_ApplyMDL.PSN_RegisteProfession));
            p.Add(db.CreateParameter("ApplyTime", DbType.DateTime, _zjs_ApplyMDL.ApplyTime));
            p.Add(db.CreateParameter("ApplyCode", DbType.String, _zjs_ApplyMDL.ApplyCode));
            p.Add(db.CreateParameter("ApplyStatus", DbType.String, _zjs_ApplyMDL.ApplyStatus));
            p.Add(db.CreateParameter("GetDateTime", DbType.DateTime, _zjs_ApplyMDL.GetDateTime));
            p.Add(db.CreateParameter("GetResult", DbType.String, _zjs_ApplyMDL.GetResult));
            p.Add(db.CreateParameter("GetRemark", DbType.String, _zjs_ApplyMDL.GetRemark));
            p.Add(db.CreateParameter("GetMan", DbType.String, _zjs_ApplyMDL.GetMan));
            p.Add(db.CreateParameter("ExamineDatetime", DbType.DateTime, _zjs_ApplyMDL.ExamineDatetime));
            p.Add(db.CreateParameter("ExamineResult", DbType.String, _zjs_ApplyMDL.ExamineResult));
            p.Add(db.CreateParameter("ExamineRemark", DbType.String, _zjs_ApplyMDL.ExamineRemark));
            p.Add(db.CreateParameter("ExamineMan", DbType.String, _zjs_ApplyMDL.ExamineMan));
            p.Add(db.CreateParameter("ReportDate", DbType.DateTime, _zjs_ApplyMDL.ReportDate));
            p.Add(db.CreateParameter("ReportMan", DbType.String, _zjs_ApplyMDL.ReportMan));
            p.Add(db.CreateParameter("ReportCode", DbType.String, _zjs_ApplyMDL.ReportCode));
            p.Add(db.CreateParameter("CheckDate", DbType.DateTime, _zjs_ApplyMDL.CheckDate));
            p.Add(db.CreateParameter("CheckResult", DbType.String, _zjs_ApplyMDL.CheckResult));
            p.Add(db.CreateParameter("CheckRemark", DbType.String, _zjs_ApplyMDL.CheckRemark));
            p.Add(db.CreateParameter("CheckMan", DbType.String, _zjs_ApplyMDL.CheckMan));
            p.Add(db.CreateParameter("ConfirmDate", DbType.DateTime, _zjs_ApplyMDL.ConfirmDate));
            p.Add(db.CreateParameter("ConfirmResult", DbType.String, _zjs_ApplyMDL.ConfirmResult));
            p.Add(db.CreateParameter("ConfirmMan", DbType.String, _zjs_ApplyMDL.ConfirmMan));
            p.Add(db.CreateParameter("PublicDate", DbType.DateTime, _zjs_ApplyMDL.PublicDate));
            p.Add(db.CreateParameter("PublicMan", DbType.String, _zjs_ApplyMDL.PublicMan));
            p.Add(db.CreateParameter("PublicCode", DbType.String, _zjs_ApplyMDL.PublicCode));
            p.Add(db.CreateParameter("NoticeDate", DbType.DateTime, _zjs_ApplyMDL.NoticeDate));
            p.Add(db.CreateParameter("NoticeMan", DbType.String, _zjs_ApplyMDL.NoticeMan));
            p.Add(db.CreateParameter("NoticeCode", DbType.String, _zjs_ApplyMDL.NoticeCode));
            p.Add(db.CreateParameter("CodeDate", DbType.DateTime, _zjs_ApplyMDL.CodeDate));
            p.Add(db.CreateParameter("CodeMan", DbType.String, _zjs_ApplyMDL.CodeMan));
            p.Add(db.CreateParameter("CJR", DbType.String, _zjs_ApplyMDL.CJR));
            p.Add(db.CreateParameter("CJSJ", DbType.DateTime, _zjs_ApplyMDL.CJSJ));
            p.Add(db.CreateParameter("XGR", DbType.String, _zjs_ApplyMDL.XGR));
            p.Add(db.CreateParameter("XGSJ", DbType.DateTime, _zjs_ApplyMDL.XGSJ));
            p.Add(db.CreateParameter("Valid", DbType.Int32, _zjs_ApplyMDL.Valid));
            p.Add(db.CreateParameter("Memo", DbType.String, _zjs_ApplyMDL.Memo));
            p.Add(db.CreateParameter("SheBaoCheck", DbType.Int32, _zjs_ApplyMDL.SheBaoCheck));
            p.Add(db.CreateParameter("OldUnitName", DbType.String, _zjs_ApplyMDL.OldUnitName));
            p.Add(db.CreateParameter("OldEnt_QYZJJGDM", DbType.String, _zjs_ApplyMDL.OldEnt_QYZJJGDM));
            p.Add(db.CreateParameter("OldUnitCheckTime", DbType.DateTime, _zjs_ApplyMDL.OldUnitCheckTime));
            p.Add(db.CreateParameter("OldUnitCheckResult", DbType.String, _zjs_ApplyMDL.OldUnitCheckResult));
            p.Add(db.CreateParameter("OldUnitCheckRemark", DbType.String, _zjs_ApplyMDL.OldUnitCheckRemark));
            p.Add(db.CreateParameter("ENT_ContractType", DbType.Int32, _zjs_ApplyMDL.ENT_ContractType));
            p.Add(db.CreateParameter("ENT_ContractStartTime", DbType.DateTime, _zjs_ApplyMDL.ENT_ContractStartTime));
            p.Add(db.CreateParameter("ENT_ContractENDTime", DbType.DateTime, _zjs_ApplyMDL.ENT_ContractENDTime));
            p.Add(db.CreateParameter("LastBackResult", DbType.String, _zjs_ApplyMDL.LastBackResult));
            p.Add(db.CreateParameter("CheckZSSD", DbType.Int32, _zjs_ApplyMDL.CheckZSSD));
            p.Add(db.CreateParameter("CheckXSL", DbType.Int32, _zjs_ApplyMDL.CheckXSL));
            p.Add(db.CreateParameter("CheckCFZC", DbType.Int32, _zjs_ApplyMDL.CheckCFZC));
            p.Add(db.CreateParameter("CheckYCZC", DbType.Int32, _zjs_ApplyMDL.CheckYCZC));
            p.Add(db.CreateParameter("newUnitCheckTime", DbType.DateTime, _zjs_ApplyMDL.newUnitCheckTime));
            p.Add(db.CreateParameter("newUnitCheckResult", DbType.String, _zjs_ApplyMDL.newUnitCheckResult));
            p.Add(db.CreateParameter("newUnitCheckRemark", DbType.String, _zjs_ApplyMDL.newUnitCheckRemark));
            p.Add(db.CreateParameter("Nation", DbType.String, _zjs_ApplyMDL.Nation));
            p.Add(db.CreateParameter("Birthday", DbType.DateTime, _zjs_ApplyMDL.Birthday));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="ApplyID">主键</param>
        /// <returns></returns>
        public static int Delete(string ApplyID)
        {
            return Delete(null, ApplyID);
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ApplyID">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, string ApplyID)
        {
            string sql = @"DELETE FROM dbo.zjs_Apply WHERE ApplyID = @ApplyID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, ApplyID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_zjs_ApplyMDL">对象实体类</param>
        /// <returns></returns>
        public static int Delete(zjs_ApplyMDL _zjs_ApplyMDL)
        {
            return Delete(null, _zjs_ApplyMDL);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_zjs_ApplyMDL">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, zjs_ApplyMDL _zjs_ApplyMDL)
        {
            string sql = @"DELETE FROM dbo.zjs_Apply WHERE ApplyID = @ApplyID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, _zjs_ApplyMDL.ApplyID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ApplyID">主键</param>
        public static zjs_ApplyMDL GetObject(string ApplyID)
        {
            string sql = @"
			SELECT ApplyID,ApplyType,ApplyTypeSub,ENT_Name,ENT_OrganizationsCode,ENT_City,PSN_Name,PSN_Sex,PSN_CertificateType,PSN_CertificateNO,PSN_RegisterNo,PSN_RegisterCertificateNo,PSN_RegisteProfession,ApplyTime,ApplyCode,ApplyStatus,GetDateTime,GetResult,GetRemark,GetMan,ExamineDatetime,ExamineResult,ExamineRemark,ExamineMan,ReportDate,ReportMan,ReportCode,CheckDate,CheckResult,CheckRemark,CheckMan,ConfirmDate,ConfirmResult,ConfirmMan,PublicDate,PublicMan,PublicCode,NoticeDate,NoticeMan,NoticeCode,CodeDate,CodeMan,CJR,CJSJ,XGR,XGSJ,[VALID],Memo,SheBaoCheck,OldUnitName,OldEnt_QYZJJGDM,OldUnitCheckTime,OldUnitCheckResult,OldUnitCheckRemark,ENT_ContractType,ENT_ContractStartTime,ENT_ContractENDTime,LastBackResult,CheckZSSD,CheckXSL,CheckCFZC,CheckYCZC,newUnitCheckTime,newUnitCheckResult,newUnitCheckRemark,Nation,Birthday
			FROM dbo.zjs_Apply
			WHERE ApplyID = @ApplyID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, ApplyID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                zjs_ApplyMDL _zjs_ApplyMDL = null;
                if (reader.Read())
                {
                    _zjs_ApplyMDL = new zjs_ApplyMDL();
                    if (reader["ApplyID"] != DBNull.Value) _zjs_ApplyMDL.ApplyID = Convert.ToString(reader["ApplyID"]);
                    if (reader["ApplyType"] != DBNull.Value) _zjs_ApplyMDL.ApplyType = Convert.ToString(reader["ApplyType"]);
                    if (reader["ApplyTypeSub"] != DBNull.Value) _zjs_ApplyMDL.ApplyTypeSub = Convert.ToString(reader["ApplyTypeSub"]);
                    if (reader["ENT_Name"] != DBNull.Value) _zjs_ApplyMDL.ENT_Name = Convert.ToString(reader["ENT_Name"]);
                    if (reader["ENT_OrganizationsCode"] != DBNull.Value) _zjs_ApplyMDL.ENT_OrganizationsCode = Convert.ToString(reader["ENT_OrganizationsCode"]);
                    if (reader["ENT_City"] != DBNull.Value) _zjs_ApplyMDL.ENT_City = Convert.ToString(reader["ENT_City"]);
                    if (reader["PSN_Name"] != DBNull.Value) _zjs_ApplyMDL.PSN_Name = Convert.ToString(reader["PSN_Name"]);
                    if (reader["PSN_Sex"] != DBNull.Value) _zjs_ApplyMDL.PSN_Sex = Convert.ToString(reader["PSN_Sex"]);
                    if (reader["PSN_CertificateType"] != DBNull.Value) _zjs_ApplyMDL.PSN_CertificateType = Convert.ToString(reader["PSN_CertificateType"]);
                    if (reader["PSN_CertificateNO"] != DBNull.Value) _zjs_ApplyMDL.PSN_CertificateNO = Convert.ToString(reader["PSN_CertificateNO"]);
                    if (reader["PSN_RegisterNo"] != DBNull.Value) _zjs_ApplyMDL.PSN_RegisterNo = Convert.ToString(reader["PSN_RegisterNo"]);
                    if (reader["PSN_RegisterCertificateNo"] != DBNull.Value) _zjs_ApplyMDL.PSN_RegisterCertificateNo = Convert.ToString(reader["PSN_RegisterCertificateNo"]);
                    if (reader["PSN_RegisteProfession"] != DBNull.Value) _zjs_ApplyMDL.PSN_RegisteProfession = Convert.ToString(reader["PSN_RegisteProfession"]);
                    if (reader["ApplyTime"] != DBNull.Value) _zjs_ApplyMDL.ApplyTime = Convert.ToDateTime(reader["ApplyTime"]);
                    if (reader["ApplyCode"] != DBNull.Value) _zjs_ApplyMDL.ApplyCode = Convert.ToString(reader["ApplyCode"]);
                    if (reader["ApplyStatus"] != DBNull.Value) _zjs_ApplyMDL.ApplyStatus = Convert.ToString(reader["ApplyStatus"]);
                    if (reader["GetDateTime"] != DBNull.Value) _zjs_ApplyMDL.GetDateTime = Convert.ToDateTime(reader["GetDateTime"]);
                    if (reader["GetResult"] != DBNull.Value) _zjs_ApplyMDL.GetResult = Convert.ToString(reader["GetResult"]);
                    if (reader["GetRemark"] != DBNull.Value) _zjs_ApplyMDL.GetRemark = Convert.ToString(reader["GetRemark"]);
                    if (reader["GetMan"] != DBNull.Value) _zjs_ApplyMDL.GetMan = Convert.ToString(reader["GetMan"]);
                    if (reader["ExamineDatetime"] != DBNull.Value) _zjs_ApplyMDL.ExamineDatetime = Convert.ToDateTime(reader["ExamineDatetime"]);
                    if (reader["ExamineResult"] != DBNull.Value) _zjs_ApplyMDL.ExamineResult = Convert.ToString(reader["ExamineResult"]);
                    if (reader["ExamineRemark"] != DBNull.Value) _zjs_ApplyMDL.ExamineRemark = Convert.ToString(reader["ExamineRemark"]);
                    if (reader["ExamineMan"] != DBNull.Value) _zjs_ApplyMDL.ExamineMan = Convert.ToString(reader["ExamineMan"]);
                    if (reader["ReportDate"] != DBNull.Value) _zjs_ApplyMDL.ReportDate = Convert.ToDateTime(reader["ReportDate"]);
                    if (reader["ReportMan"] != DBNull.Value) _zjs_ApplyMDL.ReportMan = Convert.ToString(reader["ReportMan"]);
                    if (reader["ReportCode"] != DBNull.Value) _zjs_ApplyMDL.ReportCode = Convert.ToString(reader["ReportCode"]);
                    if (reader["CheckDate"] != DBNull.Value) _zjs_ApplyMDL.CheckDate = Convert.ToDateTime(reader["CheckDate"]);
                    if (reader["CheckResult"] != DBNull.Value) _zjs_ApplyMDL.CheckResult = Convert.ToString(reader["CheckResult"]);
                    if (reader["CheckRemark"] != DBNull.Value) _zjs_ApplyMDL.CheckRemark = Convert.ToString(reader["CheckRemark"]);
                    if (reader["CheckMan"] != DBNull.Value) _zjs_ApplyMDL.CheckMan = Convert.ToString(reader["CheckMan"]);
                    if (reader["ConfirmDate"] != DBNull.Value) _zjs_ApplyMDL.ConfirmDate = Convert.ToDateTime(reader["ConfirmDate"]);
                    if (reader["ConfirmResult"] != DBNull.Value) _zjs_ApplyMDL.ConfirmResult = Convert.ToString(reader["ConfirmResult"]);
                    if (reader["ConfirmMan"] != DBNull.Value) _zjs_ApplyMDL.ConfirmMan = Convert.ToString(reader["ConfirmMan"]);
                    if (reader["PublicDate"] != DBNull.Value) _zjs_ApplyMDL.PublicDate = Convert.ToDateTime(reader["PublicDate"]);
                    if (reader["PublicMan"] != DBNull.Value) _zjs_ApplyMDL.PublicMan = Convert.ToString(reader["PublicMan"]);
                    if (reader["PublicCode"] != DBNull.Value) _zjs_ApplyMDL.PublicCode = Convert.ToString(reader["PublicCode"]);
                    if (reader["NoticeDate"] != DBNull.Value) _zjs_ApplyMDL.NoticeDate = Convert.ToDateTime(reader["NoticeDate"]);
                    if (reader["NoticeMan"] != DBNull.Value) _zjs_ApplyMDL.NoticeMan = Convert.ToString(reader["NoticeMan"]);
                    if (reader["NoticeCode"] != DBNull.Value) _zjs_ApplyMDL.NoticeCode = Convert.ToString(reader["NoticeCode"]);
                    if (reader["CodeDate"] != DBNull.Value) _zjs_ApplyMDL.CodeDate = Convert.ToDateTime(reader["CodeDate"]);
                    if (reader["CodeMan"] != DBNull.Value) _zjs_ApplyMDL.CodeMan = Convert.ToString(reader["CodeMan"]);
                    if (reader["CJR"] != DBNull.Value) _zjs_ApplyMDL.CJR = Convert.ToString(reader["CJR"]);
                    if (reader["CJSJ"] != DBNull.Value) _zjs_ApplyMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
                    if (reader["XGR"] != DBNull.Value) _zjs_ApplyMDL.XGR = Convert.ToString(reader["XGR"]);
                    if (reader["XGSJ"] != DBNull.Value) _zjs_ApplyMDL.XGSJ = Convert.ToDateTime(reader["XGSJ"]);
                    if (reader["Valid"] != DBNull.Value) _zjs_ApplyMDL.Valid = Convert.ToInt32(reader["Valid"]);
                    if (reader["Memo"] != DBNull.Value) _zjs_ApplyMDL.Memo = Convert.ToString(reader["Memo"]);
                    if (reader["SheBaoCheck"] != DBNull.Value) _zjs_ApplyMDL.SheBaoCheck = Convert.ToInt32(reader["SheBaoCheck"]);
                    if (reader["OldUnitName"] != DBNull.Value) _zjs_ApplyMDL.OldUnitName = Convert.ToString(reader["OldUnitName"]);
                    if (reader["OldEnt_QYZJJGDM"] != DBNull.Value) _zjs_ApplyMDL.OldEnt_QYZJJGDM = Convert.ToString(reader["OldEnt_QYZJJGDM"]);
                    if (reader["OldUnitCheckTime"] != DBNull.Value) _zjs_ApplyMDL.OldUnitCheckTime = Convert.ToDateTime(reader["OldUnitCheckTime"]);
                    if (reader["OldUnitCheckResult"] != DBNull.Value) _zjs_ApplyMDL.OldUnitCheckResult = Convert.ToString(reader["OldUnitCheckResult"]);
                    if (reader["OldUnitCheckRemark"] != DBNull.Value) _zjs_ApplyMDL.OldUnitCheckRemark = Convert.ToString(reader["OldUnitCheckRemark"]);
                    if (reader["ENT_ContractType"] != DBNull.Value) _zjs_ApplyMDL.ENT_ContractType = Convert.ToInt32(reader["ENT_ContractType"]);
                    if (reader["ENT_ContractStartTime"] != DBNull.Value) _zjs_ApplyMDL.ENT_ContractStartTime = Convert.ToDateTime(reader["ENT_ContractStartTime"]);
                    if (reader["ENT_ContractENDTime"] != DBNull.Value) _zjs_ApplyMDL.ENT_ContractENDTime = Convert.ToDateTime(reader["ENT_ContractENDTime"]);
                    if (reader["LastBackResult"] != DBNull.Value) _zjs_ApplyMDL.LastBackResult = Convert.ToString(reader["LastBackResult"]);
                    if (reader["CheckZSSD"] != DBNull.Value) _zjs_ApplyMDL.CheckZSSD = Convert.ToInt32(reader["CheckZSSD"]);
                    if (reader["CheckXSL"] != DBNull.Value) _zjs_ApplyMDL.CheckXSL = Convert.ToInt32(reader["CheckXSL"]);
                    if (reader["CheckCFZC"] != DBNull.Value) _zjs_ApplyMDL.CheckCFZC = Convert.ToInt32(reader["CheckCFZC"]);
                    if (reader["CheckYCZC"] != DBNull.Value) _zjs_ApplyMDL.CheckYCZC = Convert.ToInt32(reader["CheckYCZC"]);
                    if (reader["newUnitCheckTime"] != DBNull.Value) _zjs_ApplyMDL.newUnitCheckTime = Convert.ToDateTime(reader["newUnitCheckTime"]);
                    if (reader["newUnitCheckResult"] != DBNull.Value) _zjs_ApplyMDL.newUnitCheckResult = Convert.ToString(reader["newUnitCheckResult"]);
                    if (reader["newUnitCheckRemark"] != DBNull.Value) _zjs_ApplyMDL.newUnitCheckRemark = Convert.ToString(reader["newUnitCheckRemark"]);
                    if (reader["Nation"] != DBNull.Value) _zjs_ApplyMDL.Nation = Convert.ToString(reader["Nation"]);
                    if (reader["Birthday"] != DBNull.Value) _zjs_ApplyMDL.Birthday = Convert.ToDateTime(reader["Birthday"]);
                }
                reader.Close();
                db.Close();
                return _zjs_ApplyMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.zjs_Apply", "*", filterWhereString, orderBy == "" ? " [XGSJ] desc,[ApplyID]" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.zjs_Apply", filterWhereString);
        }

        #region 自定义方法

        /// <summary>
        /// 获取人员填写了最新劳动合同时间的注册申请
        /// </summary>
        /// <param name="ENT_OrganizationsCode">社会统一信用代码</param>
        /// <returns>申请单实体对象</returns>
        public static zjs_ApplyMDL GetObjectLastContract(string ENT_OrganizationsCode)
        {
            string sql = @"
			SELECT top 1 ApplyID,ApplyType,ApplyTypeSub,ENT_Name,ENT_OrganizationsCode,ENT_City,PSN_Name,PSN_Sex,PSN_CertificateType,PSN_CertificateNO,PSN_RegisterNo,PSN_RegisterCertificateNo,PSN_RegisteProfession,ApplyTime,ApplyCode,ApplyStatus,GetDateTime,GetResult,GetRemark,GetMan,ExamineDatetime,ExamineResult,ExamineRemark,ExamineMan,ReportDate,ReportMan,ReportCode,CheckDate,CheckResult,CheckRemark,CheckMan,ConfirmDate,ConfirmResult,ConfirmMan,PublicDate,PublicMan,PublicCode,NoticeDate,NoticeMan,NoticeCode,CodeDate,CodeMan,CJR,CJSJ,XGR,XGSJ,[VALID],Memo,SheBaoCheck,OldUnitName,OldEnt_QYZJJGDM,OldUnitCheckTime,OldUnitCheckResult,OldUnitCheckRemark,ENT_ContractType,ENT_ContractStartTime,ENT_ContractENDTime,LastBackResult,CheckZSSD,CheckXSL,CheckCFZC,CheckYCZC,newUnitCheckTime,newUnitCheckResult,newUnitCheckRemark,Nation,Birthday
			FROM dbo.zjs_Apply
			WHERE ENT_OrganizationsCode = @ENT_OrganizationsCode and ENT_ContractStartTime is not null
            order by ApplyTime desc";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ENT_OrganizationsCode", DbType.String, ENT_OrganizationsCode));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                zjs_ApplyMDL _zjs_ApplyMDL = null;
                if (reader.Read())
                {
                    _zjs_ApplyMDL = new zjs_ApplyMDL();
                    if (reader["ApplyID"] != DBNull.Value) _zjs_ApplyMDL.ApplyID = Convert.ToString(reader["ApplyID"]);
                    if (reader["ApplyType"] != DBNull.Value) _zjs_ApplyMDL.ApplyType = Convert.ToString(reader["ApplyType"]);
                    if (reader["ApplyTypeSub"] != DBNull.Value) _zjs_ApplyMDL.ApplyTypeSub = Convert.ToString(reader["ApplyTypeSub"]);
                    if (reader["ENT_Name"] != DBNull.Value) _zjs_ApplyMDL.ENT_Name = Convert.ToString(reader["ENT_Name"]);
                    if (reader["ENT_OrganizationsCode"] != DBNull.Value) _zjs_ApplyMDL.ENT_OrganizationsCode = Convert.ToString(reader["ENT_OrganizationsCode"]);
                    if (reader["ENT_City"] != DBNull.Value) _zjs_ApplyMDL.ENT_City = Convert.ToString(reader["ENT_City"]);
                    if (reader["PSN_Name"] != DBNull.Value) _zjs_ApplyMDL.PSN_Name = Convert.ToString(reader["PSN_Name"]);
                    if (reader["PSN_Sex"] != DBNull.Value) _zjs_ApplyMDL.PSN_Sex = Convert.ToString(reader["PSN_Sex"]);
                    if (reader["PSN_CertificateType"] != DBNull.Value) _zjs_ApplyMDL.PSN_CertificateType = Convert.ToString(reader["PSN_CertificateType"]);
                    if (reader["PSN_CertificateNO"] != DBNull.Value) _zjs_ApplyMDL.PSN_CertificateNO = Convert.ToString(reader["PSN_CertificateNO"]);
                    if (reader["PSN_RegisterNo"] != DBNull.Value) _zjs_ApplyMDL.PSN_RegisterNo = Convert.ToString(reader["PSN_RegisterNo"]);
                    if (reader["PSN_RegisterCertificateNo"] != DBNull.Value) _zjs_ApplyMDL.PSN_RegisterCertificateNo = Convert.ToString(reader["PSN_RegisterCertificateNo"]);
                    if (reader["PSN_RegisteProfession"] != DBNull.Value) _zjs_ApplyMDL.PSN_RegisteProfession = Convert.ToString(reader["PSN_RegisteProfession"]);
                    if (reader["ApplyTime"] != DBNull.Value) _zjs_ApplyMDL.ApplyTime = Convert.ToDateTime(reader["ApplyTime"]);
                    if (reader["ApplyCode"] != DBNull.Value) _zjs_ApplyMDL.ApplyCode = Convert.ToString(reader["ApplyCode"]);
                    if (reader["ApplyStatus"] != DBNull.Value) _zjs_ApplyMDL.ApplyStatus = Convert.ToString(reader["ApplyStatus"]);
                    if (reader["GetDateTime"] != DBNull.Value) _zjs_ApplyMDL.GetDateTime = Convert.ToDateTime(reader["GetDateTime"]);
                    if (reader["GetResult"] != DBNull.Value) _zjs_ApplyMDL.GetResult = Convert.ToString(reader["GetResult"]);
                    if (reader["GetRemark"] != DBNull.Value) _zjs_ApplyMDL.GetRemark = Convert.ToString(reader["GetRemark"]);
                    if (reader["GetMan"] != DBNull.Value) _zjs_ApplyMDL.GetMan = Convert.ToString(reader["GetMan"]);
                    if (reader["ExamineDatetime"] != DBNull.Value) _zjs_ApplyMDL.ExamineDatetime = Convert.ToDateTime(reader["ExamineDatetime"]);
                    if (reader["ExamineResult"] != DBNull.Value) _zjs_ApplyMDL.ExamineResult = Convert.ToString(reader["ExamineResult"]);
                    if (reader["ExamineRemark"] != DBNull.Value) _zjs_ApplyMDL.ExamineRemark = Convert.ToString(reader["ExamineRemark"]);
                    if (reader["ExamineMan"] != DBNull.Value) _zjs_ApplyMDL.ExamineMan = Convert.ToString(reader["ExamineMan"]);
                    if (reader["ReportDate"] != DBNull.Value) _zjs_ApplyMDL.ReportDate = Convert.ToDateTime(reader["ReportDate"]);
                    if (reader["ReportMan"] != DBNull.Value) _zjs_ApplyMDL.ReportMan = Convert.ToString(reader["ReportMan"]);
                    if (reader["ReportCode"] != DBNull.Value) _zjs_ApplyMDL.ReportCode = Convert.ToString(reader["ReportCode"]);
                    if (reader["CheckDate"] != DBNull.Value) _zjs_ApplyMDL.CheckDate = Convert.ToDateTime(reader["CheckDate"]);
                    if (reader["CheckResult"] != DBNull.Value) _zjs_ApplyMDL.CheckResult = Convert.ToString(reader["CheckResult"]);
                    if (reader["CheckRemark"] != DBNull.Value) _zjs_ApplyMDL.CheckRemark = Convert.ToString(reader["CheckRemark"]);
                    if (reader["CheckMan"] != DBNull.Value) _zjs_ApplyMDL.CheckMan = Convert.ToString(reader["CheckMan"]);
                    if (reader["ConfirmDate"] != DBNull.Value) _zjs_ApplyMDL.ConfirmDate = Convert.ToDateTime(reader["ConfirmDate"]);
                    if (reader["ConfirmResult"] != DBNull.Value) _zjs_ApplyMDL.ConfirmResult = Convert.ToString(reader["ConfirmResult"]);
                    if (reader["ConfirmMan"] != DBNull.Value) _zjs_ApplyMDL.ConfirmMan = Convert.ToString(reader["ConfirmMan"]);
                    if (reader["PublicDate"] != DBNull.Value) _zjs_ApplyMDL.PublicDate = Convert.ToDateTime(reader["PublicDate"]);
                    if (reader["PublicMan"] != DBNull.Value) _zjs_ApplyMDL.PublicMan = Convert.ToString(reader["PublicMan"]);
                    if (reader["PublicCode"] != DBNull.Value) _zjs_ApplyMDL.PublicCode = Convert.ToString(reader["PublicCode"]);
                    if (reader["NoticeDate"] != DBNull.Value) _zjs_ApplyMDL.NoticeDate = Convert.ToDateTime(reader["NoticeDate"]);
                    if (reader["NoticeMan"] != DBNull.Value) _zjs_ApplyMDL.NoticeMan = Convert.ToString(reader["NoticeMan"]);
                    if (reader["NoticeCode"] != DBNull.Value) _zjs_ApplyMDL.NoticeCode = Convert.ToString(reader["NoticeCode"]);
                    if (reader["CodeDate"] != DBNull.Value) _zjs_ApplyMDL.CodeDate = Convert.ToDateTime(reader["CodeDate"]);
                    if (reader["CodeMan"] != DBNull.Value) _zjs_ApplyMDL.CodeMan = Convert.ToString(reader["CodeMan"]);
                    if (reader["CJR"] != DBNull.Value) _zjs_ApplyMDL.CJR = Convert.ToString(reader["CJR"]);
                    if (reader["CJSJ"] != DBNull.Value) _zjs_ApplyMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
                    if (reader["XGR"] != DBNull.Value) _zjs_ApplyMDL.XGR = Convert.ToString(reader["XGR"]);
                    if (reader["XGSJ"] != DBNull.Value) _zjs_ApplyMDL.XGSJ = Convert.ToDateTime(reader["XGSJ"]);
                    if (reader["Valid"] != DBNull.Value) _zjs_ApplyMDL.Valid = Convert.ToInt32(reader["Valid"]);
                    if (reader["Memo"] != DBNull.Value) _zjs_ApplyMDL.Memo = Convert.ToString(reader["Memo"]);
                    if (reader["SheBaoCheck"] != DBNull.Value) _zjs_ApplyMDL.SheBaoCheck = Convert.ToInt32(reader["SheBaoCheck"]);
                    if (reader["OldUnitName"] != DBNull.Value) _zjs_ApplyMDL.OldUnitName = Convert.ToString(reader["OldUnitName"]);
                    if (reader["OldEnt_QYZJJGDM"] != DBNull.Value) _zjs_ApplyMDL.OldEnt_QYZJJGDM = Convert.ToString(reader["OldEnt_QYZJJGDM"]);
                    if (reader["OldUnitCheckTime"] != DBNull.Value) _zjs_ApplyMDL.OldUnitCheckTime = Convert.ToDateTime(reader["OldUnitCheckTime"]);
                    if (reader["OldUnitCheckResult"] != DBNull.Value) _zjs_ApplyMDL.OldUnitCheckResult = Convert.ToString(reader["OldUnitCheckResult"]);
                    if (reader["OldUnitCheckRemark"] != DBNull.Value) _zjs_ApplyMDL.OldUnitCheckRemark = Convert.ToString(reader["OldUnitCheckRemark"]);
                    if (reader["ENT_ContractType"] != DBNull.Value) _zjs_ApplyMDL.ENT_ContractType = Convert.ToInt32(reader["ENT_ContractType"]);
                    if (reader["ENT_ContractStartTime"] != DBNull.Value) _zjs_ApplyMDL.ENT_ContractStartTime = Convert.ToDateTime(reader["ENT_ContractStartTime"]);
                    if (reader["ENT_ContractENDTime"] != DBNull.Value) _zjs_ApplyMDL.ENT_ContractENDTime = Convert.ToDateTime(reader["ENT_ContractENDTime"]);
                    if (reader["LastBackResult"] != DBNull.Value) _zjs_ApplyMDL.LastBackResult = Convert.ToString(reader["LastBackResult"]);
                    if (reader["CheckZSSD"] != DBNull.Value) _zjs_ApplyMDL.CheckZSSD = Convert.ToInt32(reader["CheckZSSD"]);
                    if (reader["CheckXSL"] != DBNull.Value) _zjs_ApplyMDL.CheckXSL = Convert.ToInt32(reader["CheckXSL"]);
                    if (reader["CheckCFZC"] != DBNull.Value) _zjs_ApplyMDL.CheckCFZC = Convert.ToInt32(reader["CheckCFZC"]);
                    if (reader["CheckYCZC"] != DBNull.Value) _zjs_ApplyMDL.CheckYCZC = Convert.ToInt32(reader["CheckYCZC"]);
                    if (reader["newUnitCheckTime"] != DBNull.Value) _zjs_ApplyMDL.newUnitCheckTime = Convert.ToDateTime(reader["newUnitCheckTime"]);
                    if (reader["newUnitCheckResult"] != DBNull.Value) _zjs_ApplyMDL.newUnitCheckResult = Convert.ToString(reader["newUnitCheckResult"]);
                    if (reader["newUnitCheckRemark"] != DBNull.Value) _zjs_ApplyMDL.newUnitCheckRemark = Convert.ToString(reader["newUnitCheckRemark"]);
                    if (reader["Nation"] != DBNull.Value) _zjs_ApplyMDL.Nation = Convert.ToString(reader["Nation"]);
                    if (reader["Birthday"] != DBNull.Value) _zjs_ApplyMDL.Birthday = Convert.ToDateTime(reader["Birthday"]);
                }
                reader.Close();
                db.Close();
                return _zjs_ApplyMDL;
            }
        }

        /// <summary>
        /// 根据专业1申请单ID，复制到专业2申请单
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="ZhuanYe1_ApplyID">专业1申请单ID</param>
        /// <param name="ApplyID">专业2申请单ID</param>
        /// <param name="PSN_RegisterNo">专业2注册编号</param>
        /// <param name="PSN_RegisteProfession">专业2名称</param>
        /// <returns></returns>
        public static int InsertZhuanYe2ApplyWithZhuanYe1ApplyID(DbTransaction tran, string ZhuanYe1_ApplyID, string ApplyID, string PSN_RegisterNo, string PSN_RegisteProfession)
        {
            string sql = @"INSERT INTO [dbo].[zjs_Apply]([ApplyID],[ApplyType],[ApplyTypeSub],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[PSN_Name],[PSN_Sex],[PSN_CertificateType],[PSN_CertificateNO],[PSN_RegisterNo],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[ApplyTime],[ApplyCode],[ApplyStatus],[GetDateTime],[GetResult],[GetRemark],[GetMan],[ExamineDatetime],[ExamineResult],[ExamineRemark],[ExamineMan],[ReportDate],[ReportMan],[ReportCode],[CheckDate],[CheckResult],[CheckRemark],[CheckMan],[ConfirmDate],[ConfirmResult],[ConfirmMan],[PublicDate],[PublicMan],[PublicCode],[NoticeDate],[NoticeMan],[NoticeCode],[CodeDate],[CodeMan],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[SheBaoCheck],[OldUnitName],[OldEnt_QYZJJGDM],[OldUnitCheckTime],[OldUnitCheckResult],[OldUnitCheckRemark],[ENT_ContractType],[ENT_ContractStartTime],[ENT_ContractENDTime],[LastBackResult],[CheckZSSD],[CheckXSL],[CheckCFZC],[CheckYCZC],[newUnitCheckTime],[newUnitCheckResult],[newUnitCheckRemark],[Nation],[Birthday]) 
                                                SELECT @ApplyID,[ApplyType],[ApplyTypeSub],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[PSN_Name],[PSN_Sex],[PSN_CertificateType],[PSN_CertificateNO],@PSN_RegisterNo,[PSN_RegisterCertificateNo],@PSN_RegisteProfession,[ApplyTime],[ApplyCode],[ApplyStatus],[GetDateTime],[GetResult],[GetRemark],[GetMan],[ExamineDatetime],[ExamineResult],[ExamineRemark],[ExamineMan],[ReportDate],[ReportMan],[ReportCode],[CheckDate],[CheckResult],[CheckRemark],[CheckMan],[ConfirmDate],[ConfirmResult],[ConfirmMan],[PublicDate],[PublicMan],[PublicCode],[NoticeDate],[NoticeMan],[NoticeCode],[CodeDate],[CodeMan],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[SheBaoCheck],[OldUnitName],[OldEnt_QYZJJGDM],[OldUnitCheckTime],[OldUnitCheckResult],[OldUnitCheckRemark],[ENT_ContractType],[ENT_ContractStartTime],[ENT_ContractENDTime],[LastBackResult],[CheckZSSD],[CheckXSL],[CheckCFZC],[CheckYCZC],[newUnitCheckTime],[newUnitCheckResult],[newUnitCheckRemark],[Nation],[Birthday]  
                                                FROM [dbo].[zjs_Apply]
                                                WHERE ApplyID = @ZhuanYe1_ApplyID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ZhuanYe1_ApplyID", DbType.String, ZhuanYe1_ApplyID));
            p.Add(db.CreateParameter("ApplyID", DbType.String, ApplyID));
            p.Add(db.CreateParameter("PSN_RegisterNo", DbType.String, PSN_RegisterNo));
            p.Add(db.CreateParameter("PSN_RegisteProfession", DbType.String, PSN_RegisteProfession));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        ///  根据专业1申请单ID，更新到专业2申请单
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ApplyID1">专业1申请单ID</param>
        /// <param name="ApplyID2">专业2申请单ID</param>
        /// <returns></returns>
        public static int UpdateZhuanYe2ApplyWithZhuanYe1ApplyID(DbTransaction tran, string ApplyID1, string ApplyID2)
        {
            string sql = @"UPDATE a2
                                set 
                                a2.[ApplyType]=a1.[ApplyType],
                                a2.[ApplyTypeSub]=a1.[ApplyTypeSub],
                                a2.[ENT_Name]=a1.[ENT_Name],
                                a2.[ENT_OrganizationsCode]=a1.[ENT_OrganizationsCode],
                                a2.[ENT_City]=a1.[ENT_City],
                                a2.[PSN_Name]=a1.[PSN_Name],
                                a2.[PSN_Sex]=a1.[PSN_Sex],
                                a2.[PSN_CertificateType]=a1.[PSN_CertificateType],
                                a2.[PSN_CertificateNO]=a1.[PSN_CertificateNO],
                                a2.[ApplyTime]=a1.[ApplyTime],
                                a2.[ApplyCode]=a1.[ApplyCode],
                                a2.[ApplyStatus]=a1.[ApplyStatus],
                                a2.[GetDateTime]=a1.[GetDateTime],
                                a2.[GetResult]=a1.[GetResult],
                                a2.[GetRemark]=a1.[GetRemark],
                                a2.[GetMan]=a1.[GetMan],
                                a2.[ExamineDatetime]=a1.[ExamineDatetime],
                                a2.[ExamineResult]=a1.[ExamineResult],
                                a2.[ExamineRemark]=a1.[ExamineRemark],
                                a2.[ExamineMan]=a1.[ExamineMan],
                                a2.[ReportDate]=a1.[ReportDate],
                                a2.[ReportMan]=a1.[ReportMan],
                                a2.[ReportCode]=a1.[ReportCode],
                                a2.[CheckDate]=a1.[CheckDate],
                                a2.[CheckResult]=a1.[CheckResult],
                                a2.[CheckRemark]=a1.[CheckRemark],
                                a2.[CheckMan]=a1.[CheckMan],
                                a2.[ConfirmDate]=a1.[ConfirmDate],
                                a2.[ConfirmResult]=a1.[ConfirmResult],
                                a2.[ConfirmMan]=a1.[ConfirmMan],
                                a2.[PublicDate]=a1.[PublicDate],
                                a2.[PublicMan]=a1.[PublicMan],
                                a2.[PublicCode]=a1.[PublicCode],
                                a2.[NoticeDate]=a1.[NoticeDate],
                                a2.[NoticeMan]=a1.[NoticeMan],
                                a2.[NoticeCode]=a1.[NoticeCode],
                                a2.[CodeDate]=a1.[CodeDate],
                                a2.[CodeMan]=a1.[CodeMan],
                                a2.[CJR]=a1.[CJR],
                                a2.[CJSJ]=a1.[CJSJ],
                                a2.[XGR]=a1.[XGR],
                                a2.[XGSJ]=a1.[XGSJ],
                                a2.[Valid]=a1.[Valid],
                                a2.[Memo]=a1.[Memo],
                                a2.[SheBaoCheck]=a1.[SheBaoCheck],
                                a2.[OldUnitName]=a1.[OldUnitName],
                                a2.[OldEnt_QYZJJGDM]=a1.[OldEnt_QYZJJGDM],
                                a2.[OldUnitCheckTime]=a1.[OldUnitCheckTime],
                                a2.[OldUnitCheckResult]=a1.[OldUnitCheckResult],
                                a2.[OldUnitCheckRemark]=a1.[OldUnitCheckRemark],
                                a2.[ENT_ContractType]=a1.[ENT_ContractType],
                                a2.[ENT_ContractStartTime]=a1.[ENT_ContractStartTime],
                                a2.[ENT_ContractENDTime]=a1.[ENT_ContractENDTime],
                                a2.[LastBackResult]=a1.[LastBackResult],
                                a2.[CheckZSSD]=a1.[CheckZSSD],
                                a2.[CheckXSL]=a1.[CheckXSL],
                                a2.[CheckCFZC]=a1.[CheckCFZC],
                                a2.[CheckYCZC]=a1.[CheckYCZC],
                                a2.[newUnitCheckTime]=a1.[newUnitCheckTime],
                                a2.[newUnitCheckResult]=a1.[newUnitCheckResult],
                                a2.[newUnitCheckRemark]=a1.[newUnitCheckRemark],
                                a2.[Nation]=a1.[Nation],
                                a2.[Birthday]=a1.[Birthday]
                             from dbo.zjs_Apply a1
                             inner join  dbo.zjs_Apply a2 on a1.ApplyID=@ApplyID1 and a2.ApplyID=@ApplyID2 and a1.PSN_CertificateNO = a2.PSN_CertificateNO 
                             WHERE a1.ApplyID = @ApplyID1";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID1", DbType.String, ApplyID1));
            p.Add(db.CreateParameter("ApplyID2", DbType.String, ApplyID2));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 获取执业企业变更申请单对象集合（多专业根据ApplyID去找申请单批次号ApplyCode，再根据批次号取出全部全部申请单），注意按注册编号顺序排序，以便保证从任意一个申请单进入获取结果集合，排序一致。
        /// </summary>
        /// <param name="ApplyID">申请ID</param>
        /// <returns>申请单集合</returns>
        public static List<zjs_ApplyMDL> GetListObjectChangeUnit(string ApplyID)
        {
            string sql = @"
			SELECT ApplyID,ApplyType,ApplyTypeSub,ENT_Name,ENT_OrganizationsCode,ENT_City,PSN_Name,PSN_Sex,PSN_CertificateType,PSN_CertificateNO,PSN_RegisterNo,PSN_RegisterCertificateNo,PSN_RegisteProfession,ApplyTime,ApplyCode,ApplyStatus,GetDateTime,GetResult,GetRemark,GetMan,ExamineDatetime,ExamineResult,ExamineRemark,ExamineMan,ReportDate,ReportMan,ReportCode,CheckDate,CheckResult,CheckRemark,CheckMan,ConfirmDate,ConfirmResult,ConfirmMan,PublicDate,PublicMan,PublicCode,NoticeDate,NoticeMan,NoticeCode,CodeDate,CodeMan,CJR,CJSJ,XGR,XGSJ,[VALID],Memo,SheBaoCheck,OldUnitName,OldEnt_QYZJJGDM,OldUnitCheckTime,OldUnitCheckResult,OldUnitCheckRemark,ENT_ContractType,ENT_ContractStartTime,ENT_ContractENDTime,LastBackResult,CheckZSSD,CheckXSL,CheckCFZC,CheckYCZC,newUnitCheckTime,newUnitCheckResult,newUnitCheckRemark,Nation,Birthday
			FROM dbo.zjs_Apply
            Where  ApplyCode=(select ApplyCode from dbo.zjs_Apply WHERE ApplyID = @ApplyID) order by PSN_RegisterNo";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, ApplyID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                List<zjs_ApplyMDL> list = new List<zjs_ApplyMDL>();
                zjs_ApplyMDL _zjs_ApplyMDL = null;
                while (reader.Read())
                {
                    _zjs_ApplyMDL = new zjs_ApplyMDL();
                    if (reader["ApplyID"] != DBNull.Value) _zjs_ApplyMDL.ApplyID = Convert.ToString(reader["ApplyID"]);
                    if (reader["ApplyType"] != DBNull.Value) _zjs_ApplyMDL.ApplyType = Convert.ToString(reader["ApplyType"]);
                    if (reader["ApplyTypeSub"] != DBNull.Value) _zjs_ApplyMDL.ApplyTypeSub = Convert.ToString(reader["ApplyTypeSub"]);
                    if (reader["ENT_Name"] != DBNull.Value) _zjs_ApplyMDL.ENT_Name = Convert.ToString(reader["ENT_Name"]);
                    if (reader["ENT_OrganizationsCode"] != DBNull.Value) _zjs_ApplyMDL.ENT_OrganizationsCode = Convert.ToString(reader["ENT_OrganizationsCode"]);
                    if (reader["ENT_City"] != DBNull.Value) _zjs_ApplyMDL.ENT_City = Convert.ToString(reader["ENT_City"]);
                    if (reader["PSN_Name"] != DBNull.Value) _zjs_ApplyMDL.PSN_Name = Convert.ToString(reader["PSN_Name"]);
                    if (reader["PSN_Sex"] != DBNull.Value) _zjs_ApplyMDL.PSN_Sex = Convert.ToString(reader["PSN_Sex"]);
                    if (reader["PSN_CertificateType"] != DBNull.Value) _zjs_ApplyMDL.PSN_CertificateType = Convert.ToString(reader["PSN_CertificateType"]);
                    if (reader["PSN_CertificateNO"] != DBNull.Value) _zjs_ApplyMDL.PSN_CertificateNO = Convert.ToString(reader["PSN_CertificateNO"]);
                    if (reader["PSN_RegisterNo"] != DBNull.Value) _zjs_ApplyMDL.PSN_RegisterNo = Convert.ToString(reader["PSN_RegisterNo"]);
                    if (reader["PSN_RegisterCertificateNo"] != DBNull.Value) _zjs_ApplyMDL.PSN_RegisterCertificateNo = Convert.ToString(reader["PSN_RegisterCertificateNo"]);
                    if (reader["PSN_RegisteProfession"] != DBNull.Value) _zjs_ApplyMDL.PSN_RegisteProfession = Convert.ToString(reader["PSN_RegisteProfession"]);
                    if (reader["ApplyTime"] != DBNull.Value) _zjs_ApplyMDL.ApplyTime = Convert.ToDateTime(reader["ApplyTime"]);
                    if (reader["ApplyCode"] != DBNull.Value) _zjs_ApplyMDL.ApplyCode = Convert.ToString(reader["ApplyCode"]);
                    if (reader["ApplyStatus"] != DBNull.Value) _zjs_ApplyMDL.ApplyStatus = Convert.ToString(reader["ApplyStatus"]);
                    if (reader["GetDateTime"] != DBNull.Value) _zjs_ApplyMDL.GetDateTime = Convert.ToDateTime(reader["GetDateTime"]);
                    if (reader["GetResult"] != DBNull.Value) _zjs_ApplyMDL.GetResult = Convert.ToString(reader["GetResult"]);
                    if (reader["GetRemark"] != DBNull.Value) _zjs_ApplyMDL.GetRemark = Convert.ToString(reader["GetRemark"]);
                    if (reader["GetMan"] != DBNull.Value) _zjs_ApplyMDL.GetMan = Convert.ToString(reader["GetMan"]);
                    if (reader["ExamineDatetime"] != DBNull.Value) _zjs_ApplyMDL.ExamineDatetime = Convert.ToDateTime(reader["ExamineDatetime"]);
                    if (reader["ExamineResult"] != DBNull.Value) _zjs_ApplyMDL.ExamineResult = Convert.ToString(reader["ExamineResult"]);
                    if (reader["ExamineRemark"] != DBNull.Value) _zjs_ApplyMDL.ExamineRemark = Convert.ToString(reader["ExamineRemark"]);
                    if (reader["ExamineMan"] != DBNull.Value) _zjs_ApplyMDL.ExamineMan = Convert.ToString(reader["ExamineMan"]);
                    if (reader["ReportDate"] != DBNull.Value) _zjs_ApplyMDL.ReportDate = Convert.ToDateTime(reader["ReportDate"]);
                    if (reader["ReportMan"] != DBNull.Value) _zjs_ApplyMDL.ReportMan = Convert.ToString(reader["ReportMan"]);
                    if (reader["ReportCode"] != DBNull.Value) _zjs_ApplyMDL.ReportCode = Convert.ToString(reader["ReportCode"]);
                    if (reader["CheckDate"] != DBNull.Value) _zjs_ApplyMDL.CheckDate = Convert.ToDateTime(reader["CheckDate"]);
                    if (reader["CheckResult"] != DBNull.Value) _zjs_ApplyMDL.CheckResult = Convert.ToString(reader["CheckResult"]);
                    if (reader["CheckRemark"] != DBNull.Value) _zjs_ApplyMDL.CheckRemark = Convert.ToString(reader["CheckRemark"]);
                    if (reader["CheckMan"] != DBNull.Value) _zjs_ApplyMDL.CheckMan = Convert.ToString(reader["CheckMan"]);
                    if (reader["ConfirmDate"] != DBNull.Value) _zjs_ApplyMDL.ConfirmDate = Convert.ToDateTime(reader["ConfirmDate"]);
                    if (reader["ConfirmResult"] != DBNull.Value) _zjs_ApplyMDL.ConfirmResult = Convert.ToString(reader["ConfirmResult"]);
                    if (reader["ConfirmMan"] != DBNull.Value) _zjs_ApplyMDL.ConfirmMan = Convert.ToString(reader["ConfirmMan"]);
                    if (reader["PublicDate"] != DBNull.Value) _zjs_ApplyMDL.PublicDate = Convert.ToDateTime(reader["PublicDate"]);
                    if (reader["PublicMan"] != DBNull.Value) _zjs_ApplyMDL.PublicMan = Convert.ToString(reader["PublicMan"]);
                    if (reader["PublicCode"] != DBNull.Value) _zjs_ApplyMDL.PublicCode = Convert.ToString(reader["PublicCode"]);
                    if (reader["NoticeDate"] != DBNull.Value) _zjs_ApplyMDL.NoticeDate = Convert.ToDateTime(reader["NoticeDate"]);
                    if (reader["NoticeMan"] != DBNull.Value) _zjs_ApplyMDL.NoticeMan = Convert.ToString(reader["NoticeMan"]);
                    if (reader["NoticeCode"] != DBNull.Value) _zjs_ApplyMDL.NoticeCode = Convert.ToString(reader["NoticeCode"]);
                    if (reader["CodeDate"] != DBNull.Value) _zjs_ApplyMDL.CodeDate = Convert.ToDateTime(reader["CodeDate"]);
                    if (reader["CodeMan"] != DBNull.Value) _zjs_ApplyMDL.CodeMan = Convert.ToString(reader["CodeMan"]);
                    if (reader["CJR"] != DBNull.Value) _zjs_ApplyMDL.CJR = Convert.ToString(reader["CJR"]);
                    if (reader["CJSJ"] != DBNull.Value) _zjs_ApplyMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
                    if (reader["XGR"] != DBNull.Value) _zjs_ApplyMDL.XGR = Convert.ToString(reader["XGR"]);
                    if (reader["XGSJ"] != DBNull.Value) _zjs_ApplyMDL.XGSJ = Convert.ToDateTime(reader["XGSJ"]);
                    if (reader["Valid"] != DBNull.Value) _zjs_ApplyMDL.Valid = Convert.ToInt32(reader["Valid"]);
                    if (reader["Memo"] != DBNull.Value) _zjs_ApplyMDL.Memo = Convert.ToString(reader["Memo"]);
                    if (reader["SheBaoCheck"] != DBNull.Value) _zjs_ApplyMDL.SheBaoCheck = Convert.ToInt32(reader["SheBaoCheck"]);
                    if (reader["OldUnitName"] != DBNull.Value) _zjs_ApplyMDL.OldUnitName = Convert.ToString(reader["OldUnitName"]);
                    if (reader["OldEnt_QYZJJGDM"] != DBNull.Value) _zjs_ApplyMDL.OldEnt_QYZJJGDM = Convert.ToString(reader["OldEnt_QYZJJGDM"]);
                    if (reader["OldUnitCheckTime"] != DBNull.Value) _zjs_ApplyMDL.OldUnitCheckTime = Convert.ToDateTime(reader["OldUnitCheckTime"]);
                    if (reader["OldUnitCheckResult"] != DBNull.Value) _zjs_ApplyMDL.OldUnitCheckResult = Convert.ToString(reader["OldUnitCheckResult"]);
                    if (reader["OldUnitCheckRemark"] != DBNull.Value) _zjs_ApplyMDL.OldUnitCheckRemark = Convert.ToString(reader["OldUnitCheckRemark"]);
                    if (reader["ENT_ContractType"] != DBNull.Value) _zjs_ApplyMDL.ENT_ContractType = Convert.ToInt32(reader["ENT_ContractType"]);
                    if (reader["ENT_ContractStartTime"] != DBNull.Value) _zjs_ApplyMDL.ENT_ContractStartTime = Convert.ToDateTime(reader["ENT_ContractStartTime"]);
                    if (reader["ENT_ContractENDTime"] != DBNull.Value) _zjs_ApplyMDL.ENT_ContractENDTime = Convert.ToDateTime(reader["ENT_ContractENDTime"]);
                    if (reader["LastBackResult"] != DBNull.Value) _zjs_ApplyMDL.LastBackResult = Convert.ToString(reader["LastBackResult"]);
                    if (reader["CheckZSSD"] != DBNull.Value) _zjs_ApplyMDL.CheckZSSD = Convert.ToInt32(reader["CheckZSSD"]);
                    if (reader["CheckXSL"] != DBNull.Value) _zjs_ApplyMDL.CheckXSL = Convert.ToInt32(reader["CheckXSL"]);
                    if (reader["CheckCFZC"] != DBNull.Value) _zjs_ApplyMDL.CheckCFZC = Convert.ToInt32(reader["CheckCFZC"]);
                    if (reader["CheckYCZC"] != DBNull.Value) _zjs_ApplyMDL.CheckYCZC = Convert.ToInt32(reader["CheckYCZC"]);
                    if (reader["newUnitCheckTime"] != DBNull.Value) _zjs_ApplyMDL.newUnitCheckTime = Convert.ToDateTime(reader["newUnitCheckTime"]);
                    if (reader["newUnitCheckResult"] != DBNull.Value) _zjs_ApplyMDL.newUnitCheckResult = Convert.ToString(reader["newUnitCheckResult"]);
                    if (reader["newUnitCheckRemark"] != DBNull.Value) _zjs_ApplyMDL.newUnitCheckRemark = Convert.ToString(reader["newUnitCheckRemark"]);
                    if (reader["Nation"] != DBNull.Value) _zjs_ApplyMDL.Nation = Convert.ToString(reader["Nation"]);
                    if (reader["Birthday"] != DBNull.Value) _zjs_ApplyMDL.Birthday = Convert.ToDateTime(reader["Birthday"]);
                    list.Add(_zjs_ApplyMDL);
                }
                reader.Close();
                db.Close();
                return list;
            }
        }

        //        /// <summary>
        //        /// 根据申请批次号获取第一个申请单实体（用于企业信息变更获取一条记录，读取审核进度及状态）
        //        /// </summary>
        //        /// <param name="ApplyCode">申请批次号</param>
        //        public static zjs_ApplyMDL GetFirstObjectByApplyCode(string ApplyCode)
        //        {
        //            string sql = @"
        //			SELECT top 1 ApplyID,ApplyType,ApplyTypeSub,ENT_Name,ENT_OrganizationsCode,ENT_City,PSN_Name,PSN_Sex,PSN_CertificateType,PSN_CertificateNO,PSN_RegisterNo,PSN_RegisterCertificateNo,PSN_RegisteProfession,ApplyTime,ApplyCode,ApplyStatus,GetDateTime,GetResult,GetRemark,GetMan,ExamineDatetime,ExamineResult,ExamineRemark,ExamineMan,ReportDate,ReportMan,ReportCode,CheckDate,CheckResult,CheckRemark,CheckMan,ConfirmDate,ConfirmResult,ConfirmMan,PublicDate,PublicMan,PublicCode,NoticeDate,NoticeMan,NoticeCode,CodeDate,CodeMan,CJR,CJSJ,XGR,XGSJ,[VALID],Memo,SheBaoCheck,OldUnitName,OldEnt_QYZJJGDM,OldUnitCheckTime,OldUnitCheckResult,OldUnitCheckRemark,ENT_ContractType,ENT_ContractStartTime,ENT_ContractENDTime,LastBackResult,CheckZSSD,CheckXSL,CheckCFZC,CheckYCZC,newUnitCheckTime,newUnitCheckResult,newUnitCheckRemark,Nation,Birthday
        //			FROM dbo.zjs_Apply
        //			WHERE ApplyCode = @ApplyCode";

        //            DBHelper db = new DBHelper();
        //            List<SqlParameter> p = new List<SqlParameter>();
        //            p.Add(db.CreateParameter("ApplyCode", DbType.String, ApplyCode));
        //            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
        //            {
        //                zjs_ApplyMDL _zjs_ApplyMDL = null;
        //                if (reader.Read())
        //                {
        //                    _zjs_ApplyMDL = new zjs_ApplyMDL();
        //                    if (reader["ApplyID"] != DBNull.Value) _zjs_ApplyMDL.ApplyID = Convert.ToString(reader["ApplyID"]);
        //                    if (reader["ApplyType"] != DBNull.Value) _zjs_ApplyMDL.ApplyType = Convert.ToString(reader["ApplyType"]);
        //                    if (reader["ApplyTypeSub"] != DBNull.Value) _zjs_ApplyMDL.ApplyTypeSub = Convert.ToString(reader["ApplyTypeSub"]);
        //                    if (reader["ENT_Name"] != DBNull.Value) _zjs_ApplyMDL.ENT_Name = Convert.ToString(reader["ENT_Name"]);
        //                    if (reader["ENT_OrganizationsCode"] != DBNull.Value) _zjs_ApplyMDL.ENT_OrganizationsCode = Convert.ToString(reader["ENT_OrganizationsCode"]);
        //                    if (reader["ENT_City"] != DBNull.Value) _zjs_ApplyMDL.ENT_City = Convert.ToString(reader["ENT_City"]);
        //                    if (reader["PSN_Name"] != DBNull.Value) _zjs_ApplyMDL.PSN_Name = Convert.ToString(reader["PSN_Name"]);
        //                    if (reader["PSN_Sex"] != DBNull.Value) _zjs_ApplyMDL.PSN_Sex = Convert.ToString(reader["PSN_Sex"]);
        //                    if (reader["PSN_CertificateType"] != DBNull.Value) _zjs_ApplyMDL.PSN_CertificateType = Convert.ToString(reader["PSN_CertificateType"]);
        //                    if (reader["PSN_CertificateNO"] != DBNull.Value) _zjs_ApplyMDL.PSN_CertificateNO = Convert.ToString(reader["PSN_CertificateNO"]);
        //                    if (reader["PSN_RegisterNo"] != DBNull.Value) _zjs_ApplyMDL.PSN_RegisterNo = Convert.ToString(reader["PSN_RegisterNo"]);
        //                    if (reader["PSN_RegisterCertificateNo"] != DBNull.Value) _zjs_ApplyMDL.PSN_RegisterCertificateNo = Convert.ToString(reader["PSN_RegisterCertificateNo"]);
        //                    if (reader["PSN_RegisteProfession"] != DBNull.Value) _zjs_ApplyMDL.PSN_RegisteProfession = Convert.ToString(reader["PSN_RegisteProfession"]);
        //                    if (reader["ApplyTime"] != DBNull.Value) _zjs_ApplyMDL.ApplyTime = Convert.ToDateTime(reader["ApplyTime"]);
        //                    if (reader["ApplyCode"] != DBNull.Value) _zjs_ApplyMDL.ApplyCode = Convert.ToString(reader["ApplyCode"]);
        //                    if (reader["ApplyStatus"] != DBNull.Value) _zjs_ApplyMDL.ApplyStatus = Convert.ToString(reader["ApplyStatus"]);
        //                    if (reader["GetDateTime"] != DBNull.Value) _zjs_ApplyMDL.GetDateTime = Convert.ToDateTime(reader["GetDateTime"]);
        //                    if (reader["GetResult"] != DBNull.Value) _zjs_ApplyMDL.GetResult = Convert.ToString(reader["GetResult"]);
        //                    if (reader["GetRemark"] != DBNull.Value) _zjs_ApplyMDL.GetRemark = Convert.ToString(reader["GetRemark"]);
        //                    if (reader["GetMan"] != DBNull.Value) _zjs_ApplyMDL.GetMan = Convert.ToString(reader["GetMan"]);
        //                    if (reader["ExamineDatetime"] != DBNull.Value) _zjs_ApplyMDL.ExamineDatetime = Convert.ToDateTime(reader["ExamineDatetime"]);
        //                    if (reader["ExamineResult"] != DBNull.Value) _zjs_ApplyMDL.ExamineResult = Convert.ToString(reader["ExamineResult"]);
        //                    if (reader["ExamineRemark"] != DBNull.Value) _zjs_ApplyMDL.ExamineRemark = Convert.ToString(reader["ExamineRemark"]);
        //                    if (reader["ExamineMan"] != DBNull.Value) _zjs_ApplyMDL.ExamineMan = Convert.ToString(reader["ExamineMan"]);
        //                    if (reader["ReportDate"] != DBNull.Value) _zjs_ApplyMDL.ReportDate = Convert.ToDateTime(reader["ReportDate"]);
        //                    if (reader["ReportMan"] != DBNull.Value) _zjs_ApplyMDL.ReportMan = Convert.ToString(reader["ReportMan"]);
        //                    if (reader["ReportCode"] != DBNull.Value) _zjs_ApplyMDL.ReportCode = Convert.ToString(reader["ReportCode"]);
        //                    if (reader["CheckDate"] != DBNull.Value) _zjs_ApplyMDL.CheckDate = Convert.ToDateTime(reader["CheckDate"]);
        //                    if (reader["CheckResult"] != DBNull.Value) _zjs_ApplyMDL.CheckResult = Convert.ToString(reader["CheckResult"]);
        //                    if (reader["CheckRemark"] != DBNull.Value) _zjs_ApplyMDL.CheckRemark = Convert.ToString(reader["CheckRemark"]);
        //                    if (reader["CheckMan"] != DBNull.Value) _zjs_ApplyMDL.CheckMan = Convert.ToString(reader["CheckMan"]);
        //                    if (reader["ConfirmDate"] != DBNull.Value) _zjs_ApplyMDL.ConfirmDate = Convert.ToDateTime(reader["ConfirmDate"]);
        //                    if (reader["ConfirmResult"] != DBNull.Value) _zjs_ApplyMDL.ConfirmResult = Convert.ToString(reader["ConfirmResult"]);
        //                    if (reader["ConfirmMan"] != DBNull.Value) _zjs_ApplyMDL.ConfirmMan = Convert.ToString(reader["ConfirmMan"]);
        //                    if (reader["PublicDate"] != DBNull.Value) _zjs_ApplyMDL.PublicDate = Convert.ToDateTime(reader["PublicDate"]);
        //                    if (reader["PublicMan"] != DBNull.Value) _zjs_ApplyMDL.PublicMan = Convert.ToString(reader["PublicMan"]);
        //                    if (reader["PublicCode"] != DBNull.Value) _zjs_ApplyMDL.PublicCode = Convert.ToString(reader["PublicCode"]);
        //                    if (reader["NoticeDate"] != DBNull.Value) _zjs_ApplyMDL.NoticeDate = Convert.ToDateTime(reader["NoticeDate"]);
        //                    if (reader["NoticeMan"] != DBNull.Value) _zjs_ApplyMDL.NoticeMan = Convert.ToString(reader["NoticeMan"]);
        //                    if (reader["NoticeCode"] != DBNull.Value) _zjs_ApplyMDL.NoticeCode = Convert.ToString(reader["NoticeCode"]);
        //                    if (reader["CodeDate"] != DBNull.Value) _zjs_ApplyMDL.CodeDate = Convert.ToDateTime(reader["CodeDate"]);
        //                    if (reader["CodeMan"] != DBNull.Value) _zjs_ApplyMDL.CodeMan = Convert.ToString(reader["CodeMan"]);
        //                    if (reader["CJR"] != DBNull.Value) _zjs_ApplyMDL.CJR = Convert.ToString(reader["CJR"]);
        //                    if (reader["CJSJ"] != DBNull.Value) _zjs_ApplyMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
        //                    if (reader["XGR"] != DBNull.Value) _zjs_ApplyMDL.XGR = Convert.ToString(reader["XGR"]);
        //                    if (reader["XGSJ"] != DBNull.Value) _zjs_ApplyMDL.XGSJ = Convert.ToDateTime(reader["XGSJ"]);
        //                    if (reader["Valid"] != DBNull.Value) _zjs_ApplyMDL.Valid = Convert.ToInt32(reader["Valid"]);
        //                    if (reader["Memo"] != DBNull.Value) _zjs_ApplyMDL.Memo = Convert.ToString(reader["Memo"]);
        //                    if (reader["SheBaoCheck"] != DBNull.Value) _zjs_ApplyMDL.SheBaoCheck = Convert.ToInt32(reader["SheBaoCheck"]);
        //                    if (reader["OldUnitName"] != DBNull.Value) _zjs_ApplyMDL.OldUnitName = Convert.ToString(reader["OldUnitName"]);
        //                    if (reader["OldEnt_QYZJJGDM"] != DBNull.Value) _zjs_ApplyMDL.OldEnt_QYZJJGDM = Convert.ToString(reader["OldEnt_QYZJJGDM"]);
        //                    if (reader["OldUnitCheckTime"] != DBNull.Value) _zjs_ApplyMDL.OldUnitCheckTime = Convert.ToDateTime(reader["OldUnitCheckTime"]);
        //                    if (reader["OldUnitCheckResult"] != DBNull.Value) _zjs_ApplyMDL.OldUnitCheckResult = Convert.ToString(reader["OldUnitCheckResult"]);
        //                    if (reader["OldUnitCheckRemark"] != DBNull.Value) _zjs_ApplyMDL.OldUnitCheckRemark = Convert.ToString(reader["OldUnitCheckRemark"]);
        //                    if (reader["ENT_ContractType"] != DBNull.Value) _zjs_ApplyMDL.ENT_ContractType = Convert.ToInt32(reader["ENT_ContractType"]);
        //                    if (reader["ENT_ContractStartTime"] != DBNull.Value) _zjs_ApplyMDL.ENT_ContractStartTime = Convert.ToDateTime(reader["ENT_ContractStartTime"]);
        //                    if (reader["ENT_ContractENDTime"] != DBNull.Value) _zjs_ApplyMDL.ENT_ContractENDTime = Convert.ToDateTime(reader["ENT_ContractENDTime"]);
        //                    if (reader["LastBackResult"] != DBNull.Value) _zjs_ApplyMDL.LastBackResult = Convert.ToString(reader["LastBackResult"]);
        //                    if (reader["CheckZSSD"] != DBNull.Value) _zjs_ApplyMDL.CheckZSSD = Convert.ToInt32(reader["CheckZSSD"]);
        //                    if (reader["CheckXSL"] != DBNull.Value) _zjs_ApplyMDL.CheckXSL = Convert.ToInt32(reader["CheckXSL"]);
        //                    if (reader["CheckCFZC"] != DBNull.Value) _zjs_ApplyMDL.CheckCFZC = Convert.ToInt32(reader["CheckCFZC"]);
        //                    if (reader["CheckYCZC"] != DBNull.Value) _zjs_ApplyMDL.CheckYCZC = Convert.ToInt32(reader["CheckYCZC"]);
        //                    if (reader["newUnitCheckTime"] != DBNull.Value) _zjs_ApplyMDL.newUnitCheckTime = Convert.ToDateTime(reader["newUnitCheckTime"]);
        //                    if (reader["newUnitCheckResult"] != DBNull.Value) _zjs_ApplyMDL.newUnitCheckResult = Convert.ToString(reader["newUnitCheckResult"]);
        //                    if (reader["newUnitCheckRemark"] != DBNull.Value) _zjs_ApplyMDL.newUnitCheckRemark = Convert.ToString(reader["newUnitCheckRemark"]);
        //                    if (reader["Nation"] != DBNull.Value) _zjs_ApplyMDL.Nation = Convert.ToString(reader["Nation"]);
        //                    if (reader["Birthday"] != DBNull.Value) _zjs_ApplyMDL.Birthday = Convert.ToDateTime(reader["Birthday"]);
        //                }
        //                reader.Close();
        //                db.Close();
        //                return _zjs_ApplyMDL;
        //            }
        //        }

        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        public static DataTable GetListApplyView(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.View_ZJS_Applying", "*", filterWhereString, orderBy == "" ? " xgsj desc,PSN_CertificateNO" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountApplyView(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.View_ZJS_Applying", filterWhereString);
        }

        /// <summary>
        /// 获取下一个造价师注册申请单号
        /// 申请表编号规则：共14位，第一位固定z,年份（2016），月份（05），日（15），注册类别（初始10、变更31、32、33、延续20、重新60、注销51）、自然序号（000）
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="type">注册类型</param>
        /// <returns>单号</returns>
        public static string GetNextApplyCode(DbTransaction tran, string type)
        {
            string num = null;
            switch (type)
            {
                case "初始注册":
                    num = "10";
                    break;
                case "个人信息变更":
                    num = "31";
                    break;
                case "企业信息变更":
                    num = "32";
                    break;
                case "执业企业变更":
                    num = "33";
                    break;
                case "延续注册":
                    num = "20";
                    break;
                case "注销":
                    num = "51";
                    break;
                default:
                    break;
            }
            string sql = @"select isnull(max([ApplyCode]),'')
                            from zjs_Apply
                            group by case when ApplyTypeSub <>''  then ApplyTypeSub else  ApplyType end  
                            having  case when ApplyTypeSub <>''  then ApplyTypeSub else  ApplyType end  ='{0}'";
            string ApplyCode = CommonDAL.GetDataTable(string.Format(sql, type)).Rows.Count > 0 ? CommonDAL.GetDataTable(string.Format(sql, type)).Rows[0][0].ToString() : "";

            if (ApplyCode == ""
                || ApplyCode.Substring(1, 8) != DateTime.Now.ToString("yyyyMMdd"))
            {
                return string.Format("z{0}{1}001", DateTime.Now.ToString("yyyyMMdd"), num);
            }
            else
            {

                return string.Format("z{0}{1}{2}", DateTime.Now.ToString("yyyyMMdd"), num, (Convert.ToInt32(ApplyCode.Substring(11, 3)) + 1).ToString().PadLeft(3, '0'));
            }


        }
        /// <summary>
        /// 上报批次号：1位固定字符z + 2位区县编号 + 8位年月日 + 2位注册类型 + 3位自然序号（000）
        /// </summary>
        /// <param name="city">区县</param>
        /// <param name="type">申报事项</param>
        /// <returns></returns>
        public static string GetNextReportCode(string city, string type)
        {
            string ncity = null;
            string num = null;
            switch (city)
            {
                case "东城":
                case "东城区":
                    ncity = Model.EnumManager.CityEnum.东城区;
                    break;
                case "西城":
                case "西城区":
                    ncity = Model.EnumManager.CityEnum.西城区;
                    break;
                case "崇文":
                case "崇文区":
                    ncity = Model.EnumManager.CityEnum.崇文区;
                    break;
                case "宣武":
                case "宣武区":
                    ncity = Model.EnumManager.CityEnum.宣武区;
                    break;
                case "朝阳":
                case "朝阳区":
                    ncity = Model.EnumManager.CityEnum.朝阳区;
                    break;
                case "丰台":
                case "丰台区":
                    ncity = Model.EnumManager.CityEnum.丰台区;
                    break;
                case "石景山":
                case "石景山区":
                    ncity = Model.EnumManager.CityEnum.石景山区;
                    break;
                case "海淀":
                case "海淀区":
                    ncity = Model.EnumManager.CityEnum.海淀区;
                    break;
                case "门头沟":
                case "门头沟区":
                    ncity = Model.EnumManager.CityEnum.门头沟区;
                    break;
                case "房山":
                case "房山区":
                    ncity = Model.EnumManager.CityEnum.房山区;
                    break;
                case "通州":
                case "通州区":
                    ncity = Model.EnumManager.CityEnum.通州区;
                    break;
                case "顺义":
                case "顺义区":
                    ncity = Model.EnumManager.CityEnum.顺义区;
                    break;
                case "昌平":
                case "昌平区":
                    ncity = Model.EnumManager.CityEnum.昌平区;
                    break;
                case "大兴":
                case "大兴区":
                    ncity = Model.EnumManager.CityEnum.大兴区;
                    break;
                case "怀柔":
                case "怀柔区":
                    ncity = Model.EnumManager.CityEnum.怀柔区;
                    break;
                case "平谷":
                case "平谷区":
                    ncity = Model.EnumManager.CityEnum.平谷区;
                    break;
                case "亦庄":
                case "亦庄开发区":
                    ncity = Model.EnumManager.CityEnum.亦庄;
                    break;
                case "密云":
                case "密云区":
                    ncity = Model.EnumManager.CityEnum.密云区;
                    break;
                case "延庆":
                case "延庆区":
                    ncity = Model.EnumManager.CityEnum.延庆区;
                    break;
                case "全市":
                    ncity = "00";
                    break;
            }
            switch (type)
            {
                case "初始注册":
                    num = "10";
                    break;
                case "变更注册":
                    num = "31";
                    break;
                case "延期注册":
                    num = "20";
                    break;
                case "注销":
                    num = "51";
                    break;
            }
            string sql = string.Format("select isnull(max(substring(ReportCode,4,16)),'') from zjs_apply group by applytype having applytype='{0}'", type);
            string ApplyCode = CommonDAL.GetDataTable(sql).Rows.Count > 0 ? CommonDAL.GetDataTable(sql).Rows[0][0].ToString() : "";
            if (ApplyCode == ""
              || ApplyCode.Substring(0, 8) != DateTime.Now.ToString("yyyyMMdd"))
            {
                return string.Format("z{0}{1}001", ncity, DateTime.Now.ToString("yyyyMMdd") + num);
            }
            else
            {

                return string.Format("z{0}{1}{2}", ncity, DateTime.Now.ToString("yyyyMMdd") + num, (Convert.ToInt32(ApplyCode.Substring(10, 3)) + 1).ToString().PadLeft(3, '0'));
            }
        }
        /// <summary>
        /// 获取公示或公告下一个申请单号
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="type">类型：初始注册、延续注册</param>
        /// <returns></returns>
        public static string GetNextNoticeCode(string type)
        {
            string num = null;
            switch (type)
            {
                case "初始注册":
                    num = "10";
                    break;
                case "延续注册":
                    num = "20";
                    break;
                default:
                    break;
            }

            string sg = "Z";//公告编号前缀
            string sql = string.Format("select isnull(max(substring([NoticeCode],2,13)),'') from zjs_apply group by applytype having applytype='{0}'", type);
            string ApplyCode = CommonDAL.GetDataTable(sql).Rows.Count > 0 ? CommonDAL.GetDataTable(sql).Rows[0][0].ToString() : "";
            if (ApplyCode == ""
                || ApplyCode.Substring(0, 8) != DateTime.Now.ToString("yyyyMMdd"))
            {
                return string.Format("{0}{1}001", sg, DateTime.Now.ToString("yyyyMMdd") + num);
            }
            else
            {
                return string.Format("{0}{1}{2}", sg, DateTime.Now.ToString("yyyyMMdd") + num, (Convert.ToInt32(ApplyCode.Substring(10, 3)) + 1).ToString().PadLeft(3, '0'));
            }
        }
        /// <summary>
        /// 企业获取申请单审批记录集合
        /// </summary>     
        /// <param name="ApplyID">申请ID</param>
        /// <returns>审批历史记录集合</returns>
        public static DataTable GetCheckHistoryList(string ApplyID)
        {


            string sql = @"select row_number() over(order by ActionData ) as RowNo ,t.* from 
(
 select case  when memo ='申请强制注销' then case when [CJR]=[PSN_Name] then '个人申请强制注销' else '单位申请强制注销' end else '个人申请' + [ApplyType] end  as 'Action',
        [ApplyTime] as ActionData,case [ApplyStatus] when '未申报' then '未申报' else '已提交' end as ActionResult,
        case [ApplyStatus] when '未申报' then '未提交审核' else  case  when memo ='申请强制注销' then '提交区县审核' else '提交单位确认' end end as ActionRemark, 
        case  when memo ='申请强制注销' then case when [CJR]=[PSN_Name] then  [PSN_Name] else [ENT_Name] end else [PSN_Name] end as ActionMan 
        FROM [dbo].[zjs_Apply]  where [ApplyID]='{0}'
 union all
select   '原单位确认' as 'Action',[OldUnitCheckTime] as ActionData,[OldUnitCheckResult] as ActionResult,[OldUnitCheckRemark] as ActionRemark,OldENT_Name as ActionMan FROM [dbo].[zjs_Apply] left join [zjs_ApplyChange] on  zjs_Apply.ApplyID=zjs_ApplyChange.ApplyID where zjs_Apply.[ApplyID]='{0}' and zjs_Apply.[OldUnitCheckTime] is not null and zjs_Apply.[ApplyTypeSub] ='执业企业变更'
 union all
 select   '单位确认' as 'Action',[OldUnitCheckTime] as ActionData,[OldUnitCheckResult] as ActionResult,[OldUnitCheckRemark] as ActionRemark,[ENT_Name] as ActionMan FROM [dbo].[zjs_Apply]  where [ApplyID]='{0}' and [OldUnitCheckTime] is not null and ([ApplyTypeSub] is null or [ApplyTypeSub] <>'执业企业变更')
 union all
select   '新单位确认' as 'Action',[newUnitCheckTime] as ActionData,[newUnitCheckResult] as ActionResult,[newUnitCheckRemark] as ActionRemark,ENT_Name as ActionMan FROM [dbo].[zjs_Apply] where [ApplyID]='{0}' and [newUnitCheckTime] is not null and [ApplyTypeSub] ='执业企业变更'
 union all
 select   '市级受理' as 'Action',[GetDateTime] as ActionData,[GetResult] as ActionResult,[GetRemark] as ActionRemark,[GetMan] as ActionMan FROM [dbo].[zjs_Apply] where [ApplyID]='{0}' and [GetDateTime] is not null
 union all
  select  '市级审核',[ExamineDatetime],[ExamineResult],[ExamineRemark],[ExamineMan] FROM [dbo].[zjs_Apply] where [ApplyID]='{0}' and [ExamineDatetime] is not null
 union all
  select  '市级复核',[CheckDate],[CheckResult],[CheckRemark],[CheckMan] FROM [dbo].[zjs_Apply] where [ApplyID]='{0}' and [CheckDate] is not null and (([NoticeDate] is not null and ApplyType in ('初始注册','重新注册','延续注册')) or (ApplyType not in ('初始注册','重新注册','延续注册')))
 union all
  select  '市级决定',[ConfirmDate],[ConfirmResult],'',[ConfirmMan] FROM [dbo].[zjs_Apply] where [ApplyID]='{0}' and [ConfirmDate] is not null and (([NoticeDate] is not null and ApplyType in ('初始注册','重新注册','延续注册')) or (ApplyType not in ('初始注册','重新注册','延续注册')))
 union all
  select  '公告',[NoticeDate],'','批次号：'+[NoticeCode],[NoticeMan] FROM [dbo].[zjs_Apply] where [ApplyID]='{0}' and [NoticeDate] is not null and [NoticeMan] is not null 
) t";
            return CommonDAL.GetDataTable(string.Format(sql, ApplyID));
        }

        /// <summary>
        /// 注册中心获取申请单审批记录集合 
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCheckHistoryList2(string ApplyID)
        {
            string sql = @"select row_number() over(order by ActionData ) as RowNo ,t.* from 
(
 select case  when memo ='申请强制注销' then case when [CJR]=[PSN_Name] then '个人申请强制注销' else '单位申请强制注销' end else '个人申请' + [ApplyType] end  as 'Action',
        [ApplyTime] as ActionData,case [ApplyStatus] when '未申报' then '未申报' else '已提交' end as ActionResult,
        case [ApplyStatus] when '未申报' then '未提交审核' else  case  when memo ='申请强制注销' then '提交区县审核' else '提交单位确认' end end as ActionRemark, 
        case  when memo ='申请强制注销' then case when [CJR]=[PSN_Name] then  [PSN_Name] else [ENT_Name] end else [PSN_Name] end as ActionMan 
        FROM [dbo].[zjs_Apply]  where [ApplyID]='{0}'
 union all
 select   '原单位确认' as 'Action',[OldUnitCheckTime] as ActionData,[OldUnitCheckResult] as ActionResult,[OldUnitCheckRemark] as ActionRemark,OldENT_Name as ActionMan FROM [dbo].[zjs_Apply] left join [zjs_ApplyChange] on  zjs_Apply.ApplyID=zjs_ApplyChange.ApplyID where zjs_Apply.[ApplyID]='{0}' and [OldUnitCheckTime] is not null and [ApplyTypeSub] ='执业企业变更'
 union all
 select   '单位确认' as 'Action',[OldUnitCheckTime] as ActionData,[OldUnitCheckResult] as ActionResult,[OldUnitCheckRemark] as ActionRemark,[ENT_Name] as ActionMan FROM [dbo].[zjs_Apply]  where [ApplyID]='{0}' and [OldUnitCheckTime] is not null and ([ApplyTypeSub] is null or [ApplyTypeSub] <>'执业企业变更')
  union all
select   '新单位确认' as 'Action',[newUnitCheckTime] as ActionData,[newUnitCheckResult] as ActionResult,[newUnitCheckRemark] as ActionRemark,ENT_Name as ActionMan FROM [dbo].[zjs_Apply] where [ApplyID]='{0}' and [newUnitCheckTime] is not null and [ApplyTypeSub] ='执业企业变更'
 union all
 select   '市级受理' as 'Action',[GetDateTime] as ActionData,[GetResult] as ActionResult,[GetRemark] as ActionRemark,[GetMan] as ActionMan FROM [dbo].[zjs_Apply] where [ApplyID]='{0}' and [GetDateTime] is not null
 union all
  select  '市级审核',[ExamineDatetime],[ExamineResult],[ExamineRemark],[ExamineMan] FROM [dbo].[zjs_Apply] where [ApplyID]='{0}' and [ExamineDatetime] is not null
 union all
  select  '市级复核',[CheckDate],[CheckResult],[CheckRemark],[CheckMan] FROM [dbo].[zjs_Apply] where [ApplyID]='{0}' and [CheckDate] is not null 
 union all
  select  '市级决定',[ConfirmDate],[ConfirmResult],'',[ConfirmMan] FROM [dbo].[zjs_Apply] where [ApplyID]='{0}' and [ConfirmDate] is not null
 union all
  select  '公告',[NoticeDate],'','批次号：'+[NoticeCode],[NoticeMan] FROM [dbo].[zjs_Apply] where [ApplyID]='{0}' and [NoticeDate] is not null and [NoticeMan] is not null 
) t";
            return CommonDAL.GetDataTable(string.Format(sql, ApplyID));

        }

        /// <summary>
        /// 按事项类型统计代办任务数量
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static DataTable GetApplyGroupByApplyType(string where)
        {
            DBHelper db = new DBHelper();
            string sql = @"select ApplyType,count(1)as Num from 
                           (
                                select distinct case when ApplyType ='变更注册' then ApplyTypeSub else ApplyType  end as ApplyType
                                ,case when ApplyTypeSub ='企业信息变更' then [ENT_OrganizationsCode] else [PSN_CertificateNO] end PSN_CertificateNO
                                ,case when ApplyTypeSub ='企业信息变更' then '' else [PSN_RegisteProfession] end PSN_RegisteProfession
                                from  zjs_apply where 1=1 {0}
                            )t group by ApplyType ";
            return db.GetFillData(string.Format(sql, where));
        }

        /// <summary>
        /// 修改批量保存要上报数据，只将上报批次号赋值，不修改申报状态
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ReportCode"></param>
        /// <returns></returns>
        public static int UpdatePatchReport(DbTransaction tran, string ReportCode)
        {
            string sql = " update zjs_Apply set ReportCode=null where ReportCode='{0}'";
            return (new DBHelper()).GetExcuteNonQuery(tran, string.Format(sql, ReportCode));
        }
        /// <summary>
        /// 批量保存要上报数据，只将上报批次号赋值，不修改申报状态
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ReportCode">汇总批次号</param>
        /// <param name="applyid">申请单ID集合，用逗号分隔</param>
        /// <returns></returns>
        public static int SavePatchReport(DbTransaction tran, string ReportCode, string applyid)
        {
            string sql = " update zjs_Apply set ReportCode='{0}' where applyid in ({1})";
            return (new DBHelper()).GetExcuteNonQuery(tran, string.Format(sql, ReportCode, applyid));
        }

        /// <summary>
        /// 批量删除上报数据清单
        /// </summary>
        /// <param name="ReportCode">汇总批次号</param>
        /// <param name="Region">区县</param>
        ///  <param name="ApplyType">事项类型</param>
        /// <returns></returns>
        public static int DelPatchReport(string ReportCode, string Region, string ApplyType)
        {
            string sql = " update zjs_Apply set ReportCode=null where ReportCode='{0}' and ENT_City like '{1}%' and ApplyType='{2}'";
            return (new DBHelper()).GetExcuteNonQuery(string.Format(sql, ReportCode, Region, ApplyType));
        }

        /// <summary>
        /// 批量上报并办结
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ReportCode">上报批次号</param>
        /// <param name="Region">区县</param>
        /// <param name="ApplyType">申报类型</param>
        /// <param name="ReportMan">上报人</param>
        /// <returns></returns>
        public static int PatchReportFinish(DbTransaction tran, string ReportCode, string Region, string ApplyType, string ReportMan)
        {
            string sql = " update zjs_Apply  set reportdate='{0}' , reportman='{1}',applystatus='已公告',NoticeDate='{0}',[XGR] = '{1}' ,[XGSJ] ='{0}'  where ReportCode = '{2}' and ENT_City like '{3}%' and ApplyType='{4}'";
            return (new DBHelper()).GetExcuteNonQuery(tran, string.Format(sql, DateTime.Now, ReportMan, ReportCode, Region, ApplyType));
        }

        /// <summary>
        /// 批量上报
        /// </summary>
        /// <param name="ReportCode">上报批次号</param>
        /// <param name="Region">区县</param>
        /// <param name="ApplyType">申报类型</param>
        /// <param name="ReportMan">上报人</param>
        /// <returns></returns>
        public static int PatchReport(string ReportCode, string Region, string ApplyType, string ReportMan)
        {
            string sql = " update zjs_Apply  set reportdate='{0}' , reportman='{1}',applystatus='已上报' where ReportCode = '{2}' and ENT_City like '{3}%' and ApplyType='{4}'";
            return (new DBHelper()).GetExcuteNonQuery(string.Format(sql, DateTime.Now, ReportMan, ReportCode, Region, ApplyType));
        }

        ///// <summary>
        ///// 批量取消上报
        ///// </summary>
        ///// <param name="ReportCode">上报批次号</param>
        ///// <param name="Region">区县</param>
        /////  <param name="ApplyType">事项类型</param>
        ///// <returns></returns>
        //public static int CancelPatchReport(string ReportCode, string Region, string ApplyType)
        //{
        //    string sql = " update zjs_Apply set reportdate=null,reportman=null,applystatus='已审核' where ReportCode = '{0}' and ENT_City like '{1}%' and ApplyType='{2}'";
        //    return (new DBHelper()).GetExcuteNonQuery(string.Format(sql, ReportCode, Region, ApplyType));
        //}

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
              , @"(select max(ReportDate) as ReportDate
                    ,ReportCode
                    ,max(ApplyType) as ApplyType
                    ,max(ApplyTypeSub) as ApplyTypeSub
                    ,max(ENT_Name) as ENT_Name
                    ,count(1) as ManCount
                    ,(case when max([ReportMan]) is null then '未上报' else '已上报' end ) ReportStatus
                    ,(case when max([CheckMan]) is null then '未复核' else '已复核' end ) CheckStatus
                    ,(case when max([ConfirmMan]) is null then '未批准' else '已决定' end ) ConfirmStatus            
                    ,max(ENT_City)  as  ENT_City
                     from zjs_Apply group by ReportCode having ReportCode is not null
                    ) t"
              , "*"
              , filterWhereString, orderBy == "" ? " ReportCode desc" : orderBy);
        }

        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectReportCount(string filterWhereString)
        {

            return CommonDAL.SelectRowCount(@"(select max(ReportDate) as ReportDate
                                                ,ReportCode
                                                ,max(ApplyType) as ApplyType
                                                ,max(ApplyTypeSub) as ApplyTypeSub
                                                ,max(ENT_Name) as ENT_Name
                                                ,count(1) as ManCount
                                                ,(case when max([ReportMan]) is null then '未上报' else '已上报' end ) ReportStatus
                                                ,(case when max([CheckMan]) is null then '未复核' else '已复核' end ) CheckStatus
                                                ,(case when max([ConfirmMan]) is null then '未批准' else '已决定' end ) ConfirmStatus      
                                                ,max(ENT_City)  as  ENT_City
                                                 from zjs_Apply group by ReportCode having ReportCode is not null
                                                ) t", filterWhereString);
        }


        /// <summary>
        /// 批量保存要公告数据，只将公告流水号赋值，不修改申报状态
        /// </summary>
        /// <param name="NoticeCode">公告批次号</param>
        /// <param name="applyid">申请单ID集合，用逗号分隔</param>
        /// <returns></returns>
        public static int SaveNoticeCode(string NoticeCode, string applyid)
        {
            string sql = "  update zjs_Apply set NoticeCode = null where NoticeCode='{0}';update zjs_Apply set NoticeCode='{0}' where applyid in ({1})";
            return (new DBHelper()).GetExcuteNonQuery(string.Format(sql, NoticeCode, applyid));
        }
        /// <summary>
        /// 取消公告选择，退回让用户重新选择
        /// </summary>
        /// <param name="NoticeCode">公告批次号</param>
        /// <returns></returns>
        public static int DeleteUpdateNotice(string NoticeCode)
        {
            string sql = string.Format("update  zjs_Apply  set NoticeCode=Null where NoticeCode='{0}' and ApplyStatus='{1}'", NoticeCode, EnumManager.ZJSApplyStatus.已决定);
            return (new DBHelper()).GetExcuteNonQuery(sql);
        }

        /// <summary>
        /// 批量公告
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ApplyStatus">申报状态</param>
        /// <param name="NoticeDate">公告时间</param>
        /// <param name="NoticeMan">公告人</param>
        /// <param name="NoticeCode">公告批次号</param>
        /// <returns></returns>
        public static int ExeNoticeReport(DbTransaction tran, string ApplyStatus, DateTime NoticeDate, string NoticeMan, string NoticeCode)
        {
            string sql = " update [zjs_Apply] set ApplyStatus='{0}', NoticeDate='{1}' , NoticeMan='{2}' where NoticeCode='{3}'";
            return (new DBHelper()).GetExcuteNonQuery(tran, string.Format(sql, ApplyStatus, NoticeDate, NoticeMan, NoticeCode));
        }

        /// <summary>
        /// 公告查询获取实体集合
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        public static DataTable GetNoticeList(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows
                , @"(
                        select NoticeCode, max(case when NoticeMan is null then '未公告' else '已公告' end) as 'Notice',
                        count(1) as Num ,max(applytype) as ApplyType,max([NoticeDate]) as NoticeDate,max([CodeDate]) CodeDate,max([ConfirmResult]) as ConfirmResult
                         from zjs_Apply where NoticeCode is not null group by NoticeCode 
                    ) t"
                , "*"
                , filterWhereString, orderBy == "" ? " NoticeCode desc" : orderBy);
        }
        /// <summary>
        /// 统计公告查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectNoticeCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount(@"(   select NoticeCode, max(case when NoticeMan is null then '未公告' else '已公告' end) as 'Notice',
                        count(1) as Num ,max(applytype) as ApplyType,max([NoticeDate]) as NoticeDate,max([CodeDate]) CodeDate
                         from zjs_Apply where NoticeCode is not null group by NoticeCode 
                                                ) t", filterWhereString);
        }

        /// <summary>
        /// 检查待发放的注册编号是否已经存才
        /// </summary>
        /// <param name="list_PSN_RegisterNo">待检查注册编号集合</param>
        /// <returns>已存在的注册编号</returns>
        public static DataTable FindExistPSN_RegisterNo(string list_PSN_RegisterNo)
        {
            return CommonDAL.GetDataTable(string.Format("select [PSN_RegisterNo] from dbo.zjs_Apply where [PSN_RegisterNo] in({0})", list_PSN_RegisterNo));
        }

        /// <summary>
        /// 获取可放号注册编号：证书注册号尾号+1
        /// </summary>
        /// <returns>注册号</returns>
        public static long GetNextPSN_RegisterNO()
        {
            string sql = @"SELECT isnull(max(right([PSN_RegisterNO],6)),'0')         
                            FROM [dbo].[zjs_Certificate]";

            long code = Convert.ToInt64(CommonDAL.GetDataTable(sql).Rows[0][0]);
            if (code >= 999999)
            {
                throw new Exception("证书流水号已经操作最大6位允许范围");
            }
            return code + 1;
        }


        /// <summary>
        /// 根据公告批次号获取决定结果：通过 or 不通过
        /// </summary>
        /// <param name="NoticeCode">公告批次号</param>
        /// <returns>决定结果</returns>
        public static string GetConfirmResultByNoticeCode(string NoticeCode)
        {
            string sql = @"
			SELECT top 1 ConfirmResult
            FROM dbo.zjs_Apply
			WHERE NoticeCode = @NoticeCode";

            string rtn = "";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("NoticeCode", DbType.String, NoticeCode));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {

                if (reader.Read())
                {
                    rtn = Convert.ToString(reader["ConfirmResult"]);
                }
                reader.Close();
                db.Close();
                return rtn;
            }
        }

        /// <summary>
        /// 统计注册事项数量
        /// </summary>
        /// <param name="filterWhereString">过滤条件</param>
        /// <returns></returns>
        public static DataTable TjApplyCountByApplyType(string filterWhereString)
        {
            DBHelper db = new DBHelper();
            string sql = string.Format(@"select ApplyType,count(ApplyID)as Num  
                                        from zjs_apply 
                                        where  1=1 {0} 
                                        group by ApplyType", filterWhereString);
            return db.GetFillData(sql);
        }

        #endregion
    }
}
