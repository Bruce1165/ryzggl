using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Model;
using DataAccess;
using System.Text;


namespace ZYRYJG.County
{
    public partial class TransactStatistical : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BtnQuery_Click(sender, e);
            }
        }

        protected void BtnQuery_Click(object sender, EventArgs e)
        {
             StringBuilder sxlb = new StringBuilder();
            //事项数量
            StringBuilder sxsl = new StringBuilder();

           

            QueryParamOB q = new QueryParamOB();
            if (IfExistRoleID("3") == true || IfExistRoleID("7") == true)//区县
            {
                //如果为区县，只显示自己区县的数据
                q.Add(string.Format("ENT_City like '{0}%'", Region));
            }
            q.Add(string.Format("GetDateTime >='{0}'",RadDatePickerStart.SelectedDate.HasValue ==true?RadDatePickerStart.SelectedDate.Value.ToString("yyyy-MM-dd"):"2000-01-01"));
            q.Add(string.Format("GetDateTime <='{0}'", RadDatePickerEnd.SelectedDate.HasValue == true ? RadDatePickerEnd.SelectedDate.Value.ToString("yyyy-MM-dd 23:59:59") : "2050-01-01"));

            DataTable dt = ApplyDAL.GetApplyGetMan(q);
            DataColumn[] k = new DataColumn[1];
            k[0] = dt.Columns["GetMan"];
            dt.PrimaryKey = k;

            DataTable dtCount = ApplyDAL.GetApplyGetManDoCount(q);
            DataRow find = null;
            foreach(DataRow r in dtCount.Rows)
            {
                find = dt.Rows.Find(r["GetMan"]);
                if (find != null)
                {
                    find[r["ApplyType"].ToString()] = r["num"];
                }
            }

         
            int[] sum = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["RowNum"] = i + 1;
                for (int j = 0; j < dt.Columns.Count - 2; j++)
                {
                    sum[j] += Convert.ToInt32(dt.Rows[i][j + 2]);
                }

                sxlb.Append("\"");
                sxlb.Append(dt.Rows[i]["GetMan"]);
                sxlb.Append("\"");
                sxlb.Append(",");
              
                sxsl.Append("{");
                sxsl.Append("value:" + dt.Rows[i]["小计"] + ",name:'" + dt.Rows[i]["GetMan"] + "'");
                sxsl.Append("}");
                sxsl.Append(",");
            }

            //移除三个字符串最后一次加上的逗号
            if (sxlb.Length > 0)
                sxlb.Remove(sxlb.Length - 1, 1);
            if (sxsl.Length > 0)
                sxsl.Remove(sxsl.Length - 1, 1);

            //ViewState["SXLX"] = sxlb.ToString();
            //ViewState["SXSL"] = sxsl.ToString();

            sum[7] = sum[0] + sum[1] + sum[2] + sum[3] + sum[4] + sum[5] + sum[6];

            DataRow drsum = dt.NewRow();
            drsum["GetMan"] = "总计";
            dt.Rows.Add(drsum);

            int lastrow = dt.Rows.Count;
            drsum["RowNum"] = lastrow ;

            for (int j = 0; j < dt.Columns.Count - 2; j++)
            {
                dt.Rows[lastrow -1][j +2] = sum[j];
            }

            RadGridTJ.DataSource = dt;
            RadGridTJ.DataBind();
        }
    }
}