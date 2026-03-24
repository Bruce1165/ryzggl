using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

using Model;

namespace DataAccess
{
    /// <summary>
    /// 业务类实现--ExamRoomAllot_OperationDAL(实操考试考室安排)
    /// </summary>
    public class ExamRoomAllot_OperationDAL
    {
        /// <summary>
        /// 考场分配信息
        /// </summary>
        public ExamRoomAllot_OperationDAL() { }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_ExamRoomAllot_OperationOB">对象实体类</param>
        /// <returns></returns>
        public static long Insert(ExamRoomAllot_OperationOB _ExamRoomAllot_OperationOB)
        {
            return Insert(null, _ExamRoomAllot_OperationOB);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_ExamRoomAllot_OperationOB">对象实体类</param>
        /// <returns></returns>
        public static long Insert(DbTransaction tran, ExamRoomAllot_OperationOB _ExamRoomAllot_OperationOB)
        {
            DBHelper db = new DBHelper();

            string sql = @"
			INSERT INTO dbo.ExamRoomAllot_Operation(ExamPlaceAllotID,ExamPlanID,ExamPlaceID,ExamRoomCode,PersonNumber,ExamCardIDFromTo,Status,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,ExamStartTime,ExamEndTime)
			VALUES (@ExamPlaceAllotID,@ExamPlanID,@ExamPlaceID,@ExamRoomCode,@PersonNumber,@ExamCardIDFromTo,@Status,@CreatePersonID,@CreateTime,@ModifyPersonID,@ModifyTime,@ExamStartTime,@ExamEndTime);SELECT @ExamRoomAllotID = @@IDENTITY";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateOutParameter("ExamRoomAllotID", DbType.Int64));
            p.Add(db.CreateParameter("ExamPlaceAllotID", DbType.Int64, _ExamRoomAllot_OperationOB.ExamPlaceAllotID));
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, _ExamRoomAllot_OperationOB.ExamPlanID));
            p.Add(db.CreateParameter("ExamPlaceID", DbType.Int64, _ExamRoomAllot_OperationOB.ExamPlaceID));
            p.Add(db.CreateParameter("ExamRoomCode", DbType.String, _ExamRoomAllot_OperationOB.ExamRoomCode));
            p.Add(db.CreateParameter("PersonNumber", DbType.Int32, _ExamRoomAllot_OperationOB.PersonNumber));
            p.Add(db.CreateParameter("ExamCardIDFromTo", DbType.String, _ExamRoomAllot_OperationOB.ExamCardIDFromTo));
            p.Add(db.CreateParameter("Status", DbType.String, _ExamRoomAllot_OperationOB.Status));
            p.Add(db.CreateParameter("CreatePersonID", DbType.Int64, _ExamRoomAllot_OperationOB.CreatePersonID));
            p.Add(db.CreateParameter("CreateTime", DbType.DateTime, _ExamRoomAllot_OperationOB.CreateTime));
            p.Add(db.CreateParameter("ModifyPersonID", DbType.Int64, _ExamRoomAllot_OperationOB.ModifyPersonID));
            p.Add(db.CreateParameter("ModifyTime", DbType.DateTime, _ExamRoomAllot_OperationOB.ModifyTime));
            p.Add(db.CreateParameter("ExamStartTime", DbType.DateTime, _ExamRoomAllot_OperationOB.ExamStartTime));
            p.Add(db.CreateParameter("ExamEndTime", DbType.DateTime, _ExamRoomAllot_OperationOB.ExamEndTime));
            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            _ExamRoomAllot_OperationOB.ExamRoomAllotID = Convert.ToInt64(p[0].Value);
            return rtn;
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_ExamRoomAllot_OperationOB">对象实体类</param>
        /// <returns></returns>
        public static int Update(ExamRoomAllot_OperationOB _ExamRoomAllot_OperationOB)
        {
            return Update(null, _ExamRoomAllot_OperationOB);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_ExamRoomAllot_OperationOB">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, ExamRoomAllot_OperationOB _ExamRoomAllot_OperationOB)
        {
            string sql = @"
			UPDATE dbo.ExamRoomAllot_Operation
				SET	ExamPlaceAllotID = @ExamPlaceAllotID,ExamPlanID = @ExamPlanID,ExamPlaceID = @ExamPlaceID,ExamRoomCode = @ExamRoomCode,PersonNumber = @PersonNumber,ExamCardIDFromTo = @ExamCardIDFromTo,Status = @Status,CreatePersonID = @CreatePersonID,CreateTime = @CreateTime,ModifyPersonID = @ModifyPersonID,ModifyTime = @ModifyTime,ExamStartTime = @ExamStartTime,ExamEndTime = @ExamEndTime
			WHERE
				ExamRoomAllotID = @ExamRoomAllotID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamRoomAllotID", DbType.Int64, _ExamRoomAllot_OperationOB.ExamRoomAllotID));
            p.Add(db.CreateParameter("ExamPlaceAllotID", DbType.Int64, _ExamRoomAllot_OperationOB.ExamPlaceAllotID));
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, _ExamRoomAllot_OperationOB.ExamPlanID));
            p.Add(db.CreateParameter("ExamPlaceID", DbType.Int64, _ExamRoomAllot_OperationOB.ExamPlaceID));
            p.Add(db.CreateParameter("ExamRoomCode", DbType.String, _ExamRoomAllot_OperationOB.ExamRoomCode));
            p.Add(db.CreateParameter("PersonNumber", DbType.Int32, _ExamRoomAllot_OperationOB.PersonNumber));
            p.Add(db.CreateParameter("ExamCardIDFromTo", DbType.String, _ExamRoomAllot_OperationOB.ExamCardIDFromTo));
            p.Add(db.CreateParameter("Status", DbType.String, _ExamRoomAllot_OperationOB.Status));
            p.Add(db.CreateParameter("CreatePersonID", DbType.Int64, _ExamRoomAllot_OperationOB.CreatePersonID));
            p.Add(db.CreateParameter("CreateTime", DbType.DateTime, _ExamRoomAllot_OperationOB.CreateTime));
            p.Add(db.CreateParameter("ModifyPersonID", DbType.Int64, _ExamRoomAllot_OperationOB.ModifyPersonID));
            p.Add(db.CreateParameter("ModifyTime", DbType.DateTime, _ExamRoomAllot_OperationOB.ModifyTime));
            p.Add(db.CreateParameter("ExamStartTime", DbType.DateTime, _ExamRoomAllot_OperationOB.ExamStartTime));
            p.Add(db.CreateParameter("ExamEndTime", DbType.DateTime, _ExamRoomAllot_OperationOB.ExamEndTime));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 更新考试所有考场编号（大排行）
        /// </summary>
        /// <param name="ExamPlanID">考试计划ID</param>
        /// <returns>影响行数</returns>
        public static int UpdateExamRoomCode(Int64 ExamPlanID)
        {
            string sql = @"
			update dbo.ExamRoomAllot_Operation 
            set ExamRoomCode = cast(t2.rn as varchar(100))
            from 
            (
                select row_number() over(order by examplaceallotid,ExamRoomCode) as rn,t.ExamRoomAllotID from 
	            (
                    select ExamRoomAllotID ,examplaceallotid,cast(ExamRoomCode as int) as ExamRoomCode
                    from dbo.ExamRoomAllot_Operation 
	                where examplanid=@ExamPlanID                     
                ) as t
            ) t2
            where t2.ExamRoomAllotID = dbo.ExamRoomAllot_Operation.ExamRoomAllotID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, ExamPlanID));
            return db.ExcuteNonQuery(sql, p.ToArray());
        }

        /// <summary>
        /// 设置考试计划考场状态
        /// </summary>
        /// <param name="ExamPlanID">考试计划ID</param>
        /// <param name="_ExamRoomAllotStatus">考场状态</param>
        /// <returns></returns>
        public static int UpdateExamRoomStatus(DbTransaction tran, Int64 ExamPlanID, string _ExamRoomAllotStatus, Int64 ModifyPersonID, DateTime ModifyTime)
        {
            string sql = @"update dbo.ExamRoomAllot_Operation 
set [STATUS] = @STATUS
,ExamCardIDFromTo=''
,ModifyPersonID =@ModifyPersonID
,ModifyTime=@ModifyTime
where ExamPlanID = @ExamPlanID";
            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, ExamPlanID));
            p.Add(db.CreateParameter("STATUS", DbType.String, _ExamRoomAllotStatus));
            p.Add(db.CreateParameter("ModifyPersonID", DbType.Int64, ModifyPersonID));
            p.Add(db.CreateParameter("ModifyTime", DbType.DateTime, ModifyTime));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="ExamRoomAllotID">主键</param>
        /// <returns></returns>
        public static int Delete(long ExamRoomAllotID)
        {
            return Delete(null, ExamRoomAllotID);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamRoomAllotID">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, long ExamRoomAllotID)
        {
            string sql = @"DELETE FROM dbo.ExamRoomAllot_Operation WHERE ExamRoomAllotID = @ExamRoomAllotID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamRoomAllotID", DbType.Int64, ExamRoomAllotID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据考点分配ID删除信息
        /// </summary>
        /// <param name="ExamPlaceAllotID">考点分配ID</param>
        /// <returns></returns>
        public static int DeleteByExamPlaceAllotID(long ExamPlaceAllotID)
        {
            return DeleteByExamPlaceAllotID(null, ExamPlaceAllotID);
        }
        /// <summary>
        /// 根据考点分配ID删除信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamPlaceAllotID">考点分配ID</param>
        /// <returns></returns>
        public static int DeleteByExamPlaceAllotID(DbTransaction tran, long ExamPlaceAllotID)
        {
            string sql = @"DELETE FROM dbo.ExamRoomAllot_Operation WHERE ExamPlaceAllotID = @ExamPlaceAllotID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPlaceAllotID", DbType.Int64, ExamPlaceAllotID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="_ExamRoomAllot_OperationOB">对象实体类</param>
        /// <returns></returns>
        public static int Delete(ExamRoomAllot_OperationOB _ExamRoomAllot_OperationOB)
        {
            return Delete(null, _ExamRoomAllot_OperationOB);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_ExamRoomAllot_OperationOB">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, ExamRoomAllot_OperationOB _ExamRoomAllot_OperationOB)
        {
            string sql = @"DELETE FROM dbo.ExamRoomAllot_Operation WHERE ExamRoomAllotID = @ExamRoomAllotID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamRoomAllotID", DbType.Int64, _ExamRoomAllot_OperationOB.ExamRoomAllotID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ExamRoomAllotID">主键</param>
        public static ExamRoomAllot_OperationOB GetObject(long ExamRoomAllotID)
        {
            string sql = @"
			SELECT ExamRoomAllotID,ExamPlaceAllotID,ExamPlanID,ExamPlaceID,ExamRoomCode,PersonNumber,ExamCardIDFromTo,Status,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,ExamStartTime,ExamEndTime
			FROM dbo.ExamRoomAllot_Operation
			WHERE ExamRoomAllotID = @ExamRoomAllotID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamRoomAllotID", DbType.Int64, ExamRoomAllotID));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    ExamRoomAllot_OperationOB _ExamRoomAllot_OperationOB = null;
                    if (reader.Read())
                    {
                        _ExamRoomAllot_OperationOB = new ExamRoomAllot_OperationOB();
                        if (reader["ExamRoomAllotID"] != DBNull.Value) _ExamRoomAllot_OperationOB.ExamRoomAllotID = Convert.ToInt64(reader["ExamRoomAllotID"]);
                        if (reader["ExamPlaceAllotID"] != DBNull.Value) _ExamRoomAllot_OperationOB.ExamPlaceAllotID = Convert.ToInt64(reader["ExamPlaceAllotID"]);
                        if (reader["ExamPlanID"] != DBNull.Value) _ExamRoomAllot_OperationOB.ExamPlanID = Convert.ToInt64(reader["ExamPlanID"]);
                        if (reader["ExamPlaceID"] != DBNull.Value) _ExamRoomAllot_OperationOB.ExamPlaceID = Convert.ToInt64(reader["ExamPlaceID"]);
                        if (reader["ExamRoomCode"] != DBNull.Value) _ExamRoomAllot_OperationOB.ExamRoomCode = Convert.ToString(reader["ExamRoomCode"]);
                        if (reader["PersonNumber"] != DBNull.Value) _ExamRoomAllot_OperationOB.PersonNumber = Convert.ToInt32(reader["PersonNumber"]);
                        if (reader["ExamCardIDFromTo"] != DBNull.Value) _ExamRoomAllot_OperationOB.ExamCardIDFromTo = Convert.ToString(reader["ExamCardIDFromTo"]);
                        if (reader["Status"] != DBNull.Value) _ExamRoomAllot_OperationOB.Status = Convert.ToString(reader["Status"]);
                        if (reader["CreatePersonID"] != DBNull.Value) _ExamRoomAllot_OperationOB.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                        if (reader["CreateTime"] != DBNull.Value) _ExamRoomAllot_OperationOB.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                        if (reader["ModifyPersonID"] != DBNull.Value) _ExamRoomAllot_OperationOB.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                        if (reader["ModifyTime"] != DBNull.Value) _ExamRoomAllot_OperationOB.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                        if (reader["ExamStartTime"] != DBNull.Value) _ExamRoomAllot_OperationOB.ExamStartTime = Convert.ToDateTime(reader["ExamStartTime"]);
                        if (reader["ExamEndTime"] != DBNull.Value) _ExamRoomAllot_OperationOB.ExamEndTime = Convert.ToDateTime(reader["ExamEndTime"]);
                    }
                    reader.Close();
                    db.Close();
                    return _ExamRoomAllot_OperationOB;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.ExamRoomAllot_Operation", "*", filterWhereString, orderBy == "" ? " ExamStartTime,cast(ExamRoomCode as int)" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.ExamRoomAllot_Operation", filterWhereString);
        }

        /// <summary>
        /// 获取考试所有考场最大容纳人数总和
        /// </summary>
        /// <param name="ExamPlanID">考试计划ID</param>
        /// <returns></returns>
        public static int GetSumOfPersonNumber(Int64 ExamPlanID)
        {
            string sql = @"SELECT ISNULL(SUM(PersonNumber),0) FROM dbo.ExamRoomAllot_Operation WHERE ExamPlanID = @ExamPlanID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, ExamPlanID));
            object result = db.ExecuteScalar(sql, p.ToArray());
            return Convert.ToInt32(result);
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.ExamPlaceAllot_Operation inner join dbo.ExamRoomAllot_Operation on dbo.ExamRoomAllot_Operation.ExamPlaceAllotID= dbo.ExamPlaceAllot_Operation.ExamPlaceAllotID ",
                @"dbo.EXAMPLACEALLOT.EXAMPLACEALLOTID,
                    dbo.ExamPlaceAllot_Operation.EXAMPLANID, 
                    dbo.ExamPlaceAllot_Operation.EXAMPLACEID,
                    dbo.ExamPlaceAllot_Operation.EXAMPLACENAME,
                    dbo.ExamPlaceAllot_Operation.ROOMNUM, 
                    dbo.ExamPlaceAllot_Operation.EXAMPERSONNUM,
                    dbo.ExamPlaceAllot_Operation.STATUS, 
                    dbo.ExamPlaceAllot_Operation.CREATEPERSONID,
                    dbo.ExamPlaceAllot_Operation.CREATETIME, 
                    dbo.ExamPlaceAllot_Operation.MODIFYPERSONID,
                    dbo.ExamPlaceAllot_Operation.MODIFYTIME, 
                    dbo.ExamRoomAllot_Operation.EXAMROOMALLOTID,
                    dbo.ExamRoomAllot_Operation.EXAMROOMCODE, 
                    dbo.ExamRoomAllot_Operation.PERSONNUMBER, 
                    dbo.ExamRoomAllot_Operation.EXAMCARDIDFROMTO,
                    dbo.ExamRoomAllot_Operation.ExamStartTime,
                    dbo.ExamRoomAllot_Operation.ExamEndTime",
                filterWhereString, orderBy == "" ? "ExamPlaceAllot_Operation.ExamPlaceAllotID,cast(ExamRoomCode as int)" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountView(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.ExamPlaceAllot_Operation inner join dbo.ExamRoomAllot_Operation on dbo.ExamRoomAllot_Operation.ExamPlaceAllotID= dbo.ExamPlaceAllot_Operation.ExamPlaceAllotID ", filterWhereString);
        }
     
    }
}
