using Model;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using Utility;
using DataAccess;
using System.Text;

namespace ZYRYJG
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Session["userInfo"] == null)///Session["user"]根据个人逻辑要或不要
            {
                Session.Abandon();
                Request.Cookies.Clear();
                Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            }
        }

        //登录
        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            string loginInfo = string.Empty;
            byte[] bytes = Convert.FromBase64String(HiddenFieldLogin.Value);

            loginInfo = UTF8Encoding.UTF8.GetString(bytes);

            string[] parts = loginInfo.Split('\\');
            string username = parts[0];
            string password = parts[1];

            TextBoxUserName.Text = username;

            if (username == "")
            {
                UIHelp.layerAlert(Page, "请输入用户名！");

                return;
            }
            if (password == "")
            {
                TextBoxPassWord.Focus();
                UIHelp.layerAlert(Page, "请输入登录密码！");
                return;
            }
           
            //校验验验证码
            var httpCookie = Request.Cookies["ExamCheckCode"];
            if (httpCookie != null && String.Compare(httpCookie.Value, Request.Form["txtValidator"], StringComparison.OrdinalIgnoreCase) != 0)
            {
                txtValidator.Value = "";
                TextBoxPassWord.Focus();
                UIHelp.layerAlert(Page, "请输入正确的验证码！");
                return;
            }

             //设置验证码90秒过期，过期的不让登录
            DateTime dateout = Convert.ToDateTime(Session["timeout"]);
            if(dateout.AddSeconds(90) < DateTime.Now)
            {
                UIHelp.layerAlert(Page, "验证码已过期，请刷新验证码");
                return;
            }

            //UserOB userOb = UserDAL.GetObject(TextBoxUserName.Text.Trim());
            //if (userOb == null || userOb.UserPwd != TextBoxPassWord.Text.Trim())
            //{
            //    txtValidator.Value = "";
            //    TextBoxUserName.Focus();
            //    //Utility.CacheHelp.AddAbsoluteeExpirationCache(Page,username, username, 0.2);                
            //    UIHelp.layerAlert(Page, "登录名或密码有误！");
            //    return;
            //}

            int count =CommonDAL.SelectRowCount(string.Format("select count(*)  FROM [dbo].[OperateLog] where [LogTime] >'{0}' and [PersonID]='{1}' and [LogDetail] like '%用户名或密码错误%'"
                ,DateTime.Now.AddMinutes(-10)
                , username
                ));
            if(count >2)
            {
                DataTable tdlog = CommonDAL.GetDataTable(string.Format("select top 3 *  FROM [dbo].[OperateLog] where [LogTime] >'{0}' and [PersonID]='{1}' and [LogDetail] like '%用户名或密码错误%' order by [LogTime] desc"
                , DateTime.Now.AddMinutes(-10)
                , username
                ));
                if (Convert.ToDateTime(tdlog.Rows[0]["LogTime"]).AddMinutes(5) > DateTime.Now)
                {
                    if (Convert.ToDateTime(tdlog.Rows[0]["LogTime"]).AddMinutes(-2) < Convert.ToDateTime(tdlog.Rows[2]["LogTime"]))//2分钟内登录失败3次，锁定5分钟
                    {
                        UIHelp.layerAlert(Page, "连续3次输错密码，账户已经被锁定，请稍后再试！", "document.getElementById('ImgCode').src = 'ValidateCode.aspx?o=' + Math.random();");
                        return;
                    }
                }              
            }

            UserOB userOb = UserDAL.GetObject(username, password);

            if (userOb == null)
            {
                txtValidator.Value = "";
                TextBoxUserName.Focus();
                UIHelp.WriteOperateLog(username, username, "登录", "用户名或密码错误");
                UIHelp.layerAlert(Page, "用户名或密码错误，连续3次输错密码，账户将被锁定，请您谨慎输入。", "document.getElementById('ImgCode').src = 'ValidateCode.aspx?o=' + Math.random();");
               
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

            //string deptId = "";
            //if (userOb.Dept != null)
            //{
            //    deptId = userOb.Dept.DeptID;
            //}
            // string deptName = "";
            //if (userOb.Dept != null)
            //{
            //    deptName = userOb.Dept.DeptName;
            //}

            OrganizationOB _OrganizationOB = OrganizationDAL.GetObject(Convert.ToInt64(userOb.OrganID));
            string loginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//登录时间

            //0)用户ID,1)用户名称,2)用户所属区县,3)用户角ID色集合用“|”分割,4)用户所属机构ID,5)用户所属部门ID,6)机构名称,7)机构代码(或区县编码或身份证号码) ，8）用户最后登录时间
            string userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}"
                , userOb.UserID
                , userOb.RelUserName
                , _OrganizationOB.OrganNature=="虚拟区县"?_OrganizationOB.OrganName:"全市"
                , roleIDs.ToString()
                , userOb.OrganID
                , ""
                , _OrganizationOB.OrganCoding.Substring(0, 4) == "0108" ? _OrganizationOB.OrganName + "建委" : _OrganizationOB.OrganName//"北京市住房和城乡建设委员会"
                , _OrganizationOB.OrganCode
                , loginTime);

            //用户类型：1超级管理员；  2考生； 3企业； 4培训点；5外阜企业 ； 6行政管理机构
            string _personType = "6";
            //Session[string.Format("{0}user{1}", _personType, userOb.UserID)] = loginTime;

            userInfo = Cryptography.Encrypt(userInfo);
            FormsAuthentication.SetAuthCookie(userInfo, false);
            Session["userInfo"] = userInfo;

            string strRegex = @"^(?![a-zA-Z]+$)(?![A-Z0-9]+$)(?![A-Z\W_]+$)(?![a-z0-9]+$)(?![a-z\W_]+$)(?![0-9\W_]+$)[a-zA-Z0-9\W_]{8,16}$";
            System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex(strRegex);
            if (re.IsMatch(password) == false)
            {
                Response.Redirect("~/systemmanage/UserMaintain.aspx?o=ruopk", false);
                return;
            }
            try
            {
                UIHelp.WriteOperateLog(userOb.RelUserName, userOb.UserID.ToString(), "登录", string.Format("管理者网站登录页面登录。隶属机构：{0}", _OrganizationOB.OrganCoding.Substring(0, 4) == "0108" ? _OrganizationOB.OrganName + "建委" : _OrganizationOB.OrganName));
            }
            catch { }
            Response.Redirect("~/Default.aspx", false);
        }

       

        #region

        /// <summary>
        /// 取得客户端真实IP。如果有代理则取第一个非内网地址
        /// </summary>
        private static string IpAddress
        {
            get
            {
                string result = String.Empty;

                result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (result != null && result != String.Empty)
                {
                    //可能有代理
                    if (result.IndexOf(".") == -1)    //没有“.”肯定是非IPv4格式
                        result = null;
                    else
                    {
                        if (result.IndexOf(",") != -1)
                        {
                            //有“,”，估计多个代理。取第一个不是内网的IP。
                            result = result.Replace(" ", "").Replace("'", "");
                            string[] temparyip = result.Split(",;".ToCharArray());
                            for (int i = 0; i < temparyip.Length; i++)
                            {
                                if (IsIPAddress(temparyip[i])
                                    && temparyip[i].Substring(0, 3) != "10."
                                    && temparyip[i].Substring(0, 7) != "192.168"
                                    && temparyip[i].Substring(0, 7) != "172.16.")
                                {
                                    return temparyip[i];    //找到不是内网的地址
                                }
                            }
                        }
                        else if (IsIPAddress(result)) //代理即是IP格式
                            return result;
                        else
                            result = null;    //代理中的内容 非IP，取IP
                    }
                }

                string IpAddress = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != String.Empty) ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                if (null == result || result == String.Empty)
                    result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                if (result == null || result == String.Empty)
                    result = HttpContext.Current.Request.UserHostAddress;

                return result;
            }
        }

        #region bool IsIPAddress(str1) 判断是否是IP格式
        /**/

        /// <summary>
        /// 判断是否是IP地址格式 0.0.0.0
        /// </summary>
        /// <param name="str1">待判断的IP地址</param>
        /// <returns>true or false</returns>
        public static bool IsIPAddress(string str1)
        {
            if (str1 == null || str1 == string.Empty || str1.Length < 7 || str1.Length > 15) return false;

            string regformat = @"^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}$";

            Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);
            return regex.IsMatch(str1);
        }

        #endregion bool IsIPAddress(str1) 判断是否是IP格式

        #endregion


        //重置
        protected void Button1_Click(object sender, EventArgs e)
        {
           Response.Redirect("Login.aspx");
         
        }
    }
}