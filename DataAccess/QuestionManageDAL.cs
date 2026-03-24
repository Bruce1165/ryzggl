using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

using Model;

namespace DataAccess
{
    public class QuestionManageDAL
    {
        public QuestionManageDAL() { }


        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="ExamPlanOB">对象实体类</param>
        /// <returns></returns>
        public static long Insert(QuestionManageOB _QuestionManageOB)
        {
            return Insert(null, _QuestionManageOB);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamPlanOB">对象实体类</param>
        /// <returns></returns>
        public static long Insert(DbTransaction tran, QuestionManageOB _QuestionManageOB)
        {
            DBHelper db = new DBHelper();          

            string sql = @"
			INSERT INTO dbo.Question(SubjectID,TagCode,QuestionType,Title,Option,OptionColumn,Answer,Flag,Difficulty)
                       VALUES (@SubjectID,@TagCode,@QuestionType,@Title,@Option,@OptionColumn,@Answer,@Flag,@Difficulty);SELECT @QuestionID = @@IDENTITY";

            List<SqlParameter> p = new List<SqlParameter>();

            p.Add(db.CreateOutParameter("QuestionID", DbType.Int64));
            p.Add(db.CreateParameter("SubjectID", DbType.Int64, _QuestionManageOB.SubjectID));
            p.Add(db.CreateParameter("TagCode", DbType.String, _QuestionManageOB.TagCode));
            p.Add(db.CreateParameter("QuestionType", DbType.String, _QuestionManageOB.QuestionType));
            p.Add(db.CreateParameter("Title", DbType.String, _QuestionManageOB.Title));
            p.Add(db.CreateParameter("Option", DbType.String, _QuestionManageOB.Option));
            p.Add(db.CreateParameter("OptionColumn", DbType.Int32, _QuestionManageOB.OptionColumn));
            p.Add(db.CreateParameter("Answer", DbType.String, _QuestionManageOB.Answer));
            p.Add(db.CreateParameter("Flag", DbType.String, _QuestionManageOB.Flag));
            p.Add(db.CreateParameter("Difficulty", DbType.String, _QuestionManageOB.Difficulty));

            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            _QuestionManageOB.QuestionID = Convert.ToInt64(p[0].Value);
            return rtn;
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="ExamPlanOB">对象实体类</param>
        /// <returns></returns>
        public static int Update(QuestionManageOB _QuestionManageOB)
        {
            return Update(null, _QuestionManageOB);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamPlanOB">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, QuestionManageOB _QuestionManageOB)
        {
            string sql = @"
			UPDATE dbo.QUESTION
				SET	Title=@Title,Flag=@Flag,Answer=@Answer,QuestionType=@QuestionType,Option=@Option,Difficulty=@Difficulty,OptionColumn=@OptionColumn,TagCode=@TagCode
			WHERE
				QuestionID = @QuestionID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("QuestionID", DbType.Int64, _QuestionManageOB.QuestionID));
            p.Add(db.CreateParameter("Title", DbType.String, _QuestionManageOB.Title));
            p.Add(db.CreateParameter("Flag", DbType.String, _QuestionManageOB.Flag));
            p.Add(db.CreateParameter("Answer", DbType.String, _QuestionManageOB.Answer));
            p.Add(db.CreateParameter("QuestionType", DbType.String, _QuestionManageOB.QuestionType));
            p.Add(db.CreateParameter("Option", DbType.String, _QuestionManageOB.Option));
            p.Add(db.CreateParameter("Difficulty", DbType.String, _QuestionManageOB.Difficulty));
            p.Add(db.CreateParameter("OptionColumn", DbType.Int32, _QuestionManageOB.OptionColumn));
            p.Add(db.CreateParameter("TagCode", DbType.Int32, _QuestionManageOB.TagCode));

            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="ExamPlanID">主键</param>
        /// <returns></returns>
        public static int Delete(long ExamPlanID)
        {
            return Delete(null, ExamPlanID);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamPlanID">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, long QuestionID)
        {
            string sql = @"DELETE FROM dbo.Question WHERE QuestionID = @QuestionID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("QuestionID", DbType.Int64, QuestionID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamPlanOB">对象实体类</param>
        /// <returns></returns>
        public static int Delete(ExamPlanOB _ExamPlanOB)
        {
            return Delete(null, _ExamPlanOB);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamPlanOB">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, ExamPlanOB _ExamPlanOB)
        {
            string sql = @"DELETE FROM dbo.ExamPlan WHERE ExamPlanID = @ExamPlanID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, _ExamPlanOB.ExamPlanID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ExamPlanID">主键</param>
        public static QuestionManageOB GetObject(long QuestionID)
        {
            string sql = @"
			SELECT QuestionID,SubjectID,TagCode,QuestionType,Title,Option,OptionColumn,Answer,Flag,Difficulty
FROM  DBO . QUESTION as e
   
			WHERE e.QuestionID = @QuestionID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("QuestionID", DbType.Int64, QuestionID));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    QuestionManageOB _QuestionManageOB = null;
                    if (reader.Read())
                    {
                        _QuestionManageOB = new QuestionManageOB();
                        if (reader["QuestionID"] != DBNull.Value) _QuestionManageOB.QuestionID = Convert.ToInt64(reader["QuestionID"]);
                        if (reader["SubjectID"] != DBNull.Value) _QuestionManageOB.SubjectID = Convert.ToInt64(reader["SubjectID"]);
                        if (reader["TagCode"] != DBNull.Value) _QuestionManageOB.TagCode = Convert.ToString(reader["TagCode"]);

                        if (reader["QuestionType"] != DBNull.Value) _QuestionManageOB.QuestionType = Convert.ToString(reader["QuestionType"]);
                        if (reader["Title"] != DBNull.Value) _QuestionManageOB.Title = Convert.ToString(reader["Title"]);
                        if (reader["Option"] != DBNull.Value) _QuestionManageOB.Option = Convert.ToString(reader["Option"]);
                        if (reader["OptionColumn"] != DBNull.Value) _QuestionManageOB.OptionColumn = Convert.ToInt32(reader["OptionColumn"]);

                        if (reader["Answer"] != DBNull.Value) _QuestionManageOB.Answer = Convert.ToString(reader["Answer"]);
                        ;
                        if (reader["Flag"] != DBNull.Value) _QuestionManageOB.Flag = Convert.ToString(reader["Flag"]);

                        if (reader["Difficulty"] != DBNull.Value) _QuestionManageOB.Difficulty = Convert.ToString(reader["Difficulty"]);



                    }
                    reader.Close();
                    db.Close();
                    return _QuestionManageOB;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.Question", "*", filterWhereString, orderBy == "" ? " QuestionID desc" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.Question", filterWhereString);
        }

        //public static DataTable GetListView(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        //{
        //    return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.View_ExamPlan", "*", filterWhereString, orderBy == "" ? " ExamPlanID desc" : orderBy);
        //}

        /// <summary>
        /// 获取考试计划是本年度第几次考试
        /// </summary>
        /// <param name="ExamPlanID">考试计划ID</param>
        /// <returns>年度第几次考试（3位）</returns>
        public static string GetIndexOfExamInYear(Int64 ExamPlanID)
        {
            string sql = string.Format(@"SELECT isnull(COUNT(distinct p.ExamPlanID),0) FROM dbo.ExamPlan as p inner join dbo.ExamResult as r on p.ExamPlanID = r.ExamPlanID 
                                        WHERE year(p.ExamStartDate) = (select year(ExamStartDate) from dbo.ExamPlan Where ExamplanID = {0})", ExamPlanID.ToString());
            DBHelper db = new DBHelper();
            int count = Convert.ToInt32(db.ExecuteScalar(sql));
            return Convert.ToString(count + 1).PadLeft(3, '0');
        }

        /// <summary>
        /// 检查考试计划是否所有科目都录入了成绩
        /// </summary>
        /// <param name="ExamPlanID">考试计划ID</param>
        /// <returns>未录入成绩科目数量</returns>
        public static int CheckExamPlanScoreFinish(Int64 ExamPlanID)
        {
            string sql = @"select (SELECT count(*) FROM DBO.EXAMPLANSUBJECT where EXAMPLANID = {0}) - (select count(*) from(SELECT count(*) FROM DBO.EXAMSUBJECTRESULT where EXAMPLANID = {0} group  by POSTID having count(*) >0)) as noscore";
            DBHelper db = new DBHelper();
            return Convert.ToInt32(db.ExecuteScalar(string.Format(sql, ExamPlanID.ToString())));
        }

        public static DataTable GetTopList(int _topCount, string _QuestionType, string _TableName)
        {
            string sql = @"select top  " + _topCount + " * from " + _TableName + " where 1=1 and QuestionType = " + _QuestionType;

            DBHelper db = new DBHelper();
            DataTable dt = new DataTable();

            //if (tran != null)
            //    dt = db.GetFillData(tran, sql);
            //else
            dt = db.GetFillData(sql);
            return dt;
        }
    }
}
