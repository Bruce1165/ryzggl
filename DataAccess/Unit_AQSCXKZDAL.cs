using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--Unit_AQSCXKZDAL(填写类描述)
	/// </summary>
    public class Unit_AQSCXKZDAL
    {
        public Unit_AQSCXKZDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="Unit_AQSCXKZMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(Unit_AQSCXKZMDL _Unit_AQSCXKZMDL)
		{
		    return Insert(null,_Unit_AQSCXKZMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="Unit_AQSCXKZMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,Unit_AQSCXKZMDL _Unit_AQSCXKZMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.Unit_AQSCXKZ([ID],AQSCXKZBH,ZZJGDM,QYMC,JJLX,DWDZ,ZYFZR,ZYFZRZJLX,ZYFZRZJHM,AQSCXKZSQLX,AQSCXKFW,AQSCXKZKSRQ,AQSCXKZJSRQ,AQSCXKZKSRQ_SYC,AQSCXKZJSRQ_SYC,AQSCXKZFZJG,AQSCXKZFZRQ,AQSCXKZZSZT,BZ,[VALID],CJR,CJDEPTID,CJSJ,XGR,XGDEPTID,XGSJ,SFDYZS,CheckCode)
			VALUES (@ID,@AQSCXKZBH,@ZZJGDM,@QYMC,@JJLX,@DWDZ,@ZYFZR,@ZYFZRZJLX,@ZYFZRZJHM,@AQSCXKZSQLX,@AQSCXKFW,@AQSCXKZKSRQ,@AQSCXKZJSRQ,@AQSCXKZKSRQ_SYC,@AQSCXKZJSRQ_SYC,@AQSCXKZFZJG,@AQSCXKZFZRQ,@AQSCXKZZSZT,@BZ,@VALID,@CJR,@CJDEPTID,@CJSJ,@XGR,@XGDEPTID,@XGSJ,@SFDYZS,@CheckCode)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ID",DbType.Guid, _Unit_AQSCXKZMDL.ID));
			p.Add(db.CreateParameter("AQSCXKZBH",DbType.String, _Unit_AQSCXKZMDL.AQSCXKZBH));
			p.Add(db.CreateParameter("ZZJGDM",DbType.String, _Unit_AQSCXKZMDL.ZZJGDM));
			p.Add(db.CreateParameter("QYMC",DbType.String, _Unit_AQSCXKZMDL.QYMC));
			p.Add(db.CreateParameter("JJLX",DbType.String, _Unit_AQSCXKZMDL.JJLX));
			p.Add(db.CreateParameter("DWDZ",DbType.String, _Unit_AQSCXKZMDL.DWDZ));
			p.Add(db.CreateParameter("ZYFZR",DbType.String, _Unit_AQSCXKZMDL.ZYFZR));
			p.Add(db.CreateParameter("ZYFZRZJLX",DbType.String, _Unit_AQSCXKZMDL.ZYFZRZJLX));
			p.Add(db.CreateParameter("ZYFZRZJHM",DbType.String, _Unit_AQSCXKZMDL.ZYFZRZJHM));
			p.Add(db.CreateParameter("AQSCXKZSQLX",DbType.String, _Unit_AQSCXKZMDL.AQSCXKZSQLX));
			p.Add(db.CreateParameter("AQSCXKFW",DbType.String, _Unit_AQSCXKZMDL.AQSCXKFW));
			p.Add(db.CreateParameter("AQSCXKZKSRQ",DbType.DateTime, _Unit_AQSCXKZMDL.AQSCXKZKSRQ));
			p.Add(db.CreateParameter("AQSCXKZJSRQ",DbType.DateTime, _Unit_AQSCXKZMDL.AQSCXKZJSRQ));
			p.Add(db.CreateParameter("AQSCXKZKSRQ_SYC",DbType.DateTime, _Unit_AQSCXKZMDL.AQSCXKZKSRQ_SYC));
			p.Add(db.CreateParameter("AQSCXKZJSRQ_SYC",DbType.DateTime, _Unit_AQSCXKZMDL.AQSCXKZJSRQ_SYC));
			p.Add(db.CreateParameter("AQSCXKZFZJG",DbType.String, _Unit_AQSCXKZMDL.AQSCXKZFZJG));
			p.Add(db.CreateParameter("AQSCXKZFZRQ",DbType.DateTime, _Unit_AQSCXKZMDL.AQSCXKZFZRQ));
			p.Add(db.CreateParameter("AQSCXKZZSZT",DbType.String, _Unit_AQSCXKZMDL.AQSCXKZZSZT));
			p.Add(db.CreateParameter("BZ",DbType.String, _Unit_AQSCXKZMDL.BZ));
			p.Add(db.CreateParameter("VALID",DbType.Int32, _Unit_AQSCXKZMDL.VALID));
			p.Add(db.CreateParameter("CJR",DbType.String, _Unit_AQSCXKZMDL.CJR));
			p.Add(db.CreateParameter("CJDEPTID",DbType.String, _Unit_AQSCXKZMDL.CJDEPTID));
			p.Add(db.CreateParameter("CJSJ",DbType.DateTime, _Unit_AQSCXKZMDL.CJSJ));
			p.Add(db.CreateParameter("XGR",DbType.String, _Unit_AQSCXKZMDL.XGR));
			p.Add(db.CreateParameter("XGDEPTID",DbType.String, _Unit_AQSCXKZMDL.XGDEPTID));
			p.Add(db.CreateParameter("XGSJ",DbType.DateTime, _Unit_AQSCXKZMDL.XGSJ));
			p.Add(db.CreateParameter("SFDYZS",DbType.String, _Unit_AQSCXKZMDL.SFDYZS));
			p.Add(db.CreateParameter("CheckCode",DbType.String, _Unit_AQSCXKZMDL.CheckCode));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="Unit_AQSCXKZMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(Unit_AQSCXKZMDL _Unit_AQSCXKZMDL)
		{
			return Update(null,_Unit_AQSCXKZMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="Unit_AQSCXKZMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,Unit_AQSCXKZMDL _Unit_AQSCXKZMDL)
		{
			string sql = @"
			UPDATE dbo.Unit_AQSCXKZ
				SET	AQSCXKZBH = @AQSCXKZBH,ZZJGDM = @ZZJGDM,QYMC = @QYMC,JJLX = @JJLX,DWDZ = @DWDZ,ZYFZR = @ZYFZR,ZYFZRZJLX = @ZYFZRZJLX,ZYFZRZJHM = @ZYFZRZJHM,AQSCXKZSQLX = @AQSCXKZSQLX,AQSCXKFW = @AQSCXKFW,AQSCXKZKSRQ = @AQSCXKZKSRQ,AQSCXKZJSRQ = @AQSCXKZJSRQ,AQSCXKZKSRQ_SYC = @AQSCXKZKSRQ_SYC,AQSCXKZJSRQ_SYC = @AQSCXKZJSRQ_SYC,AQSCXKZFZJG = @AQSCXKZFZJG,AQSCXKZFZRQ = @AQSCXKZFZRQ,AQSCXKZZSZT = @AQSCXKZZSZT,BZ = @BZ,""VALID"" = @VALID,CJR = @CJR,CJDEPTID = @CJDEPTID,CJSJ = @CJSJ,XGR = @XGR,XGDEPTID = @XGDEPTID,XGSJ = @XGSJ,SFDYZS = @SFDYZS,CheckCode = @CheckCode
			WHERE
				ID = @ID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ID",DbType.Guid, _Unit_AQSCXKZMDL.ID));
			p.Add(db.CreateParameter("AQSCXKZBH",DbType.String, _Unit_AQSCXKZMDL.AQSCXKZBH));
			p.Add(db.CreateParameter("ZZJGDM",DbType.String, _Unit_AQSCXKZMDL.ZZJGDM));
			p.Add(db.CreateParameter("QYMC",DbType.String, _Unit_AQSCXKZMDL.QYMC));
			p.Add(db.CreateParameter("JJLX",DbType.String, _Unit_AQSCXKZMDL.JJLX));
			p.Add(db.CreateParameter("DWDZ",DbType.String, _Unit_AQSCXKZMDL.DWDZ));
			p.Add(db.CreateParameter("ZYFZR",DbType.String, _Unit_AQSCXKZMDL.ZYFZR));
			p.Add(db.CreateParameter("ZYFZRZJLX",DbType.String, _Unit_AQSCXKZMDL.ZYFZRZJLX));
			p.Add(db.CreateParameter("ZYFZRZJHM",DbType.String, _Unit_AQSCXKZMDL.ZYFZRZJHM));
			p.Add(db.CreateParameter("AQSCXKZSQLX",DbType.String, _Unit_AQSCXKZMDL.AQSCXKZSQLX));
			p.Add(db.CreateParameter("AQSCXKFW",DbType.String, _Unit_AQSCXKZMDL.AQSCXKFW));
			p.Add(db.CreateParameter("AQSCXKZKSRQ",DbType.DateTime, _Unit_AQSCXKZMDL.AQSCXKZKSRQ));
			p.Add(db.CreateParameter("AQSCXKZJSRQ",DbType.DateTime, _Unit_AQSCXKZMDL.AQSCXKZJSRQ));
			p.Add(db.CreateParameter("AQSCXKZKSRQ_SYC",DbType.DateTime, _Unit_AQSCXKZMDL.AQSCXKZKSRQ_SYC));
			p.Add(db.CreateParameter("AQSCXKZJSRQ_SYC",DbType.DateTime, _Unit_AQSCXKZMDL.AQSCXKZJSRQ_SYC));
			p.Add(db.CreateParameter("AQSCXKZFZJG",DbType.String, _Unit_AQSCXKZMDL.AQSCXKZFZJG));
			p.Add(db.CreateParameter("AQSCXKZFZRQ",DbType.DateTime, _Unit_AQSCXKZMDL.AQSCXKZFZRQ));
			p.Add(db.CreateParameter("AQSCXKZZSZT",DbType.String, _Unit_AQSCXKZMDL.AQSCXKZZSZT));
			p.Add(db.CreateParameter("BZ",DbType.String, _Unit_AQSCXKZMDL.BZ));
			p.Add(db.CreateParameter("VALID",DbType.Int32, _Unit_AQSCXKZMDL.VALID));
			p.Add(db.CreateParameter("CJR",DbType.String, _Unit_AQSCXKZMDL.CJR));
			p.Add(db.CreateParameter("CJDEPTID",DbType.String, _Unit_AQSCXKZMDL.CJDEPTID));
			p.Add(db.CreateParameter("CJSJ",DbType.DateTime, _Unit_AQSCXKZMDL.CJSJ));
			p.Add(db.CreateParameter("XGR",DbType.String, _Unit_AQSCXKZMDL.XGR));
			p.Add(db.CreateParameter("XGDEPTID",DbType.String, _Unit_AQSCXKZMDL.XGDEPTID));
			p.Add(db.CreateParameter("XGSJ",DbType.DateTime, _Unit_AQSCXKZMDL.XGSJ));
			p.Add(db.CreateParameter("SFDYZS",DbType.String, _Unit_AQSCXKZMDL.SFDYZS));
			p.Add(db.CreateParameter("CheckCode",DbType.String, _Unit_AQSCXKZMDL.CheckCode));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="Unit_AQSCXKZID">主键</param>
		/// <returns></returns>
        public static int Delete( Guid ID )
		{
			return Delete(null, ID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="Unit_AQSCXKZID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, Guid ID)
		{
			string sql=@"DELETE FROM dbo.Unit_AQSCXKZ WHERE ID = @ID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ID",DbType.Guid,ID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="Unit_AQSCXKZMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(Unit_AQSCXKZMDL _Unit_AQSCXKZMDL)
		{
			return Delete(null,_Unit_AQSCXKZMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="Unit_AQSCXKZMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,Unit_AQSCXKZMDL _Unit_AQSCXKZMDL)
		{
			string sql=@"DELETE FROM dbo.Unit_AQSCXKZ WHERE ID = @ID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ID",DbType.Guid,_Unit_AQSCXKZMDL.ID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="Unit_AQSCXKZID">主键</param>
        public static Unit_AQSCXKZMDL GetObject( Guid ID )
		{
			string sql=@"
			SELECT [ID],AQSCXKZBH,ZZJGDM,QYMC,JJLX,DWDZ,ZYFZR,ZYFZRZJLX,ZYFZRZJHM,AQSCXKZSQLX,AQSCXKFW,AQSCXKZKSRQ,AQSCXKZJSRQ,AQSCXKZKSRQ_SYC,AQSCXKZJSRQ_SYC,AQSCXKZFZJG,AQSCXKZFZRQ,AQSCXKZZSZT,BZ,[VALID],CJR,CJDEPTID,CJSJ,XGR,XGDEPTID,XGSJ,SFDYZS,CheckCode
			FROM dbo.Unit_AQSCXKZ
			WHERE ID = @ID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ID", DbType.Guid, ID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                Unit_AQSCXKZMDL _Unit_AQSCXKZMDL = null;
                if (reader.Read())
                {
                    _Unit_AQSCXKZMDL = new Unit_AQSCXKZMDL();
					if (reader["ID"] != DBNull.Value) _Unit_AQSCXKZMDL.ID = new Guid(Convert.ToString(reader["ID"]));
					if (reader["AQSCXKZBH"] != DBNull.Value) _Unit_AQSCXKZMDL.AQSCXKZBH = Convert.ToString(reader["AQSCXKZBH"]);
					if (reader["ZZJGDM"] != DBNull.Value) _Unit_AQSCXKZMDL.ZZJGDM = Convert.ToString(reader["ZZJGDM"]);
					if (reader["QYMC"] != DBNull.Value) _Unit_AQSCXKZMDL.QYMC = Convert.ToString(reader["QYMC"]);
					if (reader["JJLX"] != DBNull.Value) _Unit_AQSCXKZMDL.JJLX = Convert.ToString(reader["JJLX"]);
					if (reader["DWDZ"] != DBNull.Value) _Unit_AQSCXKZMDL.DWDZ = Convert.ToString(reader["DWDZ"]);
					if (reader["ZYFZR"] != DBNull.Value) _Unit_AQSCXKZMDL.ZYFZR = Convert.ToString(reader["ZYFZR"]);
					if (reader["ZYFZRZJLX"] != DBNull.Value) _Unit_AQSCXKZMDL.ZYFZRZJLX = Convert.ToString(reader["ZYFZRZJLX"]);
					if (reader["ZYFZRZJHM"] != DBNull.Value) _Unit_AQSCXKZMDL.ZYFZRZJHM = Convert.ToString(reader["ZYFZRZJHM"]);
					if (reader["AQSCXKZSQLX"] != DBNull.Value) _Unit_AQSCXKZMDL.AQSCXKZSQLX = Convert.ToString(reader["AQSCXKZSQLX"]);
					if (reader["AQSCXKFW"] != DBNull.Value) _Unit_AQSCXKZMDL.AQSCXKFW = Convert.ToString(reader["AQSCXKFW"]);
					if (reader["AQSCXKZKSRQ"] != DBNull.Value) _Unit_AQSCXKZMDL.AQSCXKZKSRQ = Convert.ToDateTime(reader["AQSCXKZKSRQ"]);
					if (reader["AQSCXKZJSRQ"] != DBNull.Value) _Unit_AQSCXKZMDL.AQSCXKZJSRQ = Convert.ToDateTime(reader["AQSCXKZJSRQ"]);
					if (reader["AQSCXKZKSRQ_SYC"] != DBNull.Value) _Unit_AQSCXKZMDL.AQSCXKZKSRQ_SYC = Convert.ToDateTime(reader["AQSCXKZKSRQ_SYC"]);
					if (reader["AQSCXKZJSRQ_SYC"] != DBNull.Value) _Unit_AQSCXKZMDL.AQSCXKZJSRQ_SYC = Convert.ToDateTime(reader["AQSCXKZJSRQ_SYC"]);
					if (reader["AQSCXKZFZJG"] != DBNull.Value) _Unit_AQSCXKZMDL.AQSCXKZFZJG = Convert.ToString(reader["AQSCXKZFZJG"]);
					if (reader["AQSCXKZFZRQ"] != DBNull.Value) _Unit_AQSCXKZMDL.AQSCXKZFZRQ = Convert.ToDateTime(reader["AQSCXKZFZRQ"]);
					if (reader["AQSCXKZZSZT"] != DBNull.Value) _Unit_AQSCXKZMDL.AQSCXKZZSZT = Convert.ToString(reader["AQSCXKZZSZT"]);
					if (reader["BZ"] != DBNull.Value) _Unit_AQSCXKZMDL.BZ = Convert.ToString(reader["BZ"]);
					if (reader["VALID"] != DBNull.Value) _Unit_AQSCXKZMDL.VALID = Convert.ToInt32(reader["VALID"]);
					if (reader["CJR"] != DBNull.Value) _Unit_AQSCXKZMDL.CJR = Convert.ToString(reader["CJR"]);
					if (reader["CJDEPTID"] != DBNull.Value) _Unit_AQSCXKZMDL.CJDEPTID = Convert.ToString(reader["CJDEPTID"]);
					if (reader["CJSJ"] != DBNull.Value) _Unit_AQSCXKZMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
					if (reader["XGR"] != DBNull.Value) _Unit_AQSCXKZMDL.XGR = Convert.ToString(reader["XGR"]);
					if (reader["XGDEPTID"] != DBNull.Value) _Unit_AQSCXKZMDL.XGDEPTID = Convert.ToString(reader["XGDEPTID"]);
					if (reader["XGSJ"] != DBNull.Value) _Unit_AQSCXKZMDL.XGSJ = Convert.ToDateTime(reader["XGSJ"]);
					if (reader["SFDYZS"] != DBNull.Value) _Unit_AQSCXKZMDL.SFDYZS = Convert.ToString(reader["SFDYZS"]);
					if (reader["CheckCode"] != DBNull.Value) _Unit_AQSCXKZMDL.CheckCode = Convert.ToString(reader["CheckCode"]);
                }
				reader.Close();
                db.Close();
                return _Unit_AQSCXKZMDL;
            }
		}
        /// <summary>
        /// 根据组织机构代码获取单个实体
        /// </summary>
        /// <param name="Unit_AQSCXKZID">主键</param>
        public static Unit_AQSCXKZMDL GetObjectZzjgdm(string ZZJGDM)
        {
            string sql = @"
			SELECT [ID],AQSCXKZBH,ZZJGDM,QYMC,JJLX,DWDZ,ZYFZR,ZYFZRZJLX,ZYFZRZJHM,AQSCXKZSQLX,AQSCXKFW,AQSCXKZKSRQ,AQSCXKZJSRQ,AQSCXKZKSRQ_SYC,AQSCXKZJSRQ_SYC,AQSCXKZFZJG,AQSCXKZFZRQ,AQSCXKZZSZT,BZ,[VALID],CJR,CJDEPTID,CJSJ,XGR,XGDEPTID,XGSJ,SFDYZS,CheckCode
			FROM dbo.Unit_AQSCXKZ
			WHERE ZZJGDM = @ZZJGDM";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ZZJGDM", DbType.String, ZZJGDM));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                Unit_AQSCXKZMDL _Unit_AQSCXKZMDL = null;
                if (reader.Read())
                {
                    _Unit_AQSCXKZMDL = new Unit_AQSCXKZMDL();
                    if (reader["ID"] != DBNull.Value) _Unit_AQSCXKZMDL.ID = new Guid(Convert.ToString(reader["ID"]));
                    if (reader["AQSCXKZBH"] != DBNull.Value) _Unit_AQSCXKZMDL.AQSCXKZBH = Convert.ToString(reader["AQSCXKZBH"]);
                    if (reader["ZZJGDM"] != DBNull.Value) _Unit_AQSCXKZMDL.ZZJGDM = Convert.ToString(reader["ZZJGDM"]);
                    if (reader["QYMC"] != DBNull.Value) _Unit_AQSCXKZMDL.QYMC = Convert.ToString(reader["QYMC"]);
                    if (reader["JJLX"] != DBNull.Value) _Unit_AQSCXKZMDL.JJLX = Convert.ToString(reader["JJLX"]);
                    if (reader["DWDZ"] != DBNull.Value) _Unit_AQSCXKZMDL.DWDZ = Convert.ToString(reader["DWDZ"]);
                    if (reader["ZYFZR"] != DBNull.Value) _Unit_AQSCXKZMDL.ZYFZR = Convert.ToString(reader["ZYFZR"]);
                    if (reader["ZYFZRZJLX"] != DBNull.Value) _Unit_AQSCXKZMDL.ZYFZRZJLX = Convert.ToString(reader["ZYFZRZJLX"]);
                    if (reader["ZYFZRZJHM"] != DBNull.Value) _Unit_AQSCXKZMDL.ZYFZRZJHM = Convert.ToString(reader["ZYFZRZJHM"]);
                    if (reader["AQSCXKZSQLX"] != DBNull.Value) _Unit_AQSCXKZMDL.AQSCXKZSQLX = Convert.ToString(reader["AQSCXKZSQLX"]);
                    if (reader["AQSCXKFW"] != DBNull.Value) _Unit_AQSCXKZMDL.AQSCXKFW = Convert.ToString(reader["AQSCXKFW"]);
                    if (reader["AQSCXKZKSRQ"] != DBNull.Value) _Unit_AQSCXKZMDL.AQSCXKZKSRQ = Convert.ToDateTime(reader["AQSCXKZKSRQ"]);
                    if (reader["AQSCXKZJSRQ"] != DBNull.Value) _Unit_AQSCXKZMDL.AQSCXKZJSRQ = Convert.ToDateTime(reader["AQSCXKZJSRQ"]);
                    if (reader["AQSCXKZKSRQ_SYC"] != DBNull.Value) _Unit_AQSCXKZMDL.AQSCXKZKSRQ_SYC = Convert.ToDateTime(reader["AQSCXKZKSRQ_SYC"]);
                    if (reader["AQSCXKZJSRQ_SYC"] != DBNull.Value) _Unit_AQSCXKZMDL.AQSCXKZJSRQ_SYC = Convert.ToDateTime(reader["AQSCXKZJSRQ_SYC"]);
                    if (reader["AQSCXKZFZJG"] != DBNull.Value) _Unit_AQSCXKZMDL.AQSCXKZFZJG = Convert.ToString(reader["AQSCXKZFZJG"]);
                    if (reader["AQSCXKZFZRQ"] != DBNull.Value) _Unit_AQSCXKZMDL.AQSCXKZFZRQ = Convert.ToDateTime(reader["AQSCXKZFZRQ"]);
                    if (reader["AQSCXKZZSZT"] != DBNull.Value) _Unit_AQSCXKZMDL.AQSCXKZZSZT = Convert.ToString(reader["AQSCXKZZSZT"]);
                    if (reader["BZ"] != DBNull.Value) _Unit_AQSCXKZMDL.BZ = Convert.ToString(reader["BZ"]);
                    if (reader["VALID"] != DBNull.Value) _Unit_AQSCXKZMDL.VALID = Convert.ToInt32(reader["VALID"]);
                    if (reader["CJR"] != DBNull.Value) _Unit_AQSCXKZMDL.CJR = Convert.ToString(reader["CJR"]);
                    if (reader["CJDEPTID"] != DBNull.Value) _Unit_AQSCXKZMDL.CJDEPTID = Convert.ToString(reader["CJDEPTID"]);
                    if (reader["CJSJ"] != DBNull.Value) _Unit_AQSCXKZMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
                    if (reader["XGR"] != DBNull.Value) _Unit_AQSCXKZMDL.XGR = Convert.ToString(reader["XGR"]);
                    if (reader["XGDEPTID"] != DBNull.Value) _Unit_AQSCXKZMDL.XGDEPTID = Convert.ToString(reader["XGDEPTID"]);
                    if (reader["XGSJ"] != DBNull.Value) _Unit_AQSCXKZMDL.XGSJ = Convert.ToDateTime(reader["XGSJ"]);
                    if (reader["SFDYZS"] != DBNull.Value) _Unit_AQSCXKZMDL.SFDYZS = Convert.ToString(reader["SFDYZS"]);
                    if (reader["CheckCode"] != DBNull.Value) _Unit_AQSCXKZMDL.CheckCode = Convert.ToString(reader["CheckCode"]);
                }
                reader.Close();
                db.Close();
                return _Unit_AQSCXKZMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.Unit_AQSCXKZ", "*", filterWhereString, orderBy == "" ? " ID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.Unit_AQSCXKZ", filterWhereString);
        }
        
        #region 自定义方法
        
        #endregion
    }
}
