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
    public partial class ExamAdnormalList : BasePage
    {
        protected bool isExcelExport = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
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
            ObjectDataSource1.SelectParameters.Clear();//清空查询参数
            QueryParamOB q = new QueryParamOB();
            string _ExamPlanID = ExamPlanSelect1.ExamPlanID.HasValue ? ExamPlanSelect1.ExamPlanID.ToString() : "0";//考试计划
            if (_ExamPlanID != "0")
            {
                q.Add("m.ExamPlanID=" + _ExamPlanID);// 考试计划
            }
            //q.Add(string.Format("ExamStatus != '{0}'", "正常"));   //状态
            q.Add("(ExamStatus = '替考' or ExamStatus = '违纪')");   //状态
            if (RadComboBoxPostTypeID.SelectedItem.Text .ToString() != "请选择")
            {
                q.Add(string.Format("k.PostName='{0}'", RadComboBoxPostTypeID.SelectedItem.Text.ToString()));
            }
            if (RadTextBoxExamPlaceName.Text.Trim() != "")
            {
                q.Add(string.Format("m.ExamPlaceName like '%{0}%'", RadTextBoxExamPlaceName.Text.ToString()));
            }
            if (RadTextBoxExamRoomCode.Text.Trim() != "")
            {
                q.Add(string.Format("m.ExamRoomCode like '%{0}%'", RadTextBoxExamRoomCode.Text.ToString()));
            }
            if (RadTextBoxExamCardID.Text.Trim() != "")
            {
                q.Add(string.Format("m.ExamCardID like '%{0}%'", RadTextBoxExamCardID.Text.ToString()));
            }
            if (RadTextBoxWorkerName.Text.Trim() != "")
            {
                q.Add(string.Format("m.WorkerName like '%{0}%'", RadTextBoxWorkerName.Text.ToString()));
            }           
          
            if (PostSelect1.PostID != "")//工种
            {
                q.Add(string.Format("m.PostID >= {0} and m.PostID <= {0}", PostSelect1.PostID));
            }
            else if (PostSelect1.PostTypeID != "")//岗位
            {
                q.Add(string.Format("m.PostTypeID >= {0} and m.PostTypeID <= {0} ", PostSelect1.PostTypeID));
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
                case "ExamCardID": e.Cell.Style["mso-number-format"] = @"\@"; break;
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

        //绑定科目
        protected void RadComboBoxPostTypeID_Init(object sender, EventArgs e)
        {
            RadComboBox rcb = (RadComboBox)sender;
            List<PostInfoOB> dt = PostInfoDAL.GetObject();
            rcb.DataSource = dt;
            rcb.DataBind();
            rcb.Items.Insert(0, new RadComboBoxItem("请选择", ""));


        }

    }
}
