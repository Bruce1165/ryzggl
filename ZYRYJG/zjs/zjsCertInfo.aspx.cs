using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;
using System.Data;

namespace ZYRYJG.zjs
{
    public partial class zjsCertInfo : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "zjs/zjsCertList.aspx|PersonnelFile/WorkerCertiInfoList.aspx";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                zjs_CertificateMDL o = zjs_CertificateDAL.GetObject(Utility.Cryptography.Decrypt(Request["o"]));
                if (o != null)
                {
                    #region 绑定注册信息

                    UIHelp.SetData(EditTable, o, true);
                    switch (o.PSN_RegisteType)
                    {
                        case "01":
                            TextBoxPSN_RegisteType.Text = "初始注册";
                            break;
                        case "02":
                            TextBoxPSN_RegisteType.Text = "变更注册";
                            break;
                        case "03":
                            TextBoxPSN_RegisteType.Text = "延续注册";
                            break;
                        case "07":
                            TextBoxPSN_RegisteType.Text = "注销";
                            break;
                    }
                    TextBoxPSN_CertificationDate.Text = o.PSN_CertificationDate.HasValue==false?"":o.PSN_CertificationDate.Value.ToString("yyyy-MM-dd");
                    TextBoxPSN_CertificateValidity.Text = o.PSN_CertificateValidity.HasValue == false ? "" : o.PSN_CertificateValidity.Value.ToString("yyyy-MM-dd");
                    TextBoxPSN_RegistePermissionDate.Text = o.PSN_RegistePermissionDate.HasValue == false ? "" : o.PSN_RegistePermissionDate.Value.ToString("yyyy-MM-dd");

                    TextBoxPSN_BirthDate.Text = o.PSN_BirthDate.HasValue == false ? "" : o.PSN_BirthDate.Value.ToString("yyyy-MM-dd");

                    ImgCode.Src = UIHelp.ShowFile(UIHelp.ShowFaceImageJZS(o.PSN_RegisterNO, o.PSN_CertificateNO));

                    RefreshLockStatus(o.PSN_CertificateNO);

                    #endregion

                    #region 绑定注册历史

                    string sql = @"select *,
                            CASE PSN_RegisteType 
                            WHEN '01' THEN '初始注册'
                            WHEN '02' THEN '变更注册'
                            WHEN '03' THEN '延续注册'                          
                            WHEN '07' THEN '注销'
                            ELSE '' END AS PSN_RegisteTypeName
                            from [zjs_Certificate_His] where [PSN_RegisterNO]='{0}' order by [HisTime] desc";

                    DataTable dt = CommonDAL.GetDataTable(string.Format(sql, o.PSN_RegisterNO));
                    RadGridHis.DataSource = dt;
                    RadGridHis.DataBind();

                    #endregion

                    #region 检索历次锁定记录

                    ObjectDataSource1.SelectParameters.Clear();
                    QueryParamOB q = new QueryParamOB();
                    q.Add(string.Format("[PSN_CertificateNO] = '{0}'", o.PSN_CertificateNO));//证书ID
                    ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
                    RadGridLock.CurrentPageIndex = 0;
                    RadGridLock.DataSourceID = ObjectDataSource1.ID;

                    #endregion

                    #region 绑定最近3个月社保记录

                    sql = @"select * from
                                    (
                                        SELECT	  
	                                        [CertificateCode]
	                                        ,[WorkerName]		  
	                                        ,[CreditCode]
	                                        ,[ENT_Name]
	                                        ,[JFYF]		
	                                        ,[XZName]
	                                        ,[XZCode]
	                                        ,convert(varchar(11),[CJSJ],23) as 'CJSJ'	                                    
                                        FROM [dbo].[SheBao]
                                        where [CertificateCode]='{0}' and [JFYF] between  {1} and  {2}
                                    ) p
                                    PIVOT(MAX([XZName]) FOR [XZCode] IN ([01],[02],[03],[04],[05])) AS T";
                    dt = CommonDAL.GetDataTable(string.Format(sql, o.PSN_CertificateNO
                        , DateTime.Now.AddMonths(-3).ToString("yyyyMM")
                        , DateTime.Now.AddMonths(-1).ToString("yyyyMM")));

                    RadGrid1.DataSource = dt;
                    RadGrid1.DataBind();

                    #endregion
                }
            }
        }

        private void RefreshLockStatus(string PSN_CertificateNO)
        {
            //证书锁定状态           
            bool lockStatus = LockZJSDAL.GetLockStatus(PSN_CertificateNO);
            if (lockStatus == false)//已解锁
            {
                DivDetail.Visible = false;
            }
            else
            {
                DivDetail.Visible = true;
            }
        }
    }
}