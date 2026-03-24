using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

using Model;

namespace DataAccess
{
    /// <summary>
    /// 业务类实现--ExamSubjectResultDAL(填写类描述)
    /// </summary>
    public class ExamSubjectResultDAL
    {
        public ExamSubjectResultDAL() { }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="ExamSubjectResultOB">对象实体类</param>
        /// <returns></returns>
        public static long Insert(ExamSubjectResultOB _ExamSubjectResultOB)
        {
            return Insert(null, _ExamSubjectResultOB);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamSubjectResultOB">对象实体类</param>
        /// <returns></returns>
        public static long Insert(DbTransaction tran, ExamSubjectResultOB _ExamSubjectResultOB)
        {
            DBHelper db = new DBHelper();

            string sql = @"
			INSERT INTO dbo.ExamSubjectResult(ExamCardID,ExamPlanID,PostID,SubjectiveTopicScore,ObjectiveTopicScore,SumScore,ExamStatus,Status,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime)
			VALUES (@ExamCardID,@ExamPlanID,@PostID,@SubjectiveTopicScore,@ObjectiveTopicScore,@SumScore,@ExamStatus,@Status,@CreatePersonID,@CreateTime,@ModifyPersonID,@ModifyTime);SELECT @ExamSubjectResultID = @@IDENTITY";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateOutParameter("ExamSubjectResultID", DbType.Int64));
            p.Add(db.CreateParameter("ExamCardID", DbType.String, _ExamSubjectResultOB.ExamCardID));
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, _ExamSubjectResultOB.ExamPlanID));
            p.Add(db.CreateParameter("PostID", DbType.Int32, _ExamSubjectResultOB.PostID));
            p.Add(db.CreateParameter("SubjectiveTopicScore", DbType.Decimal, _ExamSubjectResultOB.SubjectiveTopicScore));
            p.Add(db.CreateParameter("ObjectiveTopicScore", DbType.Decimal, _ExamSubjectResultOB.ObjectiveTopicScore));
            p.Add(db.CreateParameter("SumScore", DbType.Decimal, _ExamSubjectResultOB.SumScore));
            p.Add(db.CreateParameter("ExamStatus", DbType.String, _ExamSubjectResultOB.ExamStatus));
            p.Add(db.CreateParameter("Status", DbType.String, _ExamSubjectResultOB.Status));
            p.Add(db.CreateParameter("CreatePersonID", DbType.Int64, _ExamSubjectResultOB.CreatePersonID));
            p.Add(db.CreateParameter("CreateTime", DbType.DateTime, _ExamSubjectResultOB.CreateTime));
            p.Add(db.CreateParameter("ModifyPersonID", DbType.Int64, _ExamSubjectResultOB.ModifyPersonID));
            p.Add(db.CreateParameter("ModifyTime", DbType.DateTime, _ExamSubjectResultOB.ModifyTime));

            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            _ExamSubjectResultOB.ExamSubjectResultID = Convert.ToInt64(p[0].Value);
            return rtn;
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="ExamSubjectResultOB">对象实体类</param>
        /// <returns></returns>
        public static int Update(ExamSubjectResultOB _ExamSubjectResultOB)
        {
            return Update(null, _ExamSubjectResultOB);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamSubjectResultOB">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, ExamSubjectResultOB _ExamSubjectResultOB)
        {
            string sql = @"
			UPDATE dbo.ExamSubjectResult
				SET	ExamCardID = @ExamCardID,ExamPlanID = @ExamPlanID,PostID = @PostID,SubjectiveTopicScore = @SubjectiveTopicScore,ObjectiveTopicScore = @ObjectiveTopicScore,SumScore = @SumScore,ExamStatus = @ExamStatus,Status = @Status,CreatePersonID = @CreatePersonID,CreateTime = @CreateTime,ModifyPersonID = @ModifyPersonID,ModifyTime = @ModifyTime
			WHERE
				ExamSubjectResultID = @ExamSubjectResultID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamSubjectResultID", DbType.Int64, _ExamSubjectResultOB.ExamSubjectResultID));
            p.Add(db.CreateParameter("ExamCardID", DbType.String, _ExamSubjectResultOB.ExamCardID));
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, _ExamSubjectResultOB.ExamPlanID));
            p.Add(db.CreateParameter("PostID", DbType.Int32, _ExamSubjectResultOB.PostID));
            p.Add(db.CreateParameter("SubjectiveTopicScore", DbType.Decimal, _ExamSubjectResultOB.SubjectiveTopicScore));
            p.Add(db.CreateParameter("ObjectiveTopicScore", DbType.Decimal, _ExamSubjectResultOB.ObjectiveTopicScore));
            p.Add(db.CreateParameter("SumScore", DbType.Decimal, _ExamSubjectResultOB.SumScore));
            p.Add(db.CreateParameter("ExamStatus", DbType.String, _ExamSubjectResultOB.ExamStatus));
            p.Add(db.CreateParameter("Status", DbType.String, _ExamSubjectResultOB.Status));
            p.Add(db.CreateParameter("CreatePersonID", DbType.Int64, _ExamSubjectResultOB.CreatePersonID));
            p.Add(db.CreateParameter("CreateTime", DbType.DateTime, _ExamSubjectResultOB.CreateTime));
            p.Add(db.CreateParameter("ModifyPersonID", DbType.Int64, _ExamSubjectResultOB.ModifyPersonID));
            p.Add(db.CreateParameter("ModifyTime", DbType.DateTime, _ExamSubjectResultOB.ModifyTime));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="ExamSubjectResultID">主键</param>
        /// <returns></returns>
        public static int Delete(long ExamSubjectResultID)
        {
            return Delete(null, ExamSubjectResultID);
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamSubjectResultID">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, long ExamSubjectResultID)
        {
            string sql = @"DELETE FROM dbo.ExamSubjectResult WHERE ExamSubjectResultID = @ExamSubjectResultID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamSubjectResultID", DbType.Int64, ExamSubjectResultID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据考试计划Id和科目id删除成绩
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamPlanID">考试计划Id</param>
        /// <param name="PostID">科目id</param>
        /// <returns></returns>
        public static int DeleteByKeMuID(DbTransaction tran, long ExamPlanID, int PostID)
        {
            string sql = @"DELETE FROM dbo.ExamSubjectResult WHERE ExamPlanID = @ExamPlanID and PostID= @PostID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, ExamPlanID));
            p.Add(db.CreateParameter("PostID", DbType.Int64, PostID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamSubjectResultOB">对象实体类</param>
        /// <returns></returns>
        public static int Delete(ExamSubjectResultOB _ExamSubjectResultOB)
        {
            return Delete(null, _ExamSubjectResultOB);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamSubjectResultOB">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, ExamSubjectResultOB _ExamSubjectResultOB)
        {
            string sql = @"DELETE FROM dbo.ExamSubjectResult WHERE ExamSubjectResultID = @ExamSubjectResultID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamSubjectResultID", DbType.Int64, _ExamSubjectResultOB.ExamSubjectResultID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ExamSubjectResultID">主键</param>
        public static ExamSubjectResultOB GetObject(long ExamSubjectResultID)
        {
            string sql = @"
			SELECT ExamSubjectResultID,ExamCardID,ExamPlanID,PostID,SubjectiveTopicScore,ObjectiveTopicScore,SumScore,ExamStatus,Status,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime
			FROM dbo.ExamSubjectResult
			WHERE ExamSubjectResultID = @ExamSubjectResultID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamSubjectResultID", DbType.Int64, ExamSubjectResultID));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    ExamSubjectResultOB _ExamSubjectResultOB = null;
                    if (reader.Read())
                    {
                        _ExamSubjectResultOB = new ExamSubjectResultOB();
                        if (reader["ExamSubjectResultID"] != DBNull.Value) _ExamSubjectResultOB.ExamSubjectResultID = Convert.ToInt64(reader["ExamSubjectResultID"]);
                        if (reader["ExamCardID"] != DBNull.Value) _ExamSubjectResultOB.ExamCardID = Convert.ToString(reader["ExamCardID"]);
                        if (reader["ExamPlanID"] != DBNull.Value) _ExamSubjectResultOB.ExamPlanID = Convert.ToInt64(reader["ExamPlanID"]);
                        if (reader["PostID"] != DBNull.Value) _ExamSubjectResultOB.PostID = Convert.ToInt32(reader["PostID"]);
                        if (reader["SubjectiveTopicScore"] != DBNull.Value) _ExamSubjectResultOB.SubjectiveTopicScore = Convert.ToDecimal(reader["SubjectiveTopicScore"]);
                        if (reader["ObjectiveTopicScore"] != DBNull.Value) _ExamSubjectResultOB.ObjectiveTopicScore = Convert.ToDecimal(reader["ObjectiveTopicScore"]);
                        if (reader["SumScore"] != DBNull.Value) _ExamSubjectResultOB.SumScore = Convert.ToDecimal(reader["SumScore"]);
                        if (reader["ExamStatus"] != DBNull.Value) _ExamSubjectResultOB.ExamStatus = Convert.ToString(reader["ExamStatus"]);
                        if (reader["Status"] != DBNull.Value) _ExamSubjectResultOB.Status = Convert.ToString(reader["Status"]);
                        if (reader["CreatePersonID"] != DBNull.Value) _ExamSubjectResultOB.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                        if (reader["CreateTime"] != DBNull.Value) _ExamSubjectResultOB.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                        if (reader["ModifyPersonID"] != DBNull.Value) _ExamSubjectResultOB.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                        if (reader["ModifyTime"] != DBNull.Value) _ExamSubjectResultOB.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                    }
                    reader.Close();
                    db.Close();
                    return _ExamSubjectResultOB;
                }
            }
            catch (Exception ex)
            {
                db.Close();
                throw ex;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.ExamSubjectResult", "*", filterWhereString, orderBy == "" ? " ExamSubjectResultID" : orderBy);
        }

        /// <summary>
        /// 获取考试科目成绩试图
        /// </summary>
        /// <param name="startRowIndex">开始行索引</param>
        /// <param name="maximumRows">每页最大行</param>
        /// <param name="filterWhereString">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <returns>DataTable</returns>
        /// <summary>
        public static DataTable GetListView(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.VIEW_EXAMRESULT as m inner join dbo.ExamSubjectResult as r on m.ExamPlanID= r.ExamPlanID and m.examcardid=r.examcardid left join dbo.PostInfo as k on r.PostID = k.PostID", "m.*,r.ExamSubjectResultID,r.ExamStatus,r.PostID as KeMuID,k.PostName as KeMuName", filterWhereString, orderBy == "" ? " m.ExamPlanID desc,k.PostName,WorkerName " : orderBy);
        }
        public static int SelectCountGetListView(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.VIEW_EXAMRESULT as m inner join dbo.ExamSubjectResult as r on m.ExamPlanID= r.ExamPlanID and m.examcardid=r.examcardid  left join dbo.PostInfo as k on r.PostID = k.PostID", filterWhereString);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.ExamSubjectResult", filterWhereString);
        }
    }
}
