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
    public partial class ExamPageEdit : BasePage
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
                //试题类型设置
                DataTable dt = CommonDAL.GetDataTable(0, int.MaxValue - 1
                    , string.Format("DBO.TYPES left join dbo.ExamPageQuestionType  on TYPEID='107' and TYPENAME = QuestionType and ExamPageID ={0}", string.IsNullOrEmpty(Request["o"]) == false ? Request["o"] : "0")
                    , "TYPENAME,TYPEVALUE,SortID,ExamPageQuestionType.*"
                    , " and TYPEID='107'", "ShowOrder,SortID");
                RadGridQuestionType.DataSource = dt;
                RadGridQuestionType.DataBind();


                if (string.IsNullOrEmpty(Request["o"]) == false)//update
                {
                    ExamPageOB o = ExamPageDAL.GetObject(Convert.ToInt64(Request["o"]));
                    if (o != null)
                    {
                        ViewState["ExamPageOB"] = o;
                        //UIHelp.SetData(divExamPage, o);
                        PostSelect.PostTypeID = Request["ptid"];
                        PostSelect.PostID = Request["pid"];
                        PostSelect_OnPostSelectChange(sender, e);

                        //RadioButtonListKeMuID.SelectedValue = o.SubjectID.ToString();
                        RadioButtonListKeMuID.Items.FindByValue(o.SubjectID.ToString()).Selected = true;
                        if (string.IsNullOrEmpty(o.Difficulty) == false) RadNumericTextBoxDifficulty.Value = Convert.ToDouble(o.Difficulty);
                        RadTextBoxExamPageTitle.Text = o.ExamPageTitle;
                        RadNumericTextBoxYear.Value = o.ExamYear;
                        RadTextBoxRemark.Text = o.Remark;
                        RadNumericTextBoxScore.Value = o.Score;
                        RadNumericTextBoxTimeLimit.Value = o.TimeLimit;

                        //<%--Value='<%# Eval("QuestionCount") ==DBNull.Value?double.NaN:Convert.ToDouble(Eval("QuestionCount"))%>'--%>
                        //试卷试题属性
                        RadNumericTextBox _RadNumericTextBoxShowOrder = null;
                        RadNumericTextBox _RadNumericTextBoxQuestionCount = null;
                        RadNumericTextBox _RadNumericTextBoxScore = null;
                        RadTextBox _RadTextBoxRemark = null;
                        CheckBox cbox = null;
                        foreach (GridItem i in RadGridQuestionType.MasterTableView.Items)
                        {
                            //选中状态
                            cbox = (CheckBox)i.FindControl("CheckBox1");

                            //试题数量
                            _RadNumericTextBoxQuestionCount = i.Cells[RadGridQuestionType.MasterTableView.Columns.FindByUniqueName("QuestionCount").OrderIndex].Controls[1] as RadNumericTextBox;
                            if (RadGridQuestionType.MasterTableView.DataKeyValues[i.ItemIndex]["QuestionCount"] != DBNull.Value)
                            {
                                _RadNumericTextBoxQuestionCount.Value = Convert.ToDouble(RadGridQuestionType.MasterTableView.DataKeyValues[i.ItemIndex]["QuestionCount"]);
                                cbox.Checked = true;
                            }

                            //出题顺序
                            _RadNumericTextBoxShowOrder = i.Cells[RadGridQuestionType.MasterTableView.Columns.FindByUniqueName("ShowOrder").OrderIndex].Controls[1] as RadNumericTextBox;
                            if (RadGridQuestionType.MasterTableView.DataKeyValues[i.ItemIndex]["ShowOrder"] != DBNull.Value)
                            {
                                _RadNumericTextBoxShowOrder.Value = Convert.ToDouble(RadGridQuestionType.MasterTableView.DataKeyValues[i.ItemIndex]["ShowOrder"]);
                            }

                            //每题分数
                            _RadNumericTextBoxScore = i.Cells[RadGridQuestionType.MasterTableView.Columns.FindByUniqueName("Score").OrderIndex].Controls[1] as RadNumericTextBox;
                            if (RadGridQuestionType.MasterTableView.DataKeyValues[i.ItemIndex]["Score"] != DBNull.Value)
                            {
                                _RadNumericTextBoxScore.Value = Convert.ToDouble(RadGridQuestionType.MasterTableView.DataKeyValues[i.ItemIndex]["Score"]);
                            }

                            //答题说明
                            _RadTextBoxRemark = i.Cells[RadGridQuestionType.MasterTableView.Columns.FindByUniqueName("Remark").OrderIndex].Controls[1] as RadTextBox;
                            if (RadGridQuestionType.MasterTableView.DataKeyValues[i.ItemIndex]["Remark"] != DBNull.Value)
                            {
                                _RadTextBoxRemark.Text = RadGridQuestionType.MasterTableView.DataKeyValues[i.ItemIndex]["Remark"].ToString();
                            }
                        }
                    }

                    //试题出题难度统计
                    BindRadGridQuestionCount(Convert.ToInt64(Request["o"]));

                    //已出试题
                    BindRadGridQuestion(Convert.ToInt64(Request["o"]));

                    RadTabStrip1.Visible = true;
                }
                else//new
                {
                    RadTabStrip1.Visible = false;
                    RadNumericTextBoxYear.Value = DateTime.Now.Year;
                    RadTextBoxRemark.Text = "1、本考试为闭卷考试。\r\n2、应试人员答题前，务必将自己的姓名、准考证号用签字笔按要求填写在机读卡的指定位置，并用2B铅笔按照填涂样例将与准考证号对应的数字涂黑。\r\n3、用2B铅笔在答题卡上作答，未在规定的答题区域作答或超出答题区域作答均不计分。\r\n4、试题、答题卡一律不得带出考场。";
                }
            }
        }

        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder rtnErr = new System.Text.StringBuilder();//表单数据错误信息
            System.Text.StringBuilder rowErr = new System.Text.StringBuilder();//行数据错误信息
            RadNumericTextBox _RadNumericTextBoxShowOrder = null;
            RadNumericTextBox _RadNumericTextBoxQuestionCount = null;
            RadNumericTextBox _RadNumericTextBoxScore = null;
            RadTextBox _RadTextBoxRemark = null;
            CheckBox cbox = null;
            string QuestionType = "";//试题类型
            int Score = 0;//试题总分

            #region 有效检查

            if (RadioButtonListKeMuID.SelectedValue == "")
            {
                UIHelp.layerAlert(Page, "请选择一个考试科目！");
                return;
            }

            //试卷试题属性
            foreach (GridItem i in RadGridQuestionType.MasterTableView.Items)
            {
                cbox = (CheckBox)i.FindControl("CheckBox1");
                if (cbox.Checked == false) continue;

                rowErr.Remove(0, rowErr.Length);//行错误清空

                QuestionType = RadGridQuestionType.MasterTableView.DataKeyValues[i.ItemIndex]["TYPENAME"].ToString();//试题类型

                //试题数量
                _RadNumericTextBoxQuestionCount = i.Cells[RadGridQuestionType.MasterTableView.Columns.FindByUniqueName("QuestionCount").OrderIndex].Controls[1] as RadNumericTextBox;
                if (_RadNumericTextBoxQuestionCount.Value.HasValue == false)
                {
                    rowErr.Append(string.Format("请输入{0}试题数量！", QuestionType));
                }

                //出题顺序
                _RadNumericTextBoxShowOrder = i.Cells[RadGridQuestionType.MasterTableView.Columns.FindByUniqueName("ShowOrder").OrderIndex].Controls[1] as RadNumericTextBox;
                if (_RadNumericTextBoxShowOrder.Value.HasValue == false)
                {
                    rowErr.Append(string.Format("请输入{0}出题顺序", QuestionType));
                }

                //每题分数
                _RadNumericTextBoxScore = i.Cells[RadGridQuestionType.MasterTableView.Columns.FindByUniqueName("Score").OrderIndex].Controls[1] as RadNumericTextBox;
                if (_RadNumericTextBoxScore.Value.HasValue == false)
                {
                    rowErr.Append(string.Format("请输入{0}每题分数！", QuestionType));
                }

                //计算试题总分
                if (_RadNumericTextBoxQuestionCount.Value.HasValue == true && _RadNumericTextBoxScore.Value.HasValue == true)
                {
                    Score += Convert.ToInt32(_RadNumericTextBoxQuestionCount.Value * _RadNumericTextBoxScore.Value);
                }

                if (rowErr.Length > 0)
                {
                    rtnErr.Append(rowErr.ToString());
                }
            }

            if (Score != Convert.ToInt32(RadNumericTextBoxScore.Value))
            {
                rtnErr.Append("<br />试卷总分【").Append(RadNumericTextBoxScore.Value.ToString()).Append("】与试题总分【").Append(Score.ToString()).Append("】不一致，请调整一致后再保存！");
            }

            if (rtnErr.Length > 0)
            {
                UIHelp.layerAlert(Page, rtnErr.ToString());
                return;
            }

            #endregion 有效检查

            ExamPageOB ob = ViewState["ExamPageOB"] == null ? new ExamPageOB() : (ExamPageOB)ViewState["ExamPageOB"];
            ob.SubjectID = Convert.ToInt32(RadioButtonListKeMuID.SelectedValue);
            //ob.Difficulty = RadNumericTextBoxDifficulty.Value
            ob.ExamPageTitle = RadTextBoxExamPageTitle.Text.Trim();
            ob.ExamYear = Convert.ToInt32(RadNumericTextBoxYear.Value);
            ob.Remark = RadTextBoxRemark.Text.Trim();
            ob.Score = Convert.ToInt32(RadNumericTextBoxScore.Value);
            ob.TimeLimit = Convert.ToInt32(RadNumericTextBoxTimeLimit.Value);
            ob.Flag = "1";//有效
            if (ViewState["ExamPageOB"] == null)//new
            {
                ob.CreatePersonID = PersonID;
                ob.CreateTime = DateTime.Now;
            }
            ob.ModifyPersonID = PersonID;
            ob.ModifyTime = ob.CreateTime;

            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            try
            {
                //试卷属性
                if (ViewState["ExamPageOB"] == null)//new
                {
                    ExamPageDAL.Insert(tran, ob);
                }
                else//update
                {
                    ExamPageDAL.Update(tran, ob);
                    ExamPageQuestionTypeDAL.Delete(tran, ob.ExamPageID.Value);
                }

                //试卷试题属性
                foreach (GridItem i in RadGridQuestionType.MasterTableView.Items)
                {
                    cbox = (CheckBox)i.FindControl("CheckBox1");
                    if (cbox.Checked == false) continue;

                    ExamPageQuestionTypeOB o = new ExamPageQuestionTypeOB();
                    o.ExamPageID = ob.ExamPageID;
                    o.QuestionType = RadGridQuestionType.MasterTableView.DataKeyValues[i.ItemIndex]["TYPENAME"].ToString();//试题类型

                    //试题数量
                    _RadNumericTextBoxQuestionCount = i.Cells[RadGridQuestionType.MasterTableView.Columns.FindByUniqueName("QuestionCount").OrderIndex].Controls[1] as RadNumericTextBox;
                    if (_RadNumericTextBoxQuestionCount.Value.HasValue == false)
                    {
                        UIHelp.layerAlert(Page, string.Format("请输入{0}试题数量！", o.QuestionType));
                        tran.Rollback();
                        return;
                    }
                    o.QuestionCount = Convert.ToInt32(_RadNumericTextBoxQuestionCount.Value);

                    //出题顺序
                    _RadNumericTextBoxShowOrder = i.Cells[RadGridQuestionType.MasterTableView.Columns.FindByUniqueName("ShowOrder").OrderIndex].Controls[1] as RadNumericTextBox;
                    if (_RadNumericTextBoxShowOrder.Value.HasValue == false)
                    {
                        UIHelp.layerAlert(Page, string.Format("请输入{0}出题顺序！", o.QuestionType));
                        tran.Rollback();
                        return;
                    }
                    o.ShowOrder = Convert.ToInt32(_RadNumericTextBoxShowOrder.Value);

                    //每题分数
                    _RadNumericTextBoxScore = i.Cells[RadGridQuestionType.MasterTableView.Columns.FindByUniqueName("Score").OrderIndex].Controls[1] as RadNumericTextBox;
                    if (_RadNumericTextBoxScore.Value.HasValue == false)
                    {
                        UIHelp.layerAlert(Page, string.Format("请输入{0}每题分数！", o.QuestionType));
                        tran.Rollback();
                        return;
                    }
                    o.Score = Convert.ToInt32(_RadNumericTextBoxScore.Value);

                    //答题说明
                    _RadTextBoxRemark = i.Cells[RadGridQuestionType.MasterTableView.Columns.FindByUniqueName("Remark").OrderIndex].Controls[1] as RadTextBox;
                    o.Remark = _RadTextBoxRemark.Text.Trim();
                    ExamPageQuestionTypeDAL.Insert(tran, o);
                }

                tran.Commit();
                ViewState["ExamPageOB"] = ob;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "创建试卷失败！", ex);
                return;
            }

            UIHelp.WriteOperateLog(PersonName, UserID, "编辑试卷", string.Format("试卷名称：{0}。"
           , ob.ExamPageTitle));

            UIHelp.layerAlert(Page, "保存成功！",6,3000);
            RadTabStrip1.Visible = true;
            //试题出题难度统计
            BindRadGridQuestionCount(ob.ExamPageID.Value);

            //已出试题
            BindRadGridQuestion(ob.ExamPageID.Value);

            //刷新父窗体
            ScriptManager.RegisterStartupScript(this, this.GetType(), "isfresh", "var isfresh = true;", true);

        }

        //变换岗位工种，匹配科目
        protected void PostSelect_OnPostSelectChange(object source, EventArgs e)
        {
           
            try
            {
                DataTable dt = CommonDAL.GetDataTable(0, int.MaxValue - 1, "dbo.View_PostInfo", "SUBJECTID,SUBJECTNAME", string.Format(" and PostID={0}", PostSelect.PostID == "" ? "-1" : PostSelect.PostID), "SUBJECTID");
                RadioButtonListKeMuID.DataSource = dt;
                RadioButtonListKeMuID.DataBind();

                if (string.IsNullOrEmpty(PostSelect.PostID) == false)
                {
                    RadTextBoxExamPageTitle.Text = string.Format("{0}_{1}_科目_试卷A", DateTime.Now.AddMonths(1).ToString("yyyy年MM月"), PostSelect.PostName);
                }
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "获取科目信息失败", ex);
                return;
            }
        }

        protected void RadTabStrip1_TabClick(object sender, RadTabStripEventArgs e)
        {
            if (e.Tab.Value == "试卷定义")
            {
                divExamPage.Visible = true;
                divQuestion.Visible = false;
            }
            else//随机组卷
            {
                divExamPage.Visible = false;
                divQuestion.Visible = true;
            }
        }

        /// <summary>
        /// 绑定组卷难度设置
        /// </summary>
        /// <param name="ExamPageID"></param>
        private void BindRadGridQuestionCount(long ExamPageID)
        {
            DataTable dt = ExamPageQuestionTypeDAL.GetCountByDifficulty(ExamPageID);
            //RaestionCount.DataSource = dt;
            //RadGridQuestionCount.DataBind();dGridQu

            ViewState["ExamPageSet"] = dt;

            string sql=@"select d.title,d.tagcode,d.Weight,q.questionCount,0 as SendWeight
                            from 
                            (
                                select count(*) questionCount,tagcode
                                FROM  dbo.question q
                                where SUBJECTID=(select SUBJECTID from dbo.exampage where exampageid={0})
                                group  by tagcode
                            ) q
                            inner join 
                            (
                                select * from 
                                dbo.ExamRangeSub 
                                where ExamYear=(select examyear from dbo.exampage where exampageid={0}) 
                                and SUBJECTID=(select SUBJECTID from dbo.exampage where exampageid={0})
                            ) d
                            on  q.tagcode = d.tagcode
                            order by q.questionCount,d.weight desc";
            DBHelper db = new DBHelper();
            DataTable dtQuestionCountByTag = db.GetFillData(string.Format(sql, ExamPageID));
            ViewState["ExamInfoTag"] = dtQuestionCountByTag;

            //RadGridInfoTag.DataSource = dtQuestionCountByTag;
            //RadGridInfoTag.DataBind();
        }

        /// <summary>
        /// 绑定组卷试题
        /// </summary>
        /// <param name="ExamPageID"></param>
        private void BindRadGridQuestion(long ExamPageID)
        {
            DataTable dt = PageQuestionDAL.GetList(0,int.MaxValue -1,string.Format(" and ExamPageID={0}",ExamPageID.ToString()),"QuestionNo");
            RadGridQuestion.DataSource = dt;
            RadGridQuestion.DataBind();

            if (RadGridQuestion.MasterTableView.Items.Count > 0)
            {
                ButtonCreateQuestion.Text = "重新出题";
                ButtonCreateQuestion.OnClientClick = "return confirm('重新出题将清除已存在的试题，你确定要重新出题吗?');";
            }
            else
            {
                ButtonCreateQuestion.Text = "随机出题";
            }
        }

        /// <summary>
        /// 获取题库有效试题
        /// </summary>
        /// <param name="SubjectID">科目ID</param>
        /// <param name="QuestionType">试题类型：</param>
        /// <param name="Difficulty">难度：1,2,3</param>
        /// <returns></returns>
        private int CountQuestion(int SubjectID, string QuestionType,int Difficulty)
        {
            return QuestionDAL.SelectCount(string.Format(" and Flag=1 and SubjectID={0} and QUESTIONTYPE='{1}' and DIFFICULTY={2}",SubjectID.ToString(),QuestionType,Difficulty.ToString()));
        }

        //随机出题
        protected void ButtonCreateQuestion_Click(object sender, EventArgs e)
        {
            ExamPageOB _ExamPageOB = ViewState["ExamPageOB"] as ExamPageOB;
            System.Text.StringBuilder rtnErr = new System.Text.StringBuilder();//表单数据错误信息
            DataRow[] GetRows = null;//临时行            
            Random myRandom = new Random();
            int rowNo;//随机行号
            DataRow findPage;//试卷设置定位
            DataRow findTag;//大纲定位
            decimal difficulty=1.0M;//难度
            int QuestionCount = 0;//试题数量
            int TryCount = 0;//尝试次数
            int TagSendWeight=0;//大纲已分配权重

            //试卷题型设置（各类试题数量及单题分值）
            DataTable PageSet = (DataTable)ViewState["ExamPageSet"];
            DataColumn[] pk = new DataColumn[1]; 
            pk[0] = PageSet.Columns["QuestionType"];
            PageSet.PrimaryKey = pk;

            //大纲权重
            DataTable dtTag =(DataTable)ViewState["ExamInfoTag"];
            DataColumn[] pkTag = new DataColumn[1];
            pkTag[0] = dtTag.Columns["TagCode"];
            dtTag.PrimaryKey = pkTag;

            if (ButtonCreateQuestion.Text == "重新出题")
                {
                    foreach (DataRow r in PageSet.Rows)
                    {
                        r["SendCount"] = 0;
                    }

                    foreach (DataRow r in dtTag.Rows)
                    {
                        r["SendWeight"] = 0;
                    }
                }

            //试题库
            DataTable dtQuestion = CommonDAL.GetDataTable(0, int.MaxValue - 1, "dbo.Question", "QUESTIONID,TAGCODE,QUESTIONTYPE,DIFFICULTY,TITLE,ANSWER", string.Format(" and SubjectID={0} and Flag=1", _ExamPageOB.SubjectID.ToString()), "TAGCODE");

            if (dtQuestion.Rows.Count == 0)
            {
                UIHelp.layerAlert(Page,"题库中有效试题数量为0，无法出题！");
                return;
            }

            //已分配试题
            DataTable dtPageQuestion = dtQuestion.Clone();
            DataColumn[] pkQuestion = new DataColumn[1];
            pkQuestion[0] = dtPageQuestion.Columns["QUESTIONID"];
            dtPageQuestion.PrimaryKey = pkQuestion;

            //按大纲权重出题，先出题数少，权重大的的大纲试题
            foreach (DataRow r in dtTag.Rows)
            {   

                //复制本条大纲试题数据到临时表
                DataTable dtCur = dtQuestion.Clone();//某条大纲试题库
                GetRows = dtQuestion.Select(string.Format("TAGCODE={0}",r["TAGCODE"].ToString()));
                GetRows.CopyToDataTable<System.Data.DataRow>(dtCur, LoadOption.OverwriteChanges);

                TryCount = 0;

                while (dtCur.Rows.Count > 0)
                {
                    TryCount++;
                    if (TryCount > (GetRows.Length * 5)) break;

                    //随机取一行号
                    rowNo = myRandom.Next(0, dtCur.Rows.Count - 1);

                    //定位试卷题型设置：试题类型所在行
                    findPage = PageSet.Rows.Find(dtCur.Rows[rowNo]["QuestionType"]);

                    //定位大纲权重：大纲代码所在行
                    findTag =  dtTag.Rows.Find(r["TAGCODE"].ToString());

                    if (findPage == null )
                    {
                        UIHelp.layerAlert(Page, string.Format("出错，试题中存在未知的试题类型“{0}”，请检查题库", dtCur.Rows[rowNo]["QuestionType"].ToString()));
                        return;
                    }
                    if ( findTag ==null)
                    {
                        UIHelp.layerAlert(Page,string.Format("出错，试题中存在未知的大纲编号“{0}”，请检查题库",r["TAGCODE"].ToString()));
                        return;
                    }                    
                   
                    //大纲权重已达标
                    if(Convert.ToInt32(findTag["SendWeight"]) == Convert.ToInt32(findTag["Weight"]) )
                    {
                        break;
                    }

                     //试题类型试题数量已达标
                    if(Convert.ToInt32(findPage["SendCount"]) == Convert.ToInt32(findPage["QuestionCount"]))
                    {
                        continue;
                    }

                    //试卷满分可能大于100分，所以权重分数<>试题分数
                    TagSendWeight = Convert.ToInt32(findTag["SendWeight"]) + (Convert.ToInt32(findPage["Score"]) * _ExamPageOB.Score.Value / 100);

                    if (TagSendWeight <= Convert.ToInt32(findTag["Weight"]))
                    {
                        //分配试题
                        dtPageQuestion.Rows.Add(dtCur.Rows[rowNo].ItemArray);

                        //更新已分配试题类型数量
                        findPage["SendCount"] = Convert.ToInt32(findPage["SendCount"]) + 1;

                        //更新已分配大纲权重
                        findTag["SendWeight"] = TagSendWeight;

                        //难度累加
                        difficulty += Convert.ToInt32(dtCur.Rows[rowNo]["DIFFICULTY"]);

                        //从待分配题库中剔除
                        dtCur.Rows.RemoveAt(rowNo);

                        QuestionCount +=1;//试题数量
                    }                    
                }
            }

            //检查试题类型数量是否都已满足,不满足随机补充试题
            foreach (DataRow r in PageSet.Rows)
            {
                if (Convert.ToInt32(r["SendCount"]) == Convert.ToInt32(r["QuestionCount"]))
                {
                    continue;
                }
                DataTable dtCur = dtQuestion.Clone();//某条大纲试题库
                GetRows = dtQuestion.Select(string.Format("QUESTIONTYPE='{0}'", r["QuestionType"]));
                GetRows.CopyToDataTable<System.Data.DataRow>(dtCur, LoadOption.OverwriteChanges);

                while (dtCur.Rows.Count>0 && Convert.ToInt32(r["SendCount"]) < Convert.ToInt32(r["QuestionCount"]))
                {
                    //随机取一行号
                    rowNo = myRandom.Next(0, dtCur.Rows.Count - 1);

                    if (dtPageQuestion.Rows.Find(dtCur.Rows[rowNo]["QUESTIONID"]) == null)
                    {
                        //分配试题
                        dtPageQuestion.Rows.Add(dtCur.Rows[rowNo].ItemArray);

                        //更新已分配试题类型数量
                        r["SendCount"] = Convert.ToInt32(r["SendCount"]) + 1;

                        //难度累加
                        difficulty += Convert.ToInt32(dtCur.Rows[rowNo]["DIFFICULTY"]);

                        //从待分配题库中剔除
                        dtCur.Rows.RemoveAt(rowNo);

                        QuestionCount += 1;//试题数量
                    }
                    else
                    {
                        dtCur.Rows.RemoveAt(rowNo);
                    }
                }
            }

            //保存试题
            System.Text.StringBuilder sql = new System.Text.StringBuilder();
            int QuestionN0 = 1;//试题编号
            foreach (DataRow rType in PageSet.Rows)
            {
                GetRows = dtPageQuestion.Select(string.Format("QUESTIONTYPE='{0}'", rType["QuestionType"]));
                foreach (DataRow r in GetRows)
                {
                    sql.Append(string.Format(@" Union all select {0},{1},'{2}','{3}','{4}',{5},{6},{7} "
                     , _ExamPageOB.ExamPageID,r["QUESTIONID"] , r["QUESTIONTYPE"], r["TITLE"], r["ANSWER"] , QuestionN0, r["TAGCODE"], r["DIFFICULTY"]
                       ));
                    QuestionN0++;
                }
            }
            if (sql.Length > 0) { sql.Remove(0, 10); }//去除第一个union
            sql.Insert(0, @"INSERT INTO DBO.PAGEQUESTION(EXAMPAGEID,QUESTIONID,QUESTIONTYPE,TITLE,ANSWER,QUESTIONNO,TAGCODE,DIFFICULTY) ");

            try
            {
                if (ButtonCreateQuestion.Text == "重新出题")
                {
                    //清除已存在试题
                    PageQuestionDAL.Delete(null, _ExamPageOB.ExamPageID.Value);
                }

                //Utility.FileLog.WriteLog(sql.ToString(), null);

                //出题
                DBHelper db = new DBHelper();
                db.ExcuteNonQuery(sql.ToString());

               

                //计算更新试卷难度
                ExamPageDAL.UpdateDifficulty(_ExamPageOB.ExamPageID.Value);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "组卷失败！", ex);
                return;
            }

            UIHelp.WriteOperateLog(PersonName, UserID, "试卷组卷", string.Format("试卷名称：{0}。"
           , _ExamPageOB.ExamPageTitle));
            
            UIHelp.layerAlert(Page, string.Format("{0}成功！", ButtonCreateQuestion.Text));
            BindRadGridQuestion(_ExamPageOB.ExamPageID.Value);

            //刷新父窗体
            ScriptManager.RegisterStartupScript(this, this.GetType(), "isfresh", "var isfresh = true;", true);



            ////检查大纲权重是否都已满足
            //foreach (DataRow r in dtTag.Rows)
            //{
            //    if (r["Weight"].ToString() != r["SendWeight"].ToString())
            //    {
            //        rtnErr.Append(string.Format("大纲“{0}”要求权重{1}，题库只能分配权重{2}，请检查题库试题数量是否满足！<br />", r["title"], r["Weight"], r["SendWeight"]));
            //    }
            //}
            
            ////检查试题类型数量是否都已满足
            //foreach (DataRow r in PageSet.Rows)
            //{
            //    if (r["QuestionCount"].ToString() != r["SendCount"].ToString())
            //    {
            //        rtnErr.Append(string.Format("“{0}”已分配试题数量{1}，小于要求{2}，请检查题库试题数量是否满足！<br />", r["QuestionType"], r["SendCount"], r["QuestionCount"]));
            //    }
            //}
            //if (rtnErr.Length > 0)
            //{
            //    UIHelp.layerAlert(Page, string.Format("试题库无法满足出题要求！<br />{0}", rtnErr.ToString()));
            //    return;
            //}
     
        }

      

//        //随机出题
//        protected void ButtonCreateQuestion_Click(object sender, EventArgs e)
//        {
//            ExamPageOB _ExamPageOB = ViewState["ExamPageOB"] as ExamPageOB;
//            System.Text.StringBuilder rtnErr = new System.Text.StringBuilder();//表单数据错误信息
//            RadNumericTextBox _RadNumericTextBoxD1Count = null;
//            RadNumericTextBox _RadNumericTextBoxD2Count = null;
//            RadNumericTextBox _RadNumericTextBoxD3Count = null;
//            string QuestionType = "";//试题类型
//            int QuestionCount = 0;//试题数量
//            int QuestionLibaryCcount = 0;//题库有效试题数量
//            List<string> parms = new List<string>();//出题参数

//            //校验难度设置
//            #region 校验难度设置

//            foreach (GridItem i in RadGridQuestionCount.MasterTableView.Items)
//            {
//                QuestionType = RadGridQuestionCount.MasterTableView.DataKeyValues[i.ItemIndex]["QuestionType"].ToString();//试题类型
//                QuestionCount = Convert.ToInt32(RadGridQuestionCount.MasterTableView.DataKeyValues[i.ItemIndex]["QuestionCount"]);//试题数量

//                //简单题数量
//                _RadNumericTextBoxD1Count = i.Cells[RadGridQuestionCount.MasterTableView.Columns.FindByUniqueName("D1Count").OrderIndex].Controls[1] as RadNumericTextBox;
//                QuestionLibaryCcount = CountQuestion(_ExamPageOB.SubjectID.Value, QuestionType, 1);
//                if (QuestionLibaryCcount < _RadNumericTextBoxD1Count.Value)
//                {
//                    rtnErr.Append(string.Format("<br />题库中“{0}”简单题试题数量【{1}】低于需要数量【{2}】，无法出题。", QuestionType, QuestionLibaryCcount.ToString(), _RadNumericTextBoxD1Count.Value.ToString()));
//                }

//                //中等题数量
//                _RadNumericTextBoxD2Count = i.Cells[RadGridQuestionCount.MasterTableView.Columns.FindByUniqueName("D2Count").OrderIndex].Controls[1] as RadNumericTextBox;
//                QuestionLibaryCcount = CountQuestion(_ExamPageOB.SubjectID.Value, QuestionType, 2);
//                if (QuestionLibaryCcount < _RadNumericTextBoxD2Count.Value)
//                {
//                    rtnErr.Append(string.Format("<br />题库中“{0}”中等题试题数量【{1}】低于需要数量【{2}】，无法出题。", QuestionType, QuestionLibaryCcount.ToString(), _RadNumericTextBoxD2Count.Value.ToString()));
//                }

//                //难题数量
//                _RadNumericTextBoxD3Count = i.Cells[RadGridQuestionCount.MasterTableView.Columns.FindByUniqueName("D3Count").OrderIndex].Controls[1] as RadNumericTextBox;
//                QuestionLibaryCcount = CountQuestion(_ExamPageOB.SubjectID.Value, QuestionType, 3);
//                if (QuestionLibaryCcount < _RadNumericTextBoxD3Count.Value)
//                {
//                    rtnErr.Append(string.Format("<br />题库中“{0}”难题试题数量【{1}】低于需要数量【{2}】，无法出题。", QuestionType, QuestionLibaryCcount.ToString(), _RadNumericTextBoxD3Count.Value.ToString()));
//                }

//                if (Convert.ToInt32(_RadNumericTextBoxD1Count.Value + _RadNumericTextBoxD2Count.Value + _RadNumericTextBoxD3Count.Value) != QuestionCount)
//                {
//                    rtnErr.Append(string.Format("<br />“{0}”试题数量与各难度试题数量不相等，请修改出题参数。", QuestionType));
//                }

//                parms.Add(string.Format("{0},{1},{2},{3}", QuestionType, _RadNumericTextBoxD1Count.Value.ToString(), _RadNumericTextBoxD2Count.Value.ToString(), _RadNumericTextBoxD3Count.Value.ToString()));
//            }
//            if (rtnErr.Length > 0)
//            {
//                UIHelp.layerAlert(Page, rtnErr.ToString());
//                return;
//            }

//            #endregion

//            //保存出题结果
//            string[] p = null;
//            System.Text.StringBuilder sql = new System.Text.StringBuilder();

//            foreach (string s in parms)
//            {
//                p = s.Split(',');
//                for (int i = 1; i <= 3; i++)
//                {
//                    //INSERT INTO "DBO"."PAGEQUESTION"("EXAMPAGEID","QUESTIONID","QUESTIONTYPE","TITLE","ANSWER","QUESTIONNO","TAGCODE","DIFFICULTY" )
//                    //SELECT EXAMPAGEID,"QUESTIONID","QUESTIONTYPE","TITLE","ANSWER",QUESTIONNO,"TAGCODE","DIFFICULTY"
//                    //FROM "DBO"."QUESTION"
//                    //where Flag=1 and SubjectID=? and QUESTIONTYPE=? and DIFFICULTY=?
//                    //Limit ?
//                    sql.Append(string.Format(@" Union all 
//                (SELECT {0} as ExamPageID,QUESTIONID,QUESTIONTYPE,TITLE,ANSWER,round(RANDOM(),6) as sn,TAGCODE,DIFFICULTY
//                FROM DBO.QUESTION
//                where Flag=1 and SubjectID={1} and QUESTIONTYPE='{2}' and DIFFICULTY={3}
//                order by sn  Limit {4}) ", _ExamPageOB.ExamPageID.ToString()
//                        //,QUESTIONNO
//                            , _ExamPageOB.SubjectID.ToString()
//                            , p[0]//QUESTIONTYPE
//                            , i.ToString()//DIFFICULTY
//                            , p[i]//limit n
//                            ));
//                }
//            }
//            if (sql.Length > 0) { sql.Remove(0, 10); }//去除第一个union
//            sql.Insert(0, @"INSERT INTO DBO.PAGEQUESTION(EXAMPAGEID,QUESTIONID,QUESTIONTYPE,TITLE,ANSWER,QUESTIONNO,TAGCODE,DIFFICULTY)
//select ExamPageID,QUESTIONID,QUESTIONTYPE,TITLE,ANSWER,rownum,TAGCODE,DIFFICULTY from(").Append(")");

//            try
//            {
//                if (ButtonCreateQuestion.Text == "重新出题")
//                {
//                    //清除已存在试题
//                    PageQuestionDAL.Delete(null, _ExamPageOB.ExamPageID.Value);
//                }

//                //出题
//                DBHelper db = new DBHelper();
//                db.ExcuteNonQuery(sql.ToString());

//                //计算更新试卷难度
//                ExamPageDAL.UpdateDifficulty(_ExamPageOB.ExamPageID.Value);
//            }
//            catch (Exception ex)
//            {
//                UIHelp.WriteErrorLog(Page, "组卷失败！", ex);
//                return;
//            }

//            UIHelp.WriteOperateLog(PersonName, UserID, "试卷组卷", string.Format("试卷名称：{0}。"
//           , _ExamPageOB.ExamPageTitle));

//            BindRadGridQuestion(_ExamPageOB.ExamPageID.Value);
//            UIHelp.layerAlert(Page, string.Format("{0}成功！", ButtonCreateQuestion.Text));

//            //刷新父窗体
//            ScriptManager.RegisterStartupScript(this, this.GetType(), "isfresh", "var isfresh = true;", true);
//        }
    }
}
