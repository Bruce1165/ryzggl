using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using Telerik.Web.UI;
using System.Data;
using System.Collections.Specialized;

namespace ZYRYJG.StAnManage
{
    public partial class CompareJZS : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = CertificateDAL.AnalysisCompareJZS();
                RadGrid2.DataSource = dt;
                RadGrid2.DataBind();
                ButtonSearch_Click(sender, e);
            }
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ObjectDataSource1.SelectParameters.Clear();
            QueryParamOB q = new QueryParamOB();
            if (RadTextBoxCertificateCode.Text.Trim() != "")
            {
                q.Add(string.Format("CERTIFICATECODE like '{0}%'", RadTextBoxCertificateCode.Text.Trim()));
            }
            if (RadTextBoxWorkerName.Text.Trim() != "")
            {
                q.Add(string.Format("WorkerName like '{0}%'", RadTextBoxWorkerName.Text.Trim()));
            }
            
            if (RadTextBoxWorkerCertificateCode.Text.Trim() != "")
            {
                q.Add(string.Format("WorkerCertificateCode like '{0}%'", RadTextBoxWorkerCertificateCode.Text.Trim()));
            }
            if (RadTextBoxUnitName.Text.Trim() != "")
            {
                q.Add(string.Format("UnitName like '{0}%'", RadTextBoxUnitName.Text.Trim()));
            }
            if (RadTextBoxUnitCode.Text.Trim() != "")
            {
                q.Add(string.Format("UnitCode like '{0}%'", RadTextBoxUnitCode.Text.Trim()));
            }
            if (RadDatePicker_ValidEndDate.SelectedDate.HasValue)//有效期至
            {
                q.Add(string.Format("ValidEndDate ='{0}'", RadDatePicker_ValidEndDate.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }
            
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGrid1.CurrentPageIndex = 0;
            RadGrid1.DataSourceID = ObjectDataSource1.ID;
        }
    }
}
