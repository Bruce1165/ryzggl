using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace WS_GetData.gb.Verification
{
    /// <summary>
    ///     返回结果
    /// </summary>
    [Serializable]
    public class ResponseResult
    {
        /// <summary>
        ///     返回状态码  0 失败  1 成功  2 警告
        /// </summary>
        [JsonProperty("ReturnCode")]
        public string ReturnCode { get; set; }

        /// <summary>
        ///     返回信息
        /// </summary>
        [JsonProperty("ReturnMsg")]
        public string ReturnMsg { get; set; }

        /// <summary>
        ///     返回的错误数据
        /// </summary>
        [JsonProperty("ReturnData")]
        public ReturnData ReturnData { get; set; }
    }
}
