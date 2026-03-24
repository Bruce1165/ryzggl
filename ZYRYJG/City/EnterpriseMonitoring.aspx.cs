using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAccess;
using System.Text;

namespace ZYRYJG.City
{
    //企业监控
    public partial class EnterpriseMonitoring : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                try
                {
                    BindJZS();
                    BindAQXK();
                    BindRYLSV();
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "全市人员生命周期，企业监控数据获取失败", ex);
                }
            }
        }

        /// <summary>
        /// 绑定企业资质预警
        /// </summary>
        protected void BindJZS()
        {
            string sql = "";
            if (Region == "全市")//全市
            {
                sql = @"select [Region], DataName,DataValue  from [dbo].[TJ_ZYRYCount] 	
                        where [TJDate] =(select max([TJDate])  from [dbo].[TJ_ZYRYCount] where [TJDate] <999999)
                        and [Region]='{0}' and [DataName] = '企业资质预警' 
                        union 
                        select '全市','施工企业数量', count(*)
                        FROM [dbo].[jcsjk_QY_JBXX]";
            }
            else
            {
                sql = @"select [Region], DataName,DataValue  from [dbo].[TJ_ZYRYCount] 	
                        where [TJDate] =(select max([TJDate])  from [dbo].[TJ_ZYRYCount] where [TJDate] <999999)
                        and [Region] like '%{0}%' 
                        and [DataName] = '企业资质预警' 
                        union  
                        select q.[RegionName],'施工企业数量',d.sl
                        from
                        (
	                          select [XZDQBM],count(*) sl
	                          FROM [dbo].[jcsjk_QY_JBXX]
	                          group by [XZDQBM]
                         ) d
                        inner join [dbo].[Dict_Region] q
                        on d.[XZDQBM]=q.[RegionCode]
                        where q.[RegionName] like '%{0}%'";
            }           

           DataTable dt = CommonDAL.GetDataTable(string.Format(sql,  updateregion(Region)));

            foreach (DataRow r in dt.Rows)
            {
                if (r["DataName"].ToString() == "企业资质预警")
                {
                    ViewState["企业资质预警"] = r["datavalue"];
                }
                if (r["DataName"].ToString() == "施工企业数量")
                {
                    ViewState["施工企业数量"] = r["datavalue"];
                }
            }
            ViewState["已达标企业数量"] = Convert.ToInt32(ViewState["施工企业数量"]) - Convert.ToInt32(ViewState["企业资质预警"]);

        }

        /// <summary>
        /// 绑定企业安全许可预警
        /// </summary>
        protected void BindAQXK()
        {
            string sql = "";
            if (Region == "全市")//全市
            {
                sql = @"select [Region], DataName,DataValue  from [dbo].[TJ_ZYRYCount] 	
                        where [TJDate] =(select max([TJDate])  from [dbo].[TJ_ZYRYCount] where [TJDate] <999999)
                        and [Region]='{0}' and [DataName] = '安全许可预警'";
            }
            else
            {
                sql = @"select [Region], DataName,DataValue  from [dbo].[TJ_ZYRYCount] 	
                        where [TJDate] =(select max([TJDate])  from [dbo].[TJ_ZYRYCount] where [TJDate] <999999)
                        and [Region] like '%{0}%' 
                        and [DataName] = '安全许可预警'";
            }

            DataTable dt = CommonDAL.GetDataTable(string.Format(sql, updateregion(Region)));

            foreach (DataRow r in dt.Rows)
            {
                if (r["DataName"].ToString() == "安全许可预警")
                {
                    ViewState["安全许可预警"] = r["datavalue"];
                }               
            }
            ViewState["安全许可已达标企业数量"] = Convert.ToInt32(ViewState["施工企业数量"]) - Convert.ToInt32(ViewState["安全许可预警"]);

        }

        /// <summary>
        /// 人员主动流失率（变更单位、注销，但不包含过期）
        /// </summary>
        protected void BindRYLSV()
        {
            string sql = "";
            if (Region == "全市")//全市
            {
                sql = @"select count(*)
                          FROM [dbo].[jcsjk_QY_JBXX] j
                          left join 
                          (
	                          select ENT_Name_from,count(*) LostCount from jcsjk_jzs_bgqy 
	                          where len(ENT_Name_from) >0
	                          group by ENT_Name_from
                          ) c on j.[QYMC] = c.ENT_Name_from
                          left join 
                          (
	                          select ENT_Name_to,count(*) GetCount from jcsjk_jzs_bgqy 
	                          where len(ENT_Name_to) >0
	                          group by ENT_Name_to
                          ) r on j.[QYMC] = r.ENT_Name_to
                          left join [dbo].[jcsjk_tj_qy_jzs] d on j.[QYMC] = d.[QYMC]
                          where case when isnull(r.GetCount,0)=0 then 0 else  isnull(c.LostCount,0) * 100 /isnull(r.GetCount,0) end  >50";
            }
            else
            {
                sql = @"select count(*)
                          FROM [dbo].[jcsjk_QY_JBXX] j
                          left join 
                          (
	                          select ENT_Name_from,count(*) LostCount from jcsjk_jzs_bgqy 
	                          where len(ENT_Name_from) >0
	                          group by ENT_Name_from
                          ) c on j.[QYMC] = c.ENT_Name_from
                          left join 
                          (
	                          select ENT_Name_to,count(*) GetCount from jcsjk_jzs_bgqy 
	                          where len(ENT_Name_to) >0
	                          group by ENT_Name_to
                          ) r on j.[QYMC] = r.ENT_Name_to
                          left join [dbo].[jcsjk_tj_qy_jzs] d on j.[QYMC] = d.[QYMC]
                          
                        inner join [dbo].[Dict_Region] q
                        on j.[XZDQBM]=q.[RegionCode]
                        where case when isnull(r.GetCount,0)=0 then 0 else  isnull(c.LostCount,0) * 100 /isnull(r.GetCount,0) end  >50
                        and q.[RegionName] like '%{0}%'";
            }
            DataTable dt = CommonDAL.GetDataTable(string.Format(sql, updateregion(Region)));
            ViewState["建造师流失率超50%的企业数量"] = dt.Rows[0][0];
            ViewState["建造师流失率低于50%的企业数量"] = Convert.ToInt32(ViewState["施工企业数量"]) - Convert.ToInt32(ViewState["建造师流失率超50%的企业数量"]);
            //            //详细
            //            @"select isnull(d.[总量],0) CurCount
            //	                          ,isnull(c.LostCount,0) LostCount
            //	                          ,isnull(r.GetCount,0) GetCount
            //	                          ,case when isnull(r.GetCount,0)=0 then 0 else  isnull(c.LostCount,0) * 100 /isnull(r.GetCount,0) end as LSV
            //	                          ,j.*
            //                          FROM [dbo].[jcsjk_QY_JBXX] j
            //                          left join 
            //                          (
            //	                          select ENT_Name_from,count(*) LostCount from jcsjk_jzs_bgqy 
            //	                          where len(ENT_Name_from) >0
            //	                          group by ENT_Name_from
            //                          ) c on j.[QYMC] = c.ENT_Name_from
            //                          left join 
            //                          (
            //	                          select ENT_Name_to,count(*) GetCount from jcsjk_jzs_bgqy 
            //	                          where len(ENT_Name_to) >0
            //	                          group by ENT_Name_to
            //                          ) r on j.[QYMC] = r.ENT_Name_to
            //                          left join [dbo].[jcsjk_tj_qy_jzs] d on j.[QYMC] = d.[QYMC]";
        }

////查看企业资质未达标明细
//select top 10 j.[QYMC],z.[ZZLB],z.[ZZXL],z.[ZZDJ],r.[标准要求]
//,r.* --资质标准要求数量
//,s.* --实际建造师数量
//from [dbo].[jcsjk_QY_JBXX] j 
//inner join jcsjk_QY_ZZZSXXXX z on j.zzjgdm = z.zzjgdm
//left join [dbo].[Dict_QYZZBZ_JZS] r 
//on z.[ZZXL] = r.[资质序列]  and z.[ZZLB] like r.[资质专业]+'%' and z.[ZZDJ] like r.[资质等级] +'%'
//left join dbo.jcsjk_tj_qy_jzs s on  j.[QYMC] = s.QYMC
//where (z.[ZZXL] = '施工总承包' or z.[ZZXL] = '专业承包') 
//and
//(
//        (s.[总量]     < r.[总量]  )
//     or (s.[市政]     < r.[市政]  )
//     or (s.[市政一级] < r.[市政一级]  )
//     or (s.[市政二级] < r.[市政二级]  )
//     or (s.[矿业]     < r.[矿业]  )
//     or (s.[矿业一级] < r.[矿业一级]  )
//     or (s.[矿业二级] < r.[矿业二级]  )
//     or (s.[建筑]     < r.[建筑]  )
//     or (s.[建筑一级] < r.[建筑一级]  )
//     or (s.[建筑二级] < r.[建筑二级]  )
//     or (s.[民航]     < r.[民航]  )
//     or (s.[民航一级] < r.[民航一级]  )
//     or (s.[民航二级] < r.[民航二级]  )
//     or (s.[机电]     < r.[机电]  )
//     or (s.[机电一级] < r.[机电一级]  )
//     or (s.[机电二级] < r.[机电二级]  )
//     or (s.[水利]     < r.[水利]  )
//     or (s.[水利一级] < r.[水利一级]  )
//     or (s.[水利二级] < r.[水利二级]  )
//     or (s.[铁路]     < r.[铁路]  )
//     or (s.[铁路一级] < r.[铁路一级]  )
//     or (s.[铁路二级] < r.[铁路二级]  )
//     or (s.[港航]     < r.[港航]  )
//     or (s.[港航一级] < r.[港航一级]  )
//     or (s.[港航二级] < r.[港航二级]  )
//     or (s.[通信]     < r.[通信]  )
//     or (s.[通信一级] < r.[通信一级]  )
//     or (s.[通信二级] < r.[通信二级]  )
//     or (s.[公路]     < r.[公路]  )
//     or (s.[公路一级] < r.[公路一级]  )
//     or (s.[公路二级] < r.[公路二级]  )
//)
    }
}