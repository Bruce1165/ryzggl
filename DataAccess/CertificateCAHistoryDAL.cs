using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--CertificateCAHistoryDAL(填写类描述)
	/// </summary>
    public class CertificateCAHistoryDAL
    {
        public CertificateCAHistoryDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="CertificateCAHistoryMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(CertificateCAHistoryMDL _CertificateCAHistoryMDL)
		{
		    return Insert(null,_CertificateCAHistoryMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="CertificateCAHistoryMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,CertificateCAHistoryMDL _CertificateCAHistoryMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.CertificateCAHistory(CertificateCAID,ApplyCATime,SendCATime,ReturnCATime,CertificateID)
			VALUES (@CertificateCAID,@ApplyCATime,@SendCATime,@ReturnCATime,@CertificateID)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("CertificateCAID",DbType.String, _CertificateCAHistoryMDL.CertificateCAID));
			p.Add(db.CreateParameter("ApplyCATime",DbType.DateTime, _CertificateCAHistoryMDL.ApplyCATime));
			p.Add(db.CreateParameter("SendCATime",DbType.DateTime, _CertificateCAHistoryMDL.SendCATime));
			p.Add(db.CreateParameter("ReturnCATime",DbType.DateTime, _CertificateCAHistoryMDL.ReturnCATime));
			p.Add(db.CreateParameter("CertificateID",DbType.Int64, _CertificateCAHistoryMDL.CertificateID));
			return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="CertificateCAHistoryMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(CertificateCAHistoryMDL _CertificateCAHistoryMDL)
		{
			return Update(null,_CertificateCAHistoryMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="CertificateCAHistoryMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,CertificateCAHistoryMDL _CertificateCAHistoryMDL)
		{
			string sql = @"
			UPDATE dbo.CertificateCAHistory
				SET	ApplyCATime = @ApplyCATime,SendCATime = @SendCATime,ReturnCATime = @ReturnCATime,CertificateID = @CertificateID
			WHERE
				CertificateCAID = @CertificateCAID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("CertificateCAID",DbType.String, _CertificateCAHistoryMDL.CertificateCAID));
			p.Add(db.CreateParameter("ApplyCATime",DbType.DateTime, _CertificateCAHistoryMDL.ApplyCATime));
			p.Add(db.CreateParameter("SendCATime",DbType.DateTime, _CertificateCAHistoryMDL.SendCATime));
			p.Add(db.CreateParameter("ReturnCATime",DbType.DateTime, _CertificateCAHistoryMDL.ReturnCATime));
			p.Add(db.CreateParameter("CertificateID",DbType.Int64, _CertificateCAHistoryMDL.CertificateID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="CertificateCAHistoryID">主键</param>
		/// <returns></returns>
        public static int Delete( string CertificateCAID )
		{
			return Delete(null, CertificateCAID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="CertificateCAHistoryID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, string CertificateCAID)
		{
			string sql=@"DELETE FROM dbo.CertificateCAHistory WHERE CertificateCAID = @CertificateCAID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("CertificateCAID",DbType.String,CertificateCAID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="CertificateCAHistoryMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(CertificateCAHistoryMDL _CertificateCAHistoryMDL)
		{
			return Delete(null,_CertificateCAHistoryMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="CertificateCAHistoryMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,CertificateCAHistoryMDL _CertificateCAHistoryMDL)
		{
			string sql=@"DELETE FROM dbo.CertificateCAHistory WHERE CertificateCAID = @CertificateCAID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("CertificateCAID",DbType.String,_CertificateCAHistoryMDL.CertificateCAID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="CertificateCAHistoryID">主键</param>
        public static CertificateCAHistoryMDL GetObject( string CertificateCAID )
		{
			string sql=@"
			SELECT CertificateCAID,ApplyCATime,SendCATime,ReturnCATime,CertificateID
			FROM dbo.CertificateCAHistory
			WHERE CertificateCAID = @CertificateCAID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("CertificateCAID", DbType.String, CertificateCAID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                CertificateCAHistoryMDL _CertificateCAHistoryMDL = null;
                if (reader.Read())
                {
                    _CertificateCAHistoryMDL = new CertificateCAHistoryMDL();
					if (reader["CertificateCAID"] != DBNull.Value) _CertificateCAHistoryMDL.CertificateCAID = Convert.ToString(reader["CertificateCAID"]);
					if (reader["ApplyCATime"] != DBNull.Value) _CertificateCAHistoryMDL.ApplyCATime = Convert.ToDateTime(reader["ApplyCATime"]);
					if (reader["SendCATime"] != DBNull.Value) _CertificateCAHistoryMDL.SendCATime = Convert.ToDateTime(reader["SendCATime"]);
					if (reader["ReturnCATime"] != DBNull.Value) _CertificateCAHistoryMDL.ReturnCATime = Convert.ToDateTime(reader["ReturnCATime"]);
					if (reader["CertificateID"] != DBNull.Value) _CertificateCAHistoryMDL.CertificateID = Convert.ToInt64(reader["CertificateID"]);
                }
				reader.Close();
                db.Close();
                return _CertificateCAHistoryMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.CertificateCAHistory", "*", filterWhereString, orderBy == "" ? " CertificateCAID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.CertificateCAHistory", filterWhereString);
        }
        
        #region 自定义方法
        
        #endregion
    }
}
