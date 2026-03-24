using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;

namespace ZYRYJG.CheckMgr
{
    public partial class RandomCheckBase : BasePage
    {       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RadNumericTextBoxSourceYear.Value = DateTime.Now.Year;
                ButtonSearch_Click(sender, e);
            }
        }

        //变换人员类型
        protected void RadComboBoxPersonType_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            switch (RadComboBoxPersonType.SelectedValue)
            {
                case ""://全部
                    RadComboBoxPSN_RegisteType.Visible = true;
                    RadComboBoxEJZJGCS.Visible = false;
                    RadComboBox_CongYeApplyType.Visible = false;
                    break;
                case "二级注册建造师":
                    RadComboBoxPSN_RegisteType.Visible = true;
                    RadComboBoxEJZJGCS.Visible = false;
                    RadComboBox_CongYeApplyType.Visible = false;
                    break;
                case "二级造价工程师":
                    RadComboBoxPSN_RegisteType.Visible = false;
                    RadComboBoxEJZJGCS.Visible = true;
                    RadComboBox_CongYeApplyType.Visible = false;
                    break;
                default://从业人员（三类人、特种作业）
                    RadComboBoxPSN_RegisteType.Visible = false;
                    RadComboBoxEJZJGCS.Visible = false;
                    RadComboBox_CongYeApplyType.Visible = true;
                    break;
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
            RadGridQY.CurrentPageIndex = 0;

            //抽查年度
            q.Add(string.Format("[CheckYear] = {0}", RadNumericTextBoxSourceYear.Value));

            //人员类型
            if (RadComboBoxPersonType.SelectedValue != "")
            {
                q.Add(string.Format("[POSTTYPENAME] like '{0}%'", RadComboBoxPersonType.SelectedValue));
            }

            switch (RadComboBoxPersonType.SelectedValue)
            {
                case "二级注册建造师":
                    //业务类型
                    if (RadComboBoxPSN_RegisteType.SelectedValue != "")
                    {
                        q.Add(string.Format("[ApplyType] like '{0}%'", RadComboBoxPSN_RegisteType.SelectedValue));
                    }
                    break;
                case "二级造价工程师":
                    //业务类型
                    if (RadComboBoxEJZJGCS.SelectedValue != "")
                    {
                        q.Add(string.Format("[ApplyType] like '{0}%'", RadComboBoxEJZJGCS.SelectedValue));
                    }
                    break;
                default://从业人员（三类人、特种作业）                  
                    //业务类型
                    if (RadComboBox_CongYeApplyType.Visible==true && RadComboBox_CongYeApplyType.SelectedValue != "")
                    {
                        q.Add(string.Format("[ApplyType] like '{0}%'", RadComboBox_CongYeApplyType.SelectedValue));
                    }
                    else if (RadComboBoxPSN_RegisteType.Visible == true && RadComboBoxPSN_RegisteType.SelectedValue != "")
                    {
                        q.Add(string.Format("[ApplyType] like '{0}%'", RadComboBoxPSN_RegisteType.SelectedValue));
                    }
                    else if (RadComboBoxEJZJGCS.Visible == true && RadComboBoxEJZJGCS.SelectedValue != "")
                    {
                        q.Add(string.Format("[ApplyType] like '{0}%'", RadComboBoxEJZJGCS.SelectedValue));
                    }
                    break;
            }

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridQY.DataSourceID = ObjectDataSource1.ID;

            ViewState["CheckObject_QueryParamOB"] = q;
        }

        //导出数据
        protected void ButtonExportToExcel_Click(object sender, EventArgs e)
        {
            //检查临时目录
            if (!System.IO.Directory.Exists(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"));


            QueryParamOB q = ViewState["CheckObject_QueryParamOB"] as QueryParamOB;

            string saveFile = string.Format("~/UpLoad/CertifEnterApply/{1}{2}检查对象_{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmss"), RadNumericTextBoxSourceYear.Value, RadComboBoxPersonType.SelectedValue);//保存文件名
            string colHead = @"人员类型\抽查年度\姓名\证件号码\企业名称\证书编号\专业及有效期\申报事项\申报日期\办结日期";

            string colName = @"PostTypeName\CheckYear\PSN_Name\PSN_CertificateNO\ENT_Name\PSN_RegisterNO\ProfessionWithValid\ApplyType\CONVERT(varchar(10), ApplyTime, 20)\CONVERT(varchar(10), NoticeDate, 20)";

            try
            {        
                CommonDAL.OutputXls(MyWebConfig.DBOutputPath + Server.MapPath(saveFile).Substring(1), "DBO.[CheckObject]", q.ToWhereString(), "CheckYear desc,PSN_RegisterNo,CheckID", colHead, colName);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "导出检查对象信息失败！", ex);
                return;
            }

            List<ResultUrl> url = new List<ResultUrl>();
            url.Add(new ResultUrl(string.Format("{0}{1}检查对象下载", RadNumericTextBoxSourceYear.Value, RadComboBoxPersonType.SelectedValue), saveFile));
            UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);
        }

        //新建筛查
        protected void ButtonNew_Click(object sender, EventArgs e)
        {

        }
    }
}