using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;
using System.Data;

namespace ZYRYJG.Unit
{
    public partial class ZCZJSDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                DataTable dtJLS = jcsjk_zjsDAL.GetList(0, 1, string.Format(" and ([SFZH]='{0}')", Utility.Cryptography.Decrypt(Request["o"])), "PYDW");

                if (dtJLS != null && dtJLS.Rows.Count > 0)
                {
                    DataRow dr = dtJLS.Rows[0];
                    if (dr["XM"] != DBNull.Value) TextBoxPSN_Name.Text = dr["XM"].ToString();
                    if (dr["SFZH"] != DBNull.Value) TextBoxPSN_CertificateNO.Text = dr["SFZH"].ToString();
                    if (dr["PYDW"] != DBNull.Value) TextBoxENT_Name.Text = dr["PYDW"].ToString();
                    if (dr["Region"] != DBNull.Value) TextBox所在区县.Text = dr["Region"].ToString();
                    if (dr["ZCZH"] != DBNull.Value) TextBoxPSN_RegisterNO.Text = dr["ZCZH"].ToString();                                 
                    TextBoxPSN_CertificateValidity.Text = ((DateTime)dr["ZSYXQ"]).ToString("yyyy-MM-dd");

                    if (dr["ZY"] != DBNull.Value) TextBoxZY.Text = dr["ZY"].ToString();
                    if (dr["FZRQ"] != DBNull.Value) TextBoxFZRQ.Text = ((DateTime)dr["FZRQ"]).ToString("yyyy-MM-dd");



                    //ImgCode.Src = UIHelp.ShowFile(UIHelp.ShowFaceImage(dr["ZCZH"].ToString()));

                    ImgCode.Src = UIHelp.ShowFile(UIHelp.GetFaceImagePath("", dr["ZCZH"].ToString()));

                    //绑定工程信息
                    string sql = @"
                    select r.sfzh,r.xm,s.SGDW,g.GCBM,g.GCJSDD,g.GCMC,g.HTKGRQ,g.HTJGRQ from 
                    (
	                    --工程合同项目经理（立项）
	                    select g.gcbm ,h.[ProjectManager] xm,h.[ProjectManagerCardNo] sfzh
	                    from [dbo].[jcsjk_HT_SG] h	
	                    inner join [dbo].[jcsjk_GC_GCXX_NEW] g on h.ProjectSubID = g.[HTBH]
	                    where h.valid=1 and g.valid=1
	                    except 
	                    --施工项目经理
	                    select distinct s.gcbm,s.[SGDWXMFZR] xm,s.[SGDWXMFZRSFZH] sfzh
	                    FROM [dbo].[jcsjk_GC_WFZT_SGDW] s
	                    left join [dbo].[jcsjk_GC_GCXX_NEW] g on s.GCBM = g.GCBM
	                    where len(s.[SGDWXMFZRSFZH])>0 
                    ) r
                    left join jcsjk_GC_GCXX_NEW g
                    on r.gcbm = g.gcbm
                    left join [jcsjk_GC_WFZT_SGDW] s
                    on r.gcbm = s.gcbm
                    where r.sfzh='{0}'
                    order by g.HTKGRQ";

                    DataTable dt = CommonDAL.GetDataTable(string.Format(sql, dr["SFZH"]));
                    RadGridGC.DataSource = dt;
                    RadGridGC.DataBind();

                    //绑定注册历史
                   
                }
            }
        }
    }
}