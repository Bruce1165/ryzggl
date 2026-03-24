using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZYRYJG.SystemManage
{
    public partial class AddUser :  BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "SystemManage/AddUser.aspx";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                BindDate();
            }
        }
        public void BindDate()
        {

            ObjectDataSource1.SelectParameters.Clear();
            var q = new QueryParamOB();
            if (!string.IsNullOrEmpty(RadTextBoxUser.Text))
            {
                q.Add(string.Format("[USER_NAME] like '{0}%'", RadTextBoxUser.Text));
            }
            if (!string.IsNullOrEmpty(RadTextBoxzjhm.Text))
            {
                q.Add(string.Format("ID_CARD_CODE = '{0}'", RadTextBoxzjhm.Text));
            }
            if (RadComboBoxZT.SelectedIndex!=0)
            {
                q.Add(string.Format("LICENSE = '{0}'", RadComboBoxZT.SelectedValue));
            }
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridAddUser.CurrentPageIndex = 0;
            RadGridAddUser.DataSourceID = ObjectDataSource1.ID;
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            BindDate();
        }

    
    }
}