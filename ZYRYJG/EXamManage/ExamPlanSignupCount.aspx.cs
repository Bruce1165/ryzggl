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

namespace ZYRYJG.EXamManage
{
    public partial class ExamPlanSignupCount : BasePage
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
            ObjectDataSource2.SelectParameters.Clear();
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
            if (rbl.SelectedValue == "2")//已考试
            {
                q.Add(string.Format("ExamStartDate<='{0}'", DateTime.Now.ToString("yyyy-MM-dd")));
            }
            ObjectDataSource2.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridExamPlan.CurrentPageIndex = 0;
            RadGridExamPlan.DataSourceID = ObjectDataSource2.ID;

        }
    }
}
