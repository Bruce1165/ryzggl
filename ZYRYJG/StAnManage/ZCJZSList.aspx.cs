using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Telerik.Web.UI;

namespace ZYRYJG.StAnManage
{
    public partial class ZCJZSList : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) ButtonSearch_Click(sender, e);
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ObjectDataSource1.SelectParameters.Clear();

            QueryParamOB q = new QueryParamOB();
            if (rdtxtQYMC.Text.Trim() != "")   //企业名称
            {
                q.Add(string.Format("QYMC like '%{0}%'", rdtxtQYMC.Text.Trim()));
            }
            if (RadTextBoxUNITCODE.Text.Trim() != "")   //机构代码
            {
                q.Add(string.Format("ZZJGDM like '%{0}%'", RadTextBoxUNITCODE.Text.Trim()));
            }
            if (RadTextBoxXM.Text.Trim() != "")   //姓名
            {
                q.Add(string.Format("XM like '%{0}%'", RadTextBoxXM.Text.Trim()));
            }
            if (RadTextBoxZJHM.Text.Trim() != "")   //证件号码
            {
                q.Add(string.Format("ZJHM like '%{0}%'", RadTextBoxZJHM.Text.Trim()));
            }
            if (RadTextBoxZCBH.Text.Trim() != "")   //注册号
            {
                q.Add(string.Format("ZCH like '%{0}%'", RadTextBoxZCBH.Text.Trim()));
            }
            if (RadioButtonListQY.SelectedValue != "全部")   //区域
            {
                q.Add(string.Format("QY = '{0}'", RadioButtonListQY.SelectedValue));
            }      

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGrid1.CurrentPageIndex = 0;
            RadGrid1.DataSourceID = ObjectDataSource1.ID;
        }
    }
}
