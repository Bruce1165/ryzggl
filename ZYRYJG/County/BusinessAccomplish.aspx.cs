using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using System.Data;
using DataAccess;

namespace ZYRYJG.County
{
    public partial class BusinessAccomplish : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                ButtonSearch_Click(sender, e);
            }

        }
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            ObjectDataSource1.SelectParameters.Clear();
            var q = new QueryParamOB();
            if (RoleIDs == "3")//区县
            {
                q.Add(string.Format("ent_city = '{0}'", Region));
            }
            if (RadTextBoxValue.Text.Trim() != "")//自定义查询项
            {
                q.Add(string.Format("{0} like '%{1}%'", RadComboBoxIten.SelectedValue, RadTextBoxValue.Text.Trim()));
            }

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridYBYW.CurrentPageIndex = 0;
            RadGridYBYW.DataSourceID = ObjectDataSource1.ID;
        }
    }
}