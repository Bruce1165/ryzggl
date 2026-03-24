using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZYRYJG.zjs
{
    //企业信息变更
    public partial class zjsExamine : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "zjs/zjsAgency.aspx";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
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
            var q = new QueryParamOB();
            if (IfExistRoleID("20")==true)//受理
            {
                q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ZJSApplyStatus.已申报));
            }
            else if (IfExistRoleID("21") == true)//审核
            {
                q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ZJSApplyStatus.已受理));
            }
          
            else if (IfExistRoleID("23") == true)//批准
            {
                q.Add(string.Format("ApplyStatus='{0}'", EnumManager.ZJSApplyStatus.已审核));
            }

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridQY.CurrentPageIndex = 0;
            RadGridQY.DataSourceID = ObjectDataSource1.ID;
        }

        //导出EXCEL
        protected void ImageButtonOutput_Click(object sender, EventArgs e)
        {
             try
             {
                 var q = new QueryParamOB();
                 if (IfExistRoleID("20") == true)//受理
                 {
                     q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ZJSApplyStatus.已申报));
                 }
                 else if (IfExistRoleID("21") == true)//审核
                 {
                     q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ZJSApplyStatus.已受理));
                 }
                 else if (IfExistRoleID("23") == true)//决定
                 {
                     q.Add(string.Format("ApplyStatus='{0}'", EnumManager.ZJSApplyStatus.已审核));
                 }

                 //EXCEL表头明
                 string head = @"机构代码\变更前企业名称\变更后企业名称\变更证书数量";
                 //数据表的列明
                 string column = @"ENT_OrganizationsCode\ENT_NameFrom\ENT_NameTo\Num";
                 //过滤条件
                 if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/Excel/"))) System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Upload/Excel/"));
                 string filePath = string.Format("~/Upload/Excel/Excel{0}{1}.xls",UserID, DateTime.Now.ToString("yyyyMMddHHmmss"));
                 CommonDAL.OutputXls(HttpContext.Current.Server.MapPath(filePath)
                     , "View_zjs_ApplyChangeUnitName"
                     , q.ToString(), "ENT_OrganizationsCode", head.ToString(), column.ToString());
                 string size = CommonDAL.GetFileSize(HttpContext.Current.Server.MapPath(filePath));
                 spanOutput.InnerHtml = string.Format(@"<div style=""width: 98%; font-weight: bold; ""><a href=""{0}"">{1}</a><span  style=""padding-left:20px;"">（{2}）</span></div>"
                     ,UIHelp.AddUrlReadParam( filePath.Replace("~", ".."))
                     , "点击我下载"
                     , size);
             }
            catch(Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "造价工程师企业信息变更导出EXCEL失败！", ex);
            }
        }
    }
}