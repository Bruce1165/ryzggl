using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
    /// <summary>
    /// 业务类实现--EJCertUseDAL(填写类描述)
    /// </summary>
    public class EJCertUseDAL
    {
        public EJCertUseDAL() { }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_EJCertUseMDL">对象实体类</param>
        /// <returns></returns>
        public static int Insert(EJCertUseMDL _EJCertUseMDL)
        {
            return Insert(null, _EJCertUseMDL);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_EJCertUseMDL">对象实体类</param>
        /// <returns></returns>
        public static int Insert(DbTransaction tran, EJCertUseMDL _EJCertUseMDL)
        {
            DBHelper db = new DBHelper();
            string sql = @"
			INSERT INTO dbo.EJCertUse(CertificateCAID,PSN_ServerID,BeginTime,EndTime,CJSJ,[VALID],ApplyCATime,Pdf_SignCATime,Pdf_license_code,Pdf_auth_code,Pdf_ReturnCATime,ZZBS,ENT_ServerID,ENT_Name,ENT_OrganizationsCode,PSN_Name,PSN_Sex,PSN_BirthDate,PSN_CertificateType,PSN_CertificateNO,PSN_RegisteType,PSN_RegisterNO,PSN_RegisterCertificateNo,PSN_RegisteProfession,PSN_CertificationDate,PSN_CertificateValidity,PSN_RegistePermissionDate,Ofd_SignCATime,Ofd_license_code,Ofd_auth_code,Ofd_ReturnCATime,ZGZSBH)
			VALUES (@CertificateCAID,@PSN_ServerID,@BeginTime,@EndTime,@CJSJ,@Valid,@ApplyCATime,@Pdf_SignCATime,@Pdf_license_code,@Pdf_auth_code,@Pdf_ReturnCATime,@ZZBS,@ENT_ServerID,@ENT_Name,@ENT_OrganizationsCode,@PSN_Name,@PSN_Sex,@PSN_BirthDate,@PSN_CertificateType,@PSN_CertificateNO,@PSN_RegisteType,@PSN_RegisterNO,@PSN_RegisterCertificateNo,@PSN_RegisteProfession,@PSN_CertificationDate,@PSN_CertificateValidity,@PSN_RegistePermissionDate,@Ofd_SignCATime,@Ofd_license_code,@Ofd_auth_code,@Ofd_ReturnCATime,@ZGZSBH)";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("CertificateCAID", DbType.String, _EJCertUseMDL.CertificateCAID));
            p.Add(db.CreateParameter("PSN_ServerID", DbType.String, _EJCertUseMDL.PSN_ServerID));
            p.Add(db.CreateParameter("BeginTime", DbType.DateTime, _EJCertUseMDL.BeginTime));
            p.Add(db.CreateParameter("EndTime", DbType.DateTime, _EJCertUseMDL.EndTime));
            p.Add(db.CreateParameter("CJSJ", DbType.DateTime, _EJCertUseMDL.CJSJ));
            p.Add(db.CreateParameter("Valid", DbType.Int32, _EJCertUseMDL.Valid));
            p.Add(db.CreateParameter("ApplyCATime", DbType.DateTime, _EJCertUseMDL.ApplyCATime));
            p.Add(db.CreateParameter("Pdf_SignCATime", DbType.DateTime, _EJCertUseMDL.Pdf_SignCATime));
            p.Add(db.CreateParameter("Pdf_license_code", DbType.String, _EJCertUseMDL.Pdf_license_code));
            p.Add(db.CreateParameter("Pdf_auth_code", DbType.String, _EJCertUseMDL.Pdf_auth_code));
            p.Add(db.CreateParameter("Pdf_ReturnCATime", DbType.DateTime, _EJCertUseMDL.Pdf_ReturnCATime));
            p.Add(db.CreateParameter("ZZBS", DbType.String, _EJCertUseMDL.ZZBS));
            p.Add(db.CreateParameter("ENT_ServerID", DbType.String, _EJCertUseMDL.ENT_ServerID));
            p.Add(db.CreateParameter("ENT_Name", DbType.String, _EJCertUseMDL.ENT_Name));
            p.Add(db.CreateParameter("ENT_OrganizationsCode", DbType.String, _EJCertUseMDL.ENT_OrganizationsCode));
            p.Add(db.CreateParameter("PSN_Name", DbType.String, _EJCertUseMDL.PSN_Name));
            p.Add(db.CreateParameter("PSN_Sex", DbType.String, _EJCertUseMDL.PSN_Sex));
            p.Add(db.CreateParameter("PSN_BirthDate", DbType.DateTime, _EJCertUseMDL.PSN_BirthDate));
            p.Add(db.CreateParameter("PSN_CertificateType", DbType.String, _EJCertUseMDL.PSN_CertificateType));
            p.Add(db.CreateParameter("PSN_CertificateNO", DbType.String, _EJCertUseMDL.PSN_CertificateNO));
            p.Add(db.CreateParameter("PSN_RegisteType", DbType.String, _EJCertUseMDL.PSN_RegisteType));
            p.Add(db.CreateParameter("PSN_RegisterNO", DbType.String, _EJCertUseMDL.PSN_RegisterNO));
            p.Add(db.CreateParameter("PSN_RegisterCertificateNo", DbType.String, _EJCertUseMDL.PSN_RegisterCertificateNo));
            p.Add(db.CreateParameter("PSN_RegisteProfession", DbType.String, _EJCertUseMDL.PSN_RegisteProfession));
            p.Add(db.CreateParameter("PSN_CertificationDate", DbType.DateTime, _EJCertUseMDL.PSN_CertificationDate));
            p.Add(db.CreateParameter("PSN_CertificateValidity", DbType.DateTime, _EJCertUseMDL.PSN_CertificateValidity));
            p.Add(db.CreateParameter("PSN_RegistePermissionDate", DbType.DateTime, _EJCertUseMDL.PSN_RegistePermissionDate));
            p.Add(db.CreateParameter("Ofd_SignCATime", DbType.DateTime, _EJCertUseMDL.Ofd_SignCATime));
            p.Add(db.CreateParameter("Ofd_license_code", DbType.String, _EJCertUseMDL.Ofd_license_code));
            p.Add(db.CreateParameter("Ofd_auth_code", DbType.String, _EJCertUseMDL.Ofd_auth_code));
            p.Add(db.CreateParameter("Ofd_ReturnCATime", DbType.DateTime, _EJCertUseMDL.Ofd_ReturnCATime));
            p.Add(db.CreateParameter("ZGZSBH", DbType.String, _EJCertUseMDL.ZGZSBH));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_EJCertUseMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(EJCertUseMDL _EJCertUseMDL)
        {
            return Update(null, _EJCertUseMDL);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_EJCertUseMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, EJCertUseMDL _EJCertUseMDL)
        {
            string sql = @"
			UPDATE dbo.EJCertUse
				SET	PSN_ServerID = @PSN_ServerID,BeginTime = @BeginTime,EndTime = @EndTime,CJSJ = @CJSJ,""VALID"" = @Valid,ApplyCATime = @ApplyCATime,Pdf_SignCATime = @Pdf_SignCATime,Pdf_license_code = @Pdf_license_code,Pdf_auth_code = @Pdf_auth_code,Pdf_ReturnCATime = @Pdf_ReturnCATime,ZZBS = @ZZBS,ENT_ServerID = @ENT_ServerID,ENT_Name = @ENT_Name,ENT_OrganizationsCode = @ENT_OrganizationsCode,PSN_Name = @PSN_Name,PSN_Sex = @PSN_Sex,PSN_BirthDate = @PSN_BirthDate,PSN_CertificateType = @PSN_CertificateType,PSN_CertificateNO = @PSN_CertificateNO,PSN_RegisteType = @PSN_RegisteType,PSN_RegisterNO = @PSN_RegisterNO,PSN_RegisterCertificateNo = @PSN_RegisterCertificateNo,PSN_RegisteProfession = @PSN_RegisteProfession,PSN_CertificationDate = @PSN_CertificationDate,PSN_CertificateValidity = @PSN_CertificateValidity,PSN_RegistePermissionDate = @PSN_RegistePermissionDate,Ofd_SignCATime = @Ofd_SignCATime,Ofd_license_code = @Ofd_license_code,Ofd_auth_code = @Ofd_auth_code,Ofd_ReturnCATime = @Ofd_ReturnCATime,ZGZSBH = @ZGZSBH
			WHERE
				CertificateCAID = @CertificateCAID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("CertificateCAID", DbType.String, _EJCertUseMDL.CertificateCAID));
            p.Add(db.CreateParameter("PSN_ServerID", DbType.String, _EJCertUseMDL.PSN_ServerID));
            p.Add(db.CreateParameter("BeginTime", DbType.DateTime, _EJCertUseMDL.BeginTime));
            p.Add(db.CreateParameter("EndTime", DbType.DateTime, _EJCertUseMDL.EndTime));
            p.Add(db.CreateParameter("CJSJ", DbType.DateTime, _EJCertUseMDL.CJSJ));
            p.Add(db.CreateParameter("Valid", DbType.Int32, _EJCertUseMDL.Valid));
            p.Add(db.CreateParameter("ApplyCATime", DbType.DateTime, _EJCertUseMDL.ApplyCATime));
            p.Add(db.CreateParameter("Pdf_SignCATime", DbType.DateTime, _EJCertUseMDL.Pdf_SignCATime));
            p.Add(db.CreateParameter("Pdf_license_code", DbType.String, _EJCertUseMDL.Pdf_license_code));
            p.Add(db.CreateParameter("Pdf_auth_code", DbType.String, _EJCertUseMDL.Pdf_auth_code));
            p.Add(db.CreateParameter("Pdf_ReturnCATime", DbType.DateTime, _EJCertUseMDL.Pdf_ReturnCATime));
            p.Add(db.CreateParameter("ZZBS", DbType.String, _EJCertUseMDL.ZZBS));
            p.Add(db.CreateParameter("ENT_ServerID", DbType.String, _EJCertUseMDL.ENT_ServerID));
            p.Add(db.CreateParameter("ENT_Name", DbType.String, _EJCertUseMDL.ENT_Name));
            p.Add(db.CreateParameter("ENT_OrganizationsCode", DbType.String, _EJCertUseMDL.ENT_OrganizationsCode));
            p.Add(db.CreateParameter("PSN_Name", DbType.String, _EJCertUseMDL.PSN_Name));
            p.Add(db.CreateParameter("PSN_Sex", DbType.String, _EJCertUseMDL.PSN_Sex));
            p.Add(db.CreateParameter("PSN_BirthDate", DbType.DateTime, _EJCertUseMDL.PSN_BirthDate));
            p.Add(db.CreateParameter("PSN_CertificateType", DbType.String, _EJCertUseMDL.PSN_CertificateType));
            p.Add(db.CreateParameter("PSN_CertificateNO", DbType.String, _EJCertUseMDL.PSN_CertificateNO));
            p.Add(db.CreateParameter("PSN_RegisteType", DbType.String, _EJCertUseMDL.PSN_RegisteType));
            p.Add(db.CreateParameter("PSN_RegisterNO", DbType.String, _EJCertUseMDL.PSN_RegisterNO));
            p.Add(db.CreateParameter("PSN_RegisterCertificateNo", DbType.String, _EJCertUseMDL.PSN_RegisterCertificateNo));
            p.Add(db.CreateParameter("PSN_RegisteProfession", DbType.String, _EJCertUseMDL.PSN_RegisteProfession));
            p.Add(db.CreateParameter("PSN_CertificationDate", DbType.DateTime, _EJCertUseMDL.PSN_CertificationDate));
            p.Add(db.CreateParameter("PSN_CertificateValidity", DbType.DateTime, _EJCertUseMDL.PSN_CertificateValidity));
            p.Add(db.CreateParameter("PSN_RegistePermissionDate", DbType.DateTime, _EJCertUseMDL.PSN_RegistePermissionDate));
            p.Add(db.CreateParameter("Ofd_SignCATime", DbType.DateTime, _EJCertUseMDL.Ofd_SignCATime));
            p.Add(db.CreateParameter("Ofd_license_code", DbType.String, _EJCertUseMDL.Ofd_license_code));
            p.Add(db.CreateParameter("Ofd_auth_code", DbType.String, _EJCertUseMDL.Ofd_auth_code));
            p.Add(db.CreateParameter("Ofd_ReturnCATime", DbType.DateTime, _EJCertUseMDL.Ofd_ReturnCATime));
            p.Add(db.CreateParameter("ZGZSBH", DbType.String, _EJCertUseMDL.ZGZSBH));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="EJCertUseID">主键</param>
        /// <returns></returns>
        public static int Delete(string CertificateCAID)
        {
            return Delete(null, CertificateCAID);
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="EJCertUseID">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, string CertificateCAID)
        {
            string sql = @"DELETE FROM dbo.EJCertUse WHERE CertificateCAID = @CertificateCAID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("CertificateCAID", DbType.String, CertificateCAID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="_EJCertUseMDL">对象实体类</param>
        /// <returns></returns>
        public static int Delete(EJCertUseMDL _EJCertUseMDL)
        {
            return Delete(null, _EJCertUseMDL);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_EJCertUseMDL">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, EJCertUseMDL _EJCertUseMDL)
        {
            string sql = @"DELETE FROM dbo.EJCertUse WHERE CertificateCAID = @CertificateCAID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("CertificateCAID", DbType.String, _EJCertUseMDL.CertificateCAID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="EJCertUseID">主键</param>
        public static EJCertUseMDL GetObject(string CertificateCAID)
        {
            string sql = @"
			SELECT CertificateCAID,PSN_ServerID,BeginTime,EndTime,CJSJ,[VALID],ApplyCATime,Pdf_SignCATime,Pdf_license_code,Pdf_auth_code,Pdf_ReturnCATime,ZZBS,ENT_ServerID,ENT_Name,ENT_OrganizationsCode,PSN_Name,PSN_Sex,PSN_BirthDate,PSN_CertificateType,PSN_CertificateNO,PSN_RegisteType,PSN_RegisterNO,PSN_RegisterCertificateNo,PSN_RegisteProfession,PSN_CertificationDate,PSN_CertificateValidity,PSN_RegistePermissionDate,Ofd_SignCATime,Ofd_license_code,Ofd_auth_code,Ofd_ReturnCATime,ZGZSBH
			FROM dbo.EJCertUse
			WHERE CertificateCAID = @CertificateCAID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("CertificateCAID", DbType.String, CertificateCAID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                EJCertUseMDL _EJCertUseMDL = null;
                if (reader.Read())
                {
                    _EJCertUseMDL = new EJCertUseMDL();
                    if (reader["CertificateCAID"] != DBNull.Value) _EJCertUseMDL.CertificateCAID = Convert.ToString(reader["CertificateCAID"]);
                    if (reader["PSN_ServerID"] != DBNull.Value) _EJCertUseMDL.PSN_ServerID = Convert.ToString(reader["PSN_ServerID"]);
                    if (reader["BeginTime"] != DBNull.Value) _EJCertUseMDL.BeginTime = Convert.ToDateTime(reader["BeginTime"]);
                    if (reader["EndTime"] != DBNull.Value) _EJCertUseMDL.EndTime = Convert.ToDateTime(reader["EndTime"]);
                    if (reader["CJSJ"] != DBNull.Value) _EJCertUseMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
                    if (reader["Valid"] != DBNull.Value) _EJCertUseMDL.Valid = Convert.ToInt32(reader["Valid"]);
                    if (reader["ApplyCATime"] != DBNull.Value) _EJCertUseMDL.ApplyCATime = Convert.ToDateTime(reader["ApplyCATime"]);
                    if (reader["Pdf_SignCATime"] != DBNull.Value) _EJCertUseMDL.Pdf_SignCATime = Convert.ToDateTime(reader["Pdf_SignCATime"]);
                    if (reader["Pdf_license_code"] != DBNull.Value) _EJCertUseMDL.Pdf_license_code = Convert.ToString(reader["Pdf_license_code"]);
                    if (reader["Pdf_auth_code"] != DBNull.Value) _EJCertUseMDL.Pdf_auth_code = Convert.ToString(reader["Pdf_auth_code"]);
                    if (reader["Pdf_ReturnCATime"] != DBNull.Value) _EJCertUseMDL.Pdf_ReturnCATime = Convert.ToDateTime(reader["Pdf_ReturnCATime"]);
                    if (reader["ZZBS"] != DBNull.Value) _EJCertUseMDL.ZZBS = Convert.ToString(reader["ZZBS"]);
                    if (reader["ENT_ServerID"] != DBNull.Value) _EJCertUseMDL.ENT_ServerID = Convert.ToString(reader["ENT_ServerID"]);
                    if (reader["ENT_Name"] != DBNull.Value) _EJCertUseMDL.ENT_Name = Convert.ToString(reader["ENT_Name"]);
                    if (reader["ENT_OrganizationsCode"] != DBNull.Value) _EJCertUseMDL.ENT_OrganizationsCode = Convert.ToString(reader["ENT_OrganizationsCode"]);
                    if (reader["PSN_Name"] != DBNull.Value) _EJCertUseMDL.PSN_Name = Convert.ToString(reader["PSN_Name"]);
                    if (reader["PSN_Sex"] != DBNull.Value) _EJCertUseMDL.PSN_Sex = Convert.ToString(reader["PSN_Sex"]);
                    if (reader["PSN_BirthDate"] != DBNull.Value) _EJCertUseMDL.PSN_BirthDate = Convert.ToDateTime(reader["PSN_BirthDate"]);
                    if (reader["PSN_CertificateType"] != DBNull.Value) _EJCertUseMDL.PSN_CertificateType = Convert.ToString(reader["PSN_CertificateType"]);
                    if (reader["PSN_CertificateNO"] != DBNull.Value) _EJCertUseMDL.PSN_CertificateNO = Convert.ToString(reader["PSN_CertificateNO"]);
                    if (reader["PSN_RegisteType"] != DBNull.Value) _EJCertUseMDL.PSN_RegisteType = Convert.ToString(reader["PSN_RegisteType"]);
                    if (reader["PSN_RegisterNO"] != DBNull.Value) _EJCertUseMDL.PSN_RegisterNO = Convert.ToString(reader["PSN_RegisterNO"]);
                    if (reader["PSN_RegisterCertificateNo"] != DBNull.Value) _EJCertUseMDL.PSN_RegisterCertificateNo = Convert.ToString(reader["PSN_RegisterCertificateNo"]);
                    if (reader["PSN_RegisteProfession"] != DBNull.Value) _EJCertUseMDL.PSN_RegisteProfession = Convert.ToString(reader["PSN_RegisteProfession"]);
                    if (reader["PSN_CertificationDate"] != DBNull.Value) _EJCertUseMDL.PSN_CertificationDate = Convert.ToDateTime(reader["PSN_CertificationDate"]);
                    if (reader["PSN_CertificateValidity"] != DBNull.Value) _EJCertUseMDL.PSN_CertificateValidity = Convert.ToDateTime(reader["PSN_CertificateValidity"]);
                    if (reader["PSN_RegistePermissionDate"] != DBNull.Value) _EJCertUseMDL.PSN_RegistePermissionDate = Convert.ToDateTime(reader["PSN_RegistePermissionDate"]);
                    if (reader["Ofd_SignCATime"] != DBNull.Value) _EJCertUseMDL.Ofd_SignCATime = Convert.ToDateTime(reader["Ofd_SignCATime"]);
                    if (reader["Ofd_license_code"] != DBNull.Value) _EJCertUseMDL.Ofd_license_code = Convert.ToString(reader["Ofd_license_code"]);
                    if (reader["Ofd_auth_code"] != DBNull.Value) _EJCertUseMDL.Ofd_auth_code = Convert.ToString(reader["Ofd_auth_code"]);
                    if (reader["Ofd_ReturnCATime"] != DBNull.Value) _EJCertUseMDL.Ofd_ReturnCATime = Convert.ToDateTime(reader["Ofd_ReturnCATime"]);
                    if (reader["ZGZSBH"] != DBNull.Value) _EJCertUseMDL.ZGZSBH = Convert.ToString(reader["ZGZSBH"]);
                }
                reader.Close();
                db.Close();
                return _EJCertUseMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.EJCertUse", "*", filterWhereString, orderBy == "" ? " cjsj desc" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.EJCertUse", filterWhereString);
        }

        #region 自定义方法

        /// <summary>
        /// 获取当前有效的使用件
        /// </summary>
        /// <param name="PSN_ServerID">二建证书ID</param>
        /// <returns></returns>
        public static EJCertUseMDL GetCurrentUse(string PSN_ServerID)
        {
            string sql = string.Format(@"
			SELECT top 1 CertificateCAID,PSN_ServerID,BeginTime,EndTime,CJSJ,[VALID],ApplyCATime,Pdf_SignCATime,Pdf_license_code,Pdf_auth_code,Pdf_ReturnCATime,ZZBS,ENT_ServerID,ENT_Name,ENT_OrganizationsCode,PSN_Name,PSN_Sex,PSN_BirthDate,PSN_CertificateType,PSN_CertificateNO,PSN_RegisteType,PSN_RegisterNO,PSN_RegisterCertificateNo,PSN_RegisteProfession,PSN_CertificationDate,PSN_CertificateValidity,PSN_RegistePermissionDate,Ofd_SignCATime,Ofd_license_code,Ofd_auth_code,Ofd_ReturnCATime,ZGZSBH
			FROM dbo.EJCertUse
			WHERE PSN_ServerID = @PSN_ServerID and [VALID]=1 and EndTime >='{0}'
            order by CJSJ desc",DateTime.Now.ToString("yyyy-MM-dd"));

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PSN_ServerID", DbType.String, PSN_ServerID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                EJCertUseMDL _EJCertUseMDL = null;
                if (reader.Read())
                {
                    _EJCertUseMDL = new EJCertUseMDL();
                    if (reader["CertificateCAID"] != DBNull.Value) _EJCertUseMDL.CertificateCAID = Convert.ToString(reader["CertificateCAID"]);
                    if (reader["PSN_ServerID"] != DBNull.Value) _EJCertUseMDL.PSN_ServerID = Convert.ToString(reader["PSN_ServerID"]);
                    if (reader["BeginTime"] != DBNull.Value) _EJCertUseMDL.BeginTime = Convert.ToDateTime(reader["BeginTime"]);
                    if (reader["EndTime"] != DBNull.Value) _EJCertUseMDL.EndTime = Convert.ToDateTime(reader["EndTime"]);
                    if (reader["CJSJ"] != DBNull.Value) _EJCertUseMDL.CJSJ = Convert.ToDateTime(reader["CJSJ"]);
                    if (reader["Valid"] != DBNull.Value) _EJCertUseMDL.Valid = Convert.ToInt32(reader["Valid"]);
                    if (reader["ApplyCATime"] != DBNull.Value) _EJCertUseMDL.ApplyCATime = Convert.ToDateTime(reader["ApplyCATime"]);
                    if (reader["Pdf_SignCATime"] != DBNull.Value) _EJCertUseMDL.Pdf_SignCATime = Convert.ToDateTime(reader["Pdf_SignCATime"]);
                    if (reader["Pdf_license_code"] != DBNull.Value) _EJCertUseMDL.Pdf_license_code = Convert.ToString(reader["Pdf_license_code"]);
                    if (reader["Pdf_auth_code"] != DBNull.Value) _EJCertUseMDL.Pdf_auth_code = Convert.ToString(reader["Pdf_auth_code"]);
                    if (reader["Pdf_ReturnCATime"] != DBNull.Value) _EJCertUseMDL.Pdf_ReturnCATime = Convert.ToDateTime(reader["Pdf_ReturnCATime"]);
                    if (reader["ZZBS"] != DBNull.Value) _EJCertUseMDL.ZZBS = Convert.ToString(reader["ZZBS"]);
                    if (reader["ENT_ServerID"] != DBNull.Value) _EJCertUseMDL.ENT_ServerID = Convert.ToString(reader["ENT_ServerID"]);
                    if (reader["ENT_Name"] != DBNull.Value) _EJCertUseMDL.ENT_Name = Convert.ToString(reader["ENT_Name"]);
                    if (reader["ENT_OrganizationsCode"] != DBNull.Value) _EJCertUseMDL.ENT_OrganizationsCode = Convert.ToString(reader["ENT_OrganizationsCode"]);
                    if (reader["PSN_Name"] != DBNull.Value) _EJCertUseMDL.PSN_Name = Convert.ToString(reader["PSN_Name"]);
                    if (reader["PSN_Sex"] != DBNull.Value) _EJCertUseMDL.PSN_Sex = Convert.ToString(reader["PSN_Sex"]);
                    if (reader["PSN_BirthDate"] != DBNull.Value) _EJCertUseMDL.PSN_BirthDate = Convert.ToDateTime(reader["PSN_BirthDate"]);
                    if (reader["PSN_CertificateType"] != DBNull.Value) _EJCertUseMDL.PSN_CertificateType = Convert.ToString(reader["PSN_CertificateType"]);
                    if (reader["PSN_CertificateNO"] != DBNull.Value) _EJCertUseMDL.PSN_CertificateNO = Convert.ToString(reader["PSN_CertificateNO"]);
                    if (reader["PSN_RegisteType"] != DBNull.Value) _EJCertUseMDL.PSN_RegisteType = Convert.ToString(reader["PSN_RegisteType"]);
                    if (reader["PSN_RegisterNO"] != DBNull.Value) _EJCertUseMDL.PSN_RegisterNO = Convert.ToString(reader["PSN_RegisterNO"]);
                    if (reader["PSN_RegisterCertificateNo"] != DBNull.Value) _EJCertUseMDL.PSN_RegisterCertificateNo = Convert.ToString(reader["PSN_RegisterCertificateNo"]);
                    if (reader["PSN_RegisteProfession"] != DBNull.Value) _EJCertUseMDL.PSN_RegisteProfession = Convert.ToString(reader["PSN_RegisteProfession"]);
                    if (reader["PSN_CertificationDate"] != DBNull.Value) _EJCertUseMDL.PSN_CertificationDate = Convert.ToDateTime(reader["PSN_CertificationDate"]);
                    if (reader["PSN_CertificateValidity"] != DBNull.Value) _EJCertUseMDL.PSN_CertificateValidity = Convert.ToDateTime(reader["PSN_CertificateValidity"]);
                    if (reader["PSN_RegistePermissionDate"] != DBNull.Value) _EJCertUseMDL.PSN_RegistePermissionDate = Convert.ToDateTime(reader["PSN_RegistePermissionDate"]);
                    if (reader["Ofd_SignCATime"] != DBNull.Value) _EJCertUseMDL.Ofd_SignCATime = Convert.ToDateTime(reader["Ofd_SignCATime"]);
                    if (reader["Ofd_license_code"] != DBNull.Value) _EJCertUseMDL.Ofd_license_code = Convert.ToString(reader["Ofd_license_code"]);
                    if (reader["Ofd_auth_code"] != DBNull.Value) _EJCertUseMDL.Ofd_auth_code = Convert.ToString(reader["Ofd_auth_code"]);
                    if (reader["Ofd_ReturnCATime"] != DBNull.Value) _EJCertUseMDL.Ofd_ReturnCATime = Convert.ToDateTime(reader["Ofd_ReturnCATime"]);
                    if (reader["ZGZSBH"] != DBNull.Value) _EJCertUseMDL.ZGZSBH = Convert.ToString(reader["ZGZSBH"]);
                }
                reader.Close();
                db.Close();
                return _EJCertUseMDL;
            }
        }

        #endregion
    }
}
