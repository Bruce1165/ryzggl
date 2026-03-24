using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;

namespace ZYRYJG.CertifManage
{
    public partial class EndWarn : BasePage
    {
        protected override bool IsNeedLogin
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 证书类别
        /// </summary>
        protected string CertType
        {
            get { return ViewState["CertType"].ToString(); }
        }

        protected override void Page_Init(object sender, EventArgs e)
        {
            base.Page_Init(sender, e);
            ViewState["CertType"] = Request["o"];
            bindGrid(Request["o"]);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                //ViewState["CertType"] = Request["o"];
                //bindGrid(Request["o"]);
            }
        }

        protected void bindGrid(string CertType)
        {
            ObjectDataSource1.SelectParameters.Clear();
            var q = new QueryParamOB();
            switch (CertType)
            {
                case "ej":
                    q.Add(string.Format("p.[PRO_ValidityEnd] between '{0}' and '{1}' and  c.PSN_RegisteType < '07' and a.applyid is null", DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.AddMonths(3).ToString("yyyy-MM-dd")));
                    if (IfExistRoleID("0") == true)//个人
                    {
                        q.Add(string.Format("c.PSN_CertificateNO like '{0}'", WorkerCertificateCode));
                    }
                    if (IfExistRoleID("2") == true)//企业
                    {
                        q.Add(string.Format(" (c.ENT_OrganizationsCode = '{0}' or c.ENT_OrganizationsCode like '________{0}_')", ZZJGDM));
                    }
                    if (IfExistRoleID("3") == true || IfExistRoleID("7") == true)//区县
                    {
                        q.Add(string.Format(" (c.ENT_City like '{0}%') ", Region));
                    }
                    ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
                    ObjectDataSource1.SelectMethod = "GetListValidEndWarnEJ";
                    ObjectDataSource1.SelectCountMethod = "SelectCountValidEndWarnEJ";
                    RadGridCert.DataSourceID = "ObjectDataSource1";
                    break;
                case "ez":
                    q.Add(string.Format("c.[PSN_CertificateValidity] between '{0}' and '{1}' and  c.PSN_RegisteType < '07' and a.applyid is null", DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.AddMonths(3).ToString("yyyy-MM-dd")));
                    if (IfExistRoleID("0") == true)//个人
                    {
                        q.Add(string.Format("c.PSN_CertificateNO like '{0}'", WorkerCertificateCode));
                    }
                    if (IfExistRoleID("2") == true)//企业
                    {
                        q.Add(string.Format("c.ENT_OrganizationsCode = '{0}'", SHTJXYDM));
                    }
                    ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
                    ObjectDataSource1.SelectMethod = "GetListValidEndWarnEZ";
                    ObjectDataSource1.SelectCountMethod = "SelectCountValidEndWarnEZ";
                    RadGridCert.DataSourceID = "ObjectDataSource1";
                    break;
                case "slr":
                    q.Add(string.Format("c.POSTTYPEID=1 and c.VALIDENDDATE between '{0}' and '{1}' and  c.[STATUS] <> '待审批' AND c.[STATUS] <> '待进京审批' AND c.[STATUS] <> '离京变更' AND c.[STATUS] <> '注销' and a.CertificateContinueid is null", DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.AddMonths(3).ToString("yyyy-MM-dd")));
                    if (IfExistRoleID("0") == true)//个人
                    {
                        q.Add(string.Format("c.WorkerCertificateCode like '{0}'", WorkerCertificateCode));
                    }
                    if (IfExistRoleID("2") == true)//企业
                    {
                        q.Add(string.Format("c.UnitCode = '{0}'", ZZJGDM));
                    }
                    if (IfExistRoleID("3") == true || IfExistRoleID("7") == true)//区县
                    {
                        q.Add(string.Format(" (s.username like '{0}%') ", Region));
                    }
                    ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
                    ObjectDataSource1.SelectMethod = "GetListValidEndWarnSLR";
                    ObjectDataSource1.SelectCountMethod = "SelectCountValidEndWarnSLR";
                    RadGridCert.DataSourceID = "ObjectDataSource1";
                    break;
                case "tzzy":
                    q.Add(string.Format("c.POSTTYPEID=2 and c.VALIDENDDATE between '{0}' and '{1}' and  c.[STATUS] <> '待审批' AND c.[STATUS] <> '注销' and a.CertificateContinueid is null", DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.AddMonths(3).ToString("yyyy-MM-dd")));
                    if (IfExistRoleID("0") == true)//个人
                    {
                        q.Add(string.Format("c.WorkerCertificateCode like '{0}'", WorkerCertificateCode));
                    }
                    if (IfExistRoleID("2") == true)//企业
                    {
                        q.Add(string.Format("c.UnitCode = '{0}'", ZZJGDM));
                    }
                    ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
                    ObjectDataSource1.SelectMethod = "GetListValidEndWarnTZZY";
                    ObjectDataSource1.SelectCountMethod = "SelectCountValidEndWarnTZZY";
                    RadGridCert.DataSourceID = "ObjectDataSource1";
                    break;
                default:
                    break;
            }

        }

        //导出excel
        protected void ButtonExportToExcel_Click(object sender, EventArgs e)
        {
            //if (RadGridCert.MasterTableView.VirtualItemCount == 0)
            //{
            //    UIHelp.layerAlert(Page, "没有可导出的数据！");
            //    return;
            //}
            string tableName = "";
            string colHead = @"姓名\证书类型\证书编号\专业\有效期截止日期";
            string colName = "";
            string sortBy = "";

            string saveFile = string.Format("~/UpLoad/CertifEnterApply/过期预警名单_{0}.xls", DateTime.Now.ToString("yyyyMMddhhmmss"));//保存文件名
            try
            {
                //检查临时目录
                if (!System.IO.Directory.Exists(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"));

                switch(CertType)
                {
                    case "ej":
                        tableName = @"[dbo].[COC_TOW_Person_BaseInfo] c 
                                    inner join [dbo].[COC_TOW_Register_Profession] p on c.[PSN_ServerID] = p.[PSN_ServerID]
                                    left join Apply a on c.PSN_RegisterNO = a.PSN_RegisterNo and a.ApplyType='延期注册' and a.ApplyTime > dateadd(month,-4,p.PRO_ValidityEnd)";
                        colName = @"c.PSN_Name\'二级建造师'\c.PSN_RegisterNO\p.PRO_Profession\CONVERT(varchar(10), p.PRO_ValidityEnd, 20)";
                        sortBy = "c.PSN_RegisterNO";
                        break;
                    case "ez":
                        tableName = @"[dbo].[zjs_Certificate] c 
                                  left join [dbo].[zjs_Apply] a on c.PSN_RegisterNO = a.PSN_RegisterNo 
                                  and a.ApplyType='延期注册' and a.ApplyTime > dateadd(month,-4,c.[PSN_CertificateValidity])";
                        colName = @"c.PSN_Name\'二级造价工程师'\c.PSN_RegisterNO\c.PSN_RegisteProfession\CONVERT(varchar(10), c.PSN_CertificateValidity, 20)";
                        sortBy = "c.PSN_RegisterNO";
                        break;
                    case "slr":
                        tableName = @"[dbo].[CERTIFICATE] c 
                                    left join [dbo].[CERTIFICATECONTINUE] a 
                                    on c.posttypeid=1 and c.[CERTIFICATEID] = a.[CERTIFICATEID]  and a.APPLYDATE > dateadd(month,-4,c.VALIDENDDATE)";
                        colName = @"c.WORKERNAME\'安全生产三类人'\c.CERTIFICATECODE\c.POSTNAME\CONVERT(varchar(10), c.VALIDENDDATE, 20)";
                        sortBy = "c.CERTIFICATECODE";
                        break;
                    case "tzzy":
                        tableName = @"[dbo].[CERTIFICATE] c 
                                     left join [dbo].[CERTIFICATECONTINUE] a 
                                     on c.posttypeid=2 and c.[CERTIFICATEID] = a.[CERTIFICATEID]  and a.APPLYDATE > dateadd(month,-4,c.VALIDENDDATE)";
                        colName = @"c.WORKERNAME\'特种作业'\c.CERTIFICATECODE \c.POSTNAME\CONVERT(varchar(10), c.VALIDENDDATE, 20)";
                        sortBy = "c.CERTIFICATECODE";
                        break;
                    default:
                        break;
                }                
                CommonDAL.OutputXls(MyWebConfig.DBOutputPath + Server.MapPath(saveFile).Substring(1)
                    , tableName, ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, sortBy, colHead, colName);

            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "导出过期预警名单失败！", ex);
                return;
            }

            //List<ResultUrl> url = new List<ResultUrl>();
            //url.Add(new ResultUrl("过期预警名单下载", saveFile));
            //UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);

            string size = CommonDAL.GetFileSize(HttpContext.Current.Server.MapPath(saveFile));
            spanOutput.InnerHtml = string.Format(@"<a href=""{0}"">{1}</a><span  style=""padding-left:20px;"">导出结果下载（{2}）</span>"
                , UIHelp.ShowFile(saveFile)
                , "点击我下载（或鼠标右键另存）"
                , size);
        }
    }
}