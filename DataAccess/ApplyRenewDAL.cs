using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--ApplyRenewDAL(填写类描述)
	/// </summary>
    public class ApplyRenewDAL
    {
        public ApplyRenewDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="ApplyRenewMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(ApplyRenewMDL _ApplyRenewMDL)
		{
		    return Insert(null,_ApplyRenewMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ApplyRenewMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,ApplyRenewMDL _ApplyRenewMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.ApplyRenew(ApplyID,PSN_Telephone,PSN_MobilePhone,PSN_Email,OldRegisterNo,OldRegisterCertificateNo,DisnableDate,DisnableReason,FR,LinkMan,ENT_Telephone,ENT_Correspondence,ENT_Economic_Nature,END_Addess,ENT_Postcode,ENT_Type,ENT_Sort,ENT_Grade,ENT_QualificationCertificateNo,ENT_Sort2,ENT_Grade2,ENT_QualificationCertificateNo2,ExamInfo,OtherCert,Nation)
			VALUES (@ApplyID,@PSN_Telephone,@PSN_MobilePhone,@PSN_Email,@OldRegisterNo,@OldRegisterCertificateNo,@DisnableDate,@DisnableReason,@FR,@LinkMan,@ENT_Telephone,@ENT_Correspondence,@ENT_Economic_Nature,@END_Addess,@ENT_Postcode,@ENT_Type,@ENT_Sort,@ENT_Grade,@ENT_QualificationCertificateNo,@ENT_Sort2,@ENT_Grade2,@ENT_QualificationCertificateNo2,@ExamInfo,@OtherCert,@Nation)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ApplyID",DbType.String, _ApplyRenewMDL.ApplyID));
			p.Add(db.CreateParameter("PSN_Telephone",DbType.String, _ApplyRenewMDL.PSN_Telephone));
			p.Add(db.CreateParameter("PSN_MobilePhone",DbType.String, _ApplyRenewMDL.PSN_MobilePhone));
			p.Add(db.CreateParameter("PSN_Email",DbType.String, _ApplyRenewMDL.PSN_Email));
			p.Add(db.CreateParameter("OldRegisterNo",DbType.String, _ApplyRenewMDL.OldRegisterNo));
			p.Add(db.CreateParameter("OldRegisterCertificateNo",DbType.String, _ApplyRenewMDL.OldRegisterCertificateNo));
			p.Add(db.CreateParameter("DisnableDate",DbType.DateTime, _ApplyRenewMDL.DisnableDate));
			p.Add(db.CreateParameter("DisnableReason",DbType.String, _ApplyRenewMDL.DisnableReason));
			p.Add(db.CreateParameter("FR",DbType.String, _ApplyRenewMDL.FR));
			p.Add(db.CreateParameter("LinkMan",DbType.String, _ApplyRenewMDL.LinkMan));
			p.Add(db.CreateParameter("ENT_Telephone",DbType.String, _ApplyRenewMDL.ENT_Telephone));
			p.Add(db.CreateParameter("ENT_Correspondence",DbType.String, _ApplyRenewMDL.ENT_Correspondence));
			p.Add(db.CreateParameter("ENT_Economic_Nature",DbType.String, _ApplyRenewMDL.ENT_Economic_Nature));
			p.Add(db.CreateParameter("END_Addess",DbType.String, _ApplyRenewMDL.END_Addess));
			p.Add(db.CreateParameter("ENT_Postcode",DbType.String, _ApplyRenewMDL.ENT_Postcode));
			p.Add(db.CreateParameter("ENT_Type",DbType.String, _ApplyRenewMDL.ENT_Type));
			p.Add(db.CreateParameter("ENT_Sort",DbType.String, _ApplyRenewMDL.ENT_Sort));
			p.Add(db.CreateParameter("ENT_Grade",DbType.String, _ApplyRenewMDL.ENT_Grade));
			p.Add(db.CreateParameter("ENT_QualificationCertificateNo",DbType.String, _ApplyRenewMDL.ENT_QualificationCertificateNo));
			p.Add(db.CreateParameter("ENT_Sort2",DbType.String, _ApplyRenewMDL.ENT_Sort2));
			p.Add(db.CreateParameter("ENT_Grade2",DbType.String, _ApplyRenewMDL.ENT_Grade2));
			p.Add(db.CreateParameter("ENT_QualificationCertificateNo2",DbType.String, _ApplyRenewMDL.ENT_QualificationCertificateNo2));
			p.Add(db.CreateParameter("ExamInfo",DbType.String, _ApplyRenewMDL.ExamInfo));
            p.Add(db.CreateParameter("OtherCert", DbType.String, _ApplyRenewMDL.OtherCert));
            p.Add(db.CreateParameter("Nation", DbType.String, _ApplyRenewMDL.Nation));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="ApplyRenewMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(ApplyRenewMDL _ApplyRenewMDL)
		{
			return Update(null,_ApplyRenewMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ApplyRenewMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,ApplyRenewMDL _ApplyRenewMDL)
		{
			string sql = @"
			UPDATE dbo.ApplyRenew
				SET	PSN_Telephone = @PSN_Telephone,PSN_MobilePhone = @PSN_MobilePhone,PSN_Email = @PSN_Email,OldRegisterNo = @OldRegisterNo,OldRegisterCertificateNo = @OldRegisterCertificateNo,DisnableDate = @DisnableDate,DisnableReason = @DisnableReason,FR = @FR,LinkMan = @LinkMan,ENT_Telephone = @ENT_Telephone,ENT_Correspondence = @ENT_Correspondence,ENT_Economic_Nature = @ENT_Economic_Nature,END_Addess = @END_Addess,ENT_Postcode = @ENT_Postcode,ENT_Type = @ENT_Type,ENT_Sort = @ENT_Sort,ENT_Grade = @ENT_Grade,ENT_QualificationCertificateNo = @ENT_QualificationCertificateNo,ENT_Sort2 = @ENT_Sort2,ENT_Grade2 = @ENT_Grade2,ENT_QualificationCertificateNo2 = @ENT_QualificationCertificateNo2,ExamInfo = @ExamInfo,OtherCert = @OtherCert,Nation = @Nation
			WHERE
				ApplyID = @ApplyID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ApplyID",DbType.String, _ApplyRenewMDL.ApplyID));
			p.Add(db.CreateParameter("PSN_Telephone",DbType.String, _ApplyRenewMDL.PSN_Telephone));
			p.Add(db.CreateParameter("PSN_MobilePhone",DbType.String, _ApplyRenewMDL.PSN_MobilePhone));
			p.Add(db.CreateParameter("PSN_Email",DbType.String, _ApplyRenewMDL.PSN_Email));
            p.Add(db.CreateParameter("Nation", DbType.String, _ApplyRenewMDL.Nation));
			p.Add(db.CreateParameter("OldRegisterNo",DbType.String, _ApplyRenewMDL.OldRegisterNo));
			p.Add(db.CreateParameter("OldRegisterCertificateNo",DbType.String, _ApplyRenewMDL.OldRegisterCertificateNo));
			p.Add(db.CreateParameter("DisnableDate",DbType.DateTime, _ApplyRenewMDL.DisnableDate));
			p.Add(db.CreateParameter("DisnableReason",DbType.String, _ApplyRenewMDL.DisnableReason));
			p.Add(db.CreateParameter("FR",DbType.String, _ApplyRenewMDL.FR));
			p.Add(db.CreateParameter("LinkMan",DbType.String, _ApplyRenewMDL.LinkMan));
			p.Add(db.CreateParameter("ENT_Telephone",DbType.String, _ApplyRenewMDL.ENT_Telephone));
			p.Add(db.CreateParameter("ENT_Correspondence",DbType.String, _ApplyRenewMDL.ENT_Correspondence));
			p.Add(db.CreateParameter("ENT_Economic_Nature",DbType.String, _ApplyRenewMDL.ENT_Economic_Nature));
			p.Add(db.CreateParameter("END_Addess",DbType.String, _ApplyRenewMDL.END_Addess));
			p.Add(db.CreateParameter("ENT_Postcode",DbType.String, _ApplyRenewMDL.ENT_Postcode));
			p.Add(db.CreateParameter("ENT_Type",DbType.String, _ApplyRenewMDL.ENT_Type));
			p.Add(db.CreateParameter("ENT_Sort",DbType.String, _ApplyRenewMDL.ENT_Sort));
			p.Add(db.CreateParameter("ENT_Grade",DbType.String, _ApplyRenewMDL.ENT_Grade));
			p.Add(db.CreateParameter("ENT_QualificationCertificateNo",DbType.String, _ApplyRenewMDL.ENT_QualificationCertificateNo));
			p.Add(db.CreateParameter("ENT_Sort2",DbType.String, _ApplyRenewMDL.ENT_Sort2));
			p.Add(db.CreateParameter("ENT_Grade2",DbType.String, _ApplyRenewMDL.ENT_Grade2));
			p.Add(db.CreateParameter("ENT_QualificationCertificateNo2",DbType.String, _ApplyRenewMDL.ENT_QualificationCertificateNo2));
			p.Add(db.CreateParameter("ExamInfo",DbType.String, _ApplyRenewMDL.ExamInfo));
			p.Add(db.CreateParameter("OtherCert",DbType.String, _ApplyRenewMDL.OtherCert));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="ApplyRenewID">主键</param>
		/// <returns></returns>
        public static int Delete( string ApplyID )
		{
			return Delete(null, ApplyID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="ApplyRenewID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, string ApplyID)
		{
			string sql=@"DELETE FROM dbo.ApplyRenew WHERE ApplyID = @ApplyID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ApplyID",DbType.String,ApplyID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="ApplyRenewMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(ApplyRenewMDL _ApplyRenewMDL)
		{
			return Delete(null,_ApplyRenewMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ApplyRenewMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,ApplyRenewMDL _ApplyRenewMDL)
		{
			string sql=@"DELETE FROM dbo.ApplyRenew WHERE ApplyID = @ApplyID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ApplyID",DbType.String,_ApplyRenewMDL.ApplyID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ApplyRenewID">主键</param>
        public static ApplyRenewMDL GetObject( string ApplyID )
		{
			string sql= @"
			SELECT ApplyID,Nation,PSN_Telephone,PSN_MobilePhone,PSN_Email,OldRegisterNo,OldRegisterCertificateNo,DisnableDate,DisnableReason,FR,LinkMan,ENT_Telephone,ENT_Correspondence,ENT_Economic_Nature,END_Addess,ENT_Postcode,ENT_Type,ENT_Sort,ENT_Grade,ENT_QualificationCertificateNo,ENT_Sort2,ENT_Grade2,ENT_QualificationCertificateNo2,ExamInfo,OtherCert
			FROM dbo.ApplyRenew
			WHERE ApplyID = @ApplyID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, ApplyID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                ApplyRenewMDL _ApplyRenewMDL = null;
                if (reader.Read())
                {
                    _ApplyRenewMDL = new ApplyRenewMDL();
					if (reader["ApplyID"] != DBNull.Value) _ApplyRenewMDL.ApplyID = Convert.ToString(reader["ApplyID"]);
                    if (reader["Nation"] != DBNull.Value) _ApplyRenewMDL.Nation = Convert.ToString(reader["Nation"]);
                    if (reader["PSN_Telephone"] != DBNull.Value) _ApplyRenewMDL.PSN_Telephone = Convert.ToString(reader["PSN_Telephone"]);
					if (reader["PSN_MobilePhone"] != DBNull.Value) _ApplyRenewMDL.PSN_MobilePhone = Convert.ToString(reader["PSN_MobilePhone"]);
					if (reader["PSN_Email"] != DBNull.Value) _ApplyRenewMDL.PSN_Email = Convert.ToString(reader["PSN_Email"]);
					if (reader["OldRegisterNo"] != DBNull.Value) _ApplyRenewMDL.OldRegisterNo = Convert.ToString(reader["OldRegisterNo"]);
					if (reader["OldRegisterCertificateNo"] != DBNull.Value) _ApplyRenewMDL.OldRegisterCertificateNo = Convert.ToString(reader["OldRegisterCertificateNo"]);
					if (reader["DisnableDate"] != DBNull.Value) _ApplyRenewMDL.DisnableDate = Convert.ToDateTime(reader["DisnableDate"]);
					if (reader["DisnableReason"] != DBNull.Value) _ApplyRenewMDL.DisnableReason = Convert.ToString(reader["DisnableReason"]);
					if (reader["FR"] != DBNull.Value) _ApplyRenewMDL.FR = Convert.ToString(reader["FR"]);
					if (reader["LinkMan"] != DBNull.Value) _ApplyRenewMDL.LinkMan = Convert.ToString(reader["LinkMan"]);
					if (reader["ENT_Telephone"] != DBNull.Value) _ApplyRenewMDL.ENT_Telephone = Convert.ToString(reader["ENT_Telephone"]);
					if (reader["ENT_Correspondence"] != DBNull.Value) _ApplyRenewMDL.ENT_Correspondence = Convert.ToString(reader["ENT_Correspondence"]);
					if (reader["ENT_Economic_Nature"] != DBNull.Value) _ApplyRenewMDL.ENT_Economic_Nature = Convert.ToString(reader["ENT_Economic_Nature"]);
					if (reader["END_Addess"] != DBNull.Value) _ApplyRenewMDL.END_Addess = Convert.ToString(reader["END_Addess"]);
					if (reader["ENT_Postcode"] != DBNull.Value) _ApplyRenewMDL.ENT_Postcode = Convert.ToString(reader["ENT_Postcode"]);
					if (reader["ENT_Type"] != DBNull.Value) _ApplyRenewMDL.ENT_Type = Convert.ToString(reader["ENT_Type"]);
					if (reader["ENT_Sort"] != DBNull.Value) _ApplyRenewMDL.ENT_Sort = Convert.ToString(reader["ENT_Sort"]);
					if (reader["ENT_Grade"] != DBNull.Value) _ApplyRenewMDL.ENT_Grade = Convert.ToString(reader["ENT_Grade"]);
					if (reader["ENT_QualificationCertificateNo"] != DBNull.Value) _ApplyRenewMDL.ENT_QualificationCertificateNo = Convert.ToString(reader["ENT_QualificationCertificateNo"]);
					if (reader["ENT_Sort2"] != DBNull.Value) _ApplyRenewMDL.ENT_Sort2 = Convert.ToString(reader["ENT_Sort2"]);
					if (reader["ENT_Grade2"] != DBNull.Value) _ApplyRenewMDL.ENT_Grade2 = Convert.ToString(reader["ENT_Grade2"]);
					if (reader["ENT_QualificationCertificateNo2"] != DBNull.Value) _ApplyRenewMDL.ENT_QualificationCertificateNo2 = Convert.ToString(reader["ENT_QualificationCertificateNo2"]);
					if (reader["ExamInfo"] != DBNull.Value) _ApplyRenewMDL.ExamInfo = Convert.ToString(reader["ExamInfo"]);
					if (reader["OtherCert"] != DBNull.Value) _ApplyRenewMDL.OtherCert = Convert.ToString(reader["OtherCert"]);
                }
				reader.Close();
                db.Close();
                return _ApplyRenewMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.ApplyRenew", "*", filterWhereString, orderBy == "" ? " ApplyID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.ApplyRenew", filterWhereString);
        }
        
        #region 自定义方法
        
        #endregion
    }
}
