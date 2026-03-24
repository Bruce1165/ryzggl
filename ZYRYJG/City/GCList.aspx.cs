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
    //项目监控列表
    public partial class GCList : BasePage
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
                ButtonSearch_Click(sender, e);


            }
        }
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (UIHelp.CheckSQLParam() == false)
                {
                    //UIHelp.Alert(Page, "输入信息存在非法字符，请勿非法提交。");
                    UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                    return;
                }

                ObjectDataSource1.SelectParameters.Clear();
                var q = new QueryParamOB();
                string qx = Region;
                switch (qx)
                {
                    case "北京市住房和城乡建设委员会":
                        qx = "全市";
                        break;
                    case "西城区":
                        qx = "西城区宣武区";
                        break;
                    case "东城区":
                        qx = "东城区崇文区";
                        break;
                    case "亦庄":
                        qx = "亦庄开发区经济技术开发区";
                        break;
                }

                if (Region != "全市")//区县
                {
                    //q.Add(string.Format("patindex('%'+ g.GCSZQX +'%','{0}') >0", qx));
                    q.Add(string.Format("patindex('%{0}%',g.GCSZQX) >0", qx));
                }
                if (RadTextBoxValue.Text.Trim() != "")//自定义查询项
                {
                    q.Add(string.Format("g.{0} like '%{1}%'", RadComboBoxIten.SelectedValue, RadTextBoxValue.Text.Trim()));
                }
                
                string sql = @"select g.* from [dbo].[jcsjk_GC_GCXX_NEW] g 
                                inner join 
                                (
	                                select r.gcbm from
	                                (
		                                select gcbm,xm from
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
		                                ) t 
		                                except
		                                --企业自填质量曾诺信息
		                                select  GCBM,CNRXM xm
		                                from [dbo].[jcsjk_GC_CP_GCZLCNS]  S
		                                where dwlx ='施工总承包单位' 
	                                ) r
	                                inner join dbo.jcsjk_GC_CP_SGXKZ j on r.gcbm = j.gcbm
	                                where  j.sgjd='在施'
	                                 group by r.gcbm
	                                having count(*) >2
                                ) t on g.gcbm = t.gcbm 
                                where 1=1 {0}";
                DataTable dt = CommonDAL.GetDataTable(string.Format(sql, q.ToWhereString()));
                RadGridQY.DataSource = dt;
                RadGridQY.DataBind();
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "获取在施频繁变更项目经理数据失败。", ex);
            }
        }

        protected void RadGridQY_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                e.Item.Cells[RadGridQY.Columns.FindByUniqueName("RowNum").OrderIndex].Text = (e.Item.ItemIndex +1).ToString();
            }

        }
    }
}