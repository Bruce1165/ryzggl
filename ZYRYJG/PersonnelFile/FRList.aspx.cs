using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Telerik.Web.UI;

namespace ZYRYJG.PersonnelFile
{
    public partial class FRList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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

            QueryParamOB q = new QueryParamOB();
            if (rdtxtWorkerName.Text.Trim() != "")    //姓名
            {
                q.Add(string.Format("fddbr like '%{0}%'", rdtxtWorkerName.Text.Trim()));
            }
           
            if (rdtxtZJHM.Text.Trim() != "")   //证件号码
            {
                q.Add(string.Format("zjhm like '%{0}%'", rdtxtZJHM.Text.Trim()));
            }

             if (RadTextBoxENT_Name.Text.Trim() != "")   //企业名称
            {
                q.Add(string.Format("qymc like '%{0}%'", RadTextBoxENT_Name.Text.Trim()));
            }

             if (RadTextBoxENT_OrganizationsCode.Text.Trim() != "")   //组织机构代码或社会统一信用代码
            {
                q.Add(string.Format("(zzjgdm like '%{0}%' or tyshxydm like '%{0}%')", RadTextBoxENT_OrganizationsCode.Text.Trim()));
            }
           

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGrid1.CurrentPageIndex = 0;
            RadGrid1.DataSourceID = ObjectDataSource1.ID;
        }

        //protected void Button2_Click(object sender, EventArgs e)
        //{
        //    rdtxtWorkerName.Text = "";
        //    rdtxtZJHM.Text = "";
        //}
    }
}
