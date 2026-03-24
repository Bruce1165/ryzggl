using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAccess;
using Model;
using Utility;
using Telerik.Web.UI;

namespace ZYRYJG.QuestionMgr
{
    public partial class ExamPagePrint : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "MainExamPage.aspx";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    //试卷信息
                    ExamPageOB _ExamPageOB = ExamPageDAL.GetObject(Convert.ToInt64(Request["o"]));

                    HiddenFieldCreateTime.Value = _ExamPageOB.CreateTime.Value.ToString("yyyy.MM");

                    //岗位工种信息
                    DataTable dtkm = CommonDAL.GetDataTable(0, 1, "dbo.VIEW_POSTINFO", "*", string.Format(" and SUBJECTID={0}", _ExamPageOB.SubjectID.ToString()), "SUBJECTID");

                    //试题类型设置
                    DataTable dtQuestionType = ExamPageQuestionTypeDAL.GetList(0, int.MaxValue - 1, string.Format(" and ExamPageID ={0}", Request["o"]), "ShowOrder");
                    DataColumn[] dc = new DataColumn[1];
                    dc[0] = dtQuestionType.Columns["QUESTIONTYPE"];
                    dtQuestionType.PrimaryKey = dc;

                    //试题
                    DataTable dtQuestion = PageQuestionDAL.GetList(0, int.MaxValue - 1, string.Format(" and ExamPageID={0}", Request["o"]), "QuestionNo");

                    string QuestionType = "";//试题类型
                    DataRow find = null;
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    //输出试卷说明
                    sb.Append(string.Format("<p><center><div><b><h2><span>北京市建筑业从业人员考试</span></h2></b></div></center></p><p><center><div><b><span>{0}  {1}  {2}</span></b></div></center></p>", dtkm.Rows[0]["PostTypeName"].ToString(), dtkm.Rows[0]["PostName"].ToString(), dtkm.Rows[0]["SubjectName"].ToString()));
                    sb.Append(string.Format("<p><center><div><b><span>（试卷满分：{0}分，考试时间：{1}分钟）</span></b></div></center></p>", _ExamPageOB.Score.ToString(), _ExamPageOB.TimeLimit.ToString()));
                    if (string.IsNullOrEmpty(_ExamPageOB.Remark) == false)
                    {
                        sb.Append(string.Format("<p><div><span>说明：<br/>{0}</span></div></p>", _ExamPageOB.Remark.Replace("\r\n", "<br/>")));
                    }

                    System.Text.StringBuilder sbAnswer = new System.Text.StringBuilder();
                    sbAnswer.Append(string.Format("<p><center><div><b><h2><span>北京市建筑业从业人员考试</span></h2></b></div></center></p><p><center><div><b><span>{0}</span></b></div></center></p><p><div><span>答案：</span></div></p><p><div><span>", _ExamPageOB.ExamPageTitle));

                    int rowno = 0;
                    foreach (DataRow dr in dtQuestion.Rows)
                    {
                        rowno++;
                        if (dr["QuestionType"].ToString() != QuestionType)
                        {
                            QuestionType = dr["QuestionType"].ToString();
                            find = dtQuestionType.Rows.Find(QuestionType);
                            if (find != null)
                            {
                                //输出试题类型说明      
                                sb.Append("<p><div><b><span>").Append(string.Format("{0}、{1}：（共{2}题，每题{3}分，共{4}分）{5}"
                                    , StringHelper.ToChinaNumber(Convert.ToInt32(find["SHOWORDER"]))
                                    , QuestionType
                                    , find["QUESTIONCOUNT"].ToString()
                                    , find["SCORE"].ToString()
                                    , Convert.ToString(Convert.ToInt32(find["QUESTIONCOUNT"]) * Convert.ToInt32(find["SCORE"]))
                                    , find["REMARK"].ToString().Replace("\r\n", "<br/>"))
                                    ).Append("</span></b></div></p>");
                            }
                        }
                        //输出试题
                        sb.Append(string.Format("<p><div><span>{0}、{3}{1}{2}</span></div></p>"
                            , dr["QuestionNo"].ToString()
                            , ClearLastReturn(dr["Title"].ToString()).Replace("\r\n", "<br/>")

                            , (QuestionType == "简答题") ? "<br/><br/><br/><br/><br/><br/><br/>" : ""//简答题预留7行空行。
                             , (QuestionType == "判断题") ? "（　）" : "")//判断题标题前统一添加括号（）。
                           
                            );

                        //答案
                        if (QuestionType == "简答题")
                        {
                            sbAnswer.Append(string.Format("<p><div>{0}、{1}</div></p>", dr["QuestionNo"].ToString(), dr["Answer"].ToString()));
                        }
                        else
                        {
                            sbAnswer.Append(
                                        string.Format("{4}{0}、{1}{2}{3}"
                                        , dr["QuestionNo"]
                                        , dr["Answer"]
                                        , "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;".Substring(0, (8 - dr["Answer"].ToString().Length) * 6)
                                        , (rowno % 5 == 0) ? "<br/>" : ""
                                        , "&nbsp;&nbsp;".Substring(0, (3 - dr["QuestionNo"].ToString().Length) * 6)
                                                        )
                                );
                        }
                    }

                    divExamPage.InnerHtml = sb.ToString();

                    sbAnswer.Append("</span></div></p>");
                    ViewState["Answer"] = sbAnswer;
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "读取试卷信息失败！", ex);
                    return;
                }
            }

        }

        /// <summary>
        /// 去掉最后的回车换行
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string ClearLastReturn(string str)
        {
            if (str.Length >= 4 && str.Substring(str.Length - 4) == "\r\n")
            {
                str.Remove(str.Length - 4);
            }
            return str;
        }

        //导出试卷
        protected void ButtonPrint_Click(object sender, EventArgs e)
        {
            //检查临时文件路径
            if (!System.IO.Directory.Exists(Page.Server.MapPath("~/UpLoad/ExamPagePrint/")))
            {
                System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/ExamPagePrint/"));
            }

            Dictionary<string, string> printData  = new Dictionary<string, string>();
            printData.Add("content", HtmlToXml(divExamPage.InnerHtml));
            printData.Add("CreateTime", HiddenFieldCreateTime.Value);

            Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, "~/Template/试卷打印.doc", string.Format("~/UpLoad/ExamPagePrint/{0}.doc", Request["o"]), printData);

            //Utility.WordDelHelp.ExportWord(this.Page, Server.MapPath(string.Format("~/UpLoad/ExamPagePrint/{0}.doc", Request["o"])));

            List<ResultUrl> url = new List<ResultUrl>();
            url.Add(new ResultUrl("试卷打印", string.Format("~/UpLoad/ExamPagePrint/{0}.doc", Request["o"])));
            UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);
        }

        //html标签转化为xml标签
        private string HtmlToXml(string html)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(html);

            System.Collections.Generic.Dictionary<string, string> ReplaceKey = new Dictionary<string, string>();

            //字体（单元属性）
            ReplaceKey.Add("<h2>", "<w:rPr><w:sz w:val=\"36\"/><w:sz-cs w:val=\"36\"/></w:rPr>");
            ReplaceKey.Add("</h2>", "");

            //居中（段落属性）
            ReplaceKey.Add("<center>", "<w:pPr><w:jc w:val=\"center\"/></w:pPr>");
            ReplaceKey.Add("</center>", "");

            //粗体（单元属性）
            ReplaceKey.Add("<b>", "<w:pPr><w:rPr><w:b/></w:rPr></w:pPr>");
            ReplaceKey.Add("</b>", "");

            //空格
            ReplaceKey.Add("&nbsp;", " ");

            //换行
            ReplaceKey.Add("<br/>", "</w:t></w:r></w:p><w:p><w:r><w:t>");
            ReplaceKey.Add("<BR/>", "</w:t></w:r></w:p><w:p><w:r><w:t>");
            ReplaceKey.Add("<br>", "</w:t></w:r></w:p><w:p><w:r><w:t>");
            ReplaceKey.Add("<BR>", "</w:t></w:r></w:p><w:p><w:r><w:t>");

            //文本单元
            ReplaceKey.Add("<span>", "<w:t>");
            ReplaceKey.Add("</span>", "</w:t>");

            //文本单元属性
            ReplaceKey.Add("<div>", "<w:r>");
            ReplaceKey.Add("</div>", "</w:r>");

            //段落
            ReplaceKey.Add("<p></p>", "");
            ReplaceKey.Add("<p>", "</w:t></w:r></w:p><w:p>");
            ReplaceKey.Add("</p>", "</w:p><w:p><w:r><w:t>");
            ReplaceKey.Add("<w:p><w:r><w:t></w:t></w:r></w:p>", "");
            foreach (string k in ReplaceKey.Keys)
            {
                sb.Replace(k, ReplaceKey[k]);
            }

            return sb.ToString();
        }

        // 导出答案
        protected void ButtonAnwser_Click(object sender, EventArgs e)
        {
            //检查临时文件路径
            if (!System.IO.Directory.Exists(Page.Server.MapPath("~/UpLoad/ExamPagePrint/")))
            {
                System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/ExamPagePrint/"));
            }

            Dictionary<string, string> printData = new Dictionary<string, string>();

            printData.Add("content", HtmlToXml((ViewState["Answer"] as System.Text.StringBuilder).ToString()));

            Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, "~/Template/试卷答案.doc", string.Format("~/UpLoad/ExamPagePrint/{0}_da.doc", Request["o"]), printData);

            //Utility.WordDelHelp.ExportWord(this.Page, Server.MapPath(string.Format("~/UpLoad/ExamPagePrint/{0}_da.doc", Request["o"])));

            List<ResultUrl> url = new List<ResultUrl>();
            url.Add(new ResultUrl("试卷答案", string.Format("~/UpLoad/ExamPagePrint/{0}_da.doc", Request["o"])));
            UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);
        }

    }
}
