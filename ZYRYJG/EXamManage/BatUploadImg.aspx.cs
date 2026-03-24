using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.IO;
using Model;
using DataAccess;

namespace ZYRYJG.EXamManage
{
    public partial class BatUploadImg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //照片上传
        protected void ButtonUploadImg_Click(object sender, EventArgs e)
        {
            if (RadAsyncUploadFacePhoto.UploadedFiles.Count > 0)
            {
                string targetFolder = Server.MapPath("~/Images/faceImg/");
                foreach (UploadedFile validFile in RadAsyncUploadFacePhoto.UploadedFiles)
                {
                    validFile.SaveAs(Path.Combine(targetFolder, validFile.GetName()), true);
                }
            }
        }
    }
}
