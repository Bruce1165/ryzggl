using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
    /// <summary>
    /// 业务类实现--PolicyNewsDAL(填写类描述)
    /// </summary>
    public class PolicyNewsDAL
    {
        public PolicyNewsDAL() { }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="PolicyNewsMDL">对象实体类</param>
        /// <returns></returns>
        public static int Insert(PolicyNewsMDL _PolicyNewsMDL)
        {
            return Insert(null, _PolicyNewsMDL);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="PolicyNewsMDL">对象实体类</param>
        /// <returns></returns>
        public static int Insert(DbTransaction tran, PolicyNewsMDL _PolicyNewsMDL)
        {
            DBHelper db = new DBHelper();
            string sql = @"
			INSERT INTO dbo.PolicyNews([ID],Title,Content,FileUrl,GetDateTime)
			VALUES (@ID,@Title,@Content,@FileUrl,@GetDateTime)";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ID", DbType.String, _PolicyNewsMDL.ID));
            p.Add(db.CreateParameter("Title", DbType.String, _PolicyNewsMDL.Title));
            p.Add(db.CreateParameter("Content", DbType.String, _PolicyNewsMDL.Content));
            p.Add(db.CreateParameter("FileUrl", DbType.String, _PolicyNewsMDL.FileUrl));
            p.Add(db.CreateParameter("GetDateTime", DbType.DateTime, _PolicyNewsMDL.GetDateTime));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="PolicyNewsMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(PolicyNewsMDL _PolicyNewsMDL)
        {
            return Update(null, _PolicyNewsMDL);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="PolicyNewsMDL">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, PolicyNewsMDL _PolicyNewsMDL)
        {
            string sql = @"
			UPDATE dbo.PolicyNews
				SET	Title = @Title,Content = @Content,FileUrl = @FileUrl,GetDateTime = @GetDateTime
			WHERE
				ID = @ID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ID", DbType.String, _PolicyNewsMDL.ID));
            p.Add(db.CreateParameter("Title", DbType.String, _PolicyNewsMDL.Title));
            p.Add(db.CreateParameter("Content", DbType.String, _PolicyNewsMDL.Content));
            p.Add(db.CreateParameter("FileUrl", DbType.String, _PolicyNewsMDL.FileUrl));
            p.Add(db.CreateParameter("GetDateTime", DbType.DateTime, _PolicyNewsMDL.GetDateTime));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="PolicyNewsID">主键</param>
        /// <returns></returns>
        public static int Delete(string ID)
        {
            return Delete(null, ID);
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="PolicyNewsID">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, string ID)
        {
            string sql = "DELETE FROM dbo.PolicyNews WHERE ID in({0})";

            DBHelper db = new DBHelper();

            return db.GetExcuteNonQuery(tran, string.Format(sql, ID));
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="PolicyNewsMDL">对象实体类</param>
        /// <returns></returns>
        public static int Delete(PolicyNewsMDL _PolicyNewsMDL)
        {
            return Delete(null, _PolicyNewsMDL);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="PolicyNewsMDL">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, PolicyNewsMDL _PolicyNewsMDL)
        {
            string sql = @"DELETE FROM dbo.PolicyNews WHERE ID = @ID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ID", DbType.String, _PolicyNewsMDL.ID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="PolicyNewsID">主键</param>
        public static PolicyNewsMDL GetObject(string ID)
        {
            string sql = @"
			SELECT [ID],Title,Content,FileUrl,GetDateTime
			FROM dbo.PolicyNews
			WHERE ID = @ID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("ID", DbType.String, ID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                PolicyNewsMDL _PolicyNewsMDL = null;
                if (reader.Read())
                {
                    _PolicyNewsMDL = new PolicyNewsMDL();
                    if (reader["ID"] != DBNull.Value) _PolicyNewsMDL.ID = Convert.ToString(reader["ID"]);
                    if (reader["Title"] != DBNull.Value) _PolicyNewsMDL.Title = Convert.ToString(reader["Title"]);
                    if (reader["Content"] != DBNull.Value) _PolicyNewsMDL.Content = Convert.ToString(reader["Content"]);
                    if (reader["FileUrl"] != DBNull.Value) _PolicyNewsMDL.FileUrl = Convert.ToString(reader["FileUrl"]);
                    if (reader["GetDateTime"] != DBNull.Value) _PolicyNewsMDL.GetDateTime = Convert.ToDateTime(reader["GetDateTime"]);
                }
                reader.Close();
                db.Close();
                return _PolicyNewsMDL;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.PolicyNews", "*", filterWhereString, orderBy == "" ? " GetDateTime desc" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.PolicyNews", filterWhereString);
        }

        #region 自定义方法

        #endregion
    }
}
