using System;
using Newtonsoft.Json;

namespace WS_GetData.Api.Model
{
    /// <summary>
    ///     创建制证数据返回结果
    /// </summary>
    [Serializable]
    public class CreateResponseMdl
    {
        /// <summary>
        ///     电子证照标识码
        /// </summary>
        [JsonProperty("license_code")]
        public string LicenseCode { set; get; }

        /// <summary>
        ///     电子证照查验码
        /// </summary>
        [JsonProperty("auth_code")]
        public string AuthCode { set; get; }
    }
}