using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--ApplyContinueDAL(填写类描述)
	/// </summary>
    public class ApplyContinueDAL
    {
        public ApplyContinueDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="ApplyContinueMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(ApplyContinueMDL _ApplyContinueMDL)
		{
		    return Insert(null,_ApplyContinueMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ApplyContinueMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,ApplyContinueMDL _ApplyContinueMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.ApplyContinue(ApplyID,PSN_MobilePhone,PSN_Email,LinkMan,ENT_Telephone,ENT_MobilePhone,ENT_Correspondence,ENT_Economic_Nature,END_Addess,ENT_Postcode,PSN_RegisterCertificateNo,PSN_RegisteProfession1,PSN_CertificateValidity1,IfContinue1,BiXiu1,XuanXiu1,Remark1,PSN_RegisteProfession2,PSN_CertificateValidity2,IfContinue2,BiXiu2,XuanXiu2,Remark2,PSN_RegisteProfession3,PSN_CertificateValidity3,IfContinue3,BiXiu3,XuanXiu3,Remark3,PSN_RegisteProfession4,PSN_CertificateValidity4,IfContinue4,BiXiu4,XuanXiu4,Remark4,MainJob,OtherCert,Nation)
			VALUES (@ApplyID,@PSN_MobilePhone,@PSN_Email,@LinkMan,@ENT_Telephone,@ENT_MobilePhone,@ENT_Correspondence,@ENT_Economic_Nature,@END_Addess,@ENT_Postcode,@PSN_RegisterCertificateNo,@PSN_RegisteProfession1,@PSN_CertificateValidity1,@IfContinue1,@BiXiu1,@XuanXiu1,@Remark1,@PSN_RegisteProfession2,@PSN_CertificateValidity2,@IfContinue2,@BiXiu2,@XuanXiu2,@Remark2,@PSN_RegisteProfession3,@PSN_CertificateValidity3,@IfContinue3,@BiXiu3,@XuanXiu3,@Remark3,@PSN_RegisteProfession4,@PSN_CertificateValidity4,@IfContinue4,@BiXiu4,@XuanXiu4,@Remark4,@MainJob,@OtherCert,@Nation)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ApplyID",DbType.String, _ApplyContinueMDL.ApplyID));
			p.Add(db.CreateParameter("PSN_MobilePhone",DbType.String, _ApplyContinueMDL.PSN_MobilePhone));
			p.Add(db.CreateParameter("PSN_Email",DbType.String, _ApplyContinueMDL.PSN_Email));
			p.Add(db.CreateParameter("LinkMan",DbType.String, _ApplyContinueMDL.LinkMan));
			p.Add(db.CreateParameter("ENT_Telephone",DbType.String, _ApplyContinueMDL.ENT_Telephone));
			p.Add(db.CreateParameter("ENT_MobilePhone",DbType.String, _ApplyContinueMDL.ENT_MobilePhone));
			p.Add(db.CreateParameter("ENT_Correspondence",DbType.String, _ApplyContinueMDL.ENT_Correspondence));
			p.Add(db.CreateParameter("ENT_Economic_Nature",DbType.String, _ApplyContinueMDL.ENT_Economic_Nature));
			p.Add(db.CreateParameter("END_Addess",DbType.String, _ApplyContinueMDL.END_Addess));
			p.Add(db.CreateParameter("ENT_Postcode",DbType.String, _ApplyContinueMDL.ENT_Postcode));
			p.Add(db.CreateParameter("PSN_RegisterCertificateNo",DbType.String, _ApplyContinueMDL.PSN_RegisterCertificateNo));
			p.Add(db.CreateParameter("PSN_RegisteProfession1",DbType.String, _ApplyContinueMDL.PSN_RegisteProfession1));
			p.Add(db.CreateParameter("PSN_CertificateValidity1",DbType.DateTime, _ApplyContinueMDL.PSN_CertificateValidity1));
			p.Add(db.CreateParameter("IfContinue1",DbType.Boolean, _ApplyContinueMDL.IfContinue1));
			p.Add(db.CreateParameter("BiXiu1",DbType.Int32, _ApplyContinueMDL.BiXiu1));
			p.Add(db.CreateParameter("XuanXiu1",DbType.Int32, _ApplyContinueMDL.XuanXiu1));
			p.Add(db.CreateParameter("Remark1",DbType.String, _ApplyContinueMDL.Remark1));
			p.Add(db.CreateParameter("PSN_RegisteProfession2",DbType.String, _ApplyContinueMDL.PSN_RegisteProfession2));
			p.Add(db.CreateParameter("PSN_CertificateValidity2",DbType.DateTime, _ApplyContinueMDL.PSN_CertificateValidity2));
			p.Add(db.CreateParameter("IfContinue2",DbType.Boolean, _ApplyContinueMDL.IfContinue2));
			p.Add(db.CreateParameter("BiXiu2",DbType.Int32, _ApplyContinueMDL.BiXiu2));
			p.Add(db.CreateParameter("XuanXiu2",DbType.Int32, _ApplyContinueMDL.XuanXiu2));
			p.Add(db.CreateParameter("Remark2",DbType.String, _ApplyContinueMDL.Remark2));
			p.Add(db.CreateParameter("PSN_RegisteProfession3",DbType.String, _ApplyContinueMDL.PSN_RegisteProfession3));
			p.Add(db.CreateParameter("PSN_CertificateValidity3",DbType.DateTime, _ApplyContinueMDL.PSN_CertificateValidity3));
			p.Add(db.CreateParameter("IfContinue3",DbType.Boolean, _ApplyContinueMDL.IfContinue3));
			p.Add(db.CreateParameter("BiXiu3",DbType.Int32, _ApplyContinueMDL.BiXiu3));
			p.Add(db.CreateParameter("XuanXiu3",DbType.Int32, _ApplyContinueMDL.XuanXiu3));
			p.Add(db.CreateParameter("Remark3",DbType.String, _ApplyContinueMDL.Remark3));
			p.Add(db.CreateParameter("PSN_RegisteProfession4",DbType.String, _ApplyContinueMDL.PSN_RegisteProfession4));
			p.Add(db.CreateParameter("PSN_CertificateValidity4",DbType.DateTime, _ApplyContinueMDL.PSN_CertificateValidity4));
			p.Add(db.CreateParameter("IfContinue4",DbType.Boolean, _ApplyContinueMDL.IfContinue4));
			p.Add(db.CreateParameter("BiXiu4",DbType.Int32, _ApplyContinueMDL.BiXiu4));
			p.Add(db.CreateParameter("XuanXiu4",DbType.Int32, _ApplyContinueMDL.XuanXiu4));
			p.Add(db.CreateParameter("Remark4",DbType.String, _ApplyContinueMDL.Remark4));
			p.Add(db.CreateParameter("MainJob",DbType.String, _ApplyContinueMDL.MainJob));
            p.Add(db.CreateParameter("OtherCert", DbType.String, _ApplyContinueMDL.OtherCert));
            p.Add(db.CreateParameter("Nation", DbType.String, _ApplyContinueMDL.Nation));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="ApplyContinueMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(ApplyContinueMDL _ApplyContinueMDL)
		{
			return Update(null,_ApplyContinueMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ApplyContinueMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,ApplyContinueMDL _ApplyContinueMDL)
		{
			string sql = @"
			UPDATE dbo.ApplyContinue
				SET	PSN_MobilePhone = @PSN_MobilePhone,PSN_Email = @PSN_Email,LinkMan = @LinkMan,ENT_Telephone = @ENT_Telephone,ENT_MobilePhone = @ENT_MobilePhone,ENT_Correspondence = @ENT_Correspondence,ENT_Economic_Nature = @ENT_Economic_Nature,END_Addess = @END_Addess,ENT_Postcode = @ENT_Postcode,PSN_RegisterCertificateNo = @PSN_RegisterCertificateNo,PSN_RegisteProfession1 = @PSN_RegisteProfession1,PSN_CertificateValidity1 = @PSN_CertificateValidity1,IfContinue1 = @IfContinue1,BiXiu1 = @BiXiu1,XuanXiu1 = @XuanXiu1,Remark1 = @Remark1,PSN_RegisteProfession2 = @PSN_RegisteProfession2,PSN_CertificateValidity2 = @PSN_CertificateValidity2,IfContinue2 = @IfContinue2,BiXiu2 = @BiXiu2,XuanXiu2 = @XuanXiu2,Remark2 = @Remark2,PSN_RegisteProfession3 = @PSN_RegisteProfession3,PSN_CertificateValidity3 = @PSN_CertificateValidity3,IfContinue3 = @IfContinue3,BiXiu3 = @BiXiu3,XuanXiu3 = @XuanXiu3,Remark3 = @Remark3,PSN_RegisteProfession4 = @PSN_RegisteProfession4,PSN_CertificateValidity4 = @PSN_CertificateValidity4,IfContinue4 = @IfContinue4,BiXiu4 = @BiXiu4,XuanXiu4 = @XuanXiu4,Remark4 = @Remark4,MainJob = @MainJob,OtherCert = @OtherCert,Nation=@Nation
			WHERE
				ApplyID = @ApplyID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ApplyID",DbType.String, _ApplyContinueMDL.ApplyID));
			p.Add(db.CreateParameter("PSN_MobilePhone",DbType.String, _ApplyContinueMDL.PSN_MobilePhone));
			p.Add(db.CreateParameter("PSN_Email",DbType.String, _ApplyContinueMDL.PSN_Email));
			p.Add(db.CreateParameter("LinkMan",DbType.String, _ApplyContinueMDL.LinkMan));
			p.Add(db.CreateParameter("ENT_Telephone",DbType.String, _ApplyContinueMDL.ENT_Telephone));
			p.Add(db.CreateParameter("ENT_MobilePhone",DbType.String, _ApplyContinueMDL.ENT_MobilePhone));
			p.Add(db.CreateParameter("ENT_Correspondence",DbType.String, _ApplyContinueMDL.ENT_Correspondence));
			p.Add(db.CreateParameter("ENT_Economic_Nature",DbType.String, _ApplyContinueMDL.ENT_Economic_Nature));
			p.Add(db.CreateParameter("END_Addess",DbType.String, _ApplyContinueMDL.END_Addess));
			p.Add(db.CreateParameter("ENT_Postcode",DbType.String, _ApplyContinueMDL.ENT_Postcode));
			p.Add(db.CreateParameter("PSN_RegisterCertificateNo",DbType.String, _ApplyContinueMDL.PSN_RegisterCertificateNo));
			p.Add(db.CreateParameter("PSN_RegisteProfession1",DbType.String, _ApplyContinueMDL.PSN_RegisteProfession1));
			p.Add(db.CreateParameter("PSN_CertificateValidity1",DbType.DateTime, _ApplyContinueMDL.PSN_CertificateValidity1));
			p.Add(db.CreateParameter("IfContinue1",DbType.Boolean, _ApplyContinueMDL.IfContinue1));
			p.Add(db.CreateParameter("BiXiu1",DbType.Int32, _ApplyContinueMDL.BiXiu1));
			p.Add(db.CreateParameter("XuanXiu1",DbType.Int32, _ApplyContinueMDL.XuanXiu1));
			p.Add(db.CreateParameter("Remark1",DbType.String, _ApplyContinueMDL.Remark1));
			p.Add(db.CreateParameter("PSN_RegisteProfession2",DbType.String, _ApplyContinueMDL.PSN_RegisteProfession2));
			p.Add(db.CreateParameter("PSN_CertificateValidity2",DbType.DateTime, _ApplyContinueMDL.PSN_CertificateValidity2));
			p.Add(db.CreateParameter("IfContinue2",DbType.Boolean, _ApplyContinueMDL.IfContinue2));
			p.Add(db.CreateParameter("BiXiu2",DbType.Int32, _ApplyContinueMDL.BiXiu2));
			p.Add(db.CreateParameter("XuanXiu2",DbType.Int32, _ApplyContinueMDL.XuanXiu2));
			p.Add(db.CreateParameter("Remark2",DbType.String, _ApplyContinueMDL.Remark2));
			p.Add(db.CreateParameter("PSN_RegisteProfession3",DbType.String, _ApplyContinueMDL.PSN_RegisteProfession3));
			p.Add(db.CreateParameter("PSN_CertificateValidity3",DbType.DateTime, _ApplyContinueMDL.PSN_CertificateValidity3));
			p.Add(db.CreateParameter("IfContinue3",DbType.Boolean, _ApplyContinueMDL.IfContinue3));
			p.Add(db.CreateParameter("BiXiu3",DbType.Int32, _ApplyContinueMDL.BiXiu3));
			p.Add(db.CreateParameter("XuanXiu3",DbType.Int32, _ApplyContinueMDL.XuanXiu3));
			p.Add(db.CreateParameter("Remark3",DbType.String, _ApplyContinueMDL.Remark3));
			p.Add(db.CreateParameter("PSN_RegisteProfession4",DbType.String, _ApplyContinueMDL.PSN_RegisteProfession4));
			p.Add(db.CreateParameter("PSN_CertificateValidity4",DbType.DateTime, _ApplyContinueMDL.PSN_CertificateValidity4));
			p.Add(db.CreateParameter("IfContinue4",DbType.Boolean, _ApplyContinueMDL.IfContinue4));
			p.Add(db.CreateParameter("BiXiu4",DbType.Int32, _ApplyContinueMDL.BiXiu4));
			p.Add(db.CreateParameter("XuanXiu4",DbType.Int32, _ApplyContinueMDL.XuanXiu4));
			p.Add(db.CreateParameter("Remark4",DbType.String, _ApplyContinueMDL.Remark4));
			p.Add(db.CreateParameter("MainJob",DbType.String, _ApplyContinueMDL.MainJob));
            p.Add(db.CreateParameter("OtherCert", DbType.String, _ApplyContinueMDL.OtherCert));
            p.Add(db.CreateParameter("Nation", DbType.String, _ApplyContinueMDL.Nation));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="ApplyContinueID">主键</param>
		/// <returns></returns>
        public static int Delete( string ApplyID )
		{
			return Delete(null, ApplyID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="ApplyContinueID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, string ApplyID)
		{
			string sql=@"DELETE FROM dbo.ApplyContinue WHERE ApplyID = @ApplyID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ApplyID",DbType.String,ApplyID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="ApplyContinueMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(ApplyContinueMDL _ApplyContinueMDL)
		{
			return Delete(null,_ApplyContinueMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ApplyContinueMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,ApplyContinueMDL _ApplyContinueMDL)
		{
			string sql=@"DELETE FROM dbo.ApplyContinue WHERE ApplyID = @ApplyID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ApplyID",DbType.String,_ApplyContinueMDL.ApplyID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ApplyContinueID">主键</param>
        public static ApplyContinueMDL GetObject( string ApplyID )
		{
			string sql= @"
			SELECT ApplyID,Nation,PSN_MobilePhone,PSN_Email,LinkMan,ENT_Telephone,ENT_MobilePhone,ENT_Correspondence,ENT_Economic_Nature,END_Addess,ENT_Postcode,PSN_RegisterCertificateNo,PSN_RegisteProfession1,PSN_CertificateValidity1,IfContinue1,BiXiu1,XuanXiu1,Remark1,PSN_RegisteProfession2,PSN_CertificateValidity2,IfContinue2,BiXiu2,XuanXiu2,Remark2,PSN_RegisteProfession3,PSN_CertificateValidity3,IfContinue3,BiXiu3,XuanXiu3,Remark3,PSN_RegisteProfession4,PSN_CertificateValidity4,IfContinue4,BiXiu4,XuanXiu4,Remark4,MainJob,OtherCert
			FROM dbo.ApplyContinue
			WHERE ApplyID = @ApplyID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, ApplyID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                ApplyContinueMDL _ApplyContinueMDL = null;
                if (reader.Read())
                {
                    _ApplyContinueMDL = new ApplyContinueMDL();
					if (reader["ApplyID"] != DBNull.Value) _ApplyContinueMDL.ApplyID = Convert.ToString(reader["ApplyID"]);
					if (reader["PSN_MobilePhone"] != DBNull.Value) _ApplyContinueMDL.PSN_MobilePhone = Convert.ToString(reader["PSN_MobilePhone"]);
					if (reader["PSN_Email"] != DBNull.Value) _ApplyContinueMDL.PSN_Email = Convert.ToString(reader["PSN_Email"]);
					if (reader["LinkMan"] != DBNull.Value) _ApplyContinueMDL.LinkMan = Convert.ToString(reader["LinkMan"]);
					if (reader["ENT_Telephone"] != DBNull.Value) _ApplyContinueMDL.ENT_Telephone = Convert.ToString(reader["ENT_Telephone"]);
					if (reader["ENT_MobilePhone"] != DBNull.Value) _ApplyContinueMDL.ENT_MobilePhone = Convert.ToString(reader["ENT_MobilePhone"]);
					if (reader["ENT_Correspondence"] != DBNull.Value) _ApplyContinueMDL.ENT_Correspondence = Convert.ToString(reader["ENT_Correspondence"]);
					if (reader["ENT_Economic_Nature"] != DBNull.Value) _ApplyContinueMDL.ENT_Economic_Nature = Convert.ToString(reader["ENT_Economic_Nature"]);
					if (reader["END_Addess"] != DBNull.Value) _ApplyContinueMDL.END_Addess = Convert.ToString(reader["END_Addess"]);
					if (reader["ENT_Postcode"] != DBNull.Value) _ApplyContinueMDL.ENT_Postcode = Convert.ToString(reader["ENT_Postcode"]);
					if (reader["PSN_RegisterCertificateNo"] != DBNull.Value) _ApplyContinueMDL.PSN_RegisterCertificateNo = Convert.ToString(reader["PSN_RegisterCertificateNo"]);
					if (reader["PSN_RegisteProfession1"] != DBNull.Value) _ApplyContinueMDL.PSN_RegisteProfession1 = Convert.ToString(reader["PSN_RegisteProfession1"]);
					if (reader["PSN_CertificateValidity1"] != DBNull.Value) _ApplyContinueMDL.PSN_CertificateValidity1 = Convert.ToDateTime(reader["PSN_CertificateValidity1"]);
					if (reader["IfContinue1"] != DBNull.Value) _ApplyContinueMDL.IfContinue1 = Convert.ToBoolean(reader["IfContinue1"]);
					if (reader["BiXiu1"] != DBNull.Value) _ApplyContinueMDL.BiXiu1 = Convert.ToInt32(reader["BiXiu1"]);
					if (reader["XuanXiu1"] != DBNull.Value) _ApplyContinueMDL.XuanXiu1 = Convert.ToInt32(reader["XuanXiu1"]);
					if (reader["Remark1"] != DBNull.Value) _ApplyContinueMDL.Remark1 = Convert.ToString(reader["Remark1"]);
					if (reader["PSN_RegisteProfession2"] != DBNull.Value) _ApplyContinueMDL.PSN_RegisteProfession2 = Convert.ToString(reader["PSN_RegisteProfession2"]);
					if (reader["PSN_CertificateValidity2"] != DBNull.Value) _ApplyContinueMDL.PSN_CertificateValidity2 = Convert.ToDateTime(reader["PSN_CertificateValidity2"]);
					if (reader["IfContinue2"] != DBNull.Value) _ApplyContinueMDL.IfContinue2 = Convert.ToBoolean(reader["IfContinue2"]);
					if (reader["BiXiu2"] != DBNull.Value) _ApplyContinueMDL.BiXiu2 = Convert.ToInt32(reader["BiXiu2"]);
					if (reader["XuanXiu2"] != DBNull.Value) _ApplyContinueMDL.XuanXiu2 = Convert.ToInt32(reader["XuanXiu2"]);
					if (reader["Remark2"] != DBNull.Value) _ApplyContinueMDL.Remark2 = Convert.ToString(reader["Remark2"]);
					if (reader["PSN_RegisteProfession3"] != DBNull.Value) _ApplyContinueMDL.PSN_RegisteProfession3 = Convert.ToString(reader["PSN_RegisteProfession3"]);
					if (reader["PSN_CertificateValidity3"] != DBNull.Value) _ApplyContinueMDL.PSN_CertificateValidity3 = Convert.ToDateTime(reader["PSN_CertificateValidity3"]);
					if (reader["IfContinue3"] != DBNull.Value) _ApplyContinueMDL.IfContinue3 = Convert.ToBoolean(reader["IfContinue3"]);
					if (reader["BiXiu3"] != DBNull.Value) _ApplyContinueMDL.BiXiu3 = Convert.ToInt32(reader["BiXiu3"]);
					if (reader["XuanXiu3"] != DBNull.Value) _ApplyContinueMDL.XuanXiu3 = Convert.ToInt32(reader["XuanXiu3"]);
					if (reader["Remark3"] != DBNull.Value) _ApplyContinueMDL.Remark3 = Convert.ToString(reader["Remark3"]);
					if (reader["PSN_RegisteProfession4"] != DBNull.Value) _ApplyContinueMDL.PSN_RegisteProfession4 = Convert.ToString(reader["PSN_RegisteProfession4"]);
					if (reader["PSN_CertificateValidity4"] != DBNull.Value) _ApplyContinueMDL.PSN_CertificateValidity4 = Convert.ToDateTime(reader["PSN_CertificateValidity4"]);
					if (reader["IfContinue4"] != DBNull.Value) _ApplyContinueMDL.IfContinue4 = Convert.ToBoolean(reader["IfContinue4"]);
					if (reader["BiXiu4"] != DBNull.Value) _ApplyContinueMDL.BiXiu4 = Convert.ToInt32(reader["BiXiu4"]);
					if (reader["XuanXiu4"] != DBNull.Value) _ApplyContinueMDL.XuanXiu4 = Convert.ToInt32(reader["XuanXiu4"]);
					if (reader["Remark4"] != DBNull.Value) _ApplyContinueMDL.Remark4 = Convert.ToString(reader["Remark4"]);
					if (reader["MainJob"] != DBNull.Value) _ApplyContinueMDL.MainJob = Convert.ToString(reader["MainJob"]);
                    if (reader["OtherCert"] != DBNull.Value) _ApplyContinueMDL.OtherCert = Convert.ToString(reader["OtherCert"]);
                    if (reader["Nation"] != DBNull.Value) _ApplyContinueMDL.Nation = Convert.ToString(reader["Nation"]);
                }
				reader.Close();
                db.Close();
                return _ApplyContinueMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.ApplyContinue", "*", filterWhereString, orderBy == "" ? " ApplyID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.ApplyContinue", filterWhereString);
        }
        
        #region 自定义方法
        
        #endregion
    }
}
