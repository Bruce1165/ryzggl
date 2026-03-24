using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.IO;
using DataAccess;
using Model;

namespace ZYRYJG.jxjy
{
    public partial class QuestionView : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "QuestionMgr.aspx";
            }
        }
        /// <summary>
        /// 当前试题ID
        /// </summary>
        public long? QuestionID
        {
            get
            {
                if (ViewState["QuestionID"] == null)
                    return null;
                else
                    return Convert.ToInt64(ViewState["QuestionID"]);
            }
            set { ViewState["QuestionID"] = value; }
        }

        /// <summary>
        /// 当前试题编号
        /// </summary>
        public string QuestionNo
        {
            get { return Convert.ToString(ViewState["QuestionNo"]); }
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
        /// 试题类型
        /// </summary>
        public string QuestionType
        {
            get { return Convert.ToString(ViewState["QuestionType"]); }
            set { ViewState["QuestionType"] = value; }
        }

        /// <summary>
        /// 试题id集合
        /// </summary>
        public List<long> ListQuestion
        {
            get { return ViewState["ListQuestion"] as List<long>; }
            set { ViewState["ListQuestion"] = value; }
        }
        /// <summary>
        /// 当前显示证书索引，从0开始
        /// </summary>
        public int CurrentIndex
        {
            get { return Convert.ToInt32(ViewState["CurrentIndex"]); }
            set
            {
                ViewState["CurrentIndex"] = value;
                if (ListQuestion.Count < 2)
                {
                    ButtonNext.Visible = false;
                    ButtonPrev.Visible = false;
                    LabelPage.Visible = false;
        
                }
                else
                {         
                    ButtonNext.Visible = true;
                    ButtonPrev.Visible = true;
                    LabelPage.Visible = true;
                    LabelPage.Text = string.Format("第 {1} 题   共 {0} 题", ListQuestion.Count, Convert.ToString(value + 1));

                    if (value == 0)
                        ButtonPrev.Enabled = false;
                    else
                        ButtonPrev.Enabled = true;

                    if (value == ListQuestion.Count - 1)
                        ButtonNext.Enabled = false;
                    else
                        ButtonNext.Enabled = true;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request["o"]) == false)//now,QuestionType,QuestionID              
                {
                    string[] q = Utility.Cryptography.Decrypt(Request["o"]).Split(',');
                    //RadComboBoxItem f= RadComboBoxQuestionType.FindItemByText(q[1]);
                    //if(f !=null)
                    //{
                    //    f.Selected = true;
                    //}

                    TrainQuestionMDL _questionOB = TrainQuestionDAL.GetObject(Convert.ToInt64(q[2]));
                    if (_questionOB != null)
                    {
                        QuestionID = _questionOB.QuestionID.Value;
                        SourceMDL s = SourceDAL.GetObject(_questionOB.SourceID.Value);
                        if (s != null)
                        {
                            SelectSource.SetValue(s);
                        }
                    }
                    
                }
                ButtonSearch_Click(sender, e);
            }
        }

       

        //查询
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            QueryParamOB q = new QueryParamOB();
            if (RadComboBoxQuestionType.SelectedValue != "")
            {
                q.Add(string.Format("QuestionType = '{0}'", RadComboBoxQuestionType.SelectedValue));//分类
            }
            if (SelectSource.SourceID.HasValue == true)
            {
                q.Add(string.Format("SourceID = {0}", SelectSource.SourceID));//课程ID
            }
            DataTable dt = CommonDAL.GetDataTableDB("DBRYPX", string.Format("Select [QuestionID] FROM [dbo].[TrainQuestion] where 1=1 {0} order by [SourceID] desc,[QuestionType],[QuestionNo]", q.ToWhereString()));
            List<long> list = new List<long>();
            int cur=0;
            for(int i=0;i<dt.Rows.Count;i++)
            {
                list.Add(Convert.ToInt64(dt.Rows[i]["QuestionID"]));
                if(QuestionID.HasValue && QuestionID==Convert.ToInt64(dt.Rows[i]["QuestionID"]))
                {
                    cur = i;
                }
            }

            ListQuestion = list;
            if (list.Count > 0)
            {
                RefreshQuestion(cur);
                ButtonEdit.Enabled = true;
            }
            else
            {
                LabelPage.Text = "";
                DivQuestion.InnerHtml = "";
                RadGridQuestOption.DataSource = null;
                RadGridQuestOption.DataBind();

                ButtonPrev.Enabled = false;
                ButtonNext.Enabled = false;
                ButtonEdit.Enabled = false;
            }
            QuestionID = null;
        }

        //绑定试题选项
        private void RefreshQuestion(int index)
        {
            CurrentIndex = index;

            TrainQuestionMDL _questionOB = TrainQuestionDAL.GetObject(ListQuestion[index]);
            Answer = _questionOB.Answer;
            QuestionNo = _questionOB.QuestionNo;
            QuestionType = _questionOB.QuestionType;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(string.Format("<p>试题类型：<b>{2}</b>&nbsp;&nbsp;&nbsp;&nbsp;试题编号：<b>{3}</b>&nbsp;&nbsp;&nbsp;&nbsp;标准答案：（<b>{4}</b>）<br/><br/><b>{0}、{1}</b></p>", index + 1, _questionOB.Title, _questionOB.QuestionType, _questionOB.QuestionNo, _questionOB.Answer));

            DataTable dt = CommonDAL.GetDataTableDB("DBRYPX", string.Format("select * from dbo.TrainQuestOption where QuestionID={0} order by OptionNo", _questionOB.QuestionID));

            DivQuestion.InnerHtml = sb.ToString();
            RadGridQuestOption.DataSource = dt;
            RadGridQuestOption.DataBind();

           

        }

        //上一题
        protected void ButtonPrev_Click(object sender, EventArgs e)
        {
            RefreshQuestion(CurrentIndex - 1);
        }
        //下一题
        protected void ButtonNext_Click(object sender, EventArgs e)
        {
            if (CompareAnswer() == false)
            {
                DivQuestionAnswerTip.InnerHtml += string.Format("<p>试题编号：{0} 答案无法匹配。</p>", QuestionNo);
                errowCount += 1;
            }
            switch (QuestionType)
            {
                case "单选题":
                case "多选题":
                    if (RadGridQuestOption.Items.Count < 4)
                    {
                        DivQuestionAnswerTip.InnerHtml += string.Format("<p>试题编号：{0} 待选项数量疑似存在问题。</p>", QuestionNo);
                        errowCount += 1;
                    }
                    break;
                case "判断题":
                    if (RadGridQuestOption.Items.Count != 2)
                    {
                        DivQuestionAnswerTip.InnerHtml += string.Format("<p>试题编号：{0} 待选项数量疑似存在问题。</p>", QuestionNo);
                        errowCount += 1;
                    }
                    break;
            }

            RefreshQuestion(CurrentIndex + 1);
        }
        //编辑试题
        protected void ButtonEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("QuestionEdit.aspx?o={0}", ListQuestion[CurrentIndex]), true);
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

        //自动选中答案
        protected void RadGridQuestOption_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                if (Answer.Contains(RadGridQuestOption.MasterTableView.DataKeyValues[e.Item.ItemIndex]["OptionNo"].ToString()) == true)
                {
                    (e.Item.FindControl("CheckBox1") as CheckBox).Checked = true;
                }
            }

        }

        private int errowCount=0;//错误数量
        //扫描试题错误
        protected void ButtonCheck_Click(object sender, EventArgs e)
        {
            DivQuestionAnswerTip.InnerHtml = "";
            errowCount = 0;
            int i = 0;
            DivQuestionAnswerTip.InnerHtml += string.Format("<p>{0}开始扫描</p>", DateTime.Now);
            while (ButtonNext.Enabled == true && i < ListQuestion.Count)
            {
                ButtonNext_Click(sender, e);
                i++;
            }

            DivQuestionAnswerTip.InnerHtml += string.Format("<p>{0}扫描结束，发现{1}处错误。</p>", DateTime.Now, errowCount);
        }
    }
}