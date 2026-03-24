using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Model;
using DataAccess;
using System.Text;
using System.IO;
using Telerik.Web.UI;

namespace ZYRYJG.County
{
    public partial class PublicityModifyImg : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "County/PublicityLook.aspx";
            }
        }
        //业务员上报
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                var files = Directory.GetFiles(Server.MapPath(string.Format("../Upload/shenshu/{0}/", Request["o"])), "*.jpg");

                 string radom = Utility.Cryptography.Encrypt(string.Format("{0},{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "synergyFileAccess"));

                System.Text.StringBuilder sb = new StringBuilder();
                foreach (var f in files)
                {
                    sb.Append(string.Format("<div><img src=\"../Upload/shenshu/{0}/{1}?read={2}\" /></div>"
                        , Request["o"]
                        , f.Substring(f.LastIndexOf('\\') + 1)
                        , radom
                        ));
                    content.InnerHtml = sb.ToString();
                }
                
            }
        }    
    }
}