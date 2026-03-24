using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--JxjyBaseDAL(个人委培教育记录)
	/// </summary>
    public class JxjyBaseDAL
    {
        public JxjyBaseDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_JxjyBaseMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(JxjyBaseMDL _JxjyBaseMDL)
		{
		    return Insert(null,_JxjyBaseMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_JxjyBaseMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,JxjyBaseMDL _JxjyBaseMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.JxjyBase(PostTypeID,WorkerName,WorkerCertificateCode,PostTypeName,PostName,CertificateCode,ValidEndDate,UnitName,cjsj,ApplyID)
			VALUES (@PostTypeID,@WorkerName,@WorkerCertificateCode,@PostTypeName,@PostName,@CertificateCode,@ValidEndDate,@UnitName,@cjsj,@ApplyID);SELECT @BaseID = @@IDENTITY";

		    List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateOutParameter("BaseID", DbType.Int64));
			p.Add(db.CreateParameter("PostTypeID",DbType.Int32, _JxjyBaseMDL.PostTypeID));
			p.Add(db.CreateParameter("WorkerName",DbType.String, _JxjyBaseMDL.WorkerName));
			p.Add(db.CreateParameter("WorkerCertificateCode",DbType.String, _JxjyBaseMDL.WorkerCertificateCode));
			p.Add(db.CreateParameter("PostTypeName",DbType.String, _JxjyBaseMDL.PostTypeName));
			p.Add(db.CreateParameter("PostName",DbType.String, _JxjyBaseMDL.PostName));
			p.Add(db.CreateParameter("CertificateCode",DbType.String, _JxjyBaseMDL.CertificateCode));
			p.Add(db.CreateParameter("ValidEndDate",DbType.DateTime, _JxjyBaseMDL.ValidEndDate));
			p.Add(db.CreateParameter("UnitName",DbType.String, _JxjyBaseMDL.UnitName));
			p.Add(db.CreateParameter("cjsj",DbType.DateTime, _JxjyBaseMDL.cjsj));
			p.Add(db.CreateParameter("ApplyID",DbType.String, _JxjyBaseMDL.ApplyID));
            int rtn = db.GetExcuteNonQuery(tran, sql, p.ToArray());
            _JxjyBaseMDL.BaseID = Convert.ToInt64(p[0].Value);
            return rtn;
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_JxjyBaseMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(JxjyBaseMDL _JxjyBaseMDL)
		{
			return Update(null,_JxjyBaseMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_JxjyBaseMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,JxjyBaseMDL _JxjyBaseMDL)
		{
			string sql = @"
			UPDATE dbo.JxjyBase
				SET	PostTypeID = @PostTypeID,WorkerName = @WorkerName,WorkerCertificateCode = @WorkerCertificateCode,PostTypeName = @PostTypeName,PostName = @PostName,CertificateCode = @CertificateCode,ValidEndDate = @ValidEndDate,UnitName = @UnitName,cjsj = @cjsj,ApplyID = @ApplyID
			WHERE
				BaseID = @BaseID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("BaseID",DbType.Int64, _JxjyBaseMDL.BaseID));
			p.Add(db.CreateParameter("PostTypeID",DbType.Int32, _JxjyBaseMDL.PostTypeID));
			p.Add(db.CreateParameter("WorkerName",DbType.String, _JxjyBaseMDL.WorkerName));
			p.Add(db.CreateParameter("WorkerCertificateCode",DbType.String, _JxjyBaseMDL.WorkerCertificateCode));
			p.Add(db.CreateParameter("PostTypeName",DbType.String, _JxjyBaseMDL.PostTypeName));
			p.Add(db.CreateParameter("PostName",DbType.String, _JxjyBaseMDL.PostName));
			p.Add(db.CreateParameter("CertificateCode",DbType.String, _JxjyBaseMDL.CertificateCode));
			p.Add(db.CreateParameter("ValidEndDate",DbType.DateTime, _JxjyBaseMDL.ValidEndDate));
			p.Add(db.CreateParameter("UnitName",DbType.String, _JxjyBaseMDL.UnitName));
			p.Add(db.CreateParameter("cjsj",DbType.DateTime, _JxjyBaseMDL.cjsj));
			p.Add(db.CreateParameter("ApplyID",DbType.String, _JxjyBaseMDL.ApplyID));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="JxjyBaseID">主键</param>
		/// <returns></returns>
        public static int Delete( long BaseID )
		{
			return Delete(null, BaseID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="JxjyBaseID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, long BaseID)
		{
			string sql=@"DELETE FROM dbo.JxjyBase WHERE BaseID = @BaseID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("BaseID",DbType.Int64,BaseID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="_JxjyBaseMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(JxjyBaseMDL _JxjyBaseMDL)
		{
			return Delete(null,_JxjyBaseMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_JxjyBaseMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,JxjyBaseMDL _JxjyBaseMDL)
		{
			string sql=@"DELETE FROM dbo.JxjyBase WHERE BaseID = @BaseID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("BaseID",DbType.Int64,_JxjyBaseMDL.BaseID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="JxjyBaseID">主键</param>
        public static JxjyBaseMDL GetObject( long BaseID )
		{
			string sql=@"
			SELECT BaseID,PostTypeID,WorkerName,WorkerCertificateCode,PostTypeName,PostName,CertificateCode,ValidEndDate,UnitName,cjsj,ApplyID
			FROM dbo.JxjyBase
			WHERE BaseID = @BaseID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("BaseID",DbType.Int64,BaseID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                JxjyBaseMDL _JxjyBaseMDL = null;
                if (reader.Read())
                {
                    _JxjyBaseMDL = new JxjyBaseMDL();
					if (reader["BaseID"] != DBNull.Value) _JxjyBaseMDL.BaseID = Convert.ToInt64(reader["BaseID"]);
					if (reader["PostTypeID"] != DBNull.Value) _JxjyBaseMDL.PostTypeID = Convert.ToInt32(reader["PostTypeID"]);
					if (reader["WorkerName"] != DBNull.Value) _JxjyBaseMDL.WorkerName = Convert.ToString(reader["WorkerName"]);
					if (reader["WorkerCertificateCode"] != DBNull.Value) _JxjyBaseMDL.WorkerCertificateCode = Convert.ToString(reader["WorkerCertificateCode"]);
					if (reader["PostTypeName"] != DBNull.Value) _JxjyBaseMDL.PostTypeName = Convert.ToString(reader["PostTypeName"]);
					if (reader["PostName"] != DBNull.Value) _JxjyBaseMDL.PostName = Convert.ToString(reader["PostName"]);
					if (reader["CertificateCode"] != DBNull.Value) _JxjyBaseMDL.CertificateCode = Convert.ToString(reader["CertificateCode"]);
					if (reader["ValidEndDate"] != DBNull.Value) _JxjyBaseMDL.ValidEndDate = Convert.ToDateTime(reader["ValidEndDate"]);
					if (reader["UnitName"] != DBNull.Value) _JxjyBaseMDL.UnitName = Convert.ToString(reader["UnitName"]);
					if (reader["cjsj"] != DBNull.Value) _JxjyBaseMDL.cjsj = Convert.ToDateTime(reader["cjsj"]);
					if (reader["ApplyID"] != DBNull.Value) _JxjyBaseMDL.ApplyID = Convert.ToString(reader["ApplyID"]);
                }
				reader.Close();
                db.Close();
                return _JxjyBaseMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.JxjyBase", "*", filterWhereString, orderBy == "" ? " BaseID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.JxjyBase", filterWhereString);
        }
        
        #region 自定义方法

        /// <summary>
        /// 根据申请单ID获取单个实体
        /// </summary>
        /// <param name="PostTypeID"></param>
        /// <param name="ApplyID">申请单ID</param>
        /// <returns></returns>
        public static JxjyBaseMDL GetObjectByApplyID(int PostTypeID,string ApplyID)
        {
            string sql = @"
			SELECT BaseID,PostTypeID,WorkerName,WorkerCertificateCode,PostTypeName,PostName,CertificateCode,ValidEndDate,UnitName,cjsj,ApplyID
			FROM dbo.JxjyBase
			WHERE ApplyID = @ApplyID and PostTypeID = @PostTypeID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PostTypeID", DbType.Int32, PostTypeID));
            p.Add(db.CreateParameter("ApplyID", DbType.String, ApplyID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                JxjyBaseMDL _JxjyBaseMDL = null;
                if (reader.Read())
                {
                    _JxjyBaseMDL = new JxjyBaseMDL();
                    if (reader["BaseID"] != DBNull.Value) _JxjyBaseMDL.BaseID = Convert.ToInt64(reader["BaseID"]);
                    if (reader["PostTypeID"] != DBNull.Value) _JxjyBaseMDL.PostTypeID = Convert.ToInt32(reader["PostTypeID"]);
                    if (reader["WorkerName"] != DBNull.Value) _JxjyBaseMDL.WorkerName = Convert.ToString(reader["WorkerName"]);
                    if (reader["WorkerCertificateCode"] != DBNull.Value) _JxjyBaseMDL.WorkerCertificateCode = Convert.ToString(reader["WorkerCertificateCode"]);
                    if (reader["PostTypeName"] != DBNull.Value) _JxjyBaseMDL.PostTypeName = Convert.ToString(reader["PostTypeName"]);
                    if (reader["PostName"] != DBNull.Value) _JxjyBaseMDL.PostName = Convert.ToString(reader["PostName"]);
                    if (reader["CertificateCode"] != DBNull.Value) _JxjyBaseMDL.CertificateCode = Convert.ToString(reader["CertificateCode"]);
                    if (reader["ValidEndDate"] != DBNull.Value) _JxjyBaseMDL.ValidEndDate = Convert.ToDateTime(reader["ValidEndDate"]);
                    if (reader["UnitName"] != DBNull.Value) _JxjyBaseMDL.UnitName = Convert.ToString(reader["UnitName"]);
                    if (reader["cjsj"] != DBNull.Value) _JxjyBaseMDL.cjsj = Convert.ToDateTime(reader["cjsj"]);
                    if (reader["ApplyID"] != DBNull.Value) _JxjyBaseMDL.ApplyID = Convert.ToString(reader["ApplyID"]);
                }
                reader.Close();
                db.Close();
                return _JxjyBaseMDL;
            }
        }

        /// <summary>
        /// 删除没有明细的个人委培教育记录记录（删除最后一条明细时调用）
        /// </summary>
        /// <param name="PostTypeID"></param>
        /// <param name="ApplyID">业务申请单ID</param>
        /// <returns></returns>
        public static int DeleteNullSubDetail(int PostTypeID, string ApplyID)
		{
            string sql = @"if(not exists(select top 1 [DetailID] from [dbo].[JxjyDetail] where [BaseID] = (select [BaseID] FROM [dbo].[JxjyBase] where [ApplyID]=@ApplyID and PostTypeID = @PostTypeID)))
                           delete FROM [dbo].[JxjyBase] where ApplyID = @ApplyID and PostTypeID = @PostTypeID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PostTypeID", DbType.Int32, PostTypeID));
            p.Add(db.CreateParameter("ApplyID", DbType.String, ApplyID));
            return db.GetExcuteNonQuery(sql, p.ToArray());
		}

        /// <summary>
        /// 删除申请单对应的个人委培教育记录记录
        /// </summary>
        /// <param name="PostTypeID"></param>
        /// <param name="ApplyID">业务申请单ID</param>
        /// <returns></returns>
        public static int Delete(int PostTypeID, string ApplyID)
        {
            string sql = @"DELETE FROM dbo.JxjyBase WHERE ApplyID = @ApplyID and PostTypeID = @PostTypeID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PostTypeID", DbType.Int32, PostTypeID));
            p.Add(db.CreateParameter("ApplyID", DbType.String, ApplyID));
            return db.GetExcuteNonQuery(sql, p.ToArray());
        }

        #endregion
    }
}
