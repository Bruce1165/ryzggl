using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
    /// <summary>
    /// 业务类实现--CertificateHistoryDAL(填写类描述)
    /// </summary>
    public class CertificateHistoryDAL
    {
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="certificateHistoryOb">对象实体类</param>
        /// <returns></returns>
        public static long Insert(CertificateHistoryOB certificateHistoryOb)
        {
            return Insert(null, certificateHistoryOb);
        }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="certificateHistoryOb">对象实体类</param>
        /// <returns></returns>
        public static long Insert(DbTransaction tran, CertificateHistoryOB certificateHistoryOb)
        {
            var db = new DBHelper();         

            const string sql = @"
			INSERT INTO dbo.CertificateHistory(OperateType,CertificateID,ExamPlanID,WorkerID,CertificateType,PostTypeID,PostID,CertificateCode,WorkerName,Sex,Birthday,UnitName,ConferDate,ValidStartDate,ValidEndDate,ConferUnit,Status,CheckMan,CheckAdvise,CheckDate,PrintMan,PrintDate,Status_1,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime)
			VALUES (@OperateType,@CertificateID,@ExamPlanID,@WorkerID,@CertificateType,@PostTypeID,@PostID,@CertificateCode,@WorkerName,@Sex,@Birthday,@UnitName,@ConferDate,@ValidStartDate,@ValidEndDate,@ConferUnit,@Status,@CheckMan,@CheckAdvise,@CheckDate,@PrintMan,@PrintDate,@Status_1,@CreatePersonID,@CreateTime,@ModifyPersonID,@ModifyTime);SELECT @CertificateHistoryID = @@IDENTITY";

            var p = new List<SqlParameter>
            {
                db.CreateOutParameter("CertificateHistoryID", DbType.Int64),
                db.CreateParameter("OperateType", DbType.String, certificateHistoryOb.OperateType),
                db.CreateParameter("CertificateID", DbType.Int64, certificateHistoryOb.CertificateID),
                db.CreateParameter("ExamPlanID", DbType.Int64, certificateHistoryOb.ExamPlanID),
                db.CreateParameter("WorkerID", DbType.Int64, certificateHistoryOb.WorkerID),
                db.CreateParameter("CertificateType", DbType.String, certificateHistoryOb.CertificateType),
                db.CreateParameter("PostTypeID", DbType.Int32, certificateHistoryOb.PostTypeID),
                db.CreateParameter("PostID", DbType.Int32, certificateHistoryOb.PostID),
                db.CreateParameter("CertificateCode", DbType.String, certificateHistoryOb.CertificateCode),
                db.CreateParameter("WorkerName", DbType.String, certificateHistoryOb.WorkerName),
                db.CreateParameter("Sex", DbType.String, certificateHistoryOb.Sex),
                db.CreateParameter("Birthday", DbType.DateTime, certificateHistoryOb.Birthday),
                db.CreateParameter("UnitName", DbType.String, certificateHistoryOb.UnitName),
                db.CreateParameter("ConferDate", DbType.DateTime, certificateHistoryOb.ConferDate),
                db.CreateParameter("ValidStartDate", DbType.DateTime, certificateHistoryOb.ValidStartDate),
                db.CreateParameter("ValidEndDate", DbType.DateTime, certificateHistoryOb.ValidEndDate),
                db.CreateParameter("ConferUnit", DbType.String, certificateHistoryOb.ConferUnit),
                db.CreateParameter("Status", DbType.String, certificateHistoryOb.Status),
                db.CreateParameter("CheckMan", DbType.String, certificateHistoryOb.CheckMan),
                db.CreateParameter("CheckAdvise", DbType.String, certificateHistoryOb.CheckAdvise),
                db.CreateParameter("CheckDate", DbType.DateTime, certificateHistoryOb.CheckDate),
                db.CreateParameter("PrintMan", DbType.String, certificateHistoryOb.PrintMan),
                db.CreateParameter("PrintDate", DbType.DateTime, certificateHistoryOb.PrintDate),
                db.CreateParameter("Status_1", DbType.String, certificateHistoryOb.Status_1),
                db.CreateParameter("CreatePersonID", DbType.Int64, certificateHistoryOb.CreatePersonID),
                db.CreateParameter("CreateTime", DbType.DateTime, certificateHistoryOb.CreateTime),
                db.CreateParameter("ModifyPersonID", DbType.Int64, certificateHistoryOb.ModifyPersonID),
                db.CreateParameter("ModifyTime", DbType.DateTime, certificateHistoryOb.ModifyTime)
            };
            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            certificateHistoryOb.CertificateHistoryID = Convert.ToInt64(p[0].Value);
            return rtn;
        }

        /// <summary>
        /// 添加变更历史
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="certificateId"></param>
        /// <returns></returns>
        public static int InsertChangeHistory(DbTransaction tran, long certificateId)
        {
            var db = new DBHelper();

            string sql = @"
			INSERT INTO dbo.CertificateHistory(OPERATETYPE,CERTIFICATEID,EXAMPLANID,WORKERID,CERTIFICATETYPE,POSTTYPEID,POSTID,CERTIFICATECODE,WORKERNAME,SEX,BIRTHDAY,UNITNAME,CONFERDATE,VALIDSTARTDATE,VALIDENDDATE,CONFERUNIT,STATUS,CHECKMAN,CHECKADVISE,CHECKDATE,PRINTMAN,PRINTDATE,CREATEPERSONID,CREATETIME,MODIFYPERSONID,MODIFYTIME,WORKERCERTIFICATECODE,UNITCODE,AddItemName,PostTypeName,PostName  )
SELECT '变更',CERTIFICATEID,EXAMPLANID,WORKERID,CERTIFICATETYPE,POSTTYPEID,POSTID,CERTIFICATECODE,WORKERNAME,SEX,BIRTHDAY,UNITNAME,CONFERDATE,VALIDSTARTDATE,VALIDENDDATE,CONFERUNIT,STATUS,CHECKMAN,CHECKADVISE,CHECKDATE,PRINTMAN,PRINTDATE,CREATEPERSONID,CREATETIME,MODIFYPERSONID,MODIFYTIME,WORKERCERTIFICATECODE,UNITCODE,AddItemName,PostTypeName,PostName
FROM DBO.CERTIFICATE where CertificateID={0}";

            return db.ExcuteNonQuery(tran, string.Format(sql,certificateId));
        }

        /// <summary>
        /// 添加续期历史
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="certificateId"></param>
        /// <returns></returns>
        public static int InsertContinueHistory(DbTransaction tran, long certificateId)
        {
            var db = new DBHelper();

            string sql = @"
			INSERT INTO dbo.CertificateHistory(OPERATETYPE,CERTIFICATEID,EXAMPLANID,WORKERID,CERTIFICATETYPE,POSTTYPEID,POSTID,CERTIFICATECODE,WORKERNAME,SEX,BIRTHDAY,UNITNAME,CONFERDATE,VALIDSTARTDATE,VALIDENDDATE,CONFERUNIT,STATUS,CHECKMAN,CHECKADVISE,CHECKDATE,PRINTMAN,PRINTDATE,CREATEPERSONID,CREATETIME,MODIFYPERSONID,MODIFYTIME,WORKERCERTIFICATECODE,UNITCODE,PostTypeName,PostName  )
SELECT '续期',CERTIFICATEID,EXAMPLANID,WORKERID,CERTIFICATETYPE,POSTTYPEID,POSTID,CERTIFICATECODE,WORKERNAME,SEX,BIRTHDAY,UNITNAME,CONFERDATE,VALIDSTARTDATE,VALIDENDDATE,CONFERUNIT,STATUS,CHECKMAN,CHECKADVISE,CHECKDATE,PRINTMAN,PRINTDATE,CREATEPERSONID,CREATETIME,MODIFYPERSONID,MODIFYTIME,WORKERCERTIFICATECODE,UNITCODE,PostTypeName,PostName
FROM DBO.CERTIFICATE where CertificateID={0}";

            return db.ExcuteNonQuery(tran, string.Format(sql, certificateId));
        }

        /// <summary>
        /// 批量添加续期历史
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="filterString">过滤条件</param>
        /// <returns></returns>
        public static int InsertContinueHistoryBatch(DbTransaction tran, string filterString)
        {
            var db = new DBHelper();

            string sql = @"
			INSERT INTO dbo.CertificateHistory(OPERATETYPE,CERTIFICATEID,EXAMPLANID,WORKERID,CERTIFICATETYPE,POSTTYPEID,POSTID,CERTIFICATECODE,WORKERNAME,SEX,BIRTHDAY,UNITNAME,CONFERDATE,VALIDSTARTDATE,VALIDENDDATE,CONFERUNIT,STATUS,CHECKMAN,CHECKADVISE,CHECKDATE,PRINTMAN,PRINTDATE,CREATEPERSONID,CREATETIME,MODIFYPERSONID,MODIFYTIME,WORKERCERTIFICATECODE,UNITCODE,PostTypeName,PostName  )
SELECT '续期',CERTIFICATEID,EXAMPLANID,WORKERID,CERTIFICATETYPE,POSTTYPEID,POSTID,CERTIFICATECODE,WORKERNAME,SEX,BIRTHDAY,UNITNAME,CONFERDATE,VALIDSTARTDATE,VALIDENDDATE,CONFERUNIT,STATUS,CHECKMAN,CHECKADVISE,CHECKDATE,PRINTMAN,PRINTDATE,CREATEPERSONID,CREATETIME,MODIFYPERSONID,MODIFYTIME,WORKERCERTIFICATECODE,UNITCODE,PostTypeName,PostName
FROM DBO.CERTIFICATE where 1=1 " + filterString;

            return db.ExcuteNonQuery(tran, sql);
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="certificateHistoryOb">对象实体类</param>
        /// <returns></returns>
        public static int Update(CertificateHistoryOB certificateHistoryOb)
        {
            return Update(null, certificateHistoryOb);
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="certificateHistoryOb">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, CertificateHistoryOB certificateHistoryOb)
        {
            const string sql = @"
			UPDATE dbo.CertificateHistory
				SET	OperateType = @OperateType,CertificateID = @CertificateID,ExamPlanID = @ExamPlanID,WorkerID = @WorkerID,CertificateType = @CertificateType,PostTypeID = @PostTypeID,PostID = @PostID,CertificateCode = @CertificateCode,WorkerName = @WorkerName,Sex = @Sex,Birthday = @Birthday,UnitName = @UnitName,ConferDate = @ConferDate,ValidStartDate = @ValidStartDate,ValidEndDate = @ValidEndDate,ConferUnit = @ConferUnit,Status = @Status,CheckMan = @CheckMan,CheckAdvise = @CheckAdvise,CheckDate = @CheckDate,PrintMan = @PrintMan,PrintDate = @PrintDate,Status_1 = @Status_1,CreatePersonID = @CreatePersonID,CreateTime = @CreateTime,ModifyPersonID = @ModifyPersonID,ModifyTime = @ModifyTime
			WHERE
				CertificateHistoryID = @CertificateHistoryID";

            var db = new DBHelper();
            var p = new List<SqlParameter>
            {
                db.CreateParameter("CertificateHistoryID", DbType.Int64, certificateHistoryOb.CertificateHistoryID),
                db.CreateParameter("OperateType", DbType.String, certificateHistoryOb.OperateType),
                db.CreateParameter("CertificateID", DbType.Int64, certificateHistoryOb.CertificateID),
                db.CreateParameter("ExamPlanID", DbType.Int64, certificateHistoryOb.ExamPlanID),
                db.CreateParameter("WorkerID", DbType.Int64, certificateHistoryOb.WorkerID),
                db.CreateParameter("CertificateType", DbType.String, certificateHistoryOb.CertificateType),
                db.CreateParameter("PostTypeID", DbType.Int32, certificateHistoryOb.PostTypeID),
                db.CreateParameter("PostID", DbType.Int32, certificateHistoryOb.PostID),
                db.CreateParameter("CertificateCode", DbType.String, certificateHistoryOb.CertificateCode),
                db.CreateParameter("WorkerName", DbType.String, certificateHistoryOb.WorkerName),
                db.CreateParameter("Sex", DbType.String, certificateHistoryOb.Sex),
                db.CreateParameter("Birthday", DbType.DateTime, certificateHistoryOb.Birthday),
                db.CreateParameter("UnitName", DbType.String, certificateHistoryOb.UnitName),
                db.CreateParameter("ConferDate", DbType.DateTime, certificateHistoryOb.ConferDate),
                db.CreateParameter("ValidStartDate", DbType.DateTime, certificateHistoryOb.ValidStartDate),
                db.CreateParameter("ValidEndDate", DbType.DateTime, certificateHistoryOb.ValidEndDate),
                db.CreateParameter("ConferUnit", DbType.String, certificateHistoryOb.ConferUnit),
                db.CreateParameter("Status", DbType.String, certificateHistoryOb.Status),
                db.CreateParameter("CheckMan", DbType.String, certificateHistoryOb.CheckMan),
                db.CreateParameter("CheckAdvise", DbType.String, certificateHistoryOb.CheckAdvise),
                db.CreateParameter("CheckDate", DbType.DateTime, certificateHistoryOb.CheckDate),
                db.CreateParameter("PrintMan", DbType.String, certificateHistoryOb.PrintMan),
                db.CreateParameter("PrintDate", DbType.DateTime, certificateHistoryOb.PrintDate),
                db.CreateParameter("Status_1", DbType.String, certificateHistoryOb.Status_1),
                db.CreateParameter("CreatePersonID", DbType.Int64, certificateHistoryOb.CreatePersonID),
                db.CreateParameter("CreateTime", DbType.DateTime, certificateHistoryOb.CreateTime),
                db.CreateParameter("ModifyPersonID", DbType.Int64, certificateHistoryOb.ModifyPersonID),
                db.CreateParameter("ModifyTime", DbType.DateTime, certificateHistoryOb.ModifyTime)
            };
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="certificateHistoryId">主键</param>
        /// <returns></returns>
        public static int Delete(long certificateHistoryId)
        {
            return Delete(null, certificateHistoryId);
        }

        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="certificateHistoryId">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, long certificateHistoryId)
        {
            const string sql = @"DELETE FROM dbo.CertificateHistory WHERE CertificateHistoryID = @CertificateHistoryID";

            var db = new DBHelper();
            var p = new List<SqlParameter>
            {
                db.CreateParameter("CertificateHistoryID", DbType.Int64, certificateHistoryId)
            };
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="certificateHistoryOb">对象实体类</param>
        /// <returns></returns>
        public static int Delete(CertificateHistoryOB certificateHistoryOb)
        {
            return Delete(null, certificateHistoryOb);
        }

        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="certificateHistoryOb">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, CertificateHistoryOB certificateHistoryOb)
        {
            const string sql = @"DELETE FROM dbo.CertificateHistory WHERE CertificateHistoryID = @CertificateHistoryID";

            var db = new DBHelper();
            var p = new List<SqlParameter>
            {
                db.CreateParameter("CertificateHistoryID", DbType.Int64, certificateHistoryOb.CertificateHistoryID)
            };
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="certificateHistoryId">主键</param>
        public static CertificateHistoryOB GetObject(long certificateHistoryId)
        {
            const string sql = @"
			SELECT CertificateHistoryID,OperateType,CertificateID,ExamPlanID,WorkerID,CertificateType,PostTypeID,PostID,CertificateCode,WorkerName,Sex,Birthday,UnitName,ConferDate,ValidStartDate,ValidEndDate,ConferUnit,Status,CheckMan,CheckAdvise,CheckDate,PrintMan,PrintDate,Status_1,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime
			FROM dbo.CertificateHistory
			WHERE CertificateHistoryID = @CertificateHistoryID";

            var db = new DBHelper();
            var p = new List<SqlParameter>
            {
                db.CreateParameter("CertificateHistoryID", DbType.Int64, certificateHistoryId)
            };
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    CertificateHistoryOB certificateHistoryOb = null;
                    if (reader.Read())
                    {
                        certificateHistoryOb = new CertificateHistoryOB();
                        if (reader["CertificateHistoryID"] != DBNull.Value) certificateHistoryOb.CertificateHistoryID = Convert.ToInt64(reader["CertificateHistoryID"]);
                        if (reader["OperateType"] != DBNull.Value) certificateHistoryOb.OperateType = Convert.ToString(reader["OperateType"]);
                        if (reader["CertificateID"] != DBNull.Value) certificateHistoryOb.CertificateID = Convert.ToInt64(reader["CertificateID"]);
                        if (reader["ExamPlanID"] != DBNull.Value) certificateHistoryOb.ExamPlanID = Convert.ToInt64(reader["ExamPlanID"]);
                        if (reader["WorkerID"] != DBNull.Value) certificateHistoryOb.WorkerID = Convert.ToInt64(reader["WorkerID"]);
                        if (reader["CertificateType"] != DBNull.Value) certificateHistoryOb.CertificateType = Convert.ToString(reader["CertificateType"]);
                        if (reader["PostTypeID"] != DBNull.Value) certificateHistoryOb.PostTypeID = Convert.ToInt32(reader["PostTypeID"]);
                        if (reader["PostID"] != DBNull.Value) certificateHistoryOb.PostID = Convert.ToInt32(reader["PostID"]);
                        if (reader["CertificateCode"] != DBNull.Value) certificateHistoryOb.CertificateCode = Convert.ToString(reader["CertificateCode"]);
                        if (reader["WorkerName"] != DBNull.Value) certificateHistoryOb.WorkerName = Convert.ToString(reader["WorkerName"]);
                        if (reader["Sex"] != DBNull.Value) certificateHistoryOb.Sex = Convert.ToString(reader["Sex"]);
                        if (reader["Birthday"] != DBNull.Value) certificateHistoryOb.Birthday = Convert.ToDateTime(reader["Birthday"]);
                        if (reader["UnitName"] != DBNull.Value) certificateHistoryOb.UnitName = Convert.ToString(reader["UnitName"]);
                        if (reader["ConferDate"] != DBNull.Value) certificateHistoryOb.ConferDate = Convert.ToDateTime(reader["ConferDate"]);
                        if (reader["ValidStartDate"] != DBNull.Value) certificateHistoryOb.ValidStartDate = Convert.ToDateTime(reader["ValidStartDate"]);
                        if (reader["ValidEndDate"] != DBNull.Value) certificateHistoryOb.ValidEndDate = Convert.ToDateTime(reader["ValidEndDate"]);
                        if (reader["ConferUnit"] != DBNull.Value) certificateHistoryOb.ConferUnit = Convert.ToString(reader["ConferUnit"]);
                        if (reader["Status"] != DBNull.Value) certificateHistoryOb.Status = Convert.ToString(reader["Status"]);
                        if (reader["CheckMan"] != DBNull.Value) certificateHistoryOb.CheckMan = Convert.ToString(reader["CheckMan"]);
                        if (reader["CheckAdvise"] != DBNull.Value) certificateHistoryOb.CheckAdvise = Convert.ToString(reader["CheckAdvise"]);
                        if (reader["CheckDate"] != DBNull.Value) certificateHistoryOb.CheckDate = Convert.ToDateTime(reader["CheckDate"]);
                        if (reader["PrintMan"] != DBNull.Value) certificateHistoryOb.PrintMan = Convert.ToString(reader["PrintMan"]);
                        if (reader["PrintDate"] != DBNull.Value) certificateHistoryOb.PrintDate = Convert.ToDateTime(reader["PrintDate"]);
                        if (reader["Status_1"] != DBNull.Value) certificateHistoryOb.Status_1 = Convert.ToString(reader["Status_1"]);
                        if (reader["CreatePersonID"] != DBNull.Value) certificateHistoryOb.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                        if (reader["CreateTime"] != DBNull.Value) certificateHistoryOb.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                        if (reader["ModifyPersonID"] != DBNull.Value) certificateHistoryOb.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                        if (reader["ModifyTime"] != DBNull.Value) certificateHistoryOb.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                    }
                    reader.Close();
                    db.Close();
                    return certificateHistoryOb;
                }
            }
            catch (Exception)
            {
                db.Close();
                throw;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.CertificateHistory", "*", filterWhereString, orderBy == "" ? " CertificateHistoryID" : orderBy);
        }

        public static DataTable GetListView(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "DBO.VIEW_CERTIFICATEHISTORY", "*", filterWhereString, orderBy == "" ? " CertificateHistoryID" : orderBy);
        }

        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.CertificateHistory", filterWhereString);
        }

        public static int SelectCountView(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("DBO.VIEW_CERTIFICATEHISTORY", filterWhereString);
        }
    }
}