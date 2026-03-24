using System;

namespace ZYRYJG
{
    public partial class ResultInfoPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["resultMessage"] != null)
            {
                LabelMessage.InnerHtml = Session["resultMessage"].ToString();
                Session["resultMessage"] = null;
            }
            else if (Request["o"] != null)
            {
                LabelMessage.InnerHtml = Request["o"];
            }
        }
    }
}