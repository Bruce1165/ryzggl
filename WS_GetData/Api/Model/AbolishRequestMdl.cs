using System;
using Newtonsoft.Json;

namespace WS_GetData.Api.Model
{
    /// <summary>
    ///     废止一张电子证照
    /// </summary>
    [Serializable]
    public class AbolishRequestMdl
    {
        /// <summary>
        ///     废止请求数据
        /// </summary>
        [JsonProperty("data")]
        public AbolishRequestData Data { set; get; }
    }

    /// <summary>
    ///     废止请求数据
    /// </summary>
    [Serializable]
    public class AbolishRequestData : BaseRequestMdl
    {
        /// <summary>
        ///     电子证照标识码
        /// </summary>
        [JsonProperty("license_code")]
        public string LicenseCode { set; get; }

        /// <summary>
        ///     证照号码
        /// </summary>
        [JsonProperty("id_code")]
        public string IdCode { set; get; }

        /// <summary>
        ///     制证操作人信息
        /// </summary>
        [JsonProperty("operator")]
        public UserInfoMdl Operator { get; set; }

        /// <summary>
        ///     对应办件的业务流水号
        /// </summary>
        [JsonProperty("biz_num")]
        public string BizNum { get; set; }
        
    }
}