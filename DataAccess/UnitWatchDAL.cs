using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--UnitWatchDAL(填写类描述)
	/// </summary>
    public class UnitWatchDAL
    {
        public UnitWatchDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="UnitWatchMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(UnitWatchMDL _UnitWatchMDL)
		{
		    return Insert(null,_UnitWatchMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="UnitWatchMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,UnitWatchMDL _UnitWatchMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.UnitWatch(UnitName,CreditCode,UnitCertCode,ENT_City,FaRen,FRPhone,Address,CJSJ,ValidEnd)
			VALUES (@UnitName,@CreditCode,@UnitCertCode,@ENT_City,@FaRen,@FRPhone,@Address,@CJSJ,@ValidEnd)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("UnitName",DbType.String, _UnitWatchMDL.UnitName));
			p.Add(db.CreateParameter("CreditCode",DbType.String, _UnitWatchMDL.CreditCode));
			p.Add(db.CreateParameter("UnitCertCode",DbType.String, _UnitWatchMDL.UnitCertCode));
			p.Add(db.CreateParameter("ENT_City",DbType.String, _UnitWatchMDL.ENT_City));
			p.Add(db.CreateParameter("FaRen",DbType.String, _UnitWatchMDL.FaRen));
			p.Add(db.CreateParameter("FRPhone",DbType.String, _UnitWatchMDL.FRPhone));
			p.Add(db.CreateParameter("Address",DbType.String, _UnitWatchMDL.Address));
			p.Add(db.CreateParameter("CJSJ",DbType.DateTime, _UnitWatchMDL.CJSJ));
			p.Add(db.CreateParameter("ValidEnd",DbType.DateTime, _UnitWatchMDL.ValidEnd));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="UnitWatchMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(UnitWatchMDL _UnitWatchMDL)
		{
			return Update(null,_UnitWatchMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="UnitWatchMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,UnitWatchMDL _UnitWatchMDL)
		{
			string sql = @"
			UPDATE dbo.UnitWatch
				SET	UnitName = @UnitName,UnitCertCode = @UnitCertCode,ENT_City = @ENT_City,FaRen = @FaRen,FRPhone = @FRPhone,Address = @Address,CJSJ = @CJSJ,ValidEnd = @ValidEnd,CreditCode=@newCreditCode
			WHERE
				CreditCode = @CreditCode";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("UnitName",DbType.String, _UnitWatchMDL.UnitName));
			p.Add(db.CreateParameter("CreditCode",DbType.String, _UnitWatchMDL.CreditCode));
            p.Add(db.CreateParameter("newCreditCode", DbType.String, _UnitWatchMDL.newCreditCode));
			p.Add(db.CreateParameter("UnitCertCode",DbType.String, _UnitWatchMDL.UnitCertCode));
			p.Add(db.CreateParameter("ENT_City",DbType.String, _UnitWatchMDL.ENT_City));
			p.Add(db.CreateParameter("FaRen",DbType.String, _UnitWatchMDL.FaRen));
			p.Add(db.CreateParameter("FRPhone",DbType.String, _UnitWatchMDL.FRPhone));
			p.Add(db.CreateParameter("Address",DbType.String, _UnitWatchMDL.Address));
			p.Add(db.CreateParameter("CJSJ",DbType.DateTime, _UnitWatchMDL.CJSJ));
			p.Add(db.CreateParameter("ValidEnd",DbType.DateTime, _UnitWatchMDL.ValidEnd));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="UnitWatchID">主键</param>
		/// <returns></returns>
        public static int Delete( string CreditCode )
		{
			return Delete(null, CreditCode);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="UnitWatchID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, string CreditCode)
		{
			string sql=@"DELETE FROM dbo.UnitWatch WHERE CreditCode = @CreditCode";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("CreditCode",DbType.String,CreditCode));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="UnitWatchMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(UnitWatchMDL _UnitWatchMDL)
		{
			return Delete(null,_UnitWatchMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="UnitWatchMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,UnitWatchMDL _UnitWatchMDL)
		{
			string sql=@"DELETE FROM dbo.UnitWatch WHERE CreditCode = @CreditCode";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("CreditCode",DbType.String,_UnitWatchMDL.CreditCode));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="UnitWatchID">主键</param>
        public static UnitWatchMDL GetObject( string CreditCode )
		{
			string sql=@"
			SELECT UnitName,CreditCode,UnitCertCode,ENT_City,FaRen,FRPhone,Address,CJSJ,ValidEnd
			FROM dbo.UnitWatch
			WHERE CreditCode = @CreditCode";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("CreditCode", DbType.String, CreditCode));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                UnitWatchMDL _UnitWatchMDL = null;
                if (reader.Read())
                {
                    _UnitWatchMDL = new UnitWatchMDL();
					if (reader["UnitName"] != DBNull.Value) _UnitWatchMDL.UnitName = Convert.ToString(reader["UnitName"]);
					if (reader["CreditCode"] != DBNull.Value) _UnitWatchMDL.CreditCode = Convert.ToString(reader["CreditCode"]);
					if (reader["UnitCertCode"] != DBNull.Value) _UnitWatchMDL.UnitCertCode = Convert.ToString(reader["UnitCertCode"]);
					if (reader["ENT_City"] != DBNull.Value) _UnitWatchMDL.ENT_City = Convert.ToString(reader["ENT_City"]);
					if (reader["FaRen"] != DBNull.Value) _UnitWatchMDL.FaRen = Convert.ToString(reader["FaRen"]);
					if (reader["FRPhone"] != DBNull.Value) _UnitWatchMDL.FRPhone = Convert.ToString(reader["FRPhone"]);
					if (reader["Address"] != DBNull.Value) _UnitWatchMDL.Address = Convert.ToString(reader["Address"]);
					if (reader["CJSJ"] != DBNull.Value) _UnitWatchMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
					if (reader["ValidEnd"] != DBNull.Value) _UnitWatchMDL.ValidEnd = Convert.ToDateTime(reader["ValidEnd"]);
                }
				reader.Close();
                db.Close();
                return _UnitWatchMDL;
            }
		}
        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="UnitCode">组织机构代码</param>
        public static UnitWatchMDL GetObjectByUnitCode(string UnitCode)
        {
            string sql = @"
			SELECT UnitName,CreditCode,UnitCertCode,ENT_City,FaRen,FRPhone,Address,CJSJ,ValidEnd
			FROM dbo.UnitWatch
			WHERE  CreditCode like '________{0}_'";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
       
            using (SqlDataReader reader = db.GetDataReader(string.Format(sql,UnitCode)))
            {
                UnitWatchMDL _UnitWatchMDL = null;
                if (reader.Read())
                {
                    _UnitWatchMDL = new UnitWatchMDL();
                    if (reader["UnitName"] != DBNull.Value) _UnitWatchMDL.UnitName = Convert.ToString(reader["UnitName"]);
                    if (reader["CreditCode"] != DBNull.Value) _UnitWatchMDL.CreditCode = Convert.ToString(reader["CreditCode"]);
                    if (reader["UnitCertCode"] != DBNull.Value) _UnitWatchMDL.UnitCertCode = Convert.ToString(reader["UnitCertCode"]);
                    if (reader["ENT_City"] != DBNull.Value) _UnitWatchMDL.ENT_City = Convert.ToString(reader["ENT_City"]);
                    if (reader["FaRen"] != DBNull.Value) _UnitWatchMDL.FaRen = Convert.ToString(reader["FaRen"]);
                    if (reader["FRPhone"] != DBNull.Value) _UnitWatchMDL.FRPhone = Convert.ToString(reader["FRPhone"]);
                    if (reader["Address"] != DBNull.Value) _UnitWatchMDL.Address = Convert.ToString(reader["Address"]);
                    if (reader["CJSJ"] != DBNull.Value) _UnitWatchMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
                    if (reader["ValidEnd"] != DBNull.Value) _UnitWatchMDL.ValidEnd = Convert.ToDateTime(reader["ValidEnd"]);
                }
                reader.Close();
                db.Close();
                return _UnitWatchMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.UnitWatch", "*", filterWhereString, orderBy == "" ? " CreditCode" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.UnitWatch", filterWhereString);
        }
        
        #region 自定义方法
        
        #endregion
    }
}
