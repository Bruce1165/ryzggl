using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--ExamPageQuestionTypeDAL(填写类描述)
	/// </summary>
    public class ExamPageQuestionTypeDAL
    {
        public ExamPageQuestionTypeDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="ExamPageQuestionTypeOB">对象实体类</param>
		/// <returns></returns>
        public static long Insert(ExamPageQuestionTypeOB _ExamPageQuestionTypeOB)
		{
		    return Insert(null,_ExamPageQuestionTypeOB);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ExamPageQuestionTypeOB">对象实体类</param>
		/// <returns></returns>
        public static long Insert(DbTransaction tran,ExamPageQuestionTypeOB _ExamPageQuestionTypeOB)
		{
			DBHelper db = new DBHelper();
		
			string sql = @"
			INSERT INTO dbo.ExamPageQuestionType(ExamPageID,QuestionType,QuestionCount,Score,Remark,ShowOrder)
			VALUES (@ExamPageID,@QuestionType,@QuestionCount,@Score,@Remark,@ShowOrder)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ExamPageID",DbType.Int64, _ExamPageQuestionTypeOB.ExamPageID));
			p.Add(db.CreateParameter("QuestionType",DbType.String, _ExamPageQuestionTypeOB.QuestionType));
			p.Add(db.CreateParameter("QuestionCount",DbType.Int32, _ExamPageQuestionTypeOB.QuestionCount));
			p.Add(db.CreateParameter("Score",DbType.Int32, _ExamPageQuestionTypeOB.Score));
			p.Add(db.CreateParameter("Remark",DbType.String, _ExamPageQuestionTypeOB.Remark));
			p.Add(db.CreateParameter("ShowOrder",DbType.Int32, _ExamPageQuestionTypeOB.ShowOrder));
			return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="ExamPageQuestionTypeOB">对象实体类</param>
		/// <returns></returns>
        public static int Update(ExamPageQuestionTypeOB _ExamPageQuestionTypeOB)
		{
			return Update(null,_ExamPageQuestionTypeOB);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ExamPageQuestionTypeOB">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,ExamPageQuestionTypeOB _ExamPageQuestionTypeOB)
		{
			string sql = @"
			UPDATE dbo.ExamPageQuestionType
				SET	QuestionCount =@QuestionCount,Score =@Score,Remark =@Remark,ShowOrder =@ShowOrder
			WHERE
				ExamPageID =@ExamPageIDAND QuestionType =@QuestionType";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ExamPageID",DbType.Int64, _ExamPageQuestionTypeOB.ExamPageID));
			p.Add(db.CreateParameter("QuestionType",DbType.String, _ExamPageQuestionTypeOB.QuestionType));
			p.Add(db.CreateParameter("QuestionCount",DbType.Int32, _ExamPageQuestionTypeOB.QuestionCount));
			p.Add(db.CreateParameter("Score",DbType.Int32, _ExamPageQuestionTypeOB.Score));
			p.Add(db.CreateParameter("Remark",DbType.String, _ExamPageQuestionTypeOB.Remark));
			p.Add(db.CreateParameter("ShowOrder",DbType.Int32, _ExamPageQuestionTypeOB.ShowOrder));
			return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="ExamPageQuestionTypeID">主键</param>
		/// <returns></returns>
        public static int Delete( long ExamPageID)
		{
			return Delete(null, ExamPageID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="ExamPageQuestionTypeID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, long ExamPageID)
		{
			string sql=@"DELETE FROM dbo.ExamPageQuestionType WHERE ExamPageID =@ExamPageID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ExamPageID",DbType.Int64,ExamPageID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="ExamPageQuestionTypeOB">对象实体类</param>
		/// <returns></returns>
        public static int Delete(ExamPageQuestionTypeOB _ExamPageQuestionTypeOB)
		{
			return Delete(null,_ExamPageQuestionTypeOB);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ExamPageQuestionTypeOB">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,ExamPageQuestionTypeOB _ExamPageQuestionTypeOB)
		{
			string sql=@"DELETE FROM dbo.ExamPageQuestionType WHERE ExamPageID =@ExamPageIDAND QuestionType =@QuestionType";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ExamPageID",DbType.Int64,_ExamPageQuestionTypeOB.ExamPageID));
			p.Add(db.CreateParameter("QuestionType",DbType.String,_ExamPageQuestionTypeOB.QuestionType));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ExamPageQuestionTypeID">主键</param>
        public static ExamPageQuestionTypeOB GetObject( long ExamPageID, string QuestionType )
		{
			string sql=@"
			SELECT ExamPageID,QuestionType,QuestionCount,Score,Remark,ShowOrder
			FROM dbo.ExamPageQuestionType
			WHERE ExamPageID =@ExamPageIDAND QuestionType =@QuestionType";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPageID", DbType.Int64, ExamPageID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                ExamPageQuestionTypeOB _ExamPageQuestionTypeOB = null;
                if (reader.Read())
                {
                    _ExamPageQuestionTypeOB = new ExamPageQuestionTypeOB();
					if (reader["ExamPageID"] != DBNull.Value) _ExamPageQuestionTypeOB.ExamPageID = Convert.ToInt64(reader["ExamPageID"]);
					if (reader["QuestionType"] != DBNull.Value) _ExamPageQuestionTypeOB.QuestionType = Convert.ToString(reader["QuestionType"]);
					if (reader["QuestionCount"] != DBNull.Value) _ExamPageQuestionTypeOB.QuestionCount = Convert.ToInt32(reader["QuestionCount"]);
					if (reader["Score"] != DBNull.Value) _ExamPageQuestionTypeOB.Score = Convert.ToInt32(reader["Score"]);
					if (reader["Remark"] != DBNull.Value) _ExamPageQuestionTypeOB.Remark = Convert.ToString(reader["Remark"]);
					if (reader["ShowOrder"] != DBNull.Value) _ExamPageQuestionTypeOB.ShowOrder = Convert.ToInt32(reader["ShowOrder"]);
                }
				reader.Close();
                db.Close();
                return _ExamPageQuestionTypeOB;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.ExamPageQuestionType", "*", filterWhereString, orderBy == "" ? " ExamPageID, QuestionType" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.ExamPageQuestionType", filterWhereString);
        }

        /// <summary>
        /// 统计试卷各类题型难度试题数量
        /// </summary>
        /// <param name="ExamPageID">试卷ID</param>
        /// <returns>统计结果</returns>
        public static DataTable GetCountByDifficulty(long ExamPageID)
        {
            string sql = @"select t.QuestionType,t.QuestionCount,0 as SendCount,t.Score,isnull(t1.D1Count,0) D1Count,isnull(t2.D2Count,0) D2Count,isnull(t3.D3Count,0) D3Count
                            from
                            (select QuestionType,QuestionCount,ShowOrder,Score from dbo.ExamPageQuestionType
                            where ExamPageID=@ExamPageID) t
                            left join
                            (select QuestionType,count(*) as D1Count from  dbo.PageQuestion 
                            where ExamPageID=@ExamPageID and Difficulty =1
                            group by QuestionType) t1
                            on t.QuestionType = t1.QuestionType
                            left join
                            (select QuestionType,count(*) as D2Count from  dbo.PageQuestion 
                            where ExamPageID=@ExamPageID and Difficulty =2
                            group by QuestionType) t2
                            on t.QuestionType = t2.QuestionType
                            left join
                            (select QuestionType,count(*) as D3Count from  dbo.PageQuestion 
                            where ExamPageID=@ExamPageID and Difficulty =3
                            group by QuestionType) t3
                            on t.QuestionType = t3.QuestionType
                            order by t.ShowOrder";
            DBHelper db = new DBHelper();
            DataTable dt = new DataTable();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPageID", DbType.Int64, ExamPageID));
            dt = db.GetFillData(sql, p.ToArray());
            return dt;
        } 
    }
}
