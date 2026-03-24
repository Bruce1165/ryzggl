using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAccess;
using Model;
using System.Text;

namespace ZYRYJG
{
    //项目监控详细
    public partial class GCDetail : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "City/ItemsMonitoring.aspx";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string GCBM = Utility.Cryptography.Decrypt(Request["o"]);

                bindGCDetail(GCBM);

                BindXMJL(GCBM);
            }
        }

        /// <summary>
        /// 绑定工程基本信息
        /// </summary>
        protected void bindGCDetail(string GCBM)
        {
            try
            {
                string sql = @"select top 1 * from  [dbo].[jcsjk_GC_GCXX_NEW] where valid=1 and GCBM ='{0}'";
                DataTable dt = CommonDAL.GetDataTable(string.Format(sql, GCBM));
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["GCMC"] != null) LabelGCGM.Text = dt.Rows[0]["GCMC"].ToString();
                    if (dt.Rows[0]["GCGM"] != null) LabelGCGM.Text = dt.Rows[0]["GCGM"].ToString();
                    if (dt.Rows[0]["GCBM"] != null) LabelGCBM.Text = dt.Rows[0]["GCBM"].ToString();
                    if (dt.Rows[0]["GCXZ"] != null) LabelGCXZ.Text = dt.Rows[0]["GCXZ"].ToString();
                    if (dt.Rows[0]["HTBH"] != null) LabelHTBH.Text = dt.Rows[0]["HTBH"].ToString();
                    if (dt.Rows[0]["HTJG"] != null) LabelHTJG.Text = dt.Rows[0]["HTJG"].ToString();
                    if (dt.Rows[0]["GCLB"] != null) LabelGCLB.Text = dt.Rows[0]["GCLB"].ToString();
                    if (dt.Rows[0]["GCJSDD"] != null) LabelGCJSDD.Text = dt.Rows[0]["GCJSDD"].ToString();
                    if (dt.Rows[0]["JGLX"] != null) LabelJGLX.Text = dt.Rows[0]["JGLX"].ToString();
                    if (dt.Rows[0]["GCGM"] != null) LabelGCGM.Text = dt.Rows[0]["GCGM"].ToString();
                    if (dt.Rows[0]["GCSZQX"] != null) LabelGCSZQX.Text = dt.Rows[0]["GCSZQX"].ToString();
                    if (dt.Rows[0]["GHXKZH"] != null) LabelGHXKZH.Text = dt.Rows[0]["GHXKZH"].ToString();
                    if (dt.Rows[0]["JSDWFDDBR"] != null) LabelJSDWFDDBR.Text = dt.Rows[0]["JSDWFDDBR"].ToString();
                    if (dt.Rows[0]["JSDWFDDBRDH"] != null) LabelJSDWFDDBRDH.Text = dt.Rows[0]["JSDWFDDBRDH"].ToString();
                    if (dt.Rows[0]["JSDWXMFZR"] != null) LabelJSDWXMFZR.Text = dt.Rows[0]["JSDWXMFZR"].ToString();
                    if (dt.Rows[0]["JSDWXMFZRDH"] != null) LabelJSDWXMFZRDH.Text = dt.Rows[0]["JSDWXMFZRDH"].ToString();
                }

            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "获取工程详细信息失败。", ex);
            }
        }

        /// <summary>
        /// 绑定项目经理阶段历史
        /// </summary>
        protected void BindXMJL(string GCBM)
        {
            string sql = @"select * from (
	                            select 1 sortID,'立项' step,recordTime,h.[ProjectManager] xm,h.[ProjectManagerCardNo] sfzh,isnull([ContractAlter],'') + isnull([AssureBook],'') remark
	                            from [dbo].[jcsjk_HT_SG] h	
	                            inner join [dbo].[jcsjk_GC_GCXX_NEW] g on h.ProjectSubID = g.[HTBH]
	                            where h.valid=1 and g.valid=1 and g.gcbm='{0}'
	                            --order by recordTime
	                            union all 
	                            --施工许可证项目经理
	                            select 2,'施工许可证' ,x.fzrq,d.sgdwxmfzr,d.sgdwxmfzrsfzh,'' FROM [dbo].[jcsjk_GC_WFZT_SGDW] d
	                            inner join [dbo].[jcsjk_GC_SGXKZ_NEW] x on d.gcbm= x.gcbm
	                            where d.gcbm='{0}'
	                            union all 
	                            --施工许可证变更
	                            select 2,'施工许可证',bgsj,bgqz,'',BGX +'从“' +BGQZ +'”变更为“'+ BGHZ+'”'	FROM  dbo.jcsjk_GC_SGXKZBGJL_NEW
	                            where BGX ='施工单位项目负责人' and gcbm ='{0}'
	                            union all 
                                --企业自填质量曾诺信息
                                select  3,'质量监督',cjsj,CNRXM xm,'',''
                                from [dbo].[jcsjk_GC_CP_GCZLCNS]  S
                                where dwlx ='施工总承包单位'  and gcbm='{0}'
                        ) t order by sortID ,recordTime ";
            DataTable dt = CommonDAL.GetDataTable(string.Format(sql, GCBM));
            string xm = "";//姓名
            string step = "";//阶段
            for (int i = dt.Rows.Count - 1; i >= 0;i-- )
            {
                if (dt.Rows[i]["step"].ToString() == step && dt.Rows[i]["xm"].ToString() == xm)
                {
                    dt.Rows.Remove(dt.Rows[i+1]);
                }
                else
                {
                    step = dt.Rows[i]["step"].ToString();
                    xm = dt.Rows[i]["xm"].ToString();
                }
            }
            RadGridQY.DataSource = dt;
            RadGridQY.DataBind();
        }

    }
}