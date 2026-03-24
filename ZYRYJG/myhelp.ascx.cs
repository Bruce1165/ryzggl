using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZYRYJG
{
    public partial class myhelp : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 帮助页ID
        /// </summary>
        public string PageID
        {
            get { return HiddenFieldHelpPageID.Value; }
            set { HiddenFieldHelpPageID.Value = value; }
        }
    }
}