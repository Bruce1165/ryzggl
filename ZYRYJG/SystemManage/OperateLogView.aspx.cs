using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using Telerik.Web.UI;

namespace ZYRYJG.SystemManage
{
    public partial class OperateLogView : BasePage
    {
        protected override bool IsNeedLogin
        {
            get
            {
                return false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {
                OperateLogOB ob = OperateLogDAL.GetObject(Convert.ToInt64(Request["o"]));

                LabelLogTime.Text = ob.LogTime.Value.ToString("yyyy年MM月dd日 HH时mm分ss秒");
                LabelPersonName.Text = ob.PersonName;
                LabelOperateName.Text = ob.OperateName;               
                P_OperateName.InnerText = ob.LogDetail;//详细               
            }
        }
       
    }
}
