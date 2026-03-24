using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;

namespace ZYRYJG.County
{
    /// <summary>
    /// 水利部门、公路部门会审建造师（作废）
    /// </summary>
    public partial class CheckReturnList : BasePage
    {
    
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                Response.Clear();
                //ButtonSearch_Click(sender, e);
            }
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            if (RadioButtonListCityType.SelectedValue == "not")
            {
                BttSave.Visible = false;
            }
            else 
            {
                BttSave.Visible = true ;
            }
            ObjectDataSource1.SelectParameters.Clear();
            string type = Request.QueryString["type"];
            ViewState["ApplyType"] = Request.QueryString["type"];
            var q = new QueryParamOB();
            
            if (IfExistRoleID("8") == true)//水利部门
            {
                q.Add(string.Format("(PSN_RegisteProfession like '%水利%' or ApplyID in(SELECT [ApplyID] FROM [dbo].[ApplyAddItem] where [AddItem1] like '水利' or [AddItem2] like '水利')) and len(OtherDeptCheckMan)>0", EnumManager.ApplyStatus.已审查));

            }
            if (IfExistRoleID("9") == true)//公路部门
            {
                q.Add(string.Format("(PSN_RegisteProfession like '%公路%' or ApplyID in(SELECT [ApplyID] FROM [dbo].[ApplyAddItem] where [AddItem1] like '公路' or [AddItem2] like '公路')) and len(OtherDeptCheckMan)>0", EnumManager.ApplyStatus.已审查));
            }
            
            if (RadTextBoxValue.Text.Trim() != "")//自定义查询项
            {
                q.Add(string.Format("{0} like '%{1}%'", RadComboBoxIten.SelectedValue, RadTextBoxValue.Text.Trim()));
            }

            if (RadComboBoxApplyType.SelectedValue != "")//申报类型
            {
                q.Add(string.Format("ApplyType like '{0}'", RadComboBoxApplyType.SelectedValue));
            }
            

            //会审日期
            if (RadDatePickerCheckDateStart.SelectedDate.HasValue == true)
            {
                q.Add(string.Format("OtherDeptCheckDate >= '{0}'", RadDatePickerCheckDateStart.SelectedDate.Value));
            }
            if (RadDatePickerCheckDateEnd.SelectedDate.HasValue == true)
            {
                q.Add(string.Format("OtherDeptCheckDate <= '{0}'", RadDatePickerCheckDateEnd.SelectedDate.Value));
            }

            //是否已回传
            q.Add(string.Format("OtherDeptCheckDate is {0} null", RadioButtonListCityType.SelectedValue));
           
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridQY.CurrentPageIndex = 0;
            RadGridQY.DataSourceID = ObjectDataSource1.ID;
        }
       
        /// <summary>
        /// 申请填报页面地址
        /// </summary>
        public string GetApplyTypeUrl(string type,string bgzc)
        {
            string URL = "";
            if(!string.IsNullOrEmpty(bgzc))
            {
                if (bgzc == "个人信息变更")
                    URL = "../Unit/ApplyChange.aspx";
                if (bgzc == "执业企业变更")
                    URL = "../Unit/ApplyChangePersonnel.aspx";
            }
            switch (type)
            {
                case "增项注册":
                    return "../Unit/ApplyAddItem.aspx";//增项注册
                case "初始注册":
                    return "../Unit/ApplyFirstAdd.aspx";//初始注册
                case "变更注册":
                    return URL;//变更注册
                case "延期注册":
                    return "../Unit/ApplyContinue.aspx";//延期注册
                case "重新注册":
                    return "../Unit/ApplyRenew.aspx";//重新注册
                case "遗失补办":
                    return "../Unit/ApplyReplace.aspx";//遗失补办
                case "注销":
                    return "../Unit/ApplyCancel.aspx";//注销注册
                default:
                    return "#";
            }

        }
        //EXCEL导入导出
        protected void ImageButtonOutput_Click(object sender, EventArgs e)
        {
            try
            {
                var q = new QueryParamOB();

                if (IfExistRoleID("8") == true)//水利部门
                {
                    q.Add(string.Format("PSN_RegisteProfession like '%水利%' and len(OtherDeptCheckMan)>0", EnumManager.ApplyStatus.已审查));

                }
                if (IfExistRoleID("9") == true)//公路部门
                {
                    q.Add(string.Format("PSN_RegisteProfession like '%公路%' and len(OtherDeptCheckMan)>0", EnumManager.ApplyStatus.已审查));
                }

                if (RadTextBoxValue.Text.Trim() != "")//自定义查询项
                {
                    q.Add(string.Format("{0} like '%{1}%'", RadComboBoxIten.SelectedValue, RadTextBoxValue.Text.Trim()));
                }

                if (RadComboBoxApplyType.SelectedValue != "")//申报类型
                {
                    q.Add(string.Format("ApplyType like '{0}'", RadComboBoxApplyType.SelectedValue));
                }


                //会审日期
                if (RadDatePickerCheckDateStart.SelectedDate.HasValue == true)
                {
                    q.Add(string.Format("OtherDeptCheckDate >= '{0}'", RadDatePickerCheckDateStart.SelectedDate.Value));
                }
                if (RadDatePickerCheckDateEnd.SelectedDate.HasValue == true)
                {
                    q.Add(string.Format("OtherDeptCheckDate <= '{0}'", RadDatePickerCheckDateEnd.SelectedDate.Value));
                }

                //是否已回传
                q.Add(string.Format("OtherDeptCheckDate is {0} null", RadioButtonListCityType.SelectedValue));

                //excel标题
                string Caption = @"
                <tr style=""height:30pt"">
                <td style=""font-weight:bold;text-align:center;"" class=""noborder"" colspan=""9"">北京市二级建造师初始、重新、增项注册专业部门会审意见汇总表</td>
                </tr>
                <tr style=""height:15pt"">
                <td style=""text-align:left;"" class=""noborder"" colspan=""1"">部门公章：</td>
                </tr>";
                //EXCEL表头明
                string head = @"序号\姓名\证件号码\企业名称\注册专业\申报类型\市建委审核时间\会审结果\回传日期";
                //数据表的列明
                string column = @"row_number() over(order by [dbo].[Apply].[ApplyID])\PSN_Name\PSN_CertificateNO\ENT_Name\PSN_RegisteProfession\ApplyType\convert(varchar(10),CheckDate,111)\OtherDeptCheckResult\convert(varchar(10),OtherDeptCheckDate,111)";

                string foot = @"
                <tr style=""height:30pt"">
                <td style=""text-align:left;"" colspan=""4"" class=""noborder"">填报人：</td>
                <td style=""text-align:right;"" class=""noborder"" colspan=""3"">审核人：</td>
                </tr>";

                if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/Excel/")))
                {
                    System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Upload/Excel/"));
                }
                string filePath = string.Format("~/Upload/Excel/Excel{0}{1}.xls",UserID, DateTime.Now.ToString("yyyyMMddHHmmss"));
                CommonDAL.OutputXls(HttpContext.Current.Server.MapPath(filePath)
                    , "Apply"
                    , q.ToWhereString()
                    , "ApplyID", head.ToString(), column.ToString(), Caption,foot);
                string size = CommonDAL.GetFileSize(HttpContext.Current.Server.MapPath(filePath));
                spanOutput.InnerHtml = string.Format(@"<div style=""width: 98%; font-weight: bold; ""><a href=""{0}"">{1}</a><span  style=""padding-left:20px;"">（{2}）</span></div>"
                    ,UIHelp.ShowFile(filePath)
                    , "点击我下载"
                    , size);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "申请列表导出EXCEL失败！", ex);
            }
        }
        /// <summary>
        /// 获取grid勾选行ApplyID集合
        /// </summary>
        /// <returns></returns>
        public string GetRadGridADDRYSelect()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i < RadGridQY.MasterTableView.Items.Count; i++)
            {
                CheckBox CheckBox1 = RadGridQY.Items[i].FindControl("CheckBox1") as CheckBox;
                if (CheckBox1.Checked)
                {
                    sb.Append(",'").Append(RadGridQY.MasterTableView.DataKeyValues[i]["ApplyID"].ToString()).Append("'");
                }
            }
            if (sb.Length > 0)
            {
                sb.Remove(0, 1);
            }
            return sb.ToString();
        }
        //批量回传
        protected void BttSave_Click(object sender, EventArgs e)
        {
            try
            {
                //获取Grid选中集合
                string applyidlist = GetRadGridADDRYSelect();
                if (applyidlist == "")
                {
                    UIHelp.layerAlert(Page, "您尚未勾选任何人员！");
                    return;
                }
                DateTime OtherDeptCheckDate = DateTime.Now;

                string sql = string.Format(@"Update Apply set OtherDeptCheckDate='{0}' Where ApplyID in({1})",
                                              OtherDeptCheckDate,  applyidlist);
                if (CommonDAL.ExecSQL(sql))
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "refresh", "window.location.href = window.location.href;", true);
                }

            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "批量回传会审记录失败", ex);
            }
            
        }   
     
    }
}