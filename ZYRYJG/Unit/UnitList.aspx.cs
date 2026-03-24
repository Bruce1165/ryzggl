using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;
using System.Data;

namespace ZYRYJG.Unit
{
    public partial class UnitList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ButtonSearch_Click(sender, e);
            }
        }

        //根据条件查询
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ObjectDataSource1.SelectParameters.Clear();
            var q = new QueryParamOB();
            if (RadTextBoxENT_OrganizationsCode.Text.Trim() != "")//组织机构代码
            {
                q.Add(string.Format("(ENT_OrganizationsCode like '%{0}%' or [CreditCode] = '{0}')", RadTextBoxENT_OrganizationsCode.Text.Trim()));
            }
            if (RadTextBoxENT_Name.Text.Trim() != "")//企业名称
            {
                q.Add(string.Format("ENT_Name like '%{0}%'", RadTextBoxENT_Name.Text.Trim()));
            }
            if (RadComboBoxENT_City.SelectedValue != "")
            {
                q.Add(string.Format("ENT_City like '{0}'", RadComboBoxENT_City.SelectedValue));
            }

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridQY.CurrentPageIndex = 0;
            RadGridQY.DataSourceID = ObjectDataSource1.ID;
        }

        /// <summary>
        /// 重点监管企业（组织机构代码）
        /// </summary>
        DataTable dtUnitWatch
        {
            get
            {
                if (Cache["dtUnitWatch"] == null)
                {
                    string sql = @"SELECT substring([CreditCode],9,9)  FROM [dbo].[UnitWatch]  where [ValidEnd]>getdate() and len([CreditCode])=18";

                    DataTable dtUnitWatch = CommonDAL.GetDataTable(sql);
                    DataColumn[] c = new DataColumn[1];
                    c[0] = dtUnitWatch.Columns[0];
                    dtUnitWatch.PrimaryKey = c;
                    Utility.CacheHelp.AddAbsoluteeExpirationCache(Page, "dtUnitWatch", dtUnitWatch, 12);

                    return dtUnitWatch;
                }
                else
                {
                    return (DataTable)Cache["dtUnitWatch"];
                }
            }
        }

        /// <summary>
        /// 检查组织机构代码是否在重点监管企业列表中
        /// </summary>
        /// <param name="ZZJGDM">组织机构代码</param>
        /// <returns>true：是重点检查企业，false：不是重点检查企业</returns>
        protected bool CheckUnitWatch(string ZZJGDM)
        {
            if (dtUnitWatch.Rows.Find(ZZJGDM) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}