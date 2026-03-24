using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WS_GetData.gb.Verification
{
    /// <summary>
    /// 
    /// </summary>
    public class Request<T> where T : class
    {
        /// <summary>
        /// 推送数据
        /// </summary>
        [JsonProperty("AcceptData")]
        public List<T> AcceptData { get; set; }
    }
}
