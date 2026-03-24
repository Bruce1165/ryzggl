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
    public partial class NewStatistics : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BtnQuery_Click(sender, e);
            }
        }

        protected void ButtonExport_Click(object sender, EventArgs e)
        {

            if (RadGridTJ.Items.Count > 0)
            {
                string format = "yyyyMMdd";

                string stringValue = DateTime.Now.ToString(format);
                //RadGridRepeat.DataSource =dt;

                //RadGridRepeat.DataBind();

                RadGridTJ.ExportSettings.IgnorePaging = true;

                RadGridTJ.ExportSettings.OpenInNewWindow = true;

                RadGridTJ.ExportSettings.FileName = "办理量统计" + stringValue;

                RadGridTJ.MasterTableView.ExportToExcel();
            }
        }

        protected void BtnQuery_Click(object sender, EventArgs e)
        {
            var q = new QueryParamOB();
            if (RadDatePickerStart.SelectedDate.HasValue == true)
            {
                q.Add(string.Format("XslDateTime >= '{0}'", RadDatePickerStart.SelectedDate.Value));
            }
            if (RadDatePickerEnd.SelectedDate.HasValue == true)
            {
                q.Add(string.Format("XslDateTime <= '{0} 23:59:59'", RadDatePickerEnd.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }
            string sql = string.Format(@"
            select b.ENT_City,
            sum (case when a.ApplyType = '初始注册' then 1 else 0 end) as '初始注册',
            sum (case when a.ApplyType = '重新注册' then 1 else 0 end) as '重新注册',
            sum (case when a.ApplyType = '执业企业变更' then 1 else 0 end) as '执业企业变更',
            sum (case when b.ConfirmResult = '通过' then 1 else 0 end) as '通过',
			sum (case when b.ConfirmResult = '未通过' then 1 else 0 end) as '未通过',
			sum (case when b.ConfirmResult is null  then 1 else 0 end) as '未办结',
            count(a.ApplyId) as 人数,count(distinct b.ENT_Name) as 企业个数
            from  [dbo].[NewSetUp] a inner join Apply b on a.ApplyId=b.ApplyID where 1=1 {0} group by b.ENT_City
            union all
            select '合计' ,sum (case when a.ApplyType = '初始注册' then 1 else 0 end) as '初始注册',
            sum (case when a.ApplyType = '重新注册' then 1 else 0 end) as '重新注册',
            sum (case when a.ApplyType = '执业企业变更' then 1 else 0 end) as '执业企业变更',
            sum (case when b.ConfirmResult = '通过' then 1 else 0 end) as '通过',
			sum (case when b.ConfirmResult = '未通过' then 1 else 0 end) as '未通过',
			sum (case when b.ConfirmResult is null  then 1 else 0 end) as '未办结',
            count(a.ApplyId) as 人数,count(distinct b.ENT_Name) as 企业个数
            from  [dbo].[NewSetUp] a inner join Apply b on a.ApplyId=b.ApplyID where 1=1 {0}
            ", q.ToWhereString());
            DataTable dt = CommonDAL.GetDataTable(sql);
            RadGridTJ.DataSource = dt;
            RadGridTJ.DataBind();
        }
    }
}