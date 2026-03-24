using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GridPagerTemple : System.Web.UI.UserControl
{
    protected void LinkButtonGo_Click(object sender, EventArgs e)
    {
        this.LinkButtonGo.CommandArgument = tbPageNumber.Text;
    }
}