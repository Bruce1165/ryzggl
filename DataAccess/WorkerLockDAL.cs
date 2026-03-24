using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--WorkerLockDAL(填写类描述)
	/// </summary>
    public class WorkerLockDAL
    {
        public WorkerLockDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="WorkerLockOB">对象实体类</param>
		/// <returns></returns>
        public static long Insert(WorkerLockOB _WorkerLockOB)
		{
		    return Insert(null,_WorkerLockOB);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="WorkerLockOB">对象实体类</param>
		/// <returns></returns>
        public static long Insert(DbTransaction tran,WorkerLockOB _WorkerLockOB)
		{
			DBHelper db = new DBHelper();
		
			string sql = @"
			INSERT INTO dbo.WorkerLock(WorkerID,LockType,LockTime,LockEndTime,LockPerson,Remark,UnlockTime,UnlockPerson,LockStatus)
			VALUES (@WorkerID,@LockType,@LockTime,@LockEndTime,@LockPerson,@Remark,@UnlockTime,@UnlockPerson,@LockStatus);SELECT @LockID = @@IDENTITY";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateOutParameter("LockID",DbType.Int64));
			p.Add(db.CreateParameter("WorkerID",DbType.Int64, _WorkerLockOB.WorkerID));
			p.Add(db.CreateParameter("LockType",DbType.String, _WorkerLockOB.LockType));
			p.Add(db.CreateParameter("LockTime",DbType.DateTime, _WorkerLockOB.LockTime));
			p.Add(db.CreateParameter("LockEndTime",DbType.DateTime, _WorkerLockOB.LockEndTime));
			p.Add(db.CreateParameter("LockPerson",DbType.String, _WorkerLockOB.LockPerson));
			p.Add(db.CreateParameter("Remark",DbType.String, _WorkerLockOB.Remark));
			p.Add(db.CreateParameter("UnlockTime",DbType.DateTime, _WorkerLockOB.UnlockTime));
			p.Add(db.CreateParameter("UnlockPerson",DbType.String, _WorkerLockOB.UnlockPerson));
			p.Add(db.CreateParameter("LockStatus",DbType.String, _WorkerLockOB.LockStatus));

            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            _WorkerLockOB.LockID = Convert.ToInt64(p[0].Value);
            return rtn;
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="WorkerLockOB">对象实体类</param>
		/// <returns></returns>
        public static int Update(WorkerLockOB _WorkerLockOB)
		{
			return Update(null,_WorkerLockOB);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="WorkerLockOB">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,WorkerLockOB _WorkerLockOB)
		{
			string sql = @"
			UPDATE dbo.WorkerLock
				SET	WorkerID =@WorkerID,LockType =@LockType,LockTime =@LockTime,LockEndTime =@LockEndTime,LockPerson =@LockPerson,Remark =@Remark,UnlockTime =@UnlockTime,UnlockPerson =@UnlockPerson,LockStatus =@LockStatus
			WHERE
				LockID =@LockID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("LockID",DbType.Int64, _WorkerLockOB.LockID));
			p.Add(db.CreateParameter("WorkerID",DbType.Int64, _WorkerLockOB.WorkerID));
			p.Add(db.CreateParameter("LockType",DbType.String, _WorkerLockOB.LockType));
			p.Add(db.CreateParameter("LockTime",DbType.DateTime, _WorkerLockOB.LockTime));
			p.Add(db.CreateParameter("LockEndTime",DbType.DateTime, _WorkerLockOB.LockEndTime));
			p.Add(db.CreateParameter("LockPerson",DbType.String, _WorkerLockOB.LockPerson));
			p.Add(db.CreateParameter("Remark",DbType.String, _WorkerLockOB.Remark));
			p.Add(db.CreateParameter("UnlockTime",DbType.DateTime, _WorkerLockOB.UnlockTime));
			p.Add(db.CreateParameter("UnlockPerson",DbType.String, _WorkerLockOB.UnlockPerson));
			p.Add(db.CreateParameter("LockStatus",DbType.String, _WorkerLockOB.LockStatus));
			return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="WorkerLockID">主键</param>
		/// <returns></returns>
        public static int Delete( long LockID )
		{
			return Delete(null, LockID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="WorkerLockID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, long LockID)
		{
			string sql=@"DELETE FROM dbo.WorkerLock WHERE LockID =@LockID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("LockID",DbType.Int64,LockID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="WorkerLockOB">对象实体类</param>
		/// <returns></returns>
        public static int Delete(WorkerLockOB _WorkerLockOB)
		{
			return Delete(null,_WorkerLockOB);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="WorkerLockOB">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,WorkerLockOB _WorkerLockOB)
		{
			string sql=@"DELETE FROM dbo.WorkerLock WHERE LockID =@LockID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("LockID",DbType.Int64,_WorkerLockOB.LockID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="WorkerLockID">主键</param>
        public static WorkerLockOB GetObject( long LockID )
		{
			string sql=@"
			SELECT LockID,WorkerID,LockType,LockTime,LockEndTime,LockPerson,Remark,UnlockTime,UnlockPerson,LockStatus
			FROM dbo.WorkerLock
			WHERE LockID =@LockID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("LockID", DbType.Int64, LockID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                WorkerLockOB _WorkerLockOB = null;
                if (reader.Read())
                {
                    _WorkerLockOB = new WorkerLockOB();
					if (reader["LockID"] != DBNull.Value) _WorkerLockOB.LockID = Convert.ToInt64(reader["LockID"]);
					if (reader["WorkerID"] != DBNull.Value) _WorkerLockOB.WorkerID = Convert.ToInt64(reader["WorkerID"]);
					if (reader["LockType"] != DBNull.Value) _WorkerLockOB.LockType = Convert.ToString(reader["LockType"]);
					if (reader["LockTime"] != DBNull.Value) _WorkerLockOB.LockTime = Convert.ToDateTime(reader["LockTime"]);
					if (reader["LockEndTime"] != DBNull.Value) _WorkerLockOB.LockEndTime = Convert.ToDateTime(reader["LockEndTime"]);
					if (reader["LockPerson"] != DBNull.Value) _WorkerLockOB.LockPerson = Convert.ToString(reader["LockPerson"]);
					if (reader["Remark"] != DBNull.Value) _WorkerLockOB.Remark = Convert.ToString(reader["Remark"]);
					if (reader["UnlockTime"] != DBNull.Value) _WorkerLockOB.UnlockTime = Convert.ToDateTime(reader["UnlockTime"]);
					if (reader["UnlockPerson"] != DBNull.Value) _WorkerLockOB.UnlockPerson = Convert.ToString(reader["UnlockPerson"]);
					if (reader["LockStatus"] != DBNull.Value) _WorkerLockOB.LockStatus = Convert.ToString(reader["LockStatus"]);
                }
				reader.Close();
                db.Close();
                return _WorkerLockOB;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.WorkerLock", "*", filterWhereString, orderBy == "" ? " LockTime desc,LockID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.WorkerLock", filterWhereString);
        }


        #region 自定义方法

        /// <summary>
        /// 根据人员ID获取最后创建的锁定信息
        /// </summary>
        /// <param name="WorkerID">人员ID</param>
        public static WorkerLockOB GetLastObject(long WorkerID)
        {
            string sql = @"
			SELECT top 1 LockID,WorkerID,LockType,LockTime,LockEndTime,LockPerson,Remark,UnlockTime,UnlockPerson,LockStatus
			FROM dbo.WorkerLock
			WHERE WorkerID =@WorkerID  order by LockTime desc";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("WorkerID", DbType.Int64, WorkerID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                WorkerLockOB _WorkerLockOB = null;
                if (reader.Read())
                {
                    _WorkerLockOB = new WorkerLockOB();
                    if (reader["LockID"] != DBNull.Value) _WorkerLockOB.LockID = Convert.ToInt64(reader["LockID"]);
                    if (reader["WorkerID"] != DBNull.Value) _WorkerLockOB.WorkerID = Convert.ToInt64(reader["WorkerID"]);
                    if (reader["LockType"] != DBNull.Value) _WorkerLockOB.LockType = Convert.ToString(reader["LockType"]);
                    if (reader["LockTime"] != DBNull.Value) _WorkerLockOB.LockTime = Convert.ToDateTime(reader["LockTime"]);
                    if (reader["LockEndTime"] != DBNull.Value) _WorkerLockOB.LockEndTime = Convert.ToDateTime(reader["LockEndTime"]);
                    if (reader["LockPerson"] != DBNull.Value) _WorkerLockOB.LockPerson = Convert.ToString(reader["LockPerson"]);
                    if (reader["Remark"] != DBNull.Value) _WorkerLockOB.Remark = Convert.ToString(reader["Remark"]);
                    if (reader["UnlockTime"] != DBNull.Value) _WorkerLockOB.UnlockTime = Convert.ToDateTime(reader["UnlockTime"]);
                    if (reader["UnlockPerson"] != DBNull.Value) _WorkerLockOB.UnlockPerson = Convert.ToString(reader["UnlockPerson"]);
                    if (reader["LockStatus"] != DBNull.Value) _WorkerLockOB.LockStatus = Convert.ToString(reader["LockStatus"]);
                }
                reader.Close();
                db.Close();
                return _WorkerLockOB;
            }
        }
        /// <summary>
        /// 根据人员ID获取最后创建的锁定信息
        /// </summary>
        /// <param name="证件号码">WorkerCertificateCode</param>
        public static WorkerLockOB GetLastObject(string WorkerCertificateCode)
        {
            string sql = @"
			SELECT top 1 LockID,WorkerID,LockType,LockTime,LockEndTime,LockPerson,Remark,UnlockTime,UnlockPerson,LockStatus
			FROM dbo.WorkerLock
			WHERE WorkerID in(select  WorkerID from dbo.Worker where CertificateCode=@CertificateCode)
            order by LockTime desc";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("CertificateCode", DbType.String, WorkerCertificateCode));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                WorkerLockOB _WorkerLockOB = null;
                if (reader.Read())
                {
                    _WorkerLockOB = new WorkerLockOB();
                    if (reader["LockID"] != DBNull.Value) _WorkerLockOB.LockID = Convert.ToInt64(reader["LockID"]);
                    if (reader["WorkerID"] != DBNull.Value) _WorkerLockOB.WorkerID = Convert.ToInt64(reader["WorkerID"]);
                    if (reader["LockType"] != DBNull.Value) _WorkerLockOB.LockType = Convert.ToString(reader["LockType"]);
                    if (reader["LockTime"] != DBNull.Value) _WorkerLockOB.LockTime = Convert.ToDateTime(reader["LockTime"]);
                    if (reader["LockEndTime"] != DBNull.Value) _WorkerLockOB.LockEndTime = Convert.ToDateTime(reader["LockEndTime"]);
                    if (reader["LockPerson"] != DBNull.Value) _WorkerLockOB.LockPerson = Convert.ToString(reader["LockPerson"]);
                    if (reader["Remark"] != DBNull.Value) _WorkerLockOB.Remark = Convert.ToString(reader["Remark"]);
                    if (reader["UnlockTime"] != DBNull.Value) _WorkerLockOB.UnlockTime = Convert.ToDateTime(reader["UnlockTime"]);
                    if (reader["UnlockPerson"] != DBNull.Value) _WorkerLockOB.UnlockPerson = Convert.ToString(reader["UnlockPerson"]);
                    if (reader["LockStatus"] != DBNull.Value) _WorkerLockOB.LockStatus = Convert.ToString(reader["LockStatus"]);
                }
                reader.Close();
                db.Close();
                return _WorkerLockOB;
            }
        }

        /// <summary>
        /// 获取人员是否正在锁定中
        /// </summary>
        /// <param name="CertificateID">证书ID</param>
        /// <returns>锁定中返回true，否则返回false</returns>
        public static bool GetWorkerLockStatus(string WorkerCertificateCode)
        {
            //检索最后锁定信息
            WorkerLockOB lockOB = WorkerLockDAL.GetLastObject(WorkerCertificateCode);
            if (lockOB == null //没有锁定记录
                || lockOB.LockEndTime.Value < DateTime.Now //加锁已过期
                || lockOB.UnlockTime.HasValue)//已解锁
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
