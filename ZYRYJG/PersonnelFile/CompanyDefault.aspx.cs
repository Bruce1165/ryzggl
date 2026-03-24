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
    public partial class CompanyDefault : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                
            if (!IsPostBack)
            {
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
            try
            {
                //UnitInfoOB _UnitInfoOB = UnitInfoDAL.GetObject(PersonID);

                string sql = "";
                DataTable dt = null;
                QueryParamOB q = new QueryParamOB();
                switch(RadioButtonListItemType.SelectedValue)
                {
                    case "考试报名":
                        sql = @"select t.*,row_number() over(order by ApplyDate desc) as rn from 
                            (
                                SELECT  s.[EXAMSIGNUPID] DataID
                                        ,s.[SIGNUPDATE] ApplyDate  
                                        ,'考试报名' ItemType                                    
	                                    ,s.[FIRSTTRIALTIME] FirstCheckDate
	                                    , case when s.[FIRSTTRIALTIME] is not null then '初审通过' else '' end FirstCheckResult 
                                        ,s.[CHECKDATE]
	                                    ,'审核' + s.[CHECKRESULT]  as CHECKRESULT
                                        ,r.[CREATETIME] as CONFRIMDATE
	                                    ,case when r.[CREATETIME] is not null then '发放准考证' else '' end  CONFRIMRESULT
	                                    ,r.[MODIFYTIME] as NOTICEDATE
	                                    ,case when r.[STATUS] ='成绩已公告' then '考试结果：' + r.[EXAMRESULT]
										when r.[STATUS] is null  then ''
										else '考试结果：' + r.[STATUS] 
										 end as NOTICERESULT
                                        ,e.PostName
                                        ,s.WorkerName
                                      ,s.RESULTCERTIFICATECODE as CertificateCode
                                FROM [dbo].[EXAMSIGNUP] s
                                inner join examplan e on s.examplanid = e.examplanid
		                        left join [dbo].[EXAMRESULT] r on s.EXAMSIGNUPID = r.EXAMSIGNUP_ID
	                            where s.UnitCode='{0}' {1}                               
                            ) t
                            ";
                        //姓名
                        if (RadTextBoxWorkerName.Text.Trim() !="")
                        {
                            q.Add(string.Format("s.WorkerName ='{0}'", RadTextBoxWorkerName.Text.Trim()));
                        }
                        //证书编号
                        if (RadTextBoxCertificateCode.Text.Trim() != "")
                        {
                            q.Add(string.Format("s.RESULTCERTIFICATECODE ='{0}'", RadTextBoxCertificateCode.Text.Trim()));
                        }
                        //申请日期
                        q.Add(string.Format("s.[SIGNUPDATE] >'{0}'",DateTime.Now.AddMonths(- Convert.ToInt32(RadioButtonListApplyDate.SelectedValue))));

                        dt = CommonDAL.GetDataTable(string.Format(sql, ZZJGDM,q.ToWhereString()));
                        break;
                    case "证书变更":
                        sql = @"select t.*,row_number() over(order by ApplyDate desc) as rn from 
                            (                                
                                SELECT b.[CERTIFICATECHANGEID]  DataID
                                      ,b.[APPLYDATE]    
	                                  ,b.[CHANGETYPE]  ItemType                                 
                                      ,b.[GETDATE] FirstCheckDate
                                      ,'受理' + b.[GETRESULT] FirstCheckResult   
                                      ,b.[CHECKDATE]
                                      ,'审查' + b.[CHECKRESULT] CHECKRESULT    
                                      ,b.[CONFRIMDATE]
                                      ,'决定' + b.[CONFRIMRESULT] CONFRIMRESULT
                                      ,b.[NOTICEDATE]
                                      ,'告知' +b.[NOTICERESULT] NOTICERESULT  
                                      ,c.PostName
                                      ,c.WorkerName
                                      ,c.CertificateCode
                                FROM [dbo].[CERTIFICATECHANGE] b
                                 inner join [dbo].[CERTIFICATE] c 
                                on (b.UNITCODE='{0}' or b.NEWUNITCODE='{0}') and b.CERTIFICATEID = c.CERTIFICATEID 
                                where 1=1 {1}                              
                            ) t
                            ";
                        //姓名
                        if (RadTextBoxWorkerName.Text.Trim() != "")
                        {
                            q.Add(string.Format("c.WorkerName ='{0}'", RadTextBoxWorkerName.Text.Trim()));
                        }
                        //证书编号
                        if (RadTextBoxCertificateCode.Text.Trim() != "")
                        {
                            q.Add(string.Format("c.CERTIFICATECODE ='{0}'", RadTextBoxCertificateCode.Text.Trim()));
                        }
                        //申请日期
                        q.Add(string.Format("b.[APPLYDATE] >'{0}'", DateTime.Now.AddMonths(-Convert.ToInt32(RadioButtonListApplyDate.SelectedValue))));

                        dt = CommonDAL.GetDataTable(string.Format(sql, ZZJGDM, q.ToWhereString()));
                        break;
                    case "证书续期":
                        sql = @"select t.*,row_number() over(order by ApplyDate desc) as rn from 
                            (                                
                                SELECT x.[CERTIFICATECONTINUEID]  DataID
                                      ,x.[APPLYDATE] 
	                                  ,'证书续期'  ItemType   
                                      ,x.[GETDATE] FirstCheckDate
                                      ,x.[GETRESULT] FirstCheckResult    
                                      ,x.[CHECKDATE]
                                      ,x.[CHECKRESULT]    
                                      ,x.[CONFIRMDATE] CONFRIMDATE
                                      ,x.[CONFIRMRESULT] CONFRIMRESULT
	                                  ,x.[CONFIRMDATE] NOTICEDATE
                                      ,replace(x.[CONFIRMRESULT],'决定','告知') NOTICERESULT
                                      ,c.PostName
                                      ,c.WorkerName
                                      ,c.CertificateCode
                                  FROM [dbo].[CERTIFICATECONTINUE] x
                                inner join [dbo].[CERTIFICATE] c 
                                on (x.UNITCODE='{0}' or x.NEWUNITCODE='{0}') and x.CERTIFICATEID = c.CERTIFICATEID
                                where 1=1  {1}
                            ) t
                            ";
                        //姓名
                        if (RadTextBoxWorkerName.Text.Trim() != "")
                        {
                            q.Add(string.Format("c.WorkerName ='{0}'", RadTextBoxWorkerName.Text.Trim()));
                        }
                        //证书编号
                        if (RadTextBoxCertificateCode.Text.Trim() != "")
                        {
                            q.Add(string.Format("c.CERTIFICATECODE ='{0}'", RadTextBoxCertificateCode.Text.Trim()));
                        }
                        //申请日期
                        q.Add(string.Format("x.[APPLYDATE] >'{0}'", DateTime.Now.AddMonths(-Convert.ToInt32(RadioButtonListApplyDate.SelectedValue))));

                        dt = CommonDAL.GetDataTable(string.Format(sql, ZZJGDM, q.ToWhereString()));
                        break;
                    default://全部
                        sql = @"select t.*,row_number() over(order by ApplyDate desc) as rn from 
                            (
                                SELECT  s.[EXAMSIGNUPID] DataID
                                        ,s.[SIGNUPDATE] ApplyDate  
                                        ,'考试报名' ItemType                                    
	                                    ,s.[FIRSTTRIALTIME] FirstCheckDate
	                                    , case when s.[FIRSTTRIALTIME] is not null then '初审通过' else '' end FirstCheckResult 
                                        ,s.[CHECKDATE]
	                                    ,'审核' + s.[CHECKRESULT]  as CHECKRESULT
                                        ,r.[CREATETIME] as CONFRIMDATE
	                                    ,case when r.[CREATETIME] is not null then '发放准考证' else '' end  CONFRIMRESULT
	                                    ,r.[MODIFYTIME] as NOTICEDATE
	                                    ,case when r.[STATUS] ='成绩已公告' then '考试结果：' + r.[EXAMRESULT]
										when r.[STATUS] is null  then ''
										else '考试结果：' + r.[STATUS] 
										 end as NOTICERESULT
                                        ,e.PostName
                                        ,s.WorkerName
                                      ,s.RESULTCERTIFICATECODE as CertificateCode
                                FROM [dbo].[EXAMSIGNUP] s
                                inner join examplan e on s.examplanid = e.examplanid
		                        left join [dbo].[EXAMRESULT] r on s.EXAMSIGNUPID = r.EXAMSIGNUP_ID
	                            where s.UnitCode='{0}' {1}
                                union all
                                SELECT b.[CERTIFICATECHANGEID]  
                                      ,b.[APPLYDATE]    
	                                  ,b.[CHANGETYPE]                                   
                                      ,b.[GETDATE]
                                      ,'受理' + b.[GETRESULT]    
                                      ,b.[CHECKDATE]
                                      ,'审查' + b.[CHECKRESULT]     
                                      ,b.[CONFRIMDATE]
                                      ,'决定' + b.[CONFRIMRESULT]
                                      ,b.[NOTICEDATE]
                                      ,'告知' +b.[NOTICERESULT]   
                                      ,c.PostName
                                      ,c.WorkerName
                                      ,c.CertificateCode
                                FROM [dbo].[CERTIFICATECHANGE] b
                                 inner join [dbo].[CERTIFICATE] c 
                                on (b.UNITCODE='{0}' or b.NEWUNITCODE='{0}') and b.CERTIFICATEID = c.CERTIFICATEID 
                                where 1=1 {2}
                                union all
                                SELECT b.[CERTIFICATECONTINUEID]  
                                      ,b.[APPLYDATE] 
	                                  ,'证书续期'     
                                      ,b.[GETDATE]
                                      ,b.[GETRESULT]     
                                      ,b.[CHECKDATE]
                                      ,b.[CHECKRESULT]    
                                      ,b.[CONFIRMDATE]
                                      ,b.[CONFIRMRESULT]
	                                  ,b.[CONFIRMDATE]
                                      ,replace(b.[CONFIRMRESULT],'决定','告知')
                                      ,c.PostName
                                      ,c.WorkerName
                                      ,c.CertificateCode
                                  FROM [dbo].[CERTIFICATECONTINUE] b
                                inner join [dbo].[CERTIFICATE] c 
                                on (b.UNITCODE='{0}' or b.NEWUNITCODE='{0}') and b.CERTIFICATEID = c.CERTIFICATEID
                                where 1=1  {2}
                            ) t
                            ";

                        QueryParamOB q2 = new QueryParamOB();
  
                        //姓名
                        if (RadTextBoxWorkerName.Text.Trim() != "")
                        {
                            q.Add(string.Format("s.WorkerName ='{0}'", RadTextBoxWorkerName.Text.Trim()));
                            q2.Add(string.Format("c.WorkerName ='{0}'", RadTextBoxWorkerName.Text.Trim()));                          
                        }
                        //证书编号
                        if (RadTextBoxCertificateCode.Text.Trim() != "")
                        {
                            q.Add(string.Format("s.RESULTCERTIFICATECODE ='{0}'", RadTextBoxCertificateCode.Text.Trim()));
                            q2.Add(string.Format("c.CERTIFICATECODE ='{0}'", RadTextBoxCertificateCode.Text.Trim()));                          
                        }
                        //申请日期
                        q.Add(string.Format("s.[SIGNUPDATE] >'{0}'", DateTime.Now.AddMonths(-Convert.ToInt32(RadioButtonListApplyDate.SelectedValue))));
                        q2.Add(string.Format("b.[APPLYDATE] >'{0}'", DateTime.Now.AddMonths(-Convert.ToInt32(RadioButtonListApplyDate.SelectedValue))));                     

                        dt = CommonDAL.GetDataTable(string.Format(sql, ZZJGDM, q.ToWhereString(),q2.ToWhereString()));
                        break;
                }
                
                RadGridProcess.DataSource = dt;
                RadGridProcess.DataBind();

            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "企业查询业务办理进度失败！", ex);
                return;
            }
        }

    }
}
