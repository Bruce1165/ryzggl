using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZYRYJG.RenewCertifates
{
    public partial class CertifConfirmP : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "CertifConfirm.aspx";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
