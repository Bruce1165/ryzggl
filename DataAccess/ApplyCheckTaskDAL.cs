using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--ApplyCheckTaskDAL(填写类描述)
	/// </summary>
    public class ApplyCheckTaskDAL
    {
        public ApplyCheckTaskDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_ApplyCheckTaskMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(ApplyCheckTaskMDL _ApplyCheckTaskMDL)
		{
		    return Insert(null,_ApplyCheckTaskMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_ApplyCheckTaskMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,ApplyCheckTaskMDL _ApplyCheckTaskMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.ApplyCheckTask(cjr,cjsj,BusRange,BusRangeCode,BusStartDate,BusEndDate,CheckPer,ItemCount)
			VALUES (@cjr,@cjsj,@BusRange,@BusRangeCode,@BusStartDate,@BusEndDate,@CheckPer,@ItemCount);SELECT @TaskID = @@IDENTITY";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateOutParameter("TaskID",DbType.Int64));
			p.Add(db.CreateParameter("cjr",DbType.String, _ApplyCheckTaskMDL.cjr));
			p.Add(db.CreateParameter("cjsj",DbType.DateTime, _ApplyCheckTaskMDL.cjsj));
			p.Add(db.CreateParameter("BusRange",DbType.String, _ApplyCheckTaskMDL.BusRange));
			p.Add(db.CreateParameter("BusRangeCode",DbType.String, _ApplyCheckTaskMDL.BusRangeCode));
			p.Add(db.CreateParameter("BusStartDate",DbType.DateTime, _ApplyCheckTaskMDL.BusStartDate));
			p.Add(db.CreateParameter("BusEndDate",DbType.DateTime, _ApplyCheckTaskMDL.BusEndDate));
			p.Add(db.CreateParameter("CheckPer",DbType.Int32, _ApplyCheckTaskMDL.CheckPer));
			p.Add(db.CreateParameter("ItemCount",DbType.Int32, _ApplyCheckTaskMDL.ItemCount));

            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            _ApplyCheckTaskMDL.TaskID = Convert.ToInt64(p[0].Value);
            return rtn;
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_ApplyCheckTaskMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(ApplyCheckTaskMDL _ApplyCheckTaskMDL)
		{
			return Update(null,_ApplyCheckTaskMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_ApplyCheckTaskMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,ApplyCheckTaskMDL _ApplyCheckTaskMDL)
		{
			string sql = @"
			UPDATE dbo.ApplyCheckTask
				SET	cjr = @cjr,cjsj = @cjsj,BusRange = @BusRange,BusRangeCode = @BusRangeCode,BusStartDate = @BusStartDate,BusEndDate = @BusEndDate,CheckPer = @CheckPer,ItemCount = @ItemCount
			WHERE
				TaskID = @TaskID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("TaskID",DbType.Int64, _ApplyCheckTaskMDL.TaskID));
			p.Add(db.CreateParameter("cjr",DbType.String, _ApplyCheckTaskMDL.cjr));
			p.Add(db.CreateParameter("cjsj",DbType.DateTime, _ApplyCheckTaskMDL.cjsj));
			p.Add(db.CreateParameter("BusRange",DbType.String, _ApplyCheckTaskMDL.BusRange));
			p.Add(db.CreateParameter("BusRangeCode",DbType.String, _ApplyCheckTaskMDL.BusRangeCode));
			p.Add(db.CreateParameter("BusStartDate",DbType.DateTime, _ApplyCheckTaskMDL.BusStartDate));
			p.Add(db.CreateParameter("BusEndDate",DbType.DateTime, _ApplyCheckTaskMDL.BusEndDate));
			p.Add(db.CreateParameter("CheckPer",DbType.Int32, _ApplyCheckTaskMDL.CheckPer));
			p.Add(db.CreateParameter("ItemCount",DbType.Int32, _ApplyCheckTaskMDL.ItemCount));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="ApplyCheckTaskID">主键</param>
		/// <returns></returns>
        public static int Delete( long TaskID )
		{
			return Delete(null, TaskID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="ApplyCheckTaskID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, long TaskID)
		{
			string sql=@"DELETE FROM dbo.ApplyCheckTask WHERE TaskID = @TaskID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("TaskID",DbType.Int64,TaskID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="_ApplyCheckTaskMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(ApplyCheckTaskMDL _ApplyCheckTaskMDL)
		{
			return Delete(null,_ApplyCheckTaskMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_ApplyCheckTaskMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,ApplyCheckTaskMDL _ApplyCheckTaskMDL)
		{
			string sql=@"DELETE FROM dbo.ApplyCheckTask WHERE TaskID = @TaskID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("TaskID",DbType.Int64,_ApplyCheckTaskMDL.TaskID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ApplyCheckTaskID">主键</param>
        public static ApplyCheckTaskMDL GetObject( long TaskID )
		{
			string sql=@"
			SELECT TaskID,cjr,cjsj,BusRange,BusRangeCode,BusStartDate,BusEndDate,CheckPer,ItemCount
			FROM dbo.ApplyCheckTask
			WHERE TaskID = @TaskID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("TaskID",DbType.Int64,TaskID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                ApplyCheckTaskMDL _ApplyCheckTaskMDL = null;
                if (reader.Read())
                {
                    _ApplyCheckTaskMDL = new ApplyCheckTaskMDL();
					if (reader["TaskID"] != DBNull.Value) _ApplyCheckTaskMDL.TaskID = Convert.ToInt64(reader["TaskID"]);
					if (reader["cjr"] != DBNull.Value) _ApplyCheckTaskMDL.cjr = Convert.ToString(reader["cjr"]);
					if (reader["cjsj"] != DBNull.Value) _ApplyCheckTaskMDL.cjsj = Convert.ToDateTime(reader["cjsj"]);
					if (reader["BusRange"] != DBNull.Value) _ApplyCheckTaskMDL.BusRange = Convert.ToString(reader["BusRange"]);
					if (reader["BusRangeCode"] != DBNull.Value) _ApplyCheckTaskMDL.BusRangeCode = Convert.ToString(reader["BusRangeCode"]);
					if (reader["BusStartDate"] != DBNull.Value) _ApplyCheckTaskMDL.BusStartDate = Convert.ToDateTime(reader["BusStartDate"]);
					if (reader["BusEndDate"] != DBNull.Value) _ApplyCheckTaskMDL.BusEndDate = Convert.ToDateTime(reader["BusEndDate"]);
					if (reader["CheckPer"] != DBNull.Value) _ApplyCheckTaskMDL.CheckPer = Convert.ToInt32(reader["CheckPer"]);
					if (reader["ItemCount"] != DBNull.Value) _ApplyCheckTaskMDL.ItemCount = Convert.ToInt32(reader["ItemCount"]);
                }
				reader.Close();
                db.Close();
                return _ApplyCheckTaskMDL;
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
		public static DataTable GetList(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.ApplyCheckTask", "*,CheckCount=(select count([CheckTime]) from [ApplyCheckTaskItem] where [TaskID]=[ApplyCheckTask].[TaskID])", filterWhereString, orderBy == "" ? " TaskID desc" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.ApplyCheckTask", filterWhereString);
        }
        
        #region 自定义方法

        /// <summary>
        /// 更新抽查实际记录数
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="TaskID">抽查ID</param>
        /// <returns></returns>
        public static int UpdateItemCount(DbTransaction tran, long TaskID)
        {
            string sql = @"
			UPDATE dbo.ApplyCheckTask
				SET	ItemCount = (select count(*) from ApplyCheckTaskItem where TaskID = @TaskID)
			WHERE
				TaskID = @TaskID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("TaskID", DbType.Int64, TaskID));          
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 获取抽查任务已审核记录数量
        /// </summary>
        /// <param name="TaskID">抽查任务ID</param>
        /// <returns>审核记录数量</returns>
         public static int SelectCheckCount(long TaskID)
        {
            object rtn= CommonDAL.GetObject(string.Format(@"select isnull(count([CheckTime]),0) checkCount
                                                          FROM [dbo].[ApplyCheckTaskItem]
                                                          where [TaskID] ={0}", TaskID));
             return Convert.ToInt32(rtn);
        }

        

        #endregion
    }
}
