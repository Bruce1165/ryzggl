using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;

namespace ZYRYJG
{
    public partial class SelectSource : System.Web.UI.UserControl
    {

        //定义一个委托
        public delegate void userEvent(object sender, EventArgs arg);

        public event userEvent SourceSelectChange;

        public bool AutoPostBack
        {
            get
            {
                if (this.SourceSelectChange != null)
                    return true;
                else
                    return false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (HiddenFieldSourceID.Value != "" && HiddenFieldSourceID.Value != "undefined")
                {
                    SourceMDL _SourceMDL = SourceDAL.GetObject(Convert.ToInt64(HiddenFieldSourceID.Value));
                    if (_SourceMDL != null)
                    {
                        SetValue(_SourceMDL);
                    }
                }
                else if (HiddenFieldSourceID.Value != "" && ViewState["SelectSource"] != null)
                {
                    SetValue(ViewState["SelectSource"] as SourceMDL);
                }
                else if (HiddenFieldSourceID.Value == "")
                {
                    TextBoxSource.Text = "";
                    HiddenFieldSourceID.Value = "";
                    ViewState["SelectSource"] = null;
                }

                if (Request.Params["__EVENTTARGET"] == TextBoxSource.UniqueID)//选择课程后回发处理
                {
                    this.SourceSelectChange(this, e);
                }
            }
        }

        /// <summary>
        /// 考试ID
        /// </summary>
        public Int64? SourceID
        {
            get
            {
                if (HiddenFieldSourceID.Value == "")
                    return null;
                else
                    return Convert.ToInt64(HiddenFieldSourceID.Value);
            }
        }

        /// <summary>
        /// 考试计划名称
        /// </summary>
        public string SourceName
        {
            get
            {
                return TextBoxSource.Text;
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
        /// 为控件赋值
        /// </summary>
        /// <param name="ob">考试计划OB</param>
        public void SetValue(SourceMDL ob)
        {
            ViewState["SelectSource"] = ob;
            TextBoxSource.Text = ob.SourceName;
            HiddenFieldSourceID.Value = ob.SourceID.ToString();

        }
    }

}