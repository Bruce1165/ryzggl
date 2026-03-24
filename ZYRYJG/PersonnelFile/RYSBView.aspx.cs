using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Net;
using System.IO;
using System.Data;
using Model;
using DataAccess;
using System.Drawing;

namespace ZYRYJG.PersonnelFile
{
    public partial class RYSBView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //o1=WorkerCertificateCode&o2=UnitCode&o3=ApplyDate
                if (!string.IsNullOrEmpty(Request["o1"]) && !string.IsNullOrEmpty(Request["o2"]) && !string.IsNullOrEmpty(Request["o3"]))
                {
                    if (Convert.ToDateTime(Request["o3"]).CompareTo(DateTime.Parse("2014-07-01")) <= 0)
                    {
                        main.InnerHtml = "<span style='color:red;'>社保比对于2014-07-01日启用，发生在之前的业务没有进行社保比对。</span>";
                    }
                    else if (Convert.ToDateTime(Request["o3"]).ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
                    {
                        main.InnerHtml = "<span style='color:red;'>系统无法查询当日提交业务的社保记录，请次日进行查看。</span>";
                    }
                    else
                    {
                        string sql=@"select * from
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
	                                        ,'{0}' as CheckUnitCode
                                        FROM [dbo].[SheBao]
                                        where [CertificateCode]='{1}' and [JFYF] between  {2} and  {3}
                                    ) p
                                    PIVOT(MAX([XZName]) FOR [XZCode] IN ([01],[02],[03],[04],[05])) AS T";
                        DataTable dt = CommonDAL.GetDataTable(string.Format(sql, Request["o2"]
                            , Request["o1"]
                            , Convert.ToDateTime(Request["o3"]).AddMonths(-2).ToString("yyyyMM")
                            , Convert.ToDateTime(Request["o3"]).AddMonths(-1).ToString("yyyyMM")));

                        RadGrid1.DataSource = dt;
                        if (dt == null)
                        {
                            if (Convert.ToDateTime(Request["o3"]).ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
                            {
                                main.InnerHtml = "<span style='color:red;'>系统无法查询当日提交业务的社保记录，请次日进行查看。</span>";
                                return;
                            }
                            else
                            {
                                RadGrid1.DataBind();
                            }
                        }
                        else
                        {
                            RadGrid1.DataBind();

                            UnitMDL _UnitMDL = UnitDAL.GetObjectByENT_OrganizationsCode(Request["o2"]);
                            p_CheckUnit.InnerHtml = string.Format("申请核验社保单位：<b style='font-size:20px;color:{1}'>{0}</b>"
                                , _UnitMDL.ENT_Name
                                , (dt.Rows.Count == 0 || dt.Rows[dt.Rows.Count - 1]["CreditCode"].ToString() != _UnitMDL.CreditCode?"red":"black")
                                );
                        }
                        // //string Url = string.Format("http://192.168.150.162/RBCJ/SearchInfo.aspx?UserName=xzzxt&SFZHM={0}&ZZJGDM={1}&SBYF={2}"
                        ////要抓取的URL地址 
                        //try
                        //{
                        //    string Url = string.Format("{3}?UserName=xzzxt&SFZHM={0}&ZZJGDM={1}&SBYF={2}"
                        //        , Base64Code(Request["o1"])//身份证
                        //        , Base64Code(Request["o2"].Substring(0, 8))//组织机构代码，前8位
                        //        , Base64Code(Convert.ToDateTime(Request["o3"]).ToString("yyyyMM"))//申报月份，YYYYMM  
                        //        , MyWebConfig.SheBaoSearch
                        //        );

                        //    //得到指定Url的源码 
                        //    string strWebContent = UIHelp.GetWebContent(Url);

                        //    int iStart = strWebContent.IndexOf("<div id=\"printdiv\">", 0);
                        //    int iTableStart = strWebContent.IndexOf("<table", iStart);
                        //    int iTableEnd = strWebContent.IndexOf("</table>", iTableStart);
                        //    main.InnerHtml = strWebContent.Substring(iTableStart, iTableEnd - iTableStart + 8);
                        //}
                        //catch(Exception ex)
                        //{
                        //    Utility.FileLog.WriteLog("查询社保结果失败。", ex);
                        //    main.InnerHtml = "<span style='color:red;'>系统忙，查询社保结果失败。</span>";
                        //}
                    }
                }

            }
        }

        private string Base64Code(string Message)
        {
            byte[] bytes = Encoding.Default.GetBytes(Message);
            return Convert.ToBase64String(bytes);
        }      

          //<%-- <telerik:GridTemplateColumn UniqueName="01" HeaderText="养老">
          //                      <ItemTemplate>
          //                          <%# Eval("01") != System.DBNull.Value ? "是" : "否"%>
          //                      </ItemTemplate>
          //                      <HeaderStyle HorizontalAlign="Center" Wrap="false" />
          //                      <ItemStyle HorizontalAlign="Center" />
          //                  </telerik:GridTemplateColumn>
          //                  <telerik:GridTemplateColumn UniqueName="02" HeaderText="失业">
          //                      <ItemTemplate>
          //                          <%# Eval("02") != System.DBNull.Value ? "是" : "否"%>
          //                      </ItemTemplate>
          //                      <HeaderStyle HorizontalAlign="Center" Wrap="false" />
          //                      <ItemStyle HorizontalAlign="Center" />
          //                  </telerik:GridTemplateColumn>
          //                  <telerik:GridTemplateColumn UniqueName="03" HeaderText="医疗">
          //                      <ItemTemplate>
          //                          <%# Eval("03") != System.DBNull.Value ? "是" : "否"%>
          //                      </ItemTemplate>
          //                      <HeaderStyle HorizontalAlign="Center" Wrap="false" />
          //                      <ItemStyle HorizontalAlign="Center" />
          //                  </telerik:GridTemplateColumn>
          //                  <telerik:GridTemplateColumn UniqueName="04" HeaderText="工伤">
          //                      <ItemTemplate>
          //                          <%# Eval("04") != System.DBNull.Value ? "是" : "否"%>
          //                      </ItemTemplate>
          //                      <HeaderStyle HorizontalAlign="Center" Wrap="false" />
          //                      <ItemStyle HorizontalAlign="Center" />
          //                  </telerik:GridTemplateColumn>
          //                  <telerik:GridTemplateColumn UniqueName="05" HeaderText="生育">
          //                      <ItemTemplate>
          //                          <%# Eval("05") != System.DBNull.Value ? "是" : "否"%>
          //                      </ItemTemplate>
          //                      <HeaderStyle HorizontalAlign="Center" Wrap="false" />
          //                      <ItemStyle HorizontalAlign="Center" />
          //                  </telerik:GridTemplateColumn>--%>
    }
}