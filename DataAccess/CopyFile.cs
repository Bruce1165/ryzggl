using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace DataAccess
{
    public class CopyFile
    {
        ///// <summary>
        ///// 获取DOS命令
        ///// 参考命令行：copy \\192.168.5.131\d$\DBBack\Subway_backup_201210090200.bak E:\DBBack
        ///// </summary>
        ///// <param name="fileName"></param>
        ///// <returns></returns>
        //public static string GetComand(string fileName)
        //{
        //    string strCommand = "";
        //    const string sourceDir = "\\\\192.168.5.80\\E$\\Exam\\UpLoad\\CertifEnterApply\\";
        //    const string targetDir = "E:\\Exam\\UpLoad\\CertifEnterApply";
        //    if (fileName != null)
        //    {
        //        // 考虑备份时间的不确定性，增加了*通配符
        //        strCommand = string.Format("Copy /Y {0}{1}.csv {2}", sourceDir, fileName, targetDir);
        //    }
        //    return strCommand;
        //}


        /// <summary>
        /// 执行DOS命令
        /// </summary>
        /// <param name="strCommand"></param> 
        public static void CopyDBFile(string strCommand)
        {
            Process p = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();

            try
            {
                startInfo.FileName = String.Format("{0}\\cmd.exe ", Environment.GetFolderPath(Environment.SpecialFolder.System));

                startInfo.Arguments = "/C " + strCommand; // 设定参数，其中的“/C”表示执行完命令后马上退出 
                startInfo.UseShellExecute = false;        // 不使用系统外壳程序启动 
                startInfo.RedirectStandardInput = false;  // 不重定向输入 
                startInfo.RedirectStandardOutput = true;  // 重定向输出 
                startInfo.RedirectStandardError = true;
                startInfo.CreateNoWindow = true;          // 不创建窗口 

                p.StartInfo = startInfo;

                string strFrom = DateTime.Now.ToString("HH:mm:ss");
                p.Start();
                string output = p.StandardOutput.ReadToEnd();
                if (!String.IsNullOrEmpty(output) && output.LastIndexOf("\r\n") > 0)
                {
                    output.Substring(0, output.LastIndexOf("\r\n"));
                }
                p.WaitForExit();
                string strEnd = DateTime.Now.ToString("HH:mm:ss");

                //CUtility.WriteLog(String.Format("{0} - {1} {2} {3}", strFrom, strEnd, strCommand, output));
                
            }
            catch (Exception ex)
            {
                Utility.FileLog.WriteLog("导出证书查询信息失败！", ex);
                //CUtility.WriteLog(strCommand, ex.Message);
            }
            finally
            {
                p.Close();
            }
        }
    }
}
