using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--WorkResultDAL(填写类描述)
	/// </summary>
    public class WorkResultDAL
    {
        public WorkResultDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_WorkResultMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(WorkResultMDL _WorkResultMDL)
		{
		    return Insert(null,_WorkResultMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_WorkResultMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,WorkResultMDL _WorkResultMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.WorkResult(ApplyID,DataNo,DateStart,DateEnd,ProjectName,Job,Cost)
			VALUES (@ApplyID,@DataNo,@DateStart,@DateEnd,@ProjectName,@Job,@Cost);SELECT @DetailID = @@IDENTITY";

		    List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateOutParameter("DetailID", DbType.Int64));
			p.Add(db.CreateParameter("ApplyID",DbType.String, _WorkResultMDL.ApplyID));
			p.Add(db.CreateParameter("DataNo",DbType.Int32, _WorkResultMDL.DataNo));
			p.Add(db.CreateParameter("DateStart",DbType.DateTime, _WorkResultMDL.DateStart));
			p.Add(db.CreateParameter("DateEnd",DbType.DateTime, _WorkResultMDL.DateEnd));
			p.Add(db.CreateParameter("ProjectName",DbType.String, _WorkResultMDL.ProjectName));
			p.Add(db.CreateParameter("Job",DbType.String, _WorkResultMDL.Job));
			p.Add(db.CreateParameter("Cost",DbType.String, _WorkResultMDL.Cost));
			int rtn =  db.GetExcuteNonQuery(tran, sql, p.ToArray());
            _WorkResultMDL.DetailID = Convert.ToInt64(p[0].Value);          
            return rtn;
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_WorkResultMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(WorkResultMDL _WorkResultMDL)
		{
			return Update(null,_WorkResultMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_WorkResultMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,WorkResultMDL _WorkResultMDL)
		{
			string sql = @"
			UPDATE dbo.WorkResult
				SET	ApplyID = @ApplyID,DataNo = @DataNo,DateStart = @DateStart,DateEnd = @DateEnd,ProjectName = @ProjectName,Job = @Job,Cost = @Cost
			WHERE
				DetailID = @DetailID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("DetailID",DbType.Int64, _WorkResultMDL.DetailID));
			p.Add(db.CreateParameter("ApplyID",DbType.String, _WorkResultMDL.ApplyID));
			p.Add(db.CreateParameter("DataNo",DbType.Int32, _WorkResultMDL.DataNo));
			p.Add(db.CreateParameter("DateStart",DbType.DateTime, _WorkResultMDL.DateStart));
			p.Add(db.CreateParameter("DateEnd",DbType.DateTime, _WorkResultMDL.DateEnd));
			p.Add(db.CreateParameter("ProjectName",DbType.String, _WorkResultMDL.ProjectName));
			p.Add(db.CreateParameter("Job",DbType.String, _WorkResultMDL.Job));
			p.Add(db.CreateParameter("Cost",DbType.String, _WorkResultMDL.Cost));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="WorkResultID">主键</param>
		/// <returns></returns>
        public static int Delete( long DetailID )
		{
			return Delete(null, DetailID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="WorkResultID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, long DetailID)
		{
			string sql=@"DELETE FROM dbo.WorkResult WHERE DetailID = @DetailID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("DetailID",DbType.Int64,DetailID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="_WorkResultMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(WorkResultMDL _WorkResultMDL)
		{
			return Delete(null,_WorkResultMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_WorkResultMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,WorkResultMDL _WorkResultMDL)
		{
			string sql=@"DELETE FROM dbo.WorkResult WHERE DetailID = @DetailID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("DetailID",DbType.Int64,_WorkResultMDL.DetailID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="WorkResultID">主键</param>
        public static WorkResultMDL GetObject( long DetailID )
		{
			string sql=@"
			SELECT DetailID,ApplyID,DataNo,DateStart,DateEnd,ProjectName,Job,Cost
			FROM dbo.WorkResult
			WHERE DetailID = @DetailID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("DetailID",DbType.Int64,DetailID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                WorkResultMDL _WorkResultMDL = null;
                if (reader.Read())
                {
                    _WorkResultMDL = new WorkResultMDL();
					if (reader["DetailID"] != DBNull.Value) _WorkResultMDL.DetailID = Convert.ToInt64(reader["DetailID"]);
					if (reader["ApplyID"] != DBNull.Value) _WorkResultMDL.ApplyID = Convert.ToString(reader["ApplyID"]);
					if (reader["DataNo"] != DBNull.Value) _WorkResultMDL.DataNo = Convert.ToInt32(reader["DataNo"]);
					if (reader["DateStart"] != DBNull.Value) _WorkResultMDL.DateStart = Convert.ToDateTime(reader["DateStart"]);
					if (reader["DateEnd"] != DBNull.Value) _WorkResultMDL.DateEnd = Convert.ToDateTime(reader["DateEnd"]);
					if (reader["ProjectName"] != DBNull.Value) _WorkResultMDL.ProjectName = Convert.ToString(reader["ProjectName"]);
					if (reader["Job"] != DBNull.Value) _WorkResultMDL.Job = Convert.ToString(reader["Job"]);
					if (reader["Cost"] != DBNull.Value) _WorkResultMDL.Cost = Convert.ToString(reader["Cost"]);
                }
				reader.Close();
                db.Close();
                return _WorkResultMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.WorkResult", "*", filterWhereString, orderBy == "" ? " DetailID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.WorkResult", filterWhereString);
        }

        /// <summary>
        /// 获取二造续期申请单个人全部工作业绩
        /// </summary>
        /// <param name="ApplyID">申请单ID</param>
        /// <returns></returns>
        public static DataTable GetList(string ApplyID)
        {
            return CommonDAL.GetDataTable(string.Format("select * from dbo.WorkResult where ApplyID='{0}' order by DataNo", ApplyID));
        } 
        
        #region 自定义方法
        
        #endregion
    }
}
