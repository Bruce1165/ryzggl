using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;

namespace ZYRYJG.Register
{
    public partial class NumberIssueChoice : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "NumberIssue.aspx";
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
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
             ObjectDataSource1.SelectParameters.Clear();
                QueryParamOB q = new QueryParamOB();
                q.Add("(ApplyType like '初始注册%' or ApplyType like '重新注册%')");
                q.Add("Notice='已公告'");
                //q.Add("CodeDate is null");//未编号
                if (RadTextBoxValue.Text.Trim() != "")
                {
                    q.Add(string.Format("NoticeCode like'%{0}%'", RadTextBoxValue.Text.Trim()));
                }
                ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
                RadGridADDRY.CurrentPageIndex = 0;
                RadGridADDRY.DataSourceID = ObjectDataSource1.ID;
        }


        protected void BtnSave_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("NumberIssue.aspx?o={0}&t={1}"
                ,RadGridADDRY.MasterTableView.DataKeyValues[RadGridADDRY.SelectedItems[0].ItemIndex]["NoticeCode"].ToString()
                , Server.UrlEncode(RadGridADDRY.MasterTableView.DataKeyValues[RadGridADDRY.SelectedItems[0].ItemIndex]["ApplyType"].ToString())
                ), false);
            
        }

      
    }
}