using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace ZYRYJG.SystemManage
{
    public partial class UserManage : BasePage
    {
        //private new string OrganID = string.Empty; //机构ID
        //private new string DeptID = string.Empty; //部门ID
        //private string BZ = string.Empty; //标志性
        //private OrganizationMDL Organ = new OrganizationMDL(); //机构实例
        //private new UserMDL User = new UserMDL(); //用户实例

        //protected override string CheckVisiteRgihtUrl
        //{
        //    get
        //    {
        //        return "SystemManage/MainUser.aspx";
        //    }
        //}

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    OrganID = Request.QueryString["OrganID"] == null ? "" : Request.QueryString["OrganID"].ToString();
        //    DeptID = Request.QueryString["DeptID"] == null ? "" : Request.QueryString["DeptID"].ToString();
        //    BZ = Request.QueryString["BZ"] == null ? "" : Request.QueryString["BZ"].ToString();
        //    Organ = OrganizationDAL.GetOrganizationByOrganID(OrganID);

        //    if (BZ == "")
        //    {
        //        this.RadGrid1.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
        //    }
        //    if (Organ != null)
        //    {
        //        this.lblOrganName.Text = Organ.OrganName;
        //        this.lblOrganCoding.Text = Organ.OrganCoding;
        //        this.lblOrganDescription.Text = Organ.OrganDescription;
        //        this.lblOrganNature.Text = Organ.OrganNature;
        //        this.lblOrganAddress.Text = Organ.OrganAddress;
        //        this.lblOrganTelphone.Text = Organ.OrganTelphone;
        //        this.lblOrganCode.Text = Organ.OrganCode;
        //    }
        //}

        ////绑定数据
        //protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        //{
        //    DataTable alluser = new DataTable();
        //    if (DeptID != "")
        //    {
        //        alluser = UserDAL.GetAllUserByDeptID(DeptID);
        //    }
        //    else
        //    {
        //        alluser = UserDAL.GetAllUserByOrganID(OrganID);
        //    }
        //    this.RadGrid1.DataSource = alluser;
        //}

        ////添加
        //protected void RadGrid1_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        //{
        //    List<string> li = new List<string>();
        //    if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
        //    {
        //        GridEditFormItem editFormItem = e.Item as GridEditFormItem;
        //        RadTreeView rtv = editFormItem.FindControl("RadTreeView1") as RadTreeView;
        //        IList<RadTreeNode> nodeCollection = rtv.CheckedNodes;
        //        foreach (RadTreeNode node in nodeCollection)
        //        {
        //            li.Add(node.Value);
        //        }
        //    }
        //    GridItem edititem = (GridItem)e.Item;
        //    string UserName = (edititem.FindControl("txtUserName") as TextBox).Text;
        //    string UserPwd = (edititem.FindControl("txtUserPwd") as TextBox).Text;
        //    string RelUserName = (edititem.FindControl("txtRelUserName") as TextBox).Text;
        //    string License = (edititem.FindControl("txtLicense") as TextBox).Text;
        //    string Telphone = (edititem.FindControl("txtTelphone") as TextBox).Text;
        //    string Mobile = (edititem.FindControl("txtMobile") as TextBox).Text;
        //    string Code = (edititem.FindControl("txtCode") as TextBox).Text;

        //    User.UserName = UserName;
        //    User.UserPwd = UserPwd;
        //    User.RelUserName = RelUserName;
        //    User.License = License;
        //    User.Telphone = Telphone;
        //    User.Mobile = Mobile;
        //    User.Sex = "";
        //    User.Code = Code;
        //    User.Dept = new DepartmentMDL();
        //    User.Dept.DeptID = DeptID;
        //    User.Organ = new OrganizationMDL();
        //    User.Organ.OrganID = OrganID;

        //    DBHelper db = new DBHelper();
        //    DbTransaction trans = db.BeginTransaction();
        //    try
        //    {
        //        if (UserDAL.GetCountIsUser(UserName) == 0)
        //        {
        //            if (li.Count > 0)
        //            {
        //                UserDAL.AddUser(trans, User);
        //                RoleDAL.AddUserRole(trans, User.UserID, li);
        //                trans.Commit();
        //                UIHelp.Alert(Page, "添加成功！");
        //            }
        //            else
        //            {
        //                UIHelp.Alert(Page, "请选择角色！");
        //            }
        //        }
        //        else
        //        {
        //            UIHelp.Alert(Page, "该用户已存在！");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        trans.Rollback();
        //        UIHelp.WriteErrorLog(Page, "添加失败！", ex);
        //        return;
        //    }
        //}

        ////删除
        //protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        //{
        //    GridDataItem item = (GridDataItem)e.Item;
        //    string UserID = item.OwnerTableView.DataKeyValues[item.ItemIndex]["UserID"].ToString();

        //    DBHelper db = new DBHelper();
        //    DbTransaction trans = db.BeginTransaction();
        //    try
        //    {
        //        RoleDAL.DeleteUserRole(trans, UserID); //删除用户角色关系
        //        UserDAL.DeleteUserByUserID(trans, UserID);//删除用户
        //        trans.Commit();
        //        UIHelp.Alert(Page, "删除成功！");
        //    }
        //    catch (Exception ex)
        //    {
        //        trans.Rollback();
        //        UIHelp.WriteErrorLog(Page, "删除失败！", ex);
        //        return;
        //    }
        //}

        ////修改
        //protected void RadGrid1_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        //{
        //    GridItem edititem = (GridItem)e.Item;
        //    string UserID = edititem.OwnerTableView.DataKeyValues[edititem.ItemIndex]["UserID"].ToString();
        //    List<string> li = new List<string>();
        //    if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
        //    {
        //        GridEditFormItem editFormItem = e.Item as GridEditFormItem;
        //        RadTreeView rtv = editFormItem.FindControl("RadTreeView1") as RadTreeView;
        //        IList<RadTreeNode> nodeCollection = rtv.CheckedNodes;
        //        foreach (RadTreeNode node in nodeCollection)
        //        {
        //            li.Add(node.Value);
        //        }
        //    }
        //    GridItem item = (GridItem)e.Item;
        //    string UserName = (item.FindControl("txtUserName") as TextBox).Text;
        //    string OldUserPwd = (item.FindControl("txtUserPwd") as TextBox).Attributes["Value"];
          
        //    string newUserPwd = (item.FindControl("txtUserPwd") as TextBox).Text;


        //    string RelUserName = (item.FindControl("txtRelUserName") as TextBox).Text;
        //    string License = (item.FindControl("txtLicense") as TextBox).Text;
        //    string Telphone = (item.FindControl("txtTelphone") as TextBox).Text;
        //    string Mobile = (item.FindControl("txtMobile") as TextBox).Text;
        //    string Code = (item.FindControl("txtCode") as TextBox).Text;

        //    User.UserID = UserID;
        //    User.UserName = UserName;
        //    User.UserPwd = newUserPwd;
        //    User.RelUserName = RelUserName;
        //    User.License = License;
        //    User.Telphone = Telphone;
        //    User.Sex = "";
        //    User.Mobile = Mobile;
        //    User.Code = Code;
        //    User.Dept = new DepartmentMDL();
        //    User.Dept.DeptID = DeptID;
        //    User.Organ = new OrganizationMDL();
        //    User.Organ.OrganID = OrganID;

        //    DBHelper db = new DBHelper();
        //    DbTransaction trans = db.BeginTransaction();
        //    try
        //    {
        //        UserMDL U = UserDAL.GetUserByUserID(UserID);
        //        if (U.UserName != UserName)
        //        {
        //            if (UserDAL.GetCountIsUser(UserName) > 0)
        //            {
        //                UIHelp.Alert(Page, "该用户已存在！");
        //            }
        //            else
        //            {
        //                if (li.Count > 0)
        //                {
        //                    RoleDAL.AddUserRole(trans, UserID, li);

        //                    if (OldUserPwd != newUserPwd)//修改了密码
        //                    { UserDAL.ModifyUser(trans, User); }
        //                    else 
        //                    { UserDAL.ModifyUserNoUpdatePassword(trans, User); }//没有修改密码
                    
        //                    trans.Commit();
        //                    UIHelp.Alert(Page, "修改成功！");
        //                }
        //                else
        //                {
        //                    UIHelp.Alert(Page, "请选择角色！");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (li.Count > 0)
        //            {
        //                RoleDAL.AddUserRole(trans, UserID, li);
        //                UserDAL.ModifyUser(trans, User);
        //                trans.Commit();
        //                UIHelp.Alert(Page, "修改成功！");
        //            }
        //            else
        //            {
        //                UIHelp.Alert(Page, "请选择角色！");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        trans.Rollback();
        //        UIHelp.WriteErrorLog(Page, "修改失败！", ex);
        //        return;
        //    }
        //}

        ////列绑定事件
        //protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        //{
        //    string RoleName = string.Empty;
        //    if (e.Item is GridDataItem)
        //    {
        //        //序号列
        //        Label lbl = e.Item.FindControl("numberLabel") as Label;
        //        lbl.Text = (e.Item.ItemIndex + 1).ToString();

        //        //角色列
        //        string UserID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["UserID"].ToString();
        //        DataTable userrole = RoleDAL.GetAllUserRoleByUserID(UserID);
        //        foreach (DataRow dr in userrole.Rows)
        //        {
        //            RoleName += RoleDAL.GetRoleByRoleID(dr["RoleID"].ToString()).RoleName + ",";
        //        }
        //        if (RoleName.Length > 0)
        //        {
        //            RoleName = RoleName.Substring(0, RoleName.Length - 1);
        //        }
        //        else
        //        {
        //            RoleName = "";
        //        }
        //        Label role = e.Item.FindControl("Role") as Label;
        //        role.Text = RoleName;
        //        //机构部门列
        //        User = UserDAL.GetUserByUserID(UserID);
        //        Label DeptOrgan = e.Item.FindControl("DeptOrgan") as Label;
        //        if (User.Dept != null)
        //        {
        //            DeptOrgan.Text = User.Dept.DeptName;
        //        }
        //        else
        //        {
        //            DeptOrgan.Text = User.Organ.OrganName;
        //        }
        //    }
        //    //角色树
        //    if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
        //    {
        //        GridEditFormItem editFormItem = e.Item as GridEditFormItem;
        //        string UserID = (editFormItem.FindControl("txtUserID") as TextBox).Text.ToString();
        //        RadTreeView rtv = editFormItem.FindControl("RadTreeView1") as RadTreeView;
        //        DataTable drole = RoleDAL.GetAllRole();
        //        DataTable userrole = RoleDAL.GetAllUserRoleByUserID(UserID);
        //        foreach (DataRow dr in drole.Rows)
        //        {
        //            RadTreeNode rtn = new RadTreeNode();
        //            rtn.Text = dr["RoleName"].ToString();
        //            rtn.Value = dr["RoleID"].ToString();
        //            foreach (DataRow row in userrole.Rows)
        //            {
        //                if (row["RoleID"].ToString() == rtn.Value)
        //                {
        //                    rtn.Checked = true;
        //                }
        //            }
        //            rtv.Nodes.Add(rtn);
        //        }
        //    }
        //}

        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "MainUser.aspx";
            }
        }
        UserOB Users = new UserOB();//用户对象
        protected Int64 _DeptID;  //部门ID
        protected Int64 _OrganID; //机构ID
        OrganizationOB ogn = new OrganizationOB(); //机构对象
        protected void Page_Load(object sender, EventArgs e)
        {
            _DeptID = Request.QueryString["DeptID"] == null ? 0 : Convert.ToInt64(Request.QueryString["DeptID"].ToString());
            _OrganID = Request.QueryString["OrganID"] == null ? 0 : Convert.ToInt64(Request.QueryString["OrganID"].ToString());
            ogn = OrganizationDAL.GetObject(_OrganID);
            if (ogn != null)
            {
                //加载机构信息
                this.lblOrganName.Text = ogn.OrganName;
                this.lblOrganCoding.Text = ogn.OrganCoding;
                this.lblOrganDescription.Text = ogn.OrganDescription;
                this.lblOrganNature.Text = ogn.OrganNature;
                this.lblOrganAddress.Text = ogn.OrganAddress;
                this.lblOrganTelphone.Text = ogn.OrganTelphone;
                this.lblOrganCode.Text = ogn.OrganCode;

                Div_UserResourceList.InnerHtml = "<a target=\"_blank\" style=\"text-decoration:underline; font-size:12px; color:Blue;\" href=\"UserResourceList.aspx?o=" + _OrganID.ToString() + "\">>> 机构用户权限一览表</a>";
            }
            if (_OrganID == 0 && _DeptID == 0)
            {
                this.RadGrid1.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
            }
        }


        #region  绑定数据
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable alluser = new DataTable();
            if (_DeptID != 0)
            {
                alluser = UserDAL.GetAllUserByDeptID(_DeptID);
            }
            else
            {
                alluser = UserDAL.GetAllUserByOrganID(_OrganID);
            }
            this.RadGrid1.DataSource = alluser;
        }
        #endregion

        #region 动态加序号,角色
        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            string RoleName = string.Empty;
            if (e.Item is GridDataItem)
            {
                //序号列
                Label lbl = e.Item.FindControl("numberLabel") as Label;
                lbl.Text = (e.Item.ItemIndex + 1).ToString();

                //角色列
                long _UserID = Convert.ToInt64(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["UserID"].ToString());
                DataTable userrole = RoleDAL.GetAllUserRoleByUserID(_UserID.ToString());
                foreach (DataRow dr in userrole.Rows)
                {
                    RoleName += dr["RoleName"] + ",";
                }
                if (RoleName.Length > 0)
                {
                    RoleName = RoleName.Substring(0, RoleName.Length - 1);
                }
                else
                {
                    RoleName = "";
                }
                Label role = e.Item.FindControl("Role") as Label;
                role.Text = RoleName;
                //机构部门列
                Users = UserDAL.GetObject(_UserID);
                Label DeptOrgan = e.Item.FindControl("DeptOrgan") as Label;
                if (Users.DeptID != 0)
                {
                    DeptOrgan.Text = DepartmentDAL.GetObject(Convert.ToInt64(Users.DeptID)).DeptName;
                }
                else
                {
                    DeptOrgan.Text = OrganizationDAL.GetObject(Convert.ToInt64(Users.OrganID)).OrganName;
                }
            }
            //角色树
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                DataTable userrole = new DataTable();
                GridEditFormItem editFormItem = e.Item as GridEditFormItem;
                if ((editFormItem.FindControl("txtUserID") as TextBox).Text.ToString() != "")
                {
                    Int64 _UserID = Convert.ToInt64((editFormItem.FindControl("txtUserID") as TextBox).Text.ToString());
                    userrole = RoleDAL.GetAllUserRoleByUserID(_UserID.ToString());
                }
                RadTreeView rtv = editFormItem.FindControl("RadTreeView1") as RadTreeView;
                DataTable drole = RoleDAL.GetAllRole();
                foreach (DataRow dr in drole.Rows)
                {
                    RadTreeNode rtn = new RadTreeNode();
                    rtn.Text = dr["RoleName"].ToString();
                    rtn.Value = dr["RoleID"].ToString();
                    foreach (DataRow row in userrole.Rows)
                    {
                        if (row["RoleID"].ToString() == rtn.Value)
                        {
                            rtn.Checked = true;
                        }
                    }
                    rtv.Nodes.Add(rtn);
                }
            }

        }
        #endregion

        #region 添加
        protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
        {
            List<string> li = new List<string>();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                GridEditFormItem editFormItem = e.Item as GridEditFormItem;
                RadTreeView rtv = editFormItem.FindControl("RadTreeView1") as RadTreeView;
                IList<RadTreeNode> nodeCollection = rtv.CheckedNodes;
                foreach (RadTreeNode node in nodeCollection)
                {
                    li.Add(node.Value);
                    sb.Append("、").Append(node.Text);
                }
                if (sb.Length > 0) sb.Remove(0, 1);
            }
            GridItem edititem = (GridItem)e.Item;
            string UserName = (edititem.FindControl("txtUserName") as TextBox).Text;
            string UserPwd = (edititem.FindControl("txtUserPwd") as TextBox).Text;
            string RelUserName = (edititem.FindControl("txtRelUserName") as TextBox).Text;
            string License = (edititem.FindControl("txtLicense") as TextBox).Text;
            string Telphone = (edititem.FindControl("txtTelphone") as TextBox).Text;
            string Mobile = (edititem.FindControl("txtMobile") as TextBox).Text;
            string Code = (edititem.FindControl("txtCode") as TextBox).Text;

            Users.UserName = UserName;
            Users.UserPwd = UserPwd;
            Users.RelUserName = RelUserName;
            Users.License = License;
            Users.Telphone = Telphone;
            Users.Mobile = Mobile;
            Users.Code = Code;
            Users.DeptID = _DeptID;
            Users.OrganID = _OrganID;


            DBHelper db = new DBHelper();
            DbTransaction trans = db.BeginTransaction();
            try
            {
                if (UserDAL.GetCountIsUser(UserName) == 0)
                {
                    if (li.Count > 0)
                    {
                        UserDAL.Insert(trans, Users);
                        RoleDAL.AddUserRole(trans, Users.UserID.ToString(), li);
                        trans.Commit();

                        UIHelp.WriteOperateLog(PersonName, UserID, "添加用户", string.Format("用户名称：{0}，所在机构：{1}，角色：{2}。"
                           , Users.RelUserName, lblOrganName.Text, sb.ToString()));

                        UIHelp.layerAlert(Page, "添加成功！", 6, 3000);
                    }
                    else
                    {
                        UIHelp.layerAlert(Page, "请选择角色！");
                        e.Canceled = true;
                        return;
                    }
                }
                else
                {
                    UIHelp.layerAlert(Page, "该用户已存在！");
                    e.Canceled = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                trans.Rollback();
                UIHelp.WriteErrorLog(Page, "添加失败！", ex);
                return;
            }
        }
        #endregion

        #region 修改
        protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
        {
            List<string> li = new List<string>();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                GridEditFormItem editFormItem = e.Item as GridEditFormItem;
                RadTreeView rtv = editFormItem.FindControl("RadTreeView1") as RadTreeView;
                IList<RadTreeNode> nodeCollection = rtv.CheckedNodes;
                foreach (RadTreeNode node in nodeCollection)
                {
                    li.Add(node.Value);
                    sb.Append("、").Append(node.Text);
                }
                if (sb.Length > 0) sb.Remove(0, 1);
            }

            GridItem item = (GridItem)e.Item;
            Int64 _UserID = Convert.ToInt64(item.OwnerTableView.DataKeyValues[item.ItemIndex]["UserID"].ToString());
            string UserName = (item.FindControl("txtUserName") as TextBox).Text;
            string UserPwd = (item.FindControl("txtUserPwd") as TextBox).Text;
            string RelUserName = (item.FindControl("txtRelUserName") as TextBox).Text;
            string License = (item.FindControl("txtLicense") as TextBox).Text;
            string Telphone = (item.FindControl("txtTelphone") as TextBox).Text;
            string Mobile = (item.FindControl("txtMobile") as TextBox).Text;
            string Code = (item.FindControl("txtCode") as TextBox).Text;

            Users.UserID = _UserID;
            Users.UserName = UserName;
            Users.UserPwd = UserPwd;
            Users.RelUserName = RelUserName;
            Users.License = License;
            Users.Telphone = Telphone;
            Users.Mobile = Mobile;
            Users.Code = Code;
            Users.DeptID = _DeptID;
            Users.OrganID = _OrganID;

            DBHelper db = new DBHelper();
            DbTransaction trans = db.BeginTransaction();
            try
            {
                UserOB U = UserDAL.GetObject(_UserID);
                if (U.UserName != UserName)
                {
                    if (UserDAL.GetCountIsUser(UserName) > 0)
                    {
                        UIHelp.layerAlert(Page, "该用户已存在！");
                        e.Canceled = true;
                        return;
                    }
                    else
                    {
                        if (li.Count > 0)
                        {
                            RoleDAL.AddUserRole(trans, _UserID.ToString(), li);
                            UserDAL.Update(trans, Users);
                            trans.Commit();
                            UIHelp.layerAlert(Page, "修改成功！", 6, 3000);
                        }
                        else
                        {
                            UIHelp.layerAlert(Page, "请选择角色！");
                            e.Canceled = true;
                            return;
                        }
                    }
                }
                else
                {
                    if (li.Count > 0)
                    {
                        RoleDAL.AddUserRole(trans, _UserID.ToString(), li);
                        UserDAL.Update(trans, Users);
                        trans.Commit();

                        UIHelp.WriteOperateLog(PersonName, UserID, "修改用户", string.Format("用户名称：{0}，所在机构：{1}，角色：{2}。"
                           , Users.RelUserName, lblOrganName.Text, sb.ToString()));

                        UIHelp.layerAlert(Page, "修改成功！", 6, 3000);
                    }
                    else
                    {
                        UIHelp.layerAlert(Page, "请选择角色！");
                        e.Canceled = true;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                trans.Rollback();
                UIHelp.WriteErrorLog(Page, "修改失败！", ex);
                return;
            }

        }
        #endregion

        #region 删除
        protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Int64 _UserID = Convert.ToInt64(item.OwnerTableView.DataKeyValues[item.ItemIndex]["UserID"].ToString());
            DBHelper db = new DBHelper();
            DbTransaction trans = db.BeginTransaction();
            try
            {
                RoleDAL.DeleteUserRole(trans, _UserID.ToString());
                UserDAL.Delete(trans, _UserID);
                UIHelp.layerAlert(Page, "删除成功！", 6, 3000);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                UIHelp.WriteErrorLog(Page, "删除失败！", ex);
                return;
            }

            UIHelp.WriteOperateLog(PersonName, UserID, "删除用户", string.Format("用户名称：{0}，所在机构：{1}。"
                 , e.Item.Cells[RadGrid1.MasterTableView.Columns.FindByUniqueName("RelUserName").OrderIndex].Text
                , lblOrganName.Text));

        }
        #endregion
    }
}