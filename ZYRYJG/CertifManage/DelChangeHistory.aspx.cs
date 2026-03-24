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

namespace ZYRYJG.CertifManage
{
    public partial class DelChangeHistory : BasePage
    {

        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "CertifChangeCheckConfirm.aspx";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                switch (string.IsNullOrEmpty(Request["o"]) ? "1" : Request["o"].ToString())
                {
                    case "1":
                        LabelCheck.Text = "三类人员";
                        break;
                    case "2":
                        LabelCheck.Text = "特种作业";
                        break;
                    case "3":
                        LabelCheck.Text = "造价员";
                        break;
                }
                //rbl.SelectedValue = "0";
                PostSelect1.PostTypeID = string.IsNullOrEmpty(Request["o"]) ? "1" : Request["o"].ToString();
                PostSelect1.LockPostTypeID();
                ButtonSearch_Click(sender, e);
            }
        }
        //根据输入的条件查询
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ObjectDataSource1.SelectParameters.Clear();
            QueryParamOB q = new QueryParamOB();
            if (txtCertificateCode.Text.Trim() != "")//证件编号
            {
                q.Add(string.Format("CERTIFICATECODE like '%{0}%'", txtCertificateCode.Text.Trim()));
            }
            if (RadTextBoxWorkerName.Text.Trim() != "")//姓名
            {
                q.Add(string.Format("WorkerName like '%{0}%'", RadTextBoxWorkerName.Text.Trim()));
            }
            if (PostSelect1.PostTypeID != "")//岗位
            {
                q.Add(string.Format("PostTypeID = {0}", PostSelect1.PostTypeID));
            }
            //工种类别
            if (PostSelect1.PostID != "")
            {
                switch (PostSelect1.PostID)
                {
                    case "9"://土建
                        q.Add(string.Format("(PostID = {0} or PostName like '%增土建')", PostSelect1.PostID));
                        break;
                    case "12"://安装
                        q.Add(string.Format("(PostID = {0} or PostName like '%增安装')", PostSelect1.PostID));
                        break;
                    default:
                        q.Add(string.Format("PostID = {0}", PostSelect1.PostID));
                        break;
                }
            }
            if (RadTextBoxWorkerCertificateCode.Text.Trim() != "")
            {
                q.Add(string.Format("WorkerCertificateCode like '%{0}%'", RadTextBoxWorkerCertificateCode.Text.Trim()));
            }
            //if (txtUnitName.Text.Trim() != "")//企业名称
            //{
            //    q.Add(string.Format("UnitName like '%{0}%'", txtUnitName.Text.Trim()));
            //}
            //if (RadTextBoxUnitCode.Text.Trim() != "")//组织机构代码
            //{
            //    q.Add(string.Format("UnitCode like '%{0}%'", RadTextBoxUnitCode.Text.Trim()));
            //}
            if (RadTextBoxApplyCode.Text.Trim() != "")   //申请编号
            {
                q.Add(string.Format("ApplyCode like '%{0}%'", RadTextBoxApplyCode.Text.Trim()));
            }

            ////初审单位
            //if (RadTextBoxFirstCheckUnitName.Text.Trim() != "") q.Add(string.Format("FirstCheckUnitName like '%{0}%'", RadTextBoxFirstCheckUnitName.Text.Trim()));

            ////证书有效期至
            //if (!txtValidEndtDate.IsEmpty)
            //{
            //    q.Add(string.Format("ValidEndDate ='{0}'", txtValidEndtDate.SelectedDate.Value.ToString("yyyy-MM-dd")));
            //}
            //if (rbl.SelectedValue != "") q.Add(string.Format("Status ='{0}'", rbl.SelectedValue));
            //switch (RadioButtonListResult.SelectedValue)
            //{
            //    case "不予受理":
            //        q.Add("GetResult ='不予受理'");
            //        break;
            //    case "决定通过":
            //        q.Add("ConfirmResult ='决定通过'");
            //        break;
            //    case "决定不通过":
            //        q.Add("ConfirmResult ='决定不通过'");
            //        break;
            //}
               
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridCheck.CurrentPageIndex = 0;
            RadGridCheck.DataSourceID = ObjectDataSource1.ID;
        }
 
     
        //导出
        protected void ButtonExport_Click(object sender, EventArgs e)
        {
            if (RadGridCheck.MasterTableView.VirtualItemCount == 0)
            {
                UIHelp.layerAlert(Page, "没有可导出的数据！");
                return;
            }
           

            RadGridCheck.PageSize = RadGridCheck.MasterTableView.VirtualItemCount;//
            RadGridCheck.CurrentPageIndex = 0;
            RadGridCheck.Rebind();
            RadGridCheck.ExportSettings.ExportOnlyData = true;
            RadGridCheck.ExportSettings.OpenInNewWindow = true;
            RadGridCheck.MasterTableView.ExportToExcel();
            RadGridCheck.MasterTableView.HeaderStyle.BackColor = System.Drawing.Color.FromName("#DEDEDE");
        }
        //格式化Excel
        protected void RadGridCheck_ExcelExportCellFormatting(object source, ExcelExportCellFormattingEventArgs e)
        {
            switch (e.FormattedColumn.UniqueName)
            {
                case "WorkerCertificateCode": e.Cell.Style["mso-number-format"] = @"\@"; break;

            }
            e.Cell.Attributes.Add("align", "center");
            e.Cell.Style.Add("border-width", "0.1pt");
            e.Cell.Style.Add("border-style", "solid");
            e.Cell.Style.Add("border-color", "#CCCCCC");
            if ( e.Cell.Parent is GridHeaderItem)
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
    }
}
