using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

using Model;

namespace DataAccess
{
	/// <summary>
    /// 业务类实现--ExamPlaceAllot_OperationDAL(实操考试考场安排)
	/// </summary>
    public class ExamPlaceAllot_OperationDAL
    {
        public ExamPlaceAllot_OperationDAL() { }       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="ExamPlaceAllotOB">对象实体类</param>
		/// <returns></returns>
        public static long Insert(ExamPlaceAllot_OperationOB o)
		{
            return Insert(null, o);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ExamPlaceAllotOB">对象实体类</param>
		/// <returns></returns>
        public static long Insert(DbTransaction tran, ExamPlaceAllot_OperationOB o)
		{
			DBHelper db = new DBHelper();
		
			string sql = @"
			INSERT INTO dbo.ExamPlaceAllot_Operation(ExamPlanID,ExamPlaceID,ExamPlaceName,RoomNum,ExamPersonNum,Status,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime)
			VALUES (@ExamPlanID,@ExamPlaceID,@ExamPlaceName,@RoomNum,@ExamPersonNum,@Status,@CreatePersonID,@CreateTime,@ModifyPersonID,@ModifyTime);SELECT @ExamPlaceAllotID = @@IDENTITY";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateOutParameter("ExamPlaceAllotID",DbType.Int64));
			p.Add(db.CreateParameter("ExamPlanID",DbType.Int64, o.ExamPlanID));
			p.Add(db.CreateParameter("ExamPlaceID",DbType.Int64, o.ExamPlaceID));
			p.Add(db.CreateParameter("ExamPlaceName",DbType.String, o.ExamPlaceName));
			p.Add(db.CreateParameter("RoomNum",DbType.Int32, o.RoomNum));
			p.Add(db.CreateParameter("ExamPersonNum",DbType.Int32, o.ExamPersonNum));
			p.Add(db.CreateParameter("Status",DbType.String, o.Status));
			p.Add(db.CreateParameter("CreatePersonID",DbType.Int64, o.CreatePersonID));
			p.Add(db.CreateParameter("CreateTime",DbType.DateTime, o.CreateTime));
			p.Add(db.CreateParameter("ModifyPersonID",DbType.Int64, o.ModifyPersonID));
			p.Add(db.CreateParameter("ModifyTime",DbType.DateTime, o.ModifyTime));
            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            o.ExamPlaceAllotID = Convert.ToInt64(p[0].Value);
            return rtn;
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="ExamPlaceAllotOB">对象实体类</param>
		/// <returns></returns>
        public static int Update(ExamPlaceAllot_OperationOB o)
		{
			return Update(null,o);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ExamPlaceAllotOB">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran, ExamPlaceAllot_OperationOB o)
		{
			string sql = @"
			UPDATE dbo.ExamPlaceAllot_Operation
				SET	ExamPlanID = @ExamPlanID,ExamPlaceID = @ExamPlaceID,ExamPlaceName = @ExamPlaceName,RoomNum = @RoomNum,ExamPersonNum = @ExamPersonNum,Status = @Status,CreatePersonID = @CreatePersonID,CreateTime = @CreateTime,ModifyPersonID = @ModifyPersonID,ModifyTime = @ModifyTime
			WHERE
				ExamPlaceAllotID = @ExamPlaceAllotID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ExamPlaceAllotID",DbType.Int64, o.ExamPlaceAllotID));
			p.Add(db.CreateParameter("ExamPlanID",DbType.Int64, o.ExamPlanID));
			p.Add(db.CreateParameter("ExamPlaceID",DbType.Int64, o.ExamPlaceID));
			p.Add(db.CreateParameter("ExamPlaceName",DbType.String, o.ExamPlaceName));
			p.Add(db.CreateParameter("RoomNum",DbType.Int32, o.RoomNum));
			p.Add(db.CreateParameter("ExamPersonNum",DbType.Int32, o.ExamPersonNum));
			p.Add(db.CreateParameter("Status",DbType.String, o.Status));
			p.Add(db.CreateParameter("CreatePersonID",DbType.Int64, o.CreatePersonID));
			p.Add(db.CreateParameter("CreateTime",DbType.DateTime, o.CreateTime));
			p.Add(db.CreateParameter("ModifyPersonID",DbType.Int64, o.ModifyPersonID));
			p.Add(db.CreateParameter("ModifyTime",DbType.DateTime, o.ModifyTime));
			return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="ExamPlaceAllotID">主键</param>
		/// <returns></returns>
        public static int Delete( long ExamPlaceAllotID )
		{
			return Delete(null, ExamPlaceAllotID);
		}
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="ExamPlaceAllotID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, long ExamPlaceAllotID)
		{
            string sql = @"DELETE FROM dbo.ExamPlaceAllot_Operation WHERE ExamPlaceAllotID = @ExamPlaceAllotID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ExamPlaceAllotID",DbType.Int64,ExamPlaceAllotID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="ExamPlaceAllotOB">对象实体类</param>
		/// <returns></returns>
        public static int Delete(ExamPlaceAllot_OperationOB o)
		{
			return Delete(null,o);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ExamPlaceAllotOB">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, ExamPlaceAllot_OperationOB o)
		{
            string sql = @"DELETE FROM dbo.ExamPlaceAllot_Operation WHERE ExamPlaceAllotID = @ExamPlaceAllotID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ExamPlaceAllotID",DbType.Int64,o.ExamPlaceAllotID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ExamPlaceAllotID">主键</param>
        public static ExamPlaceAllot_OperationOB GetObject(long ExamPlaceAllotID)
        {
            string sql = @"
			SELECT ExamPlaceAllotID,ExamPlanID,ExamPlaceID,ExamPlaceName,RoomNum,ExamPersonNum,Status,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime
			FROM dbo.ExamPlaceAllot_Operation
			WHERE ExamPlaceAllotID = @ExamPlaceAllotID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPlaceAllotID", DbType.Int64, ExamPlaceAllotID));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    ExamPlaceAllot_OperationOB _ExamPlaceAllotOB = null;
                    if (reader.Read())
                    {
                        _ExamPlaceAllotOB = new ExamPlaceAllot_OperationOB();
                        if (reader["ExamPlaceAllotID"] != DBNull.Value) _ExamPlaceAllotOB.ExamPlaceAllotID = Convert.ToInt64(reader["ExamPlaceAllotID"]);
                        if (reader["ExamPlanID"] != DBNull.Value) _ExamPlaceAllotOB.ExamPlanID = Convert.ToInt64(reader["ExamPlanID"]);
                        if (reader["ExamPlaceID"] != DBNull.Value) _ExamPlaceAllotOB.ExamPlaceID = Convert.ToInt64(reader["ExamPlaceID"]);
                        if (reader["ExamPlaceName"] != DBNull.Value) _ExamPlaceAllotOB.ExamPlaceName = Convert.ToString(reader["ExamPlaceName"]);
                        if (reader["RoomNum"] != DBNull.Value) _ExamPlaceAllotOB.RoomNum = Convert.ToInt32(reader["RoomNum"]);
                        if (reader["ExamPersonNum"] != DBNull.Value) _ExamPlaceAllotOB.ExamPersonNum = Convert.ToInt32(reader["ExamPersonNum"]);
                        if (reader["Status"] != DBNull.Value) _ExamPlaceAllotOB.Status = Convert.ToString(reader["Status"]);
                        if (reader["CreatePersonID"] != DBNull.Value) _ExamPlaceAllotOB.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                        if (reader["CreateTime"] != DBNull.Value) _ExamPlaceAllotOB.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                        if (reader["ModifyPersonID"] != DBNull.Value) _ExamPlaceAllotOB.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                        if (reader["ModifyTime"] != DBNull.Value) _ExamPlaceAllotOB.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                    }
                    reader.Close();
                    db.Close();
                    return _ExamPlaceAllotOB;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.ExamPlaceAllot_Operation", "*", filterWhereString, orderBy == "" ? " ExamPlaceAllotID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.ExamPlaceAllot_Operation", filterWhereString);
        }

        /// <summary>
        /// 设置考试计划考点状态
        /// </summary>
        /// <param name="ExamPlanID">考试计划ID</param>
        /// <param name="_ExamPlaceAllotStatus">考点状态</param>
        /// <returns></returns>
        public static int UpdateExamPlaceAllotStatus(DbTransaction tran, Int64 ExamPlanID, string _ExamPlaceAllotStatus, Int64 ModifyPersonID, DateTime ModifyTime)
        {
            string sql = @"update dbo.ExamPlaceAllot_Operation 
        set [STATUS] = @STATUS
        ,ModifyPersonID =@ModifyPersonID
        ,ModifyTime=@ModifyTime
        where ExamPlanID = @ExamPlanID";
            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, ExamPlanID));
            p.Add(db.CreateParameter("STATUS", DbType.String, _ExamPlaceAllotStatus));
            p.Add(db.CreateParameter("ModifyPersonID", DbType.Int64, ModifyPersonID));
            p.Add(db.CreateParameter("ModifyTime", DbType.DateTime, ModifyTime));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
    }
}
