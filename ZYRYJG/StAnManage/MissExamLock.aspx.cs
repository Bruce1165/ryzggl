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

namespace ZYRYJG.StAnManage
{
    public partial class MissExamLock : BasePage
    {
        protected bool isExcelExport = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                ButtonSearch_Click(sender, e);

//                select * from [dbo].[VIEW_EXAMSCORE_NEW]
//where [WorkerCERTIFICATECODE] = '11010119850525203X' and EXAMSTARTDATE between '2017-10-26' and '2020-10-01'
//order by EXAMSTARTDATE
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
            ObjectDataSource1.SelectParameters.Clear();//清空查询参数
            QueryParamOB q = new QueryParamOB();

            if (RadTextBoxWorkerName.Text.Trim() != "") q.Add(string.Format("WorkerName like '%{0}%'", RadTextBoxWorkerName.Text.Trim()));//姓名
            if (RadTextBoxCertificateCode.Text.Trim() != "") q.Add(string.Format("WorkerCertificateCode like '%{0}%'", RadTextBoxCertificateCode.Text.Trim()));//个人证件号码

            //锁定日期
            if (RadDatePicker_ExamStartDate.SelectedDate.HasValue == true)
            {
                q.Add(string.Format("LockStartDate >= '{0}'", RadDatePicker_ExamStartDate.SelectedDate.Value));
            }
            if (RadDatePicker_ExamEndDate.SelectedDate.HasValue == true)
            {
                q.Add(string.Format("LockStartDate < '{0}'", RadDatePicker_ExamEndDate.SelectedDate.Value.AddDays(1)));
            }
            switch(RadioButtonListLockSatatus.SelectedValue)
            {
                case "锁定中":
                    q.Add(string.Format("LockEndDate > '{0}'", DateTime.Now.ToString("yyyy-MM-dd")));
                    break;
                case "已解锁":
                    q.Add(string.Format("LockEndDate <= '{0}'", DateTime.Now.ToString("yyyy-MM-dd")));
                    break;
            }
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridExamResult.CurrentPageIndex = 0;
            RadGridExamResult.DataSourceID = ObjectDataSource1.ID;    
        }       

        protected void ButtonPrint_Click(object sender, EventArgs e)
        {
            if (RadGridExamResult.MasterTableView.VirtualItemCount == 0)
            {
                UIHelp.layerAlert(Page, "没有可导出的数据！");
                return;
            }
            isExcelExport = true;
            
            RadGridExamResult.PageSize = RadGridExamResult.MasterTableView.VirtualItemCount;
            RadGridExamResult.CurrentPageIndex = 0;
            RadGridExamResult.Rebind();
            
            RadGridExamResult.ExportSettings.ExportOnlyData = true;//导出翻页所有的数据，如果省了，则下面导出的是显示页的数据
            RadGridExamResult.ExportSettings.OpenInNewWindow = true;
            RadGridExamResult.MasterTableView.ExportToExcel();
        }

        protected void RadGridExamResult_DataBound(object sender, EventArgs e)
        {
           
            if (RadGridExamResult.Items.Count > 0)
            {
                ButtonPrint.Enabled = true;

            }
            else
            {
                ButtonPrint.Enabled = false;
            }
        }


        protected void RadGridExamResult_ExcelExportCellFormatting(object source, ExcelExportCellFormattingEventArgs e)
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
