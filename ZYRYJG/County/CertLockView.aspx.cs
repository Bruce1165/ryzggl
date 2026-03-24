using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Net;
using System.IO;
using System.Data;
using Model;
using DataAccess;
using System.Drawing;
using System.Configuration;

namespace ZYRYJG.County
{
    public partial class CertLockView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["o"]))
                {
                    DataTable dt = CommonDAL.GetDataTable(string.Format(@"
                        SELECT top 1 [LockID]
                              ,[LockJZS].[PSN_ServerID]
                              ,[LockJZS].[LockType]
                              ,[LockJZS].[LockTime]
                              ,[LockJZS].[LockEndTime]
                              ,[LockJZS].[LockPerson]
                              ,[LockJZS].[LockRemark]
                              ,[LockJZS].[UnlockTime]
                              ,[LockJZS].[UnlockPerson]
                              ,[LockJZS].[UnlockRemark]
                              ,[LockJZS].[LockStatus]
	                          ,[COC_TOW_Person_BaseInfo].[PSN_RegisterCertificateNo] 
	                          ,[COC_TOW_Person_BaseInfo].[PSN_Name]
	                          ,[COC_TOW_Person_BaseInfo].[PSN_CertificateNO]
                          FROM [dbo].[LockJZS]
                          inner join [COC_TOW_Person_BaseInfo] on [LockJZS].[PSN_ServerID] = [COC_TOW_Person_BaseInfo].[PSN_ServerID]
                          where [COC_TOW_Person_BaseInfo].PSN_CertificateNO='{0}'
                          order by [LockJZS].[LockEndTime] desc", Request["o"]));

                    if (dt !=null & dt.Rows.Count>0)
                    {

                        main.InnerHtml = string.Format(
@"<p style='line-height:200%; margin:40px 40px; font-size:16px;padding:50px 0 50px 320px'>
加锁原因：{1}<br/>
被锁证书信息：<br/>
姓名：{2}<br/>
身份证号：{3}<br/>
证书编号：{4}<br/>
解锁请联系北京市住建委注册中心。<br/>
<a target=""_blank"" href=""http://120.52.185.14/Register/NewsView.aspx?o={5}"" style=""text-decoration:underline"">（关于注册状态异常标记的解释及处理流程）</a>
</p>", Convert.ToDateTime(dt.Rows[0]["LockTime"]).ToString("yyyy-MM-dd")
                                 , dt.Rows[0]["LockRemark"]
                                 , dt.Rows[0]["PSN_Name"]
                                 , dt.Rows[0]["PSN_CertificateNO"]
                                 , dt.Rows[0]["PSN_RegisterCertificateNo"]
                                 , Utility.Cryptography.Encrypt("1cf44934-db2b-4de2-9ab5-a6650417824a"));
                    }
                    else 
                    {
                        main.InnerHtml = "<span style='color:red;'>未查到锁定信息。</span>";
                    }

                }

            }
        }

    }
}