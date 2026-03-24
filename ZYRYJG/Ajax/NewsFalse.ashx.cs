using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;


namespace ZYRYJG.Ajax
{
    /// <summary>
    /// NewsFalse 的摘要说明
    /// </summary>
    public class NewsFalse : BasePage, IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public new void ProcessRequest(HttpContext context)
        {
            CommonDAL.ExecSQL(string.Format("UPDATE [ApplyNews] SET [SFCK]=1 WHERE [ENT_OrganizationsCode]='{0}'", ZZJGDM));
        }

        public new bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}