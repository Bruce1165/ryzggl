using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WS_GetData.gb.Verification
{
    /// <summary>
    ///  警告信息
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
        ///  警告原因
        /// </summary>
        [JsonProperty("WarnMsg")]
        public string WarnMsg { get; set; }
    }
}
