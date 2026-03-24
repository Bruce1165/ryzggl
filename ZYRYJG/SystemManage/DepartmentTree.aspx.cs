using System;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using DataAccess;

namespace ZYRYJG.SystemManage
{
    public partial class DepartmentTree : BasePage
    {
        //private static new string ID = string.Empty;  //根据ID值判断部门和用户 1：部门  2：用户

        //protected override bool IsNeedLogin
        //{
        //    get
        //    {
        //        return false;
        //    }
        //}

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    ID = Request.QueryString["ID"].ToString();

        //    if (!Page.IsPostBack)
        //    {
        //        RadTreeNode rtn = new RadTreeNode();
        //        rtn.Text = "全市";
        //        rtn.Value = "98371098-6C95-4AA1-AE86-C7EAA30E8DF9";
        //        rtn.Style.Add("Color", "#0066cc");
        //        if (ID == "2")  //用户页面
        //        {
        //            rtn.NavigateUrl = "UserManage.aspx?OrganID=" + rtn.Value + "&BZ=1";
        //        }
        //        else
        //        {
        //            rtn.NavigateUrl = "DepartmentManage.aspx?OrganID=" + rtn.Value;
        //        }
        //        rtn.Target = "right";
        //        rtn.Expanded = true;
        //        this.RadTreeView1.Nodes.Add(rtn);
        //        LoadRootNodes(rtn, TreeNodeExpandMode.ServerSide);
        //    }
        //}

        ////获取根节点(机构)
        //private static void LoadRootNodes(RadTreeNode tn, TreeNodeExpandMode expandMode)
        //{
        //    DataTable organ = OrganizationDAL.GetChildOrgan("01", 4);
        //    foreach (DataRow row in organ.Rows)
        //    {
        //        RadTreeNode node = new RadTreeNode();
        //        node.Text = row["OrganName"].ToString();
        //        node.Value = row["OrganID"].ToString();
        //        node.Style.Add("Color", "#0066cc");
        //        if (ID == "2")
        //        {
        //            node.NavigateUrl = "UserManage.aspx?OrganID=" + row["OrganID"].ToString() + "&BZ=1";
        //        }
        //        else
        //        {
        //            node.NavigateUrl = "DepartmentManage.aspx?OrganID=" + row["OrganID"].ToString() + "&BZ=1";
        //        }
        //        node.Target = "right";
        //        //node.ExpandMode = expandMode;
        //        node.Expanded = true;
        //        tn.Nodes.Add(node);
        //        GetOrganChildNodes(node, node.Value.ToString());

        //    }
        //}

        ////读取
        //private static void GetOrganChildNodes(RadTreeNode rtn, string DeptID)
        //{
        //    ////if (e.Node.Level >= 2)//部门
        //    ////{
        //    DataTable firstDept = DepartmentDAL.GetFirstDepartment(rtn.Value);
        //    foreach (DataRow dr in firstDept.Rows)
        //    {
        //        RadTreeNode node = new RadTreeNode();
        //        node.Text = dr["DeptName"].ToString();
        //        node.Value = dr["DeptID"].ToString();
        //        node.Style.Add("Color", "#0066cc");
        //        if (ID == "2")
        //        {
        //            node.NavigateUrl = "UserManage.aspx?OrganID=" + rtn.Value + "&DeptID=" + node.Value + "&BZ=1";
        //        }
        //        else
        //        {
        //            node.NavigateUrl = "DepartmentManage.aspx?OrganID=" + rtn.Value + "&DeptID=" + node.Value + "&BZ=1";
        //        }
        //        node.Target = "right";
        //        //rtn.ExpandMode = TreeNodeExpandMode.ServerSide;
        //        rtn.Nodes.Add(node);
        //        node.Expanded = true;
        //        GetAllChildNodes(node, node.Value.ToString());
        //    }
        //    //}
        //    //else //机构
        //    //{
        //    //    DataTable childorgan = OrganizationDAL.GetChildOrgan(e.Node.Value, 6);
        //    //    {
        //    //        foreach (DataRow row in childorgan.Rows)
        //    //        {
        //    //            RadTreeNode node = new RadTreeNode();
        //    //            node.Text = row["OrganName"].ToString();
        //    //            node.Value = row["OrganID"].ToString();
        //    //            node.Style.Add("Color", "#0066cc");
        //    //            if (ID == "2")
        //    //            {
        //    //                node.NavigateUrl = "UserManage.aspx?OrganID=" + node.Value + "&BZ=1";
        //    //            }
        //    //            else
        //    //            {
        //    //                node.NavigateUrl = "DepartmentManage.aspx?OrganID=" + node.Value + "&BZ=1";
        //    //            }
        //    //            node.Target = "right";
        //    //            if (DepartmentDAL.GetFirstDepartment(row["OrganID"].ToString()).Rows.Count > 0)
        //    //            {
        //    //                node.ExpandMode = expandMode;
        //    //            }
        //    //            e.Node.Nodes.Add(node);
        //    //        }
        //    //    }
        //    //}
        //    rtn.Expanded = true;
        //}

        ////获取子级部门
        //public static void GetAllChildNodes(RadTreeNode rtn, string DeptID)
        //{
        //    DataTable childDept = DepartmentDAL.GetAllChildDepartment(DeptID);
        //    foreach (DataRow dr in childDept.Rows)
        //    {
        //        RadTreeNode node = new RadTreeNode();
        //        node.Text = dr["DeptName"].ToString();
        //        node.Value = dr["DeptID"].ToString();
        //        if (ID == "2")
        //        {
        //            node.NavigateUrl = "UserManage.aspx?OrganID=" + dr["OrganID"].ToString() + "&DeptID=" + node.Value + "&BZ=1";
        //        }
        //        else
        //        {
        //            node.NavigateUrl = "DepartmentManage.aspx?OrganID=" + dr["OrganID"].ToString() + "&DeptID=" + node.Value + "&BZ=1";
        //        }
        //        node.Target = "right";
        //        rtn.Nodes.Add(node);
        //        node.Expanded = true;
        //        GetAllChildNodes(node, node.Value.ToString());
        //    }
        //}

        ////protected void RadTreeView1_NodeExpand(object sender, RadTreeNodeEventArgs e)
        ////{
        ////    e.Node.Nodes.Clear();
        ////    PopulateNodeOnDemand(e, TreeNodeExpandMode.ServerSide);
        ////}

        protected override bool IsNeedLogin
        {
            get
            {
                return false;
            }
        }

        public static string IDD = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            IDD = Request.QueryString["IDD"] == null ? "" : Request.QueryString["IDD"].ToString();
            if (!Page.IsPostBack)
            {
                RadTreeNode rtn = new RadTreeNode();
                rtn.Text = "全市";
                rtn.Value = "209";
                rtn.Style.Add("Color", "#0066cc");
                if (IDD == "2")  //用户页面
                {
                    rtn.NavigateUrl = "UserManage.aspx?OrganID=" + rtn.Value;
                }
                else
                {
                    rtn.NavigateUrl = "DepartmentManage.aspx?ID=" + rtn.Value;
                }
                rtn.Target = "right";
                rtn.Expanded = true;
                this.RadTreeView1.Nodes.Add(rtn);
                LoadRootNodes(rtn, TreeNodeExpandMode.ServerSide);
            }
        }

        //获取根节点(机构)
        private static void LoadRootNodes(RadTreeNode tn, TreeNodeExpandMode expandMode)
        {

            DataTable organ = OrganizationDAL.GetOrgan();

            foreach (DataRow row in organ.Rows)
            {
                RadTreeNode node = new RadTreeNode();
                node.Text = row["OrganName"].ToString();
                node.Value = row["OrganCoding"].ToString();
                node.Style.Add("Color", "#0066cc");
                if (IDD == "2")
                {
                    node.NavigateUrl = "UserManage.aspx?OrganID=" + row["OrganID"].ToString();
                }
                else
                {
                    node.NavigateUrl = "DepartmentManage.aspx?ID=" + row["OrganID"].ToString();
                }
                node.Target = "right";
                node.ExpandMode = expandMode;
                tn.Nodes.Add(node);
            }

        }

        //读取
        private static void PopulateNodeOnDemand(RadTreeNodeEventArgs e, TreeNodeExpandMode expandMode)
        {
            //if (e.Node.Level >= 2)
            //{
            //    DataTable firstdept = DepartmentDAL.GetAllFirstDepartment(Convert.ToInt64(e.Node.Value));

            //    foreach (DataRow row in firstdept.Rows)
            //    {
            //        RadTreeNode node = new RadTreeNode();
            //        node.Text = row["DeptName"].ToString();
            //        node.Value = row["DeptID"].ToString();
            //        if (IDD == "1")
            //        {
            //            node.NavigateUrl = "DepartmentManage.aspx?DeptID=" + node.Value + "&OID=" + e.Node.Value + "&ID=" + e.Node.Value;
            //        }
            //        else
            //        {
            //            node.NavigateUrl = "UserManage.aspx?DeptID=" + node.Value + "&OrganID=" + e.Node.Value;
            //        }
            //        node.Target = "right";
            //        e.Node.ExpandMode = expandMode;
            //        e.Node.Nodes.Add(node);
            //        GetAllChildNodes(node, Convert.ToInt64(node.Value.ToString()));
            //    }
            //}
            //else
            //{
                DataTable childorgan = OrganizationDAL.GetChildOrgan(e.Node.Value);
                //{
                    foreach (DataRow row in childorgan.Rows)
                    {
                        RadTreeNode node = new RadTreeNode();
                        node.Text = row["OrganName"].ToString();
                        node.Value = row["OrganID"].ToString();
                        node.Style.Add("Color", "#0066cc");
                        if (IDD == "2")
                        {
                            node.NavigateUrl = "UserManage.aspx?OrganID=" + node.Value;
                        }
                        else
                        {
                            node.NavigateUrl = "DepartmentManage.aspx?OrganID=" + node.Value + "&OID=" + node.Value + "&ID=" + node.Value;
                        }
                        node.Target = "right";
                        if (DepartmentDAL.GetDeptCountByOrganID(row["OrganID"].ToString()) > 0)
                        {
                            node.ExpandMode = expandMode;
                        }
                        e.Node.Nodes.Add(node);
                    }
                //}
            //}
            e.Node.Expanded = true;

        }

        //获取子级部门
        public static void GetAllChildNodes(RadTreeNode rtn, Int64 DeptID)
        {
            DataTable data = DepartmentDAL.GetAllChildDepartment(DeptID.ToString());
            foreach (DataRow row in data.Rows)
            {
                RadTreeNode node = new RadTreeNode();
                node.Text = row["DeptName"].ToString();
                node.Value = row["DeptID"].ToString();
                node.Value = row["DeptID"].ToString();
                if (IDD == "2")
                {
                    node.NavigateUrl = "UserManage.aspx?DeptID=" + node.Value;
                }
                else
                {
                    node.NavigateUrl = "DepartmentManage.aspx?DeptID=" + node.Value + "&OID=" + row["OrganID"].ToString() + "&ID=" + row["OrganID"].ToString();
                }
                node.Target = "right";
                rtn.Nodes.Add(node);
                GetAllChildNodes(node, Convert.ToInt64(node.Value.ToString()));
            }
        }

        protected void RadTreeView1_NodeExpand(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
        {
            e.Node.Nodes.Clear();
            PopulateNodeOnDemand(e, TreeNodeExpandMode.ServerSide);
        }
    }
}