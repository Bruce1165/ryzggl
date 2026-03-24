using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using DataAccess;

namespace ZYRYJG.CheckMgr
{
    public partial class RandomCheckSheBao : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindYhrwbh();
            }
        }

        //绑定可选的双随机摇号任务编号
        protected void BindYhrwbh()
        {
            if (RootUrl.ToLower().Contains("localhost") == false)
            {
                Response.Write("测试环境，不发起服务评价，请关闭此页面。");
                return;
            }
            DataTable dt = CommonDAL.GetDataTable(string.Format("select distinct Yhrwbh from {0}.dbo.Gx_Task_ToZczx order by Yhrwbh"
                , RootUrl.ToLower().Contains("localhost") == false ? "[192.168.7.175].[LESP_3.0_DataExchange]" : "[LESP_3.0]"));
            ListBoxYhrwbh.DataSource = dt;
            ListBoxYhrwbh.DataTextField = "Yhrwbh";
            ListBoxYhrwbh.DataValueField = "Yhrwbh";
            ListBoxYhrwbh.DataBind();
            ListBoxYhrwbh.SelectedIndex = 0;
            BindRadGridTask();
        }

        protected void BindRadGridTask()
        {
            DataTable dt = CommonDAL.GetDataTable(string.Format("select *,row_number() over(order by [ID]) as RowNum  from {1}.dbo.Gx_Task_ToZczx Where Yhrwbh like '{0}'"
               , ListBoxYhrwbh.SelectedValue == "" ? "%" : ListBoxYhrwbh.SelectedValue
               , RootUrl.ToLower().Contains("localhost") == false ? "[192.168.7.175].[LESP_3.0_DataExchange]" : "[LESP_3.0]"
               ));
            RadGridTask.DataSource = dt;
            RadGridTask.DataBind();
            if (dt.Rows.Count > 0)
            {
                LabelYhrwbh.Text = dt.Rows[0]["Yhrwbh"].ToString();
                LabelRwmc.Text = dt.Rows[0]["Rwmc"].ToString();
                LabelYhsj.Text = Convert.ToDateTime(dt.Rows[0]["Yhsj"]).ToString("yyyy-MM-dd");
                LabelCount.Text = string.Format("{0}人", dt.Rows.Count);
            }
        }

        protected void ListBoxYhrwbh_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindRadGridTask();
        }
    }
}