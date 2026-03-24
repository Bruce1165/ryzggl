using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--TJ_VisitDAL(填写类描述)
	/// </summary>
    public class TJ_VisitDAL
    {
        public TJ_VisitDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_TJ_VisitMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(TJ_VisitMDL _TJ_VisitMDL)
		{
		    return Insert(null,_TJ_VisitMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_TJ_VisitMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,TJ_VisitMDL _TJ_VisitMDL)
		{
            DBHelper db = new DBHelper("DBRYPX");	
			string sql = @"
			INSERT INTO dbo.TJ_Visit(TJ_Year,TJ_Count)
			VALUES (@TJ_Year,@TJ_Count)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("TJ_Year",DbType.Int32, _TJ_VisitMDL.TJ_Year));
			p.Add(db.CreateParameter("TJ_Count",DbType.Int64, _TJ_VisitMDL.TJ_Count));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_TJ_VisitMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(TJ_VisitMDL _TJ_VisitMDL)
		{
			return Update(null,_TJ_VisitMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_TJ_VisitMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,TJ_VisitMDL _TJ_VisitMDL)
		{
			string sql = @"
			UPDATE dbo.TJ_Visit
				SET	TJ_Count = @TJ_Count
			WHERE
				TJ_Year = @TJ_Year";

            DBHelper db = new DBHelper("DBRYPX");
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("TJ_Year",DbType.Int32, _TJ_VisitMDL.TJ_Year));
			p.Add(db.CreateParameter("TJ_Count",DbType.Int64, _TJ_VisitMDL.TJ_Count));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="TJ_VisitID">主键</param>
		/// <returns></returns>
        public static int Delete( int TJ_Year )
		{
			return Delete(null, TJ_Year);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="TJ_VisitID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, int TJ_Year)
		{
			string sql=@"DELETE FROM dbo.TJ_Visit WHERE TJ_Year = @TJ_Year";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("TJ_Year",DbType.Int32,TJ_Year));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="_TJ_VisitMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(TJ_VisitMDL _TJ_VisitMDL)
		{
			return Delete(null,_TJ_VisitMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_TJ_VisitMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,TJ_VisitMDL _TJ_VisitMDL)
		{
			string sql=@"DELETE FROM dbo.TJ_Visit WHERE TJ_Year = @TJ_Year";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("TJ_Year",DbType.Int32,_TJ_VisitMDL.TJ_Year));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="TJ_VisitID">主键</param>
        public static TJ_VisitMDL GetObject( int TJ_Year )
		{
			string sql=@"
			SELECT TJ_Year,TJ_Count
			FROM dbo.TJ_Visit
			WHERE TJ_Year = @TJ_Year";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("TJ_Year",DbType.Int32,TJ_Year));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                TJ_VisitMDL _TJ_VisitMDL = null;
                if (reader.Read())
                {
                    _TJ_VisitMDL = new TJ_VisitMDL();
					if (reader["TJ_Year"] != DBNull.Value) _TJ_VisitMDL.TJ_Year = Convert.ToInt32(reader["TJ_Year"]);
					if (reader["TJ_Count"] != DBNull.Value) _TJ_VisitMDL.TJ_Count = Convert.ToInt64(reader["TJ_Count"]);
                }
				reader.Close();
                db.Close();
                return _TJ_VisitMDL;
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
            return CommonDAL.GetDataTableDB("DBRYPX", startRowIndex, maximumRows, "dbo.TJ_Visit", "*", filterWhereString, orderBy == "" ? " TJ_Year" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCountDB("DBRYPX", "dbo.TJ_Visit", filterWhereString);
        }
        
        #region 自定义方法
        
        #endregion
    }
}
