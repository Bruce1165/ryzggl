using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--PackageDAL(填写类描述)
	/// </summary>
    public class PackageDAL
    {
        public PackageDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_PackageMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(PackageMDL _PackageMDL)
		{
		    return Insert(null,_PackageMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_PackageMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,PackageMDL _PackageMDL)
		{
            DBHelper db = new DBHelper("DBRYPX");		
			string sql = @"
			INSERT INTO dbo.Package(PackageTitle,Period,Description,Status,PostTypeName,PostName)
			VALUES (@PackageTitle,@Period,@Description,@Status,@PostTypeName,@PostName);SELECT @PackageID = @@IDENTITY";

		    List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateOutParameter("PackageID", DbType.Int64));  
			p.Add(db.CreateParameter("PackageTitle",DbType.String, _PackageMDL.PackageTitle));
			p.Add(db.CreateParameter("Period",DbType.Int32, _PackageMDL.Period));
			p.Add(db.CreateParameter("Description",DbType.String, _PackageMDL.Description));
			p.Add(db.CreateParameter("Status",DbType.String, _PackageMDL.Status));
			p.Add(db.CreateParameter("PostTypeName",DbType.String, _PackageMDL.PostTypeName));
			p.Add(db.CreateParameter("PostName",DbType.String, _PackageMDL.PostName));

            int rtn = db.GetExcuteNonQuery(tran, sql, p.ToArray());
            _PackageMDL.PackageID = Convert.ToInt64(p[0].Value);
            return rtn;
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_PackageMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(PackageMDL _PackageMDL)
		{
			return Update(null,_PackageMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_PackageMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,PackageMDL _PackageMDL)
		{
			string sql = @"
			UPDATE dbo.Package
				SET	PackageTitle = @PackageTitle,Period = @Period,Description = @Description,Status = @Status,PostTypeName = @PostTypeName,PostName = @PostName
			WHERE
				PackageID = @PackageID";

            DBHelper db = new DBHelper("DBRYPX");
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("PackageID",DbType.Int64, _PackageMDL.PackageID));
			p.Add(db.CreateParameter("PackageTitle",DbType.String, _PackageMDL.PackageTitle));
			p.Add(db.CreateParameter("Period",DbType.Int32, _PackageMDL.Period));
			p.Add(db.CreateParameter("Description",DbType.String, _PackageMDL.Description));
			p.Add(db.CreateParameter("Status",DbType.String, _PackageMDL.Status));
			p.Add(db.CreateParameter("PostTypeName",DbType.String, _PackageMDL.PostTypeName));
			p.Add(db.CreateParameter("PostName",DbType.String, _PackageMDL.PostName));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
       	/// <param name="PackageID">主键</param>
		/// <returns></returns>
        public static int Delete( long PackageID )
		{
			return Delete(null, PackageID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
       	/// <param name="PackageID">主键</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, long PackageID)
		{
			string sql=@"DELETE FROM dbo.Package WHERE PackageID = @PackageID";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("PackageID",DbType.Int64,PackageID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="_PackageMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(PackageMDL _PackageMDL)
		{
			return Delete(null,_PackageMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_PackageMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,PackageMDL _PackageMDL)
		{
			string sql=@"DELETE FROM dbo.Package WHERE PackageID = @PackageID";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("PackageID",DbType.Int64,_PackageMDL.PackageID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="PackageID">主键</param>
        public static PackageMDL GetObject( long PackageID )
		{
			string sql=@"
			SELECT PackageID,PackageTitle,Period,Description,Status,PostTypeName,PostName
			FROM dbo.Package
			WHERE PackageID = @PackageID";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("PackageID",DbType.Int64,PackageID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                PackageMDL _PackageMDL = null;
                if (reader.Read())
                {
                    _PackageMDL = new PackageMDL();
					if (reader["PackageID"] != DBNull.Value) _PackageMDL.PackageID = Convert.ToInt64(reader["PackageID"]);
					if (reader["PackageTitle"] != DBNull.Value) _PackageMDL.PackageTitle = Convert.ToString(reader["PackageTitle"]);
					if (reader["Period"] != DBNull.Value) _PackageMDL.Period = Convert.ToInt32(reader["Period"]);
					if (reader["Description"] != DBNull.Value) _PackageMDL.Description = Convert.ToString(reader["Description"]);
					if (reader["Status"] != DBNull.Value) _PackageMDL.Status = Convert.ToString(reader["Status"]);
					if (reader["PostTypeName"] != DBNull.Value) _PackageMDL.PostTypeName = Convert.ToString(reader["PostTypeName"]);
					if (reader["PostName"] != DBNull.Value) _PackageMDL.PostName = Convert.ToString(reader["PostName"]);
                }
				reader.Close();
                db.Close();
                return _PackageMDL;
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
            return CommonDAL.GetDataTableDB("DBRYPX",startRowIndex, maximumRows, "dbo.Package", "*", filterWhereString, orderBy == "" ? " PackageID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCountDB("DBRYPX","dbo.Package", filterWhereString);
        }
        
        #region 自定义方法
        /// <summary>
        /// 发布培训计划
        /// </summary>
        /// <param name="PackageIDList">培训计划ID集合，用逗号分割</param>
        /// <param name="PublishStatus">发布状态</param>
        /// <returns></returns>
        public static int Publish(string PackageIDList, string PublishStatus)
        {
            string sql = string.Format("UPDATE package set Status = @Status	WHERE PackageID in({0})", PackageIDList);

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("Status", DbType.String, PublishStatus));
            return db.ExcuteNonQuery(sql, p.ToArray());
        }

        /// <summary>
        /// 根据岗位类别和岗位工种名称获取培训配置包
        /// </summary>
        /// <param name="PostTypeName">岗位类别名称（必填）</param>
        /// <param name="PostName">岗位工种名称（选填）</param>
        /// <returns>培训配置包</returns>
        public static PackageMDL GetObject(string PostTypeName, string PostName)
        {
            string sql = @"
			SELECT top 1 PackageID,PackageTitle,Period,Description,Status,PostTypeName,PostName
			FROM dbo.Package
			WHERE PostTypeName=@PostTypeName  and (PostName is null or PostName=@PostName)";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PostTypeName", DbType.String, PostTypeName));
            p.Add(db.CreateParameter("PostName", DbType.String, PostName));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                PackageMDL _PackageMDL = null;
                if (reader.Read())
                {
                    _PackageMDL = new PackageMDL();
                    if (reader["PackageID"] != DBNull.Value) _PackageMDL.PackageID = Convert.ToInt64(reader["PackageID"]);
                    if (reader["PackageTitle"] != DBNull.Value) _PackageMDL.PackageTitle = Convert.ToString(reader["PackageTitle"]);
                    if (reader["Period"] != DBNull.Value) _PackageMDL.Period = Convert.ToInt32(reader["Period"]);
                    if (reader["Description"] != DBNull.Value) _PackageMDL.Description = Convert.ToString(reader["Description"]);
                    if (reader["Status"] != DBNull.Value) _PackageMDL.Status = Convert.ToString(reader["Status"]);
                    if (reader["PostTypeName"] != DBNull.Value) _PackageMDL.PostTypeName = Convert.ToString(reader["PostTypeName"]);
                    if (reader["PostName"] != DBNull.Value) _PackageMDL.PostName = Convert.ToString(reader["PostName"]);
                }
                reader.Close();
                db.Close();
                return _PackageMDL;
            }
        }
        #endregion
    }
}
