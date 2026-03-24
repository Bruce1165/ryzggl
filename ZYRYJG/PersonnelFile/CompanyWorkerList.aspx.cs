using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class CompanyWorkerList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                
            if (!IsPostBack)
            {
                ButtonSearch_Click(sender, e);
            }
        }
         //查询
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ObjectDataSource1.SelectParameters.Clear();
            QueryParamOB q = new QueryParamOB();
            q.Add(string.Format("WorkerID in (select WorkerID from dbo.Certificate where UnitCode='{0}') ", ZZJGDM));
            if (rdtxtWorkerName.Text.Trim() != "")    //姓名
            {
                q.Add(string.Format("WorkerName like '%{0}%'", rdtxtWorkerName.Text.Trim()));
            }
            if (rdtxtZJHM.Text.Trim() != "")   //证件号码
            {
                q.Add(string.Format("CERTIFICATECODE like '%{0}%'", rdtxtZJHM.Text.Trim()));
            }
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGrid1.CurrentPageIndex = 0;
            RadGrid1.DataSourceID = ObjectDataSource1.ID;
        }
    }
}
