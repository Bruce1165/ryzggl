using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace WS_GetData.gb.InCheck
{
    /// <summary>
    /// 接口响应请求数据
    /// </summary>
    [Serializable]
    public class ResponseResult
    {
        /// <summary>
        /// 处理结果编码：0=有错误，1=成功
        /// </summary>
        [JsonProperty("ReturnCode")]
        public string ReturnCode { get; set; }

        /// <summary>
        /// 处理结果消息
        /// </summary>
        [JsonProperty("ReturnMsg")]
        public string ReturnMsg { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        [JsonProperty("ReturnData")]
        public ReturnData ReturnData { get; set; }

        /// <summary>
        /// 记录总数
        /// </summary>
        [JsonProperty("TotalCount")]
        public int TotalCount { get; set; }

    }
}
