using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using Model;
using DataAccess;
using Telerik.Web.UI;
using System.Data;
using System.IO;
//using System.Threading;
using System.Text.RegularExpressions;

namespace ZYRYJG.QuestionMgr
{
    public partial class MainQuestionManage : BasePage
    {
        /// <summary>
        /// 试题类型
        /// </summary>
        private const string _QuestionType = "判断题,单选题,多选题,简答题";

        /// <summary>
        /// 试题难度
        /// </summary>
        private const string _Difficulty = "难,中,易";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //string filePath = Request.Url.AbsoluteUri.Replace("/QuestionMgr/MainQuestionManage.aspx", "/UpLoad/ExamPage/《建筑施工专业基础与实务(初级)》习题---.htm");
                //string strWebContent = Utility.Check.CleanHtml(UIHelp.GetWebContent(filePath, System.Text.Encoding.GetEncoding("GB2312")));

                //strWebContent = Utility.Check.CleanHtml(strWebContent);
                //    strWebContent = strWebContent.Replace("src=\"", "src=\"" + Request.Url.AbsoluteUri.Replace("/QuestionMgr/MainQuestionManage.aspx", "/UpLoad/ExamPage/"));

                //    int iStart = strWebContent.IndexOf("<body", 0);
                //    int iTableStart = strWebContent.IndexOf(">", iStart) + 1;
                //    int iTableEnd = strWebContent.IndexOf("</body>", iTableStart);
                //    //RadEditor1.Content = strWebContent.Substring(iTableStart, iTableEnd - iTableStart );
                //    DivHtmlSource.InnerHtml = strWebContent.Substring(iTableStart, iTableEnd - iTableStart);
                //    //RadEditor1.
                //}
                //RadEditor1.Content = strWebContent;

                //GetHtml(Request.Url.AbsoluteUri.Replace("/QuestionMgr/MainQuestionManage.aspx", "/UpLoad/ExamPage/《建筑施工专业基础与实务(初级)》习题---.html"));


                //FileStream fileStream = new FileStream(Server.MapPath("~/upload/廖亮简历.rtf"), FileMode.Open, FileAccess.Read);
                //fileStream.Seek(0, SeekOrigin.Begin);
                //RadEditor1.LoadRtfContent(fileStream);

                //GetHtml(Server.MapPath("~/UpLoad/ExamPage/《建筑施工专业基础与实务(初级)》习题---.html"));

                //HyperLink1.NavigateUrl = Request.Url.AbsoluteUri.Replace("/QuestionMgr/MainQuestionManage.aspx", "/UpLoad/ExamPage/《建筑施工专业基础与实务(初级)》习题---.html");
                //GetHtml("../UpLoad/ExamPage/《建筑施工专业基础与实务(初级)》习题---.html");



                //显示科目列表（隐藏考试科目参考其它科目数据，即 SUBJECTID <> CodeForma）
                DataTable dtPost = null;
                if (Cache["Exam_KeMu"] != null)
                {
                    dtPost = Cache["Exam_KeMu"] as DataTable;
                }
                else
                {
                    dtPost = CommonDAL.GetDataTable(0, int.MaxValue - 1, "dbo.View_PostInfo", "PostTypeName,PostName,SubjectName,SubjectID,PostTypeID,PostID", " and SUBJECTID = CodeFormat", "PostTypeID,PostID,SubjectID");

                    //在最后一次被访问后8小时自动过期
                    Utility.CacheHelp.AddSlidingExpirationCache(Page, "Exam_KeMu", dtPost, 8);
                }

                RadGridPost.DataSource = dtPost;
                RadGridPost.DataBind();
                RadGridPost.MasterTableView.Items[0].Selected = true;
                //RadGridInfoTag.DataBind();

                RadGridPost_SelectedIndexChanged(sender, e);
            }
        }

        //试题Grid绑定内容
        protected void RadGridQuestion_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            //   int rowCount = CommonDAL.SelectRowCount("dbo.Question",  string.Format(" and SubjectID={0}"
            //          , RadGridPost.MasterTableView.DataKeyValues[RadGridPost.SelectedItems[0].ItemIndex]["SubjectID"].ToString()));

            //DataTable dt = CommonDAL.GetDataTable(RadGridQuestion.CurrentPageIndex, RadGridQuestion.PageSize, "dbo.Question", "*", string.Format(" and SubjectID={0}"
            //          , RadGridPost.MasterTableView.DataKeyValues[RadGridPost.SelectedItems[0].ItemIndex]["SubjectID"].ToString()), "TagCode,QuestionID");
            //RadGridQuestion.DataSource = dt;

            //int pageCount =  rowCount / RadGridQuestion.PageSize;
            //RadGridQuestion.Page = (rowCount % RadGridQuestion.PageSize == 0) ? pageCount : pageCount + 1;


            ObjectDataSource1.SelectParameters.Clear();
            QueryParamOB q = new QueryParamOB();

            q.Add(string.Format(@"
                SubjectID={0} 
                and  title like '%{1}%'
                ", RadGridPost.MasterTableView.DataKeyValues[RadGridPost.SelectedItems[0].ItemIndex]["SubjectID"], RadTextBoxKey.Text.Trim()));

            if (RadioButtonListStatus.SelectedValue != "")
            {
                q.Add(string.Format("Flag={0}", RadioButtonListStatus.SelectedValue));
            }

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridQuestion.CurrentPageIndex = 0;
            RadGridQuestion.DataSourceID = ObjectDataSource1.ID;
        }

        //变换科目选择后
        protected void RadGridPost_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadGridQuestion_NeedDataSource(sender, null);
            //RadGridQuestion.Rebind();
            //RadGridInfoTag.ExportSettings.FileName = Server.UrlEncode(string.Format("{0}-{1}-{2}-知识大纲", RadGridPost.SelectedItems[0].Cells[RadGridPost.MasterTableView.Columns.FindByUniqueName("PostTypeName").OrderIndex].Text
            //    , RadGridPost.SelectedItems[0].Cells[RadGridPost.MasterTableView.Columns.FindByUniqueName("PostName").OrderIndex].Text
            //    , RadGridPost.SelectedItems[0].Cells[RadGridPost.MasterTableView.Columns.FindByUniqueName("SubjectName").OrderIndex].Text));
            LabelSelectPost.Text = string.Format("当前科目：【{0} - {1} - {2}】"
                , RadGridPost.SelectedItems[0].Cells[RadGridPost.MasterTableView.Columns.FindByUniqueName("PostTypeName").OrderIndex].Text
                , RadGridPost.SelectedItems[0].Cells[RadGridPost.MasterTableView.Columns.FindByUniqueName("PostName").OrderIndex].Text
                , RadGridPost.SelectedItems[0].Cells[RadGridPost.MasterTableView.Columns.FindByUniqueName("SubjectName").OrderIndex].Text);
        }

        //保存试题
        private void SaveImportQuestion(string html)
        {
            //string[] str_question =  Regex.Split(html,"【试题类型[^【]*】");
            string[] str_question = Regex.Split(html, "【试题类型】");
            //string[] str_item = null;
            MatchCollection str_item = null;
            string item = "";

            DateTime createTime = DateTime.Now;
            int SubjectID = Convert.ToInt32(RadGridPost.MasterTableView.DataKeyValues[RadGridPost.SelectedItems[0].ItemIndex]["SubjectID"].ToString());//科目
            List<QuestionOB> list = new List<QuestionOB>();
            int TagCode = 0;//大纲编号

            try
            {
                foreach (string s in str_question)
                {
                    if (s.IndexOf("【题目】") == -1)
                    {
                        continue;
                    }
                    //str_item = Regex.Split("【试题类型】" + s, "【[^【]*】");
                    str_item = Regex.Matches("【试题类型】" + s, "【[^【]*】*");

                    QuestionOB ob = new QuestionOB();
                    //科目ID
                    ob.SubjectID = SubjectID;

                    foreach (Match it in str_item)
                    {
                        switch (Regex.Replace(it.Value, @"【([^【]*)】((.*\r\n)*)", "$1").Trim().Replace("&nbsp;", "").Replace(" ", "").Replace("　", ""))
                        {
                            case "试题类型":
                                item = Regex.Replace(it.Value, @"【([^【]*)】((.*\r\n)*)", "$2").Replace("\r\n", "").Replace("&nbsp;", "").Replace(" ", "").Replace("　", "");
                                //试题类型
                                ob.QuestionType = item;
                                break;
                            case "难度":
                                item = Regex.Replace(it.Value, @"【([^【]*)】((.*\r\n)*)", "$2").Replace("\r\n", "").Replace("&nbsp;", "").Replace(" ", "").Replace("　", "");
                                //难度
                                ob.Difficulty = ConvertQuestionDifficulty(item);
                                break;
                            case "大纲编号":
                                item = Regex.Replace(it.Value, @"【([^【]*)】((.*\r\n)*)", "$2").Replace("\r\n", "").Replace("&nbsp;", "").Replace(" ", "").Replace("　", "");
                                TagCode = FormatTagCode(item);
                                //大纲编码
                                ob.TagCode = TagCode;
                                //显示编码
                                ob.ShowCode = item;
                                break;
                            case "题目":
                                item = Regex.Replace(it.Value, @"【([^【]*)】((.*\r\n)*)", "$2").Trim();

                                //问题
                                ob.Title = item;
                                break;
                            case "答案":
                                item = Regex.Replace(it.Value, @"【([^【]*)】((.*\r\n)*)", "$2").Trim();


                                //答案
                                if ("判断题,单选题,多选题".IndexOf(ob.QuestionType) >= 0)
                                {
                                    ob.Answer = item.Replace("\r\n", "").Replace("&nbsp;", "").Replace(" ", "").Replace("　", "").ToUpper();
                                }
                                else
                                {
                                    ob.Answer = item;
                                }
                                break;
                        }

                    }

                    //状态
                    ob.Flag = 0;//未发布

                    //最后修改人ID
                    ob.ModifyPersonID = PersonID;
                    ob.ModifyTime = DateTime.Now;

                    //创建人ID
                    ob.CreatePersonID = PersonID;
                    //创建时间
                    ob.CreateTime = DateTime.Now;

                    if (string.IsNullOrEmpty(ob.Title) == true)
                    {
                        throw new Exception(string.Format("第{0}题导入时出错，请检查是否录入标题或者标题标签前没有回车换行！", list.Count + 1));
                    }
                    if (ob.Difficulty.HasValue == false)
                    {
                        throw new Exception(string.Format("第{0}题导入时出错，请检查是否录入难度或者难度标签前没有回车换行！", list.Count + 1));
                    }
                    if (ob.TagCode.HasValue == false)
                    {
                        throw new Exception(string.Format("第{0}题导入时出错，请检查是否录入大纲编号或者大纲编号标签前没有回车换行！", list.Count + 1));
                    }
                    if (string.IsNullOrEmpty(ob.Answer) == true)
                    {
                        throw new Exception(string.Format("第{0}题导入时出错，请检查是否录入答案或者答案标签前没有回车换行！", list.Count + 1));
                    }                   

                    
                    list.Add(ob);
                }
            }
            catch (Exception ex)
            {
                Utility.FileLog.WriteLog("批量导入试题失败！", ex);
                UIHelp.layerAlert(Page, string.Format("试题导入失败：{0}", ex.Message));
                return;
            }

            if (list.Count == 0) return;

            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();

            try
            {
                //导入试题
                foreach (QuestionOB o in list)
                {
                    QuestionDAL.Insert(tran, o);
                }
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                Utility.FileLog.WriteLog("批量导入试题失败！", ex);
                UIHelp.layerAlert(Page, string.Format("试题导入失败：{0}", ex.Message));
                return;
            }
            UIHelp.WriteOperateLog(PersonName, UserID, "批量导入试题", string.Format("批量导入试题：科目：{0}，试题数量{1}条。",
               RadGridPost.SelectedItems[0].Cells[RadGridPost.MasterTableView.Columns.FindByUniqueName("SubjectName").OrderIndex].Text, list.Count.ToString()));

            UIHelp.layerAlert(Page, "导入试题成功！",6,3000);
            RadGridQuestion.Rebind();
            //int start=0;
            //int find = html.LastIndexOf("【试题类型",start);
            //while (find > 0)
            //{
            //    Regex.Split(html,)(html, @"\r\n\r\n", "\r\n", RegexOptions.IgnoreCase);
            //}
            //System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            //doc.InnerXml =string.Format("<xml>{0}</xml>", html);
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();


            //foreach (System.Xml.XmlNode n in doc.ChildNodes)
            //{
            //    switch (n.Name)
            //    {
            //        case "p":
            //            UIHelp.layerAlert(Page, n.InnerXml);
            //            break;
            //        case "w:p":
            //            sb.Append("<br />");
            //            break;
            //    }
            //}
            //div_tip.InnerHtml = sb.ToString();
        }

        //导入大纲
        protected void ButtonImport_Click(object sender, EventArgs e)
        {
            CheckSaveDirectory();

            if (RadUploadFile.UploadedFiles.Count > 0)
            {
                //上传excel
                string targetFolder = Server.MapPath("~/UpLoad/ExamPage/");
                //string filePath = Path.Combine(targetFolder, RadUploadFile.UploadedFiles[0].GetName());
                string fileID = Guid.NewGuid().ToString();

                string filePath = Path.Combine(targetFolder, string.Format("{0}.doc", fileID));

                RadUploadFile.UploadedFiles[0].SaveAs(filePath, true);

                string html = "";
                try
                {
                    Utility.WordDelHelp.WordToHtmlFile(filePath);

                    //string htmlPath = filePath.Replace(".doc", ".htm");

                    //string htmlPath = Request.Url.AbsoluteUri.Replace("/QuestionMgr/MainQuestionManage.aspx", string.Format("/UpLoad/ExamPage/{0}.htm", fileID));

                    string serverType = System.Configuration.ConfigurationManager.AppSettings["serverType"];
                    string htmlPath = "";
                    if(serverType=="ww")//公网
                        htmlPath = UIHelp.AddUrlReadParam(string.Format("http://172.16.78.134/UpLoad/ExamPage/{0}.htm", fileID));
                    else//专网
                        htmlPath = UIHelp.AddUrlReadParam(string.Format("http://172.24.17.175/UpLoad/ExamPage/{0}.htm", fileID));

                    //////本地测试专用
                    //string htmlPath = UIHelp.AddUrlReadParam(string.Format("http://localhost:7191/UpLoad/ExamPage/{0}.htm", fileID));

                    //Utility.FileLog.WriteLog(htmlPath, null);//*************

                    html = Utility.Check.CleanHtml(UIHelp.GetWebContent(htmlPath, System.Text.Encoding.GetEncoding("GB2312")));

                    //Utility.FileLog.WriteLog(html, null);//*************

                    html = Regex.Replace(html, @"(<IMG[^>]*src="")([^>]*>)", "$1../UpLoad/ExamPage/$2", RegexOptions.IgnoreCase);

                    //有待需要优化，以上正则表达式没有替换掉 <sub></sub><sup></sup><a ....></a>

                    //Utility.FileLog.WriteLog(html, null);//*************
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "批量导入试题失败！", ex);
                    return;
                }

                SaveImportQuestion(html);
            }
        }

        ////导入大纲(EXCEL模板)
        //protected void ButtonImport_Click(object sender, EventArgs e)
        //{
        //    if (RadUploadFile.UploadedFiles.Count > 0)
        //    {
        //        //上传excel
        //        string targetFolder = Server.MapPath("~/App_Data/RadUploadTemp/");
        //        string filePath = Path.Combine(targetFolder, string.Format("QuestionBat_{0}_{1}.xls", PersonID.ToString(), DateTime.Now.ToString("yyyyMMddHHmmss")));
        //        RadUploadFile.UploadedFiles[0].SaveAs(filePath, true);

        //        //读入DataSet再校验并保存
        //        DataSet dsImport = null;

        //        try
        //        {
        //            dsImport = Utility.ExcelDealHelp.ImportExcell(filePath, "");
        //        }
        //        catch (Exception ex)
        //        {
        //            UIHelp.WriteErrorLog(Page, "导入知识大纲数据失败", ex);
        //            return;
        //        }

        //        if (dsImport == null || dsImport.Tables.Count == 0 || dsImport.Tables[0].Rows.Count == 0)
        //        {
        //            UIHelp.layerAlert(Page, "未找到任何可导入的数据!");
        //            return;
        //        }
        //        else
        //        {
        //            if (VaildImportTemplate(dsImport.Tables[0]) == false)
        //            {
        //                UIHelp.layerAlert(Page, "导入文件格式无效，请下载标准的导入模板，正确填写大纲格式，进行导入!");
        //                return;
        //            }
        //            SaveImportData(dsImport.Tables[0]);//保存
        //        }
        //    }
        //    else
        //    {
        //        RadGridQuestion.Rebind();
        //    }


        //}

        ////验证导入模板有效性
        //protected bool VaildImportTemplate(DataTable dt)
        //{
        //    if (dt.Columns.Count < 5) return false;//模版列：试题类型,题目,答案,大纲编号,难度

        //    string[] colNames = new string[5] { "试题类型", "题目", "答案", "大纲编号", "难度" };
        //    for (int i = 0; i < 2; i++)
        //    {
        //        if (dt.Columns[i].ColumnName != colNames[i]) return false;
        //    }

        //    return true;
        //}

        ///// <summary>
        ///// 验证数据是否为空值
        ///// </summary>
        ///// <param name="dt">数据表</param>
        ///// <param name="rowIndex">行</param>
        ///// <param name="colName">列名</param>
        ///// <param name="rowErr">行校验结果</param>
        //private void ValidNull(DataTable dt, int rowIndex, string colName, System.Text.StringBuilder rowErr)
        //{
        //    if (dt.Rows[rowIndex][colName].ToString() == "") rowErr.Append("<br />“").Append(colName).Append("”不能为空值！");
        //}

        //根据现实序号格式化返回数据库编号
        private int FormatTagCode(string ShowCode)
        {
            string rtn = "";
            int code = 0;
            try
            {
                string[] temp = ShowCode.Split('.');
                foreach (string s in temp)
                {
                    if (int.TryParse(Utility.Check.removeInputErrorChares(s), out code) == true)
                    {
                        if (code > 99 || code < 1) return -2;//每层编号有效范围1~99
                        rtn += code.ToString("00");
                    }
                    else
                    {
                        return -1;//无法转化为数字
                    }
                }
                return Convert.ToInt32((rtn + "00000000").Substring(0, 8));
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 转化试题难度，用数字表示
        /// </summary>
        /// <param name="Difficulty">难度文字：难,中,易</param>
        /// <returns>难度数字：3,2,1</returns>
        private byte ConvertQuestionDifficulty(string Difficulty)
        {
            switch (Difficulty)
            {
                case "易":
                    return 1;
                case "中":
                    return 2;
                case "难":
                    return 3;
            }
            return 0;
        }

        ////保存导入的信息
        //protected void SaveImportData(DataTable dt)
        //{
        //    #region 格式校验

        //    System.Text.StringBuilder rtnErr = new System.Text.StringBuilder();//表单数据错误信息
        //    System.Text.StringBuilder rowErr = new System.Text.StringBuilder();//行数据错误信息

        //    // 删除空行
        //    for (int m = dt.Rows.Count - 1; m >= 0; m--)
        //    {
        //        if (dt.Rows[m]["试题类型"].ToString().Trim() == ""
        //            && dt.Rows[m]["题目"].ToString().Trim() == ""
        //            && dt.Rows[m]["答案"].ToString().Trim() == ""
        //            && dt.Rows[m]["大纲编号"].ToString().Trim() == ""
        //            && dt.Rows[m]["难度"].ToString().Trim() == "")
        //        {
        //            dt.Rows.RemoveAt(m);
        //        }
        //    }

        //    //List<string> listCode = new List<string>();//已出现过的序号
        //    //int weight = 0;//权重

        //    for (int m = 0; m < dt.Rows.Count; m++)
        //    {
        //        rowErr.Remove(0, rowErr.Length);//行错误清空
        //        ValidNull(dt, m, "试题类型", rowErr);
        //        if (_QuestionType.IndexOf(dt.Rows[m]["试题类型"].ToString().Trim()) < 0)
        //        {
        //            rowErr.Append("<br />试题类型只能输入（判断题,单选题,多选题,简答题）中的一项。");
        //        }

        //        ValidNull(dt, m, "题目", rowErr);
        //        ValidNull(dt, m, "答案", rowErr);
        //        if ("判断题,单选题,多选题".IndexOf(dt.Rows[m]["试题类型"].ToString().Trim()) >= 0)
        //        {
        //            if(Utility.Check.IsMatch("[A-Za-z]+",dt.Rows[m]["答案"].ToString().Trim()) ==false)
        //            {
        //                rowErr.Append("<br />判断、选择题答案格式不正确，请用字母表示（不带分隔符）。");
        //            }
        //        }
        //        else if (Utility.Check.Text_Length(dt.Rows[m]["答案"].ToString().Trim()) > 4000)
        //        {
        //            rowErr.Append("<br />答案不得超过4000个字符（中文算2个）。");
        //        }

        //        ValidNull(dt, m, "大纲编号", rowErr);
        //        if (FormatTagCode(dt.Rows[m]["大纲编号"].ToString().Trim()) <= 0)
        //        {
        //            rowErr.Append("<br />大纲编号格式不正确，请使用数字和小数点表示序号（例：1.2.1）。");
        //        }

        //        ValidNull(dt, m, "难度", rowErr);
        //        if (_Difficulty.IndexOf(dt.Rows[m]["难度"].ToString().Trim()) < 0)
        //        {
        //            rowErr.Append("<br />难度只能输入（难,中,易）中的一项。");
        //        }                

        //        if (rowErr.Length > 0)
        //        {
        //            rtnErr.Append("<br />---第【").Append(Convert.ToString(m + 1)).Append("】行：-------------------------------");
        //            rtnErr.Append(rowErr.ToString());
        //        }
        //    }

        //    if (rtnErr.Length > 0)
        //    {
        //        UIHelp.layerAlert(Page, rtnErr.ToString());
        //        return;
        //    }

        //    #endregion 格式校验

        //    DateTime createTime = DateTime.Now;
        //    int SubjectID = Convert.ToInt32(RadGridPost.MasterTableView.DataKeyValues[RadGridPost.SelectedItems[0].ItemIndex]["SubjectID"].ToString());//科目

        //    DBHelper db = new DBHelper();
        //    DbTransaction tran = db.BeginTransaction();
        //    QuestionOB ob = null;
        //    int TagCode = 0;//大纲编号
        //    try
        //    {
        //        //导入试题
        //        for (int m = 0; m < dt.Rows.Count; m++)
        //        {
        //            TagCode = FormatTagCode(dt.Rows[m]["大纲编号"].ToString().Trim());

        //            ob = new QuestionOB();
        //            //科目ID
        //            ob.SubjectID = SubjectID;
        //            //显示编码
        //            ob.ShowCode = dt.Rows[m]["大纲编号"].ToString().Trim();
        //            //大纲编码
        //            ob.TagCode = TagCode;
        //            //试题类型
        //            ob.QuestionType = dt.Rows[m]["试题类型"].ToString().Trim();

        //            //问题
        //            ob.Title = dt.Rows[m]["题目"].ToString().Trim();

        //            //答案
        //            if ("判断题,单选题,多选题".IndexOf(dt.Rows[m]["试题类型"].ToString().Trim()) >= 0)
        //            {
        //                ob.Answer = dt.Rows[m]["答案"].ToString().Trim().ToUpper();
        //            }
        //            else
        //            {
        //                ob.Answer = dt.Rows[m]["答案"].ToString().Trim();
        //            }
        //            //难度
        //            ob.Difficulty = ConvertQuestionDifficulty(dt.Rows[m]["难度"].ToString().Trim());              

        //            //状态
        //            ob.Flag = 0;//未发布

        //            //最后修改人ID
        //            ob.ModifyPersonID = PersonID;
        //            ob.ModifyTime = DateTime.Now;

        //            //创建人ID
        //            ob.CreatePersonID = PersonID;
        //            //创建时间
        //            ob.CreateTime = DateTime.Now;
        //            QuestionDAL.Insert(tran, ob);
        //        }
        //        tran.Commit();
        //    }
        //    catch (Exception ex)
        //    {
        //        tran.Rollback();
        //        UIHelp.WriteErrorLog(Page, "批量导入试题失败！", ex);
        //        return;
        //    }
        //    UIHelp.WriteOperateLog(PersonName, UserID, "批量导入试题", string.Format("批量导入试题：科目：{0}，试题数量{1}条。",
        //       RadGridPost.SelectedItems[0].Cells[RadGridPost.MasterTableView.Columns.FindByUniqueName("SubjectName").OrderIndex].Text, dt.Rows.Count.ToString()));

        //    UIHelp.layerAlert(Page, "导入试题成功！");
        //    RadGridQuestion.Rebind();
        //}

        //行格式化
        protected void RadGridQuestion_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem && e.Item.IsInEditMode == false)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                dataItem["Difficulty"].Text = FormatDifficulty(dataItem["Difficulty"].Text);

            }
        }

        //格式化显示“试题难度”
        private string FormatDifficulty(string value)
        {
            switch (value)
            {
                case "1":
                    return "易";
                case "2":
                    return "中";
                case "3":
                    return "难";
                default:
                    return "未知";
            }

        }

        protected void RadGridQuestion_ItemCommand(object source, GridCommandEventArgs e)
        {

            HiddenField HiddenFieldQuestionID = e.Item.FindControl("HiddenFieldQuestionID") as HiddenField;
            RadTextBox RadTextBoxShowCode = e.Item.FindControl("RadTextBoxShowCode") as RadTextBox;
            RadioButtonList RadioButtonListQuestionType = e.Item.FindControl("RadioButtonListQuestionType") as RadioButtonList;
            RadEditor RadEditorTitle = e.Item.FindControl("RadEditorTitle") as RadEditor;
            RadTextBox RadTextBoxAnswer = e.Item.FindControl("RadTextBoxAnswer") as RadTextBox;
            RadioButtonList RadioButtonListDifficulty = e.Item.FindControl("RadioButtonListDifficulty") as RadioButtonList;
            RadioButtonList RadioButtonListFlag = e.Item.FindControl("RadioButtonListFlag") as RadioButtonList;
            QuestionOB ob = null;
            switch (e.CommandName)
            {
                case RadGrid.InitInsertCommandName://新增初始化
                    break;
                case RadGrid.PerformInsertCommandName://新增
                    #region 新增试题
                    int SubjectID = Convert.ToInt32(RadGridPost.MasterTableView.DataKeyValues[RadGridPost.SelectedItems[0].ItemIndex]["SubjectID"].ToString());//科目
                    ob = new QuestionOB();
                    //科目ID
                    ob.SubjectID = SubjectID;
                    //显示编码
                    ob.ShowCode = RadTextBoxShowCode.Text.Trim();
                    //大纲编码
                    ob.TagCode = FormatTagCode(ob.ShowCode);
                    //试题类型
                    ob.QuestionType = RadioButtonListQuestionType.SelectedValue;

                    //问题
                    ob.Title = Utility.Check.CleanHtml(RadEditorTitle.Content.Replace("<br>", "\r\n").Replace("</p><p>", "\r\n").Replace("<p>", "").Replace("</p>", ""));

                    //答案
                    if ("判断题,单选题,多选题".IndexOf(ob.QuestionType) >= 0)
                    {
                        ob.Answer = RadTextBoxAnswer.Text.Trim().ToUpper();
                    }
                    else
                    {
                        ob.Answer = RadTextBoxAnswer.Text.Trim();
                    }
                    //难度
                    ob.Difficulty = Convert.ToByte(RadioButtonListDifficulty.SelectedValue);

                    //状态
                    ob.Flag = Convert.ToByte(RadioButtonListFlag.SelectedValue);

                    //最后修改人ID
                    ob.ModifyPersonID = PersonID;
                    ob.ModifyTime = DateTime.Now;

                    //创建人ID
                    ob.CreatePersonID = PersonID;
                    //创建时间
                    ob.CreateTime = DateTime.Now;
                    try
                    {
                        QuestionDAL.Insert(ob);
                    }
                    catch (Exception ex)
                    {
                        UIHelp.WriteErrorLog(Page, "新增试题失败！", ex);
                        return;
                    }
                    UIHelp.WriteOperateLog(PersonName, UserID, "新增试题", string.Format("新增试题：科目：{0}，试题ID：{1}。",
             RadGridPost.SelectedItems[0].Cells[RadGridPost.MasterTableView.Columns.FindByUniqueName("SubjectName").OrderIndex].Text, ob.QuestionID.ToString()));

                    UIHelp.layerAlert(Page, "新增试题成功！",6,3000);
                    #endregion 新增试题
                    break;
                case RadGrid.UpdateCommandName:
                    #region 修改试题



                    ob = QuestionDAL.GetObject(Convert.ToInt64(HiddenFieldQuestionID.Value));

                    //显示编码
                    ob.ShowCode = RadTextBoxShowCode.Text.Trim();
                    //大纲编码
                    ob.TagCode = FormatTagCode(ob.ShowCode);
                    //试题类型
                    ob.QuestionType = RadioButtonListQuestionType.SelectedValue;

                    //问题
                    ob.Title = Utility.Check.CleanHtml(RadEditorTitle.Content.Replace("<br>","\r\n").Replace("</p><p>", "\r\n").Replace("<p>","").Replace("</p>",""));

                    //答案
                    if ("判断题,单选题,多选题".IndexOf(ob.QuestionType) >= 0)
                    {
                        ob.Answer = RadTextBoxAnswer.Text.Trim().ToUpper();
                    }
                    else
                    {
                        ob.Answer = RadTextBoxAnswer.Text.Trim();
                    }
                    //难度
                    ob.Difficulty = Convert.ToByte(RadioButtonListDifficulty.SelectedValue);

                    //状态
                    ob.Flag = Convert.ToByte(RadioButtonListFlag.SelectedValue);

                    //最后修改人ID
                    ob.ModifyPersonID = PersonID;
                    ob.ModifyTime = DateTime.Now;
                    try
                    {
                        QuestionDAL.Update(ob);
                    }
                    catch (Exception ex)
                    {
                        UIHelp.WriteErrorLog(Page, "更新试题失败！", ex);
                        return;
                    }
                    UIHelp.WriteOperateLog(PersonName, UserID, "更新试题", string.Format("更新试题：科目：{0}，试题ID：{1}。",
             RadGridPost.SelectedItems[0].Cells[RadGridPost.MasterTableView.Columns.FindByUniqueName("SubjectName").OrderIndex].Text, RadGridQuestion.MasterTableView.DataKeyValues[e.Item.ItemIndex]["QuestionID"].ToString()));

                    UIHelp.layerAlert(Page, "更新试题成功！",6,3000);

                    #endregion 修改试题
                    break;
                case RadGrid.DeleteCommandName://删除
                    try
                    {
                        QuestionDAL.Delete(Convert.ToInt64(RadGridQuestion.MasterTableView.DataKeyValues[e.Item.ItemIndex]["QuestionID"]));
                    }
                    catch (Exception ex)
                    {
                        UIHelp.WriteErrorLog(Page, "删除入试题失败！", ex);
                        return;
                    }
                    UIHelp.WriteOperateLog(PersonName, UserID, "删除试题", string.Format("删除试题：科目：{0}，试题ID：{1}。",
              RadGridPost.SelectedItems[0].Cells[RadGridPost.MasterTableView.Columns.FindByUniqueName("SubjectName").OrderIndex].Text, RadGridQuestion.MasterTableView.DataKeyValues[e.Item.ItemIndex]["QuestionID"].ToString()));

                    UIHelp.layerAlert(Page, "删除试题成功！",6,3000);
                    break;
            }
        }

        //检查临时目录
        protected void CheckSaveDirectory()
        {
            //考试报名表存放路径(按考试计划ID分类)
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/ExamPage/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/ExamPage/"));
        }

        //发布
        protected void ButtonPublsh_Click(object sender, EventArgs e)
        {
            QueryParamOB q = new QueryParamOB();
            q.Add(string.Format("SubjectID={0}",RadGridPost.MasterTableView.DataKeyValues[RadGridPost.SelectedItems[0].ItemIndex]["SubjectID"]));

            if (RadTextBoxKey.Text.Trim() != "")
            {
                q.Add(string.Format(" title like '%{0}%'", RadTextBoxKey.Text.Trim()));
            }
            if (RadioButtonListStatus.SelectedValue != "")
            {
                q.Add(string.Format("Flag={0}", RadioButtonListStatus.SelectedValue));
            }

            try
            {
                CommonDAL.ExecSQL(string.Format("Update dbo.Question set flag=1 where 1=1 {0}", q.ToWhereString()));
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "批量发布试题失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(PersonName, UserID, "批量发布试题", string.Format("批量发布试题：{0}。", LabelSelectPost.Text));

            UIHelp.layerAlert(Page, "批量发布试题成功！",6,3000);
            RadGridQuestion.Rebind();
        }

        //查询过滤
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            QueryParamOB q = new QueryParamOB();
        
            q.Add(string.Format(@"
                SubjectID={0} 
                and  title like '%{1}%'
                ", RadGridPost.MasterTableView.DataKeyValues[RadGridPost.SelectedItems[0].ItemIndex]["SubjectID"], RadTextBoxKey.Text.Trim()));

            if (RadioButtonListStatus.SelectedValue != "")
            {
                 q.Add(string.Format("Flag={0}",RadioButtonListStatus.SelectedValue));
            }
            ObjectDataSource1.SelectParameters.Clear();
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridQuestion.CurrentPageIndex = 0;
            RadGridQuestion.DataSourceID = ObjectDataSource1.ID;
        }

        //正确性检查
        protected void ButtonCheck_Click(object sender, EventArgs e)
        {
            try
            {
                QueryParamOB q = new QueryParamOB();
                q.Add(string.Format(@"
                SubjectID={0} 
                and (
                    TAGCODE is null or QUESTIONTYPE is null or title is null or ANSWER is null or DIFFICULTY is null 
                    or (
                            (QUESTIONTYPE ='判断题' or QUESTIONTYPE ='单选题' or QUESTIONTYPE ='多选题')
                            and 
                            (
                                (
                                case when CHARINDEX('A' ,ANSWER ) >0 then 1 else 0 end                               
                                +case when CHARINDEX('B' , ANSWER)>0 then 1 else 0 end 
                                +case when CHARINDEX('C' , ANSWER)>0 then 1 else 0 end 
                                +case when CHARINDEX('D' , ANSWER)>0 then 1 else 0 end 
                                +case when CHARINDEX('E' , ANSWER)>0 then 1 else 0 end 
                                +case when CHARINDEX('F' , ANSWER)>0 then 1 else 0 end 
                                ) <> len(ANSWER)
                            )
                        )
                    )
                ", RadGridPost.MasterTableView.DataKeyValues[RadGridPost.SelectedItems[0].ItemIndex]["SubjectID"].ToString()));


                if (RadTextBoxKey.Text.Trim() != "")
                {
                    q.Add(string.Format(" title like '%{0}%'", RadTextBoxKey.Text.Trim()));
                }
                if (RadioButtonListStatus.SelectedValue != "")
                {
                    q.Add(string.Format("Flag={0}", RadioButtonListStatus.SelectedValue));
                }
                int errorCount = QuestionDAL.SelectCount(q.ToWhereString());

                if (errorCount > 0)
                {
                    ObjectDataSource1.SelectParameters.Clear();
                    ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
                    RadGridQuestion.CurrentPageIndex = 0;
                    RadGridQuestion.DataSourceID = ObjectDataSource1.ID;

                    UIHelp.layerAlert(Page, string.Format("下列{0}条试题记录疑似存在问题，请检查修改！", errorCount.ToString()));
                }
                else
                {
                    UIHelp.layerAlert(Page, "字段检查合格（本检查不做语义检查）！");

                    RadGridQuestion_NeedDataSource(sender, null);
                }
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "试题正确性检查失败！", ex);
                return;
            }
        }

        //批量删除
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                QueryParamOB q = new QueryParamOB();
                q.Add(string.Format("SubjectID={0}", RadGridPost.MasterTableView.DataKeyValues[RadGridPost.SelectedItems[0].ItemIndex]["SubjectID"]));

                if (RadTextBoxKey.Text.Trim() != "")
                {
                    q.Add(string.Format(" title like '%{0}%'", RadTextBoxKey.Text.Trim()));
                }
                if (RadioButtonListStatus.SelectedValue != "")
                {
                    q.Add(string.Format("Flag={0}", RadioButtonListStatus.SelectedValue));
                }

                QuestionDAL.DeleteBySubjectID(q.ToWhereString());
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "批量删除试题失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(PersonName, UserID, "批量删除试题", string.Format("批量删除试题：{0}。", LabelSelectPost.Text));

            UIHelp.layerAlert(Page, "批量删除试题成功！",6,3000);
            RadGridQuestion.Rebind();
        }


    }
}
