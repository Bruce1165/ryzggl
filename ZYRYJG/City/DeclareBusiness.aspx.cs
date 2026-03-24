using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZYRYJG.City
{
    public partial class DeclareBusiness :BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BtnQuery_Click(sender, e);
            }
        }

        protected void BtnQuery_Click(object sender, EventArgs e)
        {
            //QueryParamOB q = new QueryParamOB();
            //q.Add(string.Format("GetDateTime >='{0}'", RadDatePickerStart.SelectedDate.HasValue == true ? RadDatePickerStart.SelectedDate.Value.ToString("yyyy-MM-dd") : "2000-01-01"));
            //q.Add(string.Format("GetDateTime <='{0}'", RadDatePickerEnd.SelectedDate.HasValue == true ? RadDatePickerEnd.SelectedDate.Value.ToString("yyyy-MM-dd 23:59:59") : "2050-01-01"));
            string query = "";//区县受理/决定   
            string query2 = "";//注册中心受理/决定

            if (RadComboBoxIten.SelectedValue == "受理时间" && RadDatePickerStart.SelectedDate.HasValue == true)
            {
                query = string.Format("GetDateTime >'{0}'", RadDatePickerStart.SelectedDate.Value);
                query2 = string.Format("CheckDate  >'{0}'", RadDatePickerStart.SelectedDate.Value);
            }
            if (RadComboBoxIten.SelectedValue == "受理时间"&& RadDatePickerEnd.SelectedDate.HasValue == true)
            {
                query += string.Format("and GetDateTime <='{0} 23:59:59'", RadDatePickerEnd.SelectedDate.Value.ToString("yyyy-MM-dd"));
                query2 += string.Format("and CheckDate <='{0} 23:59:59'", RadDatePickerEnd.SelectedDate.Value.ToString("yyyy-MM-dd"));
            }
            if (RadComboBoxIten.SelectedValue == "决定时间" && RadDatePickerStart.SelectedDate.HasValue == true)
            {
                query = string.Format("ExamineDatetime >'{0}'", RadDatePickerStart.SelectedDate.Value);
                query2 = string.Format("ConfirmDate >'{0}'", RadDatePickerStart.SelectedDate.Value);
            }
            if (RadComboBoxIten.SelectedValue == "决定时间" &&  RadDatePickerEnd.SelectedDate.HasValue == true)
            {
                query += string.Format("and ExamineDatetime <='{0} 23:59:59'", RadDatePickerEnd.SelectedDate.Value.ToString("yyyy-MM-dd"));
                query2 += string.Format("and ConfirmDate <='{0} 23:59:59'", RadDatePickerEnd.SelectedDate.Value.ToString("yyyy-MM-dd"));
            }
            #region 注册中心
            
        
            string sql2 = string.Format(@"
        union
        select ENT_City='注册中心',
		ApplyType=case when [ApplyType] = '变更注册' then [ApplyTypeSub] else [ApplyType] end,
		ApplyCount=count(*)
		FROM  [dbo].[Apply] 
		where len([ENT_City])>0 and {0} and PSN_Level='{1}'
		group by ENT_City,
		case when [ApplyType] = '变更注册' then [ApplyTypeSub] else [ApplyType] end
		", query2 == "" ? "1=1" : query2, RadioButtonPSN_Level.SelectedValue);
         #endregion


            #region 总计
            
          
            string sql3 = string.Format(@"
            union
			select ENT_City='总计',ApplyType,ApplyCount=sum(ApplyCount)
			from
				(
				select 
				ENT_City=replace(case when [ENT_City] like '%亦庄%' or [ENT_City] like '%开发区%' then '开发区'else [ENT_City] end,'县','区'),
				ApplyType=case when [ApplyType] = '变更注册' then [ApplyTypeSub] else [ApplyType] end,
				ApplyCount=count(*)
				FROM [dbo].[Apply]
				where len([ENT_City])>0  and  {0} and PSN_Level='{1}'
				group by replace(case when [ENT_City] like '%亦庄%' or [ENT_City] like '%开发区%' then '开发区'else [ENT_City] end,'县','区'),
				case when [ApplyType] = '变更注册' then [ApplyTypeSub] else [ApplyType] end
          
				union
				select ENT_City='注册中心',
				ApplyType=case when [ApplyType] = '变更注册' then [ApplyTypeSub] else [ApplyType] end,
				ApplyCount=count(*)
				FROM  [dbo].[Apply] 
				where len([ENT_City])>0 and {2}  and PSN_Level='{1}'
				group by ENT_City,
				case when [ApplyType] = '变更注册' then [ApplyTypeSub] else [ApplyType] end
				) h
	       group by ApplyType", query == "" ? "1=1" : query, RadioButtonPSN_Level.SelectedValue, query2 == "" ? "1=1" : query2);

            #endregion


            #region 全部
           
            string sql = string.Format(@"
select q.[ENT_City]
,isnull(d.初始注册,0) 初始注册
,isnull(d.重新注册,0) 重新注册
,isnull(d.增项注册,0) 增项注册
,isnull(d.延期注册,0) 延期注册
,isnull(d.遗失补办,0) 遗失补办
,isnull(d.注销,0) 注销
,isnull(d.企业信息变更,0) 企业信息变更
,isnull(d.执业企业变更,0) 执业企业变更
,isnull(d.个人信息变更,0) 个人信息变更
,(isnull(d.初始注册,0)+isnull(d.重新注册,0)+isnull(d.增项注册,0)+isnull(d.延期注册,0)+isnull(d.遗失补办,0)+isnull(d.注销,0)+isnull(d.企业信息变更,0) +isnull(d.执业企业变更,0)+isnull(d.个人信息变更,0)) as 合计
from
(
  SELECT [RegionCode],replace(replace([RegionName],'县',''),'亦庄','开发区') ENT_City
  FROM [dbo].[Dict_Region]
  where [RegionName] not in('崇文区','宣武区')
  union select '800000','注册中心'
  union select '900000','总计'
 ) q
 left join
 (
		select *
		from
		 (
		 select 
			ENT_City=replace(case when [ENT_City] like '%亦庄%' or [ENT_City] like '%开发区%' then '开发区'else [ENT_City] end,'县','区'),
			ApplyType=case when [ApplyType] = '变更注册' then [ApplyTypeSub] else [ApplyType] end,
			ApplyCount=count(*)
		  FROM [dbo].[Apply]
		  where len([ENT_City])>0  and {0} and PSN_Level='{1}'
		  group by replace(case when [ENT_City] like '%亦庄%' or [ENT_City] like '%开发区%' then '开发区'else [ENT_City] end,'县','区'),
		  case when [ApplyType] = '变更注册' then [ApplyTypeSub] else [ApplyType] end
          {2}
          {3}
		) a 
		pivot (sum(ApplyCount) for [ApplyType] in 
		(
	    重新注册,
		注销,
		企业信息变更,
		遗失补办,
		延期注册,
		初始注册,
		执业企业变更,
		个人信息变更,
		增项注册
		)
		) b
) d on q.ENT_City = d.ENT_City where 1=1  order by q.[RegionCode]", query == "" ? "1=1" : query, RadioButtonPSN_Level.SelectedValue, sql2,sql3);

            #endregion
            DataTable dt = CommonDAL.GetDataTable(sql);
            RadGridTJ.DataSource = dt;
            RadGridTJ.DataBind();
        }

        protected void ButtonExport_Click(object sender, EventArgs e)
        {
            if (RadGridTJ.Items.Count > 0)
            {
                string format = "yyyyMMdd";

                string stringValue = DateTime.Now.ToString(format);
                //RadGridRepeat.DataSource =dt;

                //RadGridRepeat.DataBind();

                RadGridTJ.ExportSettings.IgnorePaging = true;

                RadGridTJ.ExportSettings.OpenInNewWindow = true;

                RadGridTJ.ExportSettings.FileName = "办理量统计" + stringValue;

                RadGridTJ.MasterTableView.ExportToExcel();
            }
        }
    }
}