using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;

namespace ZYRYJG.Register
{
    public partial class NewsView : BasePage
    {
        protected override bool IsNeedLogin
        {
            get
            {
                return false;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PolicyNewsMDL o = PolicyNewsDAL.GetObject(Utility.Cryptography.Decrypt(Request["o"]));
                title.InnerText = o.Title;
                content.InnerHtml = o.Content;
            }
        }
    }
}