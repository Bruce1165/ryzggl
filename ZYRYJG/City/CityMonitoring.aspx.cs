using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAccess;
using System.Text;

namespace ZYRYJG
{
    //全市监控
    public partial class CityMonitoring : BasePage
    {
        //各区县人员总量及分布情况内存表
        protected DataTable QXRYSL;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                try
                {
                    DataTable dt = null;
                    DataRow find = null;
                    System.Text.StringBuilder sb = new StringBuilder();
                    System.Text.StringBuilder sbItem = new StringBuilder();
                    System.Text.StringBuilder sbrow = new StringBuilder();
                    string[] regions = new string[] { "东城区", "西城区", "海淀区", "朝阳区", "石景山区", "丰台区", "大兴区", "亦庄", "房山区", "门头沟区", "密云区", "平谷区", "通州区", "顺义区", "昌平区", "延庆区", "怀柔区" };
                    string[] series = new string[] { "一级建造师", "一级临时建造师", "二级建造师", "二级临时建造师", "监理师", "造价师" };
                    string[] zaiShi_series = new string[] { "在施一级建造师", "在施一级临时建造师", "在施二级建造师", "在施二级临时建造师", "在施监理师" };

                    //全市执业人员分布
                    #region 全市执业人员分布

                    string sql = @"select [TJDate],[Region], DataName,DataValue  from [dbo].[TJ_ZYRYCount] 	
                    where [TJDate] =(select max([TJDate])  from [dbo].[TJ_ZYRYCount] where [TJDate] <999999)
                    and [Region]='全市' and ([DataName] like '一级建造师' or [DataName] like '一级临时建造师' or [DataName] like '二级建造师' or [DataName] like '二级临时建造师' or [DataName] like '监理师' or [DataName] like '造价师');";

                    dt = CommonDAL.GetDataTable(sql);
                    //[{ value: 3380, name: '建造师' },{ value: 2176, name: '监理师' },{ value: 1089, name: '造价师' }]
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

                    LabelPersonCount.Text = string.Format("全市执业人员总量：{0}人", PersonCount);


                    #endregion 全市执业人员分布

                    //绑定执业人员增长趋势
                    BindPersonAdd();

                    //各区县人员总量及分布情况
                    #region 各区县人员总量及分布情况

                    sb.Remove(0, sb.Length);

                    sql = @"select DataName,replace(replace([Region],'宣武','西城'),'崇文','东城') as Region, sum(DataValue) as DataValue  from [dbo].[TJ_ZYRYCount] 	
                                where [TJDate] =(select max([TJDate])  from [dbo].[TJ_ZYRYCount] where [TJDate] <999999)
                                and [Region] <>'全市'
                                group by replace(replace([Region],'宣武','西城'),'崇文','东城'),DataName
								order by DataName,[Region]";

                    dt = CommonDAL.GetDataTable(sql);

                    DataColumn[] pk = new DataColumn[2];
                    pk[0] = dt.Columns["Region"];
                    pk[1] = dt.Columns["DataName"];
                    dt.PrimaryKey = pk;

                    foreach (string s in series)
                    {
                        sbItem.Remove(0, sbItem.Length);

                        sbrow.Append("<tr><td>").Append(s).Append("</td>");

                        foreach (string q in regions)
                        {
                            find = dt.Rows.Find(new object[] { q, s });
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

                    Div_RegionPerson.InnerHtml = string.Format("<table class=\"tb\"><tr><td></td><td>东城区</td><td>西城区</td><td>海淀区</td><td>朝阳区</td><td>石景山区</td><td>丰台区</td><td>大兴区</td><td>亦庄</td><td>房山区</td><td>门头沟区</td><td>密云区</td><td>平谷区</td><td>通州区</td><td>顺义区</td><td>昌平区</td><td>延庆区</td><td>怀柔区</td></tr>{0}</table>"
                        , sbrow.ToString()
                        );

                    #endregion 各区县人员总量及分布情况

                    //各区县在施人员总量及分布情况
                    #region 各区县在施人人员总量及分布情况

                    sb.Remove(0, sb.Length);
                    sbItem.Remove(0, sbItem.Length);
                    sbrow.Remove(0, sbrow.Length);

                    foreach (string s in zaiShi_series)
                    {
                        sbItem.Remove(0, sbItem.Length);

                        sbrow.Append("<tr><td>").Append(s).Append("</td>");

                        foreach (string q in regions)
                        {
                            find = dt.Rows.Find(new object[] { q, s });
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
                        sb.Append(string.Format(@",(name: '{0}',type: 'bar',stack:'在施一级建造师',data: [{1}],temStyle : ( normal: (barBorderRadius :5)))", s, sbItem.ToString()));
                        sbrow.Append("</tr>");
                    }
                    if (sb.Length > 0)
                    {
                        sb.Remove(0, 1).Replace("(", "{").Replace(")", "}");
                    }
                    ViewState["RegionZaiShiPersonValue"] = sb.ToString();

                    Div_ZaiShiRegionPerson.InnerHtml = string.Format("<table class=\"tb\"><tr><td></td><td>东城区</td><td>西城区</td><td>海淀区</td><td>朝阳区</td><td>石景山区</td><td>丰台区</td><td>大兴区</td><td>亦庄</td><td>房山区</td><td>门头沟区</td><td>密云区</td><td>平谷区</td><td>通州区</td><td>顺义区</td><td>昌平区</td><td>延庆区</td><td>怀柔区</td></tr>{0}</table>"
                        , sbrow.ToString()
                        );

                    #endregion 各区县在施人人员总量及分布情况

                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "全市人员生命周期，全市监控数据源获取失败", ex);
                }

            }
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
                            and [Region]='{1}' 					
					        group  by [TJDate],[Region]
					        order by [TJDate]";

            dt = CommonDAL.GetDataTable(string.Format(sql, DateTime.Now.ToString("yyyy-MM-dd"), "全市"));
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
                , string.Format("BindChartPersonAdd('{0}',{1},{2});", ViewState["personAdd"], DateTime.Now.AddMonths(-11).Month, minData - 500)
                , true);
        }
    }
}