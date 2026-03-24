using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Model;
using DataAccess;
using System.Text;
using System.IO;
using Telerik.Web.UI;

namespace ZYRYJG.County
{
    public partial class BusinessSummarizing : BasePage
    {
        //业务员上报
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                BindRadGridHZSB();
            }
        }
        private void BindRadGridHZSB()
        {
            //select  ENT_City,[ApplyType],cast([ReportDate] as date) as ReportDate,count(*) as ManCount
            //,case when max([ReportMan]) is null then '未上报' else '已上报' end ReportStatus
            // FROM [dbo].[Apply]
            //where ReportDate is not null and ENT_City='朝阳区'
            //group by ENT_City,[ApplyType],cast([ReportDate] as date) 
            //having case when max([ReportMan]) is null then '未上报' else '已上报' end like '%'

            QueryParamOB q = new QueryParamOB();
            q.Add(string.Format("t.ENT_City like '{0}%' and t.ReportStatus like '{1}'"
                , Region
                , RadioButtonListReportStatus.SelectedValue == "" ? "%" : RadioButtonListReportStatus.SelectedValue
                ));

            ObjectDataSource1.SelectParameters.Clear();
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridHZSB.CurrentPageIndex = 0;
            RadGridHZSB.DataSourceID = ObjectDataSource1.ID;
        }

        /// <summary>
        /// 是否上传了汇总扫描件
        /// </summary>
        /// <param name="ReportCode">汇总编号</param>
        /// <returns>true：已经上传，false：尚未上传</returns>
        protected bool IfUploadReportImg(string ReportCode)
        {
            try
            {
                DirectoryInfo TheFolder = new DirectoryInfo(Server.MapPath(string.Format("~/Upload/ReportImg/{0}/", ReportCode)));
                FileInfo[] fi = TheFolder.GetFiles("*.jpg");
                if (fi.Length > 0)
                    return true;
                else
                    return false;
            }
            catch { return false; }
        }

        //变换上报状态
        protected void RadioButtonListReportStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindRadGridHZSB();
        }

        protected void RadGridHZSB_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "delete"://删除
                    try
                    {
                        ApplyDAL.DelPatchReport(RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ReportCode"].ToString()
                        , Region, RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ApplyType"].ToString());
                        BindRadGridHZSB();
                        UIHelp.layerAlert(Page, "删除成功！", 6, 2000);

                    }
                    catch (Exception ex)
                    {
                        UIHelp.WriteErrorLog(Page, "删除上报清单失败", ex);
                    }

                    break;
                case "report"://上报

                    if (RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ApplyTypeSub"].ToString() == "执业企业变更"
                        || RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ApplyType"].ToString() == "初始注册"
                        || RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ApplyType"].ToString() == "重新注册")
                    {

                        //int count = CommonDAL.GetRowCount("Apply", "*", string.Format(" and  ReportCode='{0}' and [CheckXSL]=1", RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ReportCode"]));
                        //if (count > 0)
                        //{
                        //    UIHelp.layerAlert(Page, string.Format("记录中存在{0}条新设立企业人员，请在企业资质审批合格后再上报！", count));
                        //    return;
                        //}
                        if (RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ApplyTypeSub"].ToString() == "执业企业变更")
                        {
                            int count2 = CommonDAL.GetRowCount("Apply", "*", string.Format("and  ReportCode='{0}' and [CheckZSSD]=1", RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ReportCode"]));
                            if (count2 > 0)
                            {
                                UIHelp.layerAlert(Page, string.Format("记录中存在{0}条在施锁定，请解锁后再上报！", count2));
                                return;
                            }
                        }
                    }
                    string sql = "";
                    switch (RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ApplyTypeSub"].ToString())
                    {
                        case "执业企业变更"://办结

                            #region 执业企业变更

                            DBHelper db = new DBHelper();
                            DbTransaction tran = db.BeginTransaction();
                            try
                            {
                                //更新申请单
                                ApplyDAL.PatchReportFinish(tran
                                 , RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ReportCode"].ToString()
                                , Region
                                , RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ApplyType"].ToString()
                                , UserName
                                );

                                //正式表写入历史表
                                sql = @"INSERT INTO [dbo].[COC_TOW_Person_BaseInfo_His]
                                ([HisID],[PSN_ServerID],[ENT_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[BeginTime],[EndTime],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Telephone],[PSN_Email],[PSN_PMGrade],[PSN_PMCertificateNo],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_ChangeReason],[PSN_BeforENT_Name],[PSN_BeforENT_ServerID],[PSN_BeforPersonName],[PSN_InterprovincialChange],[PSN_ExpiryReasons],[PSN_ExpiryDate],[PSN_RenewalProfession],[PSN_AddProfession],[PSN_CancelPerson],[PSN_CancelReason],[PSN_ReReasons],[PSN_ReContent],[PSN_CheckCode],[ENT_Province_Code],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[GteDate])	
                                  SELECT newid(),[PSN_ServerID],[ENT_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[BeginTime],[EndTime],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Telephone],[PSN_Email],[PSN_PMGrade],[PSN_PMCertificateNo],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_ChangeReason],[PSN_BeforENT_Name],[PSN_BeforENT_ServerID],[PSN_BeforPersonName],[PSN_InterprovincialChange],[PSN_ExpiryReasons],[PSN_ExpiryDate],[PSN_RenewalProfession],[PSN_AddProfession],[PSN_CancelPerson],[PSN_CancelReason],[PSN_ReReasons],[PSN_ReContent],[PSN_CheckCode],[ENT_Province_Code],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],getdate() 
                                  FROM [dbo].[COC_TOW_Person_BaseInfo] 
                                  where [PSN_ServerID] in(select [PSN_ServerID] from [dbo].[Apply] where ReportCode='{0}' and ExamineResult='通过')";

                                CommonDAL.ExecSQL(tran, string.Format(sql, RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ReportCode"]));

                                //更新正式表
                                sql = @"UPDATE [dbo].[COC_TOW_Person_BaseInfo]
                                            SET [COC_TOW_Person_BaseInfo].[PSN_BeforENT_ServerID] = [Apply].PSN_ServerID
                                            ,[COC_TOW_Person_BaseInfo].[PSN_BeforENT_Name] = [ApplyChange].OldENT_Name
                                            ,[COC_TOW_Person_BaseInfo].[ENT_ServerID] = [Apply].ENT_ServerID
                                            ,[COC_TOW_Person_BaseInfo].[ENT_Name] = [Apply].ENT_Name
                                            ,[COC_TOW_Person_BaseInfo].[ENT_OrganizationsCode] = [Apply].ENT_OrganizationsCode
                                            ,[COC_TOW_Person_BaseInfo].[ENT_City] = [Apply].ENT_City 
                                            ,[COC_TOW_Person_BaseInfo].[END_Addess] = [ApplyChange].[END_Addess]                                         
                                            ,[COC_TOW_Person_BaseInfo].[PSN_ChangeReason]='{1}'
                                            ,[COC_TOW_Person_BaseInfo].[PSN_RegisteType]='{2}'
                                            ,[COC_TOW_Person_BaseInfo].[PSN_RegistePermissionDate]='{3}'
                                            ,[COC_TOW_Person_BaseInfo].[XGR] = '{4}' 
                                            ,[COC_TOW_Person_BaseInfo].[XGSJ] = '{3}' 
                                            ,[COC_TOW_Person_BaseInfo].[PSN_BeforPersonName]=null 
                                        FROM [dbo].[COC_TOW_Person_BaseInfo] inner join dbo.Apply
                                        on [COC_TOW_Person_BaseInfo].[PSN_RegisterNO] = [Apply].[PSN_RegisterNO]
                                        inner join [dbo].[ApplyChange] 
                                        on Apply.ApplyID=[ApplyChange].ApplyID
                                        where [Apply].ReportCode='{0}' and [Apply].ExamineResult='通过'";
                                //  where [COC_TOW_Person_BaseInfo].[PSN_ServerID] in(select [PSN_ServerID] from [dbo].[Apply] where ReportCode='{0}' and ExamineResult='通过') and [Apply].ReportCode='{0}'";
                                CommonDAL.ExecSQL(tran, string.Format(sql
                                    , RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ReportCode"]
                                    , EnumManager.ApplyType.执业企业变更
                                    , "02"
                                    , DateTime.Now
                                    , UserName));

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
		                                    where [Apply].ReportCode='{0}' and [Apply].ExamineResult='通过'
	                                    ) a
	                                    inner join 
	                                    (
		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[COC_TOW_Person_File]
		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
                                            inner join [dbo].[Apply] 
                                            on [COC_TOW_Person_File].PSN_RegisterNO = [Apply].PSN_RegisterNo
		                                    where  [Apply].ReportCode='{0}' and [Apply].ExamineResult='通过'
	                                    ) b 
	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
                                    ) t
                                    on [COC_TOW_Person_File].FileID = t.FileID", RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ReportCode"]));

                                //删除要覆盖的附件
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
		                                    where [Apply].ReportCode='{0}' and [Apply].ExamineResult='通过'
	                                    ) a
	                                    inner join 
	                                    (
		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[COC_TOW_Person_File]
		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
                                            inner join [dbo].[Apply] 
                                            on [COC_TOW_Person_File].PSN_RegisterNO = [Apply].PSN_RegisterNo
		                                    where  [Apply].ReportCode='{0}' and [Apply].ExamineResult='通过'
	                                    ) b 
	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
                                    ) t
                                    on [COC_TOW_Person_File].FileID = t.FileID
                                    )", RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ReportCode"]));

                                //将申请单附件写入证书附件库
                                CommonDAL.ExecSQL(tran, string.Format(@"
                                    INSERT INTO [dbo].[COC_TOW_Person_File]([FileID],[PSN_RegisterNO],[IsHistory])
                                    select [ApplyFile].FileID,[Apply].PSN_RegisterNo,0 
                                    from [dbo].[ApplyFile]
                                    inner join [dbo].[Apply] 
                                    on [ApplyFile].ApplyID = [Apply].ApplyID 
                                    where [Apply].ReportCode='{0}' and [Apply].ExamineResult='通过'", RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ReportCode"]));


                                //                                //给消息表发送企业消息通知
                                //                                sql = @"INSERT INTO [dbo].[ApplyNews]([ID],[ApplyID],[PSN_Name],[PSN_CertificateNO] ,[PSN_RegisterNo] ,[ApplyType],[SFCK],[ENT_OrganizationsCode],[ENT_City])
                                //                                        SELECT NEWID(),[ApplyID],[PSN_Name],[PSN_CertificateNO],[PSN_RegisterNo],[ApplyType],0,[ENT_OrganizationsCode],[ENT_City]
                                //                                        FROM APPLY WHERE ReportCode='{0}' and ExamineResult='通过'";

                                //                                CommonDAL.ExecSQL(tran, string.Format(sql, RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ReportCode"]));

                                #region 同步更新从业人员B、C本(由企业资质)

                                //记录一条审批通过的申请单，备注关联说明二建申请

                                sql = @"
                                    INSERT INTO [dbo].[CERTIFICATECHANGE]
                                    ([CERTIFICATEID],[CHANGETYPE],[WORKERNAME],[SEX],[BIRTHDAY],[CONFERDATE],[VALIDSTARTDATE],[VALIDENDDATE]
                                    ,[CONFERUNIT],[DEALWAY],[OLDUNITADVISE],[NEWUNITADVISE],[OLDCONFERUNITADVISE],[NEWCONFERUNITADVISE]
                                    ,[APPLYDATE],[APPLYMAN],[APPLYCODE],[GETDATE],[GETRESULT],[GETMAN],[GETCODE],[CHECKDATE]
                                    ,[CHECKRESULT],[CHECKMAN],[CHECKCODE],[CONFRIMDATE],[CONFRIMRESULT],[CONFRIMMAN],[CONFRIMCODE],[NOTICEDATE]
                                    ,[NOTICERESULT],[NOTICEMAN],[NOTICECODE],[STATUS],[CREATEPERSONID],[CREATETIME],[MODIFYPERSONID],[MODIFYTIME]
                                    ,[UNITNAME],[NEWUNITNAME],[UNITCODE],[NEWUNITCODE],[WORKERCERTIFICATECODE],[LINKWAY],[NEWWORKERCERTIFICATECODE],[NEWWORKERNAME]
                                    ,[NEWSEX],[NEWBIRTHDAY],[SHEBAOCHECK],[IFUPDATEPHOTO],[PATCHSHEBAOCHECK],[OldUnitCheckTime],[NewUnitCheckTime],[ChangeRemark])
                                    select 
                                    [CERTIFICATE].CERTIFICATEID
                                    ,'京内变更' [CHANGETYPE]
                                    ,[CERTIFICATE].WORKERNAME
                                    ,[CERTIFICATE].[SEX]
                                    ,[CERTIFICATE].[BIRTHDAY]
                                    ,[CERTIFICATE].[CONFERDATE]
                                    ,[CERTIFICATE].[VALIDSTARTDATE]
                                    ,[CERTIFICATE].[VALIDENDDATE]
                                    ,[CERTIFICATE].[CONFERUNIT]
                                    ,'重新制作证书' [DEALWAY]
                                    ,'' [OLDUNITADVISE]
                                    ,'' [NEWUNITADVISE]
                                    ,'' [OLDCONFERUNITADVISE]
                                    ,'' [NEWCONFERUNITADVISE]
                                    ,getdate() [APPLYDATE]
                                    ,[CERTIFICATE].WORKERNAME [APPLYMAN]
                                    ,'BGSQ' + [Apply].ApplyCode [APPLYCODE]
                                    ,dateadd(s,1,getdate()) [GETDATE]
                                    ,'通过' [GETRESULT]
                                    ,'系统' [GETMAN]
                                    ,'BGSL' + [Apply].ApplyCode  [GETCODE]
                                    ,dateadd(s,2,getdate()) [CHECKDATE]
                                    ,'通过' [CHECKRESULT]
                                    ,'系统' [CHECKMAN]
                                    ,'BGSH' + [Apply].ApplyCode  [CHECKCODE]
                                    ,dateadd(s,3,getdate()) [CONFRIMDATE]
                                    ,'通过' [CONFRIMRESULT]
                                    ,'系统' [CONFRIMMAN]
                                    ,'BGJD' + [Apply].ApplyCode  [CONFRIMCODE]
                                    ,dateadd(s,4,getdate()) [NOTICEDATE]
                                    ,'通过' [NOTICERESULT]
                                    ,'系统' [NOTICEMAN]
                                    ,'BGGZ' + [Apply].ApplyCode  [NOTICECODE]
                                    ,'已告知' [STATUS]
                                    ,-1 [CREATEPERSONID]
                                    ,[Apply].[NoticeDate] [CREATETIME]
                                    ,-1 [MODIFYPERSONID]
                                    ,[Apply].[NoticeDate] [MODIFYTIME]
                                    ,[ApplyChange].OldENT_Name [UNITNAME]
                                    ,[ApplyChange].ENT_Name [NEWUNITNAME]
                                    ,[Apply].[OldEnt_QYZJJGDM] [UNITCODE]
                                    ,[Apply].[ENT_OrganizationsCode] [NEWUNITCODE]
                                    ,[CERTIFICATE].[WORKERCERTIFICATECODE]
                                    ,[ApplyChange].PSN_MobilePhone [LINKWAY]
                                    ,[CERTIFICATE].[WORKERCERTIFICATECODE] [NEWWORKERCERTIFICATECODE]
                                    ,[CERTIFICATE].WORKERNAME [NEWWORKERNAME]
                                    ,[CERTIFICATE].[SEX] [NEWSEX]
                                    ,[CERTIFICATE].[BIRTHDAY] [NEWBIRTHDAY]
                                    ,null [SHEBAOCHECK]
                                    ,null [IFUPDATEPHOTO]
                                    ,null [PATCHSHEBAOCHECK]
                                    ,null [OldUnitCheckTime]
                                    ,null [NewUnitCheckTime]
                                    ,Convert(varchar(10),[Apply].[NoticeDate],120) + '日二建变更（申请编号：'+ [Apply].ApplyCode +'）审核通过，触发本证书同步变更到新单位。'
                                     from [dbo].[Apply] 
                                     inner join [dbo].[ApplyChange] on [Apply].ApplyID=[ApplyChange].ApplyID
                                     inner join [dbo].[CERTIFICATE] on [CERTIFICATE].POSTTYPEID=1 and [CERTIFICATE].POSTID <>147 AND [Apply].[PSN_CertificateNO]= [CERTIFICATE].WORKERCERTIFICATECODE
                                     left join (select distinct ZZJGDM from dbo.QY_BWDZZZS WHERE  qylb is not null and qylb <>'' and qylb <>'外地进京' and qymc is not null and qymc <>'') t on [Apply].ENT_OrganizationsCode = t.ZZJGDM
                                      where [Apply].ReportCode='{0}' and [Apply].ExamineResult='通过' and [Apply].[ApplyTypeSub]='执业企业变更'
                                    and [CERTIFICATE].VALIDENDDATE > dateadd(day,-1,getdate())  and [CERTIFICATE].[STATUS] <>'注销'  and [CERTIFICATE].[STATUS] <>'离京变更' and [CERTIFICATE].[STATUS] <>'待审批' and [CERTIFICATE].[STATUS] <>'进京待审批'
                                    and [Apply].[ENT_OrganizationsCode] <> [CERTIFICATE].[UNITCODE]
                                    and t.ZZJGDM is not null
                                    ";
                                CommonDAL.ExecSQL(tran, string.Format(sql, RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ReportCode"]));

                                //从业人员证书写入历史表
                                sql = @"INSERT INTO dbo.CertificateHistory(OPERATETYPE,CERTIFICATEID,EXAMPLANID,WORKERID,CERTIFICATETYPE,POSTTYPEID,POSTID,CERTIFICATECODE,WORKERNAME,SEX,BIRTHDAY,UNITNAME,CONFERDATE,VALIDSTARTDATE,VALIDENDDATE,CONFERUNIT,STATUS,CHECKMAN,CHECKADVISE,CHECKDATE,PRINTMAN,PRINTDATE,CREATEPERSONID,CREATETIME,MODIFYPERSONID,MODIFYTIME,WORKERCERTIFICATECODE,UNITCODE,AddItemName,PostTypeName,PostName  )
                                        SELECT '变更',[CERTIFICATE].CERTIFICATEID,[CERTIFICATE].EXAMPLANID,[CERTIFICATE].WORKERID,[CERTIFICATE].CERTIFICATETYPE,[CERTIFICATE].POSTTYPEID,[CERTIFICATE].POSTID,[CERTIFICATE].CERTIFICATECODE,[CERTIFICATE].WORKERNAME,[CERTIFICATE].SEX,[CERTIFICATE].BIRTHDAY,[CERTIFICATE].UNITNAME,[CERTIFICATE].CONFERDATE,[CERTIFICATE].VALIDSTARTDATE,[CERTIFICATE].VALIDENDDATE,[CERTIFICATE].CONFERUNIT,[CERTIFICATE].STATUS,[CERTIFICATE].CHECKMAN,[CERTIFICATE].CHECKADVISE,[CERTIFICATE].CHECKDATE,[CERTIFICATE].PRINTMAN,[CERTIFICATE].PRINTDATE,[CERTIFICATE].CREATEPERSONID,[CERTIFICATE].CREATETIME,[CERTIFICATE].MODIFYPERSONID,[CERTIFICATE].MODIFYTIME,[CERTIFICATE].WORKERCERTIFICATECODE,[CERTIFICATE].UNITCODE,[CERTIFICATE].AddItemName,[CERTIFICATE].PostTypeName,[CERTIFICATE].PostName
                                        FROM DBO.CERTIFICATE 
                                        inner join dbo.Apply on [CERTIFICATE].POSTTYPEID=1 and [CERTIFICATE].POSTID <>147 AND [CERTIFICATE].WORKERCERTIFICATECODE = [Apply].[PSN_CertificateNO]  
                                        left join (select distinct ZZJGDM from dbo.QY_BWDZZZS WHERE  qylb is not null and qylb <>'' and qylb <>'外地进京' and qymc is not null and qymc <>'') t on [Apply].ENT_OrganizationsCode = t.ZZJGDM                                      
                                        where [Apply].ReportCode='{0}' and [Apply].ExamineResult='通过' and [Apply].[ApplyTypeSub]='执业企业变更'
                                        and [CERTIFICATE].VALIDENDDATE > dateadd(day,-1,getdate())  and [CERTIFICATE].[STATUS] <>'注销'  and [CERTIFICATE].[STATUS] <>'离京变更' and [CERTIFICATE].[STATUS] <>'待审批' and [CERTIFICATE].[STATUS] <>'进京待审批'
                                        and [Apply].[ENT_OrganizationsCode] <> [CERTIFICATE].[UNITCODE]
                                        and t.ZZJGDM is not null";
                                CommonDAL.ExecSQL(tran, string.Format(sql, RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ReportCode"]));

                                // 级联更新B、C本证书到新单位
                                sql = @"UPDATE [dbo].[CERTIFICATE]
                                            SET [CERTIFICATE].[UNITNAME]=[Apply].ENT_Name,
		                                        [CERTIFICATE].[UNITCODE]=[Apply].ENT_OrganizationsCode,
		                                        [CERTIFICATE].[MODIFYTIME]=getdate(),
                                                [CERTIFICATE].CheckDate=[Apply].[NoticeDate],
                                                [CERTIFICATE].CheckMan='系统',
                                                [CERTIFICATE].CheckAdvise='二建变更触发级联变更',
                                                [CERTIFICATE].[Status]='京内变更'
                                        FROM [dbo].[CERTIFICATE] 
                                        inner join dbo.Apply on [CERTIFICATE].POSTTYPEID=1 and [CERTIFICATE].POSTID <>147 AND [CERTIFICATE].WORKERCERTIFICATECODE = [Apply].[PSN_CertificateNO]
                                        left join (select distinct ZZJGDM from dbo.QY_BWDZZZS WHERE  qylb is not null and qylb <>'' and qylb <>'外地进京' and qymc is not null and qymc <>'') t on [Apply].ENT_OrganizationsCode = t.ZZJGDM                                         
                                        where [Apply].ReportCode='{0}' and [Apply].ExamineResult='通过' and [Apply].[ApplyTypeSub]='执业企业变更'
                                        and [CERTIFICATE].VALIDENDDATE > dateadd(day,-1,getdate())  and [CERTIFICATE].[STATUS] <>'注销'  and [CERTIFICATE].[STATUS] <>'离京变更' and [CERTIFICATE].[STATUS] <>'待审批' and [CERTIFICATE].[STATUS] <>'进京待审批'
                                        and [Apply].[ENT_OrganizationsCode] <> [CERTIFICATE].[UNITCODE]
                                        and t.ZZJGDM is not null
                                        ";
                                CommonDAL.ExecSQL(tran, string.Format(sql, RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ReportCode"]));

                                #endregion 同步更新从业人员B、C本

                                tran.Commit();
                            }
                            catch (Exception ex)
                            {
                                tran.Rollback();
                                UIHelp.WriteErrorLog(Page, "上报失败", ex);
                                return;
                            }
                            BindRadGridHZSB();
                            UIHelp.layerAlert(Page, "上报成功！", 6, 2000);
                           
                            #endregion 执业企业变更

                            break;

                        case "企业信息变更"://办结

                            #region 企业信息变更

                            DBHelper db1 = new DBHelper();
                            DbTransaction tran1 = db1.BeginTransaction();
                            try
                            {
                                //更新申请单
                                ApplyDAL.PatchReportFinish(tran1
                                 , RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ReportCode"].ToString()
                                , Region
                                , RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ApplyType"].ToString()
                                , UserName
                                );

                                //正式表写入历史表
                                sql = @"INSERT INTO [dbo].[COC_TOW_Person_BaseInfo_His]
                                ([HisID],[PSN_ServerID],[ENT_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[BeginTime],[EndTime],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Telephone],[PSN_Email],[PSN_PMGrade],[PSN_PMCertificateNo],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_ChangeReason],[PSN_BeforENT_Name],[PSN_BeforENT_ServerID],[PSN_BeforPersonName],[PSN_InterprovincialChange],[PSN_ExpiryReasons],[PSN_ExpiryDate],[PSN_RenewalProfession],[PSN_AddProfession],[PSN_CancelPerson],[PSN_CancelReason],[PSN_ReReasons],[PSN_ReContent],[PSN_CheckCode],[ENT_Province_Code],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[GteDate])	
                                  SELECT newid(),c.[PSN_ServerID],c.[ENT_ServerID],c.[ENT_Name],c.[ENT_OrganizationsCode],c.[ENT_City],c.[BeginTime],c.[EndTime],c.[PSN_Name],c.[PSN_Sex],c.[PSN_BirthDate],c.[PSN_National],c.[PSN_CertificateType],c.[PSN_CertificateNO],c.[PSN_GraduationSchool],c.[PSN_Specialty],c.[PSN_GraduationTime],c.[PSN_Qualification],c.[PSN_Degree],c.[PSN_MobilePhone],c.[PSN_Telephone],c.[PSN_Email],c.[PSN_PMGrade],c.[PSN_PMCertificateNo],c.[PSN_RegisteType],c.[PSN_RegisterNO],c.[PSN_RegisterCertificateNo],c.[PSN_RegisteProfession],c.[PSN_CertificationDate],c.[PSN_CertificateValidity],c.[PSN_RegistePermissionDate],c.[PSN_ChangeReason],c.[PSN_BeforENT_Name],c.[PSN_BeforENT_ServerID],c.[PSN_BeforPersonName],c.[PSN_InterprovincialChange],c.[PSN_ExpiryReasons],c.[PSN_ExpiryDate],c.[PSN_RenewalProfession],c.[PSN_AddProfession],c.[PSN_CancelPerson],c.[PSN_CancelReason],c.[PSN_ReReasons],c.[PSN_ReContent],c.[PSN_CheckCode],c.[ENT_Province_Code],c.[PSN_Level],c.[ZGZSBH],c.[CJR],c.[CJSJ],c.[XGR],c.[XGSJ],c.[Valid],c.[Memo],getdate() 
                                  FROM [dbo].[COC_TOW_Person_BaseInfo] c inner join dbo.Apply a on c.[PSN_RegisterNO] = a.[PSN_RegisterNO]
                                   where a.ReportCode='{0}' and a.ExamineResult='通过' and c.[ENT_OrganizationsCode] = a.[ENT_OrganizationsCode]";

                                CommonDAL.ExecSQL(tran1, string.Format(sql, RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ReportCode"]));

                                //更新正式表
                                sql = @"UPDATE [dbo].[COC_TOW_Person_BaseInfo]
                                            SET [COC_TOW_Person_BaseInfo].[ENT_Name] = [ApplyChange].ENT_NameTo
                                            ,[COC_TOW_Person_BaseInfo].[ENT_City] = [ApplyChange].ToENT_City
                                            ,[COC_TOW_Person_BaseInfo].[PSN_BeforENT_Name] = (case when [ApplyChange].ENT_NameFrom = [ApplyChange].ENT_NameTo then null else [ApplyChange].ENT_NameFrom end)
                                            ,[COC_TOW_Person_BaseInfo].[PSN_ChangeReason]='{1}'
                                            ,[COC_TOW_Person_BaseInfo].[PSN_RegisteType]='{2}'
                                            ,[COC_TOW_Person_BaseInfo].[PSN_RegistePermissionDate]='{3}'
                                            ,[COC_TOW_Person_BaseInfo].[XGR] = '{4}' 
                                            ,[COC_TOW_Person_BaseInfo].[XGSJ] = '{3}' 
                                            ,[COC_TOW_Person_BaseInfo].[PSN_BeforPersonName]= null 
                                            ,[COC_TOW_Person_BaseInfo].[END_Addess]=  [ApplyChange].[ToEND_Addess]
                                        FROM [dbo].[COC_TOW_Person_BaseInfo] inner join dbo.Apply
                                        on [COC_TOW_Person_BaseInfo].[PSN_RegisterNO] = [Apply].[PSN_RegisterNO]
                                        inner join [dbo].[ApplyChange] 
                                        on Apply.ApplyID=[ApplyChange].ApplyID
                                        where [Apply].ReportCode='{0}' and [Apply].ExamineResult='通过' and [COC_TOW_Person_BaseInfo].[ENT_OrganizationsCode] = [Apply].[ENT_OrganizationsCode]";
                                //增加过滤条件 [COC_TOW_Person_BaseInfo].[ENT_OrganizationsCode] = [Apply].[ENT_OrganizationsCode] 申办途中变更了单位，不再更新企业名称。

                                CommonDAL.ExecSQL(tran1, string.Format(sql
                                    , RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ReportCode"]
                                    , EnumManager.ApplyType.企业信息变更
                                    , "02"
                                    , DateTime.Now
                                    , UserName));

                                //企业表的数据写入历史
                                sql = @"INSERT INTO [dbo].[Unit_His]([HisID],[UnitID],[BeginTime],[EndTime],[ENT_Name],[ENT_OrganizationsCode],[ENT_Economic_Nature],[ENT_Province],[ENT_Province_Code],[ENT_City],[END_Addess],[ENT_Corporate],[ENT_Correspondence],[ENT_Postcode],[ENT_Contact],[ENT_Telephone],[ENT_MobilePhone],[ENT_Type],[ENT_Sort],[ENT_Grade],[ENT_QualificationCertificateNo],[CreditCode],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[GteDate])  
                                        SELECT newid() ,[UnitID],[BeginTime],[EndTime],[ENT_Name],[ENT_OrganizationsCode],[ENT_Economic_Nature],[ENT_Province],[ENT_Province_Code],[ENT_City],[END_Addess],[ENT_Corporate],[ENT_Correspondence],[ENT_Postcode],[ENT_Contact],[ENT_Telephone],[ENT_MobilePhone],[ENT_Type],[ENT_Sort],[ENT_Grade],[ENT_QualificationCertificateNo],[CreditCode],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],getdate() 
                                        FROM [dbo].[Unit] where [ENT_OrganizationsCode] in(select distinct ENT_OrganizationsCode from [dbo].[Apply] where ReportCode='{0}' and ExamineResult='通过')";
                                CommonDAL.ExecSQL(tran1, string.Format(sql, RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ReportCode"]));

                                //修改企业表的数据
                                sql = @"UPDATE [dbo].[Unit]
                                       SET [Unit].[ENT_Name] = a.ENT_NameTo
                                          ,[Unit].[ENT_City] = a.ToENT_City
                                          ,[Unit].[END_Addess] = a.[ToEND_Addess]
                                          ,[Unit].[ENT_Correspondence] = a.[ToEND_Addess]
                                          ,[Unit].[XGR] = '{1}' 
                                          ,[Unit].[XGSJ] = getdate()   
	                                    from [dbo].[Unit] inner join 
	                                    (
	                                    select distinct Apply.[ENT_OrganizationsCode], [ApplyChange].ENT_NameTo,[ApplyChange].ToENT_City,[ApplyChange].[ToEND_Addess]
	                                    from dbo.Apply 
		                                    inner join [dbo].[ApplyChange] 
                                            on Apply.ApplyID=[ApplyChange].ApplyID
	                                        where Apply.ReportCode='{0}'  and ExamineResult='通过'
	                                    ) a on [Unit].[ENT_OrganizationsCode]=a.[ENT_OrganizationsCode] ";
                                CommonDAL.ExecSQL(tran1, string.Format(sql, RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ReportCode"], UserName));

                                //给消息表发送企业消息通知
                                sql = @"INSERT INTO [dbo].[ApplyNews]([ID],[ApplyID],[PSN_Name],[PSN_CertificateNO] ,[PSN_RegisterNo] ,[ApplyType],[SFCK],[ENT_OrganizationsCode],[ENT_City])
                                        SELECT NEWID(),[ApplyID],[PSN_Name],[PSN_CertificateNO],[PSN_RegisterNo],[ApplyType],0,[ENT_OrganizationsCode],[ENT_City]
                                        FROM APPLY WHERE ReportCode='{0}' and ExamineResult='通过'";

                                CommonDAL.ExecSQL(tran1, string.Format(sql, RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ReportCode"]));

                                tran1.Commit();
                            }
                            catch (Exception ex)
                            {
                                tran1.Rollback();
                                UIHelp.WriteErrorLog(Page, "上报失败", ex);
                                return;
                            }
                            BindRadGridHZSB();
                            UIHelp.layerAlert(Page, "上报成功！", 6, 2000);                       

                            #endregion 企业信息变更

                            break;
                        case "个人信息变更"://办结

                            #region 个人信息变更

                            DBHelper db2 = new DBHelper();
                            DbTransaction tran2 = db2.BeginTransaction();
                            try
                            {
                                //更新申请单
                                ApplyDAL.PatchReportFinish(tran2
                                 , RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ReportCode"].ToString()
                                , Region
                                , RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ApplyType"].ToString()
                                , UserName
                                );

                                //正式表写入历史表
                                sql = @"INSERT INTO [dbo].[COC_TOW_Person_BaseInfo_His]
                                ([HisID],[PSN_ServerID],[ENT_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[BeginTime],[EndTime],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Telephone],[PSN_Email],[PSN_PMGrade],[PSN_PMCertificateNo],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_ChangeReason],[PSN_BeforENT_Name],[PSN_BeforENT_ServerID],[PSN_BeforPersonName],[PSN_InterprovincialChange],[PSN_ExpiryReasons],[PSN_ExpiryDate],[PSN_RenewalProfession],[PSN_AddProfession],[PSN_CancelPerson],[PSN_CancelReason],[PSN_ReReasons],[PSN_ReContent],[PSN_CheckCode],[ENT_Province_Code],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[GteDate])	
                                  SELECT newid(),[PSN_ServerID],[ENT_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[BeginTime],[EndTime],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Telephone],[PSN_Email],[PSN_PMGrade],[PSN_PMCertificateNo],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_ChangeReason],[PSN_BeforENT_Name],[PSN_BeforENT_ServerID],[PSN_BeforPersonName],[PSN_InterprovincialChange],[PSN_ExpiryReasons],[PSN_ExpiryDate],[PSN_RenewalProfession],[PSN_AddProfession],[PSN_CancelPerson],[PSN_CancelReason],[PSN_ReReasons],[PSN_ReContent],[PSN_CheckCode],[ENT_Province_Code],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],getdate() 
                                  FROM [dbo].[COC_TOW_Person_BaseInfo] 
                                  where [PSN_ServerID] in(select [PSN_ServerID] from [dbo].[Apply] where ReportCode='{0}' and ExamineResult='通过')";

                                CommonDAL.ExecSQL(tran2, string.Format(sql, RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ReportCode"]));

                                //更新正式表
                                sql = @"UPDATE [dbo].[COC_TOW_Person_BaseInfo]
                                            SET  [COC_TOW_Person_BaseInfo].[PSN_RegistePermissionDate] = '{3}'
                                                ,[COC_TOW_Person_BaseInfo].[XGR]='{4}'
                                                ,[COC_TOW_Person_BaseInfo].[XGSJ]='{3}'
                                                ,[COC_TOW_Person_BaseInfo].[PSN_ChangeReason]='{1}'
                                                ,[COC_TOW_Person_BaseInfo].[PSN_RegisteType]='{2}'
                                        FROM [dbo].[COC_TOW_Person_BaseInfo] 
                                        inner join dbo.Apply on [COC_TOW_Person_BaseInfo].[PSN_RegisterNO] = [Apply].[PSN_RegisterNO]
                                        inner join [dbo].[ApplyChange] on Apply.ApplyID=[ApplyChange].ApplyID
                                        where [Apply].ReportCode='{0}' and [Apply].ExamineResult='通过'";
                               
                                CommonDAL.ExecSQL(tran2, string.Format(sql
                                    , RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ReportCode"]
                                    , EnumManager.ApplyType.个人信息变更
                                    , "02"
                                    , DateTime.Now
                                    , UserName));

                                //更新证书附件中需要被覆盖的附件为历史附件
                                CommonDAL.ExecSQL(tran2, string.Format(@"
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
		                                    where [Apply].ReportCode='{0}' and [Apply].ExamineResult='通过'
	                                    ) a
	                                    inner join 
	                                    (
		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[COC_TOW_Person_File]
		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
                                            inner join [dbo].[Apply] 
                                            on [COC_TOW_Person_File].PSN_RegisterNO = [Apply].PSN_RegisterNo
		                                    where  [Apply].ReportCode='{0}' and [Apply].ExamineResult='通过'
	                                    ) b 
	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
                                    ) t
                                    on [COC_TOW_Person_File].FileID = t.FileID", RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ReportCode"]));

                                //删除要覆盖的附件
                                CommonDAL.ExecSQL(tran2, string.Format(@"
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
		                                    where [Apply].ReportCode='{0}' and [Apply].ExamineResult='通过'
	                                    ) a
	                                    inner join 
	                                    (
		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
		                                    from [dbo].[FileInfo]
		                                    inner join [dbo].[COC_TOW_Person_File]
		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
                                            inner join [dbo].[Apply] 
                                            on [COC_TOW_Person_File].PSN_RegisterNO = [Apply].PSN_RegisterNo
		                                    where  [Apply].ReportCode='{0}' and [Apply].ExamineResult='通过'
	                                    ) b 
	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
                                    ) t
                                    on [COC_TOW_Person_File].FileID = t.FileID
                                    )", RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ReportCode"]));

                                //将申请单附件写入证书附件库
                                CommonDAL.ExecSQL(tran2, string.Format(@"
                                    INSERT INTO [dbo].[COC_TOW_Person_File]([FileID],[PSN_RegisterNO],[IsHistory])
                                    select [ApplyFile].FileID,[Apply].PSN_RegisterNo,0 
                                    from [dbo].[ApplyFile]
                                    inner join [dbo].[Apply] 
                                    on [ApplyFile].ApplyID = [Apply].ApplyID 
                                    where [Apply].ReportCode='{0}' and [Apply].ExamineResult='通过'", RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ReportCode"]));

                                tran2.Commit();
                            }
                            catch (Exception ex)
                            {
                                tran2.Rollback();
                                UIHelp.WriteErrorLog(Page, "上报失败", ex);
                                return;
                            }
                            BindRadGridHZSB();
                            UIHelp.layerAlert(Page, "上报成功！", 6, 2000);

                            #endregion 个人信息变更

                            break;
                        default://中间环节
                            try
                            {
                                ApplyDAL.PatchReport(RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ReportCode"].ToString()
                                , Region
                                , RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ApplyType"].ToString()
                                , UserName
                                );

                                BindRadGridHZSB();
                                UIHelp.layerAlert(Page, "上报成功！", 6, 2000);

                            }
                            catch (Exception ex)
                            {
                                UIHelp.WriteErrorLog(Page, "上报失败", ex);
                            }
                            break;
                    }
                    break;

                case "Cancelreport"://取消上报
                    try
                    {
                        ApplyDAL.CancelPatchReport(RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ReportCode"].ToString()
                        , Region, RadGridHZSB.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ApplyType"].ToString());
                        BindRadGridHZSB();
                        UIHelp.layerAlert(Page, "取消上报成功！", 6, 2000);

                    }
                    catch (Exception ex)
                    {
                        UIHelp.WriteErrorLog(Page, "取消上报失败", ex);
                    }
                    break;
            }
        }

        //获取表格勾选集合
        private string GetRadGridADDRYSelect()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i < RadGridHZSB.MasterTableView.Items.Count; i++)
            {
                CheckBox CheckBox1 = RadGridHZSB.Items[i].FindControl("CheckBox1") as CheckBox;
                if (CheckBox1.Checked)
                {
                    sb.Append(",'").Append(RadGridHZSB.MasterTableView.DataKeyValues[i]["ReportCode"].ToString()).Append("'");
                }
            }
            if (sb.Length > 0)
            {
                sb.Remove(0, 1);
            }
            return sb.ToString();
        }

        //EXCEL导出
        protected void ImageButton1_Click1(object sender, ImageClickEventArgs e)
        {
            //Checkd选中集合
            string selectReportCodes = GetRadGridADDRYSelect();
            if (selectReportCodes == "")
            {
                UIHelp.layerAlert(Page, "您尚未勾选任何人员！");
                return;
            }

            //excel的Caption
            string Caption = @"
<tr style=""height:30pt"">
    <td style=""font-weight:bold;text-align:center;"" class=""noborder"" colspan=""8"">二级建造师重新注册初审汇总表</td>
</tr>
<tr>
    <td style=""text-align:left;"" colspan=""4"" class=""noborder"">批次号：</td>
    <td style=""text-align:right;"" class=""noborder"" colspan=""4"">申报日期：</td>
</tr>";

            string Foot = @"
<tr style=""height:30pt"">
    <td style=""text-align:center;"" colspan=""4"" class=""noborder"">制表人（签字）：</td>
    <td style=""text-align:center;"" class=""noborder"" colspan=""2"">审核人（签字）：</td>
    <td style=""text-align:center;"" class=""noborder"" colspan=""2"">部门公章</td>
</tr>";

            //EXCEL列头
            string head = @"姓名\性别\证件类型\证件号码\申报类型\企业名称\企业组织机构代码\上报批次号";
            //数据表的数据列
            string column = @"PSN_Name\PSN_Sex\PSN_CertificateType\PSN_CertificateNO\ApplyType\ENT_Name\ENT_OrganizationsCode\ReportCode";
            //过滤条件
            string filterSql = string.Format(" and ENT_City like '{0}%' AND  ReportCode in ({1})", Region, selectReportCodes);

            if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/Excel/"))) System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Upload/Excel/"));
            string filePath = string.Format("~/Upload/Excel/Excel{0}{1}.xls", UserID, DateTime.Now.ToString("yyyyMMddHHmmss"));
            CommonDAL.OutputXls(HttpContext.Current.Server.MapPath(filePath)
                , "Apply"
                , filterSql, "ApplyID", head.ToString(), column.ToString(), Caption, Foot);
            string size = CommonDAL.GetFileSize(HttpContext.Current.Server.MapPath(filePath));
            spanOutput.InnerHtml = string.Format(@"<div style=""width: 80%; font-weight: bold; ""><a href=""{0}"">{1}</a><span  style=""padding-left:20px;"">（{2}）</span></div>"
                , filePath.Replace("~", "..")
                , "点击我下载"
                , size);
        }

        ////上传汇总扫描件
        //protected void ButtonUpload_Click(object sender, EventArgs e)
        //{

        //    if (RadUploadReportList.UploadedFiles.Count > 0)
        //    {
        //        if (RadUploadReportList.UploadedFiles[0].GetExtension().ToLower() != ".jpg")
        //        {
        //            UIHelp.Alert(Page, "扫描件请统一使用jpg格式图片");
        //            return;
        //        }
        //        if (RadUploadReportList.UploadedFiles[0].ContentLength > 512000)
        //        {
        //            UIHelp.Alert(Page, "图片大小不能超过500k，请使用工具进行转化！");
        //            return;
        //        }
        //    }

        //    //个人照片存放路径(按证件号码后3位)
        //    if (!Directory.Exists(Page.Server.MapPath(string.Format("~/UpLoad/ReportImg/{0}/",HiddenFieldReportCode.Value))))
        //    {
        //        System.IO.Directory.CreateDirectory(Page.Server.MapPath(string.Format("~/UpLoad/ReportImg/{0}/", HiddenFieldReportCode.Value)));
        //    }

        //    if (RadUploadReportList.UploadedFiles.Count > 0)//上传照片
        //    {

        //        //foreach (UploadedFile validFile in RadUploadReportList.UploadedFiles)
        //        //{                   
        //        //    validFile.SaveAs(Path.Combine(workerPhotoFolder, HiddenFieldReportCode.Value + ".jpg"), true);
        //        //    ImgCode.Src = UIHelp.ShowFaceImage(UIHelp.ShowFaceImage(RadTextBoxPSN_CertificateNO.Text));//绑定照片;
        //        //    break;
        //        //}
        //    }



        //}
    }
}