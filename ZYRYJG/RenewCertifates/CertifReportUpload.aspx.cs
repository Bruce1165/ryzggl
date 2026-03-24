using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Model;
using DataAccess;
using System.Data;
using Utility;
using System.IO;

namespace ZYRYJG.RenewCertifates
{
    public partial class CertifReportUpload : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "CertifReport.aspx";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (string.IsNullOrEmpty(Request["r"]) == false)
                {
                    ViewState["ReportCode"] = Request["r"];//汇总批次号

                    DataTable dt = CertificateContinueDAL.GetReportList(0, 1, string.Format(" and ReportCode='{0}'", Request["r"]), "ReportCode");
                    if (dt.Rows.Count > 0)
                    {
                        LabelCertCount.Text = dt.Rows[0]["CertCount"].ToString();//证书数量
                        LabelChechUnit.Text = dt.Rows[0]["FIRSTCHECKUNITNAME"].ToString();//初审单位
                        LabelCheckDate.Text = Convert.ToDateTime(dt.Rows[0]["FirstCheckStartDate"]).ToString("yyyy-MM-dd") == Convert.ToDateTime(dt.Rows[0]["FirstCheckEndDate"]).ToString("yyyy-MM-dd") ? Convert.ToDateTime(dt.Rows[0]["FirstCheckStartDate"]).ToString("yyyy.MM.dd") : string.Format("{0} - {1}", Convert.ToDateTime(dt.Rows[0]["FirstCheckStartDate"]).ToString("yyyy.MM.dd"), Convert.ToDateTime(dt.Rows[0]["FirstCheckEndDate"]).ToString("yyyy.MM.dd"));//初审时间

                        LabelPostTypeName.Text = PostTypeName(dt.Rows[0]["PostTypeID"].ToString());
                        LabelReportCode.Text = dt.Rows[0]["ReportCode"].ToString();
                        LabelReportDate.Text =  Convert.ToDateTime(dt.Rows[0]["ReportDate"]).ToString("yyyy-MM-dd");
                    }
                }

            }
        }

        protected string PostTypeName(string PostTypeID)
        {
            switch (PostTypeID)
            {
                case "1":
                    return "安全生产考核三类人员";
                case "2":
                    return "建筑施工特种作业";
                case "3":
                    return "造价员";
                case "4":
                    return "建设职业技能岗位";
                case "5":
                    return "关键岗位专业技术管理人员";
                default:
                    return "未知";
            }
        }
        //上传扫描件
        protected void ButtonUpload_Click(object sender, EventArgs e)
        {
            bool rtn = false;
            ////存放路径
            //if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/XQReport/")))
            //{
            //    Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/XQReport/"));
            //}

            if (RadUploadReportImg.UploadedFiles.Count > 0)
            {
                if (RadUploadReportImg.UploadedFiles[0].GetExtension().ToLower() != ".jpg")
                {
                    UIHelp.layerAlert(Page, "扫描件格式不正确！只能是有jpg格式图片");
                    return;
                }
                if (RadUploadReportImg.UploadedFiles[0].ContentLength > 512000)
                {
                    UIHelp.layerAlert(Page, "扫描件大小不能超过500k！");
                    return;
                }
                if (RadUploadReportImg.UploadedFiles[0].ContentLength < 200)
                {
                    UIHelp.layerAlert(Page, "扫描件大小存在问题，请检查照片是否有效！");
                    return;
                }

                RadUploadReportImg.UploadedFiles[0].SaveAs(Server.MapPath(string.Format("~/Upload/XQReport/{0}.jpg", LabelReportCode.Text.Trim())));
                rtn = true;
              
            }

            if (RadUploadExcel.UploadedFiles.Count > 0)
            {
                if (RadUploadExcel.UploadedFiles[0].GetExtension().ToLower() != ".xls" && RadUploadExcel.UploadedFiles[0].GetExtension().ToLower() != ".xlsx")
                {
                    UIHelp.layerAlert(Page, "上传Excel格式有误！");
                    return;
                }
                if (RadUploadExcel.UploadedFiles[0].ContentLength > 5120000)
                {
                    UIHelp.layerAlert(Page, "扫描件大小不能超过5M！");
                    return;
                }

                RadUploadExcel.UploadedFiles[0].SaveAs(Server.MapPath(string.Format("~/Upload/XQReport/{0}.xls", LabelReportCode.Text.Trim())));
                rtn = true;
              
               
            }

            if (rtn==true)
            {
                UIHelp.layerAlert(Page, "上传附件成功！",6,3000);
            }
           
        }

    }
}
