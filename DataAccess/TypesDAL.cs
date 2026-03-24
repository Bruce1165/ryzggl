using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--TypesDAL(填写类描述)
	/// </summary>
    public class TypesDAL
    {
        public TypesDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="TypesOB">对象实体类</param>
		/// <returns></returns>
        public static int Insert(TypesOB _TypesOB)
		{
		    return Insert(null,_TypesOB);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="TypesOB">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,TypesOB _TypesOB)
		{
			DBHelper db = new DBHelper();
		
			string sql = @"
			INSERT INTO dbo.[Types](TypeID,SortID,TypeName,TypeValue)
			VALUES (:TypeID,:SortID,:TypeName,:TypeValue)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("TypeID",DbType.String, _TypesOB.TypeID));
			p.Add(db.CreateParameter("SortID",DbType.Int32, _TypesOB.SortID));
			p.Add(db.CreateParameter("TypeName",DbType.String, _TypesOB.TypeName));
			p.Add(db.CreateParameter("TypeValue",DbType.String, _TypesOB.TypeValue));
			return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="TypesOB">对象实体类</param>
		/// <returns></returns>
        public static int Update(TypesOB _TypesOB)
		{
			return Update(null,_TypesOB);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="TypesOB">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,TypesOB _TypesOB)
		{
			string sql = @"
			UPDATE dbo.[Types]
				SET	TypeValue =@TypeValue
			WHERE
				TypeID =@TypeID and TypeName =@TypeName";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("TypeID",DbType.String, _TypesOB.TypeID));
			p.Add(db.CreateParameter("TypeName",DbType.String, _TypesOB.TypeName));
			p.Add(db.CreateParameter("TypeValue",DbType.String, _TypesOB.TypeValue));
			return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="TypesID">主键</param>
		/// <returns></returns>
        public static int Delete( string TypeID )
		{
			return Delete(null, TypeID);
		}
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="TypesID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, string TypeID)
		{
            string sql = @"DELETE FROM dbo.[Types] WHERE TypeID =@TypeID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("TypeID",DbType.String,TypeID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="TypesOB">对象实体类</param>
		/// <returns></returns>
        public static int Delete(TypesOB _TypesOB)
		{
			return Delete(null,_TypesOB);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="TypesOB">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,TypesOB _TypesOB)
		{
            string sql = @"DELETE FROM dbo.[Types] WHERE TypeID =@TypeID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("TypeID",DbType.String,_TypesOB.TypeID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="TypesID">主键</param>
        public static TypesOB GetObject(string TypeID,string TypeName)
        {
            string sql = @"
			SELECT TypeID,SortID,TypeName,TypeValue
			FROM dbo.[Types]
			WHERE TypeID =@TypeID and TypeName =@TypeName";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("TypeID", DbType.String, TypeID));
            p.Add(db.CreateParameter("TypeName", DbType.String, TypeName));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    TypesOB _TypesOB = null;
                    if (reader.Read())
                    {
                        _TypesOB = new TypesOB();
                        if (reader["TypeID"] != DBNull.Value) _TypesOB.TypeID = Convert.ToString(reader["TypeID"]);
                        if (reader["SortID"] != DBNull.Value) _TypesOB.SortID = Convert.ToInt32(reader["SortID"]);
                        if (reader["TypeName"] != DBNull.Value) _TypesOB.TypeName = Convert.ToString(reader["TypeName"]);
                        if (reader["TypeValue"] != DBNull.Value) _TypesOB.TypeValue = Convert.ToString(reader["TypeValue"]);
                    }
                    reader.Close();
                    db.Close();
                    return _TypesOB;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.[Types]", "*", filterWhereString, orderBy == "" ? " TypeID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.[Types]", filterWhereString);
        }

        /// <summary>
        /// 根据类型ID获取配置集合
        /// </summary>
        /// <param name="typeID">类型ID</param>
        /// <returns></returns>
        public static DataTable GetListByTypeID(string typeID)
        {
            return CommonDAL.GetDataTable(0, int.MaxValue - 1, "dbo.[Types]", "*", string.Format(" and TypeID='{0}'", typeID), " SortID");
        } 
    }
}
