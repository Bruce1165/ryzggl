using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using System.Data;
using DataAccess;


namespace ZYRYJG.Unit
{
    public partial class FilialeInfo : BasePage
    {
        /// <summary>
        /// 是否为集团企业
        /// </summary>
        protected bool IfJT { get { return Convert.ToBoolean(ViewState["是否为集团"]); } }

        protected void Page_Load(object sender, EventArgs e)
        {
             if(!this.IsPostBack)
             {
                 DataTable dt = CommonDAL.GetDataTable("select * from [Dict_JTQY]");
                 DataColumn[] pk = new DataColumn[1];
                 pk[0] = dt.Columns[0];
                 dt.PrimaryKey=pk;

                 ViewState["是否为集团"] = (dt.Rows.Find(UserName) != null);

                 if (IfExistRoleID("2") == true)//企业
                 {
                     ButtonSearch_Click(sender, e);
                 }
             }
        }

        private void BindDate(int pageIndex, int pageSize, string strwhere)
        {
            DataTable dt = CommonDAL.GetDataTable(string.Format(@"
SELECT  row_number() over(order by ZZJGDM) as RowNum,* FROM [dbo].[View_TI_QY_PersonCount] 
WHERE HYLZGX = (SELECT distinct HYLZGX FROM [dbo].[View_TI_QY_PersonCount] 
                WHERE (ZZJGDM='{0}' OR ZZJGDM  like '________{0}_')
                ) 
AND (ZZJGDM NOT like '%{0}%') 
and isnull((select 1 from [Dict_JTQY] where QYMC ='{1}'),0)=1", ZZJGDM, UserName));




            //DataTable dt = CommonDAL.GetDataTable(string.Format("SELECT  row_number() over(order by ZZJGDM) as RowNum,* FROM [dbo].[View_TI_QY_PersonCount] WHERE 1=1 {0}", strwhere));

            RadGridQY.DataSource = dt;
          
            RadGridQY.DataBind();
        }
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }

            string strwhere = string.Format(@"AND HYLZGX = (SELECT distinct HYLZGX FROM [dbo].[View_TI_QY_PersonCount] WHERE (ZZJGDM='{0}' OR ZZJGDM  like '________{0}_')) AND (ZZJGDM NOT like '%{0}%') {1}", ZZJGDM, IfJT==true?"":" and 1>2 ");
            //string strwhere = string.Format("AND HYLZGX = (SELECT distinct HYLZGX FROM [dbo].[View_QYLSGX] WHERE (ZZJGDM='{0}' OR ZZJGDM  like '________{0}_')) AND (ZZJGDM NOT like '%{0}%')",ZZJGDM);
           if(!string.IsNullOrEmpty(RadTextBoxValue.Text))
           {
               strwhere = strwhere+string.Format("AND {0}='{1}'", RadComboBoxIten.SelectedValue, RadTextBoxValue.Text);
           }
           BindDate(0, 10, strwhere);
        }

       
      
       
    }
}