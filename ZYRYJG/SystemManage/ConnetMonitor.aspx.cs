using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAccess;
using Model;
using Telerik.Web.UI;

namespace ZYRYJG.SystemManage
{
    public partial class ConnetMonitor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ButtonSearch_Click(sender, e);
            }
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ObjectDataSource1.SelectParameters.Clear();

            QueryParamOB q = new QueryParamOB();

       

            if (datePickerFrom.SelectedDate.HasValue)
            {
                q.Add(string.Format(" dtime>='{0}'", datePickerFrom.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }
            if (datePickerEnd.SelectedDate.HasValue)
            {
                q.Add(string.Format("dtime<'{0}'", datePickerEnd.SelectedDate.Value.AddDays(1).ToString("yyyy-MM-dd")));
            }

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridConnet.CurrentPageIndex = 0;
            RadGridConnet.DataSourceID = ObjectDataSource1.ID;
        }

        protected void RadGridConnet_ExcelExportCellFormatting(object source, Telerik.Web.UI.ExcelExportCellFormattingEventArgs e)
        {
            //HeadCell
            GridItem item = e.Cell.Parent as GridItem;
            if (item.ItemIndex == 0 && e.FormattedColumn.UniqueName == "dtime")
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

        protected void ButtonOutput_Click(object sender, EventArgs e)
        {
            if (RadGridConnet.MasterTableView.Items.Count == 0)
            {
                UIHelp.layerAlert(Page, "没有可导出的数据！");
                return;
            }
            RadGridConnet.MasterTableView.Columns.FindByUniqueName("Detail").Visible = false;
            RadGridConnet.ExportSettings.ExportOnlyData = true;
            RadGridConnet.ExportSettings.IgnorePaging = false;
            RadGridConnet.ExportSettings.OpenInNewWindow = true;
            RadGridConnet.MasterTableView.ExportToExcel();
        }
    }
}