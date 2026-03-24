using System;
using Model;

namespace ZYRYJG.StAnManage
{
    public partial class OperateLogList : BasePage
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
            //访问用户
            if (txtAccessUser.Text.Trim() != "")
            {
                q.Add(string.Format("AccessUser like '%{0}%'", txtAccessUser.Text.Trim()));
            }

            //访问时间
            if (RadDatePicker_AccessDateStartDate.SelectedDate.HasValue || RadDatePicker_AccessDateEndDate.SelectedDate.HasValue)
            {
                if (RadDatePicker_AccessDateStartDate.SelectedDate.HasValue && RadDatePicker_AccessDateEndDate.SelectedDate.HasValue)
                {
                    TimeSpan ts = RadDatePicker_AccessDateStartDate.SelectedDate.Value.Subtract(RadDatePicker_AccessDateEndDate.SelectedDate.Value);
                    if ((ts).Ticks > 0)
                    {
                        UIHelp.layerAlert(Page, "Alert", "开始时间不能大于结束时间!");
                    }
                }
                q.Add(string.Format("ACCESSDATE BETWEEN '{0}' AND '{1}'"
                    ,
                    RadDatePicker_AccessDateStartDate.SelectedDate.HasValue
                        ? RadDatePicker_AccessDateStartDate.SelectedDate.Value.ToString("yyyy-MM-dd")
                        : DateTime.MinValue.AddDays(1).ToString("yyyy-MM-dd")
                    ,
                    RadDatePicker_AccessDateEndDate.SelectedDate.HasValue
                        ? RadDatePicker_AccessDateEndDate.SelectedDate.Value.ToString("yyyy-MM-dd")
                        : DateTime.MaxValue.AddDays(-1).ToString("yyyy-MM-dd")
                    )
                    );
            }

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridInterFaceLog.CurrentPageIndex = 0;
            RadGridInterFaceLog.DataSourceID = ObjectDataSource1.ID;
        }
    }
}