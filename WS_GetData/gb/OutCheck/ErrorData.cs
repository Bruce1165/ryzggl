using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WS_GetData.gb.OutCheck
{
    /// <summary>
    ///  变更证件接口错误数据
    /// </summary>
    [Serializable]
    public class ErrorData
    {
        /// <summary>
        ///  证书编号
        /// </summary>
        [JsonProperty("certNum")]
        public string certNum { get; set; }

        /// <summary>
        /// 错误原因
        /// </summary>
        [JsonProperty("ErrorMsg")]
        public string ErrorMsg { get; set; }
    }
}
