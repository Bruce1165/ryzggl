using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;
using System.IO;

namespace ZYRYJG.Unit
{
    public partial class ZCJLS : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
            if (IfExistRoleID("2") == true)//企业
            {
                if (Request.QueryString["a"] == null)
                {
                    q.Add(string.Format("([组织机构代码] = '{0}' or [组织机构代码] like '________{0}_')", ZZJGDM));
                }
                else
                {
                    q.Add(string.Format("([组织机构代码] = '{0}' or [组织机构代码] like '________{0}_')", Request.QueryString["a"].ToString()));
                }
              
            }
            if (IfExistRoleID("3") == true || IfExistRoleID("7") == true)//区县
            {
                q.Add(string.Format("([所在区县] = '{0}')", Region));
            }
            if (RadTextBoxValue.Text.Trim() != "")//自定义查询项
            {
                q.Add(string.Format("{0} like '%{1}%'", RadComboBoxIten.SelectedValue, RadTextBoxValue.Text.Trim()));
            }
            //if (RadComboBoxPSN_RegisteType.SelectedValue != "")//申报事项
            //{
            //    q.Add(string.Format("PSN_RegisteType = '{0}'", RadComboBoxPSN_RegisteType.SelectedValue));
            //}
            ViewState["ZCJLS_QueryParamOB"] = q;

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridQY.CurrentPageIndex = 0;
            RadGridQY.DataSourceID = ObjectDataSource1.ID;
        }

        //导出数据
        protected void ButtonExportToExcel_Click(object sender, EventArgs e)
        {
            //检查临时目录
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"));


            QueryParamOB q = ViewState["ZCJLS_QueryParamOB"] as QueryParamOB;

            string saveFile = string.Format("~/UpLoad/CertifEnterApply/证书信息_{0}{1}.xls", UserID, DateTime.Now.ToString("yyyyMMddHHmmss"));//保存文件名
            try
            {

                string colHead = @"所在区县\姓名\证件号码\聘用单位\执业资格证书编号\注册号\注册证书编号\注册专业\发证日期\注册有效期";
                string colName = @"所在区县\姓名\证件号\聘用单位\执业资格证书编号\注册号\注册证书编号\注册专业1 + (case when 注册专业2 is null or 注册专业2='无' then '' else '；' +注册专业2 end )\CONVERT(varchar(10), 发证日期, 20)\CONVERT(varchar(10), 注册有效期, 20)";

                CommonDAL.OutputXls(MyWebConfig.DBOutputPath + Server.MapPath(saveFile).Substring(1), "DBO.jcsjk_jls", q.ToWhereString(), "聘用单位", colHead, colName);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "导出注册监理师证书信息失败！", ex);
                return;
            }

            List<ResultUrl> url = new List<ResultUrl>();
            url.Add(new ResultUrl("注册监理师下载", saveFile));
            UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);
        }
    }
}