using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--ApplyAddItemDAL(填写类描述)
	/// </summary>
    public class ApplyAddItemDAL
    {
        public ApplyAddItemDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="ApplyAddItemMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(ApplyAddItemMDL _ApplyAddItemMDL)
		{
		    return Insert(null,_ApplyAddItemMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ApplyAddItemMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,ApplyAddItemMDL _ApplyAddItemMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.ApplyAddItem(ApplyID,PSN_MobilePhone,PSN_Email,ENT_Telephone,PSN_RegisterCertificateNo,PSN_RegisteProfession1,PSN_CertificateValidity1,PSN_RegisteProfession2,PSN_CertificateValidity2,PSN_RegisteProfession3,PSN_CertificateValidity3,AddItem1,ExamCode1,ExamDate1,BiXiu1,XuanXiu1,AddItem2,ExamCode2,ExamDate2,BiXiu2,XuanXiu2,Nation)
			VALUES (@ApplyID,@PSN_MobilePhone,@PSN_Email,@ENT_Telephone,@PSN_RegisterCertificateNo,@PSN_RegisteProfession1,@PSN_CertificateValidity1,@PSN_RegisteProfession2,@PSN_CertificateValidity2,@PSN_RegisteProfession3,@PSN_CertificateValidity3,@AddItem1,@ExamCode1,@ExamDate1,@BiXiu1,@XuanXiu1,@AddItem2,@ExamCode2,@ExamDate2,@BiXiu2,@XuanXiu2,@Nation)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ApplyID",DbType.String, _ApplyAddItemMDL.ApplyID));
			p.Add(db.CreateParameter("PSN_MobilePhone",DbType.String, _ApplyAddItemMDL.PSN_MobilePhone));
			p.Add(db.CreateParameter("PSN_Email",DbType.String, _ApplyAddItemMDL.PSN_Email));
			p.Add(db.CreateParameter("ENT_Telephone",DbType.String, _ApplyAddItemMDL.ENT_Telephone));
			p.Add(db.CreateParameter("PSN_RegisterCertificateNo",DbType.String, _ApplyAddItemMDL.PSN_RegisterCertificateNo));
			p.Add(db.CreateParameter("PSN_RegisteProfession1",DbType.String, _ApplyAddItemMDL.PSN_RegisteProfession1));
			p.Add(db.CreateParameter("PSN_CertificateValidity1",DbType.DateTime, _ApplyAddItemMDL.PSN_CertificateValidity1));
			p.Add(db.CreateParameter("PSN_RegisteProfession2",DbType.String, _ApplyAddItemMDL.PSN_RegisteProfession2));
			p.Add(db.CreateParameter("PSN_CertificateValidity2",DbType.DateTime, _ApplyAddItemMDL.PSN_CertificateValidity2));
			p.Add(db.CreateParameter("PSN_RegisteProfession3",DbType.String, _ApplyAddItemMDL.PSN_RegisteProfession3));
			p.Add(db.CreateParameter("PSN_CertificateValidity3",DbType.DateTime, _ApplyAddItemMDL.PSN_CertificateValidity3));
			p.Add(db.CreateParameter("AddItem1",DbType.String, _ApplyAddItemMDL.AddItem1));
			p.Add(db.CreateParameter("ExamCode1",DbType.String, _ApplyAddItemMDL.ExamCode1));
			p.Add(db.CreateParameter("ExamDate1",DbType.DateTime, _ApplyAddItemMDL.ExamDate1));
			p.Add(db.CreateParameter("BiXiu1",DbType.Int32, _ApplyAddItemMDL.BiXiu1));
			p.Add(db.CreateParameter("XuanXiu1",DbType.Int32, _ApplyAddItemMDL.XuanXiu1));
			p.Add(db.CreateParameter("AddItem2",DbType.String, _ApplyAddItemMDL.AddItem2));
			p.Add(db.CreateParameter("ExamCode2",DbType.String, _ApplyAddItemMDL.ExamCode2));
			p.Add(db.CreateParameter("ExamDate2",DbType.DateTime, _ApplyAddItemMDL.ExamDate2));
			p.Add(db.CreateParameter("BiXiu2",DbType.Int32, _ApplyAddItemMDL.BiXiu2));
            p.Add(db.CreateParameter("XuanXiu2", DbType.Int32, _ApplyAddItemMDL.XuanXiu2));
            p.Add(db.CreateParameter("Nation", DbType.String, _ApplyAddItemMDL.Nation));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="ApplyAddItemMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(ApplyAddItemMDL _ApplyAddItemMDL)
		{
			return Update(null,_ApplyAddItemMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ApplyAddItemMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,ApplyAddItemMDL _ApplyAddItemMDL)
		{
			string sql = @"
			UPDATE dbo.ApplyAddItem
				SET	PSN_MobilePhone = @PSN_MobilePhone,PSN_Email = @PSN_Email,ENT_Telephone = @ENT_Telephone,PSN_RegisterCertificateNo = @PSN_RegisterCertificateNo,PSN_RegisteProfession1 = @PSN_RegisteProfession1,PSN_CertificateValidity1 = @PSN_CertificateValidity1,PSN_RegisteProfession2 = @PSN_RegisteProfession2,PSN_CertificateValidity2 = @PSN_CertificateValidity2,PSN_RegisteProfession3 = @PSN_RegisteProfession3,PSN_CertificateValidity3 = @PSN_CertificateValidity3,AddItem1 = @AddItem1,ExamCode1 = @ExamCode1,ExamDate1 = @ExamDate1,BiXiu1 = @BiXiu1,XuanXiu1 = @XuanXiu1,AddItem2 = @AddItem2,ExamCode2 = @ExamCode2,ExamDate2 = @ExamDate2,BiXiu2 = @BiXiu2,XuanXiu2 = @XuanXiu2,Nation=@Nation
			WHERE
				ApplyID = @ApplyID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ApplyID",DbType.String, _ApplyAddItemMDL.ApplyID));
			p.Add(db.CreateParameter("PSN_MobilePhone",DbType.String, _ApplyAddItemMDL.PSN_MobilePhone));
			p.Add(db.CreateParameter("PSN_Email",DbType.String, _ApplyAddItemMDL.PSN_Email));
			p.Add(db.CreateParameter("ENT_Telephone",DbType.String, _ApplyAddItemMDL.ENT_Telephone));
			p.Add(db.CreateParameter("PSN_RegisterCertificateNo",DbType.String, _ApplyAddItemMDL.PSN_RegisterCertificateNo));
			p.Add(db.CreateParameter("PSN_RegisteProfession1",DbType.String, _ApplyAddItemMDL.PSN_RegisteProfession1));
			p.Add(db.CreateParameter("PSN_CertificateValidity1",DbType.DateTime, _ApplyAddItemMDL.PSN_CertificateValidity1));
			p.Add(db.CreateParameter("PSN_RegisteProfession2",DbType.String, _ApplyAddItemMDL.PSN_RegisteProfession2));
			p.Add(db.CreateParameter("PSN_CertificateValidity2",DbType.DateTime, _ApplyAddItemMDL.PSN_CertificateValidity2));
			p.Add(db.CreateParameter("PSN_RegisteProfession3",DbType.String, _ApplyAddItemMDL.PSN_RegisteProfession3));
			p.Add(db.CreateParameter("PSN_CertificateValidity3",DbType.DateTime, _ApplyAddItemMDL.PSN_CertificateValidity3));
			p.Add(db.CreateParameter("AddItem1",DbType.String, _ApplyAddItemMDL.AddItem1));
			p.Add(db.CreateParameter("ExamCode1",DbType.String, _ApplyAddItemMDL.ExamCode1));
			p.Add(db.CreateParameter("ExamDate1",DbType.DateTime, _ApplyAddItemMDL.ExamDate1));
			p.Add(db.CreateParameter("BiXiu1",DbType.Int32, _ApplyAddItemMDL.BiXiu1));
			p.Add(db.CreateParameter("XuanXiu1",DbType.Int32, _ApplyAddItemMDL.XuanXiu1));
			p.Add(db.CreateParameter("AddItem2",DbType.String, _ApplyAddItemMDL.AddItem2));
			p.Add(db.CreateParameter("ExamCode2",DbType.String, _ApplyAddItemMDL.ExamCode2));
			p.Add(db.CreateParameter("ExamDate2",DbType.DateTime, _ApplyAddItemMDL.ExamDate2));
			p.Add(db.CreateParameter("BiXiu2",DbType.Int32, _ApplyAddItemMDL.BiXiu2));
            p.Add(db.CreateParameter("XuanXiu2", DbType.Int32, _ApplyAddItemMDL.XuanXiu2));
            p.Add(db.CreateParameter("Nation", DbType.String, _ApplyAddItemMDL.Nation));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="ApplyAddItemID">主键</param>
		/// <returns></returns>
        public static int Delete( string ApplyID )
		{
			return Delete(null, ApplyID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="ApplyAddItemID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, string ApplyID)
		{
			string sql=@"DELETE FROM dbo.ApplyAddItem WHERE ApplyID = @ApplyID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ApplyID",DbType.String,ApplyID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="ApplyAddItemMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(ApplyAddItemMDL _ApplyAddItemMDL)
		{
			return Delete(null,_ApplyAddItemMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ApplyAddItemMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,ApplyAddItemMDL _ApplyAddItemMDL)
		{
			string sql=@"DELETE FROM dbo.ApplyAddItem WHERE ApplyID = @ApplyID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ApplyID",DbType.String,_ApplyAddItemMDL.ApplyID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ApplyAddItemID">主键</param>
        public static ApplyAddItemMDL GetObject( string ApplyID )
		{
			string sql= @"
			SELECT ApplyID,Nation,PSN_MobilePhone,PSN_Email,ENT_Telephone,PSN_RegisterCertificateNo,PSN_RegisteProfession1,PSN_CertificateValidity1,PSN_RegisteProfession2,PSN_CertificateValidity2,PSN_RegisteProfession3,PSN_CertificateValidity3,AddItem1,ExamCode1,ExamDate1,BiXiu1,XuanXiu1,AddItem2,ExamCode2,ExamDate2,BiXiu2,XuanXiu2
			FROM dbo.ApplyAddItem
			WHERE ApplyID = @ApplyID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, ApplyID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                ApplyAddItemMDL _ApplyAddItemMDL = null;
                if (reader.Read())
                {
                    _ApplyAddItemMDL = new ApplyAddItemMDL();
					if (reader["ApplyID"] != DBNull.Value) _ApplyAddItemMDL.ApplyID = Convert.ToString(reader["ApplyID"]);
					if (reader["PSN_MobilePhone"] != DBNull.Value) _ApplyAddItemMDL.PSN_MobilePhone = Convert.ToString(reader["PSN_MobilePhone"]);
					if (reader["PSN_Email"] != DBNull.Value) _ApplyAddItemMDL.PSN_Email = Convert.ToString(reader["PSN_Email"]);
					if (reader["ENT_Telephone"] != DBNull.Value) _ApplyAddItemMDL.ENT_Telephone = Convert.ToString(reader["ENT_Telephone"]);
					if (reader["PSN_RegisterCertificateNo"] != DBNull.Value) _ApplyAddItemMDL.PSN_RegisterCertificateNo = Convert.ToString(reader["PSN_RegisterCertificateNo"]);
					if (reader["PSN_RegisteProfession1"] != DBNull.Value) _ApplyAddItemMDL.PSN_RegisteProfession1 = Convert.ToString(reader["PSN_RegisteProfession1"]);
					if (reader["PSN_CertificateValidity1"] != DBNull.Value) _ApplyAddItemMDL.PSN_CertificateValidity1 = Convert.ToDateTime(reader["PSN_CertificateValidity1"]);
					if (reader["PSN_RegisteProfession2"] != DBNull.Value) _ApplyAddItemMDL.PSN_RegisteProfession2 = Convert.ToString(reader["PSN_RegisteProfession2"]);
					if (reader["PSN_CertificateValidity2"] != DBNull.Value) _ApplyAddItemMDL.PSN_CertificateValidity2 = Convert.ToDateTime(reader["PSN_CertificateValidity2"]);
					if (reader["PSN_RegisteProfession3"] != DBNull.Value) _ApplyAddItemMDL.PSN_RegisteProfession3 = Convert.ToString(reader["PSN_RegisteProfession3"]);
					if (reader["PSN_CertificateValidity3"] != DBNull.Value) _ApplyAddItemMDL.PSN_CertificateValidity3 = Convert.ToDateTime(reader["PSN_CertificateValidity3"]);
					if (reader["AddItem1"] != DBNull.Value) _ApplyAddItemMDL.AddItem1 = Convert.ToString(reader["AddItem1"]);
					if (reader["ExamCode1"] != DBNull.Value) _ApplyAddItemMDL.ExamCode1 = Convert.ToString(reader["ExamCode1"]);
					if (reader["ExamDate1"] != DBNull.Value) _ApplyAddItemMDL.ExamDate1 = Convert.ToDateTime(reader["ExamDate1"]);
					if (reader["BiXiu1"] != DBNull.Value) _ApplyAddItemMDL.BiXiu1 = Convert.ToInt32(reader["BiXiu1"]);
					if (reader["XuanXiu1"] != DBNull.Value) _ApplyAddItemMDL.XuanXiu1 = Convert.ToInt32(reader["XuanXiu1"]);
					if (reader["AddItem2"] != DBNull.Value) _ApplyAddItemMDL.AddItem2 = Convert.ToString(reader["AddItem2"]);
					if (reader["ExamCode2"] != DBNull.Value) _ApplyAddItemMDL.ExamCode2 = Convert.ToString(reader["ExamCode2"]);
					if (reader["ExamDate2"] != DBNull.Value) _ApplyAddItemMDL.ExamDate2 = Convert.ToDateTime(reader["ExamDate2"]);
					if (reader["BiXiu2"] != DBNull.Value) _ApplyAddItemMDL.BiXiu2 = Convert.ToInt32(reader["BiXiu2"]);
                    if (reader["XuanXiu2"] != DBNull.Value) _ApplyAddItemMDL.XuanXiu2 = Convert.ToInt32(reader["XuanXiu2"]);
                    if (reader["Nation"] != DBNull.Value) _ApplyAddItemMDL.Nation = Convert.ToString(reader["Nation"]);
                }
				reader.Close();
                db.Close();
                return _ApplyAddItemMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.ApplyAddItem", "*", filterWhereString, orderBy == "" ? " ApplyID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.ApplyAddItem", filterWhereString);
        }
        
        #region 自定义方法
        
        #endregion
    }
}
