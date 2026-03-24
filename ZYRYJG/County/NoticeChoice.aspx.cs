using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZYRYJG.County
{
    /// <summary>
    /// 页面功能：选择二级建造师公告数据。
    /// 页面传递参数说明：
    /// Request["01"]=NoticeCode 公告编号
    /// Request["02"]=applytype 注册类型
    /// Request["03"]=页面访问方式{null=新增；xg=修改；xx=详细}
    /// </summary>
    public partial class NoticeChoice : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "County/NoticeLook.aspx";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["01"]))
                {
                    ViewState["NoticeCode"] = Request["01"];
                    string ConfirmResult=ApplyDAL.GetConfirmResultByNoticeCode(Request["01"]);
                    RadioButtonListReportStatus.ClearSelection();
                    RadioButtonListReportStatus.Items.FindByValue(ConfirmResult).Selected = true;

                    //修改时不能修改申报事项类型
                    UIHelp.SelectDropDownListItemByValue(RadComboBoxIfContinue1, Request["02"]);
                    UIHelp.SetReadOnly(RadComboBoxIfContinue1, true);
                }
                ButtonSearch_Click(sender, e);
            }
        }

        //查询待公告数据
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
                q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ApplyStatus.已决定));
                q.Add(string.Format("ConfirmResult = '{0}'", RadioButtonListReportStatus.SelectedValue));//决定结果
            }
            //修改
            if (Request.QueryString["03"] != null && Request.QueryString["03"] == "xg")//edit
            {
                q.Add("NoticeDate is null");//只保存了待公告数据（有公告批次号），未公告（无公告时间）
                q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ApplyStatus.已决定));
                q.Add(string.Format("ConfirmResult = '{0}'", RadioButtonListReportStatus.SelectedValue));//决定结果
                q.Add(string.Format("NoticeCode ='{0}'", ViewState["NoticeCode"]));//公告批次号
            }
            //详细
            if (Request.QueryString["03"] != null && Request.QueryString["03"] == "xx")
            {
                q.Add(string.Format("NoticeCode ='{0}'", ViewState["NoticeCode"]));//公告批次号
            }

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridADDRY.CurrentPageIndex = 0;
            RadGridADDRY.DataSourceID = ObjectDataSource1.ID;
        }

        //保存公告
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            string selectApplyIDs = GetRadGridADDRYSelect();
            if (selectApplyIDs == "")
            {
                UIHelp.layerAlert(Page, "您尚未勾选任何人员！");
                return;
            }

            string NoticeCode = ""; //公告批次号
            try
            {
                if (ViewState["NoticeCode"] != null)//edit
                {
                    NoticeCode = ViewState["NoticeCode"].ToString();
                }
                else
                {
                    NoticeCode = ApplyDAL.GetNextPublicCode("NoticeCode", RadComboBoxIfContinue1.SelectedValue);
                }
                ApplyDAL.SaveNoticeCode(NoticeCode, selectApplyIDs);

                UIHelp.WriteOperateLog(UserName, UserID, "二级注册建造师公告保存成功", string.Format("公告批次号：{0}", NoticeCode));
                UIHelp.ParentAlert(Page, "保存成功！", true);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "保存要公告的二级注册建造师信息失败", ex);
            }

            try
            {
                Export(NoticeCode);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "公告查询结果导出EXCEL失败！", ex);
            }
        }

        //        //立刻公告
        //        protected void ButtonReport_Click(object sender, EventArgs e)
        //        {
        //            //开启事务
        //            string sql = "";
        //            DateTime doTime = DateTime.Now;//处理时间

        //            string selectApplyIDs = GetRadGridADDRYSelect();
        //            if (selectApplyIDs == "")
        //            {
        //                UIHelp.layerAlert(Page, "您尚未勾选任何人员！");
        //                return;
        //            }

        //            DBHelper db = new DBHelper();
        //            DbTransaction tran = db.BeginTransaction();
        //            try
        //            {
        //                string date;
        //                //公告批次号
        //                if (ViewState["NoticeCode"] != null)//edit
        //                    date = ViewState["NoticeCode"].ToString();
        //                //公告批次号
        //                else
        //                    date = ApplyDAL.GetNextPublicCode("NoticeCode", RadComboBoxIfContinue1.SelectedValue);

        //                //更新申请表
        //                ApplyDAL.ExeNoticeReport(tran, EnumManager.ApplyStatus.已公告, doTime, UserName, date, selectApplyIDs);

        //                string type = RadComboBoxIfContinue1.SelectedValue;
        //                //公告编号
        //                string noticecode = date;
        //                switch (type)
        //                {
        //                    case "增项注册":

        //                        #region 增项注册
        //                        //专业写入历史表
        //                        sql = @"INSERT INTO [dbo].[COC_TOW_Register_Profession_His]([His_ID],[PRO_ServerID],[PSN_ServerID],[PRO_Profession],[PRO_ValidityBegin],[PRO_ValidityEnd],[DogID],[ENT_Province_Code],[DownType],[LastModifyTime],[ApplyType],[GetDate]) 
        //                                SELECT newid(),p.[PRO_ServerID],p.[PSN_ServerID],p.[PRO_Profession],p.[PRO_ValidityBegin],p.[PRO_ValidityEnd],p.[DogID],p.[ENT_Province_Code],p.[DownType],p.[LastModifyTime],'{0}','{1}'
        //                                FROM [dbo].[Apply] a 	
        //                                inner join [dbo].[COC_TOW_Register_Profession] p on a.PSN_ServerID = p.PSN_ServerID
        //                                where a.NoticeCode='{2}' and a.ConfirmResult='通过'";

        //                        CommonDAL.ExecSQL(tran, string.Format(sql, "增项注册", DateTime.Now, noticecode));

        //                        //删除过期的增项专业
        //                        sql = @"DELETE FROM [dbo].[COC_TOW_Register_Profession]
        //                                WHERE [PRO_ServerID] in
        //                                (
        //                                    SELECT  p.[PRO_ServerID]
        //                                    FROM [dbo].[Apply] a 
        //                                    inner join [dbo].[ApplyAddItem] b on a.ApplyID = b.ApplyID
        //                                    inner join [dbo].[COC_TOW_Register_Profession] p on a.PSN_ServerID = p.PSN_ServerID and (p.PRO_Profession = b.[AddItem1] or p.PRO_Profession = b.[AddItem2])
        //                                    where  a.NoticeCode='{0}' and a.ConfirmResult='通过'
        //                                )";

        //                        CommonDAL.ExecSQL(tran, string.Format(sql, noticecode));

        //                        //写入增项专业表
        //                        sql = @"INSERT INTO [dbo].[COC_TOW_Register_Profession]([PRO_ServerID],[PSN_ServerID],[PRO_Profession],[PRO_ValidityBegin],[PRO_ValidityEnd],[ENT_Province_Code],[LastModifyTime])
        //                                  select newid(),a.PSN_ServerID,ZY,convert(char(10),a.[ConfirmDate],120),[dbo].[GET_PSN_CertificateValidity](convert(char(10),dateadd(day,-1,dateadd(year,3,a.[ConfirmDate])),120),a.PSN_CertificateNO,a.PSN_Level),'110000','{1}'
        //                                  FROM [dbo].[Apply] a 
        //                                  inner join
        //                                  (
        //                                      select applyid,[AddItem1] ZY from  [dbo].[ApplyAddItem] where [AddItem1] is not null
        //                                      union
        //                                      select applyid,[AddItem2] ZY from  [dbo].[ApplyAddItem] where [AddItem2] is not null
        //                                  ) z on  a.applyid = z.applyid
        //                                   where a.NoticeCode='{0}' and a.ConfirmResult='通过'";
        //                        CommonDAL.ExecSQL(tran, string.Format(sql, noticecode, DateTime.Now));

        //                        //人员表写入历史
        //                        sql = @"INSERT INTO [dbo].[COC_TOW_Person_BaseInfo_His]
        //                                ([HisID],[PSN_ServerID],[ENT_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[BeginTime],[EndTime],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Telephone],[PSN_Email],[PSN_PMGrade],[PSN_PMCertificateNo],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_ChangeReason],[PSN_BeforENT_Name],[PSN_BeforENT_ServerID],[PSN_BeforPersonName],[PSN_InterprovincialChange],[PSN_ExpiryReasons],[PSN_ExpiryDate],[PSN_RenewalProfession],[PSN_AddProfession],[PSN_CancelPerson],[PSN_CancelReason],[PSN_ReReasons],[PSN_ReContent],[PSN_CheckCode],[ENT_Province_Code],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[GteDate])	
        //                          SELECT newid(),[PSN_ServerID],[ENT_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[BeginTime],[EndTime],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Telephone],[PSN_Email],[PSN_PMGrade],[PSN_PMCertificateNo],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_ChangeReason],[PSN_BeforENT_Name],[PSN_BeforENT_ServerID],[PSN_BeforPersonName],[PSN_InterprovincialChange],[PSN_ExpiryReasons],[PSN_ExpiryDate],[PSN_RenewalProfession],[PSN_AddProfession],[PSN_CancelPerson],[PSN_CancelReason],[PSN_ReReasons],[PSN_ReContent],[PSN_CheckCode],[ENT_Province_Code],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],getdate() 
        //                          FROM [dbo].[COC_TOW_Person_BaseInfo] 
        //                          where [PSN_ServerID] in(select [PSN_ServerID] from [dbo].[Apply] where NoticeCode='{0}' and ConfirmResult='通过')";

        //                        CommonDAL.ExecSQL(tran, string.Format(sql, noticecode));

        //                        //更新人员正式表
        //                        sql = @"update [dbo].[COC_TOW_Person_BaseInfo] 
        //                                    set 
        //                                    [COC_TOW_Person_BaseInfo].PSN_RegisteProfession =
        //		                                    replace(','+
        //		                                    (
        //		                                    select ',' +[COC_TOW_Register_Profession].PRO_Profession from [dbo].[COC_TOW_Register_Profession]
        //		                                     where [COC_TOW_Register_Profession].PSN_ServerID = a.PSN_ServerID
        //		                                     for xml path('')
        //		                                    ),',,','')
        //                                    ,[COC_TOW_Person_BaseInfo].[PSN_CertificateValidity] =
        //		                                    (
        //		                                    select max([COC_TOW_Register_Profession].PRO_ValidityEnd) from [dbo].[COC_TOW_Register_Profession] where [COC_TOW_Register_Profession].PSN_ServerID = a.PSN_ServerID
        //		                                    )               
        //                                    ,[COC_TOW_Person_BaseInfo].[PSN_RegistePermissionDate] = a.[ConfirmDate]
        //                                    ,[COC_TOW_Person_BaseInfo].[PSN_RegisteType]='04'
        //                                    ,[COC_TOW_Person_BaseInfo].[XGR]='{1}'
        //                                    ,[COC_TOW_Person_BaseInfo].[XGSJ]='{0}'
        //                                    ,[COC_TOW_Person_BaseInfo].[PSN_AddProfession]=                                   
        //		                                    (
        //		                                        select [ApplyAddItem].AddItem1 + case when [ApplyAddItem].AddItem2 is null then '' else ',' +[ApplyAddItem].AddItem2 end
        //                                                from [dbo].[ApplyAddItem]
        //		                                        where [ApplyAddItem].Applyid = a.Applyid		                                   
        //		                                    )
        //                                FROM [dbo].[COC_TOW_Person_BaseInfo] inner join [dbo].[Apply] a 
        //                                on [COC_TOW_Person_BaseInfo].[PSN_RegisterNO] = a.[PSN_RegisterNO]
        //                                where a.NoticeCode='{2}' and a.ConfirmResult='通过'";


        //                        CommonDAL.ExecSQL(tran, string.Format(sql
        //                            , DateTime.Now
        //                            , UserName
        //                            , noticecode));

        //                        //更新证书附件中需要被覆盖的附件为历史附件
        //                        CommonDAL.ExecSQL(tran, string.Format(@"
        //                                    Insert into  COC_TOW_Person_FileHistory(HisID,FileID,PSN_RegisterNO ,WriteTime) 
        //                                    SELECT newid(),[COC_TOW_Person_File].[FileID],[COC_TOW_Person_File].[PSN_RegisterNO],getdate()
        //                                    from [dbo].[COC_TOW_Person_File]
        //                                    inner join 
        //                                    (
        //	                                    select b.FileID, b.PSN_RegisterNO,b.DataType
        //	                                    from 
        //	                                    (
        //		                                    select distinct [FileInfo].DataType,[Apply].PSN_RegisterNo 
        //		                                    from [dbo].[FileInfo]
        //		                                    inner join [dbo].[ApplyFile]
        //		                                    on [FileInfo].FileID = [ApplyFile].FileID
        //		                                    inner join [dbo].[Apply] on [ApplyFile].ApplyID = [Apply].ApplyID
        //		                                    where [Apply].NoticeCode='{0}' and [Apply].ConfirmResult='通过'
        //	                                    ) a
        //	                                    inner join 
        //	                                    (
        //		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
        //		                                    from [dbo].[FileInfo]
        //		                                    inner join [dbo].[COC_TOW_Person_File]
        //		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
        //                                            inner join [dbo].[Apply] 
        //                                            on [COC_TOW_Person_File].PSN_RegisterNO = [Apply].PSN_RegisterNo
        //		                                    where  [Apply].NoticeCode='{0}' and [Apply].ConfirmResult='通过'
        //	                                    ) b 
        //	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
        //                                    ) t
        //                                    on [COC_TOW_Person_File].FileID = t.FileID", noticecode));


        //                        CommonDAL.ExecSQL(tran, string.Format(@"
        //                                    delete from [dbo].[COC_TOW_Person_File]
        //                                    where FileID in( select [COC_TOW_Person_File].[FileID]
        //                                    from [dbo].[COC_TOW_Person_File]
        //                                    inner join 
        //                                    (
        //	                                    select b.FileID, b.PSN_RegisterNO,b.DataType
        //	                                    from 
        //	                                    (
        //		                                    select distinct [FileInfo].DataType,[Apply].PSN_RegisterNo 
        //		                                    from [dbo].[FileInfo]
        //		                                    inner join [dbo].[ApplyFile]
        //		                                    on [FileInfo].FileID = [ApplyFile].FileID
        //		                                    inner join [dbo].[Apply] on [ApplyFile].ApplyID = [Apply].ApplyID
        //		                                    where [Apply].NoticeCode='{0}' and [Apply].ConfirmResult='通过' 
        //	                                    ) a
        //	                                    inner join 
        //	                                    (
        //		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
        //		                                    from [dbo].[FileInfo]
        //		                                    inner join [dbo].[COC_TOW_Person_File]
        //		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
        //                                            inner join [dbo].[Apply] 
        //                                            on [COC_TOW_Person_File].PSN_RegisterNO = [Apply].PSN_RegisterNo
        //		                                    where  [Apply].NoticeCode='{0}' and [Apply].ConfirmResult='通过' 
        //	                                    ) b 
        //	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
        //                                    ) t
        //                                    on [COC_TOW_Person_File].FileID = t.FileID
        //                                    )", noticecode));

        //                        //将申请单附件写入证书附件库
        //                        CommonDAL.ExecSQL(tran, string.Format(@"
        //                                    INSERT INTO [dbo].[COC_TOW_Person_File]([FileID],[PSN_RegisterNO],[IsHistory])
        //                                    select [ApplyFile].FileID,[Apply].PSN_RegisterNo,0 
        //                                    from [dbo].[ApplyFile]
        //                                    inner join [dbo].[Apply] 
        //                                    on [ApplyFile].ApplyID = [Apply].ApplyID 
        //                                    where [Apply].NoticeCode='{0}' and [Apply].ConfirmResult='通过' ", noticecode));

        //                          tran.Commit();
        //                          UIHelp.WriteOperateLog(UserName, UserID, "公告成功", string.Format("公告时间：{0},公告批次号{1},公告类型：{2}", DateTime.Now, noticecode, "增项注册"));


        //                        #endregion
        //                        break;
        //                    case "延期注册":
        //                        #region 延续注册
        //                        //专业写入历史表
        //                        sql = @"INSERT INTO [dbo].[COC_TOW_Register_Profession_His]([His_ID],[PRO_ServerID],[PSN_ServerID],[PRO_Profession],[PRO_ValidityBegin],[PRO_ValidityEnd],[DogID],[ENT_Province_Code],[DownType],[LastModifyTime],[ApplyType],[GetDate]) 
        //                                SELECT newid(),p.[PRO_ServerID],p.[PSN_ServerID],p.[PRO_Profession],p.[PRO_ValidityBegin],p.[PRO_ValidityEnd],p.[DogID],p.[ENT_Province_Code],p.[DownType],p.[LastModifyTime],'{0}','{1}'
        //                                FROM [dbo].[Apply] a 	
        //                                inner join [dbo].[COC_TOW_Register_Profession] p on a.PSN_ServerID = p.PSN_ServerID
        //                                where a.NoticeCode='{2}' and a.ConfirmResult='通过'";

        //                        CommonDAL.ExecSQL(tran, string.Format(sql, "延期注册", doTime, noticecode));

        //                        //更新专业表有效期（统一更新为申请续期专业最小有效期+3年）
        //                        sql = @"update [dbo].[COC_TOW_Register_Profession]
        //                                set [COC_TOW_Register_Profession].[PRO_ValidityEnd] = case when t.minValid is null then [COC_TOW_Register_Profession].[PRO_ValidityEnd] else [dbo].[GET_PSN_CertificateValidity](dateadd(day,-1,dateadd(year,3,t.minValid)),t.PSN_CertificateNO,t.PSN_Level) end
        //                                ,[LastModifyTime]='{1}'
        //                                FROM 
        //                                (
        //	                                select  a.ApplyID ,a.PSN_ServerID,a.NoticeCode,a.ConfirmResult,c.[PSN_RegisteProfession1],c.[PSN_RegisteProfession2],c.[PSN_RegisteProfession3],a.PSN_CertificateNO,a.PSN_Level
        //                                    ,minValid=
        //                                     case when isnull(c.PSN_CertificateValidity3,'2050-1-1') <
        //                                     (
        //	                                     case when isnull(c.PSN_CertificateValidity1,'2050-1-1') < isnull(c.PSN_CertificateValidity2,'2050-1-1') 
        //	                                     then c.PSN_CertificateValidity1
        //	                                     else c.PSN_CertificateValidity2 end
        //                                     )
        //                                     then c.PSN_CertificateValidity3 else 
        //                                     (
        //	                                    case  when isnull(c.PSN_CertificateValidity1,'2050-1-1') < isnull(c.PSN_CertificateValidity2,'2050-1-1') 
        //	                                    then c.PSN_CertificateValidity1
        //	                                    else c.PSN_CertificateValidity2 end
        //                                      )
        //                                     end 
        //	                                from [dbo].[Apply] a 
        //	                                inner join [dbo].[ApplyContinue] c 
        //	                                on a.NoticeCode ='{0}' and a.ConfirmResult='通过' and a.applyid =c.applyid
        //                                ) t
        //                                inner join [dbo].[COC_TOW_Register_Profession] 
        //                                on t.PSN_ServerID = [COC_TOW_Register_Profession].PSN_ServerID
        //                                and 
        //                                (
        //	                                t.[PSN_RegisteProfession1] = [COC_TOW_Register_Profession].[PRO_Profession] 
        //	                                or t.[PSN_RegisteProfession2] = [COC_TOW_Register_Profession].[PRO_Profession] 
        //	                                or t.[PSN_RegisteProfession3] = [COC_TOW_Register_Profession].[PRO_Profession]
        //                                ) 
        //                                where t.NoticeCode ='{0}' and t.ConfirmResult='通过'";
        //                        CommonDAL.ExecSQL(tran, string.Format(sql, noticecode, doTime.ToString("yyyy-MM-dd HH:mm:ss")));

        //                        //人员表写入历史
        //                        sql = @"INSERT INTO [dbo].[COC_TOW_Person_BaseInfo_His]
        //                                ([HisID],[PSN_ServerID],[ENT_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[BeginTime],[EndTime],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Telephone],[PSN_Email],[PSN_PMGrade],[PSN_PMCertificateNo],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_ChangeReason],[PSN_BeforENT_Name],[PSN_BeforENT_ServerID],[PSN_BeforPersonName],[PSN_InterprovincialChange],[PSN_ExpiryReasons],[PSN_ExpiryDate],[PSN_RenewalProfession],[PSN_AddProfession],[PSN_CancelPerson],[PSN_CancelReason],[PSN_ReReasons],[PSN_ReContent],[PSN_CheckCode],[ENT_Province_Code],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[GteDate])	
        //                              SELECT newid(),[PSN_ServerID],[ENT_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[BeginTime],[EndTime],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Telephone],[PSN_Email],[PSN_PMGrade],[PSN_PMCertificateNo],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_ChangeReason],[PSN_BeforENT_Name],[PSN_BeforENT_ServerID],[PSN_BeforPersonName],[PSN_InterprovincialChange],[PSN_ExpiryReasons],[PSN_ExpiryDate],[PSN_RenewalProfession],[PSN_AddProfession],[PSN_CancelPerson],[PSN_CancelReason],[PSN_ReReasons],[PSN_ReContent],[PSN_CheckCode],[ENT_Province_Code],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],getdate() 
        //                              FROM [dbo].[COC_TOW_Person_BaseInfo] 
        //                              where [PSN_ServerID] in(select [PSN_ServerID] from [dbo].[Apply] where NoticeCode ='{0}' and ConfirmResult='通过')";

        //                        CommonDAL.ExecSQL(tran, string.Format(sql, noticecode));

        //                        //更新人员正式表
        //                        sql = @"update [dbo].[COC_TOW_Person_BaseInfo] 
        //                                    set [COC_TOW_Person_BaseInfo].[PSN_CertificateValidity] =
        //		                                    (
        //		                                    select max([COC_TOW_Register_Profession].PRO_ValidityEnd) from [dbo].[COC_TOW_Register_Profession] where [COC_TOW_Register_Profession].PSN_ServerID = a.PSN_ServerID
        //		                                    )               
        //                                    ,[COC_TOW_Person_BaseInfo].[PSN_RegistePermissionDate] = a.[ConfirmDate]
        //                                    ,[COC_TOW_Person_BaseInfo].[PSN_RegisteType]='03'
        //                                    ,[COC_TOW_Person_BaseInfo].[XGR]='{1}'
        //                                    ,[COC_TOW_Person_BaseInfo].[XGSJ]='{0}'
        //                                    ,[COC_TOW_Person_BaseInfo].[PSN_RenewalProfession]=a.PSN_RegisteProfession
        //                                FROM [dbo].[COC_TOW_Person_BaseInfo] inner join [dbo].[Apply] a 
        //                                on [COC_TOW_Person_BaseInfo].[PSN_RegisterNO] = a.[PSN_RegisterNO]
        //                                where a.NoticeCode ='{2}' and a.ConfirmResult='通过'";
        //                        CommonDAL.ExecSQL(tran, string.Format(sql
        //                            , doTime
        //                            , UserName
        //                            , noticecode));


        //                        //更新证书附件中需要被覆盖的附件为历史附件
        //                        CommonDAL.ExecSQL(tran, string.Format(@"
        //                                    Insert into  COC_TOW_Person_FileHistory(HisID,FileID,PSN_RegisterNO ,WriteTime) 
        //                                    SELECT newid(),[COC_TOW_Person_File].[FileID],[COC_TOW_Person_File].[PSN_RegisterNO],getdate()
        //                                    from [dbo].[COC_TOW_Person_File]
        //                                    inner join 
        //                                    (
        //	                                    select b.FileID, b.PSN_RegisterNO,b.DataType
        //	                                    from 
        //	                                    (
        //		                                    select distinct [FileInfo].DataType,[Apply].PSN_RegisterNo 
        //		                                    from [dbo].[FileInfo]
        //		                                    inner join [dbo].[ApplyFile]
        //		                                    on [FileInfo].FileID = [ApplyFile].FileID
        //		                                    inner join [dbo].[Apply] on [ApplyFile].ApplyID = [Apply].ApplyID
        //		                                    where  [Apply].NoticeCode='{0}' and [Apply].ConfirmResult='通过' 
        //	                                    ) a
        //	                                    inner join 
        //	                                    (
        //		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
        //		                                    from [dbo].[FileInfo]
        //		                                    inner join [dbo].[COC_TOW_Person_File]
        //		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
        //                                            inner join [dbo].[Apply] 
        //                                            on [COC_TOW_Person_File].PSN_RegisterNO = [Apply].PSN_RegisterNo
        //		                                    where   [Apply].NoticeCode='{0}' and [Apply].ConfirmResult='通过' 
        //	                                    ) b 
        //	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
        //                                    ) t
        //                                    on [COC_TOW_Person_File].FileID = t.FileID", noticecode));


        //                        CommonDAL.ExecSQL(tran, string.Format(@"
        //                                    delete from [dbo].[COC_TOW_Person_File]
        //                                    where FileID in( select [COC_TOW_Person_File].[FileID]
        //                                    from [dbo].[COC_TOW_Person_File]
        //                                    inner join 
        //                                    (
        //	                                    select b.FileID, b.PSN_RegisterNO,b.DataType
        //	                                    from 
        //	                                    (
        //		                                    select distinct [FileInfo].DataType,[Apply].PSN_RegisterNo 
        //		                                    from [dbo].[FileInfo]
        //		                                    inner join [dbo].[ApplyFile]
        //		                                    on [FileInfo].FileID = [ApplyFile].FileID
        //		                                    inner join [dbo].[Apply] on [ApplyFile].ApplyID = [Apply].ApplyID
        //		                                    where  [Apply].NoticeCode='{0}' and [Apply].ConfirmResult='通过' 
        //	                                    ) a
        //	                                    inner join 
        //	                                    (
        //		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
        //		                                    from [dbo].[FileInfo]
        //		                                    inner join [dbo].[COC_TOW_Person_File]
        //		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
        //                                            inner join [dbo].[Apply] 
        //                                            on [COC_TOW_Person_File].PSN_RegisterNO = [Apply].PSN_RegisterNo
        //		                                    where   [Apply].NoticeCode='{0}' and [Apply].ConfirmResult='通过' 
        //	                                    ) b 
        //	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
        //                                    ) t
        //                                    on [COC_TOW_Person_File].FileID = t.FileID
        //                                    )", noticecode));

        //                        //将申请单附件写入证书附件库
        //                        CommonDAL.ExecSQL(tran, string.Format(@"
        //                                    INSERT INTO [dbo].[COC_TOW_Person_File]([FileID],[PSN_RegisterNO],[IsHistory])
        //                                    select [ApplyFile].FileID,[Apply].PSN_RegisterNo,0 
        //                                    from [dbo].[ApplyFile]
        //                                    inner join [dbo].[Apply] 
        //                                    on [ApplyFile].ApplyID = [Apply].ApplyID 
        //                                    where  [Apply].NoticeCode='{0}' and [Apply].ConfirmResult='通过'  ", noticecode));

        //                            tran.Commit();
        //                            UIHelp.WriteOperateLog(UserName, UserID, "公告成功", string.Format("公告时间：{0},公告批次号{1},公告类型：{2}", DateTime.Now, noticecode, "延期注册"));
        //                            //UIHelp.ParentAlert(Page, "公告成功！", true);

        //                        #endregion
        //                        break;
        //                }

        //                UIHelp.ParentAlert(Page, "公告成功！", true);
        //            }
        //            catch (Exception ex)
        //            {
        //                tran.Rollback();
        //                UIHelp.WriteErrorLog(Page, "公告失败", ex);
        //            }
        //        }

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
               && RadGridADDRY.MasterTableView.DataKeyValues[0]["NoticeMan"] != DBNull.Value)//已公示
            {
                //ButtonReport.Visible = false;
                BtnSave.Visible = false;
                tableSearch.Visible = false;
            }
        }

        /// <summary>
        /// 导出公告结果
        /// </summary>
        /// <param name="NoticeCode">公告批次号</param>
        public void Export(string NoticeCode)
        {
            if (RadGridADDRY.MasterTableView.Items.Count == 0)
            {
                return;
            }

            string a = @"<tr style=""height:30pt""><td style=""font-weight:bold;text-align:center;"" class=""noborder"" colspan=""5"">{1}北京市二级建造师{0}条件的人员名单</td></tr>";
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
                a = @"<tr style=""height:30pt""><td style=""font-weight:bold;text-align:center;"" class=""noborder"" colspan=""6"">{1}北京市二级建造师{0}条件的人员名单</td></tr>";
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
            string filterSql = string.Format(" and NoticeCode ='{0}'", NoticeCode);
            if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/ReportXls/")))
            {
                System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Upload/ReportXls/"));
            }
            string filePath = string.Format("~/Upload/ReportXls/Excel{0}.xls", NoticeCode);
            CommonDAL.OutputXls(HttpContext.Current.Server.MapPath(filePath)
                , "[dbo].[Apply]"
                , filterSql
                , "row_number() over(order by [dbo].[Apply].[ApplyID])"
                , head.ToString()
                , column.ToString()
                , string.Format(a, RadComboBoxIfContinue1.SelectedValue, RadioButtonListReportStatus.SelectedValue == "通过" ? "符合" : "不符合")
                , Foot
            );
        }
    }
}