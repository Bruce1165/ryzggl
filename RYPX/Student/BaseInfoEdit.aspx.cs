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

public partial class Student_BaseInfoEdit : BasePage
{
    protected override string CheckVisiteRgihtUrl
    {
        get
        {
            return "jxjy/MyTrain.aspx";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        CachejxjyClickCount += 1;
        if (!IsPostBack)
        {
            int cur = DateTime.Now.Month;
            if ((cur >= 4 && cur <= 6) || (cur >= 10 && cur <= 12))
            {
                divTip.Visible = true;
            }
            WorkerOB _WorkerOB = WorkerDAL.GetObject(PersonID);
            //LabelWorkerName.Text = _WorkerOB.WorkerName;
            //LabelWorkerCertificateCode.Text = _WorkerOB.CertificateCode;
            //LabelMobile.Text = _WorkerOB.Mobile;
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

            string sql = @"
select c.*,p.[PackageID]
from 
(
SELECT cast([CERTIFICATEID] as varchar(64)) as [CERTIFICATEID],[POSTTYPEID],[POSTID],[POSTTYPENAME],[POSTNAME],[CERTIFICATECODE],[VALIDENDDATE],[WORKERCERTIFICATECODE],[UNITNAME],[STATUS],[REMARK]
FROM [dbo].[CERTIFICATE] where (WORKERCERTIFICATECODE='{0}' or WORKERCERTIFICATECODE='{1}') and [VALIDENDDATE] > dateadd(day,-1,getdate()) and Status <>'待审批' and Status <>'进京待审批' and Status <>'离京变更' and Status <>'注销'
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
) c left join [RYPX].[dbo].[Package] p 
on ((c.[POSTTYPENAME] = p.[POSTTYPENAME]  and c.[POSTNAME] = p.[POSTNAME]) or (c.[POSTTYPENAME] = p.[POSTTYPENAME]  and p.[POSTNAME] is null)) and p.[Status]='已发布'
";
            DataTable dt = CommonDAL.GetDataTable(string.Format(sql, IDCard15, IDCard18));
            RadGridCertificate.DataSource = dt;
            RadGridCertificate.DataBind();
        }
    }

}
