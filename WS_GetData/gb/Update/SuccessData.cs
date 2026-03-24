using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace WS_GetData.gb.Update
{
    /// <summary>
    /// 变更证件接口成功信息
    /// </summary>
    [Serializable]
    public class SuccessData
    {
        /// <summary>
        /// 返回
        /// </summary>
        [JsonProperty("ReturnMsg")]
        public string ReturnMsg { get; set; }

        /// <summary>
        /// 证书编号
        /// </summary>
        [JsonProperty("certNum")]
        public string certNum { get; set; }

      
        ///// <summary>
        ///// 
        ///// </summary>
        //[JsonProperty("eCertID")]
        //public string ECertID { get; set; }
    }
}
