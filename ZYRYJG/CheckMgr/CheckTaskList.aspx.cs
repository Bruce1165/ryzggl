using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Telerik.Web.UI;
using DataAccess;
using System.Data;

namespace ZYRYJG.CheckMgr
{
    public partial class CheckTaskList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                for (int i = 2024; i <= (DateTime.Now.Year ); i++)
                {
                    RadComboBoxYear.Items.Insert(0, new RadComboBoxItem(i.ToString(), i.ToString()));
                }
                RadComboBoxYear.FindItemByValue(DateTime.Now.Year.ToString()).Selected = true;

                ButtonFind_Click(sender, e);
            }
        }

        protected void ButtonFind_Click(object sender, EventArgs e)
        {
            ObjectDataSourceCheckFeedBack.SelectParameters.Clear();
            QueryParamOB q = new QueryParamOB();
            q.Add(string.Format("Unit like '{0}%'", UserName));
            q.Add("[DataStatusCode] > 0");
            if (RadTextBoxValue.Text.Trim() != "")//自定义查询项
            {
                q.Add(string.Format("{0} like '%{1}%'", RadComboBoxItem.SelectedValue, RadTextBoxValue.Text.Trim()));
            }

            //发布时间          
            if (RadComboBoxMonth.SelectedValue != "")
            {
                string startD = string.Format("{0}-{1}-01"
                    , (RadComboBoxYear.SelectedValue != "" ? RadComboBoxYear.SelectedValue : DateTime.Now.Year.ToString())
                    , RadComboBoxMonth.SelectedValue);

                string endD = Convert.ToDateTime(startD).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss");

                q.Add(string.Format("([PublishiTime] between '{0}' and '{1}')", startD, endD));//月
            }
            else if (RadComboBoxYear.SelectedValue != "")
            {
                string startD = string.Format("{0}-01-01", RadComboBoxYear.SelectedValue);

                string endD = Convert.ToDateTime(startD).AddYears(1).AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss");

                q.Add(string.Format("([PublishiTime] between '{0}' and '{1}')", startD, endD));//月
            }

            try
            {
                ObjectDataSourceCheckFeedBack.SelectParameters.Add("filterWhereString", q.ToWhereString());
                RadGridCheckFeedBack.CurrentPageIndex = 0;
                RadGridCheckFeedBack.DataSourceID = ObjectDataSourceCheckFeedBack.ID;

            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "获取监管记录失败！", ex);
                return;
            }
        }      

        //导出
        protected void ButtonOut_Click(object sender, EventArgs e)
        {
            if (RadGridCheckFeedBack.MasterTableView.VirtualItemCount == 0)
            {
                UIHelp.layerAlert(Page, "没有可导出的数据！");
                return;
            }


            RadGridCheckFeedBack.PageSize = RadGridCheckFeedBack.MasterTableView.VirtualItemCount;
            RadGridCheckFeedBack.CurrentPageIndex = 0;
            RadGridCheckFeedBack.Rebind();
            RadGridCheckFeedBack.ExportSettings.ExportOnlyData = true;
            RadGridCheckFeedBack.ExportSettings.OpenInNewWindow = true;
            RadGridCheckFeedBack.MasterTableView.ExportToExcel();
        }

        protected void RadGridCheckFeedBack_ExcelExportCellFormatting(object source, ExcelExportCellFormattingEventArgs e)
        {
            switch (e.FormattedColumn.UniqueName)
            {
                case "CertificateCode": e.Cell.Style["mso-number-format"] = @"\@"; break;
                case "WorkerCertificateCode": e.Cell.Style["mso-number-format"] = @"\@"; break;
            }
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