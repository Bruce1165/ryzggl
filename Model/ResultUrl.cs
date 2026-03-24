using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 跳转链接类
    /// </summary>
    [Serializable]
    public class ResultUrl
    {

        protected string _FunctionName;
        protected string _Url;

        /// <summary>
        /// 跳转链接
        /// </summary>
        /// <param name="functionName">功能名称</param>
        /// <param name="url">URL链接</param>
        public ResultUrl(string functionName, string url)
        {
            _FunctionName = functionName;
            _Url = url;
        }

        /// <summary>
        /// 功能名称
        /// </summary>
        public string FunctionName
        {
            get { return _FunctionName; }
            set { _FunctionName = value; }
        }
        /// <summary>
        /// URL链接
        /// </summary>
        public string Url
        {
            get { return _Url; }
            set { _Url = value; }
        }
    }
}
