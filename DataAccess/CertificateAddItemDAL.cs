using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
    /// <summary>
    /// 业务类实现--CertificateAddItemDAL(填写类描述)
    /// </summary>
    public class CertificateAddItemDAL
    {
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="certificateAddItemOb">对象实体类</param>
        /// <returns></returns>
        public static long Insert(CertificateAddItemOB certificateAddItemOb)
        {
            return Insert(null, certificateAddItemOb);
        }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="certificateAddItemOb">对象实体类</param>
        /// <returns></returns>
        public static long Insert(DbTransaction tran, CertificateAddItemOB certificateAddItemOb)
        {
            var db = new DBHelper();           

            const string sql = @"
			INSERT INTO dbo.CertificateAddItem(CertificateID,PostTypeID,PostID,CreatePerson,CreateTime,CaseStatus)
			VALUES (@CertificateID,@PostTypeID,@PostID,@CreatePerson,@CreateTime,@CaseStatus);SELECT @CertificateAddItemID = @@IDENTITY";

            var p = new List<SqlParameter>
            {
                db.CreateOutParameter("CertificateAddItemID", DbType.Int64),
                db.CreateParameter("CertificateID", DbType.Int64, certificateAddItemOb.CertificateID),
                db.CreateParameter("PostTypeID", DbType.Int32, certificateAddItemOb.PostTypeID),
                db.CreateParameter("PostID", DbType.Int32, certificateAddItemOb.PostID),
                db.CreateParameter("CreatePerson", DbType.String, certificateAddItemOb.CreatePerson),
                db.CreateParameter("CreateTime", DbType.DateTime, certificateAddItemOb.CreateTime),
                db.CreateParameter("CaseStatus", DbType.String, certificateAddItemOb.CaseStatus)
            };
            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            certificateAddItemOb.CertificateAddItemID = Convert.ToInt64(p[0].Value);
            return rtn;
        }

        /// <summary>
        /// 批量添加增项数据
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="examPlanId">考试计划</param>
        /// <param name="postId">证书岗位ID</param>
        /// <returns></returns>
        public static long InsertBatch(DbTransaction tran, long examPlanId, int postId)
        {
            var db = new DBHelper();

            const string sql = @"INSERT INTO DBO.CERTIFICATEADDITEM(CERTIFICATEID,PostTypeID,POSTID )
SELECT CERTIFICATEID,PostTypeID,case PostID when 12 then 9 else 12 end as PostID FROM DBO.Certificate WHERE POSTID=@POSTID and validenddate >=getdate()
and [STATUS] <>'注销' and [STATUS] <>'离京变更' and additemname is null and workercertificatecode in (select workercertificatecode from
DBO.VIEW_EXAMSCORE where ExamPlanID=@ExamPlanID and Status = '成绩已公告' and ExamResult = '合格' )";

            var p = new List<SqlParameter>
            {
                db.CreateParameter("ExamPlanID", DbType.Int64, examPlanId),
                db.CreateParameter("PostID", DbType.Int32, postId)
            };
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="certificateAddItemOb">对象实体类</param>
        /// <returns></returns>
        public static int Update(CertificateAddItemOB certificateAddItemOb)
        {
            return Update(null, certificateAddItemOb);
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="certificateAddItemOb">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, CertificateAddItemOB certificateAddItemOb)
        {
            const string sql = @"
			UPDATE dbo.CertificateAddItem
				SET	CertificateID = @CertificateID,PostTypeID = @PostTypeID,PostID = @PostID,CreatePerson = @CreatePerson,CreateTime = @CreateTime,CaseStatus = @CaseStatus
			WHERE
				CertificateAddItemID = @CertificateAddItemID";

            var db = new DBHelper();
            var p = new List<SqlParameter>
            {
                db.CreateParameter("CertificateAddItemID", DbType.Int64, certificateAddItemOb.CertificateAddItemID),
                db.CreateParameter("CertificateID", DbType.Int64, certificateAddItemOb.CertificateID),
                db.CreateParameter("PostTypeID", DbType.Int32, certificateAddItemOb.PostTypeID),
                db.CreateParameter("PostID", DbType.Int32, certificateAddItemOb.PostID),
                db.CreateParameter("CreatePerson", DbType.String, certificateAddItemOb.CreatePerson),
                db.CreateParameter("CreateTime", DbType.DateTime, certificateAddItemOb.CreateTime),
                db.CreateParameter("CaseStatus", DbType.String, certificateAddItemOb.CaseStatus)
            };
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="certificateAddItemId">主键</param>
        /// <returns></returns>
        public static int Delete(long certificateAddItemId)
        {
            return Delete(null, certificateAddItemId);
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="certificateAddItemId">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, long certificateAddItemId)
        {
            const string sql = @"DELETE FROM dbo.CertificateAddItem WHERE CertificateAddItemID = @CertificateAddItemID";

            var db = new DBHelper();
            var p = new List<SqlParameter>
            {
                db.CreateParameter("CertificateAddItemID", DbType.Int64, certificateAddItemId)
            };
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="certificateAddItemOb">对象实体类</param>
        /// <returns></returns>
        public static int Delete(CertificateAddItemOB certificateAddItemOb)
        {
            return Delete(null, certificateAddItemOb);
        }

        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="certificateAddItemOb">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, CertificateAddItemOB certificateAddItemOb)
        {
            const string sql = @"DELETE FROM dbo.CertificateAddItem WHERE CertificateAddItemID = @CertificateAddItemID";

            var db = new DBHelper();
            var p = new List<SqlParameter>
            {
                db.CreateParameter("CertificateAddItemID", DbType.Int64, certificateAddItemOb.CertificateAddItemID)
            };
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="certificateAddItemId">主键</param>
        public static CertificateAddItemOB GetObject(long certificateAddItemId)
        {
            const string sql = @"
			SELECT CertificateAddItemID,CertificateID,PostTypeID,PostID,CreatePerson,CreateTime,CaseStatus
			FROM dbo.CertificateAddItem
			WHERE CertificateAddItemID = @CertificateAddItemID";

            var db = new DBHelper();
            var p = new List<SqlParameter>
            {
                db.CreateParameter("CertificateAddItemID", DbType.Int64, certificateAddItemId)
            };
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    CertificateAddItemOB certificateAddItemOb = null;
                    if (reader.Read())
                    {
                        certificateAddItemOb = new CertificateAddItemOB();
                        if (reader["CertificateAddItemID"] != DBNull.Value) certificateAddItemOb.CertificateAddItemID = Convert.ToInt64(reader["CertificateAddItemID"]);
                        if (reader["CertificateID"] != DBNull.Value) certificateAddItemOb.CertificateID = Convert.ToInt64(reader["CertificateID"]);
                        if (reader["PostTypeID"] != DBNull.Value) certificateAddItemOb.PostTypeID = Convert.ToInt32(reader["PostTypeID"]);
                        if (reader["PostID"] != DBNull.Value) certificateAddItemOb.PostID = Convert.ToInt32(reader["PostID"]);
                        if (reader["CreatePerson"] != DBNull.Value) certificateAddItemOb.CreatePerson = Convert.ToString(reader["CreatePerson"]);
                        if (reader["CreateTime"] != DBNull.Value) certificateAddItemOb.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                        if (reader["CaseStatus"] != DBNull.Value) certificateAddItemOb.CaseStatus = Convert.ToString(reader["CaseStatus"]);
                    }
                    reader.Close();
                    db.Close();
                    return certificateAddItemOb;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.VIEW_CERTIFICATE_ADDITEM", "*", filterWhereString, orderBy == "" ? " CertificateAddItemID" : orderBy);
        }

        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.VIEW_CERTIFICATE_ADDITEM", filterWhereString);
        }

        /// <summary>
        /// 获取证书增项名称集合
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="certificateId">证书ID</param>
        /// <returns>增项工种名称集合用逗号分隔</returns>
        public static string GetAddItemNameString(DbTransaction tran, long certificateId)
        {
            DataTable dt = CommonDAL.GetDataTable(tran, 0, int.MaxValue - 1, "dbo.CertificateAddItem as a left join dbo.PostInfo as p on a.PostID = p.PostID ", "p.PostName", " and a.CertificateID=" + certificateId, "CertificateAddItemID");
            var sb = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                sb.Append(",增").Append(dr["PostName"]);
            }
            return sb.ToString();
        }
    }
}