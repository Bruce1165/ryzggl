using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
    /// 业务类实现--TJ_MissExamLockDAL(填写类描述)
    /// 缺考锁定数据物理化：作业【考务每日作业任务】
	/// </summary>
    public class TJ_MissExamLockDAL
    {
        public TJ_MissExamLockDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_TJ_MissExamLockMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(TJ_MissExamLockMDL _TJ_MissExamLockMDL)
		{
		    return Insert(null,_TJ_MissExamLockMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_TJ_MissExamLockMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,TJ_MissExamLockMDL _TJ_MissExamLockMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.TJ_MissExamLock(WorkerCertificateCode,WorkerName,FirstExamDate,LockStartDate,LockEndDate)
			VALUES (@WorkerCertificateCode,@WorkerName,@FirstExamDate,@LockStartDate,@LockEndDate)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("WorkerCertificateCode",DbType.String, _TJ_MissExamLockMDL.WorkerCertificateCode));
			p.Add(db.CreateParameter("WorkerName",DbType.String, _TJ_MissExamLockMDL.WorkerName));
			p.Add(db.CreateParameter("FirstExamDate",DbType.DateTime, _TJ_MissExamLockMDL.FirstExamDate));
			p.Add(db.CreateParameter("LockStartDate",DbType.DateTime, _TJ_MissExamLockMDL.LockStartDate));
			p.Add(db.CreateParameter("LockEndDate",DbType.DateTime, _TJ_MissExamLockMDL.LockEndDate));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_TJ_MissExamLockMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(TJ_MissExamLockMDL _TJ_MissExamLockMDL)
		{
			return Update(null,_TJ_MissExamLockMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_TJ_MissExamLockMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,TJ_MissExamLockMDL _TJ_MissExamLockMDL)
		{
			string sql = @"
			UPDATE dbo.TJ_MissExamLock
				SET	WorkerName = @WorkerName,FirstExamDate = @FirstExamDate,LockEndDate = @LockEndDate
			WHERE
				WorkerCertificateCode = @WorkerCertificateCode AND LockStartDate = @LockStartDate";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("WorkerCertificateCode",DbType.String, _TJ_MissExamLockMDL.WorkerCertificateCode));
			p.Add(db.CreateParameter("WorkerName",DbType.String, _TJ_MissExamLockMDL.WorkerName));
			p.Add(db.CreateParameter("FirstExamDate",DbType.DateTime, _TJ_MissExamLockMDL.FirstExamDate));
			p.Add(db.CreateParameter("LockStartDate",DbType.DateTime, _TJ_MissExamLockMDL.LockStartDate));
			p.Add(db.CreateParameter("LockEndDate",DbType.DateTime, _TJ_MissExamLockMDL.LockEndDate));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="TJ_MissExamLockID">主键</param>
		/// <returns></returns>
        public static int Delete( string WorkerCertificateCode, DateTime LockStartDate )
		{
			return Delete(null, WorkerCertificateCode, LockStartDate);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="WorkerCertificateCode">身份证号</param>
        /// <param name="LockStartDate">锁定日期</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, string WorkerCertificateCode, DateTime LockStartDate)
		{
			string sql=@"DELETE FROM dbo.TJ_MissExamLock WHERE WorkerCertificateCode = @WorkerCertificateCode AND LockStartDate = @LockStartDate";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("WorkerCertificateCode",DbType.String,WorkerCertificateCode));
			p.Add(db.CreateParameter("LockStartDate",DbType.DateTime,LockStartDate));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="_TJ_MissExamLockMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(TJ_MissExamLockMDL _TJ_MissExamLockMDL)
		{
			return Delete(null,_TJ_MissExamLockMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_TJ_MissExamLockMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,TJ_MissExamLockMDL _TJ_MissExamLockMDL)
		{
			string sql=@"DELETE FROM dbo.TJ_MissExamLock WHERE WorkerCertificateCode = @WorkerCertificateCode AND LockStartDate = @LockStartDate";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("WorkerCertificateCode",DbType.String,_TJ_MissExamLockMDL.WorkerCertificateCode));
			p.Add(db.CreateParameter("LockStartDate",DbType.DateTime,_TJ_MissExamLockMDL.LockStartDate));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="WorkerCertificateCode">身份证号</param>
        /// <param name="LockStartDate">锁定日期</param>
        public static TJ_MissExamLockMDL GetObject( string WorkerCertificateCode, DateTime LockStartDate )
		{
			string sql=@"
			SELECT WorkerCertificateCode,WorkerName,FirstExamDate,LockStartDate,LockEndDate
			FROM dbo.TJ_MissExamLock
			WHERE WorkerCertificateCode = @WorkerCertificateCode AND LockStartDate = @LockStartDate";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("WorkerCertificateCode", DbType.String, WorkerCertificateCode));
            p.Add(db.CreateParameter("LockStartDate", DbType.DateTime, LockStartDate));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                TJ_MissExamLockMDL _TJ_MissExamLockMDL = null;
                if (reader.Read())
                {
                    _TJ_MissExamLockMDL = new TJ_MissExamLockMDL();
					if (reader["WorkerCertificateCode"] != DBNull.Value) _TJ_MissExamLockMDL.WorkerCertificateCode = Convert.ToString(reader["WorkerCertificateCode"]);
					if (reader["WorkerName"] != DBNull.Value) _TJ_MissExamLockMDL.WorkerName = Convert.ToString(reader["WorkerName"]);
					if (reader["FirstExamDate"] != DBNull.Value) _TJ_MissExamLockMDL.FirstExamDate = Convert.ToDateTime(reader["FirstExamDate"]);
					if (reader["LockStartDate"] != DBNull.Value) _TJ_MissExamLockMDL.LockStartDate = Convert.ToDateTime(reader["LockStartDate"]);
					if (reader["LockEndDate"] != DBNull.Value) _TJ_MissExamLockMDL.LockEndDate = Convert.ToDateTime(reader["LockEndDate"]);
                }
				reader.Close();
                db.Close();
                return _TJ_MissExamLockMDL;
            }
        }

        /// <summary>
        /// 获取锁定中的记录
        /// </summary>
        /// <param name="WorkerCertificateCode">身份证号</param>
        public static TJ_MissExamLockMDL GetObject(string WorkerCertificateCode)
        {
            string sql = @"
			SELECT WorkerCertificateCode,WorkerName,FirstExamDate,LockStartDate,LockEndDate
			FROM dbo.TJ_MissExamLock
			WHERE WorkerCertificateCode = @WorkerCertificateCode AND LockEndDate > getdate()";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("WorkerCertificateCode", DbType.String, WorkerCertificateCode));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                TJ_MissExamLockMDL _TJ_MissExamLockMDL = null;
                if (reader.Read())
                {
                    _TJ_MissExamLockMDL = new TJ_MissExamLockMDL();
                    if (reader["WorkerCertificateCode"] != DBNull.Value) _TJ_MissExamLockMDL.WorkerCertificateCode = Convert.ToString(reader["WorkerCertificateCode"]);
                    if (reader["WorkerName"] != DBNull.Value) _TJ_MissExamLockMDL.WorkerName = Convert.ToString(reader["WorkerName"]);
                    if (reader["FirstExamDate"] != DBNull.Value) _TJ_MissExamLockMDL.FirstExamDate = Convert.ToDateTime(reader["FirstExamDate"]);
                    if (reader["LockStartDate"] != DBNull.Value) _TJ_MissExamLockMDL.LockStartDate = Convert.ToDateTime(reader["LockStartDate"]);
                    if (reader["LockEndDate"] != DBNull.Value) _TJ_MissExamLockMDL.LockEndDate = Convert.ToDateTime(reader["LockEndDate"]);
                }
                reader.Close();
                db.Close();
                return _TJ_MissExamLockMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.TJ_MissExamLock", "*", filterWhereString, orderBy == "" ? "LockStartDate desc,WorkerCertificateCode" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.TJ_MissExamLock", filterWhereString);
        }
        
        #region 自定义方法
        
        #endregion
    }
}
