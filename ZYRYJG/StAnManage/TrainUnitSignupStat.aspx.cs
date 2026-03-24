using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using Telerik.Web.UI;
using System.Data;
using System.Collections.Specialized;

namespace ZYRYJG.StAnManage
{
    public partial class TrainUnitSignupStat : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //ButtonSearch_Click(sender, e);
            }
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            //统计结果模版
            DataTable dtBase = ExamSignUpDAL.StatTrainUnitExamSignup(RadDatePicker_StartDate.SelectedDate, RadDatePicker_EndDate.SelectedDate,PostSelect1.PostTypeID,PostSelect1.PostID);
            RadGrid1.BorderWidth =System.Web.UI.WebControls.Unit.Pixel(1);
            RadGrid1.DataSource = dtBase;
            ViewState["dtBase"] = dtBase;
            RadGrid1.DataBind();
        }

        //导出
        protected void ButtonOutput_Click(object sender, EventArgs e)
        {
            if (RadGrid1.MasterTableView.Items.Count == 0)
            {
                UIHelp.layerAlert(Page, "没有可导出的数据！");
                return;
            }

            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.OpenInNewWindow = true;
            RadGrid1.MasterTableView.ExportToExcel();
        }

        //格式化Excel
        protected void RadGrid1_ExcelExportCellFormatting(object source, ExcelExportCellFormattingEventArgs e)
        {
            //HeadCell
            GridItem item = e.Cell.Parent as GridItem;
            if (item.ItemIndex == 0 && e.FormattedColumn.UniqueName == "RowNum")
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
