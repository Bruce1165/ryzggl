using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--CheckObjectDAL(填写类描述)
	/// </summary>
    public class CheckObjectDAL
    {
        public CheckObjectDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_CheckObjectMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(CheckObjectMDL _CheckObjectMDL)
		{
		    return Insert(null,_CheckObjectMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_CheckObjectMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,CheckObjectMDL _CheckObjectMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.CheckObject(CheckID,CheckYear,PSN_RegisterNo,PostTypeName,PSN_Name,PSN_CertificateNO,ENT_Name,ProfessionWithValid,ApplyType,ApplyTime,NoticeDate)
			VALUES (@CheckID,@CheckYear,@PSN_RegisterNo,@PostTypeName,@PSN_Name,@PSN_CertificateNO,@ENT_Name,@ProfessionWithValid,@ApplyType,@ApplyTime,@NoticeDate)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("CheckID",DbType.String, _CheckObjectMDL.CheckID));
			p.Add(db.CreateParameter("CheckYear",DbType.Int32, _CheckObjectMDL.CheckYear));
			p.Add(db.CreateParameter("PSN_RegisterNo",DbType.String, _CheckObjectMDL.PSN_RegisterNo));
			p.Add(db.CreateParameter("PostTypeName",DbType.String, _CheckObjectMDL.PostTypeName));
			p.Add(db.CreateParameter("PSN_Name",DbType.String, _CheckObjectMDL.PSN_Name));
			p.Add(db.CreateParameter("PSN_CertificateNO",DbType.String, _CheckObjectMDL.PSN_CertificateNO));
			p.Add(db.CreateParameter("ENT_Name",DbType.String, _CheckObjectMDL.ENT_Name));
			p.Add(db.CreateParameter("ProfessionWithValid",DbType.String, _CheckObjectMDL.ProfessionWithValid));
			p.Add(db.CreateParameter("ApplyType",DbType.String, _CheckObjectMDL.ApplyType));
			p.Add(db.CreateParameter("ApplyTime",DbType.DateTime, _CheckObjectMDL.ApplyTime));
			p.Add(db.CreateParameter("NoticeDate",DbType.DateTime, _CheckObjectMDL.NoticeDate));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_CheckObjectMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(CheckObjectMDL _CheckObjectMDL)
		{
			return Update(null,_CheckObjectMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_CheckObjectMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,CheckObjectMDL _CheckObjectMDL)
		{
			string sql = @"
			UPDATE dbo.CheckObject
				SET	CheckYear = @CheckYear,PSN_RegisterNo = @PSN_RegisterNo,PostTypeName = @PostTypeName,PSN_Name = @PSN_Name,PSN_CertificateNO = @PSN_CertificateNO,ENT_Name = @ENT_Name,ProfessionWithValid = @ProfessionWithValid,ApplyType = @ApplyType,ApplyTime = @ApplyTime,NoticeDate = @NoticeDate
			WHERE
				CheckID = @CheckID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("CheckID",DbType.String, _CheckObjectMDL.CheckID));
			p.Add(db.CreateParameter("CheckYear",DbType.Int32, _CheckObjectMDL.CheckYear));
			p.Add(db.CreateParameter("PSN_RegisterNo",DbType.String, _CheckObjectMDL.PSN_RegisterNo));
			p.Add(db.CreateParameter("PostTypeName",DbType.String, _CheckObjectMDL.PostTypeName));
			p.Add(db.CreateParameter("PSN_Name",DbType.String, _CheckObjectMDL.PSN_Name));
			p.Add(db.CreateParameter("PSN_CertificateNO",DbType.String, _CheckObjectMDL.PSN_CertificateNO));
			p.Add(db.CreateParameter("ENT_Name",DbType.String, _CheckObjectMDL.ENT_Name));
			p.Add(db.CreateParameter("ProfessionWithValid",DbType.String, _CheckObjectMDL.ProfessionWithValid));
			p.Add(db.CreateParameter("ApplyType",DbType.String, _CheckObjectMDL.ApplyType));
			p.Add(db.CreateParameter("ApplyTime",DbType.DateTime, _CheckObjectMDL.ApplyTime));
			p.Add(db.CreateParameter("NoticeDate",DbType.DateTime, _CheckObjectMDL.NoticeDate));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="CheckObjectID">主键</param>
		/// <returns></returns>
        public static int Delete( string CheckID )
		{
			return Delete(null, CheckID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="CheckObjectID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, string CheckID)
		{
			string sql=@"DELETE FROM dbo.CheckObject WHERE CheckID = @CheckID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("CheckID",DbType.String,CheckID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="_CheckObjectMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(CheckObjectMDL _CheckObjectMDL)
		{
			return Delete(null,_CheckObjectMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_CheckObjectMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,CheckObjectMDL _CheckObjectMDL)
		{
			string sql=@"DELETE FROM dbo.CheckObject WHERE CheckID = @CheckID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("CheckID",DbType.String,_CheckObjectMDL.CheckID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="CheckObjectID">主键</param>
        public static CheckObjectMDL GetObject( string CheckID )
		{
			string sql=@"
			SELECT CheckID,CheckYear,PSN_RegisterNo,PostTypeName,PSN_Name,PSN_CertificateNO,ENT_Name,ProfessionWithValid,ApplyType,ApplyTime,NoticeDate
			FROM dbo.CheckObject
			WHERE CheckID = @CheckID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("CheckID",DbType.String,CheckID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                CheckObjectMDL _CheckObjectMDL = null;
                if (reader.Read())
                {
                    _CheckObjectMDL = new CheckObjectMDL();
					if (reader["CheckID"] != DBNull.Value) _CheckObjectMDL.CheckID = Convert.ToString(reader["CheckID"]);
					if (reader["CheckYear"] != DBNull.Value) _CheckObjectMDL.CheckYear = Convert.ToInt32(reader["CheckYear"]);
					if (reader["PSN_RegisterNo"] != DBNull.Value) _CheckObjectMDL.PSN_RegisterNo = Convert.ToString(reader["PSN_RegisterNo"]);
					if (reader["PostTypeName"] != DBNull.Value) _CheckObjectMDL.PostTypeName = Convert.ToString(reader["PostTypeName"]);
					if (reader["PSN_Name"] != DBNull.Value) _CheckObjectMDL.PSN_Name = Convert.ToString(reader["PSN_Name"]);
					if (reader["PSN_CertificateNO"] != DBNull.Value) _CheckObjectMDL.PSN_CertificateNO = Convert.ToString(reader["PSN_CertificateNO"]);
					if (reader["ENT_Name"] != DBNull.Value) _CheckObjectMDL.ENT_Name = Convert.ToString(reader["ENT_Name"]);
					if (reader["ProfessionWithValid"] != DBNull.Value) _CheckObjectMDL.ProfessionWithValid = Convert.ToString(reader["ProfessionWithValid"]);
					if (reader["ApplyType"] != DBNull.Value) _CheckObjectMDL.ApplyType = Convert.ToString(reader["ApplyType"]);
					if (reader["ApplyTime"] != DBNull.Value) _CheckObjectMDL.ApplyTime = Convert.ToDateTime(reader["ApplyTime"]);
					if (reader["NoticeDate"] != DBNull.Value) _CheckObjectMDL.NoticeDate = Convert.ToDateTime(reader["NoticeDate"]);
                }
				reader.Close();
                db.Close();
                return _CheckObjectMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.CheckObject", "*", filterWhereString, orderBy == "" ? " CheckYear desc,PSN_RegisterNo,CheckID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.CheckObject", filterWhereString);
        }
        
        #region 自定义方法
        
        #endregion
    }
}
