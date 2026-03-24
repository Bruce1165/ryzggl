using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Model;
using DataAccess;
using System.Data;
using Utility;
using System.IO;

namespace ZYRYJG.EXamManage
{
    /// <summary>
    /// 不干胶打印：作废
    /// </summary>
    public partial class PMCertPrintManage : BasePage
    {
//        protected override void Page_Init(object sender, EventArgs e)
//        {
//            if (string.IsNullOrEmpty(Request["t"]) == false && Request.QueryString["t"].ToString() == "Continue")
//            {
//                //续期打印使用不同的数据源
//                ObjectDataSource1.SelectMethod = "GetListContinuePrint";
//                ObjectDataSource1.SelectCountMethod = "SelectCountContinuePrint";
//                TrPage.Visible = true;
//            }

//            base.Page_Init(sender, e);
//        }

//        protected void Page_Load(object sender, EventArgs e)
//        {            
//            if (!this.IsPostBack)
//            {
//                //RadComboBoxPrint.Items.FindItemByText("未打印").Selected = true;
//                //RadComboBoxCaseStatus.Items.FindItemByText("未归档").Selected = true;
//                ViewState["ChangeType"] = Request["t"];//续期或变更
//                ViewState["PostTypeID"] = Request["o"];//岗位类型

//                if (string.IsNullOrEmpty(Request["o"]) != true)//续期或变更
//                {
//                    PostSelect1.PostTypeID = string.IsNullOrEmpty(Request["o"]) ? "1" : Request["o"].ToString();
//                    //PostSelect1.LockPostTypeID();
      
//                    //变更，显示受理告知批次号查询条件
//                    TrChangeQueryParam.Visible = (Request.QueryString["t"].ToString() == "Manage");

//                    //续期，显示决定批次号查询条件
//                    TrContinueParm.Visible = (Request.QueryString["t"].ToString() == "Continue");

//                }                
                
//                ButtonSearch_Click(sender, e);
//            }
//        }

//        //查询
//        protected void ButtonSearch_Click(object sender, EventArgs e)
//        {
//            if (UIHelp.CheckSQLParam() == false)
//            {
//                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
//                return;
//            }

//            ClearGridSelectedKeys(RadGridCertificate);
//            if (RadTextBoxCodeFrom.Text != "" && RadTextBoxCodeTo.Text != "")
//            {
//                if (Convert.ToInt32(RadTextBoxCodeFrom.Text) > Convert.ToInt32(RadTextBoxCodeTo.Text))
//                {
//                    UIHelp.layerAlert(Page, "查询条件中“证书编号”流水号范围错误，正确格式如：前缀“京10”，流水号“801”-“808”。");
//                    return;
//                }
//            }
//            ObjectDataSource1.SelectParameters.Clear();
//            QueryParamOB q = new QueryParamOB();
//            //有效证书
//            //q.Add(string.Format("Status in('{0}','{1}','{2}','{3}','{4}')", EnumManager.CertificateUpdateType.first, EnumManager.CertificateUpdateType.InBeiJing, EnumManager.CertificateUpdateType.ChangeInBeiJing, EnumManager.CertificateUpdateType.Continue, EnumManager.CertificateUpdateType.Patch));

//            if (ExamPlanSelect1.ExamPlanID.HasValue == true) q.Add("ExamPlanID=" + ExamPlanSelect1.ExamPlanID.ToString());// 考试计划
//            if (string.IsNullOrEmpty(Request["o"]) == true)//考试报名(证书打印)
//            {

//                q.Add(string.Format("Status in('{0}')", EnumManager.CertificateUpdateType.first));//证书更新状态(首次)
//                //Post.Visible = false;//隐藏岗位工种控件
//                //PostSelect1.Visible = false;
//                ExamPlan.Visible = true;
//            }
//            else//续期或变更
//            {
//                ExamPlan.Visible = false;//隐藏考试计划控件
//                RadWindowManager1.OnClientClose = "";
//                Post.Visible = true;
//                PostSelect1.Visible = true;

//                //有效证书
//                q.Add(string.Format("Status in('{0}','{1}','{2}','{3}')", EnumManager.CertificateUpdateType.InBeiJing, EnumManager.CertificateUpdateType.ChangeInBeiJing, EnumManager.CertificateUpdateType.Continue, EnumManager.CertificateUpdateType.Patch));
//                //q.Add(string.Format("PostTypeID = {0}", Request.QueryString["o"].ToString()));//岗位类型

//            }
//            if (PostSelect1.PostID != "")
//            {
//                //q.Add(string.Format("PostID = '{0}'", PostSelect1.PostID));//工种
//                switch (PostSelect1.PostID)
//                {
//                    case "9"://土建
//                        q.Add(string.Format("((PostID >= {0} and PostID <= {0}) or PostName like '%增土建')", PostSelect1.PostID));
//                        break;
//                    case "12"://安装
//                        q.Add(string.Format("((PostID >= {0} and PostID <= {0}) or PostName like '%增安装')", PostSelect1.PostID));
//                        break;
//                    default:
//                        q.Add(string.Format("PostID = {0}", PostSelect1.PostID));
//                        break;
//                }
//            }
//            else if (PostSelect1.PostTypeID != "")
//            {
//                q.Add(string.Format("PostTypeID = {0}", PostSelect1.PostTypeID));//岗位类别
//            }

//            if (RadComboBoxPrint.SelectedItem.Text == "已打印")
//            {
//                q.Add("PrintMan is not null");//打印人不为空(已打印)
//            }
//            else if (RadComboBoxPrint.SelectedItem.Text == "未打印")
//            {
//                q.Add("PrintMan is null");//打印人为空(未打印)
//            }
//            if (RadComboBoxCaseStatus.SelectedItem.Text == "已归档")
//            {
//                q.Add("CaseStatus='已归档'");//归档状态
//            }
//            else if (RadComboBoxCaseStatus.SelectedItem.Text == "未归档")
//            {
//                q.Add("CaseStatus is null");//归档状态
//            }

//            //变更批次号（告知批号）
//            if (TrChangeQueryParam.Visible == true)
//            {
//                if (RadTextBoxNoticeCode.Text.Trim() != "")
//                {
//                    q.Add(string.Format("CertificateID in(select CertificateID from dbo.CertificateChange where NoticeCode like '%{0}%')", RadTextBoxNoticeCode.Text.Trim()));
//                }
//                //变更受理时间
//                if (RadDatePickerNoticeDateStart.SelectedDate.HasValue || RadDatePickerNoticeDateEnd.SelectedDate.HasValue)
//                {
//                    q.Add(string.Format("CertificateID in(select CertificateID from dbo.CertificateChange where ( NoticeDate BETWEEN  '{0}' AND '{1}'))"
//                        , RadDatePickerNoticeDateStart.SelectedDate.HasValue ? RadDatePickerNoticeDateStart.SelectedDate.Value.ToString() : DateTime.MinValue.AddDays(1).ToString()
//                        , RadDatePickerNoticeDateEnd.SelectedDate.HasValue ? RadDatePickerNoticeDateEnd.SelectedDate.Value.AddDays(1).AddMinutes(-1).ToString() : DateTime.MaxValue.AddDays(-1).ToString()));
//                }
//            }          

//            if (RadTextBoxPrintMan.Text.Trim() != "")
//            {
//                if (RadComboBoxPrint.SelectedItem.Text == "未打印")
//                {
//                    q.Add("1=2");
//                    UIHelp.layerAlert(Page, "同时按“未打印”和“打印人”查询，无法查到结果！");
//                }
//                q.Add(string.Format("PrintMan like '%{0}%'", RadTextBoxPrintMan.Text.Trim()));// 打印人
//            }

//            //打印时间
//            if (RadDatePicker_PrintStartDate.SelectedDate.HasValue || RadDatePicker_PrintEndDate.SelectedDate.HasValue)
//            {
//                if (RadComboBoxPrint.SelectedItem.Text == "未打印")
//                {
//                    q.Add("1=2");
//                    UIHelp.layerAlert(Page, "同时按“未打印”和“打印时间”查询，无法查到结果！");
//                }
//                q.Add(string.Format("( PrintDate BETWEEN  '{0}' AND '{1}')"
//                    , RadDatePicker_PrintStartDate.SelectedDate.HasValue ? RadDatePicker_PrintStartDate.SelectedDate.Value.ToString() : DateTime.MinValue.AddDays(1).ToString()
//                    , RadDatePicker_PrintEndDate.SelectedDate.HasValue ? RadDatePicker_PrintEndDate.SelectedDate.Value.AddDays(1).ToString() : DateTime.MaxValue.AddDays(-1).ToString()));
//            }

//            if (RadTextBoxWorkerName.Text.Trim() != "")
//                q.Add(string.Format("WorkerName like '%{0}%'", RadTextBoxWorkerName.Text.Trim()));// 考生姓名

//            if (RadTextBoxUnitName.Text.Trim() != "")
//                q.Add(string.Format("UnitName like '%{0}%'", RadTextBoxUnitName.Text.Trim()));// 企业名称

//            if (RadTextBoxPeiXunUnitName.Text.Trim() != "")
//                q.Add(string.Format("ApplyMan like '%{0}%'", RadTextBoxPeiXunUnitName.Text.Trim()));// 培训点

//            //证书编号：前缀 + 流水号范围
//            if (RadTextBoxCertificateCode.Text.Trim() != "") q.Add(string.Format("CertificateCode like '{0}%'", RadTextBoxCertificateCode.Text.Trim()));// 证书编号前缀
//            if (RadTextBoxCodeFrom.Text != "" || RadTextBoxCodeTo.Text != "")
//            {
//                int maxLength = 0;//取流水号位数   
//                string beginNum = "";//流水号开始
//                string endNum = "";//流水号结束
//                if (RadTextBoxCodeFrom.Text != "" && RadTextBoxCodeFrom.Text.Length > maxLength)
//                    maxLength = RadTextBoxCodeFrom.Text.Length;
//                if (RadTextBoxCodeTo.Text != "" && RadTextBoxCodeTo.Text.Length > maxLength)
//                    maxLength = RadTextBoxCodeTo.Text.Length;
//                beginNum = RadTextBoxCodeFrom.Text != "" ? RadTextBoxCodeFrom.Text : RadTextBoxCodeTo.Text;
//                endNum = RadTextBoxCodeTo.Text != "" ? RadTextBoxCodeTo.Text : RadTextBoxCodeFrom.Text;
//                q.Add(string.Format("ISNUMERIC(right (CERTIFICATECODE,{0}) )= 1 and cast(right(CERTIFICATECODE,{0}) as int) between {1} and {2}", maxLength.ToString(), beginNum, endNum));
//            }   

//            //续期决定批次号（注意必须放到最后）
//            if (TrContinueParm.Visible == true)
//            {
//                if (RadTextBoxConfirmCode.Text.Trim() != "")
//                {
//                    q.Add(string.Format(" ConfirmCode like '%{0}%'", RadTextBoxConfirmCode.Text.Trim()));
//                }
//                //续期决定时间
//                if (RadDatePickerConfirmDateStart.SelectedDate.HasValue || RadDatePickerConfirmDateEnd.SelectedDate.HasValue)
//                {
//                    q.Add(string.Format("Continue_ConfirmDate BETWEEN  '{0}' AND '{1}'"
//                        , RadDatePickerConfirmDateStart.SelectedDate.HasValue ? RadDatePickerConfirmDateStart.SelectedDate.Value.ToString() : DateTime.MinValue.AddDays(1).ToString()
//                        , RadDatePickerConfirmDateEnd.SelectedDate.HasValue ? RadDatePickerConfirmDateEnd.SelectedDate.Value.AddDays(1).AddMinutes(-1).ToString() : DateTime.MaxValue.AddDays(-1).ToString()));
//                }


//                //页号（注意必须放到最后）
//                if (RadTextBoxPageFrom.Text.Trim() != "" && RadTextBoxPageTo.Text.Trim() != "")
//                {
//                    q.Add(string.Format(
//                        @"CertificateID in(select certificateid from
//                                            (
//	                                            select *,row_number() over(order by {1}) as rn 
//	                                            from
//	                                            (
//		                                            select certificateid,CONTINUE_CHECKDATE,CONTINUE_GETDATE,UNITCODE,POSTID,CERTIFICATECODE
//		                                            from dbo.VIEW_CERTIFICATE_PRINTCONTINUE 
//		                                            where 1=1 {0} 	
//	                                            ) t1	
//                                            ) t2
//                                            where rn between {3} and {2})"

//                        , q.ToWhereString()
//                        , "CONTINUE_CHECKDATE,CONTINUE_GETDATE,UNITCODE,POSTID,CERTIFICATECODE"
//                        , (Convert.ToInt32(RadTextBoxPageTo.Text.Trim()) * RadGridCertificate.PageSize)
//                        , (Convert.ToInt32(RadTextBoxPageFrom.Text.Trim()) - 1) * RadGridCertificate.PageSize + 1
//                        ));
//                }

//            }

//            //排序规则
//            RadGridCertificate.MasterTableView.AllowMultiColumnSorting = true;
//            RadGridCertificate.MasterTableView.SortExpressions.Clear();
//            if (string.IsNullOrEmpty(Request["o"]) != true && Request.QueryString["t"].ToString() == "Continue")//续期
//            {
//                GridSortExpression sortStr1 = new GridSortExpression();
//                sortStr1.FieldName = "CONTINUE_CHECKDATE,CONTINUE_GETDATE,UNITCODE,POSTID,CERTIFICATECODE";
//                sortStr1.SortOrder = GridSortOrder.Ascending;
//                RadGridCertificate.MasterTableView.SortExpressions.AddSortExpression(sortStr1);
//            }
//            else
//            {
//                GridSortExpression sortStr1 = new GridSortExpression();
//                sortStr1.FieldName = "PostID,CertificateCode";
//                sortStr1.SortOrder = GridSortOrder.Ascending;
//                RadGridCertificate.MasterTableView.SortExpressions.AddSortExpression(sortStr1);
//            }

//            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
//            RadGridCertificate.CurrentPageIndex = 0;
//            RadGridCertificate.DataSourceID = ObjectDataSource1.ID;
//        }

//        //Grid换页
//        protected void RadGridAccept_PageIndexChanged(object source, GridPageChangedEventArgs e)
//        {
//            UpdateGridSelectedKeys(RadGridCertificate, "CertificateID");
//        }

//        //Grid绑定勾选checkbox状态
//        protected void RadGridAccept_DataBound(object sender, EventArgs e)
//        {
//            UpdateGriSelectedStatus(RadGridCertificate, "CertificateID");
//        }

//        //证书打印
//        protected void ButtonPrint_Click(object sender, EventArgs e)
//        {
//            UpdateGridSelectedKeys(RadGridCertificate, "CertificateID");
//            if (!IsGridSelected(RadGridCertificate))
//            {
//                UIHelp.layerAlert(Page, "你还没有选择证书！");
//                return;
//            }
//            string filterString = "";//过滤条件

//            if (GetGridIfCheckAll(RadGridCertificate) == true)//全选
//            {
//                filterString = ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
//            }
//            else
//            {
//                if (GetGridIfSelectedExclude(RadGridCertificate) == true)//排除
//                    filterString = string.Format(" {0} and CertificateID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridCertificate));
//                else//包含
//                    filterString = string.Format(" {0} and CertificateID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridCertificate));
//            }
//            List<string> printList = new List<string>();//待打印证书ID集合              
//            DataTable dt = CertificateDAL.GetList(0, int.MaxValue - 1, filterString, "CertificateCode");

//            if (string.IsNullOrEmpty(Request["o"]) != true && Request.QueryString["t"].ToString() == "Continue")//续期
//            {
//                dt = CertificateDAL.GetList(0, int.MaxValue - 1, filterString, "UnitCode,CertificateID");               
//            }
//            else
//            {
//                dt = CertificateDAL.GetList(0, int.MaxValue - 1, filterString, "PostID,CertificateCode");            
//            }

//            for (int i = 0; i < dt.Rows.Count; i++)
//            {
//                printList.Add(dt.Rows[i]["CertificateID"].ToString());               
//            }

//            Session["printList"] = printList;
//            Session["printTable"] = dt;
//            Response.Redirect(string.Format("PMCertPrint.aspx?CertificateID=&rtn_o={0}&rnt_t={1}",
//                 ViewState["PostTypeID"] == null ? "" : ViewState["PostTypeID"].ToString(),
//                  ViewState["ChangeType"] == null ? "" : ViewState["ChangeType"].ToString()),
//                false);
//        }

//        //归档
//        protected void ButtonCaseUpdate_Click(object sender, EventArgs e)
//        {
//            UpdateGridSelectedKeys(RadGridCertificate, "CertificateID");
//            if (!IsGridSelected(RadGridCertificate))
//            {
//                UIHelp.layerAlert(Page, "你还没有选择证书！");
//                return;
//            }
//            string filterString = "";//过滤条件

//            if (GetGridIfCheckAll(RadGridCertificate) == true)//全选
//            {
//                filterString = ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
//            }
//            else
//            {
//                if (GetGridIfSelectedExclude(RadGridCertificate) == true)//排除
//                    filterString = string.Format(" {0} and CertificateID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridCertificate));
//                else//包含
//                    filterString = string.Format(" {0} and CertificateID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridCertificate));
//            }

//            int count = CertificateDAL.SelectCount(filterString);
//            System.Text.StringBuilder sb = new System.Text.StringBuilder();
//            try
//            {               
//                CertificateDAL.UpdateCaseStatus(filterString);          
//            }
//            catch (Exception ex)
//            {
//                UIHelp.WriteErrorLog(Page, "证书打印归档失败", ex);
//                return;
//            }
//            UIHelp.WriteOperateLog(PersonName, UserID, "证书打印归档", string.Format("归档证书数量：{0}本。"
//                    , count.ToString()));
//            UIHelp.layerAlert(Page, "证书归档成功");
//            ClearGridSelectedKeys(RadGridCertificate);
//            RadGridCertificate.DataBind();
//        }

//        //检查文件保存路径
//        protected void CheckSaveDirectory(string PostType)
//        {
//            //证书申请书存放路径(~/UpLoad/CertifApply/考试计划ID/照片阵列_考场编号.doc)
//            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/PrintCertificate/"))) Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/PrintCertificate/"));
//            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/PrintCertificate/" + PostType))) Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/PrintCertificate/" + PostType));
//        }

//        //打印证书照片阵列
//        protected void btnPrint_Click(object sender, EventArgs e)
//        {
//            UpdateGridSelectedKeys(RadGridCertificate, "CertificateID");
//            if (!IsGridSelected(RadGridCertificate))
//            {
//                UIHelp.layerAlert(Page, "你还没有选择证书！");
//                return;
//            }
//            else
//            {
//                CheckSaveDirectory();
//                Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, "~/Template/证书照片阵列.doc"
//                     , string.Format("~/UpLoad/CertificatePhotoList/证书照片阵列_{0}.doc", PersonID.ToString())
//                    , GetPrintData());
//                ClientScript.RegisterStartupScript(Page.GetType(), "printword", string.Format("Print('{1}/UpLoad/CertificatePhotoList/证书照片阵列_{0}.doc');", PersonID.ToString(), RootUrl), true);
//            }
//        }

//        //检查文件保存路径
//        protected void CheckSaveDirectory()
//        {
//            //照片阵列存放路径(~/UpLoad/CertificatePhotoList/照片阵列_考场编号.doc)
//            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/CertificatePhotoList/"))) Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/CertificatePhotoList/"));
//            //if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/CertificatePhotoList/" + ViewState["ExamPlanID"].ToString()))) Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/CertificatePhotoList/" + ViewState["ExamPlanID"].ToString()));
//        }

//        //获取照片阵列打印数据（按ListView分页导出Word）
//        protected List<Dictionary<string, string>> GetPrintData()
//        {
//            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();

//            Dictionary<string, string> printData = null;
//            int cellNo = 0;//当前页第几单元格
//            int pagecount = 0; //总页数
//            string filterString = "";//过滤条件

//            if (GetGridIfCheckAll(RadGridCertificate) == true)//全选
//            {
//                filterString = ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
//            }
//            else
//            {
//                if (GetGridIfSelectedExclude(RadGridCertificate) == true)//排除
//                    filterString = string.Format(" {0} and CertificateID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridCertificate));
//                else//包含
//                    filterString = string.Format(" {0} and CertificateID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridCertificate));
//            }

//            string workcode = "";//证件号码
//            int photoCount = 0;

//            DataTable dt = CertificateDAL.GetList(0, int.MaxValue - 1, filterString, "CertificateCode");
//            photoCount = dt.Rows.Count;

//            if (dt.Rows.Count % 25 == 0)
//            { pagecount = dt.Rows.Count / 25; }
//            else
//            {
//                pagecount = dt.Rows.Count / 25 + 1;
//            }
//            for (int i = 0; i < dt.Rows.Count; i++)
//            {
//                cellNo = i % 25 + 1;
//                if (i % 25 == 0)
//                {
//                    printData = new Dictionary<string, string>();
//                    list.Add(printData);
//                }

//                printData.Add("ExamCardID_" + cellNo.ToString(), dt.Rows[i]["CertificateCode"].ToString());//证书编号
//                printData.Add("WorkerName_" + cellNo.ToString(), dt.Rows[i]["WorkerName"].ToString());//姓名
//                printData.Add("CertificateCode_" + cellNo.ToString(), dt.Rows[i]["WorkerCertificateCode"].ToString());//证件号

//                //证件号码的后三位
//                workcode = dt.Rows[i]["WorkerCertificateCode"].ToString();
//                printData.Add("FacePhoto_" + cellNo.ToString(), dt.Rows[i]["CertificateCode"].ToString());//照片名称
//                //printData.Add("Img_FacePhoto_" + cellNo.ToString(), GetFacePhotoPath(dt.Rows[i]["WorkerCertificateCode"].ToString()));
//                printData.Add("Img_FacePhoto_" + cellNo.ToString(), UIHelp.GetFaceImagePath(dt.Rows[i]["FacePhoto"].ToString(), dt.Rows[i]["WorkerCertificateCode"].ToString()));
//            }

//            if (photoCount % 25 != 0)//最后一页有空行
//            {
//                string[] labels = "CertificateCode_,WorkerName_,ExamCardID_,FacePhoto_,Img_FacePhoto_".Split(',');//替换标签名称

//                for (int i = (photoCount % 25) + 1; i <= 25; i++)
//                {
//                    for (int j = 0; j < labels.Length; j++)
//                    {
//                        if (labels[j].IndexOf("Img_") != -1)//照片
//                            printData.Add(string.Format("{0}{1}", labels[j], i.ToString()), "~/Images/null.gif");
//                        else if (labels[j].IndexOf("FacePhoto_") != -1)//照片标签
//                            printData.Add(string.Format("{0}{1}", labels[j], i.ToString()), "null");
//                        else
//                            printData.Add(string.Format("{0}{1}", labels[j], i.ToString()), "");
//                    }
//                }
//            }
//            return list;
//        }

//        //导出列表（excel），统计工作量
//        protected void ButtonExportExcel_Click(object sender, EventArgs e)
//        {
//            if (RadGridCertificate.MasterTableView.VirtualItemCount == 0)
//            {
//                UIHelp.layerAlert(Page, "没有可导出的数据！");
//                return;
//            }
//            RadGridCertificate.MasterTableView.Columns.FindByUniqueName("TemplateColumn").Visible = false;
//            //RadGridCertificate.MasterTableView.Columns.FindByUniqueName("PrintStatus").Visible = false;
//            RadGridCertificate.MasterTableView.Columns.FindByUniqueName("DetailView").Visible = false;
//            RadGridCertificate.MasterTableView.Columns.FindByUniqueName("PrintMan").Visible = true;
//            RadGridCertificate.MasterTableView.Columns.FindByUniqueName("PrintDate").Visible = true;
            
//            RadGridCertificate.PageSize = RadGridCertificate.MasterTableView.VirtualItemCount;//
//            RadGridCertificate.CurrentPageIndex = 0;
//            RadGridCertificate.Rebind();
//            RadGridCertificate.ExportSettings.ExportOnlyData = true;
//            RadGridCertificate.ExportSettings.OpenInNewWindow = true;


//            RadGridCertificate.MasterTableView.ExportToExcel();
//        }

//        //导出excel格式化
//        protected void RadGridCertificate_ExcelExportCellFormatting(object source, ExcelExportCellFormattingEventArgs e)
//        {
//            switch (e.FormattedColumn.UniqueName)
//            {
//                case "ExamCardID": e.Cell.Style["mso-number-format"] = @"\@"; break;
//                case "WorkerCertificateCode": e.Cell.Style["mso-number-format"] = @"\@"; break;
//                case "CertificateCode": e.Cell.Style["mso-number-format"] = @"\@"; break;
//            }
//            //HeadCell
//            GridItem item = e.Cell.Parent as GridItem;
//            if (item.ItemIndex == 0 && e.FormattedColumn.UniqueName == "RowNum")
//            {
//                GridTableView gtv = e.Cell.Parent.Parent.Parent as GridTableView;
//                GridItem ghi = gtv.GetItems(GridItemType.Header)[0];
//                for (int i = 0; i < ghi.Cells.Count; i++)
//                {
//                    ghi.Cells[i].Style.Add("border-width", "0.1pt");
//                    ghi.Cells[i].Style.Add("border-style", "solid");
//                    ghi.Cells[i].Style.Add("border-color", "#CCCCCC");
//                }
//            }
//            //Itemcell
//            e.Cell.Attributes.Add("align", "center");
//            e.Cell.Style.Add("border-width", "0.1pt");
//            e.Cell.Style.Add("border-style", "solid");
//            e.Cell.Style.Add("border-color", "#CCCCCC");
//        }

//        //grid格式化
//        protected void RadGridCertificate_ItemDataBound(object sender, GridItemEventArgs e)
//        {
//            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
//            {
//                ////有效期
//                //e.Item.Cells[RadGridCertificate.MasterTableView.Columns.FindByUniqueName("ValidStartDate").OrderIndex].Text=string.Format("{0}-{1}"
//                //    ,Convert.ToDateTime(RadGridCertificate.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ValidStartDate"]).ToString("yyyy.MM.dd")
//                //    , Convert.ToDateTime(RadGridCertificate.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ValidEndDate"]).ToString("yyyy.MM.dd"));
                                          
//                //打印状态
//                if (RadGridCertificate.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PrintMan"] == null
//                    || RadGridCertificate.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PrintMan"].ToString() == "")

//                    e.Item.Cells[RadGridCertificate.MasterTableView.Columns.FindByUniqueName("PrintStatus").OrderIndex].Text = "未打印";
//                else
//                    e.Item.Cells[RadGridCertificate.MasterTableView.Columns.FindByUniqueName("PrintStatus").OrderIndex].Text = "已打印";
 
//                //详细
//                e.Item.Cells[RadGridCertificate.MasterTableView.Columns.FindByUniqueName("DetailView").OrderIndex].Text = "详细";
//                e.Item.Cells[RadGridCertificate.MasterTableView.Columns.FindByUniqueName("DetailView").OrderIndex].Style.Add("cursor", "pointer");
//                e.Item.Cells[RadGridCertificate.MasterTableView.Columns.FindByUniqueName("DetailView").OrderIndex].Attributes.Add("onclick"
//                    , string.Format("location='PMCertPrint.aspx?CertificateID={0}&rtn_o={1}&rnt_t={2}';",
//                RadGridCertificate.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CertificateID"].ToString(),              
//                ViewState["PostTypeID"] == null ? "" : ViewState["PostTypeID"].ToString(),
//                ViewState["ChangeType"] == null ? "" : ViewState["ChangeType"].ToString()));
//           }
//        }


//        protected void PostSelect1_PostTypeSelectChange(object source, EventArgs e)
//        {
//            if (ViewState["PostTypeID"] != null)
//            {
//                ViewState["PostTypeID"] = PostSelect1.PostTypeID;
//            }
//        }


    }
}
