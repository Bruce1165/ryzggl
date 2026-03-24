using System;

namespace WS_GetData.Api
{
    /// <summary>
    ///     公用方法
    /// </summary>
    public static class Common
    {
        /// <summary>
        ///     ini中 system节
        /// </summary>
        public static class SystemSection
        {
            /// <summary>
            ///     system.ini 路径
            /// </summary>
            private static string SystemIniFilePath
            {
                get { return AppDomain.CurrentDomain.BaseDirectory + "system.ini"; }
            }

            /// <summary>
            ///     是否启动服务
            /// </summary>
            /// <returns></returns>
            public static bool IsUploadCertificate
            {
                get
                {
                    bool isFixedTime = false;
                    const string section = "system";
                    const string key = "IsUploadCertificate";

                    string value = IniHelper.Read(section, key, SystemIniFilePath);

                    bool result;
                    if (bool.TryParse(value, out result))
                    {
                        isFixedTime = result;
                    }
                    else
                    {
                        IniHelper.WritePrivateProfileString(section, key, "false", SystemIniFilePath);
                    }
                    return isFixedTime;
                }
            }

            /// <summary>
            ///     是否解析PDF
            /// </summary>
            public static bool IsAnalysisExtract
            {
                get
                {
                    bool isFixedTime = false;
                    const string section = "system";
                    const string key = "IsAnalysisExtract";

                    string value = IniHelper.Read(section, key, SystemIniFilePath);

                    bool result;
                    if (bool.TryParse(value, out result))
                    {
                        isFixedTime = result;
                    }
                    else
                    {
                        IniHelper.WritePrivateProfileString(section, key, "false", SystemIniFilePath);
                    }
                    return isFixedTime;
                }
            }

            /// <summary>
            ///     是否解析PDF中的二维码或照片
            /// </summary>
            public static bool IsAnalysisQRCode
            {
                get
                {
                    bool isFixedTime = false;
                    const string section = "system";
                    const string key = "IsAnalysisQRCode";

                    string value = IniHelper.Read(section, key, SystemIniFilePath);

                    bool result;
                    if (bool.TryParse(value, out result))
                    {
                        isFixedTime = result;
                    }
                    else
                    {
                        IniHelper.WritePrivateProfileString(section, key, "false", SystemIniFilePath);
                    }
                    return isFixedTime;
                }
            }

            /// <summary>
            ///     系统是否输出报文
            /// </summary>
            /// <returns></returns>
            public static bool IsPrintPacket()
            {
                bool isRun = false;
                const string section = "system";
                const string key = "printPacket";

                string value = IniHelper.Read(section, key, SystemIniFilePath);

                bool result;
                if (bool.TryParse(value, out result))
                {
                    isRun = result;
                }
                else
                {
                    IniHelper.WritePrivateProfileString(section, key, "false", SystemIniFilePath);
                }
                return isRun;
            }

            /// <summary>
            ///     系统是否运行中
            /// </summary>
            /// <returns></returns>
            public static bool IsRun()
            {
                bool isRun = false;
                const string section = "system";
                const string key = "isRun";

                string value = IniHelper.Read(section, key, SystemIniFilePath);

                bool result;
                if (bool.TryParse(value, out result))
                {
                    isRun = result;
                }
                else
                {
                    IniHelper.WritePrivateProfileString(section, key, "false", SystemIniFilePath);
                }
                return isRun;
            }

            /// <summary>
            ///     系统是否定时运行
            /// </summary>
            /// <returns></returns>
            private static bool IsFixedTime()
            {
                bool isFixedTime = false;
                const string section = "system";
                const string key = "isFixedTime";

                string value = IniHelper.Read(section, key, SystemIniFilePath);

                bool result;
                if (bool.TryParse(value, out result))
                {
                    isFixedTime = result;
                }
                else
                {
                    IniHelper.WritePrivateProfileString(section, key, "false", SystemIniFilePath);
                }
                return isFixedTime;
            }

            /// <summary>
            ///     设置运行状态
            /// </summary>
            public static void SetSystemRun(bool state)
            {
                const string section = "system";
                const string key = "isRun";

                IniHelper.WritePrivateProfileString(section, key, state ? "true" : "false", SystemIniFilePath);
            }

            /// <summary>
            ///     获取每次上报数据的条数
            /// </summary>
            /// <returns></returns>
            public static int GetEachReportQuantity()
            {
                int quantity = 10000;
                const string section = "system";
                const string key = "eachReportQuantity";

                string value = IniHelper.Read(section, key, SystemIniFilePath);

                int result;
                if (int.TryParse(value, out result))
                {
                    quantity = result;
                }
                else
                {
                    IniHelper.WritePrivateProfileString(section, key, quantity.ToString(), SystemIniFilePath);
                }
                return quantity;
            }

            /// <summary>
            ///     获取服务每隔多少分钟数执行
            /// </summary>
            /// <returns></returns>
            private static int GetRegularMinutes()
            {
                int minutes = 20;
                const string section = "system";
                const string key = "regularminutes";

                string value = IniHelper.Read(section, key, SystemIniFilePath);

                int result;
                if (int.TryParse(value, out result))
                {
                    minutes = result;
                }
                else
                {
                    IniHelper.WritePrivateProfileString(section, key, minutes.ToString(), SystemIniFilePath);
                }
                return minutes;
            }

            /// <summary>
            ///     定点发送数据的启动时间(24H)
            /// </summary>
            /// <returns></returns>
            private static DateTime GetStartTime()
            {
                TimeSpan ts;
                const string section = "system";
                const string key = "fixedTime";

                string fixedTime = IniHelper.Read(section, key, SystemIniFilePath);

                if (!TimeSpan.TryParse(fixedTime, out ts))
                {
                    IniHelper.WritePrivateProfileString(section, key, "00:00:00", SystemIniFilePath);
                    TimeSpan.TryParse("00:00:00", out ts);
                }

                return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, ts.Hours, ts.Minutes, 0);
            }

            /// <summary>
            ///     是否启动服务
            /// </summary>
            /// <returns></returns>
            public static bool IsStartService()
            {
                bool result = false;

                if (IsFixedTime() == false)
                {
                    //间隔执行
                    if (DateTime.Now.Minute%GetRegularMinutes() == 0)
                    {
                        result = true;
                    }
                }
                else
                {
                    //定时
                    const string section = "system";
                    const string key = "execDay";

                    var currentDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0); //2019-08-13 00:00:00.000
                    if (DateTime.Compare(currentDay, GetStartTime()) == 0 && IsRun() == false)
                    {
                        result = true;
                        IniHelper.WritePrivateProfileString(section, key, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), SystemIniFilePath);
                    }
                }

                return result;
            }

            /// <summary>
            /// 读取附件转化为Base64String
            /// </summary>
            /// <param name="id"></param>
            /// <param name="filePath"></param>
            /// <returns></returns>
            public static string ReadSealDoc(string id,string filePath)
            {
                System.IO.FileStream stream=null;
                try
                {
                    stream = System.IO.File.Open(filePath, System.IO.FileMode.Open);
                    var buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    stream.Close();

                    return Convert.ToBase64String(buffer);
                }
                catch (Exception exp)
                {
                    Log.ReadFileLog(id, filePath, exp);
                    throw;
                }
                finally
                {
                    if (stream != null)
                        stream.Close();
                }               
            }

            /// <summary>
            /// 将Base64String生成文件
            /// </summary>
            /// <param name="filePath">文件保存路径</param>
            /// <param name="fileContent">文件内容Base64String</param>
            public static void CreateFileByBase64String(string filePath,string fileContent)
            {
                byte[] s = Convert.FromBase64String(fileContent);

                System.IO.File.WriteAllBytes(filePath, s);
            }
        }
    }
}