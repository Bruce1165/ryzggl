using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;
using System.Data;

namespace ZYRYJG.County
{
    //代办业务列表
    public partial class BusinessList : BasePage
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
                    string sql = @"select distinct [COC_TOW_Person_BaseInfo].[PSN_CertificateNO] FROM [dbo].[LockJZS]
                          inner join [COC_TOW_Person_BaseInfo] on [LockJZS].[PSN_ServerID] = [COC_TOW_Person_BaseInfo].[PSN_ServerID]";

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
            if (dtUnitWatch.Rows.Find(ZZJGDM) != null)
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
                return "County/Agency.aspx";
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
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            ClearGridSelectedKeys(RadGridQY);
            ObjectDataSource1.SelectParameters.Clear();

            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请修改输入信息。");
                return;
            }


            string type = ViewState["ApplyType"].ToString();
            var q = new QueryParamOB();

            if (PersonType == 3)//企业，只能看到待本企业确认的（注意：执业企业变更先原单位审核，在现单位审核）
            {
                divCheckLimit.Visible = false;
                RadGridQY.Columns.FindByUniqueName("CheckLimit").Display = false;

                q.Add(string.Format("(ENT_OrganizationsCode='{0}' or ENT_OrganizationsCode like '________{0}_'  or OldEnt_QYZJJGDM like'{0}%')", ZZJGDM));
                q.Add(string.Format("ApplyStatus='{0}'", EnumManager.ApplyStatus.待确认));
            }

            if (IfExistRoleID("3") == true)//区县业务员
            {
                qxywy.Visible = true;
                divQX.Visible = true;
                zcdj.Visible = true;
                q.Add(string.Format("ent_city like '{0}%'", Region));

                //区县不在受理增项业务2020-11-24
                q.Add(string.Format("ApplyStatus='{0}' and ApplyType <>'增项注册'", EnumManager.ApplyStatus.已申报));
                //申报日期
                if (RadDatePickerApplyTimeStart.SelectedDate.HasValue == true)
                {
                    q.Add(string.Format("OldUnitCheckTime >= '{0}'", RadDatePickerApplyTimeStart.SelectedDate.Value));
                }
                if (RadDatePickerApplyTimeEnd.SelectedDate.HasValue == true)
                {
                    q.Add(string.Format("OldUnitCheckTime <= '{0}'", RadDatePickerApplyTimeEnd.SelectedDate.Value.ToString("yyyy-MM-dd 23:59:59")));
                }
                if (RadioButtonListPsnLevel.SelectedValue != "")
                {
                    q.Add(string.Format("PSN_Level = '{0}'", RadioButtonListPsnLevel.SelectedValue));
                }
            }
            if (IfExistRoleID("7") == true)//区县领导
            {
                qxkz.Visible = true;
                divQXCK.Visible = true;
                zcdj.Visible = true;
                q.Add(string.Format("ent_city like '{0}%'", Region));
                q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ApplyStatus.已受理));
                //受理日期
                if (RadDatePickerGetDateTimeStart.SelectedDate.HasValue == true)
                {
                    q.Add(string.Format("GetDateTime >= '{0}'", RadDatePickerGetDateTimeStart.SelectedDate.Value));
                }
                if (RadDatePickerGetDateTimeEnd.SelectedDate.HasValue == true)
                {
                    q.Add(string.Format("GetDateTime <= '{0}'", RadDatePickerGetDateTimeEnd.SelectedDate.Value.ToString("yyyy-MM-dd 23:59:59")));
                }
                if (RadioButtonListPsnLevel.SelectedValue != "")
                {
                    q.Add(string.Format("PSN_Level = '{0}'", RadioButtonListPsnLevel.SelectedValue));
                }
            }
            if (IfExistRoleID("4") == true)//注册中心业务员
            {
                zczxyewy.Visible = true;
                divCheck.Visible = true;
                zcdj.Visible = true;
                RadGridQY.MasterTableView.Columns.FindByUniqueName("ReportDate").Visible = true;

                //增项改为单位确认好市建委即可审核（在途业务区建委上报也能看见）
                q.Add(string.Format("(ApplyStatus='{0}' or (ApplyType='增项注册' and ApplyStatus='{1}'))", EnumManager.ApplyStatus.已上报, EnumManager.ApplyStatus.已申报));
                //上报日期
                if (RadDatePickerAcceptDateStart.SelectedDate.HasValue == true)
                {
                    q.Add(string.Format("ReportDate >= '{0}'", RadDatePickerAcceptDateStart.SelectedDate.Value));
                }
                if (RadDatePickerAcceptDateEnd.SelectedDate.HasValue == true)
                {
                    //南静    2019-09-27   修改   修改原因：结束日期应该是   日期+23:59:59
                    //q.Add(string.Format("ReportDate <= '{0}'", RadDatePickerAcceptDateEnd.SelectedDate.Value));
                    string dtstr = RadDatePickerAcceptDateEnd.SelectedDate.Value.ToString("yyyy-MM-dd");
                    DateTime dt = Convert.ToDateTime(dtstr + " 23:59:59");
                    q.Add(string.Format("ReportDate <= '{0}'", dt));
                }
                if (RadioButtonListPsnLevel.SelectedValue != "")
                {
                    q.Add(string.Format("PSN_Level = '{0}'", RadioButtonListPsnLevel.SelectedValue));
                }

            }
            if (IfExistRoleID("6") == true)//注册中心领导
            {

                zczxld.Visible = true;
                divDecide.Visible = true;
                zcdj.Visible = true;
                RadioButtonListCountyType.Visible = true;
                RadGridQY.MasterTableView.Columns.FindByUniqueName("ReportDate").Visible = true;
                RadGridQY.MasterTableView.Columns.FindByUniqueName("CheckResult").Visible = true;

                q.Add(string.Format("ApplyStatus='{0}'", EnumManager.ApplyStatus.已审查));
                //上报日期                   
                if (RadDatePickerCheckDateStart.SelectedDate.HasValue == true)
                {
                    q.Add(string.Format("ReportDate >= '{0}'", RadDatePickerCheckDateStart.SelectedDate.Value));
                }
                if (RadDatePickerCheckDateEnd.SelectedDate.HasValue == true)
                {
                    q.Add(string.Format("ReportDate <= '{0}'", RadDatePickerCheckDateEnd.SelectedDate.Value.ToString("yyyy-MM-dd 23:59:59")));
                }
                if (RadioButtonListCountyType.SelectedValue != "")
                {
                    q.Add(string.Format("CheckResult ='{0}'", RadioButtonListCountyType.SelectedValue));
                }
                if (RadioButtonListPsnLevel.SelectedValue != "")
                {
                    q.Add(string.Format("PSN_Level = '{0}'", RadioButtonListPsnLevel.SelectedValue));
                }
            }
            if (IfExistRoleID("1") == true)//系统管理员
            {
                qxywy.Visible = true;
                Td_Status.Visible = true;
                RadGridQY.MasterTableView.Columns.FindByUniqueName("ReportDate").Visible = true;
                RadGridQY.MasterTableView.Columns.FindByUniqueName("CheckResult").Visible = true;
                RadGridQY.MasterTableView.Columns.FindByUniqueName("progress").Visible = true;
                q.Add("[NoticeDate] is null");//未办结

                //申报日期
                if (RadDatePickerApplyTimeStart.SelectedDate.HasValue == true)
                {
                    q.Add(string.Format("OldUnitCheckTime >= '{0}'", RadDatePickerApplyTimeStart.SelectedDate.Value));
                }
                if (RadDatePickerApplyTimeEnd.SelectedDate.HasValue == true)
                {
                    q.Add(string.Format("OldUnitCheckTime <= '{0}'", RadDatePickerApplyTimeEnd.SelectedDate.Value.ToString("yyyy-MM-dd 23:59:59")));
                }
                if (RadioButtonListPsnLevel.SelectedValue != "")
                {
                    q.Add(string.Format("PSN_Level = '{0}'", RadioButtonListPsnLevel.SelectedValue));
                }

                //受理日期
                if (RadDatePickerGetDateTimeStart.SelectedDate.HasValue == true)
                {
                    q.Add(string.Format("GetDateTime >= '{0}'", RadDatePickerGetDateTimeStart.SelectedDate.Value));
                }
                if (RadDatePickerGetDateTimeEnd.SelectedDate.HasValue == true)
                {
                    q.Add(string.Format("GetDateTime <= '{0}'", RadDatePickerGetDateTimeEnd.SelectedDate.Value.ToString("yyyy-MM-dd 23:59:59")));
                }

                //上报日期
                if (RadDatePickerAcceptDateStart.SelectedDate.HasValue == true)
                {
                    q.Add(string.Format("ReportDate >= '{0}'", RadDatePickerAcceptDateStart.SelectedDate.Value));
                }
                if (RadDatePickerAcceptDateEnd.SelectedDate.HasValue == true)
                {
                    //南静    2019-09-27   修改   修改原因：结束日期应该是   日期+23:59:59
                    //q.Add(string.Format("ReportDate <= '{0}'", RadDatePickerAcceptDateEnd.SelectedDate.Value));
                    string dtstr = RadDatePickerAcceptDateEnd.SelectedDate.Value.ToString("yyyy-MM-dd");
                    DateTime dt = Convert.ToDateTime(dtstr + " 23:59:59");
                    q.Add(string.Format("ReportDate <= '{0}'", dt));
                }

                //申请状态
                switch (RadComboBoxApplyStatus_City.SelectedValue)
                {
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
                    default://全部
                        q.Add(string.Format("(ApplyStatus = '{0}' or ApplyStatus = '{1}' or ApplyStatus = '{2}' or ApplyStatus = '{3}' or ApplyStatus = '{4}' or ApplyStatus = '{5}')"
                            , EnumManager.ApplyStatus.已上报
                            , EnumManager.ApplyStatus.已审查
                            , EnumManager.ApplyStatus.已决定
                            , EnumManager.ApplyStatus.已公示
                            , EnumManager.ApplyStatus.已公告
                            , EnumManager.ApplyStatus.已驳回
                            ));
                        break;
                }

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

            if (ViewState["ReportCode"] != null)//上报批次号
            {
                q.Add(string.Format("ReportCode = '{0}'", ViewState["ReportCode"]));
            }

            if (RadTextBoxValue.Text.Trim() != "")//自定义查询项
            {
                q.Add(string.Format("{0} like '%{1}%'", RadComboBoxIten.SelectedValue, RadTextBoxValue.Text.Trim()));
            }

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridQY.CurrentPageIndex = 0;
            RadGridQY.DataSourceID = ObjectDataSource1.ID;

            if (IfExistRoleID("6") == true)//注册中心领导
            {
                DataTable dtCheckResult = CommonDAL.GetDataTable(string.Format("select count(*) sl,CheckResult  FROM [dbo].[Apply]  where 1=1 {0} group by CheckResult", q.ToWhereString()));
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (DataRow r in dtCheckResult.Rows)
                {
                    sb.AppendFormat("，{0} {1} 条", r["CheckResult"], r["sl"]);
                }
                spanTJCount.InnerText = string.Format("提示：当前查询结果中{0}，请先勾选要决定的记录再进行操作。", sb);
            }

        }

        protected void RadGridQY_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            UpdateGridSelectedKeys(RadGridQY, "ApplyID");
        }

        protected void RadGridQY_DataBound(object sender, EventArgs e)
        {
            UpdateGriSelectedStatus(RadGridQY, "ApplyID");
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

        //区县批量受理
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            //NewSetUpMDL _NewSetUpMDL = new NewSetUpMDL();

            ////获取新设立企业
            //string idlist = GetRadGridSelect();
            
            UpdateGridSelectedKeys(RadGridQY, "ApplyID");

            if (IsGridSelected(RadGridQY) == false)
            {
                UIHelp.layerAlert(Page, "你还没有勾选选任数据！");
                return;
            }

            string filterString = "";//过滤条件

            if (GetGridIfCheckAll(RadGridQY) == true)//全选
            {
                filterString = ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
            }
            else
            {
                if (GetGridIfSelectedExclude(RadGridQY) == true)//排除
                    filterString = string.Format(" {0} and ApplyID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedStrKeysToString(RadGridQY));
                else//包含
                    filterString = string.Format(" {0} and ApplyID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedStrKeysToString(RadGridQY));
            }

            #region 查询是否有证书锁定的人员

            if (RadioButtonListApplyStatus.SelectedValue == "通过" && ViewState["ApplyType"].ToString() != "注销")//锁定允许注销
            {
                int lockcount = LockJZSDAL.SelectCount(string.Format(" and LockEndTime > dateadd(day,-1,getdate()) and UnlockTime is null  and [PSN_ServerID] in (select [PSN_ServerID] from Apply where 1=1 {0})", filterString));
                if (lockcount > 0)
                {
                    UIHelp.layerAlert(Page, string.Format("勾选记录中存在{0}条锁定证书，请单个审批！", lockcount));
                    return;
                }
            }
            #endregion

            #region 检查是否为新设立企业


            if (ViewState["ApplyType"].ToString() == "初始注册" || ViewState["ApplyType"].ToString() == "重新注册" || ViewState["ApplyType"].ToString() == "执业企业变更")
            {
                NewSetUpDAL.BatchInsert(filterString, ViewState["ApplyType"].ToString());
            }
            #endregion

            try
            {
                string sql = "";
                DateTime GetDateTime = DateTime.Now;
                if (RadioButtonListApplyStatus.SelectedValue == "通过")
                {
                    sql = string.Format(@"Update Apply set [GetDateTime]='{0}',XGSJ='{0}',[XGR]='{1}',[GetMan]='{1}',[GetResult]='{2}',[GetRemark]='{3}',ApplyStatus='{4}' Where  1=1 {5}",
                                                       GetDateTime,
                                                       UserName,
                                                       RadioButtonListApplyStatus.SelectedValue,
                                                       TextBoxApplyGetResult.Text,
                                                       RadioButtonListApplyStatus.SelectedValue == "通过" ? EnumManager.ApplyStatus.已受理 : EnumManager.ApplyStatus.已驳回,
                                                        filterString);
                }
                else
                {
                    sql = string.Format(@"Update Apply set [GetDateTime]='{0}',XGSJ='{0}',[XGR]='{1}',[GetMan]='{1}',[GetResult]='{2}',[GetRemark]='{3}',ApplyStatus='{4}',LastBackResult='{6}' Where  1=1 {5}",
                                                       GetDateTime,
                                                       UserName,
                                                       RadioButtonListApplyStatus.SelectedValue,
                                                       TextBoxApplyGetResult.Text,
                                                       RadioButtonListApplyStatus.SelectedValue == "通过" ? EnumManager.ApplyStatus.已受理 : EnumManager.ApplyStatus.已驳回,
                                                        filterString,
                                                        string.Format("{0}区县驳回申请，驳回说明：{1}", GetDateTime.ToString("yyyy-MM-dd HH:mm:ss"), TextBoxApplyGetResult.Text.Trim())
                                                        );
                }
                if (CommonDAL.ExecSQL(sql))
                {

                    ButtonSearch_Click(sender, e);
                    UIHelp.WriteOperateLog(UserName, UserID, "待办业务区县批量受理成功", string.Format("受理时间：{0}", DateTime.Now));

                }

            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "批量受理失败！", ex);
                return;
            }
            ////跳转至受理单打印页面
            //Session["applyidlist"] = applyidlist;
            //Response.Redirect(string.Format("BusinessQuery.aspx"), true);

            ScriptManager.RegisterStartupScript(Page, GetType(), "refresh", string.Format(@"layer.alert('{0}',{{offset:'100px',icon:{1},time:{2}}});window.location.href = window.location.href;", "批量受理成功！", 6, 2000), true);
        }

        //区县领导批量审批
        protected void BttSave_Click(object sender, EventArgs e)
        {
            UpdateGridSelectedKeys(RadGridQY, "ApplyID");

            if (IsGridSelected(RadGridQY) == false)
            {
                UIHelp.layerAlert(Page, "你还没有勾选选任数据！");
                return;
            }

            string filterString = "";//过滤条件

            if (GetGridIfCheckAll(RadGridQY) == true)//全选
            {
                filterString = ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
            }
            else
            {
                if (GetGridIfSelectedExclude(RadGridQY) == true)//排除
                    filterString = string.Format(" {0} and ApplyID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedStrKeysToString(RadGridQY));
                else//包含
                    filterString = string.Format(" {0} and ApplyID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedStrKeysToString(RadGridQY));
            }

            #region 查询是否有证书锁定的人员
            if (RadioButtonListExamineResult.SelectedValue == "通过" && ViewState["ApplyType"].ToString() != "注销")//锁定允许注销
            {
                int lockcount = LockJZSDAL.SelectCount(string.Format(" and LockEndTime > dateadd(day,-1,getdate()) and UnlockTime is null and [PSN_ServerID] in (select [PSN_ServerID] from Apply where 1=1 {0})", filterString));

                if (lockcount > 0)
                {
                    UIHelp.layerAlert(Page, string.Format("勾选记录中存在{0}条锁定证书，请单个审批！", lockcount));
                    return;
                }
            }
            #endregion

            //移动到汇总上报时校验
            ////注销不判断企业资质（新设立），允许个人注销后再其他有资质企业重新注册
            //if (ViewState["ApplyType"].ToString() != "注销")
            //{
            //    int count = CommonDAL.GetRowCount("Apply", "*", string.Format(" and ApplyID in({0}) and [CheckXSL]=1", applyidlist));
            //    if (count > 0)
            //    {
            //        UIHelp.layerAlert(Page, string.Format("勾选记录中存在{0}条新设立企业人员，请在企业资质审批合格后再来审批！",count));
            //        return;
            //    }
            //}

            DateTime ExamineDatetime = DateTime.Now;
            string ExamineMan = UserName;
            string ExamineResult = RadioButtonListExamineResult.SelectedValue;
            string ExamineRemark = TextBoxExamineRemark1.Text.Trim();
            string ApplyStatus = RadioButtonListExamineResult.SelectedValue == "通过" ? EnumManager.ApplyStatus.区县审查 : EnumManager.ApplyStatus.已驳回;
            string sql = "";
            if (RadioButtonListExamineResult.SelectedValue == "通过")
            {
                sql = string.Format(@"Update Apply set ExamineDatetime='{0}',XGSJ='{0}',[XGR]='{1}',ExamineMan='{1}',ExamineResult='{2}',ExamineRemark='{3}',ApplyStatus='{4}' Where  1=1 {5}",
                                               ExamineDatetime, ExamineMan, ExamineResult, ExamineRemark, ApplyStatus, filterString);
            }
            else
            {
                sql = string.Format(@"Update Apply set ExamineDatetime='{0}',XGSJ='{0}',[XGR]='{1}',ExamineMan='{1}',ExamineResult='{2}',ExamineRemark='{3}',ApplyStatus='{4}',LastBackResult='{6}' Where  1=1 {5}",
                                              ExamineDatetime, ExamineMan, ExamineResult, ExamineRemark, ApplyStatus, filterString,
                                                string.Format("{0}区县驳回申请，驳回说明：{1}", ExamineDatetime.ToString("yyyy-MM-dd HH:mm:ss"), TextBoxExamineRemark1.Text.Trim())
                );
            }
            try
            {
                CommonDAL.ExecSQL(sql);
                ScriptManager.RegisterStartupScript(Page, GetType(), "refresh", string.Format(@"layer.alert('{0}',{{offset:'100px',icon:{1},time:{2}}});window.location.href = window.location.href;", "批量审核成功！", 6, 2000), true);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "区县领导批量审查失败", ex);
            }
        }

        //注册中心批量审核
        protected void ButtonCheck_Click(object sender, EventArgs e)
        {
            UpdateGridSelectedKeys(RadGridQY, "ApplyID");

            if (IsGridSelected(RadGridQY) == false)
            {
                UIHelp.layerAlert(Page, "你还没有勾选选任数据！");
                return;
            }

            string filterString = "";//过滤条件

            if (GetGridIfCheckAll(RadGridQY) == true)//全选
            {
                filterString = ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
            }
            else
            {
                if (GetGridIfSelectedExclude(RadGridQY) == true)//排除
                    filterString = string.Format(" {0} and ApplyID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedStrKeysToString(RadGridQY));
                else//包含
                    filterString = string.Format(" {0} and ApplyID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedStrKeysToString(RadGridQY));
            }

            #region 查询是否有证书锁定的人员
            if (RadioButtonListCheckResult.SelectedValue == "通过" && ViewState["ApplyType"].ToString() != "注销")//锁定允许注销
            {
                int lockcount = LockJZSDAL.SelectCount(string.Format(" and LockEndTime > dateadd(day,-1,getdate()) and UnlockTime is null  and [PSN_ServerID] in (select [PSN_ServerID] from Apply where 1=1 {0})", filterString));
                if (lockcount > 0)
                {
                    UIHelp.layerAlert(Page, string.Format("勾选记录中存在{0}条锁定证书，请单个审批！", lockcount));
                    return;
                }
            }
            #endregion

            try
            {
                string sql = "";
                if (RadioButtonListCheckResult.SelectedValue == "通过")
                {
                    sql = string.Format(@"Update Apply set [CheckDate]='{0}',XGSJ='{0}',[XGR]='{1}',[CheckMan]='{1}',[CheckResult]='{2}',[CheckRemark]='{3}',ApplyStatus='{4}' Where 1=1 {5}",
                                                 DateTime.Now,
                                                 UserName,
                                                 RadioButtonListCheckResult.SelectedValue,
                                                 TextBoxApplyCheckRemark.Text,
                                                 EnumManager.ApplyStatus.已审查,
                                                  filterString);
                }
                else
                {
                    sql = string.Format(@"Update Apply set [CheckDate]='{0}',XGSJ='{0}',[XGR]='{1}',[CheckMan]='{1}',[CheckResult]='{2}',[CheckRemark]='{3}',ApplyStatus='{4}',LastBackResult='{6}' Where 1=1 {5}",
                                                 DateTime.Now,
                                                 UserName,
                                                 RadioButtonListCheckResult.SelectedValue,
                                                 TextBoxApplyCheckRemark.Text,
                                                 EnumManager.ApplyStatus.已驳回,
                                                  filterString,
                                                  string.Format("{0}市级驳回申请，驳回说明：{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), TextBoxApplyCheckRemark.Text)
                                                  );
                }
                if (CommonDAL.ExecSQL(sql))
                {
                    ButtonSearch_Click(sender, e);
                    UIHelp.WriteOperateLog(UserName, UserID, "市级批量审查二建注册成功", string.Format("注册类型：{0}，审批结果：{1}，审批意见：{2}。", ViewState["ApplyType"], RadioButtonListCheckResult.SelectedValue, TextBoxApplyCheckRemark.Text));
                    UIHelp.layerAlert(Page, "批量审查成功！", 6, 2000);
                    //ClientScript.RegisterClientScriptBlock(GetType(), "refresh", "window.location.href = window.location.href;", true);
                }
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "批量审查失败！", ex);
                return;
            }
        }

        //注册中心领导批量决定
        protected void ButtonDecide_Click(object sender, EventArgs e)
        {
            UpdateGridSelectedKeys(RadGridQY, "ApplyID");

            if (IsGridSelected(RadGridQY) == false)
            {
                UIHelp.layerAlert(Page, "你还没有勾选选任数据！");
                return;
            }

            string filterString = "";//过滤条件

            if (GetGridIfCheckAll(RadGridQY) == true)//全选
            {
                filterString = ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
            }
            else
            {
                if (GetGridIfSelectedExclude(RadGridQY) == true)//排除
                    filterString = string.Format(" {0} and ApplyID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedStrKeysToString(RadGridQY));
                else//包含
                    filterString = string.Format(" {0} and ApplyID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedStrKeysToString(RadGridQY));
            }
           
            #region 查询是否有证书锁定的人员
            if (RadioButtonListDecide.SelectedValue == "通过" && ViewState["ApplyType"].ToString() != "注销")//锁定允许注销
            {
                int lockcount = LockJZSDAL.SelectCount(string.Format(" and LockEndTime > dateadd(day,-1,getdate()) and UnlockTime is null  and [PSN_ServerID] in (select [PSN_ServerID] from Apply where 1=1 {0})", filterString));
                if (lockcount > 0)
                {
                    UIHelp.layerAlert(Page, string.Format("勾选记录中存在{0}条锁定证书，请单个审批！", lockcount));
                    return;
                }
            }
            #endregion

            DataTable dtCheckResult = CommonDAL.GetDataTable(string.Format("select count(*) sl,CheckResult  FROM [dbo].[Apply]  where 1=1 {0} group by CheckResult", filterString));
            if (dtCheckResult.Rows.Count > 1)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (DataRow r in dtCheckResult.Rows)
                {
                    sb.AppendFormat("，{0} {1} 条", r["CheckResult"], r["sl"]);
                }
                UIHelp.layerAlert(Page, string.Format("警告：当前勾选待决定申请中{0}，审核结果不唯一无法批量决定，请重新勾选你要决定的记录。", sb));
                return;
            }

            DateTime ConfirmDate = DateTime.Now;
            string ConfirmMan = UserName;
            string ConfirmResult = RadioButtonListDecide.SelectedValue;
            string ApplyStatus = "";
            string sql = "";

            if (ViewState["ApplyType"].ToString() == "注销")
            {
                #region 注销

                //开启事务
                DBHelper db = new DBHelper();
                DbTransaction tran = db.BeginTransaction();
                try
                {
                    if (RadioButtonListDecide.SelectedValue == "通过")
                    {
                        //注意：先处理注销证书，再处理注册专业。顺序不能颠倒。
                        #region 注销证书


                        //更新人员正式表
                        sql = @"update [dbo].[COC_TOW_Person_BaseInfo] 
                                    set 
                                    [COC_TOW_Person_BaseInfo].[PSN_CertificateValidity] = '{0}'
                                    ,[COC_TOW_Person_BaseInfo].[PSN_CancelPerson] = c.ApplyManType
                                    ,[COC_TOW_Person_BaseInfo].[PSN_CancelReason] =c.CancelReason
                                    ,[COC_TOW_Person_BaseInfo].[PSN_RegistePermissionDate] = '{0}'
                                    ,[COC_TOW_Person_BaseInfo].[XGR]='{1}'
                                    ,[COC_TOW_Person_BaseInfo].[XGSJ]='{0}'
                                    ,[COC_TOW_Person_BaseInfo].[PSN_RegisteType]='07'
                                FROM [dbo].[Apply] a 
                                inner join [dbo].[ApplyCancel] c on a.ApplyID= c.ApplyID
                                inner join [dbo].[COC_TOW_Person_BaseInfo] on a.PSN_ServerID = [COC_TOW_Person_BaseInfo].PSN_ServerID
                                where a.ApplyID in(select [ApplyID] from Apply where 1=1 {2}) and len(a.PSN_RegisteProfession) = len([COC_TOW_Person_BaseInfo].PSN_RegisteProfession) and a.ApplyType='注销'";
                        CommonDAL.ExecSQL(tran, string.Format(sql
                            , ConfirmDate.ToString("yyyy-MM-dd")
                            , UserName
                            , filterString));

                        #endregion

                        #region 注销专业

                        //专业写入历史表
                        sql = @"INSERT INTO [dbo].[COC_TOW_Register_Profession_His]([His_ID],[PRO_ServerID],[PSN_ServerID],[PRO_Profession],[PRO_ValidityBegin],[PRO_ValidityEnd],[DogID],[ENT_Province_Code],[DownType],[LastModifyTime],[ApplyType],[GetDate]) 
                                SELECT newid(),p.[PRO_ServerID],p.[PSN_ServerID],p.[PRO_Profession],p.[PRO_ValidityBegin],p.[PRO_ValidityEnd],p.[DogID],p.[ENT_Province_Code],p.[DownType],p.[LastModifyTime],'{0}','{1}'
                                FROM [dbo].[Apply] a 
								inner join [dbo].[COC_TOW_Person_BaseInfo] b on a.PSN_ServerID = b.PSN_ServerID
                                inner join [dbo].[COC_TOW_Register_Profession] p on a.PSN_ServerID = p.PSN_ServerID
                                where a.ApplyID in(select [ApplyID] from Apply where 1=1 {2}) and  len(a.PSN_RegisteProfession) <> len(b.PSN_RegisteProfession) and a.ApplyType='注销'";

                        CommonDAL.ExecSQL(tran, string.Format(sql, "注销", ConfirmDate, filterString));

                        //删除注销专业
                        sql = @"DELETE FROM [dbo].[COC_TOW_Register_Profession]
                                WHERE [PRO_ServerID] in
                                (
                                    SELECT  p.[PRO_ServerID]
                                    FROM [dbo].[Apply] a 
                                    inner join [dbo].[COC_TOW_Person_BaseInfo] b on a.PSN_ServerID = b.PSN_ServerID
                                    inner join [dbo].[COC_TOW_Register_Profession] p on a.PSN_ServerID = p.PSN_ServerID and PATINDEX ( '%'+ p.PRO_Profession +'%' , a.PSN_RegisteProfession )>0
                                    where a.ApplyID in(select [ApplyID] from Apply where 1=1 {0}) and  len(a.PSN_RegisteProfession) <> len(b.PSN_RegisteProfession) and a.ApplyType='注销'
                                )";

                        CommonDAL.ExecSQL(tran, string.Format(sql, filterString));

                        //人员表写入历史
                        sql = @"INSERT INTO [dbo].[COC_TOW_Person_BaseInfo_His]
                                ([HisID],[PSN_ServerID],[ENT_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[BeginTime],[EndTime],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Telephone],[PSN_Email],[PSN_PMGrade],[PSN_PMCertificateNo],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_ChangeReason],[PSN_BeforENT_Name],[PSN_BeforENT_ServerID],[PSN_BeforPersonName],[PSN_InterprovincialChange],[PSN_ExpiryReasons],[PSN_ExpiryDate],[PSN_RenewalProfession],[PSN_AddProfession],[PSN_CancelPerson],[PSN_CancelReason],[PSN_ReReasons],[PSN_ReContent],[PSN_CheckCode],[ENT_Province_Code],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[GteDate])	
                          SELECT newid(),[PSN_ServerID],[ENT_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[BeginTime],[EndTime],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Telephone],[PSN_Email],[PSN_PMGrade],[PSN_PMCertificateNo],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_ChangeReason],[PSN_BeforENT_Name],[PSN_BeforENT_ServerID],[PSN_BeforPersonName],[PSN_InterprovincialChange],[PSN_ExpiryReasons],[PSN_ExpiryDate],[PSN_RenewalProfession],[PSN_AddProfession],[PSN_CancelPerson],[PSN_CancelReason],[PSN_ReReasons],[PSN_ReContent],[PSN_CheckCode],[ENT_Province_Code],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],getdate() 
                          FROM [dbo].[COC_TOW_Person_BaseInfo] 
                          where [PSN_ServerID] in(select [PSN_ServerID] from [dbo].[Apply] where 1=1 {0})";

                        CommonDAL.ExecSQL(tran, string.Format(sql, filterString));

                        //更新人员正式表
                        sql = @"update [dbo].[COC_TOW_Person_BaseInfo] 
                                    set 
                                    [COC_TOW_Person_BaseInfo].PSN_RegisteProfession =
		                                    replace(','+
		                                    (
		                                    select ',' +[COC_TOW_Register_Profession].PRO_Profession from [dbo].[COC_TOW_Register_Profession]
		                                     where [COC_TOW_Register_Profession].PSN_ServerID = a.PSN_ServerID
		                                     for xml path('')
		                                    ),',,','')
                                    ,[COC_TOW_Person_BaseInfo].[PSN_CertificateValidity] =
		                                    (
		                                    select max([COC_TOW_Register_Profession].PRO_ValidityEnd) from [dbo].[COC_TOW_Register_Profession] where [COC_TOW_Register_Profession].PSN_ServerID = a.PSN_ServerID
		                                    ) 
                                    ,[COC_TOW_Person_BaseInfo].[PSN_CancelPerson] = c.ApplyManType
                                    ,[COC_TOW_Person_BaseInfo].[PSN_CancelReason] =c.CancelReason
                                    ,[COC_TOW_Person_BaseInfo].[PSN_RegistePermissionDate] = '{0}'
                                    ,[COC_TOW_Person_BaseInfo].[XGR]='{1}'
                                    ,[COC_TOW_Person_BaseInfo].[XGSJ]='{0}'

                                FROM [dbo].[Apply] a 
                                inner join [dbo].[ApplyCancel] c on a.ApplyID= c.ApplyID
                                inner join [dbo].[COC_TOW_Person_BaseInfo] on a.PSN_ServerID = [COC_TOW_Person_BaseInfo].PSN_ServerID
                                where a.ApplyID in(select [ApplyID] from Apply where 1=1 {2}) and len(a.PSN_RegisteProfession) <> len([COC_TOW_Person_BaseInfo].PSN_RegisteProfession) and a.ApplyType='注销'";
                        CommonDAL.ExecSQL(tran, string.Format(sql
                            , ConfirmDate.ToString("yyyy-MM-dd")
                            , UserName
                            , filterString));

                        #endregion

                    }

                    ApplyStatus = EnumManager.ApplyStatus.已公告;//办结

                    //更新申请表
                    sql = string.Format(@"Update Apply set  ConfirmDate='{0}',XGSJ='{0}',NoticeDate='{0}',ConfirmMan='{1}',[XGR]='{1}',ConfirmResult='{2}',ApplyStatus='{3}' Where 1=1 {4}",
                                                    ConfirmDate, ConfirmMan, ConfirmResult, ApplyStatus, filterString);
                    CommonDAL.ExecSQL(tran, sql);

                    tran.Commit();

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    UIHelp.WriteErrorLog(Page, "注册中心领导批量审查逐注销注册失败", ex);
                    return;
                }

                #endregion 注销
            }
            else if (ViewState["ApplyType"].ToString() == "个人信息变更")
            {
                #region 个人信息变更

                //开启事务
                DBHelper db = new DBHelper();
                DbTransaction tran = db.BeginTransaction();

                try
                { 
                    if (RadioButtonListDecide.SelectedValue == "通过")
                    {
                        //人员表写入历史
                        sql = @"INSERT INTO [dbo].[COC_TOW_Person_BaseInfo_His]
                                ([HisID],[PSN_ServerID],[ENT_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[BeginTime],[EndTime],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Telephone],[PSN_Email],[PSN_PMGrade],[PSN_PMCertificateNo],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_ChangeReason],[PSN_BeforENT_Name],[PSN_BeforENT_ServerID],[PSN_BeforPersonName],[PSN_InterprovincialChange],[PSN_ExpiryReasons],[PSN_ExpiryDate],[PSN_RenewalProfession],[PSN_AddProfession],[PSN_CancelPerson],[PSN_CancelReason],[PSN_ReReasons],[PSN_ReContent],[PSN_CheckCode],[ENT_Province_Code],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[GteDate])	
                          SELECT newid(),[PSN_ServerID],[ENT_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[BeginTime],[EndTime],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Telephone],[PSN_Email],[PSN_PMGrade],[PSN_PMCertificateNo],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_ChangeReason],[PSN_BeforENT_Name],[PSN_BeforENT_ServerID],[PSN_BeforPersonName],[PSN_InterprovincialChange],[PSN_ExpiryReasons],[PSN_ExpiryDate],[PSN_RenewalProfession],[PSN_AddProfession],[PSN_CancelPerson],[PSN_CancelReason],[PSN_ReReasons],[PSN_ReContent],[PSN_CheckCode],[ENT_Province_Code],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],getdate() 
                          FROM [dbo].[COC_TOW_Person_BaseInfo] 
                          where [PSN_ServerID] in(select [PSN_ServerID] from [dbo].[Apply] where 1=1 {0})";

                        CommonDAL.ExecSQL(tran, string.Format(sql, filterString));

//                        //更新人员正式表
//                        sql = @"update [dbo].[COC_TOW_Person_BaseInfo] 
//                         set 
//                            [COC_TOW_Person_BaseInfo].[PSN_Name] = c.[PSN_NameTo] 
//                            ,[COC_TOW_Person_BaseInfo].[PSN_BeforPersonName]=c.[PSN_NameFrom]    
//                            ,[COC_TOW_Person_BaseInfo].[PSN_Sex]=c.[ToPSN_Sex]     
//                            ,[COC_TOW_Person_BaseInfo].[PSN_BirthDate]=c.[ToPSN_BirthDate]   
//                            ,[COC_TOW_Person_BaseInfo].[PSN_CertificateNO]=c.[ToPSN_CertificateNO]
//                            ,[COC_TOW_Person_BaseInfo].[ZGZSBH]=c.[To_ZGZSBH]
//                            ,[COC_TOW_Person_BaseInfo].[PSN_RegistePermissionDate] = '{0}'
//                            ,[COC_TOW_Person_BaseInfo].[XGR]='{1}'
//                            ,[COC_TOW_Person_BaseInfo].[XGSJ]='{0}'
//                            ,[COC_TOW_Person_BaseInfo].[PSN_ChangeReason]='个人信息变更'
//                            ,[COC_TOW_Person_BaseInfo].[PSN_RegisteType]='02'
//                        FROM [dbo].[ApplyChange] c
//                          inner join [dbo].[Apply] a on c.ApplyID = a.ApplyID
//                          inner join [dbo].[COC_TOW_Person_BaseInfo] on a.PSN_ServerID = [COC_TOW_Person_BaseInfo].PSN_ServerID
//                          where a.ApplyTypeSub='个人信息变更' and a.ApplyID in(select [ApplyID] from Apply where 1=1 {2})";
//                        CommonDAL.ExecSQL(tran, string.Format(sql
//                            , ConfirmDate.ToString("yyyy-MM-dd")
//                            , UserName
//                            , filterString));

                        //更新人员正式表
                        sql = @"update [dbo].[COC_TOW_Person_BaseInfo] 
                         set                             
                            [COC_TOW_Person_BaseInfo].[PSN_RegistePermissionDate] = '{0}'
                            ,[COC_TOW_Person_BaseInfo].[XGR]='{1}'
                            ,[COC_TOW_Person_BaseInfo].[XGSJ]='{0}'
                            ,[COC_TOW_Person_BaseInfo].[PSN_ChangeReason]='个人信息变更'
                            ,[COC_TOW_Person_BaseInfo].[PSN_RegisteType]='02'
                        FROM [dbo].[ApplyChange] c
                          inner join [dbo].[Apply] a on c.ApplyID = a.ApplyID
                          inner join [dbo].[COC_TOW_Person_BaseInfo] on a.PSN_ServerID = [COC_TOW_Person_BaseInfo].PSN_ServerID
                          where a.ApplyTypeSub='个人信息变更' and a.ApplyID in(select [ApplyID] from Apply where 1=1 {2})";
                        CommonDAL.ExecSQL(tran, string.Format(sql
                            , ConfirmDate.ToString("yyyy-MM-dd")
                            , UserName
                            , filterString));


                        //更新证书附件中需要被覆盖的附件为历史附件
                        CommonDAL.ExecSQL(tran, string.Format(@"
                                    Insert into  COC_TOW_Person_FileHistory(HisID,FileID,PSN_RegisterNO ,WriteTime) 
                                    SELECT newid(),[COC_TOW_Person_File].[FileID],[COC_TOW_Person_File].[PSN_RegisterNO],getdate()
                                    from [dbo].[COC_TOW_Person_File]
                                    inner join 
                                    (
	                                    select b.FileID, b.PSN_RegisterNO,b.DataType
	                                    from 
	                                    (
		                                    select distinct [FileInfo].DataType,[Apply].PSN_RegisterNo 
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[ApplyFile]
		                                    on [FileInfo].FileID = [ApplyFile].FileID
		                                    inner join [dbo].[Apply] on [ApplyFile].ApplyID = [Apply].ApplyID
		                                    where [Apply].ApplyID in(select [ApplyID] from [dbo].[Apply] where 1=1 {0}) 
	                                    ) a
	                                    inner join 
	                                    (
		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[COC_TOW_Person_File]
		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
                                            inner join [dbo].[Apply] 
                                            on [COC_TOW_Person_File].PSN_RegisterNO = [Apply].PSN_RegisterNo
		                                    where  [Apply].ApplyID  in(select [ApplyID] from [dbo].[Apply] where 1=1 {0})
	                                    ) b 
	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
                                    ) t
                                    on [COC_TOW_Person_File].FileID = t.FileID", filterString));


                        CommonDAL.ExecSQL(tran, string.Format(@"
                                    delete from [dbo].[COC_TOW_Person_File]
                                    where FileID in( select [COC_TOW_Person_File].[FileID]
                                    from [dbo].[COC_TOW_Person_File]
                                    inner join 
                                    (
	                                    select b.FileID, b.PSN_RegisterNO,b.DataType
	                                    from 
	                                    (
		                                    select distinct [FileInfo].DataType,[Apply].PSN_RegisterNo 
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[ApplyFile]
		                                    on [FileInfo].FileID = [ApplyFile].FileID
		                                    inner join [dbo].[Apply] on [ApplyFile].ApplyID = [Apply].ApplyID
		                                    where [Apply].ApplyID in(select [ApplyID] from [dbo].[Apply] where 1=1 {0})
	                                    ) a
	                                    inner join 
	                                    (
		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[COC_TOW_Person_File]
		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
                                            inner join [dbo].[Apply] 
                                            on [COC_TOW_Person_File].PSN_RegisterNO = [Apply].PSN_RegisterNo
		                                    where  [Apply].ApplyID in(select [ApplyID] from [dbo].[Apply] where 1=1 {0}) 
	                                    ) b 
	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
                                    ) t
                                    on [COC_TOW_Person_File].FileID = t.FileID
                                    )", filterString));

                        //将申请单附件写入证书附件库
                        CommonDAL.ExecSQL(tran, string.Format(@"
                                    INSERT INTO [dbo].[COC_TOW_Person_File]([FileID],[PSN_RegisterNO],[IsHistory])
                                    select [ApplyFile].FileID,[Apply].PSN_RegisterNo,0 
                                    from [dbo].[ApplyFile]
                                    inner join [dbo].[Apply] 
                                    on [ApplyFile].ApplyID = [Apply].ApplyID 
                                    where [Apply].ApplyID in(select [ApplyID] from [dbo].[Apply] where 1=1 {0})  ", filterString));
                    }
                    ApplyStatus = EnumManager.ApplyStatus.已公告;//办结

                    //更新申请表
                    sql = string.Format(@"Update Apply set  ConfirmDate='{0}',XGSJ='{0}',NoticeDate='{0}',ConfirmMan='{1}',[XGR]='{1}',ConfirmResult='{2}',ApplyStatus='{3}' Where  1=1 {4}",
                                                    ConfirmDate, ConfirmMan, ConfirmResult, ApplyStatus, filterString);
                    CommonDAL.ExecSQL(tran, sql);
                    tran.Commit();

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    UIHelp.WriteErrorLog(Page, "注册中心领导批量审查个人信息变更失败", ex);
                    return;
                }

                #endregion 个人信息变更
            }
            else if (ViewState["ApplyType"].ToString() == "增项注册")
            {
                #region 增项注册

                //开启事务
                DBHelper db = new DBHelper();
                DbTransaction tran = db.BeginTransaction();

                try
                {
                    if (RadioButtonListDecide.SelectedValue == "通过")
                    {
                        #region 决定通过

                        //专业写入历史表
                        sql = @"INSERT INTO [dbo].[COC_TOW_Register_Profession_His]([His_ID],[PRO_ServerID],[PSN_ServerID],[PRO_Profession],[PRO_ValidityBegin],[PRO_ValidityEnd],[DogID],[ENT_Province_Code],[DownType],[LastModifyTime],[ApplyType],[GetDate]) 
                                SELECT newid(),p.[PRO_ServerID],p.[PSN_ServerID],p.[PRO_Profession],p.[PRO_ValidityBegin],p.[PRO_ValidityEnd],p.[DogID],p.[ENT_Province_Code],p.[DownType],p.[LastModifyTime],'{0}','{1}'
                                FROM [dbo].[Apply] a 	
                                inner join [dbo].[COC_TOW_Register_Profession] p on a.PSN_ServerID = p.PSN_ServerID
                                where a.ApplyID in(select [ApplyID] from Apply where 1=1 {2}) ";

                        CommonDAL.ExecSQL(tran, string.Format(sql, "增项注册", ConfirmDate.ToString("yyyy-MM-dd HH:mm:ss"), filterString));

                        //删除过期的增项专业
                        sql = @"DELETE FROM [dbo].[COC_TOW_Register_Profession]
                                WHERE [PRO_ServerID] in
                                (
                                    SELECT  p.[PRO_ServerID]
                                    FROM [dbo].[Apply] a 
                                    inner join [dbo].[ApplyAddItem] b on a.ApplyID = b.ApplyID
                                    inner join [dbo].[COC_TOW_Register_Profession] p on a.PSN_ServerID = p.PSN_ServerID and (p.PRO_Profession = b.[AddItem1] or p.PRO_Profession = b.[AddItem2])
                                    where  a.ApplyID in(select [ApplyID] from Apply where 1=1 {0})
                                )";

                        CommonDAL.ExecSQL(tran, string.Format(sql, filterString));

                        //写入增项专业表
                        sql = @"INSERT INTO [dbo].[COC_TOW_Register_Profession]([PRO_ServerID],[PSN_ServerID],[PRO_Profession],[PRO_ValidityBegin],[PRO_ValidityEnd],[ENT_Province_Code],[LastModifyTime])
                                  select newid(),a.PSN_ServerID,ZY,convert(char(10),'{1}',120),[dbo].[GET_PSN_CertificateValidity](convert(char(10),dateadd(day,-1,dateadd(year,3,'{1}')),120),a.PSN_CertificateNO,a.PSN_Level),'110000','{1}'
                                  FROM [dbo].[Apply] a 
                                  inner join
                                  (
                                      select applyid,[AddItem1] ZY from  [dbo].[ApplyAddItem] where [AddItem1] is not null
                                      union
                                      select applyid,[AddItem2] ZY from  [dbo].[ApplyAddItem] where [AddItem2] is not null
                                  ) z on  a.applyid = z.applyid
                                   where a.ApplyID in(select [ApplyID] from Apply where 1=1 {0})";
                        CommonDAL.ExecSQL(tran, string.Format(sql, filterString, ConfirmDate.ToString("yyyy-MM-dd HH:mm:ss")));

                        //人员表写入历史
                        sql = @"INSERT INTO [dbo].[COC_TOW_Person_BaseInfo_His]
                                ([HisID],[PSN_ServerID],[ENT_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[BeginTime],[EndTime],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Telephone],[PSN_Email],[PSN_PMGrade],[PSN_PMCertificateNo],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_ChangeReason],[PSN_BeforENT_Name],[PSN_BeforENT_ServerID],[PSN_BeforPersonName],[PSN_InterprovincialChange],[PSN_ExpiryReasons],[PSN_ExpiryDate],[PSN_RenewalProfession],[PSN_AddProfession],[PSN_CancelPerson],[PSN_CancelReason],[PSN_ReReasons],[PSN_ReContent],[PSN_CheckCode],[ENT_Province_Code],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[GteDate])	
                          SELECT newid(),[PSN_ServerID],[ENT_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[BeginTime],[EndTime],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Telephone],[PSN_Email],[PSN_PMGrade],[PSN_PMCertificateNo],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_ChangeReason],[PSN_BeforENT_Name],[PSN_BeforENT_ServerID],[PSN_BeforPersonName],[PSN_InterprovincialChange],[PSN_ExpiryReasons],[PSN_ExpiryDate],[PSN_RenewalProfession],[PSN_AddProfession],[PSN_CancelPerson],[PSN_CancelReason],[PSN_ReReasons],[PSN_ReContent],[PSN_CheckCode],[ENT_Province_Code],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],getdate() 
                          FROM [dbo].[COC_TOW_Person_BaseInfo] 
                          where [PSN_ServerID] in(select [PSN_ServerID] from [dbo].[Apply] where 1=1 {0})";

                        CommonDAL.ExecSQL(tran, string.Format(sql, filterString));

                        //更新人员正式表
                        sql = @"update [dbo].[COC_TOW_Person_BaseInfo] 
                                    set 
                                    [COC_TOW_Person_BaseInfo].PSN_RegisteProfession =
		                                    replace(','+
		                                    (
		                                    select ',' +[COC_TOW_Register_Profession].PRO_Profession from [dbo].[COC_TOW_Register_Profession]
		                                     where [COC_TOW_Register_Profession].PSN_ServerID = a.PSN_ServerID
		                                     for xml path('')
		                                    ),',,','')
                                    ,[COC_TOW_Person_BaseInfo].[PSN_CertificateValidity] =
		                                    (
		                                    select max([COC_TOW_Register_Profession].PRO_ValidityEnd) from [dbo].[COC_TOW_Register_Profession] where [COC_TOW_Register_Profession].PSN_ServerID = a.PSN_ServerID
		                                    )               
                                    ,[COC_TOW_Person_BaseInfo].[PSN_RegistePermissionDate] = '{0}'
                                    ,[COC_TOW_Person_BaseInfo].[PSN_RegisteType]='04'
                                    ,[COC_TOW_Person_BaseInfo].[XGR]='{1}'
                                    ,[COC_TOW_Person_BaseInfo].[XGSJ]='{0}'
                                    ,[COC_TOW_Person_BaseInfo].[PSN_AddProfession]=                                   
		                                    (
		                                        select [ApplyAddItem].AddItem1 + case when [ApplyAddItem].AddItem2 is null then '' else ',' +[ApplyAddItem].AddItem2 end
                                                from [dbo].[ApplyAddItem]
		                                        where [ApplyAddItem].Applyid = a.Applyid		                                   
		                                    )
                                FROM [dbo].[COC_TOW_Person_BaseInfo] inner join [dbo].[Apply] a 
                                on [COC_TOW_Person_BaseInfo].[PSN_RegisterNO] = a.[PSN_RegisterNO]
                                where  a.ApplyID in(select [ApplyID] from Apply where 1=1 {2})";


                        CommonDAL.ExecSQL(tran, string.Format(sql
                            , ConfirmDate.ToString("yyyy-MM-dd HH:mm:ss")
                            , UserName
                            , filterString));

                        //更新证书附件中需要被覆盖的附件为历史附件
                        CommonDAL.ExecSQL(tran, string.Format(@"
                                    Insert into  COC_TOW_Person_FileHistory(HisID,FileID,PSN_RegisterNO ,WriteTime) 
                                    SELECT newid(),[COC_TOW_Person_File].[FileID],[COC_TOW_Person_File].[PSN_RegisterNO],getdate()
                                    from [dbo].[COC_TOW_Person_File]
                                    inner join 
                                    (
	                                    select b.FileID, b.PSN_RegisterNO,b.DataType
	                                    from 
	                                    (
		                                    select distinct [FileInfo].DataType,[Apply].PSN_RegisterNo 
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[ApplyFile]
		                                    on [FileInfo].FileID = [ApplyFile].FileID
		                                    inner join [dbo].[Apply] on [ApplyFile].ApplyID = [Apply].ApplyID
		                                    where [Apply].ApplyID in(select [ApplyID] from Apply where 1=1 {0})
	                                    ) a
	                                    inner join 
	                                    (
		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[COC_TOW_Person_File]
		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
                                            inner join [dbo].[Apply] 
                                            on [COC_TOW_Person_File].PSN_RegisterNO = [Apply].PSN_RegisterNo
		                                    where  [Apply].ApplyID in(select [ApplyID] from Apply where 1=1 {0})
	                                    ) b 
	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
                                    ) t
                                    on [COC_TOW_Person_File].FileID = t.FileID", filterString));


                        CommonDAL.ExecSQL(tran, string.Format(@"
                                    delete from [dbo].[COC_TOW_Person_File]
                                    where FileID in( select [COC_TOW_Person_File].[FileID]
                                    from [dbo].[COC_TOW_Person_File]
                                    inner join 
                                    (
	                                    select b.FileID, b.PSN_RegisterNO,b.DataType
	                                    from 
	                                    (
		                                    select distinct [FileInfo].DataType,[Apply].PSN_RegisterNo 
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[ApplyFile]
		                                    on [FileInfo].FileID = [ApplyFile].FileID
		                                    inner join [dbo].[Apply] on [ApplyFile].ApplyID = [Apply].ApplyID
		                                    where [Apply].ApplyID in(select [ApplyID] from Apply where 1=1 {0})
	                                    ) a
	                                    inner join 
	                                    (
		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[COC_TOW_Person_File]
		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
                                            inner join [dbo].[Apply] 
                                            on [COC_TOW_Person_File].PSN_RegisterNO = [Apply].PSN_RegisterNo
		                                    where  [Apply].ApplyID in(select [ApplyID] from Apply where 1=1 {0})
	                                    ) b 
	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
                                    ) t
                                    on [COC_TOW_Person_File].FileID = t.FileID
                                    )", filterString));

                        //将申请单附件写入证书附件库
                        CommonDAL.ExecSQL(tran, string.Format(@"
                                    INSERT INTO [dbo].[COC_TOW_Person_File]([FileID],[PSN_RegisterNO],[IsHistory])
                                    select [ApplyFile].FileID,[Apply].PSN_RegisterNo,0 
                                    from [dbo].[ApplyFile]
                                    inner join [dbo].[Apply] 
                                    on [ApplyFile].ApplyID = [Apply].ApplyID 
                                    where [Apply].ApplyID in(select [ApplyID] from Apply where 1=1 {0})", filterString));
                        #endregion
                    }
                    //更新申请表
                    ApplyStatus = EnumManager.ApplyStatus.已公告;//办结                   
                    sql = string.Format(@"Update Apply set  ConfirmDate='{0}',XGSJ='{0}',NoticeDate='{0}',ConfirmMan='{1}',[XGR]='{1}',ConfirmResult='{2}',ApplyStatus='{3}' Where  1=1 {4}",
                                                    ConfirmDate.ToString("yyyy-MM-dd HH:mm:ss"), ConfirmMan, ConfirmResult, ApplyStatus, filterString);
                    CommonDAL.ExecSQL(tran, sql);

                    tran.Commit();

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    UIHelp.WriteErrorLog(Page, "注册中心领导批量审查个人信息变更失败", ex);
                    return;
                }

                #endregion 增项注册
            }
            //else if (ViewState["ApplyType"].ToString() == "遗失补办")
            //{
            //    #region 遗失补办

        //    ApplyStatus = EnumManager.ApplyStatus.已公告;//办结

        //    //更新申请表
            //    sql = string.Format(@"Update Apply set  ConfirmDate='{0}',XGSJ='{0}',NoticeDate='{0}',ConfirmMan='{1}',[XGR]='{1}',ConfirmResult='{2}',ApplyStatus='{3}' Where  ApplyID in({4})",
            //                                    ConfirmDate, ConfirmMan, ConfirmResult, ApplyStatus, applyidlist);

        //    try
            //    {
            //        CommonDAL.ExecSQL(sql);
            //    }
            //    catch (Exception ex)
            //    {
            //        UIHelp.WriteErrorLog(Page, "注册中心领导批量审查失败", ex);
            //        return;
            //    }


        //    #endregion 遗失补办
            //}
            else//其他
            {
                ApplyStatus = EnumManager.ApplyStatus.已决定;
                sql = string.Format(@"Update Apply set  ConfirmDate='{0}',XGSJ='{0}',ConfirmMan='{1}',[XGR]='{1}',ConfirmResult='{2}',ApplyStatus='{3}' Where 1=1 {4}",
                                                 ConfirmDate, ConfirmMan, ConfirmResult, ApplyStatus, filterString);
                try
                {
                    CommonDAL.ExecSQL(sql);
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "注册中心领导批量决定失败", ex);
                    return;
                }
            }

            ButtonSearch_Click(sender, e);
            UIHelp.WriteOperateLog(UserName, UserID, "待办业务建委批量决定成功", string.Format("决定时间：{0}", DateTime.Now));
            UIHelp.layerAlert(Page, "批量决定成功！", 6, 2000);
            //ClientScript.RegisterClientScriptBlock(GetType(), "refresh", "window.location.href = window.location.href;", true);

        }


        /// <summary>
        /// 格式化审批时限
        /// </summary>
        /// <param name="ApplyType">申报类型</param>
        /// <param name="UnitApplyDate">企业上报时间</param>
        /// <param name="UpReportDate">区县上报时间</param>
        /// <returns></returns>
        protected string formatCheckList(object ApplyType, object UnitApplyDate, object newUnitApplyDate, object UpReportDate)
        {
            if (IfExistRoleID("2") == true)//企业
            {
                return "";
            }

            int limitDays = 5;//审核时限（天数）
            int WaitDays = 0;//等待审批天数
            int level = 0;//当前所处审批级别：0区县，1市级

            if (UpReportDate != DBNull.Value)//已到市级审批
            {
                level = 1;
                //WaitDays =Convert.ToInt32((DateTime.Now.Date- Convert.ToDateTime(UpReportDate).Date).TotalDays);
                WaitDays = UIHelp.ComputWorkDaySpan(Convert.ToDateTime(UpReportDate));
            }
            else if (UnitApplyDate != DBNull.Value)//处在县级审批
            {
                //WaitDays =Convert.ToInt32((DateTime.Now.Date- Convert.ToDateTime(UnitApplyDate).Date).TotalDays);
                WaitDays = UIHelp.ComputWorkDaySpan(Convert.ToDateTime(UnitApplyDate));
            }

            switch (ApplyType.ToString())
            {
                case "初始注册":
                    limitDays = 10;
                    break;
                case "个人信息变更":
                    limitDays = 5;
                    break;
                case "执业企业变更":
                    limitDays = 5;
                    if (UnitApplyDate != DBNull.Value && newUnitApplyDate != DBNull.Value && level == 0)//处在县级审批
                    {
                        if (Convert.ToDateTime(UnitApplyDate) > Convert.ToDateTime(newUnitApplyDate))
                        {
                            //WaitDays = Convert.ToInt32((DateTime.Now.Date - Convert.ToDateTime(UnitApplyDate).Date).TotalDays);
                            WaitDays = UIHelp.ComputWorkDaySpan(Convert.ToDateTime(UnitApplyDate));
                        }
                        else
                        {
                            //WaitDays = Convert.ToInt32((DateTime.Now.Date - Convert.ToDateTime(newUnitApplyDate).Date).TotalDays);
                            WaitDays = UIHelp.ComputWorkDaySpan(Convert.ToDateTime(newUnitApplyDate));
                        }
                    }
                    break;
                case "企业信息变更":
                    limitDays = 5;
                    break;
                case "延期注册":
                    limitDays = ((level == 0) ? 5 : 10);
                    break;
                case "增项注册":
                    return "";
                case "重新注册":
                    limitDays = 10;
                    break;
                case "遗失补办":
                    return "";
                case "注销":
                    limitDays = 5;
                    break;
                default:
                    break;
            }

            return string.Format("<span {2}>限{0}天,已过{1}天</span>", limitDays, WaitDays
                , (WaitDays > limitDays ? "style='diaplay:block;color:#FF0000;'" : (WaitDays < limitDays && WaitDays > (limitDays - 3) ? "style='diaplay:block;color:#F5AF02;'" : "")));
        }

        ////EXCEL导入导出
        //protected void ImageButtonOutput_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        System.Text.StringBuilder filterSql = new System.Text.StringBuilder();
        //        if (!string.IsNullOrEmpty(RadTextBoxValue.Text.Trim()))
        //        {
        //            filterSql.Append(string.Format("{0} like '%{1}%'", RadComboBoxIten.SelectedValue, RadTextBoxValue.Text.Trim()));
        //        }
        //        string type = "";//申报类型
        //        if (IfExistRoleID("3") == true)//区县业务员
        //        {
        //            type = RadioButtonListCountyType.SelectedValue;
        //            //申报日期
        //            filterSql.Append(string.Format("AND ENT_City='{0}'", Region));
        //            if (RadDatePickerApplyTimeStart.SelectedDate.HasValue == true)
        //            {
        //                filterSql.Append(string.Format("AND ApplyTime >= '{0}'", RadDatePickerApplyTimeStart.SelectedDate.Value));
        //            }
        //            if (RadDatePickerApplyTimeEnd.SelectedDate.HasValue == true)
        //            {
        //                filterSql.Append(string.Format(" AND ApplyTime <= '{0}'", RadDatePickerApplyTimeEnd.SelectedDate.Value));
        //            }

        //        }
        //        //if (IfExistRoleID("7") == true)//区县领导
        //        //{
        //        //    filterSql.Append(string.Format("AND ENT_City='{0}'", Region));
        //        //    //受理日期
        //        //    if (RadDatePickerGetDateTimeStart.SelectedDate.HasValue == true)
        //        //    {
        //        //        filterSql.Append(string.Format("AND GetDateTime >= '{0}'", RadDatePickerGetDateTimeStart.SelectedDate.Value));
        //        //    }
        //        //    if (RadDatePickerGetDateTimeEnd.SelectedDate.HasValue == true)
        //        //    {
        //        //        filterSql.Append(string.Format("AND GetDateTime <= '{0}'", RadDatePickerGetDateTimeEnd.SelectedDate.Value));
        //        //    }
        //        //}
        //        if (IfExistRoleID("4") == true)//注册中心业务员
        //        {
        //            type = RadioButtonListCityType.SelectedValue;
        //            //收件日期
        //            if (RadDatePickerAcceptDateStart.SelectedDate.HasValue == true)
        //            {
        //                filterSql.Append(string.Format("AND AcceptDate >= '{0}'", RadDatePickerAcceptDateStart.SelectedDate.Value));
        //            }
        //            if (RadDatePickerAcceptDateEnd.SelectedDate.HasValue == true)
        //            {
        //                filterSql.Append(string.Format("AND AcceptDate <= '{0}'", RadDatePickerAcceptDateEnd.SelectedDate.Value));
        //            }
        //        }
        //        //if (IfExistRoleID("6") == true)//注册中心领导
        //        // {
        //        //     //审查日期
        //        //     if (RadDatePickerCheckDateStart.SelectedDate.HasValue == true)
        //        //     {
        //        //         filterSql.Append(string.Format("AND CheckDate >= '{0}'", RadDatePickerCheckDateStart.SelectedDate.Value));
        //        //     }
        //        //     if (RadDatePickerCheckDateEnd.SelectedDate.HasValue == true)
        //        //     {
        //        //         filterSql.Append(string.Format("AND CheckDate <= '{0}'", RadDatePickerCheckDateEnd.SelectedDate.Value));
        //        //     }          
        //        // }
        //        //EXCEL表头明
        //        string head = @"姓名\性别\注册号\证件号码\申报类型\企业名称\企业组织机构代码\建造师类别";
        //        //数据表的列明
        //        string column = @"PSN_Name\PSN_Sex\PSN_RegisterNo\PSN_CertificateNO\ApplyType\ENT_Name\ENT_OrganizationsCode\PSN_Level";
        //        //过滤条件
        //        filterSql.Append(string.Format(" AND  ApplyStatus='{0}' AND ApplyType='{1}'", type, ViewState["ApplyType"].ToString()));
        //        if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/Excel/"))) System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/UploadFiles/Excel/"));
        //        string filePath = string.Format("~/Upload/Excel/Excel{0}{1}.xls",UserID, DateTime.Now.ToString("yyyyMMddHHmmss"));
        //        CommonDAL.OutputXls(HttpContext.Current.Server.MapPath(filePath)
        //            , "Apply"
        //            , filterSql.ToString(), "ApplyID", head.ToString(), column.ToString());
        //        string size = CommonDAL.GetFileSize(HttpContext.Current.Server.MapPath(filePath));
        //        spanOutput.InnerHtml = string.Format(@"<div style=""width: 98%; font-weight: bold; ""><a href=""{0}"">{1}</a><span  style=""padding-left:20px;"">（{2}）</span></div>"
        //            , filePath.Replace("~", "..")
        //            , "点击我下载"
        //            , size);
        //    }
        //    catch (Exception ex)
        //    {
        //        UIHelp.WriteErrorLog(Page, "申请列表导出EXCEL失败！", ex);
        //    }
        //}

        ///// <summary>
        ///// 获取grid勾选行ApplyID集合
        ///// </summary>
        ///// <returns></returns>
        //public string GetRadGridADDRYSelect()
        //{
        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();

        //    for (int i = 0; i < RadGridQY.MasterTableView.Items.Count; i++)
        //    {
        //        CheckBox CheckBox1 = RadGridQY.Items[i].FindControl("CheckBox1") as CheckBox;
        //        if (CheckBox1.Checked)
        //        {
        //            sb.Append(",'").Append(RadGridQY.MasterTableView.DataKeyValues[i]["ApplyID"].ToString()).Append("'");
        //        }
        //    }
        //    if (sb.Length > 0)
        //    {
        //        sb.Remove(0, 1);
        //    }
        //    return sb.ToString();
        //}

        ////专业局会审
        //protected void BttTrial_Click(object sender, EventArgs e)
        //{
        //    //获取Grid选中集合
        //    string applyidlist = GetRadGridADDRYSelect();
        //    if (applyidlist == "")
        //    {
        //        UIHelp.layerAlert(Page, "您尚未勾选任何人员！");
        //        return;
        //    }

        //    #region 查询是否有证书锁定的人员
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
        //    #endregion

        //    try
        //    {
        //        string sql = string.Format(@"Update Apply set XGSJ='{0}',[XGR]='{1}',[OtherDeptCheckMan]='{1}',[OtherDeptCheckResult]='{2}',[OtherDeptCheckRemark]='{3}' Where ApplyID in({4})",
        //                                          DateTime.Now,
        //                                          UserName,
        //                                          RadioButtonListCheckResult.SelectedValue,
        //                                          TextBoxApplyCheckRemark.Text,
        //                                           applyidlist);
        //        if (CommonDAL.ExecSQL(sql))
        //        {
        //            ButtonSearch_Click(sender, e);
        //            UIHelp.WriteOperateLog(UserName, UserID, "待办业务专业局批量会审成功", string.Format("审查时间：{0}", DateTime.Now));
        //            UIHelp.layerAlert(Page, "批量会审成功！", 6, 2000);
        //            //ClientScript.RegisterClientScriptBlock(GetType(), "refresh", "window.location.href = window.location.href;", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        UIHelp.WriteErrorLog(Page, "批量会审失败！", ex);
        //        return;
        //    }
        //}

        ////获取新设立企业人员申请ID
        //public string GetRadGridSelect()
        //{
        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();

        //    for (int i = 0; i < RadGridQY.MasterTableView.Items.Count; i++)
        //    {
        //        CheckBox CheckBox1 = RadGridQY.Items[i].FindControl("CheckBox1") as CheckBox;
        //        string xsl = RadGridQY.MasterTableView.DataKeyValues[i]["CheckXSL"].ToString();

        //        if (CheckBox1.Checked && xsl.ToString() != "0")
        //        {
        //            sb.Append(",'").Append(RadGridQY.MasterTableView.DataKeyValues[i]["ApplyID"].ToString()).Append("'");
        //        }
        //    }
        //    if (sb.Length > 0)
        //    {
        //        sb.Remove(0, 1);
        //    }
        //    return sb.ToString();
        //}

        ////获取人员id
        //public string GetPSN_CertificateNOGridSelect()
        //{
        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();

        //    for (int i = 0; i < RadGridQY.MasterTableView.Items.Count; i++)
        //    {
        //        CheckBox CheckBox1 = RadGridQY.Items[i].FindControl("CheckBox1") as CheckBox;
        //        string sfzh = RadGridQY.MasterTableView.DataKeyValues[i]["PSN_CertificateNO"].ToString();

        //        if (CheckBox1.Checked)
        //        {
        //            sb.Append(",'").Append(RadGridQY.MasterTableView.DataKeyValues[i]["PSN_CertificateNO"].ToString()).Append("'");
        //        }
        //    }
        //    if (sb.Length > 0)
        //    {
        //        sb.Remove(0, 1);
        //    }
        //    return sb.ToString();
        //}
    }
}