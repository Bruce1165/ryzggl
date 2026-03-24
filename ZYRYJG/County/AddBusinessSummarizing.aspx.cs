using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZYRYJG.County
{
    public partial class AddBusinessSummarizing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("RowNum", typeof(object));
                dt.Columns.Add("AA", typeof(object));
                dt.Columns.Add("BB", typeof(object));
                dt.Columns.Add("CC", typeof(object));
                dt.Columns.Add("DD", typeof(object));
                dt.Columns.Add("EE", typeof(object));
                dt.Columns.Add("FF", typeof(object));
                dt.Columns.Add("GG", typeof(object));
                RadGridRYXX.DataSource = Session["const"] != null ? Session["const"]:dt;
                RadGridRYXX.DataBind();
            }
        }
    }
}