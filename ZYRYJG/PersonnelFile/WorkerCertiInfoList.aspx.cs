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
    public partial class WorkerCertiInfoList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                WorkerOB _WorkerOB = WorkerDAL.GetObject(PersonID);
                string IDCard15 = _WorkerOB.CertificateCode; 
                string IDCard18 = _WorkerOB.CertificateCode;
                if (_WorkerOB.CertificateCode.Length == 15 && _WorkerOB.CertificateType == "身份证")
                {                  
                    IDCard18 = Utility.Check.ConvertoIDCard15To18(IDCard15);
                    IDCard15 = _WorkerOB.CertificateCode;
                }
                else if (_WorkerOB.CertificateCode.Length == 18 && _WorkerOB.CertificateType == "身份证")
                {
                    IDCard15 = _WorkerOB.CertificateCode.Remove(17, 1).Remove(6, 2);
                }
                else
                {
                    IDCard15 = _WorkerOB.CertificateCode;
                    IDCard18 = _WorkerOB.CertificateCode;
                }

                string sql= @"
SELECT cast([CERTIFICATEID] as varchar(64)) as [CERTIFICATEID],[POSTTYPEID],[POSTID],[POSTTYPENAME],[POSTNAME],[CERTIFICATECODE],[VALIDENDDATE],[WORKERCERTIFICATECODE],[UNITNAME],[STATUS],[REMARK]
FROM [dbo].[CERTIFICATE] where (WORKERCERTIFICATECODE='{0}' or WORKERCERTIFICATECODE='{1}') and Status <>'待审批' and Status <>'进京待审批'
union all  
SELECT [PSN_ServerID],-99,-99, [PSN_Level] +'建造师',[PSN_RegisteProfession],[PSN_RegisterNO],[PSN_CertificateValidity],[PSN_CertificateNO],[ENT_Name],[PSN_RegisteType],''      
FROM [dbo].[jcsjk_jzs] where [PSN_Level] like '一级%' and (PSN_CertificateNO='{0}' or PSN_CertificateNO='{1}')
union all
    SELECT [PSN_ServerID],0,0 ,[PSN_Level] +'建造师',[PSN_RegisteProfession],[PSN_RegisterNO],[PSN_CertificateValidity],[PSN_CertificateNO],[ENT_Name],
    case [PSN_RegisteType]
    when '01' then '初始注册'
    when '02' then '变更注册'
    when '03' then '延期注册'
    when '04' then '增项注册'
    when '05' then '重新注册'
    when '06' then '遗失补办'
    when '07' then '注销'
    else '' end as [PSN_RegisteType]
    ,[Memo] 
    FROM [dbo].[COC_TOW_Person_BaseInfo] 
    where (PSN_CertificateNO='{0}' or PSN_CertificateNO='{1}')
union all
    SELECT cast([ID] as varchar(64)),-99,-99,'一级造价工程师','' ,[ZCZH],[ZSYXQ],[SFZH],[PYDW],'',''
    FROM [dbo].[jcsjk_zjs]
    where ([SFZH]='{0}' or [SFZH]='{1}')
union all
    SELECT [PSN_ServerID],-1,-1,'二级造价工程师',[PSN_RegisteProfession],[PSN_RegisterNO],[PSN_CertificateValidity],[PSN_CertificateNO],[ENT_Name],
    case [PSN_RegisteType]
    when '01' then '初始注册'
    when '02' then '变更注册'
    when '03' then '延期注册'
    when '04' then '增项注册'
    when '05' then '重新注册'
    when '06' then '遗失补办'
    when '07' then '注销'
    else '' end as [PSN_RegisteType]
    ,[Memo] 
    FROM [dbo].[zjs_Certificate]
    where (PSN_CertificateNO='{0}' or PSN_CertificateNO='{1}')
union all
    SELECT [证件号],-99,-99,'监理师',[注册专业1]+ case [注册专业2] when '无' then '' else ','+[注册专业2] end,[注册号],[注册有效期],[证件号],[聘用单位],'',''
    FROM [dbo].[jcsjk_jls]
    where ([证件号]='{0}' or [证件号]='{1}')
";
                DataTable dt = CommonDAL.GetDataTable(string.Format(sql, IDCard15, IDCard18));
                RadGrid1.DataSource = dt;
                RadGrid1.DataBind();

                //最近业务进度
                GetApplyProcess(IDCard18);

                //重要信息提示
                string ts = GetNotice(IDCard18);

                if (string.IsNullOrEmpty(ts) == false)
                {
                    divMessage.InnerHtml = ts;
                }

                RefreshRadGridContinueTimeSpan();
            }
        }

        //续期时间段列表
        protected void RefreshRadGridContinueTimeSpan()
        {
            DBHelper db = new DBHelper();
            try
            {
                DataTable dt = TypesDAL.GetListByTypeID("106");//续期时间设置
                RadGridContinueTimeSpan.DataSource = dt;
            
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "读取续期时间设置失败！", ex);
                return;
            }
        }

        
        protected void RadGridContinueTimeSpan_ItemDataBound(object sender, GridItemEventArgs e)
        {
            //格式化列
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                string[] settings = RadGridContinueTimeSpan.MasterTableView.DataKeyValues[e.Item.ItemIndex]["TypeValue"].ToString().Split(',');
                e.Item.Cells[RadGridContinueTimeSpan.MasterTableView.Columns.FindByUniqueName("ValidTo").OrderIndex].Text = settings[1].Replace("-", "月") + "日";
                e.Item.Cells[RadGridContinueTimeSpan.MasterTableView.Columns.FindByUniqueName("RenewMonth").OrderIndex].Text = string.Format("{0}月-{1}月", settings[2], settings[3]);
            }

          
        }


        //获取最新50条业务办理进度
        private void GetApplyProcess(string WorkerCertificateCode)
        {
            string sql = @"select top 50 t.*,row_number() over(order by ApplyDate desc) as RowNum from 
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
                                FROM [dbo].[EXAMSIGNUP] s
                                inner join examplan e on s.examplanid = e.examplanid
		                        left join [dbo].[EXAMRESULT] r on s.EXAMSIGNUPID = r.EXAMSIGNUP_ID
	                            where CERTIFICATECODE='{0}'
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
                                FROM [dbo].[CERTIFICATECHANGE] b
                                 inner join [dbo].[CERTIFICATE] c 
                                on c.WORKERCERTIFICATECODE='{0}' and b.CERTIFICATEID = c.CERTIFICATEID                              
                                union all
                                SELECT x.[CERTIFICATECONTINUEID]  
                                      ,x.[APPLYDATE] 
	                                  ,'证书续期'     
                                      ,x.[GETDATE]
                                      ,x.[GETRESULT]     
                                      ,x.[CHECKDATE]
                                      ,x.[CHECKRESULT]    
                                      ,x.[CONFIRMDATE]
                                      ,x.[CONFIRMRESULT]
	                                  ,x.[CONFIRMDATE]
                                      ,x.[CONFIRMRESULT]
                                      ,c.PostName
                                  FROM [dbo].[CERTIFICATECONTINUE] x
                                inner join [dbo].[CERTIFICATE] c 
                                on c.WORKERCERTIFICATECODE='{0}' and x.CERTIFICATEID = c.CERTIFICATEID
                            ) t
                            ";

            DataTable dt = CommonDAL.GetDataTable(string.Format(sql, WorkerCertificateCode));

            RadGridProcess.DataSource = dt;
            RadGridProcess.DataBind();
	
        }

        //重要通知
        private string GetNotice(string WorkerCertificateCode)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            //近一年即将过期证书提醒
            string sql = @"SELECT [CERTIFICATEID] ,[CERTIFICATECODE],[VALIDENDDATE],[POSTTYPENAME],[POSTNAME]
                          FROM [dbo].[CERTIFICATE]
                          where [WORKERCERTIFICATECODE]='{0}'
                          and dateadd(year,-1,[VALIDENDDATE]) < getdate() and [VALIDENDDATE] > getdate()
                          order by VALIDENDDATE";

            DataTable dt = CommonDAL.GetDataTable(string.Format(sql, WorkerCertificateCode));

            if(dt==null || dt.Rows.Count==0)
            {
                div_ZYTJ.Visible = false;//没有预警信息，重要提示隐藏
            }

            foreach(DataRow r in dt.Rows)
            {
                if (sb.Length > 0) {
                    sb.Append("<br />");
                }
                sb.Append(string.Format("你的【{0} - {1}】证书，编号：{2} 将于<span class='f-red'>{3}</span>日过期，请您注意续期开放时间段，提前做好续期准备。"
                    , r["POSTTYPENAME"], r["POSTNAME"], r["CERTIFICATECODE"], Convert.ToDateTime(r["VALIDENDDATE"]).ToString("yyyy-MM-dd")));
            }

            return sb.ToString();
        }
    }
}
