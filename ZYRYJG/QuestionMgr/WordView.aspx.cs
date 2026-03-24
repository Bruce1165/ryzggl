using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZYRYJG.QuestionMgr
{
    /// <summary>
    /// 在线编辑word处理页面
    /// 传入实例：Response.Redirect("http://localhost:55886/QuestionMgr/WordView.aspx?o=http://localhost:55886/11.doc&s=http://localhost:55886/QuestionMgr/WordWrite.aspx");
    /// 传入参数
    /// 1：u=要编辑的word所在地址url
    /// 2：s=要处理接受保存修改返回word的处理页面地址url
    ///    处理页面代码写法参考
    ///    protected void Page_Load(object sender, EventArgs e)
    ///    {
    ///        if (Request.Files.Count > 0)
    ///        {
    ///            HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
    ///            files[0].SaveAs(Server.MapPath(string.Format("{0}.doc", DateTime.Now.ToString("yyMMddHHmmss"))));               
    ///        }            
    ///    }
    /// 
    /// </summary>
    public partial class WordView : System.Web.UI.Page
    {

        /// <summary>
        /// 回传保存处理页面地址
        /// </summary>
        protected string SaveUrl = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request["s"]) == false)
                {
                    SaveUrl = Request["s"];
                }
            }
        }

    }
}