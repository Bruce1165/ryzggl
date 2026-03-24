using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZYRYJG.County
{
    public partial class LockList : BasePage
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
            var q = new QueryParamOB();

            if (RadComboBoxIten.SelectedValue == "LX" && RadTextBoxValue.Text.Trim() == "二级建造师" && RadComboBoxSDZT.SelectedValue == "解锁" && RadComboBoxDCCZ.SelectedValue == "是")
            {
                divJS.Visible = true;
                BtnSave.Visible = true;
            }
            else
            {
                divJS.Visible = false;
                BtnSave.Visible = false;
            }

            //q.Add("1=1");
            //q.Add(string.Format("(ENT_OrganizationsCode = '{0}' or ENT_OrganizationsCode like '________{0}_')", Request["p"]));
            if (RadTextBoxValue.Text.Trim() != "")//自定义查询项
            {
                q.Add(string.Format("a.{0} like '%{1}%'", RadComboBoxIten.SelectedValue, RadTextBoxValue.Text.Trim()));
            }
            if (RadComboBoxSDZT.SelectedValue!="")
            {
                if (RadComboBoxSDZT.SelectedValue == "加锁")
                {
                    q.Add(string.Format("LockStates = '{0}' ", "加锁"));
                }
                else
                {
                    q.Add(string.Format("(LockStates ='{0}' or LockStates is null)  ", "解锁"));
                }

            } 
            if (RadComboBoxDCCZ.SelectedValue!="")
            {
                if (RadComboBoxDCCZ.SelectedValue=="是")
                {
                      q.Add(string.Format("a.PSN_CertificateNO IN ({0})","select PSN_CertificateNO from Apply where ApplyType='重新注册' and ApplyStatus='已公告' and ConfirmResult='通过' and DATEADD(year,1,NoticeDate)>GETDATE()  group by PSN_CertificateNO having COUNT(PSN_CertificateNO)>1"));
                }
            }

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridQY.CurrentPageIndex = 0;
            RadGridQY.DataSourceID = ObjectDataSource1.ID;
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {

            if (RadDatePickerGetDateEnd.SelectedDate.HasValue==false)
            {
                UIHelp.layerAlert(Page, "请选择锁定截止时间！", 6, 2000);
                return;
            }
            if (TextBoxApplyGetResult.Text.Trim()=="")
            {
                UIHelp.layerAlert(Page, "请填写锁定原因！", 6, 2000);
                return;
            }

            //获取Grid选中集合
            string applyidlist = GetRadGridSelect();
            string sql="";
            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            try
            {
                sql = string.Format(@"INSERT INTO [dbo].[CertificateLock_JZS]
           ([Id]
           ,[Fid]
           ,[LockStartTime]
           ,[LockContent]
           ,[LockPeople]
           ,[LockEndTime]
           ,[LX]
           ,[LockStates]
           ,[PSN_RegisterCertificateNo]
           ,[PSN_Name]
           ,[PSN_CertificateNO])
            select newid() as ID,[Fid],GETDATE(),'{0}','{1}','{2}','{3}','加锁',PSN_RegisterCertificateNo,PSN_Name,PSN_CertificateNO from [dbo].[View_SixPeople] where fid in ({4})", TextBoxApplyGetResult.Text.Trim(), UserName, RadDatePickerGetDateEnd.SelectedDate.Value, "二级建造师", applyidlist);
              CommonDAL.ExecSQL(tran, sql);
               
                sql = string.Format(@"INSERT INTO [dbo].[CertificateLock_His]
           ([HisId]
           ,[Fid]
           ,[LockStartTime]
           ,[LockContent]
           ,[LockPeople]
           ,[LockEndTime]
           ,[LX]
           ,[LockStates]
           ,[WriteDateTime]
           ,[PSN_RegisterCertificateNo]
           ,[PSN_Name]
           ,[Id]
           ,[PSN_CertificateNO])
            select newid() as ID,[Fid],LockStartTime,LockContent,LockPeople,LockEndTime,LX,LockStates,GETDATE(),PSN_RegisterCertificateNo,PSN_Name,Id,[PSN_CertificateNO] from [dbo].[CertificateLock_JZS] where fid in ({0})", applyidlist);
            
             CommonDAL.ExecSQL(tran, sql);
             tran.Commit();
            }
            catch (Exception ex)
            {

                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "批量加锁失败", ex);
                return;
                
            }
            ButtonSearch_Click(sender, e);
 
            UIHelp.layerAlert(Page, "批量加锁成功！", 6, 2000);
           
        }
        public string GetRadGridSelect()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i < RadGridQY.MasterTableView.Items.Count; i++)
            {
                CheckBox CheckBox1 = RadGridQY.Items[i].FindControl("CheckBox1") as CheckBox;
                if (CheckBox1.Checked)
                {
                    sb.Append(",'").Append(RadGridQY.MasterTableView.DataKeyValues[i]["Fid"].ToString()).Append("'");
                }
            }
            if (sb.Length > 0)
            {
                sb.Remove(0, 1);
            }
            return sb.ToString();
        }
        
    }
}