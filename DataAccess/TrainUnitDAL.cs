using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--TrainUnitDAL(填写类描述)
	/// </summary>
    public class TrainUnitDAL
    {
        public TrainUnitDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_TrainUnitMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(TrainUnitMDL _TrainUnitMDL)
		{
		    return Insert(null,_TrainUnitMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_TrainUnitMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,TrainUnitMDL _TrainUnitMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.TrainUnit(UnitNo,TrainUnitName,UnitCode,PostSet,UseStatus)
			VALUES (@UnitNo,@TrainUnitName,@UnitCode,@PostSet,@UseStatus)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("UnitNo",DbType.String, _TrainUnitMDL.UnitNo));
			p.Add(db.CreateParameter("TrainUnitName",DbType.String, _TrainUnitMDL.TrainUnitName));
			p.Add(db.CreateParameter("UnitCode",DbType.String, _TrainUnitMDL.UnitCode));
			p.Add(db.CreateParameter("PostSet",DbType.String, _TrainUnitMDL.PostSet));
			p.Add(db.CreateParameter("UseStatus",DbType.Int32, _TrainUnitMDL.UseStatus));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_TrainUnitMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(TrainUnitMDL _TrainUnitMDL)
		{
			return Update(null,_TrainUnitMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_TrainUnitMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,TrainUnitMDL _TrainUnitMDL)
		{
			string sql = @"
			UPDATE dbo.TrainUnit
				SET	TrainUnitName = @TrainUnitName,UnitCode = @UnitCode,PostSet = @PostSet,UseStatus = @UseStatus
			WHERE
				UnitNo = @UnitNo";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("UnitNo",DbType.String, _TrainUnitMDL.UnitNo));
			p.Add(db.CreateParameter("TrainUnitName",DbType.String, _TrainUnitMDL.TrainUnitName));
			p.Add(db.CreateParameter("UnitCode",DbType.String, _TrainUnitMDL.UnitCode));
			p.Add(db.CreateParameter("PostSet",DbType.String, _TrainUnitMDL.PostSet));
			p.Add(db.CreateParameter("UseStatus",DbType.Int32, _TrainUnitMDL.UseStatus));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="TrainUnitID">主键</param>
		/// <returns></returns>
        public static int Delete( string UnitNo )
		{
			return Delete(null, UnitNo);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="TrainUnitID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, string UnitNo)
		{
			string sql=@"DELETE FROM dbo.TrainUnit WHERE UnitNo = @UnitNo";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("UnitNo",DbType.String,UnitNo));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="_TrainUnitMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(TrainUnitMDL _TrainUnitMDL)
		{
			return Delete(null,_TrainUnitMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_TrainUnitMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,TrainUnitMDL _TrainUnitMDL)
		{
			string sql=@"DELETE FROM dbo.TrainUnit WHERE UnitNo = @UnitNo";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("UnitNo",DbType.String,_TrainUnitMDL.UnitNo));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="TrainUnitID">主键</param>
        public static TrainUnitMDL GetObject( string UnitNo )
		{
			string sql=@"
			SELECT UnitNo,TrainUnitName,UnitCode,PostSet,UseStatus
			FROM dbo.TrainUnit
			WHERE UnitNo = @UnitNo";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("UnitNo",DbType.String,UnitNo));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                TrainUnitMDL _TrainUnitMDL = null;
                if (reader.Read())
                {
                    _TrainUnitMDL = new TrainUnitMDL();
					if (reader["UnitNo"] != DBNull.Value) _TrainUnitMDL.UnitNo = Convert.ToString(reader["UnitNo"]);
					if (reader["TrainUnitName"] != DBNull.Value) _TrainUnitMDL.TrainUnitName = Convert.ToString(reader["TrainUnitName"]);
					if (reader["UnitCode"] != DBNull.Value) _TrainUnitMDL.UnitCode = Convert.ToString(reader["UnitCode"]);
					if (reader["PostSet"] != DBNull.Value) _TrainUnitMDL.PostSet = Convert.ToString(reader["PostSet"]);
					if (reader["UseStatus"] != DBNull.Value) _TrainUnitMDL.UseStatus = Convert.ToInt32(reader["UseStatus"]);
                }
				reader.Close();
                db.Close();
                return _TrainUnitMDL;
            }
		}

        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="SHTYXYDM">社会统一信用代码</param>
        public static TrainUnitMDL GetObjectBySHTYXYDM(string SHTYXYDM)
        {
            string sql = @"
			SELECT UnitNo,TrainUnitName,UnitCode,PostSet,UseStatus
			FROM dbo.TrainUnit
			WHERE UnitCode = @UnitCode";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("UnitCode", DbType.String, SHTYXYDM));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                TrainUnitMDL _TrainUnitMDL = null;
                if (reader.Read())
                {
                    _TrainUnitMDL = new TrainUnitMDL();
                    if (reader["UnitNo"] != DBNull.Value) _TrainUnitMDL.UnitNo = Convert.ToString(reader["UnitNo"]);
                    if (reader["TrainUnitName"] != DBNull.Value) _TrainUnitMDL.TrainUnitName = Convert.ToString(reader["TrainUnitName"]);
                    if (reader["UnitCode"] != DBNull.Value) _TrainUnitMDL.UnitCode = Convert.ToString(reader["UnitCode"]);
                    if (reader["PostSet"] != DBNull.Value) _TrainUnitMDL.PostSet = Convert.ToString(reader["PostSet"]);
                    if (reader["UseStatus"] != DBNull.Value) _TrainUnitMDL.UseStatus = Convert.ToInt32(reader["UseStatus"]);
                }
                reader.Close();
                db.Close();
                return _TrainUnitMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.TrainUnit", "*", filterWhereString, orderBy == "" ? " UnitNo" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.TrainUnit", filterWhereString);
        }
        
        #region 自定义方法
        
        #endregion
    }
}
