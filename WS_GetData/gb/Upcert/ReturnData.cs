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
    public class ReturnData
    {
        /// <summary>
        /// 错误ID
        /// </summary>
        [JsonProperty("ErrorGuid")]
        public string ErrorGuid { get; set; }

        /// <summary>
        ///  错误数据
        /// </summary>
        [JsonProperty("ErrorData")]
        public List<ErrorData> ErrorData { get; set; }
    }
}
