using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;

namespace ZYRYJG.County
{
    public partial class BusinessAccepted : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            ApplyMDL _ApplyMDL = new ApplyMDL();
            _ApplyMDL = ApplyDAL.GetObject(Request.QueryString["aid"]);
            _ApplyMDL.GetDateTime = DateTime.Now;
            _ApplyMDL.GetResult = RadioButtonListApplyStatus.Text == "通过" ? EnumManager.ApplyStatus.已受理 : EnumManager.ApplyStatus.已驳回;
            _ApplyMDL.GetRemark = RadTextBoxGetResult.Text;
            _ApplyMDL.ApplyStatus = RadioButtonListApplyStatus.Text == "通过" ? EnumManager.ApplyStatus.已受理 : EnumManager.ApplyStatus.已驳回;
            _ApplyMDL.GetMan = UserName;
            int i = ApplyDAL.Update(_ApplyMDL);
            if(i>0)
            {
                UIHelp.Alert(Page, "受理成功！");
            }
            ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
        }
    }
}