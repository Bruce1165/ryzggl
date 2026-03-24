using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;

namespace ZYRYJG
{
    public partial class ReportSelect : System.Web.UI.UserControl
    {
        //定义一个委托
        public delegate void userEvent(object sender, EventArgs arg);

        public event userEvent TextBoxReportCodeSelectChange;

        public bool AutoPostBack
        {
            get
            {
                if (this.TextBoxReportCodeSelectChange != null)
                    return true;
                else
                    return false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {            
            if (IsPostBack)
            {
                if (HiddenFieldReportCode.Value != "") TextBoxReportCode.Text = HiddenFieldReportCode.Value;
                if (Request.Params["__EVENTTARGET"] == TextBoxReportCode.UniqueID)//选择考试计划后回发处理
                {
                    this.TextBoxReportCodeSelectChange(this, e);
                }
            }
        }   
     
        /// <summary>
        /// 考试计划名称
        /// </summary>
        public string ReportCode
        {
            get
            {
                return TextBoxReportCode.Text;
            }
        }

        /// <summary>
        /// 网站根节点url
        /// </summary>
        public static string RootUrl
        {
            get
            {
                HttpContext context = HttpContext.Current;
                string executionPath = context.Request.ApplicationPath;
                return string.Format("{0}://{1}{2}", context.Request.Url.Scheme,
                context.Request.Url.Authority,
                executionPath.Length == 1 ? string.Empty : executionPath);
            }
        }

        /// <summary>
        /// 设置汇总批次号
        /// </summary>
        /// <param name="ReportCode">汇总批次号</param>
        public void SetValue(string ReportCode)
        {
            TextBoxReportCode.Text = ReportCode;           
        }

        /// <summary>
        /// 设置岗位类别
        /// </summary>
        /// <param name="PostTypeID">岗位类别ID</param>
        public void SetPostTypeID(string PostTypeID)
        {
            HiddenFieldtPostTypeID.Value = PostTypeID;
        }

    }
}