using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace ZYRYJG
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

            if (Request.RawUrl.ToLower().Contains("systemmanage/sysupdate.aspx") == true)
            {
                return;
            }
            if (Request.RequestType.ToUpper() == "POST")
            {

                if (CheckSQLParam() == false)
                {
                    System.Web.HttpContext.Current.Response.Write("<script>alert('输入信息存在非法字符！');</script>");
                    System.Web.HttpContext.Current.Response.End();

                }
            }
          

        }

        //Sql注入时,可能出现的sql关键字,可根据自己的实际情况进行初始化,每个关键字由'|'分隔开来
        //private const string StrKeyWord = @"select|insert|delete|from|count(|drop table|update|truncate|asc(|mid(|char(|xp_cmdshell|exec master|netlocalgroup administrators|:|net user|""|or|and";
        private const string StrKeyWord = @"select|insert|delete|from|drop table|update|truncate|exec master|netlocalgroup administrators|:|net user|and|eval|exec|shell_exec|base64_decode|proc_open|curl|curl_exec|fopen|fwrite";
        //private const string StrKeyWord = @"select|insert|delete";
        //Sql注入时,可能出现的特殊符号,,可根据自己的实际情况进行初始化,每个符号由'|'分隔开来
        //private const string StrRegex = @"-|;|,|/|(|)|[|]|}|{|%|@|*|!|'";
        private const string StrRegex = @"--|'|%";


        /// <summary>
        /// 检查URL参数中是否带有SQL注入的可能关键字。
        /// </summary>
        /// <returns>存在SQL注入关键字时返回 true，否则返回 false</returns>
        public static bool CheckRequestQuery()
        {
            bool result = false;
            if (HttpContext.Current.Request.QueryString.Count != 0)
            {
                //若URL中参数存在，则逐个检验参数。
                foreach (string queryName in HttpContext.Current.Request.QueryString)
                {
                    //过虑一些特殊的请求状态值,主要是一些有关页面视图状态的参数
                    if (queryName == "__VIEWSTATE" || queryName == "__EVENTVALIDATION")
                        continue;
                    //开始检查请求参数值是否合法
                    if (CheckKeyWord(HttpContext.Current.Request.QueryString[queryName]))
                    {
                        //只要存在一个可能出现Sql注入的参数,则直接退出
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 检查提交表单中是否存在SQL注入的可能关键字
        /// </summary>
        /// <returns>存在SQL注入关键字时返回 true，否则返回 false</returns>
        public static bool CheckRequestForm()
        {
            //return false;
            bool result = false;
            if (HttpContext.Current.Request.Form.Count > 0)
            {
                //若获取提交的表单项个数不为0,则逐个比较参数
                foreach (string queryName in HttpContext.Current.Request.Form)
                {
                    //过虑一些特殊的请求状态值,主要是一些有关页面视图状态的参数
                    if (queryName == "__VIEWSTATE" || queryName == "__EVENTVALIDATION" || queryName == null)
                        continue;

                    if (queryName.IndexOf("RadTextBox") == -1
                        && queryName.IndexOf("TextBox") == -1
                        && queryName.IndexOf("rdtxt") == -1
                        && queryName.IndexOf("RadTxt") == -1

                        )
                    {
                        continue;
                    }
                    //开始检查提交的表单参数值是否合法
                    if (CheckKeyWord(HttpContext.Current.Request.Form[queryName].ToLower()))
                    {
                        //只要存在一个可能出现Sql注入的参数,则直接退出
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 检查_sword是否包涵SQL关键字
        /// </summary>
        /// <param name="_sWord">需要检查的字符串</param>
        /// <returns>存在SQL注入关键字时返回 true，否则返回 false</returns>
        public static bool CheckKeyWord(string _sWord)
        {
            if (_sWord == "") return false;
            bool result = false;
            //模式1 : 对应Sql注入的可能关键字
            string[] patten1 = StrKeyWord.Split('|');
            //模式2 : 对应Sql注入的可能特殊符号
            string[] patten2 = StrRegex.Split('|');
            //开始检查 模式1:Sql注入的可能关键字 的注入情况
            foreach (string sqlKey in patten1)
            {
                if (_sWord.IndexOf(" " + sqlKey) >= 0 || _sWord.IndexOf(sqlKey + " ") >= 0)
                {
                    //只要存在一个可能出现Sql注入的参数,则直接退出
                    System.Web.HttpContext.Current.Response.Write(string.Format("<script>alert('输入信息存在非法字符【{0}】');</script>",sqlKey));
                    result = true;
                    break;
                }
            }
            //开始检查 模式1:Sql注入的可能特殊符号 的注入情况
            foreach (string sqlKey in patten2)
            {
                if (_sWord.IndexOf(sqlKey) >= 0)
                {
                    //只要存在一个可能出现Sql注入的参数,则直接退出
                     System.Web.HttpContext.Current.Response.Write(string.Format("<script>alert('输入信息存在非法字符【{0}】');</script>",sqlKey));
                    result = true;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// 执行Sql注入验证
        /// </summary>
        /// <returns>返回false表示有非法字符，返回true表示无sql注入</returns>
        public static bool CheckSQLParam()
        {
            if (CheckRequestQuery() || CheckRequestForm())
            {
                return false;
            }
            return true;
        }
    }
}