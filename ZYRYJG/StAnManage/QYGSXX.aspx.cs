using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Telerik.Web.UI;

namespace ZYRYJG.StAnManage
{
    public partial class QYGSXX : BasePage
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
                q.Add(string.Format("ENT_NAME like '%{0}%'", rdtxtQYMC.Text.Trim()));
            }

            if (RadTextBoxUNITCODE.Text.Trim() != "")   //机构代码
            {
                q.Add(string.Format("UNI_SCID like '%{0}%'", RadTextBoxUNITCODE.Text.Trim()));
            }
            if (RadTextBoxCORP_RPT.Text.Trim() != "")   //机构代码
            {
                q.Add(string.Format("CORP_RPT like '%{0}%'", RadTextBoxCORP_RPT.Text.Trim()));
            }   

            

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGrid1.CurrentPageIndex = 0;
            RadGrid1.DataSourceID = ObjectDataSource1.ID;
        }
    }
}
