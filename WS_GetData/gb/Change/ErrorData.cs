using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WS_GetData.gb.Change
{
    /// <summary>
    ///  变更证件接口错误数据
    /// </summary>
    [Serializable]
    public class ErrorData
    {
        /// <summary>
        ///  证书ID
        /// </summary>
        [JsonProperty("eCertID")]
        public string eCertID { get; set; }

        /// <summary>
        /// 错误原因
        /// </summary>
        [JsonProperty("ErrorMsg")]
        public string ErrorMsg { get; set; }
    }
}
