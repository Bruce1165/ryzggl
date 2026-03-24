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
    public class BusinessInformation
    {
        /// <summary>
        /// 代码
        /// </summary>
        [JsonProperty("itemvalue")]
        public string Itemvalue { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty("itemtext")]
        public string Itemtext { get; set; }
    }
}
