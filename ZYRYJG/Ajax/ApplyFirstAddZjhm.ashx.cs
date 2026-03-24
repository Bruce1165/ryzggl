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
    /// ApplyFirstAddZjhm 的摘要说明
    /// </summary>
    public class ApplyFirstAddZjhm : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            List<QualificationMDL> _QualificationMDL = QualificationDAL.GetObjectList(context.Request["zjhm"]);
            if (_QualificationMDL.Count>0)
            {
                JavaScriptSerializer ser = new JavaScriptSerializer();
                string josn = ser.Serialize(_QualificationMDL);
                context.Response.Write(josn);
            }
            else
            {
                context.Response.Write("False");
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}