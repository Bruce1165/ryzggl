using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;
using System.Data;
using System.Collections;

namespace ZYRYJG.zjs
{
    //业务查询
    public partial class zjsBusinessQuery : BasePage
    {
        /// <summary>
        /// 注册类型
        /// </summary>
        public string ApplyType
        {
            get
            {
                if (ViewState["ApplyType"] == null) return "";
                switch (ViewState["ApplyType"].ToString())
                {
                    case "f":
                        return "初始注册";
                    case "c":
                        return "个人信息变更";
                    case "u":
                        return "执业企业变更";
                    case "y":
                        return "延续注册";
                    case "z":
                        return "注销";
                    default:
                        return "";
                }
            }
        }

        /// <summary>
        /// 申请填报页面地址
        /// </summary>
        public string GetApplyTypeUrl(string type, string typesub)
        {
            if (!string.IsNullOrEmpty(typesub))
            {
                switch (typesub)
                {
                    case "个人信息变更":
                        return "zjsApplyChange.aspx";//个人信息变更注册
                    case "执业企业变更":
                        return "zjsApplyChangeUnit.aspx";//执业企业变更注册
                    case "企业信息变更":
                        return "zjsApplyChangeUnitName.aspx";
                }
            }
            switch (type)
            {
                case "初始注册":
                    return "zjsApplyFirstAdd.aspx";//初始注册
                case "延续注册":
                    return "zjsApplyContinue.aspx";//延续注册
                case "注销":
                    return "zjsApplyCancel.aspx";//注销注册
                default:
                    return "#";
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["ApplyType"] = Request["o"];

                //获取批量受理id
                ViewState["applyidlist"] = Session["applyidlist"];

                Session["applyidlist"] = "";

                ////市级受理权限||市级审核
                //if (IfExistRoleID("3") == true || IfExistRoleID("7") == true)
                //{
                //    RadComboBoxRQItem.Visible = true;
                //    RadComboBoxApplyStatus.Visible = true;
                //    RadGridQY.Columns[10].Visible=true;
                //    RadComboBoxRQItem_City.Visible = false;
                //    RadComboBoxApplyStatus_City.Visible = false;
                //}

                ////建委审核权限||建委领导审核权限
                //if (IfExistRoleID("4") == true || IfExistRoleID("6") == true || IfExistRoleID("1") == true)
                //{
                //    RadComboBoxRQItem.Visible = false;
                //    RadComboBoxApplyStatus.Visible = false;

                //    RadComboBoxRQItem_City.Visible = true;
                //    RadComboBoxApplyStatus_City.Visible = true;

                //    //TrOtherCheck.Visible = true;
                //}


                //指定查询申报日期
                if (string.IsNullOrEmpty(Request["b"]) == false)
                {
                    RadDatePickerGetDateStart.DbSelectedDate = Request["b"];
                }
                if (string.IsNullOrEmpty(Request["e"]) == false)
                {
                    RadDatePickerGetDateEnd.DbSelectedDate = Request["e"];
                }

                //申报类型
                Telerik.Web.UI.RadComboBoxItem f = RadComboBoxPSN_RegisteType.Items.FindItemByValue(ApplyType);
                if (f != null)
                {
                    f.Selected = true;
                }

                ////指定查询状态
                //if (string.IsNullOrEmpty(Request["s"]) == false)
                //{
                //    Telerik.Web.UI.RadComboBoxItem li = RadComboBoxApplyStatus.Items.FindItemByText(Request["s"]);
                //    if (li != null)
                //    {
                //        li.Selected = true;
                //    }
                //}

                ButtonSearch_Click(sender, e);
            }
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns></returns>
        protected QueryParamOB GetQueryParam()
        {
            var q = new QueryParamOB();

            //if (IfExistRoleID("3") == true || IfExistRoleID("7") == true)//区县
            //{
            //    q.Add(string.Format("[ENT_City] like '{0}%'", Region));

            //    if (RadComboBoxApplyStatus.SelectedValue == "已受理" || RadComboBoxApplyStatus.SelectedValue == "区县审核" || RadComboBoxApplyStatus.SelectedValue == "已上报" && RadComboBoxPSN_RegisteType.SelectedValue != "企业信息变更" &&RadComboBoxPSN_RegisteType.SelectedValue != "全部")
            //    {
            //        ButtonPrint.Visible = true;
            //        RadGridQY.MasterTableView.Columns[0].Visible = true;
            //    }
            //    else 
            //    {
            //        ButtonPrint.Visible = false;
            //        RadGridQY.MasterTableView.Columns[0].Visible = false;
            //    }                
            //}       

            //日期类型自定义查询条件
            if (RadDatePickerGetDateStart.SelectedDate.HasValue == true)
            {
                if (RadComboBoxRQItem_City.Visible == true)
                {
                    q.Add(string.Format("{0} >= '{1}'", RadComboBoxRQItem_City.SelectedValue, RadDatePickerGetDateStart.SelectedDate.Value.ToString("yyyy-MM-dd")));
                }
            }
            if (RadDatePickerGetDateEnd.SelectedDate.HasValue == true)
            {
                if (RadComboBoxRQItem_City.Visible == true)
                {
                    q.Add(string.Format("{0} <= '{1}'", RadComboBoxRQItem_City.SelectedValue, RadDatePickerGetDateEnd.SelectedDate.Value.ToString("yyyy-MM-dd 23:59:59")));
                }
            }

            //申请进度
            switch (RadComboBoxApplyStatus_City.SelectedValue)
            {
                case "未申报":
                    q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ZJSApplyStatus.未申报));
                    break;
                case "待确认":
                    q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ZJSApplyStatus.待确认));
                    break;
                case "已申报":
                    q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ZJSApplyStatus.已申报));
                    break;
                case "已受理":
                    q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ZJSApplyStatus.已受理));
                    break;
                case "已审核":
                    q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ZJSApplyStatus.已审核));
                    break;           
                case EnumManager.ZJSApplyStatus.已决定:
                    q.Add(string.Format(" ApplyStatus = '{0}'", EnumManager.ZJSApplyStatus.已决定));
                    break;
                case "已公告":
                    q.Add(string.Format("ApplyStatus= '{0}'", EnumManager.ZJSApplyStatus.已公告));
                    break;
                case EnumManager.ZJSApplyStatus.已驳回:
                    q.Add(string.Format(" ApplyStatus = '{0}'", EnumManager.ZJSApplyStatus.已驳回));
                    break;
                case "已放号":
                    q.Add(" [CodeMan] >''");
                    break;
            }


            //文本类型自定义查询条件
            if (RadTextBoxValue.Text.Trim() != "")
            {
                q.Add(string.Format("{0} like '%{1}%'", RadComboBoxItem.SelectedValue, RadTextBoxValue.Text.Trim().Replace("[", "[[]")));
            }

            //申报事项
            if (RadComboBoxPSN_RegisteType.SelectedValue != "全部")
            {
                if (RadComboBoxPSN_RegisteType.SelectedValue == "个人信息变更"
                   || RadComboBoxPSN_RegisteType.SelectedValue == "执业企业变更"
                   || RadComboBoxPSN_RegisteType.SelectedValue == "企业信息变更"
                   )
                {
                    q.Add(string.Format("ApplyTypesub like '{0}%'", RadComboBoxPSN_RegisteType.SelectedValue));
                }
                else
                {
                    q.Add(string.Format("ApplyType ='{0}'", RadComboBoxPSN_RegisteType.SelectedValue));
                }
            }

            //审核结果
            switch (RadComboBoxStatus_Result.SelectedValue)
            {
                case "受理通过":
                    q.Add("[GetResult] = '通过'");
                    break;
                case "受理驳回":
                    q.Add(string.Format(" ApplyStatus = '{0}' and [GetDateTime] >'1950-01-01'", EnumManager.ZJSApplyStatus.已驳回));
                    break;
                case "审核通过":
                    q.Add("[ExamineResult] = '通过'");
                    break;
                case "审核不通过":
                    q.Add("[ExamineResult] <> '通过'");
                    break;
                case "决定通过":
                    q.Add("[ConfirmResult] = '通过'");
                    break;
                case "决定不通过":
                    q.Add("[ConfirmResult] <> '通过'");
                    break;
            }

            if (string.IsNullOrEmpty(Request["id"]) == false && Request["type"] != "执业企业变更")
            {
                q.Add(string.Format("applyid in ('{0}')", Request["id"]));
            }

            if (string.IsNullOrEmpty(Request["id"]) == false && Request["type"] == "企业信息变更")
            {
                q.Add(string.Format("ApplyCode = ('{0}')", Request["id"]));   
            }
            if (ViewState["applyidlist"] != null && ViewState["applyidlist"].ToString() != "")
            {
                q.Add(string.Format("applyid in ({0})", ViewState["applyidlist"]));
            }

            return q;
        }


        //根据条件查询
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ObjectDataSource1.SelectParameters.Clear();

            QueryParamOB q = GetQueryParam();

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridQY.CurrentPageIndex = 0;
            RadGridQY.DataSourceID = ObjectDataSource1.ID;

            //spanOutput.InnerHtml = "";
        }

        public string BindApplyStatus(string applyStatus)
        {
            return applyStatus;
        }

        //导出excel
        protected void ButtonOutput_Click(object sender, EventArgs e)
        {
            QueryParamOB q = GetQueryParam();

            try
            {
                //EXCEL表头明
                string head = @"姓名\性别\证件号码\注册专业\注册编号\企业名称\机构代码\申报事项";
                //数据表的列明
                string column = @"PSN_Name\PSN_Sex\PSN_CertificateNO\PSN_RegisteProfession\PSN_RegisterNo\ENT_Name\ENT_OrganizationsCode\case ApplyType when '变更注册' then ApplyTypeSub else ApplyType end";
                //过滤条件

                if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/Excel/"))) System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Upload/Excel/"));
                string filePath = string.Format("~/Upload/Excel/{0}_{1}.xls", DateTime.Now.ToString("yyyyMMdd"), Guid.NewGuid());
                CommonDAL.OutputXls(HttpContext.Current.Server.MapPath(filePath)
                    , "zjs_Apply"
                    , q.ToWhereString(), "ApplyID", head.ToString(), column.ToString());
                string size = CommonDAL.GetFileSize(HttpContext.Current.Server.MapPath(filePath));
                spanOutput.InnerHtml = string.Format(@"<a href=""{0}"">{1}</a><span  style=""padding-left:20px;"">（{2}）</span>"
                    ,UIHelp.AddUrlReadParam( filePath.Replace("~", ".."))
                    , "点击我下载"
                    , size);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "业务查询结果导出EXCEL失败！", ex);
            }
        }
    }
}