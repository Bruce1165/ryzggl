using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using DataAccess;
using Telerik.Web.UI;
using Utility;

namespace ZYRYJG.StAnManage
{
    public partial class CertifApplyResultQuery : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RadDatePicker_StartDate.SelectedDate = Convert.ToDateTime(string.Format("{0}-01-01", DateTime.Now.Year));
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

            //统计结果模版
            DataTable dtBase = CertificateDAL.AnalysisCertificateApplyResultBase();
            DataRow drSum = dtBase.NewRow();
            dtBase.Rows.Add(drSum);
            drSum["PostTypeName"] = "总计";
            DataColumn[] dcKeys = new DataColumn[2];
            dcKeys[0] = dtBase.Columns["PostTypeName"];
            dtBase.PrimaryKey = dcKeys;

            //填充统计数据
            DataTable dtData = CertificateDAL.AnalysisCertificateApplyResultData(RadDatePicker_StartDate.SelectedDate, RadDatePicker_EndDate.SelectedDate);
            DataRow drFind = null;
            foreach (DataRow dr in dtData.Rows)
            {
                drFind = dtBase.Rows.Find(new object[] { dr["PostTypeName"] });
                //drFind = dtBase.Rows.Find( dr["PostName"] );
                if (drFind != null && dtBase.Columns.IndexOf(dr["countType"].ToString()) != -1) drFind[dr["countType"].ToString()] = dr["Itemcount"];
            }

            //计算小计、合计
            //System.Collections.Generic.Dictionary<string, int> sum = new Dictionary<string, int>() { { "考试申请个数", 0 }, { "续期申请个数", 0 }, { "京内变更申请个数", 0 }, { "离京变更申请个数", 0 }, { "进京变更申请个数", 0 }, { "注销申请个数", 0 }, { "补办申请个数", 0 }, { "考试通过个数", 0 }, { "续期通过个数", 0 }, { "京内变更通过个数", 0 }, { "离京变更通过个数", 0 }, { "进京变更通过个数", 0 }, { "注销通过个数", 0 }, { "补办通过个数", 0 } };
            //var sum = new Dictionary<string, int>() { { "考试申请个数", 0 }, { "续期申请个数", 0 }, { "京内变更申请个数", 0 }, { "离京变更申请个数", 0 }, { "进京变更申请个数", 0 }, { "注销申请个数", 0 }, { "补办申请个数", 0 }, { "考试通过个数", 0 }, { "续期通过个数", 0 }, { "京内变更通过个数", 0 }, { "离京变更通过个数", 0 }, { "进京变更通过个数", 0 }, { "注销通过个数", 0 }, { "补办通过个数", 0 }, { "续期申请并通过个数", 0 }, { "京内变更申请并通过个数", 0 }, { "离京变更申请并通过个数", 0 }, { "进京变更申请并通过个数", 0 }, { "注销申请并通过个数", 0 }, { "补办申请并通过个数", 0 } };
            var sum = new Dictionary<string, int>() { { "考试申请个数", 0 }, { "续期申请个数", 0 }, { "京内变更申请个数", 0 }, { "离京变更申请个数", 0 }, { "进京变更申请个数", 0 }, { "注销申请个数", 0 },  { "考试通过个数", 0 }, { "续期通过个数", 0 }, { "京内变更通过个数", 0 }, { "离京变更通过个数", 0 }, { "进京变更通过个数", 0 }, { "注销通过个数", 0 },{ "续期申请并通过个数", 0 }, { "京内变更申请并通过个数", 0 }, { "离京变更申请并通过个数", 0 }, { "进京变更申请并通过个数", 0 }, { "注销申请并通过个数", 0 }, { "缺考人数", 0 }};
            //string[] keys = new string[14];
            var keys = new string[18];
            sum.Keys.CopyTo(keys, 0);
            for (int i = 0; i < dtBase.Rows.Count - 1; i++)
            {
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
            ViewState["dtBase"] = dtBase;
            RadGrid1.DataBind();

            Session["CertifApplyResultQuery_dtBase"] = dtBase;

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
            var item = e.Cell.Parent as GridItem;
            if (item.ItemIndex == 0 && e.FormattedColumn.UniqueName == "PostTypeName")
            {
                var gtv = e.Cell.Parent.Parent.Parent as GridTableView;
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

        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Header)
            {
                DynamicTHeaderHepler dHelper = new DynamicTHeaderHepler();
                //string header = "空1#空2#证书类别#考试 参加数量,通过数量#续期 申请数量,通过数量#京内变更 申请数量,通过数量#离京变更 申请数量,通过数量#进京变更 申请数量,通过数量#注销 申请数量,通过数量#补办 申请数量,通过数量";// "证书类别 ,#考试 参加数量,通过数量";
                const string header = "空1#空2#证书类别#考试 参加数量,通过数量,缺考人数#续期 申请数量,通过数量,申请并通过量#京内变更 申请数量,通过数量,申请并通过量#离京变更 申请数量,通过数量,申请并通过量#进京变更 申请数量,通过数量,申请并通过量#注销 申请数量,通过数量,申请并通过量"; // "证书类别 ,#考试 参加数量,通过数量";
                dHelper.SplitTableHeader(e.Item, header);
            }
        }
    }
}