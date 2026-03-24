using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;

namespace ZYRYJG.QuestionMgr
{
    public partial class WordWrite : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Files.Count > 0)
            {
                //HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
                //files[0].SaveAs(Server.MapPath(string.Format("{0}.doc", DateTime.Now.ToString("yyMMddHHmmss"))));               
            }            
        }
    }
}