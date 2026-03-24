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
    public partial class ZCJLSDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dtJLS = jcsjk_jlsDAL.GetList(0, 1, string.Format(" and 证件号 = '{0}'", Utility.Cryptography.Decrypt(Request["o"])), "组织机构代码");

                if (dtJLS != null && dtJLS.Rows.Count > 0)
                {
                    DataRow dr = dtJLS.Rows[0];
                    if (dr["姓名"] != DBNull.Value) TextBoxPSN_Name.Text = dr["姓名"].ToString();
                    if (dr["聘用单位"] != DBNull.Value) TextBoxENT_Name.Text = dr["聘用单位"].ToString();
                    if (dr["所在区县"] != DBNull.Value) TextBox所在区县.Text = dr["所在区县"].ToString();
                    TextBoxPSN_RegisteProfession.Text = string.Format("{0}，{1}", dr["注册专业1"], dr["注册专业2"]);
                    if (dr["执业资格证书编号"] != DBNull.Value) TextBox执业资格证书编号.Text = dr["执业资格证书编号"].ToString();
                    if (dr["注册号"] != DBNull.Value) TextBoxPSN_RegisterNO.Text = dr["注册号"].ToString();
                    if (dr["注册证书编号"] != DBNull.Value) TextBoxPSN_RegisterCertificateNo.Text = dr["注册证书编号"].ToString();
                    if (dr["证件号"] != DBNull.Value) TextBoxPSN_CertificateNO.Text = dr["证件号"].ToString();
                    if (dr["发证日期"] != DBNull.Value) TextBoxPSN_CertificationDate.Text = ((DateTime)dr["发证日期"]).ToString("yyyy-MM-dd");
                    if (dr["注册有效期"] != DBNull.Value) TextBoxPSN_CertificateValidity.Text = ((DateTime)dr["注册有效期"]).ToString("yyyy-MM-dd");



                    ImgCode.Src = UIHelp.ShowFile(UIHelp.GetFaceImagePath("",dr["证件号"].ToString()));

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

                    DataTable dt = CommonDAL.GetDataTable(string.Format(sql, dr["证件号"]));
                    RadGridGC.DataSource = dt;
                    RadGridGC.DataBind();

                    //绑定注册历史

                }
            }
        }
    }
}