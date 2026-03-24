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
    public class zjsQualification : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
                    zjs_QualificationMDL _zjs_QualificationMDL = zjs_QualificationDAL.GetObject(context.Request["dzgzsbh"]);
                    if(_zjs_QualificationMDL!=null)
                    {
                        JavaScriptSerializer ser = new JavaScriptSerializer();
                        string josn = ser.Serialize(_zjs_QualificationMDL);
                        context.Response.Write(josn);
                    }
                    else
                    {
                        context.Response.Write("False");
                    }
                    break;
                case "selectCert":
                    zjs_CertificateMDL _zjs_CertificateMDL = zjs_CertificateDAL.GetObjectByZGZSBH(context.Request["dzgzsbh"]);
                    if (_zjs_CertificateMDL != null)
                    {
                        JavaScriptSerializer ser = new JavaScriptSerializer();
                        string josn = ser.Serialize(_zjs_CertificateMDL);
                        context.Response.Write(josn);
                     }
                    else
                    {
                        context.Response.Write("False");
                    }
                    break;

                    
                case "update":
                    int year = 0;
                    bool checkyear=int.TryParse(context.Request["QDNF"],out year);
                    if(checkyear==false || year<1950 || year> (DateTime.Now.Year +1))
                    {
                        context.Response.Write("取得年份格式有误！");
                        break;
                    }
                    if(context.Request["ZJHM"].Length==18)
                    {
                        if(Utility.Check.isChinaIDCard(context.Request["ZJHM"])==false)
                        {
                            context.Response.Write("证件号码格式有误！");
                            break;
                        }
                    }
                    if (context.Request["ZJHM"].Length >18)
                    {
                        context.Response.Write("证件号码不能大于18位！");
                        break;
                    }
                    zjs_QualificationMDL _UpDATEzjs_QualificationMDL = zjs_QualificationDAL.GetObject(context.Request["dzgzsbh"]);
                    _UpDATEzjs_QualificationMDL.XM = context.Request["XM"];
                    _UpDATEzjs_QualificationMDL.ZJHM = context.Request["ZJHM"];
                    _UpDATEzjs_QualificationMDL.GZDW = context.Request["GZDW"];
                    _UpDATEzjs_QualificationMDL.ZGZSBH = context.Request["ZGZSBH"];
                    _UpDATEzjs_QualificationMDL.GLH =context.Request["ZGZSBH"];
                    _UpDATEzjs_QualificationMDL.QDNF = context.Request["QDNF"];
                    _UpDATEzjs_QualificationMDL.ZYLB = context.Request["ZYLB"];
                    _UpDATEzjs_QualificationMDL.QFSJ = Convert.ToDateTime(context.Request["QFSJ"]);
                    _UpDATEzjs_QualificationMDL.BYXX = context.Request["BYXX"];
                    _UpDATEzjs_QualificationMDL.BYSJ = Convert.ToDateTime(context.Request["BYSJ"]);
                    _UpDATEzjs_QualificationMDL.SXZY = context.Request["SXZY"];
                    _UpDATEzjs_QualificationMDL.ZGXL = context.Request["ZGXL"];
                    _UpDATEzjs_QualificationMDL.Old_ZGZSBH = context.Request["dzgzsbh"];
                    int j = 0;
                    try
                    {
                        j = zjs_QualificationDAL.Update(_UpDATEzjs_QualificationMDL);
                    }
                    catch(Exception ex)
                    {
                        Utility.FileLog.WriteLog("修改二造资格证书失败", ex);
                    }
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
                    zjs_CertificateMDL _UpDATEzjs_CertificateMDL = zjs_CertificateDAL.GetObjectByZGZSBH(context.Request["dzgzsbh"]);
                    _UpDATEzjs_CertificateMDL.PSN_Name = context.Request["PSN_Name"];
                    _UpDATEzjs_CertificateMDL.PSN_Sex = context.Request["PSN_Sex"];
                    _UpDATEzjs_CertificateMDL.ZGZSBH = context.Request["ZGZSBH"];
                    int g = zjs_CertificateDAL.Update(_UpDATEzjs_CertificateMDL);
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
                    int i = zjs_QualificationDAL.Delete(context.Request["dzgzsbh"]);
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