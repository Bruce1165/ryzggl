using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--ReEducateCertificateDAL(填写类描述)
	/// </summary>
    public class ReEducateCertificateDAL
    {
        public ReEducateCertificateDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_ReEducateCertificateMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(ReEducateCertificateMDL _ReEducateCertificateMDL)
		{
		    return Insert(null,_ReEducateCertificateMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_ReEducateCertificateMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,ReEducateCertificateMDL _ReEducateCertificateMDL)
		{
            DBHelper db = new DBHelper("DBRYPX");		
			string sql = @"
			INSERT INTO dbo.ReEducateCertificate(ReEducateCertificateID,CertificateCode,PostTypeName,PostName,ConferDate,ValidEndDate,WorkerName,WorkerCertificateCode,ReEducateCertificateCode,ReEducateConferDate,Period,PackageID,PackageTitle,TestStatus,PackageYear)
			VALUES (@ReEducateCertificateID,@CertificateCode,@PostTypeName,@PostName,@ConferDate,@ValidEndDate,@WorkerName,@WorkerCertificateCode,@ReEducateCertificateCode,@ReEducateConferDate,@Period,@PackageID,@PackageTitle,@TestStatus,@PackageYear)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ReEducateCertificateID",DbType.Int64, _ReEducateCertificateMDL.ReEducateCertificateID));
			p.Add(db.CreateParameter("CertificateCode",DbType.String, _ReEducateCertificateMDL.CertificateCode));
			p.Add(db.CreateParameter("PostTypeName",DbType.String, _ReEducateCertificateMDL.PostTypeName));
			p.Add(db.CreateParameter("PostName",DbType.String, _ReEducateCertificateMDL.PostName));
			p.Add(db.CreateParameter("ConferDate",DbType.DateTime, _ReEducateCertificateMDL.ConferDate));
			p.Add(db.CreateParameter("ValidEndDate",DbType.DateTime, _ReEducateCertificateMDL.ValidEndDate));
			p.Add(db.CreateParameter("WorkerName",DbType.String, _ReEducateCertificateMDL.WorkerName));
			p.Add(db.CreateParameter("WorkerCertificateCode",DbType.String, _ReEducateCertificateMDL.WorkerCertificateCode));
			p.Add(db.CreateParameter("ReEducateCertificateCode",DbType.String, _ReEducateCertificateMDL.ReEducateCertificateCode));
			p.Add(db.CreateParameter("ReEducateConferDate",DbType.DateTime, _ReEducateCertificateMDL.ReEducateConferDate));
			p.Add(db.CreateParameter("Period",DbType.Int32, _ReEducateCertificateMDL.Period));
			p.Add(db.CreateParameter("PackageID",DbType.Int64, _ReEducateCertificateMDL.PackageID));
			p.Add(db.CreateParameter("PackageTitle",DbType.String, _ReEducateCertificateMDL.PackageTitle));
			p.Add(db.CreateParameter("TestStatus",DbType.String, _ReEducateCertificateMDL.TestStatus));
			p.Add(db.CreateParameter("PackageYear",DbType.Int32, _ReEducateCertificateMDL.PackageYear));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_ReEducateCertificateMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(ReEducateCertificateMDL _ReEducateCertificateMDL)
		{
			return Update(null,_ReEducateCertificateMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_ReEducateCertificateMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,ReEducateCertificateMDL _ReEducateCertificateMDL)
		{
			string sql = @"
			UPDATE dbo.ReEducateCertificate
				SET	CertificateCode = @CertificateCode,PostTypeName = @PostTypeName,PostName = @PostName,ConferDate = @ConferDate,ValidEndDate = @ValidEndDate,WorkerName = @WorkerName,WorkerCertificateCode = @WorkerCertificateCode,ReEducateCertificateCode = @ReEducateCertificateCode,ReEducateConferDate = @ReEducateConferDate,Period = @Period,PackageID = @PackageID,PackageTitle = @PackageTitle,TestStatus = @TestStatus,PackageYear = @PackageYear
			WHERE
				ReEducateCertificateID = @ReEducateCertificateID";

            DBHelper db = new DBHelper("DBRYPX");
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ReEducateCertificateID",DbType.Int64, _ReEducateCertificateMDL.ReEducateCertificateID));
			p.Add(db.CreateParameter("CertificateCode",DbType.String, _ReEducateCertificateMDL.CertificateCode));
			p.Add(db.CreateParameter("PostTypeName",DbType.String, _ReEducateCertificateMDL.PostTypeName));
			p.Add(db.CreateParameter("PostName",DbType.String, _ReEducateCertificateMDL.PostName));
			p.Add(db.CreateParameter("ConferDate",DbType.DateTime, _ReEducateCertificateMDL.ConferDate));
			p.Add(db.CreateParameter("ValidEndDate",DbType.DateTime, _ReEducateCertificateMDL.ValidEndDate));
			p.Add(db.CreateParameter("WorkerName",DbType.String, _ReEducateCertificateMDL.WorkerName));
			p.Add(db.CreateParameter("WorkerCertificateCode",DbType.String, _ReEducateCertificateMDL.WorkerCertificateCode));
			p.Add(db.CreateParameter("ReEducateCertificateCode",DbType.String, _ReEducateCertificateMDL.ReEducateCertificateCode));
			p.Add(db.CreateParameter("ReEducateConferDate",DbType.DateTime, _ReEducateCertificateMDL.ReEducateConferDate));
			p.Add(db.CreateParameter("Period",DbType.Int32, _ReEducateCertificateMDL.Period));
			p.Add(db.CreateParameter("PackageID",DbType.Int64, _ReEducateCertificateMDL.PackageID));
			p.Add(db.CreateParameter("PackageTitle",DbType.String, _ReEducateCertificateMDL.PackageTitle));
			p.Add(db.CreateParameter("TestStatus",DbType.String, _ReEducateCertificateMDL.TestStatus));
			p.Add(db.CreateParameter("PackageYear",DbType.Int32, _ReEducateCertificateMDL.PackageYear));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="ReEducateCertificateID">主键</param>
		/// <returns></returns>
        public static int Delete( long ReEducateCertificateID )
		{
			return Delete(null, ReEducateCertificateID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="ReEducateCertificateID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, long ReEducateCertificateID)
		{
			string sql=@"DELETE FROM dbo.ReEducateCertificate WHERE ReEducateCertificateID = @ReEducateCertificateID";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ReEducateCertificateID",DbType.Int64,ReEducateCertificateID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="_ReEducateCertificateMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(ReEducateCertificateMDL _ReEducateCertificateMDL)
		{
			return Delete(null,_ReEducateCertificateMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_ReEducateCertificateMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,ReEducateCertificateMDL _ReEducateCertificateMDL)
		{
			string sql=@"DELETE FROM dbo.ReEducateCertificate WHERE ReEducateCertificateID = @ReEducateCertificateID";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ReEducateCertificateID",DbType.Int64,_ReEducateCertificateMDL.ReEducateCertificateID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ReEducateCertificateID">主键</param>
        public static ReEducateCertificateMDL GetObject( long ReEducateCertificateID )
		{
			string sql=@"
			SELECT ReEducateCertificateID,CertificateCode,PostTypeName,PostName,ConferDate,ValidEndDate,WorkerName,WorkerCertificateCode,ReEducateCertificateCode,ReEducateConferDate,Period,PackageID,PackageTitle,TestStatus,PackageYear
			FROM dbo.ReEducateCertificate
			WHERE ReEducateCertificateID = @ReEducateCertificateID";

            DBHelper db = new DBHelper("DBRYPX");
            //List<SqlParameter> p = new List<SqlParameter>();
            //p.Add(db.CreateParameter("ReEducateCertificateID", DbType.Int64, ReEducateCertificateID));
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ReEducateCertificateID",DbType.Int64,ReEducateCertificateID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                ReEducateCertificateMDL _ReEducateCertificateMDL = null;
                if (reader.Read())
                {
                    _ReEducateCertificateMDL = new ReEducateCertificateMDL();
					if (reader["ReEducateCertificateID"] != DBNull.Value) _ReEducateCertificateMDL.ReEducateCertificateID = Convert.ToInt64(reader["ReEducateCertificateID"]);
					if (reader["CertificateCode"] != DBNull.Value) _ReEducateCertificateMDL.CertificateCode = Convert.ToString(reader["CertificateCode"]);
					if (reader["PostTypeName"] != DBNull.Value) _ReEducateCertificateMDL.PostTypeName = Convert.ToString(reader["PostTypeName"]);
					if (reader["PostName"] != DBNull.Value) _ReEducateCertificateMDL.PostName = Convert.ToString(reader["PostName"]);
					if (reader["ConferDate"] != DBNull.Value) _ReEducateCertificateMDL.ConferDate = Convert.ToDateTime(reader["ConferDate"]);
					if (reader["ValidEndDate"] != DBNull.Value) _ReEducateCertificateMDL.ValidEndDate = Convert.ToDateTime(reader["ValidEndDate"]);
					if (reader["WorkerName"] != DBNull.Value) _ReEducateCertificateMDL.WorkerName = Convert.ToString(reader["WorkerName"]);
					if (reader["WorkerCertificateCode"] != DBNull.Value) _ReEducateCertificateMDL.WorkerCertificateCode = Convert.ToString(reader["WorkerCertificateCode"]);
					if (reader["ReEducateCertificateCode"] != DBNull.Value) _ReEducateCertificateMDL.ReEducateCertificateCode = Convert.ToString(reader["ReEducateCertificateCode"]);
					if (reader["ReEducateConferDate"] != DBNull.Value) _ReEducateCertificateMDL.ReEducateConferDate = Convert.ToDateTime(reader["ReEducateConferDate"]);
					if (reader["Period"] != DBNull.Value) _ReEducateCertificateMDL.Period = Convert.ToInt32(reader["Period"]);
					if (reader["PackageID"] != DBNull.Value) _ReEducateCertificateMDL.PackageID = Convert.ToInt64(reader["PackageID"]);
					if (reader["PackageTitle"] != DBNull.Value) _ReEducateCertificateMDL.PackageTitle = Convert.ToString(reader["PackageTitle"]);
					if (reader["TestStatus"] != DBNull.Value) _ReEducateCertificateMDL.TestStatus = Convert.ToString(reader["TestStatus"]);
					if (reader["PackageYear"] != DBNull.Value) _ReEducateCertificateMDL.PackageYear = Convert.ToInt32(reader["PackageYear"]);
                }
				reader.Close();
                db.Close();
                return _ReEducateCertificateMDL;
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
            return CommonDAL.GetDataTableDB("DBRYPX",startRowIndex, maximumRows, "dbo.ReEducateCertificate", "*", filterWhereString, orderBy == "" ? " ReEducateCertificateID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCountDB("DBRYPX","dbo.ReEducateCertificate", filterWhereString);
        }
        
        #region 自定义方法
        
        #endregion
    }
}
