using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;

namespace ZYRYJG.Unit
{
    public partial class UnitDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UnitMDL o = UnitDAL.GetObject(Utility.Cryptography.Decrypt(Request["o"]));
                if (o != null)
                {
                    UIHelp.SetData(EditTable, o, true);
                }
            }
        }
    }
}