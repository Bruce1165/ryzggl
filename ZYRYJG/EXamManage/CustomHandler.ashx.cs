using System;
using System.Web;
using Telerik.Web.UI;

namespace ZYRYJG.EXamManage
{
    public class CustomHandler : AsyncUploadHandler 
    {
        protected override IAsyncUploadResult Process(UploadedFile file, HttpContext context, IAsyncUploadConfiguration configuration, string tempFileName)
        {
            configuration.TimeToLive = TimeSpan.FromHours(4);
            return base.Process(file, context, configuration, tempFileName);
        }
    }
}
