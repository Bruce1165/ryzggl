using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using System.Data;
using System.IO;
using System.Drawing;

public partial class Student_PrintCertDetail : BasePage
{
    protected override string CheckVisiteRgihtUrl
    {
        get
        {
            return "jxjy/MyTrain.aspx";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        CachejxjyClickCount += 1;
        if (!IsPostBack)
        {
            try
            {
                ObjectDataSource1.SelectParameters.Clear();
                QueryParamOB q = new QueryParamOB();

                //if (string.IsNullOrEmpty(Request["o"])==false)
                //{
                //    q.Add(string.Format("[PackageID]={0}", Convert.ToInt64(Request["o"])));
                //}

                //已加入我的学习计划的
                q.Add(string.Format("[WorkerCertificateCode]='{0}'", WorkerCertificateCode));
                q.Add("[StudyStatus]=1");//已达标

                ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
                RadGridMyPackage.CurrentPageIndex = 0;
                RadGridMyPackage.DataSourceID = ObjectDataSource1.ID;
                RadGridMyPackage.DataBind();
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "读取我的学习成果失败！", ex);
                return;
            }

           
        }
    }

}