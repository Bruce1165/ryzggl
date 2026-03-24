using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--ForeignPostInfoDAL(填写类描述)
	/// </summary>
    public class ForeignPostInfoDAL
    {
        public ForeignPostInfoDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="ForeignPostInfoOB">对象实体类</param>
		/// <returns></returns>
        public static int Insert(ForeignPostInfoOB _ForeignPostInfoOB)
		{
		    return Insert(null,_ForeignPostInfoOB);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ForeignPostInfoOB">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,ForeignPostInfoOB _ForeignPostInfoOB)
		{
			DBHelper db = new DBHelper();
		
			string sql = @"
			INSERT INTO dbo.ForeignPostInfo(PostID,PostType,PostName,UpPostID,ExamFee,CurrentNumber,CodeYear,CodeFormat)
			VALUES (:PostID,:PostType,:PostName,:UpPostID,:ExamFee,:CurrentNumber,:CodeYear,:CodeFormat)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("PostID",DbType.Int32, _ForeignPostInfoOB.PostID));
			p.Add(db.CreateParameter("PostType",DbType.String, _ForeignPostInfoOB.PostType));
			p.Add(db.CreateParameter("PostName",DbType.String, _ForeignPostInfoOB.PostName));
			p.Add(db.CreateParameter("UpPostID",DbType.Int32, _ForeignPostInfoOB.UpPostID));
			p.Add(db.CreateParameter("ExamFee",DbType.Decimal, _ForeignPostInfoOB.ExamFee));
            p.Add(db.CreateParameter("CurrentNumber", DbType.Int64, _ForeignPostInfoOB.CurrentNumber));
            p.Add(db.CreateParameter("CodeYear", DbType.Int32, _ForeignPostInfoOB.CodeYear));
            p.Add(db.CreateParameter("CodeFormat", DbType.String, _ForeignPostInfoOB.CodeFormat));
			return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="ForeignPostInfoOB">对象实体类</param>
		/// <returns></returns>
        public static int Update(ForeignPostInfoOB _ForeignPostInfoOB)
		{
			return Update(null,_ForeignPostInfoOB);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ForeignPostInfoOB">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,ForeignPostInfoOB _ForeignPostInfoOB)
		{
			string sql = @"
			UPDATE dbo.ForeignPostInfo
				SET	PostType =@PostType,PostName =@PostName,UpPostID =@UpPostID,ExamFee =@ExamFee,CurrentNumber =@CurrentNumber,CodeYear =@CodeYear,CodeFormat=@CodeFormat
			WHERE
				PostID =@PostID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("PostID",DbType.Int32, _ForeignPostInfoOB.PostID));
			p.Add(db.CreateParameter("PostType",DbType.String, _ForeignPostInfoOB.PostType));
			p.Add(db.CreateParameter("PostName",DbType.String, _ForeignPostInfoOB.PostName));
			p.Add(db.CreateParameter("UpPostID",DbType.Int32, _ForeignPostInfoOB.UpPostID));
			p.Add(db.CreateParameter("ExamFee",DbType.Decimal, _ForeignPostInfoOB.ExamFee));

            p.Add(db.CreateParameter("CurrentNumber", DbType.Int64, _ForeignPostInfoOB.CurrentNumber));
            p.Add(db.CreateParameter("CodeYear", DbType.Int32, _ForeignPostInfoOB.CodeYear));
            p.Add(db.CreateParameter("CodeFormat", DbType.String, _ForeignPostInfoOB.CodeFormat));
			return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="ForeignPostInfoID">主键</param>
		/// <returns></returns>
        public static int Delete( int PostID )
		{
			return Delete(null, PostID);
		}
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="ForeignPostInfoID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, int PostID)
		{
			string sql=@"DELETE FROM dbo.ForeignPostInfo WHERE PostID =@PostID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("PostID",DbType.Int32,PostID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="ForeignPostInfoOB">对象实体类</param>
		/// <returns></returns>
        public static int Delete(ForeignPostInfoOB _ForeignPostInfoOB)
		{
			return Delete(null,_ForeignPostInfoOB);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ForeignPostInfoOB">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,ForeignPostInfoOB _ForeignPostInfoOB)
		{
			string sql=@"DELETE FROM dbo.ForeignPostInfo WHERE PostID =@PostID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("PostID",DbType.Int32,_ForeignPostInfoOB.PostID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ForeignPostInfoID">主键</param>
        public static ForeignPostInfoOB GetObject(int PostID)
        {
            string sql = @"
			SELECT PostID,PostType,PostName,UpPostID,ExamFee,CurrentNumber,CodeYear,CodeFormat
			FROM dbo.ForeignPostInfo
			WHERE PostID =@PostID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PostID", DbType.Int32, PostID));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    ForeignPostInfoOB _ForeignPostInfoOB = null;
                    if (reader.Read())
                    {
                        _ForeignPostInfoOB = new ForeignPostInfoOB();
                        if (reader["PostID"] != DBNull.Value) _ForeignPostInfoOB.PostID = Convert.ToInt32(reader["PostID"]);
                        if (reader["PostType"] != DBNull.Value) _ForeignPostInfoOB.PostType = Convert.ToString(reader["PostType"]);
                        if (reader["PostName"] != DBNull.Value) _ForeignPostInfoOB.PostName = Convert.ToString(reader["PostName"]);
                        if (reader["UpPostID"] != DBNull.Value) _ForeignPostInfoOB.UpPostID = Convert.ToInt32(reader["UpPostID"]);
                        if (reader["ExamFee"] != DBNull.Value) _ForeignPostInfoOB.ExamFee = Convert.ToDecimal(reader["ExamFee"]);

                        if (reader["CurrentNumber"] != DBNull.Value) _ForeignPostInfoOB.CurrentNumber = Convert.ToInt64(reader["CurrentNumber"]);
                        if (reader["CodeYear"] != DBNull.Value) _ForeignPostInfoOB.CodeYear = Convert.ToInt32(reader["CodeYear"]);
                        if (reader["CodeFormat"] != DBNull.Value) _ForeignPostInfoOB.CodeFormat = Convert.ToString(reader["CodeFormat"]);
                    }
                    reader.Close();
                    db.Close();
                    return _ForeignPostInfoOB;
                }
            }
            catch (Exception ex)
            {
                db.Close();
                throw ex;
            }
        }

        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ForeignPostInfoID">主键</param>
        public static ForeignPostInfoOB GetObject(DbTransaction tran, int PostID)
        {
            string sql = @"
			SELECT PostID,PostType,PostName,UpPostID,ExamFee,CurrentNumber,CodeYear,CodeFormat
			FROM dbo.ForeignPostInfo
			WHERE PostID =@PostID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PostID", DbType.Int32, PostID));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(tran, sql, p.ToArray()))
                {
                    ForeignPostInfoOB _ForeignPostInfoOB = null;
                    if (reader.Read())
                    {
                        _ForeignPostInfoOB = new ForeignPostInfoOB();
                        if (reader["PostID"] != DBNull.Value) _ForeignPostInfoOB.PostID = Convert.ToInt32(reader["PostID"]);
                        if (reader["PostType"] != DBNull.Value) _ForeignPostInfoOB.PostType = Convert.ToString(reader["PostType"]);
                        if (reader["PostName"] != DBNull.Value) _ForeignPostInfoOB.PostName = Convert.ToString(reader["PostName"]);
                        if (reader["UpPostID"] != DBNull.Value) _ForeignPostInfoOB.UpPostID = Convert.ToInt32(reader["UpPostID"]);
                        if (reader["ExamFee"] != DBNull.Value) _ForeignPostInfoOB.ExamFee = Convert.ToDecimal(reader["ExamFee"]);

                        if (reader["CurrentNumber"] != DBNull.Value) _ForeignPostInfoOB.CurrentNumber = Convert.ToInt64(reader["CurrentNumber"]);
                        if (reader["CodeYear"] != DBNull.Value) _ForeignPostInfoOB.CodeYear = Convert.ToInt32(reader["CodeYear"]);
                        if (reader["CodeFormat"] != DBNull.Value) _ForeignPostInfoOB.CodeFormat = Convert.ToString(reader["CodeFormat"]);
                    }
                    reader.Close();
                    if (tran == null) db.Close();
                    return _ForeignPostInfoOB;
                }
            }
            catch (Exception ex)
            {
                if (tran == null) db.Close();
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.ForeignPostInfo", "*", filterWhereString, orderBy == "" ? " PostName" : orderBy);
        }

        /// <summary>
        /// 根据岗位类型ID获取考试科目名称列表
        /// </summary>
        /// <param name="PostID">岗位类型ID</param>
        /// <returns>考试科目名称列表</returns>
        public static DataTable GetListByPostID(string PostID)
        {
            //按科目ID排序
            return CommonDAL.GetDataTable(0, int.MaxValue - 1, "dbo.ForeignPostInfo", "PostName,0 as SubjectID,0 as ExamPlanSubjectID,null as ExamStartTime,null as ExamEndTime,0 as ExamFee", string.Format(" and UPPOSTID in (SELECT POSTID FROM DBO.ForeignPostInfo where UPPOSTID ={0}) group by PostName", PostID), " min(POSTID)");
        }

        /// <summary>
        /// 根据岗位类别获取第一个岗位工种对象
        /// </summary>
        /// <param name="UpPostID">岗位类别ID</param>
        /// <returns>岗位工种对象</returns>
        public static ForeignPostInfoOB GetFirtPostByPostTypeID(int UpPostID)
        {
            string sql = @"
			SELECT top(1) PostID,PostType,PostName,UpPostID,ExamFee,CurrentNumber,CodeYear,CodeFormat
			FROM dbo.ForeignPostInfo
			WHERE UpPostID =@UpPostID ";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("UpPostID", DbType.Int32, UpPostID));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    ForeignPostInfoOB _ForeignPostInfoOB = null;
                    if (reader.Read())
                    {
                        _ForeignPostInfoOB = new ForeignPostInfoOB();
                        if (reader["PostID"] != DBNull.Value) _ForeignPostInfoOB.PostID = Convert.ToInt32(reader["PostID"]);
                        if (reader["PostType"] != DBNull.Value) _ForeignPostInfoOB.PostType = Convert.ToString(reader["PostType"]);
                        if (reader["PostName"] != DBNull.Value) _ForeignPostInfoOB.PostName = Convert.ToString(reader["PostName"]);
                        if (reader["UpPostID"] != DBNull.Value) _ForeignPostInfoOB.UpPostID = Convert.ToInt32(reader["UpPostID"]);
                        if (reader["ExamFee"] != DBNull.Value) _ForeignPostInfoOB.ExamFee = Convert.ToDecimal(reader["ExamFee"]);
                        if (reader["CurrentNumber"] != DBNull.Value) _ForeignPostInfoOB.CurrentNumber = Convert.ToInt64(reader["CurrentNumber"]);
                        if (reader["CodeYear"] != DBNull.Value) _ForeignPostInfoOB.CodeYear = Convert.ToInt32(reader["CodeYear"]);
                        if (reader["CodeFormat"] != DBNull.Value) _ForeignPostInfoOB.CodeFormat = Convert.ToString(reader["CodeFormat"]);

                    }
                    reader.Close();
                    db.Close();
                    return _ForeignPostInfoOB;
                }
            }
            catch (Exception ex)
            {
                db.Close();
                throw ex;
            }
        } 

		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.ForeignPostInfo", filterWhereString);
        }

        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ForeignPostInfoID">主键</param>
        public static ForeignPostInfoOB GetObject(int UpPostID, string PostName)
        {
            string sql = @"
			SELECT PostID,PostType,PostName,UpPostID,ExamFee,CurrentNumber,CodeYear,CodeFormat
			FROM dbo.ForeignPostInfo
			WHERE UpPostID =@UpPostID and PostName=@PostName";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("UpPostID", DbType.Int32, UpPostID));
            p.Add(db.CreateParameter("PostName", DbType.String, PostName));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    ForeignPostInfoOB _ForeignPostInfoOB = null;
                    if (reader.Read())
                    {
                        _ForeignPostInfoOB = new ForeignPostInfoOB();
                        if (reader["PostID"] != DBNull.Value) _ForeignPostInfoOB.PostID = Convert.ToInt32(reader["PostID"]);
                        if (reader["PostType"] != DBNull.Value) _ForeignPostInfoOB.PostType = Convert.ToString(reader["PostType"]);
                        if (reader["PostName"] != DBNull.Value) _ForeignPostInfoOB.PostName = Convert.ToString(reader["PostName"]);
                        if (reader["UpPostID"] != DBNull.Value) _ForeignPostInfoOB.UpPostID = Convert.ToInt32(reader["UpPostID"]);
                        if (reader["ExamFee"] != DBNull.Value) _ForeignPostInfoOB.ExamFee = Convert.ToDecimal(reader["ExamFee"]);
                        if (reader["CurrentNumber"] != DBNull.Value) _ForeignPostInfoOB.CurrentNumber = Convert.ToInt64(reader["CurrentNumber"]);
                        if (reader["CodeYear"] != DBNull.Value) _ForeignPostInfoOB.CodeYear = Convert.ToInt32(reader["CodeYear"]);
                        if (reader["CodeFormat"] != DBNull.Value) _ForeignPostInfoOB.CodeFormat = Convert.ToString(reader["CodeFormat"]);

                    }
                    reader.Close();
                    db.Close();
                    return _ForeignPostInfoOB;
                }
            }
            catch (Exception ex)
            {
                db.Close();
                throw ex;
            }
        }

        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ForeignPostInfoID">主键</param>
        public static List<ForeignPostInfoOB> GetObject()
        {
            string sql = @"
			SELECT distinct PostName
			FROM dbo.ForeignPostInfo
			WHERE POSTTYPE = 3";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            List<ForeignPostInfoOB> dt = new List<ForeignPostInfoOB>();
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    ForeignPostInfoOB _ForeignPostInfoOB = null;
                    while (reader.Read())
                    {
                        _ForeignPostInfoOB = new ForeignPostInfoOB();

                        //if (reader["PostType"] != DBNull.Value) _ForeignPostInfoOB.PostType = Convert.ToString(reader["PostType"]);
                        if (reader["PostName"] != DBNull.Value) _ForeignPostInfoOB.PostName = Convert.ToString(reader["PostName"]);
                        //if (reader["UpPostID"] != DBNull.Value) _ForeignPostInfoOB.UpPostID = Convert.ToInt32(reader["UpPostID"]);
                        //if (reader["ExamFee"] != DBNull.Value) _ForeignPostInfoOB.ExamFee = Convert.ToDecimal(reader["ExamFee"]);
                        dt.Add(_ForeignPostInfoOB);
                    }
                    reader.Close();
                    db.Close();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                db.Close();
                throw ex;
            }
        }

        /// <summary>
        /// 获取外埠岗位工种表最大ID
        /// </summary>
        /// <returns></returns>
        public static int SelectMaxPostID()
        {
            string sql = @"
			SELECT isnull(max(PostID),0) FROM dbo.ForeignPostInfo";

            DBHelper db = new DBHelper();
            return Convert.ToInt32(db.ExecuteScalar(sql));
        }
    }
}
