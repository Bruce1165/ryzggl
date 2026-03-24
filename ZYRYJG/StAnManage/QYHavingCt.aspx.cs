using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Telerik.Web.UI;

namespace ZYRYJG.StAnManage
{
    public partial class QYHavingCt : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) ButtonSearch_Click(sender, e);
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ObjectDataSource1.SelectParameters.Clear();

            QueryParamOB q = new QueryParamOB();
            if (rdtxtQYMC.Text.Trim() != "")   //企业名称
            {
                q.Add(string.Format("UnitName like '{0}%'", rdtxtQYMC.Text.Trim()));
            }

            if (RadTextBoxUNITCODE.Text.Trim() != "")   //机构代码
            {
                q.Add(string.Format("UNITCODE like '{0}%'", RadTextBoxUNITCODE.Text.Trim()));
            }
            if (txtCertificateCode.Text.Trim() != "")//证书编号
            {
                q.Add(string.Format("CERTIFICATECODE like '{0}%'", txtCertificateCode.Text.Trim()));
            }
            if (RadDatePicker_ValidEndDate.SelectedDate.HasValue)//有效期至
            {
                q.Add(string.Format("ValidEndDate ='{0}'", RadDatePicker_ValidEndDate.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }
            switch (RadComboBoxStatus.SelectedValue)//状态
            {
                case "当前有效":
                    q.Add(string.Format("ValidEndDate >='{0}' and ([STATUS] = '首次' or [STATUS] = '续期' or [STATUS] = '进京变更' or [STATUS] = '京内变更' or [STATUS] = '补办')", DateTime.Now.ToString("yyyy-MM-dd")));
                    break;
                case "已过期":
                    q.Add(string.Format("ValidEndDate <'{0}'", DateTime.Now.ToString("yyyy-MM-dd")));
                    break;
                case "离京变更":
                    q.Add(string.Format("[STATUS] = '{0}'", "离京变更"));
                    break;
                case "注销":
                    q.Add(string.Format("[STATUS] = '{0}'", "注销"));
                    break;

            }

            if (PostSelect1.PostTypeID != "") q.Add(string.Format("PostTypeID = {0}", PostSelect1.PostTypeID));     //岗位类别
            if (PostSelect1.PostID != "") q.Add(string.Format("PostID = {0}", PostSelect1.PostID));               //岗位工种

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGrid1.CurrentPageIndex = 0;
            RadGrid1.DataSourceID = ObjectDataSource1.ID;
        }
    }
}
