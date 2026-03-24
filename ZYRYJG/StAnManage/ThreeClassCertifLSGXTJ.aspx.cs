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
    public partial class ThreeClassCertifLSGXTJ : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string sql = @"
                        select 
                            case when lsgxtype like '%无效隶属%' then '无效隶属' else right(lsgxtype,len(lsgxtype) -1) end lsgxtype
                            ,min(lsgx) lsgx
                            ,userid
                            ,organid
                            ,count(*) CertCount 
                        from 
                        (
                            select 
                                c.certificateid,
                                case 
                                    when w.qylb ='外地进京' then '4外地进京'
                                    when g.organid=242 then '1区县建委' 
                                    when g.organid=246 then '2市属集团总公司' 
                                    when g.organid=247 then '3中央驻京单位'
                                    when j.qylb='起重机械租赁企业' then '5起重机械租赁企业'
                                    else '6无效隶属_' + isnull(g.lsgx,'') 
                                end as lsgxtype,
                                case 
                                    when w.qylb ='外地进京' then '外地进京'
                                    when g.userid is not null  then g.lsgx 
                                    when j.qylb='起重机械租赁企业' then '起重机械租赁企业'
                                    else isnull(g.lsgx,'') 
                                end
                                as lsgx,
                                case 
                                    when w.qylb ='外地进京' then 338
                                    when g.userid is not null  then g.userid
                                    when j.qylb='起重机械租赁企业' then 328
                                    else g.userid 
                                end as userid,
                                case 
                                    when w.qylb ='外地进京' then 242
                                    when g.userid is not null  then g.organid
                                    when j.qylb='起重机械租赁企业' then 242
                                    else g.organid 
                                end as organid
                            from dbo.VIEW_CERTIFICATECONTINUE_THREECLASS_CURYEAR c
                            left join dbo.qy_hylsgx g on c.unitcode = g.zzjgdm
                            left join
                            (
	                            select distinct zzjgdm,qymc,qylb
	                             from dbo.QY_BWDZZZS 
	                            WHERE qylb='外地进京' and qymc <>''
                            ) w on c.unitcode =w.zzjgdm
                            left join
                            (
	                            select distinct zzjgdm,qymc,qylb
	                             from dbo.QY_BWDZZZS 
	                            WHERE qylb='起重机械租赁企业' and qymc <>''
                            ) j on c.unitcode =j.zzjgdm     
                        ) t
                        where 1=1 {0}
                        group by lsgxtype,organid,userid
                        order by t.lsgxtype,organid,userid;";
                DataTable dt = CommonDAL.GetDataTable(string.Format(sql, (PersonID == 242 || PersonID == 246 || PersonID == 247 || PersonID == 228 || PersonID == 338) ? string.Format("and userid={1}", PersonID) : ""));
                DataRow zj = dt.NewRow();
                zj["lsgx"] = "总计";
                int sum = 0;
                foreach (DataRow r in dt.Rows)
                {
                    sum += Convert.ToInt32(r["CertCount"]);
                }
                zj["CertCount"] = sum;
                dt.Rows.Add(zj);
                RadGrid1.DataSource = dt;
                RadGrid1.DataBind();
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
            switch (e.FormattedColumn.UniqueName)
            {
                case "CertificateCode":
                case "WorkerCertificateCode":
                case "UnitCode":
                    e.Cell.Style["mso-number-format"] = @"\@";
                    break;
            }

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
    }
}