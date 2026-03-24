using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Telerik.Web.UI;
using DataAccess;

namespace ZYRYJG.CertifManage
{
    public partial class CertifMoreAccepted : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ButtonSearch_Click(sender, e);
            }
        }

        protected string formatStauts(string status)
        {
            switch (status)
            {
                case "已申请":
                    return "未审核";
                case "已审核":
                case "已决定":
                    return "审核通过";
                case "退回修改":
                    return "退回修改";
                default:
                    return "";
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
            ObjectDataSource1.SelectParameters.Clear();
            QueryParamOB q = new QueryParamOB();
       

            if (rdtxtWorkerName.Text.Trim() != "")    //姓名
            {
                q.Add(string.Format("WorkerName like '%{0}%'", rdtxtWorkerName.Text.Trim()));
            }


            if (rdtxtZJHM.Text.Trim() != "")   //证件号码
            {
                q.Add(string.Format("WorkerCertificateCode like '%{0}%'", rdtxtZJHM.Text.Trim()));
            }

            if (RadioButtonListApplyStatus.SelectedItem.Value == "")//全部
            {
                q.Add(string.Format("(ApplyStatus = '{0}' or ApplyStatus = '{1}' or ApplyStatus = '{2}' or ApplyStatus = '{3}' )"
                    , EnumManager.CertificateMore.Applyed
                    , EnumManager.CertificateMore.Checked
                    , EnumManager.CertificateMore.Decided
                    , EnumManager.CertificateMore.SendBack
                    )); 
            }
            else if(RadioButtonListApplyStatus.SelectedItem.Value == EnumManager.CertificateMore.Checked)//审核通过
            {
                q.Add(string.Format("(ApplyStatus = '{0}' or ApplyStatus = '{1}')", EnumManager.CertificateMore.Checked, EnumManager.CertificateMore.Decided)); 
            }
            else//未审核，退回修改
            {
                q.Add(string.Format("ApplyStatus = '{0}'", RadioButtonListApplyStatus.SelectedItem.Value)); 
            }
           
            //if (RadioButtonListApplyStatus.SelectedItem.Value == "已申请")
            //{
            //    q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.CertificateMore.Applyed));   //已申请
            //             LabelTitle.Text = "待受理申请列表";
            //}
            //else
            //{
            //    q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.CertificateMore.Decided));   //受理过的

            //}

            if (RadDatePicker_GetDateStart.SelectedDate.HasValue)//受理时间段起始
            {
                q.Add(string.Format("CheckDate >='{0}'", RadDatePicker_GetDateStart.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }
            if (RadDatePicker_GetDateEnd.SelectedDate.HasValue)//受理时间段截止
            {
                q.Add(string.Format("CheckDate <'{0}'", RadDatePicker_GetDateEnd.SelectedDate.Value.AddDays(1).ToString("yyyy-MM-dd")));
            }


            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGrid1.CurrentPageIndex = 0;
            RadGrid1.DataSourceID = ObjectDataSource1.ID;
            ViewState["QueryParamOB"] = q;
        }
        
        //导出excel
        protected void ButtonExportToExcel_Click(object sender, EventArgs e)
        {
            if (RadGrid1.MasterTableView.VirtualItemCount == 0)
            {
                UIHelp.layerAlert(Page, "没有可导出的数据！");
                return;
            }

            QueryParamOB q = (QueryParamOB)ViewState["QueryParamOB"];
            string sortBy = "ApplyID desc";

            string saveFile = string.Format("~/UpLoad/CertifEnterApply/{0}_A本增发名单_{1}.xls",DateTime.Now.ToString("yyyyMMdd"), UserID);//保存文件名
            try
            {
                //检查临时目录
                if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"));

                string colHead = @"企业提交日期\受理日期\姓名\身份证号\现有A本单位名称\申请增发A本单位名称\状态\增发A本证书编号";
                string colName = @"CONVERT(varchar(10), [NewUnitCheckTime], 20)\CONVERT(varchar(10), [CheckDate], 20)\WorkerName\WorkerCertificateCode\UnitName\UnitNameMore\ApplyStatus\CertificateCodeMore";
                CommonDAL.OutputXls(MyWebConfig.DBOutputPath + Server.MapPath(saveFile).Substring(1)
                    , "DBO.CertificateMore"
                    , q.ToWhereString(), sortBy, colHead, colName);

            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "导出A本增发查询结果失败！", ex);
                return;
            }

            List<ResultUrl> url = new List<ResultUrl>();
            url.Add(new ResultUrl("A本增发查询结果下载", saveFile));
            UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);
        }
    }
}
