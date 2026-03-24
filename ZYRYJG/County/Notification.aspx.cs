using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Model;
using DataAccess;

namespace ZYRYJG.County
{
    //发送通知
    public partial class Notification : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                BindDate();
            }
        }
        private void BindDate()
        {
            ObjectDataSource1.SelectParameters.Clear();
            string type = RadTextBoxValue.Text.Trim();
            var q = new QueryParamOB();
            if (IfExistRoleID("3") == true)//区县
            {
                q.Add(string.Format("ENT_City like '{0}%'", Region));
            }
            if (RadioButtonListReportStatus.SelectedValue == "未发送")
                q.Add("GetDateTime IS NULL");
            else
                q.Add("GetDateTime IS NOT NULL");
            if (RadComboBoxIten.SelectedValue != "全部")
                q.Add(string.Format("ApplyType = '{0}'", RadComboBoxIten.SelectedValue));
            if (RadTextBoxValue.Text.Trim() != "")//自定义查询项
            {
                //q.Add(string.Format("PSN_CertificateNO = '{0}'", RadTextBoxValue.Text.Trim()));
                q.Add(string.Format("NoticeCode like '%{0}%'", RadTextBoxValue.Text.Trim()));
            }
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridFSTZ.CurrentPageIndex = 0;
            RadGridFSTZ.DataSourceID = ObjectDataSource1.ID;
        }
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            BindDate();
        }
        //发送通知
        protected void RadGridFSTZ_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if(e.CommandName=="Top")
            {
               try
               {
                   //发送通知按钮
                   CommonDAL.ExecSQL(string.Format(@"INSERT INTO [dbo].[ApplyNews]([ID],[ApplyID],[PSN_Name],[PSN_CertificateNO] ,[PSN_RegisterNo] ,[ApplyType],[SFCK],[ENT_OrganizationsCode],[ENT_City])
                    SELECT NEWID(),[ApplyID],[PSN_Name],[PSN_CertificateNO],[PSN_RegisterNo],[ApplyType],0,[ENT_OrganizationsCode],[ENT_City]
                    FROM APPLY WHERE NoticeCode='{0}' AND ENT_City like '" + Region + "%'", RadGridFSTZ.MasterTableView.DataKeyValues[e.Item.ItemIndex]["NoticeCode"].ToString()));
                   BindDate();
                   UIHelp.layerAlert(Page, "发送注册办结通知成功！", 6, 2000);
               }
                catch(Exception ex)
               {
                   UIHelp.WriteErrorLog(Page, "发送注册办结通知失败！", ex);
               }
            }
        }

        protected void RadioButtonListReportStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDate();
        }
    }
}