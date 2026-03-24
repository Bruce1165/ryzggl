using DataAccess;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace ZYRYJG.SystemManage
{
    public partial class RoleManage : BasePage
    {
        private string RoleID = string.Empty; //角色ID
        private string RoleName = string.Empty; //角色名称
        private string BZ = string.Empty; //标志
        private RoleMDL Role = new RoleMDL(); //角色实例

        protected void Page_Load(object sender, EventArgs e)
        {
            RoleID = Request.QueryString["RoleID"] == null ? "" : Request.QueryString["RoleID"].ToString();
            RoleName = Request.QueryString["RoleName"] == null ? "" : Request.QueryString["RoleName"].ToString();
            BZ = Request.QueryString["BZ"] == null ? "" : Request.QueryString["BZ"].ToString();
            if (RoleID != "")
            {
                this.DivRoleList.Visible = false;
            }
            else
            {
                this.DivMenuList.Visible = false;
            }

            if (BZ == "")
            {
                this.RadGrid1.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
            }
            if (!IsPostBack)
            {
                this.lblRoleName.Text = RoleName;
                this.BindMenuList();
                BindRoleResourceList();
            }
        }

        //绑定数据
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid1.DataSource = RoleDAL.GetAllRole();
        }

        //行绑定事件
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            //UIHelp.Alert(Page, e.Item.GetType().ToString());
            
            if (e.Item is GridDataItem)
            {
                //序号列
                Label lbl = e.Item.FindControl("numberLabel") as Label;
                if (lbl == null) return;
                lbl.Text = (this.RadGrid1.PageSize * this.RadGrid1.CurrentPageIndex + e.Item.ItemIndex + 1).ToString();
            }
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                //菜单树
                GridEditFormItem editFormItem = e.Item as GridEditFormItem;
                RoleID = (editFormItem.FindControl("txtRoleID") as TextBox).Text.ToString();
                this.ViewState["RoleID"] = RoleID;
                RadTreeView rtv = editFormItem.FindControl("RadTreeView1") as RadTreeView;
                LoadRootNodes(rtv, TreeNodeExpandMode.ServerSide);
            }
        }

        //添加
        protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            //获取选中的菜单
            List<string> li = new List<string>();
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                GridEditFormItem editFormItem = e.Item as GridEditFormItem;
                RadTreeView rtv = editFormItem.FindControl("RadTreeView1") as RadTreeView;
                IList<RadTreeNode> nodeCollection = rtv.CheckedNodes;
                foreach (RadTreeNode node in nodeCollection)
                {
                    li.Add(node.Value);
                }
            }

            GridItem item = (GridItem)e.Item;
            string RoleName = (item.FindControl("TextBoxRoleName") as TextBox).Text;
            string RoleMemo = (item.FindControl("TextBoxRoleMemo") as TextBox).Text;

            Role.RoleName = RoleName;
            Role.Memo = RoleMemo;

            DBHelper db = new DBHelper();
            DbTransaction trans = db.BeginTransaction();
            try
            {
                if (RoleDAL.GetCountIsRoleName(RoleName) == 0)
                {
                    if (li.Count > 0)
                    {
                        RoleDAL.AddRole(trans, Role);  //角色添加
                        RoleDAL.AddRoleResource(trans, Role.RoleID, li);  //角色菜单关系添加
                        trans.Commit();
                        UIHelp.WriteOperateLog(UserName, UserID, "角色添加成功", string.Format("添加时间：{0}", DateTime.Now));
                        UIHelp.layerAlert(Page, "添加成功！");
                    }
                    else
                    {
                        UIHelp.layerAlert(Page, "请选择菜单！");
                    }
                }
                else
                {
                    UIHelp.layerAlert(Page, "该角色已存在！");
                }
            }
            catch (Exception ex)
            {
                trans.Rollback();
                UIHelp.WriteErrorLog(Page, "添加失败！", ex);
                return;
            }
        }

        //修改
        protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            //获取选中的菜单
            List<string> li = new List<string>();
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                GridEditFormItem editFormItem = e.Item as GridEditFormItem;
                RadTreeView rtv = editFormItem.FindControl("RadTreeView1") as RadTreeView;
                IList<RadTreeNode> nodeCollection = rtv.CheckedNodes;
                foreach (RadTreeNode node in nodeCollection)
                {
                    li.Add(node.Value);
                }
            }

            GridItem item = (GridItem)e.Item;
            string RoleID = item.OwnerTableView.DataKeyValues[item.ItemIndex]["RoleID"].ToString();
            string RoleName = (item.FindControl("TextBoxRoleName") as TextBox).Text;
            string RoleMemo = (item.FindControl("TextBoxRoleMemo") as TextBox).Text;
            Role.RoleID = RoleID;
            Role.RoleName = RoleName;
            Role.Memo = RoleMemo;

            DBHelper db = new DBHelper();
            DbTransaction trans = db.BeginTransaction();
            try
            {
                RoleMDL RE = RoleDAL.GetRoleByRoleID(RoleID);
                Role.OrderID = RE.OrderID;
                if (RE.RoleName != RoleName)
                {
                    if (RoleDAL.GetCountIsRoleName(RoleName) > 0)
                    {
                        UIHelp.layerAlert(Page, "该角色已存在！");
                        return;
                    }
                    else
                    {
                        if (li.Count > 0)
                        {
                            RoleDAL.AddRoleResource(trans, RoleID, li);
                            RoleDAL.ModifyRole(trans, Role);
                            trans.Commit();
                            UIHelp.WriteOperateLog(UserName, UserID, "角色修改成功", string.Format("保存时间：{0}", DateTime.Now));
                            UIHelp.layerAlert(Page, "修改成功！");
                        }
                        else
                        {
                            UIHelp.layerAlert(Page, "请选择菜单！");
                            return;
                        }
                    }
                }
                else
                {
                    if (li.Count > 0)
                    {
                        RoleDAL.AddRoleResource(trans, RoleID, li);
                        RoleDAL.ModifyRole(trans, Role);
                        trans.Commit();
                        UIHelp.layerAlert(Page, "修改成功！");
                    }
                    else
                    {
                        UIHelp.layerAlert(Page, "请选择菜单！");
                        return;
                    }
                }
                if (Cache["RoleResourceIDList"] != null) Cache.Remove("RoleResourceIDList");
                if (Cache["RoleResourceUrlList"] != null) Cache.Remove("RoleResourceUrlList");
            }
            catch (Exception ex)
            {
                trans.Rollback();
                UIHelp.WriteErrorLog(Page, "修改失败！", ex);
                return;
            }
        }

        //删除
        protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridItem item = (GridItem)e.Item;
            string RoleID = item.OwnerTableView.DataKeyValues[item.ItemIndex]["RoleID"].ToString();

            DBHelper db = new DBHelper();
            DbTransaction trans = db.BeginTransaction();
            try
            {
                RoleDAL.DeleteRoleResource(trans, RoleID);  //删除角色菜单关系
                RoleDAL.DeleteRoleByRoleID(trans, RoleID); //删除角色
                trans.Commit();
                UIHelp.layerAlert(Page, "删除成功！");
            }
            catch (Exception ex)
            {
                trans.Rollback();
                UIHelp.WriteErrorLog(Page, "删除失败！", ex);
                return;
            }
        }

        #region 动态加载菜单

        public void BindMenuList()
        {
            Hashtable hstable = new Hashtable();
            DataTable xz = ResourceDAL.GetAllRoleResourceByRoleID(RoleID);
            foreach (DataRow aa in xz.Rows)
            {
                if (!hstable.Contains(aa["MenuID"].ToString()))
                {
                    hstable.Add(aa["MenuID"].ToString(), aa["MenuID"].ToString());
                }
            }

            string list = string.Empty;
            list = "<table cellpadding='5' cellspacing='0' width='100%' style='border:1px solid #bdd8f4;font-size:small;'>";
            DataTable firstmenu = ResourceDAL.GetAllFirstResourceMenu();
            foreach (DataRow dr in firstmenu.Rows)
            {
                list += "<tr style='font-weight:bold; background-color:#e3f1ff;'><td colspan='4'>";
                if (hstable.Contains(dr["MenuID"].ToString()))
                {
                    list += "<input type='CheckBox' name='first' value='" + dr["MenuID"].ToString() + "' checked='checked' /><font color='Blue'/>";
                }
                else
                {
                    list += "<input type='CheckBox' name='first' value='" + dr["MenuID"].ToString() + "'/>";
                }
                list += dr["MenuName"].ToString() + "</td></tr>";
                DataTable childmenu = ResourceDAL.GetChildResourceMenuByMenuID(dr["MenuID"].ToString());
                list += "<tr>";
                int i = 0;
                foreach (DataRow row in childmenu.Rows)
                {
                    i++;
                    list += "<td>";
                    if (hstable.Contains(row["MenuID"].ToString()))
                    {
                        list += "<input type='CheckBox' name='child" + dr["MenuID"].ToString() + "' id='" + row["MenuID"].ToString() + "' runat='server'  value='" + row["MenuID"].ToString() + "' checked='checked'/><font color='Blue'/>";
                    }
                    else
                    {
                        list += "<input type='CheckBox' name='child" + dr["MenuID"].ToString() + "' id='" + row["MenuID"].ToString() + "' runat='server'  value='" + row["MenuID"].ToString() + "'/>";
                    }
                    list += row["MenuName"].ToString() + "</td>";
                    if (i % 4 == 0)
                    {
                        list += "</tr>";
                    }
                }
            }
            list += "</table>";
            this.MenuList.Text = list;
        }

        #endregion 动态加载菜单

        #region 菜单树

        private void LoadRootNodes(RadTreeView treeView, TreeNodeExpandMode expandMode)
        {
            DataTable firstmenu = ResourceDAL.GetAllFirstResourceMenu();

            foreach (DataRow row in firstmenu.Rows)
            {
                RadTreeNode node = new RadTreeNode();
                node.Text = row["MenuName"].ToString();
                node.Value = row["MenuID"].ToString();
                node.ImageUrl = "../Images/1034.gif";
                treeView.Nodes.Add(node);
                LoadChildNodes(node, node.Value);
            }
        }

        private void LoadChildNodes(RadTreeNode rtn, string MenuID)
        {
            DataTable childmenu = ResourceDAL.GetChildResourceMenuByMenuID(rtn.Value);
            foreach (DataRow dr in childmenu.Rows)
            {
                RadTreeNode node = new RadTreeNode();
                node.Text = dr["MenuName"].ToString();
                node.Value = dr["MenuID"].ToString();
                if (!Convert.ToBoolean(dr["IsMenu"].ToString()))
                {
                    node.ImageUrl = "../Images/ts.gif";
                }
                else
                {
                    node.ImageUrl = "../Images/1034.gif";
                }
                DataTable dd = ResourceDAL.GetAllRoleResourceByRoleID(RoleID);
                foreach (DataRow row in dd.Rows)
                {
                    if (row["MenuID"].ToString() == node.Value)
                    {
                        node.Checked = true;
                    }
                }
                rtn.Nodes.Add(node);
                LoadChildNodes(node, node.Value);
            }
        }

        #endregion 菜单树

        #region 角色资源列表

        //获取权限树
        public void getsub(ref DataRow dr, string ResourceID, ref int level, ref int Addcount)
        {
            DataTable sub = ResourceDAL.GetChildResourceMenuByMenuID(ResourceID);
            if (sub == null || sub.Rows.Count == 0) return;
            DataRow tempRow = null;
            foreach (DataRow drr in sub.Rows)
            {
                Addcount++;
                int newlevel = level + 1;
                DataRow dtnew = dr.Table.NewRow();

                if (dr.Table.Rows.IndexOf(dr) == -1)
                    dr.Table.Rows.Add(dtnew);
                else
                    dr.Table.Rows.InsertAt(dtnew, dr.Table.Rows.IndexOf(dr) + Addcount);
                dtnew["MenuID"] = drr["MenuID"].ToString();
                dtnew["MenuName"] = drr["MenuName"].ToString().PadLeft(drr["MenuName"].ToString().Length + newlevel - 1, '…').Replace("…", "… ");
                tempRow = dtnew;
                int count = 0;
                getsub(ref dtnew, drr["MenuID"].ToString(), ref newlevel, ref count);
                Addcount += count;
            }

        }

        // 绑定人员权限列表
        public void BindRoleResourceList()
        {
            try
            {
                //获取权限树结构
                DataTable dt = new DataTable();
                DataColumn dc1 = new DataColumn("MenuID", typeof(string));
                dt.Columns.Add(dc1);
                DataColumn dc2 = new DataColumn("MenuName", typeof(string));
                dt.Columns.Add(dc2);

                DataRow dr = dt.NewRow();
                int level = 0;
                int count = 0;
                getsub(ref dr, "root", ref level, ref count);

                DataColumn[] dcKeys = new DataColumn[1];
                dcKeys[0] = dt.Columns["MenuID"];
                dt.PrimaryKey = dcKeys;

                //绑定角色权限
                DataTable userTable = ResourceDAL.GetRoleResource();
                string RoleID = "";
                DataRow find = null;
                Dictionary<string, string> dic = new Dictionary<string, string>();
                foreach (DataRow r in userTable.Rows)
                {
                    if (RoleID != r["RoleID"].ToString())
                    {
                        dic.Add(r["RoleID"].ToString(), r["RoleName"].ToString());
                        DataColumn dc3 = new DataColumn(r["RoleID"].ToString(), typeof(string));
                        dt.Columns.Add(dc3);
                        RoleID = r["RoleID"].ToString();
                    }
                    find = dt.Rows.Find(r["MenuID"]);
                    if (find != null) find[r["RoleID"].ToString()] = "√";
                }
                RadGridRoleResource.DataSource = dt;

                RadGridRoleResource.MasterTableView.Columns.Clear();

                GridBoundColumn gc1 = new GridBoundColumn();
                gc1.HeaderText = "功能名称";
                gc1.UniqueName = "MenuName";
                gc1.DataField = "MenuName";
                RadGridRoleResource.MasterTableView.Columns.Add(gc1);

                foreach (string k in dic.Keys)
                {
                    GridBoundColumn gc = new GridBoundColumn();
                    gc.HeaderText = dic[k];
                    gc.UniqueName = k;
                    gc.DataField = k;

                    RadGridRoleResource.MasterTableView.Columns.Add(gc);
                }
                RadGridRoleResource.DataBind();
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "查看绑定人员权限列表失败！", ex);
                return;
            }
        }

        //导出excel
        protected void ButtonExportToExcel_Click(object sender, EventArgs e)
        {
            BindRoleResourceList();
            RadGridRoleResource.ExportSettings.ExportOnlyData = true;
            RadGridRoleResource.ExportSettings.OpenInNewWindow = true;
            RadGridRoleResource.MasterTableView.ExportToExcel();
        }

        //格式化Excel
        protected void RadGridRoleResource_ExcelExportCellFormatting(object source, ExcelExportCellFormattingEventArgs e)
        {
            if (e.FormattedColumn.UniqueName == "MenuName") e.Cell.Style["text-align"] = "left";

            //HeadCell
            GridItem item = e.Cell.Parent as GridItem;
            if (item.ItemIndex == 0 && e.FormattedColumn.UniqueName == "MenuName")
            {
                GridTableView gtv = e.Cell.Parent.Parent.Parent as GridTableView;
                GridItem ghi = gtv.GetItems(GridItemType.Header)[0];
                for (int i = 0; i < ghi.Cells.Count; i++)
                {
                    ghi.Cells[i].Style.Add("border-width", "0.1pt");
                    ghi.Cells[i].Style.Add("border-style", "solid");
                    ghi.Cells[i].Style.Add("border-color", "#CCCCCC");
                }
            }
            //Itemcell
            e.Cell.Attributes.Add("align", "center");
            e.Cell.Style.Add("border-width", "0.1pt");
            e.Cell.Style.Add("border-style", "solid");
            e.Cell.Style.Add("border-color", "#CCCCCC");

        }

        #endregion

        //绑定角色分配人员列表
        protected void RadGrid1_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            try
            {
                GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
                switch (e.DetailTableView.Name)
                {
                    case "RoleUser":
                        {
                            string RoleID = dataItem.GetDataKeyValue("RoleID").ToString();
                            e.DetailTableView.DataSource = CommonDAL.GetDataTable(string.Format(@"
SELECT U.USERID,U.RELUSERNAME,U.[OrganName],UR.[RoleID]
FROM   DBO.USERROLE AS UR 
INNER JOIN  [View_User] AS U ON UR.USERID=U.USERID
Where UR.[RoleID]={0}
ORDER BY U.ORGANID,U.RELUSERNAME ", RoleID));


                            break;
                        }

                }
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "查看角色绑定人员列表失败！", ex);
                return;
            }
        }
    }
}