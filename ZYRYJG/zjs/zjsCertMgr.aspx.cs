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
    public partial class zjsCertMgr : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
            ClearGridSelectedKeys(RadGridQY);
            ObjectDataSource1.SelectParameters.Clear();
          
            var q = GetQueryParamOB();

            switch (RadioButtonListAction.SelectedValue)
            {
                case "手动锁定":
                    ButtonLock.Visible = true;
                    ButtonUnLock.Visible = false;
                    divCancel.Visible = false;
                    divLock.Visible = true;
                    trLockEndTime.Visible = true;
                    LabelLockDateTip.Text = "加锁日期";
                    LabelLockDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    LabelLockRemark.Text = "锁定原因说明";
                    break;
                case "手动解锁":
                    divCancel.Visible = false;
                    divLock.Visible = true;
                    trLockEndTime.Visible = false;
                    LabelLockDateTip.Text = "解锁日期";
                    LabelLockDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    LabelLockRemark.Text = "解锁原因说明";
                    ButtonLock.Visible = false;
                    ButtonUnLock.Visible = true;
                    break;
                case "手动注销":
                    divCancel.Visible = true;
                    divLock.Visible = false;
                    break;
                default:
                    break;
            }

            switch (RadioButtonListPSN_RegisteType.SelectedValue)//是否注销
            {
                case "未注销":
                    divCancel.Style.Add("display", "block");
                    break;
                case "已注销":
                    divCancel.Style.Add("display", "none");
                    break;
                default:
                    divCancel.Style.Add("display", "block");
                    break;
            }

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridQY.CurrentPageIndex = 0;
            RadGridQY.DataSourceID = ObjectDataSource1.ID;
            RadGridQY.MasterTableView.SortExpressions.AddSortExpression("PSN_RegisterNO");

            spanOutput.InnerHtml = "";
        }
        
        private QueryParamOB GetQueryParamOB()
        {
            var q = new QueryParamOB();

            if (RadTextBoxValue.Text.Trim() != "")//自定义查询项
            {
                q.Add(string.Format("{0} like '%{1}%'", RadComboBoxIten.SelectedValue, RadTextBoxValue.Text.Trim().Replace("[", "[[]")));
            }

            switch (RadioButtonListPSN_RegisteType.SelectedValue)//是否注销
            {
                case "未注销":
                    q.Add("[PSN_RegisteType] < '07'");//排除注销
                    break;
                case "已注销":
                    q.Add("[PSN_RegisteType] > '06'");//注销
                    break;
            }

            if (RadDatePickerGetDateStart.SelectedDate.HasValue == true)//有效期
            {
                q.Add(string.Format("[PSN_CertificateValidity] <= '{0}'", RadDatePickerGetDateStart.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }

            if (CheckBoxPSN_Ag.Checked == true)//只查超龄（大于70岁）
            {
                q.Add(string.Format("PSN_BirthDate <= '{0}'", DateTime.Now.AddYears(-70)));
            }

            switch (RadioButtonListYuJingType.SelectedValue)
            {
                case "无预警数据":
                    q.Add("LockID is null and [PSN_RegisterNO] not IN (select [CertificateCode] from [CheckFeedBack] where [DataStatusCode] >0 and [DataStatusCode] < 7 )");
                    break;
                case "异常注册":
                    q.Add("LockID is not null ");
                    break;
                case "未整改完成涉嫌挂证":
                     q.Add("[PSN_RegisterNO] IN (select [CertificateCode] from [CheckFeedBack] where [DataStatusCode] >0 and [DataStatusCode] < 7 )");
                     break;
                default:
                    break;
            }

            switch (RadioButtonListAction.SelectedValue)
            {
                case "手动锁定":
                    q.Add("LockID is null");
                    break;
                case "手动解锁":
                    q.Add("LockID is not null");
                    break;
                case "手动注销":
                    q.Add("ApplyID is null");//无在办业务
                    break;
            }
            return q;
        }

        // 批量注销 
        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            UpdateGridSelectedKeys(RadGridQY, "PSN_ServerID");
            if (IsGridSelected(RadGridQY) == false)
            {
                UIHelp.layerAlert(Page, "你还没有勾选选任数据！");
                return;
            }

            if (RadioButtonListApplyStatus.SelectedValue == "")
            {
                UIHelp.layerAlert(Page, "请选择注销原因！");
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
                    filterString = string.Format(" {0} and PSN_ServerID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedStrKeysToString(RadGridQY));
                else//包含
                    filterString = string.Format(" {0} and PSN_ServerID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedStrKeysToString(RadGridQY));
            }
          

            #region 插入到历史表并且更新证书表

            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            try
            {
                //1将二级人员信息更新至历史表
                string sql2 = string.Format(@"INSERT INTO [dbo].[zjs_Certificate_His]
                        ([HisID],[PSN_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[END_Addess],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Email],[PSN_Telephone],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[ApplyCATime],[SendCATime],[ReturnCATime],[CertificateCAID],[license_code],[auth_code],[SignCATime],[HisTime])
                        select newid(),[PSN_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[END_Addess],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Email],[PSN_Telephone],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[ApplyCATime],[SendCATime],[ReturnCATime],[CertificateCAID],[license_code],[auth_code],[SignCATime],getdate() 
                        FROM [dbo].[zjs_Certificate]
                        where [PSN_RegisteType] < '07' and [PSN_ServerID] in(select [PSN_ServerID] from [View_ZJS] where 1=1 {0})", filterString);

                CommonDAL.ExecSQL(tran, sql2);

                //2更新二级人员信息的注销状态、失效日期、修改人、申请注销原因、修改日期

                string sql = string.Format(@"Update [zjs_Certificate] 
                                             set [PSN_RegisteType]='07',PSN_RegistePermissionDate='{0}',[XGR]='{1}',[Memo]='{0}省级建设主管部门手动注销，注销原因：{2}' ,XGSJ='{3}' 
                                             Where [PSN_RegisteType] < '07'  and [PSN_ServerID] in(select [PSN_ServerID] from [View_ZJS] where 1=1 {4})",
                                                DateTime.Now.Date.ToString("yyyy-MM-dd"),
                                                UserName,
                                                RadioButtonListApplyStatus.SelectedValue,                                            
                                                DateTime.Now.Date,
                                                filterString
                                                );

                CommonDAL.ExecSQL(tran, sql);
                tran.Commit();
                ButtonSearch_Click(sender, e);
                UIHelp.WriteOperateLog(UserName, UserID, "批量手动注销二级造价工程师成功", string.Format("注销时间：{0}", DateTime.Now));
                UIHelp.layerAlert(Page, "批量手动注销成功！", 6, 2000);

            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "批量手动过期注销二级造价工程师失败！", ex);
                return;
            }
            #endregion

        }
                

        protected void RadioButtonListPSN_RegisteType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ButtonSearch_Click(sender, e);
        }

        protected void RadGridQY_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            UpdateGridSelectedKeys(RadGridQY, "PSN_ServerID");
        }

        protected void RadGridQY_DataBound(object sender, EventArgs e)
        {
            UpdateGriSelectedStatus(RadGridQY, "PSN_ServerID");
        }

        //变换批量操作类型
        protected void RadioButtonListAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            ButtonSearch_Click(null, null);
        }

        //批量锁定
        protected void ButtonLock_Click(object sender, EventArgs e)
        {
            UpdateGridSelectedKeys(RadGridQY, "PSN_ServerID");

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
                    filterString = string.Format(" and [PSN_ServerID] in(select [PSN_ServerID] from [View_ZJS] where 1=1 {0}) and PSN_ServerID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedStrKeysToString(RadGridQY));
                else//包含
                    filterString = string.Format(" and [PSN_ServerID] in(select [PSN_ServerID] from [View_ZJS] where 1=1 {0}) and PSN_ServerID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedStrKeysToString(RadGridQY));
            }

            int rowCount = CommonDAL.SelectRowCount(string.Format(@"
            select count(*) from [zjs_Certificate]
            where [PSN_ServerID] in(select [PSN_ServerID] from [View_ZJS] where 1=1 {0})", filterString));

            LockZJSMDL o = new LockZJSMDL();
            o.LockEndTime = RadDatePickerLockEndTime.SelectedDate;
            o.LockPerson = PersonName;
            o.LockTime = DateTime.Now;
            o.LockType = "其他";
            o.LockRemark = RadTextBoxRemark.Text;
            o.LockStatus = "加锁";

            try
            {
                LockZJSDAL.BatchLock(o, filterString);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "批量锁定二级造价工程师失败！", ex);
                return;
            }

            UIHelp.WriteOperateLog(PersonName, UserID, "批量锁定二级造价工程师", string.Format("锁定了{0}本证书。", rowCount));
            ButtonSearch_Click(sender, e);
            UIHelp.layerAlert(Page, string.Format("成功锁定了{0}本证书。", rowCount));
        }

        //批量解锁
        protected void ButtonUnLock_Click(object sender, EventArgs e)
        {
            UpdateGridSelectedKeys(RadGridQY, "PSN_ServerID");

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
                    filterString = string.Format(" and [PSN_ServerID] in(select [PSN_ServerID] from [View_ZJS] where 1=1 {0}) and PSN_ServerID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedStrKeysToString(RadGridQY));
                else//包含
                    filterString = string.Format(" and [PSN_ServerID] in(select [PSN_ServerID] from [View_ZJS] where 1=1 {0}) and PSN_ServerID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedStrKeysToString(RadGridQY));
            }

            int rowCount = CommonDAL.SelectRowCount(string.Format(@"
            select count(*) from [zjs_Certificate]
            where [PSN_ServerID] in(select [PSN_ServerID] from [View_ZJS] where 1=1 {0})", filterString));

            LockZJSMDL o = new LockZJSMDL();
            o.UnlockPerson = PersonName;
            o.UnlockTime = DateTime.Now;
            o.UnlockRemark = RadTextBoxRemark.Text;
            o.LockStatus = "解锁";

            try
            {
                LockZJSDAL.BatchUnlock(o, filterString);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "批量解锁二级造价工程师失败！", ex);
                return;
            }

            UIHelp.WriteOperateLog(PersonName, UserID, "批量解锁二级造价工程师", string.Format("解锁了{0}本证书。", rowCount));
            ButtonSearch_Click(sender, e);
            UIHelp.layerAlert(Page, string.Format("成功解锁了{0}本证书。", rowCount));
        }

        protected void ButtonOutput_Click(object sender, EventArgs e)
        {
            var q = GetQueryParamOB();
            try
            {
                //EXCEL表头明
                string head = @"姓名\性别\注册号\证件号码\企业名称\企业组织机构代码\注册专业\有效期\注销状态\异常锁定\异常锁定时间\异常说明";
                //数据表的列明
                string column = @"PSN_Name\PSN_Sex\PSN_RegisterNo\PSN_CertificateNO\ENT_Name\ENT_OrganizationsCode\PSN_RegisteProfession\PSN_CertificateValidity\case when PSN_RegisteType = '07' then '已注销' else '未注销' end\case when LockID is not null then '异常注册' else '' end\CONVERT(varchar(100),LockEndTime, 23)\LockRemark";

                //                //EXCEL表头明
                //                string head = @"姓名\性别\注册号\证件号码\企业名称\企业组织机构代码\建造师类别\注册专业及有效期\注销原因\处理人\处理时间\异常状态预警\异常说明";
                //                //数据表的列明
                //                string column = @"A.PSN_Name\A.PSN_Sex\A.PSN_RegisterNo\A.PSN_CertificateNO\A.ENT_Name\A.ENT_OrganizationsCode\A.PSN_Level\A.ProfessionWithValid\A.PSN_CancelReason\A.XGR\CONVERT(varchar(100),A.XGSJ, 23)\case when C.SDZT=1 then '【在施锁定】'  else '' end + case when L.LockStates='加锁' then '【异常注册】' else '' end\case when C.SDZT=1 then '【锁定项目：'+C.XMMC+'，合同编号：'+C.HTBH+'，中标企业：'+C.ZBQY+'，锁定时间：'+CONVERT(varchar(100),C.SDSJ, 23)+'】' else '' end +case when L.LockStates='加锁' then '【加锁时间：'+ CONVERT(varchar(100),L.LockEndTime, 23)+'，加锁原因：'+ L.LockContent+'】' else '' end
                //";


                //过滤条件
                if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/Excel/"))) System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/UploadFiles/Excel/"));
                string filePath = string.Format("~/Upload/Excel/{0}_{1}.xls", DateTime.Now.ToString("yyyyMMdd"), Guid.NewGuid());
                CommonDAL.OutputXls(HttpContext.Current.Server.MapPath(filePath)
                    , "View_ZJS"
                    , q.ToWhereString(), "PSN_ServerID", head.ToString(), column.ToString());
                string size = CommonDAL.GetFileSize(HttpContext.Current.Server.MapPath(filePath));
                spanOutput.InnerHtml = string.Format(@"<a href=""{0}"">{1}</a><span  style=""padding-left:10px;"">（{2}）</span>"
                    , UIHelp.AddUrlReadParam(filePath.Replace("~", ".."))
                    , "点击我下载"
                    , size);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "业务查询结果导出EXCEL失败！", ex);
            }
        }

        protected void RadioButtonListYuJingType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ButtonSearch_Click(null, null);
        }

    }
}