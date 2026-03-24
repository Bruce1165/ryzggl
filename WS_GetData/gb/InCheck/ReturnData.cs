using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace WS_GetData.gb.InCheck
{
    /// <summary>
    /// 接口调用返回信息
    /// </summary>
    [Serializable]
    public class ReturnData
    {
        /// <summary>
        /// 成功信息
        /// </summary>
        [JsonProperty("SuccessData")]
        public List<SuccessData> SuccessData { get; set; }

        /// <summary>
        ///  错误数据
        /// </summary>
        [JsonProperty("ErrorData")]
        public List<ErrorData> ErrorData { get; set; }
    }
}
