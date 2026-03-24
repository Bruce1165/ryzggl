using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--QuestionDAL(填写类描述)
	/// </summary>
    public class QuestionDAL
    {
        public QuestionDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="QuestionOB">对象实体类</param>
		/// <returns></returns>
        public static long Insert(QuestionOB _QuestionOB)
		{
		    return Insert(null,_QuestionOB);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="QuestionOB">对象实体类</param>
		/// <returns></returns>
        public static long Insert(DbTransaction tran,QuestionOB _QuestionOB)
		{
			DBHelper db = new DBHelper();			
		
			string sql = @"
			INSERT INTO dbo.Question(SubjectID,TagCode,ShowCode,QuestionType,Title,Answer,Flag,Difficulty,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime)
			VALUES (@SubjectID,@TagCode,@ShowCode,@QuestionType,@Title,@Answer,@Flag,@Difficulty,@CreatePersonID,@CreateTime,@ModifyPersonID,@ModifyTime);SELECT @QuestionID = @@IDENTITY";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateOutParameter("QuestionID",DbType.Int64));
			p.Add(db.CreateParameter("SubjectID",DbType.Int32, _QuestionOB.SubjectID));
			p.Add(db.CreateParameter("TagCode",DbType.Int32, _QuestionOB.TagCode));
			p.Add(db.CreateParameter("ShowCode",DbType.String, _QuestionOB.ShowCode));
			p.Add(db.CreateParameter("QuestionType",DbType.String, _QuestionOB.QuestionType));
			p.Add(db.CreateParameter("Title",DbType.String, _QuestionOB.Title));
			p.Add(db.CreateParameter("Answer",DbType.String, _QuestionOB.Answer));
			p.Add(db.CreateParameter("Flag",DbType.Byte, _QuestionOB.Flag));
			p.Add(db.CreateParameter("Difficulty",DbType.Byte, _QuestionOB.Difficulty));
			p.Add(db.CreateParameter("CreatePersonID",DbType.Int64, _QuestionOB.CreatePersonID));
			p.Add(db.CreateParameter("CreateTime",DbType.DateTime, _QuestionOB.CreateTime));
			p.Add(db.CreateParameter("ModifyPersonID",DbType.Int64, _QuestionOB.ModifyPersonID));
			p.Add(db.CreateParameter("ModifyTime",DbType.DateTime, _QuestionOB.ModifyTime));

            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            _QuestionOB.QuestionID = Convert.ToInt64(p[0].Value);
            return rtn;
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="QuestionOB">对象实体类</param>
		/// <returns></returns>
        public static int Update(QuestionOB _QuestionOB)
		{
			return Update(null,_QuestionOB);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="QuestionOB">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,QuestionOB _QuestionOB)
		{
			string sql = @"
			UPDATE dbo.Question
				SET	SubjectID = @SubjectID,TagCode = @TagCode,ShowCode = @ShowCode,QuestionType = @QuestionType,Title = @Title,Answer = @Answer,Flag = @Flag,Difficulty = @Difficulty,CreatePersonID = @CreatePersonID,CreateTime = @CreateTime,ModifyPersonID = @ModifyPersonID,ModifyTime = @ModifyTime
			WHERE
				QuestionID = @QuestionID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("QuestionID",DbType.Int64, _QuestionOB.QuestionID));
			p.Add(db.CreateParameter("SubjectID",DbType.Int32, _QuestionOB.SubjectID));
			p.Add(db.CreateParameter("TagCode",DbType.Int32, _QuestionOB.TagCode));
			p.Add(db.CreateParameter("ShowCode",DbType.String, _QuestionOB.ShowCode));
			p.Add(db.CreateParameter("QuestionType",DbType.String, _QuestionOB.QuestionType));
			p.Add(db.CreateParameter("Title",DbType.String, _QuestionOB.Title));
			p.Add(db.CreateParameter("Answer",DbType.String, _QuestionOB.Answer));
			p.Add(db.CreateParameter("Flag",DbType.Byte, _QuestionOB.Flag));
			p.Add(db.CreateParameter("Difficulty",DbType.Byte, _QuestionOB.Difficulty));
			p.Add(db.CreateParameter("CreatePersonID",DbType.Int64, _QuestionOB.CreatePersonID));
			p.Add(db.CreateParameter("CreateTime",DbType.DateTime, _QuestionOB.CreateTime));
			p.Add(db.CreateParameter("ModifyPersonID",DbType.Int64, _QuestionOB.ModifyPersonID));
			p.Add(db.CreateParameter("ModifyTime",DbType.DateTime, _QuestionOB.ModifyTime));
			return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="QuestionID">主键</param>
		/// <returns></returns>
        public static int Delete( long QuestionID )
		{
			return Delete(null, QuestionID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="QuestionID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, long QuestionID)
		{
			string sql=@"DELETE FROM dbo.Question WHERE QuestionID = @QuestionID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("QuestionID",DbType.Int64,QuestionID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}

        /// <summary>
        /// 清空某个专业科目试题
        /// </summary>
        /// <param name="SubjectID">科目ID</param>
        /// <returns></returns>
        public static int DeleteBySubjectID(string whereString)
        {
            string sql = string.Format(@"DELETE FROM dbo.Question WHERE 1=1 {0}",whereString);

            DBHelper db = new DBHelper();
      
            return db.ExcuteNonQuery(sql);
        }

		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="QuestionOB">对象实体类</param>
		/// <returns></returns>
        public static int Delete(QuestionOB _QuestionOB)
		{
			return Delete(null,_QuestionOB);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="QuestionOB">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,QuestionOB _QuestionOB)
		{
			string sql=@"DELETE FROM dbo.Question WHERE QuestionID = @QuestionID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("QuestionID",DbType.Int64,_QuestionOB.QuestionID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="QuestionID">主键</param>
        public static QuestionOB GetObject( long QuestionID )
		{
			string sql=@"
			SELECT QuestionID,SubjectID,TagCode,ShowCode,QuestionType,Title,Answer,Flag,Difficulty,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime
			FROM dbo.Question
			WHERE QuestionID = @QuestionID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("QuestionID", DbType.Int64, QuestionID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                QuestionOB _QuestionOB = null;
                if (reader.Read())
                {
                    _QuestionOB = new QuestionOB();
					if (reader["QuestionID"] != DBNull.Value) _QuestionOB.QuestionID = Convert.ToInt64(reader["QuestionID"]);
					if (reader["SubjectID"] != DBNull.Value) _QuestionOB.SubjectID = Convert.ToInt32(reader["SubjectID"]);
					if (reader["TagCode"] != DBNull.Value) _QuestionOB.TagCode = Convert.ToInt32(reader["TagCode"]);
					if (reader["ShowCode"] != DBNull.Value) _QuestionOB.ShowCode = Convert.ToString(reader["ShowCode"]);
					if (reader["QuestionType"] != DBNull.Value) _QuestionOB.QuestionType = Convert.ToString(reader["QuestionType"]);
					if (reader["Title"] != DBNull.Value) _QuestionOB.Title = Convert.ToString(reader["Title"]);
					if (reader["Answer"] != DBNull.Value) _QuestionOB.Answer = Convert.ToString(reader["Answer"]);
					if (reader["Flag"] != DBNull.Value) _QuestionOB.Flag = Convert.ToByte(reader["Flag"]);
					if (reader["Difficulty"] != DBNull.Value) _QuestionOB.Difficulty = Convert.ToByte(reader["Difficulty"]);
					if (reader["CreatePersonID"] != DBNull.Value) _QuestionOB.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
					if (reader["CreateTime"] != DBNull.Value) _QuestionOB.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
					if (reader["ModifyPersonID"] != DBNull.Value) _QuestionOB.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
					if (reader["ModifyTime"] != DBNull.Value) _QuestionOB.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                }
				reader.Close();
                db.Close();
                return _QuestionOB;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.Question", "*", filterWhereString, orderBy == "" ? " QuestionID" : orderBy);
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
    }
}
