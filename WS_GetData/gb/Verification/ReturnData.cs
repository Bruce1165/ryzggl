using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WS_GetData.gb.Verification
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ReturnData
    {
        /// <summary>
        ///  错误数据
        /// </summary>
        [JsonProperty("ErrorData")]
        public List<ErrorData> ErrorData { get; set; }

        /// <summary>
        ///  警告数据
        /// </summary>
        [JsonProperty("WarnData")]
        public List<WarnData> WarnData { get; set; }
    }
}
