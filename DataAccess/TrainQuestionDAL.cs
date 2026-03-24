using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--TrainQuestionDAL(填写类描述)
	/// </summary>
    public class TrainQuestionDAL
    {
        public TrainQuestionDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_TrainQuestionMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(TrainQuestionMDL _TrainQuestionMDL)
		{
		    return Insert(null,_TrainQuestionMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_TrainQuestionMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,TrainQuestionMDL _TrainQuestionMDL)
		{
            DBHelper db = new DBHelper("DBRYPX");		
			string sql = @"
			INSERT INTO dbo.TrainQuestion(QuestionType,Title,Score,Answer,Flag,LastModifyTime,QuestionNo,SourceID)
			VALUES (@QuestionType,@Title,@Score,@Answer,@Flag,@LastModifyTime,@QuestionNo,@SourceID);SELECT @QuestionID = @@IDENTITY";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateOutParameter("QuestionID",DbType.Int64));
			p.Add(db.CreateParameter("QuestionType",DbType.String, _TrainQuestionMDL.QuestionType));
			p.Add(db.CreateParameter("Title",DbType.String, _TrainQuestionMDL.Title));
			p.Add(db.CreateParameter("Score",DbType.Int32, _TrainQuestionMDL.Score));
			p.Add(db.CreateParameter("Answer",DbType.String, _TrainQuestionMDL.Answer));
			p.Add(db.CreateParameter("Flag",DbType.String, _TrainQuestionMDL.Flag));
			p.Add(db.CreateParameter("LastModifyTime",DbType.DateTime, _TrainQuestionMDL.LastModifyTime));
            p.Add(db.CreateParameter("QuestionNo", DbType.String, _TrainQuestionMDL.QuestionNo));
            p.Add(db.CreateParameter("SourceID", DbType.Int64, _TrainQuestionMDL.SourceID));
            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            _TrainQuestionMDL.QuestionID = Convert.ToInt64(p[0].Value);
            return rtn;
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_TrainQuestionMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(TrainQuestionMDL _TrainQuestionMDL)
		{
			return Update(null,_TrainQuestionMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_TrainQuestionMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,TrainQuestionMDL _TrainQuestionMDL)
		{
            string sql = @"
			UPDATE dbo.TrainQuestion
				SET	QuestionType = @QuestionType,Title = @Title,Score = @Score,Answer = @Answer,Flag = @Flag,LastModifyTime = @LastModifyTime,QuestionNo = @QuestionNo,SourceID = @SourceID
			WHERE
				QuestionID = @QuestionID";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("QuestionID", DbType.Int64, _TrainQuestionMDL.QuestionID));
            p.Add(db.CreateParameter("QuestionType", DbType.String, _TrainQuestionMDL.QuestionType));
            p.Add(db.CreateParameter("Title", DbType.String, _TrainQuestionMDL.Title));
            p.Add(db.CreateParameter("Score", DbType.Int32, _TrainQuestionMDL.Score));
            p.Add(db.CreateParameter("Answer", DbType.String, _TrainQuestionMDL.Answer));
            p.Add(db.CreateParameter("Flag", DbType.String, _TrainQuestionMDL.Flag));
            p.Add(db.CreateParameter("LastModifyTime", DbType.DateTime, _TrainQuestionMDL.LastModifyTime));
            p.Add(db.CreateParameter("QuestionNo", DbType.String, _TrainQuestionMDL.QuestionNo));
            p.Add(db.CreateParameter("SourceID", DbType.Int64, _TrainQuestionMDL.SourceID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="TrainQuestionID">主键</param>
		/// <returns></returns>
        public static int Delete( long QuestionID )
		{
			return Delete(null, QuestionID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="TrainQuestionID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, long QuestionID)
		{
			string sql=@"DELETE FROM dbo.TrainQuestion WHERE QuestionID = @QuestionID";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("QuestionID",DbType.Int64,QuestionID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="_TrainQuestionMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(TrainQuestionMDL _TrainQuestionMDL)
		{
			return Delete(null,_TrainQuestionMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_TrainQuestionMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,TrainQuestionMDL _TrainQuestionMDL)
		{
			string sql=@"DELETE FROM dbo.TrainQuestion WHERE QuestionID = @QuestionID";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("QuestionID",DbType.Int64,_TrainQuestionMDL.QuestionID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="TrainQuestionID">主键</param>
        public static TrainQuestionMDL GetObject( long QuestionID )
		{
            string sql = @"
			SELECT QuestionID,QuestionType,Title,Score,Answer,Flag,LastModifyTime,QuestionNo,SourceID
			FROM dbo.TrainQuestion
			WHERE QuestionID = @QuestionID";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("QuestionID", DbType.Int64, QuestionID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                TrainQuestionMDL _TrainQuestionMDL = null;
                if (reader.Read())
                {
                    _TrainQuestionMDL = new TrainQuestionMDL();
                    if (reader["QuestionID"] != DBNull.Value) _TrainQuestionMDL.QuestionID = Convert.ToInt64(reader["QuestionID"]);
                    if (reader["QuestionType"] != DBNull.Value) _TrainQuestionMDL.QuestionType = Convert.ToString(reader["QuestionType"]);
                    if (reader["Title"] != DBNull.Value) _TrainQuestionMDL.Title = Convert.ToString(reader["Title"]);
                    if (reader["Score"] != DBNull.Value) _TrainQuestionMDL.Score = Convert.ToInt32(reader["Score"]);
                    if (reader["Answer"] != DBNull.Value) _TrainQuestionMDL.Answer = Convert.ToString(reader["Answer"]);
                    if (reader["Flag"] != DBNull.Value) _TrainQuestionMDL.Flag = Convert.ToString(reader["Flag"]);
                    if (reader["LastModifyTime"] != DBNull.Value) _TrainQuestionMDL.LastModifyTime = Convert.ToDateTime(reader["LastModifyTime"]);
                    if (reader["QuestionNo"] != DBNull.Value) _TrainQuestionMDL.QuestionNo = Convert.ToString(reader["QuestionNo"]);
                    if (reader["SourceID"] != DBNull.Value) _TrainQuestionMDL.SourceID = Convert.ToInt64(reader["SourceID"]);
                }
                reader.Close();
                db.Close();
                return _TrainQuestionMDL;
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
            return CommonDAL.GetDataTableDB("DBRYPX", startRowIndex, maximumRows, "dbo.TrainQuestion", "*", filterWhereString, orderBy == "" ? " QuestionNo desc,QuestionID desc" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCountDB("DBRYPX","dbo.TrainQuestion", filterWhereString);
        }
        
        #region 自定义方法

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
            return CommonDAL.GetDataTableDB("DBRYPX", startRowIndex, maximumRows, "dbo.View_TrainQuestion", "*", filterWhereString, orderBy == "" ? " [SourceID] desc,[QuestionType],[QuestionNo]" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCountView(string filterWhereString)
        {
            return CommonDAL.SelectRowCountDB("DBRYPX", "dbo.View_TrainQuestion", filterWhereString);
        }


        /// <summary>
        /// 批量修改试题状态
        /// </summary>
        /// <param name="filterWhereString">过滤条件</param>
        /// <param name="Flag">启用 or 停用</param>
        /// <param name="LastModifyTime">更新时间</param>
        /// <returns></returns>
        public static int UpdateFlag(string filterWhereString, string Flag, DateTime LastModifyTime)
        {
            string sql = @"
			UPDATE TrainQuestion
				SET	Flag = @Flag,LastModifyTime = @LastModifyTime
			WHERE 1=1 " + filterWhereString;

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();

            p.Add(db.CreateParameter("Flag", DbType.String, Flag));
            p.Add(db.CreateParameter("LastModifyTime", DbType.DateTime, LastModifyTime));
            return db.ExcuteNonQuery(sql, p.ToArray());
        }

        /// <summary>
        /// 删除课程所有试题
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="SourceID">课程ID</param>
        /// <returns></returns>
        public static int DeleteBySourceID(DbTransaction tran, long SourceID)
        {
            string sql = @"DELETE FROM dbo.TrainQuestion WHERE SourceID = @SourceID";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("SourceID", DbType.Int64, SourceID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }

        #endregion
    }
}
