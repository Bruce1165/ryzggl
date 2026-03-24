using log4net;
using System;

namespace Utility
{
    public class FileLog : System.Web.UI.Page
    {
        static FileLog()
        {
            log4net.Config.XmlConfigurator.Configure();
        }
        /// <summary>
        /// 在文件中记录异常的日志
        /// </summary>
        /// <param name="description">描述</param>
        public static void WriteLog(string description)
        {
            log4net.ILog log = LogManager.GetLogger("LogException");
            log.Info("-----------------------------------------------------------"
                     + "\r\n错误描述 : " + description + "\r\n"
                );
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
        /// 在文件中记录访问日志
        /// </summary>
        /// <param name="visitTime">访问时间</param>
        /// <param name="visitor">访问者</param>
        /// <param name="sysName">调用模块</param>
        /// <param name="funName">调用方法</param>
        /// <param name="paramName">调用参数</param>
        /// <param name="description">详细描述</param>
        public static void WriteVisitLog(DateTime visitTime, string visitor, string sysName, string funName, string paramName, string description)
        {
            //<appender name="VisitAppender" type="log4net.Appender.RollingFileAppender">
            //  <!--错误日志存放路径-->
            //  <param name="File" value="d:\\log\\VisiteLog\\FileShare\\"/>
            //  <param name="AppendToFile" value="true"/>
            //  <param name="MaxSizeRollBackups" value="10"/>
            //  <param name="DatePattern" value="yyyy-MM-dd&quot;.log&quot;"/>
            //  <param name="RollingStyle" value="Date"/>
            //  <param name="StaticLogFileName" value="false"/>
            //  <layout type="log4net.Layout.PatternLayout">
            //    <param name="ConversionPattern" value="%m%n"/>
            //  </layout>
            //  <filter type="log4net.Filter.LevelRangeFilter">
            //    <param name="LevelMin" value="WARN" />
            //    <param name="LevelMax" value="WARN" />
            //  </filter>
            //</appender>

            log4net.ILog log = LogManager.GetLogger("VisitAppender");

            //访问时间，访问人，访问模块，访问方法，访问参数，访问描述
            log.Warn(string.Format("{0} {1} {2} {3} {4} {5}", visitTime.ToString("yyyy-MM-dd HH:mm:ss"), visitor, sysName, funName, paramName, description));
        }
    }
}