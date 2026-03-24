using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using Telerik.Web.UI;
using System.Data;
using Utility;
using System.IO;
using System.Text.RegularExpressions;

namespace ZYRYJG.EXamManage
{
    public partial class CertificateAddList : BasePage
    {

        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "CertificateAdd.aspx";
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
            q.Add("Remark = '证书补登记'");//补登记
            //姓名
            if (RadTextBoxWorkerName.Text.Trim() != "")
            {
                q.Add(string.Format("WorkerName like '%{0}%'", RadTextBoxWorkerName.Text.Trim()));
            }
            //证件号码
            if (RadTextBoxWorkerCertificateCode.Text.Trim() != "")
            {
                q.Add(string.Format("WorkerCertificateCode like '%{0}%'", RadTextBoxWorkerCertificateCode.Text.Trim()));
            }
            //证书编号
            if (RadTxtCertificateCode.Text.Trim() != "")
            {
                q.Add(string.Format("CertificateCode like '%{0}%'", RadTxtCertificateCode.Text.Trim()));
            }
            if (PostSelect1.PostTypeID !="")//岗位类别
            {
                q.Add("PostTypeID=" + PostSelect1.PostTypeID);
            }
            if (PostSelect1.PostID != "")//岗位工种
            {
                q.Add("PostID=" + PostSelect1.PostID);
            }
            //创建人
            if (RadTextBoxApplyMan.Text.Trim() != "")
            {
                q.Add(string.Format("ApplyMan like '%{0}%'", RadTextBoxApplyMan.Text.Trim()));
            }
            //创建时间
            if (RadDatePicker_StartDate.SelectedDate.HasValue) 
            {
                q.Add(string.Format("CreateTime >= '{0}'" , RadDatePicker_StartDate.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }
            if (RadDatePicker_EndDate.SelectedDate.HasValue)
            {
                q.Add(string.Format("CreateTime < '{0}'", RadDatePicker_EndDate.SelectedDate.Value.AddDays(1).ToString("yyyy-MM-dd")));
            }
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());


            RadGrid1.MasterTableView.SortExpressions.Clear();

            GridSortExpression sortStr1 = new GridSortExpression();
            sortStr1.FieldName = "CreateTime";
            sortStr1.SortOrder = GridSortOrder.Descending;
            RadGrid1.MasterTableView.SortExpressions.AddSortExpression(sortStr1);

            RadGrid1.CurrentPageIndex = 0;
            RadGrid1.DataSourceID = ObjectDataSource1.ID;
        }

        //导出成绩
        protected void ButtonOutput_Click(object sender, EventArgs e)
        {
            if (RadGrid1.MasterTableView.Items.Count == 0)
            {
                UIHelp.layerAlert(Page, "没有可导出的数据！");
                return;
            }
            RadGrid1.PageSize = RadGrid1.MasterTableView.VirtualItemCount;//
            RadGrid1.CurrentPageIndex = 0;
            RadGrid1.Rebind();

            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.IgnorePaging = false;
            RadGrid1.ExportSettings.OpenInNewWindow = true;
            RadGrid1.MasterTableView.ExportToExcel();
        }

        //格式化Excel
        protected void RadGrid1_ExcelExportCellFormatting(object source, ExcelExportCellFormattingEventArgs e)
        {
            switch (e.FormattedColumn.UniqueName)
            {
                case "WorkerCertificateCode": e.Cell.Style["mso-number-format"] = @"\@"; break;
                case "CertificateCode": e.Cell.Style["mso-number-format"] = @"\@"; break;
            }
            //HeadCell
            GridItem item = e.Cell.Parent as GridItem;
            if (item.ItemIndex == 0 && e.FormattedColumn.UniqueName == "WorkerName")
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
