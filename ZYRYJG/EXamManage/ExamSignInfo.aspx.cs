using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;

namespace ZYRYJG.EXamManage
{
    public partial class ExamSignInfo : BasePage
    {
        protected override bool IsNeedLogin
        {
            get
            {
                return false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RefreshGrid();
            }
        }

        //刷新grid
        protected void RefreshGrid()
        {
            ObjectDataSource1.SelectParameters.Clear();
            QueryParamOB queryOB = new QueryParamOB();
            queryOB.Add(string.Format("SignUpCode='{0}'", Request.QueryString["SignUpCode"].ToString()));
            queryOB.Add(string.Format(" CreateTime = '{0}'", Convert.ToDateTime(Request.QueryString["CreateTime"]).ToString("yyyy-MM-dd HH:mm:ss.fff")));
            ObjectDataSource1.SelectParameters.Add("filterWhereString", queryOB.ToWhereString());
            RadGridExamSignUp.CurrentPageIndex = 0;
            RadGridExamSignUp.DataSourceID = "ObjectDataSource1";

        }
    }
}