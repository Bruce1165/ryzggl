using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--FileDownLogDAL(填写类描述)
	/// </summary>
    public class FileDownLogDAL
    {
        public FileDownLogDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="FileDownLogMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(FileDownLogMDL _FileDownLogMDL)
		{
		    return Insert(null,_FileDownLogMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="FileDownLogMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,FileDownLogMDL _FileDownLogMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.FileDownLog(FileID,DownMAN,DownManID,DownTime,DownFileName,FileTypeCode,DownDesc)
			VALUES (@FileID,@DownMAN,@DownManID,@DownTime,@DownFileName,@FileTypeCode,@DownDesc);SELECT @LogID = @@IDENTITY";

		    List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateOutParameter("LogID", DbType.Int64));
			p.Add(db.CreateParameter("FileID",DbType.String, _FileDownLogMDL.FileID));
			p.Add(db.CreateParameter("DownMAN",DbType.String, _FileDownLogMDL.DownMAN));
            p.Add(db.CreateParameter("DownManID", DbType.String, _FileDownLogMDL.DownManID));
			p.Add(db.CreateParameter("DownTime",DbType.DateTime, _FileDownLogMDL.DownTime));
			p.Add(db.CreateParameter("DownFileName",DbType.String, _FileDownLogMDL.DownFileName));
			p.Add(db.CreateParameter("FileTypeCode",DbType.Int32, _FileDownLogMDL.FileTypeCode));
			p.Add(db.CreateParameter("DownDesc",DbType.String, _FileDownLogMDL.DownDesc));

            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            _FileDownLogMDL.LogID = Convert.ToInt64(p[0].Value);
            return rtn;
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="FileDownLogMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(FileDownLogMDL _FileDownLogMDL)
		{
			return Update(null,_FileDownLogMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="FileDownLogMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,FileDownLogMDL _FileDownLogMDL)
		{
			string sql = @"
			UPDATE dbo.FileDownLog
				SET	FileID = @FileID,DownMAN = @DownMAN,DownManID = @DownManID,DownTime = @DownTime,DownFileName = @DownFileName,FileTypeCode = @FileTypeCode,DownDesc = @DownDesc
			WHERE
				LogID = @LogID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("LogID",DbType.Int64, _FileDownLogMDL.LogID));
			p.Add(db.CreateParameter("FileID",DbType.String, _FileDownLogMDL.FileID));
			p.Add(db.CreateParameter("DownMAN",DbType.String, _FileDownLogMDL.DownMAN));
            p.Add(db.CreateParameter("DownManID", DbType.String, _FileDownLogMDL.DownManID));
			p.Add(db.CreateParameter("DownTime",DbType.DateTime, _FileDownLogMDL.DownTime));
			p.Add(db.CreateParameter("DownFileName",DbType.String, _FileDownLogMDL.DownFileName));
			p.Add(db.CreateParameter("FileTypeCode",DbType.Int32, _FileDownLogMDL.FileTypeCode));
			p.Add(db.CreateParameter("DownDesc",DbType.String, _FileDownLogMDL.DownDesc));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="FileDownLogID">主键</param>
		/// <returns></returns>
        public static int Delete( long LogID )
		{
			return Delete(null, LogID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="FileDownLogID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, long LogID)
		{
			string sql=@"DELETE FROM dbo.FileDownLog WHERE LogID = @LogID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("LogID",DbType.Int64,LogID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="FileDownLogMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(FileDownLogMDL _FileDownLogMDL)
		{
			return Delete(null,_FileDownLogMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="FileDownLogMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,FileDownLogMDL _FileDownLogMDL)
		{
			string sql=@"DELETE FROM dbo.FileDownLog WHERE LogID = @LogID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("LogID",DbType.Int64,_FileDownLogMDL.LogID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="FileDownLogID">主键</param>
        public static FileDownLogMDL GetObject( long LogID )
		{
			string sql=@"
			SELECT LogID,FileID,DownMAN,DownManID,DownTime,DownFileName,FileTypeCode,DownDesc
			FROM dbo.FileDownLog
			WHERE LogID = @LogID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("LogID", DbType.Int64, LogID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                FileDownLogMDL _FileDownLogMDL = null;
                if (reader.Read())
                {
                    _FileDownLogMDL = new FileDownLogMDL();
					if (reader["LogID"] != DBNull.Value) _FileDownLogMDL.LogID = Convert.ToInt64(reader["LogID"]);
					if (reader["FileID"] != DBNull.Value) _FileDownLogMDL.FileID = Convert.ToString(reader["FileID"]);
					if (reader["DownMAN"] != DBNull.Value) _FileDownLogMDL.DownMAN = Convert.ToString(reader["DownMAN"]);
                    if (reader["DownManID"] != DBNull.Value) _FileDownLogMDL.DownManID = Convert.ToString(reader["DownManID"]);
					if (reader["DownTime"] != DBNull.Value) _FileDownLogMDL.DownTime = Convert.ToDateTime(reader["DownTime"]);
					if (reader["DownFileName"] != DBNull.Value) _FileDownLogMDL.DownFileName = Convert.ToString(reader["DownFileName"]);
					if (reader["FileTypeCode"] != DBNull.Value) _FileDownLogMDL.FileTypeCode = Convert.ToInt32(reader["FileTypeCode"]);
					if (reader["DownDesc"] != DBNull.Value) _FileDownLogMDL.DownDesc = Convert.ToString(reader["DownDesc"]);
                }
				reader.Close();
                db.Close();
                return _FileDownLogMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.FileDownLog", "*", filterWhereString, orderBy == "" ? " LogID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.FileDownLog", filterWhereString);
        }
        
        #region 自定义方法
        
        #endregion
    }
}
