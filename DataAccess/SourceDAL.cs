using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--SourceDAL(填写类描述)
	/// </summary>
    public class SourceDAL
    {
        public SourceDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_SourceMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(SourceMDL _SourceMDL)
		{
		    return Insert(null,_SourceMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_SourceMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,SourceMDL _SourceMDL)
		{
			DBHelper db = new DBHelper("DBRYPX");		
			string sql = @"
			INSERT INTO RYPX.dbo.Source(SourceName,Teacher,WorkUnit,SourceType,Status,Description,ParentSourceID,SortID,Period,SourceWareCount,SourceWareUrl,SourceWarePlayParam,SourceYear,Lab,BarType,SourceImg,ShowPeriod)
			VALUES (@SourceName,@Teacher,@WorkUnit,@SourceType,@Status,@Description,@ParentSourceID,@SortID,@Period,@SourceWareCount,@SourceWareUrl,@SourceWarePlayParam,@SourceYear,@Lab,@BarType,@SourceImg,@ShowPeriod);SELECT @SourceID = @@IDENTITY";

		    List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateOutParameter("SourceID", DbType.Int64));            
			p.Add(db.CreateParameter("SourceName",DbType.String, _SourceMDL.SourceName));
			p.Add(db.CreateParameter("Teacher",DbType.String, _SourceMDL.Teacher));
			p.Add(db.CreateParameter("WorkUnit",DbType.String, _SourceMDL.WorkUnit));
			p.Add(db.CreateParameter("SourceType",DbType.String, _SourceMDL.SourceType));
			p.Add(db.CreateParameter("Status",DbType.String, _SourceMDL.Status));
			p.Add(db.CreateParameter("Description",DbType.String, _SourceMDL.Description));
			p.Add(db.CreateParameter("ParentSourceID",DbType.Int64, _SourceMDL.ParentSourceID));
			p.Add(db.CreateParameter("SortID",DbType.Int32, _SourceMDL.SortID));
			p.Add(db.CreateParameter("Period",DbType.Int32, _SourceMDL.Period));
			p.Add(db.CreateParameter("SourceWareCount",DbType.Int32, _SourceMDL.SourceWareCount));
			p.Add(db.CreateParameter("SourceWareUrl",DbType.String, _SourceMDL.SourceWareUrl));
			p.Add(db.CreateParameter("SourceWarePlayParam",DbType.String, _SourceMDL.SourceWarePlayParam));
			p.Add(db.CreateParameter("SourceYear",DbType.Int32, _SourceMDL.SourceYear));
            p.Add(db.CreateParameter("Lab", DbType.String, _SourceMDL.Lab));
            p.Add(db.CreateParameter("BarType", DbType.String, _SourceMDL.BarType));
            p.Add(db.CreateParameter("SourceImg", DbType.String, _SourceMDL.SourceImg));
            p.Add(db.CreateParameter("ShowPeriod", DbType.Decimal, _SourceMDL.ShowPeriod));

            int rtn = db.GetExcuteNonQuery(tran, sql, p.ToArray());
            _SourceMDL.SourceID = Convert.ToInt64(p[0].Value);
            return rtn;
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_SourceMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(SourceMDL _SourceMDL)
		{
			return Update(null,_SourceMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_SourceMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,SourceMDL _SourceMDL)
		{
			string sql = @"
			UPDATE dbo.Source
				SET	SourceName = @SourceName,Teacher = @Teacher,WorkUnit = @WorkUnit,SourceType = @SourceType,Status = @Status,Description = @Description,ParentSourceID = @ParentSourceID,SortID = @SortID,Period = @Period,SourceWareCount = @SourceWareCount,SourceWareUrl = @SourceWareUrl,SourceWarePlayParam = @SourceWarePlayParam,SourceYear = @SourceYear,Lab = @Lab,BarType = @BarType,SourceImg = @SourceImg,ShowPeriod = @ShowPeriod
			WHERE
				SourceID = @SourceID";

            DBHelper db = new DBHelper("DBRYPX");
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("SourceID",DbType.Int64, _SourceMDL.SourceID));
			p.Add(db.CreateParameter("SourceName",DbType.String, _SourceMDL.SourceName));
			p.Add(db.CreateParameter("Teacher",DbType.String, _SourceMDL.Teacher));
			p.Add(db.CreateParameter("WorkUnit",DbType.String, _SourceMDL.WorkUnit));
			p.Add(db.CreateParameter("SourceType",DbType.String, _SourceMDL.SourceType));
			p.Add(db.CreateParameter("Status",DbType.String, _SourceMDL.Status));
			p.Add(db.CreateParameter("Description",DbType.String, _SourceMDL.Description));
			p.Add(db.CreateParameter("ParentSourceID",DbType.Int64, _SourceMDL.ParentSourceID));
			p.Add(db.CreateParameter("SortID",DbType.Int32, _SourceMDL.SortID));
			p.Add(db.CreateParameter("Period",DbType.Int32, _SourceMDL.Period));
			p.Add(db.CreateParameter("SourceWareCount",DbType.Int32, _SourceMDL.SourceWareCount));
			p.Add(db.CreateParameter("SourceWareUrl",DbType.String, _SourceMDL.SourceWareUrl));
			p.Add(db.CreateParameter("SourceWarePlayParam",DbType.String, _SourceMDL.SourceWarePlayParam));
			p.Add(db.CreateParameter("SourceYear",DbType.Int32, _SourceMDL.SourceYear));
            p.Add(db.CreateParameter("Lab", DbType.String, _SourceMDL.Lab));
            p.Add(db.CreateParameter("BarType", DbType.String, _SourceMDL.BarType));
            p.Add(db.CreateParameter("SourceImg", DbType.String, _SourceMDL.SourceImg));
            p.Add(db.CreateParameter("ShowPeriod", DbType.Decimal, _SourceMDL.ShowPeriod));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="SourceID">主键</param>
		/// <returns></returns>
        public static int Delete( long SourceID )
		{
			return Delete(null, SourceID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="SourceID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, long SourceID)
		{
			string sql=@"DELETE FROM dbo.Source WHERE SourceID = @SourceID";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("SourceID",DbType.Int64,SourceID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="_SourceMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(SourceMDL _SourceMDL)
		{
			return Delete(null,_SourceMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_SourceMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,SourceMDL _SourceMDL)
		{
			string sql=@"DELETE FROM dbo.Source WHERE SourceID = @SourceID";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("SourceID",DbType.Int64,_SourceMDL.SourceID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="SourceID">主键</param>
        public static SourceMDL GetObject( long SourceID )
		{
			string sql= @"
			SELECT SourceID,SourceName,Teacher,WorkUnit,SourceType,Status,Description,ParentSourceID,SortID,Period,SourceWareCount,SourceWareUrl,SourceWarePlayParam,SourceYear,Lab,BarType,SourceImg,ShowPeriod
			FROM dbo.Source
			WHERE SourceID = @SourceID";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("SourceID",DbType.Int64,SourceID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                SourceMDL _SourceMDL = null;
                if (reader.Read())
                {
                    _SourceMDL = new SourceMDL();
					if (reader["SourceID"] != DBNull.Value) _SourceMDL.SourceID = Convert.ToInt64(reader["SourceID"]);
					if (reader["SourceName"] != DBNull.Value) _SourceMDL.SourceName = Convert.ToString(reader["SourceName"]);
					if (reader["Teacher"] != DBNull.Value) _SourceMDL.Teacher = Convert.ToString(reader["Teacher"]);
					if (reader["WorkUnit"] != DBNull.Value) _SourceMDL.WorkUnit = Convert.ToString(reader["WorkUnit"]);
					if (reader["SourceType"] != DBNull.Value) _SourceMDL.SourceType = Convert.ToString(reader["SourceType"]);
					if (reader["Status"] != DBNull.Value) _SourceMDL.Status = Convert.ToString(reader["Status"]);
					if (reader["Description"] != DBNull.Value) _SourceMDL.Description = Convert.ToString(reader["Description"]);
					if (reader["ParentSourceID"] != DBNull.Value) _SourceMDL.ParentSourceID = Convert.ToInt64(reader["ParentSourceID"]);
					if (reader["SortID"] != DBNull.Value) _SourceMDL.SortID = Convert.ToInt32(reader["SortID"]);
					if (reader["Period"] != DBNull.Value) _SourceMDL.Period = Convert.ToInt32(reader["Period"]);
					if (reader["SourceWareCount"] != DBNull.Value) _SourceMDL.SourceWareCount = Convert.ToInt32(reader["SourceWareCount"]);
					if (reader["SourceWareUrl"] != DBNull.Value) _SourceMDL.SourceWareUrl = Convert.ToString(reader["SourceWareUrl"]);
					if (reader["SourceWarePlayParam"] != DBNull.Value) _SourceMDL.SourceWarePlayParam = Convert.ToString(reader["SourceWarePlayParam"]);
					if (reader["SourceYear"] != DBNull.Value) _SourceMDL.SourceYear = Convert.ToInt32(reader["SourceYear"]);
                    if (reader["Lab"] != DBNull.Value) _SourceMDL.Lab = Convert.ToString(reader["Lab"]);
                    if (reader["BarType"] != DBNull.Value) _SourceMDL.BarType = Convert.ToString(reader["BarType"]);
                    if (reader["SourceImg"] != DBNull.Value) _SourceMDL.SourceImg = Convert.ToString(reader["SourceImg"]);
                    if (reader["ShowPeriod"] != DBNull.Value) _SourceMDL.ShowPeriod = Convert.ToDecimal(reader["ShowPeriod"]);
                }
				reader.Close();
                db.Close();
                return _SourceMDL;
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
            return CommonDAL.GetDataTableDB("DBRYPX", startRowIndex, maximumRows, "dbo.Source", "*", filterWhereString, orderBy == "" ? " [SourceYear] desc,[SortID]" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCountDB("DBRYPX", "dbo.Source", filterWhereString);
        }
        
        #region 自定义方法

        /// <summary>
        /// 将课件学时转化为显示学时：保留以1位小数（规则：小于0.1计为0，大于等于0.1且小于0.6计为0.5，大于等于0.6计为1.0）
        /// </summary>
        /// <param name="Period">课件实际学分（分钟）</param>
        /// <returns>显示学时（保留以为小数）</returns>
        public static decimal ConvertShowPeriod(int Period)
        {
            int temp = Period % 45;
            decimal rtn = (temp >= 27 ? 1 : temp < 5 ? 0 : 0.5m);
            rtn += Period / 45;
            return rtn;
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
        public static DataTable GetListWithQuestionCount(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTableDB("DBRYPX", startRowIndex, maximumRows, "dbo.Source",
                @"*,QuestionCount=isnull((
	                    SELECT cast(isnull([单选题],0) + isnull([多选题],0) + isnull([判断题],0)as varchar(10)) + '(单:' + cast([单选题] as varchar(10)) + ',多:' + cast([多选题] as varchar(10)) + ',判:' + cast([判断题] as varchar(10)) + ')' 
	                    FROM (
		                    SELECT [SourceID], [QuestionType]
		                    FROM [TrainQuestion]
	                    ) AS SourceTable
	                    PIVOT (
		                    count(QuestionType)
		                    FOR [QuestionType] IN ([单选题], [多选题],[判断题])
	                    ) AS PivotTable
	                    where [SourceID] = Source.[SourceID]
                    ),0)"
                , filterWhereString, orderBy == "" ? " [SourceYear] desc,[SortID]" : orderBy);
        } 

        /// <summary>
        /// 获取下一个课程或课件排序ID
        /// </summary>
        /// <param name="ParentSourceID">父课程ID，课程的父课程ID为0</param>
        /// <returns></returns>
        public static int GetNextSortID(long ParentSourceID)
        {
            string sql = @"
			SELECT isnull(max(SortID),0) + 10
			FROM source
			WHERE ParentSourceID=@ParentSourceID";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ParentSourceID", DbType.Int64, ParentSourceID));
            return Convert.ToInt32(db.ExecuteScalar(sql, p.ToArray()));
        }

        /// <summary>
        /// 更新课程总学时
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ParentSourceID">课程ID</param>
        /// <returns></returns>
        public static int UpdatePeriod(DbTransaction tran, long ParentSourceID)
        {
            string sql = @"			
            UPDATE source
            SET source.Period = (select isnull(sum(Period),0) from source where ParentSourceID=@ParentSourceID and Status='启用'),
                showperiod= (select isnull(sum(s.showperiod),0) from source s  where s.ParentSourceID=source.SourceID and s.Status='启用')
            WHERE  source.SourceID = @ParentSourceID";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();

            p.Add(db.CreateParameter("ParentSourceID", DbType.Int64, ParentSourceID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 更新课件总个数
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="ParentSourceID">课程ID</param>
        /// <returns></returns>
        public static int UpdateSourceWareCount(DbTransaction tran, long ParentSourceID)
        {
            string sql = @"			
            UPDATE source
            SET source.SourceWareCount = (select isnull(count(*),0) from source where ParentSourceID=@ParentSourceID and Status='启用')
            WHERE  source.SourceID = @ParentSourceID";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();

            p.Add(db.CreateParameter("ParentSourceID", DbType.Int64, ParentSourceID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 根据课程ID更新其包含课件的类型（选修、必修），专业，状态
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="ParentSourceID">课程ID</param>
        /// <param name="SourceType">课程类型（选修、必修）</param>
        /// <param name="SourceStatus">状态（启用、停用）</param>
        /// <returns></returns>
        public static int UpdateSubSourceType(DbTransaction tran, Int64 ParentSourceID, string SourceType,string SourceStatus)
        {
            string sql = @"
			UPDATE source
				SET	SourceType = @SourceType,Status=@Status
			WHERE
				ParentSourceID = @ParentSourceID";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("SourceType", DbType.String, SourceType));
            p.Add(db.CreateParameter("ParentSourceID", DbType.Int64, ParentSourceID));
            p.Add(db.CreateParameter("Status", DbType.String, SourceStatus));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 根据点播编号获取课件信息
        /// </summary>
        /// <param name="SourceWarePlayParam">点播编号</param>
        public static SourceMDL GetObjectBySourceWarePlayParam(string SourceWarePlayParam)
        {
            string sql = @"
			SELECT top 1 SourceID,SourceName,Teacher,WorkUnit,SourceType,Status,Description,ParentSourceID,SortID,Period,SourceWareCount,SourceWareUrl,SourceWarePlayParam,SourceYear,Lab,BarType,SourceImg,ShowPeriod
			FROM dbo.Source
			WHERE SourceWarePlayParam = @SourceWarePlayParam";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("SourceWarePlayParam", DbType.String, SourceWarePlayParam));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                SourceMDL _SourceMDL = null;
                if (reader.Read())
                {
                    _SourceMDL = new SourceMDL();
                    if (reader["SourceID"] != DBNull.Value) _SourceMDL.SourceID = Convert.ToInt64(reader["SourceID"]);
                    if (reader["SourceName"] != DBNull.Value) _SourceMDL.SourceName = Convert.ToString(reader["SourceName"]);
                    if (reader["Teacher"] != DBNull.Value) _SourceMDL.Teacher = Convert.ToString(reader["Teacher"]);
                    if (reader["WorkUnit"] != DBNull.Value) _SourceMDL.WorkUnit = Convert.ToString(reader["WorkUnit"]);
                    if (reader["SourceType"] != DBNull.Value) _SourceMDL.SourceType = Convert.ToString(reader["SourceType"]);
                    if (reader["Status"] != DBNull.Value) _SourceMDL.Status = Convert.ToString(reader["Status"]);
                    if (reader["Description"] != DBNull.Value) _SourceMDL.Description = Convert.ToString(reader["Description"]);
                    if (reader["ParentSourceID"] != DBNull.Value) _SourceMDL.ParentSourceID = Convert.ToInt64(reader["ParentSourceID"]);
                    if (reader["SortID"] != DBNull.Value) _SourceMDL.SortID = Convert.ToInt32(reader["SortID"]);
                    if (reader["Period"] != DBNull.Value) _SourceMDL.Period = Convert.ToInt32(reader["Period"]);
                    if (reader["SourceWareCount"] != DBNull.Value) _SourceMDL.SourceWareCount = Convert.ToInt32(reader["SourceWareCount"]);
                    if (reader["SourceWareUrl"] != DBNull.Value) _SourceMDL.SourceWareUrl = Convert.ToString(reader["SourceWareUrl"]);
                    if (reader["SourceWarePlayParam"] != DBNull.Value) _SourceMDL.SourceWarePlayParam = Convert.ToString(reader["SourceWarePlayParam"]);
                    if (reader["SourceYear"] != DBNull.Value) _SourceMDL.SourceYear = Convert.ToInt32(reader["SourceYear"]);
                    if (reader["Lab"] != DBNull.Value) _SourceMDL.Lab = Convert.ToString(reader["Lab"]);
                    if (reader["BarType"] != DBNull.Value) _SourceMDL.BarType = Convert.ToString(reader["BarType"]);
                    if (reader["SourceImg"] != DBNull.Value) _SourceMDL.SourceImg = Convert.ToString(reader["SourceImg"]);
                    if (reader["ShowPeriod"] != DBNull.Value) _SourceMDL.ShowPeriod = Convert.ToDecimal(reader["ShowPeriod"]);
                }
                reader.Close();
                db.Close();
                return _SourceMDL;
            }
        }
        #endregion
    }
}
