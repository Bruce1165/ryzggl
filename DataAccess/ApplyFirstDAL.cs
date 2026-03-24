using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--ApplyFirstDAL(填写类描述)
	/// </summary>
    public class ApplyFirstDAL
    {
        public ApplyFirstDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="ApplyFirstMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(ApplyFirstMDL _ApplyFirstMDL)
		{
		    return Insert(null,_ApplyFirstMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ApplyFirstMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,ApplyFirstMDL _ApplyFirstMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.ApplyFirst(ApplyID,ArchitectType,PSN_Telephone,PSN_MobilePhone,PSN_Email,Nation,Birthday,School,Major,GraduationTime,XueLi,XueWei,FR,LinkMan,ENT_Telephone,ENT_Correspondence,ENT_Economic_Nature,END_Addess,ENT_Postcode,ENT_Type,ENT_Sort,ENT_Grade,ENT_QualificationCertificateNo,ZhuanYe,GetType,PSN_ExamCertCode,ConferDate,ApplyRegisteProfession,BiXiu,XuanXiu,ExamInfo,OtherCert,IfSameUnit,MainJob)
			VALUES (@ApplyID,@ArchitectType,@PSN_Telephone,@PSN_MobilePhone,@PSN_Email,@Nation,@Birthday,@School,@Major,@GraduationTime,@XueLi,@XueWei,@FR,@LinkMan,@ENT_Telephone,@ENT_Correspondence,@ENT_Economic_Nature,@END_Addess,@ENT_Postcode,@ENT_Type,@ENT_Sort,@ENT_Grade,@ENT_QualificationCertificateNo,@ZhuanYe,@GetType,@PSN_ExamCertCode,@ConferDate,@ApplyRegisteProfession,@BiXiu,@XuanXiu,@ExamInfo,@OtherCert,@IfSameUnit,@MainJob)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ApplyID",DbType.String, _ApplyFirstMDL.ApplyID));
            p.Add(db.CreateParameter("ArchitectType", DbType.String, _ApplyFirstMDL.ArchitectType));
			p.Add(db.CreateParameter("PSN_Telephone",DbType.String, _ApplyFirstMDL.PSN_Telephone));
			p.Add(db.CreateParameter("PSN_MobilePhone",DbType.String, _ApplyFirstMDL.PSN_MobilePhone));
			p.Add(db.CreateParameter("PSN_Email",DbType.String, _ApplyFirstMDL.PSN_Email));
			p.Add(db.CreateParameter("Nation",DbType.String, _ApplyFirstMDL.Nation));
			p.Add(db.CreateParameter("Birthday",DbType.DateTime, _ApplyFirstMDL.Birthday));
			p.Add(db.CreateParameter("School",DbType.String, _ApplyFirstMDL.School));
			p.Add(db.CreateParameter("Major",DbType.String, _ApplyFirstMDL.Major));
			p.Add(db.CreateParameter("GraduationTime",DbType.DateTime, _ApplyFirstMDL.GraduationTime));
			p.Add(db.CreateParameter("XueLi",DbType.String, _ApplyFirstMDL.XueLi));
			p.Add(db.CreateParameter("XueWei",DbType.String, _ApplyFirstMDL.XueWei));
			p.Add(db.CreateParameter("FR",DbType.String, _ApplyFirstMDL.FR));
			p.Add(db.CreateParameter("LinkMan",DbType.String, _ApplyFirstMDL.LinkMan));
			p.Add(db.CreateParameter("ENT_Telephone",DbType.String, _ApplyFirstMDL.ENT_Telephone));
			p.Add(db.CreateParameter("ENT_Correspondence",DbType.String, _ApplyFirstMDL.ENT_Correspondence));
			p.Add(db.CreateParameter("ENT_Economic_Nature",DbType.String, _ApplyFirstMDL.ENT_Economic_Nature));
			p.Add(db.CreateParameter("END_Addess",DbType.String, _ApplyFirstMDL.END_Addess));
			p.Add(db.CreateParameter("ENT_Postcode",DbType.String, _ApplyFirstMDL.ENT_Postcode));
			p.Add(db.CreateParameter("ENT_Type",DbType.String, _ApplyFirstMDL.ENT_Type));
			p.Add(db.CreateParameter("ENT_Sort",DbType.String, _ApplyFirstMDL.ENT_Sort));
			p.Add(db.CreateParameter("ENT_Grade",DbType.String, _ApplyFirstMDL.ENT_Grade));
			p.Add(db.CreateParameter("ENT_QualificationCertificateNo",DbType.String, _ApplyFirstMDL.ENT_QualificationCertificateNo));
			p.Add(db.CreateParameter("ZhuanYe",DbType.String, _ApplyFirstMDL.ZhuanYe));
			p.Add(db.CreateParameter("GetType",DbType.String, _ApplyFirstMDL.GetType));
			p.Add(db.CreateParameter("PSN_ExamCertCode",DbType.String, _ApplyFirstMDL.PSN_ExamCertCode));
			p.Add(db.CreateParameter("ConferDate",DbType.DateTime, _ApplyFirstMDL.ConferDate));
			p.Add(db.CreateParameter("ApplyRegisteProfession",DbType.String, _ApplyFirstMDL.ApplyRegisteProfession));
			p.Add(db.CreateParameter("BiXiu",DbType.Int32, _ApplyFirstMDL.BiXiu));
			p.Add(db.CreateParameter("XuanXiu",DbType.Int32, _ApplyFirstMDL.XuanXiu));
			p.Add(db.CreateParameter("ExamInfo",DbType.String, _ApplyFirstMDL.ExamInfo));
			p.Add(db.CreateParameter("OtherCert",DbType.String, _ApplyFirstMDL.OtherCert));
			p.Add(db.CreateParameter("IfSameUnit",DbType.Boolean, _ApplyFirstMDL.IfSameUnit));
			p.Add(db.CreateParameter("MainJob",DbType.String, _ApplyFirstMDL.MainJob));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="ApplyFirstMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(ApplyFirstMDL _ApplyFirstMDL)
		{
			return Update(null,_ApplyFirstMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ApplyFirstMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,ApplyFirstMDL _ApplyFirstMDL)
		{
            string sql = @"
			UPDATE dbo.ApplyFirst
				SET	ArchitectType = @ArchitectType,PSN_Telephone = @PSN_Telephone,PSN_MobilePhone = @PSN_MobilePhone,PSN_Email = @PSN_Email,Nation = @Nation,Birthday = @Birthday,School = @School,Major = @Major,GraduationTime = @GraduationTime,XueLi = @XueLi,XueWei = @XueWei,FR = @FR,LinkMan = @LinkMan,ENT_Telephone = @ENT_Telephone,ENT_Correspondence = @ENT_Correspondence,ENT_Economic_Nature = @ENT_Economic_Nature,END_Addess = @END_Addess,ENT_Postcode = @ENT_Postcode,ENT_Type = @ENT_Type,ENT_Sort = @ENT_Sort,ENT_Grade = @ENT_Grade,ENT_QualificationCertificateNo = @ENT_QualificationCertificateNo,ZhuanYe = @ZhuanYe,GetType = @GetType,PSN_ExamCertCode = @PSN_ExamCertCode,ConferDate = @ConferDate,ApplyRegisteProfession = @ApplyRegisteProfession,BiXiu = @BiXiu,XuanXiu = @XuanXiu,ExamInfo = @ExamInfo,OtherCert = @OtherCert,IfSameUnit = @IfSameUnit,MainJob = @MainJob
			WHERE
				ApplyID = @ApplyID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ApplyID",DbType.String, _ApplyFirstMDL.ApplyID));
            p.Add(db.CreateParameter("ArchitectType", DbType.String, _ApplyFirstMDL.ArchitectType));
			p.Add(db.CreateParameter("PSN_Telephone",DbType.String, _ApplyFirstMDL.PSN_Telephone));
			p.Add(db.CreateParameter("PSN_MobilePhone",DbType.String, _ApplyFirstMDL.PSN_MobilePhone));
			p.Add(db.CreateParameter("PSN_Email",DbType.String, _ApplyFirstMDL.PSN_Email));
			p.Add(db.CreateParameter("Nation",DbType.String, _ApplyFirstMDL.Nation));
			p.Add(db.CreateParameter("Birthday",DbType.DateTime, _ApplyFirstMDL.Birthday));
			p.Add(db.CreateParameter("School",DbType.String, _ApplyFirstMDL.School));
			p.Add(db.CreateParameter("Major",DbType.String, _ApplyFirstMDL.Major));
			p.Add(db.CreateParameter("GraduationTime",DbType.DateTime, _ApplyFirstMDL.GraduationTime));
			p.Add(db.CreateParameter("XueLi",DbType.String, _ApplyFirstMDL.XueLi));
			p.Add(db.CreateParameter("XueWei",DbType.String, _ApplyFirstMDL.XueWei));
			p.Add(db.CreateParameter("FR",DbType.String, _ApplyFirstMDL.FR));
			p.Add(db.CreateParameter("LinkMan",DbType.String, _ApplyFirstMDL.LinkMan));
			p.Add(db.CreateParameter("ENT_Telephone",DbType.String, _ApplyFirstMDL.ENT_Telephone));
			p.Add(db.CreateParameter("ENT_Correspondence",DbType.String, _ApplyFirstMDL.ENT_Correspondence));
			p.Add(db.CreateParameter("ENT_Economic_Nature",DbType.String, _ApplyFirstMDL.ENT_Economic_Nature));
			p.Add(db.CreateParameter("END_Addess",DbType.String, _ApplyFirstMDL.END_Addess));
			p.Add(db.CreateParameter("ENT_Postcode",DbType.String, _ApplyFirstMDL.ENT_Postcode));
			p.Add(db.CreateParameter("ENT_Type",DbType.String, _ApplyFirstMDL.ENT_Type));
			p.Add(db.CreateParameter("ENT_Sort",DbType.String, _ApplyFirstMDL.ENT_Sort));
			p.Add(db.CreateParameter("ENT_Grade",DbType.String, _ApplyFirstMDL.ENT_Grade));
			p.Add(db.CreateParameter("ENT_QualificationCertificateNo",DbType.String, _ApplyFirstMDL.ENT_QualificationCertificateNo));
			p.Add(db.CreateParameter("ZhuanYe",DbType.String, _ApplyFirstMDL.ZhuanYe));
			p.Add(db.CreateParameter("GetType",DbType.String, _ApplyFirstMDL.GetType));
			p.Add(db.CreateParameter("PSN_ExamCertCode",DbType.String, _ApplyFirstMDL.PSN_ExamCertCode));
			p.Add(db.CreateParameter("ConferDate",DbType.DateTime, _ApplyFirstMDL.ConferDate));
			p.Add(db.CreateParameter("ApplyRegisteProfession",DbType.String, _ApplyFirstMDL.ApplyRegisteProfession));
			p.Add(db.CreateParameter("BiXiu",DbType.Int32, _ApplyFirstMDL.BiXiu));
			p.Add(db.CreateParameter("XuanXiu",DbType.Int32, _ApplyFirstMDL.XuanXiu));
			p.Add(db.CreateParameter("ExamInfo",DbType.String, _ApplyFirstMDL.ExamInfo));
			p.Add(db.CreateParameter("OtherCert",DbType.String, _ApplyFirstMDL.OtherCert));
			p.Add(db.CreateParameter("IfSameUnit",DbType.Boolean, _ApplyFirstMDL.IfSameUnit));
			p.Add(db.CreateParameter("MainJob",DbType.String, _ApplyFirstMDL.MainJob));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="ApplyFirstID">主键</param>
		/// <returns></returns>
        public static int Delete( string ApplyID )
		{
			return Delete(null, ApplyID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="ApplyFirstID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, string ApplyID)
		{
			string sql=@"DELETE FROM dbo.ApplyFirst WHERE ApplyID = @ApplyID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ApplyID",DbType.String,ApplyID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="ApplyFirstMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(ApplyFirstMDL _ApplyFirstMDL)
		{
			return Delete(null,_ApplyFirstMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ApplyFirstMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,ApplyFirstMDL _ApplyFirstMDL)
		{
			string sql=@"DELETE FROM dbo.ApplyFirst WHERE ApplyID = @ApplyID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ApplyID",DbType.String,_ApplyFirstMDL.ApplyID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ApplyFirstID">主键</param>
        public static ApplyFirstMDL GetObject( string ApplyID )
		{
            string sql = @"
			SELECT ApplyID,ArchitectType,PSN_Telephone,PSN_MobilePhone,PSN_Email,Nation,Birthday,School,Major,GraduationTime,XueLi,XueWei,FR,LinkMan,ENT_Telephone,ENT_Correspondence,ENT_Economic_Nature,END_Addess,ENT_Postcode,ENT_Type,ENT_Sort,ENT_Grade,ENT_QualificationCertificateNo,ZhuanYe,GetType,PSN_ExamCertCode,ConferDate,ApplyRegisteProfession,BiXiu,XuanXiu,ExamInfo,OtherCert,IfSameUnit,MainJob
			FROM dbo.ApplyFirst
			WHERE ApplyID = @ApplyID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, ApplyID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                ApplyFirstMDL _ApplyFirstMDL = null;
                if (reader.Read())
                {
                    _ApplyFirstMDL = new ApplyFirstMDL();
					if (reader["ApplyID"] != DBNull.Value) _ApplyFirstMDL.ApplyID = Convert.ToString(reader["ApplyID"]);
                    if (reader["ArchitectType"] != DBNull.Value) _ApplyFirstMDL.ArchitectType = Convert.ToString(reader["ArchitectType"]);
					if (reader["PSN_Telephone"] != DBNull.Value) _ApplyFirstMDL.PSN_Telephone = Convert.ToString(reader["PSN_Telephone"]);
					if (reader["PSN_MobilePhone"] != DBNull.Value) _ApplyFirstMDL.PSN_MobilePhone = Convert.ToString(reader["PSN_MobilePhone"]);
					if (reader["PSN_Email"] != DBNull.Value) _ApplyFirstMDL.PSN_Email = Convert.ToString(reader["PSN_Email"]);
					if (reader["Nation"] != DBNull.Value) _ApplyFirstMDL.Nation = Convert.ToString(reader["Nation"]);
					if (reader["Birthday"] != DBNull.Value) _ApplyFirstMDL.Birthday = Convert.ToDateTime(reader["Birthday"]);
					if (reader["School"] != DBNull.Value) _ApplyFirstMDL.School = Convert.ToString(reader["School"]);
					if (reader["Major"] != DBNull.Value) _ApplyFirstMDL.Major = Convert.ToString(reader["Major"]);
					if (reader["GraduationTime"] != DBNull.Value) _ApplyFirstMDL.GraduationTime = Convert.ToDateTime(reader["GraduationTime"]);
					if (reader["XueLi"] != DBNull.Value) _ApplyFirstMDL.XueLi = Convert.ToString(reader["XueLi"]);
					if (reader["XueWei"] != DBNull.Value) _ApplyFirstMDL.XueWei = Convert.ToString(reader["XueWei"]);
					if (reader["FR"] != DBNull.Value) _ApplyFirstMDL.FR = Convert.ToString(reader["FR"]);
					if (reader["LinkMan"] != DBNull.Value) _ApplyFirstMDL.LinkMan = Convert.ToString(reader["LinkMan"]);
					if (reader["ENT_Telephone"] != DBNull.Value) _ApplyFirstMDL.ENT_Telephone = Convert.ToString(reader["ENT_Telephone"]);
					if (reader["ENT_Correspondence"] != DBNull.Value) _ApplyFirstMDL.ENT_Correspondence = Convert.ToString(reader["ENT_Correspondence"]);
					if (reader["ENT_Economic_Nature"] != DBNull.Value) _ApplyFirstMDL.ENT_Economic_Nature = Convert.ToString(reader["ENT_Economic_Nature"]);
					if (reader["END_Addess"] != DBNull.Value) _ApplyFirstMDL.END_Addess = Convert.ToString(reader["END_Addess"]);
					if (reader["ENT_Postcode"] != DBNull.Value) _ApplyFirstMDL.ENT_Postcode = Convert.ToString(reader["ENT_Postcode"]);
					if (reader["ENT_Type"] != DBNull.Value) _ApplyFirstMDL.ENT_Type = Convert.ToString(reader["ENT_Type"]);
					if (reader["ENT_Sort"] != DBNull.Value) _ApplyFirstMDL.ENT_Sort = Convert.ToString(reader["ENT_Sort"]);
					if (reader["ENT_Grade"] != DBNull.Value) _ApplyFirstMDL.ENT_Grade = Convert.ToString(reader["ENT_Grade"]);
					if (reader["ENT_QualificationCertificateNo"] != DBNull.Value) _ApplyFirstMDL.ENT_QualificationCertificateNo = Convert.ToString(reader["ENT_QualificationCertificateNo"]);
					if (reader["ZhuanYe"] != DBNull.Value) _ApplyFirstMDL.ZhuanYe = Convert.ToString(reader["ZhuanYe"]);
					if (reader["GetType"] != DBNull.Value) _ApplyFirstMDL.GetType = Convert.ToString(reader["GetType"]);
					if (reader["PSN_ExamCertCode"] != DBNull.Value) _ApplyFirstMDL.PSN_ExamCertCode = Convert.ToString(reader["PSN_ExamCertCode"]);
					if (reader["ConferDate"] != DBNull.Value) _ApplyFirstMDL.ConferDate = Convert.ToDateTime(reader["ConferDate"]);
					if (reader["ApplyRegisteProfession"] != DBNull.Value) _ApplyFirstMDL.ApplyRegisteProfession = Convert.ToString(reader["ApplyRegisteProfession"]);
					if (reader["BiXiu"] != DBNull.Value) _ApplyFirstMDL.BiXiu = Convert.ToInt32(reader["BiXiu"]);
					if (reader["XuanXiu"] != DBNull.Value) _ApplyFirstMDL.XuanXiu = Convert.ToInt32(reader["XuanXiu"]);
					if (reader["ExamInfo"] != DBNull.Value) _ApplyFirstMDL.ExamInfo = Convert.ToString(reader["ExamInfo"]);
					if (reader["OtherCert"] != DBNull.Value) _ApplyFirstMDL.OtherCert = Convert.ToString(reader["OtherCert"]);
					if (reader["IfSameUnit"] != DBNull.Value) _ApplyFirstMDL.IfSameUnit = Convert.ToBoolean(reader["IfSameUnit"]);
					if (reader["MainJob"] != DBNull.Value) _ApplyFirstMDL.MainJob = Convert.ToString(reader["MainJob"]);
                }
				reader.Close();
                db.Close();
                return _ApplyFirstMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.ApplyFirst", "*", filterWhereString, orderBy == "" ? " ApplyID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.ApplyFirst", filterWhereString);
        }
        
        #region 自定义方法
        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        /// <summary>
        public static DataTable GetListView(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.View_JZS_TOW_ApplyFirst", "*", filterWhereString, orderBy == "" ? " ENT_City,ENT_Name" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountView(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.View_JZS_TOW_ApplyFirst", filterWhereString);
        }
        #endregion
    }
}
