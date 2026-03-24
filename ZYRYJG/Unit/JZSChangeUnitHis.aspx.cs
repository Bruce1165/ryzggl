using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;

namespace ZYRYJG.Unit
{
    public partial class JZSChangeUnitHis : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "UnitList.aspx";
            }
        }
         protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ViewState["ENT_OrganizationsCode"]=Utility.Cryptography.Decrypt(Request["o"]);

                UnitWatchMDL o = UnitWatchDAL.GetObjectByUnitCode(Utility.Cryptography.Decrypt(Request["o"]));
                ViewState["UnitWatchMDL"] = o;
                div_Tip.InnerHtml = string.Format("<p>重点核查企业：{0}。核查期：{1} - {2}</p>", o.UnitName, o.CJSJ.Value.ToString("yyyy年MM月dd日"), o.ValidEnd.Value.ToString("yyyy年MM月dd日"));
                
             

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

            UnitWatchMDL o = (UnitWatchMDL)ViewState["UnitWatchMDL"];

            q.Add(string.Format("[ENT_OrganizationsCode] ='{0}'", Utility.Cryptography.Decrypt(Request["o"])));

             q.Add(string.Format("[NoticeDate] between '{0}' and '{1}'", o.CJSJ.Value.ToString("yyyy-MM-dd"),o.ValidEnd.Value.ToString("yyyy-MM-dd 23:59:59")));

             q.Add("[ApplyStatus]='已公告' and (ApplyType='初始注册' or ApplyType='重新注册' or [ApplyTypeSub]='执业企业变更')");

             //申报日期
             if (RadDatePickerGetDateStart.SelectedDate.HasValue == true)
             {
                 q.Add(string.Format("ApplyTime >= '{0}'", RadDatePickerGetDateStart.SelectedDate.Value.ToString("yyyy-MM-dd")));
             }
             if (RadDatePickerGetDateEnd.SelectedDate.HasValue == true)
             {
                 q.Add(string.Format("ApplyTime <= '{0} 23:59:59'", RadDatePickerGetDateEnd.SelectedDate.Value.ToString("yyyy-MM-dd")));
             }
             if (RadTextBoxValue.Text.Trim() != "")//自定义查询项
             {
                 q.Add(string.Format("{0} like '%{1}%'", RadComboBoxIten.SelectedValue, RadTextBoxValue.Text.Trim()));
             }
             //办结日期
             if (RadDatePickerNoticeDateStart.SelectedDate.HasValue == true)
             {
                 q.Add(string.Format("NoticeDate >= '{0}'", RadDatePickerNoticeDateStart.SelectedDate.Value.ToString("yyyy-MM-dd")));
             }
             if (RadDatePickerNoticeDateEnd.SelectedDate.HasValue == true)
             {
                 q.Add(string.Format("NoticeDate <= '{0}  23:59:59'", RadDatePickerNoticeDateEnd.SelectedDate.Value.ToString("yyyy-MM-dd")));
             }
         
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridQY.CurrentPageIndex = 0;
            RadGridQY.DataSourceID = ObjectDataSource1.ID;


            ObjectDataSource2.SelectParameters.Clear();
            q = new QueryParamOB();

            q.Add(string.Format("([ENT_OrganizationsCode] ='{0}' or [ENT_OrganizationsCode] like '________{0}_')", Utility.Cryptography.Decrypt(Request["o"])));

            q.Add(string.Format("([PSN_CertificationDate] between '{0}' and '{1}' or [PSN_CertificateValidity] between '{0}' and '{1}')", o.CJSJ.Value.ToString("yyyy-MM-dd"), o.ValidEnd.Value.ToString("yyyy-MM-dd 23:59:59")));

            if (RadTextBoxValue.Text.Trim() != "")//自定义查询项
            {
                q.Add(string.Format("{0} like '%{1}%'", RadComboBoxIten.SelectedValue, RadTextBoxValue.Text.Trim()));
            }

            ObjectDataSource2.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridOneJZS.CurrentPageIndex = 0;
            RadGridOneJZS.DataSourceID = ObjectDataSource2.ID;
        }      

    }
}