using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAccess;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //获取近4年课程，按内容方向（lab）分类显示
            string sql = @"SELECT *
                              FROM [dbo].[Source]
                              where [ParentSourceID]=0 and [Status]='启用' and BarType is not null and  [SourceYear] in
                                (
                                    SELECT distinct top 4 [SourceYear]
                                    FROM [dbo].[Source]
                                    where BarType='工匠讲堂'
                                    union 
                                    SELECT distinct top 4 [SourceYear]
                                    FROM [dbo].[Source]
                                    where BarType='行业推荐精品课程'
                                    union 
                                    SELECT distinct top 4 [SourceYear]
                                    FROM [dbo].[Source]
                                    where BarType='首都建设云课堂'
                                )
                              order by case BarType when '工匠讲堂' then 1 when '首都建设云课堂' then 2 when '行业推荐精品课程' then 3 end ,[SourceYear] desc,[SortID]";
            DataTable dt = CommonDAL.GetDataTableDB("DBRYPX", sql);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string lab = "";
            int rowno = 0;
            foreach (DataRow r in dt.Rows)
            {
                if (r["BarType"].ToString() != lab)
                {
                    if (lab == "工匠讲堂")
                    {
                        sb.Append("</td></tr>");
                    }
                    sb.AppendFormat("<tr><td colspan='4' class='lab'>{0}</td></tr>", r["BarType"]);
                    if (r["BarType"].ToString() == "工匠讲堂")
                    {
                        sb.Append("<tr><td colspan='4' style='padding-top:26px;'>");
                    }

                    lab = r["BarType"].ToString();
                    rowno = 1;
                }
                if (lab == "工匠讲堂")
                {
                    sb.AppendFormat("<a href=\"../Student/WebClass.aspx?s={0}&t={1}\"><div class='gjjt'>{3}.　{2}</div></a>"
                       , Utility.Cryptography.Encrypt(r["SourceID"].ToString())
                        , r["BarType"]
                       , r["SourceName"]
                       , rowno
                       );
                }
                else
                {
                    sb.AppendFormat("<tr><td class='td_no'>{0}.</td><td class='td_kc'><a href=\"../Student/WebClass.aspx?s={1}&t={5}\">{2}</a></td><td class='td_js'>{3}</td><td class='td_dw'>{4}</td></tr>"
                        , rowno
                        , Utility.Cryptography.Encrypt(r["SourceID"].ToString())
                        , r["SourceName"]
                        , r["Teacher"]
                        , r["WorkUnit"]
                        , r["BarType"]
                        );
                }
                rowno += 1;
            }
            if (sb.Length == 0) return;

            sb.Insert(0, "<table class='table_kc'>");
            sb.Append("</table>");

            divKe.InnerHtml = sb.ToString();
        }
    }

}