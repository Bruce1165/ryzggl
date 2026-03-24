using System;
using Newtonsoft.Json;

namespace WS_GetData.Api.Model
{
    /// <summary>
    ///     错误信息
    /// </summary>
    [Serializable]
    public class Error
    {
        /// <summary>
        ///     错误代码
        /// </summary>
        [JsonProperty("code")]
        public string Code { set; get; }

        /// <summary>
        ///     错误信息描述
        /// </summary>
        [JsonProperty("message")]
        public string Message { set; get; }

        /// <summary>
        ///     内部代码
        /// </summary>
        [JsonProperty("inner_code")]
        public string InnerCode { set; get; }
    }
}