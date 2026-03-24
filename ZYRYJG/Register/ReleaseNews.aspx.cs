using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;

namespace ZYRYJG.Register
{
    public partial class ReleaseNews : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                ButtonSearch_Click(sender, e);
            }
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ObjectDataSource1.SelectParameters.Clear();
            var q = new QueryParamOB();
            if (RadTextBoxValue.Text.Trim()!="")
            {
                q.Add(string.Format("[Title] like '%{0}%'", RadTextBoxValue.Text));
            }
            switch (RadComboBox_Release.SelectedValue)
            {   
                
                case "全部":
                break;
                case"已发布":
                q.Add(string.Format("States='1'"));
                break;
                case"未发布":
                q.Add(string.Format("States='0'"));
                break;

            }
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridQY.CurrentPageIndex = 0;
            RadGridQY.DataSourceID = ObjectDataSource1.ID;
        }


        //删除数据
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            string deletelist = GetRadGridADDRYSelect();

            if (deletelist.Length > 0)
            {
                DataAccess.PolicyNewsDAL.Delete(deletelist);
                ButtonSearch_Click(sender, e);
                UIHelp.WriteOperateLog(UserName, UserID, "政策删除成功", string.Format("删除时间：{0}", DateTime.Now));
                UIHelp.layerAlert(Page, "删除成功！");
             
            }
            else
            {
                UIHelp.layerAlert(Page, "您尚未勾选任何人员！");
                return;
            
            }
            Utility.CacheHelp.RemoveCache(Page, "DataTableZCTZ");
        }

        //获取表格勾选集合
        private string GetRadGridADDRYSelect()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i < RadGridQY.MasterTableView.Items.Count; i++)
            {
                CheckBox CheckBox1 = RadGridQY.Items[i].FindControl("CheckBox1") as CheckBox;
                if (CheckBox1.Checked)
                {
                    sb.Append(",'").Append(RadGridQY.MasterTableView.DataKeyValues[i]["ID"].ToString()).Append("'");
                }
            }
            if (sb.Length > 0)
            {
                sb.Remove(0, 1);
            }
            return sb.ToString();
        }

        //发布
        protected void ButtonRelease_Click(object sender, EventArgs e)
        {
            string Releaselist = GetRadGridADDRYSelect();
            if (Releaselist.Length > 0)
            {
                string sql = string.Format("Update [PolicyNews] set States='1',GetDateTime='{0}' where ID in ({1})", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Releaselist);
                CommonDAL.ExecSQL(sql);
            }
            UIHelp.WriteOperateLog(UserName, UserID, "政策发布成功", string.Format("发布时间：{0}", DateTime.Now));
            ButtonSearch_Click(sender, e);
            UIHelp.layerAlert(Page, "政策发布成功");

            Utility.CacheHelp.RemoveCache(Page, "DataTableZCTZ");
          
        }

        protected void RadComboBox_Release_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ButtonSearch_Click(null, null);
        }
    }
}