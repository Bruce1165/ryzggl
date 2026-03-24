using System;
//using WS_GetData.Common;

namespace WS_GetData.Api
{
    /// <summary>
    ///     错误日志
    /// </summary>
    public static class Log
    {
        /// <summary>
        ///     制证错误日志
        /// </summary>
        /// <param name="id"></param>
        /// <param name="exp"></param>
        public static void CreateLog(string id, Exception exp)
        {
            FileLog.WriteLog("调用北京市电子证照系统【制证】接口失败！错误：" + exp.Message + ",主键为：" + id + "。", exp);
        }

        /// <summary>
        ///     签发错误日志
        /// </summary>
        /// <param name="id"></param>
        /// <param name="exp"></param>
        public static void SignLog(string id, Exception exp)
        {
            FileLog.WriteLog("调用北京市电子证照系统【签发】接口失败！错误：" + exp.Message + ",主键为：" + id + "。", exp);
        }

        /// <summary>
        ///     解析提取错误日志
        /// </summary>
        /// <param name="id"></param>
        /// <param name="exp"></param>
        public static void AnalysisExtracLog(string id, Exception exp)
        {
            FileLog.WriteLog("解析提取Pdf文件失败！错误：" + exp.Message + "，主键为：" + id + "。", exp);
        }

        /// <summary>
        ///     读取文件错误日志
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filePath"></param>
        /// <param name="exp"></param>
        public static void ReadFileLog(string id, string filePath, Exception exp)
        {
            FileLog.WriteLog("读取文件失败！错误：" + exp.Message + "，主键为：" + id + ",文件路径为：" + filePath + "。", exp);
        }
    }
}