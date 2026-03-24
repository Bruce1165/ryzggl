using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WS_GetData.gb.OutCheck
{
    /// <summary>
    ///  三类人证书转出数据校验请求
    /// </summary>
    public class AcceptData
    {
        /// <summary>
        ///     证书编号（建筑施工企业安全生产管理人员考核合格证书的唯一编号）,证照编号和身份证件号码必填一项
        /// </summary>
        [JsonProperty("certNum")]
        public string certNum { get; set; }

        ///// <summary>
        /// 身份证件号码(持证人员的有效身份证件号),
        /// </summary>
        [JsonProperty("identityCard")]
        public string identityCard { get; set; }
        
    }
}
