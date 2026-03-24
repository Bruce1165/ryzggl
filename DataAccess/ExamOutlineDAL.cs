using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

using Model;
namespace DataAccess
{
    public class ExamOutlineDAL
    {

        public ExamOutlineDAL() { }       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="ExamPlanOB">对象实体类</param>
		/// <returns></returns>
        public static long Insert(ExamOutlineOB _ExamOutlineOB)
		{
            return Insert(null, _ExamOutlineOB);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ExamPlanOB">对象实体类</param>
		/// <returns></returns>
        public static long Insert(DbTransaction tran, ExamOutlineOB _ExamOutlineOB)
		{
			DBHelper db = new DBHelper();			
		
			string sql = @"
			INSERT INTO dbo.ExamRange(ExamYear,SubjectID,Flag,CreatePersonID,CreateTime ,ModifyPersonID,ModifyTime)
			VALUES (@ExamYear,@SubjectID,@Flag,@CreatePersonID,@CreateTime,@ModifyPersonID,@ModifyTime)";

		    List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamYear", DbType.Int32, _ExamOutlineOB.ExamYear));
            p.Add(db.CreateParameter("SubjectID", DbType.Int32, _ExamOutlineOB.SubjectID));
            p.Add(db.CreateParameter("Flag", DbType.Int32, _ExamOutlineOB.Flag));
            p.Add(db.CreateParameter("CreatePersonID", DbType.Int64, _ExamOutlineOB.CreatePersonID));
            p.Add(db.CreateParameter("CreateTime", DbType.DateTime, _ExamOutlineOB.CreateTime));
            p.Add(db.CreateParameter("ModifyPersonID", DbType.Int64, _ExamOutlineOB.ModifyPersonID));
            p.Add(db.CreateParameter("ModifyTime", DbType.DateTime, _ExamOutlineOB.ModifyTime));
           
			int t1= db.ExcuteNonQuery(tran, sql, p.ToArray());
            //子表
             sql = @"
			INSERT INTO dbo.ExamRangeSub(ExamYear,SubjectID,TagID,TagCode,Title)
		                       	VALUES (@ExamYear,@SubjectID,@TagID  ,@TagCode,@Title)";

                p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamYear", DbType.Int32, _ExamOutlineOB.ExamYear));
            p.Add(db.CreateParameter("SubjectID", DbType.Int32, _ExamOutlineOB.SubjectID));
            p.Add(db.CreateParameter("TagID", DbType.Int32, _ExamOutlineOB.TageID));
            p.Add(db.CreateParameter("TagCode", DbType.String, _ExamOutlineOB.TagCode));
            p.Add(db.CreateParameter("Title", DbType.String, _ExamOutlineOB.Title));
           

            int t2 = db.ExcuteNonQuery(tran, sql, p.ToArray());
            return t2;
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="ExamPlanOB">对象实体类</param>
		/// <returns></returns>
        public static int Update(ExamPlanOB _ExamPlanOB)
		{
			return Update(null,_ExamPlanOB);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ExamPlanOB">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,ExamPlanOB _ExamPlanOB)
		{
			string sql = @"
			UPDATE dbo.ExamPlan
				SET	PostTypeID = @PostTypeID,PostID = @PostID,SignUpStartDate = @SignUpStartDate,SignUpEndDate = @SignUpEndDate,ExamCardSendStartDate = @ExamCardSendStartDate,ExamCardSendEndDate = @ExamCardSendEndDate,ExamStartDate = @ExamStartDate,ExamEndDate = @ExamEndDate,SignUpPlace = @SignUpPlace,Remark = @Remark,Status = @Status,ExamPlanName = @ExamPlanName,CreatePersonID = @CreatePersonID,CreateTime = @CreateTime,ModifyPersonID = @ModifyPersonID,ModifyTime = @ModifyTime,LatestCheckDate = @LatestCheckDate,LatestPayDate = @LatestPayDate,ExamFee = @ExamFee,IfPublish = @IfPublish,PlanSkillLevel = @PlanSkillLevel
			WHERE
				ExamPlanID = @ExamPlanID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ExamPlanID",DbType.Int64, _ExamPlanOB.ExamPlanID));
			p.Add(db.CreateParameter("PostTypeID",DbType.Int32, _ExamPlanOB.PostTypeID));
			p.Add(db.CreateParameter("PostID",DbType.Int32, _ExamPlanOB.PostID));
			p.Add(db.CreateParameter("SignUpStartDate",DbType.DateTime, _ExamPlanOB.SignUpStartDate));
			p.Add(db.CreateParameter("SignUpEndDate",DbType.DateTime, _ExamPlanOB.SignUpEndDate));
			p.Add(db.CreateParameter("ExamCardSendStartDate",DbType.DateTime, _ExamPlanOB.ExamCardSendStartDate));
			p.Add(db.CreateParameter("ExamCardSendEndDate",DbType.DateTime, _ExamPlanOB.ExamCardSendEndDate));
			p.Add(db.CreateParameter("ExamStartDate",DbType.DateTime, _ExamPlanOB.ExamStartDate));
			p.Add(db.CreateParameter("ExamEndDate",DbType.DateTime, _ExamPlanOB.ExamEndDate));
			p.Add(db.CreateParameter("SignUpPlace",DbType.String, _ExamPlanOB.SignUpPlace));
			p.Add(db.CreateParameter("Remark",DbType.String, _ExamPlanOB.Remark));
			p.Add(db.CreateParameter("Status",DbType.String, _ExamPlanOB.Status));
			p.Add(db.CreateParameter("ExamPlanName",DbType.String, _ExamPlanOB.ExamPlanName));
			p.Add(db.CreateParameter("CreatePersonID",DbType.Int64, _ExamPlanOB.CreatePersonID));
			p.Add(db.CreateParameter("CreateTime",DbType.DateTime, _ExamPlanOB.CreateTime));
			p.Add(db.CreateParameter("ModifyPersonID",DbType.Int64, _ExamPlanOB.ModifyPersonID));
			p.Add(db.CreateParameter("ModifyTime",DbType.DateTime, _ExamPlanOB.ModifyTime));
            p.Add(db.CreateParameter("LatestCheckDate", DbType.DateTime, _ExamPlanOB.LatestCheckDate));
            p.Add(db.CreateParameter("LatestPayDate", DbType.DateTime, _ExamPlanOB.LatestPayDate));
            p.Add(db.CreateParameter("ExamFee", DbType.Decimal, _ExamPlanOB.ExamFee));
            p.Add(db.CreateParameter("IfPublish", DbType.String, _ExamPlanOB.IfPublish));
            p.Add(db.CreateParameter("PlanSkillLevel", DbType.String, _ExamPlanOB.PlanSkillLevel));
			return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="ExamPlanID">主键</param>
		/// <returns></returns>
        public static int Delete(long SubjectID, int ExamYear)
		{
			return Delete(null, SubjectID, ExamYear);
		}
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="ExamPlanID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, long SubjectID, int ExamYear)
		{
            string sql = @"DELETE FROM dbo.ExamRange WHERE SubjectID = @SubjectID and ExamYear= @ExamYear";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("SubjectID", DbType.Int64, SubjectID));
            p.Add(db.CreateParameter("ExamYear", DbType.Int32, ExamYear));
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
			return Delete(null,_ExamPlanOB);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ExamPlanOB">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,ExamPlanOB _ExamPlanOB)
		{
			string sql=@"DELETE FROM dbo.ExamPlan WHERE ExamPlanID = @ExamPlanID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ExamPlanID",DbType.Int64,_ExamPlanOB.ExamPlanID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ExamPlanID">主键</param>
        public static ExamPlanOB GetObject(long ExamPlanID)
        {
            string sql = @"
			SELECT ExamPlanID,e.PostTypeID,pt.PostName as PostTypeName,e.PostID,p.PostName,SignUpStartDate,SignUpEndDate,ExamCardSendStartDate,ExamCardSendEndDate,ExamStartDate,ExamEndDate,SignUpPlace,Remark,Status,ExamPlanName,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,LatestCheckDate,LatestPayDate,e.ExamFee,e.IfPublish,e.PlanSkillLevel
			FROM dbo.ExamPlan as e 
            left join dbo.PostInfo as pt on e.PostID = pt.PostID
            left join dbo.PostInfo as p on e.PostID = p.PostID   
			WHERE e.ExamPlanID = @ExamPlanID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, ExamPlanID));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    ExamPlanOB _ExamPlanOB = null;
                    if (reader.Read())
                    {
                        _ExamPlanOB = new ExamPlanOB();
                        if (reader["ExamPlanID"] != DBNull.Value) _ExamPlanOB.ExamPlanID = Convert.ToInt64(reader["ExamPlanID"]);
                        if (reader["PostTypeID"] != DBNull.Value) _ExamPlanOB.PostTypeID = Convert.ToInt32(reader["PostTypeID"]);
                        if (reader["PostID"] != DBNull.Value) _ExamPlanOB.PostID = Convert.ToInt32(reader["PostID"]);
                        if (reader["SignUpStartDate"] != DBNull.Value) _ExamPlanOB.SignUpStartDate = Convert.ToDateTime(reader["SignUpStartDate"]);
                        if (reader["SignUpEndDate"] != DBNull.Value) _ExamPlanOB.SignUpEndDate = Convert.ToDateTime(reader["SignUpEndDate"]);
                        if (reader["ExamCardSendStartDate"] != DBNull.Value) _ExamPlanOB.ExamCardSendStartDate = Convert.ToDateTime(reader["ExamCardSendStartDate"]);
                        if (reader["ExamCardSendEndDate"] != DBNull.Value) _ExamPlanOB.ExamCardSendEndDate = Convert.ToDateTime(reader["ExamCardSendEndDate"]);
                        if (reader["ExamStartDate"] != DBNull.Value) _ExamPlanOB.ExamStartDate = Convert.ToDateTime(reader["ExamStartDate"]);
                        if (reader["ExamEndDate"] != DBNull.Value) _ExamPlanOB.ExamEndDate = Convert.ToDateTime(reader["ExamEndDate"]);
                        if (reader["SignUpPlace"] != DBNull.Value) _ExamPlanOB.SignUpPlace = Convert.ToString(reader["SignUpPlace"]);
                        if (reader["Remark"] != DBNull.Value) _ExamPlanOB.Remark = Convert.ToString(reader["Remark"]);
                        if (reader["Status"] != DBNull.Value) _ExamPlanOB.Status = Convert.ToString(reader["Status"]);
                        if (reader["ExamPlanName"] != DBNull.Value) _ExamPlanOB.ExamPlanName = Convert.ToString(reader["ExamPlanName"]);
                        if (reader["CreatePersonID"] != DBNull.Value) _ExamPlanOB.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                        if (reader["CreateTime"] != DBNull.Value) _ExamPlanOB.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                        if (reader["ModifyPersonID"] != DBNull.Value) _ExamPlanOB.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                        if (reader["ModifyTime"] != DBNull.Value) _ExamPlanOB.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                        if (reader["PostTypeName"] != DBNull.Value) _ExamPlanOB.PostTypeName = Convert.ToString(reader["PostTypeName"]);
                        if (reader["PostName"] != DBNull.Value) _ExamPlanOB.PostName = Convert.ToString(reader["PostName"]);
                        if (reader["LatestCheckDate"] != DBNull.Value) _ExamPlanOB.LatestCheckDate = Convert.ToDateTime(reader["LatestCheckDate"]);
                        if (reader["LatestPayDate"] != DBNull.Value) _ExamPlanOB.LatestPayDate = Convert.ToDateTime(reader["LatestPayDate"]);
                        if (reader["ExamFee"] != DBNull.Value) _ExamPlanOB.ExamFee = Convert.ToDecimal(reader["ExamFee"]);
                        if (reader["IfPublish"] != DBNull.Value) _ExamPlanOB.IfPublish = Convert.ToString(reader["IfPublish"]);
                        if (reader["PlanSkillLevel"] != DBNull.Value) _ExamPlanOB.PlanSkillLevel = Convert.ToString(reader["PlanSkillLevel"]);
                    }
                    reader.Close();
                    db.Close();
                    return _ExamPlanOB;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.View_ExamOutline", "*", filterWhereString, orderBy == "" ? " ExamYear desc" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.VIEW_ExamOutline", filterWhereString);
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
            return Convert.ToInt32(db.ExecuteScalar(string.Format(sql,ExamPlanID.ToString())));
        }
    }
}
