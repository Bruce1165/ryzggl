using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;

namespace ZYRYJG.PersonnelFile
{
    public partial class GetFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                //q[0]=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), q[1]="synergyFileAccess", q[2]=CertificateCAID, q[3]="pdf|ofd"
                string[] q =  Utility.Cryptography.Decrypt(Request["o"]).Split(',');

                Response.Clear();
                if (Convert.ToDateTime(q[0]).AddMinutes(600) < DateTime.Now)
                {
                    Response.Write("访问超时");
                    return;
                }

                if (q.Length < 4 || q[1] != "synergyFileAccess")
                {
                    Response.Write("非法访问");                 
                    return;
                }

                string certUrl = string.Format("{0}/{1}/{2}.{3}", MyWebConfig.CAFile, q[2].Substring(q[2].Length - 3, 3), q[2], q[3]);

                 byte[] file = null;
                 file = Utility.ImageHelp.FileToByte(certUrl);

                 
                 Response.Write(Convert.ToBase64String(file));
                 Response.End();

            }
        }
    }
}