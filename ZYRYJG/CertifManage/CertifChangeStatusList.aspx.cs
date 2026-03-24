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

namespace ZYRYJG.CertifManage
{
    public partial class CertifChangeStatusList1 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PostSelect1.PostTypeID = string.IsNullOrEmpty(Request["o"]) ? "1" : Request["o"].ToString();
                PostSelect1.LockPostTypeID();
                btnSearch_Click(sender, e);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ObjectDataSource1.SelectParameters.Clear();

            QueryParamOB q = new QueryParamOB();
            if (rdtxtWorkerName.Text.Trim() != "")    //姓名
            {
                q.Add(string.Format("WorkerName like '%{0}%'", rdtxtWorkerName.Text.Trim()));
            }
            if (rdtxtCertificateCode.Text.Trim() != "")  //证书编码
            {
                q.Add(string.Format("CertificateCode like '%{0}%'", rdtxtCertificateCode.Text.Trim()));
            }
            if (rdtxtZJHM.Text.Trim() != "")   //证件号码
            {
                q.Add(string.Format("WorkerCertificateCode like '%{0}%'", rdtxtZJHM.Text.Trim()));
            }
            if (rdtxtQYMC.Text.Trim() != "")   //企业名称
            {
                q.Add(string.Format("UnitName like '%{0}%'", rdtxtQYMC.Text.Trim()));
            }
            //证书有效期结束时间
            if (!txtValidStartDate.IsEmpty)
            {
                q.Add(string.Format("ValidEndDate>='{0}'", txtValidStartDate.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }
            if (!txtValidEndtDate.IsEmpty)
            {
                q.Add(string.Format("ValidEndDate<'{0}'", txtValidEndtDate.SelectedDate.Value.AddDays(1).ToString("yyyy-MM-dd")));
            }
          
            if (PostSelect1.PostTypeID != "") q.Add(string.Format("PostTypeID = {0}", PostSelect1.PostTypeID));      //岗位类别
            if (PostSelect1.PostID != "") q.Add(string.Format("PostID = {0}", PostSelect1.PostID));               //岗位工种
           
            switch (rbl.SelectedValue)
            {
                case "1":
                    q.Add(string.Format("Status ='{0}'", EnumManager.CertificateContinueStatus.Applyed));
                    break;
                case "2":
                    q.Add(string.Format("Status ='{0}'", EnumManager.CertificateContinueStatus.Accepted));
                    break;
                case "3":
                    q.Add(string.Format("Status ='{0}'", EnumManager.CertificateContinueStatus.Checked));
                    break;
                case "4":
                    q.Add(string.Format("Status ='{0}'", EnumManager.CertificateContinueStatus.Decided));
                    break;
            }
            int posttypeid = Request.QueryString["o"] == null ? 1 : Convert.ToInt32(Request.QueryString["o"].ToString());
            q.Add(string.Format("PostTypeID={0}", posttypeid));
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGrid1.CurrentPageIndex = 0;
            RadGrid1.DataSourceID = ObjectDataSource1.ID;
        }

      

        protected void RadGrid1_DataBound(object sender, EventArgs e)
        {
            //格式化引用列
            RadGrid rgd = sender as RadGrid;
            RefColumnOB ob = new RefColumnOB();
            ob.AddTableRef("PostTypeID", "DBO.PostInfo", "PostName", "PostID", true);
            ob.AddTableRef("PostID", "DBO.PostInfo", "PostName", "PostID", true);
            UIHelp.SetRefColumn(rgd, ob);
        }

    }
}
