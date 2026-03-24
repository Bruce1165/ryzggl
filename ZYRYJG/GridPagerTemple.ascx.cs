using System;

namespace ZYRYJG
{
    public partial class GridPagerTemple : System.Web.UI.UserControl
    {
        protected void LinkButtonGo_Click(object sender, EventArgs e)
        {
            this.LinkButtonGo.CommandArgument = tbPageNumber.Text;
        }
    }
}