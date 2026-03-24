using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--CheckTaskDAL(填写类描述)
	/// </summary>
    public class CheckTaskDAL
    {
        public CheckTaskDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_CheckTaskMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(CheckTaskMDL _CheckTaskMDL)
		{
		    return Insert(null,_CheckTaskMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_CheckTaskMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,CheckTaskMDL _CheckTaskMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.CheckTask(PatchCode,CheckType,CreateTime,CJR,PublishiTime,LastReportTime,ifPhoneNotice,PhoneNotice,ifTipNotice,TipNotice)
			VALUES (@PatchCode,@CheckType,@CreateTime,@CJR,@PublishiTime,@LastReportTime,@ifPhoneNotice,@PhoneNotice,@ifTipNotice,@TipNotice)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("PatchCode",DbType.Int32, _CheckTaskMDL.PatchCode));
			p.Add(db.CreateParameter("CheckType",DbType.String, _CheckTaskMDL.CheckType));
			p.Add(db.CreateParameter("CreateTime",DbType.DateTime, _CheckTaskMDL.CreateTime));
			p.Add(db.CreateParameter("CJR",DbType.String, _CheckTaskMDL.CJR));
			p.Add(db.CreateParameter("PublishiTime",DbType.DateTime, _CheckTaskMDL.PublishiTime));
			p.Add(db.CreateParameter("LastReportTime",DbType.DateTime, _CheckTaskMDL.LastReportTime));
			p.Add(db.CreateParameter("ifPhoneNotice",DbType.Boolean, _CheckTaskMDL.ifPhoneNotice));
			p.Add(db.CreateParameter("PhoneNotice",DbType.String, _CheckTaskMDL.PhoneNotice));
			p.Add(db.CreateParameter("ifTipNotice",DbType.Boolean, _CheckTaskMDL.ifTipNotice));
			p.Add(db.CreateParameter("TipNotice",DbType.String, _CheckTaskMDL.TipNotice));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_CheckTaskMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(CheckTaskMDL _CheckTaskMDL)
		{
			return Update(null,_CheckTaskMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_CheckTaskMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,CheckTaskMDL _CheckTaskMDL)
		{
			string sql = @"
			UPDATE dbo.CheckTask
				SET	CheckType = @CheckType,CreateTime = @CreateTime,CJR = @CJR,PublishiTime = @PublishiTime,LastReportTime = @LastReportTime,ifPhoneNotice = @ifPhoneNotice,PhoneNotice = @PhoneNotice,ifTipNotice = @ifTipNotice,TipNotice = @TipNotice
			WHERE
				PatchCode = @PatchCode";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("PatchCode",DbType.Int32, _CheckTaskMDL.PatchCode));
			p.Add(db.CreateParameter("CheckType",DbType.String, _CheckTaskMDL.CheckType));
			p.Add(db.CreateParameter("CreateTime",DbType.DateTime, _CheckTaskMDL.CreateTime));
			p.Add(db.CreateParameter("CJR",DbType.String, _CheckTaskMDL.CJR));
			p.Add(db.CreateParameter("PublishiTime",DbType.DateTime, _CheckTaskMDL.PublishiTime));
			p.Add(db.CreateParameter("LastReportTime",DbType.DateTime, _CheckTaskMDL.LastReportTime));
			p.Add(db.CreateParameter("ifPhoneNotice",DbType.Boolean, _CheckTaskMDL.ifPhoneNotice));
			p.Add(db.CreateParameter("PhoneNotice",DbType.String, _CheckTaskMDL.PhoneNotice));
			p.Add(db.CreateParameter("ifTipNotice",DbType.Boolean, _CheckTaskMDL.ifTipNotice));
			p.Add(db.CreateParameter("TipNotice",DbType.String, _CheckTaskMDL.TipNotice));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="CheckTaskID">主键</param>
		/// <returns></returns>
        public static int Delete( int PatchCode )
		{
			return Delete(null, PatchCode);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="CheckTaskID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, int PatchCode)
		{
			string sql=@"DELETE FROM dbo.CheckTask WHERE PatchCode = @PatchCode";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("PatchCode",DbType.Int32,PatchCode));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="_CheckTaskMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(CheckTaskMDL _CheckTaskMDL)
		{
			return Delete(null,_CheckTaskMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_CheckTaskMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,CheckTaskMDL _CheckTaskMDL)
		{
			string sql=@"DELETE FROM dbo.CheckTask WHERE PatchCode = @PatchCode";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("PatchCode",DbType.Int32,_CheckTaskMDL.PatchCode));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="CheckTaskID">主键</param>
        public static CheckTaskMDL GetObject( int PatchCode )
		{
			string sql=@"
			SELECT PatchCode,CheckType,CreateTime,CJR,PublishiTime,LastReportTime,ifPhoneNotice,PhoneNotice,ifTipNotice,TipNotice
			FROM dbo.CheckTask
			WHERE PatchCode = @PatchCode";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("PatchCode",DbType.Int32,PatchCode));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                CheckTaskMDL _CheckTaskMDL = null;
                if (reader.Read())
                {
                    _CheckTaskMDL = new CheckTaskMDL();
					if (reader["PatchCode"] != DBNull.Value) _CheckTaskMDL.PatchCode = Convert.ToInt32(reader["PatchCode"]);
					if (reader["CheckType"] != DBNull.Value) _CheckTaskMDL.CheckType = Convert.ToString(reader["CheckType"]);
					if (reader["CreateTime"] != DBNull.Value) _CheckTaskMDL.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
					if (reader["CJR"] != DBNull.Value) _CheckTaskMDL.CJR = Convert.ToString(reader["CJR"]);
					if (reader["PublishiTime"] != DBNull.Value) _CheckTaskMDL.PublishiTime = Convert.ToDateTime(reader["PublishiTime"]);
					if (reader["LastReportTime"] != DBNull.Value) _CheckTaskMDL.LastReportTime = Convert.ToDateTime(reader["LastReportTime"]);
					if (reader["ifPhoneNotice"] != DBNull.Value) _CheckTaskMDL.ifPhoneNotice = Convert.ToBoolean(reader["ifPhoneNotice"]);
					if (reader["PhoneNotice"] != DBNull.Value) _CheckTaskMDL.PhoneNotice = Convert.ToString(reader["PhoneNotice"]);
					if (reader["ifTipNotice"] != DBNull.Value) _CheckTaskMDL.ifTipNotice = Convert.ToBoolean(reader["ifTipNotice"]);
					if (reader["TipNotice"] != DBNull.Value) _CheckTaskMDL.TipNotice = Convert.ToString(reader["TipNotice"]);
                }
				reader.Close();
                db.Close();
                return _CheckTaskMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.CheckTask", "*,(SELECT  COUNT(*) FROM dbo.CheckFeedBack where PatchCode=CheckTask.PatchCode) AS DataCount", filterWhereString, orderBy == "" ? " PatchCode" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.CheckTask", filterWhereString);
        }
        
        #region 自定义方法

        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        public static DataTable GetListTjView(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.View_CheckTaskTj", "*", filterWhereString, orderBy == "" ? " PatchCode" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountTjView(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.View_CheckTaskTj", filterWhereString);
        }

        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        public static DataTable GetListTjByCountryView(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.View_CheckTaskTjByCountry", "*", filterWhereString, orderBy == "" ? " PatchCode" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountTjByCountryView(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.View_CheckTaskTjByCountry", filterWhereString);
        }

        #endregion
    }
}
