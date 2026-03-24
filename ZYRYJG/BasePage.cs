using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Utility;
using System.Data;
using DataAccess;
using System.Text;

namespace ZYRYJG
{
    public class BasePage : Page
    {        
        protected virtual void Page_Init(object sender, EventArgs e)
        {

            if (Request.IsAuthenticated)
            {
                //Title = Title + " 当前登录用户：" + UserName;
            }

            //登录交验
            if (IsNeedLogin) //需要登录
            {
                if (Request.IsAuthenticated) //已经登录了
                {
                    if (ValidatePageRight() == false)//校验权限
                    {
                        Response.Redirect("~/ResultInfoPage.aspx?o=" + Server.UrlEncode("您没有访问该页面的权限！"), false);
                        Response.End();
                    }

                    //if (Session[string.Format("{0}user{1}", (PersonType == 1 ? 6 : PersonType), UserID)] == null)
                    //{
                        
                    //    Response.Redirect("~/ResultInfoPage.aspx?o=登录授权已过时，请重新登录！", false);
                    //    System.Web.Security.FormsAuthentication.SignOut();
                    //    Response.End();
                    //}
                    //if (Session[string.Format("{0}user{1}", (PersonType == 1 ? 6 : PersonType), UserID)].ToString() != LoginTime)
                    //{

                    //    Response.Redirect("~/ResultInfoPage.aspx?o=登录授权已过时，请重新登录！", false);                       
                    //    System.Web.Security.FormsAuthentication.SignOut();
                    //    Response.End();
                    //}
                }
                else
                {
                    Response.Redirect("~/ResultInfoPage.aspx?o=" + Server.UrlEncode("登录超时，请重新登录！"), false);
                    //Response.Redirect("~/Login.aspx?o=" + Server.UrlEncode(Request.Url.PathAndQuery), false);
                    Response.End();
                    return;
                }
            }
            else
            {
                PageRightType = new List<string>();
            }
        }

        /// <summary>
        /// 检查当前访问页面级权限
        /// </summary>
        /// <returns>有权限返回Treu，否则为False</returns>
        protected virtual bool ValidatePageRight()
        {
            string[] itemURLSet = (CheckVisiteRgihtUrl == "self") ? Request.Url.AbsoluteUri.Split('|') : CheckVisiteRgihtUrl.Split('|');
            foreach (string s in itemURLSet)
            {
                string itemURL = s;
                if (itemURL.IndexOf("?") > 0) itemURL = itemURL.Remove(itemURL.IndexOf("?"));
                itemURL = itemURL.ToLower().Replace(RootUrl.ToLower() + "/", "");

                //判断页面访问权限
                if (ValidPageViewLimit(RoleIDs, itemURL) == true)
                {
                    ////获取拥有该页面内的具体控制权限
                    //List<string> resourceTypeList = ResourceBL.GetPageRight(resourceID, LoginName);
                    //if (resourceTypeList != null)
                    //{
                    //    this._PageRightType = resourceTypeList;
                    //}
                    return true;
                }
                //else
                //{
                //    return false;
                //}
            }
            return false;

        }

        ///// <summary>
        ///// 检查页面级权限
        ///// </summary>
        ///// <param name="itemURL">页面url</param>
        //protected virtual void ValidatePageRight(string itemURL)
        //{
        //    if (itemURL.IndexOf("?") > 0) itemURL = itemURL.Remove(itemURL.IndexOf("?"));
        //    itemURL = itemURL.Replace(RootUrl, "~");

        //    //判断页面访问权限
        //    if (RoleResourceDAL.ValidPageViewLimit(RoleIDs, itemURL) == false)
        //    {
        //        Response.Redirect("~/ResultInfoPage.aspx?o=" + Server.UrlEncode("您没有访问该页面的权限！"), false);
        //        Response.End();
        //        return;
        //    }
        //}

        /// <summary>
        /// 验证页面地址访问权限
        /// </summary>
        /// <param name="roleList">角色Id列表（用“|”分割）</param>
        /// <param name="url">页面url</param>
        /// <returns>true：可以访问，false：无权访问</returns>
        public Boolean ValidPageViewLimit(string roleList, string url)
        {

            string[] list = roleList.Split('|');
            foreach (string s in list)
            {
                if (RoleResourceUrlList.ContainsKey(s) && RoleResourceUrlList[s].Contains(url.ToLower()) == true)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 验证访问资源ID权限
        /// </summary>
        /// <param name="roleList">角色ID集合</param>
        /// <param name="resoureID">资源ID</param>
        /// <returns></returns>
        public Boolean ValidResourceIDLimit(string roleList, string resoureID)
        {
            string[] list = roleList.Split('|');
            foreach (string s in list)
            {
                if (RoleResourceIDList.ContainsKey(s) && RoleResourceIDList[s].Contains(resoureID) == true)
                {
                    return true;
                }
            }
            return false;
        }

        ///// <summary>
        /////     设置访问权限
        ///// </summary>
        //protected virtual void SetAccessPermissions()
        //{
        //}

        ///// <summary>
        /////     重新绑定数据
        ///// </summary>
        //protected virtual void ReBindingData()
        //{
        //}

        /// <summary>
        ///     判断是否选择复选框
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        protected bool IsSelected(RadGrid grid)
        {
            for (int i = 0; i <= grid.Items.Count - 1; i++)
            {
                if (grid.Items[i].ItemType == GridItemType.Item ||
                    grid.Items[i].ItemType == GridItemType.AlternatingItem)
                {
                    var cbox = (CheckBox)grid.Items[i].FindControl("CheckBox1");
                    if (cbox.Checked)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void CheckLogin()
        {
            if (Session["userInfo"] == null)
            {
//                string str = @"<p>录授权已过时，请重新登录！</p>
//<p><a href=""http://zjw.beijing.gov.cn"">住建委公网门户网站登录（企业、个人）</a></p>
//<p><a href=""http://172.26.67.242"">住建委政务专网门户（管理人员）</a></p>
//<p><a href=""{0}/login.aspx"">人员资格公网管理登录（管理人员）</a></p>
//";
//                Session["resultMessage"] = string.Format(str,BasePage.RootUrl);
                Response.Redirect("~/ResultInfoPage.aspx?o=登录授权已过时，请重新登录！", true);
            }
        }

        #region 变量、属性

        /// <summary>
        ///     用户ID
        /// </summary>
        public string UserID
        {
            get
            {
                CheckLogin();
                //if (Session["userInfo"] == null)
                //{                  
                //    List<Model.ResultUrl> url = new List<Model.ResultUrl>();
                //    url.Add(new Model.ResultUrl("住建委公网门户网站登录（企业、个人）", "http://zjw.beijing.gov.cn"));
                //    url.Add(new Model.ResultUrl("住建委政务专网门户（管理人员）", "http://172.26.67.242"));
                //    url.Add(new Model.ResultUrl("人员资格管理登录", "~/login.aspx"));                    
                //    UIHelp.ShowMsgAndRedirect(Page, "录授权已过时，请重新登录！", url);
                //}

                return Cryptography.Decrypt(Session["userInfo"].ToString()).Split(',')[0];
            }
        }

        /// <summary>
        ///  用户名称
        /// </summary>
        public string UserName
        {
            get
            {
                CheckLogin();
                return Cryptography.Decrypt(Session["userInfo"].ToString()).Split(',')[1];
            }
        }

        /// <summary>
        ///  用户所属区县
        /// </summary>
        public string Region
        {
            get
            {
                CheckLogin();
                return Cryptography.Decrypt(Session["userInfo"].ToString()).Split(',')[2].Replace("开发区", "").Replace("区", "").Replace("县", "");
            }
        }

        #region 从考务系统移植过来的变量

        /// <summary>
        ///   用户角ID色集合，用“|”分割。
        ///   0：个人，1:系统管理员,2:企业,3:区县业务员,4:注册中心注册管理室业务员,5:服务大厅,6:注册中心领导,7:区县领导，8：服务大厅领导，
        ///   10：注册中心考务管理室业务员，11：三类人员续期初审，12：特种作业续期初审，13：全生命周期监控，14：题库管理，15：注册中心注册管理室管理员，16：注册中心考务管理室管理员，
        ///   17：注销角色，18：服务大厅查询，20：造价师市级受理，21：造价师市级审核，22：造价师市级复核，23：造价师市级决定，25：公益教育培训管理，26：注册中心教育研究室业务员
        ///   100：集团企业，200：培训点，300：造价管理处，999：无权限
        /// </summary>
        public string RoleIDs
        {
            get
            {
                CheckLogin();
                return Cryptography.Decrypt(Session["userInfo"].ToString()).Split(',')[3];

            }
        }

        /// <summary>
        /// 判断是否是超级管理员
        /// </summary>
        /// <returns>超级管理员返回true，否则返回false</returns>
        protected bool IfSupperAdmin()
        {
            bool ifAdmin = false;//是否是超级管理员（不受限制）
            foreach (string s in RoleIDs.Split('|'))
            {
                if (s == "1")
                {
                    ifAdmin = true;
                    break;
                }
            }
            return ifAdmin;
        }

        /// <summary>
        /// 个人用户ID
        /// </summary>
        public Int64 PersonID
        {
            get
            {
                return Convert.ToInt64(UserID);
            }
        }


        /// <summary>
        /// 用户名称
        /// </summary>
        public string PersonName
        {
            get
            {
                return UserName;
            }
        }

        /// <summary>
        /// Exam系统用户类型：1超级管理员；  2考生； 3企业； 4培训点；5外阜企业 ； 6行政管理机构
        /// </summary>
        public int PersonType
        {
            get
            {
                CheckLogin();


                //重新匹配，以便适应原考务系统固定角色

                if (IfExistRoleID("0")) return 2;//考生

                if (IfExistRoleID("1")) return 1;//超级管理员

                if (IfExistRoleID("2")) return 3;//企业
                
                return 6;//默认：行政管理机构

            }
        }

        /// <summary>
        /// 单位ID
        /// </summary>
        public string UnitID
        {
            get
            {
                CheckLogin();
                return Cryptography.Decrypt(Context.User.Identity.Name).Split(',')[4];
            }
        }

        #endregion

        /// <summary>
        /// 检查登录人是否用于指定角色。
        /// 0：个人，1:系统管理员,2:企业,3:区县业务员,4:注册中心注册管理室业务员,5:服务大厅,6:注册中心领导,7:区县领导，8：服务大厅领导，
        /// 10：注册中心考务管理室业务员 ,11：三类人员续期初审  ,12：特种作业续期初审 , 13：全生命周期监控  , 14：题库管理   ,15：注册中心注册管理室管理员 ,16：注册中心考务管理室管理员 , 
        /// 17：注销角色  ,18：服务大厅查询 ，
        /// 20：造价师市级受理，21：造价师市级审核，22：造价师市级复核，23：造价师市级决定，25：公益教育培训管理，26：注册中心教育研究室业务员
        /// 100：集团企业 集团、央企 ,
        /// 200：培训点
        /// 300：造价管理处
        /// 999：无权限 ,
        /// </summary>
        /// <param name="roleid">指定角色ID</param>
        /// <returns>用于返回true，否则返回false</returns>
        public bool IfExistRoleID(string roleid)
        {
            return ("|" + RoleIDs + "|").Contains("|" + roleid +"|");
        }

        /// <summary>
        ///     用户所属机构ID
        /// </summary>
        public string OrganID
        {
            get
            {
                CheckLogin();
                return Cryptography.Decrypt(Session["userInfo"].ToString()).Split(',')[4];
            }
        }

        /// <summary>
        ///     用户所属部门ID
        /// </summary>
        public string DeptID
        {
            get
            {
                CheckLogin();
                return Cryptography.Decrypt(Session["userInfo"].ToString()).Split(',')[5];
            }
        }

        /// <summary>
        ///     机构名称
        /// </summary>
        public string OrganName
        {
            get
            {
                CheckLogin();
                return Cryptography.Decrypt(Session["userInfo"].ToString()).Split(',')[6];
            }
        }


        /// <summary>
        ///  区县编码（管理用户登录有效,非区县显示100000）
        /// </summary>
        public string RegionCode
        {
            get
            {
                CheckLogin();
                string r=Cryptography.Decrypt(Session["userInfo"].ToString()).Split(',')[7];
                return (r == "" ? "100000" : r);
            }
        }

        /// <summary>
        ///  企业组织机构代码（企业登录后有效）
        /// </summary>
        public string ZZJGDM
        {
            get
            {
                CheckLogin();
                if(IfExistRoleID("0")==true)
                {
                    string str = @"<p>录授权已过时，请重新登录！</p>
                                    <p><a href=""http://zjw.beijing.gov.cn"">住建委公网门户网站登录（企业、个人）</a></p>                                   
                                    ";
                    Session["resultMessage"] = string.Format(str, BasePage.RootUrl);
                    Response.Redirect("~/ResultInfoPage.aspx?o=登录授权已过时，请重新登录！", true);
                }
                return Cryptography.Decrypt(Session["userInfo"].ToString()).Split(',')[7];
            }
        }

        /// <summary>
        ///  社会统一信用代码（企业登录后有效）
        /// </summary>
        public string SHTJXYDM
        {
            get
            {
                CheckLogin();
                if (IfExistRoleID("0") == true)
                {
                    string str = @"<p>录授权已过时，请重新登录！</p>
                                    <p><a href=""http://zjw.beijing.gov.cn"">住建委公网门户网站登录（企业、个人）</a></p>                                   
                                    ";
                    Session["resultMessage"] = string.Format(str, BasePage.RootUrl);
                    Response.Redirect("~/ResultInfoPage.aspx?o=登录授权已过时，请重新登录！", true);
                }
                return Cryptography.Decrypt(Session["userInfo"].ToString()).Split(',')[4];
            }
        }


        /// <summary>
        ///  身份证号码（个人登录后属性有效）
        /// </summary>
        public string WorkerCertificateCode
        {
            get
            {
                CheckLogin();
                return Cryptography.Decrypt(Session["userInfo"].ToString()).Split(',')[7];
            }
        }

        /// <summary>
        /// 用户登录时间
        /// </summary>
        public string LoginTime
        {
            get
            {
                CheckLogin();
                return Cryptography.Decrypt(Session["userInfo"].ToString()).Split(',')[8];

            }
        }
        

        /// <summary>
        ///  页面是否需要登录
        /// </summary>
        protected virtual bool IsNeedLogin
        {
            get { return true; }
        }

        /// <summary>
        ///  页面权限验证url：默认验证自己self
        /// </summary>
        protected virtual string CheckVisiteRgihtUrl
        {
            get { return "self"; }
        }

        /// <summary>
        ///  页面内权限ID集合
        /// </summary>
        public List<string> PageRightType { set; get; }

        /// <summary>
        ///  网站根节点url
        /// </summary>
        public static string RootUrl
        {
            get
            {
                HttpContext context = HttpContext.Current;
                string executionPath = context.Request.ApplicationPath;
                return string.Format("{0}://{1}{2}", context.Request.Url.Scheme, context.Request.Url.Authority,
                    executionPath != null && executionPath.Length == 1 ? string.Empty : executionPath);
            }
        }

        /// <summary>
        /// 系统角色访问页面url权限列表（RoleID,UrlList）,其中UrlList格式为（url1,url2......）
        /// </summary>
        public Dictionary<string, string> RoleResourceUrlList
        {
            get
            {
                if (Cache["RoleResourceUrlList"] != null)
                {
                    return Cache["RoleResourceUrlList"] as Dictionary<string, string>;
                }
                else
                {
                    //页面url访问权限列表
                    Dictionary<string, string> _RoleResourceUrlList = new Dictionary<string, string>();
                    DataTable dt = CommonDAL.GetDataTable(0, int.MaxValue - 1, "dbo.RoleResource as r inner join dbo.Resource as m on r.MenuID = m.MenuID", "r.RoleID,m.LinkURL,r.MenuID", "and m.LinkURL is not null", "r.RoleID,r.MenuID");
                    string roleID = "";
                    StringBuilder resourceSet = new StringBuilder();
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (roleID != dr["RoleID"].ToString())
                        {
                            if (resourceSet.Length > 0)
                            {
                                resourceSet.Remove(0, 1);
                                _RoleResourceUrlList.Add(roleID, resourceSet.ToString().ToLower());
                            }
                            roleID = dr["RoleID"].ToString();
                            resourceSet.Remove(0, resourceSet.Length);
                        }
                        resourceSet.Append(",").Append(dr["LinkURL"].ToString());
                    }
                    if (resourceSet.Length > 0)
                    {
                        resourceSet.Remove(0, 1);
                        _RoleResourceUrlList.Add(roleID, resourceSet.ToString().ToLower());
                    }
                    Cache["RoleResourceUrlList"] = _RoleResourceUrlList;
                    return _RoleResourceUrlList;
                }
            }
        }

        /// <summary
        /// 系统角色访问资源ID权限列表（RoleID,RoleResourceIDList）,其中UrlList格式为（ResourceID1,ResourceID2......）
        /// </summary>
        public Dictionary<string, string> RoleResourceIDList
        {
            get
            {
                if (Cache["RoleResourceIDList"] != null)
                {
                    return Cache["RoleResourceIDList"] as Dictionary<string, string>;
                }
                else
                {
                    //资源ID访问权限列表
                    string roleID = "";
                    Dictionary<string, string> _RoleResourceIDList = new Dictionary<string, string>();
                    DataTable dt = CommonDAL.GetDataTable(0, int.MaxValue - 1, "dbo.RoleResource as r inner join dbo.Resource as m on r.MenuID = m.MenuID", "r.RoleID,m.MenuID", "", "r.RoleID,m.MenuID");
                    StringBuilder resourceSet = new StringBuilder();
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (roleID != dr["RoleID"].ToString())
                        {
                            if (resourceSet.Length > 0)
                            {
                                resourceSet.Remove(0, 1);
                                _RoleResourceIDList.Add(roleID, resourceSet.ToString());
                            }
                            roleID = dr["RoleID"].ToString();
                            resourceSet.Remove(0, resourceSet.Length);
                        }
                        resourceSet.Append(",").Append(dr["MenuID"].ToString());
                    }
                    if (resourceSet.Length > 0)
                    {
                        resourceSet.Remove(0, 1);
                        _RoleResourceIDList.Add(roleID, resourceSet.ToString());
                    }
                    Cache["RoleResourceIDList"] = _RoleResourceIDList;
                    return _RoleResourceIDList;
                }
            }
        }
        #endregion 变量、属性

    
        #region 修改密云县，延庆县，亦庄开发区的全生命周期问题
        protected static string updateregion(string Region)
        {
            if (Region == "密云县")
            {
               return  "密云区";
            }
            else if (Region == "延庆县")
            {
               return  "延庆区";
            }
            else if (Region == "亦庄开发区")
            {
               return "亦庄";
            }
            else
            {
                return Region;
            }
        }
        #endregion


        /// <summary>
        /// Grid选择行键值集合List[string]
        /// </summary>
        /// <param name="grid">列表控件</param>
        /// <returns>主键值集合</returns>
        protected List<string> GetGridSelectedKeys(RadGrid grid)
        {
            if (ViewState[string.Format("{0}_SelectedKeys", grid.ID)] == null)
                return new List<string>();
            else
                return ViewState[string.Format("{0}_SelectedKeys", grid.ID)] as List<string>;
        }

        /// <summary>
        /// 判断是否选择复选框（全部页）
        /// </summary>
        /// <param name="grid">列表控件</param>
        /// <returns></returns>
        protected bool IsGridSelected(RadGrid grid)
        {
            if (GetGridIfCheckAll(grid) == true)
            {
                return true;//全选控件为选中状态
            }
            else
            {
                List<string> list = GetGridSelectedKeys(grid);
                if (list.Count > 0)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 判断Grid是否勾选了全选CheckBox
        /// </summary>
        /// <param name="grid">列表控件</param>
        /// <returns></returns>
        protected bool GetGridIfCheckAll(RadGrid grid)
        {
            GridHeaderItem headerItem = grid.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;
            CheckAll checkAll = headerItem.Cells[0].FindControl("CheckAll1") as CheckAll;
            return checkAll.IsCheckAll;
        }

        /// <summary>
        /// 获取Grid选择行键值（int类型）集合（字符串用逗号分割）
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        protected string GetGridSelectedKeysToString(RadGrid grid)
        {
            List<string> list = GetGridSelectedKeys(grid);
            StringBuilder sb = new StringBuilder();
            foreach (string s in list)
            {
                sb.Append(",").Append(s);
            }
            if (sb.Length > 0) sb.Remove(0, 1);
            return sb.ToString();
        }

        /// <summary>
        /// 获取Grid选择行键值(string类型)集合（字符串用逗号分割）
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        protected string GetGridSelectedStrKeysToString(RadGrid grid)
        {
            List<string> list = GetGridSelectedKeys(grid);
            StringBuilder sb = new StringBuilder();
            foreach (string s in list)
            {
                sb.Append(",'").Append(s).Append("'");
            }
            if (sb.Length > 0) sb.Remove(0, 1);
            return sb.ToString();
        }

        /// <summary>
        /// 清空Grid选择行集合
        /// </summary>
        /// <param name="grid">列表控件</param>
        protected void ClearGridSelectedKeys(RadGrid grid)
        {
            ViewState[string.Format("{0}_SelectedKeys", grid.ID)] = null;
            Session["RefreshCount"] = 0;

            if (grid.MasterTableView.GetItems(GridItemType.Header).Length > 0)
            {
                GridHeaderItem headerItem = grid.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;
                CheckAll checkAll = headerItem.Cells[0].FindControl("CheckAll1") as CheckAll;
                checkAll.IniCheckAll();
            }
            System.Web.UI.WebControls.CheckBox cbox = null;
            for (int i = 0; i <= grid.Items.Count - 1; i++)
            {
                cbox = (System.Web.UI.WebControls.CheckBox)grid.Items[i].FindControl("CheckBox1");
                cbox.Checked = false;
            }
        }

        /// <summary>
        /// 更新Grid选择行集合
        /// </summary>
        /// <param name="grid">列表控件</param>
        /// <param name="keyColumName">键值列名称</param>
        protected void UpdateGridSelectedKeys(RadGrid grid, string keyColumName)
        {
            if (GetGridIfCheckAll(grid) == true)//全选控件为选中状态
            {
                ViewState[string.Format("{0}_SelectedKeys", grid.ID)] = null;
                return;
            }
            else//全选控件为未选中状态
            {
                List<string> list = GetGridSelectedKeys(grid);
                GridHeaderItem headerItem = grid.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;
                CheckAll checkAll = headerItem.Cells[0].FindControl("CheckAll1") as CheckAll;
                if (checkAll.RefreshCount == "1") list.Clear();
                //if (RefreshCount.HasValue == false)
                //{
                //    list.Clear();
                //    ((System.Web.UI.WebControls.HiddenField)checkAll.FindControl("HiddenFieldSelectAll")).Value = "";
                //}

                System.Web.UI.WebControls.CheckBox cbox = null;
                string key = "";

                if (GetGridIfCheckAllOfLastClick(grid) == true)//缓存未勾选行
                {
                    for (int i = 0; i <= grid.Items.Count - 1; i++)
                    {
                        key = grid.MasterTableView.DataKeyValues[i][keyColumName].ToString();
                        cbox = (System.Web.UI.WebControls.CheckBox)grid.Items[i].FindControl("CheckBox1");
                        if (cbox.Checked == false && list.Contains(key) == false)
                        {
                            list.Add(key);
                        }
                        else if (cbox.Checked == true && list.Contains(key) == true)
                        {
                            list.Remove(key);
                        }
                    }
                }
                else//缓存勾选行
                {
                    for (int i = 0; i <= grid.Items.Count - 1; i++)
                    {
                        key = grid.MasterTableView.DataKeyValues[i][keyColumName].ToString();
                        cbox = (System.Web.UI.WebControls.CheckBox)grid.Items[i].FindControl("CheckBox1");
                        if (cbox.Checked == true && list.Contains(key) == false)
                        {
                            list.Add(key);
                        }
                        else if (cbox.Checked == false && list.Contains(key) == true)
                        {
                            list.Remove(key);
                        }
                    }
                }
                ViewState[string.Format("{0}_SelectedKeys", grid.ID)] = list;
            }
        }

        /// <summary>
        /// 绑定Grid选择行Checkbox勾选状态
        /// </summary>
        /// <param name="grid">列表控件</param>
        /// <param name="keyColumName">键值列名称</param>
        protected void UpdateGriSelectedStatus(RadGrid grid, string keyColumName)
        {
            System.Web.UI.WebControls.CheckBox cbox = null;
            string key = "";

            if (GetGridIfCheckAll(grid) == true)//全选控件为选中状态
            {
                if (grid.Items.Count == 0)
                {
                    GridHeaderItem headerItem = grid.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;
                    CheckAll checkAll = headerItem.Cells[0].FindControl("CheckAll1") as CheckAll;
                    checkAll.IniCheckAll();
                    return;
                }
                for (int i = 0; i <= grid.Items.Count - 1; i++)
                {
                    cbox = (System.Web.UI.WebControls.CheckBox)grid.Items[i].FindControl("CheckBox1");
                    cbox.Checked = true;
                }
                return;
            }
            else//全选控件为未选中状态
            {
                List<string> list = GetGridSelectedKeys(grid);

                if (GetGridIfCheckAllOfLastClick(grid) == true)//缓存未勾选行
                {
                    for (int i = 0; i <= grid.Items.Count - 1; i++)
                    {
                        key = grid.MasterTableView.DataKeyValues[i][keyColumName].ToString();
                        cbox = (System.Web.UI.WebControls.CheckBox)grid.Items[i].FindControl("CheckBox1");
                        if (list.Count == 0 || list.Contains(key) == true)
                            cbox.Checked = false;
                        else
                            cbox.Checked = true;
                    }
                }
                else//缓存勾选行
                {
                    for (int i = 0; i <= grid.Items.Count - 1; i++)
                    {
                        key = grid.MasterTableView.DataKeyValues[i][keyColumName].ToString();
                        cbox = (System.Web.UI.WebControls.CheckBox)grid.Items[i].FindControl("CheckBox1");
                        if (list.Contains(key) == true)
                            cbox.Checked = true;
                        else
                            cbox.Checked = false;
                    }
                }
            }
        }


        /// <summary>
        /// 判断最后一次操作Grid全选控件CheckBox是否为勾选状态
        /// </summary>
        /// <param name="grid">列表控件</param>
        /// <returns></returns>
        protected bool GetGridIfCheckAllOfLastClick(RadGrid grid)
        {
            GridHeaderItem headerItem = grid.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;
            CheckAll checkAll = headerItem.Cells[0].FindControl("CheckAll1") as CheckAll;
            return checkAll.IsCheckAllOfLastClick;
        }

        /// <summary>
        /// 判断是否Grid缓存行未勾选状态
        /// </summary>
        /// <param name="grid">列表控件</param>
        /// <returns>true表示缓存未勾选行，false表示缓存勾选行</returns>
        protected bool GetGridIfSelectedExclude(RadGrid grid)
        {
            if (GetGridIfCheckAll(grid) == false && GetGridIfCheckAllOfLastClick(grid) == true)
                return true;
            else
                return false;
        }


       
    }
}