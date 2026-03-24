using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZYRYJG.County
{
    //选择公示
    public partial class PublicityChoice :BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "County/PublicityLook.aspx";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["01"]))
                {
                    ViewState["PublicCode"] = Request["01"];
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
                q.Add("PublicCode is null");
                q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ApplyStatus.已决定));
                //添加决定的过滤条件
                q.Add(string.Format("ConfirmResult = '{0}'", RadioButtonListReportStatus.SelectedValue));
                q.Add(string.Format("PSN_Level='{0}'", RadioButtonList1.SelectedValue));
            }
              //修改
            if (Request.QueryString["03"] != null&& Request.QueryString["03"].ToString() == "xg")//edit
            {
                q.Add("PublicDate is null");
                q.Add(string.Format("PublicCode ='{0}'", ViewState["PublicCode"]));
                q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ApplyStatus.已决定));
            }
            //详细
            if (Request.QueryString["03"] != null&& Request.QueryString["03"].ToString() == "xx")
            {
                //公高批次号
                q.Add(string.Format("PublicCode ='{0}'", ViewState["PublicCode"]));
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
                string date;
                //公示批次号
                if (ViewState["PublicCode"] != null)//edit
                    date = ViewState["PublicCode"].ToString();
                //公示批次号
                else
                    date = ApplyDAL.GetNextPublicCode("PublicCode", RadComboBoxIfContinue1.SelectedValue);
                ApplyDAL.SavePublicCode(date, selectApplyIDs);

                Export(date); //保存导出表

                UIHelp.WriteOperateLog(UserName, UserID, "公示保存成功", string.Format("公示时间：{0}", DateTime.Now));
                UIHelp.ParentAlert(Page, "保存成功！", true);

            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "保存要上报的信息失败", ex);
            }
        }

        //立刻上报
        protected void ButtonReport_Click(object sender, EventArgs e)
        {
            string selectApplyIDs = GetRadGridADDRYSelect();
            if (selectApplyIDs == "")
            {
                UIHelp.layerAlert(Page, "您尚未勾选任何人员！");
                return;
            }
            try
            {
                string date;
                //公示批次号
                if (ViewState["PublicCode"] != null)//edit
                    date = ViewState["PublicCode"].ToString();
                //公示批次号
                else
                    date = ApplyDAL.GetNextPublicCode("PublicCode", RadComboBoxIfContinue1.SelectedValue);
                ApplyDAL.ExePublicReport(EnumManager.ApplyStatus.已公示, DateTime.Now, UserName,date, selectApplyIDs);

                Export(date); //保存导出表

                UIHelp.WriteOperateLog(UserName, UserID, "公示成功", string.Format("公示时间：{0}", DateTime.Now));
                UIHelp.ParentAlert(Page, "公示成功！", true);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "公示失败", ex);
            }
        }

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
               && RadGridADDRY.MasterTableView.DataKeyValues[0]["PublicMan"] != DBNull.Value)//已公示
            {
                ButtonReport.Visible = false;
                BtnSave.Visible = false;
                tableSearch.Visible = false;
            }
        }

        //导出
        public void Export(string PublicCode)
        {
            if (RadGridADDRY.MasterTableView.Items.Count==0)
            {
                return;
            }
            try
            {
                string a = "";
                if (RadGridADDRY.MasterTableView.DataKeyValues[0]["PSN_Level"].ToString() == "二级")
                {
                    //标题
                    a = @"<tr style=""height:30pt""><td style=""font-weight:bold;text-align:center;"" class=""noborder"" colspan=""7"">{1}北京市二级建造师{0}条件的人员名单</td></tr>";
                }
                else if (RadGridADDRY.MasterTableView.DataKeyValues[0]["PSN_Level"].ToString() == "二级临时")
                {
                    //标题
                    a = @"<tr style=""height:30pt""><td style=""font-weight:bold;text-align:center;"" class=""noborder"" colspan=""7"">{1}北京市二级临时建造师{0}条件的人员名单</td></tr>";
                }

                string head = "";
                string column = "";

                if (RadGridADDRY.MasterTableView.DataKeyValues[0]["ConfirmResult"].ToString() == "通过")
                {
                    if (RadGridADDRY.MasterTableView.DataKeyValues[0]["ApplyType"].ToString() == "增项注册")
                    {
                        //EXCEL表头明
                        head = @"序号\市区\企业名称\姓名\申请专业";
                        //数据表的列明
                        column = @"row_number() over(order by [dbo].[Apply].[ApplyID])\replace(case when [ENT_City] like '%亦庄%' or [ENT_City] like '%开发区%' then '开发区'else [ENT_City] end,'县','区')\ENT_Name\PSN_Name\(select  ISNULL(a.AddItem1,'')+ISNULL(+','+a.AddItem2,'')  from  ApplyAddItem a where ISNULL(a.AddItem1,'')+ISNULL(a.AddItem2,'') !='' and a.ApplyID=[Apply].ApplyID)";
                        //过滤条件
                    }
                    else
                    {
                        //EXCEL表头明
                        head = @"序号\市区\企业名称\姓名\申请专业";
                        //数据表的列明
                        column = @"row_number() over(order by [dbo].[Apply].[ApplyID])\replace(case when [ENT_City] like '%亦庄%' or [ENT_City] like '%开发区%' then '开发区'else [ENT_City] end,'县','区')\ENT_Name\PSN_Name\PSN_RegisteProfession";
                        //过滤条件
                    }
                }
                else if (RadGridADDRY.MasterTableView.DataKeyValues[0]["ConfirmResult"].ToString() == "不通过")
                {

                    if (RadGridADDRY.MasterTableView.DataKeyValues[0]["ApplyType"].ToString() == "增项注册")
                    {
                        //EXCEL表头明
                        head = @"序号\市区\企业名称\姓名\申请专业\不通过注册理由";
                        //数据表的列明
                        column = @"row_number() over(order by [dbo].[Apply].[ApplyID])\replace(case when [ENT_City] like '%亦庄%' or [ENT_City] like '%开发区%' then '开发区'else [ENT_City] end,'县','区')\ENT_Name\PSN_Name\(select  ISNULL(a.AddItem1,'')+ISNULL(+','+a.AddItem2,'')  from  ApplyAddItem a where ISNULL(a.AddItem1,'')+ISNULL(a.AddItem2,'') !='' and a.ApplyID=[Apply].ApplyID)\CheckRemark";
                    }
                    else
                    {
                        //EXCEL表头明
                        head = @"序号\市区\企业名称\姓名\申请专业\不通过注册理由";
                        //数据表的列明
                        column = @"row_number() over(order by [dbo].[Apply].[ApplyID])\replace(case when [ENT_City] like '%亦庄%' or [ENT_City] like '%开发区%' then '开发区'else [ENT_City] end,'县','区')\ENT_Name\PSN_Name\PSN_RegisteProfession\CheckRemark";

                    }
                }
                string Foot = "";
                //过滤条件
                string filterSql = string.Format(" and PublicCode ='{0}'", PublicCode);
                if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/ReportXls/")))
                {
                    System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Upload/ReportXls/"));
                }
                string filePath = string.Format("~/Upload/ReportXls/Excel{0}.xls", PublicCode);
                CommonDAL.OutputXls(HttpContext.Current.Server.MapPath(filePath)
                    , "[dbo].[Apply]"
                    , filterSql
                    , "row_number() over(order by [dbo].[Apply].[ApplyID])"
                    , head.ToString()
                    , column.ToString()
                    , string.Format(a, RadComboBoxIfContinue1.SelectedValue, RadioButtonListReportStatus.SelectedValue=="通过"?"符合":"不符合")
                    , Foot
                );
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "业务查询结果导出EXCEL失败！", ex);
            }



           
        
        }

      
    }
}