using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--LockZJSDAL(填写类描述)
	/// </summary>
    public class LockZJSDAL
    {
        public LockZJSDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_LockZJSMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(LockZJSMDL _LockZJSMDL)
		{
		    return Insert(null,_LockZJSMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_LockZJSMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,LockZJSMDL _LockZJSMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.LockZJS(PSN_CertificateNO,LockType,LockTime,LockEndTime,LockPerson,LockRemark,UnlockTime,UnlockPerson,UnlockRemark,LockStatus)
			VALUES (@PSN_CertificateNO,@LockType,@LockTime,@LockEndTime,@LockPerson,@LockRemark,@UnlockTime,@UnlockPerson,@UnlockRemark,@LockStatus);SELECT @LockID = @@IDENTITY";

		    List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateOutParameter("LockID", DbType.Int64));
			p.Add(db.CreateParameter("PSN_CertificateNO",DbType.String, _LockZJSMDL.PSN_CertificateNO));
			p.Add(db.CreateParameter("LockType",DbType.String, _LockZJSMDL.LockType));
			p.Add(db.CreateParameter("LockTime",DbType.DateTime, _LockZJSMDL.LockTime));
			p.Add(db.CreateParameter("LockEndTime",DbType.DateTime, _LockZJSMDL.LockEndTime));
			p.Add(db.CreateParameter("LockPerson",DbType.String, _LockZJSMDL.LockPerson));
			p.Add(db.CreateParameter("LockRemark",DbType.String, _LockZJSMDL.LockRemark));
			p.Add(db.CreateParameter("UnlockTime",DbType.DateTime, _LockZJSMDL.UnlockTime));
			p.Add(db.CreateParameter("UnlockPerson",DbType.String, _LockZJSMDL.UnlockPerson));
			p.Add(db.CreateParameter("UnlockRemark",DbType.String, _LockZJSMDL.UnlockRemark));
			p.Add(db.CreateParameter("LockStatus",DbType.String, _LockZJSMDL.LockStatus));
            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            _LockZJSMDL.LockID = Convert.ToInt64(p[0].Value);
            return rtn;
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_LockZJSMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(LockZJSMDL _LockZJSMDL)
		{
			return Update(null,_LockZJSMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_LockZJSMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,LockZJSMDL _LockZJSMDL)
		{
			string sql = @"
			UPDATE dbo.LockZJS
				SET	PSN_CertificateNO = @PSN_CertificateNO,LockType = @LockType,LockTime = @LockTime,LockEndTime = @LockEndTime,LockPerson = @LockPerson,LockRemark = @LockRemark,UnlockTime = @UnlockTime,UnlockPerson = @UnlockPerson,UnlockRemark = @UnlockRemark,LockStatus = @LockStatus
			WHERE
				LockID = @LockID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("LockID",DbType.Int64, _LockZJSMDL.LockID));
			p.Add(db.CreateParameter("PSN_CertificateNO",DbType.String, _LockZJSMDL.PSN_CertificateNO));
			p.Add(db.CreateParameter("LockType",DbType.String, _LockZJSMDL.LockType));
			p.Add(db.CreateParameter("LockTime",DbType.DateTime, _LockZJSMDL.LockTime));
			p.Add(db.CreateParameter("LockEndTime",DbType.DateTime, _LockZJSMDL.LockEndTime));
			p.Add(db.CreateParameter("LockPerson",DbType.String, _LockZJSMDL.LockPerson));
			p.Add(db.CreateParameter("LockRemark",DbType.String, _LockZJSMDL.LockRemark));
			p.Add(db.CreateParameter("UnlockTime",DbType.DateTime, _LockZJSMDL.UnlockTime));
			p.Add(db.CreateParameter("UnlockPerson",DbType.String, _LockZJSMDL.UnlockPerson));
			p.Add(db.CreateParameter("UnlockRemark",DbType.String, _LockZJSMDL.UnlockRemark));
			p.Add(db.CreateParameter("LockStatus",DbType.String, _LockZJSMDL.LockStatus));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="LockZJSID">主键</param>
		/// <returns></returns>
        public static int Delete( long LockID )
		{
			return Delete(null, LockID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="LockZJSID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, long LockID)
		{
			string sql=@"DELETE FROM dbo.LockZJS WHERE LockID = @LockID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("LockID",DbType.Int64,LockID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="_LockZJSMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(LockZJSMDL _LockZJSMDL)
		{
			return Delete(null,_LockZJSMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_LockZJSMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,LockZJSMDL _LockZJSMDL)
		{
			string sql=@"DELETE FROM dbo.LockZJS WHERE LockID = @LockID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("LockID",DbType.Int64,_LockZJSMDL.LockID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="LockZJSID">主键</param>
        public static LockZJSMDL GetObject( long LockID )
		{
			string sql=@"
			SELECT LockID,PSN_CertificateNO,LockType,LockTime,LockEndTime,LockPerson,LockRemark,UnlockTime,UnlockPerson,UnlockRemark,LockStatus
			FROM dbo.LockZJS
			WHERE LockID = @LockID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("LockID",DbType.Int64,LockID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                LockZJSMDL _LockZJSMDL = null;
                if (reader.Read())
                {
                    _LockZJSMDL = new LockZJSMDL();
					if (reader["LockID"] != DBNull.Value) _LockZJSMDL.LockID = Convert.ToInt64(reader["LockID"]);
					if (reader["PSN_CertificateNO"] != DBNull.Value) _LockZJSMDL.PSN_CertificateNO = Convert.ToString(reader["PSN_CertificateNO"]);
					if (reader["LockType"] != DBNull.Value) _LockZJSMDL.LockType = Convert.ToString(reader["LockType"]);
					if (reader["LockTime"] != DBNull.Value) _LockZJSMDL.LockTime = Convert.ToDateTime(reader["LockTime"]);
					if (reader["LockEndTime"] != DBNull.Value) _LockZJSMDL.LockEndTime = Convert.ToDateTime(reader["LockEndTime"]);
					if (reader["LockPerson"] != DBNull.Value) _LockZJSMDL.LockPerson = Convert.ToString(reader["LockPerson"]);
					if (reader["LockRemark"] != DBNull.Value) _LockZJSMDL.LockRemark = Convert.ToString(reader["LockRemark"]);
					if (reader["UnlockTime"] != DBNull.Value) _LockZJSMDL.UnlockTime = Convert.ToDateTime(reader["UnlockTime"]);
					if (reader["UnlockPerson"] != DBNull.Value) _LockZJSMDL.UnlockPerson = Convert.ToString(reader["UnlockPerson"]);
					if (reader["UnlockRemark"] != DBNull.Value) _LockZJSMDL.UnlockRemark = Convert.ToString(reader["UnlockRemark"]);
					if (reader["LockStatus"] != DBNull.Value) _LockZJSMDL.LockStatus = Convert.ToString(reader["LockStatus"]);
                }
				reader.Close();
                db.Close();
                return _LockZJSMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.LockZJS", "*", filterWhereString, orderBy == "" ? "  LockID desc" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.LockZJS", filterWhereString);
        }
        
        #region 自定义方法

        /// <summary>
        /// 批量锁定二造证书
        /// </summary>
        /// <param name="_LockZJSMDL">锁定参数类</param>
        /// <param name="filterWhereString">过滤条件</param>
        /// <returns></returns>
        public static int BatchLock(LockZJSMDL _LockZJSMDL, string filterWhereString)
        {
            DBHelper db = new DBHelper();
            string sql = @"
			INSERT INTO dbo.LockZJS(PSN_CertificateNO,LockType,LockTime,LockEndTime,LockPerson,LockRemark,LockStatus)
			select distinct PSN_CertificateNO,@LockType,@LockTime,@LockEndTime,@LockPerson,@LockRemark,@LockStatus
            from [zjs_Certificate]
            where  1=1 {0}";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("LockType", DbType.String, _LockZJSMDL.LockType));
            p.Add(db.CreateParameter("LockTime", DbType.DateTime, _LockZJSMDL.LockTime));
            p.Add(db.CreateParameter("LockEndTime", DbType.DateTime, _LockZJSMDL.LockEndTime));
            p.Add(db.CreateParameter("LockPerson", DbType.String, _LockZJSMDL.LockPerson));
            p.Add(db.CreateParameter("LockRemark", DbType.String, _LockZJSMDL.LockRemark));
            p.Add(db.CreateParameter("LockStatus", DbType.String, _LockZJSMDL.LockStatus));

            return db.ExcuteNonQuery(string.Format(sql, filterWhereString), p.ToArray());
        }

        /// <summary>
        /// 批量解锁二造证书
        /// </summary>
        /// <param name="_LockZJSMDL">解锁参数类</param>
        /// <param name="filterWhereString">过滤条件</param>
        /// <returns></returns>
        public static int BatchUnlock(LockZJSMDL _LockZJSMDL, string filterWhereString)
        {
            DBHelper db = new DBHelper();
            string sql = @"
			UPDATE dbo.LockZJS
            Set UnlockTime = @UnlockTime,UnlockPerson = @UnlockPerson,UnlockRemark = @UnlockRemark,LockStatus = @LockStatus,LockEndTime = @UnlockTime
            where PSN_CertificateNO in(
                select distinct PSN_CertificateNO
                from [zjs_Certificate]                
                where 1=1 {0}
            );";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("UnlockTime", DbType.DateTime, _LockZJSMDL.UnlockTime));
            p.Add(db.CreateParameter("UnlockPerson", DbType.String, _LockZJSMDL.UnlockPerson));
            p.Add(db.CreateParameter("UnlockRemark", DbType.String, _LockZJSMDL.UnlockRemark));
            p.Add(db.CreateParameter("LockStatus", DbType.String, _LockZJSMDL.LockStatus));

            return db.ExcuteNonQuery(string.Format(sql, filterWhereString), p.ToArray());
        }


        /// <summary>
        /// 判断证书是否锁定中
        /// </summary>
        /// <param name="PSN_CertificateNO">身份证</param>
        /// <returns>锁定：true；未锁定：false</returns>
        public static bool GetLockStatus(string PSN_CertificateNO)
        {
            string sql = @"
			SELECT top 1 LockID,PSN_CertificateNO,LockType,LockTime,LockEndTime,LockPerson,LockRemark,UnlockTime,UnlockPerson,UnlockRemark,LockStatus
			FROM dbo.LockZJS
			WHERE PSN_CertificateNO = @PSN_CertificateNO
            order by LockTime desc";

            LockZJSMDL _LockZJSMDL = null;

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PSN_CertificateNO", DbType.String, PSN_CertificateNO));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {

                if (reader.Read())
                {
                    _LockZJSMDL = new LockZJSMDL();
                    if (reader["LockID"] != DBNull.Value) _LockZJSMDL.LockID = Convert.ToInt64(reader["LockID"]);
                    if (reader["PSN_CertificateNO"] != DBNull.Value) _LockZJSMDL.PSN_CertificateNO = Convert.ToString(reader["PSN_CertificateNO"]);
                    if (reader["LockType"] != DBNull.Value) _LockZJSMDL.LockType = Convert.ToString(reader["LockType"]);
                    if (reader["LockTime"] != DBNull.Value) _LockZJSMDL.LockTime = Convert.ToDateTime(reader["LockTime"]);
                    if (reader["LockEndTime"] != DBNull.Value) _LockZJSMDL.LockEndTime = Convert.ToDateTime(reader["LockEndTime"]);
                    if (reader["LockPerson"] != DBNull.Value) _LockZJSMDL.LockPerson = Convert.ToString(reader["LockPerson"]);
                    if (reader["LockRemark"] != DBNull.Value) _LockZJSMDL.LockRemark = Convert.ToString(reader["LockRemark"]);
                    if (reader["UnlockTime"] != DBNull.Value) _LockZJSMDL.UnlockTime = Convert.ToDateTime(reader["UnlockTime"]);
                    if (reader["UnlockPerson"] != DBNull.Value) _LockZJSMDL.UnlockPerson = Convert.ToString(reader["UnlockPerson"]);
                    if (reader["UnlockRemark"] != DBNull.Value) _LockZJSMDL.UnlockRemark = Convert.ToString(reader["UnlockRemark"]);
                    if (reader["LockStatus"] != DBNull.Value) _LockZJSMDL.LockStatus = Convert.ToString(reader["LockStatus"]);
                }
                reader.Close();
                db.Close();
            }

            if (_LockZJSMDL == null //没有锁定记录
               || _LockZJSMDL.LockEndTime.Value < DateTime.Now //加锁已过期
               || _LockZJSMDL.UnlockTime.HasValue)//已解锁
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}
