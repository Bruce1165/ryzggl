using Model;
using System;
using System.Data;
using System.Data.SqlClient;
using Utility;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class UserDAL
    {
        //public static bool AddUser(DbTransaction tran, UserMDL user)
        //{
        //    var db = new DBHelper();
        //    string userId = Convert.ToString(db.ExecuteScalar<object>("select newid();")).ToUpper();
        //    user.UserID = userId;
        //    string sql =
        //        "INSERT [User] (UserID,OrganID, DeptID, UserName, UserPwd, RelUserName, Sex, License, Telphone, Mobile, Code)" +
        //        "VALUES (@UserID,@OrganID, @DeptID, @UserName, '0x'+UPPER(sys.fn_varbintohexsubstring(0, HashBytes('MD5', '" + user.UserPwd + "'), 1, 0)), @RelUserName, @Sex, @License, @Telphone, @Mobile, @Code)";
        //    sql += " ; SELECT @@IDENTITY";

        //    SqlParameter[] para =
        //    {
        //        new SqlParameter("@UserID", user.UserID),
        //        new SqlParameter("@DeptID", user.Dept.DeptID), //FK
        //        new SqlParameter("@OrganID", user.Organ.OrganID), //FK
        //        new SqlParameter("@UserName", user.UserName),
        //        //new SqlParameter("@UserPwd", user.UserPwd),
        //        new SqlParameter("@RelUserName", user.RelUserName),
        //        new SqlParameter("@Sex", user.Sex),
        //        new SqlParameter("@License", user.License),
        //        new SqlParameter("@Telphone", user.Telphone),
        //        new SqlParameter("@Mobile", user.Mobile),
        //        new SqlParameter("@Code", user.Code)
        //    };

        //    return (new DBHelper()).GetExcuteNonQuery(tran, sql, para) > 0;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <returns></returns>
        //public static UserMDL GetUserByUserID(string userId)
        //{
        //    const string sql = "SELECT * FROM [User] WHERE UserID = @UserID";

        //    SqlDataReader dr = (new DBHelper()).GetDataReader(sql, new SqlParameter("@UserID", userId));
        //    if (dr.Read())
        //    {
        //        var user = new UserMDL
        //        {
        //            UserID = (string) Check.ConvertDBNull(dr["UserID"], SqlDbType.VarChar),
        //            OrganID = (string) Check.ConvertDBNull(dr["OrganID"], SqlDbType.VarChar),
        //            DeptID = (string) Check.ConvertDBNull(dr["DeptID"], SqlDbType.VarChar),
        //            UserName = (string) Check.ConvertDBNull(dr["UserName"], SqlDbType.VarChar),
        //            UserPwd = (string) Check.ConvertDBNull(dr["UserPwd"], SqlDbType.VarChar),
        //            RelUserName = (string) Check.ConvertDBNull(dr["RelUserName"], SqlDbType.VarChar),
        //            Sex = (string) Check.ConvertDBNull(dr["Sex"], SqlDbType.VarChar),
        //            License = (string) Check.ConvertDBNull(dr["License"], SqlDbType.VarChar),
        //            Telphone = (string) Check.ConvertDBNull(dr["Telphone"], SqlDbType.VarChar),
        //            Mobile = (string) Check.ConvertDBNull(dr["Mobile"], SqlDbType.VarChar),
        //            Code = (string) Check.ConvertDBNull(dr["Code"], SqlDbType.VarChar),
        //            OrderID = (int)Check.ConvertDBNull(dr["OrderID"], SqlDbType.Int),
        //            Dept =DepartmentDAL.GetDepartmentByDeptID((string) Check.ConvertDBNull(dr["DeptID"], SqlDbType.VarChar)),
        //            Organ =OrganizationDAL.GetOrganizationByOrganID((string) Check.ConvertDBNull(dr["OrganID"], SqlDbType.VarChar))
        //        };

        //        dr.Close();

        //        return user;
        //    }
        //    dr.Close();
        //    return null;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="license"></param>
        ///// <returns></returns>
        //public static UserMDL GetUserInfo(string license)
        //{
        //    const string sql = "SELECT * FROM [User] WHERE License = @License";

        //    SqlDataReader dr = (new DBHelper()).GetDataReader(sql, new SqlParameter("@License", license));
        //    if (dr.Read())
        //    {
        //        var user = new UserMDL
        //        {
        //            UserID = (string)Check.ConvertDBNull(dr["UserID"], SqlDbType.VarChar),
        //            OrganID = (string)Check.ConvertDBNull(dr["OrganID"], SqlDbType.VarChar),
        //            DeptID = (string)Check.ConvertDBNull(dr["DeptID"], SqlDbType.VarChar),
        //            UserName = (string)Check.ConvertDBNull(dr["UserName"], SqlDbType.VarChar),
        //            UserPwd = (string)Check.ConvertDBNull(dr["UserPwd"], SqlDbType.VarChar),
        //            RelUserName = (string)Check.ConvertDBNull(dr["RelUserName"], SqlDbType.VarChar),
        //            Sex = (string)Check.ConvertDBNull(dr["Sex"], SqlDbType.VarChar),
        //            License = (string)Check.ConvertDBNull(dr["License"], SqlDbType.VarChar),
        //            Telphone = (string)Check.ConvertDBNull(dr["Telphone"], SqlDbType.VarChar),
        //            Mobile = (string)Check.ConvertDBNull(dr["Mobile"], SqlDbType.VarChar),
        //            Code = (string)Check.ConvertDBNull(dr["Code"], SqlDbType.VarChar),
        //            OrderID = (int)Check.ConvertDBNull(dr["OrderID"], SqlDbType.Int),
        //            Dept = DepartmentDAL.GetDepartmentByDeptID((string)Check.ConvertDBNull(dr["DeptID"], SqlDbType.VarChar)),
        //            Organ = OrganizationDAL.GetOrganizationByOrganID((string)Check.ConvertDBNull(dr["OrganID"], SqlDbType.VarChar))
        //        };

        //        dr.Close();

        //        return user;
        //    }
        //    dr.Close();
        //    return null;
        //}

        //public static bool ModifyUser(DbTransaction tran, UserMDL user)
        //{
        //    string sql =
        //        "UPDATE [User] " +
        //        "SET " +
        //        "DeptID = @DeptID, " + //FK
        //        "OrganID = @OrganID, " + //FK
        //        "UserName = @UserName, " +
        //        "UserPwd ='0x'+UPPER(sys.fn_varbintohexsubstring(0, HashBytes('MD5', '" + user.UserPwd + "'), 1, 0)), " +
        //        "RelUserName = @RelUserName, " +
        //        "Sex = @Sex, " +
        //        "License = @License, " +
        //        "Telphone = @Telphone, " +
        //        "Mobile = @Mobile, " +
        //        "Code = @Code " +
        //        "WHERE UserID = @UserID";

        //    SqlParameter[] para =
        //    {
        //        new SqlParameter("@UserID", user.UserID),
        //        new SqlParameter("@DeptID", user.Dept.DeptID), //FK
        //        new SqlParameter("@OrganID", user.Organ.OrganID), //FK
        //        new SqlParameter("@UserName", user.UserName),
        //        new SqlParameter("@RelUserName", user.RelUserName),
        //        new SqlParameter("@Sex", user.Sex),
        //        new SqlParameter("@License", user.License),
        //        new SqlParameter("@Telphone", user.Telphone),
        //        new SqlParameter("@Mobile", user.Mobile),
        //        new SqlParameter("@Code", user.Code)
        //    };

        //    return (new DBHelper()).GetExcuteNonQuery(tran, sql, para) > 0;
        //}

        //public static bool ModifyUserNoUpdatePassword(DbTransaction tran, UserMDL user)
        //{
        //    string sql =
        //        "UPDATE [User] " +
        //        "SET " +
        //        "DeptID = @DeptID, " + //FK
        //        "OrganID = @OrganID, " + //FK
        //        "UserName = @UserName, " +
        //        "RelUserName = @RelUserName, " +
        //        "Sex = @Sex, " +
        //        "License = @License, " +
        //        "Telphone = @Telphone, " +
        //        "Mobile = @Mobile, " +
        //        "Code = @Code " +
        //        "WHERE UserID = @UserID";

        //    SqlParameter[] para =
        //    {
        //        new SqlParameter("@UserID", user.UserID),
        //        new SqlParameter("@DeptID", user.Dept.DeptID), //FK
        //        new SqlParameter("@OrganID", user.Organ.OrganID), //FK
        //        new SqlParameter("@UserName", user.UserName),
        //        new SqlParameter("@RelUserName", user.RelUserName),
        //        new SqlParameter("@Sex", user.Sex),
        //        new SqlParameter("@License", user.License),
        //        new SqlParameter("@Telphone", user.Telphone),
        //        new SqlParameter("@Mobile", user.Mobile),
        //        new SqlParameter("@Code", user.Code)
        //    };

        //    return (new DBHelper()).GetExcuteNonQuery(tran, sql, para) > 0;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="userName"></param>
        ///// <returns></returns>
        //public static UserMDL GetObject(string userName)
        //{
        //    const string sql = "SELECT * FROM [User] WHERE UserName = @UserName";
        //    SqlDataReader dr = (new DBHelper()).GetDataReader(sql, new SqlParameter("@UserName", userName));
        //    if (dr.Read())
        //    {
        //        var user = new UserMDL
        //        {
        //            UserID = (string)Check.ConvertDBNull(dr["UserID"], SqlDbType.VarChar),
        //            OrganID = (string)Check.ConvertDBNull(dr["OrganID"], SqlDbType.VarChar),
        //            DeptID = (string)Check.ConvertDBNull(dr["DeptID"], SqlDbType.VarChar),
        //            UserName = (string)Check.ConvertDBNull(dr["UserName"], SqlDbType.VarChar),
        //            UserPwd = (string)Check.ConvertDBNull(dr["UserPwd"], SqlDbType.VarChar),
        //            RelUserName = (string)Check.ConvertDBNull(dr["RelUserName"], SqlDbType.VarChar),
        //            Sex = (string)Check.ConvertDBNull(dr["Sex"], SqlDbType.VarChar),
        //            License = (string)Check.ConvertDBNull(dr["License"], SqlDbType.VarChar),
        //            Telphone = (string)Check.ConvertDBNull(dr["Telphone"], SqlDbType.VarChar),
        //            Mobile = (string)Check.ConvertDBNull(dr["Mobile"], SqlDbType.VarChar),
        //            Code = (string)Check.ConvertDBNull(dr["Code"], SqlDbType.VarChar),
        //            OrderID = (int)Check.ConvertDBNull(dr["OrderID"], SqlDbType.Int),
        //            Dept = DepartmentDAL.GetDepartmentByDeptID((string)Check.ConvertDBNull(dr["DeptID"], SqlDbType.VarChar)),
        //            Organ = OrganizationDAL.GetOrganizationByOrganID((string)Check.ConvertDBNull(dr["OrganID"], SqlDbType.VarChar))
        //        };

        //        dr.Close();

        //        return user;
        //    }
        //    dr.Close();
        //    return null;
        //}
        //public static UserMDL GetObjectSfzj(string License)
        //{
        //    const string sql = "SELECT * FROM [User] WHERE License = @License";
        //    SqlDataReader dr = (new DBHelper()).GetDataReader(sql, new SqlParameter("@License", License));
        //    if (dr.Read())
        //    {
        //        var user = new UserMDL
        //        {
        //            UserID = (string)Check.ConvertDBNull(dr["UserID"], SqlDbType.VarChar),
        //            OrganID = (string)Check.ConvertDBNull(dr["OrganID"], SqlDbType.VarChar),
        //            DeptID = (string)Check.ConvertDBNull(dr["DeptID"], SqlDbType.VarChar),
        //            UserName = (string)Check.ConvertDBNull(dr["UserName"], SqlDbType.VarChar),
        //            UserPwd = (string)Check.ConvertDBNull(dr["UserPwd"], SqlDbType.VarChar),
        //            RelUserName = (string)Check.ConvertDBNull(dr["RelUserName"], SqlDbType.VarChar),
        //            Sex = (string)Check.ConvertDBNull(dr["Sex"], SqlDbType.VarChar),
        //            License = (string)Check.ConvertDBNull(dr["License"], SqlDbType.VarChar),
        //            Telphone = (string)Check.ConvertDBNull(dr["Telphone"], SqlDbType.VarChar),
        //            Mobile = (string)Check.ConvertDBNull(dr["Mobile"], SqlDbType.VarChar),
        //            Code = (string)Check.ConvertDBNull(dr["Code"], SqlDbType.VarChar),
        //            OrderID = (int)Check.ConvertDBNull(dr["OrderID"], SqlDbType.Int),
        //            Dept = DepartmentDAL.GetDepartmentByDeptID((string)Check.ConvertDBNull(dr["DeptID"], SqlDbType.VarChar)),
        //            Organ = OrganizationDAL.GetOrganizationByOrganID((string)Check.ConvertDBNull(dr["OrganID"], SqlDbType.VarChar))
        //        };

        //        dr.Close();

        //        return user;
        //    }
        //    dr.Close();
        //    return null;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="userName"></param>
        ///// <returns></returns>
        //public static UserMDL GetObject(string userName, string userPwd)
        //{
        //    const string sql = "SELECT * FROM [User] WHERE UserName = '{0}' and UserPwd= '0x'+UPPER(sys.fn_varbintohexsubstring(0, HashBytes('MD5', '{1}'), 1, 0))";

        //    //SqlParameter[] para =
        //    //{
        //    //    new SqlParameter("@UserPwd", userPwd),
        //    //    new SqlParameter("@UserName", userName)
        //    //};
        //    SqlDataReader dr = (new DBHelper()).GetDataReader(string.Format(sql,userName,userPwd));
        //    if (dr.Read())
        //    {
        //        var user = new UserMDL
        //        {
        //            UserID = (string)Check.ConvertDBNull(dr["UserID"], SqlDbType.VarChar),
        //            OrganID = (string)Check.ConvertDBNull(dr["OrganID"], SqlDbType.VarChar),
        //            DeptID = (string)Check.ConvertDBNull(dr["DeptID"], SqlDbType.VarChar),
        //            UserName = (string)Check.ConvertDBNull(dr["UserName"], SqlDbType.VarChar),
        //            UserPwd = (string)Check.ConvertDBNull(dr["UserPwd"], SqlDbType.VarChar),
        //            RelUserName = (string)Check.ConvertDBNull(dr["RelUserName"], SqlDbType.VarChar),
        //            Sex = (string)Check.ConvertDBNull(dr["Sex"], SqlDbType.VarChar),
        //            License = (string)Check.ConvertDBNull(dr["License"], SqlDbType.VarChar),
        //            Telphone = (string)Check.ConvertDBNull(dr["Telphone"], SqlDbType.VarChar),
        //            Mobile = (string)Check.ConvertDBNull(dr["Mobile"], SqlDbType.VarChar),
        //            Code = (string)Check.ConvertDBNull(dr["Code"], SqlDbType.VarChar),
        //            OrderID = (int)Check.ConvertDBNull(dr["OrderID"], SqlDbType.Int),
        //            Dept = DepartmentDAL.GetDepartmentByDeptID((string)Check.ConvertDBNull(dr["DeptID"], SqlDbType.VarChar)),
        //            Organ = OrganizationDAL.GetOrganizationByOrganID((string)Check.ConvertDBNull(dr["OrganID"], SqlDbType.VarChar))
        //        };

        //        dr.Close();

        //        return user;
        //    }
        //    dr.Close();
        //    return null;
        //}


        ///// <summary>
        /////     根据部门ID获取相应人员
        ///// </summary>
        ///// <param name="deptId">部门ID</param>
        ///// <returns></returns>
        //public static DataTable GetAllUserByDeptID(string deptId)
        //{
        //    if (deptId.Trim() == "")
        //        return null;
        //    const string sql = "SELECT * FROM [User] WHERE DeptID=@DeptID Order BY UserName";
        //    SqlParameter[] para =
        //    {
        //        new SqlParameter("@DeptID", deptId)
        //    };
        //    return (new DBHelper()).GetFillData(sql, para);
        //}

        ///// <summary>
        /////     获取数据字典具体项
        ///// </summary>
        ///// <param name="organId"></param>
        ///// <param name="deptIDs"></param>
        ///// <returns></returns>
        //public static DataTable GetAllUser(string organId, string deptIDs)
        //{
        //    string sql = "select * from [User] where UserName<>'synergy' ";
        //    if (organId.Trim() != "")
        //    {
        //        sql += " and OrganID in (" + organId + ")";
        //    }
        //    if (deptIDs.Trim() != "")
        //    {
        //        sql += " and DeptID in (" + deptIDs + ")";
        //    }
        //    return (new DBHelper()).GetFillData(sql);
        //}

        ///// <summary>
        /////     根据机构ID获取相应人员
        ///// </summary>
        ///// <param name="organId">机构ID</param>
        ///// <returns></returns>
        //public static DataTable GetAllUserByOrganID(string organId)
        //{
        //    const string sql = "SELECT * FROM [User] WHERE OrganID=@OrganID Order BY OrderID";
        //    SqlParameter[] para =
        //    {
        //        new SqlParameter("@OrganID", organId)
        //    };
        //    return (new DBHelper()).GetFillData(sql, para);
        //}

        ///// <summary>
        ///// 判断当前用户是否存在
        ///// </summary>
        ///// <param name="userName"></param>
        ///// <returns></returns>
        //public static int GetCountIsUser(string userName)
        //{
        //    const string sql = "SELECT COUNT(*) FROM [User] WHERE UserName=@UserName";
        //    SqlParameter[] para =
        //    {
        //        new SqlParameter("@UserName", userName)
        //    };
        //    return (new DBHelper()).ExecuteScalar<int>(sql, para);
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="tran"></param>
        ///// <param name="userId"></param>
        ///// <returns></returns>
        //public static bool DeleteUserByUserID(DbTransaction tran, string userId)
        //{
        //    const string sql = "DELETE [User] WHERE UserID = @UserID";

        //    SqlParameter[] para =
        //    {
        //        new SqlParameter("@UserID", userId)
        //    };

        //    return (new DBHelper()).GetExcuteNonQuery(tran, sql, para) > 0;
        //}

        ///// <summary>
        ///// 修改密码
        ///// </summary>
        ///// <param name="userPwd"></param>
        ///// <param name="userId"></param>
        ///// <returns></returns>
        //public static bool ModifyUserPwd(string userPwd, string userId)
        //{
        //    const string sql = "UPDATE [User] SET UserPwd='0x'+UPPER(sys.fn_varbintohexsubstring(0, HashBytes('MD5', @UserPwd), 1, 0)) WHERE UserID=@UserID";
        //    SqlParameter[] para =
        //    {
        //        new SqlParameter("@UserPwd", SqlDbType.VarChar,36),
        //        new SqlParameter("@UserID", SqlDbType.VarChar,64)
        //    };
        //    para[0].Value = userPwd;
        //    para[1].Value = userId;
        //    return (new DBHelper()).GetExcuteNonQuery(sql, para) > 0;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="condition"></param>
        ///// <param name="orderBy"></param>
        ///// <returns></returns>
        //public static DataTable GetAllUsers(string condition, string orderBy)
        //{
        //    SqlParameter[] para =
        //    {
        //        new SqlParameter("@WhereClause", SqlDbType.VarChar, 2000),
        //        new SqlParameter("@OrderBy", SqlDbType.VarChar, 2000)
        //    };
        //    para[0].Value = condition;
        //    para[1].Value = orderBy;
        //    return (new DBHelper()).GetFillData("User_SELECT", CommandType.StoredProcedure, para);
        //}
        //#region ck添加
        ///// <summary>
        ///// 获取实体集合
        ///// </summary>
        ///// <param name="startRowIndex">开始行索引</param>
        ///// <param name="maximumRows">每页最大行</param>
        ///// <param name="filterWhereString">查询条件</param>
        ///// <param name="orderBy">排序规则</param>
        ///// <returns>DataTable</returns>
        ///// <summary>
        //public static DataTable GetList(int startRowIndex, int maximumRows, string filterWhereString, string orderBy)
        //{
        //    return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.Zws_AddUser", "*", filterWhereString, orderBy == "" ? "ID" : orderBy);
        //}
        ///// <summary>
        ///// 统计查询结果记录数
        ///// </summary>
        ///// <param name="filterWhereString">查询条件</param>
        ///// <returns>记录总行数</returns>
        //public static int SelectCount(string filterWhereString)
        //{
        //    return CommonDAL.SelectRowCount("dbo.Zws_AddUser", filterWhereString);
        //}
        //#endregion

        public UserDAL() { }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="UserOB">对象实体类</param>
        /// <returns></returns>
        public static long Insert(UserOB _UserOB)
        {
            return Insert(null, _UserOB);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="UserOB">对象实体类</param>
        /// <returns></returns>
        public static long Insert(DbTransaction tran, UserOB _UserOB)
        {
            DBHelper db = new DBHelper();

            string sql = @"
			INSERT INTO dbo.[User](OrganID,DeptID,UserName,UserPwd,RelUserName,License,Telphone,Mobile,Code)
			VALUES (@OrganID,@DeptID,@UserName,'0x'+UPPER(sys.fn_varbintohexsubstring(0, HashBytes('MD5', cast(@UserPwd as varchar(50))), 1, 0)),@RelUserName,@License,@Telphone,@Mobile,@Code);SELECT @UserID = @@IDENTITY";
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateOutParameter("UserID", DbType.Int64));
            p.Add(db.CreateParameter("OrganID", DbType.Int64, _UserOB.OrganID));
            p.Add(db.CreateParameter("DeptID", DbType.Int64, _UserOB.DeptID));
            p.Add(db.CreateParameter("UserName", DbType.String, _UserOB.UserName));
            p.Add(db.CreateParameter("UserPwd", DbType.String, _UserOB.UserPwd));
            p.Add(db.CreateParameter("RelUserName", DbType.String, _UserOB.RelUserName));
            p.Add(db.CreateParameter("License", DbType.String, _UserOB.License));
            p.Add(db.CreateParameter("Telphone", DbType.String, _UserOB.Telphone));
            p.Add(db.CreateParameter("Mobile", DbType.String, _UserOB.Mobile));
            p.Add(db.CreateParameter("Code", DbType.String, _UserOB.Code));

            int rtn = db.ExcuteNonQuery(tran, sql, p.ToArray());
            _UserOB.UserID = Convert.ToInt64(p[0].Value);
            return rtn;
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="UserOB">对象实体类</param>
        /// <returns></returns>
        public static int Update(UserOB _UserOB)
        {
            return Update(null, _UserOB);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="UserOB">对象实体类</param>
        /// <returns></returns>
        public static int Update(DbTransaction tran, UserOB _UserOB)
        {
//            string sql = string.Format(@"
//			UPDATE dbo.[User]
//				SET	OrganID = @OrganID,DeptID = @DeptID,UserName = @UserName,RelUserName = @RelUserName,License = @License,Telphone = @Telphone,Mobile = @Mobile,Code = @Code
//                ,UserPwd ='0x'+UPPER(sys.fn_varbintohexsubstring(0, HashBytes('MD5', '{0}'), 1, 0))
//			WHERE
//				UserID = @UserID", _UserOB.UserPwd);

            string sql = @"
			UPDATE dbo.[User]
				SET	OrganID = @OrganID,DeptID = @DeptID,UserName = @UserName,RelUserName = @RelUserName,License = @License,Telphone = @Telphone,Mobile = @Mobile,Code = @Code
                ,UserPwd =(case when UserPwd = cast(@UserPwd as varchar(50)) then UserPwd else '0x'+UPPER(sys.fn_varbintohexsubstring(0, HashBytes('MD5', cast(@UserPwd as varchar(50))), 1, 0)) end)
			WHERE
				UserID = @UserID";
            

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("UserID", DbType.Int64, _UserOB.UserID));
            p.Add(db.CreateParameter("OrganID", DbType.Int64, _UserOB.OrganID));
            p.Add(db.CreateParameter("DeptID", DbType.Int64, _UserOB.DeptID));
            p.Add(db.CreateParameter("UserName", DbType.String, _UserOB.UserName));
            p.Add(db.CreateParameter("UserPwd", DbType.String, _UserOB.UserPwd));
            p.Add(db.CreateParameter("RelUserName", DbType.String, _UserOB.RelUserName));
            p.Add(db.CreateParameter("License", DbType.String, _UserOB.License));
            p.Add(db.CreateParameter("Telphone", DbType.String, _UserOB.Telphone));
            p.Add(db.CreateParameter("Mobile", DbType.String, _UserOB.Mobile));
            p.Add(db.CreateParameter("Code", DbType.String, _UserOB.Code));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="UserID">主键</param>
        /// <returns></returns>
        public static int Delete(long UserID)
        {
            return Delete(null, UserID);
        }
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="UserID">主键</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, long UserID)
        {
            string sql = @"DELETE FROM dbo.[User] WHERE UserID = @UserID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("UserID", DbType.Int64, UserID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="UserOB">对象实体类</param>
        /// <returns></returns>
        public static int Delete(UserOB _UserOB)
        {
            return Delete(null, _UserOB);
        }
        /// <summary>
        /// 根据对象删除信息，用于ObjectDatasource
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="UserOB">对象实体类</param>
        /// <returns></returns>
        public static int Delete(DbTransaction tran, UserOB _UserOB)
        {
            string sql = @"DELETE FROM dbo.[User] WHERE UserID = @UserID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("UserID", DbType.Int64, _UserOB.UserID));
            return db.ExcuteNonQuery(tran, sql, p.ToArray());
        }
        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="UserID">主键</param>
        public static UserOB GetObject(long UserID)
        {
            string sql = @"
			SELECT UserID,OrganID,DeptID,UserName,UserPwd,RelUserName,License,Telphone,Mobile,Code
			FROM dbo.[User]
			WHERE UserID = @UserID";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("UserID", DbType.Int64, UserID));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    UserOB _UserOB = null;
                    if (reader.Read())
                    {
                        _UserOB = new UserOB();
                        if (reader["UserID"] != DBNull.Value) _UserOB.UserID = Convert.ToInt64(reader["UserID"]);
                        if (reader["OrganID"] != DBNull.Value) _UserOB.OrganID = Convert.ToInt64(reader["OrganID"]);
                        if (reader["DeptID"] != DBNull.Value) _UserOB.DeptID = Convert.ToInt64(reader["DeptID"]);
                        if (reader["UserName"] != DBNull.Value) _UserOB.UserName = Convert.ToString(reader["UserName"]);
                        if (reader["UserPwd"] != DBNull.Value) _UserOB.UserPwd = Convert.ToString(reader["UserPwd"]);
                        if (reader["RelUserName"] != DBNull.Value) _UserOB.RelUserName = Convert.ToString(reader["RelUserName"]);
                        if (reader["License"] != DBNull.Value) _UserOB.License = Convert.ToString(reader["License"]);
                        if (reader["Telphone"] != DBNull.Value) _UserOB.Telphone = Convert.ToString(reader["Telphone"]);
                        if (reader["Mobile"] != DBNull.Value) _UserOB.Mobile = Convert.ToString(reader["Mobile"]);
                        if (reader["Code"] != DBNull.Value) _UserOB.Code = Convert.ToString(reader["Code"]);
                    }
                    reader.Close();
                    db.Close();
                    return _UserOB;
                }
            }
            catch (Exception ex)
            {
                db.Close();
                throw ex;
            }
        }
        /// <summary>
        /// 根据登录名获取单个实体
        /// </summary>
        /// <param name="UserName">登录名</param>
        public static UserOB GetObject(string UserName)
        {
            string sql = @"
			SELECT UserID,OrganID,DeptID,UserName,UserPwd,RelUserName,License,Telphone,Mobile,Code
			FROM dbo.[User]
			WHERE UserName = @UserName";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("UserName", DbType.String, UserName));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    UserOB _UserOB = null;
                    if (reader.Read())
                    {
                        _UserOB = new UserOB();
                        if (reader["UserID"] != DBNull.Value) _UserOB.UserID = Convert.ToInt64(reader["UserID"]);
                        if (reader["OrganID"] != DBNull.Value) _UserOB.OrganID = Convert.ToInt64(reader["OrganID"]);
                        if (reader["DeptID"] != DBNull.Value) _UserOB.DeptID = Convert.ToInt64(reader["DeptID"]);
                        if (reader["UserName"] != DBNull.Value) _UserOB.UserName = Convert.ToString(reader["UserName"]);
                        if (reader["UserPwd"] != DBNull.Value) _UserOB.UserPwd = Convert.ToString(reader["UserPwd"]);
                        if (reader["RelUserName"] != DBNull.Value) _UserOB.RelUserName = Convert.ToString(reader["RelUserName"]);
                        if (reader["License"] != DBNull.Value) _UserOB.License = Convert.ToString(reader["License"]);
                        if (reader["Telphone"] != DBNull.Value) _UserOB.Telphone = Convert.ToString(reader["Telphone"]);
                        if (reader["Mobile"] != DBNull.Value) _UserOB.Mobile = Convert.ToString(reader["Mobile"]);
                        if (reader["Code"] != DBNull.Value) _UserOB.Code = Convert.ToString(reader["Code"]);
                    }
                    reader.Close();
                    db.Close();
                    return _UserOB;
                }
            }
            catch (Exception ex)
            {
                db.Close();
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static UserOB GetObject(string userName, string userPwd)
        {
            //const string sql = "SELECT * FROM [User] WHERE UserName = '{0}' and UserPwd= '0x'+UPPER(sys.fn_varbintohexsubstring(0, HashBytes('MD5', '{1}'), 1, 0))";

            ////SqlParameter[] para =
            ////{
            ////    new SqlParameter("@UserPwd", userPwd),
            ////    new SqlParameter("@UserName", userName)
            ////};
            //SqlDataReader reader = (new DBHelper()).GetDataReader(string.Format(sql, userName, userPwd));


            const string sql = "SELECT * FROM [User] WHERE UserName = @userName and UserPwd= '0x'+UPPER(sys.fn_varbintohexsubstring(0, HashBytes('MD5', cast(@UserPwd as varchar(50))), 1, 0))";

            SqlParameter[] para =
            {
                new SqlParameter("@UserPwd", userPwd),
                new SqlParameter("@UserName", userName)
            };
            SqlDataReader reader = (new DBHelper()).GetDataReader(sql, para);
            if (reader.Read())
            {
                UserOB _UserOB = new UserOB();

                if (reader["UserID"] != DBNull.Value) _UserOB.UserID = Convert.ToInt64(reader["UserID"]);
                if (reader["OrganID"] != DBNull.Value) _UserOB.OrganID = Convert.ToInt64(reader["OrganID"]);
                if (reader["DeptID"] != DBNull.Value) _UserOB.DeptID = Convert.ToInt64(reader["DeptID"]);
                if (reader["UserName"] != DBNull.Value) _UserOB.UserName = Convert.ToString(reader["UserName"]);
                if (reader["UserPwd"] != DBNull.Value) _UserOB.UserPwd = Convert.ToString(reader["UserPwd"]);
                if (reader["RelUserName"] != DBNull.Value) _UserOB.RelUserName = Convert.ToString(reader["RelUserName"]);
                if (reader["License"] != DBNull.Value) _UserOB.License = Convert.ToString(reader["License"]);
                if (reader["Telphone"] != DBNull.Value) _UserOB.Telphone = Convert.ToString(reader["Telphone"]);
                if (reader["Mobile"] != DBNull.Value) _UserOB.Mobile = Convert.ToString(reader["Mobile"]);
                if (reader["Code"] != DBNull.Value) _UserOB.Code = Convert.ToString(reader["Code"]);

                reader.Close();

                return _UserOB;

            }
            reader.Close();
            return null;
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
            return CommonDAL.GetDataTable(startRowIndex, maximumRows, "dbo.[User]", "*", filterWhereString, orderBy == "" ? " UserID" : orderBy);
        }
        /// <summary>
        /// 统计查询结果记录数
        /// </summary>
        /// <param name="filterWhereString">查询条件</param>
        /// <returns>记录总行数</returns>
        public static int SelectCount(string filterWhereString)
        {
            return CommonDAL.SelectRowCount("dbo.[User]", filterWhereString);
        }

        //根据部门ID获取相应人员
        public static DataTable GetAllUserByDeptID(long DeptID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM dbo.[User] WHERE 1=1");
            sb.Append(" AND DeptID=@DeptID");
            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("DeptID", DbType.Int64, DeptID));
            try
            {
                return db.GetFillData(sb.ToString(), p.ToArray());
            }
            catch (Exception e)
            {
                db.Close();
                Console.WriteLine(e.Message);
                throw e;
            }
        }
        //根据机构ID获取相应人员
        public static DataTable GetAllUserByOrganID(long OrganID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM dbo.[User] WHERE 1=1 AND OrganID=@OrganID order by RelUserName");
            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("OrganID", DbType.Int64, OrganID));
            try
            {
                return db.GetFillData(sb.ToString(), p.ToArray());
            }
            catch (Exception e)
            {
                db.Close();
                Console.WriteLine(e.Message);
                throw e;
            }
        }
        //获取当前用户是否存在
        public static int GetCountIsUser(string UserName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT COUNT(*) FROM dbo.[User] WHERE 1=1");
            sb.Append(" AND UserName=@UserName");
            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("UserName", DbType.String, UserName));
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(sb.ToString(), p.ToArray()));
            }
            catch (Exception e)
            {
                db.Close();
                Console.WriteLine(e.Message);
                throw e;
            }
        }


        public static UserOB GetObjectByLicense(string License)
        {
            string sql = @"
			SELECT UserID,OrganID,DeptID,UserName,UserPwd,RelUserName,License,Telphone,Mobile,Code
			FROM dbo.[User]
			WHERE License = @License";

            DBHelper db = new DBHelper();
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(db.CreateParameter("License", DbType.String, License));
            try
            {
                using (SqlDataReader reader = db.GetDataReader(sql, p.ToArray()))
                {
                    UserOB _UserOB = null;
                    if (reader.Read())
                    {
                        _UserOB = new UserOB();
                        if (reader["UserID"] != DBNull.Value) _UserOB.UserID = Convert.ToInt64(reader["UserID"]);
                        if (reader["OrganID"] != DBNull.Value) _UserOB.OrganID = Convert.ToInt64(reader["OrganID"]);
                        if (reader["DeptID"] != DBNull.Value) _UserOB.DeptID = Convert.ToInt64(reader["DeptID"]);
                        if (reader["UserName"] != DBNull.Value) _UserOB.UserName = Convert.ToString(reader["UserName"]);
                        if (reader["UserPwd"] != DBNull.Value) _UserOB.UserPwd = Convert.ToString(reader["UserPwd"]);
                        if (reader["RelUserName"] != DBNull.Value) _UserOB.RelUserName = Convert.ToString(reader["RelUserName"]);
                        if (reader["License"] != DBNull.Value) _UserOB.License = Convert.ToString(reader["License"]);
                        if (reader["Telphone"] != DBNull.Value) _UserOB.Telphone = Convert.ToString(reader["Telphone"]);
                        if (reader["Mobile"] != DBNull.Value) _UserOB.Mobile = Convert.ToString(reader["Mobile"]);
                        if (reader["Code"] != DBNull.Value) _UserOB.Code = Convert.ToString(reader["Code"]);
                    }
                    reader.Close();
                    db.Close();
                    return _UserOB;
                }
            }
            catch (Exception ex)
            {
                db.Close();
                throw ex;
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userPwd"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool ModifyUserPwd(string userPwd, string userId)
        {
            //const string sql = "UPDATE [User] SET UserPwd='0x'+UPPER(sys.fn_varbintohexsubstring(0, HashBytes('MD5', @UserPwd), 1, 0)) WHERE UserID=@UserID";

            //const string sql = "UPDATE [User] SET UserPwd= @UserPwd WHERE UserID=@UserID";

            string sql = @"UPDATE [User] 
            SET UserPwd =(case when UserPwd = cast(@UserPwd as varchar(50)) then UserPwd else '0x'+UPPER(sys.fn_varbintohexsubstring(0, HashBytes('MD5', cast(@UserPwd as varchar(50))), 1, 0)) end)
             WHERE UserID=@UserID;";
            SqlParameter[] para =
                {
                    new SqlParameter("@UserPwd", userPwd),
                    new SqlParameter("@UserID", userId)
                };
            return (new DBHelper()).GetExcuteNonQuery(sql, para) > 0;
        }
    }
}