using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Telerik.Web.UI;
using DataAccess;
using System.Data;

namespace ZYRYJG.CheckMgr
{
    public partial class ApplyCheckMgr : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (ValidResourceIDLimit(RoleIDs, "ApplyCheckEdit") == true)
                {
                    RadGridTask.MasterTableView.Columns.FindByUniqueName("Edit").Visible = true;
                    RadGridTask.MasterTableView.Columns.FindByUniqueName("Delete").Visible = true;
                    ButtonNew.Visible = true;
                }
                for (int i = DateTime.Now.Year -5; i <= DateTime.Now.Year; i++)
                {
                    RadComboBoxYear.Items.Insert(0, new RadComboBoxItem(i.ToString(), i.ToString()));
                }
                RadComboBoxYear.Items.Insert(0, new RadComboBoxItem("全部", ""));
                BindData();
            }
        }

        protected void BindData()
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ObjectDataSource1.SelectParameters.Clear();

            QueryParamOB q = new QueryParamOB();

            if (RadComboBoxYear.SelectedValue != "")
            {
                DateTime dtime ;
                if (RadComboBoxMonth.SelectedValue != "")
                {
                    dtime = Convert.ToDateTime(string.Format("{0}-{1}-01", RadComboBoxYear.SelectedValue, RadComboBoxMonth.SelectedValue));
                    q.Add(string.Format("cjsj >= '{0}' and cjsj < '{1}'", dtime.ToString("yyyy-MM-dd"), dtime.AddMonths(1).ToString("yyyy-MM-dd")));
                }
                else
                {
                    dtime = Convert.ToDateTime(string.Format("{0}-01-01", RadComboBoxYear.SelectedValue));
                    q.Add(string.Format("cjsj >= '{0}' and cjsj < '{1}'", dtime.ToString("yyyy-MM-dd"), dtime.AddYears(1).ToString("yyyy-MM-dd")));
                }
            }           

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridTask.CurrentPageIndex = 0;
            RadGridTask.DataSourceID = ObjectDataSource1.ID;
        }

        protected void RadGridTask_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "ButtonDelete")
            {
                long TaskID = Convert.ToInt64(RadGridTask.MasterTableView.DataKeyValues[e.Item.ItemIndex]["TaskID"]);
                
                try
                {
                    ApplyCheckTaskDAL.Delete(TaskID);
                    UIHelp.layerAlert(this.Page, "删除成功！", 6, 3000);
                    RadGridTask.DataBind();

                    UIHelp.WriteOperateLog(PersonName, UserID, "删除业务申请单抽任务", string.Format("TaskID：{0}。", TaskID));
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "删除业务申请单抽任务失败！", ex);
                    return;
                }

            }
        }

        protected void RadComboBoxYear_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindData();
        }

        protected void RadComboBoxMonth_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindData();
        }
     
    }
}