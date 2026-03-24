using System;
using System.Web.UI;

namespace ZYRYJG
{
    public partial class CheckAll : UserControl
    {
        /// <summary>
        /// 是否全选
        /// </summary>
        public bool IsCheckAll
        {
            get { return CheckBoxAll.Checked; }
        }

        /// <summary>
        /// 最后一次操作全选控件是否是全选
        /// </summary>
        public bool IsCheckAllOfLastClick
        {
            get { return HiddenFieldSelectAll.Value.ToLower() == "true"; }
        }

        /// <summary>
        /// 勾选全选后刷新次数
        /// </summary>
        public string RefreshCount
        {
            get { return HiddenFieldSelectAllRefreshCount.Value; }
        }

        /// <summary>
        /// 初始化全选控件
        /// </summary>
        public void IniCheckAll()
        {
            CheckBoxAll.Checked = false;
            HiddenFieldSelectAll.Value = "";
            HiddenFieldSelectAllRefreshCount.Value = "0";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["RefreshCount"] = null;
            }
            else
            {
                if (Request.Form[CheckBoxAll.ClientID.Replace("_", "$")] == "on")//Request.Form["ctl00$ContentPlaceHolder1$RadGridApply$ctl00$ctl02$ctl01$CheckAll1$CheckBoxAll"]
                {
                    CheckBoxAll.Checked = true;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "checkBoxAll_Click", "checkBoxAllClick('" + CheckBoxAll.ClientID + "');", true);
                }
                else
                {
                    CheckBoxAll.Checked = false;
                }


                Page p = this.Parent.Page;
                Type pageType = p.GetType();

                if (Session["RefreshCount"] != null)
                {
                    HiddenFieldSelectAll.Value = "";
                    HiddenFieldSelectAllRefreshCount.Value = "1";
                    Session["RefreshCount"] = null;
                }
                else
                {
                    HiddenFieldSelectAll.Value = Request.Form[HiddenFieldSelectAll.ClientID.Replace("_", "$")];
                    if (Request.Form[HiddenFieldSelectAllRefreshCount.ClientID.Replace("_", "$")] != "")
                    {
                        HiddenFieldSelectAllRefreshCount.Value = Convert.ToString(Convert.ToInt32(Request.Form[HiddenFieldSelectAllRefreshCount.ClientID.Replace("_", "$")]) + 1);
                    }
                }
            }
        }

    }
}