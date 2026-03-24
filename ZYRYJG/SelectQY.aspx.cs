using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;
using Telerik.Web.UI;

namespace ZYRYJG
{
    public partial class SelectQY : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ButtonSearch_Click(sender, e);
            }
        }

        //根据条件查询
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }

            ObjectDataSource1.SelectParameters.Clear();
            var q = new QueryParamOB();
            if (RadTextBoxENT_OrganizationsCode.Text.Trim() != "")//组织机构代码
            {
                q.Add(string.Format("ENT_OrganizationsCode like '%{0}%' or CreditCode like '%{0}%'", RadTextBoxENT_OrganizationsCode.Text.Trim()));
            }
            if (RadTextBoxENT_Name.Text.Trim() != "")//企业名称
            {
                q.Add(string.Format("ENT_Name like '%{0}%'", RadTextBoxENT_Name.Text.Trim()));
            }
            q.Add("len(ENT_OrganizationsCode) =9");//过滤掉组织机构代码不正确的数据



            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());

            GridSortExpression sortStr1 = new GridSortExpression();
            sortStr1.FieldName = "[CreditCode] desc,[ENT_City]";
            sortStr1.SortOrder = GridSortOrder.Descending;
            RadGridQY.MasterTableView.SortExpressions.AddSortExpression(sortStr1);

            RadGridQY.CurrentPageIndex = 0;
            RadGridQY.DataSourceID = ObjectDataSource1.ID;
        }

        protected void RadGridQY_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                e.Item.Style.Add("cursor", "pointer");
                e.Item.Attributes.Add("onclick", string.Format("setdata('{0}','{1}','{2}')"
                    , e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ENT_Name"]
                    , e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ENT_OrganizationsCode"]
                    , e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CreditCode"]
                    ));


            }
        }

    }
}