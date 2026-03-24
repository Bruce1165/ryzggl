using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using System.Data;
using Telerik.Web.UI;

namespace ZYRYJG.jxjy
{
    public partial class TjStudyCert : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                for (int i = DateTime.Now.Year; i >= 2022; i--)
                {
                    RadComboBoxPackageYear.Items.Add(new Telerik.Web.UI.RadComboBoxItem(string.Format("{0}年", i), i.ToString()));
                }
                RadComboBoxPackageYear.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem("全部", ""));
                RadComboBoxPackageYear.SelectedIndex = 1;
                BindTj();
            }
        }

        //绑定统计结果
        protected void BindTj()
        {
            DataTable dt = null;
            if (RadComboBoxPackageYear.SelectedValue == "")//全部，按年份统计
            {
                dt = CommonDAL.GetDataTableDB("DBRYPX"
                    , @"select cast(year([FinishDate]) as varchar(4)) +'年' '时间'
                                ,sum(case when PostTypeName='二级建造师' then 1 else 0 end) as '二级建造师'
                                ,sum(case when PostTypeName='二级造价工程师' then 1 else 0 end) as '二级造价工程师'
                                ,sum(case when PostTypeName='安全生产考核三类人员' then 1 else 0 end) as '安全生产考核三类人员'
                                ,sum(case when PostTypeName='建筑施工特种作业' then 1 else 0 end) as '建筑施工特种作业'
                                ,sum(case when PostTypeName='关键岗位专业技术管理人员' then 1 else 0 end) as '关键岗位专业技术管理人员'
                                ,sum(case when PostTypeName='建设职业技能岗位' then 1 else 0 end) as '建设职业技能岗位'
                                ,count(*) 小计
                        FROM [dbo].[FinishCert]
                        group by year([FinishDate])");
            }
            else//选中年度，按月份统计
            {
                dt = CommonDAL.GetDataTableDB("DBRYPX"
                    , string.Format(@"select '{0}-' + t.时间 as '时间'
                                            ,isnull(d.二级建造师,0) '二级建造师'
                                            ,isnull(d.二级造价工程师,0) '二级造价工程师'
                                            ,isnull(d.安全生产考核三类人员,0) '安全生产考核三类人员'
                                            ,isnull(d.建筑施工特种作业,0) '建筑施工特种作业'
                                            ,isnull(d.关键岗位专业技术管理人员,0) '关键岗位专业技术管理人员'
                                            ,isnull(d.建设职业技能岗位,0) '建设职业技能岗位'
                                            ,isnull(d.小计,0) '小计'
                                    from
                                    (
	                                    select '01' as '时间'
                                            union    select '02'
                                            union    select '03'
                                            union    select '04'
                                            union    select '05'
                                            union    select '06'
                                            union    select '07'
                                            union    select '08'
                                            union    select '09'
                                            union    select '10'
                                            union    select '11'
                                            union    select '12'
                                    ) t
                                    left join
                                    (
	                                    select convert(varchar(7),[FinishDate],21) '时间'
	                                        ,sum(case when PostTypeName='二级建造师' then 1 else 0 end) as '二级建造师'
	                                        ,sum(case when PostTypeName='二级造价工程师' then 1 else 0 end) as '二级造价工程师'
	                                        ,sum(case when PostTypeName='安全生产考核三类人员' then 1 else 0 end) as '安全生产考核三类人员'
	                                        ,sum(case when PostTypeName='建筑施工特种作业' then 1 else 0 end) as '建筑施工特种作业'
	                                        ,sum(case when PostTypeName='关键岗位专业技术管理人员' then 1 else 0 end) as '关键岗位专业技术管理人员'
	                                        ,sum(case when PostTypeName='建设职业技能岗位' then 1 else 0 end) as '建设职业技能岗位'
	                                        ,count(*) 小计
	                                    FROM [dbo].[FinishCert] 
	                                    where [FinishDate] between '{0}-01-01' and '{0}-12-31 23:59:59'
	                                    group by convert(varchar(7),[FinishDate],21)
                                    ) d on t.时间 = right(d.时间,2)
                                    union all
                                    select '合计'
                                        ,sum(case when PostTypeName='二级建造师' then 1 else 0 end) as '二级建造师'
                                        ,sum(case when PostTypeName='二级造价工程师' then 1 else 0 end) as '二级造价工程师'
                                        ,sum(case when PostTypeName='安全生产考核三类人员' then 1 else 0 end) as '安全生产考核三类人员'
                                        ,sum(case when PostTypeName='建筑施工特种作业' then 1 else 0 end) as '建筑施工特种作业'
                                        ,sum(case when PostTypeName='关键岗位专业技术管理人员' then 1 else 0 end) as '关键岗位专业技术管理人员'
                                        ,sum(case when PostTypeName='建设职业技能岗位' then 1 else 0 end) as '建设职业技能岗位'
                                        ,count(*) 小计
                                    FROM [dbo].[FinishCert]                           
                                    where  [FinishDate] between '{0}-01-01' and '{0}-12-31 23:59:59'
                                    group by year([FinishDate])", RadComboBoxPackageYear.SelectedValue));


//                dt = CommonDAL.GetDataTableDB("DBRYPX"
//                    , string.Format(@"select '{0}-' + t.时间 as '时间'
//                                            ,isnull(d.二级建造师,0) '二级建造师'
//                                            ,isnull(d.二级造价工程师,0) '二级造价工程师'
//                                            ,isnull(d.安全生产考核三类人员,0) '安全生产考核三类人员'
//                                            ,isnull(d.建筑施工特种作业,0) '建筑施工特种作业'
//                                            ,isnull(d.关键岗位专业技术管理人员,0) '关键岗位专业技术管理人员'
//                                            ,isnull(d.建设职业技能岗位,0) '建设职业技能岗位'
//                                            ,isnull(d.小计,0) '小计'
//                                    from
//                                    (
//	                                    select '01' as '时间'
//                                            union    select '02'
//                                            union    select '03'
//                                            union    select '04'
//                                            union    select '05'
//                                            union    select '06'
//                                            union    select '07'
//                                            union    select '08'
//                                            union    select '09'
//                                            union    select '10'
//                                            union    select '11'
//                                            union    select '12'
//                                    ) t
//                                    left join
//                                    (
//	                                    select convert(varchar(7),s.[FinishDate],21) '时间'
//	                                        ,sum(case when p.PostTypeName='二级建造师' then 1 else 0 end) as '二级建造师'
//	                                        ,sum(case when p.PostTypeName='二级造价工程师' then 1 else 0 end) as '二级造价工程师'
//	                                        ,sum(case when p.PostTypeName='安全生产考核三类人员' then 1 else 0 end) as '安全生产考核三类人员'
//	                                        ,sum(case when p.PostTypeName='建筑施工特种作业' then 1 else 0 end) as '建筑施工特种作业'
//	                                        ,sum(case when p.PostTypeName='关键岗位专业技术管理人员' then 1 else 0 end) as '关键岗位专业技术管理人员'
//	                                        ,sum(case when p.PostTypeName='建设职业技能岗位' then 1 else 0 end) as '建设职业技能岗位'
//	                                        ,count(*) 小计
//	                                    FROM [dbo].[StudyPlan] s
//	                                    inner join [dbo].[Package] p on s.[PackageID]=p.[PackageID]
//	                                    where s.[FinishDate] between '{0}-01-01' and '{0}-12-31 23:59:59'
//	                                    group by convert(varchar(7),s.[FinishDate],21)
//                                    ) d on t.时间 = right(d.时间,2)
//                                    union all
//                                    select '合计'
//                                        ,sum(case when p.PostTypeName='二级建造师' then 1 else 0 end) as '二级建造师'
//                                        ,sum(case when p.PostTypeName='二级造价工程师' then 1 else 0 end) as '二级造价工程师'
//                                        ,sum(case when p.PostTypeName='安全生产考核三类人员' then 1 else 0 end) as '安全生产考核三类人员'
//                                        ,sum(case when p.PostTypeName='建筑施工特种作业' then 1 else 0 end) as '建筑施工特种作业'
//                                        ,sum(case when p.PostTypeName='关键岗位专业技术管理人员' then 1 else 0 end) as '关键岗位专业技术管理人员'
//                                        ,sum(case when p.PostTypeName='建设职业技能岗位' then 1 else 0 end) as '建设职业技能岗位'
//                                        ,count(*) 小计
//                                    FROM [dbo].[StudyPlan] s
//                                    inner join [dbo].[Package] p on s.[PackageID]=p.[PackageID]
//                                    where  s.[FinishDate] between '{0}-01-01' and '{0}-12-31 23:59:59'
//                                    group by year(s.[FinishDate])", RadComboBoxPackageYear.SelectedValue));
            }

            RadGridStudyPlan.DataSource = dt;
            RadGridStudyPlan.DataBind();
        }

        protected void RadComboBoxPackageYear_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindTj();
        }

        protected void ButtonOutput_Click(object sender, EventArgs e)
        {
            if (RadGridStudyPlan.MasterTableView.Items.Count == 0)
            {
                UIHelp.layerAlert(Page, "没有可导出的数据！");
                return;
            }
            RadGridStudyPlan.ExportSettings.ExportOnlyData = true;
            RadGridStudyPlan.ExportSettings.IgnorePaging = false;
            RadGridStudyPlan.ExportSettings.OpenInNewWindow = true;
            RadGridStudyPlan.MasterTableView.ExportToExcel();
        }

        //格式化Excel
        protected void RadGridStudyPlan_ExcelExportCellFormatting(object source, ExcelExportCellFormattingEventArgs e)
        {
            //HeadCell
            GridItem item = e.Cell.Parent as GridItem;
            if (item.ItemIndex == 0 && e.FormattedColumn.UniqueName == "DTime")
            {
                GridTableView gtv = e.Cell.Parent.Parent.Parent as GridTableView;
                GridItem ghi = gtv.GetItems(GridItemType.Header)[0];
                for (int i = 0; i < ghi.Cells.Count; i++)
                {
                    ghi.Cells[i].Style.Add("border-width", "0.1pt");
                    ghi.Cells[i].Style.Add("border-style", "solid");
                    ghi.Cells[i].Style.Add("border-color", "#CCCCCC");
                }
            }
            //Itemcell
            e.Cell.Attributes.Add("align", "center");
            e.Cell.Style.Add("border-width", "0.1pt");
            e.Cell.Style.Add("border-style", "solid");
            e.Cell.Style.Add("border-color", "#CCCCCC");
        }
    }
}