using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--ExamPlanForUserDAL(填写类描述)
	/// </summary>
    public class ExamPlanForUserDAL
    {
        public ExamPlanForUserDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="ExamPlanForUserOB">对象实体类</param>
		/// <returns></returns>
        public static long Insert(ExamPlanForUserOB _ExamPlanForUserOB)
		{
		    return Insert(null,_ExamPlanForUserOB);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ExamPlanForUserOB">对象实体类</param>
		/// <returns></returns>
        public static long Insert(DbTransaction tran,ExamPlanForUserOB _ExamPlanForUserOB)
		{
			DBHelper db = new DBHelper();
		
			string sql = @"
			INSERT INTO dbo.ExamPlanForUser(ExamPlanID,UserType,CertificateCode,UnitCode,TrainUnitID)
			VALUES (@ExamPlanID,@UserType,@CertificateCode,@UnitCode,@TrainUnitID);SELECT @DataID = @@IDENTITY";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateOutParameter("DataID",DbType.Int64));
			p.Add(db.CreateParameter("ExamPlanID",DbType.Int64, _ExamPlanForUserOB.ExamPlanID));
			p.Add(db.CreateParameter("UserType",DbType.String, _ExamPlanForUserOB.UserType));
			p.Add(db.CreateParameter("CertificateCode",DbType.String, _ExamPlanForUserOB.CertificateCode));
			p.Add(db.CreateParameter("UnitCode",DbType.String, _ExamPlanForUserOB.UnitCode));
			p.Add(db.CreateParameter("TrainUnitID",DbType.Int64, _ExamPlanForUserOB.TrainUnitID));
            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            _ExamPlanForUserOB.DataID = Convert.ToInt64(p[0].Value);
            return rtn;
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="ExamPlanForUserOB">对象实体类</param>
		/// <returns></returns>
        public static int Update(ExamPlanForUserOB _ExamPlanForUserOB)
		{
			return Update(null,_ExamPlanForUserOB);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ExamPlanForUserOB">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,ExamPlanForUserOB _ExamPlanForUserOB)
		{
			string sql = @"
			UPDATE dbo.ExamPlanForUser
				SET	ExamPlanID = @ExamPlanID,UserType = @UserType,CertificateCode = @CertificateCode,UnitCode = @UnitCode,TrainUnitID = @TrainUnitID
			WHERE
				DataID = @DataID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("DataID",DbType.Int64, _ExamPlanForUserOB.DataID));
			p.Add(db.CreateParameter("ExamPlanID",DbType.Int64, _ExamPlanForUserOB.ExamPlanID));
			p.Add(db.CreateParameter("UserType",DbType.String, _ExamPlanForUserOB.UserType));
			p.Add(db.CreateParameter("CertificateCode",DbType.String, _ExamPlanForUserOB.CertificateCode));
			p.Add(db.CreateParameter("UnitCode",DbType.String, _ExamPlanForUserOB.UnitCode));
			p.Add(db.CreateParameter("TrainUnitID",DbType.Int64, _ExamPlanForUserOB.TrainUnitID));
			return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="ExamPlanID">考试计划ID</param>
		/// <returns></returns>
        public static int Delete( long ExamPlanID )
		{
            return Delete(null, ExamPlanID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ExamPlanID">考试计划ID</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, long ExamPlanID)
		{
            string sql = @"DELETE FROM dbo.ExamPlanForUser WHERE ExamPlanID = @ExamPlanID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ExamPlanID", DbType.Int64, ExamPlanID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}

		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="ExamPlanForUserOB">对象实体类</param>
		/// <returns></returns>
        public static int Delete(ExamPlanForUserOB _ExamPlanForUserOB)
		{
			return Delete(null,_ExamPlanForUserOB);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="ExamPlanForUserOB">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,ExamPlanForUserOB _ExamPlanForUserOB)
		{
			string sql=@"DELETE FROM dbo.ExamPlanForUser WHERE DataID = @DataID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("DataID",DbType.Int64,_ExamPlanForUserOB.DataID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="ExamPlanForUserID">主键</param>
        public static ExamPlanForUserOB GetObject( long DataID )
		{
			string sql=@"
			SELECT DataID,ExamPlanID,UserType,CertificateCode,UnitCode,TrainUnitID
			FROM dbo.ExamPlanForUser
			WHERE DataID = @DataID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("DataID", DbType.Int64, DataID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                ExamPlanForUserOB _ExamPlanForUserOB = null;
                if (reader.Read())
                {
                    _ExamPlanForUserOB = new ExamPlanForUserOB();
					if (reader["DataID"] != DBNull.Value) _ExamPlanForUserOB.DataID = Convert.ToInt64(reader["DataID"]);
					if (reader["ExamPlanID"] != DBNull.Value) _ExamPlanForUserOB.ExamPlanID = Convert.ToInt64(reader["ExamPlanID"]);
					if (reader["UserType"] != DBNull.Value) _ExamPlanForUserOB.UserType = Convert.ToString(reader["UserType"]);
					if (reader["CertificateCode"] != DBNull.Value) _ExamPlanForUserOB.CertificateCode = Convert.ToString(reader["CertificateCode"]);
					if (reader["UnitCode"] != DBNull.Value) _ExamPlanForUserOB.UnitCode = Convert.ToString(reader["UnitCode"]);
					if (reader["TrainUnitID"] != DBNull.Value) _ExamPlanForUserOB.TrainUnitID = Convert.ToInt64(reader["TrainUnitID"]);
                }
				reader.Close();
                db.Close();
                return _ExamPlanForUserOB;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.ExamPlanForUser", "*", filterWhereString, orderBy == "" ? " DataID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.ExamPlanForUser", filterWhereString);
        }
    }
}
