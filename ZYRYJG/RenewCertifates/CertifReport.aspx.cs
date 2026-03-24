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
    public partial class CertifReport : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RadDatePickerReportDateStart.DbSelectedDate = DateTime.Now.Date.AddMonths(-2);
                RadDatePickerReportDateEnd.DbSelectedDate = DateTime.Now.Date;
                switch (string.IsNullOrEmpty(Request["o"]) ? "1" : Request["o"].ToString())
                {
                    case "1":
                        LabelAccepy.Text = "三类人员";
                        break;
                    case "2":
                        LabelAccepy.Text = "特种作业";
                        break;
                    case "3":
                        LabelAccepy.Text = "造价员";
                        break;
                }
                PostSelect1.PostTypeID = string.IsNullOrEmpty(Request["o"]) ? "1" : Request["o"].ToString();
                if (PersonType == 1 || PersonType == 6)
                {
                    PostSelect1.LockPostTypeID();
                }

                ButtonSearch_Click(sender, e);
            }
        }

        protected string PostName(string PostTypeID)
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

        //根据条件查询
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }

            QueryParamOB q = new QueryParamOB();
            //q.Add(string.Format("FIRSTCHECKUNITNAME = '{0}'", PersonName));
            q.Add(string.Format("FirstCheckUnitCode ='{0}'", RegionCode));

            //审核时间
            if (RadDatePickerCheckStartDate.SelectedDate.HasValue == true)
            {
                q.Add(string.Format("FirstCheckStartDate >= '{0}' ", RadDatePickerCheckStartDate.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }
            if (RadDatePickerCheckEndDate.SelectedDate.HasValue == true)
            {
                q.Add(string.Format("FirstCheckStartDate <= '{0} 23:59:59' ", RadDatePickerCheckEndDate.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }

            //汇总时间
            q.Add(string.Format("ReportDate between '{0}' and '{1} 23:59:59'"
                , RadDatePickerReportDateStart.SelectedDate.HasValue ? RadDatePickerReportDateStart.SelectedDate.Value.ToString("yyyy-MM-dd") : "2018-01-01"
                , RadDatePickerReportDateEnd.SelectedDate.HasValue ? RadDatePickerReportDateEnd.SelectedDate.Value.ToString("yyyy-MM-dd") : "2050-01-01"));

            //汇总编号
            if (RadTextBoxReportCode.Text.Trim() != "")
            {
                q.Add(string.Format("ReportCode = '{0}'", RadTextBoxReportCode.Text.Trim()));
            }

            //岗位类别
            if (PostSelect1.PostTypeID != "")
            {
                q.Add(string.Format("PostTypeID = {0}", PostSelect1.PostTypeID));
            }


            ObjectDataSource1.SelectParameters.Clear();
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());


            RadGridReport.CurrentPageIndex = 0;
            RadGridReport.DataSourceID = ObjectDataSource1.ID;

        }

        /// <summary>
        /// 是否上传了汇总扫描件
        /// </summary>
        /// <param name="ReportCode">汇总编号</param>
        /// <returns>true：已经上传，false：尚未上传</returns>
        protected bool IfUploadReportImg(string ReportCode)
        {
            bool rtn = false;
            try
            {
                rtn = File.Exists(Server.MapPath(string.Format("~/Upload/XQReport/{0}.jpg", ReportCode)));
                if (rtn == false) return false;
                rtn = File.Exists(Server.MapPath(string.Format("~/Upload/XQReport/{0}.xls", ReportCode)));
                if (rtn == true) return true;
                rtn = File.Exists(Server.MapPath(string.Format("~/Upload/XQReport/{0}.xlsx", ReportCode)));
                return rtn;
            }
            catch { return false; }
        }

        /// <summary>
        /// 显示上传的汇总附件
        /// </summary>
        /// <param name="ReportCode">汇总批次号</param>
        /// <returns></returns>
        protected string showFJ(string ReportCode)
        {
            //服务器类型
            string serverType = System.Configuration.ConfigurationManager.AppSettings["serverType"];

            //外网
            string ww = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["ww"]);

            //专网
            string zw = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["zw"]);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            string radom = Utility.Cryptography.Encrypt(string.Format("{0},{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "synergyFileAccess"));

            bool b_file = File.Exists(Server.MapPath(string.Format("~/Upload/XQReport/{0}.jpg", ReportCode)));
            if (b_file == true)
            {
                sb.Append(string.Format("<a class=\"link_edit\" target=\"_blank\" href=\"../Upload/XQReport/{0}.jpg?read={1}\">【签字扫描件】</a><br/>", ReportCode,radom));
            }
            else
            {
                if (serverType == "ww")
                {
                    if (UIHelp.IfUriExist(string.Format("{0}/Upload/XQReport/{1}.jpg", zw, ReportCode)) == true)
                    {
                        sb.Append(string.Format("<a class=\"link_edit\" target=\"_blank\" href=\"{0}/Upload/XQReport/{1}.jpg?read={2}\">【签字扫描件】</a><br/>", zw, ReportCode, radom));

                    }
                }
                else
                {
                    if (UIHelp.IfUriExist(string.Format("{0}/Upload/XQReport/{1}.jpg", ww, ReportCode)) == true)
                    {
                        sb.Append(string.Format("<a class=\"link_edit\" target=\"_blank\" href=\"{0}/Upload/XQReport/{1}.jpg?read={2}\">【签字扫描件】</a><br/>", ww, ReportCode, radom));

                    }
                }
            }

            b_file = File.Exists(Server.MapPath(string.Format("~/Upload/XQReport/{0}.xls", ReportCode)));
            if (b_file == true)
            {
                sb.Append(string.Format("<a class=\"link_edit\" target=\"_blank\" href=\"../Upload/XQReport/{0}.xls?read={1}\">【汇总明细】</a>", ReportCode, radom));
                return sb.ToString();
            }
            else
            {
                if (serverType == "ww")
                {
                    if (UIHelp.IfUriExist(string.Format("{0}/Upload/XQReport/{1}.xls", zw, ReportCode)) == true)
                    {
                        sb.Append(string.Format("<a class=\"link_edit\" target=\"_blank\" href=\"{0}/Upload/XQReport/{1}.xls?read={2}\">【汇总明细】</a><br/>", zw, ReportCode, radom));
                        return sb.ToString();
                    }
                }
                else
                {
                    if (UIHelp.IfUriExist(string.Format("{0}/Upload/XQReport/{1}.xls", ww, ReportCode)) == true)
                    {
                        sb.Append(string.Format("<a class=\"link_edit\" target=\"_blank\" href=\"{0}/Upload/XQReport/{1}.xls?read={2}\">【汇总明细】</a><br/>", ww, ReportCode, radom));
                        return sb.ToString();
                    }
                }
            }

            b_file = File.Exists(Server.MapPath(string.Format("~/Upload/XQReport/{0}.xlsx", ReportCode)));
            if (b_file == true)
            {
                sb.Append(string.Format("<a class=\"link_edit\" target=\"_blank\" href=\"../Upload/XQReport/{0}.xlsx?read={1}\">【汇总明细】</a>", ReportCode, radom));
                return sb.ToString();
            }
            else
            {
                if (serverType == "ww")
                {
                    if (UIHelp.IfUriExist(string.Format("{0}/Upload/XQReport/{1}.xlsx", zw, ReportCode)) == true)
                    {
                        sb.Append(string.Format("<a class=\"link_edit\" target=\"_blank\" href=\"{0}/Upload/XQReport/{1}.xlsx?read={2}\">【汇总明细】</a><br/>", zw, ReportCode, radom));
                        return sb.ToString();
                    }
                }
                else
                {
                    if (UIHelp.IfUriExist(string.Format("{0}/Upload/XQReport/{1}.xlsx", ww, ReportCode)) == true)
                    {
                        sb.Append(string.Format("<a class=\"link_edit\" target=\"_blank\" href=\"{0}/Upload/XQReport/{1}.xlsx?read={2}\">【汇总明细】</a><br/>", ww, ReportCode, radom));
                        return sb.ToString();
                    }
                }
            }
            return sb.ToString();
        }

        ///// <summary>
        ///// 显示上传的汇总附件
        ///// </summary>
        ///// <param name="ReportCode">汇总批次号</param>
        ///// <returns></returns>
        //protected string showFJ(string ReportCode)
        //{
        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();

        //    bool b_file = File.Exists(Server.MapPath(string.Format("~/Upload/XQReport/{0}.jpg", ReportCode)));
        //    if(b_file == true)
        //    {
        //        sb.Append(string.Format("<a class=\"link_edit\" target=\"_blank\" href=\"../Upload/XQReport/{0}.jpg\">【签字扫描件】</a><br/>", ReportCode));
        //    }
        //    b_file = File.Exists(Server.MapPath(string.Format("~/Upload/XQReport/{0}.xls", ReportCode)));
        //    if (b_file == true)
        //    {
        //        sb.Append(string.Format("<a class=\"link_edit\" target=\"_blank\" href=\"../Upload/XQReport/{0}.xls\">【汇总明细】</a>", ReportCode));
        //        return sb.ToString();
        //    }
        //    b_file = File.Exists(Server.MapPath(string.Format("~/Upload/XQReport/{0}.xlsx", ReportCode)));
        //    if (b_file == true)
        //    {
        //        sb.Append(string.Format("<a class=\"link_edit\" target=\"_blank\" href=\"../Upload/XQReport/{0}.xlsx\">【汇总明细】</a>", ReportCode));
        //        return sb.ToString();
        //    }
        //    return sb.ToString();
        //}

        protected void RadGridReport_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                string pcode = RadGridReport.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ReportCode"].ToString();
                switch (e.CommandName)
                {
                    case "delete"://删除汇总
                        try
                        {
                            //取消已分配批次号
                            CertificateContinueDAL.ReportCancel(null, pcode);
                            RadGridReport.Rebind();
                            UIHelp.WriteOperateLog(PersonName, UserID, "取消续期初审汇总", string.Format("汇总批次号：“{0}”。", pcode));
                        }
                        catch (Exception ex)
                        {
                            UIHelp.WriteErrorLog(Page, "取消续期初审汇总失败", ex);
                        }
                        break;
                    case "report"://上报
                        try
                        {
                            CertificateContinueDAL.ReportCommit(RadGridReport.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ReportCode"].ToString(), true, PersonName);
                            RadGridReport.Rebind();
                            UIHelp.WriteOperateLog(PersonName, UserID, "上报续期初审汇总", string.Format("汇总批次号：“{0}”。", pcode));
                        }
                        catch (Exception ex)
                        {
                            UIHelp.WriteErrorLog(Page, "上报续期初审汇总失败", ex);
                        }
                        break;
                    case "Cancelreport"://取消上报
                        try
                        {
                            bool Ifchecked = CertificateContinueDAL.IfReportChecked(RadGridReport.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ReportCode"].ToString());
                            if (Ifchecked == true)
                            {
                                UIHelp.layerAlert(Page, "该批次汇总数据市建委已经审批，无法取消申报！");
                                return;
                            }
                            CertificateContinueDAL.ReportCommit(RadGridReport.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ReportCode"].ToString(), false, PersonName);
                            RadGridReport.Rebind();
                            UIHelp.WriteOperateLog(PersonName, UserID, "取消上报续期初审汇总", string.Format("汇总批次号：“{0}”。", pcode));
                        }
                        catch (Exception ex)
                        {
                            UIHelp.WriteErrorLog(Page, "取消上报续期初审汇总失败", ex);
                        }
                        break;
                    case "OutPutReport"://导出汇总签字表
                        CheckSaveDirectory();
                        System.Collections.Generic.Dictionary<string, string> list = new Dictionary<string, string>();
                        list.Add("ReportCode", RadGridReport.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ReportCode"].ToString());//汇总批次号
                        list.Add("ReportDate", Convert.ToDateTime(RadGridReport.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ReportDate"]).ToString("yyyy-MM-dd"));//汇总日期
                        list.Add("PostTypeName", PostName(RadGridReport.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PostTypeID"].ToString()));//岗位类别
                        list.Add("CertCount", RadGridReport.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CertCount"].ToString());//证书数量
                        list.Add("ChechUnit", RadGridReport.MasterTableView.DataKeyValues[e.Item.ItemIndex]["FIRSTCHECKUNITNAME"].ToString());//初审单位名称
                        list.Add("CheckDate", Convert.ToDateTime(RadGridReport.MasterTableView.DataKeyValues[e.Item.ItemIndex]["FirstCheckStartDate"]).ToString("yyyy-MM-dd") == Convert.ToDateTime(RadGridReport.MasterTableView.DataKeyValues[e.Item.ItemIndex]["FirstCheckEndDate"]).ToString("yyyy-MM-dd") ? Convert.ToDateTime(RadGridReport.MasterTableView.DataKeyValues[e.Item.ItemIndex]["FirstCheckStartDate"]).ToString("yyyy.MM.dd") : string.Format("{0} - {1}", Convert.ToDateTime(RadGridReport.MasterTableView.DataKeyValues[e.Item.ItemIndex]["FirstCheckStartDate"]).ToString("yyyy.MM.dd"), Convert.ToDateTime(RadGridReport.MasterTableView.DataKeyValues[e.Item.ItemIndex]["FirstCheckEndDate"]).ToString("yyyy.MM.dd")));//初审时间

                        Utility.WordDelHelp.CreateXMLWordWithDot(this.Page,
                            "~/Template/续期汇总上报.doc"
                            , string.Format("~/UpLoad/XQReport/续期汇总上报_{0}.doc", UserID)
                            , list);
                        //Utility.WordDelHelp.ExportWord(this.Page, Server.MapPath(string.Format("~/UpLoad/XQReport/续期汇总上报_{0}.doc", UserID)));

                          List<ResultUrl> url = new List<ResultUrl>();
                          url.Add(new ResultUrl("续期汇总上报", string.Format("~/UpLoad/XQReport/续期汇总上报_{0}.doc", UserID)));
                          UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);

                        break;

                    case "OutPutReportExcel"://导出汇总明细

                        QueryParamOB q = new QueryParamOB();
                        q.Add(string.Format("ReportCode='{0}'", RadGridReport.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ReportCode"].ToString()));
                        string sortBy = "[GETDATE],UNITCODE,POSTID,CERTIFICATECODE";

                        string saveFile = string.Format("~/UpLoad/CertifEnterApply/续期初审_{0}.xls", UserID);//保存文件名
                        try
                        {
                            //检查临时目录
                            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"));

                            //导出数据到数据库服务器
                            //CertificateContinueDAL.OutputXlsFile(MyWebConfig.DBOutputPath + Server.MapPath(saveFile).Substring(1), q.ToWhereString(), sortBy);

                            string colHead = @"申请时间\初审时间\审核时间\岗位工种\证书编号\有效期至\姓名\证件号码\企业名称\机构代码\初审单位";
                            string colName = @"CONVERT(varchar(10), ApplyDate, 20)\CONVERT(varchar(10), [GETDATE], 20)\CONVERT(varchar(10), CheckDate, 20)\PostName\CertificateCode\CONVERT(varchar(10), ValidEndDate, 20)\WorkerName\WorkerCertificateCode\UnitName\UnitCode\FirstCheckUnitname";
                            CommonDAL.OutputXls(MyWebConfig.DBOutputPath + Server.MapPath(saveFile).Substring(1)
                                , "DBO.VIEW_CERTIFICATECONTINUE"
                                , q.ToWhereString(), sortBy, colHead, colName);

                        }
                        catch (Exception ex)
                        {
                            UIHelp.WriteErrorLog(Page, "导出续期初审查询结果失败！", ex);
                            return;
                        }

                        List<ResultUrl> url2 = new List<ResultUrl>();
                        url2.Add(new ResultUrl("续期初审查询结果下载", saveFile));
                        UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url2);
                        break;
                }
            }
        }

        //检查续期上报文件保存路径
        protected void CheckSaveDirectory()
        {
            //存放路径
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/XQReport/"))) Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/XQReport/"));
        }
    }
}
