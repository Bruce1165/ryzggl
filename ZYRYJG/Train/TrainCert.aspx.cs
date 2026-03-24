using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Model;
using DataAccess;
using System.Data;
using Utility;
using System.IO;

namespace ZYRYJG.Train
{
    public partial class TrainCert : BasePage
    {
        /// <summary>
        /// 当前培训点信息
        /// </summary>
        protected TrainUnitMDL curTrainUnit
        {
            get { return ViewState["TrainUnitMDL"] == null ? null : (ViewState["TrainUnitMDL"] as TrainUnitMDL); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TrainUnitMDL _TrainUnitMDL = TrainUnitDAL.GetObjectBySHTYXYDM(SHTJXYDM);
                if (_TrainUnitMDL != null)
                {
                    ViewState["TrainUnitMDL"] = _TrainUnitMDL;
                }

                //根据培训点绑定可创建考试计划的工种
                Dictionary<string, string> postFilterString = new Dictionary<string, string>();
                postFilterString.Add("4000", string.Format("PostID in({0})", _TrainUnitMDL.PostSet));//
                PostSelect1.PostFilterString = postFilterString;

                //初始化岗位类别
                for (int i = 1; i < 6; i++)
                {
                    PostSelect1.RadComboBoxPostTypeID.Items.FindItemByValue(i.ToString()).Remove();
                }
                PostSelect1.RadComboBoxPostTypeID.Items.FindItemByText("请选择").Remove();
                PostSelect1.PostTypeID = "4000";
                PostSelect1.HideRadComboBoxPostType();

                for (int i = 2023; i <= (DateTime.Now.Year + 1); i++)
                {
                    RadComboBoxYear.Items.Insert(0, new RadComboBoxItem(i.ToString(), i.ToString()));
                }
                RadComboBoxYear.Items.Insert(0, new RadComboBoxItem("全部", ""));
                RadComboBoxYear.Items.FindItemByValue(DateTime.Now.Year.ToString()).Selected = true;
               

                ButtonSearch_Click(sender,e);

                //BindTj(ZZJGDM);
            }
        }

//        /// <summary>
//        /// 绑定企业证书统计
//        /// </summary>
//        protected void BindTj(string UnitCode)
//        {
//            string sql = @"SELECT PostTypeName,count(*) sl
//                          FROM [dbo].[CERTIFICATE]
//                           where UNITCODE='{0}'
//                           and [VALIDENDDATE] > getdate() and [STATUS] in('首次','进京变更','京内变更','续期','补办')
//                         group  by PostTypeName";

//            DataTable dt = CommonDAL.GetDataTable(string.Format(sql, UnitCode));

//            //续期开放时间段提醒
        
//            System.Text.StringBuilder sb = new System.Text.StringBuilder();
//            int sum = 0;
//              for (int i = 0; i < dt.Rows.Count; i++)
//            {
                
//                //if (sb.Length > 0) sb.Append("<br />");
//                sum += Convert.ToInt32(dt.Rows[i]["sl"]);
//                sb.Append(string.Format("，{0}{1}本", dt.Rows[i]["PostTypeName"], dt.Rows[i]["sl"]));
//            }
//              if (sb.Length > 0)
//              {
//                  sb.Insert(0, string.Format("统计：本企业持有有效证书共计{0}本，其中", sum));
//                  sb.Append("。");
//              }
           
//            divBaseInfo.InnerHtml = sb.ToString();
//        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ObjectDataSource1.SelectParameters.Clear();
            QueryParamOB q = GetQueryParamOB();
           
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridApply.CurrentPageIndex = 0;
            RadGridApply.DataSourceID = ObjectDataSource1.ID;
        }

        //导出csv
        protected void ButtonExportToExcel_Click(object sender, EventArgs e)
        {
            QueryParamOB q = GetQueryParamOB();

            string saveFile = string.Format("~/UpLoad/CertifEnterApply/证书信息_{0}{1}.xls", UnitID, DateTime.Now.ToString("yyyyMMddHHmmss"));//保存文件名
            try
            {
                //检查临时目录
                if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"));

                string colHead = "";
                string colName = "";

                //导出数据到数据库服务器
                colHead = @"姓名\证件号码\岗位工种\等级\证书编号\发证时间\证书最后状态";
                colName = @"WorkerName\WorkerCertificateCode\POSTNAME\SkillLevel\CERTIFICATECODE\CONVERT(varchar(10), CONFERDATE, 20)\STATUS";

                CommonDAL.OutputXls(MyWebConfig.DBOutputPath + Server.MapPath(saveFile).Substring(1), "DBO.CERTIFICATE", q.ToWhereString(), "CertificateID", colHead, colName);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "导出从业人员证书信息失败！", ex);
                return;
            }

            List<ResultUrl> url = new List<ResultUrl>();
            url.Add(new ResultUrl("证书信息下载", saveFile));
            UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);
        }

        private QueryParamOB GetQueryParamOB()
        {
            QueryParamOB q = new QueryParamOB();

            q.Add(string.Format("PostTypeID =4000 and TRAINUNITNAME='{0}'", curTrainUnit.TrainUnitName));

            if (RadTextBoxCertificateCode.Text.Trim() != "")
            {
                //证书编号
                q.Add(string.Format("CERTIFICATECODE like '%{0}%'", RadTextBoxCertificateCode.Text.Trim()));
            }
           
            if (RadTextBoxWorkerName.Text.Trim() != "")
            {
                //证书人姓名
                q.Add(string.Format("WorkerName like '%{0}%'", RadTextBoxWorkerName.Text.Trim()));
            }
            if (RadTextBoxWorkerCertificateCode.Text.Trim() != "")
            {
                //证件号码
                q.Add(string.Format("WorkerCertificateCode like '%{0}%'", RadTextBoxWorkerCertificateCode.Text.Trim()));
            }
           
            if (PostSelect1.PostID != "")
            {
                //工种
                q.Add(string.Format("PostID = {0}", PostSelect1.PostID));
            }

            //发证时间
            if (RadComboBoxYear.SelectedValue != "") q.Add(string.Format("DATEPART(year,CONFERDATE) = {0}", RadComboBoxYear.SelectedValue));//年
            if (RadComboBoxMonth.SelectedValue != "") q.Add(string.Format("DATEPART(month,CONFERDATE) = {0}", RadComboBoxMonth.SelectedValue));//月

            return q;
        }
    }
}
