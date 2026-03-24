using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

using Model;

namespace DataAccess
{
    /// <summary>
    /// 业务类实现--ExamResultDAL(填写类描述)
    /// </summary>
    public class ExamResult_OperationDAL
    {
        public ExamResult_OperationDAL() { }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="ExamResultOB">对象实体类</param>
        /// <returns></returns>
        public static long Insert(ExamResult_OperationOB _ExamResultOB)
        {
            return Insert(null, _ExamResultOB);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamResultOB">对象实体类</param>
        /// <returns></returns>
        public static long Insert(DbTransaction tran, ExamResult_OperationOB _ExamResultOB)
        {
            DBHelper db = new DBHelper();

            string sql = @"
			INSERT INTO dbo.ExamResult_Operation(ExamRoomAllotID,ExamPlanID,WorkerID,ExamCardID,ExamResult,Status,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,ExamSignUp_ID)
			VALUES (@ExamRoomAllotID,@ExamPlanID,@WorkerID,@ExamCardID,@ExamResult,@Status,@CreatePersonID,@CreateTime,@ModifyPersonID,@ModifyTime,@ExamSignUp_ID);SELECT @ExamResultID = @@IDENTITY";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateOutParameter("ExamResultID", DbType.Int64));
            p.Add(db.CreateParameter("ExamRoomAllotID", DbType.Int64, _ExamResultOB.ExamRoomAllotID));
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, _ExamResultOB.ExamPlanID));
            p.Add(db.CreateParameter("WorkerID", DbType.Int64, _ExamResultOB.WorkerID));
            p.Add(db.CreateParameter("ExamCardID", DbType.String, _ExamResultOB.ExamCardID));
            p.Add(db.CreateParameter("ExamResult", DbType.String, _ExamResultOB.ExamResult));
            p.Add(db.CreateParameter("Status", DbType.String, _ExamResultOB.Status));
            p.Add(db.CreateParameter("CreatePersonID", DbType.Int64, _ExamResultOB.CreatePersonID));
            p.Add(db.CreateParameter("CreateTime", DbType.DateTime, _ExamResultOB.CreateTime));
            p.Add(db.CreateParameter("ModifyPersonID", DbType.Int64, _ExamResultOB.ModifyPersonID));
            p.Add(db.CreateParameter("ModifyTime", DbType.DateTime, _ExamResultOB.ModifyTime));
            p.Add(db.CreateParameter("ExamSignUp_ID", DbType.Int64, _ExamResultOB.ExamSignUp_ID));
            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            _ExamResultOB.ExamResultID = Convert.ToInt64(p[0].Value);
            return rtn;
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="ExamResultOB">对象实体类</param>
        /// <returns></returns>
        public static int Update(ExamResult_OperationOB _ExamResultOB)
        {
            return Update(null, _ExamResultOB);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamResultOB">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, ExamResult_OperationOB _ExamResultOB)
        {
            string sql = @"
			UPDATE dbo.ExamResult_Operation
				SET	ExamRoomAllotID = @ExamRoomAllotID,ExamPlanID = @ExamPlanID,WorkerID = @WorkerID,ExamCardID = @ExamCardID,ExamResult = @ExamResult,Status = @Status,CreatePersonID = @CreatePersonID,CreateTime = @CreateTime,ModifyPersonID = @ModifyPersonID,ModifyTime = @ModifyTime
			WHERE
				ExamResultID = @ExamResultID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamResultID", DbType.Int64, _ExamResultOB.ExamResultID));
            p.Add(db.CreateParameter("ExamRoomAllotID", DbType.Int64, _ExamResultOB.ExamRoomAllotID));
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, _ExamResultOB.ExamPlanID));
            p.Add(db.CreateParameter("WorkerID", DbType.Int64, _ExamResultOB.WorkerID));
            p.Add(db.CreateParameter("ExamCardID", DbType.String, _ExamResultOB.ExamCardID));
            p.Add(db.CreateParameter("ExamResult", DbType.String, _ExamResultOB.ExamResult));
            p.Add(db.CreateParameter("Status", DbType.String, _ExamResultOB.Status));
            p.Add(db.CreateParameter("CreatePersonID", DbType.Int64, _ExamResultOB.CreatePersonID));
            p.Add(db.CreateParameter("CreateTime", DbType.DateTime, _ExamResultOB.CreateTime));
            p.Add(db.CreateParameter("ModifyPersonID", DbType.Int64, _ExamResultOB.ModifyPersonID));
            p.Add(db.CreateParameter("ModifyTime", DbType.DateTime, _ExamResultOB.ModifyTime));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="ExamResultID">主键</param>
        /// <returns></returns>
        public static int Delete(long ExamResultID)
        {
            return Delete(null, ExamResultID);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamResultID">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, long ExamResultID)
        {
            string sql = @"DELETE FROM dbo.ExamResult_Operation WHERE ExamResultID = @ExamResultID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamResultID", DbType.Int64, ExamResultID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamResultOB">对象实体类</param>
        /// <returns></returns>
        public static int Delete(ExamResultOB _ExamResultOB)
        {
            return Delete(null, _ExamResultOB);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamResultOB">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, ExamResultOB _ExamResultOB)
        {
            string sql = @"DELETE FROM dbo.ExamResult_Operation WHERE ExamResultID = @ExamResultID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamResultID", DbType.Int64, _ExamResultOB.ExamResultID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 删除考试计划已分配的准考证
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_ExamPlanID">考试计划ID</param>
        /// <returns></returns>
        public static int DeleteByExamPlanID(DbTransaction tran, long _ExamPlanID)
        {
            string sql = @"DELETE FROM dbo.ExamResult_Operation WHERE ExamPlanID = @ExamPlanID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, _ExamPlanID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ExamResultID">主键</param>
        public static ExamResult_OperationOB GetObject(long ExamResultID)
        {
            string sql = @"
			SELECT ExamResultID,ExamRoomAllotID,ExamPlanID,WorkerID,ExamCardID,ExamResult,Status,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,ExamSignUp_ID
			FROM dbo.ExamResult_Operation
			WHERE ExamResultID = @ExamResultID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamResultID", DbType.Int64, ExamResultID));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    ExamResult_OperationOB _ExamResultOB = null;
                    if (reader.Read())
                    {
                        _ExamResultOB = new ExamResult_OperationOB();
                        if (reader["ExamResultID"] != DBNull.Value) _ExamResultOB.ExamResultID = Convert.ToInt64(reader["ExamResultID"]);
                        if (reader["ExamRoomAllotID"] != DBNull.Value) _ExamResultOB.ExamRoomAllotID = Convert.ToInt64(reader["ExamRoomAllotID"]);
                        if (reader["ExamPlanID"] != DBNull.Value) _ExamResultOB.ExamPlanID = Convert.ToInt64(reader["ExamPlanID"]);
                        if (reader["WorkerID"] != DBNull.Value) _ExamResultOB.WorkerID = Convert.ToInt64(reader["WorkerID"]);
                        if (reader["ExamCardID"] != DBNull.Value) _ExamResultOB.ExamCardID = Convert.ToString(reader["ExamCardID"]);
                        if (reader["ExamResult"] != DBNull.Value) _ExamResultOB.ExamResult = Convert.ToString(reader["ExamResult"]);
                        if (reader["Status"] != DBNull.Value) _ExamResultOB.Status = Convert.ToString(reader["Status"]);
                        if (reader["CreatePersonID"] != DBNull.Value) _ExamResultOB.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                        if (reader["CreateTime"] != DBNull.Value) _ExamResultOB.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                        if (reader["ModifyPersonID"] != DBNull.Value) _ExamResultOB.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                        if (reader["ModifyTime"] != DBNull.Value) _ExamResultOB.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                        if (reader["ExamSignUp_ID"] != DBNull.Value) _ExamResultOB.ExamSignUp_ID = Convert.ToInt64(reader["ExamSignUp_ID"]);
                    }
                    reader.Close();
                    db.Close();
                    return _ExamResultOB;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.ExamResult_Operation", "*", filterWhereString, orderBy == "" ? " ExamResultID" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.ExamResult_Operation", filterWhereString);
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
        public static DataTable GetListView(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.View_ExamResult_Operation", "*", filterWhereString, orderBy == "" ? "ExamPlanID desc,ExamCardID" : orderBy);
        }

        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountView(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.View_ExamResult_Operation", filterWhereString);
        }

        /// <summary>
        /// 获取查询结果集中考试计划ID
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>只有一个考试计划返回ID，没有查到返回0，多个考试计划返回-1</returns>
        public static long GetExamPlanID(string filterWhereString)
        {
            DataTable dt = CommonDAL.GetDataTable( string.Format("select distinct ExamPlanID from dbo.View_ExamResult_Operation where 1=1 ", filterWhereString));
            if (dt == null || dt.Rows.Count == 0) return 0;
            if (dt.Rows.Count > 1) return -1;
            return Convert.ToInt64(dt.Rows[0]["ExamPlanID"]);
        }


    }
}
