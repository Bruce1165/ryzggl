using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string userInfo = Request["o"];
            FormsAuthentication.SetAuthCookie(userInfo, false);
            Session["userInfo"] = userInfo;

            Response.Redirect(string.Format("~/mFram.aspx?y={0}", Request["y"]), false);
            //Response.Redirect(string.Format("~/mFram.html?y={0}", Request["y"]), false);
        }

    }
}
