using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using DataAccess;
using Telerik.Web.UI;
using System.Text;

namespace ZYRYJG.StAnManage
{
    public partial class CertifCAReport : BasePage
    {

        string[] Gridhead = new string[] { "安全生产考核三类人员", "建设职业技能岗位", "关键岗位专业技术管理人员", "建筑施工特种作业" };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRadGrid1();

                RadNumericTextBoxYear.Value = DateTime.Now.Year;
                BindECertCreateByMonth(DateTime.Now.Year);
                BindECertDownByMonth(DateTime.Now.Year);
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "BindECertCreateByMonth", "var o1=\"" + ViewState["ECertCreateByMonth"].ToString() + "\";var g1=\"" + ViewState["ECertCreateByMonthLegend"].ToString() + "\";var o2=\"" + ViewState["ECertDownByMonth"].ToString() + "\";var g2=\"" + ViewState["ECertDownByMonthLegend"].ToString() + "\";BindECertCreateByMonth(o1,g1,o2,g2);", true);
            }
        }

        protected void BindRadGrid1()
        {
            string sql = @"select 
	                            c.PostTypeid,max(c.posttypename) posttypename,count(*) CerCount,
	                            sum(case when c.[CertificateCAID] is null then 0 else 1 end) ECerCount,
	                            sum(case when d.downCount is null then 0 else 1 end) downCount
                            FROM [dbo].[CERTIFICATE] c
                            left join 
                            (
	                            select c.CertificateID,count(*) as downCount from [dbo].[FileDownLog] d
	                            inner  join [dbo].[CertificateCAHistory] c on d.FileID = c.CertificateCAID
									group by c.CertificateID
                            ) d on c.CERTIFICATEID = d.CertificateID
                            where (c.[STATUS]='首次' or c.[STATUS]='进京变更' or c.[STATUS]='补办' or c.[STATUS]='京内变更' or c.[STATUS]='续期') 
							and c.VALIDENDDATE > dateadd(day,-1,getdate())
                            group by PostTypeid
                            ";
            

            //统计结果模版
            DataTable dtBase = CommonDAL.GetDataTable(sql);
            DataRow drNew = dtBase.NewRow();
            drNew["PostTypeid"] = 0;
            drNew["posttypename"] = "";
            drNew["CerCount"] = 0;
            drNew["ECerCount"] = 0;
            drNew["downCount"] = 0;
        
            foreach(DataRow dr in dtBase.Rows)
            {
                drNew["CerCount"] = Convert.ToInt32(drNew["CerCount"]) + Convert.ToInt32(dr["CerCount"]);
                drNew["ECerCount"] = Convert.ToInt32(drNew["ECerCount"]) + Convert.ToInt32(dr["ECerCount"]);
                drNew["downCount"] = Convert.ToInt32(drNew["downCount"]) + Convert.ToInt32(dr["downCount"]);
            }
            dtBase.Rows.Add(drNew);


            RadGrid1.BorderWidth = System.Web.UI.WebControls.Unit.Pixel(1);
            RadGrid1.DataSource = dtBase;
             RadGrid1.DataBind();

        }

//        private void BindReportByMonth(DateTime DateFrom,DateTime DataTo)
//        {
//            string sql = @"
//                         select t1.posttypename,t1.DMonth,t1.CerCount,t2.DownCount
//                         from
//                         (
//                             select  c.posttypeid,convert(varchar(7),h.ReturnCATime,21) as DMonth ,
//                             count(*) as CerCount,
//                             max(c.posttypename) posttypename
//                             from [dbo].[CertificateCAHistory] h
//                             inner join [dbo].[CERTIFICATE] c
//                              on h.CertificateID = c.CERTIFICATEID
//                              where h.ReturnCATime between '{0}' and '{1}'
//                             group by c.posttypeid,convert(varchar(7),h.ReturnCATime,21)
//
//                         ) t1
//                          left join 
//                        (
//	                        select 
//	                         c.posttypeid,convert(varchar(7),d.DownTime,21) as DMonth , count(*) as DownCount
//	                        from [dbo].[FileDownLog] d
//	                        inner  join [dbo].[CertificateCAHistory] h on d.FileID = h.CertificateCAID
//	                        inner join [dbo].[CERTIFICATE] c on h.CertificateID = c.CertificateID
//	                         where d.DownTime between '{0}' and '{1}'
//	                         group by c.posttypeid,convert(varchar(7),d.DownTime,21)
//                        ) t2 on t1.PostTypeid = t2.PostTypeid and t1.Dmonth = t2.Dmonth 
//                          order by  t1.posttypeid,t1.DMonth desc
//                        ";

//            //统计结果模版
//            DataTable dtBase = CommonDAL.GetDataTable(string.Format(sql,DateFrom.ToString("yyyy-MM-dd"), DataTo.ToString("yyyy-MM-dd 23:59:59")));
//            DataRow drNew = dtBase.NewRow();
//            drNew["posttypename"] = "合计";
//            drNew["DMonth"] = "";
//            drNew["CerCount"] = 0;       
//            drNew["downCount"] = 0;

//            foreach (DataRow dr in dtBase.Rows)
//            {
//                drNew["CerCount"] = Convert.ToInt32(drNew["CerCount"]) + Convert.ToInt32(dr["CerCount"]);
//                drNew["downCount"] = Convert.ToInt32(drNew["downCount"]) + Convert.ToInt32(dr["downCount"]);
//            }
//            dtBase.Rows.Add(drNew);


//            RadGridDataByMonth.BorderWidth = Unit.Pixel(1);
//            RadGridDataByMonth.DataSource = dtBase;
//            RadGridDataByMonth.DataBind();
//        }

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


        //protected void RadGridDataByMonth_ExcelExportCellFormatting(object source, ExcelExportCellFormattingEventArgs e)
        //{
        //    //HeadCell
        //    GridItem item = e.Cell.Parent as GridItem;
        //    if (item.ItemIndex == 0 && e.FormattedColumn.UniqueName == "PostTypeName")
        //    {
        //        GridTableView gtv = e.Cell.Parent.Parent.Parent as GridTableView;
        //        GridItem ghi = gtv.GetItems(GridItemType.Header)[0];
        //        for (int i = 0; i < ghi.Cells.Count; i++)
        //        {
        //            ghi.Cells[i].Style.Add("border-width", "0.1pt");
        //            ghi.Cells[i].Style.Add("border-style", "solid");
        //            ghi.Cells[i].Style.Add("border-color", "#CCCCCC");
        //        }
        //    }
        //    //Itemcell
        //    e.Cell.Attributes.Add("align", "center");
        //    e.Cell.Style.Add("border-width", "0.1pt");
        //    e.Cell.Style.Add("border-style", "solid");
        //    e.Cell.Style.Add("border-color", "#CCCCCC");
        //}

        /// <summary>
        /// 绑定年度按月统计电子证书生成数量
        /// </summary>
        /// <param name="year">统计年份</param>
        private void BindECertCreateByMonth(int year)
        {
            DataTable dt = null;
            System.Text.StringBuilder sb = new StringBuilder();
            string sql = "";
            StringBuilder sbItem = new StringBuilder();
            StringBuilder sbrow = new StringBuilder();
            StringBuilder sbLegend = new StringBuilder();

            sql = @"select  c.posttypeid,month(h.ReturnCATime) as yue ,count(*) as DataValue,max(c.posttypename) DataName
                    from [dbo].[CertificateCAHistory] h
                    inner join [dbo].[CERTIFICATE] c
                    on h.CertificateID = c.CERTIFICATEID
                    where year(h.ReturnCATime) = {0}
                    group by c.posttypeid,month(h.ReturnCATime)
                    order by yue,posttypeid";
                dt = CommonDAL.GetDataTable(string.Format(sql, year));
          



            DataColumn[] pk = new DataColumn[2];
            pk[0] = dt.Columns["yue"];
            pk[1] = dt.Columns["DataName"];
            dt.PrimaryKey = pk;

            string[] months = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };//月份
            string[] series = new string[] { };//岗位类别序列

            #region 读取岗位类别序列

            sql = @"select distinct c.posttypename,c.posttypeid
                    from [dbo].[CertificateCAHistory] h
                    inner join [dbo].[CERTIFICATE] c
                    on h.CertificateID = c.CERTIFICATEID
                    where year(h.ReturnCATime) = {0} order by c.posttypeid";

            DataTable dt_series = CommonDAL.GetDataTable(string.Format(sql, year));
            series = new string[dt_series.Rows.Count];
            for(int i=0;i< dt_series.Rows.Count;i++)
            {
                series[i] = dt_series.Rows[i]["posttypename"].ToString();
            }
            #endregion 读取岗位类别序列

            DataRow find = null;
            foreach (string s in series)
            {
                sbLegend.Append(",").Append("'").Append(s).Append("'");

                sbItem.Remove(0, sbItem.Length);

                sbrow.Append("<tr><td>").Append(s).Append("</td>");

                foreach (string q in months)
                {
                    find = dt.Rows.Find(new object[] { Convert.ToInt32(q), s });
                    if (find != null)
                    {
                        sbItem.Append(",").Append(find["DataValue"].ToString());
                        sbrow.Append("<td>").Append(find["DataValue"].ToString()).Append("</td>");
                    }
                    else
                    {
                        sbItem.Append(",0");
                        sbrow.Append("<td>").Append("0").Append("</td>");
                    }
                }
                if (sbItem.Length > 0)
                {
                    sbItem.Remove(0, 1);
                }
                sb.Append(string.Format(@",(name: '{0}',type: 'bar',stack:'关键岗位专业技术管理人员',data: [{1}],temStyle : ( normal: (barBorderRadius :5)))", s, sbItem.ToString()));
                sbrow.Append("</tr>");
            }
            if (sb.Length > 0)
            {
                sb.Remove(0, 1).Replace("(", "{").Replace(")", "}");
            }
            if (sbLegend.Length>0)
            {
                sbLegend.Remove(0, 1);
            }
            ViewState["ECertCreateByMonth"] = sb.ToString();
            ViewState["ECertCreateByMonthLegend"] = sbLegend.ToString();

            Div_TableECertCreateByMonth.InnerHtml = string.Format("<table class=\"tb\"><tr><td></td><td>1月</td><td>2月</td><td>3月</td><td>4月</td><td>5月</td><td>6月</td><td>7月</td><td>8月</td><td>9月</td><td>10月</td><td>11月</td><td>12月</td></tr>{0}</table>"
                , sbrow.ToString()
                );
        }

        /// <summary>
        /// 绑定年度按月统计电子证书下载数量
        /// </summary>
        /// <param name="year">统计年份</param>
        private void BindECertDownByMonth(int year)
        {
            DataTable dt = null;
            System.Text.StringBuilder sb = new StringBuilder();
            string sql = "";
            System.Text.StringBuilder sbItem = new StringBuilder();
            System.Text.StringBuilder sbrow = new StringBuilder();
            StringBuilder sbLegend = new StringBuilder();

            sql = @"select c.posttypeid,max(c.posttypename) DataName,month(d.DownTime) as yue , count(*) as DataValue
	                        from [dbo].[FileDownLog] d
	                        inner  join [dbo].[CertificateCAHistory] h on d.FileID = h.CertificateCAID
	                        inner join [dbo].[CERTIFICATE] c on h.CertificateID = c.CertificateID
	                         where year(d.DownTime)= {0}
                            group by c.posttypeid,month(d.DownTime)
                            order by yue,posttypeid";
            dt = CommonDAL.GetDataTable(string.Format(sql, year));

            DataColumn[] pk = new DataColumn[2];
            pk[0] = dt.Columns["yue"];
            pk[1] = dt.Columns["DataName"];
            dt.PrimaryKey = pk;

            string[] months = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };//月份
            string[] series = new string[] { };//岗位类别序列

            #region 读取岗位类别序列

            sql = @" select distinct c.posttypename,c.posttypeid
                     from [dbo].[FileDownLog] d
	                inner  join [dbo].[CertificateCAHistory] h on d.FileID = h.CertificateCAID
	                inner join [dbo].[CERTIFICATE] c on h.CertificateID = c.CertificateID
	                where year(d.DownTime)= {0} order by c.posttypeid";

            DataTable dt_series = CommonDAL.GetDataTable(string.Format(sql, year));
            series = new string[dt_series.Rows.Count];
            for (int i = 0; i < dt_series.Rows.Count; i++)
            {
                series[i] = dt_series.Rows[i]["posttypename"].ToString();
            }
            #endregion 读取岗位类别序列

            DataRow find = null;
            foreach (string s in series)
            {
                sbLegend.Append(",").Append("'").Append(s).Append("'");

                sbItem.Remove(0, sbItem.Length);

                sbrow.Append("<tr><td>").Append(s).Append("</td>");

                foreach (string q in months)
                {
                    find = dt.Rows.Find(new object[] { Convert.ToInt32(q), s });
                    if (find != null)
                    {
                        sbItem.Append(",").Append(find["DataValue"].ToString());
                        sbrow.Append("<td>").Append(find["DataValue"].ToString()).Append("</td>");
                    }
                    else
                    {
                        sbItem.Append(",0");
                        sbrow.Append("<td>").Append("0").Append("</td>");
                    }
                }
                if (sbItem.Length > 0)
                {
                    sbItem.Remove(0, 1);
                }
                sb.Append(string.Format(@",(name: '{0}',type: 'bar',stack:'关键岗位专业技术管理人员',data: [{1}],temStyle : ( normal: (barBorderRadius :5)))", s, sbItem.ToString()));
                sbrow.Append("</tr>");
            }
            if (sb.Length > 0)
            {
                sb.Remove(0, 1).Replace("(", "{").Replace(")", "}");
            }
            if (sbLegend.Length > 0)
            {
                sbLegend.Remove(0, 1);
            }
       

            ViewState["ECertDownByMonth"] = sb.ToString();
            ViewState["ECertDownByMonthLegend"] = sbLegend.ToString();

            Div_TableECertDownByMonth.InnerHtml = string.Format("<table class=\"tb\"><tr><td></td><td>1月</td><td>2月</td><td>3月</td><td>4月</td><td>5月</td><td>6月</td><td>7月</td><td>8月</td><td>9月</td><td>10月</td><td>11月</td><td>12月</td></tr>{0}</table>"
                , sbrow.ToString()
                );
        }


        protected void RadNumericTextBoxYear_TextChanged(object sender, EventArgs e)
        {
            BindECertCreateByMonth(Convert.ToInt32(RadNumericTextBoxYear.Value));
            BindECertDownByMonth(Convert.ToInt32(RadNumericTextBoxYear.Value));
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "BindECertCreateByMonth", "var o1=\"" + ViewState["ECertCreateByMonth"].ToString() + "\";var g1=\"" + ViewState["ECertCreateByMonthLegend"].ToString() + "\";var o2=\"" + ViewState["ECertDownByMonth"].ToString() + "\";var g2=\"" + ViewState["ECertDownByMonthLegend"].ToString() + "\";BindECertCreateByMonth(o1,g1,o2,g2);", true);

        }
    }
}