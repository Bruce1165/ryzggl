using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DataAccess
{
	/// <summary>
	/// 业务类实现--PackageSourceDAL(填写类描述)
	/// </summary>
    public class PackageSourceDAL
    {
        public PackageSourceDAL(){}       
		
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="_PackageSourceMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(PackageSourceMDL _PackageSourceMDL)
		{
		    return Insert(null,_PackageSourceMDL);
		}
		/// <summary>
        /// 插入信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_PackageSourceMDL">对象实体类</param>
		/// <returns></returns>
        public static int Insert(DbTransaction tran,PackageSourceMDL _PackageSourceMDL)
		{
            DBHelper db = new DBHelper("DBRYPX");		
			string sql = @"
			INSERT INTO dbo.PackageSource(SourceID,PackageID,SortID,SourceYear)
			VALUES (@SourceID,@PackageID,@SortID,@SourceYear)";

		    List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("SourceID",DbType.Int64, _PackageSourceMDL.SourceID));
			p.Add(db.CreateParameter("PackageID",DbType.Int64, _PackageSourceMDL.PackageID));
			p.Add(db.CreateParameter("SortID",DbType.Int32, _PackageSourceMDL.SortID));
            p.Add(db.CreateParameter("SourceYear", DbType.Int32, _PackageSourceMDL.SourceYear));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_PackageSourceMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(PackageSourceMDL _PackageSourceMDL)
		{
			return Update(null,_PackageSourceMDL);
		}
		/// <summary>
        /// 更新信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_PackageSourceMDL">对象实体类</param>
		/// <returns></returns>
        public static int Update(DbTransaction tran,PackageSourceMDL _PackageSourceMDL)
		{
			string sql = @"
			UPDATE dbo.PackageSource
				SET	SortID = @SortID,SourceYear= @SourceYear
			WHERE
				SourceID = @SourceID AND PackageID = @PackageID";

            DBHelper db = new DBHelper("DBRYPX");
			List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("SourceID",DbType.Int64, _PackageSourceMDL.SourceID));
			p.Add(db.CreateParameter("PackageID",DbType.Int64, _PackageSourceMDL.PackageID));
			p.Add(db.CreateParameter("SortID",DbType.Int32, _PackageSourceMDL.SortID));
            p.Add(db.CreateParameter("SourceYear", DbType.Int32, _PackageSourceMDL.SourceYear));
			return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="SourceID">课程ID</param>
        ///  <param name="PackageID">课程包ID</param>
		/// <returns></returns>
        public static int Delete( long SourceID, long PackageID )
		{
			return Delete(null, SourceID, PackageID);
		}
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="SourceID">课程ID</param>
        ///  <param name="PackageID">课程包ID</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran, long SourceID, long PackageID)
		{
			string sql=@"DELETE FROM dbo.PackageSource WHERE SourceID = @SourceID AND PackageID = @PackageID";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("SourceID",DbType.Int64,SourceID));
			p.Add(db.CreateParameter("PackageID",DbType.Int64,PackageID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		 /// <param name="tran">事务</param>
        /// <param name="_PackageSourceMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(PackageSourceMDL _PackageSourceMDL)
		{
			return Delete(null,_PackageSourceMDL);
		}
		/// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
		/// <param name="tran">事务</param>
        /// <param name="_PackageSourceMDL">对象实体类</param>
		/// <returns></returns>
        public static int Delete(DbTransaction tran,PackageSourceMDL _PackageSourceMDL)
		{
			string sql=@"DELETE FROM dbo.PackageSource WHERE SourceID = @SourceID AND PackageID = @PackageID";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("SourceID",DbType.Int64,_PackageSourceMDL.SourceID));
			p.Add(db.CreateParameter("PackageID",DbType.Int64,_PackageSourceMDL.PackageID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
		}

	    /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="SourceID">课程ID</param>
        /// <param name="PackageID">课程包ID</param>
        public static PackageSourceMDL GetObject( long SourceID, long PackageID )
		{
			string sql= @"
			SELECT SourceID,PackageID,SortID,SourceYear
			FROM dbo.PackageSource
			WHERE SourceID = @SourceID AND PackageID = @PackageID";

            DBHelper db = new DBHelper("DBRYPX");
            //List<SqlParameter> p = new List<SqlParameter>();
            //p.Add(db.CreateParameter("SourceID", DbType.Int64, SourceID));
            List<SqlParameter> p = new List<SqlParameter>();
			p.Add(db.CreateParameter("SourceID",DbType.Int64,SourceID));
			p.Add(db.CreateParameter("PackageID",DbType.Int64,PackageID));
            using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
            {
                PackageSourceMDL _PackageSourceMDL = null;
                if (reader.Read())
                {
                    _PackageSourceMDL = new PackageSourceMDL();
					if (reader["SourceID"] != DBNull.Value) _PackageSourceMDL.SourceID = Convert.ToInt64(reader["SourceID"]);
					if (reader["PackageID"] != DBNull.Value) _PackageSourceMDL.PackageID = Convert.ToInt64(reader["PackageID"]);
					if (reader["SortID"] != DBNull.Value) _PackageSourceMDL.SortID = Convert.ToInt32(reader["SortID"]);
                    if (reader["SourceYear"] != DBNull.Value) _PackageSourceMDL.SortID = Convert.ToInt32(reader["SourceYear"]);
                }
				reader.Close();
                db.Close();
                return _PackageSourceMDL;
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
            return CommonDAL.GetDataTableDB("DBRYPX",startRowIndex, maximumRows, "dbo.PackageSource", "*", filterWhereString, orderBy == "" ? " SourceID, PackageID" : orderBy);
        } 
		/// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCountDB("DBRYPX","dbo.PackageSource", filterWhereString);
        }
        
        #region 自定义方法
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="PackageID">主键</param>
        /// <returns></returns>
        public static int Delete(long PackageID)
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
            string sql = @"DELETE FROM PackageSource WHERE PackageID = @PackageID";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PackageID", DbType.Int64, PackageID));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="PackageID">课程包ID</param>
        /// <param name="year">上架年度</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, long PackageID,int year)
        {
            string sql = @"DELETE FROM PackageSource WHERE PackageID = @PackageID and [SourceYear]= @SourceYear";

            DBHelper db = new DBHelper("DBRYPX");
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("PackageID", DbType.Int64, PackageID));
            p.Add(db.CreateParameter("SourceYear", DbType.Int32, year));
            return db.GetExcuteNonQuery(tran, sql, p.ToArray());
        }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="SourceID">课程ID</param>
        /// <param name="PackageID">课程包ID</param>
        /// <param name="sortID">排序ID</param>
        /// <returns></returns>
        public static int Insert(DbTransaction tran, Int64 SourceID, Int64 PackageID, int sortID, int SourceYear)
        {
            DBHelper db = new DBHelper("DBRYPX");

            string sql = @"
			INSERT INTO packagesource(SourceID,PackageID,SortID,SourceYear)
			VALUES (@SourceID,@PackageID,@SortID,@SourceYear);";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("SourceID", DbType.Int64, SourceID));
            p.Add(db.CreateParameter("PackageID", DbType.Int64, PackageID));
            p.Add(db.CreateParameter("SortID", DbType.Int32, sortID *10));
            p.Add(db.CreateParameter("SourceYear", DbType.Int32, SourceYear));
            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            return rtn;
        }

        /// <summary>
        /// 统计某培训包已选课程学时数量
        /// </summary>
        /// <param name="PackageID">培训设置包ID</param>
        /// <param name="FilterYear">要过滤年度</param>
        /// <returns>返回已选课时数</returns>
        public static decimal GetPackageSourcePeriodSum(long PackageID, int FilterYear)
        {
            string sql = @"select isnull(sum([Source].ShowPeriod),0)
                           FROM [dbo].[PackageSource]
                          inner join  [dbo].[Source]  on   [PackageSource].SourceID = [Source].SourceID
                          where [PackageSource].[PackageID]={0} and [Source].SourceYear <> {1}";
            sql = string.Format(sql, PackageID, FilterYear);
            return (new DBHelper("DBRYPX")).ExecuteScalar<decimal>(sql);
        }
        #endregion
    }
}
