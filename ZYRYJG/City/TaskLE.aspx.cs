using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Model;
using DataAccess;
using System.Data;
using Utility;
namespace ZYRYJG.City
{
    public partial class TaskLE : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //RadAjaxManager1.AjaxSettings.AddAjaxSetting((this.Master.FindControl("RadScriptManager1") as ScriptManager), RadGridTask);
            //RadAjaxManager1.AjaxSettings.AddAjaxSetting((this.FindControl("RadScriptManager1") as ScriptManager), RadGridTask);
            if (!this.IsPostBack)
            {
                RadGridTask.MasterTableView.Columns.FindByUniqueName("Jcjg").Visible = false;
                RadGridTask.MasterTableView.Columns.FindByUniqueName("Jcsjks").Visible = false;
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
            ObjectDataSourceRadGridTask.SelectParameters.Clear();

            QueryParamOB q = new QueryParamOB();
            if (RadioButtonListCheckStatus.SelectedValue == "未填报")
            {
                q.Add("[Jcsjks] is null");
            }
            else
            {
                q.Add("[Jcsjks] > '1949-01-01'");//已填报
            }
            //自定义文本查询项
            if (RadComboBoxIten.SelectedValue != "" && RadTextBoxValue.Text.Trim() != "")
            {
                q.Add(string.Format("{0} like '%{1}%'", RadComboBoxIten.SelectedValue, RadTextBoxValue.Text.Trim()));
            }

            //自定义日期查询项
            if (RadComboBoxDateTimeItem.SelectedValue != "")
            {       

                if (RadDatePickerApplyTimeStart.SelectedDate.HasValue == true && RadDatePickerApplyTimeEnd.SelectedDate.HasValue == true)
                {
                    if (RadComboBoxDateTimeItem.SelectedValue == "Jcsjks")
                    {
                        q.Add(string.Format("((Jcsjks >= '{0}' and Jcsjks < '{1}') or (Jcsjjs >= '{0}' and Jcsjjs < '{1}')  or (Jcsjks < '{0}' and Jcsjjs >= '{1}'))", RadDatePickerApplyTimeStart.SelectedDate.Value.ToString("yyyy-MM-dd"), RadDatePickerApplyTimeEnd.SelectedDate.Value.AddDays(1).ToString("yyyy-MM-dd")));
                    }
                    else
                    {
                        q.Add(string.Format("{0} >= '{1}' and {0} < '{2}'", RadComboBoxDateTimeItem.SelectedValue, RadDatePickerApplyTimeStart.SelectedDate.Value.ToString("yyyy-MM-dd"), RadDatePickerApplyTimeEnd.SelectedDate.Value.AddDays(1).ToString("yyyy-MM-dd")));
                    }
                }
                else if (RadDatePickerApplyTimeStart.SelectedDate.HasValue == true)
                {
                    if (RadComboBoxDateTimeItem.SelectedValue == "Jcsjks")
                    {
                        q.Add(string.Format("(Jcsjks >= '{0}' or Jcsjjs >= '{0}')", RadDatePickerApplyTimeStart.SelectedDate.Value.ToString("yyyy-MM-dd")));
                    }
                    else
                    {
                        q.Add(string.Format("{0} >= '{1}'", RadComboBoxDateTimeItem.SelectedValue, RadDatePickerApplyTimeStart.SelectedDate.Value.ToString("yyyy-MM-dd")));
                    }
                }
                else if (RadDatePickerApplyTimeEnd.SelectedDate.HasValue == true)
                {
                    if (RadComboBoxDateTimeItem.SelectedValue == "Jcsjks")
                    {
                        q.Add(string.Format("(Jcsjks < '{0}' or Jcsjjs < '{0}')", RadDatePickerApplyTimeEnd.SelectedDate.Value.AddDays(1).ToString("yyyy-MM-dd")));
                    }
                    else
                    {
                        q.Add(string.Format("{0} < '{1}'", RadComboBoxDateTimeItem.SelectedValue, RadDatePickerApplyTimeEnd.SelectedDate.Value.AddDays(1).ToString("yyyy-MM-dd")));
                    }
                }
            }

            ObjectDataSourceRadGridTask.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridTask.CurrentPageIndex = 0;
            RadGridTask.DataSourceID = ObjectDataSourceRadGridTask.ID;
        }

        //编号
        protected void ZSBH(object sender, EventArgs e)
        {
            DateTime startTime = RadDatePickerStart.SelectedDate.Value;
            DateTime endTime = RadDatePickerEnd.SelectedDate.Value;
            string cjjg = RadComboBoxResult.SelectedValue;
         
            if (RadGridTask.MasterTableView.Items.Count == 0)
            {
                UIHelp.layerAlert(Page, "没有要反馈的数据！");
                return;
            }

            string  filterString = ObjectDataSourceRadGridTask.SelectParameters["filterWhereString"].DefaultValue;
            try
            {
                int count = Gx_Task_ToZczxDAL.SelectCount(filterString);
                Gx_Task_ToZczxDAL.UpdateBatch(filterString, cjjg, startTime, endTime);

                RadDatePickerStart.Clear();
                RadDatePickerEnd.Clear();
                RadComboBoxResult.SelectedIndex = 0;
                UIHelp.WriteOperateLog(PersonName, UserID, "批量填报双随机执法任务结果", string.Format("检查时间：{0} - {1}，检查结果：{2}，共计：{3}条。"
                    , startTime, endTime, cjjg, count));

                UIHelp.layerAlert(Page, string.Format("填报成功，共计{0}条。" ,count));
              
            }
            catch (Exception ex)
            {

                UIHelp.WriteErrorLog(Page, "批量填报双随机执法任务结果失败！", ex);
                return;
            }

            ButtonSearch_Click(sender, e);
        }
        

        //变换填报状态
        protected void RadioButtonListCheckStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            if(RadioButtonListCheckStatus.SelectedValue=="未填报")
            {
                BtnBH.Visible = true;
                RadGridTask.MasterTableView.Columns.FindByUniqueName("Jcjg").Visible = false;
                RadGridTask.MasterTableView.Columns.FindByUniqueName("Jcsjks").Visible = false;
            }
            else
            {
                BtnBH.Visible = false;
                RadGridTask.MasterTableView.Columns.FindByUniqueName("Jcjg").Visible = true;
                RadGridTask.MasterTableView.Columns.FindByUniqueName("Jcsjks").Visible = true;
            }
            ButtonSearch_Click(sender, e);
        }
    }
}