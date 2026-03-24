using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;
using System.Data;

namespace ZYRYJG.zjs
{
    //代办业务列表
    public partial class zjsBusinessList : BasePage
    {
        /// <summary>
        /// 异常注册记录中身份证列表（重点监管人员）
        /// </summary>
        DataTable dtYCZC_sfz
        {
            get
            {
                if (Cache["dtYCZC"] == null)
                {
                    string sql = @"select distinct [zjs_Certificate].[PSN_CertificateNO] FROM [dbo].[LockZJS]
                          inner join [dbo].[zjs_Certificate] on [LockZJS].[PSN_CertificateNO] = [zjs_Certificate].[PSN_CertificateNO]";

                    DataTable dtYCZC = CommonDAL.GetDataTable(sql);
                    DataColumn[] c = new DataColumn[1];
                    c[0] = dtYCZC.Columns[0];
                    dtYCZC.PrimaryKey = c;
                    Utility.CacheHelp.AddAbsoluteeExpirationCache(Page, "dtYCZC", dtYCZC, 12);

                    return dtYCZC;
                }
                else
                {
                    return (DataTable)Cache["dtYCZC"];
                }
            }
        }

        /// <summary>
        /// 检查身份证是否在重点监管身份证列表中
        /// </summary>
        /// <param name="sfz">身份证编号</param>
        /// <returns>true：是重点检查对象，false：不是重点检查对象</returns>
        protected bool CheckYCZC_SFZ(string sfz)
        {
            if (dtYCZC_sfz.Rows.Find(sfz) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 重点监管企业（组织机构代码）
        /// </summary>
        DataTable dtUnitWatch
        {
            get
            {
                if (Cache["dtUnitWatch"] == null)
                {
                    string sql = @"SELECT substring([CreditCode],9,9)  FROM [dbo].[UnitWatch]  where [ValidEnd]>getdate() and len([CreditCode])=18";

                    DataTable dtUnitWatch = CommonDAL.GetDataTable(sql);
                    DataColumn[] c = new DataColumn[1];
                    c[0] = dtUnitWatch.Columns[0];
                    dtUnitWatch.PrimaryKey = c;
                    Utility.CacheHelp.AddAbsoluteeExpirationCache(Page, "dtUnitWatch", dtUnitWatch, 12);

                    return dtUnitWatch;
                }
                else
                {
                    return (DataTable)Cache["dtUnitWatch"];
                }
            }
        }

        /// <summary>
        /// 检查组织机构代码是否在重点监管企业列表中
        /// </summary>
        /// <param name="ZZJGDM">组织机构代码</param>
        /// <returns>true：是重点检查企业，false：不是重点检查企业</returns>
        protected bool CheckUnitWatch(string ZZJGDM)
        {
            if (dtUnitWatch.Rows.Find(ZZJGDM.Substring(8,9)) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "zjs/zjsAgency.aspx";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //验证工商信息是否通过 
                if (IfExistRoleID("2") == true)//企业
                {
                    UnitMDL o = DataAccess.UnitDAL.GetObject(UserID);
                    if (o.ResultGSXX == 0 || o.ResultGSXX == 1 || string.IsNullOrEmpty(o.ENT_City) == true)
                    {
                        Response.Write("<script>window.location.href='../Unit/UnitMgr.aspx'</script>");
                    }
                }

                ViewState["ApplyType"] = Request.QueryString["type"];

                if (string.IsNullOrEmpty(Request["r"]) == false)//上报批次号
                {
                    ViewState["ReportCode"] = Request.QueryString["r"];
                }
                ButtonSearch_Click(sender, e);
            }
            else if (Request["__EVENTTARGET"] == "Decide")//发现决定结果与审核结果不一致，仍然继续执行决定。
            {
                string applyidlist = GetRadGridADDRYSelect();
                Decide(applyidlist);
                ButtonSearch_Click(sender, e);
            }
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请修改输入信息。");
                return;
            }

            ObjectDataSource1.SelectParameters.Clear();

            string type = ViewState["ApplyType"].ToString();
            var q = new QueryParamOB();

            if (IfExistRoleID("1") == true)//系统管理员
            {
                #region 系统管理员
                qxywy.Visible = true;
                Td_Status.Visible = true;
                RadGridQY.MasterTableView.Columns.FindByUniqueName("ExamineResult").Visible = true;
                RadGridQY.MasterTableView.Columns.FindByUniqueName("progress").Visible = true;
                q.Add("[NoticeDate] is null");//未办结

                //申报日期
                if (RadDatePickerApplyTimeStart.SelectedDate.HasValue == true)
                {
                    q.Add(string.Format("OldUnitCheckTime >= '{0}'", RadDatePickerApplyTimeStart.SelectedDate.Value.ToString("yyyy-MM-dd")));
                }
                if (RadDatePickerApplyTimeEnd.SelectedDate.HasValue == true)
                {
                    q.Add(string.Format("OldUnitCheckTime <= '{0}'", RadDatePickerApplyTimeEnd.SelectedDate.Value.ToString("yyyy-MM-dd 23:59:59")));
                }

                //受理日期
                if (RadDatePickerGetDateTimeStart.SelectedDate.HasValue == true)
                {
                    q.Add(string.Format("GetDateTime >= '{0}'", RadDatePickerGetDateTimeStart.SelectedDate.Value.ToString("yyyy-MM-dd")));
                }
                if (RadDatePickerGetDateTimeEnd.SelectedDate.HasValue == true)
                {
                    q.Add(string.Format("GetDateTime <= '{0}'", RadDatePickerGetDateTimeEnd.SelectedDate.Value.ToString("yyyy-MM-dd 23:59:59")));
                }

                //申请状态
                switch (RadComboBoxApplyStatus_City.SelectedValue)
                {
                    case "全部":
                        q.Add(string.Format("(ApplyStatus = '{0}' or ApplyStatus = '{1}' or ApplyStatus = '{2}' or ApplyStatus = '{3}' or ApplyStatus = '{4}' or ApplyStatus = '{5}' or ApplyStatus = '{6}' )"
                           , EnumManager.ZJSApplyStatus.已申报
                           , EnumManager.ZJSApplyStatus.已受理
                           , EnumManager.ZJSApplyStatus.已审核
                           , EnumManager.ZJSApplyStatus.已复核
                           , EnumManager.ZJSApplyStatus.已决定
                           , EnumManager.ZJSApplyStatus.已公告
                           , EnumManager.ZJSApplyStatus.已驳回
                           ));
                        break;
                    default:
                        q.Add(string.Format("ApplyStatus = '{0}'", RadComboBoxApplyStatus_City.SelectedValue));
                        break;
                }
                #endregion 系统管理员
            }
            else if (IfExistRoleID("23") == true)//决定
            {
                #region 决定
                zczxld.Visible = true;
                divDecide.Visible = true;
                RadioButtonListSearchExamineResult.Visible = true;
                RadGridQY.MasterTableView.Columns.FindByUniqueName("ExamineResult").Visible = true;
                RadGridQY.MasterTableView.Columns.FindByUniqueName("progress").Visible = true;

                q.Add(string.Format("ApplyStatus='{0}'", EnumManager.ZJSApplyStatus.已审核));
                //复核日期                   
                if (RadDatePickerCheckDateStart.SelectedDate.HasValue == true)
                {
                    q.Add(string.Format("ExamineDatetime >= '{0}'", RadDatePickerCheckDateStart.SelectedDate.Value));
                }
                if (RadDatePickerCheckDateEnd.SelectedDate.HasValue == true)
                {
                    q.Add(string.Format("ExamineDatetime <= '{0}'", RadDatePickerCheckDateEnd.SelectedDate.Value.ToString("yyyy-MM-dd 23:59:59")));
                }
                if (RadioButtonListSearchExamineResult.SelectedValue != "")
                {
                    q.Add(string.Format("[ExamineResult] ='{0}'", RadioButtonListSearchExamineResult.SelectedValue));
                }

                //按审核结果排序，通过放在不通过前面
                RadGridQY.MasterTableView.SortExpressions.Clear();
                Telerik.Web.UI.GridSortExpression sortStr1 = new Telerik.Web.UI.GridSortExpression();
                sortStr1.FieldName = "ExamineResult desc,ApplyID";
                sortStr1.SortOrder = Telerik.Web.UI.GridSortOrder.Ascending;
                RadGridQY.MasterTableView.SortExpressions.AddSortExpression(sortStr1);
                #endregion 决定
            }
            else if (IfExistRoleID("21") == true)//审核
            {
                #region 审核
                qxkz.Visible = true;
                divQXCK.Visible = true;
                RadGridQY.MasterTableView.Columns.FindByUniqueName("ExamineResult").Visible = true;
                RadGridQY.MasterTableView.Columns.FindByUniqueName("progress").Visible = true;

                q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ZJSApplyStatus.已受理));
                //受理日期
                if (RadDatePickerGetDateTimeStart.SelectedDate.HasValue == true)
                {
                    q.Add(string.Format("GetDateTime >= '{0}'", RadDatePickerGetDateTimeStart.SelectedDate.Value));
                }
                if (RadDatePickerGetDateTimeEnd.SelectedDate.HasValue == true)
                {
                    q.Add(string.Format("GetDateTime <= '{0}'", RadDatePickerGetDateTimeEnd.SelectedDate.Value.ToString("yyyy-MM-dd 23:59:59")));
                }
                #endregion 审核
            }
            else if (IfExistRoleID("20") == true)//受理
            {
                #region 受理
                qxywy.Visible = true;
                divQX.Visible = true;
                RadGridQY.MasterTableView.Columns.FindByUniqueName("ExamineResult").Visible = true;
                RadGridQY.MasterTableView.Columns.FindByUniqueName("progress").Visible = true;

                q.Add(string.Format("ApplyStatus='{0}' ", EnumManager.ZJSApplyStatus.已申报));
                //申报日期
                if (RadDatePickerApplyTimeStart.SelectedDate.HasValue == true)
                {
                    q.Add(string.Format("OldUnitCheckTime >= '{0}'", RadDatePickerApplyTimeStart.SelectedDate.Value));
                }
                if (RadDatePickerApplyTimeEnd.SelectedDate.HasValue == true)
                {
                    q.Add(string.Format("OldUnitCheckTime <= '{0}'", RadDatePickerApplyTimeEnd.SelectedDate.Value.ToString("yyyy-MM-dd 23:59:59")));
                }
                #endregion  受理
            }
            else if (PersonType == 3)//企业，只能看到待本企业确认的（注意：执业企业变更先原单位审核，在现单位审核）
            {
                #region 企业
                divCheckLimit.Visible = false;
                RadGridQY.Columns.FindByUniqueName("CheckLimit").Display = false;

                q.Add(string.Format("(ENT_OrganizationsCode='{0}' or ENT_OrganizationsCode like '________{0}_'  or OldEnt_QYZJJGDM like'{0}%')", SHTJXYDM));
                q.Add(string.Format("ApplyStatus='{0}'", EnumManager.ZJSApplyStatus.待确认));
                #endregion 企业
            }
            else
            {
                q.Add("1=2");
            }

            switch (type)
            {
                case "个人信息变更":
                    q.Add(string.Format("ApplyTypeSub = '{0}'", type));
                    break;
                case "执业企业变更":
                    q.Add(string.Format("ApplyTypeSub = '{0}'", type));
                    break;
                default:
                    q.Add(string.Format("ApplyType = '{0}'", type));
                    break;
            }

            if (RadTextBoxValue.Text.Trim() != "")//自定义查询项
            {
                q.Add(string.Format("{0} like '%{1}%'", RadComboBoxIten.SelectedValue, RadTextBoxValue.Text.Trim().Replace("[", "[[]")));
            }

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridQY.CurrentPageIndex = 0;
            RadGridQY.DataSourceID = ObjectDataSource1.ID;
        }

        /// <summary>
        /// 申请填报页面地址
        /// </summary>
        public string GetApplyTypeUrl(string type, string bgzc)
        {
            string URL = "";
            if (!string.IsNullOrEmpty(bgzc))
            {
                if (bgzc == "个人信息变更")
                    URL = "../zjs/zjsApplyChange.aspx";
                if (bgzc == "执业企业变更")
                    URL = "../zjs/zjsApplyChangeUnit.aspx";
            }
            switch (type)
            {
                case "初始注册":
                    return "../zjs/zjsApplyFirstAdd.aspx";//初始注册
                case "变更注册":
                    return URL;//变更注册
                case "延续注册":
                    return "../zjs/zjsApplyContinue.aspx";//延续注册
                case "注销":
                    return "../zjs/zjsApplyCancel.aspx";//注销注册
                default:
                    return "#";
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

        //批量受理
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            //获取Grid选中集合
            string applyidlist = GetRadGridADDRYSelect();

            if (applyidlist == "")
            {
                UIHelp.layerAlert(Page, "您尚未勾选任何人员！");
                return;
            }

            //#region 查询在施锁定
            //if (RadioButtonListApplyStatus.SelectedValue == "通过")
            //{
            //    int aa = DataAccess.ApplyDAL.Batch_ZSSDIsorNot(applyidlist);
            //    if (aa > 0)
            //    {
            //        UIHelp.layerAlert(Page, string.Format("勾选记录中存在{0}条在施锁定证书，请单个审批！", aa));
            //        return;
            //    }
            //}

            //#endregion

            //#region 查询是否有证书锁定的人员
            //if (RadioButtonListApplyStatus.SelectedValue == "通过")
            //{
            //    if (ViewState["ApplyType"].ToString() != "注销")//锁定允许注销
            //    {
            //        string lockidlist = GetPSN_CertificateNOGridSelect();
            //        if (lockidlist != "")
            //        {
            //            int lockcount = DataAccess.Certificate_LockDAL.Batch_LockIsorNot(lockidlist);
            //            if (lockcount > 0)
            //            {
            //                UIHelp.layerAlert(Page, string.Format("勾选记录中存在{0}条锁定证书，请单个审批！", lockcount));
            //                return;
            //            }

            //        }
            //    }
            //}
            //#endregion
            DateTime dotime = DateTime.Now;
            string LastBackResult = "";

            if (RadioButtonListApplyStatus.SelectedValue == "不通过")//不同意时记录最后驳回意见
            {
                LastBackResult = string.Format(",[LastBackResult]='{0}市级驳回申请，驳回说明：{1}'", dotime.ToString("yyyy-MM-dd HH:mm:ss"), TextBoxApplyGetResult.Text.Trim());
            }

            string sql = "";
            try
            {
                if (ViewState["ApplyType"].ToString() == EnumManager.ZJSApplyType.执业企业变更)
                {
                    sql = "Update zjs_Apply set [GetDateTime]='{0}',XGSJ='{0}',[XGR]='{1}',[GetMan]='{1}',[GetResult]='{2}',[GetRemark]='{3}',ApplyStatus='{4}'{5} where [ApplyCode] in(select [ApplyCode] from zjs_Apply  Where ApplyID in({6}))";
                }
                else
                {
                    sql = "Update zjs_Apply set [GetDateTime]='{0}',XGSJ='{0}',[XGR]='{1}',[GetMan]='{1}',[GetResult]='{2}',[GetRemark]='{3}',ApplyStatus='{4}'{5} Where ApplyID in({6})";
                }
                if (CommonDAL.ExecSQL(string.Format(sql, dotime, UserName, RadioButtonListApplyStatus.SelectedValue, TextBoxApplyGetResult.Text,
                                                  RadioButtonListApplyStatus.SelectedValue == "通过" ? EnumManager.ZJSApplyStatus.已受理 : EnumManager.ZJSApplyStatus.已驳回,
                                                   LastBackResult,applyidlist)))
                {
                    UIHelp.WriteOperateLog(UserName, UserID, "批量受理造价工程师注册申请", string.Format("ApplyID：{0}", applyidlist.Length <= 1000 ? applyidlist : applyidlist.Substring(0, 2000) + "......"));
                }
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "批量受理造价工程师注册申请失败！", ex);
                return;
            }
            ButtonSearch_Click(sender, e);
            UIHelp.layerAlert(Page, "批量受理成功！", 6, 2000);

            ////跳转至受理单打印页面
            //Session["applyidlist"] = applyidlist;
            //Response.Redirect(string.Format("zjsBusinessQuery.aspx"), true);
        }

        //批量审核
        protected void BttSave_Click(object sender, EventArgs e)
        {
            //获取Grid选中集合
            string applyidlist = GetRadGridADDRYSelect();
            if (applyidlist == "")
            {
                UIHelp.layerAlert(Page, "您尚未勾选任何人员！");
                return;
            }

            //#region 查询是否有证书锁定的人员
            //if (ViewState["ApplyType"].ToString() != "注销")//锁定允许注销
            //{
            //    string lockidlist = GetPSN_CertificateNOGridSelect();
            //    if (lockidlist != "")
            //    {
            //        int lockcount = DataAccess.Certificate_LockDAL.Batch_LockIsorNot(lockidlist);
            //        if (lockcount > 0)
            //        {
            //            UIHelp.layerAlert(Page, string.Format("勾选记录中存在{0}条锁定证书，请单个审批！", lockcount));
            //            return;
            //        }
            //    }
            //}
            //#endregion

            DateTime ExamineDatetime = DateTime.Now;
            string ExamineMan = UserName;
            string ExamineResult = RadioButtonListExamineResult.SelectedValue;
            string ExamineRemark = TextBoxExamineRemark1.Text.Trim();
            string ApplyStatus = EnumManager.ZJSApplyStatus.已审核 ;
            string sql = "";
            if (ViewState["ApplyType"].ToString() == EnumManager.ZJSApplyType.执业企业变更)
            {
                sql = "Update zjs_Apply set ExamineDatetime='{0}',XGSJ='{0}',[XGR]='{1}',ExamineMan='{1}',ExamineResult='{2}',ExamineRemark='{3}',ApplyStatus='{4}' where [ApplyCode] in(select [ApplyCode] from zjs_Apply  Where ApplyID in({5}))";
            }
            else
            {
                sql = "Update zjs_Apply set ExamineDatetime='{0}',XGSJ='{0}',[XGR]='{1}',ExamineMan='{1}',ExamineResult='{2}',ExamineRemark='{3}',ApplyStatus='{4}' Where ApplyID in({5})";
            }
          
            try
            {
                CommonDAL.ExecSQL(string.Format(sql, ExamineDatetime, ExamineMan, ExamineResult, ExamineRemark, ApplyStatus, applyidlist));
                UIHelp.WriteOperateLog(UserName, UserID, "批量审核失造价工程师注册申请", string.Format("ApplyID：{0}", applyidlist.Length <= 1000 ? applyidlist : applyidlist.Substring(0, 2000) + "......"));
                ScriptManager.RegisterStartupScript(Page, GetType(), "refresh", string.Format(@"layer.alert('{0}',{{offset:'100px',icon:{1},time:{2}}});window.location.href = window.location.href;", "批量审核成功！", 6, 2000), true);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "批量审核失造价工程师注册申请务败", ex);
            }
        }

        //批量决定
        protected void ButtonDecide_Click(object sender, EventArgs e)
        {
            //获取Grid选中集合
            string applyidlist = GetRadGridADDRYSelect();
            if (applyidlist == "")
            {
                UIHelp.layerAlert(Page, "您尚未勾选任何人员！");
                return;
            }

            //#region 查询是否有证书锁定的人员
            //if (ViewState["ApplyType"].ToString() != "注销")//锁定允许注销
            //{
            //    string lockidlist = GetPSN_CertificateNOGridSelect();
            //    if (lockidlist != "")
            //    {
            //        int lockcount = DataAccess.Certificate_LockDAL.Batch_LockIsorNot(lockidlist);
            //        if (lockcount > 0)
            //        {
            //            UIHelp.layerAlert(Page, string.Format("勾选记录中存在{0}条锁定证书，请单个审批！", lockcount));
            //            return;
            //        }
            //    }
            //}
            //#endregion

            //统计决定结果与审核结果不一致记录数量
            int ChangeCheckResultDataCount = CommonDAL.GetRowCount("zjs_Apply", "*", string.Format(" and [ExamineResult] <>'{0}' and ApplyID in({1})", RadioButtonListDecide.SelectedValue, applyidlist));
            if (ChangeCheckResultDataCount > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm",
                    string.Format(@"layer.confirm('发现{0}条记录【决定结果】与【审核结果】不一致，是否真的要继续操作？', {{btn: ['继续审核', '重新审核'],icon:3, title: '警告'}}, function () {{ __doPostBack('Decide', '');}});", ChangeCheckResultDataCount)
                    , true);
                return;
            }

            Decide(applyidlist);
            ButtonSearch_Click(sender, e);
        }

        /// <summary>
        /// 申请单决定
        /// </summary>
        /// <param name="applyidlist">勾选申请单ID集合</param>
        protected void Decide(string applyidlist)
        {
            DateTime ConfirmDate = DateTime.Now;
            string ConfirmMan = UserName;
            string ConfirmResult = RadioButtonListDecide.SelectedValue;
            string ApplyStatus = "";
            string sql = "";
            string type = ViewState["ApplyType"].ToString();

            if (type == EnumManager.ZJSApplyType.注销
                || type == EnumManager.ZJSApplyType.执业企业变更
                || type == EnumManager.ZJSApplyType.企业信息变更
                || type == EnumManager.ZJSApplyType.个人信息变更
                )
            {
                //开启事务
                DBHelper db = new DBHelper();
                DbTransaction tran = db.BeginTransaction();
                try
                {
                    ApplyStatus = EnumManager.ZJSApplyStatus.已决定;//办结

                    //更新申请表

                    if (type == EnumManager.ZJSApplyType.执业企业变更)
                    {
                        sql = "Update zjs_Apply set  ConfirmDate='{0}',XGSJ='{0}',NoticeDate='{0}',ConfirmMan='{1}',[XGR]='{1}',ConfirmResult='{2}',ApplyStatus='{3}' where [ApplyCode] in(select [ApplyCode] from zjs_Apply  Where ApplyID in({4}))";
                    }
                    else
                    {
                        sql = "Update zjs_Apply set  ConfirmDate='{0}',XGSJ='{0}',NoticeDate='{0}',ConfirmMan='{1}',[XGR]='{1}',ConfirmResult='{2}',ApplyStatus='{3}' Where  ApplyID in({4})";
                    }
                    CommonDAL.ExecSQL(tran, string.Format(sql, ConfirmDate, ConfirmMan, ConfirmResult, ApplyStatus, applyidlist));

                    if (RadioButtonListDecide.SelectedValue == "通过")
                    {
                        #region 人员表写入历史

                        if (type == EnumManager.ZJSApplyType.执业企业变更)//按申请编号（可能存在多专业）
                        {
                            sql = @"INSERT INTO [dbo].[zjs_Certificate_His]
                                ([HisID],[PSN_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[END_Addess],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Email],[PSN_Telephone],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[ApplyCATime],[SendCATime],[ReturnCATime],[CertificateCAID],[license_code],[auth_code],[SignCATime],[HisTime]) 	
                          SELECT newid(),[PSN_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[END_Addess],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Email],[PSN_Telephone],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[ApplyCATime],[SendCATime],[ReturnCATime],[CertificateCAID],[license_code],[auth_code],[SignCATime],getdate() 
                          FROM [dbo].[zjs_Certificate] 
                          where [PSN_RegisterNO] in(select [PSN_RegisterNO] from [dbo].[zjs_Apply] where [ApplyCode] in (select [ApplyCode] from zjs_Apply where ApplyID in({0})))";
                        }
                        else if (type == EnumManager.ZJSApplyType.个人信息变更)//资格证书编号未变化，按身份证号（可能存在多专业）；否则按申请单（单专业）
                        {
                            sql = @"INSERT INTO [dbo].[zjs_Certificate_His]
                                ([HisID],[PSN_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[END_Addess],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Email],[PSN_Telephone],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[ApplyCATime],[SendCATime],[ReturnCATime],[CertificateCAID],[license_code],[auth_code],[SignCATime],[HisTime]) 	
                          SELECT newid(),[PSN_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[END_Addess],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Email],[PSN_Telephone],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[ApplyCATime],[SendCATime],[ReturnCATime],[CertificateCAID],[license_code],[auth_code],[SignCATime],getdate() 
                          FROM [dbo].[zjs_Certificate] 
                          where [PSN_RegisterNO] in(select [zjs_Apply].[PSN_RegisterNO] from [dbo].[zjs_Apply] inner join [dbo].[zjs_ApplyChange] on [zjs_Apply].ApplyID = [zjs_ApplyChange].ApplyID 
                                                        where [zjs_ApplyChange].ZGZSBH !=[zjs_ApplyChange].To_ZGZSBH and [zjs_Apply].ApplyID in({0})
                                                    );

                            INSERT INTO [dbo].[zjs_Certificate_His]
                                ([HisID],[PSN_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[END_Addess],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Email],[PSN_Telephone],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[ApplyCATime],[SendCATime],[ReturnCATime],[CertificateCAID],[license_code],[auth_code],[SignCATime],[HisTime]) 	
                          SELECT newid(),[PSN_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[END_Addess],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Email],[PSN_Telephone],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[ApplyCATime],[SendCATime],[ReturnCATime],[CertificateCAID],[license_code],[auth_code],[SignCATime],getdate() 
                          FROM [dbo].[zjs_Certificate] 
                          where [PSN_CertificateNO] in(select [zjs_Apply].[PSN_CertificateNO] from [dbo].[zjs_Apply] inner join [dbo].[zjs_ApplyChange] on [zjs_Apply].ApplyID = [zjs_ApplyChange].ApplyID 
                                                        where [zjs_ApplyChange].ZGZSBH =[zjs_ApplyChange].To_ZGZSBH and [zjs_Apply].ApplyID in({0})
                                                    );";
                        }
                        else
                        {
                            sql = @"INSERT INTO [dbo].[zjs_Certificate_His]
                                ([HisID],[PSN_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[END_Addess],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Email],[PSN_Telephone],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[ApplyCATime],[SendCATime],[ReturnCATime],[CertificateCAID],[license_code],[auth_code],[SignCATime],[HisTime]) 	
                          SELECT newid(),[PSN_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[END_Addess],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Email],[PSN_Telephone],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[ApplyCATime],[SendCATime],[ReturnCATime],[CertificateCAID],[license_code],[auth_code],[SignCATime],getdate() 
                          FROM [dbo].[zjs_Certificate] 
                          where [PSN_RegisterNO] in(select [PSN_RegisterNO] from [dbo].[zjs_Apply] where ApplyID in({0}))";
                        }

                        CommonDAL.ExecSQL(tran, string.Format(sql, applyidlist));
                        #endregion

                        #region 更新人员正式表
                        switch (type)
                        {
                            case EnumManager.ZJSApplyType.注销:
                                #region 注销
                                sql = @"update [dbo].[zjs_Certificate] 
                                            set 
                                            [zjs_Certificate].[PSN_CertificateValidity] = '{0}'
                                            ,[zjs_Certificate].[PSN_RegistePermissionDate] = '{0}'
                                            ,[zjs_Certificate].[XGR]='{1}'
                                            ,[zjs_Certificate].[XGSJ]='{2}'
                                            ,[zjs_Certificate].[PSN_RegisteType]='{4}'
                                        FROM [dbo].[zjs_Apply] a 
                                        inner join [dbo].[zjs_ApplyCancel] c on a.ApplyID= c.ApplyID
                                        inner join [dbo].[zjs_Certificate] on a.PSN_CertificateNO =zjs_Certificate.PSN_CertificateNO AND  a.PSN_RegisteProfession =zjs_Certificate.PSN_RegisteProfession
                                        where a.ApplyID in({3}) and a.[ConfirmResult]='通过' and a.ApplyType='注销'";
                                CommonDAL.ExecSQL(tran, string.Format(sql, ConfirmDate.ToString("yyyy-MM-dd"), UserName, DateTime.Now, applyidlist, EnumManager.ZJSApplyTypeCode.注销));
                                #endregion 注销
                                break;
                            case EnumManager.ZJSApplyType.执业企业变更:
                                #region 执业企业变更

                                sql = @"UPDATE [dbo].[zjs_Certificate]
                                            SET [zjs_Certificate].[ENT_Name] = [zjs_Apply].ENT_Name
                                            ,[zjs_Certificate].[ENT_OrganizationsCode] = [zjs_Apply].ENT_OrganizationsCode
                                            ,[zjs_Certificate].[ENT_City] = [zjs_Apply].ENT_City 
                                            ,[zjs_Certificate].[END_Addess] = [zjs_ApplyChange].[END_Addess]                                         
                                            ,[zjs_Certificate].[PSN_RegisteType]='{4}'
                                            ,[zjs_Certificate].[PSN_RegistePermissionDate]='{0}'
                                            ,[zjs_Certificate].[XGR] = '{1}' 
                                            ,[zjs_Certificate].[XGSJ] = '{2}' 
                                        FROM [dbo].[zjs_Certificate] 
                                        inner join dbo.[zjs_Apply]
                                        on [zjs_Apply].PSN_CertificateNO =zjs_Certificate.PSN_CertificateNO AND [zjs_Apply].PSN_RegisteProfession =zjs_Certificate.PSN_RegisteProfession
                                        inner join [dbo].[zjs_ApplyChange] 
                                        on [zjs_Apply].ApplyID=[zjs_ApplyChange].ApplyID
                                        where [ApplyCode] in (select [ApplyCode] from zjs_Apply where [zjs_Apply].ApplyID in({3})) 
                                        and [zjs_Apply].[ConfirmResult]='通过' and [zjs_Apply].[ApplyTypeSub]='执业企业变更'";

                                CommonDAL.ExecSQL(tran, string.Format(sql, ConfirmDate.ToString("yyyy-MM-dd"), UserName, DateTime.Now, applyidlist, EnumManager.ZJSApplyTypeCode.变更注册));
                                #endregion 执业企业变更
                                break;
                            case EnumManager.ZJSApplyType.企业信息变更:
                                #region 企业信息变更
                                #endregion 企业信息变更
                                break;
                            case EnumManager.ZJSApplyType.个人信息变更://资格证书编号未变化，按身份证号（可能存在多专业）；否则按申请单（单专业）                      
                                #region 个人信息变更
                                sql = @"update [dbo].[zjs_Certificate] 
                                         set [zjs_Certificate].[PSN_Name] = c.[PSN_NameTo]   
                                            ,[zjs_Certificate].[PSN_Sex]=c.[ToPSN_Sex]     
                                            ,[zjs_Certificate].[PSN_BirthDate]=c.[ToPSN_BirthDate]   
                                            ,[zjs_Certificate].[PSN_CertificateNO]=c.[ToPSN_CertificateNO]
                                            ,[zjs_Certificate].[ZGZSBH]=c.[To_ZGZSBH]
                                            ,[zjs_Certificate].[PSN_RegistePermissionDate] = '{0}'
                                            ,[zjs_Certificate].[XGR]='{1}'
                                            ,[zjs_Certificate].[XGSJ]='{2}'
                                            ,[zjs_Certificate].[PSN_RegisteType]='{4}'
                                        FROM [dbo].[zjs_ApplyChange] c
                                            inner join [dbo].[zjs_Apply] a on c.ApplyID = a.ApplyID
                                            inner join [dbo].[zjs_Certificate] on a.PSN_CertificateNO =zjs_Certificate.PSN_CertificateNO AND a.PSN_RegisteProfession =zjs_Certificate.PSN_RegisteProfession
                                            where a.ApplyID in({3}) and a.ApplyTypeSub='个人信息变更' and a.[ConfirmResult]='通过' and c.ZGZSBH !=c.To_ZGZSBH;

                                        update [dbo].[zjs_Certificate] 
                                         set [zjs_Certificate].[PSN_Name] = c.[PSN_NameTo]   
                                            ,[zjs_Certificate].[PSN_Sex]=c.[ToPSN_Sex]     
                                            ,[zjs_Certificate].[PSN_BirthDate]=c.[ToPSN_BirthDate]   
                                            ,[zjs_Certificate].[PSN_CertificateNO]=c.[ToPSN_CertificateNO]                                        
                                            ,[zjs_Certificate].[PSN_RegistePermissionDate] = '{0}'
                                            ,[zjs_Certificate].[XGR]='{1}'
                                            ,[zjs_Certificate].[XGSJ]='{2}'
                                            ,[zjs_Certificate].[PSN_RegisteType]='{4}'
                                        FROM [dbo].[zjs_ApplyChange] c
                                            inner join [dbo].[zjs_Apply] a on c.ApplyID = a.ApplyID
                                            inner join [dbo].[zjs_Certificate] on a.PSN_CertificateNO =zjs_Certificate.PSN_CertificateNO 
                                            where a.ApplyID in({3}) and a.ApplyTypeSub='个人信息变更' and a.[ConfirmResult]='通过' and c.ZGZSBH =c.To_ZGZSBH;";
                                CommonDAL.ExecSQL(tran, string.Format(sql, ConfirmDate.ToString("yyyy-MM-dd"), UserName, DateTime.Now, applyidlist, EnumManager.ZJSApplyTypeCode.变更注册));
                                #endregion 个人信息变更
                                break;
                        }
                        #endregion

                        #region 更新证书附件中需要被覆盖的附件为历史附件

                        if (type == EnumManager.ZJSApplyType.执业企业变更)
                        {
                            CommonDAL.ExecSQL(tran, string.Format(@"
                                    Insert into  COC_TOW_Person_FileHistory(HisID,FileID,PSN_RegisterNO ,WriteTime) 
                                    SELECT newid(),[COC_TOW_Person_File].[FileID],[COC_TOW_Person_File].[PSN_RegisterNO],getdate()
                                    from [dbo].[COC_TOW_Person_File]
                                    inner join 
                                    (
	                                    select b.FileID, b.PSN_RegisterNO,b.DataType
	                                    from 
	                                    (
		                                    select distinct [FileInfo].DataType,[zjs_Apply].PSN_RegisterNo 
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[ApplyFile]
		                                    on [FileInfo].FileID = [ApplyFile].FileID
		                                    inner join [dbo].[zjs_Apply] on [ApplyFile].ApplyID = [zjs_Apply].ApplyID
		                                    where [ApplyCode] in (select [ApplyCode] from zjs_Apply where [zjs_Apply].ApplyID in({0})) 
	                                    ) a
	                                    inner join 
	                                    (
		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[COC_TOW_Person_File]
		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
                                            inner join [dbo].[zjs_Apply] 
                                            on [COC_TOW_Person_File].PSN_RegisterNO = [zjs_Apply].PSN_RegisterNo
		                                    where [ApplyCode] in (select [ApplyCode] from zjs_Apply where [zjs_Apply].ApplyID in({0})) 
	                                    ) b 
	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
                                    ) t
                                    on [COC_TOW_Person_File].FileID = t.FileID", applyidlist));
                        }
                        else
                        {
                            CommonDAL.ExecSQL(tran, string.Format(@"
                                    Insert into  COC_TOW_Person_FileHistory(HisID,FileID,PSN_RegisterNO ,WriteTime) 
                                    SELECT newid(),[COC_TOW_Person_File].[FileID],[COC_TOW_Person_File].[PSN_RegisterNO],getdate()
                                    from [dbo].[COC_TOW_Person_File]
                                    inner join 
                                    (
	                                    select b.FileID, b.PSN_RegisterNO,b.DataType
	                                    from 
	                                    (
		                                    select distinct [FileInfo].DataType,[zjs_Apply].PSN_RegisterNo 
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[ApplyFile]
		                                    on [FileInfo].FileID = [ApplyFile].FileID
		                                    inner join [dbo].[zjs_Apply] on [ApplyFile].ApplyID = [zjs_Apply].ApplyID
		                                    where [zjs_Apply].ApplyID in({0}) 
	                                    ) a
	                                    inner join 
	                                    (
		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[COC_TOW_Person_File]
		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
                                            inner join [dbo].[zjs_Apply] 
                                            on [COC_TOW_Person_File].PSN_RegisterNO = [zjs_Apply].PSN_RegisterNo
		                                    where  [zjs_Apply].ApplyID in({0}) 
	                                    ) b 
	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
                                    ) t
                                    on [COC_TOW_Person_File].FileID = t.FileID", applyidlist));
                        }
                        #endregion

                        #region 删除要覆盖的附件
                        if (type == EnumManager.ZJSApplyType.执业企业变更)
                        {
                            CommonDAL.ExecSQL(tran, string.Format(@"
                                    delete from [dbo].[COC_TOW_Person_File]
                                    where FileID in( select [COC_TOW_Person_File].[FileID]
                                    from [dbo].[COC_TOW_Person_File]
                                    inner join 
                                    (
	                                    select b.FileID, b.PSN_RegisterNO,b.DataType
	                                    from 
	                                    (
		                                    select distinct [FileInfo].DataType,[zjs_Apply].PSN_RegisterNo 
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[ApplyFile]
		                                    on [FileInfo].FileID = [ApplyFile].FileID
		                                    inner join [dbo].[zjs_Apply] on [ApplyFile].ApplyID = [zjs_Apply].ApplyID
		                                    where [ApplyCode] in (select [ApplyCode] from zjs_Apply where [zjs_Apply].ApplyID in({0})) 
	                                    ) a
	                                    inner join 
	                                    (
		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[COC_TOW_Person_File]
		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
                                            inner join [dbo].[zjs_Apply] 
                                            on [COC_TOW_Person_File].PSN_RegisterNO = [zjs_Apply].PSN_RegisterNo
		                                    where [ApplyCode] in (select [ApplyCode] from zjs_Apply where  [zjs_Apply].ApplyID in({0}))  
	                                    ) b 
	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
                                    ) t
                                    on [COC_TOW_Person_File].FileID = t.FileID
                                    )", applyidlist));
                        }
                        else
                        {
                            CommonDAL.ExecSQL(tran, string.Format(@"
                                    delete from [dbo].[COC_TOW_Person_File]
                                    where FileID in( select [COC_TOW_Person_File].[FileID]
                                    from [dbo].[COC_TOW_Person_File]
                                    inner join 
                                    (
	                                    select b.FileID, b.PSN_RegisterNO,b.DataType
	                                    from 
	                                    (
		                                    select distinct [FileInfo].DataType,[zjs_Apply].PSN_RegisterNo 
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[ApplyFile]
		                                    on [FileInfo].FileID = [ApplyFile].FileID
		                                    inner join [dbo].[zjs_Apply] on [ApplyFile].ApplyID = [zjs_Apply].ApplyID
		                                    where [zjs_Apply].ApplyID in({0}) 
	                                    ) a
	                                    inner join 
	                                    (
		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[COC_TOW_Person_File]
		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
                                            inner join [dbo].[zjs_Apply] 
                                            on [COC_TOW_Person_File].PSN_RegisterNO = [zjs_Apply].PSN_RegisterNo
		                                    where  [zjs_Apply].ApplyID in({0})  
	                                    ) b 
	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
                                    ) t
                                    on [COC_TOW_Person_File].FileID = t.FileID
                                    )", applyidlist));
                        }
                        #endregion

                        #region 将申请单附件写入证书附件库
                        if (type == EnumManager.ZJSApplyType.执业企业变更)
                        {
                            CommonDAL.ExecSQL(tran, string.Format(@"
                                    INSERT INTO [dbo].[COC_TOW_Person_File]([FileID],[PSN_RegisterNO],[IsHistory])
                                    select [ApplyFile].FileID,[zjs_Apply].PSN_RegisterNo,0 
                                    from [dbo].[ApplyFile]
                                    inner join [dbo].[zjs_Apply] 
                                    on [ApplyFile].ApplyID = [zjs_Apply].ApplyID 
                                    where [ApplyCode] in (select [ApplyCode] from zjs_Apply where [zjs_Apply].ApplyID in({0})) ", applyidlist));
                        }
                        else
                        {
                            CommonDAL.ExecSQL(tran, string.Format(@"
                                    INSERT INTO [dbo].[COC_TOW_Person_File]([FileID],[PSN_RegisterNO],[IsHistory])
                                    select [ApplyFile].FileID,[zjs_Apply].PSN_RegisterNo,0 
                                    from [dbo].[ApplyFile]
                                    inner join [dbo].[zjs_Apply] 
                                    on [ApplyFile].ApplyID = [zjs_Apply].ApplyID 
                                    where [zjs_Apply].ApplyID in({0}) ", applyidlist));
                        }
                        #endregion
                    }
                    tran.Commit();

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    UIHelp.WriteErrorLog(Page, "批量批准造价工程师注销申请失败", ex);
                    return;
                }
            }
            else
            {
                ApplyStatus = EnumManager.ZJSApplyStatus.已决定;
                sql = string.Format(@"Update zjs_Apply set  ConfirmDate='{0}',XGSJ='{0}',ConfirmMan='{1}',[XGR]='{1}',ConfirmResult='{2}',ApplyStatus='{3}' Where ApplyID in({4})"
                    , ConfirmDate, ConfirmMan, ConfirmResult, ApplyStatus, applyidlist);
                try
                {
                    CommonDAL.ExecSQL(sql);
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "批量批准造价工程师注册申请失败", ex);
                    return;
                }
            }
            UIHelp.WriteOperateLog(UserName, UserID, "批量批准造价工程师注销申请", string.Format("ApplyID：{0}", applyidlist.Length <= 1000 ? applyidlist : applyidlist.Substring(0, 2000) + "......"));
            //ButtonSearch_Click(sender, e);
            UIHelp.layerAlert(Page, "批量批准成功！", 6, 2000);
        }

        //获取人员id
        public string GetPSN_CertificateNOGridSelect()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i < RadGridQY.MasterTableView.Items.Count; i++)
            {
                CheckBox CheckBox1 = RadGridQY.Items[i].FindControl("CheckBox1") as CheckBox;
                string sfzh = RadGridQY.MasterTableView.DataKeyValues[i]["PSN_CertificateNO"].ToString();

                if (CheckBox1.Checked)
                {
                    sb.Append(",'").Append(RadGridQY.MasterTableView.DataKeyValues[i]["PSN_CertificateNO"].ToString()).Append("'");
                }
            }
            if (sb.Length > 0)
            {
                sb.Remove(0, 1);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 格式化审批时限
        /// </summary>
        /// <param name="ApplyType">申报类型</param>
        /// <param name="UnitApplyDate">企业上报时间</param>
        /// <param name="ExamineDatetime">市级审核时间</param>
        /// <returns></returns>
        protected string formatCheckList(object ApplyType, object UnitApplyDate, object newUnitApplyDate, object ExamineDatetime)
        {
            if (IfExistRoleID("2") == true)//企业
            {
                return "";
            }

            int limitDays = 10;//审核时限（天数）
            int WaitDays = 0;//等待审批天数
            //int level = 1;//当前所处审批级别：0区县，1市级

            WaitDays = UIHelp.ComputWorkDaySpan(Convert.ToDateTime(UnitApplyDate));

            //if (ExamineDatetime != DBNull.Value)//已到市级复核
            //{
            //    level = 1;
            //    WaitDays = UIHelp.ComputWorkDaySpan(Convert.ToDateTime(ExamineDatetime));
            //}
            //else if (UnitApplyDate != DBNull.Value)//处在市级审核
            //{
            //    level = 0;
            //    WaitDays = UIHelp.ComputWorkDaySpan(Convert.ToDateTime(UnitApplyDate));
            //}

            switch (ApplyType.ToString())
            {
                case "初始注册":
                    limitDays = 20;
                    break;
                case "执业企业变更":
                    if (UnitApplyDate != DBNull.Value && newUnitApplyDate != DBNull.Value)
                    {
                        if (Convert.ToDateTime(UnitApplyDate) > Convert.ToDateTime(newUnitApplyDate))
                        {
                            WaitDays = UIHelp.ComputWorkDaySpan(Convert.ToDateTime(UnitApplyDate));
                        }
                        else
                        {
                            WaitDays = UIHelp.ComputWorkDaySpan(Convert.ToDateTime(newUnitApplyDate));
                        }
                    }
                    break;              
                default:
                    break;
            }

            return string.Format("<span {2}>限{0}天,已过{1}天</span>", limitDays, WaitDays
                , (WaitDays > limitDays ? "style='diaplay:block;color:#FF0000;'" : (WaitDays < limitDays && WaitDays > (limitDays - 3) ? "style='diaplay:block;color:#F5AF02;'" : "")));
        }

        ////批量复核
        //protected void ButtonCheck_Click(object sender, EventArgs e)
        //{
        //    //获取Grid选中集合
        //    string applyidlist = GetRadGridADDRYSelect();
        //    if (applyidlist == "")
        //    {
        //        UIHelp.layerAlert(Page, "您尚未勾选任何人员！");
        //        return;
        //    }

        //    //#region 查询是否有证书锁定的人员
        //    //if (ViewState["ApplyType"].ToString() != "注销")//锁定允许注销
        //    //{
        //    //    string lockidlist = GetPSN_CertificateNOGridSelect();
        //    //    if (lockidlist != "")
        //    //    {
        //    //        int lockcount = DataAccess.Certificate_LockDAL.Batch_LockIsorNot(lockidlist);
        //    //        if (lockcount > 0)
        //    //        {
        //    //            UIHelp.layerAlert(Page, string.Format("勾选记录中存在{0}条锁定证书，请单个审批！", lockcount));
        //    //            return;
        //    //        }
        //    //    }
        //    //}
        //    //#endregion

        //    try
        //    {
        //        string sql = string.Format(@"Update zjs_Apply set [CheckDate]='{0}',XGSJ='{0}',[XGR]='{1}',[CheckMan]='{1}',[CheckResult]='{2}',[CheckRemark]='{3}',ApplyStatus='{4}' Where ApplyID in({5})",
        //                                          DateTime.Now,
        //                                          UserName,
        //                                          RadioButtonListCheckResult.SelectedValue,
        //                                          TextBoxApplyCheckRemark.Text,
        //                                          EnumManager.ZJSApplyStatus.已复核,
        //                                           applyidlist);
        //        if (CommonDAL.ExecSQL(sql))
        //        {
        //            UIHelp.WriteOperateLog(UserName, UserID, "批量复核造价工程师注册申请", string.Format("ApplyID：{0}", applyidlist.Length <= 1000 ? applyidlist : applyidlist.Substring(0, 2000) + "......"));
        //            ButtonSearch_Click(sender, e);
        //            UIHelp.layerAlert(Page, "批量复核成功！", 6, 2000);
        //            //ClientScript.RegisterClientScriptBlock(GetType(), "refresh", "window.location.href = window.location.href;", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        UIHelp.WriteErrorLog(Page, "批量复核造价工程师注册申请失败！", ex);
        //        return;
        //    }
        //}
    }
}