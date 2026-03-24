using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--LockJZSDAL(填写类描述)
	/// </summary>
    public class LockJZSDAL
    {
        public LockJZSDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_LockJZSMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(LockJZSMDL _LockJZSMDL)
		{
		    return Insert(null,_LockJZSMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_LockJZSMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,LockJZSMDL _LockJZSMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.LockJZS(PSN_ServerID,LockType,LockTime,LockEndTime,LockPerson,LockRemark,UnlockTime,UnlockPerson,UnlockRemark,LockStatus)
			VALUES (@PSN_ServerID,@LockType,@LockTime,@LockEndTime,@LockPerson,@LockRemark,@UnlockTime,@UnlockPerson,@UnlockRemark,@LockStatus);SELECT @LockID = @@IDENTITY";

		    List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateOutParameter("LockID", DbType.Int64));
			p.Add(db.CreateParameter("PSN_ServerID",DbType.String, _LockJZSMDL.PSN_ServerID));
			p.Add(db.CreateParameter("LockType",DbType.String, _LockJZSMDL.LockType));
			p.Add(db.CreateParameter("LockTime",DbType.DateTime, _LockJZSMDL.LockTime));
			p.Add(db.CreateParameter("LockEndTime",DbType.DateTime, _LockJZSMDL.LockEndTime));
			p.Add(db.CreateParameter("LockPerson",DbType.String, _LockJZSMDL.LockPerson));
			p.Add(db.CreateParameter("LockRemark",DbType.String, _LockJZSMDL.LockRemark));
			p.Add(db.CreateParameter("UnlockTime",DbType.DateTime, _LockJZSMDL.UnlockTime));
			p.Add(db.CreateParameter("UnlockPerson",DbType.String, _LockJZSMDL.UnlockPerson));
			p.Add(db.CreateParameter("UnlockRemark",DbType.String, _LockJZSMDL.UnlockRemark));
			p.Add(db.CreateParameter("LockStatus",DbType.String, _LockJZSMDL.LockStatus));

            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            _LockJZSMDL.LockID = Convert.ToInt64(p[0].Value);
            return rtn;
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_LockJZSMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(LockJZSMDL _LockJZSMDL)
		{
			return Update(null,_LockJZSMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_LockJZSMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,LockJZSMDL _LockJZSMDL)
		{
			string sql = @"
			UPDATE dbo.LockJZS
				SET	PSN_ServerID = @PSN_ServerID,LockType = @LockType,LockTime = @LockTime,LockEndTime = @LockEndTime,LockPerson = @LockPerson,LockRemark = @LockRemark,UnlockTime = @UnlockTime,UnlockPerson = @UnlockPerson,UnlockRemark = @UnlockRemark,LockStatus = @LockStatus
			WHERE
				LockID = @LockID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("LockID",DbType.Int64, _LockJZSMDL.LockID));
			p.Add(db.CreateParameter("PSN_ServerID",DbType.String, _LockJZSMDL.PSN_ServerID));
			p.Add(db.CreateParameter("LockType",DbType.String, _LockJZSMDL.LockType));
			p.Add(db.CreateParameter("LockTime",DbType.DateTime, _LockJZSMDL.LockTime));
			p.Add(db.CreateParameter("LockEndTime",DbType.DateTime, _LockJZSMDL.LockEndTime));
			p.Add(db.CreateParameter("LockPerson",DbType.String, _LockJZSMDL.LockPerson));
			p.Add(db.CreateParameter("LockRemark",DbType.String, _LockJZSMDL.LockRemark));
			p.Add(db.CreateParameter("UnlockTime",DbType.DateTime, _LockJZSMDL.UnlockTime));
			p.Add(db.CreateParameter("UnlockPerson",DbType.String, _LockJZSMDL.UnlockPerson));
			p.Add(db.CreateParameter("UnlockRemark",DbType.String, _LockJZSMDL.UnlockRemark));
			p.Add(db.CreateParameter("LockStatus",DbType.String, _LockJZSMDL.LockStatus));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="LockJZSID">主键</param>
		/// <returns></returns>
        public static int Delete( long LockID )
		{
			return Delete(null, LockID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="LockJZSID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, long LockID)
		{
			string sql=@"DELETE FROM dbo.LockJZS WHERE LockID = @LockID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("LockID",DbType.Int64,LockID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="_LockJZSMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(LockJZSMDL _LockJZSMDL)
		{
			return Delete(null,_LockJZSMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_LockJZSMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,LockJZSMDL _LockJZSMDL)
		{
			string sql=@"DELETE FROM dbo.LockJZS WHERE LockID = @LockID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("LockID",DbType.Int64,_LockJZSMDL.LockID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="LockID">主键</param>
	    /// <returns></returns>
        public static LockJZSMDL GetObject( long LockID )
		{
			string sql=@"
			SELECT LockID,PSN_ServerID,LockType,LockTime,LockEndTime,LockPerson,LockRemark,UnlockTime,UnlockPerson,UnlockRemark,LockStatus
			FROM dbo.LockJZS
			WHERE LockID = @LockID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("LockID",DbType.Int64,LockID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                LockJZSMDL _LockJZSMDL = null;
                if (reader.Read())
                {
                    _LockJZSMDL = new LockJZSMDL();
					if (reader["LockID"] != DBNull.Value) _LockJZSMDL.LockID = Convert.ToInt64(reader["LockID"]);
					if (reader["PSN_ServerID"] != DBNull.Value) _LockJZSMDL.PSN_ServerID = Convert.ToString(reader["PSN_ServerID"]);
					if (reader["LockType"] != DBNull.Value) _LockJZSMDL.LockType = Convert.ToString(reader["LockType"]);
					if (reader["LockTime"] != DBNull.Value) _LockJZSMDL.LockTime = Convert.ToDateTime(reader["LockTime"]);
					if (reader["LockEndTime"] != DBNull.Value) _LockJZSMDL.LockEndTime = Convert.ToDateTime(reader["LockEndTime"]);
					if (reader["LockPerson"] != DBNull.Value) _LockJZSMDL.LockPerson = Convert.ToString(reader["LockPerson"]);
					if (reader["LockRemark"] != DBNull.Value) _LockJZSMDL.LockRemark = Convert.ToString(reader["LockRemark"]);
					if (reader["UnlockTime"] != DBNull.Value) _LockJZSMDL.UnlockTime = Convert.ToDateTime(reader["UnlockTime"]);
					if (reader["UnlockPerson"] != DBNull.Value) _LockJZSMDL.UnlockPerson = Convert.ToString(reader["UnlockPerson"]);
					if (reader["UnlockRemark"] != DBNull.Value) _LockJZSMDL.UnlockRemark = Convert.ToString(reader["UnlockRemark"]);
					if (reader["LockStatus"] != DBNull.Value) _LockJZSMDL.LockStatus = Convert.ToString(reader["LockStatus"]);
                }
				reader.Close();
                db.Close();
                return _LockJZSMDL;
            }
		}

        /// <summary>
        /// 判断证书是否锁定中
        /// </summary>
        /// <param name="PSN_ServerID">证书ID</param>
        /// <returns>锁定：true；未锁定：false</returns>
        public static bool GetLockStatus(string PSN_ServerID)
        {
            string sql = @"
			SELECT top 1 LockID,PSN_ServerID,LockType,LockTime,LockEndTime,LockPerson,LockRemark,UnlockTime,UnlockPerson,UnlockRemark,LockStatus
			FROM dbo.LockJZS
			WHERE PSN_ServerID = @PSN_ServerID
            order by LockTime desc";

            LockJZSMDL _LockJZSMDL = null;

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PSN_ServerID", DbType.String, PSN_ServerID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                
                if (reader.Read())
                {
                    _LockJZSMDL = new LockJZSMDL();
                    if (reader["LockID"] != DBNull.Value) _LockJZSMDL.LockID = Convert.ToInt64(reader["LockID"]);
                    if (reader["PSN_ServerID"] != DBNull.Value) _LockJZSMDL.PSN_ServerID = Convert.ToString(reader["PSN_ServerID"]);
                    if (reader["LockType"] != DBNull.Value) _LockJZSMDL.LockType = Convert.ToString(reader["LockType"]);
                    if (reader["LockTime"] != DBNull.Value) _LockJZSMDL.LockTime = Convert.ToDateTime(reader["LockTime"]);
                    if (reader["LockEndTime"] != DBNull.Value) _LockJZSMDL.LockEndTime = Convert.ToDateTime(reader["LockEndTime"]);
                    if (reader["LockPerson"] != DBNull.Value) _LockJZSMDL.LockPerson = Convert.ToString(reader["LockPerson"]);
                    if (reader["LockRemark"] != DBNull.Value) _LockJZSMDL.LockRemark = Convert.ToString(reader["LockRemark"]);
                    if (reader["UnlockTime"] != DBNull.Value) _LockJZSMDL.UnlockTime = Convert.ToDateTime(reader["UnlockTime"]);
                    if (reader["UnlockPerson"] != DBNull.Value) _LockJZSMDL.UnlockPerson = Convert.ToString(reader["UnlockPerson"]);
                    if (reader["UnlockRemark"] != DBNull.Value) _LockJZSMDL.UnlockRemark = Convert.ToString(reader["UnlockRemark"]);
                    if (reader["LockStatus"] != DBNull.Value) _LockJZSMDL.LockStatus = Convert.ToString(reader["LockStatus"]);
                }
                reader.Close();
                db.Close();      
            }

            if (_LockJZSMDL == null //没有锁定记录
               || _LockJZSMDL.LockEndTime.Value < DateTime.Now //加锁已过期
               || _LockJZSMDL.UnlockTime.HasValue)//已解锁
            {
                return false;
            }
            return true;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.LockJZS", "*", filterWhereString, orderBy == "" ? " LockID desc" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.LockJZS", filterWhereString);
        }
        
        #region 自定义方法

        /// <summary>
        /// 批量锁定二建证书
        /// </summary>
        /// <param name="_LockJZSMDL">锁定参数类</param>
        /// <param name="filterWhereString">过滤条件</param>
        /// <returns></returns>
        public static int BatchLock(LockJZSMDL _LockJZSMDL, string filterWhereString)
        {
            DBHelper db = new DBHelper();
            string sql = @"
			INSERT INTO dbo.LockJZS(PSN_ServerID,LockType,LockTime,LockEndTime,LockPerson,LockRemark,LockStatus)
			select PSN_ServerID,@LockType,@LockTime,@LockEndTime,@LockPerson,@LockRemark,@LockStatus
            from [COC_TOW_Person_BaseInfo]
            where PSN_ServerID in(
                select A.PSN_ServerID
                from dbo.View_JZS_TOW_WithProfession A 
                left join [dbo].[View_JZS_TOW_Applying] B 
                    on a.PSN_ServerID=b.PSN_ServerID 
                left join [jcsjk_RY_JZS_ZSSD] C 
                    on a.PSN_RegisterNO=c.ZCH 
                left join [dbo].[LockJZS] L
                    on A.[PSN_ServerID] = L.[PSN_ServerID] and L.LockStatus='加锁' and LockEndTime > getdate()
                where 1=1 {0}
            );";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("LockType", DbType.String, _LockJZSMDL.LockType));
            p.Add(db.CreateParameter("LockTime", DbType.DateTime, _LockJZSMDL.LockTime));
            p.Add(db.CreateParameter("LockEndTime", DbType.DateTime, _LockJZSMDL.LockEndTime));
            p.Add(db.CreateParameter("LockPerson", DbType.String, _LockJZSMDL.LockPerson));
            p.Add(db.CreateParameter("LockRemark", DbType.String, _LockJZSMDL.LockRemark));
            p.Add(db.CreateParameter("LockStatus", DbType.String, _LockJZSMDL.LockStatus));

           return db.ExcuteNonQuery(string.Format(sql, filterWhereString), p.ToArray());           
        }

        /// <summary>
        /// 批量解锁二建证书
        /// </summary>
        /// <param name="_LockJZSMDL">解锁参数类</param>
        /// <param name="filterWhereString">过滤条件</param>
        /// <returns></returns>
        public static int BatchUnlock(LockJZSMDL _LockJZSMDL, string filterWhereString)
        {
            DBHelper db = new DBHelper();
            string sql = @"
			UPDATE dbo.LockJZS
            Set UnlockTime = @UnlockTime,UnlockPerson = @UnlockPerson,UnlockRemark = @UnlockRemark,LockStatus = @LockStatus,LockEndTime = @UnlockTime
            where LockID in(
                select L.LockID
                from dbo.View_JZS_TOW_WithProfession A 
                left join [dbo].[View_JZS_TOW_Applying] B 
                    on a.PSN_ServerID=b.PSN_ServerID 
                left join [jcsjk_RY_JZS_ZSSD] C 
                    on a.PSN_RegisterNO=c.ZCH 
                left join [dbo].[LockJZS] L
                    on A.[PSN_ServerID] = L.[PSN_ServerID] and L.LockStatus='加锁' and LockEndTime > getdate()
                where 1=1 {0}
            );";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("UnlockTime", DbType.DateTime, _LockJZSMDL.UnlockTime));
            p.Add(db.CreateParameter("UnlockPerson", DbType.String, _LockJZSMDL.UnlockPerson));
            p.Add(db.CreateParameter("UnlockRemark", DbType.String, _LockJZSMDL.UnlockRemark));
            p.Add(db.CreateParameter("LockStatus", DbType.String, _LockJZSMDL.LockStatus));

            return db.ExcuteNonQuery(string.Format(sql, filterWhereString), p.ToArray());
        }

        #endregion
    }
}
