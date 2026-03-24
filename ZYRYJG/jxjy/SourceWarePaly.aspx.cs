using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;

namespace ZYRYJG.jxjy
{    
    public partial class SourceWarePaly : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "SourceMgr.aspx";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                  SourceMDL _SourceMDL = SourceDAL.GetObject(Convert.ToInt64(Utility.Cryptography.Decrypt(Request["o"])));
                  if (_SourceMDL != null)
                  {
                      ScriptManager.RegisterStartupScript(Page,this.GetType(),"play"
                          , string.Format("IfrmView.src = urlRandom('{0}?nickname={1}&uid={2}&k={3}');kjid='{4}';", _SourceMDL.SourceWareUrl, Server.UrlEncode(PersonName), uid(), getPlayKey(), Request["o"])
                          ,true);
                      
                  }
            }
        }

        protected string uid()
        {
            return "1" + "0".PadLeft(9, '0');
        }

        protected string getPlayKey()
        {
            return Server.UrlEncode(Utility.Cryptography.Encrypt(string.Format("{0},{1},{2}"
                , DateTime.Now.ToString("yyyy-MM-ddHH:mm:ss")//学习时间
                , -1//课件ID
                , -1//学习人证件
                 )));
        }
    }
}