using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZYRYJG.Unit
{
    public partial class PeopleList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              ObjectDataSource1.SelectParameters.Clear();
              var q = new QueryParamOB();
              q.Add(string.Format("(ENT_OrganizationsCode = '{0}' or ENT_OrganizationsCode like '________{0}_')", Utility.Cryptography.Decrypt(Request["p"])));
              ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
              RadGridQY.CurrentPageIndex = 0;
              RadGridQY.DataSourceID = ObjectDataSource1.ID;
              //Unit_Name.InnerText ="";
              string str = "";
              DataTable dt = DataAccess.COC_TOW_Person_BaseInfoDAL.PeopleCount(Utility.Cryptography.Decrypt(Request["p"]));
              for (int i = 0; i < dt.Rows.Count; i++)
              {
                  str += dt.Rows[i]["LX"].ToString()+" ：";
                  str += int.Parse(dt.Rows[i]["Num"].ToString()) > 0 ? dt.Rows[i]["Num"].ToString() + " 人 " : 0 + " 人 ";
              }     

              PeopleCount.InnerText = str;
            }
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ObjectDataSource1.SelectParameters.Clear();
            var q = new QueryParamOB();
            q.Add(string.Format("(ENT_OrganizationsCode = '{0}' or ENT_OrganizationsCode like '________{0}_')", Utility.Cryptography.Decrypt(Request["p"])));
            if (RadTextBoxValue.Text.Trim() != "")//自定义查询项
            {
                q.Add(string.Format("{0} like '%{1}%'", RadComboBoxIten.SelectedValue, RadTextBoxValue.Text.Trim()));
            }
            if (RadDatePickerGetDateStart.SelectedDate.HasValue==true)
            {
                q.Add(string.Format("PSN_CertificateValidity >= '{0}'", RadDatePickerGetDateStart.SelectedDate.Value));
            }
            if (RadDatePickerGetDateEnd.SelectedDate.HasValue == true)
            {
                q.Add(string.Format("PSN_CertificateValidity <= '{0}'", RadDatePickerGetDateEnd.SelectedDate.Value));
            }
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridQY.CurrentPageIndex = 0;
            RadGridQY.DataSourceID = ObjectDataSource1.ID;

        }

        //protected void ButtonReturn_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("UnitList.aspx",true);
        //}
    }
}