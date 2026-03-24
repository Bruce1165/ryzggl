using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAccess;

namespace ZYRYJG.SystemManage
{
    public partial class AddUserRole : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "SystemManage/AddUser.aspx";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                BindDate();
            }
        }
        public void BindDate()
        {
            //机构
            DataTable Organization = CommonDAL.GetDataTable("SELECT * FROM  Organization WHERE OrganName !='全市' ORDER BY OrderID ASC");
            RadComboBoxOrganID.DataSource = Organization;
            RadComboBoxOrganID.DataTextField = "OrganName";
            RadComboBoxOrganID.DataValueField = "OrganID";
            RadComboBoxOrganID.DataBind();
            //部门
            DataTable Department = CommonDAL.GetDataTable("SELECT * FROM Department WHERE OrganID='9998BB1A-9199-4F92-B135-C1203F30FBDE'");
            RadComboBoxDepartment.DataSource = Department;
            RadComboBoxDepartment.DataTextField = "DeptName";
            RadComboBoxDepartment.DataValueField = "DeptID";
            RadComboBoxDepartment.DataBind();
            //角色
            DataTable Role = CommonDAL.GetDataTable("SELECT * FROM  [Role]");
            RadComboBoxRole.DataSource = Role;
            RadComboBoxRole.DataTextField = "RoleName";
            RadComboBoxRole.DataValueField = "RoleID";
            RadComboBoxRole.DataBind();
        }

        protected void RadComboBoxOrganID_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
                DataTable Department = CommonDAL.GetDataTable("SELECT * FROM Department WHERE OrganID='" + RadComboBoxOrganID.SelectedValue + "'");
                RadComboBoxDepartment.DataSource = Department;
                RadComboBoxDepartment.DataTextField = "DeptName";
                RadComboBoxDepartment.DataValueField = "DeptID";
                RadComboBoxDepartment.DataBind();
          
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            //用户信息
            string Organizationid = RadComboBoxOrganID.SelectedValue;//机构名称
            string Departmentid = RadComboBoxDepartment.SelectedValue;//部门
            string Roleid = RadComboBoxRole.SelectedValue;//角色
            string id = Request.QueryString["uid"];
            DataTable dt = CommonDAL.GetDataTable("SELECT LOGIN_NAME,[PASSWORD],[USER_NAME],case when SEX = '1' then '男' else '女'end SEX,ID_CARD_CODE FROM [dbo].[ZHJY_userInfo] WHERE ID='" + id + "'");
            string uid=Guid.NewGuid().ToString();
            string loginname = dt.Rows[0]["LOGIN_NAME"] == DBNull.Value ? dt.Rows[0]["LOGIN_NAME"].ToString() : "";
            string password = dt.Rows[0]["PASSWORD"] == DBNull.Value ? dt.Rows[0]["PASSWORD"].ToString() : "";
            string username = dt.Rows[0]["User_Name"] == DBNull.Value ? dt.Rows[0]["User_Name"].ToString() : "";
            string sex = dt.Rows[0]["SEX"] == DBNull.Value ? dt.Rows[0]["SEX"].ToString() : "";
            string cordcode = dt.Rows[0]["ID_CARD_CODE"] == DBNull.Value ? dt.Rows[0]["ID_CARD_CODE"].ToString() : "";
            //用户
            string usersql = string.Format(@"INSERT INTO [dbo].[User]([UserID],[OrganID],[DeptID],[UserName],[UserPwd],[RelUserName],[Sex] ,[License])VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                                       uid, Organizationid, Departmentid, loginname, password, username, sex, cordcode);
            //角色
            string rolesql = string.Format(@"INSERT INTO [dbo].[UserRole] ([UserID],[RoleID]) VALUES('{0}','{1}')",uid,Roleid);
            //开启事务
            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            try
            {
                CommonDAL.ExecSQL(tran, usersql);
                CommonDAL.ExecSQL(tran, rolesql);
                Response.Write("<script>window.parent.location.href='AddUser.aspx';</script>");
                tran.Commit();
                //UIHelp.Alert(Page, "添加用户成功！");
                UIHelp.WriteOperateLog(UserName, UserID, "添加用户机构部门角色成功", string.Format("添加时间：{0}", DateTime.Now));
               
            }
            catch(Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "添加用户机构部门角色失败！", ex);
            }

        }
    }
}