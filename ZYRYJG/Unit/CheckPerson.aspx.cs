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
    public partial class CheckPerson : BasePage
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

            var q = new QueryParamOB();
            RadGridQY.CurrentPageIndex = 0;

            //人员类型
            if (RadComboBoxPersonType.SelectedValue != "")
            {
                q.Add(string.Format("[POSTTYPENAME] like '{0}%'", RadComboBoxPersonType.SelectedValue));
            }

            //申请日期
            if (RadDatePicker_ApplyStartDate.SelectedDate.HasValue == true)
            {
                q.Add(string.Format("ApplyTime >= '{0}'", RadDatePicker_ApplyStartDate.SelectedDate.Value));
            }
            if (RadDatePicker_ApplyEndDate.SelectedDate.HasValue == true)
            {
                q.Add(string.Format("ApplyTime < '{0}'", RadDatePicker_ApplyEndDate.SelectedDate.Value.AddDays(1)));
            }

            //办理日期
            if (RadDatePicker_NoticeStartDate.SelectedDate.HasValue == true)
            {
                q.Add(string.Format("[NoticeDate] >= '{0}'", RadDatePicker_NoticeStartDate.SelectedDate.Value));
            }
            if (RadDatePicker_NoticeEndDate.SelectedDate.HasValue == true)
            {
                q.Add(string.Format("[NoticeDate] < '{0}'", RadDatePicker_NoticeEndDate.SelectedDate.Value.AddDays(1)));
            }

            if (RadTextBoxValue.Text.Trim() != "")//自定义查询项
            {
                q.Add(string.Format("{0} like '%{1}%'", RadComboBoxIten.SelectedValue, RadTextBoxValue.Text.Trim()));
            }

            //if (RadComboBoxPersonType.SelectedValue == "二级注册建造师")//人员类型
            //{
            //    ObjectDataSource1.SelectParameters.Clear();

            //    //业务类型
            //    if (RadComboBoxPSN_RegisteType.SelectedValue != "")
            //    {
            //        q.Add(string.Format("[ApplyType] like '{0}%'", RadComboBoxPSN_RegisteType.SelectedValue));
            //    }

            //    ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            //    RadGridQY.DataSourceID = ObjectDataSource1.ID;
            //}
            //else//安全生产考核三类人员 or 建筑施工特种作业人员
            //{
            //    ObjectDataSource2.SelectParameters.Clear();

            //    //业务类型
            //    if (RadComboBox_CongYeApplyType.SelectedValue != "")
            //    {
            //        q.Add(string.Format("[ApplyType] like '{0}%'", RadComboBox_CongYeApplyType.SelectedValue));
            //    }

            //    ObjectDataSource2.SelectParameters.Add("filterWhereString", q.ToWhereString());
            //    RadGridQY.DataSourceID = ObjectDataSource2.ID;
            //}

            switch (RadComboBoxPersonType.SelectedValue)
            {
                case "二级注册建造师":
                    ObjectDataSource1.SelectParameters.Clear();

                    //业务类型
                    if (RadComboBoxPSN_RegisteType.SelectedValue != "")
                    {
                        q.Add(string.Format("[ApplyType] like '{0}%'", RadComboBoxPSN_RegisteType.SelectedValue));
                    }

                    ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
                    RadGridQY.DataSourceID = ObjectDataSource1.ID;
                    break;
                case "二级造价工程师":
                    ObjectDataSource3.SelectParameters.Clear();

                    //业务类型
                    if (RadComboBoxEJZJGCS.SelectedValue != "")
                    {
                        q.Add(string.Format("[ApplyType] like '{0}%'", RadComboBoxEJZJGCS.SelectedValue));
                    }

                    ObjectDataSource3.SelectParameters.Add("filterWhereString", q.ToWhereString());
                    RadGridQY.DataSourceID = ObjectDataSource3.ID;
                    break;
                default://从业人员（三类人、特种作业）
                    ObjectDataSource2.SelectParameters.Clear();

                    //业务类型
                    if (RadComboBox_CongYeApplyType.SelectedValue != "")
                    {
                        q.Add(string.Format("[ApplyType] like '{0}%'", RadComboBox_CongYeApplyType.SelectedValue));
                    }

                    ObjectDataSource2.SelectParameters.Add("filterWhereString", q.ToWhereString());
                    RadGridQY.DataSourceID = ObjectDataSource2.ID;
                    break;
            }

            ViewState["JZS_TOW_Check_QueryParamOB"] = q;
        }

        ////导出数据
        //protected void ButtonExportToExcel_Click(object sender, EventArgs e)
        //{
        //    //检查临时目录
        //    if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"));


        //    QueryParamOB q = ViewState["JZS_TOW_Check_QueryParamOB"] as QueryParamOB;

        //    string saveFile = string.Format("~/UpLoad/CertifEnterApply/{2}检查对象_{0}{1}.xls", UserID, DateTime.Now.ToString("yyyyMMddHHmmss"), RadComboBoxPersonType.SelectedValue);//保存文件名
        //    string colHead = "";
            
        //    if (RadComboBoxPersonType.SelectedValue == "二级注册建造师" || RadComboBoxPersonType.SelectedValue == "二级造价工程师")//人员类型
        //    {
        //        colHead = @"人员类型\姓名\证件号码\企业名称\注册号\注册专业及有效期\申报事项\申报日期\申报进度";
        //    }
        //    else
        //    {
        //        colHead = @"人员类型\姓名\证件号码\企业名称\证书编号\注册专业及有效期\申报事项\申报日期\申报进度";
        //    }
        //    string colName = @"PostTypeName\PSN_Name\PSN_CertificateNO\ENT_Name\PSN_RegisterNO\ProfessionWithValid\ApplyType\CONVERT(varchar(10), ApplyTime, 20)\CONVERT(varchar(10), NoticeDate, 20)";


        //    try
        //    {
          

        //        switch (RadComboBoxPersonType.SelectedValue)
        //        {
        //            case "二级注册建造师":
        //                CommonDAL.OutputXlsx(MyWebConfig.DBOutputPath + Server.MapPath(saveFile).Substring(1),RadComboBoxPersonType.SelectedValue, "DBO.[TJ_JZS_TOW_Check]", q.ToWhereString(), "[PSN_CertificateNO]", colHead, colName);
        //                break;
        //            case "二级造价工程师":
        //                CommonDAL.OutputXlsx(MyWebConfig.DBOutputPath + Server.MapPath(saveFile).Substring(1), RadComboBoxPersonType.SelectedValue, "DBO.[TJ_ZJGCS_TOW_Check]", q.ToWhereString(), "[PSN_CertificateNO]", colHead, colName);
        //                break;
        //            default://从业人员（三类人、特种作业）
        //                CommonDAL.OutputXlsx(MyWebConfig.DBOutputPath + Server.MapPath(saveFile).Substring(1), RadComboBoxPersonType.SelectedValue, "DBO.[TJ_CongYe_Check]", q.ToWhereString(), "[PSN_CertificateNO]", colHead, colName);
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        UIHelp.WriteErrorLog(Page, "导出检查对象信息失败！", ex);
        //        return;
        //    }

        //    List<ResultUrl> url = new List<ResultUrl>();
        //    url.Add(new ResultUrl(string.Format("{0}检查对象下载", RadComboBoxPersonType.SelectedValue), saveFile));
        //    UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);
        //}

        //导出数据
        protected void ButtonExportToExcel_Click(object sender, EventArgs e)
        {
            //检查临时目录
            if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"));


            QueryParamOB q = ViewState["JZS_TOW_Check_QueryParamOB"] as QueryParamOB;

            string saveFile = string.Format("~/UpLoad/CertifEnterApply/{2}检查对象_{0}{1}.xls", UserID, DateTime.Now.ToString("yyyyMMddHHmmss"), RadComboBoxPersonType.SelectedValue);//保存文件名
            string colHead = "";
            if (RadComboBoxPersonType.SelectedValue == "二级注册建造师" || RadComboBoxPersonType.SelectedValue == "二级造价工程师")//人员类型
            {
                colHead = @"人员类型\姓名\证件号码\企业名称\注册号\注册专业及有效期\申报事项\申报日期\申报进度";
            }
            else
            {
                colHead = @"人员类型\姓名\证件号码\企业名称\证书编号\注册专业及有效期\申报事项\申报日期\申报进度";
            }
            string colName = @"PostTypeName\PSN_Name\PSN_CertificateNO\ENT_Name\PSN_RegisterNO\ProfessionWithValid\ApplyType\CONVERT(varchar(10), ApplyTime, 20)\CONVERT(varchar(10), NoticeDate, 20)";

            try
            {
                //if (RadComboBoxPersonType.SelectedValue == "二级注册建造师")//人员类型
                //{
                //    CommonDAL.OutputXls(MyWebConfig.DBOutputPath + Server.MapPath(saveFile).Substring(1), "DBO.[TJ_JZS_TOW_Check]", q.ToWhereString(), "[PSN_CertificateNO]", colHead, colName);
                //}
                //else
                //{
                //    CommonDAL.OutputXls(MyWebConfig.DBOutputPath + Server.MapPath(saveFile).Substring(1), "DBO.[TJ_CongYe_Check]", q.ToWhereString(), "[PSN_CertificateNO]", colHead, colName);
                //}

                switch (RadComboBoxPersonType.SelectedValue)
                {
                    case "二级注册建造师":
                        CommonDAL.OutputXls(MyWebConfig.DBOutputPath + Server.MapPath(saveFile).Substring(1), "DBO.[TJ_JZS_TOW_Check]", q.ToWhereString(), "[PSN_CertificateNO]", colHead, colName);
                        break;
                    case "二级造价工程师":
                        CommonDAL.OutputXls(MyWebConfig.DBOutputPath + Server.MapPath(saveFile).Substring(1), "DBO.[TJ_ZJGCS_TOW_Check]", q.ToWhereString(), "[PSN_CertificateNO]", colHead, colName);
                        break;
                    default://从业人员（三类人、特种作业）
                        CommonDAL.OutputXls(MyWebConfig.DBOutputPath + Server.MapPath(saveFile).Substring(1), "DBO.[TJ_CongYe_Check]", q.ToWhereString(), "[PSN_CertificateNO]", colHead, colName);
                        break;
                }
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "导出检查对象信息失败！", ex);
                return;
            }

            List<ResultUrl> url = new List<ResultUrl>();
            url.Add(new ResultUrl(string.Format("{0}检查对象下载", RadComboBoxPersonType.SelectedValue), saveFile));
            UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);
        }

        //变换人员类型
        protected void RadComboBoxPersonType_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            switch (RadComboBoxPersonType.SelectedValue)
            {
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
    }
}