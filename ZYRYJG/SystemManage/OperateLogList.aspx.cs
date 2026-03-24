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
    public partial class OperateLogList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) ButtonSearch_Click(sender, e);
        }

        //查询
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ObjectDataSource1.SelectParameters.Clear();

            QueryParamOB q = new QueryParamOB();

            if (RadTextBoxOperateName.Text != "")//操作名称
            {
                q.Add(string.Format("OperateName like '%{0}%'", RadTextBoxOperateName.Text.Trim()));
            }
            if (RadTextBoxLogDetail.Text != "")//内容详细
            {
                q.Add(string.Format("LogDetail like '%{0}%'", RadTextBoxLogDetail.Text.Trim()));
            }

            if (RadTextBoxPersonName.Text != "")// 操作者
            {
                q.Add(string.Format(" PersonName like'%{0}%'", RadTextBoxPersonName.Text.Trim()));
            }
            
            if (datePickerFrom.SelectedDate.HasValue)
            {
                q.Add(string.Format(" LogTime>='{0}'", datePickerFrom.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }
            if (datePickerEnd.SelectedDate.HasValue)
            {
                q.Add(string.Format("LogTime<'{0}'", datePickerEnd.SelectedDate.Value.AddDays(1).ToString("yyyy-MM-dd")));
            }

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridOperateLog.CurrentPageIndex = 0;
            RadGridOperateLog.DataSourceID = ObjectDataSource1.ID;
        }
    }
}
