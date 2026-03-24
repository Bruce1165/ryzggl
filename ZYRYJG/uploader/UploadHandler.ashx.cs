using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;
using DataAccess;

namespace ZYRYJG.uploader
{
    /// <summary>
    /// UploadHandler 的摘要说明
    /// </summary>
    public class UploadHandler : IHttpHandler
    {
        ///// <summary>
        ///// 上传文件保存目录
        ///// </summary>
        //protected string UploadDir
        //{
        //     get { return (string.IsNullOrEmpty(HttpContext.Current.Request["o"]) == true) ? "" : HttpContext.Current.Request["o"]; }
        //}

        ///// <summary>
        ///// 申请单ID
        ///// </summary>
        //protected string ApplyID
        //{
        //    get { return (string.IsNullOrEmpty(HttpContext.Current.Request["a"]) == true) ? "" : HttpContext.Current.Request["a"]; }
        //}

        ///// <summary>
        ///// 上传文件业务类型
        ///// </summary>
        //protected string FileBusType
        //{
        //    get { return (string.IsNullOrEmpty(HttpContext.Current.Request["t"]) == true) ? "" : HttpContext.Current.Request["t"]; }
        //}

        
        ///// <summary>
        /////  用户名称
        ///// </summary>
        //public string UserName
        //{
        //    get
        //    {
        //        if (System.Web.HttpContext.Current.Session["userInfo"] == null)
        //        {
        //            return "";
        //        }
        //        return Utility.Cryptography.Decrypt(System.Web.HttpContext.Current.Session["userInfo"].ToString()).Split(',')[1];
        //    }
        //}


        /// <summary>
        ///  用户名称
        /// </summary>
        public string Get_UserName(HttpContext context)
        {

            if (context.Session==null||context.Session["userInfo"] == null)
                {
                    return "";
                }
            return Utility.Cryptography.Decrypt(context.Session["userInfo"].ToString()).Split(',')[1];
         
        }

        public void ProcessRequest(HttpContext context)
        {
            switch (context.Request["a"])
            {
                case "0"://删除
                    string fileUrl = context.Request["o"];
                    //Utility.FileLog.WriteLog(fileUrl, null);
                    try
                    {
                        System.IO.File.Delete(context.Server.MapPath(fileUrl));
                        DeleteFile("~"+ fileUrl.Substring(2));
                        HttpContext.Current.Response.Write("true");
                    }
                    catch (Exception ex)
                    {
                        Utility.FileLog.WriteLog(string.Format("删除附件“{0}”失败",fileUrl), ex);
                        HttpContext.Current.Response.Write(ex.Message);
                    }
                    break;
                case "1"://上传
                    context.Response.ContentType = "text/plain";
                    context.Response.ContentEncoding = System.Text.Encoding.UTF8;
                    if (context.Request["REQUEST_METHOD"] == "OPTIONS")
                    {
                        context.Response.End();
                    }
                    SaveFile(context.Request["o"], context.Request["i"], context.Request["t"], context.Request["s"],Get_UserName(context));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="dir">目录（附件位置在/Upload/下任意目录名字，例如传入参数“zj”表示附件将存放在/Upload/zj/目录下）</param>
        /// <param name="ApplyID">申请单ID（可选）</param>
        /// <param name="FileBusType">文件业务类型（可选）</param>
        /// <param name="ShowName">文件显示名称,不传记录客户端选择文件名称</param>
        private void SaveFile(string dir, string ApplyID, string FileBusType, string ShowName, string UserName)
        {
            string filePath = string.Format("..{0}", dir);//文件逻辑存储位置
            string basePath = HttpContext.Current.Server.MapPath(filePath);//绝对目录           

            string name;//文件逻辑名

            string fileGuid = (string.IsNullOrEmpty(ShowName)==true?Guid.NewGuid().ToString():ShowName);//文件转存GUID名称
            
            HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            if (!System.IO.Directory.Exists(basePath))
            {
                System.IO.Directory.CreateDirectory(basePath);
            }

            var allowedExtensions = new[] {"jpg"};

            var suffix = System.IO.Path.GetExtension(files[0].FileName).ToLower().Replace(".",""); //获取文件格式  
            if (suffix=="")
            {
                Utility.FileLog.WriteLog("文件上传失败，扩展名为空！");
                HttpContext.Current.Response.Write("false");
                return;
            }
            if (allowedExtensions.Contains(suffix) ==false)
            {
                Utility.FileLog.WriteLog("文件上传失败，格式不在允许范围！");
                HttpContext.Current.Response.Write("false");
                return;
            }

          
            
           

            var _temp = System.Web.HttpContext.Current.Request["name"];//客户端文件名称

            //如果不修改文件名，则创建随机文件名  
            if (!string.IsNullOrEmpty(_temp))
            {
                name = _temp;
            }
            else
            {
                Random rand = new Random(24 * (int)DateTime.Now.Ticks);
                name = string.Format("{0}{1}", Guid.NewGuid(), suffix);
            }

            //文件保存  
            var full = string.Format("{0}{1}.{2}", basePath,fileGuid, suffix); //文件绝对路径
            filePath = string.Format("{0}{1}.{2}", filePath, fileGuid, suffix);//文件逻辑路径
            files[0].SaveAs(full);


            //if (UIHelp.IsWebShell(files[0].InputStream) == true)
            //{
            //    Utility.FileLog.WriteLog("文件上传内容存在webshell命令，已被禁止。");
            //    System.IO.File.Delete(full);               
            //    HttpContext.Current.Response.Write("false");
            //    return;
            //}

            var _result = "{\"jsonrpc\" : \"2.0\", \"result\" : null, \"id\" : \"" + name + "\"}";
            HttpContext.Current.Response.Write(_result);


            //文件属性信息写入数据库
            string fileID = Guid.NewGuid().ToString();

            try
            {
                FileInfoMDL f = new FileInfoMDL();
                f.FileID = fileID;
                f.FileName = (string.IsNullOrEmpty(ShowName)==true)?name:ShowName;
                f.FileSize = files[0].ContentLength;
                f.DataType = FileBusType;
                f.FileType = suffix;
                f.FileUrl = string.Format("~{0}", filePath.Substring(2));
                f.UploadMan = UserName;
                f.AddTime = DateTime.Now;
                FileInfoDAL.Insert(f);

            }
            catch (Exception ex)
            {
                Utility.FileLog.WriteLog("文件上传失败！", ex);
                return;
            }

           

            //记录附件申请单
            if (string.IsNullOrEmpty(ApplyID) == false)
            {
                if (string.IsNullOrEmpty(ShowName) == false)//指定文件名称
                {
                    //删除已存在的
                    string sql = "delete from ApplyFile where ApplyID='{0}' and FileID in(select FileID from FileInfo where FileName like '{1}')";
                    CommonDAL.ExecSQL(string.Format(sql, ApplyID, ShowName));
                }

                ApplyFileMDL _ApplyFileMDL = new ApplyFileMDL();
                _ApplyFileMDL.ApplyID = ApplyID;
                _ApplyFileMDL.FileID = fileID;
                _ApplyFileMDL.CheckResult = 0;
                ApplyFileDAL.Insert(_ApplyFileMDL);
            }
        }

        private void DeleteFile(string fileUrl)
        {
            try
            {
                string sql = @"delete from [dbo].[ApplyFile] where [FileID] = (  select [FileID]  FROM [dbo].[FileInfo] where [FileUrl]='{0}' );
                           delete FROM [dbo].[FileInfo] where [FileUrl]='{0}';";
                CommonDAL.ExecSQL(string.Format(sql, fileUrl));
            }
            catch { }
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