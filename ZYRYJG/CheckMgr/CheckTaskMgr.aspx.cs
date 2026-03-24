using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Telerik.Web.UI;
using DataAccess;
using System.Data;

namespace ZYRYJG.CheckMgr
{
    public partial class CheckTaskMgr : BasePage
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

            QueryParamOB q = new QueryParamOB();
            //日期类型自定义查询条件
            if (RadDatePickerGetDateStart.SelectedDate.HasValue == true)
            {         
                    q.Add(string.Format("{0} >= '{1}'", RadComboBoxItem.SelectedValue, RadDatePickerGetDateStart.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }
            if (RadDatePickerGetDateEnd.SelectedDate.HasValue == true)
            {
                q.Add(string.Format("{0} <= '{1}'", RadComboBoxItem.SelectedValue, RadDatePickerGetDateEnd.SelectedDate.Value.ToString("yyyy-MM-dd 23:59:59")));
            }
              //申报事项
            if (RadComboBoxCheckType.SelectedValue != "")
            {
                 q.Add(string.Format("CheckType like '{0}'",RadComboBoxCheckType.SelectedValue));
            }

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridTask.CurrentPageIndex = 0;
            RadGridTask.DataSourceID = ObjectDataSource1.ID;
        }

        protected void RadGridTask_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "ButtonDelete")
            {
                Int32 PatchCode = Convert.ToInt32(RadGridTask.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PatchCode"]);
                int RtnCount = CommonDAL.GetRowCount("[CheckFeedBack]", string.Format(" and PatchCode={0} and [WorkerRerpotTime] >'2024-1-1'", PatchCode));
                if (RtnCount >0 && PersonType !=1)
                {
                    UIHelp.layerAlert(this.Page, "系统已经检测到个人反馈了数据，不能删除。如若仍想删除数据，请联系系统管理员进行删除！", 2, 0);
                    return;
                }
                try
                {      
                    CheckTaskDAL.Delete(PatchCode);
                    UIHelp.layerAlert(this.Page, "删除成功！", 6, 3000);
                    RadGridTask.DataBind();

                    UIHelp.WriteOperateLog(PersonName, UserID, "删除监管任务", string.Format("批次号：{0}。", PatchCode));
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "监管任务删除失败！", ex);
                    return;
                }

            }
        }
     
    }
}