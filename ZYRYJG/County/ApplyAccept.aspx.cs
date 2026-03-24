using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Model;
using DataAccess;

namespace ZYRYJG.County
{
    public partial class ApplyAccept : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if(!IsPostBack)
            //{
            //    ButtonSearch_Click(sender, e);
            //}
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            //QueryParamOB q = new QueryParamOB();
            //if (RadComboBoxENT_City.SelectedValue != "")//区县
            //{
            //    q.Add(string.Format("ENT_City like'%{0}%'", RadComboBoxENT_City.SelectedValue));
            //}

            //if (RadComboBoxApplyType.SelectedValue != "")//申报事项
            //{
            //    q.Add(string.Format("ApplyType like'%{0}%'", RadComboBoxApplyType.SelectedValue));
            //}

            //q.Add("ReportStatus ='已上报' and AcceptStatus = '未收件'");

            //ObjectDataSource1.SelectParameters.Clear();
            //ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            //RadGridHZSB.CurrentPageIndex = 0;
            //RadGridHZSB.DataSourceID = ObjectDataSource1.ID;
          
        }
    }
}