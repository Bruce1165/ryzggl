using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using ZYRYJG.Thehall;
using System.Text;

namespace ZYRYJG
{
    
    public partial class LoginJava : System.Web.UI.Page
    {

        //个人登录
        protected void ButtonWorker_Click(object sender, EventArgs e)
        {

            //WorkerOB _WorkerOB = WorkerDAL.GetUserByName(RadTextBoxCID.Text.Trim());


            //if (_WorkerOB == null)
            //{
            //    UIHelp.layerAlert(Page, "没有查到你的个人注册信息，请确认是否已经在建委官方网站注册个人用户！");
            //    return;

            //}

            //string loginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//登录时间

            ////0)用户ID,1)用户名称,2)用户所属区县,3)用户角ID色集合用“|”分割,4)用户所属机构ID,5)用户所属部门ID,6)机构名称,7)机构代码，8）用户最后登录时间         
            //string userInfo = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", _WorkerOB.WorkerID, _WorkerOB.WorkerName, "", "0", "", "", "", _WorkerOB.CertificateCode, loginTime);


            ////用户类型：1超级管理员；  2考生； 3企业； 4培训点；5外阜企业 ； 6行政管理机构
            //string _personType = "2";
            ////Utility.CacheHelp.AddAbsoluteeExpirationCache(Page, string.Format("{0}user{1}", _personType, _WorkerOB.WorkerID), loginTime, 10);
            ////Session[string.Format("{0}user{1}", _personType, _WorkerOB.WorkerID)] = loginTime;

            //userInfo = Cryptography.Encrypt(userInfo);
            //FormsAuthentication.SetAuthCookie(userInfo, false);
            //Session["userInfo"] = userInfo;
            //Response.Redirect("~/Default.aspx", false);
        }
    }


}