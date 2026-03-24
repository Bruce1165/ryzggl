using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using DataAccess;
using Model;

namespace ZYRYJG.SystemManage
{
    public partial class main : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                RadGrid1.DataSourceID = ObjectDataSource1.ID;
        }

        protected void RadGrid1_ItemDeleted(object source, GridDeletedEventArgs e)
        {
            //string item = getItemName(e.Item.OwnerTableView.Name);
            //string field = getFieldName(e.Item.OwnerTableView.Name);
            if (e.Exception != null)
            {
                e.ExceptionHandled = true;
                //DisplayMessage(item + " " + e.Item[field].Text + " cannot be deleted. Reason: " + e.Exception.Message);
            }
            else
            {
                //DisplayMessage(item + " " + e.Item[field].Text + " deleted");
            }
        }

        protected void RadGrid1_ItemUpdated(object source, Telerik.Web.UI.GridUpdatedEventArgs e)
        {
            //string item = getItemName(e.Item.OwnerTableView.Name);
            //string field = getFieldName(e.Item.OwnerTableView.Name);
            if (e.Exception != null)
            {
                e.KeepInEditMode = true;
                e.ExceptionHandled = true;
                //DisplayMessage(item + " " + e.Item[field].Text + " cannot be updated. Reason: " + e.Exception.Message);
            }
            else
            {
                //DisplayMessage(item + " " + e.Item[field].Text + " updated");
            }
        }

        //添加
        protected void RadGrid1_ItemInserted(object source, GridInsertedEventArgs e)
        {
            //GridEditableItem editedItem = e.Item as GridEditableItem;
            //PostInfoOB postInfoOB=new PostInfoOB();
            //UIHelp .GetData(editedItem,postInfoOB); 
            //postInfoOB .PostType ="1";  //岗位
            //postInfoOB .UpPostID=0;
            //DBHelper db = new DBHelper();
            //DbTransaction trans = db.BeginTransaction();
            //try
            //{
            //    PostInfoDAL.Insert(trans,postInfoOB);
            //    trans.Commit();
            //}
            //catch (Exception ex)
            //{
            //    trans.Rollback();
            //    UIHelp.WriteErrorLog(Page, "添加岗位失败！", ex);
            //}
            //UIHelp.layerAlert(Page, "添加岗位成功！");
            string item = getItemName(e.Item.OwnerTableView.Name);
            if (e.Exception != null)
            {
                e.ExceptionHandled = true;
                DisplayMessage(item + " cannot be inserted. Reason: " + e.Exception.Message);
            }
            else
            {
                DisplayMessage(item + " inserted");
            }

        }

        protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
        {
            if ("Customers".Equals(e.Item.OwnerTableView.Name))
            {
                GridDataItem parentItem = (GridDataItem)e.Item.OwnerTableView.ParentItem;
                //SessionDataSource2.InsertParameters["CustomerID"].DefaultValue = parentItem.OwnerTableView.DataKeyValues[parentItem.ItemIndex]["CustomerID"].ToString();
                PostInfoOB postInfoOB = new PostInfoOB();
                postInfoOB.PostType = "1";  //岗位
                postInfoOB.UpPostID = 0;
                DBHelper db = new DBHelper();
                DbTransaction trans = db.BeginTransaction();
                try
                {
                   
                    e.Item.OwnerTableView.InsertItem(postInfoOB);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    UIHelp.WriteErrorLog(Page, "添加岗位失败！", ex);
                }
                UIHelp.layerAlert(Page, "添加岗位成功！",6,3000);
                
            }
            else if ("Project".Equals(e.Item.OwnerTableView.Name))
            {
                GridDataItem parentItem = (GridDataItem)e.Item.OwnerTableView.ParentItem;
                //SessionDataSource3.InsertParameters["OrderID"].DefaultValue = parentItem.OwnerTableView.DataKeyValues[parentItem.ItemIndex]["OrderID"].ToString();
            }
        }

        private void DisplayMessage(string text)
        {
            RadGrid1.Controls.Add(new LiteralControl(string.Format("<span style='color:red'>{0}</span>", text)));
        }

        protected void RadGrid1_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
            switch (e.DetailTableView.Name)
            {
                case "Post":
                    {
                        string upPostId = dataItem.GetDataKeyValue("PostID").ToString();
                        e.DetailTableView.DataSource = PostInfoDAL.GetList(0, int.MaxValue - 1, string.Format("and PostType='2' and UpPostID={0}", upPostId), "CODEFORMAT,PostName");
                       
                        break;
                    }

                case "Project":
                    {
                        string upPostId = dataItem.GetDataKeyValue("PostID").ToString();
                        e.DetailTableView.DataSource = PostInfoDAL.GetList(0, int.MaxValue - 1, string.Format("and PostType='3' and UpPostID={0}", upPostId), "POSTORDER,PostName");
                        break;
                    }
            }

        }

        private String getItemName(string tableName)
        {
            switch (tableName)
            {
                case ("Customers"):
                    {
                        return "Customer";
                    }
                case ("Post"):
                    {
                        return "Post";
                    }
                case ("Project"):
                    {
                        return "Project for order";
                    }
                default: return "";

            }
        }
        private String getFieldName(string tableName)
        {
            switch (tableName)
            {
                case ("Customers"):
                    {
                        return "PostID";
                    }
                case ("Post"):
                    {
                        return "PostID";
                    }
                case ("Project"):
                    {
                        return "UpPostID";
                    }
                default: return "";
            }
        }

    }
}
