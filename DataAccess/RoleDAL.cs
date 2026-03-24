using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Utility;

namespace DataAccess
{
    public class RoleDAL
    {
        //添加角色
        public static bool AddRole(DbTransaction tran, RoleMDL role)
        {
            var db = new DBHelper();
            string RoleID = Convert.ToString(db.ExecuteScalar<object>("select newid();")).ToUpper();
            role.RoleID = RoleID;
            string sql =
                "INSERT Role (RoleID,RoleName, Memo, OrderID)" +
                "VALUES (@RoleID,@RoleName, @Memo, @OrderID)";

            sql += " ; SELECT @@IDENTITY";

            try
            {
                var para = new SqlParameter[]
                {
                    new SqlParameter("@RoleID", role.RoleID),
                    new SqlParameter("@RoleName", role.RoleName),
                    new SqlParameter("@Memo", role.Memo),
                    new SqlParameter("@OrderID", role.OrderID)
                };

                return (new DBHelper()).GetExcuteNonQuery(tran, sql, para) > 0 ? true : false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }

        //向角色权限表中添加记录
        public static bool AddRoleResource(DbTransaction tran, string RoleID, List<string> MenuID)
        {
            string sql;
            bool del = DeleteRoleResource(tran, RoleID);
            bool add = false;
            try
            {
                if (MenuID != null)
                {
                    for (int i = 0; i < MenuID.Count; i++)
                    {
                        sql = "insert into RoleResource(RoleID,MenuID) values(@RoleID,@MenuID)";
                        var para = new SqlParameter[]
                        {
                            new SqlParameter("@RoleID", RoleID),
                            new SqlParameter("@MenuID", MenuID[i].ToString())
                        };
                        add = (new DBHelper()).GetExcuteNonQuery(tran, sql, para) > 0 ? true : false;
                    }
                }
                return add;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }

        //删除角色菜单关系
        public static bool DeleteRoleResource(DbTransaction tran, string RoleID)
        {
            string sql = "Delete From RoleResource where RoleID=@RoleID";
            try
            {
                var para = new SqlParameter[]
                {
                    new SqlParameter("@RoleID", RoleID)
                };

                return (new DBHelper()).GetExcuteNonQuery(tran, sql, para) > 0 ? true : false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }

        //删除角色
        public static bool DeleteRoleByRoleID(DbTransaction tran, string roleID)
        {
            string sql = "DELETE Role WHERE RoleID = @RoleID";

            try
            {
                var para = new SqlParameter[]
                {
                    new SqlParameter("@RoleID", roleID)
                };

                return (new DBHelper()).GetExcuteNonQuery(tran, sql, para) > 0 ? true : false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }

        public static RoleMDL GetRoleByRoleID(string roleID)
        {
            string sql = "SELECT * FROM Role WHERE RoleID = @RoleID";

            SqlDataReader dr = (new DBHelper()).GetDataReader(sql, new SqlParameter("@RoleID", roleID));
            try
            {
                if (dr.Read())
                {
                    var role = new RoleMDL();

                    role.RoleID = (string)Check.ConvertDBNull(dr["RoleID"], SqlDbType.VarChar);
                    role.RoleName = (string)Check.ConvertDBNull(dr["RoleName"], SqlDbType.VarChar);
                    role.Memo = (string)Check.ConvertDBNull(dr["Memo"], SqlDbType.VarChar);
                    role.OrderID = (int)Check.ConvertDBNull(dr["OrderID"], SqlDbType.Int);

                    dr.Close();

                    return role;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
            finally
            {
                if (dr != null)
                    dr.Close();
            }
        }

        //修改角色信息
        public static bool ModifyRole(DbTransaction tran, RoleMDL role)
        {
            string sql =
                "UPDATE Role " +
                "SET " +
                "RoleName = @RoleName, " +
                "Memo = @Memo, " +
                "OrderID = @OrderID " +
                "WHERE RoleID = @RoleID";

            try
            {
                var para = new SqlParameter[]
                {
                    new SqlParameter("@RoleID", role.RoleID),
                    new SqlParameter("@RoleName", role.RoleName),
                    new SqlParameter("@Memo", role.Memo),
                    new SqlParameter("@OrderID", role.OrderID)
                };

                return (new DBHelper()).GetExcuteNonQuery(tran, sql, para) > 0 ? true : false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }

        //获取所有角色
        public static DataTable GetAllRole()
        {
            string SQL = "SELECT * FROM Role  ORDER BY OrderID";
            try
            {
                return (new DBHelper()).GetFillData(SQL);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }

        //根据用户ID获取相应的角色
        public static DataTable GetAllUserRoleByUserID(string UserID)
        {
            string SQL =
                "SELECT UserRole.*,[Role].RoleName FROM dbo.UserRole inner join dbo.[Role] on UserRole.RoleID=[Role].RoleID WHERE UserRole.UserID=@UserID";
            try
            {
                DataTable dt = (new DBHelper()).GetFillData(SQL, new SqlParameter("@UserID", UserID));
                return dt;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }

        //根据角色名称获取该名称是否已存在
        public static int GetCountIsRoleName(string RoleName)
        {
            string sql = "SELECT COUNT(*) FROM Role WHERE RoleName=@RoleName";
            try
            {
                var para = new SqlParameter[]
                {
                    new SqlParameter("@RoleName", RoleName)
                };
                return (new DBHelper()).ExecuteScalar<int>(sql, para);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }

        //向用户角色表中添加记录
        public static bool AddUserRole(DbTransaction tran, string UserID, List<string> RoleID)
        {
            string sql;
            bool del = DeleteUserRole(tran, UserID);
            bool add = false;
            try
            {
                if (RoleID != null)
                {
                    for (int i = 0; i < RoleID.Count; i++)
                    {
                        sql = "insert into UserRole(UserID,RoleID) values(@UserID,@RoleID)";
                        var para = new SqlParameter[]
                        {
                            new SqlParameter("@UserID", UserID),
                            new SqlParameter("@RoleID", RoleID[i].ToString())
                        };
                        add = (new DBHelper()).GetExcuteNonQuery(tran, sql, para) > 0 ? true : false;
                    }
                }
                return add;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }

        public static bool DeleteUserRole(DbTransaction tran, string UserID)
        {
            string sql = "Delete From UserRole where UserID=@UserID";
            try
            {
                var para = new SqlParameter[]
                {
                    new SqlParameter("@UserID", UserID)
                };

                return (new DBHelper()).GetExcuteNonQuery(tran, sql, para) > 0 ? true : false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }


        /// <summary>
        /// 查询角色已经分配的人数
        /// </summary>
        /// <param name="RoleID">角色ID</param>
        /// <returns>已分配的人数</returns>
        public static int GetCountOfUserRoleByRoleID(long RoleID)
        {
            string sql = @"SELECT COUNT(*) FROM dbo.UserRole WHERE RoleID=@RoleID";
            try
            {
                DBHelper db = new DBHelper();
                List<SqlParameter> p = new List<SqlParameter>();
                p.Add(db.CreateParameter("RoleID", DbType.Int64, RoleID));
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