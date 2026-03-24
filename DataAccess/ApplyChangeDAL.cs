using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;


namespace DataAccess
{
    /// <summary>
    /// 业务类实现--ApplyChangeDAL(填写类描述)
    /// </summary>
    public class ApplyChangeDAL
    {
        public ApplyChangeDAL() { }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_ApplyChangeMDL">对象实体类</param>
        /// <returns></returns>
        public static int Insert(ApplyChangeMDL _ApplyChangeMDL)
        {
            return Insert(null, _ApplyChangeMDL);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_ApplyChangeMDL">对象实体类</param>
        /// <returns></returns>
        public static int Insert(DbTransaction tran, ApplyChangeMDL _ApplyChangeMDL)
        {
            DBHelper db = new DBHelper();
            string sql = @"
			INSERT INTO dbo.ApplyChange(ApplyID,PSN_MobilePhone,PSN_Email,PSN_RegisterNo,ValidDate,ChangeReason,OldENT_Name,OldLinkMan,OldENT_Telephone,OldENT_Correspondence,OldENT_Postcode,ENT_Name,FR,LinkMan,ENT_Telephone,ENT_Correspondence,ENT_Postcode,END_Addess,IfOutside,ENT_Type,ENT_Economic_Nature,ENT_Sort,ENT_Grade,ENT_QualificationCertificateNo,ENT_Sort2,ENT_Grade2,ENT_QualificationCertificateNo2,ENT_NameFrom,ENT_NameTo,PSN_NameFrom,PSN_NameTo,FromENT_City,ToENT_City,FromPSN_Sex,ToPSN_Sex,FromPSN_BirthDate,ToPSN_BirthDate,FromPSN_CertificateType,ToPSN_CertificateType,FromPSN_CertificateNO,ToPSN_CertificateNO,ZGZSBH,To_ZGZSBH,FromEND_Addess,ToEND_Addess,Nation)
			VALUES (@ApplyID,@PSN_MobilePhone,@PSN_Email,@PSN_RegisterNo,@ValidDate,@ChangeReason,@OldENT_Name,@OldLinkMan,@OldENT_Telephone,@OldENT_Correspondence,@OldENT_Postcode,@ENT_Name,@FR,@LinkMan,@ENT_Telephone,@ENT_Correspondence,@ENT_Postcode,@END_Addess,@IfOutside,@ENT_Type,@ENT_Economic_Nature,@ENT_Sort,@ENT_Grade,@ENT_QualificationCertificateNo,@ENT_Sort2,@ENT_Grade2,@ENT_QualificationCertificateNo2,@ENT_NameFrom,@ENT_NameTo,@PSN_NameFrom,@PSN_NameTo,@FromENT_City,@ToENT_City,@FromPSN_Sex,@ToPSN_Sex,@FromPSN_BirthDate,@ToPSN_BirthDate,@FromPSN_CertificateType,@ToPSN_CertificateType,@FromPSN_CertificateNO,@ToPSN_CertificateNO,@ZGZSBH,@To_ZGZSBH,@FromEND_Addess,@ToEND_Addess,@Nation)";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, _ApplyChangeMDL.ApplyID));
            p.Add(db.CreateParameter("PSN_MobilePhone", DbType.String, _ApplyChangeMDL.PSN_MobilePhone));
            p.Add(db.CreateParameter("PSN_Email", DbType.String, _ApplyChangeMDL.PSN_Email));
            p.Add(db.CreateParameter("PSN_RegisterNo", DbType.String, _ApplyChangeMDL.PSN_RegisterNo));
            p.Add(db.CreateParameter("ValidDate", DbType.DateTime, _ApplyChangeMDL.ValidDate));
            p.Add(db.CreateParameter("ChangeReason", DbType.String, _ApplyChangeMDL.ChangeReason));
            p.Add(db.CreateParameter("OldENT_Name", DbType.String, _ApplyChangeMDL.OldENT_Name));
            p.Add(db.CreateParameter("OldLinkMan", DbType.String, _ApplyChangeMDL.OldLinkMan));
            p.Add(db.CreateParameter("OldENT_Telephone", DbType.String, _ApplyChangeMDL.OldENT_Telephone));
            p.Add(db.CreateParameter("OldENT_Correspondence", DbType.String, _ApplyChangeMDL.OldENT_Correspondence));
            p.Add(db.CreateParameter("OldENT_Postcode", DbType.String, _ApplyChangeMDL.OldENT_Postcode));
            p.Add(db.CreateParameter("ENT_Name", DbType.String, _ApplyChangeMDL.ENT_Name));
            p.Add(db.CreateParameter("FR", DbType.String, _ApplyChangeMDL.FR));
            p.Add(db.CreateParameter("LinkMan", DbType.String, _ApplyChangeMDL.LinkMan));
            p.Add(db.CreateParameter("ENT_Telephone", DbType.String, _ApplyChangeMDL.ENT_Telephone));
            p.Add(db.CreateParameter("ENT_Correspondence", DbType.String, _ApplyChangeMDL.ENT_Correspondence));
            p.Add(db.CreateParameter("ENT_Postcode", DbType.String, _ApplyChangeMDL.ENT_Postcode));
            p.Add(db.CreateParameter("END_Addess", DbType.String, _ApplyChangeMDL.END_Addess));
            p.Add(db.CreateParameter("IfOutside", DbType.Boolean, _ApplyChangeMDL.IfOutside));
            p.Add(db.CreateParameter("ENT_Type", DbType.String, _ApplyChangeMDL.ENT_Type));
            p.Add(db.CreateParameter("ENT_Economic_Nature", DbType.String, _ApplyChangeMDL.ENT_Economic_Nature));
            p.Add(db.CreateParameter("ENT_Sort", DbType.String, _ApplyChangeMDL.ENT_Sort));
            p.Add(db.CreateParameter("ENT_Grade", DbType.String, _ApplyChangeMDL.ENT_Grade));
            p.Add(db.CreateParameter("ENT_QualificationCertificateNo", DbType.String, _ApplyChangeMDL.ENT_QualificationCertificateNo));
            p.Add(db.CreateParameter("ENT_Sort2", DbType.String, _ApplyChangeMDL.ENT_Sort2));
            p.Add(db.CreateParameter("ENT_Grade2", DbType.String, _ApplyChangeMDL.ENT_Grade2));
            p.Add(db.CreateParameter("ENT_QualificationCertificateNo2", DbType.String, _ApplyChangeMDL.ENT_QualificationCertificateNo2));
            p.Add(db.CreateParameter("ENT_NameFrom", DbType.String, _ApplyChangeMDL.ENT_NameFrom));
            p.Add(db.CreateParameter("ENT_NameTo", DbType.String, _ApplyChangeMDL.ENT_NameTo));
            p.Add(db.CreateParameter("PSN_NameFrom", DbType.String, _ApplyChangeMDL.PSN_NameFrom));
            p.Add(db.CreateParameter("PSN_NameTo", DbType.String, _ApplyChangeMDL.PSN_NameTo));
            p.Add(db.CreateParameter("FromENT_City", DbType.String, _ApplyChangeMDL.FromENT_City));
            p.Add(db.CreateParameter("ToENT_City", DbType.String, _ApplyChangeMDL.ToENT_City));
            p.Add(db.CreateParameter("FromPSN_Sex", DbType.String, _ApplyChangeMDL.FromPSN_Sex));
            p.Add(db.CreateParameter("ToPSN_Sex", DbType.String, _ApplyChangeMDL.ToPSN_Sex));
            p.Add(db.CreateParameter("FromPSN_BirthDate", DbType.DateTime, _ApplyChangeMDL.FromPSN_BirthDate));
            p.Add(db.CreateParameter("ToPSN_BirthDate", DbType.DateTime, _ApplyChangeMDL.ToPSN_BirthDate));
            p.Add(db.CreateParameter("FromPSN_CertificateType", DbType.String, _ApplyChangeMDL.FromPSN_CertificateType));
            p.Add(db.CreateParameter("ToPSN_CertificateType", DbType.String, _ApplyChangeMDL.ToPSN_CertificateType));
            p.Add(db.CreateParameter("FromPSN_CertificateNO", DbType.String, _ApplyChangeMDL.FromPSN_CertificateNO));
            p.Add(db.CreateParameter("ToPSN_CertificateNO", DbType.String, _ApplyChangeMDL.ToPSN_CertificateNO));
            p.Add(db.CreateParameter("ZGZSBH", DbType.String, _ApplyChangeMDL.ZGZSBH));
            p.Add(db.CreateParameter("To_ZGZSBH", DbType.String, _ApplyChangeMDL.To_ZGZSBH));
            p.Add(db.CreateParameter("FromEND_Addess", DbType.String, _ApplyChangeMDL.FromEND_Addess));
            p.Add(db.CreateParameter("ToEND_Addess", DbType.String, _ApplyChangeMDL.ToEND_Addess));
            p.Add(db.CreateParameter("Nation", DbType.String, _ApplyChangeMDL.Nation));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_ApplyChangeMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(ApplyChangeMDL _ApplyChangeMDL)
        {
            return Update(null, _ApplyChangeMDL);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_ApplyChangeMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, ApplyChangeMDL _ApplyChangeMDL)
        {
            string sql = @"
			UPDATE dbo.ApplyChange
				SET PSN_MobilePhone = @PSN_MobilePhone,PSN_Email = @PSN_Email,PSN_RegisterNo = @PSN_RegisterNo,ValidDate = @ValidDate,ChangeReason = @ChangeReason,OldENT_Name = @OldENT_Name,OldLinkMan = @OldLinkMan,OldENT_Telephone = @OldENT_Telephone,OldENT_Correspondence = @OldENT_Correspondence,OldENT_Postcode = @OldENT_Postcode,ENT_Name = @ENT_Name,FR = @FR,LinkMan = @LinkMan,ENT_Telephone = @ENT_Telephone,ENT_Correspondence = @ENT_Correspondence,ENT_Postcode = @ENT_Postcode,END_Addess = @END_Addess,IfOutside = @IfOutside,ENT_Type = @ENT_Type,ENT_Economic_Nature = @ENT_Economic_Nature,ENT_Sort = @ENT_Sort,ENT_Grade = @ENT_Grade,ENT_QualificationCertificateNo = @ENT_QualificationCertificateNo,ENT_Sort2 = @ENT_Sort2,ENT_Grade2 = @ENT_Grade2,ENT_QualificationCertificateNo2 = @ENT_QualificationCertificateNo2,ENT_NameFrom = @ENT_NameFrom,ENT_NameTo = @ENT_NameTo,PSN_NameFrom = @PSN_NameFrom,PSN_NameTo = @PSN_NameTo,FromENT_City = @FromENT_City,ToENT_City = @ToENT_City,FromPSN_Sex = @FromPSN_Sex,ToPSN_Sex = @ToPSN_Sex,FromPSN_BirthDate = @FromPSN_BirthDate,ToPSN_BirthDate = @ToPSN_BirthDate,FromPSN_CertificateType = @FromPSN_CertificateType,ToPSN_CertificateType = @ToPSN_CertificateType,FromPSN_CertificateNO = @FromPSN_CertificateNO,ToPSN_CertificateNO = @ToPSN_CertificateNO,ZGZSBH = @ZGZSBH,To_ZGZSBH = @To_ZGZSBH,FromEND_Addess = @FromEND_Addess,ToEND_Addess = @ToEND_Addess,Nation=@Nation
			WHERE
				ApplyID = @ApplyID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, _ApplyChangeMDL.ApplyID));
            p.Add(db.CreateParameter("PSN_MobilePhone", DbType.String, _ApplyChangeMDL.PSN_MobilePhone));
            p.Add(db.CreateParameter("PSN_Email", DbType.String, _ApplyChangeMDL.PSN_Email));
            p.Add(db.CreateParameter("PSN_RegisterNo", DbType.String, _ApplyChangeMDL.PSN_RegisterNo));
            p.Add(db.CreateParameter("ValidDate", DbType.DateTime, _ApplyChangeMDL.ValidDate));
            p.Add(db.CreateParameter("ChangeReason", DbType.String, _ApplyChangeMDL.ChangeReason));
            p.Add(db.CreateParameter("OldENT_Name", DbType.String, _ApplyChangeMDL.OldENT_Name));
            p.Add(db.CreateParameter("OldLinkMan", DbType.String, _ApplyChangeMDL.OldLinkMan));
            p.Add(db.CreateParameter("OldENT_Telephone", DbType.String, _ApplyChangeMDL.OldENT_Telephone));
            p.Add(db.CreateParameter("OldENT_Correspondence", DbType.String, _ApplyChangeMDL.OldENT_Correspondence));
            p.Add(db.CreateParameter("OldENT_Postcode", DbType.String, _ApplyChangeMDL.OldENT_Postcode));
            p.Add(db.CreateParameter("ENT_Name", DbType.String, _ApplyChangeMDL.ENT_Name));
            p.Add(db.CreateParameter("FR", DbType.String, _ApplyChangeMDL.FR));
            p.Add(db.CreateParameter("LinkMan", DbType.String, _ApplyChangeMDL.LinkMan));
            p.Add(db.CreateParameter("ENT_Telephone", DbType.String, _ApplyChangeMDL.ENT_Telephone));
            p.Add(db.CreateParameter("ENT_Correspondence", DbType.String, _ApplyChangeMDL.ENT_Correspondence));
            p.Add(db.CreateParameter("ENT_Postcode", DbType.String, _ApplyChangeMDL.ENT_Postcode));
            p.Add(db.CreateParameter("END_Addess", DbType.String, _ApplyChangeMDL.END_Addess));
            p.Add(db.CreateParameter("IfOutside", DbType.Boolean, _ApplyChangeMDL.IfOutside));
            p.Add(db.CreateParameter("ENT_Type", DbType.String, _ApplyChangeMDL.ENT_Type));
            p.Add(db.CreateParameter("ENT_Economic_Nature", DbType.String, _ApplyChangeMDL.ENT_Economic_Nature));
            p.Add(db.CreateParameter("ENT_Sort", DbType.String, _ApplyChangeMDL.ENT_Sort));
            p.Add(db.CreateParameter("ENT_Grade", DbType.String, _ApplyChangeMDL.ENT_Grade));
            p.Add(db.CreateParameter("ENT_QualificationCertificateNo", DbType.String, _ApplyChangeMDL.ENT_QualificationCertificateNo));
            p.Add(db.CreateParameter("ENT_Sort2", DbType.String, _ApplyChangeMDL.ENT_Sort2));
            p.Add(db.CreateParameter("ENT_Grade2", DbType.String, _ApplyChangeMDL.ENT_Grade2));
            p.Add(db.CreateParameter("ENT_QualificationCertificateNo2", DbType.String, _ApplyChangeMDL.ENT_QualificationCertificateNo2));
            p.Add(db.CreateParameter("ENT_NameFrom", DbType.String, _ApplyChangeMDL.ENT_NameFrom));
            p.Add(db.CreateParameter("ENT_NameTo", DbType.String, _ApplyChangeMDL.ENT_NameTo));
            p.Add(db.CreateParameter("PSN_NameFrom", DbType.String, _ApplyChangeMDL.PSN_NameFrom));
            p.Add(db.CreateParameter("PSN_NameTo", DbType.String, _ApplyChangeMDL.PSN_NameTo));
            p.Add(db.CreateParameter("FromENT_City", DbType.String, _ApplyChangeMDL.FromENT_City));
            p.Add(db.CreateParameter("ToENT_City", DbType.String, _ApplyChangeMDL.ToENT_City));
            p.Add(db.CreateParameter("FromPSN_Sex", DbType.String, _ApplyChangeMDL.FromPSN_Sex));
            p.Add(db.CreateParameter("ToPSN_Sex", DbType.String, _ApplyChangeMDL.ToPSN_Sex));
            p.Add(db.CreateParameter("FromPSN_BirthDate", DbType.DateTime, _ApplyChangeMDL.FromPSN_BirthDate));
            p.Add(db.CreateParameter("ToPSN_BirthDate", DbType.DateTime, _ApplyChangeMDL.ToPSN_BirthDate));
            p.Add(db.CreateParameter("FromPSN_CertificateType", DbType.String, _ApplyChangeMDL.FromPSN_CertificateType));
            p.Add(db.CreateParameter("ToPSN_CertificateType", DbType.String, _ApplyChangeMDL.ToPSN_CertificateType));
            p.Add(db.CreateParameter("FromPSN_CertificateNO", DbType.String, _ApplyChangeMDL.FromPSN_CertificateNO));
            p.Add(db.CreateParameter("ToPSN_CertificateNO", DbType.String, _ApplyChangeMDL.ToPSN_CertificateNO));
            p.Add(db.CreateParameter("ZGZSBH",DbType.String, _ApplyChangeMDL.ZGZSBH));
            p.Add(db.CreateParameter("To_ZGZSBH", DbType.String, _ApplyChangeMDL.To_ZGZSBH));
            p.Add(db.CreateParameter("FromEND_Addess", DbType.String, _ApplyChangeMDL.FromEND_Addess));
            p.Add(db.CreateParameter("ToEND_Addess", DbType.String, _ApplyChangeMDL.ToEND_Addess));
            p.Add(db.CreateParameter("Nation", DbType.String, _ApplyChangeMDL.Nation));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="ApplyID">主键</param>
        /// <returns></returns>
        public static int Delete(string ApplyID)
        {
            return Delete(null, ApplyID);
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ApplyID">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, string ApplyID)
        {
            string sql = @"DELETE FROM dbo.ApplyChange WHERE ApplyID = @ApplyID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, ApplyID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="_ApplyChangeMDL">对象实体类</param>
        /// <returns></returns>
        public static int Delete(ApplyChangeMDL _ApplyChangeMDL)
        {
            return Delete(null, _ApplyChangeMDL);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="_ApplyChangeMDL">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, ApplyChangeMDL _ApplyChangeMDL)
        {
            string sql = @"DELETE FROM dbo.ApplyChange WHERE ApplyID = @ApplyID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, _ApplyChangeMDL.ApplyID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ApplyID">主键</param>
        public static ApplyChangeMDL GetObject(string ApplyID)
        {
            string sql = @"
			SELECT ApplyID,Nation,PSN_MobilePhone,PSN_Email,PSN_RegisterNo,ValidDate,ChangeReason,OldENT_Name,OldLinkMan,OldENT_Telephone,OldENT_Correspondence,OldENT_Postcode,ENT_Name,FR,LinkMan,ENT_Telephone,ENT_Correspondence,ENT_Postcode,END_Addess,IfOutside,ENT_Type,ENT_Economic_Nature,ENT_Sort,ENT_Grade,ENT_QualificationCertificateNo,ENT_Sort2,ENT_Grade2,ENT_QualificationCertificateNo2,ENT_NameFrom,ENT_NameTo,PSN_NameFrom,PSN_NameTo,FromENT_City,ToENT_City,FromPSN_Sex,ToPSN_Sex,FromPSN_BirthDate,ToPSN_BirthDate,FromPSN_CertificateType,ToPSN_CertificateType,FromPSN_CertificateNO,ToPSN_CertificateNO,ZGZSBH,To_ZGZSBH,FromEND_Addess,ToEND_Addess
			FROM dbo.ApplyChange
			WHERE ApplyID = @ApplyID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, ApplyID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                ApplyChangeMDL _ApplyChangeMDL = null;
                if (reader.Read())
                {
                    _ApplyChangeMDL = new ApplyChangeMDL();
                    if (reader["ApplyID"] != DBNull.Value) _ApplyChangeMDL.ApplyID = Convert.ToString(reader["ApplyID"]);
                    if (reader["PSN_MobilePhone"] != DBNull.Value) _ApplyChangeMDL.PSN_MobilePhone = Convert.ToString(reader["PSN_MobilePhone"]);
                    if (reader["PSN_Email"] != DBNull.Value) _ApplyChangeMDL.PSN_Email = Convert.ToString(reader["PSN_Email"]);
                    if (reader["PSN_RegisterNo"] != DBNull.Value) _ApplyChangeMDL.PSN_RegisterNo = Convert.ToString(reader["PSN_RegisterNo"]);
                    if (reader["ValidDate"] != DBNull.Value) _ApplyChangeMDL.ValidDate = Convert.ToDateTime(reader["ValidDate"]);
                    if (reader["ChangeReason"] != DBNull.Value) _ApplyChangeMDL.ChangeReason = Convert.ToString(reader["ChangeReason"]);
                    if (reader["OldENT_Name"] != DBNull.Value) _ApplyChangeMDL.OldENT_Name = Convert.ToString(reader["OldENT_Name"]);
                    if (reader["OldLinkMan"] != DBNull.Value) _ApplyChangeMDL.OldLinkMan = Convert.ToString(reader["OldLinkMan"]);
                    if (reader["OldENT_Telephone"] != DBNull.Value) _ApplyChangeMDL.OldENT_Telephone = Convert.ToString(reader["OldENT_Telephone"]);
                    if (reader["OldENT_Correspondence"] != DBNull.Value) _ApplyChangeMDL.OldENT_Correspondence = Convert.ToString(reader["OldENT_Correspondence"]);
                    if (reader["OldENT_Postcode"] != DBNull.Value) _ApplyChangeMDL.OldENT_Postcode = Convert.ToString(reader["OldENT_Postcode"]);
                    if (reader["ENT_Name"] != DBNull.Value) _ApplyChangeMDL.ENT_Name = Convert.ToString(reader["ENT_Name"]);
                    if (reader["FR"] != DBNull.Value) _ApplyChangeMDL.FR = Convert.ToString(reader["FR"]);
                    if (reader["LinkMan"] != DBNull.Value) _ApplyChangeMDL.LinkMan = Convert.ToString(reader["LinkMan"]);
                    if (reader["ENT_Telephone"] != DBNull.Value) _ApplyChangeMDL.ENT_Telephone = Convert.ToString(reader["ENT_Telephone"]);
                    if (reader["ENT_Correspondence"] != DBNull.Value) _ApplyChangeMDL.ENT_Correspondence = Convert.ToString(reader["ENT_Correspondence"]);
                    if (reader["ENT_Postcode"] != DBNull.Value) _ApplyChangeMDL.ENT_Postcode = Convert.ToString(reader["ENT_Postcode"]);
                    if (reader["END_Addess"] != DBNull.Value) _ApplyChangeMDL.END_Addess = Convert.ToString(reader["END_Addess"]);
                    if (reader["IfOutside"] != DBNull.Value) _ApplyChangeMDL.IfOutside = Convert.ToBoolean(reader["IfOutside"]);
                    if (reader["ENT_Type"] != DBNull.Value) _ApplyChangeMDL.ENT_Type = Convert.ToString(reader["ENT_Type"]);
                    if (reader["ENT_Economic_Nature"] != DBNull.Value) _ApplyChangeMDL.ENT_Economic_Nature = Convert.ToString(reader["ENT_Economic_Nature"]);
                    if (reader["ENT_Sort"] != DBNull.Value) _ApplyChangeMDL.ENT_Sort = Convert.ToString(reader["ENT_Sort"]);
                    if (reader["ENT_Grade"] != DBNull.Value) _ApplyChangeMDL.ENT_Grade = Convert.ToString(reader["ENT_Grade"]);
                    if (reader["ENT_QualificationCertificateNo"] != DBNull.Value) _ApplyChangeMDL.ENT_QualificationCertificateNo = Convert.ToString(reader["ENT_QualificationCertificateNo"]);
                    if (reader["ENT_Sort2"] != DBNull.Value) _ApplyChangeMDL.ENT_Sort2 = Convert.ToString(reader["ENT_Sort2"]);
                    if (reader["ENT_Grade2"] != DBNull.Value) _ApplyChangeMDL.ENT_Grade2 = Convert.ToString(reader["ENT_Grade2"]);
                    if (reader["ENT_QualificationCertificateNo2"] != DBNull.Value) _ApplyChangeMDL.ENT_QualificationCertificateNo2 = Convert.ToString(reader["ENT_QualificationCertificateNo2"]);
                    if (reader["ENT_NameFrom"] != DBNull.Value) _ApplyChangeMDL.ENT_NameFrom = Convert.ToString(reader["ENT_NameFrom"]);
                    if (reader["ENT_NameTo"] != DBNull.Value) _ApplyChangeMDL.ENT_NameTo = Convert.ToString(reader["ENT_NameTo"]);
                    if (reader["PSN_NameFrom"] != DBNull.Value) _ApplyChangeMDL.PSN_NameFrom = Convert.ToString(reader["PSN_NameFrom"]);
                    if (reader["PSN_NameTo"] != DBNull.Value) _ApplyChangeMDL.PSN_NameTo = Convert.ToString(reader["PSN_NameTo"]);
                    if (reader["FromENT_City"] != DBNull.Value) _ApplyChangeMDL.FromENT_City = Convert.ToString(reader["FromENT_City"]);
                    if (reader["ToENT_City"] != DBNull.Value) _ApplyChangeMDL.ToENT_City = Convert.ToString(reader["ToENT_City"]);
                    if (reader["FromPSN_Sex"] != DBNull.Value) _ApplyChangeMDL.FromPSN_Sex = Convert.ToString(reader["FromPSN_Sex"]);
                    if (reader["ToPSN_Sex"] != DBNull.Value) _ApplyChangeMDL.ToPSN_Sex = Convert.ToString(reader["ToPSN_Sex"]);
                    if (reader["FromPSN_BirthDate"] != DBNull.Value) _ApplyChangeMDL.FromPSN_BirthDate = Convert.ToDateTime(reader["FromPSN_BirthDate"]);
                    if (reader["ToPSN_BirthDate"] != DBNull.Value) _ApplyChangeMDL.ToPSN_BirthDate = Convert.ToDateTime(reader["ToPSN_BirthDate"]);
                    if (reader["FromPSN_CertificateType"] != DBNull.Value) _ApplyChangeMDL.FromPSN_CertificateType = Convert.ToString(reader["FromPSN_CertificateType"]);
                    if (reader["ToPSN_CertificateType"] != DBNull.Value) _ApplyChangeMDL.ToPSN_CertificateType = Convert.ToString(reader["ToPSN_CertificateType"]);
                    if (reader["FromPSN_CertificateNO"] != DBNull.Value) _ApplyChangeMDL.FromPSN_CertificateNO = Convert.ToString(reader["FromPSN_CertificateNO"]);
                    if (reader["ToPSN_CertificateNO"] != DBNull.Value) _ApplyChangeMDL.ToPSN_CertificateNO = Convert.ToString(reader["ToPSN_CertificateNO"]);
                    if (reader["ZGZSBH"] != DBNull.Value) _ApplyChangeMDL.ZGZSBH = Convert.ToString(reader["ZGZSBH"]);
                    if (reader["To_ZGZSBH"] != DBNull.Value) _ApplyChangeMDL.To_ZGZSBH = Convert.ToString(reader["To_ZGZSBH"]);
                    if (reader["FromEND_Addess"] != DBNull.Value) _ApplyChangeMDL.FromEND_Addess = Convert.ToString(reader["FromEND_Addess"]);
                    if (reader["ToEND_Addess"] != DBNull.Value) _ApplyChangeMDL.ToEND_Addess = Convert.ToString(reader["ToEND_Addess"]);
                    if (reader["Nation"] != DBNull.Value) _ApplyChangeMDL.Nation = Convert.ToString(reader["Nation"]);
                }
                reader.Close();
                db.Close();
                return _ApplyChangeMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.ApplyChange", "*", filterWhereString, orderBy == "" ? " ApplyID" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.ApplyChange", filterWhereString);
        }

        #region 自定义方法
        /// <summary>
        /// 判断View_JZS_TOW_Applying视图根据组织机构代码，根据申请表applytype判断有没有这个公司有没有人在事项申报
        /// </summary>
        /// <param name="ENT_OrganizationsCode">组织机构代码</param>
        /// <returns></returns>
        public static DataTable  SelectView_JZS_TOW_Applying(string ENT_OrganizationsCode)
        {

            //string sql = @"select * from  View_JZS_TOW_Applying where  (ENT_OrganizationsCode='" + ENT_OrganizationsCode + "' or  ENT_OrganizationsCode like '________" + ENT_OrganizationsCode + "_') and applytype is not null and ENT_Name=NewENT_Name and ApplyStatus <>'已公告'";
            string sql = @"select * from  View_JZS_TOW_Applying where  (ENT_OrganizationsCode='" + ENT_OrganizationsCode + "' or  ENT_OrganizationsCode like '________" + ENT_OrganizationsCode + "_') and applytype is not null  and ApplyStatus <>'已公告' and ApplyStatus <>'已驳回' and  PSN_RegisteType !='07'";
            return (new DBHelper()).GetFillData(sql);

        }
        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        public static DataTable GetApplyChangeList(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.View_ApplyChange", "*", filterWhereString, orderBy == "" ? " ENT_OrganizationsCode" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectApplyChangeCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.View_ApplyChange", filterWhereString);
        }
        #endregion
    }
}
