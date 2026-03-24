using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace ZYRYJG.Ajax
{
    /// <summary>
    /// MainNews 的摘要说明,注意：ajax访问session需要继承System.Web.SessionState.IRequiresSessionState
    /// </summary>
    public class MainNews : BasePage,IHttpHandler,System.Web.SessionState.IRequiresSessionState
    {

        public new void ProcessRequest(HttpContext context)
        {
          //HttpContext.Current.Session
            List<ApplyNewsMDL> _ListApplyNewsMDL = ApplyNewsDAL.GetList(ZZJGDM);
            if(_ListApplyNewsMDL.Count>0)
            { 
                JavaScriptSerializer ser = new JavaScriptSerializer();
                string josn = ser.Serialize(_ListApplyNewsMDL);
                context.Response.Write(josn);
            }
            else
            {
                context.Response.Write("False");
            }
          
        }
        public new  bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}