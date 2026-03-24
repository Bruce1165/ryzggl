using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
    /// 业务类实现--WorkerSetDAL(个人空间设置)
	/// </summary>
    public class WorkerSetDAL
    {
        public WorkerSetDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_WorkerSetMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(WorkerSetMDL _WorkerSetMDL)
		{
		    return Insert(null,_WorkerSetMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_WorkerSetMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,WorkerSetMDL _WorkerSetMDL)
		{
            DBHelper db = new DBHelper("DBRYPX");		
			string sql = @"
			INSERT INTO dbo.WorkerSet(WorkerCertificateCode,SaveSource)
			VALUES (@WorkerCertificateCode,@SaveSource)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("WorkerCertificateCode",DbType.String, _WorkerSetMDL.WorkerCertificateCode));
			p.Add(db.CreateParameter("SaveSource",DbType.String, _WorkerSetMDL.SaveSource));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_WorkerSetMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(WorkerSetMDL _WorkerSetMDL)
		{
			return Update(null,_WorkerSetMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_WorkerSetMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,WorkerSetMDL _WorkerSetMDL)
		{
			string sql = @"
			UPDATE dbo.WorkerSet
				SET	SaveSource = @SaveSource
			WHERE
				WorkerCertificateCode = @WorkerCertificateCode";

            DBHelper db = new DBHelper("DBRYPX");
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("WorkerCertificateCode",DbType.String, _WorkerSetMDL.WorkerCertificateCode));
			p.Add(db.CreateParameter("SaveSource",DbType.String, _WorkerSetMDL.SaveSource));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="WorkerCertificateCode">主键</param>
		/// <returns></returns>
        public static int Delete( string WorkerCertificateCode )
		{
			return Delete(null, WorkerCertificateCode);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="WorkerCertificateCode">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, string WorkerCertificateCode)
		{
			string sql=@"DELETE FROM dbo.WorkerSet WHERE WorkerCertificateCode = @WorkerCertificateCode";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("WorkerCertificateCode",DbType.String,WorkerCertificateCode));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="_WorkerSetMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(WorkerSetMDL _WorkerSetMDL)
		{
			return Delete(null,_WorkerSetMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_WorkerSetMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,WorkerSetMDL _WorkerSetMDL)
		{
			string sql=@"DELETE FROM dbo.WorkerSet WHERE WorkerCertificateCode = @WorkerCertificateCode";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("WorkerCertificateCode",DbType.String,_WorkerSetMDL.WorkerCertificateCode));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="WorkerCertificateCode">主键</param>
        public static WorkerSetMDL GetObject( string WorkerCertificateCode )
		{
			string sql=@"
			SELECT WorkerCertificateCode,SaveSource
			FROM dbo.WorkerSet
			WHERE WorkerCertificateCode = @WorkerCertificateCode";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("WorkerCertificateCode",DbType.String,WorkerCertificateCode));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                WorkerSetMDL _WorkerSetMDL = null;
                if (reader.Read())
                {
                    _WorkerSetMDL = new WorkerSetMDL();
					if (reader["WorkerCertificateCode"] != DBNull.Value) _WorkerSetMDL.WorkerCertificateCode = Convert.ToString(reader["WorkerCertificateCode"]);
					if (reader["SaveSource"] != DBNull.Value) _WorkerSetMDL.SaveSource = Convert.ToString(reader["SaveSource"]);
                }
				reader.Close();
                db.Close();
                return _WorkerSetMDL;
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
            return CommonDAL.GetDataTableDB("DBRYPX", startRowIndex, maximumRows, "dbo.WorkerSet", "*", filterWhereString, orderBy == "" ? " WorkerCertificateCode" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCountDB("DBRYPX", "dbo.WorkerSet", filterWhereString);
        }
        
        #region 自定义方法
        
        #endregion
    }
}
