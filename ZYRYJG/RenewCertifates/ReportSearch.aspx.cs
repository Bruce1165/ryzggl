using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using Telerik.Web.UI;
using System.Data;
using System.IO;


namespace ZYRYJG.RenewCertifates
{
    public partial class ReportSearch : BasePage
    {
        protected override bool IsNeedLogin
        {
            get
            {
                return false;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(string.IsNullOrEmpty(Request["o"])==false)
                {
                    PostSelect1.PostTypeID = Request["o"];
                    PostSelect1.LockPostTypeID();
                }
                PostSelect1.HideRadComboBoxPostID();

                DataTable dt = CommonDAL.GetDataTable(string.Format("select distinct FIRSTCHECKUNITNAME from certificatecontinue where ReportMan>''"));
                RadComboBoxReportMan.DataSource = dt;
                RadComboBoxReportMan.DataTextField = "FIRSTCHECKUNITNAME";
                RadComboBoxReportMan.DataValueField = "FIRSTCHECKUNITNAME";
                RadComboBoxReportMan.DataBind();
                RadComboBoxReportMan.Items.Insert(0, new RadComboBoxItem("全部", ""));

                ButtonSearch_Click(sender, e);
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
            QueryParamOB q = new QueryParamOB();

            //岗位类别
            if (PostSelect1.PostTypeID != "")
            {
                q.Add(string.Format("PostTypeID = {0}", PostSelect1.PostTypeID));
            }

            //工种类别
            if (PostSelect1.PostID != "")
            {
                switch (PostSelect1.PostID)
                {
                    case "9"://土建
                        q.Add(string.Format("(PostID = {0} or PostName like '%增土建')", PostSelect1.PostID));
                        break;
                    case "12"://安装
                        q.Add(string.Format("(PostID = {0} or PostName like '%增安装')", PostSelect1.PostID));
                        break;
                    default:
                        q.Add(string.Format("PostID = {0}", PostSelect1.PostID));
                        break;
                }
            }

            //汇总时间
            q.Add(string.Format("ReportDate between '{0}' and '{1} 23:59:59'"
                , RadDatePickerReportDateStart.SelectedDate.HasValue ? RadDatePickerReportDateStart.SelectedDate.Value.ToString("yyyy-MM-dd") : "2018-01-01"
            , RadDatePickerReportDateEnd.SelectedDate.HasValue ? RadDatePickerReportDateEnd.SelectedDate.Value.ToString("yyyy-MM-dd") : "2050-01-01"));

            //汇总批次号
            if (RadTextBoxReportCode.Text.Trim() != "")
            {
                q.Add(string.Format("ReportCode = '{0}'", RadTextBoxReportCode.Text.Trim()));
            }

            //汇总人
            if (RadComboBoxReportMan.SelectedValue != "")
            {
                q.Add(string.Format("FIRSTCHECKUNITNAME = '{0}'", RadComboBoxReportMan.SelectedValue));
            }

            //复合状态
            switch (RadioButtonCheckStatus.SelectedValue)
            {
                case "未复合":
                    q.Add("CheckStatus = '已初审'");
                    break;
                case "已复合":
                    q.Add("(CheckStatus = '已审核' or CheckStatus = '已决定')");
                    break;
            }

            ObjectDataSource1.SelectParameters.Clear();
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());

            RadGridReport.CurrentPageIndex = 0;
            RadGridReport.DataSourceID = ObjectDataSource1.ID;
        }

        protected void RadGridReport_ItemDataBound(object sender, GridItemEventArgs e)
        {
            //if (e.Item is GridDataItem)
            //{
            //    e.Item.Style.Add("cursor", "pointer");
            //    e.Item.Attributes.Add("onclick", string.Format("returnToParent('{0}')", e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ReportCode"]));
            //}
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
                sb.Append(string.Format("<a class=\"link_edit\" target=\"_blank\" href=\"../Upload/XQReport/{0}.jpg?read={1}\">【签字扫描件】</a><br/>", ReportCode, radom));
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
        //    if (b_file == true)
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

    }
}
