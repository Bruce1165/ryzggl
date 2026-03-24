using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Telerik.Web.UI;
using DataAccess;
using System.Data;

namespace ZYRYJG.CheckMgr
{
    public partial class QuestionFeedBack : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObjectDataSourceCheckFeedBack.SelectParameters.Clear();
                QueryParamOB q = new QueryParamOB();

                q.Add(string.Format("WorkerCertificateCode like '{0}' and DataStatusCode >0", WorkerCertificateCode));//个人，并已发布
                RadGridCheckFeedBack.MasterTableView.SortExpressions.Clear();

                GridSortExpression sortStr1 = new GridSortExpression();
                sortStr1.FieldName = "PublishiTime";
                sortStr1.SortOrder = GridSortOrder.Descending;
                RadGridCheckFeedBack.MasterTableView.SortExpressions.AddSortExpression(sortStr1);

                ObjectDataSourceCheckFeedBack.SelectParameters.Add("filterWhereString", q.ToWhereString());
                RadGridCheckFeedBack.CurrentPageIndex = 0;
                RadGridCheckFeedBack.DataSourceID = ObjectDataSourceCheckFeedBack.ID;
            }
        }


    }
}