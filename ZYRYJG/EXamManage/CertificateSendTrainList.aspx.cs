using System;
using System.Collections.Generic;
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
    public partial class CertificateSendTrainList : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "CertificatePrint.aspx";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {        
            if(!IsPostBack)
            {
                for (int i = 2010; i <= (DateTime.Now.Year + 1); i++)
                {
                    RadComboBoxYear.Items.Insert(0, new RadComboBoxItem(i.ToString(), i.ToString()));
                    if (i == DateTime.Now.Year)
                    {
                        RadComboBoxYear.Items.FindItemByValue(i.ToString()).Selected = true;
                    }
                }
                RadComboBoxMonth.Items.FindItemByValue(DateTime.Now.Month.ToString()).Selected = true;
                RadComboBoxYear.Items.Insert(0, new RadComboBoxItem("全部", ""));


                ButtonSearch_Click(sender, e);
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
            QueryParamOB q = new QueryParamOB();

            //发证时间
            if (RadComboBoxYear.SelectedValue != "") q.Add(string.Format("DATEPART(year,CONFERDATE) = {0}", RadComboBoxYear.SelectedValue));//年
            if (RadComboBoxMonth.SelectedValue != "") q.Add(string.Format("DATEPART(month,CONFERDATE) = {0}", RadComboBoxMonth.SelectedValue));//月
          
            if (PostSelect1.PostID != "")
            {
                q.Add(string.Format("PostID = {0}", PostSelect1.PostID));//工种
            }
            else if (PostSelect1.PostTypeID != "")
            {
                q.Add(string.Format("PostTypeID = {0}", PostSelect1.PostTypeID));//岗位类别
            }
            if (RadTextBoxPeiXunUnitName.Text.Trim() != "")
            {
                q.Add(string.Format("TrainUnitName like '%{0}%'", RadTextBoxPeiXunUnitName.Text.Trim()));// 培训点
            }
            if (RadDatePickerCheckDate.SelectedDate.HasValue ==true)
            {
                q.Add(string.Format("(CheckDate between '{0}' and '{1}')", RadDatePickerCheckDate.SelectedDate.Value.ToString("yyyy-MM-dd"), RadDatePickerCheckDate.SelectedDate.Value.ToString("yyyy-MM-dd 23:59:59")));//年
            }
            if(ExamPlanSelect1.ExamPlanID.HasValue == true)//考试计划
            {
                q.Add(string.Format("ExamPlanID = {0} ", ExamPlanSelect1.ExamPlanID));// 考试计划
            }
            string sql = @"select  TrainUnitName,max(PostName) as PostName,PostID,CONFERDATE,count(*) 'Count',min(certificatecode) min_code,max(certificatecode) max_code
                             from dbo.certificate
                            where examplanid >0
                            and ValidEndDate >=(cast(year(getdate()) as varchar(4)) + '-12-31') and ([STATUS] = '首次' or [STATUS] = '续期' or [STATUS] = '进京变更' or [STATUS] = '京内变更' or [STATUS] = '补办')
                            {0}
                            group by TrainUnitName,PostID,CONFERDATE                        
                            order by PostID,CONFERDATE,min_code";
           DBHelper db = new DBHelper();
           DataTable dt = null;
           try
           {
               dt = db.GetFillData(string.Format(sql, q.ToWhereString()));
           }
           catch (Exception ex)
           {
               UIHelp.WriteErrorLog(Page, "查询证书下发单位对照表失败！", ex);
               return;
           }
           int sum = 0;
           foreach (DataRow dr in dt.Rows)
           {
               sum += Convert.ToInt32(dr["Count"]);
           }

           DataRow drSum = dt.NewRow();
           dt.Rows.Add(drSum);
           drSum["postname"] = "合计";
           drSum["count"] = sum;
           drSum["TrainUnitName"] = "";


           RadGridTrainUnitCertificateCode.DataSource = dt;
           RadGridTrainUnitCertificateCode.DataBind();
        }


        //导出证书下发培训点、企业对照表（excel）
        protected void ButtonOutputSendTablel_Click(object sender, EventArgs e)
        {
            RadGridTrainUnitCertificateCode.ExportSettings.FileName = DateTime.Now.ToString("yyyy-MM-dd");
            RadGridTrainUnitCertificateCode.ExportSettings.OpenInNewWindow = true;
            RadGridTrainUnitCertificateCode.ExportSettings.ExportOnlyData = true;
            //RadGridTrainUnitCertificateCode.PageSize = RadGridTrainUnitCertificateCode.MasterTableView.VirtualItemCount;//
            //RadGridTrainUnitCertificateCode.CurrentPageIndex = 0;
            //RadGridTrainUnitCertificateCode.Rebind();
            RadGridTrainUnitCertificateCode.MasterTableView.ExportToExcel();
            RadGridTrainUnitCertificateCode.MasterTableView.HeaderStyle.BackColor = System.Drawing.Color.FromName("#DEDEDE");
        }

        //导出excel格式化（证书下发培训点、企业对照表）
        protected void RadGridTrainUnitCertificateCode_ExcelExportCellFormatting(object source, ExcelExportCellFormattingEventArgs e)
        {
            switch (e.FormattedColumn.UniqueName)
            {
                case "min_code": e.Cell.Style["mso-number-format"] = @"\@"; break;
                case "max_code": e.Cell.Style["mso-number-format"] = @"\@"; break;
            }
            //HeadCell
            GridItem item = e.Cell.Parent as GridItem;
            if (item.ItemIndex == 0 && e.FormattedColumn.UniqueName == "TrainUnitName")
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
