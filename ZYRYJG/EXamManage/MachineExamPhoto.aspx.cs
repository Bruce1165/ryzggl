using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using Telerik.Web.UI;
using System.Data;
using System.IO;

namespace ZYRYJG.EXamManage
{
    public partial class MachineExamPhoto : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {               
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
            ObjectDataSource1.SelectParameters.Clear();
            QueryParamOB q = new QueryParamOB();
            //计划名称
            if (RadTextBoxExam_PlanName.Text.Trim() != "") q.Add(string.Format("ExamPlanName like '%{0}%'", RadTextBoxExam_PlanName.Text.Trim()));
            //岗位工种
            if (PostSelect2.PostID != "")
                q.Add(string.Format("PostID = {0}", PostSelect2.PostID));
            else if (PostSelect2.PostTypeID != "")
                q.Add(string.Format("PostTypeID = {0}", PostSelect2.PostTypeID));

            //报名时间
            if (RadDatePicker_SignUpStartDate.SelectedDate.HasValue || RadDatePicker_SignUpEndDate.SelectedDate.HasValue)
            {
                q.Add(string.Format("(('{0}' BETWEEN  SignUpStartDate AND SignUpEndDate) or ('{1}' BETWEEN SignUpStartDate AND SignUpEndDate) or (SignUpStartDate BETWEEN '{0}' AND '{1}') or (SignUpEndDate BETWEEN '{0}' AND '{1}') )"
                    , RadDatePicker_SignUpStartDate.SelectedDate.HasValue ? RadDatePicker_SignUpStartDate.SelectedDate.Value.ToString() : DateTime.MinValue.AddDays(1).ToString()
                    , RadDatePicker_SignUpEndDate.SelectedDate.HasValue ? RadDatePicker_SignUpEndDate.SelectedDate.Value.ToString() : DateTime.MaxValue.AddDays(-1).ToString()));
            }
            //考试时间
            if (RadDatePicker_ExamStartDate.SelectedDate.HasValue || RadDatePicker_ExamEndDate.SelectedDate.HasValue)
            {
                q.Add(string.Format("(('{0}' BETWEEN ExamStartDate AND ExamEndDate) or ('{1}' BETWEEN ExamStartDate AND ExamEndDate) or (ExamStartDate BETWEEN '{0}' AND '{1}') or (ExamEndDate BETWEEN '{0}' AND '{1}') )"
                    , RadDatePicker_ExamStartDate.SelectedDate.HasValue ? RadDatePicker_ExamStartDate.SelectedDate.Value.ToString() : DateTime.MinValue.AddDays(1).ToString()
                    , RadDatePicker_ExamEndDate.SelectedDate.HasValue ? RadDatePicker_ExamEndDate.SelectedDate.Value.ToString() : DateTime.MaxValue.AddDays(-1).ToString()));
            }
            if (rbl.SelectedValue == "1")//未考试
            {
                q.Add(string.Format("ExamStartDate>'{0}'", DateTime.Now.ToString("yyyy-MM-dd")));
            }
            if (rbl.SelectedValue == "2")//以考试
            {
                q.Add(string.Format("ExamStartDate<='{0}'", DateTime.Now.ToString("yyyy-MM-dd")));
            }
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridExamPlan.CurrentPageIndex = 0;
            RadGridExamPlan.DataSourceID = ObjectDataSource1.ID;

        }

        protected void RadGridExamPlan_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                if (File.Exists(Server.MapPath(string.Format("~/Upload/ExamFacePhoto/{0}{1}_{2}.rar"
                    , RadGridExamPlan.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PostName"]
                    , Convert.ToDateTime(RadGridExamPlan.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ExamStartDate"]).ToString("yyyyMMdd")
                    , RadGridExamPlan.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ExamPlanID"]
                    ))))
                {
                    (e.Item.Cells[RadGridExamPlan.MasterTableView.Columns.FindByUniqueName("photo").OrderIndex].FindControl("HyperLink1") as HyperLink).NavigateUrl =UIHelp.AddUrlReadParam(string.Format("~/Upload/ExamFacePhoto/{0}{1}_{2}.rar"
                         , RadGridExamPlan.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PostName"]
                         , Convert.ToDateTime(RadGridExamPlan.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ExamStartDate"]).ToString("yyyyMMdd")
                         , RadGridExamPlan.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ExamPlanID"]
                         ));

                    (e.Item.Cells[RadGridExamPlan.MasterTableView.Columns.FindByUniqueName("photo").OrderIndex].FindControl("HyperLink1") as HyperLink).Text = string.Format("{0}{1}_{2}.rar"
                    , RadGridExamPlan.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PostName"]
                    , Convert.ToDateTime(RadGridExamPlan.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ExamStartDate"]).ToString("yyyyMMdd")
                    , RadGridExamPlan.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ExamPlanID"]
                    );

                    (e.Item.Cells[RadGridExamPlan.MasterTableView.Columns.FindByUniqueName("photo").OrderIndex].FindControl("ButtonCreate") as Button).Visible = false;
                    (e.Item.Cells[RadGridExamPlan.MasterTableView.Columns.FindByUniqueName("photo").OrderIndex].FindControl("HyperLink1") as HyperLink).Visible = true;
                }
                else if (File.Exists(Server.MapPath(string.Format("~/Upload/ExamFacePhoto/{0}.txt", RadGridExamPlan.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ExamPlanID"]
                   ))))
                {
                    e.Item.Cells[RadGridExamPlan.MasterTableView.Columns.FindByUniqueName("photo").OrderIndex].Text = "任务生成中...";
                }
                else
                {
                    (e.Item.Cells[RadGridExamPlan.MasterTableView.Columns.FindByUniqueName("photo").OrderIndex].FindControl("HyperLink1") as HyperLink).Visible = false;
                    (e.Item.Cells[RadGridExamPlan.MasterTableView.Columns.FindByUniqueName("photo").OrderIndex].FindControl("ButtonCreate") as Button).Visible = true;
                }
            }
        }

        //发起生成照片压缩包任务
        protected void RadGridExamPlan_ItemCommand(object source, GridCommandEventArgs e)
        {
            if(e.CommandName=="down")
            {
                int count = CommonDAL.GetRowCount("[VIEW_EXAMRESULT]", "", string.Format(" and [examplanid]={0}", e.CommandArgument));

                if (count == 0)
                {
                    UIHelp.layerAlert(Page, "该考试计划尚未创建准考证，无法下载照片。");
                    return;
                }

                File.WriteAllLines(Server.MapPath(string.Format("~/Upload/ExamFacePhoto/{0}.txt", e.CommandArgument)), new string[1], System.Text.Encoding.Default);

                if (File.Exists(Server.MapPath(string.Format("~/Upload/ExamFacePhoto/{0}.txt", RadGridExamPlan.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ExamPlanID"]
                   ))))
                {
                    e.Item.Cells[RadGridExamPlan.MasterTableView.Columns.FindByUniqueName("photo").OrderIndex].Text = "任务生成中...";
                    UIHelp.WriteOperateLog(PersonName, UserID, "发起生成上机考试人员照片下载包", string.Format("考试计划ID：{0}", RadGridExamPlan.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ExamPlanID"]));
                }
            }
        }
    }
}
