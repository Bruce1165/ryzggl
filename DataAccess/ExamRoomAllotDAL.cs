using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

using Model;

namespace DataAccess
{
    /// <summary>
    /// 业务类实现--ExamRoomAllotDAL(填写类描述)
    /// </summary>
    public class ExamRoomAllotDAL
    {
        public ExamRoomAllotDAL() { }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="ExamRoomAllotOB">对象实体类</param>
        /// <returns></returns>
        public static long Insert(ExamRoomAllotOB _ExamRoomAllotOB)
        {
            return Insert(null, _ExamRoomAllotOB);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamRoomAllotOB">对象实体类</param>
        /// <returns></returns>
        public static long Insert(DbTransaction tran, ExamRoomAllotOB _ExamRoomAllotOB)
        {
            DBHelper db = new DBHelper();

            string sql = @"
			INSERT INTO dbo.ExamRoomAllot(ExamPlaceAllotID,ExamPlanID,ExamPlaceID,ExamRoomCode,PersonNumber,ExamCardIDFromTo,Status,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,ExamStartTime,ExamEndTime)
			VALUES (@ExamPlaceAllotID,@ExamPlanID,@ExamPlaceID,@ExamRoomCode,@PersonNumber,@ExamCardIDFromTo,@Status,@CreatePersonID,@CreateTime,@ModifyPersonID,@ModifyTime,@ExamStartTime,@ExamEndTime);SELECT @ExamRoomAllotID = @@IDENTITY";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateOutParameter("ExamRoomAllotID", DbType.Int64));
            p.Add(db.CreateParameter("ExamPlaceAllotID", DbType.Int64, _ExamRoomAllotOB.ExamPlaceAllotID));
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, _ExamRoomAllotOB.ExamPlanID));
            p.Add(db.CreateParameter("ExamPlaceID", DbType.Int64, _ExamRoomAllotOB.ExamPlaceID));
            p.Add(db.CreateParameter("ExamRoomCode", DbType.String, _ExamRoomAllotOB.ExamRoomCode));
            p.Add(db.CreateParameter("PersonNumber", DbType.Int32, _ExamRoomAllotOB.PersonNumber));
            p.Add(db.CreateParameter("ExamCardIDFromTo", DbType.String, _ExamRoomAllotOB.ExamCardIDFromTo));
            p.Add(db.CreateParameter("Status", DbType.String, _ExamRoomAllotOB.Status));
            p.Add(db.CreateParameter("CreatePersonID", DbType.Int64, _ExamRoomAllotOB.CreatePersonID));
            p.Add(db.CreateParameter("CreateTime", DbType.DateTime, _ExamRoomAllotOB.CreateTime));
            p.Add(db.CreateParameter("ModifyPersonID", DbType.Int64, _ExamRoomAllotOB.ModifyPersonID));
            p.Add(db.CreateParameter("ModifyTime", DbType.DateTime, _ExamRoomAllotOB.ModifyTime));
            p.Add(db.CreateParameter("ExamStartTime", DbType.DateTime, _ExamRoomAllotOB.ExamStartTime));
            p.Add(db.CreateParameter("ExamEndTime", DbType.DateTime, _ExamRoomAllotOB.ExamEndTime));
            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            _ExamRoomAllotOB.ExamRoomAllotID = Convert.ToInt64(p[0].Value);
            return rtn;
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="ExamRoomAllotOB">对象实体类</param>
        /// <returns></returns>
        public static int Update(ExamRoomAllotOB _ExamRoomAllotOB)
        {
            return Update(null, _ExamRoomAllotOB);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamRoomAllotOB">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, ExamRoomAllotOB _ExamRoomAllotOB)
        {
            string sql = @"
			UPDATE dbo.ExamRoomAllot
				SET	ExamPlaceAllotID = @ExamPlaceAllotID,ExamPlanID = @ExamPlanID,ExamPlaceID = @ExamPlaceID,ExamRoomCode = @ExamRoomCode,PersonNumber = @PersonNumber,ExamCardIDFromTo = @ExamCardIDFromTo,Status = @Status,CreatePersonID = @CreatePersonID,CreateTime = @CreateTime,ModifyPersonID = @ModifyPersonID,ModifyTime = @ModifyTime,ExamStartTime = @ExamStartTime,ExamEndTime = @ExamEndTime
			WHERE
				ExamRoomAllotID = @ExamRoomAllotID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamRoomAllotID", DbType.Int64, _ExamRoomAllotOB.ExamRoomAllotID));
            p.Add(db.CreateParameter("ExamPlaceAllotID", DbType.Int64, _ExamRoomAllotOB.ExamPlaceAllotID));
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, _ExamRoomAllotOB.ExamPlanID));
            p.Add(db.CreateParameter("ExamPlaceID", DbType.Int64, _ExamRoomAllotOB.ExamPlaceID));
            p.Add(db.CreateParameter("ExamRoomCode", DbType.String, _ExamRoomAllotOB.ExamRoomCode));
            p.Add(db.CreateParameter("PersonNumber", DbType.Int32, _ExamRoomAllotOB.PersonNumber));
            p.Add(db.CreateParameter("ExamCardIDFromTo", DbType.String, _ExamRoomAllotOB.ExamCardIDFromTo));
            p.Add(db.CreateParameter("Status", DbType.String, _ExamRoomAllotOB.Status));
            p.Add(db.CreateParameter("CreatePersonID", DbType.Int64, _ExamRoomAllotOB.CreatePersonID));
            p.Add(db.CreateParameter("CreateTime", DbType.DateTime, _ExamRoomAllotOB.CreateTime));
            p.Add(db.CreateParameter("ModifyPersonID", DbType.Int64, _ExamRoomAllotOB.ModifyPersonID));
            p.Add(db.CreateParameter("ModifyTime", DbType.DateTime, _ExamRoomAllotOB.ModifyTime));
            p.Add(db.CreateParameter("ExamStartTime", DbType.DateTime, _ExamRoomAllotOB.ExamStartTime));
            p.Add(db.CreateParameter("ExamEndTime", DbType.DateTime, _ExamRoomAllotOB.ExamEndTime));
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
			update dbo.ExamRoomAllot 
            set ExamRoomCode = cast(t2.rn as varchar(100))
            from 
            (
                select row_number() over(order by examplaceallotid,ExamRoomCode) as rn,t.ExamRoomAllotID from 
	            (
                    select ExamRoomAllotID ,examplaceallotid,cast(ExamRoomCode as int) as ExamRoomCode
                    from dbo.ExamRoomAllot 
	                where examplanid=@ExamPlanID                     
                ) as t
            ) t2
            where t2.ExamRoomAllotID = dbo.ExamRoomAllot.ExamRoomAllotID";

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
            string sql = @"update dbo.ExamRoomAllot 
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
            string sql = @"DELETE FROM dbo.ExamRoomAllot WHERE ExamRoomAllotID = @ExamRoomAllotID";

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
            string sql = @"DELETE FROM dbo.ExamRoomAllot WHERE ExamPlaceAllotID = @ExamPlaceAllotID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPlaceAllotID", DbType.Int64, ExamPlaceAllotID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamRoomAllotOB">对象实体类</param>
        /// <returns></returns>
        public static int Delete(ExamRoomAllotOB _ExamRoomAllotOB)
        {
            return Delete(null, _ExamRoomAllotOB);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamRoomAllotOB">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, ExamRoomAllotOB _ExamRoomAllotOB)
        {
            string sql = @"DELETE FROM dbo.ExamRoomAllot WHERE ExamRoomAllotID = @ExamRoomAllotID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamRoomAllotID", DbType.Int64, _ExamRoomAllotOB.ExamRoomAllotID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ExamRoomAllotID">主键</param>
        public static ExamRoomAllotOB GetObject(long ExamRoomAllotID)
        {
            string sql = @"
			SELECT ExamRoomAllotID,ExamPlaceAllotID,ExamPlanID,ExamPlaceID,ExamRoomCode,PersonNumber,ExamCardIDFromTo,Status,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,ExamStartTime,ExamEndTime
			FROM dbo.ExamRoomAllot
			WHERE ExamRoomAllotID = @ExamRoomAllotID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamRoomAllotID", DbType.Int64, ExamRoomAllotID));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    ExamRoomAllotOB _ExamRoomAllotOB = null;
                    if (reader.Read())
                    {
                        _ExamRoomAllotOB = new ExamRoomAllotOB();
                        if (reader["ExamRoomAllotID"] != DBNull.Value) _ExamRoomAllotOB.ExamRoomAllotID = Convert.ToInt64(reader["ExamRoomAllotID"]);
                        if (reader["ExamPlaceAllotID"] != DBNull.Value) _ExamRoomAllotOB.ExamPlaceAllotID = Convert.ToInt64(reader["ExamPlaceAllotID"]);
                        if (reader["ExamPlanID"] != DBNull.Value) _ExamRoomAllotOB.ExamPlanID = Convert.ToInt64(reader["ExamPlanID"]);
                        if (reader["ExamPlaceID"] != DBNull.Value) _ExamRoomAllotOB.ExamPlaceID = Convert.ToInt64(reader["ExamPlaceID"]);
                        if (reader["ExamRoomCode"] != DBNull.Value) _ExamRoomAllotOB.ExamRoomCode = Convert.ToString(reader["ExamRoomCode"]);
                        if (reader["PersonNumber"] != DBNull.Value) _ExamRoomAllotOB.PersonNumber = Convert.ToInt32(reader["PersonNumber"]);
                        if (reader["ExamCardIDFromTo"] != DBNull.Value) _ExamRoomAllotOB.ExamCardIDFromTo = Convert.ToString(reader["ExamCardIDFromTo"]);
                        if (reader["Status"] != DBNull.Value) _ExamRoomAllotOB.Status = Convert.ToString(reader["Status"]);
                        if (reader["CreatePersonID"] != DBNull.Value) _ExamRoomAllotOB.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                        if (reader["CreateTime"] != DBNull.Value) _ExamRoomAllotOB.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                        if (reader["ModifyPersonID"] != DBNull.Value) _ExamRoomAllotOB.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                        if (reader["ModifyTime"] != DBNull.Value) _ExamRoomAllotOB.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                        if (reader["ExamStartTime"] != DBNull.Value) _ExamRoomAllotOB.ExamStartTime = Convert.ToDateTime(reader["ExamStartTime"]);
                        if (reader["ExamEndTime"] != DBNull.Value) _ExamRoomAllotOB.ExamEndTime = Convert.ToDateTime(reader["ExamEndTime"]);
                    }
                    reader.Close();
                    db.Close();
                    return _ExamRoomAllotOB;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.ExamRoomAllot", "*", filterWhereString, orderBy == "" ? "  ExamStartTime,cast(ExamRoomCode as int)" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.ExamRoomAllot", filterWhereString);
        }

        /// <summary>
        /// 获取考试所有考场最大容纳人数总和
        /// </summary>
        /// <param name="ExamPlanID">考试计划ID</param>
        /// <returns></returns>
        public static int GetSumOfPersonNumber(Int64 ExamPlanID)
        {
            string sql = @"SELECT ISNULL(SUM(PersonNumber),0) FROM dbo.ExamRoomAllot WHERE ExamPlanID = @ExamPlanID";

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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.ExamPlaceAllot inner join dbo.ExamRoomAllot on dbo.ExamRoomAllot.ExamPlaceAllotID= dbo.ExamPlaceAllot.ExamPlaceAllotID ",
                @"dbo.EXAMPLACEALLOT.EXAMPLACEALLOTID,
                    dbo.EXAMPLACEALLOT.EXAMPLANID, 
                    dbo.EXAMPLACEALLOT.EXAMPLACEID,
                    dbo.EXAMPLACEALLOT.EXAMPLACENAME,
                    dbo.EXAMPLACEALLOT.ROOMNUM, 
                    dbo.EXAMPLACEALLOT.EXAMPERSONNUM,
                    dbo.EXAMPLACEALLOT.STATUS, 
                    dbo.EXAMPLACEALLOT.CREATEPERSONID,
                    dbo.EXAMPLACEALLOT.CREATETIME, 
                    dbo.EXAMPLACEALLOT.MODIFYPERSONID,
                    dbo.EXAMPLACEALLOT.MODIFYTIME, 
                    dbo.EXAMROOMALLOT.EXAMROOMALLOTID,
                    dbo.EXAMROOMALLOT.EXAMROOMCODE, 
                    dbo.EXAMROOMALLOT.PERSONNUMBER, 
                    dbo.EXAMROOMALLOT.EXAMCARDIDFROMTO,
                    dbo.EXAMROOMALLOT.ExamStartTime,
                    dbo.EXAMROOMALLOT.ExamEndTime",
                filterWhereString, orderBy == "" ? "ExamPlaceAllot.ExamPlaceAllotID,EXAMROOMALLOT.ExamStartTime,cast(EXAMROOMALLOT.ExamRoomCode as int)" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountView(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.ExamPlaceAllot inner join dbo.ExamRoomAllot on dbo.ExamRoomAllot.ExamPlaceAllotID= dbo.ExamPlaceAllot.ExamPlaceAllotID ", filterWhereString);
        }

    }
}
