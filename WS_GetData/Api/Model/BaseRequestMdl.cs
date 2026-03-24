using System;
using Newtonsoft.Json;

namespace WS_GetData.Api.Model
{
    /// <summary>
    ///     基本请求参数
    /// </summary>
    [Serializable]
    public class BaseRequestMdl
    {
        /// <summary>
        ///     事项编码
        /// </summary>
        [JsonProperty("service_item_code")]
        public string ServiceItemCode { set; get; }

        /// <summary>
        ///     事项名称
        /// </summary>
        [JsonProperty("service_item_name")]
        public string ServiceItemName { set; get; }

        ///// <summary>
        /////     对应办件的业务流水号
        ///// </summary>
        //[JsonProperty("biz_num")]
        //public string BizNum { set; get; }

        ///// <summary>
        /////  签章状态
        ///// </summary>
        //[JsonProperty("sign_attach")]
        //public string SignAttach { set; get; }
    }
}