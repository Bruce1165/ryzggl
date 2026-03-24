using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--ExamRangeDAL(填写类描述)
	/// </summary>
    public class ExamRangeDAL
    {
        public ExamRangeDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="ExamRangeOB">对象实体类</param>
		/// <returns></returns>
        public static int Insert(ExamRangeOB _ExamRangeOB)
		{
		    return Insert(null,_ExamRangeOB);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ExamRangeOB">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran, ExamRangeOB _ExamRangeOB)
		{
			DBHelper db = new DBHelper();
		
			string sql = @"
			INSERT INTO dbo.ExamRange(ExamYear,SubjectID,Flag,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,Remark)
			VALUES (:ExamYear,:SubjectID,:Flag,:CreatePersonID,:CreateTime,:ModifyPersonID,:ModifyTime,:Remark)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ExamYear",DbType.Int16, _ExamRangeOB.ExamYear));
			p.Add(db.CreateParameter("SubjectID",DbType.Int32, _ExamRangeOB.SubjectID));
			p.Add(db.CreateParameter("Flag",DbType.Byte, _ExamRangeOB.Flag));
			p.Add(db.CreateParameter("CreatePersonID",DbType.Int64, _ExamRangeOB.CreatePersonID));
			p.Add(db.CreateParameter("CreateTime",DbType.DateTime, _ExamRangeOB.CreateTime));
			p.Add(db.CreateParameter("ModifyPersonID",DbType.Int64, _ExamRangeOB.ModifyPersonID));
			p.Add(db.CreateParameter("ModifyTime",DbType.DateTime, _ExamRangeOB.ModifyTime));
			p.Add(db.CreateParameter("Remark",DbType.String, _ExamRangeOB.Remark));
			return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="ExamRangeOB">对象实体类</param>
		/// <returns></returns>
        public static int Update(ExamRangeOB _ExamRangeOB)
		{
			return Update(null,_ExamRangeOB);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ExamRangeOB">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,ExamRangeOB _ExamRangeOB)
		{
			string sql = @"
			UPDATE dbo.ExamRange
				SET	Flag =@Flag,CreatePersonID =@CreatePersonID,CreateTime =@CreateTime,ModifyPersonID =@ModifyPersonID,ModifyTime =@ModifyTime,Remark =@Remark
			WHERE
				ExamYear =@ExamYearAND SubjectID =@SubjectID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ExamYear",DbType.Int16, _ExamRangeOB.ExamYear));
			p.Add(db.CreateParameter("SubjectID",DbType.Int32, _ExamRangeOB.SubjectID));
			p.Add(db.CreateParameter("Flag",DbType.Byte, _ExamRangeOB.Flag));
			p.Add(db.CreateParameter("CreatePersonID",DbType.Int64, _ExamRangeOB.CreatePersonID));
			p.Add(db.CreateParameter("CreateTime",DbType.DateTime, _ExamRangeOB.CreateTime));
			p.Add(db.CreateParameter("ModifyPersonID",DbType.Int64, _ExamRangeOB.ModifyPersonID));
			p.Add(db.CreateParameter("ModifyTime",DbType.DateTime, _ExamRangeOB.ModifyTime));
			p.Add(db.CreateParameter("Remark",DbType.String, _ExamRangeOB.Remark));
			return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="ExamRangeID">主键</param>
		/// <returns></returns>
        public static int Delete( short ExamYear, int SubjectID )
		{
			return Delete(null, ExamYear, SubjectID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="ExamRangeID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, short ExamYear, int SubjectID)
		{
			string sql=@"DELETE FROM dbo.ExamRange WHERE ExamYear =@ExamYear AND SubjectID =@SubjectID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ExamYear",DbType.Int16,ExamYear));
			p.Add(db.CreateParameter("SubjectID",DbType.Int32,SubjectID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="ExamRangeOB">对象实体类</param>
		/// <returns></returns>
        public static int Delete(ExamRangeOB _ExamRangeOB)
		{
			return Delete(null,_ExamRangeOB);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ExamRangeOB">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,ExamRangeOB _ExamRangeOB)
		{
			string sql=@"DELETE FROM dbo.ExamRange WHERE ExamYear =@ExamYearAND SubjectID =@SubjectID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ExamYear",DbType.Int16,_ExamRangeOB.ExamYear));
			p.Add(db.CreateParameter("SubjectID",DbType.Int32,_ExamRangeOB.SubjectID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ExamRangeID">主键</param>
        public static ExamRangeOB GetObject( short ExamYear, int SubjectID )
		{
			string sql=@"
			SELECT ExamYear,SubjectID,Flag,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime,Remark
			FROM dbo.ExamRange
			WHERE ExamYear =@ExamYearAND SubjectID =@SubjectID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamYear", DbType.Int16, ExamYear));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                ExamRangeOB _ExamRangeOB = null;
                if (reader.Read())
                {
                    _ExamRangeOB = new ExamRangeOB();
					if (reader["ExamYear"] != DBNull.Value) _ExamRangeOB.ExamYear = Convert.ToInt16(reader["ExamYear"]);
					if (reader["SubjectID"] != DBNull.Value) _ExamRangeOB.SubjectID = Convert.ToInt32(reader["SubjectID"]);
					if (reader["Flag"] != DBNull.Value) _ExamRangeOB.Flag = Convert.ToByte(reader["Flag"]);
					if (reader["CreatePersonID"] != DBNull.Value) _ExamRangeOB.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
					if (reader["CreateTime"] != DBNull.Value) _ExamRangeOB.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
					if (reader["ModifyPersonID"] != DBNull.Value) _ExamRangeOB.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
					if (reader["ModifyTime"] != DBNull.Value) _ExamRangeOB.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
					if (reader["Remark"] != DBNull.Value) _ExamRangeOB.Remark = Convert.ToString(reader["Remark"]);
                }
				reader.Close();
                db.Close();
                return _ExamRangeOB;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.ExamRange", "*", filterWhereString, orderBy == "" ? " ExamYear, SubjectID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.ExamRange", filterWhereString);
        }

        #region 用户自定义方法

        /// <summary>
        /// 批量拷贝知识大纲到考试大纲中
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="ExamYear">考试年度</param>
        /// <param name="CreatePersonID">创建人ID</param>
        /// <param name="CreateTime">创建时间</param>
        /// <param name="PostTypeID">岗位类别</param>
        /// <param name="PostID">岗位工种</param>
        /// <returns></returns>
        public static int InsertPatch(DbTransaction tran, int ExamYear, long CreatePersonID, DateTime CreateTime, string PostTypeID, string PostID)
        {
            DBHelper db = new DBHelper();

            string sql = @"
			INSERT INTO dbo.ExamRange(ExamYear,SubjectID,Flag,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime)
            SELECT distinct {0},SubjectID,Flag,{1},'{2}',{1},'{2}' from dbo.view_InfoTag
			where Flag=1 {3} {4}";

            return db.ExcuteNonQuery(tran, string.Format(sql, ExamYear.ToString(), CreatePersonID, CreateTime.ToString()
                , PostTypeID == "" ? "" : string.Format(" and PostTypeID={0}", PostTypeID)
                , PostID == "" ? "" : string.Format(" and PostID={0}", PostID)
                ));
        }

        /// <summary>
        /// 批量拷贝知识大纲明细到考试大纲中
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="ExamYear">考试年度</param>
        /// <param name="PostTypeID">岗位类别</param>
        /// <param name="PostID">岗位工种</param>
        /// <returns></returns>
        public static int InsertPatchExamRangeSub(DbTransaction tran, int ExamYear, string PostTypeID, string PostID)
        {
            DBHelper db = new DBHelper();

            string sql = @"
			INSERT INTO dbo.ExamRangeSub(EXAMYEAR,SUBJECTID,TAGID,TAGCODE,TITLE,WEIGHT,SHOWCODE)
            SELECT {0},SubjectID,TAGID,TAGCODE,TITLE,WEIGHT,SHOWCODE from dbo.view_InfoTag
			where Flag=1 {1} {2}";

            return db.ExcuteNonQuery(tran, string.Format(sql, ExamYear.ToString()
                , PostTypeID == "" ? "" : string.Format(" and PostTypeID={0}", PostTypeID)
                , PostID == "" ? "" : string.Format(" and PostID={0}", PostID)
                ));
        }

        /// <summary>
        /// 根据年度和岗位工种清除考试大纲
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="ExamYear">年度</param>
        /// <param name="PostTypeID">岗位类别ID</param>
        /// <param name="PostID">岗位工种ID</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, short ExamYear, string PostTypeID, string PostID)
        {
            string sql = @"DELETE FROM dbo.ExamRange WHERE ExamYear = {0} AND SubjectID in(select SubjectID from dbo.view_postinfo where 1=1 {1} {2})";

            DBHelper db = new DBHelper();
            return db.ExcuteNonQuery(tran, string.Format(sql,ExamYear.ToString()
                  , PostTypeID == "" ? "" : string.Format(" and PostTypeID={0}", PostTypeID)
                , PostID == "" ? "" : string.Format(" and PostID={0}", PostID)
                ));
        }

        #endregion 用户自定义方法
    }
}
