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

namespace ZYRYJG.RenewCertifates
{
    public partial class CertifStatusList : BasePage
    {
        protected bool isExcelExport = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                switch (string.IsNullOrEmpty(Request["o"]) ? "1" : Request["o"].ToString())
                {
                    case "1":
                        LabelCheck.Text = "三类人员";
                        break;
                    case "2":
                        LabelCheck.Text = "特种作业";
                        break;
                    case "3":
                        LabelCheck.Text = "造价员";
                        break;
                }
                Link_DelHistory.HRef = "DelHistory.aspx?o=" + Request["o"];
                rbl.SelectedValue = "0";
                PostSelect1.PostTypeID = string.IsNullOrEmpty(Request["o"]) ? "1" : Request["o"].ToString();
                PostSelect1.LockPostTypeID();
                ButtonSearch_Click(sender, e);
            }
        }
        //根据输入的条件查询
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ObjectDataSource1.SelectParameters.Clear();
            QueryParamOB q = new QueryParamOB();
            if (RadTextBoxCertificateCode.Text.Trim() != "")//证件编号
            {
                q.Add(string.Format("CERTIFICATECODE like '%{0}%'", RadTextBoxCertificateCode.Text.Trim()));
            }
            if (RadTextBoxWorkerName.Text.Trim() != "")//姓名
            {
                q.Add(string.Format("WorkerName like '%{0}%'", RadTextBoxWorkerName.Text.Trim()));
            }
            if (PostSelect1.PostTypeID != "")//岗位
            {
                q.Add(string.Format("PostTypeID = {0}", PostSelect1.PostTypeID));
            }
            //工种类别
            if (PostSelect1.PostID != "")
            {
                switch (PostSelect1.PostID)
                {
                    case "9"://土建
                        q.Add(string.Format("(PostID = {0} or PostName like '%增土建')", PostSelect1.PostID));
                        break;
                    case "12"://安装
                        q.Add(string.Format("(PostID = {0} or PostName like '%增安装')", PostSelect1.PostID));
                        break;
                    default:
                        q.Add(string.Format("PostID = {0}", PostSelect1.PostID));
                        break;
                }
            }
            if (RadTextBoxWorkerCertificateCode.Text.Trim() != "")
            {
                q.Add(string.Format("WorkerCertificateCode like '%{0}%'", RadTextBoxWorkerCertificateCode.Text.Trim()));
            }
            if (RadTextBoxUnitName.Text.Trim() != "")//企业名称
            {
                q.Add(string.Format("UnitName like '%{0}%'", RadTextBoxUnitName.Text.Trim()));
            }
            if (RadTextBoxUnitCode.Text.Trim() != "")//组织机构代码
            {
                q.Add(string.Format("UnitCode like '%{0}%'", RadTextBoxUnitCode.Text.Trim()));
            }
            //if (RadTextBoxApplyCode.Text.Trim() != "")   //申请编号
            //{
            //    q.Add(string.Format("ApplyCode like '%{0}%'", RadTextBoxApplyCode.Text.Trim()));
            //}

            //初审单位
            if (RadTextBoxFirstCheckUnitName.Text.Trim() != "") q.Add(string.Format("FirstCheckUnitName like '%{0}%'", RadTextBoxFirstCheckUnitName.Text.Trim()));

            //发证年度
            if (RadNumericTextBoxConferData.Value.HasValue == true)
            {
                //q.Add(string.Format("DATEPART(year,conferdate)={0}", RadNumericTextBoxConferData.Value));
                q.Add(string.Format("conferdate between '{0}-01-01' and '{0}-12-31'", RadNumericTextBoxConferData.Value));
            }
            //证书有效期
            if (!RadDatePickerFrom.IsEmpty)
            {
                q.Add(string.Format("ValidEndDate >='{0}'", RadDatePickerFrom.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }
            if (!RadDatePickerEnd.IsEmpty)
            {
                q.Add(string.Format("ValidEndDate <='{0} 23:59:59'", RadDatePickerEnd.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }
           
            if (rbl.SelectedValue !="") q.Add(string.Format("Status ='{0}'", rbl.SelectedValue));
            switch (RadioButtonListResult.SelectedValue)
            {
                case "不予受理":
                    q.Add("GetResult ='不予受理'");
                    break;
                case "决定通过":
                    q.Add("ConfirmResult ='决定通过'");
                    break;
                case "决定不通过":
                    q.Add("ConfirmResult ='决定不通过'");
                    break;
            }

            if (RadioButtonListjxjyway.SelectedValue != "")//继续教育形式
            {
                q.Add(string.Format("jxjyway={0}", RadioButtonListjxjyway.SelectedValue));
            }
               
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridCheck.CurrentPageIndex = 0;
            RadGridCheck.DataSourceID = ObjectDataSource1.ID;
        }
 
        //打印申请
        protected void ButtonApplyPrint_Click(object sender, EventArgs e)
        {
            DataTable dt = CertificateContinueDAL.GetList(0, int.MaxValue - 1, ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue + string.Format(" and Status='{0}'", EnumManager.CertificateContinueStatus.Applyed), "CertificateContinueID");
            string ApplyCode = dt.Rows[0]["ApplyCode"].ToString();
            CheckSaveDirectory();
            Utility.WordDelHelp.CreateXMLWordWithDot(this.Page, "~/Template/证书续期申请表.doc"
                , string.Format("~/UpLoad/CertifApply/{0}/证书续期申请表_{1}.doc", PersonID.ToString(), ApplyCode)
                , GetPrintData(ApplyCode));
            //ClientScript.RegisterStartupScript(Page.GetType(), "printword", string.Format("Print('{2}/UpLoad/CertifApply/{0}/证书续期申请表_{1}.doc');",  PersonID.ToString(), ApplyCode, RootUrl), true);
            //Utility.WordDelHelp.ExportWord(this.Page, Server.MapPath(string.Format("~/UpLoad/CertifApply/{0}/证书续期申请表_{1}.doc", PersonID.ToString(), ApplyCode)));

            List<ResultUrl> url = new List<ResultUrl>();
            url.Add(new ResultUrl("证书续期申请表", string.Format("~/UpLoad/CertifApply/{0}/证书续期申请表_{1}.doc", PersonID, ApplyCode)));
            UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);
        }
        //检查文件保存路径
        protected void CheckSaveDirectory()
        {
            //证书申请书存放路径(~/UpLoad/CertifApply/考试计划ID/照片阵列_考场编号.doc)
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/CertifApply/"))) Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/CertifApply/"));
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/CertifApply/" + PersonID))) Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/CertifApply/" + PersonID));
        }
        //获取打印数据（按ListView分页导出Word）
        protected List<Dictionary<string, string>> GetPrintData(string ApplyCode)
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            Dictionary<string, string> printData = null;
            DataTable dt = CertificateContinueDAL.GetList(0, int.MaxValue - 1, ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, "CertificateContinueID");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CertificateOB cfob = CertificateDAL.GetObject(Convert.ToInt64(dt.Rows[i]["CertificateID"]));
                WorkerOB wob = WorkerDAL.GetObject(cfob.WorkerID.Value);
                UnitMDL uob = UnitDAL.GetObjectByENT_OrganizationsCode(dt.Rows[i]["UnitCode"].ToString());
                printData = new Dictionary<string, string>();
                printData.Add("ApplyCode", ApplyCode);
                printData.Add("WorkerName", wob.WorkerName);
                printData.Add("Sex", wob.Sex);
                printData.Add("Age", wob.Birthday.Value.ToString("yyyy-MM-dd"));
                printData.Add("CertificateCode", wob.CertificateCode);
                printData.Add("Phone", wob.Phone);
                printData.Add("PostID", string.Format("{0}({1})", PostInfoDAL.GetObject(cfob.PostTypeID.Value).PostName, PostInfoDAL.GetObject(cfob.PostID.Value).PostName));
                printData.Add("CulturalLevel", wob.CulturalLevel);
                printData.Add("UnitName", uob.ENT_Name);
                printData.Add("UnitCode", uob.ENT_OrganizationsCode);
                list.Add(printData);
            }
            return list;
        }
        //导出
        protected void ButtonExport_Click(object sender, EventArgs e)
        {
            if (RadGridCheck.MasterTableView.VirtualItemCount == 0)
            {
                UIHelp.layerAlert(Page, "没有可导出的数据！");
                return;
            }
            isExcelExport = true;

            RadGridCheck.PageSize = RadGridCheck.MasterTableView.VirtualItemCount;//
            RadGridCheck.CurrentPageIndex = 0;
            RadGridCheck.Rebind();
            RadGridCheck.ExportSettings.ExportOnlyData = true;
            RadGridCheck.ExportSettings.OpenInNewWindow = true;
            RadGridCheck.MasterTableView.ExportToExcel();
            RadGridCheck.MasterTableView.HeaderStyle.BackColor = System.Drawing.Color.FromName("#DEDEDE");
        }
        //格式化Excel
        protected void RadGridCheck_ExcelExportCellFormatting(object source, ExcelExportCellFormattingEventArgs e)
        {
            switch (e.FormattedColumn.UniqueName)
            {
                case "WorkerCertificateCode": e.Cell.Style["mso-number-format"] = @"\@"; break;

            }
            e.Cell.Attributes.Add("align", "center");
            e.Cell.Style.Add("border-width", "0.1pt");
            e.Cell.Style.Add("border-style", "solid");
            e.Cell.Style.Add("border-color", "#CCCCCC");
            if (isExcelExport && e.Cell.Parent is GridHeaderItem)
            {
                GridHeaderItem head = e.Cell.Parent as GridHeaderItem;
                Table tb = e.Cell.Parent.Parent as Table;
                foreach (TableCell cell in head.Cells)
                {
                    e.Cell.Style.Add("border-width", "0.1pt");
                    e.Cell.Style.Add("border-style", "solid");
                    e.Cell.Style.Add("border-color", "#CCCCCC");
                    e.Cell.Style.Add("background-color", "#DEDEDE");
                }
            }

        }
    }
}
