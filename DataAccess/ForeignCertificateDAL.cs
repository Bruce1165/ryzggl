using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

using Model;


namespace DataAccess
{
    /// <summary>
    /// 业务类实现--ForeignCertificateDAL(填写类描述)
    /// </summary>
    public class ForeignCertificateDAL
    {
        public ForeignCertificateDAL() { }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="ForeignCertificateOB">对象实体类</param>
        /// <returns></returns>
        public static long Insert(ForeignCertificateOB _ForeignCertificateOB)
        {
            return Insert(null, _ForeignCertificateOB);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ForeignCertificateOB">对象实体类</param>
        /// <returns></returns>
        public static long Insert(DbTransaction tran, ForeignCertificateOB _ForeignCertificateOB)
        {
            DBHelper db = new DBHelper();

            string sql = @"
			INSERT INTO dbo.ForeignCertificate(ApplyID,CertificateType,PostTypeID,PostID,CertificateCode,WorkerName,Sex,Birthday,UnitName,ConferDate,ValidStartDate,ValidEndDate,ConferUnit,Status,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,WorkerCertificateCode,UnitCode,ApplyCode,Phone,AddItemName,Remark,SkillLevel)
			VALUES (@ApplyID,@CertificateType,@PostTypeID,@PostID,@CertificateCode,@WorkerName,@Sex,@Birthday,@UnitName,@ConferDate,@ValidStartDate,@ValidEndDate,@ConferUnit,@Status,@CreatePersonID,@CreateTime,@ModifyPersonID,@ModifyTime,@WorkerCertificateCode,@UnitCode,@ApplyCode,@Phone,@AddItemName,@Remark,@SkillLevel);SELECT @CertificateID = @@IDENTITY";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateOutParameter("CertificateID", DbType.Int64));
            p.Add(db.CreateParameter("ApplyID", DbType.Int64, _ForeignCertificateOB.ApplyID));
            p.Add(db.CreateParameter("CertificateType", DbType.String, _ForeignCertificateOB.CertificateType));
            p.Add(db.CreateParameter("PostTypeID", DbType.Int32, _ForeignCertificateOB.PostTypeID));
            p.Add(db.CreateParameter("PostID", DbType.Int32, _ForeignCertificateOB.PostID));
            p.Add(db.CreateParameter("CertificateCode", DbType.String, _ForeignCertificateOB.CertificateCode));
            p.Add(db.CreateParameter("WorkerName", DbType.String, _ForeignCertificateOB.WorkerName));
            p.Add(db.CreateParameter("Sex", DbType.String, _ForeignCertificateOB.Sex));
            p.Add(db.CreateParameter("Birthday", DbType.DateTime, _ForeignCertificateOB.Birthday));
            p.Add(db.CreateParameter("UnitName", DbType.String, _ForeignCertificateOB.UnitName));
            p.Add(db.CreateParameter("ConferDate", DbType.DateTime, _ForeignCertificateOB.ConferDate));
            p.Add(db.CreateParameter("ValidStartDate", DbType.DateTime, _ForeignCertificateOB.ValidStartDate));
            p.Add(db.CreateParameter("ValidEndDate", DbType.DateTime, _ForeignCertificateOB.ValidEndDate));
            p.Add(db.CreateParameter("ConferUnit", DbType.String, _ForeignCertificateOB.ConferUnit));
            p.Add(db.CreateParameter("Status", DbType.String, _ForeignCertificateOB.Status));
            p.Add(db.CreateParameter("CreatePersonID", DbType.Int64, _ForeignCertificateOB.CreatePersonID));
            p.Add(db.CreateParameter("CreateTime", DbType.DateTime, _ForeignCertificateOB.CreateTime));
            p.Add(db.CreateParameter("ModifyPersonID", DbType.Int64, _ForeignCertificateOB.ModifyPersonID));
            p.Add(db.CreateParameter("ModifyTime", DbType.DateTime, _ForeignCertificateOB.ModifyTime));
            p.Add(db.CreateParameter("WorkerCertificateCode", DbType.String, _ForeignCertificateOB.WorkerCertificateCode));
            p.Add(db.CreateParameter("UnitCode", DbType.String, _ForeignCertificateOB.UnitCode));
            p.Add(db.CreateParameter("ApplyCode", DbType.String, _ForeignCertificateOB.ApplyCode));
            p.Add(db.CreateParameter("Phone", DbType.String, _ForeignCertificateOB.Phone));
            p.Add(db.CreateParameter("AddItemName", DbType.String, _ForeignCertificateOB.AddItemName));
            p.Add(db.CreateParameter("Remark", DbType.String, _ForeignCertificateOB.Remark));
            p.Add(db.CreateParameter("SkillLevel", DbType.String, _ForeignCertificateOB.SkillLevel));

            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            _ForeignCertificateOB.CertificateID = Convert.ToInt64(p[0].Value);
            return rtn;
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="ForeignCertificateOB">对象实体类</param>
        /// <returns></returns>
        public static int Update(ForeignCertificateOB _ForeignCertificateOB)
        {
            return Update(null, _ForeignCertificateOB);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ForeignCertificateOB">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, ForeignCertificateOB _ForeignCertificateOB)
        {
            string sql = @"
			UPDATE dbo.ForeignCertificate
				SET	ApplyID = @ApplyID,CertificateType = @CertificateType,PostTypeID = @PostTypeID,PostID = @PostID,CertificateCode = @CertificateCode,WorkerName = @WorkerName,Sex = @Sex,Birthday = @Birthday,UnitName = @UnitName,ConferDate = @ConferDate,ValidStartDate = @ValidStartDate,ValidEndDate = @ValidEndDate,ConferUnit = @ConferUnit,Status = @Status,CreatePersonID = @CreatePersonID,CreateTime = @CreateTime,ModifyPersonID = @ModifyPersonID,ModifyTime = @ModifyTime,WorkerCertificateCode = @WorkerCertificateCode,UnitCode = @UnitCode,ApplyCode = @ApplyCode,Phone = @Phone,AddItemName = @AddItemName,Remark= @Remark,SkillLevel= @SkillLevel
			WHERE
				CertificateID = @CertificateID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("CertificateID", DbType.Int64, _ForeignCertificateOB.CertificateID));
            p.Add(db.CreateParameter("ApplyID", DbType.Int64, _ForeignCertificateOB.ApplyID));
            p.Add(db.CreateParameter("CertificateType", DbType.String, _ForeignCertificateOB.CertificateType));
            p.Add(db.CreateParameter("PostTypeID", DbType.Int32, _ForeignCertificateOB.PostTypeID));
            p.Add(db.CreateParameter("PostID", DbType.Int32, _ForeignCertificateOB.PostID));
            p.Add(db.CreateParameter("CertificateCode", DbType.String, _ForeignCertificateOB.CertificateCode));
            p.Add(db.CreateParameter("WorkerName", DbType.String, _ForeignCertificateOB.WorkerName));
            p.Add(db.CreateParameter("Sex", DbType.String, _ForeignCertificateOB.Sex));
            p.Add(db.CreateParameter("Birthday", DbType.DateTime, _ForeignCertificateOB.Birthday));
            p.Add(db.CreateParameter("UnitName", DbType.String, _ForeignCertificateOB.UnitName));
            p.Add(db.CreateParameter("ConferDate", DbType.DateTime, _ForeignCertificateOB.ConferDate));
            p.Add(db.CreateParameter("ValidStartDate", DbType.DateTime, _ForeignCertificateOB.ValidStartDate));
            p.Add(db.CreateParameter("ValidEndDate", DbType.DateTime, _ForeignCertificateOB.ValidEndDate));
            p.Add(db.CreateParameter("ConferUnit", DbType.String, _ForeignCertificateOB.ConferUnit));
            p.Add(db.CreateParameter("Status", DbType.String, _ForeignCertificateOB.Status));
            p.Add(db.CreateParameter("CreatePersonID", DbType.Int64, _ForeignCertificateOB.CreatePersonID));
            p.Add(db.CreateParameter("CreateTime", DbType.DateTime, _ForeignCertificateOB.CreateTime));
            p.Add(db.CreateParameter("ModifyPersonID", DbType.Int64, _ForeignCertificateOB.ModifyPersonID));
            p.Add(db.CreateParameter("ModifyTime", DbType.DateTime, _ForeignCertificateOB.ModifyTime));
            p.Add(db.CreateParameter("WorkerCertificateCode", DbType.String, _ForeignCertificateOB.WorkerCertificateCode));
            p.Add(db.CreateParameter("UnitCode", DbType.String, _ForeignCertificateOB.UnitCode));
            p.Add(db.CreateParameter("ApplyCode", DbType.String, _ForeignCertificateOB.ApplyCode));
            p.Add(db.CreateParameter("Phone", DbType.String, _ForeignCertificateOB.Phone));
            p.Add(db.CreateParameter("AddItemName", DbType.String, _ForeignCertificateOB.AddItemName));
            p.Add(db.CreateParameter("Remark", DbType.String, _ForeignCertificateOB.Remark));
            p.Add(db.CreateParameter("SkillLevel", DbType.String, _ForeignCertificateOB.SkillLevel));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="ForeignCertificateID">主键</param>
        /// <returns></returns>
        public static int Delete(long CertificateID)
        {
            return Delete(null, CertificateID);
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ForeignCertificateID">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, long CertificateID)
        {
            string sql = @"DELETE FROM dbo.ForeignCertificate WHERE CertificateID = @CertificateID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("CertificateID", DbType.Int64, CertificateID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ForeignCertificateOB">对象实体类</param>
        /// <returns></returns>
        public static int Delete(ForeignCertificateOB _ForeignCertificateOB)
        {
            return Delete(null, _ForeignCertificateOB);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ForeignCertificateOB">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, ForeignCertificateOB _ForeignCertificateOB)
        {
            string sql = @"DELETE FROM dbo.ForeignCertificate WHERE CertificateID = @CertificateID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("CertificateID", DbType.Int64, _ForeignCertificateOB.CertificateID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ForeignCertificateID">主键</param>
        public static ForeignCertificateOB GetObject(long CertificateID)
        {
            string sql = @"
			SELECT CertificateID,ApplyID,CertificateType,PostTypeID,PostID,CertificateCode,WorkerName,Sex,Birthday,UnitName,ConferDate,ValidStartDate,ValidEndDate,ConferUnit,Status,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,WorkerCertificateCode,UnitCode,ApplyCode,Phone,AddItemName,Remark,SkillLevel
			FROM dbo.ForeignCertificate
			WHERE CertificateID = @CertificateID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("CertificateID", DbType.Int64, CertificateID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                ForeignCertificateOB _ForeignCertificateOB = null;
                if (reader.Read())
                {
                    _ForeignCertificateOB = new ForeignCertificateOB();
                    if (reader["CertificateID"] != DBNull.Value) _ForeignCertificateOB.CertificateID = Convert.ToInt64(reader["CertificateID"]);
                    if (reader["ApplyID"] != DBNull.Value) _ForeignCertificateOB.ApplyID = Convert.ToInt64(reader["ApplyID"]);
                    if (reader["CertificateType"] != DBNull.Value) _ForeignCertificateOB.CertificateType = Convert.ToString(reader["CertificateType"]);
                    if (reader["PostTypeID"] != DBNull.Value) _ForeignCertificateOB.PostTypeID = Convert.ToInt32(reader["PostTypeID"]);
                    if (reader["PostID"] != DBNull.Value) _ForeignCertificateOB.PostID = Convert.ToInt32(reader["PostID"]);
                    if (reader["CertificateCode"] != DBNull.Value) _ForeignCertificateOB.CertificateCode = Convert.ToString(reader["CertificateCode"]);
                    if (reader["WorkerName"] != DBNull.Value) _ForeignCertificateOB.WorkerName = Convert.ToString(reader["WorkerName"]);
                    if (reader["Sex"] != DBNull.Value) _ForeignCertificateOB.Sex = Convert.ToString(reader["Sex"]);
                    if (reader["Birthday"] != DBNull.Value) _ForeignCertificateOB.Birthday = Convert.ToDateTime(reader["Birthday"]);
                    if (reader["UnitName"] != DBNull.Value) _ForeignCertificateOB.UnitName = Convert.ToString(reader["UnitName"]);
                    if (reader["ConferDate"] != DBNull.Value) _ForeignCertificateOB.ConferDate = Convert.ToDateTime(reader["ConferDate"]);
                    if (reader["ValidStartDate"] != DBNull.Value) _ForeignCertificateOB.ValidStartDate = Convert.ToDateTime(reader["ValidStartDate"]);
                    if (reader["ValidEndDate"] != DBNull.Value) _ForeignCertificateOB.ValidEndDate = Convert.ToDateTime(reader["ValidEndDate"]);
                    if (reader["ConferUnit"] != DBNull.Value) _ForeignCertificateOB.ConferUnit = Convert.ToString(reader["ConferUnit"]);
                    if (reader["Status"] != DBNull.Value) _ForeignCertificateOB.Status = Convert.ToString(reader["Status"]);
                    if (reader["CreatePersonID"] != DBNull.Value) _ForeignCertificateOB.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                    if (reader["CreateTime"] != DBNull.Value) _ForeignCertificateOB.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                    if (reader["ModifyPersonID"] != DBNull.Value) _ForeignCertificateOB.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                    if (reader["ModifyTime"] != DBNull.Value) _ForeignCertificateOB.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                    if (reader["WorkerCertificateCode"] != DBNull.Value) _ForeignCertificateOB.WorkerCertificateCode = Convert.ToString(reader["WorkerCertificateCode"]);
                    if (reader["UnitCode"] != DBNull.Value) _ForeignCertificateOB.UnitCode = Convert.ToString(reader["UnitCode"]);
                    if (reader["ApplyCode"] != DBNull.Value) _ForeignCertificateOB.ApplyCode = Convert.ToString(reader["ApplyCode"]);
                    if (reader["Phone"] != DBNull.Value) _ForeignCertificateOB.Phone = Convert.ToString(reader["Phone"]);
                    if (reader["AddItemName"] != DBNull.Value) _ForeignCertificateOB.AddItemName = Convert.ToString(reader["AddItemName"]);
                    if (reader["Remark"] != DBNull.Value) _ForeignCertificateOB.Remark = Convert.ToString(reader["Remark"]);
                    if (reader["SkillLevel"] != DBNull.Value) _ForeignCertificateOB.SkillLevel = Convert.ToString(reader["SkillLevel"]);    
                }
                reader.Close();
                db.Close();
                return _ForeignCertificateOB;
            }
        }

        /// <summary>
        /// 根据证书编号读取外阜证书信息
        /// </summary>
        /// <param name="CertificateCode">外阜证书编号</param>
        /// <param name="PostID">岗位工种ID</param>
        /// <returns>外阜证书实体对象</returns>
        public static ForeignCertificateOB GetObject(string CertificateCode,int PostID)
        {
            string sql = @"
			SELECT CertificateID,ApplyID,CertificateType,PostTypeID,PostID,CertificateCode,WorkerName,Sex,Birthday,UnitName,ConferDate,ValidStartDate,ValidEndDate,ConferUnit,Status,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,WorkerCertificateCode,UnitCode,ApplyCode,Phone,AddItemName,Remark,SkillLevel
			FROM dbo.ForeignCertificate
			WHERE CertificateCode = @CertificateCode and PostID = @PostID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("CertificateCode", DbType.String, CertificateCode));
            p.Add(db.CreateParameter("PostID", DbType.Int32, PostID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                ForeignCertificateOB _ForeignCertificateOB = null;
                if (reader.Read())
                {
                    _ForeignCertificateOB = new ForeignCertificateOB();
                    if (reader["CertificateID"] != DBNull.Value) _ForeignCertificateOB.CertificateID = Convert.ToInt64(reader["CertificateID"]);
                    if (reader["ApplyID"] != DBNull.Value) _ForeignCertificateOB.ApplyID = Convert.ToInt64(reader["ApplyID"]);
                    if (reader["CertificateType"] != DBNull.Value) _ForeignCertificateOB.CertificateType = Convert.ToString(reader["CertificateType"]);
                    if (reader["PostTypeID"] != DBNull.Value) _ForeignCertificateOB.PostTypeID = Convert.ToInt32(reader["PostTypeID"]);
                    if (reader["PostID"] != DBNull.Value) _ForeignCertificateOB.PostID = Convert.ToInt32(reader["PostID"]);
                    if (reader["CertificateCode"] != DBNull.Value) _ForeignCertificateOB.CertificateCode = Convert.ToString(reader["CertificateCode"]);
                    if (reader["WorkerName"] != DBNull.Value) _ForeignCertificateOB.WorkerName = Convert.ToString(reader["WorkerName"]);
                    if (reader["Sex"] != DBNull.Value) _ForeignCertificateOB.Sex = Convert.ToString(reader["Sex"]);
                    if (reader["Birthday"] != DBNull.Value) _ForeignCertificateOB.Birthday = Convert.ToDateTime(reader["Birthday"]);
                    if (reader["UnitName"] != DBNull.Value) _ForeignCertificateOB.UnitName = Convert.ToString(reader["UnitName"]);
                    if (reader["ConferDate"] != DBNull.Value) _ForeignCertificateOB.ConferDate = Convert.ToDateTime(reader["ConferDate"]);
                    if (reader["ValidStartDate"] != DBNull.Value) _ForeignCertificateOB.ValidStartDate = Convert.ToDateTime(reader["ValidStartDate"]);
                    if (reader["ValidEndDate"] != DBNull.Value) _ForeignCertificateOB.ValidEndDate = Convert.ToDateTime(reader["ValidEndDate"]);
                    if (reader["ConferUnit"] != DBNull.Value) _ForeignCertificateOB.ConferUnit = Convert.ToString(reader["ConferUnit"]);
                    if (reader["Status"] != DBNull.Value) _ForeignCertificateOB.Status = Convert.ToString(reader["Status"]);
                    if (reader["CreatePersonID"] != DBNull.Value) _ForeignCertificateOB.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                    if (reader["CreateTime"] != DBNull.Value) _ForeignCertificateOB.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                    if (reader["ModifyPersonID"] != DBNull.Value) _ForeignCertificateOB.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                    if (reader["ModifyTime"] != DBNull.Value) _ForeignCertificateOB.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                    if (reader["WorkerCertificateCode"] != DBNull.Value) _ForeignCertificateOB.WorkerCertificateCode = Convert.ToString(reader["WorkerCertificateCode"]);
                    if (reader["UnitCode"] != DBNull.Value) _ForeignCertificateOB.UnitCode = Convert.ToString(reader["UnitCode"]);
                    if (reader["ApplyCode"] != DBNull.Value) _ForeignCertificateOB.ApplyCode = Convert.ToString(reader["ApplyCode"]);
                    if (reader["Phone"] != DBNull.Value) _ForeignCertificateOB.Phone = Convert.ToString(reader["Phone"]);
                    if (reader["AddItemName"] != DBNull.Value) _ForeignCertificateOB.AddItemName = Convert.ToString(reader["AddItemName"]);
                    if (reader["Remark"] != DBNull.Value) _ForeignCertificateOB.Remark = Convert.ToString(reader["Remark"]);
                    if (reader["SkillLevel"] != DBNull.Value) _ForeignCertificateOB.SkillLevel = Convert.ToString(reader["SkillLevel"]);
                }
                reader.Close();
                db.Close();
                return _ForeignCertificateOB;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.ForeignCertificate", "*", filterWhereString, orderBy == "" ? " CertificateID" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.ForeignCertificate", filterWhereString);
        }

        public static DataTable GetListView(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.View_ForeignCertificate", "*", filterWhereString, orderBy == "" ? " CertificateID" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountView(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.View_ForeignCertificate", filterWhereString);
        }


        /// <summary>
        /// 添加证书历史信息
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="CertificateID"></param>
        /// <returns></returns>
        public static int InsertForeignCertificateHistory(DbTransaction tran, long CertificateID)
        {
            DBHelper db = new DBHelper();

            string sql = @"
			INSERT INTO DBO.FOREIGNCERTIFICATEHISTORY(OPERATETYPE,CERTIFICATEID,APPLYID,CERTIFICATETYPE,POSTTYPEID,POSTID,CERTIFICATECODE,WORKERNAME,SEX,BIRTHDAY,UNITNAME,CONFERDATE,VALIDSTARTDATE,VALIDENDDATE,CONFERUNIT,STATUS,CREATEPERSONID,CREATETIME,MODIFYPERSONID,MODIFYTIME,WORKERCERTIFICATECODE,UNITCODE,APPLYCODE,PHONE,AddItemName,Remark,SkillLevel  )
SELECT  '备案',CERTIFICATEID,APPLYID,CERTIFICATETYPE,POSTTYPEID,POSTID,CERTIFICATECODE,WORKERNAME,SEX,BIRTHDAY,UNITNAME,CONFERDATE,VALIDSTARTDATE,VALIDENDDATE,CONFERUNIT,STATUS,CREATEPERSONID,CREATETIME,MODIFYPERSONID,MODIFYTIME,WORKERCERTIFICATECODE,UNITCODE,APPLYCODE,PHONE,AddItemName,Remark,SkillLevel
FROM DBO.FOREIGNCERTIFICATE WHERE CertificateID=" + CertificateID.ToString();

            return db.ExcuteNonQuery(tran, sql);
        }
    }
}
