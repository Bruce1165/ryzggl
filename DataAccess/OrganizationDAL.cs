using Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Utility;
using System.Collections.Generic;

namespace DataAccess
{
    public class OrganizationDAL
    {
        //public static OrganizationMDL GetOrganizationByOrganID(string organID)
        //{
        //    string sql =
        //        "SELECT Organization.*,Dictionary.OrderID AS RegionID FROM Organization LEFT OUTER JOIN Dictionary ON Organization.OrganType = Dictionary.DicName WHERE OrganID = @OrganID";

        //    try
        //    {
        //        SqlDataReader dr = (new DBHelper()).GetDataReader(sql, new SqlParameter("@OrganID", organID));
        //        if (dr.Read())
        //        {
        //            var organization = new OrganizationMDL();

        //            organization.OrganID = (string)Check.ConvertDBNull(dr["OrganID"], SqlDbType.VarChar);
        //            organization.OrderID = (int)Check.ConvertDBNull(dr["OrderID"], SqlDbType.Int);
        //            organization.OrganCoding = (string)Check.ConvertDBNull(dr["OrganCoding"], SqlDbType.VarChar);
        //            organization.OrganType = (string)Check.ConvertDBNull(dr["OrganType"], SqlDbType.VarChar);
        //            organization.OrganNature = (string)Check.ConvertDBNull(dr["OrganNature"], SqlDbType.VarChar);
        //            organization.OrganName = (string)Check.ConvertDBNull(dr["OrganName"], SqlDbType.VarChar);
        //            organization.OrganDescription =
        //                (string)Check.ConvertDBNull(dr["OrganDescription"], SqlDbType.VarChar);
        //            organization.BusinessProperties =
        //                (string)Check.ConvertDBNull(dr["BusinessProperties"], SqlDbType.VarChar);
        //            organization.OrganTelphone = (string)Check.ConvertDBNull(dr["OrganTelphone"], SqlDbType.VarChar);
        //            organization.OrganAddress = (string)Check.ConvertDBNull(dr["OrganAddress"], SqlDbType.VarChar);
        //            organization.OrganCode = (string)Check.ConvertDBNull(dr["OrganCode"], SqlDbType.VarChar);
        //            organization.RegionID = (string)Check.ConvertDBNull(dr["RegionID"], SqlDbType.VarChar);

        //            dr.Close();

        //            return organization;
        //        }
        //        dr.Close();
        //        return null;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        throw e;
        //    }
        //}

        //public static DataTable GetTableOrganizationByOrganID(string organID)
        //{
        //    string sql = "select * from Organization where OrganID=@OrganID";
        //    try
        //    {
        //        SqlParameter[] para =
        //        {
        //            new SqlParameter("@OrganID", organID)
        //        };
        //        return (new DBHelper()).GetFillData(sql, para);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        throw e;
        //    }
        //}

        //public static DataTable GetOrganizationByRegionID(string RegionID)
        //{
        //    string sql = "select * from Organization where RegionID=@RegionID";
        //    try
        //    {
        //        SqlParameter[] para =
        //        {
        //            new SqlParameter("@RegionID", RegionID)
        //        };
        //        return (new DBHelper()).GetFillData(sql, para);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        throw e;
        //    }
        //}

        ////Add by lys 2012-09-06
        //public static DataTable GetTableOrganization(string Coloumns, string Condition, string OrderBy)
        //{
        //    if (Coloumns.Trim() == "")
        //        Coloumns = "*";
        //    string sql = "select " + Coloumns + " from Organization where 1=1 ";
        //    try
        //    {
        //        sql += Condition;
        //        sql += OrderBy;
        //        return (new DBHelper()).GetFillData(sql, null);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        throw e;
        //    }
        //}

        ///// <summary>
        /////     获取所有子级机构
        ///// </summary>
        ///// <param name="coding">机构编码</param>
        ///// <param name="length">编码长度</param>
        ///// <returns></returns>
        //public static DataTable GetChildOrgan(string coding, int length)
        //{
        //    var sb = new StringBuilder();
        //    sb.Append("select * from Organization ");
        //    sb.Append(string.Format("where OrganCoding like '{0}%' ", coding));
        //    sb.Append(string.Format("and len(OrganCoding)='{0}' ", length));
        //    sb.Append("and IsVisible=1 ");
        //    sb.Append("order by OrderID");
        //    string SQLSTR = sb.ToString();
        //    return (new DBHelper()).GetFillData(SQLSTR, null);
        //}

        public OrganizationDAL() { }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="OrganizationOB">对象实体类</param>
        /// <returns></returns>
        public static long Insert(OrganizationOB _OrganizationOB)
        {
            return Insert(null, _OrganizationOB);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="OrganizationOB">对象实体类</param>
        /// <returns></returns>
        public static long Insert(DbTransaction tran, OrganizationOB _OrganizationOB)
        {
            DBHelper db = new DBHelper();

            string sql = @"
			INSERT INTO dbo.Organization(OrderID,OrganCoding,OrganType,OrganNature,OrganName,OrganDescription,BusinessProperties,OrganTelphone,OrganAddress,OrganCode)
			VALUES (@OrderID,@OrganCoding,@OrganType,@OrganNature,@OrganName,@OrganDescription,@BusinessProperties,@OrganTelphone,@OrganAddress,@OrganCode);SELECT @OrganID = @@IDENTITY";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateOutParameter("OrganID", DbType.Int64));
            p.Add(db.CreateParameter("OrderID", DbType.Int32, _OrganizationOB.OrderID));
            p.Add(db.CreateParameter("OrganCoding", DbType.String, _OrganizationOB.OrganCoding));
            p.Add(db.CreateParameter("OrganType", DbType.String, _OrganizationOB.OrganType));
            p.Add(db.CreateParameter("OrganNature", DbType.String, _OrganizationOB.OrganNature));
            p.Add(db.CreateParameter("OrganName", DbType.String, _OrganizationOB.OrganName));
            p.Add(db.CreateParameter("OrganDescription", DbType.String, _OrganizationOB.OrganDescription));
            p.Add(db.CreateParameter("BusinessProperties", DbType.String, _OrganizationOB.BusinessProperties));
            p.Add(db.CreateParameter("OrganTelphone", DbType.String, _OrganizationOB.OrganTelphone));
            p.Add(db.CreateParameter("OrganAddress", DbType.String, _OrganizationOB.OrganAddress));
            p.Add(db.CreateParameter("OrganCode", DbType.String, _OrganizationOB.OrganCode));

            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            _OrganizationOB.OrganID = Convert.ToInt64(p[0].Value);
            return rtn;
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="OrganizationOB">对象实体类</param>
        /// <returns></returns>
        public static int Update(OrganizationOB _OrganizationOB)
        {
            return Update(null, _OrganizationOB);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="OrganizationOB">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, OrganizationOB _OrganizationOB)
        {
            string sql = @"
			UPDATE dbo.Organization
				SET	OrderID = @OrderID,OrganCoding = @OrganCoding,OrganType = @OrganType,OrganNature = @OrganNature,OrganName = @OrganName,OrganDescription = @OrganDescription,BusinessProperties = @BusinessProperties,OrganTelphone = @OrganTelphone,OrganAddress = @OrganAddress,OrganCode = @OrganCode
			WHERE
				OrganID = @OrganID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("OrganID", DbType.Int64, _OrganizationOB.OrganID));
            p.Add(db.CreateParameter("OrderID", DbType.Int32, _OrganizationOB.OrderID));
            p.Add(db.CreateParameter("OrganCoding", DbType.String, _OrganizationOB.OrganCoding));
            p.Add(db.CreateParameter("OrganType", DbType.String, _OrganizationOB.OrganType));
            p.Add(db.CreateParameter("OrganNature", DbType.String, _OrganizationOB.OrganNature));
            p.Add(db.CreateParameter("OrganName", DbType.String, _OrganizationOB.OrganName));
            p.Add(db.CreateParameter("OrganDescription", DbType.String, _OrganizationOB.OrganDescription));
            p.Add(db.CreateParameter("BusinessProperties", DbType.String, _OrganizationOB.BusinessProperties));
            p.Add(db.CreateParameter("OrganTelphone", DbType.String, _OrganizationOB.OrganTelphone));
            p.Add(db.CreateParameter("OrganAddress", DbType.String, _OrganizationOB.OrganAddress));
            p.Add(db.CreateParameter("OrganCode", DbType.String, _OrganizationOB.OrganCode));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="OrganizationID">主键</param>
        /// <returns></returns>
        public static int Delete(long OrganID)
        {
            return Delete(null, OrganID);
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="OrganizationID">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, long OrganID)
        {
            string sql = @"DELETE FROM dbo.Organization WHERE OrganID = @OrganID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("OrganID", DbType.Int64, OrganID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="OrganizationOB">对象实体类</param>
        /// <returns></returns>
        public static int Delete(OrganizationOB _OrganizationOB)
        {
            return Delete(null, _OrganizationOB);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="OrganizationOB">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, OrganizationOB _OrganizationOB)
        {
            string sql = @"DELETE FROM dbo.Organization WHERE OrganID = @OrganID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("OrganID", DbType.Int64, _OrganizationOB.OrganID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="OrganizationID">主键</param>
        public static OrganizationOB GetObject(long OrganID)
        {
            string sql = @"
			SELECT OrganID,OrderID,OrganCoding,OrganType,OrganNature,OrganName,OrganDescription,BusinessProperties,OrganTelphone,OrganAddress,OrganCode
			FROM dbo.Organization
			WHERE OrganID = @OrganID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("OrganID", DbType.Int64, OrganID));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    OrganizationOB _OrganizationOB = null;
                    if (reader.Read())
                    {
                        _OrganizationOB = new OrganizationOB();
                        if (reader["OrganID"] != DBNull.Value) _OrganizationOB.OrganID = Convert.ToInt64(reader["OrganID"]);
                        if (reader["OrderID"] != DBNull.Value) _OrganizationOB.OrderID = Convert.ToInt32(reader["OrderID"]);
                        if (reader["OrganCoding"] != DBNull.Value) _OrganizationOB.OrganCoding = Convert.ToString(reader["OrganCoding"]);
                        if (reader["OrganType"] != DBNull.Value) _OrganizationOB.OrganType = Convert.ToString(reader["OrganType"]);
                        if (reader["OrganNature"] != DBNull.Value) _OrganizationOB.OrganNature = Convert.ToString(reader["OrganNature"]);
                        if (reader["OrganName"] != DBNull.Value) _OrganizationOB.OrganName = Convert.ToString(reader["OrganName"]);
                        if (reader["OrganDescription"] != DBNull.Value) _OrganizationOB.OrganDescription = Convert.ToString(reader["OrganDescription"]);
                        if (reader["BusinessProperties"] != DBNull.Value) _OrganizationOB.BusinessProperties = Convert.ToString(reader["BusinessProperties"]);
                        if (reader["OrganTelphone"] != DBNull.Value) _OrganizationOB.OrganTelphone = Convert.ToString(reader["OrganTelphone"]);
                        if (reader["OrganAddress"] != DBNull.Value) _OrganizationOB.OrganAddress = Convert.ToString(reader["OrganAddress"]);
                        if (reader["OrganCode"] != DBNull.Value) _OrganizationOB.OrganCode = Convert.ToString(reader["OrganCode"]);
                    }
                    reader.Close();
                    db.Close();
                    return _OrganizationOB;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.Organization", "*", filterWhereString, orderBy == "" ? " OrganID" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.Organization", filterWhereString);
        }

        //获取所有机构（王）
        public static DataTable GetOrgan()
        {
            string sql = @"SELECT * FROM dbo.Organization WHERE len(OrganCoding)=4 ORDER BY OrderID";
            DBHelper db = new DBHelper();
            return db.GetFillData(sql);
        }
        //获取所有子级机构（王）
        public static DataTable GetChildOrgan(string coding)
        {
            string sql = @"SELECT * FROM dbo.Organization WHERE OrganCoding LIKE '" + coding + "%' AND len(OrganCoding)=6 ORDER BY OrderID";
            DBHelper db = new DBHelper();
            return db.GetFillData(sql);
        }
    }
}