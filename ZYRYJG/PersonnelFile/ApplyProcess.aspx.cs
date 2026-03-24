using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Model;
using DataAccess;
using System.Data;
using Utility;

namespace ZYRYJG.PersonnelFile
{
    public partial class ApplyProcess : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //WorkerOB _WorkerOB = WorkerDAL.GetObject(PersonID);


                ////查询有效证书列表
                ////ObjectDataSource1.SelectParameters.Clear();
                ////QueryParamOB q = new QueryParamOB();
                //string IDCard15 = "";
                //string IDCard18 = "";
                //if (_WorkerOB == null)
                //{
                //    IDCard15 = IDCard18 = "null";
                //}
                //else if (_WorkerOB.CertificateCode.Length == 15)
                //{
                //    IDCard15 = _WorkerOB.CertificateCode;
                //    IDCard18 = Utility.Check.ConvertoIDCard15To18(IDCard15);
                //}
                //else if (_WorkerOB.CertificateCode.Length == 18)
                //{
                //    IDCard15 = _WorkerOB.CertificateCode.Remove(17, 1).Remove(6, 2);
                //    IDCard18 = _WorkerOB.CertificateCode;
                //}
                //else
                //{
                //    IDCard15 = _WorkerOB.CertificateCode;
                //    IDCard18 = _WorkerOB.CertificateCode;
                //}
                ////最近业务进度
                //GetApplyProcess(IDCard18);

                GetApplyProcess(WorkerCertificateCode);

            }
        }




        //获取最新50条业务办理进度
        private void GetApplyProcess(string WorkerCertificateCode)
        {
            ////规则1：当前小于准考证下载前一天，不允许看到建委审核考试报名结果记录信息
//            string sql = @"select top 50 t.*,row_number() over(order by ApplyDate desc) as RowNum from 
//(
//     SELECT  cast(s.[EXAMSIGNUPID] as varchar(64)) DataID
//            ,s.[RESULTCERTIFICATECODE] as CERTIFICATECODE
//            ,s.[SIGNUPDATE] ApplyDate  
//            ,'考试报名' ItemType   
//            ,'exam' ItemCode                                
//	        ,s.[FIRSTTRIALTIME] UnitCheckDate
//	        , HireUnitAdvise as UnitRsult 		
//            ,case when dateadd(day,-1,e.ExamCardSendStartDate) < getdate() then s.[CHECKDATE] else null end as AcceptDate
//	        ,case when dateadd(day,-1,e.ExamCardSendStartDate) < getdate() then '审核' + s.[CHECKRESULT] else '' end as AcceptResult
//			,case when dateadd(day,-1,e.ExamCardSendStartDate) < getdate() then [PAYCONFIRMDATE] else null end as CHECKDATE
//			,case when dateadd(day,-1,e.ExamCardSendStartDate) < getdate() then [PAYCONFIRMRULT] else '' end as CHECKRESULT      
//            ,r.[CREATETIME] as CONFRIMDATE
//	        ,case when r.[CREATETIME] is not null then '发放准考证' else '' end  CONFRIMRESULT
//	        ,r.[MODIFYTIME] as NOTICEDATE
//	        ,case when r.[STATUS] ='成绩已公告' then '考试结果：' + r.[EXAMRESULT]
//			when r.[STATUS] is null  then '' else '考试结果：' + r.[STATUS] end as NOTICERESULT
//            ,e.PostName
//    FROM [dbo].[EXAMSIGNUP] s
//    inner join examplan e on s.examplanid = e.examplanid
//	left join [dbo].[EXAMRESULT] r on s.EXAMSIGNUPID = r.EXAMSIGNUP_ID
//	where s.CERTIFICATECODE='{0}'
//    union all
//    SELECT cast(b.[CERTIFICATECHANGEID]  as varchar(64)) 
//            ,c.[CERTIFICATECODE]
//            ,b.[APPLYDATE]    
//	        ,b.[CHANGETYPE] 
//            ,'change' ItemCode  
//			,case when b.[NewUnitCheckTime] is not null then b.[NewUnitCheckTime] else b.[OldUnitCheckTime] end
//			,case when b.[NewUnitCheckTime] is not null then b.[NewUnitAdvise] else b.[OldUnitAdvise] end                                 
//            ,b.[GETDATE]
//            ,'受理' + b.[GETRESULT]    
//            ,b.[CHECKDATE]
//            ,'审查' + b.[CHECKRESULT]     
//            ,b.[CONFRIMDATE]
//            ,'决定' + b.[CONFRIMRESULT]
//            ,b.[NOTICEDATE]
//            ,'告知' +b.[NOTICERESULT]   
//            ,c.PostName
//    FROM [dbo].[CERTIFICATECHANGE] b
//    inner join [dbo].[CERTIFICATE] c on c.WORKERCERTIFICATECODE='{0}' and b.CERTIFICATEID = c.CERTIFICATEID   and b.CREATEPERSONID>0     
//    union all
//    SELECT cast([APPLYID]   as varchar(64))
//            ,[CERTIFICATECODE]
//            ,[APPLYDATE] 
//	        ,'证书进京'   
//            ,'JinJing' ItemCode    
//			,[NewUnitCheckTime]
//			,[NewUnitAdvise]
//            ,[ACCEPTDATE]
//            ,[GETRESULT]     
//            ,[CHECKDATE]
//            ,[CHECKRESULT]    
//            ,[CONFRIMDATE]
//			,[CONFRIMRESULT]
//	        ,[CONFRIMDATE]
//			,NEWCERTIFICATECODE
//            ,PostName	
//    FROM [dbo].[VIEW_CERTIFICATE_ENTER] where  WORKERCERTIFICATECODE='{0}'                         
//    union all
//    SELECT cast(x.[CERTIFICATECONTINUEID]   as varchar(64))
//            ,c.[CERTIFICATECODE]
//            ,x.[APPLYDATE] 
//	        ,'证书续期'   
//            ,'continue' ItemCode    
//			,x.[NewUnitCheckTime]
//			,x.[NewUnitAdvise]
//            ,case when x.[STATUS]='退回修改' or x.[ReportMan] is not null then x.[GETDATE] else null end 
//            ,case when x.[STATUS]='退回修改' or x.[ReportMan] is not null  then x.[GETRESULT] else null end     
//            ,x.[CHECKDATE]
//            ,x.[CHECKRESULT]    
//            ,x.[CONFIRMDATE]
//            ,x.[CONFIRMRESULT]
//	        ,x.[CONFIRMDATE]
//            ,x.[CONFIRMRESULT]
//            ,c.PostName
//    FROM [dbo].[CERTIFICATECONTINUE] x
//    inner join [dbo].[CERTIFICATE] c on c.WORKERCERTIFICATECODE='{0}' and x.CERTIFICATEID = c.CERTIFICATEID
//	union all
//	SELECT 
//		[ApplyID]
//        ,[PSN_RegisterNo]
//		,[ApplyTime]
//		,case when [ApplyTypeSub] is null then [ApplyType] else [ApplyTypeSub] end
//        ,'apply' ItemCode  
//		,[OldUnitCheckTime]
//		,[OldUnitCheckResult]
//		,[GetDateTime]
//		,[GetResult]
//		,[ExamineDatetime]
//		,[ExamineResult]
//		,case when [ApplyTypeSub]='执业企业变更' then [ExamineDatetime] else case when ((PublicDate is not null and ApplyType in ('初始注册','重新注册','增项注册','延期注册')) or ([ConfirmDate] is not null and ApplyType ='注销') or (ApplyType in ('变更注册','遗失补办'))) then [CheckDate] else null end end
//		,case when [ApplyTypeSub]='执业企业变更' then [ExamineResult] else case when ((PublicDate is not null and ApplyType in ('初始注册','重新注册','增项注册','延期注册')) or ([ConfirmDate] is not null and ApplyType ='注销') or (ApplyType in ('变更注册','遗失补办'))) then [CheckResult] else null end end
//		,case when [ApplyTypeSub]='执业企业变更' then [ExamineDatetime] else case when ((PublicDate is not null and ApplyType in ('初始注册','重新注册','增项注册','延期注册')) or (ApplyType in ('注销','变更注册','遗失补办'))) then [ConfirmDate] else null end end
//		,case when [ApplyTypeSub]='执业企业变更' then [ExamineResult] else case when ((PublicDate is not null and ApplyType in ('初始注册','重新注册','增项注册','延期注册')) or (ApplyType in ('注销','变更注册','遗失补办'))) then [ConfirmResult] else null end end
//		,'二级注册建造师'
//  FROM [dbo].[Apply] 
//  where [PSN_CertificateNO]='{0}'  and 
//  ([ApplyTypeSub] <>'企业信息变更' or [ApplyTypeSub] is null)
//) t";


            //规则2：正常即可显示考试报名审核记录
            string sql = @"select top 50 t.*,row_number() over(order by ApplyDate desc) as RowNum from 
(
     SELECT  cast(s.[EXAMSIGNUPID] as varchar(64)) DataID
            ,s.[RESULTCERTIFICATECODE] as CERTIFICATECODE
            ,s.[SIGNUPDATE] ApplyDate  
            ,'考试报名' ItemType   
            ,'exam' ItemCode                                
	        ,s.[FIRSTTRIALTIME] UnitCheckDate
	        , HireUnitAdvise as UnitRsult 		
            ,s.[CHECKDATE] as AcceptDate
	        ,'审核' + s.[CHECKRESULT]  as AcceptResult
			,[PAYCONFIRMDATE] as CHECKDATE
			,[PAYCONFIRMRULT] as CHECKRESULT     
            ,r.[CREATETIME] as CONFRIMDATE
	        ,case when r.[CREATETIME] is not null then '发放准考证' else '' end  CONFRIMRESULT
	        ,r.[MODIFYTIME] as NOTICEDATE
	        ,case when r.[STATUS] ='成绩已公告' then '考试结果：' + r.[EXAMRESULT]
			when r.[STATUS] is null  then '' else '考试结果：' + r.[STATUS] end as NOTICERESULT
            ,e.PostName
    FROM [dbo].[EXAMSIGNUP] s
    inner join examplan e on s.examplanid = e.examplanid
	left join [dbo].[EXAMRESULT] r on s.EXAMSIGNUPID = r.EXAMSIGNUP_ID
	where s.CERTIFICATECODE='{0}'
    union all
    SELECT cast(b.[CERTIFICATECHANGEID]  as varchar(64)) 
            ,c.[CERTIFICATECODE]
            ,b.[APPLYDATE]    
	        ,b.[CHANGETYPE] 
            ,'change' ItemCode  
			,case when b.[NewUnitCheckTime] is not null then b.[NewUnitCheckTime] else b.[OldUnitCheckTime] end
			,case when b.[NewUnitCheckTime] is not null then b.[NewUnitAdvise] else b.[OldUnitAdvise] end                                 
            ,b.[GETDATE]
            ,'受理' + b.[GETRESULT]    
            ,b.[CHECKDATE]
            ,'审查' + b.[CHECKRESULT]     
            ,b.[CONFRIMDATE]
            ,'决定' + b.[CONFRIMRESULT]
            ,b.[NOTICEDATE]
            ,'告知' +b.[NOTICERESULT]   
            ,c.PostName
    FROM [dbo].[CERTIFICATECHANGE] b
    inner join [dbo].[CERTIFICATE] c on c.WORKERCERTIFICATECODE='{0}' and b.CERTIFICATEID = c.CERTIFICATEID   and b.CREATEPERSONID>0     
    union all
    SELECT cast([APPLYID]   as varchar(64))
            ,[CERTIFICATECODE]
            ,[APPLYDATE] 
	        ,'证书进京'   
            ,'JinJing' ItemCode    
			,[NewUnitCheckTime]
			,[NewUnitAdvise]
            ,[ACCEPTDATE]
            ,[GETRESULT]     
            ,[CHECKDATE]
            ,[CHECKRESULT]    
            ,[CONFRIMDATE]
			,[CONFRIMRESULT]
	        ,[CONFRIMDATE]
			,NEWCERTIFICATECODE
            ,PostName	
    FROM [dbo].[VIEW_CERTIFICATE_ENTER] where  WORKERCERTIFICATECODE='{0}'                         
    union all
    SELECT cast(x.[CERTIFICATECONTINUEID]   as varchar(64))
            ,c.[CERTIFICATECODE]
            ,x.[APPLYDATE] 
	        ,'证书续期'   
            ,'continue' ItemCode    
			,x.[NewUnitCheckTime]
			,x.[NewUnitAdvise]
            ,case when x.[STATUS]='退回修改' or x.[ReportMan] is not null then x.[GETDATE] else null end 
            ,case when x.[STATUS]='退回修改' or x.[ReportMan] is not null  then x.[GETRESULT] else null end     
            ,x.[CHECKDATE]
            ,x.[CHECKRESULT]    
            ,x.[CONFIRMDATE]
            ,x.[CONFIRMRESULT]
	        ,x.[CONFIRMDATE]
            ,x.[CONFIRMRESULT]
            ,c.PostName
    FROM [dbo].[CERTIFICATECONTINUE] x
    inner join [dbo].[CERTIFICATE] c on c.WORKERCERTIFICATECODE='{0}' and x.CERTIFICATEID = c.CERTIFICATEID
	union all
	SELECT 
		[ApplyID]
        ,[PSN_RegisterNo]
		,[ApplyTime]
		,case when [ApplyTypeSub] is null then [ApplyType] else [ApplyTypeSub] end
        ,'apply' ItemCode  
		,[OldUnitCheckTime]
		,[OldUnitCheckResult]
		,[GetDateTime]
		,[GetResult]
		,[ExamineDatetime]
		,[ExamineResult]
		,case when [ApplyTypeSub]='执业企业变更' then [ExamineDatetime] else case when ((ConfirmDate is not null and ApplyType in ('初始注册','重新注册','增项注册','延期注册')) or ([ConfirmDate] is not null and ApplyType ='注销') or (ApplyType in ('变更注册','遗失补办'))) then [CheckDate] else null end end
		,case when [ApplyTypeSub]='执业企业变更' then [ExamineResult] else case when ((ConfirmDate is not null and ApplyType in ('初始注册','重新注册','增项注册','延期注册')) or ([ConfirmDate] is not null and ApplyType ='注销') or (ApplyType in ('变更注册','遗失补办'))) then [CheckResult] else null end end
		,case when [ApplyTypeSub]='执业企业变更' then [ExamineDatetime] else case when ((ConfirmDate is not null and ApplyType in ('初始注册','重新注册','增项注册','延期注册')) or (ApplyType in ('注销','变更注册','遗失补办'))) then [ConfirmDate] else null end end
		,case when [ApplyTypeSub]='执业企业变更' then [ExamineResult] else case when ((ConfirmDate is not null and ApplyType in ('初始注册','重新注册','增项注册','延期注册')) or (ApplyType in ('注销','变更注册','遗失补办'))) then [ConfirmResult] else null end end
		,'二级注册建造师'
  FROM [dbo].[Apply] 
  where [PSN_CertificateNO]='{0}'  and 
  ([ApplyTypeSub] <>'企业信息变更' or [ApplyTypeSub] is null)
    union all
	SELECT 
		[ApplyID]
        ,[PSN_RegisterNo]
		,[ApplyTime]
		,case when [ApplyTypeSub] is null then [ApplyType] else [ApplyTypeSub] end
        ,'applyZJS' ItemCode  
		,[OldUnitCheckTime]
		,[OldUnitCheckResult]
		,[GetDateTime]
		,[GetResult]
		,[ExamineDatetime]
		,[ExamineResult]
		,case when [ApplyTypeSub]='执业企业变更' then [ExamineDatetime] else case when ((ConfirmDate is not null and ApplyType in ('初始注册','延续注册')) or ([ConfirmDate] is not null and ApplyType ='注销') or (ApplyType = '变更注册')) then [CheckDate] else null end end
		,case when [ApplyTypeSub]='执业企业变更' then [ExamineResult] else case when ((ConfirmDate is not null and ApplyType in ('初始注册','延续注册')) or ([ConfirmDate] is not null and ApplyType ='注销') or (ApplyType = '变更注册')) then [CheckResult] else null end end
		,case when [ApplyTypeSub]='执业企业变更' then [ExamineDatetime] else case when ((ConfirmDate is not null and ApplyType in ('初始注册','延续注册')) or (ApplyType in ('注销','变更注册'))) then [ConfirmDate] else null end end
		,case when [ApplyTypeSub]='执业企业变更' then [ExamineResult] else case when ((ConfirmDate is not null and ApplyType in ('初始注册','延续注册')) or (ApplyType in ('注销','变更注册'))) then [ConfirmResult] else null end end
		,'二级注册造价工程师'
  FROM [dbo].[zjs_Apply] 
  where [PSN_CertificateNO]='{0}'  and 
  ([ApplyTypeSub] <>'企业信息变更' or [ApplyTypeSub] is null)
) t";

            DataTable dt = CommonDAL.GetDataTable(string.Format(sql, WorkerCertificateCode));
            RadGridProcess.DataSource = dt;
            RadGridProcess.DataBind();
        }
    }
}
