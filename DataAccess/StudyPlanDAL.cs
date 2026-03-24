using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--StudyPlanDAL(填写类描述)
	/// </summary>
    public class StudyPlanDAL
    {
        public StudyPlanDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_StudyPlanMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(StudyPlanMDL _StudyPlanMDL)
		{
		    return Insert(null,_StudyPlanMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_StudyPlanMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,StudyPlanMDL _StudyPlanMDL)
		{
            DBHelper db = new DBHelper("DBRYPX");
            string sql = @"
			INSERT INTO dbo.StudyPlan(WorkerCertificateCode,PackageID,WorkerName,CreateTime,Creater,AddType,FinishDate,TestStatus)
			VALUES (@WorkerCertificateCode,@PackageID,@WorkerName,@CreateTime,@Creater,@AddType,@FinishDate,@TestStatus)";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("WorkerCertificateCode", DbType.String, _StudyPlanMDL.WorkerCertificateCode));
            p.Add(db.CreateParameter("PackageID", DbType.Int64, _StudyPlanMDL.PackageID));
            p.Add(db.CreateParameter("WorkerName", DbType.String, _StudyPlanMDL.WorkerName));
            p.Add(db.CreateParameter("CreateTime", DbType.DateTime, _StudyPlanMDL.CreateTime));
            p.Add(db.CreateParameter("Creater", DbType.String, _StudyPlanMDL.Creater));
            p.Add(db.CreateParameter("AddType", DbType.String, _StudyPlanMDL.AddType));
            p.Add(db.CreateParameter("FinishDate", DbType.DateTime, _StudyPlanMDL.FinishDate));
            p.Add(db.CreateParameter("TestStatus", DbType.String, _StudyPlanMDL.TestStatus));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_StudyPlanMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(StudyPlanMDL _StudyPlanMDL)
		{
			return Update(null,_StudyPlanMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_StudyPlanMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,StudyPlanMDL _StudyPlanMDL)
		{
            string sql = @"
			UPDATE dbo.StudyPlan
				SET	WorkerName = @WorkerName,CreateTime = @CreateTime,Creater = @Creater,AddType = @AddType,FinishDate = @FinishDate,TestStatus = @TestStatus
			WHERE
				WorkerCertificateCode = @WorkerCertificateCode AND PackageID = @PackageID";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("WorkerCertificateCode", DbType.String, _StudyPlanMDL.WorkerCertificateCode));
            p.Add(db.CreateParameter("PackageID", DbType.Int64, _StudyPlanMDL.PackageID));
            p.Add(db.CreateParameter("WorkerName", DbType.String, _StudyPlanMDL.WorkerName));
            p.Add(db.CreateParameter("CreateTime", DbType.DateTime, _StudyPlanMDL.CreateTime));
            p.Add(db.CreateParameter("Creater", DbType.String, _StudyPlanMDL.Creater));
            p.Add(db.CreateParameter("AddType", DbType.String, _StudyPlanMDL.AddType));
            p.Add(db.CreateParameter("FinishDate", DbType.DateTime, _StudyPlanMDL.FinishDate));
            p.Add(db.CreateParameter("TestStatus", DbType.String, _StudyPlanMDL.TestStatus));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="StudyPlanID">主键</param>
		/// <returns></returns>
        public static int Delete( string WorkerCertificateCode, long PackageID )
		{
			return Delete(null, WorkerCertificateCode, PackageID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="StudyPlanID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, string WorkerCertificateCode, long PackageID)
		{
			string sql=@"DELETE FROM dbo.StudyPlan WHERE WorkerCertificateCode = @WorkerCertificateCode AND PackageID = @PackageID";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("WorkerCertificateCode",DbType.String,WorkerCertificateCode));
			p.Add(db.CreateParameter("PackageID",DbType.Int64,PackageID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="_StudyPlanMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(StudyPlanMDL _StudyPlanMDL)
		{
			return Delete(null,_StudyPlanMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_StudyPlanMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,StudyPlanMDL _StudyPlanMDL)
		{
			string sql=@"DELETE FROM dbo.StudyPlan WHERE WorkerCertificateCode = @WorkerCertificateCode AND PackageID = @PackageID";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("WorkerCertificateCode",DbType.String,_StudyPlanMDL.WorkerCertificateCode));
			p.Add(db.CreateParameter("PackageID",DbType.Int64,_StudyPlanMDL.PackageID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="StudyPlanID">主键</param>
        public static StudyPlanMDL GetObject( string WorkerCertificateCode, long PackageID )
		{
            string sql = @"
			SELECT WorkerCertificateCode,PackageID,WorkerName,CreateTime,Creater,AddType,FinishDate,TestStatus
			FROM dbo.StudyPlan
			WHERE WorkerCertificateCode = @WorkerCertificateCode AND PackageID = @PackageID";

            DBHelper db = new DBHelper("DBRYPX");
            //List<SqlParameter> p = new List<SqlParameter>();
            //p.Add(db.CreateParameter("WorkerCertificateCode", DbType.String, WorkerCertificateCode));
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("WorkerCertificateCode",DbType.String,WorkerCertificateCode));
			p.Add(db.CreateParameter("PackageID",DbType.Int64,PackageID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                StudyPlanMDL _StudyPlanMDL = null;
                if (reader.Read())
                {
                    _StudyPlanMDL = new StudyPlanMDL();
					if (reader["WorkerCertificateCode"] != DBNull.Value) _StudyPlanMDL.WorkerCertificateCode = Convert.ToString(reader["WorkerCertificateCode"]);
					if (reader["PackageID"] != DBNull.Value) _StudyPlanMDL.PackageID = Convert.ToInt64(reader["PackageID"]);
					if (reader["WorkerName"] != DBNull.Value) _StudyPlanMDL.WorkerName = Convert.ToString(reader["WorkerName"]);
					if (reader["CreateTime"] != DBNull.Value) _StudyPlanMDL.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
					if (reader["Creater"] != DBNull.Value) _StudyPlanMDL.Creater = Convert.ToString(reader["Creater"]);
					if (reader["AddType"] != DBNull.Value) _StudyPlanMDL.AddType = Convert.ToString(reader["AddType"]);
                    if (reader["FinishDate"] != DBNull.Value) _StudyPlanMDL.FinishDate = Convert.ToDateTime(reader["FinishDate"]);
                    if (reader["TestStatus"] != DBNull.Value) _StudyPlanMDL.TestStatus = Convert.ToString(reader["TestStatus"]);
                }
				reader.Close();
                db.Close();
                return _StudyPlanMDL;
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
            return CommonDAL.GetDataTableDB("DBRYPX",startRowIndex, maximumRows, "dbo.StudyPlan", "*", filterWhereString, orderBy == "" ? " WorkerCertificateCode, PackageID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCountDB("DBRYPX","dbo.StudyPlan", filterWhereString);
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
            return CommonDAL.GetDataTableDB("DBRYPX", startRowIndex, maximumRows, "dbo.View_StudyPlan", "*", filterWhereString, orderBy == "" ? " WorkerCertificateCode, PackageID" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountView(string filterWhereString)
        {
            return CommonDAL.SelectRowCountDB("DBRYPX", "dbo.View_StudyPlan", filterWhereString);
        }
        
        #region 自定义方法
        
        #endregion
    }
}
