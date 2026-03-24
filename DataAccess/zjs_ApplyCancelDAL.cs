using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--zjs_ApplyCancelDAL(填写类描述)
	/// </summary>
    public class zjs_ApplyCancelDAL
    {
        public zjs_ApplyCancelDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_zjs_ApplyCancelMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(zjs_ApplyCancelMDL _zjs_ApplyCancelMDL)
		{
		    return Insert(null,_zjs_ApplyCancelMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_zjs_ApplyCancelMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,zjs_ApplyCancelMDL _zjs_ApplyCancelMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.zjs_ApplyCancel(ApplyID,END_Addess,ENT_Postcode,ENT_Telephone,PSN_MobilePhone,PSN_RegisterCertificateNo,PSN_RegisterNO,RegisterValidity,CancelReason,PSN_Email,LinkMan,ApplyManType,FR,ENT_Economic_Nature)
			VALUES (@ApplyID,@END_Addess,@ENT_Postcode,@ENT_Telephone,@PSN_MobilePhone,@PSN_RegisterCertificateNo,@PSN_RegisterNO,@RegisterValidity,@CancelReason,@PSN_Email,@LinkMan,@ApplyManType,@FR,@ENT_Economic_Nature)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ApplyID",DbType.String, _zjs_ApplyCancelMDL.ApplyID));
			p.Add(db.CreateParameter("END_Addess",DbType.String, _zjs_ApplyCancelMDL.END_Addess));
			p.Add(db.CreateParameter("ENT_Postcode",DbType.String, _zjs_ApplyCancelMDL.ENT_Postcode));
			p.Add(db.CreateParameter("ENT_Telephone",DbType.String, _zjs_ApplyCancelMDL.ENT_Telephone));
			p.Add(db.CreateParameter("PSN_MobilePhone",DbType.String, _zjs_ApplyCancelMDL.PSN_MobilePhone));
			p.Add(db.CreateParameter("PSN_RegisterCertificateNo",DbType.String, _zjs_ApplyCancelMDL.PSN_RegisterCertificateNo));
			p.Add(db.CreateParameter("PSN_RegisterNO",DbType.String, _zjs_ApplyCancelMDL.PSN_RegisterNO));
			p.Add(db.CreateParameter("RegisterValidity",DbType.DateTime, _zjs_ApplyCancelMDL.RegisterValidity));
			p.Add(db.CreateParameter("CancelReason",DbType.String, _zjs_ApplyCancelMDL.CancelReason));
			p.Add(db.CreateParameter("PSN_Email",DbType.String, _zjs_ApplyCancelMDL.PSN_Email));
			p.Add(db.CreateParameter("LinkMan",DbType.String, _zjs_ApplyCancelMDL.LinkMan));
			p.Add(db.CreateParameter("ApplyManType",DbType.String, _zjs_ApplyCancelMDL.ApplyManType));
			p.Add(db.CreateParameter("FR",DbType.String, _zjs_ApplyCancelMDL.FR));
			p.Add(db.CreateParameter("ENT_Economic_Nature",DbType.String, _zjs_ApplyCancelMDL.ENT_Economic_Nature));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_zjs_ApplyCancelMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(zjs_ApplyCancelMDL _zjs_ApplyCancelMDL)
		{
			return Update(null,_zjs_ApplyCancelMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_zjs_ApplyCancelMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,zjs_ApplyCancelMDL _zjs_ApplyCancelMDL)
		{
			string sql = @"
			UPDATE dbo.zjs_ApplyCancel
				SET	END_Addess = @END_Addess,ENT_Postcode = @ENT_Postcode,ENT_Telephone = @ENT_Telephone,PSN_MobilePhone = @PSN_MobilePhone,PSN_RegisterCertificateNo = @PSN_RegisterCertificateNo,PSN_RegisterNO = @PSN_RegisterNO,RegisterValidity = @RegisterValidity,CancelReason = @CancelReason,PSN_Email = @PSN_Email,LinkMan = @LinkMan,ApplyManType = @ApplyManType,FR = @FR,ENT_Economic_Nature = @ENT_Economic_Nature
			WHERE
				ApplyID = @ApplyID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ApplyID",DbType.String, _zjs_ApplyCancelMDL.ApplyID));
			p.Add(db.CreateParameter("END_Addess",DbType.String, _zjs_ApplyCancelMDL.END_Addess));
			p.Add(db.CreateParameter("ENT_Postcode",DbType.String, _zjs_ApplyCancelMDL.ENT_Postcode));
			p.Add(db.CreateParameter("ENT_Telephone",DbType.String, _zjs_ApplyCancelMDL.ENT_Telephone));
			p.Add(db.CreateParameter("PSN_MobilePhone",DbType.String, _zjs_ApplyCancelMDL.PSN_MobilePhone));
			p.Add(db.CreateParameter("PSN_RegisterCertificateNo",DbType.String, _zjs_ApplyCancelMDL.PSN_RegisterCertificateNo));
			p.Add(db.CreateParameter("PSN_RegisterNO",DbType.String, _zjs_ApplyCancelMDL.PSN_RegisterNO));
			p.Add(db.CreateParameter("RegisterValidity",DbType.DateTime, _zjs_ApplyCancelMDL.RegisterValidity));
			p.Add(db.CreateParameter("CancelReason",DbType.String, _zjs_ApplyCancelMDL.CancelReason));
			p.Add(db.CreateParameter("PSN_Email",DbType.String, _zjs_ApplyCancelMDL.PSN_Email));
			p.Add(db.CreateParameter("LinkMan",DbType.String, _zjs_ApplyCancelMDL.LinkMan));
			p.Add(db.CreateParameter("ApplyManType",DbType.String, _zjs_ApplyCancelMDL.ApplyManType));
			p.Add(db.CreateParameter("FR",DbType.String, _zjs_ApplyCancelMDL.FR));
			p.Add(db.CreateParameter("ENT_Economic_Nature",DbType.String, _zjs_ApplyCancelMDL.ENT_Economic_Nature));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="zjs_ApplyCancelID">主键</param>
		/// <returns></returns>
        public static int Delete( string ApplyID )
		{
			return Delete(null, ApplyID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="zjs_ApplyCancelID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, string ApplyID)
		{
			string sql=@"DELETE FROM dbo.zjs_ApplyCancel WHERE ApplyID = @ApplyID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ApplyID",DbType.String,ApplyID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="_zjs_ApplyCancelMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(zjs_ApplyCancelMDL _zjs_ApplyCancelMDL)
		{
			return Delete(null,_zjs_ApplyCancelMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_zjs_ApplyCancelMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,zjs_ApplyCancelMDL _zjs_ApplyCancelMDL)
		{
			string sql=@"DELETE FROM dbo.zjs_ApplyCancel WHERE ApplyID = @ApplyID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ApplyID",DbType.String,_zjs_ApplyCancelMDL.ApplyID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="zjs_ApplyCancelID">主键</param>
        public static zjs_ApplyCancelMDL GetObject( string ApplyID )
		{
			string sql=@"
			SELECT ApplyID,END_Addess,ENT_Postcode,ENT_Telephone,PSN_MobilePhone,PSN_RegisterCertificateNo,PSN_RegisterNO,RegisterValidity,CancelReason,PSN_Email,LinkMan,ApplyManType,FR,ENT_Economic_Nature
			FROM dbo.zjs_ApplyCancel
			WHERE ApplyID = @ApplyID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.String, ApplyID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                zjs_ApplyCancelMDL _zjs_ApplyCancelMDL = null;
                if (reader.Read())
                {
                    _zjs_ApplyCancelMDL = new zjs_ApplyCancelMDL();
					if (reader["ApplyID"] != DBNull.Value) _zjs_ApplyCancelMDL.ApplyID = Convert.ToString(reader["ApplyID"]);
					if (reader["END_Addess"] != DBNull.Value) _zjs_ApplyCancelMDL.END_Addess = Convert.ToString(reader["END_Addess"]);
					if (reader["ENT_Postcode"] != DBNull.Value) _zjs_ApplyCancelMDL.ENT_Postcode = Convert.ToString(reader["ENT_Postcode"]);
					if (reader["ENT_Telephone"] != DBNull.Value) _zjs_ApplyCancelMDL.ENT_Telephone = Convert.ToString(reader["ENT_Telephone"]);
					if (reader["PSN_MobilePhone"] != DBNull.Value) _zjs_ApplyCancelMDL.PSN_MobilePhone = Convert.ToString(reader["PSN_MobilePhone"]);
					if (reader["PSN_RegisterCertificateNo"] != DBNull.Value) _zjs_ApplyCancelMDL.PSN_RegisterCertificateNo = Convert.ToString(reader["PSN_RegisterCertificateNo"]);
					if (reader["PSN_RegisterNO"] != DBNull.Value) _zjs_ApplyCancelMDL.PSN_RegisterNO = Convert.ToString(reader["PSN_RegisterNO"]);
					if (reader["RegisterValidity"] != DBNull.Value) _zjs_ApplyCancelMDL.RegisterValidity = Convert.ToDateTime(reader["RegisterValidity"]);
					if (reader["CancelReason"] != DBNull.Value) _zjs_ApplyCancelMDL.CancelReason = Convert.ToString(reader["CancelReason"]);
					if (reader["PSN_Email"] != DBNull.Value) _zjs_ApplyCancelMDL.PSN_Email = Convert.ToString(reader["PSN_Email"]);
					if (reader["LinkMan"] != DBNull.Value) _zjs_ApplyCancelMDL.LinkMan = Convert.ToString(reader["LinkMan"]);
					if (reader["ApplyManType"] != DBNull.Value) _zjs_ApplyCancelMDL.ApplyManType = Convert.ToString(reader["ApplyManType"]);
					if (reader["FR"] != DBNull.Value) _zjs_ApplyCancelMDL.FR = Convert.ToString(reader["FR"]);
					if (reader["ENT_Economic_Nature"] != DBNull.Value) _zjs_ApplyCancelMDL.ENT_Economic_Nature = Convert.ToString(reader["ENT_Economic_Nature"]);
                }
				reader.Close();
                db.Close();
                return _zjs_ApplyCancelMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.zjs_ApplyCancel", "*", filterWhereString, orderBy == "" ? " ApplyID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.zjs_ApplyCancel", filterWhereString);
        }
        
        #region 自定义方法
        
        #endregion
    }
}
