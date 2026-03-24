using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;
using System.Data;
namespace ZYRYJG.SystemManage
{
    public partial class CertMgr :  BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lbl_DateQuery.Visible = false;
                RadDatePickerGetDateStart.Visible = false;
                ButtonSearch_Click(sender, e);

            } 
           
        }

        //查询
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ClearGridSelectedKeys(RadGridQY);
            ObjectDataSource1.SelectParameters.Clear();
          
            var q = new QueryParamOB();
            switch (RadioButtonList2.SelectedValue)
            {
                case "手动锁定":
                case "手动解锁":
                    EnabledTrue();
                    divCancel.Visible = false;
                    divLock.Visible = true;
                    q = Lock();
                    break;
                case "手动注销":
                    EnabledTrue();
                    divCancel.Visible = true;
                    divLock.Visible = false;
                    q = Manual();
                    break;
                case "证书过期":
                case "专业过期":
                    EnabledFalse();
                    divCancel.Visible = true;
                    divLock.Visible = false;
                    q = Certificate();
                    break;
                default:
                    break;
            }
           
            if (CheckBoxZSSD.Checked==true)//在施锁定
            {
                q.Add("c.SDZT = 1");
            }
            if (CheckBoxYCZC.Checked == true)//异常注册
            {
                q.Add("L.LockID is not null");
            }
            if (CheckBoxNormalData.Checked == true)//无预警数据
            {
                q.Add("(c.SDZT = 0 or c.SDZT is null) and (L.LockID is null)");
            }
            if (RadioButtonGuaZheng.Checked == true)//未整改完成涉嫌挂证
            {
                q.Add("A.[PSN_RegisterNO] IN (select [CertificateCode] from [CheckFeedBack] where [DataStatusCode] >0 and [DataStatusCode] < 7 )");

            }
            if (RadioButtonRepeat.Checked == true)//一年两次重新注册
            {
                //安重新注册计算（排除注销后重新注册与原单位是同一单位情况）
                q.Add(@"A.PSN_CertificateNO IN (      
                            select PSN_CertificateNO 
                            from Apply 
                            where ApplyType='重新注册' and ApplyStatus='已公告' and ConfirmResult='通过' and DATEADD(year,1,NoticeDate)>GETDATE()  
                            group by PSN_CertificateNO
                            having count(distinct ENT_OrganizationsCode) > 1
                        )
                         and exists
                         (
                           select 1 from 
                           (
                               select top 1 case when preApply.ENT_OrganizationsCode = firstApply.ENT_OrganizationsCode then 0 else 1 end as ifchangeunit 
                               from (
                                   select top 1 PSN_CertificateNO,NoticeDate,ENT_OrganizationsCode from apply 
                                   where PSN_CertificateNO = A.PSN_CertificateNO 
                                   and ApplyType='重新注册' and ApplyStatus='已公告' and ConfirmResult='通过' and DATEADD(year,1,NoticeDate)>GETDATE()  
                                   order by NoticeDate
                               ) as firstApply
                               inner join 
                               apply as preApply on firstApply.PSN_CertificateNO = preApply.PSN_CertificateNO and firstApply.NoticeDate > preApply.NoticeDate
	                           where preApply.ApplyStatus='已公告' and ( (preApply.ApplyTypeSub='执业企业变更' and preApply.ExamineResult='通过') or ( preApply.ConfirmResult='通过'))
                               order by preApply.NoticeDate desc
                           ) t where ifchangeunit=1
                         )");

              

                #region 安变换单位计算（初始、重新、执业企业变更）
                ////安变换单位计算（初始、重新、执业企业变更）
                //                q.Add(@"A.PSN_CertificateNO IN (                            
                //                            select PSN_CertificateNO
                //                            from
                //                            (
                //	                            select  PSN_CertificateNO ,min(ENT_OrganizationsCode) as ENT_OrganizationsCode
                //	                            from Apply 
                //	                            where ((ApplyTypeSub = '执业企业变更' and ApplyStatus = '已公告' and ExamineResult = '通过') or ((ApplyType = '初始注册' or ApplyType = '重新注册') and ConfirmResult = '通过' and ApplyStatus = '已公告'))
                //	                            and DATEADD(year,1,NoticeDate) > GETDATE()  
                //	                            group by PSN_CertificateNO 
                //	                            having COUNT(distinct ENT_OrganizationsCode) > 1
                //	                            union all
                //	                            select  PSN_CertificateNO ,max(ENT_OrganizationsCode) as ENT_OrganizationsCode
                //	                            from Apply 
                //	                            where ((ApplyTypeSub = '执业企业变更' and ApplyStatus = '已公告' and ExamineResult = '通过') or ((ApplyType = '初始注册' or ApplyType = '重新注册') and ConfirmResult = '通过' and ApplyStatus = '已公告'))
                //	                            and DATEADD(year,1,NoticeDate) > GETDATE()  
                //	                            group by PSN_CertificateNO 
                //	                            having COUNT(distinct ENT_OrganizationsCode) > 1
                //	                            union all
                //	                            SELECT PSN_CertificateNO,ENT_OrganizationsCode
                //	                            from(
                //		                            SELECT 
                //			                            PSN_CertificateNO,
                //			                            ENT_OrganizationsCode,
                //			                            ROW_NUMBER() OVER (PARTITION BY PSN_CertificateNO ORDER BY NoticeDate desc) AS RowNum
                //		                            FROM Apply
                //		                            where PSN_CertificateNO in(
                //				                            select  PSN_CertificateNO 
                //				                            from Apply 
                //				                            where ((ApplyTypeSub = '执业企业变更' and ApplyStatus = '已公告' and ExamineResult = '通过') or ((ApplyType = '初始注册' or ApplyType = '重新注册') and ConfirmResult = '通过' and ApplyStatus = '已公告'))
                //				                            and DATEADD(year,1,NoticeDate) > GETDATE()  
                //				                            group by PSN_CertificateNO 
                //				                            having COUNT(distinct ENT_OrganizationsCode) > 1
                //		                            )	
                //		                            and ((ApplyTypeSub = '执业企业变更' and ApplyStatus = '已公告' and ExamineResult = '通过') or ((ApplyType = '初始注册' or ApplyType = '重新注册') and ConfirmResult = '通过' and ApplyStatus = '已公告'))
                //		                            and DATEADD(year,1,NoticeDate) < GETDATE()
                //	                            ) t
                //	                            where 	RowNum=1  
                //                            ) t
                //                            group by PSN_CertificateNO 
                //                            having COUNT(distinct ENT_OrganizationsCode) = COUNT(*)
                //                       )");

                #endregion
            }

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridQY.CurrentPageIndex = 0;
            RadGridQY.DataSourceID = ObjectDataSource1.ID;
        }
        
        /// <summary>
        /// 注销 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            UpdateGridSelectedKeys(RadGridQY, "PSN_ServerID");
            if (IsGridSelected(RadGridQY) == false)
            {
                UIHelp.layerAlert(Page, "你还没有勾选选任数据！");
                return;
            }

            //string applyidlist = GetRadGridADDRYSelect();
          
            if (RadioButtonListApplyStatus.SelectedValue=="")
            {
                UIHelp.layerAlert(Page, "请选择注销原因！");
                return;
            }
            if (RadComboBoxPSN_RegisteType.SelectedValue != "未注销" && RadioButtonList2.SelectedValue == "手动注销")
            {
                UIHelp.layerAlert(Page, "请将注销原因选择为未注销！");
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
                    filterString = string.Format(" {0} and A.PSN_ServerID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedStrKeysToString(RadGridQY));
                else//包含
                    filterString = string.Format(" {0} and A.PSN_ServerID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedStrKeysToString(RadGridQY));
            }

            if (RadioButtonList2.SelectedValue == "手动注销" )
            {
                //将二级人员信息更新至历史表、更新二级人员信息的注销状态、失效日期、修改人、申请注销原因、修改日期
                //01	初始注册 02	变更注册 03	延期注册 04	增项注册 05	重新注册 06	遗失补办 07	注销
                #region 插入到历史表并且更新二建基本表

                DBHelper db = new DBHelper();
                DbTransaction tran = db.BeginTransaction();
                try
                {
                    //1将二级人员信息更新至历史表
                    string sql2 = string.Format(@"insert into COC_TOW_Person_BaseInfo_His (
                        [HisID],[PSN_ServerID],[ENT_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[BeginTime]
                        ,[EndTime],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO]
                        ,[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone]
                        ,[PSN_Telephone],[PSN_Email],[PSN_PMGrade],[PSN_PMCertificateNo],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo]
                        ,[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_ChangeReason]
                        ,[PSN_BeforENT_Name],[PSN_BeforENT_ServerID],[PSN_BeforPersonName],[PSN_InterprovincialChange],[PSN_ExpiryReasons]
                        ,[PSN_ExpiryDate],[PSN_RenewalProfession],[PSN_AddProfession],[PSN_CancelPerson],[PSN_CancelReason],[PSN_ReReasons]
                        ,[PSN_ReContent],[PSN_CheckCode],[ENT_Province_Code],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[GteDate]) 
                    select  NEWID(),[PSN_ServerID],[ENT_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[BeginTime]
                        ,[EndTime],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO]
                        ,[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone]
                        ,[PSN_Telephone],[PSN_Email],[PSN_PMGrade],[PSN_PMCertificateNo],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo]
                        ,[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_ChangeReason]
                        ,[PSN_BeforENT_Name],[PSN_BeforENT_ServerID],[PSN_BeforPersonName],[PSN_InterprovincialChange],[PSN_ExpiryReasons]
                        ,[PSN_ExpiryDate],[PSN_RenewalProfession],[PSN_AddProfession],[PSN_CancelPerson],[PSN_CancelReason],[PSN_ReReasons],[PSN_ReContent]
                        ,[PSN_CheckCode],[ENT_Province_Code],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],CONVERT(varchar(100), GETDATE(), 23)
		            from COC_TOW_Person_BaseInfo  
                    where PSN_ServerID in(
                            select A.PSN_ServerID
                            from dbo.View_JZS_TOW_WithProfession A 
                            left join [dbo].[View_JZS_TOW_Applying] B 
                                on a.PSN_ServerID=b.PSN_ServerID 
                            left join [jcsjk_RY_JZS_ZSSD] C 
                                on a.PSN_RegisterNO=c.ZCH 
                            left join [dbo].[LockJZS] L
                                on A.[PSN_ServerID] = L.[PSN_ServerID] and L.LockStatus='加锁' and LockEndTime > getdate()
                            where 1=1 {0}
                        );",filterString);
                    
                    CommonDAL.ExecSQL(tran, sql2);

                    //2更新二级人员信息的注销状态、失效日期、修改人、申请注销原因、修改日期

                    string sql = string.Format(@"
                        Update COC_TOW_Person_BaseInfo 
                        set [PSN_RegisteType]='07',PSN_RegistePermissionDate='{0}',[XGR]='{1}',[PSN_CancelReason]='{2}',XGSJ='{0}',PSN_CancelPerson='省级建设主管部门' 
                        where PSN_ServerID in(
                            select A.PSN_ServerID
                            from dbo.View_JZS_TOW_WithProfession A 
                            left join [dbo].[View_JZS_TOW_Applying] B 
                                on a.PSN_ServerID=b.PSN_ServerID 
                            left join [jcsjk_RY_JZS_ZSSD] C 
                                on a.PSN_RegisterNO=c.ZCH 
                            left join [dbo].[LockJZS] L
                                on A.[PSN_ServerID] = L.[PSN_ServerID] and L.LockStatus='加锁' and LockEndTime > getdate()
                            where 1=1 {3}
                        );",
                        DateTime.Now.Date,
                        UserName,
                        RadioButtonListApplyStatus.SelectedValue,
                        filterString);

                    CommonDAL.ExecSQL(tran, sql);
                    tran.Commit();
                    ButtonSearch_Click(sender, e);
                    UIHelp.WriteOperateLog(UserName, UserID, "批量手动注销二级建造师成功", string.Format("注销时间：{0}", DateTime.Now));
                    UIHelp.layerAlert(Page, "批量手动注销成功！", 6, 2000);

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    UIHelp.WriteErrorLog(Page, "批量手动过期注销二级建造师失败！", ex);
                    return;
                }
                #endregion
            }
            else if (RadioButtonList2.SelectedValue == "证书过期")
            {
                #region 插入到历史表并且更新二建基本表

                DBHelper db = new DBHelper();
                DbTransaction tran = db.BeginTransaction();
                try
                {
                    //1将二级人员信息更新至历史表
                    string sql2 = string.Format(@"
                    insert into COC_TOW_Person_BaseInfo_His (
                        [HisID],[PSN_ServerID],[ENT_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[BeginTime],[EndTime],[PSN_Name],[PSN_Sex],[PSN_BirthDate]
                        ,[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification]
                        ,[PSN_Degree],[PSN_MobilePhone],[PSN_Telephone],[PSN_Email],[PSN_PMGrade],[PSN_PMCertificateNo],[PSN_RegisteType],[PSN_RegisterNO]
                        ,[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate]
                        ,[PSN_ChangeReason],[PSN_BeforENT_Name],[PSN_BeforENT_ServerID],[PSN_BeforPersonName],[PSN_InterprovincialChange],[PSN_ExpiryReasons]
                        ,[PSN_ExpiryDate],[PSN_RenewalProfession],[PSN_AddProfession],[PSN_CancelPerson],[PSN_CancelReason],[PSN_ReReasons],[PSN_ReContent],[PSN_CheckCode]
                        ,[ENT_Province_Code],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[GteDate]) 
                    select  NEWID(),[PSN_ServerID],[ENT_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[BeginTime],[EndTime],[PSN_Name],[PSN_Sex],[PSN_BirthDate]
                        ,[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification]
                        ,[PSN_Degree],[PSN_MobilePhone],[PSN_Telephone],[PSN_Email],[PSN_PMGrade],[PSN_PMCertificateNo],[PSN_RegisteType],[PSN_RegisterNO]
                        ,[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate]
                        ,[PSN_ChangeReason],[PSN_BeforENT_Name],[PSN_BeforENT_ServerID],[PSN_BeforPersonName],[PSN_InterprovincialChange],[PSN_ExpiryReasons]
                        ,[PSN_ExpiryDate],[PSN_RenewalProfession],[PSN_AddProfession],[PSN_CancelPerson],[PSN_CancelReason],[PSN_ReReasons],[PSN_ReContent]
                        ,[PSN_CheckCode],[ENT_Province_Code],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],CONVERT(varchar(100), GETDATE(), 23)
		            from COC_TOW_Person_BaseInfo  
                     where PSN_ServerID in(
                            select A.PSN_ServerID
                            from dbo.View_JZS_TOW_WithProfession A 
                            left join [dbo].[View_JZS_TOW_Applying] B 
                                on a.PSN_ServerID=b.PSN_ServerID 
                            left join [jcsjk_RY_JZS_ZSSD] C 
                                on a.PSN_RegisterNO=c.ZCH 
                            left join [dbo].[LockJZS] L
                                on A.[PSN_ServerID] = L.[PSN_ServerID] and L.LockStatus='加锁' and LockEndTime > getdate()
                            where 1=1 {0}
                        );", filterString);

                    CommonDAL.ExecSQL(tran, sql2);

                    //2更新二级人员信息的注销状态、失效日期、修改人、申请注销原因、修改日期

                    string sql = string.Format(@"
                        Update COC_TOW_Person_BaseInfo 
                        set [PSN_RegisteType]='07',PSN_RegistePermissionDate='{0}',[XGR]='{1}',[PSN_CancelReason]='{2}',XGSJ='{0}',PSN_CancelPerson='省级建设主管部门' 
                        where PSN_ServerID in(
                            select A.PSN_ServerID
                            from dbo.View_JZS_TOW_WithProfession A 
                            left join [dbo].[View_JZS_TOW_Applying] B 
                                on a.PSN_ServerID=b.PSN_ServerID 
                            left join [jcsjk_RY_JZS_ZSSD] C 
                                on a.PSN_RegisterNO=c.ZCH 
                            left join [dbo].[LockJZS] L
                                on A.[PSN_ServerID] = L.[PSN_ServerID] and L.LockStatus='加锁' and LockEndTime > getdate()
                            where 1=1 {3}
                        );",
                        DateTime.Now.Date,
                        UserName,
                        RadioButtonListApplyStatus.SelectedValue,
                        filterString);

                    CommonDAL.ExecSQL(tran, sql);
                    tran.Commit();
                    ButtonSearch_Click(sender, e);
                    UIHelp.WriteOperateLog(UserName, UserID, "批量二级建造师证书过期注销成功", string.Format("注销时间：{0}", DateTime.Now));
                    UIHelp.layerAlert(Page, "批量证书过期注销成功！", 6, 2000);

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    UIHelp.WriteErrorLog(Page, "批量二级建造师证书过期注销失败！", ex);
                    return;
                }
                #endregion

            }
            else if (RadioButtonList2.SelectedValue == "专业过期") 
            {
                #region 专业过期

                DBHelper db = new DBHelper();
                DbTransaction tran = db.BeginTransaction();
                try
                {
                    //以下4步顺序不可修改

                    #region 将此人的基本信息添加到基本信息历史表。

                    string sql3 = string.Format(@"
                    insert into COC_TOW_Person_BaseInfo_His (
                        [HisID],[PSN_ServerID],[ENT_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[BeginTime],[EndTime],[PSN_Name],[PSN_Sex],[PSN_BirthDate]
                        ,[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification]
                        ,[PSN_Degree],[PSN_MobilePhone],[PSN_Telephone],[PSN_Email],[PSN_PMGrade],[PSN_PMCertificateNo],[PSN_RegisteType],[PSN_RegisterNO]
                        ,[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate]
                        ,[PSN_ChangeReason],[PSN_BeforENT_Name],[PSN_BeforENT_ServerID],[PSN_BeforPersonName],[PSN_InterprovincialChange],[PSN_ExpiryReasons]
                        ,[PSN_ExpiryDate],[PSN_RenewalProfession],[PSN_AddProfession],[PSN_CancelPerson],[PSN_CancelReason],[PSN_ReReasons],[PSN_ReContent],[PSN_CheckCode]
                        ,[ENT_Province_Code],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[GteDate]) 
                    select  NEWID(),[PSN_ServerID],[ENT_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[BeginTime],[EndTime],[PSN_Name],[PSN_Sex],[PSN_BirthDate]
                        ,[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification]
                        ,[PSN_Degree],[PSN_MobilePhone],[PSN_Telephone],[PSN_Email],[PSN_PMGrade],[PSN_PMCertificateNo],[PSN_RegisteType],[PSN_RegisterNO]
                        ,[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate]
                        ,[PSN_ChangeReason],[PSN_BeforENT_Name],[PSN_BeforENT_ServerID],[PSN_BeforPersonName],[PSN_InterprovincialChange],[PSN_ExpiryReasons]
                        ,[PSN_ExpiryDate],[PSN_RenewalProfession],[PSN_AddProfession],[PSN_CancelPerson],[PSN_CancelReason],[PSN_ReReasons],[PSN_ReContent]
                        ,[PSN_CheckCode],[ENT_Province_Code],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],CONVERT(varchar(100), GETDATE(), 23)
		            from COC_TOW_Person_BaseInfo  
                     where PSN_ServerID in(
                            select A.PSN_ServerID
                            from dbo.View_JZS_TOW_WithProfession A 
                            left join [dbo].[View_JZS_TOW_Applying] B 
                                on a.PSN_ServerID=b.PSN_ServerID 
                            left join [jcsjk_RY_JZS_ZSSD] C 
                                on a.PSN_RegisterNO=c.ZCH 
                            left join [dbo].[LockJZS] L
                                on A.[PSN_ServerID] = L.[PSN_ServerID] and L.LockStatus='加锁' and LockEndTime > getdate()
                            where 1=1 {0}
                        );", filterString);

                    CommonDAL.ExecSQL(tran, sql3);

                    #endregion

                    #region 将专业表数据添加至专业历史表

                    string sql = string.Format(@"
                    insert into [dbo].[COC_TOW_Register_Profession_His](
                        [His_ID]
                        ,[PRO_ServerID]
                        ,[PSN_ServerID]
                        ,[PRO_Profession]
                        ,[PRO_ValidityBegin]
                        ,[PRO_ValidityEnd]
                        ,[DogID]
                        ,[ENT_Province_Code]
                        ,[DownType]
                        ,[LastModifyTime]
                        ,[ApplyType]
                        ,[GetDate]
                    ) 
                    select NEWID()
                        ,c.[PRO_ServerID]
                        ,c.[PSN_ServerID]
                        ,c.[PRO_Profession]
                        ,c.[PRO_ValidityBegin]
                        ,c.[PRO_ValidityEnd]
                        ,c.[DogID]
                        ,c.[ENT_Province_Code]
                        ,c.[DownType]
                        ,c.[LastModifyTime]
                        ,'注销'
                        ,CONVERT(varchar(100), GETDATE(), 23)
                    from dbo.COC_TOW_Person_BaseInfo A 
                    inner join COC_TOW_Register_Profession c on A.PSN_ServerID=c.PSN_ServerID
                    WHERE c.PRO_ValidityEnd<='{0}' 
                          and a.PSN_ServerID in(
                                select A.PSN_ServerID
                                from dbo.View_JZS_TOW_WithProfession A 
                                left join [dbo].[View_JZS_TOW_Applying] B 
                                    on a.PSN_ServerID=b.PSN_ServerID 
                                left join [jcsjk_RY_JZS_ZSSD] C 
                                    on a.PSN_RegisterNO=c.ZCH 
                                left join [dbo].[LockJZS] L
                                    on A.[PSN_ServerID] = L.[PSN_ServerID] and L.LockStatus='加锁' and LockEndTime > getdate()
                                where 1=1 {1}
                        );"
                        , RadDatePickerGetDateStart.SelectedDate.HasValue == true ? RadDatePickerGetDateStart.SelectedDate.Value : DateTime.Now.Date
                        , filterString);

                    CommonDAL.ExecSQL(tran, sql);

                    #endregion

                    #region 更新该人的注册专业

                    string sql4 = string.Format(@"
                    update [dbo].[COC_TOW_Person_BaseInfo] 
                    set [dbo].[COC_TOW_Person_BaseInfo].PSN_RegisteProfession =c.PRO_Profession,
	                    [dbo].[COC_TOW_Person_BaseInfo].PSN_CertificateValidity=c.PRO_ValidityEnd
                    from 
                    (
                        select PSN_ServerID,
                        PRO_Profession=STUFF((select ','+[PRO_Profession] from COC_TOW_Register_Profession a where a.PSN_ServerID=b.PSN_ServerID and PRO_ValidityEnd > '{0}' for xml PATH('')), 1, 1, ''),
                        PRO_ValidityEnd=STUFF((select ','+ convert(varchar(10),max([PRO_ValidityEnd]),20)  from COC_TOW_Register_Profession a where a.PSN_ServerID=b.PSN_ServerID and PRO_ValidityEnd > '{0}' for xml PATH('')), 1, 1, '')
                        from COC_TOW_Register_Profession  b 
                        group by b.PSN_ServerID
                    ) c 
                    where [dbo].[COC_TOW_Person_BaseInfo].PSN_ServerID=c.PSN_ServerID 
                        and c.PSN_ServerID in(
                            select A.PSN_ServerID
                            from dbo.View_JZS_TOW_WithProfession A 
                            left join [dbo].[View_JZS_TOW_Applying] B 
                                on a.PSN_ServerID=b.PSN_ServerID 
                            left join [jcsjk_RY_JZS_ZSSD] C 
                                on a.PSN_RegisterNO=c.ZCH 
                            left join [dbo].[LockJZS] L
                                on A.[PSN_ServerID] = L.[PSN_ServerID] and L.LockStatus='加锁' and LockEndTime > getdate()
                            where 1=1 {1}
                    );"
                        , RadDatePickerGetDateStart.SelectedDate.HasValue == true ? RadDatePickerGetDateStart.SelectedDate.Value.ToString("yyyy-MM-dd") : DateTime.Now.Date.ToString("yyyy-MM-dd")
                        ,filterString);

                    CommonDAL.ExecSQL(tran, sql4);

                    #endregion

                    #region  删除专业表数据

                    string sql2 = string.Format(@"delete from COC_TOW_Register_Profession where PRO_ServerID in
                    (
                        select c.PRO_ServerID 
                        from dbo.COC_TOW_Person_BaseInfo A 
                        inner join COC_TOW_Register_Profession c on A.PSN_ServerID=c.PSN_ServerID
                        WHERE c.PRO_ValidityEnd<='{0}' 
                              and a.PSN_ServerID in(
                                    select A.PSN_ServerID
                                    from dbo.View_JZS_TOW_WithProfession A 
                                    left join [dbo].[View_JZS_TOW_Applying] B 
                                        on a.PSN_ServerID=b.PSN_ServerID 
                                    left join [jcsjk_RY_JZS_ZSSD] C 
                                        on a.PSN_RegisterNO=c.ZCH 
                                    left join [dbo].[LockJZS] L
                                        on A.[PSN_ServerID] = L.[PSN_ServerID] and L.LockStatus='加锁' and LockEndTime > getdate()
                                    where 1=1 {1}
                            )
                    );"
                    , RadDatePickerGetDateStart.SelectedDate.HasValue == true ? RadDatePickerGetDateStart.SelectedDate.Value : DateTime.Now.Date
                    , filterString);

                    CommonDAL.ExecSQL(tran, sql2);

                    #endregion

                    tran.Commit();
                    ButtonSearch_Click(sender, e);
                    UIHelp.WriteOperateLog(UserName, UserID, "批量二级建造师专业过期注销成功", string.Format("注销时间：{0}", DateTime.Now));
                    UIHelp.layerAlert(Page, "批量专业过期注销成功！", 6, 2000);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    UIHelp.WriteErrorLog(Page, "批量二级建造师专业过期注销失败！", ex);
                    return;
                }
                #endregion
            
            }

        }

        /// <summary>
        /// 证书过期或专业过期
        /// </summary>
        /// <returns></returns>
        private QueryParamOB Certificate()
        { 
          var q = new QueryParamOB();
                    
          q.Add("(b.ApplyStatus is null)");
          q.Add("A.PSN_RegisteType < '07'");
          q.Add("A.PSN_Level = '二级'");

          switch (RadioButtonList2.SelectedValue)
          {
              case "证书过期":
                  q.Add(string.Format("exists(select PSN_ServerID  from COC_TOW_Register_Profession  where PSN_ServerID =a.PSN_ServerID group by PSN_ServerID having MAX(PRO_ValidityEnd)< '{0}')", RadDatePickerGetDateStart.SelectedDate.HasValue == true ? RadDatePickerGetDateStart.SelectedDate.Value.ToString("yyyy-MM-dd") : DateTime.Now.Date.ToString("yyyy-MM-dd")));
                  break;
              case "专业过期":
                  q.Add(string.Format("exists(select PSN_ServerID  from COC_TOW_Register_Profession  where PRO_ValidityEnd > '{0}' and PSN_ServerID =a.PSN_ServerID)", RadDatePickerGetDateStart.SelectedDate.HasValue == true ? RadDatePickerGetDateStart.SelectedDate.Value.ToString("yyyy-MM-dd") : DateTime.Now.Date.ToString("yyyy-MM-dd")));
                  q.Add(string.Format("exists(select PSN_ServerID  from COC_TOW_Register_Profession  where PRO_ValidityEnd <='{0}' and PSN_ServerID =a.PSN_ServerID)", RadDatePickerGetDateStart.SelectedDate.HasValue == true ? RadDatePickerGetDateStart.SelectedDate.Value.ToString("yyyy-MM-dd") : DateTime.Now.Date.ToString("yyyy-MM-dd")));
                  break;
          }

           return q;
        }

        /// <summary>
        /// 手动注销
        /// </summary>
        /// <returns></returns>
        private QueryParamOB Manual()
        {
            var q = new QueryParamOB();
            q.Add("A.PSN_Level = '二级'");
            q.Add("(b.ApplyStatus is null)");
            switch (RadComboBoxPSN_Age.SelectedValue)
            { 
                case"":
                    break;
                case "大于65岁":
                   // q.Add(string.Format("datediff(year,A.PSN_BirthDate,getdate()) >65"));
                    q.Add(string.Format(" FLOOR(datediff(DY,A.PSN_BirthDate,getdate())/365.25) >=65"));
                   
                    break;
                case "大于60岁":
                    q.Add(string.Format(" FLOOR(datediff(DY,A.PSN_BirthDate,getdate())/365.25) >=60"));
                    break;
            
            }
            switch (RadComboBoxPSN_RegisteType.SelectedValue)
            {
                case "":
                    break;
                case "未注销":
                    q.Add("A.PSN_RegisteType < '07'");//排除注销
                    break;
                case "已注销":
                    q.Add("A.PSN_RegisteType > '06'");//注销
                    break;
            }

            if (RadTextBoxValue.Text.Trim() != "")//自定义查询项
            {
                q.Add(string.Format("{0} like '%{1}%'", "A." + RadComboBoxIten.SelectedValue, RadTextBoxValue.Text.Trim()));
            }
            switch (RadComboBoxENT_Qyzz.SelectedValue)
            {
                case"":
                    break;
                case "资质已注销":
                    //q.Add(string.Format("not exists (select [ZZJGDM] FROM [dbo].[jcsjk_QY_ZZZS] c where [ZSZT]='有效'and c.ZZJGDM=a.ENT_OrganizationsCode and getdate() between [ZSYXQKS]  and [ZSYXQJS])"));
                    //a.ENT_OrganizationsCode not in 
                    q.Add(string.Format("not exists (select ZZJGDM from  dbo.jcsjk_QY_ZHXX where (SJLX='本地招标代理机构' OR SJLX='本地监理企业' OR SJLX='设计施工一体化' OR SJLX='本地造价咨询企业' OR SJLX='本地施工企业' OR SJLX='新设立企业' OR SJLX='工程勘察' OR SJLX='工程设计') and A.ENT_OrganizationsCode=ZZJGDM and getdate() between [ZSYXQKS]  and [ZSYXQJS])"));
                    break;
            }
            return q;
        
        }

        //锁定/解锁查询
        private QueryParamOB Lock()
        {
            var q = new QueryParamOB();
            q.Add("A.PSN_Level = '二级'");
            switch (RadComboBoxPSN_Age.SelectedValue)
            {
                case "":
                    break;
                case "大于65岁":
                    // q.Add(string.Format("datediff(year,A.PSN_BirthDate,getdate()) >65"));
                    q.Add(string.Format(" FLOOR(datediff(DY,A.PSN_BirthDate,getdate())/365.25) >=65"));

                    break;
                case "大于60岁":
                    q.Add(string.Format(" FLOOR(datediff(DY,A.PSN_BirthDate,getdate())/365.25) >=60"));
                    break;

            }
            switch (RadComboBoxPSN_RegisteType.SelectedValue)
            {
                case "":
                    break;
                case "未注销":
                    q.Add("A.PSN_RegisteType < '07'");//排除注销
                    break;
                case "已注销":
                    q.Add("A.PSN_RegisteType > '06'");//注销
                    break;
            }

            if (RadTextBoxValue.Text.Trim() != "")//自定义查询项
            {
                q.Add(string.Format("{0} like '%{1}%'", "A." + RadComboBoxIten.SelectedValue, RadTextBoxValue.Text.Trim()));
            }
            switch (RadComboBoxENT_Qyzz.SelectedValue)
            {
                case "":
                    break;
                case "资质已注销":
                    //q.Add(string.Format("not exists (select [ZZJGDM] FROM [dbo].[jcsjk_QY_ZZZS] c where [ZSZT]='有效'and c.ZZJGDM=a.ENT_OrganizationsCode and getdate() between [ZSYXQKS]  and [ZSYXQJS])"));
                    //a.ENT_OrganizationsCode not in 
                    q.Add(string.Format("not exists (select ZZJGDM from  dbo.jcsjk_QY_ZHXX where (SJLX='本地招标代理机构' OR SJLX='本地监理企业' OR SJLX='设计施工一体化' OR SJLX='本地造价咨询企业' OR SJLX='本地施工企业' OR SJLX='新设立企业' OR SJLX='工程勘察' OR SJLX='工程设计') and A.ENT_OrganizationsCode=ZZJGDM and getdate() between [ZSYXQKS]  and [ZSYXQJS])"));
                    break;
            }

            if (RadioButtonList2.SelectedValue == "手动锁定")
            {
                q.Add("L.LockID is null");
                trLockEndTime.Visible = true;
                LabelLockDateTip.Text = "加锁日期";
                LabelLockDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                LabelLockRemark.Text = "锁定原因说明";
                ButtonLock.Visible = true;
                ButtonUnLock.Visible = false;
            }
            else if (RadioButtonList2.SelectedValue == "手动解锁")
            {
                q.Add("L.LockID is not null");
                trLockEndTime.Visible = false;
                LabelLockDateTip.Text = "解锁日期";
                LabelLockDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                LabelLockRemark.Text = "解锁原因说明";
                ButtonLock.Visible = false;
                ButtonUnLock.Visible = true;
            }
            return q;

        }

        /// <summary>
        /// 启用按钮手动注销
        /// </summary>
        private void EnabledTrue()
        {
            lbl_Query.Visible= true;
            LabelCustomer.Visible = true;
            lbl_DateQuery.Visible = false;
            RadComboBoxIten.Visible = true;
            RadTextBoxValue.Visible = true;
            RadComboBoxPSN_RegisteType.Visible = true;
            //RadioButtonList1.Visible = true;
            RadDatePickerGetDateStart.Visible = false;
            RadioButtonListApplyStatus.Enabled = true;

            lbl_Query1.Visible = true;
            RadComboBoxPSN_Age.Visible = true;
            lbl_Query3.Visible = true;
            RadComboBoxENT_Qyzz.Visible = true;

        }

        /// <summary>
        /// 禁用按钮
        /// </summary>
        private void EnabledFalse()
        {
            lbl_Query.Visible = false;
            LabelCustomer.Visible = false;
            lbl_DateQuery.Visible = true;
            RadComboBoxIten.Visible = false;
            RadTextBoxValue.Visible = false;
            RadComboBoxPSN_RegisteType.Visible = false;
            //RadioButtonList1.Visible = false;
            RadDatePickerGetDateStart.Visible = true;
          
            //RadComboBoxIten.SelectedIndex = -1;
            //RadTextBoxValue.Text = "";
            //RadComboBoxPSN_RegisteType.SelectedIndex = -1;
            //RadioButtonList1.SelectedIndex = -1;
            RadDatePickerGetDateStart.SelectedDate = DateTime.Now.Date;

            RadioButtonListApplyStatus.SelectedValue = "注册有效期满且未延续注册";
            RadioButtonListApplyStatus.Enabled = false;

            lbl_Query1.Visible = false;
            RadComboBoxPSN_Age.Visible = false;
            lbl_Query3.Visible = false;
            RadComboBoxENT_Qyzz.Visible = false;

        }

        protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //switch (RadioButtonList2.SelectedValue)
            //{
            //    case "手动锁定":
            //    case "手动解锁":
            //        EnabledTrue();
            //        divCancel.Visible = false;
            //        divLock.Visible = true;
            //        break;
            //    case "手动注销":
            //        EnabledTrue();
            //        divCancel.Visible = true;
            //        divLock.Visible = false;
            //        break;
            //    case "证书过期":
            //    case "专业过期":
            //        EnabledFalse();
            //        divCancel.Visible = true;
            //        divLock.Visible = false;
            //        break;
            //    default:
            //        break;
            //}
            
            ButtonSearch_Click(null, null);
        }
        
        /// <summary>
        /// 导出查询结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonOutput_Click(object sender, EventArgs e)
        {
            var q = new QueryParamOB();
            switch (RadioButtonList2.SelectedValue)
            {
                case "手动锁定":
                case "手动解锁":
                    q = Lock();
                    break;
                case "手动注销":
                    q = Manual();
                    break;
                case "证书过期":
                case "专业过期":
                    q = Certificate();
                    break;
                default:
                    break;
            }
            if (CheckBoxZSSD.Checked == true)//在施锁定
            {
                q.Add("c.SDZT = 1");
            }
            if (CheckBoxYCZC.Checked == true)//异常注册
            {
                q.Add("L.LockID is not null");
            }
            if (CheckBoxNormalData.Checked == true)//无预警数据
            {
                q.Add("(c.SDZT = 0 or c.SDZT is null) and (L.LockID is null)");
            }
            if (RadioButtonGuaZheng.Checked == true)//未整改完成涉嫌挂证
            {
                q.Add("A.[PSN_RegisterNO] IN (select [CertificateCode] from [CheckFeedBack] where [DataStatusCode] >0 and [DataStatusCode] < 7 )");

            }
            if (RadioButtonRepeat.Checked == true)//一年两次重新注册（组织机构代码变换）
            {
//                //安重新注册计算（排除注销后重新注册与原单位是同一单位情况）
//                q.Add(@"A.PSN_CertificateNO IN (      
//                            select PSN_CertificateNO 
//                            from Apply 
//                            where ApplyType='重新注册' and ApplyStatus='已公告' and ConfirmResult='通过' and DATEADD(year,1,NoticeDate)>GETDATE()  
//                            group by PSN_CertificateNO
//                            having count(distinct ENT_OrganizationsCode) > 1
//                        )");

                //安重新注册计算（排除注销后重新注册与原单位是同一单位情况）
                q.Add(@"A.PSN_CertificateNO IN (      
                            select PSN_CertificateNO 
                            from Apply 
                            where ApplyType='重新注册' and ApplyStatus='已公告' and ConfirmResult='通过' and DATEADD(year,1,NoticeDate)>GETDATE()  
                            group by PSN_CertificateNO
                            having count(distinct ENT_OrganizationsCode) > 1
                        )
                         and exists
                         (
                           select 1 from 
                           (
                               select top 1 case when preApply.ENT_OrganizationsCode = firstApply.ENT_OrganizationsCode then 0 else 1 end as ifchangeunit 
                               from (
                                   select top 1 PSN_CertificateNO,NoticeDate,ENT_OrganizationsCode from apply 
                                   where PSN_CertificateNO = A.PSN_CertificateNO 
                                   and ApplyType='重新注册' and ApplyStatus='已公告' and ConfirmResult='通过' and DATEADD(year,1,NoticeDate)>GETDATE()  
                                   order by NoticeDate
                               ) as firstApply
                               inner join 
                               apply as preApply on firstApply.PSN_CertificateNO = preApply.PSN_CertificateNO and firstApply.NoticeDate > preApply.NoticeDate
	                           where preApply.ApplyStatus='已公告' and ( (preApply.ApplyTypeSub='执业企业变更' and preApply.ExamineResult='通过') or ( preApply.ConfirmResult='通过'))
                               order by preApply.NoticeDate desc
                           ) t where ifchangeunit=1
                         )");


                #region 安变换单位计算（初始、重新、执业企业变更）

//                q.Add(@"A.PSN_CertificateNO IN (                            
//                            select PSN_CertificateNO
//                            from
//                            (
//	                            select  PSN_CertificateNO ,min(ENT_OrganizationsCode) as ENT_OrganizationsCode
//	                            from Apply 
//	                            where ((ApplyTypeSub = '执业企业变更' and ApplyStatus = '已公告' and ExamineResult = '通过') or ((ApplyType = '初始注册' or ApplyType = '重新注册') and ConfirmResult = '通过' and ApplyStatus = '已公告'))
//	                            and DATEADD(year,1,NoticeDate) > GETDATE()  
//	                            group by PSN_CertificateNO 
//	                            having COUNT(distinct ENT_OrganizationsCode) > 1
//	                            union all
//	                            select  PSN_CertificateNO ,max(ENT_OrganizationsCode) as ENT_OrganizationsCode
//	                            from Apply 
//	                            where ((ApplyTypeSub = '执业企业变更' and ApplyStatus = '已公告' and ExamineResult = '通过') or ((ApplyType = '初始注册' or ApplyType = '重新注册') and ConfirmResult = '通过' and ApplyStatus = '已公告'))
//	                            and DATEADD(year,1,NoticeDate) > GETDATE()  
//	                            group by PSN_CertificateNO 
//	                            having COUNT(distinct ENT_OrganizationsCode) > 1
//	                            union all
//	                            SELECT PSN_CertificateNO,ENT_OrganizationsCode
//	                            from(
//		                            SELECT 
//			                            PSN_CertificateNO,
//			                            ENT_OrganizationsCode,
//			                            ROW_NUMBER() OVER (PARTITION BY PSN_CertificateNO ORDER BY NoticeDate desc) AS RowNum
//		                            FROM Apply
//		                            where PSN_CertificateNO in(
//				                            select  PSN_CertificateNO 
//				                            from Apply 
//				                            where ((ApplyTypeSub = '执业企业变更' and ApplyStatus = '已公告' and ExamineResult = '通过') or ((ApplyType = '初始注册' or ApplyType = '重新注册') and ConfirmResult = '通过' and ApplyStatus = '已公告'))
//				                            and DATEADD(year,1,NoticeDate) > GETDATE()  
//				                            group by PSN_CertificateNO 
//				                            having COUNT(distinct ENT_OrganizationsCode) > 1
//		                            )	
//		                            and ((ApplyTypeSub = '执业企业变更' and ApplyStatus = '已公告' and ExamineResult = '通过') or ((ApplyType = '初始注册' or ApplyType = '重新注册') and ConfirmResult = '通过' and ApplyStatus = '已公告'))
//		                            and DATEADD(year,1,NoticeDate) < GETDATE()
//	                            ) t
//	                            where 	RowNum=1  
//                            ) t
//                            group by PSN_CertificateNO 
//                            having COUNT(distinct ENT_OrganizationsCode) = COUNT(*)
                //                       )");
                #endregion
            }
                        
            try
            {
                //EXCEL表头明
                string head = @"姓名\性别\注册号\证件号码\企业名称\企业组织机构代码\建造师类别\注册专业及有效期\注销原因\处理人\处理时间\在施锁定\锁定项目\合同编号\中标企业\在施锁定时间\异常锁定\异常锁定时间\异常说明";
                //数据表的列明
                string column = @"A.PSN_Name\A.PSN_Sex\A.PSN_RegisterNo\A.PSN_CertificateNO\A.ENT_Name\A.ENT_OrganizationsCode\A.PSN_Level\A.ProfessionWithValid\A.PSN_CancelReason\A.XGR\CONVERT(varchar(100),A.XGSJ, 23)\case when C.SDZT=1 then '在施锁定'  else '' end\C.XMMC\C.HTBH\C.ZBQY\CONVERT(varchar(100),C.SDSJ, 23)\case when L.LockID is not null then '异常注册' else '' end\CONVERT(varchar(100),L.LockEndTime, 23)\L.LockRemark";

//                //EXCEL表头明
//                string head = @"姓名\性别\注册号\证件号码\企业名称\企业组织机构代码\建造师类别\注册专业及有效期\注销原因\处理人\处理时间\异常状态预警\异常说明";
//                //数据表的列明
//                string column = @"A.PSN_Name\A.PSN_Sex\A.PSN_RegisterNo\A.PSN_CertificateNO\A.ENT_Name\A.ENT_OrganizationsCode\A.PSN_Level\A.ProfessionWithValid\A.PSN_CancelReason\A.XGR\CONVERT(varchar(100),A.XGSJ, 23)\case when C.SDZT=1 then '【在施锁定】'  else '' end + case when L.LockStates='加锁' then '【异常注册】' else '' end\case when C.SDZT=1 then '【锁定项目：'+C.XMMC+'，合同编号：'+C.HTBH+'，中标企业：'+C.ZBQY+'，锁定时间：'+CONVERT(varchar(100),C.SDSJ, 23)+'】' else '' end +case when L.LockStates='加锁' then '【加锁时间：'+ CONVERT(varchar(100),L.LockEndTime, 23)+'，加锁原因：'+ L.LockContent+'】' else '' end
//";
               

                //过滤条件
                if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/Excel/"))) System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/UploadFiles/Excel/"));
                string filePath = string.Format("~/Upload/Excel/{0}_{1}.xls", DateTime.Now.ToString("yyyyMMdd"), Guid.NewGuid());
                CommonDAL.OutputXls(HttpContext.Current.Server.MapPath(filePath)
                    , @"dbo.View_JZS_TOW_WithProfession A 
                    left join [dbo].[View_JZS_TOW_Applying] B 
                        on a.PSN_ServerID=b.PSN_ServerID 
                    left join [jcsjk_RY_JZS_ZSSD] C 
                        on a.PSN_RegisterNO=c.ZCH 
                    left join [dbo].[LockJZS] L
                        on A.[PSN_ServerID] = L.[PSN_ServerID] and L.LockStatus='加锁' and LockEndTime > getdate()"
                    ,  q.ToWhereString(), "C.SDZT", head.ToString(), column.ToString());
                string size = CommonDAL.GetFileSize(HttpContext.Current.Server.MapPath(filePath));
                spanOutput.InnerHtml = string.Format(@"<a href=""{0}"">{1}</a><span  style=""padding-left:10px;"">（{2}）</span>"
                    ,UIHelp.AddUrlReadParam(filePath.Replace("~", ".."))
                    , "点击我下载"
                    , size);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "业务查询结果导出EXCEL失败！", ex);
            }
        }

        //加锁
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
                    filterString = string.Format(" {0} and A.PSN_ServerID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedStrKeysToString(RadGridQY));
                else//包含
                    filterString = string.Format(" {0} and A.PSN_ServerID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedStrKeysToString(RadGridQY));
            }

            int rowCount = CommonDAL.SelectRowCount(string.Format(@"
            select count(*) from [COC_TOW_Person_BaseInfo]
            where PSN_ServerID in(
                select A.PSN_ServerID
                from dbo.View_JZS_TOW_WithProfession A 
                left join [dbo].[View_JZS_TOW_Applying] B 
                    on a.PSN_ServerID=b.PSN_ServerID 
                left join [jcsjk_RY_JZS_ZSSD] C 
                    on a.PSN_RegisterNO=c.ZCH 
                left join [dbo].[LockJZS] L
                    on A.[PSN_ServerID] = L.[PSN_ServerID] and L.LockStatus='加锁' and LockEndTime > getdate()
                where 1=1 {0}
            );",filterString));

            LockJZSMDL o = new LockJZSMDL();
            o.LockEndTime = RadDatePickerLockEndTime.SelectedDate;
            o.LockPerson = PersonName;
            o.LockTime = DateTime.Now;
            o.LockType = "其他";
            o.LockRemark = RadTextBoxRemark.Text;
            o.LockStatus = "加锁";

            try
            {
                LockJZSDAL.BatchLock(o, filterString);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "批量锁定二级注册建造师失败！", ex);
                return;
            }

            UIHelp.WriteOperateLog(PersonName, UserID, "批量锁定二级注册建造师", string.Format("锁定了{0}本证书。", rowCount));
            ButtonSearch_Click(sender, e);
            UIHelp.layerAlert(Page, string.Format("成功锁定了{0}本证书。", rowCount));
        }

        //解锁
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
                    filterString = string.Format(" {0} and A.PSN_ServerID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedStrKeysToString(RadGridQY));
                else//包含
                    filterString = string.Format(" {0} and A.PSN_ServerID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedStrKeysToString(RadGridQY));
            }

            int rowCount = CommonDAL.SelectRowCount(string.Format(@"
            select count(*) from [COC_TOW_Person_BaseInfo]
            where PSN_ServerID in(
                select A.PSN_ServerID
                from dbo.View_JZS_TOW_WithProfession A 
                left join [dbo].[View_JZS_TOW_Applying] B 
                    on a.PSN_ServerID=b.PSN_ServerID 
                left join [jcsjk_RY_JZS_ZSSD] C 
                    on a.PSN_RegisterNO=c.ZCH 
                left join [dbo].[LockJZS] L
                    on A.[PSN_ServerID] = L.[PSN_ServerID] and L.LockStatus='加锁' and LockEndTime > getdate()
                where 1=1 {0}
            );", filterString));

            LockJZSMDL o = new LockJZSMDL();
            o.UnlockPerson = PersonName;
            o.UnlockTime = DateTime.Now;
            o.UnlockRemark = RadTextBoxRemark.Text;
            o.LockStatus = "解锁";

            try
            {
                LockJZSDAL.BatchUnlock(o, filterString);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "批量解锁二级注册建造师失败！", ex);
                return;
            }

            UIHelp.WriteOperateLog(PersonName, UserID, "批量解锁二级注册建造师", string.Format("解锁了{0}本证书。", rowCount));
            ButtonSearch_Click(sender, e);
            UIHelp.layerAlert(Page, string.Format("成功解锁了{0}本证书。", rowCount));
        }

        protected void RadGridQY_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            UpdateGridSelectedKeys(RadGridQY, "PSN_ServerID");
        }

        protected void RadGridQY_DataBound(object sender, EventArgs e)
        {
            UpdateGriSelectedStatus(RadGridQY, "PSN_ServerID");
        }
    }
}