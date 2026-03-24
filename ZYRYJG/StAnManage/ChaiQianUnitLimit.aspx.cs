using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Telerik.Web.UI;

namespace ZYRYJG.StAnManage
{
    public partial class ChaiQianUnitLimit : BasePage
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
            if (rdtxtQYMC.Text.Trim() != "")   //企业名称
            {
                q.Add(string.Format("GSMC like '%{0}%'", rdtxtQYMC.Text.Trim()));
            }

            if (RadTextBoxUNITCODE.Text.Trim() != "")   //机构代码
            {
                q.Add(string.Format("ZZJGDM like '%{0}%'", RadTextBoxUNITCODE.Text.Trim()));
            }
            ViewState["QueryParamOB"] = q;
            RefreshGrid(0);
        }

        protected void RadGrid1_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            RefreshGrid(e.NewPageIndex );
        }

        //刷新成绩Grid
        protected void RefreshGrid(int pageIndex)
        {
            System.Data.DataSet ds = null;
            int resultCount = 0;
            Synergy.Common.DESCrypto descObj = new Synergy.Common.DESCrypto();
            string userName = descObj.EncryptString("RTDL_RYKWXT");
            string userPassword = descObj.EncryptString("RTDL_2013");
            QueryParamOB q = ViewState["QueryParamOB"] as QueryParamOB;
            try
            {
                BaseDataService.InterFaceService ifs = new BaseDataService.InterFaceService();
                ds = ifs.PageQueryData(userName, userPassword, "e047d8f6-6427-4be9-b04b-749450ba4dea", "dt", pageIndex +1, RadGrid1.PageSize, string.Format(" 1=1 {0}", q.ToWhereString()), "", out resultCount);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page,"查询拆迁企业资质信息失败！",ex);
                return;
            }

            //绑定数据
            RadGrid1.VirtualItemCount = resultCount;
            RadGrid1.CurrentPageIndex = pageIndex;
            RadGrid1.DataSource = ds;
            RadGrid1.DataBind();
        }
    }
}
