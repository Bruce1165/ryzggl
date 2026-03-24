using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZYRYJG.Unit
{
    public partial class ApplyTypeList :BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {


                ButtonSearch_Click(sender, e);
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
            string type = Request.QueryString["type"];
            var q = new QueryParamOB();
            q.Add(string.Format("ApplyType = '{0}'", type));
            if (IfExistRoleID("2") == true)//企业
            {
                q.Add(string.Format("(ENT_OrganizationsCode = '{0}' or ENT_OrganizationsCode like '________{0}_')", ZZJGDM));
            }
            if (RadTextBoxValue.Text.Trim() != "")//自定义查询项
            {
                q.Add(string.Format("{0} like '%{1}%'", RadComboBoxIten.SelectedValue, RadTextBoxValue.Text.Trim()));
            }

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridQY.CurrentPageIndex = 0;
            RadGridQY.DataSourceID = ObjectDataSource1.ID;
        }
        /// <summary>
        /// 申请填报页面地址
        /// </summary>
        public string GetApplyTypeUrl(string type)
        {
            switch (type)
            {
                case "增项注册":
                    return "../Unit/ApplyAddItem.aspx";//增项注册
                case "初始注册":
                    return "../Unit/ApplyFirstDetail.aspx";//初始注册
                case "变更注册":
                    return "../Unit/ApplyChange.aspx";//变更注册
                case "延期注册":
                    return "../Unit/ApplyContinue.aspx";//延续注册
                case "重新注册":
                    return "../Unit/ApplyRenew.aspx";//重新注册
                case "遗失补办":
                    return "../Unit/ApplyReplace.aspx";//遗失补办
                case "注销":
                    return "../Unit/ApplyCancel.aspx";//注销注册
                default:
                    return "#";
            }

        }
    }
}