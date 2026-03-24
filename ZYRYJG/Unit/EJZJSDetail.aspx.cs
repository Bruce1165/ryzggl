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
    public partial class EJZJSDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                COC_TOW_Person_BaseInfoMDL o = COC_TOW_Person_BaseInfoDAL.GetObject(Utility.Cryptography.Decrypt(Request["o"]));
                if (o != null)
                {
                    #region 绑定注册信息

                    UIHelp.SetData(EditTable, o, true);
                    switch(o.PSN_RegisteType)
                    {
                        case"01":
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
                    TextBoxPSN_CertificationDate.Text = ((DateTime)o.PSN_CertificationDate).ToString("yyyy-MM-dd");
                    TextBoxPSN_CertificateValidity.Text = ((DateTime)o.PSN_CertificateValidity).ToString("yyyy-MM-dd");
                    TextBoxPSN_RegistePermissionDate.Text = ((DateTime)o.PSN_RegistePermissionDate).ToString("yyyy-MM-dd");

                    ImgCode.Src = UIHelp.ShowFile(UIHelp.ShowFaceImageJZS(o.PSN_RegisterNO, o.PSN_CertificateNO));

                    RefreshLockStatus(o.PSN_ServerID);

                    #endregion

//                    //绑定注册历史
//                    string sql = @"select *,
//                            CASE PSN_RegisteType 
//                            WHEN '01' THEN '初始注册'
//                            WHEN '02' THEN '变更注册'
//                            WHEN '03' THEN '延期注册'
//                            WHEN '04' THEN '增项注册'
//                            WHEN '05' THEN '重新注册'
//                            WHEN '06' THEN '遗失补办'
//                            WHEN '07' THEN '注销'
//                            ELSE '' END AS PSN_RegisteTypeName
//                            from [COC_TOW_Person_BaseInfo_His] where [PSN_RegisterNO]='{0}' order by PSN_RegistePermissionDate desc";

//                    DataTable dt = CommonDAL.GetDataTable(string.Format(sql, o.PSN_RegisterNO));
//                    RadGridHis.DataSource = dt;
                    //                    RadGridHis.DataBind();


                    #region 绑定注册历史
                    string sql = @"select p.[RegisteHis]
                            from [dbo].[COC_TOW_Person_BaseInfo] b
                            inner join
                            (
	                            select [PSN_ServerID],[PSN_CertificateNO],
			                            [RegisteHis] = replace(replace(replace(replace(replace(replace(replace(replace(
			                            (
				                            select Convert(varchar(11),a.[PSN_RegistePermissionDate],21) a1, a.[ENT_Name] a2,a.[PSN_RegisteType] a3,a.[PSN_RegisteProfession]  +'\r\n' a4 from 
				                            (
					                            select [PSN_ServerID], [PSN_RegisteType],[PSN_RegistePermissionDate],max([ENT_Name]) as ENT_Name,max([PSN_RegisteProfession]) as [PSN_RegisteProfession]
					                            from(
						                            select [PSN_ServerID],r.[MC] as [PSN_RegisteType], Convert(varchar(11),[PSN_RegistePermissionDate],21) [PSN_RegistePermissionDate], ENT_Name,[PSN_RegisteProfession]
						                            FROM [dbo].[COC_TOW_Person_BaseInfo_His] h
						                            inner join [dbo].[Dict_RegisteType] r on  h.[PSN_RegisteType] = r.BM
						                            UNION 
						                            select [PSN_ServerID],[ApplyType] as [PSN_RegisteType],Convert(varchar(11),NoticeDate,21) as [PSN_RegistePermissionDate], ENT_Name,[PSN_RegisteProfession]
						                            FROM [dbo].[Apply]
						                            where NoticeDate >'1950-1-1' and [ConfirmResult]='通过'and 	(([ApplyType]='初始注册' and  [CodeDate]>'1950-1-1') or [ApplyType]<>'初始注册')
						                            ) t
						                            group by [PSN_ServerID],[PSN_RegisteType],[PSN_RegistePermissionDate]
					
				                            ) a
				                            where a.[PSN_ServerID]=t.[PSN_ServerID] order by a.[PSN_ServerID],a.[PSN_RegistePermissionDate] for xml path('') 	
			                            ),'<a1>',''),'</a1>',''),'<a2>','（'),'</a2>','）'),'<a3>',''),'</a3>',''),'<a4>','：'),'</a4>','')
	                            from [COC_TOW_Person_BaseInfo] t 
	                            where PSN_Level='二级'
	                            group by [PSN_ServerID],[PSN_CertificateNO]
                            ) p
                            on b.[PSN_ServerID] = p.[PSN_ServerID]
                            where b.[PSN_CertificateNO] ='{0}'";

                    DataTable dt = CommonDAL.GetDataTable(string.Format(sql, o.PSN_CertificateNO));

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        div_regHis.InnerHtml = dt.Rows[0][0].ToString().Replace("\\r\\n", "<br />");
                    }

                    #endregion

                    #region 绑定工程信息

                    sql = @"
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

                    dt = CommonDAL.GetDataTable(string.Format(sql, o.PSN_CertificateNO));
                    RadGridGC.DataSource = dt;
                    RadGridGC.DataBind();

                    #endregion

                    #region 绑定继续教育信息

                    sql = @"SELECT * FROM jcsjk_ejjxjy WHERE WorkerCertificateCode ='{0}'";
                    dt = CommonDAL.GetDataTable(string.Format(sql, o.PSN_CertificateNO));
                    RadGridJXJY.DataSource = dt;
                    RadGridJXJY.DataBind();

                    #endregion

                    #region 检索历次锁定记录

                    ObjectDataSource1.SelectParameters.Clear();
                    QueryParamOB q = new QueryParamOB();
                    q.Add(string.Format("PSN_ServerID = '{0}'", o.PSN_ServerID));//证书ID
                    ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
                    RadGridLock.CurrentPageIndex = 0;
                    RadGridLock.DataSourceID = ObjectDataSource1.ID;

                    #endregion 检索历次锁定记录

                    #region 绑定最近3个月社保记录

                    sql = @"select * from
                                    (
                                        SELECT	  
	                                        [CertificateCode]
	                                        ,[WorkerName]		  
	                                        ,[CreditCode]
	                                        ,[ENT_Name]
	                                        ,[JFYF]		
	                                        ,[XZName]
	                                        ,[XZCode]
	                                        ,convert(varchar(11),[CJSJ],23) as 'CJSJ'	                                    
                                        FROM [dbo].[SheBao]
                                        where [CertificateCode]='{0}' and [JFYF] between  {1} and  {2}
                                    ) p
                                    PIVOT(MAX([XZName]) FOR [XZCode] IN ([01],[02],[03],[04],[05])) AS T";
                    dt = CommonDAL.GetDataTable(string.Format(sql, o.PSN_CertificateNO                     
                        , DateTime.Now.AddMonths(-3).ToString("yyyyMM")
                        , DateTime.Now.AddMonths(-1).ToString("yyyyMM")));

                    RadGrid1.DataSource = dt;
                    RadGrid1.DataBind();

                    #endregion
                }
            }
        }

        private void RefreshLockStatus(string PSN_ServerID)
        {
            //证书锁定状态           
            bool lockStatus = LockJZSDAL.GetLockStatus(PSN_ServerID);
            if (lockStatus == false)//已解锁
            {
                DivDetail.Visible = false;
            }
            else
            {
                DivDetail.Visible = true;
            }
        }
    }
}