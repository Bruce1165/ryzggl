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
    public class WarnData
    {
        /// <summary>
        /// 警告ID
        /// </summary>
        [JsonProperty("WarnGuid")]
        public string WarnGuid { get; set; }

        /// <summary>
        /// 警告信息
        /// </summary>
        [JsonProperty("WarnMsg")]
        public string WarnMsg { get; set; }
    }
}
