using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

using Model;

namespace DataAccess
{
    /// <summary>
    /// 业务类实现--ExamSignUpDAL(填写类描述)
    /// </summary>
    public class ExamSignUpDAL
    {
        public ExamSignUpDAL() { }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_ExamSignUpOB">对象实体类</param>
        /// <returns></returns>
        public static long Insert(ExamSignUpOB _ExamSignUpOB)
        {
            return Insert(null, _ExamSignUpOB);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_ExamSignUpOB">对象实体类</param>
        /// <returns></returns>
        public static long Insert(DbTransaction tran, ExamSignUpOB _ExamSignUpOB)
        {
            DBHelper db = new DBHelper();

            string sql = @"
			INSERT INTO dbo.ExamSignUp(SignUpCode,SignUpDate,WorkerID,UnitID,TrainUnitID,ExamPlanID,WorkStartDate,WorkYearNumer,PersonDetail,HireUnitAdvise,AdminUnitAdvise,CheckCode,CheckResult,CheckMan,CheckDate,PayNoticeCode,PayNoticeResult,PayNoticeMan,PayNoticeDate,PayMoney,PayConfirmCode,PayConfirmRult,PayConfirmMan,PayConfirmDate,FacePhoto,Status,WorkerName,CertificateType,CertificateCode,UnitName,UnitCode,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,SKILLLEVEL,S_SEX,S_CULTURALLEVEL,S_PHONE,S_BIRTHDAY,IsConditions,FIRSTTRIALTIME,S_TRAINUNITNAME,SignUpMan,Promise,SignUpPlaceID,PlaceName,CheckDatePlan,FirstCheckType,AcceptMan,AcceptTime,AcceptResult,ENT_ContractType,ENT_ContractStartTime,ENT_ContractENDTime,SignupPromise,ZACheckTime,ZACheckResult,ZACheckRemark,Job,SafeTrainType,SafeTrainUnit,SafeTrainUnitCode,SafeTrainUnitValidEndDate,SafeTrainUnitOfDept)
			VALUES (@SignUpCode,@SignUpDate,@WorkerID,@UnitID,@TrainUnitID,@ExamPlanID,@WorkStartDate,@WorkYearNumer,@PersonDetail,@HireUnitAdvise,@AdminUnitAdvise,@CheckCode,@CheckResult,@CheckMan,@CheckDate,@PayNoticeCode,@PayNoticeResult,@PayNoticeMan,@PayNoticeDate,@PayMoney,@PayConfirmCode,@PayConfirmRult,@PayConfirmMan,@PayConfirmDate,@FacePhoto,@Status,@WorkerName,@CertificateType,@CertificateCode,@UnitName,@UnitCode,@CreatePersonID,@CreateTime,@ModifyPersonID,@ModifyTime,@SKILLLEVEL,@S_SEX,@S_CULTURALLEVEL,@S_PHONE,@S_BIRTHDAY,@IsConditions,@FIRSTTRIALTIME,@S_TRAINUNITNAME,@SignUpMan,@Promise,@SignUpPlaceID,@PlaceName,@CheckDatePlan,@FirstCheckType,@AcceptMan,@AcceptTime,@AcceptResult,@ENT_ContractType,@ENT_ContractStartTime,@ENT_ContractENDTime,@SignupPromise,@ZACheckTime,@ZACheckResult,@ZACheckRemark,@Job,@SafeTrainType,@SafeTrainUnit,@SafeTrainUnitCode,@SafeTrainUnitValidEndDate,@SafeTrainUnitOfDept);SELECT @ExamSignUpID = @@IDENTITY";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateOutParameter("ExamSignUpID", DbType.Int64));
            p.Add(db.CreateParameter("SignUpCode", DbType.String, _ExamSignUpOB.SignUpCode));
            p.Add(db.CreateParameter("SignUpDate", DbType.DateTime, _ExamSignUpOB.SignUpDate));
            p.Add(db.CreateParameter("WorkerID", DbType.Int64, _ExamSignUpOB.WorkerID));
            p.Add(db.CreateParameter("UnitID", DbType.Int64, _ExamSignUpOB.UnitID));
            p.Add(db.CreateParameter("TrainUnitID", DbType.Int64, _ExamSignUpOB.TrainUnitID));
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, _ExamSignUpOB.ExamPlanID));
            p.Add(db.CreateParameter("WorkStartDate", DbType.DateTime, _ExamSignUpOB.WorkStartDate));
            p.Add(db.CreateParameter("WorkYearNumer", DbType.Int32, _ExamSignUpOB.WorkYearNumer));
            p.Add(db.CreateParameter("PersonDetail", DbType.String, _ExamSignUpOB.PersonDetail));
            p.Add(db.CreateParameter("HireUnitAdvise", DbType.String, _ExamSignUpOB.HireUnitAdvise));
            p.Add(db.CreateParameter("AdminUnitAdvise", DbType.String, _ExamSignUpOB.AdminUnitAdvise));
            p.Add(db.CreateParameter("CheckCode", DbType.String, _ExamSignUpOB.CheckCode));
            p.Add(db.CreateParameter("CheckResult", DbType.String, _ExamSignUpOB.CheckResult));
            p.Add(db.CreateParameter("CheckMan", DbType.String, _ExamSignUpOB.CheckMan));
            p.Add(db.CreateParameter("CheckDate", DbType.DateTime, _ExamSignUpOB.CheckDate));
            p.Add(db.CreateParameter("PayNoticeCode", DbType.String, _ExamSignUpOB.PayNoticeCode));
            p.Add(db.CreateParameter("PayNoticeResult", DbType.String, _ExamSignUpOB.PayNoticeResult));
            p.Add(db.CreateParameter("PayNoticeMan", DbType.String, _ExamSignUpOB.PayNoticeMan));
            p.Add(db.CreateParameter("PayNoticeDate", DbType.DateTime, _ExamSignUpOB.PayNoticeDate));
            p.Add(db.CreateParameter("PayMoney", DbType.Decimal, _ExamSignUpOB.PayMoney));
            p.Add(db.CreateParameter("PayConfirmCode", DbType.String, _ExamSignUpOB.PayConfirmCode));
            p.Add(db.CreateParameter("PayConfirmRult", DbType.String, _ExamSignUpOB.PayConfirmRult));
            p.Add(db.CreateParameter("PayConfirmMan", DbType.String, _ExamSignUpOB.PayConfirmMan));
            p.Add(db.CreateParameter("PayConfirmDate", DbType.DateTime, _ExamSignUpOB.PayConfirmDate));
            p.Add(db.CreateParameter("FacePhoto", DbType.String, _ExamSignUpOB.FacePhoto));
            p.Add(db.CreateParameter("Status", DbType.String, _ExamSignUpOB.Status));
            p.Add(db.CreateParameter("WorkerName", DbType.String, _ExamSignUpOB.WorkerName));
            p.Add(db.CreateParameter("CertificateType", DbType.String, _ExamSignUpOB.CertificateType));
            p.Add(db.CreateParameter("CertificateCode", DbType.String, _ExamSignUpOB.CertificateCode));
            p.Add(db.CreateParameter("UnitName", DbType.String, _ExamSignUpOB.UnitName));
            p.Add(db.CreateParameter("UnitCode", DbType.String, _ExamSignUpOB.UnitCode));
            p.Add(db.CreateParameter("CreatePersonID", DbType.Int64, _ExamSignUpOB.CreatePersonID));
            p.Add(db.CreateParameter("CreateTime", DbType.DateTime, _ExamSignUpOB.CreateTime));
            p.Add(db.CreateParameter("ModifyPersonID", DbType.Int64, _ExamSignUpOB.ModifyPersonID));
            p.Add(db.CreateParameter("ModifyTime", DbType.DateTime, _ExamSignUpOB.ModifyTime));
            p.Add(db.CreateParameter("SKILLLEVEL", DbType.String, _ExamSignUpOB.SKILLLEVEL));
            p.Add(db.CreateParameter("S_SEX", DbType.String, _ExamSignUpOB.S_SEX));
            p.Add(db.CreateParameter("S_CULTURALLEVEL", DbType.String, _ExamSignUpOB.S_CULTURALLEVEL));
            p.Add(db.CreateParameter("S_PHONE", DbType.String, _ExamSignUpOB.S_PHONE));
            p.Add(db.CreateParameter("S_BIRTHDAY", DbType.DateTime, _ExamSignUpOB.S_BIRTHDAY));
            p.Add(db.CreateParameter("IsConditions", DbType.String, _ExamSignUpOB.IsConditions));
            p.Add(db.CreateParameter("FIRSTTRIALTIME", DbType.DateTime, _ExamSignUpOB.FIRSTTRIALTIME));
            p.Add(db.CreateParameter("S_TRAINUNITNAME", DbType.String, _ExamSignUpOB.S_TRAINUNITNAME));
            p.Add(db.CreateParameter("SignUpMan", DbType.String, _ExamSignUpOB.SignUpMan));
            p.Add(db.CreateParameter("Promise", DbType.Int32, _ExamSignUpOB.Promise));
            p.Add(db.CreateParameter("SignUpPlaceID", DbType.Int64, _ExamSignUpOB.SignUpPlaceID));
            p.Add(db.CreateParameter("PlaceName", DbType.String, _ExamSignUpOB.PlaceName));
            p.Add(db.CreateParameter("CheckDatePlan", DbType.DateTime, _ExamSignUpOB.CheckDatePlan));
            p.Add(db.CreateParameter("FirstCheckType", DbType.Int32, _ExamSignUpOB.FirstCheckType));
            p.Add(db.CreateParameter("AcceptResult", DbType.String, _ExamSignUpOB.AcceptResult));
            p.Add(db.CreateParameter("AcceptMan", DbType.String, _ExamSignUpOB.AcceptMan));
            p.Add(db.CreateParameter("AcceptTime", DbType.DateTime, _ExamSignUpOB.AcceptTime));
            p.Add(db.CreateParameter("ENT_ContractType", DbType.Int32, _ExamSignUpOB.ENT_ContractType));
            p.Add(db.CreateParameter("ENT_ContractStartTime", DbType.DateTime, _ExamSignUpOB.ENT_ContractStartTime));
            p.Add(db.CreateParameter("ENT_ContractENDTime", DbType.DateTime, _ExamSignUpOB.ENT_ContractENDTime));
            p.Add(db.CreateParameter("SignupPromise", DbType.Int32, _ExamSignUpOB.SignupPromise));
            p.Add(db.CreateParameter("ZACheckTime", DbType.DateTime, _ExamSignUpOB.ZACheckTime));
            p.Add(db.CreateParameter("ZACheckResult", DbType.Int32, _ExamSignUpOB.ZACheckResult));
            p.Add(db.CreateParameter("ZACheckRemark", DbType.String, _ExamSignUpOB.ZACheckRemark));
            p.Add(db.CreateParameter("Job", DbType.String, _ExamSignUpOB.Job));
            p.Add(db.CreateParameter("SafeTrainType",DbType.String, _ExamSignUpOB.SafeTrainType));
			p.Add(db.CreateParameter("SafeTrainUnit",DbType.String, _ExamSignUpOB.SafeTrainUnit));
			p.Add(db.CreateParameter("SafeTrainUnitCode",DbType.String, _ExamSignUpOB.SafeTrainUnitCode));
			p.Add(db.CreateParameter("SafeTrainUnitValidEndDate",DbType.DateTime, _ExamSignUpOB.SafeTrainUnitValidEndDate));
            p.Add(db.CreateParameter("SafeTrainUnitOfDept", DbType.String, _ExamSignUpOB.SafeTrainUnitOfDept));
            
            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            _ExamSignUpOB.ExamSignUpID = Convert.ToInt64(p[0].Value);
            return rtn;
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_ExamSignUpOB">对象实体类</param>
        /// <returns></returns>
        public static int Update(ExamSignUpOB _ExamSignUpOB)
        {
            return Update(null, _ExamSignUpOB);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_ExamSignUpOB">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, ExamSignUpOB _ExamSignUpOB)
        {
            string sql = @"
			UPDATE dbo.ExamSignUp
				SET	SignUpCode = @SignUpCode,SignUpDate = @SignUpDate,WorkerID = @WorkerID,UnitID = @UnitID,TrainUnitID = @TrainUnitID,ExamPlanID = @ExamPlanID,WorkStartDate = @WorkStartDate,WorkYearNumer = @WorkYearNumer,PersonDetail = @PersonDetail,HireUnitAdvise = @HireUnitAdvise,AdminUnitAdvise = @AdminUnitAdvise,
                    CheckCode = @CheckCode,CheckResult = @CheckResult,CheckMan = @CheckMan,CheckDate = @CheckDate,PayNoticeCode = @PayNoticeCode,PayNoticeResult = @PayNoticeResult,PayNoticeMan = @PayNoticeMan,PayNoticeDate = @PayNoticeDate,PayMoney = @PayMoney,PayConfirmCode = @PayConfirmCode,PayConfirmRult = @PayConfirmRult,PayConfirmMan = @PayConfirmMan,PayConfirmDate = @PayConfirmDate,
                    FacePhoto = @FacePhoto,Status = @Status,WorkerName = @WorkerName,CertificateType = @CertificateType,CertificateCode = @CertificateCode,UnitName = @UnitName,UnitCode = @UnitCode,CreatePersonID = @CreatePersonID,CreateTime = @CreateTime,ModifyPersonID = @ModifyPersonID,ModifyTime = @ModifyTime,
                    SKILLLEVEL = @SKILLLEVEL,S_SEX = @S_SEX,S_CULTURALLEVEL = @S_CULTURALLEVEL,S_PHONE = @S_PHONE,S_BIRTHDAY = @S_BIRTHDAY,FIRSTTRIALTIME =@FIRSTTRIALTIME,S_TRAINUNITNAME = @S_TRAINUNITNAME,SignUpMan = @SignUpMan,Promise = @Promise,SignUpPlaceID = @SignUpPlaceID,PlaceName = @PlaceName,CheckDatePlan = @CheckDatePlan,FirstCheckType = @FirstCheckType,
                    LockTime =@LockTime,LockEndTime =@LockEndTime,LockReason =@LockReason,LockMan =@LockMan,AcceptMan =@AcceptMan,AcceptTime =@AcceptTime,AcceptResult =@AcceptResult,ENT_ContractType = @ENT_ContractType,ENT_ContractStartTime = @ENT_ContractStartTime,ENT_ContractENDTime = @ENT_ContractENDTime,SignupPromise = @SignupPromise,
                    ZACheckTime = @ZACheckTime,ZACheckResult = @ZACheckResult,ZACheckRemark = @ZACheckRemark,Job = @Job,SafeTrainType = @SafeTrainType,SafeTrainUnit = @SafeTrainUnit,SafeTrainUnitCode = @SafeTrainUnitCode,SafeTrainUnitValidEndDate = @SafeTrainUnitValidEndDate,SafeTrainUnitOfDept = @SafeTrainUnitOfDept
			WHERE
				ExamSignUpID = @ExamSignUpID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamSignUpID", DbType.Int64, _ExamSignUpOB.ExamSignUpID));
            p.Add(db.CreateParameter("SignUpCode", DbType.String, _ExamSignUpOB.SignUpCode));
            p.Add(db.CreateParameter("SignUpDate", DbType.DateTime, _ExamSignUpOB.SignUpDate));
            p.Add(db.CreateParameter("WorkerID", DbType.Int64, _ExamSignUpOB.WorkerID));
            p.Add(db.CreateParameter("UnitID", DbType.Int64, _ExamSignUpOB.UnitID));
            p.Add(db.CreateParameter("TrainUnitID", DbType.Int64, _ExamSignUpOB.TrainUnitID));
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, _ExamSignUpOB.ExamPlanID));
            p.Add(db.CreateParameter("WorkStartDate", DbType.DateTime, _ExamSignUpOB.WorkStartDate));
            p.Add(db.CreateParameter("WorkYearNumer", DbType.Int32, _ExamSignUpOB.WorkYearNumer));
            p.Add(db.CreateParameter("PersonDetail", DbType.String, _ExamSignUpOB.PersonDetail));
            p.Add(db.CreateParameter("HireUnitAdvise", DbType.String, _ExamSignUpOB.HireUnitAdvise));
            p.Add(db.CreateParameter("AdminUnitAdvise", DbType.String, _ExamSignUpOB.AdminUnitAdvise));
            p.Add(db.CreateParameter("CheckCode", DbType.String, _ExamSignUpOB.CheckCode));
            p.Add(db.CreateParameter("CheckResult", DbType.String, _ExamSignUpOB.CheckResult));
            p.Add(db.CreateParameter("CheckMan", DbType.String, _ExamSignUpOB.CheckMan));
            p.Add(db.CreateParameter("CheckDate", DbType.DateTime, _ExamSignUpOB.CheckDate));
            p.Add(db.CreateParameter("PayNoticeCode", DbType.String, _ExamSignUpOB.PayNoticeCode));
            p.Add(db.CreateParameter("PayNoticeResult", DbType.String, _ExamSignUpOB.PayNoticeResult));
            p.Add(db.CreateParameter("PayNoticeMan", DbType.String, _ExamSignUpOB.PayNoticeMan));
            p.Add(db.CreateParameter("PayNoticeDate", DbType.DateTime, _ExamSignUpOB.PayNoticeDate));
            p.Add(db.CreateParameter("PayMoney", DbType.Decimal, _ExamSignUpOB.PayMoney));
            p.Add(db.CreateParameter("PayConfirmCode", DbType.String, _ExamSignUpOB.PayConfirmCode));
            p.Add(db.CreateParameter("PayConfirmRult", DbType.String, _ExamSignUpOB.PayConfirmRult));
            p.Add(db.CreateParameter("PayConfirmMan", DbType.String, _ExamSignUpOB.PayConfirmMan));
            p.Add(db.CreateParameter("PayConfirmDate", DbType.DateTime, _ExamSignUpOB.PayConfirmDate));
            p.Add(db.CreateParameter("FacePhoto", DbType.String, _ExamSignUpOB.FacePhoto));
            p.Add(db.CreateParameter("Status", DbType.String, _ExamSignUpOB.Status));
            p.Add(db.CreateParameter("WorkerName", DbType.String, _ExamSignUpOB.WorkerName));
            p.Add(db.CreateParameter("CertificateType", DbType.String, _ExamSignUpOB.CertificateType));
            p.Add(db.CreateParameter("CertificateCode", DbType.String, _ExamSignUpOB.CertificateCode));
            p.Add(db.CreateParameter("UnitName", DbType.String, _ExamSignUpOB.UnitName));
            p.Add(db.CreateParameter("UnitCode", DbType.String, _ExamSignUpOB.UnitCode));
            p.Add(db.CreateParameter("CreatePersonID", DbType.Int64, _ExamSignUpOB.CreatePersonID));
            p.Add(db.CreateParameter("CreateTime", DbType.DateTime, _ExamSignUpOB.CreateTime));
            p.Add(db.CreateParameter("ModifyPersonID", DbType.Int64, _ExamSignUpOB.ModifyPersonID));
            p.Add(db.CreateParameter("ModifyTime", DbType.DateTime, _ExamSignUpOB.ModifyTime));
            p.Add(db.CreateParameter("SKILLLEVEL", DbType.String, _ExamSignUpOB.SKILLLEVEL));
            p.Add(db.CreateParameter("S_SEX", DbType.String, _ExamSignUpOB.S_SEX));
            p.Add(db.CreateParameter("S_CULTURALLEVEL", DbType.String, _ExamSignUpOB.S_CULTURALLEVEL));
            p.Add(db.CreateParameter("S_PHONE", DbType.String, _ExamSignUpOB.S_PHONE));
            p.Add(db.CreateParameter("S_BIRTHDAY", DbType.DateTime, _ExamSignUpOB.S_BIRTHDAY));
            p.Add(db.CreateParameter("IsConditions", DbType.String, _ExamSignUpOB.IsConditions));
            p.Add(db.CreateParameter("FIRSTTRIALTIME", DbType.DateTime, _ExamSignUpOB.FIRSTTRIALTIME));
            p.Add(db.CreateParameter("S_TRAINUNITNAME", DbType.String, _ExamSignUpOB.S_TRAINUNITNAME));
            p.Add(db.CreateParameter("SignUpMan", DbType.String, _ExamSignUpOB.SignUpMan));
            p.Add(db.CreateParameter("Promise", DbType.Int32, _ExamSignUpOB.Promise));
            p.Add(db.CreateParameter("SignUpPlaceID", DbType.Int64, _ExamSignUpOB.SignUpPlaceID));
            p.Add(db.CreateParameter("PlaceName", DbType.String, _ExamSignUpOB.PlaceName));
            p.Add(db.CreateParameter("CheckDatePlan", DbType.DateTime, _ExamSignUpOB.CheckDatePlan));
            p.Add(db.CreateParameter("FirstCheckType", DbType.Int32, _ExamSignUpOB.FirstCheckType));
            p.Add(db.CreateParameter("LockTime", DbType.DateTime, _ExamSignUpOB.LockTime));
            p.Add(db.CreateParameter("LockEndTime", DbType.DateTime, _ExamSignUpOB.LockEndTime));
            p.Add(db.CreateParameter("LockReason", DbType.String, _ExamSignUpOB.LockReason));
            p.Add(db.CreateParameter("LockMan", DbType.String, _ExamSignUpOB.LockMan));
            p.Add(db.CreateParameter("AcceptResult", DbType.String, _ExamSignUpOB.AcceptResult));
            p.Add(db.CreateParameter("AcceptMan", DbType.String, _ExamSignUpOB.AcceptMan));
            p.Add(db.CreateParameter("AcceptTime", DbType.DateTime, _ExamSignUpOB.AcceptTime));
            p.Add(db.CreateParameter("ENT_ContractType", DbType.Int32, _ExamSignUpOB.ENT_ContractType));
            p.Add(db.CreateParameter("ENT_ContractStartTime", DbType.DateTime, _ExamSignUpOB.ENT_ContractStartTime));
            p.Add(db.CreateParameter("ENT_ContractENDTime", DbType.DateTime, _ExamSignUpOB.ENT_ContractENDTime));
            p.Add(db.CreateParameter("SignupPromise", DbType.Int32, _ExamSignUpOB.SignupPromise));
            p.Add(db.CreateParameter("ZACheckTime", DbType.DateTime, _ExamSignUpOB.ZACheckTime));
            p.Add(db.CreateParameter("ZACheckResult", DbType.Int32, _ExamSignUpOB.ZACheckResult));
            p.Add(db.CreateParameter("ZACheckRemark", DbType.String, _ExamSignUpOB.ZACheckRemark));
            p.Add(db.CreateParameter("Job", DbType.String, _ExamSignUpOB.Job));
            p.Add(db.CreateParameter("SafeTrainType", DbType.String, _ExamSignUpOB.SafeTrainType));
            p.Add(db.CreateParameter("SafeTrainUnit", DbType.String, _ExamSignUpOB.SafeTrainUnit));
            p.Add(db.CreateParameter("SafeTrainUnitCode", DbType.String, _ExamSignUpOB.SafeTrainUnitCode));
            p.Add(db.CreateParameter("SafeTrainUnitValidEndDate", DbType.DateTime, _ExamSignUpOB.SafeTrainUnitValidEndDate));
            p.Add(db.CreateParameter("SafeTrainUnitOfDept", DbType.String, _ExamSignUpOB.SafeTrainUnitOfDept));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="ExamSignUpID">主键</param>
        /// <returns></returns>
        public static int Delete(long ExamSignUpID)
        {
            return Delete(null, ExamSignUpID);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamSignUpID">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, long ExamSignUpID)
        {
            string sql = @"DELETE FROM dbo.ExamSignUp WHERE ExamSignUpID = @ExamSignUpID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamSignUpID", DbType.Int64, ExamSignUpID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="_ExamSignUpOB">对象实体类</param>
        /// <returns></returns>
        public static int Delete(ExamSignUpOB _ExamSignUpOB)
        {
            return Delete(null, _ExamSignUpOB);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_ExamSignUpOB">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, ExamSignUpOB _ExamSignUpOB)
        {
            string sql = @"DELETE FROM dbo.ExamSignUp WHERE ExamSignUpID = @ExamSignUpID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamSignUpID", DbType.Int64, _ExamSignUpOB.ExamSignUpID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ExamSignUpID">主键</param>
        public static ExamSignUpOB GetObject(long? ExamSignUpID)
        {
            string sql = @"
			SELECT ExamSignUpID,SignUpCode,SignUpDate,WorkerID,UnitID,TrainUnitID,ExamPlanID,WorkStartDate,WorkYearNumer,PersonDetail,
HireUnitAdvise,AdminUnitAdvise,CheckCode,CheckResult,CheckMan,CheckDate,PayNoticeCode,PayNoticeResult,PayNoticeMan,PayNoticeDate,
PayMoney,PayConfirmCode,PayConfirmRult,PayConfirmMan,PayConfirmDate,FacePhoto,Status,WorkerName,CertificateType,CertificateCode,
UnitName,UnitCode,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,SKILLLEVEL,S_SEX,S_CULTURALLEVEL,
S_PHONE,S_BIRTHDAY,S_TRAINUNITNAME,ISCONDITIONS,FIRSTTRIALTIME,SheBaoCheck,SignUpMan,PROMISE,SIGNUPPLACEID,CHECKDATEPLAN,PLACENAME,FirstCheckType
,[LockTime],[LockEndTime],[LockReason],[LockMan],AcceptMan,AcceptTime,AcceptResult,ENT_ContractType,ENT_ContractStartTime,ENT_ContractENDTime,SignupPromise
,ZACheckTime,ZACheckResult,ZACheckRemark,Job,SafeTrainType,SafeTrainUnit,SafeTrainUnitCode,SafeTrainUnitValidEndDate,SafeTrainUnitOfDept
			FROM dbo.ExamSignUp
			WHERE ExamSignUpID = @ExamSignUpID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamSignUpID", DbType.Int64, ExamSignUpID));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    ExamSignUpOB _ExamSignUpOB = null;
                    if (reader.Read())
                    {
                        _ExamSignUpOB = new ExamSignUpOB();
                        if (reader["ExamSignUpID"] != DBNull.Value) _ExamSignUpOB.ExamSignUpID = Convert.ToInt64(reader["ExamSignUpID"]);
                        if (reader["SignUpCode"] != DBNull.Value) _ExamSignUpOB.SignUpCode = Convert.ToString(reader["SignUpCode"]);
                        if (reader["SignUpDate"] != DBNull.Value) _ExamSignUpOB.SignUpDate = Convert.ToDateTime(reader["SignUpDate"]);
                        if (reader["WorkerID"] != DBNull.Value) _ExamSignUpOB.WorkerID = Convert.ToInt64(reader["WorkerID"]);
                        if (reader["UnitID"] != DBNull.Value) _ExamSignUpOB.UnitID = Convert.ToInt64(reader["UnitID"]);
                        if (reader["TrainUnitID"] != DBNull.Value) _ExamSignUpOB.TrainUnitID = Convert.ToInt64(reader["TrainUnitID"]);
                        if (reader["ExamPlanID"] != DBNull.Value) _ExamSignUpOB.ExamPlanID = Convert.ToInt64(reader["ExamPlanID"]);
                        if (reader["WorkStartDate"] != DBNull.Value) _ExamSignUpOB.WorkStartDate = Convert.ToDateTime(reader["WorkStartDate"]);
                        if (reader["WorkYearNumer"] != DBNull.Value) _ExamSignUpOB.WorkYearNumer = Convert.ToInt32(reader["WorkYearNumer"]);
                        if (reader["PersonDetail"] != DBNull.Value) _ExamSignUpOB.PersonDetail = Convert.ToString(reader["PersonDetail"]);
                        if (reader["HireUnitAdvise"] != DBNull.Value) _ExamSignUpOB.HireUnitAdvise = Convert.ToString(reader["HireUnitAdvise"]);
                        if (reader["AdminUnitAdvise"] != DBNull.Value) _ExamSignUpOB.AdminUnitAdvise = Convert.ToString(reader["AdminUnitAdvise"]);
                        if (reader["CheckCode"] != DBNull.Value) _ExamSignUpOB.CheckCode = Convert.ToString(reader["CheckCode"]);
                        if (reader["CheckResult"] != DBNull.Value) _ExamSignUpOB.CheckResult = Convert.ToString(reader["CheckResult"]);
                        if (reader["CheckMan"] != DBNull.Value) _ExamSignUpOB.CheckMan = Convert.ToString(reader["CheckMan"]);
                        if (reader["CheckDate"] != DBNull.Value) _ExamSignUpOB.CheckDate = Convert.ToDateTime(reader["CheckDate"]);
                        if (reader["PayNoticeCode"] != DBNull.Value) _ExamSignUpOB.PayNoticeCode = Convert.ToString(reader["PayNoticeCode"]);
                        if (reader["PayNoticeResult"] != DBNull.Value) _ExamSignUpOB.PayNoticeResult = Convert.ToString(reader["PayNoticeResult"]);
                        if (reader["PayNoticeMan"] != DBNull.Value) _ExamSignUpOB.PayNoticeMan = Convert.ToString(reader["PayNoticeMan"]);
                        if (reader["PayNoticeDate"] != DBNull.Value) _ExamSignUpOB.PayNoticeDate = Convert.ToDateTime(reader["PayNoticeDate"]);
                        if (reader["PayMoney"] != DBNull.Value) _ExamSignUpOB.PayMoney = Convert.ToDecimal(reader["PayMoney"]);
                        if (reader["PayConfirmCode"] != DBNull.Value) _ExamSignUpOB.PayConfirmCode = Convert.ToString(reader["PayConfirmCode"]);
                        if (reader["PayConfirmRult"] != DBNull.Value) _ExamSignUpOB.PayConfirmRult = Convert.ToString(reader["PayConfirmRult"]);
                        if (reader["PayConfirmMan"] != DBNull.Value) _ExamSignUpOB.PayConfirmMan = Convert.ToString(reader["PayConfirmMan"]);
                        if (reader["PayConfirmDate"] != DBNull.Value) _ExamSignUpOB.PayConfirmDate = Convert.ToDateTime(reader["PayConfirmDate"]);
                        if (reader["FacePhoto"] != DBNull.Value) _ExamSignUpOB.FacePhoto = Convert.ToString(reader["FacePhoto"]);
                        if (reader["Status"] != DBNull.Value) _ExamSignUpOB.Status = Convert.ToString(reader["Status"]);
                        if (reader["WorkerName"] != DBNull.Value) _ExamSignUpOB.WorkerName = Convert.ToString(reader["WorkerName"]);
                        if (reader["CertificateType"] != DBNull.Value) _ExamSignUpOB.CertificateType = Convert.ToString(reader["CertificateType"]);
                        if (reader["CertificateCode"] != DBNull.Value) _ExamSignUpOB.CertificateCode = Convert.ToString(reader["CertificateCode"]);
                        if (reader["UnitName"] != DBNull.Value) _ExamSignUpOB.UnitName = Convert.ToString(reader["UnitName"]);
                        if (reader["UnitCode"] != DBNull.Value) _ExamSignUpOB.UnitCode = Convert.ToString(reader["UnitCode"]);
                        if (reader["CreatePersonID"] != DBNull.Value) _ExamSignUpOB.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                        if (reader["CreateTime"] != DBNull.Value) _ExamSignUpOB.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                        if (reader["ModifyPersonID"] != DBNull.Value) _ExamSignUpOB.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                        if (reader["ModifyTime"] != DBNull.Value) _ExamSignUpOB.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                        if (reader["SKILLLEVEL"] != DBNull.Value) _ExamSignUpOB.SKILLLEVEL = Convert.ToString(reader["SKILLLEVEL"]);
                        if (reader["S_SEX"] != DBNull.Value) _ExamSignUpOB.S_SEX = Convert.ToString(reader["S_SEX"]);
                        if (reader["S_CULTURALLEVEL"] != DBNull.Value) _ExamSignUpOB.S_CULTURALLEVEL = Convert.ToString(reader["S_CULTURALLEVEL"]);
                        if (reader["S_PHONE"] != DBNull.Value) _ExamSignUpOB.S_PHONE = Convert.ToString(reader["S_PHONE"]);
                        if (reader["S_BIRTHDAY"] != DBNull.Value) _ExamSignUpOB.S_BIRTHDAY = Convert.ToDateTime(reader["S_BIRTHDAY"]);
                        if (reader["S_TRAINUNITNAME"] != DBNull.Value) _ExamSignUpOB.S_TRAINUNITNAME = Convert.ToString(reader["S_TRAINUNITNAME"]);
                        if (reader["ISCONDITIONS"] != DBNull.Value) _ExamSignUpOB.IsConditions = Convert.ToString(reader["ISCONDITIONS"]);
                        if (reader["FIRSTTRIALTIME"] != DBNull.Value) _ExamSignUpOB.FIRSTTRIALTIME = Convert.ToDateTime(reader["FIRSTTRIALTIME"]);
                        if (reader["SheBaoCheck"] != DBNull.Value) _ExamSignUpOB.SheBaoCheck = Convert.ToByte(reader["SheBaoCheck"]);
                        if (reader["SignUpMan"] != DBNull.Value) _ExamSignUpOB.SignUpMan = Convert.ToString(reader["SignUpMan"]);
                        if (reader["Promise"] != DBNull.Value) _ExamSignUpOB.Promise = Convert.ToInt32(reader["Promise"]);
                        if (reader["SignUpPlaceID"] != DBNull.Value) _ExamSignUpOB.SignUpPlaceID = Convert.ToInt64(reader["SignUpPlaceID"]);
                        if (reader["PlaceName"] != DBNull.Value) _ExamSignUpOB.PlaceName = Convert.ToString(reader["PlaceName"]);
                        if (reader["CheckDatePlan"] != DBNull.Value) _ExamSignUpOB.CheckDatePlan = Convert.ToDateTime(reader["CheckDatePlan"]);
                        if (reader["FirstCheckType"] != DBNull.Value) _ExamSignUpOB.FirstCheckType = Convert.ToInt32(reader["FirstCheckType"]);
                        if (reader["LockTime"] != DBNull.Value) _ExamSignUpOB.LockTime = Convert.ToDateTime(reader["LockTime"]);
                        if (reader["LockEndTime"] != DBNull.Value) _ExamSignUpOB.LockEndTime = Convert.ToDateTime(reader["LockEndTime"]);
                        if (reader["LockReason"] != DBNull.Value) _ExamSignUpOB.LockReason = Convert.ToString(reader["LockReason"]);
                        if (reader["LockMan"] != DBNull.Value) _ExamSignUpOB.LockMan = Convert.ToString(reader["LockMan"]);
                        if (reader["AcceptResult"] != DBNull.Value) _ExamSignUpOB.AcceptResult = Convert.ToString(reader["AcceptResult"]);
                        if (reader["AcceptMan"] != DBNull.Value) _ExamSignUpOB.AcceptMan = Convert.ToString(reader["AcceptMan"]);
                        if (reader["AcceptTime"] != DBNull.Value) _ExamSignUpOB.AcceptTime = Convert.ToDateTime(reader["AcceptTime"]);
                        if (reader["ENT_ContractType"] != DBNull.Value) _ExamSignUpOB.ENT_ContractType = Convert.ToInt32(reader["ENT_ContractType"]);
                        if (reader["ENT_ContractStartTime"] != DBNull.Value) _ExamSignUpOB.ENT_ContractStartTime = Convert.ToDateTime(reader["ENT_ContractStartTime"]);
                        if (reader["ENT_ContractENDTime"] != DBNull.Value) _ExamSignUpOB.ENT_ContractENDTime = Convert.ToDateTime(reader["ENT_ContractENDTime"]);
                        if (reader["SignupPromise"] != DBNull.Value) _ExamSignUpOB.SignupPromise = Convert.ToInt32(reader["SignupPromise"]);
                        if (reader["ZACheckTime"] != DBNull.Value) _ExamSignUpOB.ZACheckTime = Convert.ToDateTime(reader["ZACheckTime"]);
                        if (reader["ZACheckResult"] != DBNull.Value) _ExamSignUpOB.ZACheckResult = Convert.ToInt32(reader["ZACheckResult"]);
                        if (reader["ZACheckRemark"] != DBNull.Value) _ExamSignUpOB.ZACheckRemark = Convert.ToString(reader["ZACheckRemark"]);
                        if (reader["Job"] != DBNull.Value) _ExamSignUpOB.Job = Convert.ToString(reader["Job"]);
                        if (reader["SafeTrainType"] != DBNull.Value) _ExamSignUpOB.SafeTrainType = Convert.ToString(reader["SafeTrainType"]);
                        if (reader["SafeTrainUnit"] != DBNull.Value) _ExamSignUpOB.SafeTrainUnit = Convert.ToString(reader["SafeTrainUnit"]);
                        if (reader["SafeTrainUnitCode"] != DBNull.Value) _ExamSignUpOB.SafeTrainUnitCode = Convert.ToString(reader["SafeTrainUnitCode"]);
                        if (reader["SafeTrainUnitValidEndDate"] != DBNull.Value) _ExamSignUpOB.SafeTrainUnitValidEndDate = Convert.ToDateTime(reader["SafeTrainUnitValidEndDate"]);
                        if (reader["SafeTrainUnitOfDept"] != DBNull.Value) _ExamSignUpOB.SafeTrainUnitOfDept = Convert.ToString(reader["SafeTrainUnitOfDept"]);
                    }
                    reader.Close();
                    db.Close();
                    return _ExamSignUpOB;
                }
            }
            catch (Exception ex)
            {
                db.Close();
                throw ex;
            }
        }

        #region 自定义方法

        /// <summary>
        /// 根据ExamPlanID,CreefePersonid,status删除信息
        /// </summary>
        /// <param name="ExamSignUpID">主键</param>
        /// <returns></returns>
        public static int DeleteByECS(DbTransaction tran, long ExamPlanID, long CreatePersonID, string Status)
        {
            string sql = @"DELETE FROM dbo.ExamSignUp WHERE ExamPlanID = @ExamPlanID and CreatePersonID = @CreatePersonID and Status=@Status";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, ExamPlanID));
            p.Add(db.CreateParameter("CreatePersonID", DbType.Int64, CreatePersonID));
            p.Add(db.CreateParameter("Status", DbType.String, Status));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }


        /// <summary>
        /// 查询人员是否已经报名
        /// </summary>
        /// <param name="WorkerCertificateCode">证件号码</param>
        /// <param name="ExamPlanID">考试计划ID</param>
        public static ExamSignUpOB GetObject(string WorkerCertificateCode, long ExamPlanID)
        {
            string sql = @"
			SELECT ExamSignUpID,SignUpCode,SignUpDate,WorkerID,UnitID,TrainUnitID,ExamPlanID,WorkStartDate,WorkYearNumer,PersonDetail,HireUnitAdvise,AdminUnitAdvise,CheckCode,CheckResult,CheckMan,CheckDate,
                    PayNoticeCode,PayNoticeResult,PayNoticeMan,PayNoticeDate,PayMoney,PayConfirmCode,PayConfirmRult,PayConfirmMan,PayConfirmDate,FacePhoto,Status,WorkerName,CertificateType,CertificateCode,
                    UnitName,UnitCode,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,SKILLLEVEL,S_SEX,S_CULTURALLEVEL,S_PHONE,S_BIRTHDAY,S_TRAINUNITNAME,ISCONDITIONS,FIRSTTRIALTIME,SheBaoCheck,
                    SignUpMan,PROMISE,SIGNUPPLACEID,CHECKDATEPLAN,PLACENAME,FirstCheckType,[LockTime],[LockEndTime],[LockReason],[LockMan],AcceptMan,AcceptTime,AcceptResult,ENT_ContractType,ENT_ContractStartTime,ENT_ContractENDTime,SignupPromise
                    ,ZACheckTime,ZACheckResult,ZACheckRemark,Job,SafeTrainType,SafeTrainUnit,SafeTrainUnitCode,SafeTrainUnitValidEndDate,SafeTrainUnitOfDept
			FROM dbo.ExamSignUp
			WHERE CertificateCode = @CertificateCode and ExamPlanID = @ExamPlanID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("CertificateCode", DbType.String, WorkerCertificateCode));
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, ExamPlanID));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    ExamSignUpOB _ExamSignUpOB = null;
                    if (reader.Read())
                    {
                        _ExamSignUpOB = new ExamSignUpOB();
                        if (reader["ExamSignUpID"] != DBNull.Value) _ExamSignUpOB.ExamSignUpID = Convert.ToInt64(reader["ExamSignUpID"]);
                        if (reader["SignUpCode"] != DBNull.Value) _ExamSignUpOB.SignUpCode = Convert.ToString(reader["SignUpCode"]);
                        if (reader["SignUpDate"] != DBNull.Value) _ExamSignUpOB.SignUpDate = Convert.ToDateTime(reader["SignUpDate"]);
                        if (reader["WorkerID"] != DBNull.Value) _ExamSignUpOB.WorkerID = Convert.ToInt64(reader["WorkerID"]);
                        if (reader["UnitID"] != DBNull.Value) _ExamSignUpOB.UnitID = Convert.ToInt64(reader["UnitID"]);
                        if (reader["TrainUnitID"] != DBNull.Value) _ExamSignUpOB.TrainUnitID = Convert.ToInt64(reader["TrainUnitID"]);
                        if (reader["ExamPlanID"] != DBNull.Value) _ExamSignUpOB.ExamPlanID = Convert.ToInt64(reader["ExamPlanID"]);
                        if (reader["WorkStartDate"] != DBNull.Value) _ExamSignUpOB.WorkStartDate = Convert.ToDateTime(reader["WorkStartDate"]);
                        if (reader["WorkYearNumer"] != DBNull.Value) _ExamSignUpOB.WorkYearNumer = Convert.ToInt32(reader["WorkYearNumer"]);
                        if (reader["PersonDetail"] != DBNull.Value) _ExamSignUpOB.PersonDetail = Convert.ToString(reader["PersonDetail"]);
                        if (reader["HireUnitAdvise"] != DBNull.Value) _ExamSignUpOB.HireUnitAdvise = Convert.ToString(reader["HireUnitAdvise"]);
                        if (reader["AdminUnitAdvise"] != DBNull.Value) _ExamSignUpOB.AdminUnitAdvise = Convert.ToString(reader["AdminUnitAdvise"]);
                        if (reader["CheckCode"] != DBNull.Value) _ExamSignUpOB.CheckCode = Convert.ToString(reader["CheckCode"]);
                        if (reader["CheckResult"] != DBNull.Value) _ExamSignUpOB.CheckResult = Convert.ToString(reader["CheckResult"]);
                        if (reader["CheckMan"] != DBNull.Value) _ExamSignUpOB.CheckMan = Convert.ToString(reader["CheckMan"]);
                        if (reader["CheckDate"] != DBNull.Value) _ExamSignUpOB.CheckDate = Convert.ToDateTime(reader["CheckDate"]);
                        if (reader["PayNoticeCode"] != DBNull.Value) _ExamSignUpOB.PayNoticeCode = Convert.ToString(reader["PayNoticeCode"]);
                        if (reader["PayNoticeResult"] != DBNull.Value) _ExamSignUpOB.PayNoticeResult = Convert.ToString(reader["PayNoticeResult"]);
                        if (reader["PayNoticeMan"] != DBNull.Value) _ExamSignUpOB.PayNoticeMan = Convert.ToString(reader["PayNoticeMan"]);
                        if (reader["PayNoticeDate"] != DBNull.Value) _ExamSignUpOB.PayNoticeDate = Convert.ToDateTime(reader["PayNoticeDate"]);
                        if (reader["PayMoney"] != DBNull.Value) _ExamSignUpOB.PayMoney = Convert.ToDecimal(reader["PayMoney"]);
                        if (reader["PayConfirmCode"] != DBNull.Value) _ExamSignUpOB.PayConfirmCode = Convert.ToString(reader["PayConfirmCode"]);
                        if (reader["PayConfirmRult"] != DBNull.Value) _ExamSignUpOB.PayConfirmRult = Convert.ToString(reader["PayConfirmRult"]);
                        if (reader["PayConfirmMan"] != DBNull.Value) _ExamSignUpOB.PayConfirmMan = Convert.ToString(reader["PayConfirmMan"]);
                        if (reader["PayConfirmDate"] != DBNull.Value) _ExamSignUpOB.PayConfirmDate = Convert.ToDateTime(reader["PayConfirmDate"]);
                        if (reader["FacePhoto"] != DBNull.Value) _ExamSignUpOB.FacePhoto = Convert.ToString(reader["FacePhoto"]);
                        if (reader["Status"] != DBNull.Value) _ExamSignUpOB.Status = Convert.ToString(reader["Status"]);
                        if (reader["WorkerName"] != DBNull.Value) _ExamSignUpOB.WorkerName = Convert.ToString(reader["WorkerName"]);
                        if (reader["CertificateType"] != DBNull.Value) _ExamSignUpOB.CertificateType = Convert.ToString(reader["CertificateType"]);
                        if (reader["CertificateCode"] != DBNull.Value) _ExamSignUpOB.CertificateCode = Convert.ToString(reader["CertificateCode"]);
                        if (reader["UnitName"] != DBNull.Value) _ExamSignUpOB.UnitName = Convert.ToString(reader["UnitName"]);
                        if (reader["UnitCode"] != DBNull.Value) _ExamSignUpOB.UnitCode = Convert.ToString(reader["UnitCode"]);
                        if (reader["CreatePersonID"] != DBNull.Value) _ExamSignUpOB.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                        if (reader["CreateTime"] != DBNull.Value) _ExamSignUpOB.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                        if (reader["ModifyPersonID"] != DBNull.Value) _ExamSignUpOB.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                        if (reader["ModifyTime"] != DBNull.Value) _ExamSignUpOB.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                        if (reader["SKILLLEVEL"] != DBNull.Value) _ExamSignUpOB.SKILLLEVEL = Convert.ToString(reader["SKILLLEVEL"]);
                        if (reader["S_SEX"] != DBNull.Value) _ExamSignUpOB.S_SEX = Convert.ToString(reader["S_SEX"]);
                        if (reader["S_CULTURALLEVEL"] != DBNull.Value) _ExamSignUpOB.S_CULTURALLEVEL = Convert.ToString(reader["S_CULTURALLEVEL"]);
                        if (reader["S_PHONE"] != DBNull.Value) _ExamSignUpOB.S_PHONE = Convert.ToString(reader["S_PHONE"]);
                        if (reader["S_BIRTHDAY"] != DBNull.Value) _ExamSignUpOB.S_BIRTHDAY = Convert.ToDateTime(reader["S_BIRTHDAY"]);
                        if (reader["S_TRAINUNITNAME"] != DBNull.Value) _ExamSignUpOB.S_TRAINUNITNAME = Convert.ToString(reader["S_TRAINUNITNAME"]);
                        if (reader["ISCONDITIONS"] != DBNull.Value) _ExamSignUpOB.IsConditions = Convert.ToString(reader["ISCONDITIONS"]);
                        if (reader["FIRSTTRIALTIME"] != DBNull.Value) _ExamSignUpOB.FIRSTTRIALTIME = Convert.ToDateTime(reader["FIRSTTRIALTIME"]);
                        if (reader["SheBaoCheck"] != DBNull.Value) _ExamSignUpOB.SheBaoCheck = Convert.ToByte(reader["SheBaoCheck"]);
                        if (reader["SignUpMan"] != DBNull.Value) _ExamSignUpOB.SignUpMan = Convert.ToString(reader["SignUpMan"]);
                        if (reader["Promise"] != DBNull.Value) _ExamSignUpOB.Promise = Convert.ToInt32(reader["Promise"]);
                        if (reader["SignUpPlaceID"] != DBNull.Value) _ExamSignUpOB.SignUpPlaceID = Convert.ToInt64(reader["SignUpPlaceID"]);
                        if (reader["PlaceName"] != DBNull.Value) _ExamSignUpOB.PlaceName = Convert.ToString(reader["PlaceName"]);
                        if (reader["CheckDatePlan"] != DBNull.Value) _ExamSignUpOB.CheckDatePlan = Convert.ToDateTime(reader["CheckDatePlan"]);
                        if (reader["FirstCheckType"] != DBNull.Value) _ExamSignUpOB.FirstCheckType = Convert.ToInt32(reader["FirstCheckType"]);
                        if (reader["LockTime"] != DBNull.Value) _ExamSignUpOB.LockTime = Convert.ToDateTime(reader["LockTime"]);
                        if (reader["LockEndTime"] != DBNull.Value) _ExamSignUpOB.LockEndTime = Convert.ToDateTime(reader["LockEndTime"]);
                        if (reader["LockReason"] != DBNull.Value) _ExamSignUpOB.LockReason = Convert.ToString(reader["LockReason"]);
                        if (reader["LockMan"] != DBNull.Value) _ExamSignUpOB.LockMan = Convert.ToString(reader["LockMan"]);
                        if (reader["AcceptResult"] != DBNull.Value) _ExamSignUpOB.AcceptResult = Convert.ToString(reader["AcceptResult"]);
                        if (reader["AcceptMan"] != DBNull.Value) _ExamSignUpOB.AcceptMan = Convert.ToString(reader["AcceptMan"]);
                        if (reader["AcceptTime"] != DBNull.Value) _ExamSignUpOB.AcceptTime = Convert.ToDateTime(reader["AcceptTime"]);
                        if (reader["ENT_ContractType"] != DBNull.Value) _ExamSignUpOB.ENT_ContractType = Convert.ToInt32(reader["ENT_ContractType"]);
                        if (reader["ENT_ContractStartTime"] != DBNull.Value) _ExamSignUpOB.ENT_ContractStartTime = Convert.ToDateTime(reader["ENT_ContractStartTime"]);
                        if (reader["ENT_ContractENDTime"] != DBNull.Value) _ExamSignUpOB.ENT_ContractENDTime = Convert.ToDateTime(reader["ENT_ContractENDTime"]);
                        if (reader["SignupPromise"] != DBNull.Value) _ExamSignUpOB.SignupPromise = Convert.ToInt32(reader["SignupPromise"]);
                        if (reader["ZACheckTime"] != DBNull.Value) _ExamSignUpOB.ZACheckTime = Convert.ToDateTime(reader["ZACheckTime"]);
                        if (reader["ZACheckResult"] != DBNull.Value) _ExamSignUpOB.ZACheckResult = Convert.ToInt32(reader["ZACheckResult"]);
                        if (reader["ZACheckRemark"] != DBNull.Value) _ExamSignUpOB.ZACheckRemark = Convert.ToString(reader["ZACheckRemark"]);
                        if (reader["Job"] != DBNull.Value) _ExamSignUpOB.Job = Convert.ToString(reader["Job"]);
                        if (reader["SafeTrainType"] != DBNull.Value) _ExamSignUpOB.SafeTrainType = Convert.ToString(reader["SafeTrainType"]);
                        if (reader["SafeTrainUnit"] != DBNull.Value) _ExamSignUpOB.SafeTrainUnit = Convert.ToString(reader["SafeTrainUnit"]);
                        if (reader["SafeTrainUnitCode"] != DBNull.Value) _ExamSignUpOB.SafeTrainUnitCode = Convert.ToString(reader["SafeTrainUnitCode"]);
                        if (reader["SafeTrainUnitValidEndDate"] != DBNull.Value) _ExamSignUpOB.SafeTrainUnitValidEndDate = Convert.ToDateTime(reader["SafeTrainUnitValidEndDate"]);
                        if (reader["SafeTrainUnitOfDept"] != DBNull.Value) _ExamSignUpOB.SafeTrainUnitOfDept = Convert.ToString(reader["SafeTrainUnitOfDept"]);
                    }
                    reader.Close();
                    db.Close();
                    return _ExamSignUpOB;
                }
            }
            catch (Exception ex)
            {
                db.Close();
                throw ex;
            }
        }

        /// <summary>
        /// 获取个人最近一次报名信息
        /// </summary>
        /// <param name="WorkerCertificateCode">主键证件号码param>
        public static ExamSignUpOB GetLastExamSingup(string WorkerCertificateCode)
        {
            string sql = @"
			SELECT top 1 ExamSignUpID,SignUpCode,SignUpDate,WorkerID,UnitID,TrainUnitID,ExamPlanID,WorkStartDate,WorkYearNumer,PersonDetail,
                HireUnitAdvise,AdminUnitAdvise,CheckCode,CheckResult,CheckMan,CheckDate,PayNoticeCode,PayNoticeResult,PayNoticeMan,PayNoticeDate,
                PayMoney,PayConfirmCode,PayConfirmRult,PayConfirmMan,PayConfirmDate,FacePhoto,Status,WorkerName,CertificateType,CertificateCode,
                UnitName,UnitCode,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,SKILLLEVEL,S_SEX,S_CULTURALLEVEL,
                S_PHONE,S_BIRTHDAY,S_TRAINUNITNAME,ISCONDITIONS,FIRSTTRIALTIME,SheBaoCheck,SignUpMan,PROMISE,SIGNUPPLACEID,CHECKDATEPLAN,PLACENAME,
                FirstCheckType,[LockTime],[LockEndTime],[LockReason],[LockMan],AcceptMan,AcceptTime,AcceptResult,
                ENT_ContractType,ENT_ContractStartTime,ENT_ContractENDTime,SignupPromise,ZACheckTime,ZACheckResult,ZACheckRemark,Job,SafeTrainType,SafeTrainUnit,SafeTrainUnitCode,SafeTrainUnitValidEndDate,SafeTrainUnitOfDept
			FROM dbo.ExamSignUp
			WHERE CertificateCode = @CertificateCode 
            order by CreateTime desc";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("CertificateCode", DbType.String, WorkerCertificateCode));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    ExamSignUpOB _ExamSignUpOB = null;
                    if (reader.Read())
                    {
                        _ExamSignUpOB = new ExamSignUpOB();
                        if (reader["ExamSignUpID"] != DBNull.Value) _ExamSignUpOB.ExamSignUpID = Convert.ToInt64(reader["ExamSignUpID"]);
                        if (reader["SignUpCode"] != DBNull.Value) _ExamSignUpOB.SignUpCode = Convert.ToString(reader["SignUpCode"]);
                        if (reader["SignUpDate"] != DBNull.Value) _ExamSignUpOB.SignUpDate = Convert.ToDateTime(reader["SignUpDate"]);
                        if (reader["WorkerID"] != DBNull.Value) _ExamSignUpOB.WorkerID = Convert.ToInt64(reader["WorkerID"]);
                        if (reader["UnitID"] != DBNull.Value) _ExamSignUpOB.UnitID = Convert.ToInt64(reader["UnitID"]);
                        if (reader["TrainUnitID"] != DBNull.Value) _ExamSignUpOB.TrainUnitID = Convert.ToInt64(reader["TrainUnitID"]);
                        if (reader["ExamPlanID"] != DBNull.Value) _ExamSignUpOB.ExamPlanID = Convert.ToInt64(reader["ExamPlanID"]);
                        if (reader["WorkStartDate"] != DBNull.Value) _ExamSignUpOB.WorkStartDate = Convert.ToDateTime(reader["WorkStartDate"]);
                        if (reader["WorkYearNumer"] != DBNull.Value) _ExamSignUpOB.WorkYearNumer = Convert.ToInt32(reader["WorkYearNumer"]);
                        if (reader["PersonDetail"] != DBNull.Value) _ExamSignUpOB.PersonDetail = Convert.ToString(reader["PersonDetail"]);
                        if (reader["HireUnitAdvise"] != DBNull.Value) _ExamSignUpOB.HireUnitAdvise = Convert.ToString(reader["HireUnitAdvise"]);
                        if (reader["AdminUnitAdvise"] != DBNull.Value) _ExamSignUpOB.AdminUnitAdvise = Convert.ToString(reader["AdminUnitAdvise"]);
                        if (reader["CheckCode"] != DBNull.Value) _ExamSignUpOB.CheckCode = Convert.ToString(reader["CheckCode"]);
                        if (reader["CheckResult"] != DBNull.Value) _ExamSignUpOB.CheckResult = Convert.ToString(reader["CheckResult"]);
                        if (reader["CheckMan"] != DBNull.Value) _ExamSignUpOB.CheckMan = Convert.ToString(reader["CheckMan"]);
                        if (reader["CheckDate"] != DBNull.Value) _ExamSignUpOB.CheckDate = Convert.ToDateTime(reader["CheckDate"]);
                        if (reader["PayNoticeCode"] != DBNull.Value) _ExamSignUpOB.PayNoticeCode = Convert.ToString(reader["PayNoticeCode"]);
                        if (reader["PayNoticeResult"] != DBNull.Value) _ExamSignUpOB.PayNoticeResult = Convert.ToString(reader["PayNoticeResult"]);
                        if (reader["PayNoticeMan"] != DBNull.Value) _ExamSignUpOB.PayNoticeMan = Convert.ToString(reader["PayNoticeMan"]);
                        if (reader["PayNoticeDate"] != DBNull.Value) _ExamSignUpOB.PayNoticeDate = Convert.ToDateTime(reader["PayNoticeDate"]);
                        if (reader["PayMoney"] != DBNull.Value) _ExamSignUpOB.PayMoney = Convert.ToDecimal(reader["PayMoney"]);
                        if (reader["PayConfirmCode"] != DBNull.Value) _ExamSignUpOB.PayConfirmCode = Convert.ToString(reader["PayConfirmCode"]);
                        if (reader["PayConfirmRult"] != DBNull.Value) _ExamSignUpOB.PayConfirmRult = Convert.ToString(reader["PayConfirmRult"]);
                        if (reader["PayConfirmMan"] != DBNull.Value) _ExamSignUpOB.PayConfirmMan = Convert.ToString(reader["PayConfirmMan"]);
                        if (reader["PayConfirmDate"] != DBNull.Value) _ExamSignUpOB.PayConfirmDate = Convert.ToDateTime(reader["PayConfirmDate"]);
                        if (reader["FacePhoto"] != DBNull.Value) _ExamSignUpOB.FacePhoto = Convert.ToString(reader["FacePhoto"]);
                        if (reader["Status"] != DBNull.Value) _ExamSignUpOB.Status = Convert.ToString(reader["Status"]);
                        if (reader["WorkerName"] != DBNull.Value) _ExamSignUpOB.WorkerName = Convert.ToString(reader["WorkerName"]);
                        if (reader["CertificateType"] != DBNull.Value) _ExamSignUpOB.CertificateType = Convert.ToString(reader["CertificateType"]);
                        if (reader["CertificateCode"] != DBNull.Value) _ExamSignUpOB.CertificateCode = Convert.ToString(reader["CertificateCode"]);
                        if (reader["UnitName"] != DBNull.Value) _ExamSignUpOB.UnitName = Convert.ToString(reader["UnitName"]);
                        if (reader["UnitCode"] != DBNull.Value) _ExamSignUpOB.UnitCode = Convert.ToString(reader["UnitCode"]);
                        if (reader["CreatePersonID"] != DBNull.Value) _ExamSignUpOB.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                        if (reader["CreateTime"] != DBNull.Value) _ExamSignUpOB.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                        if (reader["ModifyPersonID"] != DBNull.Value) _ExamSignUpOB.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                        if (reader["ModifyTime"] != DBNull.Value) _ExamSignUpOB.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                        if (reader["SKILLLEVEL"] != DBNull.Value) _ExamSignUpOB.SKILLLEVEL = Convert.ToString(reader["SKILLLEVEL"]);
                        if (reader["S_SEX"] != DBNull.Value) _ExamSignUpOB.S_SEX = Convert.ToString(reader["S_SEX"]);
                        if (reader["S_CULTURALLEVEL"] != DBNull.Value) _ExamSignUpOB.S_CULTURALLEVEL = Convert.ToString(reader["S_CULTURALLEVEL"]);
                        if (reader["S_PHONE"] != DBNull.Value) _ExamSignUpOB.S_PHONE = Convert.ToString(reader["S_PHONE"]);
                        if (reader["S_BIRTHDAY"] != DBNull.Value) _ExamSignUpOB.S_BIRTHDAY = Convert.ToDateTime(reader["S_BIRTHDAY"]);
                        if (reader["S_TRAINUNITNAME"] != DBNull.Value) _ExamSignUpOB.S_TRAINUNITNAME = Convert.ToString(reader["S_TRAINUNITNAME"]);
                        if (reader["ISCONDITIONS"] != DBNull.Value) _ExamSignUpOB.IsConditions = Convert.ToString(reader["ISCONDITIONS"]);
                        if (reader["FIRSTTRIALTIME"] != DBNull.Value) _ExamSignUpOB.FIRSTTRIALTIME = Convert.ToDateTime(reader["FIRSTTRIALTIME"]);
                        if (reader["SheBaoCheck"] != DBNull.Value) _ExamSignUpOB.SheBaoCheck = Convert.ToByte(reader["SheBaoCheck"]);
                        if (reader["SignUpMan"] != DBNull.Value) _ExamSignUpOB.SignUpMan = Convert.ToString(reader["SignUpMan"]);
                        if (reader["Promise"] != DBNull.Value) _ExamSignUpOB.Promise = Convert.ToInt32(reader["Promise"]);
                        if (reader["SignUpPlaceID"] != DBNull.Value) _ExamSignUpOB.SignUpPlaceID = Convert.ToInt64(reader["SignUpPlaceID"]);
                        if (reader["PlaceName"] != DBNull.Value) _ExamSignUpOB.PlaceName = Convert.ToString(reader["PlaceName"]);
                        if (reader["CheckDatePlan"] != DBNull.Value) _ExamSignUpOB.CheckDatePlan = Convert.ToDateTime(reader["CheckDatePlan"]);
                        if (reader["FirstCheckType"] != DBNull.Value) _ExamSignUpOB.FirstCheckType = Convert.ToInt32(reader["FirstCheckType"]);
                        if (reader["LockTime"] != DBNull.Value) _ExamSignUpOB.LockTime = Convert.ToDateTime(reader["LockTime"]);
                        if (reader["LockEndTime"] != DBNull.Value) _ExamSignUpOB.LockEndTime = Convert.ToDateTime(reader["LockEndTime"]);
                        if (reader["LockReason"] != DBNull.Value) _ExamSignUpOB.LockReason = Convert.ToString(reader["LockReason"]);
                        if (reader["LockMan"] != DBNull.Value) _ExamSignUpOB.LockMan = Convert.ToString(reader["LockMan"]);
                        if (reader["AcceptResult"] != DBNull.Value) _ExamSignUpOB.AcceptResult = Convert.ToString(reader["AcceptResult"]);
                        if (reader["AcceptMan"] != DBNull.Value) _ExamSignUpOB.AcceptMan = Convert.ToString(reader["AcceptMan"]);
                        if (reader["AcceptTime"] != DBNull.Value) _ExamSignUpOB.AcceptTime = Convert.ToDateTime(reader["AcceptTime"]);
                        if (reader["ENT_ContractType"] != DBNull.Value) _ExamSignUpOB.ENT_ContractType = Convert.ToInt32(reader["ENT_ContractType"]);
                        if (reader["ENT_ContractStartTime"] != DBNull.Value) _ExamSignUpOB.ENT_ContractStartTime = Convert.ToDateTime(reader["ENT_ContractStartTime"]);
                        if (reader["ENT_ContractENDTime"] != DBNull.Value) _ExamSignUpOB.ENT_ContractENDTime = Convert.ToDateTime(reader["ENT_ContractENDTime"]);
                        if (reader["SignupPromise"] != DBNull.Value) _ExamSignUpOB.SignupPromise = Convert.ToInt32(reader["SignupPromise"]);
                        if (reader["ZACheckTime"] != DBNull.Value) _ExamSignUpOB.ZACheckTime = Convert.ToDateTime(reader["ZACheckTime"]);
                        if (reader["ZACheckResult"] != DBNull.Value) _ExamSignUpOB.ZACheckResult = Convert.ToInt32(reader["ZACheckResult"]);
                        if (reader["ZACheckRemark"] != DBNull.Value) _ExamSignUpOB.ZACheckRemark = Convert.ToString(reader["ZACheckRemark"]);
                        if (reader["Job"] != DBNull.Value) _ExamSignUpOB.Job = Convert.ToString(reader["Job"]);
                        if (reader["SafeTrainType"] != DBNull.Value) _ExamSignUpOB.SafeTrainType = Convert.ToString(reader["SafeTrainType"]);
                        if (reader["SafeTrainUnit"] != DBNull.Value) _ExamSignUpOB.SafeTrainUnit = Convert.ToString(reader["SafeTrainUnit"]);
                        if (reader["SafeTrainUnitCode"] != DBNull.Value) _ExamSignUpOB.SafeTrainUnitCode = Convert.ToString(reader["SafeTrainUnitCode"]);
                        if (reader["SafeTrainUnitValidEndDate"] != DBNull.Value) _ExamSignUpOB.SafeTrainUnitValidEndDate = Convert.ToDateTime(reader["SafeTrainUnitValidEndDate"]);
                        if (reader["SafeTrainUnitOfDept"] != DBNull.Value) _ExamSignUpOB.SafeTrainUnitOfDept = Convert.ToString(reader["SafeTrainUnitOfDept"]);
                    }
                    reader.Close();
                    db.Close();
                    return _ExamSignUpOB;
                }
            }
            catch (Exception ex)
            {
                db.Close();
                throw ex;
            }
        }

        /// <summary>
        /// 查询是否有人已经报名
        /// </summary>
        /// <param name="ExamPlanID">考试计划ID</param>
        public static DataTable GetObjectByExamPlanID(long ExamPlanID)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM dbo.ExamSignUp WHERE 1=1");
            sb.Append(" AND ExamPlanID = @ExamPlanID");
            try
            {
                DBHelper db = new DBHelper();
                List<SqlParameter> p = new List<SqlParameter>();
                p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, ExamPlanID));
                return db.GetFillData(sb.ToString(), p.ToArray());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }

        /// <summary>
        /// 查询是否有人已经报名
        /// </summary>
        /// <param name="ExamPlanID">考试计划ID</param>
        /// <param name="ExamPlanID">登陆者ID</param>
        /// /// <param name="ExamPlanID">状态为待审核</param>
        public static DataTable GetObjectByExamPlanID(long ExamPlanID, long CreatePersonID, string Status)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM dbo.ExamSignUp WHERE 1=1");
            sb.Append(" AND ExamPlanID = @ExamPlanID");
            sb.Append(" AND CreatePersonID = @CreatePersonID");
            sb.Append(" AND Status = @Status");
            try
            {
                DBHelper db = new DBHelper();
                List<SqlParameter> p = new List<SqlParameter>();
                p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, ExamPlanID));
                p.Add(db.CreateParameter("CreatePersonID", DbType.Int64, CreatePersonID));
                p.Add(db.CreateParameter("Status", DbType.String, Status));
                return db.GetFillData(sb.ToString(), p.ToArray());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }

        ///// <summary>
        ///// 获取实体集合
        ///// </summary>
        ///// <param name="startRowIndex">开始行索引</param>
        ///// <param name="maximumRows">每页最大行</param>
        ///// <param name="filterWhereString">查询条件</param>
        ///// <param name="orderBy">排序规则</param>
        ///// <returns>DataTable</returns>
        ///// <summary>
        //public static DataTable GetList(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        //{
        //    return CommonDAL.GetDataTable(startRowIndex, maximumRows, "DBO.VIEW_EXAMSIGNUP", "*", filterWhereString, orderBy == "" ? " ExamSignUpID" : orderBy);
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
        public static DataTable GetList_New(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "DBO.VIEW_EXAMSIGNUP_NEW", "*", filterWhereString, orderBy == "" ? " ExamSignUpID" : orderBy);
        }


        ///// <summary>
        ///// 统计查询结果记录数
        ///// </summary>
        ///// <param name="filterWhereString">查询条件</param>
        ///// <returns>记录总行数</returns>
        //public static int SelectCount(string filterWhereString)
        //{
        //    return CommonDAL.SelectRowCount("DBO.VIEW_EXAMSIGNUP", filterWhereString);
        //}
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount_New(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("DBO.VIEW_EXAMSIGNUP_NEW", filterWhereString);
        }

        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount_New(DbTransaction tran, string filterWhereString)
        {
            return CommonDAL.SelectRowCount(tran, "DBO.VIEW_EXAMSIGNUP_NEW", filterWhereString);
        }

        /// <summary>
        /// 统计考试计划当前报名人数
        /// </summary>
        /// <param name="ExamPlanID">考试计划ID</param>
        /// <returns>当前已经报名人数</returns>
        public static int SelectSignupCount(long ExamPlanID)
        {
            //使用废弃字段CheckDatePlan（原意：计划初审日期，现在用于记录个人确认报名时间（预报名后抢报名名额））
            //return CommonDAL.SelectRowCount("DBO.EXAMSIGNUP", string.Format(" and ExamPlanID={0} and CheckDatePlan >'2021-01-01'", ExamPlanID));
            return CommonDAL.SelectRowCount("DBO.EXAMSIGNUP", string.Format(" and ExamPlanID={0} ", ExamPlanID));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <param name="filterWhereString"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static DataTable GetList_VIEW_EXAMSIGNUP_CE(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            //return CommonDAL.GetDataTable(startRowIndex, maximumRows, "DBO.VIEW_EXAMSIGNUP_CE", "*", filterWhereString, orderBy == "" ? " CREATETIME" : orderBy);
            return CommonDAL.GetDataTable(startRowIndex, maximumRows
                , @"DBO.EXAMSIGNUP s JOIN DBO.[USER] u ON s.CREATEPERSONID = u.USERID"
                , "s.CREATEPERSONID, s.CREATETIME, s.SIGNUPCODE, u.RELUSERNAME,s.EXAMSIGNUPID,s.[WORKERNAME],s.[UNITNAME],s.[SIGNUPDATE]"
                , filterWhereString
                , orderBy == "" ? " s.EXAMSIGNUPID desc" : orderBy);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterWhereString"></param>
        /// <returns></returns>
        public static int SelectCountVIEW_EXAMSIGNUP_CE(string filterWhereString)
        {
            return CommonDAL.SelectRowCount(@"DBO.EXAMSIGNUP s JOIN DBO.[USER] u ON s.CREATEPERSONID = u.USERID", filterWhereString);

        }

        /// <summary>
        /// 报名初审(单位审核)
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_ExamSignUpOB">要修改的值</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns></returns>
        public static int CheckFirst(ExamSignUpOB _ExamSignUpOB, string filterWhereString)
        {
            DBHelper db = new DBHelper();
            string sql = "UPDATE dbo.ExamSignUp SET [STATUS]=@Status,ModifyTime=@ModifyTime,FirstTrialTime=@ModifyTime,HireUnitAdvise=@HireUnitAdvise WHERE 2>1" + filterWhereString;
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("Status", DbType.String, _ExamSignUpOB.Status));
            p.Add(db.CreateParameter("ModifyTime", DbType.DateTime, _ExamSignUpOB.ModifyTime));
            p.Add(db.CreateParameter("HireUnitAdvise", DbType.String, _ExamSignUpOB.HireUnitAdvise));
            return db.ExcuteNonQuery(sql, p.ToArray());
        }

        /// <summary>
        /// 报名受理
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_ExamSignUpOB">要修改的值</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns></returns>
        public static int Accept(DbTransaction tran, ExamSignUpOB _ExamSignUpOB, string filterWhereString)
        {
            string sql = "UPDATE dbo.ExamSignUp SET AcceptResult =@AcceptResult,AcceptTime = @AcceptTime,AcceptMan = @AcceptMan,[STATUS]=@Status,ModifyTime=@ModifyTime WHERE 2>1" + filterWhereString;
            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("AcceptTime", DbType.DateTime, _ExamSignUpOB.AcceptTime));
            p.Add(db.CreateParameter("AcceptResult", DbType.String, _ExamSignUpOB.AcceptResult));
            p.Add(db.CreateParameter("AcceptMan", DbType.String, _ExamSignUpOB.AcceptMan));
            p.Add(db.CreateParameter("Status", DbType.String, _ExamSignUpOB.Status));
            p.Add(db.CreateParameter("ModifyTime", DbType.DateTime, _ExamSignUpOB.ModifyTime));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 报名审核
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_ExamSignUpOB">要修改的值</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns></returns>
        public static int Check(DbTransaction tran, ExamSignUpOB _ExamSignUpOB, string filterWhereString)
        {
            string sql = "UPDATE dbo.ExamSignUp SET CheckResult =@CheckResult,CheckDate = @CheckDate,CheckMan = @CheckMan,CheckCode = @CheckCode,[STATUS]=@Status,ModifyTime=@ModifyTime WHERE 2>1" + filterWhereString;
            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("CheckDate", DbType.DateTime, _ExamSignUpOB.CheckDate));
            p.Add(db.CreateParameter("CheckResult", DbType.String, _ExamSignUpOB.CheckResult));
            p.Add(db.CreateParameter("CheckMan", DbType.String, _ExamSignUpOB.CheckMan));
            p.Add(db.CreateParameter("CheckCode", DbType.String, _ExamSignUpOB.CheckCode));
            p.Add(db.CreateParameter("Status", DbType.String, _ExamSignUpOB.Status));
            p.Add(db.CreateParameter("ModifyTime", DbType.DateTime, _ExamSignUpOB.ModifyTime));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 报名通知缴费
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_ExamSignUpOB">要修改的值</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns></returns>
        public static int PayNotice(DbTransaction tran, ExamSignUpOB _ExamSignUpOB, string filterWhereString)
        {
            string sql = "UPDATE dbo.ExamSignUp SET PayNoticeResult =@PayNoticeResult,PayNoticeDate = @PayNoticeDate,PayNoticeMan = @PayNoticeMan,PayNoticeCode = @PayNoticeCode,[STATUS]=@Status,ModifyPersonID=@ModifyPersonID,ModifyTime=@ModifyTime WHERE 2>1" + filterWhereString;
            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PayNoticeDate", DbType.DateTime, _ExamSignUpOB.PayNoticeDate));
            p.Add(db.CreateParameter("PayNoticeResult", DbType.String, _ExamSignUpOB.PayNoticeResult));
            p.Add(db.CreateParameter("PayNoticeMan", DbType.String, _ExamSignUpOB.PayNoticeMan));
            p.Add(db.CreateParameter("PayNoticeCode", DbType.String, _ExamSignUpOB.PayNoticeCode));
            p.Add(db.CreateParameter("Status", DbType.String, _ExamSignUpOB.Status));
            p.Add(db.CreateParameter("ModifyPersonID", DbType.Int64, _ExamSignUpOB.ModifyPersonID));
            p.Add(db.CreateParameter("ModifyTime", DbType.DateTime, _ExamSignUpOB.ModifyTime));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());

        }
        /// <summary>
        /// 报名缴费确认
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_ExamSignUpOB">要修改的值</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns></returns>
        public static int PayConfirm(DbTransaction tran, ExamSignUpOB _ExamSignUpOB, string filterWhereString)
        {
            string sql = "UPDATE dbo.ExamSignUp SET PayConfirmRult =@PayConfirmRult,PayConfirmDate = @PayConfirmDate,PayConfirmMan = @PayConfirmMan,PayConfirmCode = @PayConfirmCode,[STATUS]=@Status,ModifyPersonID=@ModifyPersonID,ModifyTime=@ModifyTime WHERE 2>1" + filterWhereString;
            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PayConfirmDate", DbType.DateTime, _ExamSignUpOB.PayConfirmDate));
            p.Add(db.CreateParameter("PayConfirmRult", DbType.String, _ExamSignUpOB.PayConfirmRult));
            p.Add(db.CreateParameter("PayConfirmMan", DbType.String, _ExamSignUpOB.PayConfirmMan));
            p.Add(db.CreateParameter("PayConfirmCode", DbType.String, _ExamSignUpOB.PayConfirmCode));
            p.Add(db.CreateParameter("Status", DbType.String, _ExamSignUpOB.Status));
            p.Add(db.CreateParameter("ModifyPersonID", DbType.Int64, _ExamSignUpOB.ModifyPersonID));
            p.Add(db.CreateParameter("ModifyTime", DbType.DateTime, _ExamSignUpOB.ModifyTime));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());

        }

        /// <summary>
        /// 报名审核并缴费确认（新版职业技能工人）
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_ExamSignUpOB">要修改的值</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns></returns>
        public static int CheckAndPayConfirm(DbTransaction tran, ExamSignUpOB _ExamSignUpOB, string filterWhereString)
        {
            string sql = "UPDATE dbo.ExamSignUp SET CheckResult =@CheckResult,CheckDate = @CheckDate,CheckMan = @CheckMan,CheckCode = @CheckCode,PayConfirmRult =@PayConfirmRult,PayConfirmDate = @PayConfirmDate,PayConfirmMan = @PayConfirmMan,PayConfirmCode = @PayConfirmCode,[STATUS]=@Status,ModifyTime=@ModifyTime WHERE 2>1" + filterWhereString;
            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("CheckDate", DbType.DateTime, _ExamSignUpOB.CheckDate));
            p.Add(db.CreateParameter("CheckResult", DbType.String, _ExamSignUpOB.CheckResult));
            p.Add(db.CreateParameter("CheckMan", DbType.String, _ExamSignUpOB.CheckMan));
            p.Add(db.CreateParameter("CheckCode", DbType.String, _ExamSignUpOB.CheckCode));
            p.Add(db.CreateParameter("PayConfirmDate", DbType.DateTime, _ExamSignUpOB.PayConfirmDate));
            p.Add(db.CreateParameter("PayConfirmRult", DbType.String, _ExamSignUpOB.PayConfirmRult));
            p.Add(db.CreateParameter("PayConfirmMan", DbType.String, _ExamSignUpOB.PayConfirmMan));
            p.Add(db.CreateParameter("PayConfirmCode", DbType.String, _ExamSignUpOB.PayConfirmCode));
            p.Add(db.CreateParameter("Status", DbType.String, _ExamSignUpOB.Status));
            p.Add(db.CreateParameter("ModifyTime", DbType.DateTime, _ExamSignUpOB.ModifyTime));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 统计各培训点报名审核通过率和考试通过率
        /// </summary>
        /// <param name="startDate">查询时间起始</param>
        /// <param name="endDate">查询时间截止</param>
        /// <param name="PostTypeID">岗位类别ID</param>
        /// <param name="PostID">岗位工种ID</param>
        /// <returns></returns>
        public static DataTable StatTrainUnitExamSignup(DateTime? startDate, DateTime? endDate, string PostTypeID, string PostID)
        {
            string sql = @"SELECT row_number() over(order by 培训点) RowNum,t.* from 
(SELECT s.[TRAINUNITNAME] as 培训点,
count(s.[STATUS]) as 报名人数,
sum(case s.[STATUS] when '已缴费' then 1 else 0 end) as 审核通过人数,
round((cast(sum(case s.[STATUS] when '已缴费' then 1 else 0 end) as numeric(14)) / count(s.[STATUS]) * 100),2) as '审核通过率％',
sum(case r.[EXAMRESULT] when '合格' then 1 else 0 end)  as 考试合格人数,
round(cast(sum(case r.[EXAMRESULT] when '合格' then 1 else 0 end) as numeric(14)) / count(s.[STATUS])* 100 ,2) as '考试合格率％'
FROM [DBO].[VIEW_EXAMSIGNUP] as s
left join dbo.examresult as r on s.examplanid=r.examplanid and s.workerid = r.workerid
where s.examplanid in (select examplanid FROM [DBO].[EXAMPLANSUBJECT] where PASSLINE is not null) 
and TrainUnitId is not null and [SIGNUPDATE] BETWEEN '{0}' AND '{1}' {2} {3}
group by s.[TRAINUNITNAME]) as t
";
            sql = string.Format(sql, startDate.HasValue ? startDate.Value.ToString("yyyy-MM-dd") : "1900-1-1"
                , endDate.HasValue ? endDate.Value.ToString("yyyy-MM-dd") + " 23:59:59" : "2050-1-1"
                , string.IsNullOrEmpty(PostTypeID) == false ? " and PostTypeID=" + PostTypeID : ""
                , string.IsNullOrEmpty(PostID) == false ? " and PostID=" + PostID : "");
            DBHelper db = new DBHelper();
            return db.GetFillData(sql);
        }

        /// <summary>
        /// 检查报名人当月是否存在相同岗位类别的其他岗位类别考试报名信息
        /// </summary>
        /// <param name="EXAMPLANID">本次考试计划</param>
        /// <param name="PostTypeID">岗位类别ID</param>
        /// <param name="YM">考试计划考试时间，格式(6位数字)：yyyyMM</param>
        /// <param name="WorkerCertificatecode">报名人身份证号</param>
        /// <returns>报名次数，大于0表示有其他报名，不允许本次报名</returns>
        public static int CheckExamSignupCount(long EXAMPLANID, int PostTypeID, string YM, string WorkerCertificatecode)
        {
            string sql = @"select count(1)
                            FROM dbo.examsignup s inner join dbo.examplan p
                            on s.certificatecode like'{0}%' and p.posttypeid={1} and replace(CONVERT(varchar(7),p.EXAMSTARTDATE, 20),'-','') ={2}
                            and s.EXAMPLANID = p.EXAMPLANID
                            where  p.EXAMPLANID<> {3}
                            and 
                            (
	                            (
		                            (s.[STATUS]='已受理' or s.[STATUS]='已审核' or s.[STATUS]='已缴费' or s.[STATUS]='待缴费')
		                            and [LATESTCHECKDATE] <getdate()
	                            )
	                            or [LATESTCHECKDATE] >=getdate()
                            )";
            DBHelper db = new DBHelper();
            return Convert.ToInt32(db.ExecuteScalar(string.Format(sql, WorkerCertificatecode, PostTypeID, YM, EXAMPLANID)));
        }

        /// <summary>
        /// 获取考试报名审批记录集合
        /// </summary>
        /// <param name="ExamSignUpID">报名ID</param>
        /// <returns>审批历史记录集合</returns>
        public static DataTable GetCheckHistoryList(long ExamSignUpID)
        {
            string sql = @"select row_number() over(order by ActionData, Action desc) as RowNo ,t.* from 
(
 SELECT '考试报名' as 'Action', [SIGNUPDATE] as ActionData, '已报名' as ActionResult,case when [STATUS] = '退回修改' then '退回修改' else '申请单位审核' end as ActionRemark, [WORKERNAME] as ActionMan 
 FROM [dbo].[EXAMSIGNUP] where [EXAMSIGNUPID]={0} and [SIGNUPDATE] is not null and  [STATUS] <> '未提交' 
  union all
  SELECT '单位审核' as 'Action', [FIRSTTRIALTIME] as ActionData,'已初审' as ActionResult,[HIREUNITADVISE] as ActionRemark, [UNITNAME] as ActionMan 
 FROM [dbo].[EXAMSIGNUP]   where [EXAMSIGNUPID]={0} and [FIRSTTRIALTIME] is not null and [S_TRAINUNITNAME] is null
    union all
   SELECT '报名初审' as 'Action', [FIRSTTRIALTIME] as ActionData,'已初审' ActionResult,'通过' as ActionRemark, [S_TRAINUNITNAME] as ActionMan 
 FROM [dbo].[EXAMSIGNUP]   where [EXAMSIGNUPID]={0} and [FIRSTTRIALTIME] is not null and [S_TRAINUNITNAME] is not null
 union all
  SELECT '建委受理' as 'Action', [AcceptTime] as ActionData, case [AcceptResult] when '通过' then '已受理' else '不予受理' end ActionResult, [AcceptResult] as ActionRemark, [AcceptMan] as ActionMan 
 FROM [dbo].[EXAMSIGNUP]   where [EXAMSIGNUPID]={0} and [AcceptResult] is not null 
   union all
  SELECT '建委审核' as 'Action', [CHECKDATE] as ActionData, case [CHECKRESULT] when '通过' then '已审核' else '审核未通过' end ActionResult, [CHECKRESULT] as ActionRemark, [CHECKMAN] as ActionMan 
 FROM [dbo].[EXAMSIGNUP]   where [EXAMSIGNUPID]={0} and CheckResult is not null 
 union all
  SELECT '审核确认' as 'Action', [PAYCONFIRMDATE] as ActionData, '审核确认' ActionResult, [PAYCONFIRMRULT] as ActionRemark, [PAYCONFIRMMAN] as ActionMan 
 FROM [dbo].[EXAMSIGNUP]   where [EXAMSIGNUPID]={0} and [PAYCONFIRMRULT] is not null 
    union all
  SELECT '违规申报锁定' as 'Action', [LockTime] as ActionData, case when dateadd(day,1,[LockEndTime])>getdate() then '锁定中' else '已解锁' end ActionResult, [LockReason] as ActionRemark, [CHECKMAN] as ActionMan 
 FROM [dbo].[EXAMSIGNUP]   where [EXAMSIGNUPID]={0} and [LockTime] >'2021-09-18'
  union all
SELECT '成绩公告' as 'Action', [MODIFYTIME] as ActionData, [STATUS] ActionResult, [EXAMRESULT] as ActionRemark, '建委注册中心' as ActionMan 
 FROM [dbo].[EXAMRESULT]   where [EXAMSIGNUP_ID]={0} and [STATUS]='成绩已公告' 
  union all
  SELECT '证书编号' as 'Action', [CREATETIME] as ActionData, case when [STATUS]='待审批' then '' else '已编号' end ActionResult, [CERTIFICATECODE] as ActionRemark, '建委注册中心' as ActionMan 
 FROM [dbo].[CERTIFICATE]   
 where [EXAMPLANID]=(select [EXAMPLANID] FROM [dbo].[EXAMSIGNUP]   where [EXAMSIGNUPID]={0}) 
 and [WORKERCERTIFICATECODE]=(select CERTIFICATECODE FROM [dbo].[EXAMSIGNUP]   where [EXAMSIGNUPID]={0})
 union all 
    SELECT '住建部核准' as 'Action', 
	        case when ZZUrlUpTime > [CREATETIME] and [STATUS] = '首次' then  ZZUrlUpTime 
                 when ZZUrlUpTime is null and [STATUS] = '首次' and EleCertErrTime is not null  then  EleCertErrTime 			    
			     else null
	        end as ActionData,
            case when ZZUrlUpTime > [CREATETIME] then  '已核准' 
			     when ZZUrlUpTime is null and [STATUS] = '首次' and EleCertErrTime is not null  then '核准未通过'
			     else null
	        end as ActionResult,
            case when ZZUrlUpTime > [CREATETIME] then  '已生成电子证书（办结）' 
			     when ZZUrlUpTime is null and [STATUS] = '首次' and EleCertErrTime is not null  then '审批不通过，未生成电子证书。' + [EleCertErrDesc]
			     else null
	        end as ActionRemark, 
         '住建部' as ActionMan 
    FROM [dbo].[CERTIFICATE]   
 where [EXAMPLANID]=(select [EXAMPLANID] FROM [dbo].[EXAMSIGNUP]   where [EXAMSIGNUPID]={0}) 
 and [WORKERCERTIFICATECODE]=(select CERTIFICATECODE FROM [dbo].[EXAMSIGNUP]   where [EXAMSIGNUPID]={0})
     and (ZZUrlUpTime > [CREATETIME] or (EleCertErrTime > [CREATETIME] and ZZUrlUpTime is null ) )
) t";
            return CommonDAL.GetDataTable(string.Format(sql, ExamSignUpID));
        }

        /// <summary>
        /// 获取考试报名审批记录集合（对个人和企业，业务决定后，住建部校验不通过前3天显示等待，不显示不通过原因）
        /// </summary>
        /// <param name="ExamSignUpID">报名ID</param>
        /// <returns>审批历史记录集合</returns>
        public static DataTable GetCheckHistoryListForGRQY(long ExamSignUpID)
        {
            string sql = @"select row_number() over(order by ActionData, Action desc) as RowNo ,t.* from 
(
 SELECT '考试报名' as 'Action', [SIGNUPDATE] as ActionData, '已报名' as ActionResult,case when [STATUS] = '退回修改' then '退回修改' else '申请单位审核' end as ActionRemark, [WORKERNAME] as ActionMan 
 FROM [dbo].[EXAMSIGNUP] where [EXAMSIGNUPID]={0} and [SIGNUPDATE] is not null and  [STATUS] <> '未提交' 
  union all
  SELECT '单位审核' as 'Action', [FIRSTTRIALTIME] as ActionData,'已初审' as ActionResult,[HIREUNITADVISE] as ActionRemark, [UNITNAME] as ActionMan 
 FROM [dbo].[EXAMSIGNUP]   where [EXAMSIGNUPID]={0} and [FIRSTTRIALTIME] is not null and [S_TRAINUNITNAME] is null
    union all
   SELECT '报名初审' as 'Action', [FIRSTTRIALTIME] as ActionData,'已初审' ActionResult,'通过' as ActionRemark, [S_TRAINUNITNAME] as ActionMan 
 FROM [dbo].[EXAMSIGNUP]   where [EXAMSIGNUPID]={0} and [FIRSTTRIALTIME] is not null and [S_TRAINUNITNAME] is not null
 union all
  SELECT '建委受理' as 'Action', [AcceptTime] as ActionData, case [AcceptResult] when '通过' then '已受理' else '不予受理' end ActionResult, [AcceptResult] as ActionRemark, [AcceptMan] as ActionMan 
 FROM [dbo].[EXAMSIGNUP]   where [EXAMSIGNUPID]={0} and [AcceptResult] is not null 
   union all
  SELECT '建委审核' as 'Action', [CHECKDATE] as ActionData, case [CHECKRESULT] when '通过' then '已审核' else '审核未通过' end ActionResult, [CHECKRESULT] as ActionRemark, [CHECKMAN] as ActionMan 
 FROM [dbo].[EXAMSIGNUP]   where [EXAMSIGNUPID]={0} and CheckResult is not null 
 union all
  SELECT '审核确认' as 'Action', [PAYCONFIRMDATE] as ActionData, '审核确认' ActionResult, [PAYCONFIRMRULT] as ActionRemark, [PAYCONFIRMMAN] as ActionMan 
 FROM [dbo].[EXAMSIGNUP]   where [EXAMSIGNUPID]={0} and [PAYCONFIRMRULT] is not null 
    union all
  SELECT '违规申报锁定' as 'Action', [LockTime] as ActionData, case when dateadd(day,1,[LockEndTime])>getdate() then '锁定中' else '已解锁' end ActionResult, [LockReason] as ActionRemark, [CHECKMAN] as ActionMan 
 FROM [dbo].[EXAMSIGNUP]   where [EXAMSIGNUPID]={0} and [LockTime] >'2021-09-18'
  union all
SELECT '成绩公告' as 'Action', [MODIFYTIME] as ActionData, [STATUS] ActionResult, [EXAMRESULT] as ActionRemark, '建委注册中心' as ActionMan 
 FROM [dbo].[EXAMRESULT]   where [EXAMSIGNUP_ID]={0} and [STATUS]='成绩已公告' 
  union all
  SELECT '证书编号' as 'Action', [CREATETIME] as ActionData, case when [STATUS]='待审批' then '' else '已编号' end ActionResult, [CERTIFICATECODE] as ActionRemark, '建委注册中心' as ActionMan 
 FROM [dbo].[CERTIFICATE]   
 where [EXAMPLANID]=(select [EXAMPLANID] FROM [dbo].[EXAMSIGNUP]   where [EXAMSIGNUPID]={0}) 
 and [WORKERCERTIFICATECODE]=(select CERTIFICATECODE FROM [dbo].[EXAMSIGNUP]   where [EXAMSIGNUPID]={0})
 union all 
    SELECT '住建部核准' as 'Action', 
	        case when ZZUrlUpTime > [CREATETIME] and [STATUS] = '首次' then  ZZUrlUpTime 
                 when ZZUrlUpTime is null and [STATUS] = '首次' and EleCertErrTime is not null and [CHECKDATE] < dateadd(day,-2,getdate()) then  EleCertErrTime 			    
			     else null
	        end as ActionData,
            case when ZZUrlUpTime > [CREATETIME] then  '已核准' 
			     when ZZUrlUpTime is null and [STATUS] = '首次' and EleCertErrTime is not null and [CHECKDATE] < dateadd(day,-2,getdate())  then '核准未通过'
			     else null
	        end as ActionResult,
            case when ZZUrlUpTime > [CREATETIME] then  '已生成电子证书（办结）' 
			     when ZZUrlUpTime is null and [STATUS] = '首次' and EleCertErrTime is not null and [CHECKDATE] < dateadd(day,-2,getdate())  then '审批不通过，未生成电子证书。' + [EleCertErrDesc]
                 when ZZUrlUpTime is null and [STATUS] = '首次' and EleCertErrTime is not null then '住建部正在进行数据校验核对，生成电子证书需要1-3个工作日，请您耐心等待。'
			     else null
	        end as ActionRemark, 
         '住建部' as ActionMan 
    FROM [dbo].[CERTIFICATE]   
 where [EXAMPLANID]=(select [EXAMPLANID] FROM [dbo].[EXAMSIGNUP]   where [EXAMSIGNUPID]={0}) 
 and [WORKERCERTIFICATECODE]=(select CERTIFICATECODE FROM [dbo].[EXAMSIGNUP]   where [EXAMSIGNUPID]={0})
     and (ZZUrlUpTime > [CREATETIME] or (EleCertErrTime > [CREATETIME] and ZZUrlUpTime is null ) )
) t";
            return CommonDAL.GetDataTable(string.Format(sql, ExamSignUpID));
        }

        /// <summary>
        /// 获取考试报名审批记录集合,隐藏建委审核记录（用于准考证放号前对个人显示）
        /// </summary>
        /// <param name="ExamSignUpID">报名ID</param>
        /// <returns>审批历史记录集合</returns>
        public static DataTable GetCheckHistoryListHideJWCheck(long ExamSignUpID)
        {
            string sql = @"select row_number() over(order by ActionData, Action desc) as RowNo ,t.* from 
(
 SELECT '考试报名' as 'Action', [SIGNUPDATE] as ActionData, '已报名' as ActionResult,case when [STATUS] = '退回修改' then '退回修改' else '申请单位审核' end as ActionRemark, [WORKERNAME] as ActionMan 
 FROM [dbo].[EXAMSIGNUP] where [EXAMSIGNUPID]={0} and [SIGNUPDATE] is not null and  [STATUS] <> '未提交' 
  union all
  SELECT '单位审核' as 'Action', [FIRSTTRIALTIME] as ActionData,'已初审' as ActionResult,[HIREUNITADVISE] as ActionRemark, [UNITNAME] as ActionMan 
 FROM [dbo].[EXAMSIGNUP]   where [EXAMSIGNUPID]={0} and [FIRSTTRIALTIME] is not null and [S_TRAINUNITNAME] is null
    union all
   SELECT '报名初审' as 'Action', [FIRSTTRIALTIME] as ActionData,'已初审' ActionResult,'通过' as ActionRemark, [S_TRAINUNITNAME] as ActionMan 
 FROM [dbo].[EXAMSIGNUP]   where [EXAMSIGNUPID]={0} and [FIRSTTRIALTIME] is not null and [S_TRAINUNITNAME] is not null

) t";
            return CommonDAL.GetDataTable(string.Format(sql, ExamSignUpID));
        }

        /// <summary>
        /// 获取考试报名审批记录集合(新版职业技能)
        /// </summary>
        /// <param name="ExamSignUpID">报名ID</param>
        /// <returns>审批历史记录集合</returns>
        public static DataTable GetCheckHistoryListOfNewZYJN(long ExamSignUpID)
        {
            string sql = @"select row_number() over(order by ActionData, Action desc) as RowNo ,t.* from 
(
 SELECT '考试报名' as 'Action', [SIGNUPDATE] as ActionData, '已报名' as ActionResult,case when [STATUS] = '退回修改' then '退回修改' else '提交培训点审核' end as ActionRemark, [WORKERNAME] as ActionMan 
 FROM [dbo].[EXAMSIGNUP] where [EXAMSIGNUPID]={0} and [SIGNUPDATE] is not null and  [STATUS] <> '未提交' 
    union all
  SELECT '培训点审核' as 'Action', [CHECKDATE] as ActionData, '已审核' ActionResult, [CHECKRESULT] as ActionRemark, [CHECKMAN] as ActionMan 
 FROM [dbo].[EXAMSIGNUP]   where [EXAMSIGNUPID]={0} and CheckResult is not null 
    union all
SELECT '成绩公告' as 'Action', [MODIFYTIME] as ActionData, [STATUS] ActionResult, [EXAMRESULT] as ActionRemark, '建委注册中心' as ActionMan 
 FROM [dbo].[EXAMRESULT]   where [EXAMSIGNUP_ID]={0} and [STATUS]='成绩已公告' 
  union all
  SELECT '发放证书' as 'Action', [CONFERDATE] as ActionData, case when [STATUS]='待审批' then '' else '已编号' end ActionResult, [CERTIFICATECODE] as ActionRemark, '建委注册中心' as ActionMan 
 FROM [dbo].[CERTIFICATE]   
 where [EXAMPLANID]=(select [EXAMPLANID] FROM [dbo].[EXAMSIGNUP]   where [EXAMSIGNUPID]={0}) 
 and [WORKERCERTIFICATECODE]=(select CERTIFICATECODE FROM [dbo].[EXAMSIGNUP]   where [EXAMSIGNUPID]={0})
) t";
            return CommonDAL.GetDataTable(string.Format(sql, ExamSignUpID));
        }

        /// <summary>
        /// 判断考生近期是否存在专业考试成绩尚未判定
        /// </summary>
        /// <param name="WorkerCertificateCode">身份证号</param>
        /// <param name="PostIDList">专业ID列表，用英文逗号分隔</param>
        /// <param name="monthSpan">查询近几个月的考试报名</param>
        /// <param name="ExamPlanID">当前要报名的考试计划ID</param>
        /// <returns>存在问出成绩考试返回true，否则返回false</returns>
        public static bool CheckIfHaveExaming(string WorkerCertificateCode, string PostIDList, int monthSpan, long ExamPlanID)
        {
//            string sql = @"select top 1 count(*) from 
//                             DBO.EXAMSIGNUP s left join [dbo].[EXAMRESULT] r ON s.[EXAMSIGNUPID] = r.[EXAMSIGNUP_ID] 
//                             left join EXAMPLAN p on s.EXAMPLANID = p.EXAMPLANID
//                            where s.CERTIFICATECODE='{0}' and r.[STATUS]<>'成绩已公告' and p.SIGNUPSTARTDATE > DATEADD(month,-{2},getdate()) and  p.POSTID in({1})
//                            group by s.[EXAMPLANID]
//                            having count(*) >0
//                            order by s.EXAMPLANID desc";

            //1）、上次报名审核尚未截止，这次不允许报名。
            //2）、上次报名审核未通过，这次可以报名。
            //3）、上次报名审核通过，尚未发放准考证，不允许报名。
            //4）、上次报名审核通过，已经发放准考证，尚未公告成绩，不允许报名。
            //5）、上次报名审核通过，已经发放准考证，已经公告成绩（未通过），允许报名。
            //6）、上次报名审核通过，已经发放准考证，已经公告成绩（通过，尚未发放证书），不允许报名。

            string sql = @"select top 1 count(*) from 
                             DBO.EXAMSIGNUP s left join [dbo].[EXAMRESULT] r ON s.[EXAMSIGNUPID] = r.[EXAMSIGNUP_ID] 
                             inner join EXAMPLAN p on s.EXAMPLANID = p.EXAMPLANID
                            where s.CERTIFICATECODE='{0}' 
                                and (	r.[STATUS]<>'成绩已公告' 
									    or (r.[STATUS]='成绩已公告' and r.EXAMRESULT='合格' and not exists(select 1 from [dbo].[CERTIFICATE] where [CERTIFICATE].WORKERCERTIFICATECODE='{0}' and [CERTIFICATE].EXAMPLANID =r.EXAMPLANID))
									    or (p.LATESTCHECKDATE >dateadd(day,-1,getdate()) and r.EXAMRESULTID is null)
									    or (p.LATESTCHECKDATE <dateadd(day,-1,getdate()) and s.[STATUS]='已缴费' and r.EXAMRESULTID is null)
								    )
                                and p.SIGNUPSTARTDATE > DATEADD(month,-{2},getdate()) 
                                and p.POSTID in({1})
                                and s.[EXAMPLANID] <> {3}
                            group by s.[EXAMPLANID]
                            having count(*) >0
                            order by s.EXAMPLANID desc";

            DataTable dt = CommonDAL.GetDataTable(string.Format(sql, WorkerCertificateCode, PostIDList, monthSpan, ExamPlanID));

            if (dt != null && dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0][0]) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 是否存在未办结的安管人员考试（1、报名审核中、2、已发准考证未出成绩、3成绩合格尚未发证）
        /// </summary>
        /// <param name="WorkerCertificateCode">身份证号</param>
        /// <returns>存在返回true，否则返回false</returns>
        public static bool CheckIfHaveExamSinupNoFinishi(string WorkerCertificateCode)
        {

            string sql = @"select count(*) 
                        from DBO.EXAMSIGNUP s 
                        left join [dbo].[EXAMRESULT] r ON s.[EXAMSIGNUPID] = r.[EXAMSIGNUP_ID] 
                        inner join EXAMPLAN p on s.EXAMPLANID = p.EXAMPLANID
                        where s.CERTIFICATECODE='{0}' 
                            and (	
			                        (p.LATESTCHECKDATE > getdate() and s.[STATUS]='已初审')
			                        or (p.LATESTCHECKDATE > getdate() and s.[STATUS]='已受理')
			                        or (p.[EXAMSTARTDATE] > getdate() and s.[STATUS]='已审核')			
			                        or (s.[STATUS]='已缴费' and r.EXAMRESULTID is null)
			                        or r.[STATUS]='成绩未公告'
			                        or r.[STATUS]='成绩未判定'
			                        or (r.[STATUS]='成绩已公告' and r.EXAMRESULT='合格' and not exists(select 1 from [dbo].[CERTIFICATE] where [CERTIFICATE].WORKERCERTIFICATECODE='{0}' and [CERTIFICATE].EXAMPLANID =r.EXAMPLANID and [CERTIFICATE].CHECKDATE > '1950-1-1'))

		                        )
                            and p.SIGNUPSTARTDATE > DATEADD(month,-6,getdate()) 
                        ";

            int rows = CommonDAL.SelectRowCount(string.Format(sql, WorkerCertificateCode));

            if (rows > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 锁定报名表
        /// </summary>
        /// <param name="ExamSignUpID">报名ID</param>
        /// <param name="LockTime">锁定时间</param>
        /// <param name="LockEndTime">锁定截止日期</param>
        /// <param name="LockReason">锁定原因，最大4000个字符（2000汉字）</param>
        /// <param name="LockMan">锁定人</param>
        /// <returns></returns>
        public static int Lock(long ExamSignUpID, DateTime LockTime, DateTime LockEndTime, string LockReason, string LockMan)
        {
            string sql = @"UPDATE dbo.ExamSignUp
				SET	LockTime = @LockTime,LockEndTime = @LockEndTime,LockReason = @LockReason,LockMan = @LockMan
			WHERE
				ExamSignUpID = @ExamSignUpID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamSignUpID", DbType.Int64, ExamSignUpID));
            p.Add(db.CreateParameter("LockTime", DbType.DateTime, LockTime));
            p.Add(db.CreateParameter("LockEndTime", DbType.DateTime, LockEndTime));
            p.Add(db.CreateParameter("LockReason", DbType.String, LockReason));
            p.Add(db.CreateParameter("LockMan", DbType.String, LockMan));
            return db.ExcuteNonQuery(sql, p.ToArray());
        }

        /// <summary>
        /// 解锁报名
        /// </summary>
        /// <param name="ExamSignUpID">报名ID</param>
        /// <returns></returns>
        public static int UnLock(long ExamSignUpID)
        {
            string sql = @"UPDATE dbo.ExamSignUp
				SET	LockEndTime = getdate()
			WHERE
				ExamSignUpID = @ExamSignUpID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamSignUpID", DbType.Int64, ExamSignUpID));
            return db.ExcuteNonQuery(sql, p.ToArray());
        }

        /// <summary>
        /// 获取当事人正在被锁定的报名记录
        /// </summary>
        /// <param name="WorkerCertificateCode">身份证号</param>
        /// <returns></returns>
        public static ExamSignUpOB GetLockIng(string WorkerCertificateCode)
        {
            string sql = @"
			SELECT ExamSignUpID,SignUpCode,SignUpDate,WorkerID,UnitID,TrainUnitID,ExamPlanID,WorkStartDate,WorkYearNumer,PersonDetail,
                HireUnitAdvise,AdminUnitAdvise,CheckCode,CheckResult,CheckMan,CheckDate,PayNoticeCode,PayNoticeResult,PayNoticeMan,PayNoticeDate,
                PayMoney,PayConfirmCode,PayConfirmRult,PayConfirmMan,PayConfirmDate,FacePhoto,Status,WorkerName,CertificateType,CertificateCode,
                UnitName,UnitCode,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,SKILLLEVEL,S_SEX,S_CULTURALLEVEL,
                S_PHONE,S_BIRTHDAY,S_TRAINUNITNAME,ISCONDITIONS,FIRSTTRIALTIME,SheBaoCheck,SignUpMan,PROMISE,SIGNUPPLACEID,CHECKDATEPLAN,PLACENAME,FirstCheckType
                ,[LockTime],[LockEndTime],[LockReason],[LockMan],AcceptMan,AcceptTime,AcceptResult,ENT_ContractType,ENT_ContractStartTime,ENT_ContractENDTime,SignupPromise
			FROM dbo.ExamSignUp
			WHERE CertificateCode = @WorkerCertificateCode and dateadd(day,1,[LockEndTime])>getdate()";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("WorkerCertificateCode", DbType.String, WorkerCertificateCode));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    ExamSignUpOB _ExamSignUpOB = null;
                    if (reader.Read())
                    {
                        _ExamSignUpOB = new ExamSignUpOB();
                        if (reader["ExamSignUpID"] != DBNull.Value) _ExamSignUpOB.ExamSignUpID = Convert.ToInt64(reader["ExamSignUpID"]);
                        if (reader["SignUpCode"] != DBNull.Value) _ExamSignUpOB.SignUpCode = Convert.ToString(reader["SignUpCode"]);
                        if (reader["SignUpDate"] != DBNull.Value) _ExamSignUpOB.SignUpDate = Convert.ToDateTime(reader["SignUpDate"]);
                        if (reader["WorkerID"] != DBNull.Value) _ExamSignUpOB.WorkerID = Convert.ToInt64(reader["WorkerID"]);
                        if (reader["UnitID"] != DBNull.Value) _ExamSignUpOB.UnitID = Convert.ToInt64(reader["UnitID"]);
                        if (reader["TrainUnitID"] != DBNull.Value) _ExamSignUpOB.TrainUnitID = Convert.ToInt64(reader["TrainUnitID"]);
                        if (reader["ExamPlanID"] != DBNull.Value) _ExamSignUpOB.ExamPlanID = Convert.ToInt64(reader["ExamPlanID"]);
                        if (reader["WorkStartDate"] != DBNull.Value) _ExamSignUpOB.WorkStartDate = Convert.ToDateTime(reader["WorkStartDate"]);
                        if (reader["WorkYearNumer"] != DBNull.Value) _ExamSignUpOB.WorkYearNumer = Convert.ToInt32(reader["WorkYearNumer"]);
                        if (reader["PersonDetail"] != DBNull.Value) _ExamSignUpOB.PersonDetail = Convert.ToString(reader["PersonDetail"]);
                        if (reader["HireUnitAdvise"] != DBNull.Value) _ExamSignUpOB.HireUnitAdvise = Convert.ToString(reader["HireUnitAdvise"]);
                        if (reader["AdminUnitAdvise"] != DBNull.Value) _ExamSignUpOB.AdminUnitAdvise = Convert.ToString(reader["AdminUnitAdvise"]);
                        if (reader["CheckCode"] != DBNull.Value) _ExamSignUpOB.CheckCode = Convert.ToString(reader["CheckCode"]);
                        if (reader["CheckResult"] != DBNull.Value) _ExamSignUpOB.CheckResult = Convert.ToString(reader["CheckResult"]);
                        if (reader["CheckMan"] != DBNull.Value) _ExamSignUpOB.CheckMan = Convert.ToString(reader["CheckMan"]);
                        if (reader["CheckDate"] != DBNull.Value) _ExamSignUpOB.CheckDate = Convert.ToDateTime(reader["CheckDate"]);
                        if (reader["PayNoticeCode"] != DBNull.Value) _ExamSignUpOB.PayNoticeCode = Convert.ToString(reader["PayNoticeCode"]);
                        if (reader["PayNoticeResult"] != DBNull.Value) _ExamSignUpOB.PayNoticeResult = Convert.ToString(reader["PayNoticeResult"]);
                        if (reader["PayNoticeMan"] != DBNull.Value) _ExamSignUpOB.PayNoticeMan = Convert.ToString(reader["PayNoticeMan"]);
                        if (reader["PayNoticeDate"] != DBNull.Value) _ExamSignUpOB.PayNoticeDate = Convert.ToDateTime(reader["PayNoticeDate"]);
                        if (reader["PayMoney"] != DBNull.Value) _ExamSignUpOB.PayMoney = Convert.ToDecimal(reader["PayMoney"]);
                        if (reader["PayConfirmCode"] != DBNull.Value) _ExamSignUpOB.PayConfirmCode = Convert.ToString(reader["PayConfirmCode"]);
                        if (reader["PayConfirmRult"] != DBNull.Value) _ExamSignUpOB.PayConfirmRult = Convert.ToString(reader["PayConfirmRult"]);
                        if (reader["PayConfirmMan"] != DBNull.Value) _ExamSignUpOB.PayConfirmMan = Convert.ToString(reader["PayConfirmMan"]);
                        if (reader["PayConfirmDate"] != DBNull.Value) _ExamSignUpOB.PayConfirmDate = Convert.ToDateTime(reader["PayConfirmDate"]);
                        if (reader["FacePhoto"] != DBNull.Value) _ExamSignUpOB.FacePhoto = Convert.ToString(reader["FacePhoto"]);
                        if (reader["Status"] != DBNull.Value) _ExamSignUpOB.Status = Convert.ToString(reader["Status"]);
                        if (reader["WorkerName"] != DBNull.Value) _ExamSignUpOB.WorkerName = Convert.ToString(reader["WorkerName"]);
                        if (reader["CertificateType"] != DBNull.Value) _ExamSignUpOB.CertificateType = Convert.ToString(reader["CertificateType"]);
                        if (reader["CertificateCode"] != DBNull.Value) _ExamSignUpOB.CertificateCode = Convert.ToString(reader["CertificateCode"]);
                        if (reader["UnitName"] != DBNull.Value) _ExamSignUpOB.UnitName = Convert.ToString(reader["UnitName"]);
                        if (reader["UnitCode"] != DBNull.Value) _ExamSignUpOB.UnitCode = Convert.ToString(reader["UnitCode"]);
                        if (reader["CreatePersonID"] != DBNull.Value) _ExamSignUpOB.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                        if (reader["CreateTime"] != DBNull.Value) _ExamSignUpOB.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                        if (reader["ModifyPersonID"] != DBNull.Value) _ExamSignUpOB.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                        if (reader["ModifyTime"] != DBNull.Value) _ExamSignUpOB.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                        if (reader["SKILLLEVEL"] != DBNull.Value) _ExamSignUpOB.SKILLLEVEL = Convert.ToString(reader["SKILLLEVEL"]);
                        if (reader["S_SEX"] != DBNull.Value) _ExamSignUpOB.S_SEX = Convert.ToString(reader["S_SEX"]);
                        if (reader["S_CULTURALLEVEL"] != DBNull.Value) _ExamSignUpOB.S_CULTURALLEVEL = Convert.ToString(reader["S_CULTURALLEVEL"]);
                        if (reader["S_PHONE"] != DBNull.Value) _ExamSignUpOB.S_PHONE = Convert.ToString(reader["S_PHONE"]);
                        if (reader["S_BIRTHDAY"] != DBNull.Value) _ExamSignUpOB.S_BIRTHDAY = Convert.ToDateTime(reader["S_BIRTHDAY"]);
                        if (reader["S_TRAINUNITNAME"] != DBNull.Value) _ExamSignUpOB.S_TRAINUNITNAME = Convert.ToString(reader["S_TRAINUNITNAME"]);
                        if (reader["ISCONDITIONS"] != DBNull.Value) _ExamSignUpOB.IsConditions = Convert.ToString(reader["ISCONDITIONS"]);
                        if (reader["FIRSTTRIALTIME"] != DBNull.Value) _ExamSignUpOB.FIRSTTRIALTIME = Convert.ToDateTime(reader["FIRSTTRIALTIME"]);
                        if (reader["SheBaoCheck"] != DBNull.Value) _ExamSignUpOB.SheBaoCheck = Convert.ToByte(reader["SheBaoCheck"]);
                        if (reader["SignUpMan"] != DBNull.Value) _ExamSignUpOB.SignUpMan = Convert.ToString(reader["SignUpMan"]);
                        if (reader["Promise"] != DBNull.Value) _ExamSignUpOB.Promise = Convert.ToInt32(reader["Promise"]);
                        if (reader["SignUpPlaceID"] != DBNull.Value) _ExamSignUpOB.SignUpPlaceID = Convert.ToInt64(reader["SignUpPlaceID"]);
                        if (reader["PlaceName"] != DBNull.Value) _ExamSignUpOB.PlaceName = Convert.ToString(reader["PlaceName"]);
                        if (reader["CheckDatePlan"] != DBNull.Value) _ExamSignUpOB.CheckDatePlan = Convert.ToDateTime(reader["CheckDatePlan"]);
                        if (reader["FirstCheckType"] != DBNull.Value) _ExamSignUpOB.FirstCheckType = Convert.ToInt32(reader["FirstCheckType"]);
                        if (reader["LockTime"] != DBNull.Value) _ExamSignUpOB.LockTime = Convert.ToDateTime(reader["LockTime"]);
                        if (reader["LockEndTime"] != DBNull.Value) _ExamSignUpOB.LockEndTime = Convert.ToDateTime(reader["LockEndTime"]);
                        if (reader["LockReason"] != DBNull.Value) _ExamSignUpOB.LockReason = Convert.ToString(reader["LockReason"]);
                        if (reader["LockMan"] != DBNull.Value) _ExamSignUpOB.LockMan = Convert.ToString(reader["LockMan"]);
                        if (reader["AcceptResult"] != DBNull.Value) _ExamSignUpOB.AcceptResult = Convert.ToString(reader["AcceptResult"]);
                        if (reader["AcceptMan"] != DBNull.Value) _ExamSignUpOB.AcceptMan = Convert.ToString(reader["AcceptMan"]);
                        if (reader["AcceptTime"] != DBNull.Value) _ExamSignUpOB.AcceptTime = Convert.ToDateTime(reader["AcceptTime"]);
                        if (reader["ENT_ContractType"] != DBNull.Value) _ExamSignUpOB.ENT_ContractType = Convert.ToInt32(reader["ENT_ContractType"]);
                        if (reader["ENT_ContractStartTime"] != DBNull.Value) _ExamSignUpOB.ENT_ContractStartTime = Convert.ToDateTime(reader["ENT_ContractStartTime"]);
                        if (reader["ENT_ContractENDTime"] != DBNull.Value) _ExamSignUpOB.ENT_ContractENDTime = Convert.ToDateTime(reader["ENT_ContractENDTime"]);
                        if (reader["SignupPromise"] != DBNull.Value) _ExamSignUpOB.SignupPromise = Convert.ToInt32(reader["SignupPromise"]);
                    }
                    reader.Close();
                    db.Close();
                    return _ExamSignUpOB;
                }
            }
            catch (Exception ex)
            {
                db.Close();
                throw ex;
            }
        }

        /// <summary>
        /// 更新承诺标识
        /// </summary>
        /// <param name="ExamSignUpID">报名ID</param>
        /// <param name="SignupPromise">是否上传报名承诺</param>
        /// <returns></returns>
        public static int UpdateSignupPromise(long ExamSignUpID, int SignupPromise)
        {
            string sql = @"
			UPDATE dbo.ExamSignUp
				SET	SignupPromise = @SignupPromise        
			WHERE
				ExamSignUpID = @ExamSignUpID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamSignUpID", DbType.Int64, ExamSignUpID));
            p.Add(db.CreateParameter("SignupPromise", DbType.Int32, SignupPromise));
            return db.ExcuteNonQuery(sql, p.ToArray());
        }

        /// <summary>
        /// 清空社保核验结果，重新发起核验
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamSignUpID">报名ID</param>
        /// <returns></returns>
        public static int ClearSheBaoCheck(DbTransaction tran, long ExamSignUpID)
        {
            string sql = @"
			UPDATE dbo.ExamSignUp
				SET	SheBaoCheck = null       
			WHERE
				ExamSignUpID = @ExamSignUpID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamSignUpID", DbType.Int64, ExamSignUpID));

            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        public static int ClearSheBaoCheck(long ExamSignUpID)
        {
            return ClearSheBaoCheck(null, ExamSignUpID);
        }

        /// <summary>
        /// 格式化社保比对类型：社保符合、法人符合、人工审核
        /// </summary>
        /// <param name="FirstCheckTypeID">格式化社保比对类型ID</param>
        /// <returns>格式化社保比对类型名称</returns>
        public static string FormatFirstCheckType(string FirstCheckTypeID)
        {
            switch (FirstCheckTypeID)
            {
                case "-1":

                    return "人工审核";
                case "0":
                    return "人工审核";

                case "1":
                    return "人工审核";

                case "3":
                    return "社保符合";

                case "4":
                    return "法人符合";
                default:
                    return "人工审核";
            }
        }
        /// <summary>
        /// 格式化社保缴费月份
        /// </summary>
        /// <param name="JFCount">缴费月类型，0：无缴费记录；1：上月有缴费记录；2：上上个月有缴费记录。</param>
        /// <returns>格式化后的缴费月份</returns>
        public static string FormatJFCount(object JFCount)
        {
            if (JFCount == DBNull.Value || JFCount == null) return "";
            switch (Convert.ToInt32(JFCount))
            {
                case 0:
                    return "";
                case 1:
                    return "上";
                case 2:
                    return "上上";
                default:
                    return "";
            }
        }

        #endregion 自定义方法
    }
}
