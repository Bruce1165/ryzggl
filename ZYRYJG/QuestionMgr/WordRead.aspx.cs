using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace ZYRYJG.QuestionMgr
{
    public partial class WordRead : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(Request["o"])==true)
            {
                return;
            }

            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                client.Encoding = System.Text.Encoding.UTF8;
                byte[] bytes = client.DownloadData(Request["o"]);
                Response.BinaryWrite(bytes);
            }

            Response.End();
            
        }
    }
}