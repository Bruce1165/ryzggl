using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--ExamPlanSubjectDAL(填写类描述)
	/// </summary>
    public class ExamPlanSubjectDAL
    {
        public ExamPlanSubjectDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="ExamPlanSubjectOB">对象实体类</param>
		/// <returns></returns>
        public static long Insert(ExamPlanSubjectOB _ExamPlanSubjectOB)
		{
		    return Insert(null,_ExamPlanSubjectOB);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ExamPlanSubjectOB">对象实体类</param>
		/// <returns></returns>
        public static long Insert(DbTransaction tran,ExamPlanSubjectOB _ExamPlanSubjectOB)
		{
			DBHelper db = new DBHelper();
		
			string sql = @"
			INSERT INTO dbo.ExamPlanSubject(ExamPlanID,PostID,ExamStartTime,ExamEndTime,PassLine,ExamFee,Status,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime)
			VALUES (@ExamPlanID,@PostID,@ExamStartTime,@ExamEndTime,@PassLine,@ExamFee,@Status,@CreatePersonID,@CreateTime,@ModifyPersonID,@ModifyTime);SELECT @ExamPlanSubjectID = @@IDENTITY";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateOutParameter("ExamPlanSubjectID",DbType.Int64));
			p.Add(db.CreateParameter("ExamPlanID",DbType.Int64, _ExamPlanSubjectOB.ExamPlanID));
			p.Add(db.CreateParameter("PostID",DbType.Int32, _ExamPlanSubjectOB.PostID));
			p.Add(db.CreateParameter("ExamStartTime",DbType.DateTime, _ExamPlanSubjectOB.ExamStartTime));
			p.Add(db.CreateParameter("ExamEndTime",DbType.DateTime, _ExamPlanSubjectOB.ExamEndTime));
			p.Add(db.CreateParameter("PassLine",DbType.Int32, _ExamPlanSubjectOB.PassLine));
			p.Add(db.CreateParameter("ExamFee",DbType.Decimal, _ExamPlanSubjectOB.ExamFee));
			p.Add(db.CreateParameter("Status",DbType.String, _ExamPlanSubjectOB.Status));
			p.Add(db.CreateParameter("CreatePersonID",DbType.Int64, _ExamPlanSubjectOB.CreatePersonID));
			p.Add(db.CreateParameter("CreateTime",DbType.DateTime, _ExamPlanSubjectOB.CreateTime));
			p.Add(db.CreateParameter("ModifyPersonID",DbType.Int64, _ExamPlanSubjectOB.ModifyPersonID));
			p.Add(db.CreateParameter("ModifyTime",DbType.DateTime, _ExamPlanSubjectOB.ModifyTime));
            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            _ExamPlanSubjectOB.ExamPlanSubjectID = Convert.ToInt64(p[0].Value);
            return rtn;
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="ExamPlanSubjectOB">对象实体类</param>
		/// <returns></returns>
        public static int Update(ExamPlanSubjectOB _ExamPlanSubjectOB)
		{
			return Update(null,_ExamPlanSubjectOB);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ExamPlanSubjectOB">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,ExamPlanSubjectOB _ExamPlanSubjectOB)
		{
			string sql = @"
			UPDATE dbo.ExamPlanSubject
				SET	ExamPlanID = @ExamPlanID,PostID = @PostID,ExamStartTime = @ExamStartTime,ExamEndTime = @ExamEndTime,PassLine = @PassLine,ExamFee = @ExamFee,Status = @Status,CreatePersonID = @CreatePersonID,CreateTime = @CreateTime,ModifyPersonID = @ModifyPersonID,ModifyTime = @ModifyTime
			WHERE
				ExamPlanSubjectID = @ExamPlanSubjectID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ExamPlanSubjectID",DbType.Int64, _ExamPlanSubjectOB.ExamPlanSubjectID));
			p.Add(db.CreateParameter("ExamPlanID",DbType.Int64, _ExamPlanSubjectOB.ExamPlanID));
			p.Add(db.CreateParameter("PostID",DbType.Int32, _ExamPlanSubjectOB.PostID));
			p.Add(db.CreateParameter("ExamStartTime",DbType.DateTime, _ExamPlanSubjectOB.ExamStartTime));
			p.Add(db.CreateParameter("ExamEndTime",DbType.DateTime, _ExamPlanSubjectOB.ExamEndTime));
			p.Add(db.CreateParameter("PassLine",DbType.Int32, _ExamPlanSubjectOB.PassLine));
			p.Add(db.CreateParameter("ExamFee",DbType.Decimal, _ExamPlanSubjectOB.ExamFee));
			p.Add(db.CreateParameter("Status",DbType.String, _ExamPlanSubjectOB.Status));
			p.Add(db.CreateParameter("CreatePersonID",DbType.Int64, _ExamPlanSubjectOB.CreatePersonID));
			p.Add(db.CreateParameter("CreateTime",DbType.DateTime, _ExamPlanSubjectOB.CreateTime));
			p.Add(db.CreateParameter("ModifyPersonID",DbType.Int64, _ExamPlanSubjectOB.ModifyPersonID));
			p.Add(db.CreateParameter("ModifyTime",DbType.DateTime, _ExamPlanSubjectOB.ModifyTime));
			return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
         

		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="ExamPlanSubjectID">主键</param>
		/// <returns></returns>
        public static int Delete( long ExamPlanSubjectID )
		{
			return Delete(null, ExamPlanSubjectID);
		}
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="ExamPlanSubjectID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, long ExamPlanSubjectID)
		{
			string sql=@"DELETE FROM dbo.ExamPlanSubject WHERE ExamPlanSubjectID = @ExamPlanSubjectID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ExamPlanSubjectID",DbType.Int64,ExamPlanSubjectID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}

        /// <summary>
        /// 根据考试计划id删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ExamPlanSubjectID">主键</param>
        /// <returns></returns>
        public static int DeleteByExamPlanID(DbTransaction tran, long ExamPlanID)
        {
            string sql = @"DELETE FROM dbo.ExamPlanSubject WHERE ExamPlanID = @ExamPlanID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, ExamPlanID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="ExamPlanSubjectOB">对象实体类</param>
		/// <returns></returns>
        public static int Delete(ExamPlanSubjectOB _ExamPlanSubjectOB)
		{
			return Delete(null,_ExamPlanSubjectOB);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ExamPlanSubjectOB">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,ExamPlanSubjectOB _ExamPlanSubjectOB)
		{
			string sql=@"DELETE FROM dbo.ExamPlanSubject WHERE ExamPlanSubjectID = @ExamPlanSubjectID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ExamPlanSubjectID",DbType.Int64,_ExamPlanSubjectOB.ExamPlanSubjectID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ExamPlanSubjectID">主键</param>
        public static ExamPlanSubjectOB GetObject(long ExamPlanSubjectID)
        {
            string sql = @"
			SELECT ExamPlanSubjectID,ExamPlanID,PostID,ExamStartTime,ExamEndTime,PassLine,ExamFee,Status,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime
			FROM dbo.ExamPlanSubject
			WHERE ExamPlanSubjectID = @ExamPlanSubjectID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPlanSubjectID", DbType.Int64, ExamPlanSubjectID));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    ExamPlanSubjectOB _ExamPlanSubjectOB = null;
                    if (reader.Read())
                    {
                        _ExamPlanSubjectOB = new ExamPlanSubjectOB();
                        if (reader["ExamPlanSubjectID"] != DBNull.Value) _ExamPlanSubjectOB.ExamPlanSubjectID = Convert.ToInt64(reader["ExamPlanSubjectID"]);
                        if (reader["ExamPlanID"] != DBNull.Value) _ExamPlanSubjectOB.ExamPlanID = Convert.ToInt64(reader["ExamPlanID"]);
                        if (reader["PostID"] != DBNull.Value) _ExamPlanSubjectOB.PostID = Convert.ToInt32(reader["PostID"]);
                        if (reader["ExamStartTime"] != DBNull.Value) _ExamPlanSubjectOB.ExamStartTime = Convert.ToDateTime(reader["ExamStartTime"]);
                        if (reader["ExamEndTime"] != DBNull.Value) _ExamPlanSubjectOB.ExamEndTime = Convert.ToDateTime(reader["ExamEndTime"]);
                        if (reader["PassLine"] != DBNull.Value) _ExamPlanSubjectOB.PassLine = Convert.ToInt32(reader["PassLine"]);
                        if (reader["ExamFee"] != DBNull.Value) _ExamPlanSubjectOB.ExamFee = Convert.ToDecimal(reader["ExamFee"]);
                        if (reader["Status"] != DBNull.Value) _ExamPlanSubjectOB.Status = Convert.ToString(reader["Status"]);
                        if (reader["CreatePersonID"] != DBNull.Value) _ExamPlanSubjectOB.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                        if (reader["CreateTime"] != DBNull.Value) _ExamPlanSubjectOB.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                        if (reader["ModifyPersonID"] != DBNull.Value) _ExamPlanSubjectOB.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                        if (reader["ModifyTime"] != DBNull.Value) _ExamPlanSubjectOB.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                    }
                    reader.Close();
                    db.Close();
                    return _ExamPlanSubjectOB;
                }
            }
            catch (Exception ex)
            {
                db.Close();
                throw ex;
            }
        }
        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="ExamPlanID">考试计划ID</param>
        /// <param name="PostID">科目ID</param>
        public static ExamPlanSubjectOB GetObject(long ExamPlanID, int PostID)
        {
            return GetObject(null, ExamPlanID, PostID);
//            string sql = @"
//			SELECT ExamPlanSubjectID,ExamPlanID,PostID,ExamStartTime,ExamEndTime,PassLine,ExamFee,Status,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime
//			FROM dbo.ExamPlanSubject
//			WHERE ExamPlanID =@ExamPlanID and PostID=@PostID";

//            DBHelper db = new DBHelper();
//            List<SqlParameter> p = new List<SqlParameter>();
//            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, ExamPlanID));
//            p.Add(db.CreateParameter("PostID", DbType.Int32, PostID));
//            try
//            {
//                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
//                {
//                    ExamPlanSubjectOB _ExamPlanSubjectOB = null;
//                    if (reader.Read())
//                    {
//                        _ExamPlanSubjectOB = new ExamPlanSubjectOB();
//                        if (reader["ExamPlanSubjectID"] != DBNull.Value) _ExamPlanSubjectOB.ExamPlanSubjectID = Convert.ToInt64(reader["ExamPlanSubjectID"]);
//                        if (reader["ExamPlanID"] != DBNull.Value) _ExamPlanSubjectOB.ExamPlanID = Convert.ToInt64(reader["ExamPlanID"]);
//                        if (reader["PostID"] != DBNull.Value) _ExamPlanSubjectOB.PostID = Convert.ToInt32(reader["PostID"]);
//                        if (reader["ExamStartTime"] != DBNull.Value) _ExamPlanSubjectOB.ExamStartTime = Convert.ToDateTime(reader["ExamStartTime"]);
//                        if (reader["ExamEndTime"] != DBNull.Value) _ExamPlanSubjectOB.ExamEndTime = Convert.ToDateTime(reader["ExamEndTime"]);
//                        if (reader["PassLine"] != DBNull.Value) _ExamPlanSubjectOB.PassLine = Convert.ToInt32(reader["PassLine"]);
//                        if (reader["ExamFee"] != DBNull.Value) _ExamPlanSubjectOB.ExamFee = Convert.ToDecimal(reader["ExamFee"]);
//                        if (reader["Status"] != DBNull.Value) _ExamPlanSubjectOB.Status = Convert.ToString(reader["Status"]);
//                        if (reader["CreatePersonID"] != DBNull.Value) _ExamPlanSubjectOB.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
//                        if (reader["CreateTime"] != DBNull.Value) _ExamPlanSubjectOB.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
//                        if (reader["ModifyPersonID"] != DBNull.Value) _ExamPlanSubjectOB.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
//                        if (reader["ModifyTime"] != DBNull.Value) _ExamPlanSubjectOB.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
//                    }
//                    reader.Close();
//                    db.Close();
//                    return _ExamPlanSubjectOB;
//                }
//            }
//            catch (Exception ex)
//            {
//                db.Close();
//                throw ex;
//            }
        }
        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="ExamPlanID">考试计划ID</param>
        /// <param name="PostID">科目ID</param>
        public static ExamPlanSubjectOB GetObject(DbTransaction tran, long ExamPlanID, int PostID)
        {
            string sql = @"
			SELECT ExamPlanSubjectID,ExamPlanID,PostID,ExamStartTime,ExamEndTime,PassLine,ExamFee,Status,CreatePersonID,CreateTime,ModifyPersonID,ModifyTime
			FROM dbo.ExamPlanSubject
			WHERE ExamPlanID =@ExamPlanID and PostID=@PostID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, ExamPlanID));
            p.Add(db.CreateParameter("PostID", DbType.Int32, PostID));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(tran,sql, p.ToArray()))
                {
                    ExamPlanSubjectOB _ExamPlanSubjectOB = null;
                    if (reader.Read())
                    {
                        _ExamPlanSubjectOB = new ExamPlanSubjectOB();
                        if (reader["ExamPlanSubjectID"] != DBNull.Value) _ExamPlanSubjectOB.ExamPlanSubjectID = Convert.ToInt64(reader["ExamPlanSubjectID"]);
                        if (reader["ExamPlanID"] != DBNull.Value) _ExamPlanSubjectOB.ExamPlanID = Convert.ToInt64(reader["ExamPlanID"]);
                        if (reader["PostID"] != DBNull.Value) _ExamPlanSubjectOB.PostID = Convert.ToInt32(reader["PostID"]);
                        if (reader["ExamStartTime"] != DBNull.Value) _ExamPlanSubjectOB.ExamStartTime = Convert.ToDateTime(reader["ExamStartTime"]);
                        if (reader["ExamEndTime"] != DBNull.Value) _ExamPlanSubjectOB.ExamEndTime = Convert.ToDateTime(reader["ExamEndTime"]);
                        if (reader["PassLine"] != DBNull.Value) _ExamPlanSubjectOB.PassLine = Convert.ToInt32(reader["PassLine"]);
                        if (reader["ExamFee"] != DBNull.Value) _ExamPlanSubjectOB.ExamFee = Convert.ToDecimal(reader["ExamFee"]);
                        if (reader["Status"] != DBNull.Value) _ExamPlanSubjectOB.Status = Convert.ToString(reader["Status"]);
                        if (reader["CreatePersonID"] != DBNull.Value) _ExamPlanSubjectOB.CreatePersonID = Convert.ToInt64(reader["CreatePersonID"]);
                        if (reader["CreateTime"] != DBNull.Value) _ExamPlanSubjectOB.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                        if (reader["ModifyPersonID"] != DBNull.Value) _ExamPlanSubjectOB.ModifyPersonID = Convert.ToInt64(reader["ModifyPersonID"]);
                        if (reader["ModifyTime"] != DBNull.Value) _ExamPlanSubjectOB.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                    }
                    reader.Close();
                    if (tran == null) db.Close();
                    return _ExamPlanSubjectOB;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.ExamPlanSubject", "*", filterWhereString, orderBy == "" ? " ExamPlanSubjectID" : orderBy);
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
        public static DataTable GetList(Int64 ExamPlanID)
        {
            return CommonDAL.GetDataTable(0, int.MaxValue - 1, "dbo.ExamPlanSubject left join dbo.PostInfo on dbo.ExamPlanSubject.PostID = dbo.PostInfo.PostID", "dbo.ExamPlanSubject.*,dbo.PostInfo.PostName", string.Format(" and ExamPlanID={0}", ExamPlanID.ToString()), " ExamPlanSubjectID");
        }
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.ExamPlanSubject", filterWhereString);
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
        public static DataTable GetListView(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.ExamPlanSubject left join dbo.PostInfo on dbo.ExamPlanSubject.PostID = dbo.PostInfo.PostID", "dbo.ExamPlanSubject.*,dbo.PostInfo.PostName", filterWhereString, orderBy == "" ? " ExamPlanSubjectID" : orderBy);
        } 
    }
}
