using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAccess;
using System.Text;
using Model;

namespace ZYRYJG.City
{
    //人员监控
    public partial class PersonnelMonitoring : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                RadNumericTextBoxYear.Value = DateTime.Now.Year;
                try
                {
                    //各类执业人员总量统计
                    BindPersonCount();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "BindChartPerson", "var o=\"" + ViewState["person"].ToString() + "\";BindChartPerson(o);", true);

                    //绑定执业人员增长趋势（近12个月）
                    BindPersonAdd();

                    //注册有效期到期预警
                    BindValidEndMonitoring(DateTime.Now.Year);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "BindChartQXRY", "var p=\"" + ViewState["RegionPersonValue"].ToString() + "\";BindChartQXRY(p);", true);

                    //绑定重复注册人员
                    BindRepeatRegition();
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "全生命周期监控 - 人员监控 数据源获取失败。", ex);
                }

            }
        }

        /// <summary>
        /// 绑定执业人员分布
        /// </summary>
        private void BindPersonCount()
        {
            DataTable dt = null;
            System.Text.StringBuilder sb = new StringBuilder();

            //执业人员分布
            string sql = @"select [TJDate],[Region], DataName,DataValue  from [dbo].[TJ_ZYRYCount] 	
                    where [TJDate] =(select max([TJDate])  from [dbo].[TJ_ZYRYCount] where [TJDate] <999999)
                    and ([DataName] like '一级建造师' or [DataName] like '一级临时建造师' or [DataName] like '二级建造师' or [DataName] like '二级临时建造师' or [DataName] like '监理师' or [DataName] like '造价师')
                    and [Region] like '%{0}%'";
           // dt = CommonDAL.GetDataTable(string.Format(sql, (Region == "北京市住房和城乡建设委员会" ? "全市" : Region)));
            //string updateregion = "";
            //if (Region=="密云县")
            //{
            //    updateregion = "密云区";
            //}
            //else if (Region == "延庆县")
            //{
            //    updateregion = "延庆区";
            //}
            //else if (Region == "亦庄开发区")
            //{
            //    updateregion = "亦庄";
            //}
            dt = CommonDAL.GetDataTable(string.Format(sql, updateregion(Region)));
            int PersonCount = 0;
            foreach (DataRow r in dt.Rows)
            {
                sb.Append(string.Format(",[ value: {0}, name: '{1}' ]", r["DataValue"], r["DataName"]));
                PersonCount += Convert.ToInt32(r["DataValue"]);
            }
            if (sb.Length > 0)
            {
                sb.Remove(0, 1).Replace("[", "{").Replace("]", "}");
            }
            ViewState["person"] = sb.ToString();

            LabelPersonCount.Text = string.Format("执业人员总量：{0}人", PersonCount);
        }

        /// <summary>
        /// 绑定执业人员增长趋势（近12个月）
        /// </summary>
        private void BindPersonAdd()
        {
            DataTable dt = null;
            System.Text.StringBuilder sb = new StringBuilder();


            //执业人员分布
            string sql = @"select [TJDate],[Region], sum(DataValue) 'DataValue' from [dbo].[TJ_ZYRYCount] 	
                            where  [TJDate] <999999  and [TJDate] > convert(char(6),dateadd(month,-12,'{0}'),112)
					        and (DataName ='一级建造师' or DataName ='一级临时建造师' or DataName ='二级建造师' or 
					        DataName ='二级临时建造师' or DataName ='监理师' or DataName ='造价师')
                            and [Region] like '%{1}%' 					
					        group  by [TJDate],[Region]
					        order by [TJDate]";

            dt = CommonDAL.GetDataTable(string.Format(sql, DateTime.Now.ToString("yyyy-MM-dd"), updateregion(Region)));
            int minData = 0;
            foreach (DataRow r in dt.Rows)
            {
                sb.Append(string.Format(",{0}", r["DataValue"]));
                if (minData == 0 || minData > Convert.ToInt32(r["DataValue"]))
                {
                    minData = Convert.ToInt32(r["DataValue"]);
                }
            }
            if (sb.Length > 0)
            {
                sb.Remove(0, 1);
            }
            ViewState["personAdd"] = sb.ToString();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "BindChartPersonAdd"
                ,string.Format("BindChartPersonAdd('{0}',{1},{2});",ViewState["personAdd"],DateTime.Now.AddMonths(-11).Month ,minData -500)
                , true);
        }

        /// <summary>
        /// 绑定证书到期预警信息
        /// </summary>
        /// <param name="year">统计年份</param>
        private void BindValidEndMonitoring(int year)
        {
            DataTable dt = null;
            System.Text.StringBuilder sb = new StringBuilder();
            string sql = "";
            System.Text.StringBuilder sbItem = new StringBuilder();
            System.Text.StringBuilder sbrow = new StringBuilder();
            if (Region == "全市")
            {
                sql = @"select * from 
                            (
	                            select month(PSN_CertificateValidity) as yue,[PSN_Level] + '建造师' as DataName,count(*) DataValue 
	                            from dbo.jcsjk_jzs 
	                            where year(PSN_CertificateValidity)={0} and  PSN_RegisteType <>'注销' 
	                            group by month(PSN_CertificateValidity),[PSN_Level]
	                            union all
	                            select month(注册有效期) as yue, '监理师' as DataName,count(*) DataValue 
	                            from dbo.[jcsjk_jls] 
	                            where year(注册有效期)={0} 
	                            group by month(注册有效期)
	                            union all
	                            select month(ZSYXQ) as yue, '造价师' as DataName,count(*) DataValue 
	                            from dbo.[jcsjk_zjs] 
	                            where year(ZSYXQ)={0} and valid =1
	                            group by month(ZSYXQ)
                            ) t order by yue,DataName";
                dt = CommonDAL.GetDataTable(string.Format(sql, year));
            }
            else//区县
            {
                sql = @"select * from 
                            (
	                            select month(PSN_CertificateValidity) as yue,[PSN_Level] + '建造师' as DataName,count(*) DataValue 
	                            from dbo.jcsjk_jzs 
	                            where year(PSN_CertificateValidity)={0} and len(ENT_City)>0 and patindex('%{1}%',ENT_City)>0 and  PSN_RegisteType <>'注销' 
 	                            group by month(PSN_CertificateValidity),[PSN_Level]
	                            union all
	                            select month(注册有效期) as yue, '监理师' as DataName,count(*) DataValue 
	                            from dbo.[jcsjk_jls] 
	                            where year(注册有效期)={0} and len(所在区县)>0 and patindex('%{1}%',所在区县)>0
	                            group by month(注册有效期)
	                            union all
	                            select month(ZSYXQ) as yue, '造价师' as DataName,count(*) DataValue 
	                            from dbo.[jcsjk_zjs] 
	                            where year(ZSYXQ)={0} and valid =1 and len(region)>0 and patindex('%{1}%',region)>0
	                            group by month(ZSYXQ)
                            ) t order by yue,DataName";
                string qx = Region;
                switch (qx)
                {
                    case "西城区":
                        qx = "西城区宣武区";
                        break;
                    case "东城区":
                        qx = "东城区崇文区";
                        break;
                    case "密云县":
                        qx="密云区";
                        break;
                    case "延庆县":
                        qx = "延庆区";
                        break;
                    case "亦庄开发区":
                        qx = "亦庄";
                        break;

                }
                dt = CommonDAL.GetDataTable(string.Format(sql, year, qx));
            }
        
           

            DataColumn[] pk = new DataColumn[2];
            pk[0] = dt.Columns["yue"];
            pk[1] = dt.Columns["DataName"];
            dt.PrimaryKey = pk;

            string[] months = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"};
            string[] series = new string[] { "一级建造师", "一级临时建造师", "二级建造师", "二级临时建造师", "监理师", "造价师" };
            DataRow find = null;
            foreach (string s in series)
            {
                sbItem.Remove(0, sbItem.Length);

                sbrow.Append("<tr><td>").Append(s).Append("</td>");

                foreach (string q in months)
                {
                    find = dt.Rows.Find(new object[] { Convert.ToInt32(q), s });
                    if (find != null)
                    {
                        sbItem.Append(",").Append(find["DataValue"].ToString());
                        sbrow.Append("<td>").Append(find["DataValue"].ToString()).Append("</td>");
                    }
                    else
                    {
                        sbItem.Append(",0");
                        sbrow.Append("<td>").Append("0").Append("</td>");
                    }
                }
                if (sbItem.Length > 0)
                {
                    sbItem.Remove(0, 1);
                }
                sb.Append(string.Format(@",(name: '{0}',type: 'bar',stack:'一级建造师',data: [{1}],temStyle : ( normal: (barBorderRadius :5)))", s, sbItem.ToString()));
                sbrow.Append("</tr>");
            }
            if (sb.Length > 0)
            {
                sb.Remove(0, 1).Replace("(", "{").Replace(")", "}");
            }
            ViewState["RegionPersonValue"] = sb.ToString();

            Div_RegionPerson.InnerHtml = string.Format("<table class=\"tb\"><tr><td></td><td>1月</td><td>2月</td><td>3月</td><td>4月</td><td>5月</td><td>6月</td><td>7月</td><td>8月</td><td>9月</td><td>10月</td><td>11月</td><td>12月</td></tr>{0}</table>"
                , sbrow.ToString()
                );
        }
        
        /// <summary>
        /// 绑定重复注册人员
        /// </summary>
        private void BindRepeatRegition()
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }

            #region   旧的
            
           
//            string sql = @"select row_number() over(order by p.[PSN_Name] ) as RowNum,p.[PSN_Name],p.SFZH,r.RepeatNum,jz1.ENT_Name '一级建造师',jz1L.ENT_Name '一级临时建造师',jz2.ENT_Name '二级建造师',jz2L.ENT_Name '二级临时建造师',jl.[聘用单位] '监理师',zj.[PYDW] '造价师'
//							from
//							(
//								select distinct [PSN_Name],[PSN_CertificateNO] as SFZH from dbo.jcsjk_jzs  where PSN_RegisteType <>'注销' and PSN_CertificateValidity >getdate()
//								union
//								select distinct [姓名],[证件号] from dbo.[jcsjk_jls]  where [注册有效期] >getdate()
//								union
//								select distinct [XM],[SFZH] from dbo.[jcsjk_zjs]  where  valid =1 and ZSYXQ >getdate()
//							) p
//							inner join
//							(
//								select t.SFZH,count(*) RepeatNum
//								from
//								(
//									select distinct [ENT_Name],[PSN_CertificateNO] as SFZH from dbo.jcsjk_jzs  where PSN_RegisteType <>'注销' and PSN_CertificateValidity >getdate()
//									union
//									select distinct [聘用单位],[证件号] from dbo.[jcsjk_jls]  where [注册有效期] >getdate()
//									union
//									select distinct [PYDW],[SFZH] from dbo.[jcsjk_zjs]  where  valid =1 and ZSYXQ >getdate()
//								) t group by SFZH
//								having count(*)>1
//							) r
//							on p.SFZH=r.SFZH
//	                        left join dbo.jcsjk_jzs  as jz1
//	                        on jz1.[PSN_Level] ='一级' and jz1.PSN_RegisteType <>'注销' and p.SFZH=jz1.[PSN_CertificateNO] and jz1.PSN_CertificateValidity >getdate()
//							left join dbo.jcsjk_jzs  as jz1L
//	                        on jz1L.[PSN_Level] ='一级临时' and jz1L.PSN_RegisteType <>'注销' and p.SFZH=jz1L.[PSN_CertificateNO] and jz1L.PSN_CertificateValidity >getdate()
//							left join dbo.jcsjk_jzs  as jz2
//	                        on jz2.[PSN_Level] ='二级' and jz2.PSN_RegisteType <>'注销' and p.SFZH=jz2.[PSN_CertificateNO] and jz2.PSN_CertificateValidity >getdate()
//							left join dbo.jcsjk_jzs  as jz2L
//	                        on jz2L.[PSN_Level] ='二级临时' and jz2L.PSN_RegisteType <>'注销' and p.SFZH=jz2L.[PSN_CertificateNO] and jz2L.PSN_CertificateValidity >getdate()
//							left join dbo.[jcsjk_jls] jl 
//							on jl.[注册有效期] >getdate() and p.SFZH=jl.[证件号]
//							left join dbo.[jcsjk_zjs] zj 
//							on zj.[ZSYXQ] >getdate() and p.SFZH=zj.[SFZH]
//                            where {0} {1}";

            #endregion

            #region 新的
            string sql = @" select row_number() over(order by p.[PSN_Name] ) as RowNum,p.[PSN_Name],p.SFZH,r.RepeatNum,jz1.ENT_Name '一级建造师',jz2.ENT_Name '二级建造师',jl.[聘用单位] '监理师',zj.[PYDW] '造价师'
							from
							(
								select distinct [PSN_Name],[PSN_CertificateNO] as SFZH from dbo.jcsjk_jzs  where PSN_RegisteType <>'注销'
								union
								select distinct [姓名],[证件号] from dbo.[jcsjk_jls]  where [注册有效期] >getdate()
								union
								select distinct [XM],[SFZH] from dbo.[jcsjk_zjs]  where  valid =1 and ZSYXQ >getdate()
							) p
							inner join
							(
								select t.SFZH,count(*) RepeatNum
								from
								(
									select distinct [ENT_Name],[PSN_CertificateNO] as SFZH from dbo.jcsjk_jzs  where PSN_RegisteType <>'注销'
									union
									select distinct [聘用单位],[证件号] from dbo.[jcsjk_jls]  where [注册有效期] >getdate()
									union
									select distinct [PYDW],[SFZH] from dbo.[jcsjk_zjs]  where  valid =1 and ZSYXQ >getdate()
									union
									 SELECT distinct '', [PSN_CertificateNO]      
										  FROM jcsjk_jzs
										where [PSN_CertificateNO]
										in(
											select [PSN_CertificateNO]
											  FROM jcsjk_jzs
											where [PSN_RegisteType] <>'注销'
											group by [PSN_CertificateNO]
											having count(*) >1
										)  and [PSN_RegisteType] <>'注销'
								) t group by SFZH
								having count(*)>1
							) r
							on p.SFZH=r.SFZH
	                        left join dbo.jcsjk_jzs  as jz1
	                        on jz1.[PSN_Level] ='一级' and jz1.PSN_RegisteType <>'注销' and p.SFZH=jz1.[PSN_CertificateNO] 						
							left join dbo.jcsjk_jzs  as jz2
	                        on jz2.[PSN_Level] ='二级' and jz2.PSN_RegisteType <>'注销' and p.SFZH=jz2.[PSN_CertificateNO]							
							left join dbo.[jcsjk_jls] jl 
							on jl.[注册有效期] >getdate() and p.SFZH=jl.[证件号]
							left join dbo.[jcsjk_zjs] zj 
							on zj.[ZSYXQ] >getdate() and p.SFZH=zj.[SFZH]
                            where {0} {1} ";

            #endregion



            DataTable dt = CommonDAL.GetDataTable(string.Format(sql
                , updateregion(Region) == "全市" ? "1=1" : string.Format(@"(
                    (len(jz1.ENT_City)>0 and patindex('%{0}%',jz1.ENT_City)>0)
                 or (len(jz2.ENT_City)>0 and patindex('%{0}%',jz2.ENT_City)>0)
                 or (len(jl.[所在区县])>0 and patindex('%{0}%',jl.[所在区县])>0)
                 or (len(zj.region)>0 and patindex('%{0}%',zj.region)>0)
                 )", updateregion(Region))
                   , RadTextBoxQYMC.Text.Trim() == "" ? "" : string.Format(@" and (
                    (jz1.[ENT_Name] like '%{0}%')
                 or (jz2.[ENT_Name] like '%{0}%')
                 or (jl.[聘用单位] like '%{0}%')
                 or (zj.[PYDW] like '%{0}%')
                 )", RadTextBoxQYMC.Text.Trim())
                   ));
            RadGridRepeat.DataSource = dt;
            
            RadGridRepeat.DataBind();
        }

        //变换年度
        protected void RadNumericTextBoxYear_TextChanged(object sender, EventArgs e)
        {
            BindValidEndMonitoring(Convert.ToInt32(RadNumericTextBoxYear.Value));

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "BindChartQXRY", "var p=\"" + ViewState["RegionPersonValue"].ToString() + "\";BindChartQXRY(p);", true);
        }

        //查询重复注册企业
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            BindRepeatRegition();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "BindChartQXRY", "var p=\"" + ViewState["RegionPersonValue"].ToString() + "\";BindChartQXRY(p);", true);
        }

        protected void ButtonExport_Click(object sender, EventArgs e)
        {
            if (RadGridRepeat.Items.Count>0)
            {                

                try
                {
                    //EXCEL表头明
                    string head = @"序号\姓名\证件号码\一级建造师\二级建造师\监理师\造价师\重复度";
                    //数据表的列明
                    string column = @"row_number() over(order by p.[PSN_Name])\p.[PSN_Name]\p.SFZH\jz1.ENT_Name\jz2.ENT_Name\jl.[聘用单位]\zj.[PYDW]\r.RepeatNum";
                    //过滤条件

                    if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/Excel/"))) System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Upload/Excel/"));
                    string filePath = string.Format("~/Upload/Excel/{0}_{1}.xls", DateTime.Now.ToString("yyyyMMdd"), Guid.NewGuid());
                    CommonDAL.OutputXls(HttpContext.Current.Server.MapPath(filePath)
                        , @"(
								select distinct [PSN_Name],[PSN_CertificateNO] as SFZH from dbo.jcsjk_jzs  where PSN_RegisteType <>'注销'
								union
								select distinct [姓名],[证件号] from dbo.[jcsjk_jls]  where [注册有效期] >getdate()
								union
								select distinct [XM],[SFZH] from dbo.[jcsjk_zjs]  where  valid =1 and ZSYXQ >getdate()
							) p
							inner join
							(
								select t.SFZH,count(*) RepeatNum
								from
								(
									select distinct [ENT_Name],[PSN_CertificateNO] as SFZH from dbo.jcsjk_jzs  where PSN_RegisteType <>'注销'
									union
									select distinct [聘用单位],[证件号] from dbo.[jcsjk_jls]  where [注册有效期] >getdate()
									union
									select distinct [PYDW],[SFZH] from dbo.[jcsjk_zjs]  where  valid =1 and ZSYXQ >getdate()
									union
									 SELECT distinct '', [PSN_CertificateNO]      
										  FROM jcsjk_jzs
										where [PSN_CertificateNO]
										in(
											select [PSN_CertificateNO]
											  FROM jcsjk_jzs
											where [PSN_RegisteType] <>'注销'
											group by [PSN_CertificateNO]
											having count(*) >1
										)  and [PSN_RegisteType] <>'注销'
								) t group by SFZH
								having count(*)>1
							) r
							on p.SFZH=r.SFZH
	                        left join dbo.jcsjk_jzs  as jz1
	                        on jz1.[PSN_Level] ='一级' and jz1.PSN_RegisteType <>'注销' and p.SFZH=jz1.[PSN_CertificateNO] 							
							left join dbo.jcsjk_jzs  as jz2
	                        on jz2.[PSN_Level] ='二级' and jz2.PSN_RegisteType <>'注销' and p.SFZH=jz2.[PSN_CertificateNO]							
							left join dbo.[jcsjk_jls] jl 
							on jl.[注册有效期] >getdate() and p.SFZH=jl.[证件号]
							left join dbo.[jcsjk_zjs] zj 
							on zj.[ZSYXQ] >getdate() and p.SFZH=zj.[SFZH]"
                        , string.Format(" {0} {1}", 
                             updateregion(Region) == "全市" ? "" : string.Format(@"(
                             (len(jz1.ENT_City)>0 and patindex('%{0}%',jz1.ENT_City)>0)
                             or (len(jz2.ENT_City)>0 and patindex('%{0}%',jz2.ENT_City)>0)
                             or (len(jl.[所在区县])>0 and patindex('%{0}%',jl.[所在区县])>0)
                             or (len(zj.region)>0 and patindex('%{0}%',zj.region)>0)
                             )", updateregion(Region))

                           , RadTextBoxQYMC.Text.Trim() == "" ? "" : string.Format(@" and (
                            (jz1.[ENT_Name] like '%{0}%')
                             or (jz2.[ENT_Name] like '%{0}%')
                             or (jl.[聘用单位] like '%{0}%')
                             or (zj.[PYDW] like '%{0}%')
                             )", RadTextBoxQYMC.Text.Trim())
                        )
                   , "row_number() over(order by p.[PSN_Name])", head.ToString(), column.ToString());
                    string size = CommonDAL.GetFileSize(HttpContext.Current.Server.MapPath(filePath));
                    spanOutput.InnerHtml = string.Format(@"<a href=""{0}"">{1}</a><span  style=""padding-left:20px;"">（{2}）</span>"
                        ,UIHelp.ShowFile(filePath)
                        , "点击我下载"
                        , size);
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "业务查询结果导出EXCEL失败！", ex);
                }
              
            }

        }
    }
}