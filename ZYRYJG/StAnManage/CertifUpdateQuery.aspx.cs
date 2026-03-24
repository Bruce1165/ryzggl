using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using DataAccess;
using Telerik.Web.UI;

namespace ZYRYJG.StAnManage
{
    public partial class CertifUpdateQuery : BasePage
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
            DataTable dtBase = CertificateDAL.AnalysisCertificateManageBase(null, null);
            DataRow drSum = dtBase.NewRow();
            dtBase.Rows.Add(drSum);
            drSum["PostTypeName"] = "总计";
            drSum["PostName"] = "总计";
            //DataColumn[] dcKeys = new DataColumn[1];
            //dcKeys[0] = dtBase.Columns["PostName"];
            DataColumn[] dcKeys = new DataColumn[2];
            dcKeys[0] = dtBase.Columns["PostTypeName"];
            dcKeys[1] = dtBase.Columns["PostName"];
            dtBase.PrimaryKey = dcKeys;

            //填充统计数据
            DataTable dtData = CertificateDAL.AnalysisCertificateUpdateData(RadDatePicker_StartDate.SelectedDate, RadDatePicker_EndDate.SelectedDate, null, null);
            DataRow drFind = null;

            foreach (DataRow dr in dtData.Rows)
            {
                drFind = dtBase.Rows.Find(new object[] { dr["PostTypeName"], dr["PostName"] });
                if (drFind != null && dtBase.Columns.IndexOf(dr["CHANGETYPE"].ToString()) != -1)
                    drFind[dr["CHANGETYPE"].ToString()] = dr["count"];
            }

            //计算小计、合计
            System.Collections.Generic.Dictionary<string, int> sum = new Dictionary<string, int>() { { "首次", 0 }, { "续期", 0 }, { "京内变更", 0 }, { "离京变更", 0 }, { "进京变更", 0 }, { "注销", 0 }, { "补办", 0 }, { "小计", 0 } };
            string[] keys = new string[8];
            sum.Keys.CopyTo(keys, 0);
            int rowSum = 0;
            for (int i = 0; i < dtBase.Rows.Count - 1; i++)
            {
                //小计
                rowSum = 0;
                foreach (string s in keys)
                {
                    rowSum += Convert.ToInt32(dtBase.Rows[i][s]);
                }
                dtBase.Rows[i]["小计"] = rowSum;

                //合计
                foreach (string s in keys)
                {
                    sum[s] += Convert.ToInt32(dtBase.Rows[i][s]);
                }
            }
            foreach (string s in keys)
            {
                drSum[s] = sum[s];
            }
            RadGrid1.BorderWidth = System.Web.UI.WebControls.Unit.Pixel(1);
            RadGrid1.DataSource = dtBase;
            RadGrid1.DataBind();
            Session["CertifUpdateQuery_dtBase"] = dtBase;
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
    }
}