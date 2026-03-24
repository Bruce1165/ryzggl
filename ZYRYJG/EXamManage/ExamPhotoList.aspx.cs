using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;
using Telerik.Web.UI;
using System.Data;
using System.IO;
using System.Threading;

namespace ZYRYJG.EXamManage
{
    public partial class ExamPhotoList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        //查询
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ClearGridSelectedKeys(RadGridExamResult);
            QueryParamOB q = new QueryParamOB();
            if (ExamPlanSelect1.ExamPlanID.HasValue == true)
            {
                q.Add("ExamPlaceAllot.ExamPlanID=" + ExamPlanSelect1.ExamPlanID.ToString());// 考试计划
            }
            if (RadTextBoxExamPlaceName.Text.Trim() != "") q.Add(string.Format("ExamPlaceName like '%{0}%'", RadTextBoxExamPlaceName.Text.Trim()));//考点名称
            if (RadTextBoxExamRoomCode.Text.Trim() != "") q.Add(string.Format("ExamRoomCode ='{0}'", RadTextBoxExamRoomCode.Text.Trim()));//考场号

            ObjectDataSource1.SelectParameters.Clear();
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridExamResult.CurrentPageIndex = 0;
            RadGridExamResult.DataSourceID = ObjectDataSource1.ID;

            ViewState["ExamPlanID"] = ExamPlanSelect1.ExamPlanID;
        }
        //Grid换页
        protected void RadGridExamResult_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateGridSelectedKeys(RadGridExamResult, "ExamRoomAllotID");
        }

        //Grid绑定勾选checkbox状态
        protected void RadGridExamResult_DataBound(object sender, EventArgs e)
        {
            UpdateGriSelectedStatus(RadGridExamResult, "ExamRoomAllotID");
        }

        //批量打印
        protected void ButtonPrint_Click(object sender, EventArgs e)
        {
            UpdateGridSelectedKeys(RadGridExamResult, "ExamRoomAllotID");
            if (ViewState["ExamPlanID"] == null)
            {
                UIHelp.layerAlert(Page, "请首先选择一个考试计划进行查询，才能进行批量打印");
                return;
            }
            if (!IsGridSelected(RadGridExamResult))
            {
                UIHelp.layerAlert(Page, "你还没有选择数据！");
                return;
            }

            UIHelp.WriteOperateLog(PersonName, UserID, "批量打印考场照片阵列", string.Format("考试计划：{0}。", ExamPlanSelect1.ExamPlanName));

            CheckSaveDirectory();
            ViewState["printPageNextIndex"] = 0;

            string filterString = "";//过滤条件

            if (GetGridIfCheckAll(RadGridExamResult) == true)//全选
            {
                
            }
            else
            {
                if (GetGridIfSelectedExclude(RadGridExamResult) == true)//排除
                    filterString = string.Format(" and ExamRoomAllotID not in({0})",  GetGridSelectedKeysToString(RadGridExamResult));
                else//包含
                    filterString = string.Format(" and ExamRoomAllotID in({0})", GetGridSelectedKeysToString(RadGridExamResult));
            }

            DataTable dt = ExamRoomAllotDAL.GetList(0, int.MaxValue - 1, string.Format(" and ExamPlanID={0} {1}", ViewState["ExamPlanID"].ToString(), filterString), "ExamPlaceAllotID,ExamRoomAllotID");
            List<long> List_ExamRoomAllotID = new List<long>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                List_ExamRoomAllotID.Add(Convert.ToInt64(dt.Rows[i]["ExamRoomAllotID"]));
            }
            ViewState["List_ExamRoomAllotID"] = List_ExamRoomAllotID;
            ViewState["MaxPageIndex"] = List_ExamRoomAllotID.Count;
            Timer1.Enabled = true;
            UpdatePanelPrint.Visible = true;
            LabelTip.Text = "正在加载打印数据……";
            ClientScript.RegisterStartupScript(this.GetType(), "setDivManDisplay", "setDivManDisplay('none');", true);

            //if (ViewState["ExamPlanID"] == null)
            //{
            //    UIHelp.layerAlert(Page, "请首先选择一个考试计划进行查询，才能进行批量打印");
            //    return;
            //}

            //UIHelp.WriteOperateLog(PersonName, UserID, "批量打印考场照片阵列", string.Format("考试计划：{0}。", ExamPlanSelect1.ExamPlanName));

            //CheckSaveDirectory();
            ////ViewState["printPageNextIndex"] = 0;
            //DataTable dt = ExamRoomAllotDAL.GetList(0, int.MaxValue - 1, " and ExamPlanID=" + ViewState["ExamPlanID"].ToString(), "ExamPlaceAllotID,ExamRoomAllotID");
            //List<string> List_ExamRoomAllotID = new List<string>();
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    List_ExamRoomAllotID.Add(dt.Rows[i]["ExamRoomAllotID"].ToString());
            //}

            //Session["ListExamRoomAllotID"] = List_ExamRoomAllotID;

            //Response.Redirect("ExamPhotoBatch.aspx");
        
        }

        //批量导出
        protected void ButtonOutput_Click(object sender, EventArgs e)
        {
            UpdateGridSelectedKeys(RadGridExamResult, "ExamRoomAllotID");
            if (ViewState["ExamPlanID"] == null)
            {
                UIHelp.layerAlert(Page, "请首先选择一个考试计划进行查询！");
                return;
            }
            if (!IsGridSelected(RadGridExamResult))
            {
                UIHelp.layerAlert(Page, "你还没有选择数据！");
                return;
            }

            UIHelp.WriteOperateLog(PersonName, UserID, "批量导出考场照片阵列", string.Format("考试计划：{0}。", ExamPlanSelect1.ExamPlanName));

            CheckSaveDirectory();

            string filterString = "";//过滤条件

            if (GetGridIfCheckAll(RadGridExamResult) == true)//全选
            {

            }
            else
            {
                if (GetGridIfSelectedExclude(RadGridExamResult) == true)//排除
                    filterString = string.Format(" and ExamRoomAllotID not in({0})", GetGridSelectedKeysToString(RadGridExamResult));
                else//包含
                    filterString = string.Format(" and ExamRoomAllotID in({0})", GetGridSelectedKeysToString(RadGridExamResult));
            }

            DataTable dt = ExamRoomAllotDAL.GetList(0, int.MaxValue - 1, string.Format(" and ExamPlanID={0} {1}", ViewState["ExamPlanID"].ToString(), filterString), "ExamPlaceAllotID,ExamRoomAllotID");

            List<System.Collections.Hashtable> list = new List<System.Collections.Hashtable>();

            //StateObject callbackparam = new StateObject();
            //callbackparam.PostName = this.ExamPlanSelect1.PostName;
            //callbackparam.ExamPlanID = ViewState["ExamPlanID"].ToString();
            //callbackparam.p = this.Page;
            //callbackparam.RootUrl = BasePage.RootUrl;
         
            HiddenFieldOutputOk.Value = "";
            this.Timer2.Enabled = true;
            d.BeginInvoke(ref list, dt, ViewState["ExamPlanID"].ToString(), Server, this.Page, this.ExamPlanSelect1.PostName, null, null);
            ButtonOutput.Text = "正在生成...";
           
        }

        public delegate void CalculateFolderSizeDelegate(ref  List<System.Collections.Hashtable> list, DataTable dt, string ExamPlanID, HttpServerUtility s, System.Web.UI.Page p, string PostName);
        private static CalculateFolderSizeDelegate d = CreatOutputFile;

        //private class StateObject
        //{
        //    public string PostName { get; set; }

        //    public string ExamPlanID { get; set; }

        //    public System.Web.UI.Page p { get; set; }

        //    public string RootUrl { get; set; }
        //}

        //public  void ShowFolderSize(IAsyncResult result)
        //{
    

        //    StateObject state = (StateObject)result.AsyncState;

        //    ButtonOutput.BackColor = System.Drawing.Color.Blue;
        //    ButtonOutput.ForeColor = System.Drawing.Color.Blue;
        //    List<ResultUrl> url = new List<ResultUrl>();
        //    url.Add(new ResultUrl(string.Format("考场照片阵列_{0}.doc", ExamPlanSelect1.PostName), string.Format("~/UpLoad/ExamerPhotoList/{0}/考场照片阵列_{1}.doc", ViewState["ExamPlanID"].ToString(), ExamPlanSelect1.PostName)));

        //    System.Text.StringBuilder resultMessage = new System.Text.StringBuilder();
        //    resultMessage.Append("<table style=\"font-size:14px; font-weight:bold; color:blue; margin:20px 40px 40px 40px; \"><tr><td style=\"padding-bottom:10px; color:black;font-weight:normal; \"><nobr>").Append(UIHelp.DownLoadTip).Append("</nobr></td></tr>");
        //    System.IO.FileInfo fi;
        //    long size = 0;
        //    foreach (ResultUrl r in url)
        //    {
        //        fi = new System.IO.FileInfo(Server.MapPath(r.Url));
        //        size = fi.Length / 1024;
        //        if (size < 1024)
        //            resultMessage.Append("<tr><td align=\"left\"><a href=\"").Append(r.Url.Replace("~", state.RootUrl)).Append("\" runat=\"server\"><nobr>").Append(r.FunctionName).Append("（").Append(size.ToString()).Append(" KB）</nobr></a></td></tr>");
        //        else
        //            resultMessage.Append("<tr><td align=\"left\"><a href=\"").Append(r.Url.Replace("~", state.RootUrl)).Append("\" runat=\"server\"><nobr>").Append(r.FunctionName).Append("（").Append((size / 1024).ToString()).Append(" MB）</nobr></a></td></tr>");
        //    }
        //    resultMessage.Append("</table>");
            

        //    Session["resultMessage"] = resultMessage.ToString();
        //    RadScriptManager.RegisterStartupScript(Page, this.GetType(), "openWindow", string.Format("window.setTimeout(function(){{return radopen(\"{0}/ResultInfoPage.aspx\", \"RadWindow1\").maximize();}},500);", state.RootUrl), true);

            
            
       
        //    //(state.p.Controls[0].Controls[3].Controls[3].FindControl("Timer2") as System.Web.UI.Timer).Enabled = false;
        //    //(state.p.Controls[0].Controls[3].Controls[3].FindControl("HiddenFieldOutputOk") as System.Web.UI.WebControls.HiddenField).Value = "1";
        //    (state.p.Controls[0].Controls[3].Controls[3].FindControl("ButtonOutput") as System.Web.UI.WebControls.Button).Text= "000000000";

        //    //List<ResultUrl> url = new List<ResultUrl>();
        //    //url.Add(new ResultUrl(string.Format("考场照片阵列_{0}.doc", state.PostName), string.Format("~/UpLoad/ExamerPhotoList/{0}/考场照片阵列_{1}.doc", state.ExamPlanID, state.PostName)));

        //    //System.Text.StringBuilder resultMessage = new System.Text.StringBuilder();
        //    //resultMessage.Append("<table style=\"font-size:14px; font-weight:bold; color:blue; margin:20px 40px 40px 40px; \"><tr><td style=\"padding-bottom:10px; color:black;font-weight:normal; \"><nobr>").Append(UIHelp.DownLoadTip).Append("</nobr></td></tr>");
        //    //System.IO.FileInfo fi;
        //    //long size = 0;
        //    //foreach (ResultUrl r in url)
        //    //{
        //    //    fi = new System.IO.FileInfo(state.p.Server.MapPath(r.Url));
        //    //    size = fi.Length / 1024;
        //    //    if (size < 1024)
        //    //        resultMessage.Append("<tr><td align=\"left\"><a href=\"").Append(r.Url.Replace("~", state.RootUrl)).Append("\" runat=\"server\"><nobr>").Append(r.FunctionName).Append("（").Append(size.ToString()).Append(" KB）</nobr></a></td></tr>");
        //    //    else
        //    //        resultMessage.Append("<tr><td align=\"left\"><a href=\"").Append(r.Url.Replace("~", state.RootUrl)).Append("\" runat=\"server\"><nobr>").Append(r.FunctionName).Append("（").Append((size / 1024).ToString()).Append(" MB）</nobr></a></td></tr>");
        //    //}
        //    //resultMessage.Append("</table>");
         
        //    //state.p.Session["resultMessage"] = resultMessage.ToString();
        //    //RadScriptManager.RegisterStartupScript(state.p, state.p.GetType(), "openWindow", string.Format("window.setTimeout(function(){{return radopen(\"{0}/ResultInfoPage.aspx\", \"RadWindow1\").maximize();}},500);", state.RootUrl), true);
        //}


        //生成导出文件
        private static void CreatOutputFile(ref  List<System.Collections.Hashtable> list, DataTable dt, string ExamPlanID, HttpServerUtility s, System.Web.UI.Page p, string PostName)
        {
            (p.Controls[0].Controls[3].Controls[3].FindControl("Timer2") as System.Web.UI.Timer).Enabled = true;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                GetOutputData((long)(dt.Rows[i]["ExamRoomAllotID"]), ref list, ExamPlanID, s);
            }

            string sourceFile = s.MapPath("~/Template/考场照片阵列.docx");

            string fileName = string.Format("~/UpLoad/ExamerPhotoList/{0}/考场照片阵列_{1}.docx", ExamPlanID, PostName);

            PrintDocument.CreateWordByHashtable(sourceFile, fileName, list,s);



       //     Utility.WordDelHelp.CreateXMLWordWithDot(p, "~/Template/考场照片阵列.docx"
       //, string.Format("~/UpLoad/ExamerPhotoList/{0}/考场照片阵列_{1}.docx", ExamPlanID, PostName), list);
        }

        protected static void GetOutputData(long ExamRoomAllotID, ref List<System.Collections.Hashtable> list, string ExamPlanID, HttpServerUtility s)
        {
            string filterWhereString = " and  ExamRoomAllotID=" + ExamRoomAllotID;
       
            DataTable dt = ExamResultDAL.GetListView(0, int.MaxValue - 1, filterWhereString, "ExamCardID");

            string ExamTime = "";
            if(dt!=null && dt.Rows.Count>0)
            {
                ExamTime=ExamPlanDAL.GetExamTimeByExamPlanID(Convert.ToInt64(dt.Rows[0]["ExamPlanID"]));
            }
            System.Collections.Hashtable printData = null;
            int pageNo = 0;//第几页
            int cellNo = 0;//当前页第几单元格
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cellNo = (i % 25) + 1;
                if (i % 25 == 0)
                {
                    printData = new System.Collections.Hashtable();
                    list.Add(printData);
                    pageNo++;

                    printData.Add("PersonNumber", dt.Rows[i]["PersonNumber"].ToString());//考场人数  
                    printData.Add("ExamRoomCode", dt.Rows[i]["ExamRoomCode"].ToString());//考场号
                    printData.Add("ExamPlaceName", dt.Rows[i]["ExamPlaceName"].ToString());//考场名称
                    //if (Convert.ToDateTime(dt.Rows[i]["ExamStartDate"]).CompareTo(Convert.ToDateTime(dt.Rows[i]["ExamEndDate"])) == 0)
                    //{
                    //    printData.Add("ExamDate",string.Format("{0}日，{1}", Convert.ToDateTime(dt.Rows[i]["ExamStartDate"]).ToString("yyyy.MM.dd"),ExamTime));//考试时间
                    //}
                    //else
                    //{
                    //    printData.Add("ExamDate", string.Format("{0}-{1}日，{2}", Convert.ToDateTime(dt.Rows[i]["ExamStartDate"]).ToString("yyyy.MM.dd"), Convert.ToDateTime(dt.Rows[i]["ExamEndDate"]).ToString("yyyy.MM.dd"),ExamTime));//考试时间
                    //}

                    if (Convert.ToDateTime(dt.Rows[i]["ExamStartDate"]).ToString() == Convert.ToDateTime(dt.Rows[i]["ExamStartTime"]).ToString())
                    {
                        printData.Add("ExamDate", string.Format("{0}，{1}", Convert.ToDateTime(dt.Rows[i]["ExamStartDate"]).ToString("yyyy年MM月dd日"), ExamTime));//考试时间
                    }
                    else
                    {
                        printData.Add("ExamDate", string.Format("{0}-{1}", Convert.ToDateTime(dt.Rows[i]["ExamStartTime"]).ToString("yyyy年MM月dd日 HH:mm"), Convert.ToDateTime(dt.Rows[i]["ExamEndTime"]).ToString("HH:mm")));//考试时间
                    }

                    printData.Add("ExamPlanName", string.Format("{0}【岗位工种：{1}】", dt.Rows[i]["ExamPlanName"].ToString(),
                    UIHelp.FormatPostNameByExamplanName(Convert.ToInt32(dt.Rows[i]["PostID"]), dt.Rows[i]["PostName"].ToString(), dt.Rows[i]["ExamPlanName"].ToString())
                    ));//考试名称
                }

                printData.Add("CertificateCode_" + cellNo.ToString(), dt.Rows[i]["CertificateCode"].ToString());//证件号
                printData.Add("WorkerName_" + cellNo.ToString(), dt.Rows[i]["WorkerName"].ToString());//姓名
                printData.Add("ExamCardID_" + cellNo.ToString(), dt.Rows[i]["ExamCardID"].ToString());//准考证
                //照片
                string path = GetFacePhotoPath(ExamPlanID, dt.Rows[i]["CertificateCode"].ToString(),s);
                if (File.Exists(s.MapPath(path)) == false) path = "~/Images/photo_ry.jpg";
                //printData.Add("FacePhoto_" + cellNo.ToString(), dt.Rows[i]["CertificateCode"].ToString());//照片名称
                printData.Add("Img_FacePhoto_" + cellNo.ToString(), path);

            }
            if (dt.Rows.Count % 25 != 0)//最后一页有空行
            {
                string[] labels = "CertificateCode_,WorkerName_,ExamCardID_,FacePhoto_,Img_FacePhoto_".Split(',');//替换标签名称

                for (int i = (dt.Rows.Count % 25) + 1; i <= 25; i++)
                {
                    for (int j = 0; j < labels.Length; j++)
                    {
                        if (labels[j].IndexOf("Img_") != -1)//照片
                            printData.Add(string.Format("{0}{1}", labels[j], i.ToString()), "~/Images/null.gif");
                        else if (labels[j].IndexOf("FacePhoto_") != -1)//照片标签
                            printData.Add(string.Format("{0}{1}", labels[j], i.ToString()), "null");
                        else
                            printData.Add(string.Format("{0}{1}", labels[j], i.ToString()), "");
                    }
                }
            }
        }

        protected void Timer2_Tick(object sender, EventArgs e)
        {

            System.IO.FileInfo fi;
            long size = 0;
            fi = new System.IO.FileInfo(Server.MapPath(string.Format("~/UpLoad/ExamerPhotoList/{0}/考场照片阵列_{1}.docx", ExamPlanSelect1.ExamPlanID, ExamPlanSelect1.PostName)));
            if (fi.Exists == false) return;
            size = fi.Length / 1024;
            if (HiddenFieldOutputOk.Value != "" && HiddenFieldOutputOk.Value == size.ToString())
            {
                //导出完毕
                ButtonOutput.Text = "批量导出";
                this.Timer2.Enabled = false;
                List<ResultUrl> url = new List<ResultUrl>();
                url.Add(new ResultUrl(string.Format("考场照片阵列_{0}.docx", ExamPlanSelect1.PostName), string.Format("~/UpLoad/ExamerPhotoList/{0}/考场照片阵列_{1}.docx", ViewState["ExamPlanID"].ToString(), ExamPlanSelect1.PostName)));

                System.Text.StringBuilder resultMessage = new System.Text.StringBuilder();
                resultMessage.Append("<table style=\"font-size:14px; font-weight:bold; color:blue; margin:20px 40px 40px 40px; \"><tr><td style=\"padding-bottom:10px; color:black;font-weight:normal; \"><nobr>").Append(UIHelp.DownLoadTip).Append("</nobr></td></tr>");


                //if (size < 1024)
                //    resultMessage.Append("<tr><td align=\"left\"><a href=\"").Append(url[0].Url.Replace("~", BasePage.RootUrl)).Append("\" runat=\"server\"><nobr>").Append(url[0].FunctionName).Append("（").Append(size.ToString()).Append(" KB）</nobr></a></td></tr>");
                //else
                //    resultMessage.Append("<tr><td align=\"left\"><a href=\"").Append(url[0].Url.Replace("~", BasePage.RootUrl)).Append("\" runat=\"server\"><nobr>").Append(url[0].FunctionName).Append("（").Append((size / 1024).ToString()).Append(" MB）</nobr></a></td></tr>");

                if (size < 1024)
                    resultMessage.Append("<tr><td align=\"left\"><a href=\"").Append(UIHelp.ShowFile(url[0].Url)).Append("\" runat=\"server\"><nobr>").Append(url[0].FunctionName).Append("（").Append(size.ToString()).Append(" KB）</nobr></a></td></tr>");
                else
                    resultMessage.Append("<tr><td align=\"left\"><a href=\"").Append(UIHelp.ShowFile(url[0].Url)).Append("\" runat=\"server\"><nobr>").Append(url[0].FunctionName).Append("（").Append((size / 1024).ToString()).Append(" MB）</nobr></a></td></tr>");

                resultMessage.Append("</table>");

                Session["resultMessage"] = resultMessage.ToString();
                RadScriptManager.RegisterStartupScript(Page, this.GetType(), "openWindow", string.Format("window.setTimeout(function(){{return radopen(\"{0}/ResultInfoPage.aspx\", \"RadWindow1\").maximize();}},500);", BasePage.RootUrl), true);
                return;
            }
            HiddenFieldOutputOk.Value = size.ToString();
            if (size < 1024)
                ButtonOutput.Text = string.Format("正在生成({0} KB)", size);
            else
                ButtonOutput.Text = string.Format("正在生成({0} MB)", (size / 1024));
        }

        //批量分时打印
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            if (ViewState["printPageIndex"] == null || ViewState["printPageIndex"].ToString() != ViewState["printPageNextIndex"].ToString())
            {
                int printPageIndex = Convert.ToInt32(ViewState["printPageNextIndex"]);
                Thread thread = new Thread(new ThreadStart(UpdatePrintPageIndex));
                thread.Start();

                List<long> List_ExamRoomAllotID = ViewState["List_ExamRoomAllotID"] as List<long>;

               // Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, "~/Template/考场照片阵列.docx"
               //, string.Format("~/UpLoad/ExamerPhotoList/{0}/考场照片阵列_{1}.docx", ViewState["ExamPlanID"].ToString(), List_ExamRoomAllotID[printPageIndex].ToString())
               //, GetPrintData(List_ExamRoomAllotID[printPageIndex]));

                string sourceFile = Server.MapPath("~/Template/考场照片阵列.docx");

                string fileName = string.Format("~/UpLoad/ExamerPhotoList/{0}/考场照片阵列_{1}.docx", ViewState["ExamPlanID"], List_ExamRoomAllotID[printPageIndex]);

                PrintDocument.CreateWordByHashtable(sourceFile, fileName, GetPrintData(List_ExamRoomAllotID[printPageIndex]),Page.Server);


                System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanelPrint, UpdatePanelPrint.GetType(), "printword", string.Format("Print('{2}/UpLoad/ExamerPhotoList/{0}/考场照片阵列_{1}.docx');", ViewState["ExamPlanID"].ToString(), List_ExamRoomAllotID[printPageIndex].ToString(), RootUrl), true);
                
                if (Convert.ToInt32(ViewState["printPageNextIndex"]) + 1 == Convert.ToInt32(ViewState["MaxPageIndex"]))
                {
                    UpdatePanelPrint.Visible = false;
                    Timer1.Enabled = false;
                    LabelTip.Text = "打印完成";
                    ViewState["printPageIndex"] = null;
                    System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanelPrint, UpdatePanelPrint.GetType(), "setDivManDisplay", "setDivManDisplay('inline');", true);
                }
                else
                {
                    LabelTip.Text = string.Format("正在打印，已完成：{0}% ……", Convert.ToString((printPageIndex + 1) * 100 / Convert.ToInt32(ViewState["MaxPageIndex"])));
                    ViewState["printPageNextIndex"] = Convert.ToInt32(ViewState["printPageNextIndex"]) + 1;
                }
            }
        }

        //更新当前打印页Index
        protected void UpdatePrintPageIndex()
        {
            ViewState["printPageIndex"] = ViewState["printPageNextIndex"];
        }


        //检查文件保存路径
        protected void CheckSaveDirectory()
        {
            //照片阵列存放路径(~/UpLoad/ExamerPhotoList/考试计划ID/照片阵列_考场编号.doc)
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/ExamerPhotoList/"))) Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/ExamerPhotoList/"));
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/ExamerPhotoList/" + ViewState["ExamPlanID"].ToString()))) Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/ExamerPhotoList/" + ViewState["ExamPlanID"].ToString()));
        }

        //获取打印数据（按ListView分页导出Word）
        protected List<System.Collections.Hashtable> GetPrintData(long ExamRoomAllotID)
        {
            string filterWhereString = " and  ExamRoomAllotID=" + ExamRoomAllotID;
            List<System.Collections.Hashtable> list = new List<System.Collections.Hashtable>();
            DataTable dt = ExamResultDAL.GetListView(0, int.MaxValue - 1, filterWhereString, "ExamCardID");
            string ExamTime = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                ExamTime = ExamPlanDAL.GetExamTimeByExamPlanID(Convert.ToInt64(dt.Rows[0]["ExamPlanID"]));
            }
            System.Collections.Hashtable printData = null;
            int pageNo = 0;//第几页
            int cellNo = 0;//当前页第几单元格
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cellNo = (i % 25) + 1;
                if (i % 25 == 0)
                {
                    printData = new System.Collections.Hashtable();
                    list.Add(printData);
                    pageNo++;

                    printData.Add("PersonNumber", dt.Rows[i]["PersonNumber"].ToString());//考场人数  
                    printData.Add("ExamRoomCode", dt.Rows[i]["ExamRoomCode"].ToString());//考场号
                    printData.Add("ExamPlaceName", dt.Rows[i]["ExamPlaceName"].ToString());//考场名称
                    //if (Convert.ToDateTime(dt.Rows[i]["ExamStartDate"]).CompareTo(Convert.ToDateTime(dt.Rows[i]["ExamEndDate"])) == 0)
                    //{
                    //     printData.Add("ExamDate",string.Format("{0}日，{1}", Convert.ToDateTime(dt.Rows[i]["ExamStartDate"]).ToString("yyyy.MM.dd"),ExamTime));//考试时间
                    //}
                    //else
                    //{
                    //    printData.Add("ExamDate", string.Format("{0}-{1}日，{2}", Convert.ToDateTime(dt.Rows[i]["ExamStartDate"]).ToString("yyyy.MM.dd"), Convert.ToDateTime(dt.Rows[i]["ExamEndDate"]).ToString("yyyy.MM.dd"),ExamTime));//考试时间
                    //}
                    if (Convert.ToDateTime(dt.Rows[i]["ExamStartDate"]).ToString() == Convert.ToDateTime(dt.Rows[i]["ExamStartTime"]).ToString())
                    {
                        printData.Add("ExamDate", string.Format("{0}，{1}", Convert.ToDateTime(dt.Rows[i]["ExamStartDate"]).ToString("yyyy年MM月dd日"), ExamTime));//考试时间
                    }
                    else
                    {
                        printData.Add("ExamDate", string.Format("{0}-{1}", Convert.ToDateTime(dt.Rows[i]["ExamStartTime"]).ToString("yyyy年MM月dd日 HH:mm"), Convert.ToDateTime(dt.Rows[i]["ExamEndTime"]).ToString("HH:mm")));//考试时间
                    }

                    //printData.Add("ExamPlanName", string.Format("{0}【岗位工种：{1}】", dt.Rows[i]["ExamPlanName"].ToString(), dt.Rows[i]["PostName"].ToString()));//考试名称
                    printData.Add("ExamPlanName", string.Format("{0}【岗位工种：{1}】", dt.Rows[i]["ExamPlanName"].ToString(),
                    UIHelp.FormatPostNameByExamplanName(Convert.ToInt32(dt.Rows[i]["PostID"]), dt.Rows[i]["PostName"].ToString(), dt.Rows[i]["ExamPlanName"].ToString())
                    ));//考试名称
                }

                printData.Add("CertificateCode_" + cellNo.ToString(), dt.Rows[i]["CertificateCode"].ToString());//证件号
                printData.Add("WorkerName_" + cellNo.ToString(), dt.Rows[i]["WorkerName"].ToString());//姓名
                printData.Add("ExamCardID_" + cellNo.ToString(), dt.Rows[i]["ExamCardID"].ToString());//准考证
                //照片
                string path = GetFacePhotoPath(ViewState["ExamPlanID"].ToString(), dt.Rows[i]["CertificateCode"].ToString());
                if (File.Exists(Server.MapPath(path)) == false) path = "~/Images/photo_ry.jpg";
                //printData.Add("FacePhoto_" + cellNo.ToString(), dt.Rows[i]["CertificateCode"].ToString());//照片名称
                printData.Add("Img_FacePhoto_" + cellNo.ToString(), path);

            }
            if (dt.Rows.Count % 25 != 0)//最后一页有空行
            {
                string[] labels = "CertificateCode_,WorkerName_,ExamCardID_,FacePhoto_,Img_FacePhoto_".Split(',');//替换标签名称

                for (int i = (dt.Rows.Count % 25) + 1; i <= 25; i++)
                {
                    for (int j = 0; j < labels.Length; j++)
                    {
                        if (labels[j].IndexOf("Img_") != -1)//照片
                            printData.Add(string.Format("{0}{1}", labels[j], i.ToString()), "~/Images/null.gif");
                        else if (labels[j].IndexOf("FacePhoto_") != -1)//照片标签
                            printData.Add(string.Format("{0}{1}", labels[j], i.ToString()), "null");
                        else
                            printData.Add(string.Format("{0}{1}", labels[j], i.ToString()), "");
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 获取个人照片地址
        /// </summary>
        /// <param name="ExamPlanID">考试计划ID</param>
        /// <param name="CertificateCode">证件号码</param>
        /// <returns></returns>
        public string GetFacePhotoPath(string ExamPlanID, string CertificateCode)
        {
            if (CertificateCode == "") return "~/Images/tup.gif";
            string path = string.Format("~/UpLoad/SignUpPhoto/{0}/{1}.jpg", ExamPlanID, CertificateCode);
            if (File.Exists(Server.MapPath(path)) == true)
                return path;
            else
            {
                path = string.Format("~/UpLoad/WorkerPhoto/{0}/{1}.jpg", CertificateCode.Substring(CertificateCode.Length - 3, 3), CertificateCode);
                if (File.Exists(Server.MapPath(path)) == true)
                    return path;
                else
                    return "~/Images/tup.gif";
            }
        }


        public static string GetFacePhotoPath(string ExamPlanID, string CertificateCode, HttpServerUtility s)
        {
            if (CertificateCode == "") return "~/Images/tup.gif";
            string path = string.Format("~/UpLoad/SignUpPhoto/{0}/{1}.jpg", ExamPlanID, CertificateCode);
            if (File.Exists(s.MapPath(path)) == true)
                return path;
            else
            {
                path = string.Format("~/UpLoad/WorkerPhoto/{0}/{1}.jpg", CertificateCode.Substring(CertificateCode.Length - 3, 3), CertificateCode);
                if (File.Exists(s.MapPath(path)) == true)
                    return path;
                else
                    return "~/Images/tup.gif";
            }
        }



        
    }


}
