using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using Model;
using DataAccess;
using Telerik.Web.UI;
using System.Data;
using System.IO;
using System.Threading;

namespace ZYRYJG.EXamManage
{
    public partial class ExamCardManageSC : BasePage
    {
        protected bool isExcelExport = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (PersonType == 1 || PersonType == 4 || PersonType == 6)//培训点和管理员可以按企业名称查询
                {
                    spanUnit.Visible = true;
                    RadTxtUnitName.Visible = true;
                }
                if (PersonType == 2)
                {
                    divPrint.Style.Add("display", "none");
                }
                for (int i = 2010; i <= (DateTime.Now.Year + 1); i++)
                {
                    RadComboBoxYear.Items.Insert(0, new RadComboBoxItem(i.ToString(), i.ToString()));
                }
                RadComboBoxYear.Items.Insert(0, new RadComboBoxItem("全部", ""));
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
            ClearGridSelectedKeys(RadGridExamResult);
            QueryParamOB q = new QueryParamOB();
            switch (PersonType)
            {
                case 2://考生
                    //WorkerOB _WorkerOB = WorkerDAL.GetObject(PersonID);
                    //string IDCard15 = "";
                    //string IDCard18 = "";
                    //if (_WorkerOB == null)
                    //{
                    //    IDCard15 = IDCard18 = "null";
                    //}
                    //else if (_WorkerOB.CertificateCode.Length == 15)
                    //{
                    //    IDCard15 = _WorkerOB.CertificateCode;
                    //    IDCard18 = Utility.Check.ConvertoIDCard15To18(IDCard15);
                    //}
                    //else if (_WorkerOB.CertificateCode.Length == 18)
                    //{
                    //    IDCard15 = _WorkerOB.CertificateCode.Remove(17, 1).Remove(6, 2);
                    //    IDCard18 = _WorkerOB.CertificateCode;
                    //}
                    //else
                    //{
                    //    IDCard15 = _WorkerOB.CertificateCode;
                    //    IDCard18 = _WorkerOB.CertificateCode;
                    //}
                    //q.Add(string.Format("(CertificateCode='{0}' or CertificateCode='{1}')", IDCard15, IDCard18));//证件号码

                     q.Add(string.Format("CertificateCode='{0}'", WorkerCertificateCode));//证件号码
                    q.Add("ExamCardSendStartDate <= getdate()");//到下载时间才能下载
                    break;
                case 3://企业
                    q.Add(string.Format("UnitCode='{0}'", ZZJGDM));//组织机构代码
                    q.Add("ExamCardSendStartDate <= getdate()");//到下载时间才能下载
                    break;
                case 4://培训点
                    q.Add(string.Format("TrainUnitID = {0}", PersonID.ToString()));//培训点ID
                    q.Add("ExamCardSendStartDate <= getdate()");//到下载时间才能下载
                    break;
            }



            string _ExamPlanID = ExamPlanSelect1.ExamPlanID.HasValue ? ExamPlanSelect1.ExamPlanID.ToString() : "0";//考试计划
            if (_ExamPlanID != "0")
            {
                q.Add("ExamPlanID=" + _ExamPlanID);
                ViewState["ExamPlanID"] = _ExamPlanID;
            }
            else
            {
                ViewState["ExamPlanID"] = null;
            }
            //q.Add(string.Format("ExamPlanID={0}" ,(ExamPlanSelect1.ExamPlanID.HasValue == true)? ExamPlanSelect1.ExamPlanID.ToString():"0"));// 考试计划
            if (RadTextBoxExamPlaceName.Text.Trim() != "") q.Add(string.Format("ExamPlaceName like '%{0}%'", RadTextBoxExamPlaceName.Text.Trim()));//考点名称
            if (RadTextBoxExamRoomCode.Text.Trim() != "") q.Add(string.Format("ExamRoomCode ='{0}'", RadTextBoxExamRoomCode.Text.Trim()));//考场号
            if (RadTextBoxWorkerName.Text.Trim() != "") q.Add(string.Format("WorkerName like '%{0}%'", RadTextBoxWorkerName.Text.Trim()));//姓名
            if (RadTextBoxExamCardID.Text.Trim() != "") q.Add(string.Format("ExamCardID like '%{0}%'", RadTextBoxExamCardID.Text.Trim()));//准考证
            if (RadTextBoxCertificateCode.Text.Trim() != "") q.Add(string.Format("CertificateCode like '%{0}%'", RadTextBoxCertificateCode.Text.Trim()));//个人证件号码
            if (RadTextBoxSignUpCode.Text.Trim() != "") q.Add(string.Format("SignUpCode='{0}'", RadTextBoxSignUpCode.Text.Trim()));//报名批号
            // 单位名称
            if (RadTxtUnitName.Text != "")
            {
                q.Add(string.Format("UnitName like '%{0}%'", RadTxtUnitName.Text.Trim()));
            }

            //岗位工种
            if (PostSelect2.PostID != "")
                q.Add(string.Format("PostID = {0}", PostSelect2.PostID));
            else if (PostSelect2.PostTypeID != "")
                q.Add(string.Format("PostTypeID = {0}", PostSelect2.PostTypeID));

            ////考试时间
            //if (RadComboBoxYear.SelectedValue != "") q.Add(string.Format("DATEPART(year,ExamStartTime) = {0}", RadComboBoxYear.SelectedValue));//年
            //if (RadComboBoxMonth.SelectedValue != "") q.Add(string.Format("DATEPART(month,ExamStartTime) = {0}", RadComboBoxMonth.SelectedValue));//月

            //考试时间          
            if (RadComboBoxMonth.SelectedValue != "")
            {
                string startD = string.Format("{0}-{1}-01"
                    , (RadComboBoxYear.SelectedValue != "" ? RadComboBoxYear.SelectedValue : DateTime.Now.Year.ToString())
                    , RadComboBoxMonth.SelectedValue);

                string endD = Convert.ToDateTime(startD).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");

                q.Add(string.Format("((ExamStartTime <= '{0}' and ExamEndTime >= '{1}') or (ExamStartTime between '{0}' and '{1}') or (ExamEndTime between '{0}' and '{1}'))", startD, endD));//月
            }
            else if (RadComboBoxYear.SelectedValue != "")
            {
                q.Add(string.Format("(DATEPART(year,ExamStartTime) = {0} or DATEPART(year,ExamEndTime) = {0})", RadComboBoxYear.SelectedValue));//年
            }

            if (CheckBoxExamNotBeigin.Checked == true) q.Add(string.Format("ExamEndTime >='{0}'", DateTime.Now.ToString("yyyy-MM-dd")));//只查询考试未截止数据

            ObjectDataSource1.SelectParameters.Clear();
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());

            if (PersonType == 4)
            {
                //培训点不安准考证号排序，而按初审顺序方便打印后分发：Sort by 报名ID
                RadGridExamResult.MasterTableView.SortExpressions.Clear();
                GridSortExpression sortStr1 = new GridSortExpression();
                sortStr1.FieldName = "FirstTrialTime,ExamSignUpID";
                sortStr1.SortOrder = GridSortOrder.Ascending;
                RadGridExamResult.MasterTableView.SortExpressions.AddSortExpression(sortStr1);
            }
            else
            {
                RadGridExamResult.MasterTableView.SortExpressions.Clear();
                GridSortExpression sortStr1 = new GridSortExpression();
                sortStr1.FieldName = "ExamCardID";
                sortStr1.SortOrder = GridSortOrder.Ascending;
                RadGridExamResult.MasterTableView.SortExpressions.AddSortExpression(sortStr1);
            }
            RadGridExamResult.CurrentPageIndex = 0;
            RadGridExamResult.DataSourceID = ObjectDataSource1.ID;
            RadGridExamResult.DataBind();
            if (RadGridExamResult.Items.Count > 0)
            {
                ButtonExportToExcel.Enabled = true;
            }
        }

        //Grid换页
        protected void RadGridAccept_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateGridSelectedKeys(RadGridExamResult, "ExamResultID");
        }

        //Grid绑定勾选checkbox状态
        protected void RadGridAccept_DataBound(object sender, EventArgs e)
        {
            UpdateGriSelectedStatus(RadGridExamResult, "ExamResultID");
        }

        //导出excel
        protected void ButtonExportToExcel_Click(object sender, EventArgs e)
        {
            if (RadGridExamResult.MasterTableView.VirtualItemCount == 0)
            {
                UIHelp.layerAlert(Page, "没有可导出的数据！");
                return;
            }
            //isExcelExport = true;
            //RadGridExamResult.MasterTableView.Columns.FindByUniqueName("SelectAllColumn").Visible = false;
            //RadGridExamResult.MasterTableView.Columns.FindByUniqueName("SingUp").Visible = false;
            //RadGridExamResult.MasterTableView.Columns.FindByUniqueName("UnitName").Visible = true;
            //if (PersonType == 1 || PersonType == 6)//行政管理者导出时加“报名点”列
            //{
            //    RadGridExamResult.MasterTableView.Columns.FindByUniqueName("TrainUnitName").Visible = true;
            //    RadGridExamResult.MasterTableView.Columns.FindByUniqueName("SkillLevel").Visible = true;
            //}

            //RadGridExamResult.PageSize = RadGridExamResult.MasterTableView.VirtualItemCount;//
            //RadGridExamResult.CurrentPageIndex = 0;
            //RadGridExamResult.Rebind();
            //RadGridExamResult.ExportSettings.ExportOnlyData = true;
            //RadGridExamResult.ExportSettings.OpenInNewWindow = true;
            //RadGridExamResult.MasterTableView.ExportToExcel();
            //RadGridExamResult.MasterTableView.HeaderStyle.BackColor = System.Drawing.Color.FromName("#DEDEDE");




            QueryParamOB q = new QueryParamOB();
            switch (PersonType)
            {
                case 2://考生
                    //WorkerOB _WorkerOB = WorkerDAL.GetObject(PersonID);
                    //string IDCard15 = "";
                    //string IDCard18 = "";
                    //if (_WorkerOB == null)
                    //{
                    //    IDCard15 = IDCard18 = "null";
                    //}
                    //else if (_WorkerOB.CertificateCode.Length == 15)
                    //{
                    //    IDCard15 = _WorkerOB.CertificateCode;
                    //    IDCard18 = Utility.Check.ConvertoIDCard15To18(IDCard15);
                    //}
                    //else if (_WorkerOB.CertificateCode.Length == 18)
                    //{
                    //    IDCard15 = _WorkerOB.CertificateCode.Remove(17, 1).Remove(6, 2);
                    //    IDCard18 = _WorkerOB.CertificateCode;
                    //}
                    //else
                    //{
                    //    IDCard15 = _WorkerOB.CertificateCode;
                    //    IDCard18 = _WorkerOB.CertificateCode;
                    //}
                    //q.Add(string.Format("(CertificateCode='{0}' or CertificateCode='{1}')", IDCard15, IDCard18));//证件号码

                     q.Add(string.Format("CertificateCode='{0}'", WorkerCertificateCode));//证件号码
                    break;
                case 3://企业
                    q.Add(string.Format("UnitCode='{0}'", ZZJGDM));//组织机构代码
                    break;
                case 4://培训点
                    q.Add(string.Format("TrainUnitID = {0}", PersonID.ToString()));//培训点ID
                    break;
            }
            string _ExamPlanID = ExamPlanSelect1.ExamPlanID.HasValue ? ExamPlanSelect1.ExamPlanID.ToString() : "0";//考试计划
            if (ViewState["ExamPlanID"] != null)
            {
                q.Add("ExamPlanID=" + _ExamPlanID);
            }

            //q.Add(string.Format("ExamPlanID={0}" ,(ExamPlanSelect1.ExamPlanID.HasValue == true)? ExamPlanSelect1.ExamPlanID.ToString():"0"));// 考试计划
            if (RadTextBoxExamPlaceName.Text.Trim() != "") q.Add(string.Format("ExamPlaceName like '%{0}%'", RadTextBoxExamPlaceName.Text.Trim()));//考点名称
            if (RadTextBoxExamRoomCode.Text.Trim() != "") q.Add(string.Format("ExamRoomCode ='{0}'", RadTextBoxExamRoomCode.Text.Trim()));//考场号
            if (RadTextBoxWorkerName.Text.Trim() != "") q.Add(string.Format("WorkerName like '%{0}%'", RadTextBoxWorkerName.Text.Trim()));//姓名
            if (RadTextBoxExamCardID.Text.Trim() != "") q.Add(string.Format("ExamCardID like '%{0}%'", RadTextBoxExamCardID.Text.Trim()));//准考证
            if (RadTextBoxCertificateCode.Text.Trim() != "") q.Add(string.Format("CertificateCode like '%{0}%'", RadTextBoxCertificateCode.Text.Trim()));//个人证件号码
            if (RadTextBoxSignUpCode.Text.Trim() != "") q.Add(string.Format("SignUpCode='{0}'", RadTextBoxSignUpCode.Text.Trim()));//报名批号
            // 单位名称
            if (RadTxtUnitName.Text != "")
            {
                q.Add(string.Format("UnitName like '%{0}%'", RadTxtUnitName.Text.Trim()));
            }

            //岗位工种
            if (PostSelect2.PostID != "")
                q.Add(string.Format("PostID = {0}", PostSelect2.PostID));
            else if (PostSelect2.PostTypeID != "")
                q.Add(string.Format("PostTypeID = {0}", PostSelect2.PostTypeID));

            //考试时间
            if (RadComboBoxYear.SelectedValue != "") q.Add(string.Format("DATEPART(year,ExamStartTime) = {0}", RadComboBoxYear.SelectedValue));//年
            if (RadComboBoxMonth.SelectedValue != "") q.Add(string.Format("DATEPART(month,ExamStartTime) = {0}", RadComboBoxMonth.SelectedValue));//月
            if (CheckBoxExamNotBeigin.Checked == true) q.Add(string.Format("ExamStartTime >='{0}'", DateTime.Now.ToString("yyyy-MM-dd")));//只查询为考试数据

            string sortBy = "ExamCardID";
            //if (PersonType == 4)
            //{
            //    //培训点不安准考证号排序，而按初审顺序方便打印后分发：Sort by 报名ID
            //    sortBy = "FirstTrialTime,ExamSignUpID";
            //}
            //else
            //{
            //    sortBy = "ExamCardID";
            //}

            string saveFile = string.Format("~/UpLoad/ExamCard/准考证列表{0}_{1}.xls", DateTime.Now.ToString("yyyyMMddHHmm"), UserID);//保存文件名
            string colHead = @"岗位工种\考试时间\考点名称\考场号\准考证号\考生姓名\证件号码\企业名称\组织机构代码";
            string colName = @"PostName
                            \CONVERT(varchar(16), ExamStartTime, 20) + '-' + CONVERT(varchar(5), ExamEndTime, 114)
                            \ExamPlaceName\ExamRoomCode\ExamCardID\WorkerName\CertificateCode\UnitName\UnitCode";

            try
            {
                //检查临时目录
                if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/ExamCard/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/ExamCard/"));

                //导出数据到数据库服务器
                CommonDAL.OutputXls(MyWebConfig.DBOutputPath + Server.MapPath(saveFile).Substring(1), "DBO.VIEW_EXAMRESULT_Operation", q.ToWhereString(), sortBy, colHead, colName);

            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "导出准考证列表失败！", ex);
                return;
            }

            List<ResultUrl> url = new List<ResultUrl>();
            url.Add(new ResultUrl("准考证列表下载", saveFile));
            UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);
        }

        ////批量打印
        //protected void ButtonPrint_Click(object sender, EventArgs e)
        //{
        //    UpdateGridSelectedKeys(RadGridExamResult, "ExamResultID");
        //    if (ViewState["ExamPlanID"] == null)
        //    {
        //        long ExamPlanID = ExamResultDAL.GetExamPlanID(ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue);
        //        switch (ExamPlanID)
        //        {
        //            case -1:
        //                UIHelp.layerAlert(Page, "查询结果中不能唯一确定一次考试，无法批量操作，请修改查询条件重新查询。");
        //                return;
        //            case 0:
        //                UIHelp.layerAlert(Page, "没有可打印数据。");
        //                return;
        //            default:
        //                ViewState["ExamPlanID"] = ExamPlanID;
        //                break;
        //        }
        //    }

        //    if (IsGridSelected(RadGridExamResult) == false)
        //    {
        //        UIHelp.layerAlert(Page, "至少选择一条数据！");
        //        return;
        //    }

        //    CheckSaveDirectory();

        //    GridHeaderItem headerItem = RadGridExamResult.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;
        //    CheckAll checkAll = headerItem.Cells[0].FindControl("CheckAll1") as CheckAll;
        //    if (checkAll.IsCheckAll == true)//全选
        //    {
        //        ViewState["printPageNextIndex"] = 0;
        //        int rowCount = ExamResultDAL.SelectCountView(ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue);
        //        ViewState["MaxPageIndex"] = rowCount % 10 == 0 ? (rowCount / 10) : (rowCount / 10) + 1;
        //        Timer1.Enabled = true;
        //        UpdatePanelPrint.Visible = true;
        //        LabelTip.Text = "正在加载打印数据……";
        //        ClientScript.RegisterStartupScript(this.GetType(), "setDivManDisplay", "setDivManDisplay('none');", true);
        //    }
        //    else
        //    {
        //        Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, "~/Template/准考证_实操.doc"
        //       , string.Format("~/UpLoad/ExamCardBat/准考证_{0}.doc", PersonID.ToString())
        //       , GetPrintData(0));
        //        ClientScript.RegisterStartupScript(Page.GetType(), "printword", string.Format("Print('{1}/UpLoad/ExamCardBat/准考证_{0}.doc');", PersonID.ToString(), RootUrl), true);
        //    }
        //}

        ////批量导出Word
        //protected void ButtonExport_Click(object sender, EventArgs e)
        //{
        //    UpdateGridSelectedKeys(RadGridExamResult, "ExamResultID");
        //    if (ViewState["ExamPlanID"] == null)
        //    {
        //        long ExamPlanID = ExamResult_OperationDAL.GetExamPlanID(ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue);
        //        switch (ExamPlanID)
        //        {
        //            case -1:
        //                UIHelp.layerAlert(Page, "查询结果中不能唯一确定一次考试，无法批量操作，请修改查询条件重新查询。");
        //                return;
        //            case 0:
        //                UIHelp.layerAlert(Page, "没有可打印数据。");
        //                return;
        //            default:
        //                ViewState["ExamPlanID"] = ExamPlanID;
        //                break;
        //        }
        //    }
        //    if (IsGridSelected(RadGridExamResult) == false)
        //    {
        //        UIHelp.layerAlert(Page, "至少选择一条数据！");
        //        return;
        //    }
        //    CheckSaveDirectory();
        //    string fileID = string.Format("{0}_{1}", PersonID.ToString(), DateTime.Now.ToString("yyyyMMddHHmmss"));
        //    Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, "~/Template/准考证_实操.doc"
        //        , string.Format("~/UpLoad/ExamCardBat/准考证_{0}.doc", fileID)
        //        , GetPrintData(null));

        //    List<ResultUrl> url = new List<ResultUrl>();
        //    url.Add(new ResultUrl("准考证", string.Format("~/UpLoad/ExamCardBat/准考证_{0}.doc", fileID)));
        //    UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);
        //    //Utility.WordDelHelp.ExportWord(this.Page, Server.MapPath(string.Format("~/UpLoad/ExamCardBat/准考证_{0}.doc", PersonID.ToString())));
        //}

        //批量导出Word
        protected void ButtonExport_Click(object sender, EventArgs e)
        {
            UpdateGridSelectedKeys(RadGridExamResult, "ExamResultID");
            if (ViewState["ExamPlanID"] == null)
            {
                long ExamPlanID = ExamResult_OperationDAL.GetExamPlanID(ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue);
                switch (ExamPlanID)
                {
                    case -1:
                        UIHelp.layerAlert(Page, "查询结果中不能唯一确定一次考试，无法批量操作，请修改查询条件重新查询。");
                        return;
                    case 0:
                        UIHelp.layerAlert(Page, "没有可打印数据。");
                        return;
                    default:
                        ViewState["ExamPlanID"] = ExamPlanID;
                        break;
                }
            }
            if (IsGridSelected(RadGridExamResult) == false)
            {
                UIHelp.layerAlert(Page, "至少选择一条数据！");
                return;
            }
            CheckSaveDirectory();
            string fileID = string.Format("~/UpLoad/ExamCardBat/zhunkaozheng_{0}_{1}.docx", UserID, DateTime.Now.ToString("yyyyMMddHHmmss"));
            //Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, "~/Template/准考证_实操.doc"
            //    , string.Format("~/UpLoad/ExamCardBat/准考证_{0}.doc", fileID)
            //    , GetPrintData(null));

            string sourceFile = HttpContext.Current.Server.MapPath("~/Template/准考证_实操.docx");
            PrintDocument.CreateWordByHashtable(sourceFile, fileID, GetPrintData(), Server);

            List<ResultUrl> url = new List<ResultUrl>();
            url.Add(new ResultUrl("准考证", fileID));
            UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);
            
        }

        protected void CheckSaveDirectory()
        {
            //批量准考证存放路径
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/ExamCardBat/"))) Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/ExamCardBat/"));
        }

        //获取打印数据（按ListView分页导出Word）
        protected List<Dictionary<string, string>> GetPrintData(int? printPageIndex)
        {
            //科目（2或3科）
            Dictionary<string, string> kemuList = new Dictionary<string, string>();
            DataTable sub = ExamPlanSubjectDAL.GetListView(0, int.MaxValue - 1, string.Format(" and ExamPlanID={0}  and PostStatus=1 and PostInfo.PostID <> 50 and PostInfo.PostID <> 54 and postName='实操'", ViewState["ExamPlanID"].ToString()), "PostOrder");
            if (sub != null && sub.Rows.Count > 0)
            {
                for (int i = 0; i < sub.Rows.Count; i++)
                {
                    kemuList.Add(string.Format("km{0}_text", Convert.ToString(i + 1)), "科目：");
                    kemuList.Add(string.Format("km{0}_value", Convert.ToString(i + 1)), sub.Rows[i]["PostName"].ToString());
                    //  string.Format("{0} - {1} {2}", Convert.ToDateTime(sub.Rows[i]["ExamStartTime"]).ToString("HH:mm")
                    //  , Convert.ToDateTime(sub.Rows[i]["ExamEndTime"]).ToString("HH:mm")
                    //, sub.Rows[i]["PostName"].ToString()));
                }
            }
            if (sub.Rows.Count % 3 != 0)//考试科目未满三门
            {
                for (int i = sub.Rows.Count % 3 + 1; i <= 3; i++)
                {
                    kemuList.Add(string.Format("km{0}_text", i.ToString()), "");
                    kemuList.Add(string.Format("km{0}_value", i.ToString()), "");
                }
            }

            string filterString = "";//过滤条件

            if (GetGridIfCheckAll(RadGridExamResult) == true)//全选
            {
                filterString = ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
            }
            else
            {
                if (GetGridIfSelectedExclude(RadGridExamResult) == true)//排除
                    filterString = string.Format(" {0} and ExamResultID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridExamResult));
                else//包含
                    filterString = string.Format(" {0} and ExamResultID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridExamResult));
            }

            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            DataTable dt = null;
            if (printPageIndex.HasValue == true)
                dt = ExamResult_OperationDAL.GetListView(printPageIndex.Value * RadGridExamResult.PageSize, 10, filterString, PersonType == 4 ? "ExamSignUpID" : "ExamCardID");
            else
                dt = ExamResult_OperationDAL.GetListView(0, int.MaxValue - 1, filterString, PersonType == 4 ? "ExamSignUpID" : "ExamCardID");

            Dictionary<string, string> printData = null;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                printData = new Dictionary<string, string>();
                list.Add(printData);

                string PostTypeID = dt.Rows[i]["PostTypeID"].ToString();
                string SkillLevel = dt.Rows[i]["SkillLevel"].ToString() == "" ? "" : "(" + dt.Rows[i]["SkillLevel"].ToString() + ")";

                printData.Add("WorkerName", dt.Rows[i]["WorkerName"].ToString());//姓名
                printData.Add("PostTypeName", dt.Rows[i]["PostTypeName"].ToString());//岗位类别
                if (PostTypeID == "4")
                {
                    printData.Add("PostName", dt.Rows[i]["PostName"].ToString() + SkillLevel);//工种
                }
                else if (dt.Rows[i]["PostID"].ToString() == "12")
                {
                    if (dt.Rows[i]["EXAMPLANNAME"].ToString().Contains("暖通") == true)
                        printData.Add("PostName", dt.Rows[i]["PostName"].ToString() + "（暖通）");//工种 "（暖通）"
                    else if (dt.Rows[i]["EXAMPLANNAME"].ToString().Contains("电气") == true)
                        printData.Add("PostName", dt.Rows[i]["PostName"].ToString() + "（电气）");//工种 "（暖通）"
                    else
                        printData.Add("PostName", dt.Rows[i]["PostName"].ToString());//工种
                }
                else
                {
                    printData.Add("PostName", dt.Rows[i]["PostName"].ToString());//工种
                }

                printData.Add("UnitName", dt.Rows[i]["UnitName"].ToString());//工作单位
                printData.Add("ExamCardID", dt.Rows[i]["ExamCardID"].ToString());//准考证
                printData.Add("CertificateCode", dt.Rows[i]["CertificateCode"].ToString());//证件号码
                printData.Add("ExamPlaceName", string.Format("{0}（{1}）", dt.Rows[i]["ExamPlaceName"].ToString(), dt.Rows[i]["ExamPlaceAddress"].ToString()));//考点名称
                printData.Add("ExamRoomCode", dt.Rows[i]["ExamRoomCode"].ToString());//考场号

                printData.Add("ExamDate",string.Format("{0} - {1}"
                    , Convert.ToDateTime(dt.Rows[i]["ExamStartTime"]).ToString("yyyy年MM月dd日  HH:mm")
                    , Convert.ToDateTime(dt.Rows[i]["ExamEndTime"]).ToString("HH:mm")));//考试时间

                printData.Add("FacePhoto", dt.Rows[i]["ExamCardID"].ToString());//照片标签
                //printData.Add("Img_FacePhoto", string.Format("~/UpLoad/SignUpPhoto/{0}/{1}.jpg", dt.Rows[i]["ExamPlanID"].ToString(), dt.Rows[i]["CertificateCode"].ToString()));//绑定照片    
                printData.Add("Img_FacePhoto", GetFacePhotoPath(dt.Rows[i]["ExamPlanID"].ToString(), dt.Rows[i]["CertificateCode"].ToString()));
                foreach (string s in kemuList.Keys)
                {
                    printData.Add(s, kemuList[s]);
                }
            }
            return list;
        }

        protected List<Hashtable> GetPrintData()
        {
            //科目（2或3科）
            Hashtable kemuList = new Hashtable();
            DataTable sub = ExamPlanSubjectDAL.GetListView(0, int.MaxValue - 1, string.Format(" and ExamPlanID={0}  and PostStatus=1 and PostInfo.PostID <> 50 and PostInfo.PostID <> 54 and postName='实操'", ViewState["ExamPlanID"].ToString()), "PostOrder");
            if (sub != null && sub.Rows.Count > 0)
            {
                for (int i = 0; i < sub.Rows.Count; i++)
                {
                    kemuList.Add(string.Format("km{0}_text", Convert.ToString(i + 1)), "科目：");
                    kemuList.Add(string.Format("km{0}_value", Convert.ToString(i + 1)), sub.Rows[i]["PostName"].ToString());
                    //  string.Format("{0} - {1} {2}", Convert.ToDateTime(sub.Rows[i]["ExamStartTime"]).ToString("HH:mm")
                    //  , Convert.ToDateTime(sub.Rows[i]["ExamEndTime"]).ToString("HH:mm")
                    //, sub.Rows[i]["PostName"].ToString()));
                }
            }
            if (sub.Rows.Count % 3 != 0)//考试科目未满三门
            {
                for (int i = sub.Rows.Count % 3 + 1; i <= 3; i++)
                {
                    kemuList.Add(string.Format("km{0}_text", i.ToString()), "");
                    kemuList.Add(string.Format("km{0}_value", i.ToString()), "");
                }
            }

            string filterString = "";//过滤条件

            if (GetGridIfCheckAll(RadGridExamResult) == true)//全选
            {
                filterString = ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
            }
            else
            {
                if (GetGridIfSelectedExclude(RadGridExamResult) == true)//排除
                    filterString = string.Format(" {0} and ExamResultID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridExamResult));
                else//包含
                    filterString = string.Format(" {0} and ExamResultID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridExamResult));
            }

            List<Hashtable> list = new List<Hashtable>();
            DataTable dt = ExamResult_OperationDAL.GetListView(0, int.MaxValue - 1, filterString, PersonType == 4 ? "ExamSignUpID" : "ExamCardID");

            Hashtable printData = null;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                printData = new Hashtable();
                list.Add(printData);

                string PostTypeID = dt.Rows[i]["PostTypeID"].ToString();
                string SkillLevel = dt.Rows[i]["SkillLevel"].ToString() == "" ? "" : "(" + dt.Rows[i]["SkillLevel"].ToString() + ")";

                printData.Add("WorkerName", dt.Rows[i]["WorkerName"].ToString());//姓名
                printData.Add("PostTypeName", dt.Rows[i]["PostTypeName"].ToString());//岗位类别
                if (PostTypeID == "4")
                {
                    printData.Add("PostName", dt.Rows[i]["PostName"].ToString() + SkillLevel);//工种
                }
                else if (dt.Rows[i]["PostID"].ToString() == "12")
                {
                    if (dt.Rows[i]["EXAMPLANNAME"].ToString().Contains("暖通") == true)
                        printData.Add("PostName", dt.Rows[i]["PostName"].ToString() + "（暖通）");//工种 "（暖通）"
                    else if (dt.Rows[i]["EXAMPLANNAME"].ToString().Contains("电气") == true)
                        printData.Add("PostName", dt.Rows[i]["PostName"].ToString() + "（电气）");//工种 "（暖通）"
                    else
                        printData.Add("PostName", dt.Rows[i]["PostName"].ToString());//工种
                }
                else
                {
                    printData.Add("PostName", dt.Rows[i]["PostName"].ToString());//工种
                }

                printData.Add("UnitName", dt.Rows[i]["UnitName"].ToString());//工作单位
                printData.Add("ExamCardID", dt.Rows[i]["ExamCardID"].ToString());//准考证
                printData.Add("CertificateCode", dt.Rows[i]["CertificateCode"].ToString());//证件号码
                printData.Add("ExamPlaceName", string.Format("{0}（{1}）", dt.Rows[i]["ExamPlaceName"].ToString(), dt.Rows[i]["ExamPlaceAddress"].ToString()));//考点名称
                printData.Add("ExamRoomCode", dt.Rows[i]["ExamRoomCode"].ToString());//考场号

                printData.Add("ExamDate", string.Format("{0} - {1}"
                    , Convert.ToDateTime(dt.Rows[i]["ExamStartTime"]).ToString("yyyy年MM月dd日  HH:mm")
                    , Convert.ToDateTime(dt.Rows[i]["ExamEndTime"]).ToString("HH:mm")));//考试时间

                //printData.Add("FacePhoto", dt.Rows[i]["ExamCardID"].ToString());//照片标签
                //printData.Add("Img_FacePhoto", string.Format("~/UpLoad/SignUpPhoto/{0}/{1}.jpg", dt.Rows[i]["ExamPlanID"].ToString(), dt.Rows[i]["CertificateCode"].ToString()));//绑定照片    
                printData.Add("photo", GetFacePhotoPath(dt.Rows[i]["ExamPlanID"].ToString(), dt.Rows[i]["CertificateCode"].ToString()));
                printData.Add("Phone", dt.Rows[i]["S_PHONE"].ToString().Substring(dt.Rows[i]["S_PHONE"].ToString().Length -4));//联系电话
                foreach (string s in kemuList.Keys)
                {
                    printData.Add(s, kemuList[s]);
                }
            }
            return list;
        }

        public string GetFacePhotoPath(string ExamPlanID, string CertificateCode)
        {
            if (CertificateCode == "") return "~/Images/photo_ry.jpg";
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

        //批量分时打印
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //if (ViewState["printPageIndex"] == null || ViewState["printPageIndex"].ToString() != ViewState["printPageNextIndex"].ToString())
            //{
            //    int printPageIndex = Convert.ToInt32(ViewState["printPageNextIndex"]);
            //    Thread thread = new Thread(new ThreadStart(UpdatePrintPageIndex));
            //    thread.Start();

            //    Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, "~/Template/准考证.doc"
            //                                         , string.Format("~/UpLoad/ExamCardBat/准考证_{0}.doc", PersonID.ToString())
            //                                         , GetPrintData(printPageIndex));

            //    System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanelPrint, UpdatePanelPrint.GetType(), "printword", string.Format("Print('{1}/UpLoad/ExamCardBat/准考证_{0}.doc');", PersonID.ToString(), RootUrl), true);
            //    if (Convert.ToInt32(ViewState["printPageNextIndex"]) + 1 == Convert.ToInt32(ViewState["MaxPageIndex"]))
            //    {
            //        UpdatePanelPrint.Visible = false;
            //        Timer1.Enabled = false;
            //        LabelTip.Text = "打印完成";
            //        ViewState["printPageIndex"] = null;
            //        System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanelPrint, UpdatePanelPrint.GetType(), "setDivManDisplay", "setDivManDisplay('inline');", true);
            //    }
            //    else
            //    {
            //        LabelTip.Text = string.Format("正在打印，已完成：{0}% ……", Convert.ToString((printPageIndex + 1) * 100 / Convert.ToInt32(ViewState["MaxPageIndex"])));
            //        ViewState["printPageNextIndex"] = Convert.ToInt32(ViewState["printPageNextIndex"]) + 1;
            //    }
            //}
        }

        //更新当前打印页Index
        protected void UpdatePrintPageIndex()
        {
            ViewState["printPageIndex"] = ViewState["printPageNextIndex"];
        }
    }
}
