using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--CertificatePauseDAL(电子证书暂扣记录)
	/// </summary>
    public class CertificatePauseDAL
    {
        public CertificatePauseDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_CertificatePauseMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(CertificatePauseMDL _CertificatePauseMDL)
		{
		    return Insert(null,_CertificatePauseMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_CertificatePauseMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,CertificatePauseMDL _CertificatePauseMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.CertificatePause(PostTypeID,CertificateCode,PauseApplyTime,PauseDoTime,PauseApplyMan,EndPauseApplyTime,EndPauseDoTime,EndPauseApplyMan,PauseStatusCode,PauseStatus,Remark)
			VALUES (@PostTypeID,@CertificateCode,@PauseApplyTime,@PauseDoTime,@PauseApplyMan,@EndPauseApplyTime,@EndPauseDoTime,@EndPauseApplyMan,@PauseStatusCode,@PauseStatus,@Remark);SELECT @PauseID = @@IDENTITY";

		    List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateOutParameter("PauseID", DbType.Int64));
			p.Add(db.CreateParameter("PostTypeID",DbType.Int32, _CertificatePauseMDL.PostTypeID));
			p.Add(db.CreateParameter("CertificateCode",DbType.String, _CertificatePauseMDL.CertificateCode));
			p.Add(db.CreateParameter("PauseApplyTime",DbType.DateTime, _CertificatePauseMDL.PauseApplyTime));
			p.Add(db.CreateParameter("PauseDoTime",DbType.DateTime, _CertificatePauseMDL.PauseDoTime));
			p.Add(db.CreateParameter("PauseApplyMan",DbType.String, _CertificatePauseMDL.PauseApplyMan));
			p.Add(db.CreateParameter("EndPauseApplyTime",DbType.DateTime, _CertificatePauseMDL.EndPauseApplyTime));
			p.Add(db.CreateParameter("EndPauseDoTime",DbType.DateTime, _CertificatePauseMDL.EndPauseDoTime));
			p.Add(db.CreateParameter("EndPauseApplyMan",DbType.String, _CertificatePauseMDL.EndPauseApplyMan));
			p.Add(db.CreateParameter("PauseStatusCode",DbType.Int32, _CertificatePauseMDL.PauseStatusCode));
			p.Add(db.CreateParameter("PauseStatus",DbType.String, _CertificatePauseMDL.PauseStatus));
			p.Add(db.CreateParameter("Remark",DbType.String, _CertificatePauseMDL.Remark));
            int rtn = db.GetExcuteNonQuery(tran, sql, p.ToArray());
            _CertificatePauseMDL.PauseID = Convert.ToInt64(p[0].Value);
            return rtn;
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_CertificatePauseMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(CertificatePauseMDL _CertificatePauseMDL)
		{
			return Update(null,_CertificatePauseMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_CertificatePauseMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,CertificatePauseMDL _CertificatePauseMDL)
		{
			string sql = @"
			UPDATE dbo.CertificatePause
				SET	PostTypeID = @PostTypeID,CertificateCode = @CertificateCode,PauseApplyTime = @PauseApplyTime,PauseDoTime = @PauseDoTime,PauseApplyMan = @PauseApplyMan,EndPauseApplyTime = @EndPauseApplyTime,EndPauseDoTime = @EndPauseDoTime,EndPauseApplyMan = @EndPauseApplyMan,PauseStatusCode = @PauseStatusCode,PauseStatus = @PauseStatus,Remark = @Remark
			WHERE
				PauseID = @PauseID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("PauseID",DbType.Int64, _CertificatePauseMDL.PauseID));
			p.Add(db.CreateParameter("PostTypeID",DbType.Int32, _CertificatePauseMDL.PostTypeID));
			p.Add(db.CreateParameter("CertificateCode",DbType.String, _CertificatePauseMDL.CertificateCode));
			p.Add(db.CreateParameter("PauseApplyTime",DbType.DateTime, _CertificatePauseMDL.PauseApplyTime));
			p.Add(db.CreateParameter("PauseDoTime",DbType.DateTime, _CertificatePauseMDL.PauseDoTime));
			p.Add(db.CreateParameter("PauseApplyMan",DbType.String, _CertificatePauseMDL.PauseApplyMan));
			p.Add(db.CreateParameter("EndPauseApplyTime",DbType.DateTime, _CertificatePauseMDL.EndPauseApplyTime));
			p.Add(db.CreateParameter("EndPauseDoTime",DbType.DateTime, _CertificatePauseMDL.EndPauseDoTime));
			p.Add(db.CreateParameter("EndPauseApplyMan",DbType.String, _CertificatePauseMDL.EndPauseApplyMan));
			p.Add(db.CreateParameter("PauseStatusCode",DbType.Int32, _CertificatePauseMDL.PauseStatusCode));
			p.Add(db.CreateParameter("PauseStatus",DbType.String, _CertificatePauseMDL.PauseStatus));
			p.Add(db.CreateParameter("Remark",DbType.String, _CertificatePauseMDL.Remark));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="CertificatePauseID">主键</param>
		/// <returns></returns>
        public static int Delete( long PauseID )
		{
			return Delete(null, PauseID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="CertificatePauseID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, long PauseID)
		{
			string sql=@"DELETE FROM dbo.CertificatePause WHERE PauseID = @PauseID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("PauseID",DbType.Int64,PauseID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="_CertificatePauseMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(CertificatePauseMDL _CertificatePauseMDL)
		{
			return Delete(null,_CertificatePauseMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_CertificatePauseMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,CertificatePauseMDL _CertificatePauseMDL)
		{
			string sql=@"DELETE FROM dbo.CertificatePause WHERE PauseID = @PauseID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("PauseID",DbType.Int64,_CertificatePauseMDL.PauseID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="CertificatePauseID">主键</param>
        public static CertificatePauseMDL GetObject( long PauseID )
		{
			string sql=@"
			SELECT PauseID,PostTypeID,CertificateCode,PauseApplyTime,PauseDoTime,PauseApplyMan,EndPauseApplyTime,EndPauseDoTime,EndPauseApplyMan,PauseStatusCode,PauseStatus,Remark
			FROM dbo.CertificatePause
			WHERE PauseID = @PauseID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("PauseID",DbType.Int64,PauseID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                CertificatePauseMDL _CertificatePauseMDL = null;
                if (reader.Read())
                {
                    _CertificatePauseMDL = new CertificatePauseMDL();
					if (reader["PauseID"] != DBNull.Value) _CertificatePauseMDL.PauseID = Convert.ToInt64(reader["PauseID"]);
					if (reader["PostTypeID"] != DBNull.Value) _CertificatePauseMDL.PostTypeID = Convert.ToInt32(reader["PostTypeID"]);
					if (reader["CertificateCode"] != DBNull.Value) _CertificatePauseMDL.CertificateCode = Convert.ToString(reader["CertificateCode"]);
					if (reader["PauseApplyTime"] != DBNull.Value) _CertificatePauseMDL.PauseApplyTime = Convert.ToDateTime(reader["PauseApplyTime"]);
					if (reader["PauseDoTime"] != DBNull.Value) _CertificatePauseMDL.PauseDoTime = Convert.ToDateTime(reader["PauseDoTime"]);
					if (reader["PauseApplyMan"] != DBNull.Value) _CertificatePauseMDL.PauseApplyMan = Convert.ToString(reader["PauseApplyMan"]);
					if (reader["EndPauseApplyTime"] != DBNull.Value) _CertificatePauseMDL.EndPauseApplyTime = Convert.ToDateTime(reader["EndPauseApplyTime"]);
					if (reader["EndPauseDoTime"] != DBNull.Value) _CertificatePauseMDL.EndPauseDoTime = Convert.ToDateTime(reader["EndPauseDoTime"]);
					if (reader["EndPauseApplyMan"] != DBNull.Value) _CertificatePauseMDL.EndPauseApplyMan = Convert.ToString(reader["EndPauseApplyMan"]);
					if (reader["PauseStatusCode"] != DBNull.Value) _CertificatePauseMDL.PauseStatusCode = Convert.ToInt32(reader["PauseStatusCode"]);
					if (reader["PauseStatus"] != DBNull.Value) _CertificatePauseMDL.PauseStatus = Convert.ToString(reader["PauseStatus"]);
					if (reader["Remark"] != DBNull.Value) _CertificatePauseMDL.Remark = Convert.ToString(reader["Remark"]);
                }
				reader.Close();
                db.Close();
                return _CertificatePauseMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.CertificatePause", "*", filterWhereString, orderBy == "" ? " PauseID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.CertificatePause", filterWhereString);
        }
        
        #region 自定义方法
        
        /// <summary>
        /// 获取电子证书暂扣记录
        /// </summary>
        /// <param name="PostTypeID">岗位类别ID：二建：0；二造：-1；三类人：1，特种作业：2。</param>
        /// <param name="CertificateCode">证书编号</param>
        /// <returns></returns>
        public static CertificatePauseMDL GetLastObject(int PostTypeID, string CertificateCode)
		{
			string sql= @"
			SELECT top 1 PauseID,PostTypeID,CertificateCode,PauseApplyTime,PauseDoTime,PauseApplyMan,EndPauseApplyTime,EndPauseDoTime,EndPauseApplyMan,PauseStatusCode,PauseStatus,Remark
			FROM dbo.CertificatePause
			WHERE PostTypeID = @PostTypeID and CertificateCode =@CertificateCode and PauseStatusCode < 4
            order by PauseApplyTime desc";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PostTypeID", DbType.Int32, PostTypeID));
            p.Add(db.CreateParameter("CertificateCode", DbType.String, CertificateCode));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                CertificatePauseMDL _CertificatePauseMDL = null;
                if (reader.Read())
                {
                    _CertificatePauseMDL = new CertificatePauseMDL();
					if (reader["PauseID"] != DBNull.Value) _CertificatePauseMDL.PauseID = Convert.ToInt64(reader["PauseID"]);
					if (reader["PostTypeID"] != DBNull.Value) _CertificatePauseMDL.PostTypeID = Convert.ToInt32(reader["PostTypeID"]);
					if (reader["CertificateCode"] != DBNull.Value) _CertificatePauseMDL.CertificateCode = Convert.ToString(reader["CertificateCode"]);
					if (reader["PauseApplyTime"] != DBNull.Value) _CertificatePauseMDL.PauseApplyTime = Convert.ToDateTime(reader["PauseApplyTime"]);
					if (reader["PauseDoTime"] != DBNull.Value) _CertificatePauseMDL.PauseDoTime = Convert.ToDateTime(reader["PauseDoTime"]);
					if (reader["PauseApplyMan"] != DBNull.Value) _CertificatePauseMDL.PauseApplyMan = Convert.ToString(reader["PauseApplyMan"]);
					if (reader["EndPauseApplyTime"] != DBNull.Value) _CertificatePauseMDL.EndPauseApplyTime = Convert.ToDateTime(reader["EndPauseApplyTime"]);
					if (reader["EndPauseDoTime"] != DBNull.Value) _CertificatePauseMDL.EndPauseDoTime = Convert.ToDateTime(reader["EndPauseDoTime"]);
					if (reader["EndPauseApplyMan"] != DBNull.Value) _CertificatePauseMDL.EndPauseApplyMan = Convert.ToString(reader["EndPauseApplyMan"]);
					if (reader["PauseStatusCode"] != DBNull.Value) _CertificatePauseMDL.PauseStatusCode = Convert.ToInt32(reader["PauseStatusCode"]);
					if (reader["PauseStatus"] != DBNull.Value) _CertificatePauseMDL.PauseStatus = Convert.ToString(reader["PauseStatus"]);
					if (reader["Remark"] != DBNull.Value) _CertificatePauseMDL.Remark = Convert.ToString(reader["Remark"]);
                }
				reader.Close();
                db.Close();
                return _CertificatePauseMDL;
            }
		}
        #endregion
    }
}
