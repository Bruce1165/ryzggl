using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--ApplyReplaceDAL(填写类描述)
	/// </summary>
    public class ApplyReplaceDAL
    {
        public ApplyReplaceDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="ApplyReplaceMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(ApplyReplaceMDL _ApplyReplaceMDL)
		{
		    return Insert(null,_ApplyReplaceMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ApplyReplaceMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,ApplyReplaceMDL _ApplyReplaceMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.ApplyReplace(ApplyID,PSN_MobilePhone,PSN_Email,RegisterNo,RegisterCertificateNo,DisnableDate,ValidCode,LinkMan,ENT_Telephone,ENT_Correspondence,ENT_Postcode,PSN_RegisteProfession1,PSN_CertificateValidity1,PSN_RegisteProfession2,PSN_CertificateValidity2,PSN_RegisteProfession3,PSN_CertificateValidity3,PSN_CertificateValidity4,PSN_RegisteProfession4,ReplaceReason,ReplaceType)
			VALUES (@ApplyID,@PSN_MobilePhone,@PSN_Email,@RegisterNo,@RegisterCertificateNo,@DisnableDate,@ValidCode,@LinkMan,@ENT_Telephone,@ENT_Correspondence,@ENT_Postcode,@PSN_RegisteProfession1,@PSN_CertificateValidity1,@PSN_RegisteProfession2,@PSN_CertificateValidity2,@PSN_RegisteProfession3,@PSN_CertificateValidity3,@PSN_CertificateValidity4,@PSN_RegisteProfession4,@ReplaceReason,@ReplaceType)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ApplyID",DbType.String, _ApplyReplaceMDL.ApplyID));
			p.Add(db.CreateParameter("PSN_MobilePhone",DbType.String, _ApplyReplaceMDL.PSN_MobilePhone));
			p.Add(db.CreateParameter("PSN_Email",DbType.String, _ApplyReplaceMDL.PSN_Email));
			p.Add(db.CreateParameter("RegisterNo",DbType.String, _ApplyReplaceMDL.RegisterNo));
			p.Add(db.CreateParameter("RegisterCertificateNo",DbType.String, _ApplyReplaceMDL.RegisterCertificateNo));
			p.Add(db.CreateParameter("DisnableDate",DbType.DateTime, _ApplyReplaceMDL.DisnableDate));
			p.Add(db.CreateParameter("ValidCode",DbType.String, _ApplyReplaceMDL.ValidCode));
			p.Add(db.CreateParameter("LinkMan",DbType.String, _ApplyReplaceMDL.LinkMan));
			p.Add(db.CreateParameter("ENT_Telephone",DbType.String, _ApplyReplaceMDL.ENT_Telephone));
			p.Add(db.CreateParameter("ENT_Correspondence",DbType.String, _ApplyReplaceMDL.ENT_Correspondence));
			p.Add(db.CreateParameter("ENT_Postcode",DbType.String, _ApplyReplaceMDL.ENT_Postcode));
			p.Add(db.CreateParameter("PSN_RegisteProfession1",DbType.String, _ApplyReplaceMDL.PSN_RegisteProfession1));
			p.Add(db.CreateParameter("PSN_CertificateValidity1",DbType.DateTime, _ApplyReplaceMDL.PSN_CertificateValidity1));
			p.Add(db.CreateParameter("PSN_RegisteProfession2",DbType.String, _ApplyReplaceMDL.PSN_RegisteProfession2));
			p.Add(db.CreateParameter("PSN_CertificateValidity2",DbType.DateTime, _ApplyReplaceMDL.PSN_CertificateValidity2));
			p.Add(db.CreateParameter("PSN_RegisteProfession3",DbType.String, _ApplyReplaceMDL.PSN_RegisteProfession3));
			p.Add(db.CreateParameter("PSN_CertificateValidity3",DbType.DateTime, _ApplyReplaceMDL.PSN_CertificateValidity3));
			p.Add(db.CreateParameter("PSN_CertificateValidity4",DbType.DateTime, _ApplyReplaceMDL.PSN_CertificateValidity4));
			p.Add(db.CreateParameter("PSN_RegisteProfession4",DbType.String, _ApplyReplaceMDL.PSN_RegisteProfession4));
			p.Add(db.CreateParameter("ReplaceReason",DbType.String, _ApplyReplaceMDL.ReplaceReason));
			p.Add(db.CreateParameter("ReplaceType",DbType.String, _ApplyReplaceMDL.ReplaceType));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="ApplyReplaceMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(ApplyReplaceMDL _ApplyReplaceMDL)
		{
			return Update(null,_ApplyReplaceMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ApplyReplaceMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,ApplyReplaceMDL _ApplyReplaceMDL)
		{
			string sql = @"
			UPDATE dbo.ApplyReplace
				SET	PSN_MobilePhone = @PSN_MobilePhone,PSN_Email = @PSN_Email,RegisterNo = @RegisterNo,RegisterCertificateNo = @RegisterCertificateNo,DisnableDate = @DisnableDate,ValidCode = @ValidCode,LinkMan = @LinkMan,ENT_Telephone = @ENT_Telephone,ENT_Correspondence = @ENT_Correspondence,ENT_Postcode = @ENT_Postcode,PSN_RegisteProfession1 = @PSN_RegisteProfession1,PSN_CertificateValidity1 = @PSN_CertificateValidity1,PSN_RegisteProfession2 = @PSN_RegisteProfession2,PSN_CertificateValidity2 = @PSN_CertificateValidity2,PSN_RegisteProfession3 = @PSN_RegisteProfession3,PSN_CertificateValidity3 = @PSN_CertificateValidity3,PSN_CertificateValidity4 = @PSN_CertificateValidity4,PSN_RegisteProfession4 = @PSN_RegisteProfession4,ReplaceReason = @ReplaceReason,ReplaceType = @ReplaceType
			WHERE
				ApplyID = @ApplyID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ApplyID",DbType.String, _ApplyReplaceMDL.ApplyID));
			p.Add(db.CreateParameter("PSN_MobilePhone",DbType.String, _ApplyReplaceMDL.PSN_MobilePhone));
			p.Add(db.CreateParameter("PSN_Email",DbType.String, _ApplyReplaceMDL.PSN_Email));
			p.Add(db.CreateParameter("RegisterNo",DbType.String, _ApplyReplaceMDL.RegisterNo));
			p.Add(db.CreateParameter("RegisterCertificateNo",DbType.String, _ApplyReplaceMDL.RegisterCertificateNo));
			p.Add(db.CreateParameter("DisnableDate",DbType.DateTime, _ApplyReplaceMDL.DisnableDate));
			p.Add(db.CreateParameter("ValidCode",DbType.String, _ApplyReplaceMDL.ValidCode));
			p.Add(db.CreateParameter("LinkMan",DbType.String, _ApplyReplaceMDL.LinkMan));
			p.Add(db.CreateParameter("ENT_Telephone",DbType.String, _ApplyReplaceMDL.ENT_Telephone));
			p.Add(db.CreateParameter("ENT_Correspondence",DbType.String, _ApplyReplaceMDL.ENT_Correspondence));
			p.Add(db.CreateParameter("ENT_Postcode",DbType.String, _ApplyReplaceMDL.ENT_Postcode));
			p.Add(db.CreateParameter("PSN_RegisteProfession1",DbType.String, _ApplyReplaceMDL.PSN_RegisteProfession1));
			p.Add(db.CreateParameter("PSN_CertificateValidity1",DbType.DateTime, _ApplyReplaceMDL.PSN_CertificateValidity1));
			p.Add(db.CreateParameter("PSN_RegisteProfession2",DbType.String, _ApplyReplaceMDL.PSN_RegisteProfession2));
			p.Add(db.CreateParameter("PSN_CertificateValidity2",DbType.DateTime, _ApplyReplaceMDL.PSN_CertificateValidity2));
			p.Add(db.CreateParameter("PSN_RegisteProfession3",DbType.String, _ApplyReplaceMDL.PSN_RegisteProfession3));
			p.Add(db.CreateParameter("PSN_CertificateValidity3",DbType.DateTime, _ApplyReplaceMDL.PSN_CertificateValidity3));
			p.Add(db.CreateParameter("PSN_CertificateValidity4",DbType.DateTime, _ApplyReplaceMDL.PSN_CertificateValidity4));
			p.Add(db.CreateParameter("PSN_RegisteProfession4",DbType.String, _ApplyReplaceMDL.PSN_RegisteProfession4));
			p.Add(db.CreateParameter("ReplaceReason",DbType.String, _ApplyReplaceMDL.ReplaceReason));
			p.Add(db.CreateParameter("ReplaceType",DbType.String, _ApplyReplaceMDL.ReplaceType));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="ApplyReplaceID">主键</param>
		/// <returns></returns>
        public static int Delete( string ApplyID )
		{
			return Delete(null, ApplyID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="ApplyReplaceID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, string ApplyID)
		{
			string sql=@"DELETE FROM dbo.ApplyReplace WHERE ApplyID = @ApplyID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ApplyID",DbType.String,ApplyID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="ApplyReplaceMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(ApplyReplaceMDL _ApplyReplaceMDL)
		{
			return Delete(null,_ApplyReplaceMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ApplyReplaceMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,ApplyReplaceMDL _ApplyReplaceMDL)
		{
			string sql=@"DELETE FROM dbo.ApplyReplace WHERE ApplyID = @ApplyID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ApplyID",DbType.String,_ApplyReplaceMDL.ApplyID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ApplyReplaceID">主键</param>
        public static ApplyReplaceMDL GetObject( string ApplyID )
		{
			string sql=@"
			SELECT ApplyID,PSN_MobilePhone,PSN_Email,RegisterNo,RegisterCertificateNo,DisnableDate,ValidCode,LinkMan,ENT_Telephone,ENT_Correspondence,ENT_Postcode,PSN_RegisteProfession1,PSN_CertificateValidity1,PSN_RegisteProfession2,PSN_CertificateValidity2,PSN_RegisteProfession3,PSN_CertificateValidity3,PSN_CertificateValidity4,PSN_RegisteProfession4,ReplaceReason,ReplaceType
			FROM dbo.ApplyReplace
			WHERE ApplyID = @ApplyID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, ApplyID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                ApplyReplaceMDL _ApplyReplaceMDL = null;
                if (reader.Read())
                {
                    _ApplyReplaceMDL = new ApplyReplaceMDL();
					if (reader["ApplyID"] != DBNull.Value) _ApplyReplaceMDL.ApplyID = Convert.ToString(reader["ApplyID"]);
					if (reader["PSN_MobilePhone"] != DBNull.Value) _ApplyReplaceMDL.PSN_MobilePhone = Convert.ToString(reader["PSN_MobilePhone"]);
					if (reader["PSN_Email"] != DBNull.Value) _ApplyReplaceMDL.PSN_Email = Convert.ToString(reader["PSN_Email"]);
					if (reader["RegisterNo"] != DBNull.Value) _ApplyReplaceMDL.RegisterNo = Convert.ToString(reader["RegisterNo"]);
					if (reader["RegisterCertificateNo"] != DBNull.Value) _ApplyReplaceMDL.RegisterCertificateNo = Convert.ToString(reader["RegisterCertificateNo"]);
					if (reader["DisnableDate"] != DBNull.Value) _ApplyReplaceMDL.DisnableDate = Convert.ToDateTime(reader["DisnableDate"]);
					if (reader["ValidCode"] != DBNull.Value) _ApplyReplaceMDL.ValidCode = Convert.ToString(reader["ValidCode"]);
					if (reader["LinkMan"] != DBNull.Value) _ApplyReplaceMDL.LinkMan = Convert.ToString(reader["LinkMan"]);
					if (reader["ENT_Telephone"] != DBNull.Value) _ApplyReplaceMDL.ENT_Telephone = Convert.ToString(reader["ENT_Telephone"]);
					if (reader["ENT_Correspondence"] != DBNull.Value) _ApplyReplaceMDL.ENT_Correspondence = Convert.ToString(reader["ENT_Correspondence"]);
					if (reader["ENT_Postcode"] != DBNull.Value) _ApplyReplaceMDL.ENT_Postcode = Convert.ToString(reader["ENT_Postcode"]);
					if (reader["PSN_RegisteProfession1"] != DBNull.Value) _ApplyReplaceMDL.PSN_RegisteProfession1 = Convert.ToString(reader["PSN_RegisteProfession1"]);
					if (reader["PSN_CertificateValidity1"] != DBNull.Value) _ApplyReplaceMDL.PSN_CertificateValidity1 = Convert.ToDateTime(reader["PSN_CertificateValidity1"]);
					if (reader["PSN_RegisteProfession2"] != DBNull.Value) _ApplyReplaceMDL.PSN_RegisteProfession2 = Convert.ToString(reader["PSN_RegisteProfession2"]);
					if (reader["PSN_CertificateValidity2"] != DBNull.Value) _ApplyReplaceMDL.PSN_CertificateValidity2 = Convert.ToDateTime(reader["PSN_CertificateValidity2"]);
					if (reader["PSN_RegisteProfession3"] != DBNull.Value) _ApplyReplaceMDL.PSN_RegisteProfession3 = Convert.ToString(reader["PSN_RegisteProfession3"]);
					if (reader["PSN_CertificateValidity3"] != DBNull.Value) _ApplyReplaceMDL.PSN_CertificateValidity3 = Convert.ToDateTime(reader["PSN_CertificateValidity3"]);
					if (reader["PSN_CertificateValidity4"] != DBNull.Value) _ApplyReplaceMDL.PSN_CertificateValidity4 = Convert.ToDateTime(reader["PSN_CertificateValidity4"]);
					if (reader["PSN_RegisteProfession4"] != DBNull.Value) _ApplyReplaceMDL.PSN_RegisteProfession4 = Convert.ToString(reader["PSN_RegisteProfession4"]);
					if (reader["ReplaceReason"] != DBNull.Value) _ApplyReplaceMDL.ReplaceReason = Convert.ToString(reader["ReplaceReason"]);
					if (reader["ReplaceType"] != DBNull.Value) _ApplyReplaceMDL.ReplaceType = Convert.ToString(reader["ReplaceType"]);
                }
				reader.Close();
                db.Close();
                return _ApplyReplaceMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.ApplyReplace", "*", filterWhereString, orderBy == "" ? " ApplyID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.ApplyReplace", filterWhereString);
        }
        
        #region 自定义方法
        
        #endregion
    }
}
