using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using Telerik.Web.UI;
using System.Data;

namespace ZYRYJG.jxjy
{
    public partial class SourceSearch : BasePage
    {
        protected override bool IsNeedLogin
        {
            get
            {
                return false;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                for (int i = 2023; i <= (DateTime.Now.Year + 1); i++)
                {
                    RadComboBoxSourceYear.Items.Insert(0, new RadComboBoxItem(i.ToString(), i.ToString()));
                }
                RadComboBoxSourceYear.Items.Insert(0, new RadComboBoxItem("全部", ""));
                ButtonSearch_Click(sender, e);
            }
        }
     

        //查询
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ObjectDataSource1.SelectParameters.Clear();
            ObjectDataSource1.SelectParameters.Clear();
            QueryParamOB q = new QueryParamOB();
            q.Add("ParentSourceID=0");

            if (RadioButtonListSourceType.SelectedValue != "")//课程类型
            {
                q.Add(string.Format("SourceType='{0}'", RadioButtonListSourceType.SelectedValue));
            }
            if (RadTextBoxSourceName.Text.Trim() != "")    //课程名称
            {
                q.Add(string.Format("SourceName like '%{0}%'", RadTextBoxSourceName.Text.Trim()));
            }
            if (RadTextBoxTeacher.Text.Trim() != "")    //教师
            {
                q.Add(string.Format("Teacher like '%{0}%'", RadTextBoxTeacher.Text.Trim()));
            }
            if (RadComboBoxSourceYear.SelectedValue != "")
            {
                q.Add(string.Format("SourceYear={0}", RadComboBoxSourceYear.SelectedValue));
            }
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());

            RadGridSource.CurrentPageIndex = 0;
            RadGridSource.DataSourceID = ObjectDataSource1.ID;
          
        }

        protected void RadGridSource_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                e.Item.Style.Add("cursor", "pointer");
                e.Item.Attributes.Add("onclick", string.Format("returnToParent('{0}','{1}')"
                    ,e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["SourceID"]
                    ,e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["SourceName"]
                    ));
            }
        }
     
    }
}
