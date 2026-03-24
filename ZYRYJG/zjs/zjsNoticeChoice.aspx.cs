using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZYRYJG.zjs
{
    /// <summary>
    /// 页面功能：选择二级造价工程师公告数据。
    /// 页面传递参数说明：
    /// Request["01"]=NoticeCode 公告编号
    /// Request["02"]=applytype 注册类型
    /// Request["03"]=页面访问方式{null=新增；xg=修改；xx=详细}
    /// </summary>
    public partial class zjsNoticeChoice : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "zjs/zjsNoticeLook.aspx";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["01"]))
                {
                    ViewState["NoticeCode"] = Request["01"];

                    string ConfirmResult = zjs_ApplyDAL.GetConfirmResultByNoticeCode(Request["01"]);
                    RadioButtonListReportStatus.ClearSelection();
                    RadioButtonListReportStatus.Items.FindByValue(ConfirmResult).Selected = true;

                    //修改时不能修改申报事项类型
                    UIHelp.SelectDropDownListItemByValue(RadComboBoxIfContinue1, Request["02"]);
                    UIHelp.SetReadOnly(RadComboBoxIfContinue1, false);
                }
                ButtonSearch_Click(sender, e);
            }
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ObjectDataSource1.SelectParameters.Clear();
            var q = new QueryParamOB();
            q.Add(string.Format("applytype ='{0}'", RadComboBoxIfContinue1.SelectedValue));//申报类型
            //决定日期
            if (RadDatePickerGetDateStart.SelectedDate.HasValue == true)
            {
                q.Add(string.Format("ConfirmDate >= '{0}'", RadDatePickerGetDateStart.SelectedDate.Value));
            }
            if (RadDatePickerGetDateEnd.SelectedDate.HasValue == true)
            {
                q.Add(string.Format("ConfirmDate < '{0}'", RadDatePickerGetDateEnd.SelectedDate.Value.AddDays(1)));
            }
            //新增
            if (Request.QueryString["03"] == null)
            {
                q.Add("NoticeCode is null");//未创建公告批次号
                q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ZJSApplyStatus.已决定));
                q.Add(string.Format("ConfirmResult = '{0}'", RadioButtonListReportStatus.SelectedValue));//决定结果
            }
            //修改
            if (Request.QueryString["03"] != null && Request.QueryString["03"].ToString() == "xg")//edit
            {
                //q.Add("PublicDate is null");
                //q.Add(string.Format("NoticeCode ='{0}'", ViewState["NoticeCode"]));
                //q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ZJSApplyStatus.已决定));

                q.Add("NoticeDate is null");//只保存了待公告数据（有公告批次号），未公告（无公告时间）
                q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ZJSApplyStatus.已决定));
                q.Add(string.Format("ConfirmResult = '{0}'", RadioButtonListReportStatus.SelectedValue));//决定结果
            }
            //详细
            if (Request.QueryString["03"] != null && Request.QueryString["03"].ToString() == "xx")
            {
                //公告批次号
                q.Add(string.Format("NoticeCode ='{0}'", ViewState["NoticeCode"]));
            }
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridADDRY.CurrentPageIndex = 0;
            RadGridADDRY.DataSourceID = ObjectDataSource1.ID;
        }
        //保存
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            string selectApplyIDs = GetRadGridADDRYSelect();
            if (selectApplyIDs == "")
            {
                UIHelp.layerAlert(Page, "您尚未勾选任何人员！");
                return;
            }
            try
            {
                string NoticeCode;//公告批次号
                
                if (ViewState["NoticeCode"] != null)//edit
                {
                    NoticeCode = ViewState["NoticeCode"].ToString();
                }
                else
                {
                    NoticeCode = zjs_ApplyDAL.GetNextNoticeCode(RadComboBoxIfContinue1.SelectedValue);
                }
                zjs_ApplyDAL.SaveNoticeCode(NoticeCode, selectApplyIDs);
                ViewState["NoticeCode"] = NoticeCode;
                Export(NoticeCode); //保存导出表

                UIHelp.WriteOperateLog(UserName, UserID, "造价工程师公告保存成功", string.Format("公示时间：{0}", DateTime.Now));
                UIHelp.ParentAlert(Page, "保存成功！", true);

            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "保存造价工程师公告失败", ex);
            }
        }

        ////立刻公告
        //protected void ButtonReport_Click(object sender, EventArgs e)
        //{
        //    string selectApplyIDs = GetRadGridADDRYSelect();
        //    if (selectApplyIDs == "")
        //    {
        //        UIHelp.layerAlert(Page, "您尚未勾选任何人员！");
        //        return;
        //    }
        //    try
        //    {
        //        string NoticeCode; //公告批次号
               
        //        if (ViewState["NoticeCode"] != null)//edit
        //        {
        //            NoticeCode = ViewState["NoticeCode"].ToString();
        //        }
        //        else
        //        {
        //            NoticeCode = zjs_ApplyDAL.GetNextNoticeCode( RadComboBoxIfContinue1.SelectedValue);
        //        }
        //        zjs_ApplyDAL.ExeNoticeReport(EnumManager.ApplyStatus.已公告, DateTime.Now, UserName, NoticeCode, selectApplyIDs);
        //        ViewState["NoticeCode"] = NoticeCode;

        //        Export(NoticeCode); //保存导出表

        //        UIHelp.WriteOperateLog(UserName, UserID, "造价工程师公告成功", string.Format("公告批次号：{0}", NoticeCode));
        //        UIHelp.ParentAlert(Page, "公告成功！", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        UIHelp.WriteErrorLog(Page, "公告失败", ex);
        //    }
        //}

        /// <summary>
        /// 获取grid勾选行ApplyID集合
        /// </summary>
        /// <returns></returns>
        private string GetRadGridADDRYSelect()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < RadGridADDRY.MasterTableView.Items.Count; i++)
            {
                CheckBox CheckBox1 = RadGridADDRY.Items[i].FindControl("CheckBox1") as CheckBox;
                if (CheckBox1.Checked)
                {
                    sb.Append(",'").Append(RadGridADDRY.MasterTableView.DataKeyValues[i]["ApplyID"].ToString()).Append("'");
                }
            }
            if (sb.Length > 0)
            {
                sb.Remove(0, 1);
            }
            return sb.ToString();
        }

        //所有数据绑定完毕，在执行
        protected void RadGridADDRY_DataBound(object sender, EventArgs e)
        {
            if (RadGridADDRY.MasterTableView.Items.Count > 0
               && RadGridADDRY.MasterTableView.DataKeyValues[0]["NoticeMan"] != DBNull.Value)//已公告
            {
                BtnSave.Visible = false;
                tableSearch.Visible = false;
            }
        }
  
        /// <summary>
        /// 导出公告结果
        /// </summary>
        /// <param name="NoticeCode">公告编号</param>
        public void Export(string NoticeCode)
        {
            try
            {
                //标题
                string title = @"<tr style=""height:30pt""><td style=""font-weight:bold;text-align:center;"" class=""noborder"" colspan=""7"">{1}北京市二级造价工程师{0}条件的人员名单</td></tr>";
                string head = "";//excel列头
                string column = "";//列映射字段

                if (RadioButtonListReportStatus.SelectedValue != null && RadioButtonListReportStatus.SelectedValue == "通过")
                {
                    //EXCEL表头名
                    head = @"序号\企业名称\姓名\申请专业";
                    //数据表的列名
                    column = @"row_number() over(order by [dbo].[zjs_Apply].[ApplyID])\ENT_Name\PSN_Name\PSN_RegisteProfession";
                    //过滤条件
                }
                else if (RadioButtonListReportStatus.SelectedValue != null && RadioButtonListReportStatus.SelectedValue == "不通过")
                {
                    //EXCEL表头名
                    head = @"序号\企业名称\姓名\申请专业\不通过注册理由";
                    //数据表的列名
                    column = @"row_number() over(order by [dbo].[zjs_Apply].[ApplyID])\ENT_Name\PSN_Name\PSN_RegisteProfession\ExamineRemark";
                }
                else
                {
                    return;
                }
                string Foot = "";
                //过滤条件
                string filterSql = string.Format(" and NoticeCode ='{0}'", NoticeCode);
                if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/ReportXls/")))
                {
                    System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Upload/ReportXls/"));
                }
                string filePath = string.Format("~/Upload/ReportXls/Excel{0}.xls", NoticeCode);
                CommonDAL.OutputXls(HttpContext.Current.Server.MapPath(filePath)
                    , "[dbo].[zjs_Apply]"
                    , filterSql
                    , "row_number() over(order by [dbo].[zjs_Apply].[ApplyID])"
                    , head.ToString()
                    , column.ToString()
                    , string.Format(title, RadComboBoxIfContinue1.SelectedValue, RadioButtonListReportStatus.SelectedValue == "通过" ? "符合" : "不符合")
                    , Foot
                );
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "导出造价工程师公告结果EXCEL失败！", ex);
            }
        }

        //protected void ButtonOutPut_Click(object sender, EventArgs e)
        //{
        //    if(ViewState["NoticeCode"] == null)
        //    {
        //        return;
        //    }
        //    Export(ViewState["NoticeCode"].ToString());
        //}
    }
}