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
    public partial class TjStudyCount : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string dTime = Request["o"];
                BindTj(dTime);
            }
        }

        //绑定统计结果
        protected void BindTj(string dTime)
        {
            try
            {
                DataTable dt = null;

//                if (string.IsNullOrEmpty( dTime) == true)//全部，按年份统计
//                {
//                    dt = CommonDAL.GetDataTableDB("DBRYPX"
//                    , @"select year([LearnTime]) as DTime,
//                                clickCount =isnull((SELECT sum([TJ_Count]) FROM [dbo].[TJ_Visit] where [TJ_Year] like cast(year([FinishSourceWare].[LearnTime]) as varchar(4)) +'____'),count(distinct WorkerCertificateCode)),
//                                count(distinct WorkerCertificateCode) ManCount,
//                                sum([FinishPeriod]) as FinishPeriod,
//                                sum(case when [StudyStatus]=1 then [FinishPeriod] else 0 end) as TestPeriod
//                        FROM [dbo].[FinishSourceWare] 
//                        group by year([LearnTime])
//                        order by DTime desc");
//                }
//                else if(dTime.Length ==4)//选中年度，按月份统计
//                {
//                    dt = CommonDAL.GetDataTableDB("DBRYPX"
//                        , string.Format(@"select Convert(varchar(7),[LearnTime],120) as DTime, 
//                                                clickCount =isnull((SELECT sum([TJ_Count]) FROM [dbo].[TJ_Visit] where [TJ_Year] like replace(Convert(varchar(7),[FinishSourceWare].[LearnTime],120),'-','') +'__'),count(distinct WorkerCertificateCode)),
//                                                count(distinct WorkerCertificateCode) ManCount,
//                                                sum([FinishPeriod]) as FinishPeriod,
//                                                sum(case when [StudyStatus]=1 then [FinishPeriod] else 0 end) as TestPeriod
//                                    FROM [dbo].[FinishSourceWare] 
//                                    where [LearnTime] >='{0}-01-01' and [LearnTime] < '{1}-01-01'
//                                    group by Convert(varchar(7),[LearnTime],120)
//                                    order by DTime desc", dTime, Convert.ToInt32(dTime) + 1));
//                }
//                else if (dTime.Length == 7)//选中月度，按日统计
//                {
//                    dt = CommonDAL.GetDataTableDB("DBRYPX"
//                        , string.Format(@"select Convert(varchar(10),[LearnTime],120) as DTime,
//                                                clickCount =isnull((SELECT sum([TJ_Count]) FROM [dbo].[TJ_Visit] where [TJ_Year] like replace(Convert(varchar(10),[FinishSourceWare].[LearnTime],120),'-','')), count(distinct WorkerCertificateCode)) ,
//                                                count(distinct WorkerCertificateCode) ManCount,
//                                                sum([FinishPeriod]) as FinishPeriod,
//                                                sum(case when [StudyStatus]=1 then [FinishPeriod] else 0 end) as TestPeriod
//                                    FROM [dbo].[FinishSourceWare] 
//                                    where [LearnTime] >='{0}-01' and [LearnTime] < '{1}'
//                                    group by Convert(varchar(10),[LearnTime],120)
//                                    order by DTime desc", dTime, Convert.ToDateTime(dTime +"-01").AddMonths(1).ToString("yyyy-MM-dd")));
//                }

                if (string.IsNullOrEmpty(dTime) == true)//全部，按年份统计
                {
                    dt = CommonDAL.GetDataTableDB("DBRYPX"
                    , @"select t1.DTime,t1.clickCount,isnull(t2.ManCount,0) as ManCount,isnull(t2.FinishPeriod,0) as FinishPeriod,isnull(t2.TestPeriod,0) as TestPeriod
                        from
                        (
	                        SELECT left([TJ_Year],4) as DTime,
			                        sum([TJ_Count]) as clickCount
	                        FROM [RYPX].[dbo].[TJ_Visit] 
	                        where [TJ_Year] >9999
	                        group by left([TJ_Year],4)
                        ) t1
                        left join 
                        (
	                        select year([LearnTime]) as DTime,
			                        count(distinct WorkerCertificateCode) ManCount,
			                        sum(CONVERT(bigint, [FinishPeriod])) as FinishPeriod,
			                        sum(CONVERT(bigint, case when [StudyStatus]=1 then [FinishPeriod] else 0 end)) as TestPeriod
	                        FROM [RYPX].[dbo].[FinishSourceWare] 
	                        group by year([LearnTime])
                        ) t2 on  t1.DTime =t2.DTime
                        order by  t1.DTime desc");
                }
                else if (dTime.Length == 4)//选中年度，按月份统计
                {
                    dt = CommonDAL.GetDataTableDB("DBRYPX"
                        , string.Format(@"select t1.DTime,t1.clickCount,isnull(t2.ManCount,0) as ManCount,isnull(t2.FinishPeriod,0) as FinishPeriod,isnull(t2.TestPeriod,0) as TestPeriod
                                            from
                                            (
	                                            SELECT left([TJ_Year],4) +'-' + right(left([TJ_Year],6),2) as DTime,
			                                            sum([TJ_Count]) as clickCount
	                                            FROM [dbo].[TJ_Visit] 
	                                            where [TJ_Year] like '{0}____'
	                                            group by left([TJ_Year],4) +'-' + right(left([TJ_Year],6),2)
                                            ) t1
                                            left join 
                                            (
	                                            select Convert(varchar(7),[LearnTime],120) as DTime,
			                                            count(distinct WorkerCertificateCode) ManCount,
			                                            sum(CONVERT(bigint,[FinishPeriod])) as FinishPeriod,
			                                            sum(CONVERT(bigint,case when [StudyStatus]=1 then [FinishPeriod] else 0 end)) as TestPeriod
	                                            FROM [dbo].[FinishSourceWare] 
	                                            where [LearnTime] >='{0}-01-01' and [LearnTime] < '{1}-01-01'
	                                            group by Convert(varchar(7),[LearnTime],120)
                                            ) t2 on  t1.DTime =t2.DTime
                                            order by  t1.DTime desc", dTime, Convert.ToInt32(dTime) + 1));
                }
                else if (dTime.Length == 7)//选中月度，按日统计
                {
                    dt = CommonDAL.GetDataTableDB("DBRYPX"
                        , string.Format(@"select t1.DTime,t1.clickCount,isnull(t2.ManCount,0) as ManCount,isnull(t2.FinishPeriod,0) as FinishPeriod,isnull(t2.TestPeriod,0) as TestPeriod
                                            from
                                            (
	                                            SELECT left([TJ_Year],4) +'-' + right(left([TJ_Year],6),2)+'-'+right([TJ_Year],2) as DTime,
			                                            sum([TJ_Count]) as clickCount
	                                            FROM [dbo].[TJ_Visit] 
	                                            where [TJ_Year] like '{0}__'
	                                            group by left([TJ_Year],4) +'-' + right(left([TJ_Year],6),2)+'-'+right([TJ_Year],2)
                                            ) t1
                                            left join 
                                            (
	                                            select Convert(varchar(10),[LearnTime],120) as DTime,
			                                            count(distinct WorkerCertificateCode) ManCount,
			                                            sum(CONVERT(bigint,[FinishPeriod])) as FinishPeriod,
			                                            sum(CONVERT(bigint,case when [StudyStatus]=1 then [FinishPeriod] else 0 end)) as TestPeriod
	                                            FROM [dbo].[FinishSourceWare] 
	                                            where [LearnTime] >='{1}-01' and [LearnTime] < '{2}'
	                                            group by Convert(varchar(10),[LearnTime],120)
                                            ) t2 on  t1.DTime =t2.DTime
                                            order by  t1.DTime desc", dTime.Replace("-",""),dTime, Convert.ToDateTime(dTime + "-01").AddMonths(1).ToString("yyyy-MM-dd")));
                }

                RadGridStudyPlan.DataSource = dt;
                RadGridStudyPlan.DataBind();
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "统计公益培训人数失败！", ex);
                return;
            }
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

        /// <summary>
        /// 格式化显示学习时间
        /// </summary>
        /// <param name="dtime">学习时间</param>
        /// <returns></returns>
        protected string FormatDtime(string dtime)
        {
            System.Text.StringBuilder sb = new  System.Text.StringBuilder();
            string[] list = dtime.Split('-');
            for(int i=0;i<list.Length;i++)
            {
                if(i==0)
                {
                    sb.Append(list[i]).Append("年");
                }
                else if (i == 1)
                {
                    sb.Append(list[i]).Append("月");
                }
                else if (i == 2)
                {
                    sb.Append(list[i]).Append("日");
                }
            }
            return sb.ToString();
        }

        //后退上一层
        protected string up(object dtime)
        {
            switch (dtime.ToString().Length)
            {
                case 10:
                    return dtime.ToString().Substring(0, 4);
                case 7:
                    return "";
                default:
                    return "";
            }           
        }
    }
}