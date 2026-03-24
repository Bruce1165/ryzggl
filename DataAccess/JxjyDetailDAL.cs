using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--JxjyDetailDAL(填写类描述)
	/// </summary>
    public class JxjyDetailDAL
    {
        public JxjyDetailDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_JxjyDetailMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(JxjyDetailMDL _JxjyDetailMDL)
		{
		    return Insert(null,_JxjyDetailMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_JxjyDetailMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,JxjyDetailMDL _JxjyDetailMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.JxjyDetail(BaseID,DataNo,TrainDateStart,TrainDateEnd,TrainName,TrainWay,TrainUnit,Period,cjsj,ExamResult)
			VALUES (@BaseID,@DataNo,@TrainDateStart,@TrainDateEnd,@TrainName,@TrainWay,@TrainUnit,@Period,@cjsj,@ExamResult);SELECT @DetailID = @@IDENTITY";

		    List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateOutParameter("DetailID", DbType.Int64));
			p.Add(db.CreateParameter("BaseID",DbType.Int64, _JxjyDetailMDL.BaseID));
			p.Add(db.CreateParameter("DataNo",DbType.Int32, _JxjyDetailMDL.DataNo));
			p.Add(db.CreateParameter("TrainDateStart",DbType.DateTime, _JxjyDetailMDL.TrainDateStart));
			p.Add(db.CreateParameter("TrainDateEnd",DbType.DateTime, _JxjyDetailMDL.TrainDateEnd));
			p.Add(db.CreateParameter("TrainName",DbType.String, _JxjyDetailMDL.TrainName));
			p.Add(db.CreateParameter("TrainWay",DbType.String, _JxjyDetailMDL.TrainWay));
			p.Add(db.CreateParameter("TrainUnit",DbType.String, _JxjyDetailMDL.TrainUnit));
			p.Add(db.CreateParameter("Period",DbType.Int32, _JxjyDetailMDL.Period));
			p.Add(db.CreateParameter("cjsj",DbType.DateTime, _JxjyDetailMDL.cjsj));
            p.Add(db.CreateParameter("ExamResult", DbType.String, _JxjyDetailMDL.ExamResult));
            int rtn = db.GetExcuteNonQuery(tran, sql, p.ToArray());           
            _JxjyDetailMDL.DetailID = Convert.ToInt64(p[0].Value);
            return rtn;
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_JxjyDetailMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(JxjyDetailMDL _JxjyDetailMDL)
		{
			return Update(null,_JxjyDetailMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_JxjyDetailMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,JxjyDetailMDL _JxjyDetailMDL)
		{
			string sql = @"
			UPDATE dbo.JxjyDetail
				SET	BaseID = @BaseID,DataNo = @DataNo,TrainDateStart = @TrainDateStart,TrainDateEnd = @TrainDateEnd,TrainName = @TrainName,TrainWay = @TrainWay,TrainUnit = @TrainUnit,Period = @Period,cjsj = @cjsj,ExamResult = @ExamResult
			WHERE
				DetailID = @DetailID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("DetailID",DbType.Int64, _JxjyDetailMDL.DetailID));
			p.Add(db.CreateParameter("BaseID",DbType.Int64, _JxjyDetailMDL.BaseID));
			p.Add(db.CreateParameter("DataNo",DbType.Int32, _JxjyDetailMDL.DataNo));
			p.Add(db.CreateParameter("TrainDateStart",DbType.DateTime, _JxjyDetailMDL.TrainDateStart));
			p.Add(db.CreateParameter("TrainDateEnd",DbType.DateTime, _JxjyDetailMDL.TrainDateEnd));
			p.Add(db.CreateParameter("TrainName",DbType.String, _JxjyDetailMDL.TrainName));
			p.Add(db.CreateParameter("TrainWay",DbType.String, _JxjyDetailMDL.TrainWay));
			p.Add(db.CreateParameter("TrainUnit",DbType.String, _JxjyDetailMDL.TrainUnit));
			p.Add(db.CreateParameter("Period",DbType.Int32, _JxjyDetailMDL.Period));
			p.Add(db.CreateParameter("cjsj",DbType.DateTime, _JxjyDetailMDL.cjsj));
            p.Add(db.CreateParameter("ExamResult", DbType.String, _JxjyDetailMDL.ExamResult));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="JxjyDetailID">主键</param>
		/// <returns></returns>
        public static int Delete( long DetailID )
		{
			return Delete(null, DetailID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="JxjyDetailID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, long DetailID)
		{
			string sql=@"DELETE FROM dbo.JxjyDetail WHERE DetailID = @DetailID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("DetailID",DbType.Int64,DetailID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="_JxjyDetailMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(JxjyDetailMDL _JxjyDetailMDL)
		{
			return Delete(null,_JxjyDetailMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_JxjyDetailMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,JxjyDetailMDL _JxjyDetailMDL)
		{
			string sql=@"DELETE FROM dbo.JxjyDetail WHERE DetailID = @DetailID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("DetailID",DbType.Int64,_JxjyDetailMDL.DetailID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="JxjyDetailID">主键</param>
        public static JxjyDetailMDL GetObject( long DetailID )
		{
			string sql= @"
			SELECT DetailID,BaseID,DataNo,TrainDateStart,TrainDateEnd,TrainName,TrainWay,TrainUnit,Period,cjsj,ExamResult
			FROM dbo.JxjyDetail
			WHERE DetailID = @DetailID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("DetailID",DbType.Int64,DetailID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                JxjyDetailMDL _JxjyDetailMDL = null;
                if (reader.Read())
                {
                    _JxjyDetailMDL = new JxjyDetailMDL();
					if (reader["DetailID"] != DBNull.Value) _JxjyDetailMDL.DetailID = Convert.ToInt64(reader["DetailID"]);
					if (reader["BaseID"] != DBNull.Value) _JxjyDetailMDL.BaseID = Convert.ToInt64(reader["BaseID"]);
					if (reader["DataNo"] != DBNull.Value) _JxjyDetailMDL.DataNo = Convert.ToInt32(reader["DataNo"]);
					if (reader["TrainDateStart"] != DBNull.Value) _JxjyDetailMDL.TrainDateStart = Convert.ToDateTime(reader["TrainDateStart"]);
					if (reader["TrainDateEnd"] != DBNull.Value) _JxjyDetailMDL.TrainDateEnd = Convert.ToDateTime(reader["TrainDateEnd"]);
					if (reader["TrainName"] != DBNull.Value) _JxjyDetailMDL.TrainName = Convert.ToString(reader["TrainName"]);
					if (reader["TrainWay"] != DBNull.Value) _JxjyDetailMDL.TrainWay = Convert.ToString(reader["TrainWay"]);
					if (reader["TrainUnit"] != DBNull.Value) _JxjyDetailMDL.TrainUnit = Convert.ToString(reader["TrainUnit"]);
					if (reader["Period"] != DBNull.Value) _JxjyDetailMDL.Period = Convert.ToInt32(reader["Period"]);
					if (reader["cjsj"] != DBNull.Value) _JxjyDetailMDL.cjsj = Convert.ToDateTime(reader["cjsj"]);
                    if (reader["ExamResult"] != DBNull.Value) _JxjyDetailMDL.ExamResult = Convert.ToString(reader["ExamResult"]);
                }
				reader.Close();
                db.Close();
                return _JxjyDetailMDL;
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
		public static DataTable GetList(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        {
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.JxjyDetail", "*", filterWhereString, orderBy == "" ? " DetailID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.JxjyDetail", filterWhereString);
        }
        
        #region 自定义方法

        /// <summary>
        /// 获取业务审定单绑定的委培继续教育记录
        /// </summary>
        /// <param name="PostTypeID">岗位类别ID</param>
        /// <param name="ApplyID">申请单ID</param>
        /// <returns>委培继续教育记录集合</returns>
        public static DataTable GetList(int PostTypeID, string ApplyID)
        {
            string sql = string.Format("SELECT *  FROM [dbo].[JxjyDetail]  where [BaseID] in(select [BaseID] from [dbo].[JxjyBase] where [ApplyID] ='{0}' and PostTypeID = {1}) order by [DataNo] ", ApplyID, PostTypeID);
            return CommonDAL.GetDataTable(sql);
        }

        /// <summary>
        /// 获取业务审定单绑定的按年度统计委培继续教育记录总学分数
        /// </summary>
        /// <param name="PostTypeID">岗位类别ID</param>
        /// <param name="ApplyID">申请单ID</param>
        /// <returns>统计集合：TrainYear，period</returns>
        public static DataTable GetSumPeriodGroupByYear(int PostTypeID, string ApplyID)
        {
            string sql = string.Format("SELECT year(TrainDateStart) TrainYear, isnull(sum(period),0) period  FROM [dbo].[JxjyDetail]  where [BaseID] in(select [BaseID] from [dbo].[JxjyBase] where [ApplyID] ='{0}' and PostTypeID = {1}) group by year(TrainDateStart) ", ApplyID, PostTypeID);
            DataTable dt = CommonDAL.GetDataTable(sql);

            DataColumn[] c = new DataColumn[1];
            c[0] = dt.Columns["TrainYear"];
            dt.PrimaryKey = c;
            return dt;
        }

        /// <summary>
        /// 获取业务审定单绑定的委培继续教育记录总学分数
        /// </summary>
        /// <param name="PostTypeID">岗位类别ID</param>
        /// <param name="ApplyID">申请单ID</param>
        /// <returns>学分总数</returns>
        public static int GetSumPeriod(int PostTypeID, string ApplyID)
        {
            string sql = string.Format("SELECT isnull(sum(period),0)  FROM [dbo].[JxjyDetail]  where [BaseID] in(select [BaseID] from [dbo].[JxjyBase] where [ApplyID] ='{0}' and PostTypeID = {1}) ", ApplyID, PostTypeID);

            int rtn= (new DBHelper()).ExecuteScalar<int>(sql);
            return rtn;
        }

        /// <summary>
        /// 检查继续教育选修课是否满足每年学分要求
        /// </summary>
        /// <param name="YearSpan">检查近几年的选修课</param>
        /// <param name="Period">每年要求的选修课学时</param>
        /// <param name="checkTime">业务检查时点（申请时点）</param>
        /// <param name="ApplyID">业务申请ID</param>
        /// <param name="PostTypeID">岗位类型ID：二建=0，二级造价工程=-1，三类人=1，特种作业=2</param>
        /// <returns>符合返回true，否则返回false</returns>
        public static bool CheckXuanXiuPeriod(int YearSpan, int Period, DateTime checkTime, string ApplyID, int PostTypeID)
        {
            int finishYear = 0;//满足年数
            int startYear = checkTime.AddYears(-YearSpan).Year;//学习开始年度

            DataTable dt = JxjyDetailDAL.GetSumPeriodGroupByYear(PostTypeID, ApplyID);//继续教育委培记录

            foreach (DataRow r in dt.Rows)
            {
                if (Convert.ToInt32(r["TrainYear"]) >= startYear && Convert.ToInt32(r["period"]) >= Period)
                {
                    finishYear++;
                    if (finishYear >= YearSpan)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 检查继续教育选修课是否满足近n年总学分要求，不限制每年学分
        /// </summary>
        /// <param name="YearSpan">检查近几年的选修课</param>
        /// <param name="Period">每年要求的选修课学时</param>
        /// <param name="checkTime">业务检查时点（申请时点）</param>
        /// <param name="ApplyID">业务申请ID</param>
        /// <param name="PostTypeID">岗位类型ID：二建=0，二级造价工程=-1，三类人=1，特种作业=2</param>
        /// <returns>符合返回true，否则返回false</returns>
        public static bool CheckXuanXiuSumPeriod(int YearSpan, int Period, DateTime checkTime, string ApplyID, int PostTypeID)
        {
            int SumPeriod = 0;//总学分
            int startYear = checkTime.AddYears(-YearSpan).Year;//学习开始年度

            DataTable dt = JxjyDetailDAL.GetSumPeriodGroupByYear(PostTypeID, ApplyID);//继续教育委培记录

            foreach (DataRow r in dt.Rows)
            {
                if (Convert.ToInt32(r["TrainYear"]) >= startYear )
                {
                    SumPeriod += Convert.ToInt32(r["period"]);
                }
            }

            if (SumPeriod >= (YearSpan * Period))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
