using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WS_GetData.gb.InCheck
{
    /// <summary>
    ///  接口错误数据
    /// </summary>
    [Serializable]
    public class ErrorData
    {
        /// <summary>
        ///  所属省份，按照民政部官网《2020年中华人民共和国行政区划代码》,C6
        /// </summary>
        [JsonProperty("provinceNum")]
        public string provinceNum { get; set; }

        /// <summary>
        /// 错误原因
        /// </summary>
        [JsonProperty("ErrorMsg")]
        public string ErrorMsg { get; set; }
    }
}
