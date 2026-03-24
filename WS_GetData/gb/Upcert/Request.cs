using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace WS_GetData.gb.Upcert
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Request<T> where T : class
    {
        /// <summary>
        /// 推送数据
        /// </summary>
        [JsonProperty("AcceptData")]
        public List<T> AcceptData { get; set; }
    }
}
