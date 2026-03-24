using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace DataAccess
{
    public class ResourceDAL
    {
        /// <summary>
        ///     获取登录帐号在页面内拥有的控制权限类型集合
        /// </summary>
        /// <param name="parentResourceID">上级资源ID</param>
        /// <returns>下级权限集合</returns>
        public static List<string> GetResource(string UserID)
        {
            var _ResourceTypeList = new List<string>();
            string SQL =
                "SELECT * FROM Resource WHERE MenuLevel=1 AND MenuID IN (SELECT MenuID FROM RoleResource WHERE RoleID IN (SELECT RoleID FROM UserRole WHERE UserID=@UserID)) Order By OrderID";
            var db = new DBHelper();
            SqlParameter[] para =
            {
                new SqlParameter("@UserID", UserID)
            };
            try
            {
                using (SqlDataReader dr = db.GetDataReader(SQL, para))
                {
                    while (dr.Read())
                    {
                        _ResourceTypeList.Add(dr["pMenuID"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                db.Close();
                throw ex;
            }
            return _ResourceTypeList;
        }

        /// <summary>
        ///     获取资源/菜单XML文件
        /// </summary>
        /// <param name="ParentID">资源父ＩＤ</param>
        /// <returns></returns>
        public static XmlDocument GetResourceXml(string ParentID, string UserID, bool IsMenu)
        {
            // 创建一个XmlDocument对象，用于载入存储信息的XML文件
            var xdoc = new XmlDocument();
            xdoc.LoadXml("<PanelBar>" + "</PanelBar>");
            XmlDeclaration xmldecl = xdoc.CreateXmlDeclaration("1.0", "utf-8", null);
            XmlElement root = xdoc.DocumentElement;
            xdoc.InsertBefore(xmldecl, root);

            CreateResourceXml(ParentID, UserID, IsMenu, xdoc, root);
            return xdoc;
        }

        private static void CreateResourceXml(string ParentID, string UserID, bool IsMenu, XmlDocument xdoc,
            XmlElement childRoot)
        {
            DataTable dt = null;
            string sql =
                "SELECT * FROM Resource WHERE IsMenu=@IsMenu AND pMenuID=@ParentID AND MenuID IN (SELECT MenuID FROM RoleResource WHERE RoleID IN (SELECT RoleID FROM UserRole WHERE UserID=@UserID)) ORDER BY OrderID";
            SqlParameter[] para =
            {
                new SqlParameter("@IsMenu", IsMenu),
                new SqlParameter("@ParentID", ParentID),
                new SqlParameter("@UserID", UserID)
            };
            dt = (new DBHelper()).GetFillData(sql, para);

            if (IsMenu)
            {
                foreach (DataRow reader in dt.Rows)
                {
                    XmlElement parentNode = xdoc.CreateElement("Item");
                    parentNode.SetAttribute("Text", reader["MenuName"].ToString());
                    parentNode.SetAttribute("Expanded", "True");
                    if (reader["LinkURL"] != DBNull.Value)
                    {
                        parentNode.SetAttribute("onclick", string.Format("setURL('{0}')", reader["LinkURL"]));
                    }
                    CreateResourceXml(Convert.ToString(reader["MenuID"]), UserID, IsMenu, xdoc, parentNode);
                    if (ParentID == "Verification")
                    {
                        SqlParameter[] para2 =
                        {
                            new SqlParameter("@UserID", UserID)
                        };
                        DataTable t = (new DBHelper()).GetFillData("p_GetVerificationCount", CommandType.StoredProcedure,
                            para2);

                        for (int i = 0; i < parentNode.ChildNodes.Count; i++)
                        {
                            string value = parentNode.ChildNodes[i].OuterXml.Split(' ')[1];
                            value = value.Substring(6, value.Length - 7);
                            if (t.Select("t='" + value + "'").Length > 0)
                            {
                                DataRow[] drs = t.Select("t='" + value + "'");
                                parentNode.InnerXml = parentNode.InnerXml.Replace(value, value + "(" + drs[0]["v"] + ")");
                            }
                        }
                    }
                    childRoot.AppendChild(parentNode);
                }
            }
            else
            {
                foreach (DataRow reader in dt.Rows)
                {
                    // 创建一个新的menuItem节点并将它添加到根节点下
                    XmlElement parentNode = xdoc.CreateElement("menuItem");
                    parentNode.SetAttribute("text", reader["MenuName"].ToString());
                    parentNode.SetAttribute("ID", reader["MenuID"].ToString());
                    // 创建一个新的menuItem节点并将它添加到根节点下
                    CreateResourceXml(Convert.ToString(reader["MenuID"]), UserID, IsMenu, xdoc, parentNode);
                }
            }
        }

        /// <summary>
        ///     获取资源/菜单XML文件
        /// </summary>
        /// <param name="ParentID">资源父ＩＤ</param>
        /// <returns></returns>
        public static XmlDocument GetResourceXmlByRoleID(string ParentID, string RoleID, bool IsMenu)
        {
            // 创建一个XmlDocument对象，用于载入存储信息的XML文件
            var xdoc = new XmlDocument();
            xdoc.LoadXml("<PanelBar>" + "</PanelBar>");
            XmlDeclaration xmldecl = xdoc.CreateXmlDeclaration("1.0", "utf-8", null);
            XmlElement root = xdoc.DocumentElement;
            xdoc.InsertBefore(xmldecl, root);

            CreateResourceXmlByRoleID(ParentID, RoleID, IsMenu, xdoc, root);
            return xdoc;
        }

        private static void CreateResourceXmlByRoleID(string ParentID, string RoleID, bool IsMenu, XmlDocument xdoc,
            XmlElement childRoot)
        {
            DataTable dt = null;
            string sql =
                "SELECT * FROM Resource WHERE IsMenu=@IsMenu AND pMenuID=@ParentID AND MenuID IN (SELECT MenuID FROM RoleResource WHERE RoleID =@RoleID) ORDER BY OrderID";
            SqlParameter[] para =
            {
                new SqlParameter("@IsMenu", IsMenu),
                new SqlParameter("@ParentID", ParentID),
                new SqlParameter("@RoleID", RoleID)
            };
            dt = (new DBHelper()).GetFillData(sql, para);

            if (IsMenu)
            {
                foreach (DataRow reader in dt.Rows)
                {
                    XmlElement parentNode = xdoc.CreateElement("Item");
                    parentNode.SetAttribute("Text", reader["MenuName"].ToString());
                    parentNode.SetAttribute("Expanded", "True");
                    if (reader["LinkURL"] != DBNull.Value)
                    {
                        parentNode.SetAttribute("onclick", string.Format("setURL('{0}')", reader["LinkURL"]));
                    }
                    CreateResourceXmlByRoleID(Convert.ToString(reader["MenuID"]), RoleID, IsMenu, xdoc, parentNode);
                    childRoot.AppendChild(parentNode);
                }
            }
            else
            {
                foreach (DataRow reader in dt.Rows)
                {
                    // 创建一个新的menuItem节点并将它添加到根节点下
                    XmlElement parentNode = xdoc.CreateElement("menuItem");
                    parentNode.SetAttribute("text", reader["MenuName"].ToString());
                    parentNode.SetAttribute("ID", reader["MenuID"].ToString());
                    // 创建一个新的menuItem节点并将它添加到根节点下
                    CreateResourceXmlByRoleID(Convert.ToString(reader["MenuID"]), RoleID, IsMenu, xdoc, parentNode);
                }
            }
        }

        //获取所有一级菜单
        public static DataTable GetAllFirstResourceMenu()
        {
            const string sql = "SELECT * FROM Resource WHERE MenuLevel=0 Order By OrderID";
            return (new DBHelper()).GetFillData(sql);
        }

        //根据一级菜单ID获取所有子菜单
        public static DataTable GetChildResourceMenuByMenuID(string MenuID)
        {
            const string sql = "SELECT * FROM Resource WHERE pMenuID=@MenuID Order By OrderID";
            SqlParameter[] para =
            {
                new SqlParameter("@MenuID", MenuID)
            };
            return (new DBHelper()).GetFillData(sql, para);
        }

        //根据角色ID获取相应权限Menu
        public static DataTable GetAllRoleResourceByRoleID(string RoleID)
        {
            const string sql = "SELECT * FROM RoleResource WHERE RoleID=@RoleID";
            SqlParameter[] para =
            {
                new SqlParameter("@RoleID", RoleID)
            };
            return (new DBHelper()).GetFillData(sql, para);
        }

        public static bool GetButtonIsVisible(string RoleID, string MenuID)
        {
            const string sql = @" SELECT * 
                                  FROM Resource 
                                  WHERE MenuID IN (SELECT MenuID 
                                                   FROM RoleResource 
                                                   WHERE charindex(RoleID,@RoleID)>0) 
                                        AND MenuID=@MenuID";
            SqlParameter[] para =
            {
                new SqlParameter("@RoleID", RoleID),
                new SqlParameter("@MenuID", MenuID)
            };
            return (new DBHelper()).GetFillData(sql, para).Rows.Count > 0;
        }

        //获取机构人员权限
        public static DataTable GetUserResourceOfOrgan(string OrganID)
        {
            string sql = @"SELECT U.USERID,U.RELUSERNAME,S.[MenuID]
                            FROM   DBO.USERROLE AS UR 
                            INNER JOIN  DBO.[USER] AS U ON UR.USERID=U.USERID
                            INNER JOIN DBO.[ROLE] AS R ON UR.ROLEID=R.ROLEID
                            INNER JOIN DBO.ROLERESOURCE AS RS ON R.ROLEID =RS.ROLEID
                            INNER JOIN DBO.[RESOURCE] AS S ON RS.[MenuID]=s.[MenuID]
                            Where OrganID={0}
                            ORDER BY U.ORGANID,U.RELUSERNAME ,S.[pMenuID],S.OrderID;";
            try
            {
                DBHelper db = new DBHelper();
                return db.GetFillData(string.Format(sql, OrganID));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }

        //获取角色权限列表
        public static DataTable GetRoleResource()
        {
            string sql = @"SELECT U.[RoleID],U.[RoleName],S.[MenuID]
                            FROM   [dbo].[RoleResource] AS UR 
                            INNER JOIN  [dbo].[Role] AS U ON UR.[RoleID]=U.[RoleID]
                            INNER JOIN DBO.[ROLE] AS R ON UR.ROLEID=R.ROLEID
                            INNER JOIN DBO.ROLERESOURCE AS RS ON R.ROLEID =RS.ROLEID
                            INNER JOIN DBO.[RESOURCE] AS S ON RS.[MenuID]=s.[MenuID]
                            ORDER BY U.[OrderID] ,S.[pMenuID],S.OrderID;";
            try
            {
                DBHelper db = new DBHelper();
                return db.GetFillData(sql);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }

        #region 试验菜单
        /// <summary>
        ///     获取资源/菜单UL文件
        /// </summary>
        /// <param name="ParentID">资源父ＩＤ</param>
        /// <returns></returns>
        public static XmlDocument GetResourceUl(string ParentID, string UserID, bool IsMenu)
        {
            // 创建一个XmlDocument对象，用于载入存储信息的XML文件
            var xdoc = new XmlDocument();
            xdoc.LoadXml("<div  class=\"menu\">" + "</div>");
            XmlElement root = xdoc.DocumentElement;

            CreateResourceUl(ParentID, UserID, IsMenu, xdoc, root);
            return xdoc;
        }

        private static int CreateResourceUl(string ParentID, string UserID, bool IsMenu, XmlDocument xdoc,
            XmlElement childRoot)
        {
            DataTable dt = null;
            string sql =
                "SELECT * FROM Resource WHERE IsMenu=@IsMenu AND pMenuID=@ParentID AND MenuID IN (SELECT MenuID FROM RoleResource WHERE RoleID IN (SELECT RoleID FROM UserRole WHERE UserID=@UserID)) ORDER BY OrderID";
            SqlParameter[] para =
            {
                new SqlParameter("@IsMenu", IsMenu),
                new SqlParameter("@ParentID", ParentID),
                new SqlParameter("@UserID", UserID)
            };
            dt = (new DBHelper()).GetFillData(sql, para);
            int childCount = 0;//子菜单数量
            if (IsMenu && dt.Rows.Count > 0)
            {

                XmlElement ulNode = xdoc.CreateElement("ul");
                childRoot.AppendChild(ulNode);

                foreach (DataRow reader in dt.Rows)
                {
                    XmlElement parentNode = xdoc.CreateElement("li");

                    parentNode.InnerXml = string.Format("<a  {0}><span class=\"arrowUp1\"></span><span class=\"arrowUp2\"></span>{1}</a>"
                      , reader["LinkURL"] == DBNull.Value ? "href=\"#none\"" : string.Format("href=\"#none\" onclick=\"setURL('{0}')\"", reader["LinkURL"])
                      , reader["MenuName"]
                      );
                    childCount = CreateResourceUl(Convert.ToString(reader["MenuID"]), UserID, IsMenu, xdoc, parentNode);

                    if (childCount > 0) parentNode.SetAttribute("class", "inactive");

                    ulNode.AppendChild(parentNode);
                }
            }
            return dt.Rows.Count;

        }

        /// <summary>
        ///     获取资源/菜单UL文件
        /// </summary>
        /// <param name="ParentID">资源父ＩＤ</param>
        /// <returns></returns>
        public static XmlDocument GetResourceULByRoleID(string ParentID, string RoleID, bool IsMenu)
        {
            // 创建一个XmlDocument对象，用于载入存储信息的XML文件
            var xdoc = new XmlDocument();
            xdoc.LoadXml("<div  class=\"menu\">" + "</div>");
            XmlElement root = xdoc.DocumentElement;
            CreateResourceULByRoleID(ParentID, RoleID, IsMenu, xdoc, root);
            return xdoc;
        }

        private static int CreateResourceULByRoleID(string ParentID, string RoleID, bool IsMenu, XmlDocument xdoc,
            XmlElement childRoot)
        {
            DataTable dt = null;
             string sql ="";
            string[] roleList = RoleID.Split('|');
            if (roleList.Length == 1)
            {
                sql = "SELECT * FROM Resource WHERE IsMenu=@IsMenu AND pMenuID=@ParentID AND MenuID IN (SELECT MenuID FROM RoleResource WHERE RoleID =@RoleID) ORDER BY OrderID";
            }
            else
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach(string s in roleList)
                {
                    sb.Append(" or RoleID =").Append(s);
                }
                if(sb.Length>0)
                {
                    sb.Remove(0, 3);                    
                }
                sql = string.Format("SELECT * FROM Resource WHERE IsMenu=@IsMenu AND pMenuID=@ParentID AND MenuID IN (SELECT MenuID FROM RoleResource WHERE {0}) ORDER BY OrderID",sb);
            }

            SqlParameter[] para =
            {
                new SqlParameter("@IsMenu", IsMenu),
                new SqlParameter("@ParentID", ParentID),
                new SqlParameter("@RoleID", RoleID)
            };
            dt = (new DBHelper()).GetFillData(sql, para);
            int childCount = 0;//子菜单数量
            if (IsMenu && dt.Rows.Count > 0)
            {

                XmlElement ulNode = xdoc.CreateElement("ul");
                childRoot.AppendChild(ulNode);

                foreach (DataRow reader in dt.Rows)
                {
                    XmlElement parentNode = xdoc.CreateElement("li");

                    if (reader["MenuName"].ToString() == "我的培训")
                    {
                        parentNode.InnerXml = string.Format("<a href=\"{0}\" Target=\"_blank\"><span class=\"arrowUp1\"></span><span class=\"arrowUp2\"></span>{1}</a>"
                         , reader["LinkURL"], reader["MenuName"]);
                    }
                    else
                    {
                        parentNode.InnerXml = string.Format("<a  {0}><span class=\"arrowUp1\"></span><span class=\"arrowUp2\"></span>{1}</a>"
                         , reader["LinkURL"] == DBNull.Value ? "href=\"#none\"" : string.Format("href=\"#none\" onclick=\"setURL('{0}')\"", reader["LinkURL"])
                         , reader["MenuName"]
                         );
                    }

                    childCount = CreateResourceULByRoleID(Convert.ToString(reader["MenuID"]), RoleID, IsMenu, xdoc, parentNode);
                    if (childCount > 0)
                    {
                        parentNode.SetAttribute("class", "inactive");
                    }
                    ulNode.AppendChild(parentNode);
                }
            }

            return dt.Rows.Count;
        }

        #endregion
    }
}