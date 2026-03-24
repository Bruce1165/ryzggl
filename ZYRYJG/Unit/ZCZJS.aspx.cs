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
    public partial class ZCZJS : BasePage
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
                    q.Add(string.Format("([ZZJGDM] = '{0}' or [ZZJGDM] like '________{0}_' or [PYDW]='{1}')", ZZJGDM,UserName));
                }
                else
                {
                    q.Add(string.Format("([ZZJGDM] = '{0}' or [ZZJGDM] like '________{0}_' or [PYDW]='{1}')", Request.QueryString["a"],UserName));
                }
            }
            if (IfExistRoleID("3") == true || IfExistRoleID("7") == true)//区县
            {
                q.Add(string.Format("([Region] = '{0}')", Region));
            }
            if (RadTextBoxValue.Text.Trim() != "")//自定义查询项
            {
                q.Add(string.Format("{0} like '%{1}%'", RadComboBoxIten.SelectedValue, RadTextBoxValue.Text.Trim()));
            }
            //if (RadComboBoxPSN_RegisteType.SelectedValue != "")//申报事项
            //{
            //    q.Add(string.Format("PSN_RegisteType = '{0}'", RadComboBoxPSN_RegisteType.SelectedValue));
            //}

            ViewState["ZCZJS_QueryParamOB"] = q;

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridQY.CurrentPageIndex = 0;
            RadGridQY.DataSourceID = ObjectDataSource1.ID;
        }

        //导出数据
        protected void ButtonExportToExcel_Click(object sender, EventArgs e)
        {
            //检查临时目录
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"));


            QueryParamOB q = ViewState["ZCZJS_QueryParamOB"] as QueryParamOB;

            string saveFile = string.Format("~/UpLoad/CertifEnterApply/证书信息_{0}{1}.xls", UserID, DateTime.Now.ToString("yyyyMMddHHmmss"));//保存文件名
            try
            {

                string colHead = @"所在区县\姓名\身份证号\聘用单位\注册证号\注册专业\证书有效期";
                string colName = @"Region\XM\SFZH\PYDW\ZCZH\ZY\CONVERT(varchar(10), ZSYXQ, 20)";

                CommonDAL.OutputXls(MyWebConfig.DBOutputPath + Server.MapPath(saveFile).Substring(1), "DBO.jcsjk_zjs", q.ToWhereString(), "PYDW", colHead, colName);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "导出注册造价师证书信息失败！", ex);
                return;
            }

            List<ResultUrl> url = new List<ResultUrl>();
            url.Add(new ResultUrl("注册造价师下载", saveFile));
            UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);
        }
    }
}