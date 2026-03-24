using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZYRYJG.County
{
    public partial class UnitMessage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                ButtonQuery_Click(sender, e);
            }
        }

        protected void ButtonQuery_Click(object sender, EventArgs e)
        {
            ObjectDataSource1.SelectParameters.Clear();
            var q = new QueryParamOB();
            if (RadComboBoxItem.SelectedValue != null && RadTextBoxZJHM.Text.Trim() != "")
            {
                q.Add(string.Format("{1} like '%{0}%'", RadTextBoxZJHM.Text.Trim(), RadComboBoxItem.SelectedValue));
            }
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridZGGL.CurrentPageIndex = 0;
            RadGridZGGL.DataSourceID = ObjectDataSource1.ID;
        }
    }
}