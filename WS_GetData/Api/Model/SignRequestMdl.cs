using System;
using Newtonsoft.Json;

namespace WS_GetData.Api.Model
{
    /// <summary>
    ///     签发一张证照请求参数
    /// </summary>
    public class SignRequestMdl
    {
        /// <summary>
        /// </summary>
        [JsonProperty("data")]
        public SignRequestData Data { set; get; }
    }

    /// <summary>
    ///     ①license_code 和 id_code 不能同时为空。
    ///     ②如果 id_code 存在多条记录，默认获取已制证的电子证照，如果存在多条制证的电子证照，则提示报错。此种情况只能通过 license_code 的方式进行操作
    /// </summary>
    [Serializable]
    public class SignRequestData : BaseRequestMdl
    {
        /// <summary>
        ///     制证操作人信息
        /// </summary>
        [JsonProperty("operator")]
        public UserInfoMdl Operator { get; set; }

        /// <summary>
        ///     证照号码
        /// </summary>
        [JsonProperty("id_code")]
        public string IdCode { get; set; }

        /// <summary>
        ///     电子证照标识码
        /// </summary>
        [JsonProperty("license_code")]
        public string LicenseCode { get; set; }
    }
}