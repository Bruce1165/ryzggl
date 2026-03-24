using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WS_GetData.gb.Verification.Aqscglry
{
    /// <summary>
    ///  三类人数据校验请求数据
    /// </summary>
    public class AcceptData
    {
        /// <summary>
        ///  业务数据ID
        /// </summary>
        [JsonProperty("eCertID")]
        public string eCertID { get; set; }

        /// <summary>
        ///     所属省份(按照民政部官网《2020年中华人民共和国行政区划代码》)
        /// </summary>
        [JsonProperty("provinceNum")]
        public string ProvinceNum { get; set; }

        /// <summary>
        ///     身份证件号码(持证人员的有效身份证件号)
        /// </summary>
        [JsonProperty("identityCard")]
        public string IdentityCard { get; set; }

        /// <summary>
        ///     岗位类别代码（详见7.3.3.2岗位类别字典表）
        /// </summary>
        [JsonProperty("categoryCode")]
        public string CategoryCode { get; set; }

        /// <summary>
        ///     统一社会信用代码（持该证书的安管人员受聘企业的统一社会信用代码）
        /// </summary>
        [JsonProperty("creditCode")]
        public string CreditCode { get; set; }

        /// <summary>
        ///     证书编号（证书唯一编号，按照《全国一体化政务服务平台电子证照 建筑施工企业安全生产管理人员考核合格证书》附录A.1编号规则生成），操作类型是办理新发电子证照时，非必填，其他的操作类型要求必填。
        /// </summary>
        [JsonProperty("certNum")]
        public string CertNum { get; set; }

        /// <summary>
        /// 操作类型：
        /// 01	办理新发电子证照
        /// 02	办理延续
        /// 03	办理暂扣
        /// 04	办理过期失效
        /// 05	办理注销（撤销、吊销、办理转出）
        /// 06	办理暂扣发还
        /// 07	办理其他业务
        /// 08	办理转入
        /// 09	办理变更
        /// </summary>
        [JsonProperty("operateType")]
        public string operateType { get; set; }

        /// <summary>
        /// 职务:
        /// 01	法定代表人
        /// 02	总经理（总裁）
        /// 03	分管安全生产的副总经理（副总裁 ）
        /// 04	分管生产经营的副总经理（副总裁）
        /// 05	技术负责人
        /// 06	安全总监
        /// 07	项目负责人（项目经理）
        /// 08	专职安全生产管理人员
        /// 99	其他
        /// </summary>
        [JsonProperty("appointment")]
        public string appointment { get; set; }

        /// <summary>
        /// 转出省原证照编号:当操作类型是办理转入时，要求必填
        /// </summary>
        [JsonProperty("oldcertNum")]
        public string oldcertNum { get; set; }
    }
}
