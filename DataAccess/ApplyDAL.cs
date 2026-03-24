using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
    /// <summary>
    /// 业务类实现--ApplyDAL(填写类描述)
    /// </summary>
    public class ApplyDAL
    {
        public ApplyDAL() { }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="ApplyMDL">对象实体类</param>
        /// <returns></returns>
        public static int Insert(ApplyMDL _ApplyMDL)
        {
            return Insert(null, _ApplyMDL);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ApplyMDL">对象实体类</param>
        /// <returns></returns>
        public static int Insert(DbTransaction tran, ApplyMDL _ApplyMDL)
        {
            //给申请表加上人员是二级还是二级临时信息
            string psn_level = "二级";
            if (_ApplyMDL.PSN_ServerID != null)
            {
                DataTable dt = CommonDAL.GetDataTable("SELECT PSN_Level FROM COC_TOW_Person_BaseInfo where PSN_ServerID='" + _ApplyMDL.PSN_ServerID + "'");
                psn_level = dt.Rows[0][0] != DBNull.Value ? dt.Rows[0][0].ToString() : psn_level;
                _ApplyMDL.PSN_Level = psn_level;
            }

            DBHelper db = new DBHelper();
            string sql = @"
			INSERT INTO dbo.Apply(ApplyID,OldEnt_QYZJJGDM,ApplyType,ApplyTypeSub,ENT_ServerID,ENT_Name,ENT_OrganizationsCode,ENT_City,ENT_ContractStartTime,ENT_ContractENDTime,PSN_ServerID,PSN_Name,PSN_Sex,PSN_CertificateType,PSN_CertificateNO,PSN_RegisterNo,PSN_RegisterCertificateNo,PSN_RegisteProfession,ApplyTime,ApplyCode,ApplyStatus,GetDateTime,GetResult,GetRemark,GetMan,ExamineDatetime,ExamineResult,ExamineRemark,ExamineMan,ReportDate,ReportMan,ReportCode,AcceptDate,AcceptMan,CheckDate,CheckResult,CheckRemark,CheckMan,ConfirmDate,ConfirmResult,ConfirmRemark,ConfirmMan,PublicDate,PublicMan,PublicCode,NoticeDate,NoticeMan,NoticeCode,CJR,CJSJ,XGR,XGSJ,[VALID],Memo,CodeDate,CodeMan,SheBaoCheck,PSN_Level,OtherDeptCheckDate,OtherDeptCheckResult,OtherDeptCheckRemark,OtherDeptCheckMan,ENT_ContractType)
			VALUES (@ApplyID,@OldEnt_QYZJJGDM,@ApplyType,@ApplyTypeSub,@ENT_ServerID,@ENT_Name,@ENT_OrganizationsCode,@ENT_City,@ENT_ContractStartTime,@ENT_ContractENDTime,@PSN_ServerID,@PSN_Name,@PSN_Sex,@PSN_CertificateType,@PSN_CertificateNO,@PSN_RegisterNo,@PSN_RegisterCertificateNo,@PSN_RegisteProfession,@ApplyTime,@ApplyCode,@ApplyStatus,@GetDateTime,@GetResult,@GetRemark,@GetMan,@ExamineDatetime,@ExamineResult,@ExamineRemark,@ExamineMan,@ReportDate,@ReportMan,@ReportCode,@AcceptDate,@AcceptMan,@CheckDate,@CheckResult,@CheckRemark,@CheckMan,@ConfirmDate,@ConfirmResult,@ConfirmRemark,@ConfirmMan,@PublicDate,@PublicMan,@PublicCode,@NoticeDate,@NoticeMan,@NoticeCode,@CJR,@CJSJ,@XGR,@XGSJ,@Valid,@Memo,@CodeDate,@CodeMan,@SheBaoCheck,@PSN_Level,@OtherDeptCheckDate,@OtherDeptCheckResult,@OtherDeptCheckRemark,@OtherDeptCheckMan,@ENT_ContractType)";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, _ApplyMDL.ApplyID));
            p.Add(db.CreateParameter("ApplyType", DbType.String, _ApplyMDL.ApplyType));
            p.Add(db.CreateParameter("ApplyTypeSub", DbType.String, _ApplyMDL.ApplyTypeSub));
            p.Add(db.CreateParameter("ENT_ServerID", DbType.String, _ApplyMDL.ENT_ServerID));
            p.Add(db.CreateParameter("ENT_Name", DbType.String, _ApplyMDL.ENT_Name));
            p.Add(db.CreateParameter("ENT_OrganizationsCode", DbType.String, _ApplyMDL.ENT_OrganizationsCode));
            p.Add(db.CreateParameter("ENT_City", DbType.String, _ApplyMDL.ENT_City));
            p.Add(db.CreateParameter("ENT_ContractStartTime", DbType.DateTime, _ApplyMDL.ENT_ContractStartTime));
            p.Add(db.CreateParameter("ENT_ContractENDTime", DbType.DateTime, _ApplyMDL.ENT_ContractENDTime));
            p.Add(db.CreateParameter("PSN_ServerID", DbType.String, _ApplyMDL.PSN_ServerID));
            p.Add(db.CreateParameter("PSN_Name", DbType.String, _ApplyMDL.PSN_Name));
            p.Add(db.CreateParameter("PSN_Sex", DbType.String, _ApplyMDL.PSN_Sex));
            p.Add(db.CreateParameter("PSN_CertificateType", DbType.String, _ApplyMDL.PSN_CertificateType));
            p.Add(db.CreateParameter("PSN_CertificateNO", DbType.String, _ApplyMDL.PSN_CertificateNO));
            p.Add(db.CreateParameter("PSN_RegisterNo", DbType.String, _ApplyMDL.PSN_RegisterNo));
            p.Add(db.CreateParameter("PSN_RegisterCertificateNo", DbType.String, _ApplyMDL.PSN_RegisterCertificateNo));
            p.Add(db.CreateParameter("PSN_RegisteProfession", DbType.String, _ApplyMDL.PSN_RegisteProfession));
            p.Add(db.CreateParameter("ApplyTime", DbType.DateTime, _ApplyMDL.ApplyTime));
            p.Add(db.CreateParameter("ApplyCode", DbType.String, _ApplyMDL.ApplyCode));
            p.Add(db.CreateParameter("ApplyStatus", DbType.String, _ApplyMDL.ApplyStatus));
            p.Add(db.CreateParameter("GetDateTime", DbType.DateTime, _ApplyMDL.GetDateTime));
            p.Add(db.CreateParameter("GetResult", DbType.String, _ApplyMDL.GetResult));
            p.Add(db.CreateParameter("GetRemark", DbType.String, _ApplyMDL.GetRemark));
            p.Add(db.CreateParameter("GetMan", DbType.String, _ApplyMDL.GetMan));
            p.Add(db.CreateParameter("ExamineDatetime", DbType.DateTime, _ApplyMDL.ExamineDatetime));
            p.Add(db.CreateParameter("ExamineResult", DbType.String, _ApplyMDL.ExamineResult));
            p.Add(db.CreateParameter("ExamineRemark", DbType.String, _ApplyMDL.ExamineRemark));
            p.Add(db.CreateParameter("ExamineMan", DbType.String, _ApplyMDL.ExamineMan));
            p.Add(db.CreateParameter("ReportDate", DbType.DateTime, _ApplyMDL.ReportDate));
            p.Add(db.CreateParameter("ReportMan", DbType.String, _ApplyMDL.ReportMan));
            p.Add(db.CreateParameter("ReportCode", DbType.String, _ApplyMDL.ReportCode));
            p.Add(db.CreateParameter("AcceptDate", DbType.DateTime, _ApplyMDL.AcceptDate));
            p.Add(db.CreateParameter("AcceptMan", DbType.String, _ApplyMDL.AcceptMan));
            p.Add(db.CreateParameter("CheckDate", DbType.DateTime, _ApplyMDL.CheckDate));
            p.Add(db.CreateParameter("CheckResult", DbType.String, _ApplyMDL.CheckResult));
            p.Add(db.CreateParameter("CheckRemark", DbType.String, _ApplyMDL.CheckRemark));
            p.Add(db.CreateParameter("CheckMan", DbType.String, _ApplyMDL.CheckMan));
            p.Add(db.CreateParameter("ConfirmDate", DbType.DateTime, _ApplyMDL.ConfirmDate));
            p.Add(db.CreateParameter("ConfirmResult", DbType.String, _ApplyMDL.ConfirmResult));
            p.Add(db.CreateParameter("ConfirmRemark", DbType.String, _ApplyMDL.ConfirmRemark));
            p.Add(db.CreateParameter("ConfirmMan", DbType.String, _ApplyMDL.ConfirmMan));
            p.Add(db.CreateParameter("PublicDate", DbType.DateTime, _ApplyMDL.PublicDate));
            p.Add(db.CreateParameter("PublicMan", DbType.String, _ApplyMDL.PublicMan));
            p.Add(db.CreateParameter("PublicCode", DbType.String, _ApplyMDL.PublicCode));
            p.Add(db.CreateParameter("NoticeDate", DbType.DateTime, _ApplyMDL.NoticeDate));
            p.Add(db.CreateParameter("NoticeMan", DbType.String, _ApplyMDL.NoticeMan));
            p.Add(db.CreateParameter("NoticeCode", DbType.String, _ApplyMDL.NoticeCode));
            p.Add(db.CreateParameter("CJR", DbType.String, _ApplyMDL.CJR));
            p.Add(db.CreateParameter("CJSJ", DbType.DateTime, _ApplyMDL.CJSJ));
            p.Add(db.CreateParameter("XGR", DbType.String, _ApplyMDL.XGR));
            p.Add(db.CreateParameter("XGSJ", DbType.DateTime, _ApplyMDL.XGSJ));
            p.Add(db.CreateParameter("Valid", DbType.Int32, _ApplyMDL.Valid));
            p.Add(db.CreateParameter("Memo", DbType.String, _ApplyMDL.Memo));
            p.Add(db.CreateParameter("CodeMan", DbType.String, _ApplyMDL.CodeMan));
            p.Add(db.CreateParameter("CodeDate", DbType.DateTime, _ApplyMDL.CodeDate));
            p.Add(db.CreateParameter("SheBaoCheck", DbType.Int32, _ApplyMDL.SheBaoCheck));
            p.Add(db.CreateParameter("PSN_Level", DbType.String, psn_level));
            p.Add(db.CreateParameter("OtherDeptCheckDate", DbType.DateTime, _ApplyMDL.OtherDeptCheckDate));
            p.Add(db.CreateParameter("OtherDeptCheckResult", DbType.String, _ApplyMDL.OtherDeptCheckResult));
            p.Add(db.CreateParameter("OtherDeptCheckRemark", DbType.String, _ApplyMDL.OtherDeptCheckRemark));
            p.Add(db.CreateParameter("OtherDeptCheckMan", DbType.String, _ApplyMDL.OtherDeptCheckMan));
            p.Add(db.CreateParameter("ENT_ContractType", DbType.Int32, _ApplyMDL.ENT_ContractType));
            p.Add(db.CreateParameter("OldEnt_QYZJJGDM", DbType.String, _ApplyMDL.OldEnt_QYZJJGDM));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="ApplyMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(ApplyMDL _ApplyMDL)
        {
            return Update(null, _ApplyMDL);
        }
        /// <summary>
        /// 给初始注册一个新的建造师ID
        /// </summary>
        /// <param name="tran">事物</param>
        /// <param name="psn_serverid">建造师ID</param>
        /// <param name="applyid">申请表ID</param>
        /// <returns></returns>
        public static int UpdateApplyFirst(DbTransaction tran, string psn_serverid, string applyid)
        {
            string sql = string.Format("Update Apply set PSN_ServerID='{0}' Where ApplyID='{1}'", psn_serverid, applyid);
            return (new DBHelper()).GetExcuteNonQuery(tran, sql);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="applyid">申请表ID</param>
        /// <returns></returns>
        public static int UpdateApply(string applyid, string applystatus, DateTime? applytime, string xgr, DateTime xgsj)
        {
            string sql = string.Format("Update Apply set ApplyStatus='{0}',ApplyTime='{1}',XGR='{2}',XGSJ='{3}' where applyid in({4})", applystatus, applytime, xgr, xgsj, applyid);
            return (new DBHelper()).GetExcuteNonQuery(sql);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ApplyMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, ApplyMDL _ApplyMDL)
        {
            string sql = @"
			UPDATE dbo.Apply
				SET	ApplyType = @ApplyType,ApplyTypeSub = @ApplyTypeSub,ENT_ServerID = @ENT_ServerID,ENT_Name = @ENT_Name,ENT_OrganizationsCode = @ENT_OrganizationsCode,ENT_City = @ENT_City,ENT_ContractStartTime = @ENT_ContractStartTime,ENT_ContractENDTime = @ENT_ContractENDTime,PSN_ServerID = @PSN_ServerID,PSN_Name = @PSN_Name,PSN_Sex = @PSN_Sex,PSN_CertificateType = @PSN_CertificateType,PSN_CertificateNO = @PSN_CertificateNO,PSN_RegisterNo = @PSN_RegisterNo,PSN_RegisterCertificateNo = @PSN_RegisterCertificateNo,PSN_RegisteProfession = @PSN_RegisteProfession,ApplyTime = @ApplyTime,ApplyCode = @ApplyCode,ApplyStatus = @ApplyStatus,GetDateTime = @GetDateTime,GetResult = @GetResult,GetRemark = @GetRemark,GetMan = @GetMan,ExamineDatetime = @ExamineDatetime,ExamineResult = @ExamineResult,ExamineRemark = @ExamineRemark,ExamineMan = @ExamineMan,ReportDate = @ReportDate,ReportMan = @ReportMan,ReportCode = @ReportCode,AcceptDate = @AcceptDate,AcceptMan = @AcceptMan,CheckDate = @CheckDate,CheckResult = @CheckResult,CheckRemark = @CheckRemark,CheckMan = @CheckMan,ConfirmDate = @ConfirmDate,ConfirmResult = @ConfirmResult,ConfirmRemark = @ConfirmRemark,ConfirmMan = @ConfirmMan,PublicDate = @PublicDate,PublicMan = @PublicMan,PublicCode = @PublicCode,NoticeDate = @NoticeDate,NoticeMan = @NoticeMan,NoticeCode = @NoticeCode,CJR = @CJR,CJSJ = @CJSJ,XGR = @XGR,XGSJ = @XGSJ,""VALID"" = @Valid,Memo = @Memo,CodeDate = @CodeDate,CodeMan = @CodeMan,SheBaoCheck = @SheBaoCheck,[OtherDeptCheckDate] = @OtherDeptCheckDate,[OtherDeptCheckResult] = @OtherDeptCheckResult,[OtherDeptCheckRemark] = @OtherDeptCheckRemark,[OtherDeptCheckMan] = @OtherDeptCheckMan,OldUnitCheckTime =@OldUnitCheckTime,OldUnitCheckResult=@OldUnitCheckResult,OldUnitCheckRemark=@OldUnitCheckRemark,OldEnt_QYZJJGDM =@OldEnt_QYZJJGDM,newUnitCheckTime =@newUnitCheckTime,newUnitCheckResult=@newUnitCheckResult,newUnitCheckRemark=@newUnitCheckRemark,[ENT_ContractType] = @ENT_ContractType,LastBackResult= @LastBackResult
			WHERE
				ApplyID = @ApplyID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, _ApplyMDL.ApplyID));
            p.Add(db.CreateParameter("ApplyType", DbType.String, _ApplyMDL.ApplyType));
            p.Add(db.CreateParameter("ApplyTypeSub", DbType.String, _ApplyMDL.ApplyTypeSub));
            p.Add(db.CreateParameter("ENT_ServerID", DbType.String, _ApplyMDL.ENT_ServerID));
            p.Add(db.CreateParameter("ENT_Name", DbType.String, _ApplyMDL.ENT_Name));
            p.Add(db.CreateParameter("ENT_OrganizationsCode", DbType.String, _ApplyMDL.ENT_OrganizationsCode));
            p.Add(db.CreateParameter("ENT_City", DbType.String, _ApplyMDL.ENT_City));
            p.Add(db.CreateParameter("ENT_ContractStartTime", DbType.DateTime, _ApplyMDL.ENT_ContractStartTime));
            p.Add(db.CreateParameter("ENT_ContractENDTime", DbType.DateTime, _ApplyMDL.ENT_ContractENDTime));
            p.Add(db.CreateParameter("PSN_ServerID", DbType.String, _ApplyMDL.PSN_ServerID));
            p.Add(db.CreateParameter("PSN_Name", DbType.String, _ApplyMDL.PSN_Name));
            p.Add(db.CreateParameter("PSN_Sex", DbType.String, _ApplyMDL.PSN_Sex));
            p.Add(db.CreateParameter("PSN_CertificateType", DbType.String, _ApplyMDL.PSN_CertificateType));
            p.Add(db.CreateParameter("PSN_CertificateNO", DbType.String, _ApplyMDL.PSN_CertificateNO));
            p.Add(db.CreateParameter("PSN_RegisterNo", DbType.String, _ApplyMDL.PSN_RegisterNo));
            p.Add(db.CreateParameter("PSN_RegisterCertificateNo", DbType.String, _ApplyMDL.PSN_RegisterCertificateNo));
            p.Add(db.CreateParameter("PSN_RegisteProfession", DbType.String, _ApplyMDL.PSN_RegisteProfession));
            p.Add(db.CreateParameter("ApplyTime", DbType.DateTime, _ApplyMDL.ApplyTime));
            p.Add(db.CreateParameter("ApplyCode", DbType.String, _ApplyMDL.ApplyCode));
            p.Add(db.CreateParameter("ApplyStatus", DbType.String, _ApplyMDL.ApplyStatus));
            p.Add(db.CreateParameter("GetDateTime", DbType.DateTime, _ApplyMDL.GetDateTime));
            p.Add(db.CreateParameter("GetResult", DbType.String, _ApplyMDL.GetResult));
            p.Add(db.CreateParameter("GetRemark", DbType.String, _ApplyMDL.GetRemark));
            p.Add(db.CreateParameter("GetMan", DbType.String, _ApplyMDL.GetMan));
            p.Add(db.CreateParameter("ExamineDatetime", DbType.DateTime, _ApplyMDL.ExamineDatetime));
            p.Add(db.CreateParameter("ExamineResult", DbType.String, _ApplyMDL.ExamineResult));
            p.Add(db.CreateParameter("ExamineRemark", DbType.String, _ApplyMDL.ExamineRemark));
            p.Add(db.CreateParameter("ExamineMan", DbType.String, _ApplyMDL.ExamineMan));
            p.Add(db.CreateParameter("ReportDate", DbType.DateTime, _ApplyMDL.ReportDate));
            p.Add(db.CreateParameter("ReportMan", DbType.String, _ApplyMDL.ReportMan));
            p.Add(db.CreateParameter("ReportCode", DbType.String, _ApplyMDL.ReportCode));
            p.Add(db.CreateParameter("AcceptDate", DbType.DateTime, _ApplyMDL.AcceptDate));
            p.Add(db.CreateParameter("AcceptMan", DbType.String, _ApplyMDL.AcceptMan));
            p.Add(db.CreateParameter("CheckDate", DbType.DateTime, _ApplyMDL.CheckDate));
            p.Add(db.CreateParameter("CheckResult", DbType.String, _ApplyMDL.CheckResult));
            p.Add(db.CreateParameter("CheckRemark", DbType.String, _ApplyMDL.CheckRemark));
            p.Add(db.CreateParameter("CheckMan", DbType.String, _ApplyMDL.CheckMan));
            p.Add(db.CreateParameter("ConfirmDate", DbType.DateTime, _ApplyMDL.ConfirmDate));
            p.Add(db.CreateParameter("ConfirmResult", DbType.String, _ApplyMDL.ConfirmResult));
            p.Add(db.CreateParameter("ConfirmRemark", DbType.String, _ApplyMDL.ConfirmRemark));
            p.Add(db.CreateParameter("ConfirmMan", DbType.String, _ApplyMDL.ConfirmMan));
            p.Add(db.CreateParameter("PublicDate", DbType.DateTime, _ApplyMDL.PublicDate));
            p.Add(db.CreateParameter("PublicMan", DbType.String, _ApplyMDL.PublicMan));
            p.Add(db.CreateParameter("PublicCode", DbType.String, _ApplyMDL.PublicCode));
            p.Add(db.CreateParameter("NoticeDate", DbType.DateTime, _ApplyMDL.NoticeDate));
            p.Add(db.CreateParameter("NoticeMan", DbType.String, _ApplyMDL.NoticeMan));
            p.Add(db.CreateParameter("NoticeCode", DbType.String, _ApplyMDL.NoticeCode));
            p.Add(db.CreateParameter("CJR", DbType.String, _ApplyMDL.CJR));
            p.Add(db.CreateParameter("CJSJ", DbType.DateTime, _ApplyMDL.CJSJ));
            p.Add(db.CreateParameter("XGR", DbType.String, _ApplyMDL.XGR));
            p.Add(db.CreateParameter("XGSJ", DbType.DateTime, _ApplyMDL.XGSJ));
            p.Add(db.CreateParameter("Valid", DbType.Int32, _ApplyMDL.Valid));
            p.Add(db.CreateParameter("Memo", DbType.String, _ApplyMDL.Memo));
            p.Add(db.CreateParameter("CodeDate", DbType.DateTime, _ApplyMDL.CodeDate));
            p.Add(db.CreateParameter("CodeMan", DbType.String, _ApplyMDL.CodeMan));
            p.Add(db.CreateParameter("SheBaoCheck", DbType.Int32, _ApplyMDL.SheBaoCheck));
            p.Add(db.CreateParameter("OtherDeptCheckDate", DbType.DateTime, _ApplyMDL.OtherDeptCheckDate));
            p.Add(db.CreateParameter("OtherDeptCheckResult", DbType.String, _ApplyMDL.OtherDeptCheckResult));
            p.Add(db.CreateParameter("OtherDeptCheckRemark", DbType.String, _ApplyMDL.OtherDeptCheckRemark));
            p.Add(db.CreateParameter("OtherDeptCheckMan", DbType.String, _ApplyMDL.OtherDeptCheckMan));
            p.Add(db.CreateParameter("OldUnitCheckTime", DbType.DateTime, _ApplyMDL.OldUnitCheckTime));
            p.Add(db.CreateParameter("OldUnitCheckResult", DbType.String, _ApplyMDL.OldUnitCheckResult));
            p.Add(db.CreateParameter("OldUnitCheckRemark", DbType.String, _ApplyMDL.OldUnitCheckRemark));

            p.Add(db.CreateParameter("OldEnt_QYZJJGDM", DbType.String, _ApplyMDL.OldEnt_QYZJJGDM));
            p.Add(db.CreateParameter("newUnitCheckTime", DbType.DateTime, _ApplyMDL.newUnitCheckTime));
            p.Add(db.CreateParameter("newUnitCheckResult", DbType.String, _ApplyMDL.newUnitCheckResult));
            p.Add(db.CreateParameter("newUnitCheckRemark", DbType.String, _ApplyMDL.newUnitCheckRemark));
            p.Add(db.CreateParameter("ENT_ContractType", DbType.Int32, _ApplyMDL.ENT_ContractType));
            p.Add(db.CreateParameter("LastBackResult", DbType.String, _ApplyMDL.LastBackResult));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 删除传过来的ID
        /// </summary>
        /// <param name="applyid">申请表ID</param>
        /// <returns></returns>
        public static int DeleteApply(string applyid)
        {
            string sql = string.Format("DELETE FROM dbo.Apply WHERE ApplyType = '变更注册' AND ApplyTypeSub = '企业信息变更' AND ApplyStatus='未申报' AND ApplyID in({0})", applyid);
            return (new DBHelper()).GetExcuteNonQuery(sql);
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
            string sql = @"DELETE FROM dbo.Apply WHERE ApplyID = @ApplyID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, ApplyID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_ApplyMDL">对象实体类</param>
        /// <returns></returns>
        public static int Delete(ApplyMDL _ApplyMDL)
        {
            return Delete(null, _ApplyMDL);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_ApplyMDL">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, ApplyMDL _ApplyMDL)
        {
            string sql = @"DELETE FROM dbo.Apply WHERE ApplyID = @ApplyID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, _ApplyMDL.ApplyID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ApplyID">主键</param>
        public static ApplyMDL GetObject(string ApplyID)
        {
            string sql = @"
			SELECT ApplyID,ApplyType,ApplyTypeSub,ENT_ServerID,ENT_Name,ENT_OrganizationsCode,ENT_City,ENT_ContractStartTime,ENT_ContractENDTime,PSN_ServerID,PSN_Name,PSN_Sex,PSN_CertificateType,PSN_CertificateNO,PSN_RegisterNo,PSN_RegisterCertificateNo,PSN_RegisteProfession,ApplyTime,ApplyCode,ApplyStatus,GetDateTime,GetResult,GetRemark,GetMan,ExamineDatetime,ExamineResult,ExamineRemark,ExamineMan,ReportDate,ReportMan,ReportCode,AcceptDate,AcceptMan,CheckDate,CheckResult,CheckRemark,CheckMan,ConfirmDate,ConfirmResult,ConfirmRemark,ConfirmMan,PublicDate,PublicMan,PublicCode,NoticeDate,NoticeMan,NoticeCode,CJR,CJSJ,XGR,XGSJ,[VALID],Memo,CodeDate,CodeMan,SheBaoCheck,PSN_Level,OtherDeptCheckDate,OtherDeptCheckResult,OtherDeptCheckRemark,OtherDeptCheckMan,OldUnitCheckTime,OldUnitCheckResult,OldUnitCheckRemark,OldEnt_QYZJJGDM,newUnitCheckTime,newUnitCheckResult,newUnitCheckRemark,[CheckZSSD],[CheckXSL],[CheckCFZC],ENT_ContractType,LastBackResult
            FROM dbo.Apply
			WHERE ApplyID = @ApplyID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, ApplyID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                ApplyMDL _ApplyMDL = null;
                if (reader.Read())
                {
                    _ApplyMDL = new ApplyMDL();
                    if (reader["ApplyID"] != DBNull.Value) _ApplyMDL.ApplyID = Convert.ToString(reader["ApplyID"]);
                    if (reader["ApplyType"] != DBNull.Value) _ApplyMDL.ApplyType = Convert.ToString(reader["ApplyType"]);
                    if (reader["ApplyTypeSub"] != DBNull.Value) _ApplyMDL.ApplyTypeSub = Convert.ToString(reader["ApplyTypeSub"]);
                    if (reader["ENT_ServerID"] != DBNull.Value) _ApplyMDL.ENT_ServerID = Convert.ToString(reader["ENT_ServerID"]);
                    if (reader["ENT_Name"] != DBNull.Value) _ApplyMDL.ENT_Name = Convert.ToString(reader["ENT_Name"]);
                    if (reader["ENT_OrganizationsCode"] != DBNull.Value) _ApplyMDL.ENT_OrganizationsCode = Convert.ToString(reader["ENT_OrganizationsCode"]);
                    if (reader["ENT_City"] != DBNull.Value) _ApplyMDL.ENT_City = Convert.ToString(reader["ENT_City"]);
                    if (reader["ENT_ContractStartTime"] != DBNull.Value) _ApplyMDL.ENT_ContractStartTime = Convert.ToDateTime(reader["ENT_ContractStartTime"]);
                    if (reader["ENT_ContractENDTime"] != DBNull.Value) _ApplyMDL.ENT_ContractENDTime = Convert.ToDateTime(reader["ENT_ContractENDTime"]);
                    if (reader["PSN_ServerID"] != DBNull.Value) _ApplyMDL.PSN_ServerID = Convert.ToString(reader["PSN_ServerID"]);
                    if (reader["PSN_Name"] != DBNull.Value) _ApplyMDL.PSN_Name = Convert.ToString(reader["PSN_Name"]);
                    if (reader["PSN_Sex"] != DBNull.Value) _ApplyMDL.PSN_Sex = Convert.ToString(reader["PSN_Sex"]);
                    if (reader["PSN_CertificateType"] != DBNull.Value) _ApplyMDL.PSN_CertificateType = Convert.ToString(reader["PSN_CertificateType"]);
                    if (reader["PSN_CertificateNO"] != DBNull.Value) _ApplyMDL.PSN_CertificateNO = Convert.ToString(reader["PSN_CertificateNO"]);
                    if (reader["PSN_RegisterNo"] != DBNull.Value) _ApplyMDL.PSN_RegisterNo = Convert.ToString(reader["PSN_RegisterNo"]);
                    if (reader["PSN_RegisterCertificateNo"] != DBNull.Value) _ApplyMDL.PSN_RegisterCertificateNo = Convert.ToString(reader["PSN_RegisterCertificateNo"]);
                    if (reader["PSN_RegisteProfession"] != DBNull.Value) _ApplyMDL.PSN_RegisteProfession = Convert.ToString(reader["PSN_RegisteProfession"]);
                    if (reader["ApplyTime"] != DBNull.Value) _ApplyMDL.ApplyTime = Convert.ToDateTime(reader["ApplyTime"]);
                    if (reader["ApplyCode"] != DBNull.Value) _ApplyMDL.ApplyCode = Convert.ToString(reader["ApplyCode"]);
                    if (reader["ApplyStatus"] != DBNull.Value) _ApplyMDL.ApplyStatus = Convert.ToString(reader["ApplyStatus"]);
                    if (reader["GetDateTime"] != DBNull.Value) _ApplyMDL.GetDateTime = Convert.ToDateTime(reader["GetDateTime"]);
                    if (reader["GetResult"] != DBNull.Value) _ApplyMDL.GetResult = Convert.ToString(reader["GetResult"]);
                    if (reader["GetRemark"] != DBNull.Value) _ApplyMDL.GetRemark = Convert.ToString(reader["GetRemark"]);
                    if (reader["GetMan"] != DBNull.Value) _ApplyMDL.GetMan = Convert.ToString(reader["GetMan"]);
                    if (reader["ExamineDatetime"] != DBNull.Value) _ApplyMDL.ExamineDatetime = Convert.ToDateTime(reader["ExamineDatetime"]);
                    if (reader["ExamineResult"] != DBNull.Value) _ApplyMDL.ExamineResult = Convert.ToString(reader["ExamineResult"]);
                    if (reader["ExamineRemark"] != DBNull.Value) _ApplyMDL.ExamineRemark = Convert.ToString(reader["ExamineRemark"]);
                    if (reader["ExamineMan"] != DBNull.Value) _ApplyMDL.ExamineMan = Convert.ToString(reader["ExamineMan"]);
                    if (reader["ReportDate"] != DBNull.Value) _ApplyMDL.ReportDate = Convert.ToDateTime(reader["ReportDate"]);
                    if (reader["ReportMan"] != DBNull.Value) _ApplyMDL.ReportMan = Convert.ToString(reader["ReportMan"]);
                    if (reader["ReportCode"] != DBNull.Value) _ApplyMDL.ReportCode = Convert.ToString(reader["ReportCode"]);
                    if (reader["AcceptDate"] != DBNull.Value) _ApplyMDL.AcceptDate = Convert.ToDateTime(reader["AcceptDate"]);
                    if (reader["AcceptMan"] != DBNull.Value) _ApplyMDL.AcceptMan = Convert.ToString(reader["AcceptMan"]);
                    if (reader["CheckDate"] != DBNull.Value) _ApplyMDL.CheckDate = Convert.ToDateTime(reader["CheckDate"]);
                    if (reader["CheckResult"] != DBNull.Value) _ApplyMDL.CheckResult = Convert.ToString(reader["CheckResult"]);
                    if (reader["CheckRemark"] != DBNull.Value) _ApplyMDL.CheckRemark = Convert.ToString(reader["CheckRemark"]);
                    if (reader["CheckMan"] != DBNull.Value) _ApplyMDL.CheckMan = Convert.ToString(reader["CheckMan"]);
                    if (reader["ConfirmDate"] != DBNull.Value) _ApplyMDL.ConfirmDate = Convert.ToDateTime(reader["ConfirmDate"]);
                    if (reader["ConfirmResult"] != DBNull.Value) _ApplyMDL.ConfirmResult = Convert.ToString(reader["ConfirmResult"]);
                    if (reader["ConfirmRemark"] != DBNull.Value) _ApplyMDL.ConfirmRemark = Convert.ToString(reader["ConfirmRemark"]);                    
                    if (reader["ConfirmMan"] != DBNull.Value) _ApplyMDL.ConfirmMan = Convert.ToString(reader["ConfirmMan"]);
                    if (reader["PublicDate"] != DBNull.Value) _ApplyMDL.PublicDate = Convert.ToDateTime(reader["PublicDate"]);
                    if (reader["PublicMan"] != DBNull.Value) _ApplyMDL.PublicMan = Convert.ToString(reader["PublicMan"]);
                    if (reader["PublicCode"] != DBNull.Value) _ApplyMDL.PublicCode = Convert.ToString(reader["PublicCode"]);
                    if (reader["NoticeDate"] != DBNull.Value) _ApplyMDL.NoticeDate = Convert.ToDateTime(reader["NoticeDate"]);
                    if (reader["NoticeMan"] != DBNull.Value) _ApplyMDL.NoticeMan = Convert.ToString(reader["NoticeMan"]);
                    if (reader["NoticeCode"] != DBNull.Value) _ApplyMDL.NoticeCode = Convert.ToString(reader["NoticeCode"]);
                    if (reader["CJR"] != DBNull.Value) _ApplyMDL.CJR = Convert.ToString(reader["CJR"]);
                    if (reader["CJSJ"] != DBNull.Value) _ApplyMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
                    if (reader["XGR"] != DBNull.Value) _ApplyMDL.XGR = Convert.ToString(reader["XGR"]);
                    if (reader["XGSJ"] != DBNull.Value) _ApplyMDL.XGSJ = Convert.ToDateTime(reader["XGSJ"]);
                    if (reader["Valid"] != DBNull.Value) _ApplyMDL.Valid = Convert.ToInt32(reader["Valid"]);
                    if (reader["Memo"] != DBNull.Value) _ApplyMDL.Memo = Convert.ToString(reader["Memo"]);
                    if (reader["CodeDate"] != DBNull.Value) _ApplyMDL.CodeDate = Convert.ToDateTime(reader["CodeDate"]);
                    if (reader["CodeMan"] != DBNull.Value) _ApplyMDL.CodeMan = Convert.ToString(reader["CodeMan"]);
                    if (reader["SheBaoCheck"] != DBNull.Value) _ApplyMDL.SheBaoCheck = Convert.ToInt32(reader["SheBaoCheck"]);
                    if (reader["PSN_Level"] != DBNull.Value) _ApplyMDL.PSN_Level = Convert.ToString(reader["PSN_Level"]);
                    if (reader["OtherDeptCheckDate"] != DBNull.Value) _ApplyMDL.OtherDeptCheckDate = Convert.ToDateTime(reader["OtherDeptCheckDate"]);
                    if (reader["OtherDeptCheckResult"] != DBNull.Value) _ApplyMDL.OtherDeptCheckResult = Convert.ToString(reader["OtherDeptCheckResult"]);
                    if (reader["OtherDeptCheckRemark"] != DBNull.Value) _ApplyMDL.OtherDeptCheckRemark = Convert.ToString(reader["OtherDeptCheckRemark"]);
                    if (reader["OtherDeptCheckMan"] != DBNull.Value) _ApplyMDL.OtherDeptCheckMan = Convert.ToString(reader["OtherDeptCheckMan"]);
                    if (reader["OldUnitCheckTime"] != DBNull.Value) _ApplyMDL.OldUnitCheckTime = Convert.ToDateTime(reader["OldUnitCheckTime"]);
                    if (reader["OldUnitCheckResult"] != DBNull.Value) _ApplyMDL.OldUnitCheckResult = Convert.ToString(reader["OldUnitCheckResult"]);
                    if (reader["OldUnitCheckRemark"] != DBNull.Value) _ApplyMDL.OldUnitCheckRemark = Convert.ToString(reader["OldUnitCheckRemark"]);

                    if (reader["OldEnt_QYZJJGDM"] != DBNull.Value) _ApplyMDL.OldEnt_QYZJJGDM = Convert.ToString(reader["OldEnt_QYZJJGDM"]);
                    if (reader["newUnitCheckTime"] != DBNull.Value) _ApplyMDL.newUnitCheckTime = Convert.ToDateTime(reader["newUnitCheckTime"]);
                    if (reader["newUnitCheckResult"] != DBNull.Value) _ApplyMDL.newUnitCheckResult = Convert.ToString(reader["newUnitCheckResult"]);
                    if (reader["newUnitCheckRemark"] != DBNull.Value) _ApplyMDL.newUnitCheckRemark = Convert.ToString(reader["newUnitCheckRemark"]);

                    if (reader["CheckZSSD"] != DBNull.Value) _ApplyMDL.CheckZSSD = Convert.ToInt32(reader["CheckZSSD"]);
                    if (reader["CheckXSL"] != DBNull.Value) _ApplyMDL.CheckXSL = Convert.ToInt32(reader["CheckXSL"]);
                    if (reader["CheckCFZC"] != DBNull.Value) _ApplyMDL.CheckCFZC = Convert.ToInt32(reader["CheckCFZC"]);
                    if (reader["ENT_ContractType"] != DBNull.Value) _ApplyMDL.ENT_ContractType = Convert.ToInt32(reader["ENT_ContractType"]);

                    if (reader["LastBackResult"] != DBNull.Value) _ApplyMDL.LastBackResult = Convert.ToString(reader["LastBackResult"]);
                    
                }
                reader.Close();
                db.Close();
                return _ApplyMDL;
            }
        }
        /// <summary>
        /// 根据注册编号获取单个实体
        /// </summary>
        /// <param name="PSN_RegisterNo">主键</param>
        public static ApplyMDL GetObjectPSN_RegisterNo(string PSN_RegisterNo)
        {
            string sql = @"
			SELECT ApplyID,ApplyType,ApplyTypeSub,ENT_ServerID,ENT_Name,ENT_OrganizationsCode,ENT_City,ENT_ContractStartTime,ENT_ContractENDTime,PSN_ServerID,PSN_Name,PSN_Sex,PSN_CertificateType,PSN_CertificateNO,PSN_RegisterNo,PSN_RegisterCertificateNo,PSN_RegisteProfession,ApplyTime,ApplyCode,ApplyStatus,GetDateTime,GetResult,GetRemark,GetMan,ExamineDatetime,ExamineResult,ExamineRemark,ExamineMan,ReportDate,ReportMan,ReportCode,AcceptDate,AcceptMan,CheckDate,CheckResult,CheckRemark,CheckMan,ConfirmDate,ConfirmResult,ConfirmRemark,ConfirmMan,PublicDate,PublicMan,PublicCode,NoticeDate,NoticeMan,NoticeCode,CJR,CJSJ,XGR,XGSJ,[VALID],Memo,CodeDate,CodeMan,SheBaoCheck,PSN_Level,OtherDeptCheckDate,OtherDeptCheckResult,OtherDeptCheckRemark,OtherDeptCheckMan,OldUnitCheckTime,OldUnitCheckResult,OldUnitCheckRemark,OldEnt_QYZJJGDM,newUnitCheckTime,newUnitCheckResult,newUnitCheckRemark,ENT_ContractType,LastBackResult
			FROM dbo.Apply
			WHERE PSN_RegisterNo = @PSN_RegisterNo";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PSN_RegisterNo", DbType.String, PSN_RegisterNo));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                ApplyMDL _ApplyMDL = null;
                if (reader.Read())
                {
                    _ApplyMDL = new ApplyMDL();
                    if (reader["ApplyID"] != DBNull.Value) _ApplyMDL.ApplyID = Convert.ToString(reader["ApplyID"]);
                    if (reader["ApplyType"] != DBNull.Value) _ApplyMDL.ApplyType = Convert.ToString(reader["ApplyType"]);
                    if (reader["ApplyTypeSub"] != DBNull.Value) _ApplyMDL.ApplyTypeSub = Convert.ToString(reader["ApplyTypeSub"]);
                    if (reader["ENT_ServerID"] != DBNull.Value) _ApplyMDL.ENT_ServerID = Convert.ToString(reader["ENT_ServerID"]);
                    if (reader["ENT_Name"] != DBNull.Value) _ApplyMDL.ENT_Name = Convert.ToString(reader["ENT_Name"]);
                    if (reader["ENT_OrganizationsCode"] != DBNull.Value) _ApplyMDL.ENT_OrganizationsCode = Convert.ToString(reader["ENT_OrganizationsCode"]);
                    if (reader["ENT_City"] != DBNull.Value) _ApplyMDL.ENT_City = Convert.ToString(reader["ENT_City"]);
                    if (reader["ENT_ContractStartTime"] != DBNull.Value) _ApplyMDL.ENT_ContractStartTime = Convert.ToDateTime(reader["ENT_ContractStartTime"]);
                    if (reader["ENT_ContractENDTime"] != DBNull.Value) _ApplyMDL.ENT_ContractENDTime = Convert.ToDateTime(reader["ENT_ContractENDTime"]);
                    if (reader["PSN_ServerID"] != DBNull.Value) _ApplyMDL.PSN_ServerID = Convert.ToString(reader["PSN_ServerID"]);
                    if (reader["PSN_Name"] != DBNull.Value) _ApplyMDL.PSN_Name = Convert.ToString(reader["PSN_Name"]);
                    if (reader["PSN_Sex"] != DBNull.Value) _ApplyMDL.PSN_Sex = Convert.ToString(reader["PSN_Sex"]);
                    if (reader["PSN_CertificateType"] != DBNull.Value) _ApplyMDL.PSN_CertificateType = Convert.ToString(reader["PSN_CertificateType"]);
                    if (reader["PSN_CertificateNO"] != DBNull.Value) _ApplyMDL.PSN_CertificateNO = Convert.ToString(reader["PSN_CertificateNO"]);
                    if (reader["PSN_RegisterNo"] != DBNull.Value) _ApplyMDL.PSN_RegisterNo = Convert.ToString(reader["PSN_RegisterNo"]);
                    if (reader["PSN_RegisterCertificateNo"] != DBNull.Value) _ApplyMDL.PSN_RegisterCertificateNo = Convert.ToString(reader["PSN_RegisterCertificateNo"]);
                    if (reader["PSN_RegisteProfession"] != DBNull.Value) _ApplyMDL.PSN_RegisteProfession = Convert.ToString(reader["PSN_RegisteProfession"]);
                    if (reader["ApplyTime"] != DBNull.Value) _ApplyMDL.ApplyTime = Convert.ToDateTime(reader["ApplyTime"]);
                    if (reader["ApplyCode"] != DBNull.Value) _ApplyMDL.ApplyCode = Convert.ToString(reader["ApplyCode"]);
                    if (reader["ApplyStatus"] != DBNull.Value) _ApplyMDL.ApplyStatus = Convert.ToString(reader["ApplyStatus"]);
                    if (reader["GetDateTime"] != DBNull.Value) _ApplyMDL.GetDateTime = Convert.ToDateTime(reader["GetDateTime"]);
                    if (reader["GetResult"] != DBNull.Value) _ApplyMDL.GetResult = Convert.ToString(reader["GetResult"]);
                    if (reader["GetRemark"] != DBNull.Value) _ApplyMDL.GetRemark = Convert.ToString(reader["GetRemark"]);
                    if (reader["GetMan"] != DBNull.Value) _ApplyMDL.GetMan = Convert.ToString(reader["GetMan"]);
                    if (reader["ExamineDatetime"] != DBNull.Value) _ApplyMDL.ExamineDatetime = Convert.ToDateTime(reader["ExamineDatetime"]);
                    if (reader["ExamineResult"] != DBNull.Value) _ApplyMDL.ExamineResult = Convert.ToString(reader["ExamineResult"]);
                    if (reader["ExamineRemark"] != DBNull.Value) _ApplyMDL.ExamineRemark = Convert.ToString(reader["ExamineRemark"]);
                    if (reader["ExamineMan"] != DBNull.Value) _ApplyMDL.ExamineMan = Convert.ToString(reader["ExamineMan"]);
                    if (reader["ReportDate"] != DBNull.Value) _ApplyMDL.ReportDate = Convert.ToDateTime(reader["ReportDate"]);
                    if (reader["ReportMan"] != DBNull.Value) _ApplyMDL.ReportMan = Convert.ToString(reader["ReportMan"]);
                    if (reader["ReportCode"] != DBNull.Value) _ApplyMDL.ReportCode = Convert.ToString(reader["ReportCode"]);
                    if (reader["AcceptDate"] != DBNull.Value) _ApplyMDL.AcceptDate = Convert.ToDateTime(reader["AcceptDate"]);
                    if (reader["AcceptMan"] != DBNull.Value) _ApplyMDL.AcceptMan = Convert.ToString(reader["AcceptMan"]);
                    if (reader["CheckDate"] != DBNull.Value) _ApplyMDL.CheckDate = Convert.ToDateTime(reader["CheckDate"]);
                    if (reader["CheckResult"] != DBNull.Value) _ApplyMDL.CheckResult = Convert.ToString(reader["CheckResult"]);
                    if (reader["CheckRemark"] != DBNull.Value) _ApplyMDL.CheckRemark = Convert.ToString(reader["CheckRemark"]);
                    if (reader["CheckMan"] != DBNull.Value) _ApplyMDL.CheckMan = Convert.ToString(reader["CheckMan"]);
                    if (reader["ConfirmDate"] != DBNull.Value) _ApplyMDL.ConfirmDate = Convert.ToDateTime(reader["ConfirmDate"]);
                    if (reader["ConfirmResult"] != DBNull.Value) _ApplyMDL.ConfirmResult = Convert.ToString(reader["ConfirmResult"]);
                    if (reader["ConfirmRemark"] != DBNull.Value) _ApplyMDL.ConfirmRemark = Convert.ToString(reader["ConfirmRemark"]); 
                    if (reader["ConfirmMan"] != DBNull.Value) _ApplyMDL.ConfirmMan = Convert.ToString(reader["ConfirmMan"]);
                    if (reader["PublicDate"] != DBNull.Value) _ApplyMDL.PublicDate = Convert.ToDateTime(reader["PublicDate"]);
                    if (reader["PublicMan"] != DBNull.Value) _ApplyMDL.PublicMan = Convert.ToString(reader["PublicMan"]);
                    if (reader["PublicCode"] != DBNull.Value) _ApplyMDL.PublicCode = Convert.ToString(reader["PublicCode"]);
                    if (reader["NoticeDate"] != DBNull.Value) _ApplyMDL.NoticeDate = Convert.ToDateTime(reader["NoticeDate"]);
                    if (reader["NoticeMan"] != DBNull.Value) _ApplyMDL.NoticeMan = Convert.ToString(reader["NoticeMan"]);
                    if (reader["NoticeCode"] != DBNull.Value) _ApplyMDL.NoticeCode = Convert.ToString(reader["NoticeCode"]);
                    if (reader["CJR"] != DBNull.Value) _ApplyMDL.CJR = Convert.ToString(reader["CJR"]);
                    if (reader["CJSJ"] != DBNull.Value) _ApplyMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
                    if (reader["XGR"] != DBNull.Value) _ApplyMDL.XGR = Convert.ToString(reader["XGR"]);
                    if (reader["XGSJ"] != DBNull.Value) _ApplyMDL.XGSJ = Convert.ToDateTime(reader["XGSJ"]);
                    if (reader["Valid"] != DBNull.Value) _ApplyMDL.Valid = Convert.ToInt32(reader["Valid"]);
                    if (reader["Memo"] != DBNull.Value) _ApplyMDL.Memo = Convert.ToString(reader["Memo"]);
                    if (reader["CodeDate"] != DBNull.Value) _ApplyMDL.CodeDate = Convert.ToDateTime(reader["CodeDate"]);
                    if (reader["CodeMan"] != DBNull.Value) _ApplyMDL.CodeMan = Convert.ToString(reader["CodeMan"]);
                    if (reader["SheBaoCheck"] != DBNull.Value) _ApplyMDL.SheBaoCheck = Convert.ToInt32(reader["SheBaoCheck"]);
                    if (reader["PSN_Level"] != DBNull.Value) _ApplyMDL.PSN_Level = Convert.ToString(reader["PSN_Level"]);
                    if (reader["OtherDeptCheckDate"] != DBNull.Value) _ApplyMDL.OtherDeptCheckDate = Convert.ToDateTime(reader["OtherDeptCheckDate"]);
                    if (reader["OtherDeptCheckResult"] != DBNull.Value) _ApplyMDL.OtherDeptCheckResult = Convert.ToString(reader["OtherDeptCheckResult"]);
                    if (reader["OtherDeptCheckRemark"] != DBNull.Value) _ApplyMDL.OtherDeptCheckRemark = Convert.ToString(reader["OtherDeptCheckRemark"]);
                    if (reader["OtherDeptCheckMan"] != DBNull.Value) _ApplyMDL.OtherDeptCheckMan = Convert.ToString(reader["OtherDeptCheckMan"]);
                    if (reader["OldUnitCheckTime"] != DBNull.Value) _ApplyMDL.OldUnitCheckTime = Convert.ToDateTime(reader["OldUnitCheckTime"]);
                    if (reader["OldUnitCheckResult"] != DBNull.Value) _ApplyMDL.OldUnitCheckResult = Convert.ToString(reader["OldUnitCheckResult"]);
                    if (reader["OldUnitCheckRemark"] != DBNull.Value) _ApplyMDL.OldUnitCheckRemark = Convert.ToString(reader["OldUnitCheckRemark"]);
                    if (reader["OldEnt_QYZJJGDM"] != DBNull.Value) _ApplyMDL.OldEnt_QYZJJGDM = Convert.ToString(reader["OldEnt_QYZJJGDM"]);
                    if (reader["newUnitCheckTime"] != DBNull.Value) _ApplyMDL.newUnitCheckTime = Convert.ToDateTime(reader["newUnitCheckTime"]);
                    if (reader["newUnitCheckResult"] != DBNull.Value) _ApplyMDL.newUnitCheckResult = Convert.ToString(reader["newUnitCheckResult"]);
                    if (reader["newUnitCheckRemark"] != DBNull.Value) _ApplyMDL.newUnitCheckRemark = Convert.ToString(reader["newUnitCheckRemark"]);
                    if (reader["ENT_ContractType"] != DBNull.Value) _ApplyMDL.ENT_ContractType = Convert.ToInt32(reader["ENT_ContractType"]);

                    if (reader["LastBackResult"] != DBNull.Value) _ApplyMDL.LastBackResult = Convert.ToString(reader["LastBackResult"]);
                }
                reader.Close();
                db.Close();
                return _ApplyMDL;
            }
        }

        /// <summary>
        /// 获取本次申请前最后一次注册申请信息
        /// </summary>
        /// <param name="ApplyID">本次申请ID</param>
        public static ApplyMDL GetLastApplyObject(string ApplyID)
        {
            string sql = @"
			SELECT top 1 ApplyID,ApplyType,ApplyTypeSub,ENT_ServerID,ENT_Name,ENT_OrganizationsCode,ENT_City,ENT_ContractStartTime,ENT_ContractENDTime,PSN_ServerID,PSN_Name,PSN_Sex,PSN_CertificateType,PSN_CertificateNO,PSN_RegisterNo,PSN_RegisterCertificateNo,PSN_RegisteProfession,ApplyTime,ApplyCode,ApplyStatus,GetDateTime,GetResult,GetRemark,GetMan,ExamineDatetime,ExamineResult,ExamineRemark,ExamineMan,ReportDate,ReportMan,ReportCode,AcceptDate,AcceptMan,CheckDate,CheckResult,CheckRemark,CheckMan,ConfirmDate,ConfirmResult,ConfirmRemark,ConfirmMan,PublicDate,PublicMan,PublicCode,NoticeDate,NoticeMan,NoticeCode,CJR,CJSJ,XGR,XGSJ,[VALID],Memo,CodeDate,CodeMan,SheBaoCheck,PSN_Level,OtherDeptCheckDate,OtherDeptCheckResult,OtherDeptCheckRemark,OtherDeptCheckMan,OldUnitCheckTime,OldUnitCheckResult,OldUnitCheckRemark,OldEnt_QYZJJGDM,newUnitCheckTime,newUnitCheckResult,newUnitCheckRemark,[CheckZSSD],[CheckXSL],[CheckCFZC],ENT_ContractType,LastBackResult
            FROM dbo.Apply
			WHERE PSN_CertificateNO = (select PSN_CertificateNO FROM dbo.Apply where ApplyID = @ApplyID) and  ApplyID <> @ApplyID
            order by [ApplyTime] desc";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, ApplyID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                ApplyMDL _ApplyMDL = null;
                if (reader.Read())
                {
                    _ApplyMDL = new ApplyMDL();
                    if (reader["ApplyID"] != DBNull.Value) _ApplyMDL.ApplyID = Convert.ToString(reader["ApplyID"]);
                    if (reader["ApplyType"] != DBNull.Value) _ApplyMDL.ApplyType = Convert.ToString(reader["ApplyType"]);
                    if (reader["ApplyTypeSub"] != DBNull.Value) _ApplyMDL.ApplyTypeSub = Convert.ToString(reader["ApplyTypeSub"]);
                    if (reader["ENT_ServerID"] != DBNull.Value) _ApplyMDL.ENT_ServerID = Convert.ToString(reader["ENT_ServerID"]);
                    if (reader["ENT_Name"] != DBNull.Value) _ApplyMDL.ENT_Name = Convert.ToString(reader["ENT_Name"]);
                    if (reader["ENT_OrganizationsCode"] != DBNull.Value) _ApplyMDL.ENT_OrganizationsCode = Convert.ToString(reader["ENT_OrganizationsCode"]);
                    if (reader["ENT_City"] != DBNull.Value) _ApplyMDL.ENT_City = Convert.ToString(reader["ENT_City"]);
                    if (reader["ENT_ContractStartTime"] != DBNull.Value) _ApplyMDL.ENT_ContractStartTime = Convert.ToDateTime(reader["ENT_ContractStartTime"]);
                    if (reader["ENT_ContractENDTime"] != DBNull.Value) _ApplyMDL.ENT_ContractENDTime = Convert.ToDateTime(reader["ENT_ContractENDTime"]);
                    if (reader["PSN_ServerID"] != DBNull.Value) _ApplyMDL.PSN_ServerID = Convert.ToString(reader["PSN_ServerID"]);
                    if (reader["PSN_Name"] != DBNull.Value) _ApplyMDL.PSN_Name = Convert.ToString(reader["PSN_Name"]);
                    if (reader["PSN_Sex"] != DBNull.Value) _ApplyMDL.PSN_Sex = Convert.ToString(reader["PSN_Sex"]);
                    if (reader["PSN_CertificateType"] != DBNull.Value) _ApplyMDL.PSN_CertificateType = Convert.ToString(reader["PSN_CertificateType"]);
                    if (reader["PSN_CertificateNO"] != DBNull.Value) _ApplyMDL.PSN_CertificateNO = Convert.ToString(reader["PSN_CertificateNO"]);
                    if (reader["PSN_RegisterNo"] != DBNull.Value) _ApplyMDL.PSN_RegisterNo = Convert.ToString(reader["PSN_RegisterNo"]);
                    if (reader["PSN_RegisterCertificateNo"] != DBNull.Value) _ApplyMDL.PSN_RegisterCertificateNo = Convert.ToString(reader["PSN_RegisterCertificateNo"]);
                    if (reader["PSN_RegisteProfession"] != DBNull.Value) _ApplyMDL.PSN_RegisteProfession = Convert.ToString(reader["PSN_RegisteProfession"]);
                    if (reader["ApplyTime"] != DBNull.Value) _ApplyMDL.ApplyTime = Convert.ToDateTime(reader["ApplyTime"]);
                    if (reader["ApplyCode"] != DBNull.Value) _ApplyMDL.ApplyCode = Convert.ToString(reader["ApplyCode"]);
                    if (reader["ApplyStatus"] != DBNull.Value) _ApplyMDL.ApplyStatus = Convert.ToString(reader["ApplyStatus"]);
                    if (reader["GetDateTime"] != DBNull.Value) _ApplyMDL.GetDateTime = Convert.ToDateTime(reader["GetDateTime"]);
                    if (reader["GetResult"] != DBNull.Value) _ApplyMDL.GetResult = Convert.ToString(reader["GetResult"]);
                    if (reader["GetRemark"] != DBNull.Value) _ApplyMDL.GetRemark = Convert.ToString(reader["GetRemark"]);
                    if (reader["GetMan"] != DBNull.Value) _ApplyMDL.GetMan = Convert.ToString(reader["GetMan"]);
                    if (reader["ExamineDatetime"] != DBNull.Value) _ApplyMDL.ExamineDatetime = Convert.ToDateTime(reader["ExamineDatetime"]);
                    if (reader["ExamineResult"] != DBNull.Value) _ApplyMDL.ExamineResult = Convert.ToString(reader["ExamineResult"]);
                    if (reader["ExamineRemark"] != DBNull.Value) _ApplyMDL.ExamineRemark = Convert.ToString(reader["ExamineRemark"]);
                    if (reader["ExamineMan"] != DBNull.Value) _ApplyMDL.ExamineMan = Convert.ToString(reader["ExamineMan"]);
                    if (reader["ReportDate"] != DBNull.Value) _ApplyMDL.ReportDate = Convert.ToDateTime(reader["ReportDate"]);
                    if (reader["ReportMan"] != DBNull.Value) _ApplyMDL.ReportMan = Convert.ToString(reader["ReportMan"]);
                    if (reader["ReportCode"] != DBNull.Value) _ApplyMDL.ReportCode = Convert.ToString(reader["ReportCode"]);
                    if (reader["AcceptDate"] != DBNull.Value) _ApplyMDL.AcceptDate = Convert.ToDateTime(reader["AcceptDate"]);
                    if (reader["AcceptMan"] != DBNull.Value) _ApplyMDL.AcceptMan = Convert.ToString(reader["AcceptMan"]);
                    if (reader["CheckDate"] != DBNull.Value) _ApplyMDL.CheckDate = Convert.ToDateTime(reader["CheckDate"]);
                    if (reader["CheckResult"] != DBNull.Value) _ApplyMDL.CheckResult = Convert.ToString(reader["CheckResult"]);
                    if (reader["CheckRemark"] != DBNull.Value) _ApplyMDL.CheckRemark = Convert.ToString(reader["CheckRemark"]);
                    if (reader["CheckMan"] != DBNull.Value) _ApplyMDL.CheckMan = Convert.ToString(reader["CheckMan"]);
                    if (reader["ConfirmDate"] != DBNull.Value) _ApplyMDL.ConfirmDate = Convert.ToDateTime(reader["ConfirmDate"]);
                    if (reader["ConfirmResult"] != DBNull.Value) _ApplyMDL.ConfirmResult = Convert.ToString(reader["ConfirmResult"]);
                    if (reader["ConfirmRemark"] != DBNull.Value) _ApplyMDL.ConfirmRemark = Convert.ToString(reader["ConfirmRemark"]); 
                    if (reader["ConfirmMan"] != DBNull.Value) _ApplyMDL.ConfirmMan = Convert.ToString(reader["ConfirmMan"]);
                    if (reader["PublicDate"] != DBNull.Value) _ApplyMDL.PublicDate = Convert.ToDateTime(reader["PublicDate"]);
                    if (reader["PublicMan"] != DBNull.Value) _ApplyMDL.PublicMan = Convert.ToString(reader["PublicMan"]);
                    if (reader["PublicCode"] != DBNull.Value) _ApplyMDL.PublicCode = Convert.ToString(reader["PublicCode"]);
                    if (reader["NoticeDate"] != DBNull.Value) _ApplyMDL.NoticeDate = Convert.ToDateTime(reader["NoticeDate"]);
                    if (reader["NoticeMan"] != DBNull.Value) _ApplyMDL.NoticeMan = Convert.ToString(reader["NoticeMan"]);
                    if (reader["NoticeCode"] != DBNull.Value) _ApplyMDL.NoticeCode = Convert.ToString(reader["NoticeCode"]);
                    if (reader["CJR"] != DBNull.Value) _ApplyMDL.CJR = Convert.ToString(reader["CJR"]);
                    if (reader["CJSJ"] != DBNull.Value) _ApplyMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
                    if (reader["XGR"] != DBNull.Value) _ApplyMDL.XGR = Convert.ToString(reader["XGR"]);
                    if (reader["XGSJ"] != DBNull.Value) _ApplyMDL.XGSJ = Convert.ToDateTime(reader["XGSJ"]);
                    if (reader["Valid"] != DBNull.Value) _ApplyMDL.Valid = Convert.ToInt32(reader["Valid"]);
                    if (reader["Memo"] != DBNull.Value) _ApplyMDL.Memo = Convert.ToString(reader["Memo"]);
                    if (reader["CodeDate"] != DBNull.Value) _ApplyMDL.CodeDate = Convert.ToDateTime(reader["CodeDate"]);
                    if (reader["CodeMan"] != DBNull.Value) _ApplyMDL.CodeMan = Convert.ToString(reader["CodeMan"]);
                    if (reader["SheBaoCheck"] != DBNull.Value) _ApplyMDL.SheBaoCheck = Convert.ToInt32(reader["SheBaoCheck"]);
                    if (reader["PSN_Level"] != DBNull.Value) _ApplyMDL.PSN_Level = Convert.ToString(reader["PSN_Level"]);
                    if (reader["OtherDeptCheckDate"] != DBNull.Value) _ApplyMDL.OtherDeptCheckDate = Convert.ToDateTime(reader["OtherDeptCheckDate"]);
                    if (reader["OtherDeptCheckResult"] != DBNull.Value) _ApplyMDL.OtherDeptCheckResult = Convert.ToString(reader["OtherDeptCheckResult"]);
                    if (reader["OtherDeptCheckRemark"] != DBNull.Value) _ApplyMDL.OtherDeptCheckRemark = Convert.ToString(reader["OtherDeptCheckRemark"]);
                    if (reader["OtherDeptCheckMan"] != DBNull.Value) _ApplyMDL.OtherDeptCheckMan = Convert.ToString(reader["OtherDeptCheckMan"]);
                    if (reader["OldUnitCheckTime"] != DBNull.Value) _ApplyMDL.OldUnitCheckTime = Convert.ToDateTime(reader["OldUnitCheckTime"]);
                    if (reader["OldUnitCheckResult"] != DBNull.Value) _ApplyMDL.OldUnitCheckResult = Convert.ToString(reader["OldUnitCheckResult"]);
                    if (reader["OldUnitCheckRemark"] != DBNull.Value) _ApplyMDL.OldUnitCheckRemark = Convert.ToString(reader["OldUnitCheckRemark"]);

                    if (reader["OldEnt_QYZJJGDM"] != DBNull.Value) _ApplyMDL.OldEnt_QYZJJGDM = Convert.ToString(reader["OldEnt_QYZJJGDM"]);
                    if (reader["newUnitCheckTime"] != DBNull.Value) _ApplyMDL.newUnitCheckTime = Convert.ToDateTime(reader["newUnitCheckTime"]);
                    if (reader["newUnitCheckResult"] != DBNull.Value) _ApplyMDL.newUnitCheckResult = Convert.ToString(reader["newUnitCheckResult"]);
                    if (reader["newUnitCheckRemark"] != DBNull.Value) _ApplyMDL.newUnitCheckRemark = Convert.ToString(reader["newUnitCheckRemark"]);

                    if (reader["CheckZSSD"] != DBNull.Value) _ApplyMDL.CheckZSSD = Convert.ToInt32(reader["CheckZSSD"]);
                    if (reader["CheckXSL"] != DBNull.Value) _ApplyMDL.CheckXSL = Convert.ToInt32(reader["CheckXSL"]);
                    if (reader["CheckCFZC"] != DBNull.Value) _ApplyMDL.CheckCFZC = Convert.ToInt32(reader["CheckCFZC"]);
                    if (reader["ENT_ContractType"] != DBNull.Value) _ApplyMDL.ENT_ContractType = Convert.ToInt32(reader["ENT_ContractType"]);

                    if (reader["LastBackResult"] != DBNull.Value) _ApplyMDL.LastBackResult = Convert.ToString(reader["LastBackResult"]);

                }
                reader.Close();
                db.Close();
                return _ApplyMDL;
            }
        }

        /// <summary>
        /// 根据组织机构代码获得这个公司的申请集合
        /// </summary>
        /// <param name="ENT_OrganizationsCode">组织机构代码</param>
        public static List<ApplyMDL> GetApplyList(string ENT_OrganizationsCode)
        {
            string sql = @"
			SELECT ApplyID,ApplyType,ApplyTypeSub,ENT_ServerID,ENT_Name,ENT_OrganizationsCode,ENT_City,ENT_ContractStartTime,ENT_ContractENDTime,PSN_ServerID,PSN_Name,PSN_Sex,PSN_CertificateType,PSN_CertificateNO,PSN_RegisterNo,PSN_RegisterCertificateNo,PSN_RegisteProfession,ApplyTime,ApplyCode,ApplyStatus,GetDateTime,GetResult,GetRemark,GetMan,ExamineDatetime,ExamineResult,ExamineRemark,ExamineMan,ReportDate,ReportMan,ReportCode,AcceptDate,AcceptMan,CheckDate,CheckResult,CheckRemark,CheckMan,ConfirmDate,ConfirmResult,ConfirmRemark,ConfirmMan,PublicDate,PublicMan,PublicCode,NoticeDate,NoticeMan,NoticeCode,CJR,CJSJ,XGR,XGSJ,[VALID],Memo,CodeDate,CodeMan,SheBaoCheck,PSN_Level,OtherDeptCheckDate,OtherDeptCheckResult,OtherDeptCheckRemark,OtherDeptCheckMan,OldUnitCheckTime,OldUnitCheckResult,OldUnitCheckRemark,OldEnt_QYZJJGDM,newUnitCheckTime,newUnitCheckResult,newUnitCheckRemark,ENT_ContractType,LastBackResult
			FROM dbo.Apply
			WHERE (ENT_OrganizationsCode = @ENT_OrganizationsCode or ENT_OrganizationsCode like '________" + ENT_OrganizationsCode + "_') AND VALID=1 ";
            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ENT_OrganizationsCode", DbType.String, ENT_OrganizationsCode));
            using (DataTable dt = db.GetFillData(sql, p.ToArray()))
            {
                List<ApplyMDL> __ListApplyMDL = new List<ApplyMDL>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ApplyMDL _ApplyMDL = new ApplyMDL();
                        if (dt.Rows[i]["ApplyID"] != DBNull.Value) _ApplyMDL.ApplyID = Convert.ToString(dt.Rows[i]["ApplyID"]);
                        if (dt.Rows[i]["ApplyType"] != DBNull.Value) _ApplyMDL.ApplyType = Convert.ToString(dt.Rows[i]["ApplyType"]);
                        if (dt.Rows[i]["ApplyTypeSub"] != DBNull.Value) _ApplyMDL.ApplyTypeSub = Convert.ToString(dt.Rows[i]["ApplyTypeSub"]);
                        if (dt.Rows[i]["ENT_ServerID"] != DBNull.Value) _ApplyMDL.ENT_ServerID = Convert.ToString(dt.Rows[i]["ENT_ServerID"]);
                        if (dt.Rows[i]["ENT_Name"] != DBNull.Value) _ApplyMDL.ENT_Name = Convert.ToString(dt.Rows[i]["ENT_Name"]);
                        if (dt.Rows[i]["ENT_OrganizationsCode"] != DBNull.Value) _ApplyMDL.ENT_OrganizationsCode = Convert.ToString(dt.Rows[i]["ENT_OrganizationsCode"]);
                        if (dt.Rows[i]["ENT_City"] != DBNull.Value) _ApplyMDL.ENT_City = Convert.ToString(dt.Rows[i]["ENT_City"]);
                        if (dt.Rows[i]["ENT_ContractStartTime"] != DBNull.Value) _ApplyMDL.ENT_ContractStartTime = Convert.ToDateTime(dt.Rows[i]["ENT_ContractStartTime"]);
                        if (dt.Rows[i]["ENT_ContractENDTime"] != DBNull.Value) _ApplyMDL.ENT_ContractENDTime = Convert.ToDateTime(dt.Rows[i]["ENT_ContractENDTime"]);
                        if (dt.Rows[i]["PSN_ServerID"] != DBNull.Value) _ApplyMDL.PSN_ServerID = Convert.ToString(dt.Rows[i]["PSN_ServerID"]);
                        if (dt.Rows[i]["PSN_Name"] != DBNull.Value) _ApplyMDL.PSN_Name = Convert.ToString(dt.Rows[i]["PSN_Name"]);
                        if (dt.Rows[i]["PSN_Sex"] != DBNull.Value) _ApplyMDL.PSN_Sex = Convert.ToString(dt.Rows[i]["PSN_Sex"]);
                        if (dt.Rows[i]["PSN_CertificateType"] != DBNull.Value) _ApplyMDL.PSN_CertificateType = Convert.ToString(dt.Rows[i]["PSN_CertificateType"]);
                        if (dt.Rows[i]["PSN_CertificateNO"] != DBNull.Value) _ApplyMDL.PSN_CertificateNO = Convert.ToString(dt.Rows[i]["PSN_CertificateNO"]);
                        if (dt.Rows[i]["PSN_RegisterNo"] != DBNull.Value) _ApplyMDL.PSN_RegisterNo = Convert.ToString(dt.Rows[i]["PSN_RegisterNo"]);
                        if (dt.Rows[i]["PSN_RegisterCertificateNo"] != DBNull.Value) _ApplyMDL.PSN_RegisterCertificateNo = Convert.ToString(dt.Rows[i]["PSN_RegisterCertificateNo"]);
                        if (dt.Rows[i]["PSN_RegisteProfession"] != DBNull.Value) _ApplyMDL.PSN_RegisteProfession = Convert.ToString(dt.Rows[i]["PSN_RegisteProfession"]);
                        if (dt.Rows[i]["ApplyTime"] != DBNull.Value) _ApplyMDL.ApplyTime = Convert.ToDateTime(dt.Rows[i]["ApplyTime"]);
                        if (dt.Rows[i]["ApplyCode"] != DBNull.Value) _ApplyMDL.ApplyCode = Convert.ToString(dt.Rows[i]["ApplyCode"]);
                        if (dt.Rows[i]["ApplyStatus"] != DBNull.Value) _ApplyMDL.ApplyStatus = Convert.ToString(dt.Rows[i]["ApplyStatus"]);
                        if (dt.Rows[i]["GetDateTime"] != DBNull.Value) _ApplyMDL.GetDateTime = Convert.ToDateTime(dt.Rows[i]["GetDateTime"]);
                        if (dt.Rows[i]["GetResult"] != DBNull.Value) _ApplyMDL.GetResult = Convert.ToString(dt.Rows[i]["GetResult"]);
                        if (dt.Rows[i]["GetRemark"] != DBNull.Value) _ApplyMDL.GetRemark = Convert.ToString(dt.Rows[i]["GetRemark"]);
                        if (dt.Rows[i]["GetMan"] != DBNull.Value) _ApplyMDL.GetMan = Convert.ToString(dt.Rows[i]["GetMan"]);
                        if (dt.Rows[i]["ExamineDatetime"] != DBNull.Value) _ApplyMDL.ExamineDatetime = Convert.ToDateTime(dt.Rows[i]["ExamineDatetime"]);
                        if (dt.Rows[i]["ExamineResult"] != DBNull.Value) _ApplyMDL.ExamineResult = Convert.ToString(dt.Rows[i]["ExamineResult"]);
                        if (dt.Rows[i]["ExamineRemark"] != DBNull.Value) _ApplyMDL.ExamineRemark = Convert.ToString(dt.Rows[i]["ExamineRemark"]);
                        if (dt.Rows[i]["ExamineMan"] != DBNull.Value) _ApplyMDL.ExamineMan = Convert.ToString(dt.Rows[i]["ExamineMan"]);
                        if (dt.Rows[i]["ReportDate"] != DBNull.Value) _ApplyMDL.ReportDate = Convert.ToDateTime(dt.Rows[i]["ReportDate"]);
                        if (dt.Rows[i]["ReportMan"] != DBNull.Value) _ApplyMDL.ReportMan = Convert.ToString(dt.Rows[i]["ReportMan"]);
                        if (dt.Rows[i]["ReportCode"] != DBNull.Value) _ApplyMDL.ReportCode = Convert.ToString(dt.Rows[i]["ReportCode"]);
                        if (dt.Rows[i]["AcceptDate"] != DBNull.Value) _ApplyMDL.AcceptDate = Convert.ToDateTime(dt.Rows[i]["AcceptDate"]);
                        if (dt.Rows[i]["AcceptMan"] != DBNull.Value) _ApplyMDL.AcceptMan = Convert.ToString(dt.Rows[i]["AcceptMan"]);
                        if (dt.Rows[i]["CheckDate"] != DBNull.Value) _ApplyMDL.CheckDate = Convert.ToDateTime(dt.Rows[i]["CheckDate"]);
                        if (dt.Rows[i]["CheckResult"] != DBNull.Value) _ApplyMDL.CheckResult = Convert.ToString(dt.Rows[i]["CheckResult"]);
                        if (dt.Rows[i]["CheckRemark"] != DBNull.Value) _ApplyMDL.CheckRemark = Convert.ToString(dt.Rows[i]["CheckRemark"]);
                        if (dt.Rows[i]["CheckMan"] != DBNull.Value) _ApplyMDL.CheckMan = Convert.ToString(dt.Rows[i]["CheckMan"]);
                        if (dt.Rows[i]["ConfirmDate"] != DBNull.Value) _ApplyMDL.ConfirmDate = Convert.ToDateTime(dt.Rows[i]["ConfirmDate"]);
                        if (dt.Rows[i]["ConfirmResult"] != DBNull.Value) _ApplyMDL.ConfirmResult = Convert.ToString(dt.Rows[i]["ConfirmResult"]);
                        if (dt.Rows[i]["ConfirmRemark"] != DBNull.Value) _ApplyMDL.ConfirmRemark = Convert.ToString(dt.Rows[i]["ConfirmRemark"]); 
                        if (dt.Rows[i]["ConfirmMan"] != DBNull.Value) _ApplyMDL.ConfirmMan = Convert.ToString(dt.Rows[i]["ConfirmMan"]);
                        if (dt.Rows[i]["PublicDate"] != DBNull.Value) _ApplyMDL.PublicDate = Convert.ToDateTime(dt.Rows[i]["PublicDate"]);
                        if (dt.Rows[i]["PublicMan"] != DBNull.Value) _ApplyMDL.PublicMan = Convert.ToString(dt.Rows[i]["PublicMan"]);
                        if (dt.Rows[i]["PublicCode"] != DBNull.Value) _ApplyMDL.PublicCode = Convert.ToString(dt.Rows[i]["PublicCode"]);
                        if (dt.Rows[i]["NoticeDate"] != DBNull.Value) _ApplyMDL.NoticeDate = Convert.ToDateTime(dt.Rows[i]["NoticeDate"]);
                        if (dt.Rows[i]["NoticeMan"] != DBNull.Value) _ApplyMDL.NoticeMan = Convert.ToString(dt.Rows[i]["NoticeMan"]);
                        if (dt.Rows[i]["NoticeCode"] != DBNull.Value) _ApplyMDL.NoticeCode = Convert.ToString(dt.Rows[i]["NoticeCode"]);
                        if (dt.Rows[i]["CJR"] != DBNull.Value) _ApplyMDL.CJR = Convert.ToString(dt.Rows[i]["CJR"]);
                        if (dt.Rows[i]["CJSJ"] != DBNull.Value) _ApplyMDL.CJSJ = Convert.ToDateTime(dt.Rows[i]["CJSJ"]);
                        if (dt.Rows[i]["XGR"] != DBNull.Value) _ApplyMDL.XGR = Convert.ToString(dt.Rows[i]["XGR"]);
                        if (dt.Rows[i]["XGSJ"] != DBNull.Value) _ApplyMDL.XGSJ = Convert.ToDateTime(dt.Rows[i]["XGSJ"]);
                        if (dt.Rows[i]["Valid"] != DBNull.Value) _ApplyMDL.Valid = Convert.ToInt32(dt.Rows[i]["Valid"]);
                        if (dt.Rows[i]["Memo"] != DBNull.Value) _ApplyMDL.Memo = Convert.ToString(dt.Rows[i]["Memo"]);
                        if (dt.Rows[i]["CodeDate"] != DBNull.Value) _ApplyMDL.CodeDate = Convert.ToDateTime(dt.Rows[i]["CodeDate"]);
                        if (dt.Rows[i]["CodeMan"] != DBNull.Value) _ApplyMDL.CodeMan = Convert.ToString(dt.Rows[i]["CodeMan"]);
                        if (dt.Rows[i]["SheBaoCheck"] != DBNull.Value) _ApplyMDL.SheBaoCheck = Convert.ToInt32(dt.Rows[i]["SheBaoCheck"]);
                        if (dt.Rows[i]["PSN_Level"] != DBNull.Value) _ApplyMDL.PSN_Level = Convert.ToString(dt.Rows[i]["PSN_Level"]);
                        if (dt.Rows[i]["OtherDeptCheckDate"] != DBNull.Value) _ApplyMDL.OtherDeptCheckDate = Convert.ToDateTime(dt.Rows[i]["OtherDeptCheckDate"]);
                        if (dt.Rows[i]["OtherDeptCheckResult"] != DBNull.Value) _ApplyMDL.OtherDeptCheckResult = Convert.ToString(dt.Rows[i]["OtherDeptCheckResult"]);
                        if (dt.Rows[i]["OtherDeptCheckRemark"] != DBNull.Value) _ApplyMDL.OtherDeptCheckRemark = Convert.ToString(dt.Rows[i]["OtherDeptCheckRemark"]);
                        if (dt.Rows[i]["OtherDeptCheckMan"] != DBNull.Value) _ApplyMDL.OtherDeptCheckMan = Convert.ToString(dt.Rows[i]["OtherDeptCheckMan"]);
                        if (dt.Rows[i]["OldUnitCheckTime"] != DBNull.Value) _ApplyMDL.OldUnitCheckTime = Convert.ToDateTime(dt.Rows[i]["OldUnitCheckTime"]);
                        if (dt.Rows[i]["OldUnitCheckResult"] != DBNull.Value) _ApplyMDL.OldUnitCheckResult = Convert.ToString(dt.Rows[i]["OldUnitCheckResult"]);
                        if (dt.Rows[i]["OldUnitCheckRemark"] != DBNull.Value) _ApplyMDL.OldUnitCheckRemark = Convert.ToString(dt.Rows[i]["OldUnitCheckRemark"]);
                        if (dt.Rows[i]["OldEnt_QYZJJGDM"] != DBNull.Value) _ApplyMDL.OldEnt_QYZJJGDM = Convert.ToString(dt.Rows[i]["OldEnt_QYZJJGDM"]);
                        if (dt.Rows[i]["newUnitCheckTime"] != DBNull.Value) _ApplyMDL.newUnitCheckTime = Convert.ToDateTime(dt.Rows[i]["newUnitCheckTime"]);
                        if (dt.Rows[i]["newUnitCheckResult"] != DBNull.Value) _ApplyMDL.newUnitCheckResult = Convert.ToString(dt.Rows[i]["newUnitCheckResult"]);
                        if (dt.Rows[i]["newUnitCheckRemark"] != DBNull.Value) _ApplyMDL.newUnitCheckRemark = Convert.ToString(dt.Rows[i]["newUnitCheckRemark"]);
                        if (dt.Rows[i]["ENT_ContractType"] != DBNull.Value) _ApplyMDL.ENT_ContractType = Convert.ToInt32(dt.Rows[i]["ENT_ContractType"]);

                        if (dt.Rows[i]["LastBackResult"] != DBNull.Value) _ApplyMDL.LastBackResult = Convert.ToString(dt.Rows[i]["LastBackResult"]);
                        __ListApplyMDL.Add(_ApplyMDL);
                    }
                }

                db.Close();
                return __ListApplyMDL;
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
            //return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.Apply", "*", filterWhereString, orderBy == "" ? " ENT_City,ENT_Name" : orderBy);
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.Apply", "*", filterWhereString, orderBy == "" ? " ENT_City,ENT_Name" : orderBy);
        }
        /// <summary>
        /// 遗失补办
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <param name="filterWhereString"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static DataTable GetList3(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.Apply inner join ApplyReplace on Apply.ApplyID =ApplyReplace.ApplyID", "Apply.*,ApplyReplace.RegisterCertificateNo", filterWhereString, orderBy == "" ? " ENT_City,ENT_Name" : orderBy);
        }

        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.Apply", filterWhereString);
        }

        #region 自定义方法

        /// <summary>
        /// 检查申请单是否上传了指定的附件类型
        /// </summary>
        /// <param name="ApplyID">申请单ID</param>
        /// <param name="DataType">文件类型名称</param>
        /// <returns>存在返回true，否则返回false</returns>
        public static bool CheckIfUploadFileType(string ApplyID, string DataType)
        {
            int count = CommonDAL.SelectRowCount("ApplyFile A INNER JOIN FileInfo B ON A.FILEID=B.FILEID ", string.Format(" and A.APPLYID='{0}' and B.[DataType]='{1}'", ApplyID, DataType));
            if (count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 获取申述申请实体集合
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        public static DataTable GetListWithShenShu(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.Apply"
                , @"*,case when exists(
			                        SELECT 1
			                        FROM [dbo].[ApplyFile] a
			                        inner join [dbo].[FileInfo] f on a.FileID = f.FileID
			                        where a.[ApplyID] = [Apply].[ApplyID] and f.[DataType]='申述扫描件'
	                          ) 
	                          then 1
	                          else 0
	                          end ShenShu"
                , filterWhereString, orderBy == "" ? " ApplyID" : orderBy);
        }
        /// <summary>
        /// 统计查询申述申请记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountWithShenShu(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.Apply", filterWhereString);
        }

        /// <summary>
        /// 获取申请单上报的附件信息
        /// </summary>
        /// <param name="ApplyID">申请单ID</param>
        /// <returns>附件信息集合</returns>
        public static DataTable GetApplyFile(string ApplyID)
        {
            DataTable dt = CommonDAL.GetDataTable(string.Format(@"
                SELECT b.FileID,b.DataType,a.ApplyID FROM ApplyFile A INNER JOIN FileInfo B 
                ON A.FILEID=B.FILEID 
                WHERE A.APPLYID='{0}'", ApplyID));
            return dt;
        }

        //public static int UpdateApplyChange(string applystatus, DateTime sqsj, string xgr, DateTime xgsj, string id)
        //{
        //    string sql = string.Format("UPDATE APPLY SET ApplyStatus='{0}',ApplyTime='{1}',XGR='{2}',XGSJ='{3}' WHERE APPLYID='{4}'", applystatus, sqsj, xgr, xgsj, id);
        //    return (new DBHelper()).GetExcuteNonQuery(sql);
        //}

        /// <summary>
        /// 原单位审核执业企业变更调出申请
        /// </summary>
        /// <param name="ifPass">是否同意调出：同意true，不同意false</param>
        /// <param name="xgr">审核人</param>
        /// <param name="xgsj">审核时间</param>
        /// <param name="id">申请ID</param>
        /// <returns></returns>
        public static int ApplyChangeOldUnitCheck(bool ifPass, string xgr, DateTime xgsj, string id)
        {
            string sql = "";
            if (ifPass == true)//同意
            {
                sql = string.Format("UPDATE APPLY SET OldUnitCheckTime='{1}',OldUnitCheckResult='通过',OldUnitCheckRemark='同意',XGR='{0}',XGSJ='{1}' WHERE APPLYID='{2}'", xgr, xgsj, id);
            }
            else//不同意，设置公告时间NoticeDate表示已办结，不要设置公告人NoticeMan，以便审核历史记录不展现公告记录
            {
                sql = string.Format("UPDATE APPLY SET OldUnitCheckTime='{1}',OldUnitCheckResult='不通过',OldUnitCheckRemark='不同意',XGR='{0}',XGSJ='{1}',[NoticeDate]='{1}',ApplyStatus='已驳回' WHERE APPLYID='{2}'", xgr, xgsj, id);
            }
            return (new DBHelper()).GetExcuteNonQuery(sql);
        }

        /// <summary>
        ///获取执业企业变更申请的数据
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        /// <summary>
        public static DataTable GetApplyChangeList(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows
                , @"(
                        SELECT A.ApplyID,A.PSN_ServerID,A.PSN_Name,A.PSN_Sex,A.PSN_CertificateType,A.PSN_CertificateNO,A.PSN_RegisterNO,
                        A.[ENT_ServerID],A.ENT_Name,A.ENT_OrganizationsCode,A.ENT_City,A.ApplyType,A.ApplyTypeSub,A.ApplyStatus,
                        B.OldENT_Name,A.OldUnitCheckResult,A.OldUnitCheckTime,A.OldUnitCheckRemark,A.cjsj FROM APPLY A INNER JOIN APPLYCHANGE B ON A.APPLYID=B.APPLYID  
                    ) t"
                , "*"
                , filterWhereString, orderBy == "" ? " cjsj desc,PSN_RegisterNO" : orderBy);
        }

        /// <summary>
        /// 获取执业企业变更申请的数据
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectApplyChangeCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount(@"( 
                       SELECT A.ApplyID,A.PSN_Name,A.PSN_Sex,A.PSN_CertificateType,A.PSN_CertificateNO,A.PSN_RegisterNO,
                        A.[ENT_ServerID],A.ENT_Name,A.ENT_OrganizationsCode,A.ENT_City,A.ApplyType,A.ApplyTypeSub,A.ApplyStatus,
                        B.OldENT_Name FROM APPLY A INNER JOIN APPLYCHANGE B ON A.APPLYID=B.APPLYID  
                                                ) t", filterWhereString);
        }

        /// <summary>
        ///获取执业企业变更申请调入的数据
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        /// <summary>
        public static DataTable GetApplyChangePersonList(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows
                , @"(
						SELECT A.ApplyID,A.PSN_Name,A.PSN_Sex,A.PSN_CertificateType,A.PSN_CertificateNO,A.PSN_RegisterNO,
                        B.OldENT_Name,B.OldLinkMan,B.OldENT_Telephone,B.OldENT_Correspondence,A.ApplyTypeSub,A.ApplyStatus
                        FROM APPLY A INNER JOIN APPLYCHANGE B ON A.APPLYID=B.APPLYID AND A.NoticeDate IS NULL AND A.ApplyTypeSub='执业企业变更' 
                    ) t"
                , "*"
                , filterWhereString, orderBy == "" ? " OldENT_Name" : orderBy);
        }

        /// <summary>
        /// 获取执业企业变更申请调入的数据
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectApplyChangePersonCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount(@"( 
                       	SELECT A.ApplyID,A.PSN_Name,A.PSN_Sex,A.PSN_CertificateType,A.PSN_CertificateNO,A.PSN_RegisterNO,
                        B.OldENT_Name,B.OldLinkMan,B.OldENT_Telephone,B.OldENT_Correspondence,A.ApplyTypeSub,A.ApplyStatus
                        FROM APPLY A INNER JOIN APPLYCHANGE B ON A.APPLYID=B.APPLYID AND A.NoticeDate IS NULL AND A.ApplyTypeSub='执业企业变更' 
                                                ) t", filterWhereString);
        }

        /// <summary>
        /// 个人、企业获取申请单审批记录集合。
        /// 初始、重新、延续、增项注册在公示发出后，申请人、企业用户端才可以看到市级审查、市级决定的意见。
        /// 注销注册在事项决定后，申请人、企业用户端才可以看到市级审查、市级决定的意
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
        FROM [dbo].[Apply]  where [ApplyID]='{0}'
 union all
select  '原单位确认' as 'Action',[OldUnitCheckTime] as ActionData,[OldUnitCheckResult] as ActionResult,[OldUnitCheckRemark] as ActionRemark,OldENT_Name as ActionMan FROM [dbo].[Apply] left join ApplyChange on  Apply.ApplyID=ApplyChange.ApplyID where Apply.[ApplyID]='{0}' and [OldUnitCheckTime] is not null and [ApplyTypeSub] ='执业企业变更'
 union all
 select   '单位确认' as 'Action',[OldUnitCheckTime] as ActionData,[OldUnitCheckResult] as ActionResult,[OldUnitCheckRemark] as ActionRemark,[ENT_Name] as ActionMan FROM [dbo].[Apply]  where [ApplyID]='{0}' and [OldUnitCheckTime] is not null and ([ApplyTypeSub] is null or [ApplyTypeSub] <>'执业企业变更')
 union all
select   '新单位确认' as 'Action',[newUnitCheckTime] as ActionData,[newUnitCheckResult] as ActionResult,[newUnitCheckRemark] as ActionRemark,ENT_Name as ActionMan FROM [dbo].[Apply] where Apply.[ApplyID]='{0}' and [newUnitCheckTime] is not null and [ApplyTypeSub] ='执业企业变更'
 union all
 select   '区级受理' as 'Action',[GetDateTime] as ActionData,[GetResult] as ActionResult,[GetRemark] as ActionRemark,[GetMan] as ActionMan FROM [dbo].[Apply] where [ApplyID]='{0}' and [GetDateTime] is not null
 union all
  select  '区级审查',[ExamineDatetime],[ExamineResult],[ExamineRemark],[ExamineMan] FROM [dbo].[Apply] where [ApplyID]='{0}' and [ExamineDatetime] is not null
 union all
  select  '区级上报',[ReportDate],case when [ApplyTypeSub] ='企业信息变更' or [ApplyTypeSub] ='执业企业变更' then '已办结' else '已上报' end,case when [ApplyTypeSub] ='企业信息变更' or [ApplyTypeSub] ='执业企业变更' then '请下载电子证书' else '提交市级审查' end,[ReportMan] FROM [dbo].[Apply] where [ApplyID]='{0}' and [ReportDate] is not null
 union all
  select  '服务大厅收件',[AcceptDate],'','',[AcceptMan] FROM [dbo].[Apply] where [ApplyID]='{0}' and [AcceptDate] is not null
 union all
  select  '市级审查',[CheckDate],[CheckResult],[CheckRemark],[CheckMan] FROM [dbo].[Apply] where [ApplyID]='{0}' and [CheckDate] is not null and (([NoticeDate] is not null and ApplyType in ('初始注册','重新注册','延期注册')) or ([ConfirmDate] is not null and ApplyType ='注销') or (ApplyType ='增项注册') or (ApplyType ='变更注册') or (ApplyType ='遗失补办'))
 union all
  select  '市级决定',[ConfirmDate],[ConfirmResult],[ConfirmRemark],[ConfirmMan] FROM [dbo].[Apply] where [ApplyID]='{0}' and [ConfirmDate] is not null and (([NoticeDate] is not null and ApplyType in ('初始注册','重新注册','增项注册','延期注册')) or (ApplyType in ('注销','变更注册','遗失补办')))
 union all
  select  '公示',[PublicDate],'','批次号：'+[PublicCode],[PublicMan] FROM [dbo].[Apply] where [ApplyID]='{0}' and [PublicDate] is not null
union all
  select  '公告',[NoticeDate],'','批次号：'+[NoticeCode],[NoticeMan] FROM [dbo].[Apply] where [ApplyID]='{0}' and [NoticeDate] is not null and [NoticeMan] is not null 
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
        FROM [dbo].[Apply]  where [ApplyID]='{0}'
 union all
 select   '原单位确认' as 'Action',[OldUnitCheckTime] as ActionData,[OldUnitCheckResult] as ActionResult,[OldUnitCheckRemark] as ActionRemark,OldENT_Name as ActionMan FROM [dbo].[Apply] left join ApplyChange on  Apply.ApplyID=ApplyChange.ApplyID where Apply.[ApplyID]='{0}' and [OldUnitCheckTime] is not null and [ApplyTypeSub] ='执业企业变更'
 union all
 select   '单位确认' as 'Action',[OldUnitCheckTime] as ActionData,[OldUnitCheckResult] as ActionResult,[OldUnitCheckRemark] as ActionRemark,[ENT_Name] as ActionMan FROM [dbo].[Apply]  where [ApplyID]='{0}' and [OldUnitCheckTime] is not null and ([ApplyTypeSub] is null or [ApplyTypeSub] <>'执业企业变更')
  union all
select   '新单位确认' as 'Action',[newUnitCheckTime] as ActionData,[newUnitCheckResult] as ActionResult,[newUnitCheckRemark] as ActionRemark,ENT_Name as ActionMan FROM [dbo].[Apply] where Apply.[ApplyID]='{0}' and [newUnitCheckTime] is not null and [ApplyTypeSub] ='执业企业变更'
 union all
 select   '区级受理' as 'Action',[GetDateTime] as ActionData,[GetResult] as ActionResult,[GetRemark] as ActionRemark,[GetMan] as ActionMan FROM [dbo].[Apply] where [ApplyID]='{0}' and [GetDateTime] is not null
 union all
  select  '区级审查',[ExamineDatetime],[ExamineResult],[ExamineRemark],[ExamineMan] FROM [dbo].[Apply] where [ApplyID]='{0}' and [ExamineDatetime] is not null
 union all
  select  '区级上报',[ReportDate],case when [ApplyTypeSub] ='企业信息变更' or [ApplyTypeSub] ='执业企业变更' then '已办结' else '已上报' end,case when [ApplyTypeSub] ='企业信息变更' or [ApplyTypeSub] ='执业企业变更' then '请下载电子证书' else '提交市级审查' end,[ReportMan] FROM [dbo].[Apply] where [ApplyID]='{0}' and [ReportDate] is not null
 union all
  select  '服务大厅收件',[AcceptDate],'','',[AcceptMan] FROM [dbo].[Apply] where [ApplyID]='{0}' and [AcceptDate] is not null
 union all
  select  '市级审查',[CheckDate],[CheckResult],[CheckRemark],[CheckMan] FROM [dbo].[Apply] where [ApplyID]='{0}' and [CheckDate] is not null 
 union all
  select  '专业局会审',[OtherDeptCheckDate],[OtherDeptCheckResult],[OtherDeptCheckRemark],[OtherDeptCheckMan] FROM [dbo].[Apply] where [ApplyID]='{0}' and [OtherDeptCheckDate] is not null 
 union all
  select  '市级决定',[ConfirmDate],[ConfirmResult],[ConfirmRemark],[ConfirmMan] FROM [dbo].[Apply] where [ApplyID]='{0}' and [ConfirmDate] is not null
 union all
  select  '公示',[PublicDate],'','批次号：'+[PublicCode],[PublicMan] FROM [dbo].[Apply] where [ApplyID]='{0}' and [PublicDate] is not null
union all
  select  '公告',[NoticeDate],'','批次号：'+[NoticeCode],[NoticeMan] FROM [dbo].[Apply] where [ApplyID]='{0}' and [NoticeDate] is not null and [NoticeMan] is not null 
) t";
            return CommonDAL.GetDataTable(string.Format(sql, ApplyID));

        }

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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.View_JZS_TOW_Applying", "*", filterWhereString, orderBy == "" ? " xgsj desc,PSN_ServerID" : orderBy);
        }

        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountApplyView(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.View_JZS_TOW_Applying", filterWhereString);
        }

        /// <summary>
        /// 获取证书打印实体集合
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        /// <summary>
        public static DataTable GetListPrintView(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.View_JZS_TOW_Print", "*", filterWhereString, orderBy == "" ? "PSN_RegisterCertificateNo" : orderBy);
        }

        /// <summary>
        /// 统计证书打印查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountPrintView(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.View_JZS_TOW_Print", filterWhereString);
        }

        /// <summary>
        /// 获取下一个申请单号
        /// 申请表编号规则：共13位，年份（2016），月份（05），日（15），注册类别（初始10、变更31、32、33、延续20、重新60、注销51、52、增项40、遗失71、污损72）、自然序号（000）
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
                case "延期注册":
                    num = "20";
                    break;
                case "遗失补办":
                    num = "71";
                    break;
                case "污损补办":
                    num = "72";
                    break;
                case "增项注册":
                    num = "40";
                    break;
                case "重新注册":
                    num = "60";
                    break;
                case "注销":
                    num = "51";
                    break;
                default:
                    break;
            }
            string sql = @"select isnull(max([ApplyCode]),'')
                            from apply
                            group by case when ApplyTypeSub <>''  then ApplyTypeSub else  ApplyType end  
                            having  case when ApplyTypeSub <>''  then ApplyTypeSub else  ApplyType end  ='{0}'";
            //string ApplyCode = CommonDAL.GetDataTable(tran, string.Format(sql,type)).Rows[0][0].ToString();
            string ApplyCode = CommonDAL.GetDataTable(string.Format(sql, type)).Rows.Count > 0 ? CommonDAL.GetDataTable(string.Format(sql, type)).Rows[0][0].ToString() : "";

            if (ApplyCode == ""
                || ApplyCode.Substring(0, 8) != DateTime.Now.ToString("yyyyMMdd"))
            {
                return string.Format("{0}{1}001", DateTime.Now.ToString("yyyyMMdd"), num);
            }
            else
            {

                return string.Format("{0}{1}{2}", DateTime.Now.ToString("yyyyMMdd"), num, (Convert.ToInt32(ApplyCode.Substring(10, 3)) + 1).ToString().PadLeft(3, '0'));
            }


        }

        /// <summary>
        /// 上报区县批次号：2位区县编号 + 8位年月日 + 注册类型 +3 位自然序号（000）
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
                case "遗失补办":
                    num = "71";
                    break;
                case "增项注册":
                    num = "40";
                    break;
                case "重新注册":
                    num = "60";
                    break;
                case "注销":
                    num = "51";
                    break;
            }
            string sql = string.Format("select isnull(max(substring(ReportCode,3,15)),'') from apply group by applytype having applytype='{0}'", type);
            string ApplyCode = CommonDAL.GetDataTable(sql).Rows.Count > 0 ? CommonDAL.GetDataTable(sql).Rows[0][0].ToString() : "";
            if (ApplyCode == ""
              || ApplyCode.Substring(0, 8) != DateTime.Now.ToString("yyyyMMdd"))
            {
                return string.Format("{0}{1}001", ncity, DateTime.Now.ToString("yyyyMMdd") + num);
            }
            else
            {

                return string.Format("{0}{1}{2}", ncity, DateTime.Now.ToString("yyyyMMdd") + num, (Convert.ToInt32(ApplyCode.Substring(10, 3)) + 1).ToString().PadLeft(3, '0'));
            }
        }

        /// <summary>
        /// 获取公示或公告下一个申请单号
        /// </summary>
        /// <param name="tran">事物</param>
        /// <param name="lie">是公示还是公告：PublicCode，NoticeCode</param>
        /// <param name="type">类型：初始注册、增项注册、重新注册、延期注册</param>
        /// <returns></returns>
        public static string GetNextPublicCode(string lie, string type)
        {
            string num = null;
            switch (type)
            {
                case "初始注册":
                    num = "10";
                    break;
                case "增项注册":
                    num = "40";
                    break;
                case "重新注册":
                    num = "60";
                    break;
                case "延期注册":
                    num = "20";
                    break;
                default:
                    break;
            }
            //是公示还是公告
            string sg = null;
            switch (lie)
            {
                case "PublicCode":
                    sg = "S";
                    break;
                case "NoticeCode":
                    sg = "G";
                    break;
            }
            string sql = string.Format("select isnull(max(substring({0},2,13)),'') from apply group by applytype having applytype='{1}'", lie, type);
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
        /// 区县分别计算出已申报每个事项有多少个人
        /// </summary>
        /// <param name="city">过滤条件</param>
        /// <returns>统计结果</returns>
        public static DataTable GetApplyGroupBy(string city)
        {
            DBHelper db = new DBHelper();
            string sql = @"select ApplyTypeSub,count(ApplyID)as Num from
                       (select ApplyID, case when ApplyTypeSub <>''  then ApplyTypeSub else  ApplyType end as ApplyTypeSub from apply where 1=1 " + city + " and ApplyStatus='已申报')t group by ApplyTypeSub";
            return db.GetFillData(sql);
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
                                select case when ApplyType ='变更注册' then ApplyTypeSub 
                                                     else ApplyType  end as ApplyType
                                                            from  apply where 1=1 {0}
							                                )t group by ApplyType ";
            return db.GetFillData(string.Format(sql, where));
        }

        /// <summary>
        /// 按事项类型统计代办任务数量,变更注册的企业变更单算
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static DataTable GetApplyGroupByApplyTypeQybg(string where)
        {
            DBHelper db = new DBHelper();
            string sql = @"select ENT_OrganizationsCode from  apply where  APPLYTYPE='变更注册' AND ApplyTypeSub='企业信息变更' {0} group by ENT_OrganizationsCode";
            return db.GetFillData(string.Format(sql, where));
        }

//        /// <summary>
//        /// 按事项类型统计代办任务数量,变更注册的执业企业变更单算
//        /// </summary>
//        /// <param name="where"></param>
//        /// <param name="QYCode">原单位企业机构代码</param>
//        /// <returns></returns>
//        public static DataTable GetApplyGroupByApplyTypeZYQYbg(string where, string QYCode)
//        {
//            DBHelper db = new DBHelper();
//            string sql = @"SELECT ENT_OrganizationsCode FROM APPLY A INNER JOIN APPLYCHANGE B ON A.APPLYID=B.APPLYID 
//                            where  APPLYTYPE='变更注册' AND ApplyTypeSub='执业企业变更' and OldEnt_QYZJJGDM like '{0}%' and ApplyStatus='待确认' group by ENT_OrganizationsCode";
//            //string sql = @"select ENT_OrganizationsCode from  apply where  APPLYTYPE='变更注册' AND ApplyTypeSub='执业企业变更' and OldENT_Name like '{0}' {1} group by ENT_OrganizationsCode";
//            return db.GetFillData(string.Format(sql, QYCode));
//        }

        /// <summary>
        /// 企业分别计算出未申报每个事项有多少个人
        /// </summary>
        /// <param name="ENT_ServerID">企业ID</param>
        /// <param name="BeginTime">统计开始时间</param>
        /// <param name="endTime">统计截止时间</param>
        /// <returns></returns>
        public static DataTable GetApplyGroupByNot(string ENT_ServerID, DateTime BeginTime, DateTime endTime)
        {
            DBHelper db = new DBHelper();
            string sql = @"
                        select * from
                        (
                            select  a.ApplyType,a.ApplyStatus,isnull(b.Num,0) ApplyCount
                            from 
                            (
	                            select '初始注册' ApplyType,1 orderby,'未申报' ApplyStatus
                                union all select '初始注册',2, '待确认' 
	                            union all select '初始注册',3, '已申报' 
	                            union all select '初始注册',4 ,'已受理' 
	                            union all select '初始注册',5 ,'已驳回'
	                            union all select '初始注册',6 ,'已办结'
	                            union all select '重新注册' ,1 ,'未申报' 
                                union all select '重新注册',2, '待确认' 
	                            union all select '重新注册',3, '已申报' 
	                            union all select '重新注册',4 ,'已受理' 
	                            union all select '重新注册',5 ,'已驳回'
	                            union all select '重新注册',6 ,'已办结'
	                            union all select '增项注册' ,1 ,'未申报' 
                                union all select '增项注册',2, '待确认' 
	                            union all select '增项注册',3, '已申报' 
	                            union all select '增项注册',4 ,'已受理' 
	                            union all select '增项注册',5 ,'已驳回'
	                            union all select '增项注册',6 ,'已办结'
	                            union all select '延期注册' ,1 ,'未申报' 
                                union all select '延期注册',2, '待确认' 
	                            union all select '延期注册',3, '已申报' 
	                            union all select '延期注册',4 ,'已受理' 
	                            union all select '延期注册',5 ,'已驳回'
	                            union all select '延期注册',6 ,'已办结'
	                            union all select '个人信息变更' ,1 ,'未申报' 
                                union all select '个人信息变更',2, '待确认' 
	                            union all select '个人信息变更',3, '已申报' 
	                            union all select '个人信息变更',4 ,'已受理' 
	                            union all select '个人信息变更',5 ,'已驳回'
	                            union all select '个人信息变更',6 ,'已办结'
		                        union all select '执业企业变更' ,1 ,'未申报' 
                                union all select '执业企业变更',2, '待确认' 
	                            union all select '执业企业变更',3, '已申报' 
	                            union all select '执业企业变更',4 ,'已受理' 
	                            union all select '执业企业变更',5 ,'已驳回'
	                            union all select '执业企业变更',6 ,'已办结'
	                            union all select '企业信息变更' ,1 ,'未申报' 
                                union all select '企业信息变更',2, '待确认' 
	                            union all select '企业信息变更',3, '已申报' 
	                            union all select '企业信息变更',4 ,'已受理' 
	                            union all select '企业信息变更',5 ,'已驳回'
	                            union all select '企业信息变更',6 ,'已办结'
	                            union all select '遗失补办' ,1 ,'未申报' 
                                union all select '遗失补办',2, '待确认' 
	                            union all select '遗失补办',3, '已申报' 
	                            union all select '遗失补办',4 ,'已受理' 
	                            union all select '遗失补办',5 ,'已驳回'
	                            union all select '遗失补办',6 ,'已办结'
	                            union all select '注销' ,1 ,'未申报' 
                                union all select '注销',2, '待确认' 
	                            union all select '注销',3, '已申报' 
	                            union all select '注销',4 ,'已受理' 
	                            union all select '注销',5 ,'已驳回'
	                            union all select '注销',6 ,'已办结'
                            ) a
                            left join
                            (
	                            select ApplyType,ApplyStatus ,count(*) as Num
	                             from 
	                             (
	                                  select case when ApplyTypeSub <>''  then ApplyTypeSub else  ApplyType end as ApplyType
	                                 ,case when ApplyStatus ='已受理' or ApplyStatus ='已上报' or ApplyStatus ='已收件'
			                                  or ApplyStatus ='已审查' or ApplyStatus ='已决定' or ApplyStatus ='已公示' then '已受理'
		                                   when  ApplyStatus ='已公告' then '已办结'
		                                   else  ApplyStatus end as ApplyStatus
	                                 from apply
	                                 where [ENT_ServerID] = '{0}'
	                                 and 
	                                 (
		                                (ApplyTime between '{1}' and '{2}')
		                                 or (ApplyTime is null and cjsj between '{1}' and '{2}')
	                                 )
	                            ) t
	                            group  by ApplyType,ApplyStatus
                            ) b
                            on a. ApplyType =b.ApplyType and a.ApplyStatus = b.ApplyStatus
                        ) r
                        pivot ( sum(r.ApplyCount) for r.ApplyStatus in ([未申报],[待确认],[已申报],[已受理],[已驳回],[已办结])) as rtn
                        ORDER BY rtn.[ApplyType];";
            return db.GetFillData(string.Format(sql, ENT_ServerID, BeginTime.ToString("yyyy-MM-dd"), endTime.ToString("yyyy-MM-dd 23:59:59")));
        }

        /// <summary>
        /// 执行批量上报
        /// </summary>
        /// <param name="zt">更改状态</param>
        /// <param name="sbsj">上报时间</param>
        /// <param name="sbr">上报人</param>
        /// <param name="applyid">数据主键ID</param>
        /// <returns></returns>
        public static int ExePatchReport(string zt, DateTime sbsj, string sbr, string applyid)
        {
            string sql = " update Apply set applystatus='{0}', reportdate='{1}' , reportman='{2}' where applyid in ({3})";
            return (new DBHelper()).GetExcuteNonQuery(string.Format(sql, zt, sbsj, sbr, applyid));
        }

        /// <summary>
        /// 修改批量保存要上报数据，只将上报批次号赋值，不修改申报状态
        /// </summary>
        /// <param name="ReportCode"></param>
        /// <returns></returns>
        public static int UpdatePatchReport(DbTransaction tran, string ReportCode)
        {
            string sql = " update Apply set ReportCode=null where ReportCode='{0}'";
            return (new DBHelper()).GetExcuteNonQuery(tran, string.Format(sql, ReportCode));
        }

        /// <summary>
        /// 批量保存要上报数据，只将上报批次号赋值，不修改申报状态
        /// </summary>
        /// <param name="ReportDate">选择上报数据日期</param>
        /// <param name="applyid">申请单ID集合，用逗号分隔</param>
        /// <returns></returns>
        public static int SavePatchReport(DbTransaction tran, string ReportCode, string applyid)
        {
            string sql = " update Apply set ReportCode='{0}' where applyid in ({1})";
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
            string sql = " update Apply set ReportCode=null where ReportCode='{0}' and ENT_City like '{1}%' and ApplyType='{2}'";
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
            string sql = " update Apply  set reportdate='{0}' , reportman='{1}',applystatus='已公告',NoticeDate='{0}',[XGR] = '{1}' ,[XGSJ] ='{0}'  where ReportCode = '{2}' and ENT_City like '{3}%' and ApplyType='{4}'";
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
            string sql = " update Apply  set reportdate='{0}' , reportman='{1}',applystatus='已上报' where ReportCode = '{2}' and ENT_City like '{3}%' and ApplyType='{4}'";
            return (new DBHelper()).GetExcuteNonQuery(string.Format(sql, DateTime.Now, ReportMan, ReportCode, Region, ApplyType));
        }

        /// <summary>
        /// 批量取消上报
        /// </summary>
        /// <param name="ReportCode">上报批次号</param>
        /// <param name="Region">区县</param>
        ///  <param name="ApplyType">事项类型</param>
        /// <returns></returns>
        public static int CancelPatchReport(string ReportCode, string Region, string ApplyType)
        {
            string sql = " update Apply set reportdate=null,reportman=null,applystatus='区县审查' where ReportCode = '{0}' and ENT_City like '{1}%' and ApplyType='{2}'";
            return (new DBHelper()).GetExcuteNonQuery(string.Format(sql, ReportCode, Region, ApplyType));
        }

        /// <summary>
        /// 批量收件
        /// </summary>
        /// <param name="ReportCode">上报批次号</param>
        /// <param name="Region">区县</param>
        /// <param name="ApplyType">申报类型</param>
        /// <param name="AcceptMan">收件人</param>
        /// <returns></returns>
        public static int PatchAccept(string ReportCode, string Region, string ApplyType, string AcceptMan)
        {
            string sql = " update Apply  set AcceptDate='{3}' , AcceptMan='{4}',applystatus='{5}' where ReportCode={0} and ENT_City like '{1}%' and ApplyType='{2}' and applystatus='{6}'";
            return (new DBHelper()).GetExcuteNonQuery(string.Format(sql, ReportCode, Region, ApplyType, DateTime.Now, AcceptMan, EnumManager.ApplyStatus.已收件, EnumManager.ApplyStatus.已上报));
        }

        /// <summary>
        /// 统计图事项类型总数统计
        /// </summary>
        /// <param name="city">是区县就带区，不是默认为空</param>
        /// <returns></returns>
        public static DataTable GetApplyGroupByNot(string city)
        {
            DBHelper db = new DBHelper();
            string sql = @"select ApplyType,count(ApplyID)as Num  from apply where  1=1 " + city + " group by ApplyType";
            return db.GetFillData(sql);
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
        public static DataTable GetReportList(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            //select  ENT_City,[ApplyType],cast([ReportDate] as date) as ReportDate,count(*) as ManCount
            //,case when max([ReportMan]) is null then '未上报' else '已上报' end ReportStatus
            // FROM [dbo].[Apply]
            //where ReportDate is not null and ENT_City='朝阳区'
            //group by ENT_City,[ApplyType],cast([ReportDate] as date) 
            //having case when max([ReportMan]) is null then '未上报' else '已上报' end like '%'

            //            return CommonDAL.GetDataTable(startRowIndex, maximumRows
            //                , @"(select  ENT_City,[ApplyType],convert(varchar(10),[ReportDate],120) as ReportDate,count(*) as ManCount
            //                        ,(case when max([ReportMan]) is null then '未上报' else '已上报' end ) ReportStatus
            //                        ,(case when max([AcceptMan]) is null then '未收件' else '已收件' end ) AcceptStatus
            //                         FROM [dbo].[Apply] where ReportDate is not null
            //                        group by ENT_City,[ApplyType],convert(varchar(10),[ReportDate],120)
            //                    ) t"
            //                , "*"
            //                , filterWhereString, orderBy == "" ? " ReportDate" : orderBy);
            return CommonDAL.GetDataTable(startRowIndex, maximumRows
              , @"(select max(ReportDate) as ReportDate
                    ,ReportCode
                    ,max(ApplyType) as ApplyType
                    ,max(ApplyTypeSub) as ApplyTypeSub
                    ,max(ENT_Name) as ENT_Name
                    ,count(1) as ManCount
                    ,(case when max([ReportMan]) is null then '未上报' else '已上报' end ) ReportStatus
                    ,(case when max([CheckMan]) is null then '未审查' else '已审查' end ) CheckStatus
                    ,(case when max([ConfirmMan]) is null then '未审查' else '已审查' end ) ConfirmStatus            
                    ,max(ENT_City)  as  ENT_City
                     from apply group by ReportCode having ReportCode is not null
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
            //            return CommonDAL.SelectRowCount(@"(select  ENT_City,[ApplyType],convert(varchar(10),[ReportDate],120) as ReportDate,count(*) as ManCount
            //                                                ,(case when max([ReportMan]) is null then '未上报' else '已上报' end ) ReportStatus
            //                                                ,(case when max([AcceptMan]) is null then '未收件' else '已收件' end ) AcceptStatus
            //                                                 FROM [dbo].[Apply] where ReportDate is not null
            //                                                group by ENT_City,[ApplyType],convert(varchar(10),[ReportDate],120)
            //                                                ) t", filterWhereString);
            return CommonDAL.SelectRowCount(@"(select max(ReportDate) as ReportDate
                                                ,ReportCode
                                                ,max(ApplyType) as ApplyType
                                                ,max(ApplyTypeSub) as ApplyTypeSub
                                                ,max(ENT_Name) as ENT_Name
                                                ,count(1) as ManCount
                                                ,(case when max([ReportMan]) is null then '未上报' else '已上报' end ) ReportStatus
                                                ,(case when max([CheckMan]) is null then '未审查' else '已审查' end ) CheckStatus
                                                ,(case when max([ConfirmMan]) is null then '未审查' else '已审查' end ) ConfirmStatus      
                                                ,max(ENT_City)  as  ENT_City
                                                 from apply group by ReportCode having ReportCode is not null
                                                ) t", filterWhereString);
        }

        /// <summary>
        /// 批量保存要公示数据，只将公示流水号赋值，不修改申报状态
        /// </summary>
        /// <param name="PublicCode">公示流水号</param>
        /// <param name="applyid">申请单ID集合，用逗号分隔</param>
        /// <returns></returns>
        public static int SavePublicCode(string PublicCode, string applyid)
        {
            string sql = " update Apply set PublicCode='{0}' where applyid in ({1})";
            return (new DBHelper()).GetExcuteNonQuery(string.Format(sql, PublicCode, applyid));
        }

        /// <summary>
        /// 批量公示
        /// </summary>
        /// <param name="zt">申报状态</param>
        /// <param name="gssj">公示时间</param>
        /// <param name="gsr">公示人</param>
        /// <param name="gspch">公示批次号</param>
        /// <param name="applyid">申请表ID</param>
        /// <returns></returns>
        public static int ExePublicReport(string zt, DateTime gssj, string gsr, string gspch, string applyid)
        {
            string sql = " update Apply set applystatus='{0}', PublicDate='{1}' , PublicMan='{2}',PublicCode='{3}' where applyid in ({4})";
            return (new DBHelper()).GetExcuteNonQuery(string.Format(sql, zt, gssj, gsr, gspch, applyid));
        }

        /// <summary>
        /// 公示查询获取实体集合
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        /// <summary>
        public static DataTable GetPublicList(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            //南静  2019-09-27   添加 ‘已公告’ 筛选显示
            return CommonDAL.GetDataTable(startRowIndex, maximumRows
                , @"(
                        select PublicCode,max( PSN_Level) as PSN_Level, MAX(ConfirmResult) as ConfirmResult,  max(case when NoticeDate is not null then '已公告' when PublicMan is null then '未公示' else '公示中' end)  as 'public',
                        count(1) as Num ,max(applytype) as ApplyType
                         from apply where PublicCode is not null group by PublicCode 
                    ) t"
                , "*"
                , filterWhereString, orderBy == "" ? " PublicCode desc" : orderBy);
        }

        /// <summary>
        /// 统计公示查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectPublicCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount(@"( select PublicCode,max( PSN_Level) as PSN_Level, MAX(ConfirmResult) as ConfirmResult, max(case when PublicMan is null then '未公示' else '公示中' end) as 'public',
                        count(1) as Num ,max(applytype) as ApplyType
                         from apply where PublicCode is not null group by PublicCode 
                                                ) t", filterWhereString);
        }

        /// <summary>
        /// 公示取消公示，退回让用户重新选择
        /// </summary>
        /// <param name="PublicCode">公示批次号</param>
        /// <returns></returns>
        public static int DeleteUpdatePublic(string PublicCode)
        {
            string sql = string.Format("update  apply  set PublicCode=Null where PublicCode='{0}' and ApplyStatus='已决定'", PublicCode);
            return (new DBHelper()).GetExcuteNonQuery(sql);
        }

        /// <summary>
        /// 根据批次号展示批量公示
        /// </summary>
        /// <param name="zt">申报状态</param>
        /// <param name="gssj">公示时间</param>
        /// <param name="gsr">公示人</param>
        /// <param name="gspch">公示批次号</param>
        /// <returns></returns>
        public static int ExePublicSelect(string zt, DateTime gssj, string gsr, string gspch)
        {
            string sql = "update Apply set applystatus='{0}', PublicDate='{1}' , PublicMan='{2}' where PublicCode='{3}'";
            return (new DBHelper()).GetExcuteNonQuery(string.Format(sql, zt, gssj, gsr, gspch));
        }

        /// <summary>
        /// 批量保存要公示数据，只将公示流水号赋值，不修改申报状态
        /// </summary>
        /// <param name="NoticeCode">公告流水号</param>
        /// <param name="applyid">申请单ID集合，用逗号分隔</param>
        /// <returns></returns>
        public static int SaveNoticeCode(string NoticeCode, string applyid)
        {
            string sql = " update Apply set NoticeCode = null where NoticeCode='{0}';update Apply set NoticeCode='{0}' where applyid in ({1})";
            return (new DBHelper()).GetExcuteNonQuery(string.Format(sql, NoticeCode, applyid));
        }

        /// <summary>
        /// 批量公告
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="zt">申报状态</param>
        /// <param name="gssj">公告时间</param>
        /// <param name="gsr">公告人</param>
        /// <param name="gspch">公告批次号</param>
        /// <param name="applyid">申请表ID</param>
        /// <returns></returns>
        public static int ExeNoticeReport(DbTransaction tran, string zt, DateTime gssj, string gsr, string gspch, string applyid)
        {
            string sql = " update Apply set applystatus='{0}', NoticeDate='{1}' , NoticeMan='{2}',NoticeCode='{3}' where applyid in ({4})";
            return (new DBHelper()).GetExcuteNonQuery(tran, string.Format(sql, zt, gssj, gsr, gspch, applyid));
        }

        /// <summary>
        /// 公告查询获取实体集合
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        /// <summary>
        public static DataTable GetNoticeList(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows
                , @"(
                        select NoticeCode, max(case when NoticeMan is null then '未公告' else '已公告' end) as 'Notice',
                        count(1) as Num ,max(applytype) as ApplyType,max([NoticeDate]) as NoticeDate,max([CodeDate]) CodeDate,max([ConfirmResult]) as ConfirmResult
                         from apply where NoticeCode is not null group by NoticeCode 
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
                         from apply where NoticeCode is not null group by NoticeCode 
                                                ) t", filterWhereString);
        }

        /// <summary>
        /// 公告取消公公告，退回让用户重新选择
        /// </summary>
        /// <param name="NoticeCode">公告批次号</param>
        /// <returns></returns>
        public static int DeleteUpdateNotice(string NoticeCode)
        {
            string sql = string.Format("update  apply  set NoticeCode=Null where NoticeCode='{0}' and ApplyStatus='已决定'", NoticeCode);
            return (new DBHelper()).GetExcuteNonQuery(sql);
        }

        /// <summary>
        /// 公告根据批次号展示批量公示
        /// </summary>
        /// <param name="zt">申报状态</param>
        /// <param name="gssj">公告时间</param>
        /// <param name="gsr">公告人</param>
        /// <param name="gspch">公告批次号</param>
        /// <returns></returns>
        public static int ExeUpdateNotice(DbTransaction tran, string zt, DateTime gssj, string gsr, string gspch)
        {
            string sql = "update Apply set applystatus='{0}', NoticeDate='{1}' , NoticeMan='{2}' where NoticeCode='{3}'";
            return (new DBHelper()).GetExcuteNonQuery(tran, string.Format(sql, zt, gssj, gsr, gspch));
        }

        /// <summary>
        /// 区县发送通知Table
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        /// <summary>
        public static DataTable GetNotificationList(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, @"(SELECT NoticeCode,ENT_City,MAX(ApplyType) AS ApplyType,COUNT(ApplyID)AS NUM,MAX(GetDateTime)AS GetDateTime FROM 
                                                                        (
                                                                        SELECT A.ApplyID, A.PSN_Name,A.PSN_CertificateNO,A.PSN_RegisterNo,A.ApplyType,A.NoticeCode,A.ENT_City,B.GetDateTime FROM 
                                                                        [dbo].[Apply] A LEFT JOIN [dbo].[ApplyNews] B ON A.[ApplyID]=B.[ApplyID]
                                                                        )u GROUP BY NoticeCode,ENT_City HAVING NoticeCode IS NOT NULL) t"
                , "*"
                , filterWhereString, orderBy == "" ? " NoticeCode" : orderBy);
        }

        /// <summary>
        /// 统计区县发送通知结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectNotificationCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount(@"(SELECT NoticeCode,ENT_City,MAX(ApplyType) AS ApplyType,COUNT(ApplyID)AS NUM,MAX(GetDateTime)AS GetDateTime FROM 
                                                                        (
                                                                        SELECT A.ApplyID, A.PSN_Name,A.PSN_CertificateNO,A.PSN_RegisterNo,A.ApplyType,A.NoticeCode,A.ENT_City,B.GetDateTime FROM 
                                                                        [dbo].[Apply] A LEFT JOIN [dbo].[ApplyNews] B ON A.[ApplyID]=B.[ApplyID]
                                                                        )u GROUP BY NoticeCode,ENT_City HAVING NoticeCode IS NOT NULL) t", filterWhereString);
        }

        /// <summary>
        /// 获取注册申请受理人列表
        /// </summary>
        /// <param name="q">查询条件</param>
        /// <returns></returns>
        public static DataTable GetApplyGetMan(QueryParamOB q)
        {
            DBHelper db = new DBHelper();
            string sql = @"  select 0 RowNum,[GetMan]
		                    ,0 '初始注册'
		                    ,0 '变更注册'
		                    ,0 '延期注册'
		                    ,0 '增项注册'
		                    ,0 '重新注册'
		                    ,0 '遗失补办'
		                    ,0 '注销'
		                    ,count(*) '小计'
                      FROM [dbo].[Apply]
                      where  NoticeDate is not null {0}
                      group by [GetMan]";
            return db.GetFillData(string.Format(sql, q.ToWhereString()));
        }

        /// <summary>
        /// 统计注册受理数量
        /// </summary>
        /// <param name="q">查询条件</param>
        /// <returns></returns>
        public static DataTable GetApplyGetManDoCount(QueryParamOB q)
        {
            DBHelper db = new DBHelper();
            string sql = @"select [GetMan],[ApplyType],count(*) num
                            FROM [dbo].[Apply]
                            where  NoticeDate is not null {0}
                            group by [GetMan],[ApplyType]";
            return db.GetFillData(string.Format(sql, q.ToWhereString()));
        }

        /// <summary>
        /// 区县业务员汇总
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <param name="filterWhereString"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static DataTable GetList2(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "[dbo].[Apply] left join [dbo].[Qualification] on [dbo].[Apply].[PSN_CertificateNO]=[dbo].[Qualification].[ZJHM]", "*", filterWhereString, orderBy == "" ? " ApplyID" : orderBy);
        }

        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount2(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.Apply", filterWhereString);
        }

        /// <summary>
        /// 查询在施锁定证书记录数量
        /// </summary>
        /// <param name="applyidlist">申请单ID集合</param>
        /// <returns>在施锁定证书记录数量</returns>
        public static int Batch_ZSSDIsorNot(string applyidlist)
        {
            //string Level = PSN_Level == "二级" ? "二级建造师" : "二级临时建造师";
            string sql = string.Format("and CheckZSSD=1 and applyid in ({0})", applyidlist);
            return CommonDAL.SelectRowCount("[dbo].[apply]", sql);

        }

        /// <summary>
        /// 获取可放号证书编号：证书编号尾号+1
        /// </summary>
        /// <returns>证书编号</returns>
        public static long GetNextPSN_RegisterCertificateNo()
        {
            string sql = @"SELECT isnull(max([PSN_RegisterCertificateNo]),'0')         
                            FROM [dbo].[COC_TOW_Person_BaseInfo]
                            where [PSN_RegisterNO] like '京211%'";

            long code = Convert.ToInt64(CommonDAL.GetDataTable(sql).Rows[0][0]);
            return code + 1;          
        }

        /// <summary>
        /// 获取可放号注册编号：证书注册号尾号+1
        /// </summary>
        /// <returns>注册号</returns>
        public static long GetNextPSN_RegisterNO()
        {
            string sql = string.Format(@"SELECT isnull(max(right([PSN_RegisterNO],5)),'0')         
                            FROM [dbo].[COC_TOW_Person_BaseInfo]
                            where [PSN_RegisterNO] like '京211____{0}%'",DateTime.Now.ToString("yyyy"));

            long code = Convert.ToInt64(CommonDAL.GetDataTable(sql).Rows[0][0]);
            if (code>=99999)
            {
                throw new Exception("证书流水号已经操作最大5位允许范围");
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
            FROM dbo.Apply
			WHERE NoticeCode = @NoticeCode";

            string rtn = "";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("NoticeCode", DbType.String, NoticeCode));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                
                if (reader.Read())
                {
                   rtn= Convert.ToString(reader["ConfirmResult"]);
                }
                reader.Close();
                db.Close();
                return rtn;
            }
        }
        #endregion


        /// <summary>
        /// 临时调用
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        /// <summary>
        public static DataTable GetList_temp_ping(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.temp_ping", "*", filterWhereString, orderBy == "" ? " DataType,DataID" : orderBy);
        }
        /// <summary>
        /// 临时调用
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount_temp_ping(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.temp_ping", filterWhereString);
        }
    }
}
