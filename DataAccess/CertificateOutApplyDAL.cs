using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--CertificateOutApplyDAL(填写类描述)
	/// </summary>
    public class CertificateOutApplyDAL
    {
        public CertificateOutApplyDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_CertificateOutApplyMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(CertificateOutApplyMDL _CertificateOutApplyMDL)
		{
		    return Insert(null,_CertificateOutApplyMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_CertificateOutApplyMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,CertificateOutApplyMDL _CertificateOutApplyMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.CertificateOutApply(ApplyTime,WorkerCertificateCode,checkRtnCode,checkInfo,checkTime)
			VALUES (@ApplyTime,@WorkerCertificateCode,@checkRtnCode,@checkInfo,@checkTime);SELECT @ApplyID = @@IDENTITY";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateOutParameter("ApplyID", DbType.Int64));
			p.Add(db.CreateParameter("ApplyTime",DbType.DateTime, _CertificateOutApplyMDL.ApplyTime));
			p.Add(db.CreateParameter("WorkerCertificateCode",DbType.String, _CertificateOutApplyMDL.WorkerCertificateCode));
			p.Add(db.CreateParameter("checkRtnCode",DbType.Int32, _CertificateOutApplyMDL.checkRtnCode));
			p.Add(db.CreateParameter("checkInfo",DbType.String, _CertificateOutApplyMDL.checkInfo));
			p.Add(db.CreateParameter("checkTime",DbType.DateTime, _CertificateOutApplyMDL.checkTime));

            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            _CertificateOutApplyMDL.ApplyID = Convert.ToInt64(p[0].Value);
            return rtn;
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_CertificateOutApplyMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(CertificateOutApplyMDL _CertificateOutApplyMDL)
		{
			return Update(null,_CertificateOutApplyMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_CertificateOutApplyMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,CertificateOutApplyMDL _CertificateOutApplyMDL)
		{
			string sql = @"
			UPDATE dbo.CertificateOutApply
				SET	ApplyTime = @ApplyTime,WorkerCertificateCode = @WorkerCertificateCode,checkRtnCode = @checkRtnCode,checkInfo = @checkInfo,checkTime = @checkTime
			WHERE
				ApplyID = @ApplyID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ApplyID",DbType.Int64, _CertificateOutApplyMDL.ApplyID));
			p.Add(db.CreateParameter("ApplyTime",DbType.DateTime, _CertificateOutApplyMDL.ApplyTime));
			p.Add(db.CreateParameter("WorkerCertificateCode",DbType.String, _CertificateOutApplyMDL.WorkerCertificateCode));
			p.Add(db.CreateParameter("checkRtnCode",DbType.Int32, _CertificateOutApplyMDL.checkRtnCode));
			p.Add(db.CreateParameter("checkInfo",DbType.String, _CertificateOutApplyMDL.checkInfo));
			p.Add(db.CreateParameter("checkTime",DbType.DateTime, _CertificateOutApplyMDL.checkTime));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="CertificateOutApplyID">主键</param>
		/// <returns></returns>
        public static int Delete( long ApplyID )
		{
			return Delete(null, ApplyID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="CertificateOutApplyID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, long ApplyID)
		{
			string sql=@"DELETE FROM dbo.CertificateOutApply WHERE ApplyID = @ApplyID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ApplyID",DbType.Int64,ApplyID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="_CertificateOutApplyMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(CertificateOutApplyMDL _CertificateOutApplyMDL)
		{
			return Delete(null,_CertificateOutApplyMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_CertificateOutApplyMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,CertificateOutApplyMDL _CertificateOutApplyMDL)
		{
			string sql=@"DELETE FROM dbo.CertificateOutApply WHERE ApplyID = @ApplyID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ApplyID",DbType.Int64,_CertificateOutApplyMDL.ApplyID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="CertificateOutApplyID">主键</param>
        public static CertificateOutApplyMDL GetObject( long ApplyID )
		{
			string sql=@"
			SELECT ApplyID,ApplyTime,WorkerCertificateCode,checkRtnCode,checkInfo,checkTime
			FROM dbo.CertificateOutApply
			WHERE ApplyID = @ApplyID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ApplyID",DbType.Int64,ApplyID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                CertificateOutApplyMDL _CertificateOutApplyMDL = null;
                if (reader.Read())
                {
                    _CertificateOutApplyMDL = new CertificateOutApplyMDL();
					if (reader["ApplyID"] != DBNull.Value) _CertificateOutApplyMDL.ApplyID = Convert.ToInt64(reader["ApplyID"]);
					if (reader["ApplyTime"] != DBNull.Value) _CertificateOutApplyMDL.ApplyTime = Convert.ToDateTime(reader["ApplyTime"]);
					if (reader["WorkerCertificateCode"] != DBNull.Value) _CertificateOutApplyMDL.WorkerCertificateCode = Convert.ToString(reader["WorkerCertificateCode"]);
					if (reader["checkRtnCode"] != DBNull.Value) _CertificateOutApplyMDL.checkRtnCode = Convert.ToInt32(reader["checkRtnCode"]);
					if (reader["checkInfo"] != DBNull.Value) _CertificateOutApplyMDL.checkInfo = Convert.ToString(reader["checkInfo"]);
					if (reader["checkTime"] != DBNull.Value) _CertificateOutApplyMDL.checkTime = Convert.ToDateTime(reader["checkTime"]);
                }
				reader.Close();
                db.Close();
                return _CertificateOutApplyMDL;
            }
		}

        /// <summary>
        /// 获取当日，本人申请的同步记录
        /// </summary>
        /// <param name="WorkerCertificateCode">身份证号</param>
        public static CertificateOutApplyMDL GetObjectToday(string WorkerCertificateCode)
        {
            string sql = string.Format(@"
			SELECT top 1 ApplyID,ApplyTime,WorkerCertificateCode,checkRtnCode,checkInfo,checkTime
			FROM dbo.CertificateOutApply
			WHERE WorkerCertificateCode = @WorkerCertificateCode and ApplyTime > '{0}' order by ApplyTime desc", DateTime.Now.ToString("yyyy-MM-dd"));

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("WorkerCertificateCode", DbType.Int64, WorkerCertificateCode));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                CertificateOutApplyMDL _CertificateOutApplyMDL = null;
                if (reader.Read())
                {
                    _CertificateOutApplyMDL = new CertificateOutApplyMDL();
                    if (reader["ApplyID"] != DBNull.Value) _CertificateOutApplyMDL.ApplyID = Convert.ToInt64(reader["ApplyID"]);
                    if (reader["ApplyTime"] != DBNull.Value) _CertificateOutApplyMDL.ApplyTime = Convert.ToDateTime(reader["ApplyTime"]);
                    if (reader["WorkerCertificateCode"] != DBNull.Value) _CertificateOutApplyMDL.WorkerCertificateCode = Convert.ToString(reader["WorkerCertificateCode"]);
                    if (reader["checkRtnCode"] != DBNull.Value) _CertificateOutApplyMDL.checkRtnCode = Convert.ToInt32(reader["checkRtnCode"]);
                    if (reader["checkInfo"] != DBNull.Value) _CertificateOutApplyMDL.checkInfo = Convert.ToString(reader["checkInfo"]);
                    if (reader["checkTime"] != DBNull.Value) _CertificateOutApplyMDL.checkTime = Convert.ToDateTime(reader["checkTime"]);
                }
                reader.Close();
                db.Close();
                return _CertificateOutApplyMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.CertificateOutApply", "*", filterWhereString, orderBy == "" ? " ApplyID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.CertificateOutApply", filterWhereString);
        }
        
        #region 自定义方法
        
        #endregion
    }
}
