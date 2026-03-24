using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--jcsjk_RY_JZS_ZSSDDAL(填写类描述)
	/// </summary>
    public class jcsjk_RY_JZS_ZSSDDAL
    {
        public jcsjk_RY_JZS_ZSSDDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="jcsjk_RY_JZS_ZSSDMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(jcsjk_RY_JZS_ZSSDMDL _jcsjk_RY_JZS_ZSSDMDL)
		{
		    return Insert(null,_jcsjk_RY_JZS_ZSSDMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="jcsjk_RY_JZS_ZSSDMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,jcsjk_RY_JZS_ZSSDMDL _jcsjk_RY_JZS_ZSSDMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.jcsjk_RY_JZS_ZSSD([ID],XM,ZJHM,ZCH,HTBH,XMMC,ZBQY,SDZT,SDSJ,BZ,[VALID],CJSJ,XGSJ,CJR,XGR,CJDEPTID,XGDEPTID,CheckCode)
			VALUES (@ID,@XM,@ZJHM,@ZCH,@HTBH,@XMMC,@ZBQY,@SDZT,@SDSJ,@BZ,@VALID,@CJSJ,@XGSJ,@CJR,@XGR,@CJDEPTID,@XGDEPTID,@CheckCode)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ID",DbType.Guid, _jcsjk_RY_JZS_ZSSDMDL.ID));
			p.Add(db.CreateParameter("XM",DbType.String, _jcsjk_RY_JZS_ZSSDMDL.XM));
			p.Add(db.CreateParameter("ZJHM",DbType.String, _jcsjk_RY_JZS_ZSSDMDL.ZJHM));
			p.Add(db.CreateParameter("ZCH",DbType.String, _jcsjk_RY_JZS_ZSSDMDL.ZCH));
			p.Add(db.CreateParameter("HTBH",DbType.String, _jcsjk_RY_JZS_ZSSDMDL.HTBH));
			p.Add(db.CreateParameter("XMMC",DbType.String, _jcsjk_RY_JZS_ZSSDMDL.XMMC));
			p.Add(db.CreateParameter("ZBQY",DbType.String, _jcsjk_RY_JZS_ZSSDMDL.ZBQY));
			p.Add(db.CreateParameter("SDZT",DbType.Int32, _jcsjk_RY_JZS_ZSSDMDL.SDZT));
			p.Add(db.CreateParameter("SDSJ",DbType.DateTime, _jcsjk_RY_JZS_ZSSDMDL.SDSJ));
			p.Add(db.CreateParameter("BZ",DbType.String, _jcsjk_RY_JZS_ZSSDMDL.BZ));
			p.Add(db.CreateParameter("VALID",DbType.Int32, _jcsjk_RY_JZS_ZSSDMDL.VALID));
			p.Add(db.CreateParameter("CJSJ",DbType.DateTime, _jcsjk_RY_JZS_ZSSDMDL.CJSJ));
			p.Add(db.CreateParameter("XGSJ",DbType.DateTime, _jcsjk_RY_JZS_ZSSDMDL.XGSJ));
			p.Add(db.CreateParameter("CJR",DbType.String, _jcsjk_RY_JZS_ZSSDMDL.CJR));
			p.Add(db.CreateParameter("XGR",DbType.String, _jcsjk_RY_JZS_ZSSDMDL.XGR));
			p.Add(db.CreateParameter("CJDEPTID",DbType.String, _jcsjk_RY_JZS_ZSSDMDL.CJDEPTID));
			p.Add(db.CreateParameter("XGDEPTID",DbType.String, _jcsjk_RY_JZS_ZSSDMDL.XGDEPTID));
			p.Add(db.CreateParameter("CheckCode",DbType.String, _jcsjk_RY_JZS_ZSSDMDL.CheckCode));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="jcsjk_RY_JZS_ZSSDMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(jcsjk_RY_JZS_ZSSDMDL _jcsjk_RY_JZS_ZSSDMDL)
		{
			return Update(null,_jcsjk_RY_JZS_ZSSDMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="jcsjk_RY_JZS_ZSSDMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,jcsjk_RY_JZS_ZSSDMDL _jcsjk_RY_JZS_ZSSDMDL)
		{
			string sql = @"
			UPDATE dbo.jcsjk_RY_JZS_ZSSD
				SET	XM = @XM,ZJHM = @ZJHM,ZCH = @ZCH,HTBH = @HTBH,XMMC = @XMMC,ZBQY = @ZBQY,SDZT = @SDZT,SDSJ = @SDSJ,BZ = @BZ,""VALID"" = @VALID,CJSJ = @CJSJ,XGSJ = @XGSJ,CJR = @CJR,XGR = @XGR,CJDEPTID = @CJDEPTID,XGDEPTID = @XGDEPTID,CheckCode = @CheckCode
			WHERE
				ID = @ID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ID",DbType.Guid, _jcsjk_RY_JZS_ZSSDMDL.ID));
			p.Add(db.CreateParameter("XM",DbType.String, _jcsjk_RY_JZS_ZSSDMDL.XM));
			p.Add(db.CreateParameter("ZJHM",DbType.String, _jcsjk_RY_JZS_ZSSDMDL.ZJHM));
			p.Add(db.CreateParameter("ZCH",DbType.String, _jcsjk_RY_JZS_ZSSDMDL.ZCH));
			p.Add(db.CreateParameter("HTBH",DbType.String, _jcsjk_RY_JZS_ZSSDMDL.HTBH));
			p.Add(db.CreateParameter("XMMC",DbType.String, _jcsjk_RY_JZS_ZSSDMDL.XMMC));
			p.Add(db.CreateParameter("ZBQY",DbType.String, _jcsjk_RY_JZS_ZSSDMDL.ZBQY));
			p.Add(db.CreateParameter("SDZT",DbType.Int32, _jcsjk_RY_JZS_ZSSDMDL.SDZT));
			p.Add(db.CreateParameter("SDSJ",DbType.DateTime, _jcsjk_RY_JZS_ZSSDMDL.SDSJ));
			p.Add(db.CreateParameter("BZ",DbType.String, _jcsjk_RY_JZS_ZSSDMDL.BZ));
			p.Add(db.CreateParameter("VALID",DbType.Int32, _jcsjk_RY_JZS_ZSSDMDL.VALID));
			p.Add(db.CreateParameter("CJSJ",DbType.DateTime, _jcsjk_RY_JZS_ZSSDMDL.CJSJ));
			p.Add(db.CreateParameter("XGSJ",DbType.DateTime, _jcsjk_RY_JZS_ZSSDMDL.XGSJ));
			p.Add(db.CreateParameter("CJR",DbType.String, _jcsjk_RY_JZS_ZSSDMDL.CJR));
			p.Add(db.CreateParameter("XGR",DbType.String, _jcsjk_RY_JZS_ZSSDMDL.XGR));
			p.Add(db.CreateParameter("CJDEPTID",DbType.String, _jcsjk_RY_JZS_ZSSDMDL.CJDEPTID));
			p.Add(db.CreateParameter("XGDEPTID",DbType.String, _jcsjk_RY_JZS_ZSSDMDL.XGDEPTID));
			p.Add(db.CreateParameter("CheckCode",DbType.String, _jcsjk_RY_JZS_ZSSDMDL.CheckCode));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="jcsjk_RY_JZS_ZSSDID">主键</param>
		/// <returns></returns>
        public static int Delete( Guid ID )
		{
			return Delete(null, ID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="jcsjk_RY_JZS_ZSSDID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, Guid ID)
		{
			string sql=@"DELETE FROM dbo.jcsjk_RY_JZS_ZSSD WHERE ID = @ID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ID",DbType.Guid,ID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="jcsjk_RY_JZS_ZSSDMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(jcsjk_RY_JZS_ZSSDMDL _jcsjk_RY_JZS_ZSSDMDL)
		{
			return Delete(null,_jcsjk_RY_JZS_ZSSDMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="jcsjk_RY_JZS_ZSSDMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,jcsjk_RY_JZS_ZSSDMDL _jcsjk_RY_JZS_ZSSDMDL)
		{
			string sql=@"DELETE FROM dbo.jcsjk_RY_JZS_ZSSD WHERE ID = @ID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ID",DbType.Guid,_jcsjk_RY_JZS_ZSSDMDL.ID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="jcsjk_RY_JZS_ZSSDID">主键</param>
        public static jcsjk_RY_JZS_ZSSDMDL GetObject( Guid ID )
		{
			string sql=@"
			SELECT [ID],XM,ZJHM,ZCH,HTBH,XMMC,ZBQY,SDZT,SDSJ,BZ,[VALID],CJSJ,XGSJ,CJR,XGR,CJDEPTID,XGDEPTID,CheckCode
			FROM dbo.jcsjk_RY_JZS_ZSSD
			WHERE ID = @ID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ID", DbType.Guid, ID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                jcsjk_RY_JZS_ZSSDMDL _jcsjk_RY_JZS_ZSSDMDL = null;
                if (reader.Read())
                {
                    _jcsjk_RY_JZS_ZSSDMDL = new jcsjk_RY_JZS_ZSSDMDL();
					if (reader["ID"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.ID = new Guid(Convert.ToString(reader["ID"]));
					if (reader["XM"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.XM = Convert.ToString(reader["XM"]);
					if (reader["ZJHM"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.ZJHM = Convert.ToString(reader["ZJHM"]);
					if (reader["ZCH"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.ZCH = Convert.ToString(reader["ZCH"]);
					if (reader["HTBH"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.HTBH = Convert.ToString(reader["HTBH"]);
					if (reader["XMMC"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.XMMC = Convert.ToString(reader["XMMC"]);
					if (reader["ZBQY"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.ZBQY = Convert.ToString(reader["ZBQY"]);
					if (reader["SDZT"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.SDZT = Convert.ToInt32(reader["SDZT"]);
					if (reader["SDSJ"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.SDSJ = Convert.ToDateTime(reader["SDSJ"]);
					if (reader["BZ"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.BZ = Convert.ToString(reader["BZ"]);
					if (reader["VALID"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.VALID = Convert.ToInt32(reader["VALID"]);
					if (reader["CJSJ"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
					if (reader["XGSJ"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.XGSJ = Convert.ToDateTime(reader["XGSJ"]);
					if (reader["CJR"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.CJR = Convert.ToString(reader["CJR"]);
					if (reader["XGR"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.XGR = Convert.ToString(reader["XGR"]);
					if (reader["CJDEPTID"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.CJDEPTID = Convert.ToString(reader["CJDEPTID"]);
					if (reader["XGDEPTID"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.XGDEPTID = Convert.ToString(reader["XGDEPTID"]);
					if (reader["CheckCode"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.CheckCode = Convert.ToString(reader["CheckCode"]);
                }
				reader.Close();
                db.Close();
                return _jcsjk_RY_JZS_ZSSDMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.jcsjk_RY_JZS_ZSSD", "*", filterWhereString, orderBy == "" ? " ID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.jcsjk_RY_JZS_ZSSD", filterWhereString);
        }
        
        #region 自定义方法

        /// <summary>
        /// 判断证书是否处在“在施锁定”状态
        /// </summary>
        /// <param name="ZCH">注册证书编号</param>
        /// <returns>在施锁定返回true，否则返回false</returns>
        public static bool IfLocking(string ZCH)
        {
            string sql = @"
			SELECT top 1 [ID],XM,ZJHM,ZCH,HTBH,XMMC,ZBQY,SDZT,SDSJ,BZ,[VALID],CJSJ,XGSJ,CJR,XGR,CJDEPTID,XGDEPTID,CheckCode
			FROM dbo.jcsjk_RY_JZS_ZSSD
			WHERE ZCH = @ZCH
            order by SDSJ desc";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ZCH", DbType.String, ZCH));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                jcsjk_RY_JZS_ZSSDMDL _jcsjk_RY_JZS_ZSSDMDL = null;
                if (reader.Read())
                {
                    _jcsjk_RY_JZS_ZSSDMDL = new jcsjk_RY_JZS_ZSSDMDL();
                    if (reader["ID"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.ID = new Guid(Convert.ToString(reader["ID"]));
                    if (reader["XM"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.XM = Convert.ToString(reader["XM"]);
                    if (reader["ZJHM"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.ZJHM = Convert.ToString(reader["ZJHM"]);
                    if (reader["ZCH"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.ZCH = Convert.ToString(reader["ZCH"]);
                    if (reader["HTBH"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.HTBH = Convert.ToString(reader["HTBH"]);
                    if (reader["XMMC"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.XMMC = Convert.ToString(reader["XMMC"]);
                    if (reader["ZBQY"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.ZBQY = Convert.ToString(reader["ZBQY"]);
                    if (reader["SDZT"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.SDZT = Convert.ToInt32(reader["SDZT"]);
                    if (reader["SDSJ"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.SDSJ = Convert.ToDateTime(reader["SDSJ"]);
                    if (reader["BZ"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.BZ = Convert.ToString(reader["BZ"]);
                    if (reader["VALID"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.VALID = Convert.ToInt32(reader["VALID"]);
                    if (reader["CJSJ"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
                    if (reader["XGSJ"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.XGSJ = Convert.ToDateTime(reader["XGSJ"]);
                    if (reader["CJR"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.CJR = Convert.ToString(reader["CJR"]);
                    if (reader["XGR"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.XGR = Convert.ToString(reader["XGR"]);
                    if (reader["CJDEPTID"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.CJDEPTID = Convert.ToString(reader["CJDEPTID"]);
                    if (reader["XGDEPTID"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.XGDEPTID = Convert.ToString(reader["XGDEPTID"]);
                    if (reader["CheckCode"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.CheckCode = Convert.ToString(reader["CheckCode"]);
                }
                reader.Close();
                db.Close();

                if (_jcsjk_RY_JZS_ZSSDMDL != null && _jcsjk_RY_JZS_ZSSDMDL.SDZT ==1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ZCH">注册证书编号</param>
        /// <returns>在施锁定返回锁定详细信息，否则返回空</returns>
        public static jcsjk_RY_JZS_ZSSDMDL GetLockingInfo(string ZCH)
        {
            string sql = @"
			SELECT top 1 [ID],XM,ZJHM,ZCH,HTBH,XMMC,ZBQY,SDZT,SDSJ,BZ,[VALID],CJSJ,XGSJ,CJR,XGR,CJDEPTID,XGDEPTID,CheckCode
			FROM dbo.jcsjk_RY_JZS_ZSSD
			WHERE ZCH = @ZCH
            order by SDSJ desc";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ZCH", DbType.String, ZCH));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                jcsjk_RY_JZS_ZSSDMDL _jcsjk_RY_JZS_ZSSDMDL = null;
                if (reader.Read())
                {
                    _jcsjk_RY_JZS_ZSSDMDL = new jcsjk_RY_JZS_ZSSDMDL();
                    if (reader["ID"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.ID = new Guid(Convert.ToString(reader["ID"]));
                    if (reader["XM"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.XM = Convert.ToString(reader["XM"]);
                    if (reader["ZJHM"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.ZJHM = Convert.ToString(reader["ZJHM"]);
                    if (reader["ZCH"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.ZCH = Convert.ToString(reader["ZCH"]);
                    if (reader["HTBH"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.HTBH = Convert.ToString(reader["HTBH"]);
                    if (reader["XMMC"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.XMMC = Convert.ToString(reader["XMMC"]);
                    if (reader["ZBQY"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.ZBQY = Convert.ToString(reader["ZBQY"]);
                    if (reader["SDZT"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.SDZT = Convert.ToInt32(reader["SDZT"]);
                    if (reader["SDSJ"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.SDSJ = Convert.ToDateTime(reader["SDSJ"]);
                    if (reader["BZ"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.BZ = Convert.ToString(reader["BZ"]);
                    if (reader["VALID"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.VALID = Convert.ToInt32(reader["VALID"]);
                    if (reader["CJSJ"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
                    if (reader["XGSJ"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.XGSJ = Convert.ToDateTime(reader["XGSJ"]);
                    if (reader["CJR"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.CJR = Convert.ToString(reader["CJR"]);
                    if (reader["XGR"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.XGR = Convert.ToString(reader["XGR"]);
                    if (reader["CJDEPTID"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.CJDEPTID = Convert.ToString(reader["CJDEPTID"]);
                    if (reader["XGDEPTID"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.XGDEPTID = Convert.ToString(reader["XGDEPTID"]);
                    if (reader["CheckCode"] != DBNull.Value) _jcsjk_RY_JZS_ZSSDMDL.CheckCode = Convert.ToString(reader["CheckCode"]);
                }
                reader.Close();
                db.Close();

                if (_jcsjk_RY_JZS_ZSSDMDL != null && _jcsjk_RY_JZS_ZSSDMDL.SDZT == 1)
                {
                    return _jcsjk_RY_JZS_ZSSDMDL;
                }
                else
                {
                    return null;
                }

            }
        }

        #endregion
    }
}
