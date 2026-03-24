using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;

namespace ZYRYJG.Register
{
    public partial class NumberChange : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ButtonSearch_Click(sender, e);
            }
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ObjectDataSource1.SelectParameters.Clear();
            var q = new QueryParamOB();
            if (RadTextBoxPersonValue.Text.Trim() != "")//人员自定义查询项
            {
                q.Add(string.Format("{0} like '%{1}%'", RadComboBoxPersonItem.SelectedValue, RadTextBoxPersonValue.Text.Trim()));
            }
            if (RadTextBoxUnitValue.Text.Trim() != "")//企业自定义查询项
            {
                q.Add(string.Format("{0} like '%{1}%'", RadComboBoxUnitItem.SelectedValue, RadTextBoxUnitValue.Text.Trim()));
            }   
          

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridQY.CurrentPageIndex = 0;
            RadGridQY.DataSourceID = ObjectDataSource1.ID;

        }
    }
}