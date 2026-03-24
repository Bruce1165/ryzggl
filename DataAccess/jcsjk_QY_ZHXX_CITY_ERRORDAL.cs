using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--jcsjk_QY_ZHXX_CITY_ERRORDAL(填写类描述)
	/// </summary>
    public class jcsjk_QY_ZHXX_CITY_ERRORDAL
    {
        public jcsjk_QY_ZHXX_CITY_ERRORDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_jcsjk_QY_ZHXX_CITY_ERRORMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(jcsjk_QY_ZHXX_CITY_ERRORMDL _jcsjk_QY_ZHXX_CITY_ERRORMDL)
		{
		    return Insert(null,_jcsjk_QY_ZHXX_CITY_ERRORMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_jcsjk_QY_ZHXX_CITY_ERRORMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,jcsjk_QY_ZHXX_CITY_ERRORMDL _jcsjk_QY_ZHXX_CITY_ERRORMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.jcsjk_QY_ZHXX_CITY_ERROR(QYMC,XZDQBM,[XGSJ])
			VALUES (@QYMC,@XZDQBM,getdate())";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("QYMC",DbType.String, _jcsjk_QY_ZHXX_CITY_ERRORMDL.QYMC));
			p.Add(db.CreateParameter("XZDQBM",DbType.String, _jcsjk_QY_ZHXX_CITY_ERRORMDL.XZDQBM));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_jcsjk_QY_ZHXX_CITY_ERRORMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(jcsjk_QY_ZHXX_CITY_ERRORMDL _jcsjk_QY_ZHXX_CITY_ERRORMDL)
		{
			return Update(null,_jcsjk_QY_ZHXX_CITY_ERRORMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_jcsjk_QY_ZHXX_CITY_ERRORMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,jcsjk_QY_ZHXX_CITY_ERRORMDL _jcsjk_QY_ZHXX_CITY_ERRORMDL)
		{
			string sql = @"
			UPDATE dbo.jcsjk_QY_ZHXX_CITY_ERROR
				SET	XZDQBM = @XZDQBM,[XGSJ]=getdate()
			WHERE
				QYMC = @QYMC";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("QYMC",DbType.String, _jcsjk_QY_ZHXX_CITY_ERRORMDL.QYMC));
			p.Add(db.CreateParameter("XZDQBM",DbType.String, _jcsjk_QY_ZHXX_CITY_ERRORMDL.XZDQBM));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="jcsjk_QY_ZHXX_CITY_ERRORID">主键</param>
		/// <returns></returns>
        public static int Delete( string QYMC )
		{
			return Delete(null, QYMC);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="jcsjk_QY_ZHXX_CITY_ERRORID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, string QYMC)
		{
			string sql=@"DELETE FROM dbo.jcsjk_QY_ZHXX_CITY_ERROR WHERE QYMC = @QYMC";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("QYMC",DbType.String,QYMC));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="_jcsjk_QY_ZHXX_CITY_ERRORMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(jcsjk_QY_ZHXX_CITY_ERRORMDL _jcsjk_QY_ZHXX_CITY_ERRORMDL)
		{
			return Delete(null,_jcsjk_QY_ZHXX_CITY_ERRORMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_jcsjk_QY_ZHXX_CITY_ERRORMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,jcsjk_QY_ZHXX_CITY_ERRORMDL _jcsjk_QY_ZHXX_CITY_ERRORMDL)
		{
			string sql=@"DELETE FROM dbo.jcsjk_QY_ZHXX_CITY_ERROR WHERE QYMC = @QYMC";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("QYMC",DbType.String,_jcsjk_QY_ZHXX_CITY_ERRORMDL.QYMC));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="jcsjk_QY_ZHXX_CITY_ERRORID">主键</param>
        public static jcsjk_QY_ZHXX_CITY_ERRORMDL GetObject( string QYMC )
		{
			string sql= @"
			SELECT QYMC,XZDQBM
			FROM dbo.jcsjk_QY_ZHXX_CITY_ERROR
			WHERE QYMC = @QYMC";
			
			DBHelper db = new DBHelper();
            //List<SqlParameter> p = new List<SqlParameter>();
            //p.Add(db.CreateParameter("QYMC", DbType.String, QYMC));
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("QYMC",DbType.String,QYMC));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                jcsjk_QY_ZHXX_CITY_ERRORMDL _jcsjk_QY_ZHXX_CITY_ERRORMDL = null;
                if (reader.Read())
                {
                    _jcsjk_QY_ZHXX_CITY_ERRORMDL = new jcsjk_QY_ZHXX_CITY_ERRORMDL();
					if (reader["QYMC"] != DBNull.Value) _jcsjk_QY_ZHXX_CITY_ERRORMDL.QYMC = Convert.ToString(reader["QYMC"]);
					if (reader["XZDQBM"] != DBNull.Value) _jcsjk_QY_ZHXX_CITY_ERRORMDL.XZDQBM = Convert.ToString(reader["XZDQBM"]);
                }
				reader.Close();
                db.Close();
                return _jcsjk_QY_ZHXX_CITY_ERRORMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.jcsjk_QY_ZHXX_CITY_ERROR", "*", filterWhereString, orderBy == "" ? " [XGSJ] desc, QYMC" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.jcsjk_QY_ZHXX_CITY_ERROR", filterWhereString);
        }
        
        #region 自定义方法
        
        #endregion
    }
}
