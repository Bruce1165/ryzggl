using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Model;
using DataAccess;
using System.Data;
using Utility;


namespace ZYRYJG.PersonnelFile
{
    public partial class CertifHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["o"].ToString();
            if (!this.IsPostBack)
            {
                ObjectDataSource1.SelectParameters.Clear();
                QueryParamOB q = new QueryParamOB();
                q.Add("CertificateID = "+id );
                ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
                RadGrid1.MasterTableView.SortExpressions.Clear();
                GridSortExpression sortStr1 = new GridSortExpression();
                sortStr1.FieldName = "CERTIFICATEHISTORYID desc";

                RadGrid1.MasterTableView.SortExpressions.AddSortExpression(sortStr1);

                RadGrid1.CurrentPageIndex = 0;
                RadGrid1.DataSourceID = ObjectDataSource1.ID;
            }
        }

        protected void RadGrid1_DataBound(object sender, EventArgs e)
        {
            int columnIndex = RadGrid1.MasterTableView.Columns.FindByUniqueName("ModifyTime").OrderIndex;
            for (int i =0;i< RadGrid1.MasterTableView.Items.Count ;i++)
            {
                if (i == RadGrid1.MasterTableView.Items.Count - 1)
                {
                    RadGrid1.MasterTableView.Items[i].Cells[columnIndex].Text =Convert.ToDateTime( RadGrid1.MasterTableView.DataKeyValues[i]["CreateTime"]).ToString("yyyy-MM-dd HH:mm");
                    if (Convert.ToDateTime(RadGrid1.MasterTableView.DataKeyValues[i]["CreateTime"]).ToString("yyyy-MM-dd") == "2011-02-20")
                    {
                        RadGrid1.MasterTableView.Items[i].Cells[RadGrid1.MasterTableView.Columns.FindByUniqueName("STATUS").OrderIndex].Text = "初始导入";
                    }
                }
                else
                {
                    RadGrid1.MasterTableView.Items[i].Cells[columnIndex].Text = RadGrid1.MasterTableView.Items[i + 1].Cells[columnIndex].Text;
                }
            }
        }

    }
}
