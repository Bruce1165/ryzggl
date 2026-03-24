using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZYRYJG.County
{
    //选择公示
    public partial class PublicityModify : BasePage
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
                tableSearch.Visible = false;
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
            q.Add(string.Format("PublicCode ='{0}'", ViewState["PublicCode"]));

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridADDRY.CurrentPageIndex = 0;
            RadGridADDRY.DataSourceID = ObjectDataSource1.ID;
        }
        //保存申述
        protected void BtnSave_Click(object sender, EventArgs e)
        {

            string ConfirmResult="";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < RadGridADDRY.MasterTableView.Items.Count; i++)
            {
                if (RadGridADDRY.MasterTableView.DataKeyValues[i]["ShenShu"].ToString() == "1")
                {
                    sb.Append(",'").Append(RadGridADDRY.MasterTableView.DataKeyValues[i]["ApplyID"].ToString()).Append("'");
                }
            }
            if (sb.Length > 0)
            {
                sb.Remove(0, 1);
            }
            
            if (!string.IsNullOrEmpty(Request.QueryString["03"]))
            {
               if(Request.QueryString["03"].ToString()=="通过")
               {
                 ConfirmResult="不通过";
               }
               else
               {
                 ConfirmResult="通过";
                }

                    
             }
            string sql = @"UPDATE [dbo].[Apply]
                           SET [ConfirmResult] ='{2}'
                              ,[PublicCode] = '{0}'
                              ,[XGSJ] = getdate()
                          WHERE ApplyID in({1})";

            try
            {
                string PublicCode = ApplyDAL.GetNextPublicCode("PublicCode", RadComboBoxIfContinue1.SelectedValue);
                CommonDAL.ExecSQL(string.Format(sql, PublicCode, sb, ConfirmResult));
                Export(PublicCode, ConfirmResult);
                UIHelp.layerAlert(Page, "保存成功", 6, 3000);
                ButtonSearch_Click(sender, e);

                ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
            }
             catch (Exception ex)
             {

                 UIHelp.WriteErrorLog(Page, "保存申述信息失败", ex);
             }
        }

        //导出
        public void Export(string PublicCode, string ConfirmResult)
        {
            try
            {
                string a = "";

                    //标题
                    a = @"<tr style=""height:30pt""><td style=""font-weight:bold;text-align:center;"" class=""noborder"" colspan=""7"">{1}北京市二级建造师{0}条件的人员名单</td></tr>";




                string head = "";
                string column = "";

                if (ConfirmResult == "通过")
                {
                    if (RadComboBoxIfContinue1.SelectedValue == "增项注册")
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
                if (ConfirmResult == "不通过")
                {

                    if (RadComboBoxIfContinue1.SelectedValue == "增项注册")
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
                    , string.Format(a, RadComboBoxIfContinue1.SelectedValue, ConfirmResult == "通过" ? "符合" : "不符合")
                    , Foot
                );
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "业务查询结果导出EXCEL失败！", ex);
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
     
        protected void RadGridADDRY_DataBound(object sender, EventArgs e)
        {
            int count = 0;
            for (int i = 0; i < RadGridADDRY.MasterTableView.Items.Count; i++)
            {
                if(RadGridADDRY.MasterTableView.DataKeyValues[i]["ShenShu"].ToString() =="1")
                {
                    count++;
                }
            }

            divMessage.InnerHtml = string.Format("当前有<span style='color:red; font-size:18px'> {0} </span>条记录上传了申述扫描件，点击保存这些记录将修改为审核通过，并组成一个新的公示批次（新批次号）保存。",count);

            
        }

      
    }
}