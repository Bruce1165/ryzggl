using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Model;

namespace ZYRYJG.EXamManage
{
    public partial class ExamSignManage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //RadDatePicker_PrintStartDate.DbSelectedDate = DateTime.Now.ToString("yyyy-01-01");
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
            ObjectDataSource1.SelectParameters.Clear();//清空查询参数
            QueryParamOB q = new QueryParamOB();
            q.Add(" u.OrganID=200");//隶属注册中心人员

            if (RadTextBoxRYMC.Text.Trim() != "")   //人员名称
            {
                q.Add(string.Format("u.ReluserName like '%{0}%'", RadTextBoxRYMC.Text.Trim()));
            }

            if (RadDatePicker_PrintStartDate.SelectedDate.HasValue || RadDatePicker_PrintEndDate.SelectedDate.HasValue)//创建时间
            {
                q.Add(string.Format("( s.CreateTime BETWEEN  '{0}' AND '{1}')"
                    , RadDatePicker_PrintStartDate.SelectedDate.HasValue ? RadDatePicker_PrintStartDate.SelectedDate.Value.ToString("yyyy-MM-dd") : DateTime.MinValue.AddDays(1).ToString()
                    , RadDatePicker_PrintEndDate.SelectedDate.HasValue ? RadDatePicker_PrintEndDate.SelectedDate.Value.ToString("yyyy-MM-dd 23:59:59") : DateTime.MaxValue.AddDays(-1).ToString()));
            }
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGrid1.CurrentPageIndex = 0;
            RadGrid1.DataSourceID = ObjectDataSource1.ID;
        }
    }
}