using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--zjs_ApplyChangeDAL(填写类描述)
	/// </summary>
    public class zjs_ApplyChangeDAL
    {
        public zjs_ApplyChangeDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_zjs_ApplyChangeMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(zjs_ApplyChangeMDL _zjs_ApplyChangeMDL)
		{
		    return Insert(null,_zjs_ApplyChangeMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_zjs_ApplyChangeMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,zjs_ApplyChangeMDL _zjs_ApplyChangeMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.zjs_ApplyChange(ApplyID,PSN_MobilePhone,PSN_Email,PSN_RegisterNo,ValidDate,ChangeReason,OldENT_Name,OldEND_Addess,OldLinkMan,OldENT_Telephone,OldFR,OldENT_Type,OldENT_Sort,OldENT_Grade,OldENT_QualificationCertificateNo,ENT_Name,FR,END_Addess,LinkMan,ENT_Telephone,ENT_Type,ENT_Economic_Nature,ENT_Sort,ENT_Grade,ENT_QualificationCertificateNo,IfOutside,ENT_NameFrom,ENT_NameTo,PSN_NameFrom,PSN_NameTo,FromENT_City,ToENT_City,FromPSN_Sex,ToPSN_Sex,FromPSN_BirthDate,ToPSN_BirthDate,FromPSN_CertificateType,ToPSN_CertificateType,FromPSN_CertificateNO,ToPSN_CertificateNO,ZGZSBH,To_ZGZSBH,FromEND_Addess,ToEND_Addess)
			VALUES (@ApplyID,@PSN_MobilePhone,@PSN_Email,@PSN_RegisterNo,@ValidDate,@ChangeReason,@OldENT_Name,@OldEND_Addess,@OldLinkMan,@OldENT_Telephone,@OldFR,@OldENT_Type,@OldENT_Sort,@OldENT_Grade,@OldENT_QualificationCertificateNo,@ENT_Name,@FR,@END_Addess,@LinkMan,@ENT_Telephone,@ENT_Type,@ENT_Economic_Nature,@ENT_Sort,@ENT_Grade,@ENT_QualificationCertificateNo,@IfOutside,@ENT_NameFrom,@ENT_NameTo,@PSN_NameFrom,@PSN_NameTo,@FromENT_City,@ToENT_City,@FromPSN_Sex,@ToPSN_Sex,@FromPSN_BirthDate,@ToPSN_BirthDate,@FromPSN_CertificateType,@ToPSN_CertificateType,@FromPSN_CertificateNO,@ToPSN_CertificateNO,@ZGZSBH,@To_ZGZSBH,@FromEND_Addess,@ToEND_Addess)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ApplyID",DbType.String, _zjs_ApplyChangeMDL.ApplyID));
			p.Add(db.CreateParameter("PSN_MobilePhone",DbType.String, _zjs_ApplyChangeMDL.PSN_MobilePhone));
			p.Add(db.CreateParameter("PSN_Email",DbType.String, _zjs_ApplyChangeMDL.PSN_Email));
			p.Add(db.CreateParameter("PSN_RegisterNo",DbType.String, _zjs_ApplyChangeMDL.PSN_RegisterNo));
			p.Add(db.CreateParameter("ValidDate",DbType.DateTime, _zjs_ApplyChangeMDL.ValidDate));
			p.Add(db.CreateParameter("ChangeReason",DbType.String, _zjs_ApplyChangeMDL.ChangeReason));
			p.Add(db.CreateParameter("OldENT_Name",DbType.String, _zjs_ApplyChangeMDL.OldENT_Name));
			p.Add(db.CreateParameter("OldEND_Addess",DbType.String, _zjs_ApplyChangeMDL.OldEND_Addess));
			p.Add(db.CreateParameter("OldLinkMan",DbType.String, _zjs_ApplyChangeMDL.OldLinkMan));
			p.Add(db.CreateParameter("OldENT_Telephone",DbType.String, _zjs_ApplyChangeMDL.OldENT_Telephone));
			p.Add(db.CreateParameter("OldFR",DbType.String, _zjs_ApplyChangeMDL.OldFR));
			p.Add(db.CreateParameter("OldENT_Type",DbType.String, _zjs_ApplyChangeMDL.OldENT_Type));
			p.Add(db.CreateParameter("OldENT_Sort",DbType.String, _zjs_ApplyChangeMDL.OldENT_Sort));
			p.Add(db.CreateParameter("OldENT_Grade",DbType.String, _zjs_ApplyChangeMDL.OldENT_Grade));
			p.Add(db.CreateParameter("OldENT_QualificationCertificateNo",DbType.String, _zjs_ApplyChangeMDL.OldENT_QualificationCertificateNo));
			p.Add(db.CreateParameter("ENT_Name",DbType.String, _zjs_ApplyChangeMDL.ENT_Name));
			p.Add(db.CreateParameter("FR",DbType.String, _zjs_ApplyChangeMDL.FR));
			p.Add(db.CreateParameter("END_Addess",DbType.String, _zjs_ApplyChangeMDL.END_Addess));
			p.Add(db.CreateParameter("LinkMan",DbType.String, _zjs_ApplyChangeMDL.LinkMan));
			p.Add(db.CreateParameter("ENT_Telephone",DbType.String, _zjs_ApplyChangeMDL.ENT_Telephone));
			p.Add(db.CreateParameter("ENT_Type",DbType.String, _zjs_ApplyChangeMDL.ENT_Type));
			p.Add(db.CreateParameter("ENT_Economic_Nature",DbType.String, _zjs_ApplyChangeMDL.ENT_Economic_Nature));
			p.Add(db.CreateParameter("ENT_Sort",DbType.String, _zjs_ApplyChangeMDL.ENT_Sort));
			p.Add(db.CreateParameter("ENT_Grade",DbType.String, _zjs_ApplyChangeMDL.ENT_Grade));
			p.Add(db.CreateParameter("ENT_QualificationCertificateNo",DbType.String, _zjs_ApplyChangeMDL.ENT_QualificationCertificateNo));
			p.Add(db.CreateParameter("IfOutside",DbType.Boolean, _zjs_ApplyChangeMDL.IfOutside));
			p.Add(db.CreateParameter("ENT_NameFrom",DbType.String, _zjs_ApplyChangeMDL.ENT_NameFrom));
			p.Add(db.CreateParameter("ENT_NameTo",DbType.String, _zjs_ApplyChangeMDL.ENT_NameTo));
			p.Add(db.CreateParameter("PSN_NameFrom",DbType.String, _zjs_ApplyChangeMDL.PSN_NameFrom));
			p.Add(db.CreateParameter("PSN_NameTo",DbType.String, _zjs_ApplyChangeMDL.PSN_NameTo));
			p.Add(db.CreateParameter("FromENT_City",DbType.String, _zjs_ApplyChangeMDL.FromENT_City));
			p.Add(db.CreateParameter("ToENT_City",DbType.String, _zjs_ApplyChangeMDL.ToENT_City));
			p.Add(db.CreateParameter("FromPSN_Sex",DbType.String, _zjs_ApplyChangeMDL.FromPSN_Sex));
			p.Add(db.CreateParameter("ToPSN_Sex",DbType.String, _zjs_ApplyChangeMDL.ToPSN_Sex));
			p.Add(db.CreateParameter("FromPSN_BirthDate",DbType.DateTime, _zjs_ApplyChangeMDL.FromPSN_BirthDate));
			p.Add(db.CreateParameter("ToPSN_BirthDate",DbType.DateTime, _zjs_ApplyChangeMDL.ToPSN_BirthDate));
			p.Add(db.CreateParameter("FromPSN_CertificateType",DbType.String, _zjs_ApplyChangeMDL.FromPSN_CertificateType));
			p.Add(db.CreateParameter("ToPSN_CertificateType",DbType.String, _zjs_ApplyChangeMDL.ToPSN_CertificateType));
			p.Add(db.CreateParameter("FromPSN_CertificateNO",DbType.String, _zjs_ApplyChangeMDL.FromPSN_CertificateNO));
			p.Add(db.CreateParameter("ToPSN_CertificateNO",DbType.String, _zjs_ApplyChangeMDL.ToPSN_CertificateNO));
			p.Add(db.CreateParameter("ZGZSBH",DbType.String, _zjs_ApplyChangeMDL.ZGZSBH));
			p.Add(db.CreateParameter("To_ZGZSBH",DbType.String, _zjs_ApplyChangeMDL.To_ZGZSBH));
			p.Add(db.CreateParameter("FromEND_Addess",DbType.String, _zjs_ApplyChangeMDL.FromEND_Addess));
			p.Add(db.CreateParameter("ToEND_Addess",DbType.String, _zjs_ApplyChangeMDL.ToEND_Addess));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_zjs_ApplyChangeMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(zjs_ApplyChangeMDL _zjs_ApplyChangeMDL)
		{
			return Update(null,_zjs_ApplyChangeMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_zjs_ApplyChangeMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,zjs_ApplyChangeMDL _zjs_ApplyChangeMDL)
		{
			string sql = @"
			UPDATE dbo.zjs_ApplyChange
				SET	PSN_MobilePhone = @PSN_MobilePhone,PSN_Email = @PSN_Email,PSN_RegisterNo = @PSN_RegisterNo,ValidDate = @ValidDate,ChangeReason = @ChangeReason,OldENT_Name = @OldENT_Name,OldEND_Addess = @OldEND_Addess,OldLinkMan = @OldLinkMan,OldENT_Telephone = @OldENT_Telephone,OldFR = @OldFR,OldENT_Type = @OldENT_Type,OldENT_Sort = @OldENT_Sort,OldENT_Grade = @OldENT_Grade,OldENT_QualificationCertificateNo = @OldENT_QualificationCertificateNo,ENT_Name = @ENT_Name,FR = @FR,END_Addess = @END_Addess,LinkMan = @LinkMan,ENT_Telephone = @ENT_Telephone,ENT_Type = @ENT_Type,ENT_Economic_Nature = @ENT_Economic_Nature,ENT_Sort = @ENT_Sort,ENT_Grade = @ENT_Grade,ENT_QualificationCertificateNo = @ENT_QualificationCertificateNo,IfOutside = @IfOutside,ENT_NameFrom = @ENT_NameFrom,ENT_NameTo = @ENT_NameTo,PSN_NameFrom = @PSN_NameFrom,PSN_NameTo = @PSN_NameTo,FromENT_City = @FromENT_City,ToENT_City = @ToENT_City,FromPSN_Sex = @FromPSN_Sex,ToPSN_Sex = @ToPSN_Sex,FromPSN_BirthDate = @FromPSN_BirthDate,ToPSN_BirthDate = @ToPSN_BirthDate,FromPSN_CertificateType = @FromPSN_CertificateType,ToPSN_CertificateType = @ToPSN_CertificateType,FromPSN_CertificateNO = @FromPSN_CertificateNO,ToPSN_CertificateNO = @ToPSN_CertificateNO,ZGZSBH = @ZGZSBH,To_ZGZSBH = @To_ZGZSBH,FromEND_Addess = @FromEND_Addess,ToEND_Addess = @ToEND_Addess
			WHERE
				ApplyID = @ApplyID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ApplyID",DbType.String, _zjs_ApplyChangeMDL.ApplyID));
			p.Add(db.CreateParameter("PSN_MobilePhone",DbType.String, _zjs_ApplyChangeMDL.PSN_MobilePhone));
			p.Add(db.CreateParameter("PSN_Email",DbType.String, _zjs_ApplyChangeMDL.PSN_Email));
			p.Add(db.CreateParameter("PSN_RegisterNo",DbType.String, _zjs_ApplyChangeMDL.PSN_RegisterNo));
			p.Add(db.CreateParameter("ValidDate",DbType.DateTime, _zjs_ApplyChangeMDL.ValidDate));
			p.Add(db.CreateParameter("ChangeReason",DbType.String, _zjs_ApplyChangeMDL.ChangeReason));
			p.Add(db.CreateParameter("OldENT_Name",DbType.String, _zjs_ApplyChangeMDL.OldENT_Name));
			p.Add(db.CreateParameter("OldEND_Addess",DbType.String, _zjs_ApplyChangeMDL.OldEND_Addess));
			p.Add(db.CreateParameter("OldLinkMan",DbType.String, _zjs_ApplyChangeMDL.OldLinkMan));
			p.Add(db.CreateParameter("OldENT_Telephone",DbType.String, _zjs_ApplyChangeMDL.OldENT_Telephone));
			p.Add(db.CreateParameter("OldFR",DbType.String, _zjs_ApplyChangeMDL.OldFR));
			p.Add(db.CreateParameter("OldENT_Type",DbType.String, _zjs_ApplyChangeMDL.OldENT_Type));
			p.Add(db.CreateParameter("OldENT_Sort",DbType.String, _zjs_ApplyChangeMDL.OldENT_Sort));
			p.Add(db.CreateParameter("OldENT_Grade",DbType.String, _zjs_ApplyChangeMDL.OldENT_Grade));
			p.Add(db.CreateParameter("OldENT_QualificationCertificateNo",DbType.String, _zjs_ApplyChangeMDL.OldENT_QualificationCertificateNo));
			p.Add(db.CreateParameter("ENT_Name",DbType.String, _zjs_ApplyChangeMDL.ENT_Name));
			p.Add(db.CreateParameter("FR",DbType.String, _zjs_ApplyChangeMDL.FR));
			p.Add(db.CreateParameter("END_Addess",DbType.String, _zjs_ApplyChangeMDL.END_Addess));
			p.Add(db.CreateParameter("LinkMan",DbType.String, _zjs_ApplyChangeMDL.LinkMan));
			p.Add(db.CreateParameter("ENT_Telephone",DbType.String, _zjs_ApplyChangeMDL.ENT_Telephone));
			p.Add(db.CreateParameter("ENT_Type",DbType.String, _zjs_ApplyChangeMDL.ENT_Type));
			p.Add(db.CreateParameter("ENT_Economic_Nature",DbType.String, _zjs_ApplyChangeMDL.ENT_Economic_Nature));
			p.Add(db.CreateParameter("ENT_Sort",DbType.String, _zjs_ApplyChangeMDL.ENT_Sort));
			p.Add(db.CreateParameter("ENT_Grade",DbType.String, _zjs_ApplyChangeMDL.ENT_Grade));
			p.Add(db.CreateParameter("ENT_QualificationCertificateNo",DbType.String, _zjs_ApplyChangeMDL.ENT_QualificationCertificateNo));
			p.Add(db.CreateParameter("IfOutside",DbType.Boolean, _zjs_ApplyChangeMDL.IfOutside));
			p.Add(db.CreateParameter("ENT_NameFrom",DbType.String, _zjs_ApplyChangeMDL.ENT_NameFrom));
			p.Add(db.CreateParameter("ENT_NameTo",DbType.String, _zjs_ApplyChangeMDL.ENT_NameTo));
			p.Add(db.CreateParameter("PSN_NameFrom",DbType.String, _zjs_ApplyChangeMDL.PSN_NameFrom));
			p.Add(db.CreateParameter("PSN_NameTo",DbType.String, _zjs_ApplyChangeMDL.PSN_NameTo));
			p.Add(db.CreateParameter("FromENT_City",DbType.String, _zjs_ApplyChangeMDL.FromENT_City));
			p.Add(db.CreateParameter("ToENT_City",DbType.String, _zjs_ApplyChangeMDL.ToENT_City));
			p.Add(db.CreateParameter("FromPSN_Sex",DbType.String, _zjs_ApplyChangeMDL.FromPSN_Sex));
			p.Add(db.CreateParameter("ToPSN_Sex",DbType.String, _zjs_ApplyChangeMDL.ToPSN_Sex));
			p.Add(db.CreateParameter("FromPSN_BirthDate",DbType.DateTime, _zjs_ApplyChangeMDL.FromPSN_BirthDate));
			p.Add(db.CreateParameter("ToPSN_BirthDate",DbType.DateTime, _zjs_ApplyChangeMDL.ToPSN_BirthDate));
			p.Add(db.CreateParameter("FromPSN_CertificateType",DbType.String, _zjs_ApplyChangeMDL.FromPSN_CertificateType));
			p.Add(db.CreateParameter("ToPSN_CertificateType",DbType.String, _zjs_ApplyChangeMDL.ToPSN_CertificateType));
			p.Add(db.CreateParameter("FromPSN_CertificateNO",DbType.String, _zjs_ApplyChangeMDL.FromPSN_CertificateNO));
			p.Add(db.CreateParameter("ToPSN_CertificateNO",DbType.String, _zjs_ApplyChangeMDL.ToPSN_CertificateNO));
			p.Add(db.CreateParameter("ZGZSBH",DbType.String, _zjs_ApplyChangeMDL.ZGZSBH));
			p.Add(db.CreateParameter("To_ZGZSBH",DbType.String, _zjs_ApplyChangeMDL.To_ZGZSBH));
			p.Add(db.CreateParameter("FromEND_Addess",DbType.String, _zjs_ApplyChangeMDL.FromEND_Addess));
			p.Add(db.CreateParameter("ToEND_Addess",DbType.String, _zjs_ApplyChangeMDL.ToEND_Addess));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="zjs_ApplyChangeID">主键</param>
		/// <returns></returns>
        public static int Delete( string ApplyID )
		{
			return Delete(null, ApplyID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="zjs_ApplyChangeID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, string ApplyID)
		{
			string sql=@"DELETE FROM dbo.zjs_ApplyChange WHERE ApplyID = @ApplyID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ApplyID",DbType.String,ApplyID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="_zjs_ApplyChangeMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(zjs_ApplyChangeMDL _zjs_ApplyChangeMDL)
		{
			return Delete(null,_zjs_ApplyChangeMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_zjs_ApplyChangeMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,zjs_ApplyChangeMDL _zjs_ApplyChangeMDL)
		{
			string sql=@"DELETE FROM dbo.zjs_ApplyChange WHERE ApplyID = @ApplyID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ApplyID",DbType.String,_zjs_ApplyChangeMDL.ApplyID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="zjs_ApplyChangeID">主键</param>
        public static zjs_ApplyChangeMDL GetObject( string ApplyID )
		{
			string sql=@"
			SELECT ApplyID,PSN_MobilePhone,PSN_Email,PSN_RegisterNo,ValidDate,ChangeReason,OldENT_Name,OldEND_Addess,OldLinkMan,OldENT_Telephone,OldFR,OldENT_Type,OldENT_Sort,OldENT_Grade,OldENT_QualificationCertificateNo,ENT_Name,FR,END_Addess,LinkMan,ENT_Telephone,ENT_Type,ENT_Economic_Nature,ENT_Sort,ENT_Grade,ENT_QualificationCertificateNo,IfOutside,ENT_NameFrom,ENT_NameTo,PSN_NameFrom,PSN_NameTo,FromENT_City,ToENT_City,FromPSN_Sex,ToPSN_Sex,FromPSN_BirthDate,ToPSN_BirthDate,FromPSN_CertificateType,ToPSN_CertificateType,FromPSN_CertificateNO,ToPSN_CertificateNO,ZGZSBH,To_ZGZSBH,FromEND_Addess,ToEND_Addess
			FROM dbo.zjs_ApplyChange
			WHERE ApplyID = @ApplyID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, ApplyID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                zjs_ApplyChangeMDL _zjs_ApplyChangeMDL = null;
                if (reader.Read())
                {
                    _zjs_ApplyChangeMDL = new zjs_ApplyChangeMDL();
					if (reader["ApplyID"] != DBNull.Value) _zjs_ApplyChangeMDL.ApplyID = Convert.ToString(reader["ApplyID"]);
					if (reader["PSN_MobilePhone"] != DBNull.Value) _zjs_ApplyChangeMDL.PSN_MobilePhone = Convert.ToString(reader["PSN_MobilePhone"]);
					if (reader["PSN_Email"] != DBNull.Value) _zjs_ApplyChangeMDL.PSN_Email = Convert.ToString(reader["PSN_Email"]);
					if (reader["PSN_RegisterNo"] != DBNull.Value) _zjs_ApplyChangeMDL.PSN_RegisterNo = Convert.ToString(reader["PSN_RegisterNo"]);
					if (reader["ValidDate"] != DBNull.Value) _zjs_ApplyChangeMDL.ValidDate = Convert.ToDateTime(reader["ValidDate"]);
					if (reader["ChangeReason"] != DBNull.Value) _zjs_ApplyChangeMDL.ChangeReason = Convert.ToString(reader["ChangeReason"]);
					if (reader["OldENT_Name"] != DBNull.Value) _zjs_ApplyChangeMDL.OldENT_Name = Convert.ToString(reader["OldENT_Name"]);
					if (reader["OldEND_Addess"] != DBNull.Value) _zjs_ApplyChangeMDL.OldEND_Addess = Convert.ToString(reader["OldEND_Addess"]);
					if (reader["OldLinkMan"] != DBNull.Value) _zjs_ApplyChangeMDL.OldLinkMan = Convert.ToString(reader["OldLinkMan"]);
					if (reader["OldENT_Telephone"] != DBNull.Value) _zjs_ApplyChangeMDL.OldENT_Telephone = Convert.ToString(reader["OldENT_Telephone"]);
					if (reader["OldFR"] != DBNull.Value) _zjs_ApplyChangeMDL.OldFR = Convert.ToString(reader["OldFR"]);
					if (reader["OldENT_Type"] != DBNull.Value) _zjs_ApplyChangeMDL.OldENT_Type = Convert.ToString(reader["OldENT_Type"]);
					if (reader["OldENT_Sort"] != DBNull.Value) _zjs_ApplyChangeMDL.OldENT_Sort = Convert.ToString(reader["OldENT_Sort"]);
					if (reader["OldENT_Grade"] != DBNull.Value) _zjs_ApplyChangeMDL.OldENT_Grade = Convert.ToString(reader["OldENT_Grade"]);
					if (reader["OldENT_QualificationCertificateNo"] != DBNull.Value) _zjs_ApplyChangeMDL.OldENT_QualificationCertificateNo = Convert.ToString(reader["OldENT_QualificationCertificateNo"]);
					if (reader["ENT_Name"] != DBNull.Value) _zjs_ApplyChangeMDL.ENT_Name = Convert.ToString(reader["ENT_Name"]);
					if (reader["FR"] != DBNull.Value) _zjs_ApplyChangeMDL.FR = Convert.ToString(reader["FR"]);
					if (reader["END_Addess"] != DBNull.Value) _zjs_ApplyChangeMDL.END_Addess = Convert.ToString(reader["END_Addess"]);
					if (reader["LinkMan"] != DBNull.Value) _zjs_ApplyChangeMDL.LinkMan = Convert.ToString(reader["LinkMan"]);
					if (reader["ENT_Telephone"] != DBNull.Value) _zjs_ApplyChangeMDL.ENT_Telephone = Convert.ToString(reader["ENT_Telephone"]);
					if (reader["ENT_Type"] != DBNull.Value) _zjs_ApplyChangeMDL.ENT_Type = Convert.ToString(reader["ENT_Type"]);
					if (reader["ENT_Economic_Nature"] != DBNull.Value) _zjs_ApplyChangeMDL.ENT_Economic_Nature = Convert.ToString(reader["ENT_Economic_Nature"]);
					if (reader["ENT_Sort"] != DBNull.Value) _zjs_ApplyChangeMDL.ENT_Sort = Convert.ToString(reader["ENT_Sort"]);
					if (reader["ENT_Grade"] != DBNull.Value) _zjs_ApplyChangeMDL.ENT_Grade = Convert.ToString(reader["ENT_Grade"]);
					if (reader["ENT_QualificationCertificateNo"] != DBNull.Value) _zjs_ApplyChangeMDL.ENT_QualificationCertificateNo = Convert.ToString(reader["ENT_QualificationCertificateNo"]);
					if (reader["IfOutside"] != DBNull.Value) _zjs_ApplyChangeMDL.IfOutside = Convert.ToBoolean(reader["IfOutside"]);
					if (reader["ENT_NameFrom"] != DBNull.Value) _zjs_ApplyChangeMDL.ENT_NameFrom = Convert.ToString(reader["ENT_NameFrom"]);
					if (reader["ENT_NameTo"] != DBNull.Value) _zjs_ApplyChangeMDL.ENT_NameTo = Convert.ToString(reader["ENT_NameTo"]);
					if (reader["PSN_NameFrom"] != DBNull.Value) _zjs_ApplyChangeMDL.PSN_NameFrom = Convert.ToString(reader["PSN_NameFrom"]);
					if (reader["PSN_NameTo"] != DBNull.Value) _zjs_ApplyChangeMDL.PSN_NameTo = Convert.ToString(reader["PSN_NameTo"]);
					if (reader["FromENT_City"] != DBNull.Value) _zjs_ApplyChangeMDL.FromENT_City = Convert.ToString(reader["FromENT_City"]);
					if (reader["ToENT_City"] != DBNull.Value) _zjs_ApplyChangeMDL.ToENT_City = Convert.ToString(reader["ToENT_City"]);
					if (reader["FromPSN_Sex"] != DBNull.Value) _zjs_ApplyChangeMDL.FromPSN_Sex = Convert.ToString(reader["FromPSN_Sex"]);
					if (reader["ToPSN_Sex"] != DBNull.Value) _zjs_ApplyChangeMDL.ToPSN_Sex = Convert.ToString(reader["ToPSN_Sex"]);
					if (reader["FromPSN_BirthDate"] != DBNull.Value) _zjs_ApplyChangeMDL.FromPSN_BirthDate = Convert.ToDateTime(reader["FromPSN_BirthDate"]);
					if (reader["ToPSN_BirthDate"] != DBNull.Value) _zjs_ApplyChangeMDL.ToPSN_BirthDate = Convert.ToDateTime(reader["ToPSN_BirthDate"]);
					if (reader["FromPSN_CertificateType"] != DBNull.Value) _zjs_ApplyChangeMDL.FromPSN_CertificateType = Convert.ToString(reader["FromPSN_CertificateType"]);
					if (reader["ToPSN_CertificateType"] != DBNull.Value) _zjs_ApplyChangeMDL.ToPSN_CertificateType = Convert.ToString(reader["ToPSN_CertificateType"]);
					if (reader["FromPSN_CertificateNO"] != DBNull.Value) _zjs_ApplyChangeMDL.FromPSN_CertificateNO = Convert.ToString(reader["FromPSN_CertificateNO"]);
					if (reader["ToPSN_CertificateNO"] != DBNull.Value) _zjs_ApplyChangeMDL.ToPSN_CertificateNO = Convert.ToString(reader["ToPSN_CertificateNO"]);
					if (reader["ZGZSBH"] != DBNull.Value) _zjs_ApplyChangeMDL.ZGZSBH = Convert.ToString(reader["ZGZSBH"]);
					if (reader["To_ZGZSBH"] != DBNull.Value) _zjs_ApplyChangeMDL.To_ZGZSBH = Convert.ToString(reader["To_ZGZSBH"]);
					if (reader["FromEND_Addess"] != DBNull.Value) _zjs_ApplyChangeMDL.FromEND_Addess = Convert.ToString(reader["FromEND_Addess"]);
					if (reader["ToEND_Addess"] != DBNull.Value) _zjs_ApplyChangeMDL.ToEND_Addess = Convert.ToString(reader["ToEND_Addess"]);
                }
				reader.Close();
                db.Close();
                return _zjs_ApplyChangeMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.zjs_ApplyChange", "*", filterWhereString, orderBy == "" ? " ApplyID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.zjs_ApplyChange", filterWhereString);
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
        public static DataTable GetApplyChangeList(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.View_zjs_ApplyChangeUnitName", "*", filterWhereString, orderBy == "" ? " ENT_OrganizationsCode" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectApplyChangeCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.View_zjs_ApplyChangeUnitName", filterWhereString);
        }

        /// <summary>
        /// 根据专业1申请单ID，复制到专业2申请单
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="ZhuanYe1_ApplyID">专业1申请单ID</param>
        /// <param name="ApplyID">专业2申请单ID</param>
        /// <param name="PSN_RegisterNo">专业2注册编号</param>
        /// <param name="ValidDate">专业2有效期至</param>
        /// <returns></returns>
        public static int InsertZhuanYe2ApplyChangeWithZhuanYe1ApplyChangeID(DbTransaction tran, string ZhuanYe1_ApplyID, string ApplyID, string PSN_RegisterNo, DateTime ValidDate)
        {
            string sql = @"INSERT INTO [dbo].[zjs_ApplyChange]([ApplyID],[PSN_MobilePhone],[PSN_Email],[PSN_RegisterNo],[ValidDate],[ChangeReason],[OldENT_Name],[OldEND_Addess],[OldLinkMan],[OldENT_Telephone],[OldFR],[OldENT_Type],[OldENT_Sort],[OldENT_Grade],[OldENT_QualificationCertificateNo],[ENT_Name],[FR],[END_Addess],[LinkMan],[ENT_Telephone],[ENT_Type],[ENT_Economic_Nature],[ENT_Sort],[ENT_Grade],[ENT_QualificationCertificateNo],[IfOutside],[ENT_NameFrom],[ENT_NameTo],[PSN_NameFrom],[PSN_NameTo],[FromENT_City],[ToENT_City],[FromPSN_Sex],[ToPSN_Sex],[FromPSN_BirthDate],[ToPSN_BirthDate],[FromPSN_CertificateType],[ToPSN_CertificateType],[FromPSN_CertificateNO],[ToPSN_CertificateNO],[ZGZSBH],[To_ZGZSBH],[FromEND_Addess],[ToEND_Addess]) 
                                                        SELECT @ApplyID,[PSN_MobilePhone],[PSN_Email],@PSN_RegisterNo,@ValidDate,[ChangeReason],[OldENT_Name],[OldEND_Addess],[OldLinkMan],[OldENT_Telephone],[OldFR],[OldENT_Type],[OldENT_Sort],[OldENT_Grade],[OldENT_QualificationCertificateNo],[ENT_Name],[FR],[END_Addess],[LinkMan],[ENT_Telephone],[ENT_Type],[ENT_Economic_Nature],[ENT_Sort],[ENT_Grade],[ENT_QualificationCertificateNo],[IfOutside],[ENT_NameFrom],[ENT_NameTo],[PSN_NameFrom],[PSN_NameTo],[FromENT_City],[ToENT_City],[FromPSN_Sex],[ToPSN_Sex],[FromPSN_BirthDate],[ToPSN_BirthDate],[FromPSN_CertificateType],[ToPSN_CertificateType],[FromPSN_CertificateNO],[ToPSN_CertificateNO],[ZGZSBH],[To_ZGZSBH],[FromEND_Addess],[ToEND_Addess]  
                                                        FROM [dbo].[zjs_ApplyChange]
                                                        WHERE ApplyID = @ZhuanYe1_ApplyID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ZhuanYe1_ApplyID", DbType.String, ZhuanYe1_ApplyID));
            p.Add(db.CreateParameter("ApplyID", DbType.String, ApplyID));
            p.Add(db.CreateParameter("PSN_RegisterNo", DbType.String, PSN_RegisterNo));
            p.Add(db.CreateParameter("ValidDate", DbType.DateTime, ValidDate));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        ///  根据专业1申请单ID，更新到专业2申请单
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ApplyID1">专业1申请单ID</param>
        /// <param name="ApplyID2">专业2申请单ID</param>
        /// <returns></returns>
        public static int UpdateZhuanYe2ApplyChangeWithZhuanYe1ApplyID(DbTransaction tran, string ApplyID1, string ApplyID2)
        {
            string sql = @"UPDATE a2
                                set a2.[PSN_MobilePhone] =a1.[PSN_MobilePhone]
                                ,a2.[PSN_Email]          =a1.[PSN_Email]
                                ,a2.[ChangeReason]       =a1.[ChangeReason]
                                ,a2.[OldENT_Name]        =a1.[OldENT_Name]
                                ,a2.[OldEND_Addess]      =a1.[OldEND_Addess]
                                ,a2.[OldLinkMan]         =a1.[OldLinkMan]
                                ,a2.[OldENT_Telephone]   =a1.[OldENT_Telephone]
                                ,a2.[OldFR]              =a1.[OldFR]
                                ,a2.[OldENT_Type]        =a1.[OldENT_Type]
                                ,a2.[ENT_Name]           =a1.[ENT_Name]
                                ,a2.[FR]                 =a1.[FR]
                                ,a2.[END_Addess]         =a1.[END_Addess]
                                ,a2.[LinkMan]            =a1.[LinkMan]
                                ,a2.[ENT_Telephone]      =a1.[ENT_Telephone]
                                ,a2.[ENT_Type]           =a1.[ENT_Type]
                                ,a2.[ENT_Economic_Nature]=a1.[ENT_Economic_Nature]
                             from dbo.zjs_ApplyChange a1
                             inner join  dbo.zjs_ApplyChange a2 on a1.ApplyID=@ApplyID1 and a2.ApplyID=@ApplyID2 and a1.[PSN_RegisterNo] = a2.[PSN_RegisterNo] 
                             WHERE a1.ApplyID = @ApplyID1";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID1", DbType.String, ApplyID1));
            p.Add(db.CreateParameter("ApplyID2", DbType.String, ApplyID2));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        #endregion
    }
}
