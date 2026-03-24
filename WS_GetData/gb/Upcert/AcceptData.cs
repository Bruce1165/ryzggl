using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WS_GetData.gb.Upcert
{
    /// <summary>
    ///  电子证照预览地址归集请求数据
    /// </summary>
    public class AcceptData
    {
        /// <summary>
        ///  二维码赋码ID（不加密）
        /// </summary>
        [JsonProperty("ID")]
        public string ID { get; set; }

        /// <summary>
        /// 证照标识（《全国一体化政务服务平台 电子证照 建筑施工企业安全生产管理人员考核合格证书》标准附录A.2生成）
        /// </summary>
        [JsonProperty("zzeCertID")]
        public string ZzeCertID { get; set; }

        /// <summary>
        /// 证照预览地址
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

    }
}
