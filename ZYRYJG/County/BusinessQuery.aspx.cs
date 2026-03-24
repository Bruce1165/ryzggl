using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Model;
using DataAccess;
using System.Data;
using System.Collections;

namespace ZYRYJG.County
{
    //业务查询
    public partial class BusinessQuery : BasePage
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
                    case "a":
                        return "增项注册";
                    case "f":
                        return "初始注册";
                    case "c":
                        return "变更注册";
                    case "y":
                        return "延期注册";
                    case "r":
                        return "重新注册";
                    case "b":
                        return "遗失补办";
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
                        return "../Unit/ApplyChange.aspx";
                    case "执业企业变更":
                        return "../Unit/ApplyChangePersonnel.aspx";
                    case "企业信息变更":
                        return "../Unit/ApplyChangeEnterprise.aspx";
                }
            }
            switch (type)
            {
                case "增项注册":
                    return "../Unit/ApplyAddItem.aspx";//增项注册
                case "初始注册":
                    return "../Unit/ApplyFirstAdd.aspx";//初始注册
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["ApplyType"] = Request["o"];

                //获取批量受理id
                ViewState["applyidlist"] = Session["applyidlist"];

                Session["applyidlist"] = "";
                //区县受理权限||区县审查
                if (IfExistRoleID("3") == true || IfExistRoleID("7") == true)
                {
                    RadComboBoxRQItem.Visible = true;
                    RadComboBoxApplyStatus.Visible = true;
                    RadGridQY.Columns[10].Visible = true;
                    RadComboBoxRQItem_City.Visible = false;
                    RadComboBoxApplyStatus_City.Visible = false;
                }



                //建委审核权限||建委领导审核权限
                if (IfExistRoleID("4") == true || IfExistRoleID("6") == true || IfExistRoleID("1") == true)
                {
                    RadComboBoxRQItem.Visible = false;
                    RadComboBoxApplyStatus.Visible = false;

                    RadComboBoxRQItem_City.Visible = true;
                    RadComboBoxApplyStatus_City.Visible = true;

                    //TrOtherCheck.Visible = true;
                }


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

                //指定查询状态
                if (string.IsNullOrEmpty(Request["s"]) == false)
                {
                    Telerik.Web.UI.RadComboBoxItem li = RadComboBoxApplyStatus.Items.FindItemByText(Request["s"]);
                    if (li != null)
                    {
                        li.Selected = true;
                    }
                }

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

            if (IfExistRoleID("3") == true || IfExistRoleID("7") == true)//区县
            {
                q.Add(string.Format("[ENT_City] like '{0}%'", Region));

                if (RadComboBoxApplyStatus.SelectedValue == "已受理" || RadComboBoxApplyStatus.SelectedValue == "区县审核" || RadComboBoxApplyStatus.SelectedValue == "已上报" && RadComboBoxPSN_RegisteType.SelectedValue != "企业信息变更" && RadComboBoxPSN_RegisteType.SelectedValue != "全部")
                {
                    ButtonPrint.Visible = true;
                    RadGridQY.MasterTableView.Columns[0].Visible = true;
                }
                else
                {
                    ButtonPrint.Visible = false;
                    RadGridQY.MasterTableView.Columns[0].Visible = false;
                }
            }

            //if (IfExistRoleID("4") == true || IfExistRoleID("6") == true||IfExistRoleID("1"))//市级
            //{                
            //}

            //日期类型自定义查询条件
            if (RadDatePickerGetDateStart.SelectedDate.HasValue == true)
            {
                if (RadComboBoxRQItem.Visible == true)
                {
                    q.Add(string.Format("{0} >= '{1}'", RadComboBoxRQItem.SelectedValue, RadDatePickerGetDateStart.SelectedDate.Value));
                }

                if (RadComboBoxRQItem_City.Visible == true)
                {
                    q.Add(string.Format("{0} >= '{1}'", RadComboBoxRQItem_City.SelectedValue, RadDatePickerGetDateStart.SelectedDate.Value.ToString("yyyy-MM-dd 23:59:59")));
                }
            }
            if (RadDatePickerGetDateEnd.SelectedDate.HasValue == true)
            {
                if (RadComboBoxRQItem.Visible == true)
                {
                    q.Add(string.Format("{0} <= '{1}'", RadComboBoxRQItem.SelectedValue, RadDatePickerGetDateEnd.SelectedDate.Value));
                }
                if (RadComboBoxRQItem_City.Visible == true)
                {
                    q.Add(string.Format("{0} <= '{1}'", RadComboBoxRQItem_City.SelectedValue, RadDatePickerGetDateEnd.SelectedDate.Value.ToString("yyyy-MM-dd 23:59:59")));
                }
            }

            //申请状态
            if (RadComboBoxApplyStatus.Visible == true)
            {
                switch (RadComboBoxApplyStatus.SelectedValue)
                {
                    case "未受理":
                        q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ApplyStatus.已申报));
                        break;
                    case EnumManager.ApplyStatus.已受理:
                        q.Add(string.Format(" ApplyStatus = '{0}'", EnumManager.ApplyStatus.已受理));
                        break;
                    case EnumManager.ApplyStatus.区县审查:
                        q.Add(string.Format(" ApplyStatus = '{0}'", EnumManager.ApplyStatus.区县审查));
                        break;
                    case EnumManager.ApplyStatus.已上报:
                        q.Add(string.Format("(ApplyStatus = '{0}' or ApplyStatus = '{1}' or ApplyStatus = '{2}' or ApplyStatus = '{3}' or ApplyStatus = '{4}')"
                            , EnumManager.ApplyStatus.已上报
                            , EnumManager.ApplyStatus.已收件
                            , EnumManager.ApplyStatus.已审查
                            , EnumManager.ApplyStatus.已决定
                            , EnumManager.ApplyStatus.已公示));
                        break;
                    case EnumManager.ApplyStatus.已驳回:
                        q.Add(string.Format(" ApplyStatus = '{0}'", EnumManager.ApplyStatus.已驳回));
                        break;
                    case "已办结":
                        q.Add(string.Format("ApplyStatus= '{0}'", EnumManager.ApplyStatus.已公告));
                        break;
                    default://全部
                        q.Add(string.Format("(ApplyStatus = '{0}' or ApplyStatus = '{1}' or ApplyStatus = '{2}' or ApplyStatus = '{3}' or ApplyStatus = '{4}' or ApplyStatus = '{5}' or ApplyStatus = '{6}' or ApplyStatus = '{7}' or ApplyStatus = '{8}')"
                            , EnumManager.ApplyStatus.已受理
                            , EnumManager.ApplyStatus.区县审查
                            , EnumManager.ApplyStatus.已上报
                            , EnumManager.ApplyStatus.已审查
                            , EnumManager.ApplyStatus.已决定
                            , EnumManager.ApplyStatus.已公示
                            , EnumManager.ApplyStatus.已申报
                            , EnumManager.ApplyStatus.已驳回
                            , EnumManager.ApplyStatus.已公告
                            ));
                        break;
                }
            }
            if (RadComboBoxApplyStatus_City.Visible == true)
            {
                //申请状态
                switch (RadComboBoxApplyStatus_City.SelectedValue)
                {
                    case "未申报":
                        q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ApplyStatus.未申报));
                        break;
                    case "待确认":
                        q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ApplyStatus.待确认));
                        break;
                    case "已申报":
                        q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ApplyStatus.已申报));
                        break;
                    case "已受理":
                        q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ApplyStatus.已受理));
                        break;
                    case "区县审查":
                        q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ApplyStatus.区县审查));
                        break;
                    case "已上报":
                        q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ApplyStatus.已上报));
                        break;
                    case EnumManager.ApplyStatus.已审查:
                        q.Add(string.Format(" ApplyStatus = '{0}'", EnumManager.ApplyStatus.已审查));
                        break;
                    case EnumManager.ApplyStatus.已决定:
                        q.Add(string.Format(" ApplyStatus = '{0}'", EnumManager.ApplyStatus.已决定));
                        break;
                    case EnumManager.ApplyStatus.已公示:
                        q.Add(string.Format(" ApplyStatus = '{0}'", EnumManager.ApplyStatus.已公示));
                        break;
                    case "已公告":
                        q.Add(string.Format("ApplyStatus= '{0}'", EnumManager.ApplyStatus.已公告));
                        break;
                    case EnumManager.ApplyStatus.已驳回:
                        q.Add(string.Format(" ApplyStatus = '{0}'", EnumManager.ApplyStatus.已驳回));
                        break;
                    case "已放号":
                        q.Add(" [CodeMan] >''");
                        break;
                    //default://全部
                    //    q.Add(string.Format("(ApplyStatus = '{0}' or ApplyStatus = '{1}' or ApplyStatus = '{2}' or ApplyStatus = '{3}' or ApplyStatus = '{4}' or ApplyStatus = '{5}')"
                    //        , EnumManager.ApplyStatus.已上报
                    //        , EnumManager.ApplyStatus.已审查
                    //        , EnumManager.ApplyStatus.已决定
                    //        , EnumManager.ApplyStatus.已公示
                    //        , EnumManager.ApplyStatus.已公告
                    //        , EnumManager.ApplyStatus.已驳回
                    //        ));
                    //break;
                }
            }

            //文本类型自定义查询条件
            if (RadTextBoxValue.Text.Trim() != "")
            {
                q.Add(string.Format("{0} like '%{1}%'", RadComboBoxItem.SelectedValue, RadTextBoxValue.Text.Trim()));
            }

            //申请类型
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
                    q.Add(string.Format(" ApplyStatus = '{0}' and [GetResult]  <> '通过'", EnumManager.ApplyStatus.已驳回));
                    break;
                case "区县审查通过":
                    q.Add("[ExamineResult] = '通过'");
                    break;
                case "区县审查不通过":
                    q.Add(string.Format(" ApplyStatus = '{0}' and [ExamineResult]  <> '通过'", EnumManager.ApplyStatus.已驳回));
                    break;
                case "审核通过":
                    q.Add("[CheckResult] = '通过'");
                    break;
                case "审核不通过":
                    q.Add("[CheckResult] <> '通过'");
                    break;
                case "决定通过":
                    q.Add("[ConfirmResult] = '通过'");
                    break;
                case "决定不通过":
                    q.Add("[ConfirmResult] <> '通过'");
                    break;
            }

            //if (CheckBoxOtherCheck.Checked == true)//专业委办局已核查
            //{
            //    q.Add("OtherDeptCheckDate >'1950-1-1'");
            //}
            if (string.IsNullOrEmpty(Request["id"]) == false && Request["type"] != "执业企业变更")
            {
                q.Add(string.Format("applyid in ('{0}')", Request["id"]));
                ButtonPrint.Visible = true;
                BtnReturn2.Visible = true;

            }

            if (string.IsNullOrEmpty(Request["id"]) == false && Request["type"] == "企业信息变更")
            {
                q.Add(string.Format("ApplyCode = ('{0}')", Request["id"]));
                ButtonPrint.Visible = true;
                BtnReturn2.Visible = true;
            }
            if (ViewState["applyidlist"] != null && ViewState["applyidlist"].ToString() != "")
            {
                q.Add(string.Format("applyid in ({0})", ViewState["applyidlist"]));
                ButtonPrint.Visible = true;
                //BtnReturn2.Visible = true;
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

            GridSortExpression sortStr1 = new GridSortExpression();
            sortStr1.FieldName = "[OldUnitCheckTime]";
            sortStr1.SortOrder = GridSortOrder.Descending;
            RadGridQY.MasterTableView.SortExpressions.AddSortExpression(sortStr1);

            spanOutput.InnerHtml = "";
        }

        public string BindApplyStatus(string applyStatus)
        {
            if (IfExistRoleID("3") == true || IfExistRoleID("7") == true)//区县
            {
                switch (applyStatus)
                {
                    case Model.EnumManager.ApplyStatus.已申报:
                        return "未受理";
                    case Model.EnumManager.ApplyStatus.已上报:
                    case Model.EnumManager.ApplyStatus.已收件:
                    case Model.EnumManager.ApplyStatus.已审查:
                    case Model.EnumManager.ApplyStatus.已决定:
                    case Model.EnumManager.ApplyStatus.已公示:
                        return "已上报";
                    case Model.EnumManager.ApplyStatus.已公告:
                        return "已办结";
                    case Model.EnumManager.ApplyStatus.已驳回:
                        return "已驳回";
                    default:
                        return applyStatus;
                }
            }
            else
            {
                switch (applyStatus)
                {
                    case Model.EnumManager.ApplyStatus.已上报:
                        return "已上报";
                    case Model.EnumManager.ApplyStatus.已审查:
                        return "已审查";
                    case Model.EnumManager.ApplyStatus.已决定:
                        return "已决定";
                    case Model.EnumManager.ApplyStatus.已公示:
                        return "已公示";
                    case Model.EnumManager.ApplyStatus.已公告:
                        return "已公告";
                    case Model.EnumManager.ApplyStatus.已驳回:
                        return "已驳回";
                    default:
                        return applyStatus;
                }
            }
        }

        //导出excel
        protected void ButtonOutput_Click(object sender, EventArgs e)
        {
            QueryParamOB q = GetQueryParam();

            try
            {
                //EXCEL表头明
                string head = @"姓名\性别\证件号码\注册专业\注册编号\企业名称\企业组织机构代码\申报类型";
                //数据表的列明
                string column = @"PSN_Name\PSN_Sex\PSN_CertificateNO\PSN_RegisteProfession\PSN_RegisterNo\ENT_Name\ENT_OrganizationsCode\case ApplyType when '变更注册' then ApplyTypeSub when '延期注册' then '延续注册' else ApplyType end";
                //过滤条件

                if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/Excel/"))) System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Upload/Excel/"));
                string filePath = string.Format("~/Upload/Excel/{0}_{1}.xls", DateTime.Now.ToString("yyyyMMdd"), Guid.NewGuid());
                CommonDAL.OutputXls(HttpContext.Current.Server.MapPath(filePath)
                    , "Apply"
                    , q.ToWhereString(), "ApplyID", head.ToString(), column.ToString());
                string size = CommonDAL.GetFileSize(HttpContext.Current.Server.MapPath(filePath));
                spanOutput.InnerHtml = string.Format(@"<a href=""{0}"">{1}</a><span  style=""padding-left:20px;"">导出结果（{2}）</span>"
                    , UIHelp.ShowFile(filePath)
                    , "点击我下载"
                    , size);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "业务查询结果导出EXCEL失败！", ex);
            }
        }

        //打印受理通知单
        protected void ButtonPrint_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();

                string applyidList = "";
                applyidList = GetRadGridADDRYSelect();
                string sourceFile = "";
                string fileName = "";
                if (Request["type"] != "企业信息变更")
                {
                    sourceFile = HttpContext.Current.Server.MapPath("~/Template/区县受理通知书.docx");
                    fileName = "北京市二级建造师区县受理通知书";
                }
                else
                {
                    sourceFile = HttpContext.Current.Server.MapPath("~/Template/企业信息变更区县受理通知书.docx");
                    fileName = "北京市二级建造师区县受理通知书";

                }


                #region 单个打印受理通知单


                if (string.IsNullOrEmpty(Request["id"]) == false && Request["type"] != "企业信息变更")
                {
                    dt = CommonDAL.GetDataTable(string.Format(@"select ApplyCode,ENT_Name,PSN_Name,PSN_Level,ApplyType,ENT_City,GetMan,CONVERT(varchar(100),GetDateTime, 23) as GetDateTime from apply where applyid in ('{0}')", Request["id"]));

                    List<Hashtable> list = new List<Hashtable>();
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        DataRow row = dt.Rows[j];
                        Hashtable hash = new Hashtable();
                        for (int i = 0; i < row.Table.Columns.Count; i++)
                        {
                            hash.Add(row.Table.Columns[i].ColumnName, row[i]);
                        }
                        list.Add(hash);

                    }

                    PrintDocument.CreateDataToWordByHashtable(sourceFile, fileName, list);

                }
                #endregion


                #region 批量打印回传受理通知单
                if (ViewState["applyidlist"] != null && ViewState["applyidlist"].ToString() != "")
                {
                    dt = CommonDAL.GetDataTable(string.Format(@"select ApplyCode,ENT_Name,PSN_Name,PSN_Level,ApplyType,ENT_City,GetMan,CONVERT(varchar(100),GetDateTime, 23) as GetDateTime from apply where applyid in ({0})", ViewState["applyidlist"]));

                    List<Hashtable> list = new List<Hashtable>();
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        DataRow row = dt.Rows[j];
                        Hashtable hash = new Hashtable();
                        for (int i = 0; i < row.Table.Columns.Count; i++)
                        {
                            hash.Add(row.Table.Columns[i].ColumnName, row[i]);
                        }
                        list.Add(hash);
                    }
                    PrintDocument.CreateDataToWordByHashtable(sourceFile, fileName, list);
                }


                #endregion

                #region 企业信息变更打印受理通知单

                if (string.IsNullOrEmpty(Request["id"]) == false && Request["type"] == "企业信息变更")
                {
                    dt = CommonDAL.GetDataTable(string.Format(@"select top 1  ApplyCode,Apply.ENT_Name,ApplyTypeSub,ENT_City,GetMan,CONVERT(varchar(100),GetDateTime, 23) as GetDateTime,ENT_NameFrom,ENT_NameTo ,FromENT_City ,ToENT_City ,FromEND_Addess,ToEND_Addess  from  Apply inner join ApplyChange on Apply.ApplyID=ApplyChange.ApplyID  where ApplyCode = ('{0}')", Request["id"]));

                    var ApplyChange = new
                    {
                        ApplyCode = dt.Rows[0]["ApplyCode"].ToString(),
                        ENT_Name = dt.Rows[0]["ENT_Name"].ToString(),
                        ApplyTypeSub = dt.Rows[0]["ApplyTypeSub"].ToString(),
                        ENT_City = dt.Rows[0]["ENT_City"].ToString(),
                        GetMan = dt.Rows[0]["GetMan"].ToString(),
                        GetDateTime = dt.Rows[0]["GetDateTime"].ToString(),
                        ENT_NameFrom = dt.Rows[0]["ENT_NameFrom"].ToString(),
                        ENT_NameTo = dt.Rows[0]["ENT_NameTo"].ToString(),
                        FromEND_Addess = dt.Rows[0]["FromEND_Addess"].ToString(),
                        ToEND_Addess = dt.Rows[0]["ToEND_Addess"].ToString(),
                        FromENT_City = dt.Rows[0]["FromENT_City"].ToString(),
                        ToENT_City = dt.Rows[0]["ToENT_City"].ToString()
                    };
                    var o = new List<object>();
                    o.Add(ApplyChange);

                    var ht = PrintDocument.GetProperties(o);
                    //拿到企业下面人员信息
                    DataTable dt2 = CommonDAL.GetDataTable(string.Format("SELECT PSN_Name,PSN_CertificateType,PSN_CertificateNO,PSN_RegisterNo FROM Apply WHERE ApplyCode= '{0}'", Request["id"]));
                    ht["tableList"] = new List<DataTable> { dt2 };
                    //表格的索引
                    ht["tableIndex"] = new List<int> { 1 };
                    //行的索引
                    ht["insertIndex"] = new List<int> { 1 };
                    ht["ContainsHeader"] = new List<bool> { true };
                    ht["isCtable"] = true;
                    PrintDocument.CreateDataToWordByHashtable(sourceFile, fileName, ht);


                }
                #endregion


                #region  批量打印受理通知单

                if (applyidList != "" && applyidList.Length > 0)
                {
                    dt = CommonDAL.GetDataTable(string.Format(@"select ApplyCode,ENT_Name,PSN_Name,PSN_Level,ApplyType,ENT_City,GetMan,CONVERT(varchar(100),GetDateTime, 23) as GetDateTime from apply where applyid in ({0})", applyidList));

                    List<Hashtable> list = new List<Hashtable>();
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        DataRow row = dt.Rows[j];
                        Hashtable hash = new Hashtable();
                        for (int i = 0; i < row.Table.Columns.Count; i++)
                        {
                            hash.Add(row.Table.Columns[i].ColumnName, row[i]);
                        }
                        list.Add(hash);

                    }

                    PrintDocument.CreateDataToWordByHashtable(sourceFile, fileName, list);

                }
                #endregion



            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "打印初始申请Word失败！", ex);
            }
        }
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

        protected void BtnReturn2_Click(object sender, EventArgs e)
        {
            if (Request["type"] != "" && Request["type"] != null)
            {
                Response.Redirect(string.Format("~/County/BusinessList.aspx?type={0}", Request["type"]), true);
            }
            else
            {
                Response.Redirect(string.Format("~/County/BusinessList.aspx?type={0}", "初始注册"), true);

            }

        }

    }
}