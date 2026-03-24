using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--GYPXWhiteDAL(填写类描述)
	/// </summary>
    public class GYPXWhiteDAL
    {
        public GYPXWhiteDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_GYPXWhiteMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(GYPXWhiteMDL _GYPXWhiteMDL)
		{
		    return Insert(null,_GYPXWhiteMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_GYPXWhiteMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,GYPXWhiteMDL _GYPXWhiteMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.GYPXWhite(WorkerCertificateCode,PostTypeID,FreeStart,FreeEnd)
			VALUES (@WorkerCertificateCode,@PostTypeID,@FreeStart,@FreeEnd);SELECT @DataID = @@IDENTITY";

		    List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateOutParameter("DataID", DbType.Int64));
			p.Add(db.CreateParameter("WorkerCertificateCode",DbType.String, _GYPXWhiteMDL.WorkerCertificateCode));
			p.Add(db.CreateParameter("PostTypeID",DbType.Int32, _GYPXWhiteMDL.PostTypeID));
			p.Add(db.CreateParameter("FreeStart",DbType.DateTime, _GYPXWhiteMDL.FreeStart));
			p.Add(db.CreateParameter("FreeEnd",DbType.DateTime, _GYPXWhiteMDL.FreeEnd));

            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            _GYPXWhiteMDL.DataID = Convert.ToInt64(p[0].Value);
            return rtn;
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_GYPXWhiteMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(GYPXWhiteMDL _GYPXWhiteMDL)
		{
			return Update(null,_GYPXWhiteMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_GYPXWhiteMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,GYPXWhiteMDL _GYPXWhiteMDL)
		{
			string sql = @"
			UPDATE dbo.GYPXWhite
				SET	WorkerCertificateCode = @WorkerCertificateCode,PostTypeID = @PostTypeID,FreeStart = @FreeStart,FreeEnd = @FreeEnd
			WHERE
				DataID = @DataID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("DataID",DbType.Int64, _GYPXWhiteMDL.DataID));
			p.Add(db.CreateParameter("WorkerCertificateCode",DbType.String, _GYPXWhiteMDL.WorkerCertificateCode));
			p.Add(db.CreateParameter("PostTypeID",DbType.Int32, _GYPXWhiteMDL.PostTypeID));
			p.Add(db.CreateParameter("FreeStart",DbType.DateTime, _GYPXWhiteMDL.FreeStart));
			p.Add(db.CreateParameter("FreeEnd",DbType.DateTime, _GYPXWhiteMDL.FreeEnd));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="DataID">主键</param>
		/// <returns></returns>
        public static int Delete( long DataID )
		{
			return Delete(null, DataID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="DataID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, long DataID)
		{
			string sql=@"DELETE FROM dbo.GYPXWhite WHERE DataID = @DataID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("DataID",DbType.Int64,DataID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="_GYPXWhiteMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(GYPXWhiteMDL _GYPXWhiteMDL)
		{
			return Delete(null,_GYPXWhiteMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_GYPXWhiteMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,GYPXWhiteMDL _GYPXWhiteMDL)
		{
			string sql=@"DELETE FROM dbo.GYPXWhite WHERE DataID = @DataID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("DataID",DbType.Int64,_GYPXWhiteMDL.DataID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="DataID">主键</param>
        public static GYPXWhiteMDL GetObject( long DataID )
		{
			string sql=@"
			SELECT DataID,WorkerCertificateCode,PostTypeID,FreeStart,FreeEnd
			FROM dbo.GYPXWhite
			WHERE DataID = @DataID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("DataID",DbType.Int64,DataID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                GYPXWhiteMDL _GYPXWhiteMDL = null;
                if (reader.Read())
                {
                    _GYPXWhiteMDL = new GYPXWhiteMDL();
					if (reader["DataID"] != DBNull.Value) _GYPXWhiteMDL.DataID = Convert.ToInt64(reader["DataID"]);
					if (reader["WorkerCertificateCode"] != DBNull.Value) _GYPXWhiteMDL.WorkerCertificateCode = Convert.ToString(reader["WorkerCertificateCode"]);
					if (reader["PostTypeID"] != DBNull.Value) _GYPXWhiteMDL.PostTypeID = Convert.ToInt32(reader["PostTypeID"]);
					if (reader["FreeStart"] != DBNull.Value) _GYPXWhiteMDL.FreeStart = Convert.ToDateTime(reader["FreeStart"]);
					if (reader["FreeEnd"] != DBNull.Value) _GYPXWhiteMDL.FreeEnd = Convert.ToDateTime(reader["FreeEnd"]);
                }
				reader.Close();
                db.Close();
                return _GYPXWhiteMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.GYPXWhite", "*", filterWhereString, orderBy == "" ? " DataID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.GYPXWhite", filterWhereString);
        }
        
        #region 自定义方法

        /// <summary>
        /// 检查是否符合公益视频培训免学白名单
        /// </summary>
        /// <param name="WorkerCertificateCode">身份证号</param>
        /// <param name="PostTypeID">岗位类别ID：</param>
        /// <returns></returns>
        public static bool CheckWhiteList(string WorkerCertificateCode,int PostTypeID)
        {
            int count = CommonDAL.SelectRowCount(string.Format("select count(*) from dbo.GYPXWhite where WorkerCertificateCode = '{0}' and PostTypeID = {1} and FreeStart < getdate() and FreeEnd > dateadd(day,-1,getdate())", WorkerCertificateCode, PostTypeID));
            if (count > 0)
                return true;
            else
                return false;
        }

        #endregion
    }
}
