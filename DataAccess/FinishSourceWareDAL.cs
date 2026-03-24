using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--FinishSourceWareDAL(填写类描述)
	/// </summary>
    public class FinishSourceWareDAL
    {
        public FinishSourceWareDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_FinishSourceWareMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(FinishSourceWareMDL _FinishSourceWareMDL)
		{
		    return Insert(null,_FinishSourceWareMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_FinishSourceWareMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,FinishSourceWareMDL _FinishSourceWareMDL)
		{
			DBHelper db = new DBHelper("DBRYPX");		
			string sql = @"
			INSERT INTO dbo.FinishSourceWare(WorkerCertificateCode,SourceID,LearnTime,Period,FinishPeriod,WorkerName,StudyStatus,PlayAction)
			VALUES (@WorkerCertificateCode,@SourceID,@LearnTime,@Period,@FinishPeriod,@WorkerName,@StudyStatus,@PlayAction)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("WorkerCertificateCode",DbType.String, _FinishSourceWareMDL.WorkerCertificateCode));
			p.Add(db.CreateParameter("SourceID",DbType.Int64, _FinishSourceWareMDL.SourceID));
			p.Add(db.CreateParameter("LearnTime",DbType.DateTime, _FinishSourceWareMDL.LearnTime));
			p.Add(db.CreateParameter("Period",DbType.Int32, _FinishSourceWareMDL.Period));
			p.Add(db.CreateParameter("FinishPeriod",DbType.Int32, _FinishSourceWareMDL.FinishPeriod));
			p.Add(db.CreateParameter("WorkerName",DbType.String, _FinishSourceWareMDL.WorkerName));
            p.Add(db.CreateParameter("StudyStatus", DbType.Int32, _FinishSourceWareMDL.StudyStatus));
            p.Add(db.CreateParameter("PlayAction", DbType.Int32, _FinishSourceWareMDL.PlayAction));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_FinishSourceWareMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(FinishSourceWareMDL _FinishSourceWareMDL)
        {
            return Update(null, _FinishSourceWareMDL);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_FinishSourceWareMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, FinishSourceWareMDL _FinishSourceWareMDL)
        {
            string sql = @"
			UPDATE dbo.FinishSourceWare
				SET	LearnTime = @LearnTime,Period = @Period,FinishPeriod = @FinishPeriod,WorkerName = @WorkerName,StudyStatus = @StudyStatus,PlayAction = @PlayAction
			WHERE
				WorkerCertificateCode = @WorkerCertificateCode AND SourceID = @SourceID";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("WorkerCertificateCode", DbType.String, _FinishSourceWareMDL.WorkerCertificateCode));
            p.Add(db.CreateParameter("SourceID", DbType.Int64, _FinishSourceWareMDL.SourceID));
            p.Add(db.CreateParameter("LearnTime", DbType.DateTime, _FinishSourceWareMDL.LearnTime));
            p.Add(db.CreateParameter("Period", DbType.Int32, _FinishSourceWareMDL.Period));
            p.Add(db.CreateParameter("FinishPeriod", DbType.Int32, _FinishSourceWareMDL.FinishPeriod));
            p.Add(db.CreateParameter("WorkerName", DbType.String, _FinishSourceWareMDL.WorkerName));
            p.Add(db.CreateParameter("StudyStatus", DbType.Int32, _FinishSourceWareMDL.StudyStatus));
            p.Add(db.CreateParameter("PlayAction", DbType.Int32, _FinishSourceWareMDL.PlayAction));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="FinishSourceWareID">主键</param>
        /// <returns></returns>
        public static int Delete(string WorkerCertificateCode, long SourceID)
        {
            return Delete(null, WorkerCertificateCode, SourceID);
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="FinishSourceWareID">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, string WorkerCertificateCode, long SourceID)
        {
            //先拷贝学习记录到历史表，再删除生产表
            string sql = @"
INSERT INTO [dbo].[FinishSourceWareHis]([HisDate],[WorkerCertificateCode],[SourceID],[LearnTime],[Period],[FinishPeriod],[WorkerName],[StudyStatus],[PlayAction])
select  getdate(),[WorkerCertificateCode],[SourceID],[LearnTime],[Period],[FinishPeriod],[WorkerName],[StudyStatus],[PlayAction]
from dbo.FinishSourceWare 
WHERE WorkerCertificateCode = @WorkerCertificateCode AND SourceID = @SourceID;
DELETE FROM dbo.FinishSourceWare WHERE WorkerCertificateCode = @WorkerCertificateCode AND SourceID = @SourceID";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("WorkerCertificateCode", DbType.String, WorkerCertificateCode));
            p.Add(db.CreateParameter("SourceID", DbType.Int64, SourceID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="_FinishSourceWareMDL">对象实体类</param>
        /// <returns></returns>
        public static int Delete(FinishSourceWareMDL _FinishSourceWareMDL)
        {
            return Delete(null, _FinishSourceWareMDL);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_FinishSourceWareMDL">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, FinishSourceWareMDL _FinishSourceWareMDL)
        {
            string sql = @"DELETE FROM dbo.FinishSourceWare WHERE WorkerCertificateCode = @WorkerCertificateCode AND SourceID = @SourceID";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("WorkerCertificateCode", DbType.String, _FinishSourceWareMDL.WorkerCertificateCode));
            p.Add(db.CreateParameter("SourceID", DbType.Int64, _FinishSourceWareMDL.SourceID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="FinishSourceWareID">主键</param>
        public static FinishSourceWareMDL GetObject(string WorkerCertificateCode, long SourceID)
        {
            string sql = @"
			SELECT WorkerCertificateCode,SourceID,LearnTime,Period,FinishPeriod,WorkerName,StudyStatus,PlayAction
			FROM dbo.FinishSourceWare
			WHERE WorkerCertificateCode = @WorkerCertificateCode AND SourceID = @SourceID";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("WorkerCertificateCode", DbType.String, WorkerCertificateCode));
            p.Add(db.CreateParameter("SourceID", DbType.Int64, SourceID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                FinishSourceWareMDL _FinishSourceWareMDL = null;
                if (reader.Read())
                {
                    _FinishSourceWareMDL = new FinishSourceWareMDL();
                    if (reader["WorkerCertificateCode"] != DBNull.Value) _FinishSourceWareMDL.WorkerCertificateCode = Convert.ToString(reader["WorkerCertificateCode"]);
                    if (reader["SourceID"] != DBNull.Value) _FinishSourceWareMDL.SourceID = Convert.ToInt64(reader["SourceID"]);
                    if (reader["LearnTime"] != DBNull.Value) _FinishSourceWareMDL.LearnTime = Convert.ToDateTime(reader["LearnTime"]);
                    if (reader["Period"] != DBNull.Value) _FinishSourceWareMDL.Period = Convert.ToInt32(reader["Period"]);
                    if (reader["FinishPeriod"] != DBNull.Value) _FinishSourceWareMDL.FinishPeriod = Convert.ToInt32(reader["FinishPeriod"]);
                    if (reader["WorkerName"] != DBNull.Value) _FinishSourceWareMDL.WorkerName = Convert.ToString(reader["WorkerName"]);
                    if (reader["StudyStatus"] != DBNull.Value) _FinishSourceWareMDL.StudyStatus = Convert.ToInt32(reader["StudyStatus"]);
                    if (reader["PlayAction"] != DBNull.Value) _FinishSourceWareMDL.PlayAction = Convert.ToInt32(reader["PlayAction"]);
                }
                reader.Close();
                db.Close();
                return _FinishSourceWareMDL;
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
            return CommonDAL.GetDataTableDB("DBRYPX", startRowIndex, maximumRows, "dbo.FinishSourceWare", "*", filterWhereString, orderBy == "" ? " WorkerCertificateCode, SourceID" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCountDB("DBRYPX","dbo.FinishSourceWare", filterWhereString);
        }

        #region 自定义方法

        /// <summary>
        /// 更新课程测试结果为通过
        /// </summary>
        /// <param name="WorkerCertificateCode">证件号码</param>
        /// <param name="ParentSourceID">课程ID</param>
        /// <returns></returns>
        public static int UpdateSourceTestStatus(string WorkerCertificateCode, long ParentSourceID)
        {
            string sql = @"
			UPDATE dbo.FinishSourceWare
				SET	[StudyStatus] = 1
			WHERE
				WorkerCertificateCode = @WorkerCertificateCode AND SourceID in(select SourceID from Source where  [ParentSourceID]= @ParentSourceID)";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("WorkerCertificateCode", DbType.String, WorkerCertificateCode));
            p.Add(db.CreateParameter("ParentSourceID", DbType.Int64, ParentSourceID));

            return db.GetExcuteNonQuery(sql, p.ToArray());
        }

        /// <summary>
        /// 获取个人某专业指定时间范围内完成公益培训学时数
        /// </summary>
        /// <param name="WorkerCertificateCode">身份证号码</param>
        /// <param name="PostTypeName">岗位类别（必填）</param>
        /// <param name="PostName">岗位工种（选填）</param>
        /// <param name="StartDate">查询开始日期</param>
        /// <param name="EndDate">查询截止日期</param>
        /// <param name="EndDate">查询截止日期</param>
        /// <returns>完成学时数</returns>
        public static decimal GetFinisthPeriod(string WorkerCertificateCode, string PostTypeName, string PostName, DateTime StartDate, DateTime EndDate)
        {
            string sql = @"SELECT isnull(sum(s.ShowPeriod),0) as FinishPeriod
                            FROM [dbo].[FinishSourceWare] f
                            inner join [dbo].[Source] s on f.WorkerCertificateCode='{0}' and  f.SourceID = s.SourceID
                            inner join [dbo].[PackageSource] ps on s.ParentSourceID = ps.SourceID
                            inner join [dbo].[Package] p on ps.PackageID = p.PackageID
                            where f.WorkerCertificateCode='{0}' and p.PostTypeName='{1}' and (p.PostName is null  or  p.PostName='{2}')
                            and f.[LearnTime] between '{3}'  and '{4}' and f.[StudyStatus] =1 and (f.[Period] * 60 <= f.[FinishPeriod])";

            sql = string.Format(sql, WorkerCertificateCode, PostTypeName, PostName, StartDate.ToString("yyyy-MM-dd"), EndDate.ToString("yyyy-MM-dd 23:59:59"));

            return (new DBHelper("DBRYPX")).ExecuteScalar<decimal>(sql);
        }

        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        public static DataTable GetListView(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTableDB("DBRYPX", startRowIndex, maximumRows, "dbo.View_FinishSourceWare", "*", filterWhereString, orderBy == "" ? " [LearnTime] desc, SourceID" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountView(string filterWhereString)
        {
            return CommonDAL.SelectRowCountDB("DBRYPX", "dbo.View_FinishSourceWare", filterWhereString);
        }

        #endregion
    }
}
