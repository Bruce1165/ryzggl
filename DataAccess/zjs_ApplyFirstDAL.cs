using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--zjs_ApplyFirstDAL(填写类描述)
	/// </summary>
    public class zjs_ApplyFirstDAL
    {
        public zjs_ApplyFirstDAL(){}

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_zjs_ApplyFirstMDL">对象实体类</param>
        /// <returns></returns>
        public static int Insert(zjs_ApplyFirstMDL _zjs_ApplyFirstMDL)
        {
            return Insert(null, _zjs_ApplyFirstMDL);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_zjs_ApplyFirstMDL">对象实体类</param>
        /// <returns></returns>
        public static int Insert(DbTransaction tran, zjs_ApplyFirstMDL _zjs_ApplyFirstMDL)
        {
            DBHelper db = new DBHelper();
            string sql = @"
			INSERT INTO dbo.zjs_ApplyFirst(ApplyID,PSN_Telephone,PSN_MobilePhone,PSN_Email,School,Major,GraduationTime,XueLi,XueWei,FR,LinkMan,ENT_Telephone,ENT_Correspondence,ENT_Economic_Nature,END_Addess,ENT_Postcode,ENT_Type,ENT_Sort,ENT_Grade,ENT_QualificationCertificateNo,ZhuanYe,GetType,PSN_ExamCertCode,PSN_RegisterNo,ConferDate,ApplyRegisteProfession,BiXiu,XuanXiu)
			VALUES (@ApplyID,@PSN_Telephone,@PSN_MobilePhone,@PSN_Email,@School,@Major,@GraduationTime,@XueLi,@XueWei,@FR,@LinkMan,@ENT_Telephone,@ENT_Correspondence,@ENT_Economic_Nature,@END_Addess,@ENT_Postcode,@ENT_Type,@ENT_Sort,@ENT_Grade,@ENT_QualificationCertificateNo,@ZhuanYe,@GetType,@PSN_ExamCertCode,@PSN_RegisterNo,@ConferDate,@ApplyRegisteProfession,@BiXiu,@XuanXiu)";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, _zjs_ApplyFirstMDL.ApplyID));
            p.Add(db.CreateParameter("PSN_Telephone", DbType.String, _zjs_ApplyFirstMDL.PSN_Telephone));
            p.Add(db.CreateParameter("PSN_MobilePhone", DbType.String, _zjs_ApplyFirstMDL.PSN_MobilePhone));
            p.Add(db.CreateParameter("PSN_Email", DbType.String, _zjs_ApplyFirstMDL.PSN_Email));
            p.Add(db.CreateParameter("School", DbType.String, _zjs_ApplyFirstMDL.School));
            p.Add(db.CreateParameter("Major", DbType.String, _zjs_ApplyFirstMDL.Major));
            p.Add(db.CreateParameter("GraduationTime", DbType.DateTime, _zjs_ApplyFirstMDL.GraduationTime));
            p.Add(db.CreateParameter("XueLi", DbType.String, _zjs_ApplyFirstMDL.XueLi));
            p.Add(db.CreateParameter("XueWei", DbType.String, _zjs_ApplyFirstMDL.XueWei));
            p.Add(db.CreateParameter("FR", DbType.String, _zjs_ApplyFirstMDL.FR));
            p.Add(db.CreateParameter("LinkMan", DbType.String, _zjs_ApplyFirstMDL.LinkMan));
            p.Add(db.CreateParameter("ENT_Telephone", DbType.String, _zjs_ApplyFirstMDL.ENT_Telephone));
            p.Add(db.CreateParameter("ENT_Correspondence", DbType.String, _zjs_ApplyFirstMDL.ENT_Correspondence));
            p.Add(db.CreateParameter("ENT_Economic_Nature", DbType.String, _zjs_ApplyFirstMDL.ENT_Economic_Nature));
            p.Add(db.CreateParameter("END_Addess", DbType.String, _zjs_ApplyFirstMDL.END_Addess));
            p.Add(db.CreateParameter("ENT_Postcode", DbType.String, _zjs_ApplyFirstMDL.ENT_Postcode));
            p.Add(db.CreateParameter("ENT_Type", DbType.String, _zjs_ApplyFirstMDL.ENT_Type));
            p.Add(db.CreateParameter("ENT_Sort", DbType.String, _zjs_ApplyFirstMDL.ENT_Sort));
            p.Add(db.CreateParameter("ENT_Grade", DbType.String, _zjs_ApplyFirstMDL.ENT_Grade));
            p.Add(db.CreateParameter("ENT_QualificationCertificateNo", DbType.String, _zjs_ApplyFirstMDL.ENT_QualificationCertificateNo));
            p.Add(db.CreateParameter("ZhuanYe", DbType.String, _zjs_ApplyFirstMDL.ZhuanYe));
            p.Add(db.CreateParameter("GetType", DbType.String, _zjs_ApplyFirstMDL.GetType));
            p.Add(db.CreateParameter("PSN_ExamCertCode", DbType.String, _zjs_ApplyFirstMDL.PSN_ExamCertCode));
            p.Add(db.CreateParameter("PSN_RegisterNo", DbType.String, _zjs_ApplyFirstMDL.PSN_RegisterNo));
            p.Add(db.CreateParameter("ConferDate", DbType.DateTime, _zjs_ApplyFirstMDL.ConferDate));
            p.Add(db.CreateParameter("ApplyRegisteProfession", DbType.String, _zjs_ApplyFirstMDL.ApplyRegisteProfession));
            p.Add(db.CreateParameter("BiXiu", DbType.Int32, _zjs_ApplyFirstMDL.BiXiu));
            p.Add(db.CreateParameter("XuanXiu", DbType.Int32, _zjs_ApplyFirstMDL.XuanXiu));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_zjs_ApplyFirstMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(zjs_ApplyFirstMDL _zjs_ApplyFirstMDL)
        {
            return Update(null, _zjs_ApplyFirstMDL);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_zjs_ApplyFirstMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, zjs_ApplyFirstMDL _zjs_ApplyFirstMDL)
        {
            string sql = @"
			UPDATE dbo.zjs_ApplyFirst
				SET	PSN_Telephone = @PSN_Telephone,PSN_MobilePhone = @PSN_MobilePhone,PSN_Email = @PSN_Email,School = @School,Major = @Major,GraduationTime = @GraduationTime,XueLi = @XueLi,XueWei = @XueWei,FR = @FR,LinkMan = @LinkMan,ENT_Telephone = @ENT_Telephone,ENT_Correspondence = @ENT_Correspondence,ENT_Economic_Nature = @ENT_Economic_Nature,END_Addess = @END_Addess,ENT_Postcode = @ENT_Postcode,ENT_Type = @ENT_Type,ENT_Sort = @ENT_Sort,ENT_Grade = @ENT_Grade,ENT_QualificationCertificateNo = @ENT_QualificationCertificateNo,ZhuanYe = @ZhuanYe,GetType = @GetType,PSN_ExamCertCode = @PSN_ExamCertCode,PSN_RegisterNo = @PSN_RegisterNo,ConferDate = @ConferDate,ApplyRegisteProfession = @ApplyRegisteProfession,BiXiu = @BiXiu,XuanXiu = @XuanXiu
			WHERE
				ApplyID = @ApplyID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, _zjs_ApplyFirstMDL.ApplyID));
            p.Add(db.CreateParameter("PSN_Telephone", DbType.String, _zjs_ApplyFirstMDL.PSN_Telephone));
            p.Add(db.CreateParameter("PSN_MobilePhone", DbType.String, _zjs_ApplyFirstMDL.PSN_MobilePhone));
            p.Add(db.CreateParameter("PSN_Email", DbType.String, _zjs_ApplyFirstMDL.PSN_Email));
            p.Add(db.CreateParameter("School", DbType.String, _zjs_ApplyFirstMDL.School));
            p.Add(db.CreateParameter("Major", DbType.String, _zjs_ApplyFirstMDL.Major));
            p.Add(db.CreateParameter("GraduationTime", DbType.DateTime, _zjs_ApplyFirstMDL.GraduationTime));
            p.Add(db.CreateParameter("XueLi", DbType.String, _zjs_ApplyFirstMDL.XueLi));
            p.Add(db.CreateParameter("XueWei", DbType.String, _zjs_ApplyFirstMDL.XueWei));
            p.Add(db.CreateParameter("FR", DbType.String, _zjs_ApplyFirstMDL.FR));
            p.Add(db.CreateParameter("LinkMan", DbType.String, _zjs_ApplyFirstMDL.LinkMan));
            p.Add(db.CreateParameter("ENT_Telephone", DbType.String, _zjs_ApplyFirstMDL.ENT_Telephone));
            p.Add(db.CreateParameter("ENT_Correspondence", DbType.String, _zjs_ApplyFirstMDL.ENT_Correspondence));
            p.Add(db.CreateParameter("ENT_Economic_Nature", DbType.String, _zjs_ApplyFirstMDL.ENT_Economic_Nature));
            p.Add(db.CreateParameter("END_Addess", DbType.String, _zjs_ApplyFirstMDL.END_Addess));
            p.Add(db.CreateParameter("ENT_Postcode", DbType.String, _zjs_ApplyFirstMDL.ENT_Postcode));
            p.Add(db.CreateParameter("ENT_Type", DbType.String, _zjs_ApplyFirstMDL.ENT_Type));
            p.Add(db.CreateParameter("ENT_Sort", DbType.String, _zjs_ApplyFirstMDL.ENT_Sort));
            p.Add(db.CreateParameter("ENT_Grade", DbType.String, _zjs_ApplyFirstMDL.ENT_Grade));
            p.Add(db.CreateParameter("ENT_QualificationCertificateNo", DbType.String, _zjs_ApplyFirstMDL.ENT_QualificationCertificateNo));
            p.Add(db.CreateParameter("ZhuanYe", DbType.String, _zjs_ApplyFirstMDL.ZhuanYe));
            p.Add(db.CreateParameter("GetType", DbType.String, _zjs_ApplyFirstMDL.GetType));
            p.Add(db.CreateParameter("PSN_ExamCertCode", DbType.String, _zjs_ApplyFirstMDL.PSN_ExamCertCode));
            p.Add(db.CreateParameter("PSN_RegisterNo", DbType.String, _zjs_ApplyFirstMDL.PSN_RegisterNo));
            p.Add(db.CreateParameter("ConferDate", DbType.DateTime, _zjs_ApplyFirstMDL.ConferDate));
            p.Add(db.CreateParameter("ApplyRegisteProfession", DbType.String, _zjs_ApplyFirstMDL.ApplyRegisteProfession));
            p.Add(db.CreateParameter("BiXiu", DbType.Int32, _zjs_ApplyFirstMDL.BiXiu));
            p.Add(db.CreateParameter("XuanXiu", DbType.Int32, _zjs_ApplyFirstMDL.XuanXiu));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="zjs_ApplyFirstID">主键</param>
        /// <returns></returns>
        public static int Delete(string ApplyID)
        {
            return Delete(null, ApplyID);
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="zjs_ApplyFirstID">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, string ApplyID)
        {
            string sql = @"DELETE FROM dbo.zjs_ApplyFirst WHERE ApplyID = @ApplyID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, ApplyID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_zjs_ApplyFirstMDL">对象实体类</param>
        /// <returns></returns>
        public static int Delete(zjs_ApplyFirstMDL _zjs_ApplyFirstMDL)
        {
            return Delete(null, _zjs_ApplyFirstMDL);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_zjs_ApplyFirstMDL">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, zjs_ApplyFirstMDL _zjs_ApplyFirstMDL)
        {
            string sql = @"DELETE FROM dbo.zjs_ApplyFirst WHERE ApplyID = @ApplyID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, _zjs_ApplyFirstMDL.ApplyID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="zjs_ApplyFirstID">主键</param>
        public static zjs_ApplyFirstMDL GetObject(string ApplyID)
        {
            string sql = @"
			SELECT ApplyID,PSN_Telephone,PSN_MobilePhone,PSN_Email,School,Major,GraduationTime,XueLi,XueWei,FR,LinkMan,ENT_Telephone,ENT_Correspondence,ENT_Economic_Nature,END_Addess,ENT_Postcode,ENT_Type,ENT_Sort,ENT_Grade,ENT_QualificationCertificateNo,ZhuanYe,GetType,PSN_ExamCertCode,PSN_RegisterNo,ConferDate,ApplyRegisteProfession,BiXiu,XuanXiu
			FROM dbo.zjs_ApplyFirst
			WHERE ApplyID = @ApplyID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, ApplyID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                zjs_ApplyFirstMDL _zjs_ApplyFirstMDL = null;
                if (reader.Read())
                {
                    _zjs_ApplyFirstMDL = new zjs_ApplyFirstMDL();
                    if (reader["ApplyID"] != DBNull.Value) _zjs_ApplyFirstMDL.ApplyID = Convert.ToString(reader["ApplyID"]);
                    if (reader["PSN_Telephone"] != DBNull.Value) _zjs_ApplyFirstMDL.PSN_Telephone = Convert.ToString(reader["PSN_Telephone"]);
                    if (reader["PSN_MobilePhone"] != DBNull.Value) _zjs_ApplyFirstMDL.PSN_MobilePhone = Convert.ToString(reader["PSN_MobilePhone"]);
                    if (reader["PSN_Email"] != DBNull.Value) _zjs_ApplyFirstMDL.PSN_Email = Convert.ToString(reader["PSN_Email"]);
                    if (reader["School"] != DBNull.Value) _zjs_ApplyFirstMDL.School = Convert.ToString(reader["School"]);
                    if (reader["Major"] != DBNull.Value) _zjs_ApplyFirstMDL.Major = Convert.ToString(reader["Major"]);
                    if (reader["GraduationTime"] != DBNull.Value) _zjs_ApplyFirstMDL.GraduationTime = Convert.ToDateTime(reader["GraduationTime"]);
                    if (reader["XueLi"] != DBNull.Value) _zjs_ApplyFirstMDL.XueLi = Convert.ToString(reader["XueLi"]);
                    if (reader["XueWei"] != DBNull.Value) _zjs_ApplyFirstMDL.XueWei = Convert.ToString(reader["XueWei"]);
                    if (reader["FR"] != DBNull.Value) _zjs_ApplyFirstMDL.FR = Convert.ToString(reader["FR"]);
                    if (reader["LinkMan"] != DBNull.Value) _zjs_ApplyFirstMDL.LinkMan = Convert.ToString(reader["LinkMan"]);
                    if (reader["ENT_Telephone"] != DBNull.Value) _zjs_ApplyFirstMDL.ENT_Telephone = Convert.ToString(reader["ENT_Telephone"]);
                    if (reader["ENT_Correspondence"] != DBNull.Value) _zjs_ApplyFirstMDL.ENT_Correspondence = Convert.ToString(reader["ENT_Correspondence"]);
                    if (reader["ENT_Economic_Nature"] != DBNull.Value) _zjs_ApplyFirstMDL.ENT_Economic_Nature = Convert.ToString(reader["ENT_Economic_Nature"]);
                    if (reader["END_Addess"] != DBNull.Value) _zjs_ApplyFirstMDL.END_Addess = Convert.ToString(reader["END_Addess"]);
                    if (reader["ENT_Postcode"] != DBNull.Value) _zjs_ApplyFirstMDL.ENT_Postcode = Convert.ToString(reader["ENT_Postcode"]);
                    if (reader["ENT_Type"] != DBNull.Value) _zjs_ApplyFirstMDL.ENT_Type = Convert.ToString(reader["ENT_Type"]);
                    if (reader["ENT_Sort"] != DBNull.Value) _zjs_ApplyFirstMDL.ENT_Sort = Convert.ToString(reader["ENT_Sort"]);
                    if (reader["ENT_Grade"] != DBNull.Value) _zjs_ApplyFirstMDL.ENT_Grade = Convert.ToString(reader["ENT_Grade"]);
                    if (reader["ENT_QualificationCertificateNo"] != DBNull.Value) _zjs_ApplyFirstMDL.ENT_QualificationCertificateNo = Convert.ToString(reader["ENT_QualificationCertificateNo"]);
                    if (reader["ZhuanYe"] != DBNull.Value) _zjs_ApplyFirstMDL.ZhuanYe = Convert.ToString(reader["ZhuanYe"]);
                    if (reader["GetType"] != DBNull.Value) _zjs_ApplyFirstMDL.GetType = Convert.ToString(reader["GetType"]);
                    if (reader["PSN_ExamCertCode"] != DBNull.Value) _zjs_ApplyFirstMDL.PSN_ExamCertCode = Convert.ToString(reader["PSN_ExamCertCode"]);
                    if (reader["PSN_RegisterNo"] != DBNull.Value) _zjs_ApplyFirstMDL.PSN_RegisterNo = Convert.ToString(reader["PSN_RegisterNo"]);
                    if (reader["ConferDate"] != DBNull.Value) _zjs_ApplyFirstMDL.ConferDate = Convert.ToDateTime(reader["ConferDate"]);
                    if (reader["ApplyRegisteProfession"] != DBNull.Value) _zjs_ApplyFirstMDL.ApplyRegisteProfession = Convert.ToString(reader["ApplyRegisteProfession"]);
                    if (reader["BiXiu"] != DBNull.Value) _zjs_ApplyFirstMDL.BiXiu = Convert.ToInt32(reader["BiXiu"]);
                    if (reader["XuanXiu"] != DBNull.Value) _zjs_ApplyFirstMDL.XuanXiu = Convert.ToInt32(reader["XuanXiu"]);
                }
                reader.Close();
                db.Close();
                return _zjs_ApplyFirstMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.zjs_ApplyFirst", "*", filterWhereString, orderBy == "" ? " ApplyID" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.zjs_ApplyFirst", filterWhereString);
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.View_ZJS_ApplyFirst", "*", filterWhereString, orderBy == "" ? " ApplyID desc" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountView(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.View_ZJS_ApplyFirst", filterWhereString);
        }
        #endregion
    }
}
