using DataAccess;
using Model;
using System;
using System.Data;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace ZYRYJG.SystemManage
{
    public partial class DepartmentManage : BasePage
    {
        //protected override bool IsNeedLogin
        //{
        //    get
        //    {
        //        return false;
        //    }
        //}

        //private new string OrganID = string.Empty; //机构ID
        //private new string DeptID = string.Empty; //部门ID
        //private string BZ = string.Empty; //标志性
        //private OrganizationMDL Organ = new OrganizationMDL(); //机构实例
        //private DepartmentMDL Dept = new DepartmentMDL();  //部门实例

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
        //    DataTable alldept = new DataTable();
        //    if (DeptID == "")
        //    {
        //        alldept = DepartmentDAL.GetFirstDepartment(OrganID);
        //    }
        //    else
        //    {
        //        alldept = DepartmentDAL.GetAllChildDepartment(DeptID);
        //    }
        //    this.RadGrid1.DataSource = alldept;
        //}

        ////添加
        //protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
        //{
        //    GridItem edititem = (GridItem)e.Item;
        //    string DeptName = (edititem.FindControl("txtDeptName") as TextBox).Text;
        //    if (DeptID == "")
        //    {
        //        Dept.PDeptID = "0";
        //    }
        //    else
        //    {
        //        Dept.PDeptID = DeptID;
        //    }
        //    Dept.DeptID = Guid.NewGuid().ToString();
        //    Dept.Organ = new OrganizationMDL();
        //    Dept.Organ.OrganID = OrganID;
        //    Dept.DeptName = DeptName;
        //    try
        //    {
        //        if (DepartmentDAL.GetCountIsDept(DeptName, OrganID, DeptID) == 0)
        //        {
        //            DepartmentDAL.AddDepartment(Dept);
        //            UIHelp.Alert(Page, "添加成功！");
        //            UIHelp.ScriptAlert(this.Page, "form1", "window.parent['left'].location.reload()", true);
        //        }
        //        else
        //        {
        //            UIHelp.Alert(Page, "该部门已存在！");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        UIHelp.WriteErrorLog(Page, "添加失败！", ex);
        //        return;
        //    }
        //}

        ////删除
        //protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
        //{
        //    GridItem item = (GridItem)e.Item;
        //    string DeptID = item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["DeptID"].ToString();

        //    try
        //    {
        //        if (DepartmentDAL.GetAllChildDepartment(DeptID).Rows.Count > 0)
        //        {
        //            UIHelp.Alert(Page, "请先删除子级部门！");
        //        }
        //        else
        //        {
        //            if (UserDAL.GetAllUserByDeptID(DeptID).Rows.Count > 0)
        //            {
        //                UIHelp.Alert(Page, "请先删除该部门下人员！");
        //            }
        //            else
        //            {
        //                DepartmentDAL.DeleteDepartmentByDeptID(DeptID);
        //                UIHelp.Alert(Page, "删除成功！");
        //                UIHelp.ScriptAlert(this.Page, "form1", "window.parent['left'].location.reload()", true);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        UIHelp.WriteErrorLog(Page, "删除失败！", ex);
        //        return;
        //    }
        //}

        ////修改
        //protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
        //{
        //    GridItem edititem = (GridItem)e.Item;
        //    string DeptID = edititem.OwnerTableView.DataKeyValues[edititem.ItemIndex]["DeptID"].ToString();
        //    string DeptName = (edititem.FindControl("txtDeptName") as TextBox).Text;
        //    string pDeptID = (edititem.FindControl("txtpDeptID") as TextBox).Text;
        //    string OrganID = (edititem.FindControl("txtOrganID") as TextBox).Text;

        //    Dept.DeptID = DeptID;
        //    Dept.DeptName = DeptName;
        //    Dept.PDeptID = pDeptID;
        //    Dept.Organ = new OrganizationMDL();
        //    Dept.Organ.OrganID = OrganID;

        //    try
        //    {
        //        if (DepartmentDAL.GetCountIsDept(DeptName, OrganID, DeptID) == 0)
        //        {
        //            DepartmentDAL.ModifyDepartment(Dept);
        //            UIHelp.Alert(Page, "修改成功！");
        //            UIHelp.ScriptAlert(this.Page, "form1", "window.parent['left'].location.reload()", true);
        //        }
        //        else
        //        {
        //            UIHelp.Alert(Page, "该部门已存在！");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        UIHelp.WriteErrorLog(Page, "修改失败！", ex);
        //        return;
        //    }
        //}

        //protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        //{
        //    if (e.Item is GridDataItem)
        //    {
        //        Label lbl = e.Item.FindControl("numberLabel") as Label;
        //        lbl.Text = (this.RadGrid1.PageSize * this.RadGrid1.CurrentPageIndex + e.Item.ItemIndex + 1).ToString();
        //    }
        //}

        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "MainDepartment.aspx";
            }
        }
        Int64 _DeptID;  //部门ID
        Int64 _OrganID;  //机构ID
        Int64 OID;
        Int64 _ID;
        DepartmentOB dept = new DepartmentOB();
        OrganizationOB ogn = new OrganizationOB();
        protected void Page_Load(object sender, EventArgs e)
        {
            _DeptID = Request.QueryString["DeptID"] == null ? 0 : Convert.ToInt64(Request.QueryString["DeptID"].ToString());
            _OrganID = Request.QueryString["OrganID"] == null ? 0 : Convert.ToInt64(Request.QueryString["OrganID"].ToString());
            OID = Request.QueryString["OID"] == null ? 0 : Convert.ToInt64(Request.QueryString["OID"].ToString());
            _ID = Request.QueryString["ID"] == null ? 0 : Convert.ToInt64(Request.QueryString["ID"].ToString());
            if (OID == 0)
            {
                this.RadGrid1.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
            }
            ogn = OrganizationDAL.GetObject(_ID);
            if (ogn != null)
            {
                this.lblOrganName.Text = ogn.OrganName;
                this.lblOrganCoding.Text = ogn.OrganCoding;
                this.lblOrganDescription.Text = ogn.OrganDescription;
                this.lblOrganNature.Text = ogn.OrganNature;
                this.lblOrganAddress.Text = ogn.OrganAddress;
                this.lblOrganTelphone.Text = ogn.OrganTelphone;
                this.lblOrganCode.Text = ogn.OrganCode;
            }
        }

        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable alldept = new DataTable();
            if (_OrganID == 0)
            {
                alldept = DepartmentDAL.GetAllFirstDepartment(_OrganID.ToString());
            }
            else
            {
                alldept = DepartmentDAL.GetAllChildDepartment(_DeptID.ToString());
            }
            this.RadGrid1.DataSource = alldept;
        }

        #region 修改
        protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridItem edititem = (GridItem)e.Item;
            Int64 _DeptID = Convert.ToInt64(edititem.OwnerTableView.DataKeyValues[edititem.ItemIndex]["DeptID"]);
            string DeptName = (edititem.FindControl("txtDeptName") as TextBox).Text;
            Int64 pDeptID = Convert.ToInt64((edititem.FindControl("txtpDeptID") as TextBox).Text);
            Int64 _OrganID = Convert.ToInt64((edititem.FindControl("txtOrganID") as TextBox).Text);

            dept.DeptID = _DeptID;
            dept.DeptName = DeptName;
            dept.pDeptID = pDeptID;
            dept.OrganID = _OrganID;
            try
            {
                if (DepartmentDAL.GetCountIsDept(DeptName, _OrganID, _DeptID) == 0)
                {
                    DepartmentDAL.Update(dept);

                    UIHelp.WriteOperateLog(PersonName, UserID, "修改部门", string.Format("部门名称：{0}。", DeptName));

                    UIHelp.layerAlert(Page, "修改成功！", 6, 3000);
                }
                else
                {
                    UIHelp.layerAlert(Page, "该部门已存在！");
                    e.Canceled = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "修改失败！", ex);
                return;
            }
        }
        #endregion

        #region  添加
        protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
        {
            GridItem edititem = (GridItem)e.Item;
            string DeptName = (edititem.FindControl("txtDeptName") as TextBox).Text;



            if (_OrganID != 0)
            {
                dept.pDeptID = 0;
            }
            else
            {
                dept.pDeptID = _DeptID;
            }
            if (_OrganID != 0)
            {
                dept.OrganID = _OrganID;
            }
            else
            {
                dept.OrganID = OID;
            }
            dept.DeptName = DeptName;
            try
            {
                if (DepartmentDAL.GetCountIsDept(DeptName, _OrganID, _DeptID) == 0)
                {
                    DepartmentDAL.Insert(dept);

                    UIHelp.WriteOperateLog(PersonName, UserID, "添加部门", string.Format("部门名称：{0}。", DeptName));

                    UIHelp.layerAlert(Page, "添加成功！", 6, 3000);
                    //RadScriptManager.RegisterStartupScript(this.Page, this.GetType(), "js", "window.parent.frames.left.location.reload();", true);
                }
                else
                {
                    UIHelp.layerAlert(Page, "该部门已存在！");
                    e.Canceled = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "添加失败！", ex);
                return;
            }

        }
        #endregion

        #region  绑定序号列
        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                Label lbl = e.Item.FindControl("numberLabel") as Label;
                lbl.Text = (this.RadGrid1.PageSize * this.RadGrid1.CurrentPageIndex + e.Item.ItemIndex + 1).ToString();
            }
        }
        #endregion

        #region  删除
        protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridItem item = (GridItem)e.Item;
            Int64 _DeptID = Convert.ToInt64(item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["DeptID"].ToString());
            try
            {
                if (DepartmentDAL.GetAllChildDepartment(_DeptID.ToString()).Rows.Count > 0)
                {
                    UIHelp.layerAlert(Page, "请先删除子级部门！");
                    return;
                }
                else
                {
                    if (UserDAL.GetAllUserByDeptID(_DeptID).Rows.Count > 0)
                    {
                        UIHelp.layerAlert(Page, "请先删除该部门下人员！");
                        return;
                    }
                    else
                    {
                        DepartmentDAL.Delete(_DeptID);

                        UIHelp.WriteOperateLog(PersonName, UserID, "删除部门", string.Format("部门名称：{0}。"
                    , e.Item.Cells[RadGrid1.MasterTableView.Columns.FindByUniqueName("DeptName").OrderIndex].Text));

                        UIHelp.layerAlert(Page, "删除成功！", 6, 3000);
                    }
                }
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "删除失败！", ex);
                return;
            }
        }
        #endregion
    }
}