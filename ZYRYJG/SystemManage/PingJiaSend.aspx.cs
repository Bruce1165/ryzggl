using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;
using System.Data;
using Utility;

namespace ZYRYJG.SystemManage
{
    public partial class PingJiaSend : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "SystemManage/RoleManage.aspx";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ButtonSearch_Click(sender, e);
            }
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            ObjectDataSource1.SelectParameters.Clear();
            var q = new QueryParamOB();


            if (RadioButtonListDataType.SelectedItem.Value != "")
            {
                q.Add(string.Format("[DataType] ='{0}'", RadioButtonListDataType.SelectedItem.Value));
            }
            if (RadioButtonListStatus.SelectedItem.Value != "")
            {
                if (RadioButtonListStatus.SelectedItem.Value == "未推送")
                    q.Add("[DoTime] is null");
                else
                    q.Add("[DoTime] > '2025-1-1'");
            }
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGrid1.CurrentPageIndex = 0;
            RadGrid1.DataSourceID = ObjectDataSource1.ID;
        }
    }
}