using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace WS_GetData.gb.Imputation
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ReturnData
    {
        /// <summary>
        /// 警告信息
        /// </summary>
        [JsonProperty("WarnData")]
        public List<WarnData> WarnData { get; set; }

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
