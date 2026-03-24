using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZYRYJG.County
{
    public partial class BusinessQueryDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("RowNum", typeof(object));
                dt.Columns.Add("AA", typeof(object));
                dt.Columns.Add("BB", typeof(object));
                dt.Columns.Add("CC", typeof(object));
                dt.Columns.Add("DD", typeof(object));
                dt.Columns.Add("EE", typeof(object));
                dt.Rows.Add(new object[] { 1, "受理", "王磊", "2015-04-09 09:50:23", "通过", "予以受理" });
                dt.Rows.Add(new object[] { 2, "审查", "关羽", "2015-04-10 09:50:23", "通过", "审查通过" });
                dt.Rows.Add(new object[] { 3, "决定", "张飞", "2015-04-11 09:50:00", "通过", "符合有关规定" });
                RadGridRYXX.DataSource = dt;
                RadGridRYXX.DataBind();
            }
        }
    }
}