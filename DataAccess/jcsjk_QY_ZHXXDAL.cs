using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--jcsjk_QY_ZHXXDAL(填写类描述)
	/// </summary>
    public class jcsjk_QY_ZHXXDAL
    {
        public jcsjk_QY_ZHXXDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="jcsjk_QY_ZHXXMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(jcsjk_QY_ZHXXMDL _jcsjk_QY_ZHXXMDL)
		{
		    return Insert(null,_jcsjk_QY_ZHXXMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="jcsjk_QY_ZHXXMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,jcsjk_QY_ZHXXMDL _jcsjk_QY_ZHXXMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.jcsjk_QY_ZHXX([ID],ZZZSBH,ZZJGDM,QYMC,ZCDZ,XXDZ,JLSJ,ZCZBJ,YYZZZCH,JJLX,ZXZZDJ,ZXZZ,FDDBR,FDDBRZJLX,FDDBRZJHM,FDDBRZW,FDDBRZC,QYFZR,QYFZRZJLX,QYFZRZJHM,QYFZRZW,QYFZRZC,JSFZR,JSFZRZJLX,JSFZRZJHM,JSFZRZW,JSFZRZC,QYZZLX,FZJG,FZRQ,ZSYXQKS,ZSYXQJS,YWFW,BZ,[VALID],CJR,CJDEPTID,CJSJ,XGR,XGDEPTID,XGSJ,SFDYZS,SJLX,YXSX,XZDQBM,HYLZGX)
			VALUES (@ID,@ZZZSBH,@ZZJGDM,@QYMC,@ZCDZ,@XXDZ,@JLSJ,@ZCZBJ,@YYZZZCH,@JJLX,@ZXZZDJ,@ZXZZ,@FDDBR,@FDDBRZJLX,@FDDBRZJHM,@FDDBRZW,@FDDBRZC,@QYFZR,@QYFZRZJLX,@QYFZRZJHM,@QYFZRZW,@QYFZRZC,@JSFZR,@JSFZRZJLX,@JSFZRZJHM,@JSFZRZW,@JSFZRZC,@QYZZLX,@FZJG,@FZRQ,@ZSYXQKS,@ZSYXQJS,@YWFW,@BZ,@VALID,@CJR,@CJDEPTID,@CJSJ,@XGR,@XGDEPTID,@XGSJ,@SFDYZS,@SJLX,@YXSX,@XZDQBM,@HYLZGX)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ID",DbType.Guid, _jcsjk_QY_ZHXXMDL.ID));
			p.Add(db.CreateParameter("ZZZSBH",DbType.String, _jcsjk_QY_ZHXXMDL.ZZZSBH));
			p.Add(db.CreateParameter("ZZJGDM",DbType.String, _jcsjk_QY_ZHXXMDL.ZZJGDM));
			p.Add(db.CreateParameter("QYMC",DbType.String, _jcsjk_QY_ZHXXMDL.QYMC));
			p.Add(db.CreateParameter("ZCDZ",DbType.String, _jcsjk_QY_ZHXXMDL.ZCDZ));
			p.Add(db.CreateParameter("XXDZ",DbType.String, _jcsjk_QY_ZHXXMDL.XXDZ));
			p.Add(db.CreateParameter("JLSJ",DbType.DateTime, _jcsjk_QY_ZHXXMDL.JLSJ));
			p.Add(db.CreateParameter("ZCZBJ",DbType.Decimal, _jcsjk_QY_ZHXXMDL.ZCZBJ));
			p.Add(db.CreateParameter("YYZZZCH",DbType.String, _jcsjk_QY_ZHXXMDL.YYZZZCH));
			p.Add(db.CreateParameter("JJLX",DbType.String, _jcsjk_QY_ZHXXMDL.JJLX));
			p.Add(db.CreateParameter("ZXZZDJ",DbType.String, _jcsjk_QY_ZHXXMDL.ZXZZDJ));
			p.Add(db.CreateParameter("ZXZZ",DbType.String, _jcsjk_QY_ZHXXMDL.ZXZZ));
			p.Add(db.CreateParameter("FDDBR",DbType.String, _jcsjk_QY_ZHXXMDL.FDDBR));
			p.Add(db.CreateParameter("FDDBRZJLX",DbType.String, _jcsjk_QY_ZHXXMDL.FDDBRZJLX));
			p.Add(db.CreateParameter("FDDBRZJHM",DbType.String, _jcsjk_QY_ZHXXMDL.FDDBRZJHM));
			p.Add(db.CreateParameter("FDDBRZW",DbType.String, _jcsjk_QY_ZHXXMDL.FDDBRZW));
			p.Add(db.CreateParameter("FDDBRZC",DbType.String, _jcsjk_QY_ZHXXMDL.FDDBRZC));
			p.Add(db.CreateParameter("QYFZR",DbType.String, _jcsjk_QY_ZHXXMDL.QYFZR));
			p.Add(db.CreateParameter("QYFZRZJLX",DbType.String, _jcsjk_QY_ZHXXMDL.QYFZRZJLX));
			p.Add(db.CreateParameter("QYFZRZJHM",DbType.String, _jcsjk_QY_ZHXXMDL.QYFZRZJHM));
			p.Add(db.CreateParameter("QYFZRZW",DbType.String, _jcsjk_QY_ZHXXMDL.QYFZRZW));
			p.Add(db.CreateParameter("QYFZRZC",DbType.String, _jcsjk_QY_ZHXXMDL.QYFZRZC));
			p.Add(db.CreateParameter("JSFZR",DbType.String, _jcsjk_QY_ZHXXMDL.JSFZR));
			p.Add(db.CreateParameter("JSFZRZJLX",DbType.String, _jcsjk_QY_ZHXXMDL.JSFZRZJLX));
			p.Add(db.CreateParameter("JSFZRZJHM",DbType.String, _jcsjk_QY_ZHXXMDL.JSFZRZJHM));
			p.Add(db.CreateParameter("JSFZRZW",DbType.String, _jcsjk_QY_ZHXXMDL.JSFZRZW));
			p.Add(db.CreateParameter("JSFZRZC",DbType.String, _jcsjk_QY_ZHXXMDL.JSFZRZC));
			p.Add(db.CreateParameter("QYZZLX",DbType.String, _jcsjk_QY_ZHXXMDL.QYZZLX));
			p.Add(db.CreateParameter("FZJG",DbType.String, _jcsjk_QY_ZHXXMDL.FZJG));
			p.Add(db.CreateParameter("FZRQ",DbType.DateTime, _jcsjk_QY_ZHXXMDL.FZRQ));
			p.Add(db.CreateParameter("ZSYXQKS",DbType.DateTime, _jcsjk_QY_ZHXXMDL.ZSYXQKS));
			p.Add(db.CreateParameter("ZSYXQJS",DbType.DateTime, _jcsjk_QY_ZHXXMDL.ZSYXQJS));
			p.Add(db.CreateParameter("YWFW",DbType.String, _jcsjk_QY_ZHXXMDL.YWFW));
			p.Add(db.CreateParameter("BZ",DbType.String, _jcsjk_QY_ZHXXMDL.BZ));
			p.Add(db.CreateParameter("VALID",DbType.Int32, _jcsjk_QY_ZHXXMDL.VALID));
			p.Add(db.CreateParameter("CJR",DbType.String, _jcsjk_QY_ZHXXMDL.CJR));
			p.Add(db.CreateParameter("CJDEPTID",DbType.String, _jcsjk_QY_ZHXXMDL.CJDEPTID));
			p.Add(db.CreateParameter("CJSJ",DbType.DateTime, _jcsjk_QY_ZHXXMDL.CJSJ));
			p.Add(db.CreateParameter("XGR",DbType.String, _jcsjk_QY_ZHXXMDL.XGR));
			p.Add(db.CreateParameter("XGDEPTID",DbType.String, _jcsjk_QY_ZHXXMDL.XGDEPTID));
			p.Add(db.CreateParameter("XGSJ",DbType.DateTime, _jcsjk_QY_ZHXXMDL.XGSJ));
			p.Add(db.CreateParameter("SFDYZS",DbType.String, _jcsjk_QY_ZHXXMDL.SFDYZS));
			p.Add(db.CreateParameter("SJLX",DbType.String, _jcsjk_QY_ZHXXMDL.SJLX));
			p.Add(db.CreateParameter("YXSX",DbType.Int32, _jcsjk_QY_ZHXXMDL.YXSX));
			p.Add(db.CreateParameter("XZDQBM",DbType.String, _jcsjk_QY_ZHXXMDL.XZDQBM));
			p.Add(db.CreateParameter("HYLZGX",DbType.String, _jcsjk_QY_ZHXXMDL.HYLZGX));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="jcsjk_QY_ZHXXMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(jcsjk_QY_ZHXXMDL _jcsjk_QY_ZHXXMDL)
		{
			return Update(null,_jcsjk_QY_ZHXXMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="jcsjk_QY_ZHXXMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,jcsjk_QY_ZHXXMDL _jcsjk_QY_ZHXXMDL)
		{
			string sql = @"
			UPDATE dbo.jcsjk_QY_ZHXX
				SET	ZZZSBH = @ZZZSBH,ZZJGDM = @ZZJGDM,QYMC = @QYMC,ZCDZ = @ZCDZ,XXDZ = @XXDZ,JLSJ = @JLSJ,ZCZBJ = @ZCZBJ,YYZZZCH = @YYZZZCH,JJLX = @JJLX,ZXZZDJ = @ZXZZDJ,ZXZZ = @ZXZZ,FDDBR = @FDDBR,FDDBRZJLX = @FDDBRZJLX,FDDBRZJHM = @FDDBRZJHM,FDDBRZW = @FDDBRZW,FDDBRZC = @FDDBRZC,QYFZR = @QYFZR,QYFZRZJLX = @QYFZRZJLX,QYFZRZJHM = @QYFZRZJHM,QYFZRZW = @QYFZRZW,QYFZRZC = @QYFZRZC,JSFZR = @JSFZR,JSFZRZJLX = @JSFZRZJLX,JSFZRZJHM = @JSFZRZJHM,JSFZRZW = @JSFZRZW,JSFZRZC = @JSFZRZC,QYZZLX = @QYZZLX,FZJG = @FZJG,FZRQ = @FZRQ,ZSYXQKS = @ZSYXQKS,ZSYXQJS = @ZSYXQJS,YWFW = @YWFW,BZ = @BZ,""VALID"" = @VALID,CJR = @CJR,CJDEPTID = @CJDEPTID,CJSJ = @CJSJ,XGR = @XGR,XGDEPTID = @XGDEPTID,XGSJ = @XGSJ,SFDYZS = @SFDYZS,SJLX = @SJLX,YXSX = @YXSX,XZDQBM = @XZDQBM,HYLZGX = @HYLZGX
			WHERE
				ID = @ID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ID",DbType.Guid, _jcsjk_QY_ZHXXMDL.ID));
			p.Add(db.CreateParameter("ZZZSBH",DbType.String, _jcsjk_QY_ZHXXMDL.ZZZSBH));
			p.Add(db.CreateParameter("ZZJGDM",DbType.String, _jcsjk_QY_ZHXXMDL.ZZJGDM));
			p.Add(db.CreateParameter("QYMC",DbType.String, _jcsjk_QY_ZHXXMDL.QYMC));
			p.Add(db.CreateParameter("ZCDZ",DbType.String, _jcsjk_QY_ZHXXMDL.ZCDZ));
			p.Add(db.CreateParameter("XXDZ",DbType.String, _jcsjk_QY_ZHXXMDL.XXDZ));
			p.Add(db.CreateParameter("JLSJ",DbType.DateTime, _jcsjk_QY_ZHXXMDL.JLSJ));
			p.Add(db.CreateParameter("ZCZBJ",DbType.Decimal, _jcsjk_QY_ZHXXMDL.ZCZBJ));
			p.Add(db.CreateParameter("YYZZZCH",DbType.String, _jcsjk_QY_ZHXXMDL.YYZZZCH));
			p.Add(db.CreateParameter("JJLX",DbType.String, _jcsjk_QY_ZHXXMDL.JJLX));
			p.Add(db.CreateParameter("ZXZZDJ",DbType.String, _jcsjk_QY_ZHXXMDL.ZXZZDJ));
			p.Add(db.CreateParameter("ZXZZ",DbType.String, _jcsjk_QY_ZHXXMDL.ZXZZ));
			p.Add(db.CreateParameter("FDDBR",DbType.String, _jcsjk_QY_ZHXXMDL.FDDBR));
			p.Add(db.CreateParameter("FDDBRZJLX",DbType.String, _jcsjk_QY_ZHXXMDL.FDDBRZJLX));
			p.Add(db.CreateParameter("FDDBRZJHM",DbType.String, _jcsjk_QY_ZHXXMDL.FDDBRZJHM));
			p.Add(db.CreateParameter("FDDBRZW",DbType.String, _jcsjk_QY_ZHXXMDL.FDDBRZW));
			p.Add(db.CreateParameter("FDDBRZC",DbType.String, _jcsjk_QY_ZHXXMDL.FDDBRZC));
			p.Add(db.CreateParameter("QYFZR",DbType.String, _jcsjk_QY_ZHXXMDL.QYFZR));
			p.Add(db.CreateParameter("QYFZRZJLX",DbType.String, _jcsjk_QY_ZHXXMDL.QYFZRZJLX));
			p.Add(db.CreateParameter("QYFZRZJHM",DbType.String, _jcsjk_QY_ZHXXMDL.QYFZRZJHM));
			p.Add(db.CreateParameter("QYFZRZW",DbType.String, _jcsjk_QY_ZHXXMDL.QYFZRZW));
			p.Add(db.CreateParameter("QYFZRZC",DbType.String, _jcsjk_QY_ZHXXMDL.QYFZRZC));
			p.Add(db.CreateParameter("JSFZR",DbType.String, _jcsjk_QY_ZHXXMDL.JSFZR));
			p.Add(db.CreateParameter("JSFZRZJLX",DbType.String, _jcsjk_QY_ZHXXMDL.JSFZRZJLX));
			p.Add(db.CreateParameter("JSFZRZJHM",DbType.String, _jcsjk_QY_ZHXXMDL.JSFZRZJHM));
			p.Add(db.CreateParameter("JSFZRZW",DbType.String, _jcsjk_QY_ZHXXMDL.JSFZRZW));
			p.Add(db.CreateParameter("JSFZRZC",DbType.String, _jcsjk_QY_ZHXXMDL.JSFZRZC));
			p.Add(db.CreateParameter("QYZZLX",DbType.String, _jcsjk_QY_ZHXXMDL.QYZZLX));
			p.Add(db.CreateParameter("FZJG",DbType.String, _jcsjk_QY_ZHXXMDL.FZJG));
			p.Add(db.CreateParameter("FZRQ",DbType.DateTime, _jcsjk_QY_ZHXXMDL.FZRQ));
			p.Add(db.CreateParameter("ZSYXQKS",DbType.DateTime, _jcsjk_QY_ZHXXMDL.ZSYXQKS));
			p.Add(db.CreateParameter("ZSYXQJS",DbType.DateTime, _jcsjk_QY_ZHXXMDL.ZSYXQJS));
			p.Add(db.CreateParameter("YWFW",DbType.String, _jcsjk_QY_ZHXXMDL.YWFW));
			p.Add(db.CreateParameter("BZ",DbType.String, _jcsjk_QY_ZHXXMDL.BZ));
			p.Add(db.CreateParameter("VALID",DbType.Int32, _jcsjk_QY_ZHXXMDL.VALID));
			p.Add(db.CreateParameter("CJR",DbType.String, _jcsjk_QY_ZHXXMDL.CJR));
			p.Add(db.CreateParameter("CJDEPTID",DbType.String, _jcsjk_QY_ZHXXMDL.CJDEPTID));
			p.Add(db.CreateParameter("CJSJ",DbType.DateTime, _jcsjk_QY_ZHXXMDL.CJSJ));
			p.Add(db.CreateParameter("XGR",DbType.String, _jcsjk_QY_ZHXXMDL.XGR));
			p.Add(db.CreateParameter("XGDEPTID",DbType.String, _jcsjk_QY_ZHXXMDL.XGDEPTID));
			p.Add(db.CreateParameter("XGSJ",DbType.DateTime, _jcsjk_QY_ZHXXMDL.XGSJ));
			p.Add(db.CreateParameter("SFDYZS",DbType.String, _jcsjk_QY_ZHXXMDL.SFDYZS));
			p.Add(db.CreateParameter("SJLX",DbType.String, _jcsjk_QY_ZHXXMDL.SJLX));
			p.Add(db.CreateParameter("YXSX",DbType.Int32, _jcsjk_QY_ZHXXMDL.YXSX));
			p.Add(db.CreateParameter("XZDQBM",DbType.String, _jcsjk_QY_ZHXXMDL.XZDQBM));
			p.Add(db.CreateParameter("HYLZGX",DbType.String, _jcsjk_QY_ZHXXMDL.HYLZGX));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="jcsjk_QY_ZHXXID">主键</param>
		/// <returns></returns>
        public static int Delete( Guid ID )
		{
			return Delete(null, ID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="jcsjk_QY_ZHXXID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, Guid ID)
		{
			string sql=@"DELETE FROM dbo.jcsjk_QY_ZHXX WHERE ID = @ID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ID",DbType.Guid,ID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="jcsjk_QY_ZHXXMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(jcsjk_QY_ZHXXMDL _jcsjk_QY_ZHXXMDL)
		{
			return Delete(null,_jcsjk_QY_ZHXXMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="jcsjk_QY_ZHXXMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,jcsjk_QY_ZHXXMDL _jcsjk_QY_ZHXXMDL)
		{
			string sql=@"DELETE FROM dbo.jcsjk_QY_ZHXX WHERE ID = @ID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ID",DbType.Guid,_jcsjk_QY_ZHXXMDL.ID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="jcsjk_QY_ZHXXID">主键</param>
        public static jcsjk_QY_ZHXXMDL GetObject( Guid ID )
		{
			string sql=@"
			SELECT [ID],ZZZSBH,ZZJGDM,QYMC,ZCDZ,XXDZ,JLSJ,ZCZBJ,YYZZZCH,JJLX,ZXZZDJ,ZXZZ,FDDBR,FDDBRZJLX,FDDBRZJHM,FDDBRZW,FDDBRZC,QYFZR,QYFZRZJLX,QYFZRZJHM,QYFZRZW,QYFZRZC,JSFZR,JSFZRZJLX,JSFZRZJHM,JSFZRZW,JSFZRZC,QYZZLX,FZJG,FZRQ,ZSYXQKS,ZSYXQJS,YWFW,BZ,[VALID],CJR,CJDEPTID,CJSJ,XGR,XGDEPTID,XGSJ,SFDYZS,SJLX,YXSX,XZDQBM,HYLZGX
			FROM dbo.jcsjk_QY_ZHXX
			WHERE ID = @ID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ID", DbType.Guid, ID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                jcsjk_QY_ZHXXMDL _jcsjk_QY_ZHXXMDL = null;
                if (reader.Read())
                {
                    _jcsjk_QY_ZHXXMDL = new jcsjk_QY_ZHXXMDL();
					if (reader["ID"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.ID = new Guid(Convert.ToString(reader["ID"]));
					if (reader["ZZZSBH"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.ZZZSBH = Convert.ToString(reader["ZZZSBH"]);
					if (reader["ZZJGDM"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.ZZJGDM = Convert.ToString(reader["ZZJGDM"]);
					if (reader["QYMC"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.QYMC = Convert.ToString(reader["QYMC"]);
					if (reader["ZCDZ"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.ZCDZ = Convert.ToString(reader["ZCDZ"]);
					if (reader["XXDZ"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.XXDZ = Convert.ToString(reader["XXDZ"]);
					if (reader["JLSJ"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.JLSJ = Convert.ToDateTime(reader["JLSJ"]);
					if (reader["ZCZBJ"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.ZCZBJ = Convert.ToDecimal(reader["ZCZBJ"]);
					if (reader["YYZZZCH"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.YYZZZCH = Convert.ToString(reader["YYZZZCH"]);
					if (reader["JJLX"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.JJLX = Convert.ToString(reader["JJLX"]);
					if (reader["ZXZZDJ"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.ZXZZDJ = Convert.ToString(reader["ZXZZDJ"]);
					if (reader["ZXZZ"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.ZXZZ = Convert.ToString(reader["ZXZZ"]);
					if (reader["FDDBR"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.FDDBR = Convert.ToString(reader["FDDBR"]);
					if (reader["FDDBRZJLX"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.FDDBRZJLX = Convert.ToString(reader["FDDBRZJLX"]);
					if (reader["FDDBRZJHM"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.FDDBRZJHM = Convert.ToString(reader["FDDBRZJHM"]);
					if (reader["FDDBRZW"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.FDDBRZW = Convert.ToString(reader["FDDBRZW"]);
					if (reader["FDDBRZC"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.FDDBRZC = Convert.ToString(reader["FDDBRZC"]);
					if (reader["QYFZR"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.QYFZR = Convert.ToString(reader["QYFZR"]);
					if (reader["QYFZRZJLX"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.QYFZRZJLX = Convert.ToString(reader["QYFZRZJLX"]);
					if (reader["QYFZRZJHM"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.QYFZRZJHM = Convert.ToString(reader["QYFZRZJHM"]);
					if (reader["QYFZRZW"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.QYFZRZW = Convert.ToString(reader["QYFZRZW"]);
					if (reader["QYFZRZC"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.QYFZRZC = Convert.ToString(reader["QYFZRZC"]);
					if (reader["JSFZR"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.JSFZR = Convert.ToString(reader["JSFZR"]);
					if (reader["JSFZRZJLX"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.JSFZRZJLX = Convert.ToString(reader["JSFZRZJLX"]);
					if (reader["JSFZRZJHM"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.JSFZRZJHM = Convert.ToString(reader["JSFZRZJHM"]);
					if (reader["JSFZRZW"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.JSFZRZW = Convert.ToString(reader["JSFZRZW"]);
					if (reader["JSFZRZC"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.JSFZRZC = Convert.ToString(reader["JSFZRZC"]);
					if (reader["QYZZLX"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.QYZZLX = Convert.ToString(reader["QYZZLX"]);
					if (reader["FZJG"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.FZJG = Convert.ToString(reader["FZJG"]);
					if (reader["FZRQ"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.FZRQ = Convert.ToDateTime(reader["FZRQ"]);
					if (reader["ZSYXQKS"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.ZSYXQKS = Convert.ToDateTime(reader["ZSYXQKS"]);
					if (reader["ZSYXQJS"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.ZSYXQJS = Convert.ToDateTime(reader["ZSYXQJS"]);
					if (reader["YWFW"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.YWFW = Convert.ToString(reader["YWFW"]);
					if (reader["BZ"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.BZ = Convert.ToString(reader["BZ"]);
					if (reader["VALID"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.VALID = Convert.ToInt32(reader["VALID"]);
					if (reader["CJR"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.CJR = Convert.ToString(reader["CJR"]);
					if (reader["CJDEPTID"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.CJDEPTID = Convert.ToString(reader["CJDEPTID"]);
					if (reader["CJSJ"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
					if (reader["XGR"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.XGR = Convert.ToString(reader["XGR"]);
					if (reader["XGDEPTID"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.XGDEPTID = Convert.ToString(reader["XGDEPTID"]);
					if (reader["XGSJ"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.XGSJ = Convert.ToDateTime(reader["XGSJ"]);
					if (reader["SFDYZS"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.SFDYZS = Convert.ToString(reader["SFDYZS"]);
					if (reader["SJLX"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.SJLX = Convert.ToString(reader["SJLX"]);
					if (reader["YXSX"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.YXSX = Convert.ToInt32(reader["YXSX"]);
                    //if (reader["XZDQBM"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.XZDQBM = Convert.ToString(reader["XZDQBM"]);
                    if (reader["XZDQBM"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.XZDQBM = FormatCity(reader["XZDQBM"].ToString(),(reader["HYLZGX"] != DBNull.Value?reader["HYLZGX"].ToString():""));
                    
					if (reader["HYLZGX"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.HYLZGX = Convert.ToString(reader["HYLZGX"]);
                }
				reader.Close();
                db.Close();
                return _jcsjk_QY_ZHXXMDL;
            }
		}
        /// <summary>
        /// 根据组织机构代码获取第一个企业资质证书信息
        /// 排序顺序：1、资质类型： 1）本地施工企业，2）本地监理企业，3）本地造价咨询企业，4）本地招标代理机构，5）设计施工一体化，6）工程设计，7）工程勘察，8）其他
        ///           2、资质序列： 1）施工总承包，2）专业承包，3）专业分包，4）其他
        ///           3、资质等级： 1）特级，2）壹级，3）贰级，4）叁级，5）甲级，6）乙级，7）其他
        /// 【注意】，企业所属区县取值规则：行政地区【XZDQBM】 和 行业隶属关系【HYLZGX】亮个字段不一致时，如果HYLZGX不为空，取HYLZGX       
        /// </summary>
        /// <param name="jcsjk_QY_ZHXXID">主键</param>
        public static jcsjk_QY_ZHXXMDL GetObjectZZJGDM(string ZZJGDM)
        {
            string sql = @"
			SELECT TOP(1) [ID],ZZZSBH,ZZJGDM,QYMC,ZCDZ,XXDZ,JLSJ,ZCZBJ,YYZZZCH,JJLX,ZXZZDJ,ZXZZ,FDDBR,FDDBRZJLX,FDDBRZJHM,FDDBRZW,FDDBRZC,QYFZR,QYFZRZJLX,QYFZRZJHM,QYFZRZW,QYFZRZC,JSFZR,JSFZRZJLX,JSFZRZJHM,JSFZRZW,JSFZRZC,QYZZLX,FZJG,FZRQ,ZSYXQKS,ZSYXQJS,YWFW,BZ,[VALID],CJR,CJDEPTID,CJSJ,XGR,XGDEPTID,XGSJ,SFDYZS,SJLX,YXSX,XZDQBM,HYLZGX
            ,xl_order =case  
                        when zxzz like '%施工总承包%' then 1 
                        when zxzz like '%专业承包%' then 2 
                        else 3 end
            ,SJLX_order=case  SJLX
                when '本地施工企业' then 1
				when '本地监理企业' then 2
				when '本地造价咨询企业' then 3
				when '本地招标代理机构' then 4
				when '设计施工一体化' then 5
				when '工程设计' then 6
				when '工程勘察' then 7
                when '劳务分包' then 9 
                else 8 end
            ,zxzzdj_order=case  
				when zxzz like '%特级%' then 0 
                 when zxzz like '%壹级%' then 1 
				when zxzz like '%贰级%' then 2
				when zxzz like '%叁级%' then 3
				when zxzz like '%甲级%' then 4
				when zxzz like '%乙级%' then 5			
                else 6 end
			FROM dbo.jcsjk_QY_ZHXX
			WHERE ZZJGDM = @ZZJGDM  AND (SJLX='本地招标代理机构' OR SJLX='本地监理企业' OR SJLX='设计施工一体化' OR SJLX='本地造价咨询企业' OR SJLX='本地施工企业' OR SJLX='新设立企业' OR SJLX='工程勘察' OR SJLX='工程设计') 
            order by SJLX_order,xl_order,zxzzdj_order,ZSYXQJS desc";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ZZJGDM", DbType.String, ZZJGDM));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                jcsjk_QY_ZHXXMDL _jcsjk_QY_ZHXXMDL = null;
                if (reader.Read())
                {
                    _jcsjk_QY_ZHXXMDL = new jcsjk_QY_ZHXXMDL();
                    if (reader["ID"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.ID = new Guid(Convert.ToString(reader["ID"]));
                    if (reader["ZZZSBH"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.ZZZSBH = Convert.ToString(reader["ZZZSBH"]);
                    if (reader["ZZJGDM"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.ZZJGDM = Convert.ToString(reader["ZZJGDM"]);
                    if (reader["QYMC"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.QYMC = Convert.ToString(reader["QYMC"]);
                    if (reader["ZCDZ"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.ZCDZ = Convert.ToString(reader["ZCDZ"]);
                    if (reader["XXDZ"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.XXDZ = Convert.ToString(reader["XXDZ"]);
                    if (reader["JLSJ"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.JLSJ = Convert.ToDateTime(reader["JLSJ"]);
                    if (reader["ZCZBJ"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.ZCZBJ = Convert.ToDecimal(reader["ZCZBJ"]);
                    if (reader["YYZZZCH"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.YYZZZCH = Convert.ToString(reader["YYZZZCH"]);
                    if (reader["JJLX"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.JJLX = Convert.ToString(reader["JJLX"]);
                    if (reader["ZXZZDJ"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.ZXZZDJ = Convert.ToString(reader["ZXZZDJ"]);
                    if (reader["ZXZZ"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.ZXZZ = Convert.ToString(reader["ZXZZ"]);
                    if (reader["FDDBR"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.FDDBR = Convert.ToString(reader["FDDBR"]);
                    if (reader["FDDBRZJLX"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.FDDBRZJLX = Convert.ToString(reader["FDDBRZJLX"]);
                    if (reader["FDDBRZJHM"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.FDDBRZJHM = Convert.ToString(reader["FDDBRZJHM"]);
                    if (reader["FDDBRZW"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.FDDBRZW = Convert.ToString(reader["FDDBRZW"]);
                    if (reader["FDDBRZC"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.FDDBRZC = Convert.ToString(reader["FDDBRZC"]);
                    if (reader["QYFZR"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.QYFZR = Convert.ToString(reader["QYFZR"]);
                    if (reader["QYFZRZJLX"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.QYFZRZJLX = Convert.ToString(reader["QYFZRZJLX"]);
                    if (reader["QYFZRZJHM"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.QYFZRZJHM = Convert.ToString(reader["QYFZRZJHM"]);
                    if (reader["QYFZRZW"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.QYFZRZW = Convert.ToString(reader["QYFZRZW"]);
                    if (reader["QYFZRZC"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.QYFZRZC = Convert.ToString(reader["QYFZRZC"]);
                    if (reader["JSFZR"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.JSFZR = Convert.ToString(reader["JSFZR"]);
                    if (reader["JSFZRZJLX"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.JSFZRZJLX = Convert.ToString(reader["JSFZRZJLX"]);
                    if (reader["JSFZRZJHM"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.JSFZRZJHM = Convert.ToString(reader["JSFZRZJHM"]);
                    if (reader["JSFZRZW"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.JSFZRZW = Convert.ToString(reader["JSFZRZW"]);
                    if (reader["JSFZRZC"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.JSFZRZC = Convert.ToString(reader["JSFZRZC"]);
                    if (reader["QYZZLX"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.QYZZLX = Convert.ToString(reader["QYZZLX"]);
                    if (reader["FZJG"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.FZJG = Convert.ToString(reader["FZJG"]);
                    if (reader["FZRQ"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.FZRQ = Convert.ToDateTime(reader["FZRQ"]);
                    if (reader["ZSYXQKS"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.ZSYXQKS = Convert.ToDateTime(reader["ZSYXQKS"]);
                    if (reader["ZSYXQJS"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.ZSYXQJS = Convert.ToDateTime(reader["ZSYXQJS"]);
                    if (reader["YWFW"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.YWFW = Convert.ToString(reader["YWFW"]);
                    if (reader["BZ"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.BZ = Convert.ToString(reader["BZ"]);
                    if (reader["VALID"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.VALID = Convert.ToInt32(reader["VALID"]);
                    if (reader["CJR"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.CJR = Convert.ToString(reader["CJR"]);
                    if (reader["CJDEPTID"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.CJDEPTID = Convert.ToString(reader["CJDEPTID"]);
                    if (reader["CJSJ"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
                    if (reader["XGR"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.XGR = Convert.ToString(reader["XGR"]);
                    if (reader["XGDEPTID"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.XGDEPTID = Convert.ToString(reader["XGDEPTID"]);
                    if (reader["XGSJ"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.XGSJ = Convert.ToDateTime(reader["XGSJ"]);
                    if (reader["SFDYZS"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.SFDYZS = Convert.ToString(reader["SFDYZS"]);
                    if (reader["SJLX"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.SJLX = Convert.ToString(reader["SJLX"]);
                    if (reader["YXSX"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.YXSX = Convert.ToInt32(reader["YXSX"]);
                    //if (reader["XZDQBM"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.XZDQBM = Convert.ToString(reader["XZDQBM"]);
                    if (reader["XZDQBM"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.XZDQBM = FormatCity(reader["XZDQBM"].ToString(), (reader["HYLZGX"] != DBNull.Value ? reader["HYLZGX"].ToString() : ""));
                    if (reader["HYLZGX"] != DBNull.Value) _jcsjk_QY_ZHXXMDL.HYLZGX = Convert.ToString(reader["HYLZGX"]);
                }
                reader.Close();
                db.Close();
                return _jcsjk_QY_ZHXXMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.jcsjk_QY_ZHXX", "*", filterWhereString, orderBy == "" ? " ID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.jcsjk_QY_ZHXX", filterWhereString);
        }
        
        #region 自定义方法

        ///// <summary>
        ///// 获取招标代理企业(已取消该资质)
        ///// </summary>
        ///// <param name="Credit_Code">统一社会信用代码</param>
        ///// <returns></returns>
        //public static DataTable GetListZhaoBiaoDaiLi(string Credit_Code)
        //{
        //    return CommonDAL.GetDataTable(string.Format("select * from  [dbo].[jcsjk_Ten_TBTenderCorpBasicinfo] where CreditCode like '%{0}%'", Credit_Code));
        //}

        ///// <summary>
        ///// 获取招标代理企业(已取消该资质)
        ///// </summary>
        ///// <param name="Credit_Code">统一社会信用代码</param>
        ///// <returns></returns>
        //public static int SelectCountZhaoBiaoDaiLi(string Credit_Code)
        //{
        //     return CommonDAL.SelectRowCount(" [dbo].[jcsjk_Ten_TBTenderCorpBasicinfo]", string.Format(" and CreditCode like '%{0}%'",Credit_Code));
        //}

        /// <summary>
        /// 根据工商注册地址区县和隶属关系返回格式化后的区县
        /// </summary>
        /// <param name="XZDQBM">商注册地址中的区县</param>
        /// <param name="HYLZGX">行业隶属关系</param>
        /// <returns>格式化后的区县</returns>
        protected static string FormatCity(string XZDQBM, string HYLZGX)
        {
            if (string.IsNullOrEmpty(HYLZGX) == true
            || XZDQBM == HYLZGX)
            {
                return XZDQBM;
            }

            if ("昌平区,丰台区,海淀区,东城区,怀柔区,密云区,朝阳区,顺义区,西城区,延庆区,通州区,石景山区,平谷区,房山区,门头沟区,亦庄,大兴区".Contains(HYLZGX) == true)
            {
                return HYLZGX;
            }

            if (HYLZGX.Contains("开发区") == true)
            {
                return "亦庄";
            }
            return XZDQBM;
        }
        #endregion
    }
}
