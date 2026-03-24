using System;
using System.Configuration;

namespace DataAccess
{
    /// <summary>
    /// MyWebConfig 读取WebConfig配置项
    /// </summary>
    public static class MyWebConfig
    {
        static MyWebConfig()
        {
        }
       
        /// <summary>
        /// 数据库联接字符串Seed
        /// </summary>
        public static string Exam
        {
            get { return ConfigurationManager.ConnectionStrings["Exam"].ConnectionString; }
        }

        public static string GetConnectionStrings(string dataBase)
        {
            return ConfigurationManager.ConnectionStrings[dataBase].ConnectionString;
        }

        /// <summary>
        /// 是否为外阜系统，0=不是外阜系统，1=是
        /// </summary>
        public static string IsForeign
        {
            get { return ConfigurationManager.AppSettings["IsForeign"]; }
        }

        /// <summary>
        /// 数据库导出服务地址
        /// </summary>
        public static string DBOutputUrl
        {
            get { return ConfigurationManager.AppSettings["DBOutputUrl"]; }
        }

        public static string DBOutputPath
        {
            get { return ConfigurationManager.AppSettings["DBOutputPath"]; }
        }

        /// <summary>
        /// 电子证书根目录
        /// </summary>
        public static string CAFile
        {
            get { return ConfigurationManager.AppSettings["CAFile"]; }
        }

        /// <summary>
        /// 社保实时查询地址
        /// </summary>
        public static string SheBaoSearch
        {
            get { return ConfigurationManager.AppSettings["SheBaoSearch"]; }
        }

        /// <summary>
        /// 服务器类型，zw：专网；ww：外网
        /// </summary>
        public static string serverType
        {
            get { return ConfigurationManager.AppSettings["serverType"]; }
        }

        /// <summary>
        /// 外网根地址：http://120.52.185.14
        /// </summary>
        public static string wwRoot
        {
            get { return ConfigurationManager.AppSettings["ww"]; }
        }

        /// <summary>
        /// 专网根地址：http://172.26.68.122
        /// </summary>
        public static string zwRoot
        {
            get { return ConfigurationManager.AppSettings["zw"]; }
        }

        ///// <summary>
        ///// 证书电子照片存放地址
        ///// </summary>
        //public static string CertPhotoUrl
        //{
        //    get { return ConfigurationManager.AppSettings["CertPhotoUrl"]; }
        //}

        ///// <summary>
        ///// 网站根目录
        ///// </summary>
        //public static string VirtualPath
        //{
        //    get { return ConfigurationManager.AppSettings["VirtualPath"]; }
        //}

        ///// <summary>
        /////  系统名称
        ///// </summary>
        //public static string SysName
        //{
        //    get { return ConfigurationManager.AppSettings["SysName"]; }
        //}    

        ///// <summary>
        ///// 版权信息
        ///// </summary>
        //public static string CopyRight
        //{
        //    get { return ConfigurationManager.AppSettings["CopyRight"]; }
        //}

    }
}
