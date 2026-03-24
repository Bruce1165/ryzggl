using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace WS_GetData.Api
{
    /// <summary>
    /// 读写INI文件的类。
    /// </summary>
    public static class IniHelper
    {
        /// <summary>
        /// 参数说明：section：INI文件中的段落；key：INI文件中的关键字；val：INI文件中关键字的数值；filePath：INI文件的完整的路径和名称
        /// </summary>
        /// <param name="section">INI文件中的段落</param>
        /// <param name="key">INI文件中的关键字</param>
        /// <param name="val">INI文件中关键字的数值</param>
        /// <param name="filePath">INI文件的完整的路径和名称</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileString", CharSet = CharSet.Ansi)]
        public static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="def"></param>
        /// <param name="retVal"></param>
        /// <param name="size"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString", CharSet = CharSet.Ansi)]
        public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lpszReturnBuffer"></param>
        /// <param name="nSize"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileSectionNames", CharSet = CharSet.Ansi)]
        public static extern int GetPrivateProfileSectionNames(IntPtr lpszReturnBuffer, int nSize, string filePath);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lpAppName"></param>
        /// <param name="lpReturnedString"></param>
        /// <param name="nSize"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [DllImport("KERNEL32.DLL ", EntryPoint = "GetPrivateProfileSection", CharSet = CharSet.Ansi)]
        public static extern int GetPrivateProfileSection(string lpAppName, byte[] lpReturnedString, int nSize, string filePath);

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="iniName"></param>
        public static void Initialize(string iniName)
        {
            // 判断文件是否存在 
            var filePath = GetiniFilePath(iniName);
            var fileInfo = new FileInfo(filePath);
            if ((!fileInfo.Exists))
            {
                //初始化
                const string section = "System";
                const string key = "ExecSendDay";
                DateTime lastSendDay = Convert.ToDateTime("2016-07-01");
                WritePrivateProfileString(section, key, lastSendDay.ToString("yyyy-MM-dd HH:mm:ss"), filePath);
            }
        }

        /// <summary>
        /// 获取iniName存放路径
        /// </summary>
        /// <param name="iniName"></param>
        /// <returns></returns>
        public static string GetiniFilePath(string iniName)
        {
            return AppDomain.CurrentDomain.BaseDirectory + iniName + ".ini";
        }

        /// <summary>
        /// 向INI写入数据。
        /// </summary>
        /// <param name="section">节点名。</param>
        /// <param name="key">键名。</param>
        /// <param name="value">值名。</param>
        /// <param name="path"></param>
        public static void Write(string section, string key, string value, string path)
        {
            WritePrivateProfileString(section, key, value, path);
        }

        /// <summary>
        /// 读取INI数据。
        /// </summary>
        /// <param name="section">节点名。</param>
        /// <param name="key">键名。</param>
        /// <param name="path"></param>
        /// <returns>相应的值。</returns>
        public static string Read(string section, string key, string path)
        {
            var temp = new StringBuilder(255);
            int i = GetPrivateProfileString(section, key, "", temp, 255, path);
            return temp.ToString();
        }

        /// <summary>
        /// 读取一个ini里面所有的节
        /// </summary>
        /// <param name="sections"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static int GetAllSectionNames(out string[] sections, string path)
        {
            int MAX_BUFFER = 32767;
            IntPtr pReturnedString = Marshal.AllocCoTaskMem(MAX_BUFFER);
            int bytesReturned = GetPrivateProfileSectionNames(pReturnedString, MAX_BUFFER, path);
            if (bytesReturned == 0)
            {
                sections = null;
                return -1;
            }
            string local = Marshal.PtrToStringAnsi(pReturnedString, bytesReturned);
            Marshal.FreeCoTaskMem(pReturnedString);
            sections = local.Substring(0, local.Length - 1).Split('\0');
            return 0;
        }

        /// <summary>
        /// 得到某个节点下面所有的key和value组合
        /// </summary>
        /// <param name="section"></param>
        /// <param name="keys"></param>
        /// <param name="values"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static int GetAllKeyValues(string section, out string[] keys, out string[] values, string path)
        {
            var b = new byte[65535];

            GetPrivateProfileSection(section, b, b.Length, path);
            string s = Encoding.Default.GetString(b);
            string[] tmp = s.Split((char)0);
            var result = new ArrayList();
            foreach (string r in tmp)
            {
                if (r != string.Empty)
                    result.Add(r);
            }
            keys = new string[result.Count];
            values = new string[result.Count];
            for (int i = 0; i < result.Count; i++)
            {
                string[] item = result[i].ToString().Split(new[] { '=' });
                if (item.Length == 2)
                {
                    keys[i] = item[0].Trim();
                    values[i] = item[1].Trim();
                }
                else if (item.Length == 1)
                {
                    keys[i] = item[0].Trim();
                    values[i] = "";
                }
                else if (item.Length == 0)
                {
                    keys[i] = "";
                    values[i] = "";
                }
            }
            return 0;
        }
    }
}
