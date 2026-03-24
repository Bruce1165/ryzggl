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
    public partial class ExamSignTj : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
               
            }
        }

        //查询
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if(ExamPlanSelect1.ExamPlanID.HasValue==false)
            {
                UIHelp.layerAlert(Page, "请选择一个考试计划！");
                return;
            }

            string sql = @"SELECT 
                            S_TRAINUNITNAME,CHECKDATEPLAN
                            ,count(*) ManCount
                            ,sum(case when FIRSTCHECKTYPE <1 or FIRSTCHECKTYPE is null then 1 else 0 end) NeedManCheckCount
                            ,count(FIRSTTRIALTIME) CheckedCount
                            ,sum(case when FIRSTTRIALTIME is not null and (FIRSTCHECKTYPE <1 or FIRSTCHECKTYPE is null) then 1 else 0 end) ManCheckCount
                            ,sum(case when FIRSTCHECKTYPE >0 then 1 else 0 end) SystemCheckCount
                            ,sum(case FIRSTCHECKTYPE when 1 then 1 else 0 end) ZJSCheckCount
                            ,sum(case FIRSTCHECKTYPE when 2 then 1 else 0 end) ExamCheckCount
                            ,sum(case FIRSTCHECKTYPE when 3 then 1 else 0 end) SheBaoCheckCount
                            ,sum(case FIRSTCHECKTYPE when 4 then 1 else 0 end) FRCheckCount
                            ,count(CHECKDATE) JWCheckCount
                            ,count(PAYCONFIRMDATE) PayCount
                        FROM DBO.EXAMSIGNUP
                        where EXAMPLANID={0}
                        group by S_TRAINUNITNAME,CHECKDATEPLAN ";

            DataTable dt = CommonDAL.GetDataTable(string.Format(sql, ExamPlanSelect1.ExamPlanID));

            RadGrid1.DataSource = dt;
            RadGrid1.DataBind();
        }
              
        //导出excel
        protected void ButtonExport_Click(object sender, EventArgs e)
        {
            //if (RadGrid1.MasterTableView.VirtualItemCount == 0)
            //{
            //    UIHelp.layerAlert(Page, "没有可导出的数据！");
            //    return;
            //}

            //RadGrid1.PageSize = RadGrid1.MasterTableView.VirtualItemCount;//
            //RadGrid1.CurrentPageIndex = 0;
            //RadGrid1.Rebind();
            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.OpenInNewWindow = true;
            RadGrid1.MasterTableView.ExportToExcel();
        }



        //格式化Excel
        protected void RadGrid1_ExcelExportCellFormatting(object source, ExcelExportCellFormattingEventArgs e)
        {
           
            //HeadCell
            GridItem item = e.Cell.Parent as GridItem;
            if (item.ItemIndex == 0 && e.FormattedColumn.UniqueName == "S_TRAINUNITNAME")
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
            e.Cell.Attributes.Add("align", "left");
            e.Cell.Style.Add("border-width", "0.1pt");
            e.Cell.Style.Add("border-style", "solid");
            e.Cell.Style.Add("border-color", "#CCCCCC");
        }
    }
}
