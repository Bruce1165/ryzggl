using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAccess;

namespace ZYRYJG.County
{
    public partial class ZSSDDetail : BasePage
    {
        protected override bool IsNeedLogin
        {
            get
            {
                return false;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.IsAuthenticated==false) 
                {
                    Response.Redirect("~/ResultInfoPage.aspx?o=" + Server.UrlEncode("非法访问！"), false);
                    Response.End();
                    return;
                }
                DataTable dt = null;
                try
                {
                    if (string.IsNullOrEmpty(Request["z"]) == false)//传入建造师注册号
                    {
                        dt = CommonDAL.GetDataTable(string.Format(@"SELECT top 1 [ID]
                                                              ,[XM]
                                                              ,[ZJHM]
                                                              ,[ZCH]
                                                              ,[HTBH]
                                                              ,[XMMC]
                                                              ,[ZBQY]
                                                              ,[SDZT]
                                                              ,[SDSJ]
                                                              ,[BZ]
                                                              ,[VALID]
                                                              ,[CJSJ]
                                                              ,[XGSJ]
                                                              ,[CJR]
                                                              ,[XGR]
                                                              ,[CJDEPTID]
                                                              ,[XGDEPTID]
                                                              ,[CheckCode]
                                                          FROM [dbo].[jcsjk_RY_JZS_ZSSD]
                                                          where [ZCH]='{0}' order by [SDSJ] desc", Request["z"]));
                    }
                    else if (string.IsNullOrEmpty(Request["o"]) == false)//传入在施锁定记录ID
                    {
                        dt = CommonDAL.GetDataTable(string.Format(@"SELECT [ID]
                                                              ,[XM]
                                                              ,[ZJHM]
                                                              ,[ZCH]
                                                              ,[HTBH]
                                                              ,[XMMC]
                                                              ,[ZBQY]
                                                              ,[SDZT]
                                                              ,[SDSJ]
                                                              ,[BZ]
                                                              ,[VALID]
                                                              ,[CJSJ]
                                                              ,[XGSJ]
                                                              ,[CJR]
                                                              ,[XGR]
                                                              ,[CJDEPTID]
                                                              ,[XGDEPTID]
                                                              ,[CheckCode]
                                                          FROM [dbo].[jcsjk_RY_JZS_ZSSD]
                                                          where [ID]='{0}'", Request["o"]));
                    }

                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "获取建造师在施锁定记录详细失败", ex);
                    return;
                }

                if (dt != null && dt.Rows.Count > 0)
                {

                    TextBoxXM.Text = dt.Rows[0]["XM"] == null ? "" : dt.Rows[0]["XM"].ToString();
                    TextBoxZJHM.Text = dt.Rows[0]["ZJHM"] == null ? "" : dt.Rows[0]["ZJHM"].ToString();
                    TextBoxZCH.Text = dt.Rows[0]["ZCH"] == null ? "" : dt.Rows[0]["ZCH"].ToString();
                    TextBoxHTBH.Text = dt.Rows[0]["HTBH"] == null ? "" : dt.Rows[0]["HTBH"].ToString();
                    TextBoxXMMC.Text = dt.Rows[0]["XMMC"] == null ? "" : dt.Rows[0]["XMMC"].ToString();
                    TextBoxZBQY.Text = dt.Rows[0]["ZBQY"] == null ? "" : dt.Rows[0]["ZBQY"].ToString();
                    TextBoxSDZT.Text = dt.Rows[0]["SDZT"] == null ? "" : dt.Rows[0]["SDZT"].ToString()=="1"?"锁定":"解锁";
                    TextBoxSDSJ.Text = dt.Rows[0]["SDSJ"] == null ? "" : dt.Rows[0]["SDSJ"].ToString();

                }
            }

        }
    }
}