using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

public partial class menu : BasePage
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
            if (Request.IsAuthenticated)
            {
                BindMenu();

                if (RootUrl.ToLower().Contains("localhost") == true)
                {
                   ImageFace.ImageUrl = string.Format("http://localhost:7191/EXamManage/ExamSignimage.aspx?o={0}", Utility.Cryptography.Encrypt(string.Format("~/Upload/WorkerPhoto/{0}/{1}.jpg", WorkerCertificateCode.Substring(WorkerCertificateCode.Length - 3, 3), WorkerCertificateCode)));
                }
                else
                {
                    ImageFace.ImageUrl = string.Format("http://120.52.185.14/EXamManage/ExamSignimage.aspx?o={0}", Utility.Cryptography.Encrypt(string.Format("~/Upload/WorkerPhoto/{0}/{1}.jpg", WorkerCertificateCode.Substring(WorkerCertificateCode.Length - 3, 3), WorkerCertificateCode)));
                }
        
                divWorker.InnerText = PersonName;
            }
        }
    }

    //绑定菜单
    protected void BindMenu()
    {
        string xmldoc = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Menu><Group>
    <Item Text=""【 持证情况 】"" Expanded=""True"" Target=""mainFrame"" NavigateUrl=""./Student/BaseInfoEdit.aspx"" />
    <Item Text=""【 我的课程 】"" Expanded=""False"" Target=""mainFrame"" NavigateUrl=""./Student/WebClass.aspx?m=sc"" />
    <Item Text=""【 学习成果 】"" Expanded=""False"" Target=""mainFrame"" NavigateUrl=""./Student/FinishList.aspx"" />
</Group></Menu>";
        
        RadMenu1.LoadXml(xmldoc);
    }
}