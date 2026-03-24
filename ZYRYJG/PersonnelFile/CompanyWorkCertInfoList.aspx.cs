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

namespace ZYRYJG.PersonnelFile
{
    public partial class CompanyWorkCertInfoList:BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ////个人或企业
                //if (PersonType == 2 || PersonType == 3)
                //{
                //    RadComboBoxStatus.Items[0].Visible = false;
                //    RadComboBoxStatus.Items[2].Visible = false;
                //    RadComboBoxStatus.Items[3].Visible = false;
                //    RadComboBoxStatus.Items[4].Visible = false;
                //}
                ButtonSearch_Click(sender,e);

                BindTj(ZZJGDM);
            }
        }

        /// <summary>
        /// 绑定企业证书统计
        /// </summary>
        protected void BindTj(string UnitCode)
        {
            string sql = @"SELECT PostTypeName,count(*) sl
                          FROM [dbo].[CERTIFICATE]
                           where UNITCODE='{0}'
                           and [VALIDENDDATE] > getdate() and [STATUS] in('首次','进京变更','京内变更','续期','补办')
                         group  by PostTypeName";

            DataTable dt = CommonDAL.GetDataTable(string.Format(sql, UnitCode));

            //续期开放时间段提醒
        
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            int sum = 0;
              for (int i = 0; i < dt.Rows.Count; i++)
            {
                
                //if (sb.Length > 0) sb.Append("<br />");
                sum += Convert.ToInt32(dt.Rows[i]["sl"]);
                sb.Append(string.Format("，{0}{1}本", dt.Rows[i]["PostTypeName"], dt.Rows[i]["sl"]));
            }
              if (sb.Length > 0)
              {
                  sb.Insert(0, string.Format("统计：本企业持有有效证书共计{0}本，其中", sum));
                  sb.Append("。");
              }
           
            divBaseInfo.InnerHtml = sb.ToString();
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ObjectDataSource1.SelectParameters.Clear();
            QueryParamOB q = GetQueryParamOB();

            //q.Add(string.Format("UnitCode='{0}'", ZZJGDM));

            //if (txtCertificateCode.Text.Trim() != "")
            //{
            //    //证书编号
            //    q.Add(string.Format("CERTIFICATECODE like '%{0}%'", txtCertificateCode.Text.Trim()));
            //}
            //if (RadDatePicker_ValidEndDate.SelectedDate.HasValue)//有效期至
            //{
            //    q.Add(string.Format("ValidEndDate ='{0}'", RadDatePicker_ValidEndDate.SelectedDate.Value.ToString("yyyy-MM-dd")));
            //}
            //switch (RadComboBoxStatus.SelectedValue)//状态
            //{
            //    case "当前有效":
            //        q.Add(string.Format("ValidEndDate >='{0}' and ([STATUS] = '首次' or [STATUS] = '续期' or [STATUS] = '进京变更' or [STATUS] = '京内变更' or [STATUS] = '补办')", DateTime.Now.ToString("yyyy-MM-dd")));
            //        break;
            //    case "已过期":
            //        q.Add(string.Format("ValidEndDate <'{0}'", DateTime.Now.ToString("yyyy-MM-dd")));
            //        break;
            //    case "离京变更":
            //        q.Add(string.Format("[STATUS] = '{0}'", "离京变更"));
            //        break;
            //    case "注销":
            //        q.Add(string.Format("[STATUS] = '{0}'", "注销"));
            //        break;

            //}
            //if (txtWorkerName.Text.Trim() != "")
            //{
            //    //证书人姓名
            //    q.Add(string.Format("WorkerName like '%{0}%'", txtWorkerName.Text.Trim()));
            //}
            //if (txtWorkerCertificateCode.Text.Trim() != "")
            //{
            //    //证件号码
            //    q.Add(string.Format("WorkerCertificateCode like '%{0}%'", txtWorkerCertificateCode.Text.Trim()));
            //}
            //if (PostSelect1.PostTypeID != "")
            //{
            //    //岗位
            //    q.Add(string.Format("PostTypeID = {0}", PostSelect1.PostTypeID));
            //}
            //if (PostSelect1.PostID != "")
            //{
            //    //工种
            //    q.Add(string.Format("PostID = {0}", PostSelect1.PostID));
            //}
           
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
                if (PostSelect1.PostTypeID == "4")//职业技能列表及导出添加“技术等级”显示列
                {
                    colHead = @"姓名\证件号码\单位名称\组织机构代码\岗位类别\岗位工种\等级\证书编号\发证时间\有效期至\证书最后状态";
                    colName = @"WorkerName\WorkerCertificateCode\UnitName\UnitCode\POSTTYPENAME\POSTNAME\SkillLevel\CERTIFICATECODE\CONVERT(varchar(10), CONFERDATE, 20)\CONVERT(varchar(10), VALIDENDDATE, 20)\STATUS";

                }
                else
                {
                    colHead = @"姓名\证件号码\单位名称\组织机构代码\岗位类别\岗位工种\证书编号\发证时间\有效期至\证书最后状态";
                    colName = @"WorkerName\WorkerCertificateCode\UnitName\UnitCode\POSTTYPENAME\POSTNAME\CERTIFICATECODE\CONVERT(varchar(10), ConferDate, 20)\CONVERT(varchar(10), VALIDENDDATE, 20)\STATUS";
                }

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

            q.Add(string.Format("UnitCode='{0}'", ZZJGDM));

            if (RadTextBoxCertificateCode.Text.Trim() != "")
            {
                //证书编号
                q.Add(string.Format("CERTIFICATECODE like '%{0}%'", RadTextBoxCertificateCode.Text.Trim()));
            }
            if (RadDatePicker_ValidEndDate.SelectedDate.HasValue)//有效期至
            {
                q.Add(string.Format("ValidEndDate ='{0}'", RadDatePicker_ValidEndDate.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }
            switch (RadComboBoxStatus.SelectedValue)//状态
            {
                case "当前有效":
                    q.Add(string.Format("ValidEndDate >='{0}' and ([STATUS] = '首次' or [STATUS] = '续期' or [STATUS] = '进京变更' or [STATUS] = '京内变更' or [STATUS] = '补办')", DateTime.Now.ToString("yyyy-MM-dd")));
                    break;
                case "已过期":
                    q.Add(string.Format("ValidEndDate <'{0}'", DateTime.Now.ToString("yyyy-MM-dd")));
                    break;
                case "离京变更":
                    q.Add(string.Format("[STATUS] = '{0}'", "离京变更"));
                    break;
                case "注销":
                    q.Add(string.Format("[STATUS] = '{0}'", "注销"));
                    break;

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
            if (PostSelect1.PostTypeID != "")
            {
                //岗位
                q.Add(string.Format("PostTypeID = {0}", PostSelect1.PostTypeID));
            }
            if (PostSelect1.PostID != "")
            {
                //工种
                q.Add(string.Format("PostID = {0}", PostSelect1.PostID));
            }
            return q;
        }
    }
}
