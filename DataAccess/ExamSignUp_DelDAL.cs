using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

using Model;

namespace DataAccess
{
    /// <summary>
    /// 业务类实现--ExamSignUp_DelDAL(填写类描述)
    /// </summary>
    public class ExamSignUp_DelDAL
    {
        protected long? ExamSignUpDelID, DeleteMan, DeleteTime ;


        public ExamSignUp_DelDAL() { }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="ExamSignUp_DelOB">对象实体类</param>
        /// <returns></returns>
        public static long Insert(ExamSignUp_DelOB _ExamSignUp_DelOB)
        {
            return Insert(null, _ExamSignUp_DelOB);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamSignUp_DelOB">对象实体类</param>
        /// <returns></returns>
        public static long Insert(DbTransaction tran, ExamSignUp_DelOB _ExamSignUp_DelOB)
        {
            DBHelper db = new DBHelper();
         
            string sql = @"
			INSERT INTO dbo.ExamSignUp_Del( DeleteMan, DeleteTime,ExamSignUpID,SignUpCode,SignUpDate,WorkerID,UnitID,TrainUnitID,ExamPlanID,WorkStartDate,WorkYearNumer,PersonDetail,HireUnitAdvise,AdminUnitAdvise,CheckCode,CheckResult,CheckMan,CheckDate,PayNoticeCode,PayNoticeResult,PayNoticeMan,PayNoticeDate,PayMoney,PayConfirmCode,PayConfirmRult,PayConfirmMan,PayConfirmDate,FacePhoto,Status,WorkerName,CertificateType,CertificateCode,UnitName,UnitCode,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,SKILLLEVEL,S_SEX,S_CULTURALLEVEL,S_PHONE,S_BIRTHDAY)
			VALUES ( @DeleteMan, @DeleteTime,@ExamSignUpID,@SignUpCode,@SignUpDate,@WorkerID,@UnitID,@TrainUnitID,@ExamPlanID,@WorkStartDate,@WorkYearNumer,@PersonDetail,@HireUnitAdvise,@AdminUnitAdvise,@CheckCode,@CheckResult,@CheckMan,@CheckDate,@PayNoticeCode,@PayNoticeResult,@PayNoticeMan,@PayNoticeDate,@PayMoney,@PayConfirmCode,@PayConfirmRult,@PayConfirmMan,@PayConfirmDate,@FacePhoto,@Status,@WorkerName,@CertificateType,@CertificateCode,@UnitName,@UnitCode,@CreatePersonID,@CreateTime,@ModifyPersonID,@ModifyTime,@SKILLLEVEL,@S_SEX,@S_CULTURALLEVEL,@S_PHONE,@S_BIRTHDAY);SELECT @ExamSignUpDelID = @@IDENTITY";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateOutParameter("ExamSignUpDelID", DbType.Int64));
            p.Add(db.CreateParameter("DeleteMan", DbType.String, _ExamSignUp_DelOB.DeleteMan));
            p.Add(db.CreateParameter("DeleteTime", DbType.DateTime, _ExamSignUp_DelOB.DeleteTime));
            p.Add(db.CreateParameter("ExamSignUpID", DbType.Int64, _ExamSignUp_DelOB.ExamSignUpID));
            p.Add(db.CreateParameter("SignUpCode", DbType.String, _ExamSignUp_DelOB.SignUpCode));
            p.Add(db.CreateParameter("SignUpDate", DbType.DateTime, _ExamSignUp_DelOB.SignUpDate));
            p.Add(db.CreateParameter("WorkerID", DbType.Int64, _ExamSignUp_DelOB.WorkerID));
            p.Add(db.CreateParameter("UnitID", DbType.Int64, _ExamSignUp_DelOB.UnitID));
            p.Add(db.CreateParameter("TrainUnitID", DbType.Int64, _ExamSignUp_DelOB.TrainUnitID));
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, _ExamSignUp_DelOB.ExamPlanID));
            p.Add(db.CreateParameter("WorkStartDate", DbType.DateTime, _ExamSignUp_DelOB.WorkStartDate));
            p.Add(db.CreateParameter("WorkYearNumer", DbType.Int32, _ExamSignUp_DelOB.WorkYearNumer));
            p.Add(db.CreateParameter("PersonDetail", DbType.String, _ExamSignUp_DelOB.PersonDetail));
            p.Add(db.CreateParameter("HireUnitAdvise", DbType.String, _ExamSignUp_DelOB.HireUnitAdvise));
            p.Add(db.CreateParameter("AdminUnitAdvise", DbType.String, _ExamSignUp_DelOB.AdminUnitAdvise));
            p.Add(db.CreateParameter("CheckCode", DbType.String, _ExamSignUp_DelOB.CheckCode));
            p.Add(db.CreateParameter("CheckResult", DbType.String, _ExamSignUp_DelOB.CheckResult));
            p.Add(db.CreateParameter("CheckMan", DbType.String, _ExamSignUp_DelOB.CheckMan));
            p.Add(db.CreateParameter("CheckDate", DbType.DateTime, _ExamSignUp_DelOB.CheckDate));
            p.Add(db.CreateParameter("PayNoticeCode", DbType.String, _ExamSignUp_DelOB.PayNoticeCode));
            p.Add(db.CreateParameter("PayNoticeResult", DbType.String, _ExamSignUp_DelOB.PayNoticeResult));
            p.Add(db.CreateParameter("PayNoticeMan", DbType.String, _ExamSignUp_DelOB.PayNoticeMan));
            p.Add(db.CreateParameter("PayNoticeDate", DbType.DateTime, _ExamSignUp_DelOB.PayNoticeDate));
            p.Add(db.CreateParameter("PayMoney", DbType.Decimal, _ExamSignUp_DelOB.PayMoney));
            p.Add(db.CreateParameter("PayConfirmCode", DbType.String, _ExamSignUp_DelOB.PayConfirmCode));
            p.Add(db.CreateParameter("PayConfirmRult", DbType.String, _ExamSignUp_DelOB.PayConfirmRult));
            p.Add(db.CreateParameter("PayConfirmMan", DbType.String, _ExamSignUp_DelOB.PayConfirmMan));
            p.Add(db.CreateParameter("PayConfirmDate", DbType.DateTime, _ExamSignUp_DelOB.PayConfirmDate));
            p.Add(db.CreateParameter("FacePhoto", DbType.String, _ExamSignUp_DelOB.FacePhoto));
            p.Add(db.CreateParameter("Status", DbType.String, _ExamSignUp_DelOB.Status));
            p.Add(db.CreateParameter("WorkerName", DbType.String, _ExamSignUp_DelOB.WorkerName));
            p.Add(db.CreateParameter("CertificateType", DbType.String, _ExamSignUp_DelOB.CertificateType));
            p.Add(db.CreateParameter("CertificateCode", DbType.String, _ExamSignUp_DelOB.CertificateCode));
            p.Add(db.CreateParameter("UnitName", DbType.String, _ExamSignUp_DelOB.UnitName));
            p.Add(db.CreateParameter("UnitCode", DbType.String, _ExamSignUp_DelOB.UnitCode));
            p.Add(db.CreateParameter("CreatePersonID", DbType.Int64, _ExamSignUp_DelOB.CreatePersonID));
            p.Add(db.CreateParameter("CreateTime", DbType.DateTime, _ExamSignUp_DelOB.CreateTime));
            p.Add(db.CreateParameter("ModifyPersonID", DbType.Int64, _ExamSignUp_DelOB.ModifyPersonID));
            p.Add(db.CreateParameter("ModifyTime", DbType.DateTime, _ExamSignUp_DelOB.ModifyTime));
            p.Add(db.CreateParameter("SKILLLEVEL", DbType.String, _ExamSignUp_DelOB.SKILLLEVEL));
            p.Add(db.CreateParameter("S_SEX", DbType.String, _ExamSignUp_DelOB.S_SEX));
            p.Add(db.CreateParameter("S_CULTURALLEVEL", DbType.String, _ExamSignUp_DelOB.S_CULTURALLEVEL));
            p.Add(db.CreateParameter("S_PHONE", DbType.String, _ExamSignUp_DelOB.S_PHONE));
            p.Add(db.CreateParameter("S_BIRTHDAY", DbType.DateTime, _ExamSignUp_DelOB.S_BIRTHDAY));
            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            _ExamSignUp_DelOB.ExamSignUpDelID = Convert.ToInt64(p[0].Value);
            return rtn;
        }

        /// <summary>
        /// 保存报名表删除历史
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="ExamSignUpID">报名ID</param>
        /// <param name="DeleteMan">删除人</param>
        /// <param name="DeleteTime">删除时间</param>
        /// <returns></returns>
        public static long Insert(DbTransaction tran, long ExamSignUpID,string DeleteMan,DateTime DeleteTime)
        {
            DBHelper db = new DBHelper();

            string sql = @"
			INSERT INTO dbo.ExamSignUp_Del( DeleteMan, DeleteTime,ExamSignUpID,SignUpCode,SignUpDate,WorkerID,UnitID,TrainUnitID,ExamPlanID,WorkStartDate,WorkYearNumer,PersonDetail,HireUnitAdvise,AdminUnitAdvise,CheckCode,CheckResult,CheckMan,CheckDate,PayNoticeCode,PayNoticeResult,PayNoticeMan,PayNoticeDate,PayMoney,PayConfirmCode,PayConfirmRult,PayConfirmMan,PayConfirmDate,FacePhoto,Status,WorkerName,CertificateType,CertificateCode,UnitName,UnitCode,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,SKILLLEVEL,S_SEX,S_CULTURALLEVEL,S_PHONE,S_BIRTHDAY)
			select @DeleteMan, @DeleteTime,ExamSignUpID,SignUpCode,SignUpDate,WorkerID,UnitID,TrainUnitID,ExamPlanID,WorkStartDate,WorkYearNumer,PersonDetail,HireUnitAdvise,AdminUnitAdvise,CheckCode,CheckResult,CheckMan,CheckDate,PayNoticeCode,PayNoticeResult,PayNoticeMan,PayNoticeDate,PayMoney,PayConfirmCode,PayConfirmRult,PayConfirmMan,PayConfirmDate,FacePhoto,Status,WorkerName,CertificateType,CertificateCode,UnitName,UnitCode,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,SKILLLEVEL,S_SEX,S_CULTURALLEVEL,S_PHONE,S_BIRTHDAY from dbo.ExamSignUp
            where ExamSignUpID = @ExamSignUpID";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("DeleteMan", DbType.String, DeleteMan));
            p.Add(db.CreateParameter("DeleteTime", DbType.DateTime, DeleteTime));
            p.Add(db.CreateParameter("ExamSignUpID", DbType.Int64, ExamSignUpID));
            
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="ExamPlanID">考试计划ID</param>
        /// <param name="CreatePersonID">创建人ID</param>
        /// <param name="Status">审核状态</param>
        /// <param name="DeleteMan">删除人</param>
        /// <param name="DeleteTime">删除时间</param>
        /// <returns></returns>
        public static long Insert(DbTransaction tran, long ExamPlanID, long CreatePersonID,string Status, string DeleteMan, DateTime DeleteTime)
        {
            DBHelper db = new DBHelper();

            string sql = @"
			INSERT INTO dbo.ExamSignUp_Del( DeleteMan, DeleteTime,ExamSignUpID,SignUpCode,SignUpDate,WorkerID,UnitID,TrainUnitID,ExamPlanID,WorkStartDate,WorkYearNumer,PersonDetail,HireUnitAdvise,AdminUnitAdvise,CheckCode,CheckResult,CheckMan,CheckDate,PayNoticeCode,PayNoticeResult,PayNoticeMan,PayNoticeDate,PayMoney,PayConfirmCode,PayConfirmRult,PayConfirmMan,PayConfirmDate,FacePhoto,Status,WorkerName,CertificateType,CertificateCode,UnitName,UnitCode,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,SKILLLEVEL,S_SEX,S_CULTURALLEVEL,S_PHONE,S_BIRTHDAY)
			select @DeleteMan, @DeleteTime,ExamSignUpID,SignUpCode,SignUpDate,WorkerID,UnitID,TrainUnitID,ExamPlanID,WorkStartDate,WorkYearNumer,PersonDetail,HireUnitAdvise,AdminUnitAdvise,CheckCode,CheckResult,CheckMan,CheckDate,PayNoticeCode,PayNoticeResult,PayNoticeMan,PayNoticeDate,PayMoney,PayConfirmCode,PayConfirmRult,PayConfirmMan,PayConfirmDate,FacePhoto,Status,WorkerName,CertificateType,CertificateCode,UnitName,UnitCode,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,SKILLLEVEL,S_SEX,S_CULTURALLEVEL,S_PHONE,S_BIRTHDAY from dbo.ExamSignUp
            WHERE ExamPlanID = @ExamPlanID and CreatePersonID = @CreatePersonID and Status=@Status";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("DeleteMan", DbType.String, DeleteMan));
            p.Add(db.CreateParameter("DeleteTime", DbType.DateTime, DeleteTime));
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, ExamPlanID));
            p.Add(db.CreateParameter("CreatePersonID", DbType.Int64, CreatePersonID));
            p.Add(db.CreateParameter("Status", DbType.String, Status));

            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ExamSignUpID">主键</param>
        public static ExamSignUp_DelOB GetObject(long? ExamSignUpID)
        {
            string sql = @"
			SELECT ExamSignUpDelID, DeleteMan, DeleteTime,ExamSignUpID,SignUpCode,SignUpDate,WorkerID,UnitID,TrainUnitID,ExamPlanID,WorkStartDate,WorkYearNumer,PersonDetail,HireUnitAdvise,AdminUnitAdvise,CheckCode,CheckResult,CheckMan,CheckDate,PayNoticeCode,PayNoticeResult,PayNoticeMan,PayNoticeDate,PayMoney,PayConfirmCode,PayConfirmRult,PayConfirmMan,PayConfirmDate,FacePhoto,Status,WorkerName,CertificateType,CertificateCode,UnitName,UnitCode,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,SKILLLEVEL,S_SEX,S_CULTURALLEVEL,S_PHONE,S_BIRTHDAY
			FROM dbo.ExamSignUp_Del
			WHERE ExamSignUpID = @ExamSignUpID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamSignUpID", DbType.Int64, ExamSignUpID));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    ExamSignUp_DelOB _ExamSignUp_DelOB = null;
                    if (reader.Read())
                    {
                        _ExamSignUp_DelOB = new ExamSignUp_DelOB();
                        if (reader["ExamSignUpDelID"] != DBNull.Value) _ExamSignUp_DelOB.ExamSignUpDelID = Convert.ToInt64(reader["ExamSignUpDelID"]);
                        if (reader["DeleteMan"] != DBNull.Value) _ExamSignUp_DelOB.DeleteMan = Convert.ToString(reader["DeleteMan"]);
                        if (reader["DeleteTime"] != DBNull.Value) _ExamSignUp_DelOB.DeleteTime = Convert.ToDateTime(reader["DeleteTime"]);
                        if (reader["ExamSignUpID"] != DBNull.Value) _ExamSignUp_DelOB.ExamSignUpID = Convert.ToInt64(reader["ExamSignUpID"]);
                        if (reader["SignUpCode"] != DBNull.Value) _ExamSignUp_DelOB.SignUpCode = Convert.ToString(reader["SignUpCode"]);
                        if (reader["SignUpDate"] != DBNull.Value) _ExamSignUp_DelOB.SignUpDate = Convert.ToDateTime(reader["SignUpDate"]);                     
                        if (reader["WorkerID"] != DBNull.Value) _ExamSignUp_DelOB.WorkerID = Convert.ToInt64(reader["WorkerID"]);
                        if (reader["UnitID"] != DBNull.Value) _ExamSignUp_DelOB.UnitID = Convert.ToInt64(reader["UnitID"]);
                        if (reader["TrainUnitID"] != DBNull.Value) _ExamSignUp_DelOB.TrainUnitID = Convert.ToInt64(reader["TrainUnitID"]);
                        if (reader["ExamPlanID"] != DBNull.Value) _ExamSignUp_DelOB.ExamPlanID = Convert.ToInt64(reader["ExamPlanID"]);
                        if (reader["WorkStartDate"] != DBNull.Value) _ExamSignUp_DelOB.WorkStartDate = Convert.ToDateTime(reader["WorkStartDate"]);
                        if (reader["WorkYearNumer"] != DBNull.Value) _ExamSignUp_DelOB.WorkYearNumer = Convert.ToInt32(reader["WorkYearNumer"]);
                        if (reader["PersonDetail"] != DBNull.Value) _ExamSignUp_DelOB.PersonDetail = Convert.ToString(reader["PersonDetail"]);
                        if (reader["HireUnitAdvise"] != DBNull.Value) _ExamSignUp_DelOB.HireUnitAdvise = Convert.ToString(reader["HireUnitAdvise"]);
                        if (reader["AdminUnitAdvise"] != DBNull.Value) _ExamSignUp_DelOB.AdminUnitAdvise = Convert.ToString(reader["AdminUnitAdvise"]);
                        if (reader["CheckCode"] != DBNull.Value) _ExamSignUp_DelOB.CheckCode = Convert.ToString(reader["CheckCode"]);
                        if (reader["CheckResult"] != DBNull.Value) _ExamSignUp_DelOB.CheckResult = Convert.ToString(reader["CheckResult"]);
                        if (reader["CheckMan"] != DBNull.Value) _ExamSignUp_DelOB.CheckMan = Convert.ToString(reader["CheckMan"]);
                        if (reader["CheckDate"] != DBNull.Value) _ExamSignUp_DelOB.CheckDate = Convert.ToDateTime(reader["CheckDate"]);
                        if (reader["PayNoticeCode"] != DBNull.Value) _ExamSignUp_DelOB.PayNoticeCode = Convert.ToString(reader["PayNoticeCode"]);
                        if (reader["PayNoticeResult"] != DBNull.Value) _ExamSignUp_DelOB.PayNoticeResult = Convert.ToString(reader["PayNoticeResult"]);
                        if (reader["PayNoticeMan"] != DBNull.Value) _ExamSignUp_DelOB.PayNoticeMan = Convert.ToString(reader["PayNoticeMan"]);
                        if (reader["PayNoticeDate"] != DBNull.Value) _ExamSignUp_DelOB.PayNoticeDate = Convert.ToDateTime(reader["PayNoticeDate"]);
                        if (reader["PayMoney"] != DBNull.Value) _ExamSignUp_DelOB.PayMoney = Convert.ToDecimal(reader["PayMoney"]);
                        if (reader["PayConfirmCode"] != DBNull.Value) _ExamSignUp_DelOB.PayConfirmCode = Convert.ToString(reader["PayConfirmCode"]);
                        if (reader["PayConfirmRult"] != DBNull.Value) _ExamSignUp_DelOB.PayConfirmRult = Convert.ToString(reader["PayConfirmRult"]);
                        if (reader["PayConfirmMan"] != DBNull.Value) _ExamSignUp_DelOB.PayConfirmMan = Convert.ToString(reader["PayConfirmMan"]);
                        if (reader["PayConfirmDate"] != DBNull.Value) _ExamSignUp_DelOB.PayConfirmDate = Convert.ToDateTime(reader["PayConfirmDate"]);
                        if (reader["FacePhoto"] != DBNull.Value) _ExamSignUp_DelOB.FacePhoto = Convert.ToString(reader["FacePhoto"]);
                        if (reader["Status"] != DBNull.Value) _ExamSignUp_DelOB.Status = Convert.ToString(reader["Status"]);
                        if (reader["WorkerName"] != DBNull.Value) _ExamSignUp_DelOB.WorkerName = Convert.ToString(reader["WorkerName"]);
                        if (reader["CertificateType"] != DBNull.Value) _ExamSignUp_DelOB.CertificateType = Convert.ToString(reader["CertificateType"]);
                        if (reader["CertificateCode"] != DBNull.Value) _ExamSignUp_DelOB.CertificateCode = Convert.ToString(reader["CertificateCode"]);
                        if (reader["UnitName"] != DBNull.Value) _ExamSignUp_DelOB.UnitName = Convert.ToString(reader["UnitName"]);
                        if (reader["UnitCode"] != DBNull.Value) _ExamSignUp_DelOB.UnitCode = Convert.ToString(reader["UnitCode"]);
                        if (reader["CreatePersonID"] != DBNull.Value) _ExamSignUp_DelOB.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                        if (reader["CreateTime"] != DBNull.Value) _ExamSignUp_DelOB.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                        if (reader["ModifyPersonID"] != DBNull.Value) _ExamSignUp_DelOB.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                        if (reader["ModifyTime"] != DBNull.Value) _ExamSignUp_DelOB.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                        if (reader["SKILLLEVEL"] != DBNull.Value) _ExamSignUp_DelOB.SKILLLEVEL = Convert.ToString(reader["SKILLLEVEL"]);
                        if (reader["S_SEX"] != DBNull.Value) _ExamSignUp_DelOB.S_SEX = Convert.ToString(reader["S_SEX"]);
                        if (reader["S_CULTURALLEVEL"] != DBNull.Value) _ExamSignUp_DelOB.S_CULTURALLEVEL = Convert.ToString(reader["S_CULTURALLEVEL"]);
                        if (reader["S_PHONE"] != DBNull.Value) _ExamSignUp_DelOB.S_PHONE = Convert.ToString(reader["S_PHONE"]);
                        if (reader["S_BIRTHDAY"] != DBNull.Value) _ExamSignUp_DelOB.S_BIRTHDAY = Convert.ToDateTime(reader["S_BIRTHDAY"]);
                    }
                    reader.Close();
                    db.Close();
                    return _ExamSignUp_DelOB;
                }
            }
            catch (Exception ex)
            {
                db.Close();
                throw ex;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "DBO.VIEW_EXAMSIGNUP_DEL", "*", filterWhereString, orderBy == "" ? " DeleteTime desc,ExamSignUpID" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("DBO.VIEW_EXAMSIGNUP_DEL", filterWhereString);
        }

    }
}
