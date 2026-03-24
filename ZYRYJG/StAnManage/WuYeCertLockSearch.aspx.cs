using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Telerik.Web.UI;

namespace ZYRYJG.StAnManage
{
    public partial class WuYeCertLockSearch : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) ButtonSearch_Click(sender, e);
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            QueryParamOB q = new QueryParamOB();
            q.Add("VALID = 1");//有效标志: 1-有效 ,0-无效
            if (RadTextBoxQYMC.Text.Trim() != "")   //企业名称
            {
                q.Add(string.Format("QYMC like '%{0}%'", RadTextBoxQYMC.Text.Trim()));
            }
            if (RadTextBoxZZJGDM.Text.Trim() != "")   //机构代码
            {
                q.Add(string.Format("ZZJGDM like '%{0}%'", RadTextBoxZZJGDM.Text.Trim()));
            }
            if (RadTextBoxZSBH.Text.Trim() != "")   //证书编号
            {
                q.Add(string.Format("ZSBH like '%{0}%'", RadTextBoxZSBH.Text.Trim()));
            }
            if (RadTextBoxGWGZ.Text.Trim() != "")   //岗位工种
            {
                q.Add(string.Format("GWGZ like '%{0}%'", RadTextBoxGWGZ.Text.Trim()));
            }
            if (RadTextBoxSDYWSX.Text.Trim() != "")   //锁定业务事项
            {
                q.Add(string.Format("SDYWSX like '%{0}%'", RadTextBoxSDYWSX.Text.Trim()));
            }
            if (RadTextBoxSDYWSM.Text.Trim() != "")   //锁定业务说明
            {
                q.Add(string.Format("SDYWSM like '%{0}%'", RadTextBoxSDYWSM.Text.Trim()));
            }
            if (RadioButtonListSDZT.SelectedValue != "")//锁定状态
            {
                q.Add(string.Format("SDZT = '{0}'", RadioButtonListSDZT.SelectedValue));
            }
            ViewState["QueryParamOB"] = q;
            RefreshGrid(0, RadGrid1.PageSize);
        }

        protected void RadGrid1_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            RefreshGrid(e.NewPageIndex, RadGrid1.PageSize);
        }

        //刷新成绩Grid
        protected void RefreshGrid(int pageIndex, int pageSize)
        {
            System.Data.DataSet ds = null;
            int resultCount = 0;
            Synergy.Common.DESCrypto descObj = new Synergy.Common.DESCrypto();
            //string userName = descObj.EncryptString("XNJ_RY");
            //string userPassword = descObj.EncryptString("XNJ2011");
            string userName = descObj.EncryptString("RTDL_RYKWXT");
            string userPassword = descObj.EncryptString("RTDL_2013");
            QueryParamOB q = ViewState["QueryParamOB"] as QueryParamOB;
            try
            {
                BaseDataService.InterFaceService ifs = new BaseDataService.InterFaceService();
                //ds = ifs.PageQueryData(userName, userPassword, "94adf9c7-d8bd-48e4-93d7-1a827c831595", "RY_ZTSD", pageIndex +1, pageSize, string.Format(" 1=1 {0}", q.ToWhereString()), "SDSJ desc", out resultCount);
                ds = ifs.PageQueryData(userName, userPassword, "94adf9c7-d8bd-48e4-93d7-1a827c831595", "dt", pageIndex + 1, pageSize, string.Format(" 1=1 {0}", q.ToWhereString()), "SDSJ desc", out resultCount);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page,"查询拆迁企业资质信息失败！",ex);
                return;
            }

            //绑定数据
            RadGrid1.VirtualItemCount = resultCount;
            RadGrid1.CurrentPageIndex = pageIndex;
            RadGrid1.PageSize = pageSize;
            RadGrid1.DataSource = ds;
            RadGrid1.DataBind();
        }

        ////导出审批列表
        //protected void ButtonOutput_Click(object sender, EventArgs e)
        //{
        //    if (RadGrid1.MasterTableView.Items.Count == 0)
        //    {
        //        UIHelp.layerAlert(Page, "没有可导出的数据！");
        //        return;
        //    }
        //    RefreshGrid(0, RadGrid1.VirtualItemCount);
        //    RadGrid1.ExportSettings.ExportOnlyData = true;
        //    RadGrid1.ExportSettings.OpenInNewWindow = true;
        //    RadGrid1.MasterTableView.ExportToExcel();
        //}

        ////格式化Excel
        //protected void RadGrid1_ExcelExportCellFormatting(object source, ExcelExportCellFormattingEventArgs e)
        //{
        //    //HeadCell
        //    GridItem item = e.Cell.Parent as GridItem;
        //    if (item.ItemIndex == 0 && e.FormattedColumn.UniqueName == "GWGZ")
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
    }
}
