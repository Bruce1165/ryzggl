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
    public partial class ApplyQueryTZ : BasePage
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
    SELECT b.[CERTIFICATECONTINUEID]
			,c.PostTypeName
			,c.PostName
            ,c.[CERTIFICATECODE]
			,c.WORKERNAME
			,c.WORKERCERTIFICATECODE
			,b.NEWUNITNAME
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
   
) p";


            RadGridProcess.DataSource = CommonDAL.GetDataTable(
            pageIndex * RadGridProcess.PageSize, (pageIndex + 1) * RadGridProcess.PageSize, string.Format(sql, " and c.PostTypeID=2"), "*", q.ToWhereString(), "p.ApplyDate desc,p.dataID");

            int rowCount = CommonDAL.GetRowCount(string.Format(sql, " and c.PostTypeID=2"), q.ToWhereString());

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
                case "注销":
                    SetShowItem("填报中,已申请,退回修改,已审核,已决定");
                    break;

                case "证书续期":
                    SetShowItem("填报中,待单位确认,已申请,退回修改,已初审,已审核,已决定");
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
                case "注销":
                    return string.Format("../CertifManage/Application.aspx?o={0}", Utility.Cryptography.Encrypt(ApplyID));             
                case "证书续期":
                    return string.Format("../RenewCertifates/ApplyDetail.aspx?o={0}&o2={1}", Utility.Cryptography.Encrypt(CERTIFICATEID), Utility.Cryptography.Encrypt(ApplyID));             
                default:
                    return "#";
            }
        }
    }
}