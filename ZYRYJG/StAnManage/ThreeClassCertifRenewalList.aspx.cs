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
    public partial class ThreeClassCertifRenewalList : BasePage
    {
        protected override void OnInit(EventArgs e)
        {
            PostSelect1.PostTypeID = "1";
            PostSelect1.LockPostTypeID();
            //PostSelect1.RadComboBoxPostTypeID.Items.FindItemByValue("2").Remove();//屏蔽建设施工特种作业类别
            //PostSelect1.RadComboBoxPostTypeID.Items.FindItemByValue("3").Remove();//屏蔽造价员类别
            //PostSelect1.RadComboBoxPostTypeID.Items.FindItemByValue("4").Remove();//屏蔽职业技能类别
            //PostSelect1.RadComboBoxPostTypeID.Items.FindItemByValue("5").Remove();//屏蔽专业管理人员类别
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = CommonDAL.GetDataTable("select distinct isnull(username,'无') as itemName, isnull(userid,99999999) as userid from VIEW_UNIT_LSGX union all select '全部',0 order by userid");
                RadComboBoxLSGX.DataSource = dt;
                RadComboBoxLSGX.DataTextField = "itemName";
                RadComboBoxLSGX.DataValueField = "userid";
                RadComboBoxLSGX.DataBind();

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
            QueryParamOB q = new QueryParamOB();

            if (PostSelect1.PostTypeID != "") q.Add(string.Format("PostTypeID='{0}'", PostSelect1.PostTypeID));
            if (PostSelect1.PostID != "") q.Add(string.Format("PostID='{0}'", PostSelect1.PostID));
            if (RadTextBoxCertificateCode.Text.Trim() != "") q.Add(string.Format("CertificateCode like '%{0}%'", RadTextBoxCertificateCode.Text.Trim()));
            if (RadTextBoxWorkerName.Text.Trim() != "") q.Add(string.Format("WorkerName like '%{0}%'", RadTextBoxWorkerName.Text.Trim()));
            if (RadTextBoxWorkerCertificateCode.Text.Trim() != "") q.Add(string.Format("WorkerCertificateCode like '%{0}%'", RadTextBoxWorkerCertificateCode.Text.Trim()));
            if (RadTextBoxUnitName.Text.Trim() != "") q.Add(string.Format("UnitName like '%{0}%'", RadTextBoxUnitName.Text.Trim()));
            if (RadTextBoxUnitCode.Text.Trim() != "") q.Add(string.Format("UnitCode like '%{0}%'", RadTextBoxUnitCode.Text.Trim()));

            //隶属关系
            if (RadComboBoxLSGX.SelectedValue == "99999999")
            {
                q.Add("UnitCode in(select unitcode from VIEW_UNIT_LSGX where userid is null)");
            }
            else if (RadComboBoxLSGX.SelectedValue != "0")
            {
                q.Add(string.Format("UnitCode in(select unitcode from VIEW_UNIT_LSGX where userid={0})", RadComboBoxLSGX.SelectedValue));
            }

            //DataTable dt = CommonDAL.GetDataTable(0, int.MaxValue - 1, "dbo.CONTINUE_A", " distinct * ", q.ToWhereString(), "PostID");
            //RadGrid1.DataSourceID = ObjectDataSource1.;
            //RadGrid1.MasterTableView.VirtualItemCount = dt.Rows.Count;
            //RadGrid1.DataBind();
            ObjectDataSource1.SelectParameters.Clear();
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGrid1.CurrentPageIndex = 0;
            RadGrid1.DataSourceID = ObjectDataSource1.ID;
        }

        ////准备查询数据
        //protected void DataStart()
        //{
        //    try
        //    {
        //        DBHelper dbh = new DBHelper();
        //        dbh.ExcuteNonQuery("dbo.P_GETSLRY_FHTJ", CommandType.StoredProcedure);
        //    }
        //    catch (Exception ex)
        //    {
        //        UIHelp.WriteErrorLog(Page, "处理符合续期申请条件三类人员数据失败！", ex);
        //        return;
        //    }
        //}


        //导出申请列表excel
        protected void ButtonExportToExcel_Click(object sender, EventArgs e)
        {
            if (RadGrid1.MasterTableView.VirtualItemCount == 0)
            {
                UIHelp.layerAlert(Page, "没有可导出的数据！");
                return;
            }
            RadGrid1.PageSize = RadGrid1.MasterTableView.VirtualItemCount;//
            RadGrid1.CurrentPageIndex = 0;
            RadGrid1.Rebind();
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
            if (item.ItemIndex == 0 && e.FormattedColumn.UniqueName == "CertificateCode")
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