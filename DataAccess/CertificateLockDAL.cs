using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
    /// <summary>
    /// 业务类实现--CertificateLockDAL(证书锁定记录)
    /// </summary>
    public class CertificateLockDAL
    {
        public CertificateLockDAL() { }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="CertificateLockOB">对象实体类</param>
        /// <returns></returns>
        public static long Insert(CertificateLockOB _CertificateLockOB)
        {
            return Insert(null, _CertificateLockOB);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="CertificateLockOB">对象实体类</param>
        /// <returns></returns>
        public static long Insert(DbTransaction tran, CertificateLockOB _CertificateLockOB)
        {
            DBHelper db = new DBHelper();       

            string sql = @"
			INSERT INTO dbo.CertificateLock(CertificateID,LockType,LockTime,LockEndTime,LockPerson,Remark,UnlockTime,UnlockPerson,LockStatus)
			VALUES (@CertificateID,@LockType,@LockTime,@LockEndTime,@LockPerson,@Remark,@UnlockTime,@UnlockPerson,@LockStatus);SELECT @LockID = @@IDENTITY";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateOutParameter("LockID", DbType.Int64));
            p.Add(db.CreateParameter("CertificateID", DbType.Int64, _CertificateLockOB.CertificateID));
            p.Add(db.CreateParameter("LockType", DbType.String, _CertificateLockOB.LockType));
            p.Add(db.CreateParameter("LockTime", DbType.DateTime, _CertificateLockOB.LockTime));
            p.Add(db.CreateParameter("LockEndTime", DbType.DateTime, _CertificateLockOB.LockEndTime));
            p.Add(db.CreateParameter("LockPerson", DbType.String, _CertificateLockOB.LockPerson));
            p.Add(db.CreateParameter("Remark", DbType.String, _CertificateLockOB.Remark));
            p.Add(db.CreateParameter("UnlockTime", DbType.DateTime, _CertificateLockOB.UnlockTime));
            p.Add(db.CreateParameter("UnlockPerson", DbType.String, _CertificateLockOB.UnlockPerson));
            p.Add(db.CreateParameter("LockStatus", DbType.String, _CertificateLockOB.LockStatus));
            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            _CertificateLockOB.LockID = Convert.ToInt64(p[0].Value);
            return rtn;
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="CertificateLockOB">对象实体类</param>
        /// <returns></returns>
        public static int Update(CertificateLockOB _CertificateLockOB)
        {
            return Update(null, _CertificateLockOB);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="CertificateLockOB">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, CertificateLockOB _CertificateLockOB)
        {
            string sql = @"
			UPDATE dbo.CertificateLock
				SET	CertificateID = @CertificateID,LockType = @LockType,LockTime = @LockTime,LockEndTime = @LockEndTime,LockPerson = @LockPerson,Remark = @Remark,UnlockTime = @UnlockTime,UnlockPerson = @UnlockPerson,LockStatus = @LockStatus
			WHERE
				LockID = @LockID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("LockID", DbType.Int64, _CertificateLockOB.LockID));
            p.Add(db.CreateParameter("CertificateID", DbType.Int64, _CertificateLockOB.CertificateID));
            p.Add(db.CreateParameter("LockType", DbType.String, _CertificateLockOB.LockType));
            p.Add(db.CreateParameter("LockTime", DbType.DateTime, _CertificateLockOB.LockTime));
            p.Add(db.CreateParameter("LockEndTime", DbType.DateTime, _CertificateLockOB.LockEndTime));
            p.Add(db.CreateParameter("LockPerson", DbType.String, _CertificateLockOB.LockPerson));
            p.Add(db.CreateParameter("Remark", DbType.String, _CertificateLockOB.Remark));
            p.Add(db.CreateParameter("UnlockTime", DbType.DateTime, _CertificateLockOB.UnlockTime));
            p.Add(db.CreateParameter("UnlockPerson", DbType.String, _CertificateLockOB.UnlockPerson));
            p.Add(db.CreateParameter("LockStatus", DbType.String, _CertificateLockOB.LockStatus));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="CertificateLockID">主键</param>
        /// <returns></returns>
        public static int Delete(long LockID)
        {
            return Delete(null, LockID);
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="CertificateLockID">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, long LockID)
        {
            string sql = @"DELETE FROM dbo.CertificateLock WHERE LockID = @LockID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("LockID", DbType.Int64, LockID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="CertificateLockOB">对象实体类</param>
        /// <returns></returns>
        public static int Delete(CertificateLockOB _CertificateLockOB)
        {
            return Delete(null, _CertificateLockOB);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="CertificateLockOB">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, CertificateLockOB _CertificateLockOB)
        {
            string sql = @"DELETE FROM dbo.CertificateLock WHERE LockID = @LockID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("LockID", DbType.Int64, _CertificateLockOB.LockID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="CertificateLockID">主键</param>
        public static CertificateLockOB GetObject(long LockID)
        {
            string sql = @"
			SELECT LockID,CertificateID,LockType,LockTime,LockEndTime,LockPerson,Remark,UnlockTime,UnlockPerson,LockStatus
			FROM dbo.CertificateLock
			WHERE LockID = @LockID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("LockID", DbType.Int64, LockID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                CertificateLockOB _CertificateLockOB = null;
                if (reader.Read())
                {
                    _CertificateLockOB = new CertificateLockOB();
                    if (reader["LockID"] != DBNull.Value) _CertificateLockOB.LockID = Convert.ToInt64(reader["LockID"]);
                    if (reader["CertificateID"] != DBNull.Value) _CertificateLockOB.CertificateID = Convert.ToInt64(reader["CertificateID"]);
                    if (reader["LockType"] != DBNull.Value) _CertificateLockOB.LockType = Convert.ToString(reader["LockType"]);
                    if (reader["LockTime"] != DBNull.Value) _CertificateLockOB.LockTime = Convert.ToDateTime(reader["LockTime"]);
                    if (reader["LockEndTime"] != DBNull.Value) _CertificateLockOB.LockEndTime = Convert.ToDateTime(reader["LockEndTime"]);
                    if (reader["LockPerson"] != DBNull.Value) _CertificateLockOB.LockPerson = Convert.ToString(reader["LockPerson"]);
                    if (reader["Remark"] != DBNull.Value) _CertificateLockOB.Remark = Convert.ToString(reader["Remark"]);
                    if (reader["UnlockTime"] != DBNull.Value) _CertificateLockOB.UnlockTime = Convert.ToDateTime(reader["UnlockTime"]);
                    if (reader["UnlockPerson"] != DBNull.Value) _CertificateLockOB.UnlockPerson = Convert.ToString(reader["UnlockPerson"]);
                    if (reader["LockStatus"] != DBNull.Value) _CertificateLockOB.LockStatus = Convert.ToString(reader["LockStatus"]);
                }
                reader.Close();
                db.Close();
                return _CertificateLockOB;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.CertificateLock", "*", filterWhereString, orderBy == "" ? " LockTime desc,LockID" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.CertificateLock", filterWhereString);
        }

        #region 自定义方法

        /// <summary>
        /// 根据证书ID获取最后创建的锁定信息
        /// </summary>
        /// <param name="CertificateID">证书ID</param>
        public static CertificateLockOB GetLastObject(long CertificateID)
        {
            string sql = @"
			SELECT top 1 LockID,CertificateID,LockType,LockTime,LockEndTime,LockPerson,Remark,UnlockTime,UnlockPerson,LockStatus
			FROM dbo.CertificateLock
			WHERE CertificateID = @CertificateID order by LockTime desc";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("CertificateID", DbType.Int64, CertificateID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                CertificateLockOB _CertificateLockOB = null;
                if (reader.Read())
                {
                    _CertificateLockOB = new CertificateLockOB();
                    if (reader["LockID"] != DBNull.Value) _CertificateLockOB.LockID = Convert.ToInt64(reader["LockID"]);
                    if (reader["CertificateID"] != DBNull.Value) _CertificateLockOB.CertificateID = Convert.ToInt64(reader["CertificateID"]);
                    if (reader["LockType"] != DBNull.Value) _CertificateLockOB.LockType = Convert.ToString(reader["LockType"]);
                    if (reader["LockTime"] != DBNull.Value) _CertificateLockOB.LockTime = Convert.ToDateTime(reader["LockTime"]);
                    if (reader["LockEndTime"] != DBNull.Value) _CertificateLockOB.LockEndTime = Convert.ToDateTime(reader["LockEndTime"]);
                    if (reader["LockPerson"] != DBNull.Value) _CertificateLockOB.LockPerson = Convert.ToString(reader["LockPerson"]);
                    if (reader["Remark"] != DBNull.Value) _CertificateLockOB.Remark = Convert.ToString(reader["Remark"]);
                    if (reader["UnlockTime"] != DBNull.Value) _CertificateLockOB.UnlockTime = Convert.ToDateTime(reader["UnlockTime"]);
                    if (reader["UnlockPerson"] != DBNull.Value) _CertificateLockOB.UnlockPerson = Convert.ToString(reader["UnlockPerson"]);
                    if (reader["LockStatus"] != DBNull.Value) _CertificateLockOB.LockStatus = Convert.ToString(reader["LockStatus"]);
                }
                reader.Close();
                db.Close();
                return _CertificateLockOB;
            }
        }

        /// <summary>
        /// 获取证书是否正在锁定中（人员考务内部锁定，非基础库）
        /// </summary>
        /// <param name="CertificateID">证书ID</param>
        /// <returns>锁定中返回true，否则返回false</returns>
        public static bool GetCertificateLockStatus(long CertificateID)
        {
            //检索最后锁定信息
            CertificateLockOB lockOB = CertificateLockDAL.GetLastObject(CertificateID);
            if (lockOB == null //没有锁定记录
                || lockOB.LockEndTime.Value < DateTime.Now //加锁已过期
                || lockOB.UnlockTime.HasValue)//已解锁
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 获取证书是否正在锁定中（人员考务内部锁定，非基础库）
        /// </summary>
        /// <param name="CertificateID">证书ID</param>
        /// <param name="LockReason">锁定原因</param>
        /// <returns>锁定中返回true，否则返回false</returns>
        public static bool GetCertificateLockStatus(long CertificateID,ref string LockReason)
        {
            //检索最后锁定信息
            CertificateLockOB lockOB = CertificateLockDAL.GetLastObject(CertificateID);
            if (lockOB == null //没有锁定记录
                || lockOB.LockEndTime.Value < DateTime.Now //加锁已过期
                || lockOB.UnlockTime.HasValue)//已解锁
            {
                return false;
            }
            LockReason = lockOB.Remark;
            return true;
        }
        #endregion
    }
}
