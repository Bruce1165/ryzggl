using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--FinishCertDAL(填写类描述)
	/// </summary>
    public class FinishCertDAL
    {
        public FinishCertDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_FinishCertMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(FinishCertMDL _FinishCertMDL)
		{
		    return Insert(null,_FinishCertMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_FinishCertMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,FinishCertMDL _FinishCertMDL)
		{
            DBHelper db = new DBHelper("DBRYPX");		
			string sql = @"
			INSERT INTO dbo.FinishCert(CertificateCode,ValidEndDate,PostTypeName,PostName,WorkerCertificateCode,WorkerName,CreateTime,Period,FinishPeriod,FinishDate,StudyStatus)
			VALUES (@CertificateCode,@ValidEndDate,@PostTypeName,@PostName,@WorkerCertificateCode,@WorkerName,@CreateTime,@Period,@FinishPeriod,@FinishDate,@StudyStatus)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("CertificateCode",DbType.String, _FinishCertMDL.CertificateCode));
			p.Add(db.CreateParameter("ValidEndDate",DbType.DateTime, _FinishCertMDL.ValidEndDate));
			p.Add(db.CreateParameter("PostTypeName",DbType.String, _FinishCertMDL.PostTypeName));
			p.Add(db.CreateParameter("PostName",DbType.String, _FinishCertMDL.PostName));
			p.Add(db.CreateParameter("WorkerCertificateCode",DbType.String, _FinishCertMDL.WorkerCertificateCode));
			p.Add(db.CreateParameter("WorkerName",DbType.String, _FinishCertMDL.WorkerName));
			p.Add(db.CreateParameter("CreateTime",DbType.DateTime, _FinishCertMDL.CreateTime));
			p.Add(db.CreateParameter("Period",DbType.Int32, _FinishCertMDL.Period));
			p.Add(db.CreateParameter("FinishPeriod",DbType.Int32, _FinishCertMDL.FinishPeriod));
			p.Add(db.CreateParameter("FinishDate",DbType.DateTime, _FinishCertMDL.FinishDate));
			p.Add(db.CreateParameter("StudyStatus",DbType.Int32, _FinishCertMDL.StudyStatus));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_FinishCertMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(FinishCertMDL _FinishCertMDL)
		{
			return Update(null,_FinishCertMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_FinishCertMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,FinishCertMDL _FinishCertMDL)
		{
			string sql = @"
			UPDATE dbo.FinishCert
				SET	PostTypeName = @PostTypeName,PostName = @PostName,WorkerCertificateCode = @WorkerCertificateCode,WorkerName = @WorkerName,CreateTime = @CreateTime,Period = @Period,FinishPeriod = @FinishPeriod,FinishDate = @FinishDate,StudyStatus = @StudyStatus
			WHERE
				CertificateCode = @CertificateCode AND ValidEndDate = @ValidEndDate";

            DBHelper db = new DBHelper("DBRYPX");
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("CertificateCode",DbType.String, _FinishCertMDL.CertificateCode));
			p.Add(db.CreateParameter("ValidEndDate",DbType.DateTime, _FinishCertMDL.ValidEndDate));
			p.Add(db.CreateParameter("PostTypeName",DbType.String, _FinishCertMDL.PostTypeName));
			p.Add(db.CreateParameter("PostName",DbType.String, _FinishCertMDL.PostName));
			p.Add(db.CreateParameter("WorkerCertificateCode",DbType.String, _FinishCertMDL.WorkerCertificateCode));
			p.Add(db.CreateParameter("WorkerName",DbType.String, _FinishCertMDL.WorkerName));
			p.Add(db.CreateParameter("CreateTime",DbType.DateTime, _FinishCertMDL.CreateTime));
			p.Add(db.CreateParameter("Period",DbType.Int32, _FinishCertMDL.Period));
			p.Add(db.CreateParameter("FinishPeriod",DbType.Int32, _FinishCertMDL.FinishPeriod));
			p.Add(db.CreateParameter("FinishDate",DbType.DateTime, _FinishCertMDL.FinishDate));
			p.Add(db.CreateParameter("StudyStatus",DbType.Int32, _FinishCertMDL.StudyStatus));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="FinishCertID">主键</param>
		/// <returns></returns>
        public static int Delete( string CertificateCode, DateTime ValidEndDate )
		{
			return Delete(null, CertificateCode, ValidEndDate);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="FinishCertID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, string CertificateCode, DateTime ValidEndDate)
		{
			string sql=@"DELETE FROM dbo.FinishCert WHERE CertificateCode = @CertificateCode AND ValidEndDate = @ValidEndDate";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("CertificateCode",DbType.String,CertificateCode));
			p.Add(db.CreateParameter("ValidEndDate",DbType.DateTime,ValidEndDate));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="_FinishCertMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(FinishCertMDL _FinishCertMDL)
		{
			return Delete(null,_FinishCertMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_FinishCertMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,FinishCertMDL _FinishCertMDL)
		{
			string sql=@"DELETE FROM dbo.FinishCert WHERE CertificateCode = @CertificateCode AND ValidEndDate = @ValidEndDate";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("CertificateCode",DbType.String,_FinishCertMDL.CertificateCode));
			p.Add(db.CreateParameter("ValidEndDate",DbType.DateTime,_FinishCertMDL.ValidEndDate));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="CertificateCode">证书编号</param>
        /// <param name="ValidEndDate">有效期至</param>
        ///  <returns></returns>
        public static FinishCertMDL GetObject( string CertificateCode, DateTime ValidEndDate )
		{
			string sql=@"
			SELECT CertificateCode,ValidEndDate,PostTypeName,PostName,WorkerCertificateCode,WorkerName,CreateTime,Period,FinishPeriod,FinishDate,StudyStatus
			FROM dbo.FinishCert
			WHERE CertificateCode = @CertificateCode AND ValidEndDate = @ValidEndDate";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("CertificateCode",DbType.String,CertificateCode));
			p.Add(db.CreateParameter("ValidEndDate",DbType.DateTime,ValidEndDate));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                FinishCertMDL _FinishCertMDL = null;
                if (reader.Read())
                {
                    _FinishCertMDL = new FinishCertMDL();
					if (reader["CertificateCode"] != DBNull.Value) _FinishCertMDL.CertificateCode = Convert.ToString(reader["CertificateCode"]);
					if (reader["ValidEndDate"] != DBNull.Value) _FinishCertMDL.ValidEndDate = Convert.ToDateTime(reader["ValidEndDate"]);
					if (reader["PostTypeName"] != DBNull.Value) _FinishCertMDL.PostTypeName = Convert.ToString(reader["PostTypeName"]);
					if (reader["PostName"] != DBNull.Value) _FinishCertMDL.PostName = Convert.ToString(reader["PostName"]);
					if (reader["WorkerCertificateCode"] != DBNull.Value) _FinishCertMDL.WorkerCertificateCode = Convert.ToString(reader["WorkerCertificateCode"]);
					if (reader["WorkerName"] != DBNull.Value) _FinishCertMDL.WorkerName = Convert.ToString(reader["WorkerName"]);
					if (reader["CreateTime"] != DBNull.Value) _FinishCertMDL.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
					if (reader["Period"] != DBNull.Value) _FinishCertMDL.Period = Convert.ToInt32(reader["Period"]);
					if (reader["FinishPeriod"] != DBNull.Value) _FinishCertMDL.FinishPeriod = Convert.ToInt32(reader["FinishPeriod"]);
					if (reader["FinishDate"] != DBNull.Value) _FinishCertMDL.FinishDate = Convert.ToDateTime(reader["FinishDate"]);
					if (reader["StudyStatus"] != DBNull.Value) _FinishCertMDL.StudyStatus = Convert.ToInt32(reader["StudyStatus"]);
                }
				reader.Close();
                db.Close();
                return _FinishCertMDL;
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
            return CommonDAL.GetDataTableDB("DBRYPX", startRowIndex, maximumRows, "dbo.FinishCert", "*", filterWhereString, orderBy == "" ? " CertificateCode, ValidEndDate" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCountDB("DBRYPX", "dbo.FinishCert", filterWhereString);
        }
        
        #region 自定义方法
        
        #endregion
    }
}
