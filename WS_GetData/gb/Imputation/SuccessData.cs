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
    public class SuccessData
    {
        ///// <summary>
        ///// 
        ///// </summary>
        //[JsonProperty("ReturnMsg")]
        //public string ReturnMsg { get; set; }

        /// <summary>
        /// 赋码ID（加密）
        /// </summary>
        [JsonProperty("encryCertid")]
        public string EncryCertid { get; set; }

        /// <summary>
        /// 赋码秘钥（加密）
        /// </summary>
        [JsonProperty("encryKey")]
        public string EncryKey { get; set; }

        /// <summary>
        /// 业务数据ID
        /// </summary>
        [JsonProperty("eCertID")]
        public string ECertID { get; set; }
    }
}
