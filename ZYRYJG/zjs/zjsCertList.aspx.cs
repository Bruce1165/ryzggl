using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;
using System.IO;

namespace ZYRYJG.zjs
{
    public partial class zjsCertList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (IfExistRoleID("1")==true)
                {
                    RadGridQY.MasterTableView.Columns.FindByUniqueName("UpJsbTime").Visible = true;
                }
                ButtonSearch_Click(sender, e);
            }
        }

        //根据条件查询
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ObjectDataSource1.SelectParameters.Clear();
            var q = new QueryParamOB();

            switch (RadComboBoxPSN_RegisteType.SelectedValue)
            {
                case "":
                    break;
                case "未注销":
                    q.Add("[PSN_RegisteType] < '07'");//排除注销
                    break;
                case "已注销":
                    q.Add("[PSN_RegisteType] > '06'");//注销
                    break;
            }

            switch (RadComboBoxLockStatus.SelectedValue)
            {
                case "":
                    break;
                case "锁定":
                    q.Add("[PSN_CertificateNO] IN (select [PSN_CertificateNO] from [LockZJS] where LockStatus='加锁' and LockEndTime > getdate())");
                    break;
                case "未锁定":
                    q.Add("[PSN_CertificateNO] not IN (select [PSN_CertificateNO] from [LockZJS] where LockStatus='加锁' and LockEndTime > getdate())");//注销
                    break;
            }

            

            if (IfExistRoleID("2") == true)//企业
            {
                if (Request.QueryString["a"] == null)
                {
                    q.Add(string.Format("(ENT_OrganizationsCode = '{0}' or ENT_OrganizationsCode like '________{0}_')", ZZJGDM));
                }
                else
                {
                    q.Add(string.Format("(ENT_OrganizationsCode = '{0}' or ENT_OrganizationsCode like '________{0}_')", Request.QueryString["a"]));
                }
            }
            if (IfExistRoleID("3") == true || IfExistRoleID("7") == true)//区县
            {
                q.Add(string.Format("(ENT_City like '{0}%')", Region));
            }

            if (RadTextBoxValue.Text.Trim() != "")//自定义查询项
            {
                q.Add(string.Format("{0} like '%{1}%'", RadComboBoxIten.SelectedValue, RadTextBoxValue.Text.Trim().Replace("[","[[]")));
            }

            ViewState["zjs_QueryParamOB"] = q;

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridQY.CurrentPageIndex = 0;
            RadGridQY.DataSourceID = ObjectDataSource1.ID;
        }

        //导出数据
        protected void ButtonExportToExcel_Click(object sender, EventArgs e)
        {
            //检查临时目录
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"));

            QueryParamOB q = ViewState["zjs_QueryParamOB"] as QueryParamOB;
            string saveFile = string.Format("~/UpLoad/CertifEnterApply/二级造价工程师_{0}{1}.xls", UserID, DateTime.Now.ToString("yyyyMMddHHmmss"));//保存文件名
            try
            {
                string colHead = @"姓名\证件号码\企业名称\注册号\注册专业\有效期";
                string colName = @"PSN_Name\PSN_CertificateNO\ENT_Name\PSN_RegisterNO\PSN_RegisteProfession\CONVERT(varchar(10), PSN_CertificateValidity, 20)";

                CommonDAL.OutputXls(MyWebConfig.DBOutputPath + Server.MapPath(saveFile).Substring(1), "DBO.zjs_Certificate", q.ToWhereString(), "PSN_ServerID", colHead, colName);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "导出二级造价工程师导出失败！", ex);
                return;
            }

            List<ResultUrl> url = new List<ResultUrl>();
            url.Add(new ResultUrl("二级造价工程师下载", saveFile));
            UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);
        }
    }
}