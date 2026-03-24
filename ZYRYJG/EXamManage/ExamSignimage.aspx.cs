using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using Telerik.Web.UI;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace ZYRYJG.EXamManage
{
    public partial class ExamSignimage  : BasePage
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
            if (!IsPostBack)
            {
                Response.ClearContent();
                Response.ContentType = "image/Jpeg";
                string path = Utility.Cryptography.Decrypt(Request["o"]).Replace("..", "~");
                if (File.Exists(Server.MapPath(path)) == false) path = "~/Images/photo_ry.jpg";
                FileStream f = File.Open(Server.MapPath(path), FileMode.Open, FileAccess.Read, FileShare.Read);
                System.Drawing.Bitmap image = new System.Drawing.Bitmap(f);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                Response.BinaryWrite(ms.ToArray());
                f.Close();
                f.Dispose();
                image.Dispose();
            }
        }
      
    }
}
