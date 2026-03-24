using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--UserRoleDAL(填写类描述)
	/// </summary>
    public class UserRoleDAL
    {
        public UserRoleDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="UserRoleMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(UserRoleMDL _UserRoleMDL)
		{
		    return Insert(null,_UserRoleMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="UserRoleMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,UserRoleMDL _UserRoleMDL)
		{
			DBHelper db = new DBHelper();		
			string sql = @"
			INSERT INTO dbo.UserRole([ID],UserID,RoleID)
			VALUES (@ID,@UserID,@RoleID)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ID",DbType.Int32, _UserRoleMDL.ID));
			p.Add(db.CreateParameter("UserID",DbType.String, _UserRoleMDL.UserID));
			p.Add(db.CreateParameter("RoleID",DbType.String, _UserRoleMDL.RoleID));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="UserRoleMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(UserRoleMDL _UserRoleMDL)
		{
			return Update(null,_UserRoleMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="UserRoleMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,UserRoleMDL _UserRoleMDL)
		{
			string sql = @"
			UPDATE dbo.UserRole
				SET	UserID = @UserID,RoleID = @RoleID
			WHERE
				ID = @ID";

			DBHelper db = new DBHelper();
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ID",DbType.Int32, _UserRoleMDL.ID));
			p.Add(db.CreateParameter("UserID",DbType.String, _UserRoleMDL.UserID));
			p.Add(db.CreateParameter("RoleID",DbType.String, _UserRoleMDL.RoleID));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="UserRoleID">主键</param>
		/// <returns></returns>
        public static int Delete( int ID )
		{
			return Delete(null, ID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="UserRoleID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, int ID)
		{
			string sql=@"DELETE FROM dbo.UserRole WHERE ID = @ID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ID",DbType.Int32,ID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="UserRoleMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(UserRoleMDL _UserRoleMDL)
		{
			return Delete(null,_UserRoleMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="UserRoleMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,UserRoleMDL _UserRoleMDL)
		{
			string sql=@"DELETE FROM dbo.UserRole WHERE ID = @ID";

			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("ID",DbType.Int32,_UserRoleMDL.ID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="UserRoleID">主键</param>
        public static UserRoleMDL GetObject( int ID )
		{
			string sql=@"
			SELECT [ID],UserID,RoleID
			FROM dbo.UserRole
			WHERE ID = @ID";
			
			DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ID", DbType.Int32, ID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                UserRoleMDL _UserRoleMDL = null;
                if (reader.Read())
                {
                    _UserRoleMDL = new UserRoleMDL();
					if (reader["ID"] != DBNull.Value) _UserRoleMDL.ID = Convert.ToInt32(reader["ID"]);
					if (reader["UserID"] != DBNull.Value) _UserRoleMDL.UserID = Convert.ToString(reader["UserID"]);
					if (reader["RoleID"] != DBNull.Value) _UserRoleMDL.RoleID = Convert.ToString(reader["RoleID"]);
                }
				reader.Close();
                db.Close();
                return _UserRoleMDL;
            }
		}
        /// <summary>
        /// 根据用户获取单个实体
        /// </summary>
        /// <param name="UserRoleID">主键</param>
        public static UserRoleMDL GetObjectUserID(string UserID)
        {
            string sql = @"
			SELECT [ID],UserID,RoleID
			FROM dbo.UserRole
			WHERE UserID = @UserID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("UserID", DbType.String, UserID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                UserRoleMDL _UserRoleMDL = null;
                if (reader.Read())
                {
                    _UserRoleMDL = new UserRoleMDL();
                    if (reader["ID"] != DBNull.Value) _UserRoleMDL.ID = Convert.ToInt32(reader["ID"]);
                    if (reader["UserID"] != DBNull.Value) _UserRoleMDL.UserID = Convert.ToString(reader["UserID"]);
                    if (reader["RoleID"] != DBNull.Value) _UserRoleMDL.RoleID = Convert.ToString(reader["RoleID"]);
                }
                reader.Close();
                db.Close();
                return _UserRoleMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.UserRole", "*", filterWhereString, orderBy == "" ? " ID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.UserRole", filterWhereString);
        }
        
        #region 自定义方法
        
        #endregion
    }
}
