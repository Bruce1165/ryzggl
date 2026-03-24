using Model;
using System;
using System.Data;
using System.Data.SqlClient;
using Utility;
using System.Collections.Generic;

namespace DataAccess
{
    public class DepartmentDAL
    {
        //public static bool AddDepartment(DepartmentMDL department)
        //{
        //    string sql =
        //        "INSERT Department (DeptID,pDeptID, OrganID, DeptName, OrderID)" +
        //        "VALUES (@DeptID,@pDeptID, @OrganID, @DeptName, @OrderID)";


        //    try
        //    {
        //        SqlParameter[] para =
        //        {
        //            new SqlParameter("@DeptID", department.DeptID),
        //            new SqlParameter("@OrganID", department.Organ.OrganID), //FK
        //            new SqlParameter("@pDeptID", department.PDeptID),
        //            new SqlParameter("@DeptName", department.DeptName),
        //            new SqlParameter("@OrderID", department.OrderID)
        //        };

        //        return (new DBHelper()).GetExcuteNonQuery(sql, para) > 0 ? true : false;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        throw e;
        //    }
        //}

        //public static DepartmentMDL GetDepartmentByDeptID(string deptID)
        //{
        //    string sql = "SELECT * FROM Department WHERE DeptID = @DeptID";

        //    string organID;

        //    try
        //    {
        //        SqlDataReader dr = (new DBHelper()).GetDataReader(sql, new SqlParameter("@DeptID", deptID));
        //        if (dr.Read())
        //        {
        //            var department = new DepartmentMDL();

        //            department.DeptID = (string)Check.ConvertDBNull(dr["DeptID"], SqlDbType.VarChar);
        //            department.PDeptID = (string)Check.ConvertDBNull(dr["pDeptID"], SqlDbType.VarChar);
        //            department.DeptName = (string)Check.ConvertDBNull(dr["DeptName"], SqlDbType.VarChar);
        //            department.OrderID = (int)Check.ConvertDBNull(dr["OrderID"], SqlDbType.Int);
        //            organID = (string)dr["OrganID"]; //FK

        //            dr.Close();

        //            department.Organ = OrganizationDAL.GetOrganizationByOrganID(organID);

        //            return department;
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

        ///// <summary>
        /////     根据机构ID获取部门
        ///// </summary>
        ///// <param name="OrganID"></param>
        ///// <returns></returns>
        //public static DataTable GetFirstDepartment(string OrganID)
        //{
        //    string SQL = "SELECT * FROM Department WHERE OrganID=@OrganID AND pDeptID='0' ORDER BY OrderID ASC";
        //    try
        //    {
        //        SqlParameter[] para =
        //        {
        //            new SqlParameter("@OrganID", OrganID)
        //        };
        //        return (new DBHelper()).GetFillData(SQL, para);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        throw e;
        //    }
        //}

        ///// <summary>
        /////     获取数据字典具体项
        ///// </summary>
        ///// <param name="TypeID"></param>
        ///// <param name="OrderID"></param>
        ///// <returns></returns>
        //public static DataTable GetAllDepartment(string DeptID, string OrganID)
        //{
        //    string sql = "select * from Department where 1=1";
        //    if (OrganID.Trim() != "")
        //    {
        //        sql += " and OrganID in (" + OrganID + ")";
        //    }
        //    if (DeptID.Trim() != "")
        //    {
        //        sql += " and DeptID in (" + DeptID + ")";
        //    }
        //    try
        //    {
        //        return (new DBHelper()).GetFillData(sql);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        throw e;
        //    }
        //}

        ///// <summary>
        /////     根据部门ID获取所有子级部门
        ///// </summary>
        ///// <param name="DeptID"></param>
        ///// <returns></returns>
        //public static DataTable GetAllChildDepartment(string DeptID)
        //{
        //    string SQL = "SELECT * FROM Department WHERE pDeptID=@DeptID ORDER BY OrderID ASC";
        //    try
        //    {
        //        SqlParameter[] para =
        //        {
        //            new SqlParameter("@DeptID", DeptID)
        //        };
        //        return (new DBHelper()).GetFillData(SQL, para);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        throw e;
        //    }
        //}

        //public static bool ModifyDepartment(DepartmentMDL department)
        //{
        //    string sql =
        //        "UPDATE Department " +
        //        "SET " +
        //        "OrganID = @OrganID, " + //FK
        //        "pDeptID = @pDeptID, " +
        //        "DeptName = @DeptName, " +
        //        "OrderID = @OrderID " +
        //        "WHERE DeptID = @DeptID";

        //    try
        //    {
        //        SqlParameter[] para =
        //        {
        //            new SqlParameter("@DeptID", department.DeptID),
        //            new SqlParameter("@OrganID", department.Organ.OrganID), //FK
        //            new SqlParameter("@pDeptID", department.PDeptID),
        //            new SqlParameter("@DeptName", department.DeptName),
        //            new SqlParameter("@OrderID", department.OrderID)
        //        };

        //        return (new DBHelper()).GetExcuteNonQuery(sql, para) > 0 ? true : false;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        throw e;
        //    }
        //}

        //public static bool DeleteDepartmentByDeptID(string deptID)
        //{
        //    string sql = "DELETE Department WHERE DeptID = @DeptID";

        //    try
        //    {
        //        SqlParameter[] para =
        //        {
        //            new SqlParameter("@DeptID", deptID)
        //        };

        //        return (new DBHelper()).GetExcuteNonQuery(sql, para) > 0 ? true : false;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        throw e;
        //    }
        //}

        ////判断当前机构或部门下是否有该部门
        //public static int GetCountIsDept(string DeptName, string OrganID, string pDeptID)
        //{
        //    string sql;
        //    if (OrganID != "")
        //    {
        //        sql = "select Count(*) from dbo.Department where DeptName=@DeptName and OrganID=@OrganID";
        //    }
        //    else
        //    {
        //        sql = "select Count(*) from dbo.Department where DeptName=@DeptName and pDeptID=@pDeptID";
        //    }
        //    try
        //    {
        //        SqlParameter[] para =
        //        {
        //            new SqlParameter("@DeptName", DeptName),
        //            new SqlParameter("@OrganID", OrganID),
        //            new SqlParameter("pDeptID", pDeptID)
        //        };
        //        return (new DBHelper()).ExecuteScalar<int>(sql, para);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        throw e;
        //    }
        //}

        public DepartmentDAL() { }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="DepartmentOB">对象实体类</param>
        /// <returns></returns>
        public static long Insert(DepartmentOB _DepartmentOB)
        {
            return Insert(null, _DepartmentOB);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="DepartmentOB">对象实体类</param>
        /// <returns></returns>
        public static long Insert(DbTransaction tran, DepartmentOB _DepartmentOB)
        {
            DBHelper db = new DBHelper();


            string sql = @"
			INSERT INTO dbo.Department(pDeptID,OrganID,DeptName,OrderID)
			VALUES (@pDeptID,@OrganID,@DeptName,@OrderID);SELECT @DeptID = @@IDENTITY";

            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateOutParameter("DeptID", DbType.Int64));
            p.Add(db.CreateParameter("pDeptID", DbType.Int64, _DepartmentOB.pDeptID));
            p.Add(db.CreateParameter("OrganID", DbType.Int64, _DepartmentOB.OrganID));
            p.Add(db.CreateParameter("DeptName", DbType.String, _DepartmentOB.DeptName));
            p.Add(db.CreateParameter("OrderID", DbType.Int32, _DepartmentOB.OrderID));
            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            _DepartmentOB.DeptID = Convert.ToInt64(p[0].Value);
            return rtn;
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="DepartmentOB">对象实体类</param>
        /// <returns></returns>
        public static int Update(DepartmentOB _DepartmentOB)
        {
            return Update(null, _DepartmentOB);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="DepartmentOB">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, DepartmentOB _DepartmentOB)
        {
            string sql = @"
			UPDATE dbo.Department
				SET	pDeptID = @pDeptID,OrganID = @OrganID,DeptName = @DeptName,OrderID = @OrderID
			WHERE
				DeptID = @DeptID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("DeptID", DbType.Int64, _DepartmentOB.DeptID));
            p.Add(db.CreateParameter("pDeptID", DbType.Int64, _DepartmentOB.pDeptID));
            p.Add(db.CreateParameter("OrganID", DbType.Int64, _DepartmentOB.OrganID));
            p.Add(db.CreateParameter("DeptName", DbType.String, _DepartmentOB.DeptName));
            p.Add(db.CreateParameter("OrderID", DbType.Int32, _DepartmentOB.OrderID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="DepartmentID">主键</param>
        /// <returns></returns>
        public static int Delete(long DeptID)
        {
            return Delete(null, DeptID);
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="DepartmentID">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, long DeptID)
        {
            string sql = @"DELETE FROM dbo.Department WHERE DeptID = @DeptID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("DeptID", DbType.Int64, DeptID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="DepartmentOB">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DepartmentOB _DepartmentOB)
        {
            return Delete(null, _DepartmentOB);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="DepartmentOB">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, DepartmentOB _DepartmentOB)
        {
            string sql = @"DELETE FROM dbo.Department WHERE DeptID = @DeptID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("DeptID", DbType.Int64, _DepartmentOB.DeptID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="DepartmentID">主键</param>
        public static DepartmentOB GetObject(long DeptID)
        {
            string sql = @"
			SELECT DeptID,pDeptID,OrganID,DeptName,OrderID
			FROM dbo.Department
			WHERE DeptID = @DeptID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("DeptID", DbType.Int64, DeptID));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    DepartmentOB _DepartmentOB = null;
                    if (reader.Read())
                    {
                        _DepartmentOB = new DepartmentOB();
                        if (reader["DeptID"] != DBNull.Value) _DepartmentOB.DeptID = Convert.ToInt64(reader["DeptID"]);
                        if (reader["pDeptID"] != DBNull.Value) _DepartmentOB.pDeptID = Convert.ToInt64(reader["pDeptID"]);
                        if (reader["OrganID"] != DBNull.Value) _DepartmentOB.OrganID = Convert.ToInt64(reader["OrganID"]);
                        if (reader["DeptName"] != DBNull.Value) _DepartmentOB.DeptName = Convert.ToString(reader["DeptName"]);
                        if (reader["OrderID"] != DBNull.Value) _DepartmentOB.OrderID = Convert.ToInt32(reader["OrderID"]);
                    }
                    reader.Close();
                    db.Close();
                    return _DepartmentOB;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.Department", "*", filterWhereString, orderBy == "" ? " DeptID" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.Department", filterWhereString);
        }

        //根据机构ID获取所有一级部门(王，用于部门树)
        public static DataTable GetAllFirstDepartment(string OrganID)
        {
            string sql = @"SELECT DeptID,pDeptID,OrganID,DeptName,OrderID
			FROM dbo.Department
            WHERE OrganID=@OrganID AND pDeptID='0' ORDER BY OrderID ASC";
            try
            {
                DBHelper db = new DBHelper();
                List<SqlParameter> p = new List<SqlParameter>();
                p.Add(db.CreateParameter("OrganID", DbType.String, OrganID));
                return db.GetFillData(sql, p.ToArray());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }
        //根据部门ID获取所有子级部门（王，用于部门树）
        public static DataTable GetAllChildDepartment(string DeptID)
        {
            string sql = @"SELECT DeptID,pDeptID,OrganID,DeptName,OrderID
			FROM dbo.Department
            WHERE pDeptID=@DeptID ORDER BY OrderID ASC";
            try
            {
                DBHelper db = new DBHelper();
                List<SqlParameter> p = new List<SqlParameter>();
                p.Add(db.CreateParameter("DeptID", DbType.String, DeptID));
                return db.GetFillData(sql, p.ToArray());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }
        //获取当前机构下是否有部门
        public static int GetDeptCountByOrganID(string OrganID)
        {
            string sql = @"SELECT COUNT(*) FROM dbo.Department WHERE OrganID=@OrganID";
            try
            {
                DBHelper db = new DBHelper();
                List<SqlParameter> p = new List<SqlParameter>();
                p.Add(db.CreateParameter("OrganID", DbType.Int64, OrganID));
                return Convert.ToInt32(db.ExecuteScalar(sql, p.ToArray()));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }
        public static int GetCountIsDept(string DeptName, Int64 OrganID, Int64 pDeptID)
        {
            string sql;
            if (OrganID != 0)
            {
                sql = @"SELECT COUNT(*) FROM dbo.Department WHERE DeptName=@DeptName AND OrganID=@OrganID";
            }
            else
            {
                sql = @"SELECT COUNT(*) FROM dbo.Department WHERE DeptName=@DeptName and pDeptID=@pDeptID";
            }
            try
            {
                DBHelper db = new DBHelper();
                List<SqlParameter> p = new List<SqlParameter>();
                p.Add(db.CreateParameter("DeptName", DbType.String, DeptName));
                p.Add(db.CreateParameter("OrganID", DbType.Int64, OrganID));
                p.Add(db.CreateParameter("pDeptID", DbType.Int64, pDeptID));
                return Convert.ToInt32(db.ExecuteScalar(sql, p.ToArray()));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }
    }
}