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
    public partial class ConnetMonitorDetail : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "ConnetMonitor.aspx";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                DateTime d = Convert.ToDateTime(Request["o"]);
                DataTable dt = CommonDAL.GetDataTable(string.Format(
@"select dtime,[MaxCount],[AvgCount]
from [TJ_Connet_Hour]
where dtime >='{0}' and dtime < '{1}'
order by dtime desc",d.ToString("yyyy-MM-dd"),d.AddDays(1).ToString("yyyy-MM-dd")));
                RadGridConnet.DataSource = dt;
                RadGridConnet.DataBind();
            }
        }

        //导出成绩
        protected void ButtonOutput_Click(object sender, EventArgs e)
        {
            if (RadGridConnet.MasterTableView.Items.Count == 0)
            {
                UIHelp.layerAlert(Page, "没有可导出的数据！");
                return;
            }
            RadGridConnet.ExportSettings.ExportOnlyData = true;
            RadGridConnet.ExportSettings.IgnorePaging = false;
            RadGridConnet.ExportSettings.OpenInNewWindow = true;
            RadGridConnet.MasterTableView.ExportToExcel();
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

    }
}