using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace ZYRYJG.uploader
{
    public partial class Upload : System.Web.UI.Page
    {
        /// <summary>
        /// 上传文件保存目录
        /// </summary>
        protected string UploadDir
        {
            get { return (string.IsNullOrEmpty(Request["o"]) == true) ? "/Upload/other/" : string.Format("/UpLoad/{0}/",Request["o"]); }
        }

        /// <summary>
        /// 是否可以修改删除已上传文件
        /// </summary>
        protected bool EnableEditUploadedFile
        {
            get { return (string.IsNullOrEmpty(Request["v"]) == false && Request["v"]=="1") ?true : false; }
        }

        /// <summary>
        /// 申请单ID
        /// </summary>
        protected string ApplyID
        {
            get { return (string.IsNullOrEmpty(Request["a"]) == true) ? "" :  Request["a"]; }
        }

        /// <summary>
        /// 上传文件业务类型
        /// </summary>
        protected string FileBusType
        {
            get { return (string.IsNullOrEmpty(Request["t"]) == true) ? "" : Request["t"]; }
        }

        /// <summary>
        /// 上传文件显示名称
        /// </summary>
        protected string FileShowName
        {
            get { return (string.IsNullOrEmpty(Request["s"]) == true) ? "" : Request["s"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request["o"]) == false)//o为文件夹位置
                {
                    if (!Directory.Exists(Page.Server.MapPath(string.Format("~/UpLoad/{0}/", Request["o"]))))//创建目录
                    {
                        System.IO.Directory.CreateDirectory(Page.Server.MapPath(string.Format("~/UpLoad/{0}/", Request["o"])));
                    }
                    else if (EnableEditUploadedFile==true)
                    {

                        //展示目录下文件
                        DirectoryInfo TheFolder = new DirectoryInfo(Server.MapPath(string.Format("~/Upload/{0}/", Request["o"])));
                        FileInfo[] fi = TheFolder.GetFiles("*.jpg");
                        if (fi.Length > 0)
                        {
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            foreach (FileInfo f in fi)
                            {
                                sb.Append(string.Format(@"<li id='{1}'>                   
                                                            <p class=""imgWrap""><img src='../Upload/{2}/{0}' ondblclick='lookimg(""../Upload/{2}/{0}"")'></p>
                                                            <p class=""title"">{0}</p>
<div class=""file-panel"" style=""height: 0px;""><span class=""cancel"" onclick=""delfile('{1}','../Upload/{2}/{0}')"">删除</span></div>
                                                          </li>", f.Name,f.Name.Replace(".", "_"), Request["o"]));
                            }

                            divUploaded.InnerHtml = string.Format("<ul  class=\"filelist\">{0}</ul>", sb.ToString());
                        }
                    }
                }
            }
        }
    }
}