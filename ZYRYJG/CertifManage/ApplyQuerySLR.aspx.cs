using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;
using System.Data;
using System.Collections;

namespace ZYRYJG.CertifManage
{
    //业务查询
    public partial class ApplyQuerySLR : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                ButtonSearch_Click(sender, e);
            }
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns></returns>
        protected QueryParamOB GetQueryParam()
        {
            if(ViewState["QueryParamOB"] !=null)
            {
                return (QueryParamOB)ViewState["QueryParamOB"];
            }

            var q = new QueryParamOB();

            //日期类型自定义查询条件
            if (RadDatePickerGetDateStart.SelectedDate.HasValue == true)
            {
                q.Add(string.Format("{0} >= '{1}'", RadComboBoxRQItem.SelectedValue, RadDatePickerGetDateStart.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }
            if (RadDatePickerGetDateEnd.SelectedDate.HasValue == true)
            {
                q.Add(string.Format("{0} <= '{1} 23:59:59'", RadComboBoxRQItem.SelectedValue, RadDatePickerGetDateEnd.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }

            //申请状态            
            if(RadComboBoxApplyStatus.SelectedValue !="")
            {
                q.Add(string.Format("ApplyStatus = '{0}'", RadComboBoxApplyStatus.SelectedValue));
            }           
           
            //文本类型自定义查询条件
            if (RadTextBoxValue.Text.Trim() != "")
            {
                q.Add(string.Format("{0} like '%{1}%'", RadComboBoxTypeItem.SelectedValue, RadTextBoxValue.Text.Trim()));
            }

            //申请类型
            if (RadComboBoxPSN_RegisteType.SelectedValue != "")
            {
                q.Add(string.Format("ItemType ='{0}'", RadComboBoxPSN_RegisteType.SelectedValue));
            }
            ViewState["QueryParamOB"] = q;
            return q;
        }


        //根据条件查询
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ViewState["QueryParamOB"] = null;
            RefreshRadGridProcess(0);        
        }

        protected void RefreshRadGridProcess(int pageIndex)
        {
            QueryParamOB q = GetQueryParam();

            string sql = @"(     
    SELECT b.[CERTIFICATECHANGEID] DataID
			,c.PostTypeName
			,c.PostName
            ,c.[CERTIFICATECODE]
			,c.WORKERNAME
			,c.WORKERCERTIFICATECODE
			,b.NEWUNITNAME as UNITNAME
			,case b.[STATUS] when '已告知' then '已决定' else b.[STATUS] end as ApplyStatus
            ,b.[APPLYDATE]    
	        ,b.[CHANGETYPE] ItemType
            ,'change' ItemCode  
			,case b.[STATUS] 
				when '填报中' then b.[APPLYDATE]
				when '待单位确认' then b.[APPLYDATE]
				when '已申请' then case when b.[NewUnitCheckTime] is not null then b.[NewUnitCheckTime] else b.OldUnitCheckTime end
				when '已受理' then 	b.[GETDATE]
				when '退回修改' then case when b.[CONFRIMDATE] is not null then  b.[CONFRIMDATE] when 	b.[GetDate] is not null then 	b.[GetDate] when b.[NewUnitCheckTime] is not null then b.[NewUnitCheckTime] else b.[OldUnitCheckTime] end
				when '已审核' then 	b.[CHECKDATE]
				when '已决定' then b.[CONFRIMDATE]
				when '已告知' then b.[CONFRIMDATE] 
			end  as progressDate
			,case b.[STATUS] 
				when '填报中' then '填报中'
				when '待单位确认' then '待单位确认'
				when '已申请' then case when b.[NewUnitCheckTime] is not null then b.NewUnitAdvise else b.OldUnitAdvise end
				when '已受理' then 	b.[GETRESULT]
				when '退回修改' then case when b.[CONFRIMDATE] is not null then  b.ConfrimResult when 	b.[GetDate] is not null then 	b.GetResult when b.[NewUnitCheckTime] is not null then b.NewUnitAdvise else b.OldUnitAdvise end
				when '已审核' then 	b.[GETRESULT]
				when '已决定' then b.ConfrimResult
				when '已告知' then b.ConfrimResult 
			end  as progressResult
            ,case b.[STATUS] when '已告知' then 1 else 0 end IfEnd
			,b.CREATEPERSONID
			,b.CERTIFICATEID
    FROM [dbo].[CERTIFICATECHANGE] b
    inner join [dbo].[CERTIFICATE] c on b.CERTIFICATEID = c.CERTIFICATEID   and b.CREATEPERSONID>0  
	where 1=1 {0}
    union all
    SELECT [APPLYID] 
			,PostTypeName
			,PostName
            ,[CERTIFICATECODE]
			,WORKERNAME
			,WORKERCERTIFICATECODE
			,UNITNAME
			,[APPLYSTATUS]
            ,[APPLYDATE] 
	        ,'进京变更'   
            ,'JinJing' ItemCode   
			,case [APPLYSTATUS] 
				when '填报中' then [APPLYDATE]
				when '待单位确认' then [APPLYDATE]
				when '已申请' then  [NewUnitCheckTime] 
				when '已受理' then 	[ACCEPTDATE]
				when '退回修改' then case when [CONFRIMDATE] is not null then  [CONFRIMDATE] when [CHECKDATE] is not null then 	[CHECKDATE] when 	[ACCEPTDATE] is not null then 	[ACCEPTDATE] when [NewUnitCheckTime] is not null then [NewUnitCheckTime] else null end
				when '已审核' then 	[CHECKDATE]
				when '已编号' then [CONFRIMDATE]
			end  as progressDate
			,case [APPLYSTATUS] 
				when '填报中' then '填报中'
				when '待单位确认' then '待单位确认'
				when '已申请' then NewUnitAdvise
				when '已受理' then 	[GETRESULT]
				when '退回修改' then case when [CONFRIMDATE] is not null then  ConfrimResult when 	[ACCEPTDATE] is not null then 	GetResult when [NewUnitCheckTime] is not null then NewUnitAdvise else '' end
				when '已审核' then 	[CHECKRESULT]
				when '已编号' then ConfrimResult
			end  as progressResult
            ,case [APPLYSTATUS] when '已编号' then 1 else 0 end IfEnd
			,CREATEPERSONID
			,CERTIFICATEID
    FROM [dbo].[VIEW_CERTIFICATE_ENTER] 
    where 1=1 {0}                
    union all
    SELECT b.[CERTIFICATECONTINUEID]
			,c.PostTypeName
			,c.PostName
            ,c.[CERTIFICATECODE]
			,c.WORKERNAME
			,c.WORKERCERTIFICATECODE
			,b.[NEWUNITNAME]
			,b.[STATUS]
            ,b.[APPLYDATE] 
	        ,'证书续期'   
            ,'continue' ItemCode
			,case b.[STATUS] 
				when '填报中' then b.[APPLYDATE]
				when '待单位确认' then b.[APPLYDATE]
				when '已申请' then b.[NewUnitCheckTime]
				when '已初审' then 	b.[GETDATE]
				when '退回修改' then case when b.[CONFIRMDATE] is not null then  b.[CONFIRMDATE] when 	b.[CHECKDATE] is not null then 	b.[CHECKDATE] when 	b.[GetDate] is not null then 	b.[GetDate] when b.[NewUnitCheckTime] is not null then b.[NewUnitCheckTime] else b.[APPLYDATE] end
				when '已审核' then 	b.[CHECKDATE]
				when '已决定' then b.[CONFIRMDATE]
			end  as progressDate
			,case b.[STATUS] 
				when '填报中' then '填报中'
				when '待单位确认' then '待单位确认'
				when '已申请' then  b.NewUnitAdvise
				when '已初审' then 	b.[GETRESULT]
				when '退回修改' then case when b.[CONFIRMDATE] is not null then  b.[CONFIRMRESULT] when 	b.[CHECKDATE] is not null then 	b.[CHECKRESULT] when 	b.[GetDate] is not null then 	b.GetResult when b.[NewUnitCheckTime] is not null then b.NewUnitAdvise else '' end
				when '已审核' then 	b.[CHECKRESULT]
				when '已决定' then b.[CONFIRMRESULT]	
			end  as progressResult
            ,case b.[STATUS] when '已决定' then 1 else 0 end IfEnd
			,b.CREATEPERSONID
			,b.CERTIFICATEID
    FROM [dbo].[CERTIFICATECONTINUE] b
    inner join [dbo].[CERTIFICATE] c on  b.CERTIFICATEID = c.CERTIFICATEID
	where 1=1 {0}
    union
	select 
			[APPLYID] 
			,'安全生产考核三类人员' as	PostTypeName
			,'企业主要负责人' as PostName
            ,case when [CertificateCodeMore] is not null then [CertificateCodeMore] else [CERTIFICATECODE] end as CERTIFICATECODE
			,WORKERNAME
			,WORKERCERTIFICATECODE
			,[UnitNameMore] as UNITNAME
			,[APPLYSTATUS]
            ,[CreateTime] as [APPLYDATE] 
	        ,'A本增发'   
            ,'AddA' ItemCode   
			,case [APPLYSTATUS] 
				when '填报中' then [CreateTime]
				when '待单位确认' then [CreateTime]
				when '已申请' then  [NewUnitCheckTime] 		
				when '退回修改' then case when [ConfirmDate] is not null then  [ConfirmDate] when [CHECKDATE] is not null then 	[CHECKDATE]  when [NewUnitCheckTime] is not null then [NewUnitCheckTime] else null end
				when '已审核' then 	[CHECKDATE]
				when '已决定' then [ConfirmDate]
			end  as progressDate
			,case [APPLYSTATUS] 
				when '填报中' then '填报中'
				when '待单位确认' then '待单位确认'
				when '已申请' then NewUnitAdvise
				when '退回修改' then case when [ConfirmDate] is not null then [ConfirmAdvise]  when [CHECKDATE] is not null then [CheckAdvise]	when [NewUnitCheckTime] is not null then NewUnitAdvise else '' end
				when '已审核' then 	[CheckAdvise]
				when '已决定' then [ConfirmAdvise]
			end  as progressResult
            ,case [APPLYSTATUS] when '已决定' then 1 else 0 end IfEnd
			,0
			,CERTIFICATEID
	from dbo.CertificateMore
	where 1=1 
    union
	select 
			[APPLYID] 
			,'安全生产考核三类人员' as	PostTypeName
			,'综合类专职安全生产管理人员' as PostName
            ,[CertificateCode1] +'，'+[CertificateCode2] as CERTIFICATECODE
			,WORKERNAME
			,WORKERCERTIFICATECODE
			,[UNITNAME] 
			,[APPLYSTATUS]
            ,[APPLYDATE] 
	        ,'C1、C2合并'   
            ,'Merge' ItemCode   
			,case [APPLYSTATUS] 
				when '填报中' then [CreateTime]
				when '待单位确认' then [CreateTime]
				when '已申请' then  [UnitCheckTime] 		
				when '退回修改' then case  when [CHECKDATE] is not null then 	[CHECKDATE]  when [UnitCheckTime] is not null then [UnitCheckTime] else null end
				when '已决定' then 	[CHECKDATE]
		
			end  as progressDate
			,case [APPLYSTATUS] 
				when '填报中' then '填报中'
				when '待单位确认' then '待单位确认'
				when '已申请' then [UnitAdvise]
				when '退回修改' then case when [CHECKDATE] is not null then [CheckAdvise]	when [UnitCheckTime] is not null then [UnitAdvise] else '' end
				when '已决定' then[CheckAdvise] 
			end  as progressResult
            ,case [APPLYSTATUS] when '已决定' then 1 else 0 end IfEnd
			,CREATEPERSONID
			,0
	from dbo.[CertificateMerge]
	where 1=1 
) p";


            RadGridProcess.DataSource = CommonDAL.GetDataTable(
            pageIndex * RadGridProcess.PageSize, (pageIndex + 1) * RadGridProcess.PageSize, string.Format(sql, " and PostTypeID=1"), "*", q.ToWhereString(), "p.ApplyDate desc,p.dataID");

            int rowCount = CommonDAL.GetRowCount(string.Format(sql, " and PostTypeID=1"), q.ToWhereString());

            RadGridProcess.VirtualItemCount = rowCount;
            RadGridProcess.CurrentPageIndex = pageIndex;
            RadGridProcess.DataBind();
        }

        protected void RadGridProcess_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            RefreshRadGridProcess(e.NewPageIndex);
        }

        protected void SetShowItem(string list)
        {
            foreach (Telerik.Web.UI.RadComboBoxItem i in RadComboBoxApplyStatus.Items)
            {
                i.Visible = false;
            }
            string[] f = list.Split(',');
            foreach (var s in f )
            {
                RadComboBoxApplyStatus.Items.FindItemByValue(s).Visible = true;
            }
        }

        protected void RadComboBoxPSN_RegisteType_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            switch (RadComboBoxPSN_RegisteType.SelectedValue)
            {
                case "京内变更":
                case "离京变更":
                case "注销":
                    SetShowItem("填报中,待单位确认,已申请,退回修改,已审核,已决定");
                    break;
                case "进京变更":
                    SetShowItem("填报中,待单位确认,已申请,退回修改,已受理,已审核,已编号");
                    break;
                case "证书续期":
                    SetShowItem("填报中,待单位确认,已申请,退回修改,已初审,已审核,已决定");
                    break;
                case "A本增发":
                    SetShowItem("填报中,待单位确认,已申请,退回修改,已审核,已决定");
                    break;
                case "C1、C2合并":
                    SetShowItem("填报中,待单位确认,已申请,退回修改,已决定");
                    break;
                default:
                    foreach (Telerik.Web.UI.RadComboBoxItem i in RadComboBoxApplyStatus.Items)
                    {
                        i.Visible = true;
                    }
                    break;
            }

        }

        public string GetApplyTypeUrl(string type,string ApplyID,string CERTIFICATEID,string cjrID)
        {
           
            switch (type)
            {
                case "京内变更":
                    if (cjrID=="0")//企业信息变更
                        return string.Format("../CertifManage/CompanyNameChange.aspx?a={0}",Utility.Cryptography.Encrypt(ApplyID));
                    else
                        return string.Format("../CertifManage/Application.aspx?o={0}", Utility.Cryptography.Encrypt(ApplyID));
                case "离京变更":
                case "注销":
                    return string.Format("../CertifManage/Application.aspx?o={0}", Utility.Cryptography.Encrypt(ApplyID));
                case "进京变更":
                    return string.Format("../CertifEnter/CertifEnterApplyEdit.aspx?t={0}&o={1}", Utility.Cryptography.Encrypt("1"), Utility.Cryptography.Encrypt(ApplyID));
                case "证书续期":
                    return string.Format("../RenewCertifates/ApplyDetail.aspx?o={0}&o2={1}", Utility.Cryptography.Encrypt(CERTIFICATEID), Utility.Cryptography.Encrypt(ApplyID));
                case "A本增发":
                    return string.Format("../CertifManage/CertifMoreCheck.aspx?o={0}",Utility.Cryptography.Encrypt(ApplyID));//
                case "C1、C2合并":
                    return string.Format("../CertifManage/CertificateMergeApply.aspx?o={0}",Utility.Cryptography.Encrypt(ApplyID));//
                default:
                    return "#";
            }

        }
       

        //导出excel
        //protected void ButtonOutput_Click(object sender, EventArgs e)
        //{
        //    QueryParamOB q = GetQueryParam();

        //    try
        //    {
        //        //EXCEL表头明
        //        string head = @"姓名\性别\证件号码\注册专业\注册编号\企业名称\企业组织机构代码\申报类型";
        //        //数据表的列明
        //        string column = @"PSN_Name\PSN_Sex\PSN_CertificateNO\PSN_RegisteProfession\PSN_RegisterNo\ENT_Name\ENT_OrganizationsCode\case ApplyType when '变更注册' then ApplyTypeSub when '延期注册' then '延续注册' else ApplyType end";
        //        //过滤条件

        //        if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/Excel/"))) System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Upload/Excel/"));
        //        string filePath = string.Format("~/Upload/Excel/{0}_{1}.xls", DateTime.Now.ToString("yyyyMMdd"), Guid.NewGuid());
        //        CommonDAL.OutputXls(HttpContext.Current.Server.MapPath(filePath)
        //            , "Apply"
        //            , q.ToWhereString(), "ApplyID", head.ToString(), column.ToString());
        //        string size = CommonDAL.GetFileSize(HttpContext.Current.Server.MapPath(filePath));
        //        spanOutput.InnerHtml = string.Format(@"<a href=""{0}"">{1}</a><span  style=""padding-left:20px;"">导出结果（{2}）</span>"
        //            , UIHelp.ShowFile(filePath)
        //            , "点击我下载"
        //            , size);
        //    }
        //    catch (Exception ex)
        //    {
        //        UIHelp.WriteErrorLog(Page, "业务查询结果导出EXCEL失败！", ex);
        //    }
        //}

      



    }
}