using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--PageQuestionDAL(填写类描述)
	/// </summary>
    public class PageQuestionDAL
    {
        public PageQuestionDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="PageQuestionOB">对象实体类</param>
		/// <returns></returns>
        public static long Insert(PageQuestionOB _PageQuestionOB)
		{
		    return Insert(null,_PageQuestionOB);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="PageQuestionOB">对象实体类</param>
		/// <returns></returns>
        public static long Insert(DbTransaction tran,PageQuestionOB _PageQuestionOB)
		{
			DBHelper db = new DBHelper();
		
			string sql = @"
			INSERT INTO dbo.PageQuestion(ExamPageID,QuestionID,QuestionType,Title,Answer,QuestionNo,TagCode,Difficulty)
			VALUES (:ExamPageID,:QuestionID,:QuestionType,:Title,:Answer,:QuestionNo,:TagCode,:Difficulty)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ExamPageID",DbType.Int64, _PageQuestionOB.ExamPageID));
			p.Add(db.CreateParameter("QuestionID",DbType.Int64, _PageQuestionOB.QuestionID));
			p.Add(db.CreateParameter("QuestionType",DbType.String, _PageQuestionOB.QuestionType));
			p.Add(db.CreateParameter("Title",DbType.String, _PageQuestionOB.Title));
			p.Add(db.CreateParameter("Answer",DbType.String, _PageQuestionOB.Answer));
			p.Add(db.CreateParameter("QuestionNo",DbType.Int32, _PageQuestionOB.QuestionNo));
			p.Add(db.CreateParameter("TagCode",DbType.Int32, _PageQuestionOB.TagCode));
			p.Add(db.CreateParameter("Difficulty",DbType.Byte, _PageQuestionOB.Difficulty));
			return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="PageQuestionOB">对象实体类</param>
		/// <returns></returns>
        public static int Update(PageQuestionOB _PageQuestionOB)
		{
			return Update(null,_PageQuestionOB);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="PageQuestionOB">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,PageQuestionOB _PageQuestionOB)
		{
			string sql = @"
			UPDATE dbo.PageQuestion
				SET	QuestionType =@QuestionType,Title =@Title,Answer =@Answer,QuestionNo =@QuestionNo,TagCode =@TagCode,Difficulty =@Difficulty
			WHERE
				ExamPageID =@ExamPageID AND QuestionID =@QuestionID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ExamPageID",DbType.Int64, _PageQuestionOB.ExamPageID));
			p.Add(db.CreateParameter("QuestionID",DbType.Int64, _PageQuestionOB.QuestionID));
			p.Add(db.CreateParameter("QuestionType",DbType.String, _PageQuestionOB.QuestionType));
			p.Add(db.CreateParameter("Title",DbType.String, _PageQuestionOB.Title));
			p.Add(db.CreateParameter("Answer",DbType.String, _PageQuestionOB.Answer));
            p.Add(db.CreateParameter("QuestionNo", DbType.Int32, _PageQuestionOB.QuestionNo));
			p.Add(db.CreateParameter("TagCode",DbType.Int32, _PageQuestionOB.TagCode));
			p.Add(db.CreateParameter("Difficulty",DbType.Byte, _PageQuestionOB.Difficulty));
			return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="PageQuestionID">主键</param>
		/// <returns></returns>
        public static int Delete( long ExamPageID, long QuestionID )
		{
			return Delete(null, ExamPageID, QuestionID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamPageID">试卷ID</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, long ExamPageID)
        {
            string sql = @"DELETE FROM dbo.PageQuestion WHERE ExamPageID =@ExamPageID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPageID", DbType.Int64, ExamPageID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="PageQuestionID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, long ExamPageID, long QuestionID)
		{
			string sql=@"DELETE FROM dbo.PageQuestion WHERE ExamPageID =@ExamPageID AND QuestionID =@QuestionID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ExamPageID",DbType.Int64,ExamPageID));
			p.Add(db.CreateParameter("QuestionID",DbType.Int64,QuestionID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="PageQuestionOB">对象实体类</param>
		/// <returns></returns>
        public static int Delete(PageQuestionOB _PageQuestionOB)
		{
			return Delete(null,_PageQuestionOB);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="PageQuestionOB">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,PageQuestionOB _PageQuestionOB)
		{
			string sql=@"DELETE FROM dbo.PageQuestion WHERE ExamPageID =@ExamPageID AND QuestionID =@QuestionID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ExamPageID",DbType.Int64,_PageQuestionOB.ExamPageID));
			p.Add(db.CreateParameter("QuestionID",DbType.Int64,_PageQuestionOB.QuestionID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="PageQuestionID">主键</param>
        public static PageQuestionOB GetObject( long ExamPageID, long QuestionID )
		{
			string sql=@"
			SELECT ExamPageID,QuestionID,QuestionType,Title,Answer,QuestionNo,TagCode,Difficulty
			FROM dbo.PageQuestion
			WHERE ExamPageID =@ExamPageID AND QuestionID =@QuestionID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPageID", DbType.Int64, ExamPageID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                PageQuestionOB _PageQuestionOB = null;
                if (reader.Read())
                {
                    _PageQuestionOB = new PageQuestionOB();
					if (reader["ExamPageID"] != DBNull.Value) _PageQuestionOB.ExamPageID = Convert.ToInt64(reader["ExamPageID"]);
					if (reader["QuestionID"] != DBNull.Value) _PageQuestionOB.QuestionID = Convert.ToInt64(reader["QuestionID"]);
					if (reader["QuestionType"] != DBNull.Value) _PageQuestionOB.QuestionType = Convert.ToString(reader["QuestionType"]);
					if (reader["Title"] != DBNull.Value) _PageQuestionOB.Title = Convert.ToString(reader["Title"]);
					if (reader["Answer"] != DBNull.Value) _PageQuestionOB.Answer = Convert.ToString(reader["Answer"]);
					if (reader["QuestionNo"] != DBNull.Value) _PageQuestionOB.QuestionNo = Convert.ToInt32(reader["QuestionNo"]);
					if (reader["TagCode"] != DBNull.Value) _PageQuestionOB.TagCode = Convert.ToInt32(reader["TagCode"]);
					if (reader["Difficulty"] != DBNull.Value) _PageQuestionOB.Difficulty = Convert.ToByte(reader["Difficulty"]);
                }
				reader.Close();
                db.Close();
                return _PageQuestionOB;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.PageQuestion", "*", filterWhereString, orderBy == "" ? " ExamPageID, QuestionID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.PageQuestion", filterWhereString);
        }
    }
}
