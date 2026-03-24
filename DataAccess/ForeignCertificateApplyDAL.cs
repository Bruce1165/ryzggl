using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

using Model;

namespace DataAccess
{
    /// <summary>
    /// 业务类实现--ForeignCertificateApplyDAL(填写类描述)
    /// </summary>
    public class ForeignCertificateApplyDAL
    {
        public ForeignCertificateApplyDAL() { }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="ForeignCertificateApplyOB">对象实体类</param>
        /// <returns></returns>
        public static long Insert(ForeignCertificateApplyOB _ForeignCertificateApplyOB)
        {
            return Insert(null, _ForeignCertificateApplyOB);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ForeignCertificateApplyOB">对象实体类</param>
        /// <returns></returns>
        public static long Insert(DbTransaction tran, ForeignCertificateApplyOB _ForeignCertificateApplyOB)
        {
            DBHelper db = new DBHelper();

            string sql = @"
			INSERT INTO dbo.ForeignCertificateApply(CertificateType,PostTypeID,PostID,CertificateCode,WorkerName,Sex,Birthday,UnitName,ConferDate,ValidStartDate,ValidEndDate,ConferUnit,Status,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,WorkerCertificateCode,UnitCode,ApplyCode,Phone,AddItemName,Remark,SkillLevel  )
			VALUES (@CertificateType,@PostTypeID,@PostID,@CertificateCode,@WorkerName,@Sex,@Birthday,@UnitName,@ConferDate,@ValidStartDate,@ValidEndDate,@ConferUnit,@Status,@CreatePersonID,@CreateTime,@ModifyPersonID,@ModifyTime,@WorkerCertificateCode,@UnitCode,@ApplyCode,@Phone,@AddItemName,@Remark,@SkillLevel);SELECT @ApplyID = @@IDENTITY";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateOutParameter("ApplyID", DbType.Int64));
            p.Add(db.CreateParameter("CertificateType", DbType.String, _ForeignCertificateApplyOB.CertificateType));
            p.Add(db.CreateParameter("PostTypeID", DbType.Int32, _ForeignCertificateApplyOB.PostTypeID));
            p.Add(db.CreateParameter("PostID", DbType.Int32, _ForeignCertificateApplyOB.PostID));
            p.Add(db.CreateParameter("CertificateCode", DbType.String, _ForeignCertificateApplyOB.CertificateCode));
            p.Add(db.CreateParameter("WorkerName", DbType.String, _ForeignCertificateApplyOB.WorkerName));
            p.Add(db.CreateParameter("Sex", DbType.String, _ForeignCertificateApplyOB.Sex));
            p.Add(db.CreateParameter("Birthday", DbType.DateTime, _ForeignCertificateApplyOB.Birthday));
            p.Add(db.CreateParameter("UnitName", DbType.String, _ForeignCertificateApplyOB.UnitName));
            p.Add(db.CreateParameter("ConferDate", DbType.DateTime, _ForeignCertificateApplyOB.ConferDate));
            p.Add(db.CreateParameter("ValidStartDate", DbType.DateTime, _ForeignCertificateApplyOB.ValidStartDate));
            p.Add(db.CreateParameter("ValidEndDate", DbType.DateTime, _ForeignCertificateApplyOB.ValidEndDate));
            p.Add(db.CreateParameter("ConferUnit", DbType.String, _ForeignCertificateApplyOB.ConferUnit));
            p.Add(db.CreateParameter("Status", DbType.String, _ForeignCertificateApplyOB.Status));
            p.Add(db.CreateParameter("CreatePersonID", DbType.Int64, _ForeignCertificateApplyOB.CreatePersonID));
            p.Add(db.CreateParameter("CreateTime", DbType.DateTime, _ForeignCertificateApplyOB.CreateTime));
            p.Add(db.CreateParameter("ModifyPersonID", DbType.Int64, _ForeignCertificateApplyOB.ModifyPersonID));
            p.Add(db.CreateParameter("ModifyTime", DbType.DateTime, _ForeignCertificateApplyOB.ModifyTime));
            p.Add(db.CreateParameter("WorkerCertificateCode", DbType.String, _ForeignCertificateApplyOB.WorkerCertificateCode));
            p.Add(db.CreateParameter("UnitCode", DbType.String, _ForeignCertificateApplyOB.UnitCode));
            p.Add(db.CreateParameter("ApplyCode", DbType.String, _ForeignCertificateApplyOB.ApplyCode));
            p.Add(db.CreateParameter("Phone", DbType.String, _ForeignCertificateApplyOB.Phone));
            p.Add(db.CreateParameter("AddItemName", DbType.String, _ForeignCertificateApplyOB.AddItemName));
            p.Add(db.CreateParameter("Remark", DbType.String, _ForeignCertificateApplyOB.Remark));
            p.Add(db.CreateParameter("SkillLevel", DbType.String, _ForeignCertificateApplyOB.SkillLevel));
            
            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            _ForeignCertificateApplyOB.ApplyID = Convert.ToInt64(p[0].Value);
            return rtn;
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="ForeignCertificateApplyOB">对象实体类</param>
        /// <returns></returns>
        public static int Update(ForeignCertificateApplyOB _ForeignCertificateApplyOB)
        {
            return Update(null, _ForeignCertificateApplyOB);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ForeignCertificateApplyOB">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, ForeignCertificateApplyOB _ForeignCertificateApplyOB)
        {
            string sql = @"
			UPDATE dbo.ForeignCertificateApply
				SET	CertificateType = @CertificateType,PostTypeID = @PostTypeID,PostID = @PostID,CertificateCode = @CertificateCode,WorkerName = @WorkerName,Sex = @Sex,Birthday = @Birthday,UnitName = @UnitName,ConferDate = @ConferDate,ValidStartDate = @ValidStartDate,ValidEndDate = @ValidEndDate,ConferUnit = @ConferUnit,Status = @Status,CreatePersonID = @CreatePersonID,CreateTime = @CreateTime,ModifyPersonID = @ModifyPersonID,ModifyTime = @ModifyTime,WorkerCertificateCode = @WorkerCertificateCode,UnitCode = @UnitCode,ApplyCode = @ApplyCode,Phone = @Phone,AddItemName = @AddItemName,Remark= @Remark,SkillLevel= @SkillLevel
			WHERE
				ApplyID = @ApplyID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.Int64, _ForeignCertificateApplyOB.ApplyID));
            p.Add(db.CreateParameter("CertificateType", DbType.String, _ForeignCertificateApplyOB.CertificateType));
            p.Add(db.CreateParameter("PostTypeID", DbType.Int32, _ForeignCertificateApplyOB.PostTypeID));
            p.Add(db.CreateParameter("PostID", DbType.Int32, _ForeignCertificateApplyOB.PostID));
            p.Add(db.CreateParameter("CertificateCode", DbType.String, _ForeignCertificateApplyOB.CertificateCode));
            p.Add(db.CreateParameter("WorkerName", DbType.String, _ForeignCertificateApplyOB.WorkerName));
            p.Add(db.CreateParameter("Sex", DbType.String, _ForeignCertificateApplyOB.Sex));
            p.Add(db.CreateParameter("Birthday", DbType.DateTime, _ForeignCertificateApplyOB.Birthday));
            p.Add(db.CreateParameter("UnitName", DbType.String, _ForeignCertificateApplyOB.UnitName));
            p.Add(db.CreateParameter("ConferDate", DbType.DateTime, _ForeignCertificateApplyOB.ConferDate));
            p.Add(db.CreateParameter("ValidStartDate", DbType.DateTime, _ForeignCertificateApplyOB.ValidStartDate));
            p.Add(db.CreateParameter("ValidEndDate", DbType.DateTime, _ForeignCertificateApplyOB.ValidEndDate));
            p.Add(db.CreateParameter("ConferUnit", DbType.String, _ForeignCertificateApplyOB.ConferUnit));
            p.Add(db.CreateParameter("Status", DbType.String, _ForeignCertificateApplyOB.Status));
            p.Add(db.CreateParameter("CreatePersonID", DbType.Int64, _ForeignCertificateApplyOB.CreatePersonID));
            p.Add(db.CreateParameter("CreateTime", DbType.DateTime, _ForeignCertificateApplyOB.CreateTime));
            p.Add(db.CreateParameter("ModifyPersonID", DbType.Int64, _ForeignCertificateApplyOB.ModifyPersonID));
            p.Add(db.CreateParameter("ModifyTime", DbType.DateTime, _ForeignCertificateApplyOB.ModifyTime));
            p.Add(db.CreateParameter("WorkerCertificateCode", DbType.String, _ForeignCertificateApplyOB.WorkerCertificateCode));
            p.Add(db.CreateParameter("UnitCode", DbType.String, _ForeignCertificateApplyOB.UnitCode));
            p.Add(db.CreateParameter("ApplyCode", DbType.String, _ForeignCertificateApplyOB.ApplyCode));
            p.Add(db.CreateParameter("Phone", DbType.String, _ForeignCertificateApplyOB.Phone));
            p.Add(db.CreateParameter("AddItemName", DbType.String, _ForeignCertificateApplyOB.AddItemName));
            p.Add(db.CreateParameter("Remark", DbType.String, _ForeignCertificateApplyOB.Remark));
            p.Add(db.CreateParameter("SkillLevel", DbType.String, _ForeignCertificateApplyOB.SkillLevel));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="ForeignCertificateApplyID">主键</param>
        /// <returns></returns>
        public static int Delete(long ApplyID)
        {
            return Delete(null, ApplyID);
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ForeignCertificateApplyID">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, long ApplyID)
        {
            string sql = @"DELETE FROM dbo.ForeignCertificateApply WHERE ApplyID = @ApplyID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.Int64, ApplyID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ForeignCertificateApplyOB">对象实体类</param>
        /// <returns></returns>
        public static int Delete(ForeignCertificateApplyOB _ForeignCertificateApplyOB)
        {
            return Delete(null, _ForeignCertificateApplyOB);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ForeignCertificateApplyOB">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, ForeignCertificateApplyOB _ForeignCertificateApplyOB)
        {
            string sql = @"DELETE FROM dbo.ForeignCertificateApply WHERE ApplyID = @ApplyID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.Int64, _ForeignCertificateApplyOB.ApplyID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ForeignCertificateApplyID">主键</param>
        public static ForeignCertificateApplyOB GetObject(long ApplyID)
        {
            string sql = @"
			SELECT ApplyID,CertificateType,PostTypeID,PostID,CertificateCode,WorkerName,Sex,Birthday,UnitName,ConferDate,ValidStartDate,ValidEndDate,ConferUnit,Status,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,WorkerCertificateCode,UnitCode,ApplyCode,Phone,AddItemName,Remark,SkillLevel
			FROM dbo.ForeignCertificateApply
			WHERE ApplyID = @ApplyID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ApplyID", DbType.Int64, ApplyID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                ForeignCertificateApplyOB _ForeignCertificateApplyOB = null;
                if (reader.Read())
                {
                    _ForeignCertificateApplyOB = new ForeignCertificateApplyOB();
                    if (reader["ApplyID"] != DBNull.Value) _ForeignCertificateApplyOB.ApplyID = Convert.ToInt64(reader["ApplyID"]);
                    if (reader["CertificateType"] != DBNull.Value) _ForeignCertificateApplyOB.CertificateType = Convert.ToString(reader["CertificateType"]);
                    if (reader["PostTypeID"] != DBNull.Value) _ForeignCertificateApplyOB.PostTypeID = Convert.ToInt32(reader["PostTypeID"]);
                    if (reader["PostID"] != DBNull.Value) _ForeignCertificateApplyOB.PostID = Convert.ToInt32(reader["PostID"]);
                    if (reader["CertificateCode"] != DBNull.Value) _ForeignCertificateApplyOB.CertificateCode = Convert.ToString(reader["CertificateCode"]);
                    if (reader["WorkerName"] != DBNull.Value) _ForeignCertificateApplyOB.WorkerName = Convert.ToString(reader["WorkerName"]);
                    if (reader["Sex"] != DBNull.Value) _ForeignCertificateApplyOB.Sex = Convert.ToString(reader["Sex"]);
                    if (reader["Birthday"] != DBNull.Value) _ForeignCertificateApplyOB.Birthday = Convert.ToDateTime(reader["Birthday"]);
                    if (reader["UnitName"] != DBNull.Value) _ForeignCertificateApplyOB.UnitName = Convert.ToString(reader["UnitName"]);
                    if (reader["ConferDate"] != DBNull.Value) _ForeignCertificateApplyOB.ConferDate = Convert.ToDateTime(reader["ConferDate"]);
                    if (reader["ValidStartDate"] != DBNull.Value) _ForeignCertificateApplyOB.ValidStartDate = Convert.ToDateTime(reader["ValidStartDate"]);
                    if (reader["ValidEndDate"] != DBNull.Value) _ForeignCertificateApplyOB.ValidEndDate = Convert.ToDateTime(reader["ValidEndDate"]);
                    if (reader["ConferUnit"] != DBNull.Value) _ForeignCertificateApplyOB.ConferUnit = Convert.ToString(reader["ConferUnit"]);
                    if (reader["Status"] != DBNull.Value) _ForeignCertificateApplyOB.Status = Convert.ToString(reader["Status"]);
                    if (reader["CreatePersonID"] != DBNull.Value) _ForeignCertificateApplyOB.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                    if (reader["CreateTime"] != DBNull.Value) _ForeignCertificateApplyOB.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                    if (reader["ModifyPersonID"] != DBNull.Value) _ForeignCertificateApplyOB.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                    if (reader["ModifyTime"] != DBNull.Value) _ForeignCertificateApplyOB.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                    if (reader["WorkerCertificateCode"] != DBNull.Value) _ForeignCertificateApplyOB.WorkerCertificateCode = Convert.ToString(reader["WorkerCertificateCode"]);
                    if (reader["UnitCode"] != DBNull.Value) _ForeignCertificateApplyOB.UnitCode = Convert.ToString(reader["UnitCode"]);
                    if (reader["ApplyCode"] != DBNull.Value) _ForeignCertificateApplyOB.ApplyCode = Convert.ToString(reader["ApplyCode"]);
                    if (reader["Phone"] != DBNull.Value) _ForeignCertificateApplyOB.Phone = Convert.ToString(reader["Phone"]);
                    if (reader["AddItemName"] != DBNull.Value) _ForeignCertificateApplyOB.AddItemName = Convert.ToString(reader["AddItemName"]);
                    if (reader["Remark"] != DBNull.Value) _ForeignCertificateApplyOB.Remark = Convert.ToString(reader["Remark"]);
                    if (reader["SkillLevel"] != DBNull.Value) _ForeignCertificateApplyOB.SkillLevel = Convert.ToString(reader["SkillLevel"]);    
                }
                reader.Close();
                db.Close();
                return _ForeignCertificateApplyOB;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.ForeignCertificateApply", "*", filterWhereString, orderBy == "" ? " ApplyID" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.ForeignCertificateApply", filterWhereString);
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
        public static DataTable GetListView(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.View_ForeignCertificateApply", "*", filterWhereString, orderBy == "" ? " ApplyID" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectViewCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.View_ForeignCertificateApply", filterWhereString);
        }


        /// <summary>
        /// 证书申报
        /// </summary>
        /// <param name="CertificateOB">对象实体类</param>
        /// <returns></returns>
        public static int Check(ForeignCertificateApplyOB _ForeignCertificateApplyOB, string whereString)
        {
            string sql = @"
			UPDATE dbo.ForeignCertificateApply
				SET	
                    Status = @Status,ApplyCode = @ApplyCode,CreateTime= @CreateTime
			WHERE
				1=1 {0}";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("Status", DbType.String, _ForeignCertificateApplyOB.Status));
            p.Add(db.CreateParameter("ApplyCode", DbType.String, _ForeignCertificateApplyOB.ApplyCode));
            p.Add(db.CreateParameter("CreateTime", DbType.DateTime, _ForeignCertificateApplyOB.CreateTime));
            return db.ExcuteNonQuery(string.Format(sql, whereString), p.ToArray());
        }
    }
}
