using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class ExamSignupDelHistory : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "ExamSignSearch.aspx";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
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
            ObjectDataSource1.SelectParameters.Clear();
            QueryParamOB q = new QueryParamOB();

            if (ExamPlanSelect1.ExamPlanID.HasValue)
            {
                q.Add("ExamPlanID=" + ExamPlanSelect1.ExamPlanID.ToString());
            }

            //培训点名称
            if (RadTextBoxTrainUnitID.Text != "")
            {
                q.Add(string.Format("TrainUnitID in (select UserID from dbo.[USER] where RelUserName like '%{0}%' )", RadTextBoxTrainUnitID.Text.Trim()));
            }
            // 单位名称
            if (RadTxtUnitName.Text != "")
            {
                q.Add(string.Format("UnitName like '%{0}%'", RadTxtUnitName.Text.Trim()));
            }
            //姓名
            if (RadTxtWorkerName.Text != "")
            {
                q.Add(string.Format("WorkerName like '%{0}%'", RadTxtWorkerName.Text.Trim()));
            }
            //证件号码
            if (RadTxtCertificateCode.Text != "")
            {
                q.Add(string.Format("CertificateCode like '%{0}%'", RadTxtCertificateCode.Text.Trim()));
            }
            //报名时间
             if (RadDatePicker_SignUpStartDate.SelectedDate.HasValue || RadDatePicker_SignUpEndDate.SelectedDate.HasValue)
            {
                q.Add(string.Format("(SignUpDate BETWEEN  '{0}' AND '{1}')"
                    , RadDatePicker_SignUpStartDate.SelectedDate.HasValue ? RadDatePicker_SignUpStartDate.SelectedDate.Value.ToString() : DateTime.MinValue.AddDays(1).ToString()
                    , RadDatePicker_SignUpEndDate.SelectedDate.HasValue ? RadDatePicker_SignUpEndDate.SelectedDate.Value.AddDays(1).AddMinutes(-1).ToString() : DateTime.MaxValue.AddDays(-1).ToString()));
            }     

            //报名状态
            if (RadComboBoxStatus.SelectedValue != "")
            {
                q.Add(string.Format("Status ='{0}'", RadComboBoxStatus.SelectedItem.Text));
            }
           

            if (RadTextBoxSignUpCode.Text.Trim() != "")
            {
                q.Add(string.Format("SignUpCode='{0}'", RadTextBoxSignUpCode.Text.Trim()));
            }
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGrid1.CurrentPageIndex = 0;
            RadGrid1.DataSourceID = ObjectDataSource1.ID;
        }
              
        //检查文件保存路径
        protected void CheckSaveDirectory(string ExamPlanID)
        {
            //照片阵列存放路径(~/UpLoad/ExamerPhotoList/考试计划ID/照片阵列_考场编号.doc)
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/PaySign/"))) Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/PaySign/"));
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/PaySign/" + ExamPlanID))) Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/PaySign/" + ExamPlanID));
        }

        //导出excel
        protected void ButtonExport_Click(object sender, EventArgs e)
        {
            if (RadGrid1.MasterTableView.VirtualItemCount == 0)
            {
                UIHelp.layerAlert(Page, "没有可导出的数据！");
                return;
            }
            RadGrid1.MasterTableView.Columns.FindByUniqueName("RowNum").Visible = false;
            RadGrid1.MasterTableView.Columns.FindByUniqueName("column").Visible = false;
            RadGrid1.MasterTableView.Columns.FindByUniqueName("CheckMan").Visible = false;
            RadGrid1.MasterTableView.Columns.FindByUniqueName("CheckDate").Visible = false;
            RadGrid1.PageSize = RadGrid1.MasterTableView.VirtualItemCount;//
            RadGrid1.CurrentPageIndex = 0;
            RadGrid1.Rebind();
            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.OpenInNewWindow = true;
            RadGrid1.MasterTableView.ExportToExcel();
        }

        //格式化Excel
        protected void RadGrid1_ExcelExportCellFormatting(object source, ExcelExportCellFormattingEventArgs e)
        {
            switch (e.FormattedColumn.UniqueName)
            {
                case "CertificateCode": e.Cell.Style["mso-number-format"] = @"\@"; break;
            }
            //HeadCell
            GridItem item = e.Cell.Parent as GridItem;
            if (item.ItemIndex == 0 && e.FormattedColumn.UniqueName == "PostTypeName")
            {
                GridTableView gtv = e.Cell.Parent.Parent.Parent as GridTableView;
                GridItem ghi = gtv.GetItems(GridItemType.Header)[0];
                for (int i = 0; i < ghi.Cells.Count; i++)
                {
                    ghi.Cells[i].Style.Add("border-width", "0.1pt");
                    ghi.Cells[i].Style.Add("border-style", "solid");
                    ghi.Cells[i].Style.Add("border-color", "#CCCCCC");
                }
            }
            //Itemcell
            e.Cell.Attributes.Add("align", "center");
            e.Cell.Style.Add("border-width", "0.1pt");
            e.Cell.Style.Add("border-style", "solid");
            e.Cell.Style.Add("border-color", "#CCCCCC");
        }
    }
}
