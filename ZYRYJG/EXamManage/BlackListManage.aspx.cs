using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;
using Telerik.Web.UI;

namespace ZYRYJG.EXamManage
{
    public partial class BlackListManage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RadGridMain.MasterTableView.Columns.FindByUniqueName("View").Visible = ValidResourceIDLimit(RoleIDs, "BlackListView");
                if (ValidResourceIDLimit(RoleIDs, "BlackListEdit") == false)
                {
                    RadGridMain.MasterTableView.Columns.FindByUniqueName("Edit").Visible = false;
                    RadGridMain.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                }
                RadGridMain.MasterTableView.Columns.FindByUniqueName("Delete").Visible = ValidResourceIDLimit(RoleIDs, "BlackListDelete");

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

            QueryParamOB q = new QueryParamOB();
           
            //姓名
            if (RadTxtWorkerName.Text != "")
            {
                q.Add(string.Format("WorkerName like '%{0}%'", RadTxtWorkerName.Text.Trim()));
            }
            //证件号码
            if (RadTxtCertificateCode.Text != "")
            {
                q.Add(string.Format("CertificateCode like '%{0}%'", RadTxtCertificateCode.Text.Trim()));
            }
            if (RadTextBoxUnitName.Text.Trim() != "")    //单位名称
            {
                q.Add(string.Format("UnitName like '%{0}%'", RadTextBoxUnitName.Text.Trim()));
            }
            if (RadTextBoxTrainUnitName.Text.Trim() != "")    //培训点
            {
                q.Add(string.Format("TrainUnitName like '%{0}%'", RadTextBoxUnitName.Text.Trim()));
            }
            //生效时间
            if (RadDatePicker_StartDate.SelectedDate.HasValue || RadDatePicker_EndDate.SelectedDate.HasValue)
            {
                q.Add(string.Format("(StartDate BETWEEN  '{0}' AND '{1}')"
                    , RadDatePicker_StartDate.SelectedDate.HasValue ? RadDatePicker_StartDate.SelectedDate.Value.ToString() : DateTime.MinValue.AddDays(1).ToString()
                    , RadDatePicker_EndDate.SelectedDate.HasValue ? RadDatePicker_EndDate.SelectedDate.Value.AddDays(1).AddMinutes(-1).ToString() : DateTime.MaxValue.AddDays(-1).ToString()));
            }

            //黑名单状态
            if (RadComboBoxBlackStatus.SelectedValue != "")
            {
                q.Add(string.Format("BlackStatus ='{0}'", RadComboBoxBlackStatus.SelectedItem.Value));
            }

            //黑名单类型
            if (RadComboBoxBlackType.SelectedValue != "")
            {
                q.Add(string.Format("BlackType ='{0}'", RadComboBoxBlackType.SelectedItem.Value));
            }   
           
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridMain.CurrentPageIndex = 0;
            RadGridMain.DataSourceID = ObjectDataSource1.ID;
        }

        //删除
        protected void RadGridUnit_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string BlackListID = RadGridMain.MasterTableView.DataKeyValues[e.Item.ItemIndex]["BlackListID"].ToString();
                try
                {
                    BlackListDAL.Delete(Convert.ToInt64(BlackListID));
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "修改失败！", ex);
                    return;
                }
                UIHelp.layerAlert(Page, "删除成功！");
            }
        }
    }
}
