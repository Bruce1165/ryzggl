using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAccess;
using Model;
using System.Text;

namespace ZYRYJG
{
    //企业监控详细
    public partial class QYDetai : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "City/EnterpriseMonitoring.aspx";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                bindQYDetail();

                BindZZDetail();
            }
        }

        /// <summary>
        /// 绑定企业基本信息
        /// </summary>
        protected void bindQYDetail()
        {
            try
            {
                string sql = @"select top 1 * from  [dbo].[jcsjk_tj_qy_zzwdb] where ZZJGDM ='{0}'";
                DataTable dt = CommonDAL.GetDataTable(string.Format(sql, Request["o"]));
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["QYMC"] != null) LabelQYMC.Text = dt.Rows[0]["QYMC"].ToString();
                    if (dt.Rows[0]["ZZJGDM"] != null) LabelZZJGDM.Text = dt.Rows[0]["ZZJGDM"].ToString();

                    if (dt.Rows[0]["FDDBR"] != null) LabelFDDBR.Text = dt.Rows[0]["FDDBR"].ToString();
                    if (dt.Rows[0]["LXDH"] != null) LabelLXDH.Text = dt.Rows[0]["LXDH"].ToString();
                    if (dt.Rows[0]["RegionName"] != null) LabelRegionName.Text = dt.Rows[0]["RegionName"].ToString();
                    if (dt.Rows[0]["ZCDZ"] != null) LabelZCDZ.Text = dt.Rows[0]["ZCDZ"].ToString();
                    if (dt.Rows[0]["ZCZJ"] != null) LabelZCZJ.Text = dt.Rows[0]["ZCZJ"].ToString();
                    if (dt.Rows[0]["ZCBZ"] != null) LabelZCBZ.Text = dt.Rows[0]["ZCBZ"].ToString();
                   
                }

            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "获取企业详细信息失败。", ex);
            }
        }

        /// <summary>
        /// 绑定资质达标情况
        /// </summary>
        protected void BindZZDetail()
        {
            string sql = @"select  j.[ZZJGDM],z.[ZZLB],z.[ZZXL],z.[ZZDJ],replace(replace(r.[标准要求],'，','，<br />'),'。','。<br />') BZ,
                            case when (r.[总量] >0 ) then '总数量：' + cast(s.[总量] as varchar(10))+ '人，'+ case when s.[总量]  < r.[总量] then '小于' else '达到' end + cast(r.[总量] as varchar(10))+ '人' else '' end 
                            + case when (r.[市政] >0 ) then '<br />市政数量：' + cast(s.[市政] as varchar(10))+ '人，'+ case when s.[市政]  < r.[市政] then '小于' else '达到' end + cast(r.[市政] as varchar(10))+ '人' else '' end
                            + case when (r.[市政一级] >0 ) then '<br />市政一级量：' + cast(s.[市政一级] as varchar(10))+ '人，'+ case when s.[市政一级]  < r.[市政一级] then '小于' else '达到' end + cast(r.[市政一级] as varchar(10))+ '人' else '' end
                            + case when (r.[市政二级] >0 ) then '<br />市政二级数量：' + cast(s.[市政二级] as varchar(10))+ '人，'+ case when s.[市政二级]  < r.[市政二级] then '小于' else '达到' end + cast(r.[市政二级] as varchar(10))+ '人' else '' end
                            + case when (r.[矿业] >0 ) then '<br />矿业数量：' + cast(s.[矿业] as varchar(10))+ '人，'+ case when s.[矿业]  < r.[矿业] then '小于' else '达到' end + cast(r.[矿业] as varchar(10))+ '人' else '' end
                            + case when (r.[矿业一级] >0 ) then '<br />矿业一级数量：' + cast(s.[矿业一级] as varchar(10))+ '人，'+ case when s.[矿业一级]  < r.[矿业一级] then '小于' else '达到' end + cast(r.[矿业一级] as varchar(10))+ '人' else '' end
                            + case when (r.[矿业二级] >0 ) then '<br />矿业二级数量：' + cast(s.[矿业二级] as varchar(10))+ '人，'+ case when s.[矿业二级]  < r.[矿业二级] then '小于' else '达到' end + cast(r.[矿业二级] as varchar(10))+ '人' else '' end
                            + case when (r.[建筑] >0 ) then '<br />建筑数量：' + cast(s.[建筑] as varchar(10))+ '人，'+ case when s.[建筑]  < r.[建筑] then '小于' else '达到' end + cast(r.[建筑] as varchar(10))+ '人' else '' end
                            + case when (r.[建筑一级] >0 ) then '<br />建筑一级数量：' + cast(s.[建筑一级] as varchar(10))+ '人，'+ case when s.[建筑一级]  < r.[建筑一级] then '小于' else '达到' end + cast(r.[建筑一级] as varchar(10))+ '人' else '' end
                            + case when (r.[建筑二级] >0 ) then '<br />建筑二级数量：' + cast(s.[建筑二级] as varchar(10))+ '人，'+ case when s.[建筑二级]  < r.[建筑二级] then '小于' else '达到' end + cast(r.[建筑二级] as varchar(10))+ '人' else '' end
                            + case when (r.[民航] >0 ) then '<br />民航数量：' + cast(s.[民航] as varchar(10))+ '人，'+ case when s.[民航]  < r.[民航] then '小于' else '达到' end + cast(r.[民航] as varchar(10))+ '人' else '' end
                            + case when (r.[民航一级] >0 ) then '<br />民航一级数量：' + cast(s.[民航一级] as varchar(10))+ '人，'+ case when s.[民航一级]  < r.[民航一级] then '小于' else '达到' end + cast(r.[民航一级] as varchar(10))+ '人' else '' end
                            + case when (r.[民航二级] >0 ) then '<br />民航二级数量：' + cast(s.[民航二级] as varchar(10))+ '人，'+ case when s.[民航二级]  < r.[民航二级] then '小于' else '达到' end + cast(r.[民航二级] as varchar(10))+ '人' else '' end
                            + case when (r.[机电] >0 ) then '<br />机电数量：' + cast(s.[机电] as varchar(10))+ '人，'+ case when s.[机电]  < r.[机电] then '小于' else '达到' end + cast(r.[机电] as varchar(10))+ '人' else '' end
                            + case when (r.[机电一级] >0 ) then '<br />机电一级数量：' + cast(s.[机电一级] as varchar(10))+ '人，'+ case when s.[机电一级]  < r.[机电一级] then '小于' else '达到' end + cast(r.[机电一级] as varchar(10))+ '人' else '' end
                            + case when (r.[机电二级] >0 ) then '<br />机电二级数量：' + cast(s.[机电二级] as varchar(10))+ '人，'+ case when s.[机电二级]  < r.[机电二级] then '小于' else '达到' end + cast(r.[机电二级] as varchar(10))+ '人' else '' end
                            + case when (r.[水利] >0 ) then '<br />水利数量：' + cast(s.[水利] as varchar(10))+ '人，'+ case when s.[水利]  < r.[水利] then '小于' else '达到' end + cast(r.[水利] as varchar(10))+ '人' else '' end
                            + case when (r.[水利一级] >0 ) then '<br />水利一级数量：' + cast(s.[水利一级] as varchar(10))+ '人，'+ case when s.[水利一级]  < r.[水利一级] then '小于' else '达到' end + cast(r.[水利一级] as varchar(10))+ '人' else '' end
                            + case when (r.[水利二级] >0 ) then '<br />水利二级数量：' + cast(s.[水利二级] as varchar(10))+ '人，'+ case when s.[水利二级]  < r.[水利二级] then '小于' else '达到' end + cast(r.[水利二级] as varchar(10))+ '人' else '' end
                            + case when (r.[铁路] >0 ) then '<br />铁路数量：' + cast(s.[铁路] as varchar(10))+ '人，'+ case when s.[铁路]  < r.[铁路] then '小于' else '达到' end + cast(r.[铁路] as varchar(10))+ '人' else '' end
                            + case when (r.[铁路一级] >0 ) then '<br />铁路一级数量：' + cast(s.[铁路一级] as varchar(10))+ '人，'+ case when s.[铁路一级]  < r.[铁路一级] then '小于' else '达到' end + cast(r.[铁路一级] as varchar(10))+ '人' else '' end
                            + case when (r.[铁路二级] >0 ) then '<br />铁路二级数量：' + cast(s.[铁路二级] as varchar(10))+ '人，'+ case when s.[铁路二级]  < r.[铁路二级] then '小于' else '达到' end + cast(r.[铁路二级] as varchar(10))+ '人' else '' end
                            + case when (r.[港航] >0 ) then '<br />港航数量：' + cast(s.[港航] as varchar(10))+ '人，'+ case when s.[港航]  < r.[港航] then '小于' else '达到' end + cast(r.[港航] as varchar(10))+ '人' else '' end
                            + case when (r.[港航一级] >0 ) then '<br />港航一级数量：' + cast(s.[港航一级] as varchar(10))+ '人，'+ case when s.[港航一级]  < r.[港航一级] then '小于' else '达到' end + cast(r.[港航一级] as varchar(10))+ '人' else '' end
                            + case when (r.[港航二级] >0 ) then '<br />港航二级数量：' + cast(s.[港航二级] as varchar(10))+ '人，'+ case when s.[港航二级]  < r.[港航二级] then '小于' else '达到' end + cast(r.[港航二级] as varchar(10))+ '人' else '' end
                            + case when (r.[通信] >0 ) then '<br />通信数量：' + cast(s.[通信] as varchar(10))+ '人，'+ case when s.[通信]  < r.[通信] then '小于' else '达到' end + cast(r.[通信] as varchar(10))+ '人' else '' end
                            + case when (r.[通信一级] >0 ) then '<br />通信一级数量：' + cast(s.[通信一级] as varchar(10))+ '人，'+ case when s.[通信一级]  < r.[通信一级] then '小于' else '达到' end + cast(r.[通信一级] as varchar(10))+ '人' else '' end
                            + case when (r.[通信二级] >0 ) then '<br />通信二级数量：' + cast(s.[通信二级] as varchar(10))+ '人，'+ case when s.[通信二级]  < r.[通信二级] then '小于' else '达到' end + cast(r.[通信二级] as varchar(10))+ '人' else '' end
                            + case when (r.[公路] >0 ) then '<br />公路数量：' + cast(s.[公路] as varchar(10))+ '人，'+ case when s.[公路]  < r.[公路] then '小于' else '达到' end + cast(r.[公路] as varchar(10))+ '人' else '' end
                            + case when (r.[公路一级] >0 ) then '公路一级数量：' + cast(s.[公路一级] as varchar(10))+ '人，'+ case when s.[公路一级]  < r.[公路一级] then '小于' else '达到' end + cast(r.[公路一级] as varchar(10))+ '人' else '' end
                            + case when (r.[公路二级] >0 ) then '<br />公路二级数量：' + cast(s.[公路二级] as varchar(10))+ '人，'+ case when s.[公路二级]  < r.[公路二级] then '小于' else '达到' end + cast(r.[公路二级] as varchar(10))+ '人' else '' end
                                as Detail
                            from [dbo].[jcsjk_QY_JBXX] j 
                            inner join jcsjk_QY_ZZZSXXXX z on j.zzjgdm = z.zzjgdm
                            left join [dbo].[Dict_QYZZBZ_JZS] r 
                            on z.[ZZXL] = r.[资质序列]  and z.[ZZLB] like r.[资质专业]+'%' and z.[ZZDJ] like r.[资质等级] +'%'
                            left join dbo.jcsjk_tj_qy_jzs s on  j.[QYMC] = s.QYMC
                            where j.[ZZJGDM]='{0}' and (z.[ZZXL] = '施工总承包' or z.[ZZXL] = '专业承包') 
                            and
                            (
                                    (s.[总量]     < r.[总量]  )
                                    or (s.[市政]     < r.[市政]  )
                                    or (s.[市政一级] < r.[市政一级]  )
                                    or (s.[市政二级] < r.[市政二级]  )
                                    or (s.[矿业]     < r.[矿业]  )
                                    or (s.[矿业一级] < r.[矿业一级]  )
                                    or (s.[矿业二级] < r.[矿业二级]  )
                                    or (s.[建筑]     < r.[建筑]  )
                                    or (s.[建筑一级] < r.[建筑一级]  )
                                    or (s.[建筑二级] < r.[建筑二级]  )
                                    or (s.[民航]     < r.[民航]  )
                                    or (s.[民航一级] < r.[民航一级]  )
                                    or (s.[民航二级] < r.[民航二级]  )
                                    or (s.[机电]     < r.[机电]  )
                                    or (s.[机电一级] < r.[机电一级]  )
                                    or (s.[机电二级] < r.[机电二级]  )
                                    or (s.[水利]     < r.[水利]  )
                                    or (s.[水利一级] < r.[水利一级]  )
                                    or (s.[水利二级] < r.[水利二级]  )
                                    or (s.[铁路]     < r.[铁路]  )
                                    or (s.[铁路一级] < r.[铁路一级]  )
                                    or (s.[铁路二级] < r.[铁路二级]  )
                                    or (s.[港航]     < r.[港航]  )
                                    or (s.[港航一级] < r.[港航一级]  )
                                    or (s.[港航二级] < r.[港航二级]  )
                                    or (s.[通信]     < r.[通信]  )
                                    or (s.[通信一级] < r.[通信一级]  )
                                    or (s.[通信二级] < r.[通信二级]  )
                                    or (s.[公路]     < r.[公路]  )
                                    or (s.[公路一级] < r.[公路一级]  )
                                    or (s.[公路二级] < r.[公路二级]  )
                            )";
            DataTable dt = CommonDAL.GetDataTable(string.Format(sql, Request["o"]));
         
            RadGridQY.DataSource = dt;
            RadGridQY.DataBind();
        }

    }
}