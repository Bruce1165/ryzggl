using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAccess;
using Model;
using Telerik.Web.UI;

namespace ZYRYJG.SystemManage
{
    public partial class UserResourceList : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "MainUser.aspx";
            }
        }

        Int64 OrganID; //机构ID

        protected void Page_Load(object sender, EventArgs e)
        {
            OrganID = Request["o"] == null ? 0 : Convert.ToInt64(Request["o"]);
            if (!IsPostBack)
            {
                BindUserResourceList(OrganID.ToString());
            }
        }

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
        public void BindUserResourceList(string OrgID)
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

            //绑定人员权限
            DataTable userTable = ResourceDAL.GetUserResourceOfOrgan(OrgID);
            string userid = "";
            DataRow find = null;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (DataRow r in userTable.Rows)
            {
                if (userid != r["UserID"].ToString())
                {
                    dic.Add(r["UserID"].ToString(), r["RelUserName"].ToString());
                    DataColumn dc3 = new DataColumn(r["UserID"].ToString(), typeof(string));
                    dt.Columns.Add(dc3);
                    userid = r["UserID"].ToString();
                }
                find = dt.Rows.Find(r["MenuID"]);
                if (find != null) find[r["UserID"].ToString()] = "√";
            }
            RadGrid1.DataSource = dt;

            RadGrid1.MasterTableView.Columns.Clear();

            GridBoundColumn gc1 = new GridBoundColumn();
            gc1.HeaderText = "功能名称";
            gc1.UniqueName = "MenuName";
            gc1.DataField = "MenuName";
            RadGrid1.MasterTableView.Columns.Add(gc1);

            foreach (string k in dic.Keys)
            {
                GridBoundColumn gc = new GridBoundColumn();
                gc.HeaderText = dic[k];
                gc.UniqueName = k;
                gc.DataField = k;

                RadGrid1.MasterTableView.Columns.Add(gc);
            }
            RadGrid1.DataBind();
        }

        //导出excel
        protected void ButtonExportToExcel_Click(object sender, EventArgs e)
        {
            BindUserResourceList(OrganID.ToString());
            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.OpenInNewWindow = true;
            RadGrid1.MasterTableView.ExportToExcel();
        }

        //格式化Excel
        protected void RadGrid1_ExcelExportCellFormatting(object source, ExcelExportCellFormattingEventArgs e)
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
    }
}
