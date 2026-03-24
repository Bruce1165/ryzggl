using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using DataAccess;
using Model;


namespace ZYRYJG.StAnManage
{
    public partial class TjMessageSend : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RadDatePicker_StartDate.DbSelectedDate = DateTime.Now.ToString("yyyy-01-01");
                ButtonSearch_Click(sender, e);
            }
        }

        //导出申请列表excel
        protected void ButtonExportToExcel_Click(object sender, EventArgs e)
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
            RadGrid1.MasterTableView.HeaderStyle.BackColor = System.Drawing.Color.FromName("#DEDEDE");
        }

        //格式化Excel
        protected void RadGrid1_ExcelExportCellFormatting(object source, ExcelExportCellFormattingEventArgs e)
        {
            //switch (e.FormattedColumn.UniqueName)
            //{
            //    case "CertificateCode":
            //    case "WorkerCertificateCode":
            //    case "UnitCode":
            //        e.Cell.Style["mso-number-format"] = @"\@";
            //        break;
            //}

            //HeadCell
            GridItem item = e.Cell.Parent as GridItem;
            if (item.ItemIndex == 0 && e.FormattedColumn.UniqueName == "lsgx")
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

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }

            string sql = @"
                        select '二级建造师注册审批未通过' 业务类型,count(*) 数量
                        from [192.168.7.50].[PlatInfo].[dbo].[t_dx_fsnr] 
                        where [sjlx]=43 and [dxnr] like '%二级建造师%不通过%' {0}
                        union
                        select '二级建造师过期预警' 业务类型,count(*) 数量
                        from [192.168.7.50].[PlatInfo].[dbo].[t_dx_fsnr] 
                        where [sjlx]=43 and [dxnr] like '%二级建造师注册有效期即将届满%' {0}
                        union
                        select '考试通知' 业务类型,count(*) 数量
                        from [192.168.7.50].[PlatInfo].[dbo].[t_dx_fsnr] 
                        where [sjlx]=43 and [dxnr] like '%考试%' {0}";

            //                string sql = @"
            //                        select '延续注册' 业务类型,count(*) 数量
            //from [PlatInfo].[dbo].[t_dx_fsnr] 
            //where [sjlx]=43 and [dxnr] like '%延续注册%'
            //union
            //select '二建注册申诉' 业务类型,count(*) 数量
            //from [PlatInfo].[dbo].[t_dx_fsnr] 
            //where [sjlx]=43 and [dxnr] like '%申诉%'";

            QueryParamOB q = new QueryParamOB();
            if (RadDatePicker_StartDate.SelectedDate.HasValue)//时间段起始
            {
                q.Add(string.Format("[addtime]>='{0}'", RadDatePicker_StartDate.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }
            if (RadDatePicker_EndDate.SelectedDate.HasValue)//时间段截止
            {
                q.Add(string.Format("[addtime]<'{0}'", RadDatePicker_EndDate.SelectedDate.Value.AddDays(1).ToString("yyyy-MM-dd")));
            }

            //已发送短信类型统计
            DataTable dt = CommonDAL.GetDataTable(string.Format(sql, q.ToWhereString()));          
            RadGrid1.DataSource = dt;
            RadGrid1.DataBind();

            TJSendInfoCount();
        }

        //短信发送状态统计
        protected void TJSendInfoCount()
        {
            string sql = @"
                        select case fszt when 0 then '待发送' else '已发送' end 发送状态,count(*) 数量,[dxnr] 内容 ,min([Addtime]) SendStart,max([fssj]) SendEnd
                        from [192.168.7.50].[PlatInfo].[dbo].[t_dx_fsnr] d 
                        where  d.[Addtime]  between '{0}' and '{1}' and d.[sjlx]=43
                        group by fszt ,[dxnr]
                        order by SendStart,[dxnr],fszt";


            DataTable dt = CommonDAL.GetDataTable(string.Format(sql
                , RadDatePicker_StartDate.SelectedDate.HasValue==true? RadDatePicker_StartDate.SelectedDate.Value.ToString("yyyy-MM-dd"):"2000-01-01"
                  , RadDatePicker_EndDate.SelectedDate.HasValue == true ? RadDatePicker_EndDate.SelectedDate.Value.ToString("yyyy-MM-dd 23:59:59") : "2099-01-01"
                ));

            RadGridSendCount.DataSource = dt;
            RadGridSendCount.DataBind();
        }
    }
}