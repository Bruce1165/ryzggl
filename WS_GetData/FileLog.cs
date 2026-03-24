using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Web;

namespace WS_GetData
{
    public class FileLog : System.Web.UI.Page
    {
        static FileLog()
        {
            log4net.Config.DOMConfigurator.Configure();
        }

        /// <summary>
        /// 在文件中记录异常的日志
        /// </summary>
        /// <param name="description">描述</param>
        /// <param name="exception">异常信息</param>
        public static void WriteLog(string description, Exception exception)
        {
            log4net.ILog log = LogManager.GetLogger("LogException");
            log.Info("-----------------------------------------------------------"
                + "\r\n错误描述 : " + description
                + "\r\n异常信息 : " + exception + "\r\n"
                );
        }


        /// <summary>
        /// 在文件中记录普通日志
        /// </summary>
        /// <param name="description">描述</param>
        public static void WriteLog(string description)
        {
            log4net.ILog log = LogManager.GetLogger(description);
            log.Info("");
        }
    }
}
