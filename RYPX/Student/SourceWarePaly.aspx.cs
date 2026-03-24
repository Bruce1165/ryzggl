using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;


public partial class Student_SourceWarePaly : BasePage
{
    protected override string CheckVisiteRgihtUrl
    {
        get
        {
            return "jxjy/MyTrain.aspx";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SourceMDL _SourceMDL = SourceDAL.GetObject(Convert.ToInt64(Utility.Cryptography.Decrypt(Request["o"])));
            if (_SourceMDL != null)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "play"
                    , string.Format("IfrmView.src = urlRandom('{0}?nickname={1}&uid={2}&k={3}');kjid='{4}';", _SourceMDL.SourceWareUrl, Server.UrlEncode(PersonName), uid(), getPlayKey(_SourceMDL.SourceID.Value, WorkerCertificateCode), Request["o"])
                    , true);

            }
        }
    }

    protected string uid()
    {
        return "1" + PersonID.ToString().PadLeft(9, '0');
    }

    /// <summary>
    /// 视频播放认证
    /// </summary>
    /// <param name="sourceID">课件ID</param>
    /// <param name="zjhm">证件号码</param>
    /// <returns></returns>
    protected string getPlayKey(long sourceID, string zjhm)
    {
        //测试环境使用如下
        return Server.UrlEncode(Utility.Cryptography.Encrypt(string.Format("{0},{1},{2}"
               , DateTime.Now.ToString("yyyy-MM-ddHH:mm:ss")//学习时间
               , sourceID//课件ID
               , zjhm//学习人证件
                )));
    }

}