using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;
using Telerik.Web.UI;
using System.Data;

namespace ZYRYJG.EXamManage
{
    public partial class AcceptLineSet : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "AcceptLineManage.aspx";
            }
        }

        protected override void Page_Init(object sender, EventArgs e)
        {
            DataTable dt = ExamPlanDAL.GetList(0, 1, "and ExamplanID=" + Request["o"], "ExamplanID");
            if (dt != null && dt.Rows.Count > 0)
            {
                LabelPostTypeName.Text = dt.Rows[0]["POSTTYPENAME"].ToString();
                LabelPostName.Text = dt.Rows[0]["POSTNAME"].ToString();
                LabelExamDate.Text = Convert.ToDateTime(dt.Rows[0]["ExamCardSendStartDate"]).ToString("yyyy.MM.dd-") + Convert.ToDateTime(dt.Rows[0]["ExamCardSendEndDate"]).ToString("yyyy.MM.dd");
                LabelExamerCount.Text = ExamResultDAL.SelectCount("and ExamplanID=" + Request["o"]).ToString();

                DataTable sub = ExamPlanSubjectDAL.GetListView(0, int.MaxValue - 1, "and ExamplanID=" + Request["o"], "PostName");
                bool IsFirstSetPassLine = true;//是否是第一次设置合格线

                System.Collections.Generic.Dictionary<string, string> dicKeMu = new Dictionary<string, string>();

                for (int i = 0; i < sub.Rows.Count; i++)
                {
                    dicKeMu.Add(sub.Rows[i]["PostID"].ToString(), sub.Rows[i]["PostName"].ToString());
                        
                    Literal li = new Literal();
                    PlaceHolderPassLine.Controls.Add(li);
                    li.Text = sub.Rows[i]["PostName"].ToString() + "：";

                    RadNumericTextBox rdntx = new RadNumericTextBox();
                    PlaceHolderPassLine.Controls.Add(rdntx);
                    rdntx.ID = "RadNumericTextBox" + sub.Rows[i]["PostID"].ToString();
                    //rdntx.DataType = typeof(number);
                    rdntx.Type = NumericType.Number;
                    rdntx.NumberFormat.DecimalDigits = 0;
                    rdntx.ShowSpinButtons = true;
                    rdntx.MaxValue = 1000;
                    rdntx.MinValue = 0;
                    rdntx.TextChanged += RadNumericTextBox_TextChanged;
                    rdntx.AutoPostBack = true;

                    if (i != sub.Rows.Count - 1)
                    {
                        System.Web.UI.HtmlControls.HtmlGenericControl br = new System.Web.UI.HtmlControls.HtmlGenericControl();
                        br.InnerHtml = "<br />";
                        PlaceHolderPassLine.Controls.Add(br);
                    }

                    if (sub.Rows[i]["PassLine"] == DBNull.Value)
                    {
                        IsFirstSetPassLine = true;
                        rdntx.Value = 60;
                    }
                    else
                    {
                        IsFirstSetPassLine = false;
                        rdntx.Value = Math.Round(Convert.ToDouble(sub.Rows[i]["PassLine"]), 0);
                        rdntx.BackColor = System.Drawing.Color.LightGreen;
                    }

                }
                ViewState["dicKeMu"] = dicKeMu;//科目集合
                if (IsFirstSetPassLine == true)
                {
                    ButtonSetPassline.Text = "设定合格线";
                    ComputeTempPassResult();
                }
                else
                {
                    ButtonSetPassline.Text = "修改合格线";
                    ComputePassResult();
                }

                //成绩公告记录数
                int publishCount = ExamResultDAL.SelectCount(string.Format(" and ExamPlanID={0} and [STATUS]='{1}'", Request["o"], EnumManager.ExamResultStatus.Published));
                if (publishCount > 0)
                {
                    if (IfSupperAdmin() == false)
                    {
                        ButtonSetPassline.Enabled = false;//公告后不能修改合格线
                        UIHelp.layerAlert(Page, "成绩已公告，不能修改合格线。如有特殊情况，请联系系统管理员修改。");
                    }
                    else
                    {
                        UIHelp.layerAlert(Page, "成绩已公告，修改合格线，已公告的成绩将被重新设置，请小心设置。");
                    }
                }

            }

            //多科目考试计划没有录入所有科目成绩，不允许设置合格线
            int count = ExamPlanDAL.CheckExamPlanScoreFinish(Convert.ToInt64(Request["o"]));
            if (count > 0)
            {
                div_tip.InnerText = string.Format("注意：本考试计划尚有{0}科成绩没有录入，请录入成绩后在设定合格线。（特例：特种作业可先只录入理论成绩并设定合格线，学员可以查看单科理论成绩,但不生成综合成绩）", count);
            }
            else
            {
                div_tip.InnerText = "";
            }

            base.Page_Init(sender, e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //改变了单科分数线
        protected void RadNumericTextBox_TextChanged(object sender, EventArgs e)
        {
            ComputeTempPassResult();
        }

        //设定分数线
        protected void ButtonSetPassline_Click(object sender, EventArgs e)
        {
            //多科目考试计划没有录入所有科目成绩，不允许设置合格线
            int count = ExamPlanDAL.CheckExamPlanScoreFinish(Convert.ToInt64(Request["o"]));//本考试计划尚未录入成绩科目数量

            //获取已录入成绩科目ID
            DataTable dt = CommonDAL.GetDataTable(string.Format("SELECT POSTID FROM DBO.EXAMSUBJECTRESULT where EXAMPLANID = {0} group  by POSTID  having count(*) >0",Request["o"]));
            dt.PrimaryKey = new DataColumn[] { dt.Columns[0] };

            Dictionary<string, string> dicKeMu=ViewState["dicKeMu"] as Dictionary<string, string>;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            DBHelper db = new DBHelper();
            DbTransaction trans = db.BeginTransaction();
            int subjectCount = 0;//考试科目数
            try
            {
                //保存合格线            
                foreach (Control ctl in PlaceHolderPassLine.Controls)
                {
                    if (ctl.GetType() == typeof(Telerik.Web.UI.RadNumericTextBox))
                    {
                        if(dt.Rows.Find(ctl.ID.Replace("RadNumericTextBox", "")) ==null)
                        {
                            continue;//尚未录入成绩，不保存合格线
                        }
                        ExamPlanSubjectOB _ExamPlanSubjectOB = ExamPlanSubjectDAL.GetObject(trans,Convert.ToInt64(Request["o"]), Convert.ToInt32(ctl.ID.Replace("RadNumericTextBox", "")));
                        _ExamPlanSubjectOB.PassLine = Convert.ToInt32((ctl as RadNumericTextBox).Value);
                        ExamPlanSubjectDAL.Update(trans, _ExamPlanSubjectOB);
                        ((RadNumericTextBox)ctl).BackColor = System.Drawing.Color.LightGreen;
                        subjectCount++;
                        sb.Append(string.Format("科目：{0}，合格线：{1}分。",dicKeMu[_ExamPlanSubjectOB.PostID.ToString()],_ExamPlanSubjectOB.PassLine.ToString()));
                    }
                }

                //生成综合成绩
                if (count == 0)//所有科目都已经录入成绩
                {
                    ExamResultDAL.UpdateExamResult(trans, Convert.ToInt64(Request["o"]), subjectCount);
                }
                trans.Commit();

                if (count == 0)
                {
                    UIHelp.WriteOperateLog(PersonName, UserID, "设定合格线", string.Format("岗位名称：{0}，工种名称：{1}，考试日期：{2}。{3}",
                    LabelPostTypeName.Text,
                    LabelPostName.Text,
                    LabelExamDate.Text,
                    sb.ToString()));

                    UIHelp.layerAlert(Page, "已经成功设定合格线，成绩已生成。", 6, 3000);
                }
                else
                {
                    UIHelp.WriteOperateLog(PersonName, UserID, "设定合格线（只保存已录入成绩科目的合格线，综合成绩尚生成）", string.Format("岗位名称：{0}，工种名称：{1}，考试日期：{2}。{3}",
                   LabelPostTypeName.Text,
                   LabelPostName.Text,
                   LabelExamDate.Text,
                   sb.ToString()));

                    UIHelp.layerAlert(Page, "因为存在科目尚未录入成绩，系统只保存已录入成绩科目的合格线，综合成绩尚生成（若特种作业录入了理论成绩并设定合格线，学员可以查看单科理论成绩）。", 6, 0);
                }
            }
            catch(Exception ex)
            {
                trans.Rollback();
                UIHelp.WriteErrorLog(Page, "设定合格线失败！", ex);
                return;
            }
        
            //刷新
            ButtonSetPassline.Text = "修改合格线";
            ComputePassResult();
        }

        //计算临时通过率
        protected void ComputeTempPassResult()
        {
            //拼凑临时合格线表（列：计划科目ID，合格线）
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            int subjectCount = 0;//考试科目数
            foreach (Control ctl in PlaceHolderPassLine.Controls)
            {
                if (ctl.GetType() == typeof(Telerik.Web.UI.RadNumericTextBox))
                {
                    sb.Append(string.Format(" union select cast({0} as bigint) as PostID,{1} as PassLine"
                        , ctl.ID.Replace("RadNumericTextBox", "")
                        , (ctl as RadNumericTextBox).Value.ToString()));
                    subjectCount++;
                }
            }
            if (sb.Length > 0) sb.Remove(0, 6);

            LabelPassCount.Text = ExamResultDAL.SelectTempPassCount(Convert.ToInt64(Request["o"]), subjectCount, sb.ToString());
            if (LabelPassCount.Text == "0（人）")
                LabelPassPercent.Text = "0%";
            else
                LabelPassPercent.Text = ExamResultDAL.SelectTempPassPercent(Convert.ToInt64(Request["o"]), subjectCount, sb.ToString());
         }

        //计算通过率
        protected void ComputePassResult()
        {
            int subjectCount = 0;//考试科目数
            foreach (Control ctl in PlaceHolderPassLine.Controls)
            {
                if (ctl.GetType() == typeof(Telerik.Web.UI.RadNumericTextBox))
                {
                    subjectCount++;
                }
            }

            LabelPassCount.Text = ExamResultDAL.SelectPassCount(Convert.ToInt64(Request["o"]), subjectCount);
            if (LabelPassCount.Text == "0（人）")
                LabelPassPercent.Text = "0%";
            else
                LabelPassPercent.Text = ExamResultDAL.SelectPassPercent(Convert.ToInt64(Request["o"]), subjectCount);

        }
    }
}
