using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using System.Data;

public partial class Student_ExamStep : BasePage
{
    protected override string CheckVisiteRgihtUrl
    {
        get
        {
            return "WebClass.aspx";
        }
    }
    /// <summary>
    /// 未答试题id集合
    /// </summary>
    public List<long> ListQuestion
    {
        get { return ViewState["ListQuestion"] as List<long>; }
        set { ViewState["ListQuestion"] = value; }
    }

    /// <summary>
    /// 考试开始时间
    /// </summary>
    public DateTime ExamStartTime
    {
        get { return Convert.ToDateTime(ViewState["ExamStartTime"]); }
        set { ViewState["ExamStartTime"] = value; }
    }

    /// <summary>
    /// 考试结束时间
    /// </summary>
    public DateTime ExamEndTime
    {
        get { return Convert.ToDateTime(ViewState["ExamStartTime"]).AddMinutes(QuestionCount); }
    }

    /// <summary>
    /// 试题总条数
    /// </summary>
    public int QuestionCount
    {
        get { return Convert.ToInt32(ViewState["QuestionCount"]); }
        set { ViewState["QuestionCount"] = value; }
    }

    /// <summary>
    /// 当前题号
    /// </summary>
    public int QuestionNo
    {
        get { return Convert.ToInt32(ViewState["QuestionNo"]); }
        set { ViewState["QuestionNo"] = value; }
    }

    /// <summary>
    /// 当前试题标准答案
    /// </summary>
    public string Answer
    {
        get { return Convert.ToString(ViewState["Answer"]); }
        set { ViewState["Answer"] = value; }
    }

    /// <summary>
    /// 答对题数
    /// </summary>
    public int passQuestionCount
    {
        get { return Convert.ToInt32(ViewState["passQuestionCount"]); }
        set { ViewState["passQuestionCount"] = value; }
    }

    public long PackageID
    {
        get { return Convert.ToInt64(ViewState["PackageID"]); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //ViewState["PackageID"] = Request["o"];

            //PackageMDL o = PackageDAL.GetObject(PackageID);


            ////判断题
            //DataTable dtPD = null;
            //if (Cache[string.Format("dtPD{0}", o.PostName)] == null)
            //{
            //    if (string.IsNullOrEmpty(o.PostName) == true)
            //    {
            //        dtPD = TrainQuestionDAL.GetList(0, int.MaxValue - 1, string.Format(" and Flag='启用' and QuestionType='判断题' and ForPostTypeName='{0}'", o.PostTypeName), "");
            //    }
            //    else
            //    {
            //        dtPD = TrainQuestionDAL.GetList(0, int.MaxValue - 1, string.Format(" and Flag='启用' and QuestionType='判断题' and ((ForPostTypeName='{0}' and ForPostName is null) or ForPostName='{1}' )", o.PostTypeName, o.PostName), "");
            //    }
            //    Cache[string.Format("dtPD{0}", o.PostName)] = dtPD.Copy();
            //}
            //else
            //{
            //    dtPD = ((DataTable)Cache[string.Format("dtPD{0}", o.PostName)]).Copy();
            //}

            ////选择题
            //DataTable dtXZ = null;
            //if (Cache[string.Format("dtXZ{0}", o.PostName)] == null)
            //{
            //    if (string.IsNullOrEmpty(o.PostName) == true)
            //    {
            //        dtXZ = TrainQuestionDAL.GetList(0, int.MaxValue - 1, string.Format(" and Flag='启用' and QuestionType='选择题' and ForPostTypeName='{0}'", o.PostTypeName), "");
            //    }
            //    else
            //    {
            //        dtXZ = TrainQuestionDAL.GetList(0, int.MaxValue - 1, string.Format(" and Flag='启用' and QuestionType='选择题' and ((ForPostTypeName='{0}' and ForPostName is null) or ForPostName='{1}' )", o.PostTypeName, o.PostName), "");
            //    }
            //    Cache[string.Format("dtXZ{0}", o.PostName)] = dtXZ.Copy();
            //}
            //else
            //{
            //    dtXZ = ((DataTable)Cache[string.Format("dtXZ{0}", o.PostName)]).Copy();
            //}

            ////准备试题
            //GetQuestionsRadom(o.PostName, dtXZ, dtPD);

            //DivTip.InnerText = string.Format("试题总数：{0}道（判断题{4}道，选择题{3}道），剩余试题数{1}道。考试时长：{2}。", QuestionCount.ToString(), ListQuestion.Count.ToString(), string.Format("{0}分钟", QuestionCount.ToString()), Convert.ToString(QuestionCount / 2), Convert.ToString(QuestionCount / 2));

        }
    }

    private int GetExamQuestionCount(string PostName)
    {
        int i = 0;
        switch (PostName)
        {
            case "企业主要负责人":
                i = 15;
                break;
            case "项目负责人":
                i = 20;
                break;
            case "机械类专职安全生产管理人员":
            case "土建类专职安全生产管理人员":
            case "综合类专职安全生产管理人员":
                i = 25;
                break;
            default:
                i = 2;
                break;


        }
        return i;
    }

    /// <summary>
    /// 随即准备试题
    /// </summary>
    /// <param name="PostName">岗位工种</param>
    /// <param name="dtXZ">选择题库</param>
    /// <param name="dtPD">判断题库</param>
    private void GetQuestionsRadom(string PostName, DataTable dtXZ, DataTable dtPD)
    {
        int i = 0;
        switch (PostName)
        {
            case "企业主要负责人":
                i = 15;
                break;
            case "项目负责人":
                i = 20;
                break;
            case "机械类专职安全生产管理人员":
            case "土建类专职安全生产管理人员":
            case "综合类专职安全生产管理人员":
                i = 25;
                break;
            default:
                i = 2;
                break;
        }
        List<long> list = new List<long>();
        System.Random myRandom = new Random();
        int index = 0;
        if (dtXZ.Rows.Count >= i && dtPD.Rows.Count > i)
        {
            for (int j = 0; j < i; j++)
            {
                //选择题
                index = myRandom.Next(0, dtXZ.Rows.Count - 1);
                list.Add(Convert.ToInt64(dtXZ.Rows[index]["QuestionID"]));
                dtXZ.Rows.RemoveAt(index);

                //判断题
                index = myRandom.Next(0, dtPD.Rows.Count - 1);
                list.Insert(0, Convert.ToInt64(dtPD.Rows[index]["QuestionID"]));
                dtPD.Rows.RemoveAt(index);
            }
        }
        ListQuestion = list;
        QuestionCount = list.Count;
        QuestionNo = 0;
    }

    //更新考试结果
    private void UpdateExamResult()
    {
        if (passQuestionCount > 1 && passQuestionCount >= (QuestionCount * 0.9))
        {
            //StudyPlanMDL o = StudyPlanDAL.GetObject(WorkerCertificateCode, PackageID);
            //o.TestStatus = EnumManager.StudyTestStatus.Passed;
            //o.FinishDate = DateTime.Now;
            //StudyPlanDAL.Update(o);
        }
    }

    private void RefreshQuestion()
    {
        if (ButtonSave.Text != "开始考试" && ExamEndTime < DateTime.Now)
        {
            if (CompareAnswer() == true)
            {
                passQuestionCount += 1;
            }
            DivTip.InnerText = string.Format("试题总数：{0}道（判断题{5}道，选择题{4}道），剩余试题数{1}道。考试时长：{2}，考试开始时间：{3}。", QuestionCount.ToString(), ListQuestion.Count.ToString(), string.Format("{0}分钟", QuestionCount.ToString()), ExamStartTime.ToString("HH:mm:ss"), Convert.ToString(QuestionCount / 2), Convert.ToString(QuestionCount / 2));
            DivQuestion.InnerHtml = string.Format("<p>{0}您答对{1}道试题，考试结果：{2}。</p>", "考试结束。", passQuestionCount.ToString()
                , (passQuestionCount > 0 && passQuestionCount >= (QuestionCount * 0.9)) ? "合格，退出考试页面打印合格证明。" : "不合格，请及时复习相关知识，重新发起在线考试，直到通过考试。");
            RadGridQuestOption.DataSource = null;
            RadGridQuestOption.DataBind();
            ButtonSave.Text = "考试结束";
            UpdateExamResult();
            ButtonSave.Enabled = false;
            if (passQuestionCount >= (QuestionCount * 0.9))//考试合格,刷新
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "fresh", "var isfresh = true; ", true);
            }
            return;
        }
        if (RadGridQuestOption.Items.Count > 0 && UIHelp.IsSelected(RadGridQuestOption) == false)
        {
            UIHelp.layerAlert(Page, "请选择一个答案！");
            return;
        }
        if (ListQuestion.Count == 0)
        {
            if (CompareAnswer() == true)
            {
                passQuestionCount += 1;
            }
            DivTip.InnerText = string.Format("试题总数：{0}道（判断题{5}道，选择题{4}道），剩余试题数{1}道。考试时长：{2}，考试开始时间：{3}。", QuestionCount.ToString(), ListQuestion.Count.ToString(), string.Format("{0}分钟", QuestionCount.ToString()), ExamStartTime.ToString("HH:mm:ss"), Convert.ToString(QuestionCount / 2), Convert.ToString(QuestionCount / 2));
            DivQuestion.InnerHtml = string.Format("<p>{0}您答对{1}道试题，考试结果：{2}</p>", "考试结束。", passQuestionCount.ToString()
                , (passQuestionCount > 0 && passQuestionCount >= (QuestionCount * 0.9)) ? "合格，退出考试页面。" : "不合格，请及时复习相关知识，重新发起在线考试，直到通过考试。");
            RadGridQuestOption.DataSource = null;
            RadGridQuestOption.DataBind();
            ButtonSave.Text = "考试结束";
            UpdateExamResult();
            ButtonSave.Enabled = false;
            ButtonSave.CssClass = "bt_large  btn_no";
           
            if (passQuestionCount >= (QuestionCount * 0.9))//考试合格,刷新
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "fresh", "var isfresh = true; ", true);
            }
            return;
        }
        else
        {
            if (ButtonSave.Text == "开始考试")
            {            
                passQuestionCount = 0;
                ExamStartTime = DateTime.Now;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", string.Format(" window.setInterval(function () {{ ShowCountDown({0},{1},{2},{3},{4},{5}, 'showData'); }}, interval); ", ExamEndTime.Year.ToString(), Convert.ToString(ExamEndTime.Month - 1), ExamEndTime.Day.ToString(), ExamEndTime.ToString("HH"), ExamEndTime.ToString("mm"), ExamEndTime.ToString("ss")), true);
                ButtonSave.Text = "确 定";
            }
            else if (ButtonSave.Text == "继 续")
            {
                DivQuestionAnswerTip.InnerText = "";
                ButtonSave.Text = "确 定";
            }
            else
            {
                if (CompareAnswer() == true)
                {
                    passQuestionCount += 1;
                }
                else
                {
                    DivQuestionAnswerTip.InnerText = string.Format("您的回答错误，正确答案应为：{0}", Answer);
                    ButtonSave.Text = "继 续";
                    return;
                }
            }
            QuestionNo += 1;

            DivTip.InnerText = string.Format("试题总数：{0}道（判断题{5}道，选择题{4}道），剩余试题数{1}道。考试时长：{2}，考试开始时间：{3}。"
               , QuestionCount.ToString(), ListQuestion.Count.ToString(), string.Format("{0}分钟", QuestionCount.ToString()), ExamStartTime.ToString("HH:mm:ss"), Convert.ToString(QuestionCount / 2), Convert.ToString(QuestionCount / 2));

        }
        TrainQuestionMDL _questionOB = TrainQuestionDAL.GetObject(ListQuestion[0]);
        Answer = _questionOB.Answer;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //sb.Append(string.Format("<p>{2}</p><p>{0}、{1}</p>", QuestionNo.ToString(), _questionOB.Title, _questionOB.QuestionType));
        sb.Append(string.Format("<p>{2}<br/><b>{0}、{1}</b></p>", QuestionNo.ToString(), _questionOB.Title, _questionOB.QuestionType));

        DataTable dt = CommonDAL.GetDataTableDB("DBRYPX", string.Format("select * from dbo.TrainQuestOption where QuestionID={0} order by OptionNo", _questionOB.QuestionID));        

        DivQuestion.InnerHtml = sb.ToString();
        RadGridQuestOption.DataSource = dt;
        RadGridQuestOption.DataBind();

        ListQuestion.RemoveAt(0);

    }

    /// <summary>
    /// 判断当前选择答案是否正确
    /// </summary>
    /// <returns></returns>
    private bool CompareAnswer()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        CheckBox cbox = null;
        for (int i = 0; i <= RadGridQuestOption.Items.Count - 1; i++)
        {
            cbox = (System.Web.UI.WebControls.CheckBox)RadGridQuestOption.Items[i].FindControl("CheckBox1");
            if (cbox.Checked == true)
            {
                sb.Append(RadGridQuestOption.MasterTableView.DataKeyValues[i]["OptionNo"].ToString());
            }
        }

        if (sb.Length > 0 && sb.ToString() == Answer)
            return true;
        else
            return false;
    }

    //保存
    protected void ButtonSave_Click(object sender, EventArgs e)
    {
        //ListQuestion.RemoveAt(0);
        RefreshQuestion();

        //switch (ButtonSave.Text)
        //{
        //    case "开始考试":
        //        ScriptManager.RegisterStartupScript(Page, this.GetType(), "clock", " window.setInterval(function () { ShowCountDown(2013, 7, 22,13,52 'showData'); }, interval); ", true);
        //        ButtonSave.Text = "确 定";
        //        break;
        //    case "确 定":
        //        break;

        //}
    }
}