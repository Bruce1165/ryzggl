using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using DataAccess;
using Model;

namespace ZYRYJG.SystemManage
{
    public partial class UnitWatchInpu : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "UnitWatchList.aspx";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ButtonSearch_Click(sender, e);
            }
        }

        //查询
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ClearGridSelectedKeys(RadGridOperateLog);
            ObjectDataSource1.SelectParameters.Clear();

            QueryParamOB q = new QueryParamOB();

            if (RadTextBoxCertNo.Text != "")//证书编号
            {
                q.Add(string.Format("CertNo like '%{0}%'", RadTextBoxCertNo.Text.Trim()));
            }
            if (RadTextBoxErrorMessage.Text != "")//内容详细
            {
                q.Add(string.Format("ErrorMessage like '%{0}%'", RadTextBoxErrorMessage.Text.Trim()));
            }

            if (RadComboBoxCertType.SelectedValue != "")// 证书类型
            {
                q.Add(string.Format(" CertType like '{0}%'", RadComboBoxCertType.SelectedValue));
            }
             if (RadComboBoxStepName.SelectedValue != "")// 环节
            {
                q.Add(string.Format(" StepName like '{0}%'", RadComboBoxStepName.SelectedValue));
            }
            
            //操作时间
            if (datePickerFrom.SelectedDate.HasValue)
            {
                q.Add(string.Format(" DoTime>='{0}'", datePickerFrom.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }
            if (datePickerEnd.SelectedDate.HasValue)
            {
                q.Add(string.Format("DoTime<'{0}'", datePickerEnd.SelectedDate.Value.AddDays(1).ToString("yyyy-MM-dd")));
            }

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridOperateLog.CurrentPageIndex = 0;
            RadGridOperateLog.DataSourceID = ObjectDataSource1.ID;
        }

    
    }
}
