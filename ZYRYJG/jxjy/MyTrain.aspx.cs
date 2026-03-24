using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZYRYJG.jxjy
{
    public partial class MyTrain : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Form.Target = "_blank";

                if (RootUrl.ToLower().Contains("http://120.52.185.14") == false)
                {
                    //Response.Write("<div style='line-height:400%;font-size:30px;text-align:center'>功能升级维护中...<div>");
                    Response.Redirect(string.Format("http://localhost:1045/index.aspx?o={0}&y={1}", Session["userInfo"],Request["o"]));
                }
                else
                {
                    Response.Redirect(string.Format("http://110.43.204.156/index.aspx?o={0}&y={1}", Session["userInfo"], Request["o"]));

                    //Response.Write("<div style='line-height:400%;font-size:30px;text-align:center'>功能升级维护中...<div>");
                }
            }
        }
    }
}