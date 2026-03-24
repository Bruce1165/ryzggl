using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using Telerik.Web.UI;
using System.Data;
using System.IO;

namespace ZYRYJG.PersonnelFile
{
    public partial class PersonLock : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LabelLockPerson.Text = PersonName;
                LabelLockTime.Text = DateTime.Now.ToString("yyyy-MM-dd");

                WorkerOB ob = WorkerDAL.GetObject(Convert.ToInt64(Utility.Cryptography.Decrypt(Request["o"])));
                LabelCertificateCode.Text = ob.CertificateCode;
                LabelWorkerName.Text = ob.WorkerName;
            }
        } 
                
        //锁定
        protected void ButtonLock_Click(object sender, EventArgs e)
        {
            WorkerLockOB o = new WorkerLockOB();
            o.WorkerID = Convert.ToInt64(Utility.Cryptography.Decrypt(Request["o"]));
            o.LockEndTime = RadDatePickerLockEndTime.SelectedDate;
            o.LockPerson = PersonName;
            o.LockTime = DateTime.Now;
            o.LockType = "手动锁定";
            o.Remark = RadTextBoxRemark.Text;
            o.LockStatus = "加锁";
            try
            {
                WorkerLockDAL.Insert(o);
            }
            catch (Exception ex)
            {

                UIHelp.WriteErrorLog(Page, "锁定人员失败！", ex);
                return;
            }


            UIHelp.WriteOperateLog(PersonName, UserID, "人员加锁", string.Format("证件号码：{0}。", LabelCertificateCode.Text));
            Response.Redirect(string.Format("PersonInfoEdit.aspx?o={0}", Request["o"]));
        }

        //取消
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("PersonInfoEdit.aspx?o={0}", Request["o"]));
        }
        
      
    }
}
