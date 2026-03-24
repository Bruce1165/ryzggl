using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetCasClient.Utils;
using System.Web.Security;
using DotNetCasClient;
using System.Text;
using Model;
using DataAccess;
using System.Data;
using Utility;


namespace ZYRYJG
{
    public partial class Login1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //FormsAuthenticationTicket formsAuthenticationTicket = CasAuthentication.GetFormsAuthenticationTicket();
            //if (formsAuthenticationTicket == null)
            //{
            //    StringBuilder buffer = new StringBuilder();
            //    if (!(CasAuthentication.ServerName.StartsWith("https://") || CasAuthentication.ServerName.StartsWith("http://")))
            //    {
            //        buffer.Append(Request.IsSecureConnection ? "https://" : "http://");
            //    }
            //    buffer.Append(CasAuthentication.ServerName);

            //    EnhancedUriBuilder ub = new EnhancedUriBuilder(buffer.ToString());
            //    ub.Path = Request.Url.AbsolutePath;
            //    string url = ub.Uri.AbsoluteUri;
            //    Response.Redirect(url);
            //    FileLog.WriteLog("没有Cookie信息" + url);
            //    return;
            //}
            try
            {
                if (Session["userInfo"] != null)
                {

                    Response.Redirect("~/default.aspx", false);
                    return;
                }
                HttpCookie ticketCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                string idCard = ""; //身份证号
                if (ticketCookie != null)
                {
                    if (!string.IsNullOrEmpty(ticketCookie.Value))
                    {
                        //可以从dictionary获取更多的用户信息
                        System.Collections.Generic.IDictionary<string, string> dictionary = UserInfoUtil.getUserInfo(ticketCookie.Value);
                        idCard = dictionary[UserInfoUtil.IDCard];
                    }
                }
                string SFZHM = idCard;
                UserOB userOb = UserDAL.GetObjectByLicense(SFZHM);
                if (userOb == null)
                {
                    FormsAuthentication.SignOut();
                    Response.Redirect("~/ResultInfoPage.aspx?o=您暂时没有该系统的权限，请与系统管理员联系！", false);
                    return;
                }
                //角色ID集合
                var roleIDs = new System.Text.StringBuilder();
                DataTable dt = RoleDAL.GetAllUserRoleByUserID(userOb.UserID.ToString());
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    roleIDs.Append("|").Append(dt.Rows[i]["RoleID"]);
                }
                if (roleIDs.Length > 0) roleIDs.Remove(0, 1);

                string deptId = "";
                if (userOb.DeptID.HasValue)
                {
                    deptId = userOb.DeptID.ToString();
                }

                OrganizationOB  _OrganizationOB = OrganizationDAL.GetObject(Convert.ToInt64(userOb.OrganID));
                string loginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//登录时间
                //string deptName = "";
                //if (userOb.Dept != null)
                //{
                //    deptName = userOb.Dept.DeptName;
                //}
                //0)用户ID,1)用户名称,2)用户所属区县,3)用户角ID色集合用“|”分割,4)用户所属机构ID,5)用户所属部门ID,6)机构名称,7)机构代码(或区县编码或身份证号码) ，8）用户最后登录时间
                string userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}"
                    , userOb.UserID
                    , userOb.RelUserName
                    , _OrganizationOB.OrganNature == "虚拟区县" ? _OrganizationOB.OrganName : "全市"
                    , roleIDs.ToString()
                    , userOb.OrganID
                    , ""
                    , _OrganizationOB.OrganCoding.Substring(0, 4) == "0108" ? _OrganizationOB.OrganName + "建委" : _OrganizationOB.OrganName
                    , _OrganizationOB.OrganCode
                    , loginTime);

                //用户类型：1超级管理员；  2考生； 3企业； 4培训点；5外阜企业 ； 6行政管理机构
                string _personType = "6";
                //Session[string.Format("{0}user{1}", _personType, userOb.UserID)] = loginTime;
                //Utility.CacheHelp.AddAbsoluteeExpirationCache(Page, string.Format("{0}user{1}", _personType, userOb.UserID), loginTime, 10);

                userInfo = Cryptography.Encrypt(userInfo);

                System.Threading.Thread.Sleep(2000);
                //FormsAuthentication.SetAuthCookie(userInfo, false);

                Session["userInfo"] = userInfo;

                //HttpCookie mycookie = new HttpCookie("ZYRYJG_local", userInfo);
                //mycookie.Expires = DateTime.Now.AddHours(8d);
                //Response.Cookies.Add(mycookie);



                //HttpCookie cookie = FormsAuthentication.GetAuthCookie(userInfo, false);
                //FormsAuthenticationTicket oldTicket = FormsAuthentication.Decrypt(cookie.Value);
                //FormsAuthenticationTicket newTicket = new FormsAuthenticationTicket(1,
                //oldTicket.Name,
                //oldTicket.IssueDate,
                //DateTime.Now.AddMinutes(30),
                //oldTicket.IsPersistent,
                //oldTicket.UserData,
                //FormsAuthentication.FormsCookiePath);
                //cookie.Domain = "synergy";
                //cookie.Value = FormsAuthentication.Encrypt(newTicket);
                //HttpContext.Current.Response.Cookies.Add(cookie);

                try
                {
                    UIHelp.WriteOperateLog(userOb.RelUserName, userOb.UserID.ToString(), "登录", string.Format("管理者专网单点统一登录。隶属机构：{0}",_OrganizationOB.OrganCoding.Substring(0, 4) == "0108" ? _OrganizationOB.OrganName + "建委" : _OrganizationOB.OrganName));
                }
                catch { }

                Response.Redirect("~/default.aspx", false);
                //FileLog.WriteLog("登录成功");
             
            }
            catch (Exception ex)
            {
                //UIHelp.WriteErrorLog(Page, "单点统一登录失败", ex);
                //Response.Redirect("Login.aspx", false);
                Response.Redirect("~/ResultInfoPage.aspx?o=" + Server.UrlEncode("系统忙，专网单点统一登录失败！"), false);
                FileLog.WriteLog("专网单点统一登录失败", ex);
            }

        }
    }
}