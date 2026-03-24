using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;

namespace ZYRYJG.County
{
    public partial class ApplyAcceptDetail : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "County/ApplyAccept.aspx";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                divInfo.InnerText = string.Format("{0} {1} 共计上报 {2} {3} 人", Convert.ToDateTime(Request["o2"]).ToString("yyyy年MM月dd日"), Request["o1"], Request["o3"], Request["o4"]);
                ViewState["ENT_City"] = Request["o1"];
                ViewState["ReportCode"] = Request["o2"];
                ViewState["ApplyType"] = Request["o3"];
                ViewState["ReportCode"] = Request["o5"];

                ObjectDataSource1.SelectParameters.Clear();
                var q = new QueryParamOB();

                q.Add(string.Format("ent_city like '{0}%'", Request["o1"]));//区县

                //上报编号
                q.Add(string.Format("ReportCode = '{0}'", Request["o5"]));

                q.Add(string.Format("applytype ='{0}'", Request["o3"]));//申报类型

                q.Add("ApplyStatus ='已上报'");//状态

                
                ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
                RadGridADDRY.CurrentPageIndex = 0;
                RadGridADDRY.DataSourceID = ObjectDataSource1.ID;
            }
        }

        //收件
        protected void ButtonAccept_Click(object sender, EventArgs e)
        {
            try
            {
                ApplyDAL.PatchAccept(ViewState["ReportCode"].ToString(), ViewState["ENT_City"].ToString(), ViewState["ApplyType"].ToString(), UserName);

                UIHelp.ParentAlert(Page, "收件成功！", true);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "收件失败", ex);
            }
        }
    }
}