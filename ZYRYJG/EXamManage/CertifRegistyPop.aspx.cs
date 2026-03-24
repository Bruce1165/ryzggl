using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using Telerik.Web.UI;
using System.Data;

namespace ZYRYJG.EXamManage
{
    public partial class CertifRegistyPop : BasePage
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
        }     
    }
}
