using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;
using System.Data;

namespace ZYRYJG.County
{
    public partial class CertPrintMgr : BasePage
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
            //string type = Request.QueryString["type"];
            var q = new QueryParamOB();
            //q.Add(string.Format("ApplyStatus = '{0}'", EnumManager.ApplyStatus.已公告));

            //q.Add("PSN_RegisteType = ApplyTypeCode");//申请注册类型和证书最后注册类型一致

            q.Add(string.Format("PSN_Level  = '{0}'", RadComboBoxPSN_Level.SelectedValue));//等级

            //区县只能看自己的
            if (IfExistRoleID("3") == true || IfExistRoleID("7") == true)
            {
                q.Add(string.Format("ent_city like '{0}%'", Region));
            }


            if (TextBoxValue.Text.Trim() != "")//自定义查询项
            {
                if (RadComboBoxIten.SelectedValue == "NoticeCode")//公告批次号
                {
                    q.Add(string.Format("[PSN_RegisterNO] in (SELECT [PSN_RegisterNO] FROM [dbo].[Apply] where NoticeCode ='{0}')", TextBoxValue.Text.Trim()));
                }
                else
                {
                    q.Add(string.Format("{0} like '%{1}%'", RadComboBoxIten.SelectedValue, TextBoxValue.Text.Trim()));
                }
            }
            //申请类型
            if (RadComboBoxPSN_RegisteType.SelectedValue != "全部")
            {
                q.Add(string.Format("[PSN_RegisteType]  = '{0}'", RadComboBoxPSN_RegisteType.SelectedValue));
            }
            else
            {
                q.Add("[PSN_RegisteType] <'07'");//排出注销
            }
            
            //打印类型
            if (RadComboBoxPrintType.SelectedValue == "防伪条")
            {
                q.Add("((PSN_RegisteType ='02' and (PSN_BeforENT_Name <> ENT_Name or PSN_BeforPersonName <> PSN_Name)) or (PSN_RegisteType='03' and ( PSN_RenewalProfession is  not null  and PSN_RenewalProfession <> ''))or (PSN_RegisteType='04' and ([PSN_AddProfession] is  not null  and [PSN_AddProfession] <> '')))");
                RadGridQY.MasterTableView.Columns.FindByUniqueName("PrintCert").Visible = false;
                RadGridQY.MasterTableView.Columns.FindByUniqueName("PrintCodeBar").Visible = true;
            }
            else//打印证书
            {
                q.Add("(PSN_RegisteType ='02' or PSN_RegisteType='03' or PSN_RegisteType='01'  or PSN_RegisteType='05' or PSN_RegisteType='06')");
                RadGridQY.MasterTableView.Columns.FindByUniqueName("PrintCert").Visible = true;
                RadGridQY.MasterTableView.Columns.FindByUniqueName("PrintCodeBar").Visible = false;
            }

            //审核日期
            if (RadDatePickerGetDateStart.SelectedDate.HasValue == true)
            {
                q.Add(string.Format("[PSN_RegistePermissionDate] between '{0}' and '{1}'", RadDatePickerGetDateStart.SelectedDate.Value.ToString("yyyy-MM-dd"), RadDatePickerGetDateStart.SelectedDate.Value.ToString("yyyy-MM-dd") + " 23:59:59"));
            }

            //按证书号段打印
            if (TextBoxPSN_RegisterCertificateNo_from.Text != "" || TextBoxPSN_RegisterCertificateNo_to.Text != "")
            {
                int temp;
                if (
                    (TextBoxPSN_RegisterCertificateNo_from.Text !="" && int.TryParse(TextBoxPSN_RegisterCertificateNo_from.Text, out temp) == false)
                    ||
                    (TextBoxPSN_RegisterCertificateNo_to.Text !="" && int.TryParse(TextBoxPSN_RegisterCertificateNo_to.Text, out temp) == false)
                    )
                {
                    UIHelp.layerAlert(Page, "输入证书号段格式不正确！", 5, 0);
                    return;
                }
                 
                string beginNum = "";//流水号开始
                string endNum = "";//流水号结束               

                beginNum = TextBoxPSN_RegisterCertificateNo_from.Text != "" ? TextBoxPSN_RegisterCertificateNo_from.Text : TextBoxPSN_RegisterCertificateNo_to.Text;
                endNum = TextBoxPSN_RegisterCertificateNo_to.Text != "" ? TextBoxPSN_RegisterCertificateNo_to.Text : TextBoxPSN_RegisterCertificateNo_from.Text;

                q.Add(string.Format("[PSN_RegisterCertificateNo] between '{0}' and '{1}'", beginNum.PadLeft(8, '0'), endNum.PadLeft(8, '0')));
            }

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridQY.CurrentPageIndex = 0;
            RadGridQY.DataSourceID = ObjectDataSource1.ID;
        }

        protected void ButtonPrint_Click(object sender, EventArgs e)
        {

            List<string> printList = new List<string>();//待打印证书ID集合     

            DataTable dt;
//             string sql = @"
//             SELECT *,
//		     PSN_RenewalProfession2= STUFF((select  '、'+ convert(varchar(20),PRO_Profession) FROM COC_TOW_Register_Profession
//		     WHERE PSN_ServerID =COC_TOW_Person_BaseInfo.PSN_ServerID
//		     and [PRO_ValidityEnd]=COC_TOW_Person_BaseInfo.PSN_CertificateValidity for xml path(''))
//		     ,1,1,'')
//	         FROM dbo.COC_TOW_Person_BaseInfo
//		     where 1=1 ({0})";

             string sql2 = @"
             SELECT *,
		     PSN_RenewalProfession2= STUFF((select  '、'+ convert(varchar(20),PRO_Profession) FROM COC_TOW_Register_Profession
		     WHERE PSN_ServerID =COC_TOW_Person_BaseInfo.PSN_ServerID
		     and [PRO_ValidityEnd]=COC_TOW_Person_BaseInfo.PSN_CertificateValidity for xml path(''))
		     ,1,1,'')
	         FROM dbo.COC_TOW_Person_BaseInfo
		     where 1=1 and PSN_ServerID in ({0})";

            if (RadComboBoxPrintType.SelectedValue == "证书" && (TextBoxPSN_RegisterCertificateNo_from.Text != "" || TextBoxPSN_RegisterCertificateNo_to.Text != ""))
            {
                dt = COC_TOW_Person_BaseInfoDAL.GetList(0, int.MaxValue - 1, ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, "PSN_RegisterCertificateNo");
                //dt = CommonDAL.GetDataTable(string.Format(sql, ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue));
            }
            else
            {
                //dt = COC_TOW_Person_BaseInfoDAL.GetList(0, int.MaxValue - 1, string.Format(" and PSN_ServerID in({0})", GetRadGridSelect()), "PSN_RegisterCertificateNo");
                dt = CommonDAL.GetDataTable(string.Format(sql2, GetRadGridSelect()));
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                printList.Add(dt.Rows[i]["PSN_ServerID"].ToString());
            }

            Session["printList"] = printList;
            Session["printTable"] = dt;

            if (RadComboBoxPrintType.SelectedValue == "防伪条")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "print", "SetIfrmSrc(\"CertCodeBarPrint.aspx\")" ,true);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "print",
                //"window.setTimeout(function(){ radalert(\"<div>" + message +
                //"</div>\",270,185, '系统提示');return false;},500);", true);
                //if (ifEndResponse) page.Response.End();
            }
            else//打印证书
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "print", "SetIfrmSrc(\"CertPrint.aspx\")", true);
            }
       }

        /// <summary>
        /// 获取grid勾选行PSN_ServerID集合
        /// </summary>
        /// <returns></returns>
        private string GetRadGridSelect()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i < RadGridQY.MasterTableView.Items.Count; i++)
            {
                CheckBox CheckBox1 = RadGridQY.Items[i].FindControl("CheckBox1") as CheckBox;
                if (CheckBox1.Checked)
                {
                    sb.Append(",'").Append(RadGridQY.MasterTableView.DataKeyValues[i]["PSN_ServerID"].ToString()).Append("'");
                }
            }
            if (sb.Length > 0)
            {
                sb.Remove(0, 1);
            }
            return sb.ToString();
        }

        protected string ShowRegType(string code)
        {
            switch (code)
            {
                case "01":
                    return "初始注册";         
                case "02":
                    return "变更注册";
                case "03":
                    return "延续注册";          
                case "04":
                    return "增项注册";              
                case "05":
                    return "重新注册";               
                case "06":
                    return "遗失补办";             
                default:
                    return "";
            }
        }

    }
}