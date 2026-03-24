using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;
using System.Data;

namespace ZYRYJG.Unit
{
    public partial class YJZJSDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                COC_ONE_Person_BaseInfoMDL o = COC_ONE_Person_BaseInfoDAL.GetObject(Utility.Cryptography.Decrypt(Request["o"]));
                if (o != null)
                {
                    UIHelp.SetData(EditTable, o, true);
                    switch (o.PSN_RegisteType)
                    {
                        case "01":
                            TextBoxPSN_RegisteType.Text = "初始注册";
                            break;
                        case "02":
                            TextBoxPSN_RegisteType.Text = "变更注册";
                            break;
                        case "03":
                            TextBoxPSN_RegisteType.Text = "延续注册";
                            break;
                        case "04":
                            TextBoxPSN_RegisteType.Text = "增项注册";
                            break;
                        case "05":
                            TextBoxPSN_RegisteType.Text = "重新注册";
                            break;
                        case "06":
                            TextBoxPSN_RegisteType.Text = "遗失补办";
                            break;
                        case "07":
                            TextBoxPSN_RegisteType.Text = "注销";
                            break;
                    }
                    TextBoxPSN_CertificationDate.Text =((DateTime)o.PSN_CertificationDate).ToString("yyyy-MM-dd");
                    TextBoxPSN_CertificateValidity.Text = ((DateTime)o.PSN_CertificateValidity).ToString("yyyy-MM-dd");
                    TextBoxPSN_RegistePermissionDate.Text = ((DateTime)o.PSN_RegistePermissionDate).ToString("yyyy-MM-dd");

                    ImgCode.Src = UIHelp.ShowFaceImage(o.PSN_CertificateNO);

                    //绑定工程信息
                    string sql = @"
                    select r.sfzh,r.xm,s.SGDW,g.GCBM,g.GCJSDD,g.GCMC,g.HTKGRQ,g.HTJGRQ from 
                    (
	                    --工程合同项目经理（立项）
	                    select g.gcbm ,h.[ProjectManager] xm,h.[ProjectManagerCardNo] sfzh
	                    from [dbo].[jcsjk_HT_SG] h	
	                    inner join [dbo].[jcsjk_GC_GCXX_NEW] g on h.ProjectSubID = g.[HTBH]
	                    where h.valid=1 and g.valid=1
	                    except 
	                    --施工项目经理
	                    select distinct s.gcbm,s.[SGDWXMFZR] xm,s.[SGDWXMFZRSFZH] sfzh
	                    FROM [dbo].[jcsjk_GC_WFZT_SGDW] s
	                    left join [dbo].[jcsjk_GC_GCXX_NEW] g on s.GCBM = g.GCBM
	                    where len(s.[SGDWXMFZRSFZH])>0 
                    ) r
                    left join jcsjk_GC_GCXX_NEW g
                    on r.gcbm = g.gcbm
                    left join [jcsjk_GC_WFZT_SGDW] s
                    on r.gcbm = s.gcbm
                    where r.sfzh='{0}'
                    order by g.HTKGRQ";

                    DataTable dt = CommonDAL.GetDataTable(string.Format(sql, o.PSN_CertificateNO));
                    RadGridGC.DataSource = dt;
                    RadGridGC.DataBind();

                    //绑定注册历史
                    sql = @"select  distinct p.PSN_RegistePermissionDate,
                                    p.PSN_Name,
                                    p.PSN_CertificateNO,
                                    p.PSN_RegisteProfession,
                                    p.PSN_RegisterNO,e.ENT_Name,
                                    CASE p.PSN_RegisteType 
                                    WHEN '01' THEN '初始注册'
                                    WHEN '02' THEN '变更注册'
                                    WHEN '03' THEN '延期注册'
                                    WHEN '04' THEN '增项注册'
                                    WHEN '05' THEN '重新注册'
                                    WHEN '06' THEN '遗失补办'
                                    WHEN '07' THEN '注销'
                                    ELSE '' END AS PSN_RegisteTypeName
                            from [COC_ONE_Person_BaseInfo]  p
                                left join [dbo].[COC_ONE_ENT_BaseInfo] e
                                on p.[ENT_ServerID] = e.[ENT_ServerID]
                                    and e.[EndTime] = (select min([EndTime]) 
                                                        from [dbo].[COC_ONE_ENT_BaseInfo] 
		                                                where [ENT_ServerID]=p.[ENT_ServerID] 
                                                        and  dateadd(day,1,p.PSN_RegistePermissionDate) between [BeginTime] and [EndTime]
                                                       )
                            where p.[PSN_RegisterNO]='{0}' 
                            order by PSN_RegistePermissionDate desc";

                    dt = CommonDAL.GetDataTable(string.Format(sql, o.PSN_RegisterNO));
                    RadGridHis.DataSource = dt;
                    RadGridHis.DataBind();
                }
            }
        }
    }
}