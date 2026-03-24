using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;

namespace ZYRYJG.StAnManage
{
    public partial class OperateLogDetailList : BasePage
    {
        //protected override bool IsNeedLogin
        //{
        //    get { return false; }
        //}

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ShowDetail();
            }
        }

        protected void ShowDetail()
        {
            string queryString = Request.QueryString["o"];
            ObjectDataSource1.SelectParameters.Clear();
            var q = new QueryParamOB();
            if (!string.IsNullOrEmpty(queryString))
            {
                q.Add(string.Format("ACCESSUSER = '{0}'", queryString));
            }
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridInterFaceLogDetail.CurrentPageIndex = 0;
            RadGridInterFaceLogDetail.DataSourceID = ObjectDataSource1.ID;

        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ObjectDataSource1.SelectParameters.Clear();

            var q = new QueryParamOB();

            string queryString = Request.QueryString["o"];
            if (!string.IsNullOrEmpty(queryString))
            {
                q.Add(string.Format("ACCESSUSER = '{0}'", queryString));
            }
            //访问服务ID
            if (!string.IsNullOrEmpty(txtServerId.Text.Trim()))
            {
                q.Add(string.Format("SERVERID like '%{0}%'", txtServerId.Text.Trim()));
            }

            //调用的方法名称
            if (!string.IsNullOrEmpty(txtCallingMethodName.Text.Trim()))
            {
                q.Add(string.Format("CALLINGMETHODNAME like '%{0}%'", txtCallingMethodName.Text.Trim()));
            }


            //访问时间
            if (RadDatePicker_AccessDateStartDate.SelectedDate.HasValue ||
                RadDatePicker_AccessDateEndDate.SelectedDate.HasValue)
            {
                if (RadDatePicker_AccessDateStartDate.SelectedDate.HasValue &&
                    RadDatePicker_AccessDateEndDate.SelectedDate.HasValue)
                {
                    TimeSpan ts =
                        RadDatePicker_AccessDateStartDate.SelectedDate.Value.Subtract(
                            RadDatePicker_AccessDateEndDate.SelectedDate.Value);
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
            RadGridInterFaceLogDetail.CurrentPageIndex = 0;
            RadGridInterFaceLogDetail.DataSourceID = ObjectDataSource1.ID;
        }
    }
}