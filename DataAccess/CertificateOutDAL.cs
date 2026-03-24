using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--CertificateOutDAL(填写类描述)
	/// </summary>
    public class CertificateOutDAL
    {
        public CertificateOutDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_CertificateOutMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(CertificateOutMDL _CertificateOutMDL)
		{
		    return Insert(null,_CertificateOutMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_CertificateOutMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,CertificateOutMDL _CertificateOutMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.CertificateOut(out_certNum,out_provinceNum,out_name,out_gender,out_identityCard,out_identityCardType,out_birthDate,out_categoryCode,out_companyName,out_creditCode,out_issuAuth,out_certState,out_issuedDate,out_effectiveDate,out_expiringDate,out_zzeCertID,cjsj)
			VALUES (@out_certNum,@out_provinceNum,@out_name,@out_gender,@out_identityCard,@out_identityCardType,@out_birthDate,@out_categoryCode,@out_companyName,@out_creditCode,@out_issuAuth,@out_certState,@out_issuedDate,@out_effectiveDate,@out_expiringDate,@out_zzeCertID,@cjsj)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("out_certNum",DbType.String, _CertificateOutMDL.out_certNum));
			p.Add(db.CreateParameter("out_provinceNum",DbType.String, _CertificateOutMDL.out_provinceNum));
			p.Add(db.CreateParameter("out_name",DbType.String, _CertificateOutMDL.out_name));
			p.Add(db.CreateParameter("out_gender",DbType.String, _CertificateOutMDL.out_gender));
			p.Add(db.CreateParameter("out_identityCard",DbType.String, _CertificateOutMDL.out_identityCard));
			p.Add(db.CreateParameter("out_identityCardType",DbType.String, _CertificateOutMDL.out_identityCardType));
			p.Add(db.CreateParameter("out_birthDate",DbType.DateTime, _CertificateOutMDL.out_birthDate));
			p.Add(db.CreateParameter("out_categoryCode",DbType.String, _CertificateOutMDL.out_categoryCode));
			p.Add(db.CreateParameter("out_companyName",DbType.String, _CertificateOutMDL.out_companyName));
			p.Add(db.CreateParameter("out_creditCode",DbType.String, _CertificateOutMDL.out_creditCode));
			p.Add(db.CreateParameter("out_issuAuth",DbType.String, _CertificateOutMDL.out_issuAuth));
			p.Add(db.CreateParameter("out_certState",DbType.String, _CertificateOutMDL.out_certState));
			p.Add(db.CreateParameter("out_issuedDate",DbType.DateTime, _CertificateOutMDL.out_issuedDate));
			p.Add(db.CreateParameter("out_effectiveDate",DbType.DateTime, _CertificateOutMDL.out_effectiveDate));
			p.Add(db.CreateParameter("out_expiringDate",DbType.DateTime, _CertificateOutMDL.out_expiringDate));
			p.Add(db.CreateParameter("out_zzeCertID",DbType.String, _CertificateOutMDL.out_zzeCertID));
			p.Add(db.CreateParameter("cjsj",DbType.DateTime, _CertificateOutMDL.cjsj));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_CertificateOutMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(CertificateOutMDL _CertificateOutMDL)
		{
			return Update(null,_CertificateOutMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_CertificateOutMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,CertificateOutMDL _CertificateOutMDL)
		{
			string sql = @"
			UPDATE dbo.CertificateOut
				SET	out_provinceNum = @out_provinceNum,out_name = @out_name,out_gender = @out_gender,out_identityCard = @out_identityCard,out_identityCardType = @out_identityCardType,out_birthDate = @out_birthDate,out_categoryCode = @out_categoryCode,out_companyName = @out_companyName,out_creditCode = @out_creditCode,out_issuAuth = @out_issuAuth,out_certState = @out_certState,out_issuedDate = @out_issuedDate,out_effectiveDate = @out_effectiveDate,out_expiringDate = @out_expiringDate,out_zzeCertID = @out_zzeCertID,cjsj = @cjsj
			WHERE
				out_certNum = @out_certNum";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("out_certNum",DbType.String, _CertificateOutMDL.out_certNum));
			p.Add(db.CreateParameter("out_provinceNum",DbType.String, _CertificateOutMDL.out_provinceNum));
			p.Add(db.CreateParameter("out_name",DbType.String, _CertificateOutMDL.out_name));
			p.Add(db.CreateParameter("out_gender",DbType.String, _CertificateOutMDL.out_gender));
			p.Add(db.CreateParameter("out_identityCard",DbType.String, _CertificateOutMDL.out_identityCard));
			p.Add(db.CreateParameter("out_identityCardType",DbType.String, _CertificateOutMDL.out_identityCardType));
			p.Add(db.CreateParameter("out_birthDate",DbType.DateTime, _CertificateOutMDL.out_birthDate));
			p.Add(db.CreateParameter("out_categoryCode",DbType.String, _CertificateOutMDL.out_categoryCode));
			p.Add(db.CreateParameter("out_companyName",DbType.String, _CertificateOutMDL.out_companyName));
			p.Add(db.CreateParameter("out_creditCode",DbType.String, _CertificateOutMDL.out_creditCode));
			p.Add(db.CreateParameter("out_issuAuth",DbType.String, _CertificateOutMDL.out_issuAuth));
			p.Add(db.CreateParameter("out_certState",DbType.String, _CertificateOutMDL.out_certState));
			p.Add(db.CreateParameter("out_issuedDate",DbType.DateTime, _CertificateOutMDL.out_issuedDate));
			p.Add(db.CreateParameter("out_effectiveDate",DbType.DateTime, _CertificateOutMDL.out_effectiveDate));
			p.Add(db.CreateParameter("out_expiringDate",DbType.DateTime, _CertificateOutMDL.out_expiringDate));
			p.Add(db.CreateParameter("out_zzeCertID",DbType.String, _CertificateOutMDL.out_zzeCertID));
			p.Add(db.CreateParameter("cjsj",DbType.DateTime, _CertificateOutMDL.cjsj));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="CertificateOutID">主键</param>
		/// <returns></returns>
        public static int Delete( string out_certNum )
		{
			return Delete(null, out_certNum);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="CertificateOutID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, string out_certNum)
		{
			string sql=@"DELETE FROM dbo.CertificateOut WHERE out_certNum = @out_certNum";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("out_certNum",DbType.String,out_certNum));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="_CertificateOutMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(CertificateOutMDL _CertificateOutMDL)
		{
			return Delete(null,_CertificateOutMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_CertificateOutMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,CertificateOutMDL _CertificateOutMDL)
		{
			string sql=@"DELETE FROM dbo.CertificateOut WHERE out_certNum = @out_certNum";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("out_certNum",DbType.String,_CertificateOutMDL.out_certNum));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="CertificateOutID">主键</param>
        public static CertificateOutMDL GetObject( string out_certNum )
		{
			string sql=@"
			SELECT out_certNum,out_provinceNum,out_name,out_gender,out_identityCard,out_identityCardType,out_birthDate,out_categoryCode,out_companyName,out_creditCode,out_issuAuth,out_certState,out_issuedDate,out_effectiveDate,out_expiringDate,out_zzeCertID,cjsj
			FROM dbo.CertificateOut
			WHERE out_certNum = @out_certNum";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("out_certNum",DbType.String,out_certNum));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                CertificateOutMDL _CertificateOutMDL = null;
                if (reader.Read())
                {
                    _CertificateOutMDL = new CertificateOutMDL();
					if (reader["out_certNum"] != DBNull.Value) _CertificateOutMDL.out_certNum = Convert.ToString(reader["out_certNum"]);
					if (reader["out_provinceNum"] != DBNull.Value) _CertificateOutMDL.out_provinceNum = Convert.ToString(reader["out_provinceNum"]);
					if (reader["out_name"] != DBNull.Value) _CertificateOutMDL.out_name = Convert.ToString(reader["out_name"]);
					if (reader["out_gender"] != DBNull.Value) _CertificateOutMDL.out_gender = Convert.ToString(reader["out_gender"]);
					if (reader["out_identityCard"] != DBNull.Value) _CertificateOutMDL.out_identityCard = Convert.ToString(reader["out_identityCard"]);
					if (reader["out_identityCardType"] != DBNull.Value) _CertificateOutMDL.out_identityCardType = Convert.ToString(reader["out_identityCardType"]);
					if (reader["out_birthDate"] != DBNull.Value) _CertificateOutMDL.out_birthDate = Convert.ToDateTime(reader["out_birthDate"]);
					if (reader["out_categoryCode"] != DBNull.Value) _CertificateOutMDL.out_categoryCode = Convert.ToString(reader["out_categoryCode"]);
					if (reader["out_companyName"] != DBNull.Value) _CertificateOutMDL.out_companyName = Convert.ToString(reader["out_companyName"]);
					if (reader["out_creditCode"] != DBNull.Value) _CertificateOutMDL.out_creditCode = Convert.ToString(reader["out_creditCode"]);
					if (reader["out_issuAuth"] != DBNull.Value) _CertificateOutMDL.out_issuAuth = Convert.ToString(reader["out_issuAuth"]);
					if (reader["out_certState"] != DBNull.Value) _CertificateOutMDL.out_certState = Convert.ToString(reader["out_certState"]);
					if (reader["out_issuedDate"] != DBNull.Value) _CertificateOutMDL.out_issuedDate = Convert.ToDateTime(reader["out_issuedDate"]);
					if (reader["out_effectiveDate"] != DBNull.Value) _CertificateOutMDL.out_effectiveDate = Convert.ToDateTime(reader["out_effectiveDate"]);
					if (reader["out_expiringDate"] != DBNull.Value) _CertificateOutMDL.out_expiringDate = Convert.ToDateTime(reader["out_expiringDate"]);
					if (reader["out_zzeCertID"] != DBNull.Value) _CertificateOutMDL.out_zzeCertID = Convert.ToString(reader["out_zzeCertID"]);
					if (reader["cjsj"] != DBNull.Value) _CertificateOutMDL.cjsj = Convert.ToDateTime(reader["cjsj"]);
                }
				reader.Close();
                db.Close();
                return _CertificateOutMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.CertificateOut", "*", filterWhereString, orderBy == "" ? " out_certNum" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.CertificateOut", filterWhereString);
        }
        
        #region 自定义方法
        
        #endregion
    }
}
