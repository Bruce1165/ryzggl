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

namespace ZYRYJG.EXamManage
{
    public partial class CertificateAllocateNoList : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "CertificateAllocateNo.aspx";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                ButtonSearch_Click(sender, e);
            }
        }

        //刷新成绩Grid
        protected void RefreshGrid(int pageIndex)
        {
            DataTable dt = null;
            int rowCount = 0;
            if (ExamPlanSelect1.ExamPlanID.HasValue == false)
            {
                dt = new DataTable();
            }
            else
            {
                QueryParamOB q = new QueryParamOB();

                //查询已放号证书
                q.Add("ExamPlanID=" + (ExamPlanSelect1.ExamPlanID.HasValue == true ? ExamPlanSelect1.ExamPlanID.ToString() : "0"));// 考试计划
                rowCount = CertificateDAL.SelectCount(q.ToWhereString());

                if (this.RadioButtonListStatus.SelectedValue == "1")
                {
                    q = new QueryParamOB();
                    q.Add("ExamPlanID=" + (ExamPlanSelect1.ExamPlanID.HasValue == true ? ExamPlanSelect1.ExamPlanID.ToString() : "0"));// 考试计划
                    dt = CertificateDAL.GetList(pageIndex * RadGridExamResult.PageSize, RadGridExamResult.PageSize, q.ToWhereString(), "CertificateCode");
                }
                else
                {
                    q = new QueryParamOB();
                    q.Add(string.Format("Status = '{0}'", EnumManager.ExamResultStatus.Published));//已公告
                    q.Add(string.Format("ExamResult = '{0}'", EnumManager.ExamResult.Pass));//已通过
                    q.Add("ExamPlanID=" + (ExamPlanSelect1.ExamPlanID.HasValue == true ? ExamPlanSelect1.ExamPlanID.ToString() : "0"));// 考试计划

                    if (rowCount > 0)
                    {
                        //----查询考试结果，通过数据量比较为审批不通过使用
                        q.Add("WorkerID NOT IN (SELECT WorkerID FROM DBO.Certificate WHERE ExamPlanID=" + (ExamPlanSelect1.ExamPlanID.HasValue == true ? ExamPlanSelect1.ExamPlanID.ToString() : "0") + ")");
                    }
                    //------------------------------------------------

                    rowCount = ExamResultDAL.SelectCountView_ExamScore(q.ToWhereString());
                    dt = ExamResultDAL.GetListView_ExamScore(pageIndex * RadGridExamResult.PageSize, RadGridExamResult.PageSize, q.ToWhereString(), "TrainUnitName,FirstTrialTime,ExamSignUpID");
                    dt.Columns.Add("CertificateCode", typeof(System.String));
                }
            }

            //绑定数据
            RadGridExamResult.VirtualItemCount = rowCount;
            RadGridExamResult.CurrentPageIndex = pageIndex;
            RadGridExamResult.DataSource = dt;
            RadGridExamResult.DataBind();
        }

        //查询
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {

            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            RefreshGrid(0); 
        }

        //列绑定
        protected void RadGridExamResult_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                if (this.RadioButtonListStatus.SelectedValue == "1")
                {
                    DataRowView mark = (DataRowView)e.Item.DataItem;
                    QueryParamOB q = new QueryParamOB();
                    q.Add("ExamPlanID=" + (ExamPlanSelect1.ExamPlanID.HasValue == true ? ExamPlanSelect1.ExamPlanID.ToString() : "0"));// 考试计划
                    q.Add("WorkerID=" + mark["WorkerID"].ToString());
                    DataTable dtTrain = ExamResultDAL.GetListView_ExamScore(0, int.MaxValue - 1, q.ToWhereString(), "FirstTrialTime,ExamSignUpID");
                    string TrainUnitName = "";
                    if (dtTrain.Rows.Count > 0)
                    {
                        TrainUnitName = dtTrain.Rows[0]["TrainUnitName"].ToString();
                    }

                    e.Item.Cells[RadGridExamResult.MasterTableView.Columns.FindByUniqueName("TrainUnitName").OrderIndex].Text = TrainUnitName;
                }
            }
        }

        //成绩分页
        protected void RadGridExamResult_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            RefreshGrid(e.NewPageIndex);
        }


        //导出成绩
        protected void ButtonOutput_Click(object sender, EventArgs e)
        {
            if (RadGridExamResult.MasterTableView.Items.Count == 0)
            {
                UIHelp.layerAlert(Page, "没有可导出的数据！");
                return;
            }
            RadGridExamResult.PageSize = RadGridExamResult.MasterTableView.VirtualItemCount;//
            RadGridExamResult.ExportSettings.ExportOnlyData = true;
            RadGridExamResult.ExportSettings.IgnorePaging = false;
            RadGridExamResult.ExportSettings.OpenInNewWindow = true;
            RefreshGrid(0);
            RadGridExamResult.MasterTableView.ExportToExcel();
        }

        //格式化Excel
        protected void RadGridExamResult_ExcelExportCellFormatting(object source, ExcelExportCellFormattingEventArgs e)
        {
            switch (e.FormattedColumn.UniqueName)
            {
                case "WorkerCertificateCode": e.Cell.Style["mso-number-format"] = @"\@"; break;
                case "CertificateCode": e.Cell.Style["mso-number-format"] = @"\@"; break;
            }
            //HeadCell
            GridItem item = e.Cell.Parent as GridItem;
            if (item.ItemIndex == 0 && e.FormattedColumn.UniqueName == "WorkerName")
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