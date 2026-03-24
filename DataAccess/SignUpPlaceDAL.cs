using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--SignUpPlaceDAL(填写类描述)
	/// </summary>
    public class SignUpPlaceDAL
    {
        public SignUpPlaceDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="SignUpPlaceOB">对象实体类</param>
		/// <returns></returns>
        public static long Insert(SignUpPlaceOB _SignUpPlaceOB)
		{
		    return Insert(null,_SignUpPlaceOB);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="SignUpPlaceOB">对象实体类</param>
		/// <returns></returns>
        public static long Insert(DbTransaction tran,SignUpPlaceOB _SignUpPlaceOB)
		{
			DBHelper db = new DBHelper();
		
			string sql = @"
			INSERT INTO dbo.SignUpPlace(PlaceName,Address,Phone,CheckPersonLimit)
			VALUES (@PlaceName,@Address,@Phone,@CheckPersonLimit);SELECT @SignUpPlaceID = @@IDENTITY";

		    List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateOutParameter("SignUpPlaceID", DbType.Int64));
			p.Add(db.CreateParameter("PlaceName",DbType.String, _SignUpPlaceOB.PlaceName));
			p.Add(db.CreateParameter("Address",DbType.String, _SignUpPlaceOB.Address));
			p.Add(db.CreateParameter("Phone",DbType.String, _SignUpPlaceOB.Phone));
			p.Add(db.CreateParameter("CheckPersonLimit",DbType.Int32, _SignUpPlaceOB.CheckPersonLimit));

            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            _SignUpPlaceOB.SignUpPlaceID = Convert.ToInt64(p[0].Value);
            return rtn;
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="SignUpPlaceOB">对象实体类</param>
		/// <returns></returns>
        public static int Update(SignUpPlaceOB _SignUpPlaceOB)
		{
			return Update(null,_SignUpPlaceOB);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="SignUpPlaceOB">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,SignUpPlaceOB _SignUpPlaceOB)
		{
			string sql = @"
			UPDATE dbo.SignUpPlace
				SET	PlaceName =@PlaceName,Address =@Address,Phone =@Phone,CheckPersonLimit =@CheckPersonLimit
			WHERE
				SignUpPlaceID =@SignUpPlaceID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("SignUpPlaceID",DbType.Int64, _SignUpPlaceOB.SignUpPlaceID));
			p.Add(db.CreateParameter("PlaceName",DbType.String, _SignUpPlaceOB.PlaceName));
			p.Add(db.CreateParameter("Address",DbType.String, _SignUpPlaceOB.Address));
			p.Add(db.CreateParameter("Phone",DbType.String, _SignUpPlaceOB.Phone));
			p.Add(db.CreateParameter("CheckPersonLimit",DbType.Int32, _SignUpPlaceOB.CheckPersonLimit));
			return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="SignUpPlaceID">主键</param>
		/// <returns></returns>
        public static int Delete( long SignUpPlaceID )
		{
			return Delete(null, SignUpPlaceID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="SignUpPlaceID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, long SignUpPlaceID)
		{
			string sql=@"DELETE FROM dbo.SignUpPlace WHERE SignUpPlaceID =@SignUpPlaceID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("SignUpPlaceID",DbType.Int64,SignUpPlaceID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="SignUpPlaceOB">对象实体类</param>
		/// <returns></returns>
        public static int Delete(SignUpPlaceOB _SignUpPlaceOB)
		{
			return Delete(null,_SignUpPlaceOB);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="SignUpPlaceOB">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,SignUpPlaceOB _SignUpPlaceOB)
		{
			string sql=@"DELETE FROM dbo.SignUpPlace WHERE SignUpPlaceID =@SignUpPlaceID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("SignUpPlaceID",DbType.Int64,_SignUpPlaceOB.SignUpPlaceID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="SignUpPlaceID">主键</param>
        public static SignUpPlaceOB GetObject( long SignUpPlaceID )
		{
			string sql=@"
			SELECT SignUpPlaceID,PlaceName,Address,Phone,CheckPersonLimit
			FROM dbo.SignUpPlace
			WHERE SignUpPlaceID =@SignUpPlaceID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("SignUpPlaceID", DbType.Int64, SignUpPlaceID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                SignUpPlaceOB _SignUpPlaceOB = null;
                if (reader.Read())
                {
                    _SignUpPlaceOB = new SignUpPlaceOB();
					if (reader["SignUpPlaceID"] != DBNull.Value) _SignUpPlaceOB.SignUpPlaceID = Convert.ToInt64(reader["SignUpPlaceID"]);
					if (reader["PlaceName"] != DBNull.Value) _SignUpPlaceOB.PlaceName = Convert.ToString(reader["PlaceName"]);
					if (reader["Address"] != DBNull.Value) _SignUpPlaceOB.Address = Convert.ToString(reader["Address"]);
					if (reader["Phone"] != DBNull.Value) _SignUpPlaceOB.Phone = Convert.ToString(reader["Phone"]);
					if (reader["CheckPersonLimit"] != DBNull.Value) _SignUpPlaceOB.CheckPersonLimit = Convert.ToInt32(reader["CheckPersonLimit"]);
                }
				reader.Close();
                db.Close();
                return _SignUpPlaceOB;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.SignUpPlace", "*,0 CHECKED", filterWhereString, orderBy == "" ? " SignUpPlaceID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.SignUpPlace", filterWhereString);
        }
    }
}
