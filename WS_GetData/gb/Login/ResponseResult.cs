using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace WS_GetData.gb.Login
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ResponseResult
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("controls")]
        public List<object> Controls { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("custom")]
        public Custom Custom { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("status")]
        public Status Status { get; set; }
    }
}
