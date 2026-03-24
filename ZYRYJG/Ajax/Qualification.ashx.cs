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
    /// Qualification 的摘要说明
    /// </summary>
    public class Qualification : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            //看是什么方法进来
            string action = context.Request["action"];
            switch (action)
            {
                case "select":
                    QualificationMDL _QualificationMDL = QualificationDAL.GetObject(context.Request["dzgzsbh"]);
                    if(_QualificationMDL!=null)
                    {
                        JavaScriptSerializer ser = new JavaScriptSerializer();
                        string josn = ser.Serialize(_QualificationMDL);
                        context.Response.Write(josn);
                    }
                    else
                    {
                        context.Response.Write("False");
                    }
                    break;
                case "selectCert":
                    COC_TOW_Person_BaseInfoMDL _COC_TOW_Person_BaseInfoMDL = COC_TOW_Person_BaseInfoDAL.GetObjectByPSN_CertificateNO(context.Request["zjhm"],"二级");
                    if (_COC_TOW_Person_BaseInfoMDL != null)
                    {
                        JavaScriptSerializer ser = new JavaScriptSerializer();
                        string josn = ser.Serialize(_COC_TOW_Person_BaseInfoMDL);
                        context.Response.Write(josn);
                     }
                    else
                    {
                        context.Response.Write("False");
                    }
                    break;

                    
                case "update":
                    QualificationMDL _UpDATEQualificationMDL = QualificationDAL.GetObject(context.Request["dzgzsbh"]);
                    _UpDATEQualificationMDL.XM = context.Request["XM"];
                    _UpDATEQualificationMDL.ZJHM = context.Request["ZJHM"];
                    _UpDATEQualificationMDL.GZDW = context.Request["GZDW"];
                    _UpDATEQualificationMDL.ZGZSBH = context.Request["dzgzsbh"];
                    _UpDATEQualificationMDL.GLH =context.Request["ZGZSBH"];
                    _UpDATEQualificationMDL.QDNF = context.Request["QDNF"];
                    _UpDATEQualificationMDL.ZYLB = context.Request["ZYLB"];
                    _UpDATEQualificationMDL.QFSJ = Convert.ToDateTime(context.Request["QFSJ"]);
                    _UpDATEQualificationMDL.BYXX = context.Request["BYXX"];
                    _UpDATEQualificationMDL.BYSJ = Convert.ToDateTime(context.Request["BYSJ"]);
                    _UpDATEQualificationMDL.SXZY = context.Request["SXZY"];
                    _UpDATEQualificationMDL.ZGXL = context.Request["ZGXL"];
                    int j = QualificationDAL.Update(_UpDATEQualificationMDL);
                    if (j > 0)
                    {
                        context.Response.Write("True");
                    }
                    else
                    {
                        context.Response.Write("False");
                    }
                    break;
                case "updateCert":
                    COC_TOW_Person_BaseInfoMDL _UpDATECOC_TOW_Person_BaseInfoMDL = COC_TOW_Person_BaseInfoDAL.GetObjectByPSN_CertificateNO(context.Request["zjhm"],"二级");
                    _UpDATECOC_TOW_Person_BaseInfoMDL.PSN_Name = context.Request["PSN_Name"];
                    //_UpDATECOC_TOW_Person_BaseInfoMDL.PSN_CertificateNO = context.Request["PSN_CertificateNO"];
                    //_UpDATEQualificationMDL.GZDW = context.Request["GZDW"];
                    //_UpDATECOC_TOW_Person_BaseInfoMDL.QFSJ = Convert.ToDateTime(context.Request["QFSJ"]);
                    _UpDATECOC_TOW_Person_BaseInfoMDL.PSN_Sex = context.Request["PSN_Sex"];
                    _UpDATECOC_TOW_Person_BaseInfoMDL.ZGZSBH = context.Request["ZGZSBH"];
                    //_UpDATEQualificationMDL.GLH = context.Request["ZGZSBH"];
                    int g = COC_TOW_Person_BaseInfoDAL.Update(_UpDATECOC_TOW_Person_BaseInfoMDL);
                    if (g > 0)
                    {
                        context.Response.Write("True");
                    }
                    else
                    {
                        context.Response.Write("False");
                    }
                    break;
                case "delete":
                    int i = QualificationDAL.Delete(context.Request["dzgzsbh"]);
                    if (i > 0)
                    {
                        context.Response.Write("True");
                    }
                    else
                    {
                        context.Response.Write("False");
                    }
                    break;
                default:
                    break;

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