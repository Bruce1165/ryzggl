using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--QY_HYLSGXDAL(填写类描述)
	/// </summary>
    public class QY_LSGXDAL
    {
        public QY_LSGXDAL() { }       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="QY_HYLSGXOB">对象实体类</param>
		/// <returns></returns>
        public static int Insert(QY_HYLSGXOB _QY_HYLSGXOB)
		{
		    return Insert(null,_QY_HYLSGXOB);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="QY_HYLSGXOB">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,QY_HYLSGXOB _QY_HYLSGXOB)
		{
			DBHelper db = new DBHelper();
		
			string sql = @"
			INSERT INTO dbo.QY_HYLSGX(ID,ZZJGDM,QYMC,LSGX,USERID,ORGANID )
			VALUES (@ID,@ZZJGDM,@QYMC,@LSGX,@USERID,@ORGANID )";

		    List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ID", DbType.String, _QY_HYLSGXOB.ID));
            p.Add(db.CreateParameter("ZZJGDM", DbType.String, _QY_HYLSGXOB.ZZJGDM));
            p.Add(db.CreateParameter("QYMC", DbType.String, _QY_HYLSGXOB.QYMC));
            p.Add(db.CreateParameter("LSGX", DbType.String, _QY_HYLSGXOB.LSGX));
            p.Add(db.CreateParameter("USERID", DbType.Int64, _QY_HYLSGXOB.USERID));
            p.Add(db.CreateParameter("ORGANID", DbType.Int64, _QY_HYLSGXOB.ORGANID));
			return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="QY_HYLSGXOB">对象实体类</param>
		/// <returns></returns>
        public static int Update(QY_HYLSGXOB _QY_HYLSGXOB)
		{
			return Update(null,_QY_HYLSGXOB);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="QY_HYLSGXOB">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,QY_HYLSGXOB _QY_HYLSGXOB)
		{
			string sql = @"
			UPDATE dbo.QY_HYLSGX
                SET ZZJGDM=@ZZJGDM,QYMC=@QYMC,LSGX=@LSGX,USERID=@USERID,ORGANID=@ORGANID
			WHERE
				ID =@ID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ID", DbType.String, _QY_HYLSGXOB.ID));
            p.Add(db.CreateParameter("ZZJGDM", DbType.String, _QY_HYLSGXOB.ZZJGDM));
            p.Add(db.CreateParameter("QYMC", DbType.String, _QY_HYLSGXOB.QYMC));
            p.Add(db.CreateParameter("LSGX", DbType.String, _QY_HYLSGXOB.LSGX));
            p.Add(db.CreateParameter("USERID", DbType.Int64, _QY_HYLSGXOB.USERID));
            p.Add(db.CreateParameter("ORGANID", DbType.Int64, _QY_HYLSGXOB.ORGANID));
			return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="QY_HYLSGXID">主键</param>
		/// <returns></returns>
        public static int Delete(string ID)
		{
            return Delete(null, ID);
		}
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="QY_HYLSGXID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, string ID)
		{
            string sql = @"DELETE FROM dbo.QY_HYLSGX WHERE ID =@ID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ID", DbType.String, ID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="QY_HYLSGXOB">对象实体类</param>
		/// <returns></returns>
        public static int Delete(QY_HYLSGXOB _QY_HYLSGXOB)
		{
			return Delete(null,_QY_HYLSGXOB);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="QY_HYLSGXOB">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,QY_HYLSGXOB _QY_HYLSGXOB)
		{
            string sql = @"DELETE FROM dbo.QY_HYLSGX WHERE ID =@ID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ID", DbType.String, _QY_HYLSGXOB.ID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="QY_HYLSGXID">主键</param>
        public static QY_HYLSGXOB GetObject(string ID)
        {
            string sql = @"
			SELECT *
			FROM dbo.QY_HYLSGX
			WHERE ID =@ID ";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ID", DbType.String, ID));
     
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    QY_HYLSGXOB _QY_HYLSGXOB = null;
                    if (reader.Read())
                    {
                        _QY_HYLSGXOB = new QY_HYLSGXOB();
                        if (reader["ID"] != DBNull.Value) _QY_HYLSGXOB.ID = Convert.ToString(reader["ID"]);
                        if (reader["ZZJGDM"] != DBNull.Value) _QY_HYLSGXOB.ZZJGDM = Convert.ToString(reader["ZZJGDM"]);
                        if (reader["QYMC"] != DBNull.Value) _QY_HYLSGXOB.QYMC = Convert.ToString(reader["QYMC"]);
                        if (reader["LSGX"] != DBNull.Value) _QY_HYLSGXOB.LSGX = Convert.ToString(reader["LSGX"]);

                        if (reader["USERID"] != DBNull.Value) _QY_HYLSGXOB.USERID = Convert.ToInt64(reader["USERID"]);
                        if (reader["ORGANID"] != DBNull.Value) _QY_HYLSGXOB.ORGANID = Convert.ToInt64(reader["ORGANID"]);

                    }
                    reader.Close();
                    db.Close();
                    return _QY_HYLSGXOB;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.QY_HYLSGX", "*", filterWhereString, orderBy == "" ? " ID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.QY_HYLSGX", filterWhereString);
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.VIEW_UNIT_LSGX", "*", filterWhereString, orderBy == "" ? " UnitName" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountView(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.VIEW_UNIT_LSGX", filterWhereString);
        }



        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ZZJGDM">组织机构代码</param>
        public static QY_HYLSGXOB GetObjectByZZJGDM(string ZZJGDM)
        {
            string sql = @"
			SELECT top 1 *
			FROM dbo.QY_HYLSGX
			WHERE ZZJGDM =@ZZJGDM ";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ZZJGDM", DbType.String, ZZJGDM));

            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    QY_HYLSGXOB _QY_HYLSGXOB = null;
                    if (reader.Read())
                    {
                        _QY_HYLSGXOB = new QY_HYLSGXOB();
                        if (reader["ID"] != DBNull.Value) _QY_HYLSGXOB.ID = Convert.ToString(reader["ID"]);
                        if (reader["ZZJGDM"] != DBNull.Value) _QY_HYLSGXOB.ZZJGDM = Convert.ToString(reader["ZZJGDM"]);
                        if (reader["QYMC"] != DBNull.Value) _QY_HYLSGXOB.QYMC = Convert.ToString(reader["QYMC"]);
                        if (reader["LSGX"] != DBNull.Value) _QY_HYLSGXOB.LSGX = Convert.ToString(reader["LSGX"]);
                        if (reader["USERID"] != DBNull.Value) _QY_HYLSGXOB.USERID = Convert.ToInt64(reader["USERID"]);
                        if (reader["ORGANID"] != DBNull.Value) _QY_HYLSGXOB.ORGANID = Convert.ToInt64(reader["ORGANID"]);

                    }
                    reader.Close();
                    db.Close();
                    return _QY_HYLSGXOB;
                }
            }
            catch (Exception ex)
            {
                db.Close();
                throw ex;
            }
        }
    }
}
