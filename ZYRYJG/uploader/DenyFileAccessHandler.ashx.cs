using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZYRYJG.uploader
{
    /// <summary>
    /// DenyFileAccessHandler ：拒绝匿名访问平台上传的文件(目录Upload中文件，需要配置web.config中handlers，添加DenyFileAccessHandler)
    /// </summary>
    public class DenyFileAccessHandler : IHttpHandler
    {
        //正常访问附件传参：http://....../xxxx.jpg?read=加密("时间,synergyFileAccess")
        public void ProcessRequest(HttpContext context)
        {
            if (string.IsNullOrEmpty(context.Request["read"])==true)
            {
                context.Response.Write("您没有访问权限!");
                return;
            }
            else
            {
                string[] q =Utility.Cryptography.Decrypt(context.Request["read"]).Split(',');
                if (Convert.ToDateTime(q[0]).AddMinutes(120) < DateTime.Now)
                {
                    context.Response.Write("访问超时!");
                    return;
                }

                if(q[1] !="synergyFileAccess")
                {
                    context.Response.Write("您没有访问权限!");
                    return;
                }

                switch (System.IO.Path.GetExtension(Utility.ImageHelp.GetUrlNoParam(context.Request.Path)).ToLower())
                {
                    case ".jpg":
                    case ".jpeg":
                        context.Response.ContentType = "image/jpeg";
                        break;
                    case ".png":
                        context.Response.ContentType = "image/png";
                        break;
                    case ".gif":
                        context.Response.ContentType = "image/gif";
                        break;
                    case ".tif":
                        context.Response.ContentType = "image/tiff";
                        break;                        	
                    case ".xls":
                    case ".xlsx":
                        context.Response.ContentType = "application/vnd.ms-excel";
                        break;
                    case ".doc":
                    case ".docx":
                        context.Response.ContentType = "application/msword";
                        break;
                     case ".zip":
                        context.Response.ContentType = "application/zip";
                        break;
                    case ".rar":
                        context.Response.ContentType = "application/x-zip-compressed";
                        break;
                    case ".htm":
                    case ".html":
                        context.Response.ContentType = "text/html";
                        break;
                     case ".pdf":
                        context.Response.ContentType = "application/pdf";
                        break;
                    case ".txt":
                        context.Response.ContentType = "text/xml";
                        break;                

                    default:
                        break;
                }
                context.Response.TransmitFile(Utility.ImageHelp.GetUrlNoParam( context.Request.Path));
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