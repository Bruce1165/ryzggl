using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Telerik.Web.UI;

namespace ZYRYJG.PersonnelFile
{
    public partial class PersonInfo : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (ValidResourceIDLimit(RoleIDs, "PersonLock") == true)//人员修正与锁定权限
                {
                    RadGrid1.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
                    RadGrid1.MasterTableView.Columns.FindByUniqueName("Edit").Visible = true;
                }
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

            if (RadTextBoxValue.Text.Trim() != "")//自定义查询项
            {
                q.Add(string.Format("{0} like '%{1}%'", RadComboBoxIten.SelectedValue, RadTextBoxValue.Text.Trim()));
            }

            if (RadNumericTextBoxBirthdayFrom.Value.HasValue==true)//年龄从
            {
                q.Add(string.Format("dateadd(year,{0},BIRTHDAY) <getdate()", RadNumericTextBoxBirthdayFrom.Value));
            }
            if (RadNumericTextBoxBirthdayTo.Value.HasValue == true)//年龄至
            {
                q.Add(string.Format("dateadd(year,{0},BIRTHDAY) >getdate()", RadNumericTextBoxBirthdayTo.Value));
            }
            //if (rdtxtWorkerName.Text.Trim() != "")    //姓名
            //{
            //    q.Add(string.Format("WorkerName like '%{0}%'", rdtxtWorkerName.Text.Trim()));
            //}
           
            //if (rdtxtZJHM.Text.Trim() != "")   //证件号码
            //{
            //    q.Add(string.Format("CERTIFICATECODE like '%{0}%'", rdtxtZJHM.Text.Trim()));
            //}

            if (RadioButtonListLockStatus.SelectedValue == "锁定中")
            {
                q.Add(string.Format("WorkerID in (select WorkerID from dbo.WorkerLock where LockStatus='加锁' and LockEndTime >'{0}')", DateTime.Now.ToString("yyyy-MM-dd")));
            }
            else if (RadioButtonListLockStatus.SelectedValue == "未锁定")
            {
                q.Add(string.Format("WorkerID not in (select WorkerID from dbo.WorkerLock where LockStatus='加锁' and LockEndTime >'{0}')", DateTime.Now.ToString("yyyy-MM-dd")));
            }
            if (RadioButtonListLockRemark.SelectedValue == "缺考")
            {
                q.Add(string.Format("WorkerID in (select WorkerID from dbo.WorkerLock where LockStatus='加锁' and LockEndTime >'{0}' and REMARK like '%一年内缺考三次,锁定一年不得参与考试%')", DateTime.Now.ToString("yyyy-MM-dd")));
            }
            else if (RadioButtonListLockRemark.SelectedValue == "其他")
            {
                q.Add(string.Format("WorkerID not in (select WorkerID from dbo.WorkerLock where LockStatus='加锁' and LockEndTime >'{0}' and REMARK like '%一年内缺考三次,锁定一年不得参与考试%')", DateTime.Now.ToString("yyyy-MM-dd")));
            }

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGrid1.CurrentPageIndex = 0;
            RadGrid1.DataSourceID = ObjectDataSource1.ID;
        }

        //protected void Button2_Click(object sender, EventArgs e)
        //{
        //    rdtxtWorkerName.Text = "";
        //    rdtxtZJHM.Text = "";
        //}
    }
}
