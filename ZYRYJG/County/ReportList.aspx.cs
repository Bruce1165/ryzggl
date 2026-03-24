using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Model;
using DataAccess;
using System.Text;
using System.IO;
using Telerik.Web.UI;

namespace ZYRYJG.County
{
    public partial class ReportList : BasePage
    {
        //业务员上报
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
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
            QueryParamOB q = new QueryParamOB();
            //所属区县
            if (RadComboBoxENT_City.SelectedValue !="")
            {
                q.Add(string .Format("ENT_City like '%{0}%'",RadComboBoxENT_City.SelectedValue));
            }
            //申报事项
            if (RadComboBoxApplyType.SelectedValue !="")
            {
                q.Add(string.Format("ApplyType = '{0}'", RadComboBoxApplyType.SelectedValue));
            }
            //上报日期
            if (RadDatePickerApplyTimeStart.SelectedDate.HasValue == true)
            {
                q.Add(string.Format("ReportDate >= '{0}'", RadDatePickerApplyTimeStart.SelectedDate.Value));
            }
            if (RadDatePickerApplyTimeEnd.SelectedDate.HasValue == true)
            {
                q.Add(string.Format("ReportDate <= '{0}'", RadDatePickerApplyTimeEnd.SelectedDate.Value.ToString("yyyy-MM-dd 23:59:59")));
            }
            //上报批次号
            if (RadTextBoxReportCode.Text.Trim() != "" && RadTextBoxReportCode.Text.Trim() != null)
            {
                q.Add(string.Format("ReportCode = '{0}'", RadTextBoxReportCode.Text.Trim()));
            }
            else 
            {
                q.Add("ReportDate is not null");//已上报
            }

            //注册中心业务员
            if (IfExistRoleID("4") == true)
            {
                //申报状态
                if (RadComboBoxCheckStatus.SelectedValue != "")
                {
                    q.Add(string.Format("CheckStatus = '{0}'", RadComboBoxCheckStatus.SelectedValue));
                }
            }

            //注册中心领导
            if (IfExistRoleID("6") == true)
            {
                //申报状态
                if (RadComboBoxCheckStatus.SelectedValue != "")
                {
                    q.Add(string.Format("CheckStatus = '已审查' and ConfirmStatus = '{0}'", RadComboBoxCheckStatus.SelectedValue));
                }
            }


            ObjectDataSource1.SelectParameters.Clear();
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridHZSB.CurrentPageIndex = 0;
            RadGridHZSB.DataSourceID = ObjectDataSource1.ID;
        }

      

    }
}