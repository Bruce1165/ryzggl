using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using DataAccess;
using Model;

namespace ZYRYJG.jxjy
{
    public partial class UnitTrainPlan : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

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
            QueryParamOB q = new QueryParamOB();
            if (RadTextBoxPackageTitle.Text.Trim() != "")    //标题
            {
                q.Add(string.Format("PackageTitle like '%{0}%'", RadTextBoxPackageTitle.Text.Trim()));
            }
          
            if (RadioButtonListPublishStatus.SelectedValue != "")
            {
                q.Add(string.Format("Status='{0}'", RadioButtonListPublishStatus.SelectedValue));
            }


            if (PostSelect1.PostID!="")
            {
                q.Add(string.Format("[PostName] = '{0}'", PostSelect1.PostName));
            }
            else if (PostSelect1.PostTypeID != "")
            {
                q.Add(string.Format("[PostTypeName] = '{0}'", PostSelect1.PostTypeName));
            }
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridPackage.CurrentPageIndex = 0;
            RadGridPackage.DataSourceID = ObjectDataSource1.ID;
            RadGridPackage.DataBind();
        }

        //删除
        protected void RadGridPackage_DeleteCommand(object source, GridCommandEventArgs e)
        {
            //GridItem item = (GridItem)e.Item;
            //Int64 PackageID = Convert.ToInt64(item.OwnerTableView.DataKeyValues[item.ItemIndex]["PackageID"].ToString());

            //try
            //{
            //    PackageDAL.Delete(PackageID);
            //}
            //catch (Exception ex)
            //{
            //    UIHelp.WriteErrorLog(Page, "删除失败！", ex);
            //    return;
            //}
            //UIHelp.WriteOperateLog(UserName, UserID, "删除培训计划", string.Format("培训计划名称：{0}。", item.OwnerTableView.DataKeyValues[item.ItemIndex]["PackageTitle"].ToString()));

            //UIHelp.layerAlert(Page, "删除成功！");
        }

        protected void RadGridPackage_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                int count = PackageSourceDAL.SelectCount(string.Format(" and PackageID={0}", RadGridPackage.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PackageID"]));
                e.Item.Cells[RadGridPackage.MasterTableView.Columns.FindByUniqueName("SourceCount").OrderIndex].Text=count.ToString();
            }
        }

    }
}