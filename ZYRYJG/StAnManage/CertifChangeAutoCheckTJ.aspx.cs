using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAccess;
using Telerik.Web.UI;

namespace ZYRYJG.StAnManage
{
    public partial class CertifChangeAutoCheckTJ : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RadDatePicker_StartDate.DbSelectedDate = Convert.ToDateTime(string.Format("{0}-01-01", DateTime.Now.Year));
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

            string begin =(RadDatePicker_StartDate.SelectedDate.HasValue?RadDatePicker_StartDate.SelectedDate.Value.ToString("yyyy-MM-dd"):"1950-01-01");
            string end =(RadDatePicker_EndDate.SelectedDate.HasValue?RadDatePicker_EndDate.SelectedDate.Value.ToString("yyyy-MM-dd 23:59:59"):"2050-01-01");
         

            //string tjType=( RadioButtonListTJType.SelectedValue=="年度"?"%Y年":"%Y年%m月");

            string sql = "";

            if (RadioButtonListTJType.SelectedValue == "年度")
            {
                sql = @"select   convert(varchar(4),[NOTICEDATE],20) + '年' TJDate,
	                             c.POSTTYPENAME,
	                             count(*) SL
                          FROM [dbo].[CERTIFICATECHANGE] ch
                          inner join certificate c on ch.CERTIFICATEID = c.CERTIFICATEID
                          where [PATCHSHEBAOCHECK] >0 and [NOTICEDATE] between '{1}' and '{2}'
                          group by  convert(varchar(4),[NOTICEDATE],20) + '年',c.POSTTYPENAME
                          order by TJDate desc,POSTTYPENAME";
            }
            else
            {
                sql = @"select replace(convert(varchar(7),[NOTICEDATE],20),'-','年') + '月' TJDate,
	                          c.POSTTYPENAME,
	                           count(*) SL
                          FROM [dbo].[CERTIFICATECHANGE] ch
                          inner join certificate c on ch.CERTIFICATEID = c.CERTIFICATEID
                          where [PATCHSHEBAOCHECK] >0 and [NOTICEDATE] between '{1}' and '{2}'
                          group by  replace(convert(varchar(7),[NOTICEDATE],20),'-','年') + '月',c.POSTTYPENAME
                          order by TJDate desc,POSTTYPENAME";
                //统计结果模版
            }
         
            //统计结果模版
            DataTable dtBase = CommonDAL.GetDataTable(string.Format(sql, "", begin,end));
         
            //计算小计、合计
            int sum =0;

            for (int i = 0; i < dtBase.Rows.Count ; i++)
            {
                sum += Convert.ToInt32(dtBase.Rows[i]["SL"]);
            }

            DataRow dr = dtBase.NewRow();
            dtBase.Rows.InsertAt(dr, 0);
            dr["TJDate"] = "合计";
            dr["SL"] = sum;

            RadGrid1.BorderWidth = System.Web.UI.WebControls.Unit.Pixel(1);
            RadGrid1.DataSource = dtBase;
            RadGrid1.DataBind();
           
        }

        //导出审批列表
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
            if (item.ItemIndex == 0 && e.FormattedColumn.UniqueName == "TJDate")
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