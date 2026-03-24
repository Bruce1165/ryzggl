using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WS_GetData.gb.Change.Aqscglry
{
    /// <summary>
    ///  证书证件号码变更请求数据
    /// </summary>
    public class AcceptData
    {
        /// <summary>
        ///  证书ID
        /// </summary>
        [JsonProperty("eCertID")]
        public string eCertID { get; set; }

        /// <summary>
        /// 所属省份 110000
        /// </summary>
        [JsonProperty("provinceNum")]
        public string provinceNum { get; set; }

         /// <summary>
        /// 证书编号
        /// </summary>
        [JsonProperty("certNum")]
        public string certNum { get; set; }

         /// <summary>
        /// 岗位类别代码，详见安管人员证-岗位类别字典表
        /// </summary>
        [JsonProperty("categoryCode")]
        public string categoryCode { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        [JsonProperty("birthDate")]
        public string birthDate { get; set; }

         /// <summary>
        /// 证件类型：【非必填】，可取值为公民身份号码、护照号等，从GB/T 36903-2018的附录A中选取，【可变更】。
        /// </summary>
        [JsonProperty("identityCardType")]
        public string identityCardType { get; set; }

        /// <summary>
        /// 证件号码，【可变更】
        /// </summary>
        [JsonProperty("identityCard")]
        public string identityCard { get; set; }

        /// <summary>
        /// 证照预览地址：【非必填】，【可变更】。
        /// </summary>
        [JsonProperty("url")]
        public string url { get; set; }


    }
}
