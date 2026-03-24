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
    //项目监控
    public partial class ItemsMonitoring : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                try
                {
                    DataTable dt = null;
                    System.Text.StringBuilder sb = new StringBuilder();
                    //string qx = Region;
                    //switch (qx)
                    //{
                    //    case "北京市住房和城乡建设委员会":
                    //        qx = "全市";
                    //        break;
                    //    case "西城区":
                    //        qx = "西城区宣武区";
                    //        break;
                    //    case "东城区":
                    //        qx = "东城区崇文区";
                    //        break;
                    //}

                    //在施项目执业人员分布
                    #region 在施项目执业人员分布

                    string sql = @"select [TJDate],[Region], DataName,DataValue  from [dbo].[TJ_ZYRYCount] 	
                    where [TJDate] =(select max([TJDate])  from [dbo].[TJ_ZYRYCount] where [TJDate] <999999)
                    and [Region] like '%{0}%' and ([DataName] like '在施一级建造师' or [DataName] like '在施一级临时建造师' or [DataName] like '在施二级建造师' or [DataName] like '在施二级临时建造师' or [DataName] like '在施监理师')
                    order by DataName;";

                    dt = CommonDAL.GetDataTable(string.Format(sql, updateregion(Region)));
                    //[{ value: 3380, name: '建造师' },{ value: 2176, name: '监理师' },{ value: 1089, name: '造价师' }]
                    int PersonCount = 0;
                    foreach (DataRow r in dt.Rows)
                    {
                        sb.Append(string.Format(",[ value: {0}, name: '{1}' ]", r["DataValue"], r["DataName"]));
                        PersonCount += Convert.ToInt32(r["DataValue"]);
                    }
                    if (sb.Length > 0)
                    {
                        sb.Remove(0, 1).Replace("[", "{").Replace("]", "}").Replace("在施", "");
                    }
                    ViewState["person"] = sb.ToString();

                    ViewState["personCount"] = PersonCount;


                    #endregion 全市执业人员分布

                    //在施项目人员证书到期预警
                    #region 在施项目人员证书到期预警

//                    if (Region == "北京市住房和城乡建设委员会")//全市
//                    {
//                        sql = @"select t.dataType DataName,count(*) DataValue
//	                            from dbo.jcsjk_GC_GCXX_NEW g 
//	                            inner join
//	                            (
//		                            select r.gcbm,r.sgdwxmfzr as xm,r.SGDWXMFZRSFZH as sfzh,z.PSN_Level +'建造师' as dataType
//		                            from dbo.jcsjk_GC_WFZT_SGDW r inner join dbo.jcsjk_GC_CP_SGXKZ j on r.gcbm = j.gcbm
//		                            left join [dbo].[jcsjk_jzs] z on r.SGDWXMFZRSFZH = z.[PSN_CertificateNO]
//		                             where r.sgdwxmfzr is not null and j.sgjd='在施' and len(z.[PSN_RegisterNO])>0 and z.[PSN_CertificateValidity] <='{0}'
//		                            and  z.PSN_RegisteType <>'注销' 
//		                             union all
//		                            select r.gcbm,r.JLDWXMFZR,r.JLDWXMZJSFZH,'监理师'
//		                            from dbo.jcsjk_GC_WFZT_JLDW r inner join dbo.jcsjk_GC_CP_SGXKZ j on r.gcbm = j.gcbm
//			                            left join [dbo].[jcsjk_jls] z on r.JLDWXMZJSFZH = z.[证件号]
//		                             where r.JLDWXMZJ is not null and j.sgjd='在施' and len(z.[执业资格证书编号])>0 and z.[注册有效期] <='{0}'		
//	                             ) t
//	                             on g.gcbm = t.gcbm
//	                             group by t.dataType
//                                 order by t.dataType";
//                        dt = CommonDAL.GetDataTable(string.Format(sql, DateTime.Now.ToString("yyyy-MM-dd")));
//                    }
//                    else//区县
//                    {
//                        sql = @"select t.dataType DataName,count(*) DataValue
//	                            from dbo.jcsjk_GC_GCXX_NEW g 
//	                            inner join
//	                            (
//		                            select r.gcbm,r.sgdwxmfzr as xm,r.SGDWXMFZRSFZH as sfzh,z.PSN_Level +'建造师' as dataType,z.ENT_City as qx
//		                            from dbo.jcsjk_GC_WFZT_SGDW r inner join dbo.jcsjk_GC_CP_SGXKZ j on r.gcbm = j.gcbm
//		                            left join [dbo].[jcsjk_jzs] z on r.SGDWXMFZRSFZH = z.[PSN_CertificateNO]
//		                             where r.sgdwxmfzr is not null and j.sgjd='在施' and len(z.[PSN_RegisterNO])>0 and z.[PSN_CertificateValidity] <='{0}'
//		                             and len(z.ENT_City)>0 and patindex('%'+ z.ENT_City +'%','{1}')>0 and  z.PSN_RegisteType <>'注销' 
//		                             union all
//		                            select r.gcbm,r.JLDWXMFZR,r.JLDWXMZJSFZH,'监理师',z.[所在区县]
//		                            from dbo.jcsjk_GC_WFZT_JLDW r inner join dbo.jcsjk_GC_CP_SGXKZ j on r.gcbm = j.gcbm
//			                            left join [dbo].[jcsjk_jls] z on r.JLDWXMZJSFZH = z.[证件号]
//		                             where r.JLDWXMZJ is not null and j.sgjd='在施' and len(z.[执业资格证书编号])>0 and z.[注册有效期] <='{0}'
//		                             and len(z.[所在区县])>0 and patindex('%'+ z.[所在区县] +'%','{1}')>0
//	                             ) t
//	                             on g.gcbm = t.gcbm
//	                             group by t.dataType
//                                 order by t.dataType";

//                        dt = CommonDAL.GetDataTable(string.Format(sql, DateTime.Now.ToString("yyyy-MM-dd"), qx));
//                    }

                    sql = @"select [TJDate],[Region], DataName,DataValue  from [dbo].[TJ_ZYRYCount] 	
                    where [TJDate] =(select max([TJDate])  from [dbo].[TJ_ZYRYCount] where [TJDate] <999999)
                    and [Region] like '%{0}%' and ([DataName] like '在施项目人员证书到期预警%')
                    order by DataName;";

                    dt = CommonDAL.GetDataTable(string.Format(sql, updateregion(Region)));

                    sb.Remove(0, sb.Length);
                    int sum_person_gouqi = 0;
                    foreach (DataRow r in dt.Rows)
                    {
                        sb.Append(string.Format(",[ value: {0}, name: '{1}' ]", r["DataValue"], r["DataName"].ToString().Replace("在施项目人员证书到期预警_", "")));
                        sum_person_gouqi +=Convert.ToInt32(r["DataValue"]);
                    }
                    if (sb.Length > 0)
                    {
                        sb.Remove(0, 1).Replace("[", "{").Replace("]", "}");
                    }
                    ViewState["person_gouqi"] = sb.ToString();
                    ViewState["sum_person_gouqi"] = sum_person_gouqi;

                    #endregion 在施项目人员证书到期预警

                    //在施项目人员证书与实际单位不一致预警
                    #region 在施项目人员证书与实际单位不一致预警

//                    if (Region == "北京市住房和城乡建设委员会")//全市
//                    {
//                        sql = @"select t.dataType DataName,count(*) DataValue
//	                            from dbo.jcsjk_GC_GCXX_NEW g 
//	                            inner join
//	                            (
//		                            select r.gcbm,z.[ENT_Name],z.[ENT_OrganizationsCode],r.SGDW,r.sgdwzzjgdm,r.sgdwxmfzr as xm,r.SGDWXMFZRSFZH as sfzh,z.PSN_Level +'建造师' as dataType,z.ENT_City as qx
//		                            from dbo.jcsjk_GC_WFZT_SGDW r inner join dbo.jcsjk_GC_CP_SGXKZ j on r.gcbm = j.gcbm
//		                            left join [dbo].[jcsjk_jzs] z on r.SGDWXMFZRSFZH = z.[PSN_CertificateNO]
//		                             where r.sgdwxmfzr is not null and j.sgjd='在施' and len(z.[PSN_RegisterNO])>0 and  z.PSN_RegisteType <>'注销' 	
//		                             and z.[ENT_Name] <> r.SGDW and 
//		                             (case when len(replace(z.[ENT_OrganizationsCode],'-',''))=18 then substring(replace(z.[ENT_OrganizationsCode],'-',''),9,9)
//		                             else replace(z.[ENT_OrganizationsCode],'-','') end)  <> r.sgdwzzjgdm
//		                             union all
//		                            select r.gcbm,z.[聘用单位],z.[组织机构代码],r.[JLDW],r.[JLDWZZJGDM],r.JLDWXMFZR,r.JLDWXMZJSFZH,'监理师',z.[所在区县]
//		                            from dbo.jcsjk_GC_WFZT_JLDW r inner join dbo.jcsjk_GC_CP_SGXKZ j on r.gcbm = j.gcbm
//			                            left join [dbo].[jcsjk_jls] z on r.JLDWXMZJSFZH = z.[证件号]
//		                             where r.JLDWXMZJ is not null and j.sgjd='在施' and len(z.[执业资格证书编号])>0 
//		                              and z.[聘用单位] <> r.[JLDW] and 
//		                             (case when len(replace(z.[组织机构代码],'-',''))=18 then substring(replace(z.[组织机构代码],'-',''),9,9)
//		                             else replace(z.[组织机构代码],'-','') end)  <> r.[JLDWZZJGDM]
//	                             ) t
//	                             on g.gcbm = t.gcbm
//	                             group by t.dataType
//	                             order by t.dataType";
//                        dt = CommonDAL.GetDataTable(sql);
//                    }
//                    else//区县
//                    {
//                        sql = @"select t.dataType DataName,count(*) DataValue
//	                            from dbo.jcsjk_GC_GCXX_NEW g 
//	                            inner join
//	                            (
//		                            select r.gcbm,z.[ENT_Name],z.[ENT_OrganizationsCode],r.SGDW,r.sgdwzzjgdm,r.sgdwxmfzr as xm,r.SGDWXMFZRSFZH as sfzh,z.PSN_Level +'建造师' as dataType,z.ENT_City as qx
//		                            from dbo.jcsjk_GC_WFZT_SGDW r inner join dbo.jcsjk_GC_CP_SGXKZ j on r.gcbm = j.gcbm
//		                            left join [dbo].[jcsjk_jzs] z on r.SGDWXMFZRSFZH = z.[PSN_CertificateNO]
//		                             where r.sgdwxmfzr is not null and j.sgjd='在施' and len(z.[PSN_RegisterNO])>0 and  z.PSN_RegisteType <>'注销' 	
//		                             and len(z.ENT_City)>0 and patindex('%'+ z.ENT_City +'%','{0}')>0 	 
//		                             and z.[ENT_Name] <> r.SGDW and 
//		                             (case when len(replace(z.[ENT_OrganizationsCode],'-',''))=18 then substring(replace(z.[ENT_OrganizationsCode],'-',''),9,9)
//		                             else replace(z.[ENT_OrganizationsCode],'-','') end)  <> r.sgdwzzjgdm
//		                             union all
//		                            select r.gcbm,z.[聘用单位],z.[组织机构代码],r.[JLDW],r.[JLDWZZJGDM],r.JLDWXMFZR,r.JLDWXMZJSFZH,'监理师',z.[所在区县]
//		                            from dbo.jcsjk_GC_WFZT_JLDW r inner join dbo.jcsjk_GC_CP_SGXKZ j on r.gcbm = j.gcbm
//			                            left join [dbo].[jcsjk_jls] z on r.JLDWXMZJSFZH = z.[证件号]
//		                             where r.JLDWXMZJ is not null and j.sgjd='在施' and len(z.[执业资格证书编号])>0 
//		                             and len(z.[所在区县])>0 and patindex('%'+ z.[所在区县] +'%','{0}')>0
//		                              and z.[聘用单位] <> r.[JLDW] and 
//		                             (case when len(replace(z.[组织机构代码],'-',''))=18 then substring(replace(z.[组织机构代码],'-',''),9,9)
//		                             else replace(z.[组织机构代码],'-','') end)  <> r.[JLDWZZJGDM]
//	                             ) t
//	                             on g.gcbm = t.gcbm
//	                             group by t.dataType
//	                             order by t.dataType";

//                        dt = CommonDAL.GetDataTable(string.Format(sql, qx));
//                    }
                    sb.Remove(0, sb.Length);
                    int sum_person_KaDanWei = 0;

                    sql = @"select [TJDate],[Region], DataName,DataValue  from [dbo].[TJ_ZYRYCount] 	
                    where [TJDate] =(select max([TJDate])  from [dbo].[TJ_ZYRYCount] where [TJDate] <999999)
                    and [Region] like '%{0}%' and [DataName] like '注册单位与实际不一致%';";

                    dt = CommonDAL.GetDataTable(string.Format(sql, updateregion(Region)));

                    foreach (DataRow r in dt.Rows)
                    {
                        sb.Append(string.Format(",[ value: {0}, name: '{1}' ]", r["DataValue"], r["DataName"].ToString().Replace("注册单位与实际不一致_","")));
                        sum_person_KaDanWei += Convert.ToInt32(r["DataValue"]);
                    }
                    if (sb.Length > 0)
                    {
                        sb.Remove(0, 1).Replace("[", "{").Replace("]", "}");
                    }
                    ViewState["person_KaDanWei"] = sb.ToString();
                    ViewState["sum_person_KaDanWei"] = sum_person_KaDanWei;


                    #endregion 在施项目人员证书与实际单位不一致预警

                    //只投不施与只施不投
                    #region 只投不施与只施不投
                    sb.Remove(0, sb.Length);
                    sql = @"select DataName,DataValue  from [dbo].[TJ_ZYRYCount] 	
                    where [TJDate] =(select max([TJDate])  from [dbo].[TJ_ZYRYCount] where [TJDate] <999999)
                    and [Region] like '%{0}%' and ([DataName] like '只投不施' or [DataName] like '只施不投' or [DataName] like '参与项目')";

                    dt = CommonDAL.GetDataTable(string.Format(sql, updateregion(Region)));

//                    if (Region == "北京市住房和城乡建设委员会")//全市
//                    {
//                        sql = @"select '只投不施' dataname ,count(*) datavalue from dbo.jcsjk_tj_ztbs 
//	                            union
//	                            select '只施不投' dataname ,count(*) datavalue from dbo.jcsjk_tj_zsbt 
//	                            union
//	                            select '建造师总数',sum(datavalue) from dbo.[TJ_ZYRYCount] where [TJDate]={0} and [Region]='全市'
//	                            and (dataname='一级建造师' or dataname='一级临时建造师' or dataname='二级建造师' or dataname='二级临时建造师')
//                                union
//                                select '参与项目',count(*) from 
//		                                (
//			                                select h.[ProjectManager] xm,h.[ProjectManagerCardNo] sfzh
//			                                from [dbo].[jcsjk_HT_SG] h	
//			                                inner join [dbo].[jcsjk_GC_GCXX_NEW] g on h.ProjectSubID = g.[HTBH]
//			                                where h.valid=1 and g.valid=1
//		                                    union		
//			                                select s.[SGDWXMFZR] xm,s.[SGDWXMFZRSFZH] sfzh
//			                                FROM [dbo].[jcsjk_GC_WFZT_SGDW] s
//			                                left join [dbo].[jcsjk_GC_GCXX_NEW] g on s.GCBM = g.GCBM
//			                                where len(s.[SGDWXMFZRSFZH])>0 
//		                                ) f
//		                                inner join [dbo].[jcsjk_jzs] j on f.sfzh = j.[PSN_CertificateNO]";

//                        dt = CommonDAL.GetDataTable(string.Format(sql, DateTime.Now.ToString("yyyyMM")));
//                    }
//                    else//区县
//                    {
//                        sql = @"select '只投不施' dataname ,count(*) datavalue from dbo.jcsjk_tj_ztbs where ent_city ='{1}'
//	                            union
//	                            select '只施不投' dataname ,count(*) datavalue from dbo.jcsjk_tj_zsbt where ent_city ='{1}'
//	                            union
//	                            select '建造师总数',sum(datavalue) from dbo.[TJ_ZYRYCount] where [TJDate]={0} and [Region]='{1}'
//	                            and (dataname='一级建造师' or dataname='一级临时建造师' or dataname='二级建造师' or dataname='二级临时建造师')
//                                union
//                                select '参与项目',count(*) from 
//		                                (
//			                                select h.[ProjectManager] xm,h.[ProjectManagerCardNo] sfzh
//			                                from [dbo].[jcsjk_HT_SG] h	
//			                                inner join [dbo].[jcsjk_GC_GCXX_NEW] g on h.ProjectSubID = g.[HTBH]
//			                                where h.valid=1 and g.valid=1
//		                                    union		
//			                                select s.[SGDWXMFZR] xm,s.[SGDWXMFZRSFZH] sfzh
//			                                FROM [dbo].[jcsjk_GC_WFZT_SGDW] s
//			                                left join [dbo].[jcsjk_GC_GCXX_NEW] g on s.GCBM = g.GCBM
//			                                where len(s.[SGDWXMFZRSFZH])>0 
//		                                ) f
//		                                inner join [dbo].[jcsjk_jzs] j on f.sfzh = j.[PSN_CertificateNO]
//                                        where ent_city ='{1}'";
//                        dt = CommonDAL.GetDataTable(string.Format(sql, DateTime.Now.ToString("yyyyMM"), qx));
//                    }

                    if (dt.Rows.Count > 0)
                    {
                        ViewState["参与项目"] = dt.Rows[0]["datavalue"];
                        ViewState["非只投不施"] = Convert.ToInt32(dt.Rows[0]["datavalue"]) - Convert.ToInt32(dt.Rows[2]["datavalue"]);
                        ViewState["只投不施"] = dt.Rows[2]["datavalue"];
                        ViewState["非只施不投"] = Convert.ToInt32(dt.Rows[0]["datavalue"]) - Convert.ToInt32(dt.Rows[1]["datavalue"]);
                        ViewState["只施不投"] = dt.Rows[1]["datavalue"];
                    }



                    #endregion 只投不施与只施不投

                    //频繁变更项目经理（只比对施工单位项目负责人，即建造师）
                    #region 频繁变更项目经理（只比对施工单位项目负责人，即建造师）
                    sql = @"select [TJDate],[Region], DataName,DataValue  from [dbo].[TJ_ZYRYCount] 	
                    where [TJDate] =(select max([TJDate])  from [dbo].[TJ_ZYRYCount] where [TJDate] <999999)
                    and [Region] like '%{0}%' and ([DataName] like '在施项目数量' or [DataName] like '在施频繁更换项目经理项目数量')
                    order by DataName;";

                    dt = CommonDAL.GetDataTable(string.Format(sql, updateregion(Region)));

                    foreach (DataRow r in dt.Rows)
                    {
                        if(r["DataName"].ToString()=="在施频繁更换项目经理项目数量")
                        {
                            ViewState["在施频繁更换项目经理项目数量"] = r["datavalue"];
                        }
                        if (r["DataName"].ToString() == "在施项目数量")
                        {
                            ViewState["在施项目数量"] = r["datavalue"];
                        }
                    }
                    ViewState["在施项目其它数量"] = Convert.ToInt32(ViewState["在施项目数量"]) - Convert.ToInt32(ViewState["在施频繁更换项目经理项目数量"]);

                    #endregion 频繁变更项目经理（只比对施工单位项目负责人，即建造师）

                    //sb.Remove(0, sb.Length);
                    //foreach (DataRow r in dt.Rows)
                    //{
                    //    sb.Append(string.Format(",[ value: {0}, name: '{1}' ]", r["DataValue"], r["DataName"]));

                    //}
                    //if (sb.Length > 0)
                    //{
                    //    sb.Remove(0, 1).Replace("[", "{").Replace("]", "}");
                    //}
                    //ViewState["person_Change"] = sb.ToString();

//                    //施工许可变更
//                                        sql = @"select GCBM,BGX,BGQZ,BGHZ  from  [Server_192.168.5.98].[SHAREDB].[dbo].[GC_SGXKZBGJL_NEW] where valid =1
//                                            and BGX in('施工单位','施工单位项目负责人','监理单位','监理单位项目负责人')";


//                                         --工程合同项目经理（立项）
//                                           select g.gcbm ,h.[ProjectManager],h.[ProjectManagerCardNo] from HTGL.dbo.BCACTC_CONTRACTRECORD h	
//                                         inner join [dbo].[jcsjk_GC_GCXX_NEW] g on h.ProjectSubID = g.[HTBH]
//                                         where h.valid=1 and g.valid=1
//                                         and g.gcbm='200603300007'

//                                        --施工项目经理
//                                           select distinct s.gcbm,s.[SGDWXMFZR],s.[SGDWXMFZRSFZH]
//                                         FROM [dbo].[jcsjk_GC_WFZT_SGDW] s
//                                         left join [dbo].[jcsjk_GC_GCXX_NEW] g on s.GCBM = g.GCBM
//                                         where len(s.[SGDWXMFZRSFZH])>0 
//                                         and s.gcbm='200603300007'

//                                          --监理项目经理
//                                           select distinct s.gcbm,s.[JLDWXMZJ],s.[JLDWXMZJSFZH]
//                                         FROM [dbo].[jcsjk_GC_WFZT_JLDW] s
//                                         left join [dbo].[jcsjk_GC_GCXX_NEW] g on s.GCBM = g.GCBM
//                                         where len(s.[JLDWXMZJSFZH])>0 

//                                         --企业自填质量曾诺信息
//                                         select top 100 GCBM,DWLX,DWMC,ZZJGDM,CNRXM from 
//                                          [Server_192.168.5.49].SJZX_CPGC.dbo.GC_CP_GCZLCNS
//                                          where dwlx ='施工总承包单位' or dwlx ='监理单位'

                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "全市人员生命周期，全市监控数据源获取失败", ex);
                }

            }
        }
    }
}