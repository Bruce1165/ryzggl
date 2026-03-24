using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--考前培训内容
	/// </summary>
    public class ExamSignupTrainDAL
    {
        public ExamSignupTrainDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_ExamSignupTrainMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(ExamSignupTrainMDL _ExamSignupTrainMDL)
		{
		    return Insert(null,_ExamSignupTrainMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_ExamSignupTrainMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,ExamSignupTrainMDL _ExamSignupTrainMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.ExamSignupTrain(ExamSignUpID,DataNo,TrainDateStart,TrainDateEnd,TrainType,TrainName,TrainWay,Period,cjsj)
			VALUES (@ExamSignUpID,@DataNo,@TrainDateStart,@TrainDateEnd,@TrainType,@TrainName,@TrainWay,@Period,@cjsj);SELECT @DetailID = @@IDENTITY";

		    List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateOutParameter("DetailID", DbType.Int64));
			p.Add(db.CreateParameter("ExamSignUpID",DbType.Int64, _ExamSignupTrainMDL.ExamSignUpID));
			p.Add(db.CreateParameter("DataNo",DbType.Int32, _ExamSignupTrainMDL.DataNo));
			p.Add(db.CreateParameter("TrainDateStart",DbType.DateTime, _ExamSignupTrainMDL.TrainDateStart));
			p.Add(db.CreateParameter("TrainDateEnd",DbType.DateTime, _ExamSignupTrainMDL.TrainDateEnd));
			p.Add(db.CreateParameter("TrainType",DbType.String, _ExamSignupTrainMDL.TrainType));
			p.Add(db.CreateParameter("TrainName",DbType.String, _ExamSignupTrainMDL.TrainName));
			p.Add(db.CreateParameter("TrainWay",DbType.String, _ExamSignupTrainMDL.TrainWay));
			p.Add(db.CreateParameter("Period",DbType.Int32, _ExamSignupTrainMDL.Period));
			p.Add(db.CreateParameter("cjsj",DbType.DateTime, _ExamSignupTrainMDL.cjsj));
            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            _ExamSignupTrainMDL.DetailID = Convert.ToInt64(p[0].Value);
            return rtn;
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_ExamSignupTrainMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(ExamSignupTrainMDL _ExamSignupTrainMDL)
		{
			return Update(null,_ExamSignupTrainMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_ExamSignupTrainMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,ExamSignupTrainMDL _ExamSignupTrainMDL)
		{
			string sql = @"
			UPDATE dbo.ExamSignupTrain
				SET	ExamSignUpID = @ExamSignUpID,DataNo = @DataNo,TrainDateStart = @TrainDateStart,TrainDateEnd = @TrainDateEnd,TrainType = @TrainType,TrainName = @TrainName,TrainWay = @TrainWay,Period = @Period,cjsj = @cjsj
			WHERE
				DetailID = @DetailID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("DetailID",DbType.Int64, _ExamSignupTrainMDL.DetailID));
			p.Add(db.CreateParameter("ExamSignUpID",DbType.Int64, _ExamSignupTrainMDL.ExamSignUpID));
			p.Add(db.CreateParameter("DataNo",DbType.Int32, _ExamSignupTrainMDL.DataNo));
			p.Add(db.CreateParameter("TrainDateStart",DbType.DateTime, _ExamSignupTrainMDL.TrainDateStart));
			p.Add(db.CreateParameter("TrainDateEnd",DbType.DateTime, _ExamSignupTrainMDL.TrainDateEnd));
			p.Add(db.CreateParameter("TrainType",DbType.String, _ExamSignupTrainMDL.TrainType));
			p.Add(db.CreateParameter("TrainName",DbType.String, _ExamSignupTrainMDL.TrainName));
			p.Add(db.CreateParameter("TrainWay",DbType.String, _ExamSignupTrainMDL.TrainWay));
			p.Add(db.CreateParameter("Period",DbType.Int32, _ExamSignupTrainMDL.Period));
			p.Add(db.CreateParameter("cjsj",DbType.DateTime, _ExamSignupTrainMDL.cjsj));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="ExamSignupTrainID">主键</param>
		/// <returns></returns>
        public static int Delete( long DetailID )
		{
			return Delete(null, DetailID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="ExamSignupTrainID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, long DetailID)
		{
			string sql=@"DELETE FROM dbo.ExamSignupTrain WHERE DetailID = @DetailID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("DetailID",DbType.Int64,DetailID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="_ExamSignupTrainMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(ExamSignupTrainMDL _ExamSignupTrainMDL)
		{
			return Delete(null,_ExamSignupTrainMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_ExamSignupTrainMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,ExamSignupTrainMDL _ExamSignupTrainMDL)
		{
			string sql=@"DELETE FROM dbo.ExamSignupTrain WHERE DetailID = @DetailID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("DetailID",DbType.Int64,_ExamSignupTrainMDL.DetailID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ExamSignupTrainID">主键</param>
        public static ExamSignupTrainMDL GetObject( long DetailID )
		{
			string sql=@"
			SELECT DetailID,ExamSignUpID,DataNo,TrainDateStart,TrainDateEnd,TrainType,TrainName,TrainWay,Period,cjsj
			FROM dbo.ExamSignupTrain
			WHERE DetailID = @DetailID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("DetailID",DbType.Int64,DetailID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                ExamSignupTrainMDL _ExamSignupTrainMDL = null;
                if (reader.Read())
                {
                    _ExamSignupTrainMDL = new ExamSignupTrainMDL();
					if (reader["DetailID"] != DBNull.Value) _ExamSignupTrainMDL.DetailID = Convert.ToInt64(reader["DetailID"]);
					if (reader["ExamSignUpID"] != DBNull.Value) _ExamSignupTrainMDL.ExamSignUpID = Convert.ToInt64(reader["ExamSignUpID"]);
					if (reader["DataNo"] != DBNull.Value) _ExamSignupTrainMDL.DataNo = Convert.ToInt32(reader["DataNo"]);
					if (reader["TrainDateStart"] != DBNull.Value) _ExamSignupTrainMDL.TrainDateStart = Convert.ToDateTime(reader["TrainDateStart"]);
					if (reader["TrainDateEnd"] != DBNull.Value) _ExamSignupTrainMDL.TrainDateEnd = Convert.ToDateTime(reader["TrainDateEnd"]);
					if (reader["TrainType"] != DBNull.Value) _ExamSignupTrainMDL.TrainType = Convert.ToString(reader["TrainType"]);
					if (reader["TrainName"] != DBNull.Value) _ExamSignupTrainMDL.TrainName = Convert.ToString(reader["TrainName"]);
					if (reader["TrainWay"] != DBNull.Value) _ExamSignupTrainMDL.TrainWay = Convert.ToString(reader["TrainWay"]);
					if (reader["Period"] != DBNull.Value) _ExamSignupTrainMDL.Period = Convert.ToInt32(reader["Period"]);
					if (reader["cjsj"] != DBNull.Value) _ExamSignupTrainMDL.cjsj = Convert.ToDateTime(reader["cjsj"]);
                }
				reader.Close();
                db.Close();
                return _ExamSignupTrainMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.ExamSignupTrain", "*", filterWhereString, orderBy == "" ? " DetailID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.ExamSignupTrain", filterWhereString);
        }
        
        #region 自定义方法
        
        #endregion
    }
}
