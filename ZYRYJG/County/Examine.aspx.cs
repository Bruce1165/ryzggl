using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZYRYJG.County
{
    //企业信息变更
    public partial class Examine : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "County/Agency.aspx";
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
            ObjectDataSource1.SelectParameters.Clear();
            var q = new QueryParamOB();
            if (IfExistRoleID("3")==true)//区县受理
            {
                ImageButtonOutput.Visible = true;
                q.Add(string.Format("ent_city like '{0}%'", Region));
                q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ApplyStatus.已申报));
            }
            if (IfExistRoleID("7") == true)//区县审查
            {
                q.Add(string.Format("ent_city like '{0}%'", Region));
                q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ApplyStatus.已受理));
            }
            if (IfExistRoleID("4") == true)//注册中心业务员
            {
                ImageButtonOutput.Visible = true;
                q.Add(string.Format("ApplyStatus='{0}'", EnumManager.ApplyStatus.已收件));
            }
            if (IfExistRoleID("6") == true)//注册中心领导
            {
                q.Add(string.Format("ApplyStatus='{0}'", EnumManager.ApplyStatus.已审查));
            }
            //q.Add("GROUP BY ENT_OrganizationsCode");
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridQY.CurrentPageIndex = 0;
            RadGridQY.DataSourceID = ObjectDataSource1.ID;
        }

        //区县受理
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] as ApplyMDL;
            _ApplyMDL.GetDateTime = DateTime.Now;
            _ApplyMDL.GetMan = UserName;
            _ApplyMDL.GetResult = RadioButtonListApplyStatus.SelectedValue;
            _ApplyMDL.GetRemark = TextBoxApplyGetResult.Text.Trim();
            _ApplyMDL.ApplyStatus = RadioButtonListApplyStatus.SelectedValue == "通过" ? EnumManager.ApplyStatus.已受理 : EnumManager.ApplyStatus.已驳回;
            try
            {
                ApplyDAL.Update(_ApplyMDL);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "受理失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "企业变更信息区县受理成功", string.Format("受理时间：{0}", DateTime.Now));
            UIHelp.ParentAlert(Page, "受理成功！", true);

        }

        //导出EXCEL
        protected void ImageButtonOutput_Click(object sender, EventArgs e)
        {
             try
             {
                 System.Text.StringBuilder filterSql = new System.Text.StringBuilder();
                 if (IfExistRoleID("3") == true)//区县业务员
                 {
                     filterSql.Append(string.Format("ENT_City like '{0}%' AND ApplyStatus = '{1}'", Region, EnumManager.ApplyStatus.已申报));
                 }
                 //if (IfExistRoleID("7") == true)//区县领导
                 //{
                 //  filterSql.Append(string.Format("ENT_City='{0}' AND ApplyStatus = '{1}'", Region, EnumManager.ApplyStatus.区县审查));
                 //}
                 if (IfExistRoleID("4") == true)//注册中心业务员
                 {
                     filterSql.Append(string.Format("ApplyStatus = '{0}'", EnumManager.ApplyStatus.已收件));
                 }
                 //if (IfExistRoleID("6") == true)//注册中心领导
                 // {
                 //     filterSql.Append(string.Format("ENT_City='{0}' AND ApplyStatus = '{1}'", Region, EnumManager.ApplyStatus.已审查));
                 // }
                 //EXCEL表头明
                 string head = @"组织机构代码\变更前企业名称\变更后企业名称\人数\变更前注册地址\变更后注册地址\变更前区县\变更后区县";
                 //数据表的列明
                 string column = @"ENT_OrganizationsCode\ENT_NameFrom\ENT_NameTo\Num\FromEND_Addess\ToEND_Addess\FromENT_City\ToENT_City";
                 //过滤条件
                 if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/Excel/"))) System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Upload/Excel/"));
                 string filePath = string.Format("~/Upload/Excel/Excel{0}{1}.xls",UserID, DateTime.Now.ToString("yyyyMMddHHmmss"));
                 CommonDAL.OutputXls(HttpContext.Current.Server.MapPath(filePath)
                     , "View_ApplyChange"
                     , filterSql.ToString(), "Num", head.ToString(), column.ToString());
                 string size = CommonDAL.GetFileSize(HttpContext.Current.Server.MapPath(filePath));
                 spanOutput.InnerHtml = string.Format(@"<div style=""width: 98%; font-weight: bold; ""><a href=""{0}"">{1}</a><span  style=""padding-left:20px;"">（{2}）</span></div>"
                     , UIHelp.ShowFile(filePath)
                     , "点击我下载"
                     , size);
             }
            catch(Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "企业信息变更导出EXCEL失败！", ex);
            }
        }

    }

}