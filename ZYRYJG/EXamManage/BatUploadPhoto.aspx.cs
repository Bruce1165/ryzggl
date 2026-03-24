using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using Telerik.Web.UI;
using System.Data;
using System.IO;

namespace ZYRYJG.EXamManage
{
    public partial class BatUploadPhoto: BasePage 
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
        }

        //照片上传(从临时目录拷贝照片目录)
        protected void ButtonUploadImg_Click(object sender, EventArgs e)
        {
            //!!!不能删除，用于提交!!!
            RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "rtn", "rtn();", true);
        }

        //批量上传照片后
        protected void RadAsyncUploadFacePhoto_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            if (e.IsValid)
            {
                string workerPhotoFolder = "~/UpLoad/WorkerPhoto/";//个人照片存放路径           
                string subPath = "";//照片分类目录（证件号码后3位）
                subPath = e.File.GetNameWithoutExtension();

                subPath = subPath.Substring(subPath.Length - 3, 3);//图片按证件号后3位分目录存储
                if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/WorkerPhoto/" + subPath))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/WorkerPhoto/" + subPath));
                workerPhotoFolder = Server.MapPath("~/UpLoad/WorkerPhoto/" + subPath + "/");
                e.File.SaveAs(Path.Combine(workerPhotoFolder, e.File.GetName()), true);
            }
        }
    }
}
