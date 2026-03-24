using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZYRYJG.City
{
    public partial class LogQuery : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ButtonQuery_Click(sender, e);
            }
        }

        protected void ButtonQuery_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }

            ObjectDataSource1.SelectParameters.Clear();
            var q = new QueryParamOB();
            
            if (RadDatePickerGetDateStart.SelectedDate.HasValue == true)
            {
                q.Add(string.Format("{0} > '{1}'", "LogTime", RadDatePickerGetDateStart.SelectedDate.Value));
            }
            else if (RadDatePickerGetDateEnd.SelectedDate.HasValue == true)
            {
                q.Add(string.Format("{0} <= '{1} 23:59:59'", "LogTime", RadDatePickerGetDateEnd.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }
            else
            { 
             q.Add(string.Format("{0} > '{1} 00:00:00'", "LogTime", DateTime.Now.ToString("yyyy-MM-dd")));
            
            }
            if (RadTextBoxValue.Text.Trim()!="")
            {
                q.Add(string.Format("{0} like '%{1}%'", "PersonName", RadTextBoxValue.Text.Trim()));
            }
            //string sql = string.Format("select * from OperateLog where 1=1 {0}",q.ToWhereString());

            //DataTable dt = CommonDAL.GetDataTable(sql);
            //RadGridZGGL.DataSource = dt;
            //RadGridZGGL.DataBind();
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridZGGL.CurrentPageIndex = 0;
            RadGridZGGL.DataSourceID = ObjectDataSource1.ID;
        }
    }
}