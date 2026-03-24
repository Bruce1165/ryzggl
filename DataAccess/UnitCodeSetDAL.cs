using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--UnitCodeSetDAL(填写类描述)
	/// </summary>
    public class UnitCodeSetDAL
    {
        public UnitCodeSetDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="UnitCodeSetOB">对象实体类</param>
		/// <returns></returns>
        public static int Insert(UnitCodeSetOB _UnitCodeSetOB)
		{
		    return Insert(null,_UnitCodeSetOB);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="UnitCodeSetOB">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,UnitCodeSetOB _UnitCodeSetOB)
		{
			DBHelper db = new DBHelper();
		
			string sql = @"
			INSERT INTO dbo.UnitCodeSet(UnitCode,UnitName,CreatePerson,CreateTime,ModifyPerson,ModifyTime)
			VALUES (:UnitCode,:UnitName,:CreatePerson,:CreateTime,:ModifyPerson,:ModifyTime)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("UnitCode",DbType.String, _UnitCodeSetOB.UnitCode));
			p.Add(db.CreateParameter("UnitName",DbType.String, _UnitCodeSetOB.UnitName));
			p.Add(db.CreateParameter("CreatePerson",DbType.String, _UnitCodeSetOB.CreatePerson));
			p.Add(db.CreateParameter("CreateTime",DbType.DateTime, _UnitCodeSetOB.CreateTime));
			p.Add(db.CreateParameter("ModifyPerson",DbType.String, _UnitCodeSetOB.ModifyPerson));
			p.Add(db.CreateParameter("ModifyTime",DbType.DateTime, _UnitCodeSetOB.ModifyTime));
			return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="UnitCodeSetOB">对象实体类</param>
		/// <returns></returns>
        public static int Update(UnitCodeSetOB _UnitCodeSetOB, string newUnitCode)
		{
            return Update(null, _UnitCodeSetOB, newUnitCode);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="UnitCodeSetOB">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,UnitCodeSetOB _UnitCodeSetOB,string newUnitCode)
		{
			string sql = @"
			UPDATE dbo.UnitCodeSet
				SET	UnitCode =@NewUnitCode,UnitName =@UnitName,CreatePerson =@CreatePerson,CreateTime =@CreateTime,ModifyPerson =@ModifyPerson,ModifyTime =@ModifyTime
			WHERE
				UnitCode =@UnitCode";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("UnitCode",DbType.String, _UnitCodeSetOB.UnitCode));
            p.Add(db.CreateParameter("NewUnitCode", DbType.String, newUnitCode));
			p.Add(db.CreateParameter("UnitName",DbType.String, _UnitCodeSetOB.UnitName));
			p.Add(db.CreateParameter("CreatePerson",DbType.String, _UnitCodeSetOB.CreatePerson));
			p.Add(db.CreateParameter("CreateTime",DbType.DateTime, _UnitCodeSetOB.CreateTime));
			p.Add(db.CreateParameter("ModifyPerson",DbType.String, _UnitCodeSetOB.ModifyPerson));
			p.Add(db.CreateParameter("ModifyTime",DbType.DateTime, _UnitCodeSetOB.ModifyTime));
			return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="UnitCodeSetID">主键</param>
		/// <returns></returns>
        public static int Delete( string UnitCode )
		{
			return Delete(null, UnitCode);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="UnitCodeSetID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, string UnitCode)
		{
			string sql=@"DELETE FROM dbo.UnitCodeSet WHERE UnitCode =@UnitCode";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("UnitCode",DbType.String,UnitCode));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="UnitCodeSetOB">对象实体类</param>
		/// <returns></returns>
        public static int Delete(UnitCodeSetOB _UnitCodeSetOB)
		{
			return Delete(null,_UnitCodeSetOB);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="UnitCodeSetOB">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,UnitCodeSetOB _UnitCodeSetOB)
		{
			string sql=@"DELETE FROM dbo.UnitCodeSet WHERE UnitCode =@UnitCode";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("UnitCode",DbType.String,_UnitCodeSetOB.UnitCode));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="UnitCodeSetID">主键</param>
        public static UnitCodeSetOB GetObject( string UnitCode )
		{
			string sql=@"
			SELECT UnitCode,UnitName,CreatePerson,CreateTime,ModifyPerson,ModifyTime
			FROM dbo.UnitCodeSet
			WHERE UnitCode =@UnitCode";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("UnitCode", DbType.String, UnitCode));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                UnitCodeSetOB _UnitCodeSetOB = null;
                if (reader.Read())
                {
                    _UnitCodeSetOB = new UnitCodeSetOB();
					if (reader["UnitCode"] != DBNull.Value) _UnitCodeSetOB.UnitCode = Convert.ToString(reader["UnitCode"]);
					if (reader["UnitName"] != DBNull.Value) _UnitCodeSetOB.UnitName = Convert.ToString(reader["UnitName"]);
					if (reader["CreatePerson"] != DBNull.Value) _UnitCodeSetOB.CreatePerson = Convert.ToString(reader["CreatePerson"]);
					if (reader["CreateTime"] != DBNull.Value) _UnitCodeSetOB.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
					if (reader["ModifyPerson"] != DBNull.Value) _UnitCodeSetOB.ModifyPerson = Convert.ToString(reader["ModifyPerson"]);
					if (reader["ModifyTime"] != DBNull.Value) _UnitCodeSetOB.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                }
				reader.Close();
                db.Close();
                return _UnitCodeSetOB;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.UnitCodeSet", "*", filterWhereString, orderBy == "" ? " UnitCode" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.UnitCodeSet", filterWhereString);
        }
    }
}
