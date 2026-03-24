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
    public partial class SelectImg : System.Web.UI.UserControl
    {

        ////定义一个委托
        //public delegate void userEvent(object sender, EventArgs arg);

        //public event userEvent SourceSelectChange;

        //public bool AutoPostBack
        //{
        //    get
        //    {
        //        if (this.SourceSelectChange != null)
        //            return true;
        //        else
        //            return false;
        //    }
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                //if (HiddenFieldSourceImg.Value != "")
                //{
                //    SourceMDL _SourceMDL = SourceDAL.GetObject(Convert.ToInt64(HiddenFieldSourceImg.Value));
                //    if (_SourceMDL != null)
                //    {
                //        SetValue(_SourceMDL);
                //    }
                //}
                //if (Request.Params["__EVENTTARGET"] == TextBoxSourceImg.UniqueID)//选择课程后回发处理
                //{
                //    this.SourceSelectChange(this, e);
                //}
            }
        }


        /// <summary>
        /// 课程背景图片
        /// </summary>
        public string SourceImg
        {
            get
            {
                return HiddenFieldSourceImg.Value;
            }
            set
            {
                SetValue(value);
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
        /// <param name="_SourceImg">课程背景图片</param>
        public void SetValue(string _SourceImg)
        {
            HiddenFieldSourceImg.Value = _SourceImg;
            if(_SourceImg=="")
            {
                divSourceImg.Style.Add("background","none");
            }
            else
            {
                divSourceImg.Style.Add("background", string.Format("url(../Images/jz/{0})", _SourceImg));
                divSourceImg.Style.Add("background-size", "cover");
            }
        }
    }

}