using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utility;
using DataAccess;
using System.Data;
using System.Text;
using Model;
using DataAccess;

/// <summary>
/// BasePage 的摘要说明
/// </summary>
public class BasePage : System.Web.UI.Page
{
    public BasePage()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

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
                    Response.Redirect("https://bjt.beijing.gov.cn/renzheng/open/login/goUserLogin?client_id=100100000502&redirect_uri=http://120.52.185.14/loginYZT.aspx&response_type=code&scope=user_info&state=" , false);
                    Response.End();
                }
            }
            else
            {
                Response.Redirect("https://bjt.beijing.gov.cn/renzheng/open/login/goUserLogin?client_id=100100000502&redirect_uri=http://120.52.185.14/loginYZT.aspx&response_type=code&scope=user_info&state=", false);
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
                return true;
            }
        }
        return false;

    }

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
                DataTable dt = CommonDAL.GetDataTable("select r.RoleID,m.LinkURL,r.MenuID from dbo.RoleResource as r inner join dbo.Resource as m on r.MenuID = m.MenuID where m.LinkURL is not null order by r.RoleID,r.MenuID");
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
	

    private void CheckLogin()
    {
        if (Session["userInfo"] == null)
        {
            Response.Redirect("https://bjt.beijing.gov.cn/renzheng/open/login/goUserLogin?client_id=100100000502&redirect_uri=http://120.52.185.14/loginYZT.aspx&response_type=code&scope=user_info&state=", false);
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
    ///   17：注销角色，18：服务大厅查询，20：造价师市级受理，21：造价师市级审核，22：造价师市级复核，23：造价师市级决定，100：集团企业，999：无权限
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

    #endregion

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
            string r = Cryptography.Decrypt(Session["userInfo"].ToString()).Split(',')[7];
            return (r == "" ? "100000" : r);
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


    #endregion 变量、属性

    /// <summary>
    /// 网站点击量
    /// </summary>
    protected long ClickCount
    {
        get
        {
            if (Cache["jxjyClickCount"] == null)
            {
                TJ_VisitMDL tj = TJ_VisitDAL.GetObject(Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")));
                if (tj == null)
                {
                    tj = new TJ_VisitMDL();
                    tj.TJ_Year = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
                    tj.TJ_Count = 1;
                    TJ_VisitDAL.Insert(tj);
                    Utility.CacheHelp.AddAbsoluteeExpirationCache(Page, "jxjyClickCount", tj.TJ_Count, 2);
                }

                return tj.TJ_Count.Value;
            }
            else
            {
                return Convert.ToInt64(Cache["jxjyClickCount"]);
            }
        }
        set
        {
            TJ_VisitMDL tj = TJ_VisitDAL.GetObject(Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")));
            if (tj == null)
            {
                tj = new TJ_VisitMDL();
                tj.TJ_Year = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
                tj.TJ_Count = 1;
                TJ_VisitDAL.Insert(tj);
            }

            if (value > tj.TJ_Count)
            {
                tj.TJ_Count = value;
                TJ_VisitDAL.Update(tj);
            }
            Utility.CacheHelp.AddAbsoluteeExpirationCache(Page, "jxjyClickCount", tj.TJ_Count, 2);
        }
    }

    /// <summary>
    /// 缓存、尚未保存的网站点击量
    /// </summary>
    protected long CachejxjyClickCount
    {
        get
        {
            if (Cache["CachejxjyClickCount"] == null)
                return 1;
            else
                return Convert.ToInt64(Cache["CachejxjyClickCount"]);
        }
        set
        {
            if (value > 20)//存储超过20次更新一次数据库
            {
                ClickCount = ClickCount + value;
                Cache["CachejxjyClickCount"] = 0;
            }
            else
            {
                Cache["CachejxjyClickCount"] = value;
            }
        }
    }
}