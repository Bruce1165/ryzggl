using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using DataAccess;

namespace ZYRYJG.jxjy
{
    public partial class WenjuanTj : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                DataTable dt= CommonDAL.GetDataTable("SELECT distinct [SaveYear]  FROM [dbo].[WenJuanSub]");
                ListBoxYear.DataSource = dt;
                ListBoxYear.DataTextField = "SaveYear";
                ListBoxYear.DataValueField = "SaveYear";
                ListBoxYear.DataBind();
                ListBoxYear.Items.Insert(0, new ListItem("全部",""));
                ListBoxYear.SelectedIndex = 0;
                tj();
            }
        }

        protected void tj()
        {
            string sql = @"
SELECT w.[QuestionID]
      ,w.[Flag]
      ,w.[QuestionType]
      ,w.[QuestionNo]
      ,w.[Title]
      ,w.[A]
      ,w.[B]
      ,w.[C]
      ,w.[D]
      ,w.[E]
      ,w.[F]
	  ,Apre *100 / sumCount as Apre
	  ,Bpre *100 / sumCount as Bpre
	  ,Cpre *100 / sumCount as Cpre
	  ,Dpre *100 / sumCount as Dpre
	  ,Epre *100 / sumCount as Epre
	  ,Fpre *100 / sumCount as Fpre

  FROM [dbo].[WenJuan] w
  left join 
  (select [QuestionID],count(*) as sumCount,
	  sum(case when [CheckItem] like '%A%' then 1 else 0 end) as Apre,
	  sum(case when [CheckItem] like '%B%' then 1 else 0 end) as Bpre,
	  sum(case when [CheckItem] like '%C%' then 1 else 0 end) as Cpre,
	  sum(case when [CheckItem] like '%D%' then 1 else 0 end) as Dpre,
	  sum(case when [CheckItem] like '%E%' then 1 else 0 end) as Epre,
	  sum(case when [CheckItem] like '%F%' then 1 else 0 end) as Fpre
  from [dbo].[WenJuanSub]
  Where  [SaveYear] like '{0}'
  group by [QuestionID]
  ) s on  w.[QuestionID] = s.[QuestionID]";

            DataTable dt = CommonDAL.GetDataTable(string.Format(sql, ListBoxYear.SelectedValue == "" ? "%" : ListBoxYear.SelectedValue));
            RadGridQuestion.DataSource = dt;
            RadGridQuestion.DataBind();
        }

        protected void ListBoxYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            tj();
        }
    }
}