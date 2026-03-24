using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;
using System.Data;

namespace ZYRYJG.StAnManage
{
    public partial class MissExamDetail : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "MissExamLock.aspx";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {               

                TJ_MissExamLockMDL o = TJ_MissExamLockDAL.GetObject(Utility.Cryptography.Decrypt(Request["o1"]),Convert.ToDateTime(Utility.Cryptography.Decrypt(Request["o2"])));
                if (o != null)
                {
                    //绑定注册信息
                    UIHelp.SetData(EditTable, o, true);                   

                    //绑定注册历史
                    string sql = @"select * from [dbo].[VIEW_EXAMSCORE_NEW]
                                    where [WorkerCERTIFICATECODE] = '{0}' and EXAMSTARTDATE between '{1}' and '{2}'
                                    order by EXAMSTARTDATE desc";

                    DataTable dt = CommonDAL.GetDataTable(string.Format(sql, o.WorkerCertificateCode,o.FirstExamDate,o.LockStartDate));
                    
                    RadGridExam.DataSource = dt;
                    RadGridExam.DataBind();
                }

                if (IfExistRoleID("1") == true || IfExistRoleID("16") == true)
                {
                    ButtonSave.Visible = true;
                    UIHelp.SetReadOnly(RadDatePickerLockEndDate, false);
                    ViewState["TJ_MissExamLockMDL"]=o;
                }
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            TJ_MissExamLockMDL o = ViewState["TJ_MissExamLockMDL"] as TJ_MissExamLockMDL;

            try
            {
                o.LockEndDate = RadDatePickerLockEndDate.SelectedDate;
                TJ_MissExamLockDAL.Update(o);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "修改缺考锁定截止日期失败！", ex);
                return;
            }
            UIHelp.layerAlert(Page, "保存成功！",6,2000);
        }
    }
}